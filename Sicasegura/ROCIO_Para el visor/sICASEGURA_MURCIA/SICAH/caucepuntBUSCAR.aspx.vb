Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO
Partial Class SICAH_caucepuntBUSCAR
    Inherits System.Web.UI.Page

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim codigoCauce As String = ""

    Dim conexion_visibles As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim daCaucesVisibles As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion_visibles)
    Dim dstCaucesVisibles As System.Data.DataSet = New System.Data.DataSet()

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder, sentenciaSelCount, sentenciaWhere As String
    Dim parfila As Integer = 0
  Dim numfila As Integer = 0
  Dim e1 As System.Web.UI.ImageClickEventArgs

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
    'NodoSeleccionadoArbol(Page, treeView1)
    'Dim txtLecturasVisor As String

    If Not IsPostBack Then
      If Session("usuarioReg") = Nothing Then
        Response.Redirect("UsuarioNoRegistrado.aspx")
        Exit Sub
      End If
      'ncm 31/05/2010 si llamamos desde infoPTO.aspx forzamos la búsqueda
      If Request.QueryString("nodobusqueda") = "s" Then
        Dim valorBuscado As String() = Request.QueryString("valor").Split(";")
        txtCauce.Text = valorBuscado(0)
        imgBuscarCauce_Click(sender, e1)
        'llamada a expandirarbol del json
        ClientScript.RegisterStartupScript(Page.GetType, "ExpandirArbol", "<script language=javascript> expandirArbol('" & Request.QueryString("valor") & "')  </script>")
      End If
      '---
      '01/07/2008 EGB 
      'If rbVisor.Checked = True Then txtLecturasVisor = "V"
      'If rbLecturas.Checked = True Then txtLecturasVisor = "L"
      '----
      'If Session("DSLoaded") <> 1 Then crearArbolRecursivoBISBIS(treeView1, GetDataSet())

      'Me.treeView1.Attributes.Add("oncontextmenu", "RightClick(event);")
      'treeView1.Attributes.Add("onselectedindexchange", "CheckParent();")
    End If
  End Sub

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
        Dim cuenta As Integer

        If txtCauce.Text.Length > 0 Then
            crearDataSetCauce()
            cuenta = dstarbol.Tables("TablaArbolAuxiliar").Rows.Count

            Select Case cuenta
                Case 0 : txtCauce.Text = "[No se localizan coincidencias]"
                Case 1 : txtCauce.Text = "[Existe una coincidencia]"
                Case Else : txtCauce.Text = "[Resultados " & cuenta & " registros maestros]"
            End Select
            
            'rptBusquedas.DataSource = dstarbol.Tables("TablaArbolAuxiliar")

        End If
    rptBusquedas.DataBind()

    End Sub
    Private Sub crearDataSetCauce()

        'EGB 22/07/2008 Crear Tabla en DataSet auxiliar para localizar elementos del Arbol de cualquier tipo
        'sentenciaSel = "SELECT REPLACE(ClaveNodo,'#',';') as ClaveNodo2 ,* from PVYCR_Arbol WHERE UPPER(Descripcion) LIKE '%" & UCase(txtCauce.Text) & "%'"
        'EGB 05/08/2008 Crear Tabla en DataSet auxiliar para localizar elementos del Arbol de cualquier tipo
        'y además localizar el id de control de cliente al que se asociará el nodo. Aproximación por ordenacion.
        Dim fila As DataRow
        Dim subfila As DataRow
    'Dim l_InStrRoles As String
        'EGB 29/05/2009
        utiles.Comprobar_Conexion_BD(Page, conexion)
    'ncm 22/04/2010 comentado ya que los roles se controlan en el desplieguearbol.vb
    'l_InStrRoles = InStrRoles()

    'sentenciaSel = "SELECT *, Tipo +'1' as background_color, " & _
    '               "REPLACE(ClaveNodo,'#',';') as ClaveNodo2 , REPLACE(Descripcion, '""', '')" & " as Descripcion2, REPLACE(ParajeToma, '""', '')" & " as ParajeToma " & _
    '               "FROM PVYCR_ARBOL " & _
    '               " WHERE UPPER(Descripcion) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%" & UCase(txtCauce.Text) & "%'" & _
    '               " OR UPPER(ParajeToma) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%" & UCase(txtCauce.Text) & "%'" & _
    '               " OR UPPER(NombreTitular) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%" & UCase(txtCauce.Text) & "%'" & _
    '               " AND StrNodoPadre IN (" & l_InStrRoles & ")"
    sentenciaSel = "SELECT *, Tipo +'1' as background_color, " & _
                         "REPLACE(ClaveNodo,'#',';') as ClaveNodo2 , REPLACE(Descripcion, '""', '')" & " as Descripcion2, REPLACE(ParajeToma, '""', '')" & " as ParajeToma " & _
                         "FROM PVYCR_ARBOL " & _
                         " WHERE UPPER(Descripcion) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%" & UCase(txtCauce.Text) & "%'" & _
                         " OR UPPER(ParajeToma) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%" & UCase(txtCauce.Text) & "%'" & _
                         " OR UPPER(NombreTitular) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%" & UCase(txtCauce.Text) & "%'"

        sentenciaSel = sentenciaSel & sentenciaWhere
        daArbol.SelectCommand.CommandText = sentenciaSel
        daArbol.Fill(dstarbol, "TablaArbolAuxiliar")
        '------------------------------------------------------------------------------------
        Dim SentenciaSubSel As String
        Dim Criterio As String()
        Dim TblArbolAuxiliar As New DataTable

        TblArbolAuxiliar = dstarbol.Tables("TablaArbolAuxiliar").Clone
    For Each fila In dstarbol.Tables("TablaArbolAuxiliar").Rows

      Criterio = Split(fila("ClaveNodo"), "#")
      SentenciaSubSel = "SELECT *, Tipo+'0' as background_color,  " & _
                        "REPLACE(ClaveNodo,'#',';') as ClaveNodo2 , REPLACE(Descripcion, '""', '')" & " as Descripcion2, REPLACE(ParajeToma, '""', '')" & " as ParajeToma " & _
                        "FROM PVYCR_ARBOL  " & _
                        " WHERE ClaveNodo  LIKE '" & Criterio(0) & "%' "
      daArbol.SelectCommand.CommandText = SentenciaSubSel
      daArbol.Fill(dstarbol, "TablaArbolAuxiliarSubSelects")

      For Each subfila In dstarbol.Tables("TablaArbolAuxiliarSubSelects").Rows
        TblArbolAuxiliar.Rows.Add(subfila.ItemArray())
      Next subfila
    Next fila

        'Aplicamos DISTINCT sobre el DataTable usando un DataView
        Dim DataViewArbolAuxiliar As New DataView(TblArbolAuxiliar)
        Dim TblArbolAxiliarDistinct As New DataTable

        TblArbolAxiliarDistinct = DataViewArbolAuxiliar.ToTable(True)

        'Ordenacion de Resultados
        TblArbolAxiliarDistinct.Select("", "ClaveNodo")

        'Asignación al origen del repeater de busquedas
        rptBusquedas.DataSource = TblArbolAxiliarDistinct

        
        

    End Sub

    Private Function InStrRoles()
        Dim str As String
        daCaucesVisibles.SelectCommand.CommandText = "SELECT ARG, CM, OT0, OT1,S,VA,VB,VM from TUsuarios where login= '" & Session("loginUsuario").ToString & "' "
        daCaucesVisibles.Fill(dstCaucesVisibles, "TablaVisibles")

        'Construccion del string de permisos
        str = ""
        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("ARG") = "S" Then
            str = str & "'ARG',"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("CM") = "S" Then
            str = str & "'CM',"
        End If

        'If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("ET") = "S" Then
        '    str = str & "'ET',"
        'End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("OT0") = "S" Then
            str = str & "'OT0',"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("OT1") = "S" Then
            str = str & "'OT1',"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("S") = "S" Then
            str = str & "'S',"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VA") = "S" Then
            str = str & "'VA',"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VB") = "S" Then
            str = str & "'VB',"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VM") = "S" Then
            str = str & "'VM',"
        End If

        If Right(str, 1) = "," Then
            str = Mid(str, 1, Len(str) - 1)
        End If

        Return str
    End Function

    Private Function NodoSinHijos(ByVal idarbol As Integer) As Boolean
        Dim misentenciaSel As String
        Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        misentenciaSel = "SELECT * FROM PVYCR_ARBOL WHERE IdArbolPadre=" & idarbol
        daArbol.SelectCommand.CommandText = misentenciaSel
        daArbol.Fill(dstarbol, "TablaNodosHijos")
        If dstarbol.Tables("TablaNodosHijos").Rows.Count > 0 Then
            'El Nodo Tiene Hijos 
            NodoSinHijos = False
        Else
            NodoSinHijos = True
        End If
        dstarbol.Tables("TablaNodosHijos").Dispose()
    End Function
    Private Function RutaNodoCliente(ByVal idarbol As Integer) As String
        'EGB 06/08/2008 Funcion que retorna los DIVS de cliente que serán necesarios mostrar para el
        'posicionamiento mediante jscript en la Busqueda de Nodos desde CaucePuntBUSQUEDAS.

        Dim sentenciaSel As String
        Dim idDIVNodosHijos As String
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = "SELECT REPLACE(SubSELECT_PVYCRARBOL.idnodoTreeView,'treeView1t','treeView1n') as idDIVNodosHijos, SubSELECT_PVYCRARBOL.idnodoTreeView, REPLACE(ClaveNodo,'#',';') as ClaveNodo2 ,* from PVYCR_Arbol INNER JOIN (select idarbol, 'treeView1t' + cast(idarbol - (SELECT MIN(idarbol)FROM (SELECT idarbol FROM pvycr_arbol where x is not null)tbl_arbol ) as varchar(10)) AS idnodoTreeView from pvycr_arbol where x is not null)SubSELECT_PVYCRARBOL  ON SubSELECT_PVYCRARBOL.IdArbol = PVYCR_Arbol.IdArbol WHERE SubSELECT_PVYCRARBOL.IdArbol=" & idarbol
        daArbol.SelectCommand.CommandText = sentenciaSel
        daArbol.Fill(dstarbol, "TablaArbolAuxiliar" & idarbol)
        If dstarbol.Tables("TablaArbolAuxiliar" & idarbol).Rows.Count > 0 Then
            idDIVNodosHijos = dstarbol.Tables("TablaArbolAuxiliar" & idarbol).Rows(0).Item("idDIVNodosHijos")
            If Not (dstarbol.Tables("TablaArbolAuxiliar" & idarbol).Rows(0).Item("IdArbolPadre") Is System.DBNull.Value) Then
                'Una vez localizado el DIV del padre se hace llamada recursiva hasta llegar al nivel superior
                RutaNodoCliente = idDIVNodosHijos & "#" & RutaNodoCliente(dstarbol.Tables("TablaArbolAuxiliar" & idarbol).Rows(0).Item("IdArbolPadre"))
            Else
                RutaNodoCliente = idDIVNodosHijos
            End If
        Else
            RutaNodoCliente = ""
        End If
        dstarbol.Tables("TablaArbolAuxiliar" & idarbol).Dispose()
    End Function

    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function

End Class
