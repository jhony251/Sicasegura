Imports GuarderiaFluvial
Partial Class SICAH_CerrarAplicacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'ncm 25/01/2010 insertamos en log
        'Dim conexion As Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
        'utiles.InsertarLog("S", Session("loginUsuario"), conexion, Page)
        'conexion.Close()
        'Session.Abandon()
    End Sub
End Class
