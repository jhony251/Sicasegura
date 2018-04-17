
Partial Class Lanzador
  Inherits System.Web.UI.Page
  

  'Protected Sub A1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles A1.Click
  '  If (Request.ServerVariables("REMOTE_ADDR").ToString.Substring(0, 6) = "10.31.") Or (Request.ServerVariables("REMOTE_ADDR").ToString() = "89.140.163.10") Then
  '    Response.Redirect(ConfigurationManager.AppSettings("urlVisor_topkapi_CHS") & "autologin.asp?Operateur=sicasegura&motpasse=sicasegura")
  '  Else
  '    Response.Redirect(ConfigurationManager.AppSettings("urlVisor_topkapi") & "autologin.asp?Operateur=sicasegura&motpasse=sicasegura")
  '  End If
  'End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

  End Sub
End Class
