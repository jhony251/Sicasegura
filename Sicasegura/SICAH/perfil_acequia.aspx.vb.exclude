Imports ASP
Imports dotnetCHARTING
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Web.Profile
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports GuarderiaFluvial

Partial Class SICAH_perfil_acequia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim series3 As New Series
        Dim series2 As New Series
        Dim series, series4 As New Series
        Dim element9 As New Element
        Dim element10 As New Element
        Dim element11 As New Element
        Dim element12 As New Element
        Dim element13 As New Element
        Dim element14 As New Element
        Dim element7 As New Element
        Dim element8 As New Element
        Dim element As New Element
        Dim element2 As New Element
        Dim element3 As New Element
        Dim element4 As New Element
        Dim element5 As New Element
        Dim element6 As New Element
        Dim random As New Random
        Dim selectConnection As New SqlConnection(ConfigurationSettings.AppSettings.Item("dsnsegura_migracion"))
        Dim adapter As New SqlDataAdapter("", selectConnection)
        Dim dataSet As New DataSet
        If (selectConnection.State = ConnectionState.Closed) Then
            selectConnection.Open()
        End If

        Dim CodigoPunto As String

        'RDF. Fecha: 04/09/2008
        If Not Request.QueryString("Codigo") Is Nothing Then
            CodigoPunto = Request.QueryString("Codigo")
        Else
            CodigoPunto = Me.Session.Item("CodigoPVYCR")
        End If
        utiles.Comprobar_Conexion_BD(Page, selectConnection)
        adapter.SelectCommand.CommandText = "SELECT c.DenominacionCauce,DenominacionPunto, A1_M,A2_M,B1_M,B2_M,B3_M,H1_M,H2_M,Offset_M FROM PVYCR_Cauces C, PVYCR_Puntos P where P.codigoCauce = C.codigoCauce and P.CodigoPVYCR = '" + CodigoPunto + "'"
        'adapter.SelectCommand.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("SELECT DenominacionPunto, A1_M,A2_M,B1_M,B2_M,B3_M,H1_M,H2_M,Offset_M FROM PVYCR_Puntos P where P.CodigoPVYCR = '", Me.Session.Item("CodigoPVYCR")), "'"))

        adapter.Fill(dataSet, "TablaGrafico")
        element9.XValue = 0
        element9.YValue = (Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString)))
        element10.XValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString))
        'element10.YValue = CShort((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) - Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString))))
        element10.YValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString))
        element11.XValue = CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A2_M").ToString))))
        element11.YValue = 0
        element12.XValue = CDbl((((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A2_M").ToString)))) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B1_M").ToString))))
        element12.YValue = 0
        element13.XValue = CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B2_M").ToString))))
        '        element13.YValue = CShort((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) - Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString))))
        element13.YValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString))
        element14.XValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString))
        element14.YValue = CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString))))
        element7.XValue = (CDbl(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString))) / 2)
        element7.YValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("Offset_M").ToString))
        element8.XValue = (CDbl(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString))) / 2)
        element8.YValue = 0
        element.XValue = 0
        element.YValue = 0
        'element2.XValue = 0
        'element2.YValue = (Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString)))
        'element3.XValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString))
        'element3.YValue = (Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString)))
        'element4.XValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString))
        'element4.YValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString))
        'element5.XValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString))
        'element5.YValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString))
        'element6.XValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString))
        'element6.YValue = 0

        'calculamos la escala
        element2.XValue = -0.25
        element2.YValue = 0
        element3.XValue = -0.25
        element3.YValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("offset_m").ToString))
        'element3.YValue = 0.5
        element4.XValue = -0.25
        element4.YValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("offset_m").ToString))
        'element4.YValue = 0.5
        element5.XValue = -0.25
        element5.YValue = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString))
        'añadimos los puntos a las series para dibujar las líneas
        series3.Elements.Add(element9)
        series3.Elements.Add(element10)
        series3.Elements.Add(element11)
        series3.Elements.Add(element12)
        series3.Elements.Add(element13)
        series3.Elements.Add(element14)
        'calcula el offset no quieren que aparezca
        series2.Elements.Add(element7)
        series2.Elements.Add(element8)

        series.Elements.Add(element2)
        series.Elements.Add(element3)
        series4.Elements.Add(element4)
        series4.Elements.Add(element5)
        '        series.Elements.Add(element5)
        '       series.Elements.Add(element6)
        Me.grafico.DefaultSeries.LegendEntry.Visible = False
        'Me.grafico.DefaultLegendBox.DefaultEntry.Value = "pruebas"
        'Me.grafico.DefaultLegendBox.DefaultEntry.Name = "pruebas nombre"
        'Me.grafico.LegendBox.Visible = False
        'Me.grafico.DefaultLegendBox.Visible = False
        'no sale ninguna
        'Me.grafico.DefaultLegendBox.DefaultEntry.Visible = False
        'Me.grafico.LegendBox.DefaultEntry.Visible = False
        'Me.grafico.DefaultSeries.LegendEntry.Visible = False
        'Me.grafico.DefaultTitleBox.Visible = False

        Dim legendEntry As New LegendEntry
        Dim entry2 As New LegendEntry
        legendEntry.Name = "Sección"
        legendEntry.Background.Color = Color.Red
        Me.grafico.LegendBox.ExtraEntries.Add(legendEntry)
        entry2 = New LegendEntry
        entry2.Name = "Offset"
        entry2.Background.Color = Color.LimeGreen
        Me.grafico.LegendBox.ExtraEntries.Add(entry2)
        entry2 = New LegendEntry
        entry2.Name = "Escala"
        entry2.Background.Color = Color.Blue
        Me.grafico.LegendBox.ExtraEntries.Add(entry2)

        'serie que dibuja la sección
        series3.DefaultElement.Color = Color.Red
        'serie que dibuja el offset
        series2.DefaultElement.Color = Color.LimeGreen
        'serie que dibuja la linea de la escala
        series4.DefaultElement.Color = Color.Blue
        series.DefaultElement.Color = Color.LimeGreen

        series3.Line.Width = 4
        series.Line.Width = 4
        series.Line.Width = 4
        Me.grafico.XAxis.Minimum = -0.5
        Me.grafico.YAxis.Minimum = -0.5
        Me.grafico.DefaultSeries.Type = SeriesType.Line
        Me.grafico.DefaultSeries.Line.Width = 2
        Me.grafico.BackColor = Color.Transparent
        'color del fonde de la grafica
        Me.grafico.DefaultChartArea.Background.Color = Color.White
        'color del fonde de la grafica
        Me.grafico.DefaultShadow.Color = Color.White

        'RDF. Fecha: 04/09/2008
        'Visualización desde los mantenimientos
        'Me.grafico.TitleBox.Label = New Label(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Me.Session.Item("codigoPVYCR"), "-"), dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("DenominacionPunto").ToString)), New Font("Arial", 13.0!, FontStyle.Bold), Color.White)
        Me.grafico.TitleBox.Label = New Label(CodigoPunto + " / " + dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("DenominacionPunto").ToString + " / " + dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("DenominacionCauce").ToString, New Font("Arial", 12.0!, FontStyle.Bold), Color.White)

        Me.grafico.TitleBox.Position = TitleBoxPosition.FullWithLegend
        Me.grafico.TitleBox.CornerTopLeft = BoxCorner.Round
        Me.grafico.TitleBox.CornerTopRight = BoxCorner.Round
        Me.grafico.TitleBox.Background.ShadingEffectMode = ShadingEffectMode.Five
        Me.grafico.TitleBox.Background.Color = Color.DarkKhaki
        Me.grafico.TitleBox.CornerSize = 20
        Me.grafico.LegendBox.HeaderLabel = New Label("Sección Acequia", New Font("Arial", 9.0!, FontStyle.Bold), Color.White)
        Me.grafico.LegendBox.HeaderLabel.Alignment = StringAlignment.Center
        Me.grafico.LegendBox.HeaderBackground.ShadingEffectMode = ShadingEffectMode.Two
        Me.grafico.LegendBox.HeaderBackground.Color = Color.OliveDrab
        Me.grafico.LegendBox.CornerTopLeft = BoxCorner.Square
        Me.grafico.XAxis.DefaultTick.Label.Font = New Font("Arial", 8.0!, FontStyle.Bold)
        Me.grafico.YAxis.DefaultTick.Label.Font = New Font("Arial", 8.0!, FontStyle.Bold)
        Me.grafico.Use3D = False
        Me.grafico.Mentor = False
        Me.grafico.Debug = False
        'para que no se diferencie el área de la grafica de la ventana ponemos igual color en ambos sitios
        Me.grafico.BackColor = Color.Azure
        Me.grafico.Background.Color = Color.Azure
        Me.grafico.DefaultAxis.Line.Width = 2
        Me.grafico.SeriesCollection.Add(series3)
        'Me.grafico.SeriesCollection.Add(series2)
        Me.grafico.SeriesCollection.Add(series)
        Me.grafico.SeriesCollection.Add(series4)
    End Sub

End Class
