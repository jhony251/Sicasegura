Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles

Partial Class Listados_GraficoConsumos
    Inherits System.Web.UI.Page

    Dim dstConsumos As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
        End If

        Try
            dstConsumos = Session("dst").copy
        Catch ex As Exception
            Exit Sub
        End Try

        Dim numLecturas As Integer = dstConsumos.Tables("TablaAuxiliar").Rows.Count

        dstConsumos.Tables.Remove("A0")
        dstConsumos.Tables.Remove("A1")
        dstConsumos.Tables.Remove("TablaAuxiliar")

        dstConsumos.Tables("listado").Columns.Remove("rama")
        dstConsumos.Tables("listado").Columns.Remove("punto")
        dstConsumos.Tables("listado").Columns.Remove("descTipoElem")
        dstConsumos.Tables("listado").Columns.Remove("fecha_MedidaF")

        dstConsumos.Tables("listadoGlobales").Columns.Remove("numPags")
        dstConsumos.Tables("listadoGlobales").Columns.Add(New DataColumn("L", Type.GetType("System.String")))
        dstConsumos.Tables("listadoGlobales").Columns.Add(New DataColumn("V1T", Type.GetType("System.String")))
        dstConsumos.Tables("listadoGlobales").Columns.Add(New DataColumn("V2T", Type.GetType("System.String")))
        dstConsumos.Tables("listadoGlobales").Columns.Add(New DataColumn("V3T", Type.GetType("System.String")))
        dstConsumos.Tables("listadoGlobales").Columns.Add(New DataColumn("V3Vi", Type.GetType("System.String")))
        dstConsumos.Tables("listadoGlobales").Columns.Add(New DataColumn("V4T", Type.GetType("System.String")))
        dstConsumos.Tables("listadoGlobales").Columns.Add(New DataColumn("V4Vi", Type.GetType("System.String")))
        dstConsumos.Tables("listadoGlobales").Columns.Add(New DataColumn("V5T", Type.GetType("System.String")))
        dstConsumos.Tables("listadoGlobales").Columns.Add(New DataColumn("V5Vi", Type.GetType("System.String")))

        dstConsumos.Tables("listadoGlobales").Rows(0).Item("L") = numLecturas
        dstConsumos.Tables("listadoGlobales").Rows(0).Item("V1T") = dstConsumos.Tables("listado").Rows(0).Item("Var1Titulo")
        dstConsumos.Tables("listadoGlobales").Rows(0).Item("V2T") = dstConsumos.Tables("listado").Rows(0).Item("Var2Titulo")
        dstConsumos.Tables("listadoGlobales").Rows(0).Item("V3T") = dstConsumos.Tables("listado").Rows(0).Item("Var3Titulo")
        dstConsumos.Tables("listadoGlobales").Rows(0).Item("V3Vi") = dstConsumos.Tables("listado").Rows(0).Item("Var3Visible")
        dstConsumos.Tables("listadoGlobales").Rows(0).Item("V4T") = dstConsumos.Tables("listado").Rows(0).Item("Var4Titulo")
        dstConsumos.Tables("listadoGlobales").Rows(0).Item("V4Vi") = dstConsumos.Tables("listado").Rows(0).Item("Var4Visible")
        dstConsumos.Tables("listadoGlobales").Rows(0).Item("V5T") = dstConsumos.Tables("listado").Rows(0).Item("Var5Titulo")
        dstConsumos.Tables("listadoGlobales").Rows(0).Item("V5Vi") = dstConsumos.Tables("listado").Rows(0).Item("Var5Visible")

        dstConsumos.Tables("listado").Columns.Remove("Var1Titulo")
        dstConsumos.Tables("listado").Columns.Remove("Var2Titulo")
        dstConsumos.Tables("listado").Columns.Remove("Var3Titulo")
        dstConsumos.Tables("listado").Columns.Remove("Var3Visible")
        dstConsumos.Tables("listado").Columns.Remove("Var4Titulo")
        dstConsumos.Tables("listado").Columns.Remove("Var4Visible")
        dstConsumos.Tables("listado").Columns.Remove("Var5Titulo")
        dstConsumos.Tables("listado").Columns.Remove("Var5Visible")

        dstConsumos.Tables("listado").Columns("Var1Valor").ColumnName = "V1Va"
        dstConsumos.Tables("listado").Columns("Var2Valor").ColumnName = "V2Va"
        dstConsumos.Tables("listado").Columns("Var3Valor").ColumnName = "V3Va"
        dstConsumos.Tables("listado").Columns("Var4Valor").ColumnName = "V4Va"
        dstConsumos.Tables("listado").Columns("Var5Valor").ColumnName = "V5Va"
        dstConsumos.Tables("listado").Columns("Fecha_Medida").ColumnName = "FM"

        dstConsumos.Tables("listadoGlobales").TableName = "LG"


        Dim ruta As String = System.Configuration.ConfigurationManager.AppSettings("rutaArchivosGraficos")

        utiles.borrarFicheros(ruta)

        dstConsumos.WriteXml(ruta & "consumos_" & Session.SessionID & ".xml")

    End Sub

    Public Function initParams() As String
        Return "tipoGrafico=graficoDeConsumos, sesion=" & Session.SessionID & ", Cod=" & Request.QueryString("Cod") & ", IEM=" & Request.QueryString("IEM")
    End Function
End Class
