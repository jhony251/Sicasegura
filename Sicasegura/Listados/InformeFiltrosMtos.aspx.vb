Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Imports NineRays.Reporting.Export.PDF
Imports System.Drawing

Partial Class Listados_InformeFiltrosMtos
    Inherits System.Web.UI.Page
    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daCampos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCampos As DataSet = New System.Data.DataSet()

    Dim sentenciaSel As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Listados_InformeFiltrosMtos))

        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)
        crearDataSets()

        CType(dstCampos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSet"}, New Object() {dstCampos})

        CType(dstCampos, System.ComponentModel.ISupportInitialize).EndInit()

        If Not IsPostBack Then
            reportGenerator1.LoadTemplate(Server.MapPath("InformeFiltrosMtos.rst"))
            Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
            SharpShooterWebViewer1.Source = reportGenerator1

        End If

    End Sub

    Private Sub crearDataSets()
        Dim eti As ArrayList = Session("etiquetas")
        Dim numeti As Integer = eti.Count
        Dim i As Integer = 0
        sentenciaSel = Session("sentenciaFiltroMto")

        daCampos.SelectCommand.CommandText = sentenciaSel
        daCampos.Fill(dstCampos, "TablaCampos")
        'añadimos las columnas para las etiquetas, el tamaño del campo (size y la longitud del mismo (L)

        'vamos a rellenar el dataset de las etiquetas del report y el de los valores del report
        Dim dtEti As DataTable = New DataTable("TablaEti")
        dstCampos.Tables.Add(dtEti)
        Dim borde As Object
        For i = 0 To numeti - 1
            dtEti.Columns.Add("campo" & i + 1)
            dtEti.Columns.Add("size" & i + 1)
            dtEti.Columns.Add("L" & i + 1)
            dtEti.Columns.Add("M" & i + 1)
            'Dim col As DataColumn = New DataColumn("B" & i + 1, TypeOf Border Is Border)
            dtEti.Columns.Add("B" & i + 1, GetType(Border))
            dtEti.Columns.Add("Estilo" & i + 1)
            'tamaño del campo
            dstCampos.Tables("Tablacampos").Columns.Add("size" & i + 1)
            'posición en la hoja del campo
            dstCampos.Tables("Tablacampos").Columns.Add("L" & i + 1)
            'si el campo es multilinea o si se puede expandir.
            dstCampos.Tables("Tablacampos").Columns.Add("M" & i + 1)
            'estilo del campo
            dstCampos.Tables("Tablacampos").Columns.Add("ECampo" & i + 1)
        Next
        dtEti.Columns.Add("Cabecera")
        rellenarDatosDataset(numeti, dtEti)
    End Sub
    Protected Sub rellenarDatosDataset(ByVal Numeti As Integer, ByVal dtEti As DataTable)
        'rellenamos las columnas del  dataset
        Dim filas As Integer = dstCampos.Tables("TablaCampos").Rows.Count
        Dim columnas As Integer = dstCampos.Tables("TablaCampos").Columns.Count

        Dim i As Integer = 0
        Dim eti As ArrayList = Session("etiquetas")
        Dim longi As ArrayList = New ArrayList
        Dim longiCalc As ArrayList = New ArrayList
        'vamos a rellenar el dataset de las etiquetas
        Dim drEti As DataRow
        'Dim dtEti As DataTable = New DataTable("TablaEti")
        'dstCampos.Tables.Add(dtEti)
        drEti = dtEti.NewRow
        For i = 0 To Numeti - 1
            drEti("campo" & i + 1) = eti(i).ToString
            drEti("Estilo" & i + 1) = "Etiquetas"
        Next
        drEti("Cabecera") = "Listado de " & Request.QueryString("mto")
        dtEti.Rows.InsertAt(drEti, 0)

        Dim columnasE As Integer = dtEti.Columns.Count
        'ajustamos los tamaños de los string

        calculoTamanyoCampos(columnas, Numeti, longi, longiCalc)

    End Sub
    Protected Sub calculoTamanyoCampos(ByVal columnas As Integer, ByVal numeti As Integer, ByVal longi As ArrayList, ByVal longiCalc As ArrayList)
        Dim longiMax As Integer = 0
        Dim longiMaxR As Integer = 0
        'Dim lon As Integer = 0


        Dim fac As Double = 0.85
        'ponemos la longitud a cero
        For i = 0 To columnas - 1
            longi.Add(0)
            longiCalc.Add(0)
        Next
        ' rellenamos le valor de las etiquetas a cero

        For i = 0 To numeti - 1
            dstCampos.Tables("TablaEti").Rows(0).Item("size" & i + 1) = "0;0"
            dstCampos.Tables("TablaEti").Rows(0).Item("L" & i + 1) = "0;0"
        Next

        Dim borderInf As Border = New Border(New BorderLine(LineStyle.Solid, Color.Black, 1))
        Dim contadorFilas As Integer = 0
        For Each dr As DataRow In dstCampos.Tables("TablaCampos").Rows
            Dim p As Double = 0
            Dim t As Double
            If contadorFilas = 0 Then
                Dim tamanyo As Double = 0.0
                For i = 0 To numeti - 1

                    Dim lon As Integer = dr("campo" & i + 1).ToString.Length
                    'asignamos el borde de las etiquetas
                    dstCampos.Tables("TablaEti").Rows(0).Item("B" & i + 1) = borderInf

                    If lon > 80 Then lon = 900
                    If lon > 15 And lon < 80 Then
                        'If longi(i) > lon Then
                        '    lon = longi(i) * 100
                        'End If
                        lon = 600
                    End If
                    If lon < 15 Then lon = 400
                    longiCalc(i) = lon
                    'vamos sumando los tamaños de los campos para después comprbar que no excedemos el tamaño
                    'de la pantalla/report
                    tamanyo = tamanyo + lon

                Next
                'revisamos el tamaño para que no se salgan de la pantalla los campo
                If (tamanyo * fac) > 2400 Then
                    tamanyo = 2400 / (tamanyo * fac)
                Else
                    tamanyo = 1
                End If
                For i = 0 To numeti - 1
                    t = longiCalc(i) * fac * tamanyo
                    dr("size" & i + 1) = t.ToString & ";40"
                    dr("L" & i + 1) = (p + 60).ToString & ";10"
                    dr("M1") = "True"
                    dr("Ecampo" & i + 1) = "Campos"
                    Dim tamanyoEti() As String = dstCampos.Tables("TablaEti").Rows(0).Item("size" & i + 1).ToString.Split(";")
                    Dim longEti() As String = dstCampos.Tables("TablaEti").Rows(0).Item("L" & i + 1).ToString.Split(";")

                    If t > Convert.ToDouble(tamanyoEti(0)) Then
                        dstCampos.Tables("TablaEti").Rows(0).Item("size" & i + 1) = t.ToString & ";40"
                    End If
                    If p + 60 > Convert.ToDouble(longEti(0)) Then
                        dstCampos.Tables("TablaEti").Rows(0).Item("L" & i + 1) = (p + 60).ToString & ";10"
                    End If
                    p = p + t
                    contadorFilas += 1
                Next
            Else 'contador filas
                For i = 0 To numeti - 1
                    dr("size" & i + 1) = dstCampos.Tables("TablaCampos").Rows(0).Item("size" & i + 1)
                    dr("L" & i + 1) = dstCampos.Tables("Tablacampos").Rows(0).Item("L" & i + 1)
                    dr("M1") = "True"
                Next
            End If
        Next

    End Sub
End Class
