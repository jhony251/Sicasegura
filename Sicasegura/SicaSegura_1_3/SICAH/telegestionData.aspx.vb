Imports OpenFlashChart
Imports System.Drawing
Imports System.Collections.Generic


Partial Class SICAH_telegestionData
    Inherits System.Web.UI.Page
    Dim pto As String
    Dim fechaInicial As DateTime
    Dim fechaFin As DateTime

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.CacheControl = "no-cache"
        Dim myDS As New System.Data.DataSet()
        Dim myDt As New System.Data.DataTable()
        Dim i As Integer
        Dim fecha As DateTime
        Dim puntos() As System.Data.DataRow


        Dim datosNivus As New datosNivus2(System.Configuration.ConfigurationManager.AppSettings("dsn"), "PVYCR_DatosAcequias_NIVUS", "PVYCR_DatosMotores_NIVUS")

        'caudal = New OpenFlashChart.Charts.LineDot(1, 1, Color.DarkOliveGreen.ToString(), "caudal", 12)
        'velocidad = New OpenFlashChart.Charts.LineDot(1, 1, Color.DarkRed.ToString(), "velocidad", 12)

        If Request.Params("punto") = "" Then
            pto = "VA002P30"
        Else
            pto = Request.Params("punto")
        End If

        If Page.Request.Params("finicial") = "" Then
            fechaInicial = Date.Now

        Else
            fechaInicial = Page.Request.Params("finicial")
        End If

        If Page.Request.Params("ffinal") = "" Then
            fechaFin = Date.Now
        Else
            fechaFin = Page.Request.Params("ffinal")
        End If

        Dim mostrarVelocidad, mostrarAltura, mostrarCaudal As Boolean

        If Page.Request.Params("v") = "" Then
            mostrarVelocidad = True
        Else
            mostrarVelocidad = Page.Request.Params("v")
        End If

        If Page.Request.Params("a") = "" Then
            mostrarAltura = True
        Else
            mostrarAltura = Page.Request.Params("a")
        End If

        If Page.Request.Params("c") = "" Then
            mostrarCaudal = True
        Else
            mostrarCaudal = Page.Request.Params("c")
        End If





        myDS = datosNivus.ObtenerHistoricoDePuntosEntreFechas(pto, Format(fechaInicial, "dd/MM/yyyy 00:00:01"), Format(fechaFin, "dd/MM/yyyy 23:59:59"))
        myDt = myDS.Tables("Acequias")

        'Chart.TitleBox.Label = New dotnetCHARTING.Label("Punto de control " + pto.Text, New Font("Arial", 13, FontStyle.Bold), Color.White)
        puntos = myDt.Select("[Punto control] = '" & pto & "'")

        'CREACION DEL GRAFICO

        Dim max As Double = 0
        Dim min As Double = Integer.MaxValue
        Dim alt, cau, vel As Double

        Dim graph As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart()

        Dim altura As OpenFlashChart.LineDot = New OpenFlashChart.LineDot
        Dim caudal As OpenFlashChart.LineDot = New OpenFlashChart.LineDot
        Dim velocidad As OpenFlashChart.LineDot = New OpenFlashChart.LineDot

        Dim datosAltura As List(Of Double) = New List(Of Double)
        Dim datoscaudal As List(Of Double) = New List(Of Double)
        Dim datosvelocidad As List(Of Double) = New List(Of Double)

        Dim lbls As List(Of String) = New List(Of String)

        Dim j As Int16 
		dim nPuntos as Int16 = puntos.GetUpperBound(0)
        If nPuntos > 3000 Then
            nPuntos = 3000
        End If
		
		
		dim maxEtiquetas as Int16 = nPuntos / 20
		
        For i = nPuntos To 0 Step -1
            If mostrarVelocidad Then
                vel = puntos(i)("Velocidad") '.ToString.Replace(",", ".")
                datosvelocidad.Add(vel)

                If vel > max Then
                    max = vel
                End If
            End If

            If mostrarAltura Then
                alt = puntos(i)("altura") '.ToString.Replace(",", ".")
                datosAltura.Add(alt)
                If alt > max Then
                    max = alt
                End If
            End If

            If mostrarCaudal Then
                datoscaudal.Add(cau)
                If cau > max Then
                    max = cau
                End If
                cau = puntos(i)("Caudal") '.ToString.Replace(",", ".")
            End If
            If j = maxEtiquetas Then
                lbls.Add(puntos(i)("Fecha y Hora"))
                j = 0
            Else
                lbls.Add("")
                j += 1
            End If



        Next

        myDS.Dispose()
        myDt.Dispose()

        myDS = Nothing
        myDt = Nothing

        If mostrarAltura Then
            With altura
                .Values = datosAltura
                .HaloSize = 0
                .Width = 1
                .DotSize = 1
                .Colour = "#000000"
                .Text = "Altura (m)"
                .Fontsize = 10
            End With
            graph.AddElement(altura)
        End If
        If mostrarCaudal Then
            With caudal
                .Values = datoscaudal
                .HaloSize = 0
                .Width = 1
                .DotSize = 1
                .Colour = "#2D06D7"
                .Text = "Caudal (m3/s)"
                .Fontsize = 10
                graph.AddElement(caudal)
            End With

        End If

        If mostrarVelocidad Then
            With velocidad
                .Fontsize = 10
                .Text = "Velocidad (m/s)"
                .Values = datosvelocidad
                .HaloSize = 0
                .Width = 1
                .DotSize = 1
                .Colour = "#BC0000"
                graph.AddElement(velocidad)
            End With
        End If

        'graph.Y_Axis.SetRange(0, 15, 5)
        graph.Title = New OpenFlashChart.Title(pto)

        graph.Y_Axis.Max = max * 1.1
        graph.Y_Axis.Min = 0
        graph.Y_Axis.Steps = 1

        graph.X_Axis.Labels.Values = lbls
        graph.X_Axis.Labels.Vertical = True

        Response.Clear()
        Response.Write(graph.ToPrettyString())

        Response.End()
    End Sub

End Class
