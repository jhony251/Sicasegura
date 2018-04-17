Imports System.Data
Imports dotnetCHARTING
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Partial Class SICAH_CurvasAcequiasPreproduccion
    Inherits System.Web.UI.Page

    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daCurvasAcequias As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCurvasAcequias As DataSet = New System.Data.DataSet()
    Dim objTrans As SqlClient.SqlTransaction
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim sentenciaSel, sentenciaOrder As String
    Dim adapter As New SqlDataAdapter("", conexion)
    Dim dstNombres As DataSet = New System.Data.DataSet()
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            CrearDataSetsCurvasAcequias()
        End If
    End Sub
    Sub CrearDataSetsCurvasAcequias()
        Dim fila As DataRow
        Dim v_codigoPVYCR, v_idElementoMedida As String
        Dim dt As DataSet = Session("TablaCaudales")
        Dim dtFiltrado As DataSet

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

        'Solo mostramos el Repeater de Curvas de la Acequia si existen registros
        If dstCurvasAcequias.Tables("TablaCurvasAcequias").Rows.Count = 0 Then
            ChartSeriesDeCurvasAcequia.Visible = True
        Else

            'Cargamos las Series de Datos para mostrar las Curvas de la Acequia
            'Solo se muestran las Curvas de la Acequia Seleccionada
            If dstCurvasAcequias.Tables.Contains("TablaAuxCodigosCurvaAcequia") Then
                dstCurvasAcequias.Tables("TablaAuxCodigosCurvaAcequia").Clear()
            End If
            sentenciaSel = "SELECT DISTINCT Cod_Curva FROM dbo.PVYCR_CurvasAcequias " & _
             "WHERE CodigoPVYCR='" & v_codigoPVYCR & "' AND IdElementoMedida='" & v_idElementoMedida & "' "
            sentenciaOrder = "ORDER BY Cod_Curva"
            sentenciaSel = sentenciaSel & sentenciaOrder

            daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
            daCurvasAcequias.Fill(dstCurvasAcequias, "TablaAuxCodigosCurvaAcequia")

            'EGB 29/08/2008 Cargamos el Gráfico ChartSeriesDeCurvasAcequia con Todas las series de datos
            'de la Acequia seleccionada
            ChartSeriesDeCurvasAcequia.Visible = True
            ChartSeriesDeCurvasAcequia.SeriesCollection.Clear()
            For Each fila In dstCurvasAcequias.Tables("TablaAuxCodigosCurvaAcequia").Rows
                ActualizarDataSetValoresCurva(fila("Cod_Curva"))
                RefrescarGrafica(DeTableASerie(dstCurvasAcequias.Tables("TablaCurvasAcequiasValores")), ChartSeriesDeCurvasAcequia)
            Next fila

            IncluirPuntoscaudales(dt)
            RefrescarGraficaCaudales(IncluirPuntoscaudales(dt), ChartSeriesDeCurvasAcequia)
        End If
    End Sub
    Private Sub ActualizarDataSetValoresCurva(ByVal Cod_Curva As String)
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
                "' and idElementoMedida='" & v_idElementoMedida & "' and cod_curva=" & Cod_Curva & ") as regimen FROM dbo.PVYCR_CurvasAcequias_Valores " & _
         "WHERE Cod_Curva='" & Cod_Curva & "'"
        sentenciaOrder = "ORDER BY Cod_Curva, Nivel "

        sentenciaSel = sentenciaSel & sentenciaOrder

        daCurvasAcequias.SelectCommand.CommandText = sentenciaSel
        daCurvasAcequias.Fill(dstCurvasAcequias, "TablaCurvasAcequiasValores")

    End Sub

    Private Function DeTableASerie(ByVal midatatable As DataTable) As SeriesCollection
        'EGB 25/08/2008 Función que transforma un DataTable a un elemento de la colección SeriesCollection
        Dim SC As New SeriesCollection()
        Dim fila As DataRow
        Dim serie As New Series()
        Dim elemento As New Element()
        Dim filas As Integer

        'IPIM 01/09/2008 Para no mostrar las curvas con nivel 0 y 1000 y sólo dos lecturas
        If midatatable.Rows.Count = 2 Then
            If midatatable.Rows(0).Item("Nivel") = 0 And midatatable.Rows(1).Item("Nivel") = 1000 Then
                Return SC
                Exit Function
            End If
        End If
        filas = midatatable.Rows.Count
        If filas > 0 Then
            serie.Name = "Curva " & midatatable.Rows(0).Item("Cod_Curva") & " (" & midatatable.Rows(0).Item("Regimen") & ")"
            For Each fila In midatatable.Rows
                elemento = New Element()
                elemento.XValue = nullACero(fila("Nivel"))
                elemento.YValue = nullACero(fila("Caudal"))
                serie.Elements.Add(elemento)
                serie.Type = SeriesType.Line
            Next fila

            SC.Add(serie)
        End If
        Return SC
    End Function
    Private Sub RefrescarGrafica(ByVal MiSerieDeDatos As SeriesCollection, ByVal Migrafica As dotnetCHARTING.Chart)
        With Migrafica
            .Type = ChartType.Scatter
            .SeriesCollection.Add(MiSerieDeDatos)
        End With
    End Sub
    Private Function IncluirPuntoscaudales(ByVal dstPuntosCaudales As DataSet) As SeriesCollection
        'cada caudal del string será un punto de la gráfica
        'Dim  As DataSet = Session("TablaCaudales")
        Dim SC As New SeriesCollection()
        Dim fila As DataRow
        Dim serie As New Series()
        Dim elemento As New Element()
        Dim filas As Integer

        filas = dstPuntosCaudales.Tables("TablaCaudales").Rows.Count
        If filas > 0 Then
            serie.Name = "Lecturas " '& dstPuntosCaudales.Tables("TablaCaudales").Rows(0).Item("Curva")
            For Each fila In dstPuntosCaudales.Tables("TablaCaudales").Rows
                If nullACero(fila("Caudal")) <> 0 Then
                    elemento = New Element()
                    'obtenemos el valor de la curva

                    elemento.XValue = nullACero(fila("altura"))
                    elemento.YValue = nullACero(fila("Caudal"))
                    serie.Elements.Add(elemento)
                    serie.Type = SeriesType.Marker
                End If
            Next fila

            SC.Add(serie)
        End If
        Return SC
    End Function
    Private Sub RefrescarGraficaCaudales(ByVal MiSerieDeDatos As SeriesCollection, ByVal Migrafica As dotnetCHARTING.Chart)
        With Migrafica
            .Type = ChartType.Scatter
            .SeriesCollection.Add(MiSerieDeDatos)
            .TitleBox.Label = New Label(Session("CodigoPVYCR").ToString + " / " + dstNombres.Tables("TablaNombres").Rows(0).Item("DenominacionPunto").ToString + " / " + dstNombres.Tables("TablaNombres").Rows(0).Item("DenominacionCauce").ToString, New Font("Arial", 12.0!, FontStyle.Bold), Color.White)
            .TitleBox.Position = TitleBoxPosition.FullWithLegend
            .TitleBox.CornerTopLeft = BoxCorner.Round
            .TitleBox.CornerTopRight = BoxCorner.Round
            .TitleBox.Background.ShadingEffectMode = ShadingEffectMode.Five
            .TitleBox.Background.Color = Color.DarkKhaki
            .TitleBox.CornerSize = 20

        End With
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Session("TablaCaudales") = Nothing
    End Sub
End Class
