Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.IO
Imports SICAH_FuncionesArbolExt
Partial Class SICAH_sistemas
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
                pnlEDCauces.Visible = True
                pnlBotones.Visible = True
                pnlCauces.Visible = False
                lblTitulo.Text = "<b>NUEVA AGRUPACIÓN</b>"
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

        sentenciaSel = sentenciaSel & " * FROM SICA_SIST_Sistemas "

        If Session("FiltroSistema") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroSistema")
            Else
                sFiltro = sFiltro + Session("FiltroSistema")
            End If
        End If


        sentenciaOrder = " order by Descripcion "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If

        If dst.Tables.Contains("TablaAgrupaciones") Then
            dst.Tables("TablaAgrupaciones").Clear()
        End If

        da.SelectCommand.CommandText = sentenciaSel
        da.Fill(dst, "TablaAgrupaciones")
        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = da.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        rptCauces.DataSource = dst.Tables("TablaAgrupaciones")
        rptCauces.DataBind()
    End Sub

    Private Sub crearDataSetCauce(ByVal StrCodigoCauceSeleccionado As String)
        If dst.Tables.Contains("TablaAgrupaciones") Then
            dst.Tables("TablaAgrupaciones").Clear()
        End If

        utiles.Comprobar_Conexion_BD(Page, conexion)
        da.SelectCommand.CommandText = "select * from SICA_SIST_Sistemas where Id=" & lblIdSel.Text
        da.Fill(dst, "TablaAgrupaciones")
    End Sub

    Protected Sub nuevoCauce(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIdSel.Text = ""
        Response.Redirect("Sistemas.aspx?nuevo=yes")

    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim comando As SqlCommand = New SqlCommand("", conexion)
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        Dim idUsuario, maxId, maxIdUsuario As Integer
        comando.CommandText = "SELECT ID_Usuario from SICA_SIST_Usuario where Login='" & Session("loginUsuario") & "'"
        idUsuario = utiles.nullACero(comando.ExecuteScalar)

        If pnlIDRaac.Visible = True Then
            txtNombreAgrup.Text = ""
        End If

        If pnlNombreAgrup.Visible = True Then
            txtNumInscripcion.Text = ""
        End If

        Try
            If lblIdSel.Text = "" Then
                comando.CommandText = "Select max(id) from [SICA_SIST_Sistemas]"
                maxId = utiles.nullACero(comando.ExecuteScalar)
                comando.CommandText = "INSERT INTO [SICA_SIST_Sistemas]  (ID,Descripcion,Publico, NumInscripcion, Nombre) VALUES (" & maxId + 1 & ",@Descripcion,@Publico,@NumInscripcion, @Nombre) "
                comando.Parameters.AddWithValue("Descripcion", utiles.BlancoANull(Me.txtDescripcion.Text))
                comando.Parameters.AddWithValue("Publico", chbPublico.Checked)
                comando.Parameters.AddWithValue("NumInscripcion", utiles.BlancoANull(Me.txtNumInscripcion.Text))
                comando.Parameters.AddWithValue("Nombre", utiles.BlancoANull(Me.txtNombreAgrup.Text))
                comando.ExecuteNonQuery()

                'Insertamos también en la tabla que enlaza con usuarios                
                If idUsuario = 0 Then
                    comando.CommandText = "select max(ID_Usuario) from SICA_SIST_Usuario"
                    maxIdUsuario = utiles.nullACero(comando.ExecuteScalar)
                    comando.CommandText = "insert into SICA_SIST_Usuario (ID_Usuario, Login) values (" & maxIdUsuario + 1 & ", '" & Session("loginUsuario") & "')"
                    comando.ExecuteNonQuery()
                    idUsuario = maxIdUsuario + 1
                End If

                comando.CommandText = "INSERT INTO [SICA_SIST_Sistemas-Usuarios] (ID_Sistema, ID_Usuario) values (" & maxId + 1 & ", " & _
                        idUsuario & ")"
                comando.ExecuteNonQuery()

            Else
                comando.CommandText = "UPDATE [SICA_SIST_Sistemas] SET  " & _
                                      " Descripcion=@Descripcion, Publico=@Publico, NumInscripcion=@NumInscripcion, Nombre=@Nombre where ID=" & Me.lblIdSel.Text
                comando.Parameters.AddWithValue("Descripcion", utiles.BlancoANull(Me.txtDescripcion.Text))
                comando.Parameters.AddWithValue("Publico", chbPublico.Checked)
                comando.Parameters.AddWithValue("NumInscripcion", utiles.BlancoANull(Me.txtNumInscripcion.Text))
                comando.Parameters.AddWithValue("Nombre", utiles.BlancoANull(Me.txtNombreAgrup.Text))
                comando.ExecuteNonQuery()
            End If
        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message)
        End Try

        Me.pnlCauces.Visible = True
        pnlEDCauces.Visible = False
        pnlBotones.Visible = False
        lblIdSel.Text = ""
        crearDataSets()
        Response.Redirect("Sistemas.aspx")
    End Sub




    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.pnlCauces.Visible = True
        pnlEDCauces.Visible = False
        pnlBotones.Visible = False
        lblIdSel.Text = ""
        crearDataSets()
        Me.lblTitulo.Text = "MANTENIMIENTO SISTEMA:"
    End Sub

    Protected Sub rptCauces_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptCauces.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Session("nuevoSistema") = 0
                Me.pnlCauces.Visible = False
                pnlEDCauces.Visible = True
                pnlBotones.Visible = True

                lblIdSel.Text = e.CommandArgument
                crearDataSetCauce(lblIdSel.Text)
                cargarControlesEdicion()

                rptCauces.DataSource = dst.Tables("TablaAgrupaciones")
                rptCauces.DataBind()
            Case "borrar"
                utiles.Comprobar_Conexion_BD(Page, conexion)
                Try
                    lblIdSel.Text = e.CommandArgument
                    'Primero nos guardamos los identificadores de los punto que pertenecen a este sistema.
                    da.SelectCommand.CommandText = "select ID_PuntoSistema from [SICA_SIST_Sistemas-Punto] where ID_Sistema=" & lblIdSel.Text
                    da.Fill(dst, "TablaPuntosSistema")
                    'Después borramos la relación
                    comando.CommandText = "Delete [SICA_SIST_Sistemas-Punto] where ID_Sistema=" & lblIdSel.Text
                    comando.ExecuteNonQuery()
                    'Después borramos los puntos
                    For i As Integer = 0 To dst.Tables("TablaPuntosSistema").Rows.Count - 1
                        comando.CommandText = "Delete [SICA_SIST_PuntoSistema] where id=" & dst.Tables("TablaPuntosSistema").Rows(i).Item("ID_PuntoSistema")
                        comando.ExecuteNonQuery()
                    Next
                    'Y al final el sistema
                    comando.CommandText = "Delete [SICA_SIST_Sistemas] where id=" & lblIdSel.Text
                    comando.ExecuteNonQuery()
                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el sistema")
                End Try
                Me.pnlCauces.Visible = True
                pnlEDCauces.Visible = False
                pnlBotones.Visible = False
                lblIdSel.Text = ""
                crearDataSets()
        End Select
    End Sub
    Protected Sub cargarControlesEdicion()
        If dst.Tables("TablaAgrupaciones").Rows.Count > 0 Then
            Me.txtDescripcion.Text = utiles.nullABlanco(dst.Tables("TablaAgrupaciones").Rows(0).Item("Descripcion"))
            chbPublico.Checked = utiles.nullATrue(dst.Tables("TablaAgrupaciones").Rows(0).Item("Publico"))
            Me.txtNumInscripcion.Text = utiles.nullABlanco(dst.Tables("TablaAgrupaciones").Rows(0).Item("NumInscripcion"))
            Me.txtNombreAgrup.Text = utiles.nullABlanco(dst.Tables("TablaAgrupaciones").Rows(0).Item("Nombre"))

            If dst.Tables("TablaAgrupaciones").Rows(0).Item("NumInscripcion") Is System.DBNull.Value Then
                pnlNombreAgrup.Visible = True
                pnlIDRaac.Visible = False
                rbEsRAAC.SelectedIndex = 1
            End If

            If dst.Tables("TablaAgrupaciones").Rows(0).Item("Nombre") Is System.DBNull.Value Then
                pnlNombreAgrup.Visible = False
                pnlIDRaac.Visible = True
                rbEsRAAC.SelectedIndex = 0
            End If
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
        If txtFDescripcion.Text <> "" Then
            strFiltro = strFiltro + " AND Descripcion like '" + txtFDescripcion.Text + "' "
        End If

        If ddlFPublico.SelectedValue = 0 Then
            strFiltro = strFiltro + " AND Publico=0 "
        ElseIf ddlFPublico.SelectedValue = 1 Then
            strFiltro = strFiltro + " AND Publico=1 "
        End If

        Session("FiltroSistema") = strFiltro
        crearDataSets()
    End Sub

    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        crearDataSets()
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'Para cuando es nuevo no, pq aún no tendremos el identificador del sistema
        If lblIdSel.Text <> "" Then
            'iframeBuscar.Attributes.Add("src", "../SICAH/caucepuntBuscar.aspx?nodobusqueda=n&valor=0")
            ClientScript.RegisterStartupScript(Page.GetType, "ActivarArbol", "<script language='javascript'>document.getElementById('ContenedorArbol').style.display='block';document.getElementById('grid_agrupaciones').style.display='block';</script>")
        Else
            ClientScript.RegisterStartupScript(Page.GetType, "ActivarArbol", "<script language='javascript'>document.getElementById('ContenedorArbol').style.display='none';document.getElementById('grid_agrupaciones').style.display='none';</script>")
        End If
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

    Protected Sub FireOnSelectedIndexChangd()
        If rbEsRAAC.SelectedValue = "S" Then
            pnlIDRaac.Visible = True
            pnlNombreAgrup.Visible = False
        Else
            pnlIDRaac.Visible = False
            pnlNombreAgrup.Visible = True
        End If
    End Sub
End Class
