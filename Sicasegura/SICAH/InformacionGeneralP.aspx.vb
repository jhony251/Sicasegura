Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles

Partial Class SICAH_InformacionGeneralP
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstPuntos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0
    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ucPaginacion.ruta = "../"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True

        'ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        'ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")

        If Not IsPostBack Then
            Dim SICA_Log As SicaSegura.SICA_Log = New SicaSegura.SICA_Log()
            SICA_Log.Set_APP("SICASEGURA")
            SICA_Log.Set_USER(Session("loginUsuario"))
            SICA_Log.Set_PROFILE(Session("perfilCHS"))
            SICA_Log.Set_ESPAGINA("1")
            SICA_Log.Set_ESINFORME("0")
            SICA_Log.Set_DISPOSITIVO(Request.Browser.Platform.ToString())
            SICA_Log.acceso_pagina("Inf_Gen_Punto#" & Session("ClaveNodo"))
            'If Session("usuarioReg") = Nothing Then
            '    Response.Redirect("UsuarioNoRegistrado.aspx")
            '    Exit Sub
            'End If
            'If utiles.nullACero(Session("EnlaceP")) = 0 Then
            Session("EnlaceP") = 1
            'Session("EnlaceC") = 0
            'Session("EnlaceE") = 2
            'End If
            lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(Session("EnlaceP"), "../", Page, 11, "P", Session("ClaveNodo"), "N")
            crearDataSets()
            CargarValores()
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
        'sentenciaSel = " SELECT     CodigoCauce,CodigoPVYCR, DenominacionPunto, TipoSensor, Acceso, X_UTM, Y_UTM, PorcentajeRegable, B, B2, H, Ti, Td, Diametro_mm, " & _
        '               " FactorFlotador, TipoPunto, CodigoDataLogger, UsadoEnParteOficial, PKS, PKA, RIO, Longitud, LongitudFlotador, A1_M, A2_M, B1_M, B2_M, B3_M,  " & _
        '               " H1_M, H2_M, Offset_M, Antiguo, Observaciones " & _
        '               " FROM PVYCR_Puntos where CodigoPVYCR='" & Session("ClaveNodo") & "'"

        'IPIM 26/05/2009
        sentenciaSel = " SELECT     CodigoCauce,CodigoPVYCR, DenominacionPunto, TipoSensor, Acceso, X_UTM, Y_UTM, PorcentajeRegable, Diametro_mm, " & _
                       " FactorFlotador, TipoPunto, CodigoDataLogger, UsadoEnParteOficial, PKS, PKA, RIO, Longitud, LongitudFlotador, A1_M, A2_M, A3_M,B1_M, B2_M, B3_M,  " & _
                       " B4_M, H12_M, H23_M, H34_M, Offset_M, Antiguo, Observaciones " & _
                       " FROM PVYCR_Puntos where CodigoPVYCR='" & Session("ClaveNodo") & "'"


        daPuntos.SelectCommand.CommandText = sentenciaSel
        daPuntos.Fill(dstPuntos, "TablaPuntos")
        'rptPuntos.DataSource = dstPuntos.Tables("TablaPuntos")
        'rptPuntos.DataBind()

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
        If dstPuntos.Tables("TablaPuntos").Rows.Count > 0 Then
            ''Datos del Punto
            Me.txtcodigoPunto.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("CodigoPVYCR"))
            Me.txtcodigocauce.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("CodigoCauce"))
            Me.txtdenominacion.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("DenominacionPunto"))
            Me.txtobservaciones.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Observaciones"))
            Me.txtObservacionesGrafico.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Observaciones"))
            Me.txtsensor.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("TipoSensor"))
            Me.txtacceso.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Acceso"))
            Me.txtAccesoGrafico.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Acceso"))
            Me.txtX.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("X_UTM"))
            Me.txtY.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Y_UTM"))


            Me.txtRegable.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("PorcentajeRegable"))

            Me.txtDiametro.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Diametro_mm"))
            Me.txtFactor.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("FactorFlotador"))
            If utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("TipoPunto")) = "" Then
                Me.ddlTipoPunto.SelectedValue = "P"
            Else
                Me.ddlTipoPunto.SelectedValue = dstPuntos.Tables("TablaPuntos").Rows(0).Item("TipoPunto")
            End If

            Me.txtDatalogger.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("CodigoDataLogger"))
            If (dstPuntos.Tables("TablaPuntos").Rows(0).Item("UsadoEnParteOficial") = "1") Then
                Me.chkUso.Checked = True
            Else
                Me.chkUso.Checked = False
            End If

            Me.txtPKS.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("PKS"))
            Me.txtPKA.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("PKA"))
            Me.txtRIO.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("RIO"))
            Me.txtLongitud.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Longitud"))
            Me.txtFlotador.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("LongitudFlotador"))


            Me.txtA1_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("A1_M"))
            Me.txtA2_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("A2_M"))
            Me.txtA3_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("A3_M"))
            Me.txtB1_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("B1_M"))
            Me.txtB2_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("B2_M"))
            Me.txtB3_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("B3_M"))
            Me.txtB4_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("B4_M"))
            Me.txtH12_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("H12_M"))
            Me.txtH23_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("H23_M"))
            Me.txtH34_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("H34_M"))
            Me.txtOffset.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Offset_M"))
            Session.Item("codigoPVYCR") = txtcodigoPunto.Text
            VisualizarPanelGrafico(ddlTipoPunto.SelectedValue)
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
            Case Else
                NombreElemento = ""
        End Select
        txtDescripcionElementoMedida.Text = NombreElemento
    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim nombreinforme = "../listados/InformePuntos.aspx?nodoClave=" + utiles.BlancoANull(Me.txtcodigoPunto.Text)
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub
    Protected Sub VisualizarPanelGrafico(ByVal tipopunto As String)
        If tipopunto = "G" Then
            Me.pnlVerGraficoAcequias.Visible = True
            Me.pnlNOVerGraficoAcequias.Visible = False
        Else
            Me.pnlVerGraficoAcequias.Visible = False
            Me.pnlNOVerGraficoAcequias.Visible = True
        End If
    End Sub
End Class
