#ExternalChecksum("D:\Proyectos\Sicasegura\SilverLightSica\SicaSegura\SilverlightSica\telegestion.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","13B3CCFCBF5F7556FE8C4C2FF63C9275")
'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión del motor en tiempo de ejecución:2.0.50727.3634
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Automation.Peers
Imports System.Windows.Automation.Provider
Imports System.Windows.Controls
Imports System.Windows.Controls.DataVisualization.Charting
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Interop
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Resources
Imports System.Windows.Shapes
Imports System.Windows.Threading



<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class telegestion
    Inherits System.Windows.Controls.UserControl
    
    Friend WithEvents LayoutRoot As System.Windows.Controls.Grid
    
    Friend WithEvents dtgNivus As System.Windows.Controls.DataGrid
    
    Friend WithEvents fechaInicio As System.Windows.Controls.DatePicker
    
    Friend WithEvents fechaFin As System.Windows.Controls.DatePicker
    
    Friend WithEvents filtrarFechas As System.Windows.Controls.Button
    
    Friend WithEvents grafico As System.Windows.Controls.DataVisualization.Charting.Chart
    
    Friend WithEvents lineaCaudal As System.Windows.Controls.DataVisualization.Charting.LineSeries
    
    Friend WithEvents lineaAltura As System.Windows.Controls.DataVisualization.Charting.LineSeries
    
    Friend WithEvents lineaVelocidad As System.Windows.Controls.DataVisualization.Charting.LineSeries
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Sub InitializeComponent()
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        System.Windows.Application.LoadComponent(Me, New System.Uri("/SilverlightSica;component/telegestion.xaml", System.UriKind.Relative))
        Me.LayoutRoot = CType(Me.FindName("LayoutRoot"),System.Windows.Controls.Grid)
        Me.dtgNivus = CType(Me.FindName("dtgNivus"),System.Windows.Controls.DataGrid)
        Me.fechaInicio = CType(Me.FindName("fechaInicio"),System.Windows.Controls.DatePicker)
        Me.fechaFin = CType(Me.FindName("fechaFin"),System.Windows.Controls.DatePicker)
        Me.filtrarFechas = CType(Me.FindName("filtrarFechas"),System.Windows.Controls.Button)
        Me.grafico = CType(Me.FindName("grafico"),System.Windows.Controls.DataVisualization.Charting.Chart)
        Me.lineaCaudal = CType(Me.FindName("lineaCaudal"),System.Windows.Controls.DataVisualization.Charting.LineSeries)
        Me.lineaAltura = CType(Me.FindName("lineaAltura"),System.Windows.Controls.DataVisualization.Charting.LineSeries)
        Me.lineaVelocidad = CType(Me.FindName("lineaVelocidad"),System.Windows.Controls.DataVisualization.Charting.LineSeries)
    End Sub
End Class
