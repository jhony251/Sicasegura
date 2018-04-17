Imports OpenFlashChart
Imports System.Drawing
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles
Imports System.Collections.Generic
Imports GuarderiaFluvial.JavaScript
Imports System

Partial Class SICAH_CurvasAcequiasPreproduccionFlashData
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daCurvasAcequias As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCurvasAcequias As DataSet = New System.Data.DataSet()
    Dim objTrans As SqlClient.SqlTransaction
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim sentenciaSel, sentenciaOrder As String
    Dim adapter As New SqlClient.SqlDataAdapter("", conexion)
    Dim dstNombres As DataSet = New System.Data.DataSet()

    'IPIM 11/12/2008
    Dim grafico As String = ""
    Dim lbls As List(Of String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            'para que el gráfico no recargue la pagina
            Response.CacheControl = "no-cache"

            CrearDataSetsCurvasAcequias()
        End If
    End Sub
    Sub CrearDataSetsCurvasAcequias()
        Dim filas, i, filasCaudales As Integer
        Dim v_codigoPVYCR, v_idElementoMedida As String
        Dim dt As DataSet = Session("TablaCaudales")
        'IPIM 11/12/2008 Dim dtDesdeAcequias As DataTable = Session("TablaCaudalesDesdeAcequias")

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

        v_codigoPVYCR = Session("CodigoPVYCR").ToString
        v_idElementoMedida = Session("idElementomedida").ToString

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
            'IPIM 11/12/2008 If Not IsNothing(Session("TablaCaudalesDesdeAcequias")) Then
            'If Not IsNothing(dtDesdeAcequias.DefaultView) Then
            '    filasCaudales = dtDesdeAcequias.DefaultView.Count
            '    If filasCaudales > 0 Then
            '        For i = 0 To filasCaudales - 1
            '            MiGrafico.AddElement(IncluirPuntoscaudales(dtDesdeAcequias.DefaultView(i).Row, paso, "S", color))
            '        Next
            '    End If
            'Else
            '    filasCaudales = dt.Tables("TablaCaudales").Rows.Count
            '    If filasCaudales > 0 Then
            '        For i = 0 To filasCaudales - 1
            '            MiGrafico.AddElement(IncluirPuntoscaudales(dt.Tables("TablaCaudales").Rows(i), paso, "N", color))
            '        Next
            '    End If
            'End If

            '11/12/2008 Para Insertar los puntos
            'CREACION DEL GRAFICO
            Dim colores As New ArrayList
            With colores
                .Add("#AA00FF")
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
            grafico = Request.QueryString("grafico")
            Dim lineaTipo(0) As OpenFlashChart.LineDot

            MiGrafico.Title = New OpenFlashChart.Title(Session("Titulo") & vbCrLf & "Gráfico por " & grafico)
            MiGrafico.Title.Style = "font-size:14px; font-family:Verdana; color: #8B8B46;font-weight: bold; background-color:#D7DDBB"
            
            j = Request.QueryString.Count - 1
            For i = 1 To j - 1
                If Request.QueryString(i) = True Then
                    CrearPuntos(Request.QueryString.Keys(i), colores((i - 1) Mod 10), paso, MiGrafico, dt)

                    ReDim lineaTipo(i)
                    lineaTipo(i) = New OpenFlashChart.LineDot
                    With lineaTipo(i)
                        .HaloSize = 0
                        .Width = 1
                        .DotSize = 3
                        .Colour = colores((i - 1) Mod 10)
                        .Text = Request.QueryString.Keys(i)
                        .Fontsize = 5
                    End With
                    MiGrafico.AddElement(lineaTipo(i))
                End If
            Next


            'If filasCaudales > 0 Then
            '    For i = 0 To filasCaudales - 1
            '        MiGrafico.AddElement(IncluirPuntoscaudales(dt.Tables("TablaCaudales").Rows(i), paso))
            '    Next
            'End If

            RefrescarGrafica(MiGrafico, maximoCaudal, etiquetasEjeX)
        Else
            RefrescarGrafica(MiGrafico, 0, etiquetasEjeX)
        End If


        'RefrescarGraficaCaudales(IncluirPuntoscaudales(dt), ChartSeriesDeCurvasAcequia)
        'End If



        


    End Sub
    Private Sub ActualizarDataSetValoresCurva(ByVal Cod_Curva As String)
        Dim v_codigoPVYCR, v_idElementoMedida As String

        v_codigoPVYCR = Session("CodigoPVYCR").ToString
        v_idElementoMedida = Session("idElementomedida").ToString

        If dstCurvasAcequias.Tables.Contains("TablaCurvasAcequiasValores") Then
            dstCurvasAcequias.Tables("TablaCurvasAcequiasValores").Clear()
        End If
        Dim sFiltro As String = ""

        'Solo se muestran las Curvas de la Acequia Seleccionada
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = "SELECT Cod_Curva, Nivel,isnull(caudal,0) caudal, (select regimen from PVYCR_CurvasAcequias where codigoPVYCR='" & v_codigoPVYCR & _
                "' and idElementoMedida='" & v_idElementoMedida & "' and cod_curva=" & Cod_Curva & ") as regimen FROM dbo.PVYCR_CurvasAcequias_Valores " & _
         "WHERE Cod_Curva='" & Cod_Curva & "' "
        sentenciaOrder = "ORDER BY Cod_Curva, Nivel "

        sentenciaSel = sentenciaSel & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequiasValores")

    End Sub
    Private Sub DataSetValoresTodasLasCurvas()
        Dim v_codigoPVYCR, v_idElementoMedida As String

        v_codigoPVYCR = Session("CodigoPVYCR").ToString
        v_idElementoMedida = Session("idElementomedida").ToString

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
            capacidadEjeX = UltimoValorTodasX * 1.2 * 100
            For i = 0 To capacidadEjeX - 1
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


            'ipim 15/12/2008
            'grafico.Title = New OpenFlashChart.Title("")
            ''grafico.Title.Style = "font-size:14px; font-family:Verdana; color: #8B8B46;font-weight: bold; background-color:#D7DDBB"
            grafico.X_Legend = New OpenFlashChart.Legend("Altura (m)")
            grafico.Y_Legend = New OpenFlashChart.Legend("Caudal (m3/s)")

            grafico.X_Legend.Style = "font-size:12px; font-family:Verdana; color: #000000"
            grafico.Y_Legend.Style = "font-size:14px; font-family:Verdana; color: #000000"
           
            grafico.Y_Axis.Min = 0
            grafico.Y_Axis.TickLength = 5
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
    Private Function IncluirPuntoscaudales(ByVal filaCaudal As DataRow, ByVal paso As Double, ByVal desdeLecturas As String, ByVal color As String) As Scatter
        '    'cada caudal del string será un punto de la gráfica
        Dim filas As Integer
        'Dim fila As DataRow = dstPuntosCaudales
        Dim punto As ScatterValue = New ScatterValue(0, 0, 4)
        'Dim seriePuntos As Scatter = New Scatter("5551FF", 4) '4651B4
        Dim seriePuntos As Scatter = New Scatter(color, 4)

        Dim posicion As Integer

        ' si se llama desde lecturas las columnas tienen unos nombres y desde acequias otros, por eso el if 
        If desdeLecturas = "S" Then
            If nullACero(filaCaudal("Caudal_m3s")) <> 0 Then
                If filaCaudal.Item("calado_m") IsNot DBNull.Value Then
                    posicion = filaCaudal.Item("calado_m") / paso
                    'IPIM 25/11/2008
                    'punto.X = posicion
                    'punto.Y = nullACero(filaCaudal("Caudal_m3s"))
                    'seriePuntos.AppendValue(punto)
                Else
                    posicion = utiles.nullACero(filaCaudal.Item("escala_m")) / paso
                End If
                punto.X = posicion
                punto.Y = nullACero(filaCaudal("Caudal_m3s"))
                seriePuntos.AppendValue(punto)
            End If

        Else
            If nullACero(filaCaudal("Caudal")) <> 0 Then
                If filaCaudal.Item("altura") IsNot DBNull.Value Then
                    posicion = filaCaudal.Item("altura") / paso
                    punto.X = posicion
                    punto.Y = nullACero(filaCaudal("Caudal"))
                    seriePuntos.AppendValue(punto)
                End If
            End If
        End If
        Return seriePuntos
    End Function

    Private Sub CrearPuntos(ByVal tipo As String, ByVal color As String, ByVal paso As Double, ByVal MiGrafico As OpenFlashChart.OpenFlashChart, ByVal dt As DataSet)

        Dim i As Integer
        Dim dtDesdeAcequias As DataTable = New DataTable

        dtDesdeAcequias = Session("TablaCaudalesDesdeAcequias")

        Dim filasCaudales As Integer
        If Not IsNothing(dtDesdeAcequias) Then
            dtDesdeAcequias.DefaultView.RowFilter = ""
            dtDesdeAcequias.DefaultView.RowFilter = grafico & " = '" & tipo & "'"
            filasCaudales = dtDesdeAcequias.DefaultView.Count
            If filasCaudales > 0 Then
                For i = 0 To filasCaudales - 1
                    MiGrafico.AddElement(IncluirPuntoscaudales(dtDesdeAcequias.DefaultView(i).Row, paso, "S", color))
                Next
            End If
        Else
            Select Case grafico
                Case "TipoObtencionCaudal"
                    grafico = "TipoObtCaudal"
                Case "RegimenCurva"
                    grafico = "Curva"
            End Select
            dt.Tables("TablaCaudales").DefaultView.RowFilter = ""
            dt.Tables("TablaCaudales").DefaultView.RowFilter = grafico & " = '" & tipo & "'"

            'ipim 29/12/2008 filasCaudales = dt.Tables("TablaCaudales").Rows.Count
            filasCaudales = dt.Tables("TablaCaudales").DefaultView.Count
            If filasCaudales > 0 Then
                For i = 0 To filasCaudales - 1
                    'IPIM 29/12/2008 MiGrafico.AddElement(IncluirPuntoscaudales(dt.Tables("TablaCaudales").Rows(i), paso, "N", color))
                    MiGrafico.AddElement(IncluirPuntoscaudales(dt.Tables("TablaCaudales").DefaultView(i).Row, paso, "N", color))
                Next
            End If
        End If
    End Sub
End Class
