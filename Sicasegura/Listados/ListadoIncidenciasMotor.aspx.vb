Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports System.Data.SqlClient
Imports System
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Partial Class Listados_ListadoIncidenciasMotor
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_listados"))
    Dim conexion_insert As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim daPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstPuntos As DataSet = New System.Data.DataSet()
    'Dim dstListados As DataSet = New System.Data.DataSet()
    Dim sentenciaSel, sentenciaOrder As String
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion_insert)
    Protected reportgenerator1 As NineRays.Reporting.Components.ReportGenerator = New NineRays.Reporting.Components.ReportGenerator()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            'ncm esto es para que salga directamente en pdf, tb hay que añadir la función rediridir que hay en el aspx
            If Session("informe") = "" Then
                If Request.QueryString("format") Is Nothing Then
                    Response.Redirect(SharpShooterWebViewer1.PdfLink)
                End If
            Else
                SharpShooterWebViewer1.Style.Value = "Z-INDEX: 102; LEFT: 8px; POSITION: absolute;"
            End If

            Dim dstListados As DataSet = New DataSet()
            Dim fechainicio, fechafin As DateTime
            dstListados.Tables.Add("TablaContador")
            dstListados.Tables("TablaContador").Columns.Add(New DataColumn("codigoPVYCR"))
            dstListados.Tables("TablaContador").Columns.Add(New DataColumn("diferencial"))
            dstListados.Tables("TablaContador").Columns.Add(New DataColumn("FechaMedida"))

            'If Request.QueryString("format") Is Nothing Then
            'Session("codigoPVYCR") = Request.QueryString("codigoPVYCR")
            'Session("diferencial") = Request.QueryString("diferencial")
            'End If

            'If conexion_insert.State = ConnectionState.Closed Then conexion_insert.Open()
            utiles.Comprobar_Conexion_BD(Page, conexion_insert)
            fechainicio = "01/10/" & DateTime.Now.Year - 1
            fechafin = DateTime.Today


            '////////////// DIFERENCIAL TOTAL, ES DECIR SACAR LOS NEGATIVOS TENIENDO EN CUENTA LA PRIEMRA Y LA ÚLTIMA LECTURA//////////
            'sentenciaSel = "SELECT distinct codigoPVYCR FROM PVYCR_datosmotores C where Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechafin & " 23:59:59' "


            'daPuntos.SelectCommand.CommandText = sentenciaSel
            ''datos acequias listados
            'daPuntos.Fill(dstPuntos, "TablaPuntos")


            'Dim i As Integer
            'Dim v_diferencial, v_vol_ant, v_vol As Double
            'Dim rw As DataRow
            'For i = 0 To dstPuntos.Tables("TablaPuntos").Rows.Count - 1

            ''para cada punto obtenemos el diferncial
            'sentenciaSel = "select top 1 codigoPVYCR, ISNULL(lecturaContador_M3,0) lecturaContador_M3 from pvycr_datosmotores where codigoPVYCR = '" & _
            'dstPuntos.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechafin & " 23:59:59' order by fecha_medida"
            'daPuntos.SelectCommand.CommandText = sentenciaSel
            'daPuntos.Fill(dstPuntos, "TablaAscendentes")

            'sentenciaSel = "select top 1 codigoPVYCR, ISNULL(lecturaContador_M3,0) lecturaContador_M3 from pvycr_datosmotores where codigoPVYCR = '" & _
            'dstPuntos.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechafin & " 23:59:59' order by fecha_medida desc"
            'daPuntos.SelectCommand.CommandText = sentenciaSel
            'daPuntos.Fill(dstPuntos, "TablaDescendentes")
            'v_diferencial = utiles.nullACero(dstPuntos.Tables("TablaDescendentes").Rows(i).Item("lecturaContador_M3")) - utiles.nullACero(dstPuntos.Tables("TablaAscendentes").Rows(i).Item("lecturaContador_M3"))
            'If v_diferencial < 0 Then
            '    rw = dstListados.Tables("TablaContador").NewRow
            '    rw("codigoPVYCR") = dstPuntos.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR")
            '    rw("diferencial") = v_diferencial
            '    dstListados.Tables("TablaContador").Rows.Add(rw)
            'End If
            'Next
            '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            '//////////////DIFERENCIAL PARCIAL, SACANDO NEGATIVOS COMO EN LAS LECTURAS LOS QUE APARECEN PINTADOS DE ROJO //////////////

            sentenciaSel = "SELECT *, 'Diferencial' FROM PVYCR_datosmotores C where Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechafin & " 23:59:59' " & _
            "and lecturaContador_M3 is not null order by codigoPVYCR, fecha_medida "


            daPuntos.SelectCommand.CommandText = sentenciaSel
            'datos acequias listados
            daPuntos.Fill(dstPuntos, "TablaContador")


            Dim i As Integer
            Dim v_diferencial, v_vol_ant, v_vol As Double
            Dim rw As DataRow
            Dim codigoPVYCR, codigoPVYCR_ant As String
            If dstPuntos.Tables("TablaContador").Rows.Count > 0 Then
                'If Not dstListados.Tables("TablaContador").Columns.Contains("Diferencial") Then
                '    'añadimos la columna diferencial al dataset
                '    dstListados.Tables("TablaContador").Columns.Add("Diferencial")
                '    'dstListados.Tables("TablaContador").Columns.Add("Diferencial_Acum")
                'End If
                'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
                For i = 0 To dstPuntos.Tables("TablaContador").Rows.Count - 1
                    codigoPVYCR = dstPuntos.Tables("TablaContador").Rows(i).Item("codigoPVYCR")
                    If i = 0 Then
                        'rellenamos la columna diferencial del dataset con un 0
                        'dstListados.Tables("TablaContador").Rows.Add()
                        'dstPuntos.Tables("TablaContador").Rows(i).Item("Diferencial") = "0"
                        'dstListados.Tables("TablaContador").Rows(i).Item("Diferencial_Acum") = "0"
                        v_diferencial = 0
                    Else
                        If codigoPVYCR <> codigoPVYCR_ant Then
                            v_diferencial = 0
                            v_vol_ant = 0
                            v_vol = 0
                        End If
                        'comprobamos si existen incidencias
                        'si la incidencia es reseteo o cambio de contador se deberá tomar el valor del campo Reinicio Lectura Volumetrica
                        'si la incidencia es contador averiado o consumo negativo, el valor a tomar será el consumo volumétrico adicional
                        If (dstPuntos.Tables("TablaContador").Rows(i).Item("idincidenciaVolumetrica").ToString = "6") Or _
                            (dstPuntos.Tables("TablaContador").Rows(i).Item("idincidenciaVolumetrica").ToString = "7") Then
                            v_vol = Convert.ToDecimal(0 & dstPuntos.Tables("TablaContador").Rows(i).Item("ReiniciolecturaVolumetrica").ToString)
                            dstPuntos.Tables("TablaContador").Rows(i).Item("LecturaContador_M3") = dstPuntos.Tables("TablaContador").Rows(i).Item("ReiniciolecturaVolumetrica")
                        ElseIf (dstPuntos.Tables("TablaContador").Rows(i).Item("idincidenciaVolumetrica").ToString = "5") Or _
                            (dstPuntos.Tables("TablaContador").Rows(i).Item("idincidenciaVolumetrica").ToString = "8") Then
                            v_vol = Convert.ToDecimal(0 & dstPuntos.Tables("TablaContador").Rows(i).Item("ConsumoVolumetricoAdicional").ToString)
                            dstPuntos.Tables("TablaContador").Rows(i).Item("LecturaContador_M3") = dstPuntos.Tables("TablaContador").Rows(i).Item("ConsumoVolumetricoAdicional")
                        Else
                            v_vol = Convert.ToDecimal(0 & dstPuntos.Tables("TablaContador").Rows(i).Item("LecturaContador_M3").ToString)
                        End If
                        v_diferencial = v_vol - v_vol_ant
                        v_vol_ant = v_vol 'Convert.ToDecimal(0 & dstPuntos.Tables("TablaContador").Rows(i - 1).Item("LecturaContador_M3").ToString)

                        codigoPVYCR_ant = dstPuntos.Tables("TablaContador").Rows(i).Item("codigoPVYCR")
                        'calculamos el diferencial acumulado
                        'v_diferencial_acum = v_diferencial_acum + v_diferencial
                        'añadimos los valores a la tabla
                        If v_diferencial < 0 Then
                            rw = dstListados.Tables("TablaContador").NewRow
                            rw("codigoPVYCR") = dstPuntos.Tables("TablaContador").Rows(i).Item("codigoPVYCR")
                            rw("diferencial") = v_diferencial
                            rw("FechaMedida") = dstPuntos.Tables("TablaContador").Rows(i).Item("Fecha_Medida")
                            dstListados.Tables("TablaContador").Rows.Add(rw)
                        End If
                        'dstListados.Tables("TablaContador").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                        'dstListados.Tables("TablaContador").Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                    End If
                Next
            End If

            'dstListados.Tables("TablaContador").Select("diferencial < 0")

            CType(dstListados, System.ComponentModel.ISupportInitialize).BeginInit()

            Me.reportgenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"Contadores"}, New Object() {dstListados})

            CType(dstListados, System.ComponentModel.ISupportInitialize).EndInit()

            reportgenerator1.LoadTemplate(Server.MapPath("ListadoIncidenciasMotor.rst"))
            Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
            SharpShooterWebViewer1.Source = reportgenerator1
            SharpShooterWebViewer1.ViewMode = NineRays.Reporting.Web.ViewMode.HtmlSinglePage
            'reportgenerator1.LoadTemplate("c:\listado1.rst")
            'reportgenerator1.Prepare()
            'DG.formApli.reportGenerator1.Template.Description = "Informe de " + oc.nombre
            'reportgenerator1.DesignTemplate()
        End If
    End Sub
End Class
