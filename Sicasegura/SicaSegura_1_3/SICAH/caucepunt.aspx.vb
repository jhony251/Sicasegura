Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System

Partial Class SICAH_caucepunt
  Inherits System.Web.UI.Page

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    'Dim conexion_visibles As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    'Dim daCaucesVisibles As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion_visibles)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    'Dim dstCaucesVisibles As System.Data.DataSet = New System.Data.DataSet()
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim codigoCauce As String = ""

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder, sentenciaSelCount As String
    Dim parfila As Integer = 0
    Dim numfila As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim nodoBuscado As TreeNode
        formateo_controles_cliente()
        IncrustarLoadEvent()
        Page.MaintainScrollPositionOnPostBack = True
        If Not IsPostBack Then
            lblCabecera.Text = genHTML.cabecera(Page)
            lblPestanyas.Text = genHTML.pestanyasMenu(4, "../", Page, Session("idperfil"), Session("usuarioReg"))
            If utiles.nullABlanco(Request.QueryString("ruta")) <> "" Then
                nodoBuscado = treeView1.FindNode(Request.QueryString("ruta").ToString)
                nodoBuscado.Selected = True
                nodoBuscado.Select()

            Else
                crearDataSetsPuntos()
                crearArbolRecursivoCauces()
                'crearArbolRecursivo()

            End If
        End If
    End Sub
    Private Sub IncrustarLoadEvent()
        'Script de Cliente Necesario para mantener el scroll del panel contendor del arbol de sistemas.
        Dim script As String = _
          "function LoadEvent()" + _
          "{{" + _
          " try" + _
          " {{" + _
          "   var elem = document.getElementById('{0}_SelectedNode');" + _
          "   if(elem != null )" + _
          "   {{" + _
          "     var node = document.getElementById(elem.value);" + _
          "     if(node != null)" + _
          "     {{" + _
          "       node.scrollIntoView(true);" + _
          "       {1}.scrollLeft = 0;" + _
          "     }}" + _
          "   }}" + _
          " }}" + _
          " catch(oException)" + _
          " {{}}" + _
          "}}"

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "LoadEvent", _
          String.Format(script, Me.treeView1.ClientID, pnlArbol.ClientID), True)
    End Sub
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

    'Private Sub insertarNodosRecursivos(ByVal cauce As String, ByVal denominacionCauce As String, ByVal elNodoPadre As TreeNode, ByVal nivel As Integer)
    '    Dim registros(), registros2() As DataRow
    '    Dim i, j As Integer
    '    Dim tn, te As TreeNode

    '    Dim v_ot, tiposensor As String
    '    'código bueno

    '    'Select Case nivel
    '    '  Case -1
    '    '    If cauce = "OT000 hasta OT099" Or cauce = "OT100 hasta OT999" Then
    '    '      v_ot = cauce
    '    '      cauce = "OT"
    '    '    End If
    '    '    If cauce <> "OT" Then
    '    '      tn = New TreeNode("<b>" & cauce & " " & verDescPrefijo(cauce) & "</b>")
    '    '    Else
    '    '      tn = New TreeNode("<b>" & v_ot & " " & verDescPrefijo(v_ot) & "</b>")
    '    '    End If
    '    '    tn.Expanded = False
    '    '    Me.treeView1.Nodes.Add(tn)

    '    '    registros = dstarbol.Tables("tablaCauces").Select("codigoCauce LIKE '" & cauce & "%'")
    '    '    For i = 0 To registros.Length - 1
    '    '      If cauce <> "OT" Then
    '    '        If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 Then
    '    '          insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
    '    '        End If
    '    '      Else
    '    '        If v_ot = "OT000 hasta OT099" Then
    '    '          If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 And Integer.Parse(Mid(registros(i)("codigoCauce").ToString(), 3, 3)) < 100 Then
    '    '            insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
    '    '          End If

    '    '        ElseIf v_ot = "OT100 hasta OT999" Then
    '    '          If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 And Integer.Parse(Mid(registros(i)("codigoCauce").ToString(), 3, 3)) >= 100 Then
    '    '            insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
    '    '          End If

    '    '        End If
    '    '      End If
    '    '    Next
    '    '  Case Else

    '    '    tn = New TreeNode(cauce & " - " & denominacionCauce)
    '    '    elNodoPadre.ChildNodes.Add(tn)
    '    '    elNodoPadre.Expanded = False
    '    '    registros = dstarbol.Tables("tablaPuntos").Select("codigoCauce = '" & cauce & "'")

    '    '    For i = 0 To registros.Length - 1
    '    '      If registros(i).Item("TipoSensor") Is System.DBNull.Value Then
    '    '        tiposensor = ")"
    '    '      Else
    '    '        tiposensor = " - " & registros(i).Item("TipoSensor").ToString & ")"
    '    '      End If
    '    '      If registros(i)("tipoPunto").ToString = "M" Then
    '    '        tn.ChildNodes.Add(New TreeNode("<img src='images/puntoMotor.ico' border=0>&nbsp;&nbsp;" & "<font color=black>" & registros(i).Item("codigoPVYCR").ToString() & "(M" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</font>"))
    '    '      Else
    '    '        tn.ChildNodes.Add(New TreeNode("<img src='images/puntoAcequia2.gif' border=0>&nbsp;&nbsp;" & "<font color=black>" & registros(i).Item("codigoPVYCR").ToString() & "(G" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</font>"))
    '    '      End If
    '    '      tn.Expanded = False
    '    '    Next

    '    '    registros = dstarbol.Tables("tablaCauces").Select("codigoCauce LIKE '" & cauce & "%' AND codigoCauce <> '" & cauce & "'")
    '    '    For i = 0 To registros.Length - 1
    '    '      If numeroGuiones(registros(i)("codigoCauce").ToString()) = nivel + 1 Then
    '    '        insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
    '    '      End If
    '    '    Next
    '    'End Select
    '    ' fin código bueno

    '    Select Case nivel
    '        Case -1
    '            If cauce = "OT000 hasta OT099" Or cauce = "OT100 hasta OT999" Then
    '                v_ot = cauce
    '                cauce = "OT"
    '            End If
    '            If cauce <> "OT" Then
    '                tn = New TreeNode("<b>" & cauce & " " & verDescPrefijo(cauce) & "</b>", cauce & "#C")
    '            Else
    '                tn = New TreeNode("<b>" & v_ot & " " & verDescPrefijo(v_ot) & "</b>", cauce & "#C")
    '            End If
    '            tn.Expanded = False
    '            Me.treeView1.Nodes.Add(tn)

    '            registros = dstarbol.Tables("tablaCauces").Select("codigoCauce LIKE '" & cauce & "%'")
    '            For i = 0 To registros.Length - 1
    '                If cauce <> "OT" Then
    '                    If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 Then
    '                        insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
    '                    End If
    '                Else
    '                    If v_ot = "OT000 hasta OT099" Then
    '                        If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 And Integer.Parse(Mid(registros(i)("codigoCauce").ToString(), 3, 3)) < 100 Then
    '                            insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
    '                        End If

    '                    ElseIf v_ot = "OT100 hasta OT999" Then
    '                        If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 And Integer.Parse(Mid(registros(i)("codigoCauce").ToString(), 3, 3)) >= 100 Then
    '                            insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
    '                        End If

    '                    End If
    '                End If
    '            Next
    '        Case Else
    '            'Formato de Cauces y Subcauces

    '            If nivel < 1 Then
    '                tn = New TreeNode("<font color=" & GuarderiaFluvial.utiles.GradienteNivelesAzules(nivel) & "><b>" & cauce & " " & denominacionCauce & "</b>", cauce & "#C")
    '            Else
    '                tn = New TreeNode("<font color=" & GuarderiaFluvial.utiles.GradienteNivelesAzules(nivel) & "><b>" & cauce & " </b>" & denominacionCauce, cauce & "#C")
    '            End If

    '            registros = dstarbol.Tables("tablaCauces").Select("codigoCauce LIKE '" & cauce & "%' AND codigoCauce <> '" & cauce & "'")
    '            For i = 0 To registros.Length - 1
    '                If numeroGuiones(registros(i)("codigoCauce").ToString()) = nivel + 1 Then
    '                    insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
    '                End If
    '            Next


    '            elNodoPadre.ChildNodes.AddAt(elNodoPadre.ChildNodes.Count, tn)
    '            elNodoPadre.Expanded = False
    '            registros = dstarbol.Tables("tablaPuntos").Select("codigoCauce = '" & cauce & "'")

    '            For i = 0 To registros.Length - 1
    '                'obtenemos datos de elementos medida
    '                registros2 = dstarbol.Tables("TablaElementosMedidad").Select("CodigoPVYCR = '" & registros(i).Item("CodigoPVYCR").ToString & "'")

    '                If registros(i).Item("TipoSensor") Is System.DBNull.Value Then
    '                    tiposensor = ")"
    '                Else
    '                    tiposensor = " - " & registros(i).Item("TipoSensor").ToString & ")"
    '                End If
    '                If registros(i)("tipoPunto").ToString = "M" Then
    '                    'esto estaba antes de insertar elementos de medida
    '                    'tn.ChildNodes.Add(New TreeNode("<img src='images/puntoMotor.ico' border=0>&nbsp;&nbsp;" & "<font color=black>" & registros(i).Item("codigoPVYCR").ToString() & "(M" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</font>"))
    '                    te = New TreeNode("<img src='images/puntoMotor.ico' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & "<b>" & registros(i).Item("codigoPVYCR").ToString() & " (M" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</b></font>", registros(i).Item("codigoPVYCR").ToString() & "#P")
    '                    tn.ChildNodes.AddAt(tn.ChildNodes.Count, te)
    '                    'Insertamos elementos medida
    '                    For j = 0 To registros2.Length - 1
    '                        te.ChildNodes.Add(New TreeNode("<img src='images/distancias.gif' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & registros(i).Item("codigoPVYCR").ToString() & "-" & registros2(j).Item("idelementoMedida").ToString & " - " & registros2(j).Item("DESCTIPO").ToString, registros2(j).Item("codigoPVYCR").ToString & "#E#" & registros2(j).Item("tipo").ToString))
    '                    Next
    '                    'fin no pasar
    '                Else
    '                    'esto estaba antes de insertar elementos de medida
    '                    'tn.ChildNodes.Add(New TreeNode("<img src='images/puntoAcequia2.gif' border=0>&nbsp;&nbsp;" & "<font color=black>" & registros(i).Item("codigoPVYCR").ToString() & "(G" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</font>"))
    '                    te = New TreeNode("<img src='images/puntoAcequia2.gif' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & "<b>" & registros(i).Item("codigoPVYCR").ToString() & " (G" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "<b/></font>", registros(i).Item("codigoPVYCR").ToString() & "#P")
    '                    tn.ChildNodes.AddAt(tn.ChildNodes.Count, te)
    '                    'insertamos elementos medida
    '                    For j = 0 To registros2.Length - 1
    '                        te.ChildNodes.Add(New TreeNode("<img src='images/distancias.gif' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & registros(i).Item("codigoPVYCR").ToString() & "-" & registros2(j).Item("idelementoMedida").ToString & " - " & registros2(j).Item("DESCTIPO").ToString, registros2(j).Item("codigoPVYCR").ToString & "#E#" & registros2(j).Item("tipo").ToString))
    '                    Next
    '                End If
    '                tn.Expanded = False
    '                te.Expanded = False
    '            Next



    '    End Select

    'End Sub

    'Private Function numeroGuiones(ByVal cadena As String) As Integer
    '    Return Math.Max(Split(cadena, "-").Length - 1, 0)
    'End Function
    'Private Function numeroSeparadores(ByVal cadena As String) As Integer
    '    Return Math.Max(Split(cadena, "#").Length - 1, 0)
    'End Function

    'Protected Function verDescPrefijo(ByVal prefijo As String) As String
    '    Dim descripcion As String

    '    Select Case prefijo
    '        Case "ARG"
    '            descripcion = "Acequias"
    '        Case "CM"
    '            descripcion = "Cabecera - Río Mundo"
    '        Case "VA"
    '            descripcion = "Vega Alta"
    '        Case "VM"
    '            descripcion = "Vega Media"
    '        Case "VB"
    '            descripcion = "Vega Baja"
    '        Case "OT000 hasta OT099"
    '            descripcion = "Residuales EDAR's"
    '        Case "OT100 hasta OT999"
    '            descripcion = "Pozos de CHS"
    '        Case "S"
    '            descripcion = "Pozos Subterráneas"
    '    End Select

    '    Return descripcion
    'End Function


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
             "WHEN 'Q' THEN 'CAUDAL' " & _
             "WHEN 'H' THEN 'HORAS' " & _
             "WHEN 'V' THEN 'VOLUMEN' " & _
             "WHEN 'E' THEN 'ENERGÍA' " & _
            "END from PVYCR_Elementosmedida E, PVYCR_puntos P " & _
        "where(E.codigoPVYCR = P.codigoPVYCR) order by E.codigoPVYCR  "
        daArbol.Fill(dstarbol, "TablaElementosMedidad")

    End Sub


    Protected Sub treeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView1.SelectedNodeChanged
        ActivarPanelDetalles()
    End Sub
    Protected Sub ActivarPanelDetalles()
        Dim codigoPVYCR, tipoElem, nomElem, idelemen As String
        Dim guiones As Integer = 0
        'LIMPIAMOS TODOS LOS POSIBLES FILTROS
        txtFiltroFechaFin.Text = ""
        txtfiltroFechaIni.Text = ""
        txtFiltrarCodFuenteDato.Text = ""
        chkFiltroNulasQ.Checked = False
        txtFiltroNregQ.Text = ""

        txtFiltroFechaFinV.Text = ""
        txtFiltroFechaIniV.Text = ""
        txtFiltrarCodFuenteDatoV.Text = ""
        chkFiltroNulasV.Checked = False
        txtFiltroNRegV.Text = ""

        txtFiltroFechaFinE.Text = ""
        txtFiltroFechaIniE.Text = ""
        txtFiltrarCodFuenteDatoE.Text = ""
        txtFiltroNRegE.Text = ""
        chkFiltroNulasE.Checked = False
        'chkFiltroPorDiaE.Checked = False

        txtFiltroFechaFinH.Text = ""
        txtFiltroFechaIniH.Text = ""
        txtFiltrarCodFuenteDatoH.Text = ""
        txtFiltroNRegH.Text = ""

        'Obtenemos el número de guines, si tiene dos es porque es un elemento de medida
        guiones = numeroSeparadores(treeView1.SelectedValue.ToString)
        If guiones = 2 Then
            'parámetro código
            codigoPVYCR = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
            'parámetro tipo de elemento
            tipoElem = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
            ''parámetro nombre elemento
            nomElem = Replace(treeView1.SelectedNode.Text, "<img src='images/distancias.gif' border=0>&nbsp;&nbsp;<font color=#008500>" & treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4) & "-", "")
            idelemen = nomElem.Substring(0, nomElem.ToString.IndexOf("-") - 1)
            lblidElemento.Text = idelemen

            'Cargaremos todas las lecturas para el elemento seleccionado en el arbol
            Cargar_Elementos(tipoElem, codigoPVYCR, idelemen)
        End If
    End Sub


    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ucPaginacionE.ruta = "../"
        'ucPaginacionA.ruta = "../"
        'ucPaginacionV.ruta = "../"
        'ucPaginacionH.ruta = "../"
    End Sub

    Protected Function checkFila(ByVal tipo As String, ByVal elDataItem As DataRowView) As String
        parfila = (parfila + 1) Mod 2
        If tipo = "V" Then
            If elDataItem("Diferencial") < 0 Then
                Return "class=""filaDiferencial"""
            Else
                Return "class=""fila" & parfila & """"
            End If
        ElseIf tipo = "E" Then
            If elDataItem("Diferencial") < 0 Then
                Return "class=""filaDiferencial"""
            Else
                Return "class=""fila" & parfila & """"
            End If
        Else
            Return "class=""fila" & parfila & """"
        End If
    End Function
    Protected Function obtenerNomElemento() As String
        Return Replace(treeView1.SelectedNode.Text, "<img src='images/distancias.gif' border=0>&nbsp;&nbsp;<font color=#008500>", "")
    End Function
    Protected Function obtenerAnyoHidrologico() As String
        Return "Lecturas del: 01/10/" & DateTime.Now.Year - 1 & " - " & DateTime.Today
    End Function
    Protected Function obtenerNumLecturasQ() As String

        Return "Total lecturas cargadas: " & dstElementos.Tables("TablaAcequias").Rows.Count

    End Function
    Protected Function obtenerNumLecturasE() As String
        Return "Total lecturas cargadas: " & dstElementos.Tables("TablaAlimentacion").Rows.Count
    End Function
    Protected Function obtenerNumLecturasV() As String
        Return "Total lecturas cargadas: " & dstElementos.Tables("TablaMotores").Rows.Count
    End Function

    Protected Function obtenerNumLecturasH() As String
        Return "Total lecturas cargadas: " & dstElementos.Tables("TablaHorometros").Rows.Count
    End Function
    Protected Function obtenerTotNumLecturas(ByVal tipo As String, ByVal codigoPVYCR As String, ByVal idelemento As String) As String
        Dim sentenciaSel, v_codigo, v_idelemento As String
        Dim TotNumlecturasQ, TotNumlecturasE, TotNumlecturasH, TotNumlecturasV As Double
        v_idelemento = idelemento.Substring(idelemento.LastIndexOf("-") - 4, 3)
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        If tipo = "Q" Then
            'If dstElementos.Tables("tablaAcequias").Rows.Count > 0 Then
            sentenciaSel = "SELECT Count(*) TotNumLecturasQ " & _
                    "FROM PVYCR_DatosAcequias where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                    "idElementoMedida =  '" & v_idelemento & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            TotNumlecturasQ = String.Format("{0:#,##0.##}", daElementos.SelectCommand.ExecuteScalar())
            'Else
            '    TotNumlecturasQ = "0"
            'End If
            Return "Total lecturas existentes: " & TotNumlecturasQ
        ElseIf tipo = "E" Then
            'If dstElementos.Tables("tablaAlimentacion").Rows.Count > 0 Then
            sentenciaSel = "SELECT Count(*) TotNumLecturasE " & _
                    "FROM PVYCR_DatosAlimentacion where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                    "idElementoMedida =  '" & v_idelemento & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            TotNumlecturasE = String.Format("{0:#,##0.##}", daElementos.SelectCommand.ExecuteScalar())
            'Else
            '    TotNumlecturasE = "0"
            'End If
            Return "Total lecturas existentes: " & TotNumlecturasE
        ElseIf tipo = "V" Then
            'If dstElementos.Tables("tablaMotores").Rows.Count > 0 Then
            sentenciaSel = "SELECT Count(*) TotNumLecturasV " & _
                            "FROM PVYCR_DatosMotores where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                            "idElementoMedida =  '" & v_idelemento & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            TotNumlecturasV = String.Format("{0:#,##0.##}", daElementos.SelectCommand.ExecuteScalar())
            'Else
            'TotNumlecturasV = "0"
            'End If
            Return "Total lecturas existentes: " & TotNumlecturasV
        ElseIf tipo = "H" Then
            'If dstElementos.Tables("tablaHorometros").Rows.Count > 0 Then
            sentenciaSel = "SELECT Count(*) TotNumLecturasH " & _
                                "FROM PVYCR_DatosHorometros where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                                "idElementoMedida =  '" & v_idelemento & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            TotNumlecturasH = String.Format("{0:#,##0.##}", daElementos.SelectCommand.ExecuteScalar())
            'Else
            '    TotNumlecturasH = "0"
            'End If
            Return "Total lecturas existentes: " & TotNumlecturasH
        Else
            Return 0
        End If

    End Function
    Private Sub crearDataSets_Elementos(ByVal tipoElemen As String, ByVal codigoPVYCR As String, ByVal idelemento As String)
        'Criterios de filtrado
        Dim sFiltro As String = ""
        Dim fechainicio, fechaFin As DateTime
        Dim Nreg As Integer = 0
        Dim i As Integer = 0

        'dependiendo del tipo seleccionaremos los datos de una tabla u otra
        'calculamos el año hidrológico que va desde el 01/10/añoactual - 1 hasta la fecha actual
        fechainicio = "01/10/" & DateTime.Now.Year - 1
        fechaFin = DateTime.Today
        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        dstElementos.Clear()

        If tipoElemen = "Q" Then
            'scripts de cliente para el calendario
            imgCalFechaIniQ.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtfiltroFechaIni.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinQ.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFin.ClientID & "'),'dd/mm/yyyy');")
            If txtFiltroNregQ.Text <> "" Then
                sentenciaSel = "SELECT top " & txtFiltroNregQ.Text & " d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " & _
                                   ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD, F.FUENTE_DATOS DescFuenteDato " & _
                                   "FROM PVYCR_DatosAcequias D " & _
                                   "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                   "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                                   "idElementoMedida =  '" & idelemento & "' and " & _
                                   "Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
            Else
                sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " & _
                   ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD, F.FUENTE_DATOS DescFuenteDato " & _
                   "FROM PVYCR_DatosAcequias D " & _
                   "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                   "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                   "idElementoMedida =  '" & idelemento & "' and " & _
                   "Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
            End If
            'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
            ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
            If chkFiltroNulasQ.Checked = False Then
                sentenciaSel = sentenciaSel & " and d.caudal_M3S is not null "
            End If

            'sentenciaSel = "SELECT CodigoPVYCR,idElementoMedida, Cod_Fuente_Dato,Fecha_Medida,Escala_M,Calado_M,Observaciones " & _
            '        ",TipoObtencionCaudal,RegimenCurva,NumeroParada,Caudal_M3S,DUDA_CALIDAD " & _
            '        "FROM PVYCR_DatosAcequias where codigoPVYCR = '" & codigoPVYCR & "' and " & _
            '        "idElementoMedida =  '" & idelemento & "' "

            If (txtFiltroFechaFin.Text <> "" And txtfiltroFechaIni.Text <> "") Or txtFiltrarCodFuenteDato.Text <> "" Then
                'sFiltro = " and cast(datepart(dd,Fecha_Medida) as varchar(2))+'/'+ cast(datepart(mm,Fecha_Medida)as varchar(2))+'/' + cast(datepart(yy,Fecha_Medida)as varchar(4)) = " & _
                '"cast(datepart(dd,'" & txtfiltroFechaIni.Text & "') as varchar(2))+'/'+ cast(datepart(mm,'" & txtfiltroFechaIni.Text & "')as varchar(2))+'/' + cast(datepart(yy,'" & txtfiltroFechaIni.Text & "')as varchar(4))"
                If txtFiltrarCodFuenteDato.Text <> "" Then
                    sFiltro = " and d.Cod_Fuente_Dato = '" & txtFiltrarCodFuenteDato.Text & "'"
                End If
                If txtFiltroFechaFin.Text <> "" And txtfiltroFechaIni.Text <> "" Then
                    sFiltro = sFiltro & " and Fecha_medida between '" & txtfiltroFechaIni.Text & "' and '" & txtFiltroFechaFin.Text & "'"
                End If
            ElseIf txtFiltroFechaFin.Text = "" And txtfiltroFechaIni.Text <> "" Then
                Alert(Page, "La Fecha Hasta no puede ser nula")
                sFiltro = ""
            ElseIf txtFiltroFechaFin.Text <> "" And txtfiltroFechaIni.Text = "" Then
                Alert(Page, "La Fecha Desde no puede ser nula")
                sFiltro = ""
            Else
                sFiltro = ""
            End If

            sentenciaOrder = " order by codigoPVYCR, Fecha_Medida"

            If sFiltro <> "" Then
                sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
                sentenciaSelCount = sentenciaSelCount & sFiltro
            Else
                sentenciaSel = sentenciaSel & sentenciaOrder
            End If

            daElementos.SelectCommand.CommandText = sentenciaSel
            'datos acequias
            'daElementos.Fill(dstElementos, (CInt(ucPaginacionA.lblPaginatext) - 1) * ucPaginacionA.pageSize, ucPaginacionA.pageSize, "TablaAcequias")
            daElementos.Fill(dstElementos, "TablaAcequias")
            'Cálculo del número de páginas
            Dim txtComando As String = ""
            txtComando = daElementos.SelectCommand.CommandText
            txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
            'ucPaginacionA.calcularPags(txtComando)
            '        ElseIf Request.QueryString("tipo").ToString = "E" Then
            obtenerVolumenDiferencial("Q")
            obtenerCaudalAcumulado()
        ElseIf tipoElemen = "E" Then
            'scripts de cliente para el calendario
            imgCalFechaIniE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniE.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinE.ClientID & "'),'dd/mm/yyyy');")
            '------------------------------------------------------------------------------------------------------------------------------------
            'NCM: 1º comprobaremos si el filtro de nº de regitrsos tiene valor, ya que si tiene valor le tendremos que
            'poner el top a la select. Éste filtro junto con el de mostrar 1 registro por día y el de mostrar 1 de cada X son excluyentes.
            '1) si quiieren N registros haremos el top de la select
            '2) si quieren un registro por día, haremos un bucle para obtener un registro cada día, teniendo en cuenta los demás filtros
            '3) si kieren uno de cada X
            '------------------------------------------------------------------------------------------------------------------------------------
            'CASO1
            'If txtFiltroNRegE.Text <> "" And chkFiltroPorDiaE.Checked = False And txtFiltroUnoCadaXE.Text = "" Then
            If txtFiltroNRegE.Text <> "" Then

                sentenciaSel = "SELECT top " & txtFiltroNRegE.Text & " D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
                                "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
                                "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato " & _
                                "FROM PVYCR_DatosAlimentacion D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
                '            ElseIf txtFiltroNRegE.Text = "" And chkFiltroPorDiaE.Checked = False Then
            ElseIf txtFiltroNRegE.Text = "" Then
                sentenciaSel = "SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
                                "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
                                "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato " & _
                                "FROM PVYCR_DatosAlimentacion D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "


            End If
            If sentenciaSel <> "" Then
                'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
                ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
                If chkFiltroNulasE.Checked = False Then

                    sentenciaSel = sentenciaSel & " and d.Total_kwh is not null "

                End If
                'sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                '                "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
                '                "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
                '                "IV.descripcion descIncElec " & _
                '                "FROM PVYCR_DatosAlimentacion D " & _
                '                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
                '                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "



                If (txtFiltroFechaFinE.Text <> "" And txtFiltroFechaIniE.Text <> "") Or txtFiltrarCodFuenteDatoE.Text <> "" Then
                    If txtFiltrarCodFuenteDatoE.Text <> "" Then
                        sFiltro = " and d.Cod_Fuente_Dato = '" & txtFiltrarCodFuenteDatoE.Text & "'"
                    End If
                    If txtFiltroFechaFinE.Text <> "" And txtFiltroFechaIniE.Text <> "" Then
                        sFiltro = sFiltro & " and Fecha_medida between '" & txtFiltroFechaIniE.Text & "' and '" & txtFiltroFechaFinE.Text & "'"
                    End If
                ElseIf txtFiltroFechaFinE.Text = "" And txtFiltroFechaIniE.Text <> "" Then
                    Alert(Page, "La Fecha Hasta no puede ser nula")
                    sFiltro = ""
                ElseIf txtFiltroFechaFinE.Text <> "" And txtFiltroFechaIniE.Text = "" Then
                    Alert(Page, "La Fecha Desde no puede ser nula")
                    sFiltro = ""
                End If

                sentenciaOrder = " order by codigoPVYCR, Fecha_Medida"

                If sFiltro <> "" Then
                    sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
                Else
                    sentenciaSel = sentenciaSel & sentenciaOrder
                End If

                daElementos.SelectCommand.CommandText = sentenciaSel
                'CASO3
                'si ha marcado el filtro de uno de cada x 
                'If txtFiltroUnoCadaXE.Text <> "" Then
                '    'si existendatos en la tabla alimentacion los limpiamos
                '    'si no existe el datatble lo creamos
                '    Dim dtaux As DataTable
                '    daElementos.Fill(dstElementos, "TablaAlimentacionAux")
                '    If dstElementos.Tables.Contains("TablaAlimentacion") Then
                '        dstElementos.Tables("TablaAlimentacion").Clear()
                '    Else
                '        dtaux = dstElementos.Tables("TablaAlimentacionAux").Clone()
                '        dtaux.TableName = "TablaAlimentacion"
                '        dstElementos.Tables.Add(dtaux)
                '    End If



                '    For i = 0 To dstElementos.Tables("TablaAlimentacionAux").Rows.Count - 1
                '        If i Mod txtFiltroUnoCadaXE.Text = 0 Then
                '            'añadimos una nueva fila a la tabla allimentacion para poder copiar los datos de la auxiliar
                '            'Dim row As DataRow = dstAux.Tables("TablaAlimentacionAux").NewRow

                '            'row.ItemArray = dstAux.Tables("TablaAlimentacionAux").Rows(i).ItemArray
                '            'dstElementos.Tables("TablaAlimentacion").Rows.Add(row)
                '            dstElementos.Tables("TablaAlimentacion").Rows.Add(dstElementos.Tables("TablaAlimentacionAux").Rows(i).ItemArray)
                '        End If
                '    Next
                'End If
                ''datos energía
                ''daElementos.Fill(dstElementos, (CInt(ucPaginacionE.lblPaginatext) - 1) * ucPaginacionE.pageSize, ucPaginacionE.pageSize, "TablaAlimentacion")
                daElementos.Fill(dstElementos, "TablaAlimentacion")
            End If
            'CASO2
            'If chkFiltroPorDiaE.Checked = True Then
            '    Dim FiltroNulas, FiltroCodFuenteDato As String
            '    'si el usuario ha puesto fechas en el filtro esas serán las que tenemos que tener en cuenta, sino
            '    'será el año hisdrológico
            '    If txtFiltroFechaFinE.Text <> "" And txtFiltroFechaIniE.Text <> "" Then
            '        fechainicio = txtFiltroFechaIniE.Text
            '        fechaFin = txtFiltroFechaFinE.Text

            '    Else
            '        fechainicio = "01/10/" & DateTime.Now.Year - 1
            '        fechaFin = DateTime.Today
            '    End If
            '    'copiamos las fecha en unas varianles para poder utilizarlas 
            '    'fechainiciopaginacion = fechainicio
            '    'fechafinpaginacion = fechaFin
            '    'Filtro Nulas
            '    If chkFiltroNulasE.Checked = False Then

            '        FiltroNulas = " and d.Total_kwh is not null "

            '    End If
            '    'Filtro código fuente dato
            '    If txtFiltrarCodFuenteDatoE.Text <> "" Then
            '        FiltroCodFuenteDato = " and d.Cod_Fuente_Dato = '" & txtFiltrarCodFuenteDatoE.Text & "'"
            '    End If
            '    If txtFiltroNRegE.Text <> "" Then

            '        'si existendatos en la tabla alimentacion los limpiamos
            '        If dstElementos.Tables.Contains("TablaAlimentacion") Then
            '            dstElementos.Tables("TablaAlimentacion").Clear()
            '        End If

            '        Do While (fechainicio < fechaFin And Nreg < txtFiltroNRegE.Text + 1)
            '            sentenciaSel = "Select top 1 D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
            '                            "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
            '                            "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
            '                            "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato " & _
            '                            "FROM PVYCR_DatosAlimentacion D " & _
            '                            "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
            '                            "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
            '                            "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
            '                            "convert(nvarchar(15),Fecha_Medida,103) = '" & fechainicio & "' "
            '            'le añadimos los filtros
            '            If FiltroNulas <> "" Then
            '                sentenciaSel = sentenciaSel & FiltroNulas
            '            End If
            '            If FiltroCodFuenteDato <> "" Then
            '                sentenciaSel = sentenciaSel & FiltroCodFuenteDato
            '            End If
            '            sentenciaSel = sentenciaSel & FiltroNulas & FiltroCodFuenteDato
            '            'meteremos la información en un dataset.
            '            daElementos.SelectCommand.CommandText = sentenciaSel
            '            'datos energía
            '            'daElementos.Fill(dstElementos, (CInt(ucPaginacionE.lblPaginatext) - 1) * ucPaginacionE.pageSize, ucPaginacionE.pageSize, "TablaAlimentacion")
            '            daElementos.Fill(dstElementos, "TablaAlimentacion")
            '            'avanzamos al siguiente día
            '            fechainicio = DateAdd(DateInterval.Day, 1, fechainicio)
            '            'comprobamos si se ha cargado registro en el dataset, si es así avanzamos en el nreg
            '            'esto lo hacemos así para que realmente muestre el nº de regitros k ha pedido el usuario
            '            'y no haga X itraciones sin encontrar datos
            '            Nreg = dstElementos.Tables("TablaAlimentacion").Rows.Count + 1
            '            'Nreg = Nreg + 1
            '        Loop
            '        'sentenciaSel = Replace(Replace(sentenciaSel, "top 1", ""), "Fecha_Medida = '" & fechainicio & "' ", )
            '        'daElementos.SelectCommand.CommandText = sentenciaSel
            '    Else
            '        'si existendatos en la tabla alimentacion los limpiamos
            '        If dstElementos.Tables.Contains("TablaAlimentacion") Then
            '            dstElementos.Tables("TablaAlimentacion").Clear()
            '        End If
            '        Do While fechainicio < fechaFin
            '            sentenciaSel = "Select top 1 D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
            '                            "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
            '                            "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
            '                            "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato " & _
            '                            "FROM PVYCR_DatosAlimentacion D " & _
            '                            "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
            '                            "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
            '                            "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
            '                            "convert(nvarchar(15),Fecha_Medida,103) = '" & fechainicio & "' "
            '            'le añadimos los filtros
            '            If FiltroNulas <> "" Then
            '                sentenciaSel = sentenciaSel & FiltroNulas
            '            End If
            '            If FiltroCodFuenteDato <> "" Then
            '                sentenciaSel = sentenciaSel & FiltroCodFuenteDato
            '            End If
            '            sentenciaSel = sentenciaSel & FiltroNulas & FiltroCodFuenteDato
            '            'meteremos la información en un dataset.
            '            daElementos.SelectCommand.CommandText = sentenciaSel
            '            'datos energía
            '            'daElementos.Fill(dstElementos, (CInt(ucPaginacionE.lblPaginatext) - 1) * ucPaginacionE.pageSize, ucPaginacionE.pageSize, "TablaAlimentacion")
            '            daElementos.Fill(dstElementos, "TablaAlimentacion")
            '            'avanzamos al siguiente día
            '            fechainicio = DateAdd(DateInterval.Day, 1, fechainicio)
            '        Loop
            '        'sentenciaSel = Replace(Replace(sentenciaSel, "top 1", ""), "Fecha_Medida = '" & fechainicio & "' ", )
            '        'daElementos.SelectCommand.CommandText = sentenciaSel

            '    End If
            'End If

            'Cálculo del número de páginas
            'Dim txtComando As String = ""
            'txtComando = daElementos.SelectCommand.CommandText
            'txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
            'ucPaginacionE.calcularPags(txtComando)
            '        ElseIf Request.QueryString("tipo").ToString = "V" Then
            obtenerVolumenDiferencial("E")
            obtenerVolumenElectricoAcumulado()
        ElseIf tipoElemen = "V" Then
            'scripts de cliente para el calendario
            imgCalFechaIniV.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniV.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinV.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinV.ClientID & "'),'dd/mm/yyyy');")
            If txtFiltroNRegV.Text <> "" Then
                sentenciaSel = "SELECT top " & txtFiltroNRegV.Text & " D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                               "D.LecturaContador_M3, D.Funciona, D.Justificacion, D.Observaciones, D.idIncidenciaVolumetrica, " & _
                               "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                               "IV.descripcion DescIncVol, F.FUENTE_DATOS DescFuenteDato " & _
                               "FROM PVYCR_DatosMotores D " & _
                               "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                               "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
            Else
                sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                               "D.LecturaContador_M3, D.Funciona, D.Justificacion, D.Observaciones, D.idIncidenciaVolumetrica, " & _
                               "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                               "IV.descripcion DescIncVol, F.FUENTE_DATOS DescFuenteDato " & _
                               "FROM PVYCR_DatosMotores D " & _
                               "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                               "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "

            End If
            'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
            ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
            If chkFiltroNulasV.Checked = False Then
                sentenciaSel = sentenciaSel & " and d.lecturacontador_m3 is not null "
            End If

            If (txtFiltroFechaFinV.Text <> "" And txtFiltroFechaIniV.Text <> "") Or txtFiltrarCodFuenteDatoV.Text <> "" Then
                If txtFiltrarCodFuenteDatoV.Text <> "" Then
                    sFiltro = " and d.Cod_Fuente_Dato = '" & txtFiltrarCodFuenteDatoV.Text & "'"
                End If
                If txtFiltroFechaFinV.Text <> "" And txtFiltroFechaIniV.Text <> "" Then
                    sFiltro = sFiltro & " and Fecha_medida between '" & txtFiltroFechaIniV.Text & "' and '" & txtFiltroFechaFinV.Text & "'"
                End If
            ElseIf txtFiltroFechaFinV.Text = "" And txtFiltroFechaIniV.Text <> "" Then
                Alert(Page, "La Fecha Hasta no puede ser nula")
                sFiltro = ""
            ElseIf txtFiltroFechaFinV.Text <> "" And txtFiltroFechaIniV.Text = "" Then
                Alert(Page, "La Fecha Desde no puede ser nula")
                sFiltro = ""
            Else
                sFiltro = ""
            End If

            sentenciaOrder = " order by D.codigoPVYCR, D.Fecha_Medida"

            If sFiltro <> "" Then
                sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
            Else
                sentenciaSel = sentenciaSel & sentenciaOrder
            End If

            daElementos.SelectCommand.CommandText = sentenciaSel
            'datos volumétricos
            '            daElementos.Fill(dstElementos, (CInt(ucPaginacionV.lblPaginatext) - 1) * ucPaginacionV.pageSize, ucPaginacionV.pageSize, "TablaMotores")
            daElementos.Fill(dstElementos, "TablaMotores")
            'Cálculo del número de páginas
            Dim txtComando As String = ""
            txtComando = daElementos.SelectCommand.CommandText
            txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
            'ucPaginacionV.calcularPags(txtComando)
            obtenerVolumenDiferencial("V")
            obtenerVolumenAcumulado()

        ElseIf tipoElemen = "H" Then
            'scripts de cliente para el calendario
            imgCalFechaIniH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniH.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinH.ClientID & "'),'dd/mm/yyyy');")

            If txtFiltroNRegH.Text <> "" Then
                sentenciaSel = "SELECT top " & txtFiltroNRegH.Text & " D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
                                "D.HorasIntervalo, D.idElementoMedida, " & _
                                "D.Funciona, D.Observaciones, D.idIncidenciaVolumetrica, " & _
                                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                                "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato " & _
                                "FROM PVYCR_DatosHorometros D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "

            Else
                sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
                                "D.HorasIntervalo, D.idElementoMedida, " & _
                                "D.Funciona, D.Observaciones, D.idIncidenciaVolumetrica, " & _
                                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                                "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato " & _
                                "FROM PVYCR_DatosHorometros D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
            End If
            'sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_medida," & _
            '                        "D.HorasIntervalo, D.idElementoMedida, " & _
            '                        "D.Funciona, D.Observaciones, D.idIncidenciaVolumetrica, " & _
            '                        "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
            '                        "IV.descripcion descIncVol " & _
            '                        "FROM PVYCR_DatosHorometros D " & _
            '                        "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
            '                        "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "


            If (txtFiltroFechaFinH.Text <> "" And txtFiltroFechaIniH.Text <> "") Or txtFiltrarCodFuenteDatoH.Text <> "" Then
                If txtFiltrarCodFuenteDatoH.Text <> "" Then
                    sFiltro = " and D.Cod_Fuente_Dato = '" & txtFiltrarCodFuenteDatoH.Text & "'"
                End If
                If txtFiltroFechaFinH.Text <> "" And txtFiltroFechaIniH.Text <> "" Then
                    sFiltro = sFiltro & " and Fecha_Medida between '" & txtFiltroFechaIniH.Text & " 00:00:00' and '" & txtFiltroFechaFinH.Text & " 23:59:59' "
                ElseIf txtFiltroFechaFinH.Text = "" And txtFiltroFechaIniH.Text <> "" Then
                    Alert(Page, "La Fecha Desde no puede ser nula")
                ElseIf txtFiltroFechaFinH.Text <> "" And txtFiltroFechaIniH.Text = "" Then
                    Alert(Page, "La Fecha Hasta no puede ser nula")
                End If
            ElseIf txtFiltroFechaFinH.Text = "" And txtFiltroFechaIniH.Text <> "" Then
                Alert(Page, "La Fecha Hasta no puede ser nula")
                sFiltro = ""
            ElseIf txtFiltroFechaFinH.Text <> "" And txtFiltroFechaIniH.Text = "" Then
                Alert(Page, "La Fecha Desde no puede ser nula")
                sFiltro = ""
            Else
                sFiltro = ""
            End If

            sentenciaOrder = " order by D.codigoPVYCR, D.Fecha_medida"

            If sFiltro <> "" Then
                sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
            Else
                sentenciaSel = sentenciaSel & sentenciaOrder
            End If

            daElementos.SelectCommand.CommandText = sentenciaSel
            'datos horómetros
            'daElementos.Fill(dstElementos, (CInt(ucPaginacionH.lblPaginatext) - 1) * ucPaginacionH.pageSize, ucPaginacionH.pageSize, "TablaHorometros")
            daElementos.Fill(dstElementos, "TablaHorometros")
            'Cálculo del número de páginas
            Dim txtComando As String = ""
            txtComando = daElementos.SelectCommand.CommandText
            txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
            'ucPaginacionH.calcularPags(txtComando)
            obtenerVolumenDiferencial("H")
        End If

    End Sub


    Protected Sub btnFiltroAceptarA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptarA.Click
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        'ucPaginacionA.lblPaginatext = "1"


        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)

        rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
        rptAcequias.DataBind()
        'ucPaginacionA.lblNumpaginasDatabind()

    End Sub
    Protected Sub btnFiltrocancelarA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelarA.Click
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)

        'ucPaginacionA.lblPaginatext = "1"
        txtfiltroFechaIni.Text = ""
        txtFiltroFechaFin.Text = ""
        txtFiltrarCodFuenteDato.Text = ""
        chkFiltroNulasQ.Checked = False
        txtFiltroNregQ.Text = ""

        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)

        rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
        rptAcequias.DataBind()
        'ucPaginacionA.lblNumpaginasDatabind()
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'If ucPaginacionA.lblMuevetext = "si" Then
        '    'declaramos dentro las variables, porque al hacer el load la primera vez el árbol aún no está cargado
        '    ' y da error
        '    Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        '    Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)

        '    crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        '    rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
        '    rptAcequias.DataBind()
        '    ucPaginacionA.lblMuevetext = "no"
        'End If
        'If ucPaginacionE.lblMuevetext = "si" Then
        '    Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        '    Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)

        '    crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        '    rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacion")
        '    rptEnergia.DataBind()
        '    ucPaginacionE.lblMuevetext = "no"
        'End If
        'If ucPaginacionV.lblMuevetext = "si" Then
        '    Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        '    Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)

        '    crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        '    rptVolumen.DataSource = dstElementos.Tables("TablaMotores")
        '    rptVolumen.DataBind()
        '    ucPaginacionV.lblMuevetext = "no"
        'End If
        'If ucPaginacionH.lblMuevetext = "si" Then
        '    Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        '    Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)

        '    crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        '    rptHorometros.DataSource = dstElementos.Tables("TablaHorometros")
        '    rptHorometros.DataBind()
        '    ucPaginacionH.lblMuevetext = "no"
        'End If

    End Sub

    Protected Sub Cargar_Elementos(ByVal tipoElemen As String, ByVal codigoPVYCR As String, ByVal idElemento As String)
        Page.MaintainScrollPositionOnPostBack = True

        'If Not IsPostBack Then
        lblCabecera.Text = genHTML.cabecera(Page)
        lblPestanyas.Text = genHTML.pestanyasMenu(4, "../", Page, Session("idperfil"), Session("usuarioReg"))
        'esto es para la paginacion NO borrar
        'If Request.QueryString("pag") <> "" Then
        '    ucPaginacionA.lblPaginatext = Request.QueryString("pag")
        '    ucPaginacionE.lblPaginatext = Request.QueryString("pag")
        '    ucPaginacionV.lblPaginatext = Request.QueryString("pag")
        '    ucPaginacionH.lblPaginatext = Request.QueryString("pag")
        'Else
        '    ucPaginacionA.lblPaginatext = "1"
        '    ucPaginacionE.lblPaginatext = "1"
        '    ucPaginacionV.lblPaginatext = "1"
        '    ucPaginacionH.lblPaginatext = "1"
        'End If

        'antes de cargar los elementos ponemos el filtro de nulo deschekeado por si
        'lo habían marcado anteriormente
        chkFiltroNulasE.Checked = False
        'chkFiltroPorDiaE.Checked = False
        chkFiltroNulasQ.Checked = False
        chkFiltroNulasV.Checked = False
        'creamos datasets
        crearDataSets_Elementos(tipoElemen, codigoPVYCR, idElemento)
        'ponemos visibles los paneles y cargamos los datos en pantalla

        If tipoElemen = "Q" Then
            pnlAcequias.Visible = True
            pnlEnergia.Visible = False
            pnlVolumen.Visible = False
            pnlHorometros.Visible = False
            pnlEDAcequias.Visible = False
            pnlEDEnergia.Visible = False
            pnlEDVolumen.Visible = False
            pnlEDHorometros.Visible = False
            'obtenemos
            rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
            rptAcequias.DataBind()
            'ucPaginacionA.lblNumpaginasDatabind()
        ElseIf tipoElemen = "E" Then
            pnlAcequias.Visible = False
            pnlEnergia.Visible = True
            pnlVolumen.Visible = False
            pnlHorometros.Visible = False
            pnlHorometros.Visible = False
            pnlEDAcequias.Visible = False
            pnlEDEnergia.Visible = False
            pnlEDVolumen.Visible = False
            pnlEDHorometros.Visible = False

            rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacion")
            rptEnergia.DataBind()
            'ucPaginacionE.lblNumpaginasDatabind()
        ElseIf tipoElemen = "V" Then
            pnlAcequias.Visible = False
            pnlEnergia.Visible = False
            pnlVolumen.Visible = True
            pnlHorometros.Visible = False
            pnlHorometros.Visible = False
            pnlEDAcequias.Visible = False
            pnlEDEnergia.Visible = False
            pnlEDVolumen.Visible = False
            pnlEDHorometros.Visible = False

            rptVolumen.DataSource = dstElementos.Tables("TablaMotores")
            rptVolumen.DataBind()
            'ucPaginacionV.lblNumpaginasDatabind()
        ElseIf tipoElemen = "H" Then
            pnlAcequias.Visible = False
            pnlEnergia.Visible = False
            pnlVolumen.Visible = False
            pnlHorometros.Visible = True
            pnlEDAcequias.Visible = False
            pnlEDEnergia.Visible = False
            pnlEDVolumen.Visible = False
            pnlEDHorometros.Visible = False

            rptHorometros.DataSource = dstElementos.Tables("TablaHorometros")
            rptHorometros.DataBind()
            'ucPaginacionH.lblNumpaginasDatabind()
        End If

    End Sub

    Protected Sub btnCancelarA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarA.Click
        pnlAcequias.Visible = False
        pnlEnergia.Visible = False
        pnlVolumen.Visible = False
        pnlHorometros.Visible = False
        'deseleccionamos el nodo para que si el usuario vuelve a seleccionar el mismo salte el selectednode.changed
        'que es el procedimiento que carga todos los datos
        treeView1.SelectedNode.Selected = False
    End Sub

    Protected Sub btnFiltroAceptarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptarE.Click
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)

        'ucPaginacionE.lblPaginatext = "1"


        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacion")
        rptEnergia.DataBind()
        'ucPaginacionE.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnFiltroCancelarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelarE.Click
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)

        'ucPaginacionE.lblPaginatext = "1"
        txtFiltroFechaIniE.Text = ""
        txtFiltroFechaFinE.Text = ""
        txtFiltrarCodFuenteDatoE.Text = ""
        chkFiltroNulasE.Checked = False
        txtFiltroNRegE.Text = ""
        'chkFiltroPorDiaE.Checked = False

        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacion")
        rptEnergia.DataBind()
        'ucPaginacionA.lblNumpaginasDatabind()

    End Sub

    Protected Sub btnCancelarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarE.Click
        pnlAcequias.Visible = False
        pnlEnergia.Visible = False
        pnlVolumen.Visible = False
        pnlHorometros.Visible = False

        'deseleccionamos el nodo para que si el usuario vuelve a seleccionar el mismo salte el selectednode.changed
        'que es el procedimiento que carga todos los datos
        treeView1.SelectedNode.Selected = False
    End Sub


    Protected Sub btnFiltroAceptarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptarV.Click
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        'ucPaginacionV.lblPaginatext = "1"

        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)

        rptVolumen.DataSource = dstElementos.Tables("TablaMotores")
        rptVolumen.DataBind()
        'ucPaginacionV.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnFiltroCancelarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelarV.Click
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)

        'ucPaginacionV.lblPaginatext = "1"
        txtFiltroFechaIniV.Text = ""
        txtFiltroFechaFinV.Text = ""
        txtFiltrarCodFuenteDatoV.Text = ""
        chkFiltroNulasV.Checked = False
        txtFiltroNRegV.Text = ""

        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)

        rptVolumen.DataSource = dstElementos.Tables("TablaMotores")
        rptVolumen.DataBind()
        'ucPaginacionV.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnCancelarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarV.Click
        pnlAcequias.Visible = False
        pnlEnergia.Visible = False
        pnlVolumen.Visible = False
        pnlHorometros.Visible = False

        'deseleccionamos el nodo para que si el usuario vuelve a seleccionar el mismo salte el selectednode.changed
        'que es el procedimiento que carga todos los datos
        treeView1.SelectedNode.Selected = False
    End Sub

    Protected Function obtenerVolumenAcumulado() As String
        Dim volumen As Double
        Dim total As String
        'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
        If dstElementos.Tables("TablaMotores").Rows.Count > 0 Then
            '---------------código que utilizabamos antes de obtener el diferencial-----------------
            'volumenIni = Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(0).Item("LecturaContador_M3").ToString)
            'volumenFin = Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(dstElementos.Tables("TablaMotores").Rows.Count - 1).Item("LecturaContador_M3").ToString)
            'total = String.Format("{0:#,##0.##}", volumenFin - volumenIni)
            'Return total
            '----------------------------------------------------------------------------------------
            If dstElementos.Tables("TablaMotores").Rows(dstElementos.Tables("TablaMotores").Rows.Count - 1).Item("Diferencial_Acum").ToString = "" Then
                total = ""
            Else
                volumen = dstElementos.Tables("TablaMotores").Rows(dstElementos.Tables("TablaMotores").Rows.Count - 1).Item("Diferencial_Acum").ToString
                total = String.Format("{0:#,##0.##}", volumen)
            End If

            Return total
        Else
            Return "0"
        End If
    End Function
    Protected Function obtenerVolumenElectricoAcumulado() As String
        Dim volumen As Decimal
        Dim total As String
        'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
        If dstElementos.Tables("TablaAlimentacion").Rows.Count > 0 Then
            '---------------código que utilizabamos antes de obtener el diferencial-----------------
            'volumenIni = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(0).Item("Total_kwh").ToString)
            'volumenFin = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(dstElementos.Tables("TablaAlimentacion").Rows.Count - 1).Item("Total_kwh").ToString)
            'total = String.Format("{0:#,##0.##}", volumenFin - volumenIni)
            'Return total
            '----------------------------------------------------------------------------------------
            If dstElementos.Tables("TablaAlimentacion").Rows(dstElementos.Tables("TablaAlimentacion").Rows.Count - 1).Item("Diferencial_Acum").ToString() = "" Then
                total = ""
            Else
                volumen = dstElementos.Tables("TablaAlimentacion").Rows(dstElementos.Tables("TablaAlimentacion").Rows.Count - 1).Item("Diferencial_Acum").ToString
                total = String.Format("{0:#,##0.##}", volumen)
            End If

            Return total
        Else
            Return "0"
        End If
    End Function
    Protected Function obtenerCaudalAcumulado() As String
        Dim volumen As Decimal
        Dim total As String
        'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
        If dstElementos.Tables("TablaAcequias").Rows.Count > 0 Then
            If dstElementos.Tables("TablaAcequias").Rows(dstElementos.Tables("TablaAcequias").Rows.Count - 1).Item("Diferencial_Acum").ToString() = "" Then
                total = ""
            Else
                volumen = dstElementos.Tables("TablaAcequias").Rows(dstElementos.Tables("TablaAcequias").Rows.Count - 1).Item("Diferencial_Acum").ToString
                total = String.Format("{0:#,##0.##}", volumen)
            End If

            Return total

        Else
            Return "0"
        End If

    End Function

    Protected Function obtenerCodigoPVYCR() As String
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        Return codigoPVYCR
    End Function

    Protected Sub btnFiltroAceptarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptarH.Click
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        'ucPaginacionH.lblPaginatext = "1"


        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        rptHorometros.DataSource = dstElementos.Tables("TablaHorometros")
        rptHorometros.DataBind()
        'ucPaginacionH.lblNumpaginasDatabind()

    End Sub

    Protected Sub btnFiltroCancelarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelarH.Click
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)

        'ucPaginacionH.lblPaginatext = "1"
        txtFiltroFechaIniH.Text = ""
        txtFiltroFechaFinH.Text = ""
        txtFiltrarCodFuenteDatoH.Text = ""
        txtFiltroNRegH.Text = ""

        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)

        rptHorometros.DataSource = dstElementos.Tables("TablaHorometros")
        rptHorometros.DataBind()
        'ucPaginacionH.lblNumpaginasDatabind()

    End Sub

    Protected Sub btnCancelarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarH.Click
        pnlAcequias.Visible = False
        pnlEnergia.Visible = False
        pnlVolumen.Visible = False
        pnlHorometros.Visible = False
        'deseleccionamos el nodo para que si el usuario vuelve a seleccionar el mismo salte el selectednode.changed
        'que es el procedimiento que carga todos los datos
        treeView1.SelectedNode.Selected = False
    End Sub

    Protected Sub rptEnergia_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptEnergia.ItemCommand
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        Dim nombre As String
        Dim i As Integer
        Select Case e.CommandName
            Case "editar"
                'mostramos el panel de edición
                lblFechamedidaESel.Text = e.CommandArgument
                pnlEnergia.Visible = False
                'ucPaginacionE.Visible = False
                pnlEDEnergia.Visible = True
                pnlEDVolumen.Visible = False
                pnlEDAcequias.Visible = False

                creardatasetEd(lblidElemento.Text, codigoPVYCR, lblFechamedidaESel.Text)

                If dstElementos.Tables("TablaEdAlimentacion").Rows.Count > 0 Then
                    'txtCodigoPVYCR.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("CodigoPVYCR"))
                    txtFechaMedida.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("Fecha_medida"))
                    txtCodFuenteDato.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("Cod_fuente_Dato")) & " - " & utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("DescFuenteDato"))
                    'txtidElementoMedida.Text = utiles.nullACero(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("idElementoMedida"))
                    txtLecturaI.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("LecturaI")))
                    txtLecturaII.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("LecturaII")))
                    txtLecturaIII.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("LecturaIII")))
                    txtTotal_Kwh.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("Total_Kwh")))
                    txtTotal_Kvar.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("Total_Kvar")))


                    rellenarListas(lblidElemento.Text, codigoPVYCR)

                    'Si no tiene incidencias cargaremos toda la lista para que puedan seleccionar una
                    'If utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("idIncidenciaElectrica").ToString) = "" Then


                    'Else

                    ' si tiene incidencia comprobaremos con cual de la lista coincide y la pondremos como la seleccionada

                    ddlIncidenciasE.SelectedValue = dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("idIncidenciaElectrica").ToString

                    txtJustificacion.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("Justificacion"))
                    '                    ddlIncidenciasE.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("descIncElec"))
                    txtConsumoElectricoAdicional.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("ConsumoElectricoadicional")))
                    txtReinicioLecturaElectrica.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("ReinicioLecturaElectrica")))
                    txtObservaciones.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("Observaciones"))
                    nombre = checkNombreAlimentacion()
                End If

            Case "borrar"

                If conexion.State = ConnectionState.Closed Then conexion.Open()

                'comando.CommandText = "delete from pvycr_contadores where idContador=0" & e.CommandArgument

                comando.ExecuteNonQuery()

                'crearDataSets()
                'ucPaginacion.lblNumpaginasDatabind()
        End Select
    End Sub
    Protected Sub creardatasetEd(ByVal idelemento As String, ByVal codigoPVYCR As String, ByVal fechaMedida As DateTime)
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        If idelemento.Substring(0, 1).ToString = "E" Then

            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
                                "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
                                "IV.descripcion descIncElec, P.DenominacionPunto, F.FUENTE_DATOS DescFuenteDato " & _
                                "FROM PVYCR_DatosAlimentacion D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
                                "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida = '" & fechaMedida & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdAlimentacion")
            'datos energía
            'rptEnergia.DataSource = dstElementos.Tables("TablaEdAlimentacion")
            'rptEnergia.DataBind()
        ElseIf idelemento.Substring(0, 1).ToString = "Q" Then
            sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida,d.Cod_Fuente_Dato,d.Fecha_Medida,d.Escala_M,d.Calado_M " & _
                    ",d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.TipoObtencionCaudal,d.Duda_Calidad,d.Observaciones, p.denominacionpunto, F.FUENTE_DATOS DescFuenteDato " & _
                    "FROM PVYCR_DatosAcequias D " & _
                    "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                    "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                    "WHERE d.idElementoMedida = '" & idelemento & "' and d.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                    "d.Fecha_Medida = '" & fechaMedida & "' "
            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdAcequias")
        ElseIf idelemento.Substring(0, 1).ToString = "V" Then

            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.lecturaContador_m3, D.Funciona, D.Observaciones, D.idIncidenciavolumetrica, " & _
                                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, D.Justificacion, " & _
                                "IV.descripcion descIncVol, P.DenominacionPunto, F.FUENTE_DATOS DescFuenteDato " & _
                                "FROM PVYCR_DatosMotores D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida = '" & fechaMedida & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdMotores")
        ElseIf idelemento.Substring(0, 1).ToString = "H" Then

            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.HorasIntervalo, D.Funciona, D.Observaciones, D.idIncidenciavolumetrica, " & _
                                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                                "IV.descripcion descIncH, P.DenominacionPunto, F.FUENTE_DATOS DescFuenteDato " & _
                                "FROM PVYCR_DatosHorometros D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida = '" & fechaMedida & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdHorometros")


        End If

    End Sub
    Protected Function checkNombreAlimentacion() As String
        If utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("CodigoPVYCR")) <> "" Then
            lblTitulo.Text = "EDICIÓN DATOS ALIMENTACIÓN: " & utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("CodigoPVYCR")) & "  " & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("DenominacionPunto")) & " /" & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("idElementoMedida"))
            Return utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("CodigoPVYCR")) & "  " & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("DenominacionPunto")) & " /" & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("idElementoMedida"))
        Else
            Return "EDICIÓN DATOS ALIMENTACIÓN"
        End If

    End Function

    Protected Sub btnAceptarEDEnergia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarEDEnergia.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)

        If conexion.State = ConnectionState.Closed Then conexion.Open()

        comando.CommandText = "UPDATE PVYCR_DatosAlimentacion " & _
                               "SET [LecturaI] = @LecturaI " & _
                                  ",[LecturaII] = @LecturaII " & _
                                  ",[LecturaIII] = @LecturaIII " & _
                                  ",[Total_KWH] = @Total_KWH " & _
                                  ",[Total_Kvar] = @Total_Kvar " & _
                                  ",[Funciona] = @Funciona " & _
                                  ",[Observaciones] = @Observaciones " & _
                                  ",[Justificacion] = @Justificacion " & _
                                  ",[idIncidenciaElectrica] = @idIncidenciaElectrica " & _
                                  ",[ConsumoElectricoAdicional] = @ConsumoElectricoAdicional " & _
                                  ",[ReinicioLecturaElectrica] = @ReinicioLecturaElectrica " & _
                                  " WHERE idElementoMedida = '" & lblidElemento.Text & "' and CodigoPVYCR = '" & codigoPVYCR & "' " & _
                                  "and Fecha_Medida = '" & lblFechamedidaESel.Text & "' "


        comando.Parameters.Clear()
        'lectura I
        If utiles.nullABlanco(txtLecturaI.Text) = "" Then
            comando.Parameters.AddWithValue("LecturaI", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("LecturaI", Convert.ToDouble(Replace(txtLecturaI.Text, ".", "")))
        End If
        'lectura II
        If utiles.nullABlanco(txtLecturaII.Text) = "" Then
            comando.Parameters.AddWithValue("LecturaII", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("LecturaII", Convert.ToDouble(Replace(txtLecturaII.Text, ".", "")))
        End If
        'lectura III
        If utiles.nullABlanco(txtLecturaIII.Text) = "" Then
            comando.Parameters.AddWithValue("LecturaIII", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("LecturaIII", Convert.ToDouble(Replace(txtLecturaIII.Text, ".", "")))
        End If
        'tota kwh
        If utiles.nullABlanco(txtTotal_Kwh.Text) = "" Then
            comando.Parameters.AddWithValue("Total_KWH", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("Total_KWH", Convert.ToDouble(Replace(txtTotal_Kwh.Text, ".", "")))
        End If

        'total kvar
        If utiles.nullABlanco(txtTotal_Kvar.Text) = "" Then
            comando.Parameters.AddWithValue("Total_Kvar", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("Total_Kvar", Convert.ToDouble(Replace(txtTotal_Kvar.Text, ".", "")))
        End If

        comando.Parameters.AddWithValue("Funciona", utiles.BlancoANull(ddlfunciona.SelectedItem.ToString))
        comando.Parameters.AddWithValue("Observaciones", utiles.BlancoANull(txtObservaciones.Text))
        comando.Parameters.AddWithValue("Justificacion", utiles.BlancoANull(txtJustificacion.Text))
        comando.Parameters.AddWithValue("idIncidenciaElectrica", utiles.BlancoANull(ddlIncidenciasE.Text))
        'Consumo electrico adicional
        If utiles.nullABlanco(txtConsumoElectricoAdicional.Text) = "" Then
            comando.Parameters.AddWithValue("Consumoelectricoadicional", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("Consumoelectricoadicional", Convert.ToDouble(Replace(txtConsumoElectricoAdicional.Text, ".", "")))
        End If
        'reinicio lectura electrica
        If utiles.nullABlanco(txtReinicioLecturaElectrica.Text) = "" Then
            comando.Parameters.AddWithValue("ReinicioLecturaElectrica", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("ReinicioLecturaElectrica", Convert.ToDouble(Replace(txtReinicioLecturaElectrica.Text, ".", "")))
        End If

        comando.ExecuteNonQuery()

        lblFechamedidaESel.Text = ""
        pnlEnergia.Visible = True
        'ucPaginacionE.Visible = True
        pnlEDEnergia.Visible = False
        pnlEDVolumen.Visible = False
        pnlEDAcequias.Visible = False


        txtLecturaI.Text = ""
        txtLecturaII.Text = ""
        txtLecturaII.Text = ""
        txtTotal_Kwh.Text = ""
        txtTotal_Kvar.Text = ""
        'ddlfunciona.Text = ""
        txtJustificacion.Text = ""
        'ddlIncidenciasE.Text = ""
        txtConsumoElectricoAdicional.Text = ""
        txtReinicioLecturaElectrica.Text = ""
        txtObservaciones.Text = ""


        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacion")
        rptEnergia.DataBind()
        'ucPaginacionE.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnCancelarEDEnergia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarEDEnergia.Click

        lblFechamedidaESel.Text = ""
        pnlEnergia.Visible = True
        'ucPaginacionE.Visible = True
        pnlEDEnergia.Visible = False
        pnlEDVolumen.Visible = False
        pnlEDAcequias.Visible = False


        txtLecturaI.Text = ""
        txtLecturaII.Text = ""
        txtLecturaII.Text = ""
        txtTotal_Kwh.Text = ""
        txtTotal_Kvar.Text = ""
        'ddlfunciona.Text = ""
        txtJustificacion.Text = ""
        'ddlIncidenciasE.Text = ""
        txtConsumoElectricoAdicional.Text = ""
        txtReinicioLecturaElectrica.Text = ""
        txtObservaciones.Text = ""

    End Sub

    Protected Sub rellenarListas(ByVal idelemento As String, ByVal codigoPVYCR As String)
        Dim tipoelemento As String
        tipoelemento = idelemento.Substring(0, 1).ToString

        If tipoelemento = "E" Then


            'OBTENEMOS EL VALOR DE FUINCIONA
            'funciona
            Select Case utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("Funciona"))
                Case "", "NO"
                    ddlfunciona.SelectedIndex = 1
                Case "SI"
                    ddlfunciona.SelectedIndex = 0
            End Select

            'Si la tabla no tiene valores la cargamos, si no ponemos el IF, se carga tantas veces como la fila que
            'estamos evaluando.
            'No hacemos el databind porque ya hacemos un databind en el repeat que hace el databind de todo
            'lo que tiene denntro.
            If IsNothing(dstElementos.Tables("TablaIncidenciasE")) Then
                daElementos.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'E' order by descripcion"
                daElementos.Fill(dstElementos, "TablaIncidenciasE")
            End If

            ddlIncidenciasE.DataSource = dstElementos.Tables("TablaIncidenciasE")
            ddlIncidenciasE.DataValueField = "idIncidencia"
            ddlIncidenciasE.DataTextField = "Descripcion"
            ddlIncidenciasE.DataBind()
            ddlIncidenciasE.Items.Insert(0, New ListItem("[Seleccionar]", ""))

        ElseIf tipoelemento = "Q" Then

            If IsNothing(dstElementos.Tables("TablaCurvasAcequias")) Then
                daElementos.SelectCommand.CommandText = "select Cod_curva, Regimen from PVYCR_CurvasAcequias where codigoPVYCR = '" & _
                codigoPVYCR & "' and idElementomedida = '" & idelemento & "' "

                daElementos.Fill(dstElementos, "TablaCurvasacequias")
            End If
            'ddlRegimenCurvaQ.Items.Clear()
            ddlRegimenCurvaQ.DataSource = dstElementos.Tables("TablaCurvasAcequias")
            ddlRegimenCurvaQ.DataValueField = "Cod_curva"
            ddlRegimenCurvaQ.DataTextField = "Regimen"
            ddlRegimenCurvaQ.DataBind()
            ddlRegimenCurvaQ.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        ElseIf tipoelemento = "V" Then


            'OBTENEMOS EL VALOR DE FUINCIONA
            'funciona
            Select Case utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("Funciona"))
                Case "", "NO"
                    ddlFuncionaV.SelectedIndex = 1
                Case "SI"
                    ddlFuncionaV.SelectedIndex = 0
            End Select

            'Si la tabla no tiene valores la cargamos, si no ponemos el IF, se carga tantas veces como la fila que
            'estamos evaluando.
            'No hacemos el databind porque ya hacemos un databind en el repeat que hace el databind de todo
            'lo que tiene denntro.
            If IsNothing(dstElementos.Tables("TablaIncidenciasV")) Then
                daElementos.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'V' order by descripcion"
                daElementos.Fill(dstElementos, "TablaIncidenciasV")
            End If

            ddlIncidenciaV.DataSource = dstElementos.Tables("TablaIncidenciasV")
            ddlIncidenciaV.DataValueField = "idIncidencia"
            ddlIncidenciaV.DataTextField = "Descripcion"
            ddlIncidenciaV.DataBind()
            ddlIncidenciaV.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        ElseIf tipoelemento = "H" Then


            'OBTENEMOS EL VALOR DE FUINCIONA
            'funciona
            Select Case utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("Funciona"))
                Case "", "NO"
                    ddlFuncionaH.SelectedIndex = 1
                Case "SI"
                    ddlFuncionaH.SelectedIndex = 0
            End Select

            'Si la tabla no tiene valores la cargamos, si no ponemos el IF, se carga tantas veces como la fila que
            'estamos evaluando.
            'No hacemos el databind porque ya hacemos un databind en el repeat que hace el databind de todo
            'lo que tiene denntro.
            If IsNothing(dstElementos.Tables("TablaIncidenciasH")) Then
                daElementos.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'H' order by descripcion"
                daElementos.Fill(dstElementos, "TablaIncidenciasH")
            End If

            ddlIncidenciaH.DataSource = dstElementos.Tables("TablaIncidenciasH")
            ddlIncidenciaH.DataValueField = "idIncidencia"
            ddlIncidenciaH.DataTextField = "Descripcion"
            ddlIncidenciaH.DataBind()
            ddlIncidenciaH.Items.Insert(0, New ListItem("[Seleccionar]", ""))

        End If
    End Sub

    Protected Sub rptAcequias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptAcequias.ItemCommand
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        Dim nombre As String
        Dim i As Integer
        Select Case e.CommandName
            Case "editar"
                'mostramos el panel de edición
                lblFechamedidaQSel.Text = e.CommandArgument
                pnlAcequias.Visible = False
                'ucPaginacionA.Visible = False
                pnlEDAcequias.Visible = True

                creardatasetEd(lblidElemento.Text, codigoPVYCR, lblFechamedidaQSel.Text)

                If dstElementos.Tables("TablaEDAcequias").Rows.Count > 0 Then
                    'txtCodigoPVYCR.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("CodigoPVYCR"))
                    lblFechamedidaQ.Text = utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Fecha_medida"))
                    lblCodFuenteDatoQ.Text = utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Cod_fuente_Dato")) & " - " & utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("DescFuenteDato"))
                    lblidElementoA.Text = utiles.nullACero(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("idElementoMedida"))
                    lblTipoObtencionCaudal.Text = utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("TipoObtencionCaudal"))
                    txtEscalaQ.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Escala_M")))
                    txtCaladoQ.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Calado_M")))
                    txtCaudalQ.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Caudal_M3S")))
                    txtDudaCalidad.Text = utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Duda_Calidad"))


                    rellenarListas(lblidElementoA.Text, codigoPVYCR)
                    If dstElementos.Tables("tablaEDAcequias").Rows(0).Item("regimenCurva").ToString <> "" Then
                        ddlRegimenCurvaQ.Items.FindByText(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("RegimenCurva").ToString).Selected = True
                    End If

                    txtobservacionesQ.Text = utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Observaciones"))
                    nombre = checkNombreAcequias()
                End If

            Case "borrar"

                If conexion.State = ConnectionState.Closed Then conexion.Open()

                'comando.CommandText = "delete from pvycr_contadores where idContador=0" & e.CommandArgument

                comando.ExecuteNonQuery()

                'crearDataSets()
                'ucPaginacion.lblNumpaginasDatabind()
        End Select

    End Sub
    Protected Function checkNombreAcequias() As String
        If utiles.nullABlanco(dstElementos.Tables("TablaEdAcequias").Rows(0).Item("CodigoPVYCR")) <> "" Then
            lblTituloQ.Text = "EDICIÓN DATOS CAUDAL: " & utiles.nullABlanco(dstElementos.Tables("TablaEdAcequias").Rows(0).Item("CodigoPVYCR")) & "  " & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdAcequias").Rows(0).Item("DenominacionPunto")) & " /" & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdAcequias").Rows(0).Item("idElementoMedida"))
            Return utiles.nullABlanco(dstElementos.Tables("TablaEdAcequias").Rows(0).Item("CodigoPVYCR")) & "  " & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdAcequias").Rows(0).Item("DenominacionPunto")) & " /" & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdAcequias").Rows(0).Item("idElementoMedida"))
        Else
            Return "EDICIÓN DATOS CAUDAL"
        End If

    End Function
    Protected Function checkNombreMotores() As String
        If utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("CodigoPVYCR")) <> "" Then
            lblTituloV.Text = "EDICIÓN DATOS VOLUMEN: " & utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("CodigoPVYCR")) & "  " & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("DenominacionPunto")) & " /" & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("idElementoMedida"))
            Return utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("CodigoPVYCR")) & "  " & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("DenominacionPunto")) & " /" & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("idElementoMedida"))
        Else
            Return "EDICIÓN DATOS VOLUMEN"
        End If

    End Function

    Protected Sub btnAceptarEDAcequias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarEDAcequias.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)

        If conexion.State = ConnectionState.Closed Then conexion.Open()

        comando.CommandText = "UPDATE PVYCR_DatosAcequias " & _
                              "SET [Escala_M] = @Escala_M " & _
                              ",[Calado_M] = @Calado_M " & _
                              ",[RegimenCurva] = @RegimenCurva " & _
                              ",[NumeroParada] = @NumeroParada " & _
                              ",[Caudal_M3S] = @Caudal_M3S " & _
                              ",[Duda_Calidad] = @Duda_Calidad " & _
                              ",[Observaciones] = @Observaciones  " & _
                                  " WHERE idElementoMedida = '" & lblidElementoA.Text & "' and CodigoPVYCR = '" & codigoPVYCR & "' " & _
                                  "and Fecha_Medida = '" & lblFechamedidaQSel.Text & "' "


        comando.Parameters.Clear()
        'Escala
        If utiles.nullABlanco(txtEscalaQ.Text) = "" Then
            comando.Parameters.AddWithValue("Escala_M", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("Escala_M", Replace(Replace(txtEscalaQ.Text, ".", ""), ",", "."))
        End If
        'Calado
        If utiles.nullABlanco(txtCaladoQ.Text) = "" Then
            comando.Parameters.AddWithValue("Calado_M", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("Calado_M", Replace(Replace(txtCaladoQ.Text, ".", ""), ",", "."))
        End If
        'Número parada
        If utiles.nullABlanco(txtNumParadaQ.Text) = "" Then
            comando.Parameters.AddWithValue("NumeroParada", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("NumeroParada", Replace(Replace(txtNumParadaQ.Text, ".", ""), ",", "."))
        End If
        'Caudal
        If utiles.nullABlanco(txtCaudalQ.Text) = "" Then
            comando.Parameters.AddWithValue("Caudal_M3S", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("Caudal_M3S", Replace(Replace(txtCaudalQ.Text, ".", ""), ",", "."))
        End If

        'duda calidad
        If utiles.nullABlanco(txtDudaCalidad.Text) = "" Then
            comando.Parameters.AddWithValue("Duda_Calidad", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("Duda_Calidad", Replace(Replace(txtDudaCalidad.Text, ".", ""), ",", "."))
        End If

        comando.Parameters.AddWithValue("Observaciones", utiles.BlancoANull(txtobservacionesQ.Text))

        If ddlRegimenCurvaQ.SelectedItem.Text = "[Seleccionar]" Then
            comando.Parameters.AddWithValue("RegimenCurva", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("RegimenCurva", utiles.BlancoANull(ddlRegimenCurvaQ.SelectedItem.Text))
        End If



        comando.ExecuteNonQuery()

        lblFechamedidaQSel.Text = ""
        pnlAcequias.Visible = True
        'ucPaginacionA.Visible = True
        pnlEDAcequias.Visible = False

        txtEscalaQ.Text = ""
        txtCaladoQ.Text = ""
        txtNumParadaQ.Text = ""
        txtCaudalQ.Text = ""
        txtDudaCalidad.Text = ""
        txtobservacionesQ.Text = ""


        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElementoA.Text)
        rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
        rptAcequias.DataBind()
        'ucPaginacionA.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnCancelarEDAcequias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarEDAcequias.Click
        lblFechamedidaQSel.Text = ""
        pnlAcequias.Visible = True
        'ucPaginacionA.Visible = True
        pnlEDAcequias.Visible = False

        txtLecturaI.Text = ""
        txtEscalaQ.Text = ""
        txtCaladoQ.Text = ""
        txtNumParadaQ.Text = ""
        txtCaudalQ.Text = ""
        txtDudaCalidad.Text = ""
        txtObservaciones.Text = ""
    End Sub

    Protected Sub imgBuscarCauce_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscarCauce.Click
        Dim rutanodobuscadodescripcion As String

        'Búsqueda del nodo con el texto coincidente con la clave
        rutanodobuscadodescripcion = ""
        Dim rutanodobuscado = BuscarNodoTVW(treeView1, txtCauce.Text, rutanodobuscadodescripcion)

        If rutanodobuscado = "" Then
            Alert(Page, "No Existen nodos con los criterios seleccionados")
            SetFocusToControl(Me, Me.txtCauce)
            txtCauce.Text = ""

        Else
            'Representación del Nodo si la busqueda es positiva
            Dim nodobuscado As TreeNode = treeView1.FindNode(rutanodobuscado)

            treeView1.CollapseAll()
            ExpandirNodoRecursivo(nodobuscado)
            nodobuscado.Selected = True
            nodobuscado.Select()
            ActivarPanelDetalles()
        End If
    End Sub

    Protected Sub rptVolumen_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptVolumen.ItemCommand
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        Dim nombre As String
        Dim i As Integer
        Select Case e.CommandName
            Case "editar"
                'mostramos el panel de edición
                lblFechamedidaVSel.Text = e.CommandArgument
                pnlVolumen.Visible = False
                'ucPaginacionE.Visible = False
                pnlEDVolumen.Visible = True

                creardatasetEd(lblidElemento.Text, codigoPVYCR, lblFechamedidaVSel.Text)

                If dstElementos.Tables("TablaEdMotores").Rows.Count > 0 Then
                    lblFechaMedidaV.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("Fecha_medida"))
                    lblCodfuentedatoV.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("Cod_fuente_Dato")) & " - " & utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("DescFuenteDato"))
                    txtlecturacontador.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdMotores").Rows(0).Item("LecturaContador_M3")))

                    rellenarListas(lblidElemento.Text, codigoPVYCR)



                    ddlIncidenciaV.SelectedValue = dstElementos.Tables("TablaEdMotores").Rows(0).Item("idIncidenciaVolumetrica").ToString


                    txtJustificacionV.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("Justificacion"))
                    '                    ddlIncidenciasE.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("descIncElec"))
                    txtConsumoV.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdMotores").Rows(0).Item("ConsumoVolumetricoadicional")))
                    txtReinicioV.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdMotores").Rows(0).Item("ReinicioLecturaVolumetrica")))
                    txtObservacionesV.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("Observaciones"))
                    nombre = checkNombreMotores()
                End If

            Case "borrar"

                If conexion.State = ConnectionState.Closed Then conexion.Open()

                'comando.CommandText = "delete from pvycr_contadores where idContador=0" & e.CommandArgument

                comando.ExecuteNonQuery()

                'crearDataSets()
                'ucPaginacion.lblNumpaginasDatabind()
        End Select

    End Sub

    Protected Sub btnCancelarEDVolumen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarEDVolumen.Click
        lblFechamedidaVSel.Text = ""
        pnlVolumen.Visible = True
        'ucPaginacionE.Visible = True
        pnlEDVolumen.Visible = False

        txtlecturacontador.Text = ""
        txtJustificacionV.Text = ""
        txtConsumoV.Text = ""
        txtReinicioV.Text = ""
        txtObservacionesV.Text = ""
    End Sub

    Protected Sub btnAceptarEDVolumen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarEDVolumen.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)

        If conexion.State = ConnectionState.Closed Then conexion.Open()

        comando.CommandText = "UPDATE PVYCR_DatosMotores " & _
                               "SET [LecturaContador_M3] = @LecturaContador " & _
                                  ",[Funciona] = @FuncionaV " & _
                                  ",[Observaciones] = @ObservacionesV " & _
                                  ",[Justificacion] = @JustificacionV " & _
                                  ",[idIncidenciaVolumetrica] = @idIncidenciaV " & _
                                  ",[ConsumoVolumetricoAdicional] = @ConsumoV " & _
                                  ",[ReinicioLecturaVolumetrica] = @ReinicioV " & _
                                  " WHERE idElementoMedida = '" & lblidElemento.Text & "' and CodigoPVYCR = '" & codigoPVYCR & "' " & _
                                  "and Fecha_Medida = '" & lblFechamedidaVSel.Text & "' "


        comando.Parameters.Clear()
        'lectura Conatdor
        If utiles.nullABlanco(txtlecturacontador.Text) = "" Then
            comando.Parameters.AddWithValue("LecturaContador", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("LecturaContador", Convert.ToDouble(Replace(txtlecturacontador.Text, ".", "")))
        End If
        comando.Parameters.AddWithValue("FuncionaV", utiles.BlancoANull(ddlFuncionaV.SelectedItem.ToString))
        comando.Parameters.AddWithValue("ObservacionesV", utiles.BlancoANull(txtObservacionesV.Text))
        comando.Parameters.AddWithValue("JustificacionV", utiles.BlancoANull(txtJustificacionV.Text))
        comando.Parameters.AddWithValue("idIncidenciaV", utiles.BlancoANull(ddlIncidenciaV.Text))
        'Consumo electrico adicional
        If utiles.nullABlanco(txtConsumoV.Text) = "" Then
            comando.Parameters.AddWithValue("ConsumoV", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("ConsumoV", Convert.ToDouble(Replace(txtConsumoV.Text, ".", "")))
        End If
        'reinicio lectura electrica
        If utiles.nullABlanco(txtReinicioV.Text) = "" Then
            comando.Parameters.AddWithValue("ReinicioV", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("ReinicioV", Convert.ToDouble(Replace(txtReinicioV.Text, ".", "")))
        End If

        comando.ExecuteNonQuery()

        lblFechamedidaVSel.Text = ""
        pnlVolumen.Visible = True
        'ucPaginacionE.Visible = True
        pnlEDVolumen.Visible = False

        txtlecturacontador.Text = ""
        txtJustificacionV.Text = ""
        txtConsumoV.Text = ""
        txtReinicioV.Text = ""
        txtObservacionesV.Text = ""


        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        rptVolumen.DataSource = dstElementos.Tables("TablaMotores")
        rptVolumen.DataBind()
        'ucPaginacionE.lblNumpaginasDatabind()
    End Sub

    Protected Sub rptHorometros_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptHorometros.ItemCommand
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        Dim nombre As String
        Dim i As Integer
        Select Case e.CommandName
            Case "editar"
                'mostramos el panel de edición
                lblFechamedidaHSel.Text = e.CommandArgument
                pnlHorometros.Visible = False
                'ucPaginacionE.Visible = False
                pnlEDHorometros.Visible = True
                pnlEDVolumen.Visible = False
                pnlEDAcequias.Visible = False
                pnlEDEnergia.Visible = False

                creardatasetEd(lblidElemento.Text, codigoPVYCR, lblFechamedidaHSel.Text)

                If dstElementos.Tables("TablaEdHorometros").Rows.Count > 0 Then
                    lblFechaMedidaH.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("Fecha_medida"))
                    lblCodfuenteDatoH.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("Cod_fuente_Dato")) & " - " & utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("DescFuenteDato"))
                    txtHorasIntervalo.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("HorasIntervalo"))

                    rellenarListas(lblidElemento.Text, codigoPVYCR)


                    ddlIncidenciaH.SelectedValue = dstElementos.Tables("TablaEdHorometros").Rows(0).Item("idIncidenciaVolumetrica").ToString



                    txtConsumoH.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdHorometros").Rows(0).Item("ConsumoVolumetricoadicional")))
                    txtreinicioH.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEdHorometros").Rows(0).Item("ReinicioLecturaVolumetrica")))
                    txtobservacionesH.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("Observaciones"))
                    nombre = checkNombreHorometros()
                End If

            Case "borrar"

                If conexion.State = ConnectionState.Closed Then conexion.Open()

                'comando.CommandText = "delete from pvycr_contadores where idContador=0" & e.CommandArgument

                comando.ExecuteNonQuery()

                'crearDataSets()
                'ucPaginacion.lblNumpaginasDatabind()
        End Select
    End Sub
    Protected Function checkNombreHorometros() As String
        If utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("CodigoPVYCR")) <> "" Then
            lbltituloH.Text = "EDICIÓN DATOS HORAS: " & utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("CodigoPVYCR")) & "  " & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("DenominacionPunto")) & " /" & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("idElementoMedida"))
            Return utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("CodigoPVYCR")) & "  " & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("DenominacionPunto")) & " /" & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdHorometros").Rows(0).Item("idElementoMedida"))
        Else
            Return "EDICIÓN DATOS HORAS"
        End If

    End Function

    Protected Sub btnAceptarEDHorometro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarEDHorometro.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)

        If conexion.State = ConnectionState.Closed Then conexion.Open()

        comando.CommandText = "UPDATE PVYCR_DatosHorometros " & _
                               "SET [HorasIntervalo] = @Horas " & _
                                  ",[Funciona] = @Funciona " & _
                                  ",[Observaciones] = @Observaciones " & _
                                  ",[idIncidenciaVolumetrica] = @idIncidenciaVolumetrica " & _
                                  ",[ConsumoVolumetricoAdicional] = @ConsumoH " & _
                                  ",[ReinicioLecturaVolumetrica] = @ReinicioH " & _
                                  " WHERE idElementoMedida = '" & lblidElemento.Text & "' and CodigoPVYCR = '" & codigoPVYCR & "' " & _
                                  "and Fecha_Medida = '" & lblFechamedidaHSel.Text & "' "


        comando.Parameters.Clear()
        'Horas Intervalo
        If utiles.nullABlanco(txtHorasIntervalo.Text) = "" Then
            comando.Parameters.AddWithValue("Horas", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("Horas", Replace(Replace(txtHorasIntervalo.Text, ".", ""), ",", "."))
        End If

        comando.Parameters.AddWithValue("Funciona", utiles.BlancoANull(ddlfunciona.SelectedItem.ToString))
        comando.Parameters.AddWithValue("Observaciones", utiles.BlancoANull(txtobservacionesH.Text))
        comando.Parameters.AddWithValue("idIncidenciaVolumetrica", utiles.BlancoANull(ddlIncidenciaH.Text))
        'Consumo electrico adicional
        If utiles.nullABlanco(txtConsumoH.Text) = "" Then
            comando.Parameters.AddWithValue("ConsumoH", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("ConsumoH", Convert.ToDouble(Replace(txtConsumoH.Text, ".", "")))
        End If
        'reinicio lectura electrica
        If utiles.nullABlanco(txtreinicioH.Text) = "" Then
            comando.Parameters.AddWithValue("ReinicioH", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("ReinicioH", Convert.ToDouble(Replace(txtreinicioH.Text, ".", "")))
        End If

        comando.ExecuteNonQuery()

        lblFechamedidaHSel.Text = ""
        pnlHorometros.Visible = True
        'ucPaginacionE.Visible = True
        pnlEDEnergia.Visible = False
        pnlEDVolumen.Visible = False
        pnlEDAcequias.Visible = False
        pnlEDHorometros.Visible = False


        txtHorasIntervalo.Text = ""
        txtConsumoH.Text = ""
        txtreinicioH.Text = ""
        txtobservacionesH.Text = ""


        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        rptHorometros.DataSource = dstElementos.Tables("TablaHorometros")
        rptHorometros.DataBind()
        'ucPaginacionE.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnCancelarEDHorometro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarEDHorometro.Click
        lblFechamedidaHSel.Text = ""
        lblCodfuenteDatoH.Text = ""
        pnlHorometros.Visible = True
        pnlEDEnergia.Visible = False
        pnlEDVolumen.Visible = False
        pnlEDAcequias.Visible = False
        pnlEDHorometros.Visible = False


        txtHorasIntervalo.Text = ""
        txtConsumoH.Text = ""
        txtreinicioH.Text = ""
        txtobservacionesH.Text = ""
    End Sub
    Protected Sub formateo_controles_cliente()
        'DATOS MOTOTRES
        JavaScript.controlFormatea(Page, txtlecturacontador)
        JavaScript.controlDesFormatea(Page, txtlecturacontador)
        JavaScript.controlFormatea(Page, txtConsumoV)
        JavaScript.controlDesFormatea(Page, txtConsumoV)
        JavaScript.controlFormatea(Page, txtReinicioV)
        JavaScript.controlDesFormatea(Page, txtReinicioV)
        'DATOS ACEQUIAS
        JavaScript.controlFormatea(Page, txtEscalaQ)
        JavaScript.controlDesFormatea(Page, txtEscalaQ)
        JavaScript.controlFormatea(Page, txtCaladoQ)
        JavaScript.controlDesFormatea(Page, txtCaladoQ)
        JavaScript.controlFormatea(Page, txtCaudalQ)
        JavaScript.controlDesFormatea(Page, txtCaudalQ)
        'DATOS ALLIMENTACION
        JavaScript.controlFormatea(Page, txtLecturaI)
        JavaScript.controlDesFormatea(Page, txtLecturaI)
        JavaScript.controlFormatea(Page, txtLecturaII)
        JavaScript.controlDesFormatea(Page, txtLecturaII)
        JavaScript.controlFormatea(Page, txtLecturaIII)
        JavaScript.controlDesFormatea(Page, txtLecturaIII)
        JavaScript.controlFormatea(Page, txtTotal_Kwh)
        JavaScript.controlDesFormatea(Page, txtTotal_Kwh)
        JavaScript.controlFormatea(Page, txtTotal_Kvar)
        JavaScript.controlDesFormatea(Page, txtTotal_Kvar)
        JavaScript.controlFormatea(Page, txtConsumoElectricoAdicional)
        JavaScript.controlDesFormatea(Page, txtConsumoElectricoAdicional)
        JavaScript.controlFormatea(Page, txtReinicioLecturaElectrica)
        JavaScript.controlDesFormatea(Page, txtReinicioLecturaElectrica)
        'DATOS HOROMETROS
        JavaScript.controlFormatea(Page, txtConsumoH)
        JavaScript.controlDesFormatea(Page, txtConsumoH)
        JavaScript.controlFormatea(Page, txtreinicioH)
        JavaScript.controlDesFormatea(Page, txtreinicioH)
    End Sub
    Protected Sub obtenerVolumenDiferencial(ByVal tipo As String)
        'vamos a calcular la diferencia de volúmenes según registros
        'el caso de las acequias es un caso especial y se calcula obteniendo la media del caudal por el tiempo trancurrido entre dos lecturas.
        Dim i As Integer
        Dim v_vol, v_vol_ant, v_diferencial, v_caudal, v_caudal_ant, v_caudal_medio, v_diferencial_acum As Decimal
        Dim v_tiempo, v_tiempo_ant As Date
        Dim v_tiempo_medio As Integer

        If tipo = "V" Then
            If dstElementos.Tables("TablaMotores").Rows.Count > 0 Then
                If Not dstElementos.Tables("TablaMotores").Columns.Contains("Diferencial") Then
                    'añadimos la columna diferencial al dataset
                    dstElementos.Tables("TablaMotores").Columns.Add("Diferencial")
                    dstElementos.Tables("TablaMotores").Columns.Add("Diferencial_Acum")
                End If
                'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
                For i = 0 To dstElementos.Tables("TablaMotores").Rows.Count - 1

                    If i = 0 Then
                        'rellenamos la columna diferencial del dataset con un 0
                        dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial") = "0"
                        dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial_Acum") = "0"
                    Else
                        'comprobamos si existen incidencias
                        'si la incidencia es reseteo o cambio de contador se deberá tomar el valor del campo Reinicio Lectura Volumetrica
                        'si la incidencia es contador averiado o consumo negativo, el valor a tomar será el consumo volumétrico adicional
                        If (dstElementos.Tables("TablaMotores").Rows(i).Item("idincidenciaVolumetrica").ToString = "6") Or _
                            (dstElementos.Tables("TablaMotores").Rows(i).Item("idincidenciaVolumetrica").ToString = "7") Then
                            v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("ReiniciolecturaVolumetrica").ToString)
                            dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3") = dstElementos.Tables("TablaMotores").Rows(i).Item("ReiniciolecturaVolumetrica")
                        ElseIf (dstElementos.Tables("TablaMotores").Rows(i).Item("idincidenciaVolumetrica").ToString = "5") Or _
                            (dstElementos.Tables("TablaMotores").Rows(i).Item("idincidenciaVolumetrica").ToString = "8") Then
                            v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("ConsumoVolumetricoAdicional").ToString)
                            dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3") = dstElementos.Tables("TablaMotores").Rows(i).Item("ConsumoVolumetricoAdicional")
                        Else
                            v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3").ToString)
                        End If

                        v_vol_ant = Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i - 1).Item("LecturaContador_M3").ToString)
                        v_diferencial = v_vol - v_vol_ant
                        'calculamos el diferencial acumulado
                        v_diferencial_acum = v_diferencial_acum + v_diferencial
                        'añadimos los valores a la tabla
                        dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                        dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                    End If
                Next
            End If
        ElseIf tipo = "E" Then
            If dstElementos.Tables("TablaAlimentacion").Rows.Count > 0 Then
                If Not dstElementos.Tables("TablaAlimentacion").Columns.Contains("Diferencial") Then
                    'añadimos la columna diferencial al dataset
                    dstElementos.Tables("TablaAlimentacion").Columns.Add("Diferencial")
                    dstElementos.Tables("TablaAlimentacion").Columns.Add("Diferencial_acum")
                End If
                'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
                For i = 0 To dstElementos.Tables("TablaAlimentacion").Rows.Count - 1

                    If i = 0 Then
                        'rellenamos la columna diferencial del dataset con un 0
                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial") = "0"
                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_acum") = "0"
                    Else
                        'comprobamos si existen incidencias
                        'si la incidencia es reseteo o cambio de contador se deberá tomar el valor del campo Reinicio Lectura Volumetrica
                        'si la incidencia es contador averiado o consumo negativo, el valor a tomar será el consumo volumétrico adicional
                        If (dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaElectrica").ToString = "2") Or _
                            (dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaElectrica").ToString = "3") Then
                            v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("ReinicioLecturaElectrica").ToString)
                            dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh") = dstElementos.Tables("TablaAlimentacion").Rows(i).Item("ReinicioLecturaElectrica")
                        ElseIf (dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaElectrica").ToString = "1") Or _
                            (dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaElectrica").ToString = "3") Then
                            v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("ConsumoElectricoAdicional").ToString)
                            dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh") = dstElementos.Tables("TablaAlimentacion").Rows(i).Item("ConsumoElectricoAdicional")
                        Else
                            v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString)
                        End If

                        v_vol_ant = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i - 1).Item("Total_Kwh").ToString)
                        v_diferencial = v_vol - v_vol_ant
                        'calculamos el diferencial acumulado
                        v_diferencial_acum = v_diferencial_acum + v_diferencial
                        'cargamos datos en la tabla
                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                    End If
                Next
            End If
        ElseIf tipo = "Q" Then
            'deberemos calcular el volumen, siendo éste el caudal por el tiempo
            If dstElementos.Tables("TablaAcequias").Rows.Count > 0 Then
                If Not dstElementos.Tables("TablaAcequias").Columns.Contains("Diferencial") Then
                    'añadimos la columna diferencial al dataset
                    dstElementos.Tables("TablaAcequias").Columns.Add("Diferencial")
                    dstElementos.Tables("TablaAcequias").Columns.Add("Diferencial_acum")
                End If
                'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
                For i = 0 To dstElementos.Tables("TablaAcequias").Rows.Count - 1

                    If i = 0 Then
                        'rellenamos la columna diferencial del dataset con un 0
                        dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial") = "0"
                        dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial_acum") = "0"
                    Else
                        v_caudal = Convert.ToDecimal(0 & dstElementos.Tables("TablaAcequias").Rows(i).Item("Caudal_M3S").ToString)
                        v_caudal_ant = Convert.ToDecimal(0 & dstElementos.Tables("TablaAcequias").Rows(i - 1).Item("Caudal_M3S").ToString)
                        v_caudal_medio = (v_caudal + v_caudal_ant) / 2
                        'el tiempo deberá estar en segundos
                        v_tiempo = dstElementos.Tables("TablaAcequias").Rows(i).Item("Fecha_medida").ToString
                        v_tiempo_ant = dstElementos.Tables("TablaAcequias").Rows(i - 1).Item("Fecha_medida").ToString
                        v_tiempo_medio = Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60)
                        'lompartimos entre 1000 para pasarlo de a (m3)
                        v_diferencial = (v_caudal_medio * v_tiempo_medio)
                        'calculamos el diferencial acumulado
                        v_diferencial_acum = v_diferencial_acum + v_diferencial
                        'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
                        If utiles.nullABlanco(dstElementos.Tables("TablaAcequias").Rows(i).Item("Caudal_M3S").ToString) = "" Then
                            dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.###}", DBNull.Value)
                            dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", DBNull.Value)
                        Else
                            dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.###}", v_diferencial)
                            dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                        End If
                    End If
                Next
            ElseIf tipo = "H" Then
                'If dstElementos.Tables("TablaHorometros").Rows.Count > 0 Then
                'if not dstElementos.Tables("TablaHorometros").Columns.Contains("Diferencial") then
                '    'añadimos la columna diferencial al dataset
                '    dstElementos.Tables("TablaHorometros").Columns.Add("Diferencial")
                'end if
                '    'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
                '    For i = 0 To dstElementos.Tables("TablaHorometros").Rows.Count - 1

                '        If i = 0 Then
                '            'rellenamos la columna diferencial del dataset con un 0
                '            dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial") = "0"
                '        Else
                '            'comprobamos si existen incidencias
                '            'si la incidencia es reseteo o cambio de contador se deberá tomar el valor del campo Reinicio Lectura Volumetrica
                '            'si la incidencia es contador averiado o consumo negativo, el valor a tomar será el consumo volumétrico adicional
                '            If (dstElementos.Tables("TablaHorometros").Rows(i).Item("idincidenciaVolumetrica").ToString = "10") Or _
                '                (dstElementos.Tables("TablaHorometros").Rows(i).Item("idincidenciaVolumetrica").ToString = "11") Then
                '                v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("ReiniciolecturaVolumetrica").ToString)
                '                dstElementos.Tables("TablaHorometros").Rows(i).Item("LecturaContador_M3") = dstElementos.Tables("TablaHorometros").Rows(i).Item("ReiniciolecturaVolumetrica")
                '            ElseIf (dstElementos.Tables("TablaHorometros").Rows(i).Item("idincidenciaVolumetrica").ToString = "9") Or _
                '                (dstElementos.Tables("TablaHorometros").Rows(i).Item("idincidenciaVolumetrica").ToString = "12") Then
                '                v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("ConsumoVolumetricoAdicional").ToString)
                '                dstElementos.Tables("TablaHorometros").Rows(i).Item("LecturaContador_M3") = dstElementos.Tables("TablaHorometros").Rows(i).Item("ConsumoVolumetricoAdicional")
                '            Else
                '                v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("LecturaContador_M3").ToString)
                '            End If

                '            v_vol_ant = Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i - 1).Item("LecturaContador_M3").ToString)
                '            v_diferencial = v_vol - v_vol_ant
                '            dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                '        End If
                '    Next
                'End If

            End If
        End If
    End Sub

    Protected Sub chkFiltroNulasE_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFiltroNulasE.CheckedChanged
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        'ucPaginacionA.lblPaginatext = "1"
        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacion")
        rptEnergia.DataBind()
    End Sub

    Protected Sub chkFiltroNulasQ_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFiltroNulasQ.CheckedChanged
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        'ucPaginacionA.lblPaginatext = "1"
        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
        rptAcequias.DataBind()
    End Sub

    Protected Sub chkFiltroNulasV_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFiltroNulasV.CheckedChanged
        Dim tipoelem As String = treeView1.SelectedValue.Substring(treeView1.SelectedValue.Length - 1, 1)
        Dim codigoPVYCR As String = treeView1.SelectedValue.Substring(0, treeView1.SelectedValue.Length - 4)
        'ucPaginacionA.lblPaginatext = "1"
        crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        rptVolumen.DataSource = dstElementos.Tables("TablaMotores")
        rptVolumen.DataBind()
    End Sub

End Class
