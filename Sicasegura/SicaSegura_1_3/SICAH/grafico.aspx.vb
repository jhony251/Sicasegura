Imports Sicadll
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        variable = Request.QueryString("var")
        If Request.QueryString("idAgrupacion") <> Nothing Then

            CalculoValores(Request.QueryString("idAgrupacion"))

        End If
    End Sub
    Private Sub Array_datos(ByVal idAgrupacion As String)
        Dim i, v As Int16
        Dim str As String
        'Select Case variable
        '    Case "consumo"
        '        v = 6
        '        LIT_LinkConcesion.Text = "<a href='grafico.aspx?var=concesion&idAgrupacion=" & idAgrupacion & "'>"
        '    Case "concesion"
        '        v = 6
        '        LIT_LinkConsumo.Text = "<a href='grafico.aspx?var=consumo&idAgrupacion=" & idAgrupacion & "'>"
        'End Select
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
            str = str & "[Date.UTC(" & anyo & ", " & mes & ", " & dia & ", " & hora & ", " & minuto & ", " & segundo & "), " & valor & "],"
        Next
        str = str.Substring(0, str.Length - 1)
        str = str & "];"
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
        sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM " & _
                 " FROM         SICA_SIST_PuntoSistema INNER JOIN [SICA_SIST_Sistemas-Punto] ON SICA_SIST_PuntoSistema.ID = [SICA_SIST_Sistemas-Punto].ID_PuntoSistema" & _
                 " INNER JOIN     SICA_SIST_Sistemas ON    SICA_SIST_Sistemas.ID=[SICA_SIST_Sistemas-Punto].ID_Sistema" & _
                 " WHERE [SICA_SIST_Sistemas-Punto].ID_Sistema='" & idAgrupacion & "'"
        Dim dstPuntos As New Data.DataSet()
        utiles.Comprobar_Conexion_BD(Page, conexionDBSica)
        daAgrupaciones.SelectCommand.CommandText = sentencia
        daAgrupaciones.Fill(dstPuntos, "TablaPuntos")

        If DateTime.Now.Month < 10 Then
            FechaFin = "30/09/" & DateTime.Now.Year
            FechaIni = "01/10/" & DateTime.Now.Year - 1
        Else
            FechaFin = "30/09/" & DateTime.Now.Year + 1
            FechaIni = "01/10/" & DateTime.Now.Year
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
        For i = 0 To 11 Step 1
            'Se obtienen las fechas
          
            Dim volumenGeneral As Double = 0
            Dim concesionAprovechamiento As Integer = 0

            For Each fila In dstPuntos.Tables("TablaPuntos").Rows

                Try
                    'el volumen y la concesión son acumulados
                    resultado = utiliSica.obtenerAcumulado(fila("codigoPVYCR"), fila("EM").ToString().Trim().ToUpper(), conexion, FechaIni, FechaIniAux.AddMonths(1).AddDays(-1))

                Catch
                    Alert(Page, "Se ha producido un error en un Elemento de Medida")
                    Continue For
                End Try

                'Se calculan los acumulados
                volumenGeneral += resultado(1)
                concesionAprovechamiento += resultado(2)


            Next


            str = str & "[Date.UTC(" & FechaIniAux.Year & ", " & FechaIniAux.Month & ", " & FechaIniAux.Day & ", " & "00" & ", " & "01" & ", " & "01" & "), " & volumenGeneral & "],"
            str2 = str2 & "[Date.UTC(" & FechaIniAux.Year & ", " & FechaIniAux.Month & ", " & FechaIniAux.Day & ", " & "00" & ", " & "01" & ", " & "01" & "), " & concesionAprovechamiento & "],"

            'str = str & "[ " & volumenGeneral & "],"
            'str2 = str2 & "[" & concesionAprovechamiento & "],"


            FechaIniAux = FechaIniAux.AddMonths(1)

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
End Class
