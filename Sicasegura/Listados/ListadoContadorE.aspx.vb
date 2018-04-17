Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports System.Data.SqlClient
Imports System
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing

Partial Class Listados_ListadoContadorE
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim dstListados As DataSet = New System.Data.DataSet()
    Dim sentenciaSel, sentenciaOrder As String
    Protected reportgenerator1 As NineRays.Reporting.Components.ReportGenerator = New NineRays.Reporting.Components.ReportGenerator()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            If Request.QueryString("format") Is Nothing Then
                Session("idContador") = Request.QueryString("idContador")
                Session("fechaRevision") = Request.QueryString("fechaRevision")
                Response.Redirect(SharpShooterWebViewer1.PdfLink)
            End If

            'If conexion.State = ConnectionState.Closed Then conexion.Open()
            utiles.Comprobar_Conexion_BD(Page, conexion)
            sentenciaSel = "SELECT [idContador],[FechaRevision],c.[CodigoPVYCR],[FechaInicial],[FechaFin],c.[TipoContador],[AccesoAContador] " & _
            ",[Aforable],[DistanciaPtoAforo_Km],[E_Ref_contrato],[E_Pot_contratada] " & _
            ",[E_Tarifa_contratada],[E_TipoDiscriminacionHoraria],[E_Compl_reactiva],[E_CT_KVA],[E_TipoContActiva],[E_NumSerieContActiva] " & _
            ",[E_NumSerieContReactiva],[E_IntensidadContActiva],[E_TensionContActiva],[E_ConstContActiva],[E_FFContActiva],[E_FCorrectorContActiva] " & _
            ",p.denominacionPunto,c.descripcion " & _
            ",[E_RelacionM3_KWH],[E_TipoRelacionM3_KWH],[CodigoCaracterizacion] FROM PVYCR_Contadores C, PVYCR_puntos P " & _
            "where c.codigoPVYCR=P.codigoPVYCR and idContador = '" & Session("idContador") & "' and fechaRevision = '" & _
            Session("fechaRevision") & "' "


            daElementos.SelectCommand.CommandText = sentenciaSel
            'datos acequias listados
            daElementos.Fill(dstListados, "TablaContador")
            CType(Me.dstListados, System.ComponentModel.ISupportInitialize).BeginInit()

            Me.reportgenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"Contadores"}, New Object() {Me.dstListados})

            CType(Me.dstListados, System.ComponentModel.ISupportInitialize).EndInit()

            reportgenerator1.LoadTemplate(Server.MapPath("ListadoContadorE.rst"))
            Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
            SharpShooterWebViewer1.Source = reportgenerator1
            SharpShooterWebViewer1.ViewMode = NineRays.Reporting.Web.ViewMode.HtmlSinglePage
            'reportgenerator1.LoadTemplate("c:\listado1.rst")
            'reportgenerator1.Prepare()
            'DG.formApli.reportGenerator1.Template.Description = "Informe de " + oc.nombre
            'reportgenerator1.DesignTemplate()
        End If
    End Sub
End Class
