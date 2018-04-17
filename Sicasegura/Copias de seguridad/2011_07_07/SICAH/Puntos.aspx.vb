Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.IO
Imports SICAH_FuncionesArbolExt
Imports FuncionesGenerales

Partial Class SICAH_puntos
    Inherits System.Web.UI.Page

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstPuntos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0
    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ucPaginacion.ruta = "../"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        lbtNuevo.DataBind()

        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        'Registro de la funcionalidad del calendario
        imgfechaInicion.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaInicion.ClientID & "'),'dd/mm/yyyy');")
        imgfechaFin.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaFin.ClientID & "'),'dd/mm/yyyy');")

        lblCabecera.Text = genHTML.cabecera(Page)


        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If

            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))

            'EGB 16/07/2008 Asociación con el Arbol de Cauces --------------------------------------------------------------------
            imgCerrarVentana.Attributes.Add("onclick", "desplegarArbol(this);")
            imgArbol.Attributes.Add("onclick", "desplegarArbol(this);")

            If Request.QueryString("nuevo") = "yes" Then
                pnlEDPuntos.Visible = True
                pnlPuntos.Visible = False
                'ucPaginacion.Visible = False
                lblTitulo.Text = "&nbsp;<b>NUEVO PUNTO</b>"
                txtcodigoPunto.Enabled = True

                If treeView1.Nodes.Count = 0 Then
                    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "P") 'EGB el nuevo parametro P hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
                End If

            End If
            'If Request.QueryString("pag") <> "" Then
            '    ucPaginacion.lblPaginatext = Request.QueryString("pag")
            'Else
            '    ucPaginacion.lblPaginatext = "1"
            'End If

            rellenarListas()
            crearDataSets()

            Session.Item("codigoPVYCR") = ""
            'ucPaginacion.DataBind()
            'ucPaginacion.lblNumpaginasDatabind()
        End If
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'If ucPaginacion.lblMuevetext = "si" Then
        'crearDataSets()
        '    ucPaginacion.lblMuevetext = "no"
        'End If
    End Sub
    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Private Sub crearDataSets()
        'Criterios de filtrado
        Dim sFiltro As String = ""

        utiles.Comprobar_Conexion_BD(Page, conexion)
        If txtRegistros.Text = "" Then
            sentenciaSel = " SELECT "
        Else
            sentenciaSel = " SELECT TOP " & txtRegistros.Text
        End If

        'sentenciaSel = sentenciaSel & "   CodigoCauce,CodigoPVYCR, DenominacionPunto, TipoSensor, Acceso, X_UTM, Y_UTM, PorcentajeRegable, B, B2, H, Ti, Td, Diametro_mm, " & _
        '               " FactorFlotador, TipoPunto, CodigoDataLogger, UsadoEnParteOficial, PKS, PKA, RIO, Longitud, LongitudFlotador, A1_M, A2_M, B1_M, B2_M, B3_M,  " & _
        '               " H1_M, H2_M, Offset_M, Antiguo, Observaciones " & _
        '               " FROM PVYCR_Puntos "

        'IPIM 26/05/2009
        sentenciaSel = sentenciaSel & "   CodigoCauce,CodigoPVYCR, DenominacionPunto, TipoSensor, Acceso, X_UTM, Y_UTM, PorcentajeRegable, Diametro_mm, " & _
                       " FactorFlotador, TipoPunto, CodigoDataLogger, UsadoEnParteOficial, PKS, PKA, RIO, Longitud, LongitudFlotador, A1_M, A2_M, A3_M, B1_M, B2_M, B3_M,  " & _
                       " B4_M,H12_M, H23_M,H34_M, Offset_M, Antiguo, Observaciones,Favorito " & _
                       " FROM PVYCR_Puntos "


        'RDF 20081002
        If Session("FiltroPunt") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroPunt")
            Else
                sFiltro = sFiltro + Session("FiltroPunt")
            End If
        End If

        sentenciaOrder = " order by CodigoPVYCR "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If

        daPuntos.SelectCommand.CommandText = sentenciaSel
        daPuntos.Fill(dstPuntos, "TablaPuntos")
        'daPuntos.Fill(dstPuntos, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaPuntos")
        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daPuntos.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        'ucPaginacion.calcularPags(txtComando)

        'ncm calculamos el nº de registros que devolverá el filtro 
        txtNumRegFiltrados.Text = FuncionesGenerales.CalcularNumRegFiltrados(Session("FiltroPunt"), conexion, sFiltro, "PVYCR_puntos", Page)

        rptPuntos.DataSource = dstPuntos.Tables("TablaPuntos")
        rptPuntos.DataBind()
     

    End Sub

    'Public Function GetDataSetArbol() As DataSet
    '    Dim DS As System.Data.DataSet

    '    If File.Exists(MapPath("xmlDataSet.xml")) = False Then

    '        crearDataSetsPuntosXML2(dstarbol, treeView1, daPuntos)

    '        dstarbol.WriteXml(MapPath("xmlDataSet.xml"))

    '        Return dstarbol
    '        Session("DSLoaded") = 1
    '    End If

    '    DS = New DataSet

    '    DS.ReadXml(MapPath("xmlDataSet.xml"))

    '    DS.Relations.Add("SelfRefenceRelation", _
    '                   DS.Tables("tablaArbol").Columns("IdArbol"), _
    '                    DS.Tables("tablaArbol").Columns("IdArbolPadre"), True)
    '    'DS.Relations("SelfRefenceRelation").Nested = True
    '    Session("DSLoaded") = 1
    '    Return DS

    'End Function
    Private Function GetDataSetArbol() As DataSet
        'EGB. El arbol debe cargarse a partir de los datos de PVYCR_Arbol para no regenerar el XML en cada actualización

        crearDataSetsPuntosXML2()
        Return dstarbol
        'Session("DSLoaded") = 1

    End Function

    Private Sub crearDataSetsPuntosXML2()
        Try
            dstarbol.Clear()
            treeView1.Nodes.Clear()

            'ARBOL NORMALIZADO
            daPuntos.SelectCommand.CommandText = "Select * from dbo.PVYCR_ARBOL where Tipo='C' OR Tipo='R'"
            daPuntos.Fill(dstarbol, "tablaArbol")

            dstarbol.Relations.Add("SelfRefenceRelation", _
                            dstarbol.Tables("tablaArbol").Columns("IdArbol"), _
                            dstarbol.Tables("tablaArbol").Columns("IdArbolPadre"), True)
            dstarbol.Relations("SelfRefenceRelation").Nested = True

        Catch exp As Exception
        End Try
    End Sub

    Private Sub crearDataSetPunto()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daPuntos.SelectCommand.CommandText = "select * from PVYCR_Puntos where CodigoPVYCR='" & lblPuntoSel.Text & "'"
        daPuntos.Fill(dstPuntos, "TablaPuntos")
        'daMotobombas.SelectCommand.CommandText = "select *, CodigoPVYCR+' ('+ IdElementoMedida + ')' as CodigoElementoMedida from PVYCR_Calculos_ElementosMedida where PVYCR_Calculos_ElementosMedida.idCalculo=" & lblCalculoSel.Text
        'daMotobombas.Fill(dstMotobombas, "TablaCalculosElementosMedida")

    End Sub

    Protected Sub nuevopunto(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPuntoSel.Text = ""
        Response.Redirect("Puntos.aspx?nuevo=yes")

    End Sub


    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)
        Dim comandoArbol As SqlCommand = New SqlCommand("", conexion)
        Dim nivel As Integer = 0
        Dim y As Integer = 0
        Dim strNodoPadre As String = ""
        Dim esTelemedida As String = ""

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        Try
            If lblPuntoSel.Text = "" Then
                'Insertamos un nuevo punto
                comando.CommandText = " INSERT INTO [SIGVECTOR].[dbo].[PVYCR_Puntos] " & _
                                      " ([CodigoPVYCR],[CodigoCauce],[DenominacionPunto],[Observaciones],[TipoSensor]," & _
                                      "  [Acceso],[X_UTM],[Y_UTM],[PorcentajeRegable], [Diametro_mm]," & _
                                      "  [FactorFlotador] ,[TipoPunto],[CodigoDataLogger],[UsadoEnParteOficial],[PKS],[PKA]," & _
                                      "  [RIO],[Longitud] ,[LongitudFlotador],[A1_M],[A2_M],[A3_M],[B1_M] ,[B2_M],[B3_M],[B4_M],[H12_M]," & _
                                      "  [H23_M],[H34_M],[Offset_M],[Antiguo],favorito,fecha_inicion,fecha_fin)" & _
                                      "  VALUES (@CodigoPVYCR,@CodigoCauce,@DenominacionPunto,@Observaciones,@TipoSensor," & _
                                      "  @Acceso,@X_UTM,@Y_UTM,@PorcentajeRegable,@Diametro_mm," & _
                                      "  @FactorFlotador ,@TipoPunto,@CodigoDataLogger,@UsadoEnParteOficial,@PKS,@PKA," & _
                                      "  @RIO,@Longitud ,@LongitudFlotador,@A1_M,@A2_M,@A3_M,@B1_M ,@B2_M,@B3_M,@B4_M,@H12_M," & _
                                      "  @H23_M,@H34_M,@Offset_M,0,@favorito,@fecha_inicion,@fecha_fin)"





            Else
                'EGB2: añado esto CodigoPVYCR=@CodigoPVYCR,
                comando.CommandText = "UPDATE PVYCR_Puntos SET CodigoPVYCR=@CodigoPVYCR, " & _
                                      "  CodigoCauce=@CodigoCauce,DenominacionPunto=@DenominacionPunto,Observaciones=@Observaciones,TipoSensor=@TipoSensor," & _
                                      "  Acceso=@Acceso,X_UTM=@X_UTM,Y_UTM=@Y_UTM,PorcentajeRegable=@PorcentajeRegable,Diametro_mm=@Diametro_mm," & _
                                      "  FactorFlotador=@FactorFlotador ,TipoPunto=@TipoPunto,CodigoDataLogger=@CodigoDataLogger,UsadoEnParteOficial=@UsadoEnParteOficial,PKS=@PKS,PKA=@PKA," & _
                                      "  RIO=@RIO,Longitud=@Longitud ,LongitudFlotador=@LongitudFlotador,A1_M=@A1_M,A2_M=@A2_M,A3_M=@A3_M,B1_M=@B1_M ,B2_M=@B2_M,B3_M=@B3_M,B4_M=@B4_M,H12_M=@H12_M," & _
                                      "  H23_M=@H23_M,H34_M=@H34_M,Offset_M=@Offset_M,favorito=@favorito,fecha_inicion=@fecha_inicion,fecha_fin=@fecha_fin " & _
                                      "where CodigoPVYCR='" & Me.lblPuntoSel.Text & "' "
            End If

            comando.Parameters.AddWithValue("codigoPVYCR", utiles.BlancoANull(Me.txtcodigoPunto.Text))

            comando.Parameters.AddWithValue("CodigoCauce", utiles.BlancoANull(txtcodigocauce.Text)) 'para que no meta una fecha mñinima cuando el campo es nulo (que al ser un textbox es "")
            comando.Parameters.AddWithValue("DenominacionPunto", utiles.BlancoANull(Me.txtdenominacion.Text))
            If pnlNOVerGraficoAcequias.Visible = True Then
                comando.Parameters.AddWithValue("Acceso", utiles.BlancoANull(Me.txtAccesoA.Text))
                comando.Parameters.AddWithValue("Observaciones", utiles.BlancoANull(Me.txtObservacionesA.Text))
            Else
                comando.Parameters.AddWithValue("Acceso", utiles.BlancoANull(Me.txtacceso.Text))
                comando.Parameters.AddWithValue("Observaciones", utiles.BlancoANull(Me.txtobservaciones.Text))
            End If

            comando.Parameters.AddWithValue("TipoSensor", utiles.BlancoANull(Me.txtSensor.Text))

            comando.Parameters.AddWithValue("X_UTM", utiles.BlancoANull(Me.txtX.Text))
            comando.Parameters.AddWithValue("Y_UTM", utiles.BlancoANull(Me.txtY.Text))
            comando.Parameters.AddWithValue("PorcentajeRegable", utiles.BlancoANull(Replace(Me.txtRegable.Text, ",", ".")))

            comando.Parameters.AddWithValue("Diametro_mm", utiles.BlancoANull(Me.txtDiametro.Text))
            comando.Parameters.AddWithValue("FactorFlotador", utiles.BlancoANull(Replace(Me.txtFactor.Text, ",", ".")))
            If (Me.ddlTipoPunto.SelectedItem.Value = "0") Then
                comando.Parameters.AddWithValue("TipoPunto", "")
            Else
                comando.Parameters.AddWithValue("TipoPunto", Me.ddlTipoPunto.SelectedItem.Value)
            End If
            comando.Parameters.AddWithValue("CodigoDataLogger", utiles.BlancoANull(Me.txtDataLogger.Text))
            If chkUso.Checked Then
                comando.Parameters.AddWithValue("UsadoEnParteOficial", "1")
            Else
                comando.Parameters.AddWithValue("UsadoEnParteOficial", "0")
            End If
            comando.Parameters.AddWithValue("PKS", utiles.BlancoANull(Replace(Me.txtPKS.Text, ",", ".")))
            comando.Parameters.AddWithValue("PKA", utiles.BlancoANull(Replace(Me.txtPKA.Text, ",", ".")))
            comando.Parameters.AddWithValue("RIO", utiles.BlancoANull(Me.txtRIO.Text))

            comando.Parameters.AddWithValue("Longitud", utiles.BlancoANull(Me.txtLongitud.Text))
            comando.Parameters.AddWithValue("LongitudFlotador", utiles.BlancoANull(Replace(Me.txtFlotador.Text, ",", ".")))
            comando.Parameters.AddWithValue("A1_M", utiles.BlancoANull(Replace(Me.txtA1_M.Text, ",", ".")))
            comando.Parameters.AddWithValue("A2_M", utiles.BlancoANull(Replace(Me.txtA2_M.Text, ",", ".")))
            comando.Parameters.AddWithValue("A3_M", utiles.BlancoANull(Replace(Me.txtA3_M.Text, ",", ".")))
            comando.Parameters.AddWithValue("B1_M", utiles.BlancoANull(Replace(Me.txtB1_M.Text, ",", ".")))
            comando.Parameters.AddWithValue("B2_M", utiles.BlancoANull(Replace(Me.txtB2_M.Text, ",", ".")))

            comando.Parameters.AddWithValue("B3_M", utiles.BlancoANull(Replace(Me.txtB3_M.Text, ",", ".")))
            comando.Parameters.AddWithValue("B4_M", utiles.BlancoANull(Replace(Me.txtB4_M.Text, ",", ".")))
            comando.Parameters.AddWithValue("H12_M", utiles.BlancoANull(Replace(Me.txtH12_M.Text, ",", ".")))
            comando.Parameters.AddWithValue("H23_M", utiles.BlancoANull(Replace(Me.txtH23_M.Text, ",", ".")))
            comando.Parameters.AddWithValue("H34_M", utiles.BlancoANull(Replace(Me.txtH34_M.Text, ",", ".")))
            comando.Parameters.AddWithValue("Offset_M", utiles.BlancoANull(Replace(Me.txtOffset.Text, ",", ".")))
            comando.Parameters.AddWithValue("favorito", utiles.BlancoANull(Me.ddlFavorito.SelectedValue))
            If txtFechaInicion.Text = "" Then
                comando.Parameters.AddWithValue("fecha_inicion", System.DBNull.Value)
            Else
                comando.Parameters.AddWithValue("fecha_inicion", Me.txtFechaInicion.Text)
            End If
            If txtFechaFin.Text = "" Then
                comando.Parameters.AddWithValue("fecha_fin", System.DBNull.Value)
            Else
                comando.Parameters.AddWithValue("fecha_fin", Me.txtFechaFin.Text)
            End If

            comando.ExecuteNonQuery()
            Application("PVYCR_Arbol_Updated") = "NOK"

            'EGB 26/11/2008 Revisión de Codigo de Inserción en el Arbol.
            '-------------------------------------------------------------
          
            '---------------------------------------
            Dim IdArbolRaiz As String
            'Calcular El nodo padre al que se asociará el Nuevo Punto
            IdArbolRaiz = Me.txtIdArbol.Text
            Dim strnodoPVYCRArbol As String
            Dim strDescripcion As String

            If ddlTipoPunto.SelectedValue = "M" Then
                'Punto Motor
                strDescripcion = Me.txtcodigoPunto.Text & "(M - " & Me.txtSensor.Text & ") " & Me.txtdenominacion.Text
                strDescripcion = Left(Trim(strDescripcion), 200)
                strnodoPVYCRArbol = "<img src='images/puntoMotor.ico' border=0>&nbsp;&nbsp;<font color=red>" & strDescripcion

            ElseIf ddlTipoPunto.SelectedValue = "G" Then
                'Punto Acequia
                strDescripcion = Me.txtcodigoPunto.Text & "(G - " & Me.txtSensor.Text & ") " & Me.txtdenominacion.Text
                strDescripcion = Left(Trim(strDescripcion), 200)
                strnodoPVYCRArbol = "<img src='images/puntoAcequia2.gif' border=0>&nbsp;&nbsp;<font color=red>" & strDescripcion
            ElseIf ddlTipoPunto.SelectedValue = "P" Then
                'Punto peaje
                strDescripcion = Me.txtcodigoPunto.Text & "(P - " & Me.txtSensor.Text & ") " & Me.txtdenominacion.Text
                strDescripcion = Left(Trim(strDescripcion), 200)
                strnodoPVYCRArbol = "<img src='images/puntoAcequia2.gif' border=0>&nbsp;&nbsp;<font color=red>" & strDescripcion
            ElseIf ddlTipoPunto.SelectedValue = "T" Then
                'Punto trasvase
                strDescripcion = Me.txtcodigoPunto.Text & "(T - " & Me.txtSensor.Text & ") " & Me.txtdenominacion.Text
                strDescripcion = Left(Trim(strDescripcion), 200)
                strnodoPVYCRArbol = "<img src='images/puntoAcequia2.gif' border=0>&nbsp;&nbsp;<font color=red>" & strDescripcion
            End If
            strnodoPVYCRArbol = Replace(strnodoPVYCRArbol, "'", "''")
          
            If Me.txtSensor.Text = "" Then
                esTelemedida = "N"
            Else
                esTelemedida = "S"
            End If
            utiles.Comprobar_Conexion_BD(Page, conexion)
            If lblPuntoSel.Text = "" Then
                'NUEVO PUNTO -->INSERTAMOS NODO FICTICIO EN PVYCR_ARBOL
                '------------------------------------------------------------------
                'ncm obtenemos el nivel del nodo
                nivel = ObtenerNivelNodo(IdArbolRaiz, Page, "P")
                y = GetY(IdArbolRaiz, nivel, "P", Page)

                strNodoPadre = ObtenerStrNodo(txtcodigocauce.Text, "P", Page)
                'Calcular la clave del nodo en PVYCR_Arbol
                comando.CommandText = "INSERT INTO PVYCR_Arbol ([IdArbolPadre],[strnodo],[descripcion],[tipo],[clavenodo],[CodigoPVYCR],x,y,esTelemedida,strNodoPadre)VALUES ('" & IdArbolRaiz & "','" & strnodoPVYCRArbol & "','" & strDescripcion & "','P','" & Me.txtcodigoPunto.Text & "#P','" & Me.txtcodigoPunto.Text & "'," & nivel & "," & y & ",'" & esTelemedida & "','" & strNodoPadre & "' )"
                comando.ExecuteNonQuery()

            Else
                'EDICION PUNTO ACTUALIZAMOS EL ARBOL
                comando.CommandText = "UPDATE PVYCR_arbol SET strnodo='" & strnodoPVYCRArbol & "',descripcion='" & strDescripcion & "', " & _
                                      "clavenodo='" & Me.txtcodigoPunto.Text & "#P', " & _
                                      "esTelemedida='" & esTelemedida & "' " & _
                                      "where CodigoPVYCR='" & Me.lblPuntoSel.Text & "' AND TIPO='P'"
                comando.ExecuteNonQuery()

            End If
            '---------------------------------------------------------------------

        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                'Case 547
                '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    Alert(Page, "El Punto ya existe ")
            End Select
        End Try


        Me.pnlPuntos.Visible = True
        Me.pnlEDPuntos.Visible = False
        'Me.ucPaginacion.Visible = True
        'ucPaginacion.lblPaginatext = "1"
        rellenarListas()
        crearDataSets()

        'ucPaginacion.lblNumpaginasDatabind()

    End Sub


    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.pnlPuntos.Visible = True
        Me.pnlEDPuntos.Visible = False
        'Me.ucPaginacion.Visible = True
        'ucPaginacion.lblPaginatext = "1"

        crearDataSets()
        'ucPaginacion.lblNumpaginasDatabind()

    End Sub




    'Protected Sub Formateo_controles_cliente(ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)

    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtRegable"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtRegable"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtB"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtB"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtB2"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtB2"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtH"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtH"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtTi"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtTi"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtTd"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtTd"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtFactor"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtFactor"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtPKS"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtPKS"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtPKA"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtPKA"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtFlotador"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtFlotador"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtA1_M"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtA1_M"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtA2_M"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtA2_M"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtB1_M"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtB1_M"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtB2_M"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtB2_M"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtB3_M"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtB3_M"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtH1_M"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtH1_M"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtH2_M"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtH2_M"), TextBox))
    '    JavaScript.controlFormatea(Page, CType(e.Item.FindControl("txtOffset"), TextBox))
    '    JavaScript.controlDesFormatea(Page, CType(e.Item.FindControl("txtOffset"), TextBox))
    'End Sub


    Protected Sub rptPuntos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptPuntos.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String
        Dim objTrans As SqlTransaction

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Session("nuevoPunto") = 0
                Me.pnlEDPuntos.Visible = True
                Me.pnlPuntos.Visible = False
                txtcodigocauce.Enabled = False

                'IPIM 29/07/2010: Esto lo comento porque no hay que cargar el árbol si se modifica.
                ''EGB2: Cambio la localización de esta línea. Sólo se carga el árbol al modificar un elemento.
                'If treeView1.Nodes.Count = 0 Then
                '    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "P") 'EGB el nuevo parametro P hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
                'End If

                btnAceptar.Visible = True
                'Me.ucPaginacion.Visible = False
                lblPuntoSel.Text = e.CommandArgument
                crearDataSetPunto()
                'ncm se utiliza para mostrar el gráfico
                Session.Item("codigoPVYCR") = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("CodigoPVYCR"))
                If dstPuntos.Tables("TablaPuntos").Rows.Count > 0 Then
                    ''Datos del Punto
                    Me.txtcodigoPunto.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("CodigoPVYCR"))


                    Me.txtcodigocauce.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("CodigoCauce"))
                    Me.txtdenominacion.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("DenominacionPunto"))
                    Me.txtSensor.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("TipoSensor"))

                    Me.txtX.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("X_UTM"))
                    Me.txtY.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Y_UTM"))


                    Me.txtRegable.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("PorcentajeRegable"))

                    Me.txtDiametro.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Diametro_mm"))
                    Me.txtFactor.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("FactorFlotador"))
                    If utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("TipoPunto")) = "" Then
                        Me.ddlTipoPunto.SelectedValue = "0"
                    Else

                        Me.ddlTipoPunto.SelectedValue = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("TipoPunto"))
                    End If



                    Me.txtDataLogger.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("CodigoDataLogger"))
                    If (dstPuntos.Tables("TablaPuntos").Rows(0).Item("UsadoEnParteOficial") = "1") Then
                        Me.chkUso.Checked = True
                    Else
                        Me.chkUso.Checked = False
                    End If

                    Me.txtPKS.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("PKS"))
                    Me.txtPKA.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("PKA"))
                    Me.txtRIO.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("RIO"))
                    Me.txtLongitud.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Longitud"))
                    Me.txtFlotador.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("LongitudFlotador"))


                    Me.txtA1_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("A1_M"))
                    Me.txtA2_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("A2_M"))
                    Me.txtA3_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("A3_M"))
                    Me.txtB1_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("B1_M"))
                    Me.txtB2_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("B2_M"))
                    Me.txtB3_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("B3_M"))
                    Me.txtB4_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("B4_M"))
                    Me.txtH12_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("H12_M"))
                    Me.txtH23_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("H23_M"))
                    Me.txtH34_M.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("H34_M"))
                    Me.txtOffset.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Offset_M"))
                    If utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("favorito").ToString.ToUpper) = "" Then
                        Me.ddlFavorito.SelectedValue = "NO"
                    Else
                        Me.ddlFavorito.SelectedValue = dstPuntos.Tables("TablaPuntos").Rows(0).Item("favorito").ToString.ToUpper
                    End If

                    Me.txtFechaInicion.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("fecha_inicion"))
                    Me.txtFechaFin.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("fecha_fin"))
                    VisualizarPanelGrafico(ddlTipoPunto.SelectedValue)
                    If pnlVerGraficoAcequias.Visible = True Then
                        Me.txtobservaciones.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Observaciones"))
                        Me.txtacceso.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Acceso"))
                    Else
                        Me.txtObservacionesA.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Observaciones"))
                        Me.txtAccesoA.Text = utiles.nullABlanco(dstPuntos.Tables("TablaPuntos").Rows(0).Item("Acceso"))
                    End If
                    'IPIM 29/07/2010: No se puede modificar el código del punto ni el código del cauce asociado.
                    txtcodigoPunto.Enabled = False
                    imgArbol.Attributes.Add("onclick", "jscript:alert('No es posible reasociar el punto.\n Elimine el Punto y establezca la asociación correctamente.');")
                End If
                rptPuntos.DataSource = dstPuntos.Tables("TablaPuntos")
                rptPuntos.DataBind()


            Case "borrar"
                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion)
                objTrans = conexion.BeginTransaction()

                Try
                    comando.Transaction = objTrans
                    'En primer lugar, se eliminan las relaciones que tiene la motobomba
                    'comando.CommandText = "delete from PVYCR_ElementosMedida_MotoBombas where CodigoMotobomba='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
                    comando.ExecuteNonQuery()
                    'Se elimina la motobomba correspondiente
                    'comando.CommandText = "delete from PVYCR_MotoBombas where CodigoMotobomba='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
                    comando.ExecuteNonQuery()
                    'Borramos el punto del árbol
                    'comando.CommandText = "delete FROM pvycr_ARBOL WHERE CODIGOPVYCR = '" & lblPuntoSel.Text & "' "

                    objTrans.Commit()

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el punto")
                    objTrans.Rollback()
                End Try
                crearDataSets()
                'ucPaginacion.lblNumpaginasDatabind()
            Case "perfilAcequia"
                'RDF. Fecha: 04/09/2008
                'Generar Perfil de la acequia



        End Select
    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function


    'Protected Sub rptPuntos_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptPuntos.ItemCreated
    '    'Sólo se dispara una vez, cuando pasa por el repeater.
    '    ' solo funciona si los items son los del cuerpo, sino da error, por eso ponemos el IF
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        ' rellenamos las listas de incidencias eléctricas y las volumétricas
    '        Formateo_controles_cliente(e)
    '    End If
    'End Sub

    Protected Sub treeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView1.SelectedNodeChanged
        Dim TipoNodo As String
        Dim v() As String
        v = Split(treeView1.SelectedNode.Value, "#")
        TipoNodo = v(1)
        Select Case TipoNodo
            Case "C"
                If VerDependencia() = False Then
                    VerificarPunto(v(0).ToString())
                    lblDesCodigoCauce.Text = "Cauce de Asociación: " & treeView1.SelectedNode.Text
                    txtcodigocauce.Text = v(0).ToString()
                    txtIdArbol.Text = v(2) 'IdArbol en PVYCR_Arbol
                Else
                    'EGB 04/02/2009 Correccion para alertar un cambio no autorizado en la definicion del codigo del punto.
                    Alert(Page, "El código de punto introducido tiene registros relacionados./nCambie el código o borre los registros relacionados")
                End If
            Case "R"
                Alert(Page, "Seleccione un cauce del nivel inferior.")

            Case Else
                Alert(Page, "Seleccione un cauce al que asociar el punto.")
        End Select

    End Sub

    Private Function VerDependencia() As Boolean
        Dim resultado As Boolean

        Dim comando As SqlCommand = New SqlCommand("", conexion)
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)

        comando.CommandText = "SELECT count(*) from PVYCR_ElementosMedida where CodigoPVYCR='" & txtcodigoPunto.Text & "'"
        If comando.ExecuteScalar() > 0 Then
            resultado = True
        Else
            resultado = False
        End If
        conexion.Close()
        Return resultado
    End Function

    Private Sub VerificarPunto(ByVal codigoCauce As String)
        If txtcodigocauce.Text <> "" Then   'Si estamos modificando
            Dim ultimoElem As String
            Dim punto As Integer

            Dim comando As SqlCommand = New SqlCommand("", conexion)
            'If conexion.State = ConnectionState.Closed Then conexion.Open()
            utiles.Comprobar_Conexion_BD(Page, conexion)

            Dim v As String

            v = Mid(txtcodigoPunto.Text, Len(txtcodigocauce.Text) + 1)
            comando.CommandText = "SELECT count(*) from PVYCR_Puntos where CodigoPVYCR='" & codigoCauce & v & "'"

            If comando.ExecuteScalar > 0 Then   'Si no existe el punto, lo renombramos y si existe le asignaremos otro.
                comando.CommandText = "SELECT codigoPVYCR from PVYCR_Puntos where CodigoCauce='" & codigoCauce & "' ORDER BY codigoPVYCR desc"
                ultimoElem = comando.ExecuteScalar
                punto = Right(ultimoElem, 1)
                ultimoElem = Left(ultimoElem, Len(ultimoElem) - 1) & CStr(punto + 1)
                Alert(Page, "Se ha asignado un nuevo valor al punto, porque " & codigoCauce & v & " ya existía")
                txtcodigoPunto.Text = ultimoElem
            Else
                txtcodigoPunto.Text = codigoCauce & v
            End If

            conexion.Close()
        End If
    End Sub
    Protected Function esAcequia(ByVal Tipo As Object, Optional ByVal codigoPunto As String = "")
        'RDF. Fecha: 04/09/2008
        'Función que oculta/visualiza el icono de gráficos dependiendo
        'del valor del tipo de punto

        'IPIM 28/06/2010: También lo ocultamos si no tiene valores para la gráfica.
        Dim DataSet As New DataSet
        If codigoPunto <> "" Then
            Dim selectConnection As New SqlClient.SqlConnection(ConfigurationSettings.AppSettings.Item("dsnsegura_migracion"))
            Dim adapter As New SqlClient.SqlDataAdapter("", selectConnection)

            utiles.Comprobar_Conexion_BD(Page, selectConnection)
            adapter.SelectCommand.CommandText = "SELECT c.DenominacionCauce,DenominacionPunto, A1_M,A2_M,A3_M,B1_M,B2_M,B3_M,B4_M,H12_M,H23_M,H34_M,Offset_M " & _
                        "FROM PVYCR_Cauces C, PVYCR_Puntos P where P.codigoCauce = C.codigoCauce and P.CodigoPVYCR = '" + codigoPunto + "'"
            adapter.Fill(DataSet, "TablaGrafico")

            If DataSet.Tables("TablaGrafico").Rows.Count = 0 Then
                Return False
                Exit Function
            End If
            If CalculoUltimoValorX(DataSet.Tables("TablaGrafico")) <= 0 Then
                Return False
                Exit Function
            End If
        End If

        Dim TipoPunto As String = ""
        TipoPunto = nullABlanco(Tipo)

        If TipoPunto = "G" Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub AceptarFiltro(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081002
        Dim strFiltro As String
        strFiltro = ""
        'If txtFCodigoCauce.Text <> "" Then
        '    strFiltro = strFiltro + " AND CodigoCauce like '" + txtFCodigoCauce.Text + "' "
        'End If

        If txtFCodigoPVYCR.Text <> "" Then
            strFiltro = strFiltro + " AND CodigoPVYCR like '" + txtFCodigoPVYCR.Text + "' "
        End If

        If txtFDenominacionPunto.Text <> "" Then
            strFiltro = strFiltro + " AND DenominacionPunto like '" + txtFDenominacionPunto.Text + "' "
        End If
        If txtFiltrado.Text <> "" And ddlFiltrado.SelectedIndex <> 0 Then
            strFiltro = strFiltro + " AND " + ddlFiltrado.SelectedValue + " LIKE '" + txtFiltrado.Text + "'"
        End If
        'If ddlFTipoPunto.SelectedValue = "G" Then
        '    strFiltro = strFiltro + " AND TipoPunto='G' "
        'ElseIf ddlFTipoPunto.SelectedValue = "M" Then
        '    strFiltro = strFiltro + " AND TipoPunto='M' "
        '    'ncm 09/03/2009
        'ElseIf ddlFTipoPunto.SelectedValue = "P" Then
        '    strFiltro = strFiltro + " AND TipoPunto='P' "
        'ElseIf ddlFTipoPunto.SelectedValue = "T" Then
        '    strFiltro = strFiltro + " AND TipoPunto='T' "
        'End If


        Session("FiltroPunt") = strFiltro
        crearDataSets()
        'rellenarListas()
    End Sub

    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        'RDF 
        '29/10/2008
        crearDataSets()

    End Sub
    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim nombreinforme = "../listados/InformePuntos.aspx?nodoClave=" + utiles.BlancoANull(Me.txtcodigoPunto.Text)
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub VisualizarPanelGrafico(ByVal tipoPunto As String)
        Dim acequia As Boolean = esAcequia(tipoPunto)
        If acequia = True Then
            Me.pnlVerGraficoAcequias.Visible = True
            Me.pnlNOVerGraficoAcequias.Visible = False
        Else
            Me.pnlVerGraficoAcequias.Visible = False
            Me.pnlNOVerGraficoAcequias.Visible = True
        End If
    End Sub

    Protected Sub btnVerIncidencias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVerIncidencias.Click
        Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
                "window.open('../listados/ListadoIncidenciasMotor.aspx','_blank','')" & _
        "</script>")
    End Sub
    Protected Function VisibleSegunPerfil2() As String
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return "block"
        Else
            Return "none"
        End If

    End Function
    'Function ObtenerNivelPunto(ByVal idPadre As Integer) As Integer
    '  sentenciaSel = "SELECT X FROM PVYCR_Arbol WHERE IdArbol=" & idPadre
    '  daPuntos.SelectCommand.CommandText = sentenciaSel
    '  Return daPuntos.SelectCommand.ExecuteScalar()
    'End Function
    'Private Function GetY(ByVal IdArbolPadre As Integer, ByVal x As Integer) As Integer
    '  Dim midstArbol As New DataSet
    '  Dim y As Integer = 0

    '  'ncm 27/04/2010 obtenemos la Y del árbol para rellenarla
    '  sentenciaSel = "SELECT MAX(y)  FROM PVYCR_Arbol WHERE idarbolpadre=" & IdArbolPadre & " and tipo = 'P' AND X=" & x
    '  daPuntos.SelectCommand.CommandText = sentenciaSel
    '  If IsDBNull(daPuntos.SelectCommand.ExecuteScalar) Then
    '    y = 0
    '  Else
    '    y = y + 1
    '  End If
    '  Return y
    'End Function
    'Private Function ObtenerStrNodo(ByVal caucePadre As String) As String
    '  sentenciaSel = "select strnodoPadre from pvycr_arbol where CODIGOCAUCE='" & caucePadre & "' "
    '  daPuntos.SelectCommand.CommandText = sentenciaSel
    '  Return utiles.nothingABlanco(daPuntos.SelectCommand.ExecuteScalar)
    'End Function
    Protected Sub ModificarEsTelemedida(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim esTelemedida As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        comando.CommandText = "UPDATE PVYCR_Arbol SET esTelemedida=@esTelemedida " & _
                                 "where CodigoPVYCR='" & Me.lblPuntoSel.Text & "' "
        If txtSensor.Text = "" Then
            esTelemedida = "N"
        Else
            esTelemedida = "S"
        End If
        comando.Parameters.AddWithValue("esTelemedida", esTelemedida)
        comando.ExecuteNonQuery()
    End Sub
    Protected Sub rellenarListas()
        Dim dstFiltrado As New DataSet
        'ncm rellenamos el campo filtrado con las columnas de la tabla de cauces
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daPuntos.SelectCommand.CommandText = " SELECT Column_Name as columna  FROM INFORMATION_SCHEMA. COLUMNS where Table_Name = 'PVYCR_PUNTOS' and COLUMN_NAME IN ('" & _
                       " CodigoCauce','TipoSensor', 'Acceso', 'X_UTM', 'Y_UTM', 'PorcentajeRegable', 'Diametro_mm', " & _
                       " 'FactorFlotador', 'TipoPunto', 'CodigoDataLogger', 'UsadoEnParteOficial', 'PKS', 'PKA', 'RIO', 'Longitud', 'LongitudFlotador', 'A1_M', 'A2_M', 'A3_M', 'B1_M', 'B2_M', 'B3_M',  " & _
                       " 'B4_M','H12_M', 'H23_M','H34_M', 'Offset_M', 'Antiguo', 'Observaciones','Favorito') "
        daPuntos.Fill(dstFiltrado, "TablaFiltrado")
        ddlFiltrado.DataSource = dstFiltrado.Tables("TablaFiltrado")
        ddlFiltrado.DataValueField = "columna"
        ddlFiltrado.DataBind()
        ddlFiltrado.Items.Insert(0, New ListItem("[Seleccionar]", ""))

    End Sub
    Protected Function ObtenerFiltrado(ByVal eldataitem As DataRowView) As String
        If utiles.nullABlanco(txtFiltrado.Text) <> "" And utiles.nullABlanco(ddlFiltrado.SelectedValue) <> "" Then
            Return eldataitem("" + ddlFiltrado.SelectedValue + "")
        End If
    End Function
End Class
