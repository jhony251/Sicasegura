Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports System.Data.SqlClient
Imports System
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Partial Class Listados_ListadoMotobomba
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstListados As DataSet = New System.Data.DataSet()
    Dim sentenciaSel, sentenciaOrder As String
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Protected reportgenerator1 As NineRays.Reporting.Components.ReportGenerator = New NineRays.Reporting.Components.ReportGenerator()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            If Request.QueryString("format") Is Nothing Then
                Session("codigoMotobomba") = Request.QueryString("codigoMotobomba")
                Session("fechaRevision") = Request.QueryString("fechaRevision")
                Response.Redirect(SharpShooterWebViewer1.PdfLink)
            End If

            'If conexion.State = ConnectionState.Closed Then conexion.Open()
            utiles.Comprobar_Conexion_BD(Page, conexion)
            sentenciaSel = "SELECT [CodigoMotoBomba],[FechaRevision],M.[codigoPVYCR],[FechaInicial],[FechaFin],[Descripcion],[Grupos_Motobomba] " & _
                ",[MarcaBomba],[TipoBomba],[NumSerieBomba],[ModeloBomba],[Qn_Bomba],[MarcaMotor],[ModeloMotor],[TipoMotor_H_V],[NumSerieMotor] " & _
                ",[RPM],[Potencia_CV],[Potencia_KW],[Tipo_Pot],[PotenciaTeorica_KW],[DDP_Motor_V],[Intensidad_Motor_A],[Cos_Fi],[Caudal_LSeg],[Tipo_Caudal] " & _
                ",[Altura_Geometrica_Impulsion],[Altura_Manometrica_Teorica],[Tipo_Alt],[Profundidad_Nivel_Estatico],[Profundidad_Nivel_Dinamico] " & _
                ",[Longitud_Aspiracion],[Dn_Aspiracion],[Material_Tub_Aspiracion],[Longitud_Impulsion],[Dn_Impulsion],[Material_Tub_Impulsion] " & _
                ",[Voltimetro],[Amperimetro],[Manometro_Kgcm2],[Rev10min],[BombaEnFuncionamiento],[Cota_Toma],[Destino1],[Destino2],[Destino3] " & _
                ",[Cota_Destino1],[Cota_Destino2],[Cota_Destino3],[UTM_Destino1],[UTM_Destino2],[UTM_Destino3],M.[Observaciones],P.DENOMINACIONPUNTO " & _
                "FROM PVYCR_Motobombas M, PVYCR_puntos P " & _
                "where M.codigoPVYCR=P.codigoPVYCR and codigoMotobomba = '" & Session("codigoMotobomba") & "' and fechaRevision = '" & _
                Session("fechaRevision") & "' "


            daElementos.SelectCommand.CommandText = sentenciaSel
            'datos acequias listados
            daElementos.Fill(dstListados, "TablaContador")
            CType(Me.dstListados, System.ComponentModel.ISupportInitialize).BeginInit()

            Me.reportgenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"Contadores"}, New Object() {Me.dstListados})

            CType(Me.dstListados, System.ComponentModel.ISupportInitialize).EndInit()

            reportgenerator1.LoadTemplate(Server.MapPath("ListadoMotobomba.rst"))
            Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
            SharpShooterWebViewer1.Source = reportgenerator1

            'SharpShooterWebViewer1.ViewMode = NineRays.Reporting.Web.ViewMode.HtmlSinglePage
            'reportgenerator1.LoadTemplate("c:\listado1.rst")
            'reportgenerator1.Prepare()
            'DG.formApli.reportGenerator1.Template.Description = "Informe de " + oc.nombre
            'reportgenerator1.DesignTemplate()
        End If

    End Sub
End Class
