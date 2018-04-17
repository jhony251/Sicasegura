Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO
Partial Class SICAH_CurvasAcequiasARBOL
    Inherits System.Web.UI.Page

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim codigoCauce As String = ""


    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder, sentenciaSelCount As String
    Dim parfila As Integer = 0
    Dim numfila As Integer = 0
    Dim twcacheado As TreeView


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

        'NodoSeleccionadoArbol(Page, treeView1)
        'Dim txtLecturasVisor As String

        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            '---
            '01/07/2008 EGB 
            'If rbVisor.Checked = True Then txtLecturasVisor = "V"
            'If rbLecturas.Checked = True Then txtLecturasVisor = "L"
            '----
            If Session("DSLoaded") <> 1 Then
                Session("DSLoaded") = crearArbolRecursivoBISBIS(treeView1, GetDataSet(), "Q")
            End If
            'Me.treeView1.Attributes.Add("oncontextmenu", "RightClick(event);")
            'treeView1.Attributes.Add("onselectedindexchange", "CheckParent();")

        End If
    End Sub

    Public Function GetDataSet() As DataSet
        Dim DS As System.Data.DataSet
        
        If File.Exists(MapPath("xmlDataSet.xml")) = False Then

            crearDataSetsPuntosXML2()

            dstarbol.WriteXml(MapPath("xmlDataSet.xml"))

            Return dstarbol
            Session("DSLoaded") = 1
        End If

        DS = New DataSet

        DS.ReadXml(MapPath("xmlDataSet.xml"))

        DS.Relations.Add("SelfRefenceRelation", _
                       DS.Tables("tablaArbol").Columns("IdArbol"), _
                        DS.Tables("tablaArbol").Columns("IdArbolPadre"), True)

        Return DS

        Session("DSLoaded") = 1

    End Function



    'Private Sub crearArbolRecursivo()
    '    Dim i As Integer
    '    For i = 0 To dstarbol.Tables("tablaCaucesPadres").Rows.Count - 1
    '        insertarNodosRecursivos(dstarbol.Tables("tablaCaucesPadres").Rows(i).Item("caucepadre"), Nothing, Nothing, -1)
    '    Next
    'End Sub
    Private Sub crearArbolRecursivoCauces()
        Dim i As Integer
        For i = 0 To dstarbol.Tables("tablaCaucesPadres").Rows.Count - 1
            insertarNodosCauces(Me.treeView1, dstarbol, dstarbol.Tables("tablaCaucesPadres").Rows(i).Item("caucepadre"), Nothing, Nothing, -1, False)
        Next
    End Sub




    Private Sub crearDataSetsPuntos()
        dstarbol.Clear()
        treeView1.Nodes.Clear()
        'Cauces Padre
        daArbol.SelectCommand.CommandText = "SELECT DISTINCT LEFT(C.codigoCauce,2) as caucepadre from [PVYCR_Cauces] C where left(C.codigoCauce,1)<>'S' " & _
        " and left(c.codigoCauce,2) <> 'AR' and left(c.codigoCauce,2)<> 'OT' " & _
       "UNION " & _
       "SELECT TOP 1 'ARG' AS caucepadre from PVYCR_Cauces " & _
       "UNION " & _
       "SELECT TOP 1 'OT000 hasta OT099' AS caucepadre from PVYCR_Cauces " & _
        "UNION " & _
        "SELECT TOP 1 'OT100 hasta OT999' AS caucepadre from PVYCR_Cauces " & _
        "UNION " & _
       "SELECT TOP 1 'S' AS caucepadre from PVYCR_Cauces"
        daArbol.Fill(dstarbol, "tablaCaucesPadres")
        'Cauces
        daArbol.SelectCommand.CommandText = "SELECT C.codigoCauce, C.DenominacionCauce FROM PVYCR_Cauces C order by C.codigoCauce "
        daArbol.Fill(dstarbol, "tablaCauces")
        'Puntos
        daArbol.SelectCommand.CommandText = "SELECT C.codigoCauce, P.codigoPVYCR, P.tipoPunto, P.denominacionPunto,P.TipoSensor TipoSensor FROM PVYCR_Cauces C " & _
      " join PVYCR_Puntos P on P.codigocauce=C.codigocauce order by C.codigoCauce, P.codigoPVYCR"
        daArbol.Fill(dstarbol, "tablaPuntos")
        'elementos de medida
        daArbol.SelectCommand.CommandText = "SELECT E.codigoPVYCR, E.idElementoMedida,E.TIPO, DESCTIPO = " & _
             "CASE E.Tipo " & _
             "WHEN 'Q' THEN 'CAUDALÍMETRO' " & _
             "WHEN 'H' THEN 'HORÓMETRO' " & _
             "WHEN 'V' THEN 'VOLUMÉTRICO' " & _
             "WHEN 'E' THEN 'ENERGÍA' " & _
            "END from PVYCR_Elementosmedida E, PVYCR_puntos P " & _
        "where(E.codigoPVYCR = P.codigoPVYCR) order by E.codigoPVYCR  "
        daArbol.Fill(dstarbol, "TablaElementosMedidad")

    End Sub
    Private Sub crearDataSetsPuntosBIS()


        dstarbol.Clear()
        treeView1.Nodes.Clear()

        'ARBOL NORMALIZADO
        daArbol.SelectCommand.CommandText = "Select * from dbo.PVYCR_ARBOL"
        daArbol.Fill(dstarbol, "tablaArbol")

        'Creamos selfrelation entre ambos tables
        'If dstarbol.Relations.Contains("SelfRefenceRelation") = False Then
        '    dstarbol.Relations.Add("SelfRefenceRelation", _
        '        dstarbol.Tables("tablaArbol").Columns("IdArbol"), _
        '        dstarbol.Tables("tablaArbol").Columns("IdArbolPadre"))
        'End If
    End Sub

    Private Sub crearDataSetsPuntosXML()

        dstarbol.Clear()
        treeView1.Nodes.Clear()

        'ARBOL NORMALIZADO
        daArbol.SelectCommand.CommandText = "Select * from dbo.PVYCR_ARBOL"
        daArbol.Fill(dstarbol, "tablaArbol")

        'Creamos selfrelation entre ambos tables
        If dstarbol.Relations.Contains("SelfRefenceRelation") = False Then
            dstarbol.Relations.Add("SelfRefenceRelation", _
                dstarbol.Tables("tablaArbol").Columns("IdArbol"), _
                dstarbol.Tables("tablaArbol").Columns("IdArbolPadre"), True)
            dstarbol.Relations("SelfRefenceRelation").Nested = True
        End If
    End Sub

    Private Sub crearDataSetsPuntosXML2()

        dstarbol.Clear()
        treeView1.Nodes.Clear()

        'ARBOL NORMALIZADO
        daArbol.SelectCommand.CommandText = "Select * from dbo.PVYCR_ARBOL"
        daArbol.Fill(dstarbol, "tablaArbol")

        dstarbol.Relations.Add("SelfRefenceRelation", _
                        dstarbol.Tables("tablaArbol").Columns("IdArbol"), _
                        dstarbol.Tables("tablaArbol").Columns("IdArbolPadre"), True)
        dstarbol.Relations("SelfRefenceRelation").Nested = True

    End Sub




    Protected Function obtenerCodigoPVYCR() As String
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        Return codigoPVYCR
    End Function

    Protected Sub imgBuscarCauce_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscarCauce.Click
        'EGB 22/07/2008
        'BUSQUEDA ORIGINAL SE DECIDE REEMPLAZARLA POR BUSQUEDA MULTIPLE
        'Dim rutanodobuscadodescripcion As String

        ''Búsqueda del nodo con el texto coincidente con la clave
        'rutanodobuscadodescripcion = ""

        'Dim rutanodobuscado = BuscarNodoTVW(treeView1, txtCauce.Text, rutanodobuscadodescripcion)

        'If rutanodobuscado = "" Then
        '    Alert(Page, "No Existen nodos con los criterios seleccionados")
        '    SetFocusToControl(Me, Me.txtCauce)
        '    txtCauce.Text = ""

        'Else
        '    'Representación del Nodo si la busqueda es positiva
        '    Dim nodobuscado As TreeNode = treeView1.FindNode(rutanodobuscado)

        '    treeView1.CollapseAll()
        '    ExpandirNodoRecursivo(nodobuscado)
        '    nodobuscado.Selected = True
        '    nodobuscado.Select()
        '    'ActivarPanelDetalles()
        'End If
        '--------------------------------------------------------------------
        'BuscarNodos(treeView1, txtCauce.Text, Me.lstBusquedasResult)

        'Cargar datos en el repeater de busquedas
        If txtCauce.Text.Length > 0 Then
            crearDataSetCauce()
            rptBusquedas.DataSource = dstarbol.Tables("TablaArbolAuxiliar")
            rptBusquedas.DataBind()
        End If

    End Sub
    Private Sub crearDataSetCauce()

        'EGB 22/07/2008 Crear Tabla en DataSet auxiliar para localizar elementos del Arbol de cualquier tipo
        sentenciaSel = "SELECT REPLACE(ClaveNodo,'#',';') as ClaveNodo2 ,* from PVYCR_Arbol WHERE UPPER(Descripcion) LIKE '%" & UCase(txtCauce.Text) & "%'"
        daArbol.SelectCommand.CommandText = sentenciaSel
        daArbol.Fill(dstarbol, "TablaArbolAuxiliar")
    End Sub
    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function

    Protected Sub imgExportaExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExportaExcel.Click
        Dim grd1 As New GridView()
        grd1.DataSource = GetDataSet()
        grd1.DataBind()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ArbolCauces.xls")
        Response.Charset = ""
        Response.ContentEncoding = Encoding.Unicode
        Response.BinaryWrite(Encoding.Unicode.GetPreamble())
        Me.EnableViewState = False
        Dim tw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        grd1.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
End Class
