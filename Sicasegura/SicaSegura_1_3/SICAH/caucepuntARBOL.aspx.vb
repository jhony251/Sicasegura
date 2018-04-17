Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO
Partial Class SICAH_caucepuntARBOL
    Inherits System.Web.UI.Page

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim conexion_visibles As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim daCaucesVisibles As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion_visibles)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim dstCaucesVisibles As System.Data.DataSet = New System.Data.DataSet()
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim codigoCauce As String = ""

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder, sentenciaSelCount As String
    Dim parfila As Integer = 0
    Dim numfila As Integer = 0


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
            'If Session("DSLoaded") <> 1 Then

            If treeView1.Nodes.Count = 0 Then
                crearArbolRecursivoBISBIS(treeView1, GetDataSet())
            End If
           
            'EGB 06/10/2008 La carga de Ramas en función de los roles, se realiza desde control jscript en cliente.
            '-----------------------------------------------------------------------------------------------------
            'NCM 18/09/2008 CARGAMOS LAS RAMAS QUE SON VISIBLES SEGÚN EL USUARIO
            'RamasVisibles()
            'MostrarOcultarDIVSNodosRaiz()
            '--------------------------------------------------------------------

            'Dim cookie As New HttpCookie("arrayPermisos", RamasVisiblesString())
            'Response.Cookies.Add(cookie)

            'ClientScript.RegisterStartupScript(Page.GetType, "mostrarRamasROLES", "<script language=javascript>mostrarRamasROLES();</script>")
            'crearArbolRecursivoBISBIS(treeView1, GetDataSet(), dstCaucesVisibles)
            'End If

            'Me.treeView1.Attributes.Add("oncontextmenu", "showmenuie5(event,'');")
            'treeView1.Attributes.Add("onselectedindexchange", "CheckParent();")
        End If
    End Sub


    Public Sub MostrarOcultarDIVSNodosRaiz()
        'Función que genera script de cliente para mostrar nodos raiz en función de los permisos del usuario
        Dim strScript As String

        'Recorremos la tabla TablaVisibles para construir la visibilidad del DIV
        '--------------------------------------------------------------------------------------------

        'strScript = "window.parent.frames['iframeArbol'].document.getElementById(miidDIV+'Nodes');"
        strScript = ""

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("ARG") = "N" Then
            strScript = strScript & "document.getElementById('treeView1n0').style.display='none';"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("CM") = "N" Then
            strScript = strScript & "document.getElementById('treeView1n2').style.display='none';"
        End If

        'If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("ET") = "N" Then
        '    strScript = strScript & "document.getElementById('treeView1n227').style.display='none';"
        'End If
        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("OT0") = "N" Then
            strScript = strScript & "document.getElementById('treeView1n227').style.display='none';"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("OT1") = "N" Then
            strScript = strScript & "document.getElementById('treeView1n273').style.display='none';"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("S") = "N" Then
            strScript = strScript & "document.getElementById('treeView1n676').style.display='none';"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VA") = "N" Then
            strScript = strScript & "document.getElementById('treeView1n1603').style.display='none';"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VB") = "N" Then
            strScript = strScript & "document.getElementById('treeView1n3128').style.display='none';"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VM") = "N" Then
            strScript = "document.getElementById('treeView1n3586').style.display='none';"
        End If

        'Registro del script de cliente
        'Page.RegisterStartupScript("MostrarNodosVisibles", "<script language=javascript>" & strScript & " </script>")

        ClientScript.RegisterStartupScript(Page.GetType, "MostrarNodosVisibles", "<script language=javascript>" & strScript & " </script>")
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
        'DS.Relations("SelfRefenceRelation").Nested = True
        Session("DSLoaded") = 1
        Return DS



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
    Private Sub RamasVisibles()
        'NCM 18/09/2008 rellenamos en un dataset los valores que tiene el usuario para saber qué ramas puede ver
        daCaucesVisibles.SelectCommand.CommandText = "SELECT ARG, CM, OT0, OT1,S,VA,VB,VM from TUsuarios where login= '" & Session("loginUsuario").ToString & "' "
        daCaucesVisibles.Fill(dstCaucesVisibles, "TablaVisibles")
    End Sub
    Private Function RamasVisiblesString()
        Dim str As String

        'NCM 18/09/2008 rellenamos en un dataset los valores que tiene el usuario para saber qué ramas puede ver

        daCaucesVisibles.SelectCommand.CommandText = "SELECT ARG, CM, OT0, OT1,S,VA,VB,VM from TUsuarios where login= '" & Session("loginUsuario").ToString & "' "

        daCaucesVisibles.Fill(dstCaucesVisibles, "TablaVisibles")

        'Construccion del string de permisos
        str = ""
        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("ARG") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("CM") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        'If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("ET") = "N" Then
        '    str = str & "none#"
        'Else
        '    str = str & "#"
        'End If
        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("OT0") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If
        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("OT1") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("S") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VA") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VB") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VM") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        Return str
    End Function

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        utiles.CerrarConexion(conexion)
    End Sub
End Class
