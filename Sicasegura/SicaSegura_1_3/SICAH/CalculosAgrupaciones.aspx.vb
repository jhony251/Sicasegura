Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports NineRays.Reporting.DOM
Imports NineRays.Reporting.Export.PDF
Imports NineRays.Reporting.Export.RTF
Imports NineRays.Reporting.Export.Html
Imports NineRays.Reporting.Export.Excel
Imports NineRays.Reporting.Export.CSV

Partial Class SICAH_CalculosAgrupaciones
    Inherits System.Web.UI.Page

    'Variables para establecer la Conexion a Datos
    Dim Conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daConexion As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", Conexion)
    Dim dstCalculos As DataSet = New System.Data.DataSet()
    Dim dstSistemas As DataSet = New System.Data.DataSet()
    Dim dstArbol As DataSet = New System.Data.DataSet()


    'Variable para maquetación de estilos en función de filas pares/impares de un Repeater
    Dim parfila As Integer = 0

    'variables para listados NCM
    Dim dstlecturas(3) As DataSet
    Dim dw As DataView = New System.Data.DataView
    Dim dtLecturasOrdenadas(3) As DataTable
    Dim dtLecturasOrdenadasClone(3) As DataTable
    Dim TablaListados As DataTable = New System.Data.DataTable
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", Conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim strScript As String

        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")

        'scripts de cliente 
        imgCalFechaInicio.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaInicio.ClientID & "'),'dd/mm/yyyy');")


        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            lblCabecera.Text = genHTML.cabecera(Page)
            lblPestanyas.Text = genHTML.pestanyasMenu(5, "../", Page, Session("idperfil"), Session("usuarioReg"))

            'Carga del Arbol de Sistemas
            crearDataSets()
            'crearArbolRecursivoCauces()
            crearArbolRecursivoSistemas(Me.tvwSistemas, dstArbol)

        End If

    End Sub
    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function

    Private Sub crearArbolRecursivoCauces()
        Dim i As Integer
        For i = 0 To dstArbol.Tables("tablaCaucesPadres").Rows.Count - 1
            insertarNodosCauces(Me.tvwSistemas, dstArbol, dstArbol.Tables("tablaCaucesPadres").Rows(i).Item("caucepadre"), Nothing, Nothing, -1, True)
        Next
    End Sub


    Private Sub crearDataSets()
        dstArbol.Clear()
        'crearDataSetsCauces()
        crearDataSetsSistemas()
        'crearDataSetsCalculos()
    End Sub

    Private Sub crearDataSetsSistemas()

        'SISTEMAS
        daConexion.SelectCommand.CommandText = "Select * from dbo.PVYCR_Sistemas"
        daConexion.Fill(dstArbol, "tablaSistemasPadre")

        'Creamos selfrelation entre ambos tables
        If dstArbol.Relations.Contains("SelfRefenceRelation") = False Then
            dstArbol.Relations.Add("SelfRefenceRelation", _
                dstArbol.Tables("tablaSistemasPadre").Columns("IdSistema"), _
                dstArbol.Tables("tablaSistemasPadre").Columns("IdSistemaPadre"))
        End If

        'CAUCES-SISTEMAS
        daConexion.SelectCommand.CommandText = "SELECT     dbo.PVYCR_Sistemas.idSistema, dbo.PVYCR_Sistemas.idSistemaPadre, dbo.PVYCR_Sistemas.Nombre, dbo.PVYCR_Calculo_Sistemas.FechaInicio, " & _
                      "dbo.PVYCR_Calculo_Sistemas.FechaFin, dbo.PVYCR_Calculo_Sistemas.idCalculo, dbo.PVYCR_Calculos_ElementosMedida.CodigoPVYCR, " & _
                      "dbo.PVYCR_Calculos_ElementosMedida.idElementoMedida, dbo.PVYCR_Calculos_ElementosMedida.idCalculo AS Expr1, " & _
                      "dbo.PVYCR_Calculos_ElementosMedida.FechaInicio AS Expr2, dbo.PVYCR_Calculos_ElementosMedida.FechaFin AS Expr3, " & _
                      "dbo.PVYCR_Calculos_ElementosMedida.Factor, dbo.PVYCR_Calculos_ElementosMedida.Cod_Fuente_Dato " & _
                      "FROM dbo.PVYCR_Sistemas LEFT OUTER JOIN " & _
                      "dbo.PVYCR_Calculo_Sistemas ON dbo.PVYCR_Sistemas.idSistema = dbo.PVYCR_Calculo_Sistemas.idSistema LEFT OUTER JOIN " & _
                      "dbo.PVYCR_Calculos_ElementosMedida ON dbo.PVYCR_Calculo_Sistemas.idCalculo = dbo.PVYCR_Calculos_ElementosMedida.idCalculo "
        daConexion.Fill(dstArbol, "tablaSistemasPadreConCauce")


    End Sub
    Sub AgregarNodosCalculos(ByVal nodo As TreeNode)
        Dim filas() As DataRow
        Dim i As Integer

        filas = dstSistemas.Tables("Calculos").Select("IdSistema=" & nodo.Value)
        For i = 0 To filas.Length - 1
            If Not (filas(i)("Formula") Is System.DBNull.Value) Then
                Dim tn As New TreeNode
                tn.Text = filas(i)("Formula")
                tn.Value = filas(i)("IdCalculo") & "#" & filas(i)("IdSistema")
                tn.ImageUrl = "~/SICAH/IMAGES/Calculos.GIF"
                tn.Expand()
                nodo.ChildNodes.Add(tn)
            End If
        Next i

    End Sub
    Sub crearDataSetsCalculos()
        Dim sentenciaSel As String
        'Construcción de la sentencia SELECT para Filtrar Sistemas según IdSistema y Filtros de Fecha Seleccionados

        sentenciaSel = "SELECT DesCalculo, PVYCR_Calculos.IdCalculo, " & _
                       "LTRIM(dbo.PVYCR_GetStrCalculo(PVYCR_Calculos.IdCalculo)) as Formula," & _
                       "IdSistema " & _
                       "FROM PVYCR_Calculos INNER JOIN PVYCR_Calculo_Sistemas " & _
                       "ON PVYCR_Calculo_Sistemas.IdCalculo = PVYCR_Calculos.IdCalculo "


        daConexion.SelectCommand.CommandText = sentenciaSel
        daConexion.Fill(dstSistemas, "Calculos")

    End Sub
    Private Sub crearDataSetsCalculos(ByVal IdSistema As Integer)
        Dim sentenciaSel As String
        'Construcción de la sentencia SELECT para Filtrar Sistemas según IdSistema y Filtros de Fecha Seleccionados

        sentenciaSel = "SELECT DesCalculo, PVYCR_Calculos.IdCalculo, " & _
                       "LTRIM(dbo.PVYCR_GetStrCalculo(PVYCR_Calculos.IdCalculo)) as Formula," & _
                       "IdSistema " & _
                       "FROM PVYCR_Calculos INNER JOIN PVYCR_Calculo_Sistemas " & _
                       "ON PVYCR_Calculo_Sistemas.IdCalculo = PVYCR_Calculos.IdCalculo " & _
                       "WHERE(IdSistema = " & IdSistema & ")"

        daConexion.SelectCommand.CommandText = sentenciaSel
        daConexion.Fill(dstSistemas, "Calculos")
        rpt_calculos.DataSource = dstSistemas.Tables("Calculos")
        rpt_calculos.DataBind()

        pnlCalculos.Visible = True
        btnAgregarCalculo.Visible = VisibleSegunPerfil()

        If dstSistemas.Tables("Calculos").Rows.Count = 0 Then
            'pnlCalculos.Visible = False
            lblinfosistema2.Text = "No Existen Calculos Definidos en la Agrupación: " & CType(Session("NodoSistemas"), TreeNode).Text
            rpt_calculos.Visible = False
        Else
            'pnlCalculos.Visible = True
            lblinfosistema2.Text = "Cálculos de la Agrupación: " & CType(Session("NodoSistemas"), TreeNode).Text
            rpt_calculos.Visible = True
        End If


    End Sub

    Private Sub crearDataSetsCalculosElementosdeMedida(ByVal IdCalculo As Integer)

        'Construccion de las sentencias SELECT para mostrar los Calculos-Elementos de Medida
        Dim sentenciaSel As String
        'Construcción de la sentencia SELECT para Filtrar Sistemas según IdSistema y Filtros de Fecha Seleccionados


        sentenciaSel = "SELECT PVYCR_Calculos_ElementosMedida.Factor, PVYCR_Calculos_ElementosMedida.CodigoPVYCR, " & _
                     " PVYCR_ElementosMedida.DenominacionPunto, PVYCR_Calculos_ElementosMedida.idCalculo " & _
                     "FROM PVYCR_ElementosMedida INNER JOIN " & _
                     "PVYCR_Calculos_ElementosMedida ON " & _
                     "PVYCR_ElementosMedida.CodigoPVYCR = PVYCR_Calculos_ElementosMedida.CodigoPVYCR " & _
                     "WHERE IdCalculo=" & IdCalculo

        daConexion.SelectCommand.CommandText = sentenciaSel
        daConexion.Fill(dstSistemas, "CalculosElementosdeMedida")

    End Sub

    Protected Sub btnFiltrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltrar.Click

        crearDataSetsCalculos(CType(Session("NodoSistemas"), TreeNode).Value)

    End Sub

    Protected Sub tvwSistemas_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwSistemas.SelectedNodeChanged
        If InStr(tvwSistemas.SelectedNode.Value, "#") Then
            'El nodo es una formula

        Else
            'El nodo es un sistema
            Session("NodoSistemas") = tvwSistemas.SelectedNode
            Session("IdSistema") = tvwSistemas.SelectedNode.Value
            'txtSistema.Text = tvwSistemas.SelectedNode.Text
            lblinfosistema2.Text = "Cálculos del sistema: " & tvwSistemas.SelectedNode.Text
            crearDataSetsCalculos(CType(Session("NodoSistemas"), TreeNode).Value)
        End If

    End Sub
    Protected Sub imgBuscarSistema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscarSistema.Click
        Dim rutanodobuscadodescripcion As String
        Dim strScript As String
        Dim rutanodobuscado As String
        Dim nodobuscado As TreeNode

        'Búsqueda del nodo con el texto coincidente con la clave
        rutanodobuscadodescripcion = ""
        If pnlArbolSistemas.Visible = True Then
            rutanodobuscado = BuscarNodoTVW(tvwSistemas, txtSistema.Text, rutanodobuscadodescripcion)
        Else
            rutanodobuscado = BuscarNodoTVW(tvwSistemas, txtSistema.Text, rutanodobuscadodescripcion)
        End If

        If rutanodobuscado = "" Then
            Alert(Page, "No Existen nodos con los criterios seleccionados")
            SetFocusToControl(Me, Me.txtSistema)
            txtSistema.Text = ""

        Else
            'Representación del Nodo si la busqueda es positiva
            If pnlArbolSistemas.Visible = True Then
                nodobuscado = tvwSistemas.FindNode(rutanodobuscado)
            Else
                nodobuscado = tvwSistemas.FindNode(rutanodobuscado)
            End If

            If pnlArbolSistemas.Visible = True Then
                tvwSistemas.CollapseAll()
            Else
                tvwSistemas.CollapseAll()
            End If

            ExpandirNodoRecursivo(nodobuscado)
            nodobuscado.Selected = True
            nodobuscado.Select()
        End If
    End Sub



    Protected Sub rpt_calculos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rpt_calculos.ItemCommand
        Dim comando As SqlCommand = New SqlCommand("", Conexion)
        Dim parametros As Array
        'Obtenemos los valores de los parámetros spliteando e.CommandArgument
        parametros = Split(e.CommandArgument, "#")

        Select Case e.CommandName
            Case "editar"
                'Saltamos a la pantalla de mantenimiento de calculos
                Response.Redirect("Calculos.aspx?IdCalculo=" & parametros(0))

            Case "borrar"
                'Borrarmos los registros relacionados
                comando.CommandText = "delete from PVYCR_Calculo_Sistemas where IdSistema=" & parametros(0).ToString & " AND IdCalculo=" & parametros(1).ToString & ";"

                If Conexion.State = ConnectionState.Closed Then Conexion.Open()
                comando.ExecuteNonQuery()
                'Refrescamos el grid de cálculos
                crearDataSetsCalculos(CType(Session("NodoSistemas"), TreeNode).Value)
        End Select
    End Sub

    Protected Sub btnAgregarCalculo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarCalculo.Click

        pnlAgregarCalculos.Visible = True

    End Sub

    Protected Sub ddlTipoCalculo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoCalculo.SelectedIndexChanged

        daConexion.SelectCommand.CommandText = "SELECT     dbo.PVYCR_Calculos.DesCalculo + ': ' + LTRIM(dbo.PVYCR_GetStrCalculoCombo(dbo.PVYCR_Calculos.idCalculo)) AS Formula, " & _
                                               "dbo.PVYCR_ElementosMedida.Tipo, dbo.PVYCR_Calculos.idCalculo, dbo.PVYCR_Calculos.DesCalculo " & _
                                               "FROM         dbo.PVYCR_Calculos_ElementosMedida AS PVYCR_Calculos_ElementosMedida_1 INNER JOIN " & _
                                               "dbo.PVYCR_ElementosMedida ON PVYCR_Calculos_ElementosMedida_1.CodigoPVYCR = dbo.PVYCR_ElementosMedida.CodigoPVYCR AND " & _
                                               "PVYCR_Calculos_ElementosMedida_1.idElementoMedida = dbo.PVYCR_ElementosMedida.idElementoMedida INNER JOIN PVYCR_Calculos ON PVYCR_Calculos_ElementosMedida_1.idCalculo = PVYCR_Calculos.idCalculo WHERE Tipo = '" & ddlTipoCalculo.SelectedItem.Value & "'"

        daConexion.Fill(dstSistemas, "PVYCR_Calculos")
        ddlCalculos.Items.Clear()
        ddlCalculos.DataSource = dstSistemas.Tables("PVYCR_Calculos")
        ddlCalculos.DataValueField = "IdCalculo"
        ddlCalculos.DataTextField = "Formula"
        ddlCalculos.DataBind()
        ddlCalculos.Enabled = True
    End Sub

    Protected Sub btnguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardar.Click
        Dim MiComando As SqlCommand = New SqlCommand("", Conexion)
        MiComando.Parameters.Clear()
        If Conexion.State = ConnectionState.Closed Then Conexion.Open()

        Try
            MiComando.CommandText = "insert into PVYCR_Calculo_Sistemas(IdSistema, IdCalculo) values(@IdSistema,@IdCalculo)"
            MiComando.Parameters.AddWithValue("@IdCalculo", utiles.BlancoANull(ddlCalculos.SelectedValue))
            MiComando.Parameters.AddWithValue("@IdSistema", Session("IdSistema"))
            MiComando.ExecuteNonQuery()

            'Refrescamos el grid de cálculos
            crearDataSetsCalculos(Session("IdSistema"))

        Catch err As System.Data.SqlClient.SqlException
            If err.Number = 2627 Then
                Alert(Page, "El cálculo seleccionado ya forma parte del sistema")
            Else
                Alert(Page, "Excepción SQL consulte con el administrador del Servidor")
            End If
        End Try
    End Sub

    Protected Function obtenerCodigoPVYCR() As String
        Dim codigoPVYCR As String = tvwSistemas.SelectedValue.Substring(0, tvwSistemas.SelectedValue.Length - 4)
        Return codigoPVYCR
    End Function

    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080617
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function
    Protected Function obtenerPagina() As String
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return "block;"
        Else
            Return "none;"
        End If
    End Function


End Class
