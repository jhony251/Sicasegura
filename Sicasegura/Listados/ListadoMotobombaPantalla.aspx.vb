Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports System.Data.SqlClient
Imports System
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Partial Class Listados_ListadoMotobombaPantalla
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
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
                Session("codigoMotobomba") = Request.QueryString("codigoMotobomba")
                Session("fechaRevision") = Request.QueryString("fechaRevision")
                Response.Redirect(SharpShooterWebViewer1.PdfLink)
            End If

            'If conexion.State = ConnectionState.Closed Then conexion.Open()
            utiles.Comprobar_Conexion_BD(Page, conexion)
            sentenciaSel = "SELECT CodigoMotoBomba, convert(nvarchar(15),FechaRevision,103) FechaRevision, codigoPVYCR, FechaInicial, Descripcion, Grupos_Motobomba, MarcaBomba, TipoBomba, NumSerieBomba, " & _
                  "ModeloBomba, Qn_Bomba, MarcaMotor, ModeloMotor, TipoMotor_H_V, NumSerieMotor, RPM, Potencia_CV, Potencia_KW, Tipo_Pot, " & _
                  "PotenciaTeorica_KW, DDP_Motor_V, Intensidad_Motor_A, Cos_Fi, Caudal_LSeg, Tipo_Caudal, Altura_Geometrica_Impulsion, " & _
                  "Altura_Manometrica_Teorica, Tipo_Alt, Profundidad_Nivel_Estatico, Profundidad_Nivel_Dinamico, Longitud_Aspiracion, Dn_Aspiracion, " & _
                  "Material_Tub_Aspiracion, Longitud_Impulsion, Dn_Impulsion, Material_Tub_Impulsion, Voltimetro, Amperimetro, Manometro_Kgcm2, Rev10min, " & _
                  "BombaEnFuncionamiento, Cota_Toma, Destino1, Destino2, Destino3, Cota_Destino1, Cota_Destino2, Cota_Destino3, UTM_Destino1, UTM_Destino2,  " & _
                  "UTM_Destino3, FechaFin, Observaciones FROM dbo.PVYCR_MotoBombas "

            If Session("codigoMotobomba") <> "[Código Motobomba]" And Session("codigoMotobomba") <> "" Then
                sfiltro = " where CodigoMotoBomba like '" & Session("codigoMotobomba") & "' "
            Else
                sFiltro = ""
            End If
            sentenciaOrder = " order by CodigoMotoBomba "

            If sFiltro <> "" Then
                sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
            Else
                sentenciaSel = sentenciaSel & sentenciaOrder
            End If

            daElementos.SelectCommand.CommandText = sentenciaSel
            'datos acequias listados
            daElementos.Fill(dstListados, "TablaContador")
            CType(Me.dstListados, System.ComponentModel.ISupportInitialize).BeginInit()
            SharpShooterWebViewer1.PdfText = "Mostrar en PDF"
            Me.reportgenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"Contadores"}, New Object() {Me.dstListados})

            CType(Me.dstListados, System.ComponentModel.ISupportInitialize).EndInit()

            reportgenerator1.LoadTemplate(Server.MapPath("ListadoMotobombaPantalla.rst"))
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
