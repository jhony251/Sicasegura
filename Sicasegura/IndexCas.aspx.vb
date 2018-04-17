Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript

Partial Class IndexCas
    Inherits System.Web.UI.Page
    Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Private Const NumeroIntentos As Integer = 3

#Region " Web Form Designer Generated Code "
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
    End Sub
#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            'Aqui hay que recoger las variables y crear la sesion
            

            Dim argumentos(4) As String
            argumentos = Request.QueryString("args").Split("~")

            If ((argumentos(3) = "Consultor_SICA") Or (argumentos(3) = "Grabador_SICA") Or (argumentos(3) = "Administrador_SICA") Or (argumentos(3) = "Grabador")) Then
                Session("usuarioReg") = "99"
                Session.Add("perfilCHS", argumentos(3))
                Session.Add("nombreUsuario", argumentos(1))
                Session("nombreUsuario") = argumentos(1)
                Session.Add("loginUsuario", argumentos(0))
                Session("loginUsuario") = argumentos(0)
                Session("idJerarquiaUsuario") = "11"
                Session("idCargoUsuario") = "2"
                Session("prefijo") = "1"
                'If argumentos(2) = "39" Then
                '    Session("idPerfil") = "10"
                'ElseIf argumentos(2) = "38" Then
                '    Session("idPerfil") = "11"
                'Else
                '    Session("idPerfil") = "14"
                'End If
                If argumentos(3) = "Consultor_SICA" Then
                    Session("idPerfil") = "10"
                ElseIf argumentos(3) = "Grabador_SICA" Then
                    Session("idPerfil") = "11"
                Else
                    Session("idPerfil") = "14"
                End If
                'Session("idPerfil") = "11"
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "Alert", "<script>alert('" + argumentos(2) + "');</script>", True)

            End If



            Dim SICA_Log As SicaSegura.SICA_Log = New SicaSegura.SICA_Log()
            SICA_Log.Set_APP("SICASEGURA")
            SICA_Log.Set_USER(Session("loginUsuario"))
            SICA_Log.Set_PROFILE(Session("perfilCHS"))
            SICA_Log.Set_ESPAGINA("1")
            SICA_Log.Set_ESINFORME("0")
            SICA_Log.Set_DISPOSITIVO(Request.Browser.Platform.ToString())

            SICA_Log.acceso_pagina("Indexcas")



            Dim usuario = Session("loginUsuario")

            Dim db As SicaSegura.SICA_DB = New SicaSegura.SICA_DB()
            Dim dt As DataTable
            dt = db.GetDataDBSICA("SELECT DISTINCT usuario FROM SICA_LOG_Actividad")
            Dim i As Integer
            Dim listausuarios As String
            For i = 0 To dt.Rows.Count - 1
                listausuarios = String.Concat(listausuarios, dt.Rows(i).Item("usuario").ToString().Trim(), "#@")
            Next
            'listausuarios = listausuarios.Replace("ATSICA5", "__")

            'If listausuarios.ToUpper().Contains(usuario.ToString().ToUpper()) = True Then

            If (Session("idPerfil").ToString() = "10") Then
                Response.Redirect("SICAH/Agrupaciones_new.aspx?pag=5")
            ElseIf (Session("idPerfil").ToString() = "11") Then
                Response.Redirect("SICAH/Agrupaciones_new.aspx?pag=5")
            ElseIf ((Session("idPerfil").ToString() = "1") Or (Session("idPerfil").ToString() = "14")) Then
                Response.Redirect("SICAH/Agrupaciones_new.aspx?pag=5")
            Else

            End If
            'Else
            'Response.Write("<img src='SICAH/images/cab_izq.jpg'><br><br>")
            'Response.Write("<h2>Por favor, disculpe las molestias.<br>Se ha producido un error en el proceso de Log-in<br>")
            'Response.Write("</h2><br><br><h3>Cierre el navegador y vuelva a intentarlo<br>")
            'Response.Write("Limpie las cookies y vuelva a intentarlo<br>")
            'Response.Write("Contacte con el siguiente buzón de correo si finalmente no consigue entrar<br>")
            'Response.Write("Email : Sica@chsegura.es<br>")
            'Response.Write("<br><br></h3>")
            'Response.Write("<br><h2>Atenderemos su solicitud lo antes posible")
            'Response.Write("<br>Disculpu las molestias</h2>")
            'End If


        End If
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        utiles.CerrarConexion(conexion)
    End Sub
End Class

