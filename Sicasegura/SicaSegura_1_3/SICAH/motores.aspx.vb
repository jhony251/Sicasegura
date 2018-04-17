Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript

Partial Class SICAH_motores
  Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
  Dim daMotores As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
  Dim dstMotores As DataSet = New System.Data.DataSet()

  Public numpaginas As Integer
  Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
  Dim sentenciaSel, sentenciaOrder As String
  Dim parfila As Integer = 0
  Dim ddlF As DropDownList
  Dim ddlE As DropDownList
    Dim ddlV As DropDownList
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim objTrans As SqlTransaction
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
  Protected Function checkNombreMotor(ByVal elDataitem As DataRowView) As String
        If (utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "") Or (utiles.nullABlanco(elDataitem("DenominacionCauce")) = "") Then
            Return " "
        Else
            'Return elDataitem("CodigoPVYCR") & " / " & elDataitem("DenominacionPunto") & " / " & elDataitem("DenominacionCauce")
            Return elDataitem("DenominacionCauce")
        End If

    End Function
    Protected Function checkCodigoMotor(ByVal elDataitem As DataRowView) As String
        If Utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return " "
        Else
            lbl1.Text = elDataitem("idElementoMedida")
            Return elDataitem("CodigoPVYCR") & " / " & elDataitem("cod_fuente_dato") & " / " & elDataitem("DenominacionPunto") & " / " & elDataitem("idElementoMedida")
        End If

    End Function
    Protected Function checkNumRegEstadilloM(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return 0
        Else
            Dim v_result As String
            daMotores.SelectCommand.CommandText = "SELECT count(*) NumRegEstdilloM FROM PVYCR_DatosMotores_Estadillo E " & _
                                                  "where E.CodigoPVYCR = '" & elDataitem("CodigoPVYCR") & "' And E.Cod_Fuente_Dato = '" & elDataitem("Cod_Fuente_Dato") & "' " & _
                                                  " and E.idElementoMedida = '" & elDataitem("idElementoMedida") & "'"

            daMotores.Fill(dstMotores, "TablaNumRegEstadilloM")
            v_result = dstMotores.Tables("TablaNumRegEstadilloM").Rows((dstMotores.Tables("TablaNumRegEstadilloM").Rows.Count - 1)).Item("NumRegEstdilloM").ToString
            Return v_result
        End If

    End Function

  Public Sub calcularPags()
    Dim i As Integer
    Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
    Dim txtComando As String = ""
    txtComando = daMotores.SelectCommand.CommandText
    Try
      txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
    Catch Exc As Exception
      Response.Write("Error: " & Exc.Message)
    End Try


    comando.CommandText = "select count(*) from (" & txtComando & ") dtable"
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
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
        daMotores.SelectCommand.CommandText = sentenciaOrder
        daMotores.Fill(dstMotores, "TablaMotores")
        'daMotores.Fill(dstMotores, (CInt(lblPagina.Text) - 1) * pageSize, pageSize, "TablaMotores")
        'ncm comentado el 25/08/2008 por peticion usuario
        ''Cálculo del número de páginas
        'calcularPags()

        'If CInt(lblPagina.Text) < numpaginas Then
        '  lbtNext.Visible = True
        'Else
        '  lbtNext.Visible = False
        'End If
        'If CInt(lblPagina.Text) > 1 Then
        '  lbtPrev.Visible = True
        'Else
        '  lbtPrev.Visible = False
        'End If
  End Sub
  Private Sub ddlPaginacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPaginacion.SelectedIndexChanged
        lblPagina.Text = ddlPaginacion.Items(ddlPaginacion.SelectedIndex).Value
        If Session("ordenacion").ToString = "" Then


            sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                       "FROM PVYCR_DatosMotores_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
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
        rptMotores.DataSource = dstMotores.Tables("TablaMotores")
        rptMotores.DataBind()
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
                          "FROM PVYCR_DatosMotores_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
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
        rptMotores.DataSource = dstMotores.Tables("TablaMotores")
        rptMotores.DataBind()
  End Sub
  Protected Sub PVYCRSeleccionada(ByVal sender As Object, ByVal e As System.EventArgs)
        'lblidMotor.Text = CType(sender, LinkButton).CommandArgument
        Dim parametros() As String = Split(CType(sender, LinkButton).CommandArgument, "#")
        lblidMotor.Text = parametros(0)

    pnlMotores.Visible = False
    Panel1.Visible = False
    pnlEtiquetas.Visible = True
    pnlBotones.Visible = True
    pnlEstadillo.Visible = True
    pnlPrincipal.Visible = True
        Dim v_fuentedato As String = parametros(1)
        crearDataSetMEstadillo(v_fuentedato)

    lblNombrePVYCR.Text = lblidMotor.Text

        dstMotores.Tables("TablaMotores").DefaultView.Sort = "fecha_medida "
    rptMotoresDetalle.DataSource = dstMotores.Tables("TablaMotores").DefaultView
    rptMotoresDetalle.DataBind()

    rptMotoresDetalle2.DataSource = dstMotores.Tables("TablaMotores2")
    rptMotoresDetalle2.DataBind()

    'If Not IsNothing(boton) The
    'boton.Visible = True
    'End If

  End Sub
    Private Sub crearDataSetMEstadillo(ByVal cod_fuente_datos As String)
        Dim vpos As Integer
        Dim vcadena1, vcadena2 As String
        'obtenemos la posición de la primera / para saber hasta dónde tenemos que hacer el subtring para ontener el códigoPVYCR
        vpos = lblidMotor.Text.IndexOf("/")
        vcadena1 = lblidMotor.Text.Substring(0, vpos - 1)
        vcadena2 = lblidMotor.Text.Substring(lblidMotor.Text.Length - 3, 3)


        daMotores.SelectCommand.CommandText = "SELECT top 10 D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                          "D.LecturaContador_M3, D.Funciona, D.Observaciones, D.idIncidenciaVolumetrica, " & _
                                          "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, D.CaudalMedido, " & _
                                          "IV.descripcion IncVol " & _
                                          "FROM PVYCR_DatosMotores D " & _
                                          "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                          "where D.idElementoMedida = '" & vcadena2 & "' and D.CodigoPVYCR = '" & vcadena1 & "' And D.Cod_Fuente_Dato = '" & cod_fuente_datos & "' " & _
                                          "order by D.Fecha_Medida desc"

        daMotores.Fill(dstMotores, "TablaMotores")

        daMotores.SelectCommand.CommandText = "SELECT E.CodigoPVYCR, E.Cod_Fuente_Dato, E.Fecha_Medida, E.idElementoMedida, " & _
                                          "E.LecturaContador_M3, E.Funciona, E.Observaciones, E.idIncidenciaVolumetrica, " & _
                                          "E.ConsumoVolumetricoAdicional, E.ReinicioLecturaVolumetrica,E.login, E.CaudalMedido " & _
                                          "FROM PVYCR_DatosMotores_Estadillo E " & _
                                          "where E.idElementoMedida = '" & vcadena2 & "' and E.CodigoPVYCR = '" & vcadena1 & "' And E.Cod_Fuente_Dato = '" & cod_fuente_datos & "' " & _
                                          "order by E.Fecha_Medida"

        'daMotores.SelectCommand.CommandText = "SELECT E.CodigoPVYCR, E.Cod_Fuente_Dato, E.Fecha_Medida, E.idElementoMedida, " & _
        '                                       "E.LecturaContador_M3, E.Funciona, E.Observaciones, E.idIncidenciaVolumetrica, " & _
        '                                       "E.ConsumoVolumetricoAdicional, E.ReinicioLecturaVolumetrica " & _
        '                                       "FROM PVYCR_DatosMotores_Estadillo E " & _
        '                                       "where E.idElementoMedida = '" & lbl1.Text & "' and E.CodigoPVYCR = '" & vcadena1 & "' And E.Cod_Fuente_Dato = '" & lblFuenteDato.Text & "' " & _
        '                                       "order by E.Fecha_Medida"
        daMotores.Fill(dstMotores, "TablaMotores2")
        obtenerVolumenDiferencial()

    End Sub

  Protected Sub rptMotoresDetalle2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptMotoresDetalle2.ItemDataBound
    'el itemDataBound se carga cada vez por cada fila del repeater, éste if es para que sólo
    'evalue los items del cuerpo
    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
      Dim i As Integer
      ddlF = CType(e.Item.FindControl("ddlFunciona"), DropDownList)
            'ddlE = CType(e.Item.FindControl("ddlIncidenciasE"), DropDownList)
      ddlV = CType(e.Item.FindControl("ddlIncidenciasV"), DropDownList)
      'en el e.item.dataitem lo que tengo es la fila de la tabla con todos sus valores
      Select Case Utiles.nullABlanco(CType(e.Item.DataItem, DataRowView).Item("funciona"))
                Case "0"
                    ddlF.SelectedIndex = 1
                Case "1"
                    ddlF.SelectedIndex = 0
                Case Else
                    ddlF.SelectedIndex = 2
            End Select

            'For i = 0 To ddlE.Items.Count - 1
            '  If ddlE.Items(i).Value = Utiles.nullABlanco(CType(e.Item.DataItem, DataRowView).Item("idIncidenciaElectrica")) Then
            '    ddlE.Items(i).Selected = True
            '  Else
            '    ddlE.Items(i).Selected = False
            '  End If
            'Next

      For i = 0 To ddlV.Items.Count - 1
        If ddlV.Items(i).Value = Utiles.nullABlanco(CType(e.Item.DataItem, DataRowView).Item("idIncidenciaVolumetrica")) Then
          ddlV.Items(i).Selected = True
        Else
          ddlV.Items(i).Selected = False
        End If

      Next
      ' ponemos este código aquí y no en el rellenar lista porque es aquí dónde se cargan las listas
      'y sino, nos recarga y nos quita el seleccionar
      ddlV.Items.Insert(0, New ListItem("[Seleccionar]", ""))
            'ddlE.Items.Insert(0, New ListItem("[Seleccionar]", ""))
    End If
  End Sub

  Protected Sub btnAceptarLectura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarLectura.Click

        Dim i As Integer
        Dim v_CodigoPVYCR, v_Cod_Fuente_Dato, v_Fecha, v_Hora, v_Funciona, v_idElem, v_caudalmedido As String
        Dim v_inciVol, v_InitElec, v_observaciones As String
        Dim mensaje As String = ""

        For i = 0 To rptMotoresDetalle2.Items.Count - 1
            'si marcan que la aceptan, insertaremos en TDatosMotores y borraremos del estadillo
            'código de aceptación de lectura para la lectura actual
            v_idElem = CType(rptMotoresDetalle2.Items(i).FindControl("txtidElementoMedida"), TextBox).Text
            v_CodigoPVYCR = CType(rptMotoresDetalle2.Items(i).FindControl("txtCodigoPVYCR"), TextBox).Text
            v_Cod_Fuente_Dato = CType(rptMotoresDetalle2.Items(i).FindControl("txtCod_Fuente_Dato"), TextBox).Text
            v_Fecha = CType(rptMotoresDetalle2.Items(i).FindControl("txtFecha_Medida"), TextBox).Text

            v_inciVol = CType(rptMotoresDetalle2.Items(i).FindControl("ddlIncidenciasV"), DropDownList).SelectedValue

            v_Funciona = CType(rptMotoresDetalle2.Items(i).FindControl("ddlFunciona"), DropDownList).SelectedItem().Text
            If IsNothing(CType(rptMotoresDetalle2.Items(i).FindControl("txtObservaciones"), TextBox).Text) Then
                v_observaciones = ""
            Else
                v_observaciones = CType(rptMotoresDetalle2.Items(i).FindControl("txtObservaciones"), TextBox).Text
            End If
          
            Try
                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion)
                'abrimos una transaccion para que si falla algunos de los inserts haga un rollback
                objTrans = conexion.BeginTransaction()
                comando.Transaction = objTrans
                If CType(rptMotoresDetalle2.Items(i).FindControl("cbAceptarM"), CheckBox).Checked Then

                    comando.CommandText = "INSERT INTO PVYCR_DatosMotores([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                        ",[LecturaContador_M3],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional] " & _
                        ",[ReinicioLecturaVolumetrica],[Funciona],[justificacion], [Caudalmedido]) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_dato,@Fecha_Medida, " & _
                        "@LecturaContador_M3,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                        "@ReinicioLecturaVolumetrica, @Funciona,@justificacion,@caudalmedido)"
                    comando.Parameters.Clear()
                    comando.Parameters.AddWithValue("@idElementoMedida", v_idElem)
                    comando.Parameters.AddWithValue("@CodigoPVYCR", v_CodigoPVYCR)
                    comando.Parameters.AddWithValue("@Fecha_medida", v_Fecha)
                    comando.Parameters.AddWithValue("@cod_fuente_dato", v_Cod_Fuente_Dato)

                    'Lectura contador M3
                    If utiles.nullABlanco(CType(rptMotoresDetalle2.Items(i).FindControl("txtLeccontador"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@LecturaContador_M3", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@LecturaContador_M3", Replace(Replace(CType(rptMotoresDetalle2.Items(i).FindControl("txtLeccontador"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'obserbvaciones
                    comando.Parameters.AddWithValue("@Observaciones", v_observaciones)
                    'Id incidencia volumétrica
                    comando.Parameters.AddWithValue("@idIncidenciaVolumetrica", utiles.BlancoANull(v_inciVol))
                    'Consumo volumétrico
                    If utiles.nullABlanco(CType(rptMotoresDetalle2.Items(i).FindControl("txtConsumoVol"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", Replace(Replace(CType(rptMotoresDetalle2.Items(i).FindControl("txtConsumoVol"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Reinico lectura volum´´etrica
                    If utiles.nullABlanco(CType(rptMotoresDetalle2.Items(i).FindControl("txtReiniciVol"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", Replace(Replace(CType(rptMotoresDetalle2.Items(i).FindControl("txtReiniciVol"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Funciona
                    comando.Parameters.AddWithValue("@Funciona", v_Funciona)
                    'Justificación
                    comando.Parameters.AddWithValue("@Justificacion", "")
                    'Caudal medido
                    If utiles.nullABlanco(CType(rptMotoresDetalle2.Items(i).FindControl("txtCaudalMedido"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@CaudalMedido", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@CaudalMedido", Replace(Replace(CType(rptMotoresDetalle2.Items(i).FindControl("txtCaudalmedido"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    comando.ExecuteNonQuery()

                    'insertamos en el histórico
                    comando.CommandText = "INSERT INTO [PVYCR_DatosMotores_Estadillo_Histo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                        ",[LecturaContador_M3],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional] " & _
                        ",[ReinicioLecturaVolumetrica],[Funciona],[justificacion],[Produccion],login,[CaudalMedido]) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_dato,@Fecha_Medida, " & _
                        "@LecturaContador_M3,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                        "@ReinicioLecturaVolumetrica, @Funciona,@justificacion, @produccion, @login,@caudalmedido) "
                    comando.Parameters.AddWithValue("@Produccion", "S")
                    comando.Parameters.AddWithValue("@login", CType(rptMotoresDetalle2.Items(i).FindControl("txtLogin"), TextBox).Text)

                    comando.ExecuteNonQuery()
                    'lo borraremos de la tabla [PVYCR_DatosMotores_estadillo]

                    comando.CommandText = "delete from [PVYCR_DatosMotores_estadillo] where idelementoMedida = '" & v_idElem & "' and CodigoPVYCR = '" & v_CodigoPVYCR & "' and  Cod_Fuente_Dato = '" & v_Cod_Fuente_Dato & "' and Fecha_medida = '" & v_Fecha & "'"

                    comando.ExecuteNonQuery()

                ElseIf CType(rptMotoresDetalle2.Items(i).FindControl("cbRechazarM"), CheckBox).Checked Then
                    'código de rechazo de lectura
                    'insertamos en el histórico
                    comando.CommandText = "INSERT INTO [PVYCR_DatosMotores_Estadillo_Histo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                          ",[LecturaContador_M3],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional] " & _
                          ",[ReinicioLecturaVolumetrica],[Funciona],[justificacion],[Produccion], login,[CaudalMedido]) " & _
                          "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_dato,@Fecha_Medida, " & _
                          "@LecturaContador_M3,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                          "@ReinicioLecturaVolumetrica, @Funciona,@justificacion, @produccion, @login,@CaudalMedido) "
                    comando.Parameters.Clear()
                    comando.Parameters.AddWithValue("@idElementoMedida", v_idElem)
                    comando.Parameters.AddWithValue("@CodigoPVYCR", v_CodigoPVYCR)
                    comando.Parameters.AddWithValue("@Fecha_Medida", v_Fecha)
                    comando.Parameters.AddWithValue("@cod_fuente_dato", v_Cod_Fuente_Dato)
                    comando.Parameters.AddWithValue("@login", CType(rptMotoresDetalle2.Items(i).FindControl("txtLogin"), TextBox).Text)

                    'Lectura contador M3
                    If utiles.nullABlanco(CType(rptMotoresDetalle2.Items(i).FindControl("txtLeccontador"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@LecturaContador_M3", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@LecturaContador_M3", Replace(Replace(CType(rptMotoresDetalle2.Items(i).FindControl("txtLeccontador"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Observaciones
                    comando.Parameters.AddWithValue("@Observaciones", v_observaciones)
                    'Incidencia volumétrica
                    comando.Parameters.AddWithValue("@idIncidenciaVolumetrica", v_inciVol)
                    'Consumo volumétrico
                    If utiles.nullABlanco(CType(rptMotoresDetalle2.Items(i).FindControl("txtConsumoVol"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", Replace(Replace(CType(rptMotoresDetalle2.Items(i).FindControl("txtConsumoVol"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Rinicio volumétrico
                    If utiles.nullABlanco(CType(rptMotoresDetalle2.Items(i).FindControl("txtReiniciVol"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", Replace(Replace(CType(rptMotoresDetalle2.Items(i).FindControl("txtReiniciVol"), TextBox).Text, ".", ""), ",", "."))
                    End If
                    'Caudal Medido
                    If utiles.nullABlanco(CType(rptMotoresDetalle2.Items(i).FindControl("txtCaudalMedido"), TextBox).Text) = "" Then
                        comando.Parameters.AddWithValue("@CaudalMedido", DBNull.Value)
                    Else
                        comando.Parameters.AddWithValue("@CaudalMedido", Replace(CType(rptMotoresDetalle2.Items(i).FindControl("txtCaudalMedido"), TextBox).Text, ",", "."))
                    End If

                    comando.Parameters.AddWithValue("@Justificacion", "")

                    comando.Parameters.AddWithValue("@Funciona", v_Funciona)
                    comando.Parameters.AddWithValue("@Produccion", "N")


                    comando.ExecuteNonQuery()

                    'lo borraremos de la tabla [PVYCR_DatosMotores_estadillo]
                    comando.CommandText = "delete from [PVYCR_DatosMotores_estadillo] where idElementoMedida = '" & v_idElem & "' and CodigoPVYCR = '" & v_CodigoPVYCR & "' and  Cod_Fuente_Dato = '" & v_Cod_Fuente_Dato & "' and Fecha_Medida = '" & v_Fecha & "'"

                    comando.ExecuteNonQuery()
                End If
                objTrans.Commit()
            Catch Exc As System.Data.SqlClient.SqlException
                objTrans.Rollback()
                'Response.Write("Error: " & Exc.Message & " num2: " & Exc.Number)
                Select Case Exc.Number
                    Case 547
                        mensaje = mensaje & "No existe un elemento de medida para el punto con fecha: " & v_Fecha & " \n"
                    Case 2627
                        mensaje = mensaje & "El dato motor ya existe para la fecha: " & v_Fecha & " \n"
                End Select

            Catch Exc As Exception
                'Response.Write("Error: " & Exc.Message)
                mensaje = mensaje & "Error: " & Exc.Message & " \n"
            End Try
        Next
        If mensaje <> "" Then
            JavaScript.Alert(Page, mensaje)
        End If

        crearDataSetMEstadillo(v_Cod_Fuente_Dato)
        dstMotores.Tables("TablaMotores").DefaultView.Sort = "fecha_medida "
        rptMotoresDetalle.DataSource = dstMotores.Tables("TablaMotores")

        rptMotoresDetalle.DataBind()

        rptMotoresDetalle2.DataSource = dstMotores.Tables("TablaMotores2")
        rptMotoresDetalle2.DataBind()

    End Sub

  Protected Sub btnCancelarLectura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarLectura.Click
        lblidMotor.Text = ""
        pnlMotores.Visible = True
        'ncm paginacion 28/08/2008
        Panel1.Visible = False
        pnlBotones.Visible = False
        pnlEstadillo.Visible = False
        pnlEtiquetas.Visible = False
        pnlPrincipal.Visible = False

        'si quiero que refresque la pantalla principal, por si han aceptado todas las lecturas y no queda nada en estadillo, deberé
        'descomentar el response.redirect
        '     Response.Redirect("motores.aspx")

  End Sub
    Protected Sub rptMotoresDetalle2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptMotoresDetalle2.ItemCreated
        'Sólo se dispara una vez, cuando pasa por el repeater.
        ' solo funciona si los items son los del cuerpo, sino da error, por eso ponemos el IF
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            ' rellenamos las listas de incidencias eléctricas y las volumétricas
            rellenarListas(e)
            Formateo_controles_cliente(e)
        End If

    End Sub
  Protected Sub rellenarListas(ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        'ddlE = CType(e.Item.FindControl("ddlIncidenciasE"), DropDownList)
    ddlV = CType(e.Item.FindControl("ddlIncidenciasV"), DropDownList)
    'Si la tabla no tiene valores la cargamos, si no ponemos el IF, se carga tantas veces como la fila que
    'estamos evaluando.
    'No hacemos el databind porque ya hacemos un databind en el repeat que hace el databind de todo
    'lo que tiene denntro.
        'If IsNothing(dstMotores.Tables("TablaIncidenciasE")) Then
        '  daMotores.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'E' order by descripcion"
        '  daMotores.Fill(dstMotores, "TablaIncidenciasE")
        'End If

        'ddlE.DataSource = dstMotores.Tables("TablaIncidenciasE")
        'ddlE.DataValueField = "idIncidencia"
        'ddlE.DataTextField = "Descripcion"

    If IsNothing(dstMotores.Tables("TablaIncidenciasV")) Then
      daMotores.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'V' order by descripcion"
      daMotores.Fill(dstMotores, "TablaIncidenciasV")
    End If
    ddlV.DataSource = dstMotores.Tables("TablaIncidenciasV")
    ddlV.DataValueField = "idIncidencia"
    ddlV.DataTextField = "Descripcion"

  End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        'necesito que esté fuera del is notpostback para que se grabe la fecha de desconexion para el LOG NCM
        lblCabecera.Text = genHTML.cabecera(Page)
        imgCalendario.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtEDFechaMedida.ClientID & "'),'dd/mm/yyyy');")
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            Session("Ordenacion") = ""

            lblPestanyas.Text = genHTML.pestanyasMenu(0, "../", Page, Session("idperfil"), Session("usuarioReg"))
            utiles.Comprobar_Conexion_BD(Page, conexion)
            'ncm 25/08/2008
            'If Request.QueryString("pag") <> "" Then
            '    lblPagina.Text = Request.QueryString("pag")
            'Else
            '    lblPagina.Text = "1"
            'End If

            sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosMotores_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
            '"DM.Cod_Fuente_Dato = '05'"

            sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "

            crearDataSets()

            rptMotores.DataSource = dstMotores.Tables("TablaMotores")
            rptMotores.DataBind()
            'lblNumpaginas.DataBind()
        End If
    End Sub
    Private Sub lbtordenarPVYCR_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarPVYCR.Click
        txtFiltro.Text = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosMotores_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
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
        rptMotores.DataSource = dstMotores.Tables("TablaMotores")
        rptMotores.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtordenarCauce_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarCauce.Click
        txtFiltro.Text = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosMotores_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
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
        rptMotores.DataSource = dstMotores.Tables("TablaMotores")
        rptMotores.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtordenarNumReg_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarNumReg.Click
        txtFiltro.Text = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce, count(*) NumRegEstdilloM " & _
                           "FROM PVYCR_DatosMotores_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) and " & _
                         "group by DM.codigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_dato,P.DenominacionPunto,c.DenominacionCauce"
        '                            "DM.Cod_Fuente_Dato = '05'" & _

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
        rptMotores.DataSource = dstMotores.Tables("TablaMotores")
        rptMotores.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtFiltrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltrar.Click
        Dim sentenciaQuery As String
        utiles.Comprobar_Conexion_BD(Page, conexion)
        'lblPagina.Text = "1"
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosMotores_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"
        sentenciaQuery = sentenciaSel
        If txtFiltro.Text <> "" Then
            sentenciaQuery &= " and DM.CodigoPVYCR like '%" & txtFiltro.Text & "%' "
        End If

        sentenciaOrder = sentenciaQuery & " order by DM.CodigoPVYCR, DM.idelementoMedida, DM.Cod_Fuente_Dato "


        crearDataSets()
        rptMotores.DataSource = dstMotores.Tables("TablaMotores")
        rptMotores.DataBind()
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
                           "FROM PVYCR_DatosMotores_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"

        sentenciaOrder = sentenciaSel & " order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "
        crearDataSets()
        rptMotores.DataSource = dstMotores.Tables("TablaMotores")
        rptMotores.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Protected Sub Formateo_controles_cliente(ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)

        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtLeccontador"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtLeccontador"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtConsumoVol"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtConsumoVol"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtReiniciVol"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtReiniciVol"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtCaudalMedido"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtCaudalMedido"), TextBox))
    End Sub

    Protected Sub btnListado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListado.Click
        Dim vpos = lblidMotor.Text.IndexOf("/")
        Dim vcadena1 = lblidMotor.Text.Substring(0, vpos - 1)

        'Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
        '        "window.open('../listados/ListadoIncidenciasMotor.aspx?codigoPVYCR=" & vcadena1 & "','_blank','')" & _
        '"</script>")
        Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
        "window.open('../listados/ListadoInterriego.aspx','_blank','')" & _
"</script>")

    End Sub
    Protected Sub obtenerVolumenDiferencial()
        'vamos a calcular la diferencia de volúmenes según registros
        'el caso de las acequias es un caso especial y se calcula obteniendo la media del caudal por el tiempo trancurrido entre dos lecturas.
        Dim i As Integer
        Dim v_vol, v_vol_ant, v_diferencial, v_caudal, v_caudal_ant, v_caudal_medio, v_diferencial_acum, v_lectura_ant, v_diferencial_seg, v_diferencial_m3, v_diferencial_acumm3 As Decimal
        Dim v_tiempo, v_tiempo_ant As Date
        Dim v_tiempo_medio As Integer


        If dstMotores.Tables("TablaMotores2").Rows.Count > 0 Then
            If Not dstMotores.Tables("TablaMotores2").Columns.Contains("Diferencial") Then
                'añadimos la columna diferencial al dataset
                dstMotores.Tables("TablaMotores2").Columns.Add("Diferencial")
                dstMotores.Tables("TablaMotores2").Columns.Add("Diferencial_Acum")
            End If
            'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
            For i = 0 To dstMotores.Tables("TablaMotores2").Rows.Count - 1

                If i = 0 Then
                    'rellenamos la columna diferencial del dataset con el valor del último registro de motores, si no lo hubiera con un 0
                    Dim numMotores As Integer
                    'comprbamos si hay lecturas en la tabla motores, si no hay se pone a cero el volumen anterior
                    'si hay lecturas, tendremos que buscar cual es la inmediatamente anterior a la actual
                    numMotores = dstMotores.Tables("TablaMotores").Rows.Count
                    If numMotores = 0 Then
                        v_vol_ant = 0
                        v_lectura_ant = v_vol_ant
                    Else
                        Dim j As Integer
                        Dim fecha_medida As Date = dstMotores.Tables("TablaMotores2").Rows(i).Item("fecha_medida")

                        Dim reg As Integer = numMotores - 1

                        For j = 0 To reg
                            If dstMotores.Tables("TablaMotores").Rows(j).Item("fecha_medida") < fecha_medida Then
                                'si es nulo lo compararé con el anterior
                                If dstMotores.Tables("TablaMotores").Rows(j).Item("LecturaContador_M3").ToString() = "" Then
                                    'comprobaremos que no sea el último registro
                                    If j = reg Then
                                        v_vol_ant = Convert.ToDecimal(0 & dstMotores.Tables("TablaMotores").Rows(j).Item("LecturaContador_M3").ToString)
                                    Else
                                        v_vol_ant = Convert.ToDecimal(0 & dstMotores.Tables("TablaMotores").Rows(j + 1).Item("LecturaContador_M3").ToString)
                                        Exit For
                                    End If
                                Else
                                    v_vol_ant = Convert.ToDecimal(0 & dstMotores.Tables("TablaMotores").Rows(j).Item("LecturaContador_M3").ToString)
                                    Exit For
                                End If
                            End If
                        Next
                        v_lectura_ant = v_vol_ant
                    End If
                End If
                'comprobamos si existen incidencias
                '*************************--> obsoleto(19/05/2008)***********************-
                'si la incidencia es reseteo (6) o cambio de contador(7) se deberá tomar el valor del campo Reinicio Lectura Volumetrica 
                '************************* NUEVO ****************************************
                'si la incidencia es reseteo o cambio de contador  la fórmula es:
                '                   ((lecturam3(dia15)-reseteo(dia15) + consumo adic. (dia15)
                'si la incidencia es contador averiado yconsumo negativo (8) (5), la fñormula es :
                '                   (lecturaM3(i) + Consumovolumetricoadicional(i)) - lecturam3(i-1)

                'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
                If utiles.nullABlanco(dstMotores.Tables("TablaMotores2").Rows(i).Item("LecturaContador_M3").ToString) = "" Then
                    v_diferencial = 0
                    v_diferencial_acum = v_diferencial_acum
                    'añadimos los valores a la tabla
                    dstMotores.Tables("TablaMotores2").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", DBNull.Value)
                    dstMotores.Tables("TablaMotores2").Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                Else

                    v_vol = Convert.ToDecimal(0 & dstMotores.Tables("TablaMotores2").Rows(i).Item("LecturaContador_M3").ToString)
                    If (dstMotores.Tables("TablaMotores2").Rows(i).Item("idincidenciaVolumetrica").ToString = "6") Or _
                        (dstMotores.Tables("TablaMotores2").Rows(i).Item("idincidenciaVolumetrica").ToString = "7") Then
                        v_diferencial = ((Convert.ToDecimal(0 & dstMotores.Tables("TablaMotores2").Rows(i).Item("LecturaContador_M3").ToString) - Convert.ToDecimal(0 & dstMotores.Tables("TablaMotores2").Rows(i).Item("ReiniciolecturaVolumetrica").ToString)) + _
                        Convert.ToDecimal(0 & dstMotores.Tables("TablaMotores2").Rows(i).Item("ConsumoVolumetricoAdicional").ToString))
                    ElseIf _
                       (dstMotores.Tables("TablaMotores2").Rows(i).Item("idincidenciaVolumetrica").ToString = "5") Or _
                       (dstMotores.Tables("TablaMotores2").Rows(i).Item("idincidenciaVolumetrica").ToString = "8") Then
                        If utiles.nullABlanco(dstMotores.Tables("TablaMotores2").Rows(i).Item("ConsumoVolumetricoAdicional").ToString) <> "" Then
                            v_diferencial = (Convert.ToDecimal(0 & dstMotores.Tables("TablaMotores2").Rows(i).Item("LecturaContador_M3").ToString) + dstMotores.Tables("TablaMotores2").Rows(i).Item("ConsumoVolumetricoAdicional").ToString) - _
                            Convert.ToDecimal(0 & v_lectura_ant)
                        Else
                            v_diferencial = (Convert.ToDecimal(0 & dstMotores.Tables("TablaMotores2").Rows(i).Item("LecturaContador_M3").ToString) + Convert.ToDecimal(0 & dstMotores.Tables("TablaMotores2").Rows(i).Item("ConsumoVolumetricoAdicional").ToString)) - _
                            Convert.ToDecimal(0 & v_lectura_ant)

                        End If

                    Else
                        v_diferencial = v_vol - v_vol_ant
                    End If
                    v_vol_ant = v_vol
                    'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
                    v_lectura_ant = dstMotores.Tables("TablaMotores2").Rows(i).Item("LecturaContador_M3").ToString
                    'calculamos el diferencial acumulado
                    v_diferencial_acum = v_diferencial_acum + v_diferencial
                    'añadimos los valores a la tabla
                    dstMotores.Tables("TablaMotores2").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                    dstMotores.Tables("TablaMotores2").Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                End If
                'end if
            Next
        End If
    
    End Sub

    Protected Sub lbtNuevoElemento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtNuevoElemento.Click
        pnlMotores.Visible = False
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
            "where c.codigocauce = p.codigocauce and  p.codigopvycr = e.codigopvycr and p.tipopunto = 'M' and e.tipo ='V' " & _
            "order by c.codigocauce"
        daCauces.Fill(dstCauces, "TablaCauces")
        ddlEDCauce.DataSource = dstCauces.Tables("TablaCauces")
        ddlEDCauce.DataValueField = "codigoCauce"
        ddlEDCauce.DataTextField = "codigocauce"
        ddlEDCauce.DataBind()
        ddlEDCauce.Items.Insert(0, New ListItem("[Seleccionar]", ""))

        If IsNothing(dstMotores.Tables("TablaIncidenciasV")) Then
            daMotores.SelectCommand.CommandText = "select idIncidencia, descripcion from PVYCR_incidencias where TipoIncidencia= 'V' order by descripcion"
            daMotores.Fill(dstMotores, "TablaIncidenciasV")
        End If
        ddlEDIncidenciasV.DataSource = dstMotores.Tables("TablaIncidenciasV")
        ddlEDIncidenciasV.DataValueField = "idIncidencia"
        ddlEDIncidenciasV.DataTextField = "Descripcion"
        ddlEDIncidenciasV.DataBind()
        ddlEDIncidenciasV.Items.Insert(0, New ListItem("[Seleccionar]", ""))
     
        ddlEDfunciona.Items.Insert(0, New ListItem("[seleccionar]", ""))
        ddlEDfunciona.Items.Insert(1, New ListItem("SI", "1"))
        ddlEDfunciona.Items.Insert(2, New ListItem("NO", "0"))
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
        pnlMotores.Visible = True
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
        txtEdLecturaContador.Text = ""
        txtEDObservaciones.Text = ""
        ddlEDIncidenciasV.Items.Clear()
        txtEDConsumoVol.Text = ""
        txtEDReinicioVol.Text = ""
        ddlEDfunciona.Items.Clear()
        txtEDUsuario.Text = ""
        txtEDCaudalMedido.Text = ""
    End Sub

    Protected Sub ddlEDCauce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEDCauce.SelectedIndexChanged
        Dim daPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstPuntos As DataSet = New System.Data.DataSet()
        'lista de puntos
        daPuntos.SelectCommand.CommandText = "select distinct p.codigopvycr from pvycr_cauces c, pvycr_puntos p, pvycr_elementosmedida e " & _
                                            "where c.codigocauce = p.codigocauce and p.tipopunto = 'M' and p.codigopvycr = e.codigopvycr and " & _
                                            "e.tipo = 'V' and c.codigocauce = '" & ddlEDCauce.Text & "' " & _
                                            "order by p.codigopvycr"
        daPuntos.Fill(dstPuntos, "TablaPuntos")
        ddlEDCodigoPVYCR.DataSource = dstPuntos.Tables("TablaPuntos")
        ddlEDCodigoPVYCR.DataValueField = "codigopvycr"
        ddlEDCodigoPVYCR.DataTextField = "codigopvycr"
        ddlEDCodigoPVYCR.DataBind()
        ddlEDCodigoPVYCR.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        ddlEDidElemento.Items.Clear()
    End Sub

    Protected Sub btnEDAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEDAceptar.Click
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim objTrans As SqlTransaction
        Try
            utiles.Comprobar_Conexion_BD(Page, conexion)
            ''comprobamos si existe el elemento de medida, si no existe lo insertamos para que no falle la FK que hay entre datos alimentacion y elementos de medida
            'daMotores.SelectCommand.CommandText = "select count(*) numero from pvycr_elementosmedida where codigopvycr= '" & ddlEDCodigoPVYCR.Text & "' and idelementomedida = '" & txtEDidElementomedida.Text & "' "
            'daMotores.Fill(dstMotores, "TablaExisteElemento")

            comando.Parameters.AddWithValue("@idElementoMedida", ddlEDidElemento.Text)
            comando.Parameters.AddWithValue("@CodigoPVYCR", ddlEDCodigoPVYCR.Text)
            comando.Parameters.AddWithValue("@cod_fuente_datos", ddlEDCodFuenteDato.Text)
            'If dstMotores.Tables("TablaExisteElemento").Rows(0).Item("numero") = 0 Then
            '    comando.CommandText = "INSERT INTO PVYCR_ELEMENTOSMEDIDA (CODIGOPVYCR, idElementoMedida, TIPO) VALUES " & _
            '    "(@CodigoPVYCR,@idElementoMedida,'V')"
            '    comando.ExecuteNonQuery()
            'End If
            'abrimos una transaccion para que si falla algunos de los inserts haga un rollback
            objTrans = conexion.BeginTransaction()
            comando.Transaction = objTrans
            comando.CommandText = "INSERT INTO [PVYCR_DatosMotores_Estadillo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                        ",[LecturaContador_M3],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional] " & _
                        ",[ReinicioLecturaVolumetrica],[Funciona],[justificacion], login,[CaudalMedido]) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_datos,@Fecha_Medida, " & _
                        "@LecturaContador_M3,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                        "@ReinicioLecturaVolumetrica, @Funciona,@justificacion, @login,@CaudalMedido) "

            comando.Parameters.AddWithValue("@Fecha_medida", txtEDFechaMedida.Text)

            'Lectura contador M3
            If utiles.nullABlanco(txtEdLecturaContador.Text) = "" Then
                comando.Parameters.AddWithValue("@LecturaContador_M3", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@LecturaContador_M3", txtEdLecturaContador.Text.Replace(",", "."))
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
            comando.Parameters.AddWithValue("@Funciona", ddlEDfunciona.Text)
            comando.Parameters.AddWithValue("@login", txtEDUsuario.Text)
            comando.Parameters.AddWithValue("@Justificacion", "")
            'Caudal Medido
            If utiles.nullABlanco(txtEDCaudalMedido.Text) = "" Then
                comando.Parameters.AddWithValue("@CaudalMedido", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@CaudalMedido", Replace(txtEDCaudalMedido.Text, ",", "."))
            End If

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
                          "FROM PVYCR_DatosMotores_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                          "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                           "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
        '"DM.Cod_Fuente_Dato = '05'"

        sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "

        crearDataSets()

        rptMotores.DataSource = dstMotores.Tables("TablaMotores")
        rptMotores.DataBind()
        'lblNumpaginas.DataBind()
        pnlMotores.Visible = True
        Panel1.Visible = False
        pnlEstadillo.Visible = False
        pnlNuevoElemento.Visible = False
    End Sub

    Protected Sub ddlEDCodigoPVYCR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEDCodigoPVYCR.SelectedIndexChanged
        Dim daEDElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstEDElementos As DataSet = New System.Data.DataSet()
        'lista de puntos
        daEDElementos.SelectCommand.CommandText = "select e.idelementomedida from pvycr_puntos p, pvycr_elementosmedida e " & _
                                            "where p.codigoPVYCR = e.codigoPVYCR and p.tipopunto = 'M' and e.codigoPVYCR = '" & ddlEDCodigoPVYCR.Text & "' " & _
                                            "and e.tipo = 'V' order by e.idelementomedida"
        daEDElementos.Fill(dstEDElementos, "TablaEDElementos")
        ddlEDidElemento.DataSource = dstEDElementos.Tables("TablaEDElementos")
        ddlEDidElemento.DataValueField = "idelementomedida"
        ddlEDidElemento.DataTextField = "idelementomedida"
        ddlEDidElemento.DataBind()
    End Sub
End Class
