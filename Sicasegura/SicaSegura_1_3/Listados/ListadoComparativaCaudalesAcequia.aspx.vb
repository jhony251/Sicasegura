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

Partial Class Listados_ListadoComparativaCaudalesAcequia
    Inherits System.Web.UI.Page

    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_listados"))
    Dim conexion_insert As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim da As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New System.Data.DataSet()
    Dim sentenciaSel, sentenciaOrder As String
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion_insert)
    Dim InicioAnyoHidr As Date

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container

        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Listados_ListadoComparativaCaudalesAcequia))
        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)

        crearDatasetsAcequias()

        CType(dst, System.ComponentModel.ISupportInitialize).BeginInit()

        Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSet"}, New Object() {Me.dst})

        CType(dst, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.  
        If Request.QueryString("format") Is Nothing Then
            Session("tipoelem") = Request.QueryString("tipoelem")
            Session("codigoPVYCR") = Request.QueryString("codigoPVYCR")
            'Session("fechaInicio") = Request.QueryString("fechaInicio")
            'Session("fechaFin") = Request.QueryString("fechaFin")
            Session("fechaInicio") = "1/10/2005"
            Session("fechaFin") = "30/9/2008"
        End If
        InitializeComponent()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            'JavaScript.Alert(Me, Session("usuarioReg"))
            BuildListBox()
        End If
    End Sub

    Private Sub BuildListBox()
        LoadReportTemplate()
    End Sub

    Private Sub LoadReportTemplate()
        reportGenerator1.LoadTemplate(Server.MapPath("ListadoVolumenAcequias.rst"))
        Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
        SharpShooterWebViewer1.Source = reportGenerator1
    End Sub

    Private Sub crearDatasetsAcequias()
        Dim caudal, caudal_ant, caudal_act As Decimal
        Dim diferencial As Decimal = 0, diferencial_acum As Decimal = 0
        Dim tiempo, tiempo_ant, tiempo_act As Date
        Dim i, j, k, l As Integer

        sentenciaSel = "Select Fecha_Medida, Caudal_M3S FROM PVYCR_DatosAcequias " & _
                "WHERE Fecha_Medida BETWEEN '" & Session("fechaInicio") & "' AND '" & Session("fechaFin") & "' " & _
                "AND codigoPVYCR = 'VA004P04'" & _
                "order by fecha_medida"

        da.SelectCommand.CommandText = sentenciaSel
        da.Fill(dst, "TablaAuxiliar")

        dst.Tables.Add("TablaAcequias")

        If Month(Session("fechaInicio")) > 9 Then
            InicioAnyoHidr = "1/10/" & Year(Session("fechaInicio"))
        Else
            InicioAnyoHidr = "1/10/" & Year(Session("fechaInicio")) - 1
        End If

        j = 0
        k = 0
        l = 0

        For i = 0 To dst.Tables("TablaAuxiliar").Rows.Count - 1
            If CambiaAnyoH(dst.Tables("TablaAuxiliar").Rows(i).Item("Fecha_medida")) Then
                l = k
                k = 0
            End If

            If k = 0 Then
                j = j + 1
                dst.Tables("TablaAcequias").Columns.Add(New DataColumn("Fecha_Medida" & j, Type.GetType("System.String")))
                dst.Tables("TablaAcequias").Columns.Add(New DataColumn("Caudal" & j, Type.GetType("System.Decimal")))
                dst.Tables("TablaAcequias").Columns.Add(New DataColumn("Diferencial" & j, Type.GetType("System.Decimal")))
                dst.Tables("TablaAcequias").Columns.Add(New DataColumn("Diferencial_acum" & j, Type.GetType("System.Decimal")))
            End If

            If i <> 0 Then
                tiempo = dst.Tables("TablaAuxiliar").Rows(i).Item("Fecha_medida")
                tiempo_ant = dst.Tables("TablaAuxiliar").Rows(i - 1).Item("Fecha_medida")
                caudal_ant = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i - 1).Item("Caudal_M3S"))
                caudal = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("Caudal_M3S"))

                'Si no existe el día lo metemos
                Do While DateDiff(DateInterval.Day, tiempo_ant, tiempo) > 0
                    If Year(tiempo_act) = 2006 And Month(tiempo_act) = 9 Then
                        tiempo_act = tiempo_act
                    End If
                    tiempo_act = DateAdd(DateInterval.Day, 1, tiempo_ant)
                    caudal_act = CalculoCaudalSegunDia(tiempo_ant, tiempo, tiempo_act, caudal_ant, caudal)

                    If CambiaAnyoH(tiempo_act) Then
                        l = k
                        k = 0
                        j = j + 1
                        dst.Tables("TablaAcequias").Columns.Add(New DataColumn("Fecha_Medida" & j, Type.GetType("System.String")))
                        dst.Tables("TablaAcequias").Columns.Add(New DataColumn("Caudal" & j, Type.GetType("System.Decimal")))
                        dst.Tables("TablaAcequias").Columns.Add(New DataColumn("Diferencial" & j, Type.GetType("System.Decimal")))
                        dst.Tables("TablaAcequias").Columns.Add(New DataColumn("Diferencial_acum" & j, Type.GetType("System.Decimal")))
                    End If

                    diferencial = Math.Round(DateDiff(DateInterval.Second, tiempo_ant, tiempo_act) * (caudal_act + caudal_ant) / 2, 2)
                    diferencial_acum = diferencial_acum + diferencial

                    If k > l Or l = 0 Then
                        dst.Tables("TablaAcequias").Rows.Add()
                    End If

                    dst.Tables("TablaAcequias").Rows(k).Item("Fecha_Medida" & j) = tiempo_act
                    dst.Tables("TablaAcequias").Rows(k).Item("Caudal" & j) = caudal_act
                    dst.Tables("TablaAcequias").Rows(k).Item("Diferencial" & j) = diferencial
                    dst.Tables("TablaAcequias").Rows(k).Item("Diferencial_acum" & j) = diferencial_acum

                    tiempo_ant = tiempo_act
                    caudal_ant = caudal_act
                    k = k + 1
                Loop

                diferencial = Math.Round(DateDiff(DateInterval.Second, tiempo_ant, tiempo) * (caudal + caudal_ant) / 2, 2)
                diferencial_acum = diferencial_acum + diferencial
            End If

            If k > l Or l = 0 Then
                dst.Tables("TablaAcequias").Rows.Add()
            End If
            dst.Tables("TablaAcequias").Rows(k).Item("Fecha_Medida" & j) = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("Fecha_medida"))
            dst.Tables("TablaAcequias").Rows(k).Item("Caudal" & j) = utiles.nullACero(dst.Tables("TablaAuxiliar").Rows(i).Item("Caudal_M3S"))
            dst.Tables("TablaAcequias").Rows(k).Item("Diferencial" & j) = diferencial
            dst.Tables("TablaAcequias").Rows(k).Item("Diferencial_acum" & j) = diferencial_acum

            k = k + 1
        Next
    End Sub

    Private Function CambiaAnyoH(ByVal fecha As Date) As Boolean
        Dim InicioAnyoHidrAct As Date

        If Month(fecha) > 9 Then
            InicioAnyoHidrAct = "1/10/" & Year(fecha)
        Else
            InicioAnyoHidrAct = "1/10/" & Year(fecha) - 1
        End If

        If DateDiff(DateInterval.Day, InicioAnyoHidr, InicioAnyoHidrAct) > 0 Then
            InicioAnyoHidr = InicioAnyoHidrAct
            Return True
        Else
            Return False
        End If

    End Function

    Private Function CalculoCaudalSegunDia(ByVal x0 As DateTime, ByVal x1 As DateTime, ByVal dia As DateTime, ByVal y0 As Decimal, ByVal y1 As Decimal) As Decimal
        'Calculamos la ecuación de la recta
        Dim a, b, caudal As Decimal
        Dim x0_s, x1_s, dia_s As Integer

        a = (y1 - y0) / DateDiff(DateInterval.Second, x0, x1)

        x0_s = DateDiff(DateInterval.Second, InicioAnyoHidr, x0)
        x1_s = DateDiff(DateInterval.Second, InicioAnyoHidr, x1)
        b = (x1_s * y0 - x0_s * y1) / (x1_s - x0_s)

        dia_s = DateDiff(DateInterval.Second, InicioAnyoHidr, dia)
        caudal = Math.Round(a * dia_s + b, 2)

        Return caudal
    End Function
    Protected Function prueba()

    End Function
End Class
