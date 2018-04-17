Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.IO
Imports System.Net.Mail


Partial Class SICAH_PetiInfo_PeticionInformacion
    Inherits System.Web.UI.Page

    'Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_dbsica"))
    Dim daPeticiones As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstPeticiones As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0
    Dim midstCauces As DataSet = New System.Data.DataSet()

    Dim NO_FILTRADO As String = "[No filtrado]"
    Dim NO_VALOR As String = "[Sin Valor]"

    Dim seccion As Integer = 6
    Dim subsec As String = "../../"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Page.MaintainScrollPositionOnPostBack = True
        'lbtNuevo.DataBind()

        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")

        Me.imgFiltroFeDesde.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & Me.filtroFeDesde.ClientID & "'),'dd/mm/yyyy');")
        Me.imgFiltroFeHasta.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & Me.filtroFeHasta.ClientID & "'),'dd/mm/yyyy');")

        Me.imgEdicionFeConsuDesde.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & Me.edicionFeConsuDesde.ClientID & "'),'dd/mm/yyyy');")
        Me.imgEdicionFeConsuHasta.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & Me.edicionFeConsuHasta.ClientID & "'),'dd/mm/yyyy');")

        lblCabecera.Text = genHTML.cabeceraDesdeSICAH(Page)

        ' PARA PRUEBAS!!!!
        '        If Request.QueryString("usuarioPrueba") <> "" Then
        '        Session("loginUsuario") = Request.QueryString("usuarioPrueba")
        'Session("usuarioReg") = "123"
        'Session("idperfil") = 11
        'Else
        'Session("loginUsuario") = "atapoyogf1"
        'Session("usuarioReg") = "123"
        'Session("idperfil") = 10
        'End If

        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            If Request.QueryString("asociacion") <> "yes" Then
                lblPestanyas.Text = genHTML.pestanyasMenu(seccion, subsec, Page, Session("idperfil"), Session("usuarioReg"))
            End If

            'If Request.QueryString("pag") <> "" Then
            '    ucPaginacion.lblPaginatext = Request.QueryString("pag")
            'Else
            '    ucPaginacion.lblPaginatext = "1"
            'End If

            ' Indicamos fecha mínima por defecto

            Dim fechaDesde As DateTime = DateAdd(DateInterval.Year, -1, Now)
            Dim mes As String = fechaDesde.Month & ""
            mes = Left("0", 2 - mes.Length) & mes
            Me.filtroFeDesde.Text = fechaDesde.Day & "/" & mes & "/" & fechaDesde.Year

            rellenarListasFiltro()

            generarConsulta()
            'RDF 03/10/2008

        End If


    End Sub

    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function


    Private Sub generarConsulta()
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)

        sentenciaSel = "SELECT " & _
                        " CodSoli, " & _
                        " CONVERT(VARCHAR, FechaSoli, 103) AS FechaSoli,  " & _
                        "E.DesEstado,  " & _
                        "T.DesTipo, " & _
                        "(select DesSubtipo FROM PEINF_SUBTIPO SB WHERE SB.CodSubtipo = S.CodSubtipo) AS DesSubtipo,  " & _
                        "Inscripcion, Descripcion, CodSICA, " & _
                        "(SELECT TOP 1 A.UsuAsignado FROM PEINF_ASIGNACION A WHERE S.CodEstado IN ('PA','GR','GP','GV') and A.CodSoli = S.CodSoli ORDER BY A.CodAsignacion DESC) AS Asignado  " & _
                        " FROM PEINF_SOLICITUD S, PEINF_ESTADO E, PEINF_TIPO T " & _
                        " WHERE(S.CodEstado = E.CodEstado) " & _
                        " AND S.CodTipo = T.CodTipo "


        'Añado filtro de tipo de usuario (es interno)
        If esAsignadoResponsable() Then
            ' Ningun filtro
        ElseIf esAsignado() Then
            ' Se muesta toda la información por petición

            'sFiltro = sFiltro + " AND (S.UsuCreacion = '" & Session("loginUsuario") & "' OR " & _
            '" (S.CodEstado IN ('PA','GR','GP') " & _
            '"  AND '" & Session("loginUsuario") & "' in (select top 1 A.UsuAsignado from PEINF_ASIGNACION A where A.CodSoli = S.CodSoli order by A.CodAsignacion desc)))"
        Else
            sFiltro = sFiltro & " AND S.UsuCreacion = '" & Session("loginUsuario") & "' "
        End If


        If filtroAsignado.SelectedItem.Text <> NO_FILTRADO Then
            sFiltro = sFiltro & _
            " AND S.CodEstado IN ('PA','GR','GP', 'GV') " & _
            " AND '" & filtroAsignado.SelectedValue & "' in (select top 1 A.UsuAsignado from PEINF_ASIGNACION A where A.CodSoli = S.CodSoli order by A.CodAsignacion desc)"

        End If

        'Añado filtros si los hay
        If filtroNumsoli.Text <> "" Then
            sFiltro = sFiltro + " AND S.CodSoli = " & filtroNumsoli.Text
        End If

        If filtroFeDesde.Text <> "" Then
            sFiltro = sFiltro + " AND S.FechaSoli >= CONVERT(DATETIME, '" & filtroFeDesde.Text & "')"
        End If

        If filtroFeHasta.Text <> "" Then
            sFiltro = sFiltro + " AND S.FechaSoli < DATEADD(DAY, 1, CONVERT(DATETIME, '" & filtroFeHasta.Text & "'))"
        End If



        If filtroOrigen.SelectedItem.Text <> NO_FILTRADO Then
            sFiltro = sFiltro + " AND S.OrigenCreacion = '" & filtroOrigen.SelectedItem.Value & "'"
        End If


        If filtroNombreSoli.Text <> "" Then
            sFiltro = sFiltro + " AND S.NombreSolicitante like '" & filtroNombreSoli.Text & "'"
        End If


        If filtroClase.SelectedItem.Text <> NO_FILTRADO Then
            sFiltro = sFiltro + " AND S.CodClase = '" & filtroClase.SelectedItem.Value & "'"
        End If

        If filtroTipo.SelectedItem.Text <> NO_FILTRADO Then
            sFiltro = sFiltro + " AND S.CodTipo = '" & filtroTipo.SelectedItem.Value & "'"
        End If

        If filtroSubtipo.SelectedItem.Text <> NO_FILTRADO Then
            sFiltro = sFiltro + " AND S.CodSubtipo = '" & filtroSubtipo.SelectedItem.Value & "'"
        End If


        If filtroInscripcion.Text <> "" Then
            sFiltro = sFiltro + " AND S.Inscripcion like '" & filtroInscripcion.Text & "'"
        End If

        If filtroDescripcion.Text <> "" Then
            sFiltro = sFiltro + " AND S.Descripcion like '" & filtroDescripcion.Text & "'"
        End If


        If filtroEstado.SelectedItem.Text <> NO_FILTRADO Then
            sFiltro = sFiltro + " AND S.CodEstado = '" & filtroEstado.SelectedItem.Value & "'"
        End If

        If Me.filtroEmail.Text <> "" Then
            sFiltro = sFiltro + " AND S.EMailSolicitante like '" & filtroEmail.Text & "'"
        End If


        If Me.filtroRefExpediente.Text <> "" Then
            sFiltro = sFiltro + " AND S.RefExpediente like '" & Me.filtroRefExpediente.Text & "'"
        End If

        If Me.filtroCodSICA.Text <> "" Then
            sFiltro = sFiltro + " AND S.CodSICA like '" & filtroCodSICA.Text & "'"
        End If

        sentenciaOrder = " ORDER BY S.CodSoli DESC "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If

        daPeticiones.SelectCommand.CommandText = sentenciaSel
        'daCauces.Fill(dstCauces, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaCauces")
        daPeticiones.Fill(dstPeticiones, "ConsultaPeticiones")
        'Cálculo del número de páginas
        'Dim txtComando As String = ""
        'txtComando = daPeticiones.SelectCommand.CommandText
        'txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        'ucPaginacion.calcularPags(txtComando)
        'ncm calculamos el nº de registros que devolverá el filtro 
        'txtNumRegFiltrados.Text = FuncionesGenerales.CalcularNumRegFiltrados(Session("FiltroCauce"), conexion, sFiltro, "PVYCR_Cauces", Page)

        rptPeticiones.DataSource = dstPeticiones.Tables("ConsultaPeticiones")
        rptPeticiones.DataBind()
        rptPeticiones.Visible = True


    End Sub

    Protected Sub rellenarListasFiltro()
        'Dim dstTipoCauce As New DataSet
        Dim dstFiltrado As New DataSet
        Try

            'Dim sql As String
            'ncm 20110224 se comenta porque se cambia el filtrado de la pantalla
            'RDF 20081003
            'Se obtienen los distintos tipos de cauces que hay
            'utiles.Comprobar_Conexion_BD(Page, conexion)
            'sql = " SELECT TipoCauce  FROM PVYCR_Cauces  WHERE Not TipoCauce Is NULL GROUP BY TipoCauce ORDER BY TipoCauce "

            'Dim daCauces As New SqlDataAdapter(sql, conexion)
            'RDF 20081003
            'Se obtienen los distintos tipos de cauces que hay
            'daCauces.Fill(dstTipoCauce, "TablaTipoCauce")
            'ddlTipoCauce.DataSource = dstTipoCauce.Tables("TablaTipoCauce")
            'ddlTipoCauce.DataValueField = "TipoCauce"
            'ddlTipoCauce.DataBind()
            'ddlTipoCauce.Items.Insert(0, New ListItem("[Seleccionar]", ""))

            'ncm rellenamos el campo filtrado con las columnas de la tabla de cauces
            utiles.Comprobar_Conexion_BD(Page, conexion)


            daPeticiones.SelectCommand.CommandText = " SELECT CodOrigen as value, DesOrigen as text  FROM PEINF_ORIGEN ORDER BY DesOrigen "
            daPeticiones.Fill(dstPeticiones, "TablaOrigen")

            filtroOrigen.DataSource = dstPeticiones.Tables("TablaOrigen")
            filtroOrigen.DataValueField = "value"
            filtroOrigen.DataTextField = "text"
            filtroOrigen.DataBind()
            filtroOrigen.Items.Insert(0, New ListItem(NO_FILTRADO, ""))


            daPeticiones.SelectCommand.CommandText = " SELECT CodClase as value, DesClase as text  FROM PEINF_CLASE ORDER BY DesClase "
            daPeticiones.Fill(dstPeticiones, "TablaClase")

            filtroClase.DataSource = dstPeticiones.Tables("TablaClase")
            filtroClase.DataValueField = "value"
            filtroClase.DataTextField = "text"
            filtroClase.DataBind()
            filtroClase.Items.Insert(0, New ListItem(NO_FILTRADO, ""))


            daPeticiones.SelectCommand.CommandText = " SELECT CodTipo as value, DesTipo as text  FROM PEINF_TIPO ORDER BY DesTipo "
            daPeticiones.Fill(dstPeticiones, "TablaTipo")

            filtroTipo.DataSource = dstPeticiones.Tables("TablaTipo")
            filtroTipo.DataValueField = "value"
            filtroTipo.DataTextField = "text"
            filtroTipo.DataBind()
            filtroTipo.Items.Insert(0, New ListItem(NO_FILTRADO, ""))


            daPeticiones.SelectCommand.CommandText = " SELECT CodSubtipo as value, Dessubtipo as text  FROM PEINF_SUBTIPO ORDER BY Dessubtipo "
            daPeticiones.Fill(dstPeticiones, "TablaSubtipo")

            filtroSubtipo.DataSource = dstPeticiones.Tables("TablaSubtipo")
            filtroSubtipo.DataValueField = "value"
            filtroSubtipo.DataTextField = "text"
            filtroSubtipo.DataBind()
            filtroSubtipo.Items.Insert(0, New ListItem(NO_FILTRADO, ""))

            daPeticiones.SelectCommand.CommandText = " SELECT CodEstado as value, Desestado as text  FROM PEINF_ESTADO ORDER BY DesEstado "
            daPeticiones.Fill(dstPeticiones, "TablaEstado")

            filtroEstado.DataSource = dstPeticiones.Tables("TablaEstado")
            filtroEstado.DataValueField = "value"
            filtroEstado.DataTextField = "text"
            filtroEstado.DataBind()
            filtroEstado.Items.Insert(0, New ListItem(NO_FILTRADO, ""))


            daPeticiones.SelectCommand.CommandText = " SELECT CodUsuario as value, NombreCompleto as text  FROM PEINF_ASIGNADO ORDER BY NombreCompleto "
            daPeticiones.Fill(dstPeticiones, "TablaAsignado")

            filtroAsignado.DataSource = dstPeticiones.Tables("TablaAsignado")
            filtroAsignado.DataValueField = "value"
            filtroAsignado.DataTextField = "text"
            filtroAsignado.DataBind()
            filtroAsignado.Items.Insert(0, New ListItem(NO_FILTRADO, ""))

        Catch ex As Exception

        End Try



    End Sub

    Protected Sub rellenarListasEdicion()
        Dim dstFiltrado As New DataSet
        Try


            utiles.Comprobar_Conexion_BD(Page, conexion)


            If IsNothing(dstPeticiones.Tables("TablaOrigen")) Then
                daPeticiones.SelectCommand.CommandText = " SELECT CodOrigen as value, DesOrigen as text  FROM PEINF_ORIGEN ORDER BY DesOrigen "
                daPeticiones.Fill(dstPeticiones, "TablaOrigen")

                edicionOrigen.DataSource = dstPeticiones.Tables("TablaOrigen")
                edicionOrigen.DataValueField = "value"
                edicionOrigen.DataTextField = "text"
                edicionOrigen.DataBind()
                edicionOrigen.Items.Insert(0, New ListItem(NO_VALOR, ""))

            End If



            If IsNothing(dstPeticiones.Tables("TablaClase")) Then
                daPeticiones.SelectCommand.CommandText = " SELECT CodClase as value, DesClase as text  FROM PEINF_CLASE ORDER BY DesClase "
                daPeticiones.Fill(dstPeticiones, "TablaClase")

                edicionClase.DataSource = dstPeticiones.Tables("TablaClase")
                edicionClase.DataValueField = "value"
                edicionClase.DataTextField = "text"
                edicionClase.DataBind()
                edicionClase.Items.Insert(0, New ListItem(NO_VALOR, ""))
            End If




            If (edicionClase.SelectedItem.Text <> NO_VALOR) Then
                daPeticiones.SelectCommand.CommandText = " SELECT CodTipo as value, DesTipo as text  FROM PEINF_TIPO WHERE CodClase = '" & edicionClase.SelectedItem.Value & "' ORDER BY DesTipo "
                daPeticiones.Fill(dstPeticiones, "TablaTipo")

                edicionTipo.DataSource = dstPeticiones.Tables("TablaTipo")
                edicionTipo.DataValueField = "value"
                edicionTipo.DataTextField = "text"
                edicionTipo.DataBind()
                edicionTipo.Items.Insert(0, New ListItem(NO_VALOR, ""))
                edicionTipo.Enabled = True
            Else
                edicionTipo.Enabled = False
                edicionTipo.Items.Clear()
                edicionTipo.Items.Insert(0, New ListItem(NO_VALOR, ""))

            End If

            If IsNothing(dstPeticiones.Tables("TablaSubtipo")) Then
                daPeticiones.SelectCommand.CommandText = " SELECT CodSubtipo as value, Dessubtipo as text  FROM PEINF_SUBTIPO ORDER BY Dessubtipo "
                daPeticiones.Fill(dstPeticiones, "TablaSubtipo")

                edicionSubtipo.DataSource = dstPeticiones.Tables("TablaSubtipo")
                edicionSubtipo.DataValueField = "value"
                edicionSubtipo.DataTextField = "text"
                edicionSubtipo.DataBind()
                edicionSubtipo.Items.Insert(0, New ListItem(NO_VALOR, ""))
            End If




            If IsNothing(dstPeticiones.Tables("TablaEstado")) Then
                daPeticiones.SelectCommand.CommandText = " SELECT CodEstado as value, Desestado as text  FROM PEINF_ESTADO ORDER BY DesEstado "
                daPeticiones.Fill(dstPeticiones, "TablaEstado")

                edicionEstado.DataSource = dstPeticiones.Tables("TablaEstado")
                edicionEstado.DataValueField = "value"
                edicionEstado.DataTextField = "text"
                edicionEstado.DataBind()
                edicionEstado.Items.Insert(0, New ListItem(NO_VALOR, ""))

            End If




            If IsNothing(dstPeticiones.Tables("TablaAsignado")) Then
                daPeticiones.SelectCommand.CommandText = " SELECT CodUsuario as value, NombreCompleto as text  FROM PEINF_ASIGNADO ORDER BY NombreCompleto "
                daPeticiones.Fill(dstPeticiones, "TablaAsignado")

                edicionAsignado.DataSource = dstPeticiones.Tables("TablaAsignado")
                edicionAsignado.DataValueField = "value"
                edicionAsignado.DataTextField = "text"
                edicionAsignado.DataBind()
                edicionAsignado.Items.Insert(0, New ListItem(NO_VALOR, ""))


                edAcReAsigAsignado.DataSource = dstPeticiones.Tables("TablaAsignado")
                edAcReAsigAsignado.DataValueField = "value"
                edAcReAsigAsignado.DataTextField = "text"
                edAcReAsigAsignado.DataBind()
                edAcReAsigAsignado.Items.Insert(0, New ListItem(NO_VALOR, ""))

                edAcReReasigAsignado.DataSource = dstPeticiones.Tables("TablaAsignado")
                edAcReReasigAsignado.DataValueField = "value"
                edAcReReasigAsignado.DataTextField = "text"
                edAcReReasigAsignado.DataBind()
                edAcReReasigAsignado.Items.Insert(0, New ListItem(NO_VALOR, ""))

            End If



            ' Relleno desplegable de firma requerida
            edicionRequiereFirma.Items.Clear()
            edicionRequiereFirma.Items.Insert(0, New ListItem(NO_VALOR, ""))
            edicionRequiereFirma.Items.Insert(1, New ListItem("Jefe de Servicio", "JS"))
            edicionRequiereFirma.Items.Insert(2, New ListItem("Asignado", "AS"))

        Catch ex As Exception

        End Try



    End Sub

    Protected Sub limpiarCamposEdicion()
        edicionNumsoli.Text = ""
        edicionFecha.Text = ""

        edicionOrigen.SelectedIndex = 0
        edicionNombreSoli.Text = ""

        edicionClase.SelectedIndex = 0
        edicionTipo.SelectedIndex = 0
        edicionSubtipo.SelectedIndex = 0

        edicionRequiereFirma.SelectedIndex = 0
        edicionFeConsuDesde.Text = ""
        edicionFeConsuHasta.Text = ""

        edicionInscripcion.Text = ""
        edicionDescripcion.Text = ""

        edicionEstado.SelectedIndex = 0
        edicionEmail.Text = ""
        edicionFechaEstado.Text = ""
        edicionAsignado.SelectedIndex = 0

        edAcSoMotivoAnu.Text = ""
        edAcReAsigAsignado.SelectedIndex = 0

        edAcSoMotivoRepe.Text = ""
        edAcReMotivoAnu.Text = ""
        edAcRePedirReviAsigMotivo.Text = ""
        edAcReReasigMotivo.Text = ""
        edAcReReasigAsignado.SelectedIndex = 0

        edicionRefExpediente.Text = ""
        edicionCodSICA.Text = ""

    End Sub


    Protected Sub rptPeticiones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptPeticiones.ItemCommand

        Dim parametros() As String

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"

                rellenarListasEdicion()
                limpiarCamposEdicion()

                daPeticiones.SelectCommand.CommandText = " SELECT CodSoli" & _
                                                         " ,FechaSoli" & _
                                                         " ,CodEstado" & _
                                                         " ,OrigenCreacion" & _
                                                         " ,UsuCreacion" & _
                                                         " ,NombreSolicitante" & _
                                                         " ,EMailSolicitante" & _
                                                         " ,CodClase" & _
                                                         " ,CodTipo" & _
                                                         " ,CodSubtipo" & _
                                                         " ,Inscripcion" & _
                                                         " ,Descripcion" & _
                                                         " ,RequiereFirma" & _
                                                         " ,CodSICA" & _
                                                         " ,RefExpediente" & _
                                                         " ,CONVERT(VARCHAR, FeConsuDesde, 103) AS FeConsuDesde" & _
                                                         " ,CONVERT(VARCHAR, FeConsuHasta, 103) AS FeConsuHasta" & _
                                                         " FROM PEINF_SOLICITUD WHERE CodSoli = " & parametros(0)

                daPeticiones.Fill(dstPeticiones, "TablaSolicitud")


                edicionNumsoli.Text = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("CodSoli"))
                'edicionFecha.Text = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("FechaSoli"))


                edicionRequiereFirma.SelectedValue = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("RequiereFirma"))
                edicionFeConsuDesde.Text = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("FeConsuDesde"))
                edicionFeConsuHasta.Text = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("FeConsuHasta"))

                edicionOrigen.SelectedValue = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("OrigenCreacion"))

                edicionNombreSoli.Text = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("NombreSolicitante"))

                edicionClase.SelectedValue = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("CodClase"))

                edicionSubtipo.SelectedValue = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("CodSubtipo"))

                edicionInscripcion.Text = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("Inscripcion"))
                edicionDescripcion.Text = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("Descripcion"))

                edicionCodSICA.Text = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("CodSICA"))
                edicionRefExpediente.Text = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("RefExpediente"))


                edicionEstado.SelectedValue = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("CodEstado"))
                edicionEmail.Text = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("EMailSolicitante"))

                rellenarListasEdicion()
                ' Despues porque depende de la clase
                edicionTipo.SelectedValue = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("CodTipo"))


                ' Mostramos paneles asociados al roles del usuario
                If Session("loginUsuario") = utiles.nullABlanco(dstPeticiones.Tables("TablaSolicitud").Rows(0).Item("UsuCreacion")) Then
                    Me.pnlEdSolicitante.Visible = True
                Else
                    Me.pnlEdSolicitante.Visible = False
                End If

                visualizarPanelesEdicion()

                RefrescarEstado()

                ' Marcamos panele para visualizar
                pnlEdicion.Visible = True
                pnlConsulta.Visible = False

        End Select
    End Sub

    Protected Sub visualizarPanelesEdicion()
        Dim objTrans As SqlTransaction

        ' Por defecto oculto
        Me.pnlEdAsignado.Visible = False
        Me.pnlEdResponsable.Visible = False

        objTrans = conexion.BeginTransaction()
        comando.Parameters.Clear()


        Try
            comando.Transaction = objTrans

            comando.CommandText = "SELECT COUNT(A.UsuAsignado)  FROM (SELECT top 1 UsuAsignado FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text & " order by CodAsignacion desc) A where  A.UsuAsignado = '" + Session("loginUsuario") + "'"
            If comando.ExecuteScalar().ToString <> "0" Then
                Me.pnlEdAsignado.Visible = True
            End If

            comando.CommandText = "SELECT count(*) FROM PEINF_ASIGNADO WHERE EsResponsable = 1 and CodUsuario = '" + Session("loginUsuario") + "'"
            If comando.ExecuteScalar().ToString <> "0" Then
                Me.pnlEdResponsable.Visible = True

            Else
                ' SI EL USUARIO QUE ASIGNA ES EL ASIGNADO, tambien es responsable
                comando.CommandText = "SELECT COUNT(A.UsuAsignado)  FROM (SELECT top 1 UsuAsignacion, UsuAsignado FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text & " order by CodAsignacion desc) A where  A.UsuAsignacion = A.UsuAsignado AND A.UsuAsignado = '" + Session("loginUsuario") + "'"
                If comando.ExecuteScalar().ToString <> "0" Then
                    Me.pnlEdResponsable.Visible = True
                End If

            End If


        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                Case 2627
                    Alert(Page, "La petición ya existe ")
                Case Else
                    Alert(Page, Exc.Message)
            End Select
        End Try

        objTrans.Commit()

    End Sub
    Protected Sub botonFiltroBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonFiltroBuscar.Click
        generarConsulta()
    End Sub

    Protected Sub botonAcAsigAgregarInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcAsigAgregarInfo.Click

        Dim codigoAsig As String

        If (Me.edicionEstado.SelectedValue <> "PA" And _
            Me.edicionEstado.SelectedValue <> "GR") Then
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")
        Else

            'Agregar a la base de datos la imagen seleccionada.
            If Me.FileUpload.FileName.Length > 0 Then
                utiles.Comprobar_Conexion_BD(Page, conexion)
                comando.Parameters.Clear()
                Try

                    comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0)  FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                    codigoAsig = comando.ExecuteScalar.ToString

                    comando.CommandText = "UPDATE [PEINF_ASIGNACION] " & _
                                          "SET TipoFichero =@TipoFichero " & _
                                          ", Fichero =@Fichero " & _
                                         " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                         "  AND CodAsignacion = " & codigoAsig

                    comando.Parameters.AddWithValue("TipoFichero", utiles.BlancoANull(System.IO.Path.GetExtension(Me.FileUpload.FileName).ToLower))
                    comando.Parameters.AddWithValue("Fichero", Me.FileUpload.FileBytes)


                    comando.ExecuteNonQuery()

                Catch exnull As NullReferenceException
                    Alert(Page, "El tipo de imagen no es correcto")
                Catch Exc As System.Data.SqlClient.SqlException
                    Alert(Page, Exc.Message & " num: " & Exc.Number)
                End Try

            Else
                Alert(Page, "Seleccione un fichero para cargar desde el botón Examinar")
            End If
        End If
    End Sub


    Protected Sub botonAcAsigBorrarInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcAsigBorrarInfo.Click

        Dim codigoAsig As String

        If (Me.edicionEstado.SelectedValue <> "PA" And _
            Me.edicionEstado.SelectedValue <> "GR") Then
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")
        Else
            'Agregar a la base de datos la imagen seleccionada.
            utiles.Comprobar_Conexion_BD(Page, conexion)
            comando.Parameters.Clear()
            Try

                comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0)  FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                codigoAsig = comando.ExecuteScalar.ToString

                comando.CommandText = "UPDATE [PEINF_ASIGNACION] " & _
                                      "SET TipoFichero = null " & _
                                      ", Fichero = null " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig

                comando.ExecuteNonQuery()

            Catch exnull As NullReferenceException
                Alert(Page, "El tipo de imagen no es correcto")
            Catch Exc As System.Data.SqlClient.SqlException
                Alert(Page, Exc.Message & " num: " & Exc.Number)
            End Try
        End If
    End Sub

    Protected Sub visualizarInforme()
        Dim codigoAsig As String
        Dim extension As String
        Dim oFileStream As System.IO.FileStream
        Dim myreader As SqlDataReader
        Dim nombreFi As String
        Dim downfile As FileInfo

        Dim aux As Object


        'Agregar a la base de datos la imagen seleccionada.
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        Try

            comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0)  FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
            codigoAsig = comando.ExecuteScalar.ToString

            comando.CommandText = "SELECT TipoFichero FROM [PEINF_ASIGNACION] " & _
                                 " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                 "  AND CodAsignacion = " & codigoAsig
            aux = comando.ExecuteScalar

            If (aux Is Nothing Or aux.ToString.Trim = "") Then
                Alert(Page, "No se ha añadido fichero")
            Else

                extension = aux.ToString

                ' Para hacerlo en produccion y servidor de pruebas
                nombreFi = "..\tmp\" & Me.edicionNumsoli.Text & "_" & codigoAsig & extension
                downfile = New FileInfo(Server.MapPath(nombreFi))
                oFileStream = New System.IO.FileStream(downfile.FullName, System.IO.FileMode.Create)

                ' Para hacerlo en local
                'nombreFi = "C:\Temp" & Me.edicionNumsoli.Text & "_" & codigoAsig & extension
                'downfile = New FileInfo(nombreFi)
                'oFileStream = New System.IO.FileStream(nombreFi, System.IO.FileMode.Create)



                comando.CommandText = "SELECT Fichero FROM [PEINF_ASIGNACION] " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig

                myreader = comando.ExecuteReader

                While (myreader.Read())
                    Dim data As Byte() = myreader(0)
                    oFileStream.Write(data, 0, data.Length)
                End While
                oFileStream.Close()

                Response.ClearContent()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + downfile.Name)
                Response.AddHeader("Content-Length", downfile.Length.ToString())
                Response.ContentType = "application/vnd.android.package-archive"
                Response.TransmitFile(downfile.FullName)
                Response.Flush()
                Response.End()
                Response.Redirect(Request.UrlReferrer.ToString)

            End If


        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message & " num: " & Exc.Number)
        End Try

    End Sub

    Protected Sub botonAcAsigVisualizarInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcAsigVisualizarInfo.Click
        visualizarInforme()
    End Sub


    Protected Sub botonAcSoVisualizarInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcSoVisualizarInfo.Click
        If (Me.edicionEstado.SelectedValue = "GV" Or _
            Me.edicionEstado.SelectedValue = "AR" Or _
            Me.edicionEstado.SelectedValue = "AS" Or _
            Me.edicionEstado.SelectedValue = "PD") Then
            visualizarInforme()
        Else
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")
        End If
    End Sub


    Protected Sub botonAcReVisualizarInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcReVisualizarInfo.Click
        If (Me.edicionEstado.SelectedValue = "GP" Or _
            Me.edicionEstado.SelectedValue = "GV" Or _
            Me.edicionEstado.SelectedValue = "AR" Or _
            Me.edicionEstado.SelectedValue = "AS" Or _
            Me.edicionEstado.SelectedValue = "PD") Then
            visualizarInforme()
        Else
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")
        End If
    End Sub


    Protected Sub botonFiltroLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonFiltroLimpiar.Click

        filtroNumsoli.Text = ""
        filtroFeDesde.Text = ""
        filtroFeHasta.Text = ""

        filtroOrigen.SelectedIndex = 0
        filtroNombreSoli.Text = ""

        filtroClase.SelectedIndex = 0
        filtroTipo.SelectedIndex = 0
        filtroSubtipo.SelectedIndex = 0

        filtroInscripcion.Text = ""
        filtroDescripcion.Text = ""

        filtroEstado.SelectedIndex = 0
        filtroEmail.Text = ""

        filtroAsignado.SelectedIndex = 0

        rptPeticiones.Visible = False

    End Sub


    Protected Sub botonFiltroNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonFiltroNuevo.Click

        rellenarListasEdicion()
        limpiarCamposEdicion()
        rellenarListasEdicion()

        Me.pnlEdSolicitante.Visible = True
        visualizarPanelesEdicion()

        pnlEdicion.Visible = True
        pnlConsulta.Visible = False
    End Sub


    Protected Sub botonEdicionSalirNoGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonEdicionSalirNoGrabar.Click

        generarConsulta()
        pnlEdicion.Visible = False
        pnlConsulta.Visible = True

    End Sub


    Protected Sub grabar()

        Dim codSoliRef As String = ""
        Dim objTrans As SqlTransaction
        Dim nuevo As Boolean

        utiles.Comprobar_Conexion_BD(Page, conexion)

        objTrans = conexion.BeginTransaction()
        comando.Parameters.Clear()

        nuevo = False

        Try
            comando.Transaction = objTrans

            If (Me.edicionNumsoli.Text <> "") Then

                ' Se trata de una actualización
                codSoliRef = Me.edicionNumsoli.Text
                comando.CommandText = "  UPDATE PEINF_SOLICITUD SET " & _
                                     "  OrigenCreacion = @OrigenCreacion " & _
                                     " ,NombreSolicitante = @NombreSolicitante " & _
                                     " ,EMailSolicitante = @EMailSolicitante " & _
                                     " ,CodClase = @CodClase " & _
                                     " ,CodTipo= @CodTipo " & _
                                     " ,CodSubtipo= @CodSubtipo " & _
                                     " ,Inscripcion= @Inscripcion " & _
                                     " ,Descripcion= @Descripcion " & _
                                     " ,RefExpediente= @RefExpediente " & _
                                     " ,CodSICA= @CodSICA " & _
                                     " ,RequiereFirma= @RequiereFirma " & _
                                     " ,FeConsuDesde= @FeConsuDesde " & _
                                     " ,FeConsuHasta= @FeConsuHasta " & _
                                     "  WHERE CodSoli = " & Me.edicionNumsoli.Text

            Else

                ' Obtenemos siguiente número de petición
                comando.CommandText = "SELECT ISNULL(MAX(CodSoli),0) + 1 FROM PEINF_SOLICITUD "
                codSoliRef = comando.ExecuteScalar().ToString
                nuevo = True
                comando.CommandText = " INSERT INTO PEINF_SOLICITUD" & _
                                     " (CodSoli" & _
                                     " ,FechaSoli" & _
                                     " ,CodEstado" & _
                                     " ,OrigenCreacion" & _
                                     " ,UsuCreacion" & _
                                     " ,NombreSolicitante" & _
                                     " ,EMailSolicitante" & _
                                     " ,CodClase" & _
                                     " ,CodTipo" & _
                                     " ,CodSubtipo" & _
                                     " ,Inscripcion" & _
                                     " ,Descripcion" & _
                                     " ,RefExpediente" & _
                                     " ,RequiereFirma" & _
                                     " ,FeConsuDesde" & _
                                     " ,FeConsuHasta" & _
                                     " ,CodSICA)" & _
                                     "  VALUES " & _
                                     " (@CodSoli" & _
                                     " ,getDate()" & _
                                     " ,@CodEstado" & _
                                     " ,@OrigenCreacion" & _
                                     " ,@UsuCreacion" & _
                                     " ,@NombreSolicitante" & _
                                     " ,@EMailSolicitante" & _
                                     " ,@CodClase" & _
                                     " ,@CodTipo" & _
                                     " ,@CodSubtipo" & _
                                     " ,@Inscripcion" & _
                                     " ,@Descripcion" & _
                                     " ,@RefExpediente" & _
                                     " ,@RequiereFirma" & _
                                     " ,@FeConsuDesde" & _
                                     " ,@FeConsuHasta" & _
                                     " ,@CodSICA)"

                comando.Parameters.AddWithValue("CodSoli", codSoliRef)
                comando.Parameters.AddWithValue("CodEstado", "PN")
                comando.Parameters.AddWithValue("UsuCreacion", Session("loginUsuario"))


            End If

            comando.Parameters.AddWithValue("OrigenCreacion", utiles.BlancoANull(Me.edicionOrigen.Text))
            comando.Parameters.AddWithValue("NombreSolicitante", utiles.BlancoANull(Me.edicionNombreSoli.Text))
            comando.Parameters.AddWithValue("EMailSolicitante", utiles.BlancoANull(Me.edicionEmail.Text))
            comando.Parameters.AddWithValue("CodClase", utiles.BlancoANull(Me.edicionClase.Text))
            comando.Parameters.AddWithValue("CodTipo", utiles.BlancoANull(Me.edicionTipo.Text))
            comando.Parameters.AddWithValue("CodSubtipo", utiles.BlancoANull(Me.edicionSubtipo.Text))
            comando.Parameters.AddWithValue("Inscripcion", utiles.BlancoANull(Me.edicionInscripcion.Text))
            comando.Parameters.AddWithValue("Descripcion", utiles.BlancoANull(Me.edicionDescripcion.Text))

            comando.Parameters.AddWithValue("RefExpediente", utiles.BlancoANull(Me.edicionRefExpediente.Text))
            comando.Parameters.AddWithValue("CodSICA", utiles.BlancoANull(Me.edicionCodSICA.Text))

            comando.Parameters.AddWithValue("RequiereFirma", utiles.BlancoANull(Me.edicionRequiereFirma.Text))
            comando.Parameters.AddWithValue("FeConsuDesde", utiles.BlancoANull(Me.edicionFeConsuDesde.Text))
            comando.Parameters.AddWithValue("FeConsuHasta", utiles.BlancoANull(Me.edicionFeConsuHasta.Text))

            comando.ExecuteNonQuery()

            objTrans.Commit()

            Me.edicionNumsoli.Text = codSoliRef

            RefrescarEstado()

            If nuevo Then
                enviarCorreo(Me.edicionEmail.Text, "Notificación Petición Informes", "Nueva petición de informes. Número: " + codSoliRef)
                enviarCorreo(correosResponsables(), "Notificación Petición Informes", "Nueva petición de informes. Número: " + codSoliRef)

                Dim sender As Object = New Object
                Dim e As System.EventArgs = New System.EventArgs

                ' En caso de que el usuario solicitante sea asignado y la petición tenga origen
                ' diferente de online se le asigna al asignado directamente
                If Me.esAsignado And Me.edicionOrigen.SelectedValue <> "UC" Then
                    Me.edAcReAsigAsignado.SelectedValue = Session("loginUsuario")
                    Me.botonAcReAsig_Click(sender, e)
                End If

            End If

        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                Case 2627
                    Alert(Page, "La petición ya existe ")
                Case Else
                    Alert(Page, Exc.Message)
            End Select
            objTrans.Rollback()
        End Try

    End Sub

    Protected Function comprobarDatos() As Boolean
        'Debemos comprobar los campos
        comprobarDatos = False

        If Not (Me.edicionOrigen.Text <> "") Then
            Alert(Page, "Debe indicarse el origen de la solicitud")
        ElseIf Not (Me.edicionNombreSoli.Text.Trim <> "") Then
            Alert(Page, "Debe indicarse el nombre del solicitante")
        ElseIf (Me.edicionNombreSoli.Text.Trim.Length > 50) Then
            Alert(Page, "El nombre del solicitante supera 50 caracteres")
        ElseIf Not (Me.edicionEmail.Text.Trim <> "") Then
            Alert(Page, "Debe indicarse el email del solicitante")
        ElseIf (Not Me.edicionEmail.Text.Contains("@")) Then
            Alert(Page, "El email no tiene formato adecuado")
        ElseIf (Me.edicionEmail.Text.Length > 60) Then
            Alert(Page, "El email del solicitante supera 60 caracteres")
        ElseIf (Me.edicionClase.SelectedValue = NO_VALOR) Then
            Alert(Page, "Debe indicarse la clase de la solicitud")
        ElseIf (Me.edicionTipo.SelectedValue = NO_VALOR Or _
                Me.edicionTipo.SelectedValue.Trim.Length = 0) Then
            Alert(Page, "Debe indicarse el tipo de la solicitud")
        ElseIf (Me.edicionInscripcion.Text.Length > 4) Then
            Alert(Page, "La inscripción no puede superar los 4 dígitos")
        ElseIf (Me.edicionInscripcion.Text.Trim.Length > 0 And _
                Not IsNumeric(Me.edicionInscripcion.Text)) Then
            Alert(Page, "La inscripción debe ser un número")
        ElseIf (Me.edicionDescripcion.Text.Trim.Length > 150) Then
            Alert(Page, "La descripción no puede superar los 150 caracteres")
        ElseIf (Me.edicionDescripcion.Text.Trim.Length = 0 And Me.edicionInscripcion.Text.Trim.Length = 0) Then
            Alert(Page, "Debe indicarse un código de inscripción o una descripción")
        ElseIf (Me.edicionRefExpediente.Text.Length > 15) Then
            Alert(Page, "El código de expediente no puede superar los 15 caracteres")
        ElseIf (Me.edicionCodSICA.Text.Length > 15) Then
            Alert(Page, "El código SICA no puede superar los 15 caracteres")
        Else
            comprobarDatos = True
        End If


    End Function

    Protected Sub botonEdicionGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonEdicionGrabar.Click
        If comprobarDatos() Then
            grabar()
            visualizarPanelesEdicion()
        End If

    End Sub

    Protected Sub botonEdicionSalirGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonEdicionSalirGrabar.Click
        If comprobarDatos() Then
            grabar()
            visualizarPanelesEdicion()
            generarConsulta()
            pnlEdicion.Visible = False
            pnlConsulta.Visible = True
        End If
    End Sub

    Protected Sub botonAcSoAnular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcSoAnular.Click

        Dim objTrans As SqlTransaction
        Dim codigoAsig As String


        If (Me.edicionEstado.SelectedValue = "AR" Or _
            Me.edicionEstado.SelectedValue = "AS") Then
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")

        ElseIf Me.edAcSoMotivoAnu.Text.Trim = "" Then
            Alert(Page, "Debe indicar motivo de anulación")

        ElseIf Me.edAcSoMotivoAnu.Text.Length > 50 Then
            Alert(Page, "El motivo no puede superar los 50 caracteres")

        Else

            utiles.Comprobar_Conexion_BD(Page, conexion)
            objTrans = conexion.BeginTransaction()
            comando.Parameters.Clear()
            Try
                comando.Transaction = objTrans

                comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0)  FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                codigoAsig = comando.ExecuteScalar.ToString

                If codigoAsig = "0" Then

                    codigoAsig = "1"
                    comando.CommandText = " INSERT INTO PEINF_ASIGNACION" & _
                                         " (CodSoli" & _
                                         " ,CodAsignacion)" & _
                                         "  VALUES " & _
                                         " (@CodSoli" & _
                                         " ,@CodAsignacion)"

                    comando.Parameters.AddWithValue("CodSoli", Me.edicionNumsoli.Text)
                    comando.Parameters.AddWithValue("CodAsignacion", codigoAsig)

                    comando.ExecuteNonQuery()
                End If

                comando.CommandText = " UPDATE PEINF_ASIGNACION" & _
                                     " SET UsuAnulador = @UsuAnulador " & _
                                     " ,FechaAnulacion = getDate() " & _
                                     " ,MotivoAnuRevi = @MotivoAnuRevi " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig

                comando.Parameters.AddWithValue("UsuAnulador", Session("loginUsuario"))
                comando.Parameters.AddWithValue("MotivoAnuRevi", Me.edAcSoMotivoAnu.Text.Trim & " (ANULACIÓN)")

                comando.ExecuteNonQuery()


                comando.CommandText = "UPDATE PEINF_SOLICITUD SET CodEstado = 'AS' WHERE CodSoli = " & Me.edicionNumsoli.Text
                comando.ExecuteNonQuery()

                objTrans.Commit()


                RefrescarEstado()
                enviarCorreo(Me.edicionEmail.Text, "Notificación Petición Informes", "Petición anulada por solicitante. Número: " + Me.edicionNumsoli.Text)
                enviarCorreo(correosResponsables(), "Notificación Petición Informes", "Petición anulada por solicitante. Número: " + Me.edicionNumsoli.Text)




            Catch Exc As System.Data.SqlClient.SqlException
                Select Case Exc.Number
                    Case 2627
                        Alert(Page, "La petición ya existe ")
                    Case Else
                        Alert(Page, Exc.Message)
                End Select
                objTrans.Rollback()
            End Try


        End If

    End Sub

    Protected Sub botonAcReAnular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcReAnular.Click

        Dim objTrans As SqlTransaction
        Dim codigoAsig As String

        If (Me.edicionEstado.SelectedValue = "AR" Or _
            Me.edicionEstado.SelectedValue = "AS") Then
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")

        ElseIf Me.edAcReMotivoAnu.Text.Trim = "" Then
            Alert(Page, "Debe indicar motivo de anulación")

        ElseIf Me.edAcReMotivoAnu.Text.Length > 50 Then
            Alert(Page, "El motivo no puede superar los 50 caracteres")

        Else

            utiles.Comprobar_Conexion_BD(Page, conexion)
            objTrans = conexion.BeginTransaction()
            comando.Parameters.Clear()
            Try
                comando.Transaction = objTrans

                comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0)  FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                codigoAsig = comando.ExecuteScalar.ToString

                If codigoAsig = "0" Then

                    codigoAsig = "1"
                    comando.CommandText = " INSERT INTO PEINF_ASIGNACION" & _
                                         " (CodSoli" & _
                                         " ,CodAsignacion)" & _
                                         "  VALUES " & _
                                         " (@CodSoli" & _
                                         " ,@CodAsignacion)"

                    comando.Parameters.AddWithValue("CodSoli", Me.edicionNumsoli.Text)
                    comando.Parameters.AddWithValue("CodAsignacion", codigoAsig)

                    comando.ExecuteNonQuery()
                End If

                comando.CommandText = " UPDATE PEINF_ASIGNACION" & _
                                     " SET UsuAnulador = @UsuAnulador " & _
                                     " ,FechaAnulacion = getDate() " & _
                                     " ,MotivoAnuRevi = @MotivoAnuRevi " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig

                comando.Parameters.AddWithValue("UsuAnulador", Session("loginUsuario"))
                comando.Parameters.AddWithValue("MotivoAnuRevi", Me.edAcReMotivoAnu.Text.Trim & " (ANULACIÓN)")

                comando.ExecuteNonQuery()


                comando.CommandText = "UPDATE PEINF_SOLICITUD SET CodEstado = 'AR' WHERE CodSoli = " & Me.edicionNumsoli.Text
                comando.ExecuteNonQuery()

                objTrans.Commit()

                RefrescarEstado()
                enviarCorreo(Me.edicionEmail.Text, "Notificación Petición Informes", "Petición anulada por responsable. Número: " + Me.edicionNumsoli.Text)
                enviarCorreo(correosResponsables(), "Notificación Petición Informes", "Petición anulada por responsable. Número: " + Me.edicionNumsoli.Text)




            Catch Exc As System.Data.SqlClient.SqlException
                Select Case Exc.Number
                    Case 2627
                        Alert(Page, "La petición ya existe ")
                    Case Else
                        Alert(Page, Exc.Message)
                End Select
                objTrans.Rollback()
            End Try


        End If

    End Sub


    Protected Sub botonAcAsigPedirValiInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcAsigPedirValiInfo.Click

        Dim objTrans As SqlTransaction
        Dim codigoAsig As String
        Dim aux As Object

        If (Me.edicionEstado.SelectedValue <> "PA" And _
            Me.edicionEstado.SelectedValue <> "GR") Then
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")

        Else

            utiles.Comprobar_Conexion_BD(Page, conexion)
            objTrans = conexion.BeginTransaction()
            comando.Parameters.Clear()
            Try
                comando.Transaction = objTrans
                'aa
                comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0)  FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                codigoAsig = comando.ExecuteScalar.ToString

                comando.CommandText = "SELECT TipoFichero FROM [PEINF_ASIGNACION] " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig
                aux = comando.ExecuteScalar

                If (aux Is Nothing Or aux.ToString.Trim = "") Then
                    Alert(Page, "Debe añadirse fichero para poder pedir la validación por parte del responsable")
                Else

                    comando.CommandText = " UPDATE PEINF_ASIGNACION" & _
                                         " SET FechaGeneracion = getDate() " & _
                                         " ,MotivoAnuRevi = '' " & _
                                         " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                         "  AND CodAsignacion = " & codigoAsig

                    comando.ExecuteNonQuery()


                    comando.CommandText = "UPDATE PEINF_SOLICITUD SET CodEstado = 'GP' WHERE CodSoli = " & Me.edicionNumsoli.Text
                    comando.ExecuteNonQuery()
                End If


                objTrans.Commit()
                RefrescarEstado()

                enviarCorreo(correosResponsables(), "Notificación Petición Informes", "Petición pendiente de validación por parte de responsable. Número: " + Me.edicionNumsoli.Text)




            Catch Exc As System.Data.SqlClient.SqlException
                Select Case Exc.Number
                    Case 2627
                        Alert(Page, "La petición ya existe ")
                    Case Else
                        Alert(Page, Exc.Message)
                End Select
                objTrans.Rollback()
            End Try


        End If

    End Sub



    Protected Sub botonAcRePedirReviAsig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcRePedirReviAsig.Click


        Dim objTrans As SqlTransaction
        Dim codigoAsig As String
        Dim usuAsig As String


        If (Me.edicionEstado.SelectedValue <> "GP") Then
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")

        ElseIf Me.edAcRePedirReviAsigMotivo.Text.Trim = "" Then
            Alert(Page, "Debe indicar motivo de la revisión")

        ElseIf Me.edAcRePedirReviAsigMotivo.Text.Length > 50 Then
            Alert(Page, "El motivo no puede superar los 50 caracteres")

        Else

            utiles.Comprobar_Conexion_BD(Page, conexion)
            objTrans = conexion.BeginTransaction()
            comando.Parameters.Clear()
            Try
                comando.Transaction = objTrans

                comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0)  FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                codigoAsig = comando.ExecuteScalar.ToString

                comando.CommandText = " UPDATE PEINF_ASIGNACION" & _
                                     " SET UsuPetiRevision = @UsuPetiRevision " & _
                                     " ,FechaPetiRevision = getDate() " & _
                                     " ,MotivoAnuRevi = @MotivoAnuRevi " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig

                comando.Parameters.AddWithValue("UsuPetiRevision", Session("loginUsuario"))
                comando.Parameters.AddWithValue("MotivoAnuRevi", Me.edAcRePedirReviAsigMotivo.Text.Trim & " (REVISIÓN)")

                comando.ExecuteNonQuery()


                comando.CommandText = "UPDATE PEINF_SOLICITUD SET CodEstado = 'GR' WHERE CodSoli = " & Me.edicionNumsoli.Text
                comando.ExecuteNonQuery()

                comando.CommandText = "SELECT  UsuAsignado FROM PEINF_ASIGNACION " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig
                usuAsig = comando.ExecuteScalar.ToString

                objTrans.Commit()

                RefrescarEstado()
                enviarCorreo(correoAsignado(usuAsig), "Notificación Petición Informes", "Petición pendiente de revisión por parte del asignado. Número: " + Me.edicionNumsoli.Text)




            Catch Exc As System.Data.SqlClient.SqlException
                Select Case Exc.Number
                    Case 2627
                        Alert(Page, "La petición ya existe ")
                    Case Else
                        Alert(Page, Exc.Message)
                End Select
                objTrans.Rollback()
            End Try


        End If

    End Sub

    Protected Sub botonAcReValidarInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcReValidarInfo.Click

        Dim objTrans As SqlTransaction
        Dim codigoAsig As String
        Dim usuAsig As String

        If (Me.edicionEstado.SelectedValue <> "GP") Then
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")

        Else

            utiles.Comprobar_Conexion_BD(Page, conexion)
            objTrans = conexion.BeginTransaction()
            comando.Parameters.Clear()
            Try
                comando.Transaction = objTrans

                comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0)  FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                codigoAsig = comando.ExecuteScalar.ToString

                comando.CommandText = " UPDATE PEINF_ASIGNACION" & _
                                     " SET FechaValidacion = getDate() " & _
                                     " ,UsuValidador = @UsuValidador " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig

                comando.Parameters.AddWithValue("UsuValidador", Session("loginUsuario"))
                comando.ExecuteNonQuery()


                comando.CommandText = "UPDATE PEINF_SOLICITUD SET CodEstado = 'GV' WHERE CodSoli = " & Me.edicionNumsoli.Text
                comando.ExecuteNonQuery()

                comando.CommandText = "SELECT  UsuAsignado FROM PEINF_ASIGNACION " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig
                usuAsig = comando.ExecuteScalar.ToString

                objTrans.Commit()

                RefrescarEstado()

                enviarCorreo(Me.edicionEmail.Text, "Notificación Petición Informes", "Petición validada por parte de responsable. Número: " + Me.edicionNumsoli.Text)
                enviarCorreo(correoAsignado(usuAsig), "Notificación Petición Informes", "Petición validada por parte de responsable. Número: " + Me.edicionNumsoli.Text)



            Catch Exc As System.Data.SqlClient.SqlException
                Select Case Exc.Number
                    Case 2627
                        Alert(Page, "La petición ya existe ")
                    Case Else
                        Alert(Page, Exc.Message)
                End Select
                objTrans.Rollback()
            End Try


        End If

    End Sub


    Protected Sub botonAcReAsig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcReAsig.Click

        Dim objTrans As SqlTransaction
        Dim codigoAsig As String
        Dim usuAsig As String


        If (Me.edicionEstado.SelectedValue <> "PN" And _
            Me.edicionEstado.SelectedValue <> "PD") Then
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")

        ElseIf Me.edAcReAsigAsignado.SelectedValue = "" Then
            Alert(Page, "Debe indicar persona que desea asignar")
        Else

            utiles.Comprobar_Conexion_BD(Page, conexion)
            objTrans = conexion.BeginTransaction()
            comando.Parameters.Clear()
            Try
                comando.Transaction = objTrans

                ' Obtenemos siguiente número de petición
                comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0) + 1 FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                codigoAsig = comando.ExecuteScalar.ToString

                comando.CommandText = " INSERT INTO PEINF_ASIGNACION" & _
                                     " (CodSoli" & _
                                     " ,CodAsignacion" & _
                                     " ,UsuAsignacion" & _
                                     " ,UsuAsignado" & _
                                     " ,FechaAsignacion)" & _
                                     "  VALUES " & _
                                     " (@CodSoli" & _
                                     " ,@CodAsignacion" & _
                                     " ,@UsuAsignacion" & _
                                     " ,@UsuAsignado" & _
                                     " ,getDate())"

                comando.Parameters.AddWithValue("CodSoli", Me.edicionNumsoli.Text)
                comando.Parameters.AddWithValue("CodAsignacion", codigoAsig)
                comando.Parameters.AddWithValue("UsuAsignacion", Session("loginUsuario"))
                comando.Parameters.AddWithValue("UsuAsignado", Me.edAcReAsigAsignado.SelectedValue)
                usuAsig = Me.edAcReAsigAsignado.SelectedValue

                comando.ExecuteNonQuery()

                comando.CommandText = "UPDATE PEINF_SOLICITUD SET CodEstado = 'PA' WHERE CodSoli = " & Me.edicionNumsoli.Text
                comando.ExecuteNonQuery()

                objTrans.Commit()

                RefrescarEstado()

                enviarCorreo(Me.edicionEmail.Text, "Notificación Petición Informes", "Petición asignada por parte de responsable. Número: " + Me.edicionNumsoli.Text)
                enviarCorreo(correoAsignado(usuAsig), "Notificación Petición Informes", "Petición asignada por parte de responsable. Número: " + Me.edicionNumsoli.Text)



            Catch Exc As System.Data.SqlClient.SqlException
                Select Case Exc.Number
                    Case 2627
                        Alert(Page, "La petición ya existe ")
                    Case Else
                        Alert(Page, Exc.Message)
                End Select
                objTrans.Rollback()
            End Try


        End If



    End Sub



    Protected Sub botonAcReReasig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcReReasig.Click

        Dim objTrans As SqlTransaction
        Dim codigoAsig As String
        Dim usuAsigAnt As String
        Dim usuAsigNue As String

        If (Me.edicionEstado.SelectedValue <> "PA" And _
            Me.edicionEstado.SelectedValue <> "GP" And _
            Me.edicionEstado.SelectedValue <> "GR") Then
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")

        ElseIf Me.edAcReReasigMotivo.Text.Trim = "" Then
            Alert(Page, "Debe indicar motivo de reasignación")

        ElseIf Me.edAcReReasigMotivo.Text.Length > 50 Then
            Alert(Page, "El motivo no puede superar los 50 caracteres")

        ElseIf Me.edAcReReasigAsignado.SelectedValue = "" Then
            Alert(Page, "Debe indicar persona que desea asignar")

        Else

            utiles.Comprobar_Conexion_BD(Page, conexion)
            objTrans = conexion.BeginTransaction()
            comando.Parameters.Clear()
            Try
                comando.Transaction = objTrans

                comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0)  FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                codigoAsig = comando.ExecuteScalar.ToString


                comando.CommandText = "SELECT  UsuAsignado FROM PEINF_ASIGNACION " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig
                usuAsigAnt = comando.ExecuteScalar.ToString


                comando.CommandText = " UPDATE PEINF_ASIGNACION" & _
                                     " SET UsuAnulador = @UsuAnulador " & _
                                     " ,FechaAnulacion = getDate() " & _
                                     " ,MotivoAnuRevi = @MotivoAnuRevi " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig

                comando.Parameters.AddWithValue("UsuAnulador", Session("loginUsuario"))
                comando.Parameters.AddWithValue("MotivoAnuRevi", Me.edAcReReasigMotivo.Text.Trim & " (REASIGNACIÓN)")

                comando.ExecuteNonQuery()


                ' Obtenemos siguiente número de petición
                comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0) + 1 FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                codigoAsig = comando.ExecuteScalar.ToString

                comando.CommandText = " INSERT INTO PEINF_ASIGNACION" & _
                                     " (CodSoli" & _
                                     " ,CodAsignacion" & _
                                     " ,UsuAsignacion" & _
                                     " ,UsuAsignado" & _
                                     " ,FechaAsignacion)" & _
                                     "  VALUES " & _
                                     " (@CodSoli" & _
                                     " ,@CodAsignacion" & _
                                     " ,@UsuAsignacion" & _
                                     " ,@UsuAsignado" & _
                                     " ,getDate())"

                comando.Parameters.AddWithValue("CodSoli", Me.edicionNumsoli.Text)
                comando.Parameters.AddWithValue("CodAsignacion", codigoAsig)
                comando.Parameters.AddWithValue("UsuAsignacion", Session("loginUsuario"))
                comando.Parameters.AddWithValue("UsuAsignado", Me.edAcReReasigAsignado.SelectedValue)

                comando.ExecuteNonQuery()

                comando.CommandText = "UPDATE PEINF_SOLICITUD SET CodEstado = 'PA' WHERE CodSoli = " & Me.edicionNumsoli.Text
                comando.ExecuteNonQuery()

                objTrans.Commit()

                usuAsigNue = Me.edAcReReasigAsignado.SelectedValue

                RefrescarEstado()

                enviarCorreo(Me.edicionEmail.Text, "Notificación Petición Informes", "Petición reasignada por parte de responsable. Número: " + Me.edicionNumsoli.Text)
                enviarCorreo(correoAsignado(usuAsigAnt), "Notificación Petición Informes", "Petición desasignada por parte de responsable. Número: " + Me.edicionNumsoli.Text)
                enviarCorreo(correoAsignado(usuAsigNue), "Notificación Petición Informes", "Petición reasignada por parte de responsable. Número: " + Me.edicionNumsoli.Text)



            Catch Exc As System.Data.SqlClient.SqlException
                Select Case Exc.Number
                    Case 2627
                        Alert(Page, "La petición ya existe ")
                    Case Else
                        Alert(Page, Exc.Message)
                End Select
                objTrans.Rollback()
            End Try


        End If



    End Sub


    Protected Sub botonAcSoRepetir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles botonAcSoRepetir.Click

        Dim objTrans As SqlTransaction
        Dim codigoAsig As String

        If (Me.edicionEstado.SelectedValue <> "GV") Then
            Alert(Page, "No es posible realizar esta acción debido al estado de la petición")

        ElseIf Me.edAcSoMotivoRepe.Text.Trim = "" Then
            Alert(Page, "Debe indicar motivo de repetición")

        ElseIf Me.edAcSoMotivoRepe.Text.Length > 50 Then
            Alert(Page, "El motivo no puede superar los 50 caracteres")

        Else

            utiles.Comprobar_Conexion_BD(Page, conexion)
            objTrans = conexion.BeginTransaction()
            comando.Parameters.Clear()
            Try
                comando.Transaction = objTrans

                comando.CommandText = "SELECT ISNULL(MAX(CodAsignacion),0)  FROM PEINF_ASIGNACION WHERE CodSoli = " & Me.edicionNumsoli.Text
                codigoAsig = comando.ExecuteScalar.ToString

                comando.CommandText = " UPDATE PEINF_ASIGNACION" & _
                                     " SET UsuAnulador = @UsuAnulador " & _
                                     " ,FechaAnulacion = getDate() " & _
                                     " ,MotivoAnuRevi = @MotivoAnuRevi " & _
                                     " WHERE CodSoli = " & Me.edicionNumsoli.Text & _
                                     "  AND CodAsignacion = " & codigoAsig

                comando.Parameters.AddWithValue("UsuAnulador", Session("loginUsuario"))
                comando.Parameters.AddWithValue("MotivoAnuRevi", Me.edAcReMotivoAnu.Text.Trim & " (REPETICIÓN)")

                comando.ExecuteNonQuery()


                comando.CommandText = "UPDATE PEINF_SOLICITUD SET CodEstado = 'PD' WHERE CodSoli = " & Me.edicionNumsoli.Text
                comando.ExecuteNonQuery()

                objTrans.Commit()

                RefrescarEstado()

                enviarCorreo(Me.edicionEmail.Text, "Notificación Petición Informes", "Petición EN DESACUERDO (volver a repetír). Número: " + Me.edicionNumsoli.Text)
                enviarCorreo(correosResponsables(), "Notificación Petición Informes", "Petición EN DESACUERDO (volver a repetír). Número: " + Me.edicionNumsoli.Text)




            Catch Exc As System.Data.SqlClient.SqlException
                Select Case Exc.Number
                    Case 2627
                        Alert(Page, "La petición ya existe ")
                    Case Else
                        Alert(Page, Exc.Message)
                End Select
                objTrans.Rollback()
            End Try


        End If

    End Sub

    Protected Sub edicionClase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        If (edicionClase.SelectedItem.Text <> NO_VALOR) Then
            daPeticiones.SelectCommand.CommandText = " SELECT CodTipo as value, DesTipo as text  FROM PEINF_TIPO WHERE CodClase = '" & edicionClase.SelectedItem.Value & "' ORDER BY DesTipo "
            daPeticiones.Fill(dstPeticiones, "TablaTipo")

            edicionTipo.Items.Clear()
            edicionTipo.DataSource = dstPeticiones.Tables("TablaTipo")
            edicionTipo.DataValueField = "value"
            edicionTipo.DataTextField = "text"
            edicionTipo.DataBind()
            edicionTipo.Items.Insert(0, New ListItem(NO_VALOR, ""))
            edicionTipo.Enabled = True

        Else
            edicionTipo.Enabled = False
            edicionTipo.Items.Clear()
            edicionTipo.Items.Insert(0, New ListItem(NO_VALOR, ""))

        End If

        edicionTipo.SelectedIndex = 0
    End Sub

    Private Function ExistePeticion(ByVal strCodigo As String) As Boolean
        Dim strSQL = "SELECT * FROM PEINF_SOLICITUD WHERE CodSoli=" & strCodigo
        daPeticiones.SelectCommand.CommandText = strSQL
        daPeticiones.Fill(dstPeticiones, "TablaSoliAux")
        If dstPeticiones.Tables("TablaSoliAux").Rows.Count = 0 Then
            ExistePeticion = False
        Else
            ExistePeticion = True
        End If
    End Function





    Private Sub RefrescarEstado()

        Dim objTrans As SqlTransaction

        If (Me.edicionNumsoli.Text.Length > 0) Then
            ' En este caso existen datos asociados y puedo buscar información sobre
            ' el estado, fecha del estado y motivos


            utiles.Comprobar_Conexion_BD(Page, conexion)
            objTrans = conexion.BeginTransaction()
            comando.Parameters.Clear()
            Try
                comando.Transaction = objTrans

                comando.CommandText = "SELECT CodEstado FROM PEINF_SOLICITUD WHERE CodSoli = " + Me.edicionNumsoli.Text
                Me.edicionEstado.SelectedValue = comando.ExecuteScalar().ToString
                Me.edicionMotivoEstado.Text = ""

                comando.CommandText = "SELECT CONVERT(VARCHAR, FechaSoli, 103) FROM PEINF_SOLICITUD WHERE CodSoli = " + Me.edicionNumsoli.Text
                Me.edicionFecha.Text = comando.ExecuteScalar().ToString

                If (Me.edicionEstado.SelectedValue = "PN") Then ' Nueva
                    Me.edicionFechaEstado.Text = Me.edicionFecha.Text
                ElseIf (Me.edicionEstado.SelectedValue = "PA") Then ' Asignada
                    comando.CommandText = "SELECT TOP 1 CONVERT(VARCHAR, FechaAsignacion, 103) FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionFechaEstado.Text = comando.ExecuteScalar().ToString

                    comando.CommandText = "SELECT TOP 1 UsuAsignado FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionAsignado.SelectedValue = comando.ExecuteScalar().ToString

                ElseIf (Me.edicionEstado.SelectedValue = "GP") Then ' Generada pendiente de revisión
                    comando.CommandText = "SELECT TOP 1 CONVERT(VARCHAR, FechaGeneracion, 103) FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionFechaEstado.Text = comando.ExecuteScalar().ToString

                    comando.CommandText = "SELECT TOP 1 UsuAsignado FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionAsignado.SelectedValue = comando.ExecuteScalar().ToString

                ElseIf (Me.edicionEstado.SelectedValue = "GR") Then ' Petición de revision por parte del responsable
                    comando.CommandText = "SELECT TOP 1 CONVERT(VARCHAR, FechaPetiRevision, 103) FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionFechaEstado.Text = comando.ExecuteScalar().ToString
                    comando.CommandText = "SELECT TOP 1 MotivoAnuRevi FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionMotivoEstado.Text = comando.ExecuteScalar().ToString
                    comando.CommandText = "SELECT TOP 1 UsuAsignado FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionAsignado.SelectedValue = comando.ExecuteScalar().ToString

                ElseIf (Me.edicionEstado.SelectedValue = "GV") Then ' Revisado y validado
                    comando.CommandText = "SELECT TOP 1 CONVERT(VARCHAR, FechaValidacion, 103) FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionFechaEstado.Text = comando.ExecuteScalar().ToString

                    comando.CommandText = "SELECT TOP 1 UsuAsignado FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionAsignado.SelectedValue = comando.ExecuteScalar().ToString

                ElseIf (Me.edicionEstado.SelectedValue = "AS") Then ' Petición anulada por solicitante
                    comando.CommandText = "SELECT TOP 1 CONVERT(VARCHAR, FechaAnulacion, 103) FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionFechaEstado.Text = comando.ExecuteScalar().ToString
                    comando.CommandText = "SELECT TOP 1 MotivoAnuRevi FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionMotivoEstado.Text = comando.ExecuteScalar().ToString

                ElseIf (Me.edicionEstado.SelectedValue = "AR") Then ' Petición anulada por responsable
                    comando.CommandText = "SELECT TOP 1 CONVERT(VARCHAR, FechaAnulacion, 103) FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionFechaEstado.Text = comando.ExecuteScalar().ToString
                    comando.CommandText = "SELECT TOP 1 MotivoAnuRevi FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionMotivoEstado.Text = comando.ExecuteScalar().ToString

                ElseIf (Me.edicionEstado.SelectedValue = "PD") Then ' Petición debe repetirse por desacuerdo solicitante
                    comando.CommandText = "SELECT TOP 1 CONVERT(VARCHAR, FechaAnulacion, 103) FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionFechaEstado.Text = comando.ExecuteScalar().ToString
                    comando.CommandText = "SELECT TOP 1 MotivoAnuRevi FROM PEINF_ASIGNACION WHERE CodSoli = " + Me.edicionNumsoli.Text + " order by CodAsignacion desc"
                    Me.edicionMotivoEstado.Text = comando.ExecuteScalar().ToString

                End If


            Catch Exc As System.Data.SqlClient.SqlException
                Alert(Page, Exc.Message)
            End Try

            objTrans.Commit()
        End If
    End Sub

    Protected Function esAsignado() As Boolean
        Dim objTrans As SqlTransaction

        esAsignado = False
        utiles.Comprobar_Conexion_BD(Page, conexion)
        objTrans = conexion.BeginTransaction()
        comando.Parameters.Clear()


        Try
            comando.Transaction = objTrans

            comando.CommandText = "SELECT count(*) FROM PEINF_ASIGNADO WHERE CodUsuario = '" + Session("loginUsuario") + "'"
            If comando.ExecuteScalar().ToString <> "0" Then
                esAsignado = True
            End If


        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message)
        End Try

        objTrans.Commit()

    End Function

    Protected Function correosResponsables() As String
        Dim objTrans As SqlTransaction
        Dim reader As SqlDataReader

        correosResponsables = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        objTrans = conexion.BeginTransaction()
        comando.Parameters.Clear()


        Try
            comando.Transaction = objTrans

            comando.CommandText = "SELECT EMail FROM PEINF_ASIGNADO WHERE EsResponsable = 1"

            reader = comando.ExecuteReader


            While reader.Read()
                correosResponsables = correosResponsables + ";" + reader(0)
            End While

            reader.Close()

            correosResponsables = correosResponsables.Substring(1)

        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message)
        End Try

        objTrans.Commit()

    End Function


    Protected Function esAsignadoResponsable() As Boolean
        Dim objTrans As SqlTransaction

        esAsignadoResponsable = False
        objTrans = conexion.BeginTransaction()
        comando.Parameters.Clear()


        Try
            comando.Transaction = objTrans

            comando.CommandText = "SELECT count(*) FROM PEINF_ASIGNADO WHERE EsResponsable = 1 and CodUsuario = '" + Session("loginUsuario") + "'"
            If comando.ExecuteScalar().ToString <> "0" Then
                esAsignadoResponsable = True
            End If


        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message)
        End Try

        objTrans.Commit()

    End Function

    Protected Function correoAsignado(ByVal usuAsig As String) As String
        Dim objTrans As SqlTransaction

        correoAsignado = ""
        objTrans = conexion.BeginTransaction()
        comando.Parameters.Clear()


        Try
            comando.Transaction = objTrans

            comando.CommandText = "SELECT EMail FROM PEINF_ASIGNADO WHERE CodUsuario = '" + usuAsig + "'"
            correoAsignado = comando.ExecuteScalar().ToString


        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message)
        End Try

        objTrans.Commit()

    End Function


    Protected Sub enviarCorreo(ByVal usuariosDestino As String, ByVal asunto As String, ByVal cuerpo As String)
        Dim correo As MailMessage = New MailMessage()
        Dim htmlView As System.Net.Mail.AlternateView
        Dim clienteCorreo As SmtpClient
        Dim userInfo As System.Net.NetworkCredential
        Dim usuDestino As String()
        Dim cuerpoHTML As String

        Try
            correo.From = New MailAddress(ConfigurationSettings.AppSettings("PEINF_emailFrom"))

            usuDestino = usuariosDestino.Split(";")

            For Each usud As String In usuDestino
                correo.Bcc.Add(New MailAddress(usud))
            Next

            correo.Subject = asunto
            correo.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8")

            If ConfigurationSettings.AppSettings("PEINF_DispositionNotificationTo").Trim.Length > 0 Then
                correo.Headers.Add("Disposition-Notification-To", ConfigurationSettings.AppSettings("PEINF_DispositionNotificationTo"))
            End If

            cuerpoHTML = "<html><head><title>Notificación sistema de Petición de Informes</title></head>" & _
                         "<body><h1>Notificación sistema de Petición de Informes</h1><p>" & _
                         cuerpo & "</p></body></html>"

            cuerpoHTML = cuerpoHTML.Replace("á", "&aacute;")
            cuerpoHTML = cuerpoHTML.Replace("é", "&eacute;")
            cuerpoHTML = cuerpoHTML.Replace("í", "&iacute;")
            cuerpoHTML = cuerpoHTML.Replace("ó", "&oacute;")
            cuerpoHTML = cuerpoHTML.Replace("ú", "&uacute;")
            cuerpoHTML = cuerpoHTML.Replace("ñ", "&ntilde;")

            htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(cuerpoHTML, correo.BodyEncoding, "text/html")

            correo.AlternateViews.Add(htmlView)

            clienteCorreo = New SmtpClient()
            clienteCorreo.Host = ConfigurationSettings.AppSettings("PEINF_Host")

            If ConfigurationSettings.AppSettings("PEINF_UseDefaultCredentials").ToLower = "true" Then
                clienteCorreo.UseDefaultCredentials = True
            Else
                clienteCorreo.UseDefaultCredentials = False
                userInfo = New System.Net.NetworkCredential(ConfigurationSettings.AppSettings("PEINF_NetworkCredentialUser"), ConfigurationSettings.AppSettings("PEINF_NetworkCredentialPass"))
                clienteCorreo.Credentials = userInfo
            End If

            clienteCorreo.Send(correo)
        Catch Exc As System.Exception
            Alert(Page, "Error al enviar correo. Avise informática. Detalle: " & Exc.Message)
        End Try
    End Sub
End Class
