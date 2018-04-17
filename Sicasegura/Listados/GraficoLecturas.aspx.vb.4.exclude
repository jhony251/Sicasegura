Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.JavaScript
Imports srvDatos

Partial Class Listados_GraficoLecturas
    Inherits System.Web.UI.Page

    Dim dstLecturas As New System.Data.DataSet
    Dim dt As New System.Data.DataTable()
    Dim dtLectSel As New System.Data.DataTable
    Dim lecturasTotales As Integer = 0
    Dim FechaInicial, FechaFinal As DateTime
    Dim filtro As String = ""

    Public Function initParams() As String
        If dt Is Nothing And filtro <> "" Then
            dt = Session("dst")
            filtro = ", Cod=" & dt.Rows(0).Item("codigoPVYCR") & ", IdEM=" & dt.Rows(0).Item("idElementoMedida")
        End If

        Return "tipoGrafico=graficoDeLecturas, sesion=" & Session.SessionID & ", ID=" & Request.QueryString("ID") & ", NL=" & _
                Request.QueryString("NL") & ", TNL=" & Request.QueryString("TNL") & ", TA=" & Request.QueryString("TA") & filtro

    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
        End If

        Try
            dt = Session("dst").copy()
        Catch ex As Exception
            dstLecturas = Session("dst").copy()
            dt = dstLecturas.Tables("TablaAuxiliar")
        End Try

        Try
            'dst = Session("dst")
            'dt = dst.Tables(0)          
            'dt = Session("dst")

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

        filtro = ", Cod=" & dtLectSel.Rows(0).Item("codigoPVYCR") & ", IdEM=" & dtLectSel.Rows(0).Item("idElementoMedida")

        'Borramos las que no necesitamos
        dtLectSel.Columns.Remove("Escala_M")
        dtLectSel.Columns.Remove("Calado_M")
        dtLectSel.Columns.Remove("Observaciones")
        dtLectSel.Columns.Remove("NumeroParada")
        dtLectSel.Columns.Remove("Duda_Calidad")
        dtLectSel.Columns.Remove("modificado")
        If Not dtLectSel.Columns("Diferencial") Is Nothing Then
            dtLectSel.Columns.Remove("Diferencial")
            dtLectSel.Columns.Remove("Diferencial_acum")
        End If
        dtLectSel.Columns.Remove("DescFuenteDato")
        dtLectSel.Columns.Remove("CodigoPVYCR")
        dtLectSel.Columns.Remove("idElementoMedida")

        'Renombramos la que nos quedan con nombre más cortitos.
        dtLectSel.Columns("Cod_Fuente_Dato").ColumnName = "CFD"
        dtLectSel.Columns("Fecha_Medida").ColumnName = "FM"
        dtLectSel.Columns("TipoObtencionCaudal").ColumnName = "TOC"
        dtLectSel.Columns("RegimenCurva").ColumnName = "RC"
        dtLectSel.Columns("Caudal_M3S").ColumnName = "C"

        'Renombramos también la tabla, para que ocupe todavía menos el archivo
        dtLectSel.TableName = "T"

        Dim ruta As String = System.Configuration.ConfigurationManager.AppSettings("rutaArchivosGraficos")

        utiles.borrarFicheros(ruta)

        dtLectSel.WriteXml(ruta & "lecturas_" & Session.SessionID & ".xml")
    End Sub
End Class
