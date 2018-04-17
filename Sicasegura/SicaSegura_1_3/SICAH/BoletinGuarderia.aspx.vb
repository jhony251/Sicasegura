Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.SICA_GestionArboles
Imports GuarderiaFluvial.utiles
Imports System
Imports System.IO
Imports System.Drawing

Partial Class SICAH_BoletinGuarderia
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daBoletines As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstBoletines As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim daCodigos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCodigos As DataSet = New System.Data.DataSet()
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lbtNuevo.DataBind()
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        lblCabecera.Text = genHTML.cabecera(Page)
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If

            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))

            'scripts de cliente 
            Me.imgFecha.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFecha.ClientID & "'),'dd/mm/yyyy');")
            Me.imgFFechaFinal.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaFinal.ClientID & "'),'dd/mm/yyyy');")
            Me.imgFFechaInicial.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaInicial.ClientID & "'),'dd/mm/yyyy');")
            Me.imgPlus.Attributes.Add("onclick", "mostrar_panel_imagenes();")

            imgArbol.Attributes.Add("onclick", "desplegarArbol(this);")
            '---------------------------------------------------------------------------------------------------------------------
            If Request.QueryString("nuevo") = "yes" Then

                pnlEDBoletines.Visible = True
                pnlBoletines.Visible = False
                'ucPaginacion.Visible = False
                lblTitulo.Text = "<b>&nbsp;NUEVO BOLETIN</b>"

                ''EGB 24/11/2008 Asociación con el Arbol de Cauces 
                'If treeView1.Nodes.Count = 0 Then
                '    'EGB el nuevo parametro P hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
                '    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "E")
                '    'Registramos los componenetes del arbol
                '    imgArbol.Attributes.Add("onclick", "desplegarArbol(this);")
                '    imgCerrarVentana.Attributes.Add("onclick", "desplegarArbol(this);")
                '    '---------------------------------------------------------------------
                'End If

                crearDataSets()
                rellenarListas()
                Exit Sub
            End If

            If Request.QueryString("NumRef") = "0" Then
                pnlEDBoletines.Visible = False
                pnlBoletines.Visible = True
                lblTitulo.Text = "<b>&nbsp;MANTENIMIENTO BOLETINES</b>"
                crearDataSets()
                rellenarListas()
                Exit Sub
            End If

            'egb 10/11/2008 inclusión de la querystring el parametro de acceso para la edición desde Administracion 
            If Request.QueryString("NumRef") <> "0" Then
                
                pnlEDBoletines.Visible = True
                pnlBoletines.Visible = False
                'ucPaginacion.Visible = False
                lblTitulo.Text = "<b>&nbsp;EDICIÓN BOLETIN REFERENCIA: " & Request.QueryString("NumRef") & "</b>"
                Me.lblNumRefSel.Text = Request.QueryString("NumRef")

                'EGB 24/11/2008 Asociación con el Arbol de Cauces 
                'If treeView1.Nodes.Count = 0 Then
                '    'EGB el nuevo parametro P hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
                '    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "E")
                '    'Registramos los componenetes del arbol
                '    imgArbol.Attributes.Add("onclick", "desplegarArbol(this);")
                '    imgCerrarVentana.Attributes.Add("onclick", "desplegarArbol(this);")
                '    '---------------------------------------------------------------------
                'End If

                crearDataSetBoletin(Request.QueryString("NumRef"))
                rellenarListas()
                RellenarControlesEdicionBoletin()
                RellenarPanelImagenes()

            End If

        End If
        'If Request.QueryString("NumRef") <> "0" Then
        '    RellenarPanelImagenes()
        'End If
    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function
    Protected Sub RellenarPanelImagenes()
        'EGB Test Refresco en el HREF
        If dstBoletines.Tables.Contains("TablaBoletinImages") = True Then
            'Refrescar
            dstBoletines.Tables("TablaBoletinImages").Clear()
        End If
        crearDataSetImagenesBoletin(Me.txtNumRef.Text)
        If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
            cargarImagenes(dstBoletines.Tables("TablaBoletinImages"))
        Else
            'No existen registros para visualizar
            cargarImagenesBlanco()
        End If
    End Sub
    Protected Sub cargarImagenesBlanco()
        Dim strHtml As String
        Dim archivo As String
        Dim file As String
        Dim cadena As String

        strHtml = ""
        file = Server.MapPath("tmp\SinFoto.jpg")
        cadena = Server.UrlEncode("\tmp\SinFoto.jpg")
        strHtml &= "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0>"

        'lblHTMLimgFotoGeneral.Text = strHtml
        'lblHTMLimgFotoDetalle.Text = strHtml
        'lblHTMLimgCroquisAcceso.Text = strHtml
        'lblHTMLimgCroquisExplicativo.Text = strHtml
        lblHTMLimgFoto1.Text = strHtml
        lblHTMLimgFoto2.Text = strHtml
        lblHTMLimgFoto3.Text = strHtml
        lblHTMLimgFoto4.Text = strHtml

    End Sub
    Protected Sub cargarImagenes(ByVal dt As DataTable)
        Dim SourceImage As Drawing.Bitmap
        Dim strHtml As String
        Dim archivo As String
        Dim file As String
        Dim cadena As String

        'Almacena la imagen en bytes de la base un Bitmap 
        strHtml = ""

        ''FOTO GENERAL------------------------------------------------------------
        'archivo = "Foto General"
        'If Not dt.Rows(0).Item("FotoGeneral") Is System.DBNull.Value Then
        '    SourceImage = Bytes2Image(CType(dt.Rows(0).Item("FotoGeneral"), Byte()))
        '    SaveJPG(SourceImage, Server.MapPath("tmp\FotoGeneral.jpg"))
        '    file = Server.MapPath("tmp\FotoGeneral.jpg")
        '    cadena = Server.UrlEncode("\tmp\FotoGeneral.jpg")
        '    strHtml &= "<a href='" & cadena & "' rel='lightbox_plus' target='_blank'>" & _
        '                      "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a>"
        '    SourceImage.Dispose()

        'Else
        '    file = Server.MapPath("tmp\SinFoto.jpg")
        '    cadena = Server.UrlEncode("\tmp\SinFoto.jpg")
        '    strHtml &= "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0>"

        'End If

        ''strHtml &= "<a href='" & cadena & "' rel='lightbox_plus' target='_blank'>" & _
        ''          "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a>"
        ''strHtml &= "<a href='" & cadena & "' rel='lightbox_plus' title='" & archivo & "'class='vertical' target='_blank'>" & _
        ''                      "image #1</a>"

        'lblHTMLimgFotoGeneral.Text = strHtml

        'strHtml = ""

        ''FOTO DETALLE------------------------------------------------------------
        'archivo = "Foto Detalle"
        'If Not dt.Rows(0).Item("FotoDetalle") Is System.DBNull.Value Then
        '    SourceImage = Bytes2Image(CType(dt.Rows(0).Item("FotoDetalle"), Byte()))
        '    SaveJPG(SourceImage, Server.MapPath("tmp\FotoDetalle.jpg"))

        '    file = Server.MapPath("tmp\FotoDetalle.jpg")
        '    cadena = Server.UrlEncode("\tmp\FotoDetalle.jpg")
        '    strHtml &= "<a href='" & cadena & "' rel='lightbox_plus' class='vertical' target='_blank'>" & _
        '         "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a>"
        '    SourceImage.Dispose()
        'Else
        '    file = Server.MapPath("tmp\SinFoto.jpg")
        '    cadena = Server.UrlEncode("\tmp\SinFoto.jpg")
        '    strHtml &= "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0>"

        'End If
        'lblHTMLimgFotoDetalle.Text = strHtml

        'strHtml = ""
        ''CROQUIS DE ACCESO------------------------------------------------------------
        'archivo = "Croquis de Acceso"
        'If Not dt.Rows(0).Item("CroquisAcceso") Is System.DBNull.Value Then
        '    SourceImage = Bytes2Image(CType(dt.Rows(0).Item("CroquisAcceso"), Byte()))
        '    SaveJPG(SourceImage, Server.MapPath("tmp\CroquisAcceso.jpg"))

        '    file = Server.MapPath("tmp\CroquisAcceso.jpg")
        '    cadena = Server.UrlEncode("\tmp\CroquisAcceso.jpg")
        '    strHtml &= "<a href='" & cadena & "' rel='lightbox_plus' class='vertical' target='_blank'>" & _
        '         "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a>"
        '    SourceImage.Dispose()
        'Else
        '    file = Server.MapPath("tmp\SinFoto.jpg")
        '    cadena = Server.UrlEncode("\tmp\SinFoto.jpg")
        '    strHtml &= "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0>"
        'End If
        'lblHTMLimgCroquisAcceso.Text = strHtml

        'strHtml = ""

        ''CROQUIS EXPLICATIVO------------------------------------------------------------
        'archivo = "Croquis Explicativo"
        'If Not dt.Rows(0).Item("CroquisExplicativo") Is System.DBNull.Value Then
        '    SourceImage = Bytes2Image(CType(dt.Rows(0).Item("CroquisExplicativo"), Byte()))
        '    SaveJPG(SourceImage, Server.MapPath("tmp\CroquisExplicativo.jpg"))

        '    file = Server.MapPath("tmp\CroquisExplicativo.jpg")
        '    cadena = Server.UrlEncode("\tmp\CroquisExplicativo.jpg")
        '    strHtml &= "<a href='" & cadena & "' rel='lightbox_plus' class='vertical' target='_blank'>" & _
        '         "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a>"
        '    SourceImage.Dispose()
        'Else
        '    file = Server.MapPath("tmp\SinFoto.jpg")
        '    cadena = Server.UrlEncode("\tmp\SinFoto.jpg")
        '    strHtml &= "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0>"
        'End If
        'lblHTMLimgCroquisExplicativo.Text = strHtml

        'strHtml = ""


        'FOTO #1------------------------------------------------------------
        archivo = "Foto 1"
        If Not dt.Rows(0).Item("Foto1") Is System.DBNull.Value Then
            SourceImage = Bytes2Image(CType(dt.Rows(0).Item("Foto1"), Byte()))
            SaveJPG(SourceImage, Server.MapPath("tmp\Foto1.jpg"))

            file = Server.MapPath("tmp\Foto1.jpg")
            cadena = Server.UrlEncode("\tmp\Foto1.jpg")
            strHtml &= "<a href='" & cadena & "' rel='lightbox_plus' class='vertical' target='_blank'>" & _
                 "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a>"
            SourceImage.Dispose()
        Else
            'ncm ponemos foto blanco para que no falle report
            file = Server.MapPath("tmp\SinFoto.jpg")
            cadena = Server.UrlEncode("\tmp\SinFoto.jpg")

            'file = Server.MapPath("tmp\SinFoto.jpg")
            'cadena = Server.UrlEncode("\tmp\SinFoto.jpg")
            strHtml &= "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0>"
        End If
        lblHTMLimgFoto1.Text = strHtml

        strHtml = ""

        'FOTO #2------------------------------------------------------------
        archivo = "Foto 2"
        If Not dt.Rows(0).Item("Foto2") Is System.DBNull.Value Then
            SourceImage = Bytes2Image(CType(dt.Rows(0).Item("Foto2"), Byte()))
            SaveJPG(SourceImage, Server.MapPath("tmp\Foto2.jpg"))

            file = Server.MapPath("tmp\Foto2.jpg")
            cadena = Server.UrlEncode("\tmp\Foto2.jpg")
            strHtml &= "<a href='" & cadena & "' rel='lightbox_plus' class='vertical' target='_blank'>" & _
                 "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a>"
            SourceImage.Dispose()
        Else
            file = Server.MapPath("tmp\SinFoto.jpg")
            cadena = Server.UrlEncode("\tmp\SinFoto.jpg")
            strHtml &= "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0>"
        End If
        lblHTMLimgFoto2.Text = strHtml

        strHtml = ""

        'FOTO #3------------------------------------------------------------
        archivo = "Foto 3"
        If Not dt.Rows(0).Item("Foto3") Is System.DBNull.Value Then
            SourceImage = Bytes2Image(CType(dt.Rows(0).Item("Foto3"), Byte()))
            SaveJPG(SourceImage, Server.MapPath("tmp\Foto3.jpg"))

            file = Server.MapPath("tmp\Foto3.jpg")
            cadena = Server.UrlEncode("\tmp\Foto3.jpg")
            strHtml &= "<a href='" & cadena & "' rel='lightbox_plus' class='vertical' target='_blank'>" & _
                 "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a>"
            SourceImage.Dispose()
        Else
            file = Server.MapPath("tmp\SinFoto.jpg")
            cadena = Server.UrlEncode("\tmp\SinFoto.jpg")
            strHtml &= "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0>"
        End If
        lblHTMLimgFoto3.Text = strHtml

        strHtml = ""

        'FOTO #4------------------------------------------------------------
        archivo = "Foto 4"
        If Not dt.Rows(0).Item("Foto4") Is System.DBNull.Value Then
            SourceImage = Bytes2Image(CType(dt.Rows(0).Item("Foto4"), Byte()))
            SaveJPG(SourceImage, Server.MapPath("tmp\Foto4.jpg"))

            file = Server.MapPath("tmp\Foto4.jpg")
            cadena = Server.UrlEncode("\tmp\Foto4.jpg")
            strHtml &= "<a href='" & cadena & "' rel='lightbox_plus' class='vertical' target='_blank'>" & _
                 "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a>"
            SourceImage.Dispose()
        Else
            file = Server.MapPath("tmp\SinFoto.jpg")
            cadena = Server.UrlEncode("\tmp\SinFoto.jpg")
            strHtml &= "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0>"
        End If
        lblHTMLimgFoto4.Text = strHtml

        strHtml = ""


    End Sub
    Protected Sub SaveJPG(ByVal image As Drawing.Image, ByVal szFileName As String)
        'Si ya existe una imagen se tendra que eliminar
        File.Delete(szFileName)
        'Despues de eliminar la imagen existe se crea otra con el Drawing.Image nuevo
        Image.Save(szFileName)
    End Sub
    'Recibe  los bytes de la imagen guardada en sql y devuelve un Bitmap
    Protected Function Bytes2Image(ByVal bytes() As Byte) As Drawing.Bitmap

        If bytes Is Nothing Then Return Nothing
        Dim ms As New MemoryStream(bytes)
        Dim bm As Drawing.Bitmap = Nothing
        Try
            bm = New Drawing.Bitmap(ms)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
        End Try
        Return bm
    End Function
    Protected Sub RellenarControlesEdicionBoletin()
        'Rellena los valores de los controles de Edicion con el contenido del dataset de boletin

        If dstBoletines.Tables("TablaBoletin").Rows.Count > 0 Then
            'DATOS DEL TITULAR
            Me.txtNumRef.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("NumRef"))
            Me.txtSRef.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("SRef"))
            Me.txtPVYCRRef.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("PVYCRRef"))
            Me.ddlPVYCRRef.SelectedValue = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("CodigoPVYCR"))

            Me.txtFecha.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("Fecha"))
            Me.ddlTipo.SelectedValue = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("Tipo"))
            Me.txtZonaGuarderia.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("ZonaGuarderia"))
            Me.txtHoja.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("Hoja"))
            Me.txtutm_x.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("UTM_X"))
            Me.txtutm_y.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("UTM_Y"))
            Me.txtparaje.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("Paraje"))
            Me.txtterminomunicipal.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("TerminoMunicipal"))
            Me.txtprovincia.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("Provincia"))
            Me.txtNombreAutor.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("NombreAutor"))
            Me.txtNifAutor.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("NIFAutor"))
            Me.txtDomicilioAutor.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("DomicilioAutor"))
            Me.txtTlfAutor.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("TlfAutor"))
            Me.txtPoblacionAutor.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("PoblacionAutor"))
            Me.txtProvinciaAutor.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("ProvinciaAutor"))
            Me.txtCPAutor.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("CPAutor"))
            Me.txtTitularTerreno.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("titularterreno"))
            Me.txtIncDetectadaPor.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("IncDetectadaPor"))
            'REPRESENTANTE
            Me.txtNombreRepresentante.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("NombreRepresentante"))
            Me.txtNIFRepresentante.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("NIFRepresentante"))
            Me.txtDomicilioRepresentante.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("DomicilioRepresentante"))
            Me.txtTlfRepresentante.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("TlfRepresentante"))
            Me.txtPoblacionRepresentante.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("PoblacionRepresentante"))
            Me.txtProvinciaRepresentante.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("ProvinciaRepresentante"))
            Me.txtCPRepresentante.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("CPRepresentante"))
            'EMPRESA
            Me.txtNombreEmpresa.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("NombreEmpresa"))
            Me.txtNIFEmpresa.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("NIFEmpresa"))
            Me.txtDomicilioEmpresa.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("DomicilioEmpresa"))
            Me.txtTlfEmpresa.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("TlfEmpresa"))
            Me.txtPoblacionEmpresa.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("PoblacionEmpresa"))
            Me.txtProvinciaEmpresa.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("ProvinciaEmpresa"))
            Me.txtCPEmpresa.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("CPEmpresa"))
            'DIRECTOR OBRA
            Me.txtNombreDirectorObra.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("NombreDirectorObra"))
            Me.txtNIFDirectorObra.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("NIFDirectorObra"))
            Me.txtDomicilioDirectorObra.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("DomicilioDirectorObra"))
            Me.txtTlfDirectorObra.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("TlfDirectorObra"))
            Me.txtPoblacionDirectorObra.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("PoblacionDirectorObra"))
            Me.txtProvinciaDirectorObra.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("ProvinciaDirectorObra"))
            Me.txtCPDirectorObra.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("CPDirectorObra"))
            'checks generales
            '---
            If (dstBoletines.Tables("TablaBoletin").Rows(0).Item("Fotos") = "1") Then
                Me.chkFotos.Checked = True
            Else
                Me.chkFotos.Checked = False
            End If
            '---
            If (dstBoletines.Tables("TablaBoletin").Rows(0).Item("Croquis") = "1") Then
                Me.chkCroquis.Checked = True
            Else
                Me.chkCroquis.Checked = False
            End If
            '---
            If (dstBoletines.Tables("TablaBoletin").Rows(0).Item("Planos") = "1") Then
                Me.chkPlanos.Checked = True
            Else
                Me.chkPlanos.Checked = False
            End If
            '---
            If (dstBoletines.Tables("TablaBoletin").Rows(0).Item("TipoDenuncia") = "1") Then
                Me.chkDenun.Checked = True
            Else
                Me.chkDenun.Checked = False
            End If
            'HECHOS
            Me.txtHechos.Text = utiles.nullABlanco(dstBoletines.Tables("TablaBoletin").Rows(0).Item("Hechos"))




        End If



    End Sub
    Protected Sub rellenarListas()
        'EGB Rellenamos la tabla de Tipos de Suceso del Seguimiento Administrativo [PVYCR_SeguimientoAdministrativo_TS]
        Dim fila As DataRow

        If IsNothing(dstCodigos.Tables("TablaTiposSucesos")) Then
            ''daCodigos.SelectCommand.CommandText = "SELECT TipoSuceso,Denominacion FROM PVYCR_SeguimientoAdministrativo_TS"
            ''daCodigos.SelectCommand.CommandText = "(SELECT DISTINCT ISNULL(ts.tiposuceso,b.tipo)as TipoSuceso,ISNULL(ts.denominacion,b.tipo+' (Denominación sin Descripción)') AS DENOMINACION " & _
            '                                      "FROM PVYCR_BoletinGuarderia b " & _
            '                                      "LEFT JOIN PVYCR_SeguimientoAdministrativo_TS ts ON ts.tiposuceso = b.tipo) " & _
            '                                      "UNION " & _
            '                                      "(SELECT TipoSuceso, DENOMINACION FROM " & _
            '                                      "PVYCR_SeguimientoAdministrativo_TS)" & _
            '                                      "ORDER BY DENOMINACION"
            daCodigos.SelectCommand.CommandText = "SELECT Codigo as TipoSuceso,UPPER(Codigo) + ' - ' + Descripcion as Denominacion FROM PVYCR_BoletinGuarderia_TD"
            daCodigos.Fill(dstCodigos, "TablaTiposSucesos")
        End If

        'Combo de Tipo de Suceso en visualización
        ddlFTipoSuceso.Items.Add(New System.Web.UI.WebControls.ListItem("[Seleccionar]", ""))
        For Each fila In dstCodigos.Tables("TablaTiposSucesos").Rows
            ddlFTipoSuceso.Items.Add(New System.Web.UI.WebControls.ListItem(nullABlanco(fila("Denominacion")), nullABlanco(fila("TipoSuceso"))))
        Next fila

        'Combo de Tipo de Suceso en edición

        ddlTipo.Items.Add(New System.Web.UI.WebControls.ListItem("[Seleccionar]", ""))
        For Each fila In dstCodigos.Tables("TablaTiposSucesos").Rows
            ddlTipo.Items.Add(New System.Web.UI.WebControls.ListItem(nullABlanco(fila("Denominacion")), nullABlanco(fila("TipoSuceso"))))
        Next fila

        'Combo de Codigos PVYCR. La carga del arbol es muy lenta. Se pretende optimizar la carga de la edición

        If IsNothing(dstCodigos.Tables("TablaCodigosPunto")) Then
            daCodigos.SelectCommand.CommandText = "SELECT DISTINCT CodigoPVYCR, Descripcion from PVYCR_Arbol where codigopvycr is not null and idelementomedida is null ORDER BY CodigoPVYCR"

            daCodigos.Fill(dstCodigos, "TablaCodigosPunto")
        End If
        ddlPVYCRRef.Items.Add(New System.Web.UI.WebControls.ListItem("[Seleccionar]", ""))
        For Each fila In dstCodigos.Tables("TablaCodigosPunto").Rows
            ddlPVYCRRef.Items.Add(New System.Web.UI.WebControls.ListItem(nullABlanco(fila("Descripcion")), nullABlanco(fila("CodigoPVYCR"))))
        Next fila


    End Sub

    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Private Sub crearDataSets()

        'Borrado previo en caso de existir una carga previa
        If dstBoletines.Tables.Contains("TablaBoletines") Then
            dstBoletines.Tables("TablaBoletines").Clear()
        End If
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        If txtRegistros.Text = "" Then
            sentenciaSel = " SELECT "
        Else
            sentenciaSel = " SELECT TOP " & txtRegistros.Text
        End If

        ''sentenciaSel = sentenciaSel & " NumRef, PVYCRRef,P.Descripcion as DescripcionPunto, PVYCRRef, Fecha, Tipo, " & _
        '                              "ISNULL(ts.denominacion,b.tipo+' (Denominación sin Descripción)') as Denominacion,  " & _
        '                              "NombreAutor, Hechos FROM PVYCR_BoletinGuarderia b " & _
        '                              "LEFT JOIN PVYCR_SeguimientoAdministrativo_TS ts ON ts.tiposuceso = b.tipo " & _
        '                              "LEFT JOIN (SELECT Descripcion, CodigoPVYCR FROM PVYCR_Arbol WHERE tipo='P')P on P.CodigoPvycr=PVYCRRef "

        sentenciaSel = sentenciaSel & " b.NumRef, b.PVYCRRef, P.Descripcion AS DescripcionPunto, b.PVYCRRef AS Expr1, b.Fecha, b.Tipo, b.NombreAutor, b.Hechos, UPPER(PVYCR_BoletinGuarderia_TD.Codigo) + ' - ' + PVYCR_BoletinGuarderia_TD.Descripcion AS Denominacion, b.zonaguarderia, b.IncDetectadaPor " & _
                                       "FROM  PVYCR_BoletinGuarderia AS b LEFT JOIN " & _
                                       "PVYCR_BoletinGuarderia_TD ON b.Tipo = PVYCR_BoletinGuarderia_TD.Codigo LEFT OUTER JOIN " & _
                                       "(SELECT DISTINCT Descripcion, CodigoPVYCR FROM PVYCR_Arbol  WHERE Tipo = 'P') AS P ON P.CodigoPVYCR = b.PVYCRRef"

        If Session("FiltroBoletines") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroBoletines")
            Else
                sFiltro = sFiltro + Session("FiltroBoletines")
            End If
        End If
        'ncm necesitamos una global que nos va a servir en todos los mtos para poder listar los registros
        'que han filtrado en cada mto.
        Session("FiltroListado") = Session("FiltroBoletines")


        sentenciaOrder = " order by NumRef desc"

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If

        daBoletines.SelectCommand.CommandText = sentenciaSel
        daBoletines.Fill(dstBoletines, "TablaBoletines")

        'Dim txtComando As String = ""
        'txtComando = daBoletines.SelectCommand.CommandText
        'txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        'ncm calculamos el nº de registros que devolverá el filtro 
        txtNumRegFiltrados.Text = FuncionesGenerales.CalcularNumRegFiltrados(Session("FiltroBoletines"), conexion, sFiltro, "PVYCR_BoletinGuarderia AS b LEFT JOIN " & _
                                       "PVYCR_BoletinGuarderia_TD ON b.Tipo = PVYCR_BoletinGuarderia_TD.Codigo LEFT OUTER JOIN " & _
                                       "(SELECT DISTINCT Descripcion, CodigoPVYCR FROM PVYCR_Arbol  WHERE Tipo = 'P') AS P ON P.CodigoPVYCR = b.PVYCRRef", Page)

        rptBoletines.DataSource = dstBoletines.Tables("TablaBoletines")
        rptBoletines.DataBind()

    End Sub

    Private Sub crearDataSetBoletin(ByVal NumRef As String)

        utiles.Comprobar_Conexion_BD(Page, conexion)
        'Carga de los datos alfanumericos del boletín
        If dstBoletines.Tables.Contains("TablaBoletin") Then
            dstBoletines.Tables("TablesBoletin").Clear()
        End If
        sentenciaSel = "SELECT *, " & _
                       "CASE b.Planos WHEN 1 THEN 'Si' ELSE 'No' END AS PlanosTXT, " & _
                       "CASE b.Fotos WHEN 1 THEN 'Si' ELSE 'No' END AS FotosTXT, " & _
                       "CASE b.TipoDenuncia WHEN 1 THEN 'Si' ELSE 'No' END AS TipoDenunciaTXT,  " & _
                       "CASE b.Croquis WHEN 1 THEN 'Si' ELSE 'No' END AS CroquisTXT, " & _
                       "CAST(datepart(dd,b.Fecha) AS VARCHAR(2))+ '/'+ CAST(datepart(mm,b.Fecha)AS VARCHAR(2))+ '/'+ CAST(datepart(yyyy,b.Fecha)AS VARCHAR(4)) as FechaCorta, " & _
                       "P.Descripcion as DescripcionPunto, " & _
                       "ISNULL(ts.denominacion,b.tipo+' (Denominación sin Descripción)') as Denominacion,  " & _
                       "NombreAutor, Hechos FROM PVYCR_BoletinGuarderia b " & _
                       "LEFT JOIN PVYCR_SeguimientoAdministrativo_TS ts ON ts.tiposuceso = b.tipo " & _
                       "LEFT join (select Descripcion, CodigoPVYCR from PVYCR_Arbol WHERE Tipo='P')P on p.CodigoPvycr=PVYCRRef " & _
                       "WHERE NumRef='" & NumRef & "'"

        daBoletines.SelectCommand.CommandText = sentenciaSel
        daBoletines.Fill(dstBoletines, "TablaBoletin")

        crearDataSetImagenesBoletin(NumRef)
        'EGB 09/01/2009 
        ActualizarImagenNULL("..\SICAH\tmp\blanco.jpg", NumRef)


        ''Carga de los datos de imagen del boletín
        'If dstBoletines.Tables.Contains("TablaBoletinImages") Then
        '    dstBoletines.Tables("TablaBoletinImages").Clear()
        'End If

        'sentenciaSel = "SELECT [NumRef]" & _
        '                  ",[FotoGeneral]" & _
        '                  ",[FotoDetalle] " & _
        '                  ",[CroquisAcceso]" & _
        '                  ",[CroquisExplicativo]" & _
        '                  ",[Foto1]" & _
        '                  ",[Foto2]" & _
        '                  ",[Foto3]" & _
        '                  ",[Foto4]" & _
        '               "FROM [SIGVECTOR].[dbo].[PVYCR_Denuncia_Fotos]" & _
        '               "WHERE NumRef='" & NumRef & "'"

        'daBoletines.SelectCommand.CommandText = sentenciaSel
        'daBoletines.Fill(dstBoletines, "TablaBoletinImages")

    End Sub

    Private Sub crearDataSetImagenesBoletin(ByVal NumRef As String)

        utiles.Comprobar_Conexion_BD(Page, conexion)
       
        'Carga de los datos de imagen del boletín
        If dstBoletines.Tables.Contains("TablaBoletinImages") Then
            dstBoletines.Tables("TablaBoletinImages").Clear()
        End If

        'sentenciaSel = "SELECT [NumRef]" & _
        '                  ",[FotoGeneral]" & _
        '                  ",[FotoDetalle] " & _
        '                  ",[CroquisAcceso]" & _
        '                  ",[CroquisExplicativo]" & _
        '                  ",[Foto1]" & _
        '                  ",[Foto2]" & _
        '                  ",[Foto3]" & _
        '                  ",[Foto4]" & _
        '               "FROM [SIGVECTOR].[dbo].[PVYCR_Denuncia_Fotos]" & _
        '               "WHERE NumRef='" & NumRef & "'"

        sentenciaSel = "SELECT [NumRef]" & _
                          ",[Foto1]" & _
                          ",[Foto2]" & _
                          ",[Foto3]" & _
                          ",[Foto4]" & _
                       "FROM [SIGVECTOR].[dbo].[PVYCR_Denuncia_Fotos]" & _
                       "WHERE NumRef='" & NumRef & "'"
        daBoletines.SelectCommand.CommandText = sentenciaSel
        daBoletines.SelectCommand.CommandTimeout = 120
        daBoletines.Fill(dstBoletines, "TablaBoletinImages")

        Cache("dstBoletines") = dstBoletines

    End Sub
   
    Public Sub ActualizarImagenNULL(ByVal strRutaImagen As String, ByVal NumRef As String)

        Dim fotoEnBlancoBytes As Byte()
        Dim fotoEnBlancoImage As Drawing.Image
        Dim fotoBDEnBytes As Drawing.Image


        'Verificacion previa de la existencia de registro relacionado

        If dstBoletines.Tables.Item("TablaBoletinImages").Rows.Count = 0 Then
            '--> En caso de no existir registro en tabla de imagenes PVYCR_Denuncia_Fotos insertamos el registro.
            comando.CommandText = "INSERT INTO [PVYCR_Denuncia_Fotos] (NumRef, Foto1) " & _
                                          "VALUES ('" & utiles.BlancoANull(NumRef) & "', NULL) "
            comando.ExecuteNonQuery()
        End If

        'Conversion archivo en blanco a image
        fotoEnBlancoBytes = ConvertImageFiletoBytes(Server.MapPath(strRutaImagen))
        fotoEnBlancoImage = Bytes2Image(fotoEnBlancoBytes)
        'Conversion contenido bd a image

        If dstBoletines.Tables.Item("TablaBoletinImages").Rows.Count > 0 Then
            If dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto1") Is System.DBNull.Value Then
            Else
                fotoBDEnBytes = Bytes2Image(CType(dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto1"), Byte()))


                If CompararBitMaps(fotoBDEnBytes, fotoEnBlancoImage) Then
                    'update del valor de la fotografia en flanco en el campo nulo...
                    comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                                 "SET Foto1 =NULL " & _
                                                 "WHERE NumRef=@NumRef "
                    'convertir el valor de imagen en bytes
                    comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(NumRef))
                    comando.ExecuteNonQuery()
                End If
            End If


            If dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto2") Is System.DBNull.Value Then
            Else
                fotoBDEnBytes = Bytes2Image(CType(dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto2"), Byte()))
                If CompararBitMaps(fotoBDEnBytes, fotoEnBlancoImage) Then
                    'update del valor de la fotografia en flanco en el campo nulo...
                    comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                                 "SET Foto2 =NULL " & _
                                                 "WHERE NumRef=@NumRef2 "
                    'convertir el valor de imagen en bytes

                    comando.Parameters.AddWithValue("NumRef2", utiles.BlancoANull(NumRef))
                    comando.ExecuteNonQuery()
                End If
            End If

            If dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto3") Is System.DBNull.Value Then
            Else
                fotoBDEnBytes = Bytes2Image(CType(dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto3"), Byte()))
                If CompararBitMaps(fotoBDEnBytes, fotoEnBlancoImage) Then
                    'update del valor de la fotografia en flanco en el campo nulo...
                    comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                                 "SET Foto3 =NULL " & _
                                                 "WHERE NumRef=@NumRef3 "
                    'convertir el valor de imagen en bytes

                    comando.Parameters.AddWithValue("NumRef3", utiles.BlancoANull(NumRef))
                    comando.ExecuteNonQuery()
                End If
            End If

            If dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto4") Is System.DBNull.Value Then
            Else
                fotoBDEnBytes = Bytes2Image(CType(dstBoletines.Tables.Item("TablaBoletinImages").Rows(0).Item("Foto4"), Byte()))
                If CompararBitMaps(fotoBDEnBytes, fotoEnBlancoImage) Then
                    'update del valor de la fotografia en flanco en el campo nulo...
                    comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                                 "SET Foto4 =NULL " & _
                                                 "WHERE NumRef=@NumRef4 "
                    'convertir el valor de imagen en bytes
                    comando.Parameters.AddWithValue("NumRef4", utiles.BlancoANull(NumRef))
                    comando.ExecuteNonQuery()
                End If
            End If
            'Actualizamos el DataTable de Imagenes
            crearDataSetImagenesBoletin(NumRef)


        End If

    End Sub
    Public Function CompararBitMaps(ByVal bmp1 As Bitmap, ByVal bmp2 As Bitmap) As Boolean
        'EGB 09/02/2009 Retorna True o False en funcion del resultado de la comparacion de dos bitmaps
        Dim x As Integer
        Dim y As Integer
        If bmp1.Size <> bmp2.Size Then
            CompararBitMaps = False
        Else
            CompararBitMaps = True
            For x = 0 To bmp1.Width - 1
                For y = 0 To bmp2.Height - 1
                    If (bmp1.GetPixel(x, y) = bmp2.GetPixel(x, y)) = False Then
                        CompararBitMaps = False
                        Exit For
                    End If
                Next
            Next
        End If
    End Function

    Public Function ConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If String.IsNullOrEmpty(ImageFilePath) = True Then
            Throw New ArgumentNullException("No es posible cargar la imagen blanco.jpg", "ImageFilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(ImageFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetDataSetArbol() As DataSet
        Dim DS As System.Data.DataSet

        If File.Exists(MapPath("xmlDataSet.xml")) = False Then

            crearDataSetsPuntosXML2()

            dstarbol.WriteXml(MapPath("xmlDataSet.xml"))

            Return dstarbol
            Session("DSLoaded") = 1
        End If

        DS = New DataSet

        DS.ReadXml(MapPath("xmlDataSet.xml"))

        DS.Relations.Add("SelfRefenceRelation", _
                       DS.Tables("tablaArbol").Columns("IdArbol"), _
                        DS.Tables("tablaArbol").Columns("IdArbolPadre"), True)
        'DS.Relations("SelfRefenceRelation").Nested = True
        Session("DSLoaded") = 1
        Return DS
    End Function

    'Private Function GetDataSetArbol() As DataSet
    '    'EGB. El arbol debe cargarse a partir de los datos de PVYCR_Arbol para no regenerar el XML en cada actualización

    '    crearDataSetsPuntosXML2()
    '    Return dstarbol

    'End Function

    Private Sub crearArbolRecursivoCauces()
        Dim i As Integer
        For i = 0 To dstarbol.Tables("tablaCaucesPadres").Rows.Count - 1
            insertarNodosCauces(Me.treeView1, dstarbol, dstarbol.Tables("tablaCaucesPadres").Rows(i).Item("caucepadre"), Nothing, Nothing, -1, False)
        Next
    End Sub

    Private Sub crearDataSetsPuntosXML2()
        Try
            dstarbol.Clear()
            treeView1.Nodes.Clear()

            'ARBOL NORMALIZADO
            daBoletines.SelectCommand.CommandText = "Select * from dbo.PVYCR_ARBOL"
            daBoletines.Fill(dstarbol, "tablaArbol")

            dstarbol.Relations.Add("SelfRefenceRelation", _
                            dstarbol.Tables("tablaArbol").Columns("IdArbol"), _
                            dstarbol.Tables("tablaArbol").Columns("IdArbolPadre"), True)
            dstarbol.Relations("SelfRefenceRelation").Nested = True

        Catch exp As Exception
        End Try
    End Sub
    'Public Function GetDataSetArbol() As DataSet
    '    Dim DS As System.Data.DataSet

    '    If File.Exists(MapPath("xmlDataSet.xml")) = False Then

    '        crearDataSetsPuntosXML2(dstarbol, treeView1, daBoletines)

    '        dstarbol.WriteXml(MapPath("xmlDataSet.xml"))

    '        Return dstarbol
    '        Session("DSLoaded") = 1
    '    End If

    '    DS = New DataSet

    '    DS.ReadXml(MapPath("xmlDataSet.xml"))

    '    DS.Relations.Add("SelfRefenceRelation", _
    '                   DS.Tables("tablaArbol").Columns("IdArbol"), _
    '                    DS.Tables("tablaArbol").Columns("IdArbolPadre"), True)
    '    'DS.Relations("SelfRefenceRelation").Nested = True
    '    Session("DSLoaded") = 1
    '    Return DS

    'End Function
    Protected Sub AceptarFiltro(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtAceptarUsReg.Click
        'EGB 05/11/2008 Filtros De fecha por mes y año
        Dim strFiltro As String
        strFiltro = ""

        If txtFNumRef.Text <> "" Then
            strFiltro = strFiltro + " AND NumRef LIKE '" + txtFNumRef.Text + "' "
        End If

        'If ddlFMes.SelectedValue <> "0" Then
        '    strFiltro = strFiltro + " AND DATEPART(mm,Fecha)= '" + ddlFMes.SelectedValue + "' "
        'End If

        'If txtFAnyo.Text <> "" Then
        '    strFiltro = strFiltro + " AND DATEPART(yyyy,Fecha) ='" + txtFAnyo.Text + "' "
        'End If

        '  If (txtFiltroFechaFinH.Text <> "" And txtFiltrofechaIniH.Text <> "") Or txtFiltrarCodFuenteDatoH.Text <> "" Then
        
        If txtFFechaInicial.Text <> "" And TXTffechaFinal.Text <> "" Then
            strFiltro = strFiltro & " and Fecha between '" & txtFFechaInicial.Text & " 00:00:00' and '" & txtFFechaFinal.Text & " 23:59:59' "
        ElseIf txtFFechaInicial.Text = "" And txtFFechaFinal.Text <> "" Then
            Alert(Page, "La Fecha Desde no puede ser nula")
            strFiltro = ""
        ElseIf txtFFechaInicial.Text <> "" And txtFFechaFinal.Text = "" Then
            Alert(Page, "La Fecha Hasta no puede ser nula")
            strFiltro = ""
        End If
            

        If txtFCodigoPVYCR.Text <> "" Then
            strFiltro = strFiltro + " AND PVYCRRef LIKE '" + txtFCodigoPVYCR.Text + "' "
        End If

        If ddlFTipoSuceso.SelectedValue <> "" Then
            strFiltro = strFiltro + " AND Tipo='" + ddlFTipoSuceso.SelectedValue + "' "
        End If
        If txtFZona.Text <> "" Then
            strFiltro = strFiltro + " AND Zonaguarderia like '" + txtFZona.Text + "' "
        End If
        If txtFHechos.Text <> "" Then
            strFiltro = strFiltro + " AND Hechos like '%" + txtFHechos.Text + "%' "
        End If
        If chkGuarderia.Checked = True And chkPVYCR.Checked = False Then
            strFiltro = strFiltro + " AND Hechos not like '%pvycr%' "
        End If
        If chkPVYCR.Checked = True And chkGuarderia.Checked = False Then
            strFiltro = strFiltro + " AND Hechos like '%pvycr%' "
        End If
        If txtFIncidencia.Text <> "" Then
            strFiltro = strFiltro + " AND IncDetectadaPor like '%" + txtFIncidencia.Text + "%' "
        End If
        Session("FiltroBoletines") = strFiltro
        crearDataSets()
    End Sub

    Protected Sub nuevoBoletin(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtNuevo.Click
        Me.lblNumRefSel.Text = ""
        Response.Redirect("BoletinGuarderia.aspx?nuevo=yes&numref=0")

    End Sub

    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        crearDataSets()
    End Sub


    Protected Sub rptBoletines_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptBoletines.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String
        Dim objTrans As SqlTransaction

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Session("nuevoCauce") = 0
                Me.pnlEDBoletines.Visible = True
                Me.pnlBoletines.Visible = False

                btnAceptar.Visible = True
                'Me.ucPaginacion.Visible = False

                ''Sólo se carga el árbol al modificar un cauce
                ''If treeView1.Nodes.Count = 0 Then
                'If Request.QueryString("CodigoCauce") <> "" Or Request.QueryString("nuevo") = "yes" Or (treeView1.Nodes.Count = 0) Then
                '    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "P") 'EGB el nuevo parametro P hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
                'End If
                'imgArbol.Attributes.Clear()
                'imgArbol.Attributes.Add("onclick", "jscript:alert('No es posible reasignar el cauce padre del cauce actual.');")

                'Me.lblNumRefSel.Text = parametros(0)
                'crearDataSetBoletin(lblNumRefSel.Text)

                'RellenarControlesEdicionBoletin()
                'rellenarListas()
                '------------------------------------------------------------------
                'Esto reemplaza al proceso anterior
                Response.Redirect("BoletinGuarderia.aspx?numref=" & parametros(0), True)

            Case "borrar"
                If conexion.State = ConnectionState.Closed Then conexion.Open()
                objTrans = conexion.BeginTransaction()

                Try
                    comando.Transaction = objTrans
                    '1.Eliminar relaciones si existen
                    '--
                    comando.CommandText = "delete from PVYCR_denuncia_Fotos where NumRef='" & parametros(0) & "'"
                    comando.ExecuteNonQuery()

                    '2.Se elimina el boletín
                    comando.CommandText = "delete from PVYCR_BoletinGuarderia where NumRef='" & parametros(0) & "'"
                    comando.ExecuteNonQuery()

                    objTrans.Commit()

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el Boletín de Guardería.")
                    objTrans.Rollback()
                End Try
                crearDataSets()


        End Select
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.pnlBoletines.Visible = True
        Me.pnlEDBoletines.Visible = False

        crearDataSets()
        'EGB 18/11/2008 Al entrar en una edición después de cancelar la creación de un nuevo boletín no se actualiza la variable de session
        Me.lblTitulo.Text = "MANTENIMIENTO BOLETÍN:"
    End Sub

    Protected Sub treeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView1.SelectedNodeChanged
        Dim TipoNodo As String
        Dim v() As String
        Try
            v = Split(treeView1.SelectedNode.Value, "#")
            TipoNodo = v(1)
            If TipoNodo = "P" Then
                txtPVYCRRef.Text = v(0)
            Else
                Alert(Page, "Seleccione un punto para asociar el boletín.")
            End If
        Catch mierror As Exception
            'Error en la seleccion del arbol
            Alert(Page, "Seleccione de nuevo el punto, no se registró correctamente")
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)
        Dim sms_error As String

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        'ncm antes de aceptar comprobamos que el NIF /CIf introducido sea correcto
        sms_error = validarNIF()
        If sms_error <> "" Then
            JavaScript.Alert(Page, sms_error)
            Exit Sub
        End If

        Try

            If Me.lblNumRefSel.Text = "" Then
                'Insertamos un nuevo boletin
                comando.CommandText = "INSERT INTO [PVYCR_BoletinGuarderia] " & _
                                               "([NumRef] " & _
                                               ",[SRef]" & _
                                               ",[PVYCRRef]" & _
                                               ",[Fecha]" & _
                                               ",[Tipo]" & _
                                               ",[ZonaGuarderia]" & _
                                               ",[Hoja]" & _
                                               ",[UTM_X]" & _
                                               ",[UTM_Y]" & _
                                               ",[Paraje]" & _
                                               ",[TerminoMunicipal]" & _
                                               ",[Provincia]" & _
                                               ",[NombreAutor]" & _
                                               ",[NIFAutor]" & _
                                               ",[DomicilioAutor]" & _
                                               ",[TlfAutor]" & _
                                               ",[PoblacionAutor]" & _
                                               ",[ProvinciaAutor]" & _
                                               ",[CPAutor]" & _
                                               ",[NombreRepresentante]" & _
                                               ",[TitularTerreno]" & _
                                               ",[NIFRepresentante]" & _
                                               ",[DomicilioRepresentante]" & _
                                               ",[TlfRepresentante]" & _
                                               ",[PoblacionRepresentante]" & _
                                               ",[ProvinciaRepresentante]" & _
                                               ",[CPRepresentante]" & _
                                               ",[NombreEmpresa]" & _
                                               ",[NIFEmpresa]" & _
                                               ",[DomicilioEmpresa]" & _
                                               ",[TlfEmpresa]" & _
                                               ",[PoblacionEmpresa]" & _
                                               ",[ProvinciaEmpresa]" & _
                                               ",[CPEmpresa]" & _
                                               ",[NombreDirectorObra]" & _
                                               ",[NIFDirectorObra]" & _
                                               ",[DomicilioDirectorObra]" & _
                                               ",[TlfDirectorObra]" & _
                                               ",[PoblacionDirectorObra]" & _
                                               ",[ProvinciaDirectorObra]" & _
                                               ",[CPDirectorObra]" & _
                                               ",[TipoDenuncia]" & _
                                               ",[Fotos]" & _
                                               ",[Croquis]" & _
                                               ",[Planos]" & _
                                               ",[Hechos]" & _
                                                ",IncDetectadaPor)" & _
                                     "    VALUES " & _
                                     "         (@NumRef, " & _
                                               "@SRef," & _
                                               "@PVYCRRef," & _
                                               "@Fecha," & _
                                               "@Tipo," & _
                                               "@ZonaGuarderia," & _
                                               "@Hoja," & _
                                               "@UTM_X," & _
                                               "@UTM_Y," & _
                                               "@Paraje," & _
                                               "@TerminoMunicipal," & _
                                               "@Provincia," & _
                                               "@NombreAutor," & _
                                               "@NIFAutor," & _
                                               "@DomicilioAutor," & _
                                               "@TlfAutor," & _
                                               "@PoblacionAutor," & _
                                               "@ProvinciaAutor," & _
                                               "@CPAutor," & _
                                               "@NombreRepresentante," & _
                                               "@TitularTerreno," & _
                                               "@NIFRepresentante," & _
                                               "@DomicilioRepresentante," & _
                                               "@TlfRepresentante," & _
                                               "@PoblacionRepresentante," & _
                                               "@ProvinciaRepresentante," & _
                                               "@CPRepresentante," & _
                                               "@NombreEmpresa," & _
                                               "@NIFEmpresa," & _
                                               "@DomicilioEmpresa," & _
                                               "@TlfEmpresa," & _
                                               "@PoblacionEmpresa," & _
                                               "@ProvinciaEmpresa," & _
                                               "@CPEmpresa," & _
                                               "@NombreDirectorObra," & _
                                               "@NIFDirectorObra," & _
                                               "@DomicilioDirectorObra," & _
                                               "@TlfDirectorObra," & _
                                               "@PoblacionDirectorObra," & _
                                               "@ProvinciaDirectorObra," & _
                                               "@CPDirectorObra," & _
                                               "@TipoDenuncia," & _
                                               "@Fotos," & _
                                               "@Croquis," & _
                                               "@Planos," & _
                                               "@Hechos," & _
                                               "@IncDetectadaPor)"


            Else
                'modificamos el registro del boletin seleccionado
                comando.CommandText = "UPDATE [PVYCR_BoletinGuarderia] " & _
                                      "SET [SRef] = @SRef " & _
                                          ",[PVYCRRef] = @PVYCRRef " & _
                                          ",[Fecha] = @Fecha" & _
                                          ",[Tipo] = @Tipo " & _
                                          ",[ZonaGuarderia] = @ZonaGuarderia " & _
                                          ",[Hoja] = @Hoja " & _
                                          ",[UTM_X] = @UTM_X " & _
                                          ",[UTM_Y] = @UTM_Y " & _
                                          ",[Paraje] = @Paraje " & _
                                          ",[TerminoMunicipal] = @TerminoMunicipal " & _
                                          ",[Provincia] = @Provincia " & _
                                          ",[NombreAutor] = @NombreAutor " & _
                                          ",[NIFAutor] = @NIFAutor " & _
                                          ",[DomicilioAutor] = @DomicilioAutor " & _
                                          ",[TlfAutor] = @TlfAutor " & _
                                          ",[PoblacionAutor] = @PoblacionAutor " & _
                                          ",[ProvinciaAutor] = @ProvinciaAutor " & _
                                          ",[CPAutor] = @CPAutor " & _
                                          ",[NombreRepresentante] = @NombreRepresentante " & _
                                          ",[TitularTerreno] = @TitularTerreno " & _
                                          ",[NIFRepresentante] = @NIFRepresentante " & _
                                          ",[DomicilioRepresentante] = @DomicilioRepresentante " & _
                                          ",[TlfRepresentante] = @TlfRepresentante " & _
                                          ",[PoblacionRepresentante] = @PoblacionRepresentante " & _
                                          ",[ProvinciaRepresentante] = @ProvinciaRepresentante " & _
                                          ",[CPRepresentante] = @CPRepresentante " & _
                                          ",[NombreEmpresa] = @NombreEmpresa " & _
                                          ",[NIFEmpresa] = @NIFEmpresa " & _
                                          ",[DomicilioEmpresa] = @DomicilioEmpresa " & _
                                          ",[TlfEmpresa] = @TlfEmpresa " & _
                                          ",[PoblacionEmpresa] = @PoblacionEmpresa " & _
                                          ",[ProvinciaEmpresa] = @ProvinciaEmpresa " & _
                                          ",[CPEmpresa] = @CPEmpresa " & _
                                          ",[NombreDirectorObra] = @NombreDirectorObra " & _
                                          ",[NIFDirectorObra] = @NIFDirectorObra " & _
                                          ",[DomicilioDirectorObra] = @DomicilioDirectorObra " & _
                                          ",[TlfDirectorObra] = @TlfDirectorObra " & _
                                          ",[PoblacionDirectorObra] = @PoblacionDirectorObra " & _
                                          ",[ProvinciaDirectorObra] = @ProvinciaDirectorObra " & _
                                          ",[CPDirectorObra] = @CPDirectorObra " & _
                                          ",[TipoDenuncia] = @TipoDenuncia " & _
                                          ",[Fotos] = @Fotos " & _
                                          ",[Croquis] = @Croquis " & _
                                          ",[Planos] = @Planos " & _
                                          ",[Hechos] = @Hechos " & _
                                          ",[IncdetectadaPor] = @IncdetectadaPor " & _
                                    " WHERE NumRef= '" & lblNumRefSel.Text & "'"
            End If

            comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
            comando.Parameters.AddWithValue("SRef", utiles.BlancoANull(Me.txtSRef.Text))
            'comando.Parameters.AddWithValue("PVYCRRef", utiles.BlancoANull(Me.txtPVYCRRef.Text))
            comando.Parameters.AddWithValue("PVYCRRef", utiles.BlancoANull(ddlPVYCRRef.SelectedValue))
            comando.Parameters.AddWithValue("Fecha", utiles.BlancoANull(Me.txtFecha.Text))
            comando.Parameters.AddWithValue("Tipo", utiles.BlancoANull(ddlTipo.SelectedValue))
            comando.Parameters.AddWithValue("ZonaGuarderia", utiles.BlancoANull(Me.txtZonaGuarderia.Text))
            comando.Parameters.AddWithValue("Hoja", utiles.BlancoANull(Me.txtHoja.Text))
            comando.Parameters.AddWithValue("UTM_X", utiles.BlancoANull(Me.txtutm_x.Text))
            comando.Parameters.AddWithValue("UTM_Y", utiles.BlancoANull(Me.txtutm_y.Text))
            comando.Parameters.AddWithValue("Paraje", utiles.BlancoANull(Me.txtparaje.Text))
            comando.Parameters.AddWithValue("TerminoMunicipal", utiles.BlancoANull(Me.txtterminomunicipal.Text))
            comando.Parameters.AddWithValue("Provincia", utiles.BlancoANull(Me.txtprovincia.Text))
            comando.Parameters.AddWithValue("NombreAutor", utiles.BlancoANull(Me.txtNombreAutor.Text))
            comando.Parameters.AddWithValue("NIFAutor", utiles.BlancoANull(Me.txtNifAutor.Text))
            comando.Parameters.AddWithValue("DomicilioAutor", utiles.BlancoANull(Me.txtDomicilioAutor.Text))
            comando.Parameters.AddWithValue("TlfAutor", utiles.BlancoANull(Me.txtTlfAutor.Text))
            comando.Parameters.AddWithValue("PoblacionAutor", utiles.BlancoANull(Me.txtPoblacionAutor.Text))
            comando.Parameters.AddWithValue("ProvinciaAutor", utiles.BlancoANull(Me.txtProvinciaAutor.Text))
            comando.Parameters.AddWithValue("CPAutor", utiles.BlancoANull(Me.txtCPAutor.Text))
            comando.Parameters.AddWithValue("NombreRepresentante", utiles.BlancoANull(Me.txtNombreRepresentante.Text))
            comando.Parameters.AddWithValue("NIFRepresentante", utiles.BlancoANull(Me.txtNIFRepresentante.Text))
            comando.Parameters.AddWithValue("DomicilioRepresentante", utiles.BlancoANull(Me.txtDomicilioRepresentante.Text))
            comando.Parameters.AddWithValue("TlfRepresentante", utiles.BlancoANull(Me.txtTlfRepresentante.Text))
            comando.Parameters.AddWithValue("PoblacionRepresentante", utiles.BlancoANull(Me.txtPoblacionRepresentante.Text))
            comando.Parameters.AddWithValue("ProvinciaRepresentante", utiles.BlancoANull(Me.txtProvinciaRepresentante.Text))
            comando.Parameters.AddWithValue("CPRepresentante", utiles.BlancoANull(Me.txtCPRepresentante.Text))
            comando.Parameters.AddWithValue("NombreEmpresa", utiles.BlancoANull(Me.txtNombreEmpresa.Text))
            comando.Parameters.AddWithValue("NIFEmpresa", utiles.BlancoANull(Me.txtNIFEmpresa.Text))
            comando.Parameters.AddWithValue("DomicilioEmpresa", utiles.BlancoANull(Me.txtDomicilioEmpresa.Text))
            comando.Parameters.AddWithValue("TlfEmpresa", utiles.BlancoANull(Me.txtTlfEmpresa.Text))
            comando.Parameters.AddWithValue("PoblacionEmpresa", utiles.BlancoANull(Me.txtPoblacionEmpresa.Text))
            comando.Parameters.AddWithValue("ProvinciaEmpresa", utiles.BlancoANull(Me.txtProvinciaEmpresa.Text))
            comando.Parameters.AddWithValue("CPEmpresa", utiles.BlancoANull(Me.txtCPEmpresa.Text))
            comando.Parameters.AddWithValue("NombreDirectorObra", utiles.BlancoANull(Me.txtNombreDirectorObra.Text))
            comando.Parameters.AddWithValue("NIFDirectorObra", utiles.BlancoANull(Me.txtNIFDirectorObra.Text))
            comando.Parameters.AddWithValue("TlfDirectorObra", utiles.BlancoANull(Me.txtTlfDirectorObra.Text))
            comando.Parameters.AddWithValue("DomicilioDirectorObra", utiles.BlancoANull(Me.txtDomicilioDirectorObra.Text))
            comando.Parameters.AddWithValue("PoblacionDirectorObra", utiles.BlancoANull(Me.txtPoblacionDirectorObra.Text))
            comando.Parameters.AddWithValue("ProvinciaDirectorObra", utiles.BlancoANull(Me.txtProvinciaDirectorObra.Text))
            comando.Parameters.AddWithValue("CPDirectorObra", utiles.BlancoANull(Me.txtCPDirectorObra.Text))
            comando.Parameters.AddWithValue("TitularTerreno", utiles.BlancoANull(Me.txtTitularTerreno.Text))
            comando.Parameters.AddWithValue("IncDetectadaPor", utiles.BlancoANull(Me.txtIncDetectadaPor.Text))

            If chkDenun.Checked Then
                comando.Parameters.AddWithValue("TipoDenuncia", "1")
            Else
                comando.Parameters.AddWithValue("TipoDenuncia", "0")
            End If

            If chkFotos.Checked Then
                comando.Parameters.AddWithValue("Fotos", "1")
            Else
                comando.Parameters.AddWithValue("Fotos", "0")
            End If

            If chkCroquis.Checked Then
                comando.Parameters.AddWithValue("Croquis", "1")
            Else
                comando.Parameters.AddWithValue("Croquis", "0")
            End If

            If chkPlanos.Checked Then
                comando.Parameters.AddWithValue("Planos", "1")
            Else
                comando.Parameters.AddWithValue("Planos", "0")
            End If

            comando.Parameters.AddWithValue("Hechos", utiles.BlancoANull(Me.txtHechos.Text))

            comando.ExecuteNonQuery()

        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                Case 2627
                    JavaScript.Alert(Page, "El Elemento de Medida ya existe")
                    Exit Sub
                Case 8152
                    JavaScript.Alert(Page, "Algún campo del boletín excede el tamaño permitido")
                    Exit Sub
                Case Else
                    JavaScript.Alert(Page, "Error al insertar o modificar un boletín")
                    Exit Sub
            End Select
        End Try

        'Me.pnlBoletines.Visible = True
        'Me.pnlEDBoletines.Visible = False

        'crearDataSets()

        'Esto reemplaza al proceso anterior
        Response.Redirect("BoletinGuarderia.aspx?numref=0", True)

        
    End Sub

    'Protected Sub imgUploadFotoGeneral_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUploadFotoGeneral.Click
    '    Dim comando As SqlCommand = New SqlCommand("", conexion)

    '    utiles.Comprobar_Conexion_BD(Page, conexion)
    '    comando.Parameters.Clear()

    '    'Agregar a la base de datos la imagen seleccionada.
    '    If Me.FileUpload1.FileName.Length > 0 Then
    '        Try
    '            If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
    '                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '            End If
    '            If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
    '                comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
    '                                      "SET FotoGeneral =@FotoGeneral " & _
    '                                      "WHERE NumRef=@NumRef "
    '            Else

    '                comando.CommandText = "INSERT INTO [PVYCR_Denuncia_Fotos] (NumRef, FotoGeneral) " & _
    '                                      "VALUES (@NumRef, @FotoGeneral) "

    '            End If

    '            comando.Parameters.AddWithValue("FotoGeneral", Me.FileUpload1.FileBytes)
    '            comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
    '            comando.ExecuteNonQuery()

    '            'Refresco del fichero temporal de descarga.'--------------------------
    '            Dim SourceImage As Drawing.Bitmap
    '            SourceImage = Bytes2Image(Me.FileUpload1.FileBytes)
    '            SaveJPG(SourceImage, Server.MapPath("tmp\FotoGeneral.jpg"))
    '            SourceImage.Dispose()
    '            'Refresco del datatable de imagenes.'---------------------------------
    '            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '            '---------------------------------------------------------------------

    '            Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)
    '            'Server.Transfer("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, False)

    '        Catch Exc As System.Data.SqlClient.SqlException
    '            Alert(Page, Exc.Message & " num: " & Exc.Number)
    '        End Try

    '    Else
    '        Alert(Page, "Seleccione un fichero para cargar desde el botón Examinar")
    '    End If
    'End Sub

    'Protected Sub imgDeleteFotoGeneral_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDeleteFotoGeneral.Click
    '    Try
    '        If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
    '            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '        End If
    '        If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
    '            comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
    '                                  "SET FotoGeneral =NULL " & _
    '                                  "WHERE NumRef=@NumRef "
    '            comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
    '            comando.ExecuteNonQuery()
    '        End If

    '        'Refresco del datatable de imagenes.'---------------------------------
    '        crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '        '---------------------------------------------------------------------
    '        Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)

    '    Catch Exc As System.Data.SqlClient.SqlException
    '        Alert(Page, Exc.Message & " num: " & Exc.Number)
    '    End Try
    'End Sub

    'Protected Sub imgUploadFotoDetalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUploadFotoDetalle.Click
    '    Dim comando As SqlCommand = New SqlCommand("", conexion)

    '    utiles.Comprobar_Conexion_BD(Page, conexion)
    '    comando.Parameters.Clear()

    '    'Agregar a la base de datos la imagen seleccionada.
    '    If Me.FileUpload2.FileName.Length > 0 Then
    '        Try
    '            If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
    '                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '            End If
    '            If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
    '                comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
    '                                      "SET FotoDetalle =@FotoDetalle " & _
    '                                      "WHERE NumRef=@NumRef "
    '            Else

    '                comando.CommandText = "INSERT INTO [PVYCR_Denuncia_Fotos] (NumRef, FotoDetalle) " & _
    '                                      "VALUES (@NumRef, @FotoDetalle) "

    '            End If

    '            comando.Parameters.AddWithValue("FotoDetalle", Me.FileUpload2.FileBytes)
    '            comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
    '            comando.ExecuteNonQuery()

    '            'Refresco del fichero temporal de descarga.'--------------------------
    '            Dim SourceImage As Drawing.Bitmap
    '            SourceImage = Bytes2Image(Me.FileUpload2.FileBytes)
    '            SaveJPG(SourceImage, Server.MapPath("tmp\FotoDetalle.jpg"))
    '            SourceImage.Dispose()
    '            'Refresco del datatable de imagenes.'---------------------------------
    '            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '            '---------------------------------------------------------------------

    '            Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)
    '            'Server.Transfer("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, False)

    '        Catch Exc As System.Data.SqlClient.SqlException
    '            Alert(Page, Exc.Message & " num: " & Exc.Number)
    '        End Try

    '    Else
    '        Alert(Page, "Seleccione un fichero para cargar desde el botón Examinar")
    '    End If
    'End Sub

    'Protected Sub imgDeleteFotoDetalle_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDeleteFotoDetalle.Click
    '    Try
    '        If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
    '            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '        End If
    '        If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
    '            comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
    '                                  "SET FotoDetalle =NULL " & _
    '                                  "WHERE NumRef=@NumRef "
    '            comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
    '            comando.ExecuteNonQuery()
    '        End If

    '        'Refresco del datatable de imagenes.'---------------------------------
    '        crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '        '---------------------------------------------------------------------
    '        Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)

    '    Catch Exc As System.Data.SqlClient.SqlException
    '        Alert(Page, Exc.Message & " num: " & Exc.Number)
    '    End Try
    'End Sub

   
    'Protected Sub imgUploadCroquisAcceso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUploadCroquisAcceso.Click
    '    Dim comando As SqlCommand = New SqlCommand("", conexion)

    '    utiles.Comprobar_Conexion_BD(Page, conexion)
    '    comando.Parameters.Clear()

    '    'Agregar a la base de datos la imagen seleccionada.
    '    If Me.FileUpload3.FileName.Length > 0 Then
    '        Try
    '            If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
    '                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '            End If
    '            If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
    '                comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
    '                                      "SET CroquisAcceso =@CroquisAcceso " & _
    '                                      "WHERE NumRef=@NumRef "
    '            Else

    '                comando.CommandText = "INSERT INTO [PVYCR_Denuncia_Fotos] (NumRef, CroquisAcceso) " & _
    '                                      "VALUES (@NumRef, @CroquisAcceso) "

    '            End If

    '            comando.Parameters.AddWithValue("CroquisAcceso", Me.FileUpload3.FileBytes)
    '            comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
    '            comando.ExecuteNonQuery()

    '            'Refresco del fichero temporal de descarga.'--------------------------
    '            Dim SourceImage As Drawing.Bitmap
    '            SourceImage = Bytes2Image(Me.FileUpload3.FileBytes)
    '            SaveJPG(SourceImage, Server.MapPath("tmp\CroquisAcceso.jpg"))
    '            SourceImage.Dispose()
    '            'Refresco del datatable de imagenes.'---------------------------------
    '            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '            '---------------------------------------------------------------------

    '            Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)
    '            'Server.Transfer("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, False)

    '        Catch Exc As System.Data.SqlClient.SqlException
    '            Alert(Page, Exc.Message & " num: " & Exc.Number)
    '        End Try

    '    Else
    '        Alert(Page, "Seleccione un fichero para cargar desde el botón Examinar")
    '    End If
    'End Sub

    'Protected Sub imgDeleteCroquisAcceso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDeleteCroquisAcceso.Click
    '    Try
    '        If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
    '            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '        End If
    '        If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
    '            comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
    '                                  "SET CroquisAcceso =NULL " & _
    '                                  "WHERE NumRef=@NumRef "
    '            comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
    '            comando.ExecuteNonQuery()
    '        End If

    '        'Refresco del datatable de imagenes.'---------------------------------
    '        crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '        '---------------------------------------------------------------------
    '        Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)

    '    Catch Exc As System.Data.SqlClient.SqlException
    '        Alert(Page, Exc.Message & " num: " & Exc.Number)
    '    End Try
    'End Sub

    'Protected Sub imgDeleteCroquisExplicativo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDeleteCroquisExplicativo.Click
    '    Try
    '        If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
    '            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '        End If
    '        If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
    '            comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
    '                                  "SET CroquisExplicativo =NULL " & _
    '                                  "WHERE NumRef=@NumRef "
    '            comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
    '            comando.ExecuteNonQuery()
    '        End If

    '        'Refresco del datatable de imagenes.'---------------------------------
    '        crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '        '---------------------------------------------------------------------
    '        Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)

    '    Catch Exc As System.Data.SqlClient.SqlException
    '        Alert(Page, Exc.Message & " num: " & Exc.Number)
    '    End Try
    'End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Try
            If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
            End If
            If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
                comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                      "SET Foto1 =NULL " & _
                                      "WHERE NumRef=@NumRef "
                comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
                comando.ExecuteNonQuery()
            End If

            'Refresco del datatable de imagenes.'---------------------------------
            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
            '---------------------------------------------------------------------
            Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)

        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message & " num: " & Exc.Number)
        End Try
    End Sub

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Try
            If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
            End If
            If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
                comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                      "SET Foto2 =NULL " & _
                                      "WHERE NumRef=@NumRef "
                comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
                comando.ExecuteNonQuery()
            End If

            'Refresco del datatable de imagenes.'---------------------------------
            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
            '---------------------------------------------------------------------
            Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)

        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message & " num: " & Exc.Number)
        End Try
    End Sub

    Protected Sub ImageButton6_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton6.Click
        Try
            If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
            End If
            If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
                comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                      "SET Foto3 =NULL " & _
                                      "WHERE NumRef=@NumRef "
                comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
                comando.ExecuteNonQuery()
            End If

            'Refresco del datatable de imagenes.'---------------------------------
            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
            '---------------------------------------------------------------------
            Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)

        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message & " num: " & Exc.Number)
        End Try
    End Sub

    Protected Sub ImageButton8_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton8.Click
        Try
            If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
            End If
            If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
                comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                      "SET Foto4 =NULL " & _
                                      "WHERE NumRef=@NumRef "
                comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
                comando.ExecuteNonQuery()
            End If

            'Refresco del datatable de imagenes.'---------------------------------
            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
            '---------------------------------------------------------------------
            Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)

        Catch Exc As System.Data.SqlClient.SqlException
            Alert(Page, Exc.Message & " num: " & Exc.Number)
        End Try
    End Sub

    Protected Sub imgUploadFoto1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUploadFoto1.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)

        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()

        'Agregar a la base de datos la imagen seleccionada.
        If Me.FileUpload5.FileName.Length > 0 Then
            Try
                If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
                    crearDataSetImagenesBoletin(Me.txtNumRef.Text)
                End If
                If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
                    comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                          "SET Foto1 =@Foto1 " & _
                                          "WHERE NumRef=@NumRef "
                Else

                    comando.CommandText = "INSERT INTO [PVYCR_Denuncia_Fotos] (NumRef, Foto1) " & _
                                          "VALUES (@NumRef, @Foto1) "

                End If

                comando.Parameters.AddWithValue("Foto1", Me.FileUpload5.FileBytes)
                comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
                comando.ExecuteNonQuery()

                'Refresco del fichero temporal de descarga.'--------------------------
                Dim SourceImage As Drawing.Bitmap
                SourceImage = Bytes2Image(Me.FileUpload5.FileBytes)
                SaveJPG(SourceImage, Server.MapPath("tmp\Foto1.jpg"))
                SourceImage.Dispose()
                'Refresco del datatable de imagenes.'---------------------------------
                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
                '---------------------------------------------------------------------

                Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)
                'Server.Transfer("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, False)

            Catch Exc As System.Data.SqlClient.SqlException
                Alert(Page, Exc.Message & " num: " & Exc.Number)
            End Try

        Else
            Alert(Page, "Seleccione un fichero para cargar desde el botón Examinar")
        End If
    End Sub

    Protected Sub imgUploadFoto2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUploadFoto2.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)

        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()

        'Agregar a la base de datos la imagen seleccionada.
        If Me.FileUpload6.FileName.Length > 0 Then
            Try
                If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
                    crearDataSetImagenesBoletin(Me.txtNumRef.Text)
                End If
                If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
                    comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                          "SET Foto2 =@Foto2 " & _
                                          "WHERE NumRef=@NumRef "
                Else

                    comando.CommandText = "INSERT INTO [PVYCR_Denuncia_Fotos] (NumRef, Foto2) " & _
                                          "VALUES (@NumRef, @Foto2) "

                End If

                comando.Parameters.AddWithValue("Foto2", Me.FileUpload6.FileBytes)
                comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
                comando.ExecuteNonQuery()

                'Refresco del fichero temporal de descarga.'--------------------------
                Dim SourceImage As Drawing.Bitmap
                SourceImage = Bytes2Image(Me.FileUpload6.FileBytes)
                SaveJPG(SourceImage, Server.MapPath("tmp\Foto2.jpg"))
                SourceImage.Dispose()
                'Refresco del datatable de imagenes.'---------------------------------
                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
                '---------------------------------------------------------------------

                Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)
                'Server.Transfer("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, False)

            Catch Exc As System.Data.SqlClient.SqlException
                Alert(Page, Exc.Message & " num: " & Exc.Number)
            End Try

        Else
            Alert(Page, "Seleccione un fichero para cargar desde el botón Examinar")
        End If
    End Sub

    Protected Sub imgUploadFoto3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUploadFoto3.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)

        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()

        'Agregar a la base de datos la imagen seleccionada.
        If Me.FileUpload7.FileName.Length > 0 Then
            Try
                If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
                    crearDataSetImagenesBoletin(Me.txtNumRef.Text)
                End If
                If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
                    comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                          "SET Foto3 =@Foto3 " & _
                                          "WHERE NumRef=@NumRef "
                Else

                    comando.CommandText = "INSERT INTO [PVYCR_Denuncia_Fotos] (NumRef, Foto3) " & _
                                          "VALUES (@NumRef, @Foto3) "

                End If

                comando.Parameters.AddWithValue("Foto3", Me.FileUpload7.FileBytes)
                comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
                comando.ExecuteNonQuery()

                'Refresco del fichero temporal de descarga.'--------------------------
                Dim SourceImage As Drawing.Bitmap
                SourceImage = Bytes2Image(Me.FileUpload7.FileBytes)
                SaveJPG(SourceImage, Server.MapPath("tmp\Foto3.jpg"))
                SourceImage.Dispose()
                'Refresco del datatable de imagenes.'---------------------------------
                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
                '---------------------------------------------------------------------

                Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)
                'Server.Transfer("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, False)

            Catch Exc As System.Data.SqlClient.SqlException
                Alert(Page, Exc.Message & " num: " & Exc.Number)
            End Try

        Else
            Alert(Page, "Seleccione un fichero para cargar desde el botón Examinar")
        End If
    End Sub

    Protected Sub imgUploadFoto4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUploadFoto4.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)

        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()

        'Agregar a la base de datos la imagen seleccionada.
        If Me.FileUpload8.FileName.Length > 0 Then
            Try
                If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
                    crearDataSetImagenesBoletin(Me.txtNumRef.Text)
                End If
                If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
                    comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
                                          "SET Foto4 =@Foto4 " & _
                                          "WHERE NumRef=@NumRef "
                Else

                    comando.CommandText = "INSERT INTO [PVYCR_Denuncia_Fotos] (NumRef, Foto4) " & _
                                          "VALUES (@NumRef, @Foto4) "

                End If

                comando.Parameters.AddWithValue("Foto4", Me.FileUpload8.FileBytes)
                comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
                comando.ExecuteNonQuery()

                'Refresco del fichero temporal de descarga.'--------------------------
                Dim SourceImage As Drawing.Bitmap
                SourceImage = Bytes2Image(Me.FileUpload8.FileBytes)
                SaveJPG(SourceImage, Server.MapPath("tmp\Foto4.jpg"))
                SourceImage.Dispose()
                'Refresco del datatable de imagenes.'---------------------------------
                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
                '---------------------------------------------------------------------

                Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)
                'Server.Transfer("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, False)

            Catch Exc As System.Data.SqlClient.SqlException
                Alert(Page, Exc.Message & " num: " & Exc.Number)
            End Try

        Else
            Alert(Page, "Seleccione un fichero para cargar desde el botón Examinar")
        End If
    End Sub

    'Protected Sub imgUploadCroquisExplicativo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUploadCroquisExplicativo.Click
    '    Dim comando As SqlCommand = New SqlCommand("", conexion)

    '    utiles.Comprobar_Conexion_BD(Page, conexion)
    '    comando.Parameters.Clear()

    '    'Agregar a la base de datos la imagen seleccionada.
    '    If Me.FileUpload4.FileName.Length > 0 Then
    '        Try
    '            If dstBoletines.Tables.Contains("TablaBoletinImages") = False Then
    '                crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '            End If
    '            If dstBoletines.Tables("TablaBoletinImages").Rows.Count > 0 Then
    '                comando.CommandText = "UPDATE [PVYCR_Denuncia_Fotos] " & _
    '                                      "SET CroquisExplicativo =@CroquisExplicativo " & _
    '                                      "WHERE NumRef=@NumRef "
    '            Else

    '                comando.CommandText = "INSERT INTO [PVYCR_Denuncia_Fotos] (NumRef, CroquisExplicativo) " & _
    '                                      "VALUES (@NumRef, @CroquisExplicativo) "

    '            End If

    '            comando.Parameters.AddWithValue("CroquisExplicativo", Me.FileUpload4.FileBytes)
    '            comando.Parameters.AddWithValue("NumRef", utiles.BlancoANull(Me.txtNumRef.Text))
    '            comando.ExecuteNonQuery()

    '            'Refresco del fichero temporal de descarga.'--------------------------
    '            Dim SourceImage As Drawing.Bitmap
    '            SourceImage = Bytes2Image(Me.FileUpload4.FileBytes)
    '            SaveJPG(SourceImage, Server.MapPath("tmp\CroquisExplicativo.jpg"))
    '            SourceImage.Dispose()
    '            'Refresco del datatable de imagenes.'---------------------------------
    '            crearDataSetImagenesBoletin(Me.txtNumRef.Text)
    '            '---------------------------------------------------------------------

    '            Response.Redirect("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, True)
    '            'Server.Transfer("BoletinGuarderia.aspx?numref=" & Me.txtNumRef.Text, False)

    '        Catch Exc As System.Data.SqlClient.SqlException
    '            Alert(Page, Exc.Message & " num: " & Exc.Number)
    '        End Try

    '    Else
    '        Alert(Page, "Seleccione un fichero para cargar desde el botón Examinar")
    '    End If
    'End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        
        Dim nombreinforme = "../listados/InformeBoletin.aspx?NumRef=" + utiles.BlancoANull(Me.txtNumRef.Text)
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub
    Protected Function validarNIF() As String
        Dim cadena As String = ""
        If txtNifAutor.Text <> "" Then
            Dim esCIF As Boolean = utiles.Verificar_CIF(txtNifAutor.Text)

            Dim esNIF As Boolean = utiles.Verificar_NIF(txtNifAutor.Text)

            If esNIF = False And esCIF = False Then
                cadena = "NIF/CIF del Autor incorrecto" & " \n"
            End If

        End If
        If txtNIFRepresentante.Text <> "" Then
            Dim esCIF As Boolean = utiles.Verificar_CIF(txtNIFRepresentante.Text)

            Dim esNIF As Boolean = utiles.Verificar_NIF(txtNIFRepresentante.Text)

            If esNIF = False And esCIF = False Then
                cadena = cadena & "NIF/CIF del representante incorrecto" & " \n"
            End If

        End If

        If txtNIFEmpresa.Text <> "" Then
            Dim esCIF As Boolean = utiles.Verificar_CIF(txtNIFEmpresa.Text)

            Dim esNIF As Boolean = utiles.Verificar_NIF(txtNIFEmpresa.Text)

            If esNIF = False And esCIF = False Then
                cadena = cadena & "NIF/CIF de la empresa incorrecto" & " \n"
            End If

        End If
        If txtNIFDirectorObra.Text <> "" Then
            Dim esCIF As Boolean = utiles.Verificar_CIF(txtNIFDirectorObra.Text)

            Dim esNIF As Boolean = utiles.Verificar_NIF(txtNIFDirectorObra.Text)

            If esNIF = False And esCIF = False Then
                cadena = cadena & "NIF/CIF del Director Obra incorrecto"
            End If

        End If

        Return cadena
    End Function

  Protected Sub recuperarDatosBoletin(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim codigoCauce As String
        'ncm si le dan a nuevo boletín o lo editan y seleccionan un punto PVYCR, obtendremos la información del boltín que
        'se corresponde con el cauce del punto.
        If ddlPVYCRRef.SelectedValue = "" Then
            limpiaCampos()
        Else
            utiles.Comprobar_Conexion_BD(Page, conexion)
            comando.CommandText = "select codigoCauce from PVYCR_puntos where codigoPVYCR = '" & ddlPVYCRRef.SelectedValue & "'"
            codigoCauce = comando.ExecuteScalar
            If nullABlanco(codigoCauce) <> "" Then
                daCodigos.SelectCommand.CommandText = "select codigoCauce, parajeToma,  MunicipioToma, ProvinciaToma, X_UTM_toma, Y_UTM_toma, " & _
                "nombretitular,direcciontitular,municipiotitular,NIFtitular,codpostalTitular,provinciaTitular,TelefonoTitular from pvycr_cauces " & _
                "where codigoCauce = '" & codigoCauce & "'"

                daCodigos.Fill(dstCodigos, "TablaInfCauces")
                If dstCodigos.Tables("TablaInfCauces").Rows.Count > 0 Then
                    txtparaje.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("ParajeToma").ToString
                    txtterminomunicipal.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("MunicipioToma").ToString
                    txtprovincia.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("ProvinciaToma").ToString
                    txtutm_x.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("X_UTM_Toma").ToString
                    txtutm_y.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("Y_UTM_Toma").ToString

                    txtNombreAutor.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("Nombretitular").ToString
                    txtDomicilioAutor.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("DireccionTitular").ToString
                    txtPoblacionAutor.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("municipioTitular").ToString
                    txtNifAutor.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("NIFTitular").ToString
                    txtCPAutor.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("codpostaltitular").ToString
                    txtProvinciaAutor.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("ProvinciaTitular").ToString
                    txtTlfAutor.Text = dstCodigos.Tables("TablaInfcauces").Rows(0).Item("TelefonoTitular").ToString
                Else
                    limpiaCampos()
                End If
            Else
                limpiaCampos()
            End If
        End If
    End Sub
    Private Sub limpiaCampos()
        txtparaje.Text = ""
        txtterminomunicipal.Text = ""
        txtprovincia.Text = ""
        txtutm_x.Text = ""
        txtutm_y.Text = ""
        txtNombreAutor.Text = ""
        txtDomicilioAutor.Text = ""
        txtPoblacionAutor.Text = ""
        txtNifAutor.Text = ""
        txtCPAutor.Text = ""
        txtProvinciaAutor.Text = ""
        txtTlfAutor.Text = ""
    End Sub

    Protected Sub btnListFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListFiltro.Click
        Dim ventanaChecks = "SelecCamposFiltradosMto.aspx?mto=Boletines Guardería"

        If utiles.nullABlanco(txtNumRegFiltrados.Text) <> "" Or utiles.nullABlanco(txtNumRegFiltrados.Text) = "0" Then
            ClientScript.RegisterStartupScript(Page.GetType, "abreventanaChecks", "<script language=javascript>window.open('" + ventanaChecks + "','','toolbar=no,resizable=no, scrollbar=no, status=no')</script>")
        Else
            JavaScript.Alert(Page, "No hay elementos filtrados")
        End If

    End Sub
End Class
