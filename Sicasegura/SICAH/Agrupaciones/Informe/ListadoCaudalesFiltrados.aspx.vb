Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports CalculosSica.SICA_FuncionesCalcVolDiferencial
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Imports NineRays.Reporting.Export.PDF
Imports NineRays.Reporting.Export.RTF
Imports NineRays.Reporting.Export.Html
Imports NineRays.Reporting.Export.Excel
Imports NineRays.Reporting.Export.CSV

Partial Class Listados_ListadoComparativaCaudalesAcequia
    Inherits System.Web.UI.Page

    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim conexion_insert As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim da As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New System.Data.DataSet()
    Dim dstAux As DataSet = New System.Data.DataSet()
    Dim sentenciaSel, sentenciaOrder As String
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion_insert)
    Dim indice As Integer = 0
    Dim parametroParaPaginacion As Object
    Dim mensaje_final As String = ""


    Dim dtpuntos As DataTable
    Dim inscripcion As String
    Dim dstdef As DataSet = New System.Data.DataSet()



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    Private Sub InitializeComponent()
        Dim SICA_Log As SicaSegura.SICA_Log = New SicaSegura.SICA_Log()

        Me.components = New System.ComponentModel.Container

        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Listados_ListadoComparativaCaudalesAcequia))
        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)
        inscripcion = Request.QueryString("inscripcion")
        If inscripcion <> "" Then
            RecuperaPuntosatratar()
        End If

        For i As Integer = 0 To dtpuntos.Rows.Count - 1
            If (dtpuntos.Rows(i).Item(2).ToString().Trim() <> "") Then
                Carga_variables_en_sesion(i)
                'dst.Tables.Remove("listadoGlobales")
                CalcularVariablesGlobales()
                crearDatasetsAcequias()
                dstdef = dst
            End If
            
        Next i
        SICA_Log.Descarga_SCV_SICA_informe(Session("ClaveNodoTipo").ToString() & " -- " & Session("intervalo") & " -- " & Session("informe"))
        CType(dst, System.ComponentModel.ISupportInitialize).BeginInit()

        Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSet"}, New Object() {Me.dst})

        CType(dst, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub Carga_variables_en_sesion(ByVal i As Integer)
        If Request.QueryString("format") Is Nothing Then

            inscripcion = Request.QueryString("inscripcion")
            Session("FiltroFechaIni") = Request.QueryString("filtrofechaIni")
            Session("FiltroFechaFin") = Request.QueryString("filtrofechaFin")

            Session("intervalo") = Request.QueryString("filtroIntervalo")
            Session("ClaveNodoTipo") = Request.QueryString("nodoclave")
            Session("ClaveNodoTipo") = dtpuntos.Rows(i).Item(1).ToString().Trim() & ";E;" & dtpuntos.Rows(i).Item(2).ToString().Trim().Substring(0, 1)
            Session("ClaveNodo") = Session("ClaveNodoTipo").Substring(0, Session("ClaveNodoTipo").IndexOf(";"))
            Session("claveTipo") = Session("ClaveNodoTipo").Substring(Session("ClaveNodoTipo").IndexOf(";") + 1, 1)
            Session("nodoTexto") = Request.QueryString("nodotexto")
            Session("nodoTexto") = dtpuntos.Rows(i).Item(1).ToString().Trim() & "-" & dtpuntos.Rows(i).Item(2).ToString().Trim() & "-Volumen"
            Session("nodoclave") = Request.QueryString("nodoclave")
            Session("nodoclave") = Session("ClaveNodoTipo")

            Session("FiltroNulas") = Request.QueryString("FiltroNulas")
            Session("FiltrarCodFuenteDato") = Request.QueryString("FiltrarCodFuenteDato")
            Session("FiltroNreg") = Request.QueryString("FiltroNreg")

            Session("informe") = Request.QueryString("informe")
            Session("LecturasSeleccionadas") = Session("LecturasSeleccionadas")
            Session("reducirLecturas") = Request.QueryString("reducirLecturas")
        End If

    End Sub
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.  
        'If Request.QueryString("format") Is Nothing Then

        '    inscripcion = Request.QueryString("inscripcion")
        '    Session("FiltroFechaIni") = Request.QueryString("filtrofechaIni")
        '    Session("FiltroFechaFin") = Request.QueryString("filtrofechaFin")

        '    Session("intervalo") = Request.QueryString("filtroIntervalo")
        '    Session("ClaveNodoTipo") = Request.QueryString("nodoclave")
        '    Session("ClaveNodo") = Session("ClaveNodoTipo").Substring(0, Session("ClaveNodoTipo").IndexOf(";"))
        '    Session("claveTipo") = Session("ClaveNodoTipo").Substring(Session("ClaveNodoTipo").IndexOf(";") + 1, 1)
        '    Session("nodoTexto") = Request.QueryString("nodotexto")
        '    Session("nodoclave") = Request.QueryString("nodoclave")

        '    Session("FiltroNulas") = Request.QueryString("FiltroNulas")
        '    Session("FiltrarCodFuenteDato") = Request.QueryString("FiltrarCodFuenteDato")
        '    Session("FiltroNreg") = Request.QueryString("FiltroNreg")

        '    Session("informe") = Request.QueryString("informe")
        '    Session("LecturasSeleccionadas") = Session("LecturasSeleccionadas")
        '    Session("reducirLecturas") = Request.QueryString("reducirLecturas")
        'End If
        InitializeComponent()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            BuildListBox()
        End If

        'Esto lo comentaremos cuando queramos sacar un informe en rtf
        If Session("informe") = "" Then
            If Request.QueryString("format") Is Nothing Then
                If Request.QueryString("grafico") = "si" Then
                    Session("dst") = dst
                    'Response.Redirect("GraficoCaudalesFiltrados.aspx")
                    Dim codigoPVYCR As String
                    Dim idElementoMedida As String
                    If dst.Tables("listado").Rows.Count > 0 Then
                        Dim punto As String = dst.Tables("listado").Rows(0).Item("Punto")
                        Dim partes() = Split(punto, "-")
                        codigoPVYCR = Trim(partes(0))
                        idElementoMedida = Trim(partes(1))
                        Response.Redirect("GraficoConsumos.aspx?Cod=" & codigoPVYCR & "&IEM=" & idElementoMedida)
                    Else
                        JavaScript.Alert(Page, "No se puede mostrar el gráfico porque no hay lecturas")
                        Dim strScript As String = "<script>window.close();</script>"
                        Page.ClientScript.RegisterStartupScript(Page.GetType, "cerrarVentana", strScript)
                    End If
                Else
                    Response.Redirect(SharpShooterWebViewer1.PdfLink)
                End If
            End If
        Else
            SharpShooterWebViewer1.Style.Value = "Z-INDEX: 102; LEFT: 8px; POSITION: absolute;"
        End If

        'Esto lo descomentaremos cuando queramos sacar un informe en rtf
        'SharpShooterWebViewer1.Style.Value = "Z-INDEX: 102; LEFT: 8px; POSITION: absolute;"
        
    End Sub

    Private Sub BuildListBox()
        LoadReportTemplate()
    End Sub

    Private Sub LoadReportTemplate()
        Select Case Session("informe")
            Case "xls"
                reportGenerator1.LoadTemplate(Server.MapPath("ListadoCaudalesFiltradosXLS.rst"))
            Case Else
                reportGenerator1.LoadTemplate(Server.MapPath("ListadoCaudalesFiltrados.rst"))
        End Select

        Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
        SharpShooterWebViewer1.Source = reportGenerator1

        Dim doc As Document = reportGenerator1.RenderDocument()
        Dim m As Stream = Response.OutputStream

        'Esto lo descomentaremos cuando queramos sacar un informe en rtf
        'Session("informe") = "rtf"


        Select Case Session("informe")
            Case "XLS"
                Dim xls As ExcelExportFilter = New ExcelExportFilter
                xls.Export(doc, m)
                Page.Response.AddHeader("Content-Disposition", "attachment; filename=ReportPage.xls")
                Page.Response.ContentType = "application/excel"
            Case "PDF"
                Dim pdf As PDFExportFilter = New PDFExportFilter
                pdf.Export(doc, m)
                Page.Response.AddHeader("Content-Disposition", "attachment; filename=ReportPage.pdf")
                Page.Response.ContentType = "application/pdf"
            Case "HTML"
                Page.Response.ContentEncoding = System.Text.Encoding.UTF8
                Dim htm As HtmlExportFilter = New HtmlExportFilter
                htm.Export(doc, m)
            Case "DOC"
                Dim rtf As RTFExportFilter = New RTFExportFilter
                rtf.Export(doc, m)
                Page.Response.ContentEncoding = System.Text.Encoding.UTF8
                Page.Response.ContentType = "application/rtf"
                Page.Response.AddHeader("Content-Disposition", "attachment; filename=ReportPage.rtf")
            Case "CSV"
                Dim csv As CSVExportFilter = New CSVExportFilter
                csv.Separator = " "
                csv.Export(doc, m)
                Page.Response.ContentEncoding = System.Text.Encoding.UTF8
        End Select
    End Sub

    Private Sub crearDatasetsAcequias()
        Dim diferencial As Decimal = 0, diferencial_acum As Decimal = 0
        Dim idCauce As Integer

        If (dst.Tables("A0") Is Nothing) Then
            dst.Tables.Add("A0")
            With dst.Tables("A0").Columns
                .Add(New DataColumn("Fecha_Medida", Type.GetType("System.DateTime")))
                .Add(New DataColumn("Caudal_M3S", Type.GetType("System.Decimal")))
                .Add(New DataColumn("TipoFecha", Type.GetType("System.String")))
                .Add(New DataColumn("LecturaContador_M3", Type.GetType("System.Decimal")))
                .Add(New DataColumn("idincidenciaVolumetrica", Type.GetType("System.Int16")))
                .Add(New DataColumn("ConsumoVolumetricoAdicional", Type.GetType("System.Decimal")))
                .Add(New DataColumn("relacionM3_kwh", Type.GetType("System.Decimal")))
                .Add(New DataColumn("Total_Kwh", Type.GetType("System.Decimal")))
                .Add(New DataColumn("idIncidenciaElectrica", Type.GetType("System.Int16")))
                .Add(New DataColumn("ConsumoElectricoAdicional", Type.GetType("System.Decimal")))
                .Add(New DataColumn("ReiniciolecturaVolumetrica", Type.GetType("System.Decimal")))
                .Add(New DataColumn("Diferencial_acumm3", Type.GetType("System.Decimal")))
                .Add(New DataColumn("total_kvar", Type.GetType("System.Decimal")))
                .Add(New DataColumn("ReiniciolecturaElectrica", Type.GetType("System.Decimal")))
                .Add(New DataColumn("SuministromensualM3", Type.GetType("System.Decimal")))
                .Add(New DataColumn("idContador", Type.GetType("System.String")))
                .Add(New DataColumn("COD_FUENTE_DATO", Type.GetType("System.String")))
            End With
        End If
        If (dst.Tables("A1") Is Nothing) Then
            dst.Tables.Add("A1")
            With dst.Tables("A1").Columns
                .Add(New DataColumn("Fecha_Medida", Type.GetType("System.DateTime")))
                .Add(New DataColumn("Caudal_M3S", Type.GetType("System.Decimal")))
                .Add(New DataColumn("TipoFecha", Type.GetType("System.String")))
                .Add(New DataColumn("LecturaContador_M3", Type.GetType("System.Decimal")))
                .Add(New DataColumn("idincidenciaVolumetrica", Type.GetType("System.Int16")))
                .Add(New DataColumn("ConsumoVolumetricoAdicional", Type.GetType("System.Decimal")))
                .Add(New DataColumn("relacionM3_kwh", Type.GetType("System.Decimal")))
                .Add(New DataColumn("Total_Kwh", Type.GetType("System.Decimal")))
                .Add(New DataColumn("idIncidenciaElectrica", Type.GetType("System.Int16")))
                .Add(New DataColumn("ConsumoElectricoAdicional", Type.GetType("System.Decimal")))
                .Add(New DataColumn("ReiniciolecturaVolumetrica", Type.GetType("System.Decimal")))
                .Add(New DataColumn("total_kvar", Type.GetType("System.Decimal")))
                .Add(New DataColumn("ReiniciolecturaElectrica", Type.GetType("System.Decimal")))
                '.Add(New DataColumn("Diferencial", Type.GetType("System.Decimal")))
                '.Add(New DataColumn("Diferencialm3", Type.GetType("System.Decimal")))
                '.Add(New DataColumn("diferencial_acum", Type.GetType("System.Decimal")))
                '.Add(New DataColumn("Diferencial_acumm3", Type.GetType("System.Decimal")))
                .Add(New DataColumn("SuministromensualM3", Type.GetType("System.Decimal")))
                .Add(New DataColumn("idContador", Type.GetType("System.String")))
                .Add(New DataColumn("COD_FUENTE_DATO", Type.GetType("System.String")))
            End With
        End If

        If (dst.Tables("listado") Is Nothing) Then
            dst.Tables.Add("listado")
            With dst.Tables("listado").Columns
                .Add(New DataColumn("Rama", Type.GetType("System.String")))
                .Add(New DataColumn("DescTipoElem", Type.GetType("System.String")))
                .Add(New DataColumn("Punto", Type.GetType("System.String")))
                .Add(New DataColumn("Fecha_Medida", Type.GetType("System.String")))
                .Add(New DataColumn("Fecha_MedidaF", Type.GetType("System.String")))
                .Add(New DataColumn("Var1Titulo", Type.GetType("System.String")))
                .Add(New DataColumn("Var1Valor", Type.GetType("System.String")))
                .Add(New DataColumn("Var2Titulo", Type.GetType("System.String")))
                .Add(New DataColumn("Var2Valor", Type.GetType("System.String")))
                .Add(New DataColumn("Var3Titulo", Type.GetType("System.String")))
                .Add(New DataColumn("Var3Valor", Type.GetType("System.String")))
                .Add(New DataColumn("Var3Visible", Type.GetType("System.String")))
                .Add(New DataColumn("Var4Titulo", Type.GetType("System.String")))
                .Add(New DataColumn("Var4Valor", Type.GetType("System.String")))
                .Add(New DataColumn("Var4Visible", Type.GetType("System.String")))
                .Add(New DataColumn("Var5Titulo", Type.GetType("System.String")))
                .Add(New DataColumn("Var5Visible", Type.GetType("System.String")))
                .Add(New DataColumn("Var5Valor", Type.GetType("System.String")))
            End With
        End If

        If Session("claveTipo") = "E" Then
            Dim NombreElemento As String
            Dim separadores As Integer = 0
            Dim guiones As Integer = 0
            Dim V() As String
            V = Split(Session("nodotexto"), "-")
            separadores = numeroSeparadores(Session("nodoclave"))
            guiones = numeroGuiones(Session("nodotexto"))
            NombreElemento = Replace(Session("nodotexto"), "<img src='images/distancias.gif' border=0>&nbsp;&nbsp;<font color=#008500>" & Session("nodoclave").Substring(0, Session("nodoclave").Length - 4) & "-", "")

            Session("IdElementoMedida") = NombreElemento.Substring(0, NombreElemento.ToString.IndexOf("-") - 1)
            Session("IdElementoMedida") = V(guiones - 1).ToString

            If (Left(Session("IdElementoMedida"), 1)) <> "H" Then       'Por ahora no lo hacemos para horómetros
                CalculosParaElementoMedida(Session("ClaveNodo"), Session("IdElementoMedida"))
            End If
        Else
            Session("Filtro") = ""
            sentenciaSel = "Select idArbol from PVYCR_arbol where ClaveNodo='" & Replace(Session("ClaveNodoTipo"), ";", "#") & "'"

            daArbol.SelectCommand.CommandText = sentenciaSel
            utiles.Comprobar_Conexion_BD(Page, conexion)
            idCauce = daArbol.SelectCommand.ExecuteScalar()

            'Obtenemos de forma recursiva los id de los hijos

            Dim dstHijosRecursivo As DataSet = New System.Data.DataSet()
            ObtenerHijosEnvolvente(idCauce, dstHijosRecursivo)

            For i As Integer = 0 To dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows.Count - 1
                Session("ClaveNodo") = dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(i).Item("CodigoPVYCR")
                Session("IdElementoMedida") = dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(i).Item("IdElementoMedida")

                If dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(i).Item("Tipo") = "E" Then
                    CalculosParaElementoMedida(Session("ClaveNodo"), Session("IdElementoMedida"))
                End If
            Next
        End If
    End Sub

    Sub CalculosParaElementoMedida(ByVal claveNodo As String, ByVal idElementoMedida As String)
        Dim sFiltro As String = ""
        Dim fechainicio, fechaFin As DateTime
        fechainicio = calculaFinicio()
        fechaFin = calculaFfin()
        
        Dim tipoElem As String = Left(idElementoMedida, 1)
        Dim i As Integer = 0

        'Lo hacemos para todos los tipos de elementos de medida
        Try
            dst.Tables.Remove("TablaAuxiliar")
            dst.Tables("A0").Clear()
            dst.Tables("A1").Clear()
        Catch ex As Exception

        End Try

        '************ Con las nuevas funciones de Nayra ******************
        CrearDatasetDiferencial(tipoElem, claveNodo, idElementoMedida, Page, Session("FiltroNreg"), Session("FiltroNulas"), _
                Session("FiltroFechaFin"), Session("FiltroFechaIni"), Session("FiltrarCodFuentedato"), Session("Filtro"), _
                parametroParaPaginacion, Session("reducirLecturas"), dstAux, sentenciaSel)
        If Not dstAux.Tables("TablaAcequias") Is Nothing Then
            If Not dst.Tables("TablaAuxiliar") Is Nothing Then
                dst.Tables.Remove("TablaAuxiliar")
            End If

            Dim dt As New DataTable
            dt = dstAux.Tables("TablaAcequias").Copy
            dt.TableName = "TablaAuxiliar"
            dst.Tables.Add(dt)
        End If
        If Not dstAux.Tables("TablaAlimentacion") Is Nothing Then
            If Not dst.Tables("TablaAuxiliar") Is Nothing Then
                dst.Tables.Remove("TablaAuxiliar")
            End If

            Dim dt As New DataTable
            dt = dstAux.Tables("TablaAlimentacion").Copy
            dt.TableName = "TablaAuxiliar"
            dst.Tables.Add(dt)
        End If
        If Not dstAux.Tables("TablaMotores") Is Nothing Then
            If Not dst.Tables("TablaAuxiliar") Is Nothing Then
                dst.Tables.Remove("TablaAuxiliar")
            End If

            Dim dt As New DataTable
            dt = dstAux.Tables("TablaMotores").Copy
            dt.TableName = "TablaAuxiliar"
            dst.Tables.Add(dt)
        End If
        If Not dstAux.Tables("TablaHorometros") Is Nothing Then
            If Not dst.Tables("TablaAuxiliar") Is Nothing Then
                dst.Tables.Remove("TablaAuxiliar")
            End If

            Dim dt As New DataTable
            dt = dstAux.Tables("TablaHorometros").Copy
            dt.TableName = "TablaAuxiliar"
            dst.Tables.Add(dt)
        End If
        If Not dstAux.Tables("TablaDiferencial") Is Nothing Then
            If Not dst.Tables("TablaAuxiliar") Is Nothing Then
                dst.Tables.Remove("TablaAuxiliar")
            End If

            Dim dt As New DataTable
            dt = dstAux.Tables("TablaDiferencial").Copy
            dt.TableName = "TablaAuxiliar"
            dst.Tables.Add(dt)
        End If


        'ncm 16/04/2009 si el elemento no es suministro lo dejo como lo tenia inma
        'si es suministro y el listado es mensual parecido a lo de inma
        'si es suministro y anual creo un nuevo dataset
        If tipoElem <> "D" Then
            If tipoElem <> "H" Then
                generarlecturasreales()
                If dst.Tables("A0").Rows.Count > 0 Then
                    generarlecturasIntervalo()
                    ordenarLecturas()
                    obtenerVolumenDiferencial(tipoElem, dst.Tables("A1"), Page, mensaje_final)
                End If
                CalcularTotales()
            End If
        Else
            If Session("intervalo") = "m" Then
                'rellena la tabla A0 con las lecturas a R
                generarlecturasreales()
                If dst.Tables("A0").Rows.Count > 0 Then
                    ' si es mensul ponemos todas las lecturas a I, si es anual sólo cuando cambia el año, ya que las de I son las que se muestran en el informe
                    generarlecturasIntervaloSuministros()
                    'ordena las lecturas por si hay imaginarias (que no es nuestro caso) y rellena la tabla A1
                    ordenarLecturasDiferenciales()
                    obtenerVolumenDiferencial(tipoElem, dst.Tables("A1"), Page, mensaje_final)
                End If
                CalcularTotalesSuministros()
            Else
                CalcularListadoAnualSuministros()
            End If
        End If
    End Sub

    Sub generarlecturasreales()
        'Aquí cogemos los datos que necesitamos de la TablaAuxiliar (la que recoge los datos de la consulta) y lo metemos en la tabla A0
        Dim i As Integer
        dst.Tables("A0").Clear()

        For i = 0 To dst.Tables("TablaAuxiliar").Rows.Count - 1
            dst.Tables("A0").Rows.Add()
            Select Case Left(Session("IdElementoMedida"), 1)
                Case "Q"
                    dst.Tables("A0").Rows(i).Item("Caudal_M3S") = dst.Tables("TablaAuxiliar").Rows(i).Item("Caudal_M3S")
                Case "E"
                    dst.Tables("A0").Rows(i).Item("Total_Kwh") = dst.Tables("TablaAuxiliar").Rows(i).Item("Total_Kwh")
                    dst.Tables("A0").Rows(i).Item("Total_Kvar") = dst.Tables("TablaAuxiliar").Rows(i).Item("Total_Kvar")
                    dst.Tables("A0").Rows(i).Item("idIncidenciaElectrica") = dst.Tables("TablaAuxiliar").Rows(i).Item("idIncidenciaElectrica")
                    dst.Tables("A0").Rows(i).Item("ConsumoElectricoAdicional") = dst.Tables("TablaAuxiliar").Rows(i).Item("ConsumoElectricoAdicional")
                    dst.Tables("A0").Rows(i).Item("relacionM3_kwh") = dst.Tables("TablaAuxiliar").Rows(i).Item("relacionM3_kwh")
                    dst.Tables("A0").Rows(i).Item("ReiniciolecturaElectrica") = dst.Tables("TablaAuxiliar").Rows(i).Item("ReiniciolecturaElectrica")
                    dst.Tables("A0").Rows(i).Item("idContador") = dst.Tables("TablaAuxiliar").Rows(i).Item("idContador")
                    dst.Tables("A0").Rows(i).Item("COD_FUENTE_DATO") = dst.Tables("TablaAuxiliar").Rows(i).Item("COD_FUENTE_DATO")
                Case "H"

                Case "V"
                    dst.Tables("A0").Rows(i).Item("LecturaContador_M3") = dst.Tables("TablaAuxiliar").Rows(i).Item("LecturaContador_M3")
                    dst.Tables("A0").Rows(i).Item("idincidenciaVolumetrica") = dst.Tables("TablaAuxiliar").Rows(i).Item("idincidenciaVolumetrica")
                    dst.Tables("A0").Rows(i).Item("ConsumoVolumetricoAdicional") = dst.Tables("TablaAuxiliar").Rows(i).Item("ConsumoVolumetricoAdicional")
                    dst.Tables("A0").Rows(i).Item("ReiniciolecturaVolumetrica") = dst.Tables("TablaAuxiliar").Rows(i).Item("ReiniciolecturaVolumetrica")
                Case "D"
                    dst.Tables("A0").Rows(i).Item("SuministroMensualM3") = dst.Tables("TablaAuxiliar").Rows(i).Item("SuministroMensualM3")
            End Select

            dst.Tables("A0").Rows(i).Item("Fecha_Medida") = dst.Tables("TablaAuxiliar").Rows(i).Item("Fecha_Medida")
            dst.Tables("A0").Rows(i).Item("TipoFecha") = "R"
        Next
    End Sub

    Sub generarlecturasIntervalo()
        Dim i As Integer = 0
        Dim j As Integer = 0     'Esta variable se usará para contar el número de filas que se añaden
        Dim filasA0 As Integer = dst.Tables("A0").Rows.Count - 1
        Dim fechaAct As Date
        Dim numLectReales As Integer = dst.Tables("A0").Rows.Count

        fechaAct = dst.Tables("A0").Rows(0).Item("Fecha_Medida")

        For i = 0 To filasA0
            If DateDiff(DateInterval.Second, fechaAct, dst.Tables("A0").Rows(i).Item("Fecha_Medida")) = 0 Then
                'Si la diferencia de la fecha calculada (fecha + 1 intervalo)= la fecha que estamos cogiendo, i.e., si la fecha que queremos
                'mostrar existe, simplemente modificamos la columna TipoFecha con una I para mostrarla luego. Mostraremos las I's. 
                'Y le añadimos un intervalo a la fecha que estamos.
                dst.Tables("A0").Rows(i).Item("TipoFecha") = "I"
                fechaAct = calculaFecha(fechaAct)
            Else
                Do While DateDiff(DateInterval.Second, dst.Tables("A0").Rows(i).Item("Fecha_Medida"), fechaAct) < 0
                    'Do While DateDiff(DateInterval.Second, fechaAct, dst.Tables("A0").Rows(i - 1).Item("Fecha_Medida")) < 0 And DateDiff(DateInterval.Second, dst.Tables("A0").Rows(i).Item("Fecha_Medida"), fechaAct) < 0
                    If DateDiff(DateInterval.Second, fechaAct, dst.Tables("A0").Rows(i).Item("Fecha_Medida")) = 0 Then
                        'Si la diferencia de la fecha calculada (fecha + 1 intervalo)= la fecha que estamos cogiendo, i.e., si la fecha que queremos
                        'mostrar existe, simplemente modificamos la columna TipoFecha con una I para mostrarla luego. Mostraremos las I's. 
                        'Y le añadimos un intervalo a la fecha que estamos.
                        dst.Tables("A0").Rows(i).Item("TipoFecha") = "I"
                    Else
                        'Este caso es para cuando no existe la fecha que queremos mostrar, en este caso le añadiremos una fila con la fecha, le pondremos la I para que
                        'sea de las que se muestran y le calcularemos el caudal.
                        'En este caso le añadimos un intervalo también a la fecha que estamos.
                        dst.Tables("A0").Rows.Add()     'Cuando entre aquí tendré que controlar que inserta bien la fila en medio de la tabla
                        j = j + 1
                        dst.Tables("A0").Rows(filasA0 + j).Item("Fecha_Medida") = fechaAct
                        dst.Tables("A0").Rows(filasA0 + j).Item("TipoFecha") = "I"

                        Select Case Left(Session("IdElementoMedida"), 1)
                            Case "Q"
                                'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
                                If utiles.nullABlanco(dst.Tables("A0").Rows(i).Item("Caudal_M3S").ToString) = "" Then
                                    dst.Tables("A0").Rows(filasA0 + j).Item("Caudal_M3S") = System.DBNull.Value
                                Else
                                    'calculamos el caudal de la lectura imaginaria si el siguiente caudal no es nulo
                                    'Le pasamos el índice en el que estamos para poder calcular los valores anteriores y posteriores
                                    If utiles.nullABlanco(dst.Tables("A0").Rows(i).Item("Caudal_M3S").ToString) = "" Then
                                        dst.Tables("A0").Rows(filasA0 + j).Item("Caudal_M3S") = System.DBNull.Value
                                    Else
                                        dst.Tables("A0").Rows(filasA0 + j).Item("Caudal_M3S") = calculaCaudalImaginario(fechaAct, i)
                                    End If
                                End If
                            Case "E"
                                'Si el total es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
                                dst.Tables("A0").Rows(filasA0 + j).Item("relacionM3_kwh") = dst.Tables("A0").Rows(i).Item("relacionM3_kwh")
                                If utiles.nullABlanco(dst.Tables("A0").Rows(i).Item("Total_Kwh").ToString) = "" Then
                                    dst.Tables("A0").Rows(filasA0 + j).Item("Total_Kwh") = System.DBNull.Value
                                    dst.Tables("A0").Rows(filasA0 + j).Item("Total_Kvar") = System.DBNull.Value
                                Else
                                    'calculamos el total de la lectura imaginaria si el siguiente total no es nulo
                                    'Le pasamos el índice en el que estamos para poder calcular los valores anteriores y posteriores
                                    If utiles.nullABlanco(dst.Tables("A0").Rows(i).Item("Total_Kwh").ToString) = "" Then
                                        dst.Tables("A0").Rows(filasA0 + j).Item("Total_Kwh") = System.DBNull.Value
                                        dst.Tables("A0").Rows(filasA0 + j).Item("Total_Kvar") = System.DBNull.Value
                                    Else
                                        dst.Tables("A0").Rows(filasA0 + j).Item("Total_Kwh") = calculaTotalImaginario(fechaAct, i, "Total_Kwh")
                                        dst.Tables("A0").Rows(filasA0 + j).Item("Total_Kvar") = calculaTotalImaginario(fechaAct, i, "Total_Kvar")
                                    End If
                                End If
                            Case "H"
                            Case "V"
                                'Si la lectura del contador es nula pasaremos al registro siguiente y ése no lo tendremos en cuenta
                                If utiles.nullABlanco(dst.Tables("A0").Rows(i).Item("LecturaContador_M3").ToString) = "" Then
                                    'dst.Tables("A0").Rows(filasA0 + j).Item("LecturaContador_M3") = System.DBNull.Value
                                    dst.Tables("A0").Rows(filasA0 + j).Item("LecturaContador_M3") = calculaLecturaImaginaria(fechaAct, i)
                                Else
                                    'calculamos lectura del contador de la lectura imaginaria si la siguiente lectura del contador  no es nulo
                                    'Le pasamos el índice en el que estamos para poder calcular los valores anteriores y posteriores
                                    If utiles.nullABlanco(dst.Tables("A0").Rows(i).Item("LecturaContador_M3").ToString) = "" Then
                                        dst.Tables("A0").Rows(filasA0 + j).Item("LecturaContador_M3") = System.DBNull.Value
                                    Else
                                        dst.Tables("A0").Rows(filasA0 + j).Item("LecturaContador_M3") = calculaLecturaImaginaria(fechaAct, i)
                                    End If
                                End If
                            Case "D"
                                'Si la lectura del contador es nula pasaremos al registro siguiente y ése no lo tendremos en cuenta
                                If utiles.nullABlanco(dst.Tables("A0").Rows(i).Item("SuministroMensualM3").ToString) = "" Then
                                    'dst.Tables("A0").Rows(filasA0 + j).Item("LecturaContador_M3") = System.DBNull.Value
                                    dst.Tables("A0").Rows(filasA0 + j).Item("SuministroMensualM3") = calculaSuministroImaginario(fechaAct, i)
                                Else
                                    'calculamos lectura del contador de la lectura imaginaria si la siguiente lectura del contador  no es nulo
                                    'Le pasamos el índice en el que estamos para poder calcular los valores anteriores y posteriores
                                    If utiles.nullABlanco(dst.Tables("A0").Rows(i).Item("SuministroMensualM3").ToString) = "" Then
                                        dst.Tables("A0").Rows(filasA0 + j).Item("SuministroMensualM3") = System.DBNull.Value
                                    Else
                                        dst.Tables("A0").Rows(filasA0 + j).Item("SuministroMensualM3") = calculaSuministroImaginario(fechaAct, i)
                                    End If
                                End If
                        End Select
                    End If

                    fechaAct = calculaFecha(fechaAct)
                Loop
            End If
        Next

    End Sub

    Private Function calculaFecha(ByVal fechaAct) As DateTime

        Dim fechaNueva As DateTime

        Select Case Session("intervalo").ToString().ToUpper()
            Case "D"
                If Month(DateAdd(DateInterval.Day, 1, fechaAct)) = Month(fechaAct) Then
                    fechaNueva = Day(fechaAct) + 1 & "/" & Month(fechaAct) & "/" & Year(fechaAct) & " 0:00:00"
                Else
                    If Year(DateAdd(DateInterval.Day, 1, fechaAct)) = Year(fechaAct) Then
                        fechaNueva = "1/" & Month(fechaAct) + 1 & "/" & Year(fechaAct) & " 0:00:00"
                    Else
                        fechaNueva = "1/1/" & Year(fechaAct) + 1 & " 0:00:00"
                    End If
                End If
                If fechaAct = dst.Tables("A0").Rows(0).Item("Fecha_Medida") Then
                    fechaAct = fechaNueva
                Else
                    fechaAct = DateAdd(DateInterval.Day, 1, fechaAct)
                End If
            Case "M"
                If Month(fechaAct) <> 12 Then
                    fechaNueva = "1/" & Month(fechaAct) + 1 & "/" & Year(fechaAct)
                Else
                    fechaNueva = "1/1/" & Year(fechaAct) + 1
                End If

                'If fechaAct = dst.Tables("A0").Rows(0).Item("Fecha_Medida") Then
                '    fechaAct = DateAdd(DateInterval.Day, -1, fechaNueva)
                'Else
                fechaAct = fechaNueva
                'End If
            Case "A"
                If fechaAct = dst.Tables("A0").Rows(0).Item("Fecha_Medida") Then
                    fechaAct = "31/12/" & Year(fechaAct)
                Else
                    fechaAct = DateAdd(DateInterval.Year, 1, fechaAct)
                End If
        End Select

        Return fechaAct
    End Function

    Private Function calculaCaudalImaginario(ByVal fechaAct As Date, ByVal j As Integer) As Decimal
        'Calculamos el caudal de las lecturas agregradas en el intervalo de tiempo especificado: 
        'CaudalImaginario=CaudalLecturaAnt + [(CaudalLecturaSig-CaudalLecturaAnt)/(FechaSig-FechaAnt)*(FechaActual-FechaAnt)]
        Dim CaudalImaginario As Decimal

        Dim v_tiempo, v_tiempo_ant As Date
        Dim v_tiempo_medio, v_tiempo_medio_imaginario As Integer
        Dim caudal, caudal_ant As Decimal

        v_tiempo = dst.Tables("A0").Rows(j).Item("Fecha_medida").ToString
        v_tiempo_ant = dst.Tables("A0").Rows(j - 1).Item("Fecha_medida").ToString
        v_tiempo_medio = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60))
        v_tiempo_medio_imaginario = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, fechaAct) * 60))

        caudal = utiles.nullACero(dst.Tables("A0").Rows(j).Item("Caudal_M3S"))
        caudal_ant = utiles.nullACero(dst.Tables("A0").Rows(j - 1).Item("Caudal_M3S"))
        CaudalImaginario = caudal_ant + ((caudal - caudal_ant) / v_tiempo_medio) * v_tiempo_medio_imaginario

        Return Math.Round(CaudalImaginario, 3)

    End Function

    Private Function calculaLecturaImaginaria(ByVal fechaAct As Date, ByVal j As Integer)   '22/03/2011 ipim
        'Calculamos el caudal de las lecturas agregradas en el intervalo de tiempo especificado: 
        'CaudalImaginario=CaudalLecturaAnt + [(CaudalLecturaSig-CaudalLecturaAnt)/(FechaSig-FechaAnt)*(FechaActual-FechaAnt)]
        Dim LecturaImaginaria

        Dim v_tiempo, v_tiempo_ant As Date
        Dim v_tiempo_medio, v_tiempo_medio_imaginario As Integer
        Dim lectura, lectura_ant
        Dim idIncidenciaVolumetrica As Integer

        v_tiempo = dst.Tables("A0").Rows(j).Item("Fecha_medida").ToString
        v_tiempo_ant = dst.Tables("A0").Rows(j - 1).Item("Fecha_medida").ToString
        v_tiempo_medio = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60))
        v_tiempo_medio_imaginario = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, fechaAct) * 60))

        lectura = utiles.nullACero(dst.Tables("A0").Rows(j).Item("LecturaContador_M3"))
        'lectura_ant = utiles.nullACero(dst.Tables("A0").Rows(j - 1).Item("LecturaContador_M3"))
        lectura_ant = dst.Tables("A0").Rows(j - 1).Item("LecturaContador_M3")

        idIncidenciaVolumetrica = utiles.nullACero(dst.Tables("A0").Rows(j).Item("idIncidenciaVolumetrica"))

        If idIncidenciaVolumetrica <> 0 Then
            LecturaImaginaria = lectura_ant
        Else
            If lectura_ant Is System.DBNull.Value Then
                LecturaImaginaria = System.DBNull.Value
            Else
                LecturaImaginaria = lectura_ant + ((lectura - lectura_ant) / v_tiempo_medio) * v_tiempo_medio_imaginario
            End If
        End If

        Return LecturaImaginaria
    End Function

    Private Function calculaTotalImaginario(ByVal fechaAct As Date, ByVal j As Integer, ByVal TipoTotal As String)
        'Calculamos el caudal de las lecturas agregradas en el intervalo de tiempo especificado: 
        'CaudalImaginario=CaudalLecturaAnt + [(CaudalLecturaSig-CaudalLecturaAnt)/(FechaSig-FechaAnt)*(FechaActual-FechaAnt)]
        Dim TotalImaginario

        Dim v_tiempo, v_tiempo_ant As Date
        Dim v_tiempo_medio, v_tiempo_medio_imaginario As Integer
        Dim total, total_ant
        Dim idIncidenciaElectrica As Integer

        v_tiempo = dst.Tables("A0").Rows(j).Item("Fecha_medida").ToString
        v_tiempo_ant = dst.Tables("A0").Rows(j - 1).Item("Fecha_medida").ToString
        v_tiempo_medio = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60))
        v_tiempo_medio_imaginario = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, fechaAct) * 60))

        total = utiles.nullACero(dst.Tables("A0").Rows(j).Item(TipoTotal))
        'total_ant = utiles.nullACero(dst.Tables("A0").Rows(j - 1).Item(TipoTotal))
        total_ant = dst.Tables("A0").Rows(j - 1).Item(TipoTotal)

        idIncidenciaElectrica = utiles.nullACero(dst.Tables("A0").Rows(j).Item("idIncidenciaElectrica"))
        If idIncidenciaElectrica <> 0 Then
            TotalImaginario = total_ant
        Else
            If total_ant Is System.DBNull.Value Then
                TotalImaginario = System.DBNull.Value
            Else
                TotalImaginario = total_ant + ((total - total_ant) / v_tiempo_medio) * v_tiempo_medio_imaginario
            End If
        End If

        Return TotalImaginario

    End Function
    Private Function calculaSuministroImaginario(ByVal fechaAct As Date, ByVal j As Integer) As Decimal
        'NCM calculo para diferenciales

        'Calculamos el caudal de las lecturas agregradas en el intervalo de tiempo especificado: 
        'CaudalImaginario=CaudalLecturaAnt + [(CaudalLecturaSig-CaudalLecturaAnt)/(FechaSig-FechaAnt)*(FechaActual-FechaAnt)]
        Dim LecturaImaginaria As Decimal

        Dim v_tiempo, v_tiempo_ant As Date
        Dim v_tiempo_medio, v_tiempo_medio_imaginario As Integer
        Dim lectura, lectura_ant As Decimal

        v_tiempo = dst.Tables("A0").Rows(j).Item("Fecha_medida").ToString
        v_tiempo_ant = dst.Tables("A0").Rows(j - 1).Item("Fecha_medida").ToString
        v_tiempo_medio = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60))
        v_tiempo_medio_imaginario = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, fechaAct) * 60))

        lectura = utiles.nullACero(dst.Tables("A0").Rows(j).Item("SuministroMensualM3"))
        lectura_ant = utiles.nullACero(dst.Tables("A0").Rows(j - 1).Item("SuministroMensualM3"))
        LecturaImaginaria = lectura_ant + ((lectura - lectura_ant) / v_tiempo_medio) * v_tiempo_medio_imaginario

        Return LecturaImaginaria

    End Function

    Sub ordenarLecturas()
        'La primera lectura no la mostramos (no quedaría bien)

        dst.Tables("A0").Rows(0).Item("TipoFecha") = "R"

        dst.Tables("A0").DefaultView.Sort = "Fecha_Medida"

        For i As Integer = 0 To dst.Tables("A0").DefaultView.Count - 1
            dst.Tables("A1").Rows.Add()
            dst.Tables("A1").Rows(i).Item("Fecha_Medida") = dst.Tables("A0").DefaultView(i).Item("Fecha_Medida")
            dst.Tables("A1").Rows(i).Item("TipoFecha") = dst.Tables("A0").DefaultView(i).Item("TipoFecha")
            Select Case Left(Session("IdElementoMedida"), 1)
                Case "Q"
                    dst.Tables("A1").Rows(i).Item("Caudal_M3S") = dst.Tables("A0").DefaultView(i).Item("Caudal_M3S")
                Case "E"
                    dst.Tables("A1").Rows(i).Item("Total_Kwh") = dst.Tables("A0").DefaultView(i).Item("Total_Kwh")
                    dst.Tables("A1").Rows(i).Item("Total_Kvar") = dst.Tables("A0").DefaultView(i).Item("Total_Kvar")
                    dst.Tables("A1").Rows(i).Item("idIncidenciaElectrica") = dst.Tables("A0").DefaultView(i).Item("idIncidenciaElectrica")
                    dst.Tables("A1").Rows(i).Item("ConsumoElectricoAdicional") = dst.Tables("A0").DefaultView(i).Item("ConsumoElectricoAdicional")
                    dst.Tables("A1").Rows(i).Item("relacionM3_kwh") = dst.Tables("A0").DefaultView(i).Item("relacionM3_kwh")
                    dst.Tables("A1").Rows(i).Item("idContador") = dst.Tables("A0").DefaultView(i).Item("idContador")
                    dst.Tables("A1").Rows(i).Item("cod_fuente_dato") = dst.Tables("A0").DefaultView(i).Item("cod_fuente_dato")
                    dst.Tables("A1").Rows(i).Item("ReiniciolecturaElectrica") = dst.Tables("A0").DefaultView(i).Item("ReiniciolecturaElectrica")
                Case "H"

                Case "V"
                    dst.Tables("A1").Rows(i).Item("LecturaContador_M3") = dst.Tables("A0").DefaultView(i).Item("LecturaContador_M3")
                    dst.Tables("A1").Rows(i).Item("idincidenciaVolumetrica") = dst.Tables("A0").DefaultView(i).Item("idincidenciaVolumetrica")
                    dst.Tables("A1").Rows(i).Item("LecturaContador_M3") = dst.Tables("A0").DefaultView(i).Item("LecturaContador_M3")
                    dst.Tables("A1").Rows(i).Item("ConsumoVolumetricoAdicional") = dst.Tables("A0").DefaultView(i).Item("ConsumoVolumetricoAdicional")
             
            End Select
        Next
        dst.Tables("A1").Rows(dst.Tables("A0").DefaultView.Count - 1).Item("TipoFecha") = "I"
    End Sub

    Sub CalcularTotales()
        Dim i As Integer
        Dim Volumen As Double = 0
        Dim Volumen_acum As Double = 0
        Dim Volumen_acumm3 As Double = 0
        Dim CaudalM As Double = 0
        Dim v_tiempo, v_tiempo_ant As Date
        Dim v_tiempo_medio As Integer
        Dim FechaNueva As Date
        Dim CaudalMin As Double = 0, CaudalMax As Double = 0
        Dim rama As String = "", descripcionP As String = "", descripcionC As String
        Dim fechaMensual As String
        Dim VolumenParcial As Double = 0

        'If conexion_insert.State = ConnectionState.Closed Then conexion_insert.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion_insert)
        comando.CommandText = "Select descripcion from PVYCR_Arbol where tipo='P' and codigoPVYCR='" & Session("ClaveNodo") & "'"
        descripcionP = Replace(comando.ExecuteScalar, Session("ClaveNodo") & " ", "")

        comando.CommandText = "Select descripcion from PVYCR_Arbol where tipo='C' and codigoCauce='" & Left(Session("ClaveNodo"), InStrRev(Session("ClaveNodo"), "P") - 1) & "'"
        descripcionC = Replace(comando.ExecuteScalar, Left(Session("ClaveNodo"), InStrRev(Session("ClaveNodo"), "P") - 1) & " ", "")

        rama = vbCrLf & Session("ClaveNodo") & "-" & Session("IdElementoMedida") & " (" & descripcionC & " - " & descripcionP & ")" & vbCrLf
        Dim DescTipoElem = ""

        Select Case Left(Session("IdElementoMedida"), 1)
            Case "Q"
                DescTipoElem = "DATOS CAUDAL"
            Case "V"
                DescTipoElem = "DATOS CONTADOR VOLUMEN"
            Case "E"
                DescTipoElem = "DATOS CONTADOR ENERGÍA"
            Case "H"
                DescTipoElem = "CONTADOR HORAS"
        End Select
        DescTipoElem = "DATOS DE CONSUMO"
        For i = 0 To dst.Tables("A1").Rows.Count - 1
            If dst.Tables("A1").Rows(i).Item("TipoFecha") = "I" Then
                dst.Tables("listado").Rows.Add()
                dst.Tables("listado").Rows(indice).Item("rama") = rama
                dst.Tables("listado").Rows(indice).Item("DescTipoElem") = DescTipoElem
                dst.Tables("listado").Rows(indice).Item("punto") = Session("ClaveNodo") & "-" & Session("IdElementoMedida")
                dst.Tables("listado").Rows(indice).Item("Fecha_Medida") = "     " & dst.Tables("A1").Rows(i).Item("Fecha_Medida")

                'Para el formateo de la fecha

                Select Case Session("intervalo")
                    Case "D"
                        If Left(Session("IdElementoMedida"), 1) = "Q" Then
                            'Aquí están sacadas las lecturas del día de antes hasta las 23:59:59,por tanto para decir, en este día se ha
                            'consumido tanto, le restamos un día, excepto en el último caso que está lo consumido hasta cierta hora.
                            'dst.Tables("listado").Rows(indice).Item("Caudal") = String.Format("{0:#,##0.000}", dst.Tables("A1").Rows(i).Item("Caudal"))
                        End If
                        If i <> dst.Tables("A1").Rows.Count - 1 Then
                            dst.Tables("listado").Rows(indice).Item("Fecha_MedidaF") = "     " & DateAdd(DateInterval.Day, -1, dst.Tables("A1").Rows(i).Item("Fecha_Medida"))
                        Else
                            FechaNueva = Day(dst.Tables("A1").Rows(i).Item("Fecha_Medida")) & "/" & Month(dst.Tables("A1").Rows(i).Item("Fecha_Medida")) & "/" & Year(dst.Tables("A1").Rows(i).Item("Fecha_Medida"))
                            dst.Tables("listado").Rows(indice).Item("Fecha_MedidaF") = "     " & FechaNueva
                        End If
                    Case "M"
                        If Day(dst.Tables("A1").Rows(i).Item("Fecha_Medida")) = "1" Then
                            If Month(dst.Tables("A1").Rows(i).Item("Fecha_Medida")) = 1 Then
                                fechaMensual = UCase(Left(MonthName(12), 1)) & Mid(MonthName(12), 2) & " " & Year(dst.Tables("A1").Rows(i).Item("Fecha_Medida")) - 1
                            Else
                                fechaMensual = UCase(Left(MonthName(Month(dst.Tables("A1").Rows(i).Item("Fecha_Medida")) - 1), 1)) & Mid(MonthName(Month(dst.Tables("A1").Rows(i).Item("Fecha_Medida")) - 1), 2) & " " & Year(dst.Tables("A1").Rows(i).Item("Fecha_Medida"))
                            End If
                        Else
                            fechaMensual = UCase(Left(MonthName(Month(dst.Tables("A1").Rows(i).Item("Fecha_Medida"))), 1)) & Mid(MonthName(Month(dst.Tables("A1").Rows(i).Item("Fecha_Medida"))), 2) & " " & Year(dst.Tables("A1").Rows(i).Item("Fecha_Medida"))
                        End If
                        dst.Tables("listado").Rows(indice).Item("Fecha_MedidaF") = "     " & fechaMensual
                    Case "A"
                        dst.Tables("listado").Rows(indice).Item("Fecha_MedidaF") = "     " & Year(dst.Tables("A1").Rows(i).Item("Fecha_Medida"))
                End Select

                'Calculamos el caudal medio en el tiempo del intervalo sabiendo el volumen en ese tiempo
                If indice < 1 Then
                    v_tiempo_ant = dst.Tables("A0").Rows(0).Item("Fecha_Medida").ToString
                Else
                    v_tiempo_ant = dst.Tables("listado").Rows(indice - 1).Item("Fecha_Medida").ToString
                End If

                Select Case Left(Session("IdElementoMedida"), 1)
                    Case "Q"
                        v_tiempo = dst.Tables("listado").Rows(indice).Item("Fecha_Medida").ToString
                        v_tiempo_medio = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60))

                        'CaudalM = Math.Round(dst.Tables("A1").Rows(i).Item("diferencial") / v_tiempo_medio, 3)
                        CaudalMin = CalcularCaudal("Min", v_tiempo, v_tiempo_ant)
                        CaudalMax = CalcularCaudal("Max", v_tiempo, v_tiempo_ant)
                        'Esto es para calcular el acumulado en el periodo de tiempo establecido.
                        If indice = 0 Then
                            VolumenParcial = dst.Tables("A1").Rows(i).Item("Diferencial_acum")
                        Else
                            VolumenParcial = dst.Tables("A1").Rows(i).Item("diferencial_acum") - dst.Tables("listado").Rows(indice - 1).Item("Var5Valor")
                        End If
                        CaudalM = Math.Round(VolumenParcial / v_tiempo_medio, 3)


                        dst.Tables("listado").Rows(indice).Item("Var1Titulo") = "Caudal Mínimo Intervalo (Qmin)(m3/s)"
                        dst.Tables("listado").Rows(indice).Item("Var1Valor") = String.Format("{0:#,##0.000}", CaudalMin)
                        dst.Tables("listado").Rows(indice).Item("Var2Titulo") = "Caudal Medio Intervalo (Qi)(m3/s)"
                        dst.Tables("listado").Rows(indice).Item("Var2Valor") = String.Format("{0:#,##0.000}", Replace(CaudalM, "NeuN", "0"))
                        dst.Tables("listado").Rows(indice).Item("Var3Titulo") = "Caudal Máximo Intervalo (Qmax)(m3/s)"
                        dst.Tables("listado").Rows(indice).Item("Var3Valor") = String.Format("{0:#,##0.000}", CaudalMax)
                        dst.Tables("listado").Rows(indice).Item("Var3Visible") = "true"
                        dst.Tables("listado").Rows(indice).Item("Var4Titulo") = "Volumen Parcial (m3)"
                        'dst.Tables("listado").Rows(indice).Item("Var4Valor") = String.Format("{0:#,##0.##}", dst.Tables("A1").Rows(i).Item("diferencial"))                        
                        dst.Tables("listado").Rows(indice).Item("Var4Valor") = String.Format("{0:#,##0.##}", VolumenParcial)
                        dst.Tables("listado").Rows(indice).Item("Var4Visible") = "true"
                        dst.Tables("listado").Rows(indice).Item("Var5Titulo") = "Volumen Acumulado (m3)"
                        dst.Tables("listado").Rows(indice).Item("Var5Valor") = String.Format("{0:#,##0.##}", dst.Tables("A1").Rows(i).Item("diferencial_acum"))
                        dst.Tables("listado").Rows(indice).Item("Var5Visible") = "true"

                    Case "E"
                        If indice = 0 Then
                            VolumenParcial = dst.Tables("A1").Rows(i).Item("Diferencial_acumm3")
                        Else
                            VolumenParcial = dst.Tables("A1").Rows(i).Item("diferencial_acumm3") - dst.Tables("listado").Rows(indice - 1).Item("Var5Valor")
                        End If

                        dst.Tables("listado").Rows(indice).Item("Var1Titulo") = "Lectura contador reactiva (Kvar)"
                        dst.Tables("listado").Rows(indice).Item("Var1Valor") = String.Format("{0:#,##0.000}", Math.Round(utiles.nullACero(dst.Tables("A1").Rows(i).Item("Total_Kvar"))))
                        dst.Tables("listado").Rows(indice).Item("Var2Titulo") = "Lectura contador activa (Kw/h)"
                        dst.Tables("listado").Rows(indice).Item("Var2Valor") = String.Format("{0:#,##0.000}", Math.Round(utiles.nullACero(dst.Tables("A1").Rows(i).Item("Total_Kwh"))))
                        dst.Tables("listado").Rows(indice).Item("Var3Titulo") = "Parcial energía activa (Kw/h)"
                        dst.Tables("listado").Rows(indice).Item("Var3Valor") = String.Format("{0:#,##0.##}", dst.Tables("A1").Rows(i).Item("Diferencial_acum"))
                        dst.Tables("listado").Rows(indice).Item("Var3Visible") = "true"
                        dst.Tables("listado").Rows(indice).Item("Var4Titulo") = "Volumen calculado parcial (m3)"
                        dst.Tables("listado").Rows(indice).Item("Var4Valor") = String.Format("{0:#,##0.##}", VolumenParcial)
                        dst.Tables("listado").Rows(indice).Item("Var4Visible") = "true"
                        dst.Tables("listado").Rows(indice).Item("Var5Titulo") = "Volumen calculado acumulado (m3)"
                        dst.Tables("listado").Rows(indice).Item("Var5Valor") = String.Format("{0:#,##0.##}", dst.Tables("A1").Rows(i).Item("Diferencial_acumm3"))
                        dst.Tables("listado").Rows(indice).Item("Var5Visible") = "true"
                    Case "H"
                    Case "V"
                        If indice = 0 Then
                            VolumenParcial = dst.Tables("A1").Rows(i).Item("Diferencial_Acum")
                        Else
                            VolumenParcial = dst.Tables("A1").Rows(i).Item("Diferencial_Acum") - dst.Tables("listado").Rows(indice - 1).Item("Var3Valor")
                            If VolumenParcial < 0 Then
                                VolumenParcial = dst.Tables("A1").Rows(i).Item("Diferencial_Acum")
                            End If
                        End If
                        dst.Tables("listado").Rows(indice).Item("Var1Titulo") = "Volumen (m3)"
                        dst.Tables("listado").Rows(indice).Item("Var1Valor") = String.Format("{0:#,##0.000}", utiles.nullACero(dst.Tables("A1").Rows(i).Item("LecturaContador_M3")))       '22/03/2011 ipim 
                        dst.Tables("listado").Rows(indice).Item("Var2Titulo") = "Volumen parcial (m3)"
                        dst.Tables("listado").Rows(indice).Item("Var2Valor") = String.Format("{0:#,##0.##}", VolumenParcial)
                        dst.Tables("listado").Rows(indice).Item("Var3Titulo") = "Volumen acumulado (m3)"
                        dst.Tables("listado").Rows(indice).Item("Var3Valor") = String.Format("{0:#,##0.##}", dst.Tables("A1").Rows(i).Item("Diferencial_Acum"))
                        dst.Tables("listado").Rows(indice).Item("Var3Visible") = "true"
                        dst.Tables("listado").Rows(indice).Item("Var4Titulo") = ""
                        dst.Tables("listado").Rows(indice).Item("Var4Valor") = ""
                        dst.Tables("listado").Rows(indice).Item("Var4Visible") = "false"
                        dst.Tables("listado").Rows(indice).Item("Var5Titulo") = ""
                        dst.Tables("listado").Rows(indice).Item("Var5Valor") = ""
                        dst.Tables("listado").Rows(indice).Item("Var5Visible") = "false"
                End Select

                Volumen = 0
                indice = indice + 1
            End If
        Next

    End Sub

    Protected Sub ObtenerHijosEnvolvente(ByVal idarbol As Integer, ByRef dstHijosRecursivo As DataSet)
        Dim daHijos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstHijos As DataSet = New System.Data.DataSet()
        'variables para obtener envolvente
        Dim dstHijosRecursivo2 As DataSet = New System.Data.DataSet()
        Dim i As Integer

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = "Select idArbol, Tipo, codigoPVYCR, IdElementoMedida, Descripcion from PVYCR_arbol where idArbolPadre = " & idarbol
        daHijos.SelectCommand.CommandText = sentenciaSel
        daHijos.Fill(dstHijos, "TablaHijos")

        Dim numfilas As Integer
        If Not dstHijosRecursivo.Tables.Contains("TablaHijosRecursivo") Then
            dstHijosRecursivo.Tables.Add("TablaHijosRecursivo")
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Columns.Add(New DataColumn("idArbol"))
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Columns.Add(New DataColumn("Tipo"))
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Columns.Add(New DataColumn("CodigoPVYCR"))
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Columns.Add(New DataColumn("IdElementoMedida"))
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Columns.Add(New DataColumn("Descripcion"))
            numfilas = 0
        End If

        For i = 0 To dstHijos.Tables("TablaHijos").Rows.Count - 1
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows.Add()
            numfilas = dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows.Count
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("idArbol") = dstHijos.Tables("TablaHijos").Rows(i).Item("idArbol")
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("Tipo") = dstHijos.Tables("TablaHijos").Rows(i).Item("Tipo")
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("CodigoPVYCR") = dstHijos.Tables("TablaHijos").Rows(i).Item("CodigoPVYCR")
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("IdElementoMedida") = dstHijos.Tables("TablaHijos").Rows(i).Item("IdElementoMedida")
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("Descripcion") = dstHijos.Tables("TablaHijos").Rows(i).Item("Descripcion")
            ObtenerHijosEnvolvente(dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("idArbol").ToString, dstHijosRecursivo)
        Next
    End Sub
    Private Sub CalcularVariablesGlobales()

        Dim Filtro1, Filtro2 As String
        Dim Filtro As String
        Dim longitud As Integer

        Select Case Session("intervalo")
            Case "d"
                Filtro1 = "Periodo: Diario" & vbCrLf
            Case "m"
                Filtro1 = "Periodo: Mensual" & vbCrLf
            Case "a"
                Filtro1 = "Periodo: Anual" & vbCrLf
        End Select

        If Session("FiltroFechaIni") <> "" And Session("FiltroFechaFin") <> "" Then
            Filtro1 = Filtro1 & "Fecha: Desde " & Session("FiltroFechaIni") & " hasta " & Session("FiltroFechaFin") & vbCrLf
        End If

        If LCase(Session("FiltroNulas")) = "true" Then
            Filtro1 = Filtro1 & "Mostrar nulos: Sí" & vbCrLf
        Else
            Filtro1 = Filtro1 & "Mostrar nulos: No" & vbCrLf
        End If

        'If Session("FiltroNreg") <> "100 PERCENT " Then
        '    Filtro1 = Filtro1 & "Número de registros: " & Session("FiltroNreg") & vbCrLf
        'End If

        If Session("Filtro") <> "" Then
            longitud = Session("Filtro").ToString.Length

            If InStr(Session("Filtro"), "D.Fecha_Medida") > 0 Then
                Filtro = Mid(Session("Filtro"), InStr(Session("Filtro"), "D.Fecha_Medida between '") + 24, 10)
                Filtro1 = Filtro1 & "Fecha de medida: " & Filtro & vbCrLf
            End If

            If InStr(Session("Filtro"), "D.Escala_M") > 0 Then
                Filtro = Mid(Session("Filtro"), InStr(Session("Filtro"), "D.Escala_M =") + 12, longitud - InStr(Session("Filtro"), "D.Escala_M =") - 11)
                If InStr(Filtro, " ") > 0 Then
                    Filtro = Left(Filtro, InStr(Filtro, " "))
                End If
                Filtro1 = Filtro1 & "Escala: " & Filtro & vbCrLf
            End If

            If InStr(Session("Filtro"), "D.TipoObtencionCaudal") > 0 Then
                Filtro = Mid(Session("Filtro"), InStr(Session("Filtro"), "D.TipoObtencionCaudal like '") + 28, longitud - InStr(Session("Filtro"), "D.TipoObtencionCaudal like '") - 27)
                If InStr(Filtro, "'") > 0 Then
                    Filtro = Left(Filtro, InStr(Filtro, "'") - 1)
                End If
                'Cambiamos las siglas por su significado
                Select Case Filtro
                    Case "A"
                        Filtro = "Molinete"
                    Case "F"
                        Filtro = "Flotador"
                    Case "C"
                        Filtro = "Curva de gasto"
                    Case "Q"
                        Filtro = "Caudalímetro"
                    Case "E"
                        Filtro = "Estimado"
                End Select

                Filtro1 = Filtro1 & "Tipo Obtención Caudal: " & Filtro
            End If

            If InStr(Session("Filtro"), "D.RegimenCurva") > 0 Then
                Filtro = Mid(Session("Filtro"), InStr(Session("Filtro"), "D.RegimenCurva like '") + 21, longitud - InStr(Session("Filtro"), "D.RegimenCurva like '") - 20)
                If InStr(Filtro, "'") > 0 Then
                    Filtro = Left(Filtro, InStr(Filtro, "'") - 1)
                End If
                Filtro2 = "Régimen de Curva: " & Filtro & vbCrLf
            End If

            If InStr(Session("Filtro"), "D.NumeroParada") > 0 Then
                Filtro = Mid(Session("Filtro"), InStr(Session("Filtro"), "D.NumeroParada=") + 15, longitud - InStr(Session("Filtro"), "D.NumeroParada=") - 14)
                If InStr(Filtro, " ") > 0 Then
                    Filtro = Left(Filtro, InStr(Filtro, " ") - 1)
                End If
                Filtro2 = Filtro2 & "Número de Parada: " & Filtro & vbCrLf
            End If

            If InStr(Session("Filtro"), "D.Caudal_M3S") > 0 Then
                Filtro = Mid(Session("Filtro"), InStr(Session("Filtro"), "D.Caudal_M3S = ") + 15, longitud - InStr(Session("Filtro"), "D.Caudal_M3S = ") - 14)
                If InStr(Filtro, " ") > 0 Then
                    Filtro = Left(Filtro, InStr(Filtro, " ") - 1)
                End If
                Filtro2 = Filtro2 & "Caudal: " & Filtro & vbCrLf
            End If

            If InStr(Session("Filtro"), "D.Duda_Calidad") > 0 Then
                Filtro = Mid(Session("Filtro"), InStr(Session("Filtro"), "D.Duda_Calidad=") + 15, longitud - InStr(Session("Filtro"), "D.Caudal_M3S = ") - 14)
                If InStr(Filtro, " ") > 0 Then
                    Filtro = Left(Filtro, InStr(Filtro, " ") - 1)
                End If
                Filtro2 = Filtro2 & "Duda de Calidad: " & Filtro & vbCrLf
            End If

            If InStr(Session("Filtro"), "D.Observaciones") > 0 Then
                Filtro = Mid(Session("Filtro"), InStr(Session("Filtro"), "D.Observaciones like '") + 22, longitud - InStr(Session("Filtro"), "D.Observaciones like '") - 21)
                If InStr(Filtro, "'") > 0 Then
                    Filtro = Left(Filtro, InStr(Filtro, "'") - 1)
                End If
                Filtro2 = Filtro2 & "Observaciones: " & Filtro & vbCrLf
            End If

            If InStr(Session("Filtro"), "D.Cod_Fuente_Dato") > 0 Then
                Filtro = Mid(Session("Filtro"), InStr(Session("Filtro"), "D.Cod_Fuente_Dato like '") + 24, longitud - InStr(Session("Filtro"), "D.Cod_Fuente_Dato like '") - 23)
                If InStr(Filtro, "'") > 0 Then
                    Filtro = Left(Filtro, InStr(Filtro, "'") - 1)
                End If
                'If conexion_insert.State = ConnectionState.Closed Then conexion_insert.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion_insert)
                comando.CommandText = "Select FUENTE_DATOS from FUENTES_DE_DATOS where Cod_Fuente_Dato='" & Filtro & "'"
                Filtro = comando.ExecuteScalar
                Filtro2 = Filtro2 & "Código Fuente Dato: " & Filtro & vbCrLf
            End If

            If InStr(Session("Filtro"), "D.Calado_M") > 0 Then
                Filtro = Mid(Session("Filtro"), InStr(Session("Filtro"), "D.Calado_M =") + 12, longitud - InStr(Session("Filtro"), "D.Calado_M =") - 11)
                If InStr(Filtro, " ") > 0 Then
                    Filtro = Left(Filtro, InStr(Filtro, " ") - 1)
                End If
                Filtro2 = Filtro2 & "Calado: " & Filtro & vbCrLf
            End If
        End If

        If (dst.Tables("listadoGlobales") Is Nothing) Then
            dst.Tables.Add("listadoGlobales")
            dst.Tables("listadoGlobales").Columns.Add(New DataColumn("Filtro1", Type.GetType("System.String")))
            dst.Tables("listadoGlobales").Columns.Add(New DataColumn("Filtro2", Type.GetType("System.String")))
            dst.Tables("listadoGlobales").Columns.Add(New DataColumn("Nodo", Type.GetType("System.String")))
            dst.Tables("listadoGlobales").Columns.Add(New DataColumn("NumPags", Type.GetType("System.String")))
        End If

        dst.Tables("listadoGlobales").Rows.Add()
        dst.Tables("listadoGlobales").Rows(0).Item("Filtro1") = Filtro1
        dst.Tables("listadoGlobales").Rows(0).Item("Filtro2") = Filtro2
        dst.Tables("listadoGlobales").Rows(0).Item("Nodo") = "Inscripción en el RAACS Nº " & inscripcion
        dst.Tables("listadoGlobales").Rows(0).Item("NumPags") = SharpShooterWebViewer1.PagesText
        If dst.Tables("listadoGlobales").Rows.Count > 1 Then
            dst.Tables("listadoGlobales").Rows.RemoveAt(dst.Tables("listadoGlobales").Rows.Count - 1)
        End If
    End Sub

    Private Function CalcularCaudal(ByVal tipo As String, ByVal fecha As DateTime, ByVal fecha_ant As DateTime) As Double
        Dim strOrden As String = ""
        Dim CaudalMed As Double = 0

        Select Case tipo
            Case "Max"
                strOrden = " DESC"
            Case Else
        End Select
        Dim dv As DataView = New DataView(dst.Tables("A1"), _
                                        "Fecha_Medida>='" & fecha_ant & "' and Fecha_Medida<='" & fecha & "'", _
                                        "Caudal_M3S" & strOrden, _
                                        DataViewRowState.CurrentRows)

        If dv.Count > 0 Then
            Select Case tipo
                Case "Med"
                    For i As Integer = 0 To dv.Count - 1
                        CaudalMed = CaudalMed + utiles.nullACero(dv(i).Item("Caudal_M3S"))
                    Next
                    Return CaudalMed / dv.Count
                Case Else
                    Return utiles.nullACero(dv(0).Item("Caudal_M3S"))
            End Select
        Else
            Return 0
        End If
    End Function
    Sub ordenarLecturasDiferenciales()

        dst.Tables("A0").DefaultView.Sort = "Fecha_Medida"

        For i As Integer = 0 To dst.Tables("A0").DefaultView.Count - 1
            dst.Tables("A1").Rows.Add()
            dst.Tables("A1").Rows(i).Item("Fecha_Medida") = dst.Tables("A0").DefaultView(i).Item("Fecha_Medida")
            dst.Tables("A1").Rows(i).Item("TipoFecha") = dst.Tables("A0").DefaultView(i).Item("TipoFecha")
          
            dst.Tables("A1").Rows(i).Item("suministroMensualM3") = dst.Tables("A0").DefaultView(i).Item("suministroMensualM3")

        Next

    End Sub
    Sub CalcularTotalesSuministros()
        Dim i As Integer
        Dim Volumen As Double = 0
        Dim Volumen_acum As Double = 0
        Dim Volumen_acumm3 As Double = 0
        Dim CaudalM As Double = 0
        Dim v_tiempo, v_tiempo_ant As Date
        Dim v_tiempo_medio As Integer
        Dim FechaNueva As Date
        Dim CaudalMin As Double = 0, CaudalMax As Double = 0
        Dim rama As String = "", descripcionP As String = "", descripcionC As String
        Dim fechaMensual As String

        'If conexion_insert.State = ConnectionState.Closed Then conexion_insert.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion_insert)
        comando.CommandText = "Select descripcion from PVYCR_Arbol where tipo='P' and codigoPVYCR='" & Session("ClaveNodo") & "'"
        descripcionP = Replace(comando.ExecuteScalar, Session("ClaveNodo") & " ", "")

        comando.CommandText = "Select descripcion from PVYCR_Arbol where tipo='C' and codigoCauce='" & Left(Session("ClaveNodo"), InStrRev(Session("ClaveNodo"), "P") - 1) & "'"
        descripcionC = Replace(comando.ExecuteScalar, Left(Session("ClaveNodo"), InStrRev(Session("ClaveNodo"), "P") - 1) & " ", "")

        rama = vbCrLf & Session("ClaveNodo") & "-" & Session("IdElementoMedida") & " (" & descripcionC & " - " & descripcionP & ")" & vbCrLf
        Dim DescTipoElem = "DATOS DIFERENCIALES"


        For i = 0 To dst.Tables("A1").Rows.Count - 1

            If Left(Session("IdElementoMedida"), 1) = "E" Then
                Volumen = Volumen + utiles.nullACero(dst.Tables("A1").Rows(i).Item("Diferencialm3"))
                Volumen_acum = Volumen_acum + utiles.nullACero(dst.Tables("A1").Rows(i).Item("Diferencial"))
                Volumen_acumm3 = Volumen_acumm3 + utiles.nullACero(dst.Tables("A1").Rows(i).Item("Diferencialm3"))
            Else
                Volumen = Volumen + utiles.nullACero(dst.Tables("A1").Rows(i).Item("Diferencial"))
                Volumen_acum = Volumen_acum + utiles.nullACero(dst.Tables("A1").Rows(i).Item("Diferencial"))
            End If



            If dst.Tables("A1").Rows(i).Item("TipoFecha") = "I" Then
                dst.Tables("listado").Rows.Add()
                dst.Tables("listado").Rows(indice).Item("rama") = rama
                dst.Tables("listado").Rows(indice).Item("DescTipoElem") = DescTipoElem
                dst.Tables("listado").Rows(indice).Item("punto") = Session("ClaveNodo") & "-" & Session("IdElementoMedida")
                dst.Tables("listado").Rows(indice).Item("Fecha_Medida") = "     " & dst.Tables("A1").Rows(i).Item("Fecha_Medida")

                'Para el formateo de la fecha

                Select Case Session("intervalo")
                  
                    Case "m"
                        fechaMensual = UCase(Left(MonthName(Month(dst.Tables("A1").Rows(i).Item("Fecha_Medida"))), 1)) & Mid(MonthName(Month(dst.Tables("A1").Rows(i).Item("Fecha_Medida"))), 2) & " " & Year(dst.Tables("A1").Rows(i).Item("Fecha_Medida"))

                End Select

                'Calculamos el caudal medio en el tiempo del intervalo sabiendo el volumen en ese tiempo
                If indice <= 1 Then
                    v_tiempo_ant = dst.Tables("A0").Rows(0).Item("Fecha_Medida").ToString
                Else
                    v_tiempo_ant = dst.Tables("listado").Rows(indice - 1).Item("Fecha_Medida").ToString
                End If
                dst.Tables("listado").Rows(indice).Item("Fecha_MedidaF") = "     " & fechaMensual
                dst.Tables("listado").Rows(indice).Item("Var1Titulo") = "Suministro Mensual (m3)"
                dst.Tables("listado").Rows(indice).Item("Var1Valor") = String.Format("{0:#,##0}", utiles.nullACero(dst.Tables("A1").Rows(i).Item("SuministromensualM3")))
                dst.Tables("listado").Rows(indice).Item("Var2Titulo") = "Volumen acumulado (m3)"
                dst.Tables("listado").Rows(indice).Item("Var2Valor") = String.Format("{0:#,##0.##}", Math.Round(Volumen))
                dst.Tables("listado").Rows(indice).Item("Var3Titulo") = "" '"Volumen acumulado (m3)"
                dst.Tables("listado").Rows(indice).Item("Var3Valor") = "" 'String.Format("{0:#,##0.##}", utiles.nullACero(dst.Tables("A1").Rows(i - 1).Item("Diferencial_acum"))) 'Math.Round(Volumen_acum))
                dst.Tables("listado").Rows(indice).Item("Var3Visible") = "false"
                dst.Tables("listado").Rows(indice).Item("Var4Titulo") = ""
                dst.Tables("listado").Rows(indice).Item("Var4Valor") = ""
                dst.Tables("listado").Rows(indice).Item("Var4Visible") = "false"
                dst.Tables("listado").Rows(indice).Item("Var5Titulo") = ""
                dst.Tables("listado").Rows(indice).Item("Var5Valor") = ""
                dst.Tables("listado").Rows(indice).Item("Var5Visible") = "false"


                Volumen = 0
                indice = indice + 1
            End If
        Next

    End Sub
    Sub generarlecturasIntervaloSuministros()
        Dim i As Integer = 0
        Dim j As Integer = 0     'Esta variable se usará para contar el número de filas que se añaden
        Dim filasA0 As Integer = dst.Tables("A0").Rows.Count - 1
        Dim numLectReales As Integer = dst.Tables("A0").Rows.Count
        Dim anyo As String = Year(dst.Tables("A0").Rows(0).Item("Fecha_Medida"))
        Dim vol As Integer = 0

        dst.Tables("A0").Rows(0).Item("TipoFecha") = "I"
        vol = dst.Tables("A0").Rows(0).Item("SuministromensualM3")
        For i = 1 To filasA0

            Select Case Session("intervalo")
                Case "m"
                    dst.Tables("A0").Rows(i).Item("TipoFecha") = "I"
                Case "a"
                    If anyo <> Year(dst.Tables("A0").Rows(i).Item("Fecha_Medida")) Then
                        dst.Tables("A0").Rows(i).Item("TipoFecha") = "I"
                        dst.Tables("A0").Rows(i).Item("SuministromensualM3") = vol
                        anyo = Year(dst.Tables("A0").Rows(i).Item("Fecha_Medida"))
                    Else
                        vol = vol + dst.Tables("A0").Rows(i).Item("SuministromensualM3")
                    End If

            End Select


        Next
           
    End Sub
    Sub CalcularListadoAnualSuministros()
        Dim i As Integer
        Dim rama As String = "", descripcionP As String = "", descripcionC As String

        Dim acumulado As Integer = 0

        utiles.Comprobar_Conexion_BD(Page, conexion_insert)
        comando.CommandText = "Select descripcion from PVYCR_Arbol where tipo='P' and codigoPVYCR='" & Session("ClaveNodo") & "'"
        descripcionP = Replace(comando.ExecuteScalar, Session("ClaveNodo") & " ", "")

        comando.CommandText = "Select descripcion from PVYCR_Arbol where tipo='C' and codigoCauce='" & Left(Session("ClaveNodo"), InStrRev(Session("ClaveNodo"), "P") - 1) & "'"
        descripcionC = Replace(comando.ExecuteScalar, Left(Session("ClaveNodo"), InStrRev(Session("ClaveNodo"), "P") - 1) & " ", "")

        rama = vbCrLf & Session("ClaveNodo") & "-" & Session("IdElementoMedida") & " (" & descripcionC & " - " & descripcionP & ")" & vbCrLf
        Dim DescTipoElem = "DATOS DIFERENCIALES"
        'rellenamos el dataset
        crearDatasetAnualsuministros(Session("ClaveNodo"), Session("IdElementoMedida"))
        Dim filas As Integer = dst.Tables("TablaAnualSuministros").Rows.Count

        For i = 0 To filas - 1
            acumulado = acumulado + utiles.nullACero(dst.Tables("TablaAnualSuministros").Rows(i).Item("suministroMensualM3"))
            dst.Tables("listado").Rows.Add()
            dst.Tables("listado").Rows(indice).Item("rama") = rama
            dst.Tables("listado").Rows(indice).Item("DescTipoElem") = DescTipoElem
            dst.Tables("listado").Rows(indice).Item("punto") = Session("ClaveNodo") & "-" & Session("IdElementoMedida")
            'dst.Tables("listado").Rows(indice).Item("Fecha_Medida") = "     " & dst.Tables("A1").Rows(i).Item("Fecha_Medida")        
            dst.Tables("listado").Rows(indice).Item("Fecha_MedidaF") = "     " & dst.Tables("TablaAnualSuministros").Rows(i).Item("Anyo").ToString
            dst.Tables("listado").Rows(indice).Item("Var1Titulo") = "Suministro Mensual (m3)"
            dst.Tables("listado").Rows(indice).Item("Var1Valor") = String.Format("{0:#,##0}", utiles.nullACero(dst.Tables("TablaAnualSuministros").Rows(i).Item("suministroMensualM3")))
            dst.Tables("listado").Rows(indice).Item("Var2Titulo") = "Volumen acumulado (m3)"
            dst.Tables("listado").Rows(indice).Item("Var2Valor") = String.Format("{0:#,##0.##}", Math.Round(acumulado))
            dst.Tables("listado").Rows(indice).Item("Var3Titulo") = "" '"Volumen acumulado (m3)"
            dst.Tables("listado").Rows(indice).Item("Var3Valor") = "" 'String.Format("{0:#,##0.##}", utiles.nullACero(dst.Tables("A1").Rows(i - 1).Item("Diferencial_acum"))) 'Math.Round(Volumen_acum))
            dst.Tables("listado").Rows(indice).Item("Var3Visible") = "false"
            dst.Tables("listado").Rows(indice).Item("Var4Titulo") = ""
            dst.Tables("listado").Rows(indice).Item("Var4Valor") = ""
            dst.Tables("listado").Rows(indice).Item("Var4Visible") = "false"
            dst.Tables("listado").Rows(indice).Item("Var5Titulo") = ""
            dst.Tables("listado").Rows(indice).Item("Var5Valor") = ""
            dst.Tables("listado").Rows(indice).Item("Var5Visible") = "false"
            indice = indice + 1
        Next

    End Sub
    Sub crearDatasetAnualsuministros(ByVal clavenodo As String, ByVal idelementomedida As String)
        Dim sFiltro As String = ""
        sentenciaSel = "SELECT year(fecha_medida) anyo ,sum(suministroMensualM3) suministroMensualM3 " & _
                 "FROM PVYCR_DatosSuministros D " & _
                                   "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                   "where codigoPVYCR = '" & clavenodo & "' and " & _
                                  "idElementoMedida =  '" & idelementomedida & "' "
        'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
        ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
        If LCase(Session("FiltroNulas")) = "false" Then
            sentenciaSel = sentenciaSel & " and d.suministromensualm3 is not null "
        End If


        If (Session("FiltroFechaFin") <> "" And Session("FiltroFechaIni") <> "") Or Session("FiltrarCodFuenteDato") <> "" Then
            If Session("FiltrarCodFuenteDato") <> "" Then
                sFiltro = " and d.Cod_Fuente_Dato = '" & Session("FiltrarCodFuenteDato") & "'"
            End If
            If Session("FiltroFechaFin") <> "" And Session("FiltroFechaIni") <> "" Then
                sFiltro = sFiltro & " and Fecha_medida between '" & Session("FiltroFechaIni") & "' and '" & Session("FiltroFechaFin") & "'"
            ElseIf Session("FiltroFechaFin") = "" And Session("FiltroFechaIni") <> "" Then
                Alert(Page, "La Fecha Hasta no puede ser nula")
                sFiltro = ""
            ElseIf Session("FiltroFechaFin") <> "" And Session("FiltroFechaIni") = "" Then
                Alert(Page, "La Fecha Desde no puede ser nula")
                sFiltro = ""
            End If
        ElseIf Session("FiltroFechaFin") = "" And Session("FiltroFechaIni") <> "" Then
            Alert(Page, "La Fecha Hasta no puede ser nula")
            sFiltro = ""
        ElseIf Session("FiltroFechaFin") <> "" And Session("FiltroFechaIni") = "" Then
            Alert(Page, "La Fecha Desde no puede ser nula")
            sFiltro = ""
        ElseIf Session("FiltroFechaFin") = "" And Session("FiltroFechaIni") = "" Then
            sFiltro = sFiltro & " and Fecha_Medida between '" & calculaFinicio() & " 00:00:00' and '" & calculaFfin() & " 23:59:59' "
        Else
            sFiltro = ""
        End If

        If Session("FiltroD") <> "" Then
            sentenciaSel = sentenciaSel & Session("FiltroD")
        End If

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro
        Else
            sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & calculaFinicio() & " 00:00:00' and '" & calculaFfin() & " 23:59:59' " & sentenciaOrder
        End If
        sentenciaSel = sentenciaSel & " group by  Year(fecha_medida)"
        da.SelectCommand.CommandText = sentenciaSel
        da.Fill(dst, "TablaAnualSuministros")

    End Sub
    Function calculaFinicio()
        Dim fechainicio As DateTime
        If DateTime.Now.Month < 10 Then
            fechainicio = "01/10/" & DateTime.Now.Year - 1
        Else
            fechainicio = "01/10/" & DateTime.Now.Year
        End If
        Return fechainicio
    End Function
    Function calculaFfin()
        Dim fechaFin As DateTime
        If DateTime.Now.Month < 10 Then
            fechaFin = DateTime.Today.ToString
        Else
            fechaFin = DateTime.Today.ToString
        End If
        Return fechaFin
    End Function



    Private Sub RecuperaPuntosatratar()

        Dim SQL As String = "SELECT P.ID, P.CodigoPVYCR, P.EM, P.CFD, P.TOC, P.FI, P.FF " & _
                "FROM [SICA_SIST_Sistemas-Punto] AS SP INNER JOIN " & _
                "SICA_SIST_PuntoSistema AS P ON P.ID = SP.ID_PuntoSistema INNER JOIN " & _
                "SICA_SIST_Sistemas ON SP.ID_Sistema = SICA_SIST_Sistemas.ID " & _
                "WHERE (SICA_SIST_Sistemas.NumInscripcion = " + inscripcion + ")"
        dtpuntos = GetDataDBSICA(SQL)
    End Sub

    Private Function GetDataDBSICA(ByVal SQL As String) As Data.DataTable
        Dim cadenaConexion = ConfigurationManager.ConnectionStrings("DBSICA").ConnectionString
        Dim dt As Data.DataTable = New Data.DataTable
        Dim connection As Data.SqlClient.SqlConnection = New Data.SqlClient.SqlConnection(cadenaConexion)
        Dim adapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter(SQL, connection)
        adapter.Fill(dt)
        GetDataDBSICA = dt
    End Function

    Private Function GetDataRAACS(ByVal SQL As String) As Data.DataTable
        Dim cadenaConexion = ConfigurationManager.AppSettings("dsn_RegistroAguas").ToString()
        Dim dt As Data.DataTable = New Data.DataTable
        Dim connection As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(cadenaConexion)
        Dim adapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter(SQL, connection)
        adapter.Fill(dt)
        GetDataRAACS = dt
    End Function
End Class
