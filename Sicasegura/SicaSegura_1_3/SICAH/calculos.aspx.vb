Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles

Partial Class SICAH_calculos
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daCalculos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCalculos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0
    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        ucPaginacion.ruta = "../"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbtnuevo.databind()

        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        'Registro de la funcionalidad del calendario
        imgFecha.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaValidez.ClientID & "'),'dd/mm/yyyy');")
        imgFechaHasta.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaValidezHasta.ClientID & "'),'dd/mm/yyyy');")


        Page.MaintainScrollPositionOnPostBack = True

        If Not IsPostBack Then
            lblCabecera.Text = genHTML.cabecera(Page)
            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))
            If Request.QueryString("nuevo") = "yes" Then
                pnlEDCalculos.Visible = True
                pnlCalculos.Visible = False
                ucPaginacion.Visible = False
                'pnlOperandos.Visible = False
                btnAgregarCalculo.Visible = True
                lblCalculoFormula.Text = "<b>NUEVO CÁLCULO</b>"
                EditarCalculo()

            End If

            If Not IsNothing(Request.QueryString("IdCalculo")) Then
                Me.lblCalculoSel.Text = Request.QueryString("IdCalculo")

                Me.pnlEDCalculos.Visible = True
                Me.pnlCalculos.Visible = False
                Me.ucPaginacion.Visible = False
                crearDataSetCalculo()
                If dstCalculos.Tables("TablaCalculos").Rows.Count > 0 Then
                    'Maestro:Datos de Cabecera del Calculo
                    Me.lblNombreCalculo.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculos").Rows(0).Item("DesCalculo"))
                    Me.lblFechaValidez.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculos").Rows(0).Item("FechaInicio"))
                    Me.lblFechaValidezHasta.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculos").Rows(0).Item("FechaFin"))
                    Me.txtFechaValidez.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculos").Rows(0).Item("FechaInicio"))
                    Me.txtFechaValidezHasta.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculos").Rows(0).Item("FechaFin"))
                    Me.txtNombreCalculo.Text = Me.lblNombreCalculo.Text
                    'Detalle:Datos de Detalle de Calculo: Calculos-Elementos de Medida
                    Me.rpt_calculoselementosmedida.DataSource = dstCalculos.Tables("TablaCalculosElementosMedida")
                    rpt_calculoselementosmedida.DataBind()
                End If
            End If

            If Request.QueryString("pag") <> "" Then
                ucPaginacion.lblPaginatext = Request.QueryString("pag")
            Else
                ucPaginacion.lblPaginatext = "1"
            End If


            crearDataSets()
            'ucPaginacion.DataBind()
            ucPaginacion.lblNumpaginasDatabind()
        End If
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If ucPaginacion.lblMuevetext = "si" Then
            crearDataSets()
            ucPaginacion.lblMuevetext = "no"
        End If
    End Sub
    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Protected Sub rptCalculos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptCalculos.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim objTrans As SqlTransaction
        Select Case e.CommandName
            Case "editar"
                Me.lblCalculoSel.Text = e.CommandArgument

                Me.pnlEDCalculos.Visible = True
                Me.pnlCalculos.Visible = False
                Me.ucPaginacion.Visible = False
                crearDataSetCalculo()
                If dstCalculos.Tables("TablaCalculos").Rows.Count > 0 Then
                    'Maestro:Datos de Cabecera del Calculo
                    Me.lblNombreCalculo.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculos").Rows(0).Item("DesCalculo"))
                    Me.lblFechaValidez.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculos").Rows(0).Item("FechaInicio"))
                    Me.lblFechaValidezHasta.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculos").Rows(0).Item("FechaFin"))
                    Me.txtFechaValidez.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculos").Rows(0).Item("FechaInicio"))
                    Me.txtFechaValidezHasta.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculos").Rows(0).Item("FechaFin"))
                    Me.txtNombreCalculo.Text = Me.lblNombreCalculo.Text
                    'Detalle:Datos de Detalle de Calculo: Calculos-Elementos de Medida
                    Me.rpt_calculoselementosmedida.DataSource = dstCalculos.Tables("TablaCalculosElementosMedida")
                    rpt_calculoselementosmedida.DataBind()
                End If
            Case "borrar"
                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion)
                objTrans = conexion.BeginTransaction()
                'Borrado de los elementos relacionados
                Try

                    comando.Transaction = objTrans

                    comando.CommandText = "delete from PVYCR_Calculos_ElementosMedida where idcalculo='" & e.CommandArgument & "'"
                    comando.ExecuteNonQuery()

                    comando.CommandText = "delete from pvycr_calculos where idcalculo=" & e.CommandArgument
                    comando.ExecuteNonQuery()
                    objTrans.Commit()

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el cálculo actual porque existen sistemas asociados al mismo.\n Elimine el sistema primero.")
                    objTrans.Rollback()
                End Try
                'Refrescar el repeater de calculos
                crearDataSets()
                ucPaginacion.lblNumpaginasDatabind()
        End Select
    End Sub

    
    Private Sub crearDataSets()
        'Criterios de filtrado
        dstCalculos.Clear()
        Dim sFiltro As String = ""

        sentenciaSel = "SELECT DesCalculo, PVYCR_Calculos.IdCalculo, " & _
                        "LTRIM(dbo.PVYCR_GetStrCalculo(PVYCR_Calculos.IdCalculo)) as Formula," & _
                        "FechaInicio, FechaFin " & _
                        "FROM PVYCR_Calculos "

        If txtFiltroDescripcionCalculo.Text <> "[Descripción Cálculo]" And txtFiltroDescripcionCalculo.Text <> "" Then
            sFiltro = " where DesCalculo like '" & txtFiltroDescripcionCalculo.Text & "' "
        Else
            sFiltro = ""
        End If
        sentenciaOrder = " order by DesCalculo "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If
        'paginaAct = ucPaginacion.lblPaginatext
        'If paginaAct < 1 Then paginaAct = 1
        'primerReg = (paginaAct - 1) * ConfigurationManager.AppSettings("registrosPorPag")

        daCalculos.SelectCommand.CommandText = sentenciaSel
        daCalculos.Fill(dstCalculos, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaCalculos")
        'daCalculos.Fill(dstCalculos, "TablaCalculos")

        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daCalculos.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        ucPaginacion.calcularPags(txtComando)

        rptCalculos.DataSource = dstCalculos.Tables("TablaCalculos")
        rptCalculos.DataBind()


    End Sub
    Private Sub crearDataSetCalculo()
        daCalculos.SelectCommand.CommandText = "select * from PVYCR_Calculos where idCalculo=" & lblCalculoSel.Text
        daCalculos.Fill(dstCalculos, "TablaCalculos")
        daCalculos.SelectCommand.CommandText = "select *, CodigoPVYCR+' ('+ IdElementoMedida + ')' as CodigoElementoMedida from PVYCR_Calculos_ElementosMedida where PVYCR_Calculos_ElementosMedida.idCalculo=" & lblCalculoSel.Text
        daCalculos.Fill(dstCalculos, "TablaCalculosElementosMedida")

    End Sub
    Private Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim MiComando As SqlCommand = New SqlCommand("", conexion)
        MiComando.Parameters.Clear()
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)

        Try
            Dim CodElementoMedida() As String
            CodElementoMedida = Split(Me.ddlElementoMedida.SelectedItem.Value, "_")

            If Session("NuevoCalculoElementoMedida") = 1 Then
                MiComando.CommandText = "INSERT INTO [PVYCR_Calculos_ElementosMedida] " & _
                "([CodigoPVYCR] " & _
                ",[idElementoMedida] " & _
                ",[idCalculo] " & _
                ",[FechaInicio] " & _
                ",[FechaFin] " & _
                ",[Factor])" & _
                " VALUES (@CodigoPVYCR,@idElementoMedida,@idCalculo,@FechaInicio,@FechaFin,@Factor) "

                MiComando.Parameters.AddWithValue("@CodigoPVYCR", CodElementoMedida(0))
                MiComando.Parameters.AddWithValue("@idElementoMedida", CodElementoMedida(1))
                MiComando.Parameters.AddWithValue("@idCalculo", Me.lblCalculoSel.Text)
                MiComando.Parameters.AddWithValue("@FechaInicio", Me.txtFechaValidez.Text)
                MiComando.Parameters.AddWithValue("@FechaFin", Me.txtFechaValidezHasta.Text)
                MiComando.Parameters.AddWithValue("@Factor", Me.txtFactor.Text)
            Else
                MiComando.CommandText = "UPDATE [PVYCR_Calculos_ElementosMedida] " & _
                                        "SET [Factor] = @Factor " & _
                                        " WHERE [IdCalculo]=@IdCalculo AND [idElementoMedida] = @IdElementoMedida AND [CodigoPVYCR] = @CodigoPVYCR"

                MiComando.Parameters.AddWithValue("@CodigoPVYCR", CodElementoMedida(0))
                MiComando.Parameters.AddWithValue("@idElementoMedida", CodElementoMedida(1))
                MiComando.Parameters.AddWithValue("@idCalculo", Me.lblCalculoSel.Text)
                MiComando.Parameters.AddWithValue("@Factor", Me.txtFactor.Text)

            End If

            MiComando.ExecuteNonQuery()

            'Refrescamos el grid de PVYCR_Calculos_ElementosMedida
            crearDataSetCalculo()
            rpt_calculoselementosmedida.DataSource = dstCalculos.Tables("TablaCalculosElementosMedida")
            rpt_calculoselementosmedida.DataBind()

        Catch err As System.Data.SqlClient.SqlException
            If err.Number = 2627 Then
                Alert(Page, "El cálculo seleccionado ya forma parte del sistema")
            Else
                Alert(Page, "Excepción SQL consulte con el administrador del Servidor")
            End If
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        lblCalculoSel.Text = ""
        pnlCalculos.Visible = True
        ucPaginacion.Visible = True
        pnlEDCalculos.Visible = False

        txtNombreCalculo.Text = ""
        'Actualizar  el grid de Calculos
        crearDataSets()
        ucPaginacion.lblNumpaginasDatabind()
    End Sub
    Protected Sub nuevocalculo(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCalculoSel.Text = ""
        Response.Redirect("Calculos.aspx?nuevo=yes")
    End Sub
    Protected Sub btnFiltroAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptar.Click
        ucPaginacion.lblPaginatext = "1"
        crearDataSets()
        ucPaginacion.lblNumpaginasDatabind()
    End Sub
    Protected Sub btnFiltrocancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelar.Click, btnFiltroCancelar.Click
        ucPaginacion.lblPaginatext = "1"
        txtFiltroDescripcionCalculo.Text = "[Descripción Cálculo]"
        crearDataSets()
        ucPaginacion.lblNumpaginasDatabind()
    End Sub


    Protected Sub imgEditarNombreCalculo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgEditarNombreCalculo.Click
        EditarCalculo()
    End Sub
    Protected Sub EditarCalculo()
        Dim MiComando As SqlCommand = New SqlCommand("", conexion)
        Dim strsqlSELECT As String
        If Me.imgEditarNombreCalculo.ImageUrl = "../SICAH/IMAGES/Proceso_OK2.GIF" Then
            Me.lblNombreCalculo.Visible = True
            Me.lblFechaValidezHasta.Visible = True
            Me.lblFechaValidez.Visible = True
            Me.imgFechaHasta.Visible = False
            Me.imgFecha.Visible = False
            Me.txtNombreCalculo.Visible = False
            Me.txtFechaValidezHasta.Visible = False
            Me.txtFechaValidez.Visible = False
            Me.imgEditarNombreCalculo.ImageUrl = "../SICAH/IMAGES/Proceso_OK.GIF"
            Me.imgEditarNombreCalculo.ToolTip = "Editar Atributos del Cálculo"
            Me.btnAgregarCalculo.Visible = True

            'GUARDAR ACTUALIZACIONES O DATOS DE ALTA DEL SISTEMA
            MiComando.Parameters.Clear()
            utiles.Comprobar_Conexion_BD(Page, conexion)
            Try
                If lblCalculoFormula.Text = "<b>NUEVO CÁLCULO</b>" Then
                    'INSERCION DE DATOS
                    strsqlSELECT = "INSERT INTO [PVYCR_Calculos]" & _
                                   "([FechaInicio],[FechaFin],[DesCalculo]) " & _
                                   "VALUES(@FechaDesde,@FechaHasta,@DesCalculo)"

                    MiComando.Parameters.AddWithValue("@FechaDesde", BlancoANull(Me.txtFechaValidez.Text))
                    MiComando.Parameters.AddWithValue("@FechaHasta", BlancoANull(Me.txtFechaValidezHasta.Text))
                    MiComando.Parameters.AddWithValue("@DesCalculo", Me.txtNombreCalculo.Text)
                    'pnlOperandos.Visible = True

                    'EJECUCION DE LAS SENTENCIAS DE INSERCION O ACTUALIZACION
                    MiComando.CommandText = strsqlSELECT
                    MiComando.ExecuteNonQuery()

                    'OBTENCION DEL IdCalculo recien creado
                    '---------------------------------------------------------------------------------------------
                    strsqlSELECT = "SELECT @@IDENTITY AS 'IdCalculo'"
                    daCalculos.SelectCommand.CommandText = strsqlSELECT
                    lblCalculoSel.Text = CStr(daCalculos.SelectCommand.ExecuteScalar())


                Else
                    'ACTUALIZACION DE DATOS
                    strsqlSELECT = "UPDATE [PVYCR_Calculos]" & _
                                   "SET [FechaInicio] = @FechaDesde " & _
                                   ",[FechaFin] = @FechaHasta " & _
                                   ",[DesCalculo] = @DesCalculo" & _
                                   " WHERE IdCalculo = @IdCalculo"

                    MiComando.Parameters.AddWithValue("@FechaDesde", BlancoANull(Me.txtFechaValidez.Text))
                    MiComando.Parameters.AddWithValue("@FechaHasta", BlancoANull(Me.txtFechaValidezHasta.Text))
                    MiComando.Parameters.AddWithValue("@DesCalculo", Me.txtNombreCalculo.Text)
                    MiComando.Parameters.AddWithValue("@IdCalculo", Me.lblCalculoSel.Text)

                End If



            Catch err As System.Data.SqlClient.SqlException
                If err.Number = 2627 Then
                    Alert(Page, "El registro ya existe en el Acta actual ")
                Else
                    Alert(Page, "Excepción SQL consulte con el administrador del Servidor")
                End If
            End Try
            'Refresco de los valores actualizados
            Me.lblNombreCalculo.Text = Me.txtNombreCalculo.Text
            Me.lblFechaValidezHasta.Text = Me.txtFechaValidezHasta.Text
            Me.lblFechaValidez.Text = Me.txtFechaValidez.Text

        Else
            Me.lblNombreCalculo.Visible = False
            Me.lblFechaValidezHasta.Visible = False
            Me.lblFechaValidez.Visible = False
            Me.txtNombreCalculo.Visible = True
            Me.txtFechaValidezHasta.Visible = True
            Me.txtFechaValidez.Visible = True
            Me.imgFechaHasta.Visible = True
            Me.imgFecha.Visible = True
            Me.imgEditarNombreCalculo.ImageUrl = "../SICAH/IMAGES/Proceso_OK2.GIF"
            Me.imgEditarNombreCalculo.ToolTip = "Guardar Modificaciones"
            Me.btnAgregarCalculo.Visible = False


        End If
    End Sub


    Protected Sub btnAgregarCalculo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarCalculo.Click
        If Me.txtNombreCalculo.Text = "" Then
            Alert(Page, "Debe completar primero el nombre del cálculo")
        Else
            pnlEdicionDetalleCalculo.Visible = True
            btnAceptar.Visible = True
            Session("NuevoCalculoElementoMedida") = 1
            'Habilitar los controles que pertenecen a la clave primaria 
            Me.ddlTipoElementoMedida.Enabled = True
            Me.ddlElementoMedida.Enabled = False
            Me.txtFactor.Text = ""
        End If
        
    End Sub

    Protected Sub ddlTipoCalculo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoElementoMedida.SelectedIndexChanged
        daCalculos.SelectCommand.CommandText = "SELECT [CodigoPVYCR]+' ('+[IdElementoMedida]+')'as CodElementoMedida, [CodigoPVYCR]+ '_'+[IdElementoMedida]as MiIdElementoMedida FROM PVYCR_ElementosMedida WHERE Tipo = '" & ddlTipoElementoMedida.SelectedItem.Value & "'"
        'dstCalculos.Tables("PVYCR_ElementosMedida").Clear()
        daCalculos.Fill(dstCalculos, "PVYCR_ElementosMedida")
        ddlElementoMedida.Items.Clear()
        ddlElementoMedida.DataSource = dstCalculos.Tables("PVYCR_ElementosMedida")
        ddlElementoMedida.DataValueField = "MiIdElementoMedida"
        ddlElementoMedida.DataTextField = "CodElementoMedida"
        ddlElementoMedida.DataBind()
        ddlElementoMedida.Enabled = True
    End Sub

   

    Protected Sub rpt_calculoselementosmedida_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rpt_calculoselementosmedida.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Session("NuevoCalculoElementoMedida") = 0
                Me.pnlEdicionDetalleCalculo.Visible = True
                btnAceptar.Visible = True
                Me.ucPaginacion.Visible = False

                ActualizarCamposPanelEdicionDetalleCalculo(parametros)

            Case "borrar"
                utiles.Comprobar_Conexion_BD(Page, conexion)
                'Container.DataItem("IdCalculo")& "#" & Container.DataItem("CodigoPVYCR")& "#" & Container.DataItem("IdElementoMedida")
                comando.CommandText = "delete from PVYCR_Calculos_ElementosMedida where idcalculo='" & parametros(0) & "' and CodigoPVYCR='" & parametros(1) & "' and IdElementoMedida='" & parametros(2) & "'"

                comando.ExecuteNonQuery()
                ucPaginacion.lblNumpaginasDatabind()

                'Refrescamos el grid de PVYCR_Calculos_ElementosMedida
                crearDataSetCalculo()
                rpt_calculoselementosmedida.DataSource = dstCalculos.Tables("TablaCalculosElementosMedida")
                rpt_calculoselementosmedida.DataBind()
        End Select
    
    End Sub

    Protected Sub ActualizarCamposPanelEdicionDetalleCalculo(ByVal parametros() As String)

        Dim sentenciaSQL As String
        sentenciaSQL = "SELECT   PVYCR_Calculos_ElementosMedida.CodigoPVYCR + '_'+ PVYCR_Calculos_ElementosMedida.IdElementoMedida as MiIdElementoMedida, PVYCR_Calculos_ElementosMedida.CodigoPVYCR, PVYCR_Calculos_ElementosMedida.idElementoMedida, " & _
                       "PVYCR_ElementosMedida.Tipo, PVYCR_Calculos_ElementosMedida.idCalculo, PVYCR_Calculos_ElementosMedida.Factor " & _
                       "FROM  PVYCR_Calculos_ElementosMedida INNER JOIN " & _
                       "PVYCR_ElementosMedida ON PVYCR_Calculos_ElementosMedida.CodigoPVYCR = PVYCR_ElementosMedida.CodigoPVYCR AND " & _
                       "PVYCR_Calculos_ElementosMedida.idElementoMedida = PVYCR_ElementosMedida.idElementoMedida " & _
                       "WHERE     (dbo.PVYCR_Calculos_ElementosMedida.CodigoPVYCR = '" & parametros(1) & "') AND " & _
                       "(dbo.PVYCR_Calculos_ElementosMedida.idElementoMedida = '" & parametros(2) & "') AND " & _
                       "(dbo.PVYCR_Calculos_ElementosMedida.idCalculo = '" & parametros(0) & "')"

        daCalculos.SelectCommand.CommandText = sentenciaSQL
        daCalculos.Fill(dstCalculos, "TablaCalculosElementosMedida")
        'Actualización Campo Factor
        Me.txtFactor.Text = utiles.nullABlanco(dstCalculos.Tables("TablaCalculosElementosMedida").Rows(0).Item("Factor"))

        'Actualizacion Campo Tipo de Elemento de Medida
        Me.ddlTipoElementoMedida.SelectedValue = dstCalculos.Tables("TablaCalculosElementosMedida").Rows(0).Item("Tipo")

        'Carga y Actualización Campo Elemento de Medida
        daCalculos.SelectCommand.CommandText = "SELECT [CodigoPVYCR]+' ('+[IdElementoMedida]+')'as CodElementoMedida, [CodigoPVYCR]+ '_'+[IdElementoMedida]as MiIdElementoMedida FROM PVYCR_ElementosMedida WHERE Tipo = '" & ddlTipoElementoMedida.SelectedItem.Value & "'"
        daCalculos.Fill(dstCalculos, "PVYCR_ElementosMedida")
        ddlElementoMedida.Items.Clear()
        ddlElementoMedida.DataSource = dstCalculos.Tables("PVYCR_ElementosMedida")
        ddlElementoMedida.DataValueField = "MiIdElementoMedida"
        ddlElementoMedida.DataTextField = "CodElementoMedida"
        ddlElementoMedida.DataBind()
        ddlElementoMedida.Enabled = True
        Me.ddlElementoMedida.SelectedValue = dstCalculos.Tables("TablaCalculosElementosMedida").Rows(0).Item("MiIdElementoMedida")
        'Deshabilitar los controles que pertenecen a la clave primaria 
        Me.ddlTipoElementoMedida.Enabled = False
        Me.ddlElementoMedida.Enabled = False


    End Sub
    
    Protected Sub btncerraredicion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncerraredicion.Click
        Me.pnlEdicionDetalleCalculo.Visible = False
        Me.btnAceptar.Visible = False
        ddlTipoElementoMedida.SelectedItem.Text = "[Seleccionar]"
        ddlElementoMedida.Items.Clear()
        ddlElementoMedida.Enabled = False

    End Sub

    Protected Function VisibleSegunPerfil() As Boolean
        'IPIM 06/07/2009
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function
    
End Class
