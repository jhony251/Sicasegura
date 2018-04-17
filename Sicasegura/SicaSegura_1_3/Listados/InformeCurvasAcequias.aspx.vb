Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Imports NineRays.Reporting.Export.PDF
Imports NineRays.Reporting.Export.RTF
Imports NineRays.Reporting.Export.Html
Imports NineRays.Reporting.Export.Excel
Imports NineRays.Reporting.Export.CSV

Partial Class Listados_InformeCurvasAcequias
    Inherits System.Web.UI.Page

    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim da As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New System.Data.DataSet()

    Dim sentenciaSel As String

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container

        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Listados_InformeCurvasAcequias))
        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)

        crearDataSets()

        CType(dst, System.ComponentModel.ISupportInitialize).BeginInit()

        Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSet"}, New Object() {Me.dst})

        CType(dst, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.  
        If Request.QueryString("format") Is Nothing Then
            Session("codigoPVYCR") = Request.QueryString("codigoPVYCR")
            Session("idElementoMedida") = Request.QueryString("idElementoMedida")
        End If
        InitializeComponent()
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            BuildListBox()
        End If

        If Session("informe") = "" Then
            If Request.QueryString("format") Is Nothing Then
                Response.Redirect(SharpShooterWebViewer1.PdfLink)
            End If
        Else
            SharpShooterWebViewer1.Style.Value = "Z-INDEX: 102; LEFT: 8px; POSITION: absolute;"
        End If
    End Sub

    Private Sub BuildListBox()
        LoadReportTemplate()
    End Sub

    Private Sub LoadReportTemplate()
        reportGenerator1.LoadTemplate(Server.MapPath("InformeCurvasAcequias.rst"))
        Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
        SharpShooterWebViewer1.Source = reportGenerator1
    End Sub

    Private Sub crearDataSets()
        sentenciaSel = "SELECT * FROM PVYCR_CurvasAcequias_Valores cav left outer join dbo.PVYCR_CurvasAcequias ca on ca.cod_curva=cav.cod_curva " & _
                    "WHERE CodigoPVYCR='" & Session("CodigoPVYCR") & "' AND IdElementoMedida='" & Session("IdElementoMedida") & "' AND Caudal is not null " & _
                    "ORDER BY CodigoPVYCR, idElementoMedida, Regimen, Fecha_Inicio_Uso"
        
        da.SelectCommand.CommandText = sentenciaSel
        da.Fill(dst, "TablaCurvasAcequias")

        dst.Tables.Add("TablaListado")
        With dst.Tables("TablaListado").Columns
            .Add(New DataColumn("CodigoPVYCR", Type.GetType("System.String")))
            .Add(New DataColumn("idElementoMedida", Type.GetType("System.String")))
            .Add(New DataColumn("Regimen", Type.GetType("System.String")))
            .Add(New DataColumn("Nivel", Type.GetType("System.String")))
            .Add(New DataColumn("Caudal", Type.GetType("System.String")))
        End With

        Dim regimen As String = "", regimen_ant As String = ""
        Dim nivel, nivel_ant, caudal, caudal_ant As Double

        Dim j As Integer = 0
        Dim NoCortada As Boolean = True
        Dim dr() As System.Data.DataRow
        dr = dst.Tables("TablaCurvasAcequias").DefaultView.Table.Select
        dr = dst.Tables("TablaCurvasAcequias").DefaultView.Table.Select("Regimen='CT'")


        For i As Integer = 0 To dst.Tables("TablaCurvasAcequias").Rows.Count - 1
            NoCortada = True
            If dst.Tables("TablaCurvasAcequias").Rows(i).Item("Regimen") = "CT" Then
                If dr.Length = 2 Then
                    If dr(0).Item("Nivel") = 0 And dr(1).Item("Nivel") = 1000 Then
                        NoCortada = False
                    End If
                End If
            End If
            With dst.Tables("TablaListado")
                If NoCortada Then
                    regimen = dst.Tables("TablaCurvasAcequias").Rows(i).Item("Regimen")
                    If i = 0 Or regimen <> regimen_ant Then
                        regimen_ant = dst.Tables("TablaCurvasAcequias").Rows(i).Item("Regimen")
                        nivel_ant = Math.Round(dst.Tables("TablaCurvasAcequias").Rows(i).Item("Nivel"), 2)
                        caudal_ant = Math.Round(dst.Tables("TablaCurvasAcequias").Rows(i).Item("Caudal"), 3)
                    Else
                        regimen = dst.Tables("TablaCurvasAcequias").Rows(i).Item("Regimen")
                        nivel = Math.Round(dst.Tables("TablaCurvasAcequias").Rows(i).Item("Nivel"), 2)
                        caudal = Math.Round(dst.Tables("TablaCurvasAcequias").Rows(i).Item("Caudal"), 3)

                        Do While nivel_ant + 0.01 < nivel
                            .Rows.Add()
                            caudal_ant = Math.Round(caudal_ant + ((caudal - caudal_ant) / (nivel - nivel_ant)) * 0.01, 3)        'El 0.01 Sale de nivel_ant+0.01-nivel_ant
                            nivel_ant = Math.Round(nivel_ant + 0.01, 2)

                            .Rows(j).Item("CodigoPVYCR") = dst.Tables("TablaCurvasAcequias").Rows(i).Item("CodigoPVYCR")
                            .Rows(j).Item("idElementoMedida") = dst.Tables("TablaCurvasAcequias").Rows(i).Item("idElementoMedida")
                            .Rows(j).Item("Regimen") = regimen_ant
                            .Rows(j).Item("Nivel") = nivel_ant
                            .Rows(j).Item("Caudal") = caudal_ant
                            j += 1
                        Loop

                        regimen_ant = regimen
                        nivel_ant = nivel
                        caudal_ant = caudal
                    End If

                    .Rows.Add()
                    .Rows(j).Item("CodigoPVYCR") = dst.Tables("TablaCurvasAcequias").Rows(i).Item("CodigoPVYCR")
                    .Rows(j).Item("idElementoMedida") = dst.Tables("TablaCurvasAcequias").Rows(i).Item("idElementoMedida")
                    .Rows(j).Item("Regimen") = regimen_ant
                    .Rows(j).Item("Nivel") = nivel_ant
                    .Rows(j).Item("Caudal") = caudal_ant

                    j += 1
                End If
            End With
        Next
    End Sub
End Class
