Imports System.Data
Imports OpenFlashChart
Imports System.Collections.Generic
Imports GuarderiaFluvial
Imports System.Collections

Partial Class Listados_GraficoLecturasData
    Inherits System.Web.UI.Page
    Dim graph As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart()
    Dim lbls As List(Of String)
    Dim dt As New System.Data.DataTable()
    Dim dtLectSel As New System.Data.DataTable
    'Dim v_max As Double
    'Dim v_min As Double
    Dim grafico As String = ""
    Dim conexion_insert As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion_insert)
    Dim lecturasTotales As Integer = 0
    'ipim 03/11/2008: Dim numValores As Integer = 401
    Dim numValores As Integer = 800
    Dim datos As List(Of Double)
    Dim FechaInicial, FechaFinal As DateTime
    Dim Intervalo As String
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
        End If

        Try
            'ipim 20/11/2008
            'FechaInicial = Convert.ToDateTime(Request.QueryString("filtroFechaIni").ToString)
            'FechaFinal = Convert.ToDateTime(Request.QueryString("filtroFechaFin").ToString).AddDays(1)

            dt = Cache("dst")

            Dim dr() As System.Data.DataRow
            dtLectSel = dt.Clone()

            dr = dt.DefaultView.Table.Select
            If Session("LecturasSeleccionadas") <> "" Then
                dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")
                dr = dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")
            End If
            lecturasTotales = dt.Rows.Count
            FechaInicial = Convert.ToDateTime(dt.Rows(0).Item("Fecha_Medida"))
            'FechaFinal = Convert.ToDateTime(dt.Rows(lecturasTotales - 1).Item("Fecha_Medida").ToString).AddDays(1)
            FechaFinal = Convert.ToDateTime(dt.Rows(lecturasTotales - 1).Item("Fecha_Medida").ToString)

            Dim fila As DataRow
            For Each fila In dr
                dtLectSel.Rows.Add(fila.ItemArray)
            Next
        Catch ex As Exception
            Exit Sub
        End Try

        grafico = Request.QueryString("grafico")

        Response.CacheControl = "no-cache"
        Dim i As Integer


        'CREACION DEL GRAFICO

        Dim j As Integer = 0
        lbls = New List(Of String)
        Dim colores As New ArrayList

        With colores
            .Add("#000000")
            .Add("#0000FF")
            .Add("#FF0000")
            .Add("#00FF00")
            .Add("#545412")
            .Add("#CCCCCC")
            .Add("#00CC00")
            .Add("#CC0000")
            .Add("#0000CC")
            .Add("#DDDDDD")
        End With

        If Request.QueryString("tipoGrafico") = 0 Then  'lineas
            j = Request.QueryString.Count - 1
            For i = 1 To j - 4
                If Request.QueryString(i) = True Then
                    graph.AddElement(CrearCurva(Request.QueryString.Keys(i), colores((i - 1) Mod 10)))
                End If
            Next

            If dtLectSel.Rows.Count <> 0 Then
                ValoresEjeX(lbls)
            End If

            Dim descripcionP As String = "", descripcionC As String = "", rama As String = ""
            'If conexion_insert.State = ConnectionState.Closed Then conexion_insert.Open()
            utiles.Comprobar_Conexion_BD(Page, conexion_insert)
            comando.CommandText = "Select descripcion from PVYCR_Arbol where tipo='P' and codigoPVYCR='" & Session("ClaveNodo") & "'"
            descripcionP = Replace(comando.ExecuteScalar, Session("ClaveNodo") & " ", "")
            comando.CommandText = "Select descripcion from PVYCR_Arbol where tipo='C' and codigoCauce='" & Left(Session("ClaveNodo"), InStrRev(Session("ClaveNodo"), "P") - 1) & "'"
            descripcionC = Replace(comando.ExecuteScalar, Left(Session("ClaveNodo"), InStrRev(Session("ClaveNodo"), "P") - 1) & " ", "")
            rama = Session("ClaveNodo") & "-" & Session("IdElementoMedida") & " (" & descripcionC & " - " & descripcionP & ")"

            Dim titulo, titulo2 As String

            titulo2 = Mid(rama, InStr(rama, "("))
            titulo = Left(rama, InStr(rama, "(") - 1) & _
                    Left(titulo2, InStr(titulo2, "-") - 1) & vbCrLf & Mid(titulo2, InStr(titulo2, "-"))

            graph.Title = New OpenFlashChart.Title(titulo & vbCrLf & "Informe de Caudales por " & grafico)
            graph.Title.Style = "font-size:14px; font-family:Verdana; color: #8B8B46;font-weight: bold; background-color:#D7DDBB"
            graph.X_Legend = New OpenFlashChart.Legend("Fecha")
            graph.Y_Legend = New OpenFlashChart.Legend("Caudal (m3/s)")
            graph.X_Legend.Style = "font-size:12px; font-family:Verdana; color: #000000"
            graph.Y_Legend.Style = "font-size:14px; font-family:Verdana; color: #000000"

            Dim maximo As Double = Convert.ToDouble(dtLectSel.Compute("max(Caudal_M3S)", ""))
            Dim minimo As Double = Convert.ToDouble(dtLectSel.Compute("min(Caudal_M3S)", ""))
        
            maximo = maximo * 1.1
            If maximo <> CInt(maximo) Then
                maximo = Math.Truncate(maximo) + 1
            End If

            If minimo >= 0 Then
                minimo = 0
            Else
                minimo = minimo * 1.1
                If minimo <> CInt(minimo) Then
                    minimo = Math.Truncate(minimo) - 1
                End If
            End If

            graph.Y_Axis.Max = maximo
            graph.Y_Axis.Min = minimo

            graph.Y_Axis.Steps = 1

            If maximo > 5 Or minimo < -5 Then
                graph.Y_Axis.Steps = Math.Abs(Math.Round(maximo / 5))
            End If

            graph.Y_Axis.Max = maximo
            graph.Y_Axis.Min = minimo

            If maximo > 5 Then
                graph.Y_Axis.Steps = Math.Round(maximo / 5)
            Else
                graph.Y_Axis.Steps = 1
            End If

            graph.X_Axis.Labels.Values = lbls
            graph.Tooltip = New OpenFlashChart.ToolTip("#key#<br>#val#")


            graph.X_Axis.Labels.Vertical = True

            Response.Clear()
            Response.Write(graph.ToString())
            Response.End()
            lbls = Nothing

            GC.Collect()

        Else        'pie
            CrearGraficoPie(colores)
        End If

        dtLectSel.DefaultView.RowFilter = ""

    End Sub
    Private Function CrearCurva(ByVal tipo As String, ByVal color As String) As LineDot
        'CREACION DEL GRAFICO
        Dim i As Integer
        Dim Var As OpenFlashChart.LineDot = New OpenFlashChart.LineDot()
        'Dim datos As List(Of Double) = New List(Of Double)
        'ncm 12/11/2008 datos = New List(Of Double)
        'ipim 03/11/2008: Dim datos((numValores * 2) + 2) As Double
        Dim datos(numValores) As Double

        Dim paso As Double

        Dim j As Int16 = 0

        dtLectSel.DefaultView.RowFilter = grafico & " = '" & tipo & "'"
        'Dim numFilas As Integer = dtLectSel.DefaultView.Count

        Dim filas As Integer = dtLectSel.DefaultView.Count
        Dim v_max As Double
        Dim v_min As Double


        If filas = 0 Then v_max = 1
        'ncm 12/11/2008 Dim FechaInicial As DateTime = dt.Rows(0).Item("Fecha_Medida")
        'ncm 12/11/2008 Dim FechaFinal As DateTime = dt.Rows(lecturasTotales - 1).Item("Fecha_Medida")
        'ipim 20/11/2008 
        'FechaInicial = dt.Rows(0).Item("Fecha_Medida")
        'FechaFinal = dt.Rows(lecturasTotales - 1).Item("Fecha_Medida")

        CalculaTicks()

        Dim PrimerValorX As Double = FechaInicial.Ticks
        Dim UltimoValorTodasX As Double = FechaFinal.Ticks
        'ncm 11/11/2008 paso = (UltimoValorTodasX - PrimerValorX) / numValores
        'ipim 03/11/2008: paso = (UltimoValorTodasX - PrimerValorX) / (2 * numValores)
        paso = (UltimoValorTodasX - PrimerValorX) / numValores

        Dim posicion As Integer

        If paso = 0 Then
            paso = 0.01
        End If

        '21562880 Nº de ticks en un segundo

        'ipim 03/11/2008: For i = 0 To (numValores * 2) + 2
        For i = 0 To numValores
            'ncm datos.Add(Double.NegativeInfinity)
            datos(i) = Double.NegativeInfinity
        Next

        Dim FechaActual As DateTime
        Dim v As Double = 0
        Dim posicion_ant As Integer = 0
        'ipim 03/11/2008: Dim numvaloresmio As Integer = (numValores * 2) + 2
        Dim numValoresMio As Integer = numValores


        'If filas > numvaloresmio Then
        v = Math.Round(utiles.nullACero(dtLectSel.DefaultView(0).Item("Caudal_M3S")), 2)
        v_max = v
        v_min = v

        For i = 0 To filas - 1
            'calculo la posicion en el que tengo que insertarlo
            FechaActual = dtLectSel.DefaultView(i).Item("Fecha_Medida")
            posicion = Math.Round((FechaActual.Ticks - PrimerValorX) / paso)
            v = Math.Round(utiles.nullACero(dtLectSel.DefaultView(i).Item("Caudal_M3S")), 2)

            If posicion <> posicion_ant Then
                'como es la primera vez que entramos en esa casilla, inicializamos maximo y mínimo al valor del caudal
                v_max = v
                v_min = v
                datos(posicion) = v_min
                If posicion >= (numValoresMio) Then
                    datos(posicion) = v_max
                Else
                    datos(posicion + 1) = v_max
                End If


            Else
                'calculamos que valor es mayor o menor
                If v > v_max Then
                    v_max = v
                End If
                If v < v_min Then
                    v_min = v
                End If
                datos(posicion) = v_min
                If posicion >= (numValoresMio) Then
                    datos(posicion) = v_max
                Else
                    datos(posicion + 1) = v_max
                End If

            End If
            posicion_ant = posicion
            'ncm 12/11/2008 
            ''datos.Add(v)
            'If i = 0 Then
            '    datos(2 * posicion_array) = v
            '    datos(2 * posicion_array + 1) = v
            '    posicion_ant = posicion
            '    v_menor = v
            '    v_mayor = v
            'Else
            '    If posicion <> posicion_ant Then
            '        'datos.Add(v)

            '        datos(2 * posicion_array) = v
            '        datos(2 * posicion_array + 1) = v
            '        posicion_ant = posicion
            '        v_menor = v
            '        v_mayor = v
            '        posicion_array = posicion_array + 1
            '    Else
            '        If v > v_mayor Then
            '            datos(2 * posicion_array + 1) = v
            '            v_mayor = v
            '        End If
            '        If v < v_menor Then
            '            datos(2 * posicion_array) = v
            '            v_menor = v
            '        End If
            '    End If
            'End If

            'If v > maximo Then
            '    maximo = v
            'End If
            'If v < minimo Then
            '    minimo = v
            'End If

        Next

        With Var
            .Values = datos
            .HaloSize = 0
            .Width = 1
            .DotSize = 2
            .Colour = color
            .Text = tipo
            .Fontsize = 10
        End With

        datos = Nothing
        Return Var
    End Function


    Private Sub CrearGraficoPie(ByVal colores As ArrayList)
        Dim pie As OpenFlashChart.Pie = New OpenFlashChart.Pie()
        graph.Title = New OpenFlashChart.Title("% de lecturas por " & grafico)
        graph.Title.Style = "font-size: 10px"
        Dim campo As String = Request.QueryString("grafico")
        dtLectSel.DefaultView.RowFilter = ""
        dtLectSel.DefaultView.Sort = campo

        Dim i As Integer = 0, j As Integer = 0
        Dim strTipo As String = ""
        Dim datos As List(Of Double) = New List(Of Double)
        Dim total As Integer = dtLectSel.Rows.Count

        For i = 0 To dtLectSel.DefaultView.Count - 1
            If strTipo <> utiles.nullABlanco(dtLectSel.DefaultView(i).Item(campo)) Then
                strTipo = dtLectSel.DefaultView(i).Item(campo)
                lbls.Add(strTipo)
                dtLectSel.DefaultView.RowFilter = campo & "='" & strTipo & "'"
                pie.Values.Add(j)
                pie.Values(j) = New OpenFlashChart.PieValue(dtLectSel.DefaultView.Count, strTipo)
                dtLectSel.DefaultView.RowFilter = ""
                j = j + 1
            End If
        Next

        pie.Colours = New String() {colores(0), colores(1), colores(2), colores(3), colores(4), colores(5), colores(6), colores(7), colores(8), colores(9)}
        graph.AddElement(pie)

        Response.Clear()
        Response.CacheControl = "no-cache"
        Response.Write(graph.ToString())
        Response.End()
    End Sub

    Private Sub ValoresEjeX(ByRef lbls As List(Of String))
        Dim UltimoValorX As Double = FechaFinal.Ticks
        Dim PrimerValorX As Double = FechaInicial.Ticks

        Dim paso As Double
        Dim i As Integer

        'ipim 03/11/2008: paso = (UltimoValorX - PrimerValorX) / (2 * numValores)
        paso = (UltimoValorX - PrimerValorX) / numValores
        If paso = 0 Then
            paso = 0.01
        End If

        Dim fecha As DateTime
        'Dim valorDiv As Double
        'If Int(Math.Round((numValores - 1) / 2)) = Int((numValores - 1) / 2) Then
        '    valorDiv = Math.Round((numValores - 1) / 2)
        'Else
        '    valorDiv = Math.Round((numValores - 1) / 2) + 1
        'End If


        Dim FechaAnterior, FechaActual As DateTime
        'FechaAnterior = FechaInicial

        'ncm 11/11/2008 For i = 0 To numValores * 2
        'ipim 03/11/2008: For i = 0 To (numValores * 2) + 2
        For i = 0 To numValores
            'If i Mod (valorDiv) = 0 Then
            fecha = New DateTime(PrimerValorX + paso * i)
            FechaActual = CalculaFechas(Intervalo, fecha)

            If FechaAnterior <> FechaActual Then
                lbls.Add(EscribeEtiquetas(Intervalo, FechaActual))
                FechaAnterior = FechaActual
            Else
                lbls.Add("")
            End If
        Next

        ''Para ver si ponemos la hora exacta en las labels
        'Dim lblActual As String = lbls(0)
        'Dim FechaSinFormatear, Fecha0SinFormatear As DateTime
        'Fecha0SinFormatear = lbls(0)

        'For i = 1 To lbls.Count - 1
        '    If lbls(i) <> "" Then
        '        FechaSinFormatear = lbls(i)
        '        If (lblActual = String.Format("{0:dd/MM/yyyy}", FechaSinFormatear)) Then
        '            Exit For
        '        End If
        '        lbls(0) = String.Format("{0:dd/MM/yyyy}", Fecha0SinFormatear)
        '        lbls(i) = String.Format("{0:dd/MM/yyyy}", FechaSinFormatear)
        '        lblActual = String.Format("{0:dd/MM/yyyy}", FechaSinFormatear)
        '    End If
        'Next

    End Sub

    Private Sub CalculaTicks()
        Dim NumTicksDivision As Double = (FechaFinal.Ticks - FechaInicial.Ticks) / 20     '20: Es el número de divisiones que queremos que hayan

        'Definiciones
        Dim UltimoDiaHora As DateTime = "31/12/2008 04:59:59"
        Dim PrimerDiaHora As DateTime = "31/12/2008 00:00:00"
        Dim TicksHora As Double = UltimoDiaHora.Ticks - PrimerDiaHora.Ticks
        Dim UltimoDiaDia As DateTime = "31/12/2008 23:59:59"
        Dim PrimerDiaDia As DateTime = "31/12/2008 00:00:00"
        Dim TicksDia As Double = UltimoDiaDia.Ticks - PrimerDiaDia.Ticks
        Dim UltimoDiaSemanal As DateTime = "31/12/2008 23:59:59"
        Dim PrimerDiaSemanal As DateTime = "25/12/2008 00:00:00"
        Dim TicksSemanal As Double = UltimoDiaSemanal.Ticks - PrimerDiaSemanal.Ticks
        Dim UltimoDiaQuincenal As DateTime = "31/12/2008 23:59:59"
        Dim PrimerDiaQuincenal As DateTime = "15/12/2008 00:00:00"
        Dim TicksQuincenal As Double = UltimoDiaQuincenal.Ticks - PrimerDiaQuincenal.Ticks
        Dim UltimoDiaMes As DateTime = "31/12/2008 23:59:59"
        Dim PrimerDiaMes As DateTime = "01/12/2008 00:00:00"
        Dim TicksMes As Double = UltimoDiaMes.Ticks - PrimerDiaMes.Ticks
        Dim UltimoDiaAnyo As DateTime = "31/12/2008 23:59:59"
        Dim PrimerDiaAnyo As DateTime = "01/01/2008 00:00:00"
        Dim TicksAnyo As Double = UltimoDiaAnyo.Ticks - PrimerDiaAnyo.Ticks

        Dim DiferenciaTicksHora As Double = TicksHora - NumTicksDivision
        Dim DiferenciaTicksDia As Double = TicksDia - NumTicksDivision
        Dim DiferenciaTicksSemanal As Double = TicksSemanal - NumTicksDivision
        Dim DiferenciaTicksQuincenal As Double = TicksQuincenal - NumTicksDivision
        Dim DiferenciaTicksMes As Double = TicksMes - NumTicksDivision
        Dim DiferenciaTicksAnyo As Double = TicksAnyo - NumTicksDivision

        Dim IntervaloTicks As Double = 0

        If DiferenciaTicksHora > 0 And DiferenciaTicksDia > 0 Then
            IntervaloTicks = Math.Min(DiferenciaTicksHora, DiferenciaTicksDia)
        ElseIf DiferenciaTicksHora > 0 And DiferenciaTicksDia < 0 Then
            IntervaloTicks = DiferenciaTicksHora
        ElseIf DiferenciaTicksHora < 0 And DiferenciaTicksDia > 0 Then
            IntervaloTicks = DiferenciaTicksDia
        Else
            IntervaloTicks = 0
        End If
        If DiferenciaTicksSemanal > 0 Then
            If IntervaloTicks <> 0 Then
                IntervaloTicks = Math.Min(IntervaloTicks, DiferenciaTicksSemanal)
            Else
                IntervaloTicks = DiferenciaTicksSemanal
            End If
        End If
        If DiferenciaTicksQuincenal > 0 Then
            If IntervaloTicks <> 0 Then
                IntervaloTicks = Math.Min(IntervaloTicks, DiferenciaTicksQuincenal)
            Else
                IntervaloTicks = DiferenciaTicksQuincenal
            End If
        End If
        If DiferenciaTicksMes > 0 Then
            If IntervaloTicks <> 0 Then
                IntervaloTicks = Math.Min(IntervaloTicks, DiferenciaTicksMes)
            Else
                IntervaloTicks = DiferenciaTicksMes
            End If
        End If
        If DiferenciaTicksAnyo > 0 Then
            If IntervaloTicks <> 0 Then
                IntervaloTicks = Math.Min(IntervaloTicks, DiferenciaTicksAnyo)
            Else
                IntervaloTicks = DiferenciaTicksAnyo
            End If
        End If

        Select Case IntervaloTicks
            Case DiferenciaTicksHora
                Intervalo = "h"
                FechaInicial = Day(FechaInicial) & "/" & Month(FechaInicial) & "/" & Year(FechaInicial) & " 00:00:00"
                While Not DateDiff(DateInterval.Hour, FechaInicial, FechaFinal) Mod 5 = 0
                    FechaFinal = DateAdd(DateInterval.Hour, 1, FechaFinal)
                End While
                FechaFinal = Day(FechaFinal) & "/" & Month(FechaFinal) & "/" & Year(FechaFinal) & " " & Hour(FechaFinal) & ":00:00"
            Case DiferenciaTicksDia
                Intervalo = "d"
                FechaInicial = Day(FechaInicial) & "/" & Month(FechaInicial) & "/" & Year(FechaInicial) & " 00:00:00"
                FechaFinal = Day(FechaFinal) & "/" & Month(FechaFinal) & "/" & Year(FechaFinal) & " 23:59:59"
            Case DiferenciaTicksSemanal
                Intervalo = "s"
                While Not FechaInicial.DayOfWeek = DayOfWeek.Monday
                    FechaInicial = DateAdd(DateInterval.Day, -1, FechaInicial)
                End While
                Do While Not FechaFinal.DayOfWeek = DayOfWeek.Saturday
                    FechaFinal = DateAdd(DateInterval.Day, 1, FechaFinal)
                Loop
                FechaInicial = Day(FechaInicial) & "/" & Month(FechaInicial) & "/" & Year(FechaInicial) & " 00:00:00"
                FechaFinal = Day(FechaFinal) & "/" & Month(FechaFinal) & "/" & Year(FechaFinal) & " 23:59:59"
            Case DiferenciaTicksQuincenal
                Intervalo = "q"
                If Day(FechaInicial) <= 15 Then
                    FechaInicial = "01/" & Month(FechaInicial) & "/" & Year(FechaInicial) & " 00:00:00"
                Else
                    FechaInicial = "15/" & Month(FechaInicial) & "/" & Year(FechaInicial) & " 00:00:00"
                End If
                If Day(FechaFinal) <= 15 Then
                    FechaFinal = "15/" & Month(FechaFinal) & "/" & Year(FechaFinal) & " 23:59:59"
                Else
                    FechaFinal = "01/" & Month(DateAdd(DateInterval.Month, 1, FechaFinal)) & "/" & Year(DateAdd(DateInterval.Month, 1, FechaFinal))
                    FechaFinal = DateAdd(DateInterval.Day, -1, FechaFinal)
                    FechaFinal = Day(FechaFinal) & "/" & Month(FechaFinal) & "/" & Year(FechaFinal) & " 23:59:59"
                End If
            Case DiferenciaTicksMes
                Intervalo = "m"
                FechaInicial = "01/" & Month(FechaInicial) & "/" & Year(FechaInicial) & " 00:00:00"
                FechaFinal = "01/" & Month(DateAdd(DateInterval.Month, 1, FechaFinal)) & "/" & Year(DateAdd(DateInterval.Month, 1, FechaFinal))
                FechaFinal = DateAdd(DateInterval.Day, -1, FechaFinal)
                FechaFinal = Day(FechaFinal) & "/" & Month(FechaFinal) & "/" & Year(FechaFinal) & " 23:59:59"
            Case DiferenciaTicksAnyo
                Intervalo = "a"
                FechaInicial = "01/01/" & Year(FechaInicial) & " 00:00:00"
                FechaFinal = "31/12/" & Year(FechaFinal) & " 23:59:59"
        End Select

    End Sub

    Private Function CalculaFechas(ByVal Intervalo As String, ByVal Fecha As DateTime) As DateTime
        Dim FechaActual As DateTime
        Select Case Intervalo
            Case "h"
                While Not DateDiff(DateInterval.Hour, FechaInicial, Fecha) Mod 5 = 0
                    Fecha = DateAdd(DateInterval.Hour, -1, Fecha)
                End While
                FechaActual = Day(Fecha) & "/" & Month(Fecha) & "/" & Year(Fecha) & " " & Hour(Fecha) & ":00:00"
            Case "d"
                FechaActual = Day(Fecha) & "/" & Month(Fecha) & "/" & Year(Fecha) & " 00:00:00"
            Case "s"
                Do While Not Fecha.DayOfWeek = DayOfWeek.Monday
                    Fecha = DateAdd(DateInterval.Day, -1, Fecha)
                Loop
                FechaActual = Day(Fecha) & "/" & Month(Fecha) & "/" & Year(Fecha) & " 00:00:00"
            Case "q"
                If Day(Fecha) <= 15 Then
                    FechaActual = "01/" & Month(Fecha) & "/" & Year(Fecha) & " 00:00:00"
                Else
                    FechaActual = "16/" & Month(Fecha) & "/" & Year(Fecha) & " 00:00:00"
                End If
            Case "m"
                FechaActual = "01/" & Month(Fecha) & "/" & Year(Fecha) & " 00:00:00"
            Case "a"
                FechaActual = "01/01/" & Year(Fecha) & " 00:00:00"
        End Select

        Return FechaActual
    End Function

    Private Function EscribeEtiquetas(ByVal Intervalo As String, ByVal FechaAnterior As DateTime) As String
        Dim Etiqueta As String = ""
        Select Case Intervalo
            Case "h"
                Etiqueta = FechaAnterior
            Case "d"
                Etiqueta = String.Format("{0:dd/MM/yyyy}", FechaAnterior)
            Case "s"
                Etiqueta = String.Format("{0:dd/MM/yyyy}", FechaAnterior)
            Case "q"
                Etiqueta = String.Format("{0:dd/MM/yyyy}", FechaAnterior)
            Case "m"
                Etiqueta = UCase(Left(MonthName(Month(FechaAnterior)), 1)) & Right(MonthName(Month(FechaAnterior)), Len(MonthName(Month(FechaAnterior))) - 1) & " " & Year(FechaAnterior)
            Case "a"
                Etiqueta = Year(FechaAnterior)
        End Select
        Return Etiqueta
    End Function
End Class
