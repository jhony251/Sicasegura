Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.SICA_GestionArboles

Partial Class SICAH_sistemas_old
    Inherits System.Web.UI.Page
    'Variables para establecer la Conexion a Datos
    Dim Conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daConexion As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCalculos As DataSet = New System.Data.DataSet()
    Dim dstSistemas As DataSet = New System.Data.DataSet()
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim parfila As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        imgNuevoRaiz.DataBind()
        imgNuevo.DataBind()

        IncrustarLoadEvent()
        Page.MaintainScrollPositionOnPostBack = True

        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            lblCabecera.Text = genHTML.cabecera(Page)
            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))
            imgBorrar.Attributes.Add("onclick", "return confirmar_borrado();")
            pnlSistemas.Visible = True
            Select Case Request.QueryString("modo")
                Case "raiz"
                    pnlEDSistemas.Visible = True
                    pnlSistemas.Visible = False
                    lblTitulo.Text = "<b>NUEVO SISTEMA RAIZ</b>"
                Case "hijo"
                    pnlEDSistemas.Visible = True
                    pnlSistemas.Visible = False
                    lblTitulo.Text = "<b>NUEVO SISTEMA HIJO DE " & Request.QueryString("padre").ToString.ToUpper & "</b>"
                Case "edicion"
                    pnlEDSistemas.Visible = True
                    pnlSistemas.Visible = False
                    txtNombre.Text = DescripcionSistemaSinFormato(CInt(Session("IdSistemaSel")))
                    lblTitulo.Text = "<b>EDICIÓN DE SISTEMA"
            End Select

            crearDataSets()

        End If

    End Sub
    Public Sub IncrustarLoadEvent()
        'Script de Cliente Necesario para mantener el scroll del panel contendor del arbol de sistemas.
        Dim script As String = _
          "function LoadEvent()" + _
          "{{" + _
          " try" + _
          " {{" + _
          "   var elem = document.getElementById('{0}_SelectedNode');" + _
          "   if(elem != null )" + _
          "   {{" + _
          "     var node = document.getElementById(elem.value);" + _
          "     if(node != null)" + _
          "     {{" + _
          "       node.scrollIntoView(true);" + _
          "       {1}.scrollLeft = 0;" + _
          "     }}" + _
          "   }}" + _
          " }}" + _
          " catch(oException)" + _
          " {{}}" + _
          "}}"

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "LoadEvent", _
          String.Format(script, tvwSistemas.ClientID, pnlArbol.ClientID), True)
    End Sub

    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function

    Private Sub crearDataSets()
        tvwSistemas.Nodes.Clear()
        crearDataSetsSistemas()
        crearArbolRecursivoSistemas(Me.tvwSistemas, dstarbol)
    End Sub

    Private Sub crearDataSetsSistemas()

        dstarbol.Clear()
        daConexion.SelectCommand.CommandText = "Select * from dbo.PVYCR_Sistemas"
        daConexion.Fill(dstarbol, "tablaSistemasPadre")

        'Creamos selfrelation entre ambos tables
        If dstarbol.Relations.Contains("SelfRefenceRelation") = False Then
            dstarbol.Relations.Add("SelfRefenceRelation", _
                dstarbol.Tables(0).Columns("idsistema"), _
                dstarbol.Tables(0).Columns("idsistemapadre"))
        End If

    End Sub


    Protected Sub tvwSistemas_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwSistemas.SelectedNodeChanged
        pnlEDSistemas.Visible = False
        lblSistemaSel.Text = tvwSistemas.SelectedNode.Value
        Session("IdSistemaSel") = tvwSistemas.SelectedNode.Value
        'lblinfo.Text = "Acciones sobre sistema: " & tvwSistemas.SelectedNode.Text

    End Sub

    Protected Sub btnFiltroAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptar.Click
        Dim rutanodobuscadodescripcion As String

        'Búsqueda del nodo con el texto coincidente con la clave
        rutanodobuscadodescripcion = ""
        Dim rutanodobuscado = BuscarNodoTVW(tvwSistemas, txtFiltroNombreSistema.Text, rutanodobuscadodescripcion)

        If rutanodobuscado = "" Then
            Alert(Page, "No Existen nodos con los criterios seleccionados")
            SetFocusToControl(Me, Me.txtFiltroNombreSistema)
            txtFiltroNombreSistema.Text = ""

        Else
            'Representación del Nodo si la busqueda es positiva
            Dim nodobuscado As TreeNode = tvwSistemas.FindNode(rutanodobuscado)

            tvwSistemas.CollapseAll()
            ExpandirNodoRecursivo(nodobuscado)
            nodobuscado.Selected = True
            nodobuscado.Select()
            Session("IdSistemaSel") = nodobuscado.Value
        End If
    End Sub
    Protected Sub buscarNodo(ByVal txtTextoNodo)
        Dim rutanodobuscadodescripcion As String

        'Búsqueda del nodo con el texto coincidente con la clave
        rutanodobuscadodescripcion = ""
        Dim rutanodobuscado = BuscarNodoTVW(tvwSistemas, txtTextoNodo, rutanodobuscadodescripcion)

        If rutanodobuscado = "" Then
            'Alert(Page, "No Existen nodos con los criterios seleccionados")
            'SetFocusToControl(Me, Me.txtFiltroNombreSistema)
            'txtFiltroNombreSistema.Text = ""

        Else
            'Representación del Nodo si la busqueda es positiva
            Dim nodobuscado As TreeNode = tvwSistemas.FindNode(rutanodobuscado)

            tvwSistemas.CollapseAll()
            ExpandirNodoRecursivo(nodobuscado)
            nodobuscado.Selected = True
            nodobuscado.Select()
            Session("IdSistemaSel") = nodobuscado.Value
        End If
    End Sub

    Protected Sub imgEditar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgEditar.Click
        'If Not (IsNothing(tvwSistemas.SelectedNode)) Then
        If Session("IdSistemaSel") <> "" Then
            pnlEDSistemas.Visible = True
            pnlSistemas.Visible = False
            lblSistemaSel.Text = tvwSistemas.SelectedNode.Value
            Response.Redirect("Sistemas.aspx?modo=edicion")
        Else
            Alert(Page, "Seleccione un nodo para editar")
        End If

    End Sub
    Protected Function DescripcionSistemaSinFormato(ByVal idsistema As Integer) As String
        If Conexion.State = ConnectionState.Closed Then Conexion.Open()
        daConexion.SelectCommand.CommandText = "Select Nombre from dbo.PVYCR_Sistemas WHERE IdSistema='" & idsistema & "'"
        DescripcionSistemaSinFormato = CStr(daConexion.SelectCommand.ExecuteScalar())
    End Function

    Protected Function IdSistema(ByVal NombreSistema As Integer) As String
        If Conexion.State = ConnectionState.Closed Then Conexion.Open()
        daConexion.SelectCommand.CommandText = "Select IdSistema from dbo.PVYCR_Sistemas WHERE Nombre='" & NombreSistema & "'"
        IdSistema = CStr(daConexion.SelectCommand.ExecuteScalar())
    End Function

    Protected Sub imgBorrar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBorrar.Click
        'Borrado de sistemas

        Try
            'If lblSistemaSel.Text <> "" Then
            If Session("IdSistemaSel") <> "" Then

                crearDataSetsSistemas()
                BorrarNodosRecursivo(Session("IdSistemaSel"))

                lblSistemaSel.Text = ""
                pnlSistemas.Visible = True
                pnlEDSistemas.Visible = False

                crearDataSets()
                tvwSistemas.DataBind()

            Else
                Alert(Page, "Seleccione un nodo para eliminar")
            End If

        Catch err As System.Data.SqlClient.SqlException
            Select Case err.Number
                Case 2627
                    Alert(Page, "Existen Datos Asociados al Sistema seleccionado. ")
                Case 547
                    Alert(Page, "Existen Datos Asociados a este Sistema no es posible borrar el registro.")
                Case Else
                    Alert(Page, "Excepción SQL consulte con el administrador del Servidor.")
            End Select
        End Try
    End Sub
    Protected Sub BorrarNodosRecursivo(ByVal IdSistema As Integer)
        'Implementación de Borrado en cascada de los Sistemas
        Dim RegistrosSistemasHijos() As DataRow
        Dim fila As DataRow
        Dim strsql As String
        Dim comando As SqlCommand = New SqlCommand("", Conexion)

        RegistrosSistemasHijos = dstarbol.Tables("tablaSistemasPadre").Select("IdSistemaPadre = '" & IdSistema & "'")
        For Each fila In RegistrosSistemasHijos
            BorrarNodosRecursivo(fila("IdSistema"))
        Next
        strsql = "Delete from PVYCR_Sistemas WHERE IdSistema=" & IdSistema
        If Conexion.State = ConnectionState.Closed Then Conexion.Open()
        comando.CommandText = strsql
        comando.ExecuteNonQuery()
    End Sub



    Protected Sub lbtNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If IsNothing(tvwSistemas.SelectedNode) = True Then
            lblSistemaSel.Text = ""
            Response.Redirect("Sistemas.aspx?modo=raiz")
        Else
            lblSistemaSel.Text = tvwSistemas.SelectedNode.Value
            Response.Redirect("Sistemas.aspx?modo=hijo&idpadre=" & lblSistemaSel.Text & "&padre=" & DescripcionSistemaSinFormato(tvwSistemas.SelectedNode.Value))
        End If
        pnlSistemas.Visible = False
        pnlEDSistemas.Visible = True
    End Sub

    Protected Sub btnFiltroCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelar.Click
        txtFiltroNombreSistema.Text = "[Nombre Sistema]"
        crearDataSets()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        lblSistemaSel.Text = ""
        pnlSistemas.Visible = True
        pnlEDSistemas.Visible = False
        txtNombre.Text = ""
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim comando As SqlCommand = New SqlCommand("", Conexion)

        If Conexion.State = ConnectionState.Closed Then Conexion.Open()
        Select Case Request.QueryString("modo")
            Case "raiz"
                'Insertamos un nuevo sistema
                comando.CommandText = "INSERT INTO PVYCR_Sistemas([Nombre]) VALUES (@Nombre)"
            Case "hijo"
                comando.CommandText = "INSERT INTO PVYCR_Sistemas([Nombre],[IdSistemaPadre]) VALUES (@Nombre, " & Request.QueryString("idpadre") & ")"
            Case "edicion"
                comando.CommandText = "UPDATE PVYCR_Sistemas SET Nombre = @Nombre WHERE IdSistema=" & CInt(Session("IdSistemaSel")) 'lblSistemaSel.Text
        End Select

        comando.Parameters.Clear()
        comando.Parameters.AddWithValue("Nombre", utiles.BlancoANull(txtNombre.Text))

        comando.ExecuteNonQuery()

        lblSistemaSel.Text = ""
        pnlSistemas.Visible = True
        pnlEDSistemas.Visible = False


        crearDataSets()
        tvwSistemas.DataBind()
        'Seleccionar en el arbol el elemento recien creado
        buscarNodo(txtNombre.Text)


    End Sub

    Protected Sub imgNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNuevo.Click
        If IsNothing(tvwSistemas.SelectedNode) = True Then
            lblSistemaSel.Text = ""
            Response.Redirect("Sistemas.aspx?modo=raiz")
        Else
            lblSistemaSel.Text = tvwSistemas.SelectedNode.Value
            Response.Redirect("Sistemas.aspx?modo=hijo&idpadre=" & lblSistemaSel.Text & "&padre=" & DescripcionSistemaSinFormato(tvwSistemas.SelectedNode.Value))
        End If
        pnlSistemas.Visible = False
        pnlEDSistemas.Visible = True

    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If Session("IdSistemaSel") <> "" Then
            buscarNodo(DescripcionSistemaSinFormato(CInt(Session("IdSistemaSel"))))

        End If
    End Sub


    Protected Sub imgNuevoRaiz_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNuevoRaiz.Click

        lblSistemaSel.Text = ""
        Response.Redirect("Sistemas.aspx?modo=raiz")
        pnlSistemas.Visible = False
        pnlEDSistemas.Visible = True
    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080617
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function
End Class



























