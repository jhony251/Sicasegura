Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.IO
Partial Class SICAH_Log
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim daLog As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstLog As DataSet = New System.Data.DataSet()
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        imgCalFechaCon.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFConexion.ClientID & "'),'dd/mm/yyyy');")
        imgCalFechaDes.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFDesconexion.ClientID & "'),'dd/mm/yyyy');")
        lblCabecera.Text = genHTML.cabecera(Page)
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If

            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))
            crearDataSets()
     
            'ucPaginacion.DataBind()
            'ucPaginacion.lblNumpaginasDatabind()
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
        If txtRegistros.Text = "" Then
            sentenciaSel = " SELECT "
        Else
            sentenciaSel = " SELECT TOP " & txtRegistros.Text
        End If

        sentenciaSel = sentenciaSel & " login, FechaConexion, FechaDesconexion FROM Tlog_PVY "
   
        If Session("FiltroLog") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroLog")
            Else
                sFiltro = sFiltro + Session("FiltroLog")
            End If
        End If


        sentenciaOrder = " order by Login, fechaconexion "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If

        daLog.SelectCommand.CommandText = sentenciaSel
        'daCauces.Fill(dstCauces, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaCauces")
        daLog.Fill(dstLog, "TablaLog")
        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daLog.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        'ucPaginacion.calcularPags(txtComando)
        utiles.CerrarConexion(conexion)
        rptLog.DataSource = dstLog.Tables("TablaLog")
        rptLog.DataBind()

    End Sub
    Protected Sub AceptarFiltro(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081002
        Dim strFiltro As String
        strFiltro = ""
        If txtFLogin.Text <> "" Then
            strFiltro = " AND login like '" + txtFLogin.Text + "' "
        End If

        If txtFFConexion.Text <> "" Then
            strFiltro = strFiltro + " AND FechaConexion between '" + txtFFConexion.Text + " 00:00:00' and '" + txtFFConexion.Text + " 23:59:59'"
        End If

        If txtFFDesconexion.Text <> "" Then
            strFiltro = strFiltro + " AND fechadesconexion between '" + txtFFDesconexion.Text + " 00:00:00' and '" + txtFFDesconexion.Text + " 23:59:59'"
        End If

        Session("FiltroLog") = strFiltro
        crearDataSets()
    End Sub
    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        crearDataSets()
    End Sub
End Class
