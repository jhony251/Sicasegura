Imports GuarderiaFluvial


Partial Class SICAH_Agrupaciones_new
    Inherits System.Web.UI.Page

    Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        Dim SICA_Log As SicaSegura.SICA_Log = New SicaSegura.SICA_Log()
        SICA_Log.Set_APP("SICASEGURA")
        SICA_Log.Set_USER(Session("loginUsuario"))
        SICA_Log.Set_PROFILE(Session("perfilCHS"))
        SICA_Log.Set_ESPAGINA("1")
        SICA_Log.Set_ESINFORME("0")
        SICA_Log.Set_DISPOSITIVO(Request.Browser.Platform.ToString())

        ClientScript.RegisterStartupScript(Page.GetType, "Maximizar", "<script></script>")
        'If (Session("usuarioReg") = Nothing) Or (Session("loginUsuario") = Nothing) Then
        'Response.Redirect("UsuarioNoRegistrado.aspx")
        'Exit Sub

        'End If
        SICA_Log.acceso_pagina("Agrupaciones")



        If Not IsPostBack Then
            'Response.Write("L=" & Session("loginUsuario"))
            Try
                If (Session("perfilCHS").ToString().Contains("SICA") = False) Then
                    Response.Redirect("http://sica.chsegura.es")
                End If

            Catch
                Response.Redirect("http://sica.chsegura.es")
            End Try
        End If
        lblCabecera.Text = genHTML.cabecera(Page)
        lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))

    End Sub
End Class
