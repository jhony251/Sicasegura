Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO
Partial Class SICAH_caucepuntMAIN
    Inherits System.Web.UI.Page

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim codigoCauce As String = ""

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder, sentenciaSelCount As String
    Dim parfila As Integer = 0
    Dim numfila As Integer = 0


    Dim dsnGuarderia As String = ConfigurationSettings.AppSettings("dsn")
    Dim conexionGuarderia As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim comandoGuarderia As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexionGuarderia)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim nodoBuscado As TreeNode
        'formateo_controles_cliente()
        'IncrustarLoadEvent()
        'Page.MaintainScrollPositionOnPostBack = True
    If Not IsPostBack Then
      ' ncm 31/05/2010 para logar al usuario cuando llamamos desde esquemas segura o infopto

            If Request.QueryString("nodobusqueda") = "s" Then
                If conexionGuarderia.State = ConnectionState.Closed Then conexionGuarderia.Open()
                comandoGuarderia.CommandText = "select id from tusuarios where login='" & Request.QueryString("usuario") & "'"
                Session("usuarioReg") = comandoGuarderia.ExecuteScalar
                comandoGuarderia.CommandText = "select idperfil from tusuarios where id='" & Session("usuarioReg") & "'"
                Session("idperfil") = comandoGuarderia.ExecuteScalar
                conexionGuarderia.Close()
                Session("loginUsuario") = Request.QueryString("usuario")
            End If
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            lblCabecera.Text = genHTML.cabecera(Page)
            lblPestanyas.Text = genHTML.pestanyasMenu(4, "../", Page, Session("idperfil"), Session("usuarioReg"))
            'ncm 31/05/2010 si se llama desde infoPtoControl se pasará el valor del nodo/codigoPVYCR y la búsqueda a 's'
            If Request.QueryString("nodobusqueda") = "s" Then
                iframeBuscar.Attributes.Add("src", "../SICAH/caucepuntBuscar.aspx?nodobusqueda=s&valor=" & Request.QueryString("valor"))
            Else
                iframeBuscar.Attributes.Add("src", "../SICAH/caucepuntBuscar.aspx?nodobusqueda=n&valor=0")
            End If
            'If VisibleSegunPerfil() = False Then
            'lblPresentacion.Text = genHTML.ObtenerPresentacion()
            'End If
        End If
  End Sub
 

End Class
