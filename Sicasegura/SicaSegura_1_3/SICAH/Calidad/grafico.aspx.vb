
Partial Class SICAH_Calidad_grafico
    Inherits System.Web.UI.Page
    Public dt As Data.DataTable
    Public variable, pvycr As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        variable = Request.QueryString("var")
        pvycr = Request.QueryString("pvycr")
        dt = EjecutaSQL("SELECT * FROM SICA_Topkapi_calidad WHERE codigopvycr like '" + pvycr + "%' AND DATEPART(mi,Fecha)=0")
        Array_datos()
    End Sub
    Private Sub Array_datos()
        Dim i, v As Int16
        Dim str As String
        Select Case variable
            Case "PH"
                v = 5
                LIT_LinkTemp.Text = "<a href='grafico.aspx?var=temperatura&pvycr=" & pvycr & "'>"
                LIT_LinkConduct.Text = "<a href='grafico.aspx?var=conductividad&pvycr=" & pvycr & "'>"
                LIT_LinkLDO.Text = "<a href='grafico.aspx?var=LDO&pvycr=" & pvycr & "'>"
                LIT_LinkTurb.Text = "<a href='grafico.aspx?var=turbidez&pvycr=" & pvycr & "'>"
            Case "temperatura"
                v = 6
                LIT_LinkPH.Text = "<a href='grafico.aspx?var=PH&pvycr=" & pvycr & "'>"
                LIT_LinkConduct.Text = "<a href='grafico.aspx?var=conductividad&pvycr=" & pvycr & "'>"
                LIT_LinkLDO.Text = "<a href='grafico.aspx?var=LDO&pvycr=" & pvycr & "'>"
                LIT_LinkTurb.Text = "<a href='grafico.aspx?var=turbidez&pvycr=" & pvycr & "'>"
            Case "conductividad"
                v = 2
                LIT_LinkPH.Text = "<a href='grafico.aspx?var=PH&pvycr=" & pvycr & "'>"
                LIT_LinkTemp.Text = "<a href='grafico.aspx?var=temperatura&pvycr=" & pvycr & "'>"
                LIT_LinkLDO.Text = "<a href='grafico.aspx?var=LDO&pvycr=" & pvycr & "'>"
                LIT_LinkTurb.Text = "<a href='grafico.aspx?var=turbidez&pvycr=" & pvycr & "'>"
            Case "LDO"
                v = 4
                LIT_LinkPH.Text = "<a href='grafico.aspx?var=PH&pvycr=" & pvycr & "'>"
                LIT_LinkTemp.Text = "<a href='grafico.aspx?var=temperatura&pvycr=" & pvycr & "'>"
                LIT_LinkConduct.Text = "<a href='grafico.aspx?var=conductividad&pvycr=" & pvycr & "'>"
                LIT_LinkTurb.Text = "<a href='grafico.aspx?var=turbidez&pvycr=" & pvycr & "'>"
            Case "turbidez"
                v = 3
                LIT_LinkPH.Text = "<a href='grafico.aspx?var=PH&pvycr=" & pvycr & "'>"
                LIT_LinkTemp.Text = "<a href='grafico.aspx?var=temperatura&pvycr=" & pvycr & "'>"
                LIT_LinkConduct.Text = "<a href='grafico.aspx?var=conductividad&pvycr=" & pvycr & "'>"
                LIT_LinkLDO.Text = "<a href='grafico.aspx?var=LDO&pvycr=" & pvycr & "'>"
            Case Else
                v = 5
                LIT_LinkTemp.Text = "<a href='grafico.aspx?var=temperatura&pvycr=" & pvycr & "'>"
                LIT_LinkConduct.Text = "<a href='grafico.aspx?var=conductividad&pvycr=" & pvycr & "'>"
                LIT_LinkLDO.Text = "<a href='grafico.aspx?var=LDO&pvycr=" & pvycr & "'>"
                LIT_LinkTurb.Text = "<a href='grafico.aspx?var=turbidez&pvycr=" & pvycr & "'>"
        End Select
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
        'connectionString = ConfigurationManager.ConnectionStrings("DBSICA").ToString()
        connectionString = ConfigurationSettings.AppSettings("dsn_dbsica")
        Dim dtemp As Data.DataTable
        dtemp = New Data.DataTable()
        Dim connection As System.Data.SqlClient.SqlConnection
        connection = New System.Data.SqlClient.SqlConnection(connectionString)
        Dim adapter As System.Data.SqlClient.SqlDataAdapter
        adapter = New System.Data.SqlClient.SqlDataAdapter(queryString, connection)
        adapter.Fill(dtemp)
        Return dtemp
    End Function
End Class
