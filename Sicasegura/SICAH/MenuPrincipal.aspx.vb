Imports GuarderiaFluvial
Partial Class SICAH_MenuPrincipal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            lblCabecera.Text = genHTML.cabecera(Page)
      lblPestanyas.Text = genHTML.pestanyasMenu(4, "../", Page, Session("idperfil"), Session("usuarioReg"))
      Response.Redirect("caucepuntMAIN.aspx?nodobusqueda=n&valor=0")
        End If

    End Sub

End Class
