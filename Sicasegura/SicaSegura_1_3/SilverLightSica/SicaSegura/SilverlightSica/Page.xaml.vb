Partial Public Class Page
    Inherits UserControl


    Public Sub New()
        InitializeComponent()
        If App.Current.Resources.Contains("tipoGrafico") Then
            Dim grafico As String = App.Current.Resources("tipoGrafico").ToString()
            Dim obj As Object
            Select Case grafico
                Case "telegestion"
                    obj = New telegestion()
                    Me.LayoutRoot.Children.Add(obj)

                Case "graficoDeLecturas"
                    obj = New graficoDeLecturas
                    Me.LayoutRoot.Children.Add(obj)
                Case "graficoDeConsumos"
                    obj = New graficoDeConsumos
                    Me.LayoutRoot.Children.Add(obj)
            End Select
        Else
            Dim obj As Object = New telegestion()
            Me.LayoutRoot.Children.Add(obj)
        End If






    End Sub




End Class