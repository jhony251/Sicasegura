Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.IO
Partial Class SICAH_Agrupaciones
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daAgru As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstagru As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        lbtNuevo.DataBind()
        'scripts de cliente para el calendario
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        imgFFechaIni.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaIni.ClientID & "'),'dd/mm/yyyy');")
        ImgFFechafin.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaFin.ClientID & "'),'dd/mm/yyyy');")
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            lblCabecera.Text = genHTML.cabecera(Page)
            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))
            If Request.QueryString("nuevo") = "yes" Then
                pnlEDAgrupaciones.Visible = True
                pnlAgru.Visible = False
                'ucPaginacion.Visible = False
                lblTitulo.Text = "&nbsp;<b>NUEVA AGRUPACIÓN</b>"
            End If

            crearDataSets()
        End If
    End Sub
    Private Sub crearDataSets()
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        If txtRegistros.Text = "" Then
            sentenciaSel = " SELECT "
        Else
            sentenciaSel = " SELECT TOP " & txtRegistros.Text
        End If
        If Session("FiltroAgru") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroAgru")
            Else
                sFiltro = sFiltro + Session("FiltroAgru")
            End If
        End If

        sentenciaSel = sentenciaSel & "  IdAgrupacion, descripcion, FechaInicio, fechafin, colorCalendario from pvycr_agrupaciones "

        sentenciaOrder = " order by descripcion "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If

        daAgru.SelectCommand.CommandText = sentenciaSel
        daAgru.Fill(dstagru, "TablaAgru")
        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daAgru.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        'ucPaginacion.calcularPags(txtComando)

        rptAgru.DataSource = dstagru.Tables("TablaAgru")
        rptAgru.DataBind()

    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Protected Sub AceptarFiltro(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081002
        Dim strFiltro As String
        strFiltro = ""
        If txtFDescripcion.Text <> "" Then
            strFiltro = strFiltro + " AND descripcion like '%" + txtFDescripcion.Text + "%' "
        End If

        If txtFFechaIni.Text <> "" Then
            strFiltro = strFiltro + " AND  FechaInicio between '" + txtFFechaIni.Text + " 00:00:00' and '" + txtFFechaIni.Text + " 23:59:59' "
        End If

        If txtFFechaFin.Text <> "" Then
            strFiltro = strFiltro + " AND  FechaFin between '" + txtFFechaFin.Text + " 00:00:00' and '" + txtFFechaFin.Text + " 23:59:59' "
        End If

        If txtFColor.Text <> "" Then
            strFiltro = strFiltro + " AND colorcalendario like '%" + txtFColor.Text + "%' "
        End If
        Session("FiltroAgru") = strFiltro
        crearDataSets()
    End Sub
    Protected Sub nuevaagrupacion(ByVal sender As Object, ByVal e As System.EventArgs)
        lblAgrupacionSel.Text = ""
        Response.Redirect("agrupaciones.aspx?nuevo=yes")

    End Sub

    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        crearDataSets()
    End Sub

    'Protected Sub rptAgru_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptAgru.ItemCommand
    '    Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
    '    Dim elemento As ListItem
    '    Dim v_tipo As String
    '    Dim parametros() As String
    '    Dim objTrans As SqlTransaction

    '    parametros = Split(e.CommandArgument, "#")
    '    Select Case e.CommandName
    '        Case "editar"
    '            lblAgrupacionSel.Text = parametros(0)
    '            pnlagr.Visible = False
    '            'ucPaginacion.Visible = False
    '            pnlEDContadores.Visible = True
    '            pnlEDcabecera.Visible = True
    '            crearDataSetContador(parametros(0), parametros(1))
    '            lblTitulo.Text = "<b>CONTADOR: " & lblContadorSel.Text & "</b>"
    '            If dstContadores.Tables("TablaContadores").Rows.Count > 0 Then
    '                'activamos el panel tipo
    '                If utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("TipoContador")) = "" Then
    '                    'obtenemos el tipo del código
    '                    v_tipo = ObtenerTipoCont(dstContadores.Tables("TablaContadores").Rows(0).Item("idContador").ToString) 'dstContadores.Tables("TablaContadores").Rows(0).Item("idContador").ToString.Substring(dstContadores.Tables("TablaContadores").Rows(0).Item("idContador").ToString.Length - 6, 1)
    '                    txtTipoCont.Text = v_tipo
    '                    activarpaneltipo(v_tipo)
    '                Else
    '                    v_tipo = dstContadores.Tables("TablaContadores").Rows(0).Item("TipoContador")
    '                    txtTipoCont.Text = dstContadores.Tables("TablaContadores").Rows(0).Item("TipoContador")
    '                    activarpaneltipo(v_tipo)
    '                End If
    '                '----------cargamos los datos comunes a todos los tipos------------------------------
    '                txtidContador.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("idContador"))
    '                'como forma parte de la clave primaria no les dejamos modificar estos campos
    '                txtidContador.Enabled = False
    '                txtCodigoPVYCR.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("CodigoPVYCR"))
    '                txtFecharevision.Text = utiles.nullACero(dstContadores.Tables("TablaContadores").Rows(0).Item("FechaRevision"))
    '                txtCodigoCaracterizacion.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("CodigoCaracterizacion"))
    '                'como forma parte de la clave primaria no les dejamos modificar estos campos
    '                txtFecharevision.Enabled = False
    '                txtFechaIni.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("FechaInicial"))
    '                txtFechaFin.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("FechaFin"))
    '                'Campo Descripcion
    '                'RDF. Fecha: 10/02/2009
    '                txtDescripcion.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Descripcion"))
    '                If utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("accesoacontador")) = "SI" Then
    '                    chkAccesoContador.Checked = True
    '                Else
    '                    chkAccesoContador.Checked = False
    '                End If

    '                txtAforable.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("aforable"))
    '                txtdPtoAForo.Text = utiles.nullACero(dstContadores.Tables("TablaContadores").Rows(0).Item("distanciaptoaforo_km"))
    '                '---------aquí cargamos según el tipo------------------------'
    '                If v_tipo = "V" Then
    '                    txtmarca.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("v_marca"))
    '                    txtContvol.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("v_ffcontvolum"))
    '                    txtNSerie.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("V_Numeroserie"))
    '                ElseIf v_tipo = "E" Then
    '                    txtRefContrato.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_Ref_contrato").ToString)
    '                    txtPoContratada.Text = utiles.nullABlanco(dstContadores.Tables("Tablacontadores").Rows(0).Item("E_pot_contratada"))
    '                    txttarifaContratada.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_tarifa_Contratada"))
    '                    txtTDHoraria.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_TipodiscriminacionHoraria"))
    '                    txtComplReactiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_compl_reactiva"))
    '                    txtCT.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_CT_KVA"))
    '                    txtTipoConActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_tipoContActiva"))
    '                    txtNSCActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_NumSerieContActiva"))
    '                    txtNSCReactiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_NumSerieContreactiva"))
    '                    txtICActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_intensidadContactiva"))
    '                    txtTCActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_TensionContactiva"))
    '                    txtCCActivo.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_ConstContactiva"))
    '                    txtFFCActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_FFContactiva"))
    '                    txtFCCActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_FCorrectorContactiva"))
    '                    txtRelacion.Text = utiles.nullABlanco(String.Format("{0:#,##0.000}", dstContadores.Tables("TablaContadores").Rows(0).Item("E_relacionM3_KWH")))
    '                    txtTiporelacion.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_tiporelacionM3_KWH"))
    '                ElseIf v_tipo = "H" Then
    '                    txtMarcaH.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("H_marca"))
    '                    txtModeloH.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("H_Modelo"))
    '                    'IPIM 07052008 Cambiado la columna pq no estaba en la b.d.
    '                    'txtContvolH.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("H_ffcontvolum"))
    '                    txtContvolH.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("H_FFConthora"))
    '                    txtNSerieH.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("H_Numerioserie"))

    '                ElseIf v_tipo = "Q" Then
    '                    txtMarcaQ.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Q_marca"))
    '                    txtContvolQ.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Q_ffcontCaudal"))
    '                    txtNSerieQ.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Q_Numeroserie"))
    '                    txtUnidadesQ.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Q_unidades"))
    '                    txtModeloQ.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Q_modelo"))
    '                End If

    '            End If

    '        Case "borrar"
    '            'EGB 07/05/2008 Agregar Verificación de Registros Relacionados

    '            If conexion.State = ConnectionState.Closed Then conexion.Open()
    '            objTrans = conexion.BeginTransaction()

    '            Try
    '                comando.Transaction = objTrans
    '                'En primer lugar, se eliminan las relaciones que tiene la motobomba
    '                comando.CommandText = "delete from PVYCR_Contadores_ElementosMedida where IdContador='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
    '                comando.ExecuteNonQuery()
    '                'Se elimina la motobomba correspondiente
    '                comando.CommandText = "delete from pvycr_Contadores where idContador='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
    '                comando.ExecuteNonQuery()
    '                objTrans.Commit()
    '                crearDataSets()

    '            Catch borradoNOK As System.Data.SqlClient.SqlException
    '                Alert(Page, "No se puede eliminar el Contador")
    '                objTrans.Rollback()
    '            End Try



    '    End Select
    'End Sub
End Class
