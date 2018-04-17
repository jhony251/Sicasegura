Imports System.Data.SqlClient
Imports GuarderiaFluvial.utiles

Partial Class index
    Inherits System.Web.UI.Page
    Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim conexion_dbsica As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_dbsica"))
    Dim comando As SqlCommand=New SqlCommand("",conexion_dbsica)

    Private Function Comprobar_usuario_y_contrasenya(ByVal usuario As String, ByVal pass As String) As Boolean

        If conexion_dbsica.State = Data.ConnectionState.Closed Then conexion_dbsica.Open()
        comando.CommandText = "select id_usuario from sica_sist_usuario where login='" & usuario & "' and password='" & pass & "'"
        If comando.ExecuteScalar <> 0 Then
            Session("id_usuario") = comando.ExecuteScalar
            Session("nombreUsuario") = usuario
            conexion.Close()
            Return True
        Else
            conexion_dbsica.Close()
            Return False
        End If
    End Function

    Protected Sub BTN_login_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTN_login.Click
        Dim LC As SicaSegura.SICA_LibroControl = New SicaSegura.SICA_LibroControl(TXT_usuario.Text, TXT_pass.Text)
        Dim titular As SicaSegura.SICA_Titular_Aprovechamiento = New SicaSegura.SICA_Titular_Aprovechamiento

        Session("LCA") = LC
        If Comprobar_usuario_y_contrasenya(TXT_usuario.Text, TXT_pass.Text) Then
            If (titular.Pendiente_de_validar_datos_de_contacto(LC.ID_Inscripcion) = True) Then
                Response.Redirect("home/infousuario.aspx")
            Else
                Response.Redirect("home/index.aspx")
            End If

        Else
            TXT_usuario.Text = ""
            TXT_pass.Text = ""
            'lblIncorrecto.Visible = True
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim InterfazLibroControl As SicaSegura.SICA_Interfaz.SICA_LibroControl = New SicaSegura.SICA_Interfaz.SICA_LibroControl()

        Session.Clear()
        Dim menu As String = "Inicio de sesion-index.aspx#Alta Usuario-index.aspx"
        HTML_Links_Sup_Izq.Text = InterfazLibroControl.Get_Enlaces_Sup_Izq_login(menu.Split("#"))
        HTML_Subcabecera_Logos.Text = InterfazLibroControl.Get_Cabecera_login()
        HTML_Menu_Navegacion.Text = InterfazLibroControl.Get_Menu_Navegacion_login()
        HTML_Pie_Logo_Corporativo.Text = InterfazLibroControl.Get_pie_logo_corporativo()
        HTML_Pie_pagina.Text = InterfazLibroControl.Get_pie_pagina()
        NineRays.Reporting.Register.SetLicenseKey("4761628416024064132096")
    End Sub
End Class
