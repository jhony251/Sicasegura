Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial
Imports System.IO
Imports System.Runtime.InteropServices
Imports NineRays.Reporting.DOM
Imports NineRays.Reporting.Export.PDF
Imports NineRays.Reporting.Export.RTF
Imports NineRays.Reporting.Export.Html
Imports NineRays.Reporting.Export.Excel
Imports NineRays.Reporting.Export.CSV

Partial Class Listados_ListadoSeguimientoAdmin
    Inherits System.Web.UI.Page

    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim da As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New System.Data.DataSet()


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container

        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Listados_ListadoSeguimientoAdmin))
        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)

        RellenaDataSets()

        CType(Me.dst, System.ComponentModel.ISupportInitialize).BeginInit()

        Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSet"}, New Object() {Me.dst})
        CType(Me.dst, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.  
        'If Request.QueryString("format") Is Nothing Then
        '    Session("tipoelem") = Request.QueryString("tipoelem")
        '    Session("codigoPVYCR") = Request.QueryString("codigoPVYCR")
        'End If
        InitializeComponent()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildListBox()
        End If
    End Sub

    Private Sub BuildListBox()
        LoadReportTemplate()
    End Sub

    Private Sub LoadReportTemplate()
        reportGenerator1.LoadTemplate(Server.MapPath("InformeSeguimientos.rst"))
        'SharpShooterWebViewer1.Source = reportGenerator1
        Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
    End Sub


    Private Sub RellenaDataSets()
        Try
            dst = Cache("dstSeguimientos")
            Try
                dst.Tables("TablaSeguimientosAdministrativos").Columns.Add(New DataColumn("FechaPeque", Type.GetType("System.String")))
            Catch ex As Exception

            End Try

            For i As Integer = 0 To dst.Tables("TablaSeguimientosAdministrativos").Rows.Count - 1
                dst.Tables("TablaSeguimientosAdministrativos").Rows(i).Item("FechaPeque") = Left(dst.Tables("TablaSeguimientosAdministrativos").Rows(i).Item("Fecha"), 10)
            Next
        Catch ex As Exception
            JavaScript.Alert(Page, "Por favor, vuelve a cargar los datos de las lecturas que ya no están en memoria")
            Response.End()
        End Try

    End Sub
End Class
