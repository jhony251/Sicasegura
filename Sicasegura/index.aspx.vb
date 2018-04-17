Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript

Partial Class index
    Inherits System.Web.UI.Page
    Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Private Const NumeroIntentos As Integer = 3

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Redirect("../inicio")
        If Not IsPostBack Then
            lblCabecera.Text = genHTML.cabecera(Page)
            'iframeInicio.Attributes.Add("src", "http://192.168.194.11/topkapi/autologin.asp?Operateur=jcarril1&motpasse=jcarril1")
            'iframeInicio.Attributes.Add("src", "http://172.17.30.53/topkapi/autologin.asp?Operateur=jcarril1&motpasse=jcarril1")
            'ncm comentado el 28/10/2009 por la lineas de bajo
            'iframeInicio.Attributes.Add("src", "http://172.17.30.53/topkapi/Autosica.asp")
            'ClientScript.RegisterStartupScript(Page.GetType, "foco", "<script language=javascript>document.getElementById('" & txtLogin.ClientID & "').focus();</script>")
        End If
        'Response.Redirect("../inicio")
        'lblPestanyas.Text = genHTML.pestanyasMenu(1, "")        

    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim idUsuario As Integer = 0
        Dim rdr As SqlDataReader
        Dim encontrado As Boolean = False
        'ncm 04/11/2008 comentado porque lo cambiamos por comprobar_conexion
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion, True)

        comando.CommandText = "select u.*,j.id as idJerarquia, j.prefijo from tusuarios u join tcargos c on c.id=u.idcargo join tjerarquias j on j.id=c.idjerarquia where u.login=@login and u.password=@password"
        comando.Parameters.AddWithValue("@login", txtLogin.Text)
        comando.Parameters.AddWithValue("@password", utiles.Encripta(txtPassword.Text))
        rdr = comando.ExecuteReader()
        If rdr.Read() Then
            Session("usuarioReg") = rdr("id")
            Session("nombreUsuario") = rdr("nombre") & " " & rdr("Apellidos")
			Session("loginUsuario") = rdr("login")
            Session("idJerarquiaUsuario") = rdr("idjerarquia")
            Session("idCargoUsuario") = rdr("idCargo")
            Session("prefijo") = rdr("prefijo")
            Session("idPerfil") = rdr("idPerfil")

            Page.RegisterClientScriptBlock("Cancelar_cambioContraseña", "<script language=javascript> " & _
            "function cancelar_encontrado(){if (" & Session("idJerarquiaUsuario") & " == 19) " & _
            "{ if ((" & Session("idPerfil") & "== 1) || (" & Session("idPerfil") & "== 11) || (" & Session("idPerfil") & "== 14)){ " & _
                " document.location=""SICAH/menuPrincipal.aspx""; }" & _
            "else if (" & Session("idperfil") & "== 10){ " & _
                " document.location=""SICAH/caucepuntMAIN.aspx"";} " & _
            "else { alert(""No tiene acceso a esta aplicación"");}} else {  alert(""No tiene acceso a esta aplicación"");}} </script>")
            'Page.RegisterClientScriptBlock("Cancelar_cambioContraseña", "<script language=javascript> " & _
            '"function cancelar_encontrado(){ if (19 == 19) " & _
            '"{ if ((19 == 1) || (19 == 11)){ " & _
            '    " document.location=""SICAH/motores.aspx""; }" & _
            '"else if (19 == 10){ " & _
            '    " document.location=""SICAH/caucepuntMAIN.aspx"";} " & _
            '"else { alert(""No tiene acceso a esta aplicación"");}} else {  alert(""No tiene acceso a esta aplicación"");}} </script>")

            lblClaveIncorrecta.Text = ""
            If (rdr("FechaCaducidad") Is System.DBNull.Value) Or (utiles.nullaFechaMin(rdr("FechaCaducidad")) > Now()) Then
                If Not rdr("FechaCaducidad") Is System.DBNull.Value Then
                    'vamos a comprobar cuantos días faltan para que caduque la contraseña
                    Dim dias As Integer = DateDiff(DateInterval.Day, Now, rdr("FechaCaducidad"))
                    If dias <= 7 Then
                        Page.RegisterStartupScript("abreVentanaConfirmacion", "<script language=javascript>" & _
                             "var x = window.confirm('Faltan menos de 7 días para que caduque la contraseña ¿Desea Cambiarla?');if (x){ document.location='CambioPassword.aspx';} else cancelar_encontrado(); </script>")
                        Exit Sub
                    Else
                        encontrado = True
                    End If
                Else
                    rdr.Close()
                    'si tiene fecha de caducidad nula le ponemos una fecha para que tenga que cambiar la contraseña cada mes
                    comando.CommandText = "update tusuarios set FechaCaducidad = dateadd(month,3,getdate()) " & _
                    "where login = '" & txtLogin.Text & "' and password= '" & utiles.Encripta(txtPassword.Text) & "' "
                    comando.ExecuteNonQuery()
                    encontrado = True
                End If
            Else
                encontrado = False
                rdr.Close()
                conexion.Close()
                Response.Redirect("UsuarioBloqueado.aspx")
            End If

        Else
            'comprobamos que el usuario
            'NCM 01/07/2008 Permitir varios intentos
            Session("veces") = CInt(Session("veces")) + 1
            If Session("veces") < NumeroIntentos Then
                lblClaveIncorrecta.Text = "Usuario o Password incorrecto" '"Quedan " & (NumeroIntentos - veces) & " intentos"
                rdr.Close()
                conexion.Close()
                Exit Sub
            Else
                'aquí hacemos un update de la tabla usuarios añadiendo la fecha de caducidad
                'ncm 04/11/2008 comentado porque lo cambiamos por comprobar_conexion
                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion, True)
                rdr.Close()
                comando.CommandText = "update GFLUVIAL.dbo.tusuarios set fechacaducidad = getdate() where login = '" & txtLogin.Text & "' "
                comando.ExecuteNonQuery()

                conexion.Close()
                Response.Redirect("UsuarioBloqueado.aspx")
            End If
            encontrado = False
        End If
        rdr.Close()
        conexion.Close()

        If encontrado Then
            Dim ipaddr As String = Request.ServerVariables("REMOTE_ADDR")
            Dim cadena() As String
            'NCM Si es de la jerarquía PVYCR, redirigimos a SICAH
            If Session("idJerarquiaUsuario") = 19 Then
                'ncm insertamos en la tabla log
                utiles.InsertarLog("E", Session("loginUsuario"), conexion, Page)
                'If Request.ServerVariables("SERVER_NAME").ToLower.IndexOf(System.Configuration.ConfigurationManager.AppSettings("urlSICAH")) >= 0 Then
                cadena = Split(System.Configuration.ConfigurationManager.AppSettings("urlSICAH"), ",")
                ' MyArray(0) contains "VBScript".
                ' MyArray(1) contains "is".
                ' MyArray(2) contains "fun!".
                ' si es administrador o grabador de PVY 
                If Session("idPerfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
                    'ncm 22/10/2008 comentado para probar lo del menú de preproducción con submenú.
                    'Response.Redirect("SICAH/motores.aspx")
                    Response.Redirect("SICAH/menuPrincipal.aspx")
                ElseIf Session("idperfil") = 10 Then
                    Response.Redirect("SICAH/caucepuntMAIN.aspx")
                Else
                    Alert(Page, "No tiene acceso a esta aplicación")
                End If
            Else
                Alert(Page, "No tiene acceso a esta aplicación")
            End If
        End If
    End Sub

    Protected Sub btnCambiarP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCambiarP.Click
        Response.Redirect("CambioPassword.aspx")
    End Sub
    'Protected Sub encontrado()
    '    Dim ipaddr As String = Request.ServerVariables("REMOTE_ADDR")
    '    Dim cadena() As String
    '    'NCM Si es de la jerarquía PVYCR, redirigimos a SICAH
    '    If Session("idJerarquiaUsuario") = 19 Then

    '        'If Request.ServerVariables("SERVER_NAME").ToLower.IndexOf(System.Configuration.ConfigurationManager.AppSettings("urlSICAH")) >= 0 Then
    '        cadena = Split(System.Configuration.ConfigurationManager.AppSettings("urlSICAH"), ",")
    '        ' MyArray(0) contains "VBScript".
    '        ' MyArray(1) contains "is".
    '        ' MyArray(2) contains "fun!".
    '        ' si es administrador o grabador de PVY 
    '        If Session("idPerfil") = 1 Or Session("idPerfil") = 11 Then
    '            Response.Redirect("SICAH/motores.aspx")
    '        ElseIf Session("idperfil") = 10 Then
    '            Response.Redirect("SICAH/caucepuntMAIN.aspx")
    '        Else
    '            Alert(Page, "No tiene acceso a esta aplicación")
    '        End If
    '    Else
    '        Alert(Page, "No tiene acceso a esta aplicación")
    '    End If

    'End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        utiles.CerrarConexion(conexion)
    End Sub
End Class


