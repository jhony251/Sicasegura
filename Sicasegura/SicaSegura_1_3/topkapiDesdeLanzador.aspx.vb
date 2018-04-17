Imports System
Partial Class topkapiDesdeLanzador
    Inherits System.Web.UI.Page

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Request.QueryString("icono") = 1 Then

      If (Request.ServerVariables("REMOTE_ADDR").ToString.Substring(0, 6) = "10.31.") Or (Request.ServerVariables("REMOTE_ADDR").ToString() = "89.140.163.10") Then
        Response.Redirect("http://172.17.30.53/ESQUEMAS_SEGURA/")
      Else
        Response.Redirect("http://192.168.194.11/ESQUEMAS_SEGURA/")
      End If
    ElseIf Request.QueryString("icono") = 3 Then
      If (Request.ServerVariables("REMOTE_ADDR").ToString.Substring(0, 6) = "10.31.") Or (Request.ServerVariables("REMOTE_ADDR").ToString() = "89.140.163.10") Then
        Response.Redirect(ConfigurationManager.AppSettings("urlVisor_topkapi_CHS") & "?NumFeuille=50")
      Else
        Response.Redirect(ConfigurationManager.AppSettings("urlVisor_topkapi"))
      End If

    ElseIf Request.QueryString("icono") = 4 Then

      If (Request.ServerVariables("REMOTE_ADDR").ToString.Substring(0, 6) = "10.31.") Or (Request.ServerVariables("REMOTE_ADDR").ToString() = "89.140.163.10") Then
        Response.Redirect(ConfigurationManager.AppSettings("urlVisor_topkapi_CHS"))
      Else
        Response.Redirect(ConfigurationManager.AppSettings("urlVisor_topkapi"))
      End If

    End If
  End Sub
End Class
