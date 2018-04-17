Imports HTMLTablaEmbalses
Partial Class Lanzador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim HTE As HTMLTablaEmbalses.HTMLTablaEmbalses
        HTE = New HTMLTablaEmbalses.HTMLTablaEmbalses

        Embalses.Text = HTE.GetHTMLCode()

    End Sub
End Class
