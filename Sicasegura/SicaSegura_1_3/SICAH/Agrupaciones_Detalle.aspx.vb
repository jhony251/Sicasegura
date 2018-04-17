Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO

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



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then
            Dim id As Integer

            If Request.QueryString("Nombre") Is Nothing And Request.QueryString("RAAC") Is Nothing Then

                pnlAgrupacionConInscripcion.Visible = False
              
                'Se obtiene la lista de puntos pertenecientes a la Agrupación
                'ElseIf Request.QueryString("Nombre") Is Nothing Or Request.QueryString("RAAC") <> "1" Then
                '    pnlAgrupacionConInscripcion.Visible = False
                '    pnlAgrupacionesSinInscrip.Visible = True
            Else
                pnlAgrupacionConInscripcion.Visible = True
                id = Request.QueryString("idAgrupacion")



                txtDescripcion.Text = ObtenerDescripcionAgrupacion(id)
                Dim nombreAgrupacion As String = Request.QueryString("Nombre")
                ClientScript.RegisterStartupScript(Page.GetType, "Cargar", "<script>document.getElementById('iframeTotales').src='Agrupaciones_Totales.aspx?Nombre=" & nombreAgrupacion & "&idAgrupacion=" & id & "';</script>")
                ClientScript.RegisterStartupScript(Page.GetType, "Cargar2", "<script>document.getElementById('iframeAdmin').src='Agrupaciones_DatosAdmin.aspx?Nombre=" & nombreAgrupacion & "&RAAC=" & Request.QueryString("RAAC") & "';</script>")
                'Gráficos
                ClientScript.RegisterStartupScript(Page.GetType, "Cargar3", "<script>document.getElementById('Grafico1').src='grafico.aspx?var=consumo&idAgrupacion=" & id & "';</script>")
                ClientScript.RegisterStartupScript(Page.GetType, "Cargar4", "<script>document.getElementById('Grafico2').src='grafico2.aspx?var=consumo&idAgrupacion=" & id & "';</script>")

                Dim sentencia As String = ""
                sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR " & _
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
                    cadena &= "<td align='center' valign='bottom' style='text-align:center;'><a  class='vertical' href='#' onclick='javascript:parent.location.href=""caucepuntMAIN.aspx?nodobusqueda=s&pag=1&agrupacion=s&usuario=" & Session("loginUsuario") & "&valor=" & Valor & """ '><img " + icono + " border=0><br>" & fila("CodigoPVYCR") & "</a></td>"
                    If (contador Mod 4) = 0 Then
                        cadena = cadena & "</tr><tr>"
                    End If
                Next
                cadena &= "</tr></table>"
                lblTitulo.Text = "ELEMENTOS DEL SCV " & UCase(Request.QueryString("Nombre"))
                lblPuntosAgrupacion.Text = cadena
            End If
        End If
    End Sub

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
        Dim sentencia = "select Descripcion tipo from SICA_SIST_Sistemas where ID = " & ID

        utiles.Comprobar_Conexion_BD(Page, conexionDBSica)
        daAgrupaciones.SelectCommand.CommandText = sentencia
        Dim Descripcion As String = daAgrupaciones.SelectCommand.ExecuteScalar
        utiles.CerrarConexion(conexionDBSica)

        If Descripcion = Nothing Then
            Return ""
        Else
            Return Descripcion
        End If

    End Function
End Class
