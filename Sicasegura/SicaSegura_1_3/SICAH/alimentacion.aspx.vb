Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Partial Class SICAH_alimentacion
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daAlimentacion As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstAlimentacion As DataSet = New System.Data.DataSet()

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0
    Dim ddlF As DropDownList
    Dim ddlE As DropDownList
    Dim ddlV As DropDownList

    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Protected Function checkFuentedato(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return " "
        Else

            Return elDataitem("Cod_Fuente_Dato")
        End If
    End Function
    Protected Function checkColumna_diferencial(ByVal elDataItem As DataRowView) As String
        Dim diferencial As Decimal
        If utiles.nullABlanco(elDataItem("Diferencial")) = "" Then
            diferencial = 0
        Else
            diferencial = elDataItem("Diferencial")
        End If
        If diferencial < 0 Then
            Return "class=""DiferencialPre"""
        Else
            Return "class=""TipoCaudal"""
        End If

    End Function
    Protected Function checkNombreAlimentacion(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return " "
        Else
            'Return elDataitem("CodigoPVYCR") & " / " & elDataitem("DenominacionPunto") & " / " & elDataitem("DenominacionCauce")
            Return elDataitem("DenominacionCauce")
        End If

    End Function
    Protected Function checkCodigoAlimentacion(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return " "
        Else
            lbl1.Text = elDataitem("idElementoMedida")
            Return elDataitem("CodigoPVYCR") & " / " & elDataitem("Cod_fuente_dato") & " / " & elDataitem("DenominacionPunto") & " / " & elDataitem("idElementoMedida")
        End If

    End Function
    Protected Function checkNumRegEstadilloM(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return 0
        Else
            Dim v_result As String
            daAlimentacion.SelectCommand.CommandText = "SELECT count(*) NumRegEstdilloM FROM PVYCR_DatosAlimentacion_Estadillo E " & _
                                                  "where E.CodigoPVYCR = '" & elDataitem("CodigoPVYCR") & "' And E.Cod_Fuente_Dato = '" & elDataitem("Cod_Fuente_Dato") & "' " & _
                                                   " and E.idElementoMedida = '" & elDataitem("idElementoMedida") & "'"

            daAlimentacion.Fill(dstAlimentacion, "TablaNumRegEstadilloM")
            v_result = dstAlimentacion.Tables("TablaNumRegEstadilloM").Rows((dstAlimentacion.Tables("TablaNumRegEstadilloM").Rows.Count - 1)).Item("NumRegEstdilloM").ToString
            Return v_result
        End If

    End Function

    Public Sub calcularPags()
        Dim i As Integer
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim txtComando As String = ""
        txtComando = daAlimentacion.SelectCommand.CommandText
        Try
            txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
        Catch Exc As Exception
            Response.Write("Error: " & Exc.Message)
        End Try


        comando.CommandText = "select count(*) from (" & txtComando & ") dtable"
        utiles.Comprobar_Conexion_BD(Page, conexion)
        numpaginas = comando.ExecuteScalar

        If numpaginas Mod pageSize <> 0 Then
            numpaginas = CInt(numpaginas / pageSize - 0.5) + 1
        Else
            numpaginas = CInt(numpaginas / pageSize)
        End If

        ddlPaginacion.Items.Clear()
        If numpaginas = 0 Then
            numpaginas = 1
        End If

        For i = 1 To numpaginas
            ddlPaginacion.Items.Add(New System.Web.UI.WebControls.ListItem(i, i))
        Next
        If CInt(lblPagina.Text) > numpaginas Then
            ddlPaginacion.SelectedIndex = -1
        Else
            ddlPaginacion.SelectedIndex = CInt(lblPagina.Text) - 1
        End If
    End Sub
    Private Sub crearDataSets()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daAlimentacion.SelectCommand.CommandText = sentenciaOrder
        daAlimentacion.Fill(dstAlimentacion, "TablaAlimentacion")
        'daAlimentacion.Fill(dstAlimentacion, (CInt(lblPagina.Text) - 1) * pageSize, pageSize, "TablaAlimentacion")
        'ncm comentado el 25/08/2008 por peticion del usuario
        ''Cálculo del número de páginas
        'calcularPags()

        'If CInt(lblPagina.Text) < numpaginas Then
        '    lbtNext.Visible = True
        'Else
        '    lbtNext.Visible = False
        'End If
        'If CInt(lblPagina.Text) > 1 Then
        '    lbtPrev.Visible = True
        'Else
        '    lbtPrev.Visible = False
        'End If
    End Sub
    Private Sub ddlPaginacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPaginacion.SelectedIndexChanged
        lblPagina.Text = ddlPaginacion.Items(ddlPaginacion.SelectedIndex).Value
        If Session("ordenacion").ToString = "" Then


            sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                       "FROM PVYCR_DatosAlimentacion_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                       "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                        "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
            '"DM.Cod_Fuente_Dato = '05'"
            If txtFiltro.Text <> "" Then
                sentenciaSel &= " and DM.CodigoPVYCR like '%" & txtFiltro.Text & "%' "
            End If
            sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.Cod_Fuente_Dato "
        Else
            sentenciaOrder = Session("Ordenacion").ToString
        End If
        crearDataSets()
        rptAlimentacion.DataSource = dstAlimentacion.Tables("TablaAlimentacion")
        rptAlimentacion.DataBind()
    End Sub

    Private Sub lbtNext_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtNext.Command, lbtPrev.Command, lbtFirst.Command, lbtLast.Command
        If e.CommandName = "pagPrev" Then
            lblPagina.Text = (CInt(lblPagina.Text) - 1).ToString()
        ElseIf e.CommandName = "pagSig" Then
            lblPagina.Text = (CInt(lblPagina.Text) + 1).ToString()
        ElseIf e.CommandName = "firstPage" Then
            lblPagina.Text = "1"
        ElseIf e.CommandName = "lastPage" Then
            lblPagina.Text = ddlPaginacion.Items(ddlPaginacion.Items.Count - 1).Value
        End If

        If Session("ordenacion").ToString = "" Then


            sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                          "FROM PVYCR_DatosAlimentacion_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                          "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                           "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
            '"DM.Cod_Fuente_Dato = '05'"
            If txtFiltro.Text <> "" Then
                sentenciaSel &= " and DM.CodigoPVYCR like '%" & txtFiltro.Text & "%' "
            End If
            sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.Cod_Fuente_Dato "
        Else
            sentenciaOrder = Session("Ordenacion").ToString
        End If
        crearDataSets()
        rptAlimentacion.DataSource = dstAlimentacion.Tables("TablaAlimentacion")
        rptAlimentacion.DataBind()
    End Sub
    Protected Sub PVYCRSeleccionada(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim parametros() As String = Split(CType(sender, LinkButton).CommandArgument, "#")
        lblidAlimentacion.Text = parametros(0)
        'lblidAlimentacion.Text = CType(sender, LinkButton).CommandArgument

        pnlAlimentacion.Visible = False
        Panel1.Visible = False
        pnlEtiquetas.Visible = True
        pnlBotones.Visible = True
        pnlEstadillo.Visible = True
        pnlPrincipal.Visible = True
        Dim v_fuentedato As String = parametros(1)
        crearDataSetMEstadillo(v_fuentedato)

        lblNombrePVYCR.Text = lblidAlimentacion.Text

        dstAlimentacion.Tables("TablaAlimentacion").DefaultView.Sort = "fecha_medida "
        rptAlimentacionDetalle.DataSource = dstAlimentacion.Tables("TablaAlimentacion").DefaultView
        rptAlimentacionDetalle.DataBind()

        rptAlimentacionDetalle2.DataSource = dstAlimentacion.Tables("TablaAlimentacion2")
        rptAlimentacionDetalle2.DataBind()

        'If Not IsNothing(boton) The
        'boton.Visible = True
        'End If

    End Sub
    Private Sub crearDataSetMEstadillo(ByVal v_codfuentedato As String)
        Dim vpos As Integer
        Dim vcadena1, vcadena2 As String

        utiles.Comprobar_Conexion_BD(Page, conexion)
        'obtenemos la posición de la primera / para saber hasta dónde tenemos que hacer el subtring para ontener el códigoPVYCR
        vpos = lblidAlimentacion.Text.IndexOf("/")
        vcadena1 = lblidAlimentacion.Text.Substring(0, vpos - 1)
        'obtenemos el idelemento
        vcadena2 = lblidAlimentacion.Text.Substring(lblidAlimentacion.Text.Length - 3, 3)

        daAlimentacion.SelectCommand.CommandText = "SELECT top 10 D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                          "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
                                          "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
                                          "IV.descripcion IncElec " & _
                                          "FROM PVYCR_DatosAlimentacion D " & _
                                          "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
                                          "where D.idElementoMedida = '" & vcadena2 & "' and D.CodigoPVYCR = '" & vcadena1 & "' And D.Cod_Fuente_Dato = '" & v_codfuentedato & "' " & _
                                          "order by D.Fecha_Medida desc"

        daAlimentacion.Fill(dstAlimentacion, "TablaAlimentacion")

        daAlimentacion.SelectCommand.CommandText = "SELECT E.CodigoPVYCR, E.Cod_Fuente_Dato, E.Fecha_Medida, E.idElementoMedida, " & _
                                          "E.LecturaI,E.LecturaII, E.LecturaIII,E.total_Kwh, E.total_kvar, E.Funciona, E.Observaciones, E.idIncidenciaelectrica, " & _
                                          "E.ConsumoElectricoAdicional, E.ReinicioLecturaElectrica, E.Justificacion,E.login " & _
                                          "FROM PVYCR_DatosAlimentacion_Estadillo E " & _
                                          "where E.idElementoMedida = '" & vcadena2 & "' and E.CodigoPVYCR = '" & vcadena1 & "' And E.Cod_Fuente_Dato = '" & v_codfuentedato & "' " & _
                                          "order by E.Fecha_Medida"
        'daAlimentacion.SelectCommand.CommandText = "SELECT E.CodigoPVYCR, E.Cod_Fuente_Dato, E.Fecha_Medida, E.idElementoMedida, " & _
        '                                "E.LecturaI,E.LecturaII, E.LecturaIII,E.total_Kwh, E.total_kvar, E.Funciona, E.Observaciones, E.idIncidenciaelectrica, " & _
        '                                "E.ConsumoElectricoAdicional, E.ReinicioLecturaElectrica, E.Justificacion " & _
        '                                "FROM PVYCR_DatosAlimentacion_Estadillo E " & _
        '                                "where E.idElementoMedida = '" & lbl1.Text & "' and E.CodigoPVYCR = '" & vcadena1 & "' And E.Cod_Fuente_Dato = '" & lblFuenteDato.Text & "' " & _
        '                                "order by E.Fecha_Medida"
        daAlimentacion.Fill(dstAlimentacion, "TablaAlimentacion2")
        obtenerVolumenDiferencial()

    End Sub

    Protected Sub rptAlimentacionDetalle2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptAlimentacionDetalle2.ItemDataBound
        'el itemDataBound se carga cada vez por cada fila del repeater, éste if es para que sólo
        'evalue los items del cuerpo
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim i As Integer
            ddlF = CType(e.Item.FindControl("ddlFunciona"), DropDownList)
            ddlE = CType(e.Item.FindControl("ddlIncidenciasE"), DropDownList)

            'en el e.item.dataitem lo que tengo es la fila de la tabla con todos sus valores
            Select Case (utiles.nullABlanco(CType(e.Item.DataItem, DataRowView).Item("funciona"))).ToString
                Case "SI"
                    ddlF.SelectedIndex = 0
                Case "NO"
                    ddlF.SelectedIndex = 1
                Case Else
                    ddlF.SelectedIndex = 2
            End Select

            For i = 0 To ddlE.Items.Count - 1
                If ddlE.Items(i).Value = utiles.nullABlanco(CType(e.Item.DataItem, DataRowView).Item("idIncidenciaElectrica")) Then
                    ddlE.Items(i).Selected = True
                Else
                    ddlE.Items(i).Selected = False
                End If
            Next

          
            ' ponemos este código aquí y no en el rellenar lista porque es aquí dónde se cargan las listas
            'y sino, nos recarga y nos quita el seleccionar
            ddlE.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        End If
    End Sub

    Protected Sub btnAceptarLectura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarLectura.Click

        Dim i As Integer
        Dim v_CodigoPVYCR, v_Cod_Fuente_Dato, v_Fecha, v_Hora, v_Funciona, v_idElem As String
        Dim v_inciVol, v_InitElec, v_observaciones As String
        Dim mensaje As String = ""

        For i = 0 To rptAlimentacionDetalle2.Items.Count - 1
            'si marcan que la aceptan, insertaremos en TDatosAlimentacion y borraremos del estadillo
            'código de aceptación de lectura para la lectura actual
            v_idElem = CType(rptAlimentacionDetalle2.Items(i).FindControl("txtidElementoMedida"), TextBox).Text
            v_CodigoPVYCR = CType(rptAlimentacionDetalle2.Items(i).FindControl("txtCodigoPVYCR"), TextBox).Text
            v_Cod_Fuente_Dato = CType(rptAlimentacionDetalle2.Items(i).FindControl("txtCod_Fuente_Dato"), TextBox).Text
            v_Fecha = CType(rptAlimentacionDetalle2.Items(i).FindControl("txtFecha_Medida"), TextBox).Text

            v_InitElec = CType(rptAlimentacionDetalle2.Items(i).FindControl("ddlIncidenciasE"), DropDownList).SelectedValue

            v_Funciona = CType(rptAlimentacionDetalle2.Items(i).FindControl("ddlFunciona"), DropDownList).SelectedItem().Text
            If IsNothing(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtObservaciones"), TextBox).Text) Then
                v_observaciones = ""
            Else
                v_observaciones = CType(rptAlimentacionDetalle2.Items(i).FindControl("txtObservaciones"), TextBox).Text
            End If


            If CType(rptAlimentacionDetalle2.Items(i).FindControl("cbAceptarM"), CheckBox).Checked Then

                Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
                Dim objTrans As SqlTransaction
                Try
                    'If conexion.State = ConnectionState.Closed Then conexion.Open()
                    utiles.Comprobar_Conexion_BD(Page, conexion)
                    'abrimos una transaccion para que si falla algunos de los inserts haga un rollback
                    objTrans = conexion.BeginTransaction()
                    comando.Transaction = objTrans
                    comando.CommandText = "INSERT INTO PVYCR_DatosAlimentacion([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                        ",[LecturaI],[LecturaII],[LecturaIII],[Total_Kwh],[Total_Kvar],[Observaciones],[idIncidenciaElectrica],[ConsumoElectricoAdicional] " & _
                        ",[ReinicioLecturaelectrica],[Funciona],[justificacion]) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@Cod_fuente_dato,@Fecha_Medida, " & _
                        "@LecturaI,@LecturaII,@LecturaIII,@Total_KWH,@Total_Kvar,@Observaciones,@idIncidenciaelectrica,@ConsumoElectricoAdicional, " & _
                        "@ReinicioLecturaElectrica, @Funciona,@justificacion)"

                    comando.Parameters.AddWithValue("@idElementoMedida", v_idElem)
                    comando.Parameters.AddWithValue("@CodigoPVYCR", v_CodigoPVYCR)
                    comando.Parameters.AddWithValue("@Fecha_medida", v_Fecha)
                    comando.Parameters.AddWithValue("@Cod_fuente_dato", v_Cod_Fuente_Dato)

                    'Lectura I
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaI"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@LecturaI", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@LecturaI", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaI"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Lectura II
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaII"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@LecturaII", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@LecturaII", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaII"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Lectura III
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaIII"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@LecturaIII", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@LecturaIII", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaIII"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Total Kwh
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtTotal_kwh"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@Total_KWH", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@Total_KWH", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtTotal_kwh"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Total(Kvar)
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txttotal_kvar"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@Total_Kvar", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@Total_Kvar", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txttotal_kvar"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'obserbvaciones
                    comando.Parameters.AddWithValue("@Observaciones", v_observaciones)
                    'Id incidencia eléctrica
                    comando.Parameters.AddWithValue("@idIncidenciaElectrica", utiles.BlancoANull(v_InitElec))
                    'Consumo eléctrico
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtConsumoElec"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ConsumoElectricoAdicional", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ConsumoElectricoAdicional", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtConsumoElec"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Reinico eléctrico
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtReinicioElec"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ReinicioLecturaElectrica", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ReinicioLecturaElectrica", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtreinicioElec"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Funciona
                    comando.Parameters.AddWithValue("@Funciona", v_Funciona)
                    'Justificación
                    comando.Parameters.AddWithValue("@Justificacion", "")

                    comando.ExecuteNonQuery()

                    'insertamos en el histórico
                    comando.CommandText = "INSERT INTO [PVYCR_DatosAlimentacion_Estadillo_Histo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                         ",[LecturaI],[LecturaII],[LecturaIII],[Total_Kwh],[Total_Kvar],[Observaciones],[idIncidenciaElectrica],[ConsumoElectricoAdicional] " & _
                        ",[ReinicioLecturaelectrica],[Funciona],[justificacion],[produccion],login) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_dato,@Fecha_Medida, " & _
                        "@LecturaI,@LecturaII,@LecturaIII,@Total_Kwh,@Total_Kvar,@Observaciones,@idIncidenciaElectrica,@ConsumoElectricoAdicional, " & _
                        "@ReinicioLecturaElectrica, @Funciona,@justificacion, @produccion, @login) "
                    'comando.CommandText = "INSERT INTO [PVYCR_DatosAlimentacion_Estadillo_Histo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                    '                       ",[LecturaI],[LecturaII],[LecturaIII],[Total_Kwh],[Total_Kvar],[Observaciones],[idIncidenciaElectrica],[ConsumoElectricoAdicional] " & _
                    '                      ",[ReinicioLecturaelectrica],[Funciona],[justificacion],[produccion]) " & _
                    '                      "values (@CodigoPVYCR,@idElementoMedida,'05',@Fecha_Medida, " & _
                    '                      "@LecturaI,@LecturaII,@LecturaIII,@Total_Kwh,@Total_Kvar,@Observaciones,@idIncidenciaElectrica,@ConsumoElectricoAdicional, " & _
                    '                      "@ReinicioLecturaElectrica, @Funciona,@justificacion, @produccion) "

                    comando.Parameters.AddWithValue("@Produccion", "S")
                    comando.Parameters.AddWithValue("@login", CType(rptAlimentacionDetalle2.Items(i).FindControl("txtlogin"), TextBox).Text)

                    comando.ExecuteNonQuery()
                    'lo borraremos de la tabla [PVYCR_DatosAlimentacion_estadillo]

                    comando.CommandText = "delete from [PVYCR_DatosAlimentacion_estadillo] where idelementoMedida = '" & v_idElem & "' and CodigoPVYCR = '" & v_CodigoPVYCR & "' and  Cod_Fuente_Dato = '" & v_Cod_Fuente_Dato & "' and Fecha_medida = '" & v_Fecha & "'"

                    comando.ExecuteNonQuery()
                    objTrans.Commit()
                Catch Exc As System.Data.SqlClient.SqlException
                    objTrans.Rollback()
                    Select Case Exc.Number
                        Case 547
                            'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                            'Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                            mensaje = mensaje & "No existe un elemento de medida para el punto con fecha: " & v_Fecha & " \n"
                        Case 2627
                            'Alert(Page, "El dato Alimentacion ya existe para la fecha: " & v_Fecha)
                            mensaje = mensaje & "El dato Alimentacion ya existe para la fecha: " & v_Fecha & " \n"
                    End Select
                Catch Exc As Exception
                    objTrans.Rollback()
                    Alert(Page, "Error: " & Exc.Message)
                End Try

            ElseIf CType(rptAlimentacionDetalle2.Items(i).FindControl("cbRechazarM"), CheckBox).Checked Then
                'código de rechazo de lectura
                Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
                Dim objTrans As SqlTransaction
                Try
                    'If conexion.State = ConnectionState.Closed Then conexion.Open()
                    utiles.Comprobar_Conexion_BD(Page, conexion)
                    objTrans = conexion.BeginTransaction()
                    comando.Transaction = objTrans
                    'insertamos en el histórico
                    comando.CommandText = "INSERT INTO [PVYCR_DatosAlimentacion_Estadillo_Histo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                        ",[LecturaI],[LecturaII],[LecturaIII],[Total_Kwh],[Total_Kvar],[Observaciones],[idIncidenciaElectrica],[ConsumoElectricoAdicional] " & _
                        ",[ReinicioLecturaelectrica],[Funciona],[justificacion],[produccion],login) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_dato,@Fecha_Medida, " & _
                          "@LecturaI,@LecturaII,@LecturaIII,@Total_Kwh,@Total_Kvar,@Observaciones,@idIncidenciaElectrica,@ConsumoElectricoAdicional, " & _
                          "@ReinicioLecturaElectrica, @Funciona,@justificacion, @produccion, @login) "
                    'comando.CommandText = "INSERT INTO [PVYCR_DatosAlimentacion_Estadillo_Histo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                    '                    ",[LecturaI],[LecturaII],[LecturaIII],[Total_Kwh],[Total_Kvar],[Observaciones],[idIncidenciaElectrica],[ConsumoElectricoAdicional] " & _
                    '                    ",[ReinicioLecturaelectrica],[Funciona],[justificacion],[produccion]) " & _
                    '                    "values (@CodigoPVYCR,@idElementoMedida,'05',@Fecha_Medida, " & _
                    '                      "@LecturaI,@LecturaII,@LecturaIII,@Total_Kwh,@Total_Kvar,@Observaciones,@idIncidenciaElectrica,@ConsumoElectricoAdicional, " & _
                    '                      "@ReinicioLecturaElectrica, @Funciona,@justificacion, @produccion) "
                    comando.Parameters.AddWithValue("@idElementoMedida", v_idElem)
                    comando.Parameters.AddWithValue("@CodigoPVYCR", v_CodigoPVYCR)
                    comando.Parameters.AddWithValue("@Fecha_Medida", v_Fecha)
                    comando.Parameters.AddWithValue("@cod_fuente_dato", v_Cod_Fuente_Dato)
                    comando.Parameters.AddWithValue("@login", CType(rptAlimentacionDetalle2.Items(i).FindControl("txtlogin"), TextBox).Text)

                    'Lectura I
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaI"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@LecturaI", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@LecturaI", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaI"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Lectura II
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaII"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@LecturaII", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@LecturaII", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaII"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Lectura III
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaIII"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@LecturaIII", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@LecturaIII", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtLecturaIII"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Total Kwh
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtTotal_kwh"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@Total_KWH", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@Total_KWH", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtTotal_kwh"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Total Kvar
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txttotal_kvar"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@Total_Kvar", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@Total_Kvar", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txttotal_kvar"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Observaciones
                    comando.Parameters.AddWithValue("@Observaciones", v_observaciones)
                    'Id incidencia eléctrica
                    comando.Parameters.AddWithValue("@idIncidenciaElectrica", utiles.BlancoANull(v_InitElec))
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtConsumoElec"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ConsumoElectricoAdicional", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ConsumoElectricoAdicional", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtConsumoElec"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Reinicio eléctrico
                    If utiles.nullABlanco(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtreinicioElec"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ReinicioLecturaElectrica", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ReinicioLecturaElectrica", Replace(Replace(CType(rptAlimentacionDetalle2.Items(i).FindControl("txtreinicioElec"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'justificación
                    comando.Parameters.AddWithValue("@Justificacion", "")

                    comando.Parameters.AddWithValue("@Funciona", v_Funciona)
                    comando.Parameters.AddWithValue("@Produccion", "N")


                    comando.ExecuteNonQuery()

                    'lo borraremos de la tabla [PVYCR_DatosAlimentacion_estadillo]
                    comando.CommandText = "delete from [PVYCR_DatosAlimentacion_estadillo] where idElementoMedida = '" & v_idElem & "' and CodigoPVYCR = '" & v_CodigoPVYCR & "' and  Cod_Fuente_Dato = '" & v_Cod_Fuente_Dato & "' and Fecha_Medida = '" & v_Fecha & "'"

                    comando.ExecuteNonQuery()

                    objTrans.Commit()
                Catch Exc As System.Data.SqlClient.SqlException
                    objTrans.Rollback()
                    'Response.Write("Error: " & Exc.Message & " num2: " & Exc.Number)
                    Select Case Exc.Number
                        Case 547
                            'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                            'Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                            mensaje = mensaje & "No existe un elemento de medida para el punto con fecha: " & v_Fecha & " \n"
                        Case 2627
                            'Alert(Page, "El dato Alimentacion ya existe para la fecha: " & v_Fecha)
                            mensaje = mensaje & "El dato Alimentacion ya existe para la fecha: " & v_Fecha & " \n"
                    End Select

                Catch Exc As Exception
                    JavaScript.Alert(Page, "Error: " & Exc.Message)
                End Try
                'deschekeamos el registro
                'CType(rptAlimentacionDetalle2.Items(i).FindControl("cbRechazarM"), CheckBox).Checked = False

            End If

        Next
        If mensaje <> "" Then
            JavaScript.Alert(Page, mensaje)
        End If
        crearDataSetMEstadillo(v_Cod_Fuente_Dato)
        dstAlimentacion.Tables("TablaAlimentacion").DefaultView.Sort = "fecha_medida "
        rptAlimentacionDetalle.DataSource = dstAlimentacion.Tables("TablaAlimentacion")

        rptAlimentacionDetalle.DataBind()

        rptAlimentacionDetalle2.DataSource = dstAlimentacion.Tables("TablaAlimentacion2")
        rptAlimentacionDetalle2.DataBind()

    End Sub

    Protected Sub btnCancelarLectura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarLectura.Click
        lblidAlimentacion.Text = ""
        pnlAlimentacion.Visible = True
        'ncm 25/08/2008 paginacion
        Panel1.Visible = False
        pnlBotones.Visible = False
        pnlEstadillo.Visible = False
        pnlEtiquetas.Visible = False
        pnlPrincipal.Visible = False
        'si quiero que refresque la pantalla principal, por si han aceptado todas las lecturas y no queda nada en estadillo, deberé
        'descomentar el response.redirect
        '     Response.Redirect("Alimentacion.aspx")

    End Sub
    Protected Sub rptAlimentacionDetalle2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptAlimentacionDetalle2.ItemCreated
        'Sólo se dispara una vez, cuando pasa por el repeater.
        ' solo funciona si los items son los del cuerpo, sino da error, por eso ponemos el IF
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            ' rellenamos las listas de incidencias eléctricas y las volumétricas
            rellenarListas(e)
            Formateo_controles_cliente(e)
        End If

    End Sub
    Protected Sub rellenarListas(ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        ddlE = CType(e.Item.FindControl("ddlIncidenciasE"), DropDownList)

        'Si la tabla no tiene valores la cargamos, si no ponemos el IF, se carga tantas veces como la fila que
        'estamos evaluando.
        'No hacemos el databind porque ya hacemos un databind en el repeat que hace el databind de todo
        'lo que tiene denntro.
        If IsNothing(dstAlimentacion.Tables("TablaIncidenciasE")) Then
            daAlimentacion.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'E' order by descripcion"
            daAlimentacion.Fill(dstAlimentacion, "TablaIncidenciasE")
        End If

        ddlE.DataSource = dstAlimentacion.Tables("TablaIncidenciasE")
        ddlE.DataValueField = "idIncidencia"
        ddlE.DataTextField = "Descripcion"

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Formateo_controles_cliente()
        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")

        imgCalendario.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtEDFechaMedida.ClientID & "'),'dd/mm/yyyy');")
        lblCabecera.Text = genHTML.cabecera(Page)
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            Session("Ordenacion") = ""
            '            lblCabecera.Text = genHTML.cabecera(Page)
            'ncm comentado al juntar las pestañas de preproduccion en una 22/10/2008
            'lblPestanyas.Text = genHTML.pestanyasMenu(2, "../", Page, Session("idperfil"))
            lblPestanyas.Text = genHTML.pestanyasMenu(0, "../", Page, Session("idperfil"), Session("usuarioReg"))



            If Request.QueryString("pag") <> "" Then
                lblPagina.Text = Request.QueryString("pag")
            Else
                lblPagina.Text = "1"
            End If

            sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosAlimentacion_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
            '"DM.Cod_Fuente_Dato = '05'"

            sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "

            crearDataSets()

            rptAlimentacion.DataSource = dstAlimentacion.Tables("TablaAlimentacion")
            rptAlimentacion.DataBind()
            'lblNumpaginas.DataBind()
        End If
    End Sub
    Private Sub lbtordenarPVYCR_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarPVYCR.Click
        txtFiltro.Text = ""
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosAlimentacion_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"

        If imgOrdPVYCRA.Visible = False And imgOrdPVYCRD.Visible = False Then
            imgOrdPVYCRD.Visible = True
        End If

        If imgOrdPVYCRA.Visible = True Then


            sentenciaOrder = sentenciaSel & " order by DM.CodigoPVYCR desc, DM.idElementoMedida, DM.Cod_Fuente_Dato Desc"
            'nos guardamos la select en una variable de sesion para que nos sirva para el avance página etc
            Session("Ordenacion") = sentenciaSel & " order by DM.CodigoPVYCR desc, DM.idElementoMedida, DM.Cod_Fuente_Dato Desc"
            'utilizamos una variable de sesion para saber si ha ordenado ascendentemente o descendentemente
            'Session("ordenarPVYCR") = "A"
            imgOrdPVYCRA.Visible = False
            imgOrdPVYCRD.Visible = True
            imgOrdNumRegA.Visible = False
            imgOrdNumRegD.Visible = False
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = False
        ElseIf imgOrdPVYCRD.Visible = True Then
            'Else
            sentenciaOrder = sentenciaSel & " order by DM.CodigoPVYCR ,DM.idElementoMedida, DM.Cod_Fuente_Dato "
            'nos guardamos la select en una variable de sesion para que nos sirva para el avance página etc
            Session("Ordenacion") = sentenciaSel & " order by DM.CodigoPVYCR , DM.idElementoMedida,DM.Cod_Fuente_Dato "
            'utilizamos una variable de sesion para saber si ha ordenado ascendentemente o descendentemente
            'Session("ordenarPVYCR") = ""
            imgOrdPVYCRA.Visible = True
            imgOrdPVYCRD.Visible = False
            imgOrdNumRegA.Visible = False
            imgOrdNumRegD.Visible = False
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = False
        End If

        crearDataSets()
        rptAlimentacion.DataSource = dstAlimentacion.Tables("TablaAlimentacion")
        rptAlimentacion.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtordenarCauce_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarCauce.Click
        txtFiltro.Text = ""
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosAlimentacion_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'" se comenta ncm 25/02/2009

        If imgOrdCauceA.Visible = False And imgOrdCauceD.Visible = False Then
            imgOrdCauceD.Visible = True
        End If
        If imgOrdCauceA.Visible = True Then
            sentenciaOrder = sentenciaSel & " order by C.DenominacionCauce Desc"
            Session("Ordenacion") = sentenciaSel & " order by C.DenominacionCauce Desc"
            'Session("ordenarCauce") = "A"
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = True
            imgOrdNumRegA.Visible = False
            imgOrdNumRegD.Visible = False
            imgOrdPVYCRA.Visible = False
            imgOrdPVYCRD.Visible = False
            'Else
        ElseIf imgOrdCauceD.Visible = True Then
            sentenciaOrder = sentenciaSel & " order by C.DenominacionCauce "
            Session("Ordenacion") = sentenciaSel & " order by C.DenominacionCauce "
            'Session("ordenarCauce") = ""
            imgOrdCauceA.Visible = True
            imgOrdCauceD.Visible = False
            imgOrdNumRegA.Visible = False
            imgOrdNumRegD.Visible = False
            imgOrdPVYCRA.Visible = False
            imgOrdPVYCRD.Visible = False
        End If
        crearDataSets()
        rptAlimentacion.DataSource = dstAlimentacion.Tables("TablaAlimentacion")
        rptAlimentacion.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtordenarNumReg_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarNumReg.Click
        txtFiltro.Text = ""
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce, count(*) NumRegEstdilloM " & _
                           "FROM PVYCR_DatosAlimentacion_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) and " & _
                         "group by DM.codigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_dato,P.DenominacionPunto,c.DenominacionCauce"
        'ncm se comenta el 25/02/2009 porque ya pueden introducir a mano de cualquier tipo
        '"DM.Cod_Fuente_Dato = '05'" & _

        If imgOrdNumRegA.Visible = False And imgOrdNumRegD.Visible = False Then
            imgOrdNumRegD.Visible = True
        End If

        If imgOrdNumRegA.Visible = True Then
            sentenciaOrder = sentenciaSel & " order by NumRegEstdilloM Desc"
            Session("Ordenacion") = sentenciaSel & " order by NumRegEstdilloM Desc"
            'Session("ordenarNumReg") = "A"
            imgOrdNumRegA.Visible = False
            imgOrdNumRegD.Visible = True
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = False
            imgOrdPVYCRA.Visible = False
            imgOrdPVYCRD.Visible = False
        ElseIf imgOrdNumRegD.Visible = True Then
            sentenciaOrder = sentenciaSel & " order by NumRegEstdilloM "
            Session("Ordenacion") = sentenciaSel & " order by NumRegEstdilloM "
            'Session("ordenarNumReg") = ""
            imgOrdNumRegA.Visible = True
            imgOrdNumRegD.Visible = False
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = False
            imgOrdPVYCRA.Visible = False
            imgOrdPVYCRD.Visible = False
        End If

        crearDataSets()
        rptAlimentacion.DataSource = dstAlimentacion.Tables("TablaAlimentacion")
        rptAlimentacion.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtFiltrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltrar.Click
        Dim sentenciaQuery As String

        lblPagina.Text = "1"
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosAlimentacion_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"
        sentenciaQuery = sentenciaSel
        If txtFiltro.Text <> "" Then
            sentenciaQuery &= " and DM.CodigoPVYCR like '%" & txtFiltro.Text & "%' "
        End If

        sentenciaOrder = sentenciaQuery & " order by DM.CodigoPVYCR, DM.idelementoMedida, DM.Cod_Fuente_Dato "


        crearDataSets()
        rptAlimentacion.DataSource = dstAlimentacion.Tables("TablaAlimentacion")
        rptAlimentacion.DataBind()
        'lblNumpaginas.DataBind()
        imgOrdPVYCRA.Visible = True
        imgOrdPVYCRD.Visible = False
        imgOrdNumRegA.Visible = False
        imgOrdNumRegD.Visible = False
        imgOrdCauceA.Visible = False
        imgOrdCauceD.Visible = False

    End Sub
    Private Sub lbtBorrarfiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtBorrarFiltro.Click

        txtFiltro.Text = ""
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosAlimentacion_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"

        sentenciaOrder = sentenciaSel & " order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "
        crearDataSets()
        rptAlimentacion.DataSource = dstAlimentacion.Tables("TablaAlimentacion")
        rptAlimentacion.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Protected Function obtenertotal_kwh(ByVal elDataitem As DataRowView) As String
        Dim total As Decimal
        If utiles.nullACero(elDataitem("Total_kwh")) = 0 Then
            total = utiles.nullACero(elDataitem("LecturaI")) + utiles.nullACero(elDataitem("LecturaII")) + utiles.nullACero(elDataitem("LecturaIII"))
            Return String.Format("{0:#,##0.##}", total)
        Else
            Return String.Format("{0:#,##0.##}", elDataitem("Total_kwh"))
        End If
        'DataBinder.Eval(container.dataitem, "Total_Kwh", "{0:#,###.##}")
    End Function
    Protected Sub Formateo_controles_cliente(ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        'DATOS ALLIMENTACION

        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtLecturaI"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtLecturaI"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtLecturaII"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtLecturaII"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtLecturaIII"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtLecturaIII"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtTotal_kwh"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtTotal_kwh"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtTotal_kvar"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtTotal_kvar"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtConsumoElec"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtConsumoElec"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtReinicioElec"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtReinicioElec"), TextBox))

    End Sub
    Protected Sub obtenerVolumenDiferencial()
        'vamos a calcular la diferencia de volúmenes según registros
        'el caso de las acequias es un caso especial y se calcula obteniendo la media del caudal por el tiempo trancurrido entre dos lecturas.
        Dim i As Integer
        Dim v_vol, v_vol_ant, v_diferencial, v_diferencial_acum, v_lectura_ant As Decimal

        If dstAlimentacion.Tables("TablaAlimentacion2").Rows.Count > 0 Then
            If Not dstAlimentacion.Tables("TablaAlimentacion2").Columns.Contains("Diferencial") Then
                'añadimos la columna diferencial al dataset
                dstAlimentacion.Tables("TablaAlimentacion2").Columns.Add("Diferencial")
                dstAlimentacion.Tables("TablaAlimentacion2").Columns.Add("Diferencial_acum")
            End If
            'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
            For i = 0 To dstAlimentacion.Tables("TablaAlimentacion2").Rows.Count - 1

                If i = 0 Then
                    ''rellenamos la columna diferencial del dataset con un 0
                    'dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Diferencial") = "0"
                    'dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Diferencial_acum") = "0"
                    'v_vol_ant = Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Total_Kwh").ToString)
                    'v_lectura_ant = v_vol_ant
                    'rellenamos la columna diferencial del dataset con el valor del último registro de motores, si no lo hubiera con un 0
                    Dim numAlimentacion As Integer
                    numAlimentacion = dstAlimentacion.Tables("TablaAlimentacion").Rows.Count
                    If numAlimentacion = 0  Then

                        v_vol_ant = 0
                        v_lectura_ant = v_vol_ant
                    ElseIf (utiles.nullABlanco(dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString) = "" And _
                        utiles.nullABlanco(dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("LecturaI").ToString) = "") Then
                        v_vol_ant = 0
                        v_lectura_ant = v_vol_ant

                    ElseIf utiles.nullABlanco(dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString) = "" And _
                                            utiles.nullABlanco(dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("LecturaI").ToString) <> "" Then

                        v_vol_ant = Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("LecturaI").ToString) + _
                                Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("LecturaII").ToString) + _
                                Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("LecturaIII").ToString)
                        v_lectura_ant = v_vol_ant
                    Else
                        v_vol_ant = Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString)
                        v_lectura_ant = v_vol_ant
                    End If
                End If 'Else
                'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
                If utiles.nullABlanco(dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Total_Kwh").ToString) = "" And _
                    utiles.nullABlanco(dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("LecturaI").ToString) = "" Then
                    v_diferencial = 0
                    v_diferencial_acum = v_diferencial_acum
                    'añadimos los valores a la tabla
                    dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", DBNull.Value)
                    dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)


                ElseIf utiles.nullABlanco(dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Total_Kwh").ToString) = "" And _
                    utiles.nullABlanco(dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("LecturaI").ToString) <> "" Then

                    v_vol = Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("LecturaI").ToString) + _
                            Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("LecturaII").ToString) + _
                            Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("LecturaIII").ToString)
                    'comprobamos si existen incidencias
                    'si la incidencia es reseteo o cambio de contador se deberá tomar el valor del campo Reinicio Lectura Volumetrica
                    'si la incidencia es contador averiado o consumo negativo, el valor a tomar será el consumo volumétrico adicional
                    If (dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("idIncidenciaElectrica").ToString = "2") Or _
                        (dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("idIncidenciaElectrica").ToString = "3") Then
                        v_diferencial = (v_vol - Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("ReiniciolecturaElectrica").ToString)) + _
                        Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("ConsumoElectricoAdicional").ToString)
                    ElseIf (dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("idIncidenciaElectrica").ToString = "1") Or _
                        (dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("idIncidenciaElectrica").ToString = "4") Then
                        If utiles.nullABlanco(dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("ConsumoElectricoAdicional").ToString) <> "" Then
                            v_diferencial = (v_vol + dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("ConsumoElectricoAdicional").ToString) - _
                            Convert.ToDecimal(0 & v_lectura_ant)
                        Else
                            v_diferencial = (v_vol + Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("ConsumoElectricoAdicional").ToString)) - _
                            Convert.ToDecimal(0 & v_lectura_ant)
                        End If
                    Else
                        v_diferencial = v_vol - v_vol_ant
                    End If

                    v_vol_ant = v_vol
                    'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
                    v_lectura_ant = v_vol
                    'calculamos el diferencial acumulado
                    v_diferencial_acum = v_diferencial_acum + v_diferencial
                    'cargamos datos en la tabla, comentado porque éste es el diferencial en KWH y vamos a mostrarlo en m3
                    'dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                    dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                    dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)

                Else


                    v_vol = Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Total_Kwh").ToString)
                    'comprobamos si existen incidencias
                    'si la incidencia es reseteo o cambio de contador se deberá tomar el valor del campo Reinicio Lectura Volumetrica
                    'si la incidencia es contador averiado o consumo negativo, el valor a tomar será el consumo volumétrico adicional
                    If (dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("idIncidenciaElectrica").ToString = "2") Or _
                        (dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("idIncidenciaElectrica").ToString = "3") Then
                        v_diferencial = ((Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Total_Kwh").ToString) - Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("ReiniciolecturaElectrica").ToString)) + _
                        Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("ConsumoElectricoAdicional").ToString))
                    ElseIf (dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("idIncidenciaElectrica").ToString = "1") Or _
                        (dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("idIncidenciaElectrica").ToString = "4") Then
                        If utiles.nullABlanco(dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("ConsumoElectricoAdicional").ToString) <> "" Then
                            v_diferencial = (Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Total_Kwh").ToString) + dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("ConsumoElectricoAdicional").ToString) - _
                            Convert.ToDecimal(0 & v_lectura_ant)
                        Else
                            v_diferencial = (Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Total_Kwh").ToString) + Convert.ToDecimal(0 & dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("ConsumoElectricoAdicional").ToString)) - _
                            Convert.ToDecimal(0 & v_lectura_ant)
                        End If
                    Else
                        v_diferencial = v_vol - v_vol_ant
                    End If

                    v_vol_ant = v_vol
                    'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
                    v_lectura_ant = dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Total_Kwh").ToString
                    'calculamos el diferencial acumulado
                    v_diferencial_acum = v_diferencial_acum + v_diferencial
                    'cargamos datos en la tabla, comentado porque éste es el diferencial en KWH y vamos a mostrarlo en m3
                    'dstAlimentacion.Tables("TablaAlimentacion").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                    dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                    dstAlimentacion.Tables("TablaAlimentacion2").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                End If
                'End If
            Next
        End If


    End Sub

    Protected Sub lbtNuevoElemento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtNuevoElemento.Click
        pnlAlimentacion.Visible = False
        Panel1.Visible = False
        'mostramos la cabecera
        pnlEstadillo.Visible = True
        'mostramos el panel de edición
        pnlNuevoElemento.Visible = True
        lblNombrePVYCR.Text = "INSERCIÓN MANUAL DE LECTURAS"
        'rellenamos los elementos dropdonwlist
        cargarlistas_edicion()

        
    End Sub

    Protected Sub btnEDCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEDCancelar.Click
        limpiar_campos_edicion()
        pnlAlimentacion.Visible = True
        Panel1.Visible = False
        pnlEstadillo.Visible = False
        pnlNuevoElemento.Visible = False

    End Sub
    Protected Sub cargarlistas_edicion()
        'lista de cauces
        Dim daCauces As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstCauces As DataSet = New System.Data.DataSet()

        utiles.Comprobar_Conexion_BD(Page, conexion)
        daCauces.SelectCommand.CommandText = "select distinct c.codigocauce from pvycr_cauces c, pvycr_puntos p, pvycr_elementosmedida e " & _
            "where c.codigocauce = p.codigocauce and  p.codigopvycr = e.codigopvycr and p.tipopunto = 'M' and e.tipo ='E' " & _
            "order by c.codigocauce"
        daCauces.Fill(dstCauces, "TablaCauces")
        ddlEDCauce.DataSource = dstCauces.Tables("TablaCauces")
        ddlEDCauce.DataValueField = "codigoCauce"
        ddlEDCauce.DataTextField = "codigocauce"
        ddlEDCauce.DataBind()
        ddlEDCauce.Items.Insert(0, New ListItem("[Seleccionar]", ""))

        'lista de incidencias eléctricas
        If IsNothing(dstAlimentacion.Tables("TablaIncidenciasE")) Then
            daAlimentacion.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'E' order by descripcion"
            daAlimentacion.Fill(dstAlimentacion, "TablaIncidenciasE")
        End If

        ddlEDIncidenciasE.DataSource = dstAlimentacion.Tables("TablaIncidenciasE")
        ddlEDIncidenciasE.DataValueField = "idIncidencia"
        ddlEDIncidenciasE.DataTextField = "Descripcion"

        ddlEDIncidenciasE.DataBind()
        ddlEDIncidenciasE.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        'Funciona
        ddlEDfunciona.Items.Insert(0, New ListItem("[seleccionar]", ""))
        ddlEDfunciona.Items.Insert(1, New ListItem("SI", "SI"))
        ddlEDfunciona.Items.Insert(2, New ListItem("NO", "NO"))
        'cod fuente datos
        Dim daCodFuenteDato As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstCodFuenteDato As DataSet = New System.Data.DataSet()

        utiles.Comprobar_Conexion_BD(Page, conexion)
        daCodFuenteDato.SelectCommand.CommandText = "select cod_fuente_dato,FUENTE_DATOS,Cod_fuente_dato+' - '+FUENTE_DATOS nombre FROM FUENTES_DE_DATOS  order by cod_fuente_dato"
        daCodFuenteDato.Fill(dstCodFuenteDato, "TablaCodFuenteDato")
        ddlEDCodFuenteDato.DataSource = dstCodFuenteDato.Tables("TablaCodFuenteDato")
        ddlEDCodFuenteDato.DataValueField = "cod_fuente_dato"
        ddlEDCodFuenteDato.DataTextField = "nombre"
        ddlEDCodFuenteDato.DataBind()
        ddlEDCodFuenteDato.Items.Insert(0, New ListItem("[Seleccionar]", ""))

    End Sub

    Protected Sub btnEDAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEDAceptar.Click
     
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim objTrans As SqlTransaction
        Try
            'If conexion.State = ConnectionState.Closed Then conexion.Open()
            utiles.Comprobar_Conexion_BD(Page, conexion)
            ''comprobamos si existe el elemento de medida, si no existe lo insertamos para que no falle la FK que hay entre datos alimentacion y elementos de medida
            'daAlimentacion.SelectCommand.CommandText = "select count(*) numero from pvycr_elementosmedida where codigopvycr= '" & ddlEDCodigoPVYCR.Text & "' and idelementomedida = '" & ddlEdidElemento.Text & "' "
            'daAlimentacion.Fill(dstAlimentacion, "TablaExisteElemento")

            'abrimos una transaccion para que si falla algunos de los inserts haga un rollback
            objTrans = conexion.BeginTransaction()
            comando.Transaction = objTrans
            comando.Parameters.AddWithValue("@idElementoMedida", ddlEdidElemento.Text)
            comando.Parameters.AddWithValue("@CodigoPVYCR", ddlEDCodigoPVYCR.Text)

            'If dstAlimentacion.Tables("TablaExisteElemento").Rows(0).Item("numero") = 0 Then
            '    comando.CommandText = "INSERT INTO PVYCR_ELEMENTOSMEDIDA (CODIGOPVYCR, idElementoMedida, TIPO) VALUES " & _
            '    "(@CodigoPVYCR,@idElementoMedida,'E')"
            '    comando.ExecuteNonQuery()
            'End If
         
            comando.CommandText = "INSERT INTO PVYCR_DatosAlimentacion_estadillo([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                ",[LecturaI],[LecturaII],[LecturaIII],[Total_Kwh],[Total_Kvar],[Observaciones],[idIncidenciaElectrica],[ConsumoElectricoAdicional] " & _
                ",[ReinicioLecturaelectrica],[Funciona],[justificacion],[login]) " & _
                "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_dato,@Fecha_Medida, " & _
                "@LecturaI,@LecturaII,@LecturaIII,@Total_KWH,@Total_Kvar,@Observaciones,@idIncidenciaelectrica,@ConsumoElectricoAdicional, " & _
                "@ReinicioLecturaElectrica, @Funciona,@justificacion,@login)"

     
            comando.Parameters.AddWithValue("@cod_fuente_dato", ddlEDCodFuenteDato.Text)
            comando.Parameters.AddWithValue("@Fecha_medida", txtEDFechaMedida.Text)

            'Lectura I
            If utiles.nullABlanco(txtEDLecturaI.Text) = "" Then
                comando.Parameters.AddWithValue("@LecturaI", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@LecturaI", txtEDLecturaI.Text.Replace(",", "."))
            End If
            'Lectura II
            If utiles.nullABlanco(txtEDLecturaII.Text) = "" Then
                comando.Parameters.AddWithValue("@LecturaII", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@LecturaII", txtEDLecturaII.Text.Replace(",", "."))
            End If
            'Lectura III
            If utiles.nullABlanco(txtEDLecturaIII.Text) = "" Then
                comando.Parameters.AddWithValue("@LecturaIII", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@LecturaIII", txtEDLecturaIII.Text.Replace(",", "."))
            End If
            'Total Kwh
            If utiles.nullABlanco(txtEDTotal_kwh.Text) = "" Then
                comando.Parameters.AddWithValue("@Total_KWH", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@Total_KWH", txtEDTotal_kwh.Text.Replace(",", "."))
            End If
            'Total(Kvar)
            If utiles.nullABlanco(txtEDTotal_kvar.Text) = "" Then
                comando.Parameters.AddWithValue("@Total_Kvar", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@Total_Kvar", txtEDTotal_kvar.Text.Replace(",", "."))
            End If
            'obserbvaciones
            comando.Parameters.AddWithValue("@Observaciones", txtEDObservaciones.Text)
            'Id incidencia eléctrica
            comando.Parameters.AddWithValue("@idIncidenciaElectrica", utiles.BlancoANull(ddlEDIncidenciasE.Text))
            'Consumo eléctrico
            If utiles.nullABlanco(txtEDConsumoElec.Text) = "" Then
                comando.Parameters.AddWithValue("@ConsumoElectricoAdicional", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@ConsumoElectricoAdicional", txtEDConsumoElec.Text.Replace(",", "."))
            End If
            'Reinico eléctrico
            If utiles.nullABlanco(txtEDReinicioElec.Text) = "" Then
                comando.Parameters.AddWithValue("@ReinicioLecturaElectrica", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@ReinicioLecturaElectrica", txtEDReinicioElec.Text.Replace(",", "."))
            End If
            'Funciona
            comando.Parameters.AddWithValue("@Funciona", ddlEDfunciona.SelectedValue)
            'Justificación
            comando.Parameters.AddWithValue("@Justificacion", "")
            comando.Parameters.AddWithValue("@login", txtEDUsuario.Text)

            comando.ExecuteNonQuery()
            objTrans.Commit()
        Catch Exc As System.Data.SqlClient.SqlException
            objTrans.Rollback()
            Select Case Exc.Number
                'Case 547
                '   Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    Alert(Page, "El dato Alimentacion ya existe para la fecha: " & txtEDFechaMedida.Text)
            End Select
        Catch Exc As Exception
            objTrans.Rollback()
            Alert(Page, "Error: " & Exc.Message)
        End Try
        limpiar_campos_edicion()
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                       "FROM PVYCR_DatosAlimentacion_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                       "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                        "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"

        sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "

        crearDataSets()

        rptAlimentacion.DataSource = dstAlimentacion.Tables("TablaAlimentacion")
        rptAlimentacion.DataBind()
        'lblNumpaginas.DataBind()

        'rptAlimentacionDetalle2.DataSource = dstAlimentacion.Tables("TablaAlimentacion2")
        'rptAlimentacionDetalle2.DataBind()
        pnlAlimentacion.Visible = True
        Panel1.Visible = False
        pnlEstadillo.Visible = False
        pnlNuevoElemento.Visible = False
    End Sub

    Protected Sub ddlEDCauce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEDCauce.SelectedIndexChanged
        Dim daPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstPuntos As DataSet = New System.Data.DataSet()
        'lista de puntos
        daPuntos.SelectCommand.CommandText = "select distinct p.codigopvycr from pvycr_cauces c, pvycr_puntos p, pvycr_elementosmedida e " & _
                                            "where c.codigocauce = p.codigocauce and p.tipopunto = 'M' and p.codigopvycr = e.codigopvycr and " & _
                                            "e.tipo = 'E' and c.codigocauce = '" & ddlEDCauce.Text & "' " & _
                                            "order by p.codigopvycr"
        daPuntos.Fill(dstPuntos, "TablaPuntos")
        ddlEDCodigoPVYCR.DataSource = dstPuntos.Tables("TablaPuntos")
        ddlEDCodigoPVYCR.DataValueField = "codigopvycr"
        ddlEDCodigoPVYCR.DataTextField = "codigopvycr"
        ddlEDCodigoPVYCR.DataBind()
        ddlEDCodigoPVYCR.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        ddlEdidElemento.Items.Clear()

    End Sub
    Protected Sub limpiar_campos_edicion()
        ddlEdidElemento.Items.Clear()
        ddlEDCauce.Items.Clear()
        ddlEDCodigoPVYCR.Items.Clear()
        ddlEDCodFuenteDato.Items.Clear()
        txtEDFechaMedida.Text = ""
        txtEDLecturaI.Text = ""
        txtEDLecturaII.Text = ""
        txtEDLecturaIII.Text = ""
        txtEDTotal_kwh.Text = ""
        txtEDTotal_kvar.Text = ""
        txtEDObservaciones.Text = ""
        ddlEDIncidenciasE.Items.Clear()
        txtEDConsumoElec.Text = ""
        txtEDReinicioElec.Text = ""
        ddlEDfunciona.Items.Clear()
        txtEDUsuario.Text = ""
    End Sub

    'Protected Sub lbtNuevoElemento2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtNuevoElemento2.Click
    '    pnlAlimentacion.Visible = False
    '    Panel1.Visible = False
    '    pnlEtiquetas.Visible = False
    '    pnlPrincipal.Visible = False
    '    pnlBotones.Visible = False
    '    'mostramos la cabecera
    '    pnlEstadillo.Visible = True
    '    'mostramos el panel de edición
    '    pnlNuevoElemento.Visible = True
    '    lblNombrePVYCR.Text = "NUEVO ELEMENTO DE MEDIDA"
    '    'rellenamos los elementos dropdonwlist
    '    cargarlistas_edicion()

    ' esta sería la columna que va en el aspx para que se vea nuevo contador ncm
    '<td style="background-color: #8CA4B5; padding: 2px; padding-right: 10px;
    '                            border-bottom: 1px solid #eeeeee; " align="right"><asp:LinkButton id="lbtNuevoElemento2" Runat="server" Font-Bold="true" ForeColor="black">Nuevo Elemento</asp:LinkButton></td>
    'End Sub

    Protected Sub ddlEDCodigoPVYCR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEDCodigoPVYCR.SelectedIndexChanged
        Dim daEDElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstEDElementos As DataSet = New System.Data.DataSet()
        'lista de puntos
        daEDElementos.SelectCommand.CommandText = "select e.idelementomedida from pvycr_puntos p, pvycr_elementosmedida e " & _
                                            "where p.codigoPVYCR = e.codigoPVYCR and p.tipopunto = 'M' and e.codigoPVYCR = '" & ddlEDCodigoPVYCR.Text & "' " & _
                                            "and e.tipo = 'E' order by e.idelementomedida"
        daEDElementos.Fill(dstEDElementos, "TablaEDElementos")
        ddlEdidElemento.DataSource = dstEDElementos.Tables("TablaEDElementos")
        ddlEdidElemento.DataValueField = "idelementomedida"
        ddlEdidElemento.DataTextField = "idelementomedida"
        ddlEdidElemento.DataBind()
        'ddlEdidElemento.Items.Insert(0, New ListItem("[Seleccionar]", ""))
    End Sub
 
End Class
