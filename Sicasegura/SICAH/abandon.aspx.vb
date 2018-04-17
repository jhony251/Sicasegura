Imports GuarderiaFluvial
Imports System.Data.SqlClient.SqlConnection
Partial Class abandon
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Abandon()
        'ncm 25/01/2010 insertamos en log
        Dim conexion As Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
        utiles.InsertarLog("S", Session("loginUsuario"), conexion, Page)
        utiles.CerrarConexion(conexion)
        Response.Redirect("../index.aspx")
    End Sub
End Class
