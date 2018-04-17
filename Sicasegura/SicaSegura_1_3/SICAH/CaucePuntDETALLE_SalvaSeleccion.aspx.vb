Imports System.Data
Imports GuarderiaFluvial

Partial Class SICAH_CaucePuntDETALLE_SalvaSeleccion
    Inherits System.Web.UI.Page

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim CondicionLecturas As String = " AND ( " & Request.QueryString("CondicionLecturas")

        If CondicionLecturas = " AND ( " Then
            CondicionLecturas = ""
        Else
            CondicionLecturas = CondicionLecturas.Substring(0, CondicionLecturas.Length - 2) + ")"
        End If
        Session("LecturasSeleccionadas") = CondicionLecturas


        'asignamos el dataset a una variable global
        Dim dtLectSelPantallaInformes As New System.Data.DataTable
        Dim dtPantallaInformes As DataTable = New DataTable()
        Try
            dtPantallaInformes = Session("dst")
        Catch ex As Exception
            JavaScript.Alert(Page, "Se ha perdido la sesión")
            Exit Sub
        End Try

        Dim dr() As System.Data.DataRow
        dtLectSelPantallaInformes = dtPantallaInformes.Clone()

        dr = dtPantallaInformes.DefaultView.Table.Select
        If Session("LecturasSeleccionadas") <> "" Then
            dtPantallaInformes.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")
            dr = dtPantallaInformes.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")

            Dim fila As DataRow
            For Each fila In dr
                dtLectSelPantallaInformes.Rows.Add(fila.ItemArray)
            Next
            Session("TablaCaudalesDesdeAcequias") = dtLectSelPantallaInformes
        Else
            Session("TablaCaudalesDesdeAcequias") = Session("dst")
        End If

    End Sub
End Class
