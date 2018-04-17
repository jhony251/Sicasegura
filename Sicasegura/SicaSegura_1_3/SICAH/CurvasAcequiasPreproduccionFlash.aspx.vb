Imports OpenFlashChart
Imports System.Drawing
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles

Partial Class SICAH_CurvasAcequiasPreproduccionFlash
    Inherits System.Web.UI.Page

    Dim sentenciaSel As String
    Dim selectConnection As New SqlClient.SqlConnection(ConfigurationSettings.AppSettings.Item("dsnsegura_migracion"))
    Dim da As New SqlClient.SqlDataAdapter("", selectConnection)
    Dim ds As DataSet = New System.Data.DataSet()
    Dim url As String

    Protected Function getUrlGrafico(ByVal campo As Integer) As String
        Select Case campo
            Case 1
                CalculaURL(1)
                url = url & "&grafico=TipoObtencionCaudal"
            Case 2
                CalculaURL(2)
                url = url & "&grafico=Cod_Fuente_Dato"
            Case 3
                CalculaURL(3)
                url = url & "&grafico=RegimenCurva"
        End Select

        Return Server.UrlEncode(url)
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.CacheControl = "no-cache"
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
        End If
        utiles.Comprobar_Conexion_BD(Page, selectConnection)
        sentenciaSel = "SELECT P.DenominacionPunto, C.DenominacionCauce " & _
                   "FROM PVYCR_Cauces C,  PVYCR_Puntos P " & _
                   "WHERE C.CodigoCauce = P.CodigoCauce and P.CodigoPVYCR = '" & Session("CodigoPVYCR").ToString & "' "
        
        da.SelectCommand.CommandText = sentenciaSel
        da.Fill(ds, "TablaGrafico")

        If Not Session("TablaCaudalesDesdeAcequias") Is Nothing Then
            CrearVariablesGráfico("TipoObtencionCaudal")
            CrearVariablesGráfico("Cod_Fuente_Dato")
            CrearVariablesGráfico("RegimenCurva")
        Else
            pnlCodigoFuenteDato.Visible = False
            CrearVariablesGráfico("TipoObtCaudal")
            CrearVariablesGráfico("Curva")
        End If

        Session("Titulo") = Session("CodigoPVYCR").ToString & " / " + ds.Tables("TablaGrafico").Rows(0).Item("DenominacionPunto").ToString & " / " & vbCrLf & ds.Tables("TablaGrafico").Rows(0).Item("DenominacionCauce").ToString
    End Sub


    Private Sub CrearVariablesGráfico(ByVal campo As String)
        Dim dt As New DataTable
        Dim dstPreproduccion As New DataSet
        If Not Session("TablaCaudalesDesdeAcequias") Is Nothing Then
            dt = Session("TablaCaudalesDesdeAcequias")
        Else
            dstPreproduccion = Session("TablaCaudales")
            dt = dstPreproduccion.Tables("TablaCaudales")
        End If


        dt.DefaultView.RowFilter = ""
        dt.DefaultView.Sort = campo
        ds.Tables.Add(campo)
        ds.Tables(campo).Columns.Add(New DataColumn(campo, GetType(System.String)))

        Dim i As Integer = 0, j As Integer = 0
        Dim strTipo As String = ""
        Dim chk As CheckBox

        For i = 0 To dt.Rows.Count - 1
            If strTipo <> utiles.nullABlanco(dt.DefaultView(i).Item(campo)) Then
                strTipo = dt.DefaultView(i).Item(campo)
                With ds.Tables(campo)
                    .Rows.Add()
                    .Rows(j).Item(campo) = strTipo

                    chk = New CheckBox
                    chk.ID = "chk" & "#" & campo & "#" & i
                    chk.Text = strTipo
                    chk.AutoPostBack = True
                    chk.CausesValidation = True
                    chk.Checked = True

                    Select Case campo
                        Case "TipoObtencionCaudal"
                            plh_TipoObtencionCaudal.Controls.Add(chk)
                        Case "TipoObtCaudal"
                            plh_TipoObtencionCaudal.Controls.Add(chk)
                        Case "Cod_Fuente_Dato"
                            plh_CodFuenteDato.Controls.Add(chk)
                        Case "RegimenCurva"
                            plh_RegimenCurva.Controls.Add(chk)
                        Case "Curva"
                            plh_RegimenCurva.Controls.Add(chk)
                    End Select

                    AddHandler chk.CheckedChanged, AddressOf chkClick
                End With
                j = j + 1
            End If
        Next
    End Sub

    Private Sub chkClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chk As CheckBox = sender
        Dim tipo As String = chk.ID.Split("#")(1)
        Select Case tipo
            Case "TipoObtencionCaudal"
                CalculaURL(1)
            Case "TipoObtCaudal"
                CalculaURL(1)
            Case "Cod_Fuente_Dato"
                CalculaURL(2)
            Case "RegimenCurva"
                CalculaURL(3)
            Case "Curva"
                CalculaURL(3)
        End Select
    End Sub

    Private Function CalculaURL(ByVal campo As Integer) As String
        url = "CurvasAcequiasPreproduccionFlashData.aspx?"
        Dim item As CheckBox

        Select Case campo
            Case 1
                For Each item In plh_TipoObtencionCaudal.Controls
                    url += "&" & item.Text & "=" & item.Checked
                Next
            Case 2
                For Each item In plh_CodFuenteDato.Controls
                    url += "&" & item.Text & "=" & item.Checked
                Next
            Case 3
                For Each item In plh_RegimenCurva.Controls
                    url += "&" & item.Text & "=" & item.Checked
                Next
        End Select

        Return url
    End Function
End Class
