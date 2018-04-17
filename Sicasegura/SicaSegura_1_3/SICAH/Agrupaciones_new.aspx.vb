Imports GuarderiaFluvial
Partial Class SICAH_Agrupaciones_new
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblCabecera.Text = genHTML.cabecera(Page)
        lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))

    End Sub
End Class
