Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Partial Class SICAH_Horometros
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daHorometros As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstHorometros As DataSet = New System.Data.DataSet()

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

    Protected Function checkNombreHorometro(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Or (utiles.nullABlanco(elDataitem("DenominacionCauce")) = "") Then
            Return " "
        Else
            'Return elDataitem("CodigoPVYCR") & " / " & elDataitem("DenominacionPunto") & " / " & elDataitem("DenominacionCauce")
            Return elDataitem("DenominacionCauce")
        End If

    End Function
    Protected Function checkCodigoHorometro(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return " "
        Else
            lbl1.Text = elDataitem("idElementoMedida")
            Return elDataitem("CodigoPVYCR") & " / " & elDataitem("cod_fuente_dato") & " / " & elDataitem("DenominacionPunto") & " / " & elDataitem("idElementoMedida")
        End If

    End Function
    Protected Function checkNumRegEstadilloH(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return 0
        Else
            Dim v_result As String
            daHorometros.SelectCommand.CommandText = "SELECT count(*) NumRegEstdilloH FROM PVYCR_DatosHorometros_Estadillo E " & _
                                                  "where E.CodigoPVYCR = '" & elDataitem("CodigoPVYCR") & "' And E.Cod_Fuente_Dato = '" & elDataitem("Cod_Fuente_Dato") & "' " & _
                                                  " and E.idElementoMedida = '" & elDataitem("idElementoMedida") & "'"

            daHorometros.Fill(dstHorometros, "TablaNumRegEstadilloH")
            v_result = dstHorometros.Tables("TablaNumRegEstadilloH").Rows((dstHorometros.Tables("TablaNumRegEstadilloH").Rows.Count - 1)).Item("NumRegEstdilloH").ToString
            Return v_result
        End If

    End Function

    Public Sub calcularPags()
        Dim i As Integer
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim txtComando As String = ""
        txtComando = daHorometros.SelectCommand.CommandText
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
        daHorometros.SelectCommand.CommandText = sentenciaOrder
        daHorometros.Fill(dstHorometros, "TablaHorometros")
        'daHorometros.Fill(dstHorometros, (CInt(lblPagina.Text) - 1) * pageSize, pageSize, "TablaHorometros")
        'ncm comentado el 28/08/2008 petición del usuario
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
                       "FROM PVYCR_DatosHorometros_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
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
        rptHorometros.DataSource = dstHorometros.Tables("TablaHorometros")
        rptHorometros.DataBind()
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
                          "FROM PVYCR_DatosHorometros_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
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
        rptHorometros.DataSource = dstHorometros.Tables("TablaHorometros")
        rptHorometros.DataBind()
    End Sub
    Protected Sub PVYCRSeleccionada(ByVal sender As Object, ByVal e As System.EventArgs)
        'lblidHorometro.Text = CType(sender, LinkButton).CommandArgument
        Dim parametros() As String = Split(CType(sender, LinkButton).CommandArgument, "#")
        lblidHorometro.Text = parametros(0)

        pnlHorometros.Visible = False
        Panel1.Visible = False
        pnlEtiquetas.Visible = True
        pnlBotones.Visible = True
        pnlEstadillo.Visible = True
        pnlPrincipal.Visible = True
        Dim v_fuentedato As String = parametros(1)
        crearDataSetMEstadillo(v_fuentedato)

        lblNombrePVYCR.Text = lblidHorometro.Text

        dstHorometros.Tables("TablaHorometros").DefaultView.Sort = "fecha_medida "
        rptHorometrosDetalle.DataSource = dstHorometros.Tables("TablaHorometros").DefaultView
        rptHorometrosDetalle.DataBind()

        rptHorometrosDetalle2.DataSource = dstHorometros.Tables("TablaHorometros2")
        rptHorometrosDetalle2.DataBind()

        'If Not IsNothing(boton) The
        'boton.Visible = True
        'End If

    End Sub
    Private Sub crearDataSetMEstadillo(ByVal cod_fuente_dato As String)
        Dim vpos As Integer
        Dim vcadena1, vcadena2 As String
        utiles.Comprobar_Conexion_BD(Page, conexion)

        'obtenemos la posición de la primera / para saber hasta dónde tenemos que hacer el subtring para ontener el códigoPVYCR
        vpos = lblidHorometro.Text.IndexOf("/")
        vcadena1 = lblidHorometro.Text.Substring(0, vpos - 1)
        vcadena2 = lblidHorometro.Text.Substring(lblidHorometro.Text.Length - 3, 3)

        daHorometros.SelectCommand.CommandText = "SELECT top 10 D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_medida," & _
                                         "D.HorasIntervalo, D.idElementoMedida, " & _
                                          "D.Funciona, D.Observaciones, D.idIncidenciaVolumetrica, " & _
                                          "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                                          "IV.descripcion IncVol " & _
                                          "FROM PVYCR_DatosHorometros D " & _
                                          "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                          "where D.idElementoMedida = '" & vcadena2 & "' and D.CodigoPVYCR = '" & vcadena1 & "' And D.Cod_Fuente_Dato = '" & cod_fuente_dato & "' " & _
                                          "order by D.Fecha_medida desc"

        daHorometros.Fill(dstHorometros, "TablaHorometros")

        daHorometros.SelectCommand.CommandText = "SELECT E.CodigoPVYCR, E.Cod_Fuente_Dato, E.Fecha_medida, E.idElementoMedida, " & _
                                          "E.HorasIntervalo, E.Funciona, E.Observaciones, E.idIncidenciaVolumetrica, " & _
                                          "E.ConsumoVolumetricoAdicional, E.ReinicioLecturaVolumetrica, E.login " & _
                                          "FROM PVYCR_DatosHorometros_Estadillo E " & _
                                          "where E.idElementoMedida = '" & vcadena2 & "' and E.CodigoPVYCR = '" & vcadena1 & "' And E.Cod_Fuente_Dato = '" & cod_fuente_dato & "' " & _
                                          "order by E.Fecha_medida"
        'daHorometros.SelectCommand.CommandText = "SELECT E.CodigoPVYCR, E.Cod_Fuente_Dato, E.Fecha_medida, E.idElementoMedida, " & _
        '                                "E.HorasIntervalo, E.Funciona, E.Observaciones, E.idIncidenciaVolumetrica, " & _
        '                                "E.ConsumoVolumetricoAdicional, E.ReinicioLecturaVolumetrica " & _
        '                                "FROM PVYCR_DatosHorometros_Estadillo E " & _
        '                                "where E.idElementoMedida = '" & lbl1.Text & "' and E.CodigoPVYCR = '" & vcadena1 & "' And E.Cod_Fuente_Dato = '" & lblFuenteDato.Text & "' " & _
        '                                "order by E.Fecha_medida"
        daHorometros.Fill(dstHorometros, "TablaHorometros2")

    End Sub

    Protected Sub rptHorometrosDetalle2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptHorometrosDetalle2.ItemDataBound
        'el itemDataBound se carga cada vez por cada fila del repeater, éste if es para que sólo
        'evalue los items del cuerpo
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim i As Integer
            ddlF = CType(e.Item.FindControl("ddlFunciona"), DropDownList)
            ddlV = CType(e.Item.FindControl("ddlIncidenciasV"), DropDownList)
            'en el e.item.dataitem lo que tengo es la fila de la tabla con todos sus valores
            Select Case (utiles.nullABlanco(CType(e.Item.DataItem, DataRowView).Item("funciona"))).ToString.ToUpper()
                Case "SI"
                    ddlF.SelectedIndex = 0
                Case "NO"
                    ddlF.SelectedIndex = 1
                Case Else
                    ddlF.SelectedIndex = 2
            End Select

            For i = 0 To ddlV.Items.Count - 1
                If ddlV.Items(i).Value = utiles.nullABlanco(CType(e.Item.DataItem, DataRowView).Item("idIncidenciaVolumetrica")) Then
                    ddlV.Items(i).Selected = True
                Else
                    ddlV.Items(i).Selected = False
                End If

            Next
            ' ponemos este código aquí y no en el rellenar lista porque es aquí dónde se cargan las listas
            'y sino, nos recarga y nos quita el seleccionar
            ddlV.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        End If
    End Sub

    Protected Sub btnAceptarLectura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarLectura.Click

        Dim i As Integer
        Dim v_CodigoPVYCR, v_Cod_Fuente_Dato, v_Fecha_medida, v_Funciona, v_idElem As String
        Dim v_inciVol, v_observaciones As String
        Dim mensaje As String = ""
        For i = 0 To rptHorometrosDetalle2.Items.Count - 1
            'si marcan que la aceptan, insertaremos en TDatosMotores y borraremos del estadillo
            'código de aceptación de lectura para la lectura actual
            v_idElem = CType(rptHorometrosDetalle2.Items(i).FindControl("txtidElementoMedida"), TextBox).Text
            v_CodigoPVYCR = CType(rptHorometrosDetalle2.Items(i).FindControl("txtCodigoPVYCR"), TextBox).Text
            v_Cod_Fuente_Dato = CType(rptHorometrosDetalle2.Items(i).FindControl("txtCod_Fuente_Dato"), TextBox).Text
            v_Fecha_medida = CType(rptHorometrosDetalle2.Items(i).FindControl("txtFechaMedida"), TextBox).Text
            v_inciVol = CType(rptHorometrosDetalle2.Items(i).FindControl("ddlIncidenciasV"), DropDownList).SelectedValue
            v_Funciona = CType(rptHorometrosDetalle2.Items(i).FindControl("ddlFunciona"), DropDownList).SelectedItem().Text
            If IsNothing(CType(rptHorometrosDetalle2.Items(i).FindControl("txtObservaciones"), TextBox).Text) Then
                v_observaciones = ""
            Else
                v_observaciones = CType(rptHorometrosDetalle2.Items(i).FindControl("txtObservaciones"), TextBox).Text
            End If


            If CType(rptHorometrosDetalle2.Items(i).FindControl("cbAceptarM"), CheckBox).Checked Then

                Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
                Dim objTrans As SqlTransaction
                Try
                    utiles.Comprobar_Conexion_BD(Page, conexion)
                    'abrimos una transaccion para que si falla algunos de los inserts haga un rollback
                    objTrans = conexion.BeginTransaction()
                    comando.Transaction = objTrans
                    comando.CommandText = "INSERT INTO PVYCR_DatosHorometros([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_medida] " & _
                        ",[HorasIntervalo],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional] " & _
                        ",[ReinicioLecturaVolumetrica],[Funciona]) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_dato,@Fecha_Medida, " & _
                        "@HorasIntervalo,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                        "@ReinicioLecturaVolumetrica, @Funciona)"
                    comando.Parameters.AddWithValue("@idElementoMedida", v_idElem)
                    comando.Parameters.AddWithValue("@CodigoPVYCR", v_CodigoPVYCR)
                    comando.Parameters.AddWithValue("@Fecha_medida", v_Fecha_medida)
                    comando.Parameters.AddWithValue("@cod_fuente_dato", v_Cod_Fuente_Dato)

                    'Lectura contador M3
                    If utiles.nullABlanco(CType(rptHorometrosDetalle2.Items(i).FindControl("txtHorasIntervalo"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@HorasIntervalo", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@HorasIntervalo", Replace(Replace(CType(rptHorometrosDetalle2.Items(i).FindControl("txtHorasIntervalo"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'observaciones
                    comando.Parameters.AddWithValue("@Observaciones", v_observaciones)
                    'Id incidencia volumétrica
                    comando.Parameters.AddWithValue("@idIncidenciaVolumetrica", utiles.BlancoANull(v_inciVol))
                    'Consumo volumétrico
                    If utiles.nullABlanco(CType(rptHorometrosDetalle2.Items(i).FindControl("txtConsumoVol"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", Replace(Replace(CType(rptHorometrosDetalle2.Items(i).FindControl("txtConsumoVol"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Reinico lectura volum´´etrica
                    If utiles.nullABlanco(CType(rptHorometrosDetalle2.Items(i).FindControl("txtReiniciVol"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", Replace(Replace(CType(rptHorometrosDetalle2.Items(i).FindControl("txtReiniciVol"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Funciona
                    comando.Parameters.AddWithValue("@Funciona", v_Funciona)

                    comando.ExecuteNonQuery()

                    'insertamos en el histórico
                    comando.CommandText = "INSERT INTO [PVYCR_DatosHorometros_Estadillo_Histo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_medida] " & _
                        ",[HorasIntervalo],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional] " & _
                        ",[ReinicioLecturaVolumetrica],[Funciona],[Produccion],login) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_dato,@Fecha_medida, " & _
                        "@HorasIntervalo,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                        "@ReinicioLecturaVolumetrica, @Funciona, @produccion,@login) "
                    'comando.CommandText = "INSERT INTO [PVYCR_DatosHorometros_Estadillo_Histo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_medida] " & _
                    '                 ",[HorasIntervalo],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional] " & _
                    '                 ",[ReinicioLecturaVolumetrica],[Funciona],[Produccion]) " & _
                    '                 "values (@CodigoPVYCR,@idElementoMedida,'05',@Fecha_medida, " & _
                    '                 "@HorasIntervalo,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                    '                 "@ReinicioLecturaVolumetrica, @Funciona, @produccion) "

                    comando.Parameters.AddWithValue("@Produccion", "S")
                    comando.Parameters.AddWithValue("@login", CType(rptHorometrosDetalle2.Items(i).FindControl("txtlogin"), TextBox).Text)

                    comando.ExecuteNonQuery()
                    'lo borraremos de la tabla [PVYCR_DatosMotores_estadillo]

                    comando.CommandText = "delete from [PVYCR_DatosHorometros_estadillo] where idelementoMedida = '" & v_idElem & "' and CodigoPVYCR = '" & v_CodigoPVYCR & _
                    "' and  Cod_Fuente_Dato = '" & v_Cod_Fuente_Dato & "' and Fecha_medida = '" & v_Fecha_medida & "' "

                    comando.ExecuteNonQuery()
                    objTrans.Commit()
                Catch Exc As System.Data.SqlClient.SqlException
                    objTrans.Rollback()
                    Select Case Exc.Number
                        Case 547
                            'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                            'Alert(Page, "No existe un elemento de medida para el punto con fecha medida: " & v_Fecha_medida)
                            mensaje = mensaje & "No existe un elemento de medida para el punto con fecha medida: " & v_Fecha_medida & " \n"
                        Case 2627
                            'Alert(Page, "El dato motor ya existe para la fecha medida: " & v_Fecha_medida)
                            mensaje = mensaje & "El dato motor ya existe para la fecha medida: " & v_Fecha_medida & " \n"
                    End Select
                Catch Exc As Exception
                    objTrans.Rollback()
                    'Response.Write("Error: " & Exc.Message)
                    mensaje = mensaje & "Error: " & Exc.Message & " \n"
                End Try

            ElseIf CType(rptHorometrosDetalle2.Items(i).FindControl("cbRechazarM"), CheckBox).Checked Then
                'código de rechazo de lectura
                Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
                Dim objTrans As SqlTransaction
                Try
                    utiles.Comprobar_Conexion_BD(Page, conexion)
                    objTrans = conexion.BeginTransaction()
                    comando.Transaction = objTrans
                    'insertamos en el histórico
                    comando.CommandText = "INSERT INTO [PVYCR_DatosHorometros_Estadillo_Histo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_medida], " & _
                        "[HorasIntervalo],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional], " & _
                        "[ReinicioLecturaVolumetrica],[Funciona],[Produccion], login) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_dato,@Fecha_Medida, " & _
                        "@HorasIntervalo,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                        "@ReinicioLecturaVolumetrica, @Funciona, @produccion, @login) "
                    'comando.CommandText = "INSERT INTO [PVYCR_DatosHorometros_Estadillo_Histo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_medida], " & _
                    '                  "[HorasIntervalo],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional], " & _
                    '                  "[ReinicioLecturaVolumetrica],[Funciona],[Produccion]) " & _
                    '                  "values (@CodigoPVYCR,@idElementoMedida,'05',@Fecha_Medida, " & _
                    '                  "@HorasIntervalo,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                    '                  "@ReinicioLecturaVolumetrica, @Funciona, @produccion) "
                    comando.Parameters.AddWithValue("@idElementoMedida", v_idElem)
                    comando.Parameters.AddWithValue("@CodigoPVYCR", v_CodigoPVYCR)
                    comando.Parameters.AddWithValue("@Fecha_Medida", v_Fecha_medida)
                    comando.Parameters.AddWithValue("@cod_fuente_dato", v_Cod_Fuente_Dato)
                    comando.Parameters.AddWithValue("@login", CType(rptHorometrosDetalle2.Items(i).FindControl("txtlogin"), TextBox).Text)


                    'Horas Intervalo
                    If utiles.nullABlanco(CType(rptHorometrosDetalle2.Items(i).FindControl("txtHorasIntervalo"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@HorasIntervalo", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@HorasIntervalo", Replace(Replace(CType(rptHorometrosDetalle2.Items(i).FindControl("txtHorasIntervalo"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Observaciones
                    comando.Parameters.AddWithValue("@Observaciones", v_observaciones)
                    'Incidencia volumétrica
                    comando.Parameters.AddWithValue("@idIncidenciaVolumetrica", v_inciVol)
                    'Consumo volumétrico
                    If utiles.nullABlanco(CType(rptHorometrosDetalle2.Items(i).FindControl("txtConsumoVol"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", Replace(Replace(CType(rptHorometrosDetalle2.Items(i).FindControl("txtConsumoVol"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Rinicio volumétrico
                    If utiles.nullABlanco(CType(rptHorometrosDetalle2.Items(i).FindControl("txtReiniciVol"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", Replace(Replace(CType(rptHorometrosDetalle2.Items(i).FindControl("txtReiniciVol"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    comando.Parameters.AddWithValue("@Funciona", v_Funciona)
                    comando.Parameters.AddWithValue("@Produccion", "N")


                    comando.ExecuteNonQuery()

                    'lo borraremos de la tabla [PVYCR_DatosHorometros_estadillo]
                    comando.CommandText = "delete from [PVYCR_DatosHorometros_estadillo] where idelementoMedida = '" & v_idElem & "' and CodigoPVYCR = '" & v_CodigoPVYCR & _
                    "' and  Cod_Fuente_Dato = '" & v_Cod_Fuente_Dato & "' and Fecha_Medida = '" & v_Fecha_medida & "' "


                    comando.ExecuteNonQuery()

                    objTrans.Commit()
                Catch Exc As System.Data.SqlClient.SqlException
                    objTrans.Rollback()
                    'Response.Write("Error: " & Exc.Message & " num2: " & Exc.Number)
                    Select Case Exc.Number
                        Case 547
                            'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                            'Alert(Page, "No existe un elemento de medida para el punto con fecha medida: " & v_Fecha_medida)
                            mensaje = mensaje & "No existe un elemento de medida para el punto con fecha medida: " & v_Fecha_medida & " \n"
                        Case 2627
                            'Alert(Page, "El dato motor ya existe para la fecha medida: " & v_Fecha_medida)
                            mensaje = mensaje & "El dato motor ya existe para la fecha medida: " & v_Fecha_medida & " \n"
                    End Select

                Catch Exc As Exception
                    'Response.Write("Error: " & Exc.Message)
                    mensaje = mensaje & "Error " & Exc.Message & " \n"
                End Try

            End If

        Next
        If mensaje <> "" Then
            Alert(Page, mensaje)
        End If
        crearDataSetMEstadillo(v_Cod_Fuente_Dato)
        dstHorometros.Tables("TablaHorometros").DefaultView.Sort = "fecha_medida "
        rptHorometrosDetalle.DataSource = dstHorometros.Tables("TablaHorometros")

        rptHorometrosDetalle.DataBind()

        rptHorometrosDetalle2.DataSource = dstHorometros.Tables("TablaHorometros2")
        rptHorometrosDetalle2.DataBind()

    End Sub

    Protected Sub btnCancelarLectura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarLectura.Click
        lblidHorometro.Text = ""
        pnlHorometros.Visible = True
        'ncm 25/08/2008 paginacion
        Panel1.Visible = False
        pnlBotones.Visible = False
        pnlEstadillo.Visible = False
        pnlEtiquetas.Visible = False
        pnlPrincipal.Visible = False
        'si quiero que refresque la pantalla principal, por si han aceptado todas las lecturas y no queda nada en estadillo, deberé
        'descomentar el response.redirect
        '     Response.Redirect("motores.aspx")

    End Sub
    Protected Sub rptHorometrosDetalle2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptHorometrosDetalle2.ItemCreated
        'Sólo se dispara una vez, cuando pasa por el repeater.
        ' solo funciona si los items son los del cuerpo, sino da error, por eso ponemos el IF
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            ' rellenamos las listas de incidencias eléctricas y las volumétricas
            rellenarListas(e)
            Formateo_controles_cliente(e)
        End If

    End Sub
    Protected Sub rellenarListas(ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)

        ddlV = CType(e.Item.FindControl("ddlIncidenciasV"), DropDownList)
        'Si la tabla no tiene valores la cargamos, si no ponemos el IF, se carga tantas veces como la fila que
        'estamos evaluando.
        'No hacemos el databind porque ya hacemos un databind en el repeat que hace el databind de todo
        'lo que tiene denntro.
        'If IsNothing(dstHorometros.Tables("TablaIncidenciasE")) Then
        '  daHorometros.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'E' order by descripcion"
        '  daHorometros.Fill(dstHorometros, "TablaIncidenciasE")
        'End If


        If IsNothing(dstHorometros.Tables("TablaIncidenciasV")) Then
            daHorometros.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'H' order by descripcion"
            daHorometros.Fill(dstHorometros, "TablaIncidenciasV")
        End If
        ddlV.DataSource = dstHorometros.Tables("TablaIncidenciasV")
        ddlV.DataValueField = "idIncidencia"
        ddlV.DataTextField = "Descripcion"

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")

        imgCalendario.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtEDFechaMedida.ClientID & "'),'dd/mm/yyyy');")
        lblCabecera.Text = genHTML.cabecera(Page)
        If Not IsPostBack Then
            
            Session("Ordenacion") = ""
            'ncm 23/10/2008 comentado al agrupar las pestañas de preproducción en una sola
            'lblPestanyas.Text = genHTML.pestanyasMenu(1, "../", Page, Session("idperfil"))
            lblPestanyas.Text = genHTML.pestanyasMenu(0, "../", Page, Session("idperfil"), Session("usuarioReg"))

            If Request.QueryString("pag") <> "" Then
                lblPagina.Text = Request.QueryString("pag")
            Else
                lblPagina.Text = "1"
            End If

            sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosHorometros_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
            '"DM.Cod_Fuente_Dato = '05'"

            sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "

            crearDataSets()

            rptHorometros.DataSource = dstHorometros.Tables("TablaHorometros")
            rptHorometros.DataBind()
            'lblNumpaginas.DataBind()
        End If
    End Sub
    Private Sub lbtordenarPVYCR_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarPVYCR.Click
        txtFiltro.Text = ""
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosHorometros_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
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
        rptHorometros.DataSource = dstHorometros.Tables("TablaHorometros")
        rptHorometros.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtordenarCauce_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarCauce.Click
        txtFiltro.Text = ""
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosHorometros_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"

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
        rptHorometros.DataSource = dstHorometros.Tables("TablaHorometros")
        rptHorometros.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtordenarNumReg_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarNumReg.Click
        txtFiltro.Text = ""
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce, count(*) NumRegEstdilloH " & _
                           "FROM PVYCR_DatosHorometros_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) and " & _
                         "group by DM.codigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_dato,P.DenominacionPunto,c.DenominacionCauce"
        '"DM.Cod_Fuente_Dato = '05'" & _

        If imgOrdNumRegA.Visible = False And imgOrdNumRegD.Visible = False Then
            imgOrdNumRegD.Visible = True
        End If

        If imgOrdNumRegA.Visible = True Then
            sentenciaOrder = sentenciaSel & " order by NumRegEstdilloH Desc"
            Session("Ordenacion") = sentenciaSel & " order by NumRegEstdilloM Desc"
            'Session("ordenarNumReg") = "A"
            imgOrdNumRegA.Visible = False
            imgOrdNumRegD.Visible = True
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = False
            imgOrdPVYCRA.Visible = False
            imgOrdPVYCRD.Visible = False
        ElseIf imgOrdNumRegD.Visible = True Then
            sentenciaOrder = sentenciaSel & " order by NumRegEstdilloH "
            Session("Ordenacion") = sentenciaSel & " order by NumRegEstdilloH "
            'Session("ordenarNumReg") = ""
            imgOrdNumRegA.Visible = True
            imgOrdNumRegD.Visible = False
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = False
            imgOrdPVYCRA.Visible = False
            imgOrdPVYCRD.Visible = False
        End If

        crearDataSets()
        rptHorometros.DataSource = dstHorometros.Tables("TablaHorometros")
        rptHorometros.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtFiltrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltrar.Click
        Dim sentenciaQuery As String
        lblPagina.Text = "1"
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosHorometros_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"
        sentenciaQuery = sentenciaSel
        If txtFiltro.Text <> "" Then
            sentenciaQuery &= " and DM.CodigoPVYCR like '%" & txtFiltro.Text & "%' "
        End If

        sentenciaOrder = sentenciaQuery & " order by DM.CodigoPVYCR, DM.idelementoMedida, DM.Cod_Fuente_Dato "


        crearDataSets()
        rptHorometros.DataSource = dstHorometros.Tables("TablaHorometros")
        rptHorometros.DataBind()
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
                           "FROM PVYCR_DatosHorometros_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"

        sentenciaOrder = sentenciaSel & " order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "
        crearDataSets()
        rptHorometros.DataSource = dstHorometros.Tables("TablaHorometros")
        rptHorometros.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Protected Sub Formateo_controles_cliente(ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)

        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtConsumoVol"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtConsumoVol"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtReiniciVol"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtReiniciVol"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtHorasIntervalo"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtHorasIntervalo"), TextBox))
    End Sub
 
    Protected Sub lbtNuevoElemento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtNuevoElemento.Click
        pnlHorometros.Visible = False
        Panel1.Visible = False
        'mostramos la cabecera
        pnlEstadillo.Visible = True
        'mostramos el panel de edición
        pnlNuevoElemento.Visible = True
        lblNombrePVYCR.Text = "INSERCIÓN MANUAL DE LECTURAS"
        'rellenamos los elementos dropdonwlist
        cargarlistas_edicion()
    End Sub
    Protected Sub cargarlistas_edicion()
        'lista de cauces
        Dim daCauces As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstCauces As DataSet = New System.Data.DataSet()

        utiles.Comprobar_Conexion_BD(Page, conexion)
        daCauces.SelectCommand.CommandText = "select distinct c.codigocauce from pvycr_cauces c, pvycr_puntos p, pvycr_elementosmedida e " & _
            "where c.codigocauce = p.codigocauce and  p.codigopvycr = e.codigopvycr and p.tipopunto = 'M' and e.tipo ='H' " & _
            "order by c.codigocauce"
        daCauces.Fill(dstCauces, "TablaCauces")
        ddlEDCauce.DataSource = dstCauces.Tables("TablaCauces")
        ddlEDCauce.DataValueField = "codigoCauce"
        ddlEDCauce.DataTextField = "codigocauce"
        ddlEDCauce.DataBind()
        ddlEDCauce.Items.Insert(0, New ListItem("[Seleccionar]", ""))

        'lista de incidencias eléctricas
        If IsNothing(dstHorometros.Tables("TablaIncidenciasV")) Then
            daHorometros.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'H' order by descripcion"
            daHorometros.Fill(dstHorometros, "TablaIncidenciasV")
        End If
        ddlEDIncidenciasV.DataSource = dstHorometros.Tables("TablaIncidenciasV")
        ddlEDIncidenciasV.DataValueField = "idIncidencia"
        ddlEDIncidenciasV.DataTextField = "Descripcion"

        ddlEDIncidenciasV.DataBind()
        ddlEDIncidenciasV.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        'Funciona
        ddlEDfunciona.Items.Insert(0, New ListItem("[seleccionar]", ""))
        ddlEDfunciona.Items.Insert(1, New ListItem("SI", "SI"))
        ddlEDfunciona.Items.Insert(2, New ListItem("NO", "NO"))
        'cod fuente datos
        Dim daCodFuenteDato As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstCodFuenteDato As DataSet = New System.Data.DataSet()

        utiles.Comprobar_Conexion_BD(Page, conexion)
        daCodFuenteDato.SelectCommand.CommandText = "select cod_fuente_dato, FUENTE_DATOS,Cod_fuente_dato+' - '+FUENTE_DATOS nombre FROM FUENTES_DE_DATOS order by cod_fuente_dato"
        daCodFuenteDato.Fill(dstCodFuenteDato, "TablaCodFuenteDato")
        ddlEDCodFuenteDato.DataSource = dstCodFuenteDato.Tables("TablaCodFuenteDato")
        ddlEDCodFuenteDato.DataValueField = "cod_fuente_dato"
        ddlEDCodFuenteDato.DataTextField = "nombre"
        ddlEDCodFuenteDato.DataBind()
        ddlEDCodFuenteDato.Items.Insert(0, New ListItem("[Seleccionar]", ""))

    End Sub

    Protected Sub btnEDCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEDCancelar.Click
        limpiar_campos_edicion()
        pnlHorometros.Visible = True
        Panel1.Visible = False
        pnlEstadillo.Visible = False
        pnlNuevoElemento.Visible = False
    End Sub
    Protected Sub limpiar_campos_edicion()
        ddlEDidElemento.Items.Clear()
        ddlEDCauce.Items.Clear()
        ddlEDCodigoPVYCR.Items.Clear()
        ddlEDCodFuenteDato.Items.Clear()
        txtEDFechaMedida.Text = ""
        txtEDHorasIntervalo.Text = ""
        txtEDObservaciones.Text = ""
        ddlEDIncidenciasV.Items.Clear()
        txtEDConsumoVol.Text = ""
        txtEDReinicioVol.Text = ""
        ddlEDfunciona.Items.Clear()
        txtEDUsuario.Text = ""
    End Sub

    Protected Sub btnEDAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEDAceptar.Click
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim objTrans As SqlTransaction
        Try
            utiles.Comprobar_Conexion_BD(Page, conexion)
            ''comprobamos si existe el elemento de medida, si no existe lo insertamos para que no falle la FK que hay entre datos alimentacion y elementos de medida
            'daHorometros.SelectCommand.CommandText = "select count(*) numero from pvycr_elementosmedida where codigopvycr= '" & ddlEDCodigoPVYCR.Text & "' and idelementomedida = '" & ddlEDidElemento.Text & "' "
            'daHorometros.Fill(dstHorometros, "TablaExisteElemento")

            comando.Parameters.AddWithValue("@idElementoMedida", ddlEDidElemento.Text)
            comando.Parameters.AddWithValue("@CodigoPVYCR", ddlEDCodigoPVYCR.Text)
            comando.Parameters.AddWithValue("@codfuentedato", ddlEDCodFuenteDato.Text)
            'If dstHorometros.Tables("TablaExisteElemento").Rows(0).Item("numero") = 0 Then
            '    comando.CommandText = "INSERT INTO PVYCR_ELEMENTOSMEDIDA (CODIGOPVYCR, idElementoMedida, TIPO) VALUES " & _
            '    "(@CodigoPVYCR,@idElementoMedida,'H')"
            '    comando.ExecuteNonQuery()
            'End If
            'abrimos una transaccion para que si falla algunos de los inserts haga un rollback
            objTrans = conexion.BeginTransaction()
            comando.Transaction = objTrans
            comando.CommandText = "INSERT INTO PVYCR_DatosHorometros_estadillo([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_medida] " & _
                ",[HorasIntervalo],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional] " & _
                ",[ReinicioLecturaVolumetrica],[Funciona],login) " & _
                "values (@CodigoPVYCR,@idElementoMedida,@codfuentedato,@Fecha_Medida, " & _
                "@HorasIntervalo,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                "@ReinicioLecturaVolumetrica, @Funciona,@login)"

            comando.Parameters.AddWithValue("@Fecha_medida", txtEDFechaMedida.Text)

            'Lectura contador M3
            If utiles.nullABlanco(txtEDHorasIntervalo.Text) = "" Then
                comando.Parameters.AddWithValue("@HorasIntervalo", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@HorasIntervalo", txtEDHorasIntervalo.Text.Replace(",", "."))
            End If
            'observaciones
            comando.Parameters.AddWithValue("@Observaciones", txtEDObservaciones.Text)
            'Id incidencia volumétrica
            comando.Parameters.AddWithValue("@idIncidenciaVolumetrica", utiles.BlancoANull(ddlEDIncidenciasV.Text))
            'Consumo volumétrico
            If utiles.nullABlanco(txtEDConsumoVol.Text) = "" Then
                comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", txtEDConsumoVol.Text.Replace(",", "."))
            End If
            'Reinico lectura volum´´etrica
            If utiles.nullABlanco(txtEDReinicioVol.Text) = "" Then
                comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", txtEDReinicioVol.Text.Replace(",", "."))
            End If
            'Funciona
            comando.Parameters.AddWithValue("@Funciona", ddlEDfunciona.SelectedValue)
            comando.Parameters.AddWithValue("@login", txtEDUsuario.Text)

            comando.ExecuteNonQuery()

            objTrans.Commit()
        Catch Exc As System.Data.SqlClient.SqlException
            objTrans.Rollback()
            Select Case Exc.Number
                Case 547
                    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                    Alert(Page, "No existe un elemento de medida para el punto con fecha medida: " & txtEDFechaMedida.Text)
                Case 2627
                    Alert(Page, "El dato motor ya existe para la fecha medida: " & txtEDFechaMedida.Text)
            End Select
        Catch Exc As Exception
            objTrans.Rollback()
            Alert(Page, "Error: " & Exc.Message)
        End Try

        limpiar_campos_edicion()
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                          "FROM PVYCR_DatosHorometros_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                          "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                           "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"

        sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "

        crearDataSets()

        rptHorometros.DataSource = dstHorometros.Tables("TablaHorometros")
        rptHorometros.DataBind()
        'lblNumpaginas.DataBind()
        pnlHorometros.Visible = True
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
                                            "e.tipo = 'H' and c.codigocauce = '" & ddlEDCauce.Text & "' " & _
                                            "order by p.codigopvycr"
        daPuntos.Fill(dstPuntos, "TablaPuntos")
        ddlEDCodigoPVYCR.DataSource = dstPuntos.Tables("TablaPuntos")
        ddlEDCodigoPVYCR.DataValueField = "codigopvycr"
        ddlEDCodigoPVYCR.DataTextField = "codigopvycr"
        ddlEDCodigoPVYCR.DataBind()
        ddlEDCodigoPVYCR.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        ddlEDidElemento.Items.Clear()

    End Sub

    Protected Sub ddlEDCodigoPVYCR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEDCodigoPVYCR.SelectedIndexChanged
        Dim daEDElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstEDElementos As DataSet = New System.Data.DataSet()
        'lista de puntos
        daEDElementos.SelectCommand.CommandText = "select e.idelementomedida from pvycr_puntos p, pvycr_elementosmedida e " & _
                                            "where p.codigoPVYCR = e.codigoPVYCR and p.tipopunto = 'M' and e.codigoPVYCR = '" & ddlEDCodigoPVYCR.Text & "' " & _
                                            "and e.tipo = 'H' order by e.idelementomedida"
        daEDElementos.Fill(dstEDElementos, "TablaEDElementos")
        ddlEDidElemento.DataSource = dstEDElementos.Tables("TablaEDElementos")
        ddlEDidElemento.DataValueField = "idelementomedida"
        ddlEDidElemento.DataTextField = "idelementomedida"
        ddlEDidElemento.DataBind()
    End Sub
End Class
