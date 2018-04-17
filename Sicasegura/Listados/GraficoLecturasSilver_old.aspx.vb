Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.JavaScript

Partial Class Listados_GraficoLecturasSilver_old
    Inherits System.Web.UI.Page

    Dim dt As New System.Data.DataTable()
    Dim dtLectSel As New System.Data.DataTable
    Dim lecturasTotales As Integer = 0
    Dim FechaInicial, FechaFinal As DateTime

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
        End If

        Try
            dt = Cache("dst")

            Dim dr() As System.Data.DataRow
            dtLectSel = dt.Clone()

            dr = dt.DefaultView.Table.Select
            If Session("LecturasSeleccionadas") <> "" Then
                dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")
                dr = dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")
            End If
            lecturasTotales = dt.Rows.Count
            FechaInicial = Convert.ToDateTime(dt.Rows(0).Item("Fecha_Medida"))
            FechaFinal = Convert.ToDateTime(dt.Rows(lecturasTotales - 1).Item("Fecha_Medida").ToString)

            Dim fila As DataRow
            For Each fila In dr
                dtLectSel.Rows.Add(fila.ItemArray)
            Next
        Catch ex As Exception
            Exit Sub
        End Try

        Dim filtro As String = "&ID=" & Request.QueryString("ID") & ", NL=" & _
                Request.QueryString("NL") & ", TNL=" & Request.QueryString("TNL") & ", TA=" & Request.QueryString("TA")

        Dim ruta As String = ConfigurationSettings.AppSettings("rutaArchivosGraficos")


        utiles.borrarFicheros(ruta)

        dtLectSel.WriteXml(ruta & "lecturas_" & Session.SessionID & ".xml")
        'Response.Redirect("http://172.17.21.9:100/graficoDeLecturas.aspx?sesion=" & Session.SessionID & filtro)
        Response.Redirect("http://localhost:2304/graficoDeLecturas.aspx?sesion=" & Session.SessionID & filtro)
    End Sub
End Class
