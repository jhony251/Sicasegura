Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing

Partial Class SICAH_AccesoTopkapi
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")

        lblCabecera.Text = genHTML.cabecera(Page)
        lblPestanyas.Text = genHTML.pestanyasMenu(8, "../", Page, Session("idperfil"), Session("usuarioReg"))
        iframeVisor.Attributes.Add("src", ConfigurationManager.AppSettings("urlVisor_topkapi") & "autologin.asp?Operateur=sicasegura&motpasse=sicasegura")

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        iframeVisor.Attributes.Add("src", ConfigurationManager.AppSettings("urlVisor_topkapi") & "AuRevoir.asp")
    End Sub
End Class
