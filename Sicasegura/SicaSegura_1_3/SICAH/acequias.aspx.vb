Imports System.Data
Imports GuarderiaFluvial
Imports System.Math

Partial Class SICAH_acequias
  Inherits System.Web.UI.Page

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daAcequias As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstAcequias As DataSet = New System.Data.DataSet()
    Dim daProbabilidadCurvas As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstProbabilidad As DataSet = New System.Data.DataSet()

    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

  Public numpaginas As Integer
  Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
  Dim sentenciaSel, sentenciaOrder As String
  Dim parfila As Integer = 0
    Dim contador As Integer = 0
    Dim objTrans As SqlClient.SqlTransaction
  
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

    Protected Function checkNombreMotor(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return " "
        Else
            Return elDataitem("DenominacionCauce")
        End If
    End Function
    Protected Function checkCodigoAcequia(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return " "
        Else
            lblidelemen.Text = elDataitem("idElementoMedida")
            Return elDataitem("CodigoPVYCR") & " / " & elDataitem("Cod_fuente_dato") & " / " & elDataitem("DenominacionPunto") & " / " & elDataitem("idElementoMedida")
        End If

    End Function
    Protected Function checkNumRegEstadilloA(ByVal elDataitem As DataRowView) As String
        If utiles.nullABlanco(elDataitem("CodigoPVYCR")) = "" Then
            Return 0
        Else
            Dim v_result As String
            daAcequias.SelectCommand.CommandText = "SELECT count(*) NumRegEstdilloM FROM PVYCR_DatosAcequias_Estadillo E " & _
                                                  "where E.CodigoPVYCR = '" & elDataitem("CodigoPVYCR") & "' And E.Cod_Fuente_Dato = '" & elDataitem("Cod_Fuente_Dato") & "' " & _
                                                    " and E.idElementoMedida = '" & elDataitem("idElementoMedida") & "'"

            daAcequias.Fill(dstAcequias, "TablaNumRegEstadilloM")
            v_result = dstAcequias.Tables("TablaNumRegEstadilloM").Rows((dstAcequias.Tables("TablaNumRegEstadilloM").Rows.Count - 1)).Item("NumRegEstdilloM").ToString
            Return v_result
        End If

    End Function

    Protected Function checkTipoCaudalC(ByVal elDataitem As DataRowView) As Boolean
        If utiles.nullABlanco(elDataitem("TipoObtencionCaudal")) = "C" Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Function checkTipoCaudalA(ByVal elDataitem As DataRowView) As Boolean
        If utiles.nullABlanco(elDataitem("TipoObtencionCaudal")) = "A" Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Function checkTipoCaudalF(ByVal elDataitem As DataRowView) As Boolean
        If utiles.nullABlanco(elDataitem("TipoObtencionCaudal")) = "F" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub calcularPags()
        Dim i As Integer
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim txtComando As String = ""
        txtComando = daAcequias.SelectCommand.CommandText
        Try
            txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
        Catch Exc As Exception
            Response.Write("Error: " & Exc.Message)
        End Try


        comando.CommandText = "select count(*) from (" & txtComando & ") dtable"
        'ncm Try
        '    If conexion.State = ConnectionState.Closed Then conexion.Open()
        'Catch exc As Exception
        '    Response.Redirect("../ErrorConexion.aspx")
        'End Try
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            'btnAceptarLectura.Attributes.Add("onclick", "mensaje();")
            'ncm 23/10/2008 comentado al agrupar las pestañas de preproducción en una sola
            'lblPestanyas.Text = genHTML.pestanyasMenu(3, "../", Page, Session("idPerfil"))
            lblPestanyas.Text = genHTML.pestanyasMenu(0, "../", Page, Session("idPerfil"), Session("usuarioReg"))

            If Request.QueryString("pag") <> "" Then
                lblPagina.Text = Request.QueryString("pag")
            Else
                lblPagina.Text = "1"
            End If

            sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto, c.DenominacionCauce " & _
                           "FROM PVYCR_DatosAcequias_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI "
            'ncm comentado el 25/02/2008 ya que ahora pueden 
            'and DM.Cod_Fuente_Dato = '05'"

            sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.Cod_Fuente_Dato "

            crearDataSets()

            rptAcequias.DataSource = dstAcequias.Tables("TablaAcequias")
            rptAcequias.DataBind()
            'lblNumpaginas.DataBind()

            'ncm añadimos el evento click al botón de curvas acequias para poder pasarle la global
            'btnCurvas.OnClientClick = "AbrirCurvas(" & Session("cadenacaudales") & "); return false;"
            'btnCurvas.OnClientClick = "AbrirCurvas('" & Session("cadenacaudales") & "','" & Session("codigoPVYCR") & "','" & Session("idElementoMedida") & "'); return false;"
            'btnCurvas.OnClientClick = "AbrirCurvas(); return false;"
        End If
    End Sub

    Private Sub crearDataSets()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daAcequias.SelectCommand.CommandText = sentenciaOrder
        daAcequias.Fill(dstAcequias, "TablaAcequias")
        'daAcequias.Fill(dstAcequias, (CInt(lblPagina.Text) - 1) * pageSize, pageSize, "TablaAcequias")
        'ncm comentado a petición del usuario el 25/08/2008
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
            sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto, c.DenominacionCauce " & _
                               "FROM PVYCR_DatosAcequias_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                               "WHERE DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                                "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI  "
            'ncm comentado el 25/02/2009 ya que ahora pueden dar lecturas de forma manual
            'and DM.Cod_Fuente_Dato = '05'"
            If txtFiltro.Text <> "" Then
                sentenciaSel &= " and DM.CodigoPVYCR like '%" & txtFiltro.Text & "%' "
            End If
            sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.Cod_Fuente_Dato "
        Else
            sentenciaOrder = Session("Ordenacion").ToString
        End If
        crearDataSets()
        rptAcequias.DataSource = dstAcequias.Tables("TablaAcequias")
        rptAcequias.DataBind()
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


            sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida,DM.Cod_Fuente_Dato, P.DenominacionPunto, c.DenominacionCauce " & _
                       "FROM PVYCR_DatosAcequias_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                       "WHERE DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                       "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI "
            'ncm comantado el 25/02/2009 
            'And DM.Cod_Fuente_Dato = '05'"
            If txtFiltro.Text <> "" Then
                sentenciaSel &= " and DM.CodigoPVYCR like '%" & txtFiltro.Text & "%' "
            End If
            sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.Cod_Fuente_Dato "
        Else
            sentenciaOrder = Session("Ordenacion").ToString
        End If

        crearDataSets()
        rptAcequias.DataSource = dstAcequias.Tables("TablaAcequias")
        rptAcequias.DataBind()
    End Sub
    Protected Sub PVYCRSeleccionada(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim parametros() As String = Split(CType(sender, LinkButton).CommandArgument, "#")
        lblidMotor.Text = parametros(0)
        pnlAcequias.Visible = False
        Panel1.Visible = False
        pnlEstadillo.Visible = True
        pnlPrincipal.Visible = True
        pnlEtiquetas.Visible = True
        pnlEstadistica.Visible = True
        pnlBotones.Visible = True
        'le pasamos el codfuente datos
        Session("cod_fuente_datos") = parametros(1)
        crearDataSetAEstadillo(Session("cod_fuente_datos"))
        CrearDatasetestadistica(Session("cod_fuente_datos"))

        lblNombrePVYCR.Text = lblidMotor.Text

        'lblNombrePVYCR.Text = "&nbsp;" & dstAcequias.Tables("TablaAcequias").Rows(0).Item("CodigoPVYCR")

        dstAcequias.Tables("TablaAcequias").DefaultView.Sort = "Fecha_Medida"
        rptAcequiasDetalle.DataSource = dstAcequias.Tables("TablaAcequias").DefaultView
        rptAcequiasDetalle.DataBind()

        rptAcequiasDetalle2.DataSource = dstAcequias.Tables("TablaAcequias2")
        Session("fila") = 0
        rptAcequiasDetalle2.DataBind()

        rptestadistica.DataSource = dstAcequias.Tables("TablaEstadistica")
        rptestadistica.DataBind()

        rptTotalC.DataSource = dstAcequias.Tables("TablaEstadisticaTOTALC")
        rptTotalC.DataBind()

        rptTotal.DataSource = dstAcequias.Tables("TablaEstadisticaTOTAL")
        rptTotal.DataBind()

        'If Not IsNothing(boton) The
        'boton.Visible = True
        'End If

    End Sub
    Private Sub crearDataSetAEstadillo(ByVal cod_fuente_datos As String)
        Dim vpos As Decimal
        Dim vcadena1, vcadena2 As String
        utiles.Comprobar_Conexion_BD(Page, conexion)
        vpos = lblidMotor.Text.IndexOf("/")

        vcadena1 = lblidMotor.Text.Substring(0, vpos - 1)
        'obtenemos el idelemento
        vcadena2 = lblidMotor.Text.Substring(lblidMotor.Text.Length - 3, 3)
        'obtenemos el codfuentedato



        daAcequias.SelectCommand.CommandText = "SELECT top 10 D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.TipoObtencionCaudal, T.Descripcion, " & _
                                           "D.Escala_M, D.Calado_M, D.Observaciones, D.RegimenCurva, D.Caudal_M3S, NumeroParada, D.Duda_Calidad " & _
                                           "FROM PVYCR_DatosAcequias D left outer join PVYCR_TiposObtencionCaudal T " & _
                                           "on (D.TipoObtencionCaudal = T.TipoObtencionCaudal  COLLATE Modern_Spanish_CI_AI ) " & _
                                           "where D.CodigoPVYCR = '" & vcadena1 & "' And D.Cod_Fuente_Dato = '" & cod_fuente_datos & "' " & _
                                           "and D.idElementoMedida = '" & vcadena2 & "' order by D.Fecha_Medida desc"
        'D.Cod_Fuente_Dato = '" & cod_fuente_datos & "' " & _

        daAcequias.Fill(dstAcequias, "TablaAcequias")
        'esta tiene el login
    daAcequias.SelectCommand.CommandText = "SELECT E.idElementoMedida, E.CodigoPVYCR, E.Cod_Fuente_Dato, E.Fecha_Medida, T.TipoObtencionCaudal, T.descripcion, " & _
                                           "E.Escala_M, E.Calado_M, E.Observaciones,Tiempo_SG ,V11_RPM,V12_RPM , V21_RPM, V22_RPM, V31_RPM,V32_RPM, " & _
                                           "V11_MS,V12_MS , V21_MS, V22_MS, V31_MS, V32_MS, A1_M,A2_M,A3_M,B1_M,B2_M,B3_M,B4_M,H1_M,H2_M,H12_M,H23_M,H34_M,Offset_M, " & _
                                           "'0' as RegimenCurva, '' as Caudal_M3S, '' as NumeroParada, '' as Duda_Calidad, " & _
                                           "P.FactorFlotador,P.LongitudFlotador, '1' as B, P.diametro_mm, E.login, " & _
                                           "CASE t.TipoObtencionCaudal WHEN 'C' THEN '1' WHEN 'A' THEN '2' WHEN 'F' THEN '3' ELSE '4' END as orden " & _
                                          "FROM PVYCR_DatosAcequias_Estadillo E, PVYCR_TiposObtencionCaudal T, PVYCR_Puntos P " & _
                                          "where E.idElementoMedida = '" & vcadena2 & "' and E.CodigoPVYCR = '" & vcadena1 & "' And E.Cod_Fuente_Dato = '" & cod_fuente_datos & "' " & _
                                          "and E.CodigoPVYCR = P.CodigoPVYCR and t.TipoObtencionCaudal in ('C','F','A')" & _
                                          "order by E.Fecha_medida, orden "
        'IPIM 26/05/2009: Modificado esto '1' as B, para que siga cogiendo la B y no dé error, pero hay que quitar este campo cuanto antes, pq no 
        'existe en la base de datos.

        'daAcequias.SelectCommand.CommandText = "SELECT E.idElementoMedida, E.CodigoPVYCR, E.Cod_Fuente_Dato, E.Fecha_Medida, T.TipoObtencionCaudal, T.descripcion, " & _
        '                                            "E.Escala_M, E.Calado_M, E.Observaciones,Tiempo_SG ,V11_RPM,V12_RPM , V21_RPM, V22_RPM, V31_RPM,V32_RPM, " & _
        '                                            "V11_MS,V12_MS , V21_MS, V22_MS, V31_MS, V32_MS, A1_M,A2_M,B1_M,B2_M,B3_M,H1_M,H2_M,Offset_M, " & _
        '                                            "'0' as RegimenCurva, '' as Caudal_M3S, '' as NumeroParada, '' as Duda_Calidad, " & _
        '                                            "P.FactorFlotador,P.LongitudFlotador, P.B, P.diametro_mm " & _
        '                                           "FROM PVYCR_DatosAcequias_Estadillo E, PVYCR_TiposObtencionCaudal T, PVYCR_Puntos P " & _
        '                                           "where E.idElementoMedida = '" & vcadena2 & "' and E.CodigoPVYCR = '" & vcadena1 & "' And E.Cod_Fuente_Dato = '" & lblFuenteDato.Text & "' " & _
        '                                           "and E.CodigoPVYCR = P.CodigoPVYCR and t.TipoObtencionCaudal in ('C','F','A')" & _
        '                                           "order by E.Fecha_medida, descripcion desc "

        daAcequias.Fill(dstAcequias, "TablaAcequias2")
    'Ordenamos el dataset para que en primer lugar salgan los registros C,A,F

        'guardamos el valor del codigo PVYCR en una variable de session para luego recuperarla
        'y utilizarla para obtener datos para dibujar el gráfico.
        Session("codigoPVYCR") = vcadena1
        Session("idElementoMedida") = vcadena2



        'Response.Write("estadistica: " & dstAcequias.Tables("TablaEstadistica").Rows(0).Item("Registros"))
    End Sub
    Private Sub CrearDatasetestadistica(ByVal cod_fuente_dato As String)
        Dim vpos, i, v_por, v_reg, v_total, v_totalC, v_totalF, v_totalM, v_totalsinnulos As Decimal
        Dim vcadena1, vcadena2 As String
        Dim columna1 As DataColumn = New DataColumn("Porcentaje")

        utiles.Comprobar_Conexion_BD(Page, conexion)
        vpos = lblidMotor.Text.IndexOf("/")
        vcadena1 = lblidMotor.Text.Substring(0, vpos - 1)
        vcadena2 = lblidMotor.Text.Substring(lblidMotor.Text.Length - 3, 3)

        daAcequias.SelectCommand.CommandText = "SELECT CodigoPVYCR,REGIMEN,FECHA_INICIO_USO,Cod_Curva,En_Activo, " & _
                                       "FECHA_FIN_USO,Comentarios,Probabilidad " & _
                                       "FROM PVYCR_CurvasAcequias " & _
                                       "WHERE codigoPVYCR = '" & vcadena1 & "' and idElementoMedida = '" & vcadena2 & "' and FECHA_FIN_USO >= getdate() " & _
                                       "order by REGIMEN DESC "

        daAcequias.Fill(dstAcequias, "TablaCurvasAcequias")

        'NCM Antes únicamente tipoobtencioncaudal = C desde el 11/07/2008 C, F y A por ètición usuario
        daAcequias.SelectCommand.CommandText = "SELECT COUNT('c.REGIMEN') as Registros,c.[REGIMEN] as REGIMEN " & _
                                    "FROM dbo.PVYCR_DatosAcequias A left outer join dbo.PVYCR_CurvasAcequias C on " & _
                                    "(a.CodigoPVYCR = C.CodigoPVYCR and  a.idElementoMedida= c.idElementoMedida and a.regimenCurva = c.regimen)" & _
                                    "WHERE a.codigoPVYCR = '" & vcadena1 & "' and a.idElementoMedida = '" & vcadena2 & "' and a.cod_fuente_dato ='" & cod_fuente_dato & "' " & _
                                    "and a.TipoObtencionCaudal in ('C','F','A') " & _
                                    "group by c.REGIMEN " & _
                                    "order by regimen"
        daAcequias.Fill(dstAcequias, "TablaEstadistica")


        daAcequias.SelectCommand.CommandText = "SELECT COUNT(*) as RegistrosTTipoCaudal, 'C' tipocaudal " & _
                                               "FROM dbo.PVYCR_DatosAcequias A " & _
                                               "WHERE codigoPVYCR = '" & vcadena1 & "' and a.cod_fuente_dato ='" & cod_fuente_dato & "' " & _
                                               "and a.TipoObtencionCaudal = 'C' " & _
                                               "UNION " & _
                                               "SELECT COUNT(*) as RegistrosTTipoCaudal, 'F' tipocaudal " & _
                                               "FROM dbo.PVYCR_DatosAcequias A " & _
                                               "WHERE codigoPVYCR = '" & vcadena1 & "' and a.cod_fuente_dato ='" & cod_fuente_dato & "' " & _
                                               "and a.TipoObtencionCaudal = 'F' " & _
                                               "UNION " & _
                                               "SELECT COUNT(*) as RegistrosTTipoCaudal, 'A' tipocaudal " & _
                                               "FROM dbo.PVYCR_DatosAcequias A " & _
                                               "WHERE codigoPVYCR = '" & vcadena1 & "' and a.cod_fuente_dato ='" & cod_fuente_dato & "' " & _
                                               "and a.TipoObtencionCaudal = 'A' "


        daAcequias.Fill(dstAcequias, "TablaEstadisticaTOTALC")
        dstAcequias.Tables("TablaEstadisticaTOTALC").DefaultView.Sort = "tipocaudal ASC"

        v_totalC = dstAcequias.Tables("TablaEstadisticaTOTALC").DefaultView(0).Item("RegistrosTTipoCaudal")
        v_totalF = dstAcequias.Tables("TablaEstadisticaTOTALC").DefaultView(1).Item("RegistrosTTipoCaudal")
        v_totalM = dstAcequias.Tables("TablaEstadisticaTOTALC").DefaultView(2).Item("RegistrosTTipoCaudal")

        daAcequias.SelectCommand.CommandText = "SELECT COUNT(*) as RegistrosT " & _
                                               "FROM dbo.PVYCR_DatosAcequias A " & _
                                               "WHERE codigoPVYCR = '" & vcadena1 & "' and a.cod_fuente_dato ='" & cod_fuente_dato & "' " & _
                                               "and A.TipoObtencionCaudal in ('C','F','A')"

        daAcequias.Fill(dstAcequias, "TablaEstadisticaTOTAL")

        'vamos a añadir la columna porcentaje al dataset para calcular los porcentajes y los totales del cuadro resumen
        dstAcequias.Tables("TablaEstadistica").Columns.Add("porcentaje")
        'este total se calcula para poder obtener los porcentajes sin los valores que no tienen régimen curva
        If dstAcequias.Tables("TablaEstadistica").Rows.Count = 0 Then
            v_totalsinnulos = v_totalC - 0
            v_total = 0
            dstAcequias.Tables("TablaEstadisticaTOTAL").Rows(0).Item("RegistrosT") = v_total
        Else
            If dstAcequias.Tables("TablaEstadistica").Rows(0).Item("REGIMEN") Is System.DBNull.Value Then
                'v_totalsinnulos = v_totalC - utiles.nullACero(dstAcequias.Tables("TablaEstadistica").Rows(0).Item("Registros"))
                v_totalsinnulos = dstAcequias.Tables("TablaEstadisticaTOTAL").Rows(0).Item("RegistrosT") - utiles.nullACero(dstAcequias.Tables("TablaEstadistica").Rows(0).Item("Registros"))
                v_total = dstAcequias.Tables("TablaEstadisticaTOTAL").Rows(0).Item("RegistrosT") + utiles.nullACero(dstAcequias.Tables("TablaEstadistica").Rows(0).Item("Registros"))
                dstAcequias.Tables("TablaEstadisticaTOTAL").Rows(0).Item("RegistrosT") = v_total
            Else
                v_totalsinnulos = dstAcequias.Tables("TablaEstadisticaTOTAL").Rows(0).Item("RegistrosT") ' v_totalC
                v_total = dstAcequias.Tables("TablaEstadisticaTOTAL").Rows(0).Item("RegistrosT")
                dstAcequias.Tables("TablaEstadisticaTOTAL").Rows(0).Item("RegistrosT") = v_total
            End If

        End If

        'calculamos los porcentajes sin tener en cuenta los que no tienen régimen curva
        For i = 0 To Convert.ToInt16(dstAcequias.Tables("TablaEstadistica").Rows.Count) - 1
            If dstAcequias.Tables("TablaEstadistica").Rows(i).Item("REGIMEN") IsNot System.DBNull.Value Then
                v_reg = dstAcequias.Tables("TablaEstadistica").Rows(i).Item("Registros")
                If v_totalsinnulos = 0 Then
                    v_por = 0
                    dstAcequias.Tables("TablaEstadistica").Rows(i).Item("Porcentaje") = Format(v_por, "##0.##") & "%"
                Else
                    v_por = (v_reg * 100) / v_totalsinnulos 'v_totalC
                    dstAcequias.Tables("TablaEstadistica").Rows(i).Item("Porcentaje") = Format(v_por, "##0.##") & "%"
                End If
            End If
        Next

    End Sub
    Protected Sub btnAceptarLectura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarLectura.Click

        Dim i, historico As Integer
        Dim dr As DataRowView
        Dim v_calado, v_caudal, v_escala, v_V11, v_V12, v_V21, v_V22, v_V31, v_V32, v_tiempo, v_dudacalidad, v_NParada As Decimal
        Dim v_CodigoPVYCR, v_Fecha, v_observa, v_TOCaudal, v_RCurva, v_idElemen, v_codfuentedatos As String
        Dim elPanel As Panel
        Dim elradiobtnA, elradiobtnR As RadioButton
        Dim visibleF, visibleC, visibleA As Boolean
        Dim v_items As Integer = rptAcequiasDetalle2.Items.Count
        Dim v_mensaje As String = ""

        For i = 0 To v_items - 1
            v_caudal = 0
            'Evaluamos sólo los checkbox de aceptar y rechazar cuando pasamos por un item
            'de tipo C:
            If Not IsNothing(rptAcequiasDetalle2.Items(i).FindControl("pnlC")) Then
                If rptAcequiasDetalle2.Items(i).FindControl("pnlC").Visible Then
                    elradiobtnA = rptAcequiasDetalle2.Items(i).FindControl("rbSelCalculoA")
                    elradiobtnR = rptAcequiasDetalle2.Items(i).FindControl("rbSelCalculoR")
                    historico = 0
                End If
            End If
            Try
                utiles.Comprobar_Conexion_BD(Page, conexion)
                objTrans = conexion.BeginTransaction()
                comando.Transaction = objTrans
                ' si está marcado el check aceptar, tendremos que comprobar primero si
                ' han aceptado algún cálculo, si no ha sido así pasaremos al siguiente registro
                If elradiobtnA.Checked = True Then
                    elPanel = Nothing
                    If Not IsNothing(rptAcequiasDetalle2.Items(i).FindControl("pnlC")) Then
                        elPanel = rptAcequiasDetalle2.Items(i).FindControl("pnlC")
                    ElseIf Not IsNothing(rptAcequiasDetalle2.Items(i).FindControl("pnlA")) Then
                        elPanel = rptAcequiasDetalle2.Items(i).FindControl("pnlA")
                    ElseIf Not IsNothing(rptAcequiasDetalle2.Items(i).FindControl("pnlF")) Then
                        elPanel = rptAcequiasDetalle2.Items(i).FindControl("pnlF")
                    End If

                    If (CType(elPanel.FindControl("cbA"), CheckBox).Checked Or CType(elPanel.FindControl("cbC"), CheckBox).Checked Or CType(elPanel.FindControl("cbF"), CheckBox).Checked) Then
                        v_CodigoPVYCR = CType(rptAcequiasDetalle2.Items(i).FindControl("txtCodigoPVYCR"), TextBox).Text
                        v_Fecha = Convert.ToString(CType(rptAcequiasDetalle2.Items(i).FindControl("txtFecha"), TextBox).Text)
                        'v_Hora = Convert.ToString(CType(rptAcequiasDetalle2.Items(i).FindControl("txtHora"), TextBox).Text)
                        v_idElemen = Convert.ToString(CType(rptAcequiasDetalle2.Items(i).FindControl("txtidElementoMedida"), TextBox).Text)
                        v_codfuentedatos = CType(rptAcequiasDetalle2.Items(i).FindControl("txtcod_fuente_dato"), TextBox).Text
                        ' el (i - 8i mod 3) es para coger el valor de los datos del panel c ya que el resto están ocultos y se pierde el valor
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtEscala"), TextBox).Text) = "" Then
                            v_escala = -9999
                        Else
                            v_escala = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtEscala"), TextBox).Text
                        End If

                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtCalado"), TextBox).Text) = "" Then
                            v_calado = -9999
                        Else
                            v_calado = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtCalado"), TextBox).Text
                        End If

                        v_observa = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtObservaciones"), TextBox).Text

                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtTiempo"), TextBox).Text) = "" Then
                            v_tiempo = -9999
                        Else
                            v_tiempo = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtTiempo"), TextBox).Text
                        End If

                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV11MS"), TextBox).Text) = "" Then
                            v_V11 = -9999
                        Else
                            v_V11 = CType(rptAcequiasDetalle2.Items(i).FindControl("txtV11MS"), TextBox).Text
                        End If

                        'v_V11 = Convert.ToDecimal(0 & CType(rptAcequiasDetalle2.Items(i).FindControl("lblV11MS"), Label).Text)
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV12MS"), TextBox).Text) = "" Then
                            v_V12 = -9999
                        Else
                            v_V12 = CType(rptAcequiasDetalle2.Items(i).FindControl("txtV12MS"), TextBox).Text
                        End If
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV21MS"), TextBox).Text) = "" Then
                            v_V21 = -9999
                        Else
                            v_V21 = CType(rptAcequiasDetalle2.Items(i).FindControl("txtV21MS"), TextBox).Text
                        End If
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV22MS"), TextBox).Text) = "" Then
                            v_V22 = -9999
                        Else
                            v_V22 = CType(rptAcequiasDetalle2.Items(i).FindControl("txtV22MS"), TextBox).Text
                        End If
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV31MS"), TextBox).Text) = "" Then
                            v_V31 = -9999
                        Else
                            v_V31 = CType(rptAcequiasDetalle2.Items(i).FindControl("txtV31MS"), TextBox).Text
                        End If
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV32MS"), TextBox).Text) = "" Then
                            v_V32 = -9999
                        Else
                            v_V32 = CType(rptAcequiasDetalle2.Items(i).FindControl("txtV32MS"), TextBox).Text
                        End If

                        'Comprobamos si el RCurva no tiene valor, mostramos un sms al usuario.
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("ddlRegimenCurva"), DropDownList).SelectedItem.Text) = "" Then
                            JavaScript.Alert(Me, "No existe ningún Régimen Curva en activo")
                            Exit Sub
                        Else
                            v_RCurva = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("ddlRegimenCurva"), DropDownList).SelectedItem.Text
                        End If

                        'If v_RCurva = "" Then
                        '    JavaScript.Alert(Me, "No existe ningún Régimen Curva en activo")
                        '    Exit Sub
                        'End If

                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtNParada"), TextBox).Text) = "" Then
                            v_NParada = -9999
                        Else
                            v_NParada = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtNParada"), TextBox).Text
                        End If
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtDudaCalidad"), TextBox).Text) = "" Then
                            v_dudacalidad = -9999
                        Else
                            v_dudacalidad = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtDudaCalidad"), TextBox).Text
                        End If


                        ' si el usuario acepta los calculos, tendremos que comprobar si los tipos de caudal están o no
                        ' aceptados viendo el valor de los radio buttons.
                        ' Si el valor del radio button es aceptada se insertará en el histórico, en datos acequias y se
                        ' borrará del estadillo, si es rechazado no se hará nada
                        If i Mod 3 = 0 Then
                            If CType(rptAcequiasDetalle2.Items(i).FindControl("cbC"), CheckBox).Checked Then
                                If CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("cbestimadaC"), CheckBox).Checked = False Then
                                    v_TOCaudal = "C"
                                Else
                                    v_TOCaudal = "E"
                                End If

                                Insertar_Acequias(i, "C", v_TOCaudal, v_idElemen, v_CodigoPVYCR, v_Fecha, v_observa, v_calado, v_escala, v_V11, v_V12, v_V21, v_V22, v_V31, v_V32, v_tiempo, v_dudacalidad, v_RCurva, v_NParada, v_codfuentedatos)
                            End If
                        End If


                        'If Not IsNothing(elPanel.FindControl("cbA")) Then
                        '    Response.Write("control : " + encontrarControlRecursivo(elPanel, "cbA"))
                        'End If
                        If i Mod 3 = 1 Then
                            If CType(elPanel.FindControl("cbA"), CheckBox).Checked Then

                                If CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("cbestimadaC"), CheckBox).Checked = False Then
                                    v_TOCaudal = "A"
                                Else
                                    v_TOCaudal = "E"
                                End If
                                Insertar_Acequias(i, "A", v_TOCaudal, v_idElemen, v_CodigoPVYCR, v_Fecha, v_observa, v_calado, v_escala, v_V11, v_V12, v_V21, v_V22, v_V31, v_V32, v_tiempo, v_dudacalidad, v_RCurva, v_NParada, v_codfuentedatos)
                            End If
                        End If
                        If i Mod 3 = 2 Then
                            If CType(elPanel.FindControl("cbF"), CheckBox).Checked Then
                                If CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("cbestimadaC"), CheckBox).Checked = False Then
                                    v_TOCaudal = "F"
                                Else
                                    v_TOCaudal = "E"
                                End If
                                Insertar_Acequias(i, "F", v_TOCaudal, v_idElemen, v_CodigoPVYCR, v_Fecha, v_observa, v_calado, _
                                      v_escala, v_V11, v_V12, v_V21, v_V22, v_V31, _
                                     v_V32, v_tiempo, v_dudacalidad, v_RCurva, v_NParada, v_codfuentedatos)
                            End If
                        End If
                        ' Para que únicamente se inserte una vez en el histírico utilizaremos la variable historico
                        historico &= historico + 1
                        If historico = 1 Then
                            ' insertamos en el histórico únicamente un registro
                            insertar_acequias_h(v_idElemen, v_CodigoPVYCR, v_Fecha, v_observa, v_calado, v_escala, v_V11, v_V12, v_V21, v_V22, v_V31, v_V32, v_tiempo, CType(rptAcequiasDetalle2.Items(i).FindControl("txtlogin"), TextBox).Text, v_codfuentedatos)
                            'insertar_acequias_h(v_idElemen, v_CodigoPVYCR, v_Fecha, v_observa, v_calado, v_escala, v_V11, v_V12, v_V21, v_V22, v_V31, v_V32, v_tiempo)
                        End If
                        '    End If
                        'End If

                    End If 'algún cálculo marcado

                ElseIf elradiobtnR.Checked = True Then
                    'Si el cálculo está rechazado, no miraremos el valor del radio button, directamente se insertarán en
                    ' el histórico y se borrarán del estadillo
                    insertar_rechazadas(i)

                    'deschekeamos el registro
                    elradiobtnR.Checked = False

                End If

                objTrans.Commit()
                'conexion.Close()
            Catch Exc As System.Data.SqlClient.SqlException
                objTrans.Rollback()
                'PARA QUE EL MENSAJE NO SE REPITA TRES VECES PONEMOS EL IF
                If i Mod 3 = 0 Then
                    ' si falla por clave primaria
                    If Exc.Number = 2627 Then
                        v_mensaje = v_mensaje & "No se puede aceptar/rechazar la lectura con fecha " & Convert.ToString(CType(rptAcequiasDetalle2.Items(i).FindControl("txtFecha"), TextBox).Text) & " porque ya existe" & " \n"
                    Else
                        v_mensaje = v_mensaje & "Se ha producido un error al intentar insertar datos" & " \n"
                    End If
                End If

            End Try
        Next
        If v_mensaje <> "" Then
            JavaScript.Alert(Page, v_mensaje)
        End If

        v_codfuentedatos = CType(rptAcequiasDetalle2.Items(0).FindControl("txtcod_fuente_dato"), TextBox).Text
        crearDataSetAEstadillo(v_codfuentedatos)
        CrearDatasetestadistica(v_codfuentedatos)

        dstAcequias.Tables("TablaAcequias").DefaultView.Sort = "fecha_medida "
        rptAcequiasDetalle.DataSource = dstAcequias.Tables("TablaAcequias")

        rptAcequiasDetalle.DataBind()

        rptAcequiasDetalle2.DataSource = dstAcequias.Tables("TablaAcequias2")
        Session("fila") = 0
        rptAcequiasDetalle2.DataBind()

        rptestadistica.DataSource = dstAcequias.Tables("TablaEstadistica")
        rptestadistica.DataBind()

        rptTotalC.DataSource = dstAcequias.Tables("TablaEstadisticaTOTALC")
        rptTotalC.DataBind()

        rptTotal.DataSource = dstAcequias.Tables("TablaEstadisticaTOTAL")
        rptTotal.DataBind()


    End Sub

    Protected Sub btnCancelarLectura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarLectura.Click
        lblidMotor.Text = ""
        pnlAcequias.Visible = True
        'ncm 25/08/2008 paginacion
        Panel1.Visible = False
        pnlEstadillo.Visible = False
        pnlPrincipal.Visible = False
        pnlEtiquetas.Visible = False
        pnlEstadistica.Visible = False
        pnlBotones.Visible = False
    End Sub

    Protected Sub rptAcequiasDetalle2_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptAcequiasDetalle2.ItemCreated
        ' solo funciona si los items son los del cuerpo, sino da error, por eso ponemos el IF
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim ddl As DropDownList = Nothing

            ddl = CType(e.Item.FindControl("ddlRegimenCurva"), DropDownList)
            ddl.Items.Clear()
            ddl.DataSource = dstAcequias.Tables("TablaCurvasAcequias")
            'ddl.DataValueField = "Cod_Curva"
            ddl.DataValueField = "Cod_curva"
            ddl.DataTextField = "Regimen"
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("[Seleccionar]", ""))
            Formateo_controles_cliente(e)

        End If

    End Sub
    Public Sub SeleccionarRegimenCurva(ByVal sender As Object, ByVal e As System.EventArgs)
        'Response.Write(sender.GetType.ToString())
        Dim valor As String
        valor = CType(sender, DropDownList).SelectedValue
    End Sub

    Protected Sub btnCalcular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalcular.Click
        Dim vpos As Decimal
        Dim codigoPVYCR, idElemento As String
        utiles.Comprobar_Conexion_BD(Page, conexion)
        vpos = lblidMotor.Text.IndexOf("/")

        codigoPVYCR = lblidMotor.Text.Substring(0, vpos - 1)
        'obtenemos el idelemento
        idElemento = lblidMotor.Text.Substring(lblidMotor.Text.Length - 3, 3)

        crearDataSetAEstadillo(Session("cod_fuente_datos"))
        Dim vectorLecturas As String = ""
        Dim v_items As Integer = rptAcequiasDetalle2.Items.Count
        Dim i As Integer
        Dim mensaje As String = ""
        Dim ddl As DropDownList = Nothing
        'caudales
        Dim caudalC As String = Nothing
        Dim caudalA As String = Nothing
        Dim caudalF As String = Nothing
        Dim caudalC_ant As String = Nothing
        Dim caudalA_ant As String = Nothing
        Dim caudalF_ant As String = Nothing

        ' superficies, alturas, velocidades, etc
        Dim superTM As String = Nothing
        Dim altM As String = Nothing
        Dim superFM As String = Nothing
        Dim superRM As String = Nothing
        Dim velDM As String = Nothing
        Dim superTF As String = Nothing
        Dim altF As String = Nothing
        Dim superFF As String = Nothing
        Dim superRF As String = Nothing
        Dim velF As String = Nothing
        Dim cbC As Boolean = False
        Dim cbA As Boolean = False
        Dim cbF As Boolean = False
        For i = 0 To v_items - 1
            cbC = False
            cbA = False
            cbF = False
            ddl = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("ddlRegimenCurva"), DropDownList)
            Dim estimadaC As Boolean = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("cbestimadaC"), CheckBox).Checked
            Dim calado As String = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtCalado"), TextBox).Text
            Dim esclado As String = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtEscala"), TextBox).Text
            Dim tiempo As String = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtTiempo"), TextBox).Text
            Dim v11 As String = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV11MS"), TextBox).Text
            Dim v12 As String = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV12MS"), TextBox).Text
            Dim v21 As String = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV21MS"), TextBox).Text
            Dim v22 As String = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV22MS"), TextBox).Text
            Dim v31 As String = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV31MS"), TextBox).Text
            Dim v32 As String = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV32MS"), TextBox).Text
            Dim elcbRC As CheckBox = rptAcequiasDetalle2.Items(i).FindControl("cbregimenCurva")
            Dim color As String = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtColor"), TextBox).CssClass

            'llamamos a la función que realizará todos los cálculos
            SICA_FuncionesCalcCaudal.CalcularCaudal_final(codigoPVYCR, idElemento, Session("cod_fuente_datos"), dstAcequias, i, _
            estimadaC, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtCalado"), TextBox).Text, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtEscala"), TextBox).Text, _
            ddl, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtTiempo"), TextBox).Text, _
             CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV11MS"), TextBox).Text, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV12MS"), TextBox).Text, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV21MS"), TextBox).Text, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV22MS"), TextBox).Text, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV31MS"), TextBox).Text, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtV32MS"), TextBox).Text, Page, _
            superTM, _
            altM, _
            superFM, _
            superRM, _
            velDM, _
            superTF, _
            altF, _
            superFF, _
            superRF, _
            velF, _
            cbC, _
            cbA, _
            cbF, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtColor"), TextBox).CssClass, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("rbSelCalculoR"), RadioButton).Checked, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("rbSelCalculoA"), RadioButton).Checked, _
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtTOCaudalC"), TextBox).Text, _
            "A", _
            CType(rptAcequiasDetalle2.Items(i).FindControl("txtTOCaudalF"), TextBox).Text, _
            mensaje, _
            caudalC, _
            caudalA, _
            caudalF, _
            elcbRC.Checked, _
            caudalC_ant, _
            caudalA_ant, _
            caudalF_ant)
            'rellenamos los valores de pantalla
            If i - 1 > 0 Then
                'obtenemos los valores para A
                CType(rptAcequiasDetalle2.Items(i - 1).FindControl("txtCaudalA"), TextBox).Text = caudalA
                CType(rptAcequiasDetalle2.Items(i - 1).FindControl("txtsuperficieTM"), TextBox).Text = superTM
                CType(rptAcequiasDetalle2.Items(i - 1).FindControl("txtaltM"), TextBox).Text = altM
                CType(rptAcequiasDetalle2.Items(i - 1).FindControl("txtsuperficieFM"), TextBox).Text = superFM
                CType(rptAcequiasDetalle2.Items(i - 1).FindControl("txtsuperficieRM"), TextBox).Text = superRM
                CType(rptAcequiasDetalle2.Items(i - 1).FindControl("txtvelocidadM"), TextBox).Text = velDM
                CType(rptAcequiasDetalle2.Items(i - 1).FindControl("cbA"), CheckBox).Checked = cbA
            End If
            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtCaudalC"), TextBox).Text = caudalC
            CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudalF"), TextBox).Text = caudalF
            CType(rptAcequiasDetalle2.Items(i).FindControl("txtsuperficieTF"), TextBox).Text = superTF
            CType(rptAcequiasDetalle2.Items(i).FindControl("txtaltF"), TextBox).Text = altF
            CType(rptAcequiasDetalle2.Items(i).FindControl("txtsuperficieFF"), TextBox).Text = superFF
            CType(rptAcequiasDetalle2.Items(i).FindControl("txtsuperficieRF"), TextBox).Text = superRF
            CType(rptAcequiasDetalle2.Items(i).FindControl("txtvelocidadF"), TextBox).Text = velF

            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("cbC"), CheckBox).Checked = cbC
            CType(rptAcequiasDetalle2.Items(i).FindControl("cbF"), CheckBox).Checked = cbF

            CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("ddlRegimenCurva"), DropDownList).SelectedValue = ddl.SelectedValue
        Next
        'mostramos los posibles mensajes
        If mensaje <> "" Then
            JavaScript.Alert(Me, mensaje)
        End If
    End Sub

    Protected Sub Insertar_Acequias(ByVal i As Integer, ByVal tipocalculo As String, ByVal tipocaudal As String, ByVal idElemen As String, ByVal CodigoPVYCR As String, ByVal Fecha As String, ByVal observa As String, ByVal calado As Decimal, ByVal escala As Decimal, ByVal V11 As Decimal, ByVal V12 As Decimal, ByVal V21 As Decimal, ByVal V22 As Decimal, ByVal V31 As Decimal, ByVal V32 As Decimal, ByVal tiempo As Decimal, ByVal dudacalidad As Decimal, ByVal RCurva As String, ByVal NParada As Decimal, ByVal v_codfuentedatos As String)
        'Calcularemos el caudal
        Dim ddl As DropDownList = Nothing

        'v_caudal = CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudal" & tipocaudal), TextBox).Text
        'v_TOCaudal = tipocaudal 'CType(rptAcequiasDetalle2.Items(i).FindControl("txtTOCaudal" & tipocaudal), TextBox).Text
        'v_RCurva = CType(rptAcequiasDetalle2.Items(i).FindControl("ddlRegimenCurva"), DropDownList).SelectedValue
        'v_NParada = CType(rptAcequiasDetalle2.Items(i).FindControl("txtNParada"), TextBox).Text
        'v_dudacalidad = CType(rptAcequiasDetalle2.Items(i).FindControl("txtDudaCalidad"), TextBox).Text

        'Try

        comando.CommandText = "INSERT INTO PVYCR_DatosAcequias([CodigoPVYCR],[Cod_Fuente_Dato],[Fecha_medida] " & _
                              ",idelementoMedida,[Escala_M],[Calado_M],[Observaciones],[TipoObtencionCaudal] " & _
                              ",RegimenCurva,NumeroParada,Caudal_M3S, DUDA_CALIDAD) " & _
                              "values(@CodigoPVYCR,@cod_fuente_datos,@Fecha_medida " & _
                              ",@idelementoMedida,@Escala_M,@Calado_M,@Observaciones,@TipoObtencionCaudal, " & _
                              "@RegimenCurva,@NumeroParada,@Caudal_M3S, @DUDA_CALIDAD) "
        comando.Parameters.Clear()
        comando.Parameters.AddWithValue("@CodigoPVYCR", (CodigoPVYCR))
        comando.Parameters.AddWithValue("@Fecha_medida", Fecha)
        comando.Parameters.AddWithValue("@idElementoMedida", idElemen)
        comando.Parameters.AddWithValue("@cod_fuente_datos", v_codfuentedatos)

        'comando.Parameters.AddWithValue("Hora", Fecha & " " & Hora)

        ' si las variables tienen un -9999 es porque eran nulas y se insertarán como nulas, sino insertaremos el valor que tengan
        If escala = -9999 Then
            comando.Parameters.AddWithValue("@Escala_M", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@Escala_M", Replace(Replace(escala, ".", ""), ",", "."))
        End If
        If calado = -9999 Then
            comando.Parameters.AddWithValue("@Calado_M", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@Calado_M", Replace(Replace(calado, ".", ""), ",", "."))
        End If
        comando.Parameters.AddWithValue("@Observaciones", observa)
        comando.Parameters.AddWithValue("@TipoObtencionCaudal", tipocaudal)
        comando.Parameters.AddWithValue("@RegimenCurva", RCurva)

        If NParada = -9999 Then
            comando.Parameters.AddWithValue("@NumeroParada", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@NumeroParada", Replace(NParada, ".", ""))
        End If
        'comando.Parameters.AddWithValue("@Caudal_M3S", 150)
        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudal" & tipocalculo), TextBox).Text) = "" Then
            comando.Parameters.AddWithValue("@Caudal_M3S", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@Caudal_M3S", Replace(Replace(CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudal" & tipocalculo), TextBox).Text, ".", ""), ",", "."))
        End If

        If dudacalidad = -9999 Then
            comando.Parameters.AddWithValue("@DUDA_CALIDAD", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@DUDA_CALIDAD", Replace(dudacalidad, ".", ""))
        End If

        comando.ExecuteNonQuery()

        'Catch Exc As ConstraintException
        '    Response.Write("Error: " & Exc.Message)
        'End Try
    End Sub
    Protected Sub insertar_acequias_h(ByVal idelemen As String, ByVal CodigoPVYCR As String, ByVal Fecha As String, ByVal observa As String, ByVal calado As Decimal, ByVal escala As Decimal, ByVal V11 As Decimal, ByVal V12 As Decimal, ByVal V21 As Decimal, ByVal V22 As Decimal, ByVal V31 As Decimal, ByVal V32 As Decimal, ByVal tiempo As Decimal, ByVal login As String, ByVal v_codfuentedatos As String)
        'Protected Sub insertar_acequias_h(ByVal idelemen As String, ByVal CodigoPVYCR As String, ByVal Fecha As String, ByVal observa As String, ByVal calado As Decimal, ByVal escala As Decimal, ByVal V11 As Decimal, ByVal V12 As Decimal, ByVal V21 As Decimal, ByVal V22 As Decimal, ByVal V31 As Decimal, ByVal V32 As Decimal, ByVal tiempo As Decimal)

        'insertamos en el histórico el tipo de obtención caudal C
        comando.CommandText = "INSERT INTO [PVYCR_DatosAcequias_Estadillo_Histo]([CodigoPVYCR],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                              ",[idElementoMedida],[TipoObtencionCaudal],[Escala_M],[Calado_M],[Observaciones],[Tiempo_SG] " & _
                              ",[V11_MS],[V12_MS],[V21_MS],[V22_MS],[V31_MS],[V32_MS],Produccion, login) " & _
                              "values(@CodigoPVYCR,@cod_fuente_datos,@Fecha_Medida,@idElementoMedida,'C',@Escala_M,@Calado_M,@Observaciones, " & _
                              "@Tiempo_SG,@V11_MS,@V12_MS,@V21_MS,@V22_MS, " & _
                              "@V31_MS,@V32_MS,'S',@login)"
        'comando.CommandText = "INSERT INTO [PVYCR_DatosAcequias_Estadillo_Histo]([CodigoPVYCR],[Cod_Fuente_Dato],[Fecha_Medida] " & _
        '                          ",[idElementoMedida],[TipoObtencionCaudal],[Escala_M],[Calado_M],[Observaciones],[Tiempo_SG] " & _
        '                          ",[V11_MS],[V12_MS],[V21_MS],[V22_MS],[V31_MS],[V32_MS],Produccion) " & _
        '                          "values(@CodigoPVYCR,'05',@Fecha_Medida,@idElementoMedida,'C',@Escala_M,@Calado_M,@Observaciones, " & _
        '                          "@Tiempo_SG,@V11_MS,@V12_MS,@V21_MS,@V22_MS, " & _
        '                          "@V31_MS,@V32_MS,'S')"

        'comando.Parameters.AddWithValue("@Fecha_Medida", Fecha)
        'comando.Parameters.AddWithValue("@idElementoMedida", idelemen)

        ' si las variables tienen un -9999 es porque eran nulas y se insertarán como nulas, sino insertaremos el valor que tengan
        'comando.Parameters.AddWithValue("@TipoObtencionCaudal", tipocaudal)
        comando.Parameters.AddWithValue("@login", login)

        If tiempo = -9999 Then
            comando.Parameters.AddWithValue("@Tiempo_SG", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@Tiempo_SG", tiempo)
        End If
        If V11 = -9999 Then
            comando.Parameters.AddWithValue("@V11_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V11_MS", Replace(Replace(V11, ".", ""), ",", "."))
        End If
        If V12 = -9999 Then
            comando.Parameters.AddWithValue("@V12_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V12_MS", Replace(Replace(V12, ".", ""), ",", "."))
        End If
        If V21 = -9999 Then
            comando.Parameters.AddWithValue("@V21_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V21_MS", Replace(Replace(V21, ".", ""), ",", "."))
        End If
        If V22 = -9999 Then
            comando.Parameters.AddWithValue("@V22_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V22_MS", Replace(Replace(V22, ".", ""), ",", "."))
        End If
        If V31 = -9999 Then
            comando.Parameters.AddWithValue("@V31_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V31_MS", Replace(Replace(V31, ".", ""), ",", "."))
        End If
        If V32 = -9999 Then
            comando.Parameters.AddWithValue("@V32_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V32_MS", Replace(Replace(V32, ".", ""), ",", "."))
        End If

        comando.ExecuteNonQuery()

        ''insertamos en el histórico el tipo de obtención caudal A
        'comando.CommandText = "INSERT INTO [PVYCR_DatosAcequias_Estadillo_Histo]([CodigoPVYCR],[Cod_Fuente_Dato],[Fecha] " & _
        '                      ",[Hora],[TipoObtencionCaudal],[Escala_M],[Calado_M],[Observaciones],[Tiempo_SG] " & _
        '                      ",[V11_MS],[V12_MS],[V21_MS],[V22_MS],[V31_MS],[V32_MS],Produccion) " & _
        '                      "values(@CodigoPVYCR,'05',@Fecha,@Hora,'A',@Escala_M,@Calado_M,@Observaciones, " & _
        '                      "@Tiempo_SG,@V11_MS,@V12_MS,@V21_MS,@V22_MS, " & _
        '                      "@V31_MS,@V32_MS,'S')"

        'comando.ExecuteNonQuery()

        ''insertamos en el histórico el tipo de obtención caudal F
        'comando.CommandText = "INSERT INTO [PVYCR_DatosAcequias_Estadillo_Histo]([CodigoPVYCR],[Cod_Fuente_Dato],[Fecha] " & _
        '                      ",[Hora],[TipoObtencionCaudal],[Escala_M],[Calado_M],[Observaciones],[Tiempo_SG] " & _
        '                      ",[V11_MS],[V12_MS],[V21_MS],[V22_MS],[V31_MS],[V32_MS],Produccion) " & _
        '                      "values(@CodigoPVYCR,'05',@Fecha,@Hora,'F',@Escala_M,@Calado_M,@Observaciones, " & _
        '                      "@Tiempo_SG,@V11_MS,@V12_MS,@V21_MS,@V22_MS, " & _
        '                      "@V31_MS,@V32_MS,'S')"

        'comando.ExecuteNonQuery()

        'lo borraremos de la tabla [PVYCR_DatosAcequias_estadillo]
        comando.CommandText = "delete from [PVYCR_DatosAcequias_estadillo] where CodigoPVYCR = '" & CodigoPVYCR & "' and  " & _
        "Cod_Fuente_Dato = '" & v_codfuentedatos & "' and Fecha_medida = '" & Fecha & "' and " & _
        "idElementoMedida = '" & idelemen & "'"
        comando.ExecuteNonQuery()
    End Sub
    Protected Sub insertar_rechazadas(ByVal i As Integer)
        'código de rechazo de lectura

        Dim v_CodigoPVYCR, v_Fecha, v_idelemen, v_observa, v_login, v_codfuentedatos As String
        'Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)

        v_CodigoPVYCR = CType(rptAcequiasDetalle2.Items(i).FindControl("txtCodigoPVYCR"), TextBox).Text
        v_Fecha = CType(rptAcequiasDetalle2.Items(i).FindControl("txtFecha"), TextBox).Text
        v_idelemen = CType(rptAcequiasDetalle2.Items(i).FindControl("txtidelementomedida"), TextBox).Text
        v_observa = CType(rptAcequiasDetalle2.Items(i).FindControl("txtObservaciones"), TextBox).Text
        v_login = CType(rptAcequiasDetalle2.Items(i).FindControl("txtLogin"), TextBox).Text
        v_codfuentedatos = CType(rptAcequiasDetalle2.Items(i).FindControl("txtCod_fuente_dato"), TextBox).Text
        'insertamos en el histórico el tipo de obtencion caudal C
        comando.CommandText = "INSERT INTO [PVYCR_DatosAcequias_Estadillo_histo]([CodigoPVYCR],[Cod_Fuente_Dato],[Fecha_medida] " & _
                              ",[idElementoMedida],[TipoObtencionCaudal],[Escala_M],[Calado_M],[Observaciones],[Tiempo_SG] " & _
                              ",[V11_MS],[V12_MS],[V21_MS],[V22_MS],[V31_MS],[V32_MS],Produccion,login) " & _
                              "values(@CodigoPVYCR,@cod_fuente_datos,@Fecha_medida,@idElementoMedida,'C',@Escala_M,@Calado_M,@Observaciones, " & _
                              "@Tiempo_SG,@V11_MS,@V12_MS,@V21_MS,@V22_MS, " & _
                              "@V31_MS,@V32_MS,'N',@login)"
        'insertamos en el histórico el tipo de obtencion caudal C
        'comando.CommandText = "INSERT INTO [PVYCR_DatosAcequias_Estadillo_histo]([CodigoPVYCR],[Cod_Fuente_Dato],[Fecha_medida] " & _
        '                      ",[idElementoMedida],[TipoObtencionCaudal],[Escala_M],[Calado_M],[Observaciones],[Tiempo_SG] " & _
        '                      ",[V11_MS],[V12_MS],[V21_MS],[V22_MS],[V31_MS],[V32_MS],Produccion) " & _
        '                      "values(@CodigoPVYCR,'05',@Fecha_medida,@idElementoMedida,'C',@Escala_M,@Calado_M,@Observaciones, " & _
        '                      "@Tiempo_SG,@V11_MS,@V12_MS,@V21_MS,@V22_MS, " & _
        '                      "@V31_MS,@V32_MS,'N')"

        comando.Parameters.Clear()
        comando.Parameters.AddWithValue("@CodigoPVYCR", v_CodigoPVYCR)
        comando.Parameters.AddWithValue("@Fecha_medida", v_Fecha)
        comando.Parameters.AddWithValue("@idElementoMedida", v_idelemen)
        comando.Parameters.AddWithValue("@Observaciones", v_observa)
        comando.Parameters.AddWithValue("@login", v_login)
        comando.Parameters.AddWithValue("@cod_fuente_datos", v_codfuentedatos)


        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtEscala"), TextBox).Text) = "" Then
            comando.Parameters.AddWithValue("@Escala_M", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@Escala_M", Convert.ToDecimal(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtEscala"), TextBox).Text))
        End If
        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtCalado"), TextBox).Text) = "" Then
            comando.Parameters.AddWithValue("@Calado_M", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@Calado_M", Convert.ToDecimal(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtCalado"), TextBox).Text))
        End If

        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtTiempo"), TextBox).Text) = "" Then
            comando.Parameters.AddWithValue("@Tiempo_SG", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@Tiempo_SG", Convert.ToDecimal(CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtTiempo"), TextBox).Text))
        End If
        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV11MS"), TextBox).Text) = "" Then
            comando.Parameters.AddWithValue("@V11_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V11_MS", Replace(Replace(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV11MS"), TextBox).Text, ".", ""), ",", "."))
        End If
        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV12MS"), TextBox).Text) = "" Then
            comando.Parameters.AddWithValue("@V12_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V12_MS", Replace(Replace(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV12MS"), TextBox).Text, ".", ""), ",", "."))
        End If

        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV21MS"), TextBox).Text) = "" Then
            comando.Parameters.AddWithValue("@V21_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V21_MS", Replace(Replace(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV21MS"), TextBox).Text, ".", ""), ",", "."))
        End If
        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV22MS"), TextBox).Text) = "" Then
            comando.Parameters.AddWithValue("@V22_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V22_MS", Replace(Replace(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV22MS"), TextBox).Text, ".", ""), ",", "."))
        End If
        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV31MS"), TextBox).Text) = "" Then
            comando.Parameters.AddWithValue("@V31_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V31_MS", Replace(Replace(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV31MS"), TextBox).Text, ".", ""), ",", "."))
        End If
        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV32MS"), TextBox).Text) = "" Then
            comando.Parameters.AddWithValue("@V32_MS", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("@V32_MS", Replace(Replace(CType(rptAcequiasDetalle2.Items(i).FindControl("txtV32MS"), TextBox).Text, ".", ""), ",", "."))
        End If

        comando.ExecuteNonQuery()

        ''DESCOMENTARLO CUANDO LA CLAVE DE LA TABLA INCLUYA EL TIPOOBTENCIONCAUDAL, PROBADO Y FUNCIONA
        ''Insertamos tipo obtención caudal A
        'comando.CommandText = "INSERT INTO [PVYCR_DatosAcequias_Estadillo_histo]([CodigoPVYCR],[Cod_Fuente_Dato],[Fecha] " & _
        '              ",[Hora],[TipoObtencionCaudal],[Escala_M],[Calado_M],[Observaciones],[Tiempo_SG] " & _
        '              ",[V11_MS],[V12_MS],[V21_MS],[V22_MS],[V31_MS],[V32_MS],Produccion) " & _
        '              "values(@CodigoPVYCR,'05',@Fecha,@Hora,'A',@Escala_M,@Calado_M,@Observaciones, " & _
        '              "@Tiempo_SG,@V11_MS,@V12_MS,@V21_MS,@V22_MS, " & _
        '              "@V31_MS,@V32_MS,'N')"
        'comando.ExecuteNonQuery()

        ''Insertamos tipo obtención caudal F
        'comando.CommandText = "INSERT INTO [PVYCR_DatosAcequias_Estadillo_histo]([CodigoPVYCR],[Cod_Fuente_Dato],[Fecha] " & _
        '              ",[Hora],[TipoObtencionCaudal],[Escala_M],[Calado_M],[Observaciones],[Tiempo_SG] " & _
        '              ",[V11_MS],[V12_MS],[V21_MS],[V22_MS],[V31_MS],[V32_MS],Produccion) " & _
        '              "values(@CodigoPVYCR,'05',@Fecha,@Hora,'F',@Escala_M,@Calado_M,@Observaciones, " & _
        '              "@Tiempo_SG,@V11_MS,@V12_MS,@V21_MS,@V22_MS, " & _
        '              "@V31_MS,@V32_MS,'N')"
        'comando.ExecuteNonQuery()

        'lo borraremos de la tabla [PVYCR_DatosAcequias_estadillo]
        comando.CommandText = "delete from [PVYCR_DatosAcequias_estadillo] where CodigoPVYCR = '" & v_CodigoPVYCR & "' and  Cod_Fuente_Dato = '" & v_codfuentedatos & "' and  " & _
        "Fecha_medida = '" & v_Fecha & "' and idElementoMedida = '" & v_idelemen & "'"

        comando.ExecuteNonQuery()

    End Sub
   
    Private Sub lbtordenarPVYCR_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarPVYCR.Click
        txtFiltro.Text = ""

        sentenciaSel = "SELECT distinct DM.IdElementoMedida, DM.CodigoPVYCR, DM.Cod_Fuente_Dato, P.DenominacionPunto, c.DenominacionCauce " & _
                        "FROM PVYCR_DatosAcequias_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                        "WHERE DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                         "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI "
        '25/02/2009 ncm
        'and DM.Cod_Fuente_Dato = '05'"
        If imgOrdPVYCRA.Visible = False And imgOrdPVYCRD.Visible = False Then
            imgOrdPVYCRD.Visible = True
        End If

        If imgOrdPVYCRA.Visible = True Then
            sentenciaOrder = sentenciaSel & " order by DM.CodigoPVYCR Desc, DM.Cod_Fuente_Dato Desc"
            'variable session para que se mantenga la ordenacion al cambiar de página
            Session("Ordenacion") = sentenciaOrder
            imgOrdPVYCRA.Visible = False
            imgOrdPVYCRD.Visible = True
            imgOrdNumRegA.Visible = False
            imgOrdNumRegD.Visible = False
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = False
        ElseIf imgOrdPVYCRD.Visible = True Then
            sentenciaOrder = sentenciaSel & " order by DM.CodigoPVYCR , DM.Cod_Fuente_Dato "
            'variable session para que se mantenga la ordenacion al cambiar de página
            Session("Ordenacion") = sentenciaOrder
            imgOrdPVYCRA.Visible = True
            imgOrdPVYCRD.Visible = False
            imgOrdNumRegA.Visible = False
            imgOrdNumRegD.Visible = False
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = False
        End If

        crearDataSets()
        rptAcequias.DataSource = dstAcequias.Tables("TablaAcequias")
        rptAcequias.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtordenarCauce_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarCauce.Click
        txtFiltro.Text = ""
        sentenciaSel = "SELECT distinct DM.IdElementoMedida, DM.CodigoPVYCR, DM.Cod_Fuente_Dato, P.DenominacionPunto, c.DenominacionCauce " & _
                        "FROM PVYCR_DatosAcequias_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                        "WHERE DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                         "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI "
        '25/02/2009
        'and DM.Cod_Fuente_Dato = '05'"

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
        rptAcequias.DataSource = dstAcequias.Tables("TablaAcequias")
        rptAcequias.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtordenarNumReg_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtordenarNumReg.Click
        txtFiltro.Text = ""
        sentenciaSel = "SELECT distinct DM.IdElementoMedida,DM.CodigoPVYCR, DM.Cod_Fuente_Dato, P.DenominacionPunto, c.DenominacionCauce,count(*) NumRegEstdilloA " & _
                        "FROM PVYCR_DatosAcequias_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                        "WHERE DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                         "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI and " & _
                         "group by DM.codigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_dato,P.DenominacionPunto,c.DenominacionCauce"
        'ncm 25/02/2009
        '"DM.Cod_Fuente_Dato = '05'" & _


        If imgOrdNumRegA.Visible = False And imgOrdNumRegD.Visible = False Then
            imgOrdNumRegD.Visible = True
        End If

        If imgOrdNumRegA.Visible = True Then
            sentenciaOrder = sentenciaSel & " order by NumRegEstdilloA Desc"
            Session("Ordenacion") = sentenciaSel & " order by NumRegEstdilloA Desc"
            'Session("ordenarNumReg") = "A"
            imgOrdNumRegA.Visible = False
            imgOrdNumRegD.Visible = True
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = False
            imgOrdPVYCRA.Visible = False
            imgOrdPVYCRD.Visible = False
        ElseIf imgOrdNumRegD.Visible = True Then
            sentenciaOrder = sentenciaSel & " order by NumRegEstdilloA "
            Session("Ordenacion") = sentenciaSel & " order by NumRegEstdilloA "
            'Session("ordenarNumReg") = ""
            imgOrdNumRegA.Visible = True
            imgOrdNumRegD.Visible = False
            imgOrdCauceA.Visible = False
            imgOrdCauceD.Visible = False
            imgOrdPVYCRA.Visible = False
            imgOrdPVYCRD.Visible = False
        End If
        crearDataSets()
        rptAcequias.DataSource = dstAcequias.Tables("TablaAcequias")
        rptAcequias.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Private Sub lbtFiltrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltrar.Click
        Dim sentenciaQuery As String
        lblPagina.Text = "1"
        sentenciaSel = "SELECT distinct DM.IdElementoMedida, DM.CodigoPVYCR, DM.Cod_Fuente_Dato, P.DenominacionPunto, c.DenominacionCauce " & _
                        "FROM PVYCR_DatosAcequias_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                        "WHERE DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                         "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI "
        '25/02/2009
        'and DM.Cod_Fuente_Dato = '05'"
        sentenciaQuery = sentenciaSel
        If txtFiltro.Text <> "" Then
            sentenciaQuery &= " and DM.CodigoPVYCR like '%" & txtFiltro.Text & "%' "
        End If

        sentenciaOrder = sentenciaQuery & " order by DM.CodigoPVYCR, DM.Cod_Fuente_Dato "

        crearDataSets()

        rptAcequias.DataSource = dstAcequias.Tables("TablaAcequias")
        rptAcequias.DataBind()
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
        sentenciaSel = "SELECT distinct DM.IdElementoMedida, DM.CodigoPVYCR, DM.Cod_Fuente_Dato, P.DenominacionPunto, c.DenominacionCauce " & _
                        "FROM PVYCR_DatosAcequias_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                        "WHERE DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                         "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI "
        '25/02/2009
        'and DM.Cod_Fuente_Dato = '05'"

        sentenciaOrder = sentenciaSel & " order by DM.CodigoPVYCR, DM.Cod_Fuente_Dato "
        crearDataSets()
        rptAcequias.DataSource = dstAcequias.Tables("TablaAcequias")
        rptAcequias.DataBind()
        'lblNumpaginas.DataBind()
    End Sub
    Protected Sub SeleccionarcbRegimenCurva(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim elcbRC As CheckBox
        Dim ddlRC As DropDownList
        For i = 0 To rptAcequiasDetalle2.Items.Count - 1
            'Si el check está marcado habilitamos el regimen curva, sino lo deshabilitamos
            If Not IsNothing(rptAcequiasDetalle2.Items(i).FindControl("pnlC")) Then
                If rptAcequiasDetalle2.Items(i).FindControl("pnlC").Visible Then
                    elcbRC = rptAcequiasDetalle2.Items(i).FindControl("cbRegimenCurva")
                    ddlRC = rptAcequiasDetalle2.Items(i).FindControl("ddlRegimenCurva")
                    If elcbRC.Checked = True Then
                        ddlRC.Enabled = True
                    ElseIf elcbRC.Checked = False Then
                        ddlRC.Enabled = False
                    End If
                End If
            End If
        Next

    End Sub
    Protected Sub btnRechazadas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRechazadas.Click
        'dstAcequias.Tables("TablaAcequias").Rows(0).Item("CodigoPVYCR").ToString()
        Response.Redirect("acequiasrechazadas.aspx?codigoPVYCR=" & "VA005P02")
    End Sub
    Protected Sub Formateo_controles_cliente(ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)

        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtEscala"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtEscala"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtCalado"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtCalado"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtV11MS"), TextBox), 3)
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtV11MS"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtV12MS"), TextBox), 3)
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtV12MS"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtV21MS"), TextBox), 3)
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtV21MS"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtV22MS"), TextBox), 3)
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtV22MS"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtV31MS"), TextBox), 3)
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtV31MS"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtV32MS"), TextBox), 3)
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtV32MS"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtDudaCalidad"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtDudaCalidad"), TextBox))
        JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtNParada"), TextBox))
        JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtNParada"), TextBox))


    End Sub
    Protected Sub MarcarEstimada(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer

        For i = 0 To rptAcequiasDetalle2.Items.Count - 1
            'si es el primer registro de momento inhabilitamos el check de estimada
            If i = 0 Or i = 1 Or i = 2 Then
                CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("cbestimadaC"), CheckBox).Enabled = False
            Else
                CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("cbestimadaC"), CheckBox).Enabled = True

                If Not IsNothing(rptAcequiasDetalle2.Items(i).FindControl("pnlC")) Then
                    If rptAcequiasDetalle2.Items(i).FindControl("pnlC").Visible Then
                        If CType(rptAcequiasDetalle2.Items(i).FindControl("cbestimadaC"), CheckBox).Checked = True Then
                            CType(rptAcequiasDetalle2.Items(i).FindControl("cbestimadaA"), CheckBox).Checked = True
                            CType(rptAcequiasDetalle2.Items(i).FindControl("cbestimadaF"), CheckBox).Checked = True
                        Else
                            CType(rptAcequiasDetalle2.Items(i).FindControl("cbestimadaA"), CheckBox).Checked = False
                            CType(rptAcequiasDetalle2.Items(i).FindControl("cbestimadaF"), CheckBox).Checked = False
                        End If
                    End If
                End If
            End If
        Next
    End Sub
    Protected Function estaActivo(ByVal elDataitem As DataRowView) As Boolean

        If Session("fila") = 0 Then
            Session("fila") = Session("fila") + 1
            Return False
        Else
            Session("fila") = Session("fila") + 1
            Return True
        End If

    End Function
    Protected Function checkTipoEstimadaC() As String
        Dim resultado As String
        Dim elpanel As Panel
        Dim i As Integer
     
        resultado = "<tr >"
        resultado &= "<td colspan='17'>"
        resultado &= "<table width='100%'>"
        resultado &= "<tr>"
        resultado &= "<td class='L' align='left' style='font: bold 11px Tahoma; text-align: left; background-color: #DDDDDD' Width=20>"
        resultado &= "<input ID='txtTOCaudalEC' runat='server' value='E' "
        resultado &= "Style='font: 10px Verdana; text-align: left; border: 0px; background-color: #DDDDDD' Width=20 ReadOnly='true'></input>"
        resultado &= "</td>"
        resultado &= "<td class='L' width=1040> Caudal(m3 / s)"
        resultado &= "<input ID='txtCaudalEC' runat='server' type=tex value='0' "
        resultado &= "Style='font: 10px Verdana; text-align: left; border: 0px' ReadOnly='true' Width=1040 ></input>"
        resultado &= "</td>"
        resultado &= "<td nowrap>"
        resultado &= "<input ID='cbEC' runat='server' EnableViewState='true' />"
        resultado &= "</td>"
        resultado &= "</tr>"
        resultado &= "</table>"
        resultado &= "</td>"
        resultado &= "</tr>"
        'End If
        'Next

        Return resultado
    End Function

    Protected Sub btnCurvas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCurvas.Click
        Dim i, j As Integer
        Dim dr As DataRowView
        Dim v_caudal As Decimal
        Dim elPanel As Panel
        Dim elradiobtnA, elradiobtnR As RadioButton
        Dim visibleF, visibleC, visibleA As Boolean
        Dim v_items As Integer = rptAcequiasDetalle2.Items.Count
        Dim dstPuntos As DataSet = New DataSet
        Dim dtPuntos As DataTable = New DataTable
        Dim fila As DataRow
        Dim dstCaudal As DataSet = New DataSet
        'creamos la estrucrura del dataset
        dtPuntos = dstPuntos.Tables.Add("TablaCaudales")
        dtPuntos.Columns.Add("CodigoPVYCR")
        dtPuntos.Columns.Add("idElementoMedida")
        dtPuntos.Columns.Add("TipoObtCaudal")
        dtPuntos.Columns.Add("Curva")
        dtPuntos.Columns.Add("ValorCurva")
        dtPuntos.Columns.Add("altura")
        dtPuntos.Columns.Add("Caudal")
        j = 0
        For i = 0 To v_items - 1
            'Evaluamos sólo los checkbox de aceptar y rechazar cuando pasamos por un item
            'de tipo C:
            If Not IsNothing(rptAcequiasDetalle2.Items(i).FindControl("pnlC")) Then
                If rptAcequiasDetalle2.Items(i).FindControl("pnlC").Visible Then
                    elradiobtnA = rptAcequiasDetalle2.Items(i).FindControl("rbSelCalculoA")
                    elradiobtnR = rptAcequiasDetalle2.Items(i).FindControl("rbSelCalculoR")
                End If
            End If

            utiles.Comprobar_Conexion_BD(Page, conexion)

            ' si está marcado el check aceptar, tendremos que comprobar primero si
            ' han aceptado algún cálculo, si no ha sido así pasaremos al siguiente registro
            If elradiobtnA.Checked = True Then
                elPanel = Nothing
                If Not IsNothing(rptAcequiasDetalle2.Items(i).FindControl("pnlC")) Then
                    elPanel = rptAcequiasDetalle2.Items(i).FindControl("pnlC")
                ElseIf Not IsNothing(rptAcequiasDetalle2.Items(i).FindControl("pnlA")) Then
                    elPanel = rptAcequiasDetalle2.Items(i).FindControl("pnlA")
                ElseIf Not IsNothing(rptAcequiasDetalle2.Items(i).FindControl("pnlF")) Then
                    elPanel = rptAcequiasDetalle2.Items(i).FindControl("pnlF")
                End If

                If (CType(elPanel.FindControl("cbA"), CheckBox).Checked Or CType(elPanel.FindControl("cbC"), CheckBox).Checked Or CType(elPanel.FindControl("cbF"), CheckBox).Checked) Then

                    ' si el usuario acepta los calculos, tendremos que comprobar si los tipos de caudal están o no
                    ' aceptados viendo el valor de los radio buttons.
                    dtPuntos.Rows.Add()
                    dtPuntos.Rows(j).Item("CodigoPVYCR") = Session("codigoPVYCR").ToString

                    dtPuntos.Rows(j).Item("CodigoPVYCR") = Session("codigoPVYCR").ToString
                    dtPuntos.Rows(j).Item("idElementomedida") = Session("idElementomedida").ToString

                    dtPuntos.Rows(j).Item("Curva") = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("ddlRegimenCurva"), DropDownList).SelectedItem.Text
                    dtPuntos.Rows(j).Item("ValorCurva") = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("ddlRegimenCurva"), DropDownList).SelectedItem.Value
                    dtPuntos.Rows(j).Item("altura") = CType(rptAcequiasDetalle2.Items(i - (i Mod 3)).FindControl("txtcalado"), TextBox).Text

                    If CType(elPanel.FindControl("cbA"), CheckBox).Checked Then
                        dtPuntos.Rows(j).Item("TipoObtcaudal") = "A"
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudalA"), TextBox).Text) = "" Then
                            dtPuntos.Rows(j).Item("Caudal") = 0
                        Else
                            dtPuntos.Rows(j).Item("Caudal") = CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudalA"), TextBox).Text
                        End If

                    ElseIf CType(elPanel.FindControl("cbC"), CheckBox).Checked Then
                        dtPuntos.Rows(j).Item("TipoObtcaudal") = "C"
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudalC"), TextBox).Text) = "" Then
                            dtPuntos.Rows(j).Item("Caudal") = 0
                        Else
                            dtPuntos.Rows(j).Item("Caudal") = CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudalC"), TextBox).Text
                        End If
                    Else
                        dtPuntos.Rows(j).Item("TipoObtcaudal") = "F"
                        If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudalF"), TextBox).Text) = "" Then
                            dtPuntos.Rows(j).Item("Caudal") = 0
                        Else
                            dtPuntos.Rows(j).Item("Caudal") = CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudalF"), TextBox).Text
                        End If
                    End If

                    'If utiles.nullABlanco(CType(rptAcequiasDetalle2.Items(i).FindControl("txtCaudal" & elPanel.ID.Substring(3, 1)), TextBox).Text) = "" Then
                    '    dtPuntos.Rows(j).Item("Caudal") = 0
                    'Else
                    '    d()
                    'End If
                    j = j + 1
                End If
            End If
        Next
        'asignamos el dataset a una variable global
        'IPIM 29/12/2008 Vacío la session que pueda existir desde lecturas para que coja lo de preproducción y no lo de lecturas
        Session("TablaCaudalesDesdeAcequias") = Nothing
        Session("TablaCaudales") = dstPuntos
        'IPIM 29/12/2008
        'Page.RegisterClientScriptBlock("abreGrafica", "<script language=javascript>" & _
        '              "window.open(""CurvasAcequiasPreproduccionFlash.aspx?"" ,""grafico"", ""toolbar=no,menubar=no,top=200,left=250,height=500,width=650"");" & _
        '        "</script>")
        Page.RegisterClientScriptBlock("abreGrafica", "<script language=javascript>" & _
                      "window.open(""CurvasAcequiasPreproduccionFlash.aspx?"");" & _
                "</script>")
        'Page.RegisterClientScriptBlock("abreGrafica", "<script language=javascript>" & _
        '            "window.open(""CurvasAcequiasPreproduccion.aspx?"" ,""grafico"", ""toolbar=no,menubar=no,top=200,left=250,height=500,width=650"");" & _
        '      "</script>")
    End Sub

    Protected Sub lbtNuevoElemento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtNuevoElemento.Click
        pnlAcequias.Visible = False
        Panel1.Visible = False
        'mostramos la cabecera
        pnlEstadillo.Visible = True
        'mostramos el panel de edición
        pnlNuevoElemento.Visible = True
        lblNombrePVYCR.Text = "INSERCIÓN MANUAL DE LECTURAS"
        cargarlistas_edicion()
    End Sub
    Protected Sub cargarlistas_edicion()
        'lista de cauces
        Dim daCauces As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstCauces As DataSet = New System.Data.DataSet()

        utiles.Comprobar_Conexion_BD(Page, conexion)
        daCauces.SelectCommand.CommandText = "select distinct c.codigocauce from pvycr_cauces c, pvycr_puntos p, pvycr_elementosmedida e " & _
            "where c.codigocauce = p.codigocauce and  p.codigopvycr = e.codigopvycr and p.tipopunto = 'G' and e.tipo ='Q' " & _
            "order by c.codigocauce"
        daCauces.Fill(dstCauces, "TablaCauces")
        ddlEDCauce.DataSource = dstCauces.Tables("TablaCauces")
        ddlEDCauce.DataValueField = "codigoCauce"
        ddlEDCauce.DataTextField = "codigocauce"
        ddlEDCauce.DataBind()
        ddlEDCauce.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        'lista de codigo fuente datos
        Dim daCodfuenteDato As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstCodFuenteDato As DataSet = New System.Data.DataSet()

        utiles.Comprobar_Conexion_BD(Page, conexion)
        daCodfuenteDato.SelectCommand.CommandText = "Select Cod_fuente_dato, FUENTE_DATOS,Cod_fuente_dato+' - '+FUENTE_DATOS nombre from FUENTES_DE_DATOS order by cod_fuente_dato"
        daCodfuenteDato.Fill(dstCodFuenteDato, "TablaCodFuenteDato")
        ddlEDCodFuenteDato.DataSource = dstCodFuenteDato.Tables("TablaCodFuenteDato")
        ddlEDCodFuenteDato.DataValueField = "cod_fuente_dato"
        ddlEDCodFuenteDato.DataTextField = "nombre"
        ddlEDCodFuenteDato.DataBind()
        ddlEDCodFuenteDato.Items.Insert(0, New ListItem("[Seleccionar]", ""))
    End Sub
    Protected Sub btnEDCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEDCancelar.Click
        limpiar_campos_edicion()
        pnlAcequias.Visible = True
        Panel1.Visible = False
        pnlEstadillo.Visible = False
        pnlNuevoElemento.Visible = False
    End Sub
    Protected Sub limpiar_campos_edicion()
        ddlEDElemento.Items.Clear()
        ddlEDCauce.Items.Clear()
        ddlEDCodigoPVYCR.Items.Clear()
        ddlEDCodFuenteDato.Items.Clear()
        txtEDFechaMedida.Text = ""
        txtEDTipoObtencion.Text = ""
        txtEDEscala.Text = ""
        txtEDCalado.Text = ""
        txtEDV11.Text = ""
        txtEDV12.Text = ""
        txtEDV21.Text = ""
        txtEDV22.Text = ""
        txtEDV31.Text = ""
        txtEDV32.Text = ""
        txtEDUsuario.Text = ""
        txtEDTiempo.Text = ""
        txtEDObservaciones.Text = ""
    End Sub

    Protected Sub ddlEDCauce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEDCauce.SelectedIndexChanged
        Dim daPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstPuntos As DataSet = New System.Data.DataSet()
        'lista de puntos
        daPuntos.SelectCommand.CommandText = "select distinct p.codigopvycr from pvycr_cauces c, pvycr_puntos p, pvycr_elementosmedida e " & _
                                            "where c.codigocauce = p.codigocauce and p.tipopunto = 'G' and p.codigopvycr = e.codigopvycr and " & _
                                            "e.tipo = 'Q' and c.codigocauce = '" & ddlEDCauce.Text & "' " & _
                                            "order by p.codigopvycr"
        daPuntos.Fill(dstPuntos, "TablaPuntos")
        ddlEDCodigoPVYCR.DataSource = dstPuntos.Tables("TablaPuntos")
        ddlEDCodigoPVYCR.DataValueField = "codigopvycr"
        ddlEDCodigoPVYCR.DataTextField = "codigopvycr"
        ddlEDCodigoPVYCR.DataBind()
        ddlEDCodigoPVYCR.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        ddlEDElemento.Items.Clear()

    End Sub

    Protected Sub btnEDAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEDAceptar.Click
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim objTrans As SqlClient.SqlTransaction
        Try
            'If conexion.State = ConnectionState.Closed Then conexion.Open()
            utiles.Comprobar_Conexion_BD(Page, conexion)
            ''comprobamos si existe el elemento de medida, si no existe lo insertamos para que no falle la FK que hay entre datos alimentacion y elementos de medida
            'daAcequias.SelectCommand.CommandText = "select count(*) numero from pvycr_elementosmedida where codigopvycr= '" & ddlEDCodigoPVYCR.Text & "' and idelementomedida = '" & txtEDidElementomedida.Text & "' "
            'daAcequias.Fill(dstAcequias, "TablaExisteElemento")

            'abrimos una transaccion para que si falla algunos de los inserts haga un rollback
            objTrans = conexion.BeginTransaction()
            comando.Transaction = objTrans
            comando.Parameters.AddWithValue("@idElementoMedida", ddlEDElemento.Text)
            comando.Parameters.AddWithValue("@CodigoPVYCR", ddlEDCodigoPVYCR.Text)
            comando.Parameters.AddWithValue("@CodFuenteDato", ddlEDCodFuenteDato.Text)
            'If dstAcequias.Tables("TablaExisteElemento").Rows(0).Item("numero") = 0 Then
            '    comando.CommandText = "INSERT INTO PVYCR_ELEMENTOSMEDIDA (CODIGOPVYCR, idElementoMedida, TIPO) VALUES " & _
            '    "(@CodigoPVYCR,@idElementoMedida,'Q')"
            '    comando.ExecuteNonQuery()
            'End If

            comando.Parameters.AddWithValue("@Fecha_medida", txtEDFechaMedida.Text)

            'insertamos EN ESRADILLO
            comando.CommandText = "INSERT INTO [PVYCR_DatosAcequias_Estadillo]([CodigoPVYCR],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                                  ",[idElementoMedida],[TipoObtencionCaudal],[Escala_M],[Calado_M],[Observaciones],[Tiempo_SG] " & _
                                  ",[V11_MS],[V12_MS],[V21_MS],[V22_MS],[V31_MS],[V32_MS], login) " & _
                                  "values(@CodigoPVYCR,@CodFuenteDato,@Fecha_Medida,@idElementoMedida,@TipoObtencion,@Escala_M,@Calado_M,@Observaciones, " & _
                                  "@Tiempo_SG,@V11_MS,@V12_MS,@V21_MS,@V22_MS, " & _
                                  "@V31_MS,@V32_MS,@login)"


            comando.Parameters.AddWithValue("@login", txtEDUsuario.Text)
            comando.Parameters.AddWithValue("@TipoObtencion", txtEDTipoObtencion.Text)


            If utiles.nullABlanco(txtEDEscala.Text) = "" Then
                comando.Parameters.AddWithValue("@Escala_M", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@Escala_M", txtEDEscala.Text.Replace(",", "."))
            End If
            If utiles.nullABlanco(txtEDCalado.Text) = "" Then
                comando.Parameters.AddWithValue("@Calado_M", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@Calado_M", txtEDCalado.Text.Replace(",", "."))
            End If

            If utiles.nullABlanco(txtEDTiempo.Text) = "" Then
                comando.Parameters.AddWithValue("@Tiempo_SG", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@Tiempo_SG", txtEDTiempo.Text.Replace(",", "."))
            End If
            If utiles.nullABlanco(txtEDV11.Text) = "" Then
                comando.Parameters.AddWithValue("@V11_MS", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@V11_MS", txtEDV11.Text.Replace(",", "."))
            End If
            If utiles.nullABlanco(txtEDV12.Text) = "" Then
                comando.Parameters.AddWithValue("@V12_MS", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@V12_MS", txtEDV12.Text.Replace(",", "."))
            End If
            If utiles.nullABlanco(txtEDV21.Text) = "" Then
                comando.Parameters.AddWithValue("@V21_MS", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@V21_MS", txtEDV21.Text.Replace(",", "."))
            End If
            If utiles.nullABlanco(txtEDV22.Text) = "" Then
                comando.Parameters.AddWithValue("@V22_MS", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@V22_MS", txtEDV22.Text.Replace(",", "."))
            End If
            If utiles.nullABlanco(txtEDV31.Text) = "" Then
                comando.Parameters.AddWithValue("@V31_MS", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@V31_MS", txtEDV31.Text.Replace(",", "."))
            End If
            If utiles.nullABlanco(txtEDV32.Text) = "" Then
                comando.Parameters.AddWithValue("@V32_MS", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@V32_MS", txtEDV32.Text.Replace(",", "."))
            End If
            comando.Parameters.AddWithValue("@Observaciones", txtEDObservaciones.Text)
            comando.ExecuteNonQuery()

            objTrans.Commit()
        Catch Exc As System.Data.SqlClient.SqlException
            objTrans.Rollback()
            Select Case Exc.Number
                'Case 547
                '   Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    JavaScript.Alert(Page, "El dato Acequias ya existe para la fecha: " & txtEDFechaMedida.Text)
            End Select
        Catch Exc As Exception
            objTrans.Rollback()
            JavaScript.Alert(Page, "Error: " & Exc.Message)
        End Try
        limpiar_campos_edicion()
        sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                       "FROM PVYCR_DatosAcequias_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                       "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                        "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) "
        'ncm 25/02/2009
        'and DM.Cod_Fuente_Dato = '05'"

        sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "

        crearDataSets()

        rptAcequias.DataSource = dstAcequias.Tables("TablaAcequias")
        rptAcequias.DataBind()
        'lblNumpaginas.DataBind()
        pnlAcequias.Visible = True
        Panel1.Visible = False
        pnlEstadillo.Visible = False
        pnlNuevoElemento.Visible = False
    End Sub

    Protected Sub ddlEDCodigoPVYCR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEDCodigoPVYCR.SelectedIndexChanged
        Dim daEDElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstEDElementos As DataSet = New System.Data.DataSet()
        'lista de puntos
        daEDElementos.SelectCommand.CommandText = "select e.idelementomedida from pvycr_puntos p, pvycr_elementosmedida e " & _
                                            "where p.codigoPVYCR = e.codigoPVYCR and p.tipopunto = 'G' and e.codigoPVYCR = '" & ddlEDCodigoPVYCR.Text & "' " & _
                                            "and e.tipo = 'Q' order by e.idelementomedida"
        daEDElementos.Fill(dstEDElementos, "TablaEDElementos")
        ddlEDElemento.DataSource = dstEDElementos.Tables("TablaEDElementos")
        ddlEDElemento.DataValueField = "idelementomedida"
        ddlEDElemento.DataTextField = "idelementomedida"
        ddlEDElemento.DataBind()
    End Sub
    Protected Sub Poner_Campos_nulos(ByVal numFila As Integer, ByVal caudalA_ant As Decimal, ByRef superficieTeoA_ant As Decimal, ByRef altFangosA_ant As Decimal, ByRef superficieFangosA_ant As Decimal, ByRef superficieRealA_ant As Decimal, ByRef velFlotadorA_ant As Decimal, ByRef velMolineteA_ant As Decimal)
        CType(rptAcequiasDetalle2.Items(numFila).FindControl("txtsuperficieTM"), TextBox).Text = Nothing
        CType(rptAcequiasDetalle2.Items(numFila).FindControl("txtaltM"), TextBox).Text = Nothing
        CType(rptAcequiasDetalle2.Items(numFila).FindControl("txtsuperficieFM"), TextBox).Text = Nothing
        CType(rptAcequiasDetalle2.Items(numFila).FindControl("txtsuperficieRM"), TextBox).Text = Nothing
        CType(rptAcequiasDetalle2.Items(numFila).FindControl("txtvelocidadM"), TextBox).Text = Nothing
        CType(rptAcequiasDetalle2.Items(numFila).FindControl("txtCaudalA"), TextBox).Text = Nothing
        caudalA_ant = -9999
        superficieTeoA_ant = Nothing
        altFangosA_ant = Nothing
        superficieFangosA_ant = Nothing
        superficieRealA_ant = Nothing
        velFlotadorA_ant = Nothing
        velMolineteA_ant = Nothing
    End Sub
    Protected Function checkNivusAntes(ByVal elDataitem As DataRowView) As String
        Dim cadena As String = ""

        daAcequias.SelectCommand.CommandText = "select TOP 1 FECHA_MEDIDA, CAUDAL_M3S, ESCALA_M from pvycr_datosacequias " & _
                       "where codigopvycr = '" & elDataitem("codigoPVYCR") & "' AND  COD_FUENTE_DATO = '13' AND " & _
                    "FECHA_MEDIDA < '" & elDataitem("fecha_medida") & "' ORDER BY FECHA_MEDIDA DESC"
        daAcequias.Fill(dstAcequias, "TablaNivusAntes")
        Dim num As Integer = dstAcequias.Tables("TablaNivusAntes").Rows.Count

        If num > 0 Then
            cadena = "<td style='font-weight: bold;background-color:#D8E4D2;color:#616E51' align=left colspan='17' nowrap>Lectura anterior Nivus con fecha: " & dstAcequias.Tables("TablaNivusantes").Rows(0).Item("fecha_medida") & ", Caudal: " & dstAcequias.Tables("TablaNivusantes").Rows(0).Item("caudal_m3s") & " (m3/s) y Escala: " & dstAcequias.Tables("TablaNivusantes").Rows(0).Item("escala_m") & " (m) " & "</td>"
            dstAcequias.Tables("TablaNivusAntes").Clear()
            Return cadena
        Else
            cadena = "<td style='font-weight: bold;background-color:#D8E4D2;color:#616E51' align=left colspan='17' nowrap>No existe lectura Nivus anterior</td>"
            dstAcequias.Tables("TablaNivusAntes").Clear()
            Return cadena
        End If
    End Function
    Protected Function checkNivusDespues(ByVal elDataitem As DataRowView) As String
        Dim cadena As String = ""

        daAcequias.SelectCommand.CommandText = "select TOP 1 FECHA_MEDIDA, CAUDAL_M3S, ESCALA_M from pvycr_datosacequias " & _
                       "where codigopvycr = '" & elDataitem("codigoPVYCR") & "' AND  COD_FUENTE_DATO = '13' AND " & _
                    "FECHA_MEDIDA > '" & elDataitem("fecha_medida") & "' ORDER BY FECHA_MEDIDA "
        daAcequias.Fill(dstAcequias, "TablaNivusdespues")
        Dim num As Integer = dstAcequias.Tables("TablaNivusdespues").Rows.Count

        If num > 0 Then
            'font:9px Verdana; border:0px; background-color:#D7DDBB
            cadena = "<td style='font-weight: bold;background-color:#D8E4D2;color:#616E51' align=left colspan='17' nowrap>Lectura posterior Nivus con fecha: " & dstAcequias.Tables("TablaNivusdespues").Rows(0).Item("fecha_medida") & ", Caudal: " & dstAcequias.Tables("TablaNivusdespues").Rows(0).Item("caudal_m3s") & " (m3/s) y Escala: " & dstAcequias.Tables("TablaNivusdespues").Rows(0).Item("escala_m") & " (m) " & "</td>"
            dstAcequias.Tables("TablaNivusDespues").Clear()
            Return cadena
        Else
            cadena = "<td style='font-weight: bold;background-color:#D8E4D2;color:#616E51' align=left colspan='17' nowrap>No existe lectura Nivus posterior.</td>"
            dstAcequias.Tables("TablaNivusDespues").Clear()
            Return cadena
        End If
    End Function
    Private Function ObtenerMolineteFlotadorAnterior(ByVal cod_fuente_datos As String, ByVal pfecha_medida As String, ByRef tipo As String) As String
        Dim vpos As Decimal
        Dim vcadena1, vcadena2 As String
        utiles.Comprobar_Conexion_BD(Page, conexion)
        vpos = lblidMotor.Text.IndexOf("/")

        vcadena1 = lblidMotor.Text.Substring(0, vpos - 1)
        'obtenemos el idelemento
        vcadena2 = lblidMotor.Text.Substring(lblidMotor.Text.Length - 3, 3)
        If dstAcequias.Tables.Contains("TablaCaudalMolinete") Then
            dstAcequias.Tables("TablaCaudalMolinete").Rows.Clear()
        End If
        If dstAcequias.Tables.Contains("TablaCaudalFlotador") Then
            dstAcequias.Tables("TablaCaudalFlotador").Rows.Clear()
        End If



        'obtenemos el codfuentedato

        '1) buscamos el caudal calculado por molinete para los tres días anteriores a nuestra fecha_medida, si lo encontramos será el que devolvamos
        '2) no molinete, hacemos lo mismo para flotador.
        '3) no flotador, devolvemos una N

        daAcequias.SelectCommand.CommandText = "SELECT Caudal_M3S FROM PVYCR_DatosAcequias where tipoobtencioncaudal ='A' and " & _
                        "idElementoMedida = '" & vcadena2 & "' and CodigoPVYCR = '" & vcadena1 & "' And Cod_Fuente_Dato = '" & cod_fuente_datos & "' and " & _
                        "fecha_medida between dateadd(day,-3,'" & pfecha_medida & "') and '" & pfecha_medida & "' order by fecha_medida desc"

        daAcequias.Fill(dstAcequias, "TablaCaudalMolinete")
        If dstAcequias.Tables("TablaCaudalMolinete").Rows.Count = 0 Then
            daAcequias.SelectCommand.CommandText = "SELECT Caudal_M3S FROM PVYCR_DatosAcequias where tipoobtencioncaudal ='F' and " & _
            "idElementoMedida = '" & vcadena2 & "' and CodigoPVYCR = '" & vcadena1 & "' And Cod_Fuente_Dato = '" & cod_fuente_datos & "' and " & _
                        "fecha_medida between dateadd(day,-3,'" & pfecha_medida & "') and '" & pfecha_medida & "' order by fecha_medida desc"

            daAcequias.Fill(dstAcequias, "TablaCaudalFlotador")
            If dstAcequias.Tables("TablaCaudalFlotador").Rows.Count = 0 Then
                tipo = "N"
                Return "N"
            Else
                tipo = "F"
                Return dstAcequias.Tables("TablaCaudalFlotador").Rows(0).Item("caudal_m3s").ToString
            End If
        Else
            tipo = "A"
            Return dstAcequias.Tables("TablaCaudalMolinete").Rows(0).Item("caudal_m3s").ToString
        End If

    End Function
    Private Sub ObtenerCurvaMasProbable()
        Dim vpos, vcodigoPVYCR, vidElemento As String
        vpos = lblidMotor.Text.IndexOf("/")

        vcodigoPVYCR = lblidMotor.Text.Substring(0, vpos - 1)
        'obtenemos el idelemento
        vidElemento = lblidMotor.Text.Substring(lblidMotor.Text.Length - 3, 3)
        daProbabilidadCurvas.SelectCommand.CommandText = "SELECT REGIMEN,Cod_Curva,Probabilidad " & _
                                     "FROM PVYCR_CurvasAcequias " & _
                                     "WHERE codigoPVYCR = '" & vcodigoPVYCR & "' and idElementoMedida = '" & vidElemento & "' and FECHA_FIN_USO >= getdate() " & _
                                     " and regimen <> 'CT' " & _
                                     "order by Probabilidad DESC "

        daProbabilidadCurvas.Fill(dstProbabilidad, "TablaProbabilidad")
    End Sub
End Class
