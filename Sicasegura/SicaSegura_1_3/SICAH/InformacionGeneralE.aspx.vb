Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Partial Class SICAH_InformacionGeneralE
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()

    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0
    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ucPaginacion.ruta = "../"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            '            lblCabecera.Text = genHTML.cabecera(Page)
            'If utiles.nullACero(Session("EnlaceE")) = 0 Then
            'Session("EnlaceC") = 0
            'Session("EnlaceP") = 1
            Session("EnlaceE") = 2
            'End If
            lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(Session("EnlaceE"), "../", Page, 11, "E", Session("ClaveNodo"), "N")
            crearDataSets()
            CargarValores()
            CargarValoresBusqueda()
        End If
    End Sub

    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Private Sub crearDataSets()
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = "SELECT * FROM dbo.PVYCR_ElementosMedida where CodigoPVYCR = '" & Session("ClaveNodo") & "' and idelementoMedida = '" & Session("idElemento") & "' "

        daElementos.SelectCommand.CommandText = sentenciaSel
        daElementos.Fill(dstElementos, "TablaElementos")

        'rptElementos.DataSource = dstElementos.Tables("TablaElementos")
        'rptElementos.DataBind()

    End Sub


    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function
    Protected Sub CargarValores()
        If dstElementos.Tables("TablaElementos").Rows.Count > 0 Then
            'Datos del elemento
            '            ddlcodigoPVYCR.Items.Clear()
            '           ddlcodigoPVYCR.Items.Add(New System.Web.UI.WebControls.ListItem(dstElementos.Tables("TablaElementos").Rows(0).Item("CodigoPVYCR").ToString))
            Me.txtEdCodigoPVYCR.Text = dstElementos.Tables("TablaElementos").Rows(0).Item("CodigoPVYCR").ToString
            Me.txtIdElemento.Text = utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("IdElementoMedida"))
            '          Me.ddltipo.Text = utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo"))
            If utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo")) = "Q" Then
                Me.txtTipo.Text = "Caudal"
            ElseIf utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo")) = "H" Then
                Me.txtTipo.Text = "Horas"
            ElseIf utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo")) = "E" Then
                Me.txtTipo.Text = "Energía"
            ElseIf utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo")) = "V" Then
                Me.txtTipo.Text = "Volumen"
            ElseIf utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo")) = "D" Then
                Me.txtTipo.Text = "Diferenciales"
            End If
        End If
    End Sub
    Private Sub CargarValoresBusqueda()
        Dim NombreElemento, TipoElementoMedida As String
        'NCM 05/09/2008 Cargamos la búsqueda con el código del arbol

        txtCodigoPVYCR.Text = Session("ClaveNodo")

        txtIdElementoMedida.Text = Session("idElemento")

        Select Case TipoElementoMedida
            Case "Q"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - CAUDAL"
            Case "V"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - VOLUMEN"
            Case "E"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - ENERGIA"
            Case "H"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - HORAS"
            Case "D"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - DIFERENCIAL"
            Case Else
                NombreElemento = ""
        End Select
        txtDescripcionElementoMedida.Text = NombreElemento
    End Sub
End Class
