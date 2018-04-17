Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Partial Class CambioPassword
    Inherits System.Web.UI.Page
    Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Private Const NumeroIntentos As Integer = 3

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            lblCabecera.Text = genHTML.cabecera(Page)
        End If
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim idUsuario As Integer = 0
        Dim rdr As SqlDataReader
        Dim encontrado As Boolean = False

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
            lblClaveIncorrecta.Text = ""
            If (rdr("FechaCaducidad") Is System.DBNull.Value) Or (utiles.nullaFechaMin(rdr("FechaCaducidad")) > Now()) Then
                'If Session("CambiarCalve") = "S" Then
                'comprobamos que la clave nueva sea distinta a la repetida          
                If txtNuevaP.Text <> txtRepetirP.Text Then
                    Alert(Page, "Los campos nuevo password y repetir password no coinciden")
                    encontrado = False
                Else
                    'aquí cambiamos el password
                    'If conexion.State = ConnectionState.Closed Then conexion.Open()
                    utiles.Comprobar_Conexion_BD(Page, conexion, True)
                    rdr.Close()
                    comando.CommandText = "update tusuarios set password= '" & utiles.Encripta(txtNuevaP.Text) & "',FechaCaducidad = dateadd(month,3,getdate()) " & _
                    "where login = '" & txtLogin.Text & "' and password= '" & utiles.Encripta(txtPassword.Text) & "' "
                    comando.ExecuteNonQuery()
                    encontrado = True
                End If
                'Session("CambiarCalve") = "N"
                ' End If
                'encontrado = True
            Else
                encontrado = False
                conexion.Close()
                Response.Redirect("UsuarioBloqueado.aspx")
            End If

        Else
            'comprobamos que el usuario
            'NCM 01/07/2008 Permitir varios intentos
            lblClaveIncorrecta.Text = "Usuario o Password incorrecto" '"Quedan " & (NumeroIntentos - veces) & " intentos"
            encontrado = False
        End If
        rdr.Close()
        conexion.Close()

        If encontrado Then
            Dim ipaddr As String = Request.ServerVariables("REMOTE_ADDR")
            Dim cadena() As String
            'NCM Si es de la jerarquía PVYCR, redirigimos a SICAH
            If Session("idJerarquiaUsuario") = 19 Then
                'ncm 25/01/2010 insertamos en log
                utiles.InsertarLog("E", Session("loginUsuario"), conexion, Page)
                'If Request.ServerVariables("SERVER_NAME").ToLower.IndexOf(System.Configuration.ConfigurationManager.AppSettings("urlSICAH")) >= 0 Then
                cadena = Split(System.Configuration.ConfigurationManager.AppSettings("urlSICAH"), ",")
                ' MyArray(0) contains "VBScript".
                ' MyArray(1) contains "is".
                ' MyArray(2) contains "fun!".
                ' si es administrador o grabador de PVY 
                If Session("idPerfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
                    Response.Redirect("SICAH/motores.aspx")
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


    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("index.aspx")
    End Sub

  
End Class
