﻿Imports Sicadll
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO

Partial Class SICAH_grafico
    Inherits System.Web.UI.Page
    Public dt As Data.DataTable
    Public variable, pvycr As String
    Dim conexionDBSica As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_dbsica"))
    Dim daAgrupaciones As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionDBSica)
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim conexionRegistro As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_RegistroAguas"))
    Dim daRegistro As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionRegistro)


    Dim FF, FI As DateTime
    Dim suministrado As Double
    Dim factordeajusteescalagrafico As Integer = 1




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        variable = Request.QueryString("var")

        If Request.QueryString("FI") <> Nothing Then
            FI = DateTime.Parse(Request.QueryString("FI"))
        End If
        If Request.QueryString("FF") <> Nothing Then
            FF = DateTime.Parse(Request.QueryString("FF"))
        End If

        If Request.QueryString("idAgrupacion") <> Nothing Then
            CalculoValores(Request.QueryString("idAgrupacion"))
        End If

        If Request.QueryString("suministrado") <> Nothing Then
            suministrado = Double.Parse(Request.QueryString("suministrado"))
        End If

        If suministrado > 1000000 Then
            factordeajusteescalagrafico = 1000000
            HTML_JS_Grafico.Text = "<script src='js/grafico.js' type='text/javascript'></script><script type='text/javascript'>graficoHM();</script>"
        Else
            factordeajusteescalagrafico = 1
            HTML_JS_Grafico.Text = "<script src='js/grafico.js' type='text/javascript'></script><script type='text/javascript'>graficoM3();</script>"
        End If


        'series: [{ name: 'Suministrado(hm3)', type: 'area', data: Values, tooltip: { yDecimals: 2}},{ name: 'Concesión (hm3)', data: Values2, type: 'line', tooltip: { yDecimals: 2} }]


    End Sub
    Private Sub Array_datos(ByVal idAgrupacion As String)
        Dim i, v As Int16
        Dim str As String = ""
        'Select Case variable
        '    Case "consumo"
        '        v = 6
        '        LIT_LinkConcesion.Text = "<a href='grafico.aspx?var=concesion&idAgrupacion=" & idAgrupacion & "'>"
        '    Case "concesion"
        '        v = 6
        '        LIT_LinkConsumo.Text = "<a href='grafico.aspx?var=consumo&idAgrupacion=" & idAgrupacion & "'>"
        'End Select
        If dt.Rows.Count > 0 Then
            str = "var Values=["
            For i = 0 To dt.Rows.Count - 1
                Dim anyo As String
                anyo = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(1).ToString()).Year.ToString()
                Dim mes As String
                mes = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(1).ToString()).Month.ToString()
                Dim dia As String
                dia = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(1).ToString()).Day.ToString()
                Dim hora As String
                hora = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(1).ToString()).Hour.ToString()
                Dim minuto As String
                minuto = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(1).ToString()).Minute.ToString()
                Dim segundo As String
                segundo = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(1).ToString()).Second.ToString()
                Dim valor As String
                valor = dt.Rows(i).ItemArray.GetValue(v).ToString().Trim()
                If i = 1 Then
                    str = str & "[Date.UTC(" & anyo & ", " & Convert.ToString(Convert.ToInt16(mes) - 2) & ", 30, " & hora & ", " & minuto & ", " & segundo & "), 0 ],"
                Else
                    str = str & "[Date.UTC(" & anyo & ", " & Convert.ToString(Convert.ToInt16(mes) - 1) & ", " & dia & ", " & hora & ", " & minuto & ", " & segundo & "), " & valor & "],"
                End If
            Next

            str = str.Substring(0, str.Length - 1)

            str = str & "];"
        End If

        Dim cstext2 As New StringBuilder()
        cstext2.Append("<script type=""text/javascript"">")
        cstext2.Append(str)
        cstext2.Append("</script>")
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "strData", cstext2.ToString(), False)

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
        Dim idSistema As String

        sentencia = "SELECT ID FROM SICA_SIST_Sistemas WHERE (NumInscripcion=" + idAgrupacion.Trim().ToString() + ")"
        Dim db As SicaSegura.SICA_DB = New SicaSegura.SICA_DB()
        Dim dt As DataTable = db.GetDataDBSICA(sentencia)

        idSistema = dt.Rows(0).Item(0).ToString()

        sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, SICA_SIST_Sistemas.NumInscripcion " & _
                 " FROM         SICA_SIST_PuntoSistema INNER JOIN [SICA_SIST_Sistemas-Punto] ON SICA_SIST_PuntoSistema.ID = [SICA_SIST_Sistemas-Punto].ID_PuntoSistema" & _
                 " INNER JOIN     SICA_SIST_Sistemas ON    SICA_SIST_Sistemas.ID=[SICA_SIST_Sistemas-Punto].ID_Sistema" & _
                 " WHERE [SICA_SIST_Sistemas-Punto].ID_Sistema='" & idSistema & "'"
        Dim dstPuntos As New Data.DataSet()
        utiles.Comprobar_Conexion_BD(Page, conexionDBSica)
        daAgrupaciones.SelectCommand.CommandText = sentencia
        daAgrupaciones.Fill(dstPuntos, "TablaPuntos")

        If (FF = Nothing) And (FI = Nothing) Then
            If DateTime.Now.Month < 10 Then
                FechaFin = "30/09/" & DateTime.Now.Year
                FechaIni = "01/10/" & DateTime.Now.Year - 1
            Else
                FechaFin = "30/09/" & DateTime.Now.Year + 1
                FechaIni = "01/10/" & DateTime.Now.Year
            End If
        Else
            FechaFin = FF
            FechaIni = FI
        End If

        Dim v As Int16
        'Select Case variable
        '    Case "consumo"
        '        v = 1
        '        LIT_LinkConcesion.Text = "<a href='grafico.aspx?var=concesion&idAgrupacion=" & idAgrupacion & "'>"
        '    Case "concesion"
        '        v = 2
        '        LIT_LinkConsumo.Text = "<a href='grafico.aspx?var=consumo&idAgrupacion=" & idAgrupacion & "'>"
        'End Select

        Dim cadena As String = ""

        Dim utiliSica As New Sicadll.AccesoADatos.AccesoADatos2


        Dim PorcentajeTotal As Double = 0

        'Desde FechaIni hasta FechaFin mes a mes
        Dim PrimerMes As Integer = 10
        Dim UltimoMes As Integer = 9


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
        For i = 0 To MonthDifference(FI, FF) Step 1
            'Se obtienen las fechas

            Dim volumenGeneral As Double = 0
            Dim concesionAprovechamiento As Integer = 0

            For Each fila In dstPuntos.Tables("TablaPuntos").Rows

                Try
                    If (fila("codigoPVYCR").ToString().Trim() = "" Or fila("EM").ToString().Trim() = "") Then
                        'Este if es por si se añade algún elemento a la agrupación que no tiene EM
                    Else
                        'el volumen y la concesión son acumulados
                        resultado = utiliSica.obtenerAcumulado(fila("codigoPVYCR"), fila("EM").ToString().Trim().ToUpper(), conexion, FechaIni, FechaIniAux.AddMonths(1).AddDays(-1))
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

            If ((FechaIniAux.Year = Date.Today.Year) And (FechaIniAux.Month >= Date.Today.Month)) Then

            Else

                If i = 0 Then
                    str = str & "[Date.UTC(" & FechaIniAux.Year & ", " & Convert.ToString(Convert.ToInt16(FechaIniAux.Month) - 2) & ", 30, " & "00" & ", " & "01" & ", " & "01" & "), 0],"
                Else
                    str = str & "[Date.UTC(" & FechaIniAux.Year & ", " & Convert.ToString(Convert.ToInt16(FechaIniAux.Month) - 1) & ", " & FechaIniAux.Day & ", " & "00" & ", " & "01" & ", " & "01" & "), " & Convert.ToString(volumenGeneral / factordeajusteescalagrafico).Replace(",", ".") & "],"
                End If
                str2 = str2 & "[Date.UTC(" & FechaIniAux.Year & ", " & Convert.ToString(Convert.ToInt16(FechaIniAux.Month) - 1) & ", " & FechaIniAux.Day & ", " & "00" & ", " & "01" & ", " & "01" & "), " & Convert.ToString(concesionAprovechamiento / factordeajusteescalagrafico).Replace(",", ".") & "],"

            End If


            

            'str = str & "[ " & volumenGeneral & "],"
            'str2 = str2 & "[" & concesionAprovechamiento & "],"


            FechaIniAux = FechaIniAux.AddMonths(1)

        Next
        str = str.Substring(0, str.Length - 1)
        str = str & "];"

        str2 = str2.Substring(0, str2.Length - 1)
        str2 = str2 & "];"


        Dim cstext2 As New StringBuilder()

        If str.Length > 10 Or str.Length > 10 Then
            cstext2.Append("<script type=""text/javascript"">")
            cstext2.Append(str & str2)
            cstext2.Append("</script>")
        End If
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "strData", cstext2.ToString(), False)
    End Sub


    Public Function MonthDifference(ByVal lValue As DateTime, ByVal rValue As DateTime) As Integer
        Return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year))
    End Function

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