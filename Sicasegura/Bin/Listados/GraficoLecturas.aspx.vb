Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.SICA_GestionArboles

Partial Class Listados_GraficoLecturas
    Inherits System.Web.UI.Page
    Dim dt As New System.Data.DataTable()
    Dim dtLectSel As New System.Data.DataTable

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim da As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New System.Data.DataSet()
    Dim url As String
    Dim sentenciaSel, sentenciaOrder, sentenciaSelCount As String

    Dim FiltroFechaIni, FiltroFechaFin As Date
    Dim FiltroNulas As Boolean
    Dim Filtro, NodoTexto, NodoClave As String

    Protected Function getUrlGrafico(ByVal campo As Integer, ByVal tipoGrafico As Integer) As String
        Session("nodoTexto") = Request.QueryString("nodoTexto")
        Session("ClaveNodoTipo") = Request.QueryString("nodoclave")
        Session("ClaveNodo") = Session("ClaveNodoTipo").Substring(0, Session("ClaveNodoTipo").IndexOf(";"))
        Select Case campo
            Case 1
                CalculaURL(1)
                url = url & "&grafico=TipoObtencionCaudal&tipoGrafico=" & tipoGrafico & "&filtroFechaIni=" & Request.QueryString("filtroFechaIni") & "&filtroFechaFin=" & Request.QueryString("filtroFechaFin")
            Case 2
                CalculaURL(2)
                url = url & "&grafico=Cod_Fuente_Dato&tipoGrafico=" & tipoGrafico & "&filtroFechaIni=" & Request.QueryString("filtroFechaIni") & "&filtroFechaFin=" & Request.QueryString("filtroFechaFin")
            Case 3
                CalculaURL(3)
                url = url & "&grafico=RegimenCurva&tipoGrafico=" & tipoGrafico & "&filtroFechaIni=" & Request.QueryString("filtroFechaIni") & "&filtroFechaFin=" & Request.QueryString("filtroFechaFin")
        End Select

        Return Server.UrlEncode(url)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("../sicah/UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
        End If

        Try
            Response.CacheControl = "no-cache"
            dt = Session("dst")
        Catch ex As Exception
            JavaScript.Alert(Page, "Por favor, vuelve a cargar los datos de las lecturas que ya no están en memoria")
            Response.End()
        End Try

        Dim dr() As System.Data.DataRow
        dtLectSel = dt.Clone()

        dr = dt.DefaultView.Table.Select
        If Session("LecturasSeleccionadas") <> "" Then
            dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")
            dr = dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")
        End If

        Dim fila As DataRow
        For Each fila In dr
            dtLectSel.Rows.Add(fila.ItemArray)
        Next

        If Session("TextoError") <> "" Then
            JavaScript.Alert(Page, Session("TextoError"))
        End If

        CrearVariablesGráfico("TipoObtencionCaudal")
        CrearVariablesGráfico("Cod_Fuente_Dato")
        CrearVariablesGráfico("RegimenCurva")


    End Sub

    Private Sub CrearVariablesGráfico(ByVal campo As String)
        dtLectSel.DefaultView.RowFilter = ""
        dtLectSel.DefaultView.Sort = campo
        dst.Tables.Add(campo)
        dst.Tables(campo).Columns.Add(New DataColumn(campo, GetType(System.String)))

        Dim i As Integer = 0, j As Integer = 0
        Dim strTipo As String = ""
        Dim chk As CheckBox

        For i = 0 To dtLectSel.DefaultView.Count - 1
            If strTipo <> utiles.nullABlanco(dtLectSel.DefaultView(i).Item(campo)) Then
                strTipo = dtLectSel.DefaultView(i).Item(campo)
                With dst.Tables(campo)
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
                        Case "Cod_Fuente_Dato"
                            plh_CodFuenteDato.Controls.Add(chk)
                        Case "RegimenCurva"
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
            Case "Cod_Fuente_Dato"
                CalculaURL(2)
            Case "RegimenCurva"
                CalculaURL(3)
        End Select
    End Sub

    Private Function CalculaURL(ByVal campo As Integer) As String
        url = "GraficoLecturasData.aspx?"
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

    Protected Sub btnAmpliarTipoObtencionCaudal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAmpliarTipoObtencionCaudal.Click
        Dim url2 = getUrlGrafico(1, 0)
        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open(""GraficoAmpliado.aspx?url=" & url2 & """)</script>")
    End Sub

    Protected Sub btnAmpliarCodFuenteDato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAmpliarCodFuenteDato.Click
        Dim url2 = getUrlGrafico(2, 0)
        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open(""GraficoAmpliado.aspx?url=" & url2 & """)</script>")
    End Sub

    Protected Sub btnAmpliarRegimenCurva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAmpliarRegimenCurva.Click
        Dim url2 = getUrlGrafico(3, 0)
        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open(""GraficoAmpliado.aspx?url=" & url2 & """)</script>")
    End Sub

End Class

