Imports System.Data
Imports System.Data.SqlClient
Imports Sicadll
Imports GuarderiaFluvial.utiles

Partial Class Lecturas
    Inherits System.Web.UI.Page
    Dim conexion As New SqlConnection(ConfigurationManager.AppSettings("dsn"))
    Dim conexion_pruebas As New SqlConnection(ConfigurationManager.AppSettings("dsn_pruebas"))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ucPaginador.conexion = New SqlConnection(ConfigurationManager.AppSettings("dsn"))
        'ucPaginador.da = New SqlDataAdapter("", ucPaginador.conexion) 'jms30x
        ucPaginador.comando = New SqlCommand("", ucPaginador.conexion)

        If Not IsPostBack Then
            CargarDataSets()
        End If

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        ucPaginador.conexion.Close()
        'ucPaginador.da.Dispose() 'jms30x
        ucPaginador.comando.Dispose()
    End Sub

    Private Sub CargarDataSets()
        ucPaginador.cadenaConexion = ConfigurationManager.AppSettings("dsn")
        ucPaginador.itemsporPagina = ConfigurationManager.AppSettings("registrosPorPag")

        Dim AccesoADatos As New Sicadll.AccesoADatos.AccesoADatos2
        Dim codigoPVYCR As String = Request.QueryString("codigoPVYCR")
        Dim EM As String = Request.QueryString("EM")
        Dim fechaInicio As DateTime = CalculaAnyoHidrologico()
        Dim fechaFin As DateTime = Today
        Dim strSQL As String = AccesoADatos.SacarLecturasPreYPro(EM, " WHERE CodigoPVYCR='" & codigoPVYCR & "' AND idElementoMedida='" & EM & "' " & _
                                                                 "AND Fecha_Medida between '" & fechaInicio & "' and '" & fechaFin & "'", "ORDER BY Fecha_Medida DESC", conexion)(1)

        Select Case Left(EM, 1)
            Case "Q"
                pnlLecturasQ.Visible = True
                ucPaginador.controlMuestra = "rptLecturasQ"
            Case "V"
                pnlLecturasV.Visible = True
                ucPaginador.controlMuestra = "rptLecturasV"
            Case "E"
                pnlLecturasE.Visible = True
                ucPaginador.controlMuestra = "rptLecturasE"
            Case "H"
                pnlLecturasH.Visible = True
                ucPaginador.controlMuestra = "rptLecturasH"
        End Select

        ucPaginador.sentenciaSQL = strSQL
        ucPaginador.nombreTabla = "Datos"
        ucPaginador.comando.CommandText = Replace(ucPaginador.sentenciaSQL, "ORDER BY Fecha_Medida DESC", "")
        Dim numReg As Integer = ucPaginador.comando.ExecuteScalar

        If numReg = 0 Then
            ucPaginador.Visible = False
            lblNohay.Visible = True
        End If

        If Request.QueryString("pag") <> "" Then
            ucPaginador.numPagina = Request.QueryString("pag")
        Else
            ucPaginador.numPagina = 1
        End If

        ucPaginador.getRegistros()

        fechaInicio = "01/10/2010"
        fechaFin = "30/09/2012"
        codigoPVYCR = "CM022P01"
        EM = "H01"
        Dim acumulado(4) As Object

        acumulado = AccesoADatos.obtenerAcumulado(codigoPVYCR, EM, conexion, fechaInicio, fechaFin)
        conexion.Close()
    End Sub

    Private Function CalculaAnyoHidrologico() As DateTime
        Dim fecha As DateTime

        Select Case Today.Month
            Case Is >= 10
                fecha = "01/10/" & Today.Year
            Case Else
                fecha = "01/10/" & Today.Year - 1
        End Select

        Return fecha
    End Function

    Protected Function GetEstilo(ByVal eldataitem As DataRowView) As String
        Return "grid" & eldataitem("estadillo")
    End Function

End Class
