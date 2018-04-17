Imports OpenFlashChart
Imports System.Drawing
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles
Partial Class SICAH_Perfil_acequiaFlash
    Inherits System.Web.UI.Page
    Dim sentenciaSel As String
    Dim selectConnection As New SqlClient.SqlConnection(ConfigurationSettings.AppSettings.Item("dsnsegura_migracion"))
    Dim da As New SqlClient.SqlDataAdapter("", selectConnection)
    Dim ds As DataSet = New System.Data.DataSet()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        utiles.Comprobar_Conexion_BD(Page, selectConnection)
        Response.CacheControl = "no-cache"
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
        End If
        'RDF. Fecha: 04/09/2008
        If Not Request.QueryString("Codigo") Is Nothing Then
            Me.Session.Item("CodigoPVYCR") = Request.QueryString("Codigo")
        End If

        sentenciaSel = "SELECT P.DenominacionPunto, C.DenominacionCauce " & _
                    "FROM PVYCR_Cauces C,  PVYCR_Puntos P " & _
                    "WHERE C.CodigoCauce = P.CodigoCauce and P.CodigoPVYCR = '" & Me.Session.Item("CodigoPVYCR") & "' "
        da.SelectCommand.CommandText = sentenciaSel
        da.Fill(ds, "TablaGrafico")
        lblTitulo.Text = Me.Session.Item("CodigoPVYCR") + " / " + ds.Tables("TablaGrafico").Rows(0).Item("DenominacionPunto").ToString + " / " + ds.Tables("TablaGrafico").Rows(0).Item("DenominacionCauce").ToString



    End Sub
End Class
