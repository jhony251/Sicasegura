Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles

Partial Class SICAH_PantallaInformes
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim sentenciaSel As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")


        'ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        'ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")

        If Not IsPostBack Then
            imgCalFechaFinI.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinI.ClientID & "'),'dd/mm/yyyy');")
            imgCalFechaIniI.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniI.ClientID & "'),'dd/mm/yyyy');")


            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            If Session("claveTipo") = "C" Then
                lblNodo.Text = "Cauce - " + Session("ClaveNodo")
            ElseIf Session("claveTipo") = "P" Then
                lblNodo.Text = "Punto - " + Session("ClaveNodo")
            End If
            txtNodoClave.Text = Session("NodoClave")

            'egb corrección 10/11/2008
            If Session("claveTipo") = "E" Then Session("CodigoPVYCR") = Session("NodoClave").Substring(0, Session("NodoClave").Length - 4)
            If Left(Session("idElemento"), 1) = "H" Then
                Informesel.Text = "DocumentacionElemento"
            Else
                Informesel.Text = "Consumo"
            End If

            txtNodoTexto.Text = Request.QueryString("NodoTexto")
            crearDataSets()
            CargarValores()
            btnCaudalesFiltradosI.Attributes.Add("onclick", "abreInforme();return false;")

            'If utiles.nullACero(Session("Enlace" & Session("claveTipo"))) = 0 Then
            Session("Enlace" & Session("claveTipo")) = 3
            'End If
            'Comentado por IPIM: Esta línea la había añadido Nayra pq estaba haciendo pruebas.
            'IPIM 16/10/2008
            'lblPestanyasArbol.Text = genHTML_NCM.EnlacesMenuArbol(Session("Enlace" & Session("claveTipo")), "../", Page, 11, Session("claveTipo"), Session("ClaveNodo"), "N")
            lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(Session("Enlace" & Session("claveTipo")), "../", Page, 11, Session("claveTipo"), Session("ClaveNodo"), "N")
            If Session("claveTipo") = "E" Then
                CargarValoresBusqueda()
            End If


            'asignamos el dataset a una variable global     'Para el informe de Curvas Caudales Acequias
            Dim dtLectSel As New System.Data.DataTable
            Dim dt As DataTable = New DataTable()
            Try
                dt = Cache("dst")

                Dim dr() As System.Data.DataRow
                dtLectSel = dt.Clone()

                dr = dt.DefaultView.Table.Select
                If Session("LecturasSeleccionadas") <> "" Then
                    dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")
                    dr = dt.DefaultView.Table.Select(" " & Replace(UCase(Left(Mid(Session("LecturasSeleccionadas"), 7), (Mid(Session("LecturasSeleccionadas"), 7)).ToString.Length - 1)), "D.", "") & " ")

                    Dim fila As DataRow
                    For Each fila In dr
                        dtLectSel.Rows.Add(fila.ItemArray)
                    Next
                    Session("TablaCaudalesDesdeAcequias") = dtLectSel
                Else
                    Session("TablaCaudalesDesdeAcequias") = Cache("dst")
                End If

            Catch ex As Exception
                'IPIM 30/10/2008 Aquí no ponemos que se ha perdido la sesión pq puede suceder que se entre directamente a informes sin pasar
                'por las lecturas.
                Exit Sub
            End Try
        End If
    End Sub

    'Protected Sub VolumenesDiarios()
    'Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
    '                  "window.open('../listados/ListadoVolumenesDias.aspx?codigoPVYCR=" & Session("ClaveNodo") & "&idElementoMedida=" & Session("idElemento") & _
    '          "&FiltroNregQ=" & txtFiltroNregQ.Text & "&FiltroNulasQ=" & chkFiltroNulasQ.Checked & _
    '          "&filtroFechaIni=" & txtfiltroFechaIni.Text & "&FiltroFechaFin=" & txtFiltroFechaFin.Text & _
    '          "&FiltrarCodFuenteDato=" & txtFiltrarCodFuenteDato.Text & "','_blank','')" & _
    '          "</script>")

    'End Sub


    Private Sub crearDataSets()
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = "SELECT * FROM dbo.PVYCR_ElementosMedida where CodigoPVYCR = '" & Session("ClaveNodo") & "' and idelementoMedida = '" & Session("idElemento") & "' "

        daElementos.SelectCommand.CommandText = sentenciaSel
        daElementos.Fill(dstElementos, "TablaElementos")

        'rptElementos.DataSource = dstElementos.Tables("TablaElementos")
        'rptElementos.DataBind()

    End Sub
    Protected Sub CargarValores()
        If dstElementos.Tables("TablaElementos").Rows.Count > 0 Then
            'Datos del elemento
            '            ddlcodigoPVYCR.Items.Clear()
            '           ddlcodigoPVYCR.Items.Add(New System.Web.UI.WebControls.ListItem(dstElementos.Tables("TablaElementos").Rows(0).Item("CodigoPVYCR").ToString))
            Me.lblNodo.Text = dstElementos.Tables("TablaElementos").Rows(0).Item("CodigoPVYCR").ToString
            Me.lblNodo.Text = Me.lblNodo.Text + "-" + utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("IdElementoMedida"))
            txtCodigoPVYCR.Text = dstElementos.Tables("TablaElementos").Rows(0).Item("CodigoPVYCR").ToString
            txtIdElementoMedida.Text = dstElementos.Tables("TablaElementos").Rows(0).Item("IdElementoMedida")
            '          Me.ddltipo.Text = utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo"))
            If utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo")) = "Q" Then
                Me.lblNodo.Text = Me.lblNodo.Text + " - " + "Caudal"
            ElseIf utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo")) = "H" Then
                Me.lblNodo.Text = Me.lblNodo.Text + " - " + "Horas"
            ElseIf utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo")) = "E" Then
                Me.lblNodo.Text = Me.lblNodo.Text + " - " + "Energía"
            ElseIf utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo")) = "V" Then
                Me.lblNodo.Text = Me.lblNodo.Text + " - " + "Volumen"
            ElseIf utiles.nullABlanco(dstElementos.Tables("TablaElementos").Rows(0).Item("tipo")) = "D" Then
                Me.lblNodo.Text = Me.lblNodo.Text + " - " + "Diferencial"
            End If
            txtNodoTexto.Text = Me.lblNodo.Text


        End If
    End Sub

    Protected Function verVisibilidad(ByVal nombre) As String
        'IPIM: 28/08/2008 Para mostrar el informe de consumos siempre
        If Session("claveTipo") <> "E" Then
            Select Case nombre
                Case "GraficosL"
                    Return "none"
                Case "InformesL"
                    Return "none"
                Case "GraficosC"
                    Return "none"
                Case "InformesC"
                    Return "block"
                Case "DocumentacionPunto"
                    If Session("claveTipo") = "P" Then
                        Return "block"
                    Else
                        Return "none"
                    End If
                Case "GraficosCA"
                    Return "none"
                Case "Consumo"
                    Return "block"
                Case "DocumentacionElemento"
                    Return "none"

                Case "DocumentacionCauce"
                    If Session("claveTipo") = "C" Then
                        If Session("claveNodo") <> "P" And Session("claveNodo") <> "ARG" And Session("claveNodo") <> "CM" And Session("claveNodo") <> "OT" And Session("claveNodo") <> "S" And Session("claveNodo") <> "VA" And Session("claveNodo") <> "VB" And Session("claveNodo") <> "VM" Then
                            Return "block"
                        Else
                            Return "none"
                        End If
                    Else
                        Return "none"
                    End If
                Case "FiltroFechas3"
                    If Session("clavenodo").ToString.Substring(0, 1) = "P" Or Session("clavenodo").ToString.Substring(0, 1) = "T" Then
                        Return "none"
                    Else
                        Return True
                    End If

                Case "FiltroFechas4"
                    'ncm 30/03/2009 si el cauce o el punto son peajes o trasvases sólo se mostrará el filtro mensual o anual
                    If Session("clavenodo").ToString.Substring(0, 1) = "P" Or Session("clavenodo").ToString.Substring(0, 1) = "T" Then
                        Return "block"
                    Else
                        Return "none"
                    End If
            End Select
        Else
            Select Case nombre
                Case "InformesL"
                    If Left(Session("idElemento"), 1) = "H" Then
                        Return "none"
                    Else
                        Return True
                    End If
                Case "GraficosC"
                    If Left(Session("idElemento"), 1) = "H" Then
                        Return "none"
                    Else
                        Return True
                    End If
                Case "InformesC"
                    If Left(Session("idElemento"), 1) = "H" Then
                        Return "none"
                    Else
                        Return True
                    End If
                Case "Consumo"
                    If Left(Session("idElemento"), 1) = "H" Then
                        Return "none"
                    Else
                        Return True
                    End If
                Case "GraficosCA"
                    If Left(Session("idElemento"), 1) = "V" Or Left(Session("idElemento"), 1) = "E" Or Left(Session("idElemento"), 1) = "H" Or Left(Session("idElemento"), 1) = "D" Then
                        Return "none"
                    Else
                        Return True
                    End If
                Case "GraficosL"
                    If Left(Session("idElemento"), 1) = "V" Or Left(Session("idElemento"), 1) = "E" Or Left(Session("idElemento"), 1) = "H" Or Left(Session("idElemento"), 1) = "D" Then
                        Return "none"
                    Else
                        Return "block"
                    End If
                Case "DocumentacionElemento"
                    If Left(Session("idElemento"), 1) = "H" Or Left(Session("idElemento"), 1) = "D" Then
                        Return "none"
                    Else
                        Return True
                    End If
                Case "DocumentacionPunto"
                    Return "none"
                Case "DocumentacionCauce"
                    Return "none"
                Case "FiltroFechas1"
                    If Left(Session("idElemento"), 1) = "H" Then
                        Return "none"
                    Else
                        Return True
                    End If
                Case "FiltroFechas2"
                    If Left(Session("idElemento"), 1) = "H" Then
                        Return "none"
                    Else
                        Return True
                    End If
                Case "FiltroFechas3"
                    If Left(Session("idElemento"), 1) = "H" Or Left(Session("idElemento"), 1) = "D" Then
                        Return "none"
                    Else
                        Return True
                    End If
                Case "FiltroFechas4"
                    If Left(Session("idElemento"), 1) = "D" Then
                        Return True
                    Else
                        Return "none"
                    End If
                Case Else
                    Return True
            End Select
        End If
    End Function
    Private Sub CargarValoresBusqueda()
        Dim NombreElemento, TipoElementoMedida As String
        'NCM 05/09/2008 Cargamos la búsqueda con el código del arbol

        txtCodigoPVYCRB.Text = Session("ClaveNodo")

        txtIdElementoMedidaB.Text = Session("idElemento")
        TipoElementoMedida = Left(txtIdElementoMedidaB.Text, 1)

        Select Case TipoElementoMedida
            Case "Q"
                NombreElemento = txtCodigoPVYCRB.Text & " - " & txtIdElementoMedidaB.Text & " - CAUDAL"
            Case "V"
                NombreElemento = txtCodigoPVYCRB.Text & " - " & txtIdElementoMedidaB.Text & " - VOLUMEN"
            Case "E"
                NombreElemento = txtCodigoPVYCRB.Text & " - " & txtIdElementoMedidaB.Text & " - ENERGIA"
            Case "H"
                NombreElemento = txtCodigoPVYCRB.Text & " - " & txtIdElementoMedidaB.Text & " - HORAS"
            Case "D"
                NombreElemento = txtCodigoPVYCRB.Text & " - " & txtIdElementoMedidaB.Text & " - DIFERENCIAL"
            Case Else
                NombreElemento = ""
        End Select
        txtDescripcionElementoMedida.Text = NombreElemento
    End Sub

    Protected Sub ddlIntervaloD_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlIntervaloD.SelectedIndexChanged
        'ncm si cambia el valor rellenamos el ddlintervaloi que es el que se utiliza en el código javascript de abreInforme para no tener que cambiar este código
        ddlIntervaloI.Items(0).Value = ddlIntervaloD.Items(0).Value
    End Sub
End Class
