Imports OpenFlashChart
Imports System.Drawing
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles
Imports System.Collections.Generic
Imports GuarderiaFluvial.JavaScript
Partial Class SICAH_CurvasAcequiasDETALLEData

    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daCurvasAcequias As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCurvasAcequias As DataSet = New System.Data.DataSet()
    Dim objTrans As SqlClient.SqlTransaction
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim sentenciaSel, sentenciaOrder As String
    Dim adapter As New SqlClient.SqlDataAdapter("", conexion)
    Dim dstNombres As DataSet = New System.Data.DataSet()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            'para que el gráfico no recargue la pagina
            Response.CacheControl = "no-cache"
            CrearDataSetsCurvasAcequias()
            'If nullABlanco(Request.Params("Regimen")) = "" Then
            '    CrearDataSetCurvaAcequia()
            'End If
        End If
    End Sub
    Sub CrearDataSetCurvaAcequia()
        Dim i As Integer
        Dim v_codigoPVYCR, v_idElementoMedida, v_regimen, v_fecha_inicio_uso As String
        Dim dt As DataSet = Session("TablaCaudales")

        Dim MiGrafico As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart
        Dim maximoCaudal As Double
        Dim etiquetasEjeX As List(Of String) = New List(Of String)
        Dim arrayColores As ArrayList = New ArrayList()
        Dim j As Integer = 0
        Dim paso As Double

        arrayColores.Add("#000000")
        arrayColores.Add("#0000FF")
        arrayColores.Add("#FF0000")
        arrayColores.Add("#00FF00")

        v_codigoPVYCR = Request.Params("CodigoPVYCR") 'Session("CodigoPVYCR").ToString
        v_idElementoMedida = Request.Params("idElementomedida") 'Session("idElementomedida").ToString
        v_regimen = Request.Params("Regimen")
        v_fecha_inicio_uso = Request.Params("Fecha_Inicio_Uso")

        If dstCurvasAcequias.Tables.Contains("TablaCurvasAcequias") Then
            dstCurvasAcequias.Tables("TablaCurvasAcequias").Clear()
        End If

        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)

        'Solo se muestran los datos de la Curva Seleccionada
        sentenciaSel = "SELECT * FROM dbo.PVYCR_CurvasAcequias " & _
                       "WHERE CodigoPVYCR='" & v_codigoPVYCR & "' AND IdElementoMedida='" & v_idElementoMedida & "' AND Regimen='" & v_regimen & "' AND Fecha_INICIO_USO='" & v_fecha_inicio_uso & "' "
        sentenciaOrder = "ORDER BY CodigoPVYCR, idElementoMedida, Regimen, Fecha_Inicio_Uso"

        sentenciaSel = sentenciaSel & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequias")

        adapter.SelectCommand.CommandText = "SELECT c.DenominacionCauce, p.denominacionPunto FROM PVYCR_Cauces C, PVYCR_Puntos P where P.codigoCauce = C.codigoCauce and P.CodigoPVYCR = '" + v_codigoPVYCR + "'"
        'adapter.SelectCommand.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("SELECT DenominacionPunto, A1_M,A2_M,B1_M,B2_M,B3_M,H1_M,H2_M,Offset_M FROM PVYCR_Puntos P where P.CodigoPVYCR = '", Me.Session.Item("CodigoPVYCR")), "'"))

        adapter.Fill(dstNombres, "TablaNombres")

        'Cargamos la Serie de Datos para mostrar las Curva de la Acequia Seleccionada

        'NAY: relleno el vector de colores para poder pintar las filas

        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaMaximoCaudal")
        ValoresEjeX(v_codigoPVYCR, v_idElementoMedida, dstCurvasAcequias.Tables("TablaValoresTodasLasCurvas"), etiquetasEjeX)
        maximoCaudal = ObtenerMaximoCaudal(v_codigoPVYCR, v_idElementoMedida)

        ActualizarDataSetValoresCurva(dstCurvasAcequias.Tables("TablaAuxCodigosCurvaAcequia").Rows(i).Item("Cod_Curva"))
        If j > 4 Then
            j = 0
        End If
        MiGrafico.AddElement(CreacionLineas(dstCurvasAcequias.Tables("TablaCurvasAcequiasValores"), i, dstCurvasAcequias.Tables("TablaValoresTodasLasCurvas"), arrayColores(j), paso))
        j = j + 1

        RefrescarGrafica(MiGrafico, maximoCaudal, etiquetasEjeX)

    End Sub

    Sub CrearDataSetsCurvasAcequias()
        Dim filas, i As Integer
        Dim v_codigoPVYCR, v_idElementoMedida As String
        Dim dt As DataSet = Session("TablaCaudales")
        Dim dtFiltrado As DataSet
        Dim MiGrafico As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart
        Dim maximoCaudal As Double
        Dim etiquetasEjeX As List(Of String) = New List(Of String)
        Dim arrayColores As ArrayList = New ArrayList()
        Dim j As Integer = 0
        Dim paso As Double

        arrayColores.Add("#000000")
        arrayColores.Add("#0000FF")
        arrayColores.Add("#FF0000")
        arrayColores.Add("#00FF00")

        v_codigoPVYCR = Request.Params("CodigoPVYCR") 'Session("CodigoPVYCR").ToString
        v_idElementoMedida = Request.Params("idElementomedida") 'Session("idElementomedida").ToString

        If dstCurvasAcequias.Tables.Contains("TablaCurvasAcequias") Then
            dstCurvasAcequias.Tables("TablaCurvasAcequias").Clear()
        End If
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        'Solo se muestran las Curvas de la Acequia Seleccionada
        sentenciaSel = "SELECT * FROM dbo.PVYCR_CurvasAcequias " & _
                       "WHERE CodigoPVYCR='" & v_codigoPVYCR & "' AND IdElementoMedida='" & v_idElementoMedida & "' "
        sentenciaOrder = "ORDER BY CodigoPVYCR, idElementoMedida, Regimen, Fecha_Inicio_Uso"

        sentenciaSel = sentenciaSel & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequias")

        adapter.SelectCommand.CommandText = "SELECT c.DenominacionCauce, p.denominacionPunto FROM PVYCR_Cauces C, PVYCR_Puntos P where P.codigoCauce = C.codigoCauce and P.CodigoPVYCR = '" + v_codigoPVYCR + "'"
        'adapter.SelectCommand.CommandText = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("SELECT DenominacionPunto, A1_M,A2_M,B1_M,B2_M,B3_M,H1_M,H2_M,Offset_M FROM PVYCR_Puntos P where P.CodigoPVYCR = '", Me.Session.Item("CodigoPVYCR")), "'"))

        adapter.Fill(dstNombres, "TablaNombres")



        'Cargamos las Series de Datos para mostrar las Curvas de la Acequia
        'Solo se muestran las Curvas de la Acequia Seleccionada
        If dstCurvasAcequias.Tables.Contains("TablaAuxCodigosCurvaAcequia") Then
            dstCurvasAcequias.Tables("TablaAuxCodigosCurvaAcequia").Clear()
        End If
        sentenciaSel = "SELECT DISTINCT Cod_Curva FROM dbo.PVYCR_CurvasAcequias " & _
         "WHERE CodigoPVYCR='" & v_codigoPVYCR & "' AND IdElementoMedida='" & v_idElementoMedida & "' and Cod_Curva <> '991' "
        sentenciaOrder = "ORDER BY Cod_Curva"
        sentenciaSel = sentenciaSel & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaAuxCodigosCurvaAcequia")


        filas = dstCurvasAcequias.Tables("TablaAuxCodigosCurvaAcequia").Rows.Count
        If filas > 0 Then
            'relleno el vector de colores para poder pintar las filas

            daCurvasAcequias.Fill(dstCurvasAcequias, "TablaMaximoCaudal")
            ValoresEjeX(v_codigoPVYCR, v_idElementoMedida, dstCurvasAcequias.Tables("TablaValoresTodasLasCurvas"), etiquetasEjeX)
            maximoCaudal = ObtenerMaximoCaudal(v_codigoPVYCR, v_idElementoMedida)
            For i = 0 To filas - 1
                ActualizarDataSetValoresCurva(dstCurvasAcequias.Tables("TablaAuxCodigosCurvaAcequia").Rows(i).Item("Cod_Curva"))
                If j > 4 Then
                    j = 0
                End If
                MiGrafico.AddElement(CreacionLineas(dstCurvasAcequias.Tables("TablaCurvasAcequiasValores"), i, dstCurvasAcequias.Tables("TablaValoresTodasLasCurvas"), arrayColores(j), paso))
                j = j + 1
            Next
            'MiGrafico.AddElement(IncluirPuntoscaudales(dt, paso))
            RefrescarGrafica(MiGrafico, maximoCaudal, etiquetasEjeX)
        Else
            RefrescarGrafica(MiGrafico, 0, etiquetasEjeX)
        End If


        'RefrescarGraficaCaudales(IncluirPuntoscaudales(dt), ChartSeriesDeCurvasAcequia)
        'End If
    End Sub

    Private Function ObtenerMaximoCaudal(ByVal codigoPVYCR As String, ByVal idElementoMedida As String) As Double
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = "SELECT top 1 V.Caudal FROM dbo.PVYCR_CurvasAcequias c, dbo.PVYCR_CurvasAcequias_Valores V " & _
                     "WHERE V.COD_CURVA = C.COD_CURVA AND CodigoPVYCR='" & codigoPVYCR & "' AND IdElementoMedida='" & idElementoMedida & "' " & _
                     "and V.Cod_Curva <> '991' ORDER BY V.caudal desc"
        comando.CommandText = sentenciaSel
        Dim maximoCaudal As Double
        maximoCaudal = comando.ExecuteScalar
        Return maximoCaudal
    End Function
    Private Sub ActualizarDataSetValoresCurva(ByVal Cod_Curva As String)
        Dim v_codigoPVYCR, v_idElementoMedida As String


        v_codigoPVYCR = Request.Params("CodigoPVYCR") 'Session("CodigoPVYCR").ToString
        v_idElementoMedida = Request.Params("idElementomedida") 'Session("idElementomedida").ToString

        If dstCurvasAcequias.Tables.Contains("TablaCurvasAcequiasValores") Then
            dstCurvasAcequias.Tables("TablaCurvasAcequiasValores").Clear()
        End If
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        'Solo se muestran las Curvas de la Acequia Seleccionada
        sentenciaSel = "SELECT *, (select regimen from PVYCR_CurvasAcequias where codigoPVYCR='" & v_codigoPVYCR & _
                "' and idElementoMedida='" & v_idElementoMedida & "' and cod_curva=" & Cod_Curva & ") as regimen FROM dbo.PVYCR_CurvasAcequias_Valores " & _
         "WHERE Cod_Curva='" & Cod_Curva & "' and caudal is not null "
        sentenciaOrder = "ORDER BY Cod_Curva, Nivel "

        sentenciaSel = sentenciaSel & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequiasValores")

    End Sub
    Private Sub DataSetValoresTodasLasCurvas()
        Dim v_codigoPVYCR, v_idElementoMedida As String


        v_codigoPVYCR = Request.Params("CodigoPVYCR") 'Session("CodigoPVYCR").ToString
        v_idElementoMedida = Request.Params("idElementomedida") 'Session("idElementomedida").ToString

        If dstCurvasAcequias.Tables.Contains("TablaCurvasAcequiasValores") Then
            dstCurvasAcequias.Tables("TablaCurvasAcequiasValores").Clear()
        End If
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        'Solo se muestran las Curvas de la Acequia Seleccionada
        sentenciaSel = "SELECT *, (select regimen from PVYCR_CurvasAcequias where codigoPVYCR='" & v_codigoPVYCR & _
                "' and idElementoMedida='" & v_idElementoMedida & "' ) as regimen FROM dbo.PVYCR_CurvasAcequias_Valores "

        sentenciaOrder = "ORDER BY Cod_Curva, Nivel "

        sentenciaSel = sentenciaSel & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequiasValores")

    End Sub

    Private Function CreacionLineas(ByVal midatatable As DataTable, ByVal Numserie As Integer, ByVal midatatableTodas As DataTable, ByVal colorLinea As String, ByRef paso As Double) As LineDot

        'declaramos un array del tamaño de las filas que tengamos en el dataset
        Dim filas, filasTodas As Integer

        ' Dim grafico As OpenFlashChart.OpenFlashChart = New OpenFlashChart.OpenFlashChart()
        Dim linea As Line = New Line
        Dim i As Double

        Dim altura As OpenFlashChart.LineDot = New OpenFlashChart.LineDot
        Dim datosAltura As List(Of Double) = New List(Of Double)

        filas = midatatable.Rows.Count
        filasTodas = midatatableTodas.Rows.Count

        'IPIM 01/09/2008 Para no mostrar las curvas con nivel 0 y 1000 y sólo dos lecturas
        If filas = 2 And midatatable.Rows(0).Item("Nivel") = 0 And midatatable.Rows(1).Item("Nivel") = 1000 Then
            Return altura
        ElseIf filas > 0 Then

            Dim PrimerValorX As Double = midatatable.Rows(0).Item("Nivel")

            Dim UltimoValorTodasX As Double = midatatableTodas.Rows(filasTodas - 1).Item("Nivel")
            'Dim ultimoValorY As Double = midatatable.Rows(filas - 1).Item("Caudal")
            Dim posicion, capacidadEjeX As Integer
            'Dim paso As Double
            paso = Math.Round(((UltimoValorTodasX * 1.2 - PrimerValorX) / 100), 2)

            If paso = 0 Then
                paso = 0.01
            End If

            Dim j As Integer = 0
            'rellenamos la lista a nulos
            capacidadEjeX = UltimoValorTodasX * 100
            For i = 0 To capacidadEjeX * 1.2 - 1
                datosAltura.Add(Double.NegativeInfinity)
            Next

            For i = 0 To filas - 1
                'calculo la posicion en el que tengo que insertarlo
                posicion = (dstCurvasAcequias.Tables("TablaValoresTodasLasCurvas").Rows(i).Item("Nivel") - PrimerValorX) / paso
                datosAltura.Insert(posicion, midatatable.Rows(i).Item("Caudal"))
            Next

            With altura
                .Values = datosAltura
                .HaloSize = 0
                .Width = 1
                .DotSize = 3
                .Colour = colorLinea '"#000000"
                .Text = "Curva " & midatatable.Rows(0).Item("Cod_Curva") & " (" & midatatable.Rows(0).Item("Regimen") & ")" '"Altura"
                .Fontsize = 5
            End With

            Return altura
        End If
    End Function

    Private Sub RefrescarGrafica(ByVal grafico As OpenFlashChart.OpenFlashChart, ByVal maximoCaudal As Double, ByVal lbls As List(Of String))

        Dim j As Integer = 0
        Dim i As Double
        If maximoCaudal > 0 Then

            'Dim UltimoValorX As Double = midatatable.Rows(filas - 1).Item("Nivel")
            'Dim PrimerValorX As Double = midatatable.Rows(0).Item("Nivel")
            Dim ultimoValorY As Double = maximoCaudal
            Dim v_codigoPVYCR As String
            Dim v_idElementoMedida As String

            v_codigoPVYCR = Request.Params("CodigoPVYCR") 'Session("CodigoPVYCR").ToString
            v_idElementoMedida = Request.Params("idElementomedida") 'Session("idElementomedida").ToString

            grafico.Title = New OpenFlashChart.Title(v_codigoPVYCR + " / " + dstNombres.Tables("TablaNombres").Rows(0).Item("DenominacionPunto").ToString + " / " + vbCrLf + dstNombres.Tables("TablaNombres").Rows(0).Item("DenominacionCauce").ToString)
            grafico.Title.Style = "font-size:11px; font-family:Verdana; color: #8B8B46;font-weight: bold; background-color:#D7DDBB"
            grafico.X_Legend = New OpenFlashChart.Legend("Altura (m)")
            grafico.Y_Legend = New OpenFlashChart.Legend("Caudal (m3/s)")
            grafico.X_Legend.Style = "font-size:10px; font-family:Verdana; color: #000000"
            grafico.Y_Legend.Style = "font-size:11px; font-family:Verdana; color: #000000"

            grafico.Y_Axis.Min = 0

            'grafico.Y_Axis.Steps = 10
            If ultimoValorY > 1 Then
                grafico.Y_Axis.Max = ultimoValorY * 1.1
            Else
                grafico.Y_Axis.Max = 1
            End If
            If ultimoValorY > 100 Then
                grafico.Y_Axis.Steps = 10
            Else
                grafico.Y_Axis.Steps = 1
            End If
            'grafico.Y_Axis.Steps = 1

            grafico.X_Axis.Labels.Values = lbls
            grafico.X_Axis.Labels.Vertical = True
        Else
            grafico.Title = New OpenFlashChart.Title("No existen Valores")
        End If

        Response.Clear()

        Response.Write(grafico.ToString())

        Response.End()
    End Sub


    Private Sub ValoresEjeX(ByVal codigoPVYCR As String, ByVal idElementoMedida As String, ByVal midatatable As DataTable, ByRef lbls As List(Of String))

        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = "SELECT DISTINCT V.Nivel FROM dbo.PVYCR_CurvasAcequias c, dbo.PVYCR_CurvasAcequias_Valores V " & _
                        "WHERE V.COD_CURVA = C.COD_CURVA AND CodigoPVYCR='" & codigoPVYCR & "' AND IdElementoMedida='" & idElementoMedida & "' " & _
                        "and V.Cod_Curva <> '991' ORDER BY V.Nivel"
        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel

        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaValoresTodasLasCurvas")


        Dim filas As Integer
        midatatable = dstCurvasAcequias.Tables("TablaValoresTodasLasCurvas")
        filas = midatatable.Rows.Count

        Dim UltimoValorX As Double = Math.Round(midatatable.Rows(filas - 1).Item("Nivel"), 2)
        Dim PrimerValorX As Double = Math.Round(midatatable.Rows(0).Item("Nivel"), 2)
        'Dim ultimoValorY As Double = maximoCaudal
        'Dim lbls As List(Of String) = New List(Of String)
        Dim paso As Double
        Dim i As Double

        paso = Math.Round(((UltimoValorX * 1.2 - PrimerValorX) / 100), 2)
        If paso = 0 Then
            paso = 0.01
        End If

        For i = PrimerValorX To UltimoValorX Step paso
            If Math.Round((i * 100), 2) Mod 10 = 0 Then
                lbls.Add(Math.Round((i), 2))
            Else
                lbls.Add("")
            End If

        Next



    End Sub
    'Private Function ObtenerMaximoCaudal(ByVal codigoPVYCR As String, ByVal idElementoMedida As String) As Double
    '    Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
    '    sentenciaSel = "SELECT top 1 V.Caudal FROM dbo.PVYCR_CurvasAcequias c, dbo.PVYCR_CurvasAcequias_Valores V " & _
    '                 "WHERE V.COD_CURVA = C.COD_CURVA AND CodigoPVYCR='" & codigoPVYCR & "' AND IdElementoMedida='" & idElementoMedida & "' " & _
    '                 "and V.Cod_Curva <> '991' ORDER BY V.caudal desc"
    '    comando.CommandText = sentenciaSel
    '    Dim maximoCaudal As Double
    '    maximoCaudal = comando.ExecuteScalar
    '    Return maximoCaudal
    'End Function
    'Private Function IncluirPuntoscaudales(ByVal dstPuntosCaudales As DataSet, ByVal paso As Double) As Scatter
    '    '    'cada caudal del string será un punto de la gráfica
    '    Dim filas As Integer
    '    Dim fila As DataRow
    '    Dim punto As ScatterValue = New ScatterValue(0, 0, 4)
    '    Dim seriePuntos As Scatter = New Scatter("4651B4", 4)

    '    Dim posicion As Integer

    '    filas = dstPuntosCaudales.Tables("TablaCaudales").Rows.Count
    '    If filas > 0 Then
    '        '        serie.Name = "Lecturas " '& dstPuntosCaudales.Tables("TablaCaudales").Rows(0).Item("Curva")
    '        For Each fila In dstPuntosCaudales.Tables("TablaCaudales").Rows
    '            'seriepuntos.Value(Double.NegativeInfinity)
    '            punto.X = 0
    '            punto.Y = 0
    '            seriePuntos.AppendValue(punto)
    '        Next

    '        For Each fila In dstPuntosCaudales.Tables("TablaCaudales").Rows
    '            If nullACero(fila("Caudal")) <> 0 Then
    '                posicion = fila.Item("altura") / paso

    '                'punto.X = nullACero(fila("altura"))
    '                punto.X = posicion
    '                punto.Y = nullACero(fila("Caudal"))
    '                seriePuntos.AppendValue(punto)

    '            End If
    '        Next fila
    '    End If

    '    Return seriepuntos
    'End Function
    'Private Sub RefrescarGraficaCaudales(ByVal MiSerieDeDatos As SeriesCollection, ByVal Migrafica As dotnetCHARTING.Chart)
    '    With Migrafica
    '        .Type = ChartType.Scatter
    '        .SeriesCollection.Add(MiSerieDeDatos)
    '        .TitleBox.Label = New Label(Session("CodigoPVYCR").ToString + " / " + dstNombres.Tables("TablaNombres").Rows(0).Item("DenominacionPunto").ToString + " / " + dstNombres.Tables("TablaNombres").Rows(0).Item("DenominacionCauce").ToString, New Font("Arial", 12.0!, FontStyle.Bold), Color.White)
    '        .TitleBox.Position = TitleBoxPosition.FullWithLegend
    '        .TitleBox.CornerTopLeft = BoxCorner.Round
    '        .TitleBox.CornerTopRight = BoxCorner.Round
    '        .TitleBox.Background.ShadingEffectMode = ShadingEffectMode.Five
    '        .TitleBox.Background.Color = Color.DarkKhaki
    '        .TitleBox.CornerSize = 20

    '    End With
    'End Sub


End Class
