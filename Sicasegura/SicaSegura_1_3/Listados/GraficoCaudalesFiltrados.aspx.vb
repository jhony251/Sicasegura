Imports System.Data
Imports GuarderiaFluvial

Partial Class Listados_GraficoCaudalesFiltrados
    Inherits System.Web.UI.Page
    Dim dst As DataSet = New System.Data.DataSet()


    Protected Function getUrlGrafico(ByVal grafico) As String
        Dim url As String
        url = "GraficoCaudalesFiltradosData.aspx?grafico=" & grafico

        Select Case grafico
            Case 1
                url += "&Var1=" & chkVar1.Checked
                url += "&Var2=" & chkVar2.Checked
                url += "&Var3=" & chkVar3.Checked
            Case 2
                url += "&Var4=" & chkVar4.Checked
                url += "&Var5=" & chkVar5.Checked
            Case 3
                url += "&Var1=true"
            Case 4
                url += "&Var2=" & chkVar2E.Checked
                url += "&Var3=" & chkVar3E.Checked
        End Select

        Return Server.UrlEncode(url)
    End Function

    Protected Function verSiEsEnergia() As Boolean
        Dim NombreElemento As String
        Dim separadores As Integer = 0
        Dim guiones As Integer = 0
        Dim V() As String
        Dim IdElementoMedida As String
        V = Split(Session("nodotexto"), "-")
        separadores = Math.Max(Split(Session("claveNodoTipo"), ";").Length - 1, 0)
        guiones = Math.Max(Split(Session("nodotexto"), "-").Length - 1, 0)
        NombreElemento = Replace(Session("nodotexto"), "<img src='images/distancias.gif' border=0>&nbsp;&nbsp;<font color=#008500>" & Session("claveNodoTipo").Substring(0, Session("claveNodoTipo").Length - 4) & "-", "")

        IdElementoMedida = NombreElemento.Substring(0, NombreElemento.ToString.IndexOf("-") - 1)
        IdElementoMedida = V(guiones - 1).ToString

        If Left(IdElementoMedida, 1) = "E" Then
            Return True
        Else
            Return False
        End If

    End Function
    Function obtenerTipoElemento() As String
        Dim NombreElemento As String
        Dim separadores As Integer = 0
        Dim guiones As Integer = 0
        Dim V() As String
        Dim IdElementoMedida As String
        V = Split(Session("nodotexto"), "-")
        separadores = Math.Max(Split(Session("claveNodoTipo"), ";").Length - 1, 0)
        guiones = Math.Max(Split(Session("nodotexto"), "-").Length - 1, 0)
        NombreElemento = Replace(Session("nodotexto"), "<img src='images/distancias.gif' border=0>&nbsp;&nbsp;<font color=#008500>" & Session("claveNodoTipo").Substring(0, Session("claveNodoTipo").Length - 4) & "-", "")

        IdElementoMedida = NombreElemento.Substring(0, NombreElemento.ToString.IndexOf("-") - 1)
        IdElementoMedida = V(guiones - 1).ToString

        Return Left(IdElementoMedida, 1)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            dst = Session("dst")
        Catch ex As Exception
            JavaScript.Alert(Page, "Por favor, vuelve a cargar los datos de las lecturas que ya no están en memoria")
            Response.End()
        End Try
        'ncm si el dataset no tiene valores que no continue 30/03/2009
        If dst Is Nothing Then
            JavaScript.Alert(Page, "Por favor, vuelve a cargar los datos de las lecturas que ya no están en memoria")
            Response.End()
        End If
        If dst.Tables.Contains("listadoGlobales") Then
            Dim Filtro1 As String = utiles.nullABlanco(dst.Tables("listadoGlobales").Rows(0).Item("Filtro1"))
            Dim Filtro2 As String = utiles.nullABlanco(dst.Tables("listadoGlobales").Rows(0).Item("Filtro2"))


            Filtro1 = Replace(Filtro1, vbCrLf, "<br>")

            lblFiltro1.Text = Filtro1
            If Filtro2 <> "" Then
                lblFiltro2.Text = Filtro2 & "<br>"
            End If

            If dst.Tables("listado").Rows.Count > 0 Then
                If verSiEsEnergia() = False Then
                    'ncm 16/04/2009 si es suministros tendremos 2 columnas y no tres por tanto el código ha de ser diferente
                    If obtenerTipoElemento() <> "D" Then
                        pnl_E.Visible = False
                        chkVar1.Text = dst.Tables("listado").Rows(0).Item("Var1Titulo")
                        chkVar2.Text = dst.Tables("listado").Rows(0).Item("Var2Titulo")
                        chkVar3.Text = Replace(dst.Tables("listado").Rows(0).Item("Var3Titulo"), "V diferencial Acumulado", "Acumulado")
                        chkVar4.Text = dst.Tables("listado").Rows(0).Item("Var4Titulo")
                        chkVar5.Text = Replace(dst.Tables("listado").Rows(0).Item("Var5Titulo"), "V diferencial Acumulado", "Acumulado")
                        If dst.Tables("listado").Rows(0).Item("Var4Titulo") = "" Then
                            lblTotales.Text = "Total " & dst.Tables("listado").Rows(0).Item("Var3Titulo") & ": " & String.Format("{0:#,##0.##}", Convert.ToDouble(dst.Tables("listado").Rows(dst.Tables("listado").Rows.Count - 1).Item("Var3Valor")))
                        Else
                            lblTotales.Text = "Total " & dst.Tables("listado").Rows(0).Item("Var5Titulo") & ": " & String.Format("{0:#,##0.##}", Convert.ToDouble(dst.Tables("listado").Rows(dst.Tables("listado").Rows.Count - 1).Item("Var5Valor")))
                        End If
                        If dst.Tables("listado").Rows(0).Item("Var4Visible") = "false" Then
                            pnl_Diferenciales.Visible = False
                        End If

                        'EGB 06-03-09 Modificación de Formatos 
                        lblLeyendaElectricos.Visible = False
                        chkVar4.ForeColor = Drawing.Color.Black
                        chkVar5.ForeColor = Drawing.Color.Black
                    Else
                        pnl_E.Visible = False
                        chkVar1.Text = dst.Tables("listado").Rows(0).Item("Var1Titulo")
                        chkVar2.Text = dst.Tables("listado").Rows(0).Item("Var2Titulo")
                        chkVar3.Visible = False

                        lblTotales.Text = "Total " & dst.Tables("listado").Rows(0).Item("Var2Titulo") & ": " & String.Format("{0:#,##0.##}", Convert.ToDouble(dst.Tables("listado").Rows(dst.Tables("listado").Rows.Count - 1).Item("Var2Valor")))

                        If dst.Tables("listado").Rows(0).Item("Var4Visible") = "false" Then
                            pnl_Diferenciales.Visible = False
                        End If

                        'EGB 06-03-09 Modificación de Formatos 
                        lblLeyendaElectricos.Visible = False
                        chkVar4.ForeColor = Drawing.Color.Black
                        chkVar5.ForeColor = Drawing.Color.Black
                    End If
            Else
                pnl_VyQ.Visible = False
                lblVar1E.Text = dst.Tables("listado").Rows(0).Item("Var1Titulo")
                chkVar2E.Text = dst.Tables("listado").Rows(0).Item("Var2Titulo")
                chkVar3E.Text = Replace(dst.Tables("listado").Rows(0).Item("Var3Titulo"), "V diferencial Acumulado", "Acumulado")
                chkVar4.Text = dst.Tables("listado").Rows(0).Item("Var4Titulo")
                chkVar5.Text = Replace(dst.Tables("listado").Rows(0).Item("Var5Titulo"), "V diferencial Acumulado", "Acumulado")
                lblTotales.Text = "Total " & dst.Tables("listado").Rows(0).Item("Var5Titulo") & ": " & String.Format("{0:#,##0.##}", Convert.ToDouble(dst.Tables("listado").Rows(dst.Tables("listado").Rows.Count - 1).Item("Var5Valor")))

                'EGB 06-03-09 Modificación de Formatos 
                lblLeyendaElectricos.Visible = True
                chkVar4.ForeColor = Drawing.Color.DarkRed
                chkVar5.ForeColor = Drawing.Color.DarkRed
            End If

                lblTotales.Text = lblTotales.Text & "<br>Número de lecturas en el intervalo: " & String.Format("{0:#,##0.##}", Convert.ToDouble(dst.Tables("tablaAuxiliar").Rows.Count))
            Else
                JavaScript.Alert(Page, "No existe ningún registro")
                Response.End()
            End If
        Else
            JavaScript.Alert(Page, "No existe ningún registro")
            Response.End()
        End If
    End Sub

    Protected Sub btnAmpliar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAmpliar.Click
        Dim url2 = getUrlGrafico(1)
        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open(""GraficoAmpliado.aspx?url=" & url2 & """)</script>")
    End Sub

    Protected Sub btnAmpliarDiferenciales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAmpliarDiferenciales.Click
        Dim url2 = getUrlGrafico(2)
        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open(""GraficoAmpliado.aspx?url=" & url2 & """)</script>")
    End Sub

    Protected Sub btnAmpliarE1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAmpliarE1.Click
        Dim url2 = getUrlGrafico(3)
        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open(""GraficoAmpliado.aspx?url=" & url2 & """)</script>")
    End Sub

    Protected Sub btnAmpliarEnergia2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAmpliarEnergia2.Click
        Dim url2 = getUrlGrafico(4)
        ClientScript.RegisterStartupScript(Page.GetType, "abreGrafico", "<script language=javascript>window.open(""GraficoAmpliado.aspx?url=" & url2 & """)</script>")
    End Sub
End Class
