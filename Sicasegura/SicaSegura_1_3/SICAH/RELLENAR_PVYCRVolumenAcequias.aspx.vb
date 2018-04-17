Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Partial Class RELLENAR_PVYCRVolumenAcequias
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_listados"))
    Dim conexion_insert As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim sentenciaSel, sentenciaOrder As String
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion_insert)


    Protected Sub btnaceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaceptar.Click
        '--------PROCESO QUE GENERA LA INFORMACIÓN MANDADA A JUAN CARLOS EL DÍA 07/04/2008. LA HOJS DE CÁLCULO LA LLAMAMOS -----------'
        '-----------------------------------------"LISTADO VOLUMEN ACEQUIAS"----------------------------------------------------------'

        crearDatasetsAcequias()
        Alert(Page, "fin")
        'rellenaremos un dataset con

    End Sub
    Private Sub crearDatasetsAcequias()
        Dim j As Integer = 0
        'sentenciaSel = "SELECT distinct d.CodigoPVYCR,d.idElementoMedida " & _
        '   "FROM PVYCR_DatosAcequias D " & _
        '    "where d.Fecha_Medida between '01/10/2007 00:00:00' and '07/04/2008 23:59:00' and " & _
        '    "(CodigoPVYCR='CM016P02' OR CodigoPVYCR='CM017P01' OR CodigoPVYCR='CM018P01' OR " & _
        '    "CodigoPVYCR='CM020-001P01' OR CodigoPVYCR='CM020-002P01' OR CodigoPVYCR='VA001P01' OR " & _
        '    "CodigoPVYCR='VA002P30' OR CodigoPVYCR='VA003P03' OR CodigoPVYCR='VA004P04' OR CodigoPVYCR='VA005P02' " & _
        '    "OR CodigoPVYCR='VA006P01' OR CodigoPVYCR='VA008P02' OR CodigoPVYCR='VA009P02' OR CodigoPVYCR='VA010P09' " & _
        '    "OR CodigoPVYCR='VA011P01' OR CodigoPVYCR='VA012P01' OR CodigoPVYCR='VA013P01' OR CodigoPVYCR='VA014P01' " & _
        '    "OR CodigoPVYCR='VA015P01' OR CodigoPVYCR='VA016P01' OR CodigoPVYCR='VA017P01' OR CodigoPVYCR='VA018P01' " & _
        '    "OR CodigoPVYCR='VA019P01' OR CodigoPVYCR='VA020P01' OR CodigoPVYCR='VA021P01' OR CodigoPVYCR='VA022P02' OR " & _
        '    "CodigoPVYCR='VA024P01' OR CodigoPVYCR='VB001P01' OR CodigoPVYCR='VB002P01' OR CodigoPVYCR='VB003P01' OR " & _
        '    "CodigoPVYCR='VB057P01' OR CodigoPVYCR='VB058P01' OR CodigoPVYCR='VB059P01' OR CodigoPVYCR='VB063P01' OR " & _
        '    "CodigoPVYCR='VB068P01' OR CodigoPVYCR='VB071P01' OR CodigoPVYCR='VB075P01' OR CodigoPVYCR='VB076P01' OR " & _
        '    "CodigoPVYCR='VB079P04' OR CodigoPVYCR='VM001P02' OR CodigoPVYCR='VM002P03' OR CodigoPVYCR='VM003P01') "
        '"Cod_fuente_dato = '09' "
        sentenciaSel = "SELECT distinct d.CodigoPVYCR,d.idElementoMedida " & _
               "FROM PVYCR_DatosAcequias D " & _
                "where d.Fecha_Medida between '30/09/2007 23:59:59' and '07/04/2008 23:59:59' and " & _
                "(d.CodigoPVYCR='VA002P30') "

        daElementos.SelectCommand.CommandText = sentenciaSel
        'datos acequias
        daElementos.Fill(dstElementos, "TablaAcequias")
        For j = 0 To dstElementos.Tables("TablaAcequias").Rows.Count - 1
            crearDataSets_Elementos(dstElementos.Tables("TablaAcequias").Rows(j).Item("codigoPVYCR").ToString, dstElementos.Tables("TablaAcequias").Rows(j).Item("idElementoMedida").ToString)
        Next
    End Sub
    Private Sub crearDataSets_Elementos(ByVal codigoPVYCR As String, ByVal idElementoMedida As String)
        'Criterios de filtrado
        Dim fechainicio, fechaFin As DateTime
        Dim Nreg As Integer = 0
        Dim i As Integer = 0

        'dependiendo del tipo seleccionaremos los datos de una tabla u otra
        'calculamos el año hidrológico que va desde el 01/10/añoactual - 1 hasta la fecha actual
        fechainicio = "30/09/" & DateTime.Now.Year - 1
        fechaFin = DateTime.Today

        sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " & _
           ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD " & _
           "FROM PVYCR_DatosAcequias D " & _
           "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
           "idElementoMedida = '" & idElementoMedida & "' and " & _
           "Fecha_Medida between '" & fechainicio & " 23:59:59' and '" & fechaFin & " 23:59:59' "
        '"Cod_fuente_dato = '09' "
        sentenciaOrder = " order by codigoPVYCR, Fecha_Medida"

        sentenciaSel = sentenciaSel & sentenciaOrder
        'Do While fechainicio < fechaFin
        'sentenciaSel = "Select d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " & _
        '        ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD, F.FUENTE_DATOS DescFuenteDato " & _
        '        "FROM PVYCR_DatosAcequias D " & _
        '        "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
        '        "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
        '        "idElementoMedida = '" & idElementoMedida & "' and " & _
        '        "convert(nvarchar(15),Fecha_Medida,103) = '" & fechainicio & "' "

        'meteremos la información en un dataset.
        daElementos.SelectCommand.CommandText = sentenciaSel
        'datos energía
        'daElementos.Fill(dstElementos, (CInt(ucPaginacionE.lblPaginatext) - 1) * ucPaginacionE.pageSize, ucPaginacionE.pageSize, "TablaAlimentacion")
        daElementos.Fill(dstElementos, "TablaAcequias_Volumen")
        'avanzamos al siguiente día
        'comando.CommandText = sentenciaSel
        'If comando.ExecuteNonQuery() <> 0 Then
        obtenerVolumenDiferencial(codigoPVYCR, idElementoMedida)


        'End If

        'fechainicio = DateAdd(DateInterval.Day, 1, fechainicio)
        'Loop



        dstElementos.Tables("TablaAcequias_Volumen").Clear()
    End Sub
    Protected Sub obtenerVolumenDiferencial(ByVal codigoPVYCR As String, ByVal idElementoMedida As String)
        'vamos a calcular la diferencia de volúmenes según registros
        'el caso de las acequias es un caso especial y se calcula obteniendo la media del caudal por el tiempo trancurrido entre dos lecturas.
        Dim i As Integer
        Dim v_vol, v_vol_ant, v_diferencial, v_caudal, v_caudal_ant, v_caudal_medio, v_diferencial_acum, v_diferencial_diario As Decimal
        Dim v_tiempo, v_tiempo_ant, v_fecha, v_fecha_ant As Date
        Dim v_tiempo_medio As Integer

        v_diferencial_acum = 0
        v_diferencial_diario = 0
        'deberemos calcular el volumen, siendo éste el caudal por el tiempo
        If dstElementos.Tables("TablaAcequias_Volumen").Rows.Count > 0 Then
            If Not dstElementos.Tables("TablaAcequias_Volumen").Columns.Contains("Diferencial") Then
                'añadimos la columna diferencial al dataset
                dstElementos.Tables("TablaAcequias_Volumen").Columns.Add("Diferencial")
                dstElementos.Tables("TablaAcequias_Volumen").Columns.Add("Diferencial_d")
                dstElementos.Tables("TablaAcequias_Volumen").Columns.Add("Diferencial_acum")
            End If
            'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
            For i = 0 To dstElementos.Tables("TablaAcequias_Volumen").Rows.Count - 1

                If i = 0 Then
                    'rellenamos la columna diferencial del dataset con un 0
                    dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial") = "0"
                    dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_acum") = "0"
                    obtenerCaudalAcumulado(codigoPVYCR, idElementoMedida, i)
                Else
                    v_caudal = Convert.ToDecimal(0 & dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Caudal_M3S").ToString)
                    v_caudal_ant = Convert.ToDecimal(0 & dstElementos.Tables("TablaAcequias_Volumen").Rows(i - 1).Item("Caudal_M3S").ToString)
                    v_caudal_medio = (v_caudal + v_caudal_ant) / 2.0
                    'el tiempo deberá estar en segundos
                    v_tiempo = dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Fecha_medida").ToString
                    v_tiempo_ant = dstElementos.Tables("TablaAcequias_Volumen").Rows(i - 1).Item("Fecha_medida").ToString
                    v_tiempo_medio = Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60)
                    'lompartimos entre 1000 para pasarlo de (l/s) a (m3)
                    v_diferencial = (v_caudal_medio * v_tiempo_medio)
                    'calculamos el diferencial acumulado
                    v_diferencial_diario = v_diferencial_diario + v_diferencial
                    v_diferencial_acum = v_diferencial_acum + v_diferencial
                    'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
                    If utiles.nullABlanco(dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Caudal_M3S").ToString) = "" Then
                        dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.###}", DBNull.Value)
                        dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_d") = String.Format("{0:#,##0.###}", DBNull.Value)
                        dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", DBNull.Value)
                    Else
                        dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.###}", v_diferencial)
                        dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_d") = String.Format("{0:#,##0.###}", v_diferencial_diario)
                        dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                    End If
                    'If i <> dstElementos.Tables("TablaAcequias_Volumen").Rows.Count - 1 Then
                    If i = (dstElementos.Tables("TablaAcequias_Volumen").Rows.Count - 1) Then
                        obtenerCaudalAcumulado(codigoPVYCR, idElementoMedida, i)

                    Else
                        v_fecha = String.Format("{0:dd/MM/yyyy}", dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Fecha_medida"))
                        v_fecha_ant = String.Format("{0:dd/MM/yyyy}", dstElementos.Tables("TablaAcequias_Volumen").Rows(i + 1).Item("fecha_medida"))

                        If v_fecha <> v_fecha_ant Then
                            'solo insertamos uno por dia
                            obtenerCaudalAcumulado(codigoPVYCR, idElementoMedida, i)
                            v_diferencial_diario = 0
                        End If
                    End If

                    'End If
                End If

            Next
        End If
    End Sub
    Protected Sub obtenerCaudalAcumulado(ByVal codigoPVYCR As String, ByVal idElementoMedida As String, ByVal i As Integer)
        Dim volumen As Decimal
        Dim total As Decimal

        'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
        If dstElementos.Tables("TablaAcequias_Volumen").Rows.Count > 0 Then
            If dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_Acum").ToString() = "" Then
                total = 0
            Else
                volumen = dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_Acum").ToString
                total = Convert.ToDecimal(String.Format("{0:#,##0.##}", volumen))
            End If
            comando.Parameters.Clear()

            comando.CommandText = "insert into PVYCR_DATOSACEQUIASVOLUMEN(CodigoPVYCR,idelementomedida,Cod_fuente_dato,Fecha_medida,caudal_m3s, Diferencial, Diferencial_acum) " & _
              "values " & _
              "(@CodigoPVYCR,@idElementomedida,@Cod_fuente_dato, @Fecha_medida,@caudal_m3s,@diferencial, @Diferencial_acum) "


            comando.Parameters.AddWithValue("CodigoPVYCR", codigoPVYCR)
            comando.Parameters.AddWithValue("idElementoMedida", idElementoMedida)
            comando.Parameters.AddWithValue("cod_Fuente_Dato", dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Cod_fuente_dato").ToString)
            comando.Parameters.AddWithValue("fecha_medida", dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("fecha_medida").ToString)
            If Not IsDBNull(dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("caudal_m3s")) Then
                comando.Parameters.AddWithValue("Caudal_m3s", Convert.ToDecimal(dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("caudal_m3s").ToString))
            Else
                comando.Parameters.AddWithValue("Caudal_m3s", DBNull.Value)

            End If

            If utiles.nullABlanco(dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_d").ToString) = "" Then
                comando.Parameters.AddWithValue("Diferencial", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("Diferencial", Convert.ToDecimal(dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_d").ToString))
            End If
            If utiles.nullABlanco(dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_acum").ToString) = "" Then
                comando.Parameters.AddWithValue("Diferencial_acum", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("Diferencial_acum", Convert.ToDecimal(dstElementos.Tables("TablaAcequias_Volumen").Rows(i).Item("Diferencial_acum").ToString))
            End If

            If conexion_insert.State = ConnectionState.Closed Then conexion_insert.Open()
            comando.ExecuteNonQuery()

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If Not IsPostBack Then
            lblCabecera.Text = genHTML.cabecera(Page)
            lblPestanyas.Text = genHTML.pestanyasMenu(8, "../", Page, Session("idperfil"), Session("usuarioReg"))
        End If
    End Sub
End Class
