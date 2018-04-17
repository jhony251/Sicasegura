Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Imports System.IO
Partial Class SICAH_Agrupaciones_DatosAdmin
    Inherits System.Web.UI.Page
    Dim conexionSIGVECTOR As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim conexionRegistro As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_RegistroAguas"))
    Dim daRegistro As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionRegistro)
    Dim daSigVector As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionSIGVECTOR)
    Dim dstRegistro As DataSet = New System.Data.DataSet()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("RAAC") = "0" Or Request.QueryString("RAAC") Is Nothing And Request.QueryString("Pestanya") Is Nothing Then
            pnlAgrupacionesSinInscrip.Visible = True
            pnlAgrupacion1.Visible = False
            pnlAgrupacion2.Visible = False
        Else
            lblPestanyas.Text = genHTML.pestanyasMenuAgrupacion(Page, Request.QueryString("Nombre"))
            If Request.QueryString("Pestanya") = "1" Or Request.QueryString("Pestanya") Is Nothing Then
                pnlAgrupacionesSinInscrip.Visible = False
                pnlAgrupacion1.Visible = True
                pnlAgrupacion2.Visible = False
            Else
                pnlAgrupacionesSinInscrip.Visible = False
                pnlAgrupacion2.Visible = True
                pnlAgrupacion1.Visible = False
            End If
        End If

        If Not IsPostBack Then


            'Se obtiene la lista de puntos pertenecientes a la Agrupación
            'If Not Request.QueryString("Nombre") Is Nothing Then
            lblTitulo.Text = "DATOS ADMINISTRATIVOS: ID RACCS " & UCase(Request.QueryString("Nombre")) & " //DATOS ADMINISTRATIVOS:"

            'If Request.QueryString("RAAC") = "1" Then
            'Se cargan los datos del Registro de la Agrupación
            If pnlAgrupacionesSinInscrip.Visible = False Then

                CargarDatosAgrupacion(utiles.nullABlanco(Request.QueryString("Nombre")))
            End If
            'End If

            'End If
            'End If

        End If
    End Sub

    Protected Sub CargarDatosAgrupacion(ByVal NumInscripcion As String)
        Dim sentencia As String = ""
        If NumInscripcion <> "" Then
            'sentencia = "SELECT   * FROM AprovechamientosInscritos WHERE SEC='" & Seccion & "' AND TOMO='" & Tomo & "' AND HOJA='" & Hoja & "'"     
            sentencia = "SELECT   * FROM AprovechamientosInscritos WHERE NUMPAL=" & NumInscripcion
            utiles.Comprobar_Conexion_BD(Page, conexionRegistro)
            daRegistro.SelectCommand.CommandText = sentencia
            daRegistro.Fill(dstRegistro, "TablaRegistro")
            If dstRegistro.Tables("TablaRegistro").Rows.Count > 0 Then
                'Se cargan los controles con los datos del registro de Aguas
                Me.txtACUIF.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("ACUIF"))
                Me.txtCAUDAL1.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("CAUDAL1"))
                Me.txtCAUDALME.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("CAUDALME"))
                Me.txtHoja.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("Hoja"))
                Me.txtSec.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("Sec"))
                Me.txtTomo.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("Tomo"))
                Me.txtUSOOT.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("USOOT"))
                Me.txtVOLAB.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("VOLAB"))
                Me.txtVOLGA.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("VOLGA"))
                Me.txtVOLIN.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("VOLIN"))
                Me.txtVOLOT.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("VOLOT"))
                Me.txtVOLRE.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("VOLRE"))
                Me.chkUSOAB.Checked = IIf(utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("USOAB")) = "S", True, False)
                Me.chkUSOGA.Checked = IIf(utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("USOGA")) = "S", True, False)
                Me.chkUSOIN.Checked = IIf(utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("USOIN")) = "S", True, False)
                Me.chkUSORE.Checked = IIf(utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("USORE")) = "S", True, False)
                Me.txtUSOOT.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("USOOT"))
                Me.txtVOL1AB.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("VOL1AB"))
                Me.txtVOL1GA.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("VOL1GA"))
                Me.txtVOL1IN.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("VOL1IN"))
                Me.txtVOL1OT.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("VOL1OT"))
                Me.txtVOL1RE.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("VOL1RE"))
                Me.txtPOTEN.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("POTEN"))
                Me.txtSALTO.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("SALTO"))
                Me.txtSUPER.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("SUPER"))
                Me.txtTITULO.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("TITULO"))
                Me.txtOBS1.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("OBS1"))
                Me.txtOBS2.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("OBS2"))
                Me.txtOBS3.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("OBS3"))
                Me.txtOBS4.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("OBS4"))
                Me.txtOBS5.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("OBS5"))
                Me.txtOBS6.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("OBS6"))
                Me.txtFECINS.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("FECINS"))
                Me.txtFIRMADO.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("FIRMADO"))
                Me.txtFECCER.Text = utiles.nullABlanco(dstRegistro.Tables("TablaRegistro").Rows(0).Item("FECCER"))
                dstRegistro.Dispose()
            End If
        End If

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        conexionRegistro.Close()
        conexionSIGVECTOR.Close()
    End Sub
End Class
