Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing

Partial Class Listados_InformeSistemas
    Inherits System.Web.UI.Page

    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daConexion As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstArbol As DataSet = New System.Data.DataSet()
    Dim dstSistemas As DataSet = New System.Data.DataSet()
    Dim da As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New System.Data.DataSet()
    Dim sentenciaSel As String = ""
    Dim indice As Integer = 0
    Dim diferencial As Double = 0

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container

        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Listados_InformeSistemas))
        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)

        crearTablas()
        crearDataSetsCalculos(Session("idsistema"))

        CType(dst, System.ComponentModel.ISupportInitialize).BeginInit()

        Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSet"}, New Object() {Me.dst})

        CType(dst, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.  
        'If Request.QueryString("format") Is Nothing Then
        '    Dim fechaI As DateTime = "01/10/" & Year(Date.Now)

        '    If txtFiltroFechaIniI.Text <> "" Then
        '        Session("fechaInicio") = txtFiltroFechaIniI.Text
        '    Else
        '        If DateDiff(DateInterval.Day, Date.Now, fechaI) > 0 Then
        '            Session("fechaInicio") = "01/10/" & Year(Date.Now) - 1
        '        Else
        '            Session("fechaInicio") = fechaI
        '        End If
        '    End If

        '    If txtFiltroFechaFinI.Text <> "" Then
        '        Session("fechaFin") = txtFiltroFechaFinI.Text
        '    Else
        '        If DateDiff(DateInterval.Day, Date.Now, fechaI) > 0 Then
        '            Session("fechaFin") = "30/09/" & Year(Date.Now)
        '        Else
        '            Session("fechaFin") = "30/09/" & Year(Date.Now) + 1
        '        End If
        '    End If

        '    Session("idsistema") = ddlSistema.SelectedValue

        'End If
        'InitializeComponent()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")

        If Not IsPostBack Then
            'If Session("usuarioReg") = Nothing Then
            '    Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
            '    Exit Sub
            'End If
            imgCalFechaIniI.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniI.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinI.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinI.ClientID & "'),'dd/mm/yyyy');")
            'BuildListBox()
            crearDataSetsSistemas()
        End If


    End Sub
    Private Sub BuildListBox()
        LoadReportTemplate()
    End Sub

    Private Sub LoadReportTemplate()
        reportGenerator1.LoadTemplate(Server.MapPath("ListadoSistemas.rst"))
        Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
        SharpShooterWebViewer1.Source = reportGenerator1
    End Sub

    Private Sub crearTablas()
        dst.Tables.Add("Listados")
        dst.Tables("Listados").Columns.Add(New DataColumn("CodigoPVYCR", Type.GetType("System.String")))
        dst.Tables("Listados").Columns.Add(New DataColumn("IdElementoMedida", Type.GetType("System.String")))
        dst.Tables("Listados").Columns.Add(New DataColumn("VolumenDif", Type.GetType("System.String")))
        dst.Tables("Listados").Columns.Add(New DataColumn("Factor", Type.GetType("System.Int32")))
        dst.Tables("Listados").Columns.Add(New DataColumn("VxF", Type.GetType("System.String")))
        dst.Tables("Listados").Columns.Add(New DataColumn("Color", Type.GetType("System.String")))
        dst.Tables("Listados").Columns.Add(New DataColumn("Tipo", Type.GetType("System.String")))
        dst.Tables("Listados").Columns.Add(New DataColumn("Sistema", Type.GetType("System.String")))
    End Sub

    Private Sub crearDataSetsSistemas()
        'IPIM esto sería para todos los sistemas, a nosotros nos pasarán uno
        ''SISTEMAS
        daConexion.SelectCommand.CommandText = "Select * from dbo.PVYCR_Sistemas"
        daConexion.Fill(dstArbol, "tablaSistemasPadre")

        ddlSistema.DataSource = dstArbol.Tables("tablaSistemasPadre")
        ddlSistema.DataValueField = "idSistema"
        ddlSistema.DataTextField = "nombre"
        ddlSistema.DataBind()
        ddlSistema.Items.Insert(0, New ListItem("[Seleccionar]", ""))
    End Sub

    Private Sub crearArbolRecursivoSistemas(ByVal dstArbol As DataSet)
        Dim fila As DataRow
        For Each fila In dstArbol.Tables("tablaSistemasPadre").Rows
            If (fila.IsNull("IdSistemaPadre")) Then
                crearDataSetsCalculos(fila("idSistema"))
                Dim tn As New TreeNode
                InsertarNodosSistemas(fila, 1)
            End If
        Next
    End Sub
    Private Sub InsertarNodosSistemas(ByVal fila As DataRow, ByVal nivel As Integer)

        Dim f As DataRow

        For Each f In fila.GetChildRows("SelfRefenceRelation")
            crearDataSetsCalculos(f("IdSistema"))
            InsertarNodosSistemas(f, nivel + 1)
        Next

    End Sub

    Private Sub crearDataSetsCalculos(ByVal IdSistema As Integer)
        'Dim sentenciaSel As String
        ''Construcción de la sentencia SELECT para Filtrar Sistemas según IdSistema y Filtros de Fecha Seleccionados

        'sentenciaSel = "SELECT DesCalculo, PVYCR_Calculos.IdCalculo,IdSistema " & _
        '               "FROM PVYCR_Calculos INNER JOIN PVYCR_Calculo_Sistemas " & _
        '               "ON PVYCR_Calculo_Sistemas.IdCalculo = PVYCR_Calculos.IdCalculo " & _
        '               "WHERE(IdSistema = " & IdSistema & ")"

        'daConexion.SelectCommand.CommandText = sentenciaSel
        'daConexion.Fill(dstSistemas, "Calculos")

        'For i As Integer = 0 To dstSistemas.Tables("Calculos").Rows.Count - 1
        '    crearDataSetsCalculosElementosdeMedida(dstSistemas.Tables("Calculos").Rows(i).Item("IdCalculo"))
        'Next

        sentenciaSel = "select * from pvycr_calculo_sistemas CS " & _
            "inner join pvycr_calculos_elementosmedida CEM on CEM.idCalculo=CS.idCalculo " & _
            "inner join pvycr_elementosMedida EM on (EM.idElementoMedida=CEM.idElementoMedida and EM.CodigoPVYCR=CEM.CodigoPVYCR) " & _
            "where IdSistema = " & IdSistema

        daConexion.SelectCommand.CommandText = sentenciaSel
        daConexion.Fill(dstSistemas, "CalculosElementosdeMedida")

        For i As Integer = 0 To dstSistemas.Tables("CalculosElementosdeMedida").Rows.Count - 1
            crearDataSets(dstSistemas.Tables("CalculosElementosdeMedida").Rows(i).Item("CodigoPVYCR"), dstSistemas.Tables("CalculosElementosdeMedida").Rows(i).Item("idElementoMedida"), dstSistemas.Tables("CalculosElementosdeMedida").Rows(i).Item("Tipo"), dstSistemas.Tables("CalculosElementosdeMedida").Rows(i).Item("Factor"))
        Next
    End Sub

    'Private Sub crearDataSetsCalculosElementosdeMedida(ByVal IdCalculo As Integer)

    'Construccion de las sentencias SELECT para mostrar los Calculos-Elementos de Medida
    'Dim sentenciaSel As String
    'Construcción de la sentencia SELECT para Filtrar Sistemas según IdSistema y Filtros de Fecha Seleccionados


    'sentenciaSel = "SELECT PVYCR_Calculos_ElementosMedida.Factor as Factor, PVYCR_Calculos_ElementosMedida.CodigoPVYCR as CodigoPVYCR, " & _
    '             "PVYCR_Calculos_ElementosMedida.idElementoMedida as idElementoMedida, PVYCR_Calculos_ElementosMedida.idCalculo, " & _
    '             "PVYCR_ElementosMedida.Tipo " & _
    '             "FROM PVYCR_ElementosMedida INNER JOIN " & _
    '             "PVYCR_Calculos_ElementosMedida ON " & _
    '             "PVYCR_ElementosMedida.CodigoPVYCR = PVYCR_Calculos_ElementosMedida.CodigoPVYCR " & _
    '             "WHERE IdCalculo=" & IdCalculo

    'daConexion.SelectCommand.CommandText = sentenciaSel
    'daConexion.Fill(dstSistemas, "CalculosElementosdeMedida")

    'For i As Integer = 0 To dstSistemas.Tables("CalculosElementosdeMedida").Rows.Count - 1
    '    crearDataSets(dstSistemas.Tables("CalculosElementosdeMedida").Rows(i).Item("CodigoPVYCR"), dstSistemas.Tables("CalculosElementosdeMedida").Rows(i).Item("idElementoMedida"), dstSistemas.Tables("CalculosElementosdeMedida").Rows(i).Item("Tipo"), dstSistemas.Tables("CalculosElementosdeMedida").Rows(i).Item("Factor"))
    'Next
    'End Sub

    Private Sub crearDataSets(ByVal codigoPVYCR As String, ByVal idElementoMedida As String, ByVal Tipo As String, ByVal Factor As String)
        Dim Tabla, Campos As String

        Select Case Tipo
            Case "V"    'motor                    
                Tabla = "PVYCR_DatosMotores"
                Campos = "LecturaContador_M3 as Lectura, idIncidenciaVolumetrica as idIncidencia, ReiniciolecturaVolumetrica, ConsumoVolumetricoAdicional "
            Case "Q"    'caudalímetro
                Tabla = "PVYCR_DatosAcequias"
                Campos = "Caudal_M3S as caudal"
            Case Else
                Exit Sub
        End Select

        sentenciaSel = "Select Fecha_Medida, " & Campos & " FROM " & Tabla & " " & _
            "WHERE Fecha_Medida BETWEEN '" & Session("fechaInicio") & " 00:00:00' AND '" & Session("fechaFin") & " 23:59:59' " & _
            "AND codigoPVYCR = '" & codigoPVYCR & "' and idElementoMedida='" & idElementoMedida & "' " & _
            "order by codigoPVYCR, Fecha_Medida"
        da.SelectCommand.CommandText = sentenciaSel

        da.Fill(dst, "TablaAuxiliar")

        If dst.Tables("TablaAuxiliar").Rows.Count > 0 Then
            calcularDiferenciales(Tipo)
            CalcularTotales(codigoPVYCR, idElementoMedida, Factor, Tipo)
        End If
    End Sub

    Sub calcularDiferenciales(ByVal tipo As String)
        Dim Caudal0, Caudal1 As Double
        Dim Lectura0, Lectura1 As Double
        Dim IdIncidencia As Integer
        Dim Tiempo0, Tiempo1 As DateTime
        diferencial = 0

        Try
            dst.Tables("TablaAuxiliar").Columns.Add(New DataColumn("Diferencial", Type.GetType("System.Double")))
            dst.Tables("TablaAuxiliar").Rows(0).Item("Diferencial") = 0
        Catch ex As Exception

        End Try

        For i As Integer = 1 To dst.Tables("TablaAuxiliar").Rows.Count - 1
            Tiempo0 = dst.Tables("TablaAuxiliar").Rows(i - 1).Item("Fecha_Medida")
            Tiempo1 = dst.Tables("TablaAuxiliar").Rows(i).Item("Fecha_Medida")

            Select Case tipo
                Case "V"    'motor                    
                    Lectura0 = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i - 1).Item("Lectura"))
                    Lectura1 = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("Lectura"))
                    IdIncidencia = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("IdIncidencia"))
                    If DateDiff(DateInterval.Second, Tiempo0, Tiempo1) = 0 Then
                        dst.Tables("TablaAuxiliar").Rows(i).Item("Diferencial") = 0
                    Else
                        If IdIncidencia = 0 Then
                            dst.Tables("TablaAuxiliar").Rows(i).Item("Diferencial") = Lectura1 - Lectura0
                        Else
                            dst.Tables("TablaAuxiliar").Rows(i).Item("Diferencial") = CalcularIncidencias(i, IdIncidencia)
                        End If
                    End If
                Case "Q"    'caudalímetro
                    Caudal0 = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i - 1).Item("Caudal"))
                    Caudal1 = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("Caudal"))
                    If DateDiff(DateInterval.Second, Tiempo0, Tiempo1) = 0 Then
                        dst.Tables("TablaAuxiliar").Rows(i).Item("Diferencial") = 0
                        dst.Tables("TablaAuxiliar").Rows(i).Item("Caudal") = 0
                    Else
                        dst.Tables("TablaAuxiliar").Rows(i).Item("Diferencial") = ((Caudal0 + Caudal1) / 2) * (Tiempo1.ToOADate - Tiempo0.ToOADate) * 24 * 3600
                    End If
                Case Else
                    Exit Sub
            End Select

            diferencial = diferencial + utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("Diferencial"))
        Next
    End Sub
    Private Sub CalcularTotales(ByVal CodigoPVYCR As String, ByVal IdElementoMedida As String, ByVal Factor As Int32, ByVal Tipo As String)

        dst.Tables("Listados").Rows.Add()
        dst.Tables("Listados").Rows(indice).Item("CodigoPVYCR") = CodigoPVYCR
        dst.Tables("Listados").Rows(indice).Item("IdElementoMedida") = IdElementoMedida
        dst.Tables("Listados").Rows(indice).Item("Factor") = Factor
        dst.Tables("Listados").Rows(indice).Item("VolumenDif") = String.Format("{0:#,##0.0}", Math.Round(diferencial, 1))
        dst.Tables("Listados").Rows(indice).Item("VxF") = String.Format("{0:#,##0.0}", Math.Round(diferencial * Factor, 1))
        dst.Tables("Listados").Rows(indice).Item("Tipo") = Tipo
        If Factor > 0 Then
            dst.Tables("Listados").Rows(indice).Item("Color") = ""
        Else
            dst.Tables("Listados").Rows(indice).Item("Color") = "Red"
        End If
        dst.Tables("Listados").Rows(indice).Item("Sistema") = Session("Sistema")

        dst.Tables("TablaAuxiliar").Dispose()
        dst.Tables("TablaAuxiliar").Clear()
        diferencial = 0

        indice = indice + 1
    End Sub

    Private Function CalcularIncidencias(ByVal i As Integer, ByVal IdIncidencia As Integer) As Double
        Dim lectura, dif, lectura_ant As Double

        lectura = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("Lectura"))
        lectura_ant = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i - 1).Item("Lectura"))

        If utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("IdIncidencia")) = 6 Or utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("IdIncidencia")) = 7 Then
            dif = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("Lectura")) - utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("ReiniciolecturaVolumetrica")) + utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("ConsumoVolumetricoAdicional"))
        ElseIf utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("IdIncidencia")) = 5 Or utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("IdIncidencia")) = 8 Then
            dif = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("Lectura")) + utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("ConsumoVolumetricoAdicional")) - lectura_ant
        End If

        Return dif
    End Function

    Protected Sub btnCaudalesFiltradosI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCaudalesFiltradosI.Click
        If ddlSistema.SelectedValue = "" Then
            Alert(Me, "Debe seleccionar un sistema para lanzar el informe")
            Exit Sub
        End If
        If Session("idsistema") = "" Then
            Exit Sub
        End If
        dst.Dispose()
        SharpShooterWebViewer1.Dispose()

        If Request.QueryString("format") Is Nothing Then
            Dim fechaI As DateTime = "01/10/" & Year(Date.Now)

            If txtFiltroFechaIniI.Text <> "" Then
                Session("fechaInicio") = txtFiltroFechaIniI.Text
            Else
                If DateDiff(DateInterval.Day, Date.Now, fechaI) > 0 Then
                    Session("fechaInicio") = "01/10/" & Year(Date.Now) - 1
                Else
                    Session("fechaInicio") = fechaI
                End If
            End If

            If txtFiltroFechaFinI.Text <> "" Then
                Session("fechaFin") = txtFiltroFechaFinI.Text
            Else
                If DateDiff(DateInterval.Day, Date.Now, fechaI) > 0 Then
                    Session("fechaFin") = "30/09/" & Year(Date.Now)
                Else
                    Session("fechaFin") = "30/09/" & Year(Date.Now) + 1
                End If
            End If

            Session("idsistema") = ddlSistema.SelectedValue

        End If

        InitializeComponent()
        BuildListBox()
        Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
                            "MostrarPanel()</script>")
    End Sub

    Protected Sub ddlSistema_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSistema.SelectedIndexChanged
        Session("idsistema") = ddlSistema.SelectedValue
        Session("Sistema") = ddlSistema.SelectedItem.Text
        Try
            dst.Tables("Listados").Dispose()
        Catch ex As Exception

        End Try
    End Sub
End Class
