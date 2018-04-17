Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports System.Net


Partial Class SICAH_Agrupaciones_Detalle
    Inherits System.Web.UI.Page
    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim conexionDBSica As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_dbsica"))
    Dim conexionU As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim daAgrupaciones As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionDBSica)
    Dim daTipoPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionU)
    Dim daPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstPuntos As DataSet = New System.Data.DataSet()
    Dim IdRaacs As Integer
    Public ids As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Response.Write("L=" & Session("loginUsuario"))
            Try
                If (Session("perfilCHS").ToString().Contains("SICA") = False) Then
                    Response.Redirect("http://sica.chsegura.es")
                End If

            Catch
                Response.Redirect("http://sica.chsegura.es")
            End Try
        End If


        If Not IsPostBack Then
            DDL_Periodo_Informe.Items.Add("Diario")
            DDL_Periodo_Informe.Items.Add("Mensual")
            DDL_Periodo_Informe.Items.Add("Anual")
            DDL_Periodo_Informe.SelectedIndex = 1
            'DDL_Formato_Informe.Items.Add("HTML")
            DDL_Formato_Informe.Items.Add("PDF")
            DDL_Formato_Informe.Items.Add("XLS")
            DDL_Formato_Informe.Items.Add("DOC")
            'DDL_Formato_Informe.Items.Add("CSV")
            DDL_Formato_Informe.SelectedIndex = 0

            Dim id As Integer
            If Request.QueryString("Nombre") Is Nothing And Request.QueryString("RAAC") Is Nothing Then
                pnlAgrupacionConInscripcion.Visible = False
                PNL_Segunda_y_Tercerafila.Visible = False
                PNL_Lista_de_excedidos.Visible = True

                'Carga_Grid_Excedidos()
                'Se obtiene la lista de puntos pertenecientes a la Agrupación
                'ElseIf Request.QueryString("Nombre") Is Nothing Or Request.QueryString("RAAC") <> "1" Then
                '    pnlAgrupacionConInscripcion.Visible = False
                '    pnlAgrupacionesSinInscrip.Visible = True
            Else

                pnlAgrupacionConInscripcion.Visible = True
                id = Request.QueryString("idAgrupacion")
                ids = id

                'IdRaacs = CInt(Request.QueryString("RAAC").ToString())
                'txtDescripcion.Text = ObtenerDescripcionAgrupacion(id)
                Dim nombreAgrupacion As String = Request.QueryString("Nombre")

                Dim SICA_Log As SicaSegura.SICA_Log = New SicaSegura.SICA_Log()
                SICA_Log.Set_APP("SICASEGURA")
                SICA_Log.Set_USER(Session("loginUsuario"))
                SICA_Log.Set_PROFILE(Session("perfilCHS"))
                SICA_Log.Set_ESPAGINA("1")
                SICA_Log.Set_ESINFORME("0")
                SICA_Log.Set_DISPOSITIVO(Request.Browser.Platform.ToString())
                SICA_Log.acceso_pagina("Agrupacíon_detalle#" & nombreAgrupacion & "#" & Request.QueryString("nodotexto"))

                'RAACS
                ClientScript.RegisterStartupScript(Page.GetType, "Cargar2", "<script>document.getElementById('iframeAdmin').src='Agrupaciones_DatosAdmin.aspx?Nombre=" & nombreAgrupacion & "&RAAC=" & Request.QueryString("RAAC") & "';</script>")
                If (Request.QueryString("RAAC") Is Nothing) Then
                    PNL_Segunda_y_Tercerafila.Visible = False
                Else

                    Dim file As String = Request.QueryString("Nombre") & ".pdf"
                    Dim carpeta As String = "ruta_ficheros_alberca"
                    LBL_DescargaHoja.Text = "<a href='agrupaciones/download.aspx?carpeta=" & carpeta & "&file=" & file & "' target='_blanck'><font color='#ffffff'>Información cartográfica<img src='agrupaciones/images/pdf.png' height='18'></font></a>"

                    Dim parametrosInforme As String = "inscripcion=" & Request.QueryString("Nombre") & "&filtrofechaIni=01/10/2011&filtrofechaFin=01/10/2012&filtroIntervalo=" & DDL_Periodo_Informe.SelectedValue.ToString().Substring(0, 1) & "&nodoclave=&ClaveNodoTipo=&nodotexto=&FiltroNulas=false&FiltrarCodFuenteDato=&FiltroNreg=&informe=xls&LecturasSeleccionadas=&reducirLecturas=false"
                    Dim Inscripcion As String = Request.QueryString("Nombre")
                    'LIT_Librodig.Text = "<a href='./agrupaciones/informe/ListadoCaudalesFiltrados.aspx?" & parametrosInforme & "' target='_blank'><img src='./agrupaciones/images/download.gif'></a>&nbsp;&nbsp;&nbsp;<a href='./agrupaciones/informes/consumos.aspx?SCV=SI&DIAS=M&NUMPAL=" & Request.QueryString("Nombre") & "' target='_blank'>·</a> &nbsp;"
                    LIT_Librodig.Text += "<a href='./agrupaciones/informes/libroDig.pdf' target='_blank'><font color='#ffffff'>Libro Digital <img src='agrupaciones/images/pdf.png' height='18'></font></a><a target='new' href='../librodigital/informes.aspx?SCV=SI&Numpal=" & Inscripcion & "'></a>"

                    PNL_Segunda_y_Tercerafila.Visible = True
                    'SICA
                    ClientScript.RegisterStartupScript(Page.GetType, "Cargar", "<script>document.getElementById('iframeTotales').src='Agrupaciones_Totales.aspx?Nombre=" & nombreAgrupacion & "&idAgrupacion=" & id & "';</script>")
                    'Gráficos
                    ClientScript.RegisterStartupScript(Page.GetType, "Cargar3", "<script>document.getElementById('Grafico1').src='agrupaciones/grafico.aspx?var=consumo&idAgrupacion=" & id & "';</script>")
                    ClientScript.RegisterStartupScript(Page.GetType, "Cargar4", "<script>document.getElementById('Grafico2').src='agrupaciones/grafico2.aspx?var=consumo&idAgrupacion=" & id & "';</script>")

                    Dim sentencia As String = ""
                    sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM " & _
                             " FROM         SICA_SIST_PuntoSistema INNER JOIN [SICA_SIST_Sistemas-Punto] ON SICA_SIST_PuntoSistema.ID = [SICA_SIST_Sistemas-Punto].ID_PuntoSistema" & _
                             " WHERE [SICA_SIST_Sistemas-Punto].ID_Sistema=" & id
                    Dim dstPuntos As New Data.DataSet()
                    utiles.Comprobar_Conexion_BD(Page, conexionDBSica)
                    daAgrupaciones.SelectCommand.CommandText = sentencia
                    daAgrupaciones.Fill(dstPuntos, "TablaPuntos")
                    Dim cadena As String = ""
                    Dim contador As Integer = 0
                    cadena = "<table cellpadding=0 cellspacing=0 width=99% ><tr>"
                    For Each fila In dstPuntos.Tables("TablaPuntos").Rows
                        contador += 1
                        'Se obtiene el icono del punto
                        Dim icono As String = ""
                        icono = ObtenerIcono(fila("codigoPVYCR"))

                        Dim Valor As String = fila("CodigoPVYCR") & ";P"

                        'Se crea un botón por Punto
                        'cadena &= "<asp:Button runat='server' ID='btn" & fila("CodigoPVYCR") & "' cssclass='boton' Text='" & fila("CodigoPVYCR") & "'/>"
                        'cadena &= "<td align='center' valign='bottom' style='text-align:center;'><a  class='vertical' href='caucepuntMAIN.aspx?nodobusqueda=n&pag=1&valor=" & fila("CodigoPVYCR") & "' ><img " + icono + " border=0><br>" & fila("CodigoPVYCR") & "</a></td>"
                        'cadena &= "<td align='center' valign='bottom' style='text-align:center;'><a target='_blank' class='vertical' href='#' onclick='javascript:parent.location.href=""caucepuntMAIN.aspx?nodobusqueda=s&pag=1&agrupacion=s&usuario=" & Session("loginUsuario") & "&valor=" & Valor & """ '><img " + icono + " border=0><br>" & fila("CodigoPVYCR") & "</a></td>"
                        cadena &= "<td align='center' valign='bottom' style='text-align:center;'><a target='ptocontrol' class='vertical' href='caucepuntMAIN.aspx?nodobusqueda=s&pag=1&agrupacion=s&usuario=" & Session("loginUsuario") & "&valor=" & Valor & " '><img " + icono + " border=0><br>" & fila("CodigoPVYCR") & " - " & fila("EM") & "  </a></td>"
                        If (contador Mod 8) = 0 Then
                            cadena = cadena & "</tr><tr>"
                        End If
                    Next
                    cadena &= "</tr></table>"
                    'lblTitulo.Text = "S.I.C.A Elemetos del SCV " & UCase(Request.QueryString("Nombre"))
                    lblPuntosAgrupacion.Text = cadena
                End If
            End If
        Else
            PNL_Lista_de_excedidos.Visible = True
            pnlAgrupacionConInscripcion.Visible = False
            'Carga_Grid_Excedidos()
        End If

    End Sub

    Private Function estaYaExcedida(ByVal id As String) As Boolean

        Dim sql As String = "SELECT * from PVYCR_AprovechamientosExcedidos where id_aprovechamiento=" + id
        Dim dt As DataTable = New DataTable()
        dt = GetDataDBSICA(sql)
        Return (dt.Rows.Count > 0)
    End Function

    Protected Function ObtenerIdRaacs(ByVal id As String) As String
        Dim sql As String
        sql = "SELECT numinscripcion from SICA_SIST_Sistemas where ID=" & id
        Dim DBobject As New SicaSegura.SICA_DB()
        Dim dt As DataTable
        dt = DBobject.GetDataDBSICA(sql)
        ObtenerIdRaacs = dt.Rows(0).ItemArray.GetValue(0).ToString()
    End Function

    Protected Function ObtenerIcono(ByVal CodigoPVYCR As String)
        Dim tipoPunto As String = ""
        Dim estilo As String = ""
        Dim TieneHijosSiNo As Boolean = False
        'Dim esTelemedida As String = ""
        Dim esfavorito As String = ""
        Dim Caducado As Integer = 0
        Dim tipoIcono As String = ""
        Dim tipoIconoE As String = ""
        Dim tipoElemen As String = ""
        'vamos a obtener el tipo de punto para saber qué icono debe llevar
        'ncm lo cambiamo porque nos han cambiado los iconos y ahora se sacan de la tabla PVYCR_clasificacionelemento 08/06/2010
        tipoPunto = ObtenerTipoPunto(CodigoPVYCR)
        tipoIcono = ObtenerIconoPunto(CodigoPVYCR)
        Select Case tipoPunto
            Case "M"
                'estilo = ",cls:'PuntoMTree'"
                Select Case tipoIcono
                    Case "0"
                        estilo = "src='images/iconosBuenos/icocir_Y32.png'"
                    Case "1"
                        estilo = "src='images/iconosBuenos/icocir_O32.png'"
                    Case "2"
                        estilo = "src='images/iconosBuenos/icocir_G32.png'"
                    Case "3"
                        estilo = "src='images/iconosBuenos/icocir_B32.png'"
                End Select
            Case "G"
                'estilo = ",cls:'PuntoGTree'"
                Select Case tipoIcono
                    Case "0"
                        estilo = "src='images/iconosBuenos/icocua_Y32.png'"
                    Case "1"
                        estilo = "src='images/iconosBuenos/icocua_O32.png'"
                    Case "2"
                        estilo = "src='images/iconosBuenos/icocua_G32.png'"
                    Case "3"
                        estilo = "src='images/iconosBuenos/icocua_B32.png'"
                End Select
            Case "P"
                estilo = "src='images/puntoPeaje.ico'"
            Case "T"
                estilo = "src='images/puntoTrasvase.ico'"
        End Select
        Return estilo
    End Function

    Protected Function ObtenerIconoPunto(ByVal codigoPVYCR As String) As String
        'obtenemos tipo del icono para saber qué icono escoger.
        'si es tipo = 1 --> amarillos (xxxxY32)
        'si es tipo = 2 --> naranjas (xxxxO32)
        'si es tipo = 3 --> verdes (xxxxG32)
        'si es tipo = 4 --> azules (xxxxO32)
        Dim sentenciatipopunto = "select top 1 tipo from pvycr_clasificacionelementos where em is null and codigoPVYCR = '" & codigoPVYCR & "'"

        utiles.Comprobar_Conexion_BD(Page, conexionU)
        daTipoPuntos.SelectCommand.CommandText = sentenciatipopunto
        Dim icono As String = daTipoPuntos.SelectCommand.ExecuteScalar
        utiles.CerrarConexion(conexionU)

        If icono = Nothing Then
            icono = "0"
        End If
        Return icono
    End Function

    Protected Function ObtenerTipoPunto(ByVal codigoPVYCR As String) As String
        'obtenemos el punto si es M o G
        Dim sentenciatipopunto = "select tipoPunto from PVYCR_puntos where codigoPVYCR = '" & codigoPVYCR & "'"
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daPuntos.SelectCommand.CommandText = sentenciatipopunto
        Return daPuntos.SelectCommand.ExecuteScalar
    End Function

    Protected Function ObtenerDescripcionAgrupacion(ByVal ID As Integer) As String
        'obtenemos la descripción de la Agrupación

        Dim sentencia = "select Descripcion from SICA_SIST_Sistemas where NumInscripcion = " & ID

        Dim dt As Data.DataTable = GetDataDBSICA(sentencia)
        'utiles.Comprobar_Conexion_BD(Page, conexionDBSica)
        'daAgrupaciones.SelectCommand.CommandText = sentencia
        'Dim Descripcion As String = daAgrupaciones.SelectCommand.ExecuteScalar
        'utiles.CerrarConexion(conexionDBSica)
        Dim Descripcion As String = dt.Rows(0).Item(0).ToString()
        If Descripcion = Nothing Then
            Return ""
        Else
            Return Descripcion
        End If

    End Function

    Protected Function ObtenerVolumenInscripcion(ByVal NumInscripcion As Integer) As String
        Dim VolumenTotal As Double = 0
        Dim dtRegistro As DataTable = New System.Data.DataTable
        Try
            'Si la Agrupación es RAAC los datos, se cogen de la inscripción
            'Es la suma total de los volúmenes de abastecimiento, riego...
            'utiles.Comprobar_Conexion_BD(Page, conexionRegistro)
            'daRegistro.SelectCommand.CommandText = "Select * from AprovechamientosInscritos WHERE NUMPAL=" & NumInscripcion
            'daRegistro.Fill(dstRegistro, "TablaRegistro")
            dtRegistro = GetDataRAACS("Select * from AprovechamientosInscritos WHERE NUMPAL=" & NumInscripcion)
            If dtRegistro.Rows.Count > 0 Then

                VolumenTotal += utiles.nullACero(dtRegistro.Rows(0)("VOLRE")) + utiles.nullACero(dtRegistro.Rows(0)("VOLAB")) + utiles.nullACero(dtRegistro.Rows(0)("VOLOT"))

            End If
            ObtenerVolumenInscripcion = CStr(VolumenTotal)
            'Return String.Format("{0:#,##0.##}", VolumenTotal)


        Catch Excepcion As Exception
            Return "0"
        End Try



    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetColorArrow(ByVal Numpal As String) As String
        'obtenemos la descripción de la Agrupación

        Dim sentencia = "select Descripcion from SICA_SIST_Sistemas where NumInscripcion = " & Numpal

        Dim cadenaConexion = ConfigurationManager.ConnectionStrings("DBSICA").ConnectionString
        Dim dt As Data.DataTable = New Data.DataTable
        Dim connection As Data.SqlClient.SqlConnection = New Data.SqlClient.SqlConnection(cadenaConexion)
        Dim adapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter(sentencia, connection)
        adapter.Fill(dt)

        If dt.Rows.Count > 0 Then
            GetColorArrow = "green"
        Else
            GetColorArrow = "red"
        End If
    End Function

    Protected Function CalculoTotales(ByVal idagrupacion As Integer) As String
        Dim sentencia As String = ""
        sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, SICA_SIST_Sistemas.NumInscripcion " & _
                 " FROM         SICA_SIST_PuntoSistema INNER JOIN [SICA_SIST_Sistemas-Punto] ON SICA_SIST_PuntoSistema.ID = [SICA_SIST_Sistemas-Punto].ID_PuntoSistema" & _
                 " INNER JOIN     SICA_SIST_Sistemas ON    SICA_SIST_Sistemas.ID=[SICA_SIST_Sistemas-Punto].ID_Sistema" & _
                 " WHERE [SICA_SIST_Sistemas].NumInscripcion='" & idagrupacion & "'"


        Dim dstPuntos As New Data.DataSet()
        utiles.Comprobar_Conexion_BD(Page, conexionDBSica)
        daAgrupaciones.SelectCommand.CommandText = sentencia
        daAgrupaciones.Fill(dstPuntos, "TablaPuntos")

        'If DateTime.Now.Month < 10 Then
        '    Me.lblAnyoHidrologicoE.Attributes.Add("onclick", "CargarAnyosE('01/10/" & DateTime.Now.Year - 1 & "','30/09/" & DateTime.Now.Year & "');return false;")
        '    txtFiltroFechaFinE.Text = "30/09/" & DateTime.Now.Year
        '    txtFiltroFechaIniE.Text = "01/10/" & DateTime.Now.Year - 1
        'Else
        '    Me.lblAnyoHidrologicoE.Attributes.Add("onclick", "CargarAnyosE('01/10/" & DateTime.Now.Year & "','30/09/" & DateTime.Now.Year + 1 & "');return false;")
        '    txtFiltroFechaFinE.Text = "30/09/" & DateTime.Now.Year + 1
        '    txtFiltroFechaIniE.Text = "01/10/" & DateTime.Now.Year
        'End If

        'Me.lblAnyoHidrologicoE.Text = obtenerAnyoHidrologicoEtiqueta()

        Dim cadena As String = ""

        Dim utiliSica As New Sicadll.AccesoADatos.AccesoADatos2

        Dim volumenGeneral As Double = 0
        Dim concesionAprovechamiento As Integer = 0
        Dim PorcentajeTotal As Double = 0

        If dstPuntos.Tables("TablaPuntos").Rows.Count > 0 Then
            'Se comprueba las fechas
            For Each fila In dstPuntos.Tables("TablaPuntos").Rows
                Dim resultado As Object
                'Se obtienen los resultados de cada uno de los elementos de medida que
                'componen el punto
                Dim FI As String
                If DateTime.Now.Month < 10 Then
                    FI = "01/10/" & CStr(DateTime.Now.Year - 1)

                Else
                    FI = "01/10/" & DateTime.Now.Year.ToString()

                End If
                resultado = utiliSica.obtenerAcumulado(fila("codigoPVYCR"), fila("EM"), conexion, FI, DateTime.Now.Day & "/" & DateTime.Now.Month & "/" & DateTime.Now.Year)

                'Se calculan los acumulados
                volumenGeneral += resultado(1)

                If fila("NumInscripcion").ToString() = "" Then
                    concesionAprovechamiento += resultado(2)
                End If
                PorcentajeTotal += resultado(3)

            Next
            CalculoTotales = CStr(volumenGeneral)




            'Se cargan los valores en las cajas de texto
            'txtVolumenGeneral.Text = String.Format("{0:#,##0.##}", volumenGeneral)
            If dstPuntos.Tables("TablaPuntos").Rows(0)("NumInscripcion").ToString() <> "" Then
                'txtConcesionAprov.Text = ObtenerVolumenInscripcion(dstPuntos.Tables("TablaPuntos").Rows(0)("NumInscripcion"))
            Else
                'txtConcesionAprov.Text = String.Format("{0:#,##0.##}", concesionAprovechamiento)
            End If

            'txtPorcentajeTotal.Text = String.Format("{0:#,##0.##}", PorcentajeTotal)
        Else
            'txtVolumenGeneral.Text = 0
            'txtConcesionAprov.Text = 0
            'txtPorcentajeTotal.Text = 0

        End If
    End Function

    Private Function GetDataDBSICA(ByVal SQL As String) As Data.DataTable
        Dim cadenaConexion = ConfigurationManager.ConnectionStrings("DBSICA").ConnectionString
        Dim dt As Data.DataTable = New Data.DataTable
        Dim connection As Data.SqlClient.SqlConnection = New Data.SqlClient.SqlConnection(cadenaConexion)
        Dim adapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter(SQL, connection)
        adapter.Fill(dt)
        GetDataDBSICA = dt
    End Function

    Private Function GetDataRAACS(ByVal SQL As String) As Data.DataTable
        Dim cadenaConexion = ConfigurationManager.AppSettings("dsn_RegistroAguas").ToString()
        Dim dt As Data.DataTable = New Data.DataTable
        Dim connection As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(cadenaConexion)
        Dim adapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter(SQL, connection)
        adapter.Fill(dt)
        GetDataRAACS = dt
    End Function

    Protected Sub IMGBTN_DownloadInforma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IMGBTN_DownloadInforma.Click
        Dim FI As String
        Dim FF As String
        FI = Session("FiltroFechaIni")
        FF = Session("FiltroFechaFin")
        If FI = "" Or FF = "" Then


            If (DateTime.Now.Month > 9) Then
                FI = "01/10/" & DateTime.Now.Year
                FF = "01/10/" & DateTime.Now.AddYears(1).Year
            Else
                FI = "01/10/" & DateTime.Now.AddYears(-1).Year
                FF = "01/10/" & DateTime.Now.Year
            End If
        End If
        Dim parametrosInforme As String = "inscripcion=" & Request.QueryString("Nombre") & "&filtrofechaIni=" & FI & "&filtrofechaFin=" & FF & "&filtroIntervalo=" & DDL_Periodo_Informe.SelectedValue.ToString().Substring(0, 1) & "&nodoclave=&ClaveNodoTipo=&nodotexto=&FiltroNulas=false&FiltrarCodFuenteDato=&FiltroNreg=&informe=" & DDL_Formato_Informe.SelectedValue.ToString() & "&LecturasSeleccionadas=&reducirLecturas=false"
        Response.Redirect("./agrupaciones/informe/ListadoCaudalesFiltrados.aspx?" & parametrosInforme & "")

    End Sub

    
End Class