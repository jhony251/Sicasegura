Imports System.Windows.Controls.DataVisualization.Charting

Public Class Funciones

    Shared Function newSerie(ByVal grafico As Chart, ByVal serie As String, ByVal color As String) As LineSeries
        Dim line As LineSeries = New LineSeries()
        line.Name = "linea_" & grafico.Name & "_" & serie
        line.Title = serie

        line.AnimationSequence = AnimationSequence.FirstToLast

        line.BorderThickness = New System.Windows.Thickness(0)
        line.IndependentValueBinding = New System.Windows.Data.Binding("Key")
        line.DependentValueBinding = New System.Windows.Data.Binding("Value")

        Dim style As Style = New Style(GetType(LineDataPoint))

        style.Setters.Add(New Setter(LineDataPoint.BorderThicknessProperty, New Thickness(0)))
        If color <> "" Then
            style.Setters.Add(New Setter(LineDataPoint.ForegroundProperty, color))
            style.Setters.Add(New Setter(LineDataPoint.BorderBrushProperty, color))
            style.Setters.Add(New Setter(LineDataPoint.BackgroundProperty, color))
        End If

        style.Setters.Add(New Setter(LineDataPoint.WidthProperty, 3))
        style.Setters.Add(New Setter(LineDataPoint.HeightProperty, 3))

        line.DataPointStyle = style


        grafico.Series.Add(line)
        Return line
    End Function
End Class
