Imports System.Diagnostics
Partial Class Listados_BorrarProcesos
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idproc As Integer = GetIDProcces("EXCEL")

        If idproc <> -1 Then
            Process.GetProcessById(idproc).Kill()
        End If
        
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
