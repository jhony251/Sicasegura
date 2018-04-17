Imports Sicadll
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO

Partial Class SICAH_grafico2
    Inherits System.Web.UI.Page
    Public dt As Data.DataTable
    Public variable, pvycr As String
    Dim conexionDBSica As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_dbsica"))
    Dim daAgrupaciones As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionDBSica)
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim conexionRegistro As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_RegistroAguas"))
    Dim daRegistro As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionRegistro)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        variable = Request.QueryString("var")
        If Request.QueryString("idAgrupacion") <> Nothing Then

            CalculoValores(Request.QueryString("idAgrupacion"))

        End If
    End Sub
    
    Protected Function EjecutaSQL(ByVal queryString As String) As Data.DataTable
        Dim connectionString As String
        connectionString = ConfigurationSettings.AppSettings("dsn_dbsica").ToString()
        Dim dtemp As Data.DataTable
        dtemp = New Data.DataTable()
        Dim connection As System.Data.SqlClient.SqlConnection
        connection = New System.Data.SqlClient.SqlConnection(connectionString)
        Dim adapter As System.Data.SqlClient.SqlDataAdapter
        adapter = New System.Data.SqlClient.SqlDataAdapter(queryString, connection)
        adapter.Fill(dtemp)
        Return dtemp
    End Function

    Protected Sub CalculoValores(ByVal idAgrupacion As String)

        Dim sentencia As String = ""
        Dim FechaIni As Date
        Dim FechaFin As Date
        sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, SICA_SIST_Sistemas.NumInscripcion " & _
                 " FROM         SICA_SIST_PuntoSistema INNER JOIN [SICA_SIST_Sistemas-Punto] ON SICA_SIST_PuntoSistema.ID = [SICA_SIST_Sistemas-Punto].ID_PuntoSistema" & _
                 " INNER JOIN     SICA_SIST_Sistemas ON    SICA_SIST_Sistemas.ID=[SICA_SIST_Sistemas-Punto].ID_Sistema" & _
                 " WHERE [SICA_SIST_Sistemas-Punto].ID_Sistema='" & idAgrupacion & "'"
        Dim dstPuntos As New Data.DataSet()
        utiles.Comprobar_Conexion_BD(Page, conexionDBSica)
        daAgrupaciones.SelectCommand.CommandText = sentencia
        daAgrupaciones.Fill(dstPuntos, "TablaPuntos")

        FechaIni = "01/01/2004"

        If DateTime.Now.Month < 10 Then
            FechaFin = "30/09/" & DateTime.Now.Year
        Else
            FechaFin = "30/09/" & DateTime.Now.Year + 1
        End If
        Dim v As Int16
        Dim cadena As String = ""
        Dim utiliSica As New Sicadll.AccesoADatos.AccesoADatos2
        Dim PorcentajeTotal As Double = 0
        'Se comprueba las fechas
        Dim resultado As Object
        Dim str As String
        str = "var Values=["
        Dim str2 As String
        str2 = "var Values2=["
        'Se obtienen los resultados de cada uno de los elementos de medida que
        'componen el punto
        Dim FechaIniAux As Date = FechaIni
        Dim FechaFinAux As Date


        For i = 0 To FechaFin.Year() - FechaIni.Year() Step 1
            'Se obtienen las fechas

            Dim volumenGeneral As Double = 0
            Dim concesionAprovechamiento As Integer = 0

            For Each fila In dstPuntos.Tables("TablaPuntos").Rows
                Try
                    If (fila("codigoPVYCR").ToString().Trim() = "" Or fila("EM").ToString().Trim() = "") Then
                    Else
                        If FechaIniAux.Year() = FechaFin.Year() Then
                            'el volumen y la concesión son acumulados
                            resultado = utiliSica.obtenerAcumulado(fila("codigoPVYCR"), fila("EM").ToString().Trim().ToUpper(), conexion, FechaIniAux, FechaFin)
                        Else
                            'el volumen y la concesión son acumulados
                            resultado = utiliSica.obtenerAcumulado(fila("codigoPVYCR"), fila("EM").ToString().Trim().ToUpper(), conexion, FechaIniAux, "30/09/" & FechaIniAux.AddYears(1).Year())
                        End If
                        'Se calculan los acumulados
                        volumenGeneral += resultado(1)
                        If fila("NumInscripcion").ToString() = "" Then
                            concesionAprovechamiento += resultado(2)
                        End If
                    End If
                    

                Catch
                    Alert(Page, "Se ha producido un error en un Elemento de Medida")
                    Continue For
                End Try
                
            Next

            If dstPuntos.Tables("TablaPuntos").Rows.Count > 0 Then

                If dstPuntos.Tables("TablaPuntos").Rows(0)("NumInscripcion").ToString() <> "" Then
                    concesionAprovechamiento = ObtenerVolumenInscripcion(dstPuntos.Tables("TablaPuntos").Rows(0)("NumInscripcion"))
                End If
            End If

            str = str & "[Date.UTC(" & FechaIniAux.Year & ", " & Convert.ToString(Convert.ToInt16(FechaIniAux.Month) - 1) & ", " & FechaIniAux.Day & ", " & "00" & ", " & "01" & ", " & "01" & "), " & Convert.ToString(volumenGeneral / 1000000).Replace(",", ".") & "],"
            str2 = str2 & "[Date.UTC(" & FechaIniAux.Year & ", " & Convert.ToString(Convert.ToInt16(FechaIniAux.Month) - 1) & ", " & FechaIniAux.Day & ", " & "00" & ", " & "01" & ", " & "01" & "), " & Convert.ToString(concesionAprovechamiento / 1000000).Replace(",", ".") & "],"
            'str = str & Convert.ToString(volumenGeneral / 1000000).Replace(",", ".") & ","
            'str2 = str2 & Convert.ToString(concesionAprovechamiento / 1000000).Replace(",", ".") & ","
            FechaIniAux = "01/10/" & FechaIniAux.AddYears(1).Year()

        Next


        str = str.Substring(0, str.Length - 1)
        str = str & "];"

        str2 = str2.Substring(0, str2.Length - 1)
        str2 = str2 & "];"

        Dim cstext2 As New StringBuilder()
        cstext2.Append("<script type=""text/javascript"">")
        cstext2.Append(str & str2)
        cstext2.Append("</script>")
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "strData", cstext2.ToString(), False)
    End Sub

    Protected Function ObtenerVolumenInscripcion(ByVal NumInscripcion As Integer) As Double
        Dim VolumenTotal As Double = 0
        Dim dstRegistro As DataSet = New System.Data.DataSet()
        Try
            'Si la Agrupación es RAAC los datos, se cogen de la inscripción
            'Es la suma total de los volúmenes de abastecimiento, riego...
            utiles.Comprobar_Conexion_BD(Page, conexionRegistro)
            daRegistro.SelectCommand.CommandText = "Select * from AprovechamientosInscritos WHERE NUMPAL=" & NumInscripcion
            daRegistro.Fill(dstRegistro, "TablaRegistro")

            If dstRegistro.Tables("TablaRegistro").Rows.Count > 0 Then
                VolumenTotal += utiles.nullACero(dstRegistro.Tables("TablaRegistro").Rows(0)("VOLRE")) + utiles.nullACero(dstRegistro.Tables("TablaRegistro").Rows(0)("VOLAB")) + utiles.nullACero(dstRegistro.Tables("TablaRegistro").Rows(0)("VOLOT")) + utiles.nullACero(dstRegistro.Tables("TablaRegistro").Rows(0)("VOLIN")) + utiles.nullACero(dstRegistro.Tables("TablaRegistro").Rows(0)("VOLGA")) + utiles.nullACero(dstRegistro.Tables("TablaRegistro").Rows(0)("VOLHI"))
                'VolumenTotal += utiles.nullACero(dstRegistro.Tables("TablaRegistro").Rows(0)("VOLRE")) + utiles.nullACero(dstRegistro.Tables("TablaRegistro").Rows(0)("VOLAB")) + utiles.nullACero(dstRegistro.Tables("TablaRegistro").Rows(0)("VOLOT"))

            End If

            Return VolumenTotal


        Catch Excepcion As Exception
            Return "0"
        End Try



    End Function
End Class
