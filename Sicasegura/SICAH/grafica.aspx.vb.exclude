Imports dotnetCHARTING
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports System.Data
Imports System.Data.SqlClient


Partial Class SICAH_grafica
    Inherits System.Web.UI.Page

    Dim fechaInicial As DateTime
    Dim fechaFin As DateTime
    Dim punto As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        imgFechaInicio.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaInicio.ClientID & "'),'dd/mm/yyyy');")
        imgFechaFin.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaFin.ClientID & "'),'dd/mm/yyyy');")
        btnFiltrar.Attributes.Add("onclick", "filtrar(document.getElementById('" & pto.ClientID & "'),document.getElementById('" & txtFechaInicio.ClientID & "'), document.getElementById('" & txtFechaFin.ClientID & "'));return false")

        'If Not IsPostBack Then
        'fechaInicial = Date.Now.AddDays(-15)
        'fechaFin = Date.Now
        If Page.Request.Params("finicial") <> "" Then
            fechaInicial = Page.Request.Params("finicial")
        Else
            If IsDate(Me.txtFechaInicio.Text) Then
                fechaInicial = Me.txtFechaInicio.Text
            Else
                'EGB Corrección solicitada en fecha 25/07/2008. Ahora el filtro por defecto serán las lecturas del dia
                'fechaInicial = Date.Now.AddDays(-15)
                fechaInicial = Date.Now.ToShortDateString & " 00:00:01"
            End If
        End If
        If Page.Request.Params("ffinal") <> "" Then
            fechaFin = Page.Request.Params("ffinal")
        Else
            If IsDate(Me.txtFechaFin.Text) Then
                fechaFin = Me.txtFechaFin.Text
            Else
                fechaFin = Date.Now.ToShortDateString & " 23:59:59"
            End If

        End If

        'End If


        If Page.Request.Params("nodoclave") = "" Then
            punto = "VA002P30"
        Else
            punto = Request.QueryString("nodoclave").ToString.Split(";")(0)

        End If
        generaGrafico()

        pto.Text = punto
        txtFechaInicio.Text = fechaInicial
        txtFechaFin.Text = fechaFin

    End Sub

    Sub generaGrafico()
        With Chart
            .Type = ChartType.Scatter
            .Width = Unit.Parse(900)
            .Height = Unit.Parse(500)
            .TempDirectory = "tmp"

            'Set clean up priod(only delete the auto generated files, starts with dnc-...)
            .CleanupPeriod = 30
            .Use3D = False
            .Mentor = False
            .Debug = False
            .DefaultAxis.Line.Width = 2
            .ShadingEffectMode = ShadingEffectMode.Two
            .Background.Color = Color.White ' Color.Transparent
            .DefaultBox.CornerSize = 4
            .ChartAreaLayout.Mode = ChartAreaLayoutMode.Horizontal
            .SeriesCollection.Add(getData())

            .ChartArea.XAxis.FormatString = "dd/MM/yyyy HH:MM:ss"

        End With
        With Chart.DefaultSeries
            .Type = SeriesType.Line
            '.DefaultElement.Transparency = 20
            .DefaultElement.Marker.Type = ElementMarkerType.None ' Circle
            .DefaultElement.Marker.Size = 2
            .Line.Width = 1
            .GaugeBorderBox.Padding = 5
        End With

        With Chart.ChartArea
            .CornerBottomRight = BoxCorner.Round
            .CornerBottomLeft = BoxCorner.Round
        End With

        ' TitleBox Customization
        With Chart.TitleBox
            .Label.Shadow.Color = Color.FromArgb(105, 0, 0, 0)
            .Label.Shadow.Depth = 2
            .Position = TitleBoxPosition.FullWithLegend
            .CornerTopLeft = BoxCorner.Round
            .CornerTopRight = BoxCorner.Round
            .Background.ShadingEffectMode = ShadingEffectMode.Five
            .Background.Color = Color.FromArgb(100, 225, 165, 50)
            .CornerSize = 20
        End With

        ' Legend Box Customization
        With Chart.LegendBox
            .Template = "%icon %name"
            .HeaderLabel = New dotnetCHARTING.Label("Mediciones", New Font("Arial", 9, FontStyle.Bold), Color.White)
            .HeaderLabel.Alignment = StringAlignment.Center
            .HeaderBackground.ShadingEffectMode = ShadingEffectMode.Two
            .HeaderBackground.Color = Color.FromArgb(0, 156, 255)
            .HeaderLabel.Shadow.Color = Color.Gray
            .HeaderLabel.Shadow.Depth = 1
            .Background.Color = Color.White
            .CornerTopLeft = BoxCorner.Square ' .Round
            .CornerTopRight = BoxCorner.Cut ' Round
        End With

        Chart.ChartAreaSpacing = 15
        With Chart.XAxis
            .MinimumInterval = 1
            .SmartMinorTicks = True
            .TimeScaleLabels.RangeMode = TimeScaleLabelRangeMode.Dynamic
            .TimeScaleLabels.Mode = TimeScaleLabelMode.Hidden
            .TimeScaleLabels.DayTick.Label.Text = ""
            .DefaultTick.Label.Font = New Font("Arial", 8, FontStyle.Bold)
            .Orientation = dotnetCHARTING.Orientation.Left
        End With
        Chart.DefaultElement.ShowValue = False

        Chart.GetChartBitmap()

    End Sub

    Function getData() As SeriesCollection
        Dim SC As New SeriesCollection()

        Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("dsnsegura_migracion"))

        Dim myDS As New System.Data.DataSet()
        Dim myDA As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim myDt As New System.Data.DataTable()

        Dim i As Integer
        '******
        Dim lSql As String = "SELECT dbo.PVYCR_DatosAcequias.CodigoPVYCR, dbo.PVYCR_DatosAcequias.Cod_Fuente_Dato, " & _
            " dbo.PVYCR_DatosAcequias.Fecha_Medida, dbo.PVYCR_DatosAcequias.Caudal_M3S, dbo.FUENTES_DE_DATOS.FUENTE_DATOS " & _
            " FROM dbo.FUENTES_DE_DATOS INNER JOIN dbo.PVYCR_DatosAcequias ON dbo.FUENTES_DE_DATOS.COD_FUENTE_DATO COLLATE SQL_Latin1_General_CP1_CI_AS = dbo.PVYCR_DatosAcequias.Cod_Fuente_Dato" & _
            " WHERE (PVYCR_DatosAcequias.CodigoPVYCR = '" & punto & "')" & _
            "   AND (PVYCR_DatosAcequias.Fecha_Medida > '" & fechaInicial.ToString & "') " & _
            "   AND (PVYCR_DatosAcequias.Fecha_Medida < '" & fechaFin.ToString & "')" & _
            " ORDER BY PVYCR_DatosAcequias.Cod_Fuente_Dato, PVYCR_DatosAcequias.Fecha_Medida"

        'lSql = "SELECT TOP 100 PERCENT dbo.PVYCR_DatosAcequias.CodigoPVYCR, dbo.PVYCR_DatosAcequias.Cod_Fuente_Dato, " & _
        '            " dbo.PVYCR_DatosAcequias.Fecha_Medida, dbo.PVYCR_DatosAcequias.Caudal_M3S, dbo.FUENTES_DE_DATOS.FUENTE_DATOS " & _
        '            " FROM dbo.FUENTES_DE_DATOS INNER JOIN dbo.PVYCR_DatosAcequias ON dbo.FUENTES_DE_DATOS.COD_FUENTE_DATO COLLATE SQL_Latin1_General_CP1_CI_AS = dbo.PVYCR_DatosAcequias.Cod_Fuente_Dato" & _
        '            " WHERE (PVYCR_DatosAcequias.CodigoPVYCR = '" & punto & "')" & _
        '            "   AND (PVYCR_DatosAcequias.Fecha_Medida between '" & fechaInicial.ToString & "' and " & _
        '            "    '" & fechaFin.ToString & "')" & _
        '            " ORDER BY PVYCR_DatosAcequias.Cod_Fuente_Dato, PVYCR_DatosAcequias.Fecha_Medida"


        myDA.SelectCommand.CommandText = lSql
        myDA.Fill(myDS, "grafico")
        myDt = myDS.Tables("grafico")

        Chart.TitleBox.Label = New dotnetCHARTING.Label("Punto de control " + punto, New Font("Arial", 13, FontStyle.Bold), Color.White)
        'puntos = myDt.Rows ' .Select("CodigoPVYCR = '" & punto & "'")
        Dim codFuenteDeDatos As Integer = -1
        Dim fuenteDeDatos As String = ""

        Dim series As New Series()
        Dim elemento As New Element()

        i = 0


        For Each row As DataRow In myDt.Rows
            If codFuenteDeDatos <> row("Cod_Fuente_Dato") Then
                If series.Elements.Count > 0 Then
                    series.Name = fuenteDeDatos
                    series.LimitMode = dotnetCHARTING.LimitMode.Top
                    series.ShowOther = False
                    SC.Add(series)
                End If
                series = New Series()


                codFuenteDeDatos = row("Cod_Fuente_Dato")
                fuenteDeDatos = row("FUENTE_DATOS")
            End If

            If Not row("caudal_m3s") Is System.DBNull.Value Then
                elemento = New Element()
                elemento.Name = "Caudal " & i
                elemento.XDateTime = row("fecha_medida")
                elemento.YValue = row("caudal_m3s")
                series.Elements.Add(elemento)
                i += 1
            End If
        Next

        'Añadimos la última serie.
        If series.Elements.Count > 0 Then
            series.Name = fuenteDeDatos
            series.LimitMode = dotnetCHARTING.LimitMode.Top
            series.ShowOther = False
            SC.Add(series)

        End If
        'SC(0).DefaultElement.Color = Color.DarkBlue
        'SC(1).DefaultElement.Color = Color.DarkOliveGreen
        'SC(2).DefaultElement.Color = Color.DarkRed
        'SC(3).DefaultElement.Color = Color.DarkGray

        myDS.Dispose()
        myDt.Dispose()

        myDS = Nothing
        myDt = Nothing

        'EGB Nota para Marcos Cambiar esto es un remiendo provisional 30 hora antes de irme de vacas.

        Dim indice As Integer
        For indice = 0 To SC.Count - 1
               SC(indice).DefaultElement.Color = ColoresOscuros(indice)
        Next
        Return SC
    End Function
    Function ColoresOscuros(ByVal indice As Integer) As System.Drawing.Color
        Dim micoloroscuro As System.Drawing.Color
        Select Case indice
            Case 0
                micoloroscuro = Color.DarkGreen
            Case 1
                micoloroscuro = Color.DarkBlue
            Case 2
                micoloroscuro = Color.DarkRed
            Case 3
                micoloroscuro = Color.DarkOrange
            Case 4
                micoloroscuro = Color.DeepPink
            Case 5
                micoloroscuro = Color.DarkTurquoise
            Case 6
                micoloroscuro = Color.DarkGray
            Case 7
                micoloroscuro = Color.DarkMagenta
            Case 8
                micoloroscuro = Color.DarkCyan
            Case Else
                micoloroscuro = Color.DarkKhaki
        End Select
        ColoresOscuros = micoloroscuro
    End Function
End Class
