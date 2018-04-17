Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.IO
Partial Class SICAH_motobombas
    Inherits System.Web.UI.Page

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daMotobombas As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstMotobombas As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Dim daElementoMotobomba As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementoMotobomba As DataSet = New System.Data.DataSet()
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim sentenciaSelHistorial, sentenciaOrderHistorial As String
    Dim parfila As Integer = 0
    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ucPaginacion.ruta = "../"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        'Registro de la funcionalidad del calendario
        imgFechaRevision.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFecharevision.ClientID & "'),'dd/mm/yyyy');")
        'imgFechaInicial.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaIni.ClientID & "'),'dd/mm/yyyy');")
        'imgFechaFin.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFechaFin.ClientID & "'),'dd/mm/yyyy');")
        'scripts de cliente para el calendario
        imgFfechamedidaM.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaRevision.ClientID & "'),'dd/mm/yyyy');")
        lblCabecera.Text = genHTML.cabecera(Page)
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))
            'lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page)
            'EGB 09/07/2008 Asociación con el Arbol de Cauces --------------------------------------------------------------------
            imgCerrarVentana.Attributes.Add("onclick", "desplegarArbol(this);")
            imgArbol.Attributes.Add("onclick", "desplegarArbol(this);")
            'EBG Sólo se carga el árbol al dar de alta un nuevo elemento.
            'NCM 08/09/2009 he puesto la carga del árbol cuando le dan a relacionar, para que no se relentice tanto.
            '---------------------------------------------------------------------------------------------------------------------
         

            If Request.QueryString("nuevo") = "yes" Then
                pnlEDMotobombas.Visible = True
                pnlMotobombas.Visible = False
                'ucPaginacion.Visible = False
                lblTitulo.Text = "<b>NUEVA MOTOBOMBA</b>"
                txtcodigoMotobomba.Enabled = True
                txtFecharevision.Enabled = True
            Else
                If Request.QueryString("relacionar") = "yes" Then
                    imgArbol.Visible = True

                    imgArbol.Visible = False
                End If
            End If
            'If Request.QueryString("pag") <> "" Then
            '    ucPaginacion.lblPaginatext = Request.QueryString("pag")
            'Else
            '    ucPaginacion.lblPaginatext = "1"
            'End If


            crearDataSets()
            rellenarListas()
            'ucPaginacion.lblNumpaginasDatabind()
        End If
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'If ucPaginacion.lblMuevetext = "si" Then
        '    crearDataSets()
        '    ucPaginacion.lblMuevetext = "no"
        'End If
    End Sub
    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Private Sub crearDataSets()
        'Criterios de filtrado
        Dim sFiltro As String = ""
        If txtRegistros.Text = "" Then
            sentenciaSel = " SELECT "
        Else
            sentenciaSel = " SELECT TOP " & txtRegistros.Text
        End If
        utiles.Comprobar_Conexion_BD(Page, conexion)

        sentenciaSel = sentenciaSel & " CodigoMotoBomba, FechaRevision, codigoPVYCR, FechaInicial, Descripcion, Grupos_Motobomba, MarcaBomba, TipoBomba, NumSerieBomba, " & _
                      "ModeloBomba, Qn_Bomba, MarcaMotor, ModeloMotor, TipoMotor_H_V, NumSerieMotor, RPM, Potencia_CV, Potencia_KW, Tipo_Pot, " & _
                      "PotenciaTeorica_KW, DDP_Motor_V, Intensidad_Motor_A, Cos_Fi, Caudal_LSeg, Tipo_Caudal, Altura_Geometrica_Impulsion, " & _
                      "Altura_Manometrica_Teorica, Tipo_Alt, Profundidad_Nivel_Estatico, Profundidad_Nivel_Dinamico, Longitud_Aspiracion, Dn_Aspiracion, " & _
                      "Material_Tub_Aspiracion, Longitud_Impulsion, Dn_Impulsion, Material_Tub_Impulsion, Voltimetro, Amperimetro, Manometro_Kgcm2, Rev10min, " & _
                      "BombaEnFuncionamiento, Cota_Toma, Destino1, Destino2, Destino3, Cota_Destino1, Cota_Destino2, Cota_Destino3, UTM_Destino1, UTM_Destino2,  " & _
                      "UTM_Destino3, FechaFin, Observaciones FROM PVYCR_MotoBombas "

        'If txtFiltroCodigoPVYCR.Text <> "[Código Motobomba]" And txtFiltroCodigoPVYCR.Text <> "" Then
        '    sFiltro = " where CodigoMotoBomba like '" & txtFiltroCodigoPVYCR.Text & "' "
        'Else
        '    sFiltro = ""
        'End If
        'RDF 20080617
        If Session("FiltroMot") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroMot")
            Else
                sFiltro = sFiltro + Session("FiltroMot")
            End If
        End If
        'ncm necesitamos una global que nos va a servir en todos los mtos para poder listar los registros
        'que han filtrado en cada mto.
        Session("FiltroListado") = Session("FiltroMot")

        sentenciaOrder = " order by CodigoMotoBomba "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If
        'paginaAct = ucPaginacion.lblPaginatext
        'If paginaAct < 1 Then paginaAct = 1
        'primerReg = (paginaAct - 1) * ConfigurationManager.AppSettings("registrosPorPag")

        daMotobombas.SelectCommand.CommandText = sentenciaSel
        daMotobombas.Fill(dstMotobombas, "TablaMotobombas")
        'daMotobombas.Fill(dstMotobombas, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaMotobombas")
        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daMotobombas.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        'ucPaginacion.calcularPags(txtComando)
        'ncm calculamos el nº de registros que devolverá el filtro 
        txtNumRegFiltrados.Text = FuncionesGenerales.CalcularNumRegFiltrados(Session("FiltroMot"), conexion, sFiltro, "PVYCR_MotoBombas", Page)

        rptMotobombas.DataSource = dstMotobombas.Tables("TablaMotobombas")
        rptMotobombas.DataBind()

    End Sub

    Private Sub crearDataSetMotoBomba(ByVal Parametro0 As String, ByVal Parametro1 As String)
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daMotobombas.SelectCommand.CommandText = "select * from PVYCR_MotoBombas where CodigoMotoBomba='" & Parametro0 & "' and FechaRevision='" & Parametro1 & "' "
        daMotobombas.Fill(dstMotobombas, "TablaMotobombas")
        'daMotobombas.SelectCommand.CommandText = "select *, CodigoPVYCR+' ('+ IdElementoMedida + ')' as CodigoElementoMedida from PVYCR_Calculos_ElementosMedida where PVYCR_Calculos_ElementosMedida.idCalculo=" & lblCalculoSel.Text
        'daMotobombas.Fill(dstMotobombas, "TablaCalculosElementosMedida")

    End Sub

    Protected Sub nuevamotobomba(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMotobombaSel.Text = ""
        Response.Redirect("motobombas.aspx?nuevo=yes")

    End Sub


    'Protected Sub btnFiltroAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptar.Click
    '    'ucPaginacion.lblPaginatext = "1"
    '    crearDataSets()
    '    'ucPaginacion.lblNumpaginasDatabind()
    'End Sub
    'Protected Sub btnFiltrocancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelar.Click, btnFiltroCancelar.Click
    '    'ucPaginacion.lblPaginatext = "1"
    '    txtFiltroCodigoPVYCR.Text = "[Código Motobomba]"
    '    crearDataSets()
    '    'ucPaginacion.lblNumpaginasDatabind()
    'End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        comando.Parameters.Clear()
        Try
            If lblMotobombaSel.Text = "" Then
                'Insertamos un nuevo usuario
                comando.CommandText = "INSERT INTO [SIGVECTOR].[dbo].[PVYCR_MotoBombas] ([CodigoMotoBomba],[FechaRevision],[codigoPVYCR] ,[FechaInicial] " & _
                                    " ,[FechaFin],[Descripcion],[Grupos_Motobomba],[MarcaBomba],[TipoBomba],[NumSerieBomba],[ModeloBomba],[Qn_Bomba] ,[MarcaMotor]" & _
                                    " ,[ModeloMotor],[TipoMotor_H_V],[NumSerieMotor],[RPM],[Potencia_CV],[Potencia_KW],[Tipo_Pot],[PotenciaTeorica_KW],[DDP_Motor_V],[Intensidad_Motor_A]" & _
                                    " ,[Cos_Fi],[Caudal_LSeg],[Tipo_Caudal],[Altura_Geometrica_Impulsion],[Altura_Manometrica_Teorica],[Tipo_Alt],[Profundidad_Nivel_Estatico]" & _
                                    " ,[Profundidad_Nivel_Dinamico],[Longitud_Aspiracion],[Dn_Aspiracion],[Material_Tub_Aspiracion],[Longitud_Impulsion],[Dn_Impulsion],[Material_Tub_Impulsion]" & _
                                    " ,[Voltimetro],[Amperimetro],[Manometro_Kgcm2],[Rev10min],[BombaEnFuncionamiento],[Cota_Toma],[Destino1],[Destino2],[Destino3],[Cota_Destino1]" & _
                                    " ,[Cota_Destino2],[Cota_Destino3],[UTM_Destino1],[UTM_Destino2],[UTM_Destino3],[Observaciones]) VALUES " & _
                                    " (@CodigoMotoBomba,@FechaRevision,@codigoPVYCR ,@FechaInicial " & _
                                    " ,@FechaFin,@Descripcion,@Grupos_Motobomba,@MarcaBomba,@TipoBomba,@NumSerieBomba,@ModeloBomba,@Qn_Bomba ,@MarcaMotor" & _
                                    " ,@ModeloMotor,@TipoMotor_H_V,@NumSerieMotor,@RPM,@Potencia_CV,@Potencia_KW,@Tipo_Pot,@PotenciaTeorica_KW,@DDP_Motor_V,@Intensidad_Motor_A" & _
                                    " ,@Cos_Fi,@Caudal_LSeg,@Tipo_Caudal,@Altura_Geometrica_Impulsion,@Altura_Manometrica_Teorica,@Tipo_Alt,@Profundidad_Nivel_Estatico" & _
                                    " ,@Profundidad_Nivel_Dinamico,@Longitud_Aspiracion,@Dn_Aspiracion,@Material_Tub_Aspiracion,@Longitud_Impulsion,@Dn_Impulsion,@Material_Tub_Impulsion" & _
                                    " ,@Voltimetro,@Amperimetro,@Manometro_Kgcm2,@Rev10min,@BombaEnFuncionamiento,@Cota_Toma,@Destino1,@Destino2,@Destino3,@Cota_Destino1" & _
                                    " ,@Cota_Destino2,@Cota_Destino3,@UTM_Destino1,@UTM_Destino2,@UTM_Destino3,@Observaciones)"



                comando.Parameters.AddWithValue("codigoMotobomba", utiles.BlancoANull(Me.txtcodigoMotobomba.Text))
                comando.Parameters.AddWithValue("FechaRevision", utiles.BlancoANull(Me.txtFecharevision.Text))
            Else
                comando.CommandText = "UPDATE PVYCR_Motobombas SET CodigoPVYCR = @CodigoPVYCR " & _
                ",FechaInicial = @FechaInicial,FechaFin = @FechaFin,Descripcion=@Descripcion,Grupos_Motobomba=@Grupos_Motobomba,MarcaBomba=@MarcaBomba,TipoBomba=@TipoBomba,NumSerieBomba=@NumSerieBomba" & _
                ",ModeloBomba=@ModeloBomba,Qn_Bomba=@Qn_Bomba ,MarcaMotor=@MarcaMotor" & _
                ",ModeloMotor=@ModeloMotor,TipoMotor_H_V=@TipoMotor_H_V,NumSerieMotor=@NumSerieMotor,RPM=@RPM,Potencia_CV=@Potencia_CV " & _
                ",Potencia_KW=@Potencia_KW,Tipo_Pot=@Tipo_Pot,PotenciaTeorica_KW=@PotenciaTeorica_KW,DDP_Motor_V=@DDP_Motor_V,Intensidad_Motor_A=@Intensidad_Motor_A" & _
                ",Cos_Fi=@Cos_Fi,Caudal_LSeg=@Caudal_LSeg,Tipo_Caudal=@Tipo_Caudal,Altura_Geometrica_Impulsion=@Altura_Geometrica_Impulsion" & _
                ",Altura_Manometrica_Teorica=@Altura_Manometrica_Teorica,Tipo_Alt=@Tipo_Alt,Profundidad_Nivel_Estatico=@Profundidad_Nivel_Estatico" & _
                ",Profundidad_Nivel_Dinamico=@Profundidad_Nivel_Dinamico,Longitud_Aspiracion=@Longitud_Aspiracion,Dn_Aspiracion=@Dn_Aspiracion,Material_Tub_Aspiracion=@Material_Tub_Aspiracion" & _
                ",Longitud_Impulsion=@Longitud_Impulsion,Dn_Impulsion=@Dn_Impulsion,Material_Tub_Impulsion=@Material_Tub_Impulsion" & _
                ",Voltimetro=@Voltimetro,Amperimetro=@Amperimetro,Manometro_Kgcm2=@Manometro_Kgcm2,Rev10min=@Rev10min,BombaEnFuncionamiento=@BombaEnFuncionamiento" & _
                ",Cota_Toma=@Cota_Toma,Destino1=@Destino1,Destino2=@Destino2,Destino3=@Destino3,Cota_Destino1=@Cota_Destino1" & _
                ",Cota_Destino2=@Cota_Destino2,Cota_Destino3=@Cota_Destino3,UTM_Destino1=@UTM_Destino1,UTM_Destino2=@UTM_Destino2,UTM_Destino3=@UTM_Destino3,Observaciones=@Observaciones " & _
                "where CodigoMotobomba='" & lblMotobombaSel.Text & "' AND fechaRevision='" & txtFecharevision.Text & "'"
            End If


            comando.Parameters.AddWithValue("CodigoPVYCR", utiles.BlancoANull(txtCodigoPVYCR.Text))
            comando.Parameters.AddWithValue("FechaInicial", utiles.BlancoANull(txtFechaIni.Text)) 'para que no meta una fecha mñinima cuando el campo es nulo (que al ser un textbox es "")
            comando.Parameters.AddWithValue("FechaFin", utiles.BlancoANull(txtFechaFin.Text))
            comando.Parameters.AddWithValue("Descripcion", utiles.BlancoANull(Me.txtdescripcion.Text))

            comando.Parameters.AddWithValue("Grupos_Motobomba", utiles.BlancoANull(Me.txtGMotobomba.Text))
            comando.Parameters.AddWithValue("MarcaBomba", utiles.BlancoANull(Me.txtmarcaBomba.Text))
            comando.Parameters.AddWithValue("TipoBomba", utiles.BlancoANull(Me.txtTipoMotobomba.Text))
            comando.Parameters.AddWithValue("NumSerieBomba", utiles.BlancoANull(Me.txtNSerieMotobomba.Text))
            comando.Parameters.AddWithValue("ModeloBomba", utiles.BlancoANull(Me.txtModelo.Text))
            comando.Parameters.AddWithValue("Qn_Bomba", utiles.BlancoANull(Me.txtQnBomba.Text))

            comando.Parameters.AddWithValue("MarcaMotor", utiles.BlancoANull(Me.txtMarcaMotor.Text))
            comando.Parameters.AddWithValue("ModeloMotor", utiles.BlancoANull(Me.txtModeloMotor.Text))
            comando.Parameters.AddWithValue("TipoMotor_H_V", utiles.BlancoANull(Me.txttipoMotor.Text))
            comando.Parameters.AddWithValue("NumSerieMotor", utiles.BlancoANull(Me.txtNSerieMotobombaMotor.Text))
            comando.Parameters.AddWithValue("RPM", utiles.BlancoANull(Me.txtRPM.Text))
            comando.Parameters.AddWithValue("Potencia_CV", utiles.BlancoANull(Me.txtPotencia_CV.Text))

            comando.Parameters.AddWithValue("Potencia_KW", utiles.BlancoANull(Me.txtPotencia_kw.Text))
            comando.Parameters.AddWithValue("Tipo_Pot", utiles.BlancoANull(Me.txttipoPot.Text))
            comando.Parameters.AddWithValue("PotenciaTeorica_KW", utiles.BlancoANull(Me.txtPotenciaTeorica_kw.Text))
            comando.Parameters.AddWithValue("DDP_Motor_V", utiles.BlancoANull(Me.txtDDPMotor.Text))
            comando.Parameters.AddWithValue("Intensidad_Motor_A", utiles.BlancoANull(Me.txtIntensidadMotor_A.Text))
            If Me.txtCosFi.Text = "" Then
                comando.Parameters.AddWithValue("Cos_Fi", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("Cos_Fi", utiles.BlancoANull(Me.txtCosFi.Text.Replace(",", ".")))
                'comando.Parameters.AddWithValue("Cos_Fi", utiles.BlancoANull(Me.txtCosFi.Text.Replace(",", ".")))
                'comando.Parameters.AddWithValue("E_RelacionM3_KWH", utiles.BlancoANull(txtRelacion.Text.Replace(",", ".")))
            End If
            If Me.txtCaudal_LSeg.Text = "" Then
                comando.Parameters.AddWithValue("Caudal_LSeg", DBNull.Value)
            Else
                'comando.Parameters.AddWithValue("Caudal_LSeg", Decimal.Parse(utiles.BlancoANull(Me.txtCaudal_LSeg.Text.Replace(".", ""))))
                comando.Parameters.AddWithValue("Caudal_LSeg", utiles.BlancoANull(Me.txtCaudal_LSeg.Text.Replace(",", ".")))
            End If
            comando.Parameters.AddWithValue("Tipo_Caudal", utiles.BlancoANull(Me.txtTipoCaudal.Text))
            comando.Parameters.AddWithValue("Altura_Geometrica_Impulsion", utiles.BlancoANull(Me.txtAlturaGeometrica.Text))
            comando.Parameters.AddWithValue("Altura_Manometrica_Teorica", utiles.BlancoANull(Me.txtAlturaManometrica.Text))
            comando.Parameters.AddWithValue("Tipo_Alt", utiles.BlancoANull(Me.txtTipoAlt.Text))
            comando.Parameters.AddWithValue("Profundidad_Nivel_Estatico", utiles.BlancoANull(Me.txtProfundidadEstatica.Text))

            comando.Parameters.AddWithValue("Profundidad_Nivel_Dinamico", utiles.BlancoANull(Me.txtProfundidadDinamica.Text))
            comando.Parameters.AddWithValue("Longitud_Aspiracion", utiles.BlancoANull(Me.txtLongitudAspiracion.Text))
            comando.Parameters.AddWithValue("Dn_Aspiracion", utiles.BlancoANull(Me.txtDnAspiracion.Text))
            comando.Parameters.AddWithValue("Material_Tub_Aspiracion", utiles.BlancoANull(Me.txtMaterialTubAsp.Text))
            comando.Parameters.AddWithValue("Longitud_Impulsion", utiles.BlancoANull(Me.txtLongImpulsion.Text))
            comando.Parameters.AddWithValue("Dn_Impulsion", utiles.BlancoANull(Me.txtDnImpulsion.Text))

            comando.Parameters.AddWithValue("Material_Tub_Impulsion", utiles.BlancoANull(Me.txtMaterialtubImp.Text))
            comando.Parameters.AddWithValue("Voltimetro", utiles.BlancoANull(Me.txtVolimetro.Text))
            comando.Parameters.AddWithValue("Amperimetro", utiles.BlancoANull(Me.txtAmperimetro.Text))
            comando.Parameters.AddWithValue("Manometro_Kgcm2", utiles.BlancoANull(Me.txtManometro.Text))
            comando.Parameters.AddWithValue("Rev10min", utiles.BlancoANull(Me.txtRev10min.Text))
            comando.Parameters.AddWithValue("BombaEnFuncionamiento", utiles.BlancoANull(Me.txtBombaEnFuncionamiento.Text))
            If Me.txtCotaToma.Text = "" Then
                comando.Parameters.AddWithValue("Cota_Toma", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("Cota_Toma", utiles.BlancoANull(Me.txtCotaToma.Text.Replace(",", ".")))
            End If

            comando.Parameters.AddWithValue("Destino1", utiles.BlancoANull(Me.txtDestino1.Text))
            comando.Parameters.AddWithValue("Destino2", utiles.BlancoANull(Me.txtDestino2.Text))
            comando.Parameters.AddWithValue("Destino3", utiles.BlancoANull(Me.txtDestino3.Text))
            If Me.txtCotaDestino1.Text = "" Then
                comando.Parameters.AddWithValue("Cota_Destino1", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("Cota_Destino1", utiles.BlancoANull(Me.txtCotaDestino1.Text.Replace(",", ".")))
            End If
            If Me.txtCotaDestino2.Text = "" Then
                comando.Parameters.AddWithValue("Cota_Destino2", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("Cota_Destino2", utiles.BlancoANull(Me.txtCotaDestino2.Text.Replace(",", ".")))
            End If

            If Me.txtCotaDestino3.Text = "" Then
                comando.Parameters.AddWithValue("Cota_Destino3", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("Cota_Destino3", utiles.BlancoANull(Me.txtCotaDestino3.Text.Replace(",", ".")))
            End If

            comando.Parameters.AddWithValue("UTM_Destino1", utiles.BlancoANull(Me.txtUTMDestino1.Text))
            comando.Parameters.AddWithValue("UTM_Destino2", utiles.BlancoANull(Me.txtUTMDestino2.Text))
            comando.Parameters.AddWithValue("UTM_Destino3", utiles.BlancoANull(Me.txtUTMDestino3.Text))
            comando.Parameters.AddWithValue("Observaciones", utiles.BlancoANull(Me.txtobservaciones.Text))

            comando.ExecuteNonQuery()

         
        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                'Case 547
                '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    Alert(Page, "La Motobomba ya existe para la fecha de revision: " & txtFecharevision.Text)
            End Select
        End Try


        'RDF
        'Fecha: 13/02/2009
        'Se comprueba si tiene registros en PVYCR_ElementosMedida_Motobombas y si tiene registros en PVYCR_ElementosMedida por asignar
        daElementos.SelectCommand.CommandText = "select *   from PVYCR_ElementosMedida_MotoBombas WHERE PVYCR_ElementosMedida_MotoBombas.codigoMotobomba='" & Me.txtcodigoMotobomba.Text & "'"
        daElementos.Fill(dstElementos, "TablaElementos")
        If dstElementos.Tables("TablaElementos").Rows.Count = 0 Then
            Alert(Page, "A continuación se le muestra los datos para relacionar los Elementos de Medida")
            pnlEDMotobombas.Visible = False
            pnlMotobombas.Visible = False
            pnlRelacionarElemento.Visible = True
            pnlRelacion.Visible = False
            lblMotobombaRel.Text = Me.txtcodigoMotobomba.Text
            lblFechaRevRel.Text = Me.txtFecharevision.Text
            lblTituloRel.Text = "<b>ELEMENTOS RELACIONADOS CON LA MOTOBOMBA " & lblMotobombaRel.Text & " CON FECHA REVISIÓN " & Me.txtFecharevision.Text & "</b>"
            'RDF 
            'Fecha: 05/02/2009
            crearDataSetsHistorial(Me.txtcodigoMotobomba.Text, Me.txtFecharevision.Text)
            'RDF
            'Fecha: 11/02/2009
            RellenarListaElementos(Me.txtcodigoMotobomba.Text, Me.txtFecharevision.Text)

        Else
            Me.pnlMotobombas.Visible = True
            Me.pnlEDMotobombas.Visible = False
            crearDataSets()


        End If


        'Me.ucPaginacion.Visible = True
        'ucPaginacion.lblPaginatext = "1"
        'txtFiltroCodigoPVYCR.Text = "[Código Motobomba]"
        'ucPaginacion.lblNumpaginasDatabind()

    End Sub




    Protected Sub rptMotobombas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptMotobombas.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String
        Dim objTrans As SqlTransaction

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "relacionar"
             
                pnlEDMotobombas.Visible = False
                pnlMotobombas.Visible = False
                pnlRelacionarElemento.Visible = True
                pnlRelacion.Visible = False
                lblMotobombaRel.Text = parametros(0)
                lblFechaRevRel.Text = parametros(1)
                lblTituloRel.Text = "<b>ELEMENTOS RELACIONADOS CON LA MOTOBOMBA " & lblMotobombaRel.Text & " CON FECHA REVISIÓN " & parametros(1) & "</b>"
                'RDF 
                'Fecha: 05/02/2009
                crearDataSetsHistorial(parametros(0), parametros(1))
                'RDF
                'Fecha: 11/02/2009
                RellenarListaElementos(parametros(0), parametros(1))

            Case "editar"
                Session("NuevaMotoBomba") = 0
                Me.pnlEDMotobombas.Visible = True
                Me.pnlMotobombas.Visible = False
                Me.lblTitulo.Text = "Motobomba " + parametros(0)
                btnAceptar.Visible = True
                'Me.ucPaginacion.Visible = False
                lblMotobombaSel.Visible = False
                lblMotobombaSel.Text = parametros(0)

                crearDataSetMotoBomba(parametros(0), parametros(1))
                If dstMotobombas.Tables("TablaMotoBombas").Rows.Count > 0 Then
                    'Datos de la Motobomba
                    txtcodigoMotobomba.Enabled = False
                    txtFecharevision.Enabled = False
                    Me.txtcodigoMotobomba.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("CodigoMotoBomba"))
                    Me.txtCodigoPVYCR.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("codigoPVYCR"))
                    Me.txtFecharevision.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("FechaRevision"))
                    Me.txtFechaIni.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("FechaInicial"))
                    Me.txtFechaFin.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("FechaFin"))
                    Me.txtdescripcion.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Descripcion"))
                    Me.txtGMotobomba.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Grupos_Motobomba"))
                    Me.txtmarcaBomba.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("MarcaBomba"))
                    Me.txtTipoMotobomba.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("TipoBomba"))
                    Me.txtNSerieMotobomba.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("NumSerieBomba"))
                    Me.txtModelo.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("ModeloBomba"))
                    Me.txtQnBomba.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Qn_Bomba"))
                    Me.txtMarcaMotor.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("MarcaMotor"))
                    Me.txtModeloMotor.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("ModeloMotor"))
                    Me.txttipoMotor.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("TipoMotor_H_V"))
                    Me.txtNSerieMotobombaMotor.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("NumSerieMotor"))
                    Me.txtRPM.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("RPM"))
                    Me.txtPotencia_CV.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Potencia_CV"))
                    Me.txtPotencia_kw.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Potencia_KW"))
                    Me.txttipoPot.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Tipo_Pot"))
                    Me.txtPotenciaTeorica_kw.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("PotenciaTeorica_kw"))
                    Me.txtDDPMotor.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("DDP_Motor_V"))
                    Me.txtIntensidadMotor_A.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Intensidad_Motor_A"))
                    Me.txtCosFi.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Cos_Fi"))
                    Me.txtCaudal_LSeg.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Caudal_LSeg"))
                    Me.txtTipoCaudal.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Tipo_Caudal"))
                    Me.txtAlturaGeometrica.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Altura_Geometrica_Impulsion"))
                    Me.txtAlturaManometrica.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Altura_Manometrica_Teorica"))
                    Me.txtTipoAlt.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Tipo_Alt"))
                    Me.txtProfundidadEstatica.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Profundidad_Nivel_Estatico"))
                    Me.txtProfundidadDinamica.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Profundidad_Nivel_Dinamico"))
                    Me.txtLongitudAspiracion.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Longitud_Aspiracion"))
                    Me.txtDnAspiracion.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Dn_Aspiracion"))
                    Me.txtMaterialTubAsp.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Material_Tub_Aspiracion"))
                    Me.txtLongImpulsion.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Longitud_Impulsion"))
                    Me.txtDnImpulsion.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Dn_Impulsion"))
                    Me.txtMaterialtubImp.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Material_Tub_Impulsion"))
                    Me.txtVolimetro.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Voltimetro"))
                    Me.txtAmperimetro.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Amperimetro"))
                    Me.txtManometro.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Manometro_Kgcm2"))
                    Me.txtRev10min.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Rev10min"))
                    Me.txtBombaEnFuncionamiento.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("BombaEnFuncionamiento"))
                    Me.txtCotaToma.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Cota_Toma"))
                    Me.txtDestino1.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Destino1"))
                    Me.txtDestino2.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Destino2"))
                    Me.txtDestino3.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Destino3"))
                    Me.txtCotaDestino1.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Cota_Destino1"))
                    Me.txtCotaDestino2.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Cota_Destino2"))
                    Me.txtCotaDestino3.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Cota_Destino3"))
                    Me.txtUTMDestino1.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("UTM_Destino1"))
                    Me.txtUTMDestino2.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("UTM_Destino2"))
                    Me.txtUTMDestino3.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("UTM_Destino3"))
                    Me.txtobservaciones.Text = utiles.nullABlanco(dstMotobombas.Tables("TablaMotobombas").Rows(0).Item("Observaciones"))
                End If
                rptMotobombas.DataSource = dstMotobombas.Tables("TablaMotoBombas")
                rptMotobombas.DataBind()


            Case "borrar"
                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion)
                objTrans = conexion.BeginTransaction()

                Try
                    comando.Transaction = objTrans
                    'En primer lugar, se eliminan las relaciones que tiene la motobomba
                    comando.CommandText = "delete from PVYCR_ElementosMedida_MotoBombas where CodigoMotobomba='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
                    comando.ExecuteNonQuery()
                    'Se elimina la motobomba correspondiente
                    comando.CommandText = "delete from PVYCR_MotoBombas where CodigoMotobomba='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
                    comando.ExecuteNonQuery()

                    objTrans.Commit()

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar la motobomba")
                    objTrans.Rollback()
                End Try
                crearDataSets()
                'ucPaginacion.lblNumpaginasDatabind()


        End Select
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.pnlMotobombas.Visible = True
        Me.pnlEDMotobombas.Visible = False
        'Me.ucPaginacion.Visible = True
        'ucPaginacion.lblPaginatext = "1"
        'txtFiltroCodigoPVYCR.Text = "[Código Motobomba]"
        crearDataSets()
        'ucPaginacion.lblNumpaginasDatabind()

    End Sub

    Protected Sub txtPotencia_kw_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPotencia_kw.TextChanged

    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
                      "window.open('../listados/ListadoMotobomba.aspx?codigoMotobomba=" & txtcodigoMotobomba.Text & "&fechaRevision=" & txtFecharevision.Text & "')" & _
                     "</script>")
    End Sub

    Protected Sub btnListadoPantalla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListadoPantalla.Click
        Page.RegisterClientScriptBlock("abreInforme", "<script language=javascript>" & _
                          "window.open('../listados/ListadoMotobombaPantalla.aspx?codigoMotobomba=" & txtFiltroCodigoPVYCR.Text & "')" & _
                         "</script>")
    End Sub

    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idperfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function
    Protected Sub AceptarFiltro(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20080617
        Dim strFiltro As String
        strFiltro = ""
        If txtFCodigoMotobomba.Text <> "" Then
            strFiltro = " AND CodigoMotobomba like '" + txtFCodigoMotobomba.Text + "' "
        End If
        'If txtFCodigoPVYCR.Text <> "" Then
        '    strFiltro = strFiltro + " AND CodigoPVYCR like '" + txtFCodigoPVYCR.Text + "' "
        'End If


        If txtFFechaRevision.Text <> "" Then
            strFiltro = strFiltro + " AND  FechaRevision between '" & txtFFechaRevision.Text & " 00:00:00' and '" & txtFFechaRevision.Text & " 23:59:59' "
        End If

        If txtFDescripcion.Text <> "" Then
            strFiltro = strFiltro + " AND Descripcion like '" + txtFDescripcion.Text + "' "
        End If

        'If txtFMarca.Text <> "" Then
        '    strFiltro = strFiltro + " AND MarcaBomba like '" + txtFMarca.Text + "' "
        'End If

        'If txtFTipo.Text <> "" Then
        '    strFiltro = strFiltro + " AND TipoBomba like '" + txtFTipo.Text + "' "
        'End If

        'If txtFNSerie.Text <> "" Then
        '    strFiltro = strFiltro + " AND NumSerieBomba like '" + txtFNSerie.Text + "' "
        'End If

        'If txtFModelo.Text <> "" Then
        '    strFiltro = strFiltro + " AND ModeloBomba like '" + txtFModelo.Text + "' "
        'End If
        If txtFiltrado.Text <> "" Then
            If ddlFiltrado.SelectedValue = "FechaInicial" Or ddlFiltrado.SelectedValue = "FechaFin" Then

                strFiltro = strFiltro + " AND convert(varchar(10)," + ddlFiltrado.SelectedValue + ",103) LIKE '" + txtFiltrado.Text + "'"

            Else
                strFiltro = strFiltro + " AND " + ddlFiltrado.SelectedValue + " LIKE '" + txtFiltrado.Text + "'"
            End If
        End If
        Session("FiltroMot") = strFiltro
        crearDataSets()
    End Sub

  
    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        'RDF 
        '29/10/2008
        crearDataSets()
    End Sub

    Protected Sub treeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView1.SelectedNodeChanged
        Dim TipoNodo As String
        Dim v() As String
        Try
            v = Split(treeView1.SelectedNode.Value, "#")
            TipoNodo = v(1)
            If TipoNodo = "E" Then
                'lblDesCodigoPVYCR.Text = "Punto de Asociación: " & treeView1.SelectedNode.Text
                txtElementoMedida.Text = v(3) & "-" & v(4)
                If txtcodigoMotobomba.Text = "" Then txtcodigoMotobomba.Text = txtElementoMedida.Text & "-"
            Else

                Alert(Page, "Seleccione un Elemento de Medida para asociar el contador.")
            End If
        Catch mierror As Exception
            'Error en la seleccion del arbol
            Alert(Page, "Seleccione de nuevo el Elemento de Medida, no se registró correctamente")
        End Try

    End Sub

    Private Function GetDataSetArbol() As DataSet
        'EGB. El arbol debe cargarse a partir de los datos de PVYCR_Arbol para no regenerar el XML en cada actualización

        crearDataSetsPuntosXML2()
        Return dstarbol
        'Session("DSLoaded") = 1

    End Function

    Private Sub crearDataSetsPuntosXML2()
        Try
            dstarbol.Clear()
            treeView1.Nodes.Clear()

            'ARBOL NORMALIZADO
            daMotobombas.SelectCommand.CommandText = "Select * from PVYCR_ARBOL"
            daMotobombas.Fill(dstarbol, "tablaArbol")

            dstarbol.Relations.Add("SelfRefenceRelation", _
                            dstarbol.Tables("tablaArbol").Columns("IdArbol"), _
                            dstarbol.Tables("tablaArbol").Columns("IdArbolPadre"), True)
            dstarbol.Relations("SelfRefenceRelation").Nested = True

        Catch exp As Exception
        End Try
    End Sub

    Protected Sub nuevaRelacion(ByVal sender As Object, ByVal e As System.EventArgs)
        pnlRelacion.Visible = True
        txtElementoMedida.Text = ""
        lblElementoRel.Text = ""

    End Sub
    Private Sub crearDataSetHistorial(ByVal parametros0 As String, ByVal parametros1 As String, ByVal parametros2 As String, ByVal parametros3 As String)
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daElementoMotobomba.SelectCommand.CommandText = "select * from PVYCR_ElementosMedida_MotoBombas where codigoMotobomba='" & parametros0 & "' and FechaRevision= '" & parametros1 & "' AND " & _
                                                " idElementoMedida='" & parametros2 & "' AND CodigoPVYCR='" & parametros3 & "'"
        daElementoMotobomba.Fill(dstElementoMotobomba, "TablaElementoMotobomba")
    End Sub

    Protected Sub rptHistorial_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptHistorial.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String
        Dim objTrans As SqlTransaction
        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "borrar"
                'RDF 10/02/2009 Borrar registro PVYCR_ElementosMedida_MotoBombas

                If conexion.State = ConnectionState.Closed Then conexion.Open()
                objTrans = conexion.BeginTransaction()

                Try
                    comando.Transaction = objTrans
                    'En primer lugar, se eliminan el registro correspondiente a PVYCR_Contadores_ElementosMedida
                    comando.CommandText = "delete from PVYCR_ElementosMedida_MotoBombas where codigoMotobomba='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' and idElementoMedida='" & parametros(2) & "' AND CodigoPVYCR='" & parametros(3) & "'"
                    comando.ExecuteNonQuery()
                    objTrans.Commit()
                    crearDataSetsHistorial(parametros(0), parametros(1))
                    pnlRelacion.Visible = False
                    RellenarListaElementos(parametros(0), parametros(1))

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el registro correspondiente")
                    objTrans.Rollback()
                End Try
            Case "editar"
                If treeView1.Nodes.Count = 0 Then
                    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "") 'EGB el nuevo parametro E hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
                End If

                pnlRelacion.Visible = True
                crearDataSetHistorial(parametros(0), parametros(1), parametros(2), parametros(3))
                lblElementoRel.Text = parametros(3) & "-" & parametros(2)
                txtElementoMedida.Text = utiles.nullABlanco(dstElementoMotobomba.Tables("TablaElementoMotobomba").Rows(0).Item("codigoPVYCR")) & "-" & utiles.nullABlanco(dstElementoMotobomba.Tables("TablaElementoMotobomba").Rows(0).Item("idElementoMedida"))

        End Select
    End Sub
    Private Sub crearDataSetsHistorial(ByVal codigoMotobomba As String, ByVal FechaRevision As String)
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)

        sentenciaSelHistorial = " SELECT  CodigoPVYCR, idElementoMedida, codigoMotobomba,FechaRevision FROM   PVYCR_ElementosMedida_MotoBombas "

        If codigoMotobomba <> "" Then
            sentenciaSelHistorial = sentenciaSelHistorial & " where codigoMotobomba = '" & codigoMotobomba & "' and FechaRevision='" & FechaRevision & "' "
        End If

        daElementoMotobomba.SelectCommand.CommandText = sentenciaSelHistorial
        daElementoMotobomba.Fill(dstElementoMotobomba, "TablaElementoMotobomba")
        'daContadores.Fill(dstContadores, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaContadores")


        'ucPaginacion.calcularPags(txtComando)

        rptHistorial.DataSource = dstElementoMotobomba.Tables("TablaElementoMotobomba")
        rptHistorial.DataBind()


    End Sub
    Protected Sub btnCancelarRel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarRel.Click
        pnlRelacionarElemento.Visible = True
        pnlRelacion.Visible = False
        pnlMotobombas.Visible = False
        txtElementoMedida.Text = ""

    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        pnlMotobombas.Visible = True
        pnlRelacionarElemento.Visible = False
        pnlRelacion.Visible = False
        lblMotobombaRel.Text = ""
        lblFechaRevRel.Text = ""
        txtElementoMedida.Text = ""
        lblElementoRel.Text = ""
        crearDataSets()

    End Sub

    Protected Sub btnAnyadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnyadir.Click

        'RDF
        'Fecha: 06/02/2009
        'Se da de alta el registro de PVYCR_ElementosMedida_MotoBombas
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)


        Try
            If (txtElementoMedida.Text <> "" And lblMotobombaRel.Text <> "" And lblElementoRel.Text = "") Then
                'Insertamos un nuevo usuario
                comando.CommandText = "insert into [PVYCR_ElementosMedida_MotoBombas]([CodigoPVYCR],[idElementoMedida],[codigoMotobomba],[FechaRevision]) " & _
               " VALUES  " & _
               " ('" & txtElementoMedida.Text.Substring(0, txtElementoMedida.Text.Length - 4) & "','" & txtElementoMedida.Text.Replace(txtElementoMedida.Text.Substring(0, txtElementoMedida.Text.Length - 3), "") & "','" & lblMotobombaRel.Text & "','" & lblFechaRevRel.Text & "')"
                comando.ExecuteNonQuery()

            Else
                comando.CommandText = "update PVYCR_ElementosMedida_MotoBombas set CodigoPVYCR = '" & txtElementoMedida.Text.Substring(0, txtElementoMedida.Text.Length - 4) & "' " & _
                                ",idElementoMedida='" & txtElementoMedida.Text.Replace(txtElementoMedida.Text.Substring(0, txtElementoMedida.Text.Length - 3), "") & "' " & _
                                " where codigoMotobomba='" & lblMotobombaRel.Text & "' and FechaRevision='" & lblFechaRevRel.Text & "' AND CodigoPVYCR='" & lblElementoRel.Text.Substring(0, lblElementoRel.Text.Length - 4) & "' AND " & _
                                " IdElementoMedida='" & lblElementoRel.Text.Replace(lblElementoRel.Text.Substring(0, lblElementoRel.Text.Length - 3), "") & "'"
                comando.ExecuteNonQuery()
            End If
            crearDataSetsHistorial(lblMotobombaRel.Text, lblFechaRevRel.Text)
            pnlRelacion.Visible = False
            RellenarListaElementos(lblMotobombaRel.Text, Me.txtFecharevision.Text)
        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                'Cas e 547
                '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                Case 2627
                    Alert(Page, "La motobomba ya está relacionada con el Elemento de Medida")
            End Select
        End Try

    End Sub

    

    Private Sub RellenarListaElementos(ByVal codigoMotobomba As String, ByVal fechaRevision As Date)
        'RDF
        'Fecha: 11/02/2009
        'Rellena la lista con los elementos de medida asociados al CodigoPVYCR
        Dim codigoPVYCR As String

        codigoPVYCR = codigoMotobomba.Substring(0, Len(codigoMotobomba) - 3)

        daElementos.SelectCommand.CommandText = "select PVYCR_ElementosMedida.CodigoPVYCR + ' - ' + PVYCR_ElementosMedida.idElementoMedida as Elemento,*   from PVYCR_ElementosMedida WHERE codigoPVYCR='" & codigoPVYCR & "' AND NOT EXISTS(SELECT * FROM  PVYCR_ElementosMedida_MotoBombas " & _
        " WHERE PVYCR_ElementosMedida_MotoBombas.idElementoMedida=PVYCR_ElementosMedida.idElementoMedida AND PVYCR_ElementosMedida_MotoBombas.codigoPVYCR=PVYCR_ElementosMedida.codigoPVYCR AND PVYCR_ElementosMedida_MotoBombas.codigoMotobomba='" & codigoMotobomba & "' AND FECHAREVISION = '" & fechaRevision & "')"
        daElementos.Fill(dstElementos, "TablaElementos")

        lstElementos.Items.Clear()
        lstElementos.DataSource = dstElementos.Tables("TablaElementos")
        lstElementos.DataTextField = "Elemento"
        lstElementos.DataValueField = "idElementoMedida"
        lstElementos.DataBind()

    End Sub

    Protected Sub btnAceptarElementos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptarElementos.Click
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)

        Try
            If lstElementos.Items.Count > 0 Then
                Dim codigoPVYCR As String

                Dim item As ListItem

                codigoPVYCR = lblMotobombaRel.Text.Substring(0, Len(lblMotobombaRel.Text) - 3)
                For Each item In lstElementos.Items
                    If item.Selected = True Then
                        'Insertamos un nuevo registro
                        comando.CommandText = "insert into [PVYCR_ElementosMedida_MotoBombas]([CodigoPVYCR],[idElementoMedida],[codigoMotobomba],[FechaRevision]) " & _
                       " VALUES  " & _
                       " ('" & codigoPVYCR & "','" & item.Value & "','" & lblMotobombaRel.Text & "','" & lblFechaRevRel.Text & "')"
                        comando.ExecuteNonQuery()
                    End If
                Next item

            End If
            crearDataSetsHistorial(lblMotobombaRel.Text, lblFechaRevRel.Text)
            RellenarListaElementos(lblMotobombaRel.Text, lblFechaRevRel.Text)

        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                Case 2627
                    Alert(Page, "La motobomba ya está relacionada con el Elemento de Medida")
            End Select
        End Try

    End Sub
    Protected Sub rellenarListas()
        'Dim dstTipoCauce As New DataSet
        Dim dstFiltrado As New DataSet
        Try
            'ncm rellenamos el campo filtrado con las columnas de la tabla de cauces
            utiles.Comprobar_Conexion_BD(Page, conexion)
            daMotobombas.SelectCommand.CommandText = " SELECT Column_Name as columna  FROM INFORMATION_SCHEMA. COLUMNS where Table_Name = 'PVYCR_MOTOBOMBAS' and COLUMN_NAME NOT IN ('CODIGOMOTOBOMBA','FECHAREVISION','DESCRIPCION') "
            daMotobombas.Fill(dstFiltrado, "TablaFiltrado")
            ddlFiltrado.DataSource = dstFiltrado.Tables("TablaFiltrado")
            ddlFiltrado.DataValueField = "columna"
            ddlFiltrado.DataBind()
            ddlFiltrado.Items.Insert(0, New ListItem("[Seleccionar]", ""))

        Catch ex As Exception

        End Try



    End Sub
    Protected Function ObtenerFiltrado(ByVal eldataitem As DataRowView) As String
        If utiles.nullABlanco(txtFiltrado.Text) <> "" And utiles.nullABlanco(ddlFiltrado.SelectedValue) <> "" Then
            Return eldataitem("" + ddlFiltrado.SelectedValue + "")
        End If
    End Function

    Protected Sub btnListFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListFiltro.Click
        Dim ventanaChecks = "SelecCamposFiltradosMto.aspx?mto=Motobombas"

        If utiles.nullABlanco(txtNumRegFiltrados.Text) <> "" Or utiles.nullABlanco(txtNumRegFiltrados.Text) = "0" Then
            ClientScript.RegisterStartupScript(Page.GetType, "abreventanaChecks", "<script language=javascript>window.open('" + ventanaChecks + "','','toolbar=no,resizable=no, scrollbar=no, status=no')</script>")
        Else
            JavaScript.Alert(Page, "No hay elementos filtrados")
        End If
    End Sub
End Class
