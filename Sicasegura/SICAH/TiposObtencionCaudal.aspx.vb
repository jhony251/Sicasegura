Imports System.Data
Imports GuarderiaFluvial
Imports System.Data.SqlClient
Imports System

Partial Class SICAH_TiposObtencionCaudal

  Inherits System.Web.UI.Page
  Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
  Dim daTipo As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
  Dim dstTipo As System.Data.DataSet = New System.Data.DataSet()

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Not IsPostBack Then
      Dim sentencia As String = "select TipoObtencionCaudal,descripcion from PVYCR_tiposObtencioncaudal order by 1"
      utiles.Comprobar_Conexion_BD(Page, conexion)
      daTipo.SelectCommand.CommandText = sentencia
      daTipo.Fill(dstTipo, "TablaTipos")
      rptTipos.DataSource = dstTipo.Tables("TablaTipos")
      rptTipos.DataBind()
    End If
  End Sub
End Class
