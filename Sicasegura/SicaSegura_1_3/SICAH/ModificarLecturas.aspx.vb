Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports SICA_FuncionesLecturas

Partial Class SICAH_ModificarLecturas
    Inherits System.Web.UI.Page

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim da As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As System.Data.DataSet = New System.Data.DataSet()
    Dim dt As New System.Data.DataTable()
    Dim fechaActual As String = Replace(Today(), "/", "") & Today.Hour & Today.Minute & Today.Second

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        renellarDataSets()
        If Request.QueryString("SubirLecturas") = "" Then
            cargarDatos()
        Else
            cargarDatosSubirLecturas()
        End If
    End Sub

    Private Sub renellarDataSets()
        Try
            Response.CacheControl = "no-cache"
            dt = Session("dst")
        Catch ex As Exception
            JavaScript.Alert(Page, "Por favor, vuelve a cargar los datos de las lecturas que ya no están en memoria")
            Response.End()
        End Try
    End Sub

    Private Sub cargarDatos()
        Dim strCarpeta As String = ""
        Select Case Request.QueryString("tipoElem")
            Case "Q"
                strCarpeta = "Caudal"
            Case "E"
                strCarpeta = "Alimentacion"
            Case "V"
                strCarpeta = "Motores"
            Case "H"
                strCarpeta = "Horometros"
        End Select

        File.Copy(Server.MapPath("./ExcelTemp/" & strCarpeta & "/Plantilla.xls"), Server.MapPath("./ExcelTemp/" & strCarpeta & "/" & fechaActual & "_" & Request.QueryString("codigoPVYCR") & "_" & Session.SessionID & ".xls"), True)

        Dim sCs As String = "provider=Microsoft.Jet.OLEDB.4.0; " & "data source=" & Server.MapPath("./ExcelTemp/" & strCarpeta & "/" & fechaActual & "_" & Request.QueryString("codigoPVYCR") & "_" & Session.SessionID & ".xls") & "; Extended Properties=Excel 8.0;"

        Dim objOleConnection As System.Data.OleDb.OleDbConnection = New System.Data.OleDb.OleDbConnection(sCs)
        objOleConnection.Open()

        Dim comando As System.Data.OleDb.OleDbCommand = New System.Data.OleDb.OleDbCommand("", objOleConnection)
        Dim sSheetName As String = "Hoja1"
        Dim sRange As String = ""

        Dim numFilas As Integer = dt.Rows.Count - 1

        'Esto lo pongo para que se muestre el excel con los datos con el formato que tienen de verdad o para cuando se quiera generar
        'un excel para subirlo luego modificado.

        Dim SubirLecturas As Boolean
        If Request.QueryString("SubirLecturas") = "" Then
            SubirLecturas = False
        Else
            SubirLecturas = True
        End If


        Select Case Request.QueryString("tipoElem")
            Case "Q"

                For i As Integer = 0 To numFilas
                    comando.CommandText = "insert into " & "[" & sSheetName & "$" & sRange & "] (CodigoPVYCR,#; idElementoMedida,#; FechaMedida,#; TipoObtencionCaudal,#; [Codigo Fuente Dato],#; Escala,#; Calado,#; " & _
                                "RegimenCurva,#; Parada,#; Caudal,#; Parcial,#; DudaCalidad,#; Observaciones) VALUES ('" & dt.Rows(i).Item("codigoPVYCR") & "',#; '" & _
                                dt.Rows(i).Item("idElementoMedida") & "',#; '" & dt.Rows(i).Item("Fecha_Medida") & "',#; '" & _
                                dt.Rows(i).Item("tipoObtencionCaudal") & "',#; '" & dt.Rows(i).Item("Cod_Fuente_Dato") & "',#; " & transformarDatos(dt.Rows(i).Item("Escala_M"), "Decimal") & _
                                ",#; " & transformarDatos(dt.Rows(i).Item("Calado_M"), "Decimal") & ",#; '" & transformarDatos(dt.Rows(i).Item("RegimenCurva"), "Char") & _
                                "',#; " & transformarDatos(dt.Rows(i).Item("NumeroParada"), "Integer") & ",#; " & transformarDatos(dt.Rows(i).Item("Caudal_M3S"), "Decimal") & _
                                ",#; " & transformarDatos(dt.Rows(i).Item("Diferencial"), "Decimal") & ",#; " & transformarDatos(dt.Rows(i).Item("Duda_Calidad"), "Integer") & _
                                ",#; '" & transformarDatos(dt.Rows(i).Item("Observaciones"), "Char") & "')"


                    comando.CommandText = quitarNulos(comando.CommandText)
                    comando.ExecuteNonQuery()
                Next

            Case "E"
                For i As Integer = 0 To numFilas
                    comando.CommandText = "insert into " & "[" & sSheetName & "$" & sRange & "] (CodigoPVYCR,#; idElementoMedida,#; FechaMedida,#; " & _
                                "[Codigo Fuente Dato],#; LecturaI,#; LecturaII,#; LecturaIII,#; TotalKwh,#; Parcial,#; TotalKvar,#; " & _
                                "Funciona,#; Justificacion,#; idIncidenciaElectrica,#; ConsumoElectricoAdicional,#; ReinicioLecturaElectrica,#; Observaciones) " & _
                                "VALUES ('" & dt.Rows(i).Item("codigoPVYCR") & "',#; '" & dt.Rows(i).Item("idElementoMedida") & _
                                "',#; '" & dt.Rows(i).Item("Fecha_Medida") & "',#; '" & dt.Rows(i).Item("Cod_Fuente_Dato") & "',#; " & _
                                transformarDatos(dt.Rows(i).Item("LecturaI"), "Double") & ",#; " & transformarDatos(dt.Rows(i).Item("LecturaII"), "Double") & _
                                ",#; " & transformarDatos(dt.Rows(i).Item("LecturaIII"), "Double") & ",#; " & transformarDatos(dt.Rows(i).Item("Total_Kwh"), "Double") & _
                                ",#; " & transformarDatos(dt.Rows(i).Item("Diferencial"), "Double") & ",#; " & transformarDatos(dt.Rows(i).Item("Total_Kvar"), "Double") & _
                                ",#; '" & transformarDatos(dt.Rows(i).Item("Funciona"), "Char") & "',#; '" & transformarDatos(dt.Rows(i).Item("Justificacion"), "Char") & _
                                "',#; " & transformarDatos(dt.Rows(i).Item("idIncidenciaElectrica"), "Integer") & ",#; " & _
                                transformarDatos(dt.Rows(i).Item("ConsumoElectricoAdicional"), "Double") & ",#; " & transformarDatos(dt.Rows(i).Item("ReinicioLecturaElectrica"), "Double") & _
                                ",#; '" & transformarDatos(dt.Rows(i).Item("Observaciones"), "Char") & "')"

                    If transformarDatos(dt.Rows(i).Item("LecturaI"), "Double") Is System.DBNull.Value Then
                        Dim para As Boolean = True
                    End If
                    comando.CommandText = quitarNulos(comando.CommandText)
                    comando.ExecuteNonQuery()
                Next
            Case "V"
                
                For i As Integer = 0 To numFilas
                    If i = 2 Then
                        Dim para = "si"
                    End If
                    comando.CommandText = "insert into " & "[" & sSheetName & "$" & sRange & "] (CodigoPVYCR,#; idElementoMedida,#; FechaMedida,#; " & _
                                "[Codigo Fuente Dato],#; LecturaContador,#; Parcial,#; Funciona,#; CaudalMedido,#; Justificacion,#; idIncidenciaVolumetrica,#; " & _
                                "ConsumoVolumetricoAdicional,#; ReinicioLecturaVolumetrica,#; Observaciones) " & _
                                "VALUES ('" & dt.Rows(i).Item("codigoPVYCR") & "',#; '" & dt.Rows(i).Item("idElementoMedida") & _
                                "',#; '" & dt.Rows(i).Item("Fecha_Medida") & "',#; '" & dt.Rows(i).Item("Cod_Fuente_Dato") & "',#; " & _
                                transformarDatos(dt.Rows(i).Item("LecturaContador_M3"), "Double") & ",#; " & transformarDatos(dt.Rows(i).Item("Diferencial"), "Double") & _
                                ",#; '" & transformarDatos(dt.Rows(i).Item("Funciona"), "Char") & "',#; " & transformarDatos(dt.Rows(i).Item("CaudalMedido"), "Integer") & _
                                ",#; '" & transformarDatos(dt.Rows(i).Item("Justificacion"), "Char") & "',#; " & transformarDatos(dt.Rows(i).Item("idIncidenciaVolumetrica"), "Integer") & _
                                ",#; " & transformarDatos(dt.Rows(i).Item("ConsumoVolumetricoAdicional"), "Double") & ",#; " & transformarDatos(dt.Rows(i).Item("ReinicioLecturaVolumetrica"), "Double") & _
                                ",#; '" & transformarDatos(dt.Rows(i).Item("Observaciones"), "Char") & "')"

                    comando.CommandText = quitarNulos(comando.CommandText)
                    comando.ExecuteNonQuery()
                Next
            Case "H"

                For i As Integer = 0 To numFilas
                    comando.CommandText = "insert into " & "[" & sSheetName & "$" & sRange & "] (CodigoPVYCR,#; idElementoMedida,#; FechaMedida,#; " & _
                                "[Codigo Fuente Dato],#; LecturaHorometro,#; Parcial,#; Funciona,#; idIncidenciaVolumetrica,#; " & _
                                "ConsumoVolumetricoAdicional,#; ReinicioLecturaVolumetrica,#; Observaciones) " & _
                                "VALUES ('" & dt.Rows(i).Item("codigoPVYCR") & "',#; '" & dt.Rows(i).Item("idElementoMedida") & _
                                "',#; '" & dt.Rows(i).Item("Fecha_Medida") & "',#; '" & dt.Rows(i).Item("Cod_Fuente_Dato") & "',#; " & _
                                transformarDatos(dt.Rows(i).Item("HorasIntervalo"), "Double") & ",#; " & transformarDatos(dt.Rows(i).Item("Diferencial"), "Double") & _
                                ",#; '" & transformarDatos(dt.Rows(i).Item("Funciona"), "Char") & "',#; " & transformarDatos(dt.Rows(i).Item("idIncidenciaVolumetrica"), "Integer") & _
                                ",#; " & transformarDatos(dt.Rows(i).Item("ConsumoVolumetricoAdicional"), "Double") & ",#; " & _
                                transformarDatos(dt.Rows(i).Item("ReinicioLecturaVolumetrica"), "Double") & ",#; '" & transformarDatos(dt.Rows(i).Item("Observaciones"), "Double") & "')"

                    comando.CommandText = quitarNulos(comando.CommandText)
                    comando.ExecuteNonQuery()
                Next
        End Select

        objOleConnection.Close()

        Dim nombreArchivo As String = fechaActual & "_" & Request.QueryString("codigoPVYCR") & "_" & Session.SessionID & ".xls"
        Dim rutaArchivo As String = Server.MapPath("./ExcelTemp/" & strCarpeta & "/" & fechaActual & "_" & Request.QueryString("codigoPVYCR") & "_" & Session.SessionID & ".xls")
        Response.ClearHeaders()
        Response.ClearContent()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "filename=" & nombreArchivo)
        Response.Flush()
        Response.WriteFile(rutaArchivo)
        'Response.End()

        'Dim elArchivo() As Byte = File.ReadAllBytes(Server.MapPath("./ExcelTemp/" & Session.SessionID & ".xls"))
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.BinaryWrite(elArchivo)
        'File.Delete(rutaArchivo)
    End Sub


    Private Sub cargarDatosSubirLecturas()
        Dim strCarpeta As String = ""
        Select Case Request.QueryString("tipoElem")
            Case "Q"
                strCarpeta = "Caudal"
            Case "E"
                strCarpeta = "Alimentacion"
            Case "V"
                strCarpeta = "Motores"
            Case "H"
                strCarpeta = "Horometros"
        End Select

        File.Copy(Server.MapPath("./ExcelTemp/" & strCarpeta & "/PlantillaSubirLecturas.xls"), Server.MapPath("./ExcelTemp/" & strCarpeta & "/" & fechaActual & "_" & Request.QueryString("codigoPVYCR") & "_" & Session.SessionID & ".xls"), True)

        Dim sCs As String = "provider=Microsoft.Jet.OLEDB.4.0; " & "data source=" & Server.MapPath("./ExcelTemp/" & strCarpeta & "/" & fechaActual & "_" & Request.QueryString("codigoPVYCR") & "_" & Session.SessionID & ".xls") & "; Extended Properties=Excel 8.0;"

        Dim objOleConnection As System.Data.OleDb.OleDbConnection = New System.Data.OleDb.OleDbConnection(sCs)
        objOleConnection.Open()

        Dim comando As System.Data.OleDb.OleDbCommand = New System.Data.OleDb.OleDbCommand("", objOleConnection)
        Dim sSheetName As String = "Hoja1"
        Dim sRange As String = ""

        Dim numFilas As Integer = dt.Rows.Count - 1

        'Esto lo pongo para que se muestre el excel con los datos con el formato que tienen de verdad o para cuando se quiera generar
        'un excel para subirlo luego modificado.


        Select Case Request.QueryString("tipoElem")
            Case "Q"
                Dim daOledb As System.Data.OleDb.OleDbDataAdapter = New System.Data.OleDb.OleDbDataAdapter("", objOleConnection)
                Dim dstOledb As New DataSet()
                daOledb.SelectCommand.CommandText = "Select * from [" & sSheetName & "$" & sRange & "]"
                daOledb.Fill(dst, "tablaProv")

                For i As Integer = 0 To numFilas
                    comando.CommandText = "insert into " & "[" & sSheetName & "$" & sRange & "] (CodigoPVYCR, idElementoMedida, FechaMedida, " & _
                                "TipoObtencionCaudal, CodigoFuenteDato, Escala, Calado, RegimenCurva, Parada, Caudal, Parcial, DudaCalidad, " & _
                                "Observaciones) VALUES ('" & dt.Rows(i).Item("codigoPVYCR") & "', '" & dt.Rows(i).Item("idElementoMedida") & _
                                "', '" & dt.Rows(i).Item("Fecha_Medida") & "', '" & dt.Rows(i).Item("tipoObtencionCaudal") & "', '" & _
                                dt.Rows(i).Item("Cod_Fuente_Dato") & "', '" & transformarDatos2(dt.Rows(i).Item("Escala_M")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("Calado_M")) & "', '" & transformarDatos2(dt.Rows(i).Item("RegimenCurva")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("NumeroParada")) & "', '" & transformarDatos2(dt.Rows(i).Item("Caudal_M3S")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("Diferencial")) & "', '" & transformarDatos2(dt.Rows(i).Item("Duda_Calidad")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("Observaciones")) & "')"
                    comando.ExecuteNonQuery()
                Next
            Case "E"
                For i As Integer = 0 To numFilas
                    comando.CommandText = "insert into " & "[" & sSheetName & "$" & sRange & "] (CodigoPVYCR, idElementoMedida, FechaMedida, " & _
                                "CodigoFuenteDato, LecturaI, LecturaII, LecturaIII, TotalKwh, Parcial, TotalKvar, " & _
                                "Funciona, Justificacion, idIncidenciaElectrica, ConsumoElectricoAdicional, ReinicioLecturaElectrica, Observaciones) " & _
                                "VALUES ('" & dt.Rows(i).Item("codigoPVYCR") & "', '" & dt.Rows(i).Item("idElementoMedida") & _
                                "', '" & dt.Rows(i).Item("Fecha_Medida") & "', '" & dt.Rows(i).Item("Cod_Fuente_Dato") & "', " & _
                                "'" & transformarDatos2(dt.Rows(i).Item("LecturaI")) & "', '" & transformarDatos2(dt.Rows(i).Item("LecturaII")) & _
                                "', '" & transformarDatos2(dt.Rows(i).Item("LecturaIII")) & "', '" & transformarDatos2(dt.Rows(i).Item("Total_Kwh")) & _
                                "', '" & transformarDatos2(dt.Rows(i).Item("Diferencial")) & "', '" & transformarDatos2(dt.Rows(i).Item("Total_Kvar")) & _
                                "', '" & transformarDatos2(dt.Rows(i).Item("Funciona")) & "', '" & transformarDatos2(dt.Rows(i).Item("Justificacion")) & _
                                "', '" & transformarDatos2(dt.Rows(i).Item("idIncidenciaElectrica")) & "', " & _
                                "'" & transformarDatos2(dt.Rows(i).Item("ConsumoElectricoAdicional")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("ReinicioLecturaElectrica")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("Observaciones")) & "')"
                    comando.ExecuteNonQuery()
                Next
            Case "V"
                For i As Integer = 0 To numFilas
                    comando.CommandText = "insert into " & "[" & sSheetName & "$" & sRange & "] (CodigoPVYCR, idElementoMedida, FechaMedida, " & _
                                "CodigoFuenteDato, LecturaContador, Parcial, Funciona, CaudalMedido, Justificacion, idIncidenciaVolumetrica, " & _
                                "ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica, Observaciones) " & _
                                "VALUES ('" & dt.Rows(i).Item("codigoPVYCR") & "', '" & dt.Rows(i).Item("idElementoMedida") & _
                                "', '" & dt.Rows(i).Item("Fecha_Medida") & "', '" & dt.Rows(i).Item("Cod_Fuente_Dato") & "', " & _
                                "'" & transformarDatos2(dt.Rows(i).Item("LecturaContador_M3")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("Diferencial")) & "', '" & transformarDatos2(dt.Rows(i).Item("Funciona")) & _
                                "', '" & transformarDatos2(dt.Rows(i).Item("CaudalMedido")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("Justificacion")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("idIncidenciaVolumetrica")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("ConsumoVolumetricoAdicional")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("ReinicioLecturaVolumetrica")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("Observaciones")) & "')"
                    comando.ExecuteNonQuery()
                Next
            Case "H"
                For i As Integer = 0 To numFilas
                    comando.CommandText = "insert into " & "[" & sSheetName & "$" & sRange & "] (CodigoPVYCR, idElementoMedida, FechaMedida, " & _
                                "CodigoFuenteDato, LecturaHorometro, Parcial, Funciona, idIncidenciaVolumetrica, " & _
                                "ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica, Observaciones) " & _
                                "VALUES ('" & dt.Rows(i).Item("codigoPVYCR") & "', '" & dt.Rows(i).Item("idElementoMedida") & _
                                "', '" & dt.Rows(i).Item("Fecha_Medida") & "', '" & dt.Rows(i).Item("Cod_Fuente_Dato") & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("HorasIntervalo")) & "', '" & transformarDatos2(dt.Rows(i).Item("Diferencial")) & _
                                "', '" & transformarDatos2(dt.Rows(i).Item("Funciona")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("idIncidenciaVolumetrica")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("ConsumoVolumetricoAdicional")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("ReinicioLecturaVolumetrica")) & "', '" & _
                                transformarDatos2(dt.Rows(i).Item("Observaciones")) & "')"
                    comando.ExecuteNonQuery()
                Next
        End Select

        objOleConnection.Close()

        Dim nombreArchivo As String = fechaActual & "_" & Request.QueryString("codigoPVYCR") & "_" & Session.SessionID & ".xls"
        Dim rutaArchivo As String = Server.MapPath("./ExcelTemp/" & strCarpeta & "/" & fechaActual & "_" & Request.QueryString("codigoPVYCR") & "_" & Session.SessionID & ".xls")
        Response.ClearHeaders()
        Response.ClearContent()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "filename=" & nombreArchivo)
        Response.Flush()
        Response.WriteFile(rutaArchivo)
    End Sub

    Function transformarDatos(ByVal dato As Object, ByVal tipo As String)
        If tipo = "Integer" Then
            Try
                Return CInt(dato)
            Catch ex As Exception
                Return System.DBNull.Value
            End Try
        ElseIf tipo = "Decimal" Then
            Try
                Return Replace(CDec(dato), ",", ".")
            Catch ex As Exception
                Return System.DBNull.Value
            End Try
        ElseIf tipo = "Double" Then
            Try
                Return Replace(CDbl(dato), ",", ".")
            Catch ex As Exception
                Return System.DBNull.Value
            End Try
        ElseIf tipo = "Char" Then
            If utiles.nullABlanco(dato) = "" Then
                Return " "
            Else
                Return dato
            End If
        Else
            Return dato
        End If
    End Function

    Function transformarDatos2(ByVal dato As Object)
        If dato Is System.DBNull.Value Then
            Return " "
        Else
            Return dato
        End If
    End Function

    Function quitarNulos(ByVal texto)
        'Esto es para controlar los nulos
        Dim texto1 As String = Left(texto, InStr(texto, "VALUES"))
        Dim texto2 As String = Right(texto, texto.Length - InStr(texto, "VALUES"))

        Dim vector1() = Split(texto1, ",#;")
        Dim vector2() = Split(texto2, ",#;")

        Dim textoFinal As String = ""


        textoFinal &= vector1(0) & ", "
        For j As Integer = 1 To vector1.Length - 2      'El primer valor no cuenta, pq es obligado, y el último tampoco pq es string y si es nulo la consulta queda '', o sea q no da error
            If Not vector2(j) = " " Then
                textoFinal &= vector1(j) & ", "
            End If
        Next
        textoFinal &= vector1(vector1.Length - 1)

        textoFinal &= vector2(0) & ", "
        For j As Integer = 1 To vector1.Length - 2
            If Not vector2(j) = " " Then
                textoFinal &= vector2(j) & ", "
            End If
        Next
        textoFinal &= vector2(vector1.Length - 1)

        If InStr(textoFinal, "CAUDAL = 34") > 0 Then
            Dim cosarara As String = ""
        End If
        Return textoFinal

    End Function
End Class
