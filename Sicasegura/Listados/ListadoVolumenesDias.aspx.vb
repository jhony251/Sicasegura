Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Diagnostics
'Imports Scripting



Partial Class SICAH_ListadoVolumenesDias
    Inherits System.Web.UI.Page
    Dim sentenciaSel As String

    Private components As System.ComponentModel.IContainer

    Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim da As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Public dst As DataSet = New System.Data.DataSet()

    Dim dstlecturas(3) As DataSet
    Dim dw As DataView = New System.Data.DataView
    Dim dtLecturasOrdenadas(3) As DataTable
    Dim dtLecturasOrdenadasClone(3) As DataTable
    Dim TablaListados As DataTable = New System.Data.DataTable
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Public fechahoy As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RellenaDataSets()
            If dst.Tables("TablaAcequias").Rows.Count > 0 Then
                CrearExcel()
            Else
                JavaScript.Alert(Me, "No hay ningún registro")
            End If
        End If
    End Sub


    Private Sub CrearExcel()
        Dim archivo, plantilla As String
        'Try
        archivo = Server.MapPath("./") & "VolumenesDiarios" & Session.SessionID & ".xls"
            plantilla = Server.MapPath("./") & "Plantilla.xls"

            File.Copy(plantilla, archivo, True)

            Dim xlsap As Excel.Application = New Excel.Application()
            Dim docxls As Excel.Workbook

            docxls = xlsap.Workbooks.Open(archivo)

            Dim oSheet As Excel.Worksheet = xlsap.ActiveSheet

            With xlsap.Application
                .Visible = False
                .Workbooks.Open(archivo)
            End With

            Dim i, j As Integer

            For i = 0 To dst.Tables("TablaAcequias").Rows.Count - 1
                For j = 0 To dst.Tables("TablaAcequias").Columns.Count - 1
                    CType(xlsap.Cells.Item(i + 2, j + 1), Excel.Range).Formula = CType(utiles.nullABlanco(dst.Tables("TablaAcequias").Rows(i).Item(j)), String)
                Next
            Next

            With xlsap.Application
                .ActiveWorkbook.RunAutoMacros(Excel.XlRunAutoMacro.xlAutoOpen)
                .Run("lanzar")
            .ActiveWorkbook.Save()
            .ActiveWorkbook.Close()
        End With

        Dim idproc As Integer = GetIDProcces("EXCEL")

        If idproc <> -1 Then
            Process.GetProcessById(idproc).Kill()
        End If

        Dim elArchivo() As Byte = File.ReadAllBytes(Server.MapPath("./") & "VolumenesDiarios" & Session.SessionID & ".xls")
        Response.ContentType = "application/vnd.ms-excel"
        Response.BinaryWrite(elArchivo)
        File.Delete(Server.MapPath("./") & "VolumenesDiarios" & Session.SessionID & ".xls")

        'Catch ex As Exception
            'JavaScript.Alert(Me, ex.Message)
        'End Try
       
    End Sub

    Private Function GetIDProcces(ByVal nameProcces As String)
        Try
            Dim asProccess() As Process = Process.GetProcessesByName(nameProcces)
            For Each pProccess As Process In asProccess
                If pProccess.MainWindowTitle = "" Then
                    Return pProccess.Id
                End If
            Next
            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function


    Private Sub RellenaDataSets()
        Dim codigoPVYCR As String = Request.QueryString("codigoPVYCR")
        Dim idElementoMedida As String = Request.QueryString("idElementoMedida")
        Dim FiltroNulasQ = Request.QueryString("FiltroNulasQ")

        Dim FiltroFechaIni As String = Request.QueryString("filtroFechaIni")
        Dim FiltroFechaFin As String = Request.QueryString("FiltroFechaFin")
        Dim FiltrarCodFuenteDato As String = Request.QueryString("FiltrarCodFuenteDato")
        Dim FiltroNregQ As String = Request.QueryString("FiltroNregQ")

        'Criterios de filtrado
        Dim sFiltro As String = ""
        Dim fechainicio, fechaFin As DateTime
        Dim Nreg As Integer = 0
        Dim i As Integer = 0
        Dim sentenciaSel As String = "", sentenciaOrder As String = ""

        'dependiendo del tipo seleccionaremos los datos de una tabla u otra
        'calculamos el año hidrológico que va desde el 01/10/añoactual - 1 hasta la fecha actual
        fechainicio = "01/10/" & DateTime.Now.Year - 1
        fechaFin = DateTime.Today

        If FiltroNregQ <> "" Then
            sentenciaSel = "SELECT top " & FiltroNregQ & " d.CodigoPVYCR,d.Fecha_Medida,d.TipoObtencionCaudal, d.Cod_Fuente_Dato,Escala_M,d.Calado_M " & _
                                   ",d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD,d.Observaciones " & _
                                   "FROM PVYCR_DatosAcequias D " & _
                                   "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                                   "idElementoMedida =  '" & idElementoMedida & "' and " & _
                                   "Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59'"
        Else
            sentenciaSel = "SELECT d.CodigoPVYCR,d.Fecha_Medida,d.TipoObtencionCaudal, d.Cod_Fuente_Dato,Escala_M,d.Calado_M " & _
                                   ",d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD,d.Observaciones " & _
                                   "FROM PVYCR_DatosAcequias D " & _
                   "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                   "idElementoMedida =  '" & idElementoMedida & "' and " & _
                   "Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
        End If

        'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
        ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
        If FiltroNulasQ = False Then
            sentenciaSel = sentenciaSel & " and d.caudal_M3S is not null "
        End If

        If (FiltroFechaFin <> "" And FiltroFechaIni <> "") Or FiltrarCodFuenteDato <> "" Then
            If FiltrarCodFuenteDato <> "" Then
                sFiltro = " and d.Cod_Fuente_Dato = '" & FiltrarCodFuenteDato & "'"
            End If
            If FiltroFechaFin <> "" And FiltroFechaIni <> "" Then
                sFiltro = sFiltro & " and Fecha_medida between '" & FiltroFechaIni & "' and '" & FiltroFechaFin & "'"
            End If
        ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
            JavaScript.Alert(Page, "La Fecha Hasta no puede ser nula")
            sFiltro = ""
        ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
            JavaScript.Alert(Page, "La Fecha Desde no puede ser nula")
            sFiltro = ""
        Else
            sFiltro = ""
        End If

        sentenciaOrder = " order by codigoPVYCR, Fecha_Medida"

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
            '            sentenciaSelCount = sentenciaSelCount & sFiltro
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If

        da.SelectCommand.CommandText = sentenciaSel
        'datos acequias
        da.Fill(dst, "TablaAcequias")

        obtenerVolumenDiferencial()
    End Sub


    Protected Sub obtenerVolumenDiferencial()
        'vamos a calcular la diferencia de volúmenes según registros
        'el caso de las acequias es un caso especial y se calcula obteniendo la media del caudal por el tiempo trancurrido entre dos lecturas.
        Dim i As Integer
        Dim v_diferencial, v_caudal, v_caudal_ant, v_caudal_medio, v_diferencial_acum As Decimal
        Dim v_tiempo, v_tiempo_ant As Date
        Dim v_tiempo_medio As Integer


        'deberemos calcular el volumen, siendo éste el caudal por el tiempo
        If dst.Tables("TablaAcequias").Rows.Count > 0 Then
            If Not dst.Tables("TablaAcequias").Columns.Contains("Diferencial") Then
                'añadimos la columna diferencial al dataset
                dst.Tables("TablaAcequias").Columns.Add("Diferencial")
                'dst.Tables("TablaAcequias").Columns.Add("Diferencial_acum")
            End If
            'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
            For i = 0 To dst.Tables("TablaAcequias").Rows.Count - 1

                If i = 0 Then
                    'rellenamos la columna diferencial del dataset con un 0
                    dst.Tables("TablaAcequias").Rows(i).Item("Diferencial") = "0"
                    'dst.Tables("TablaAcequias").Rows(i).Item("Diferencial_acum") = "0"
                Else
                    v_caudal = Convert.ToDecimal(0 & dst.Tables("TablaAcequias").Rows(i).Item("Caudal_M3S").ToString)
                    v_caudal_ant = Convert.ToDecimal(0 & dst.Tables("TablaAcequias").Rows(i - 1).Item("Caudal_M3S").ToString)
                    v_caudal_medio = (v_caudal + v_caudal_ant) / 2
                    'el tiempo deberá estar en segundos
                    v_tiempo = dst.Tables("TablaAcequias").Rows(i).Item("Fecha_medida").ToString
                    v_tiempo_ant = dst.Tables("TablaAcequias").Rows(i - 1).Item("Fecha_medida").ToString
                    v_tiempo_medio = Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60)
                    'lompartimos entre 1000 para pasarlo de a (m3)
                    v_diferencial = (v_caudal_medio * v_tiempo_medio)
                    'calculamos el diferencial acumulado
                    v_diferencial_acum = v_diferencial_acum + v_diferencial
                    'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
                    If utiles.nullABlanco(dst.Tables("TablaAcequias").Rows(i).Item("Caudal_M3S").ToString) = "" Then
                        dst.Tables("TablaAcequias").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.###}", DBNull.Value)
                        'dst.Tables("TablaAcequias").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", DBNull.Value)
                    Else
                        dst.Tables("TablaAcequias").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.###}", v_diferencial)
                        'dst.Tables("TablaAcequias").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                    End If
                End If
            Next
        End If
    End Sub

    Public Function TransformarDecimales(ByVal numero As Object)
        If numero IsNot System.DBNull.Value Then
            numero = Replace(numero, ".", "")
            numero = Replace(numero, ",", ".")
        Else
            numero = 0
        End If

        Return numero
    End Function
End Class
