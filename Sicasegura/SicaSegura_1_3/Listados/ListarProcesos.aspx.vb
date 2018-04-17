Imports System.Diagnostics

Partial Class Listados_ListarProcesos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim asProccess() As Process = Process.GetProcesses()

        For Each pProccess As Process In asProccess
            If pProccess.MainWindowTitle = "" Then
                lblListado.Text = lblListado.Text & pProccess.ProcessName & "<br>"
            End If
        Next
    End Sub
End Class
