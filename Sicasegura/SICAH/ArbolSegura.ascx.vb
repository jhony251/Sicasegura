Imports System.Data
Imports GuarderiaFluvial


Partial Class ArbolSegura
  Inherits System.Web.UI.UserControl

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura"))
  Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
  Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
  Dim codigoCauce As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            crearDataSetsPuntos()
            crearArbolRecursivo()
        End If
    End Sub

  Private Sub crearArbolRecursivo()
    Dim i As Integer
    For i = 0 To dstarbol.Tables("tablaCaucesPadres").Rows.Count - 1
      insertarNodosRecursivos(dstarbol.Tables("tablaCaucesPadres").Rows(i).Item("caucepadre"), Nothing, Nothing, -1)
    Next
  End Sub

  Private Sub insertarNodosRecursivos(ByVal cauce As String, ByVal denominacionCauce As String, ByVal elNodoPadre As TreeNode, ByVal nivel As Integer)
        Dim registros(), registros2() As DataRow
        Dim i, j As Integer
        Dim tn, te As TreeNode

        Dim v_ot, tiposensor As String
        'código bueno

        'Select Case nivel
        '  Case -1
        '    If cauce = "OT000 hasta OT099" Or cauce = "OT100 hasta OT999" Then
        '      v_ot = cauce
        '      cauce = "OT"
        '    End If
        '    If cauce <> "OT" Then
        '      tn = New TreeNode("<b>" & cauce & " " & verDescPrefijo(cauce) & "</b>")
        '    Else
        '      tn = New TreeNode("<b>" & v_ot & " " & verDescPrefijo(v_ot) & "</b>")
        '    End If
        '    tn.Expanded = False
        '    Me.treeView1.Nodes.Add(tn)

        '    registros = dstarbol.Tables("tablaCauces").Select("codigoCauce LIKE '" & cauce & "%'")
        '    For i = 0 To registros.Length - 1
        '      If cauce <> "OT" Then
        '        If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 Then
        '          insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
        '        End If
        '      Else
        '        If v_ot = "OT000 hasta OT099" Then
        '          If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 And Integer.Parse(Mid(registros(i)("codigoCauce").ToString(), 3, 3)) < 100 Then
        '            insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
        '          End If

        '        ElseIf v_ot = "OT100 hasta OT999" Then
        '          If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 And Integer.Parse(Mid(registros(i)("codigoCauce").ToString(), 3, 3)) >= 100 Then
        '            insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
        '          End If

        '        End If
        '      End If
        '    Next
        '  Case Else

        '    tn = New TreeNode(cauce & " - " & denominacionCauce)
        '    elNodoPadre.ChildNodes.Add(tn)
        '    elNodoPadre.Expanded = False
        '    registros = dstarbol.Tables("tablaPuntos").Select("codigoCauce = '" & cauce & "'")

        '    For i = 0 To registros.Length - 1
        '      If registros(i).Item("TipoSensor") Is System.DBNull.Value Then
        '        tiposensor = ")"
        '      Else
        '        tiposensor = " - " & registros(i).Item("TipoSensor").ToString & ")"
        '      End If
        '      If registros(i)("tipoPunto").ToString = "M" Then
        '        tn.ChildNodes.Add(New TreeNode("<img src='images/puntoMotor.ico' border=0>&nbsp;&nbsp;" & "<font color=black>" & registros(i).Item("codigoPVYCR").ToString() & "(M" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</font>"))
        '      Else
        '        tn.ChildNodes.Add(New TreeNode("<img src='images/puntoAcequia2.gif' border=0>&nbsp;&nbsp;" & "<font color=black>" & registros(i).Item("codigoPVYCR").ToString() & "(G" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</font>"))
        '      End If
        '      tn.Expanded = False
        '    Next

        '    registros = dstarbol.Tables("tablaCauces").Select("codigoCauce LIKE '" & cauce & "%' AND codigoCauce <> '" & cauce & "'")
        '    For i = 0 To registros.Length - 1
        '      If numeroGuiones(registros(i)("codigoCauce").ToString()) = nivel + 1 Then
        '        insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
        '      End If
        '    Next
        'End Select
        ' fin código bueno

        Select Case nivel
            Case -1
                If cauce = "OT000 hasta OT099" Or cauce = "OT100 hasta OT999" Then
                    v_ot = cauce
                    cauce = "OT"
                End If
                If cauce <> "OT" Then
                    tn = New TreeNode("<b>" & cauce & " " & verDescPrefijo(cauce) & "</b>", "C")
                Else
                    tn = New TreeNode("<b>" & v_ot & " " & verDescPrefijo(v_ot) & "</b>", "C")
                End If
                tn.Expanded = False
                Me.treeView1.Nodes.Add(tn)

                registros = dstarbol.Tables("tablaCauces").Select("codigoCauce LIKE '" & cauce & "%'")
                For i = 0 To registros.Length - 1
                    If cauce <> "OT" Then
                        If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 Then
                            insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
                        End If
                    Else
                        If v_ot = "OT000 hasta OT099" Then
                            If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 And Integer.Parse(Mid(registros(i)("codigoCauce").ToString(), 3, 3)) < 100 Then
                                insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
                            End If

                        ElseIf v_ot = "OT100 hasta OT999" Then
                            If numeroGuiones(registros(i)("codigoCauce").ToString()) <= 0 And Integer.Parse(Mid(registros(i)("codigoCauce").ToString(), 3, 3)) >= 100 Then
                                insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
                            End If

                        End If
                    End If
                Next
            Case Else

                tn = New TreeNode(cauce & " - " & denominacionCauce, "C")
                elNodoPadre.ChildNodes.Add(tn)
                elNodoPadre.Expanded = False
                registros = dstarbol.Tables("tablaPuntos").Select("codigoCauce = '" & cauce & "'")
             
                For i = 0 To registros.Length - 1
                    'obtenemos datos de elementos medida
                    registros2 = dstarbol.Tables("TablaElementosMedidad").Select("CodigoPVYCR = '" & registros(i).Item("CodigoPVYCR").ToString & "'")

                    If registros(i).Item("TipoSensor") Is System.DBNull.Value Then
                        tiposensor = ")"
                    Else
                        tiposensor = " - " & registros(i).Item("TipoSensor").ToString & ")"
                    End If
                    If registros(i)("tipoPunto").ToString = "M" Then
                        'esto estaba antes de insertar elementos de medida
                        'tn.ChildNodes.Add(New TreeNode("<img src='images/puntoMotor.ico' border=0>&nbsp;&nbsp;" & "<font color=black>" & registros(i).Item("codigoPVYCR").ToString() & "(M" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</font>"))
                        te = New TreeNode("<img src='images/puntoMotor.ico' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & "<b>" & registros(i).Item("codigoPVYCR").ToString() & "(M" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "<b/></font>", "P")
                        tn.ChildNodes.Add(te)
                        'Insertamos elementos medida
                        For j = 0 To registros2.Length - 1
                            te.ChildNodes.Add(New TreeNode("<img src='images/distancias.gif' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & registros2(j).Item("idelementoMedida").ToString & " - " & registros2(j).Item("DESCTIPO").ToString, "E"))
                        Next
                        'fin no pasar
                    Else
                        'esto estaba antes de insertar elementos de medida
                        'tn.ChildNodes.Add(New TreeNode("<img src='images/puntoAcequia2.gif' border=0>&nbsp;&nbsp;" & "<font color=black>" & registros(i).Item("codigoPVYCR").ToString() & "(G" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "</font>"))
                        te = New TreeNode("<img src='images/puntoMotor.ico' border=0>&nbsp;&nbsp;" & "<font color=#008500><b>" & registros(i).Item("codigoPVYCR").ToString() & "(M" & tiposensor & " - " & registros(i).Item("DenominacionPunto").ToString & "<b/></font>", "P")
                        tn.ChildNodes.Add(te)
                        'insertamos elementos medida
                        For j = 0 To registros2.Length - 1
                            te.ChildNodes.Add(New TreeNode("<img src='images/distancias.gif' border=0>&nbsp;&nbsp;" & "<font color=#008500>" & registros2(j).Item("idelementoMedida").ToString & " - " & registros2(j).Item("DESCTIPO").ToString, "E"))
                        Next
                    End If
                    tn.Expanded = False
                    te.Expanded = False
                Next

                registros = dstarbol.Tables("tablaCauces").Select("codigoCauce LIKE '" & cauce & "%' AND codigoCauce <> '" & cauce & "'")
                For i = 0 To registros.Length - 1
                    If numeroGuiones(registros(i)("codigoCauce").ToString()) = nivel + 1 Then
                        insertarNodosRecursivos(registros(i)("codigoCauce").ToString(), registros(i)("denominacionCauce").ToString(), tn, nivel + 1)
                    End If
                Next
        End Select

  End Sub

  Private Function numeroGuiones(ByVal cadena As String) As Integer
    Return Math.Max(Split(cadena, "-").Length - 1, 0)
  End Function


  
  Protected Function verDescPrefijo(ByVal prefijo As String) As String
    Dim descripcion As String

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
            Case "CRE"
                descripcion = "Crevillente"

        End Select

    Return descripcion
  End Function



  Private Sub crearDataSetsPuntos()
        dstarbol.Clear()
        treeView1.Nodes.Clear()
        utiles.Comprobar_Conexion_BD(Page, conexion)
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
             "WHEN 'C' THEN 'CAUDAL' " & _
             "WHEN 'H' THEN 'HORAS' " & _
             "WHEN 'V' THEN 'VOLUMEN' " & _
             "WHEN 'E' THEN 'ENERGÍA' " & _
            "END from PVYCR_Elementosmedida E, PVYCR_puntos P " & _
        "where(E.codigoPVYCR = P.codigoPVYCR) order by E.codigoPVYCR  "
        daArbol.Fill(dstarbol, "TablaElementosMedidad")

  End Sub


    Protected Sub treeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView1.SelectedNodeChanged

        Response.Write("nodo: " & treeView1.SelectedValue)
        'Response.Redirect("Contadores.aspx")
    End Sub

 
End Class
