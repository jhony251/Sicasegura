﻿Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports CalculosSica.SICA_FuncionesCalcVolDiferencial

Partial Class SICAH_caucepuntDETALLE
    Inherits System.Web.UI.Page

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim daPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstPuntos As DataSet = New System.Data.DataSet()
    'RDF 11/09/2008
    Dim tCopiaSeleccionLecturas As DataTable = New System.Data.DataTable()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim codigoCauce As String = ""

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder, sentenciaSelCount, sentenciaSel_m3 As String
    Dim parfila As Integer = 0
    Dim numfila As Integer = 0
    Dim informe As String = "xls"
    Dim cadena As String = ""
    Dim dstPantalla As System.Data.DataSet = New System.Data.DataSet
    Dim NumRegPorPag As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim GraficoCurvas As Boolean = False
    Dim mensaje_final As String = ""
    'Protected WithEvents txtUpload As System.Web.UI.HtmlControls.HtmlInputFile

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        formateo_controles_cliente()
        Page.MaintainScrollPositionOnPostBack = True

        'RDF 20081007
        'Variables de sesión utilizadas en la visualización del visor
        Session("modo") = "X"
        Session("Acequias") = 1
        Session("Motores") = 1
        Session("Telemandos") = 0
        Session("Aportaciones") = 1
        Session("Edar") = 1
        Session("Gravedad") = 1
        Session("CauceMotor") = 1
        Session("Noria") = 1
        Session("Pozo") = 1
        Session("Retorno") = 1

        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")

        'Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
        '                       "function abreInforme(control) { if (control.id == 'btnInformeA') " & _
        '                          " var nombreinforme = '../listados/InformeVolumenes.aspx?" & _
        '                        "codigoPVYCR = " & txtCodigoPVYCR.Text & "&idElementoMedida = " & txtIdElementoMedida.Text & _
        '                  "&FiltroNregQ=" & txtFiltroNregQ.Text & "&FiltroNulasQ=" & chkFiltroNulasQ.Checked & _
        '                  "&filtroFechaIni=" & txtfiltroFechaIni.Text & "&FiltroFechaFin=" & txtFiltroFechaFin.Text & _
        '                  "&FiltrarCodFuenteDato=" & txtFiltrarCodFuentedato.Text & "&informe=" & informe & "'; " & _
        '                  "else nombreinforme = 'casa.aspx'; " & _
        '                          "window.open(nombreinforme,'_blank','');}" & _
        '                       "</script>")
        If Not IsPostBack Then
            'EGB 21042008
            'lblCabecera.Text = genHTML.cabecera(Page)
            'lblPestanyas.Text = genHTML.pestanyasMenu(4, "../", Page)
            If utiles.nullABlanco(Request.QueryString("nodotexto")) <> "" Then
                If Session("usuarioReg") = Nothing Then
                    Response.Redirect("UsuarioNoRegistrado.aspx")
                    Exit Sub
                End If
                pnlIndependiente.Visible = True     'ipim

                'RDf 30/07/2008
                ' Se le pasa el evente también a las etiquetas
                'imgVolDiariosA.Attributes.Add("onclick", "abreInforme(this,'','','')")
                'btnVolDiariosA.Attributes.Add("onclick", "abreInforme(this,'','','')")
                'imgInformeA.Attributes.Add("onclick", "abreInforme(this,'" & Request.QueryString("nodotexto") & "','" & Request.QueryString("nodoclave") & "','')")
                'btnInformeA.Attributes.Add("onclick", "abreInforme(this,'" & Request.QueryString("nodotexto") & "','" & Request.QueryString("nodoclave") & "','')")
                'imgCurvasAcequias.Attributes.Add("onclick", "abreInforme(this,'','','')")
                'imgComparativaCaudales.Attributes.Add("onclick", "abreInforme(this,'" & Request.QueryString("nodotexto") & "','" & Request.QueryString("nodoclave") & "','')")
                'btnComparativaCaudales.Attributes.Add("onclick", "abreInforme(this,'" & Request.QueryString("nodotexto") & "','" & Request.QueryString("nodoclave") & "','')")
                'imgCalFechaIniI.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniI.ClientID & "'),'dd/mm/yyyy');")
                'imgCalFechaFinI.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinI.ClientID & "'),'dd/mm/yyyy');")

                ''Le quitamos los enlaces para que no recargue la página
                'btnVolDiariosA.Attributes.Add("href", "javascript:void(0);")
                'btnInformeA.Attributes.Add("href", "javascript:void(0);")
                'btnComparativaCaudales.Attributes.Add("href", "javascript:void(0);")

                LBLinfo.Text = Request.QueryString("nodotexto").ToString
                ActivarPanelDetalles(Request.QueryString("nodotexto").ToString, Request.QueryString("nodoclave").ToString)



                'NCM 22/10/2008

            Else    'ipim
                pnlIndependiente.Visible = False
                'ncm 03/11/2008 incluimos la presentación únicamente si es 
                ' la primera vez que entran
                lblpresentacion.Text = genHTML.ObtenerPresentacion()
            End If

        End If
    End Sub

    Private Sub crearDataSetsPuntos(ByVal codigoPVYCR As String, ByRef TipoPunto As String)
        'ncm 22052008 comentado porque ya no se utiliza
        '  dstarbol.Clear()
        '  'Cauces Padre
        '  daArbol.SelectCommand.CommandText = "SELECT DISTINCT LEFT(C.codigoCauce,2) as caucepadre from [PVYCR_Cauces] C where left(C.codigoCauce,1)<>'S' " & _
        '  " and left(c.codigoCauce,2) <> 'AR' and left(c.codigoCauce,2)<> 'OT' " & _
        ' "UNION " & _
        ' "SELECT TOP 1 'ARG' AS caucepadre from PVYCR_Cauces " & _
        ' "UNION " & _
        ' "SELECT TOP 1 'OT000 hasta OT099' AS caucepadre from PVYCR_Cauces " & _
        '  "UNION " & _
        '  "SELECT TOP 1 'OT100 hasta OT999' AS caucepadre from PVYCR_Cauces " & _
        '  "UNION " & _
        ' "SELECT TOP 1 'S' AS caucepadre from PVYCR_Cauces"
        '  daArbol.Fill(dstarbol, "tablaCaucesPadres")
        '  'Cauces
        '  daArbol.SelectCommand.CommandText = "SELECT C.codigoCauce, C.DenominacionCauce FROM PVYCR_Cauces C order by C.codigoCauce "
        '  daArbol.Fill(dstarbol, "tablaCauces")
        '  'Puntos
        '  daArbol.SelectCommand.CommandText = "SELECT C.codigoCauce, P.codigoPVYCR, P.tipoPunto, P.denominacionPunto,P.TipoSensor TipoSensor FROM PVYCR_Cauces C " & _
        '" join PVYCR_Puntos P on P.codigocauce=C.codigocauce order by C.codigoCauce, P.codigoPVYCR"
        '  daArbol.Fill(dstarbol, "tablaPuntos")
        '  'elementos de medida
        '  daArbol.SelectCommand.CommandText = "SELECT E.codigoPVYCR, E.idElementoMedida,E.TIPO, DESCTIPO = " & _
        '       "CASE E.Tipo " & _
        '       "WHEN 'Q' THEN 'CAUDAL' " & _
        '       "WHEN 'H' THEN 'HORAS' " & _
        '       "WHEN 'V' THEN 'VOLUMEN' " & _
        '       "WHEN 'E' THEN 'ENERGÍA' " & _
        '      "END from PVYCR_Elementosmedida E, PVYCR_puntos P " & _
        '  "where(E.codigoPVYCR = P.codigoPVYCR) order by E.codigoPVYCR  "
        '  daArbol.Fill(dstarbol, "TablaElementosMedidad")

        utiles.Comprobar_Conexion_BD(Page, conexion)

        'Puntos
        daPuntos.SelectCommand.CommandText = "SELECT P.codigoPVYCR, P.tipoPunto, P.denominacionPunto,P.TipoSensor TipoSensor,P.X_UTM, P.Y_UTM " & _
      " From PVYCR_Puntos P where P.codigoPVYCR = '" & codigoPVYCR & "'"
        daPuntos.Fill(dstPuntos, "tablaPuntos")

        TipoPunto = dstPuntos.Tables("tablaPuntos").Rows(0)("TipoPunto").ToString()
    End Sub

    Protected Sub ActivarPanelDetalles(ByVal codigoPVYCR As String, ByVal TipoElemento As String, ByVal NombreElemento As String, ByVal IdElemento As String)
        Dim guiones As Integer = 0
        'LIMPIAMOS TODOS LOS POSIBLES FILTROS
        txtFiltroFechaFin.Text = ""
        txtfiltroFechaIni.Text = ""
        txtFiltrarCodFuenteDato.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasQ.Checked = True
        'ncm comentado al añadir el filtro de 50
        'txtFiltroNregQ.Text = ""

        txtFiltroFechaFinV.Text = ""
        txtFiltroFechaIniV.Text = ""
        txtFiltrarCodFuenteDatoV.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasV.Checked = True
        'txtFiltroNRegV.Text = ""

        txtFiltroFechaFinE.Text = ""
        txtFiltroFechaIniE.Text = ""
        txtFiltrarCodFuenteDatoE.Text = ""
        'txtFiltroNRegE.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasE.Checked = True
        'chkFiltroPorDiaE.Checked = False

        txtFiltroFechaFinH.Text = ""
        txtFiltroFechaIniH.Text = ""
        txtFiltrarCodFuenteDatoH.Text = ""
        'txtFiltroNRegH.Text = ""
        txtFiltroFechaFinD.Text = ""
        txtFiltroFechaIniD.Text = ""
        txtFiltrarCodFuenteDatoD.Text = ""
        ''EGB 21042008
        ''Obtenemos el número de guines, si tiene dos es porque es un elemento de medida
        'guiones = numeroSeparadores(Me.txtDescripcionElementoMedida.Text.ToString)
        'If guiones = 2 Then
        '    'parámetro código
        '    codigoPVYCR = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        '    'parámetro tipo de elemento
        '    TipoElemento = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        '    ''parámetro nombre elemento
        '    NombreElemento = Replace(treeView1.SelectedNode.Text, "<img src='images/distancias.gif' border=0>&nbsp;&nbsp;<font color=#008500>" & Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4) & "-", "")
        '    IdElemento = NombreElemento.Substring(0, NombreElemento.ToString.IndexOf("-") - 1)
        '    lblIdElementoto.Text = IdElemento

        '    'Cargaremos todas las lecturas para el elemento seleccionado en el arbol
        '    Cargar_Elementos(TipoElemento, codigoPVYCR, IdElemento)
        'End If
        'Obtenemos el número de guines, si tiene dos es porque es un elemento de medida

        'Cargaremos todas las lecturas para el elemento seleccionado en el arbol
        Cargar_Elementos(TipoElemento, codigoPVYCR, IdElemento)

    End Sub

    Protected Sub ActivarPanelDetalles(ByVal nodotexto As String, ByVal nodoclave As String)
        Dim codigoPVYCR, TipoElementoMedida, NombreElemento, IdElementoMedida As String
        Dim separadores As Integer = 0
        Dim guiones As Integer = 0
        Dim strScript As String
        'LIMPIAMOS TODOS LOS POSIBLES FILTROS
        txtFiltroFechaFin.Text = ""
        txtfiltroFechaIni.Text = ""
        txtFiltrarCodFuenteDato.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasQ.Checked = True
        'ncm comentado al añadir el filtro de 50
        'txtFiltroNregQ.Text = ""

        txtFiltroFechaFinV.Text = ""
        txtFiltroFechaIniV.Text = ""
        txtFiltrarCodFuenteDatoV.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasV.Checked = True
        'txtFiltroNRegV.Text = ""

        txtFiltroFechaFinE.Text = ""
        txtFiltroFechaIniE.Text = ""
        txtFiltrarCodFuenteDatoE.Text = ""
        'txtFiltroNRegE.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasE.Checked = True
        chkFiltroPorDiaE.Checked = False

        txtFiltroFechaFinH.Text = ""
        txtFiltroFechaIniH.Text = ""
        txtFiltrarCodFuenteDatoH.Text = ""
        'txtFiltroNRegH.Text = ""
        txtFiltroFechaFinD.Text = ""
        txtFiltroFechaIniD.Text = ""
        txtFiltrarCodFuenteDatoD.Text = ""
        'IPIM 06/03/2008: Faltaban filtros por limpiar
        Session("filtro") = ""

        'EGB 
        Dim V() As String
        V = Split(nodotexto, "-")
        'Obtenemos el número de guines, si tiene dos es porque es un elemento de medida
        separadores = numeroSeparadores(nodoclave)
        guiones = numeroGuiones(nodotexto)
        If separadores = 2 Then
            'parámetro código
            codigoPVYCR = nodoclave.Substring(0, nodoclave.Length - 4)
            'parámetro tipo de elemento
            TipoElementoMedida = nodoclave.Substring(nodoclave.Length - 1, 1)
            ''parámetro nombre elemento
            NombreElemento = Replace(nodotexto, "<img src='images/distancias.gif' border=0>&nbsp;&nbsp;<font color=#008500>" & nodoclave.Substring(0, nodoclave.Length - 4) & "-", "")

            IdElementoMedida = NombreElemento.Substring(0, NombreElemento.ToString.IndexOf("-") - 1)
            IdElementoMedida = V(guiones - 1).ToString
            lblidElemento.Text = IdElementoMedida

            'EGB 21042008 COMPATIBILIDAD CON LA LOCALIZACIÓN BÁSICA 
            Me.txtCodigoPVYCR.Text = nodoclave.Substring(0, nodoclave.Length - 4)
            Session("codigoPVYCR") = txtCodigoPVYCR.Text
            Me.txtIdElementoMedida.Text = IdElementoMedida
            Session("idElementomedida") = IdElementoMedida

            Select Case TipoElementoMedida
                Case "Q"
                    NombreElemento = codigoPVYCR & " - " & IdElementoMedida & " - CAUDAL"
                    imgGrafica.Visible = False
                    txtGrafica.Visible = False
                Case "V"
                    NombreElemento = codigoPVYCR & " - " & IdElementoMedida & " - VOLUMEN"
                    imgGrafica.Visible = False
                    txtGrafica.Visible = False
                Case "E"
                    NombreElemento = codigoPVYCR & " - " & IdElementoMedida & " - ENERGIA"
                    imgGrafica.Visible = False
                    txtGrafica.Visible = False
                Case "H"
                    NombreElemento = codigoPVYCR & " - " & IdElementoMedida & " - HORAS"
                    imgGrafica.Visible = False
                    txtGrafica.Visible = False
                Case "D"
                    NombreElemento = codigoPVYCR & " - " & IdElementoMedida & " - DIFERENCIAL"
                    imgGrafica.Visible = False
                    txtGrafica.Visible = False
                Case Else
                    NombreElemento = ""
                    imgGrafica.Visible = False
                    txtGrafica.Visible = False
            End Select
            txtDescripcionElementoMedida.Text = NombreElemento


        End If
        ''NCM 01/07/2008 en funcion de radio buton que elijan mostraremos las lecturas o el visor
        'If utiles.nullACero(Session("EnlaceE")) = 0 Then

        'End If

        If Request.QueryString("LecturasVisor") = "L" Then
            'ncm 20/10/2008 únicamente cargamos el menú cuando su iframe esté activo
            If Session("VisibleIframe") = "iframeLecturas" Then
                Session("EnlaceE") = 4
            End If
            Session("EnlaceE") = 4
            lblPestanyasArbolQ.Text = genHTML.EnlacesMenuArbol(Session("EnlaceE"), "../", Page, 11, "E", codigoPVYCR, "N")
            'Cargaremos todas las lecturas para el elemento seleccionado en el arbol
            Cargar_Elementos(TipoElementoMedida, codigoPVYCR, IdElementoMedida)
        ElseIf Request.QueryString("LecturasVisor") = "V" Then
            Dim sender As Object
            Dim e As System.Web.UI.ImageClickEventArgs
            'Page.RegisterClientScriptBlock("mostrarCargando", "<script language=javascript>" & _
            '                                      "mostrarCargando()" & _
            '                             "</script>")
            imgVisor_Click(sender, e)
            'Page.RegisterClientScriptBlock("ocultarCargando", "<script language=javascript>" & _
            '                                    "ocultarCargando()" & _
            '                           "</script>")
        End If


        ''EGB 02/07/2008 se trata del mismo codigo inmediantamente anterior comentado de Nay 
        ''que sustituye las querystrings por referencias los valores de sesion del
        ''valor <LecturasVisor>

        'Dim cookie As HttpCookie = Request.Cookies("c_lecturasvisor")

        'If cookie Is Nothing Then
        '    Session("lblLecturasVisor") = ""
        'Else
        '    Session("lblLecturasVisor") = cookie.Value
        'End If

        'If Session("lblLecturasVisor") = "V" Then
        '    Dim sender As Object
        '    Dim e As System.Web.UI.ImageClickEventArgs
        '    imgVisor_Click(sender, e)
        'Else
        '    'Cargaremos todas las lecturas para el elemento seleccionado en el arbol
        '    Cargar_Elementos(TipoElementoMedida, codigoPVYCR, IdElementoMedida)
        'End If
    End Sub


    Protected Function checkFila(ByVal tipo As String, ByVal elDataItem As DataRowView) As String
        parfila = (parfila + 1) Mod 2
        Dim diferencial As Decimal
        If tipo <> "D" Then
            If utiles.nullABlanco(elDataItem("Diferencial")) = "" Then
                diferencial = 0
            Else
                diferencial = elDataItem("Diferencial")
            End If
        End If
        If tipo = "V" Then
            If diferencial < 0 Then
                Return "class=""filaDiferencial"""
            Else
                Return "class=""fila" & parfila & """"
            End If
        ElseIf tipo = "E" Then
            If diferencial < 0 Then
                Return "class=""filaDiferencial"""
            Else
                Return "class=""fila" & parfila & """"
            End If
        ElseIf tipo = "H" Then
            If diferencial < 0 Then
                Return "class=""filaDiferencial"""
            Else
                Return "class=""fila" & parfila & """"
            End If
        Else
            Return "class=""fila" & parfila & """"
        End If


    End Function

    Protected Function VerObservaciones(ByVal elDataItem As DataRowView) As String
        Return utiles.nullABlanco(elDataItem("Observaciones"))
    End Function
    Protected Function VerObservacionesReducidas(ByVal elDataItem As DataRowView) As String
        If utiles.nullABlanco(elDataItem("observaciones")) <> "" Then
            Return Left(utiles.nullABlanco(elDataItem("Observaciones")), 12) & "..."
        Else
            Return ""
        End If
    End Function

    Protected Function VisibleSiModificado(ByVal elDataItem As DataRowView) As Boolean
        If utiles.nullAFalse(elDataItem("modificado")) = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Function obtenerNomElemento() As String
        'EGB 21042008 
        'Return Replace(treeView1.SelectedNode.Text, "<img src='images/distancias.gif' border=0>&nbsp;&nbsp;<font color=#008500>", "")
        Return Me.txtDescripcionElementoMedida.Text
    End Function
    Protected Function obtenerIdElementoMedida() As String
        'EGB 04072008
        'funcion creada para recuperar el id del elemento de medida
        Return Me.txtIdElementoMedida.Text
    End Function
    Protected Function obtenerAnyoHidrologico() As String
        If DateTime.Now.Month < 10 Then
            Return "Año hidrológico: 01/10/" & DateTime.Now.Year - 1 & " - " & DateTime.Today
        Else
            Return "Año hidrológico: 01/10/" & DateTime.Now.Year & " - " & DateTime.Today
        End If
    End Function
    Protected Function obtenerAnyoHidrologicoEtiqueta() As String
        'RDF 01/10/2008
        If DateTime.Now.Month < 10 Then
            Return "Año Hidrológico: 01/10/" & DateTime.Now.Year - 1 & " - " & "30/09/" & DateTime.Now.Year
        Else
            Return "Año Hidrológico: 01/10/" & DateTime.Now.Year & " - " & "30/09/" & DateTime.Now.Year + 1
        End If
    End Function

    Protected Function obtenerNumLecturasQ() As String
        Return dstElementos.Tables("TablaAcequias").Rows.Count
    End Function
    Protected Function obtenerNumLecturasE() As String
        Return dstElementos.Tables("TablaAlimentacion").Rows.Count
    End Function
    Protected Function obtenerNumLecturasV() As String
        Return dstElementos.Tables("TablaMotores").Rows.Count
    End Function
    Protected Function obtenerNumLecturasH() As String
        Return dstElementos.Tables("TablaHorometros").Rows.Count
    End Function
    Protected Function obtenerNumLecturasD() As String
        Return dstElementos.Tables("TablaDiferencial").Rows.Count
    End Function
    Protected Function obtenerIntervaloFechasQ() As String
        Dim n As Integer
        n = dstElementos.Tables("TablaAcequias").Rows.Count
        If n > 0 Then
            If utiles.nullABlanco(dstElementos.Tables("TablaAcequias").Rows(0).ToString) <> "" And utiles.nullABlanco(dstElementos.Tables("TablaAcequias").Rows(n - 1).ToString) <> "" Then
                Return "desde " & dstElementos.Tables("TablaAcequias").Rows(0).Item("Fecha_Medida").ToString.Substring(0, 10) & " hasta " & dstElementos.Tables("TablaAcequias").Rows(n - 1).Item("Fecha_medida").ToString.Substring(0, 10)
            End If
        Else
            Return "desde 00/00/0000 hasta 00/00/0000"
        End If
        'If Me.txtFiltroFechaFin.Text = "" Then
        '    Return "desde 00/00/0000 hasta 00/00/0000"
        'Else
        '    Return "desde " & Me.txtfiltroFechaIni.Text & " hasta " & Me.txtFiltroFechaFin.Text
        'End If
    End Function
    Protected Function obtenerIntervaloFechasE() As String
        'If Me.txtFiltroFechaFinE.Text = "" Then
        Dim n As Integer
        n = dstElementos.Tables("TablaAlimentacion").Rows.Count
        If n > 0 Then
            If utiles.nullABlanco(dstElementos.Tables("TablaAlimentacion").Rows(0).ToString) <> "" And utiles.nullABlanco(dstElementos.Tables("TablaAlimentacion").Rows(n - 1).ToString) <> "" Then
                Return "desde " & dstElementos.Tables("TablaAlimentacion").Rows(0).Item("Fecha_Medida").ToString.Substring(0, 10) & " hasta " & dstElementos.Tables("TablaAlimentacion").Rows(n - 1).Item("Fecha_medida").ToString.Substring(0, 10)
            End If
        Else
            Return "desde 00/00/0000 hasta 00/00/0000"
        End If
        'Else

        'Return "desde " & Me.txtFiltroFechaIniE.Text & " hasta " & Me.txtFiltroFechaFinE.Text
        'End If
    End Function
    Protected Function obtenerIntervaloFechasV() As String
        Dim n As Integer
        n = dstElementos.Tables("TablaMotores").Rows.Count
        'If Me.txtFiltroFechaFinV.Text = "" Then
        If n > 0 Then
            If utiles.nullABlanco(dstElementos.Tables("TablaMotores").Rows(0).ToString) <> "" And utiles.nullABlanco(dstElementos.Tables("TablaMotores").Rows(n - 1).ToString) <> "" Then
                Return "desde " & dstElementos.Tables("TablaMotores").Rows(0).Item("Fecha_Medida").ToString.Substring(0, 10) & " hasta " & dstElementos.Tables("TablaMotores").Rows(n - 1).Item("Fecha_Medida").ToString.Substring(0, 10)
            End If
        Else
            Return "desde 00/00/0000 hasta 00/00/0000"
        End If
        'Else

        'Return "desde " & Me.txtFiltroFechaIniV.Text & " hasta " & Me.txtFiltroFechaFinV.Text
        'End If
    End Function
    Protected Function obtenerIntervaloFechasH() As String
        Dim n As Integer
        n = dstElementos.Tables("TablaHorometros").Rows.Count
        If n > 0 Then
            If utiles.nullABlanco(dstElementos.Tables("TablaHorometros").Rows(0).ToString) <> "" And utiles.nullABlanco(dstElementos.Tables("TablaHorometros").Rows(n - 1).ToString) <> "" Then
                Return "desde " & dstElementos.Tables("TablaHorometros").Rows(0).Item("Fecha_Medida").ToString.Substring(0, 10) & " hasta " & dstElementos.Tables("TablaHorometros").Rows(n - 1).Item("Fecha_medida").ToString.Substring(0, 10)
            End If
        Else
            Return "desde 00/00/0000 hasta 00/00/0000"
        End If
    End Function
    Protected Function obtenerIntervaloFechasd() As String
        Dim n As Integer
        n = dstElementos.Tables("TablaDiferencial").Rows.Count
        If n > 0 Then
            If utiles.nullABlanco(dstElementos.Tables("TablaDiferencial").Rows(0).ToString) <> "" And utiles.nullABlanco(dstElementos.Tables("TablaDiferencial").Rows(n - 1).ToString) <> "" Then
                Return "desde " & dstElementos.Tables("TablaDiferencial").Rows(0).Item("Fecha_Medida").ToString.Substring(0, 10) & " hasta " & dstElementos.Tables("TablaDiferencial").Rows(n - 1).Item("Fecha_medida").ToString.Substring(0, 10)
            End If
        Else
            Return "desde 00/00/0000 hasta 00/00/0000"
        End If
    End Function
    Protected Function obtenerTotNumLecturas(ByVal tipo As String, ByVal codigoPVYCR As String, ByVal idelemento As String) As String
        Dim sentenciaSel, v_idelemento As String
        Dim TotNumlecturasQ, TotNumlecturasE, TotNumlecturasH, TotNumlecturasV, TotNumlecturasD As Double
        'EGB 04072008 
        'v_idelemento = idelemento.Substring(idelemento.LastIndexOf("-") - 4, 3)
        v_idelemento = idelemento
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
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
            Return TotNumlecturasQ
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
            Return TotNumlecturasE
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
            Return TotNumlecturasV
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
            Return TotNumlecturasH
        ElseIf tipo = "D" Then
            'If dstElementos.Tables("tablaAlimentacion").Rows.Count > 0 Then
            sentenciaSel = "SELECT Count(*) TotNumLecturasD " & _
                    "FROM PVYCR_DatosSuministros where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                    "idElementoMedida =  '" & v_idelemento & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            TotNumlecturasD = String.Format("{0:#,##0.##}", daElementos.SelectCommand.ExecuteScalar())
            Return TotNumlecturasD
        Else
            Return 0
        End If

    End Function
    'Public Sub crearDataSets_Elementos(ByVal tipoElemen As String, ByVal codigoPVYCR As String, ByVal idelemento As String)
    '    'Criterios de filtrado
    '    Dim sFiltro As String = ""
    '    Dim sFiltroInci As String = ""
    '    Dim fechainicio, fechaFin As DateTime
    '    Dim Nreg As Integer = 0
    '    Dim i As Integer = 0
    '    Dim sentenciaInci As String = ""
    '    Dim mensaje_final As String = ""

    '    'RDF
    '    'Fecha: 26/02/2009
    '    'Se comprueba si el punto tiene telemedida
    '    Dim comando As SqlCommand = New SqlCommand("", conexion)
    '    utiles.Comprobar_Conexion_BD(Page, conexion)
    '    'Se inserta el registro correspondiente
    '    comando.CommandText = "SELECT isnull(TipoSensor,'') FROM PVYCR_Puntos WHERE codigoPVYCR='" & codigoPVYCR & "'"
    '    Dim Telemedida As String

    '    Telemedida = comando.ExecuteScalar
    '    'Si es sin telemedida se amplia el año hidrológico
    '    If Telemedida = "" Then
    '        If DateTime.Now.Month < 10 Then
    '            fechainicio = "01/10/" & DateTime.Now.Year - 2
    '            fechaFin = DateTime.Today
    '        Else
    '            fechainicio = "01/10/" & DateTime.Now.Year - 1
    '            fechaFin = DateTime.Today
    '        End If

    '    Else
    '        'dependiendo del tipo seleccionaremos los datos de una tabla u otra
    '        'calculamos el año hidrológico que va desde el 01/10/añoactual - 1 hasta la fecha actual
    '        If DateTime.Now.Month < 10 Then
    '            fechainicio = "01/10/" & DateTime.Now.Year - 1
    '            fechaFin = DateTime.Today
    '        Else
    '            fechainicio = "01/10/" & DateTime.Now.Year
    '            fechaFin = DateTime.Today
    '        End If
    '    End If

    '    dstElementos.Clear()

    '    If tipoElemen = "Q" Then
    '        'scripts de cliente para el calendario

    '        imgCalFechaIniQ.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtfiltroFechaIni.ClientID & "'),'dd/mm/yyyy');")
    '        imgCalFechaFinQ.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFin.ClientID & "'),'dd/mm/yyyy');")
    '        imgFfechamedida.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedida.ClientID & "'),'dd/mm/yyyy');")

    '        If txtFiltroNregQ.Text <> "" Then
    '            sentenciaSel = "SELECT top " & txtFiltroNregQ.Text & " d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " & _
    '                               ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD, F.FUENTE_DATOS DescFuenteDato " & _
    '                               ", D.modificado FROM PVYCR_DatosAcequias D " & _
    '                               "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                               "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
    '                              "idElementoMedida =  '" & idelemento & "' "
    '        Else
    '            sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " & _
    '               ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD, F.FUENTE_DATOS DescFuenteDato " & _
    '               ",d.modificado FROM PVYCR_DatosAcequias D " & _
    '               "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
    '                "idElementoMedida =  '" & idelemento & "' "
    '        End If
    '        'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
    '        ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
    '        If chkFiltroNulasQ.Checked = False Then
    '            sentenciaSel = sentenciaSel & " and d.caudal_M3S is not null "
    '        End If

    '        'sentenciaSel = "SELECT CodigoPVYCR,idElementoMedida, Cod_Fuente_Dato,Fecha_Medida,Escala_M,Calado_M,Observaciones " & _
    '        '        ",TipoObtencionCaudal,RegimenCurva,NumeroParada,Caudal_M3S,DUDA_CALIDAD " & _
    '        '        "FROM PVYCR_DatosAcequias where codigoPVYCR = '" & codigoPVYCR & "' and " & _
    '        '        "idElementoMedida =  '" & idelemento & "' "

    '        If (txtFiltroFechaFin.Text <> "" And txtfiltroFechaIni.Text <> "") Or txtFiltrarCodFuentedato.Text <> "" Then
    '            'sFiltro = " and cast(datepart(dd,Fecha_Medida) as varchar(2))+'/'+ cast(datepart(mm,Fecha_Medida)as varchar(2))+'/' + cast(datepart(yy,Fecha_Medida)as varchar(4)) = " & _
    '            '"cast(datepart(dd,'" & txtfiltroFechaIni.Text & "') as varchar(2))+'/'+ cast(datepart(mm,'" & txtfiltroFechaIni.Text & "')as varchar(2))+'/' + cast(datepart(yy,'" & txtfiltroFechaIni.Text & "')as varchar(4))"
    '            If txtFiltrarCodFuentedato.Text <> "" Then
    '                sFiltro = " and d.Cod_Fuente_Dato = '" & txtFiltrarCodFuentedato.Text & "'"
    '            End If
    '            If txtFiltroFechaFin.Text <> "" And txtfiltroFechaIni.Text <> "" Then
    '                sFiltro = sFiltro & " and Fecha_medida between '" & txtfiltroFechaIni.Text & "' and '" & txtFiltroFechaFin.Text & "'"
    '            ElseIf txtFiltroFechaFin.Text = "" And txtfiltroFechaIni.Text <> "" Then
    '                Alert(Page, "La Fecha Hasta no puede ser nula")
    '                sFiltro = ""
    '            ElseIf txtFiltroFechaFin.Text <> "" And txtfiltroFechaIni.Text = "" Then
    '                Alert(Page, "La Fecha Desde no puede ser nula")
    '                sFiltro = ""
    '            End If
    '        ElseIf txtFiltroFechaFin.Text = "" And txtfiltroFechaIni.Text <> "" Then
    '            Alert(Page, "La Fecha Hasta no puede ser nula")
    '            sFiltro = ""
    '        ElseIf txtFiltroFechaFin.Text <> "" And txtfiltroFechaIni.Text = "" Then
    '            Alert(Page, "La Fecha Desde no puede ser nula")
    '            sFiltro = ""
    '        ElseIf txtFiltroFechaFin.Text = "" And txtfiltroFechaIni.Text = "" Then
    '            'ncm cometado el 29/10/2009 por petición usuario incidencia 159
    '            'If Telemedida = "" Then
    '            '    'RDF. Fecha: 03/03/2009
    '            '    txtfiltroFechaIni.Text = fechainicio
    '            '    txtFiltroFechaFin.Text = fechaFin
    '            'End If
    '            sFiltro = sFiltro & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
    '        Else
    '            sFiltro = ""
    '        End If


    '            'RDF 20080714
    '            If Session("Filtro") <> "" Then
    '                sentenciaSel = sentenciaSel & Session("Filtro")
    '            End If


    '            'sentenciaOrder = " order by codigoPVYCR, Fecha_Medida desc "    IPIM 18/08/2008: Lo ordeno por Cod_Fuente_dato, d.TipoObtencionCaudal al igual q en los informes, para que todos salgan igual.
    '            sentenciaOrder = " order by codigoPVYCR, Fecha_Medida  desc, d.Cod_Fuente_dato, d.TipoObtencionCaudal "

    '            If sFiltro <> "" Then
    '                sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
    '                sentenciaSelCount = sentenciaSelCount & sFiltro
    '            Else
    '                sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
    '            End If




    '            daElementos.SelectCommand.CommandText = sentenciaSel
    '            'datos acequias


    '            'IPIM 26/11/2008: Descomentamos para paginar
    '            'daElementos.Fill(dstElementos, (CInt(ucPaginacionA.lblPaginatext) - 1) * ucPaginacionA.pageSize, ucPaginacionA.pageSize, "TablaAcequias")
    '            daElementos.Fill(dstElementos, "TablaAcequias")
    '            'Cálculo del número de páginas
    '            'ordenamos las lecturas en un dataview por fecha

    '            Dim txtComando As String = ""
    '            txtComando = daElementos.SelectCommand.CommandText
    '            txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
    '            'IPIM 26/11/2008: Descomentamos para paginar
    '            ucPaginacionA.calcularPags(txtComando)
    '            '        ElseIf Request.QueryString("tipo").ToString = "E" Then
    '            'dstElementos.Tables("TablaAcequias").DefaultView.Sort = "CodigoPVYCR, Fecha_medida"
    '            Dim Tabla As DataTable
    '            Tabla = dstElementos.Tables("TablaAcequias").Clone()
    '            For i = dstElementos.Tables("TablaAcequias").Rows.Count - 1 To 0 Step -1
    '                Tabla.Rows.Add(dstElementos.Tables("TablaAcequias").Rows(i).ItemArray)
    '            Next
    '            dstElementos.Tables.Remove(dstElementos.Tables("TablaAcequias"))
    '            dstElementos.Tables.Add(Tabla)

    '        obtenerVolumenDiferencial("Q", dstElementos.Tables("TablaAcequias"), Page, mensaje_final)
    '        'obtenerVolumenDiferencial("Q")
    '            obtenerCaudalAcumulado()
    '            If chkReducirLecQ.Checked Then
    '                'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
    '                Dim dstNuevo As DataSet
    '                dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaAcequias", "Caudal_M3s", 5)
    '                dstElementos = dstNuevo.Copy
    '            End If

    '            CrearTablaParaRepeater("TablaAcequias", "Q")

    '    ElseIf tipoElemen = "E" Then

    '        'scripts de cliente para el calendario
    '        imgCalFechaIniE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniE.ClientID & "'),'dd/mm/yyyy');")
    '        imgCalFechaFinE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinE.ClientID & "'),'dd/mm/yyyy');")
    '        imgFfechamedidaE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedidaE.ClientID & "'),'dd/mm/yyyy');")
    '        '------------------------------------------------------------------------------------------------------------------------------------
    '        'NCM: 1º comprobaremos si el filtro de nº de regitrsos tiene valor, ya que si tiene valor le tendremos que
    '        'poner el top a la select. Éste filtro junto con el de mostrar 1 registro por día y el de mostrar 1 de cada X son excluyentes.
    '        '1) si quiieren N registros haremos el top de la select
    '        '2) si quieren un registro por día, haremos un bucle para obtener un registro cada día, teniendo en cuenta los demás filtros
    '        '3) si kieren uno de cada X
    '        '------------------------------------------------------------------------------------------------------------------------------------
    '        'CASO1
    '        'If txtFiltroNRegE.Text <> "" And chkFiltroPorDiaE.Checked = False And txtFiltroUnoCadaXE.Text = "" Then
    '        If txtFiltroNRegE.Text <> "" Then

    '            'EGB. SELECT ORIGINAL -> Se cambia por duplicidades
    '            'sentenciaSel = "SELECT top " & txtFiltroNRegE.Text & " D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
    '            '                "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
    '            '                "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
    '            '                "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato,ISNULL(C.E_FCorrectorContActiva * C.E_RelacionM3_KWH,0) relacionM3_Kwh " & _
    '            '                "FROM PVYCR_DatosAlimentacion D " & _
    '            '                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
    '            '                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '            '                "INNER JOIN  PVYCR_Contadores_ElementosMedida E ON E.codigoPVYCR = d.codigoPVYCR and " & _
    '            '                "E.idelementoMedida = D.idelementomedida " & _
    '            '                "LEFT OUTER JOIN  PVYCR_Contadores C ON C.codigoPVYCR = E.codigoPVYCR and " & _
    '            '                "C.FechaRevision = E.fechaRevision " & _
    '            '                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' " & _
    '            '                "and C.tipoContador = 'E' "
    '            sentenciaSel = "SELECT top " & txtFiltroNRegE.Text & " D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
    '                           "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
    '                           "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
    '                           "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato,isnull(C.E_FCorrectorContActiva,0) * isnull(C.E_RelacionM3_KWH,0) relacionM3_Kwh, C.IdContador" & _
    '                           ", D.modificado FROM PVYCR_DatosAlimentacion D " & _
    '                           "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
    '                           "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                           "LEFT OUTER JOIN  PVYCR_Contadores_ElementosMedida E ON E.codigoPVYCR = d.codigoPVYCR and " & _
    '                           "E.idelementoMedida = D.idelementomedida and D.Fecha_Medida BETWEEN E.FechaInicio AND ISNULL(E.FECHAFINAL, GETDATE()) " & _
    '                           "LEFT OUTER JOIN  PVYCR_Contadores C ON C.idContador = E.idContador and " & _
    '                           "C.FechaRevision = E.fechaRevision and c.tipocontador = 'E' " & _
    '                           "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "
    '            '"AND (EXISTS(SELECT IdContador FROM Contadores_ElementosMedida E1 WHERE E.codigoPVYCR=E1.codigoPVYCR AND d.idelementomedida=e1.idelementomedida and D.Fecha_Medida BETWEEN C1.FechaInicial AND ISNULL(C1.FECHAFIN, GETDATE()) and C.tipoContador = 'E' ) OR " & _
    '            '"NOT EXISTS(SELECT IDCONTADOR FROM PVYCR_CONTADORES C1 WHERE C.IDCONTADOR = C1.IDCONTADOR))"

    '            '            ElseIf txtFiltroNRegE.Text = "" And chkFiltroPorDiaE.Checked = False Then
    '        ElseIf txtFiltroNRegE.Text = "" Then
    '            'EGB. SELECT ORIGINAL -> Se cambia por duplicidades
    '            'sentenciaSel = "SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
    '            '                "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
    '            '                "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
    '            '                "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato,ISNULL(C.E_FCorrectorContActiva * C.E_RelacionM3_KWH,0) relacionM3_Kwh " & _
    '            '                "FROM PVYCR_DatosAlimentacion D " & _
    '            '                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
    '            '                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '            '                "INNER JOIN  PVYCR_Contadores_ElementosMedida E ON E.codigoPVYCR = d.codigoPVYCR and " & _
    '            '                "E.idelementoMedida = D.idelementomedida " & _
    '            '                "LEFT OUTER JOIN  PVYCR_Contadores C ON C.codigoPVYCR = E.codigoPVYCR and " & _
    '            '                "C.FechaRevision = E.fechaRevision " & _
    '            '                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' " & _
    '            '                "and C.tipoContador = 'E' "
    '            sentenciaSel = "SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
    '                            "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
    '                            "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
    '                            "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato,isnull(C.E_FCorrectorContActiva,0) * isnull(C.E_RelacionM3_KWH,0) relacionM3_Kwh, C.IdContador " & _
    '                            ",D.modificado FROM PVYCR_DatosAlimentacion D " & _
    '                            "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
    '                            "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                                    "LEFT OUTER JOIN  PVYCR_Contadores_ElementosMedida E ON E.codigoPVYCR = d.codigoPVYCR and " & _
    '                           "E.idelementoMedida = D.idelementomedida and D.Fecha_Medida BETWEEN E.FechaInicio AND ISNULL(E.FECHAFINAL, GETDATE()) " & _
    '                           "LEFT OUTER JOIN  PVYCR_Contadores C ON C.idContador = E.idContador and " & _
    '                           "C.FechaRevision = E.fechaRevision and c.tipocontador = 'E' " & _
    '                           "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "
    '            '"AND (EXISTS(SELECT IdContador FROM PVYCR_Contadores C1 WHERE C.IdContador=C1.IdContador AND D.Fecha_Medida BETWEEN C1.FechaInicial AND ISNULL(C1.FECHAFIN, GETDATE()) and C.tipoContador = 'E' ) OR " & _
    '            '"NOT EXISTS(SELECT IDCONTADOR FROM PVYCR_CONTADORES C1 WHERE C.IDCONTADOR = C1.IDCONTADOR))"



    '        End If
    '        If sentenciaSel <> "" Then
    '            'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
    '            ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
    '            If chkFiltroNulasE.Checked = False Then

    '                sentenciaSel = sentenciaSel & " and d.Total_kwh is not null "

    '            End If
    '            'sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
    '            '                "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
    '            '                "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
    '            '                "IV.descripcion descIncElec " & _
    '            '                "FROM PVYCR_DatosAlimentacion D " & _
    '            '                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
    '            '                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "



    '            If (txtFiltroFechaFinE.Text <> "" And txtFiltroFechaIniE.Text <> "") Or txtFiltrarCodFuenteDatoE.Text <> "" Then
    '                If txtFiltrarCodFuenteDatoE.Text <> "" Then
    '                    sFiltro = " and d.Cod_Fuente_Dato = '" & txtFiltrarCodFuenteDatoE.Text & "'"
    '                    'If txtFiltrarCodFuenteDatoE.Text <> "05" Then
    '                    '    sentenciaInci = 
    '                    'End If
    '                End If
    '                If txtFiltroFechaFinE.Text <> "" And txtFiltroFechaIniE.Text <> "" Then
    '                    sFiltro = sFiltro & " and d.Fecha_medida between '" & txtFiltroFechaIniE.Text & "' and '" & txtFiltroFechaFinE.Text & "'"
    '                ElseIf txtFiltroFechaFinE.Text = "" And txtFiltroFechaIniE.Text <> "" Then
    '                    Alert(Page, "La Fecha Hasta no puede ser nula")
    '                    sFiltro = ""
    '                ElseIf txtFiltroFechaFinE.Text <> "" And txtFiltroFechaIniE.Text = "" Then
    '                    Alert(Page, "La Fecha Desde no puede ser nula")
    '                    sFiltro = ""
    '                End If
    '            ElseIf txtFiltroFechaFinE.Text = "" And txtFiltroFechaIniE.Text <> "" Then
    '                Alert(Page, "La Fecha Hasta no puede ser nula")
    '                sFiltro = ""
    '            ElseIf txtFiltroFechaFinE.Text <> "" And txtFiltroFechaIniE.Text = "" Then
    '                Alert(Page, "La Fecha Desde no puede ser nula")
    '                sFiltro = ""
    '            ElseIf txtFiltroFechaFinE.Text = "" And txtFiltroFechaIniE.Text = "" Then
    '                'ncm cometado el 29/10/2009 por petición usuario incidencia 159
    '                'If Telemedida = "" Then
    '                '    'RDF. Fecha: 03/03/2009
    '                '    txtFiltroFechaIniE.Text = fechainicio
    '                '    txtFiltroFechaFinE.Text = fechaFin
    '                'End If
    '                sFiltro = sFiltro & " and d.Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
    '            Else
    '                sFiltro = ""
    '            End If

    '            'sFiltroInci = sFiltro.Replace("d.", "d1.")
    '            'RDF 20080714
    '            If Session("FiltroE") <> "" Then
    '                '   sFiltroInci = sFiltroInci & Session("FiltroE").tolower.Replace("d.", "d1.")
    '                'ncm 22/06/2009
    '                'sentenciaInci = " AND NOT EXISTS (SELECT D1.CODIGOPVYCR FROM PVYCR_DATOSALIMENTACION D1 where d.codigopvycr=d1.codigopvycr and " & _
    '                '    "d.idelementomedida = d1.idelementomedida and d.cod_fuente_dato = d1.cod_fuente_dato and d.fecha_medida = d1.fecha_medida and " & _
    '                '    "d1.cod_fuente_dato <> '05' and (d1.idincidenciaelectrica = 2 or d1.idincidenciaelectrica=3) " & sFiltroInci & ") "
    '                sentenciaSel = sentenciaSel & Session("FiltroE") '& sentenciaInci
    '            Else

    '                'ncm 22/06/2009
    '                'sentenciaInci = " AND NOT EXISTS (SELECT D1.CODIGOPVYCR FROM PVYCR_DATOSALIMENTACION D1 where d.codigopvycr=d1.codigopvycr and " & _
    '                '    "d.idelementomedida = d1.idelementomedida and d.cod_fuente_dato = d1.cod_fuente_dato and d.fecha_medida = d1.fecha_medida and " & _
    '                '    "d1.cod_fuente_dato <> '05' and (d1.idincidenciaelectrica = 2 or d1.idincidenciaelectrica=3) " & sFiltroInci & ") "
    '                sentenciaSel = sentenciaSel '& sentenciaInci
    '            End If

    '                sentenciaOrder = " order by D.codigoPVYCR, D.Fecha_Medida desc"


    '            If sFiltro <> "" Then
    '                sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
    '            Else
    '                sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
    '            End If


    '                daElementos.SelectCommand.CommandText = sentenciaSel
    '                'CASO3
    '                'si ha marcado el filtro de uno de cada x 
    '                'If txtFiltroUnoCadaXE.Text <> "" Then
    '                '    'si existendatos en la tabla alimentacion los limpiamos
    '                '    'si no existe el datatble lo creamos
    '                '    Dim dtaux As DataTable
    '                '    daElementos.Fill(dstElementos, "TablaAlimentacionAux")
    '                '    If dstElementos.Tables.Contains("TablaAlimentacion") Then
    '                '        dstElementos.Tables("TablaAlimentacion").Clear()
    '                '    Else
    '                '        dtaux = dstElementos.Tables("TablaAlimentacionAux").Clone()
    '                '        dtaux.TableName = "TablaAlimentacion"
    '                '        dstElementos.Tables.Add(dtaux)
    '                '    End If



    '                '    For i = 0 To dstElementos.Tables("TablaAlimentacionAux").Rows.Count - 1
    '                '        If i Mod txtFiltroUnoCadaXE.Text = 0 Then
    '                '            'añadimos una nueva fila a la tabla allimentacion para poder copiar los datos de la auxiliar
    '                '            'Dim row As DataRow = dstAux.Tables("TablaAlimentacionAux").NewRow

    '                '            'row.ItemArray = dstAux.Tables("TablaAlimentacionAux").Rows(i).ItemArray
    '                '            'dstElementos.Tables("TablaAlimentacion").Rows.Add(row)
    '                '            dstElementos.Tables("TablaAlimentacion").Rows.Add(dstElementos.Tables("TablaAlimentacionAux").Rows(i).ItemArray)
    '                '        End If
    '                '    Next
    '                'End If
    '                ''datos energía
    '                'IPIM 26/11/2008
    '                'daElementos.Fill(dstElementos, (CInt(ucPaginacionE.lblPaginatext) - 1) * ucPaginacionE.pageSize, ucPaginacionE.pageSize, "TablaAlimentacion")
    '                daElementos.Fill(dstElementos, "TablaAlimentacion")
    '                'dstElementos.Tables("TablaAlimentacion").DefaultView.Sort = "codigoPVYCR, Fecha_medida"

    '        End If
    '        'CASO2
    '        'If chkFiltroPorDiaE.Checked = True Then
    '        '    Dim FiltroNulas, FiltroCodFuenteDato As String
    '        '    'si el usuario ha puesto fechas en el filtro esas serán las que tenemos que tener en cuenta, sino
    '        '    'será el año hisdrológico
    '        '    If txtFiltroFechaFinE.Text <> "" And txtFiltroFechaIniE.Text <> "" Then
    '        '        fechainicio = txtFiltroFechaIniE.Text
    '        '        fechaFin = txtFiltroFechaFinE.Text

    '        '    Else
    '        '        fechainicio = "01/10/" & DateTime.Now.Year - 1
    '        '        fechaFin = DateTime.Today
    '        '    End If
    '        '    'copiamos las fecha en unas varianles para poder utilizarlas 
    '        '    'fechainiciopaginacion = fechainicio
    '        '    'fechafinpaginacion = fechaFin
    '        '    'Filtro Nulas
    '        '    If chkFiltroNulasE.Checked = False Then

    '        '        FiltroNulas = " and d.Total_kwh is not null "

    '        '    End If
    '        '    'Filtro código fuente dato
    '        '    If txtFiltrarCodFuenteDatoE.Text <> "" Then
    '        '        FiltroCodFuenteDato = " and d.Cod_Fuente_Dato = '" & txtFiltrarCodFuenteDatoE.Text & "'"
    '        '    End If
    '        '    If txtFiltroNRegE.Text <> "" Then

    '        '        'si existendatos en la tabla alimentacion los limpiamos
    '        '        If dstElementos.Tables.Contains("TablaAlimentacion") Then
    '        '            dstElementos.Tables("TablaAlimentacion").Clear()
    '        '        End If

    '        '        Do While (fechainicio < fechaFin And Nreg < txtFiltroNRegE.Text + 1)
    '        '            sentenciaSel = "Select top 1 D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
    '        '                            "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
    '        '                            "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
    '        '                            "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato " & _
    '        '                            "FROM PVYCR_DatosAlimentacion D " & _
    '        '                            "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
    '        '                            "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '        '                            "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
    '        '                            "convert(nvarchar(15),Fecha_Medida,103) = '" & fechainicio & "' "
    '        '            'le añadimos los filtros
    '        '            If FiltroNulas <> "" Then
    '        '                sentenciaSel = sentenciaSel & FiltroNulas
    '        '            End If
    '        '            If FiltroCodFuenteDato <> "" Then
    '        '                sentenciaSel = sentenciaSel & FiltroCodFuenteDato
    '        '            End If
    '        '            sentenciaSel = sentenciaSel & FiltroNulas & FiltroCodFuenteDato
    '        '            'meteremos la información en un dataset.
    '        '            daElementos.SelectCommand.CommandText = sentenciaSel
    '        '            'datos energía
    '        '            'daElementos.Fill(dstElementos, (CInt(ucPaginacionE.lblPaginatext) - 1) * ucPaginacionE.pageSize, ucPaginacionE.pageSize, "TablaAlimentacion")
    '        '            daElementos.Fill(dstElementos, "TablaAlimentacion")
    '        '            'avanzamos al siguiente día
    '        '            fechainicio = DateAdd(DateInterval.Day, 1, fechainicio)
    '        '            'comprobamos si se ha cargado registro en el dataset, si es así avanzamos en el nreg
    '        '            'esto lo hacemos así para que realmente muestre el nº de regitros k ha pedido el usuario
    '        '            'y no haga X itraciones sin encontrar datos
    '        '            Nreg = dstElementos.Tables("TablaAlimentacion").Rows.Count + 1
    '        '            'Nreg = Nreg + 1
    '        '        Loop
    '        '        'sentenciaSel = Replace(Replace(sentenciaSel, "top 1", ""), "Fecha_Medida = '" & fechainicio & "' ", )
    '        '        'daElementos.SelectCommand.CommandText = sentenciaSel
    '        '    Else
    '        '        'si existendatos en la tabla alimentacion los limpiamos
    '        '        If dstElementos.Tables.Contains("TablaAlimentacion") Then
    '        '            dstElementos.Tables("TablaAlimentacion").Clear()
    '        '        End If
    '        '        Do While fechainicio < fechaFin
    '        '            sentenciaSel = "Select top 1 D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
    '        '                            "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
    '        '                            "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
    '        '                            "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato " & _
    '        '                            "FROM PVYCR_DatosAlimentacion D " & _
    '        '                            "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
    '        '                            "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '        '                            "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
    '        '                            "convert(nvarchar(15),Fecha_Medida,103) = '" & fechainicio & "' "
    '        '            'le añadimos los filtros
    '        '            If FiltroNulas <> "" Then
    '        '                sentenciaSel = sentenciaSel & FiltroNulas
    '        '            End If
    '        '            If FiltroCodFuenteDato <> "" Then
    '        '                sentenciaSel = sentenciaSel & FiltroCodFuenteDato
    '        '            End If
    '        '            sentenciaSel = sentenciaSel & FiltroNulas & FiltroCodFuenteDato
    '        '            'meteremos la información en un dataset.
    '        '            daElementos.SelectCommand.CommandText = sentenciaSel
    '        '            'datos energía
    '        '            'daElementos.Fill(dstElementos, (CInt(ucPaginacionE.lblPaginatext) - 1) * ucPaginacionE.pageSize, ucPaginacionE.pageSize, "TablaAlimentacion")
    '        '            daElementos.Fill(dstElementos, "TablaAlimentacion")
    '        '            'avanzamos al siguiente día
    '        '            fechainicio = DateAdd(DateInterval.Day, 1, fechainicio)
    '        '        Loop
    '        '        'sentenciaSel = Replace(Replace(sentenciaSel, "top 1", ""), "Fecha_Medida = '" & fechainicio & "' ", )
    '        '        'daElementos.SelectCommand.CommandText = sentenciaSel

    '        '    End If
    '        'End If

    '        'Cálculo del número de páginas
    '        Dim txtComando As String = ""
    '        txtComando = daElementos.SelectCommand.CommandText
    '        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
    '        ucPaginacionE.calcularPags(txtComando)

    '        '        ElseIf Request.QueryString("tipo").ToString = "V" Then

    '        'ncm 14/07/2008
    '        Dim Tabla As DataTable
    '        Tabla = dstElementos.Tables("TablaAlimentacion").Clone()
    '        For i = dstElementos.Tables("TablaAlimentacion").Rows.Count - 1 To 0 Step -1
    '            Tabla.Rows.Add(dstElementos.Tables("TablaAlimentacion").Rows(i).ItemArray)
    '        Next
    '        dstElementos.Tables.Remove(dstElementos.Tables("TablaAlimentacion"))
    '        dstElementos.Tables.Add(Tabla)
    '        'ncm 13/09/2009
    '        QuitarRegistrosSegunFuenteDato("E")
    '        obtenerVolumenDiferencial("E", dstElementos.Tables("TablaAlimentacion"), Page, mensaje_final)
    '        'obtenerVolumenDiferencial("E")
    '        obtenerVolumenElectricoAcumulado()
    '        If chkReducirLecE.Checked Then
    '            'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
    '            Dim dstNuevo As DataSet
    '            dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaAlimentacion", "Total_Kwh", 5)
    '            dstElementos = dstNuevo.Copy
    '        End If

    '        CrearTablaParaRepeater("TablaAlimentacion", "E")

    '    ElseIf tipoElemen = "V" Then

    '        'scripts de cliente para el calendario
    '        imgCalFechaIniV.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniV.ClientID & "'),'dd/mm/yyyy');")
    '        imgCalFechaFinV.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinV.ClientID & "'),'dd/mm/yyyy');")
    '        imgFfechamedidaM.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedidaM.ClientID & "'),'dd/mm/yyyy');")

    '        If txtFiltroNRegV.Text <> "" Then
    '            sentenciaSel = "SELECT top " & txtFiltroNRegV.Text & " D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
    '                           "D.LecturaContador_M3, D.Funciona, D.Justificacion, D.Observaciones, D.idIncidenciaVolumetrica, " & _
    '                           "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, D.CaudalMedido, " & _
    '                           "IV.descripcion DescIncVol, F.FUENTE_DATOS DescFuenteDato " & _
    '                           ",d.modificado FROM PVYCR_DatosMotores D " & _
    '                           "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
    '                           "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                           "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "


    '        Else
    '            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
    '                           "D.LecturaContador_M3, D.Funciona, D.Justificacion, D.Observaciones, D.idIncidenciaVolumetrica, " & _
    '                           "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica,D.CaudalMedido, " & _
    '                           "IV.descripcion DescIncVol, F.FUENTE_DATOS DescFuenteDato " & _
    '                           ",d.modificado FROM PVYCR_DatosMotores D " & _
    '                           "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
    '                            "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                           "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "

    '        End If
    '        'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
    '        ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
    '        If chkFiltroNulasV.Checked = False Then
    '            sentenciaSel = sentenciaSel & " and d.lecturacontador_m3 is not null "
    '        End If

    '        If (txtFiltroFechaFinV.Text <> "" And txtFiltroFechaIniV.Text <> "") Or txtFiltrarCodFuenteDatoV.Text <> "" Then
    '            If txtFiltrarCodFuenteDatoV.Text <> "" Then
    '                sFiltro = " and d.Cod_Fuente_Dato = '" & txtFiltrarCodFuenteDatoV.Text & "'"
    '            End If
    '            If txtFiltroFechaFinV.Text <> "" And txtFiltroFechaIniV.Text <> "" Then
    '                sFiltro = sFiltro & " and d.Fecha_medida between '" & txtFiltroFechaIniV.Text & "' and '" & txtFiltroFechaFinV.Text & "'"
    '            ElseIf txtFiltroFechaFinV.Text = "" And txtFiltroFechaIniV.Text <> "" Then
    '                Alert(Page, "La Fecha Hasta no puede ser nula")
    '                sFiltro = ""
    '            ElseIf txtFiltroFechaFinV.Text <> "" And txtFiltroFechaIniV.Text = "" Then
    '                Alert(Page, "La Fecha Desde no puede ser nula")
    '                sFiltro = ""
    '            End If
    '        ElseIf txtFiltroFechaFinV.Text = "" And txtFiltroFechaIniV.Text <> "" Then
    '            Alert(Page, "La Fecha Hasta no puede ser nula")
    '            sFiltro = ""
    '        ElseIf txtFiltroFechaFinV.Text <> "" And txtFiltroFechaIniV.Text = "" Then
    '            Alert(Page, "La Fecha Desde no puede ser nula")
    '            sFiltro = ""
    '        ElseIf txtFiltroFechaFinV.Text = "" And txtFiltroFechaIniV.Text = "" Then
    '            'ncm cometado el 29/10/2009 por petición usuario incidencia 159
    '            'If Telemedida = "" Then
    '            '    'RDF. Fecha: 03/03/2009
    '            '    txtFiltroFechaIniV.Text = fechainicio
    '            '    txtFiltroFechaFinV.Text = fechaFin
    '            'End If
    '            sFiltro = sFiltro & " and d.Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
    '        Else
    '            sFiltro = ""
    '        End If
    '        'sFiltroInci = sFiltro.Replace("d.", "d1.")
    '            sentenciaOrder = " order by D.codigoPVYCR, D.Fecha_Medida desc"

    '            'RDF 20080716
    '        If Session("FiltroM") <> "" Then
    '            'sFiltroInci = sFiltroInci & Session("FiltroM").tolower.Replace("d.", "d1.")
    '            'sentenciaInci = " AND NOT EXISTS (SELECT D1.CODIGOPVYCR FROM PVYCR_DATOSMOTORES D1 where d.codigopvycr=d1.codigopvycr and " & _
    '            '                    "d.idelementomedida = d1.idelementomedida and d.cod_fuente_dato = d1.cod_fuente_dato and d.fecha_medida = d1.fecha_medida and " & _
    '            '                    "d1.cod_fuente_dato <> '05' and (d1.idincidenciavolumetrica = 6 or d1.idincidenciavolumetrica =7) " & sFiltroInci & ") "

    '            sentenciaSel = sentenciaSel & Session("FiltroM") '& sentenciaInci
    '        Else
    '            'sentenciaInci = " AND NOT EXISTS (SELECT D1.CODIGOPVYCR FROM PVYCR_DATOSMOTORES D1 where d.codigopvycr=d1.codigopvycr and " & _
    '            '    "d.idelementomedida = d1.idelementomedida and d.cod_fuente_dato = d1.cod_fuente_dato and d.fecha_medida = d1.fecha_medida and " & _
    '            '    "d1.cod_fuente_dato <> '05' and (d1.idincidenciavolumetrica = 6 or d1.idincidenciavolumetrica =7) " & sFiltroInci & ") "

    '            sentenciaSel = sentenciaSel '& sentenciaInci
    '        End If


    '            If sFiltro <> "" Then
    '                sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
    '            Else
    '                sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
    '            End If



    '            daElementos.SelectCommand.CommandText = sentenciaSel
    '            'datos volumétricos
    '            'IPIM 26/11/2008
    '            'daElementos.Fill(dstElementos, (CInt(ucPaginacionV.lblPaginatext) - 1) * ucPaginacionV.pageSize, ucPaginacionV.pageSize, "TablaMotores")
    '            daElementos.Fill(dstElementos, "TablaMotores")
    '            'dstElementos.Tables("TablaMotores").DefaultView.Sort = "codigoPVYCR, Fecha_Medida"
    '            'Cálculo del número de páginas
    '            Dim txtComando As String = ""
    '            txtComando = daElementos.SelectCommand.CommandText
    '            txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
    '            ucPaginacionV.calcularPags(txtComando)

    '            'ncm 14/07/2008
    '            Dim Tabla As DataTable
    '            Tabla = dstElementos.Tables("TablaMotores").Clone()
    '            For i = dstElementos.Tables("TablaMotores").Rows.Count - 1 To 0 Step -1
    '                Tabla.Rows.Add(dstElementos.Tables("TablaMotores").Rows(i).ItemArray)
    '            Next
    '            dstElementos.Tables.Remove(dstElementos.Tables("TablaMotores"))
    '            dstElementos.Tables.Add(Tabla)
    '        'ncm 13/09/2009
    '        QuitarRegistrosSegunFuenteDato("V")
    '        obtenerVolumenDiferencial("V", dstElementos.Tables("TablaMotores"), Page, mensaje_final)
    '        'obtenerVolumenDiferencial("V")
    '            obtenerVolumenAcumulado()
    '            If chkReducirlecV.Checked Then
    '                'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
    '                Dim dstNuevo As DataSet
    '                dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaMotores", "LecturaContador_M3", 5)
    '                dstElementos = dstNuevo.Copy
    '            End If

    '            CrearTablaParaRepeater("TablaMotores", "V")

    '    ElseIf tipoElemen = "H" Then

    '        'scripts de cliente para el calendario
    '        ImgCalFechaIniH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltrofechaIniH.ClientID & "'),'dd/mm/yyyy');")
    '        ImgCalFechaFinH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinH.ClientID & "'),'dd/mm/yyyy');")
    '        imgFFechaH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaMedidaH.ClientID & "'),'dd/mm/yyyy');")

    '        If txtFiltroNRegH.Text <> "" Then
    '            'NCM descomentar cuando quieran calcular con incidencias

    '            'sentenciaSel = "SELECT top " & txtFiltroNRegH.Text & " D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
    '            '                "D.HorasIntervalo, D.idElementoMedida, " & _
    '            '                "D.Funciona, D.Observaciones, D.idIncidenciaVolumetrica, " & _
    '            '                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
    '            '                "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato, M.Caudal_LSeg " & _
    '            '                "FROM PVYCR_DatosHorometros D " & _
    '            '                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
    '            '                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '            '                "INNER JOIN  PVYCR_ElementosMedida_MotoBombas E ON E.codigoPVYCR = d.codigoPVYCR and " & _
    '            '                "E.idelementoMedida = D.idelementomedida " & _
    '            '                "INNER JOIN  PVYCR_Motobombas M ON M.codigoMotobomba = E.codigoMotobomba and " & _
    '            '                "M.FechaRevision = E.fechaRevision " & _
    '            '                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "
    '            'RDF
    '            'Fecha: 01/04/2009
    '            'solución a la duplicidad de lecturas
    '            sentenciaSel = "SELECT TOP " & txtFiltroNRegH.Text & "  D2.CodigoPVYCR, D2.Cod_Fuente_Dato, D2.Fecha_Medida, D2.HorasIntervalo, D2.idElementoMedida, D2.Funciona, D2.Observaciones, " & _
    '                           "  D2.idIncidenciaVolumetrica, D2.ConsumoVolumetricoAdicional, D2.ReinicioLecturaVolumetrica, D2.descIncVol,  " & _
    '                           " D2.DescFuenteDato, D2.Caudal_LSeg, D2.codigoMotobomba " & _
    '                           "  FROM " & _
    '                            " (SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
    '                            "D.HorasIntervalo, D.idElementoMedida, " & _
    '                            "D.Funciona,substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " & _
    '                            "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
    '                            "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato,  C.Caudal_LSeg,C.codigoMotobomba " & _
    '                            ",d.modificado FROM PVYCR_DatosHorometros D " & _
    '                            "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
    '                            "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                            "INNER JOIN  PVYCR_ElementosMedida_MotoBombas E ON E.codigoPVYCR = d.codigoPVYCR and " & _
    '                           "E.idelementoMedida = D.idelementomedida " & _
    '                           "INNER JOIN  PVYCR_Motobombas C ON C.codigoMotobomba = E.codigoMotobomba and " & _
    '                           "C.FechaRevision = E.fechaRevision  " & _
    '                           "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' AND D.Fecha_Medida BETWEEN C.FechaInicial AND ISNULL(C.FECHAFIN, GETDATE()) " & _
    '                            " UNION " & _
    '                            "SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
    '                            "D.HorasIntervalo, D.idElementoMedida, " & _
    '                            "D.Funciona, substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " & _
    '                            "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
    '                            "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato, NULL AS Caudal_LSeg,NULL  AS codigoMotobomba " & _
    '                            ",d.modificado FROM PVYCR_DatosHorometros D " & _
    '                            "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
    '                            "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                            "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "'  " & _
    '                            " AND NOT EXISTS(SELECT * FROM dbo.PVYCR_ElementosMedida_MotoBombas AS E WHERE E.codigoPVYCR = D.CodigoPVYCR AND E.idElementoMedida = D.idElementoMedida )) as D2 " & _
    '                            " WHERE (1=1) "

    '            '"AND (EXISTS(SELECT CodigoMotobomba FROM PVYCR_Motobombas C1 WHERE C.codigoMotobomba=C1.codigoMotobomba AND D.Fecha_Medida BETWEEN C1.FechaInicial AND ISNULL(C1.FECHAFIN, GETDATE())  ) OR " & _
    '            '"NOT EXISTS(SELECT CodigoMotobomba FROM PVYCR_Motobombas C1 WHERE C.codigoMotobomba = C1.codigoMotobomba))"
    '            'NCM 03/03/2009 CAMBIADO POR LA RELACION CON CONTADORES
    '            '"where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "

    '        Else
    '            'RDF
    '            'Fecha: 01/04/2009
    '            'solución a la duplicidad de lecturas

    '            sentenciaSel = "SELECT  D2.CodigoPVYCR, D2.Cod_Fuente_Dato, D2.Fecha_Medida, D2.HorasIntervalo, D2.idElementoMedida, D2.Funciona, D2.Observaciones, " & _
    '                           "  D2.idIncidenciaVolumetrica, D2.ConsumoVolumetricoAdicional, D2.ReinicioLecturaVolumetrica, D2.descIncVol,  " & _
    '                           " D2.DescFuenteDato, D2.Caudal_LSeg, D2.codigoMotobomba, d2.modificado " & _
    '                           "  FROM " & _
    '                            " (SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
    '                            "D.HorasIntervalo, D.idElementoMedida, " & _
    '                            "D.Funciona, substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " & _
    '                            "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
    '                            "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato,  C.Caudal_LSeg,C.codigoMotobomba " & _
    '                            ",d.modificado FROM PVYCR_DatosHorometros D " & _
    '                            "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
    '                            "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                            "INNER JOIN  PVYCR_ElementosMedida_MotoBombas E ON E.codigoPVYCR = d.codigoPVYCR and " & _
    '                           "E.idelementoMedida = D.idelementomedida " & _
    '                           "INNER JOIN  PVYCR_Motobombas C ON C.codigoMotobomba = E.codigoMotobomba and " & _
    '                           "C.FechaRevision = E.fechaRevision  " & _
    '                           "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' AND D.Fecha_Medida BETWEEN C.FechaInicial AND ISNULL(C.FECHAFIN, GETDATE()) " & _
    '                            " UNION " & _
    '                            "SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
    '                            "D.HorasIntervalo, D.idElementoMedida, " & _
    '                            "D.Funciona,substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " & _
    '                            "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
    '                            "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato,  NULL AS Caudal_LSeg,NULL AS codigoMotobomba " & _
    '                            ",d.modificado FROM PVYCR_DatosHorometros D " & _
    '                            "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
    '                            "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                            "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "'  " & _
    '                            " AND NOT EXISTS(SELECT * FROM dbo.PVYCR_ElementosMedida_MotoBombas AS E WHERE E.codigoPVYCR = D.CodigoPVYCR AND E.idElementoMedida = D.idElementoMedida )) as D2 " & _
    '                            " WHERE (1=1) "
    '            '"AND (EXISTS(SELECT CodigoMotobomba FROM PVYCR_Motobombas C1 WHERE C.codigoMotobomba=C1.codigoMotobomba AND D.Fecha_Medida BETWEEN C1.FechaInicial AND ISNULL(C1.FECHAFIN, GETDATE())  ) OR " & _
    '            '"NOT EXISTS(SELECT CodigoMotobomba FROM PVYCR_Motobombas C1 WHERE C.codigoMotobomba = C1.codigoMotobomba))"

    '            '"where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "
    '        End If


    '        'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
    '        ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
    '        If chkFiltroNulasH.Checked = False Then
    '            sentenciaSel = sentenciaSel & " and d2.horasintervalo is not null "
    '        End If

    '        If (txtFiltroFechaFinH.Text <> "" And txtFiltrofechaIniH.Text <> "") Or txtFiltrarCodFuenteDatoH.Text <> "" Then
    '            If txtFiltrarCodFuenteDatoH.Text <> "" Then
    '                sFiltro = " and D2.Cod_Fuente_Dato = '" & txtFiltrarCodFuenteDatoH.Text & "'"
    '            End If
    '            If txtFiltroFechaFinH.Text <> "" And txtFiltrofechaIniH.Text <> "" Then
    '                sFiltro = sFiltro & " and D2.Fecha_Medida between '" & txtFiltrofechaIniH.Text & " 00:00:00' and '" & txtFiltroFechaFinH.Text & " 23:59:59' "
    '            ElseIf txtFiltroFechaFinH.Text = "" And txtFiltrofechaIniH.Text <> "" Then
    '                Alert(Page, "La Fecha Desde no puede ser nula")
    '                sFiltro = ""
    '            ElseIf txtFiltroFechaFinH.Text <> "" And txtFiltrofechaIniH.Text = "" Then
    '                Alert(Page, "La Fecha Hasta no puede ser nula")
    '                sFiltro = ""
    '            End If
    '        ElseIf txtFiltroFechaFinH.Text = "" And txtFiltrofechaIniH.Text <> "" Then
    '            Alert(Page, "La Fecha Hasta no puede ser nula")
    '            sFiltro = ""
    '        ElseIf txtFiltroFechaFinH.Text <> "" And txtFiltrofechaIniH.Text = "" Then
    '            Alert(Page, "La Fecha Desde no puede ser nula")
    '            sFiltro = ""
    '        ElseIf txtFiltroFechaFinH.Text = "" And txtFiltrofechaIniH.Text = "" Then
    '            'ncm cometado el 29/10/2009 por petición usuario incidencia 159
    '            'If Telemedida = "" Then
    '            '    'RDF. Fecha: 03/03/2009
    '            '    txtFiltrofechaIniH.Text = fechainicio
    '            '    txtFiltroFechaFinH.Text = fechaFin
    '            'End If
    '            sFiltro = sFiltro & " and D2.Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
    '        Else
    '            sFiltro = ""

    '        End If

    '        sentenciaOrder = " order by D2.codigoPVYCR, D2.Fecha_medida desc"
    '        sentenciaInci = " AND NOT EXISTS (SELECT D1.CODIGOPVYCR FROM PVYCR_DATOSHOROMETROS D1 where d2.codigopvycr=d1.codigopvycr and " & _
    '                      "d2.idelementomedida = d1.idelementomedida and d2.cod_fuente_dato = d1.cod_fuente_dato and d2.fecha_medida = d1.fecha_medida and (d1.cod_fuente_dato <> '05' and d1.idincidenciavolumetrica in (10,11)) ) "

    '        'RDF 20080921
    '        If Session("FiltroH") <> "" Then
    '            sentenciaSel = sentenciaSel & Session("FiltroH")
    '        Else
    '            sentenciaSel = sentenciaSel & sentenciaInci
    '        End If


    '        If sFiltro <> "" Then
    '            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
    '        Else
    '            sentenciaSel = sentenciaSel & " and D2.Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
    '        End If

    '        daElementos.SelectCommand.CommandText = sentenciaSel
    '        'datos horómetros
    '        'IPIM 26/11/2008
    '        'daElementos.Fill(dstElementos, (CInt(ucPaginacionH.lblPaginatext) - 1) * ucPaginacionH.pageSize, ucPaginacionH.pageSize, "TablaHorometros")
    '        daElementos.Fill(dstElementos, "TablaHorometros")
    '        'dstElementos.Tables("TablaHorometros").DefaultView.Sort = "codigoPVYCR, Fecha_medida"
    '        'Cálculo del número de páginas
    '        Dim txtComando As String = ""
    '        txtComando = daElementos.SelectCommand.CommandText
    '        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
    '        ucPaginacionH.calcularPags(txtComando)
    '        'ncm 14/07/2008
    '        Dim Tabla As DataTable
    '        Tabla = dstElementos.Tables("TablaHorometros").Clone()
    '        For i = dstElementos.Tables("TablaHorometros").Rows.Count - 1 To 0 Step -1
    '            Tabla.Rows.Add(dstElementos.Tables("TablaHorometros").Rows(i).ItemArray)
    '        Next
    '        dstElementos.Tables.Remove(dstElementos.Tables("TablaHorometros"))
    '        dstElementos.Tables.Add(Tabla)

    '        obtenerVolumenDiferencial("H", dstElementos.Tables("TablaHorometros"), Page, mensaje_final)
    '        'obtenerVolumenDiferencial("H")
    '        obtenerVolumenHorometroAcumulado()

    '        If chkReducirLecH.Checked Then
    '            'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
    '            Dim dstNuevo As DataSet
    '            dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaHorometros", "HorasIntervalo", 5)
    '            dstElementos = dstNuevo.Copy
    '        End If

    '        CrearTablaParaRepeater("TablaHorometros", "H")

    '    ElseIf tipoElemen = "D" Then
    '        'scripts de cliente para el calendario

    '        imgCalFechaIniD.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniD.ClientID & "'),'dd/mm/yyyy');")
    '        imgCalFechaFinD.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinD.ClientID & "'),'dd/mm/yyyy');")
    '        imgFfechamedidaD.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedidaD.ClientID & "'),'dd/mm/yyyy');")

    '        If txtFiltroNRegD.Text <> "" Then
    '            sentenciaSel = "SELECT top " & txtFiltroNRegD.Text & " d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,suministroMensualM3,d.Observaciones, " & _
    '                               " F.FUENTE_DATOS DescFuenteDato " & _
    '                               "FROM PVYCR_DatosSuministros D " & _
    '                               "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                               "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
    '                              "idElementoMedida =  '" & idelemento & "' "
    '        Else
    '            sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,suministroMensualM3,d.Observaciones, " & _
    '                               " F.FUENTE_DATOS DescFuenteDato " & _
    '                               "FROM PVYCR_DatosSuministros D " & _
    '                               "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
    '                               "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
    '                              "idElementoMedida =  '" & idelemento & "' "
    '        End If
    '        'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
    '        ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
    '        If chkFiltroNulasD.Checked = False Then
    '            sentenciaSel = sentenciaSel & " and d.suministromensualm3 is not null "
    '        End If


    '        If (txtFiltroFechaFinD.Text <> "" And txtFiltroFechaIniD.Text <> "") Or txtFiltrarCodFuenteDatoD.Text <> "" Then
    '            If txtFiltrarCodFuenteDatoD.Text <> "" Then
    '                sFiltro = " and d.Cod_Fuente_Dato = '" & txtFiltrarCodFuenteDatoD.Text & "'"
    '            End If
    '            If txtFiltroFechaFinD.Text <> "" And txtFiltroFechaIniD.Text <> "" Then
    '                sFiltro = sFiltro & " and Fecha_medida between '" & txtFiltroFechaIniD.Text & "' and '" & txtFiltroFechaFinD.Text & "'"
    '            ElseIf txtFiltroFechaFinD.Text = "" And txtFiltroFechaIniD.Text <> "" Then
    '                Alert(Page, "La Fecha Hasta no puede ser nula")
    '                sFiltro = ""
    '            ElseIf txtFiltroFechaFinD.Text <> "" And txtFiltroFechaIniD.Text = "" Then
    '                Alert(Page, "La Fecha Desde no puede ser nula")
    '                sFiltro = ""
    '            End If
    '        ElseIf txtFiltroFechaFinD.Text = "" And txtFiltroFechaIniD.Text <> "" Then
    '            Alert(Page, "La Fecha Hasta no puede ser nula")
    '            sFiltro = ""
    '        ElseIf txtFiltroFechaFinD.Text <> "" And txtFiltroFechaIniD.Text = "" Then
    '            Alert(Page, "La Fecha Desde no puede ser nula")
    '            sFiltro = ""
    '        ElseIf txtFiltroFechaFinD.Text = "" And txtFiltroFechaIniD.Text = "" Then
    '            'ncm cometado el 29/10/2009 por petición usuario incidencia 159
    '            'If Telemedida = "" Then
    '            '    'RDF. Fecha: 03/03/2009
    '            '    txtFiltroFechaIniD.Text = fechainicio
    '            '    txtFiltroFechaFinD.Text = fechaFin
    '            'End If
    '            sFiltro = sFiltro & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
    '        Else
    '            sFiltro = ""
    '        End If


    '        'RDF 20080714
    '        If Session("FiltroD") <> "" Then
    '            sentenciaSel = sentenciaSel & Session("FiltroD")
    '        End If

    '        sentenciaOrder = " order by codigoPVYCR, Fecha_Medida  desc, d.Cod_Fuente_dato "

    '        If sFiltro <> "" Then
    '            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
    '            sentenciaSelCount = sentenciaSelCount & sFiltro
    '        Else
    '            sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
    '        End If

    '        daElementos.SelectCommand.CommandText = sentenciaSel

    '        'IPIM 26/11/2008: Descomentamos para paginar
    '        'daElementos.Fill(dstElementos, (CInt(ucPaginacionA.lblPaginatext) - 1) * ucPaginacionA.pageSize, ucPaginacionA.pageSize, "TablaAcequias")
    '        daElementos.Fill(dstElementos, "TablaDiferencial")
    '        'Cálculo del número de páginas
    '        'ordenamos las lecturas en un dataview por fecha

    '        Dim txtComando As String = ""
    '        txtComando = daElementos.SelectCommand.CommandText
    '        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
    '        'IPIM 26/11/2008: Descomentamos para paginar
    '        ucPaginacionD.calcularPags(txtComando)
    '        Dim Tabla As DataTable
    '        Tabla = dstElementos.Tables("TablaDiferencial").Clone()
    '        For i = dstElementos.Tables("TablaDiferencial").Rows.Count - 1 To 0 Step -1
    '            Tabla.Rows.Add(dstElementos.Tables("TablaDiferencial").Rows(i).ItemArray)
    '        Next
    '        dstElementos.Tables.Remove(dstElementos.Tables("TablaDiferencial"))
    '        dstElementos.Tables.Add(Tabla)

    '        obtenerVolumenDiferencial("D", dstElementos.Tables("TablaDiferencial"), Page, mensaje_final)
    '        'obtenerVolumenDiferencial("D")
    '        'obtenerCaudalAcumulado()
    '        If chkReducirLecD.Checked Then
    '            'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
    '            Dim dstNuevo As DataSet
    '            dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaDiferencial", "suministromensualm3", 5)
    '            dstElementos = dstNuevo.Copy
    '        End If

    '        CrearTablaParaRepeater("TablaDiferencial", "D")

    '    End If

    'End Sub


    Protected Sub btnFiltroAceptarA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptarA.Click
        'EGB 21042008
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblPaginatext = "1"

        'IPIM 06052008  
        Dim tipoelem As String = "Q"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text, )
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
        txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos("Q")
        'FIN IPIM

        Session("dst") = dstElementos.Tables("TablaAcequias")              'IPIM 20080821: Para pasarle el dataset entero a los informes

        'IPIM 24/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptAcequias.DataSource = dstElementos.Tables("TablaAcequiasPag")
        'If txtFiltroNregQ.Text = "" And dstElementos.Tables("TablaAcequias").Rows.Count > 100 Then
        '    dstPantalla = dstElementos.Copy
        '    Dim i As Integer = 0
        '    Do While dstPantalla.Tables("TablaAcequias").Rows.Count > 100
        '        dstPantalla.Tables("TablaAcequias").Rows(0).Delete()
        '        i = i + 1
        '    Loop
        '    rptAcequias.DataSource = dstPantalla.Tables("TablaAcequias")
        '    lblLectPantallaQ.Text = "Lecturas cargadas en pantalla: <b>100</b>"
        'Else
        '    rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
        '    lblLectPantallaQ.Text = ""
        'End If
        'FIN IPIM 24/11/2008

        rptAcequias.DataBind()

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblNumpaginasDatabind()

    End Sub
    Protected Sub btnFiltrocancelarA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelarA.Click
        'EGB 21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblPaginatext = "1"

        Dim tipoelem As String = "Q"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        txtfiltroFechaIni.Text = ""
        txtFiltroFechaFin.Text = ""
        txtFiltrarCodFuentedato.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasQ.Checked = True
        txtFiltroNregQ.Text = ""
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
        txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)

        Session("dst") = dstElementos.Tables("TablaAcequias")              'IPIM 20080821: Para pasarle el dataset entero a los informes

        'IPIM 24/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptAcequias.DataSource = dstElementos.Tables("TablaAcequiasPag")
        'If txtFiltroNregQ.Text = "" And dstElementos.Tables("TablaAcequias").Rows.Count > 100 Then
        '    dstPantalla = dstElementos.Copy
        '    Dim i As Integer = 0
        '    Do While dstPantalla.Tables("TablaAcequias").Rows.Count > 100
        '        dstPantalla.Tables("TablaAcequias").Rows(0).Delete()
        '        i = i + 1
        '    Loop
        '    rptAcequias.DataSource = dstPantalla.Tables("TablaAcequias")
        '    lblLectPantallaQ.Text = "Lecturas cargadas en pantalla: <b>100</b>"
        'Else
        '    rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
        '    lblLectPantallaQ.Text = ""
        'End If
        'FIN IPIM 24/11/2008

        rptAcequias.DataBind()
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblNumpaginasDatabind()
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'IPIM 26/11/2008: Descomentamos para paginar
        If ucPaginacionA.lblMuevetext = "si" Then
            'declaramos dentro las variables, porque al hacer el load la primera vez el árbol aún no está cargado
            ' y da error
            'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
            'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

            'IPIM 26/11/2008
            Dim tipoelem As String = "Q"
            Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
            AgregarCalendario(tipoelem)
            'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
            CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
            txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
            FinalizarDatasetElementos(tipoelem)
            rptAcequias.DataSource = dstElementos.Tables("TablaAcequiasPag")
            rptAcequias.DataBind()
            ucPaginacionA.lblMuevetext = "no"
        End If
        If ucPaginacionE.lblMuevetext = "si" Then
            'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
            'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

            'IPIM 26/11/2008
            Dim tipoelem As String = "E"
            Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
            AgregarCalendario(tipoelem)
            'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
            CrearDatasetDiferencial(tipoelem, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
            txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
            FinalizarDatasetElementos(tipoelem)
            rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacionPag")
            rptEnergia.DataBind()
            ucPaginacionE.lblMuevetext = "no"
        End If
        If ucPaginacionV.lblMuevetext = "si" Then
            'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
            'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

            'IPIM 26/11/2008
            Dim tipoelem As String = "V"
            Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
            AgregarCalendario(tipoelem)
            'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
            CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
            txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
            FinalizarDatasetElementos(tipoelem)
            rptVolumen.DataSource = dstElementos.Tables("TablaMotoresPag")
            rptVolumen.DataBind()
            ucPaginacionV.lblMuevetext = "no"
        End If
        If ucPaginacionH.lblMuevetext = "si" Then
            'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
            'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

            'IPIM 26/11/2008
            Dim tipoelem As String = "H"
            Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
            AgregarCalendario(tipoelem)
            'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
            CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegH.Text, chkFiltroNulasH.Checked, _
            txtFiltroFechaFinH.Text, txtFiltrofechaIniH.Text, txtFiltrarCodFuenteDatoH.Text, Session("FiltroH"), ucPaginacionH, chkReducirLecH.Checked, dstElementos, sentenciaSel)
            FinalizarDatasetElementos(tipoelem)
            rptHorometros.DataSource = dstElementos.Tables("TablaHorometrosPag")
            rptHorometros.DataBind()
            ucPaginacionH.lblMuevetext = "no"
        End If
        If ucPaginacionD.lblMuevetext = "si" Then

            'IPIM 26/11/2008
            Dim tipoelem As String = "D"
            Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
            AgregarCalendario(tipoelem)
            'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
            CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegD.Text, chkFiltroNulasD.Checked, _
            txtFiltroFechaFinD.Text, txtFiltroFechaIniD.Text, txtFiltrarCodFuenteDatoD.Text, Session("FiltroD"), ucPaginacionD, chkReducirLecD.Checked, dstElementos, sentenciaSel)
            FinalizarDatasetElementos(tipoelem)
            rptDiferencial.DataSource = dstElementos.Tables("TablaDiferencialPag")
            rptDiferencial.DataBind()
            ucPaginacionD.lblMuevetext = "no"
        End If

    End Sub

    Protected Sub Cargar_Elementos(ByVal tipoElemen As String, ByVal codigoPVYCR As String, ByVal idElemento As String)
        'Page.MaintainScrollPositionOnPostBack = True

        'If Not IsPostBack Then

        '---------------------------------------------------------
        'EGB 21042008
        'lblCabecera.Text = genHTML.cabecera(Page)
        'lblPestanyas.Text = genHTML.pestanyasMenu(4, "../", Page)
        '---------------------------------------------------------
        'esto es para la paginacion NO borrar
        'IPIM 26/11/2008: Descomentamos para paginar
        If Request.QueryString("pag") <> "" Then
            ucPaginacionA.lblPaginatext = Request.QueryString("pag")
            ucPaginacionE.lblPaginatext = Request.QueryString("pag")
            ucPaginacionV.lblPaginatext = Request.QueryString("pag")
            ucPaginacionH.lblPaginatext = Request.QueryString("pag")
            ucPaginacionD.lblPaginatext = Request.QueryString("pag")
        Else
            ucPaginacionA.lblPaginatext = "1"
            ucPaginacionE.lblPaginatext = "1"
            ucPaginacionV.lblPaginatext = "1"
            ucPaginacionH.lblPaginatext = "1"
            ucPaginacionD.lblPaginatext = "1"
        End If

        'antes de cargar los elementos ponemos el filtro de nulo deschekeado por si
        'lo habían marcado anteriormente
        'ncm 20080605 chkFiltroNulasE.Checked = False
        ''chkFiltroPorDiaE.Checked = False
        'chkFiltroNulasQ.Checked = False
        'chkFiltroNulasV.Checked = False
        AgregarCalendario(tipoElemen)
      
        'ponemos visibles los paneles y cargamos los datos en pantalla
        'ncm 28/10/2009, lo necesito para obtener la concesión
        Dim ClaveNodoTipo As String = Request.QueryString("nodoclave").ToString
        'Obtenemos la clave del nodo y el tipo
        Dim codigoCauce As String = ClaveNodoTipo.Substring(0, ClaveNodoTipo.Substring(0, ClaveNodoTipo.IndexOf(";")).IndexOf("P"))
        If tipoElemen = "Q" Then
            pnlIndependiente.Visible = False
            pnlAcequias.Visible = True
            pnlEnergia.Visible = False
            pnlVolumen.Visible = False
            pnlHorometros.Visible = False
            pnlEDAcequias.Visible = False
            pnlEDEnergia.Visible = False
            pnlEDVolumen.Visible = False
            pnlEDHorometros.Visible = False
            pnlDiferencial.Visible = False
            'creamos datasets
            'crearDataSets_Elementos(tipoElemen, codigoPVYCR, idElemento)
            CrearDatasetDiferencial(tipoElemen, codigoPVYCR, idElemento, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
            txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
            FinalizarDatasetElementos(tipoElemen)
            'obtenemos
            Session("dst") = dstElementos.Tables("TablaAcequias")              'IPIM 20080821: Para pasarle el dataset entero a los informes

            'IPIM 24/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
            rptAcequias.DataSource = dstElementos.Tables("TablaAcequiasPag")
            rptAcequias.DataBind()

            'RDF 20080715
            Session("Filtro") = ""
            rellenarListas()
            'IPIM 26/11/2008: Descomentamos para paginar
            ucPaginacionA.lblNumpaginasDatabind()
            'ncm 18/07/2008
            'lblPestanyasArbolQ.Text = genHTML.EnlacesMenuArbol(4, "../", Page, 11, "E", codigoPVYCR)

            Me.lblobtenerNumLecturasQ.Text = Me.obtenerNumLecturasQ()
            Me.lblobtenerTotNumLecturasQ.Text = Me.obtenerTotNumLecturas("Q", codigoPVYCR, idElemento)
            Me.lblCaudalAcumuladoQ.Text = Me.obtenerCaudalAcumulado
            'RDF 01/10/2008
            Me.lblObtenerAnyoHidrologicoQ.Text = Me.obtenerAnyoHidrologicoEtiqueta()

            If DateTime.Now.Month < 10 Then
                Me.lblObtenerAnyoHidrologicoQ.Attributes.Add("onclick", "CargarAnyosQ('01/10/" & DateTime.Now.Year - 1 & "','30/09/" & DateTime.Now.Year & "');return false;")
            Else
                Me.lblObtenerAnyoHidrologicoQ.Attributes.Add("onclick", "CargarAnyosQ('01/10/" & DateTime.Now.Year & "','30/09/" & DateTime.Now.Year + 1 & "');return false;")
            End If


            Me.lblIntervaloFechasQ.Text = Me.obtenerIntervaloFechasQ()
            Me.lblObtenerNomElementoA.Text = Me.obtenerNomElemento()
            Me.lblVolMaxAnuLegQ.Text = ObtenerConcesion(codigoCauce)
            Me.lblPorConsumidoQ.Text = ObtenerPorConcesion(lblVolMaxAnuLegQ.Text, lblCaudalAcumuladoQ.Text)
        ElseIf tipoElemen = "E" Then
            pnlIndependiente.Visible = False
            pnlAcequias.Visible = False
            pnlEnergia.Visible = True
            pnlVolumen.Visible = False
            pnlHorometros.Visible = False
            pnlDiferencial.Visible = False
            pnlEDAcequias.Visible = False
            pnlEDEnergia.Visible = False
            pnlEDVolumen.Visible = False
            pnlEDHorometros.Visible = False
            pnlEDDiferencial.Visible = False
            'creamos datasets
            'crearDataSets_Elementos(tipoElemen, codigoPVYCR, idElemento)
            CrearDatasetDiferencial(tipoElemen, codigoPVYCR, idElemento, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
            txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
            FinalizarDatasetElementos(tipoElemen)
            Session("dst") = dstElementos.Tables("TablaAlimentacion")              'IPIM 20080912: Para pasarle el dataset entero a los informes

            'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
            rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacionPag")
            rptEnergia.DataBind()
            'RDF 01/10/2008
            If DateTime.Now.Month < 10 Then
                Me.lblAnyoHidrologicoE.Attributes.Add("onclick", "CargarAnyosE('01/10/" & DateTime.Now.Year - 1 & "','30/09/" & DateTime.Now.Year & "');return false;")
            Else
                Me.lblAnyoHidrologicoE.Attributes.Add("onclick", "CargarAnyosE('01/10/" & DateTime.Now.Year & "','30/09/" & DateTime.Now.Year + 1 & "');return false;")
            End If

            Me.lblAnyoHidrologicoE.Text = obtenerAnyoHidrologicoEtiqueta()
            Session("FiltroE") = ""
            rellenarListasE()
            Me.lblobtenervolumenacumulado.Text = Me.obtenerVolumenElectricoAcumulado
            Me.lblobtenervolumenacumuladoE.Text = Me.obtenerVolumenElectricoAcumuladoM3
            Me.lblobtenerNumlecturasE.Text = Me.obtenerNumLecturasE
            Me.lblobtenerTotNumLecturasE.Text = Me.obtenerTotNumLecturas("E", codigoPVYCR, idElemento)
            Me.lblIntervaloFechaE.Text = Me.obtenerIntervaloFechasE()
            Me.lblObtenerNomElementoE.Text = Me.obtenerNomElemento()
            'ncm 18/07/2008
            'lblPestanyasArbolQ.Text = genHTML.EnlacesMenuArbol(4, "../", Page, 11, "E", codigoPVYCR)
            Me.lblVolMaxAnuLegE.Text = ObtenerConcesion(codigoCauce)
            Me.lblPorConsumidoE.Text = ObtenerPorConcesion(lblVolMaxAnuLegE.Text, lblobtenervolumenacumuladoE.Text)
            'IPIM 26/11/2008: Descomentamos para paginar
            ucPaginacionE.lblNumpaginasDatabind()
        ElseIf tipoElemen = "V" Then
            pnlIndependiente.Visible = False
            pnlAcequias.Visible = False
            pnlEnergia.Visible = False
            pnlVolumen.Visible = True
            pnlHorometros.Visible = False
            pnlDiferencial.Visible = False
            pnlEDAcequias.Visible = False
            pnlEDEnergia.Visible = False
            pnlEDVolumen.Visible = False
            pnlEDHorometros.Visible = False
            pnlEDDiferencial.Visible = False
            'creamos datasets
            'crearDataSets_Elementos(tipoElemen, codigoPVYCR, idElemento)
            CrearDatasetDiferencial(tipoElemen, codigoPVYCR, idElemento, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
            txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
            FinalizarDatasetElementos(tipoElemen)

            Session("dst") = dstElementos.Tables("TablaMotores")              'IPIM 20080912: Para pasarle el dataset entero a los informes

            'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
            rptVolumen.DataSource = dstElementos.Tables("TablaMotoresPag")
            rptVolumen.DataBind()

            Session("FiltroM") = ""
            rellenarListasM()
            'IPIM 26/11/2008: Descomentamos para paginar
            ucPaginacionV.lblNumpaginasDatabind()
            'ncm 18/07/2008
            'lblPestanyasArbolQ.Text = genHTML.EnlacesMenuArbol(4, "../", Page, 11, "E", codigoPVYCR)
            'RDF 01/10/2008
            If DateTime.Now.Month < 10 Then
                Me.lblAnyoHidrologicoV.Attributes.Add("onclick", "CargarAnyosV('01/10/" & DateTime.Now.Year - 1 & "','30/09/" & DateTime.Now.Year & "');return false;")
            Else
                Me.lblAnyoHidrologicoV.Attributes.Add("onclick", "CargarAnyosV('01/10/" & DateTime.Now.Year & "','30/09/" & DateTime.Now.Year + 1 & "');return false;")
            End If
            Me.lblAnyoHidrologicoV.Text = obtenerAnyoHidrologicoEtiqueta()

            Me.lblobtenervolumenacumuladoV.Text = Me.obtenerVolumenAcumulado
            Me.lblobtenerNumlecturasV.Text = Me.obtenerNumLecturasV
            Me.lblobtenerTotNumLecturasV.Text = Me.obtenerTotNumLecturas("V", codigoPVYCR, idElemento)
            Me.lblIntervaloFechaV.Text = Me.obtenerIntervaloFechasV()
            Me.lblobtenerNomElementoV.Text = Me.obtenerNomElemento()
            Me.lblVolMaxAnuLegV.Text = ObtenerConcesion(codigoCauce)
            Me.lblPorConsumidoV.Text = ObtenerPorConcesion(lblVolMaxAnuLegV.Text, lblobtenervolumenacumuladoV.Text)
        ElseIf tipoElemen = "H" Then
            pnlIndependiente.Visible = False
            pnlAcequias.Visible = False
            pnlEnergia.Visible = False
            pnlVolumen.Visible = False
            pnlHorometros.Visible = True
            pnlEDAcequias.Visible = False
            pnlEDEnergia.Visible = False
            pnlEDVolumen.Visible = False
            pnlEDHorometros.Visible = False
            pnlDiferencial.Visible = False
            'creamos datasets
            'crearDataSets_Elementos(tipoElemen, codigoPVYCR, idElemento)
            CrearDatasetDiferencial(tipoElemen, codigoPVYCR, idElemento, Page, txtFiltroNRegH.Text, chkFiltroNulasH.Checked, _
            txtFiltroFechaFinH.Text, txtFiltrofechaIniH.Text, txtFiltrarCodFuenteDatoH.Text, Session("FiltroH"), ucPaginacionH, chkReducirLecH.Checked, dstElementos, sentenciaSel)
            FinalizarDatasetElementos(tipoElemen)
            Session("dst") = dstElementos.Tables("TablaHorometros")              'IPIM 20080912: Para pasarle el dataset entero a los informes

            'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
            rptHorometros.DataSource = dstElementos.Tables("TablaHorometrosPag")
            rptHorometros.DataBind()

            Session("FiltroH") = ""
            rellenarListasH()
            'IPIM 26/11/2008: Descomentamos para paginar
            ucPaginacionH.lblNumpaginasDatabind()
            'ncm 18/07/2008
            'lblPestanyasArbolQ.Text = genHTML.EnlacesMenuArbol(4, "../", Page, 11, "E", codigoPVYCR)
            'RDF 01/10/2008
            If DateTime.Now.Month < 10 Then
                Me.lblAnyoHidrologicoH.Attributes.Add("onclick", "CargarAnyosH('01/10/" & DateTime.Now.Year - 1 & "','30/09/" & DateTime.Now.Year & "');return false;")
            Else
                Me.lblAnyoHidrologicoH.Attributes.Add("onclick", "CargarAnyosH('01/10/" & DateTime.Now.Year & "','30/09/" & DateTime.Now.Year + 1 & "');return false;")
            End If
            Me.lblAnyoHidrologicoH.Text = obtenerAnyoHidrologicoEtiqueta()
            Me.lblobtenerNumLecturasH.Text = Me.obtenerNumLecturasH
            Me.lblobtenerVolumenAcumuladoHoras.Text = Me.obtenerVolumenAcumuladoHoras()
            Me.lblobtenervolumenacumuladoH.Text = Me.obtenerVolumenHorometroAcumulado
            Me.lblobtenerTotNumLecturasH.Text = Me.obtenerTotNumLecturas("H", codigoPVYCR, idElemento)
            Me.lblIntervaloFechaH.Text = Me.obtenerIntervaloFechasH()
            Me.lblObtenerNomElementoH.Text = Me.obtenerNomElemento()
            Me.lblVolMaxAnuLegH.Text = ObtenerConcesion(codigoCauce)
            Me.lblPorConsumidoH.Text = ObtenerPorConcesion(lblVolMaxAnuLegH.Text, lblobtenervolumenacumuladoH.Text)
        ElseIf tipoElemen = "D" Then
            pnlIndependiente.Visible = False
            pnlAcequias.Visible = False
            pnlEnergia.Visible = False
            pnlVolumen.Visible = False
            pnlHorometros.Visible = False
            pnlEDAcequias.Visible = False
            pnlEDEnergia.Visible = False
            pnlEDVolumen.Visible = False
            pnlEDHorometros.Visible = False
            pnlDiferencial.Visible = True
            pnlEDDiferencial.Visible = False
            'creamos datasets
            'crearDataSets_Elementos(tipoElemen, codigoPVYCR, idElemento)
            CrearDatasetDiferencial(tipoElemen, codigoPVYCR, idElemento, Page, txtFiltroNRegD.Text, chkFiltroNulasD.Checked, _
            txtFiltroFechaFinD.Text, txtFiltroFechaIniD.Text, txtFiltrarCodFuenteDatoD.Text, Session("FiltroD"), ucPaginacionD, chkReducirLecD.Checked, dstElementos, sentenciaSel)
            FinalizarDatasetElementos(tipoElemen)

            Session("dst") = dstElementos.Tables("TablaDiferencial")              'IPIM 20080912: Para pasarle el dataset entero a los informes

            'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
            rptDiferencial.DataSource = dstElementos.Tables("TablaDiferencialPag")

            rptDiferencial.DataBind()

            Session("FiltroD") = ""
            rellenarListasD()
            'IPIM 26/11/2008: Descomentamos para paginar
            ucPaginacionD.lblNumpaginasDatabind()
            'ncm 18/07/2008
            'lblPestanyasArbolQ.Text = genHTML.EnlacesMenuArbol(4, "../", Page, 11, "E", codigoPVYCR)
            'RDF 01/10/2008
            If DateTime.Now.Month < 10 Then
                Me.lblAnyoHidrologicoD.Attributes.Add("onclick", "CargarAnyosD('01/10/" & DateTime.Now.Year - 1 & "','30/09/" & DateTime.Now.Year & "');return false;")
            Else
                Me.lblAnyoHidrologicoD.Attributes.Add("onclick", "CargarAnyosD('01/10/" & DateTime.Now.Year & "','30/09/" & DateTime.Now.Year + 1 & "');return false;")
            End If
            Me.lblAnyoHidrologicoD.Text = obtenerAnyoHidrologicoEtiqueta()

            Me.lblobtenervolumenacumuladoD.Text = Me.obtenerVolumenDiferencialesAcumulado
            Me.lblobtenerNumlecturasD.Text = Me.obtenerNumLecturasD
            Me.lblobtenerTotNumLecturasD.Text = Me.obtenerTotNumLecturas("D", codigoPVYCR, idElemento)
            Me.lblIntervaloFechaD.Text = Me.obtenerIntervaloFechasd()
            Me.lblobtenerNomElementoD.Text = Me.obtenerNomElemento()
            Me.lblVolMaxAnuLegD.Text = ObtenerConcesion(codigoCauce)
            Me.lblPorConsumidoD.Text = ObtenerPorConcesion(lblVolMaxAnuLegD.Text, lblobtenervolumenacumuladoD.Text)
            Me.lblPorConsumidoD.Text = ObtenerPorConcesion(lblVolMaxAnuLegD.Text, lblobtenervolumenacumuladoD.Text)
        End If
    End Sub

    Protected Sub btnCancelarA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarA.Click
        pnlAcequias.Visible = False
        pnlEnergia.Visible = False
        pnlVolumen.Visible = False
        pnlHorometros.Visible = False
        'deseleccionamos el nodo para que si el usuario vuelve a seleccionar el mismo salte el selectednode.changed
        'que es el procedimiento que carga todos los datos
        'EGB 21042008 
        ''EGB 21042008 treeView1.SelectedNode.Selected = False
    End Sub

    Protected Sub btnFiltroAceptarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptarE.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim tipoelem As String = "E"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblPaginatext = "1"

        AgregarCalendario(tipoelem)
        '        crearDataSets_Elementos(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
        txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAlimentacion")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacionPag")
        rptEnergia.DataBind()
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnFiltroCancelarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelarE.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim tipoelem As String = "E"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblPaginatext = "1"
        txtFiltroFechaIniE.Text = ""
        txtFiltroFechaFinE.Text = ""
        txtFiltrarCodFuenteDatoE.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasE.Checked = True
        txtFiltroNRegE.Text = ""
        'chkFiltroPorDiaE.Checked = False
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
        txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAlimentacion")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacionPag")
        'If txtFiltroNRegE.Text = "" And dstElementos.Tables("TablaAlimentacion").Rows.Count > 100 Then
        '    dstPantalla = dstElementos.Copy
        '    Dim i As Integer = 0
        '    Do While dstPantalla.Tables("TablaAlimentacion").Rows.Count > 100
        '        dstPantalla.Tables("TablaAlimentacion").Rows(0).Delete()
        '        i = i + 1
        '    Loop
        '    rptEnergia.DataSource = dstPantalla.Tables("TablaAlimentacion")
        '    lblLectPantallaE.Text = "Lecturas cargadas en pantalla: <b>100</b>"
        'Else
        '    rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacion")
        '    lblLectPantallaE.Text = ""
        'End If
        'FIN IPIM 25/11/2008

        rptEnergia.DataBind()
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblNumpaginasDatabind()

    End Sub

    Protected Sub btnCancelarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarE.Click
        pnlAcequias.Visible = False
        pnlEnergia.Visible = False
        pnlVolumen.Visible = False
        pnlHorometros.Visible = False

        'deseleccionamos el nodo para que si el usuario vuelve a seleccionar el mismo salte el selectednode.changed
        'que es el procedimiento que carga todos los datos
        'EGB 21042008 treeView1.SelectedNode.Selected = False
    End Sub


    Protected Sub btnFiltroAceptarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfiltroAceptarV.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim tipoelem As String = "V"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionV.lblPaginatext = "1"
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
        txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaMotores")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptVolumen.DataSource = dstElementos.Tables("TablaMotoresPag")
        rptVolumen.DataBind()
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionV.lblNumpaginasDatabind()
        Me.lblAnyoHidrologicoV.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumuladoV.Text = Me.obtenerVolumenAcumulado
        Me.lblobtenerNumlecturasV.Text = Me.obtenerNumLecturasV
        Me.lblobtenerTotNumLecturasV.Text = Me.obtenerTotNumLecturas("V", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaV.Text = Me.obtenerIntervaloFechasV()
        Me.lblobtenerNomElementoV.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoV.Text = ObtenerPorConcesion(lblVolMaxAnuLegV.Text, lblobtenervolumenacumuladoV.Text)

        'IPIM 27/11/2008
        ucPaginacionV.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnFiltroCancelarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelarV.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        Dim tipoelem As String = "V"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionV.lblPaginatext = "1"

        txtFiltroFechaIniV.Text = ""
        txtFiltroFechaFinV.Text = ""
        txtFiltrarCodFuenteDatoV.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasV.Checked = True
        txtFiltroNRegV.Text = ""
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
        txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaMotores")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptVolumen.DataSource = dstElementos.Tables("TablaMotoresPag")
        rptVolumen.DataBind()
        Me.lblAnyoHidrologicoV.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumuladoV.Text = Me.obtenerVolumenAcumulado
        Me.lblobtenerNumlecturasV.Text = Me.obtenerNumLecturasV
        Me.lblobtenerTotNumLecturasV.Text = Me.obtenerTotNumLecturas("V", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaV.Text = Me.obtenerIntervaloFechasV()
        Me.lblobtenerNomElementoV.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoV.Text = ObtenerPorConcesion(lblVolMaxAnuLegV.Text, lblobtenervolumenacumuladoV.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionV.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnCancelarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarV.Click
        pnlAcequias.Visible = False
        pnlEnergia.Visible = False
        pnlVolumen.Visible = False
        pnlHorometros.Visible = False

        'deseleccionamos el nodo para que si el usuario vuelve a seleccionar el mismo salte el selectednode.changed
        'que es el procedimiento que carga todos los datos
        'EGB 21042008 treeView1.SelectedNode.Selected = False
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
                total = "0"
            Else
                volumen = dstElementos.Tables("TablaAlimentacion").Rows(dstElementos.Tables("TablaAlimentacion").Rows.Count - 1).Item("Diferencial_Acum").ToString
                total = String.Format("{0:#,##0.##}", volumen)
            End If

            Return total
        Else
            Return "0"
        End If
    End Function
    Protected Function obtenerVolumenHorometroAcumulado() As String
        Dim volumen As Decimal
        Dim total As String
        'RDF
        'Fecha: 03/03/2009
        'Calculo Horas Parciales


        'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
        If dstElementos.Tables("TablaHorometros").Rows.Count > 0 Then
            '---------------código que utilizabamos antes de obtener el diferencial-----------------
            'volumenIni = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(0).Item("Total_kwh").ToString)
            'volumenFin = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(dstElementos.Tables("TablaAlimentacion").Rows.Count - 1).Item("Total_kwh").ToString)
            'total = String.Format("{0:#,##0.##}", volumenFin - volumenIni)
            'Return total
            '----------------------------------------------------------------------------------------
            If dstElementos.Tables("TablaHorometros").Rows(dstElementos.Tables("TablaHorometros").Rows.Count - 1).Item("Diferencial_Acum").ToString() = "" Then
                total = "0"
            Else
        volumen = dstElementos.Tables("TablaHorometros").Rows(dstElementos.Tables("TablaHorometros").Rows.Count - 1).Item("Diferencial_Acum").ToString
                total = String.Format("{0:#,##0.##}", volumen)
            End If

            Return total & " (*)"
        Else
            Return "0 (*)"
        End If
    End Function
    Protected Function obtenerVolumenDiferencialesAcumulado() As String
        Dim volumen As Double
        Dim total As String
        'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
        If dstElementos.Tables("TablaDiferencial").Rows.Count > 0 Then
            '---------------código que utilizabamos antes de obtener el diferencial-----------------
            'volumenIni = Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(0).Item("LecturaContador_M3").ToString)
            'volumenFin = Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(dstElementos.Tables("TablaMotores").Rows.Count - 1).Item("LecturaContador_M3").ToString)
            'total = String.Format("{0:#,##0.##}", volumenFin - volumenIni)
            'Return total
            '----------------------------------------------------------------------------------------
            If dstElementos.Tables("TablaDiferencial").Rows(dstElementos.Tables("TablaDiferencial").Rows.Count - 1).Item("Diferencial").ToString = "" Then
                total = ""
            Else
                volumen = dstElementos.Tables("TablaDiferencial").Rows(dstElementos.Tables("TablaDiferencial").Rows.Count - 1).Item("Diferencial").ToString
                total = String.Format("{0:#,##0.##}", volumen)
            End If

            Return total
        Else
            Return "0"
        End If
    End Function
    Protected Function obtenerVolumenElectricoAcumuladoM3() As String
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
            If dstElementos.Tables("TablaAlimentacion").Rows(dstElementos.Tables("TablaAlimentacion").Rows.Count - 1).Item("Diferencial_AcumM3").ToString() = "" Then
                total = "0 (*)"
            Else
                volumen = dstElementos.Tables("TablaAlimentacion").Rows(dstElementos.Tables("TablaAlimentacion").Rows.Count - 1).Item("Diferencial_AcumM3").ToString
                total = String.Format("{0:#,##0.##}", volumen)
            End If

            Return total & " (*)"
        Else
            Return "0 (*)"
        End If
    End Function
    Protected Function obtenerCaudalAcumulado() As String
        Dim volumen As Decimal
        Dim total As String
        'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
        If dstElementos.Tables("TablaAcequias").Rows.Count > 0 Then

            If dstElementos.Tables("TablaAcequias").Rows(dstElementos.Tables("TablaAcequias").Rows.Count - 1).Item("Diferencial_Acum").ToString() = "" Then
                'Dim Dr As DataRow() = dstElementos.Tables("TablaAcequias").Select("diferencial_acum is not null")

                ''ncm 23/10/2007 buscamos el inmediatamente anterior que no tenga valor nulo
                'volumen = Dr(Dr.Length - 1).Item("Diferencial_Acum")
                'total = String.Format("{0:#,##0.##}", volumen)
                Dim j As Integer
                For j = dstElementos.Tables("TablaAcequias").Rows.Count - 1 To 0 Step -1
                    If dstElementos.Tables("TablaAcequias").Rows(j).Item("Diferencial_Acum").ToString() <> "" Then
                        total = dstElementos.Tables("TablaAcequias").Rows(j).Item("Diferencial_Acum").ToString()
                        Exit For
                    End If
                Next

                'total = ""
            Else
                volumen = dstElementos.Tables("TablaAcequias").Rows(dstElementos.Tables("TablaAcequias").Rows.Count - 1).Item("Diferencial_Acum").ToString
                total = String.Format("{0:#,##0.##}", volumen)
            End If




            Return total

        Else
            Return "0"
        End If

    End Function
    Protected Function obtenerVolumenAcumuladoHoras() As String
        Dim volumen As Decimal
        Dim total As String
        'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
        If dstElementos.Tables("TablaHorometros").Rows.Count > 0 Then
            '---------------código que utilizabamos antes de obtener el diferencial-----------------
            'volumenIni = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(0).Item("Total_kwh").ToString)
            'volumenFin = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(dstElementos.Tables("TablaAlimentacion").Rows.Count - 1).Item("Total_kwh").ToString)
            'total = String.Format("{0:#,##0.##}", volumenFin - volumenIni)
            'Return total
            '----------------------------------------------------------------------------------------
            If dstElementos.Tables("TablaHorometros").Rows(dstElementos.Tables("TablaHorometros").Rows.Count - 1).Item("Diferencial_Acum_horas").ToString() = "" Then
                total = "0"
            Else
                volumen = dstElementos.Tables("TablaHorometros").Rows(dstElementos.Tables("TablaHorometros").Rows.Count - 1).Item("Diferencial_Acum_horas").ToString
                total = String.Format("{0:#,##0.##}", volumen)
            End If

            Return total
        Else
            Return "0"
        End If
    End Function

    Protected Function obtenerCodigoPVYCR() As String
        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        'Return codigoPVYCR
        Return Me.txtCodigoPVYCR.Text
    End Function

    Protected Sub btnFiltroAceptarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptarH.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        Dim tipoelem As String = "H"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionH.lblPaginatext = "1"

        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegH.Text, chkFiltroNulasH.Checked, _
        txtFiltroFechaFinH.Text, txtFiltrofechaIniH.Text, txtFiltrarCodFuenteDatoH.Text, Session("FiltroH"), ucPaginacionH, chkReducirLecH.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaHorometros")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptHorometros.DataSource = dstElementos.Tables("TablaHorometrosPag")
        rptHorometros.DataBind()
        Me.lblAnyoHidrologicoH.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenerNumLecturasH.Text = Me.obtenerNumLecturasH
        Me.lblobtenerVolumenAcumuladoHoras.Text = Me.obtenerVolumenAcumuladoHoras()
        Me.lblobtenervolumenacumuladoH.Text = Me.obtenerVolumenHorometroAcumulado
        Me.lblobtenerTotNumLecturasH.Text = Me.obtenerTotNumLecturas("H", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaH.Text = Me.obtenerIntervaloFechasH()
        Me.lblObtenerNomElementoH.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoH.Text = ObtenerPorConcesion(lblVolMaxAnuLegH.Text, lblobtenervolumenacumuladoH.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionH.lblNumpaginasDatabind()

    End Sub

    Protected Sub btnFiltroCancelarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelarH.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        Dim tipoelem As String = "H"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionH.lblPaginatext = "1"
        txtFiltrofechaIniH.Text = ""
        txtFiltroFechaFinH.Text = ""
        txtFiltrarCodFuenteDatoH.Text = ""
        txtFiltroNRegH.Text = ""
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegH.Text, chkFiltroNulasH.Checked, _
        txtFiltroFechaFinH.Text, txtFiltrofechaIniH.Text, txtFiltrarCodFuenteDatoH.Text, Session("FiltroH"), ucPaginacionH, chkReducirLecH.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaHorometros")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptHorometros.DataSource = dstElementos.Tables("TablaHorometrosPag")
        rptHorometros.DataBind()
        Me.lblAnyoHidrologicoH.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenerNumLecturasH.Text = Me.obtenerNumLecturasH
        Me.lblobtenerVolumenAcumuladoHoras.Text = Me.obtenerVolumenAcumuladoHoras()
        Me.lblobtenervolumenacumuladoH.Text = Me.obtenerVolumenHorometroAcumulado
        Me.lblobtenerTotNumLecturasH.Text = Me.obtenerTotNumLecturas("H", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaH.Text = Me.obtenerIntervaloFechasH()
        Me.lblObtenerNomElementoH.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoH.Text = ObtenerPorConcesion(lblVolMaxAnuLegH.Text, lblobtenervolumenacumuladoH.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionH.lblNumpaginasDatabind()

    End Sub

    Protected Sub btnCancelarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarH.Click
        pnlAcequias.Visible = False
        pnlEnergia.Visible = False
        pnlVolumen.Visible = False
        pnlHorometros.Visible = False
        'deseleccionamos el nodo para que si el usuario vuelve a seleccionar el mismo salte el selectednode.changed
        'que es el procedimiento que carga todos los datos
        'EGB 21042008 treeView1.SelectedNode.Selected = False
    End Sub

    Protected Sub rptEnergia_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptEnergia.ItemCommand
        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        Dim nombre As String
        Dim i As Integer
        Dim parametros() As String
        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Dim cod_fuente_dato As String = parametros(0)
                lblFechamedidaESel.Text = parametros(1)
                'mostramos el panel de edición
                'lblFechamedidaESel.Text = e.CommandArgument
                pnlEnergia.Visible = False
                'IPIM 26/11/2008: Descomentamos para paginar
                ucPaginacionE.Visible = False
                pnlEDEnergia.Visible = True
                pnlEDVolumen.Visible = False
                pnlEDAcequias.Visible = False

                creardatasetEd(lblidElemento.Text, codigoPVYCR, lblFechamedidaESel.Text, cod_fuente_dato)

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

                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion)

                comando.CommandText = "delete from pvycr_datosalimentacion where codigoPVYCR='" & codigoPVYCR & "' and idElementoMedida = '" & _
                lblidElemento.Text & "' and fecha_medida='" & parametros(1) & "' and cod_fuente_dato='" & parametros(0) & "' "

                comando.ExecuteNonQuery()
                AgregarCalendario("E")
                'Me.crearDataSets_Elementos("E", codigoPVYCR, lblidElemento.Text)
                CrearDatasetDiferencial("E", codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
                txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
                FinalizarDatasetElementos("E")
                Session("dst") = dstElementos.Tables("TablaAlimentacion")
                'para las consultas e inma NCM
                rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacionPag")
                rptEnergia.DataBind()
                'ucPaginacion.lblNumpaginasDatabind()

                'IPIM 26/11/2008: Descomentamos para paginar
                ucPaginacionE.lblNumpaginasDatabind()
        End Select
    End Sub
    Protected Sub creardatasetEd(ByVal idelemento As String, ByVal codigoPVYCR As String, ByVal fechaMedida As DateTime, ByVal codfuentedato As String)
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        If idelemento.Substring(0, 1).ToString = "E" Then

            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
                                "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion,  " & _
                                "IV.descripcion descIncElec, P.DenominacionPunto, F.FUENTE_DATOS DescFuenteDato " & _
                                ",D.modificado FROM PVYCR_DatosAlimentacion D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
                                "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida = '" & fechaMedida & "' and d.cod_fuente_dato='" & codfuentedato & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdAlimentacion")
            'datos energía
            'rptEnergia.DataSource = dstElementos.Tables("TablaEdAlimentacion")
            'rptEnergia.DataBind()
        ElseIf idelemento.Substring(0, 1).ToString = "Q" Then
            sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida,d.Cod_Fuente_Dato,d.Fecha_Medida,d.Escala_M,d.Calado_M " & _
                    ",d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.TipoObtencionCaudal,d.Duda_Calidad,d.Observaciones, p.denominacionpunto, F.FUENTE_DATOS DescFuenteDato " & _
                    ", d.modificado FROM PVYCR_DatosAcequias D " & _
                    "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                    "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                    "WHERE d.idElementoMedida = '" & idelemento & "' and d.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                    "d.Fecha_Medida = '" & fechaMedida & "' and d.cod_fuente_dato='" & codfuentedato & "' "
            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdAcequias")
        ElseIf idelemento.Substring(0, 1).ToString = "V" Then

            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.lecturaContador_m3, D.Funciona, D.Observaciones, D.idIncidenciavolumetrica, " & _
                                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, D.Justificacion,D.CaudalMedido, " & _
                                "IV.descripcion descIncVol, P.DenominacionPunto, F.FUENTE_DATOS DescFuenteDato " & _
                                ",d.modificado FROM PVYCR_DatosMotores D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida = '" & fechaMedida & "' and d.cod_fuente_dato='" & codfuentedato & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdMotores")
        ElseIf idelemento.Substring(0, 1).ToString = "H" Then

            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.HorasIntervalo, D.Funciona, D.Observaciones, D.idIncidenciavolumetrica, " & _
                                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                                "IV.descripcion descIncH, P.DenominacionPunto, F.FUENTE_DATOS DescFuenteDato " & _
                                ",d.modificado FROM PVYCR_DatosHorometros D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida = '" & fechaMedida & "' and d.cod_fuente_dato='" & codfuentedato & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdHorometros")
        ElseIf idelemento.Substring(0, 1).ToString = "D" Then

            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.suministromensualM3,D.Observaciones, " & _
                                "P.DenominacionPunto, F.FUENTE_DATOS DescFuenteDato " & _
                                "FROM PVYCR_DatosSuministros D " & _
                                "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida = '" & fechaMedida & "' and d.cod_fuente_dato='" & codfuentedato & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdDiferencial")

        End If

    End Sub
    Protected Sub creardatasetEd(ByVal idelemento As String, ByVal codigoPVYCR As String, ByVal fechaMedida As DateTime, ByVal codfuentedato As String, ByVal tipoobtencioncaudal As String)
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        If idelemento.Substring(0, 1).ToString = "E" Then

            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
                                "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion,  " & _
                                "IV.descripcion descIncElec, P.DenominacionPunto, F.FUENTE_DATOS DescFuenteDato " & _
                                ", D.modificado FROM PVYCR_DatosAlimentacion D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
                                "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida = '" & fechaMedida & "' and d.cod_fuente_dato='" & codfuentedato & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdAlimentacion")
            'datos energía
            'rptEnergia.DataSource = dstElementos.Tables("TablaEdAlimentacion")
            'rptEnergia.DataBind()
        ElseIf idelemento.Substring(0, 1).ToString = "Q" Then
            sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida,d.Cod_Fuente_Dato,d.Fecha_Medida,d.Escala_M,d.Calado_M " & _
                    ",d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.TipoObtencionCaudal,d.Duda_Calidad,d.Observaciones, p.denominacionpunto, F.FUENTE_DATOS DescFuenteDato " & _
                    ",d.modificado FROM PVYCR_DatosAcequias D " & _
                    "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                    "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                    "WHERE d.idElementoMedida = '" & idelemento & "' and d.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                    "d.Fecha_Medida = '" & fechaMedida & "' and d.cod_fuente_dato='" & codfuentedato & "' and d.tipoobtencioncaudal='" & tipoobtencioncaudal & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdAcequias")
        ElseIf idelemento.Substring(0, 1).ToString = "V" Then

            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.lecturaContador_m3, D.Funciona, D.Observaciones, D.idIncidenciavolumetrica, " & _
                                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, D.Justificacion,D.CaudalMedido, " & _
                                "IV.descripcion descIncVol, P.DenominacionPunto, F.FUENTE_DATOS DescFuenteDato " & _
                                ",d.modificado FROM PVYCR_DatosMotores D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida = '" & fechaMedida & "' and d.cod_fuente_dato='" & codfuentedato & "' "

            daElementos.SelectCommand.CommandText = sentenciaSel
            daElementos.Fill(dstElementos, "TablaEdMotores")
        ElseIf idelemento.Substring(0, 1).ToString = "H" Then

            sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.HorasIntervalo, D.Funciona, D.Observaciones, D.idIncidenciavolumetrica, " & _
                                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                                "IV.descripcion descIncH, P.DenominacionPunto, F.FUENTE_DATOS DescFuenteDato " & _
                                ",d.modificado FROM PVYCR_DatosHorometros D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "LEFT OUTER JOIN PVYCR_Puntos P ON p.codigoPVYCR = D.CodigoPVYCR  " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' and " & _
                                 "Fecha_Medida = '" & fechaMedida & "' and d.cod_fuente_dato='" & codfuentedato & "' "

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

        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        Dim tipoelem As String = "E"
    Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
    Dim cod_fuente_dato() As String = txtCodFuenteDato.Text.Split("-")


        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)

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
                              "and Fecha_Medida = '" & lblFechamedidaESel.Text & "' and cod_fuente_dato='" & cod_fuente_dato(0).Replace(" ", "").ToString & "' "


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
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.Visible = True
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
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
        txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAlimentacion")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacionPag")
        rptEnergia.DataBind()
        Me.lblAnyoHidrologicoE.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumulado.Text = Me.obtenerVolumenElectricoAcumulado
        Me.lblobtenervolumenacumuladoE.Text = Me.obtenerVolumenElectricoAcumuladoM3
        Me.lblobtenerNumlecturasE.Text = Me.obtenerNumLecturasE
        Me.lblobtenerTotNumLecturasE.Text = Me.obtenerTotNumLecturas("E", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaE.Text = Me.obtenerIntervaloFechasE()
        Me.lblObtenerNomElementoE.Text = Me.obtenerNomElemento()

        Me.lblPorConsumidoE.Text = ObtenerPorConcesion(lblVolMaxAnuLegE.Text, lblobtenervolumenacumuladoE.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnCancelarEDEnergia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarEDEnergia.Click

        lblFechamedidaESel.Text = ""
        pnlEnergia.Visible = True
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.Visible = True
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
          
                Case "NO"
                    ddlfunciona.SelectedIndex = 2
                Case "SI"
                    ddlfunciona.SelectedIndex = 1
                Case Else
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
                
                Case "NO"
                    ddlFuncionaV.SelectedIndex = 2
                Case "SI"
                    ddlFuncionaV.SelectedIndex = 1
                Case Else
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
      
                Case "NO"
                    ddlFuncionaH.SelectedIndex = 2
                Case "SI"
                    ddlFuncionaH.SelectedIndex = 1
                Case Else
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
        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        Dim nombre As String
        Dim i As Integer
        Dim parametros() As String

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                'mostramos el panel de edición
                Dim cod_fuente_dato As String = parametros(0)
                Dim tipo_obtencion_caudal As String = parametros(2)
                lblFechamedidaQSel.Text = parametros(1)
                'lblFechamedidaQSel.Text = e.CommandArgument
                pnlAcequias.Visible = False
                'IPIM 26/11/2008: Descomentamos para paginar
                ucPaginacionA.Visible = False
                pnlEDAcequias.Visible = True

                creardatasetEd(lblidElemento.Text, codigoPVYCR, lblFechamedidaQSel.Text, cod_fuente_dato, tipo_obtencion_caudal)

                If dstElementos.Tables("TablaEDAcequias").Rows.Count > 0 Then
                    'txtCodigoPVYCR.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdAlimentacion").Rows(0).Item("CodigoPVYCR"))
                    lblFechamedidaQ.Text = utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Fecha_medida"))
                    lblCodFuenteDatoQ.Text = utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Cod_fuente_Dato")) & " - " & utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("DescFuenteDato"))
                    lblidElementoA.Text = utiles.nullACero(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("idElementoMedida"))
                    lblTipoObtencionCaudal.Text = utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("TipoObtencionCaudal"))
                    txtEscalaQ.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Escala_M")))
                    txtCaladoQ.Text = utiles.nullABlanco(String.Format("{0:#,##0.##}", dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Calado_M")))
                    txtCaudalQ.Text = utiles.nullABlanco(String.Format("{0:#,##0.###}", dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Caudal_M3S")))
                    txtDudaCalidad.Text = utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Duda_Calidad"))


                    rellenarListas(lblidElementoA.Text, codigoPVYCR)
                    If dstElementos.Tables("tablaEDAcequias").Rows(0).Item("regimenCurva").ToString <> "" Then
                        If dstElementos.Tables("TablaEDAcequias").Rows(0).Item("RegimenCurva").ToString <> "CT" Then
                            ddlRegimenCurvaQ.Items.FindByText(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("RegimenCurva").ToString).Selected = True
                        End If
                    End If

                    txtobservacionesQ.Text = utiles.nullABlanco(dstElementos.Tables("TablaEDAcequias").Rows(0).Item("Observaciones"))
                    nombre = checkNombreAcequias()
                End If

            Case "borrar"

                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion)
                Dim vcod As String = txtCodFuenteDato.Text

                comando.CommandText = "delete from pvycr_datosacequias where codigoPVYCR='" & codigoPVYCR & "' and idElementoMedida = '" & _
                lblidElemento.Text & "' and fecha_medida='" & parametros(1) & "' and cod_fuente_dato='" & parametros(0) & "' and tipoobtencioncaudal='" & parametros(2) & "' "

                comando.ExecuteNonQuery()
                AgregarCalendario("Q")
                'Me.crearDataSets_Elementos("Q", codigoPVYCR, lblidElemento.Text)
                CrearDatasetDiferencial("Q", codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
                txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
                FinalizarDatasetElementos("Q")
                Session("dst") = dstElementos.Tables("TablaAcequias")
                'para las consultas e inma NCM
                rptAcequias.DataSource = dstElementos.Tables("TablaAcequiasPag")
                rptAcequias.DataBind()
                'ucPaginacion.lblNumpaginasDatabind()

                'IPIM 26/11/2008: Descomentamos para paginar
                ucPaginacionA.lblNumpaginasDatabind()
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
    Protected Function checkNombreDiferencial() As String
        If utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("CodigoPVYCR")) <> "" Then
            lbltituloD.Text = "EDICIÓN DATOS DIFERENCIAL: " & utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("CodigoPVYCR")) & "  " & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("DenominacionPunto")) & " /" & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("idElementoMedida"))
            Return utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("CodigoPVYCR")) & "  " & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("DenominacionPunto")) & " /" & _
            utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("idElementoMedida"))
        Else
            Return "EDICIÓN DATOS DIFERENCIAL"
        End If

    End Function

    Protected Sub btnAceptarEDAcequias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarEDAcequias.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        Dim tipoelem As String = "Q"
    Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
    Dim cod_fuente_dato() As String = lblCodFuenteDatoQ.Text.Split("-")

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)

        comando.CommandText = "UPDATE PVYCR_DatosAcequias " & _
                              "SET [Escala_M] = @Escala_M " & _
                              ",[Calado_M] = @Calado_M " & _
                              ",[RegimenCurva] = @RegimenCurva " & _
                              ",[NumeroParada] = @NumeroParada " & _
                              ",[Caudal_M3S] = @Caudal_M3S " & _
                              ",[Duda_Calidad] = @Duda_Calidad " & _
                              ",[Observaciones] = @Observaciones  " & _
                                  " WHERE idElementoMedida = '" & lblidElementoA.Text & "' and CodigoPVYCR = '" & codigoPVYCR & "' " & _
                                  "and Fecha_Medida = '" & lblFechamedidaQSel.Text & "' and cod_fuente_dato='" & cod_fuente_dato(0).Replace(" ", "").ToString & "' and tipoobtencioncaudal='" & lblTipoObtencionCaudal.Text & "' "


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
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.Visible = True
        pnlEDAcequias.Visible = False

        txtEscalaQ.Text = ""
        txtCaladoQ.Text = ""
        txtNumParadaQ.Text = ""
        txtCaudalQ.Text = ""
        txtDudaCalidad.Text = ""
        txtobservacionesQ.Text = ""

        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElementoA.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElementoA.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
        txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAcequias")              'IPIM 20080821: Para pasarle el dataset entero a los informes

        'IPIM 24/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptAcequias.DataSource = dstElementos.Tables("TablaAcequiasPag")
        rptAcequias.DataBind()
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnCancelarEDAcequias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarEDAcequias.Click
        lblFechamedidaQSel.Text = ""
        pnlAcequias.Visible = True
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.Visible = True
        pnlEDAcequias.Visible = False

        txtLecturaI.Text = ""
        txtEscalaQ.Text = ""
        txtCaladoQ.Text = ""
        txtNumParadaQ.Text = ""
        txtCaudalQ.Text = ""
        txtDudaCalidad.Text = ""
        txtObservaciones.Text = ""
    End Sub

    'Protected Sub imgBuscarCauce_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscarCauce.Click
    '    Dim rutanodobuscadodescripcion As String

    '    'Búsqueda del nodo con el texto coincidente con la clave
    '    rutanodobuscadodescripcion = ""
    '    Dim rutanodobuscado = BuscarNodoTVW(treeView1, txtCauce.Text, rutanodobuscadodescripcion)

    '    If rutanodobuscado = "" Then
    '        Alert(Page, "No Existen nodos con los criterios seleccionados")
    '        SetFocusToControl(Me, Me.txtCauce)
    '        txtCauce.Text = ""

    '    Else
    '        'Representación del Nodo si la busqueda es positiva
    '        Dim nodobuscado As TreeNode = treeView1.FindNode(rutanodobuscado)

    '        treeView1.CollapseAll()
    '        ExpandirNodoRecursivo(nodobuscado)
    '        nodobuscado.Selected = True
    '        nodobuscado.Select()
    '        ActivarPanelDetalles()
    '    End If
    'End Sub

    Protected Sub rptVolumen_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptVolumen.ItemCommand
        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        Dim nombre As String
        Dim i As Integer
        Dim parametros() As String
        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Dim cod_fuente_dato As String = parametros(0)
                lblFechamedidaVSel.Text = parametros(1)
                'mostramos el panel de edición
                'lblFechamedidaVSel.Text = e.CommandArgument
                pnlVolumen.Visible = False
                'IPIM 26/11/2008: Descomentamos para paginar
                ucPaginacionE.Visible = False
                pnlEDVolumen.Visible = True

                creardatasetEd(lblidElemento.Text, codigoPVYCR, lblFechamedidaVSel.Text, cod_fuente_dato)

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
                    txtCaudalMedidoV.Text = utiles.nullABlanco(String.Format("{0:#,###}", dstElementos.Tables("TablaEdMotores").Rows(0).Item("Caudalmedido")))
                    txtObservacionesV.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdMotores").Rows(0).Item("Observaciones"))
                    nombre = checkNombreMotores()
                End If

            Case "borrar"

                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion)

                comando.CommandText = "delete from pvycr_datosmotores where codigoPVYCR='" & codigoPVYCR & "' and idElementoMedida = '" & _
                lblidElemento.Text & "' and fecha_medida='" & parametros(1) & "' and cod_fuente_dato='" & parametros(0) & "' "

                comando.ExecuteNonQuery()
                AgregarCalendario("V")
                'Me.crearDataSets_Elementos("V", codigoPVYCR, lblidElemento.Text)
                CrearDatasetDiferencial("V", codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
                txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
                FinalizarDatasetElementos("V")
                Session("dst") = dstElementos.Tables("Tablamotores")
                'para las consultas e inma NCM
                rptVolumen.DataSource = dstElementos.Tables("TablaMotoresPag")
                rptVolumen.DataBind()
                'ucPaginacion.lblNumpaginasDatabind()

                'IPIM 26/11/2008: Descomentamos para paginar
                ucPaginacionV.lblNumpaginasDatabind()
        End Select

    End Sub

    Protected Sub btnCancelarEDVolumen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarEDVolumen.Click
        lblFechamedidaVSel.Text = ""
        pnlVolumen.Visible = True
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.Visible = True
        pnlEDVolumen.Visible = False

        txtlecturacontador.Text = ""
        txtJustificacionV.Text = ""
        txtConsumoV.Text = ""
        txtReinicioV.Text = ""
        txtCaudalMedidoV.Text = ""
        txtObservacionesV.Text = ""
    End Sub

    Protected Sub btnAceptarEDVolumen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarEDVolumen.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        Dim CODIGOPVYCR = Me.txtCodigoPVYCR.Text
        Dim tipoelem As String = "V"
    Dim cod_fuente_dato() As String = lblCodfuentedatoV.Text.Split("-")
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)

    comando.CommandText = "UPDATE PVYCR_DatosMotores " & _
                           "SET [LecturaContador_M3] = @LecturaContador " & _
                              ",[Funciona] = @FuncionaV " & _
                              ",[Observaciones] = @ObservacionesV " & _
                              ",[Justificacion] = @JustificacionV " & _
                              ",[idIncidenciaVolumetrica] = @idIncidenciaV " & _
                              ",[ConsumoVolumetricoAdicional] = @ConsumoV " & _
                              ",[ReinicioLecturaVolumetrica] = @ReinicioV " & _
                              ",[CaudalMedido] = @CaudalmedidoV " & _
                              " WHERE idElementoMedida = '" & lblidElemento.Text & "' and CodigoPVYCR = '" & CODIGOPVYCR & "' " & _
                              "and Fecha_Medida = '" & lblFechamedidaVSel.Text & "' and cod_fuente_dato='" & cod_fuente_dato(0).Replace(" ", "").ToString & "' "


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
        If utiles.nullABlanco(txtCaudalMedidoV.Text) = "" Then
            comando.Parameters.AddWithValue("CaudalmedidoV", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("CaudalmedidoV", Convert.ToInt32(Replace(txtCaudalMedidoV.Text, ".", "")))
        End If
        comando.ExecuteNonQuery()

        lblFechamedidaVSel.Text = ""
        pnlVolumen.Visible = True
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.Visible = True
        pnlEDVolumen.Visible = False

        txtlecturacontador.Text = ""
        txtJustificacionV.Text = ""
        txtConsumoV.Text = ""
        txtReinicioV.Text = ""
        txtCaudalMedidoV.Text = ""
        txtObservacionesV.Text = ""

        AgregarCalendario(tipoelem)
        CrearDatasetDiferencial(tipoelem, CODIGOPVYCR, lblidElemento.Text, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
        txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
        'crearDataSets_Elementos(tipoelem, CODIGOPVYCR, lblidElemento.Text)

        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaMotores")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptVolumen.DataSource = dstElementos.Tables("TablaMotoresPag")
        rptVolumen.DataBind()
        Me.lblAnyoHidrologicoV.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumuladoV.Text = Me.obtenerVolumenAcumulado
        Me.lblobtenerNumlecturasV.Text = Me.obtenerNumLecturasV
        Me.lblobtenerTotNumLecturasV.Text = Me.obtenerTotNumLecturas("V", CODIGOPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaV.Text = Me.obtenerIntervaloFechasV()
        Me.lblobtenerNomElementoV.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoV.Text = ObtenerPorConcesion(lblVolMaxAnuLegV.Text, lblobtenervolumenacumuladoV.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblNumpaginasDatabind()
    End Sub

    Protected Sub rptHorometros_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptHorometros.ItemCommand
        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim codigoPVYCR = Me.txtCodigoPVYCR.Text

        Dim nombre As String
        Dim i As Integer
        Dim parametros() As String
        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Dim cod_fuente_dato As String = parametros(0)
                lblFechamedidaHSel.Text = parametros(1)
                'mostramos el panel de edición
                'lblFechamedidaHSel.Text = e.CommandArgument
                pnlHorometros.Visible = False
                'IPIM 26/11/2008: Descomentamos para paginar
                ucPaginacionE.Visible = False
                pnlEDHorometros.Visible = True
                pnlEDVolumen.Visible = False
                pnlEDAcequias.Visible = False
                pnlEDEnergia.Visible = False
                pnlEDDiferencial.Visible = False

                creardatasetEd(lblidElemento.Text, codigoPVYCR, lblFechamedidaHSel.Text, cod_fuente_dato)

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

                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion)

                comando.CommandText = "delete from pvycr_datoshorometros where codigoPVYCR='" & codigoPVYCR & "' and idElementoMedida = '" & _
                lblidElemento.Text & "' and fecha_medida='" & parametros(1) & "' and cod_fuente_dato='" & parametros(0) & "' "

                comando.ExecuteNonQuery()
                AgregarCalendario("H")
                'Me.crearDataSets_Elementos("H", codigoPVYCR, lblidElemento.Text)
                CrearDatasetDiferencial("H", codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegH.Text, chkFiltroNulasH.Checked, _
                txtFiltroFechaFinH.Text, txtFiltrofechaIniH.Text, txtFiltrarCodFuenteDatoH.Text, Session("FiltroH"), ucPaginacionH, chkReducirLecH.Checked, dstElementos, sentenciaSel)
                FinalizarDatasetElementos("H")
                Session("dst") = dstElementos.Tables("TablaHorometros")
                'para las consultas e inma NCM
                rptHorometros.DataSource = dstElementos.Tables("TablaHorometrosPag")
                rptHorometros.DataBind()
                'ucPaginacion.lblNumpaginasDatabind()

                'IPIM 26/11/2008: Descomentamos para paginar
                ucPaginacionH.lblNumpaginasDatabind()
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

        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        Dim codigoPVYCR = Me.txtCodigoPVYCR.Text
    Dim tipoelem As String = "H"
    Dim cod_fuente_dato() As String = lblCodfuenteDatoH.Text.Split("-")


        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)

    comando.CommandText = "UPDATE PVYCR_DatosHorometros " & _
                           "SET [HorasIntervalo] = @Horas " & _
                              ",[Funciona] = @Funciona " & _
                              ",[Observaciones] = @Observaciones " & _
                              ",[idIncidenciaVolumetrica] = @idIncidenciaVolumetrica " & _
                              ",[ConsumoVolumetricoAdicional] = @ConsumoH " & _
                              ",[ReinicioLecturaVolumetrica] = @ReinicioH " & _
                              " WHERE idElementoMedida = '" & lblidElemento.Text & "' and CodigoPVYCR = '" & codigoPVYCR & "' " & _
                              "and Fecha_Medida = '" & lblFechamedidaHSel.Text & "' and cod_fuente_dato='" & cod_fuente_dato(0).Replace(" ", "").ToString & "' "


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
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.Visible = True
        pnlEDEnergia.Visible = False
        pnlEDVolumen.Visible = False
        pnlEDAcequias.Visible = False
        pnlEDHorometros.Visible = False
        pnlEDDiferencial.Visible = False


        txtHorasIntervalo.Text = ""
        txtConsumoH.Text = ""
        txtreinicioH.Text = ""
        txtobservacionesH.Text = ""

        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegH.Text, chkFiltroNulasH.Checked, _
        txtFiltroFechaFinH.Text, txtFiltrofechaIniH.Text, txtFiltrarCodFuenteDatoH.Text, Session("FiltroH"), ucPaginacionH, chkReducirLecH.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaHorometros")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptHorometros.DataSource = dstElementos.Tables("TablaHorometrosPag")
        rptHorometros.DataBind()
        Me.lblAnyoHidrologicoH.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenerNumLecturasH.Text = Me.obtenerNumLecturasH
        Me.lblobtenerVolumenAcumuladoHoras.Text = Me.obtenerVolumenAcumuladoHoras()
        Me.lblobtenervolumenacumuladoH.Text = Me.obtenerVolumenHorometroAcumulado()
        Me.lblobtenerTotNumLecturasH.Text = Me.obtenerTotNumLecturas("H", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaH.Text = Me.obtenerIntervaloFechasH()
        Me.lblObtenerNomElementoH.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoH.Text = ObtenerPorConcesion(lblVolMaxAnuLegH.Text, lblobtenervolumenacumuladoH.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionH.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnCancelarEDHorometro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarEDHorometro.Click
        lblFechamedidaHSel.Text = ""
        lblCodfuenteDatoH.Text = ""
        pnlHorometros.Visible = True
        pnlEDEnergia.Visible = False
        pnlEDVolumen.Visible = False
        pnlEDAcequias.Visible = False
        pnlEDHorometros.Visible = False
        pnlEDDiferencial.Visible = False


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
        'JavaScript.controlFormatea(Page, txtCaudalMedidoV)
        'JavaScript.controlDesFormatea(Page, txtCaudalMedidoV)
        'DATOS ACEQUIAS
        JavaScript.controlFormatea(Page, txtEscalaQ)
        JavaScript.controlDesFormatea(Page, txtEscalaQ)
        JavaScript.controlFormatea(Page, txtCaladoQ)
        JavaScript.controlDesFormatea(Page, txtCaladoQ)
        JavaScript.controlFormatea(Page, txtCaudalQ, 3)
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
        'DATOS SUMINISTROS
        JavaScript.controlFormatea(Page, txtEDSuministroD)
        JavaScript.controlDesFormatea(Page, txtEDSuministroD)
    End Sub
    'ncm 01/03/2010 lo hemos pasado a la clase sica_funcionescalcvoldiferencial
    'Protected Sub obtenerVolumenDiferencial(ByVal tipo As String)
    '    'vamos a calcular la diferencia de volúmenes según registros
    '    'el caso de las acequias es un caso especial y se calcula obteniendo la media del caudal por el tiempo trancurrido entre dos lecturas.
    '    Dim i As Integer
    '    Dim v_lectura_ant_horas, v_diferencial_horas, v_vol_horas, v_vol_ant_horas, v_diferencial_acum_horas, v_vol, v_vol_ant, v_diferencial, v_diferencial_kwh, v_kwh, v_caudal, v_caudal_ant, v_caudal_medio, v_diferencial_acum, v_acum_kwh, v_lectura_ant, v_diferencial_seg, v_diferencial_m3, v_diferencial_acumm3 As Decimal
    '    Dim v_tiempo, v_tiempo_ant As Date
    '    Dim v_tiempo_medio As Integer
    '    Dim v_segundos As Integer
    '    Dim primeroNulo As String = "N"
    '    If tipo = "V" Then
    '        If dstElementos.Tables("TablaMotores").Rows.Count > 0 Then
    '            Dim filas As Integer = dstElementos.Tables("TablaMotores").Rows.Count
    '            If Not dstElementos.Tables("TablaMotores").Columns.Contains("Diferencial") Then
    '                'añadimos la columna diferencial al dataset
    '                dstElementos.Tables("TablaMotores").Columns.Add("Diferencial")
    '                dstElementos.Tables("TablaMotores").Columns.Add("Diferencial_Acum")
    '                dstElementos.Tables("TablaMotores").Columns.Add("Diferencial_seg")
    '            End If
    '            'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
    '            For i = 0 To filas - 1

    '                If i = 0 Then
    '                    'rellenamos la columna diferencial del dataset con un 0
    '                    dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial") = "0"
    '                    dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial_Acum") = "0"
    '                    dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial_Seg") = "0"
    '                    'ncm 18/02/2010 si la primera lectura es nula, marcaremos un parámetro para tenerlo en cuenta en las posteriores.
    '                    If dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3").ToString = Nothing Then
    '                        primeroNulo = "S"
    '                    End If
    '                    v_vol_ant = Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3").ToString)
    '                    v_lectura_ant = v_vol_ant
    '                Else
    '                    'comprobamos si existen incidencias
    '                    '*************************--> obsoleto(19/05/2008)***********************-
    '                    'si la incidencia es reseteo (6) o cambio de contador(7) se deberá tomar el valor del campo Reinicio Lectura Volumetrica 
    '                    '************************* NUEVO ****************************************
    '                    'si la incidencia es reseteo o cambio de contador  la fórmula es:
    '                    '                   ((lecturam3(dia15)-reseteo(dia15) + consumo adic. (dia15)
    '                    'si la incidencia es contador averiado yconsumo negativo (8) (5), la fñormula es :
    '                    '                   (lecturaM3(i) + Consumovolumetricoadicional(i)) - lecturam3(i-1)

    '                    'comprobamos si el primer registro es cero, el vol_antel diferencial es cero

    '                    'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta

    '                    If (utiles.nullABlanco(dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3").ToString) = "") Then
    '                        v_diferencial = 0
    '                        v_segundos = 0
    '                        v_diferencial_acum = v_diferencial_acum
    '                        'añadimos los valores a la tabla
    '                        dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", DBNull.Value)
    '                        dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial_seg") = String.Format("{0:#,##0}", DBNull.Value)
    '                        dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0}", v_diferencial_acum)
    '                    Else
    '                        v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3").ToString)
    '                        ' este if es por si la primera lectura es nula
    '                        If (primeroNulo = "S") Then
    '                            primeroNulo = "N"
    '                            v_diferencial = 0
    '                            v_segundos = 0
    '                        Else
    '                            'calculamos los segundos que hay entre las lecturas para poder mostrr el parcial en m3/s
    '                            v_segundos = DateDiff(DateInterval.Second, dstElementos.Tables("TablaMotores").Rows(i - 1).Item("Fecha_medida"), dstElementos.Tables("TablaMotores").Rows(i).Item("Fecha_medida"))

    '                            If (dstElementos.Tables("TablaMotores").Rows(i).Item("idincidenciaVolumetrica").ToString = "6") Or _
    '                                (dstElementos.Tables("TablaMotores").Rows(i).Item("idincidenciaVolumetrica").ToString = "7") Then
    '                                v_diferencial = ((Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3").ToString) - Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("ReiniciolecturaVolumetrica").ToString)) + _
    '                                Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("ConsumoVolumetricoAdicional").ToString))
    '                            ElseIf _
    '                               (dstElementos.Tables("TablaMotores").Rows(i).Item("idincidenciaVolumetrica").ToString = "5") Or _
    '                               (dstElementos.Tables("TablaMotores").Rows(i).Item("idincidenciaVolumetrica").ToString = "8") Then
    '                                If utiles.nullABlanco(dstElementos.Tables("TablaMotores").Rows(i).Item("ConsumoVolumetricoAdicional").ToString) <> "" Then
    '                                    v_diferencial = (Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3").ToString) + dstElementos.Tables("TablaMotores").Rows(i).Item("ConsumoVolumetricoAdicional").ToString) - _
    '                                    Convert.ToDecimal(0 & v_lectura_ant)
    '                                Else
    '                                    v_diferencial = (Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3").ToString) + Convert.ToDecimal(0 & dstElementos.Tables("TablaMotores").Rows(i).Item("ConsumoVolumetricoAdicional").ToString)) - _
    '                                    Convert.ToDecimal(0 & v_lectura_ant)

    '                                End If

    '                            Else

    '                                v_diferencial = v_vol - v_vol_ant
    '                            End If

    '                        End If ' lectura inicial nula
    '                        'si los segundos es cero no sacamos nada en el campo
    '                        If v_segundos = 0 Then
    '                            v_diferencial_seg = 0
    '                        Else
    '                            v_diferencial_seg = v_diferencial / v_segundos
    '                        End If
    '                        v_vol_ant = v_vol
    '                        'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
    '                        v_lectura_ant = dstElementos.Tables("TablaMotores").Rows(i).Item("LecturaContador_M3").ToString
    '                        'calculamos el diferencial acumulado
    '                        v_diferencial_acum = v_diferencial_acum + v_diferencial
    '                        'añadimos los valores a la tabla
    '                        dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", v_diferencial)
    '                        dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial_seg") = String.Format("{0:#,##0.###}", v_diferencial_seg)
    '                        dstElementos.Tables("TablaMotores").Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0}", v_diferencial_acum)
    '                    End If
    '                End If

    '            Next
    '        End If
    '    ElseIf tipo = "E" Then
    '        Dim mensaje_idcontador As String = ""
    '        Dim mensaje_relacion_m3 As String = ""
    '        If dstElementos.Tables("TablaAlimentacion").Rows.Count > 0 Then
    '            'cada registro que tenga el id contador nulo, lo guardaremos para mostrar un mensaje al usuario
    '            If utiles.nullABlanco(dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idContador").ToString) = "" Then
    '                mensaje_idcontador += "La lectura con fecha " & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Fecha_medida").ToString & " no tiene contador relacionado \n"
    '            End If
    '            'si el factor corrector es nulo tb se mostrará sms
    '            If utiles.nullABlanco(dstElementos.Tables("TablaAlimentacion").Rows(i).Item("relacionM3_kwh").ToString) = "" Or _
    '            dstElementos.Tables("TablaAlimentacion").Rows(i).Item("relacionM3_kwh").ToString = "0" Then
    '                mensaje_relacion_m3 += "El factor corrector no tiene valor para la lectura " & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Fecha_medida").ToString & " \n"
    '            End If

    '            If Not dstElementos.Tables("TablaAlimentacion").Columns.Contains("Diferencial") Then
    '                'añadimos la columna diferencial al dataset
    '                dstElementos.Tables("TablaAlimentacion").Columns.Add("Diferencial")
    '                dstElementos.Tables("TablaAlimentacion").Columns.Add("Diferencial_Seg")
    '                dstElementos.Tables("TablaAlimentacion").Columns.Add("Diferencial_acum")
    '                dstElementos.Tables("TablaAlimentacion").Columns.Add("Diferencial_acumM3")
    '            End If

    '            'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
    '            For i = 0 To dstElementos.Tables("TablaAlimentacion").Rows.Count - 1

    '                If i = 0 Then
    '                    'rellenamos la columna diferencial del dataset con un 0
    '                    dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial") = "0"
    '                    dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_Seg") = "0"
    '                    dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_acum") = "0" 'dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString '"0"
    '                    dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_acumM3") = "0"
    '                    v_vol_ant = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString) '* dstElementos.Tables("TablaAlimentacion").Rows(i).Item("relacionM3_kwh").ToString
    '                    v_lectura_ant = v_vol_ant
    '                    'v_acum_kwh = v_acum_kwh + v_lectura_ant
    '                    If dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString = Nothing Then
    '                        primeroNulo = "S"
    '                    End If
    '                Else

    '                    'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
    '                    If utiles.nullABlanco(dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString) = "" Then
    '                        v_diferencial = 0
    '                        v_segundos = 0
    '                        v_diferencial_acum = v_diferencial_acum
    '                        '03/08/2008 ncm calculamos el volumen diferencial en m3
    '                        v_diferencial_acumm3 = v_diferencial_acum '* dstElementos.Tables("TablaAlimentacion").Rows(i).Item("relacionM3_kwh").ToString
    '                        v_lectura_ant = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString)
    '                        'añadimos los valores a la tabla
    '                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", DBNull.Value)
    '                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_Seg") = String.Format("{0:#,##0.###}", DBNull.Value)
    '                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0}", v_diferencial_acum)
    '                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_acumM3") = String.Format("{0:#,##0}", v_diferencial_acumm3)

    '                    Else
    '                        If (primeroNulo = "S") Then
    '                            primeroNulo = "N"
    '                            v_diferencial = 0
    '                            v_diferencial_acum = 0
    '                            v_diferencial_m3 = 0
    '                            v_segundos = 0
    '                            v_kwh = 0
    '                        Else
    '                            v_segundos = DateDiff(DateInterval.Second, dstElementos.Tables("TablaAlimentacion").Rows(i - 1).Item("Fecha_medida"), dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Fecha_medida"))
    '                            v_vol = Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString)
    '                            v_lectura_ant = v_vol

    '                            'comprobamos si existen incidencias
    '                            'si la incidencia es reseteo o cambio de contador se deberá tomar el valor del campo Reinicio Lectura Volumetrica
    '                            'si la incidencia es contador averiado o consumo negativo, el valor a tomar será el consumo volumétrico adicional
    '                            If ((dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaElectrica").ToString = "2") Or _
    '                                (dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaElectrica").ToString = "3")) Then
    '                                If dstElementos.Tables("TablaAlimentacion").Rows(i).Item("COD_FUENTE_DATO").ToString = "05" Then
    '                                    v_diferencial = (((Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString) - Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("ReiniciolecturaElectrica").ToString)) + _
    '                                    Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("ConsumoElectricoAdicional").ToString))) * dstElementos.Tables("TablaAlimentacion").Rows(i).Item("relacionM3_kwh").ToString
    '                                    v_kwh = v_diferencial

    '                                End If
    '                            ElseIf (dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaElectrica").ToString = "1") Or _
    '                                (dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaElectrica").ToString = "4") Then
    '                                If utiles.nullABlanco(dstElementos.Tables("TablaAlimentacion").Rows(i).Item("ConsumoElectricoAdicional").ToString) <> "" Then
    '                                    v_diferencial = ((Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString) + dstElementos.Tables("TablaAlimentacion").Rows(i).Item("ConsumoElectricoAdicional").ToString) - _
    '                                    Convert.ToDecimal(0 & v_lectura_ant)) * dstElementos.Tables("TablaAlimentacion").Rows(i).Item("relacionM3_kwh").ToString
    '                                    v_kwh = v_diferencial
    '                                Else
    '                                    v_diferencial = ((Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString) + Convert.ToDecimal(0 & dstElementos.Tables("TablaAlimentacion").Rows(i).Item("ConsumoElectricoAdicional").ToString)) - _
    '                                    Convert.ToDecimal(0 & v_lectura_ant)) * dstElementos.Tables("TablaAlimentacion").Rows(i).Item("relacionM3_kwh").ToString
    '                                    v_kwh = v_diferencial
    '                                End If
    '                            Else
    '                                v_diferencial = (v_vol - v_vol_ant) * dstElementos.Tables("TablaAlimentacion").Rows(i).Item("relacionM3_kwh").ToString
    '                                v_kwh = (v_vol - v_vol_ant)
    '                            End If
    '                        End If ' primera lectura nula

    '                        v_vol_ant = v_vol
    '                        'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
    '                        'v_lectura_ant = dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Total_Kwh").ToString
    '                        'calculamos el diferencial acumulado
    '                        v_acum_kwh = v_acum_kwh + v_kwh
    '                        v_diferencial_acum = v_diferencial_acum + v_diferencial

    '                        '03/08/2008 ncm calculamos el volumen diferencial en m3
    '                        v_diferencial_m3 = v_diferencial '* dstElementos.Tables("TablaAlimentacion").Rows(i).Item("relacionM3_kwh").ToString
    '                        'si los segundos es cero no sacamos nada en el campo
    '                        If v_segundos = 0 Then
    '                            v_diferencial_seg = 0
    '                        Else
    '                            v_diferencial_seg = v_diferencial_m3 / v_segundos
    '                        End If
    '                        v_diferencial_acumm3 = v_diferencial_acum '* dstElementos.Tables("TablaAlimentacion").Rows(i).Item("relacionM3_kwh").ToString
    '                        'cargamos datos en la tabla, comentado porque éste es el diferencial en KWH y vamos a mostrarlo en m3
    '                        'dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
    '                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", v_diferencial_m3)
    '                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_Seg") = String.Format("{0:#,##0.###}", v_diferencial_seg)
    '                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0}", v_acum_kwh)
    '                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("Diferencial_acumm3") = String.Format("{0:#,##0}", v_diferencial_acumm3)
    '                    End If 'total_kw nulo
    '                End If 'i = 0
    '            Next
    '            'si las variables de mensaje tienen valor las mostramos
    '            Dim mensaje_final As String = ""
    '            If mensaje_idcontador <> "" Then
    '                mensaje_final += mensaje_idcontador
    '                'Alert(Page, mensaje_idcontador)
    '            End If
    '            If mensaje_relacion_m3 <> "" Then
    '                mensaje_final += " \n" & mensaje_relacion_m3
    '                'Alert(Page, mensaje_relacion_m3)
    '            End If
    '            If mensaje_final <> "" Then
    '                Alert(Page, mensaje_final)
    '            End If
    '        End If
    '    ElseIf tipo = "Q" Then
    '        'deberemos calcular el volumen, siendo éste el caudal por el tiempo
    '        If dstElementos.Tables("TablaAcequias").Rows.Count > 0 Then
    '            If Not dstElementos.Tables("TablaAcequias").Columns.Contains("Diferencial") Then
    '                'añadimos la columna diferencial al dataset
    '                dstElementos.Tables("TablaAcequias").Columns.Add("Diferencial")
    '                dstElementos.Tables("TablaAcequias").Columns.Add("Diferencial_acum")
    '            End If
    '            'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
    '            For i = 0 To dstElementos.Tables("TablaAcequias").Rows.Count - 1

    '                If i = 0 Then
    '                    'rellenamos la columna diferencial del dataset con un 0
    '                    dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial") = "0"
    '                    dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial_acum") = "0"
    '                Else
    '                    v_caudal = Convert.ToDecimal(0 & dstElementos.Tables("TablaAcequias").Rows(i).Item("Caudal_M3S").ToString)
    '                    v_caudal_ant = Convert.ToDecimal(0 & dstElementos.Tables("TablaAcequias").Rows(i - 1).Item("Caudal_M3S").ToString)
    '                    v_caudal_medio = (v_caudal + v_caudal_ant) / 2
    '                    'el tiempo deberá estar en segundos
    '                    v_tiempo = dstElementos.Tables("TablaAcequias").Rows(i).Item("Fecha_medida").ToString
    '                    v_tiempo_ant = dstElementos.Tables("TablaAcequias").Rows(i - 1).Item("Fecha_medida").ToString
    '                    v_tiempo_medio = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60))
    '                    'lompartimos entre 1000 para pasarlo de a (m3)
    '                    v_diferencial = (v_caudal_medio * v_tiempo_medio)

    '                    'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
    '                    If utiles.nullABlanco(dstElementos.Tables("TablaAcequias").Rows(i).Item("Caudal_M3S").ToString) = "" Then
    '                        v_diferencial_acum = v_diferencial_acum
    '                        dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", DBNull.Value)
    '                        dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0}", DBNull.Value)
    '                    Else
    '                        'calculamos el diferencial acumulado
    '                        v_diferencial_acum = v_diferencial_acum + v_diferencial
    '                        dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", v_diferencial)
    '                        dstElementos.Tables("TablaAcequias").Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0}", v_diferencial_acum)
    '                    End If
    '                End If
    '            Next
    '        End If
    '    ElseIf tipo = "H" Then

    '        If dstElementos.Tables("TablaHorometros").Rows.Count > 0 Then
    '            Dim mensaje_codMotobomba As String = ""
    '            Dim mensaje_caudal_m3 As String = ""
    '            'cada registro que tenga el id contador nulo, lo guardaremos para mostrar un mensaje al usuario
    '            If utiles.nullABlanco(dstElementos.Tables("TablaHorometros").Rows(i).Item("codigomotobomba").ToString) = "" Then
    '                mensaje_codMotobomba += "La lectura con fecha " & dstElementos.Tables("TablaHorometros").Rows(i).Item("Fecha_medida").ToString & " no tiene motobomba relacionada \n"
    '            End If
    '            'si el factor corrector es nulo tb se mostrará sms
    '            If utiles.nullABlanco(dstElementos.Tables("TablaHorometros").Rows(i).Item("caudal_lseg").ToString.ToString) = "" Then
    '                mensaje_caudal_m3 += "El caudal no tiene valor para la lectura " & dstElementos.Tables("TablaHorometros").Rows(i).Item("Fecha_medida").ToString & " \n"
    '            End If
    '            If Not dstElementos.Tables("TablaHorometros").Columns.Contains("Diferencial") Then
    '                'añadimos la columna diferencial al dataset
    '                dstElementos.Tables("TablaHorometros").Columns.Add("Diferencial")
    '                dstElementos.Tables("TablaHorometros").Columns.Add("Diferencial_Seg")
    '                dstElementos.Tables("TablaHorometros").Columns.Add("Diferencial_Acum")
    '                dstElementos.Tables("TablaHorometros").Columns.Add("Diferencial_Acum_horas")
    '            End If
    '            'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
    '            For i = 0 To dstElementos.Tables("TablaHorometros").Rows.Count - 1

    '                If i = 0 Then
    '                    'rellenamos la columna diferencial del dataset con un 0
    '                    dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial") = "0"
    '                    dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial_Seg") = "0"
    '                    dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial_Acum") = "0"
    '                    dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial_Acum_horas") = "0"
    '                    v_vol_ant = (Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("HorasIntervalo").ToString) * 3600) * Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("Caudal_LSeg").ToString) / 1000
    '                    v_vol_ant_horas = Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("HorasIntervalo").ToString)
    '                    v_lectura_ant = v_vol_ant
    '                    v_segundos = 0
    '                    If dstElementos.Tables("TablaHorometros").Rows(i).Item("HorasIntervalo").ToString = Nothing Then
    '                        primeroNulo = "S"
    '                    End If
    '                Else
    '                    If (primeroNulo = "S") Then
    '                        primeroNulo = "N"
    '                        v_diferencial = 0
    '                        v_segundos = 0
    '                    Else
    '                        'comprobamos si existen incidencias
    '                        '*************************--> obsoleto(19/05/2008)***********************-
    '                        'si la incidencia es reseteo (6) o cambio de contador(7) se deberá tomar el valor del campo Reinicio Lectura Volumetrica 
    '                        '************************* NUEVO ****************************************
    '                        'si la incidencia es reseteo o cambio de contador  la fórmula es:
    '                        '                   ((lecturam3(dia15)-reseteo(dia15) + consumo adic. (dia15)
    '                        'si la incidencia es contador averiado yconsumo negativo (8) (5), la fñormula es :
    '                        '                   (lecturaM3(i) + Consumovolumetricoadicional(i)) - lecturam3(i-1)

    '                        'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
    '                        If utiles.nullABlanco(dstElementos.Tables("TablaHorometros").Rows(i).Item("HorasIntervalo").ToString) = "" Then
    '                            v_diferencial = 0
    '                            v_diferencial_horas = 0
    '                            v_diferencial_acum = v_diferencial_acum
    '                            v_diferencial_acum_horas = v_diferencial_acum_horas
    '                            'añadimos los valores a la tabla
    '                            dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", DBNull.Value)
    '                            dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
    '                            dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial_Acum_horas") = String.Format("{0:#,##0.##}", v_diferencial_acum_horas)
    '                        Else

    '                            v_vol = (Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("HorasIntervalo").ToString) * 3600) * Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("Caudal_LSeg").ToString) / 1000
    '                            v_vol_horas = Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("HorasIntervalo").ToString)
    '                            v_segundos = DateDiff(DateInterval.Second, dstElementos.Tables("TablaHorometros").Rows(i - 1).Item("Fecha_medida"), dstElementos.Tables("TablaHorometros").Rows(i).Item("Fecha_medida"))
    '                            If (dstElementos.Tables("TablaHorometros").Rows(i).Item("idincidenciaVolumetrica").ToString = "10") Or _
    '                                (dstElementos.Tables("TablaHorometros").Rows(i).Item("idincidenciaVolumetrica").ToString = "11") Then
    '                                v_diferencial = ((v_vol - Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("ReiniciolecturaVolumetrica").ToString)) + _
    '                                Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("ConsumoVolumetricoAdicional").ToString))

    '                                v_diferencial_horas = ((v_vol_horas - Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("ReiniciolecturaVolumetrica").ToString)) + _
    '                                Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("ConsumoVolumetricoAdicional").ToString))
    '                            ElseIf _
    '                               (dstElementos.Tables("TablaHorometros").Rows(i).Item("idincidenciaVolumetrica").ToString = "9") Or _
    '                               (dstElementos.Tables("TablaHorometros").Rows(i).Item("idincidenciaVolumetrica").ToString = "12") Then
    '                                If utiles.nullABlanco(dstElementos.Tables("TablaHorometros").Rows(i).Item("ConsumoVolumetricoAdicional").ToString) <> "" Then
    '                                    v_diferencial = (v_vol + dstElementos.Tables("TablaHorometros").Rows(i).Item("ConsumoVolumetricoAdicional").ToString) - _
    '                                    Convert.ToDecimal(0 & v_lectura_ant)
    '                                    v_diferencial_horas = (v_vol_horas + dstElementos.Tables("TablaHorometros").Rows(i).Item("ConsumoVolumetricoAdicional").ToString) - _
    '                                    Convert.ToDecimal(0 & v_lectura_ant_horas)
    '                                Else
    '                                    v_diferencial = (v_vol + Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("ConsumoVolumetricoAdicional").ToString)) - _
    '                                    Convert.ToDecimal(0 & v_lectura_ant)
    '                                    v_diferencial_horas = (v_vol_horas + Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("ConsumoVolumetricoAdicional").ToString)) - _
    '                                                                        Convert.ToDecimal(0 & v_lectura_ant_horas)
    '                                End If

    '                            Else
    '                                v_diferencial = v_vol - v_vol_ant
    '                                v_diferencial_horas = v_vol_horas - v_vol_ant_horas
    '                            End If
    '                        End If 'primera lectura nula
    '                        v_vol_ant = v_vol
    '                        v_vol_ant_horas = v_vol_horas
    '                        'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
    '                        v_lectura_ant = (Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("HorasIntervalo").ToString) * 3600) * Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("Caudal_LSeg").ToString) / 1000
    '                        v_lectura_ant_horas = Convert.ToDecimal(0 & dstElementos.Tables("TablaHorometros").Rows(i).Item("HorasIntervalo").ToString)
    '                        'calculamos el diferencial acumulado
    '                        v_diferencial_acum = v_diferencial_acum + v_diferencial
    '                        v_diferencial_acum_horas = v_diferencial_acum_horas + v_diferencial_horas
    '                        'pasamos el diferencial a m3 03/08/2008 ncm, para ello pasamos primero el diferencial a segundos porque lo tenemos en horas
    '                        'v_diferencial_seg = v_diferencial * 3600
    '                        'v_diferencial_m3 = v_diferencial_seg * dstElementos.Tables("TablaHorometros").Rows(i).Item("HorasIntervalo")
    '                        'añadimos los valores a la tabla
    '                        'dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)

    '                        'si los segundos es cero no sacamos nada en el campo
    '                        If v_segundos = 0 Then
    '                            v_diferencial_seg = 0
    '                        Else
    '                            v_diferencial_seg = v_diferencial / v_segundos
    '                        End If

    '                        dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
    '                        dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial_Seg") = String.Format("{0:#,##0.###}", v_diferencial_seg)
    '                        dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
    '                        dstElementos.Tables("TablaHorometros").Rows(i).Item("Diferencial_Acum_horas") = String.Format("{0:#,##0.##}", v_diferencial_acum_horas)
    '                    End If

    '                End If
    '            Next
    '            'si las variables de mensaje tienen valor las mostramos
    '            Dim mensaje_final As String = ""
    '            If mensaje_codMotobomba <> "" Then
    '                mensaje_final += mensaje_codMotobomba
    '                'Alert(Page, mensaje_idcontador)
    '            End If
    '            If mensaje_caudal_m3 <> "" Then
    '                mensaje_final += " \n" & mensaje_caudal_m3
    '                'Alert(Page, mensaje_relacion_m3)
    '            End If
    '            If mensaje_final <> "" Then
    '                Alert(Page, mensaje_final)
    '            End If
    '        End If
    '    ElseIf tipo = "D" Then
    '        'ncm en este caso el diferencial = acumulado, por tanto el calculo será ir sumando y no restando
    '        If dstElementos.Tables("TablaDiferencial").Rows.Count > 0 Then
    '            If Not dstElementos.Tables("TablaDiferencial").Columns.Contains("Diferencial") Then
    '                'añadimos la columna diferencial al dataset
    '                dstElementos.Tables("TablaDiferencial").Columns.Add("Diferencial")
    '                dstElementos.Tables("TablaDiferencial").Columns.Add("Diferencial_Acum")
    '            End If
    '            'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
    '            For i = 0 To dstElementos.Tables("TablaDiferencial").Rows.Count - 1

    '                'If i = 0 Then
    '                '    'rellenamos la columna diferencial del dataset con un 0
    '                '    dstElementos.Tables("TablaDiferencial").Rows(i).Item("Diferencial") = "0"
    '                '    dstElementos.Tables("TablaDiferencial").Rows(i).Item("Diferencial_Acum") = "0"
    '                '    v_vol_ant = (Convert.ToDecimal(0 & dstElementos.Tables("TablaDiferencial").Rows(i).Item("SuministroMensualM3").ToString))
    '                '    v_lectura_ant = v_vol_ant
    '                'Else
    '                'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
    '                If utiles.nullABlanco(dstElementos.Tables("TablaDiferencial").Rows(i).Item("SuministroMensualM3").ToString) = "" Then
    '                    v_diferencial = 0
    '                    v_diferencial_acum = v_diferencial_acum
    '                    'añadimos los valores a la tabla
    '                    dstElementos.Tables("TablaDiferencial").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", DBNull.Value)
    '                    dstElementos.Tables("TablaDiferencial").Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
    '                Else

    '                    v_vol = (Convert.ToDecimal(0 & dstElementos.Tables("TablaDiferencial").Rows(i).Item("SuministroMensualM3").ToString))
    '                    v_diferencial = v_vol + v_vol_ant
    '                End If
    '                v_vol_ant = v_diferencial 'v_vol
    '                'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
    '                v_lectura_ant = (Convert.ToDecimal(0 & dstElementos.Tables("TablaDiferencial").Rows(i).Item("SuministroMensualM3").ToString))
    '                'calculamos el diferencial acumulado
    '                v_diferencial_acum = v_diferencial_acum + v_diferencial
    '                dstElementos.Tables("TablaDiferencial").Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
    '                dstElementos.Tables("TablaDiferencial").Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
    '                'End If
    '            Next
    '        End If

    '    End If

    'End Sub

    Protected Sub chkFiltroNulasE_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFiltroNulasE.CheckedChanged
        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        Dim codigoPVYCR = Me.txtCodigoPVYCR.Text
        Dim tipoelem As String = "E"

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblPaginatext = "1"
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
        txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAlimentacion")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacionPag")
        rptEnergia.DataBind()
        Me.lblAnyoHidrologicoE.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumulado.Text = Me.obtenerVolumenElectricoAcumulado
        Me.lblobtenervolumenacumuladoE.Text = Me.obtenerVolumenElectricoAcumuladoM3
        Me.lblobtenerNumlecturasE.Text = Me.obtenerNumLecturasE
        Me.lblobtenerTotNumLecturasE.Text = Me.obtenerTotNumLecturas("E", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaE.Text = Me.obtenerIntervaloFechasE()
        Me.lblObtenerNomElementoE.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoE.Text = ObtenerPorConcesion(lblVolMaxAnuLegE.Text, lblobtenervolumenacumuladoE.Text)

        ucPaginacionE.lblNumpaginasDatabind()

    End Sub

    Protected Sub chkFiltroNulasQ_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFiltroNulasQ.CheckedChanged
        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        Dim codigoPVYCR = Me.txtCodigoPVYCR.Text
        Dim tipoelem As String = "Q"

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblPaginatext = "1"
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
        txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAcequias")              'IPIM 20080821: Para pasarle el dataset entero a los informes

        'IPIM 24/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptAcequias.DataSource = dstElementos.Tables("TablaAcequiasPag")
        rptAcequias.DataBind()
        Me.lblobtenerNumLecturasQ.Text = Me.obtenerNumLecturasQ()
        Me.lblobtenerTotNumLecturasQ.Text = Me.obtenerTotNumLecturas("Q", codigoPVYCR, lblidElemento.Text)
        Me.lblCaudalAcumuladoQ.Text = Me.obtenerCaudalAcumulado
        Me.lblObtenerAnyoHidrologicoQ.Text = Me.obtenerAnyoHidrologico()
        Me.lblIntervaloFechasQ.Text = Me.obtenerIntervaloFechasQ()
        Me.lblObtenerNomElementoA.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoQ.Text = ObtenerPorConcesion(lblVolMaxAnuLegQ.Text, lblCaudalAcumuladoQ.Text)
        ucPaginacionA.lblNumpaginasDatabind()

    End Sub

    Protected Sub chkFiltroNulasV_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFiltroNulasV.CheckedChanged
        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        Dim codigoPVYCR = Me.txtCodigoPVYCR.Text
        Dim tipoelem As String = "V"

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionV.lblPaginatext = "1"
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
        txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaMotores")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptVolumen.DataSource = dstElementos.Tables("TablaMotoresPag")
        rptVolumen.DataBind()
        Me.lblAnyoHidrologicoV.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumuladoV.Text = Me.obtenerVolumenAcumulado
        Me.lblobtenerNumlecturasV.Text = Me.obtenerNumLecturasV
        Me.lblobtenerTotNumLecturasV.Text = Me.obtenerTotNumLecturas("V", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaV.Text = Me.obtenerIntervaloFechasV()
        Me.lblobtenerNomElementoV.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoV.Text = ObtenerPorConcesion(lblVolMaxAnuLegV.Text, lblobtenervolumenacumuladoV.Text)
        ucPaginacionV.lblNumpaginasDatabind()

    End Sub

    Protected Sub imgGrafica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgGrafica.Click
        'EGB13052008 
        Dim ClaveNodo As String
        Try
            ClaveNodo = nullAFalse(Request.QueryString("nodoclave").ToString)
            If ClaveNodo = "" Then
                Alert(Page, "Seleccione un elemento de medida para mostrar el gráfico")
            Else
                Response.Redirect("Grafica.aspx?nodoclave=" & Request.QueryString("nodoclave").ToString)
            End If
        Catch Excepcion As Exception
            Alert(Page, "Seleccione un elemento de medida para mostrar el gráfico")
        End Try
    End Sub

    Protected Sub imgVisor_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgVisor.Click
        'NCM22052008
        Dim ClaveNodo, ClaveNodoTipo, claveTipo As String

        'Try
        'en claveNodoTipo tenemos la clave del nodo y el tipo (C de cauce, P de punto...)
        ClaveNodoTipo = Request.QueryString("nodoclave").ToString
        'Obtenemos la clave del nodo y el tipo
        ClaveNodo = ClaveNodoTipo.Substring(0, ClaveNodoTipo.IndexOf(";"))
        claveTipo = ClaveNodoTipo.Substring(ClaveNodoTipo.Length - 1, 1)

        If ClaveNodo = "" Or ClaveNodo = "[CodigoPVYCR]" Then
            Alert(Page, "Seleccione un elemento de medida para ir al visor")
        Else

            lblPestanyasArbolQ.Text = genHTML.EnlacesMenuArbol(5, "../", Page, 11, Session("claveTipo"), ClaveNodo, "S")
            Dim x, y, NumReg As Integer
            Dim radio As Decimal
            'ncm 03/11/2009 esta variable se utiliza para que no se recargue el zoom si el usuario cambia algunos de los filtros
            Session("zoom") = "N"
            'si el tipo es cauce obtendremos las X/Y de sus puntos para conseguir las medias y obtener la envolvente
            'si el tipo es punto o elemento de medida, obtendremos sus X/Y de la tabla puntos  y haremos la llamada al visor.
            If claveTipo = "C" Then

                Obtenerenvolvente(ClaveNodo, x, y, radio, NumReg)
                If NumReg <> 0 Then
                    Page.RegisterClientScriptBlock("mostrarFrameVisor", "<script language=javascript>" & _
                                                       "if (" & NumReg & " ==0)alert('No existen puntos para ese cauce');cambioIFrame(" & x & ", " & y & "," & radio & ", " & NumReg & ", 'N')" & _
                                               "</script>")
                Else
                    Page.RegisterClientScriptBlock("mostrarFrameVisor", "<script language=javascript>" & _
                                                                 "cambioIFrame(0,0,40,0,'S')" & _
                                                         "</script>")

                End If
            Else
                Dim TipoPunto As String
                TipoPunto = ""
                'obtenemos los datos de las corrdenadas X Y, zone, huso, etc que deberemos pasar en la llamada al visor para centrarlo
                crearDataSetsPuntos(ClaveNodo, TipoPunto)

                'RDF
                '27112008
                ObtenerEnvolventePunto(ClaveNodo, TipoPunto)

                'Fin RDF


                If dstPuntos.Tables("TablaPuntos").Rows(0).Item("Y_UTM").ToString() = Nothing Or dstPuntos.Tables("TablaPuntos").Rows(0).Item("X_UTM").ToString() = Nothing Then

                    Page.RegisterClientScriptBlock("mostrarFrameVisor", "<script language=javascript>" & _
                                                "cambioIFrame(0,0,40,0,'S')" & _
                                        "</script>")
                    'JavaScript.Alert(Me, "No se puede mostrar el punto por no tener coordenadas")
                Else
                    Page.RegisterClientScriptBlock("mostrarFrameVisor", "<script language=javascript>" & _
                                                    "cambioIFrame(" & dstPuntos.Tables("TablaPuntos").Rows(0).Item("X_UTM").ToString() & _
                                                    ", " & dstPuntos.Tables("TablaPuntos").Rows(0).Item("Y_UTM").ToString() & ",40,1,'N')" & _
                                            "</script>")
                End If
            End If
        End If
        'Catch Excepcion As Exception
        '    Alert(Page, "Seleccione un elemento de medida para ir al visor")
        'End Try
    End Sub
  
    Protected Sub ObtenerHijosEnvolvente(ByVal idarbol As Integer, ByRef dstHijosRecursivo As DataSet)
        Dim daHijos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstHijos As DataSet = New System.Data.DataSet()
        'variables para obtener envolvente
        Dim dstHijosRecursivo2 As DataSet = New System.Data.DataSet()
        Dim i As Integer

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
    'sentenciaSel = "Select idArbol, Tipo from PVYCR_arbol where idArbolPadre = " & idarbol
    'ROCIO
    'FEHCA: 15/06/2010
    'NO FUNCIONA CORRECTAMENTE EL FILTRO DESTINOS - CAUCES
    '******************************************
    'he añadido el ORDER BY
    sentenciaSel = "Select idArbol, Tipo from PVYCR_arbol where idArbolPadre = " & idarbol & " ORDER BY Tipo DESC"
    '******************************************
        daHijos.SelectCommand.CommandText = sentenciaSel
        daHijos.Fill(dstHijos, "TablaHijos")
        'TablaHijosrecursivo = dstHijos.Tables("TablaHijos").Clone
        Dim numfilas As Integer
        If Not dstHijosRecursivo.Tables.Contains("TablaHijosRecursivo") Then
            dstHijosRecursivo.Tables.Add("TablaHijosRecursivo")
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Columns.Add(New DataColumn("idArbol"))
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Columns.Add(New DataColumn("Tipo"))
            numfilas = 0
        End If

        For i = 0 To dstHijos.Tables("TablaHijos").Rows.Count - 1
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows.Add()
            numfilas = dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows.Count
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("idArbol") = dstHijos.Tables("TablaHijos").Rows(i).Item("idArbol")
            dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("Tipo") = dstHijos.Tables("TablaHijos").Rows(i).Item("Tipo")
            ObtenerHijosEnvolvente(dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("idArbol").ToString, dstHijosRecursivo)
        Next
    End Sub

    Protected Sub Obtenerenvolvente(ByVal claveNodo As String, ByRef x As Integer, ByRef Y As Integer, ByRef radio As Decimal, ByRef NumReg As Integer)
        Dim idCauce, Xmayor, Xmenor, Ymayor, Ymenor, i, diferenciaXMayor, diferenciaXMayor_ant, diferenciaYMayor, diferenciaYMayor_ant As Integer
        Dim Xcentrado, Ycentrado, radioX, radioY As Decimal
        Dim daHijosCauce As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstHijosCauce As DataSet = New System.Data.DataSet()
        Dim CodigoInventario As String

        'RDF 20080718
        '------------------------------------------------------------
        Dim sentenciaInsert = ""
        Dim conexionVisor As System.Data.SqlClient.SqlConnection
        conexionVisor = New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings("dsn"))
        Dim comandoVisor As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexionVisor)
        'If conexionVisor.State = ConnectionState.Closed Then conexionVisor.Open()
        utiles.Comprobar_Conexion_BD(Page, conexionVisor, True)
        comandoVisor.CommandText = "delete from TSesionVisorResaltar where idSesion='" & Session.SessionID & "'"
        comandoVisor.ExecuteNonQuery()
        comandoVisor.CommandText = "delete from TSesionVisorDes where idSesion='" & Session.SessionID & "'"
        comandoVisor.ExecuteNonQuery()



        '------------------------------------------------------------
        'Hasta ahora no se ejecutaba esta función si se seleccionaba un punto del árbol
        'primero obtendremos todos los puntos que pertenecen al cauce
        sentenciaSel = "Select idArbol from PVYCR_arbol where codigoCauce = '" & claveNodo & "' "


        daArbol.SelectCommand.CommandText = sentenciaSel
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        idCauce = daArbol.SelectCommand.ExecuteScalar()
        'Obtenemos de forma recursiva los id de los hijos
        Dim dstHijosRecursivo As DataSet = New System.Data.DataSet()
        'RDF
        '19/11/2008
        Dim dstCauces As DataSet = New System.Data.DataSet()

        ObtenerHijosEnvolvente(idCauce, dstHijosRecursivo)

        'RDF
        'FEcha: 23/06/2010
        'No incluye el nodo seleccionado
        'Se añade también el nodo seleccionado
        sentenciaSel = "Select idArbol, Tipo from PVYCR_arbol where idArbol = " & idCauce
        daHijosCauce.SelectCommand.CommandText = sentenciaSel
        Dim dstHijos As New DataSet
        daHijosCauce.Fill(dstHijos, "TablaHijos")
        dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows.Add()
        Dim numfilas As Integer
        numfilas = dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows.Count
        dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("idArbol") = dstHijos.Tables("TablaHijos").Rows(0).Item("idArbol")
        dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows(numfilas - 1).Item("Tipo") = dstHijos.Tables("TablaHijos").Rows(0).Item("Tipo")


        If dstHijosRecursivo.Tables("TablaHijosRecursivo").Rows.Count <> 0 Then
            Dim num As Int16 = dstHijosRecursivo.Tables(0).Rows.Count
            For i = 0 To num - 1



                'PROBAR Capa CAUCES
                'DESCOMENTAR CUANDO DEN DE ALTA LA CAPA DE CAUCES
                'PROBARLO BIEN
                'RDF 27112008
                If dstHijosRecursivo.Tables(0).Rows(i).Item("Tipo").ToString = "P" Then
                    sentenciaSel = "Select A.codigocauce,A.CodigoPVYCR, X_UTM, Y_UTM,P.TipoPunto from PVYCR_arbol A, PVYCR_Puntos P " & _
                    "where A.codigoPVYCR = P.codigoPVYCR and X_UTM is not null and Y_UTM is not null " & _
                    "and idarbol = " & dstHijosRecursivo.Tables(0).Rows(i).Item("idarbol").ToString

                    'RDF
                    'Fecha 19/11/2008
                    daHijosCauce.SelectCommand.CommandText = sentenciaSel
                    daHijosCauce.Fill(dstHijosCauce, "TablaHijosCauce")


                Else
                    'RDF
                    'Fecha: 12/11/2008
                    'filtro de cauces
                    If dstHijosRecursivo.Tables(0).Rows(i).Item("Tipo").ToString = "C" Then
                        sentenciaSel = "Select A.codigocauce from PVYCR_arbol A, PVYCR_Cauces C " & _
                        "where A.codigoCauce = C.codigoCauce " & _
                        "and idarbol = " & dstHijosRecursivo.Tables(0).Rows(i).Item("idarbol").ToString

                        'RDF
                        'Fecha 19/11/2008
                        daHijosCauce.SelectCommand.CommandText = sentenciaSel
            daHijosCauce.Fill(dstCauces, "TablaCauce")

                    Else
                        sentenciaSel = "Select A.codigocauce,A.CodigoPVYCR, X_UTM, Y_UTM,P.TipoPunto from PVYCR_arbol A, PVYCR_Puntos P " & _
                        "where A.codigoCauce = P.codigoCauce and X_UTM is not null and Y_UTM is not null " & _
                        "and idarbol = " & dstHijosRecursivo.Tables(0).Rows(i).Item("idarbol").ToString

                        'RDF
                        'Fecha 19/11/2008
                        daHijosCauce.SelectCommand.CommandText = sentenciaSel
                        daHijosCauce.Fill(dstHijosCauce, "TablaHijosCauce")

                    End If
                End If
            Next


      If dstHijosCauce.Tables("TablaHijosCauce").Rows.Count = 0 Then
        NumReg = dstHijosCauce.Tables("TablaHijosCauce").Rows.Count
      Else
        'este numreg nos sirve para saber si tenemos que mostrar o no una alerta
        NumReg = dstHijosCauce.Tables("TablaHijosCauce").Rows.Count

        'obtenemos la Xmayor, Xmenor, Ymayor, Ymenor
        dstHijosCauce.Tables("TablaHijosCauce").DefaultView.Sort = "X_UTM desc"
        Xmayor = Convert.ToInt64(0 & dstHijosCauce.Tables("TablaHijosCauce").DefaultView(0).Item("X_UTM"))
        dstHijosCauce.Tables("TablaHijosCauce").DefaultView.Sort = "Y_UTM desc"
        Ymayor = Convert.ToInt64(0 & dstHijosCauce.Tables("TablaHijosCauce").DefaultView(0).Item("Y_UTM"))
        dstHijosCauce.Tables("TablaHijosCauce").DefaultView.Sort = "X_UTM"
        Xmenor = Convert.ToInt64(0 & dstHijosCauce.Tables("TablaHijosCauce").DefaultView(0).Item("X_UTM"))
        dstHijosCauce.Tables("TablaHijosCauce").DefaultView.Sort = "Y_UTM"
        Ymenor = Convert.ToInt64(0 & dstHijosCauce.Tables("TablaHijosCauce").DefaultView(0).Item("Y_UTM"))
        'calculamos el valor medio para la X y la Y
        Xcentrado = ((Xmayor - Xmenor) / 2) + Xmenor
        Ycentrado = ((Ymayor - Ymenor) / 2) + Ymenor
        'obtendremos el radio, para elloobtendremos la diferencia entre las Xcentrada y el resto de X, y nos quedaremos con la mayor diferencia
        ' lo mismo para las Y
        diferenciaXMayor = 0
        diferenciaXMayor_ant = 0
        diferenciaYMayor = 0
        diferenciaYMayor_ant = 0
        For i = 0 To dstHijosCauce.Tables("TablaHijosCauce").Rows.Count - 1
          If utiles.nullACero(dstHijosCauce.Tables("TablaHijosCauce").Rows(i).Item("X_UTM")) = 0 Then
            diferenciaXMayor = Math.Abs(Xcentrado - 0)
          Else
            diferenciaXMayor = Math.Abs(Xcentrado - dstHijosCauce.Tables("TablaHijosCauce").Rows(i).Item("X_UTM"))
          End If
          If utiles.nullACero(dstHijosCauce.Tables("TablaHijosCauce").Rows(i).Item("Y_UTM")) = 0 Then
            diferenciaYMayor = Math.Abs(Ycentrado - 0)
          Else
            diferenciaYMayor = Math.Abs(Ycentrado - dstHijosCauce.Tables("TablaHijosCauce").Rows(i).Item("Y_UTM"))
          End If

          If diferenciaXMayor > diferenciaXMayor_ant Then
            diferenciaXMayor_ant = diferenciaXMayor
          End If
          If diferenciaYMayor > diferenciaYMayor_ant Then
            diferenciaYMayor_ant = diferenciaYMayor
          End If

          'RDF 20080718
          '--------------------------------------
          If Not IsDBNull(dstHijosCauce.Tables("TablaHijosCauce").Rows(i).Item("CodigoPVYCR")) Then
            If dstHijosCauce.Tables("TablaHijosCauce").Rows(i).Item("TipoPunto") = "G" Then
              comandoVisor.CommandText = "insert into TSesionVisorResaltar(idSesion,Codigo,Capa,Tipo) values('" & Session.SessionID & "','" & dstHijosCauce.Tables("TablaHijosCauce").Rows(i).Item("CodigoPVYCR") & "','Puntos','R')"
            Else
              comandoVisor.CommandText = "insert into TSesionVisorResaltar(idSesion,Codigo,Capa,Tipo) values('" & Session.SessionID & "','" & dstHijosCauce.Tables("TablaHijosCauce").Rows(i).Item("CodigoPVYCR") & "','Puntos','O')"
            End If
          Else
            CodigoInventario = ObtenerCodigoInventario(dstHijosCauce.Tables("TablaHijosCauce").Rows(i).Item("Codigocauce"))
            If CodigoInventario <> "" Then
              comandoVisor.CommandText = "insert into TSesionVisorDes(idSesion,CodigoInventario) values('" & Session.SessionID & "','" & CodigoInventario & "')"
            End If
          End If
     


          comandoVisor.ExecuteNonQuery()
        Next
        'For i = 0 To dstCauces.Tables("TablaCauce").Rows.Count - 1
        '  If dstCauces.Tables("TablaCauce").Rows.Count > 0 Then
        '    CodigoInventario = ObtenerCodigoInventario(dstCauces.Tables("TablaCauce").Rows(i).Item("Codigocauce"))
        '    If CodigoInventario <> "" Then
        '      comandoVisor.CommandText = "insert into TSesionVisorDes(idSesion,CodigoInventario) values('" & Session.SessionID & "','" & CodigoInventario & "')"
        '    End If
        '  End If
        '  comandoVisor.ExecuteNonQuery()
        'Next


        'PROBAR Capa CAUCES
        'DESCOMENTAR CUANDO DEN DE ALTA LA CAPA DE CAUCES
        'PROBARLO BIEN
        'RDF 27112008

        '--------------------------------------
        'RDF 19/11/2008
        'Filtro de cauces
        '--------------------------------------
        'RDF 27112008
        'he añadido esta línea porque daba error, cuando la tabla TablaCauce es null
                If dstCauces.Tables.Contains("TablaCauce") Then
                    If dstCauces.Tables("TablaCauce").Rows.Count > 0 Then
                        For i = 0 To dstCauces.Tables("TablaCauce").Rows.Count - 1
                            comandoVisor.CommandText = "insert into TSesionVisorResaltar(idSesion,Codigo,Capa) values('" & Session.SessionID & "','" & dstCauces.Tables("TablaCauce").Rows(i).Item("CodigoCauce") & "','Cauces')"
                            comandoVisor.ExecuteNonQuery()

                            'CodInventario
                            CodigoInventario = ObtenerCodigoInventario(dstCauces.Tables("TablaCauce").Rows(i).Item("Codigocauce"))

                            'RDF
                            'Fecha: 23/06/2010
                            'Se ha añadido el filtrado en la capa Cauces (campos Inscripción) y la capa MU_USOSSUPERFICIAL (NUMREG1)
                            'Inscripcion
                            '********************************************************************************************************
                            Dim inscripcion As String
                            inscripcion = ObtenerInscripcion(dstCauces.Tables("TablaCauce").Rows(i).Item("Codigocauce"))

                            If CodigoInventario <> "" Or inscripcion <> "" Then
                                'Capa destinos
                                If CodigoInventario <> "" And inscripcion <> "" Then
                                    comandoVisor.CommandText = "insert into TSesionVisorDes(idSesion,CodigoInventario,INSCRIPCION) values('" & Session.SessionID & "','" & CodigoInventario & "','" & inscripcion & "')"
                                Else
                                    If CodigoInventario <> "" Then
                                        comandoVisor.CommandText = "insert into TSesionVisorDes(idSesion,CodigoInventario) values('" & Session.SessionID & "','" & CodigoInventario & "')"
                                    Else
                                        comandoVisor.CommandText = "insert into TSesionVisorDes(idSesion,INSCRIPCION) values('" & Session.SessionID & "','" & inscripcion & "')"

                                    End If
                                End If
                                '********************************************************************************************************

                                comandoVisor.ExecuteNonQuery()
                            End If
                        Next
                    End If
                End If
        '--------------------------------------


        diferenciaXMayor = diferenciaXMayor_ant
        diferenciaYMayor = diferenciaYMayor_ant

        radioX = diferenciaXMayor ' + (diferenciaXMayor * 0.3)
        radioY = diferenciaYMayor ' + (diferenciaYMayor * 0.3)

        If radioX > radioY Then
          radio = radioX
        Else
          radio = radioY
        End If
        If radio > 4000 Then
          radio = radio
        Else
          radio = radio + (radio * 0.5)
        End If
        x = Xcentrado
        Y = Ycentrado
      End If
        Else
            x = 0
            Y = 0
            NumReg = 0
            radio = 0
        End If
      
    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'Dim strScript As String
        ''Script de Cliente para visualización del panel de sistemas
        'strScript = "<SCRIPT LANGUAGE='JAVASCRIPT'>" & _
        '                   "document.getElementById('lblLecturasVisor').value = window.parent.document.getElementById('h_lecturasvisor').value;" & _
        '                   "alert(document.getElementById('lblLecturasVisor').value );" & _
        '            "</SCRIPT>"
        'ClientScript.RegisterClientScriptBlock(Page.GetType, "actualizarLecturasVisor", strScript)
        ucPaginacionA.ruta = "../"
        ucPaginacionE.ruta = "../"
        ucPaginacionV.ruta = "../"
        ucPaginacionH.ruta = "../"
        ucPaginacionD.ruta = "../"
    End Sub

    Protected Sub AceptarFiltroAcequias(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20080714
        Dim strFiltro As String
        Dim tipoelem As String = "Q"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
        Dim Operador As Boolean
        ucPaginacionA.lblPaginatext = "1"


        strFiltro = ""
        If txtFfechamedida.Text <> "" Then
      strFiltro = strFiltro + " AND  D.Fecha_Medida between '" & DateAdd(DateInterval.Day, 1, txtFfechamedida) & " 00:00:00' and '" & DateAdd(DateInterval.Day, 1, txtFfechamedida) & " 23:59:59' "

        End If

        If txtFescala.Text <> "" Then
            Operador = ((txtFescala.Text.ToString.IndexOf("=") > -1) Or (txtFescala.Text.ToString.IndexOf(">") > -1) Or (txtFescala.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D.Escala_M " + IIf(Operador = False, "=", "") + Replace(txtFescala.Text, ",", ".") + " "
        End If

        If txtFcalado.Text <> "" Then
            Operador = ((txtFcalado.Text.ToString.IndexOf("=") > -1) Or (txtFcalado.Text.ToString.IndexOf(">") > -1) Or (txtFcalado.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D.Calado_M " + IIf(Operador = False, "=", "") + Replace(txtFcalado.Text, ",", ".") + " "
        End If

        If txtFregcurva.Text <> "" Then
            strFiltro = strFiltro + " AND D.RegimenCurva like '" + txtFregcurva.Text + "' "
        End If

        If txtFnparada.Text <> "" Then
            strFiltro = strFiltro + " AND D.NumeroParada=" + txtFnparada.Text + " "
        End If

        If txtFcaudal.Text <> "" Then
            Operador = ((txtFcaudal.Text.ToString.IndexOf("=") > -1) Or (txtFcaudal.Text.ToString.IndexOf(">") > -1) Or (txtFcaudal.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D.Caudal_M3S " + IIf(Operador = False, "=", "") + Replace(txtFcaudal.Text, ",", ".") + " "
        End If

        'If txtFdiferencial.Text <> "" Then
        '    strFiltro = " AND Diferencial like '" + txtFdiferencial.Text + "' "
        'End If

        If txtFduda.Text <> "" Then
            strFiltro = strFiltro + " AND D.Duda_Calidad=" + txtFduda.Text + " "
        End If

        If txtFobservaciones.Text <> "" Then
            strFiltro = strFiltro + " AND D.Observaciones like '" + txtFobservaciones.Text + "' "
        End If

        If ddlFfuentedatos.Text <> "" Then
            strFiltro = strFiltro + " AND D.Cod_Fuente_Dato like '" + ddlFfuentedatos.Text + "' "
        End If

        If ddlFobtencioncaudal.Text <> "" Then
            strFiltro = strFiltro + " AND D.TipoObtencionCaudal like '" + ddlFobtencioncaudal.Text + "' "
        End If

        Session("Filtro") = strFiltro
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
        txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)

        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAcequias")              'IPIM 20080821: Para pasarle el dataset entero a los informes

        'IPIM 24/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptAcequias.DataSource = dstElementos.Tables("TablaAcequiasPag")
        rptAcequias.DataBind()
        'ncm 24/10/2008
        Me.lblobtenerNumLecturasQ.Text = Me.obtenerNumLecturasQ()
        Me.lblobtenerTotNumLecturasQ.Text = Me.obtenerTotNumLecturas("Q", codigoPVYCR, lblidElemento.Text)
        Me.lblCaudalAcumuladoQ.Text = Me.obtenerCaudalAcumulado
        Me.lblObtenerAnyoHidrologicoQ.Text = Me.obtenerAnyoHidrologico()
        Me.lblIntervaloFechasQ.Text = Me.obtenerIntervaloFechasQ()
        Me.lblObtenerNomElementoA.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoQ.Text = ObtenerPorConcesion(lblVolMaxAnuLegQ.Text, lblCaudalAcumuladoQ.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblNumpaginasDatabind()

    End Sub


    Protected Sub rellenarListas()
        Dim dstFuenteDatos As New DataSet

        Dim sql As String

        If sentenciaSel.IndexOf("order") > -1 Then
            sentenciaSel = sentenciaSel.Substring(0, sentenciaSel.IndexOf("order"))
        End If
        'NCM 20081021 se quita el top de la select para que salgan todos los obtencion caudal que hay en las lecturas seleccionadas, ya 
        ' que sino no aparecian todos.
        If sentenciaSel.IndexOf("d.CodigoPVYCR") > -1 Then
            sentenciaSel = "select " & sentenciaSel.Substring(sentenciaSel.IndexOf("d.CodigoPVYCR"))
        End If

        'RDF 20080908

        'En los filtros, mostrar solo los registros que aparecen en las lecturas
        sql = " SELECT DISTINCT consulta.Cod_Fuente_Dato FROM (" + sentenciaSel + ") as consulta "

        'sql = " SELECT DISTINCT consulta.Cod_Fuente_Dato, consulta.Cod_Fuente_Dato + ' - ' + consulta.DescFuenteDato  as Descripcion FROM (" + sentenciaSel + ") as consulta "
        utiles.Comprobar_Conexion_BD(Page, conexion)
        Dim daFuenteDatos As New SqlDataAdapter(sql, conexion)
        Dim dstObtencionCaudal As New DataSet

        Dim sqlObtencion As String
        sqlObtencion = " SELECT DISTINCT consulta.TipoObtencionCaudal FROM (" + sentenciaSel + ") as consulta "
        Dim daObtencionCaudal As New SqlDataAdapter(sqlObtencion, conexion)

        'RDF 20080715
        'Lista obtención caudal de los filtros
        'daFuenteDatos.SelectCommand.CommandText = "SELECT   TipoObtencionCaudal FROM PVYCR_TiposObtencionCaudal order by 1

        daFuenteDatos.Fill(dstFuenteDatos, "TablaFuente")
        ddlFfuentedatos.DataSource = dstFuenteDatos.Tables("TablaFuente")
        ddlFfuentedatos.DataValueField = "Cod_Fuente_Dato"
        'ddlFfuentedatos.DataTextField = "Descripcion"
        ddlFfuentedatos.DataBind()
        ddlFfuentedatos.Items.Insert(0, New ListItem("[Seleccionar]", ""))



        daObtencionCaudal.Fill(dstObtencionCaudal, "TablaObtencion")
        ddlFobtencioncaudal.DataSource = dstObtencionCaudal.Tables("TablaObtencion")
        ddlFobtencioncaudal.DataValueField = "TipoObtencionCaudal"
        ddlFobtencioncaudal.DataBind()
        ddlFobtencioncaudal.Items.Insert(0, New ListItem("[Seleccionar]", ""))


    End Sub
    Protected Sub rellenarListasM()
        Dim dstFuenteDatos As New DataSet
        Try
            If sentenciaSel.IndexOf("order") > -1 Then
                sentenciaSel = sentenciaSel.Substring(0, sentenciaSel.IndexOf("order"))
            End If
            'NCM 20081021 se quita el top de la select para que salgan todos los obtencion caudal que hay en las lecturas seleccionadas, ya 
            ' que sino no aparecian todos.
            If sentenciaSel.IndexOf("D.CodigoPVYCR") > -1 Then
                sentenciaSel = "select " & sentenciaSel.Substring(sentenciaSel.IndexOf("D.CodigoPVYCR"))
            End If


            Dim sql As String

            'RDF 20080908
            'En los filtros, mostrar solo los registros que aparecen en las lecturas

            sql = " SELECT DISTINCT consulta.Cod_Fuente_Dato FROM (" + sentenciaSel + ") as consulta "

            utiles.Comprobar_Conexion_BD(Page, conexion)
            Dim daFuenteDatos As New SqlDataAdapter(sql, conexion)

            'RDF 20080716
            'Lista obtención caudal de los filtros
            'daFuenteDatos.SelectCommand.CommandText = "SELECT   TipoObtencionCaudal FROM PVYCR_TiposObtencionCaudal order by 1"
            daFuenteDatos.Fill(dstFuenteDatos, "TablaFuente")
            ddlFfuentedatosM.DataSource = dstFuenteDatos.Tables("TablaFuente")
            ddlFfuentedatosM.DataValueField = "Cod_Fuente_Dato"
            ddlFfuentedatosM.DataBind()
            ddlFfuentedatosM.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        Catch ex As Exception

        End Try



    End Sub

    Protected Sub rellenarListasE()
        Dim dstFuenteDatos As New DataSet
        Dim sql As String

        'RDF 20080908
        'En los filtros, mostrar solo los registros que aparecen en las lecturas
        Try
            If sentenciaSel.IndexOf("order") > -1 Then
                sentenciaSel = sentenciaSel.Substring(0, sentenciaSel.IndexOf("order"))
            End If
            'NCM 20081021 se quita el top de la select para que salgan todos los obtencion caudal que hay en las lecturas seleccionadas, ya 
            ' que sino no aparecian todos.
            If sentenciaSel.IndexOf("D.CodigoPVYCR") > -1 Then
                sentenciaSel = "select " & sentenciaSel.Substring(sentenciaSel.IndexOf("D.CodigoPVYCR"))
            End If

            sql = " SELECT DISTINCT consulta.Cod_Fuente_Dato FROM (" + sentenciaSel + ") as consulta "

            ''sql = "SELECT DISTINCT F.Cod_Fuente_Dato FROM   FUENTES_DE_DATOS F INNER JOIN PVYCR_DatosAlimentacion D ON F.Cod_Fuente_Dato=D.Cod_Fuente_Dato WHERE D.codigoPVYCR='" + Me.txtCodigoPVYCR.Text + "'"
            'If Session("Filtro") <> "" Then
            '    sql = sql + Session("Filtro")
            'End If

            ' sql = sql + " ORDER BY 1"

            utiles.Comprobar_Conexion_BD(Page, conexion)
            Dim daFuenteDatos As New SqlDataAdapter(sql, conexion)

            'RDF 20080716
            'Lista obtención caudal de los filtros
            'daFuenteDatos.SelectCommand.CommandText = "SELECT   TipoObtencionCaudal FROM PVYCR_TiposObtencionCaudal order by 1"
            daFuenteDatos.Fill(dstFuenteDatos, "TablaFuente")
            ddlFfuentedatosE.DataSource = dstFuenteDatos.Tables("TablaFuente")
            ddlFfuentedatosE.DataValueField = "Cod_Fuente_Dato"
            ddlFfuentedatosE.DataBind()
            ddlFfuentedatosE.Items.Insert(0, New ListItem("[Seleccionar]", ""))

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub rellenarListasD()
        Dim dstFuenteDatos As New DataSet
        Try
            If sentenciaSel.IndexOf("order") > -1 Then
                sentenciaSel = sentenciaSel.Substring(0, sentenciaSel.IndexOf("order"))
            End If
            'NCM 20081021 se quita el top de la select para que salgan todos los obtencion caudal que hay en las lecturas seleccionadas, ya 
            ' que sino no aparecian todos.
            If sentenciaSel.IndexOf("D.CodigoPVYCR") > -1 Then
                sentenciaSel = "select " & sentenciaSel.Substring(sentenciaSel.IndexOf("D.CodigoPVYCR"))
            End If


            Dim sql As String

            'RDF 20080908
            'En los filtros, mostrar solo los registros que aparecen en las lecturas

            sql = " SELECT DISTINCT consulta.Cod_Fuente_Dato FROM (" + sentenciaSel + ") as consulta "

            utiles.Comprobar_Conexion_BD(Page, conexion)
            Dim daFuenteDatos As New SqlDataAdapter(sql, conexion)

            'RDF 20080716
            'Lista obtención caudal de los filtros
            'daFuenteDatos.SelectCommand.CommandText = "SELECT   TipoObtencionCaudal FROM PVYCR_TiposObtencionCaudal order by 1"
            daFuenteDatos.Fill(dstFuenteDatos, "TablaFuente")
            ddlFfuentedatosD.DataSource = dstFuenteDatos.Tables("TablaFuente")
            ddlFfuentedatosD.DataValueField = "Cod_Fuente_Dato"
            ddlFfuentedatosD.DataBind()
            ddlFfuentedatosD.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        Catch ex As Exception

        End Try
    End Sub
    

    Protected Sub AceptarFiltroMotores(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20080716
        Dim strFiltro As String
        Dim tipoelem As String = "V"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
        Dim Operador As Boolean
        strFiltro = ""

        If ddlFfuentedatosM.Text <> "" Then
            
            strFiltro = strFiltro + " AND D.Cod_Fuente_Dato like '" + ddlFfuentedatosM.Text + "' "
        End If

        If txtFlectura.Text <> "" Then
            Operador = ((txtFlectura.Text.ToString.IndexOf("=") > -1) Or (txtFlectura.Text.ToString.IndexOf(">") > -1) Or (txtFlectura.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND cast(D.LecturaContador_M3 as decimal(32,2))" + IIf(Operador = False, "=", "") + Replace(txtFlectura.Text, ",", ".") + " "
        End If
        If txtFfechamedidaM.Text <> "" Then
            strFiltro = strFiltro + " AND  D.Fecha_Medida between '" & txtFfechamedidaM.Text & " 00:00:00' and '" & txtFfechamedidaM.Text & " 23:59:59' "
        End If

        If txtFfunciona.Text <> "" Then
            strFiltro = strFiltro + " AND D.Funciona like '" + txtFfunciona.Text + "' "
        End If

        If txtFobservacionesM.Text <> "" Then
            strFiltro = strFiltro + " AND D.Observaciones like '" + txtFobservacionesM.Text + "' "
        End If

        If txtFjustificacion.Text <> "" Then
            strFiltro = strFiltro + " AND D.Justificacion like '" + txtFjustificacion.Text + "' "
        End If
        If txtFincidencia.Text <> "" Then
            strFiltro = strFiltro + " AND D.idIncidenciaVolumetrica=" + txtFincidencia.Text + " "
        End If

        If txtFconsumo.Text <> "" Then
            Operador = ((txtFconsumo.Text.ToString.IndexOf("=") > -1) Or (txtFconsumo.Text.ToString.IndexOf(">") > -1) Or (txtFconsumo.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D.ConsumoVolumetricoAdicional " + IIf(Operador = False, "=", "") + Replace(txtFconsumo.Text, ",", ".") + " "
        End If

        If txtFreinicio.Text <> "" Then
            Operador = ((txtFreinicio.Text.ToString.IndexOf("=") > -1) Or (txtFreinicio.Text.ToString.IndexOf(">") > -1) Or (txtFreinicio.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D.ReinicioLecturaVolumetrica " + IIf(Operador = False, "=", "") + Replace(txtFreinicio.Text, ",", ".") + " "
        End If
        If txtFCaudalMedido.Text <> "" Then
            Operador = ((txtFCaudalMedido.Text.ToString.IndexOf("=") > -1) Or (txtFCaudalMedido.Text.ToString.IndexOf(">") > -1) Or (txtFCaudalMedido.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D.CaudalMedido " + IIf(Operador = False, "=", "") + txtFCaudalMedido.Text + " "
        End If


        Session("FiltroM") = strFiltro
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
        txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaMotores")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptVolumen.DataSource = dstElementos.Tables("TablaMotoresPag")
        rptVolumen.DataBind()
        Me.lblAnyoHidrologicoV.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumuladoV.Text = Me.obtenerVolumenAcumulado
        Me.lblobtenerNumlecturasV.Text = Me.obtenerNumLecturasV
        Me.lblobtenerTotNumLecturasV.Text = Me.obtenerTotNumLecturas("V", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaV.Text = Me.obtenerIntervaloFechasV()
        Me.lblobtenerNomElementoV.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoV.Text = ObtenerPorConcesion(lblVolMaxAnuLegV.Text, lblobtenervolumenacumuladoV.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionV.lblNumpaginasDatabind()

    End Sub

    Protected Sub AceptarFiltroEnergia(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20080716
        Dim strFiltro As String
        Dim tipoelem As String = "E"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
        Dim Operador As Boolean

        strFiltro = ""

        If ddlFfuentedatosE.Text <> "" Then
            strFiltro = strFiltro + " AND D.Cod_Fuente_Dato like '" + ddlFfuentedatosE.Text + "' "
        End If

        If txtFlecturaIE.Text <> "" Then
            Operador = ((txtFlecturaIE.Text.ToString.IndexOf("=") > -1) Or (txtFlecturaIE.Text.ToString.IndexOf(">") > -1) Or (txtFlecturaIE.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND cast(D.LecturaI as decimal(32,2)) " + IIf(Operador = False, "=", "") + Replace(txtFlecturaIE.Text, ",", ".") + " "
        End If

        If txtFlecturaIIE.Text <> "" Then
            Operador = ((txtFlecturaIIE.Text.ToString.IndexOf("=") > -1) Or (txtFlecturaIIE.Text.ToString.IndexOf(">") > -1) Or (txtFlecturaIIE.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND cast(D.LecturaII as decimal(32,2)) " + IIf(Operador = False, "=", "") + Replace(txtFlecturaIIE.Text, ",", ".") + " "
        End If

        If txtFlecturaIIIE.Text <> "" Then
            Operador = ((txtFlecturaIIIE.Text.ToString.IndexOf("=") > -1) Or (txtFlecturaIIIE.Text.ToString.IndexOf(">") > -1) Or (txtFlecturaIIIE.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND cast(D.LecturaIII as decimal(32,2)) " + IIf(Operador = False, "=", "") + Replace(txtFlecturaIIIE.Text, ",", ".") + " "
        End If

        If txtFtotalkvarE.Text <> "" Then
            Operador = ((txtFtotalkvarE.Text.ToString.IndexOf("=") > -1) Or (txtFtotalkvarE.Text.ToString.IndexOf(">") > -1) Or (txtFtotalkvarE.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND cast(D.Total_Kvar as decimal(32,2)) " + IIf(Operador = False, "=", "") + Replace(txtFtotalkvarE.Text, ",", ".") + " "
        End If

        If txtFtotalkwE.Text <> "" Then
            Operador = ((txtFtotalkwE.Text.ToString.IndexOf("=") > -1) Or (txtFtotalkwE.Text.ToString.IndexOf(">") > -1) Or (txtFtotalkwE.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND cast(D.Total_KWH as decimal(32,2)) " + IIf(Operador = False, "=", "") + Replace(txtFtotalkwE.Text, ",", ".") + " "
        End If



        If txtFfechamedidaE.Text <> "" Then
            strFiltro = strFiltro + " AND  D.Fecha_Medida between '" & txtFfechamedidaE.Text & " 00:00:00' and '" & txtFfechamedidaE.Text & " 23:59:59' "
        End If

        If txtFfuncionaE.Text <> "" Then
            strFiltro = strFiltro + " AND D.Funciona like '" + txtFfuncionaE.Text + "' "
        End If

        If txtFobservacionesE.Text <> "" Then
            strFiltro = strFiltro + " AND D.Observaciones like '" + txtFobservacionesE.Text + "' "
        End If

        'If txtFjustificacionE.Text <> "" Then
        '    strFiltro = strFiltro + " AND D.Justificacion like '" + txtFjustificacionE.Text + "' "
        'End If
        If txtFincidenciaE.Text <> "" Then
            strFiltro = strFiltro + " AND D.idIncidenciaElectrica=" + txtFincidenciaE.Text + " "
        End If

        If txtFconsumoE.Text <> "" Then
            Operador = ((txtFconsumoE.Text.ToString.IndexOf("=") > -1) Or (txtFconsumoE.Text.ToString.IndexOf(">") > -1) Or (txtFconsumoE.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D.ConsumoElectricoAdicional " + IIf(Operador = False, "=", "") + Replace(txtFconsumoE.Text, ",", ".") + " "
        End If

        If txtFreinicioE.Text <> "" Then
            Operador = ((txtFreinicioE.Text.ToString.IndexOf("=") > -1) Or (txtFreinicioE.Text.ToString.IndexOf(">") > -1) Or (txtFreinicioE.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D.ReinicioLecturaElectrica" + IIf(Operador = False, "=", "") + Replace(txtFreinicioE.Text, ",", ".") + " "
        End If



        Session("FiltroE") = strFiltro
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
        txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAlimentacion")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacionPag")
        rptEnergia.DataBind()
        Me.lblAnyoHidrologicoE.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumulado.Text = Me.obtenerVolumenElectricoAcumulado
        Me.lblobtenervolumenacumuladoE.Text = Me.obtenerVolumenElectricoAcumuladoM3
        Me.lblobtenerNumlecturasE.Text = Me.obtenerNumLecturasE
        Me.lblobtenerTotNumLecturasE.Text = Me.obtenerTotNumLecturas("E", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaE.Text = Me.obtenerIntervaloFechasE()
        Me.lblObtenerNomElementoE.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoE.Text = ObtenerPorConcesion(lblVolMaxAnuLegE.Text, lblobtenervolumenacumuladoE.Text)

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblNumpaginasDatabind()
    End Sub

    Protected Sub AceptarFiltroDiferencial(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20080716
        Dim strFiltro As String
        Dim tipoelem As String = "D"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
        Dim Operador As Boolean

        strFiltro = ""

        If ddlFfuentedatosD.Text <> "" Then
            strFiltro = strFiltro + " AND D.Cod_Fuente_Dato like '" + ddlFfuentedatosD.Text + "' "
        End If

        If Me.txtFSuministros.Text <> "" Then
            Operador = ((txtFSuministros.Text.ToString.IndexOf("=") > -1) Or (txtFSuministros.Text.ToString.IndexOf(">") > -1) Or (txtFSuministros.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND cast(D.SuministromensualM3 as decimal(32,2)) " + IIf(Operador = False, "=", "") + Replace(txtFSuministros.Text, ",", ".") + " "
        End If

        If txtFfechamedidaD.Text <> "" Then
            strFiltro = strFiltro + " AND  D.Fecha_Medida between '" & txtFfechamedidaD.Text & " 00:00:00' and '" & txtFfechamedidaD.Text & " 23:59:59' "
        End If

        If txtFobservacionesD.Text <> "" Then
            strFiltro = strFiltro + " AND D.Observaciones like '" + txtFobservacionesD.Text + "' "
        End If

        Session("FiltroD") = strFiltro
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegD.Text, chkFiltroNulasD.Checked, _
        txtFiltroFechaFinD.Text, txtFiltroFechaIniD.Text, txtFiltrarCodFuenteDatoD.Text, Session("FiltroD"), ucPaginacionD, chkReducirLecD.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaDiferencial")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptDiferencial.DataSource = dstElementos.Tables("TablaDiferencialPag")

        rptDiferencial.DataBind()
        Me.lblAnyoHidrologicoD.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumuladoD.Text = Me.obtenerVolumenDiferencialesAcumulado
        Me.lblobtenerNumlecturasD.Text = Me.obtenerNumLecturasD()
        Me.lblObtenerTotNumlecturasD.Text = Me.obtenerTotNumLecturas("D", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaD.Text = Me.obtenerIntervaloFechasd()
        Me.lblobtenerNomElementoD.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoD.Text = ObtenerPorConcesion(lblVolMaxAnuLegD.Text, lblobtenervolumenacumuladoD.Text)

        ucPaginacionD.lblNumpaginasDatabind()
    End Sub
    Protected Sub lbtAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtAceptar.Click
        'NCM 22/10/2008 cambiado por un link button
        'Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'EGB21042008 Función Provisional para acceso a las lecturas. Este procedimiento es nuevo
        'respecto a la versión CaucePunt + Arbol
        Dim codigoPVYCR As String
        Dim TipoElemento As String
        Dim NombreElemento As String
        Dim IdElementoMedida As String
        Dim sentenciaSel As String

        codigoPVYCR = utiles.BlancoANull(Me.txtCodigoPVYCR.Text)
        IdElementoMedida = utiles.BlancoANull(Me.txtIdElementoMedida.Text)
        sentenciaSel = "SELECT Tipo FROM PVYCR_ElementosMedida WHERE CodigoPVYCR='" & codigoPVYCR & "' AND IdElementoMedida='" & IdElementoMedida & "'"
        Me.lblidElemento.Text = IdElementoMedida
        daArbol.SelectCommand.CommandText = sentenciaSel
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        TipoElemento = daArbol.SelectCommand.ExecuteScalar()

        Select Case TipoElemento
            Case "Q"
                NombreElemento = codigoPVYCR & " - " & IdElementoMedida & " - CAUDAL"
            Case "V"
                NombreElemento = codigoPVYCR & " - " & IdElementoMedida & " - VOLUMEN"
            Case "E"
                NombreElemento = codigoPVYCR & " - " & IdElementoMedida & " - ENERGIA"
            Case "H"
                NombreElemento = codigoPVYCR & " - " & IdElementoMedida & " - HORAS"
            Case Else
                NombreElemento = ""
        End Select
        txtDescripcionElementoMedida.Text = NombreElemento
        ActivarPanelDetalles(codigoPVYCR, TipoElemento, NombreElemento, IdElementoMedida)
        'End Sub
    End Sub

    Protected Sub lbtFiltroAceptarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltroAceptarE.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim tipoelem As String = "E"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblPaginatext = "1"

        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
        txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAlimentacion")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacionPag")
        rptEnergia.DataBind()
        Me.lblAnyoHidrologicoE.Text = Me.obtenerAnyoHidrologicoEtiqueta()
        Me.lblobtenervolumenacumulado.Text = Me.obtenerVolumenElectricoAcumulado
        Me.lblobtenervolumenacumuladoE.Text = Me.obtenerVolumenElectricoAcumuladoM3
        Me.lblobtenerNumlecturasE.Text = Me.obtenerNumLecturasE
        Me.lblobtenerTotNumLecturasE.Text = Me.obtenerTotNumLecturas("E", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaE.Text = Me.obtenerIntervaloFechasE()
        'Me.lblObtenerNomElementoA.Text = Me.obtenerNomElemento()       'ipim 27/11/2008 lo cambio, pq creo q es un error
        Me.lblObtenerNomElementoE.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoE.Text = ObtenerPorConcesion(lblVolMaxAnuLegE.Text, lblobtenervolumenacumuladoE.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblNumpaginasDatabind()

        rellenarListasE()
    End Sub

    Protected Sub lbtFiltroCancelarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltroCancelarE.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim tipoelem As String = "E"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblPaginatext = "1"
        txtFiltroFechaIniE.Text = ""
        txtFiltroFechaFinE.Text = ""
        txtFiltrarCodFuenteDatoE.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasE.Checked = True
        txtFiltroNRegE.Text = ""
        'chkFiltroPorDiaE.Checked = False
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
        txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAlimentacion")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacionPag")
        rptEnergia.DataBind()
        Me.lblAnyoHidrologicoE.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumulado.Text = Me.obtenerVolumenElectricoAcumulado
        Me.lblobtenervolumenacumuladoE.Text = Me.obtenerVolumenElectricoAcumuladoM3
        Me.lblobtenerNumlecturasE.Text = Me.obtenerNumLecturasE
        Me.lblobtenerTotNumLecturasE.Text = Me.obtenerTotNumLecturas("E", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaE.Text = Me.obtenerIntervaloFechasE()
        Me.lblObtenerNomElementoE.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoE.Text = ObtenerPorConcesion(lblVolMaxAnuLegE.Text, lblobtenervolumenacumuladoE.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionE.lblNumpaginasDatabind()
    End Sub



    Protected Sub lbtFiltroAceptarA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltroAceptarA.Click
        'EGB 21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblPaginatext = "1"

        'IPIM 06052008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim tipoelem As String = "Q"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
        txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        'FIN IPIM

        Session("dst") = dstElementos.Tables("TablaAcequias")              'IPIM 20080821: Para pasarle el dataset entero a los informes

        'IPIM 24/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptAcequias.DataSource = dstElementos.Tables("TablaAcequiasPag")
        rptAcequias.DataBind()
        Me.lblobtenerNumLecturasQ.Text = Me.obtenerNumLecturasQ()
        Me.lblobtenerTotNumLecturasQ.Text = Me.obtenerTotNumLecturas("Q", codigoPVYCR, lblidElemento.Text)
        Me.lblCaudalAcumuladoQ.Text = Me.obtenerCaudalAcumulado
        Me.lblObtenerAnyoHidrologicoQ.Text = Me.obtenerAnyoHidrologicoEtiqueta()
        Me.lblIntervaloFechasQ.Text = Me.obtenerIntervaloFechasQ()
        Me.lblObtenerNomElementoA.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoQ.Text = ObtenerPorConcesion(lblVolMaxAnuLegQ.Text, lblCaudalAcumuladoQ.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblNumpaginasDatabind()
        rellenarListas()

    End Sub

    Protected Sub lbtFiltroCancelarA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltroCancelarA.Click
        'EGB 21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblPaginatext = "1"

        Dim tipoelem As String = "Q"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        txtfiltroFechaIni.Text = ""
        txtFiltroFechaFin.Text = ""
        txtFiltrarCodFuentedato.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasQ.Checked = True
        txtFiltroNregQ.Text = ""
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
        txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaAcequias")              'IPIM 20080821: Para pasarle el dataset entero a los informes

        'IPIM 24/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptAcequias.DataSource = dstElementos.Tables("TablaAcequiasPag")
        rptAcequias.DataBind()
        Me.lblobtenerNumLecturasQ.Text = Me.obtenerNumLecturasQ()
        Me.lblobtenerTotNumLecturasQ.Text = Me.obtenerTotNumLecturas("Q", codigoPVYCR, lblidElemento.Text)
        Me.lblCaudalAcumuladoQ.Text = Me.obtenerCaudalAcumulado
        Me.lblObtenerAnyoHidrologicoQ.Text = Me.obtenerAnyoHidrologico()
        Me.lblIntervaloFechasQ.Text = Me.obtenerIntervaloFechasQ()
        Me.lblObtenerNomElementoA.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoQ.Text = ObtenerPorConcesion(lblVolMaxAnuLegQ.Text, lblCaudalAcumuladoQ.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionA.lblNumpaginasDatabind()
    End Sub

    Protected Sub lbtFiltroAceptarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltroAceptarV.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim tipoelem As String = "V"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionV.lblPaginatext = "1"
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
        txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaMotores")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptVolumen.DataSource = dstElementos.Tables("TablaMotoresPag")
        rptVolumen.DataBind()
        ucPaginacionV.lblNumpaginasDatabind()
        Me.lblAnyoHidrologicoV.Text = Me.obtenerAnyoHidrologicoEtiqueta()
        Me.lblobtenervolumenacumuladoV.Text = Me.obtenerVolumenAcumulado
        Me.lblobtenerNumlecturasV.Text = Me.obtenerNumLecturasV
        Me.lblobtenerTotNumLecturasV.Text = Me.obtenerTotNumLecturas("V", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaV.Text = Me.obtenerIntervaloFechasV()
        Me.lblobtenerNomElementoV.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoV.Text = ObtenerPorConcesion(lblVolMaxAnuLegV.Text, lblobtenervolumenacumuladoV.Text)
        rellenarListasM()
    End Sub

    Protected Sub lbtFiltroCancelarV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltroCancelarV.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        Dim tipoelem As String = "V"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionV.lblPaginatext = "1"

        txtFiltroFechaIniV.Text = ""
        txtFiltroFechaFinV.Text = ""
        txtFiltrarCodFuenteDatoV.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasV.Checked = True
        txtFiltroNRegV.Text = ""
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
        txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaMotores")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptVolumen.DataSource = dstElementos.Tables("TablaMotoresPag")
        rptVolumen.DataBind()
        Me.lblAnyoHidrologicoV.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenervolumenacumuladoV.Text = Me.obtenerVolumenAcumulado
        Me.lblobtenerNumlecturasV.Text = Me.obtenerNumLecturasV
        Me.lblobtenerTotNumLecturasV.Text = Me.obtenerTotNumLecturas("V", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaV.Text = Me.obtenerIntervaloFechasV()
        Me.lblobtenerNomElementoV.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoV.Text = ObtenerPorConcesion(lblVolMaxAnuLegV.Text, lblobtenervolumenacumuladoV.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionV.lblNumpaginasDatabind()
    End Sub

    Protected Sub lbtFiltroAceptarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltroAceptarH.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        Dim tipoelem As String = "H"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionH.lblPaginatext = "1"

        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegH.Text, chkFiltroNulasH.Checked, _
        txtFiltroFechaFinH.Text, txtFiltrofechaIniH.Text, txtFiltrarCodFuenteDatoH.Text, Session("FiltroH"), ucPaginacionH, chkReducirLecH.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaHorometros")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptHorometros.DataSource = dstElementos.Tables("TablaHorometrosPag")
        rptHorometros.DataBind()
        Me.lblAnyoHidrologicoH.Text = Me.obtenerAnyoHidrologicoEtiqueta()
        Me.lblobtenerNumLecturasH.Text = Me.obtenerNumLecturasH
        Me.lblobtenerVolumenAcumuladoHoras.Text = Me.obtenerVolumenAcumuladoHoras()
        Me.lblobtenervolumenacumuladoH.Text = Me.obtenerVolumenHorometroAcumulado
        Me.lblobtenerTotNumLecturasH.Text = Me.obtenerTotNumLecturas("H", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaH.Text = Me.obtenerIntervaloFechasH()
        Me.lblObtenerNomElementoH.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoH.Text = ObtenerPorConcesion(lblVolMaxAnuLegH.Text, lblobtenervolumenacumuladoH.Text)
        rellenarListasH()

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionH.lblNumpaginasDatabind()
    End Sub

    Protected Sub lbtFiltroCancelarH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltroCancelarH.Click
        'EGB21042008
        'Dim tipoelem As String = Me.txtDescripcionElementoMedida.Text.Substring(Me.txtDescripcionElementoMedida.Text.Length - 1, 1)
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)

        Dim tipoelem As String = "H"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionH.lblPaginatext = "1"
        txtFiltrofechaIniH.Text = ""
        txtFiltroFechaFinH.Text = ""
        txtFiltrarCodFuenteDatoH.Text = ""
        txtFiltroNRegH.Text = ""
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegH.Text, chkFiltroNulasH.Checked, _
        txtFiltroFechaFinH.Text, txtFiltrofechaIniH.Text, txtFiltrarCodFuenteDatoH.Text, Session("FiltroH"), ucPaginacionH, chkReducirLecH.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaHorometros")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptHorometros.DataSource = dstElementos.Tables("TablaHorometrosPag")
        rptHorometros.DataBind()
        Me.lblAnyoHidrologicoH.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenerNumLecturasH.Text = Me.obtenerNumLecturasH
        Me.lblobtenerVolumenAcumuladoHoras.Text = Me.obtenerVolumenAcumuladoHoras()
        Me.lblobtenervolumenacumuladoH.Text = Me.obtenerVolumenHorometroAcumulado()
        Me.lblobtenerTotNumLecturasH.Text = Me.obtenerTotNumLecturas("H", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaH.Text = Me.obtenerIntervaloFechasH()
        Me.lblObtenerNomElementoH.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoH.Text = ObtenerPorConcesion(lblVolMaxAnuLegH.Text, lblobtenervolumenacumuladoH.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionH.lblNumpaginasDatabind()
    End Sub

    Protected Sub lbtFiltroAceptarD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltroAceptarD.Click
        Dim tipoelem As String = "D"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        ucPaginacionD.lblPaginatext = "1"
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegD.Text, chkFiltroNulasD.Checked, _
        txtFiltroFechaFinD.Text, txtFiltroFechaIniD.Text, txtFiltrarCodFuenteDatoD.Text, Session("FiltroD"), ucPaginacionD, chkReducirLecD.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaDiferencial")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptDiferencial.DataSource = dstElementos.Tables("TablaDiferencialPag")

        rptDiferencial.DataBind()
        Me.lblAnyoHidrologicoD.Text = Me.obtenerAnyoHidrologicoEtiqueta()
        Me.lblobtenervolumenacumuladoD.Text = Me.obtenerVolumenDiferencialesAcumulado
        Me.lblobtenerNumlecturasD.Text = Me.obtenerNumLecturasD
        Me.lblobtenerTotNumLecturasD.Text = Me.obtenerTotNumLecturas("D", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaD.Text = Me.obtenerIntervaloFechasd()
        Me.lblobtenerNomElementoD.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoD.Text = ObtenerPorConcesion(lblVolMaxAnuLegD.Text, lblobtenervolumenacumuladoD.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionD.lblNumpaginasDatabind()

        rellenarListasD()
    End Sub

    Protected Sub lbtFiltroCancelarD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtFiltroCancelarD.Click
        Dim tipoelem As String = "D"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionD.lblPaginatext = "1"
        txtFiltroFechaIniD.Text = ""
        txtFiltroFechaFinD.Text = ""
        txtFiltrarCodFuenteDatoD.Text = ""
        'IPIM 10/12/2008:  Me ha dicho Juan Carlos que lo deje así, para que se calculen bien los informes por defecto chkFiltroNulasE.Checked = True
        txtFiltroNRegD.Text = ""
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegD.Text, chkFiltroNulasD.Checked, _
        txtFiltroFechaFinD.Text, txtFiltroFechaIniD.Text, txtFiltrarCodFuenteDatoD.Text, Session("FiltroD"), ucPaginacionD, chkReducirLecD.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaDiferencial")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptDiferencial.DataSource = dstElementos.Tables("TablaDiferencialPag")
        rptDiferencial.DataBind()
        Me.lblAnyoHidrologicoD.Text = Me.obtenerAnyoHidrologicoEtiqueta()
        Me.lblobtenervolumenacumuladoD.Text = Me.obtenerVolumenDiferencialesAcumulado
        Me.lblobtenerNumlecturasD.Text = Me.obtenerNumLecturasD
        Me.lblobtenerTotNumLecturasD.Text = Me.obtenerTotNumLecturas("D", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaD.Text = Me.obtenerIntervaloFechasd()
        Me.lblobtenerNomElementoD.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoD.Text = ObtenerPorConcesion(lblVolMaxAnuLegD.Text, lblobtenervolumenacumuladoD.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionD.lblNumpaginasDatabind()
    End Sub

    Protected Sub LecturasSeleccionadas(ByVal TipoElemento As String)
        Dim i As Integer
        Dim Fecha_medida As String
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text

        'RDf
        'Fecha: 10/9/2008
        'Cargar las lecturas seleccionadas en el check, y montar la condición
        'en una variable de Sesión
        AgregarCalendario(TipoElemento)
        Dim Contador As Integer
        Dim CondicionLecturas As String
        CondicionLecturas = " AND ( "

        Select Case TipoElemento

            Case "Q"
                'Selección de lecturas
                'crearDataSets_Elementos(TipoElemento, codigoPVYCR, Me.txtIdElementoMedida.Text)
                CrearDatasetDiferencial(TipoElemento, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
                txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
                FinalizarDatasetElementos(TipoElemento)
                Contador = rptAcequias.Items.Count - 1
                For i = 0 To Contador
                    Fecha_medida = dstElementos.Tables("TablaAcequias").Rows(i)("Fecha_medida").ToString
                    If CType(rptAcequias.Items(i).FindControl("chkAcequias"), CheckBox).Checked Then
                        CondicionLecturas += " (D.Fecha_medida='" + dstElementos.Tables("TablaAcequias").Rows(i)("Fecha_medida").ToString + "' AND D.Cod_Fuente_dato='" + dstElementos.Tables("TablaAcequias").Rows(i)("Cod_Fuente_dato").ToString + "'  AND d.TipoObtencionCaudal='" + dstElementos.Tables("TablaAcequias").Rows(i)("TipoObtencionCaudal").ToString + "') OR"

                    End If
                Next i
            Case "E"
                'Selección de lecturas
                'crearDataSets_Elementos(TipoElemento, codigoPVYCR, Me.txtIdElementoMedida.Text)
                CrearDatasetDiferencial(TipoElemento, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegE.Text, chkFiltroNulasE.Checked, _
                txtFiltroFechaFinE.Text, txtFiltroFechaIniE.Text, txtFiltrarCodFuenteDatoE.Text, Session("FiltroE"), ucPaginacionE, chkReducirLecE.Checked, dstElementos, sentenciaSel)
                FinalizarDatasetElementos(TipoElemento)
                Contador = rptEnergia.Items.Count - 1
                For i = 0 To Contador
                    Fecha_medida = dstElementos.Tables("TablaAlimentacion").Rows(i)("Fecha_medida").ToString
                    If CType(rptEnergia.Items(i).FindControl("chkEnergia"), CheckBox).Checked Then
                        CondicionLecturas += " (D.Fecha_medida='" + dstElementos.Tables("TablaAlimentacion").Rows(i)("Fecha_medida").ToString + "' AND D.Cod_Fuente_dato='" + dstElementos.Tables("TablaAlimentacion").Rows(i)("Cod_Fuente_dato").ToString + "') OR"

                    End If
                Next i

            Case "H"
                'Selección de lecturas
                'crearDataSets_Elementos(TipoElemento, codigoPVYCR, Me.txtIdElementoMedida.Text)
                CrearDatasetDiferencial(TipoElemento, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegH.Text, chkFiltroNulasH.Checked, _
                txtFiltroFechaFinH.Text, txtFiltrofechaIniH.Text, txtFiltrarCodFuenteDatoH.Text, Session("FiltroH"), ucPaginacionH, chkReducirLecH.Checked, dstElementos, sentenciaSel)
                FinalizarDatasetElementos(TipoElemento)
                Contador = rptHorometros.Items.Count - 1
                For i = 0 To Contador
                    Fecha_medida = dstElementos.Tables("TablaHorometros").Rows(i)("Fecha_medida").ToString
                    If CType(rptHorometros.Items(i).FindControl("chkHorometro"), CheckBox).Checked Then
                        CondicionLecturas += " (D.Fecha_medida='" + dstElementos.Tables("TablaHorometros").Rows(i)("Fecha_medida").ToString + "' AND D.Cod_Fuente_dato='" + dstElementos.Tables("TablaHorometros").Rows(i)("Cod_Fuente_dato").ToString + "') OR"
                    End If
                Next i
            Case "V"
                'Selección de lecturas
                'crearDataSets_Elementos(TipoElemento, codigoPVYCR, Me.txtIdElementoMedida.Text)
                CrearDatasetDiferencial(TipoElemento, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegV.Text, chkFiltroNulasV.Checked, _
                txtFiltroFechaFinV.Text, txtFiltroFechaIniV.Text, txtFiltrarCodFuenteDatoV.Text, Session("FiltroM"), ucPaginacionV, chkReducirlecV.Checked, dstElementos, sentenciaSel)
                FinalizarDatasetElementos(TipoElemento)
                Contador = rptVolumen.Items.Count - 1
                For i = 0 To Contador
                    Fecha_medida = dstElementos.Tables("TablaMotores").Rows(i)("Fecha_medida").ToString
                    If CType(rptVolumen.Items(i).FindControl("chkMotor"), CheckBox).Checked Then
                        CondicionLecturas += " (D.Fecha_medida='" + dstElementos.Tables("TablaMotores").Rows(i)("Fecha_medida").ToString + "' AND D.Cod_Fuente_dato='" + dstElementos.Tables("TablaMotores").Rows(i)("Cod_Fuente_dato").ToString + "') OR"

                    End If
                Next i
            Case "D"
                'Selección de lecturas
                'crearDataSets_Elementos(TipoElemento, codigoPVYCR, Me.txtIdElementoMedida.Text)
                CrearDatasetDiferencial(TipoElemento, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegD.Text, chkFiltroNulasD.Checked, _
                txtFiltroFechaFinD.Text, txtFiltroFechaIniD.Text, txtFiltrarCodFuenteDatoD.Text, Session("FiltroD"), ucPaginacionD, chkReducirLecD.Checked, dstElementos, sentenciaSel)
                FinalizarDatasetElementos(TipoElemento)
                Contador = rptDiferencial.Items.Count - 1
                For i = 0 To Contador
                    Fecha_medida = dstElementos.Tables("TablaDiferencial").Rows(i)("Fecha_medida").ToString
                    If CType(rptDiferencial.Items(i).FindControl("chkDiferencial"), CheckBox).Checked Then
                        CondicionLecturas += " (D.Fecha_medida='" + dstElementos.Tables("TablaDiferencial").Rows(i)("Fecha_medida").ToString + "' AND D.Cod_Fuente_dato='" + dstElementos.Tables("TablaDiferencial").Rows(i)("Cod_Fuente_dato").ToString + "') OR"

                    End If
                Next i

        End Select
        If CondicionLecturas = " AND ( " Then
            CondicionLecturas = ""
        Else
            CondicionLecturas = CondicionLecturas.Substring(0, CondicionLecturas.Length - 2) + ")"
        End If
        Session("LecturasSeleccionadas") = CondicionLecturas
    End Sub

    Protected Sub btnInformeA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInformeA.Click
        ' IPIM
        ' 29/04/2009
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("Q")
        'Se abre el informe
        nombreinforme = "ModificarLecturas.aspx?tipoElem=Q"
        ClientScript.RegisterStartupScript(Page.GetType, "generaExcel", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnComparativaCaudales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComparativaCaudales.Click
        ' RDF
        ' 11/09/2008
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("Q")
        'Se abre el informe
        'IPIM 06/03/2009: Elimino el filtro del querystring pq luego no se usa como tal (se usa la variable de session) y estaba dando problemas.                
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervalo.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNregQ.Text + "&filtroFechaIni=" + txtfiltroFechaIni.Text + "&FiltroFechaFin=" + txtFiltroFechaFin.Text + _
                    "&informe=&FiltroNulas=" + chkFiltroNulasQ.Checked.ToString + "&FiltrarCodFuenteDato=" + txtFiltrarCodFuentedato.Text + _
                    "&reducirLecturas=" & chkReducirLecQ.Checked
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub rptAcequias_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptAcequias.ItemDataBound
        ''RDf
        ''12/09/2008
        ''Evento onclick del check: selecciona y deselecciona una fila
        'If Not IsNothing(e.Item.FindControl("chkAcequias")) Then
        '    CType(e.Item.FindControl("chkAcequias"), CheckBox).Attributes.Add("onclick", "if (this.checked){ SeleccionarFila(this);} else{DeseleccionarFila(this)} event.cancelBubble=true;")
        'End If

        'IPIM 29/10/2008
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not IsNothing(e.Item.FindControl("chkAcequias")) Then
                CType(e.Item.FindControl("chkAcequias"), CheckBox).Attributes.Add("onclick", "clickTarea(this,'" & CType(e.Item.FindControl("Hid_Fecha_Medida"), HiddenField).Value & "','" & CType(e.Item.FindControl("Hid_Cod_Fuente_Dato"), HiddenField).Value & "');")
            End If
        End If

    End Sub

    Protected Sub rptEnergia_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptEnergia.ItemDataBound
        ''RDf
        ''12/09/2008
        ''Evento onclick del check: selecciona y deselecciona una fila
        'If Not IsNothing(e.Item.FindControl("chkEnergia")) Then
        '    CType(e.Item.FindControl("chkEnergia"), CheckBox).Attributes.Add("onclick", "if (this.checked){ SeleccionarFila(this);} else{DeseleccionarFila(this)} event.cancelBubble=true;")
        'End If

    End Sub

    Protected Sub rptVolumen_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptVolumen.ItemDataBound
        ''RDf
        ''12/09/2008
        ''Evento onclick del check: selecciona y deselecciona una fila
        'If Not IsNothing(e.Item.FindControl("chkMotor")) Then
        '    CType(e.Item.FindControl("chkMotor"), CheckBox).Attributes.Add("onclick", "if (this.checked){ SeleccionarFila(this);} else{DeseleccionarFila(this)} event.cancelBubble=true;")
        'End If

    End Sub

    Protected Sub rptHorometros_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptHorometros.ItemDataBound
        ''RDf
        ''12/09/2008
        ''Evento onclick del check: selecciona y deselecciona una fila
        'If Not IsNothing(e.Item.FindControl("chkHorometro")) Then
        '    CType(e.Item.FindControl("chkHorometro"), CheckBox).Attributes.Add("onclick", "if (this.checked){ SeleccionarFila(this);} else{DeseleccionarFila(this)} event.cancelBubble=true;")
        'End If
    End Sub

    Protected Sub btnComparativaCaudalesE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComparativaCaudalesE.Click
        ' RDF
        ' 11/09/2008
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("E")
        'Se abre el informe
        'IPIM 06/03/2009: Elimino el filtro del querystring pq luego no se usa como tal (se usa la variable de session) y estaba dando problemas.        
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervaloE.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNRegE.Text + "&filtroFechaIni=" + txtFiltroFechaIniE.Text + "&FiltroFechaFin=" + txtFiltroFechaFinE.Text + _
                    "&informe=&FiltroNulas=" + chkFiltroNulasE.Checked.ToString + "&FiltrarCodFuenteDato=" + txtFiltrarCodFuenteDatoE.Text + _
                    "&reducirLecturas=" & chkReducirLecE.Checked
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnComparativaCaudalesV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComparativaCaudalesV.Click
        ' RDF
        ' 11/09/2008
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("V")
        'Se abre el informe
        'IPIM 06/03/2009: Elimino el filtro del querystring pq luego no se usa como tal (se usa la variable de session) y estaba dando problemas.        
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervaloV.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNRegV.Text + "&filtroFechaIni=" + txtFiltroFechaIniV.Text + "&FiltroFechaFin=" + txtFiltroFechaFinV.Text + _
                    "&informe=&FiltroNulas=" + chkFiltroNulasV.Checked.ToString + "&FiltrarCodFuenteDato=" + txtFiltrarCodFuenteDatoV.Text + _
                    "&reducirLecturas=" & chkReducirlecV.Checked
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnComparativaCaudalesEXLS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComparativaCaudalesEXLS.Click
        ' RDF
        ' 11/09/2008
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("E")
        'Se abre el informe
        'IPIM 06/03/2009: Elimino el filtro del querystring pq luego no se usa como tal (se usa la variable de session) y estaba dando problemas.        
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervaloE.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNRegE.Text + "&filtroFechaIni=" + txtFiltroFechaIniE.Text + "&FiltroFechaFin=" + txtFiltroFechaFinE.Text + _
                    "&informe=xls" + "&FiltroNulas=" + chkFiltroNulasE.Checked.ToString + "&FiltrarCodFuenteDato=" + txtFiltrarCodFuenteDatoE.Text + _
                    "&reducirLecturas=" & chkReducirLecE.Checked
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnComparativaCaudalesVXLS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComparativaCaudalesVXLS.Click
        ' RDF
        ' 11/09/2008
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("V")
        'Se abre el informe
        'IPIM 06/03/2009: Elimino el filtro del querystring pq luego no se usa como tal (se usa la variable de session) y estaba dando problemas.        
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervaloV.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNRegV.Text + "&filtroFechaIni=" + txtFiltroFechaIniV.Text + "&FiltroFechaFin=" + txtFiltroFechaFinV.Text + _
                    "&informe=xls" + "&FiltroNulas=" + chkFiltroNulasV.Checked.ToString + "&FiltrarCodFuenteDato=" + txtFiltrarCodFuenteDatoV.Text + _
                    "&reducirLecturas=" & chkReducirlecV.Checked
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnComparativaCaudalesXLS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComparativaCaudalesXLS.Click
        ' RDF
        ' 11/09/2008
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("Q")
        'Se abre el informe
        'IPIM 06/03/2009: Elimino el filtro del querystring pq luego no se usa como tal (se usa la variable de session) y estaba dando problemas.        
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervalo.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNregQ.Text + "&filtroFechaIni=" + txtfiltroFechaIni.Text + "&FiltroFechaFin=" + txtFiltroFechaFin.Text + _
                    "&informe=xls" + "&FiltroNulas=" + chkFiltroNulasQ.Checked.ToString + "&FiltrarCodFuenteDato=" + txtFiltrarCodFuentedato.Text + _
                    "&reducirLecturas=" & chkReducirLecQ.Checked
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnInformeE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInformeE.Click
        ' IPIM
        ' 05/05/2009
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("E")
        'Se abre el informe
        nombreinforme = "ModificarLecturas.aspx?tipoElem=E"
        ClientScript.RegisterStartupScript(Page.GetType, "generaExcel", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnInformeH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInformeH.Click
        ' IPIM
        ' 07/09/2009
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("H")
        'Se abre el informe
        nombreinforme = "ModificarLecturas.aspx?tipoElem=H"
        ClientScript.RegisterStartupScript(Page.GetType, "generaExcel", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnInformeV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInformeV.Click
        ' IPIM
        ' 05/05/2009
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("V")
        'Se abre el informe
        nombreinforme = "ModificarLecturas.aspx?tipoElem=V"
        ClientScript.RegisterStartupScript(Page.GetType, "generaExcel", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnGrafico_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrafico.Click
        ' RDF
        ' 11/09/2008
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("Q")
        'Se abre el informe        
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervalo.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNregQ.Text + "&filtroFechaIni=" + txtfiltroFechaIni.Text + "&FiltroFechaFin=" + txtFiltroFechaFin.Text + _
                    "&FiltroNulas=" + chkFiltroNulasQ.Checked.ToString + "&grafico=si&FiltrarCodFuenteDato=" + txtFiltrarCodFuentedato.Text + _
                    "&reducirLecturas=" & chkReducirLecQ.Checked

        If lblobtenerNumLecturasQ.Text = 0 Then
            JavaScript.Alert(Page, "No se puede mostrar el gráfico porque no hay lecturas")
            Exit Sub
        End If
        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnGraficoE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraficoE.Click
        ' RDF
        ' 11/09/2008
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("E")
        'Se abre el informe
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervaloE.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNRegE.Text + "&filtroFechaIni=" + txtFiltroFechaIniE.Text + "&FiltroFechaFin=" + txtFiltroFechaFinE.Text + _
                    "&FiltroNulas=" + chkFiltroNulasE.Checked.ToString + "&grafico=si&FiltrarCodFuenteDato=" + txtFiltrarCodFuenteDatoE.Text + _
                    "&reducirLecturas=" & chkReducirLecE.Checked

        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnGraficoV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraficoV.Click
        ' RDF
        ' 11/09/2008
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("V")
        'Se abre el informe
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervaloV.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNRegV.Text + "&filtroFechaIni=" + txtFiltroFechaIniV.Text + "&FiltroFechaFin=" + txtFiltroFechaFinV.Text + _
                    "&FiltroNulas=" + chkFiltroNulasV.Checked.ToString + "&grafico=si&FiltrarCodFuenteDato=" + txtFiltrarCodFuenteDatoV.Text + _
                    "&reducirLecturas=" & chkReducirlecV.Checked

        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Function ObtenerCodigoInventario(ByVal CodigoCauce As String) As String

        Dim sql As String
        Dim CodigoInventario As String
        Dim daCauces As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        CodigoInventario = ""

        'RDF 17/09/2008
        'Esta función es necesaria para mostrar los DESTINOS de los putnos resaltados
        sql = " SELECT CodigoInventario90  FROM PVYCR_Cauces WHERE codigocauce='" & CodigoCauce & "'"

        daCauces.SelectCommand.CommandText = sql
        CodigoInventario = nullABlanco(daCauces.SelectCommand.ExecuteScalar())

        Return CodigoInventario


    End Function

    Protected Sub btnGraficoLecturas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraficoLecturas.Click
        ' RDF
        ' 11/09/2008
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("Q")
        'Se abre el informe
        'ncm 12/11/2008 nombreinforme = "../listados/GraficoLecturas.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervalo.SelectedItem.Value + "&FiltroNreg=" + txtFiltroNregQ.Text + "&filtroFechaIni=" + txtfiltroFechaIni.Text + "&FiltroFechaFin=" + txtFiltroFechaFin.Text + "&FiltroNulas=" + chkFiltroNulasQ.Checked.ToString + "&Filtro=" + Session("Filtro") + "&grafico=si"
        If txtfiltroFechaIni.Text <> "" Then
            nombreinforme = "../listados/GraficoLecturas.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervalo.SelectedItem.Value + "&FiltroNreg=" + txtFiltroNregQ.Text + "&filtroFechaIni=" + txtfiltroFechaIni.Text + "&FiltroFechaFin=" + txtFiltroFechaFin.Text + "&FiltroNulas=" + chkFiltroNulasQ.Checked.ToString + "&grafico=si"
        Else
            Dim fechas As String = obtenerIntervaloFechasQ()
            Dim fechaIni As String = fechas.Substring(6, 10).ToString
            Dim fechaFin As String = fechas.Substring(23, 10).ToString
            nombreinforme = "../listados/GraficoLecturas.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervalo.SelectedItem.Value + "&FiltroNreg=" + txtFiltroNregQ.Text + "&filtroFechaIni=" + fechaIni + "&FiltroFechaFin=" + fechaFin + "&FiltroNulas=" + chkFiltroNulasQ.Checked.ToString + "&grafico=si"
        End If

        If lblobtenerNumLecturasQ.Text = 0 Then
            JavaScript.Alert(Page, "No se puede mostrar el gráfico porque no hay lecturas")
            Exit Sub
        End If
        'IPIM 20/08/2009: Para probar los gráficos de Marcos    
        Dim filtro As String = "ID=" & lblIntervaloFechasQ.Text & "&NL=" & _
                lblobtenerNumLecturasQ.Text & "&TNL=" & lblobtenerTotNumLecturasQ.Text & "&TA=" & lblCaudalAcumuladoQ.Text
        nombreinforme &= "&" & filtro

        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub


    Protected Sub AceptarFiltroHorometro(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20080921
        Dim strFiltro As String
        Dim tipoelem As String = "H"
        Dim codigoPVYCR As String = Me.txtCodigoPVYCR.Text
        Dim Operador As Boolean

        strFiltro = ""

        If ddlFFuenteDatosH.Text <> "" Then
            'strFiltro = strFiltro + " AND D2.Cod_Fuente_Dato like '" + ddlFFuenteDatosH.Text + "' "
            'ncm 22/06/2009
            If ddlFFuenteDatosH.Text = "05" Then
                strFiltro = strFiltro + " AND D2.Cod_Fuente_Dato like '" + ddlFFuenteDatosH.Text + "' "
            Else
                strFiltro = strFiltro + "  AND (D2.Cod_Fuente_Dato like '" + ddlFFuenteDatosH.Text + "'  or (D2.Cod_Fuente_Dato like '05' and idincidenciavolumetrica in (10,11))) "
            End If
        End If

        If txtFFechaMedidaH.Text <> "" Then
            strFiltro = strFiltro + " AND  D2.Fecha_Medida between '" & txtFFechaMedidaH.Text & " 00:00:00' and '" & txtFFechaMedidaH.Text & " 23:59:59' "
        End If


        If txtFHoras.Text <> "" Then
            Operador = ((txtFHoras.Text.ToString.IndexOf("=") > -1) Or (txtFHoras.Text.ToString.IndexOf(">") > -1) Or (txtFHoras.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D2.HorasIntervalo" + IIf(Operador = False, "=", "") + Replace(txtFHoras.Text, ",", ".") + " "
        End If

        If txtFFuncionaH.Text <> "" Then
            strFiltro = strFiltro + " AND D2.Funciona like '" + txtFFuncionaH.Text + "' "
        End If

        If txtFObservacionesH.Text <> "" Then
            strFiltro = strFiltro + " AND D2.Observaciones like '%" + txtFObservacionesH.Text + "%' "
        End If

        If txtFFIncidenciaH.Text <> "" Then
            strFiltro = strFiltro + " AND D2.descripcion like '%" + txtFFIncidenciaH.Text + "%' "
        End If

        If txtFConsumoH.Text <> "" Then
            Operador = ((txtFConsumoH.Text.ToString.IndexOf("=") > -1) Or (txtFConsumoH.Text.ToString.IndexOf(">") > -1) Or (txtFConsumoH.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D2.ConsumoVolumetricoAdicional " + IIf(Operador = False, "=", "") + Replace(txtFConsumoH.Text, ",", ".") + " "
        End If

        If txtFReinicioH.Text <> "" Then
            Operador = ((txtFReinicioH.Text.ToString.IndexOf("=") > -1) Or (txtFReinicioH.Text.ToString.IndexOf(">") > -1) Or (txtFReinicioH.Text.ToString.IndexOf("<") > -1))
            strFiltro = strFiltro + " AND D2.ReinicioLecturaVolumetrica " + IIf(Operador = False, "=", "") + Replace(txtFReinicioH.Text, ",", ".") + " "
        End If



        Session("FiltroH") = strFiltro
        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, Me.txtIdElementoMedida.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, txtIdElementoMedida.Text, Page, txtFiltroNRegH.Text, chkFiltroNulasH.Checked, _
        txtFiltroFechaFinH.Text, txtFiltrofechaIniH.Text, txtFiltrarCodFuenteDatoH.Text, Session("FiltroH"), ucPaginacionH, chkReducirLecH.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaHorometros")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptHorometros.DataSource = dstElementos.Tables("TablaHorometrosPag")
        rptHorometros.DataBind()
        Me.lblAnyoHidrologicoH.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenerNumLecturasH.Text = Me.obtenerNumLecturasH
        Me.lblobtenerVolumenAcumuladoHoras.Text = Me.obtenerVolumenAcumuladoHoras()
        'Me.lblobtenervolumenacumuladoH.Text = "cvv" 'Me.obtenerNumLecturasH 'Me.obtenerVolumenHorometroAcumulado()
        Me.lblobtenervolumenacumuladoH.Text = Me.obtenerVolumenHorometroAcumulado
        Me.lblobtenerTotNumLecturasH.Text = Me.obtenerTotNumLecturas("H", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaH.Text = Me.obtenerIntervaloFechasH()
        Me.lblObtenerNomElementoH.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoH.Text = ObtenerPorConcesion(lblVolMaxAnuLegH.Text, lblobtenervolumenacumuladoH.Text)
        ucPaginacionH.lblNumpaginasDatabind()
    End Sub

    Protected Sub rellenarListasH()
        Dim dstFuenteDatos As New DataSet

        Try
            Dim sql As String

            If sentenciaSel.IndexOf("order") > -1 Then
                sentenciaSel = sentenciaSel.Substring(0, sentenciaSel.IndexOf("order"))
            End If

            'NCM 20081021 se quita el top de la select para que salgan todos los obtencion caudal que hay en las lecturas seleccionadas, ya 
            ' que sino no aparecian todos.
            If sentenciaSel.IndexOf("D.CodigoPVYCR") > -1 Then
                sentenciaSel = "select " & sentenciaSel.Substring(sentenciaSel.IndexOf("D2.CodigoPVYCR"))
            End If

            'RDF 20080921
            'En los filtros, mostrar solo los registros que aparecen en las lecturas

            sql = " SELECT DISTINCT consulta.Cod_Fuente_Dato FROM (" + sentenciaSel + ") as consulta "


            Dim daFuenteDatos As New SqlDataAdapter(sql, conexion)

            'RDF 20080716
            'Lista obtención caudal de los filtros
            'daFuenteDatos.SelectCommand.CommandText = "SELECT   TipoObtencionCaudal FROM PVYCR_TiposObtencionCaudal order by 1"
            daFuenteDatos.Fill(dstFuenteDatos, "TablaFuente")
            ddlFFuenteDatosH.DataSource = dstFuenteDatos.Tables("TablaFuente")
            ddlFFuenteDatosH.DataValueField = "Cod_Fuente_Dato"
            ddlFFuenteDatosH.DataBind()
            ddlFFuenteDatosH.Items.Insert(0, New ListItem("[Seleccionar]", ""))

        Catch ex As Exception

        End Try


    End Sub


    Protected Sub btnGraficoCurvas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraficoCurvas.Click
        'asignamos el dataset a una variable global
        Dim dtLectSel As New System.Data.DataTable
        Dim dt As DataTable = New System.Data.DataTable()
        Dim dst As DataSet = New System.Data.DataSet()
        Dim var As String

        'IPIM 02/02/2009: Ponemos la variable de que hemos entrado en Curvas Acequias pq lo necesitaremos en CrearTablaParaRepeater
        GraficoCurvas = True
        'Try
        AgregarCalendario("Q")
        'crearDataSets_Elementos("Q", Me.txtCodigoPVYCR.Text, Me.txtIdElementoMedida.Text)
        CrearDatasetDiferencial("Q", txtCodigoPVYCR.Text, txtIdElementoMedida.Text, Page, txtFiltroNregQ.Text, chkFiltroNulasQ.Checked, _
        txtFiltroFechaFin.Text, txtfiltroFechaIni.Text, txtFiltrarCodFuentedato.Text, Session("Filtro"), ucPaginacionA, chkReducirLecQ.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos("Q")
        Session("dst") = dstElementos.Tables("TablaAcequias")

        dt = Session("dst")

        LecturasSeleccionadas("Q")

        Dim dr() As System.Data.DataRow
        dtLectSel = dt.Clone()

        dr = dt.DefaultView.Table.Select
        If Session("LecturasSeleccionadas") <> "" Then
            var = " " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " "
            dr = dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")

            Dim fila As DataRow
            For Each fila In dr
                dtLectSel.Rows.Add(fila.ItemArray)
            Next
            Session("TablaCaudalesDesdeAcequias") = dtLectSel
        Else

            Session("TablaCaudalesDesdeAcequias") = dt
        End If


        'IPIM 11/12/2008
        'Page.RegisterClientScriptBlock("abreGrafica", "<script language=javascript>" & _
        '              "window.open(""CurvasAcequiasPreproduccionFlash.aspx?"" ,""grafico"", ""toolbar=no,menubar=no,top=200,left=250,height=500,width=650"");" & _
        '        "</script>")

        Page.RegisterClientScriptBlock("abreGrafica", "<script language=javascript>" & _
                      "window.open(""CurvasAcequiasPreproduccionFlash.aspx?"");" & _
                "</script>")
    End Sub

    Protected Sub CrearTablaParaRepeater(ByVal nombre As String, ByVal tipoElem As String)
        'IPIM 26/11/2008: Esto se añade para la paginación por el tema de ordenar las últimas 100 lecturas
        If GraficoCurvas = True And Not dstElementos.Tables(nombre & "Pag") Is Nothing Then 'Esto es para que funcione gráfico curvas acequias que vuelve a llamar a la función crearDataSets
            GraficoCurvas = False
            Exit Sub
        End If
        'If Not dstElementos.Tables(nombre & "Pag") Is Nothing Then      'Esto es para que funcione gráfico curvas acequias que vuelve a llamar a la función crearDataSets
        '    Exit Sub
        'End If

        Dim ucPaginacion As paginacion
        Select Case tipoElem
            Case "Q"
                ucPaginacion = ucPaginacionA
            Case "V"
                ucPaginacion = ucPaginacionV
            Case "E"
                ucPaginacion = ucPaginacionE
            Case "H"
                ucPaginacion = ucPaginacionH
            Case "D"
                ucPaginacion = ucPaginacionD
        End Select
        Dim i As Integer

        If dstElementos.Tables(nombre & "Pag") Is Nothing Then
            dstElementos.Tables.Add(nombre & "Pag")
            Dim columna As New DataColumn
            For Each columna In dstElementos.Tables(nombre).Columns
                With dstElementos.Tables(nombre & "Pag").Columns
                    .Add(New DataColumn(columna.ColumnName, columna.DataType))
                End With
            Next
        End If

        Dim dr() As System.Data.DataRow
        dr = dstElementos.Tables(nombre).DefaultView.Table.Select

        Dim fila As DataRow

        For Each fila In dr
            dstElementos.Tables(nombre & "Pag").Rows.Add(fila.ItemArray)
        Next


        Dim numFilasTotales As Integer = dstElementos.Tables(nombre & "Pag").Rows.Count

        'IPIM 03/02/2009
        For i = 0 To numFilasTotales - 1
            If i < (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize Then
                dstElementos.Tables(nombre & "Pag").Rows(0).Delete()
            Else
                Exit For
            End If
        Next

        For i = NumRegPorPag + 1 To dstElementos.Tables(nombre & "Pag").Rows.Count
            dstElementos.Tables(nombre & "Pag").Rows(NumRegPorPag).Delete()
        Next
    End Sub

    Protected Sub ObtenerEnvolventePunto(ByVal claveNodo As String, ByVal TipoPunto As String)


        'RDF 20080718
        '------------------------------------------------------------
        Dim sentenciaInsert = ""
        Dim conexionVisor As System.Data.SqlClient.SqlConnection
        conexionVisor = New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings("dsn"))
        Dim comandoVisor As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexionVisor)
        'If conexionVisor.State = ConnectionState.Closed Then conexionVisor.Open()
        utiles.Comprobar_Conexion_BD(Page, conexionVisor, True)
        comandoVisor.CommandText = "delete from TSesionVisorResaltar where idSesion='" & Session.SessionID & "'"
        comandoVisor.ExecuteNonQuery()
        comandoVisor.CommandText = "delete from TSesionVisorDes where idSesion='" & Session.SessionID & "'"
        comandoVisor.ExecuteNonQuery()
        'RDF 20080718
        '--------------------------------------

        If TipoPunto = "G" Then
            comandoVisor.CommandText = "insert into TSesionVisorResaltar(idSesion,Codigo,Capa,Tipo) values('" & Session.SessionID & "','" & claveNodo & "','Puntos','R')"
        Else
            comandoVisor.CommandText = "insert into TSesionVisorResaltar(idSesion,Codigo,Capa,Tipo) values('" & Session.SessionID & "','" & claveNodo & "','Puntos','O')"
        End If

        comandoVisor.ExecuteNonQuery()
        conexionVisor.Close()

    End Sub

    Protected Sub btnAceptarEDDiferencial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarEDDiferencial.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        Dim codigoPVYCR = Me.txtCodigoPVYCR.Text
    Dim tipoelem As String = "D"
    Dim cod_fuente_dato() As String = lblCodfuenteDatoD.Text.Split("-")


        'If conexion.State = ConnectionState.Closed Then conexion.Open()
    utiles.Comprobar_Conexion_BD(Page, conexion)


    comando.CommandText = "UPDATE PVYCR_DatosSuministros " & _
                           "SET [SuministromensualM3] = @Suministro " & _
                              ",[Observaciones] = @Observaciones " & _
                              " WHERE idElementoMedida = '" & lblidElemento.Text & "' and CodigoPVYCR = '" & codigoPVYCR & "' " & _
                              "and Fecha_Medida = '" & lblFechamedidaDSel.Text & "' and cod_fuente_dato='" & cod_fuente_dato(0).Replace(" ", "").ToString & "' "


        comando.Parameters.Clear()
        'Horas Intervalo
        If utiles.nullABlanco(txtEDSuministroD.Text) = "" Then
            comando.Parameters.AddWithValue("Suministro", DBNull.Value)
        Else
            comando.Parameters.AddWithValue("Suministro", Replace(txtEDSuministroD.Text, ".", ""))
        End If
        comando.Parameters.AddWithValue("Observaciones", utiles.BlancoANull(txtEDObservacionesD.Text))
        comando.ExecuteNonQuery()

        lblFechamedidaDSel.Text = ""
        pnlDiferencial.Visible = True
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionD.Visible = True
        pnlEDEnergia.Visible = False
        pnlEDVolumen.Visible = False
        pnlEDAcequias.Visible = False
        pnlEDHorometros.Visible = False
        pnlEDDiferencial.Visible = False

        txtEDSuministroD.Text = ""
        txtEDObservacionesD.Text = ""

        AgregarCalendario(tipoelem)
        'crearDataSets_Elementos(tipoelem, codigoPVYCR, lblidElemento.Text)
        CrearDatasetDiferencial(tipoelem, codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegD.Text, chkFiltroNulasD.Checked, _
        txtFiltroFechaFinD.Text, txtFiltroFechaIniD.Text, txtFiltrarCodFuenteDatoD.Text, Session("FiltroD"), ucPaginacionD, chkReducirLecD.Checked, dstElementos, sentenciaSel)
        FinalizarDatasetElementos(tipoelem)
        Session("dst") = dstElementos.Tables("TablaDiferencial")              'IPIM 20080912: Para pasarle el dataset entero a los informes

        'IPIM 25/11/2008: Esto se hace para que cargue en pantalla sólo los 100 últimos registros, aunque no lo hayan especificado
        rptDiferencial.DataSource = dstElementos.Tables("TablaDiferencialPag")
        rptDiferencial.DataBind()
        Me.lblAnyoHidrologicoD.Text = Me.obtenerAnyoHidrologico()
        Me.lblobtenerNumlecturasD.Text = Me.obtenerNumLecturasD
        Me.lblobtenervolumenacumuladoD.Text = Me.obtenerVolumenDiferencialesAcumulado
        Me.lblobtenerTotNumLecturasD.Text = Me.obtenerTotNumLecturas("D", codigoPVYCR, lblidElemento.Text)
        Me.lblIntervaloFechaD.Text = Me.obtenerIntervaloFechasd()
        Me.lblobtenerNomElementoD.Text = Me.obtenerNomElemento()
        Me.lblPorConsumidoD.Text = ObtenerPorConcesion(lblVolMaxAnuLegD.Text, lblobtenervolumenacumuladoD.Text)
        'IPIM 26/11/2008: Descomentamos para paginar
        ucPaginacionD.lblNumpaginasDatabind()
    End Sub

    Protected Sub btnCancelarEDDiferencial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarEDDiferencial.Click
        lblFechamedidaDSel.Text = ""
        lblCodfuenteDatoD.Text = ""
        pnlDiferencial.Visible = True
        pnlEDEnergia.Visible = False
        pnlEDVolumen.Visible = False
        pnlEDAcequias.Visible = False
        pnlEDHorometros.Visible = False
        pnlEDDiferencial.Visible = False


        txtEDSuministroD.Text = ""
        txtEDObservacionesD.Text = ""
    End Sub

    Protected Sub rptDiferencial_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptDiferencial.ItemCommand
        'EGB21042008
        'Dim codigoPVYCR As String = Me.txtDescripcionElementoMedida.Text.Substring(0, Me.txtDescripcionElementoMedida.Text.Length - 4)
        Dim codigoPVYCR = Me.txtCodigoPVYCR.Text

        Dim nombre As String
        Dim i As Integer
        Dim parametros() As String
        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Dim cod_fuente_dato As String = parametros(0)
                lblFechamedidaDSel.Text = parametros(1)
                'mostramos el panel de edición
                pnlDiferencial.Visible = False
                'IPIM 26/11/2008: Descomentamos para paginar
                ucPaginacionD.Visible = False
                pnlEDDiferencial.Visible = True
                pnlEDHorometros.Visible = False
                pnlEDVolumen.Visible = False
                pnlEDAcequias.Visible = False
                pnlEDEnergia.Visible = False

                creardatasetEd(lblidElemento.Text, codigoPVYCR, lblFechamedidaDSel.Text, cod_fuente_dato)

                If dstElementos.Tables("TablaEdDiferencial").Rows.Count > 0 Then
                    lblFechaMedidaD.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("Fecha_medida"))
                    lblCodfuenteDatoD.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("Cod_fuente_Dato")) & " - " & utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("DescFuenteDato"))
                    txtEDSuministroD.Text = utiles.nullABlanco(String.Format("{0:#,##0}", dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("SuministroMensualM3")))

                    rellenarListas(lblidElemento.Text, codigoPVYCR)

                    txtEDObservacionesD.Text = utiles.nullABlanco(dstElementos.Tables("TablaEdDiferencial").Rows(0).Item("Observaciones"))
                    nombre = checkNombreDiferencial()
                End If

            Case "borrar"
                utiles.Comprobar_Conexion_BD(Page, conexion)

                comando.CommandText = "delete from pvycr_datossuministros where codigoPVYCR='" & codigoPVYCR & "' and idElementoMedida = '" & _
                lblidElemento.Text & "' and fecha_medida='" & parametros(1) & "' and cod_fuente_dato='" & parametros(0) & "' "

                comando.ExecuteNonQuery()
                AgregarCalendario("D")
                'Me.crearDataSets_Elementos("D", codigoPVYCR, lblidElemento.Text)
                CrearDatasetDiferencial("D", codigoPVYCR, lblidElemento.Text, Page, txtFiltroNRegD.Text, chkFiltroNulasD.Checked, _
                txtFiltroFechaFinD.Text, txtFiltroFechaIniD.Text, txtFiltrarCodFuenteDatoD.Text, Session("FiltroD"), ucPaginacionD, chkReducirLecD.Checked, dstElementos, sentenciaSel)
                FinalizarDatasetElementos("D")
                Session("dst") = dstElementos.Tables("TablaDiferencial")
                'para las consultas e inma NCM
                rptDiferencial.DataSource = dstElementos.Tables("TablaDiferencialPag")
                rptDiferencial.DataBind()

                'IPIM 26/11/2008: Descomentamos para paginar
                ucPaginacionD.lblNumpaginasDatabind()
        End Select
    End Sub

    Protected Sub btnInformeD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInformeD.Click
        ' NCM
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("D")
        'Se abre el informe
        'Elimino el filtro del querystring pq luego no se usa.        
        nombreinforme = "../listados/InformeVolumenes.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.lblIntervaloFechaD.Text + "&FiltroNreg=" + txtFiltroNRegD.Text + "&filtroFechaIni=" + txtFiltroFechaIniD.Text + "&FiltroFechaFin=" + txtFiltroFechaFinD.Text + "&informe=xls" + "&FiltroNulas=" + chkFiltroNulasD.Checked.ToString
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnComparativaCaudalesDXLS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComparativaCaudalesDXLS.Click
        Dim nombreinforme As String
        'NCM
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("D")
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervaloD.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNRegD.Text + "&filtroFechaIni=" + txtFiltroFechaIniD.Text + "&FiltroFechaFin=" + txtFiltroFechaFinD.Text + _
                    "&informe=xls" + "&FiltroNulas=" + chkFiltroNulasD.Checked.ToString + "&FiltrarCodFuenteDato=" + txtFiltrarCodFuenteDatoD.Text + _
                    "&reducirLecturas=" & chkReducirLecD.Checked
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnComparativaCaudalesD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComparativaCaudalesD.Click
        ' NCM 27/03/2009
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("D")
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervaloD.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNRegD.Text + "&filtroFechaIni=" + txtFiltroFechaIniD.Text + "&FiltroFechaFin=" + txtFiltroFechaFinD.Text + _
                    "&informe=&FiltroNulas=" + chkFiltroNulasD.Checked.ToString + "&FiltrarCodFuenteDato=" + txtFiltrarCodFuenteDatoD.Text + _
                    "&reducirLecturas=" & chkReducirLecD.Checked
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnGraficoD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraficoD.Click
        ' NCM 27/03/2008
        Dim nombreinforme As String
        LecturasSeleccionadas("D")
        'Se abre el informe
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + _
                    Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.ddlIntervaloD.SelectedItem.Value + "&FiltroNreg=" + _
                    txtFiltroNRegD.Text + "&filtroFechaIni=" + txtFiltroFechaIniD.Text + "&FiltroFechaFin=" + txtFiltroFechaFinD.Text + _
                    "&FiltroNulas=" + chkFiltroNulasD.Checked.ToString + "&grafico=si&FiltrarCodFuenteDato=" + txtFiltrarCodFuenteDatoD.Text + _
                    "&reducirLecturas=" & chkReducirLecD.Checked
        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnSubirLecturas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubirLecturas.Click
        ' IPIM
        ' 04/05/2009
        Try
            Dim TargetPath = Server.MapPath("./ExcelTemp/Caudal/" & Path.GetFileName(txtUpload.PostedFile.FileName))
            txtUpload.PostedFile.SaveAs(TargetPath)

            Dim errorRespuesta As String = SICA_FuncionesLecturas.SubirArchivo(TargetPath, "Q", conexion, comando, Session("loginUsuario"), Session("dst"))
            If errorRespuesta <> "" Then
                JavaScript.Alert(Me, errorRespuesta)
            Else
                JavaScript.Alert(Me, "El archivo ha sido subido correctamente\nRecuerde que si ha modificado alguna clave primaria, ese registro no se ha actualizado\ny que la columna parcial no será modificada")
                ClientScript.RegisterStartupScript(Page.GetType, "Recargar", _
                "<script language=javascript>window.parent.document.getElementById('iframeDetalle').src='" & Request.ServerVariables("URL") & "?" & Request.ServerVariables("QUERY_STRING") & "'</script>")
            End If
            
        Catch ex As Exception
            If InStr(ex.Message, "FK_PVYCR_DatosAcequias_PVYCR_TiposObtencionCaudal") > 0 Then
                JavaScript.Alert(Me, "Ha ocurrido un error y el archivo no se ha podido subir.\nEl Tipo de Obtención de Caudal no es correcto.")
            ElseIf InStr(ex.Message, "FK_PVYCR_DatosAcequias_PVYCR_Acequias_Regimenes") > 0 Then
                JavaScript.Alert(Me, "Ha ocurrido un error y el archivo no se ha podido subir.\nEl Régimen curva no es correcto.")
            ElseIf InStr(ex.Message, " no pertenece a la tabla") > 0 Then
                JavaScript.Alert(Me, "Ha ocurrido un error y el archivo no se ha podido subir.\nEl archivo que está intentando subir no tiene el formato correcto.")
            Else
                JavaScript.Alert(Me, "Ha ocurrido un error y el archivo no se ha podido subir\n" & Replace(Replace(ex.Message, """", ""), "'", ""))
            End If
        End Try
    End Sub


    Protected Sub btnSubirLecturasE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubirLecturasE.Click
        ' IPIM
        ' 05/05/2009
        Try
            Dim TargetPath = Server.MapPath("./ExcelTemp/Alimentacion/" & Path.GetFileName(txtUploadE.PostedFile.FileName))
            txtUploadE.PostedFile.SaveAs(TargetPath)

            Dim errorRespuesta As String = SICA_FuncionesLecturas.SubirArchivo(TargetPath, "E", conexion, comando, Session("loginUsuario"), Session("dst"))
            If errorRespuesta <> "" Then
                JavaScript.Alert(Me, errorRespuesta)
            Else
                JavaScript.Alert(Me, "El archivo ha sido subido correctamente\nRecuerde que si ha modificado alguna clave primaria, ese registro no se ha actualizado\ny que la columna parcial no será modificada")
                ClientScript.RegisterStartupScript(Page.GetType, "Recargar", _
                "<script language=javascript>window.parent.document.getElementById('iframeDetalle').src='" & Request.ServerVariables("URL") & "?" & Request.ServerVariables("QUERY_STRING") & "'</script>")
            End If
            
        Catch ex As Exception
            If InStr(ex.Message, " no pertenece a la tabla") > 0 Then
                JavaScript.Alert(Me, "Ha ocurrido un error y el archivo no se ha podido subir.\nEl archivo que está intentando subir no tiene el formato correcto.")
            Else
                JavaScript.Alert(Me, "Ha ocurrido un error y el archivo no se ha podido subir\n" & Replace(Replace(ex.Message, """", ""), "'", ""))
            End If
        End Try
    End Sub

    Protected Sub btnSubirLecturasV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubirLecturasV.Click
        ' IPIM
        ' 05/05/2009
        Try
            Dim TargetPath = Server.MapPath("./ExcelTemp/Motores/" & Path.GetFileName(txtUploadV.PostedFile.FileName))
            txtUploadV.PostedFile.SaveAs(TargetPath)

            Dim errorRespuesta As String = SICA_FuncionesLecturas.SubirArchivo(TargetPath, "V", conexion, comando, Session("loginUsuario"), Session("dst"))
            If errorRespuesta <> "" Then
                JavaScript.Alert(Me, errorRespuesta)
            Else
                JavaScript.Alert(Me, "El archivo ha sido subido correctamente\nRecuerde que si ha modificado alguna clave primaria, ese registro no se ha actualizado\ny que la columna parcial no será modificada")
                ClientScript.RegisterStartupScript(Page.GetType, "Recargar", _
                "<script language=javascript>window.parent.document.getElementById('iframeDetalle').src='" & Request.ServerVariables("URL") & "?" & Request.ServerVariables("QUERY_STRING") & "'</script>")
            End If

        Catch ex As Exception
            If InStr(ex.Message, " no pertenece a la tabla") > 0 Then
                JavaScript.Alert(Me, "Ha ocurrido un error y el archivo no se ha podido subir.\nEl archivo que está intentando subir no tiene el formato correcto.")
            Else
                JavaScript.Alert(Me, "Ha ocurrido un error y el archivo no se ha podido subir\n" & Replace(Replace(ex.Message, """", ""), "'", ""))
            End If
        End Try
    End Sub

    Protected Sub btnModificarLecturasH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificarLecturasH.Click
        ' IPIM
        ' 05/05/2009
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("H")
        'Se abre el informe
        Dim codigopvycr As String = Me.txtCodigoPVYCR.Text
        nombreinforme = "ModificarLecturas.aspx?tipoElem=H&codigoPVYCR=" & codigopvycr
        ClientScript.RegisterStartupScript(Page.GetType, "generaExcel", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnSubirLecturasH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubirLecturasH.Click
        ' IPIM
        ' 05/05/2009
        Try
            Dim TargetPath = Server.MapPath("./ExcelTemp/Horometros/" & Path.GetFileName(txtUploadH.PostedFile.FileName))
            txtUploadH.PostedFile.SaveAs(TargetPath)


            Dim errorRespuesta As String = SICA_FuncionesLecturas.SubirArchivo(TargetPath, "H", conexion, comando, Session("loginUsuario"), Session("dst"))
            If errorRespuesta <> "" Then
                JavaScript.Alert(Me, errorRespuesta)
            Else
                JavaScript.Alert(Me, "El archivo ha sido subido correctamente\nRecuerde que si ha modificado alguna clave primaria, ese registro no se ha actualizado\ny que la columna parcial no será modificada")
                ClientScript.RegisterStartupScript(Page.GetType, "Recargar", _
                "<script language=javascript>window.parent.document.getElementById('iframeDetalle').src='" & Request.ServerVariables("URL") & "?" & Request.ServerVariables("QUERY_STRING") & "'</script>")
            End If

        Catch ex As Exception
            If InStr(ex.Message, " no pertenece a la tabla") > 0 Then
                JavaScript.Alert(Me, "Ha ocurrido un error y el archivo no se ha podido subir.\nEl archivo que está intentando subir no tiene el formato correcto.")
            Else
                JavaScript.Alert(Me, "Ha ocurrido un error y el archivo no se ha podido subir\n" & Replace(Replace(ex.Message, """", ""), "'", ""))
            End If
        End Try
    End Sub

    Protected Sub btnModificarLecturas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificarLecturas.Click
        ' IPIM 12/05/2009
        Dim nombreinforme As String
        LecturasSeleccionadas("Q")
        nombreinforme = "../listados/InformeVolumenes.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.lblIntervaloFechasQ.Text + "&FiltroNreg=" + txtFiltroNregQ.Text + "&filtroFechaIni=" + txtfiltroFechaIni.Text + "&FiltroFechaFin=" + txtFiltroFechaFin.Text + "&informe=xls" + "&FiltroNulas=" + chkFiltroNulasQ.Checked.ToString
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnModificarLecturasPlano_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificarLecturasPlano.Click
        ' IPIM
        ' 29/04/2009
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("Q")
        'Se abre el informe
        Dim codigopvycr As String = Me.txtCodigoPVYCR.Text
        nombreinforme = "ModificarLecturas.aspx?tipoElem=Q&codigoPVYCR=" & codigopvycr

        ClientScript.RegisterStartupScript(Page.GetType, "generaExcel", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnModificarLecturasV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificarLecturasV.Click
        ' IPIM 12/05/2009
        Dim nombreinforme As String
        LecturasSeleccionadas("V")
        nombreinforme = "../listados/InformeVolumenes.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.lblIntervaloFechaV.Text + "&FiltroNreg=" + txtFiltroNRegV.Text + "&filtroFechaIni=" + txtFiltroFechaIniV.Text + "&FiltroFechaFin=" + txtFiltroFechaFinV.Text + "&informe=xls" + "&FiltroNulas=" + chkFiltroNulasV.Checked.ToString
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnModificarLecturasVPlano_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificarLecturasVPlano.Click
        ' IPIM
        ' 05/05/2009
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("V")
        'Se abre el informe
        Dim codigopvycr As String = Me.txtCodigoPVYCR.Text
        nombreinforme = "ModificarLecturas.aspx?tipoElem=V&codigoPVYCR=" & codigopvycr
        ClientScript.RegisterStartupScript(Page.GetType, "generaExcel", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnModificarLecturasE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificarLecturasE.Click
        ' IPIM 12/05/2009 
        Dim nombreinforme As String
        LecturasSeleccionadas("E")
        nombreinforme = "../listados/InformeVolumenes.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + Request.QueryString("nodoclave").ToString + "&filtrointervalo=" + Me.lblIntervaloFechaE.Text + "&FiltroNreg=" + txtFiltroNRegE.Text + "&filtroFechaIni=" + txtFiltroFechaIniE.Text + "&FiltroFechaFin=" + txtFiltroFechaFinE.Text + "&informe=xls" + "&FiltroNulas=" + chkFiltroNulasE.Checked.ToString
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnModificarLecturasEPlano_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificarLecturasEPlano.Click
        ' IPIM
        ' 05/05/2009
        Dim nombreinforme As String
        'Se cargan en la variable de sesión las lecturas seleccionadas
        LecturasSeleccionadas("E")
        'Se abre el informe
        Dim codigopvycr As String = Me.txtCodigoPVYCR.Text
        nombreinforme = "ModificarLecturas.aspx?tipoElem=E&codigoPVYCR=" & codigopvycr
        ClientScript.RegisterStartupScript(Page.GetType, "generaExcel", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub
    Protected Function VisibleSiAdmin() As String
        'IPIM 07/07/2009
        If Session("idperfil") = 1 Or Session("idPerfil") = 14 Then
            Return "block"
        Else
            Return "none"
        End If
    End Function
    'Protected Function QuitarRegistrosSegunFuenteDato(ByVal tipoElemento As String) As String
    '    'ncm 13/09/2009
    '    'Recorreremos el dataset correspondiente y eliminaremos los registros
    '    'que tengan cod_fuente_dato <> 05 e incidencia (6 o 7 en el caso de los motores y 2 o 3 en el casod de alimentación)
    '    Dim i As Int16 = 0
    '    Dim contador As Integer
    '    Select Case tipoElemento
    '        Case "V"
    '            'comprobamos si la columna borrarFila ya existe.
    '            If Not dstElementos.Tables("TablaMotores").Columns.Contains("BorrarFila") Then
    '                dstElementos.Tables("TablaMotores").Columns.Add("BorrarFila")
    '            End If

    '            contador = dstElementos.Tables("TablaMotores").Rows.Count - 1
    '            For i = 0 To contador
    '                If dstElementos.Tables("TablaMotores").Rows(i).Item("cod_fuente_dato").ToString <> "05" And _
    '                   (dstElementos.Tables("TablaMotores").Rows(i).Item("idIncidenciavolumetrica").ToString = "6" Or _
    '                    dstElementos.Tables("TablaMotores").Rows(i).Item("idIncidenciavolumetrica").ToString = "7") Then

    '                    If dstElementos.Tables("TablaMotores").Select("cod_fuente_dato = '05' and (idincidenciavolumetrica = 6 or idincidenciavolumetrica = 7)").Length <> 0 Then
    '                        dstElementos.Tables("TablaMotores").Rows(i).Item("BorrarFila") = "S"
    '                    End If
    '                Else
    '                    dstElementos.Tables("TablaMotores").Rows(i).Item("BorrarFila") = "N"
    '                End If
    '            Next
    '            Dim dt As DataTable
    '            dt = dstElementos.Tables("TablaMotores").Copy
    '            Dim filas As Integer = dt.Rows.Count - 1
    '            For i = 0 To filas
    '                If utiles.nullABlanco(dt.Rows(i).Item("BorrarFila")) = "S" Then
    '                    dstElementos.Tables("TablaMotores").Rows(i).Delete()
    '                End If
    '            Next

    '        Case "E"
    '            If Not dstElementos.Tables("TablaAlimentacion").Columns.Contains("BorrarFila") Then
    '                dstElementos.Tables("TablaAlimentacion").Columns.Add("BorrarFila")
    '            End If

    '            contador = dstElementos.Tables("TablaAlimentacion").Rows.Count - 1
    '            For i = 0 To contador
    '                If dstElementos.Tables("TablaAlimentacion").Rows(i).Item("cod_fuente_dato").ToString <> "05" And _
    '                   (dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaelectrica").ToString = "2" Or _
    '                    dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaelectrica").ToString = "3") Then

    '                    If dstElementos.Tables("TablaAlimentacion").Select("cod_fuente_dato = '05' and (idincidenciavolumetrica = 2 or idincidenciavolumetrica = 3)").Length <> 0 Then
    '                        dstElementos.Tables("TablaAlimentacion").Rows(i).Item("BorrarFila") = "S"
    '                    End If

    '                End If
    '            Next
    '            Dim dt As DataTable
    '            dt = dstElementos.Tables("TablaAlimentacion").Copy
    '            Dim filas As Integer = dt.Rows.Count - 1
    '            For i = 0 To filas
    '                If utiles.nullABlanco(dt.Rows(i).Item("BorrarFila")) = "S" Then
    '                    dstElementos.Tables("TablaAlimentacion").Rows(i).Delete()
    '                End If
    '            Next
    '    End Select

    'End Function
    Protected Function ObtenerConcesion(ByVal CodigoCauce As String) As String
        Dim sql As String
        Dim Concesion As Integer
        Dim daConcesion As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)


        'RDF 17/09/2008
        'Esta función es necesaria para mostrar los DESTINOS de los putnos resaltados
        sql = " SELECT volumenMaximoAnuallegal_m3  FROM PVYCR_Cauces WHERE codigocauce='" & CodigoCauce & "'"

        daConcesion.SelectCommand.CommandText = sql
        Concesion = nullACero(daConcesion.SelectCommand.ExecuteScalar())

        Return String.Format("{0:#,##0.##}", Concesion)
    End Function
    Protected Function ObtenerPorConcesion(ByVal Concesion As Integer, ByVal Acumulado As String) As String
        Dim Porcentaje As Decimal = 0
        'comprobamos que el acumulado no sea 0 (*)
        If Acumulado.Contains("*") = True Then
            Acumulado = Convert.ToDecimal(Acumulado.Replace(" (*)", ""))
        End If
        If Acumulado = "0" Or Concesion = 0 Then
            Return 0
        Else
            Porcentaje = (100 * Convert.ToDecimal(Acumulado) / Concesion)
            Return String.Format("{0:#,##0.##}", utiles.nullACero(Porcentaje))
        End If
    End Function

    Protected Sub btnDescargarArchivoQ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDescargarArchivoQ.Click
        Dim nombreinforme As String
        LecturasSeleccionadas("Q")
        Dim codigopvycr As String = Me.txtCodigoPVYCR.Text
        nombreinforme = "ModificarLecturas.aspx?tipoElem=Q&codigoPVYCR=" & codigopvycr & "&SubirLecturas=si"
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnDescargarArchivoV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDescargarArchivoV.Click
        Dim nombreinforme As String
        LecturasSeleccionadas("V")
        Dim codigopvycr As String = Me.txtCodigoPVYCR.Text
        nombreinforme = "ModificarLecturas.aspx?tipoElem=V&codigoPVYCR=" & codigopvycr & "&SubirLecturas=si"
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnDescargarArchivoE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDescargarArchivoE.Click
        Dim nombreinforme As String
        LecturasSeleccionadas("E")
        Dim codigopvycr As String = Me.txtCodigoPVYCR.Text
        nombreinforme = "ModificarLecturas.aspx?tipoElem=E&codigoPVYCR=" & codigopvycr & "&SubirLecturas=si"
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnDescargarArchivoH_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDescargarArchivoH.Click
        Dim nombreinforme As String
        LecturasSeleccionadas("H")
        Dim codigopvycr As String = Me.txtCodigoPVYCR.Text
        nombreinforme = "ModificarLecturas.aspx?tipoElem=H&codigoPVYCR=" & codigopvycr & "&SubirLecturas=si"
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub
    Protected Sub AgregarCalendario(ByVal tipoelemen As String)
        If tipoelemen = "Q" Then
            imgCalFechaIniQ.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtfiltroFechaIni.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinQ.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFin.ClientID & "'),'dd/mm/yyyy');")
            imgFfechamedida.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedida.ClientID & "'),'dd/mm/yyyy');")
        ElseIf tipoelemen = "V" Then
            imgCalFechaIniV.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniV.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinV.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinV.ClientID & "'),'dd/mm/yyyy');")
            imgFfechamedidaM.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedidaM.ClientID & "'),'dd/mm/yyyy');")

        ElseIf tipoelemen = "E" Then
            imgCalFechaIniE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniE.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinE.ClientID & "'),'dd/mm/yyyy');")
            imgFfechamedidaE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedidaE.ClientID & "'),'dd/mm/yyyy');")
        ElseIf tipoelemen = "H" Then
            ImgCalFechaIniH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltrofechaIniH.ClientID & "'),'dd/mm/yyyy');")
            ImgCalFechaFinH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinH.ClientID & "'),'dd/mm/yyyy');")
            imgFFechaH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaMedidaH.ClientID & "'),'dd/mm/yyyy');")
        ElseIf tipoelemen = "D" Then
            imgCalFechaIniD.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniD.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinD.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinD.ClientID & "'),'dd/mm/yyyy');")
            imgFfechamedidaD.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedidaD.ClientID & "'),'dd/mm/yyyy');")

        End If
    End Sub

    Protected Sub FinalizarDatasetElementos(ByVal tipo As String)
        If tipo = "Q" Then
            obtenerVolumenDiferencial("Q", dstElementos.Tables("TablaAcequias"), Page, mensaje_final)
            obtenerCaudalAcumulado()
            If chkReducirLecQ.Checked Then
                'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
                Dim dstNuevo As DataSet
                dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaAcequias", "Caudal_M3s", 5)
                dstElementos = dstNuevo.Copy
            End If

            CrearTablaParaRepeater("TablaAcequias", "Q")
        ElseIf tipo = "E" Then
            obtenerVolumenDiferencial("E", dstElementos.Tables("TablaAlimentacion"), Page, mensaje_final)
            obtenerVolumenElectricoAcumulado()
            If chkReducirLecE.Checked Then
                'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
                Dim dstNuevo As DataSet
                dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaAlimentacion", "Total_Kwh", 5)
                dstElementos = dstNuevo.Copy
            End If

            CrearTablaParaRepeater("TablaAlimentacion", "E")
        ElseIf tipo = "V" Then
            obtenerVolumenDiferencial("V", dstElementos.Tables("TablaMotores"), Page, mensaje_final)
            'obtenerVolumenDiferencial("V")
            obtenerVolumenAcumulado()
            If chkReducirlecV.Checked Then
                'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
                Dim dstNuevo As DataSet
                dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaMotores", "LecturaContador_M3", 5)
                dstElementos = dstNuevo.Copy
            End If

            CrearTablaParaRepeater("TablaMotores", "V")
        ElseIf tipo = "H" Then
            obtenerVolumenDiferencial("H", dstElementos.Tables("TablaHorometros"), Page, mensaje_final)
            obtenerVolumenHorometroAcumulado()

            If chkReducirLecH.Checked Then
                'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
                Dim dstNuevo As DataSet
                dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaHorometros", "HorasIntervalo", 5)
                dstElementos = dstNuevo.Copy
            End If

            CrearTablaParaRepeater("TablaHorometros", "H")
        ElseIf tipo = "D" Then
            obtenerVolumenDiferencial("D", dstElementos.Tables("TablaDiferencial"), Page, mensaje_final)
            'obtenerCaudalAcumulado()
            If chkReducirLecD.Checked Then
                'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
                Dim dstNuevo As DataSet
                dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaDiferencial", "suministromensualm3", 5)
                dstElementos = dstNuevo.Copy
            End If

            CrearTablaParaRepeater("TablaDiferencial", "D")
        End If

  End Sub
  Protected Function VerTipoObtencionCaudal(ByVal elDataItem As DataRowView) As String
    Dim sentencia As String = "select descripcion from PVYCR_tiposObtencioncaudal where tipoobtencioncaudal='" & elDataItem("TipoObtencionCaudal") & "'"
    utiles.Comprobar_Conexion_BD(Page, conexion)
    daElementos.SelectCommand.CommandText = sentencia
    Dim descripcion As String = daElementos.SelectCommand.ExecuteScalar()
    Return descripcion
  End Function
  Protected Function VerCodFuenteDato(ByVal elDataItem As DataRowView) As String
    Select Case elDataItem("cod_fuente_dato")
      Case "01"
        Return "Servicio de Aforos / Guardería Fluvial"
      Case "02"
        Return "SAIH Segura"
      Case "03"
        Return "INM (Instituto Nacional de Meteorología)"
      Case "04"
        Return "ITGE (Instituto Tecnológico y Geominero de España)"
      Case "05"
        Return "PVYCR (Tragsa)"
      Case "06"
        Return "BD1 (base de datos del 1º PHC) - Of. Planificación"
      Case "07"
        Return "Explotación ATS"
      Case "08"
        Return "CEDEX - Anuarios de Aforos"
      Case "09"
        Return "Servicio de Hidrología - Parte Oficial (Comisaría)"
      Case "10"
        Return "Datos del titular"
      Case "11"
        Return "Factura consumo (ejem. Iberdrola)"
      Case "12"
        Return "Sensores SICA por SAIH"
      Case "13"
        Return "Telemedida SICA"
      Case "14"
        Return "Área de Explotación del Trasvase"
      Case "15"
        Return "Plan Sequía Cuenca Segura 2007"
      Case "16"
        Return "Mancomunidad de los Canales del Taibilla"
    End Select

    End Function
    Protected Function ObtenerInscripcion(ByVal CodigoCauce As String) As String

        Dim sql As String
        Dim Inscripcion As String
        Dim daCauces As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Inscripcion = ""

        'RDF 17/09/2008
        'Esta función es necesaria para mostrar los DESTINOS de los putnos resaltados
        sql = " SELECT INSCRIPCION  FROM PVYCR_Cauces WHERE codigocauce='" & CodigoCauce & "'"

        daCauces.SelectCommand.CommandText = sql
        Inscripcion = nullABlanco(daCauces.SelectCommand.ExecuteScalar())

        Return Inscripcion


    End Function
  
End Class
