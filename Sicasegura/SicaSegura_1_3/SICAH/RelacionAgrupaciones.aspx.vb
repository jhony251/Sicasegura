Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.SICA_GestionArboles
Partial Class SICAH_RelacionAgrupaciones
    Inherits System.Web.UI.Page
    'Variables para establecer la Conexion a Datos
    Dim Conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daConexion As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCalculos As DataSet = New System.Data.DataSet()
    Dim dstSistemas As DataSet = New System.Data.DataSet()
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim parfila As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
                    lblTitulo.Text = "<b>NUEVA AGRUPACIÓN RAIZ</b>"
                Case "hijo"
                    pnlEDSistemas.Visible = True
                    pnlSistemas.Visible = False
                    lblTitulo.Text = "<b>NUEVA AGRUPACIÓN HIJA DE " & Request.QueryString("padre").ToString.ToUpper & "</b>"
                Case "edicion"
                    pnlEDSistemas.Visible = True
                    pnlSistemas.Visible = False
                    txtNombre.Text = DescripcionAgruSinFormato(CInt(Session("IdAgruPadreSel")), CInt(Session("IdAgruHijaSel")))
                    lblTitulo.Text = "<b>EDICIÓN DE AGRUPACIÓN"
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
        daConexion.SelectCommand.CommandText = "Select * from dbo.PVYCR_RelacionAgrupaciones"
        daConexion.Fill(dstarbol, "tablaSistemasPadre")

        'Creamos selfrelation entre ambos tables
        If dstarbol.Relations.Contains("SelfRefenceRelation") = False Then
            dstarbol.Relations.Add("SelfRefenceRelation", _
                dstarbol.Tables(0).Columns("idAgrupacionhija"), _
                dstarbol.Tables(0).Columns("idagrupacionPadre"))
        End If

    End Sub


    Protected Sub tvwSistemas_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwSistemas.SelectedNodeChanged
        pnlEDSistemas.Visible = False
        lblSistemaSel.Text = tvwSistemas.SelectedNode.Value
        Session("IdAgruPadreSel") = tvwSistemas.SelectedNode.Value.Substring(0, tvwSistemas.SelectedNode.Value.IndexOf("&") - 1)
        Session("IdAgruHijaSel") = tvwSistemas.SelectedNode.Value.Substring(tvwSistemas.SelectedNode.Value.IndexOf("&") + 1, tvwSistemas.SelectedNode.Value.Length - tvwSistemas.SelectedNode.Value.IndexOf("&"))
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
            Session("IdAgruPadreSel") = nodobuscado.Value
            Session("IdAgruHijaSel") = nodobuscado.Value
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
            Session("IdAgruPadreSel") = nodobuscado.Value
            Session("IdAgruHijaSel") = nodobuscado.Value
        End If
    End Sub

    Protected Sub imgEditar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgEditar.Click
        'If Not (IsNothing(tvwSistemas.SelectedNode)) Then
        If Session("IdAgrupadreSel") <> "" And Session("IdAgruHijaSel") <> "" Then
            pnlEDSistemas.Visible = True
            pnlSistemas.Visible = False
            lblSistemaSel.Text = tvwSistemas.SelectedNode.Value
            Response.Redirect("RelacionAgrupaciones.aspx?modo=edicion")
        Else
            Alert(Page, "Seleccione un nodo para editar")
        End If

    End Sub
    Protected Function DescripcionAgruSinFormato(ByVal idAgruPadre As Integer, ByVal idAgruHija As Integer) As String
        If Conexion.State = ConnectionState.Closed Then Conexion.Open()
        daConexion.SelectCommand.CommandText = "Select Descripcion from dbo.PVYCR_RelacionAgrupaciones WHERE IdAgrupacionPadre=" & idAgruPadre & " and idAgrupacionHija=" & idAgruHija
        DescripcionAgruSinFormato = CStr(daConexion.SelectCommand.ExecuteScalar())
    End Function

    Protected Function IdAgrupacionPadre(ByVal NombreSistema As Integer) As String
        If Conexion.State = ConnectionState.Closed Then Conexion.Open()
        daConexion.SelectCommand.CommandText = "Select IdAgrupacionPadre from dbo.PVYCR_RelacionAgrupaciones WHERE Nombre='" & NombreSistema & "'"
        IdAgrupacionPadre = CStr(daConexion.SelectCommand.ExecuteScalar())
    End Function
    Protected Function IdAgrupacionHija(ByVal NombreSistema As Integer) As String
        If Conexion.State = ConnectionState.Closed Then Conexion.Open()
        daConexion.SelectCommand.CommandText = "Select IdAgrupacionHija from dbo.PVYCR_RelacionAgrupaciones WHERE Nombre='" & NombreSistema & "'"
        IdAgrupacionHija = CStr(daConexion.SelectCommand.ExecuteScalar())
    End Function


    Protected Sub imgBorrar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBorrar.Click
        'Borrado de sistemas

        Try
            'If lblSistemaSel.Text <> "" Then
            If Session("IdAgruPadreSel") <> "" And Session("IdAgruHijaSel") <> "" Then

                crearDataSetsSistemas()
                BorrarNodosRecursivo(Session("IdAgruPadreSel"), Session("IdAgruHijaSel"))

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
                    Alert(Page, "Existen Datos Asociados a la Agrupación seleccionada. ")
                Case 547
                    Alert(Page, "Existen Datos Asociados a esta Agrupación no es posible borrar el registro.")
                Case Else
                    Alert(Page, "Excepción SQL consulte con el administrador del Servidor.")
            End Select
        End Try
    End Sub
    Protected Sub BorrarNodosRecursivo(ByVal IdAgrupacionPadre As Integer, ByVal IdAgrupacionHija As Integer)
        'Implementación de Borrado en cascada de los Sistemas
        Dim RegistrosSistemasHijos() As DataRow
        Dim fila As DataRow
        Dim strsql As String
        Dim comando As SqlCommand = New SqlCommand("", Conexion)

        RegistrosSistemasHijos = dstarbol.Tables("tablaSistemasPadre").Select("IdAgrupacionPadre = '" & IdAgrupacionPadre & "' and IdagrupacionHija='" & IdAgrupacionHija & "'")
        For Each fila In RegistrosSistemasHijos
            BorrarNodosRecursivo(fila("IdAgrupacionPadre"), fila("IdAgrupacionHija"))
        Next
        strsql = "Delete from PVYCR_RelacionAgrupaciones WHERE IdAgrupacionPadre=" & IdAgrupacionPadre & " and IdAgrupacionHija = '" & IdAgrupacionHija & "'"
        If Conexion.State = ConnectionState.Closed Then Conexion.Open()
        comando.CommandText = strsql
        comando.ExecuteNonQuery()
    End Sub



    Protected Sub lbtNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If IsNothing(tvwSistemas.SelectedNode) = True Then
            lblSistemaSel.Text = ""
            Response.Redirect("RelacionAgrupaciones.aspx?modo=raiz")
        Else
            lblSistemaSel.Text = tvwSistemas.SelectedNode.Value
            'Response.Redirect("RelacionAgrupaciones.aspx?modo=hijo&idpadre=" & lblSistemaSel.Text & "&padre=" & DescripcionAgruSinFormato(tvwSistemas.SelectedNode.Value))
            Response.Redirect("RelacionAgrupaciones.aspx?modo=hijo&idpadre=" & lblSistemaSel.Text & "&padre=" & DescripcionAgruSinFormato(CInt(Session("IdAgruPadresel")), CInt(Session("IdAgruHijaSel"))))
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
                comando.CommandText = "INSERT INTO PVYCR_Relacionagrupaciones(Descripcion) VALUES (@Descripcion)"
            Case "hijo"
                comando.CommandText = "INSERT INTO PVYCR_Relacionagrupaciones(Descripcion,IdAgrupacionPadre) VALUES (@Descripcion, " & Request.QueryString("idpadre") & ")"
            Case "edicion"
                comando.CommandText = "UPDATE PVYCR_Relacionagrupaciones SET Descripcion = @Descripcion WHERE IdAgrupacionPadre=" & CInt(Session("IdAgruPadreSel")) & "IdAgrupacionHija=" & CInt(Session("IdAgruHijaSel")) 'lblSistemaSel.Text
        End Select

        comando.Parameters.Clear()
        comando.Parameters.AddWithValue("Descripcion", utiles.BlancoANull(txtNombre.Text))

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
            Response.Redirect("RelacionAgrupaciones.aspx?modo=raiz")
        Else
            lblSistemaSel.Text = tvwSistemas.SelectedNode.Value
            Response.Redirect("RelacionAgrupaciones.aspx?modo=hijo&idpadre=" & lblSistemaSel.Text & "&padre=" & DescripcionAgruSinFormato(CInt(Session("IdagruPadreSel")), CInt(Session("IdAgruHijaSel"))))
        End If
        pnlSistemas.Visible = False
        pnlEDSistemas.Visible = True

    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If Session("IdAgruPadreSel") <> "" And Session("IdAgruHijaSel") <> "" Then
            buscarNodo(DescripcionAgruSinFormato(CInt(Session("IdAgruPadreSel")), CInt(Session("IdAgruHijaSel"))))
        End If
    End Sub


    Protected Sub imgNuevoRaiz_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgNuevoRaiz.Click

        lblSistemaSel.Text = ""
        Response.Redirect("RelacionAgrupaciones.aspx?modo=raiz")
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
