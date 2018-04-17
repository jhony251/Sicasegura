Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles


Partial Class SICAH_InformacionGeneralC
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daCauces As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCauces As DataSet = New System.Data.DataSet()

    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True

        'ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        'ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        pnlCarpetaPDF.Visible = False
        pnlEDCauces.Visible = True

        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            'RDF. Fecha: 30/03/2009
            'Pdfs
            'Si el cauce es de 1er. nivel, tenemos carpeta, sino pdf
            If Session("NivelCauce") = "" Then
                Session("NivelCauce") = "1"
            End If

            If Not Page.Request.QueryString("X") Is Nothing Then
                Session("NivelCauce") = Page.Request.QueryString("X").ToString
            End If


            If Session("NivelCauce") <> "1" Then
                imgPDF.ImageUrl = "images/iconos/thumb_TN_psd0008.jpg"
                lbnPDF.Visible = True
            Else
                imgPDF.ImageUrl = "images/iconos/icoPestBandeja.gif"
                lbnPDF.Visible = True
            End If
            Session("Fichero") = ""
            Session("Carpeta") = ""
            Session("EnlaceC") = 0
            'Session("EnlaceP") = 1
            'Session("EnlaceE") = 2

            lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(Session("EnlaceC"), "../", Page, 11, "C", Session("ClaveNodo"), "N")
            crearDataSets()
            'CargarValoresBusqueda()
        End If
    End Sub
    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Private Sub crearDataSets()
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = " SELECT     CodigoCauce, CodigoInventario90, CodigoCampo, TipoCauce, MargenDerivacion, DenominacionCauce, OtrasReferencias, ParajeToma, MunicipioToma, " & _
                        " ProvinciaToma, DatosAdministrador, NombreTitular, NIFTitular, DireccionTitular, MunicipioTitular, CodPostalTitular, ProvinciaTitular, TelefonoTitular, " & _
                        " ReferenciaExpedienteLibroAprovechamiento, NumeroRegistroAntiguo, ReferenciaExpedientesRegistroAguas, OtrosExpedientes, Seccion, Tomo, Hoja, " & _
                        " NombreContactoCampo, DireccionContactoCampo, TelefonoContactoCampo, CaudalMaximo_LSeg, VolumenMaximoAnualLegal_M3, " & _
                        " VolumenMaximoAnualTeorico_M3, X_UTM_Toma, Y_UTM_Toma, CotaToma, SuperficieRealAproximada_HAS, SuperficieInscrita_HAS, " & _
                        " PorcentajeTradicional, TipoCultivo, LongitudCauce_KM, Observaciones, EntreOjosYContraparada, EnActivo, MedidoEnPVYCR, TitularContactado, " & _
                        " ContadorOK,INSCRIPCION, INSCRIPCION_ESTADO, INSCRIPCION_RELACIONADA  FROM PVYCR_Cauces where CodigoCauce='" & Session("ClaveNodo") & "' order by CodigoCauce "

        daCauces.SelectCommand.CommandText = sentenciaSel
        daCauces.Fill(dstCauces, "TablaCauces")
        cargarControlesEdicion()
        'Cálculo del número de páginas

        'rptCauces.DataSource = dstCauces.Tables("TablaCauces")
        'rptCauces.DataBind()

    End Sub

    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function
    Protected Sub cargarControlesEdicion()
        Dim CaucePadre As String = ""
        Try
            'EGB Jun-08: Esta parte se excinde de la funcion rptCauces_ItemCommand para reutilizacion en asociacion de mantenimientos
            If dstCauces.Tables("TablaCauces").Rows.Count > 0 Then
                'Datos del Cauce
                Me.txtcodigoCauce.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("CodigoCauce"))
                Me.txtdenominacion.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("DenominacionCauce"))
                Me.txtcodinventario.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("CodigoInventario90"))
                'RDF. Fecha: 30/03/2009
                'PDFs
                '**********************************************
                '**********************************************
                Dim comando As SqlCommand = New SqlCommand("", conexion)
                If Session("NivelCauce") = "1" And ExisteCarpeta(Server.MapPath("Fichas90") & "\" & Me.txtcodinventario.Text) Then
                    imgPDF.Visible = True
                    lbnPDF.Visible = True
                    Session("Carpeta") = ConfigurationSettings.AppSettings("ruta_pdf") & Me.txtcodinventario.Text
                Else

                    If Session("NivelCauce") <> "1" Then
                        utiles.Comprobar_Conexion_BD(Page, conexion)
                        'Cauce padre
                        If Me.txtcodigoCauce.Text.Contains("-") Then
                            CaucePadre = Me.txtcodigoCauce.Text.Substring(0, Me.txtcodigoCauce.Text.IndexOf("-"))
                        Else
                            CaucePadre = Me.txtcodigoCauce.Text
                        End If

                        'Se obtiene el CodigoInventario90
                        comando.CommandText = "SELECT CodigoInventario90 FROM PVYCR_Cauces WHERE codigoCauce='" & CaucePadre & "'"
                        Dim CodInv As String = ""
                        CodInv = utiles.nullABlanco(comando.ExecuteScalar())

                        If ExistePDF(Server.MapPath("Fichas90") & "\" & CodInv & "\" & Me.txtcodinventario.Text & ".pdf") Then
                            imgPDF.Visible = True
                            lbnPDF.Visible = True
                            Session("Fichero") = ConfigurationSettings.AppSettings("ruta_pdf") & CodInv & "/" & Me.txtcodinventario.Text & ".pdf"
                        End If
                    End If
                End If
                '**********************************************
                '**********************************************
                Me.txtcodcampo.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("CodigoCampo"))
                Me.txtTipoToma.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("TipoCauce"))
                Me.txtMargenDeriv.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("MargenDerivacion"))
                Me.txtOtrasRef.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("OtrasReferencias"))
                Me.txtParaje.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("ParajeToma"))


                Me.txtMunicipio.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("MunicipioToma"))
                Me.txtProvincia.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("ProvinciaToma"))
                Me.txtAdministrador.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("DatosAdministrador"))
                Me.txtTitular.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("NombreTitular"))
                Me.txtNIF.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("NIFTitular"))
                Me.txtDireccion.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("DireccionTitular"))
                Me.txtMunicipioTit.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("MunicipioTitular"))
                Me.txtCP.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("CodPostalTitular"))

                Me.txtProvinciaTit.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("ProvinciaTitular"))
                Me.txtTelefono.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("TelefonoTitular"))

                Me.txtExpediente.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("ReferenciaExpedienteLibroAprovechamiento"))
                Me.txtRegAntiguo.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("NumeroRegistroAntiguo"))
                Me.txtExptesLibro.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("ReferenciaExpedientesRegistroAguas"))
                Me.txtOtrosExpedientes.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("OtrosExpedientes"))
                Me.txtSeccion.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("Seccion"))


                Me.txtTomo.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("Tomo"))
                Me.txtFolio.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("Hoja"))
                Me.txtContacto.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("NombreContactoCampo"))
                Me.txtDireccionCont.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("DireccionContactoCampo"))
                Me.txtTelContacto.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("TelefonoContactoCampo"))
                Me.txtCaudal.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("CaudalMaximo_LSeg"))
                Me.txtVolumenLegal.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("VolumenMaximoAnualLegal_M3"))
                Me.txtVolumenAnual.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("VolumenMaximoAnualTeorico_M3"))

                Me.txtX.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("X_UTM_Toma"))
                Me.txtY.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("Y_UTM_Toma"))
                Me.txtCota.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("CotaToma"))
                Me.txtSupReal.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("SuperficieRealAproximada_HAS"))
                Me.txtSupInscrita.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("SuperficieInscrita_HAS"))
                Me.txtTradicional.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("PorcentajeTradicional"))
                Me.txtTipoCultivo.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("TipoCultivo"))
                Me.txtLongitudCauce.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("LongitudCauce_KM"))
                Me.txtObserva.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("Observaciones"))

                If utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("EntreOjosYContraparada")) = "1" Then
                    Me.chkEntreOjos.Checked = True
                Else
                    Me.chkEntreOjos.Checked = False
                End If

                If utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("EnActivo")) = "1" Then
                    Me.chkenActivo.Checked = True
                Else
                    Me.chkenActivo.Checked = False
                End If

                If utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("MedidoEnPVYCR")) = "1" Then
                    Me.chkMedido.Checked = True
                Else
                    Me.chkMedido.Checked = False
                End If
                If (utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("TitularContactado")) = "1") Then
                    Me.chkTitular.Checked = True
                Else
                    Me.chkTitular.Checked = False
                End If

                If utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("ContadorOK")) = "1" Then
                    Me.chkContadorOK.Checked = True
                Else
                    Me.chkContadorOK.Checked = False
                End If
                Me.txtInscripcion.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("INSCRIPCION"))
                Me.txtInscripcionEstado.Text = ValorInscripcionEstado(utiles.nullACero(dstCauces.Tables("TablaCauces").Rows(0).Item("INSCRIPCION_ESTADO")))
                Me.txtInscripcionRelacionada.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("INSCRIPCION_RELACIONADA"))

            End If
        Catch ex As System.Data.SqlClient.SqlException


        End Try

    End Sub

    Private Sub CargarValoresBusqueda()
        Dim NombreElemento, TipoElementoMedida As String
        'NCM 05/09/2008 Cargamos la búsqueda con el código del arbol

        txtCodigoPVYCR.Text = Session("codigoPVYCR")

        txtIdElementoMedida.Text = Session("idElementomedida")

        Select Case TipoElementoMedida
            Case "Q"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - CAUDAL"
            Case "V"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - VOLUMEN"
            Case "E"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - ENERGIA"
            Case "H"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - HORAS"
            Case Else
                NombreElemento = ""
        End Select
        txtDescripcionElementoMedida.Text = NombreElemento
    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim nombreinforme = "../listados/InformeCauces.aspx?nodoClave=" + utiles.BlancoANull(Me.txtcodigoCauce.Text)
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub imgPDF_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPDF.Click
        'Abrir diálogo

        If Session("Fichero") <> "" Then

            ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" & Session("Fichero") & "','null','location=no,resizable=yes')</script>")

        Else
            If Session("Carpeta") <> "" Then

                pnlCarpetaPDF.Visible = True

                pnlEDCauces.Visible = False
                CargarPDF()
            End If
        End If

    End Sub

    Protected Function ExisteCarpeta(ByVal ruta As String) As Boolean
        'RDF 30/03/2009
        If System.IO.Directory.Exists(ruta) Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Function ExistePDF(ByVal ruta As String) As Boolean
        'RDF 30/03/2009
        If System.IO.File.Exists(ruta) Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub CargarPDF()
        'bllInv.Visible = True
        Dim archivo As String = ""
        Dim cadena As String = ""
        Dim file As String = ""
        Dim strHTML As String

        strHTML = "<table width='100%' align='center'><tr>"
        'Se rellena la lista con los ficheros
        Dim lista1() As String
        Dim carpeta As String = ""

        carpeta = Session("Carpeta").ToString.Replace(ConfigurationSettings.AppSettings("ruta_pdf"), "")
        lblPDF.Text = "Contenido de la carpeta: " & carpeta
        carpeta = MapPath("Fichas90").ToString & "\" & carpeta



        lista1 = System.IO.Directory.GetFiles(carpeta)
        Dim Contador As Integer = 1
        For Each file In lista1
            archivo = System.IO.Path.GetFileName(file)
            cadena = Session("Carpeta") & "/" & archivo

            If (archivo.IndexOf("pdf") > 0) Then

                strHTML &= "<td align='center' valign='bottom' style='text-align:center;'><a href='" & cadena & "' class='vertical' target='_blank' ><img src='images/iconos/thumb_TN_psd0008.jpg' border=0><br>" & archivo & "</a></td>"
            End If
            Contador = Contador + 1
            If (Contador - 1) Mod 5 = 0 Then
                strHTML &= "</tr><tr></tr><tr></tr><tr>"
            End If
        Next
        strHTML &= "</tr></table>"
        lblHTML.Text = strHTML
    End Sub



    Protected Sub lbnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbnPDF.Click
        If Session("Fichero") <> "" Then

            ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" & Session("Fichero") & "','null','location=no,resizable=yes')</script>")

        Else
            If Session("Carpeta") <> "" Then

                pnlCarpetaPDF.Visible = True

                pnlEDCauces.Visible = False
                CargarPDF()
            End If
        End If
    End Sub

    Private Function ValorInscripcionEstado(ByVal valor As Int16) As String
        Dim resultado As String

        Select Case valor
            Case 1
                resultado = "Anulado"
            Case 2
                resultado = "Vigente"
            Case Else
                resultado = ""
        End Select
        Return resultado
    End Function
End Class
