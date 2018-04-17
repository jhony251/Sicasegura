Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.SICA_GestionArboles
Imports GuarderiaFluvial.JavaScript
Imports System.IO
Partial Class SICAH_contadores
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daContadores As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstContadores As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim daElementoContador As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementoContador As DataSet = New System.Data.DataSet()
    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim sentenciaSelHistorial, sentenciaOrderHistorial As String
    Dim daHistorialElemento As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstHistorialElemento As DataSet = New System.Data.DataSet()
    Dim sentenciaHistorialElemento As String


    Dim parfila As Integer = 0
    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ucPaginacion.ruta = "../"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        'scripts de cliente para el calendario
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        'imgFFechaInicial.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaInicial.ClientID & "'),'dd/mm/yyyy');")
        'imgFFechaFin.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaFin.ClientID & "'),'dd/mm/yyyy');")
        imgFFechaRevision.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaRevision.ClientID & "'),'dd/mm/yyyy');")
        lblCabecera.Text = genHTML.cabecera(Page)
        Dim v_tipo As String
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
         
            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))
            imgEM.Attributes.Add("onclick", "desplegarEM(this)")

            'EGB 09/07/2008 Asociación con el Arbol de Cauces --------------------------------------------------------------------
            imgCerrarVentana.Attributes.Add("onclick", "desplegarArbol(this);")
            imgArbol.Attributes.Add("onclick", "desplegarArbol(this);")
            ''EBG Sólo se carga el árbol al dar de alta un nuevo elemento.
            ''---------------------------------------------------------------------------------------------------------------------
            'If treeView1.Nodes.Count = 0 Then
            '    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "") 'EGB el nuevo parametro E hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
            'End If

 
            If Request.QueryString("nuevo") = "yes" Then

                pnlEDContadores.Visible = True
                pnlContadores.Visible = False
                pnlEDcabecera.Visible = True
                activarpaneltipo(Request.QueryString("tipo"))
                txtTipoCont.Text = Request.QueryString("tipo")
                '      ucPaginacion.Visible = False
                lblTitulo.Text = "<b>NUEVO CONTADOR</b>"
                txtidContador.Enabled = True
                txtFecharevision.Enabled = True
                'EBG Sólo se carga el árbol al dar de alta un nuevo elemento.
                '---------------------------------------------------------------------------------------------------------------------
                If treeView1.Nodes.Count = 0 Then
                    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "") 'EGB el nuevo parametro E hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
                End If
                
            Else
                If Request.QueryString("relacionar") = "yes" Then
                    imgArbol.Visible = True

                    imgArbol.Visible = False
                End If
            End If
            ddlTipoCalculo.Visible = VisibleSegunPerfil()
            lblNuevoCont.Visible = VisibleSegunPerfil()
            If Request.QueryString("pag") <> "" Then
                '      ucPaginacion.lblPaginatext = Request.QueryString("pag")
            Else
                '     ucPaginacion.lblPaginatext = "1"
            End If


            crearDataSets()
            rellenarListas()
            'ucPaginacion.DataBind()
            'ucPaginacion.lblNumpaginasDatabind()

            ''RDF
            ''fecha: 0
            ''Como pueden haber contadores Huérfanos, en edición se tiene
            ''que poder asignar un elemento
            ''Con el tiempo este código habrá que quitarlo porque no tendrá sentido
            'If txtElementoMedida.Text = "" Then
            '    imgArbol.Visible = True
            'End If
        End If
    End Sub

    Protected Sub treeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView1.SelectedNodeChanged
        Dim TipoNodo As String
        Dim v() As String
        Try
            v = Split(treeView1.SelectedNode.Value, "#")
            TipoNodo = v(1)
            If TipoNodo = "E" Then
                'lblDesCodigoPVYCR.Text = "Punto de Asociación: " & treeView1.SelectedNode.Text
                txtElementoMedida.Text = v(3) & "-" & v(4)
                If txtidContador.Text = "" Then txtidContador.Text = txtElementoMedida.Text & "-"
            Else

                Alert(Page, "Seleccione un Elemento de Medida para asociar el contador.")
            End If
        Catch mierror As Exception
            'Error en la seleccion del arbol
            Alert(Page, "Seleccione de nuevo el Elemento de Medida, no se registró correctamente")
        End Try
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'If ucPaginacion.lblMuevetext = "si" Then
        ' sentenciaSel = sentenciaSelBase
        'sentenciaQuery = sentenciaSel & "order by idTipo,idSubtipo1,idSubtipo2 "
        'crearDataSets()
        'rptContadores.DataSource = dsts.Tables("TablaTipos")
        'rpttipos.DataBind()
        'ucPaginacion.lblMuevetext = "no"
        '    End If
    End Sub
    Protected Function checkFila(ByVal FechaFinal As String) As String
        parfila = (parfila + 1) Mod 2

        If FechaFinal = "" Then
            Return "class='fila" & parfila & "'  style='color:Maroon' "
        Else
            Return "class=""fila" & parfila & """"
        End If

    End Function
    Private Sub rptContadores_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptContadores.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim elemento As ListItem
        Dim v_tipo As String
        Dim parametros() As String
        Dim objTrans As SqlTransaction

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "relacionar"
                pnlEDcabecera.Visible = False
                pnlContadores.Visible = False
                pnlEDContadores.Visible = False
                pnlEDcabecera.Visible = False
                pnlRelacionarElemento.Visible = True

                pnlRelacion.Visible = False
                lblContadorRel.Text = parametros(0)
                lblFechaRevRel.Text = parametros(1)
                lblCodigoPVYCR.Text = parametros(2)
                lblTituloRel.Text = "<b>ELEMENTOS RELACIONADOS CON EL CONTADOR " & lblContadorRel.Text & " CON FECHA REVISIÓN " & parametros(1) & "</b>"
                'RDF 
                'Fecha: 05/02/2009
                lblHistorial.Text = "<b>Historial de contadores del Punto: " & lblCodigoPVYCR.Text & "</b>"
                crearDataSetsHistorial(parametros(0), parametros(1), parametros(2))
            Case "editar"
                lblContadorSel.Text = parametros(0)
                pnlContadores.Visible = False
                'ucPaginacion.Visible = False
                pnlEDContadores.Visible = True
                pnlEDcabecera.Visible = True
                crearDataSetContador(parametros(0), parametros(1))
                lblTitulo.Text = "<b>CONTADOR: " & lblContadorSel.Text & "</b>"
                If dstContadores.Tables("TablaContadores").Rows.Count > 0 Then
                    'activamos el panel tipo
                    If utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("TipoContador")) = "" Then
                        'obtenemos el tipo del código
                        v_tipo = ObtenerTipoCont(dstContadores.Tables("TablaContadores").Rows(0).Item("idContador").ToString) 'dstContadores.Tables("TablaContadores").Rows(0).Item("idContador").ToString.Substring(dstContadores.Tables("TablaContadores").Rows(0).Item("idContador").ToString.Length - 6, 1)
                        txtTipoCont.Text = v_tipo
                        activarpaneltipo(v_tipo)
                    Else
                        v_tipo = dstContadores.Tables("TablaContadores").Rows(0).Item("TipoContador")
                        txtTipoCont.Text = dstContadores.Tables("TablaContadores").Rows(0).Item("TipoContador")
                        activarpaneltipo(v_tipo)
                    End If
                    '----------cargamos los datos comunes a todos los tipos------------------------------
                    txtidContador.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("idContador"))
                    'como forma parte de la clave primaria no les dejamos modificar estos campos
                    txtidContador.Enabled = False
                    txtCodigoPVYCR.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("CodigoPVYCR"))
                    txtFecharevision.Text = utiles.nullACero(dstContadores.Tables("TablaContadores").Rows(0).Item("FechaRevision"))
                    txtCodigoCaracterizacion.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("CodigoCaracterizacion"))
                    'como forma parte de la clave primaria no les dejamos modificar estos campos
                    txtFecharevision.Enabled = False
                    txtFechaIni.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("FechaInicial"))
                    txtFechaFin.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("FechaFin"))
                    'Campo Descripcion
                    'RDF. Fecha: 10/02/2009
                    txtDescripcion.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Descripcion"))
                    If utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("accesoacontador")) = "SI" Then
                        chkAccesoContador.Checked = True
                    Else
                        chkAccesoContador.Checked = False
                    End If

                    txtAforable.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("aforable"))
                    txtdPtoAForo.Text = utiles.nullACero(dstContadores.Tables("TablaContadores").Rows(0).Item("distanciaptoaforo_km"))
                    '---------aquí cargamos según el tipo------------------------'
                    If v_tipo = "V" Then
                        txtmarca.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("v_marca"))
                        txtContvol.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("v_ffcontvolum"))
                        txtNSerie.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("V_Numeroserie"))
                    ElseIf v_tipo = "E" Then
                        txtRefContrato.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_Ref_contrato").ToString)
                        txtPoContratada.Text = utiles.nullABlanco(dstContadores.Tables("Tablacontadores").Rows(0).Item("E_pot_contratada"))
                        txttarifaContratada.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_tarifa_Contratada"))
                        txtTDHoraria.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_TipodiscriminacionHoraria"))
                        txtComplReactiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_compl_reactiva"))
                        txtCT.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_CT_KVA"))
                        txtTipoConActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_tipoContActiva"))
                        txtNSCActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_NumSerieContActiva"))
                        txtNSCReactiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_NumSerieContreactiva"))
                        txtICActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_intensidadContactiva"))
                        txtTCActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_TensionContactiva"))
                        txtCCActivo.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_ConstContactiva"))
                        txtFFCActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_FFContactiva"))
                        txtFCCActiva.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_FCorrectorContactiva"))
                        txtRelacion.Text = utiles.nullABlanco(String.Format("{0:#,##0.000}", dstContadores.Tables("TablaContadores").Rows(0).Item("E_relacionM3_KWH")))
                        txtTiporelacion.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("E_tiporelacionM3_KWH"))
                    ElseIf v_tipo = "H" Then
                        txtMarcaH.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("H_marca"))
                        txtModeloH.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("H_Modelo"))
                        'IPIM 07052008 Cambiado la columna pq no estaba en la b.d.
                        'txtContvolH.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("H_ffcontvolum"))
                        txtContvolH.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("H_FFConthora"))
                        txtNSerieH.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("H_Numerioserie"))

                    ElseIf v_tipo = "Q" Then
                        txtMarcaQ.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Q_marca"))
                        txtContvolQ.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Q_ffcontCaudal"))
                        txtNSerieQ.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Q_Numeroserie"))
                        txtUnidadesQ.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Q_unidades"))
                        txtModeloQ.Text = utiles.nullABlanco(dstContadores.Tables("TablaContadores").Rows(0).Item("Q_modelo"))
                    End If
                  
                End If

            Case "borrar"
                'EGB 07/05/2008 Agregar Verificación de Registros Relacionados

                If conexion.State = ConnectionState.Closed Then conexion.Open()
                objTrans = conexion.BeginTransaction()

                Try
                    comando.Transaction = objTrans
                    'En primer lugar, se eliminan las relaciones que tiene la motobomba
                    comando.CommandText = "delete from PVYCR_Contadores_ElementosMedida where IdContador='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
                    comando.ExecuteNonQuery()
                    'Se elimina la motobomba correspondiente
                    comando.CommandText = "delete from pvycr_Contadores where idContador='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
                    comando.ExecuteNonQuery()
                    objTrans.Commit()
                    crearDataSets()

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el Contador")
                    objTrans.Rollback()
                End Try



        End Select
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

        sentenciaSel = sentenciaSel & " [idContador],[FechaRevision],[CodigoPVYCR],[FechaInicial],[FechaFin],[TipoContador],[AccesoAContador] " & _
            ",[Aforable],[DistanciaPtoAforo_Km],[Descripcion],[V_NumeroSerie],[V_Marca],[V_FFContVolum],[E_Ref_contrato],[E_Pot_contratada] " & _
            ",[E_Tarifa_contratada],[E_TipoDiscriminacionHoraria],[E_Compl_reactiva],[E_CT_KVA],[E_TipoContActiva],[E_NumSerieContActiva] " & _
            ",[E_NumSerieContReactiva],[E_IntensidadContActiva],[E_TensionContActiva],[E_ConstContActiva],[E_FFContActiva],[E_FCorrectorContActiva] " & _
            ",[E_RelacionM3_KWH],[E_TipoRelacionM3_KWH],[H_NumerioSerie],[H_Marca],[H_Modelo],[H_FFConthora],[Q_NumeroSerie],[Q_Marca] " & _
            ",[Q_Modelo],[Q_Unidades],[Q_FFContCaudal] ,[CodigoCaracterizacion] FROM PVYCR_Contadores "

        If txtFiltroCodigoPVYCR.Text <> "[Código PVYCR]" And txtFiltroCodigoPVYCR.Text <> "" Then
            sFiltro = " where CodigoPVYCR like '" & txtFiltroCodigoPVYCR.Text & "' "
        Else
            sFiltro = ""
        End If
        'RDF 20081001
        If Session("FiltroCont") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroCont")
            Else
                sFiltro = sFiltro + Session("FiltroCont")
            End If
        End If

        sentenciaOrder = " order by idContador "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If
        'paginaAct = ucPaginacion.lblPaginatext
        'If paginaAct < 1 Then paginaAct = 1
        'primerReg = (paginaAct - 1) * ConfigurationManager.AppSettings("registrosPorPag")

        daContadores.SelectCommand.CommandText = sentenciaSel
        daContadores.Fill(dstContadores, "TablaContadores")
        'daContadores.Fill(dstContadores, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaContadores")

        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daContadores.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        'ucPaginacion.calcularPags(txtComando)

        'ncm calculamos el nº de registros que devolverá el filtro 
        txtNumRegFiltrados.Text = FuncionesGenerales.CalcularNumRegFiltrados(Session("FiltroCont"), conexion, sFiltro, "PVYCR_Contadores", Page)

        rptContadores.DataSource = dstContadores.Tables("TablaContadores")
        rptContadores.DataBind()


    End Sub
    Private Sub crearDataSetsHistorial(ByVal idContador As String, ByVal FechaRevision As String, ByVal CodigoPVYCR As String)
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)

        sentenciaSelHistorial = " SELECT  CodigoPVYCR, idElementoMedida, FechaFinal, FechaInicio,idcontador,FechaRevision FROM   PVYCR_Contadores_ElementosMedida "

        If idContador <> "" Then
            sentenciaSelHistorial = sentenciaSelHistorial & " where idcontador = '" & idContador & "' and FechaRevision='" & FechaRevision & "' order by idElementoMedida, FechaInicio DESC, FechaFinal "
        End If

        daElementoContador.SelectCommand.CommandText = sentenciaSelHistorial
        daElementoContador.Fill(dstElementoContador, "TablaElementoContador")
        'daContadores.Fill(dstContadores, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaContadores")


        'ucPaginacion.calcularPags(txtComando)

        rptHistorial.DataSource = dstElementoContador.Tables("TablaElementoContador")
        rptHistorial.DataBind()

        'RDF
        'Fecha: 05/03/2009
        'Se obtiene el historial todos los contadores asociados al Elemento de Medida
        'sentenciaHistorialElemento = "SELECT * FROM PVYCR_Contadores_ElementosMedida where idElementoMedida='" & lblElementoRel.Text & "' AND codigoPVYCR='" & CodigoPVYCR & "' ORDER BY  FechaInicio DESC, FechaFinal DESC"
        sentenciaHistorialElemento = "SELECT * FROM PVYCR_Contadores_ElementosMedida where codigoPVYCR='" & CodigoPVYCR & "' ORDER BY  FechaInicio DESC, FechaFinal "
        daHistorialElemento.SelectCommand.CommandText = sentenciaHistorialElemento
        daHistorialElemento.Fill(dstHistorialElemento, "TablaHistorialElemento")

        rptHistorialElemento.DataSource = dstHistorialElemento.Tables("TablaHistorialElemento")
        rptHistorialElemento.DataBind()

    End Sub
    Private Sub crearDataSetContador(ByVal parametros0 As String, ByVal parametros1 As String)
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daContadores.SelectCommand.CommandText = "select * from PVYCR_contadores where idContador='" & parametros0 & "' and FechaRevision= '" & parametros1 & "' "
        daContadores.Fill(dstContadores, "TablaContadores")
    End Sub

  
    Protected Sub nuevocontador(ByVal sender As Object, ByVal e As System.EventArgs)


    End Sub

    Protected Sub nuevaRelacion(ByVal sender As Object, ByVal e As System.EventArgs)
        pnlRelacion.Visible = True
        txtElementoMedida.Text = ""
        txtFFinRel.Text = ""
        txtFIniRel.Text = ""
        lblElementoRel.Text = ""

    End Sub
 
    'Protected Sub btnFiltroAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptar.Click
    '    'ucPaginacion.lblPaginatext = "1"
    '    crearDataSets()
    '    'rptContadores.DataSource = dstUsuarios.Tables("TablaUsuarios")
    '    'rptUsuarios.DataBind()
    '    'ucPaginacion.lblNumpaginasDatabind()
    'End Sub
    'Protected Sub btnFiltrocancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelar.Click
    '    'ucPaginacion.lblPaginatext = "1"
    '    txtFiltroCodigoPVYCR.Text = "[Código PVYCR]"
    '    crearDataSets()
    '    'ucPaginacion.lblNumpaginasDatabind()
    'End Sub
    Protected Function ObtenerNumSerie(ByVal elDataitem As DataRowView)
        If utiles.nullABlanco(elDataitem("TipoContador")) <> "" Then

            If elDataitem("TipoContador") = "V" Then
                'ncm 14/03/2011 comentado tras la peticion de poner filtros dinámicos
                'Return elDataitem("V_NumeroSerie") & "<td>" & elDataitem("V_Marca") & "</td>"
                Return elDataitem("V_Marca")
            ElseIf elDataitem("TipoContador") = "E" Then
                'Return elDataitem("E_NumSerieContActiva") & "<td>" & elDataitem("E_TipoContActiva") & "</td>"
                Return elDataitem("E_TipoContActiva")
            ElseIf elDataitem("TipoContador") = "H" Then
                'Return elDataitem("H_NumerioSerie") & "<td> " & elDataitem("H_Marca") & " </td>"
                Return elDataitem("H_Marca")
            ElseIf elDataitem("TipoContador") = "Q" Then
                'Return elDataitem("Q_NumeroSerie") & "<td> &nbsp; </td>"
                Return "&nbsp;"
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function
    Protected Sub activarpaneltipo(ByVal tipo As String)
        If tipo = "E" Then
            pnlEDTipoE.Visible = True
            pnlEDTipoV.Visible = False
            pnlEDtipoH.Visible = False
            pnlEDTipoq.Visible = False
        ElseIf tipo = "V" Then
            pnlEDTipoE.Visible = False
            pnlEDTipoV.Visible = True
            pnlEDtipoH.Visible = False
            pnlEDTipoQ.Visible = False
        ElseIf tipo = "H" Then
            pnlEDTipoE.Visible = False
            pnlEDTipoV.Visible = False
            pnlEDtipoH.Visible = True
            pnlEDTipoQ.Visible = False
        ElseIf tipo = "Q" Then
            pnlEDTipoE.Visible = False
            pnlEDTipoV.Visible = False
            pnlEDtipoH.Visible = False
            pnlEDTipoQ.Visible = True
        End If
    End Sub

    Protected Sub btnAceptarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarE.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        Try
            If lblContadorSel.Text = "" Then
                'Insertamos un nuevo usuario
                comando.CommandText = "insert into PVYCR_Contadores([idContador],[CodigoPVYCR],[FechaRevision],[FechaInicial]" & _
               ",[FechaFin],[TipoContador],[AccesoAContador],[Aforable],[DistanciaPtoAforo_Km],[Descripcion]" & _
               ",[E_Ref_contrato],[E_Pot_contratada],[E_Tarifa_contratada],[E_TipoDiscriminacionHoraria],[E_Compl_reactiva]" & _
               ",[E_CT_KVA],[E_TipoContActiva],[E_NumSerieContActiva],[E_NumSerieContReactiva],[E_IntensidadContActiva],[E_TensionContActiva]" & _
               ",[E_ConstContActiva],[E_FFContActiva],[E_FCorrectorContActiva],[E_RelacionM3_KWH],[E_TipoRelacionM3_KWH],[codigocaracterizacion]) " & _
               "values " & _
               "(@idContador,@CodigoPVYCR,@FechaRevision,@FechaInicial,@FechaFin,@TipoContador,@AccesoAContador,@Aforable,@DistanciaPtoAforo_Km,@Descripcion " & _
               ",@E_Ref_contrato,@E_Pot_contratada,@E_Tarifa_contratada,@E_TipoDiscriminacionHoraria,@E_Compl_reactiva" & _
               ",@E_CT_KVA,@E_TipoContActiva,@E_NumSerieContActiva,@E_NumSerieContReactiva,@E_IntensidadContActiva,@E_TensionContActiva" & _
               ",@E_ConstContActiva,@E_FFContActiva,@E_FCorrectorContActiva,@E_RelacionM3_KWH,@E_TipoRelacionM3_KWH,@codigocaracterizacion) "
                comando.Parameters.AddWithValue("idContador", utiles.BlancoANull(txtidContador.Text))
                comando.Parameters.AddWithValue("FechaRevision", utiles.BlancoANull(txtFecharevision.Text))
            Else
                comando.CommandText = "update PVYCR_Contadores set CodigoPVYCR = @CodigoPVYCR " & _
                ",FechaInicial = @FechaInicial,FechaFin = @FechaFin,TipoContador = @TipoContador " & _
                ",AccesoAContador = @AccesoAContador,Aforable = @Aforable,DistanciaPtoAforo_Km = @DistanciaPtoAforo_Km, Descripcion=@Descripcion " & _
                ",E_Ref_contrato = @E_Ref_contrato " & _
                ",E_Pot_contratada = @E_Pot_contratada,E_Tarifa_contratada = @E_Tarifa_contratada,E_TipoDiscriminacionHoraria = @E_TipoDiscriminacionHoraria " & _
                ",E_Compl_reactiva = @E_Compl_reactiva,E_CT_KVA = @E_CT_KVA,E_TipoContActiva = @E_TipoContActiva,E_NumSerieContActiva = @E_NumSerieContActiva " & _
                ",E_NumSerieContReactiva = @E_NumSerieContReactiva,E_IntensidadContActiva = @E_IntensidadContActiva,E_TensionContActiva = @E_TensionContActiva " & _
                ",E_ConstContActiva = @E_ConstContActiva,E_FFContActiva = @E_FFContActiva,E_FCorrectorContActiva = @E_FCorrectorContActiva " & _
                ",E_RelacionM3_KWH = @E_RelacionM3_KWH,E_TipoRelacionM3_KWH = @E_TipoRelacionM3_KWH, codigocaracterizacion = @codigocaracterizacion " & _
                "where idContador='" & lblContadorSel.Text & "' "
            End If
 

            comando.Parameters.AddWithValue("CodigoPVYCR", utiles.BlancoANull(txtCodigoPVYCR.Text))
            comando.Parameters.AddWithValue("FechaInicial", utiles.BlancoANull(txtFechaIni.Text)) 'para que no meta una fecha mñinima cuando el campo es nulo (que al ser un textbox es "")
            comando.Parameters.AddWithValue("FechaFin", utiles.BlancoANull(txtFechaFin.Text))
            comando.Parameters.AddWithValue("TipoContador", utiles.BlancoANull(txtTipoCont.Text))
            If chkAccesoContador.Checked Then
                comando.Parameters.AddWithValue("AccesoAContador", "SI")
            Else
                comando.Parameters.AddWithValue("AccesoAContador", "NO")
            End If

            comando.Parameters.AddWithValue("Aforable", utiles.BlancoANull(txtAforable.Text))
            comando.Parameters.AddWithValue("DistanciaPtoAforo_Km", utiles.BlancoANull(Replace(Me.txtdPtoAForo.Text, ",", ".")))
            comando.Parameters.AddWithValue("Descripcion", utiles.BlancoANull(txtDescripcion.Text))
            comando.Parameters.AddWithValue("E_Ref_contrato", utiles.BlancoANull(txtRefContrato.Text))
            comando.Parameters.AddWithValue("E_Pot_contratada", utiles.BlancoANull(txtPoContratada.Text))
            comando.Parameters.AddWithValue("E_Tarifa_contratada", utiles.BlancoANull(txttarifaContratada.Text))
            comando.Parameters.AddWithValue("E_TipoDiscriminacionHoraria", utiles.BlancoANull(txtTDHoraria.Text))
            comando.Parameters.AddWithValue("E_Compl_reactiva", utiles.BlancoANull(txtComplReactiva.Text))
            comando.Parameters.AddWithValue("E_CT_KVA", utiles.BlancoANull(txtCT.Text))
            comando.Parameters.AddWithValue("E_TipoContActiva", utiles.BlancoANull(txtTipoConActiva.Text))
            comando.Parameters.AddWithValue("E_NumSerieContActiva", utiles.BlancoANull(txtNSCActiva.Text))
            comando.Parameters.AddWithValue("E_NumSerieContReactiva", utiles.BlancoANull(txtNSCReactiva.Text))
            comando.Parameters.AddWithValue("E_IntensidadContActiva", utiles.BlancoANull(txtICActiva.Text))
            comando.Parameters.AddWithValue("E_TensionContActiva", utiles.BlancoANull(txtTCActiva.Text))
            comando.Parameters.AddWithValue("E_ConstContActiva", utiles.BlancoANull(txtCCActivo.Text))
            comando.Parameters.AddWithValue("E_FFContActiva", utiles.BlancoANull(txtFFCActiva.Text))
            comando.Parameters.AddWithValue("E_FCorrectorContActiva", utiles.BlancoANull(txtFCCActiva.Text))
            comando.Parameters.AddWithValue("E_RelacionM3_KWH", utiles.BlancoANull(txtRelacion.Text.Replace(",", ".")))
            comando.Parameters.AddWithValue("E_TipoRelacionM3_KWH", utiles.BlancoANull(txtTiporelacion.Text))
            comando.Parameters.AddWithValue("codigocaracterizacion", utiles.BlancoANull(txtCodigoCaracterizacion.Text))

            comando.ExecuteNonQuery()
            'ncm 15/06/2009
            If txtFechaFin.Text <> "" Then
                BajaContadoreselementosmedida(txtidContador.Text, txtFecharevision.Text)
            End If

            If lblContadorSel.Text = "" Then
                'RDF
                'Fecha: 10/02/2009
                AltaAutomaticaRelacionElemento(txtidContador.Text, txtFecharevision.Text, txtFechaIni.Text)
            End If
        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                'Case 547
                '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    Alert(Page, "El contador ya existe para la fecha de revision: " & txtFecharevision.Text)
            End Select
        End Try
        lblContadorSel.Text = ""
        pnlContadores.Visible = True
        'ucPaginacion.Visible = True
        pnlEDContadores.Visible = False
        pnlEDTipoE.Visible = False

        txtidContador.Text = ""
        txtCodigoPVYCR.Text = ""
        txtFecharevision.Text = ""
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        chkAccesoContador.Checked = False
        txtAforable.Text = ""
        txtdPtoAForo.Text = ""
        txtDescripcion.Text = ""
        txtmarca.Text = ""
        txtContvol.Text = ""
        txtRefContrato.Text = ""
        txtPoContratada.Text = ""
        txttarifaContratada.Text = ""
        txtTDHoraria.Text = ""
        txtComplReactiva.Text = ""
        txtCT.Text = ""
        txtTipoConActiva.Text = ""
        txtNSCActiva.Text = ""
        txtNSCReactiva.Text = ""
        txtICActiva.Text = ""
        txtTCActiva.Text = ""
        txtCCActivo.Text = ""
        txtFFCActiva.Text = ""
        txtFCCActiva.Text = ""
        txtRelacion.Text = ""
        txtTiporelacion.Text = ""
        txtCodigoCaracterizacion.Text = ""



        crearDataSets()
        rptContadores.DataSource = dstContadores.Tables("Tablacontadores")
        rptContadores.DataBind()
        'ucPaginacion.lblNumpaginasDatabind()

    End Sub

    Protected Sub btnCancelarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarE.Click
        lblContadorSel.Text = ""
        pnlContadores.Visible = True
        'ucPaginacion.Visible = True
        pnlEDTipoE.Visible = False
        pnlEDContadores.Visible = False

        txtidContador.Text = ""
        txtCodigoPVYCR.Text = ""
        txtFecharevision.Text = ""
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        chkAccesoContador.Checked = False
        txtAforable.Text = ""
        txtdPtoAForo.Text = ""
        txtDescripcion.Text = ""
        txtRefContrato.Text = ""
        txtPoContratada.Text = ""
        txttarifaContratada.Text = ""
        txtTDHoraria.Text = ""
        txtComplReactiva.Text = ""
        txtCT.Text = ""
        txtTipoConActiva.Text = ""
        txtNSCActiva.Text = ""
        txtNSCReactiva.Text = ""
        txtICActiva.Text = ""
        txtTCActiva.Text = ""
        txtCCActivo.Text = ""
        txtFFCActiva.Text = ""
        txtFCCActiva.Text = ""
        txtRelacion.Text = ""
        txtTiporelacion.Text = ""
        txtCodigoCaracterizacion.Text = ""
    End Sub

    Protected Sub btnAceptarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarV.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        Try
            If lblContadorSel.Text = "" Then
                'Insertamos un nuevo usuario
                comando.CommandText = "insert into PVYCR_Contadores([idContador],[CodigoPVYCR],[FechaRevision],[FechaInicial]" & _
               ",[FechaFin],[TipoContador],[AccesoAContador],[Aforable],[DistanciaPtoAforo_Km],[Descripcion],[V_NumeroSerie],[V_Marca]" & _
               ",[V_FFContVolum], [codigocaracterizacion]) " & _
               "values " & _
               "(@idContador,@CodigoPVYCR,@FechaRevision,@FechaInicial,@FechaFin,@TipoContador,@AccesoAContador,@Aforable,@DistanciaPtoAforo_Km, @Descripcion," & _
               "@V_NumeroSerie,@V_Marca,@V_FFContVolum,@CodigoCaracterizacion) "
                comando.Parameters.AddWithValue("idContador", utiles.BlancoANull(txtidContador.Text))
                comando.Parameters.AddWithValue("FechaRevision", utiles.BlancoANull(txtFecharevision.Text))
            Else
                comando.CommandText = "update PVYCR_Contadores set CodigoPVYCR = @CodigoPVYCR " & _
                ",FechaInicial = @FechaInicial,FechaFin = @FechaFin,TipoContador = @TipoContador " & _
                ",AccesoAContador = @AccesoAContador,Aforable = @Aforable,DistanciaPtoAforo_Km = @DistanciaPtoAforo_Km, Descripcion=@Descripcion " & _
                ",V_NumeroSerie = @V_NumeroSerie,V_Marca = @V_Marca,V_FFContVolum = @V_FFContVolum,CodigoCaracterizacion = @CodigoCaracterizacion " & _
                "where idContador='" & lblContadorSel.Text & "' "
            End If


            comando.Parameters.AddWithValue("CodigoPVYCR", utiles.BlancoANull(txtCodigoPVYCR.Text))
            comando.Parameters.AddWithValue("FechaInicial", utiles.BlancoANull(txtFechaIni.Text)) 'para que no meta una fecha mñinima cuando el campo es nulo (que al ser un textbox es "")
            comando.Parameters.AddWithValue("FechaFin", utiles.BlancoANull(txtFechaFin.Text))
            comando.Parameters.AddWithValue("TipoContador", utiles.BlancoANull(txtTipoCont.Text))
            If chkAccesoContador.Checked Then
                comando.Parameters.AddWithValue("AccesoAContador", "SI")
            Else
                comando.Parameters.AddWithValue("AccesoAContador", "NO")
            End If
            comando.Parameters.AddWithValue("Aforable", utiles.BlancoANull(txtAforable.Text))
            comando.Parameters.AddWithValue("DistanciaPtoAforo_Km", utiles.BlancoANull(Replace(Me.txtdPtoAForo.Text, ",", ".")))
            comando.Parameters.AddWithValue("Descripcion", utiles.BlancoANull(txtDescripcion.Text))
            comando.Parameters.AddWithValue("V_NumeroSerie", utiles.BlancoANull(txtNSerie.Text))
            comando.Parameters.AddWithValue("V_Marca", utiles.BlancoANull(txtmarca.Text))
            comando.Parameters.AddWithValue("V_FFContVolum", utiles.BlancoANull(txtContvol.Text))
            comando.Parameters.AddWithValue("CodigoCaracterizacion", utiles.BlancoANull(txtCodigoCaracterizacion.Text))
            comando.ExecuteNonQuery()
            'ncm 15/06/2009
            If txtFechaFin.Text <> "" Then
                BajaContadoresElementosMedida(txtidContador.Text, txtFecharevision.Text)
            End If

            If lblContadorSel.Text = "" Then
                'RDF
                'Fecha: 10/02/2009
                AltaAutomaticaRelacionElemento(txtidContador.Text, txtFecharevision.Text, txtFechaIni.Text)
            End If
        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                'Case 547
                '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    Alert(Page, "El contador ya existe para la fecha de revision: " & txtFecharevision.Text)
            End Select
        End Try
        lblContadorSel.Text = ""
        pnlContadores.Visible = True
        'ucPaginacion.Visible = True
        pnlEDContadores.Visible = False

        txtidContador.Text = ""
        txtCodigoPVYCR.Text = ""
        txtFecharevision.Text = ""
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        chkAccesoContador.Checked = False
        txtAforable.Text = ""
        txtdPtoAForo.Text = ""
        txtDescripcion.Text = ""
        txtmarca.Text = ""
        txtContvol.Text = ""
        txtNSerie.Text = ""
        txtCodigoCaracterizacion.Text = ""

        crearDataSets()
        rptContadores.DataSource = dstContadores.Tables("Tablacontadores")
        rptContadores.DataBind()
        'ucPaginacion.lblNumpaginasDatabind()

    End Sub

    Protected Sub btnCancelarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarV.Click
        lblContadorSel.Text = ""
        pnlContadores.Visible = True
        'ucPaginacion.Visible = True
        pnlEDTipoE.Visible = False
        pnlEDContadores.Visible = False

        txtidContador.Text = ""
        txtCodigoPVYCR.Text = ""
        txtFecharevision.Text = ""
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        chkAccesoContador.Checked = False
        txtAforable.Text = ""
        txtdPtoAForo.Text = ""
        txtDescripcion.Text = ""
        txtmarca.Text = ""
        txtContvol.Text = ""
        txtNSerie.Text = ""
        txtCodigoCaracterizacion.Text = ""
    End Sub

    Protected Sub btnAceptarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarH.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        Try
            If lblContadorSel.Text = "" Then
                'Insertamos un nuevo usuario
                comando.CommandText = "insert into PVYCR_Contadores([idContador],[CodigoPVYCR],[FechaRevision],[FechaInicial]" & _
               ",[FechaFin],[TipoContador],[AccesoAContador],[Aforable],[DistanciaPtoAforo_Km],[Descripcion],[H_NumerioSerie],[H_Marca],[H_Modelo]" & _
               ",[H_FFContHora], [codigocaracterizacion]) " & _
               "values " & _
               "(@idContador,@CodigoPVYCR,@FechaRevision,@FechaInicial,@FechaFin,@TipoContador,@AccesoAContador,@Aforable,@DistanciaPtoAforo_Km, @Descripcion," & _
               "@H_NumeroSerie,@H_Marca,@H_Modelo,@H_FFConthORA,@CodigoCaracterizacion) "
                comando.Parameters.AddWithValue("idContador", utiles.BlancoANull(txtidContador.Text))
                comando.Parameters.AddWithValue("FechaRevision", utiles.BlancoANull(txtFecharevision.Text))
            Else
                comando.CommandText = "update PVYCR_Contadores set CodigoPVYCR = @CodigoPVYCR " & _
                ",FechaInicial = @FechaInicial,FechaFin = @FechaFin,TipoContador = @TipoContador " & _
                ",AccesoAContador = @AccesoAContador,Aforable = @Aforable,DistanciaPtoAforo_Km = @DistanciaPtoAforo_Km,Descripcion=@Descripcion " & _
                ",H_NumerioSerie = @H_NumeroSerie,H_Marca = @H_Marca,H_Modelo=@H_Modelo,H_FFContHora = @H_FFContHora,CodigoCaracterizacion = @CodigoCaracterizacion " & _
                "where idContador='" & lblContadorSel.Text & "' "
            End If


            comando.Parameters.AddWithValue("CodigoPVYCR", utiles.BlancoANull(txtCodigoPVYCR.Text))
            comando.Parameters.AddWithValue("FechaInicial", utiles.BlancoANull(txtFechaIni.Text)) 'para que no meta una fecha mñinima cuando el campo es nulo (que al ser un textbox es "")
            comando.Parameters.AddWithValue("FechaFin", utiles.BlancoANull(txtFechaFin.Text))
            comando.Parameters.AddWithValue("TipoContador", utiles.BlancoANull(txtTipoCont.Text))
            If chkAccesoContador.Checked Then
                comando.Parameters.AddWithValue("AccesoAContador", "SI")
            Else
                comando.Parameters.AddWithValue("AccesoAContador", "NO")
            End If
            comando.Parameters.AddWithValue("Aforable", utiles.BlancoANull(txtAforable.Text))
            comando.Parameters.AddWithValue("DistanciaPtoAforo_Km", utiles.BlancoANull(Replace(Me.txtdPtoAForo.Text, ",", ".")))
            comando.Parameters.AddWithValue("Descripcion", utiles.BlancoANull(txtDescripcion.Text))
            comando.Parameters.AddWithValue("H_NumeroSerie", utiles.BlancoANull(txtNSerieH.Text))
            comando.Parameters.AddWithValue("H_Marca", utiles.BlancoANull(txtMarcaH.Text))
            comando.Parameters.AddWithValue("H_Modelo", utiles.BlancoANull(txtModeloH.Text))
            comando.Parameters.AddWithValue("H_FFContHora", utiles.BlancoANull(txtContvolH.Text))
            comando.Parameters.AddWithValue("CodigoCaracterizacion", utiles.BlancoANull(txtCodigoCaracterizacion.Text))
            comando.ExecuteNonQuery()
            'ncm 15/06/2009
            If txtFechaFin.Text <> "" Then
                BajaContadoresElementosMedida(txtidContador.Text, txtFecharevision.Text)
            End If

            If lblContadorSel.Text = "" Then
                'RDF
                'Fecha: 10/02/2009
                AltaAutomaticaRelacionElemento(txtidContador.Text, txtFecharevision.Text, txtFechaIni.Text)
            End If
        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                'Case 547
                '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    Alert(Page, "El contador ya existe para la fecha de revision: " & txtFecharevision.Text)
            End Select
        End Try
        lblContadorSel.Text = ""
        pnlContadores.Visible = True
        'ucPaginacion.Visible = True
        pnlEDContadores.Visible = False

        txtidContador.Text = ""
        txtCodigoPVYCR.Text = ""
        txtFecharevision.Text = ""
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        chkAccesoContador.Checked = False
        txtAforable.Text = ""
        txtdPtoAForo.Text = ""
        txtDescripcion.Text = ""
        txtMarcaH.Text = ""
        txtModeloH.Text = ""
        txtContvolH.Text = ""
        txtNSerieH.Text = ""
        txtCodigoCaracterizacion.Text = ""

        crearDataSets()
        rptContadores.DataSource = dstContadores.Tables("Tablacontadores")
        rptContadores.DataBind()
        'ucPaginacion.lblNumpaginasDatabind()

    End Sub

    Protected Sub btnCancelarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarH.Click
        lblContadorSel.Text = ""
        pnlContadores.Visible = True
        'ucPaginacion.Visible = True
        pnlEDTipoE.Visible = False
        pnlEDContadores.Visible = False

        txtidContador.Text = ""
        txtCodigoPVYCR.Text = ""
        txtFecharevision.Text = ""
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        chkAccesoContador.Checked = False
        txtAforable.Text = ""
        txtdPtoAForo.Text = ""
        txtDescripcion.Text = ""
        txtMarcaH.Text = ""
        txtModeloH.Text = ""
        txtContvolH.Text = ""
        txtNSerieH.Text = ""
        txtCodigoCaracterizacion.Text = ""
    End Sub

    Protected Sub btnAceptarQ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarQ.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        Try
            If lblContadorSel.Text = "" Then
                'Insertamos un nuevo usuario
                comando.CommandText = "insert into PVYCR_Contadores([idContador],[CodigoPVYCR],[FechaRevision],[FechaInicial]" & _
               ",[FechaFin],[TipoContador],[AccesoAContador],[Aforable],[DistanciaPtoAforo_Km],[Descripcion],[q_NumeroSerie],[q_Marca]" & _
               ",[q_FFContCaudal], [q_modelo],[q_unidades],[codigocaracterizacion]) " & _
               "values " & _
               "(@idContador,@CodigoPVYCR,@FechaRevision,@FechaInicial,@FechaFin,@TipoContador,@AccesoAContador,@Aforable,@DistanciaPtoAforo_Km,@Descripcion, " & _
               "@Q_NumeroSerie,@Q_Marca,@Q_FFContCaudal, @Modelo,@Q_unidades,@CodigoCaracterizacion) "
                comando.Parameters.AddWithValue("idContador", utiles.BlancoANull(txtidContador.Text))
                comando.Parameters.AddWithValue("FechaRevision", utiles.BlancoANull(txtFecharevision.Text))
            Else
                comando.CommandText = "update PVYCR_Contadores set CodigoPVYCR = @CodigoPVYCR " & _
                ",FechaInicial = @FechaInicial,FechaFin = @FechaFin,TipoContador = @TipoContador " & _
                ",AccesoAContador = @AccesoAContador,Aforable = @Aforable,DistanciaPtoAforo_Km = @DistanciaPtoAforo_Km,Descripcion=@Descripcion " & _
                ",Q_NumeroSerie = @Q_NumeroSerie,Q_Marca = @Q_Marca, Q_Modelo=@Q_Modelo, Q_Unidades = @Q_Unidades,Q_FFContCaudal = @Q_FFContCaudal,CodigoCaracterizacion = @CodigoCaracterizacion " & _
                "where idContador='" & lblContadorSel.Text & "' "
            End If


            comando.Parameters.AddWithValue("CodigoPVYCR", utiles.BlancoANull(txtCodigoPVYCR.Text))
            comando.Parameters.AddWithValue("FechaInicial", utiles.BlancoANull(txtFechaIni.Text)) 'para que no meta una fecha mñinima cuando el campo es nulo (que al ser un textbox es "")
            comando.Parameters.AddWithValue("FechaFin", utiles.BlancoANull(txtFechaFin.Text))
            comando.Parameters.AddWithValue("TipoContador", utiles.BlancoANull(txtTipoCont.Text))
            If chkAccesoContador.Checked Then
                comando.Parameters.AddWithValue("AccesoAContador", "SI")
            Else
                comando.Parameters.AddWithValue("AccesoAContador", "NO")
            End If
            comando.Parameters.AddWithValue("Aforable", utiles.BlancoANull(txtAforable.Text))
            comando.Parameters.AddWithValue("DistanciaPtoAforo_Km", utiles.BlancoANull(Replace(Me.txtdPtoAForo.Text, ",", ".")))
            comando.Parameters.AddWithValue("Descripcion", utiles.BlancoANull(txtDescripcion.Text))
            comando.Parameters.AddWithValue("Q_NumeroSerie", utiles.BlancoANull(txtNSerieQ.Text))
            comando.Parameters.AddWithValue("Q_Marca", utiles.BlancoANull(txtMarcaQ.Text))
            comando.Parameters.AddWithValue("Q_FFContCaudal", utiles.BlancoANull(txtContvolQ.Text))
            comando.Parameters.AddWithValue("Q_MODELO", utiles.BlancoANull(txtModeloQ.Text))
            comando.Parameters.AddWithValue("Q_unidades", utiles.BlancoANull(txtUnidadesQ.Text))
            comando.Parameters.AddWithValue("Modelo", utiles.BlancoANull(txtModeloQ.Text))
            comando.Parameters.AddWithValue("CodigoCaracterizacion", utiles.BlancoANull(txtCodigoCaracterizacion.Text))
            comando.ExecuteNonQuery()
            'ncm 15/06/2009
            If txtFechaFin.Text <> "" Then
                BajaContadoresElementosMedida(txtidContador.Text, txtFecharevision.Text)
            End If

            If lblContadorSel.Text = "" Then
                'RDF
                'Fecha: 10/02/2009
                AltaAutomaticaRelacionElemento(txtidContador.Text, txtFecharevision.Text, txtFechaIni.Text)
            End If
        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                'Case 547
                '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    Alert(Page, "El contador ya existe para la fecha de revision: " & txtFecharevision.Text)
            End Select
        End Try
        lblContadorSel.Text = ""
        pnlContadores.Visible = True
        'ucPaginacion.Visible = True
        pnlEDContadores.Visible = False

        txtidContador.Text = ""
        txtCodigoPVYCR.Text = ""
        txtFecharevision.Text = ""
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        chkAccesoContador.Checked = False
        txtAforable.Text = ""
        txtDescripcion.Text = ""
        txtdPtoAForo.Text = ""
        txtMarcaH.Text = ""
        txtContvolH.Text = ""
        txtNSerieH.Text = ""
        txtCodigoCaracterizacion.Text = ""

        crearDataSets()
        rptContadores.DataSource = dstContadores.Tables("Tablacontadores")
        rptContadores.DataBind()
        'ucPaginacion.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnCancelarQ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarQ.Click
        lblContadorSel.Text = ""
        pnlContadores.Visible = True
        'ucPaginacion.Visible = True
        pnlEDTipoE.Visible = False
        pnlEDContadores.Visible = False

        txtidContador.Text = ""
        txtCodigoPVYCR.Text = ""
        txtFecharevision.Text = ""
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        chkAccesoContador.Checked = False
        txtAforable.Text = ""
        txtdPtoAForo.Text = ""
        txtDescripcion.Text = ""
        txtMarcaQ.Text = ""
        txtContvolQ.Text = ""
        txtNSerieQ.Text = ""
        txtModeloQ.Text = ""
        txtUnidadesQ.Text = ""
        txtCodigoCaracterizacion.Text = ""
    End Sub
    Protected Function ObtenerTipoCont(ByVal idcontador As String)
        'obtenemos el tipo del código
        Return idcontador.ToString.Substring(idcontador.Length - 6, 1)
    End Function


    Protected Sub ddlTipoCalculo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoCalculo.SelectedIndexChanged
        lblContadorSel.Text = ""
        'Response.Redirect("Contadores.aspx?nuevo=yes&tipo=" & txtSelTipo.Text)
        'EGB Correccion 07052008
        If ddlTipoCalculo.SelectedItem.Value = "0" Then
            Alert(Page, "Seleccione un Tipo de Contador")
        Else
            Response.Redirect("Contadores.aspx?nuevo=yes&tipo=" & ddlTipoCalculo.SelectedItem.Value)
        End If

    End Sub

    Protected Sub btnListarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarE.Click
        Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
                      "window.open('../listados/ListadoContadorE.aspx?idContador=" & txtidContador.Text & "&fechaRevision=" & txtFecharevision.Text & "')" & _
                     "</script>")

    End Sub

    Protected Sub btnListarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarV.Click
        Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
                         "window.open('../listados/ListadoContadorV.aspx?idContador=" & txtidContador.Text & "&fechaRevision=" & txtFecharevision.Text & "')" & _
                        "</script>")
    End Sub

    Protected Sub btnListadoPantalla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListadoPantalla.Click
        Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
                           "window.open('../listados/ListadoContadorPantalla.aspx?codigoPVYCR=" & txtFiltroCodigoPVYCR.Text & "')" & _
                          "</script>")
    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub AceptarFiltro(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20080930
        Dim strFiltro As String
        strFiltro = ""
        If txtFidcontador.Text <> "" Then
            strFiltro = " AND idContador like '" + txtFidcontador.Text + "' "
        End If

        If txtCodigoPVYCR.Text <> "" Then
            strFiltro = " AND CodigoPVYCR like '" + txtCodigoPVYCR.Text + "' "
        End If

        'If txtFFechaFin.Text <> "" Then
        '    strFiltro = strFiltro + " AND  FechaFin between '" + txtFFechaFin.Text + " 00:00:00' and '" + txtFFechaFin.Text + " 23:59:59' "
        'End If

        'If txtFFechaInicial.Text <> "" Then
        '    strFiltro = strFiltro + " AND  FechaInicial between '" + txtFFechaInicial.Text + " 00:00:00' and '" + txtFFechaInicial.Text + " 23:59:59' "
        'End If

        If txtFFechaRevision.Text <> "" Then
            strFiltro = strFiltro + " AND FechaRevision between '" + txtFFechaRevision.Text + " 00:00:00' and '" + txtFFechaRevision.Text + " 23:59:59' "
        End If

        'If txtFCodCaract.Text <> "" Then
        '    strFiltro = strFiltro + " AND CodigoCaracterizacion like '" + txtFCodCaract.Text + "' "
        'End If

        'If txtFNserieActiva.Text <> "" Then
        '    strFiltro = strFiltro + " AND E_NumSerieContActiva like '" + txtFNserieActiva.Text + "' "
        'End If

        If txtFNSerieReactiva.Text <> "" Then
            strFiltro = strFiltro + " AND (E_TipoContActiva like '" + txtFNSerieReactiva.Text + "' OR V_Marca like '" + txtFNSerieReactiva.Text + "' OR H_Marca like '" + txtFNSerieReactiva.Text + "') "
        End If

        If txtFiltrado.Text <> "" Then
            If ddlFiltrado.SelectedValue = "FechaInicial" Or ddlFiltrado.SelectedValue = "FechaFin" Then

                strFiltro = strFiltro + " AND convert(varchar(10)," + ddlFiltrado.SelectedValue + ",103) LIKE '" + txtFiltrado.Text + "'"

            Else
                strFiltro = strFiltro + " AND " + ddlFiltrado.SelectedValue + " LIKE '" + txtFiltrado.Text + "'"
            End If
        End If

        Session("FiltroCont") = strFiltro
        crearDataSets()
    End Sub

   
    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        'RDF 
        '29/10/2008
        crearDataSets()
    End Sub
    Private Function GetDataSetArbol() As DataSet
        'EGB. El arbol debe cargarse a partir de los datos de PVYCR_Arbol para no regenerar el XML en cada actualización

        crearDataSetsPuntosXML2()
        Return dstarbol
        'Session("DSLoaded") = 1

    End Function

    Private Sub crearDataSetsPuntosXML2()
        Try
            dstarbol.Clear()
            treeView1.Nodes.Clear()

            'ARBOL NORMALIZADO
            daContadores.SelectCommand.CommandText = "Select * from dbo.PVYCR_ARBOL"
            daContadores.Fill(dstarbol, "tablaArbol")

            dstarbol.Relations.Add("SelfRefenceRelation", _
                            dstarbol.Tables("tablaArbol").Columns("IdArbol"), _
                            dstarbol.Tables("tablaArbol").Columns("IdArbolPadre"), True)
            dstarbol.Relations("SelfRefenceRelation").Nested = True

        Catch exp As Exception
        End Try
    End Sub
    
    Protected Sub btnAnyadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnyadir.Click

        'RDF
        'Fecha: 06/02/2009
        'Se da de alta el registro de PVYCR_Contadores_ElementosMedida
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)


        Try
            Dim NumReg As Integer
            Dim idElemento As String

            If (txtElementoMedida.Text <> "" And lblContadorRel.Text <> "" And lblElementoRel.Text = "") Then
                idElemento = lblContadorRel.Text.Replace(lblCodigoPVYCR.Text & "-", "")
                idElemento = idElemento.Substring(0, 3)

                If txtElementoMedida.Text.IndexOf(idElemento, 0) > 0 Then
                    'RDF. Fecha: 04/03/2009
                    'Se comprueba que no hayan más registros con fechafin a null
                    comandoAux.CommandText = "SELECT COUNT(*) FROM PVYCR_Contadores_ElementosMedida WHERE CodigoPVYCR='" & lblCodigoPVYCR.Text & "' AND " & _
                                        " IdElementoMedida='" & idElemento & "' AND FechaFinal is null"
                    NumReg = comandoAux.ExecuteScalar()

                    If NumReg = 0 Or txtFFinRel.Text <> "" Then
                        'Insertamos un nuevo usuario
                        comando.CommandText = "insert into [PVYCR_Contadores_ElementosMedida]([CodigoPVYCR],[idElementoMedida],[idContador],[FechaRevision],[FechaInicio],[FechaFinal]) " & _
                       " VALUES  " & _
                       " ('" & txtElementoMedida.Text.Substring(0, txtElementoMedida.Text.Length - 4) & "','" & txtElementoMedida.Text.Replace(txtElementoMedida.Text.Substring(0, txtElementoMedida.Text.Length - 3), "") & "','" & lblContadorRel.Text & "','" & lblFechaRevRel.Text & "'," & IIf(txtFIniRel.Text = "", "null", "'" & txtFIniRel.Text & "'") & "," & IIf(txtFFinRel.Text = "", "null", "'" & txtFFinRel.Text & "'") & ")"
                        comando.ExecuteNonQuery()
                        crearDataSetsHistorial(lblContadorRel.Text, lblFechaRevRel.Text, lblCodigoPVYCR.Text)
                        pnlRelacion.Visible = False

                    Else
                        Alert(Page, "Revise los datos. El Elemento de Medida tiene asociado un Contador sin finalizar")
                    End If
                Else
                    Alert(Page, "Revise los datos. El Elemento no coincide con el elemento que aparece en el código del contador")
                End If
            Else

                'RDF. Fecha: 04/03/2009
                'Se comprueba que no hayan más registros con fechafin a null
                comandoAux.CommandText = "SELECT COUNT(*) FROM PVYCR_Contadores_ElementosMedida WHERE CodigoPVYCR='" & txtElementoMedida.Text.Substring(0, txtElementoMedida.Text.Length - 4) & "' AND " & _
                                    " IdElementoMedida='" & txtElementoMedida.Text.Replace(txtElementoMedida.Text.Substring(0, txtElementoMedida.Text.Length - 3), "") & "' AND FechaFinal is null" & _
                                    " AND idContador<>'" & lblContadorRel.Text & "' and FechaRevision<>'" & lblFechaRevRel.Text & "' "
                NumReg = comandoAux.ExecuteScalar()

                If NumReg = 0 Or txtFFinRel.Text <> "" Then
                    comando.CommandText = "update PVYCR_Contadores_ElementosMedida set CodigoPVYCR = '" & txtElementoMedida.Text.Substring(0, txtElementoMedida.Text.Length - 4) & "' " & _
                                    ",FechaInicio =" & IIf(txtFIniRel.Text = "", "null", "'" & txtFIniRel.Text & "'") & ",FechaFinal=" & IIf(txtFFinRel.Text = "", "null", "'" & txtFFinRel.Text & "'") & ",idElementoMedida='" & txtElementoMedida.Text.Replace(txtElementoMedida.Text.Substring(0, txtElementoMedida.Text.Length - 3), "") & "' " & _
                                    " where idContador='" & lblContadorRel.Text & "' and FechaRevision='" & lblFechaRevRel.Text & "' AND CodigoPVYCR='" & lblElementoRel.Text.Substring(0, lblElementoRel.Text.Length - 4) & "' AND " & _
                                    " IdElementoMedida='" & lblElementoRel.Text.Replace(lblElementoRel.Text.Substring(0, lblElementoRel.Text.Length - 3), "") & "'"
                    comando.ExecuteNonQuery()
                    comando.ExecuteNonQuery()
                    crearDataSetsHistorial(lblContadorRel.Text, lblFechaRevRel.Text, lblCodigoPVYCR.Text)
                    pnlRelacion.Visible = False


                Else
                    Alert(Page, "Revise los datos. El Elemento de Medida tiene asociado un Contador sin finalizar")
                End If
            End If

        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                'Case 547
                '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    Alert(Page, "El contador ya está relacionado con el Elemento de Medida")
            End Select
        End Try

    End Sub

    Protected Sub rptHistorial_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptHistorial.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String
        Dim objTrans As SqlTransaction
        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "borrar"
                'RDF 10/02/2009 Borrar registro PVYCR_Contadores_ElementosMedida

                If conexion.State = ConnectionState.Closed Then conexion.Open()
                objTrans = conexion.BeginTransaction()

                Try
                    comando.Transaction = objTrans
                    'En primer lugar, se eliminan el registro correspondiente a PVYCR_Contadores_ElementosMedida
                    comando.CommandText = "delete from PVYCR_Contadores_ElementosMedida where IdContador='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' and idElementoMedida='" & parametros(2) & "' AND CodigoPVYCR='" & parametros(3) & "'"
                    comando.ExecuteNonQuery()
                    objTrans.Commit()
                    crearDataSetsHistorial(parametros(0), parametros(1), lblCodigoPVYCR.Text)
                    pnlRelacion.Visible = False

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el registro correspondiente")
                    objTrans.Rollback()
                End Try
            Case "editar"
                pnlRelacion.Visible = True
                crearDataSetHistorial(parametros(0), parametros(1), parametros(2), parametros(3))
                lblElementoRel.Text = parametros(3) & "-" & parametros(2)
                txtElementoMedida.Text = utiles.nullABlanco(dstElementoContador.Tables("TablaElementoContador").Rows(0).Item("codigoPVYCR")) & "-" & utiles.nullABlanco(dstElementoContador.Tables("TablaElementoContador").Rows(0).Item("idElementoMedida"))
                txtFIniRel.Text = utiles.nullABlanco(dstElementoContador.Tables("TablaElementoContador").Rows(0).Item("FechaInicio"))
                txtFFinRel.Text = utiles.nullABlanco(dstElementoContador.Tables("TablaElementoContador").Rows(0).Item("FechaFinal"))

        End Select
    End Sub

    Private Sub crearDataSetHistorial(ByVal parametros0 As String, ByVal parametros1 As String, ByVal parametros2 As String, ByVal parametros3 As String)
        utiles.Comprobar_Conexion_BD(Page, conexion)
        'daElementoContador.SelectCommand.CommandText = "select * from PVYCR_Contadores_ElementosMedida where idContador='" & parametros0 & "' and FechaRevision= '" & parametros1 & "' AND " & _
        '" idElementoMedida='" & parametros2 & "' AND CodigoPVYCR='" & parametros3 & "'"
        daElementoContador.SelectCommand.CommandText = "select * from PVYCR_Contadores_ElementosMedida where idContador='" & parametros0 & "' and FechaRevision= '" & parametros1 & "' AND " & _
                                                 " CodigoPVYCR='" & parametros3 & "'"

        daElementoContador.Fill(dstElementoContador, "TablaElementoContador")

        'RDF
        'Fecha: 05/03/2009
        'Se obtiene el historial todos los contadores asociados al Elemento de Medida
        sentenciaHistorialElemento = "SELECT * FROM PVYCR_Contadores_ElementosMedida where idElementoMedida='" & parametros2 & "' AND codigoPVYCR='" & parametros3 & "' ORDER BY  idElementoMedida, FechaInicio DESC, FechaFinal "
        daHistorialElemento.SelectCommand.CommandText = sentenciaHistorialElemento
        daHistorialElemento.Fill(dstHistorialElemento, "TablaHistorialElemento")

    End Sub

    
    Protected Sub btnCancelarRel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarRel.Click
        pnlRelacionarElemento.Visible = True

        pnlRelacion.Visible = False
        pnlContadores.Visible = False
        txtElementoMedida.Text = ""
        txtFFinRel.Text = ""
        txtFIniRel.Text = ""
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        pnlContadores.Visible = True
        pnlRelacionarElemento.Visible = False

        pnlRelacion.Visible = false
        lblContadorRel.Text = ""
        lblFechaRevRel.Text = ""
        txtElementoMedida.Text = ""
        txtFFinRel.Text = ""
        txtFIniRel.Text = ""
        lblElementoRel.Text = ""

    End Sub
    Private Sub AltaAutomaticaRelacionElemento(ByVal idcontador As String, ByVal FechaRevision As String, ByVal FechaIni As String)
        'RDF
        'Fecha: 10/02/2009
        'Alta automática de la relación con el Elemento de Medida
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        utiles.Comprobar_Conexion_BD(Page, conexion)

        'Se comprueba que el idcontador es correcto, y que existe el Elemento correspondiente
        Try
            Dim codigoPVYCR As String
            Dim idElemento As String
            Dim NumReg As String

            codigoPVYCR = idcontador.Substring(0, Len(idcontador) - 7)
            idElemento = idcontador.Replace(codigoPVYCR, "")
            idElemento = idElemento.Substring(1, 3)

            'RDF. Fecha: 04/03/2009
            'Se comprueba que no hayan más registros con fechafin a null
            comandoAux.CommandText = "SELECT COUNT(*) FROM PVYCR_Contadores_ElementosMedida WHERE CodigoPVYCR='" & codigoPVYCR & "' AND " & _
                                " IdElementoMedida='" & idElemento & "' AND FechaFinal is null"
            NumReg = comandoAux.ExecuteScalar()
            If NumReg = 0 Then
                'Se inserta el registro correspondiente
                comando.CommandText = "insert into [PVYCR_Contadores_ElementosMedida]([CodigoPVYCR],[idElementoMedida],[idContador],[FechaRevision],[FechaInicio]) " & _
                                      " VALUES ('" & codigoPVYCR & "','" & idElemento & "','" & idcontador & "','" & FechaRevision & "','" & IIf(FechaIni <> "", FechaIni, Now.Date.ToString()) & "')"
                comando.ExecuteNonQuery()
            Else
                Alert(Page, "Revise los datos. El Elemento de Medida tiene asociado un Contador sin finalizar")
            End If

        Catch ex As System.Data.SqlClient.SqlException
            Alert(Page, "No se ha podido dar de alta la relación con el Elemento")

        End Try
    End Sub
    Private Sub BajaContadoresElementosMedida(ByVal id As String, ByVal fecharevision As String)
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)
        Dim numreg As Integer = 0
        utiles.Comprobar_Conexion_BD(Page, conexion)
     
        comando.CommandText = "update PVYCR_Contadores_ElementosMedida set FechaFinal=" & IIf(txtFechaFin.Text = "", "null", "'" & txtFechaFin.Text & "'") & _
                        " where idContador='" & id & "' and FechaRevision='" & fecharevision & "' "

        comando.ExecuteNonQuery()
    End Sub
    Protected Function ObtenerFiltrado(ByVal eldataitem As DataRowView) As String
        If utiles.nullABlanco(txtFiltrado.Text) <> "" And utiles.nullABlanco(ddlFiltrado.SelectedValue) <> "" Then
            Return eldataitem("" + ddlFiltrado.SelectedValue + "")
        End If
    End Function
    Protected Sub rellenarListas()
        'Dim dstTipoCauce As New DataSet
        Dim dstFiltrado As New DataSet
        Try
            'ncm rellenamos el campo filtrado con las columnas de la tabla de cauces
            utiles.Comprobar_Conexion_BD(Page, conexion)
            daContadores.SelectCommand.CommandText = " SELECT Column_Name as columna  FROM INFORMATION_SCHEMA. COLUMNS where Table_Name = 'PVYCR_CONTADORES' and COLUMN_NAME NOT IN ('IDCONTADOR','FECHAREVISION','V_MARCA','H_MARCA','Q_MARCA','E_MARCA') "
            daContadores.Fill(dstFiltrado, "TablaFiltrado")
            ddlFiltrado.DataSource = dstFiltrado.Tables("TablaFiltrado")
            ddlFiltrado.DataValueField = "columna"
            ddlFiltrado.DataBind()
            ddlFiltrado.Items.Insert(0, New ListItem("[Seleccionar]", ""))

        Catch ex As Exception

        End Try



    End Sub
End Class
