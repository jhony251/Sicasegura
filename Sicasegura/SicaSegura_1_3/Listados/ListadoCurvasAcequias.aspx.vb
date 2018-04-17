Imports System.Diagnostics
Imports vb = Microsoft.VisualBasic
Imports System.IO
Imports System.Collections
Imports GuarderiaFluvial

Partial Class SICAH_ListadoCurvasAcequias
    Inherits System.Web.UI.Page
    Public fechahoy As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CrearExcel()
        End If

    End Sub

    Private Sub CrearExcel()
        Dim archivo, plantilla As String

        Try
            archivo = Server.MapPath("./") & "CurvasAcequias" & Session.SessionID & ".xls"
            plantilla = Server.MapPath("./") & "PlantillaCurvas.xls"

            File.Copy(plantilla, archivo, True)


            Dim xlsap As Excel.Application = New Excel.Application()
            Dim docxls As Excel.Workbook

            xlsap.Application.Visible = True
            docxls = xlsap.Workbooks.Open(archivo)
            xlsap.Cells.Item(1, 1) = Request.QueryString("codigoPVYCR")
            xlsap.Application.Run("prueba")
            xlsap.Application.ActiveWorkbook.Save()
            xlsap.Application.ActiveWorkbook.Close()

            Dim elArchivo() As Byte = File.ReadAllBytes(Server.MapPath("./") & "CurvasAcequias" & Session.SessionID & ".xls")
            Response.ContentType = "application/vnd.ms-excel"
            Response.BinaryWrite(elArchivo)

            Dim idproc As Integer = GetIDProcces("EXCEL")

            If idproc <> -1 Then
                Process.GetProcessById(idproc).Kill()
            End If

            File.Delete(Server.MapPath("./") & "CurvasAcequias" & Session.SessionID & ".xls")
        Catch ex As Exception
            JavaScript.Alert(Me, ex.Message)
        End Try
    End Sub

    Private Function GetIDProcces(ByVal nameProcces As String)
        Try
            Dim asProccess() As Process = Process.GetProcessesByName(nameProcces)
            For Each pProccess As Process In asProccess
                If pProccess.MainWindowTitle = "" Then
                    Return pProccess.Id
                End If
            Next
            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function

End Class
