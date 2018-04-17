Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.SICA_GestionArboles
Imports GuarderiaFluvial.utiles
Imports System
Imports System.IO
Imports SICAH_FuncionesArbolExt

Partial Class SICAH_ElementosMedida
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim daCodigos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCodigos As DataSet = New System.Data.DataSet()
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ucPaginacion.ruta = "../"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbtNuevo.DataBind()
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        lblCabecera.Text = genHTML.cabecera(Page)
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If

            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))

            imgCalFechaIniI.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaIni.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinI.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaFin.ClientID & "'),'dd/mm/yyyy');")
            'EGB 09/07/2008 Asociación con el Arbol de Cauces --------------------------------------------------------------------
            imgCerrarVentana.Attributes.Add("onclick", "desplegarArbol(this);")
            imgArbol.Attributes.Add("onclick", "desplegarArbol(this);")
            '---------------------------------------------------------------------------------------------------------------------
            If Request.QueryString("nuevo") = "yes" Then

                pnlEDElementos.Visible = True
                pnlElementos.Visible = False
                pnlCaudalMasivo.Visible = False
                'ucPaginacion.Visible = False
                lblTitulo.Text = "<b>&nbsp;NUEVO ELEMENTO</b>"
                ddlcodigoPVYCR.Enabled = True
                txtIdElemento.Enabled = True
                rellenarListas()
                'EBG Sólo se carga el árbol al dar de alta un nuevo elemento.
                '---------------------------------------------------------------------------------------------------------------------
                If treeView1.Nodes.Count = 0 Then
                    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "E") 'EGB el nuevo parametro E hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
                End If
            Else
                'ncm 23/09/2009 si es administrador ponemos a true el panel de calculo masivo de caudal
                If utiles.esAdministrador(Session("usuarioReg")) Then
                    OperacionesCalcularCaudal()
                    pnlCaudalMasivo.Visible = True
                Else
                    pnlCaudalMasivo.Visible = False
                End If
            End If
            'If Request.QueryString("pag") <> "" Then
            '    ucPaginacion.lblPaginatext = Request.QueryString("pag")
            'Else
            '    ucPaginacion.lblPaginatext = "1"
            'End If

            crearDataSets()

            'ucPaginacion.DataBind()
            'ucPaginacion.lblNumpaginasDatabind()

        End If
    End Sub


    'Public Function GetDataSetArbol() As DataSet
    '    Dim DS As System.Data.DataSet

    '    If File.Exists(MapPath("xmlDataSet.xml")) = False Then

    '        crearDataSetsPuntosXML2(dstarbol, treeView1, daElementos)

    '        dstarbol.WriteXml(MapPath("xmlDataSet.xml"))

    '        Return dstarbol
    '        Session("DSLoaded") = 1
    '    End If

    '    DS = New DataSet

    '    DS.ReadXml(MapPath("xmlDataSet.xml"))

    '    DS.Relations.Add("SelfRefenceRelation", _
    '                   DS.Tables("tablaArbol").Columns("IdArbol"), _
    '                    DS.Tables("tablaArbol").Columns("IdArbolPadre"), True)
    '    'DS.Relations("SelfRefenceRelation").Nested = True
    '    Session("DSLoaded") = 1
    '    Return DS

    'End Function
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
            daElementos.SelectCommand.CommandText = "Select * from dbo.PVYCR_ARBOL"
            daElementos.Fill(dstarbol, "tablaArbol")

            dstarbol.Relations.Add("SelfRefenceRelation", _
                            dstarbol.Tables("tablaArbol").Columns("IdArbol"), _
                            dstarbol.Tables("tablaArbol").Columns("IdArbolPadre"), True)
            dstarbol.Relations("SelfRefenceRelation").Nested = True

        Catch exp As Exception
        End Try
    End Sub
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
    Private Sub crearDataSets()
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        If txtRegistros.Text = "" Then
            sentenciaSel = " SELECT "
        Else
            sentenciaSel = " SELECT TOP " & txtRegistros.Text
        End If

        sentenciaSel = sentenciaSel & " CodigoPVYCR, idElementoMedida, Tipo FROM PVYCR_ElementosMedida "



        'RDF 20081002
        If Session("FiltroEl") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroEl")
            Else
                sFiltro = sFiltro + Session("FiltroEl")
            End If
        End If
        'ncm necesitamos una global que nos va a servir en todos los mtos para poder listar los registros
        'que han filtrado en cada mto.
        Session("FiltroListado") = Session("FiltroEl")

        sentenciaOrder = " order by CodigoPVYCR, idElementoMedida "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If
        'paginaAct = ucPaginacion.lblPaginatext
        'If paginaAct < 1 Then paginaAct = 1
        'primerReg = (paginaAct - 1) * ConfigurationManager.AppSettings("registrosPorPag")

        daElementos.SelectCommand.CommandText = sentenciaSel
        daElementos.Fill(dstElementos, "TablaElementos")
        'daContadores.Fill(dstContadores, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaContadores")
        'daElementos.Fill(dstElementos, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaElementos")
        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daElementos.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        'ucPaginacion.calcularPags(txtComando)
        'ncm calculamos el nº de registros que devolverá el filtro 
        txtNumRegFiltrados.Text = FuncionesGenerales.CalcularNumRegFiltrados(Session("FiltroEl"), conexion, sFiltro, "PVYCR_ElementosMedida", Page)

        rptElementos.DataSource = dstElementos.Tables("TablaElementos")
        rptElementos.DataBind()

    End Sub

    Private Sub crearDataSetElemento()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daElementos.SelectCommand.CommandText = "select * from PVYCR_ElementosMedida where idelementomedida='" & lblElementoSel.Text & "' and codigoPVYCR = '" & lblcodigoSel.Text & "' "
        daElementos.Fill(dstElementos, "TablaElementos")

    End Sub

    Protected Sub nuevoelemento(ByVal sender As Object, ByVal e As System.EventArgs)
        lblElementoSel.Text = ""
        lblcodigoSel.Text = ""
        Response.Redirect("ElementosMedida.aspx?nuevo=yes")

    End Sub



    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)
        Dim nivel As Integer = 0
        Dim y As Integer = 0
        Dim strNodoPadre As String = ""
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        Try
            If lblElementoSel.Text = "" Then
                'Insertamos un nuevo usuario
                comando.CommandText = "INSERT INTO [SIGVECTOR].[dbo].[PVYCR_ElementosMedida] ([CodigoPVYCR],[idElementomedida],[tipo]) VALUES " & _
                                    " (@codigoPVYCR ,@idelementoMedida, @tipo )"


            Else
                comando.CommandText = "UPDATE PVYCR_ElementosMedida SET CodigoPVYCR = @CodigoPVYCR " & _
                ",idElementoMedida = @idElementomedida, tipo = @tipo " & _
                "where idElementomedida='" & lblElementoSel.Text & "' AND codigoPVYCR='" & lblcodigoSel.Text & "'"
            End If
            comando.Parameters.AddWithValue("codigoPVYCR", utiles.BlancoANull(Me.ddlcodigoPVYCR.Text))
            comando.Parameters.AddWithValue("idelementoMedida", utiles.BlancoANull(Me.txtIdElemento.Text))
            comando.Parameters.AddWithValue("Tipo", utiles.BlancoANull(ddltipo.SelectedValue))

            comando.ExecuteNonQuery()
            Application("PVYCR_Arbol_Updated") = "NOK"

            'EGB 26/11/2008 Revisión de Codigo de Inserción en el Arbol.
            '-------------------------------------------------------------
            If lblElementoSel.Text = "" Then
                'NUEVO ELEMENTO DE MEDIDA -->INSERTAMOS NODO FICTICIO EN PVYCR_ARBOL
                '------------------------------------------------------------------
                Dim IdArbolPadre As String
                'Calcular El nodo padre al que se asociará el Nuevo Punto
                'If Me.txtIdArbol.Text = "" Then
                '  Alert(Page, "Debe seleccionar un punto del árbol")
                'Else
                '  IdArbolPadre = Me.txtIdArbol.Text
                'End If

                '---------------------------------------
                Dim strnodoPVYCRArbol, strDescripcion, strclavenodo As String

                strDescripcion = Me.ddlcodigoPVYCR.Text & "-" & Me.txtIdElemento.Text & " - " & StrTipoElemento(ddltipo.Text)
                '******** ncm en caso de que quieran que apararezcan los nuevos iconos en este árbol, descomentar las dos líneas inferiores y comentar la tercera ********'
                'Dim tipoPunto As String = ObtenerTipoPunto(Me.ddlcodigoPVYCR.Text)
                'strDescripcion = Left(Trim(strDescripcion), 200)
                'strnodoPVYCRArbol = "<img src='images/iconosBuenos/" & SICAH_FuncionesArbolExt.ObtenerIconoPuntoE(Me.ddlcodigoPVYCR.Text, Me.txtIdElemento.Text, conexion, Page, tipoPunto, ddltipo.SelectedValue) & "' border=0>&nbsp;&nbsp;<font color=red>" & strDescripcion
                strnodoPVYCRArbol = "<img src='images/distancias.gif' border=0>&nbsp;&nbsp;<font color=red>" & strDescripcion
                strnodoPVYCRArbol = Replace(strnodoPVYCRArbol, "'", "''")
                strclavenodo = Me.ddlcodigoPVYCR.Text & "#E#" & ddltipo.Text
                'ncm obtenemos la x, la y y el strNodoPadre
                IdArbolPadre = SICAH_FuncionesArbolExt.ObtenerIdPadre(strclavenodo, Page, "E")
                nivel = ObtenerNivelNodo(IdArbolPadre, Page, "E")
                y = GetY(IdArbolPadre, nivel, "E", Page)
                strNodoPadre = ObtenerStrNodo(Me.ddlcodigoPVYCR.Text, "E", Page)

                'Insercion del nodo en PVYCR_Arbol
                comando.CommandText = "INSERT INTO PVYCR_Arbol ([IdArbolPadre],[strnodo],[descripcion],[tipo],[clavenodo],[CodigoPVYCR],[IdElementoMedida],x,y,strNodoPadre,esTelemedida)VALUES ('" & IdArbolPadre & "','" & strnodoPVYCRArbol & "','" & strDescripcion & "','E','" & strclavenodo & "','" & Me.ddlcodigoPVYCR.Text & "','" & Me.txtIdElemento.Text & "'," & nivel & "," & y & ",'" & strNodoPadre & "','N')"
                comando.ExecuteNonQuery()



            Else
                'EDICION PUNTO no actualiza PVYCR-ARbol
            End If
            '---------------------------------------------------------------------



        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                'Case 547
                '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    Alert(Page, "El Elemento de Medida ya existe")
            End Select
        End Try


        Me.pnlElementos.Visible = True
        Me.pnlEDElementos.Visible = False
        'Me.ucPaginacion.Visible = True
        'ucPaginacion.lblPaginatext = "1"

        crearDataSets()

        'ncm 23/09/2009 si es administrador ponemos a true el panel de calculo masivo de caudal
        If utiles.esAdministrador(Session("usuarioReg")) Then
            OperacionesCalcularCaudal()
            pnlCaudalMasivo.Visible = True
        Else
            pnlCaudalMasivo.Visible = False
        End If
        'ucPaginacion.lblNumpaginasDatabind()

    End Sub
    Public Function StrTipoElemento(ByVal tipo As String)
        Dim mistr As String

        Select Case tipo
            Case "Q"
                mistr = "CAUDAL"
            Case "E"
                mistr = "ENERGÍA"
            Case "V"
                mistr = "VOLUMEN"
            Case "H"
                mistr = "HORAS"
        End Select
        Return mistr
    End Function
    Protected Sub rptElementos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptElementos.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String
        Dim objTrans As SqlTransaction

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Session("NuevoElemento") = 0
                Me.pnlEDElementos.Visible = True
                Me.pnlElementos.Visible = False
                'ncm 26/10/2009
                Me.pnlCaudalMasivo.Visible = False

                btnAceptar.Visible = True
                'Me.ucPaginacion.Visible = False
                'lblElementoSel.Text = e.CommandArgument
                lblElementoSel.Text = parametros(1)
                lblcodigoSel.Text = parametros(0)
                crearDataSetElemento()
                If dstElementos.Tables("TablaElementos").Rows.Count > 0 Then
                    'Datos del elemento
                    ddlcodigoPVYCR.Items.Clear()
                    ddlcodigoPVYCR.Items.Add(New System.Web.UI.WebControls.ListItem(dstElementos.Tables("TablaElementos").Rows(0).Item("CodigoPVYCR").ToString))
                    Me.txtIdElemento.Text = utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("IdElementoMedida"))
                    Me.ddltipo.Text = utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo"))
                    'ponemos el código y el idelemento que no se pueda modificar
                    ddlcodigoPVYCR.Enabled = False
                    txtIdElemento.Enabled = False
                    'imgArbol.Attributes.Clear()
                    imgArbol.Attributes.Add("onclick", "jscript:alert('No es posible reasociar el elemento.\n Elimine el Elemento y establezca la asociación correctamente.');")
                End If
                rptElementos.DataSource = dstElementos.Tables("TablaElementos")
                rptElementos.DataBind()


            Case "borrar"
                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion)
                objTrans = conexion.BeginTransaction()

                Try
                    comando.Transaction = objTrans
                    ''En primer lugar, se eliminan las relaciones que tiene la motobomba
                    'comando.CommandText = "delete from PVYCR_ElementosMedida_MotoBombas where CodigoMotobomba='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
                    'comando.ExecuteNonQuery()
                    'Se elimina el elemento de medida correspondiente
                    comando.CommandText = "delete from PVYCR_ElementosMedida where CodigoPVYCR='" & parametros(0) & "' and idelementoMedida='" & parametros(1) & "' "
                    comando.ExecuteNonQuery()
                    comando.CommandText = "delete from PVYCR_Arbol where CodigoPVYCR='" & parametros(0) & "' and idelementoMedida='" & parametros(1) & "' "
                    comando.ExecuteNonQuery()


                    objTrans.Commit()

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el Elemento de Medida")
                    objTrans.Rollback()
                End Try
                crearDataSets()
                'ucPaginacion.lblNumpaginasDatabind()


        End Select
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.pnlElementos.Visible = True
        Me.pnlEDElementos.Visible = False
        'Me.ucPaginacion.Visible = True
        'ucPaginacion.lblPaginatext = "1"

        ddlcodigoPVYCR.Items.Clear()
        crearDataSets()

        'ncm 23/09/2009 si es administrador ponemos a true el panel de calculo masivo de caudal
        If utiles.esAdministrador(Session("usuarioReg")) Then
            OperacionesCalcularCaudal()
            pnlCaudalMasivo.Visible = True
        Else
            pnlCaudalMasivo.Visible = False
        End If
        'ucPaginacion.lblNumpaginasDatabind()

    End Sub
    Protected Sub rellenarListas()
        If IsNothing(dstElementos.Tables("TablaElementos")) Then
            utiles.Comprobar_Conexion_BD(Page, conexion)
            daCodigos.SelectCommand.CommandText = "select codigoPVYCR from PVYCR_puntos order by codigoPVYCR"
            daCodigos.Fill(dstCodigos, "TablaCodigos")
        End If
        ddlcodigoPVYCR.DataSource = dstCodigos.Tables("TablaCodigos")
        ddlcodigoPVYCR.DataValueField = "codigoPVYCR"
        ddlcodigoPVYCR.DataBind()

    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub treeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView1.SelectedNodeChanged
        Dim TipoNodo As String
        Dim v() As String
        Try
            v = Split(treeView1.SelectedNode.Value, "#")
            TipoNodo = v(1)
            If TipoNodo = "P" Then
                lblDesCodigoPVYCR.Text = "Punto de Asociación: " & treeView1.SelectedNode.Text
                ddlcodigoPVYCR.Text = v(0).ToString()
                txtIdArbol.Text = v(2) 'IdArbol en PVYCR_Arbol
            Else
                Alert(Page, "Seleccione un punto para asociar el elemento de medida.")
            End If
        Catch mierror As Exception
            'Error en la seleccion del arbol
            Alert(Page, "Seleccione de nuevo el punto, no se registró correctamente")
        End Try
    End Sub


    Protected Sub AceptarFiltro(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081002
        Dim strFiltro As String
        strFiltro = ""
        If txtFCodigoPVYCR.Text <> "" Then
            strFiltro = " AND CodigoPVYCR like '" + txtFCodigoPVYCR.Text + "' "
        End If

        If txtFidElemento.Text <> "" Then
            strFiltro = strFiltro + " AND idElementoMedida like '" + txtFidElemento.Text + "' "
        End If

        If ddlFTipoElemento.SelectedValue <> "0" Then
            strFiltro = strFiltro + " AND Tipo='" + ddlFTipoElemento.SelectedValue + "' "

        End If


        Session("FiltroEl") = strFiltro
        crearDataSets()
    End Sub

    Protected Function esAcequia(ByVal Tipo As Object)
        'RDF. Fecha: 04/09/2008
        'Función que oculta/visualiza el icono de gráficos dependiendo
        'del valor del tipo de punto
        Dim TipoPunto As String = ""
        TipoPunto = nullABlanco(Tipo)

        If TipoPunto = "Q" Then
            Return True
        Else
            Return False
        End If
    End Function



    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        'RDF 
        '29/10/2008
        crearDataSets()
    End Sub

    Protected Sub btnCaudal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCaudal.Click
        SICA_FuncionesCaudalMasivo.CalcularCaudalMasivo(ddlPVYCR.Text, ddlElemento.Text, txtFechaIni.Text, txtFechaFin.Text)
    End Sub
    Protected Sub OperacionesCalcularCaudal()
        rellenarListasCaudalesPVYCR()

    End Sub
    Protected Sub rellenarListasCaudalesPVYCR()

        utiles.Comprobar_Conexion_BD(Page, conexion)
        daCodigos.SelectCommand.CommandText = "select distinct codigoPVYCR from PVYCR_elementosmedida where tipo = 'Q' order by codigoPVYCR"
        daCodigos.Fill(dstCodigos, "TablaCodigosCaudal")

        ddlPVYCR.DataSource = dstCodigos.Tables("TablaCodigosCaudal")
        ddlPVYCR.DataValueField = "codigoPVYCR"
        ddlPVYCR.DataBind()
        ddlPVYCR.Items.Insert(0, New ListItem("[Seleccionar]", ""))

    End Sub

    Protected Sub ddlPVYCR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPVYCR.SelectedIndexChanged
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daCodigos.SelectCommand.CommandText = "select idElementoMedida from PVYCR_elementosmedida where codigoPVYCR = '" & ddlPVYCR.SelectedValue & "' and tipo ='Q' order by 1"
        daCodigos.Fill(dstCodigos, "TablaElementosCaudal")

        ddlElemento.DataSource = dstCodigos.Tables("TablaElementosCaudal")
        ddlElemento.DataValueField = "idElementoMedida"
        ddlElemento.DataBind()
    End Sub
    Protected Function ObtenerTipoPunto(ByVal codigoPVYCR As String) As String
        'obtenemos el punto si es M o G
        Dim sentenciatipopunto = "select tipoPunto from PVYCR_puntos where codigoPVYCR = '" & codigoPVYCR & "'"
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daCodigos.SelectCommand.CommandText = sentenciatipopunto
        Return daCodigos.SelectCommand.ExecuteScalar
    End Function

    Protected Sub btnListFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListFiltro.Click
        Dim ventanaChecks = "SelecCamposFiltradosMto.aspx?mto=Elementos de Medida"

        If utiles.nullABlanco(txtNumRegFiltrados.Text) <> "" Or utiles.nullABlanco(txtNumRegFiltrados.Text) = "0" Then
            ClientScript.RegisterStartupScript(Page.GetType, "abreventanaChecks", "<script language=javascript>window.open('" + ventanaChecks + "','','toolbar=no,resizable=no, scrollbar=no, status=no')</script>")
        Else
            JavaScript.Alert(Page, "No hay elementos filtrados")
        End If

    End Sub
End Class
