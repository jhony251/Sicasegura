Imports GuarderiaFluvial.JavaScript
Partial Class abandon
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Abandon()
        Response.Redirect("index.aspx")
    End Sub
End Class
