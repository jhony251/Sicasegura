Imports System.Windows.Controls.DataVisualization.Charting
'Imports grafico.webService
Imports SilverlightSica.webService

Imports System.Collections
Imports System.IO

Partial Public Class graficoDeConsumos
    Inherits UserControl

    Dim data As webService.srvDatosClient = New webService.srvDatosClient()
    Dim graficoNombre As String
    Dim graficoTop As Integer
    Dim graficoLeft As Integer
    Dim xmlFile As String
    Dim OBJ As New CheckBox()

    Dim color1 As String = "#284b70"
    Dim color2 As String = "#702828"
    Dim color3 As String = "#5f7143"
    Dim color4 As String = "#f5bb0c"
    Dim color5 As String = "#000000"

    Dim Var1, Var2, Var3, Var4, Var5 As String

    Public Sub New()

        InitializeComponent()
        LayoutRoot.Height = New System.Windows.LengthConverter().ConvertFromString("Auto")
        LayoutRoot.Width = New System.Windows.LengthConverter().ConvertFromString("Auto")
        
    End Sub

    Private Sub UserControl_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim sesion As String = ""
        If App.Current.Resources.Contains("sesion") Then
            sesion = App.Current.Resources("sesion").ToString()
        End If
        xmlFile = "C:\Temp\Datos\consumos_" & sesion & ".xml"

        AddHandler data.lineaGraficoCompleted, AddressOf Me.lineaGraficoCompleted
        AddHandler data.devuelveCauceCompleted, AddressOf Me.DevuelveCauce
        AddHandler data.devuelvePuntoCompleted, AddressOf Me.DevuelvePunto


        data.devuelveCauceAsync(App.Current.Resources("Cod").ToString())
        data.devuelvePuntoAsync(App.Current.Resources("Cod").ToString())
        data.lineaGraficoAsync(xmlFile)

    End Sub

    Private Sub CheckBox_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim chk As CheckBox = sender
        Dim serie As System.Windows.Controls.DataVisualization.Charting.LineSeries = cnv.FindName("linea_" & chk.Name)
        If Not serie Is Nothing Then
            If chk.IsChecked Then
                serie.Visibility = Windows.Visibility.Visible
            Else
                serie.Visibility = Windows.Visibility.Collapsed
            End If
        End If

    End Sub

    Private Sub HyperlinkButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        lnk_ocultar.Visibility = Windows.Visibility.Visible

        Canvas.SetZIndex(lnk_ocultar, 101)
        fondo.Width = App.Current.RootVisual.RenderSize.Width
        fondo.Height = App.Current.RootVisual.RenderSize.Height
        fondo.Visibility = Windows.Visibility.Visible
        Canvas.SetZIndex(fondo, 100)

        Dim hl As HyperlinkButton = sender
        Dim grafico As Chart

        Dim chkInvisible As New CheckBox
        'Primero escondemos todos los checkbox y luego depende del caso que sea lo mostraremos
        chkInvisible = cnv.FindName(grafico1.Name & "_" & Var1)
        chkInvisible.Visibility = Windows.Visibility.Collapsed
        chkInvisible = cnv.FindName(grafico1.Name & "_" & Var2)
        chkInvisible.Visibility = Windows.Visibility.Collapsed
        chkInvisible = cnv.FindName(grafico1.Name & "_" & Var3)
        chkInvisible.Visibility = Windows.Visibility.Collapsed

        If grafico2.Visibility = Windows.Visibility.Visible Then
            chkInvisible = cnv.FindName(grafico2.Name & "_" & Var4)
            chkInvisible.Visibility = Windows.Visibility.Collapsed
            chkInvisible = cnv.FindName(grafico2.Name & "_" & Var5)
            chkInvisible.Visibility = Windows.Visibility.Collapsed
        End If


        Select Case hl.Name
            Case "lnk_ampliar_grafico1"
                grafico = grafico1
                chkInvisible = cnv.FindName(grafico1.Name & "_" & Var1)
                chkInvisible.Visibility = Windows.Visibility.Visible
                Canvas.SetTop(chkInvisible, 165)
                chkInvisible = cnv.FindName(grafico1.Name & "_" & Var2)
                chkInvisible.Visibility = Windows.Visibility.Visible
                Canvas.SetTop(chkInvisible, 180)
                chkInvisible = cnv.FindName(grafico1.Name & "_" & Var3)
                chkInvisible.Visibility = Windows.Visibility.Visible
                Canvas.SetTop(chkInvisible, 195)
                Canvas.SetTop(rctVar1, 172)
                Canvas.SetTop(rctVar2, 187)
                Canvas.SetTop(rctVar3, 202)
                rctVar4.Visibility = Windows.Visibility.Collapsed
                rctVar5.Visibility = Windows.Visibility.Collapsed

            Case "lnk_ampliar_grafico2"
                grafico = grafico2
                chkInvisible = cnv.FindName(grafico2.Name & "_" & Var4)
                chkInvisible.Visibility = Windows.Visibility.Visible
                Canvas.SetTop(chkInvisible, 173)
                chkInvisible = cnv.FindName(grafico2.Name & "_" & Var5)
                chkInvisible.Visibility = Windows.Visibility.Visible
                Canvas.SetTop(chkInvisible, 188)

                rctVar1.Visibility = Windows.Visibility.Collapsed
                rctVar2.Visibility = Windows.Visibility.Collapsed
                rctVar3.Visibility = Windows.Visibility.Collapsed
                Canvas.SetTop(rctVar4, 180)
                Canvas.SetTop(rctVar5, 195)
        End Select

        graficoTop = Canvas.GetTop(grafico)
        graficoLeft = Canvas.GetLeft(grafico)
        graficoNombre = grafico.Name

        Canvas.SetTop(grafico, 220)
        Canvas.SetLeft(grafico, 0)
        Canvas.SetZIndex(grafico, 101)
        grafico.Width = 1000
        grafico.Height = 768

    End Sub

    Private Sub lnk_ocultar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        lnk_ocultar.Visibility = Windows.Visibility.Collapsed
        fondo.Visibility = Windows.Visibility.Collapsed

        Dim grafico As Chart = cnv.FindName(graficoNombre)

        Canvas.SetZIndex(grafico, 20)
        Canvas.SetLeft(grafico, graficoLeft)
        Canvas.SetTop(grafico, graficoTop)
        grafico.Width = 1000
        grafico.Height = 400


        'Hacemos todos los checkbox visibles
        Dim chkVisible As New CheckBox
        chkVisible = cnv.FindName(grafico1.Name & "_" & Var1)
        chkVisible.Visibility = Windows.Visibility.Visible
        Canvas.SetTop(chkVisible, 292)
        chkVisible = cnv.FindName(grafico1.Name & "_" & Var2)
        chkVisible.Visibility = Windows.Visibility.Visible
        Canvas.SetTop(chkVisible, 307)
        chkVisible = cnv.FindName(grafico1.Name & "_" & Var3)
        chkVisible.Visibility = Windows.Visibility.Visible
        Canvas.SetTop(chkVisible, 322)

        If grafico.Name = "grafico1" Then
            Canvas.SetTop(rctVar1, 300)
            Canvas.SetTop(rctVar2, 315)
            Canvas.SetTop(rctVar3, 330)
            If grafico2.Visibility = Windows.Visibility.Visible Then
                rctVar4.Visibility = Windows.Visibility.Visible
                rctVar5.Visibility = Windows.Visibility.Visible
            End If
        Else
            rctVar1.Visibility = Windows.Visibility.Visible
            rctVar2.Visibility = Windows.Visibility.Visible
            rctVar3.Visibility = Windows.Visibility.Visible
            Canvas.SetTop(rctVar4, 790)
            Canvas.SetTop(rctVar5, 805)
        End If



        If grafico2.Visibility = Windows.Visibility.Visible Then
            chkVisible = cnv.FindName(grafico2.Name & "_" & Var4)
            chkVisible.Visibility = Windows.Visibility.Visible
            Canvas.SetTop(chkVisible, 782)
            chkVisible = cnv.FindName(grafico2.Name & "_" & Var5)
            chkVisible.Visibility = Windows.Visibility.Visible
            Canvas.SetTop(chkVisible, 797)
        End If



    End Sub

    Private Sub crearCheckBox(ByVal grafico As Chart, ByVal titulo As String, ByVal variable As String)
        OBJ = New CheckBox()
        OBJ.Name = grafico.Name & "_" & titulo
        cnv.Children.Add(OBJ)

        Select Case variable
            Case "Var1"
                Canvas.SetTop(OBJ, 292)
            Case "Var2"
                Canvas.SetTop(OBJ, 307)
            Case "Var3"
                Canvas.SetTop(OBJ, 322)
            Case "Var4"
                Canvas.SetTop(OBJ, 782)
            Case "Var5"
                Canvas.SetTop(OBJ, 797)
        End Select

        Canvas.SetLeft(OBJ, 55)

        Canvas.SetZIndex(OBJ, 1000)

        OBJ.Content = titulo
        OBJ.ClickMode = ClickMode.Press
        OBJ.IsChecked = True
        AddHandler OBJ.Click, AddressOf Me.CheckBox_Click
    End Sub

    Public Sub lineaGraficoCompleted(ByVal sender As Object, ByVal e As lineaGraficoCompletedEventArgs)
        If e.Error Is Nothing Then
            lblFiltro1.Content = e.Result(0).Filtro1
            If Not e.Result(0).Filtro2 Is Nothing Then
                lblFiltro2.Content = e.Result(0).Filtro2
            End If
            Var1 = e.Result(0).Var1Titulo
            Var2 = e.Result(0).Var2Titulo
            Var3 = e.Result(0).Var3Titulo
            crearCheckBox(grafico1, Var1, "Var1")
            crearCheckBox(grafico1, Var2, "Var2")
            crearCheckBox(grafico1, Var3, "Var3")

            If e.Result(0).Var4Visible = "true" Then
                Var4 = e.Result(0).Var4Titulo
                Var5 = e.Result(0).Var5Titulo
                crearCheckBox(grafico2, Var4, "Var4")
                crearCheckBox(grafico2, Var5, "Var5")
            Else
                'Escondemos el gráfico y ya está.
                rctGrafico2.Visibility = Windows.Visibility.Collapsed
                lnk_ampliar_grafico2.Visibility = Windows.Visibility.Collapsed
                grafico2.Visibility = Windows.Visibility.Collapsed

            End If

            If e.Result.Count > 0 Then
                Dim sr As LineSeries = Funciones.newSerie(grafico1, Var1, color1)
                trataDatosConsumos(e.Result, sr, grafico1, e.Result(0).ElementoMedida, "Var1")
                sr = Funciones.newSerie(grafico1, Var2, color2)
                trataDatosConsumos(e.Result, sr, grafico1, e.Result(0).ElementoMedida, "Var2")
                sr = Funciones.newSerie(grafico1, Var3, color3)
                trataDatosConsumos(e.Result, sr, grafico1, e.Result(0).ElementoMedida, "Var3")

                'Dibujamos las líneas de color de la leyenda al lado de los check
                crearRectangulos("Var1")
                crearRectangulos("Var2")
                crearRectangulos("Var3")

                If e.Result(0).Var4Visible = "true" Then
                    sr = Funciones.newSerie(grafico2, Var4, color4)
                    trataDatosConsumos(e.Result, sr, grafico2, e.Result(0).ElementoMedida, "Var4")
                    sr = Funciones.newSerie(grafico2, Var5, color5)
                    trataDatosConsumos(e.Result, sr, grafico2, e.Result(0).ElementoMedida, "Var5")

                    'Dibujamos las líneas de color de la leyenda al lado de los check
                    crearRectangulos("Var4")
                    crearRectangulos("Var5")
                End If
            End If
        Else
            MessageBox.Show(e.Error.Message)
        End If
    End Sub

    Private Sub trataDatosConsumos(ByVal result As ObjectModel.ObservableCollection(Of webService.TablaListado), ByVal serie As LineSeries, ByVal grafico As Chart, ByVal titulo As String, ByVal variable As String)
        Dim linea As ICollection(Of KeyValuePair(Of DateTime, Double)) = New Dictionary(Of DateTime, Double)
        Dim caudalMax As Double = 0

        For Each r In result
            Try
                Select Case variable
                    Case "Var1"
                        linea.Add(New KeyValuePair(Of DateTime, Double)(r.Fecha_Medida, CDbl(r.Var1Valor)))
                        If caudalMax < CDbl(r.Var1Valor) Then
                            caudalMax = CDbl(r.Var1Valor)
                        End If
                    Case "Var2"
                        linea.Add(New KeyValuePair(Of DateTime, Double)(r.Fecha_Medida, CDbl(r.Var2Valor)))
                        If caudalMax < CDbl(r.Var2Valor) Then
                            caudalMax = CDbl(r.Var2Valor)
                        End If
                    Case "Var3"
                        linea.Add(New KeyValuePair(Of DateTime, Double)(r.Fecha_Medida, CDbl(r.Var3Valor)))
                        If caudalMax < CDbl(r.Var3Valor) Then
                            caudalMax = CDbl(r.Var3Valor)
                        End If
                    Case "Var4"
                        linea.Add(New KeyValuePair(Of DateTime, Double)(r.Fecha_Medida, CDbl(r.Var4Valor)))
                        If caudalMax < CDbl(r.Var4Valor) Then
                            caudalMax = CDbl(r.Var4Valor)
                        End If
                    Case "Var5"
                        linea.Add(New KeyValuePair(Of DateTime, Double)(r.Fecha_Medida, CDbl(r.Var5Valor)))
                        If caudalMax < CDbl(r.Var5Valor) Then
                            caudalMax = CDbl(r.Var5Valor)
                        End If
                End Select


            Catch ex As Exception
                'OJO DESECHAMOS EL VALOR...

            End Try
        Next
        'serie.DataContext = linea
        serie.ItemsSource = linea

        Dim axisY As LinearAxis = grafico.Axes(0)
        If IsNothing(axisY.Maximum) Then
            axisY.Maximum = caudalMax * 1.05
        ElseIf axisY.Maximum < caudalMax Then
            axisY.Maximum = caudalMax * 1.05
        End If
        grafico.Title = titulo

    End Sub

    Private Sub crearRectangulos(ByVal variable As String)
        'Con este select calculamos el left del rectángulo para que se dibuje después del título.
        Select Case Var1
            Case "Caudal Mínimo Intervalo (Qmin)(m3/s)"
                Canvas.SetLeft(rctVar1, 302)
                Canvas.SetLeft(rctVar2, 276)
                Canvas.SetLeft(rctVar3, 308)
                Canvas.SetLeft(rctVar4, 205)
                Canvas.SetLeft(rctVar5, 230)
            Case "Lectura contador reactiva (Kvar)"
                Canvas.SetLeft(rctVar1, 265)
                Canvas.SetLeft(rctVar2, 255)
                Canvas.SetLeft(rctVar3, 245)
                Canvas.SetLeft(rctVar4, 258)
                Canvas.SetLeft(rctVar5, 283)
            Case "Volumen (m3)"
                Canvas.SetLeft(rctVar1, 163)
                Canvas.SetLeft(rctVar2, 212)
                Canvas.SetLeft(rctVar3, 230)
        End Select


        Select Case variable
            Case "Var1"
                Canvas.SetTop(rctVar1, 300)
                rctVar1.Visibility = Windows.Visibility.Visible
            Case "Var2"
                Canvas.SetTop(rctVar2, 315)
                rctVar2.Visibility = Windows.Visibility.Visible
            Case "Var3"
                Canvas.SetTop(rctVar3, 330)
                rctVar3.Visibility = Windows.Visibility.Visible
            Case "Var4"
                Canvas.SetTop(rctVar4, 790)
                rctVar4.Visibility = Windows.Visibility.Visible
            Case "Var5"
                Canvas.SetTop(rctVar5, 805)
                rctVar5.Visibility = Windows.Visibility.Visible
        End Select

    End Sub

    Private Sub DevuelveCauce(ByVal sender As Object, ByVal e As devuelveCauceCompletedEventArgs)
        Cauce.Content = e.Result
    End Sub

    Private Sub DevuelvePunto(ByVal sender As Object, ByVal e As devuelvePuntoCompletedEventArgs)
        Punto.Content = e.Result
    End Sub

End Class
