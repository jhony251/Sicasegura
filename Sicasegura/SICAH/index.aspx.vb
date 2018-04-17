Imports GuarderiaFluvial
Partial Class SICAH_index
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Session("usuarioReg") = Nothing) Or (Session("loginUsuario") = Nothing) Then
            Response.Redirect("UsuarioNoRegistrado.aspx")
            Exit Sub
        End If
        'Put user code to initialize the page here
        lblCabecera.Text = genHTML.cabecera(Page)
        lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))
    End Sub
 
End Class
