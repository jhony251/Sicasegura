Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.SICA_GestionArboles
Imports GuarderiaFluvial.utiles
Imports System
Imports System.IO



Partial Class SICAH_SeguimientoAdministrativo
    Inherits System.Web.UI.Page
    Const c_Height_Min = "380"
    Const c_Height_Max = "530"
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daSeguimientosAdministrativos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstSeguimientosAdministrativos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim daCodigos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCodigos As DataSet = New System.Data.DataSet()
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbtNuevo.DataBind()

        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        imgCalFechaInicio.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & Me.txtEDFecha.ClientID & "'),'dd/mm/yyyy');")
        Me.imgFFechaFinal.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaFinal.ClientID & "'),'dd/mm/yyyy');")
        Me.imgFFechaInicial.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaInicial.ClientID & "'),'dd/mm/yyyy');")

        'Registramos los componenetes del arbol
        'imgFArbolCauces.Attributes.Add("onclick", "desplegarArbol(this);")
        'imgCerrarVentana.Attributes.Add("onclick", "desplegarArbol(this);")
        'EGB 13/10/2008 El menu no es necesario en esta pantalla ya que se invoca desde el Arbol a modo de consulta.
        lblCabecera.Text = genHTML.cabecera(Page)

        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If


            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))

            'End Select
            'EGB 24/11/2008 Test para carga del arbol bajo demanda
            ''EGB 21/11/2008 Filtrado de Seguimientos por Cauces 
            'If treeView1.Nodes.Count = 0 Then
            '    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "P")
            '    '---------------------------------------------------------------------
            'End If
            crearDataSets()
            rellenarListas()
            pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Max
        End If
    End Sub


    Public Function GetDataSetArbol() As DataSet
        Dim DS As New System.Data.DataSet

        DS = Cache("DTArbolCacheado")

        If DS Is Nothing Then


            If File.Exists(MapPath("xmlDataSet.xml")) = False Then

                crearDataSetsPuntosXML2(dstarbol, treeView1, daSeguimientosAdministrativos)

                dstarbol.WriteXml(MapPath("xmlDataSet.xml"))

                Return dstarbol
                Session("DSLoaded") = 1
            End If

            DS = New DataSet

            DS.ReadXml(MapPath("xmlDataSet.xml"))

            DS.Relations.Add("SelfRefenceRelation", _
                           DS.Tables("tablaArbol").Columns("IdArbol"), _
                            DS.Tables("tablaArbol").Columns("IdArbolPadre"), True)
            'DS.Relations("SelfRefenceRelation").Nested = True
            Session("DSLoaded") = 1
            'TEST EGB. 
            Cache("DTArbolCacheado") = DS
            Return DS
        Else
            Return DS
        End If

    End Function
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'If ucPaginacion.lblMuevetext = "si" Then
        'crearDataSets()
        '    ucPaginacion.lblMuevetext = "no"
        'End If
    End Sub
    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function

    'Private Sub CrearDataSetResumenBoletin(ByVal numref As String)
    '    'Carga el resumen de datos de cabecera y hechos del boletin
    '    sentenciaSel = "SELECT Fecha, NombreAutor, Hechos FROM dbo.PVYCR_BoletinGuarderia WHERE NumRef='" & numref & "'"

    '    If dstSeguimientosAdministrativos.Tables.Contains("TablaCabeceraBoletin") Then
    '        dstSeguimientosAdministrativos.Tables("TablaCabeceraBoletin").Clear()
    '    End If
    '    daSeguimientosAdministrativos.SelectCommand.CommandText = sentenciaSel
    '    daSeguimientosAdministrativos.Fill(dstSeguimientosAdministrativos, "TablaCabeceraBoletin")
    '    If dstSeguimientosAdministrativos.Tables("TablaCabeceraBoletin").Rows.Count = 0 Then
    '        Me.pnlDetalleBoletin.Visible = False
    '    Else
    '        Me.pnlDetalleBoletin.Visible = True
    '        Me.txtHechos.Text = dstSeguimientosAdministrativos.Tables("TablaCabeceraBoletin").Rows(0).Item("Hechos")
    '    End If

    'End Sub
    Private Sub crearDataSets()

        'Criterios de filtrado
        Dim sFiltro As String = ""

        If txtRegistros.Text = "" Then
            sentenciaSel = " SELECT "
        Else
            sentenciaSel = " SELECT TOP " & txtRegistros.Text
        End If

        sentenciaSel = sentenciaSel & " SA.CodigoCauce, SA.Fecha as Fecha, SA_TS.TipoSuceso, SA_TS.Denominacion, SA.CodigoPVYCR, SA.Comentarios, ISNULL(BG.NumRef, '') AS NumRef, SA.Responsable " & _
                       "FROM PVYCR_SeguimientoAdministrativo AS SA LEFT OUTER JOIN PVYCR_BoletinGuarderia AS BG " & _
                       "ON SA.Fecha = BG.Fecha AND SA.CodigoPVYCR = BG.PVYCRRef AND SA.TipoSuceso = BG.Tipo INNER JOIN " & _
                       "PVYCR_SeguimientoAdministrativo_TS AS SA_TS ON SA.TipoSuceso = SA_TS.TipoSuceso "


        '-------------------------------------------------------------------------------
        'EGB 14/10/2008 Filtros Locales
        If Session("FiltroLocal") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroLocal")
            Else
                sFiltro = sFiltro + Session("FiltroLocal")
            End If
        End If
        'ncm necesitamos una global que nos va a servir en todos los mtos para poder listar los registros
        'que han filtrado en cada mto.
        Session("FiltroListado") = Session("FiltroLocal")

        '-------------------------------------------------------------------------------
        'sentenciaOrder = " ORDER BY CodigoCauce,SA.CodigoPVYCR, SA.Fecha, SA_TS.TipoSuceso "
        'EGB. 12/02/2009 Inclusion de imagenes de up y down para activar la ordenacion por fechas ascendente y descendente
        If Me.imgBOrd.ImageUrl = "images/A_DOWN.gif" Then
            sentenciaOrder = " ORDER BY SA.Fecha DESC, CodigoCauce, SA.CodigoPVYCR, SA_TS.TipoSuceso"
        Else
            sentenciaOrder = " ORDER BY SA.Fecha, CodigoCauce, SA.CodigoPVYCR, SA_TS.TipoSuceso"
        End If
        '-------------------------------------------------------------------------------

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If

        'paginaAct = ucPaginacion.lblPaginatext
        'If paginaAct < 1 Then paginaAct = 1
        'primerReg = (paginaAct - 1) * ConfigurationManager.AppSettings("registrosPorPag")

        If dstSeguimientosAdministrativos.Tables.Contains("TablaSeguimientosAdministrativos") Then
            dstSeguimientosAdministrativos.Tables("TablaSeguimientosAdministrativos").Clear()
        End If

        daSeguimientosAdministrativos.SelectCommand.CommandText = sentenciaSel
        daSeguimientosAdministrativos.Fill(dstSeguimientosAdministrativos, "TablaSeguimientosAdministrativos")

        'daContadores.Fill(dstContadores, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaContadores")
        'daElementos.Fill(dstElementos, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaElementos")
        'Cálculo del número de páginas

        'Dim txtComando As String = ""
        'txtComando = daSeguimientosAdministrativos.SelectCommand.CommandText
        'txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        'ucPaginacion.calcularPags(txtComando)

        'ncm calculamos el nº de registros que devolverá el filtro 
        txtNumRegFiltrados.Text = FuncionesGenerales.CalcularNumRegFiltrados(Session("FiltroLocal"), conexion, sFiltro, " PVYCR_SeguimientoAdministrativo AS SA LEFT OUTER JOIN PVYCR_BoletinGuarderia AS BG " & _
                       "ON SA.Fecha = BG.Fecha AND SA.CodigoPVYCR = BG.PVYCRRef AND SA.TipoSuceso = BG.Tipo INNER JOIN " & _
                       "PVYCR_SeguimientoAdministrativo_TS AS SA_TS ON SA.TipoSuceso = SA_TS.TipoSuceso ", Page)

        'IPIM 24/03/2009: Creamos una caché con el dataset de los seguimientos para tenerlos a la hora de imprimir
        Cache("dstSeguimientos") = dstSeguimientosAdministrativos
        rptSeguimientosAdministrativos.DataSource = dstSeguimientosAdministrativos.Tables("TablaSeguimientosAdministrativos")
        rptSeguimientosAdministrativos.DataBind()

    End Sub

    Private Sub crearDataSetSeguimientoAdministrativo()

        daSeguimientosAdministrativos.SelectCommand.CommandText = "select *, datepart(mm,Fecha) as mes, datepart(yyyy,Fecha) as anyo from [PVYCR_SeguimientoAdministrativo] WHERE CodigoCauce='" & lblCodigoCauceSel.Text & "' and Fecha = '" & lblFechaSel.Text & "' and TipoSuceso='" & lblTipoSucesoSel.Text & "'"
        daSeguimientosAdministrativos.Fill(dstSeguimientosAdministrativos, "TablaSegumientoAdministrativo")

    End Sub




    'Protected Sub btnFiltroAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptar.Click
    '    'ucPaginacion.lblPaginatext = "1"
    '    crearDataSets()
    '    'ucPaginacion.lblNumpaginasDatabind()
    'End Sub
    'Protected Sub btnFiltrocancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelar.Click
    '    'ucPaginacion.lblPaginatext = "1"
    '    txtFiltroCodigoPVYCR.Text = "[Código Elemento]"
    '    crearDataSets()
    '    'ucPaginacion.lblNumpaginasDatabind()
    'End Sub

    'Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

    '    Dim comando As SqlCommand = New SqlCommand("", conexion)
    '    Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

    '    If conexion.State = ConnectionState.Closed Then conexion.Open()
    '    comando.Parameters.Clear()
    '    Try
    '        If Me.lblCodigoCauceSel.Text = "" Then
    '            'Insertamos un nuevo usuario
    '            comando.CommandText = "INSERT INTO [SIGVECTOR].[dbo].[PVYCR_ElementosMedida] ([CodigoPVYCR],[idElementomedida],[tipo]) VALUES " & _
    '                                " (@codigoPVYCR ,@idelementoMedida, @tipo )"


    '        Else
    '            comando.CommandText = "UPDATE PVYCR_ElementosMedida SET CodigoPVYCR = @CodigoPVYCR " & _
    '            ",idElementoMedida = @idElementomedida, tipo = @tipo " & _
    '            "where idElementomedida='" & lblCodigoCauceSel.Text & "' AND codigoPVYCR='" & lblCodigoCauceSel.Text & "'"
    '        End If
    '        comando.Parameters.AddWithValue("codigoPVYCR", utiles.BlancoANull(Me.ddlTipoSuceso.Text))

    '        comando.ExecuteNonQuery()

    '    Catch Exc As System.Data.SqlClient.SqlException
    '        Select Case Exc.Number
    '            'Case 547
    '            '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
    '            '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
    '            Case 2627
    '                Alert(Page, "El Elemento de Medida ya existe")
    '        End Select
    '    End Try


    '    Me.pnlSeguimientos.Visible = True
    '    'Me.pnlEDSeguimientos.Visible = False
    '    'Me.ucPaginacion.Visible = True
    '    'ucPaginacion.lblPaginatext = "1"
    '    'txtFiltroCodigoPVYCR.Text = "[Código Elemento]"
    '    crearDataSets()
    '    'ucPaginacion.lblNumpaginasDatabind()

    'End Sub
    Protected Sub rptSeguimientosAdministrativos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptSeguimientosAdministrativos.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String
        Dim objTrans As SqlTransaction

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"

                Me.pnlEDVSeguimientos.Visible = True
                Me.pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Min
                'Me.pnlDetalleBoletin.Visible = False
                'Actualización de la represantación de los comandos de edicion
                Me.imgAgregarSeguimiento.ImageUrl = "../images/save.gif"
                Me.lblOperacion.Text = "Guardar"
                Me.lblModoED.Text = "EDICION DE SEGUIMIENTO"

                'Actualización de controles con las clave de la entidad
                lblCodigoCauceSel.Text = parametros(0)
                lblFechaSel.Text = parametros(2)
                lblTipoSucesoSel.Text = parametros(1)
                crearDataSetSeguimientoAdministrativo()

                If dstSeguimientosAdministrativos.Tables("TablaSegumientoAdministrativo").Rows.Count > 0 Then
                    'Cargamos los datos del seguimiento en los controles de edición
                    'Me.txtEDAnyo.Text = dstSeguimientosAdministrativos.Tables("TablaSegumientoAdministrativo").Rows(0).Item("Anyo")
                    'Me.ddlEDMes.SelectedValue = dstSeguimientosAdministrativos.Tables("TablaSegumientoAdministrativo").Rows(0).Item("Mes")
                    Me.txtEDFecha.Text = dstSeguimientosAdministrativos.Tables("TablaSegumientoAdministrativo").Rows(0).Item("Fecha")

                    Me.ddlEDTipoSuceso.SelectedValue = dstSeguimientosAdministrativos.Tables("TablaSegumientoAdministrativo").Rows(0).Item("TipoSuceso")
                    Me.txtEDComentarios.Text = nullABlanco(dstSeguimientosAdministrativos.Tables("TablaSegumientoAdministrativo").Rows(0).Item("Comentarios"))
                    Me.ddlEDCodigoCauce.SelectedValue = nullABlanco(dstSeguimientosAdministrativos.Tables("TablaSegumientoAdministrativo").Rows(0).Item("CodigoCauce"))
                    Me.ddlEDCodigoPVYCR.SelectedValue = nullABlanco(dstSeguimientosAdministrativos.Tables("TablaSegumientoAdministrativo").Rows(0).Item("CodigoPVYCR"))

                    Me.txtEDResponsable.Text = nullABlanco(dstSeguimientosAdministrativos.Tables("TablaSegumientoAdministrativo").Rows(0).Item("Responsable"))

                End If

            Case "borrar"
                If conexion.State = ConnectionState.Closed Then conexion.Open()
                objTrans = conexion.BeginTransaction()

                Try
                    comando.Transaction = objTrans
                    '1.Eliminar relaciones si existen
                    '--
                    'comando.ExecuteNonQuery()

                    '2.Se elimina el seguimiento administrativo
                    comando.CommandText = "delete from PVYCR_SeguimientoAdministrativo where CodigoCauce='" & parametros(0) & "' and Fecha='" & parametros(2) & "' and TipoSuceso='" & parametros(1) & "'"
                    comando.ExecuteNonQuery()

                    objTrans.Commit()

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el Seguimiento Actual")
                    objTrans.Rollback()
                End Try
                crearDataSets()
                'ucPaginacion.lblNumpaginasDatabind()
                'Case "boletin"
                '    Me.lblNumRef.Text = "Boletín Referencia: " & parametros(0)
                '    Me.lblNumRefSel.Text = parametros(0)
                '    Me.pnlDetalleBoletin.Visible = True
                '    Me.pnlEDVSeguimientos.Visible = False
                '    lnbVerDetalleBoletin.Attributes.Add("onclick", "top.document.location='BoletinGuarderia.aspx?numref=" & parametros(0) & "'; event.cancelBubble=true;return false;")
                '    CrearDataSetResumenBoletin(parametros(0))

        End Select
    End Sub

    'Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
    '    Me.pnlSeguimientos.Visible = True
    '    'Me.pnlEDSeguimientos.Visible = False
    '    'Me.ucPaginacion.Visible = True
    '    'ucPaginacion.lblPaginatext = "1"
    '    'txtFiltroCodigoPVYCR.Text = "[Código Elemento]"
    '    'ddlTipoSuceso.Items.Clear()
    '    crearDataSets()
    '    'ucPaginacion.lblNumpaginasDatabind()

    'End Sub
    Protected Sub rellenarListas()
        'EGB Rellenamos la tabla de Tipos de Suceso del Seguimiento Administrativo [PVYCR_SeguimientoAdministrativo_TS]
        Dim fila As DataRow

        If IsNothing(dstCodigos.Tables("TablaTiposSucesos")) Then
            daCodigos.SelectCommand.CommandText = "SELECT TipoSuceso,Denominacion FROM PVYCR_SeguimientoAdministrativo_TS"
            daCodigos.Fill(dstCodigos, "TablaTiposSucesos")
        End If

        'Combo de Tipo de Suceso en visualización
        ddlFTipoSuceso.Items.Add(New System.Web.UI.WebControls.ListItem("[Seleccionar]", ""))
        For Each fila In dstCodigos.Tables("TablaTiposSucesos").Rows
            ddlFTipoSuceso.Items.Add(New System.Web.UI.WebControls.ListItem(fila("Denominacion"), fila("TipoSuceso")))
        Next fila

        'Combo de Tipo de Suceso en edición

        ddlEDTipoSuceso.Items.Add(New System.Web.UI.WebControls.ListItem("[Seleccionar]", ""))
        For Each fila In dstCodigos.Tables("TablaTiposSucesos").Rows
            ddlEDTipoSuceso.Items.Add(New System.Web.UI.WebControls.ListItem(fila("Denominacion"), fila("TipoSuceso")))
        Next fila

        'Combo de Cauces - En lugar de cargar un arbol para seleccionar, dejamos un combo basado en el contenido del arbol para
        'que se carguen también los nodos padres
        If IsNothing(dstCodigos.Tables("TablaCaucesEnArbol")) Then
            daCodigos.SelectCommand.CommandText = "SELECT CodigoCauce FROM pvycr_arbol WHERE tipo='c' ORDER BY codigocauce"
            daCodigos.Fill(dstCodigos, "TablaCaucesEnArbol")
        End If
        ''Lista de Cauces en filtro
        'ddlFCodigoCauce.Items.Add(New System.Web.UI.WebControls.ListItem("[Seleccionar]", ""))
        'For Each fila In dstCodigos.Tables("TablaCaucesEnArbol").Rows
        '    ddlFCodigoCauce.Items.Add(New System.Web.UI.WebControls.ListItem(fila("CodigoCauce"), fila("CodigoCauce")))
        'Next fila
        'Lista de Cauces en edición
        ddlEDCodigoCauce.Items.Add(New System.Web.UI.WebControls.ListItem("[Seleccionar]", ""))
        For Each fila In dstCodigos.Tables("TablaCaucesEnArbol").Rows
            ddlEDCodigoCauce.Items.Add(New System.Web.UI.WebControls.ListItem(fila("CodigoCauce"), fila("CodigoCauce")))
        Next fila

        'EGB 24/11/2008 CAMBIO DE ARBOL POR COMBOS - MOTIVOS RENDIMIENTO

        'Combo de Puntos - En lugar de cargar el arbol para seleccionar, dejamos un combo dependiente del anterior de cauces
        If IsNothing(dstCodigos.Tables("TablaPuntosEnArbol")) Then
            'EGB 02/02/2008 Correccion para la inclusion del CodigoPVYCR en las actualizaciones
            'daCodigos.SelectCommand.CommandText = "SELECT DISTINCT a.descripcion, p.DenominacionPunto, p.CodigoCauce, p.CodigoPVYCR FROM (SELECT CodigoPVYCR, Descripcion fROM PVYCR_Arbol WHERE TIPO='P')A inner join PVYCR_Puntos P ON A.CodigoPVYCR=P.CodigoPVYCR"
            daCodigos.SelectCommand.CommandText = "SELECT DISTINCT a.descripcion, p.DenominacionPunto, p.CodigoCauce, p.CodigoPVYCR FROM (SELECT CodigoPVYCR, Descripcion fROM PVYCR_Arbol WHERE TIPO='P')A inner join PVYCR_Puntos P ON A.CodigoPVYCR=P.CodigoPVYCR"


            daCodigos.Fill(dstCodigos, "TablaPuntosEnArbol")
        End If

         ''Lista de Cauces en filtro
        'ddlFCodigoPVYCR.Items.Add(New System.Web.UI.WebControls.ListItem("[Seleccionar]", ""))
        'For Each fila In dstCodigos.Tables("TablaPuntosEnArbol").Rows
        '    ddlFCodigoPVYCR.Items.Add(New System.Web.UI.WebControls.ListItem(fila("Descripcion"), fila("CodigoPVYCR")))
        'Next fila
        'Lista de Cauces en edición
        ddlEDCodigoPVYCR.Items.Add(New System.Web.UI.WebControls.ListItem("[Seleccionar Opcional]", ""))
        For Each fila In dstCodigos.Tables("TablaPuntosEnArbol").Rows
            ddlEDCodigoPVYCR.Items.Add(New System.Web.UI.WebControls.ListItem(fila("Descripcion"), fila("CodigoPVYCR")))
        Next fila
    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        ''EGB 14/10/2008
        'If Session("ClaveNodo") = "P" Then
        '    Return True
        'Else
        '    Return False
        'End If

        'IPIM 07/07/2009: Comento esto, pq la función que se usa en toda la aplicación es la siguiente:
        If (Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14) Or Session("ClaveNodo") = "P" Then
            Return True
        Else
            Return False
        End If


    End Function

    Protected Function VisibleSegunValor(ByVal str As String) As Boolean
        If str = "" Then
            Return False
        Else
            Return True
        End If

    End Function
    Protected Function VisibleSegunTipo() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function

    'Protected Sub treeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView1.SelectedNodeChanged
    '    Dim TipoNodo As String
    '    Dim v() As String
    '    Try
    '        v = Split(treeView1.SelectedNode.Value, "#")
    '        TipoNodo = v(1)
    '        If TipoNodo = "P" Then
    '            lblDesCodigoPVYCR.Text = "Punto de Asociación: " & treeView1.SelectedNode.Text
    '            ddlTipoSuceso.Text = v(0).ToString()
    '        Else
    '            Alert(Page, "Seleccione un punto para asociar el elemento de medida.")
    '        End If
    '    Catch mierror As Exception
    '        'Error en la seleccion del arbol
    '        Alert(Page, "Seleccione de nuevo el punto, no se registró correctamente")
    '    End Try
    'End Sub


    Protected Sub AceptarFiltro(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtAceptarUsReg.Click
        'EGB 14/10/2008 Filtros De fecha por mes y año
        Dim strFiltro As String
        strFiltro = ""
        'If ddlFMes.SelectedValue <> "0" Then
        '    strFiltro = " AND DATEPART(mm,SA.Fecha)= '" + ddlFMes.SelectedValue + "' "
        'End If

        'If txtFAnyo.Text <> "" Then
        '    strFiltro = strFiltro + " AND DATEPART(yyyy,Fecha) ='" + txtFAnyo.Text + "' "
        'End If
        If txtFFechaInicial.Text <> "" And txtFFechaFinal.Text <> "" Then
            strFiltro = strFiltro & " and SA.Fecha between '" & txtFFechaInicial.Text & " 00:00:00' and '" & txtFFechaFinal.Text & " 23:59:59' "
        ElseIf txtFFechaInicial.Text = "" And txtFFechaFinal.Text <> "" Then
            Alert(Page, "La Fecha Desde no puede ser nula")
            strFiltro = ""
        ElseIf txtFFechaInicial.Text <> "" And txtFFechaFinal.Text = "" Then
            Alert(Page, "La Fecha Hasta no puede ser nula")
            strFiltro = ""
        End If

        If ddlFTipoSuceso.SelectedValue <> "" Then
            strFiltro = strFiltro + " AND SA.TipoSuceso='" + ddlFTipoSuceso.SelectedValue + "' "
        End If

        'If Me.ddlFCodigoCauce.SelectedValue <> "" Then
        '    strFiltro = strFiltro + " AND CodigoCauce='" & ddlFCodigoCauce.SelectedValue & "'"
        'End If

        If txtFCodigoCauce.Text <> "" Then
            strFiltro = strFiltro + " AND CodigoCauce LIKE '" + txtFCodigoCauce.Text + "' "
        End If
        'If Me.ddlFCodigoPVYCR.SelectedValue <> "" Then
        '    strFiltro = strFiltro + " AND CodigoPVYCR='" & Me.ddlFCodigoPVYCR.SelectedValue & "'"
        'End If

        If txtFCodigoPVYCR.Text <> "" Then
            strFiltro = strFiltro + " AND CodigoPVYCR LIKE '" + txtFCodigoPVYCR.Text + "' "
        End If

        If txtFComentarios.Text <> "" Then
            strFiltro = strFiltro + " AND Comentarios like '" + txtFComentarios.Text + "' "
        End If

        If txtFResponsable.Text <> "" Then
            strFiltro = strFiltro + " AND Responsable like '" + txtFResponsable.Text + "' "
        End If
        Session("FiltroLocal") = strFiltro
        crearDataSets()
    End Sub

    Protected Sub imgAgregarSeguimiento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAgregarSeguimiento.Click
        Dim strSQL As String
        Try
            If Me.imgAgregarSeguimiento.ImageUrl = "../images/save.gif" Then
                'EDITAR datos del registro
                'RDF
                'Fecha: 23/03/2009
                'He añadido en la actualización que se pueda modificar la Fecha y el TipoSuceso
                strSQL = "UPDATE PVYCR_SeguimientoAdministrativo " & _
                         "SET [Comentarios] = @Comentarios " & _
                            ",[CodigoCauce] = @CodigoCauce " & _
                            ",[CodigoPVYCR] = @CodigoPVYCR " & _
                            ",[Fecha] = @Fecha " & _
                            ",[TipoSuceso] = @TipoSuceso " & _
                            ",[Responsable] = @Responsable " & _
                         " WHERE [CodigoCauce]='" & lblCodigoCauceSel.Text & "' AND [Fecha]='" & lblFechaSel.Text & "' AND [TipoSuceso]='" & lblTipoSucesoSel.Text & "'"

                'Aprovechamos para cambiar la imagen que pasa de nuevo a fijarse como nuevo
                Me.imgAgregarSeguimiento.ImageUrl = "../images/plus.gif"
                Me.lblOperacion.Text = "Agregar"

                'Actualización del Seguimiento Activo. 
                'Correccion 02/02/2009 Actualizacion del codigo de punto.
                comando.Parameters.AddWithValue("CodigoPVYCR", utiles.BlancoANull(Me.ddlEDCodigoPVYCR.SelectedValue))
                '---
                comando.Parameters.AddWithValue("CodigoCauce", utiles.BlancoANull(Me.ddlEDCodigoCauce.SelectedValue))
                comando.Parameters.AddWithValue("Fecha", utiles.BlancoANull(Me.txtEDFecha.Text))
                comando.Parameters.AddWithValue("TipoSuceso", utiles.BlancoANull(Me.ddlEDTipoSuceso.SelectedValue))
            Else
                'INSERTAMOS nuevos valores
                strSQL = "INSERT INTO PVYCR_SeguimientoAdministrativo " & _
                           "([CodigoCauce]" & _
                           ",[Fecha] " & _
                           ",[TipoSuceso] " & _
                           ",[Comentarios] " & _
                           ",[CodigoPVYCR] " & _
                           ",[Responsable]) " & _
                         " VALUES " & _
                           "(@CodigoCauce " & _
                           ",@Fecha " & _
                           ",@TipoSuceso " & _
                           ",@Comentarios " & _
                           ",@CodigoPVYCR " & _
                           ",@Responsable)"

                'Parametros clave del nuevo registro de Seguimiento
                comando.Parameters.AddWithValue("CodigoCauce", utiles.BlancoANull(Me.ddlEDCodigoCauce.SelectedValue))
                comando.Parameters.AddWithValue("Fecha", utiles.BlancoANull(Me.txtEDFecha.Text))
                comando.Parameters.AddWithValue("TipoSuceso", utiles.BlancoANull(Me.ddlEDTipoSuceso.Text))
                'Correccion 02/02/2009 Actualizacion del codigo de punto.
                comando.Parameters.AddWithValue("CodigoPVYCR", utiles.BlancoANull(Me.ddlEDCodigoPVYCR.SelectedValue))
                '---

            End If


            comando.Parameters.AddWithValue("Comentarios", utiles.BlancoANull(Me.txtEDComentarios.Text))

            comando.Parameters.AddWithValue("Responsable", utiles.BlancoANull(Me.txtEDResponsable.Text))

            If conexion.State = ConnectionState.Closed Then conexion.Open()
            comando.CommandText = strSQL
            comando.ExecuteNonQuery()

            Me.crearDataSets()

            'Limpiamos controles
            Me.txtEDFecha.Text = ""
            Me.ddlEDCodigoCauce.SelectedIndex = 0
            Me.ddlEDCodigoPVYCR.SelectedIndex = 0
            Me.ddlEDTipoSuceso.SelectedIndex = 0
            Me.txtEDComentarios.Text = ""
            Me.txtEDResponsable.Text = ""


            If Me.imgAgregarSeguimiento.ImageUrl = "../images/save.gif" Then
                'edicion de registros el panel sigue visible
                Me.pnlEDVSeguimientos.Visible = True
                Me.pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Min
            Else
                'nuevos registros
                Me.pnlEDVSeguimientos.Visible = False
                Me.pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Max
            End If


        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                Case 2627
                    Alert(Page, "Los valores ya existen ")
                Case Else
                    'Alert(Page, Err.Description & "\n Consulte con el administrador")
                    Alert(Page, "Complete los campos de Edición para Agregar el nuevo seguimiento.")
            End Select
        End Try
    End Sub
    Protected Sub nuevoSeguimiento(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtNuevo.Click
        'Aprovechamos para cambiar la imagen que pasa de nuevo a fijarse como nuevo
        Me.imgAgregarSeguimiento.ImageUrl = "../images/plus.gif"
        Me.lblOperacion.Text = "Agregar"
        pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Min
        'Visualizamos el Panel de Edición
        pnlEDVSeguimientos.Visible = True
        'Me.pnlDetalleBoletin.Visible = False
        pnlEDVSeguimientos.Visible = True
        'Limpiamos controles de Edicion

        'ddlEDMes.SelectedIndex = 0
        'txtEDAnyo.Text = ""
        txtEDFecha.Text = ""
        ddlEDTipoSuceso.SelectedIndex = 0
        txtEDComentarios.Text = ""
        lblModoED.Text = "NUEVO SEGUIMIENTO"
    End Sub

    Protected Sub imgCancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCancelar.Click
        'Cancelación de la operación de Edición o Creación del Registro.
        Me.pnlEDVSeguimientos.Visible = False
        Me.pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Max
    End Sub



    'Protected Sub imgCerrarED_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCerrarED.Click
    '    'Cancelación de la operación de Edición o Creación del Registro.
    '    Me.pnlEDVSeguimientos.Visible = False

    'End Sub

    'Protected Sub imgcerrarboletin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgcerrarboletin.Click
    '    'Me.pnlDetalleBoletin.Visible = False

    'End Sub

    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        AceptarFiltro(sender, e)
        crearDataSets()
    End Sub

    'Protected Sub imgFArbolCauces_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgFArbolCauces.Click
    '    If treeView1.Nodes.Count = 0 Then
    '        crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "P")
    '    End If
    'End Sub

    Protected Sub ddlEDCodigoCauce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fila As DataRow
        'EGB 25/11/2008 Actualizamos el desplegable de puntos con el contenido del selectedindex del codigo de cauce

        If dstCodigos.Tables.Contains("TablaPuntosEnArbol") Then
            dstCodigos.Tables("TablaPuntosEnArbol").Clear()
        End If
        daCodigos.SelectCommand.CommandText = "SELECT DISTINCT a.descripcion, p.DenominacionPunto, p.CodigoCauce, p.CodigoPVYCR FROM (SELECT CodigoPVYCR, Descripcion fROM PVYCR_Arbol WHERE TIPO='P')A inner join PVYCR_Puntos P ON A.CodigoPVYCR=P.CodigoPVYCR " & _
                                              "WHERE CodigoCauce='" & ddlEDCodigoCauce.SelectedValue & "'"
        daCodigos.Fill(dstCodigos, "TablaPuntosEnArbol")

        'Borrado previo
        ddlEDCodigoPVYCR.Items.Clear()
        'Actualización de Valores en la Lista de Puntos en edición
        ddlEDCodigoPVYCR.Items.Add(New System.Web.UI.WebControls.ListItem("[Seleccionar Opcional]", ""))
        For Each fila In dstCodigos.Tables("TablaPuntosEnArbol").Rows
            ddlEDCodigoPVYCR.Items.Add(New System.Web.UI.WebControls.ListItem(fila("Descripcion"), fila("CodigoPVYCR")))
        Next fila
        ddlEDCodigoPVYCR.DataBind()
    End Sub

   
    Protected Sub imgCerrarED_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCerrarED.Click
        'Cancelación de la operación de Edición o Creación del Registro.
        Me.pnlEDVSeguimientos.Visible = False
        pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Max
    End Sub

    Protected Sub imgBOrd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBOrd.Click
        'Filtrado por Fecha
        If imgBOrd.ImageUrl = "images/A_UP.gif" Then
            imgBOrd.ImageUrl = "images/A_DOWN.gif"
        Else
            imgBOrd.ImageUrl = "images/A_UP.gif"
        End If
        crearDataSets()
    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim nombreinforme As String
        nombreinforme = "../listados/ListadoSeguimientoAdmin.aspx"
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnListFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListFiltro.Click
        Dim ventanaChecks = "SelecCamposFiltradosMto.aspx?mto=Seguimiento Administrativo"

        If utiles.nullABlanco(txtNumRegFiltrados.Text) <> "" Or utiles.nullABlanco(txtNumRegFiltrados.Text) = "0" Then
            ClientScript.RegisterStartupScript(Page.GetType, "abreventanaChecks", "<script language=javascript>window.open('" + ventanaChecks + "','','toolbar=no,resizable=no, scrollbar=no, status=no')</script>")
        Else
            JavaScript.Alert(Page, "No hay elementos filtrados")
        End If

    End Sub
End Class
