Imports Sicadll
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO

Partial Class SICAH_Agrupaciones_Totales
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim conexionDBSica As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_dbsica"))
    Dim conexionU As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim daAgrupaciones As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionDBSica)
    Dim daTipoPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionU)
    Dim daPuntos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstPuntos As DataSet = New System.Data.DataSet()


    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")


        If Not IsPostBack Then


            'Se obtiene la lista de puntos pertenecientes a la Agrupación
            If Not Request.QueryString("Nombre") Is Nothing Then


                imgCalFechaIniE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniE.ClientID & "'),'dd/mm/yyyy');")
                imgCalFechaFinE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinE.ClientID & "'),'dd/mm/yyyy');")
                '   
                lblTitulo.Text = "TOTALES SCV " & UCase(Request.QueryString("Nombre"))
                'Se cargan los datos del Registro de la Agrupación
                CalculoTotales()
            End If


        End If
    End Sub
   

    Protected Sub CalculoTotales()
        Dim sentencia As String = ""
        sentencia = "SELECT     [SICA_SIST_Sistemas-Punto].ID_Sistema, SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM " & _
                 " FROM         SICA_SIST_PuntoSistema INNER JOIN [SICA_SIST_Sistemas-Punto] ON SICA_SIST_PuntoSistema.ID = [SICA_SIST_Sistemas-Punto].ID_PuntoSistema" & _
                 " INNER JOIN     SICA_SIST_Sistemas ON    SICA_SIST_Sistemas.ID=[SICA_SIST_Sistemas-Punto].ID_Sistema" & _
                 " WHERE [SICA_SIST_Sistemas-Punto].ID_Sistema='" & Request.QueryString("idAgrupacion") & "'"
        Dim dstPuntos As New Data.DataSet()
        utiles.Comprobar_Conexion_BD(Page, conexionDBSica)
        daAgrupaciones.SelectCommand.CommandText = sentencia
        daAgrupaciones.Fill(dstPuntos, "TablaPuntos")

        If DateTime.Now.Month < 10 Then
            Me.lblAnyoHidrologicoE.Attributes.Add("onclick", "CargarAnyosE('01/10/" & DateTime.Now.Year - 1 & "','30/09/" & DateTime.Now.Year & "');return false;")
            txtFiltroFechaFinE.Text = "30/09/" & DateTime.Now.Year
            txtFiltroFechaIniE.Text = "01/10/" & DateTime.Now.Year - 1
        Else
            Me.lblAnyoHidrologicoE.Attributes.Add("onclick", "CargarAnyosE('01/10/" & DateTime.Now.Year & "','30/09/" & DateTime.Now.Year + 1 & "');return false;")
            txtFiltroFechaFinE.Text = "30/09/" & DateTime.Now.Year + 1
            txtFiltroFechaIniE.Text = "01/10/" & DateTime.Now.Year
        End If

        Me.lblAnyoHidrologicoE.Text = obtenerAnyoHidrologicoEtiqueta()

        Dim cadena As String = ""

        Dim utiliSica As New Sicadll.AccesoADatos.AccesoADatos2

        Dim volumenGeneral As Double = 0
        Dim concesionAprovechamiento As Integer = 0
        Dim PorcentajeTotal As Double = 0


        'Se comprueba las fechas

        For Each fila In dstPuntos.Tables("TablaPuntos").Rows
            Dim resultado As Object
            'Se obtienen los resultados de cada uno de los elementos de medida que
            'componen el punto
            resultado = utiliSica.obtenerAcumulado(fila("codigoPVYCR"), fila("EM"), conexion, txtFiltroFechaIniE.Text, txtFiltroFechaFinE.Text)

            'Se calculan los acumulados
            volumenGeneral += resultado(1)
            concesionAprovechamiento += resultado(2)
            PorcentajeTotal += resultado(3)

        Next

        'Se cargan los valores en las cajas de texto
        txtVolumenGeneral.Text = String.Format("{0:#,##0.##}", volumenGeneral)
        txtConcesionAprov.Text = String.Format("{0:#,##0.##}", concesionAprovechamiento)
        txtPorcentajeTotal.Text = String.Format("{0:#,##0.##}", PorcentajeTotal)

    End Sub

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
End Class
