Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript

Partial Class SICAH_Nivus
    Inherits System.Web.UI.Page
    Dim conexion_origen As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnPruebasOrigen"))
    Dim conexion_destino As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnPruebasDestino"))
    Dim da_origen As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion_origen)
    Dim dst_origen As DataSet = New System.Data.DataSet()
    Dim da_destino As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion_destino)
    Dim dst_destino As DataSet = New System.Data.DataSet()
    Dim sentenciaSel As String
    Dim parfila As Integer = 0
    Dim porcentaje As Integer = 10

    Dim comando_origen As SqlClient.SqlCommand = New SqlCommand("", conexion_origen)
    Dim comando_destino As SqlClient.SqlCommand = New SqlCommand("", conexion_destino)

    Dim parametros() As String

    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"

    End Function

    Protected Function CompruebaHora(ByVal elDataitem As DataRowView) As String
        If elDataitem("fechaCompleta").minute = 0 And elDataitem("fechaCompleta").second = 0 Then
            Return "red"
        Else
            Return "#DDDDDD"
        End If
    End Function

    Private Sub crearDataSetsPuntos()
        da_origen.SelectCommand.CommandText = "SELECT DISTINCT codigopvycr FROM SICA_DatosAcequias_TopKapi order by codigopvycr"
        da_origen.Fill(dst_origen, "TablaPuntos")

        rptPuntos.DataSource = dst_origen.Tables("tablaPuntos")
        rptPuntos.DataBind()
    End Sub

    Private Sub crearDataSetsPuntosFechas(ByVal codigoPVYCR As String)
        da_origen.SelectCommand.CommandText = "SELECT DISTINCT codigopvycr, cast(CONVERT(nvarchar(15), Fecha, 103) as datetime) AS fecha " & _
            "FROM SICA_DatosAcequias_TopKapi where codigopvycr='" & codigoPVYCR & "' order by fecha"
        da_origen.Fill(dst_origen, "TablaPuntosFechas")

        rptPuntosFechas.DataSource = dst_origen.Tables("tablaPuntosFechas")
        rptPuntosFechas.DataBind()
    End Sub

    Private Sub crearDataSets(ByVal codigoPVYCR As String, ByVal fecha As Date)
        da_origen.SelectCommand.CommandText = "SELECT fecha as FechaCompleta, codigopvycr, convert(nvarchar(15), fecha, 103) as fecha,  convert(nvarchar(15), hora, 108) as hora, " & _
                        "calado_m, caudal, velocidad from SICA_DatosAcequias_Topkapi where codigopvycr='" & codigoPVYCR & "' and convert(nvarchar(15), fecha, 103)='" & _
                        fecha & "' and datepart(minute, fecha)=0 order by fecha"
        da_origen.Fill(dst_origen, "TablaNivus")

        rptNivus.DataSource = dst_origen.Tables("TablaNivus")
        rptNivus.DataBind()
    End Sub

    Private Sub crearDataSetDetalle(ByVal codigoPVYCR As String, ByVal fecha As String, ByVal hora As String)
        If conexion_origen.State = ConnectionState.Closed Then conexion_origen.Open()

        'Sacamos los registros de la hora anterior. En este caso siempre existirán pq se eliminan cuando es eliminada la hora en punto.
        sentenciaSel = "select codigopvycr, convert(nvarchar(15), fecha, 103) as fecha,  convert(nvarchar(15), fecha, 108) as hora, " & _
                "calado_m, caudal, velocidad,fecha as fechaCompleta from (" & _
                "SELECT top 12 * from SICA_DatosAcequias_Topkapi where codigoPVYCR='" & codigoPVYCR & "' " & _
                "and fecha <='" & fecha & " " & hora & "' and fecha >'" & DateAdd(DateInterval.Hour, -1, CDate(fecha & " " & hora)) & "' " & _
                "order by fecha desc) as dtable order by fecha"
        da_origen.SelectCommand.CommandText = sentenciaSel
        da_origen.Fill(dst_origen, "TablaNivusDetalle")

        'Sacamos los registros de la hora posterior. Si existen los mostramos y sino, los buscamos en el histórico.
        sentenciaSel = "select codigopvycr, convert(nvarchar(15), fecha, 103) as fecha,  convert(nvarchar(15), fecha, 108) as hora, " & _
                "calado_m, caudal, velocidad,fecha as fechaCompleta from (" & _
                "SELECT top 13 * from SICA_DatosAcequias_Topkapi where codigoPVYCR='" & codigoPVYCR & "' " & _
                "and fecha >'" & fecha & " " & hora & "' and fecha < '" & DateAdd(DateInterval.Hour, 1, CDate(fecha & " " & hora)) & "' " & _
                "order by fecha) as dtable order by fecha"
        comando_origen.CommandText = sentenciaSel
        If comando_origen.ExecuteScalar Is Nothing Then
            sentenciaSel = "select codigopvycr, convert(nvarchar(15), fecha_medida, 103) as fecha,  convert(nvarchar(15), fecha_medida, 108) as hora, " & _
                "calado_m, caudal_m3s as caudal, replace(Observaciones, 'Inserción Automática NIVUS. Velocidad= ','') as velocidad,fecha_medida as fechaCompleta from (" & _
                "SELECT top 13 * from SICA_DatosAcequias_Estadillo_Historico where codigoPVYCR='" & codigoPVYCR & "' " & _
                "and fecha_medida >'" & fecha & " " & hora & "' and fecha_medida < '" & DateAdd(DateInterval.Hour, 1, CDate(fecha & " " & hora)) & "' " & _
                "order by fecha_medida) as dtable order by fecha_medida"
        End If
        da_origen.SelectCommand.CommandText = sentenciaSel
        da_origen.Fill(dst_origen, "TablaNivusDetalle")
    End Sub

    Protected Sub PVYCRSeleccionada(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPVYCRSelecc.Text = CType(sender, LinkButton).CommandArgument
        parametros = Split(CType(sender, LinkButton).CommandArgument, "#")

        pnlPuntosFechasHoras.Visible = False
        pnlPuntos.Visible = False
        pnlPuntosFechas.Visible = False
        pnlDetalle.Visible = True
        btnCancelar.Visible = True
        btnEstadillo.Visible = True
        btnProduccion.Visible = True
        crearDataSetDetalle(parametros(0), parametros(1), parametros(2))

        lblNombrePVYCR.Text = parametros(0) & " - " & parametros(1)

        rptNivusDetalle.DataSource = dst_origen.Tables("TablaNivusDetalle")
        rptNivusDetalle.DataBind()
    End Sub

    Protected Sub PuntoSeleccionado(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPuntoSelecc.Text = CType(sender, LinkButton).CommandArgument

        pnlPuntos.Visible = False
        pnlPuntosFechas.Visible = True
        pnlPuntosFechasHoras.Visible = False
        pnlDetalle.Visible = False
        btnCancelar.Visible = True
        btnEstadillo.Visible = False
        btnProduccion.Visible = False
        crearDataSetsPuntosFechas(lblPuntoSelecc.Text)

        lblNombrePunto.Text = lblPuntoSelecc.Text & ": SELECCIONE UNA FECHA"
    End Sub

    Protected Sub PuntoFechaSeleccionado(ByVal sender As Object, ByVal e As System.EventArgs)
        lblFechaSelecc.Text = CType(sender, LinkButton).CommandArgument

        pnlPuntos.Visible = False
        pnlPuntosFechas.Visible = False
        pnlPuntosFechasHoras.Visible = True
        pnlDetalle.Visible = False
        btnCancelar.Visible = True
        btnEstadillo.Visible = False
        btnProduccion.Visible = False
        crearDataSets(lblPuntoSelecc.Text, String.Format("{0:dd/MM/yyyy}", lblFechaSelecc.Text))

        lblNombreFecha.Text = lblPuntoSelecc.Text & " " & lblFechaSelecc.Text & ": SELECCIONE UNA FECHA"

        rptNivusDetalle.DataSource = dst_origen.Tables("TablaNivusDetalle")
        rptNivusDetalle.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            Session("Ordenacion") = ""
            lblCabecera.Text = genHTML.cabecera(Page)
            lblPestanyas.Text = genHTML.pestanyasMenu(0, "../", Page, Session("idperfil"), Session("usuarioReg"))
            utiles.Comprobar_Conexion_BD(Page, conexion_origen)

            crearDataSetsPuntos()
        End If
    End Sub

    Private Sub Pasar_Fila(ByVal dr As DataRow)
        'PRIMERO DE TODO COMPROBAMOS QUE EXISTA EL PUNTO Y EL ELEMENTO DE MEDIDA, SINO, ABORTAMOS EL PROCESO.
        If ComprobarExistenciaPunto(dr("codigoPVYCR")) = True Then

            Dim Fase As Integer = 0
            If conexion_origen.State = ConnectionState.Closed Then conexion_origen.Open()
            If conexion_destino.State = ConnectionState.Closed Then conexion_destino.Open()

            comando_origen.CommandText = "BEGIN TRANSACTION TranTelemedida"
            comando_origen.ExecuteNonQuery()
            comando_destino.CommandText = "BEGIN TRANSACTION TranSigVector"
            comando_destino.ExecuteNonQuery()

            Try
                'copiado a histórico
                'comprobar si existe
                da_destino.SelectCommand.CommandText = "SELECT * FROM PVYCR_DatosAcequias WHERE CodigoPVYCR = '" & dr("codigopvycr") & "' " & _
                        "AND Fecha_Medida = '" & dr("fechaCompleta") & "'"
                da_destino.Fill(dst_destino, "DatosAcequiasRepetidos")

                If dst_destino.Tables("DatosAcequiasRepetidos").Rows.Count = 0 Then

                    comando_destino.CommandText = "INSERT INTO PVYCR_DatosAcequias (CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, " & _
                                                "Escala_M, Calado_M, RegimenCurva, NumeroParada, Caudal_M3S, TipoObtencionCaudal, Duda_Calidad, " & _
                                                "Observaciones) VALUES (@CodigoPVYCR, @idElementoMedida, @Cod_Fuente_Dato, @Fecha_Medida, " & _
                                                "@Escala_M, @Calado_M, @RegimenCurva, @NumeroParada, @Caudal_M3S, @TipoObtencionCaudal, @Duda_Calidad, " & _
                                                "@Observaciones)"
                    comando_destino.Parameters.AddWithValue("@CodigoPVYCR", dr("codigopvycr"))
                    comando_destino.Parameters.AddWithValue("@idElementoMedida", "Q01")
                    comando_destino.Parameters.AddWithValue("@Cod_Fuente_Dato", "13")
                    comando_destino.Parameters.AddWithValue("@Fecha_Medida", dr("FechaCompleta"))
                    comando_destino.Parameters.AddWithValue("@Escala_M", 0)
                    comando_destino.Parameters.AddWithValue("@Calado_M", dr("Calado_M"))
                    comando_destino.Parameters.AddWithValue("@RegimenCurva", CalcularRegimenCurva(dr("codigopvycr"), dr("fechaCompleta")))
                    comando_destino.Parameters.AddWithValue("@NumeroParada", "13") '?
                    comando_destino.Parameters.AddWithValue("@Caudal_M3S", dr("Caudal"))
                    comando_destino.Parameters.AddWithValue("@TipoObtencionCaudal", "Q")
                    comando_destino.Parameters.AddWithValue("@Duda_Calidad", 0)
                    comando_destino.Parameters.AddWithValue("@Observaciones", "Inserción Automática NIVUS. Velocidad= " & dr("Velocidad"))

                    comando_destino.ExecuteNonQuery()
                    comando_destino.Parameters.Clear()
                End If
                dst_destino.Tables("DatosAcequiasRepetidos").Clear()

                PasarAHistorico(dr)

                Fase = 7
                comando_origen.CommandText = "COMMIT TRANSACTION TranTelemedida"
                comando_origen.ExecuteNonQuery()
                comando_destino.CommandText = "COMMIT TRANSACTION TranSigVector"
                comando_destino.ExecuteNonQuery()

                JavaScript.Alert(Page, "El punto " & dr("codigoPVYCR") & " con fecha " & dr("fechaCompleta") & " ha sido insertado")
            Catch ex As Exception
                comando_origen.CommandText = "ROLLBACK TRANSACTION TranTelemedida"
                comando_origen.ExecuteNonQuery()
                comando_destino.CommandText = "ROLLBACK TRANSACTION TranSigVector"
                comando_destino.ExecuteNonQuery()
                JavaScript.Alert(Page, "No se ha podido insertar el registro: " & dr("codigoPVYCR") & _
                        " " & dr("fecha"))
            End Try
        Else
            JavaScript.Alert(Page, "El punto " & dr("codigoPVYCR") & " no existe, por tanto no podemos trabajar con él")
        End If


    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If pnlDetalle.Visible = True Then
            pnlPuntosFechasHoras.Visible = True
            pnlPuntosFechas.Visible = False
            pnlPuntos.Visible = False
            btnCancelar.Visible = True
        ElseIf pnlPuntosFechasHoras.Visible = True Then
            pnlPuntosFechasHoras.Visible = False
            pnlPuntosFechas.Visible = True
            pnlPuntos.Visible = False
            btnCancelar.Visible = True
        Else
            pnlPuntosFechasHoras.Visible = False
            pnlPuntosFechas.Visible = False
            pnlPuntos.Visible = True
            btnCancelar.Visible = False
        End If
        pnlDetalle.Visible = False
        btnEstadillo.Visible = False
        btnProduccion.Visible = False
    End Sub

    Protected Sub btnEstadillo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEstadillo.Click
        parametros = Split(lblPVYCRSelecc.Text, "#")

        'PRIMERO DE TODO COMPROBAMOS QUE EXISTA EL PUNTO Y EL ELEMENTO DE MEDIDA, SINO, ABORTAMOS EL PROCESO.
        If ComprobarExistenciaPunto(parametros(0)) = True Then
            sentenciaSel = "SELECT codigopvycr, convert(nvarchar(15), fecha, 103) as fecha,  convert(nvarchar(15), hora, 108) as hora, " & _
                                "calado_m, caudal, velocidad, fecha as fechaCompleta from SICA_DatosAcequias_Topkapi where codigoPVYCR='" & parametros(0) & "' and convert(nvarchar(15), fecha, 103)='" & _
                                parametros(1) & "' and convert(nvarchar(15), fecha, 108)='" & parametros(2) & "'"

            da_origen.SelectCommand.CommandText = sentenciaSel
            da_origen.Fill(dst_origen, "TablaNivusEstadillo")

            If conexion_origen.State = ConnectionState.Closed Then conexion_origen.Open()
            If conexion_destino.State = ConnectionState.Closed Then conexion_destino.Open()

            comando_origen.CommandText = "BEGIN TRANSACTION TranTelemedida"
            comando_origen.ExecuteNonQuery()
            comando_destino.CommandText = "BEGIN TRANSACTION TranSigVector"
            comando_destino.ExecuteNonQuery()

            Try
                'copiado a histórico
                PasarAHistorico(dst_origen.Tables("TablaNivusEstadillo").Rows(0))
                comando_origen.CommandText = "COMMIT TRANSACTION TranTelemedida"
                comando_origen.ExecuteNonQuery()
                comando_destino.CommandText = "COMMIT TRANSACTION TranSigVector"
                comando_destino.ExecuteNonQuery()

            Catch ex As Exception
                comando_origen.CommandText = "ROLLBACK TRANSACTION TranTelemedida"
                comando_origen.ExecuteNonQuery()
                comando_destino.CommandText = "ROLLBACK TRANSACTION TranSigVector"
                comando_destino.ExecuteNonQuery()
                JavaScript.Alert(Page, "No se ha podido insertar el registro: " & dst_origen.Tables("TablaNivusEstadillo").Rows(0).Item("codigoPVYCR") & _
                        " " & dst_origen.Tables("TablaNivusEstadillo").Rows(0).Item("fecha"))
            End Try
            dst_origen.Tables("TablaNivusEstadillo").Clear()
        Else
            JavaScript.Alert(Page, "El punto " & parametros(0) & " no existe, por tanto no podemos trabajar con él")
        End If

        comando_origen.CommandText = "UPDATE SICA_DatosAcequias_Estadillo_Historico set RegimenCurva=NULL where RegimenCurva='NULL'"
        comando_origen.ExecuteNonQuery()

        RecargarPuntos(parametros(0), parametros(1))
    End Sub

    Protected Sub btnProduccion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProduccion.Click
        parametros = Split(lblPVYCRSelecc.Text, "#")

        sentenciaSel = "SELECT codigopvycr, convert(nvarchar(15), fecha, 103) as fecha,  convert(nvarchar(15), hora, 108) as hora, " & _
                                "calado_m, caudal, velocidad, fecha as fechaCompleta from SICA_DatosAcequias_Topkapi where codigoPVYCR='" & parametros(0) & "' and convert(nvarchar(15), fecha, 103)='" & _
                                parametros(1) & "' and convert(nvarchar(15), fecha, 108)='" & parametros(2) & "'"
        da_origen.SelectCommand.CommandText = sentenciaSel
        da_origen.Fill(dst_origen, "TablaNivusDetalle")

        Pasar_Fila(dst_origen.Tables("TablaNivusDetalle").Rows(0))

        Dim codigoPVYCR As String = dst_origen.Tables("TablaNivusDetalle").Rows(0).Item("codigoPVYCR")
        Dim fecha As String = dst_origen.Tables("TablaNivusDetalle").Rows(0).Item("fecha")
        Dim hora As String = dst_origen.Tables("TablaNivusDetalle").Rows(0).Item("hora")

        dst_origen.Tables("TablaNivusDetalle").Clear()

        If conexion_destino.State = ConnectionState.Closed Then conexion_destino.Open()
        If conexion_origen.State = ConnectionState.Closed Then conexion_origen.Open()

        comando_destino.CommandText = "UPDATE PVYCR_DatosAcequias set RegimenCurva=NULL where RegimenCurva='NULL'"
        comando_destino.ExecuteNonQuery()
        comando_origen.CommandText = "UPDATE SICA_DatosAcequias_Estadillo_Historico set RegimenCurva=NULL where RegimenCurva='NULL'"
        comando_origen.ExecuteNonQuery()

        RecargarPuntos(codigoPVYCR, CDate(fecha))

    End Sub

    Protected Function CalcularRegimenCurva(ByVal codigoPVYCR As String, ByVal fecha As String) As String
        Dim comdes As SqlClient.SqlCommand = New SqlCommand("", conexion_destino)
        Dim Cod_Curva As Integer = 0
        Dim Dif, Dif_Ant As Double
        Dim Regimen As String = "NULL"
        Dif_Ant = -1      'Lo igualamos a -1, pq la diferencia nunca será negativa y así controlaremos si es la primera vez q se hace el cálculo.

        sentenciaSel = "SELECT calado_m, caudal FROM SICA_DatosAcequias_Topkapi WHERE codigoPVYCR='" & codigoPVYCR & "' AND fecha='" & _
        fecha & "' and hora='" & fecha & "'"
        da_origen.SelectCommand.CommandText = sentenciaSel
        da_origen.Fill(dst_origen, "TablaNivusCurvaGastos")

        Dim calado = CDbl(Replace(dst_origen.Tables("TablaNivusCurvaGastos").Rows(0).Item("calado_m"), ".", ","))
        Dim caudal = CDbl(Replace(dst_origen.Tables("TablaNivusCurvaGastos").Rows(0).Item("caudal"), ".", ","))

        sentenciaSel = "SELECT Cod_Curva FROM PVYCR_CurvasAcequias WHERE codigoPVYCR='" & codigoPVYCR & "' AND idElementoMedida='Q01' and Cod_Curva<>991"
        da_destino.SelectCommand.CommandText = sentenciaSel
        da_destino.Fill(dst_destino, "TablaCurvas")

        For i As Integer = 0 To dst_destino.Tables("TablaCurvas").Rows.Count - 1
            'Vemos la diferencia en valor absoluto de los valores que tenemos con los que hemos encontrado y nos quedamos con la curva cuyos valores
            'hagan la diferencia más pequeña. Lo miramos tanto por arriba como por abajo y para todas las curvas. 

            'Por abajo
            sentenciaSel = "SELECT TOP 1 * FROM PVYCR_CurvasAcequias_Valores WHERE Cod_Curva=" & dst_destino.Tables("TablaCurvas").Rows(i).Item("Cod_Curva") & _
                        " AND Nivel<" & Replace(calado, ",", ".") & " AND Caudal<" & Replace(caudal, ",", ".") & " order by nivel desc"
            da_destino.SelectCommand.CommandText = sentenciaSel
            da_destino.Fill(dst_destino, "TablaValores0")

            If dst_destino.Tables("TablaValores0").Rows.Count > 0 Then
                Dif = Math.Abs(dst_destino.Tables("TablaValores0").Rows(0).Item("Nivel") - calado) + Math.Abs(dst_destino.Tables("TablaValores0").Rows(0).Item("Caudal") - caudal)
                If Dif < Dif_Ant Or Dif_Ant = -1 Then
                    Cod_Curva = dst_destino.Tables("TablaValores0").Rows(0).Item("Cod_Curva")
                    Dif_Ant = Dif
                End If
            End If

            'Por arriba
            sentenciaSel = "SELECT TOP 1 * FROM PVYCR_CurvasAcequias_Valores WHERE Cod_Curva=" & dst_destino.Tables("TablaCurvas").Rows(i).Item("Cod_Curva") & _
                        " AND Nivel>=" & Replace(calado, ",", ".") & " AND Caudal>=" & Replace(caudal, ",", ".") & " order by nivel"
            da_destino.SelectCommand.CommandText = sentenciaSel
            da_destino.Fill(dst_destino, "TablaValores1")

            If dst_destino.Tables("TablaValores1").Rows.Count > 0 Then
                Dif = Math.Abs(dst_destino.Tables("TablaValores1").Rows(0).Item("Nivel") - CDbl(calado)) + Math.Abs(dst_destino.Tables("TablaValores1").Rows(0).Item("Caudal") - caudal)
                If Dif < Dif_Ant Or Dif_Ant = -1 Then
                    Cod_Curva = dst_destino.Tables("TablaValores1").Rows(0).Item("Cod_Curva")
                    Dif_Ant = Dif
                End If
            End If
            dst_destino.Tables("TablaValores0").Clear()
            dst_destino.Tables("TablaValores1").Clear()
        Next

        If Cod_Curva > 0 Then
            If conexion_destino.State = ConnectionState.Closed Then conexion_destino.Open()
            comdes.CommandText = "SELECT Regimen FROM PVYCR_CurvasAcequias WHERE codigoPVYCR='" & codigoPVYCR & "' AND idElementoMedida='Q01' and " & _
                    "Cod_Curva=" & Cod_Curva
            Regimen = comdes.ExecuteScalar
        End If

        Return Regimen
    End Function

    Private Function ComprobarExistenciaPunto(ByVal codigoPVYCR As String) As Boolean
        Dim comdes As SqlClient.SqlCommand = New SqlCommand("", conexion_destino)
        Dim Existe As Boolean = False
        If conexion_destino.State = ConnectionState.Closed Then conexion_destino.Open()
        comdes.CommandText = "SELECT COUNT(*) from PVYCR_ElementosMedida where codigoPVYCR='" & codigoPVYCR & "' AND idElementoMedida='Q01'"
        If Not comdes.ExecuteScalar Is Nothing Then
            If comdes.ExecuteScalar > 0 Then
                Existe = True
            End If
        End If
        Return Existe
    End Function

    Private Sub PasarAHistorico(ByVal dr As DataRow)
        'copiado a histórico tanto el punto en cuestión como los anteriores de esa hora
        'comprobar si existe
        da_origen.SelectCommand.CommandText = "SELECT * FROM SICA_DatosAcequias_Estadillo_Historico WHERE CodigoPVYCR = '" & dr("codigopvycr") & "' " & _
                                "AND Fecha_Medida = '" & dr("fechaCompleta") & "'"
        da_origen.Fill(dst_origen, "DatosAcequiasRepetidos")

        If dst_origen.Tables("DatosAcequiasRepetidos").Rows.Count = 0 Then
            da_origen.SelectCommand.CommandText = "select * from SICA_DatosAcequias_TopKapi where codigoPVYCR='" & dr("codigoPVYCR") & "' " & _
                    "and fecha > '" & DateAdd(DateInterval.Hour, -1, dr("fechaCompleta")) & "' and fecha < = '" & dr("fechaCompleta") & "'"
            da_origen.Fill(dst_origen, "TablaDatosCompletos")

            For i As Integer = 0 To dst_origen.Tables("TablaDatosCompletos").Rows.Count - 1
                With dst_origen.Tables("TablaDatosCompletos").Rows(i)
                    comando_origen.CommandText = "INSERT INTO SICA_DatosAcequias_Estadillo_Historico (CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, " & _
                        "Escala_M, Calado_M, RegimenCurva, NumeroParada, Caudal_M3S, TipoObtencionCaudal, Duda_Calidad, " & _
                        "Observaciones) VALUES (@CodigoPVYCR, @idElementoMedida, @Cod_Fuente_Dato, @Fecha_Medida, " & _
                        "@Escala_M, @Calado_M, @RegimenCurva, @NumeroParada, @Caudal_M3S, @TipoObtencionCaudal, @Duda_Calidad, " & _
                        "@Observaciones)"
                    comando_origen.Parameters.AddWithValue("@CodigoPVYCR", .Item("codigopvycr"))
                    comando_origen.Parameters.AddWithValue("@idElementoMedida", "Q01")
                    comando_origen.Parameters.AddWithValue("@Cod_Fuente_Dato", "13")
                    comando_origen.Parameters.AddWithValue("@Fecha_Medida", .Item("Fecha"))
                    comando_origen.Parameters.AddWithValue("@Escala_M", 0)
                    comando_origen.Parameters.AddWithValue("@Calado_M", .Item("Calado_M"))
                    comando_origen.Parameters.AddWithValue("@RegimenCurva", CalcularRegimenCurva(dr("codigopvycr"), .Item("fecha")))
                    comando_origen.Parameters.AddWithValue("@NumeroParada", "13") '?
                    comando_origen.Parameters.AddWithValue("@Caudal_M3S", .Item("Caudal"))
                    comando_origen.Parameters.AddWithValue("@TipoObtencionCaudal", "Q")
                    comando_origen.Parameters.AddWithValue("@Duda_Calidad", 0)
                    comando_origen.Parameters.AddWithValue("@Observaciones", "Inserción Automática NIVUS. Velocidad= " & .Item("Velocidad"))
                    comando_origen.ExecuteNonQuery()
                    comando_origen.Parameters.Clear()
                End With
            Next
            dst_origen.Tables("TablaDatosCompletos").Clear()
        End If
        dst_origen.Tables("DatosAcequiasRepetidos").Clear()

        'borrado: Borramos el registro en cuestión y los anteriores a ese en el mismo día.
        'caudales
        comando_origen.CommandText = "select * from SICA_TopKapi_Caudales where codigoPVYCR='" & dr("codigoPVYCR") & "' " & _
            "and dateadd(hour, cast(left(hora,2) as integer),dateadd(minute,cast(substring(hora,3,2) as integer),fecha))<='" & dr("fechaCompleta") & "'" & _
            "and dateadd(hour, cast(left(hora,2) as integer),dateadd(minute,cast(substring(hora,3,2) as integer),fecha))>'" & DateAdd(DateInterval.Hour, -1, dr("fechaCompleta")) & "'"
        comando_origen.ExecuteNonQuery()

        'niveles
        comando_origen.CommandText = "DELETE FROM SICA_TopKapi_Niveles WHERE codigopvycr = '" & dr("codigoPVYCR") & "' " & _
            "and dateadd(hour, cast(left(hora,2) as integer),dateadd(minute,cast(substring(hora,3,2) as integer),fecha))<='" & dr("fechaCompleta") & "'" & _
            "and dateadd(hour, cast(left(hora,2) as integer),dateadd(minute,cast(substring(hora,3,2) as integer),fecha))>'" & DateAdd(DateInterval.Hour, -1, dr("fechaCompleta")) & "'"
        comando_origen.ExecuteNonQuery()

        'velocidades
        comando_origen.CommandText = "DELETE FROM SICA_TopKapi_Veloccidades WHERE codigopvycr = '" & dr("codigoPVYCR") & "' " & _
            "and dateadd(hour, cast(left(hora,2) as integer),dateadd(minute,cast(substring(hora,3,2) as integer),fecha))<='" & dr("fechaCompleta") & "'" & _
            "and dateadd(hour, cast(left(hora,2) as integer),dateadd(minute,cast(substring(hora,3,2) as integer),fecha))>'" & DateAdd(DateInterval.Hour, -1, dr("fechaCompleta")) & "'"
        comando_origen.ExecuteNonQuery()
    End Sub

    Private Sub RecargarPuntos(ByVal codigoPVYCR As String, ByVal fecha As Date)
        lblFechaSelecc.Text = fecha

        pnlPuntos.Visible = False
        pnlPuntosFechas.Visible = False
        pnlPuntosFechasHoras.Visible = True
        pnlDetalle.Visible = False
        btnCancelar.Visible = True
        btnEstadillo.Visible = False
        btnProduccion.Visible = False
        crearDataSets(lblPuntoSelecc.Text, String.Format("{0:dd/MM/yyyy}", lblFechaSelecc.Text))

        lblNombreFecha.Text = lblPuntoSelecc.Text & " " & lblFechaSelecc.Text & ": SELECCIONE UNA FECHA"

        rptNivusDetalle.DataSource = dst_origen.Tables("TablaNivusDetalle")
        rptNivusDetalle.DataBind()
    End Sub
End Class
