Imports System.Data
Imports GuarderiaFluvial

Partial Class SICAH_SelecCamposFiltradosMto
    Inherits System.Web.UI.Page

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daAdapter As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCampos As DataSet = New System.Data.DataSet()
    Dim tabla As String = ""
    Dim reg As Integer = 0



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim mto As String = Request.QueryString("mto")
        Dim check As String = ""
        Select Case mto
            Case "cauces"
                tabla = "PVYCR_CAUCES"
                check = "CodigoCauce"
            Case "puntos"
                tabla = "PVYCR_PUNTOS"
                check = "CodigoPVYCR"
            Case "Elementos de Medida"
                tabla = "PVYCR_ELEMENTOSMEDIDA"
                check = "idElementoMedida"
            Case "Boletines Guardería"
                tabla = "PVYCR_BoletinGuarderia"
                check = "NumRef"
            Case "Seguimiento Administrativo"
                tabla = "PVYCR_SeguimientoAdministrativo"
                check = "CodigoCauce"
            Case "Contadores"
                tabla = "PVYCR_CONTADORES"
                check = "idContador"
            Case "Motobombas"
                tabla = "PVYCR_MOTOBOMBAS"
                check = "CodigoMotoBomba"
        End Select

        If Not IsPostBack Then
            Dim sentenciaSel As String = ""
            Dim i As Integer = 0
            ' obtenemos las columnas de la tabla para rellenar los check
            sentenciaSel = "SELECT Column_Name as columna  FROM INFORMATION_SCHEMA. COLUMNS where Table_Name = '" + tabla + "'"
            daAdapter.SelectCommand.CommandText = sentenciaSel
            daAdapter.Fill(dstCampos, "TablaCampos")
            Dim reg As Integer = dstCampos.Tables("TablaCampos").Rows.Count
            ' si tenemos alguna tabla rellenaremos los check
            If (dstCampos.Tables.Count > 0) Then
                Session("CamposFiltradoMto") = dstCampos
                chklMtos.DataSource = dstCampos
                chklMtos.DataBind()
                chklMtos.RepeatColumns = 4
                'chklMtos.RepeatDirection = RepeatDirection.Horizontal
                For i = 0 To reg - 1
                    chklMtos.Items(i).Text = dstCampos.Tables("TablaCampos").Rows(i).Item("columna").ToString
                    If dstCampos.Tables("TablaCampos").Rows(i).Item("columna").ToString = check Then
                        chklMtos.Items(i).Selected = True
                    End If
                Next

            End If

        End If


    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim mto As String = Request.QueryString("mto")
        Dim numItems As Integer = chklMtos.Items.Count
        Dim checkSel As Integer = 0
        For i = 0 To numItems - 1
            If chklMtos.Items(i).Selected = True Then
                checkSel = checkSel + 1
            End If
        Next
        If checkSel > 10 Then
            JavaScript.Alert(Page, "Sólo se pueden mostrar 10 columnas en el informe y ha sobrepasado el límite")
            Exit Sub
        End If
        rellenarArraySel(tabla)
        'toolbar=no,resizable=no, width=600, height=400, scrollbar=no, status=no')" /> 'resizable=no, scrollbar=no, status=no
        Dim nombreinforme = "../listados/InformeFiltrosMtos.aspx?mto=" & mto
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub
    Protected Sub rellenarArraySel(ByVal tabla As String)
        Dim campos As Integer = chklMtos.Items.Count
        Dim columnas As String = ""
        Dim sentenciaIni As String = "select "
        Dim sentenciaFin As String = " from " + tabla
        Dim i As Integer = 0
        Dim j As Integer = 1
        Dim etiquetas As ArrayList = New ArrayList

        For i = 0 To campos - 1
            If chklMtos.Items(i).Selected = True Then
                columnas = columnas & chklMtos.Items(i).Text & " campo" & j & ", "
                j = j + 1
                etiquetas.Add(chklMtos.Items(i).Text)
            End If
        Next
        columnas = columnas.Substring(0, columnas.Length - 2)
        If Session("FiltroListado") <> "" Then
            sentenciaFin = sentenciaFin & " where (1=1) " & Session("FiltroListado")
        End If

        Session("sentenciaFiltroMto") = sentenciaIni & columnas & sentenciaFin
        Session("etiquetas") = etiquetas

        'For i = 0 To campos - 1
        '    If chklMtos.Items(i).Selected = True Then
        '        columnas = columnas & chklMtos.Items(i).Text & ", "
        '        j = j + 1
        '        etiquetas.Add(chklMtos.Items(i).Text)
        '    End If
        'Next

        'Dim daCampos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        ''sentenciaSel = Session("sentenciaFiltroMto")
        'daCampos.SelectCommand.CommandText = Session("sentenciaFiltroMto")
        'daCampos.Fill(dstCampos, "TablaCampos")

        'Dim col As Integer = dstCampos.Tables("TablaCampos").Columns.Count

        'For i = 0 To col - 1
        '    Dim lon As Integer = dstCampos.Tables("TablaCampos").Columns(i).MaxLength

        'Next

    End Sub
   
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        ClientScript.RegisterStartupScript(Page.GetType, "CierraVentana", "<script language=javascript>window.close();</script>")
    End Sub
End Class
