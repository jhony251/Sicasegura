Imports GuarderiaFluvial
Imports System.Data
Partial Class SICAH_acequiasrechazadas
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura"))
    Dim daRechazadas As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstRechazadas As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'Session("Ordenacion") = ""

            lblCabecera.Text = genHTML.cabecera(Page)
            lblPestanyas.Text = genHTML.pestanyasMenu(1, "../", Page, Session("idperfil"), Session("usuarioReg"))

            'If Request.QueryString("pag") <> "" Then
            '    lblPagina.Text = Request.QueryString("pag")
            'Else
            '    lblPagina.Text = "1"
            'End If
            pnlPrincipal.Visible = True
            daRechazadas.SelectCommand.CommandText = "SELECT [CodigoPVYCR],[Cod_Fuente_Dato],[Fecha],[Hora],[TipoObtencionCaudal],[Escala_M] " & _
                           ",[Calado_M],[Observaciones],[Tiempo_SG],[V11_RPM],[V12_RPM],[V21_RPM],[V22_RPM],[V31_RPM] " & _
                           ",[V32_RPM],[V11_MS],[V12_MS],[V21_MS],[V22_MS],[V31_MS],[V32_MS],[Factor_Flotador],[Longitud_Flotador] " & _
                           "FROM PVYCR_DatosAcequias_Estadillo_Histo " & _
                           "WHERE codigoPVYCR = 'VA005P02' and produccion = 'N' " & _
                           "ORDER BY fecha desc, hora desc"

            'sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.Cod_Fuente_Dato "
            daRechazadas.Fill(dstRechazadas, "TablaHisto")
            rptRechazadas.DataSource = dstRechazadas.Tables("TablaHisto")
            rptRechazadas.DataBind()
            'lblNumpaginas.DataBind()
        End If
    End Sub

End Class
