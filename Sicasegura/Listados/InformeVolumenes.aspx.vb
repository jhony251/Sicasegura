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

Imports GuarderiaFluvial.SICA_GestionArboles

Partial Class SICAH_InformeVolumenes
    Inherits System.Web.UI.Page

    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim da As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New System.Data.DataSet()
    Dim sentenciaSel, sentenciaOrder As String
    Dim dt As New System.Data.DataTable()
    Dim dtLectSel As New System.Data.DataTable()
    Dim conexion_insert As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion_insert)

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        'Dim xls As ExcelExportFilter = New NineRays.Reporting.Export.Excel.ExcelExportFilter()
        'Dim htmlFilt As HtmlExportFilter = New NineRays.Reporting.Export.Html.HtmlExportFilter()

        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(SICAH_InformeVolumenes))
        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)

        Try
            AdaptaDstListado()

            CType(Me.dst, System.ComponentModel.ISupportInitialize).BeginInit()

            Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSet"}, New Object() {dst})
            CType(Me.dst, System.ComponentModel.ISupportInitialize).EndInit()

        Catch ex As Exception
            JavaScript.Alert(Page, "Por favor, vuelve a cargar los datos de las lecturas que ya no están en memoria")
            Response.End()
        End Try
    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.          
        InitializeComponent()
        If Request.QueryString("format") Is Nothing Then
            Session("ClaveNodoTipo") = Request.QueryString("nodoclave")
            Session("claveTipo") = Session("ClaveNodoTipo").Substring(Session("ClaveNodoTipo").IndexOf(";") + 1, 1)
            Session("ClaveNodo") = Session("ClaveNodoTipo").Substring(0, Session("ClaveNodoTipo").IndexOf(";"))
            Session("nodoTexto") = Request.QueryString("nodotexto")

            'Dim NombreElemento As String
            'Dim separadores As Integer = 0
            'Dim guiones As Integer = 0
            'Dim V() As String
            'V = Split(Session("nodotexto"), "-")
            'separadores = numeroSeparadores(Session("nodoclave"))
            'guiones = numeroGuiones(Session("nodotexto"))
            'NombreElemento = Replace(Session("nodotexto"), "<img src='images/distancias.gif' border=0>&nbsp;&nbsp;<font color=#008500>" & Session("nodoclave").Substring(0, Session("nodoclave").Length - 4) & "-", "")

            'Session("IdElementoMedida") = NombreElemento.Substring(0, NombreElemento.ToString.IndexOf("-") - 1)
            'Session("IdElementoMedida") = V(guiones - 1).ToString

        End If
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
        Select Case Left(Session("IdElementoMedida"), 1)
            Case "Q"
                reportGenerator1.LoadTemplate(Server.MapPath("informeVolumenesCaudales.rst"))
            Case "E"
                reportGenerator1.LoadTemplate(Server.MapPath("informeVolumenesEnergia.rst"))
            Case "V"
                reportGenerator1.LoadTemplate(Server.MapPath("informeVolumenesMotores.rst"))
            Case "D"
                reportGenerator1.LoadTemplate(Server.MapPath("informeVolumenesDiferenciales.rst"))
        End Select

        SharpShooterWebViewer1.Source = reportGenerator1
        Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
        Dim doc As Document = reportGenerator1.RenderDocument()
        Dim m As Stream = Response.OutputStream

        Select Case Request.QueryString("informe")
            Case "xls"
                Dim xls As ExcelExportFilter = New ExcelExportFilter
                xls.Export(doc, m)
                Page.Response.AddHeader("Content-Disposition", "attachment; filename=ReportPage.xls")
                Page.Response.ContentType = "application/excel"
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

    Private Sub AdaptaDstListado()
        Dim fila As DataRow
        Dim dr() As System.Data.DataRow
        dt = Cache("dst")
        dr = dt.DefaultView.Table.Select
        If Session("LecturasSeleccionadas") <> "" Then
            dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")
            dr = dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")
        End If

        dtLectSel = dt.Clone()
        For Each fila In dr
            dtLectSel.Rows.Add(fila.ItemArray)
        Next
        dtLectSel.Columns.Add(New DataColumn("NodoTexto", Type.GetType("System.String")))

        Dim descripcionP, descripcionC, rama As String
        If conexion_insert.State = ConnectionState.Closed Then conexion_insert.Open()
        comando.CommandText = "Select descripcion from PVYCR_Arbol where tipo='P' and codigoPVYCR='" & Session("ClaveNodo") & "'"
        descripcionP = Replace(comando.ExecuteScalar, Session("ClaveNodo") & " ", "")

        comando.CommandText = "Select descripcion from PVYCR_Arbol where tipo='C' and codigoCauce='" & Left(Session("ClaveNodo"), InStrRev(Session("ClaveNodo"), "P") - 1) & "'"
        descripcionC = Replace(comando.ExecuteScalar, Left(Session("ClaveNodo"), InStrRev(Session("ClaveNodo"), "P") - 1) & " ", "")

        rama = vbCrLf & Session("ClaveNodo") & "-" & Session("IdElementoMedida") & " (" & descripcionC & " - " & descripcionP & ")" & vbCrLf
        conexion_insert.Close()

        dtLectSel.Rows(0).Item("NodoTexto") = rama
        dst.Tables.Add(dtLectSel)
    End Sub

End Class
