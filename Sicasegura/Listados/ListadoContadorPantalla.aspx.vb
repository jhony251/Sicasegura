Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports System.Data.SqlClient
Imports System
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Partial Class Listados_ListadoContadorPantalla
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daListados As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstListados As DataSet = New System.Data.DataSet()
    Dim sentenciaSel, sentenciaOrder As String
    Protected reportgenerator1 As NineRays.Reporting.Components.ReportGenerator = New NineRays.Reporting.Components.ReportGenerator()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sfiltro As String
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            If Request.QueryString("format") Is Nothing Then
                Session("CodigoPVYCR") = Request.QueryString("codigoPVYCR")
            End If

            'If conexion.State = ConnectionState.Closed Then conexion.Open()
            utiles.Comprobar_Conexion_BD(Page, conexion)
            sentenciaSel = "SELECT [idContador],convert(nvarchar(15),[FechaRevision],103) fecharevision,[CodigoPVYCR],convert(nvarchar(15),[FechaInicial],103) FechaInicial, " & _
            "convert(nvarchar(15),[FechaFin],103) FechaFin,[TipoContador],[AccesoAContador] " & _
                 ",[Aforable],[DistanciaPtoAforo_Km],[V_NumeroSerie],[V_Marca],[V_FFContVolum],[E_Ref_contrato],[E_Pot_contratada] " & _
                 ",[E_Tarifa_contratada],[E_TipoDiscriminacionHoraria],[E_Compl_reactiva],[E_CT_KVA],[E_TipoContActiva],[E_NumSerieContActiva] " & _
                 ",[E_NumSerieContReactiva],[E_IntensidadContActiva],[E_TensionContActiva],[E_ConstContActiva],[E_FFContActiva],[E_FCorrectorContActiva] " & _
                 ",[E_RelacionM3_KWH],[E_TipoRelacionM3_KWH],[H_NumerioSerie],[H_Marca],[H_Modelo],[H_FFConthora],[Q_NumeroSerie],[Q_Marca] " & _
                 ",[Q_Modelo],[Q_Unidades],[Q_FFContCaudal] ,[CodigoCaracterizacion] FROM PVYCR_Contadores "

            If Session("CodigoPVYCR") <> "" And Session("CodigoPVYCR") <> "[Código PVYCR]" Then
                sfiltro = " where CodigoPVYCR like '" & Session("CodigoPVYCR") & "' "
            Else
                sfiltro = ""
            End If
            sentenciaOrder = " order by idContador "

            If sFiltro <> "" Then
                sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
            Else
                sentenciaSel = sentenciaSel & sentenciaOrder
            End If

            daListados.SelectCommand.CommandText = sentenciaSel
            daListados.Fill(dstListados, "TablaContador")
            CType(dstListados, System.ComponentModel.ISupportInitialize).BeginInit()
            SharpShooterWebViewer1.PdfText = "Mostrar en PDF"

            Me.reportgenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"Contadores"}, New Object() {dstListados})

            CType(dstListados, System.ComponentModel.ISupportInitialize).EndInit()

            reportgenerator1.LoadTemplate(Server.MapPath("ListadoContadorPantalla.rst"))
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
