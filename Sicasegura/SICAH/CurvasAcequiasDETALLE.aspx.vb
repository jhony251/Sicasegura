Imports System.Data
Imports OpenFlashChart
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System

Partial Class SICAH_CurvasAcequiasDETALLE
    Inherits System.Web.UI.Page

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daCurvasAcequias As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCurvasAcequias As DataSet = New System.Data.DataSet()
    Dim objTrans As SqlClient.SqlTransaction
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim IdElementoMedida As String = ""
    Dim CodigoPVYCR As String = ""
    Dim Regimen As String = ""
    Dim Fecha_Inicio_Uso = ""

    Dim sentenciaSel, sentenciaOrder, sentenciaSelCount, sentenciawhere As String
    Dim adapter As New SqlClient.SqlDataAdapter("", conexion)
    Dim dstNombres As DataSet = New System.Data.DataSet()
    'Public numpaginas As Integer
    'Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")

    Dim parfila As Integer = 0
    'Dim numfila As Integer = 0
    Dim mivectorClaves() As String

    Protected Function getUrlGrafico() As String
        Dim v_codigo, v_idelemento As String
        Dim url As String
        If Me.lblcodigopvycrSel.Text <> "" Then
            v_codigo = Me.lblcodigopvycrSel.Text
            v_idElemento = Me.lblElementoSel.Text
        ElseIf CodigoPVYCR <> "" Then
            v_codigo = CodigoPVYCR
            v_idelemento = IdElementoMedida
        End If
        url = "CurvasAcequiasDETALLEData.aspx?CodigoPVYCR=" & v_codigo
        url += "&IdElementoMedida=" & v_idelemento

        ''EGB Inclusión de los parametros del grafico individual de curva de datos
        'If Len(nullABlanco(Session("Regimen"))) > 0 Then
        '    url += "&Regimen=" & Session("Regimen")
        'End If
        'If Len(nullABlanco(Session("Fecha_inicio_uso"))) > 0 Then
        '    url += "&Fecha_inicio_uso=" & Session("Fecha_inicio_uso")
        'End If


        Return Server.UrlEncode(url)

    End Function
    Protected Function getUrlGrafico2() As String
        Dim url As String
        url = "CurvasAcequiasDETALLEData2.aspx?CodigoPVYCR=" & Me.lblcodigopvycrSel.Text
        url += "&IdElementoMedida=" & Me.lblElementoSel.Text

        'EGB Inclusión de los parametros del grafico individual de curva de datos
        If Len(nullABlanco(Session("Regimen"))) > 0 Then
            url += "&Regimen=" & Session("Regimen")
        End If
        If Len(nullABlanco(Session("Fecha_inicio_uso"))) > 0 Then
            url += "&Fecha_inicio_uso=" & Session("Fecha_inicio_uso")
        End If

        Return Server.UrlEncode(url)

    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

        lbtNuevo.DataBind()

        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        imgFechaInicioUso.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & Me.txtFechaInicioUso.ClientID & "'),'dd/mm/yyyy');")
        imgFechaFinUso.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & Me.txtFechaFinUso.ClientID & "'),'dd/mm/yyyy');")

        '-----------------------------------------------------------------------------------
        'EGB 29/09/2008
        If nullABlanco(Request.QueryString("Regimen")) > 0 Then
            Dim strScript As String
            strScript = "<SCRIPT LANGUAGE='JAVASCRIPT'>" & _
                            "document.getElementById('ChartCurvaAcequiaN').style.display='';" & _
                        "</SCRIPT>"

            ClientScript.RegisterStartupScript(Page.GetType, "VerDIVGrafico", strScript)
        End If
        '------------------------------------------------------------------------------------
        If Not IsPostBack Then
            'If Session("usuarioReg") = Nothing Then
            '    Response.Redirect("UsuarioNoRegistrado.aspx")
            '    Exit Sub
            'End If
            If utiles.nullABlanco(Request.QueryString("nodotexto")) <> "" Then
                LBLinfo.Text = nullABlanco(Request.QueryString("nodotexto").ToString)
            End If
            If utiles.nullABlanco(Request.QueryString("nodoclave")) <> "" Then
                LBLinfoclave.Text = nullABlanco(Request.QueryString("nodoclave").ToString())
                CodigoPVYCR = nullABlanco(Request.QueryString("codigoPVYCR"))
                IdElementoMedida = nullABlanco(Request.QueryString("IdElementoMedida"))

            End If
            If Request.QueryString("NuevaCurva") = "yes" Then
                pnlEDCurvasAcequias.Visible = True
                pnlCurvasAcequias.Visible = False
                pnlEdicionCurva.Visible = False
                lblTitulo.Text = "&nbsp;<b>NUEVA CURVA DE ACEQUIA</b>"
                'Cargamos los combos de edicion
                rellenarCombos()
                'Actualizamos el valor de la fecha inicial por defecto
                Me.txtFechaInicioUso.Text = Today()
                'Almacenamos las variables en controles locales
                CodigoPVYCR = nullABlanco(Request.QueryString("codigoPVYCR"))
                IdElementoMedida = nullABlanco(Request.QueryString("IdElementoMedida"))
                LBLinfoclave.Text = nullABlanco(Request.QueryString("nodoclave").ToString())
                'Mostramos el elemento sobre el que se agregará la nueva curva
                rellenaretiquetaAcequia(CodigoPVYCR, IdElementoMedida)
            Else
                pnlEDCurvasAcequias.Visible = False
                pnlCurvasAcequias.Visible = True
                pnlEdicionCurva.Visible = True

            End If

            CrearDataSetsCurvasAcequias()
            Array_datos(CodigoPVYCR, IdElementoMedida)
            'IPIM 10/06/2009
            If CodigoPVYCR <> "" And IdElementoMedida <> "" Then
                lblInformeCurvasAcequias.Text = "<img width='16' src='images/Invoice-32.png' style='cursor: hand;' onclick='window.open(""../listados/InformeCurvasAcequias.aspx?codigoPVYCR=" & _
                            Request.QueryString("codigoPVYCR") & "&idElementoMedida=" & Request.QueryString("idElementoMedida") & """)' />&nbsp;&nbsp;&nbsp;Informe curvas acequias"
            End If

        End If

    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        'EGB 13/08/2008 Copiado de Nay Ref. NCM 20080616 
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub nuevaCurvaAcequia(ByVal sender As Object, ByVal e As System.EventArgs)

        'EGB 19/08/2008 Correccion para incorporar la referencia del elemento seleccionado en el arbol 
        'tenga o no curvas asociadas
        '---
        'EGB 21/08/2008 Antes de Redirigir la página verificamos si el elemento seleccionado se corresponde con una Acequia
        Dim v_claves() As String
        If Request.QueryString("nodoclave") <> "" Then  'IPIM 01/09/2008: para el caso en el que no se ha seleccionado una acequia
            v_claves = Split(Request.QueryString("nodoclave").ToString(), ";")
            If v_claves(v_claves.GetUpperBound(0)) = "Q" Then
                Session("NuevaCurva") = "yes"
                Response.Redirect("CurvasAcequiasDETALLE.aspx?NuevaCurva=yes&CodigoPVYCR=" & Request.QueryString("codigoPVYCR").ToString & "&IdElementoMedida=" & Request.QueryString("idelementomedida").ToString & "&nodoclave=" & Request.QueryString("nodoclave").ToString())
            Else
                Alert(Page, "El elemento seleccionado no es una acequia")
            End If
        Else
            Alert(Page, "Debe seleccionar una acequia sobre la que insertar una curva")
        End If
    End Sub
    Sub CrearDataSetsCurvasAcequias()
       
       
        'EGB Sobrecaga 1/2 Metodo sobrecargado en el procedimiento siguiente.


        If dstCurvasAcequias.Tables.Contains("TablaCurvasAcequias") Then
            dstCurvasAcequias.Tables("TablaCurvasAcequias").Clear()
        End If
        Dim sFiltro As String = ""
        'Solo se muestran las Curvas de la Acequia Seleccionada
        sentenciaSel = "SELECT * FROM dbo.PVYCR_CurvasAcequias "
        sentenciawhere = "WHERE CodigoPVYCR='" & CodigoPVYCR & "' AND IdElementoMedida='" & IdElementoMedida & "' "
        sentenciaOrder = "ORDER BY CodigoPVYCR, idElementoMedida, Regimen, Fecha_Inicio_Uso"

        sentenciaSel = sentenciaSel & sentenciawhere & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequias")

        'Solo mostramos el Repeater de Curvas de la Acequia si existen registros
        If dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows.Count = 0 Then
            rptCurvasAcequias.Visible = False
            'EGB 26/09/2008 Chequar
            'ChartSeriesDeCurvasAcequia.Visible = False
        Else
            'pnlEdicionCurva.Visible = True
            rptCurvasAcequias.Visible = True
            rptCurvasAcequias.DataSource = dstCurvasAcequias.Tables("TablaCurvasAcequias")
            rptCurvasAcequias.DataBind()

            'EGB 26/09/2008 La siguiente seccion reemplaza este mecanismo
            '---------------------------------------------------------------------
            ''Cargamos las Series de Datos para mostrar las Curvas de la Acequia
            ''Solo se muestran las Curvas de la Acequia Seleccionada
            'If dstCurvasAcequias.Tables.Contains("TablaAuxCodigosCurvaAcequia") Then
            '    dstCurvasAcequias.Tables("TablaAuxCodigosCurvaAcequia").Clear()
            'End If
            'sentenciaSel = "SELECT DISTINCT Cod_Curva FROM dbo.PVYCR_CurvasAcequias "
            'sentenciawhere = "WHERE CodigoPVYCR='" & CodigoPVYCR & "' AND IdElementoMedida='" & IdElementoMedida & "' "
            'sentenciaOrder = "ORDER BY Cod_Curva"
            'sentenciaSel = sentenciaSel & sentenciawhere & sentenciaOrder

            'daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
            'daCurvasAcequias.Fill(dstCurvasAcequias, "TablaAuxCodigosCurvaAcequia")


            ''EGB 29/08/2008 Cargamos el Gráfico ChartSeriesDeCurvasAcequia con Todas las series de datos
            ''de la Acequia seleccionada
            'ChartSeriesDeCurvasAcequia.Visible = True
            'ChartSeriesDeCurvasAcequia.SeriesCollection.Clear()
            'For Each fila In dstCurvasAcequias.Tables("TablaAuxCodigosCurvaAcequia").Rows
            '    ActualizarDataSetValoresCurva(fila("Cod_Curva"))
            '    RefrescarGrafica(DeTableASerie(dstCurvasAcequias.Tables("TablaCurvasAcequiasValores")), ChartSeriesDeCurvasAcequia)
            'Next fila
            '--------------------------------------------------------------------------------------
        End If

    End Sub
    Sub CrearDataSetsCurvasAcequias(ByVal IdElementoMedida As String, ByVal CodigoPVYCR As String)
        Dim filas, i As Integer

        'EGB Sobrecaga 1/2 Metodo sobrecargado en el procedimiento siguiente.
        Dim fila As DataRow

        If dstCurvasAcequias.Tables.Contains("TablaCurvasAcequias") Then
            dstCurvasAcequias.Tables("TablaCurvasAcequias").Clear()
        End If
        Dim sFiltro As String = ""
        'Solo se muestran las Curvas de la Acequia Seleccionada
        sentenciaSel = "SELECT * FROM dbo.PVYCR_CurvasAcequias "
        sentenciawhere = "WHERE CodigoPVYCR='" & CodigoPVYCR & "' AND IdElementoMedida='" & IdElementoMedida & "' "
        sentenciaOrder = "ORDER BY CodigoPVYCR, idElementoMedida, Regimen, Fecha_Inicio_Uso"

        sentenciaSel = sentenciaSel & sentenciawhere & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequias")

        'Solo mostramos el Repeater de Curvas de la Acequia si existen registros
        If dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows.Count = 0 Then
            rptCurvasAcequias.Visible = False
            'EGB 26/09/2008 Chequar
            'ChartSeriesDeCurvasAcequia.Visible = False
        Else
            'pnlEdicionCurva.Visible = True
            rptCurvasAcequias.Visible = True
            rptCurvasAcequias.DataSource = dstCurvasAcequias.Tables("TablaCurvasAcequias")
            rptCurvasAcequias.DataBind()

            'EGB 26/09/2008 La siguiente seccion reemplaza este mecanismo
            '---------------------------------------------------------------------
            ''Cargamos las Series de Datos para mostrar las Curvas de la Acequia
            ''Solo se muestran las Curvas de la Acequia Seleccionada
            'If dstCurvasAcequias.Tables.Contains("TablaAuxCodigosCurvaAcequia") Then
            '    dstCurvasAcequias.Tables("TablaAuxCodigosCurvaAcequia").Clear()
            'End If
            'sentenciaSel = "SELECT DISTINCT Cod_Curva FROM dbo.PVYCR_CurvasAcequias "
            'sentenciawhere = "WHERE CodigoPVYCR='" & CodigoPVYCR & "' AND IdElementoMedida='" & IdElementoMedida & "' "
            'sentenciaOrder = "ORDER BY Cod_Curva"
            'sentenciaSel = sentenciaSel & sentenciawhere & sentenciaOrder

            'daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
            'daCurvasAcequias.Fill(dstCurvasAcequias, "TablaAuxCodigosCurvaAcequia")


            ''EGB 29/08/2008 Cargamos el Gráfico ChartSeriesDeCurvasAcequia con Todas las series de datos
            ''de la Acequia seleccionada
            'ChartSeriesDeCurvasAcequia.Visible = True
            'ChartSeriesDeCurvasAcequia.SeriesCollection.Clear()
            'For Each fila In dstCurvasAcequias.Tables("TablaAuxCodigosCurvaAcequia").Rows
            '    ActualizarDataSetValoresCurva(fila("Cod_Curva"))
            '    RefrescarGrafica(DeTableASerie(dstCurvasAcequias.Tables("TablaCurvasAcequiasValores")), ChartSeriesDeCurvasAcequia)
            'Next fila
            '--------------------------------------------------------------------------------------
        End If

    End Sub
    Private Sub ActualizarDataSetValoresCurva(ByVal Cod_Curva As String)
        Dim v_codigoPVYCR, v_idElementoMedida As String

        v_codigoPVYCR = CodigoPVYCR 'Session("CodigoPVYCR").ToString
        v_idElementoMedida = IdElementoMedida 'Session("idElementomedida").ToString

        If dstCurvasAcequias.Tables.Contains("TablaCurvasAcequiasValores") Then
            dstCurvasAcequias.Tables("TablaCurvasAcequiasValores").Clear()
        End If
        Dim sFiltro As String = ""

        'Solo se muestran las Curvas de la Acequia Seleccionada
        sentenciaSel = "SELECT Cod_Curva, Nivel,isnull(caudal,0) caudal, (select regimen from PVYCR_CurvasAcequias where codigoPVYCR='" & v_codigoPVYCR & _
                "' and idElementoMedida='" & v_idElementoMedida & "' and cod_curva=" & Cod_Curva & ") as regimen FROM dbo.PVYCR_CurvasAcequias_Valores " & _
         "WHERE Cod_Curva='" & Cod_Curva & "' "
        sentenciaOrder = "ORDER BY Cod_Curva, Nivel "

        sentenciaSel = sentenciaSel & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequiasValores")

    End Sub
    Private Sub DataSetValoresTodasLasCurvas()
        Dim v_codigoPVYCR, v_idElementoMedida As String

        v_codigoPVYCR = CodigoPVYCR 'Session("CodigoPVYCR").ToString
        v_idElementoMedida = IdElementoMedida 'Session("idElementomedida").ToString

        If dstCurvasAcequias.Tables.Contains("TablaCurvasAcequiasValores") Then
            dstCurvasAcequias.Tables("TablaCurvasAcequiasValores").Clear()
        End If
        Dim sFiltro As String = ""

        'Solo se muestran las Curvas de la Acequia Seleccionada
        sentenciaSel = "SELECT *, (select regimen from PVYCR_CurvasAcequias where codigoPVYCR='" & v_codigoPVYCR & _
                "' and idElementoMedida='" & v_idElementoMedida & "' ) as regimen FROM dbo.PVYCR_CurvasAcequias_Valores "

        sentenciaOrder = "ORDER BY Cod_Curva, Nivel "

        sentenciaSel = sentenciaSel & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequiasValores")

    End Sub
    Private Sub RefrescarGrafica(ByVal grafico As OpenFlashChart.OpenFlashChart, ByVal maximoCaudal As Double, ByVal lbls As List(Of String))

        Dim j As Integer = 0
        Dim i As Double
        If maximoCaudal > 0 Then

            'Dim UltimoValorX As Double = midatatable.Rows(filas - 1).Item("Nivel")
            'Dim PrimerValorX As Double = midatatable.Rows(0).Item("Nivel")
            Dim ultimoValorY As Double = maximoCaudal


            grafico.Title = New OpenFlashChart.Title(CodigoPVYCR + " / " + dstNombres.Tables("TablaNombres").Rows(0).Item("DenominacionPunto").ToString + " / " + vbCrLf + dstNombres.Tables("TablaNombres").Rows(0).Item("DenominacionCauce").ToString)
            grafico.Title.Style = "font-size:14px; font-family:Verdana; color: #8B8B46;font-weight: bold; background-color:#D7DDBB"
            grafico.X_Legend = New OpenFlashChart.Legend("Altura")
            grafico.Y_Legend = New OpenFlashChart.Legend("Caudal")
            grafico.X_Legend.Style = "font-size:12px; font-family:Verdana; color: #000000"
            grafico.Y_Legend.Style = "font-size:14px; font-family:Verdana; color: #000000"

            grafico.Y_Axis.Min = 0

            'grafico.Y_Axis.Steps = 10
            If ultimoValorY > 1 Then
                grafico.Y_Axis.Max = ultimoValorY * 1.1
            Else
                grafico.Y_Axis.Max = 1
            End If

            grafico.Y_Axis.Steps = 1

            grafico.X_Axis.Labels.Values = lbls
            grafico.X_Axis.Labels.Vertical = True
        Else
            grafico.Title = New OpenFlashChart.Title("No existen Valores")
        End If

        Response.Clear()

        Response.Write(grafico.ToString())

        Response.End()
    End Sub
    Private Sub CrearDataSetCurvaAcequia(ByVal parametros() As String)
        Dim sFiltro As String = ""
        'Solo se muestran los datos de la Curva
        sentenciaSel = "SELECT * FROM dbo.PVYCR_CurvasAcequias "
        sentenciawhere = "WHERE CodigoPVYCR='" & parametros(0) & "' AND IdElementoMedida='" & parametros(1) & "' AND Regimen='" & parametros(2) & "' AND Fecha_INICIO_USO='" & parametros(3) & "' "
        sentenciaOrder = "ORDER BY CodigoPVYCR, idElementoMedida, Regimen, Fecha_Inicio_Uso"

        sentenciaSel = sentenciaSel & sentenciawhere & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequias")

        'EGB 22/08/2008 Actualización del panel de listar valores de las curvas
        If dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows.Count > 0 Then
            CrearDataSetCurvaAcequiaValores(dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows(0).Item("Cod_Curva"))
        End If
        Array_datos_1curva(parametros(0), parametros(1), parametros(2))
    End Sub
    Private Sub CrearDataSetCurvaAcequiaValores(ByVal Cod_Curva As String)

        ActualizarDataSetValoresCurva(Cod_Curva)
        rptCurvasAcequiasValores.DataSource = dstCurvasAcequias.Tables("TablaCurvasAcequiasValores")
        rptCurvasAcequiasValores.DataBind()

        'EGB 25/08/2008 Actualización del Gráfico
        'EGB 26/09/2008 Chequear Inclusión de nuevo control de grafico
        'RefrescarGrafica(DeTableASerie(dstCurvasAcequias.Tables("TablaCurvasAcequiasValores")), ChartCurvaAcequia)

    End Sub
    'Private Sub RefrescarGrafica(ByVal MiSerieDeDatos As SeriesCollection, ByVal Migrafica As dotnetCHARTING.Chart)
    '    With Migrafica
    '        .Type = ChartType.Scatter
    '        .SeriesCollection.Add(MiSerieDeDatos)
    '    End With
    'End Sub
    'Private Function DeTableASerie(ByVal midatatable As DataTable) As SeriesCollection
    '    'EGB 25/08/2008 Función que transforma un DataTable a un elemento de la colección SeriesCollection
    '    Dim SC As New SeriesCollection()
    '    Dim fila As DataRow
    '    Dim serie As New Series()
    '    Dim elemento As New Element()

    '    'IPIM 01/09/2008 Para no mostrar las curvas con nivel 0 y 1000 y sólo dos lecturas
    '    If lblDetalle.Text = "No" And midatatable.Rows.Count = 2 Then
    '        If midatatable.Rows(0).Item("Nivel") = 0 And midatatable.Rows(1).Item("Nivel") = 1000 Then
    '            Return SC
    '            Exit Function
    '        End If
    '    End If

    '    If midatatable.Rows.Count > 0 Then
    '        serie.Name = "Curva " & midatatable.Rows(0).Item("Cod_Curva") & " (" & midatatable.Rows(0).Item("Regimen") & ")"
    '        For Each fila In midatatable.Rows
    '            elemento = New Element()
    '            elemento.XValue = nullACero(fila("Nivel"))
    '            elemento.YValue = nullACero(fila("Caudal"))
    '            serie.Elements.Add(elemento)
    '        Next fila

    '        'serie.XAxis.Name = "Nivel"
    '        'serie.YAxis.Name = "Caudal"
    '        SC.Add(serie)
    '    End If
    '    Return SC
    'End Function
    Sub ActualizarPaneles()
        'Mostrar panel de Curvas de Acequias
        pnlCurvasAcequias.Visible = True
    End Sub
    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Protected Sub rptCurvasAcequias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptCurvasAcequias.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String
        Dim objTrans As SqlTransaction

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Session("NuevaCurva") = "No"
                Me.pnlEDCurvasAcequias.Visible = True
                Me.pnlCurvasAcequias.Visible = False
                Me.pnlEdicionCurva.Visible = True


                'Valores de clave principal para la edición del registro
                Me.lblcodigopvycrSel.Text = parametros(0)
                Me.lblElementoSel.Text = parametros(1)
                Me.lblregimenSel.Text = parametros(2)
                Me.lblFechaInicioUsoSel.Text = parametros(3)

                'EGB 29/09/2008 Variables de Sesion para incrustar nuevo grafico
                Session("Regimen") = parametros(2)
                Session("Fecha_inicio_uso") = parametros(3)

                Me.lblDetalle.Text = "Si"   'IPIM 01/09/2008

                'Crear DataSet con las claves anteriores
                CrearDataSetCurvaAcequia(parametros)
                CargarControlesEdicionCurvaAcequia()

            Case "borrar"
                If conexion.State = ConnectionState.Closed Then conexion.Open()
                objTrans = conexion.BeginTransaction()

                Try
                    comando.Transaction = objTrans

                    'Primero borramos los valores que había cogido la curva (los hijos)
                    Dim cod_curva As Integer
                    comando.CommandText = "Select cod_curva from PVYCR_CurvasAcequias where CodigoPVYCR='" & parametros(0) & "' and idelementoMedida='" & parametros(1) & "' and Regimen='" & parametros(2) & "' and Fecha_Inicio_Uso='" & parametros(3) & "'"
                    cod_curva = utiles.nullACero(comando.ExecuteScalar)

                    If cod_curva = 991 Then
                        Alert(Me, "No se puede eliminar la curva 991")
                        objTrans.Rollback()
                        Exit Try
                    End If

                    If cod_curva <> 0 Then
                        comando.CommandText = "DELETE FROM PVYCR_CurvasAcequias_Valores WHERE Cod_Curva=" & cod_curva
                        comando.ExecuteNonQuery()
                    End If

                    comando.CommandText = "delete from PVYCR_CurvasAcequias where CodigoPVYCR='" & parametros(0) & "' and idelementoMedida='" & parametros(1) & "' and Regimen='" & parametros(2) & "' and Fecha_Inicio_Uso='" & parametros(3) & "'"
                    comando.ExecuteNonQuery()
                    objTrans.Commit()

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar la Curva de Acequia")
                    objTrans.Rollback()
                End Try
                CrearDataSetsCurvasAcequias(parametros(1), parametros(0))

        End Select

    End Sub
    Private Sub CargarControlesEdicionCurvaAcequia()

        'EGB 19/08/2008 Carga de los Datos del Punto en los controles del panel de Edicion de la pagina
        lblTitulo.Text = "&nbsp;<b>MANTENIMIENTO CURVA DE ACEQUIA</b>"

        If dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows.Count = 1 Then
            'Cargamos la etiqueta de la acequia seleccionada
            rellenaretiquetaAcequia(Me.lblcodigopvycrSel.Text, Me.lblElementoSel.Text)
            'Cargamos los Combos
            rellenarCombos()
            'Carga de los controles de edicion
            Me.ddlregimen.SelectedValue = dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows(0).Item("Regimen")
            Me.txtFechaInicioUso.Text = dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows(0).Item("Fecha_INICIO_USO")
            If (dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows(0).Item("En_Activo") = "1") Then
                Me.chkenActivo.Checked = True
            Else
                Me.chkenActivo.Checked = False
            End If

            'Me.ddlCodigoCurva.SelectedValue = dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows(0).Item("Cod_Curva")    'ipim 01/09/2008: quitamos el desplegable
            Me.txtCodigoCurva.Text = utiles.nullABlanco(dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows(0).Item("Cod_Curva"))
            Me.txtFechaFinUso.Text = utiles.nullABlanco(dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows(0).Item("Fecha_FIN_USO"))

            Me.txtComentarios.Text = utiles.nullABlanco(dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows(0).Item("Comentarios"))
            Me.txtProbabilidad.Text = utiles.nullABlanco(dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows(0).Item("Probabilidad"))

        End If
    End Sub
    Protected Sub rellenaretiquetaAcequia(ByVal CodigoPVYCR As String, ByVal IdElementoMedida As String)
        If IsNothing(dstCurvasAcequias.Tables("TablaArbolAUX")) Then
            daCurvasAcequias.SelectCommand.CommandText = "SELECT strnodo FROM PVYCR_Arbol WHERE CodigoPVYCR='" & CodigoPVYCR & "' AND IdElementoMedida='" & IdElementoMedida & "'"
            daCurvasAcequias.Fill(dstCurvasAcequias, "TablaArbolAUX")
        End If
        If dstCurvasAcequias.Tables("TablaArbolAUX").Rows.Count = 1 Then
            Me.lblDescripcionAcequia.Text = dstCurvasAcequias.Tables("TablaArbolAUX").Rows(0).Item("strnodo")
        End If
    End Sub
    Protected Sub rellenarCombos()
        'Carga de los datos del combo de Regimen de Acequias 
        If IsNothing(dstCurvasAcequias.Tables("TablaRegimenes")) Then
            daCurvasAcequias.SelectCommand.CommandText = "SELECT regimen + ' ' + descripcion as descripcionDDL, regimen FROM PVYCR_Acequias_Regimenes ORDER BY regimen"
            daCurvasAcequias.Fill(dstCurvasAcequias, "TablaRegimenes")
        End If
        ddlregimen.DataSource = dstCurvasAcequias.Tables("TablaRegimenes")
        ddlregimen.DataValueField = "regimen"
        ddlregimen.DataTextField = "descripcionDDL"
        ddlregimen.DataBind()

        'ipim 01/09/2008: quitamos el desplegable
        'Carga de los datos del combo de Curvas de Acequias
        'If IsNothing(dstCurvasAcequias.Tables("TablaCurvasAcequiasAUX")) Then
        '    daCurvasAcequias.SelectCommand.CommandText = "SELECT DISTINCT cod_curva FROM PVYCR_CurvasAcequias_Valores ORDER BY Cod_Curva"
        '    daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequiasAUX")
        'End If
        'ddlCodigoCurva.DataSource = dstCurvasAcequias.Tables("TablaCurvasAcequiasAUX")
        'ddlCodigoCurva.DataValueField = "cod_curva"
        'ddlCodigoCurva.DataTextField = "cod_curva"
        'ddlCodigoCurva.DataBind()
        ''ddlCodigoCurva.Text = ""

        'IPIM 01/09/2008
        'pnlEdicionCurva.Visible = True

        If Session("NuevaCurva") <> "No" Then
            txtCodigoCurva.Text = calcularMax()
            CrearDataSetCurvaAcequiaValores(txtCodigoCurva.Text)
        End If
    End Sub
    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoTemp As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)
        'Dim clavenuevonodoPVYCRArbol As String
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        comando.Parameters.Clear()
        Me.lblDetalle.Text = "No"   'IPIM 01/09/2008

        Try
            If Session("NuevaCurva") = "No" Then

                'ACTUALIZAMOS valores de la Curva de Acequias Seleccionada
                comando.CommandText = "UPDATE [PVYCR_CurvasAcequias] SET" & _
                                      " [Cod_Curva] = @Cod_Curva " & _
                                      ",[En_Activo] = @En_Activo " & _
                                      ",[FECHA_FIN_USO] = @FECHA_FIN_USO " & _
                                      ",[Comentarios] = @Comentarios " & _
                                      ",[Probabilidad] = @Probabilidad " & _
                                      "WHERE [CodigoPVYCR]=@CodigoPVYCR AND " & _
                                            "[idElementoMedida]=@IdElementoMedida AND " & _
                                            "[REGIMEN]=@REGIMEN AND " & _
                                            "[FECHA_INICIO_USO]=@Fecha_Inicio_Uso"
            Else
                'INSERTAMOS un nueva Curva de Acequias

                If comprobarCodCurva(txtCodigoCurva.Text) = True Then       'IPIM 02/09/2008 Si ya existe el código avisamos y salimos
                    Alert(Me, "Este código no puede asignarse porque ya está asociado a otra curva")
                    Exit Sub
                End If

                comando.CommandText = "INSERT INTO [PVYCR_CurvasAcequias] ([CodigoPVYCR]" & _
                                                                        " ,[idElementoMedida] " & _
                                                                        " ,[REGIMEN]" & _
                                                                        " ,[FECHA_INICIO_USO]" & _
                                                                        " ,[Cod_Curva]" & _
                                                                        " ,[En_Activo]" & _
                                                                        " ,[FECHA_FIN_USO]" & _
                                                                        " ,[Comentarios]" & _
                                                                        " ,[Probabilidad])" & _
                                                                        " VALUES(" & _
                                                                       "@CodigoPVYCR, " & _
                                                                       "@idElementoMedida, " & _
                                                                       "@REGIMEN, " & _
                                                                       "@FECHA_INICIO_USO, " & _
                                                                       "@Cod_Curva, " & _
                                                                       "@En_Activo, " & _
                                                                       "@FECHA_FIN_USO, " & _
                                                                       "@Comentarios, " & _
                                                                       "@Probabilidad)"

            End If


            comando.Parameters.AddWithValue("CodigoPVYCR", utiles.BlancoANull(Request.QueryString("CodigoPVYCR")))

            comando.Parameters.AddWithValue("idElementoMedida", utiles.BlancoANull(Request.QueryString("IdElementoMedida")))

            'comando.Parameters.AddWithValue("CodigoPVYCR", utiles.BlancoANull(Me.lblcodigopvycrSel.Text))

            'comando.Parameters.AddWithValue("idElementoMedida", utiles.BlancoANull(Me.lblElementoSel.Text))

            comando.Parameters.AddWithValue("REGIMEN", utiles.BlancoANull(Me.ddlregimen.Text))

            comando.Parameters.AddWithValue("FECHA_INICIO_USO", utiles.BlancoANull(Me.txtFechaInicioUso.Text))

            'comando.Parameters.AddWithValue("Cod_Curva", utiles.BlancoANull(Me.ddlCodigoCurva.Text))
            comando.Parameters.AddWithValue("Cod_Curva", utiles.BlancoANull(Me.txtCodigoCurva.Text))    'ipim 01/09/2008: quitamos el desplegable

            If chkenActivo.Checked Then
                comando.Parameters.AddWithValue("En_Activo", "1")
            Else
                comando.Parameters.AddWithValue("En_Activo", "0")
            End If

            comando.Parameters.AddWithValue("FECHA_FIN_USO", utiles.BlancoANull(Me.txtFechaFinUso.Text))

            comando.Parameters.AddWithValue("Comentarios", utiles.BlancoANull(Me.txtComentarios.Text))

            'comando.Parameters.AddWithValue("Probabilidad", utiles.BlancoANull(Me.txtProbabilidad.Text))
            comando.Parameters.AddWithValue("Probabilidad", utiles.BlancoANull(Replace(Me.txtProbabilidad.Text, ",", ".")))

            comando.ExecuteNonQuery()

            'IPIM 03/09/2008: Cuando se inserta la 1ª curva para una acequia, se inserta también la curva  991 (CORTADA) con sus puntos y datos.
            InsertarCurvaCortada()

            'Repintar el grafico
            CrearDataSetsCurvasAcequias(utiles.BlancoANull(Request.QueryString("IdElementoMedida")), utiles.BlancoANull(Request.QueryString("CodigoPVYCR")))

        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                Case 2627
                    Alert(Page, "La curva ya existe ")
                Case Else
                    Alert(Page, Err.Description & "\n Consulte con el administrador")
            End Select
        End Try

        Me.pnlCurvasAcequias.Visible = True
        Me.pnlEDCurvasAcequias.Visible = False

        CrearDataSetsCurvasAcequias(Request.QueryString("IdElementoMedida"), Request.QueryString("CodigoPVYCR"))

    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.pnlCurvasAcequias.Visible = True
        Me.pnlEDCurvasAcequias.Visible = False
        Me.lblDetalle.Text = "No"   'IPIM 01/09/2008

        CrearDataSetsCurvasAcequias(Request.QueryString("IdElementoMedida"), Request.QueryString("CodigoPVYCR"))
        'NCM: llamada a grafico

    End Sub
    'IPIM 01/09/2008: quitamos el desplegable
    'Protected Sub ddlCodigoCurva_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodigoCurva.SelectedIndexChanged

    '    'EGB 22/08/2008 Actualización del panel de listar valores de las curvas
    '    pnlEdicionCurva.Visible = True
    '    CrearDataSetCurvaAcequiaValores(ddlCodigoCurva.SelectedValue)

    'End Sub
    Protected Sub rptCurvasAcequiasValores_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptCurvasAcequiasValores.ItemCommand
        Dim parametros() As String
        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Me.txtNuevoCaudal.Text = parametros(2)
                Me.txtNuevoNivel.Text = parametros(1)
                Me.imgAgregarValoresCurva.ImageUrl = "../images/save.gif"
                Me.lblOperacion.Text = "Guardar"
                'Me.pnlEdicionValoresCurva.Visible = True
            Case "borrar"
                'Me.pnlEdicionValoresCurva.Visible = False
                If conexion.State = ConnectionState.Closed Then conexion.Open()
                objTrans = conexion.BeginTransaction()
                Try
                    comando.Transaction = objTrans
                    comando.CommandText = "delete from PVYCR_CurvasAcequias_Valores WHERE Cod_Curva='" & parametros(0) & "' and Nivel=" & Replace(parametros(1), ",", ".") & ""
                    comando.ExecuteNonQuery()
                    objTrans.Commit()

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el valor de la lista")
                    objTrans.Rollback()
                End Try

        End Select
        'CrearDataSetCurvaAcequiaValores(ddlCodigoCurva.SelectedValue)  'ipim 01/09/2008: quitamos el desplegable
        CrearDataSetCurvaAcequiaValores(txtCodigoCurva.Text)

    End Sub
    Protected Sub imgAgregarValoresCurva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAgregarValoresCurva.Click
        Dim strSQL As String
        Try
            If Me.imgAgregarValoresCurva.ImageUrl = "../images/save.gif" Then
                'EDITAR datos del registro

                strSQL = "UPDATE PVYCR_CurvasAcequias_Valores SET Caudal=@Caudal, Nivel=@Nivel WHERE Cod_Curva=@Cod_Curva AND Nivel=@Nivel"
                'Aprovechamos para cambiar la imagen que pasa de nuevo a fijarse como nuevo
                Me.imgAgregarValoresCurva.ImageUrl = "../images/plus.gif"
                Me.lblOperacion.Text = "Agregar"

            Else
                'INSERTAMOS nuevos valores
                strSQL = "INSERT PVYCR_CurvasAcequias_Valores (Cod_Curva,Nivel,Caudal) VALUES (@Cod_Curva,@Nivel,@Caudal)"
            End If

            'Actualización de Parametros de la Curva
            'comando.Parameters.AddWithValue("Cod_Curva", utiles.BlancoANull(Me.ddlCodigoCurva.Text))   'ipim 01/09/2008: quitamos el desplegable
            comando.Parameters.AddWithValue("Cod_Curva", utiles.BlancoANull(Me.txtCodigoCurva.Text))
            comando.Parameters.AddWithValue("Nivel", utiles.BlancoANull(Replace(Me.txtNuevoNivel.Text, ",", ".")))
            comando.Parameters.AddWithValue("Caudal", utiles.BlancoANull(Replace(Me.txtNuevoCaudal.Text, ",", ".")))

            If conexion.State = ConnectionState.Closed Then conexion.Open()
            comando.CommandText = strSQL
            comando.ExecuteNonQuery()

            'CrearDataSetCurvaAcequiaValores(Me.ddlCodigoCurva.Text)    'ipim 01/09/2008: quitamos el desplegable
            CrearDataSetCurvaAcequiaValores(Me.txtCodigoCurva.Text)

            'Limpiamos controles
            Me.txtNuevoNivel.Text = ""
            Me.txtNuevoCaudal.Text = ""

        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                Case 2627
                    Alert(Page, "Los valores ya existen ")
                Case Else
                    Alert(Page, Err.Description & "\n Consulte con el administrador")
            End Select
        End Try

    End Sub
    Protected Function calcularMax() As Integer
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        Dim maxPorPunto As Integer

        comando.CommandText = "SELECT MAX(Cod_Curva) FROM PVYCR_CurvasAcequias WHERE CodigoPVYCR='" & Request.QueryString("CodigoPVYCR") & _
                        "' AND idElementoMedida='" & Request.QueryString("IdElementoMedida") & "'"
        maxPorPunto = utiles.nullACero(comando.ExecuteScalar())

        If maxPorPunto <> 0 Then
            comando.CommandText = "SELECT count(*) FROM PVYCR_CurvasAcequias WHERE Cod_Curva=" & maxPorPunto + 1
            If comando.ExecuteScalar <> 0 Then
                comando.CommandText = "SELECT MAX(Cod_Curva) FROM PVYCR_CurvasAcequias"
                maxPorPunto = comando.ExecuteScalar + 10
            Else
                maxPorPunto = maxPorPunto + 1
            End If
        Else
            comando.CommandText = "SELECT MAX(Cod_Curva) FROM PVYCR_CurvasAcequias"
            maxPorPunto = comando.ExecuteScalar + 10
        End If

        Return maxPorPunto
    End Function
    Protected Function comprobarCodCurva(ByVal codigo As Integer) As Boolean
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        If conexion.State = ConnectionState.Closed Then conexion.Open()

        comando.CommandText = "SELECT count(*) FROM PVYCR_CurvasAcequias WHERE Cod_Curva=" & codigo
        If comando.ExecuteScalar <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub InsertarCurvaCortada()
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        If conexion.State = ConnectionState.Closed Then conexion.Open()

        'Primero tenemos que comprobar que la acequia no tiene ninguna curva todavía.
        comando.CommandText = "SELECT count(*) FROM PVYCR_CurvasAcequias WHERE CodigoPVYCR='" & Request.QueryString("CodigoPVYCR") & "'" & _
                            " and idElementoMedida='" & Request.QueryString("idElementoMedida") & "'"
        If utiles.nullACero(comando.ExecuteScalar) <> 1 Then
            Exit Sub
        End If

        comando.CommandText = "INSERT INTO PVYCR_CurvasAcequias (CodigoPVYCR, idElementoMedida, Regimen, Fecha_inicio_uso, cod_curva, En_Activo, " & _
                            "Fecha_fin_uso, Comentarios, Probabilidad) VALUES ('" & Request.QueryString("codigoPVYCR") & "', '" & _
                            Request.QueryString("idElementoMedida") & "', 'CT', '" & Date.Today & "', 991, 1, '01/01/2100', 'CORTADA', 100.00)"
        comando.ExecuteNonQuery()

    End Sub


    Private Sub Array_datos(ByVal pvycr As String, ByVal EM As String)
        Dim i, ii As Int16
        Dim str, Sql As String
        Dim dt, dt2 As Data.DataTable


        Sql = "SELECT regimen, Cod_curva FROM dbo.PVYCR_CurvasAcequias "
        Sql = Sql + "WHERE CodigoPVYCR='" & CodigoPVYCR & "' AND IdElementoMedida='" & IdElementoMedida & "' AND Fecha_Fin_Uso >'" & Date.Now() & "'"
        Sql = Sql + "ORDER BY CodigoPVYCR, idElementoMedida, Regimen, Fecha_Inicio_Uso"

        dt = EjecutaSQL(Sql)
        str = ""
        str = str & "series: ["
        For i = 0 To dt.Rows.Count - 1
            If (i > 0) Then str = str & ","
            str = str & "{name: '" & dt.Rows(i).Item(0).ToString() & "', data:["
            Sql = "select * from PVYCR_CurvasAcequias_valores where cod_curva like '" & dt.Rows(i).Item(1).ToString() & "'"
            dt2 = EjecutaSQL(Sql)
            For ii = 0 To dt2.Rows.Count - 1
                If (ii > 0) Then str = str & ","
                If (dt2.Rows(ii).Item(1) > 20) Then
                    str = str + "[2," & dt2.Rows(ii).Item(2).ToString().Replace(",", ".") & "]"
                Else
                    str = str + "[" & dt2.Rows(ii).Item(1).ToString().Replace(",", ".") & "," & dt2.Rows(ii).Item(2).ToString().Replace(",", ".") & "]"
                End If

            Next
            str = str & "]}"
        Next
        str = str & "]"
        Dim strdef As String
        strdef = "<script>jQuery(function() {var chart2 = new Highcharts.Chart({chart: { renderTo: 'chart2' },navigator: { height: 60 }, title: { text: 'Curvas Acequias', floating: false, align: 'center', x: -20, top: 20 }, yAxis: {title: {text:'Caudal'}}, xAxis: { type:'double',title: {text:'Nivel'}}," & str & " });});</script>"
        LIT_grafico.Text = strdef
        strdef = "<script>jQuery(function() {var chart21 = new Highcharts.Chart({chart: { renderTo: 'chart21' },navigator: { height: 60 }, title: { text: 'Curvas Acequias', floating: false, align: 'center', x: -20, top: 20 }, yAxis: {title: {text:'Caudal'}}, xAxis: { type:'double',title: {text:'Nivel'}}," & str & " });});</script>"
        LIT_graficoedicion.Text = strdef

        'Sql = "SELECT * FROM PVYCR_Conductividad WHERE (codigopvycr like '" + pvycr + "%' OR codigopvycr like '" + pvycr + "') order by fecha_medida asc"
        'dt = EjecutaSQL(Sql)
        'str = "var Values=["
        'For i = 0 To dt.Rows.Count - 1
        '    Dim anyo As String
        '    anyo = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(2).ToString()).Year.ToString()
        '    Dim mes As String
        '    mes = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(2).ToString()).Month.ToString()
        '    Dim dia As String
        '    dia = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(2).ToString()).Day.ToString()
        '    Dim hora As String
        '    hora = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(2).ToString()).Hour.ToString()
        '    Dim minuto As String
        '    minuto = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(2).ToString()).Minute.ToString()
        '    Dim segundo As String
        '    segundo = DateTime.Parse(dt.Rows(i).ItemArray.GetValue(2).ToString()).Second.ToString()
        '    Dim valor As String
        '    v = 8
        '    valor = dt.Rows(i).ItemArray.GetValue(v).ToString().Trim()
        '    str = str & "[Date.UTC(" & anyo & ", " & mes & ", " & dia & ", " & hora & ", " & minuto & ", " & segundo & "), " & valor.Replace(",", ".") & "],"
        'Next
        'str = str.Substring(0, str.Length - 1)
        'str = str & "];"
        'Dim cstext2 As New StringBuilder()
        'cstext2.Append("<script type=""text/javascript"">")
        'cstext2.Append(str)
        'cstext2.Append("</script>")
        'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "strData", cstext2.ToString(), False)

    End Sub

    Private Sub Array_datos_1curva(ByVal pvycr As String, ByVal EM As String, ByVal C As String)
        Dim i, ii As Int16
        Dim str, Sql As String
        Dim dt, dt2 As Data.DataTable


        Sql = "SELECT regimen, Cod_curva FROM dbo.PVYCR_CurvasAcequias "
        Sql = Sql + "WHERE CodigoPVYCR='" & pvycr & "' AND IdElementoMedida='" & EM & "' AND regimen like '" & C & "' AND Fecha_Fin_Uso >'" & Date.Now() & "'"
        Sql = Sql + "ORDER BY CodigoPVYCR, idElementoMedida, Regimen, Fecha_Inicio_Uso"

        dt = EjecutaSQL(Sql)
        str = ""
        str = str & "series: ["
        For i = 0 To dt.Rows.Count - 1
            If (i > 0) Then str = str & ","
            str = str & "{name: '" & dt.Rows(i).Item(0).ToString() & "', data:["
            Sql = "select * from PVYCR_CurvasAcequias_valores where cod_curva like '" & dt.Rows(i).Item(1).ToString() & "'"
            dt2 = EjecutaSQL(Sql)
            For ii = 0 To dt2.Rows.Count - 1
                If (ii > 0) Then str = str & ","
                If (dt2.Rows(ii).Item(1) > 20) Then
                    str = str + "[2," & dt2.Rows(ii).Item(2).ToString().Replace(",", ".") & "]"
                Else
                    str = str + "[" & dt2.Rows(ii).Item(1).ToString().Replace(",", ".") & "," & dt2.Rows(ii).Item(2).ToString().Replace(",", ".") & "]"
                End If

            Next
            str = str & "]}"
        Next
        str = str & "]"
        Dim strdef As String
        strdef = "<script>jQuery(function() {var chart21 = new Highcharts.Chart({chart: { renderTo: 'chart21' },navigator: { height: 60 }, title: { text: 'Curvas Acequias', floating: false, align: 'center', x: -20, top: 20 }, yAxis: {title: {text:'Caudal (m3/s)'}}, xAxis: { type:'double',title: {text:'Nivel (m)'}}," & str & " });});</script>"
        LIT_graficoedicion.Text = strdef
        'strdef = "<script>jQuery(function() {var chart21 = new Highcharts.Chart({chart: { renderTo: 'chart21' },navigator: { height: 60 }, title: { text: 'Curvas Acequias', floating: false, align: 'center', x: -20, top: 20 }, yAxis: {title: {text:'Caudal'}}, xAxis: { type:'double',title: {text:'Nivel'}}," & str & " });});</script>"
        'LIT_graficoedicion.Text = strdef

    End Sub




    Protected Function EjecutaSQL(ByVal queryString As String) As Data.DataTable
        Dim connectionString As String
        connectionString = ConfigurationManager.ConnectionStrings("PDASIGVECTOR").ToString()
        Dim dtemp As Data.DataTable
        dtemp = New Data.DataTable()
        Dim connection As System.Data.SqlClient.SqlConnection
        connection = New System.Data.SqlClient.SqlConnection(connectionString)
        Dim adapter As System.Data.SqlClient.SqlDataAdapter
        adapter = New System.Data.SqlClient.SqlDataAdapter(queryString, connection)
        adapter.Fill(dtemp)
        Return dtemp
    End Function



End Class


