Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO
Partial Class SICAH_CurvasAcequiasBUSCAR
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

        'NodoSeleccionadoArbol(Page, treeView1)
        'Dim txtLecturasVisor As String
        'lblCabecera.Text = genHTML.cabecera(Page)

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
            'If Session("DSLoaded") <> 1 Then crearArbolRecursivoBISBIS(treeView1, GetDataSet())

            'Me.treeView1.Attributes.Add("oncontextmenu", "RightClick(event);")
            'treeView1.Attributes.Add("onselectedindexchange", "CheckParent();")

            '---------------------------------------------------------------------------------------
            'EGB 21/08/2008 Cargamos los Elementos de Medida que tengan Regimen de Curva Asocidado
            'Cargar datos en el repeater de busquedas

            'EGB 28/08/2008 Modificación para visualizar en función del parametro Acequias Con o Sin Curva de Gasto
            'If Session("Acequias" & nullABlanco(Request.QueryString("ConCurva"))) <> 1 Then
            MostrarAcequias(nullABlanco(Request.QueryString("ConCurva")))
            'End If

            imgBuscarAcequiasConCurva.ToolTip = "Buscar Acequias " & IIf(Request.QueryString("ConCurva") = 1, "con", "sin") & " Curva..."
            txtBuscarAcequia.ToolTip = "Acequias " & IIf(Request.QueryString("ConCurva") = 1, "con", "sin") & " Curva de Gasto Asociada"

        End If
    End Sub
    Protected Sub MostrarAcequias(ByVal ConCurva As Integer)
        '---------------------------------------------------------------------------------------
        'EGB 21/08/2008 Cargamos los Elementos de Medida que tengan Regimen de Curva Asocidado
        'Cargar datos en el repeater de busquedas
        Dim cuenta As Integer

        crearDataSetAcequiasConCurvas(ConCurva)
        cuenta = dstarbol.Tables("TablaArbolAuxiliar").Rows.Count
        txtBuscarAcequia.ToolTip = "Elementos de medida con régimen de curva"
        Select Case cuenta
            'Case 0 : txtBuscarAcequia.Text = "[No existen Acequias " & IIf(ConCurva = 1, "con", "sin") & " rég. de curva]"
            'Case 1 : txtBuscarAcequia.Text = "[Existe una Acequia " & IIf(ConCurva = 1, "con", "sin") & " rég. de curva]"
            'Case Else : txtBuscarAcequia.Text = "[" & cuenta & " Acequias " & IIf(ConCurva = 1, "con", "sin") & " rég. de curva]"
            Case 0 : txtBuscarAcequia.Text = "[No existen Acequias]"
            Case 1 : txtBuscarAcequia.Text = "[Existe una Acequia ]"
            Case Else : txtBuscarAcequia.Text = "[" & cuenta & " Acequias]"
        End Select

        rptBusquedas.DataSource = dstarbol.Tables("TablaArbolAuxiliar")
        rptBusquedas.DataBind()
        '----------------------------------------------------------------------------------------
        Session("Acequias" & ConCurva) = 1
    End Sub

    Protected Function getLiteralEncabezado()
        getLiteralEncabezado = IIf(Request.QueryString("ConCurva") = 1, "Con", "Sin")
    End Function
   
    Private Sub crearDataSetAcequiasConCurvas(ByVal ConCurva As Integer)

        If dstarbol.Tables.Contains("TablaArbolAuxiliar") Then
            dstarbol.Tables("TablaArbolAuxiliar").Clear()
        End If
        If ConCurva = 1 Then
            'SENTENCIA PARA LOCALIZAR LAS ACEQUIAS QUE TIENEN CURVA DE GASTO ASOCIADA
            sentenciaSel = "SELECT PVYCR_Arbol.*, REPLACE(PVYCR_Arbol.ClaveNodo,'#',';')AS ClaveNodo2 " & _
                           "FROM dbo.PVYCR_Arbol " & _
                           "INNER JOIN " & _
                           "(SELECT CodigoPVYCR, idElementoMedida FROM PVYCR_CurvasAcequias GROUP BY CodigoPVYCR, idElementoMedida)TBLAcequiasConCurva " & _
                           "ON PVYCR_Arbol.CodigoPVYCR = TBLAcequiasConCurva.CodigoPVYCR " & _
                           "AND PVYCR_Arbol.IdElementoMedida = TBLAcequiasConCurva.idElementoMedida " & _
                           "WHERE UPPER( PVYCR_Arbol.Descripcion) LIKE '%" & UCase(Me.txtBuscarAcequia.Text) & "%'"
        Else
            'SENTENCIA PARA LOCALIZAR LA ACEQUIAS QUE NO TIENEN CURVA DE GASTO ASOCIADA
            sentenciaSel = "SELECT PVYCR_Arbol.*, REPLACE(PVYCR_Arbol.ClaveNodo,'#',';')AS ClaveNodo2 " & _
                           "FROM dbo.PVYCR_Arbol WHERE NOT EXISTS " & _
                           "(SELECT     CodigoPVYCR, idElementoMedida " & _
                           "FROM PVYCR_CurvasAcequias " & _
                           "GROUP BY CodigoPVYCR, idElementoMedida " & _
                           "HAVING PVYCR_Arbol.CodigoPVYCR = PVYCR_CurvasAcequias.CodigoPVYCR AND " & _
                           "PVYCR_Arbol.IdElementoMedida = PVYCR_CurvasAcequias.idElementoMedida) " & _
                           "AND  substring(PVYCR_Arbol.ClaveNodo,len(PVYCR_Arbol.ClaveNodo),1)='Q' " & _
                           "AND UPPER( PVYCR_Arbol.Descripcion) LIKE '%" & UCase(Me.txtBuscarAcequia.Text) & "%'"
        End If
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daArbol.SelectCommand.CommandText = sentenciaSel
        daArbol.Fill(dstarbol, "TablaArbolAuxiliar")


    End Sub
    
    Private Function NodoSinHijos(ByVal idarbol As Integer) As Boolean
        Dim misentenciaSel As String
        Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
        misentenciaSel = "SELECT * FROM PVYCR_ARBOL WHERE IdArbolPadre=" & idarbol
        utiles.Comprobar_Conexion_BD(Page, conexion)
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

    Protected Sub imgBuscarAcequiasConCurva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscarAcequiasConCurva.Click
        'EGB 28/08/2008 Refresca el Repeater de Acequias con Curvas
        MostrarAcequias(Request.QueryString("ConCurva"))
    End Sub

End Class
