Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.SICA_GestionArboles
Imports GuarderiaFluvial.utiles
Imports System
Imports System.IO



Partial Class SICAH_SeguimientoAdministrativoDETALLE
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

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ucPaginacion.ruta = "../"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        imgCalFechaInicio.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & Me.txtEDFecha.ClientID & "'),'dd/mm/yyyy');")
        Me.imgFFechaFinal.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaFinal.ClientID & "'),'dd/mm/yyyy');")
        Me.imgFFechaInicial.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaInicial.ClientID & "'),'dd/mm/yyyy');")

        'lnbVerDetalleBoletin

        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If

            'EGB 13/10/2008 El menu no es necesario en esta pantalla ya que se invoca desde el Arbol a modo de consulta.
            'lblCabecera.Text = genHTML.cabecera(Page)
            'lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"))


            'EGB 13/10/2008 Aqui debemos discriminar el tipo de nodo que hemos seleccionado para pasarlo como tipo y variable de 

            Select Case Page.Request.QueryString("ClaveTipo")
                Case "C"
                    'Session("EnlaceC") = 6
                    'lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(Session("EnlaceC"), "../", Page, 11, "C", Session("ClaveNodo"), "N")
                    lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(6, "../", Page, 11, "C", Session("ClaveNodo"), "N")
                    lblNodo.Text = "Cauce - " + Session("ClaveNodo")

                Case "P"
                    'Session("EnlaceP") = 6
                    'lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(Session("EnlaceP"), "../", Page, 11, "P", Session("ClaveNodo"), "N")
                    lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(6, "../", Page, 11, "P", Session("ClaveNodo"), "N")
                    lblNodo.Text = "Punto - " + Session("ClaveNodo")

                Case Else
                    Exit Sub
            End Select

            'EGB 09/07/2008 Asociación con el Arbol de Cauces --------------------------------------------------------------------
            imgCerrarVentana.Attributes.Add("onclick", "desplegarArbol(this);")
            'imgArbol.Attributes.Add("onclick", "desplegarArbol(this);")

            crearDataSets()
            rellenarListas()
            pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Max
        End If
    End Sub


    Public Function GetDataSetArbol() As DataSet
        Dim DS As System.Data.DataSet

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
        Return DS

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

    Private Sub CrearDataSetResumenBoletin(ByVal numref As String)
        'Carga el resumen de datos de cabecera y hechos del boletin
        sentenciaSel = "SELECT Fecha, NombreAutor, Hechos FROM dbo.PVYCR_BoletinGuarderia WHERE NumRef='" & numref & "'"

        If dstSeguimientosAdministrativos.Tables.Contains("TablaCabeceraBoletin") Then
            dstSeguimientosAdministrativos.Tables("TablaCabeceraBoletin").Clear()
        End If
        daSeguimientosAdministrativos.SelectCommand.CommandText = sentenciaSel
        daSeguimientosAdministrativos.Fill(dstSeguimientosAdministrativos, "TablaCabeceraBoletin")
        If dstSeguimientosAdministrativos.Tables("TablaCabeceraBoletin").Rows.Count = 0 Then
            Me.pnlDetalleBoletin.Visible = False
        Else
            Me.pnlDetalleBoletin.Visible = True
            pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Min
            Me.txtHechos.Text = dstSeguimientosAdministrativos.Tables("TablaCabeceraBoletin").Rows(0).Item("Hechos")
        End If

    End Sub
    Private Sub crearDataSets()

        'Criterios de filtrado
        Dim sFiltro As String = ""

        'sentenciaSel = "SELECT PVYCR_SeguimientoAdministrativo.*, PVYCR_SeguimientoAdministrativo_TS.Denominacion " & _
        '"FROM PVYCR_SeguimientoAdministrativo INNER JOIN " & _
        '"PVYCR_SeguimientoAdministrativo_TS ON " & _
        '"PVYCR_SeguimientoAdministrativo.TipoSuceso = PVYCR_SeguimientoAdministrativo_TS.TipoSuceso"

        sentenciaSel = "SELECT  SA.CodigoCauce, SA.Fecha, SA.TipoSuceso, SA_TS.Denominacion, SA.CodigoPVYCR, SA.Comentarios, ISNULL(BG.NumRef, '') AS NumRef, SA.Responsable " & _
                       "FROM PVYCR_SeguimientoAdministrativo AS SA LEFT OUTER JOIN PVYCR_BoletinGuarderia AS BG " & _
                       "ON SA.Fecha = BG.Fecha AND SA.CodigoPVYCR = BG.PVYCRRef AND SA.TipoSuceso = BG.Tipo INNER JOIN " & _
                       "PVYCR_SeguimientoAdministrativo_TS AS SA_TS ON SA.TipoSuceso = SA_TS.TipoSuceso "

        'EGB 13/10/2008 Filtro por elemento seleccionado del Arbol. Solo mostramos los Seguimientos del Nodo Activo
        If Session("ClaveNodo") <> "" Then
            Select Case Session("ClaveTipo")
                Case "C"
                    sFiltro = " WHERE CodigoCauce='" + Session("ClaveNodo") + "'"
                Case "P"
                    sFiltro = " WHERE CodigoPVYCR='" + Session("ClaveNodo") + "'"
                Case Else
                    sFiltro = ""
            End Select
        End If
        '-------------------------------------------------------------------------------
        'EGB 14/10/2008 Filtros Locales
        If Session("FiltroLocal") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroLocal")
            Else
                sFiltro = sFiltro + Session("FiltroLocal")
            End If
        End If
        '-------------------------------------------------------------------------------
        'sentenciaOrder = " ORDER BY CodigoCauce,SA.CodigoPVYCR, SA.Fecha, SA.TipoSuceso "
        'EGB. 12/02/2009 Inclusion de imagenes de up y down para activar la ordenacion por fechas ascendente y descendente
        If Me.imgBOrd.ImageUrl = "images/A_DOWN.gif" Then
            sentenciaOrder = " ORDER BY SA.Fecha DESC, CodigoCauce, SA.CodigoPVYCR, SA.TipoSuceso"
        Else
            sentenciaOrder = " ORDER BY SA.Fecha, CodigoCauce, SA.CodigoPVYCR, SA.TipoSuceso"
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

        'IPIM 25/03/2009: Creamos una caché con el dataset de los seguimientos para tenerlos a la hora de imprimir
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
                pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Min
                Me.pnlDetalleBoletin.Visible = False
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
                    Me.txtEDCodigoPVYCR.Text = nullABlanco(dstSeguimientosAdministrativos.Tables("TablaSegumientoAdministrativo").Rows(0).Item("CodigoPVYCR"))
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
            Case "boletin"
                Me.lblNumRef.Text = "Boletín Referencia: " & parametros(0)
                Me.lblNumRefSel.Text = parametros(0)
                Me.pnlDetalleBoletin.Visible = True
                pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Min
                Me.pnlEDVSeguimientos.Visible = False
                lnbVerDetalleBoletin.Attributes.Add("onclick", "top.document.location='BoletinGuarderia.aspx?numref=" & parametros(0) & "'; event.cancelBubble=true;return false;")
                CrearDataSetResumenBoletin(parametros(0))

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


    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        'EGB 14/10/2008
        If Session("ClaveNodo") = "P" Then
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
        '    strFiltro = " AND DATEPART(mm,Fecha)= '" + ddlFMes.SelectedValue + "' "
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

        If txtFComentarios.Text <> "" Then
            strFiltro = strFiltro + " AND Comentarios like '" + txtFComentarios.Text + "' "
        End If

        If txtFCodigoPVYCR.Text <> "" Then
            strFiltro = strFiltro + " AND CodigoPVYCR LIKE '" + txtFCodigoPVYCR.Text + "' "
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

                strSQL = "UPDATE PVYCR_SeguimientoAdministrativo " & _
                         "SET [Comentarios] = @Comentarios " & _
                            ",[CodigoPVYCR] = @CodigoPVYCR " & _
                            ",[Fecha] = @Fecha " & _
                            ",[TipoSuceso] = @TipoSuceso " & _
                            ",[Responsable] = @Responsable " & _
                         " WHERE [CodigoCauce]=@CodigoCauce AND [Fecha]='" & Me.lblFechaSel.Text & "' AND [TipoSuceso]='" & Me.lblTipoSucesoSel.Text & "' "

                'Aprovechamos para cambiar la imagen que pasa de nuevo a fijarse como nuevo
                Me.imgAgregarSeguimiento.ImageUrl = "../images/plus.gif"
                Me.lblOperacion.Text = "Agregar"

                'Actualización del Seguimiento Activo. 
                comando.Parameters.AddWithValue("CodigoCauce", utiles.BlancoANull(Me.lblCodigoCauceSel.Text))
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
                comando.Parameters.AddWithValue("CodigoCauce", utiles.BlancoANull(Me.lblCodigoCauceSel.Text))
                comando.Parameters.AddWithValue("Fecha", utiles.BlancoANull(Me.txtEDFecha.Text))
                comando.Parameters.AddWithValue("TipoSuceso", utiles.BlancoANull(Me.ddlEDTipoSuceso.Text))

            End If


            comando.Parameters.AddWithValue("Comentarios", utiles.BlancoANull(Me.txtEDComentarios.Text))
            comando.Parameters.AddWithValue("CodigoPVYCR", utiles.BlancoANull(Me.txtEDCodigoPVYCR.Text))
            comando.Parameters.AddWithValue("Responsable", utiles.BlancoANull(Me.txtEDResponsable.Text))

            If conexion.State = ConnectionState.Closed Then conexion.Open()
            comando.CommandText = strSQL
            comando.ExecuteNonQuery()

            Me.crearDataSets()

            'Limpiamos controles
            Me.txtEDFecha.Text = ""
            Me.txtEDCodigoPVYCR.Text = ""
            Me.txtEDComentarios.Text = ""
            Me.txtEDResponsable.Text = ""


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
        'Visualizamos el Panel de Edición
        pnlEDVSeguimientos.Visible = True
        pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Min
        Me.pnlDetalleBoletin.Visible = False
        'pnlEDVSeguimientos.Visible = True
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
        pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Max

    End Sub


   
    Protected Sub imgCerrarED_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCerrarED.Click
        'Cancelación de la operación de Edición o Creación del Registro.
        Me.pnlEDVSeguimientos.Visible = False
        pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Max
    End Sub

    Protected Sub imgcerrarboletin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgcerrarboletin.Click
        Me.pnlDetalleBoletin.Visible = False
        pnlScrollGridSeguimientosAdministrativos.Height = c_Height_Max
    End Sub

    'Protected Sub lnbVerDetalleBoletin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnbVerDetalleBoletin.Click
    '    Response.Redirect("BoletinGuarderia.aspx?")
    'End Sub

    'Protected Sub lnbVerDetalleBoletin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnbVerDetalleBoletin.Click
    '    Response.Redirect("BoletinGuarderia.aspx?NumRef=" & lblNumRefSel.Text)
    'End Sub

    'Protected Sub lbtFecha_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFecha.Click
    '    'Filtrado por Fecha
    '    If imgOrd.ImageUrl = "images/A_UP.gif" Then
    '        imgOrd.ImageUrl = "images/A_DOWN.gif"
    '    Else
    '        imgOrd.ImageUrl = "images/A_UP.gif"
    '    End If

    '    crearDataSets()
    'End Sub

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
End Class
