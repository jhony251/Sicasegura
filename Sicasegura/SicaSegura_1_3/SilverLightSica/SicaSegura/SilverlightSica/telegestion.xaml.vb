Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Diagnostics.CodeAnalysis
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.DataVisualization.Charting
Imports System.Text
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes

Partial Public Class telegestion
    Inherits UserControl
    Dim data As webService.srvDatosClient = New webService.srvDatosClient()
    Dim punto As String = "VA002P30"


    Public Sub New()
        InitializeComponent()

    End Sub




    Private Sub gridNivus_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

        AddHandler data.graficosCompleted, AddressOf Me.lineasCompleted
        AddHandler data.listadoPuntosCompleted, AddressOf Me.listadoEnd
        data.listadoPuntosAsync()

    End Sub

    Private Sub listadoEnd(ByVal sender As Object, ByVal e As webService.listadoPuntosCompletedEventArgs)
        If e.Error Is Nothing Then
            Me.dtgNivus.ItemsSource = e.Result
            ordenarColumnas()
        Else
            MessageBox.Show(e.Error.Message)
        End If

    End Sub


    Private Sub lineaCaudal_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.fechaInicio.SelectedDate = Now.AddDays(-1)
        Me.fechaFin.SelectedDate = Now
        filtrar()
    End Sub


#Region "FILTRO"
    Private Function getMaxValor(ByVal valorActual As Double, ByVal Caudal_M3S As Double, ByVal Velocidad_MS As Double, ByVal Calado_M As Double) As Double
        If valorActual >= Caudal_M3S And valorActual >= Velocidad_MS And valorActual >= Calado_M Then
            Return valorActual
        End If
        If Caudal_M3S >= valorActual And Caudal_M3S >= Velocidad_MS And Caudal_M3S >= Calado_M Then
            Return Caudal_M3S
        End If

        If Velocidad_MS >= valorActual And Velocidad_MS >= Caudal_M3S And Velocidad_MS >= Calado_M Then
            Return Velocidad_MS
        End If

        If Calado_M >= valorActual And Calado_M >= Caudal_M3S And Calado_M >= Velocidad_MS Then
            Return Calado_M
        End If
    End Function


    Private Sub lineasCompleted(ByVal sender As Object, ByVal e As webService.graficosCompletedEventArgs)
        If e.Error Is Nothing Then
            Dim lineaCaudal As ICollection(Of KeyValuePair(Of DateTime, Double)) = New Dictionary(Of DateTime, Double)
            Dim lineaVelocidad As ICollection(Of KeyValuePair(Of DateTime, Double)) = New Dictionary(Of DateTime, Double)
            Dim lineaAltura As ICollection(Of KeyValuePair(Of DateTime, Double)) = New Dictionary(Of DateTime, Double)

            Dim valorMax As Double = 0

            For Each r In e.Result
                lineaCaudal.Add(New KeyValuePair(Of DateTime, Double)(r.Fecha, r.Caudal_M3S))
                lineaVelocidad.Add(New KeyValuePair(Of DateTime, Double)(r.Fecha, r.Velocidad_MS))
                lineaAltura.Add(New KeyValuePair(Of DateTime, Double)(r.Fecha, r.Calado_M))
                valorMax = getMaxValor(valorMax, r.Caudal_M3S, r.Velocidad_MS, r.Calado_M)

            Next
            For Each ls As LineSeries In Me.grafico.Series
                'ls.DataPointStyle = style4
                'ls.Style = style4
                ls.DataContext = Nothing
                ls.Visibility = Windows.Visibility.Visible

                Select Case ls.Name
                    Case "lineaCaudal"
                        ls.DataContext = lineaCaudal
                    Case "lineaAltura"
                        ls.DataContext = lineaAltura

                    Case "lineaVelocidad"
                        ls.DataContext = lineaVelocidad

                End Select


            Next


            Dim axisY As LinearAxis = grafico.Axes(0)
            axisY.Maximum = valorMax * 1.05


            'Me.grafico.Refresh()
            lineaAltura = Nothing
            lineaCaudal = Nothing
            lineaVelocidad = Nothing
            'mostrarSeries(Windows.Visibility.Visible)
            GC.Collect()
            Me.grafico.Title = punto
        Else
            MessageBox.Show(e.Error.Message)
        End If
    End Sub



    Private Sub filtrar()
        grafico.Title = "Cargando los datos..."
        If Me.fechaInicio.SelectedDate < Me.fechaFin.SelectedDate Then
            mostrarSeries(Windows.Visibility.Collapsed)
            data.graficosAsync(punto, Me.fechaInicio.SelectedDate, Me.fechaFin.SelectedDate)

        End If
        GC.Collect()
    End Sub

    Private Sub filtrarFechas_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        filtrar()
    End Sub

    Private Sub buscarNuevoNivus(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        punto = DirectCast(DirectCast(Me.dtgNivus.SelectedItem, System.Object), SilverlightSica.webService.PVYCR_DatosAcequias_NIVUS).CodigoPVYCR
        filtrar()

    End Sub

    Private Sub mostrarSeries(ByVal mostrar As Windows.Visibility)
        For Each s As Series In Me.grafico.Series
            s.Visibility = mostrar
        Next
    End Sub
#End Region

    Private Sub dtgNivus_AutoGeneratingColumn(ByVal sender As System.Object, ByVal e As System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs)
        ordenarColumnas()
    End Sub

    Private Sub ordenarColumnas()
        If Me.dtgNivus.Columns.Count < 5 Then
            Exit Sub
        End If
        With Me.dtgNivus


            For Each col As DataGridColumn In .Columns
                Select Case col.Header.ToString
                    Case "CodigoPVYCR"
                        col.Header = "Codigo"
                        col.DisplayIndex = 0

                    Case "Fecha"
                        col.DisplayIndex = 1

                    Case "Calado_M"
                        col.Header = "Calado (m)"
                        col.DisplayIndex = 2

                    Case "Caudal_M3S"
                        col.Header = "Caudal (m3/s)"
                        col.DisplayIndex = 3


                    Case "Velocidad_MS"
                        col.Header = "Velocidad (m/s)"
                        col.DisplayIndex = 4

                    Case "Hora"
                        col.Visibility = Windows.Visibility.Collapsed
                End Select

            Next
            '.Columns("CodigoPVYCR").DisplayIndex = 0
            '.Columns("CodigoPVYCR").Header = "Codigo"
        End With
    End Sub
End Class
