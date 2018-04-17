Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Imports NineRays.Reporting.Export.PDF
Imports NineRays.Reporting.Export.RTF
Imports NineRays.Reporting.Export.Html
Imports NineRays.Reporting.Export.Excel
Imports NineRays.Reporting.Export.CSV

Partial Class Listados_InformePuntos
    Inherits System.Web.UI.Page

    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstPuntos As DataSet = New System.Data.DataSet()

    Dim sentenciaSel As String

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container

        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Listados_InformePuntos))
        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)

        crearDataSets()

        CType(dstPuntos, System.ComponentModel.ISupportInitialize).BeginInit()

        Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSet"}, New Object() {Me.dstPuntos})

        CType(dstPuntos, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.  
        If Request.QueryString("format") Is Nothing Then
            Session("informe") = Request.QueryString("informe")
            'Session("codigoPVYCR") = Request.QueryString("codigoPVYCR")
            Session("ClaveNodo") = Split(Request.QueryString("nodoClave"), ";")(0)
        End If
        InitializeComponent()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            BuildListBox()
        End If

        If Session("informe") = "" Then
            If Request.QueryString("format") Is Nothing Then
                Response.Redirect(SharpShooterWebViewer1.PdfLink)
            End If
        Else
            SharpShooterWebViewer1.Style.Value = "Z-INDEX: 102; LEFT: 8px; POSITION: absolute;"
        End If
    End Sub
    Private Sub BuildListBox()
        LoadReportTemplate()
    End Sub

    Private Sub LoadReportTemplate()
        reportGenerator1.LoadTemplate(Server.MapPath("InformePuntos.rst"))

        Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
        SharpShooterWebViewer1.Source = reportGenerator1

        Dim doc As Document = reportGenerator1.RenderDocument()
        Dim m As Stream = Response.OutputStream

        Select Case Session("informe")
            Case "xls"
                Dim xls As ExcelExportFilter = New ExcelExportFilter
                xls.Export(doc, m)
                Page.Response.AddHeader("Content-Disposition", "attachment; filename=ReportPage.xls")
                Page.Response.ContentType = "application/excel"
            Case "pdf"

            Case "html"
                Page.Response.ContentEncoding = System.Text.Encoding.UTF8
                Dim htm As HtmlExportFilter = New HtmlExportFilter
                htm.Export(doc, m)
            Case "rtf"
                Dim rtf As RTFExportFilter = New RTFExportFilter
                rtf.Export(doc, m)
                Page.Response.ContentEncoding = System.Text.Encoding.UTF8
                Page.Response.ContentType = "application/rtf"
                Page.Response.AddHeader("Content-Disposition", "attachment; filename=ReportPage.rtf")
            Case "text"
                Dim csv As CSVExportFilter = New CSVExportFilter
                csv.Separator = " "
                csv.Export(doc, m)
                Page.Response.ContentEncoding = System.Text.Encoding.UTF8
        End Select
    End Sub

    Private Sub crearDataSets()
        Dim sFiltro As String = ""
        sentenciaSel = " SELECT     CodigoCauce,CodigoPVYCR, DenominacionPunto, TipoSensor, Acceso, X_UTM, Y_UTM, PorcentajeRegable, Diametro_mm, " & _
                       " FactorFlotador, ISNULL(TipoPunto,'P'), CodigoDataLogger, UsadoEnParteOficial, PKS, PKA, RIO, Longitud, LongitudFlotador, A1_M, A2_M, A3_M,B1_M, B2_M, B3_M,  " & _
                       " B4_M, H12_M, H23_M, H34_M, Offset_M, Antiguo, Observaciones, " & _
                       " case UsadoEnParteOficial " & _
                       " when '1' then 'X' " & _
                       " else '' " & _
                       " end as chkUsadoEnParteOficial " & _
                       " FROM PVYCR_Puntos where CodigoPVYCR='" & Session("ClaveNodo") & "'"
        daPuntos.SelectCommand.CommandText = sentenciaSel
        daPuntos.Fill(dstPuntos, "TablaPuntos")
    End Sub

End Class
