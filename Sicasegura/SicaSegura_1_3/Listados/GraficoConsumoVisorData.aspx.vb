Imports OpenFlashChart
Imports System.Drawing
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles
Imports System.Collections.Generic
Imports GuarderiaFluvial.JavaScript
Imports System
Partial Class Listados_GraficoConsumoVisorData
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daConsumo As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dsConsumo As DataSet = New System.Data.DataSet()
    Dim objTrans As SqlClient.SqlTransaction
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim sentenciaSel, sentenciaOrder As String
    Dim adapter As New SqlClient.SqlDataAdapter("", conexion)
    Dim dstNombres As DataSet = New System.Data.DataSet()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            'para que el gráfico no recargue la pagina
            Response.CacheControl = "no-cache"

            'RefrescarGrafica()
        End If

    End Sub



    Private Sub RefrescarGrafica(ByVal grafico As OpenFlashChart.OpenFlashChart, ByVal maximoCaudal As Double, ByVal lbls As List(Of String))

        Dim j As Integer = 0
        Dim i As Double
        If maximoCaudal > 0 Then

            'Dim UltimoValorX As Double = midatatable.Rows(filas - 1).Item("Nivel")
            'Dim PrimerValorX As Double = midatatable.Rows(0).Item("Nivel")
            Dim ultimoValorY As Double = maximoCaudal


            grafico.Title = New OpenFlashChart.Title("")
            'grafico.Title.Style = "font-size:14px; font-family:Verdana; color: #8B8B46;font-weight: bold; background-color:#D7DDBB"
            grafico.X_Legend = New OpenFlashChart.Legend("Caudal (m3/s)")
            grafico.Y_Legend = New OpenFlashChart.Legend("Año Hidrológico (meses) ")

            grafico.X_Legend.Style = "font-size:12px; font-family:Verdana; color: #000000"
            grafico.Y_Legend.Style = "font-size:14px; font-family:Verdana; color: #000000"

            grafico.Y_Axis.Min = 0
            grafico.Y_Axis.TickLength = 5
            'grafico.Y_Axis.Steps = 10
            If ultimoValorY > 1 Then
                grafico.Y_Axis.Max = ultimoValorY * 1.1
            Else
                grafico.Y_Axis.Max = 1
            End If

            If ultimoValorY > 100 Then
                grafico.Y_Axis.Steps = 10
            Else
                grafico.Y_Axis.Steps = 1
            End If
            grafico.X_Axis.Labels.Values = lbls
            grafico.X_Axis.Labels.Vertical = True
        Else
            grafico.Title = New OpenFlashChart.Title("No existen Valores")
        End If


        'OpenFlashChart.Charts.ChartData(temp)
        'temp = New OpenFlashChart.Charts.Bar(75, "#FF0000", "Sales", 2)
        'temp.Data.Add(20)
        'temp.Data.Add(30)
        'temp.Data.Add(40)
        'temp.Data.Add(10)
        'graph.Data.Add(temp)

        Response.Clear()

        Response.Write(grafico.ToString())

        Response.End()
    End Sub

    Private Sub ValoresEjeX(ByRef lbls As List(Of String))
        lbls.Add("Octubre")
        lbls.Add("Noviembre")
        lbls.Add("Diciembre")
        lbls.Add("Enero")
        lbls.Add("Febrero")
        lbls.Add("Marzo")
        lbls.Add("Abril")
        lbls.Add("Mayo")
        lbls.Add("Junio")
        lbls.Add("Julio")
        lbls.Add("Agosto")
        lbls.Add("Septiembre")
    End Sub
End Class
