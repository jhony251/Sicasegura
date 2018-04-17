Imports System.Data
Imports OpenFlashChart
Imports System.Collections.Generic
Imports GuarderiaFluvial

Partial Class Listados_GraficoCaudalesFiltradosData
    Inherits System.Web.UI.Page

    Dim dst As DataSet = New System.Data.DataSet()
    Dim lbls As List(Of String)
    Dim datos As List(Of Double)
    Dim min, max As Double
    Dim lecturasTotales As Integer = 0
    Dim graph As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart()
    Dim numValores As Integer = 401

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
        End If

        Try
            dst = Session("dst")
        Catch ex As Exception
            Exit Sub
        End Try
        If dst Is Nothing Then
            JavaScript.Alert(Page, "Por favor, vuelve a cargar los datos de las lecturas que ya no están en memoria")
            Response.End()
        End If
        lecturasTotales = dst.Tables("listado").Rows.Count
        Response.CacheControl = "no-cache"

        Dim i As Integer

        'CREACION DEL GRAFICO

        Dim j As Integer = 0
        lbls = New List(Of String)
        Dim colores As New ArrayList
        Dim numFilas As Integer = dst.Tables("listado").Rows.Count

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


        Dim leyenda_y As String = ""
        Select Case Request.QueryString("grafico")
            Case 1
                If InStrRev(dst.Tables("listado").Rows(0).Item("Var1Titulo"), "(") > 0 Then
                    leyenda_y = Mid(dst.Tables("listado").Rows(0).Item("Var1Titulo"), InStrRev(dst.Tables("listado").Rows(0).Item("Var1Titulo"), "(") + 1, dst.Tables("listado").Rows(0).Item("Var1Titulo").ToString.Length - InStrRev(dst.Tables("listado").Rows(0).Item("Var1Titulo"), "(") - 1)
                Else
                    leyenda_y = Replace(dst.Tables("listado").Rows(0).Item("Var1Titulo"), "Total ", "")
                End If
            Case 2
                leyenda_y = Mid(dst.Tables("listado").Rows(0).Item("Var4Titulo"), InStrRev(dst.Tables("listado").Rows(0).Item("Var4Titulo"), "(") + 1, dst.Tables("listado").Rows(0).Item("Var4Titulo").ToString.Length - InStrRev(dst.Tables("listado").Rows(0).Item("Var4Titulo"), "(") - 1)
            Case 3
                If InStrRev(dst.Tables("listado").Rows(0).Item("Var1Titulo"), "(") > 0 Then
                    leyenda_y = Mid(dst.Tables("listado").Rows(0).Item("Var1Titulo"), InStrRev(dst.Tables("listado").Rows(0).Item("Var1Titulo"), "(") + 1, dst.Tables("listado").Rows(0).Item("Var1Titulo").ToString.Length - InStrRev(dst.Tables("listado").Rows(0).Item("Var1Titulo"), "(") - 1)
                Else
                    leyenda_y = Replace(dst.Tables("listado").Rows(0).Item("Var1Titulo"), "Total ", "")
                End If
            Case 4
                If InStrRev(dst.Tables("listado").Rows(0).Item("Var2Titulo"), "(") > 0 Then
                    leyenda_y = Mid(dst.Tables("listado").Rows(0).Item("Var2Titulo"), InStrRev(dst.Tables("listado").Rows(0).Item("Var2Titulo"), "(") + 1, dst.Tables("listado").Rows(0).Item("Var2Titulo").ToString.Length - InStrRev(dst.Tables("listado").Rows(0).Item("Var2Titulo"), "(") - 1)
                Else
                    leyenda_y = Replace(dst.Tables("listado").Rows(0).Item("Var2Titulo"), "Total ", "")
                End If
        End Select

        j = Request.QueryString.Count - 1
        For i = 1 To j
            If Request.QueryString(i) = True Then
                graph.AddElement(CrearCurva(Request.QueryString.Keys(i), colores((i - 1) Mod 10)))
            End If
        Next

        Dim titulo, titulo2 As String
        titulo2 = Mid(dst.Tables("listado").Rows(0).Item("rama"), InStr(dst.Tables("listado").Rows(0).Item("rama"), "("))
        titulo = Left(dst.Tables("listado").Rows(0).Item("rama"), InStr(dst.Tables("listado").Rows(0).Item("rama"), "(") - 1) & _
                Left(titulo2, InStr(titulo2, "-") - 1) & vbCrLf & Mid(titulo2, InStr(titulo2, "-"))

        graph.Title = New OpenFlashChart.Title(Replace(titulo, vbCrLf, "") & vbCrLf & "Informe de consumos")
        graph.Title.Style = "font-size:14px; font-family:Verdana; color: #8B8B46;font-weight: bold; background-color:#D7DDBB"
        graph.X_Legend = New OpenFlashChart.Legend("Fecha")
        graph.Y_Legend = New OpenFlashChart.Legend(leyenda_y)
        graph.X_Legend.Style = "font-size:12px; font-family:Verdana; color: #000000"
        graph.Y_Legend.Style = "font-size:14px; font-family:Verdana; color: #000000"

        max = max * 1.1
        If max <> CInt(max) Then
            max = Math.Truncate(max) + 1
        End If

        If min >= 0 Then
            min = 0
        Else
            min = min * 1.1
            If min <> CInt(min) Then
                min = Math.Truncate(min) - 1
            End If
        End If

        graph.Y_Axis.Max = max
        graph.Y_Axis.Min = min

        graph.Y_Axis.Steps = 1

        If max > 5 Or min < -5 Then
            graph.Y_Axis.Steps = Math.Max(Math.Round(max / 5), Math.Abs(Math.Round(min / 5)))
        End If

        lbls = New List(Of String)
        crearEjeX()

        graph.X_Axis.Labels.Values = lbls
        graph.Tooltip = New OpenFlashChart.ToolTip("#key#<br>#val#")
        graph.X_Axis.Labels.Vertical = True

        Response.Clear()
        Response.Write(graph.ToString())

        Response.End()
    End Sub


    Sub crearEjeX()

        Dim numFilas As Integer = dst.Tables("listado").Rows.Count - 1
        Dim j As Integer = 0
        Dim numSteps = Math.Round(numFilas / 5)

        For i As Integer = 0 To numFilas
            If numFilas > numSteps Then
                If j = numSteps Then
                    lbls.Add(String.Format("{0:dd/MM/yyyy}", dst.Tables("listado").Rows(i).Item("Fecha_Medida")))
                    j = 0
                Else
                    lbls.Add("")
                    j += 1
                End If
            Else
                lbls.Add(String.Format("{0:dd/MM/yyyy}", dst.Tables("listado").Rows(i).Item("Fecha_Medida")))
                j = 0
            End If
        Next
    End Sub

    Private Function CrearCurva(ByVal columna As String, ByVal color As String) As LineDot

        'CREACION DEL GRAFICO
        Dim i As Integer
        Dim Var As OpenFlashChart.LineDot = New OpenFlashChart.LineDot()
        Dim dt As New DataTable
        dt = dst.Tables("listado")

        datos = New List(Of Double)

        Dim filas As Integer = dt.Rows.Count

        Dim v As Double
        Dim valor As Decimal = 0

        For i = 0 To filas - 1
            'calculo la posicion en el que tengo que insertarlo
            valor = dt.Rows(i).Item(columna & "Valor")
            v = Math.Round(valor, 2)
            datos.Add(v)

            If v > max Then
                max = v
            End If
            If v < min Then
                min = v
            End If
        Next

        With Var
            .Values = datos
            .HaloSize = 0
            .Width = 1
            .DotSize = 2
            .Colour = color
            .Text = Replace(dt.Rows(0).Item(columna & "Titulo"), "V diferencial Acumulado", "Acumulado")
            .Fontsize = 10
        End With
        datos = Nothing

        Return Var
    End Function
End Class
