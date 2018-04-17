Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles

Public Class SICA_FuncionesLecturas

    Shared Function SubirArchivo(ByVal archivo As String, ByVal tipoElem As String, ByVal conexion As SqlConnection, ByVal comando As SqlCommand, _
                            ByVal usuario As String, ByVal dt As DataTable) As String

        Dim errorRespuesta As String = ""

        If conexion.State = ConnectionState.Closed Then conexion.Open()
        Dim objDataSet As System.Data.DataSet
        Dim objDataAdapter As System.Data.OleDb.OleDbDataAdapter

        Dim sCs As String = "Provider=Microsoft.Jet.OLEDB.4.0; " & "Data Source=" & archivo & "; Extended Properties='Excel 8.0;IMEX=1'"

        Dim objOleConnection As System.Data.OleDb.OleDbConnection
        objOleConnection = New System.Data.OleDb.OleDbConnection(sCs)
        Dim sSheetName As String = "Hoja1"
        Dim sRange As String = ""


        Dim sSql As String = "select * from " & "[" & sSheetName & "$" & sRange & "]"
        objDataAdapter = New System.Data.OleDb.OleDbDataAdapter(sSql, objOleConnection)
        objDataSet = New System.Data.DataSet

        objDataAdapter.Fill(objDataSet)

        ' ********* Comprobamos que la hoja excel tenga el formato correcto.
        Select Case tipoElem
            Case "Q"
                If objDataSet.Tables("Table").Columns.Count <> 13 Then
                    errorRespuesta = "Error actualizando datos: La hoja excel no tiene el formato correcto"
                Else
                    Dim arrayColExcel As String() = New String(12) {"CodigoPVYCR", "IdElementoMedida", "FechaMedida", _
                    "TipoObtencionCaudal", "CodigoFuenteDato", "Escala", "Calado", "RegimenCurva", "Parada", "Caudal", "Parcial", _
                    "DudaCalidad", "Observaciones"}
                    With objDataSet.Tables("Table")
                        Dim arrayDataSetExcel As String() = New String(12) {.Columns(0).ColumnName, .Columns(1).ColumnName, .Columns(2).ColumnName, _
                        .Columns(3).ColumnName, .Columns(4).ColumnName, .Columns(5).ColumnName, .Columns(6).ColumnName, .Columns(7).ColumnName, _
                        .Columns(8).ColumnName, .Columns(9).ColumnName, .Columns(10).ColumnName, .Columns(11).ColumnName, .Columns(12).ColumnName}
                        For i As Integer = 0 To 12
                            If arrayColExcel(i) <> arrayDataSetExcel(i) Then
                                errorRespuesta = "Error actualizando datos: La hoja excel no tiene el formato correcto"
                                Exit For
                            End If
                        Next
                    End With
                End If
            Case "E"
                If objDataSet.Tables("Table").Columns.Count <> 16 Then
                    errorRespuesta = "Error actualizando datos: La hoja excel no tiene el formato correcto"
                Else
                    Dim arrayColExcel As String() = New String(15) {"CodigoPVYCR", "IdElementoMedida", "FechaMedida", _
                    "CodigoFuenteDato", "LecturaI", "LecturaII", "LecturaIII", "TotalKwh", "Parcial", "TotalKvar", "Funciona", _
                    "Justificacion", "idIncidenciaElectrica", "ConsumoElectricoAdicional", "ReinicioLecturaElectrica", "Observaciones"}
                    With objDataSet.Tables("Table")
                        Dim arrayDataSetExcel As String() = New String(15) {.Columns(0).ColumnName, .Columns(1).ColumnName, .Columns(2).ColumnName, _
                        .Columns(3).ColumnName, .Columns(4).ColumnName, .Columns(5).ColumnName, .Columns(6).ColumnName, .Columns(7).ColumnName, _
                        .Columns(8).ColumnName, .Columns(9).ColumnName, .Columns(10).ColumnName, .Columns(11).ColumnName, .Columns(12).ColumnName, _
                        .Columns(13).ColumnName, .Columns(14).ColumnName, .Columns(15).ColumnName}
                        For i As Integer = 0 To 15
                            If arrayColExcel(i) <> arrayDataSetExcel(i) Then
                                errorRespuesta = "Error actualizando datos: La hoja excel no tiene el formato correcto"
                                Exit For
                            End If
                        Next
                    End With
                End If
            Case "V"
                If objDataSet.Tables("Table").Columns.Count <> 13 Then
                    errorRespuesta = "Error actualizando datos: La hoja excel no tiene el formato correcto"
                Else
                    Dim arrayColExcel As String() = New String(12) {"CodigoPVYCR", "IdElementoMedida", "FechaMedida", _
                    "CodigoFuenteDato", "LecturaContador", "Parcial", "Funciona", "CaudalMedido", "Justificacion", "idIncidenciaVolumetrica", _
                    "ConsumoVolumetricoAdicional", "ReinicioLecturaVolumetrica", "Observaciones"}
                    With objDataSet.Tables("Table")
                        Dim arrayDataSetExcel As String() = New String(12) {.Columns(0).ColumnName, .Columns(1).ColumnName, .Columns(2).ColumnName, _
                        .Columns(3).ColumnName, .Columns(4).ColumnName, .Columns(5).ColumnName, .Columns(6).ColumnName, .Columns(7).ColumnName, _
                        .Columns(8).ColumnName, .Columns(9).ColumnName, .Columns(10).ColumnName, .Columns(11).ColumnName, .Columns(12).ColumnName}
                        For i As Integer = 0 To 12
                            If arrayColExcel(i) <> arrayDataSetExcel(i) Then
                                errorRespuesta = "Error actualizando datos: La hoja excel no tiene el formato correcto"
                                Exit For
                            End If
                        Next
                    End With
                End If
            Case "H"
                If objDataSet.Tables("Table").Columns.Count <> 11 Then
                    errorRespuesta = "Error actualizando datos: La hoja excel no tiene el formato correcto"
                Else
                    Dim arrayColExcel As String() = New String(10) {"CodigoPVYCR", "IdElementoMedida", "FechaMedida", _
                    "CodigoFuenteDato", "LecturaHorometro", "Parcial", "Funciona", "idIncidenciaVolumetrica", _
                    "ConsumoVolumetricoAdicional", "ReinicioLecturaVolumetrica", "Observaciones"}
                    With objDataSet.Tables("Table")
                        Dim arrayDataSetExcel As String() = New String(10) {.Columns(0).ColumnName, .Columns(1).ColumnName, .Columns(2).ColumnName, _
                        .Columns(3).ColumnName, .Columns(4).ColumnName, .Columns(5).ColumnName, .Columns(6).ColumnName, .Columns(7).ColumnName, _
                        .Columns(8).ColumnName, .Columns(9).ColumnName, .Columns(10).ColumnName}
                        For i As Integer = 0 To 10
                            If arrayColExcel(i) <> arrayDataSetExcel(i) Then
                                errorRespuesta = "Error actualizando datos: La hoja excel no tiene el formato correcto"
                                Exit For
                            End If
                        Next
                    End With
                End If
        End Select

        If errorRespuesta <> "" Then
            Return errorRespuesta
            Exit Function
        End If


        ' Aquí comparamos el datatable que teníamos anterior y el nuevo que subimos en excel. 
        ' Crearemos otra tabla diferente con los registros que han sido modificados, con los anteriores y con los modif, y 
        ' luego dependiendo si actualizamos datos o insertamos en el histórico cogeremos unos campos u otros.

        Dim dst As New DataSet

        dst.Tables.Add("TablaModificaciones")
        'Creamos una tabla "TablaModificaciones" con los datos anteriores y los nuevos
        With dst.Tables("TablaModificaciones").Columns
            Select Case tipoElem
                Case "Q"
                    'Datos anteriores
                    .Add(New DataColumn("CodigoPVYCR", Type.GetType("System.String")))
                    .Add(New DataColumn("IdElementoMedida", Type.GetType("System.String")))
                    .Add(New DataColumn("Fecha_Medida", Type.GetType("System.DateTime")))
                    .Add(New DataColumn("TipoObtencionCaudal", Type.GetType("System.String")))
                    .Add(New DataColumn("Cod_Fuente_Dato", Type.GetType("System.String")))
                    .Add(New DataColumn("Escala_M", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("Calado_M", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("RegimenCurva", Type.GetType("System.String")))
                    .Add(New DataColumn("NumeroParada", Type.GetType("System.Int16")))
                    .Add(New DataColumn("Caudal_M3S", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("DUDA_CALIDAD", Type.GetType("System.Int16")))
                    .Add(New DataColumn("Observaciones", Type.GetType("System.String")))

                    'Datos nuevos
                    .Add(New DataColumn("XLS_CodigoPVYCR", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_IdElementoMedida", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_FechaMedida", Type.GetType("System.DateTime")))
                    .Add(New DataColumn("XLS_TipoObtencionCaudal", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_CodigoFuenteDato", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_Escala", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_Calado", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_RegimenCurva", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_Parada", Type.GetType("System.Int16")))
                    .Add(New DataColumn("XLS_Caudal", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_Dudacalidad", Type.GetType("System.Int16")))
                    .Add(New DataColumn("XLS_Observaciones", Type.GetType("System.String")))

                Case "E"
                    'Datos anteriores
                    .Add(New DataColumn("CodigoPVYCR", Type.GetType("System.String")))
                    .Add(New DataColumn("IdElementoMedida", Type.GetType("System.String")))
                    .Add(New DataColumn("Fecha_Medida", Type.GetType("System.DateTime")))
                    .Add(New DataColumn("Cod_Fuente_Dato", Type.GetType("System.String")))
                    .Add(New DataColumn("LecturaI", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("LecturaII", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("LecturaIII", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("total_Kwh", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("total_kvar", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("Funciona", Type.GetType("System.String")))
                    .Add(New DataColumn("Justificacion", Type.GetType("System.String")))
                    .Add(New DataColumn("Observaciones", Type.GetType("System.String")))
                    .Add(New DataColumn("idIncidenciaElectrica", Type.GetType("System.Int16")))
                    .Add(New DataColumn("ConsumoElectricoAdicional", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("ReinicioLecturaElectrica", Type.GetType("System.Decimal")))

                    'Datos nuevos
                    .Add(New DataColumn("XLS_CodigoPVYCR", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_IdElementoMedida", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_FechaMedida", Type.GetType("System.DateTime")))
                    .Add(New DataColumn("XLS_CodigoFuenteDato", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_LecturaI", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_LecturaII", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_LecturaIII", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_TotalKwh", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_TotalKvar", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_Funciona", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_Justificacion", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_idIncidenciaElectrica", Type.GetType("System.Int16")))
                    .Add(New DataColumn("XLS_ConsumoElectricoAdicional", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_ReinicioLecturaElectrica", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_Observaciones", Type.GetType("System.String")))
                Case "V"
                    'Datos anteriores
                    .Add(New DataColumn("CodigoPVYCR", Type.GetType("System.String")))
                    .Add(New DataColumn("IdElementoMedida", Type.GetType("System.String")))
                    .Add(New DataColumn("Fecha_Medida", Type.GetType("System.DateTime")))
                    .Add(New DataColumn("Cod_Fuente_Dato", Type.GetType("System.String")))
                    .Add(New DataColumn("LecturaContador_M3", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("Funciona", Type.GetType("System.String")))
                    .Add(New DataColumn("Justificacion", Type.GetType("System.String")))
                    .Add(New DataColumn("Observaciones", Type.GetType("System.String")))
                    .Add(New DataColumn("idIncidenciaVolumetrica", Type.GetType("System.Int16")))
                    .Add(New DataColumn("ConsumoVolumetricoAdicional", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("ReinicioLecturaVolumetrica", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("CaudalMedido", Type.GetType("System.Int16")))

                    'Datos nuevos
                    .Add(New DataColumn("XLS_CodigoPVYCR", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_IdElementoMedida", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_FechaMedida", Type.GetType("System.DateTime")))
                    .Add(New DataColumn("XLS_CodigoFuenteDato", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_LecturaContador", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_Funciona", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_CaudalMedido", Type.GetType("System.Int16")))
                    .Add(New DataColumn("XLS_Justificacion", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_idIncidenciaVolumetrica", Type.GetType("System.Int16")))
                    .Add(New DataColumn("XLS_ConsumoVolumetricoAdicional", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_ReinicioLecturaVolumetrica", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_Observaciones", Type.GetType("System.String")))
                Case "H"
                    'Datos anteriores
                    .Add(New DataColumn("CodigoPVYCR", Type.GetType("System.String")))
                    .Add(New DataColumn("IdElementoMedida", Type.GetType("System.String")))
                    .Add(New DataColumn("Fecha_Medida", Type.GetType("System.DateTime")))
                    .Add(New DataColumn("Cod_Fuente_Dato", Type.GetType("System.String")))
                    .Add(New DataColumn("HorasIntervalo", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("Funciona", Type.GetType("System.String")))
                    .Add(New DataColumn("Observaciones", Type.GetType("System.String")))
                    .Add(New DataColumn("idIncidenciaVolumetrica", Type.GetType("System.Int16")))
                    .Add(New DataColumn("ConsumoVolumetricoAdicional", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("ReinicioLecturaVolumetrica", Type.GetType("System.Decimal")))

                    'Datos nuevos
                    .Add(New DataColumn("XLS_CodigoPVYCR", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_IdElementoMedida", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_FechaMedida", Type.GetType("System.DateTime")))
                    .Add(New DataColumn("XLS_CodigoFuenteDato", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_LecturaHorometro", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_Funciona", Type.GetType("System.String")))
                    .Add(New DataColumn("XLS_idIncidenciaVolumetrica", Type.GetType("System.Int16")))
                    .Add(New DataColumn("XLS_ConsumoVolumetricoAdicional", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_ReinicioLecturaVolumetrica", Type.GetType("System.Decimal")))
                    .Add(New DataColumn("XLS_Observaciones", Type.GetType("System.String")))
            End Select
        End With

        Dim dr() As System.Data.DataRow
        dr = dt.DefaultView.Table.Select

        Dim dt2 As New DataTable
        dt2 = dt.Copy
        Dim dr2() As System.Data.DataRow
        dr2 = dt2.DefaultView.Table.Select


        Dim j As Integer = 0
        Dim esModificado As Boolean = False

        '********** Creamos la tabla "TablaModificaciones" *****************
        For i As Integer = 1 To objDataSet.Tables("Table").Rows.Count - 1
            With objDataSet.Tables("Table").Rows(i)
                Select Case tipoElem
                    Case "Q"
                        errorRespuesta = comprobarValoresQ(.Item("Escala"), .Item("Calado"), .Item("Caudal"), .Item("Parada"), .Item("DudaCalidad"), _
                                .Item("regimenCurva"), .Item("Observaciones"), comando)

                        If errorRespuesta <> "" Then
                            Return errorRespuesta
                            Exit Function
                        End If

                        'Vemos los registros que están sin ninguna modificación
                        dr = dt.DefaultView.Table.Select("CodigoPVYCR='" & .Item("CodigoPVYCR") & "' AND idElementoMedida='" & .Item("idElementoMedida") & _
                                            "' AND Cod_Fuente_Dato='" & .Item("CodigoFuenteDato") & "' AND Fecha_Medida='" & .Item("FechaMedida") & _
                                            "' AND TipoObtencionCaudal='" & .Item("TipoObtencionCaudal") & "' AND (" & _
                                            verNulos("Escala_M=" & ceroANullTransact(.Item("Escala"), "SELECT")) & ") AND (" & _
                                            verNulos("Calado_M=" & ceroANullTransact(.Item("Calado"), "SELECT")) & ") AND (" & _
                                            verNulos("RegimenCurva='" & blancoANullTransact(.Item("RegimenCurva"), "SELECT") & "'") & ") AND (" & _
                                            verNulos("NumeroParada=" & ceroANullTransact(.Item("Parada"), "SELECT")) & ") AND (" & _
                                            verNulos("Caudal_M3S=" & ceroANullTransact(.Item("Caudal"), "SELECT")) & ") AND (" & _
                                            verNulos("DUDA_CALIDAD=" & ceroANullTransact(.Item("DudaCalidad"), "SELECT")) & ")")

                        If dr.Length = 1 Then
                            esModificado = VerSiEsModificado(dr(0).Item("Observaciones"), .Item("Observaciones"))
                        End If

                        If dr.Length = 0 Or esModificado = True Then       'Si la longitud=0 significa que ese registro ha sido modificado o que no existe
                            'Filtramos por la clave primaria
                            dr2 = dt2.DefaultView.Table.Select("CodigoPVYCR='" & .Item("CodigoPVYCR") & "' AND idElementoMedida='" & .Item("idElementoMedida") & _
                                                "' AND Cod_Fuente_Dato='" & .Item("CodigoFuenteDato") & "' AND Fecha_Medida='" & .Item("FechaMedida") & _
                                                "' AND TipoObtencionCaudal='" & .Item("TipoObtencionCaudal") & "'")
                            'Si la longitud=1 significa que si que existe y que ha sido modificado, por tanto añadimos ese registro a nuestra tabla "TablaModificaciones".
                            If dr2.Length = 1 Then
                                dst.Tables("TablaModificaciones").Rows.Add()
                                With dst.Tables("TablaModificaciones").Rows(j)
                                    .Item("CodigoPVYCR") = dr2(0).Item("CodigoPVYCR")
                                    .Item("IdElementoMedida") = dr2(0).Item("IdElementoMedida")
                                    .Item("Fecha_Medida") = dr2(0).Item("Fecha_Medida")
                                    .Item("TipoObtencionCaudal") = dr2(0).Item("TipoObtencionCaudal")
                                    .Item("Cod_Fuente_Dato") = dr2(0).Item("Cod_Fuente_Dato")
                                    .Item("Escala_M") = dr2(0).Item("Escala_M")
                                    .Item("Calado_M") = dr2(0).Item("Calado_M")
                                    .Item("RegimenCurva") = dr2(0).Item("RegimenCurva")
                                    .Item("NumeroParada") = dr2(0).Item("NumeroParada")
                                    .Item("Caudal_M3S") = dr2(0).Item("Caudal_M3S")
                                    .Item("DUDA_CALIDAD") = dr2(0).Item("DUDA_CALIDAD")
                                    .Item("Observaciones") = dr2(0).Item("Observaciones")

                                    .Item("XLS_CodigoPVYCR") = objDataSet.Tables("Table").Rows(i).Item("CodigoPVYCR")
                                    .Item("XLS_IdElementoMedida") = objDataSet.Tables("Table").Rows(i).Item("idElementoMedida")
                                    .Item("XLS_FechaMedida") = objDataSet.Tables("Table").Rows(i).Item("FechaMedida")
                                    .Item("XLS_TipoObtencionCaudal") = objDataSet.Tables("Table").Rows(i).Item("TipoObtencionCaudal")
                                    .Item("XLS_CodigoFuenteDato") = objDataSet.Tables("Table").Rows(i).Item("CodigoFuenteDato")
                                    If Not objDataSet.Tables("Table").Rows(i).Item("Escala") Is System.DBNull.Value Then
                                        .Item("XLS_Escala") = Replace(objDataSet.Tables("Table").Rows(i).Item("Escala"), ".", ",")
                                    Else
                                        .Item("XLS_Escala") = System.DBNull.Value
                                    End If
                                    If Not objDataSet.Tables("Table").Rows(i).Item("Calado") Is System.DBNull.Value Then
                                        .Item("XLS_Calado") = Replace(objDataSet.Tables("Table").Rows(i).Item("Calado"), ".", ",")
                                    Else
                                        .Item("XLS_Calado") = System.DBNull.Value
                                    End If
                                    .Item("XLS_RegimenCurva") = objDataSet.Tables("Table").Rows(i).Item("RegimenCurva")
                                    .Item("XLS_Parada") = objDataSet.Tables("Table").Rows(i).Item("Parada")
                                    If Not objDataSet.Tables("Table").Rows(i).Item("Caudal") Is System.DBNull.Value Then
                                        .Item("XLS_Caudal") = Replace(objDataSet.Tables("Table").Rows(i).Item("Caudal"), ".", ",")
                                    Else
                                        .Item("XLS_Caudal") = System.DBNull.Value
                                    End If
                                    .Item("XLS_Dudacalidad") = objDataSet.Tables("Table").Rows(i).Item("DudaCalidad")
                                    .Item("XLS_Observaciones") = objDataSet.Tables("Table").Rows(i).Item("Observaciones")
                                End With
                                j = j + 1
                            End If
                        End If
                    Case "E"
                        'Vemos que no se han insertado datos incorrectos en la excel
                        errorRespuesta = comprobarValoresE(.Item("LecturaI"), .Item("LecturaII"), .Item("LecturaIII"), .Item("Totalkwh"), _
                                            .Item("Totalkvar"), .Item("ConsumoElectricoAdicional"), .Item("ReinicioLecturaElectrica"), _
                                            .Item("idIncidenciaElectrica"), .Item("funciona"), .Item("justificacion"), comando)

                        If errorRespuesta <> "" Then
                            Return errorRespuesta
                            Exit Function
                        End If

                        'Vemos los registros que están sin ninguna modificación
                        dr = dt.DefaultView.Table.Select("CodigoPVYCR='" & .Item("CodigoPVYCR") & "' AND idElementoMedida='" & .Item("idElementoMedida") & _
                                            "' AND Cod_Fuente_Dato='" & .Item("CodigoFuenteDato") & "' AND Fecha_Medida='" & .Item("FechaMedida") & "' AND (" & _
                                            verNulos("LecturaI=" & ceroANullTransact(.Item("LecturaI"), "SELECT")) & ") AND (" & _
                                            verNulos("LecturaII=" & ceroANullTransact(.Item("LecturaII"), "SELECT")) & ") AND (" & _
                                            verNulos("LecturaIII=" & ceroANullTransact(.Item("LecturaIII"), "SELECT")) & ") AND (" & _
                                            verNulos("Total_kwh=" & ceroANullTransact(.Item("Totalkwh"), "SELECT")) & ") AND (" & _
                                            verNulos("Total_Kvar=" & ceroANullTransact(.Item("TotalKvar"), "SELECT")) & ") AND (" & _
                                            verNulos("Funciona='" & blancoANullTransact(.Item("Funciona"), "SELECT") & "'") & ") AND (" & _
                                            verNulos("Justificacion='" & blancoANullTransact(.Item("Justificacion"), "SELECT") & "'") & ") AND (" & _
                                            verNulos("idIncidenciaElectrica=" & ceroANullTransact(.Item("idIncidenciaElectrica"), "SELECT")) & ") AND (" & _
                                            verNulos("ConsumoElectricoAdicional=" & ceroANullTransact(.Item("ConsumoElectricoAdicional"), "SELECT")) & ") AND (" & _
                                            verNulos("ReinicioLecturaElectrica=" & ceroANullTransact(.Item("ReinicioLecturaElectrica"), "SELECT")) & ")")

                        If dr.Length = 1 Then
                            esModificado = VerSiEsModificado(dr(0).Item("Observaciones"), .Item("Observaciones"))
                        End If

                        If dr.Length = 0 Or esModificado = True Then       'Si la longitud=0 significa que ese campo ha sido modificado o que no existe
                            'Filtramos por la clave primaria
                            dr2 = dt2.DefaultView.Table.Select("CodigoPVYCR='" & .Item("CodigoPVYCR") & "' AND idElementoMedida='" & .Item("idElementoMedida") & _
                                                "' AND Cod_Fuente_Dato='" & .Item("CodigoFuenteDato") & "' AND Fecha_Medida='" & .Item("FechaMedida") & "'")
                            'Si la longitud=1 significa que si que existe y que ha sido modificado, por tanto añadimos ese registro a nuestra tabla "TablaModificaciones".
                            If dr2.Length = 1 Then
                                dst.Tables("TablaModificaciones").Rows.Add()
                                With dst.Tables("TablaModificaciones").Rows(j)
                                    .Item("CodigoPVYCR") = dr2(0).Item("CodigoPVYCR")
                                    .Item("IdElementoMedida") = dr2(0).Item("IdElementoMedida")
                                    .Item("Fecha_Medida") = dr2(0).Item("Fecha_Medida")
                                    .Item("Cod_Fuente_Dato") = dr2(0).Item("Cod_Fuente_Dato")
                                    .Item("LecturaI") = dr2(0).Item("LecturaI")
                                    .Item("LecturaII") = dr2(0).Item("LecturaII")
                                    .Item("LecturaIII") = dr2(0).Item("LecturaIII")
                                    .Item("total_Kwh") = dr2(0).Item("total_Kwh")
                                    .Item("total_Kvar") = dr2(0).Item("total_Kvar")
                                    .Item("Funciona") = dr2(0).Item("Funciona")
                                    .Item("Justificacion") = dr2(0).Item("Justificacion")
                                    .Item("Observaciones") = dr2(0).Item("Observaciones")
                                    .Item("idIncidenciaElectrica") = dr2(0).Item("idIncidenciaElectrica")
                                    .Item("ConsumoElectricoAdicional") = dr2(0).Item("ConsumoElectricoAdicional")
                                    .Item("ReinicioLecturaElectrica") = dr2(0).Item("ReinicioLecturaElectrica")

                                    .Item("XLS_CodigoPVYCR") = objDataSet.Tables("Table").Rows(i).Item("CodigoPVYCR")
                                    .Item("XLS_IdElementoMedida") = objDataSet.Tables("Table").Rows(i).Item("idElementoMedida")
                                    .Item("XLS_FechaMedida") = objDataSet.Tables("Table").Rows(i).Item("FechaMedida")
                                    .Item("XLS_CodigoFuenteDato") = objDataSet.Tables("Table").Rows(i).Item("CodigoFuenteDato")
                                    If Not objDataSet.Tables("Table").Rows(i).Item("LecturaI") Is System.DBNull.Value Then
                                        .Item("XLS_LecturaI") = Replace(objDataSet.Tables("Table").Rows(i).Item("LecturaI"), ".", ",")
                                    Else
                                        .Item("XLS_LecturaI") = System.DBNull.Value
                                    End If
                                    If Not objDataSet.Tables("Table").Rows(i).Item("LecturaII") Is System.DBNull.Value Then
                                        .Item("XLS_LecturaII") = Replace(objDataSet.Tables("Table").Rows(i).Item("LecturaII"), ".", ",")
                                    Else
                                        .Item("XLS_LecturaII") = System.DBNull.Value
                                    End If
                                    If Not objDataSet.Tables("Table").Rows(i).Item("LecturaIII") Is System.DBNull.Value Then
                                        .Item("XLS_LecturaIII") = Replace(objDataSet.Tables("Table").Rows(i).Item("LecturaIII"), ".", ",")
                                    Else
                                        .Item("XLS_LecturaIII") = System.DBNull.Value
                                    End If
                                    If Not objDataSet.Tables("Table").Rows(i).Item("TotalKwh") Is System.DBNull.Value Then
                                        .Item("XLS_TotalKwh") = Replace(objDataSet.Tables("Table").Rows(i).Item("TotalKwh"), ".", ",")
                                    Else
                                        .Item("XLS_TotalKwh") = System.DBNull.Value
                                    End If
                                    If Not objDataSet.Tables("Table").Rows(i).Item("TotalKvar") Is System.DBNull.Value Then
                                        .Item("XLS_TotalKvar") = Replace(objDataSet.Tables("Table").Rows(i).Item("TotalKvar"), ".", ",")
                                    Else
                                        .Item("XLS_TotalKvar") = System.DBNull.Value
                                    End If
                                    .Item("XLS_Funciona") = objDataSet.Tables("Table").Rows(i).Item("Funciona")
                                    .Item("XLS_Justificacion") = objDataSet.Tables("Table").Rows(i).Item("Justificacion")
                                    .Item("XLS_idIncidenciaElectrica") = objDataSet.Tables("Table").Rows(i).Item("idIncidenciaElectrica")
                                    If Not objDataSet.Tables("Table").Rows(i).Item("ConsumoElectricoAdicional") Is System.DBNull.Value Then
                                        .Item("XLS_ConsumoElectricoAdicional") = Replace(objDataSet.Tables("Table").Rows(i).Item("ConsumoElectricoAdicional"), ".", ",")
                                    Else
                                        .Item("XLS_ConsumoElectricoAdicional") = System.DBNull.Value
                                    End If
                                    If Not objDataSet.Tables("Table").Rows(i).Item("ReinicioLecturaElectrica") Is System.DBNull.Value Then
                                        .Item("XLS_ReinicioLecturaElectrica") = Replace(objDataSet.Tables("Table").Rows(i).Item("ReinicioLecturaElectrica"), ".", ",")
                                    Else
                                        .Item("XLS_ReinicioLecturaElectrica") = System.DBNull.Value
                                    End If
                                    .Item("XLS_Observaciones") = objDataSet.Tables("Table").Rows(i).Item("Observaciones")
                                End With
                                j = j + 1
                            End If
                        End If
                    Case "V"
                        'Vemos que no se han insertado datos incorrectos en la excel
                        errorRespuesta = comprobarValoresV(.Item("lecturaContador"), .Item("consumoVolumetricoAdicional"), _
                                            .Item("reinicioLecturaVolumetrica"), .Item("idIncidenciaVolumetrica"), .Item("CaudalMedido"), .Item("funciona"), .Item("justificacion"), comando)

                        If errorRespuesta <> "" Then
                            Return errorRespuesta
                            Exit Function
                        End If

                        'Vemos los registros que están sin ninguna modificación
                        dr = dt.DefaultView.Table.Select("CodigoPVYCR='" & .Item("CodigoPVYCR") & "' AND idElementoMedida='" & .Item("idElementoMedida") & _
                                            "' AND Cod_Fuente_Dato='" & .Item("CodigoFuenteDato") & "' AND Fecha_Medida='" & .Item("FechaMedida") & "' AND (" & _
                                            verNulos("LecturaContador_M3=" & ceroANullTransact(.Item("LecturaContador"), "SELECT")) & ") AND (" & _
                                            verNulos("Funciona='" & blancoANullTransact(.Item("Funciona"), "SELECT") & "'") & ") AND (" & _
                                            verNulos("Justificacion='" & blancoANullTransact(.Item("Justificacion"), "SELECT") & "'") & ") AND (" & _
                                            verNulos("idIncidenciaVolumetrica=" & ceroANullTransact(.Item("idIncidenciaVolumetrica"), "SELECT")) & ") AND (" & _
                                            verNulos("ConsumoVolumetricoAdicional=" & ceroANullTransact(.Item("ConsumoVolumetricoAdicional"), "SELECT")) & ") AND (" & _
                                            verNulos("ReinicioLecturaVolumetrica=" & ceroANullTransact(.Item("ReinicioLecturaVolumetrica"), "SELECT")) & ") AND (" & _
                                            verNulos("Caudalmedido=" & ceroANullTransact(.Item("CaudalMedido"), "SELECT")) & ")")

                        If dr.Length = 1 Then
                            esModificado = VerSiEsModificado(dr(0).Item("Observaciones"), .Item("Observaciones"))
                        End If

                        If dr.Length = 0 Or esModificado = True Then       'Si la longitud=0 significa que ese campo ha sido modificado o que no existe
                            'Filtramos por la clave primaria
                            dr2 = dt2.DefaultView.Table.Select("CodigoPVYCR='" & .Item("CodigoPVYCR") & "' AND idElementoMedida='" & .Item("idElementoMedida") & _
                                                "' AND Cod_Fuente_Dato='" & .Item("CodigoFuenteDato") & "' AND Fecha_Medida='" & .Item("FechaMedida") & "'")
                            'Si la longitud=1 significa que si que existe y que ha sido modificado, por tanto añadimos ese registro a nuestra tabla "TablaModificaciones".
                            If dr2.Length = 1 Then
                                dst.Tables("TablaModificaciones").Rows.Add()
                                With dst.Tables("TablaModificaciones").Rows(j)
                                    .Item("CodigoPVYCR") = dr2(0).Item("CodigoPVYCR")
                                    .Item("IdElementoMedida") = dr2(0).Item("IdElementoMedida")
                                    .Item("Fecha_Medida") = dr2(0).Item("Fecha_Medida")
                                    .Item("Cod_Fuente_Dato") = dr2(0).Item("Cod_Fuente_Dato")
                                    .Item("LecturaContador_M3") = dr2(0).Item("LecturaContador_M3")
                                    .Item("Funciona") = dr2(0).Item("Funciona")
                                    .Item("Justificacion") = dr2(0).Item("Justificacion")
                                    .Item("Observaciones") = dr2(0).Item("Observaciones")
                                    .Item("idIncidenciaVolumetrica") = dr2(0).Item("idIncidenciaVolumetrica")
                                    .Item("ConsumoVolumetricoAdicional") = dr2(0).Item("ConsumoVolumetricoAdicional")
                                    .Item("ReinicioLecturaVolumetrica") = dr2(0).Item("ReinicioLecturaVolumetrica")
                                    .Item("CaudalMedido") = dr2(0).Item("CaudalMedido")

                                    .Item("XLS_CodigoPVYCR") = objDataSet.Tables("Table").Rows(i).Item("CodigoPVYCR")
                                    .Item("XLS_IdElementoMedida") = objDataSet.Tables("Table").Rows(i).Item("idElementoMedida")
                                    .Item("XLS_FechaMedida") = objDataSet.Tables("Table").Rows(i).Item("FechaMedida")
                                    .Item("XLS_CodigoFuenteDato") = objDataSet.Tables("Table").Rows(i).Item("CodigoFuenteDato")
                                    If Not objDataSet.Tables("Table").Rows(i).Item("LecturaContador") Is System.DBNull.Value Then
                                        .Item("XLS_LecturaContador") = Replace(objDataSet.Tables("Table").Rows(i).Item("LecturaContador"), ".", ",")
                                    Else
                                        .Item("XLS_LecturaContador") = System.DBNull.Value
                                    End If
                                    .Item("XLS_Funciona") = objDataSet.Tables("Table").Rows(i).Item("Funciona")
                                    .Item("XLS_CaudalMedido") = objDataSet.Tables("Table").Rows(i).Item("CaudalMedido")
                                    .Item("XLS_Justificacion") = objDataSet.Tables("Table").Rows(i).Item("Justificacion")
                                    .Item("XLS_idIncidenciaVolumetrica") = objDataSet.Tables("Table").Rows(i).Item("idIncidenciaVolumetrica")
                                    If Not objDataSet.Tables("Table").Rows(i).Item("ConsumoVolumetricoAdicional") Is System.DBNull.Value Then
                                        .Item("XLS_ConsumoVolumetricoAdicional") = Replace(objDataSet.Tables("Table").Rows(i).Item("ConsumoVolumetricoAdicional"), ".", ",")
                                    Else
                                        .Item("XLS_ConsumoVolumetricoAdicional") = System.DBNull.Value
                                    End If
                                    If Not objDataSet.Tables("Table").Rows(i).Item("ReinicioLecturaVolumetrica") Is System.DBNull.Value Then
                                        .Item("XLS_ReinicioLecturaVolumetrica") = Replace(objDataSet.Tables("Table").Rows(i).Item("ReinicioLecturaVolumetrica"), ".", ",")
                                    Else
                                        .Item("XLS_ReinicioLecturaVolumetrica") = System.DBNull.Value
                                    End If
                                    .Item("XLS_Observaciones") = objDataSet.Tables("Table").Rows(i).Item("Observaciones")
                                End With
                                j = j + 1
                            End If
                        End If
                    Case "H"
                        'Vemos que no se han insertado datos incorrectos en la excel
                        errorRespuesta = comprobarValoresH(.Item("lecturaHorometro"), .Item("consumoVolumetricoAdicional"), .Item("reinicioLecturaVolumetrica"), _
                                            .Item("idIncidenciaVolumetrica"), .Item("funciona"), comando)

                        If errorRespuesta <> "" Then
                            Return errorRespuesta
                            Exit Function
                        End If

                        dr = dt.DefaultView.Table.Select("CodigoPVYCR='" & .Item("CodigoPVYCR") & "' AND idElementoMedida='" & .Item("idElementoMedida") & _
                                            "' AND Cod_Fuente_Dato='" & .Item("CodigoFuenteDato") & "' AND Fecha_Medida='" & .Item("FechaMedida") & "' AND (" & _
                                            verNulos("HorasIntervalo=" & ceroANullTransact(.Item("LecturaHorometro"), "SELECT")) & ") AND (" & _
                                            verNulos("Funciona='" & blancoANullTransact(.Item("Funciona"), "SELECT") & "'") & ") AND (" & _
                                            verNulos("idIncidenciaVolumetrica=" & ceroANullTransact(.Item("idIncidenciaVolumetrica"), "SELECT")) & ") AND (" & _
                                            verNulos("ConsumoVolumetricoAdicional=" & ceroANullTransact(.Item("ConsumoVolumetricoAdicional"), "SELECT")) & ") AND (" & _
                                            verNulos("ReinicioLecturaVolumetrica=" & ceroANullTransact(.Item("ReinicioLecturaVolumetrica"), "SELECT")) & ")")

                        If dr.Length = 1 Then
                            esModificado = VerSiEsModificado(dr(0).Item("Observaciones"), .Item("Observaciones"))
                        End If

                        If dr.Length = 0 Or esModificado = True Then      'Si la longitud=0 significa que ese campo ha sido modificado o que no existe
                            'Filtramos por la clave primaria
                            dr2 = dt2.DefaultView.Table.Select("CodigoPVYCR='" & .Item("CodigoPVYCR") & "' AND idElementoMedida='" & .Item("idElementoMedida") & _
                                                "' AND Cod_Fuente_Dato='" & .Item("CodigoFuenteDato") & "' AND Fecha_Medida='" & .Item("FechaMedida") & "'")
                            'Si la longitud=1 significa que si que existe y que ha sido modificado, por tanto añadimos ese registro a nuestra tabla "TablaModificaciones".
                            If dr2.Length = 1 Then
                                dst.Tables("TablaModificaciones").Rows.Add()
                                With dst.Tables("TablaModificaciones").Rows(j)
                                    .Item("CodigoPVYCR") = dr2(0).Item("CodigoPVYCR")
                                    .Item("IdElementoMedida") = dr2(0).Item("IdElementoMedida")
                                    .Item("Fecha_Medida") = dr2(0).Item("Fecha_Medida")
                                    .Item("Cod_Fuente_Dato") = dr2(0).Item("Cod_Fuente_Dato")
                                    .Item("HorasIntervalo") = dr2(0).Item("HorasIntervalo")
                                    .Item("Funciona") = dr2(0).Item("Funciona")
                                    .Item("Observaciones") = dr2(0).Item("Observaciones")
                                    .Item("idIncidenciaVolumetrica") = dr2(0).Item("idIncidenciaVolumetrica")
                                    .Item("ConsumoVolumetricoAdicional") = dr2(0).Item("ConsumoVolumetricoAdicional")
                                    .Item("ReinicioLecturaVolumetrica") = dr2(0).Item("ReinicioLecturaVolumetrica")

                                    .Item("XLS_CodigoPVYCR") = objDataSet.Tables("Table").Rows(i).Item("CodigoPVYCR")
                                    .Item("XLS_IdElementoMedida") = objDataSet.Tables("Table").Rows(i).Item("idElementoMedida")
                                    .Item("XLS_FechaMedida") = objDataSet.Tables("Table").Rows(i).Item("FechaMedida")
                                    .Item("XLS_CodigoFuenteDato") = objDataSet.Tables("Table").Rows(i).Item("CodigoFuenteDato")
                                    If Not objDataSet.Tables("Table").Rows(i).Item("LecturaHorometro") Is System.DBNull.Value Then
                                        .Item("XLS_LecturaHorometro") = Replace(objDataSet.Tables("Table").Rows(i).Item("LecturaHorometro"), ".", ",")
                                    Else
                                        .Item("XLS_LecturaHorometro") = System.DBNull.Value
                                    End If
                                    .Item("XLS_Funciona") = objDataSet.Tables("Table").Rows(i).Item("Funciona")
                                    .Item("XLS_idIncidenciaVolumetrica") = objDataSet.Tables("Table").Rows(i).Item("idIncidenciaVolumetrica")
                                    If Not objDataSet.Tables("Table").Rows(i).Item("ConsumoVolumetricoAdicional") Is System.DBNull.Value Then
                                        .Item("XLS_ConsumoVolumetricoAdicional") = Replace(objDataSet.Tables("Table").Rows(i).Item("ConsumoVolumetricoAdicional"), ".", ",")
                                    Else
                                        .Item("XLS_ConsumoVolumetricoAdicional") = System.DBNull.Value
                                    End If
                                    If Not objDataSet.Tables("Table").Rows(i).Item("ReinicioLecturaVolumetrica") Is System.DBNull.Value Then
                                        .Item("XLS_ReinicioLecturaVolumetrica") = Replace(objDataSet.Tables("Table").Rows(i).Item("ReinicioLecturaVolumetrica"), ".", ",")
                                    Else
                                        .Item("XLS_ReinicioLecturaVolumetrica") = System.DBNull.Value
                                    End If
                                    .Item("XLS_Observaciones") = objDataSet.Tables("Table").Rows(i).Item("Observaciones")
                                End With
                                j = j + 1
                            End If
                        End If
                End Select
            End With
            esModificado = False
        Next



        '****** Aquí empezamos la inserción de los datos *****************
        comando.Transaction = conexion.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted)

        Try
            With dst.Tables("TablaModificaciones")
                For i As Integer = 0 To .Rows.Count - 1
                    Select Case tipoElem
                        Case "Q"
                            'Actualizamos la tabla
                            comando.CommandText = "update PVYCR_DatosAcequias set " & _
                                        "Escala_M=" & ceroANullTransact(.Rows(i).Item("XLS_Escala"), "UPDATE") & ", Calado_M=" & ceroANullTransact(.Rows(i).Item("XLS_Calado"), "UPDATE") & _
                                        ", RegimenCurva=" & blancoANullTransact(.Rows(i).Item("XLS_RegimenCurva"), "UPDATE") & ", NumeroParada=" & ceroANullTransact(.Rows(i).Item("XLS_Parada"), "UPDATE") & _
                                        ", Caudal_M3S=" & ceroANullTransact(.Rows(i).Item("XLS_Caudal"), "UPDATE") & ", Duda_Calidad=" & ceroANullTransact(.Rows(i).Item("XLS_Dudacalidad"), "UPDATE") & ", " & _
                                        "Observaciones=" & blancoANullTransact(.Rows(i).Item("XLS_Observaciones"), "UPDATE") & ", modificado=1 " & _
                                        "where codigoPVYCR='" & .Rows(i).Item("XLS_CodigoPVYCR") & "' and idElementoMedida='" & _
                                        .Rows(i).Item("XLS_IdElementoMedida") & "' and Fecha_Medida='" & .Rows(i).Item("XLS_FechaMedida") & "' and " & _
                                        "Cod_Fuente_Dato='" & .Rows(i).Item("XLS_CodigoFuenteDato") & "' and TipoObtencionCaudal='" & _
                                        .Rows(i).Item("XLS_TipoObtencionCaudal") & "'"
                            comando.ExecuteNonQuery()

                            'Insertamos el registro en la tabla Histo
                            comando.CommandText = "INSERT INTO PVYCR_DatosAcequias_Histo (CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, " & _
                                    "Escala_M, Calado_M, RegimenCurva, NumeroParada, Caudal_M3S, TipoObtencionCaudal, Duda_Calidad, Observaciones, " & _
                                    "FechaModif, Usuario)  VALUES ('" & .Rows(i).Item("CodigoPVYCR") & "','" & .Rows(i).Item("idElementoMedida") & "','" & _
                                    .Rows(i).Item("Cod_Fuente_Dato") & "','" & .Rows(i).Item("Fecha_Medida") & "'," & ceroANullTransact(.Rows(i).Item("Escala_M"), "UPDATE") & _
                                    "," & ceroANullTransact(.Rows(i).Item("Calado_M"), "UPDATE") & "," & blancoANullTransact(.Rows(i).Item("RegimenCurva"), "UPDATE") & "," & ceroANullTransact(.Rows(i).Item("NumeroParada"), "UPDATE") & _
                                    "," & ceroANullTransact(.Rows(i).Item("Caudal_M3S"), "UPDATE") & "," & blancoANullTransact(.Rows(i).Item("TipoObtencionCaudal"), "UPDATE") & _
                                    "," & ceroANullTransact(.Rows(i).Item("Duda_Calidad"), "UPDATE") & "," & blancoANullTransact(.Rows(i).Item("Observaciones"), "UPDATE") & ",'" & Date.Now & "','" & usuario & "')"
                            comando.ExecuteNonQuery()

                        Case "E"
                            'Actualizamos la tabla
                            comando.CommandText = "update PVYCR_DatosAlimentacion set LecturaI=" & Replace(nullACero(.Rows(i).Item("XLS_LecturaI")), ",", ".") & ", " & _
                                        "LecturaII=" & ceroANullTransact(.Rows(i).Item("XLS_LecturaII"), "UPDATE") & ", " & _
                                        "LecturaIII=" & ceroANullTransact(.Rows(i).Item("XLS_LecturaIII"), "UPDATE") & ", " & _
                                        "Total_Kwh=" & ceroANullTransact(.Rows(i).Item("XLS_TotalKwh"), "UPDATE") & ", " & _
                                        "Total_Kvar=" & ceroANullTransact(.Rows(i).Item("XLS_TotalKvar"), "UPDATE") & ", " & _
                                        "Funciona=" & blancoANullTransact(.Rows(i).Item("XLS_Funciona"), "UPDATE") & ", " & _
                                        "Justificacion=" & blancoANullTransact(.Rows(i).Item("XLS_Justificacion"), "UPDATE") & ", " & _
                                        "idIncidenciaElectrica=" & ceroANullTransact(.Rows(i).Item("XLS_idIncidenciaElectrica"), "UPDATE") & ", " & _
                                        "ConsumoElectricoAdicional=" & ceroANullTransact(.Rows(i).Item("XLS_ConsumoElectricoAdicional"), "UPDATE") & ", " & _
                                        "ReinicioLecturaElectrica=" & ceroANullTransact(.Rows(i).Item("XLS_ReinicioLecturaElectrica"), "UPDATE") & ", " & _
                                        "Observaciones=" & blancoANullTransact(.Rows(i).Item("XLS_Observaciones"), "UPDATE") & ", modificado=1 " & _
                                        "where codigoPVYCR='" & .Rows(i).Item("XLS_CodigoPVYCR") & "' and idElementoMedida='" & _
                                        .Rows(i).Item("XLS_idElementoMedida") & "' and Fecha_Medida='" & .Rows(i).Item("XLS_FechaMedida") & "' and " & _
                                        "Cod_Fuente_Dato='" & .Rows(i).Item("XLS_CodigoFuenteDato") & "'"
                            comando.ExecuteNonQuery()

                            'Insertamos el registro en la tabla Histo
                            comando.CommandText = "INSERT INTO PVYCR_DatosAlimentacion_Histo (CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, " & _
                                    "Fecha_Medida, LecturaI, LecturaII, LecturaIII, Total_KWH, Total_Kvar, Funciona, Justificacion, idIncidenciaElectrica, ConsumoElectricoAdicional, " & _
                                    "ReinicioLecturaElectrica, Observaciones, FechaModif, Usuario) VALUES ('" & _
                                    .Rows(i).Item("CodigoPVYCR") & "','" & .Rows(i).Item("idElementoMedida") & "','" & .Rows(i).Item("Cod_Fuente_Dato") & "','" & _
                                    .Rows(i).Item("Fecha_Medida") & "'," & ceroANullTransact(.Rows(i).Item("LecturaI"), "UPDATE") & "," & ceroANullTransact(.Rows(i).Item("LecturaII"), "UPDATE") & _
                                    "," & ceroANullTransact(.Rows(i).Item("LecturaIII"), "UPDATE") & "," & ceroANullTransact(.Rows(i).Item("total_kwh"), "UPDATE") & _
                                    "," & ceroANullTransact(.Rows(i).Item("total_kvar"), "UPDATE") & "," & blancoANullTransact(.Rows(i).Item("Funciona"), "UPDATE") & _
                                    "," & blancoANullTransact(.Rows(i).Item("Justificacion"), "UPDATE") & "," & ceroANullTransact(.Rows(i).Item("idIncidenciaElectrica"), "UPDATE") & _
                                    "," & ceroANullTransact(.Rows(i).Item("ConsumoElectricoAdicional"), "UPDATE") & "," & ceroANullTransact(.Rows(i).Item("ReinicioLecturaElectrica"), "UPDATE") & "," & blancoANullTransact(.Rows(i).Item("Observaciones"), "UPDATE") & _
                                    ",'" & Date.Now & "','" & usuario & "')"
                            comando.ExecuteNonQuery()

                        Case "V"
                            'Actualizamos la tabla
                            comando.CommandText = "update PVYCR_DatosMotores set LecturaContador_M3=" & ceroANullTransact(.Rows(i).Item("XLS_LecturaContador"), "UPDATE") & ", " & _
                                        "Funciona=" & blancoANullTransact(.Rows(i).Item("XLS_Funciona"), "UPDATE") & ", " & _
                                        "Observaciones=" & blancoANullTransact(.Rows(i).Item("XLS_Observaciones"), "UPDATE") & ", " & _
                                        "Justificacion=" & blancoANullTransact(.Rows(i).Item("XLS_Justificacion"), "UPDATE") & ", " & _
                                        "idIncidenciaVolumetrica=" & ceroANullTransact(.Rows(i).Item("XLS_idIncidenciaVolumetrica"), "UPDATE") & ", " & _
                                        "ConsumoVolumetricoAdicional=" & ceroANullTransact(.Rows(i).Item("XLS_ConsumoVolumetricoAdicional"), "UPDATE") & ", " & _
                                        "ReinicioLecturaVolumetrica=" & ceroANullTransact(.Rows(i).Item("XLS_ReinicioLecturaVolumetrica"), "UPDATE") & ", " & _
                                        "CaudalMedido=" & ceroANullTransact(.Rows(i).Item("XLS_CaudalMedido"), "UPDATE") & ", modificado=1 " & _
                                        "where codigoPVYCR='" & .Rows(i).Item("XLS_CodigoPVYCR") & "' and idElementoMedida='" & _
                                        .Rows(i).Item("XLS_idElementoMedida") & "' and Fecha_Medida='" & .Rows(i).Item("XLS_FechaMedida") & "' and " & _
                                        "Cod_Fuente_Dato='" & .Rows(i).Item("XLS_CodigoFuenteDato") & "'"
                            comando.ExecuteNonQuery()

                            'Insertamos el registro en la tabla Histo
                            comando.CommandText = "INSERT INTO PVYCR_DatosMotores_Histo (CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, " & _
                                    "LecturaContador_M3,Funciona,Observaciones,Justificacion,idIncidenciaVolumetrica,ConsumoVolumetricoAdicional," & _
                                    "ReinicioLecturaVolumetrica,Caudalmedido,FechaModif,Usuario) VALUES ('" & .Rows(i).Item("CodigoPVYCR") & "','" & _
                                    .Rows(i).Item("idElementoMedida") & "','" & .Rows(i).Item("Cod_Fuente_Dato") & "','" & .Rows(i).Item("Fecha_Medida") & "'," & _
                                    ceroANullTransact(.Rows(i).Item("lecturaContador_M3"), "UPDATE") & ", " & blancoANullTransact(.Rows(i).Item("Funciona"), "UPDATE") & "," & blancoANullTransact(.Rows(i).Item("Observaciones"), "UPDATE") & _
                                    "," & blancoANullTransact(.Rows(i).Item("Justificacion"), "UPDATE") & "," & ceroANullTransact(.Rows(i).Item("idIncidenciaVolumetrica"), "UPDATE") & "," & ceroANullTransact(.Rows(i).Item("ConsumoVolumetricoAdicional"), "UPDATE") & _
                                    "," & ceroANullTransact(.Rows(i).Item("ReinicioLecturaVolumetrica"), "UPDATE") & "," & ceroANullTransact(.Rows(i).Item("CaudalMedido"), "UPDATE") & ",'" & _
                                    Date.Now & "','" & usuario & "')"
                            comando.ExecuteNonQuery()

                        Case "H"
                            'Actualizamos la tabla
                            comando.CommandText = "update PVYCR_DatosHorometros set HorasIntervalo=" & ceroANullTransact(.Rows(i).Item("XLS_LecturaHorometro"), "UPDATE") & ", " & _
                                        "Funciona=" & blancoANullTransact(.Rows(i).Item("XLS_Funciona"), "UPDATE") & ", " & _
                                        "idIncidenciaVolumetrica=" & ceroANullTransact(.Rows(i).Item("XLS_idIncidenciaVolumetrica"), "UPDATE") & ", " & _
                                        "ConsumoVolumetricoAdicional=" & ceroANullTransact(.Rows(i).Item("XLS_ConsumoVolumetricoAdicional"), "UPDATE") & ", " & _
                                        "ReinicioLecturaVolumetrica=" & ceroANullTransact(.Rows(i).Item("XLS_ReinicioLecturaVolumetrica"), "UPDATE") & ", " & _
                                        "Observaciones=" & blancoANullTransact(.Rows(i).Item("XLS_Observaciones"), "UPDATE") & ", modificado=1 " & _
                                        "where codigoPVYCR='" & .Rows(i).Item("XLS_CodigoPVYCR") & "' and idElementoMedida='" & _
                                        .Rows(i).Item("XLS_idElementoMedida") & "' and Fecha_Medida='" & .Rows(i).Item("XLS_FechaMedida") & "' and " & _
                                        "Cod_Fuente_Dato='" & .Rows(i).Item("XLS_CodigoFuenteDato") & "'"
                            comando.ExecuteNonQuery()

                            'Insertamos el registro en la tabla Histo
                            comando.CommandText = "INSERT INTO PVYCR_DatosHorometros_Histo (CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, " & _
                                    "Fecha_Medida, HorasIntervalo, Funciona, Observaciones, idIncidenciaVolumetrica, ConsumoVolumetricoAdicional, " & _
                                    "ReinicioLecturaVolumetrica, FechaModif, Usuario) VALUES ('" & .Rows(i).Item("CodigoPVYCR") & "', '" & .Rows(i).Item("idElementoMedida") & _
                                    "', '" & .Rows(i).Item("Cod_Fuente_Dato") & "', '" & .Rows(i).Item("Fecha_Medida") & "', " & ceroANullTransact(.Rows(i).Item("HorasIntervalo"), "UPDATE") & _
                                    ", " & blancoANullTransact(.Rows(i).Item("Funciona"), "UPDATE") & ", " & blancoANullTransact(.Rows(i).Item("Observaciones"), "UPDATE") & "," & ceroANullTransact(.Rows(i).Item("idIncidenciaVolumetrica"), "UPDATE") & "," & _
                                    ceroANullTransact(.Rows(i).Item("ConsumoVolumetricoAdicional"), "UPDATE") & ", " & ceroANullTransact(.Rows(i).Item("ReinicioLecturaVolumetrica"), "UPDATE") & _
                                    ",'" & Date.Now & "','" & usuario & "')"
                            comando.ExecuteNonQuery()
                    End Select
                Next
            End With

            comando.Transaction.Commit()
        Catch ex As Exception
            comando.Transaction.Rollback()
            errorRespuesta = "Error actualizando datos: " & ex.Message
        Finally
            objOleConnection.Close()
        End Try

        Return errorRespuesta
    End Function

    Shared Function verNulos(ByVal valor As String) As String
        Dim campo As String

        If InStr(valor, "=is null") > 0 Then
            campo = Left(valor, InStr(valor, "=is null") - 1)
            valor = Replace(valor, "=is null", " is null or " & campo & "=0")
        Else
            If InStr(valor, "='is null'") > 0 Then
                campo = Left(valor, InStr(valor, "='is null'") - 1)
                valor = Replace(valor, "='is null'", " is null or " & campo & "=''")
            End If
        End If

        If InStr(valor, "Observaciones") > 0 Then
            Dim primeraComilla = InStr(valor, "'")
            Dim segundaComilla = InStrRev(valor, "'")
            Dim valorObservaciones = Mid(valor, primeraComilla + 1, segundaComilla - primeraComilla - 1)

            valor = Replace(valor, "Observaciones='" & valorObservaciones & "'", "cast(Observaciones as varchar(8000))='" & valorObservaciones & "'")
        End If

        Return valor
    End Function

    Shared Function ceroANullTransact(ByVal valor As Object, ByVal sentencia As String)
        Select Case sentencia
            Case "SELECT"
                If valor Is System.DBNull.Value Then
                    Return "is null"
                Else
                    Return Replace(valor, ",", ".")
                End If
            Case "UPDATE"
                If valor Is System.DBNull.Value Then
                    Return "NULL"
                Else
                    Return Replace(valor, ",", ".")
                End If
        End Select
    End Function

    Shared Function blancoANullTransact(ByVal valor As Object, ByVal sentencia As String)

        Select Case sentencia
            Case "SELECT"
                If valor Is System.DBNull.Value Then
                    Return "is null"
                Else
                    Return valor
                End If
            Case "UPDATE"
                If valor Is System.DBNull.Value Then
                    Return "NULL"
                Else
                    Return "'" & valor & "'"
                End If
        End Select
    End Function

    Shared Function comprobarValoresQ(ByVal escala As Object, ByVal calado As Object, ByVal caudal As Object, ByVal parada As Object, _
                                    ByVal dudaCalidad As Object, ByVal regimenCurva As Object, ByVal observaciones As Object, ByVal comando As SqlCommand)
        Dim errorRespuesta As String = ""
        Dim campo As String = ""
        Dim valorDoble As Double
        Dim valorEntero As Integer

        Try
            If Not escala Is System.DBNull.Value Then
                campo = "Escala"
                valorDoble = CDbl(escala)
            End If
            If Not calado Is System.DBNull.Value Then
                campo = "Calado"
                valorDoble = CDbl(calado)
            End If
            If Not caudal Is System.DBNull.Value Then
                campo = "Caudal"
                valorDoble = CDbl(caudal)
            End If
            If Not parada Is System.DBNull.Value Then
                campo = "Parada"
                valorEntero = CInt(parada)
                If CInt(parada) <> parada Then
                    errorRespuesta = "El campo Parada tiene que ser un valor entero"
                End If
            End If
            If Not dudaCalidad Is System.DBNull.Value Then
                campo = "DudaCalidad"
                valorEntero = CInt(dudaCalidad)
                If CInt(dudaCalidad <> dudaCalidad) Then
                    errorRespuesta = "El campo DudaCalidad tiene que ser un valor entero"
                End If
            End If

        Catch ex As Exception
            errorRespuesta = "No ha puesto un valor correcto para el campo " & campo
        End Try

        If Not regimenCurva Is System.DBNull.Value Then
            comando.CommandText = "Select * from PVYCR_Acequias_Regimenes where regimen='" & regimenCurva & "'"
            If comando.ExecuteScalar Is Nothing Then
                errorRespuesta = "El campo regimenCurva no tiene un valor correcto"
            End If
        End If

        If Not observaciones Is System.DBNull.Value Then
            If observaciones.ToString.Length > 255 Then
                errorRespuesta = "El campo observaciones no puede tener más de 255 carácteres"
            End If
        End If

        Return errorRespuesta
    End Function


    Shared Function comprobarValoresE(ByVal lecturaI As Object, ByVal lecturaII As Object, ByVal lecturaIII As Object, ByVal TotalKwh As Object, _
                                    ByVal TotalKvar As Object, ByVal consumoElectricoAdicional As Object, ByVal reinicioLecturaElectrica As Object, _
                                    ByVal idIncidenciaElectrica As Object, ByVal funciona As Object, ByVal justificacion As Object, ByVal comando As SqlCommand)
        Dim errorRespuesta As String = ""
        Dim campo As String = ""
        Dim valorDoble As Double
        Dim valorEntero As Integer

        Try
            If Not lecturaI Is System.DBNull.Value Then
                campo = "LecturaI"
                valorDoble = CDbl(lecturaI)
            End If
            If Not lecturaII Is System.DBNull.Value Then
                campo = "LecturaII"
                valorDoble = CDbl(lecturaII)
            End If
            If Not lecturaIII Is System.DBNull.Value Then
                campo = "LecturaIII"
                valorDoble = CDbl(lecturaIII)
            End If
            If Not TotalKwh Is System.DBNull.Value Then
                campo = "TotalKwh"
                valorDoble = CDbl(TotalKwh)
            End If
            If Not TotalKvar Is System.DBNull.Value Then
                campo = "TotalKvar"
                valorDoble = CDbl(TotalKvar)
            End If
            If Not consumoElectricoAdicional Is System.DBNull.Value Then
                campo = "consumoElectricoAdicional"
                valorDoble = CDbl(consumoElectricoAdicional)
            End If
            If Not reinicioLecturaElectrica Is System.DBNull.Value Then
                campo = "reinicioLecturaElectrica"
                valorDoble = CDbl(reinicioLecturaElectrica)
            End If

            If Not idIncidenciaElectrica Is System.DBNull.Value Then
                campo = "idIncidenciaElectrica"
                valorEntero = CInt(idIncidenciaElectrica)
                If CInt(idIncidenciaElectrica <> idIncidenciaElectrica) Then
                    errorRespuesta = "El campo idIncidenciaElectrica tiene que ser un valor entero"
                Else
                    If idIncidenciaElectrica <> 0 Then      'La 0 existe para todos (V,E,H)
                        comando.CommandText = "Select * from PVYCR_Incidencias where tipoincidencia='E' and idincidencia='" & idIncidenciaElectrica & "'"
                        If comando.ExecuteScalar Is Nothing Then
                            errorRespuesta = "El campo idIncidenciaElectrica no tiene un valor correcto"
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            errorRespuesta = "No ha puesto un valor correcto para el campo " & campo
        End Try

        If Not funciona Is System.DBNull.Value Then
            If funciona.ToString.Length > 2 Then
                errorRespuesta = "El campo funciona no puede tener más de 2 carácteres"
            End If
        End If

        If Not justificacion Is System.DBNull.Value Then
            If justificacion.ToString.Length > 50 Then
                errorRespuesta = "El campo justificación no puede tener más de 50 carácteres"
            End If
        End If

        Return errorRespuesta
    End Function

    Shared Function comprobarValoresV(ByVal lecturaContador As Object, ByVal consumoVolumetricoAdicional As Object, ByVal reinicioLecturaVolumetrica As Object, _
                                    ByVal idIncidenciaVolumetrica As Object, ByVal caudalMedido As Object, ByVal funciona As Object, ByVal justificacion As Object, ByVal comando As SqlCommand)
        Dim errorRespuesta As String = ""
        Dim campo As String = ""
        Dim valorDoble As Double
        Dim valorEntero As Integer

        Try
            If Not lecturaContador Is System.DBNull.Value Then
                campo = "lecturaContador"
                valorDoble = CDbl(lecturaContador)
            End If
            If Not consumoVolumetricoAdicional Is System.DBNull.Value Then
                campo = "consumoVolumetricoAdicional"
                valorDoble = CDbl(consumoVolumetricoAdicional)
            End If
            If Not reinicioLecturaVolumetrica Is System.DBNull.Value Then
                campo = "reinicioLecturaVolumetrica"
                valorDoble = CDbl(reinicioLecturaVolumetrica)
            End If


            If Not idIncidenciaVolumetrica Is System.DBNull.Value Then
                campo = "idIncidenciaVolumetrica"
                valorEntero = CInt(idIncidenciaVolumetrica)
                If CInt(idIncidenciaVolumetrica <> idIncidenciaVolumetrica) Then
                    errorRespuesta = "El campo idIncidenciaVolumetrica tiene que ser un valor entero"
                Else
                    If idIncidenciaVolumetrica <> 0 Then      'La 0 existe para todos (V,E,H)
                        comando.CommandText = "Select * from PVYCR_Incidencias where tipoincidencia='V' and idincidencia='" & idIncidenciaVolumetrica & "'"
                        If comando.ExecuteScalar Is Nothing Then
                            errorRespuesta = "El campo idIncidenciaVolumetrica no tiene un valor correcto"
                        End If
                    End If
                End If
            End If

            If Not caudalMedido Is System.DBNull.Value Then
                campo = "caudalMedido"
                valorEntero = CInt(caudalMedido)
                If CInt(caudalMedido <> caudalMedido) Then
                    errorRespuesta = "El campo caudalMedido tiene que ser un valor entero"
                End If
            End If

        Catch ex As Exception
            errorRespuesta = "No ha puesto un valor correcto para el campo " & campo
        End Try

        If Not funciona Is System.DBNull.Value Then
            If funciona.ToString.Length > 2 Then
                errorRespuesta = "El campo funciona no puede tener más de 2 carácteres"
            End If
        End If

        If Not justificacion Is System.DBNull.Value Then
            If justificacion.ToString.Length > 50 Then
                errorRespuesta = "El campo justificación no puede tener más de 50 carácteres"
            End If
        End If

        Return errorRespuesta
    End Function

    Shared Function comprobarValoresH(ByVal lecturaHorometro As Object, ByVal consumoVolumetricoAdicional As Object, ByVal reinicioLecturaVolumetrica As Object, _
                                    ByVal idIncidenciaVolumetrica As Object, ByVal funciona As Object, ByVal comando As SqlCommand)
        Dim errorRespuesta As String = ""
        Dim campo As String = ""
        Dim valorDoble As Double
        Dim valorEntero As Integer

        Try
            If Not lecturaHorometro Is System.DBNull.Value Then
                campo = "lecturaHorometro"
                valorDoble = CDbl(lecturaHorometro)
            End If
            If Not consumoVolumetricoAdicional Is System.DBNull.Value Then
                campo = "consumoVolumetricoAdicional"
                valorDoble = CDbl(consumoVolumetricoAdicional)
            End If
            If Not reinicioLecturaVolumetrica Is System.DBNull.Value Then
                campo = "reinicioLecturaVolumetrica"
                valorDoble = CDbl(reinicioLecturaVolumetrica)
            End If


            If Not idIncidenciaVolumetrica Is System.DBNull.Value Then
                campo = "idIncidenciaVolumetrica"
                valorEntero = CInt(idIncidenciaVolumetrica)
                If CInt(idIncidenciaVolumetrica <> idIncidenciaVolumetrica) Then
                    errorRespuesta = "El campo idIncidenciaVolumetrica tiene que ser un valor entero"
                Else
                    If idIncidenciaVolumetrica <> 0 Then      'La 0 existe para todos (V,E,H)
                        comando.CommandText = "Select * from PVYCR_Incidencias where tipoincidencia='H' and idincidencia='" & idIncidenciaVolumetrica & "'"
                        If comando.ExecuteScalar Is Nothing Then
                            errorRespuesta = "El campo idIncidenciaVolumetrica no tiene un valor correcto"
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            errorRespuesta = "No ha puesto un valor correcto para el campo " & campo
        End Try

        If Not funciona Is System.DBNull.Value Then
            If funciona.ToString.Length > 2 Then
                errorRespuesta = "El campo funciona no puede tener más de 2 carácteres"
            End If
        End If

        Return errorRespuesta
    End Function

    Shared Function VerSiEsModificado(ByVal valorAnt, ByVal valorNuevo) As Boolean
        If valorNuevo Is System.DBNull.Value Then
            If valorAnt Is System.DBNull.Value Then
                Return False
            Else
                If valorAnt <> "" Then
                    Return True
                Else
                    Return False
                End If
            End If
        Else
            If valorAnt Is System.DBNull.Value Then
                If valorNuevo <> "" Then
                    Return True
                Else
                    Return False
                End If
            Else
                If valorAnt <> valorNuevo Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function
End Class
