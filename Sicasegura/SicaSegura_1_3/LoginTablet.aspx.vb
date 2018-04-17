
Partial Class LoginTablet
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("usuarioReg") = 1
        'http://sicasegura.tragsatec.es/LoginTablet.aspx?url=http://sicasegura.tragsatec.es/SICAH/CaucePuntDETALLE.aspx?nodotexto=S045C223401P01-H01%20-%20HORAS&nodoclave=S045C223401P01;E;H&LecturasVisor=L

        Dim url As String = ""
        For i As Integer = 0 To Request.QueryString.Count - 1
            url &= "&" & Request.QueryString.AllKeys(i) & "=" & Request.QueryString(i)
        Next

        url = Replace(url, "&url=", "")
        Response.Redirect(url)
    End Sub
End Class
