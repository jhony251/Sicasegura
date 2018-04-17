Imports GuarderiaFluvial
Imports System.IO
Partial Class AccesoVisorTOPKAPI
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Session("usuarioReg") = Nothing Then
      Response.Redirect("Sicah/UsuarioNoRegistrado.aspx")
      Exit Sub
    End If
        'If (Request.ServerVariables("REMOTE_ADDR").ToString.Substring(0, 6) = "10.31.") Or (Request.ServerVariables("REMOTE_ADDR").ToString() = "89.140.163.10") Then
        '    Response.Redirect(ConfigurationManager.AppSettings("urlVisor_topkapi_CHS") & "autologin.asp?Operateur=sicasegura&motpasse=sicasegura")
        'Else
        '    Response.Redirect(ConfigurationManager.AppSettings("urlVisor_topkapi") & "autologin.asp?Operateur=sicasegura&motpasse=sicasegura")
        'End If
		If (Request.ServerVariables("REMOTE_ADDR").ToString.Substring(0, 6) = "10.31.") Or (Request.ServerVariables("REMOTE_ADDR").ToString() = "89.140.163.10") Then
            Response.Redirect(ConfigurationManager.AppSettings("urlVisor_topkapi_CHS") & "")
        Else
            Response.Redirect(ConfigurationManager.AppSettings("urlVisor_topkapi") & "")
        End If

    End Sub
End Class
