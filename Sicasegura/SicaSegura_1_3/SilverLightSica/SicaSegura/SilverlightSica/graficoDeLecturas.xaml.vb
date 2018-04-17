Imports System.Windows.Controls.DataVisualization.Charting
Imports SilverlightSica.webService
Imports System.Collections
Imports System.IO
Imports System.Windows.Markup

Partial Public Class graficoDeLecturas
    Inherits UserControl
    Dim data As webService.srvDatosClient = New webService.srvDatosClient()
    Dim graficoNombre As String
    Dim graficoTop As Integer
    Dim graficoLeft As Integer
    Dim xmlFile As String
    Dim OBJ As New CheckBox()
    
    Dim TOC As String()
    Dim CFD As String()
    Dim RC As String()
    Dim ColorNuevoTOC As Dictionary(Of String, SolidColorBrush) = New Dictionary(Of String, SolidColorBrush)
    Dim ColorNuevoCFD As Dictionary(Of String, SolidColorBrush) = New Dictionary(Of String, SolidColorBrush)
    Dim ColorNuevoRC As Dictionary(Of String, SolidColorBrush) = New Dictionary(Of String, SolidColorBrush)


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

        xmlFile = "C:\Temp\Datos\lecturas_" & sesion & ".xml"
        CargaGrafico1.IsBusy = True
        CargaGrafico2.IsBusy = True
        CargaGrafico3.IsBusy = True
        CargaGrafico4.IsBusy = True
        CargaGrafico5.IsBusy = True
        CargaGrafico6.IsBusy = True

        AddHandler data.tiposDeVariablesCompleted, AddressOf Me.tipoDeVariablesCompleted
        AddHandler data.lineaObtencionCaudalCompleted, AddressOf Me.lineaObtencionCaudalCompleted
        AddHandler data.lineaCodFuenteDatoCompleted, AddressOf Me.lineaCodFuenteDatoCompleted
        AddHandler data.lineaRegimenCurvaCompleted, AddressOf Me.lineaRegimenCurvaCompleted
        AddHandler data.ListaDeVariablesCompleted, AddressOf Me.ListaDeVariables
        AddHandler data.devuelveCauceCompleted, AddressOf Me.DevuelveCauce
        AddHandler data.devuelvePuntoCompleted, AddressOf Me.DevuelvePunto


        data.devuelveCauceAsync(App.Current.Resources("Cod").ToString())
        data.devuelvePuntoAsync(App.Current.Resources("Cod").ToString())

        data.ListaDeVariablesAsync(xmlFile, "TOC")
        data.ListaDeVariablesAsync(xmlFile, "CFD")
        data.ListaDeVariablesAsync(xmlFile, "RC")


        'IPIM: Sacamos el filtro aplicado
        ID.Content = ID.Content & App.Current.Resources("ID").ToString()
        NL.Content = NL.Content & App.Current.Resources("NL").ToString()
        TNL.Content = TNL.Content & App.Current.Resources("TNL").ToString()
        TA.Content = TA.Content & App.Current.Resources("TA").ToString()

    End Sub

    Private Sub tipoDeVariablesCompleted(ByVal sender As Object, ByVal e As tiposDeVariablesCompletedEventArgs)
        If e.Error Is Nothing Then
            If e.Result.Count > 0 Then
                For i As Integer = 1 To e.Result.Count - 1
                    Select Case e.Result(0)
                        Case "TOC"
                            If i = 1 Then
                                ReDim TOC(e.Result.Count - 1)
                            End If
                            data.lineaObtencionCaudalAsync(xmlFile, e.Result(i), i)
                            crearCheckBox(graficoObtencionCaudal, i, e.Result(i), "TOC")
                            TOC(i - 1) = e.Result(i)

                        Case "CFD"
                            If i = 1 Then
                                ReDim CFD(e.Result.Count - 1)
                            End If
                            data.lineaCodFuenteDatoAsync(xmlFile, e.Result(i))
                            crearCheckBox(graficoCaudalCodFuenteDato, i, e.Result(i), "CFD")
                            CFD(i - 1) = e.Result(i)
                        Case "RC"
                            If i = 1 Then
                                ReDim RC(e.Result.Count - 1)
                            End If
                            If Not e.Result(i) Is System.DBNull.Value And Not e.Result(i) Is Nothing Then
                                data.lineaRegimenCurvaAsync(xmlFile, e.Result(i))
                                crearCheckBox(graficoRegimenDeCurva, i, e.Result(i), "RC")
                                RC(i - 1) = e.Result(i)
                            End If
                    End Select
                Next
            End If
        Else
            'OJO FALTA ERROR
            'MessageBox.Show("1: " & e.Error.Message)
        End If


    End Sub


    Private Sub trataDatos(ByVal result As ObjectModel.ObservableCollection(Of webService.TablaAcequias), ByVal serie As LineSeries, ByVal grafico As Chart, ByVal titulo As String)
        Dim linea As ICollection(Of KeyValuePair(Of DateTime, Double)) = New Dictionary(Of DateTime, Double)
        Dim caudalMax As Double = 0


        For Each r In result
            Try

                linea.Add(New KeyValuePair(Of DateTime, Double)(r.Fecha_Medida, CDbl(r.Caudal_M3S.Replace(".", ","))))
                If caudalMax < CDbl(r.Caudal_M3S.Replace(".", ",")) Then
                    caudalMax = CDbl(r.Caudal_M3S.Replace(".", ","))
                End If


            Catch ex As Exception
                'OJO DESECHAMOS EL VALOR...
            End Try
        Next
        serie.ItemsSource = linea

        Dim axisY As LinearAxis = grafico.Axes(0)
        If IsNothing(axisY.Maximum) Then
            axisY.Maximum = caudalMax * 1.05
        ElseIf axisY.Maximum < caudalMax Then
            axisY.Maximum = caudalMax * 1.05
        End If
        'grafico.Title = result(0).CodigoPVYCR.ToString & " - " & result(0).idElementoMedida & vbCrLf & titulo
        grafico.Title = App.Current.Resources("Cod").ToString() & " - " & App.Current.Resources("IdEM").ToString() & vbCrLf & titulo
    End Sub

    Public Sub lineaRegimenCurvaCompleted(ByVal sender As Object, ByVal e As lineaRegimenCurvaCompletedEventArgs)
        If e.Error Is Nothing Then
            If e.Result.Count > 0 Then
                Dim color As String = ""
                Dim tipoCurva As String = e.Result(0).RegimenCurva.ToString
                color = ColorNuevoRC(tipoCurva).Color.ToString
                Dim sr As LineSeries = Funciones.newSerie(graficoRegimenDeCurva, tipoCurva, color)
                trataDatos(e.Result, sr, Me.graficoRegimenDeCurva, "Informe de Caudales por Régimen Curva")
                'pieSeries(graficoPieRegimenDeCurva, tipoCurva, e.Result.Count, color)

            End If
        Else
            'MessageBox.Show("4: " & e.Error.Message)
        End If
        CargaGrafico1.IsBusy = False
        CargaGrafico2.IsBusy = False

    End Sub

    Public Sub lineaCodFuenteDatoCompleted(ByVal sender As Object, ByVal e As lineaCodFuenteDatoCompletedEventArgs)
        If e.Error Is Nothing Then
            If e.Result.Count > 0 Then

                Dim color As String
                Dim tipoCurva As String = e.Result(0).Cod_Fuente_Dato.ToString
                color = ColorNuevoCFD(tipoCurva).Color.ToString
                Dim sr As LineSeries = Funciones.newSerie(graficoCaudalCodFuenteDato, tipoCurva, color)
                trataDatos(e.Result, sr, Me.graficoCaudalCodFuenteDato, "Informe de Caudales por cod_Fuente_Dato")
                CargaGrafico3.IsBusy = False
                'pieSeries(graficoPieCaudalCodFuenteDato, tipoCurva, e.Result.Count, color)
                CargaGrafico4.IsBusy = False
            End If
        Else
            'MessageBox.Show("3: " & e.Error.Message)
        End If


    End Sub

    Public Sub lineaObtencionCaudalCompleted(ByVal sender As Object, ByVal e As lineaObtencionCaudalCompletedEventArgs)

        If e.Error Is Nothing Then
            If e.Result.Count > 0 Then
                Dim color As String
                Dim tipoCurva As String = e.Result(0).TipoObtencionCaudal.ToString
                color = ColorNuevoTOC(tipoCurva).Color.ToString
                Dim sr As LineSeries = Funciones.newSerie(graficoObtencionCaudal, tipoCurva, color)
                trataDatos(e.Result, sr, Me.graficoObtencionCaudal, "Informe de Caudales por TipoObtencionCaudal")
                CargaGrafico5.IsBusy = False
                'pieSeries(graficoPieObtencionCaudal, tipoCurva, e.Result.Count, color)
                CargaGrafico6.IsBusy = False
            End If
        Else
            'MessageBox.Show("2: " & e.Error.Message)
        End If



    End Sub
    'Private Sub pieSeries(ByVal grafico As Chart, ByVal clave As String, ByVal valor As Integer, ByVal color As String)
    '    Dim pieSerie As PieSeries
    '    Dim valores As ICollection(Of KeyValuePair(Of String, Integer))

    '    If grafico.Series.Count = 0 Then
    '        pieSerie = New PieSeries()
    '        valores = New Dictionary(Of String, Integer)
    '        pieSerie.IndependentValueBinding = New System.Windows.Data.Binding("Key")
    '        pieSerie.DependentValueBinding = New System.Windows.Data.Binding("Value")
    '    Else
    '        pieSerie = grafico.Series(0)
    '        valores = pieSerie.ItemsSource
    '    End If

    '    valores.Add(New KeyValuePair(Of String, Integer)(clave, valor))
    '    pieSerie.ItemsSource = valores

    '    If grafico.Series.Count > 0 Then
    '        grafico.Series.RemoveAt(0)
    '    End If

    '    grafico.Series.Add(pieSerie)
    'End Sub

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
        For i As Integer = 0 To TOC.Length - 1
            If Not TOC(i) Is Nothing Then
                chkInvisible = cnv.FindName(graficoObtencionCaudal.Name & "_" & TOC(i))
                chkInvisible.Visibility = Windows.Visibility.Collapsed
            End If
        Next
        For i As Integer = 0 To CFD.Length - 1
            If Not CFD(i) Is Nothing Then
                chkInvisible = cnv.FindName(graficoCaudalCodFuenteDato.Name & "_" & CFD(i))
                chkInvisible.Visibility = Windows.Visibility.Collapsed
            End If
        Next
        For i As Integer = 0 To RC.Length - 1
            If Not RC(i) Is Nothing Then
                chkInvisible = cnv.FindName(graficoRegimenDeCurva.Name & "_" & RC(i))
                chkInvisible.Visibility = Windows.Visibility.Collapsed
            End If
        Next

        Select Case hl.Name
            Case "lnk_regimenCurva"
                grafico = Me.graficoRegimenDeCurva
                For i As Integer = 0 To RC.Length - 1
                    If Not RC(i) Is Nothing Then
                        chkInvisible = cnv.FindName(graficoRegimenDeCurva.Name & "_" & RC(i))
                        chkInvisible.Visibility = Windows.Visibility.Visible
                        Canvas.SetTop(chkInvisible, 163)
                    End If
                Next
            Case "lnk_ampliar_obtencionCaudal"
                grafico = Me.graficoObtencionCaudal
                For i As Integer = 0 To TOC.Length - 1
                    If Not TOC(i) Is Nothing Then
                        chkInvisible = cnv.FindName(graficoObtencionCaudal.Name & "_" & TOC(i))
                        chkInvisible.Visibility = Windows.Visibility.Visible
                        Canvas.SetTop(chkInvisible, 163)
                    End If
                Next
            Case "lnk_codFuenteDato"
                grafico = Me.graficoCaudalCodFuenteDato
                For i As Integer = 0 To CFD.Length - 1
                    If Not CFD(i) Is Nothing Then
                        chkInvisible = cnv.FindName(graficoCaudalCodFuenteDato.Name & "_" & CFD(i))
                        chkInvisible.Visibility = Windows.Visibility.Visible
                        Canvas.SetTop(chkInvisible, 163)
                    End If
                Next
        End Select

        graficoTop = Canvas.GetTop(grafico)
        graficoLeft = Canvas.GetLeft(grafico)
        graficoNombre = grafico.Name

        Canvas.SetTop(grafico, 185)
        Canvas.SetLeft(grafico, 0)
        Canvas.SetZIndex(grafico, 101)
        grafico.Width = 1000
        grafico.Height = 768


    End Sub

    Private Sub HyperlinkButton_Click_1(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

    End Sub

    Private Sub lnk_ocultar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        lnk_ocultar.Visibility = Windows.Visibility.Collapsed
        fondo.Visibility = Windows.Visibility.Collapsed

        Dim grafico As Chart = cnv.FindName(graficoNombre)

        Canvas.SetZIndex(grafico, 20)
        Canvas.SetLeft(grafico, graficoLeft)
        Canvas.SetTop(grafico, graficoTop)
        grafico.Width = 600
        grafico.Height = 400


        'Hacemos todos los checkbox visibles
        Dim chkVisible As New CheckBox
        For i As Integer = 0 To TOC.Length - 1
            If Not TOC(i) Is Nothing Then
                chkVisible = cnv.FindName(graficoObtencionCaudal.Name & "_" & TOC(i))
                chkVisible.Visibility = Windows.Visibility.Visible
                Canvas.SetTop(chkVisible, 212)
            End If
        Next
        For i As Integer = 0 To CFD.Length - 1
            If Not CFD(i) Is Nothing Then
                chkVisible = cnv.FindName(graficoCaudalCodFuenteDato.Name & "_" & CFD(i))
                chkVisible.Visibility = Windows.Visibility.Visible
                Canvas.SetTop(chkVisible, 672)
            End If
        Next
        For i As Integer = 0 To RC.Length - 1
            If Not RC(i) Is Nothing Then
                chkVisible = cnv.FindName(graficoRegimenDeCurva.Name & "_" & RC(i))
                chkVisible.Visibility = Windows.Visibility.Visible
                Canvas.SetTop(chkVisible, 1122)
            End If
        Next

    End Sub

    Private Sub crearCheckBox(ByVal grafico As Chart, ByVal i As Integer, ByVal valor As String, ByVal tipoVariable As String)
        OBJ = New CheckBox()
        'OBJ.Name = tipoVariable & i
        OBJ.Name = grafico.Name & "_" & valor
        cnv.Children.Add(OBJ)

        Select Case tipoVariable
            Case "TOC"
                Canvas.SetTop(OBJ, 212)
            Case "CFD"
                Canvas.SetTop(OBJ, 672)
            Case "RC"
                Canvas.SetTop(OBJ, 1122)
        End Select

        Canvas.SetLeft(OBJ, 15 + 40 * (i - 1))
        Canvas.SetZIndex(OBJ, 1000)

        OBJ.Content = valor
        OBJ.ClickMode = ClickMode.Press
        OBJ.IsChecked = True
        AddHandler OBJ.Click, AddressOf Me.CheckBox_Click
    End Sub

    Private Sub ListaDeVariables(ByVal sender As Object, ByVal e As ListaDeVariablesCompletedEventArgs)

        If e.Error Is Nothing Then
            If e.Result.Count > 0 Then
                Select Case e.Result(1).TipoVariable
                    Case "TOC"
                        For i As Integer = 0 To e.Result.Count - 1
                            ColorNuevoTOC.Add(e.Result(i).Variable, getColor(i))
                        Next
                        creaGraficoPie(graficoPieObtencionCaudal, e)
                        data.tiposDeVariablesAsync(xmlFile, "TOC")
                    Case "CFD"
                        For i As Integer = 0 To e.Result.Count - 1
                            ColorNuevoCFD.Add(e.Result(i).Variable, getColor(i))
                        Next
                        creaGraficoPie(graficoPieCaudalCodFuenteDato, e)
                        data.tiposDeVariablesAsync(xmlFile, "CFD")
                    Case "RC"
                        For i As Integer = 0 To e.Result.Count - 1
                            If Not e.Result(i).Variable Is System.DBNull.Value And Not e.Result(i).Variable Is Nothing Then
                                ColorNuevoRC.Add(e.Result(i).Variable, getColor(i))
                            End If
                        Next
                        creaGraficoPie(graficoPieRegimenDeCurva, e)
                        data.tiposDeVariablesAsync(xmlFile, "RC")
                End Select
            End If
        Else
            'MessageBox.Show("2: " & e.Error.Message)
        End If

    End Sub

    Private Function getColor(ByVal i As Integer) As SolidColorBrush

        'CREAMOS LOS COLORES DE MANERA ALEATORIA, AQUÍ HAY QUE OBTENER LOS COLORES LAS SERIES DEL OTRO GRÁFICO.
        Dim sc As New SolidColorBrush
        Dim r As Random = New Random(i)
        Dim red As Byte = (CType(255 * r.NextDouble(), Byte))
        Dim blue As Byte = (CType(255 * r.NextDouble(), Byte))
        Dim green As Byte = (CType(255 * r.NextDouble(), Byte))
        sc.Color = Color.FromArgb(255, red, green, blue)
        Return sc

    End Function


    Private Sub creaGraficoPie(ByVal grafico As Chart, ByVal e As ListaDeVariablesCompletedEventArgs)
        Dim p = New PieSeries()
        If grafico.Series.Count = 0 Then
            grafico.Series.Add(p)
        End If
        'Dim p As PieSeries = CType(grafico.Series(0), PieSeries)

        p.IndependentValueBinding = New System.Windows.Data.Binding("Variable")
        p.DependentValueBinding = New System.Windows.Data.Binding("Valor")
        p.ItemsSource = e.Result

        Dim colStyle As New System.Collections.ObjectModel.Collection(Of System.Windows.ResourceDictionary)
        For i = 0 To e.Result.Count - 1
            Dim st As New System.Windows.Style
            Dim Dictionary As ResourceDictionary = New ResourceDictionary()
            st.TargetType = GetType(DataPoint)
            st.Setters.Add(New Setter(DataPoint.BackgroundProperty, getColor(i)))
            Dictionary.Add("DataPointStyle", st)
            colStyle.Add(Dictionary)
        Next

        p.Palette = colStyle
    End Sub

    Private Sub DevuelveCauce(ByVal sender As Object, ByVal e As devuelveCauceCompletedEventArgs)
        Cauce.Content = e.Result
    End Sub

    Private Sub DevuelvePunto(ByVal sender As Object, ByVal e As devuelvePuntoCompletedEventArgs)
        Punto.Content = e.Result
    End Sub

End Class
