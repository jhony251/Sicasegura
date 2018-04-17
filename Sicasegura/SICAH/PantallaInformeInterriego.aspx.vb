Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Partial Class SICAH_PantallaInformeInterriego
    Inherits System.Web.UI.Page
    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daConexion As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstArbol As DataSet = New System.Data.DataSet()
    Dim dstSistemas As DataSet = New System.Data.DataSet()
    Dim daa As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New System.Data.DataSet()



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")

        lblCabecera.Text = genHTML.cabecera(Page)
        lblPestanyas.Text = genHTML.pestanyasMenu(9, "../", Page, Session("idperfil"), Session("usuarioReg"))

        If Not IsPostBack Then
            imgCalFechaIniI.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaIni.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaFinI.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaFin.ClientID & "'),'dd/mm/yyyy');")
            'BuildListBox() 
            'crearDataSetsSistemas()
            rellenar_ddl()

        End If


    End Sub

    Protected Sub btnListado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListado.Click
        Session("InterriegoFechaIni") = txtFechaIni.Text
        Session("InterriegoFechaFin") = txtFechaFin.Text
        Session("InterriegoIdAgru") = ddlAgrupacion.SelectedValue
        Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
           "window.open('../listados/ListadoInterriego.aspx?fechainicio=" + txtFechaIni.Text + "&fechaFin=" + txtFechaFin.Text + "&idAgru=" + ddlAgrupacion.SelectedValue + "','_blank','')" & _
   "</script>")
    End Sub
    Protected Sub rellenar_ddl()
        Dim sql As String
        sql = "select idagrupacion, descripcion from PVYCR_Agrupaciones order by 2"
        Dim daagrupacion As New SqlDataAdapter(sql, conexion)

        daagrupacion.Fill(dst, "TablaAgrupacion")
        ddlAgrupacion.DataSource = dst.Tables("TablaAgrupacion")
        ddlAgrupacion.DataValueField = "idAgrupacion"
        ddlAgrupacion.DataTextField = "Descripcion"
        ddlAgrupacion.DataBind()
        ddlAgrupacion.Items.Insert(0, New ListItem("[Seleccionar]", ""))
    End Sub
End Class
