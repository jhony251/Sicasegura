Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial
Imports System.IO
Imports System.Runtime.InteropServices
Imports NineRays.Reporting.DOM
Imports NineRays.Reporting.Export.PDF
Imports NineRays.Reporting.Export.RTF
Imports NineRays.Reporting.Export.Html
Imports NineRays.Reporting.Export.Excel
Imports NineRays.Reporting.Export.CSV

Partial Class SICAH_ListadoCaudales
    Inherits System.Web.UI.Page

    Private components As System.ComponentModel.IContainer
    'Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator
    Private FilePath As String = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\9Rays.Net\Report Sharp-Shooter").GetValue("Reports")

    Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim da As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New System.Data.DataSet()

    'variables para listados NCM
    Dim dstlecturas(3) As DataSet
    Dim dw As DataView = New System.Data.DataView
    Dim dtLecturasOrdenadas(3) As DataTable
    Dim dtLecturasOrdenadasClone(3) As DataTable
    Dim TablaListados As DataTable = New System.Data.DataTable
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(SICAH_ListadoCaudales))
        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)

        RellenaDataSets()

        CType(Me.dst, System.ComponentModel.ISupportInitialize).BeginInit()

        Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSource"}, New Object() {Me.TablaListados})
        CType(Me.dst, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.  
        If Request.QueryString("format") Is Nothing Then
            Session("tipoelem") = Request.QueryString("tipoelem")
            Session("codigoPVYCR") = Request.QueryString("codigoPVYCR")
        End If
        InitializeComponent()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BuildListBox()
        End If
    End Sub

    Private Sub BuildListBox()
        LoadReportTemplate()
    End Sub

    Private Sub LoadReportTemplate()
        reportGenerator1.LoadTemplate(Server.MapPath("ListadoVolumenAcequias.rst"))
        'SharpShooterWebViewer1.Source = reportGenerator1
        Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
    End Sub


    Private Sub RellenaDataSets()
        Dim tipoelem As String = Session("tipoelem")
        Dim codigoPVYCR As String = Session("codigoPVYCR")
        Dim anyofin, anyo As Integer
        Dim veces As Integer = 0

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        'Tenemos que obtener los datos para el año hidrológico actual y para los 2 anteriores
        anyofin = DateTime.Today.Year
        For anyo = anyofin - 3 To anyofin - 1
            crearDatasetsListados(codigoPVYCR, tipoelem, anyo, veces)
            crear_tabla(veces)
            rellenar_tabla(veces)
            veces = veces + 1
        Next
    End Sub

    Private Sub crearDatasetsListados(ByVal codigoPVYCR As String, ByVal tipoelem As String, ByVal anyo As Integer, ByVal veces As Integer)
        Dim j As Integer = 0
        Dim sentenciaSel As String

        sentenciaSel = "SELECT distinct d.CodigoPVYCR,d.idElementoMedida " & _
               "FROM PVYCR_DatosAcequias D " & _
                "where d.Fecha_Medida between '01/10/" & anyo & " 00:00:00' and '30/09/" & anyo + 1 & " 23:59:59' and " & _
                "(d.CodigoPVYCR='" & codigoPVYCR & "') "

        daElementos.SelectCommand.CommandText = sentenciaSel
        'datos acequias
        daElementos.Fill(dstElementos, "TablaComparativa")

        For j = 0 To dstElementos.Tables("TablaComparativa").Rows.Count - 1
            crearDataSets_Elementos_listados(dstElementos.Tables("TablaComparativa").Rows(j).Item("codigoPVYCR").ToString, dstElementos.Tables("TablaComparativa").Rows(j).Item("idElementoMedida").ToString, veces, anyo)
        Next
    End Sub

    Private Sub crearDataSets_Elementos_listados(ByVal codigoPVYCR As String, ByVal idElementoMedida As String, ByVal veces As Integer, ByVal anyo As Integer)
        'Criterios de filtrado
        Dim fechainicio, fechaFin As DateTime
        'Dim Nreg As Integer = 0
        'Dim i As Integer = 0
        'Dim j As Integer = 0

        'calculamos el año hidrológico que va desde el 01/10/añoactual - 1 hasta la fecha actual
        'si el parámetro anyo es diferente al año actual, el año hidrológico será completo, sino será hasta el día actual
        If anyo <> DateTime.Today.Year - 1 Then
            fechainicio = "01/10/" & anyo
            fechaFin = "30/09/" & anyo + 1
        Else
            fechainicio = "01/10/" & anyo
            fechaFin = DateTime.Today
        End If
        'caso 3, mostramos todos los días del año hidrológico, tengan o no lecturas
        lecturasPorDia(codigoPVYCR, idElementoMedida, fechainicio, fechaFin, veces)
    End Sub

    Protected Sub lecturasPorDia(ByVal codigoPVYCR As String, ByVal idelemento As String, ByVal fechainicio As DateTime, ByVal fechafin As DateTime, ByVal veces As Integer)
        Dim sentenciaSelRegistros, sentenciaSel As String
        Dim i As Integer = 0

        dstlecturas(veces) = New System.Data.DataSet()
        'para cada día del año hidrológico, miramos si hay lecturas
        Do While fechainicio < fechafin
            sentenciaSelRegistros = "Select count(*) registros " & _
                    "FROM PVYCR_DatosAcequias " & _
                    "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                    "idElementoMedida = '" & idelemento & "' and " & _
                    "convert(nvarchar(15),Fecha_Medida,103) = '" & fechainicio & "' "

            daElementos.SelectCommand.CommandText = sentenciaSelRegistros
            daElementos.Fill(dstlecturas(veces), "TablaNumReg")
            ' si no hay lecturas, añadimos las columnas a la tabla y rellenamos el código, la fecha, el id elemento medida.
            If dstlecturas(veces).Tables("TablaNumReg").Rows(i).Item("registros").ToString = "0" Then
                If dstlecturas(veces).Tables("TablaComparativa_Volumen") Is Nothing Then
                    dstlecturas(veces).Tables.Add("TablaComparativa_Volumen")
                    dstlecturas(veces).Tables("TablaComparativa_Volumen").Columns.Add("CodigoPVYCR")
                    dstlecturas(veces).Tables("TablaComparativa_Volumen").Columns.Add("idElementoMedida")
                    dstlecturas(veces).Tables("TablaComparativa_Volumen").Columns.Add("Cod_Fuente_Dato")
                    dstlecturas(veces).Tables("TablaComparativa_Volumen").Columns.Add("Fecha_Medida")
                    dstlecturas(veces).Tables("TablaComparativa_Volumen").Columns.Add("Caudal_M3S")
                    'añado a la tabla volumen el nº de registros para después poder comprobar los que no tenían lecturas
                    ' y realizar el cálculo del diferencial
                    dstlecturas(veces).Tables("TablaComparativa_Volumen").Columns.Add("NumReg")
                End If
                'añadimos una fila al final del dataset
                dstlecturas(veces).Tables("TablaComparativa_Volumen").Rows.Add()
                'rellenamos la fila que hemos añadido que será el count - 1
                dstlecturas(veces).Tables("TablaComparativa_Volumen").Rows(dstlecturas(veces).Tables("TablaComparativa_Volumen").Rows.Count - 1).Item("CodigoPVYCR") = codigoPVYCR
                dstlecturas(veces).Tables("TablaComparativa_Volumen").Rows(dstlecturas(veces).Tables("TablaComparativa_Volumen").Rows.Count - 1).Item("idElementoMedida") = idelemento
                dstlecturas(veces).Tables("TablaComparativa_Volumen").Rows(dstlecturas(veces).Tables("TablaComparativa_Volumen").Rows.Count - 1).Item("Fecha_Medida") = fechainicio
                dstlecturas(veces).Tables("TablaComparativa_Volumen").Rows(dstlecturas(veces).Tables("TablaComparativa_Volumen").Rows.Count - 1).Item("NumReg") = "0"
            Else
                'meteremos la información en un dataset.
                sentenciaSel = "Select count(*) NumReg, d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida, " & _
                                "d.Caudal_M3S " & _
                                "FROM PVYCR_DatosAcequias D " & _
                                "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                                "idElementoMedida = '" & idelemento & "' and " & _
                                "convert(nvarchar(15),Fecha_Medida,103) = '" & fechainicio & "' " & _
                                "group by d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida, d.Caudal_M3S " & _
                                "order by fecha_medida"

                daElementos.SelectCommand.CommandText = sentenciaSel
                daElementos.Fill(dstlecturas(veces), "TablaComparativa_Volumen")
            End If

            'avanzamos al siguiente día
            fechainicio = DateAdd(DateInterval.Day, 1, fechainicio)
            i = i + 1
        Loop
        'volcamos la informacion en un datatable
        dtLecturasOrdenadas(veces) = New DataTable
        dtLecturasOrdenadas(veces) = dstlecturas(veces).Tables("TablaComparativa_Volumen")
        obtenerVolumenDiferencial_listados(codigoPVYCR, idelemento, veces)
    End Sub

    Protected Sub obtenerVolumenDiferencial_listados(ByVal codigoPVYCR As String, ByVal idElementoMedida As String, ByVal veces As Integer)
        'vamos a calcular la diferencia de volúmenes según registros
        'el caso de las acequias es un caso especial y se calcula obteniendo la media del caudal por el tiempo trancurrido entre dos lecturas.
        Dim i, j, dias As Integer
        Dim v_vol, v_vol_ant, v_diferencial, v_caudal, v_caudal_ant, v_caudal_medio, v_diferencial_acum, v_diferencial_diario, v_diferencial_sinlec, caudal_sinlec, v_acum_sinlec As Decimal
        Dim v_tiempo, v_tiempo_ant As Date
        Dim v_fecha, v_fecha_ant As String
        Dim v_tiempo_medio As Integer

        'dstListados = New System.Data.DataSet()
        dtLecturasOrdenadasClone(veces) = New DataTable
        v_diferencial_acum = 0
        v_diferencial_diario = 0
        v_diferencial_sinlec = 0
        'deberemos calcular el volumen, siendo éste el caudal por el tiempo
        If dtLecturasOrdenadas(veces).Rows.Count > 0 Then
            If Not dtLecturasOrdenadas(veces).Columns.Contains("Diferencial") Then
                'añadimos la columna diferencial al dataset
                dtLecturasOrdenadas(veces).Columns.Add("Diferencial")
                dtLecturasOrdenadas(veces).Columns.Add("Diferencial_d")
                dtLecturasOrdenadas(veces).Columns.Add("Diferencial_acum")

            End If
            'clonamos la tabla
            dtLecturasOrdenadasClone(veces) = dtLecturasOrdenadas(veces).Clone()
            dias = 0
            For i = 0 To dtLecturasOrdenadas(veces).Rows.Count - 1

                If i = 0 Then
                    'rellenamos la columna diferencial del dataset con un 0
                    dtLecturasOrdenadas(veces).Rows(i).Item("Diferencial") = "0"
                    dtLecturasOrdenadas(veces).Rows(i).Item("Diferencial_acum") = "0"

                    'insertamos la fila en el datarow
                    dtLecturasOrdenadasClone(veces).ImportRow(dtLecturasOrdenadas(veces).Rows(i))
                Else
                    If dtLecturasOrdenadas(veces).Rows(i).Item("NumReg").ToString = "0" Then
                        dias = dias + 1
                    Else
                        v_caudal = Convert.ToDecimal(0 & dtLecturasOrdenadas(veces).Rows(i).Item("Caudal_m3s").ToString)
                        v_caudal_ant = Convert.ToDecimal(0 & dtLecturasOrdenadas(veces).Rows(i - 1).Item("Caudal_m3s").ToString)
                        v_caudal_medio = (v_caudal + v_caudal_ant) / 2.0
                        'el tiempo deberá estar en segundos
                        v_tiempo = dtLecturasOrdenadas(veces).Rows(i).Item("Fecha_medida").ToString
                        v_tiempo_ant = dtLecturasOrdenadas(veces).Rows(i - 1).Item("Fecha_medida").ToString
                        v_tiempo_medio = Convert.ToInt32(DateDiff(DateInterval.Second, v_tiempo_ant, v_tiempo))
                        'lompartimos entre 1000 para pasarlo de (l/s) a (m3)
                        v_diferencial = (v_caudal_medio * v_tiempo_medio)
                        'calculamos el diferencial acumulado
                        v_diferencial_diario = v_diferencial_diario + v_diferencial
                        v_diferencial_acum = v_diferencial_acum + v_diferencial
                        'Si el caudal es nulo, calcularemos el diferencial cogiendo la siguiente lectura que tenga diferencial y
                        'dividiéndo éste entre el número de días obtendremos el diferencial para esas lecturas
                        'el caudal para éstas lecturas será el diferencial obtenido *24horas

                        'compruebo si han habido lecturas sin vacias, que será el caso en el la variable dia sea mayor que cero
                        If dias > 0 Then
                            Dim k As Integer
                            v_diferencial_sinlec = v_diferencial / dias
                            caudal_sinlec = v_diferencial_sinlec / (24 * 3600)
                            For k = i - dias To i - 1
                                v_acum_sinlec = v_acum_sinlec + v_diferencial_sinlec
                                dtLecturasOrdenadas(veces).Rows(k).Item("Diferencial") = String.Format("{0:#,##0.###}", v_diferencial_sinlec)
                                dtLecturasOrdenadas(veces).Rows(k).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", v_acum_sinlec)
                                dtLecturasOrdenadas(veces).Rows(k).Item("Caudal_m3s") = String.Format("{0:#,##0.##}", caudal_sinlec)
                                'como van a ser diferentes días, habrá que insertarlos siempre
                                dtLecturasOrdenadasClone(veces).ImportRow(dtLecturasOrdenadas(veces).Rows(k))
                            Next

                            dias = 0
                        End If
                        dtLecturasOrdenadas(veces).Rows(i).Item("Diferencial") = String.Format("{0:#,##0.###}", v_diferencial)
                        dtLecturasOrdenadas(veces).Rows(i).Item("Diferencial_d") = String.Format("{0:#,##0.###}", v_diferencial_diario)
                        dtLecturasOrdenadas(veces).Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)

                        If i = (dtLecturasOrdenadas(veces).Rows.Count - 1) Then
                            dtLecturasOrdenadasClone(veces).ImportRow(dtLecturasOrdenadas(veces).Rows(i))

                        Else
                            v_fecha = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dtLecturasOrdenadas(veces).Rows(i).Item("Fecha_medida")))
                            v_fecha_ant = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dtLecturasOrdenadas(veces).Rows(i + 1).Item("fecha_medida")))

                            If v_fecha <> v_fecha_ant Then
                                If String.Format("{0:dd/MM/yyyy}", dtLecturasOrdenadas(veces).Rows(0).Item("Fecha_medida")) <> String.Format("{0:dd/MM/yyyy}", dtLecturasOrdenadas(veces).Rows(i).Item("Fecha_medida")) Then
                                    'solo insertamos uno por dia
                                    dtLecturasOrdenadasClone(veces).ImportRow(dtLecturasOrdenadas(veces).Rows(i))
                                    v_diferencial_diario = 0
                                End If
                            End If
                        End If

                    End If

                End If

            Next
        End If
    End Sub

    Public Sub crear_tabla(ByVal veces As Integer)
        'columnas de la tabla
        Dim colcodigo As DataColumn = New DataColumn("CodigoPVYCR" & veces)
        Dim colidElementoMedida As DataColumn = New DataColumn("idelementoMedida" & veces)
        Dim colCod_Fuente_Dato As DataColumn = New DataColumn("cod_Fuente_Dato" & veces)
        Dim colFecha_medida As DataColumn = New DataColumn("Fecha_Medida" & veces)
        Dim colCaudal As DataColumn = New DataColumn("Caudal_m3s" & veces)
        Dim colDiferencial As DataColumn = New DataColumn("Diferencial" & veces)
        Dim colDiferencial_acum As DataColumn = New DataColumn("Diferencial_acum" & veces)

        TablaListados.Columns.Add(colcodigo)
        TablaListados.Columns.Add(colidElementoMedida)
        TablaListados.Columns.Add(colCod_Fuente_Dato)
        TablaListados.Columns.Add(colFecha_medida)
        TablaListados.Columns.Add(colCaudal)
        TablaListados.Columns.Add(colDiferencial)
        TablaListados.Columns.Add(colDiferencial_acum)
    End Sub
    Public Sub rellenar_tabla(ByVal veces As Integer)
        Dim i As Integer = 0
        If veces = 0 Then
            For i = 0 To 365
                TablaListados.Rows.Add()
            Next
        End If
        For i = 0 To dtLecturasOrdenadasClone(veces).Rows.Count - 1
            TablaListados.Rows(i).Item("CodigoPVYCR" & veces) = dtLecturasOrdenadasClone(veces).Rows(i).Item("CodigoPVYCR")
            TablaListados.Rows(i).Item("idelementoMedida" & veces) = dtLecturasOrdenadasClone(veces).Rows(i).Item("idelementoMedida")
            TablaListados.Rows(i).Item("cod_Fuente_Dato" & veces) = dtLecturasOrdenadasClone(veces).Rows(i).Item("cod_Fuente_Dato")

            TablaListados.Rows(i).Item("Fecha_Medida" & veces) = dtLecturasOrdenadasClone(veces).Rows(i).Item("Fecha_Medida")
            TablaListados.Rows(i).Item("Caudal_m3s" & veces) = dtLecturasOrdenadasClone(veces).Rows(i).Item("Caudal_m3s")
            TablaListados.Rows(i).Item("Diferencial" & veces) = dtLecturasOrdenadasClone(veces).Rows(i).Item("Diferencial")
            TablaListados.Rows(i).Item("Diferencial_acum" & veces) = dtLecturasOrdenadasClone(veces).Rows(i).Item("Diferencial_acum")
        Next
    End Sub
End Class
