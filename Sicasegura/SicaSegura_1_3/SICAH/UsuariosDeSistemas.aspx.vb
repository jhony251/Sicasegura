Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports System.IO
Partial Class SICAH_UsuariosDeSistemas
    Inherits System.Web.UI.Page

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_dbsica"))
    Dim da As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0
    Dim midst As DataSet = New System.Data.DataSet()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblCabecera.Text = genHTML.cabecera(Page)

        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If

            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))

            If Request.QueryString("nuevo") = "yes" Then
                pnlEDUsuarios.Visible = True
                pnlBotones.Visible = True
                pnlUsuarios.Visible = False
                cargarControlesEdicion()
                lblTitulo.Text = "<b>NUEVO USUARIO DE SISTEMA</b>"
            End If
        End If

        crearDataSets()

    End Sub


    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Private Sub crearDataSets()
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        If txtRegistros.Text = "" Then
            sentenciaSel = " SELECT "
        Else
            sentenciaSel = " SELECT TOP " & txtRegistros.Text
        End If

        sentenciaSel = sentenciaSel & " * FROM SICA_SIST_Usuario "

        If Session("FiltroSistema") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroSistema")
            Else
                sFiltro = sFiltro + Session("FiltroSistema")
            End If
        End If


        sentenciaOrder = " order by Login "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If

        If dst.Tables.Contains("TablaUsuarios") Then
            dst.Tables("TablaUsuarios").Clear()
        End If

        da.SelectCommand.CommandText = sentenciaSel
        da.Fill(dst, "TablaUsuarios")
        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = da.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        rptCauces.DataSource = dst.Tables("TablaUsuarios")
        rptCauces.DataBind()
    End Sub

    Private Sub crearDataSetCauce(ByVal StrCodigoCauceSeleccionado As String)
        If dst.Tables.Contains("TablaUsuarios") Then
            dst.Tables("TablaUsuarios").Clear()
        End If

        utiles.Comprobar_Conexion_BD(Page, conexion)
        da.SelectCommand.CommandText = "select * from SICA_SIST_Usuario where ID_Usuario=" & lblIdSel.Text
        da.Fill(dst, "TablaUsuarios")
    End Sub

    Protected Sub nuevoUsuario(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIdSel.Text = ""
        Response.Redirect("UsuariosDeSistemas.aspx?nuevo=yes")

    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim comando As SqlCommand = New SqlCommand("", conexion)
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        Dim idUsuario As Integer

        Try
            If lblIdSel.Text = "" Then
                comando.CommandText = "Select max(ID_Usuario) from [SICA_SIST_Usuario]"
                Dim maxId As Integer = utiles.nullACero(comando.ExecuteScalar)
                comando.CommandText = "INSERT INTO [SICA_SIST_Usuario]  (ID_Usuario,Login,Externo,Nombre,DNI,Password) VALUES (" & maxId + 1 & _
                                    ",@Login,@Externo,@Nombre,@DNI,@Password) "
                idUsuario = maxId + 1
            Else
                comando.CommandText = "UPDATE [SICA_SIST_Usuario] SET  " & _
                                      " Login=@Login, Externo=@Externo, Nombre=@Nombre, DNI=@DNI, Password=@Password where ID_Usuario=" & Me.lblIdSel.Text
                idUsuario = lblIdSel.Text
            End If

            comando.Parameters.AddWithValue("Login", utiles.BlancoANull(Me.txtLogin.Text))
            comando.Parameters.AddWithValue("Externo", chbExterno.Checked)
            comando.Parameters.AddWithValue("Nombre", utiles.BlancoANull(Me.txtNombre.Text))
            comando.Parameters.AddWithValue("DNI", utiles.BlancoANull(Me.txtDNI.Text))
            comando.Parameters.AddWithValue("Password", utiles.BlancoANull(Me.txtPassword.Text))
            comando.ExecuteNonQuery()

            'Primero borramos todas las relaciones de ese usuario y las volvemos a crear
            comando.CommandText = "DELETE [SICA_SIST_Sistemas-Usuarios] where ID_Usuario=" & idUsuario
            comando.ExecuteNonQuery()

            For i As Integer = 0 To lbSistemas.Items.Count - 1
                If lbSistemas.Items(i).Selected = True Then
                    If lbSistemas.Items(i).Value <> 0 Then
                        comando.CommandText = "INSERT INTO [sica_sist_sistemas-usuarios] (id_sistema,id_usuario) values (" & lbSistemas.Items(i).Value & _
                                            ", " & idUsuario & ")"
                        comando.ExecuteNonQuery()
                    End If
                End If
            Next

        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message)
        End Try

        Me.pnlUsuarios.Visible = True
        pnlEDUsuarios.Visible = False
        pnlBotones.Visible = False
        lblIdSel.Text = ""
        crearDataSets()
        Response.Redirect("UsuariosDeSistemas.aspx")
    End Sub


    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.pnlUsuarios.Visible = True
        pnlEDUsuarios.Visible = False
        pnlBotones.Visible = False
        lblIdSel.Text = ""
        crearDataSets()
        Me.lblTitulo.Text = "MANTENIMIENTO USUARIO DE SISTEMA:"
    End Sub

    Protected Sub rptCauces_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptCauces.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Me.pnlUsuarios.Visible = False
                pnlEDUsuarios.Visible = True
                pnlBotones.Visible = True

                lblIdSel.Text = e.CommandArgument
                crearDataSetCauce(lblIdSel.Text)
                cargarControlesEdicion()

                rptCauces.DataSource = dst.Tables("TablaUsuarios")
                rptCauces.DataBind()
            Case "borrar"
                utiles.Comprobar_Conexion_BD(Page, conexion)
                Try
                    lblIdSel.Text = e.CommandArgument
                    'Primero quitaremos las relaciones del usuario con los sistemas
                    comando.CommandText = "delete [sica_sist_sistemas-usuarios] where id_usuario=" & lblIdSel.Text
                    comando.ExecuteNonQuery()

                    comando.CommandText = "Delete [SICA_SIST_Usuario] where id_usuario=" & lblIdSel.Text
                    comando.ExecuteNonQuery()
                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el usuario")
                End Try
                Me.pnlUsuarios.Visible = True
                pnlEDUsuarios.Visible = False
                pnlBotones.Visible = False
                lblIdSel.Text = ""
                crearDataSets()
        End Select
    End Sub
    Protected Sub cargarControlesEdicion()
        If lblIdSel.Text <> "" Then
            Me.txtLogin.Text = utiles.nullABlanco(dst.Tables("TablaUsuarios").Rows(0).Item("Login"))
            chbExterno.Checked = utiles.nullAFalse(dst.Tables("TablaUsuarios").Rows(0).Item("Externo"))
            txtNombre.Text = utiles.nullABlanco(dst.Tables("TablaUsuarios").Rows(0).Item("Nombre"))
            txtDNI.Text = utiles.nullABlanco(dst.Tables("TablaUsuarios").Rows(0).Item("DNI"))
            txtPassword.Text = utiles.nullABlanco(dst.Tables("TablaUsuarios").Rows(0).Item("Password"))
            If chbExterno.Checked = True Then
                pnlDatos.Visible = True
            End If
        End If
        If dst.Tables.Contains("TablaSistemas") = False Then
            'Cogemos sólo los privados pq los públicos se supone que los puede ver todo el mundo                
            da.SelectCommand.CommandText = "Select * from SICA_SIST_Sistemas where publico=0 "
            da.Fill(dst, "TablaSistemas")
        End If

        lbSistemas.DataSource = dst.Tables("TablaSistemas")
        lbSistemas.DataTextField = "Descripcion"
        lbSistemas.DataValueField = "ID"
        lbSistemas.DataBind()

        If dst.Tables("TablaSistemas").Rows.Count = 0 Then
            lbSistemas.Items.Add(New System.Web.UI.WebControls.ListItem("[No hay sistemas disponibles]", "0"))
        End If

        If lblIdSel.Text <> "" Then
            For i As Integer = 0 To dst.Tables("TablaSistemas").Rows.Count - 1
                comando.CommandText = "select count(*) from [SICA_SIST_Sistemas-Usuarios] where id_usuario=" & lblIdSel.Text & " and id_sistema=" & _
                                    dst.Tables("TablaSistemas").Rows(i).Item("id")
                If utiles.nullACero(comando.ExecuteScalar()) <> 0 Then
                    lbSistemas.Items(i).Selected = True
                End If
            Next
        End If

    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub AceptarFiltro(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strFiltro As String
        strFiltro = ""
        If txtFLogin.Text <> "" Then
            strFiltro = strFiltro + " AND Login like '" + txtFLogin.Text + "' "
        End If

        Session("FiltroSistema") = strFiltro
        crearDataSets()
    End Sub

    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        crearDataSets()
    End Sub

    Protected Function verLbl() As String
        Return lblIdSel.Text
    End Function
    Protected Function Chequeado(ByVal valor As Object) As String
        If utiles.nullAFalse(valor) = 0 Then
            Return ""
        Else
            Return "checked"
        End If
    End Function

    Protected Sub chbExterno_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbExterno.CheckedChanged
        If chbExterno.Checked = True Then
            pnlDatos.Visible = True
        Else
            pnlDatos.Visible = False
        End If
    End Sub
End Class
