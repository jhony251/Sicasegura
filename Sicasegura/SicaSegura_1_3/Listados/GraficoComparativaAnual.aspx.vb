
Partial Class Listados_GraficoComparativaAnual
    Inherits System.Web.UI.Page

    Protected Function getUrlGrafico() As String
        Return Server.UrlEncode(Request.QueryString("url"))
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
