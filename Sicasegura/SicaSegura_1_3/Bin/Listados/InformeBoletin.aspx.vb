Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Imports NineRays.Reporting.Export.PDF
Imports NineRays.Reporting.Export.RTF
Imports NineRays.Reporting.Export.Html
Imports NineRays.Reporting.Export.Excel
Imports NineRays.Reporting.Export.CSV


Partial Class InformeBoletin

    Inherits System.Web.UI.Page

    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daBoletines As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstBoletines As DataSet = New System.Data.DataSet()

    Dim sentenciaSel As String

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container

        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(InformeBoletin))
        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)

        crearDataSets()

        'dstBoletines = Cache("dstBoletines")
      

        CType(dstBoletines, System.ComponentModel.ISupportInitialize).BeginInit()

        Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSet"}, New Object() {Me.dstBoletines})

        CType(dstBoletines, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.  
        If Request.QueryString("format") Is Nothing Then
            Session("NumRef") = Request.QueryString("NumRef")
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
        reportGenerator1.LoadTemplate(Server.MapPath("InformeBoletin.rst"))

        Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
        SharpShooterWebViewer1.Source = reportGenerator1

        Dim doc As Document = New Document()
        doc = reportGenerator1.RenderDocument()
        doc = reportGenerator1.Document
        Dim m As Stream = Response.OutputStream


        Session("informe") = "rtf"

        Try
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
        Catch mierror As Exception
            'EGB 03/02/2009 Parche Provional a falta de saber la causa en el fallo de la renderizacion a doc en 
            'algunos boletines.
            'Convertimos el report a html. 

            Session("informe") = "html"

            Page.Response.ContentEncoding = System.Text.Encoding.UTF8
            Dim htm As HtmlExportFilter = New HtmlExportFilter
            htm.Export(doc, m)
        End Try

    End Sub
    Private Sub crearDataSets()

        utiles.Comprobar_Conexion_BD(Page, conexion)
        'Carga de los datos alfanumericos del boletín
        If dstBoletines.Tables.Contains("TablaBoletin") Then
            dstBoletines.Tables("TablesBoletin").Clear()
        End If
        sentenciaSel = "SELECT *, " & _
                       "CASE b.Planos WHEN 1 THEN 'Si' ELSE 'No' END AS PlanosTXT, " & _
                       "CASE b.Fotos WHEN 1 THEN 'Si' ELSE 'No' END AS FotosTXT, " & _
                       "CASE b.TipoDenuncia WHEN 1 THEN 'Si' ELSE 'No' END AS TipoDenunciaTXT,  " & _
                       "CASE b.Croquis WHEN 1 THEN 'Si' ELSE 'No' END AS CroquisTXT, " & _
                       "CAST(datepart(dd,b.Fecha) AS VARCHAR(2))+ '/'+ CAST(datepart(mm,b.Fecha)AS VARCHAR(2))+ '/'+ CAST(datepart(yyyy,b.Fecha)AS VARCHAR(4)) as FechaCorta, " & _
                       "P.Descripcion as DescripcionPunto, " & _
                       "ISNULL(ts.denominacion,b.tipo+' (Denominación sin Descripción)') as Denominacion,  " & _
                       "NombreAutor, Hechos FROM PVYCR_BoletinGuarderia b " & _
                       "LEFT JOIN PVYCR_SeguimientoAdministrativo_TS ts ON ts.tiposuceso = b.tipo " & _
                       "LEFT join (select Descripcion, CodigoPVYCR from PVYCR_Arbol WHERE Tipo='P')P on p.CodigoPvycr=PVYCRRef " & _
                       "WHERE NumRef='" & Session("NumRef") & "'"

        daBoletines.SelectCommand.CommandText = sentenciaSel
        daBoletines.Fill(dstBoletines, "TablaBoletin")
        'Carga de los datos de imagen del boletín
        If dstBoletines.Tables.Contains("TablaBoletinImages") Then
            dstBoletines.Tables("TablaBoletinImages").Clear()
        End If

        sentenciaSel = "SELECT [NumRef]" & _
                          ",[Foto1]" & _
                          ",[Foto2]" & _
                          ",[Foto3]" & _
                          ",[Foto4]" & _
                       "FROM [SIGVECTOR].[dbo].[PVYCR_Denuncia_Fotos]" & _
                       "WHERE NumRef='" & Session("NumRef") & "'"

        daBoletines.SelectCommand.CommandText = sentenciaSel
        daBoletines.Fill(dstBoletines, "TablaBoletinImages")

        'EGB 05/02/2008 Se Actualiza el contenido de los campos que contienen Imagen vacía para
        'que la renderización no resulte fallida. 
        ActualizarImagenEnBlanco("..\SICAH\tmp\blanco.jpg")

    End Sub
    Public Sub ActualizarImagenEnBlanco(ByVal strRutaImagen As String)

        Dim local_comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
        If dstBoletines.Tables.Item("TablaBoletinImages").Rows.Count > 0 Then
            If dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto1") Is System.DBNull.Value Then
                'update del valor de la fotografia en flanco en el campo nulo...
                local_comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                             "SET Foto1 =@Foto1 " & _
                                             "WHERE NumRef=@NumRef "
                'convertir el valor de imagen en bytes

                local_comando.Parameters.AddWithValue("Foto1", ConvertImageFiletoBytes(Server.MapPath(strRutaImagen)))
                local_comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Session("NumRef")))
                local_comando.ExecuteNonQuery()
            End If
            If dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto2") Is System.DBNull.Value Then
                'update del valor de la fotografia en flanco en el campo nulo...
                local_comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                             "SET Foto2 =@Foto2 " & _
                                             "WHERE NumRef=@NumRef2 "
                'convertir el valor de imagen en bytes

                local_comando.Parameters.AddWithValue("Foto2", ConvertImageFiletoBytes(Server.MapPath(strRutaImagen)))
                local_comando.Parameters.AddWithValue("NumRef2", utiles.BlancoANull(Session("NumRef")))
                local_comando.ExecuteNonQuery()
            End If
            If dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto3") Is System.DBNull.Value Then
                'update del valor de la fotografia en flanco en el campo nulo...
                local_comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                             "SET Foto3 =@Foto3 " & _
                                             "WHERE NumRef=@NumRef3 "
                'convertir el valor de imagen en bytes

                local_comando.Parameters.AddWithValue("Foto3", ConvertImageFiletoBytes(Server.MapPath(strRutaImagen)))
                local_comando.Parameters.AddWithValue("NumRef3", utiles.BlancoANull(Session("NumRef")))
                local_comando.ExecuteNonQuery()
            End If
            If dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto4") Is System.DBNull.Value Then
                'update del valor de la fotografia en flanco en el campo nulo...
                local_comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                             "SET Foto4 =@Foto4 " & _
                                             "WHERE NumRef=@NumRef4 "
                'convertir el valor de imagen en bytes

                local_comando.Parameters.AddWithValue("Foto4", ConvertImageFiletoBytes(Server.MapPath(strRutaImagen)))
                local_comando.Parameters.AddWithValue("NumRef4", utiles.BlancoANull(Session("NumRef")))
                local_comando.ExecuteNonQuery()
            End If

            'Refresca los datos de imagen del boletín
            If dstBoletines.Tables.Contains("TablaBoletinImages") Then
                dstBoletines.Tables("TablaBoletinImages").Clear()
            End If

            sentenciaSel = "SELECT [NumRef]" & _
                              ",[Foto1]" & _
                              ",[Foto2]" & _
                              ",[Foto3]" & _
                              ",[Foto4]" & _
                           "FROM [SIGVECTOR].[dbo].[PVYCR_Denuncia_Fotos]" & _
                           "WHERE NumRef='" & Session("NumRef") & "'"

            daBoletines.SelectCommand.CommandText = sentenciaSel
            daBoletines.Fill(dstBoletines, "TablaBoletinImages")
        End If

    End Sub

   
    Public Function ConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If String.IsNullOrEmpty(ImageFilePath) = True Then
            Throw New ArgumentNullException("No es posible cargar la imagen blanco.jpg", "ImageFilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(ImageFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
