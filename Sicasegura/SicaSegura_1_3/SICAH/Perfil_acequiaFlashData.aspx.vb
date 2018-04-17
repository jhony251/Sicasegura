Imports OpenFlashChart
Imports System.Drawing
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles
Imports System.Collections.Generic
Imports GuarderiaFluvial.JavaScript
Imports Microsoft.VisualBasic.CompilerServices
Imports FuncionesGenerales


Partial Class SICAH_Perfil_acequiaFlashData
    Inherits System.Web.UI.Page

    Dim selectConnection As New SqlClient.SqlConnection(ConfigurationSettings.AppSettings.Item("dsnsegura_migracion"))
    Dim adapter As New SqlClient.SqlDataAdapter("", selectConnection)
    Dim dataSet As New DataSet
    Dim Migrafico As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.CacheControl = "no-cache"
        'Dim CodigoPunto As String
        Dim UltimoValorX As Double
        Dim paso As Double
        Dim CodigoPunto As String
        Dim lbls As List(Of String) = New List(Of String)
        'RDF. Fecha: 04/09/2008
        If Not Request.QueryString("Codigo") Is Nothing Then
            CodigoPunto = Request.QueryString("Codigo")
        Else
            CodigoPunto = Me.Session.Item("CodigoPVYCR")
        End If
        'obtenemos los valores de las columnas A1,A2, etc para poder dibujar el grafico
        crearDataset(CodigoPunto)

        If (dataSet.Tables("TablaGrafico").Rows.Count = 0) Then
            UltimoValorX = 0
        Else
            'calculamos el valor máximo para poder calcular el paso o dovusiones del eje de la X
            UltimoValorX = CalculoUltimoValorX(dataSet.Tables("TablaGrafico"))
        End If
        If UltimoValorX > 0 Then
            'calculamos las etiquetas del eje de las X
            ValoresEjeX(UltimoValorX, lbls, paso)
            'Dibujamos el grafico
            CreacionGrafico(dataSet.Tables("TablaGrafico"), lbls, paso)
        Else
            'si no hay valores para los campos A1, A2, etc se diuja vacio
            Migrafico.Title = New OpenFlashChart.Title("No existen Valores")
            Response.Clear()

            Response.Write(Migrafico.ToString())

            Response.End()
        End If
    End Sub
    Protected Sub crearDataset(ByVal codigoPunto As String)
        'If (selectConnection.State = ConnectionState.Closed) Then
        '    selectConnection.Open()
        'End If
        utiles.Comprobar_Conexion_BD(Page, selectConnection)
        adapter.SelectCommand.CommandText = "SELECT c.DenominacionCauce,DenominacionPunto, A1_M,A2_M,A3_M,B1_M,B2_M,B3_M,B4_M,H12_M,H23_M,H34_M,Offset_M " & _
                    "FROM PVYCR_Cauces C, PVYCR_Puntos P where P.codigoCauce = C.codigoCauce and P.CodigoPVYCR = '" + codigoPunto + "'"
        'adapter.SelectCommand.CommandText = "SELECT c.DenominacionCauce,DenominacionPunto, 0.5 as A1_M,0.5 as A2_M,0.5 as A3_M,2 as B1_M,5 as B2_M,5.5 as B3_M,6 as B4_M,0.5 as H12_M,1.25 as H23_M,0.75 as H34_M,0.1 as Offset_M " & _
        '            "FROM PVYCR_Cauces C, PVYCR_Puntos P where P.codigoCauce = C.codigoCauce and P.CodigoPVYCR = '" + codigoPunto + "'"


        adapter.Fill(dataSet, "TablaGrafico")

    End Sub

    'Private Function CalculoUltimoValorX(ByVal midatatable As DataTable) As Double

    '    Dim puntos As ArrayList = New ArrayList()

    '    puntos.Add(0)
    '    puntos.Add(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)))
    '    puntos.Add(CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A2_M").ToString)))))

    '    puntos.Add(CDbl((((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A2_M").ToString)))) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B1_M").ToString)))))

    '    puntos.Add(CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B2_M").ToString)))))

    '    puntos.Add(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString)))

    '    puntos.Add((CDbl(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString))) / 2))

    '    puntos.Add(CDbl(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString))) / 2)


    '    Dim max As Double = 0
    '    Dim i As Integer
    '    For i = 0 To puntos.Capacity - 1
    '        If puntos(i) > max Then
    '            max = puntos.Item(i)
    '        End If
    '    Next

    '    Return max
    'End Function



    'Private Function CalculoUltimoValorX(ByVal midatatable As DataTable) As Double

    '    Dim puntos As ArrayList = New ArrayList()
    '    'inicializo varoles


    '    puntos.Add(0)
    '    puntos.Add(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A3_M").ToString)))
    '    puntos.Add(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A3_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)))
    '    puntos.Add(CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A2_M").ToString)))) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A3_M").ToString)))

    '    puntos.Add(CDbl((((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A2_M").ToString)))) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A3_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B1_M").ToString)))))

    '    puntos.Add(CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A3_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B2_M").ToString)))))

    '    puntos.Add(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A3_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString)))
    '    puntos.Add(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B4_M").ToString)))
    '    puntos.Add((CDbl(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B4_M").ToString))) / 2))

    '    puntos.Add(CDbl(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B4_M").ToString))) / 2)


    '    Dim max As Double = 0
    '    Dim i As Integer
    '    Dim numPuntos As Integer = puntos.Count
    '    For i = 0 To numPuntos - 1
    '        If puntos(i) > max Then
    '            max = puntos.Item(i)
    '        End If
    '    Next

    '    Return max
    'End Function

    Private Sub ValoresEjeX(ByVal UltimoValorX As Double, ByRef lbls As List(Of String), ByRef paso As Double)

        Dim filas As Integer

        'Dim UltimoValorX As Double = Math.Round(midatatable.Rows(filas - 1).Item("Nivel"), 2)
        Dim PrimerValorX As Double = -1
        'Dim ultimoValorY As Double = maximoCaudal
        'Dim lbls As List(Of String) = New List(Of String)
        'Dim paso As Double
        Dim i As Double

        paso = Math.Round(((UltimoValorX - Math.Abs(PrimerValorX)) / 200), 2)
        If paso = 0 Then
            paso = 0.01
        End If

        For i = PrimerValorX To UltimoValorX * 1.1 Step paso
            If Math.Round((i * 100), 2) Mod 50 = 0 Then
                lbls.Add(Math.Round((i), 2))
            Else
                lbls.Add("")
            End If

        Next
        paso = paso
    End Sub


    'Private Sub CreacionGrafico(ByVal midatatable As DataTable, ByVal lbls As List(Of String), ByVal paso As Double)

    '    'declaramos un array del tamaño de las filas que tengamos en el dataset
    '    Dim filas As Integer

    '    ' Dim grafico As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart()
    '    Dim linea As Line = New Line

    '    Dim seccion As OpenFlashChart.LineDot = New OpenFlashChart.LineDot
    '    Dim escala As OpenFlashChart.LineDot = New OpenFlashChart.LineDot
    '    Dim offset As OpenFlashChart.LineDot = New OpenFlashChart.LineDot

    '    'Dim datosSeccion As List(Of Double) = New List(Of Double)
    '    Dim UltimoValorX As Double = CalculoUltimoValorX(midatatable)
    '    Dim capacidadEjeX As Integer
    '    capacidadEjeX = lbls.Count 'UltimoValorX * 200

    '    Dim datosSeccion As Double() = New Double(capacidadEjeX) {}
    '    Dim listaSeccion As List(Of Double) = New List(Of Double)

    '    Dim datosEscala As List(Of Double) = New List(Of Double)
    '    Dim datosOffset As List(Of Double) = New List(Of Double)


    '    'Dim paso As Double
    '    Dim Grafico As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart


    '    filas = midatatable.Rows.Count

    '    If filas > 0 Then

    '        Dim PrimerValorX As Double = -1
    '        Dim punto1, punto2, punto3, punto4, punto5, punto6, punto7, punto8 As Double


    '        'Dim ultimoValorY As Double = midatatable.Rows(filas - 1).Item("Caudal")
    '        Dim posicion As Integer
    '        'Dim paso As Double
    '        'paso = Math.Round(((UltimoValorX - PrimerValorX) / 100), 2)
    '        Dim i As Integer
    '        'rellenamos la lista a nulos

    '        i = PrimerValorX * 200
    '        Dim max As Double
    '        Dim j As Integer

    '        For j = 0 To capacidadEjeX
    '            datosSeccion(j) = Double.NegativeInfinity
    '        Next

    '        For i = 0 To capacidadEjeX - 1
    '            '                datosSeccion(i) = Double.NegativeInfinity
    '            datosEscala.Add(Double.NegativeInfinity)
    '        Next

    '        'elemento9
    '        posicion = (0 - PrimerValorX) / paso
    '        'If posicion < 0 Then
    '        '    posicion = 1
    '        'End If

    '        'datosSeccion.Insert(posicion, Convert.ToDouble(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString))
    '        punto1 = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString))
    '        max = punto1
    '        datosSeccion(posicion) = punto1
    '        'elemento 10
    '        posicion = (Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) - PrimerValorX) / paso
    '        punto2 = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString))
    '        If punto2 > punto1 Then
    '            max = punto2
    '        End If
    '        While (datosSeccion(posicion) <> Double.NegativeInfinity)
    '            'For i = posicion To datosSeccion.Length - 1

    '            posicion = posicion + 1

    '        End While
    '        datosSeccion(posicion) = punto2
    '        'datosSeccion.Insert(posicion, (Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString))))


    '        'elemento 11
    '        posicion = (CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A2_M").ToString)))) - PrimerValorX) / paso
    '        punto3 = 0
    '        If punto3 > punto2 Then
    '            max = punto3
    '        End If
    '        While (datosSeccion(posicion) <> Double.NegativeInfinity)
    '            'For i = posicion To datosSeccion.Length - 1

    '            posicion = posicion + 1

    '        End While

    '        'datosSeccion.Insert(Math.Abs(posicion), 0)
    '        datosSeccion(posicion) = punto3


    '        'elemento 12
    '        posicion = (CDbl((((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A2_M").ToString)))) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B1_M").ToString)))) - PrimerValorX) / paso
    '        punto4 = 0
    '        If punto4 > punto3 Then
    '            max = punto4
    '        End If
    '        While (datosSeccion(posicion) <> Double.NegativeInfinity)
    '            'For i = posicion To datosSeccion.Length - 1

    '            posicion = posicion + 1

    '        End While

    '        'datosSeccion.Insert(Math.Abs(posicion), 0)
    '        datosSeccion(posicion) = punto4
    '        'elemnto 13
    '        posicion = (CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B2_M").ToString)))) - PrimerValorX) / paso
    '        punto5 = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString))
    '        If punto5 > punto4 Then
    '            max = punto5
    '        End If
    '        While (datosSeccion(posicion) <> Double.NegativeInfinity)

    '            posicion = posicion + 1

    '        End While

    '        'datosSeccion.Insert(Math.Abs(posicion), CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString)))))
    '        datosSeccion(posicion) = punto5

    '        'elemento 14
    '        posicion = (Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString)) - PrimerValorX) / paso
    '        punto6 = CDbl((Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString))))
    '        If punto6 > punto5 Then
    '            max = punto6
    '        End If
    '        While (datosSeccion(posicion) <> Double.NegativeInfinity)
    '            'For i = posicion To datosSeccion.Length - 1

    '            posicion = posicion + 1

    '        End While

    '        'datosSeccion.Insert(Math.Abs(posicion), Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)))
    '        datosSeccion(posicion) = punto6

    '        'elemento 4
    '        posicion = 0.25 / paso
    '        datosEscala.Insert(Math.Abs(posicion), Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("offset_m").ToString)))
    '        datosEscala.Insert(Math.Abs(posicion), Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H1_M").ToString)) + Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H2_M").ToString)))

    '        For i = 0 To datosSeccion.Length - 1
    '            listaSeccion.Insert(i, datosSeccion(i))
    '        Next
    '        'calculamos el offset
    '        posicion = ((CDbl(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString))) / 2) - PrimerValorX) / paso
    '        punto7 = Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("Offset_M").ToString))
    '        posicion = ((CDbl(Convert.ToDouble((Conversions.ToString(0) & dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M").ToString))) / 2) - PrimerValorX) / paso
    '        punto8 = 0

    '        With seccion
    '            .Values = listaSeccion
    '            .HaloSize = 0
    '            .Width = 2
    '            .DotSize = 5
    '            .Colour = "#FF0000"
    '            .Text = "Sección" '"Altura"
    '            .Fontsize = 5
    '        End With

    '        With escala
    '            .Values = datosEscala
    '            .HaloSize = 0
    '            .Width = 2
    '            .DotSize = 5
    '            .Colour = "#0000FF"
    '            .Text = "Escala" '"Altura"
    '            .Fontsize = 5
    '        End With

    '        With offset
    '            .Values = datosOffset
    '            .HaloSize = 0
    '            .Width = 2
    '            .DotSize = 5
    '            .Colour = "#00FF00"
    '            .Text = "Offset" '"Altura"
    '            .Fontsize = 5
    '        End With

    '        Grafico.AddElement(seccion)
    '        Grafico.AddElement(escala)
    '        Grafico.AddElement(offset)
    '        Grafico.Title = New OpenFlashChart.Title("")
    '        'Grafico.Title.Style = "font-size:14px; font-family:Verdana; color: #8B8B46;font-weight: bold; background-color:#D7DDBB"
    '        'Grafico.Bgcolor = "#EEFFFF"

    '        'Grafico.Y_Axis.SetColors("black", "#FFFF00")
    '        'Grafico.X_Axis.SetColors("black", "#FFFF00")

    '        'Grafico.Y_Axis.GridColour = "#EEFFFF"
    '        'Grafico.X_Axis.GridColour = "#EEFFFF"
    '        Grafico.X_Legend = New OpenFlashChart.Legend("")
    '        Grafico.Y_Legend = New OpenFlashChart.Legend("")
    '        Grafico.X_Legend.Style = "font-size:12px; font-family:Verdana; color: #000000"
    '        Grafico.Y_Legend.Style = "font-size:14px; font-family:Verdana; color: #000000"

    '        Grafico.Y_Axis.Steps = 1
    '        Grafico.Y_Axis.Min = -1

    '        Grafico.Y_Axis.Max = max * 1.1
    '        Grafico.Y_Axis.TickLength = 5

    '        'Grafico.X_Axis.Min = -1

    '        Grafico.X_Axis.Labels.Values = lbls
    '        Grafico.X_Axis.Labels.Vertical = True


    '        Response.Clear()

    '        Response.Write(Grafico.ToString())

    '        Response.End()

    '    End If
    'End Sub
    Private Sub CreacionGrafico(ByVal midatatable As DataTable, ByVal lbls As List(Of String), ByVal paso As Double)

        'declaramos un array del tamaño de las filas que tengamos en el dataset
        Dim filas As Integer

        ' Dim grafico As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart()
        Dim linea As Line = New Line

        Dim seccion As OpenFlashChart.LineDot = New OpenFlashChart.LineDot
        Dim escala As OpenFlashChart.LineDot = New OpenFlashChart.LineDot
        Dim offset As OpenFlashChart.LineDot = New OpenFlashChart.LineDot

        'Dim datosSeccion As List(Of Double) = New List(Of Double)
        Dim UltimoValorX As Double = CalculoUltimoValorX(midatatable)
        Dim capacidadEjeX As Integer
        capacidadEjeX = lbls.Count 'UltimoValorX * 200

        Dim datosSeccion As Double() = New Double(capacidadEjeX) {}
        Dim listaSeccion As List(Of Double) = New List(Of Double)

        Dim datosEscala As List(Of Double) = New List(Of Double)
        Dim datosOffset As List(Of Double) = New List(Of Double)


        'Dim paso As Double
        Dim Grafico As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart


        filas = midatatable.Rows.Count

        If filas > 0 Then

            Dim PrimerValorX As Double = -1
            Dim punto1, punto2, punto3, punto4, punto5, punto6, punto7, punto8 As Double


            'Dim ultimoValorY As Double = midatatable.Rows(filas - 1).Item("Caudal")
            Dim posicion As Integer
            'Dim paso As Double
            'paso = Math.Round(((UltimoValorX - PrimerValorX) / 100), 2)
            Dim i As Integer
            'rellenamos la lista a nulos

            i = PrimerValorX * 200
            Dim max As Double
            Dim j As Integer

            For j = 0 To capacidadEjeX
                datosSeccion(j) = Double.NegativeInfinity
            Next

            For i = 0 To capacidadEjeX - 1
                '                datosSeccion(i) = Double.NegativeInfinity
                datosEscala.Add(Double.NegativeInfinity)
            Next

            'LA variable posicion indica la X de la macro del excel enviada por los de murcia, y la Y es la variable punto
            posicion = (0 - PrimerValorX) / paso

            punto1 = utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H12_M")) + utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H23_M")) + utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H34_M"))
            max = punto1
            datosSeccion(posicion) = punto1
            'elemento 10
            posicion = (utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A3_M")) - PrimerValorX) / paso
            punto2 = utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H23_M")) + utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H12_M"))
            If punto2 > punto1 Then
                max = punto2
            End If
            While (datosSeccion(posicion) <> Double.NegativeInfinity)
                posicion = posicion + 1
            End While
            datosSeccion(posicion) = punto2
            'elemento 11
            posicion = (utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A2_M")) - PrimerValorX) / paso
            punto3 = utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H12_M"))
            If punto3 > punto2 Then
                max = punto3
            End If
            While (datosSeccion(posicion) <> Double.NegativeInfinity)
                posicion = posicion + 1
            End While

            datosSeccion(posicion) = punto3
            'elemento 12
            posicion = (utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M")) - PrimerValorX) / paso
            punto4 = 0
            If punto4 > punto3 Then
                max = punto4
            End If
            While (datosSeccion(posicion) <> Double.NegativeInfinity)

                posicion = posicion + 1

            End While

            datosSeccion(posicion) = punto4
            'elemnto 13
            posicion = (utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A1_M")) + utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B1_M")) - PrimerValorX) / paso
            punto5 = 0
            If punto5 > punto4 Then
                max = punto5
            End If
            While (datosSeccion(posicion) <> Double.NegativeInfinity)

                posicion = posicion + 1

            End While

            datosSeccion(posicion) = punto5

            'punto 6
            posicion = (utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B2_M")) + utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A2_M")) - PrimerValorX) / paso
            punto6 = utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H12_M"))
            If punto6 > punto5 Then
                max = punto6
            End If
            While (datosSeccion(posicion) <> Double.NegativeInfinity)
                'For i = posicion To datosSeccion.Length - 1

                posicion = posicion + 1

            End While

            datosSeccion(posicion) = punto6
            'punto 7
            posicion = (utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B3_M")) + utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("A3_M")) - PrimerValorX) / paso
            punto7 = utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H12_M")) + utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H23_M"))
            If punto7 > punto6 Then
                max = punto7
            End If
            While (datosSeccion(posicion) <> Double.NegativeInfinity)
                posicion = posicion + 1
            End While
            datosSeccion(posicion) = punto7
            'punto 8
            posicion = (utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B4_M")) - PrimerValorX) / paso
            punto8 = utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H12_M")) + utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H23_M")) + utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H34_M"))
            If punto8 > punto7 Then
                max = punto8
            End If
            While (datosSeccion(posicion) <> Double.NegativeInfinity)
                posicion = posicion + 1
            End While
            datosSeccion(posicion) = punto8

            'elemento 4
            posicion = 0.25 / paso
            datosEscala.Insert(Math.Abs(posicion), utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("offset_m")))
            datosEscala.Insert(Math.Abs(posicion), utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H12_M")) + utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H23_M")) + +utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("H34_M")))

            For i = 0 To datosSeccion.Length - 1
                listaSeccion.Insert(i, datosSeccion(i))
            Next
            'calculamos el offset
            posicion = ((utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B4_M")) / 2) - PrimerValorX) / paso
            punto7 = utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("Offset_M"))
            posicion = ((utiles.nullACero(dataSet.Tables.Item("TablaGrafico").Rows.Item(0).Item("B4_M")) / 2) - PrimerValorX) / paso
            punto8 = 0

            With seccion
                .Values = listaSeccion
                .HaloSize = 0
                .Width = 2
                .DotSize = 5
                .Colour = "#FF0000"
                .Text = "Sección" '"Altura"
                .Fontsize = 5
            End With

            With escala
                .Values = datosEscala
                .HaloSize = 0
                .Width = 2
                .DotSize = 5
                .Colour = "#0000FF"
                .Text = "Escala" '"Altura"
                .Fontsize = 5
            End With

            With offset
                .Values = datosOffset
                .HaloSize = 0
                .Width = 2
                .DotSize = 5
                .Colour = "#00FF00"
                .Text = "Offset" '"Altura"
                .Fontsize = 5
            End With

            Grafico.AddElement(seccion)
            Grafico.AddElement(escala)
            Grafico.AddElement(offset)
            Grafico.Title = New OpenFlashChart.Title("")
            'Grafico.Title.Style = "font-size:14px; font-family:Verdana; color: #8B8B46;font-weight: bold; background-color:#D7DDBB"
            'Grafico.Bgcolor = "#EEFFFF"

            'Grafico.Y_Axis.SetColors("black", "#FFFF00")
            'Grafico.X_Axis.SetColors("black", "#FFFF00")

            'Grafico.Y_Axis.GridColour = "#EEFFFF"
            'Grafico.X_Axis.GridColour = "#EEFFFF"
            Grafico.X_Legend = New OpenFlashChart.Legend("")
            Grafico.Y_Legend = New OpenFlashChart.Legend("")
            Grafico.X_Legend.Style = "font-size:12px; font-family:Verdana; color: #000000"
            Grafico.Y_Legend.Style = "font-size:14px; font-family:Verdana; color: #000000"

            Grafico.Y_Axis.Steps = 1
            Grafico.Y_Axis.Min = -1

            Grafico.Y_Axis.Max = max * 1.1
            Grafico.Y_Axis.TickLength = 5

            'Grafico.X_Axis.Min = -1

            Grafico.X_Axis.Labels.Values = lbls
            Grafico.X_Axis.Labels.Vertical = True


            Response.Clear()

            Response.Write(Grafico.ToString())

            Response.End()

        End If
    End Sub
End Class
