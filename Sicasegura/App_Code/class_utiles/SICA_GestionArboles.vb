Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles

Namespace GuarderiaFluvial

    Public Class SICA_GestionArboles

        Shared Function BuscarPathNodo(ByVal minodo As TreeNode, ByVal clave As String, ByRef PathNodoDescripcion As String) As String
            'EGB: Función que retorna el Path completo de claves de un nodo, 
            '     La variable ByRef PathNodoDescripción retorna las descripciones del path completo del nodo
            '     se utiliza posteriormente para activar el nodo buscado.
            Dim nodohijo As TreeNode
            Dim PathNodo As String
            BuscarPathNodo = ""
            If Microsoft.VisualBasic.InStr(Microsoft.VisualBasic.UCase(minodo.Text), Microsoft.VisualBasic.UCase(clave)) Then
                PathNodo = minodo.ValuePath
                BuscarPathNodo = PathNodo
                PathNodoDescripcion = PathNodoDescripcion & "/" & minodo.Text
                minodo.Expanded = True
                Exit Function
            Else
                For Each nodohijo In minodo.ChildNodes
                    PathNodo = BuscarPathNodo(nodohijo, clave, PathNodoDescripcion)
                    If PathNodo <> "" Then
                        BuscarPathNodo = PathNodo
                        PathNodoDescripcion = minodo.Text & "/" & PathNodoDescripcion
                        minodo.Expanded = True
                        Exit Function
                    End If

                Next
            End If
        End Function

        Shared Function BuscarNodoTVW(ByVal micontroltvw As TreeView, ByVal clave As String, ByRef PathNodoDescripcion As String) As String
            'EGB:Función que devuelve referencia a un nodo en caso de existir -> según criterio de búsqueda establecido por la cadena 'clave'
            Dim nodo As TreeNode
            Dim rutanodo As String


            rutanodo = ""
            For Each nodo In micontroltvw.Nodes
                rutanodo = BuscarPathNodo(nodo, clave, PathNodoDescripcion)
                If rutanodo <> "" Then
                    Exit For
                End If
            Next
            BuscarNodoTVW = rutanodo
        End Function


        Shared Sub BuscarNodos(ByVal micontroltvw As TreeView, ByVal clave As String, ByRef lstBusquedasResultados As ListBox)
            'EGB:Función que devuelve referencia a un nodo en caso de existir -> según criterio de búsqueda establecido por la cadena 'clave'
            Dim nodo As TreeNode
            Dim rutanodo As String


            rutanodo = ""
            For Each nodo In micontroltvw.Nodes
                If Microsoft.VisualBasic.InStr(Microsoft.VisualBasic.UCase(nodo.Text), Microsoft.VisualBasic.UCase(clave)) Then
                    'Rellenar la lista de coincidencias
                    Dim elementoLista As New ListItem
                    elementoLista.Text = nodo.Text
                    elementoLista.Value = nodo.Value
                    lstBusquedasResultados.Items.Add(elementoLista)
                End If

            Next
            'BuscarNodoTVW = rutanodo
        End Sub

        Shared Sub crearDataSetsPuntosXML2(ByVal midstarbol As DataSet, ByVal mitreeview As TreeView, ByVal midaarbol As SqlDataAdapter)
            'EGB Sobrecargado para globalización con parámetros
            midstarbol.Clear()
            mitreeview.Nodes.Clear()

            'ARBOL NORMALIZADO
            midaarbol.SelectCommand.CommandText = "Select * from dbo.PVYCR_ARBOL"
            midaarbol.Fill(midstarbol, "tablaArbol")

            midstarbol.Relations.Add("SelfRefenceRelation", _
                            midstarbol.Tables("tablaArbol").Columns("IdArbol"), _
                            midstarbol.Tables("tablaArbol").Columns("IdArbolPadre"), True)
            midstarbol.Relations("SelfRefenceRelation").Nested = True

        End Sub

        Shared Sub ExpandirNodoRecursivo(ByVal minodo As TreeNode)
            'EGB: Expande los nodos padre de un nodo(minodo)del arbol micontroltvw
            Try
                If Not Microsoft.VisualBasic.IsNothing(minodo.Parent) Then
                    ExpandirNodoRecursivo(minodo.Parent)
                Else
                    'Alert(Page, "Nodo no encontrado")
                End If
                minodo.Expanded = True
                'minodo.SelectAction = TreeNodeSelectAction.Expand
            Catch ex As Exception
                'Alert(pagina, "Nodo no localizado")
            End Try
        End Sub

  
        Shared Sub insertarNodosCauces(ByVal tvwArbolCauces As TreeView, ByVal dstArbol As DataSet, ByVal cauce As String, ByVal denominacionCauce As String, ByVal elNodoPadre As TreeNode, ByVal nivel As Integer, ByVal bolSistemas As Boolean)
            Dim registros(), registros2(), registros3() As DataRow
            Dim i, j, k As Integer
            Dim tn, tp, te As TreeNode
            Dim fila As DataRow
            Dim v_ot, tiposensor, esTelemedida As String
            Dim visible As String
            Select Case nivel
                Case -1
                    If cauce = "OT000 hasta OT099" Or cauce = "OT100 hasta OT999" Then
                        v_ot = cauce
                        cauce = "OT"
                    End If
                    If cauce <> "OT" Then
                        tn = New TreeNode("<b>" & cauce & " " & verDescPrefijo(cauce) & "</b>", cauce & "#C")
                    Else
                        tn = New TreeNode("<b>" & v_ot & " " & verDescPrefijo(v_ot) & "</b>", cauce & "#C")
                    End If
                    tn.Expanded = False


                    tvwArbolCauces.Nodes.Add(tn)

                    registros = dstArbol.Tables("tablaCauces").Select("codigoCauce LIKE '" & cauce & "%'")
                    For i = 0 To registros.Length - 1
                        If cauce <> "OT" Then
                            If numeroPuntoYComa(registros(i)("codigoCauce").ToString()) <= 0 Then
                                insertarNodosCauces(tvwArbolCauces, dstArbol, registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1, bolSistemas)
                            End If
                        Else
                            If v_ot = "OT000 hasta OT099" Then
                                If numeroPuntoYComa(registros(i)("codigoCauce").ToString()) <= 0 And Integer.Parse(Mid(registros(i)("codigoCauce").ToString(), 3, 3)) < 100 Then
                                    insertarNodosCauces(tvwArbolCauces, dstArbol, registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1, bolSistemas)
                                End If

                            ElseIf v_ot = "OT100 hasta OT999" Then
                                If numeroPuntoYComa(registros(i)("codigoCauce").ToString()) <= 0 And Integer.Parse(Mid(registros(i)("codigoCauce").ToString(), 3, 3)) >= 100 Then
                                    insertarNodosCauces(tvwArbolCauces, dstArbol, registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1, bolSistemas)
                                End If

                            End If
                        End If
                    Next

                Case Else
                    'Formato de Cauces y Subcauces
                    If nivel < 1 Then
                        tn = New TreeNode("<font color=" & GuarderiaFluvial.utiles.GradienteNivelesAzules(nivel) & "><b>" & cauce & " " & denominacionCauce & "</b>", cauce & "#C")
                    Else
                        tn = New TreeNode("<font color=" & GuarderiaFluvial.utiles.GradienteNivelesAzules(nivel) & "><b>" & cauce & " </b>" & denominacionCauce, cauce & "#C")
                    End If

                    registros = dstArbol.Tables("tablaCauces").Select("codigoCauce LIKE '" & cauce & "%' AND codigoCauce <> '" & cauce & "'")
                    For i = 0 To registros.Length - 1
                        If numeroPuntoYComa(registros(i)("codigoCauce").ToString()) = nivel + 1 Then
                            insertarNodosCauces(tvwArbolCauces, dstArbol, registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1, bolSistemas)
                        End If
                    Next


                    elNodoPadre.ChildNodes.AddAt(elNodoPadre.ChildNodes.Count, tn)
                    elNodoPadre.Expanded = False
                    registros = dstArbol.Tables("tablaPuntos").Select("codigoCauce = '" & cauce & "'")

                    For i = 0 To registros.Length - 1
                        'obtenemos datos de elementos medida
                        registros2 = dstArbol.Tables("TablaElementosMedidad").Select("CodigoPVYCR = '" & registros(i).Item("CodigoPVYCR").ToString & "'")

                        If registros(i).Item("TipoSensor") Is System.DBNull.Value Then
                            tiposensor = ")"
                            'NCM: ---------------------------------------------------------------------------------------------'
                            'OJO!!!! Si el tiposensor es nulo, el punto NO es de tipo telemedida, ésta variable nos indica si el punto es de tipo telemedida, si es así nos han pedido resaltar el nodo!!!
                            esTelemedida = "N"
                            '---------------------------------------------------------------------------------------------'
                        Else
                            tiposensor = " - " & registros(i).Item("TipoSensor").ToString & ")"
                            'NCM: ---------------------------------------------------------------------------------------------'
                            'OJO!!!!Si el tiposensor es nulo, el punto NO es de tipo telemedida,  ésta variable nos indica si el punto es de tipo telemedida, si es así nos han pedido resaltar el nodo!!!
                            esTelemedida = "S"
                            '---------------------------------------------------------------------------------------------'
                        End If

                        If registros(i)("tipoPunto").ToString = "M" Then
                            'NCM: ----------------------------------------------------------------------------------------------'
                            '----------------------------------------------------------------------------------------------'
                            'OJO FORMATO PARA TELEMEDIDA!!!!
                            If esTelemedida = "S" Then
                                tp = New TreeNode("<img src='images/puntoMotor.ico' border=0>&nbsp;&nbsp;" & "<IMG SRC='/SicaSegura/SICAH/images/iconos/icoPestNibus.gif' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & "<b>" & registros(i).Item("codigoPVYCR").ToString() & " (M" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</b></font>", registros(i).Item("codigoPVYCR").ToString() & "#P")
                            Else
                                tp = New TreeNode("<img src='images/puntoMotor.ico' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & "<b>" & registros(i).Item("codigoPVYCR").ToString() & " (M" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</b></font>", registros(i).Item("codigoPVYCR").ToString() & "#P")
                            End If

                            tn.ChildNodes.AddAt(tn.ChildNodes.Count, tp)
                            'Insertamos elementos medida
                            For j = 0 To registros2.Length - 1
                                te = New TreeNode("<img src='images/distancias.gif' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & registros(i).Item("codigoPVYCR").ToString() & "-" & registros2(j).Item("idelementoMedida").ToString & " - " & registros2(j).Item("DESCTIPO").ToString, registros2(j).Item("codigoPVYCR").ToString & "#E#" & registros2(j).Item("tipo").ToString)
                                tp.ChildNodes.Add(te)

                                '----Sección de carga de Sistemas asociados a los elementos de medida - Solo se carga en el Mantenimiento de Sistemas
                                If bolSistemas Then
                                    registros3 = dstArbol.Tables("tablaSistemasPadreConCauce").Select("CodigoPVYCR = '" & registros(i).Item("CodigoPVYCR").ToString & "' and IdElementoMedida='" & registros2(j).Item("idelementoMedida").ToString & "'")
                                    For Each fila In registros3
                                        InsertarNodosCaucesSistemas(fila, te, 0, dstArbol)
                                    Next
                                End If
                                '---------------------------------------------------------------------------------------------------------------------

                            Next

                        Else
                            If esTelemedida = "S" Then
                                tp = New TreeNode("<img src='images/puntoAcequia2.gif' border=0>&nbsp;&nbsp;" & "<IMG SRC='/images/icoPestNibus.gif' border=0>&nbsp;&nbsp;" & "<font color=#008500><b>" & registros(i).Item("codigoPVYCR").ToString() & " (G" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "<b/></font>", registros(i).Item("codigoPVYCR").ToString() & "#P")
                            Else
                                tp = New TreeNode("<img src='images/puntoAcequia2.gif' border=0>&nbsp;&nbsp;" & "<font color=#008500><b>" & registros(i).Item("codigoPVYCR").ToString() & " (G" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "<b/></font>", registros(i).Item("codigoPVYCR").ToString() & "#P")
                            End If

                            tn.ChildNodes.AddAt(tn.ChildNodes.Count, tp)
                            'insertamos elementos medida
                            For j = 0 To registros2.Length - 1
                                te = New TreeNode("<img src='images/distancias.gif' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & registros(i).Item("codigoPVYCR").ToString() & "-" & registros2(j).Item("idelementoMedida").ToString & " - " & registros2(j).Item("DESCTIPO").ToString, registros2(j).Item("codigoPVYCR").ToString & "#E#" & registros2(j).Item("tipo").ToString)
                                tp.ChildNodes.Add(te)

                                '----Sección de carga de Sistemas asociados a los elementos de medida - Solo se carga en el Mantenimiento de Sistemas
                                If bolSistemas Then
                                    registros3 = dstArbol.Tables("tablaSistemasPadreConCauce").Select("CodigoPVYCR = '" & registros(i).Item("CodigoPVYCR").ToString & "' and IdElementoMedida='" & registros2(j).Item("idelementoMedida").ToString & "'")
                                    For Each fila In registros3
                                        InsertarNodosCaucesSistemas(fila, te, 0, dstArbol)
                                    Next
                                End If
                                '---------------------------------------------------------------------------------------------------------------------

                            Next
                        End If
                        tn.Expanded = False
                        tp.Expanded = False
                    Next



            End Select

        End Sub
        Shared Sub InsertarNodosCaucesSistemas(ByVal fila As DataRow, ByVal nodopadreSistema As TreeNode, ByVal nivel As Integer, ByVal dstArbol As DataSet)
            Dim f, registros() As DataRow
            Dim tn As New TreeNode
            tn.Text = "<font color=" & GuarderiaFluvial.utiles.GradienteNivelesGranates(nivel) & "><b>" & fila("Nombre") & "</b>"
            tn.Value = fila("IdSistema")
            tn.ImageUrl = "~/SICAH/IMAGES/SubSistemas.GIF"
            tn.Expand()
            nodopadreSistema.ChildNodes.Add(tn)
            registros = dstArbol.Tables("tablaSistemasPadre").Select("IdSistemaPadre= '" & fila.Item("IdSistema").ToString & "'")
            For Each f In registros
                InsertarNodosCaucesSistemas(f, tn, nivel + 1, dstArbol)
            Next
        End Sub

        Shared Sub crearArbolRecursivoSistemas(ByVal tvwArbolSistemas As TreeView, ByVal dstArbol As DataSet)
            Dim fila As DataRow
            For Each fila In dstArbol.Tables("tablaSistemasPadre").Rows
                If (fila.IsNull("IdSistemaPadre")) Then
                    Dim tn As New TreeNode
                    tn.Text = "<font color=" & GuarderiaFluvial.utiles.GradienteNivelesGranates(0) & "><b>" & fila("Nombre") & "</b>"
                    tn.Value = fila("IdSistema")
                    tn.ImageUrl = "~/SICAH/IMAGES/SubSistemas.GIF"
                    tn.Expanded = True
                    InsertarNodosSistemas(fila, tn, 1)
                    tvwArbolSistemas.Nodes.Add(tn)
                End If
            Next
        End Sub
        Shared Sub InsertarNodosSistemas(ByVal fila As DataRow, ByRef nodo As TreeNode, ByVal nivel As Integer)

            Dim f As DataRow

            For Each f In fila.GetChildRows("SelfRefenceRelation")
                Dim tn As New TreeNode
                tn.Text = "<font color=" & GuarderiaFluvial.utiles.GradienteNivelesGranates(nivel) & "><b>" & f("Nombre") & "</b>"
                tn.Value = f("IdSistema")
                tn.ImageUrl = "~/SICAH/IMAGES/SubSistemas2.GIF"
                tn.Expand()
                nodo.ChildNodes.Add(tn)
                InsertarNodosSistemas(f, tn, nivel + 1)
            Next

        End Sub


        ''----------------------
        ''ELIMINAR SI OK
        ''----------------------
        ''Private Sub crearArbolRecursivo()

        ''    Dim fila As DataRow
        ''    For Each fila In dstarbol.Tables(0).Rows

        ''        If (fila.IsNull("IdSistemaPadre")) Then

        ''            Dim tn As New TreeNode
        ''            tn.Text = fila("Nombre")
        ''            tn.Value = fila("IdSistema")

        ''            tn.Expanded = False
        ''            CargarArbolRecursivo(fila, tn)
        ''            Me.tvwSistemas.Nodes.Add(tn)

        ''        End If
        ''    Next

        ''End Sub

        'Private Sub CargarArbolRecursivo(ByVal fila As DataRow, ByRef nodo As TreeNode)

        '    Dim f As DataRow

        '    For Each f In fila.GetChildRows("SelfRefenceRelation")
        '        Dim tn As New TreeNode
        '        tn.Text = f("Nombre")
        '        tn.Value = f("IdSistema")
        '        If InStr(tn.Text, "Sistema ") Then tn.ImageUrl = "~/SICAH/IMAGES/SubSistemas.GIF"
        '        tn.Expand()
        '        nodo.ChildNodes.Add(tn)
        '        'Agregar nodo de los Calculo asociados al Sistema
        '        'AgregarNodosCalculos(tn)
        '        CargarArbolRecursivo(f, tn)
        '    Next

        'End Sub

        Shared Function numeroGuiones(ByVal cadena As String) As Integer
            Return Math.Max(Split(cadena, "-").Length - 1, 0)
        End Function
        Shared Function numeroSeparadores(ByVal cadena As String) As Integer
            'EGB 21042008
            'Return Math.Max(Split(cadena, "#").Length - 1, 0)
            Return Math.Max(Split(cadena, ";").Length - 1, 0)
        End Function
        Shared Function numeroPuntoYComa(ByVal cadena As String) As Integer
            Return Math.Max(Split(cadena, ";").Length - 1, 0)
        End Function
        Shared Sub crearArbolRecursivoBISBIS(ByVal tvwArbol As TreeView, ByVal dstArbol As DataSet, ByVal dstCaucesVisibles As DataSet)
            Dim fila As DataRow
            Dim visible As String
            For Each fila In dstArbol.Tables("TablaArbol").Rows
                If (fila.IsNull("IdArbolPadre")) Then
                    If fila("codigoCauce").ToString = "OT" Then
                        If fila("descripcion").ToString = "OT Pozos de CHS" Then
                            visible = utiles.nullABlanco(dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item(fila("CodigoCauce") & "1").ToString)
                        Else
                            visible = utiles.nullABlanco(dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item(fila("CodigoCauce") & "0").ToString)
                        End If
                    Else
                        visible = utiles.nullABlanco(dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item(fila("CodigoCauce")).ToString)
                    End If

                    If visible = "S" Then
                        Dim tn As New TreeNode
                        tn.Text = fila("StrNodo")

                        tn.Value = fila("ClaveNodo")
                        tn.ToolTip = ""
                        'EGB 14/07/2008 Corrección para permitir la activación de nodos de primer nivel
                        'tn.SelectAction = TreeNodeSelectAction.Select
                        'EGB 06/08/2008 Corrección para permitir la activacion de nodos de primer nivel
                        'según hizo NAY en InsertarNodosBISBIS(fila,tn, tipoNodo)
                        'tn.NavigateUrl = "../SICAH/CaucePuntDETALLE.aspx?nodotexto=" & fila("Descripcion") & "&nodoclave=" & Replace(fila("ClaveNodo"), "#", ";")
                        'tn.NavigateUrl = "../SICAH/CaucePuntDetalle.aspx?nodotexto='" & fila("Descripcion") & "'&nodoclave='" & fila("ClaveNodo") & "'"
                        'ncm 14/10/2008
                        tn.NavigateUrl = "../SICAH/InformacionGeneral.aspx?nodotexto=" & fila("Descripcion") & "&nodoclave=" & Replace(fila("ClaveNodo"), "#", ";") & "&X=" & fila("X")
                        'tn.NavigateUrl = "../SICAH/InformacionGeneral_ncm.aspx?nodotexto=" & fila("Descripcion") & "&nodoclave=" & Replace(fila("ClaveNodo"), "#", ";")
                        'tn.NavigateUrl = "../SICAH/CaucePuntDETALLE.aspx?nodotexto=" & fila("Descripcion") & "&nodoclave=" & Replace(fila("ClaveNodo"), "#", ";")
                        tn.Target = "iframeDetalle"
                        tn.Expanded = False
                        InsertarNodosBISBIS(fila, tn)
                        tvwArbol.Nodes.Add(tn)
                    End If
                End If

            Next
        End Sub

        Shared Sub crearArbolRecursivoBISBIS(ByVal tvwArbol As TreeView, ByVal dstArbol As DataSet)
            'EGB 30/09/2008 Metodo Original de carga del arbol, la visibilidad de los nodos se ejecuta en cliente
            Dim fila As DataRow
            For Each fila In dstArbol.Tables("TablaArbol").Rows
                If (fila.IsNull("IdArbolPadre")) Then
                    Dim tn As New TreeNode
                    tn.Text = fila("StrNodo")
                    tn.Value = fila("ClaveNodo")
                    tn.ToolTip = ""
                    'EGB 14/07/2008 Corrección para permitir la activación de nodos de primer nivel
                    'tn.SelectAction = TreeNodeSelectAction.Select
                    'EGB 06/08/2008 Corrección para permitir la activacion de nodos de primer nivel
                    'según hizo NAY en InsertarNodosBISBIS(fila,tn, tipoNodo)
                    'tn.NavigateUrl = "../SICAH/CaucePuntDETALLE.aspx?nodotexto=" & fila("Descripcion") & "&nodoclave=" & Replace(fila("ClaveNodo"), "#", ";")
                    'tn.NavigateUrl = "../SICAH/CaucePuntDetalle.aspx?nodotexto='" & fila("Descripcion") & "'&nodoclave='" & fila("ClaveNodo") & "'"
                    'ncm 14/10/2008
                    tn.NavigateUrl = "../SICAH/InformacionGeneral.aspx?nodotexto=" & fila("Descripcion") & "&nodoclave=" & Replace(fila("ClaveNodo"), "#", ";") & "&X=" & fila("X")
                    'tn.NavigateUrl = "../SICAH/InformacionGeneral_ncm.aspx?nodotexto=" & fila("Descripcion") & "&nodoclave=" & Replace(fila("ClaveNodo"), "#", ";")

                    tn.Target = "iframeDetalle"
                    tn.Expanded = False
                    InsertarNodosBISBIS(fila, tn)
                    tvwArbol.Nodes.Add(tn)
                End If
            Next
        End Sub

        Shared Function crearArbolRecursivoBISBIS(ByVal tvwArbol As TreeView, ByVal dstArbol As DataSet, ByVal tipoElemento As String) As Integer
            'EGB Metodo Sobrecargado para admitir nuevo parámetro de tipo de elemento.
            Try
                Dim fila As DataRow
                For Each fila In dstArbol.Tables("TablaArbol").Rows
                    If (fila.IsNull("IdArbolPadre")) Then
                        Dim tn As New TreeNode
                        tn.Text = fila("StrNodo")
                        tn.Value = fila("ClaveNodo")
                        tn.ToolTip = ""
                        tn.SelectAction = TreeNodeSelectAction.None
                        tn.Expanded = False
                        InsertarNodosPorTipoElemento(fila, tn, tipoElemento)
                        tvwArbol.Nodes.Add(tn)
                    End If
                Next
                crearArbolRecursivoBISBIS = 1
            Catch e As Exception
                crearArbolRecursivoBISBIS = 0
            End Try

        End Function

        Shared Sub crearArbolRecursivoBISBISMTO(ByVal tvwArbol As TreeView, ByVal dstArbol As DataSet, ByVal txtLecturasVisor As String)
            Dim fila As DataRow
            For Each fila In dstArbol.Tables("TablaArbol").Rows
                If (fila.IsNull("IdArbolPadre")) Then
                    Dim tn As New TreeNode
                    tn.Text = fila("StrNodo")
                    tn.Value = fila("ClaveNodo")
                    tn.ToolTip = ""
                    tn.SelectAction = TreeNodeSelectAction.None
                    tn.NavigateUrl = "../SICAH/MantenimientosDETALLE.aspx?nodotexto=" & fila("Descripcion") & "&nodoclave=" & Replace(fila("ClaveNodo"), "#", ";") & "&Tipo=" & fila("Tipo") & "&idnodo=" & tn.ValuePath & "&LecturasVisor=" & txtLecturasVisor.ToString
                    tn.Target = "iframeDetalle"
                    tn.Expanded = False
                    InsertarNodosBISBISMTO(fila, tn, txtLecturasVisor)
                    tvwArbol.Nodes.Add(tn)
                End If
            Next
        End Sub

        Shared Sub crearArbolRecursivoEnMenu(ByVal tvwArbol As TreeView, ByVal dstArbol As DataSet, ByVal tipoNodo As String)
            'EGB Metodo De carga del arbol de cauces en modo de funcionamiento de Menu Emergente
            Dim fila As DataRow
            For Each fila In dstArbol.Tables("TablaArbol").Rows
                If (fila.IsNull("IdArbolPadre")) Then
                    Dim tn As New TreeNode
                    tn.Text = fila("StrNodo")
                    If tipoNodo = "" Then
                        ''EGB 04/02/2009 Agregamos las claves de CodigoPVYCR y ElementoMedida.
                        'tn.Value = fila("ClaveNodo") & "#" & fila("CodigoPVYCR") & "#" & utiles.nullABlanco(fila("IdElementoMedida"))
                        'EGB 10/02/2009 Agregamos las claves de CodigoPVYCR y ElementoMedida + IdArbol
                        tn.Value = fila("ClaveNodo") & "#" & fila("CodigoPVYCR") & "#" & utiles.nullABlanco(fila("IdElementoMedida")) & "#" & fila("IdArbol")
                    Else
                        'EGB 10/02/2009 Agregarmos las claves de IdArbol
                        tn.Value = fila("ClaveNodo") & "#" & fila("IdArbol")
                    End If

                    tn.ToolTip = ""
                    'tn.SelectAction = TreeNodeSelectAction.None
                    'tn.NavigateUrl = "javascript:void(0);" '"../SICAH/CaucePunt2.aspx?nodotexto='" & fila("Descripcion") & "'&nodoclave='" & fila("ClaveNodo") & "'"
                    'tn.Target = "_self"
                    tn.Expanded = False
                    InsertarNodosBISBIS(fila, tn, tipoNodo)
                    tvwArbol.Nodes.Add(tn)
                End If
            Next
        End Sub
        Shared Sub InsertarNodosBISBIS(ByVal fila As DataRow, ByRef nodo As TreeNode, ByVal tiponodo As String)
            'EGB Metodo Sobrecargado para admitir nuevo parámetro de tipo de nodo.
            'La funcion  se utiliza para el arbol emergente de mantenimientos 
            Dim f As DataRow

            For Each f In fila.GetChildRows("SelfRefenceRelation")
                If f("Tipo") = tiponodo Then
                    'No se carga el nodo
                Else
                    Dim tn As New TreeNode
                    tn.Text = f("StrNodo").ToString

                    If tiponodo = "" Then
                        'EGB 04/02/2009 Agregamos las claves de CodigoPVYCR y ElementoMedida.
                        'tn.Value = f("ClaveNodo") & "#" & f("CodigoPVYCR") & "#" & utiles.nullABlanco(f("IdElementoMedida"))
                        'EGB 10/02/2009 Agregamos las claves de CodigoPVYCR y ElementoMedida + IdArbol
                        tn.Value = f("ClaveNodo") & "#" & f("CodigoPVYCR") & "#" & utiles.nullABlanco(f("IdElementoMedida")) & "#" & f("IdArbol")
                    Else
                        tn.Value = f("ClaveNodo") & "#" & f("IdArbol")
                    End If

                    'tn.SelectAction = TreeNodeSelectAction.None
                    'tn.NavigateUrl = "javascript:void(0);" '"../SICAH/CaucePuntDETALLE.aspx?nodotexto=" & f("Descripcion") & "&nodoclave=" & Replace(f("ClaveNodo"), "#", ";")
                    tn.ToolTip = ""
                    tn.Expanded = False
                    'tn.Target = "_self"

                    nodo.ChildNodes.Add(tn)
                    tn.Expanded = False
                    InsertarNodosBISBIS(f, tn, tiponodo)
                End If
            Next

        End Sub
        Shared Sub InsertarNodosBISBIS(ByVal fila As DataRow, ByRef nodo As TreeNode)
            '
            Dim f As DataRow

            For Each f In fila.GetChildRows("SelfRefenceRelation")
                Dim tn As New TreeNode
                tn.Text = f("StrNodo")
                tn.Value = f("ClaveNodo") & "#" & fila("X")

                'tn.NavigateUrl = "../SICAH/CaucePuntDETALLE.aspx?nodotexto=" & f("Descripcion") & "&nodoclave=" & Replace(f("ClaveNodo"), "#", ";")
                'NCM comentado el 04/09/2008
                'ncm 14/10/2008
                tn.NavigateUrl = "../SICAH/InformacionGeneral.aspx?nodotexto=" & f("Descripcion") & "&nodoclave=" & Replace(f("ClaveNodo"), "#", ";") & "&X=" & f("X")
                'tn.NavigateUrl = "../SICAH/InformacionGeneral_ncm.aspx?nodotexto=" & f("Descripcion") & "&nodoclave=" & Replace(f("ClaveNodo"), "#", ";")
                tn.ToolTip = ""
                tn.Expanded = False
                tn.Target = "iframeDetalle"
                nodo.ChildNodes.Add(tn)
                tn.Expanded = False
                InsertarNodosBISBIS(f, tn)
            Next

        End Sub
        Shared Sub InsertarNodosPorTipoElemento(ByVal fila As DataRow, ByRef nodo As TreeNode, ByVal TipoElemento As String)
            'EGB 12/08/2008 Metodo Sobrecargado para admitir nuevo parámetro de tipo de elemento 
            'Se utiliza para cargar solo los elementos de determinado tipo por ejemplo elementos del tipo Q 
            'Ref. CurvaAcequiasARBOL.aspx

            Dim f As DataRow

            For Each f In fila.GetChildRows("SelfRefenceRelation")
                'EGB 13/08/2008 Comento el filtro porque el arbol al final muestra la coleccion completa
                'y desde CurvasAcequiasBUSCAR es desde donde filtro los elementos tipo Q que tengan datos asociados a 
                'PVYCR_CurvasAcequias
                'If (Left(nullABlanco(f("IdElementoMedida")), 1) = TipoElemento And f("Tipo") = "E") Or (f("Tipo") = "C") Or (f("Tipo") = "P") Then
                Dim tn As New TreeNode
                tn.Text = f("StrNodo")
                tn.Value = f("ClaveNodo")
                'NCM comentado el 17/10/2008
                'tn.NavigateUrl = "../SICAH/CaucePuntDETALLE.aspx?nodotexto=" & f("Descripcion") & "&nodoclave=" & Replace(f("ClaveNodo"), "#", ";")
                '--
                'EGB 13/08/2008 Inclusión de nuevas claves para la localización de los elementos de medida
                tn.NavigateUrl = "../SICAH/CurvasAcequiasDETALLE.aspx?nodotexto=" & f("Descripcion") & "&nodoclave=" & Replace(f("ClaveNodo"), "#", ";") & "&CodigoPVYCR=" & f("CodigoPVYCR") & "&IdElementoMedida=" & f("IdElementoMedida") & "&X=" & f("X")
                tn.ToolTip = ""
                tn.Expanded = False
                tn.Target = "iframeDetalle"
                nodo.ChildNodes.Add(tn)
                tn.Expanded = False
                InsertarNodosPorTipoElemento(f, tn, TipoElemento)
                'End If


            Next

        End Sub

        Shared Sub InsertarNodosBISBISMTO(ByVal fila As DataRow, ByRef nodo As TreeNode, ByVal txtLecturasVisor As String)

            Dim f As DataRow

            For Each f In fila.GetChildRows("SelfRefenceRelation")
                Dim tn As New TreeNode
                tn.Text = f("StrNodo")
                tn.Value = f("ClaveNodo")
                tn.NavigateUrl = "../SICAH/MantenimientosDETALLE.aspx?nodotexto=" & f("Descripcion") & "&nodoclave=" & Replace(f("ClaveNodo"), "#", ";") & "&Tipo=" & f("Tipo") & "&LecturasVisor=" & txtLecturasVisor
                tn.ToolTip = ""
                tn.Expanded = False
                tn.Target = "iframeDetalle"
                nodo.ChildNodes.Add(tn)
                tn.Expanded = False
                InsertarNodosBISBISMTO(f, tn, txtLecturasVisor)
            Next

        End Sub
        Shared Function verDescPrefijo(ByVal prefijo As String) As String
            Dim descripcion As String
            descripcion = ""
            Select Case prefijo
                Case "ARG"
                    descripcion = "Acequias"
                Case "CM"
                    descripcion = "Cabecera - Río Mundo"
                Case "VA"
                    descripcion = "Vega Alta"
                Case "VM"
                    descripcion = "Vega Media"
                Case "VB"
                    descripcion = "Vega Baja"
                Case "OT000 hasta OT099"
                    descripcion = "Residuales EDAR's"
                Case "OT100 hasta OT999"
                    descripcion = "Pozos de CHS"
                Case "S"
                    descripcion = "Pozos Subterráneas"
            End Select

            Return descripcion
        End Function
        Shared Sub InsertarNodosCaucesAgrupaciones(ByVal fila As DataRow, ByVal nodopadreAgru As TreeNode, ByVal nivel As Integer, ByVal dstArbol As DataSet)
            Dim f, registros() As DataRow
            Dim tn As New TreeNode
            tn.Text = "<font color=" & GuarderiaFluvial.utiles.GradienteNivelesGranates(nivel) & "><b>" & fila("Descripcion") & "</b>"
            tn.Value = fila("IdAgrupacionPadre") & "&" & fila("idAgrupacionHija")
            tn.ImageUrl = "~/SICAH/IMAGES/SubSistemas.GIF"
            tn.Expand()
            nodopadreAgru.ChildNodes.Add(tn)
            registros = dstArbol.Tables("tablaRelacionAgrupacionesPadre").Select("IdAgrupacionPadre= '" & fila.Item("IdAgrupacionPadre").ToString & "'")
            For Each f In registros
                InsertarNodosCaucesSistemas(f, tn, nivel + 1, dstArbol)
            Next
        End Sub

        Shared Sub crearArbolRecursivoAgrupaciones(ByVal tvwArbolAgrupaciones As TreeView, ByVal dstArbol As DataSet)
            Dim fila As DataRow
            For Each fila In dstArbol.Tables("tablaRelacionAgrupacionesPadre").Rows
                If (fila.IsNull("IdAgrupacionPadre")) Then
                    Dim tn As New TreeNode
                    tn.Text = "<font color=" & GuarderiaFluvial.utiles.GradienteNivelesGranates(0) & "><b>" & fila("Descripcion") & "</b>"
                    tn.Value = fila("IdAgrupacionPadre") & "&" & fila("idAgrupacionHija")
                    tn.ImageUrl = "~/SICAH/IMAGES/SubSistemas.GIF"
                    tn.Expanded = True
                    InsertarNodosSistemas(fila, tn, 1)
                    tvwArbolAgrupaciones.Nodes.Add(tn)
                End If
            Next
        End Sub
        Shared Sub InsertarNodosAgrupaciones(ByVal fila As DataRow, ByRef nodo As TreeNode, ByVal nivel As Integer)

            Dim f As DataRow

            For Each f In fila.GetChildRows("SelfRefenceRelation")
                Dim tn As New TreeNode
                tn.Text = "<font color=" & GuarderiaFluvial.utiles.GradienteNivelesGranates(nivel) & "><b>" & f("Descripcion") & "</b>"
                tn.Value = fila("IdAgrupacionPadre") & "&" & fila("idAgrupacionHija")
                tn.ImageUrl = "~/SICAH/IMAGES/SubSistemas2.GIF"
                tn.Expand()
                nodo.ChildNodes.Add(tn)
                InsertarNodosSistemas(f, tn, nivel + 1)
            Next

        End Sub

  End Class
End Namespace
