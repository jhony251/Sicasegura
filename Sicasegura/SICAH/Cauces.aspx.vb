Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.IO
Imports SICAH_FuncionesArbolExt
Partial Class SICAH_cauces
    Inherits System.Web.UI.Page

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daCauces As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstCauces As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()
    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0
    Dim midstCauces As DataSet = New System.Data.DataSet()




    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'ucPaginacion.ruta = "../"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Page.MaintainScrollPositionOnPostBack = True
        lbtNuevo.DataBind()

        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        lblCabecera.Text = genHTML.cabecera(Page)
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            If Request.QueryString("asociacion") <> "yes" Then
                lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))
            End If
            If Request.QueryString("nuevo") = "yes" Then
                pnlEDCauces.Visible = True
                pnlCauces.Visible = False
                'ucPaginacion.Visible = False
                lblTitulo.Text = "&nbsp;<b>NUEVO CAUCE</b>"
                '-------------------------------------------------------------------
                'EGB 16/07/2008 Asociación con el Arbol de Cauces 
                If treeView1.Nodes.Count = 0 Then
                    'EGB el nuevo parametro P hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
                    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "P")
                End If
                'EGB 13/11/2008 Ocultamos botón imprimir en caso de estar creando un cauce nuevo
                Me.btnImprimir.Visible = False
            End If

            imgArbol.Attributes.Add("onclick", "desplegarArbol(this);")
            imgCerrarVentana.Attributes.Add("onclick", "desplegarArbol(this);")

            If Request.QueryString("CodigoCauce") <> "" Then
                Session("nuevoCauce") = 0
                Me.pnlEDCauces.Visible = True
                Me.pnlCauces.Visible = False

                'EGB 13/11/2008 Mostramos botón imprimir en caso de estar en modo edición
                Me.btnImprimir.Visible = False

                btnAceptar.Visible = True

                'Me.ucPaginacion.Visible = False
                lblCauceSel.Text = Request.QueryString("CodigoCauce") 'e.CommandArgument
                crearDataSetCauce(lblCauceSel.Text)
                cargarControlesEdicion() 'EGB Jun-08: Ver nota en definicion de la funcion 
                rptCauces.DataSource = dstCauces.Tables("TablaCauces")
                rptCauces.DataBind()

            End If

            'If Request.QueryString("pag") <> "" Then
            '    ucPaginacion.lblPaginatext = Request.QueryString("pag")
            'Else
            '    ucPaginacion.lblPaginatext = "1"
            'End If

            crearDataSets()
            'RDF 03/10/2008
            rellenarListas()
            'ucPaginacion.DataBind()
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
        utiles.Comprobar_Conexion_BD(Page, conexion)
        If txtRegistros.Text = "" Then
            sentenciaSel = " SELECT "
        Else
            sentenciaSel = " SELECT TOP " & txtRegistros.Text
        End If

        sentenciaSel = sentenciaSel & " CodigoCauce, CodigoInventario90, CodigoCampo, TipoCauce, MargenDerivacion, DenominacionCauce, OtrasReferencias, ParajeToma, MunicipioToma, " & _
                        " ProvinciaToma, DatosAdministrador, NombreTitular, NIFTitular, DireccionTitular, MunicipioTitular, CodPostalTitular, ProvinciaTitular, TelefonoTitular, " & _
                        " ReferenciaExpedienteLibroAprovechamiento, NumeroRegistroAntiguo, ReferenciaExpedientesRegistroAguas, OtrosExpedientes, Seccion, Tomo, Hoja, " & _
                        " NombreContactoCampo, DireccionContactoCampo, TelefonoContactoCampo, CaudalMaximo_LSeg, VolumenMaximoAnualLegal_M3, " & _
                        " VolumenMaximoAnualTeorico_M3, X_UTM_Toma, Y_UTM_Toma, CotaToma, SuperficieRealAproximada_HAS, SuperficieInscrita_HAS, " & _
                        " PorcentajeTradicional, TipoCultivo, LongitudCauce_KM, Observaciones, EntreOjosYContraparada, EnActivo, MedidoEnPVYCR, TitularContactado, " & _
                        " ContadorOK, INSCRIPCION, INSCRIPCION_ESTADO, INSCRIPCION_RELACIONADA   FROM PVYCR_Cauces "

        'If txtFiltroCodigoCauce.Text <> "[Código Cauce]" And txtFiltroCodigoCauce.Text <> "" Then
        '    sFiltro = " where CodigoCauce like '" & txtFiltroCodigoCauce.Text & "' "
        'Else
        '    sFiltro = ""
        'End If

        'RDF 20081002
        If Session("FiltroCauce") <> "" Then
            If sFiltro = "" Then
                sFiltro = " WHERE (1=1) " + Session("FiltroCauce")
            Else
                sFiltro = sFiltro + Session("FiltroCauce")
            End If
        End If
        'ncm necesitamos una global que nos va a servir en todos los mtos para poder listar los registros
        'que han filtrado en cada mto.
        Session("FiltroListado") = Session("FiltroCauce")
        sentenciaOrder = " order by CodigoCauce "

        If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
            sentenciaSel = sentenciaSel & sentenciaOrder
        End If

        daCauces.SelectCommand.CommandText = sentenciaSel
        'daCauces.Fill(dstCauces, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaCauces")
        daCauces.Fill(dstCauces, "TablaCauces")
        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daCauces.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))

        'ucPaginacion.calcularPags(txtComando)
        'ncm calculamos el nº de registros que devolverá el filtro 
        txtNumRegFiltrados.Text = FuncionesGenerales.CalcularNumRegFiltrados(Session("FiltroCauce"), conexion, sFiltro, "PVYCR_Cauces", Page)

        rptCauces.DataSource = dstCauces.Tables("TablaCauces")
        rptCauces.DataBind()



    End Sub
    Private Function ExisteCauce(ByVal strCodigo As String) As String
        Dim strSQL = "SELECT * FROM PVYCR_Cauces WHERE CodigoCauce=" & strCodigo
        daCauces.SelectCommand.CommandText = strSQL
        daCauces.Fill(dstCauces, "TablaCaucesAux")
        If dstCauces.Tables("").Rows.Count = 0 Then
            ExisteCauce = ""
        Else
            ExisteCauce = dstCauces.Tables("").Columns("DenominacionCauce").ToString
        End If
    End Function
    Private Function GetDataSetArbol() As DataSet
        'EGB. El arbol debe cargarse a partir de los datos de PVYCR_Arbol para no regenerar el XML en cada actualización

        crearDataSetsPuntosXML2()
        Return dstarbol
        'Session("DSLoaded") = 1

    End Function

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
            daCauces.SelectCommand.CommandText = "Select * from dbo.PVYCR_ARBOL"
            daCauces.Fill(dstarbol, "tablaArbol")

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

    '        crearDataSetsPuntosXML2(dstarbol, treeView1, daCauces)

    '        dstarbol.WriteXml(MapPath("xmlDataSet.xml"))

    '        Return dstarbol
    '        Session("DSLoadedCauces") = 1
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


    Private Sub crearDataSetCauce(ByVal StrCodigoCauceSeleccionado As String)
        utiles.Comprobar_Conexion_BD(Page, conexion)
        daCauces.SelectCommand.CommandText = "select * from PVYCR_Cauces where CodigoCauce='" & lblCauceSel.Text & "'"
        daCauces.Fill(dstCauces, "TablaCauces")

        'EGB 18/07/2008 Crear Tabla en DataSet auxiliar para localizar nodos padres en la
        'nueva caracteristica en la edicion de Cauces via botón de carga del arbol de cauces en modo ventana.
        sentenciaSel = "SELECT A.IdArbol, A.Descripcion, A.CodigoCauce, A.ClaveNodo, AP.IdArbol as IdArbolPadre, AP.Descripcion as DescripcionPadre, AP.CodigoCauce As CodigoCaucePadre, AP.ClaveNodo as ClaveNodoPadre, AP.StrNodo as StrNodoPadre FROM PVYCR_Arbol A LEFT join PVYCR_ARBOL AP ON A.IdArbolPadre=AP.IdArbol WHERE A.Tipo='C'" & _
                       "AND A.CodigoCauce='" & StrCodigoCauceSeleccionado & "'"
        daCauces.SelectCommand.CommandText = sentenciaSel
        daCauces.Fill(dstCauces, "TablaArbolAuxiliar")


    End Sub
  Private Function GetIdArbol(ByVal StrCodigoCauceSeleccionado As String, ByVal objTrans As SqlTransaction) As Integer
    'Dim midstCauces As New DataSet
    'daCauces.SelectCommand.CommandText = "select * from PVYCR_Cauces where CodigoCauce='" & lblCauceSel.Text & "'"
    'daCauces.Fill(midstCauces, "TablaCauces")

    'EGB 18/07/2008 Crear Tabla en DataSet auxiliar para localizar nodos padres en la
    'nueva caracteristica en la edicion de Cauces via botón de carga del arbol de cauces en modo ventana.
    GetIdArbol = 0
    If StrCodigoCauceSeleccionado = "" Then
      Alert(Page, "Debe seleccionar previamente un nodo Raiz para crear el cauce")
    Else
      utiles.Comprobar_Conexion_BD(Page, conexion)

      sentenciaSel = "SELECT A.IdArbol, A.Descripcion, A.CodigoCauce, A.ClaveNodo, AP.IdArbol as IdArbolPadre, AP.Descripcion as DescripcionPadre, AP.CodigoCauce As CodigoCaucePadre, AP.ClaveNodo as ClaveNodoPadre, AP.StrNodo as StrNodoPadre, AP.strNodoPadre as strNodoRaiz FROM PVYCR_Arbol A LEFT join PVYCR_ARBOL AP ON A.IdArbolPadre=AP.IdArbol WHERE A.Tipo='C'" & _
                           "AND A.CodigoCauce='" & StrCodigoCauceSeleccionado & "'"
      daCauces.SelectCommand.Transaction = objTrans
      daCauces.SelectCommand.CommandText = sentenciaSel
      daCauces.Fill(midstCauces, "TablaArbolAuxiliar")
      If midstCauces.Tables("TablaArbolAuxiliar").Rows.Count = 0 Then
        Alert(Page, "Debe seleccionar previamente un nodo Raiz para crear el cauce")
      Else
        GetIdArbol = CInt(midstCauces.Tables("TablaArbolAuxiliar").Rows(0).Item("IdArbol").ToString)
      End If
    End If
    Return GetIdArbol
  End Function
  '  Private Function GetNivelIdArbol(ByVal IdArbol As Integer, ByVal objTrans As SqlTransaction) As Integer
  '      Dim midstArbol As New DataSet
  '      Dim l_GetNivelIdArbol As Integer


  '      'EGB 27/11/2008 Función que retorna el nivel al que pertene un determinado nodo dentro de PVYCR_ARBOL
  '      'utiles.Comprobar_Conexion_BD(Page, conexion)
  '      sentenciaSel = "SELECT IdArbolPadre FROM PVYCR_Arbol WHERE IdArbol=" & IdArbol
  '      daCauces.SelectCommand.Transaction = objTrans
  '      daCauces.SelectCommand.CommandText = sentenciaSel
  '      daCauces.Fill(midstArbol, "TablaArbolAuxiliar")

  '      If midstArbol.Tables("TablaArbolAuxiliar").Rows.Count = 0 Then
  '          l_GetNivelIdArbol = 0
  '      Else
  '          If midstArbol.Tables("TablaArbolAuxiliar").Rows.Item(0).Item("IdArbolPadre") Is System.DBNull.Value Then
  '              l_GetNivelIdArbol = 1
  '          Else
  '              l_GetNivelIdArbol = GetNivelIdArbol(midstArbol.Tables("TablaArbolAuxiliar").Rows.Item(0).Item("IdArbolPadre"), objTrans) + 1
  '          End If
  '      End If
  '      Return l_GetNivelIdArbol
  'End Function
  'Private Function GetY(ByVal IdArbolPadre As Integer, ByVal objTrans As SqlTransaction, ByVal x As Integer) As Integer
  '  Dim midstArbol As New DataSet
  '  Dim y As Integer = 0

  '  'ncm 27/04/2010 obtenemos la Y del árbol para rellenarla
  '  'utiles.Comprobar_Conexion_BD(Page, conexion)
  '  sentenciaSel = "SELECT ISNULL(MAX(y),0) FROM PVYCR_Arbol WHERE idarbolpadre=" & IdArbolPadre & " and tipo = 'C' AND X=" & x
  '  daCauces.SelectCommand.Transaction = objTrans
  '  daCauces.SelectCommand.CommandText = sentenciaSel
  '  y = daCauces.SelectCommand.ExecuteScalar
  '  ' si es cero es porque no hay ningún cauce y la y valdrá 0, sino valdrá el max(y) + 1
  '  If y < 0 Then
  '    y = y + 1
  '  End If
  '  Return y
  'End Function


    Protected Sub nuevoCauce(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCauceSel.Text = ""
        Response.Redirect("Cauces.aspx?nuevo=yes")

    End Sub


    'Protected Sub btnFiltroAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptar.Click
    '    'ucPaginacion.lblPaginatext = "1"
    '    crearDataSets()
    '    'ucPaginacion.lblNumpaginasDatabind()
    'End Sub
    'Protected Sub btnFiltrocancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelar.Click, btnFiltroCancelar.Click
    '    'ucPaginacion.lblPaginatext = "1"
    '    'txtFiltroCodigoCauce.Text = "[Código Cauce]"
    '    crearDataSets()
    '    'ucPaginacion.lblNumpaginasDatabind()
    'End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim comandoTemp As SqlCommand = New SqlCommand("", conexion)
        Dim comandoAux As SqlCommand = New SqlCommand("", conexion)
        Dim clavenuevonodoPVYCRArbol As String
        Dim objTrans As SqlTransaction
        Dim IdArbolRaiz As Integer
    Dim NivelIdArbolRaiz As Integer
    Dim columnaY As Integer
    Dim strNodoRaiz As String

        'If conexion.State = ConnectionState.Closed Then conexion.Open()
        utiles.Comprobar_Conexion_BD(Page, conexion)
        objTrans = conexion.BeginTransaction()
        comando.Parameters.Clear()

        Try
            comando.Transaction = objTrans
            If lblCauceSel.Text = "" Then
                'Insertamos un nuevo Cauce
                comando.CommandText = "INSERT INTO [PVYCR_Cauces]  ([CodigoCauce]" & _
                                      " ,[CodigoInventario90],[CodigoCampo],[TipoCauce],[MargenDerivacion],[DenominacionCauce] " & _
                                      " ,[OtrasReferencias],[ParajeToma],[MunicipioToma],[ProvinciaToma],[DatosAdministrador],[NombreTitular]" & _
                                      " ,[NIFTitular],[DireccionTitular],[MunicipioTitular],[CodPostalTitular],[ProvinciaTitular],[TelefonoTitular]" & _
                                      " ,[ReferenciaExpedienteLibroAprovechamiento],[NumeroRegistroAntiguo],[ReferenciaExpedientesRegistroAguas]" & _
                                      " ,[OtrosExpedientes],[Seccion],[Tomo],[Hoja],[NombreContactoCampo],[DireccionContactoCampo],[TelefonoContactoCampo]" & _
                                      " ,[CaudalMaximo_LSeg],[VolumenMaximoAnualLegal_M3],[VolumenMaximoAnualTeorico_M3],[X_UTM_Toma]" & _
                                      " ,[Y_UTM_Toma],[CotaToma],[SuperficieRealAproximada_HAS],[SuperficieInscrita_HAS],[PorcentajeTradicional]" & _
                                      " ,[TipoCultivo],[LongitudCauce_KM],[Observaciones],[EntreOjosYContraparada],[EnActivo],[MedidoEnPVYCR]" & _
                                      " ,[TitularContactado],[ContadorOK],[INSCRIPCION],[INSCRIPCION_ESTADO],[INSCRIPCION_RELACIONADA]) " & _
                                      " VALUES (@CodigoCauce" & _
                                      " ,@CodigoInventario90,@CodigoCampo,@TipoCauce,@MargenDerivacion,@DenominacionCauce " & _
                                      " ,@OtrasReferencias,@ParajeToma,@MunicipioToma,@ProvinciaToma,@DatosAdministrador,@NombreTitular" & _
                                      " ,@NIFTitular,@DireccionTitular,@MunicipioTitular,@CodPostalTitular,@ProvinciaTitular,@TelefonoTitular" & _
                                      " ,@ReferenciaExpedienteLibroAprovechamiento,@NumeroRegistroAntiguo,@ReferenciaExpedientesRegistroAguas" & _
                                      " ,@OtrosExpedientes,@Seccion,@Tomo,@Hoja,@NombreContactoCampo,@DireccionContactoCampo,@TelefonoContactoCampo" & _
                                      " ,@CaudalMaximo_LSeg,@VolumenMaximoAnualLegal_M3,@VolumenMaximoAnualTeorico_M3,@X_UTM_Toma" & _
                                      " ,@Y_UTM_Toma,@CotaToma,@SuperficieRealAproximada_HAS,@SuperficieInscrita_HAS,@PorcentajeTradicional" & _
                                      " ,@TipoCultivo,@LongitudCauce_KM,@Observaciones,@EntreOjosYContraparada,@EnActivo,@MedidoEnPVYCR" & _
                                      " ,@TitularContactado,@ContadorOK,@Inscripcion,@Inscripcion_estado,@Inscripcion_relacionada) "

                'EGB 22/07/2008 - Construccion de las claves de cauce  Se concatena el prefijo del Cauce Padre en la construcción de la clave del Codigo de Cauce

                If Me.txtcodigoCauceNuevo.Text = "" Then
                    clavenuevonodoPVYCRArbol = Left(utiles.BlancoANull(Me.txtcodigoCauce.Text), 20)
                Else
                    'comando.Parameters.AddWithValue("CodigoCauce", Left(utiles.BlancoANull(Me.txtcodigoCaucePadreNuevo.Text) & "-" & utiles.BlancoANull(Me.txtcodigoCauce.Text), 20))
                    'comando.Parameters.AddWithValue("CodigoCauce", Left(utiles.BlancoANull(Me.txtcodigoCaucePadreNuevo.Text) & utiles.BlancoANull(Me.txtcodigoCauce.Text), 20))

                    'EGB 27/11/2008 Obtención del nivel para determinar el texto del cauce
          IdArbolRaiz = GetIdArbol(txtcodigoCaucePadreNuevo.Text, objTrans)
          NivelIdArbolRaiz = ObtenerNivelNodo(IdArbolRaiz, Page, "C")
                    If NivelIdArbolRaiz > 2 Then
                        clavenuevonodoPVYCRArbol = Me.txtcodigoCaucePadreNuevo.Text & "-" & Me.txtcodigoCauce.Text
                    Else
                        clavenuevonodoPVYCRArbol = Me.txtcodigoCaucePadreNuevo.Text & Me.txtcodigoCauce.Text
                    End If
                    '------------------------------------------------------------------
        End If
        'ncm 27/04/2010
        columnaY = GetY(IdArbolRaiz, NivelIdArbolRaiz, "C", Page)
                comando.Parameters.AddWithValue("CodigoCauce", Left(clavenuevonodoPVYCRArbol, 20))

            Else
                comando.CommandText = "UPDATE [PVYCR_Cauces] SET  " & _
                                      " CodigoInventario90=@CodigoInventario90,CodigoCampo=@CodigoCampo,TipoCauce=@TipoCauce,MargenDerivacion=@MargenDerivacion,DenominacionCauce=@DenominacionCauce " & _
                                      " ,OtrasReferencias=@OtrasReferencias,ParajeToma=@ParajeToma,MunicipioToma=@MunicipioToma,ProvinciaToma=@ProvinciaToma,DatosAdministrador=@DatosAdministrador,NombreTitular=@NombreTitular" & _
                                      " ,NIFTitular=@NIFTitular,DireccionTitular=@DireccionTitular,MunicipioTitular=@MunicipioTitular,CodPostalTitular=@CodPostalTitular,ProvinciaTitular=@ProvinciaTitular,TelefonoTitular=@TelefonoTitular" & _
                                      " ,ReferenciaExpedienteLibroAprovechamiento=@ReferenciaExpedienteLibroAprovechamiento,NumeroRegistroAntiguo=@NumeroRegistroAntiguo,ReferenciaExpedientesRegistroAguas=@ReferenciaExpedientesRegistroAguas" & _
                                      " ,OtrosExpedientes=@OtrosExpedientes,Seccion=@Seccion,Tomo=@Tomo,Hoja=@Hoja,NombreContactoCampo=@NombreContactoCampo,DireccionContactoCampo=@DireccionContactoCampo,TelefonoContactoCampo=@TelefonoContactoCampo" & _
                                      " ,CaudalMaximo_LSeg=@CaudalMaximo_LSeg,VolumenMaximoAnualLegal_M3=@VolumenMaximoAnualLegal_M3,VolumenMaximoAnualTeorico_M3=@VolumenMaximoAnualTeorico_M3,X_UTM_Toma=@X_UTM_Toma" & _
                                      " ,Y_UTM_Toma=@Y_UTM_Toma,CotaToma=@CotaToma,SuperficieRealAproximada_HAS=@SuperficieRealAproximada_HAS,SuperficieInscrita_HAS=@SuperficieInscrita_HAS,PorcentajeTradicional=@PorcentajeTradicional" & _
                                      " ,TipoCultivo=@TipoCultivo,LongitudCauce_KM=@LongitudCauce_KM,Observaciones=@Observaciones,EntreOjosYContraparada=@EntreOjosYContraparada,EnActivo=@EnActivo,MedidoEnPVYCR=@MedidoEnPVYCR" & _
                                      " ,TitularContactado=@TitularContactado,ContadorOK=@ContadorOK,INSCRIPCION=@Inscripcion,INSCRIPCION_ESTADO=@Inscripcion_Estado,INSCRIPCION_RELACIONADA=@Inscripcion_Relacionada" & _
                                      " where CodigoCauce='" & Me.lblCauceSel.Text & "' "


                'EGB 22/07/2008 -  Se reemplaza el prefijo del Cauce Padre en la construcción de la clave del Codigo de Cauce
                'If Me.txtcodigoCauceNuevo.Text = "" Then
                '    comando.Parameters.AddWithValue("CodigoCauce", utiles.BlancoANull(Me.txtcodigoCauce.Text))
                'Else
                '    'Reemplazar
                '    comando.Parameters.AddWithValue("CodigoCauce", utiles.BlancoANull(Me.txtcodigoCauce.Text) & "-" & utiles.BlancoANull(Me.txtcodigoCauce.Text))
                'End If
                comando.Parameters.AddWithValue("CodigoCauce", Left(utiles.BlancoANull(Me.txtcodigoCauce.Text), 20))
            End If

            'EGB 22/07/2008 Originalmente no existe distincion en funcion del alta o de la edicion
            'comando.Parameters.AddWithValue("CodigoCauce", utiles.BlancoANull(Me.txtcodigoCauce.Text))
            comando.Parameters.AddWithValue("DenominacionCauce", utiles.BlancoANull(Me.txtdenominacion.Text))
            comando.Parameters.AddWithValue("CodigoInventario90", utiles.BlancoANull(Me.txtcodinventario.Text))

            comando.Parameters.AddWithValue("CodigoCampo", utiles.BlancoANull(Me.txtcodcampo.Text))
            comando.Parameters.AddWithValue("TipoCauce", utiles.BlancoANull(Me.txtTipoToma.Text))
            comando.Parameters.AddWithValue("MargenDerivacion", utiles.BlancoANull(Me.txtMargenDeriv.Text))
            comando.Parameters.AddWithValue("OtrasReferencias", utiles.BlancoANull(Me.txtOtrasRef.Text))
            comando.Parameters.AddWithValue("ParajeToma", utiles.BlancoANull(Replace(Me.txtParaje.Text, ",", ".")))

            comando.Parameters.AddWithValue("MunicipioToma", utiles.BlancoANull(Replace(Me.txtMunicipio.Text, ",", ".")))
            comando.Parameters.AddWithValue("ProvinciaToma", utiles.BlancoANull(Replace(Me.txtProvincia.Text, ",", ".")))
            comando.Parameters.AddWithValue("DatosAdministrador", utiles.BlancoANull(Replace(Me.txtAdministrador.Text, ",", ".")))
            comando.Parameters.AddWithValue("NombreTitular", utiles.BlancoANull(Replace(Me.txtTitular.Text, ",", ".")))
            comando.Parameters.AddWithValue("NIFTitular", utiles.BlancoANull(Me.txtNIF.Text))
            comando.Parameters.AddWithValue("DireccionTitular", utiles.BlancoANull(Replace(Me.txtDireccion.Text, ",", ".")))

            comando.Parameters.AddWithValue("MunicipioTitular", utiles.BlancoANull(Replace(Me.txtMunicipioTit.Text, ",", ".")))
            comando.Parameters.AddWithValue("CodPostalTitular", utiles.BlancoANull(Replace(Me.txtCP.Text, ",", ".")))
            comando.Parameters.AddWithValue("ProvinciaTitular", utiles.BlancoANull(Me.txtProvinciaTit.Text))

            comando.Parameters.AddWithValue("TelefonoTitular", utiles.BlancoANull(Me.txtTelefono.Text))
            comando.Parameters.AddWithValue("ReferenciaExpedienteLibroAprovechamiento", utiles.BlancoANull(Me.txtExpediente.Text))
            comando.Parameters.AddWithValue("NumeroRegistroAntiguo", utiles.BlancoANull(Replace(Me.txtRegAntiguo.Text, ",", ".")))
            comando.Parameters.AddWithValue("ReferenciaExpedientesRegistroAguas", utiles.BlancoANull(Me.txtExptesLibro.Text))
            comando.Parameters.AddWithValue("OtrosExpedientes", utiles.BlancoANull(Me.txtOtrosExpedientes.Text))
            comando.Parameters.AddWithValue("Seccion", utiles.BlancoANull(Me.txtSeccion.Text))

            comando.Parameters.AddWithValue("Tomo", utiles.BlancoANull(Replace(Me.txtTomo.Text, ",", ".")))
            comando.Parameters.AddWithValue("Hoja", utiles.BlancoANull(Replace(Me.txtFolio.Text, ",", ".")))
            comando.Parameters.AddWithValue("NombreContactoCampo", utiles.BlancoANull(Replace(Me.txtContacto.Text, ",", ".")))
            comando.Parameters.AddWithValue("DireccionContactoCampo", utiles.BlancoANull(Replace(Me.txtDireccionCont.Text, ",", ".")))
            comando.Parameters.AddWithValue("TelefonoContactoCampo", utiles.BlancoANull(Replace(Me.txtTelContacto.Text, ",", ".")))

            comando.Parameters.AddWithValue("CaudalMaximo_LSeg", utiles.BlancoANull(Replace(Me.txtCaudal.Text, ",", ".")))
            comando.Parameters.AddWithValue("VolumenMaximoAnualLegal_M3", utiles.BlancoANull(Replace(Me.txtVolumenLegal.Text, ",", ".")))
            comando.Parameters.AddWithValue("VolumenMaximoAnualTeorico_M3", utiles.BlancoANull(Replace(Me.txtVolumenAnual.Text, ",", ".")))
            comando.Parameters.AddWithValue("X_UTM_Toma", utiles.BlancoANull(Replace(Me.txtX.Text, ",", ".")))

            comando.Parameters.AddWithValue("Y_UTM_Toma", utiles.BlancoANull(Replace(Me.txtY.Text, ",", ".")))
            comando.Parameters.AddWithValue("CotaToma", utiles.BlancoANull(Replace(Me.txtCota.Text, ",", ".")))
            comando.Parameters.AddWithValue("SuperficieRealAproximada_HAS", utiles.BlancoANull(Replace(Me.txtSupReal.Text, ",", ".")))
            comando.Parameters.AddWithValue("SuperficieInscrita_HAS", utiles.BlancoANull(Replace(Me.txtSupInscrita.Text, ",", ".")))

            comando.Parameters.AddWithValue("PorcentajeTradicional", utiles.BlancoANull(Replace(Me.txtTradicional.Text, ",", ".")))
            comando.Parameters.AddWithValue("TipoCultivo", utiles.BlancoANull(Replace(Me.txtTipoCultivo.Text, ",", ".")))
            comando.Parameters.AddWithValue("LongitudCauce_KM", utiles.BlancoANull(Replace(Me.txtLongitudCauce.Text, ",", ".")))
            comando.Parameters.AddWithValue("Observaciones", utiles.BlancoANull(Replace(Me.txtObserva.Text, ",", ".")))

            If chkEntreOjos.Checked Then
                comando.Parameters.AddWithValue("EntreOjosYContraparada", "1")
            Else
                comando.Parameters.AddWithValue("EntreOjosYContraparada", "0")
            End If


            If chkenActivo.Checked Then
                comando.Parameters.AddWithValue("EnActivo", "1")
            Else
                comando.Parameters.AddWithValue("EnActivo", "0")
            End If

            If chkMedido.Checked Then
                comando.Parameters.AddWithValue("MedidoEnPVYCR", "1")
            Else
                comando.Parameters.AddWithValue("MedidoEnPVYCR", "0")
            End If

            If chkTitular.Checked Then
                comando.Parameters.AddWithValue("TitularContactado", "1")
            Else
                comando.Parameters.AddWithValue("TitularContactado", "0")
            End If

            If chkContadorOK.Checked Then
                comando.Parameters.AddWithValue("ContadorOK", "1")
            Else
                comando.Parameters.AddWithValue("ContadorOK", "0")
            End If

            comando.Parameters.AddWithValue("INSCRIPCION", utiles.BlancoANull(Replace(Me.txtInscripcion.Text, ",", ".")))
            comando.Parameters.AddWithValue("INSCRIPCION_ESTADO", cmbInscripcionEstado.SelectedValue)
            comando.Parameters.AddWithValue("INSCRIPCION_RELACIONADA", utiles.BlancoANull(Replace(Me.txtInscripcionRelacionada.Text, ",", ".")))

            comando.ExecuteNonQuery()

            'EGB 26/11/2008 Revisión de Codigo de Inserción en el Arbol.
            '-------------------------------------------------------------
            If lblCauceSel.Text = "" Then
                'NUEVO CAUCE -->INSERTAMOS NODO FICTICIO EN PVYCR_ARBOL

                'EGB 26/11/2008 Ahora se calcula en la seccion anterior
                ''EGB 12/11/2008 Verificación de la asignación a nodo padre
                'Dim IdArbolRaiz As Integer
                'IdArbolRaiz = GetIdArbol(txtcodigoCaucePadreNuevo.Text, objTrans)
                ''EGB 27/11/2008 Obtención del nivel para determinar el texto del cauce
                'If GetNivelIdArbol(IdArbolRaiz, objTrans) > 2 Then
                '    clavenuevonodoPVYCRArbol = Me.txtcodigoCaucePadreNuevo.Text & "-" & Me.txtcodigoCauce.Text
                'Else
                '    clavenuevonodoPVYCRArbol = Me.txtcodigoCaucePadreNuevo.Text & Me.txtcodigoCauce.Text
                'End If
        '------------------------------------------------------------------
        'select count(y) from pvycr_arbol where idarbolpadre = 970755 and tipo = 'C' 
                If IdArbolRaiz > 0 Then

                    'NCM Si el strnodoraiz es nulo, es porque estamos añadiendo un cauce después del raíz y en ese caso el strNodoRaiz valdrá lo que el campo cauce
                    If utiles.nullABlanco(midstCauces.Tables("TablaArbolAuxiliar").Rows(0).Item("strNodoRaiz")) = "" Then
                        strNodoRaiz = Me.txtcodigoCaucePadreNuevo.Text
                    Else
                        strNodoRaiz = midstCauces.Tables("TablaArbolAuxiliar").Rows(0).Item("strNodoRaiz").ToString
                    End If
                    '''Desahemos el cambio de OT1 por OT
                    If (strNodoRaiz = "OT") Then
                        strNodoRaiz = "OT1"
                    End If
                    comando.CommandText = "INSERT INTO PVYCR_Arbol ([IdArbolPadre],[strnodo],[descripcion],[tipo],[clavenodo],[CodigoCauce],x,y,estelemedida,strNodoPadre)VALUES ('" & _
                    IdArbolRaiz & "','" & "<font color=red>" & clavenuevonodoPVYCRArbol & " " & Me.txtdenominacion.Text & "','" & clavenuevonodoPVYCRArbol & " " & Me.txtdenominacion.Text & "','C','" & clavenuevonodoPVYCRArbol & "#C','" & clavenuevonodoPVYCRArbol & "'," & NivelIdArbolRaiz & ", " & columnaY & ", 'N','" & strNodoRaiz & "' )"
                    comando.ExecuteNonQuery()
                    'Actualizar el Arbol Temporal después de la inserción
                    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "P")
                Else
                    Alert(Page, "No se localiza el código del cauce padre, la operación se cancelará.")
                    objTrans.Rollback()
                    Exit Sub 'Se lanza alerta que general la funcion getidarbol y se aborta el procedimiento para evitar recarga.
                End If
            Else
                'EDICION CAUCE
                comando.CommandText = "UPDATE [PVYCR_ARBOL] SET  " & _
                                    "strnodo='" & "<font color=red>" & clavenuevonodoPVYCRArbol & " " & Me.txtdenominacion.Text & "', " & _
                                    "descripcion='" & Me.txtcodigoCauce.Text & " " & Me.txtdenominacion.Text & "' " & _
                                    " where CodigoCauce='" & Me.lblCauceSel.Text & "' AND TIPO='C' "

                comando.ExecuteNonQuery()

            End If


            'EGB 21/07/2008 Actualizacion de variable de Aplicacion
            Application("PVYCR_Arbol_Updated") = "NOK"
            objTrans.Commit()

        Catch Exc As System.Data.SqlClient.SqlException
            Select Case Exc.Number
                'Case 547
                '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)

                Case 2627
                    Alert(Page, "El Cauce ya existe ")
                Case Else
                    Alert(Page, Exc.Message)
            End Select
            objTrans.Rollback()
        End Try


        Me.pnlCauces.Visible = True
        Me.pnlEDCauces.Visible = False
        'Me.ucPaginacion.Visible = True
        'ucPaginacion.lblPaginatext = "1"
        'txtFiltroCodigoCauce.Text = "[Código Cauce]"
        crearDataSets()
        'RDF 20081003
        rellenarListas()

        'ucPaginacion.lblNumpaginasDatabind()

    End Sub




    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.pnlCauces.Visible = True
        Me.pnlEDCauces.Visible = False
        'Me.ucPaginacion.Visible = True
        'ucPaginacion.lblPaginatext = "1"
        'txtFiltroCodigoCauce.Text = "[Código Cauce]"
        crearDataSets()
        'ucPaginacion.lblNumpaginasDatabind()
        'EGB 13/11/2008 Al entrar en una edición después de cancelar la creación de un nuevo cauce no se actualiza la variable de session
        Me.lblTitulo.Text = "MANTENIMIENTO CAUCE:"
    End Sub

    Protected Sub rptCauces_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptCauces.ItemCommand
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim parametros() As String
        Dim objTrans As SqlTransaction

        parametros = Split(e.CommandArgument, "#")
        Select Case e.CommandName
            Case "editar"
                Session("nuevoCauce") = 0
                Me.pnlEDCauces.Visible = True
                Me.pnlCauces.Visible = False

                btnAceptar.Visible = True
                'Me.ucPaginacion.Visible = False

                'Sólo se carga el árbol al modificar un cauce
                'If treeView1.Nodes.Count = 0 Then
                If Request.QueryString("CodigoCauce") <> "" Or Request.QueryString("nuevo") = "yes" Or (treeView1.Nodes.Count = 0) Then
                    crearArbolRecursivoEnMenu(treeView1, GetDataSetArbol(), "P") 'EGB el nuevo parametro P hace referencia al Tipo de Elemento que NO se cargará dentro del arbol de cauces auxiliar
                End If
                'imgArbol.Attributes.Clear()
                imgArbol.Attributes.Add("onclick", "jscript:alert('No es posible reasignar el cauce padre del cauce actual.');")

                lblCauceSel.Text = e.CommandArgument
                crearDataSetCauce(lblCauceSel.Text)
                cargarControlesEdicion() 'EGB Jun-08: Ver nota en definicion de la funcion 
                rptCauces.DataSource = dstCauces.Tables("TablaCauces")
                rptCauces.DataBind()


            Case "borrar"
                'If conexion.State = ConnectionState.Closed Then conexion.Open()
                utiles.Comprobar_Conexion_BD(Page, conexion)
                objTrans = conexion.BeginTransaction()

                Try
                    comando.Transaction = objTrans
                    'En primer lugar, se eliminan las relaciones que tiene la motobomba
                    'comando.CommandText = "delete from PVYCR_ElementosMedida_MotoBombas where CodigoMotobomba='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
                    comando.ExecuteNonQuery()
                    'Se elimina la motobomba correspondiente
                    'comando.CommandText = "delete from PVYCR_MotoBombas where CodigoMotobomba='" & parametros(0) & "' and FechaRevision='" & parametros(1) & "' "
                    comando.ExecuteNonQuery()

                    objTrans.Commit()

                Catch borradoNOK As System.Data.SqlClient.SqlException
                    Alert(Page, "No se puede eliminar el cauce")
                    objTrans.Rollback()
                End Try
                crearDataSets()
                'ucPaginacion.lblNumpaginasDatabind()


        End Select
    End Sub
    Protected Sub cargarControlesEdicion()
        'EGB Jun-08: Esta parte se excinde de la funcion rptCauces_ItemCommand para reutilizacion en asociacion de mantenimientos
        If dstCauces.Tables("TablaCauces").Rows.Count > 0 Then
            'Datos del Cauce
            Me.txtcodigoCauce.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("CodigoCauce"))
            Me.txtdenominacion.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("DenominacionCauce"))
            Me.txtcodinventario.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("CodigoInventario90"))
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

            If (dstCauces.Tables("TablaCauces").Rows(0).Item("EntreOjosYContraparada") = "1") Then
                Me.chkEntreOjos.Checked = True
            Else
                Me.chkEntreOjos.Checked = False
            End If

            If (dstCauces.Tables("TablaCauces").Rows(0).Item("EnActivo") = "1") Then
                Me.chkenActivo.Checked = True
            Else
                Me.chkenActivo.Checked = False
            End If

            If (dstCauces.Tables("TablaCauces").Rows(0).Item("MedidoEnPVYCR") = "1") Then
                Me.chkMedido.Checked = True
            Else
                Me.chkMedido.Checked = False
            End If
            If (dstCauces.Tables("TablaCauces").Rows(0).Item("TitularContactado") = "1") Then
                Me.chkTitular.Checked = True
            Else
                Me.chkTitular.Checked = False
            End If

            If (dstCauces.Tables("TablaCauces").Rows(0).Item("ContadorOK") = "1") Then
                Me.chkContadorOK.Checked = True
            Else
                Me.chkContadorOK.Checked = False
            End If
            Me.txtInscripcion.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("INSCRIPCION"))
            Me.cmbInscripcionEstado.SelectedValue = utiles.nullACero(dstCauces.Tables("TablaCauces").Rows(0).Item("INSCRIPCION_ESTADO"))
            Me.txtInscripcionRelacionada.Text = utiles.nullABlanco(dstCauces.Tables("TablaCauces").Rows(0).Item("INSCRIPCION_RELACIONADA"))

            'EGB 22/07/2008
            'Recuperación del Información del Nodo Padre en caso de existir
            'Equivalente a Activación de SelectedNodeText
            Me.lblDesCodigoCaucePadre.Text = utiles.nullABlanco(dstCauces.Tables("TablaArbolAuxiliar").Rows(0).Item("StrNodoPadre"))
            Me.txtCodigoCaucePadre.Text = utiles.nullABlanco(dstCauces.Tables("TablaArbolAuxiliar").Rows(0).Item("CodigoCaucePadre"))

            'En edición estos controles adquieren los valores 
            Me.txtcodigoCauceNuevo.Text = Me.txtcodigoCauce.Text 'utiles.nullABlanco(dstCauces.Tables("TablaArbolAuxiliar").Rows(0).Item("CodigoCauce"))
            Me.txtcodigoCaucePadreNuevo.Text = Me.txtCodigoCaucePadre.Text 'utiles.nullABlanco(dstCauces.Tables("TablaArbolAuxiliar").Rows(0).Item("CodigoCaucePadre"))
            '-------------------------------------------------------------------------------------------------
            'Fijacion de la selección en el nodo correcto. Usamos las funciones de localizacion de nodos de 
            'la clase SICA_GestionArboles

            Dim rutanodobuscadodescripcion As String
            Dim rutanodobuscado = BuscarNodoTVW(treeView1, Me.lblDesCodigoCaucePadre.Text, rutanodobuscadodescripcion)

            If rutanodobuscado = "" Then

            Else
                'Representación del Nodo si la busqueda es positiva
                Dim nodobuscado As TreeNode = treeView1.FindNode(rutanodobuscado)
                treeView1.CollapseAll()
                ExpandirNodoRecursivo(nodobuscado)
                nodobuscado.Selected = True
                nodobuscado.Select()
            End If
            '-------------------------------------------------------------------------------------------------
            'EGB 27/11/2008 Deshabilitar la modificación del campo clave en edición.
            If Me.lblCauceSel.Text = "" Then
                'NUEVO
                Me.txtcodigoCauce.Enabled = True
            Else
                'EDICION
                Me.txtcodigoCauce.Enabled = False
            End If
        End If
    End Sub
    Protected Function VisibleSegunPerfil() As Boolean
        'NCM 20080616
        If Session("idperfil") = 1 Or Session("idPerfil") = 11 Or Session("idPerfil") = 14 Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Function GetNombreCauceNuevo() As String

        GetNombreCauceNuevo = Me.txtcodigoCauceNuevo.Text

    End Function

    'Protected Sub rptPuntos_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptPuntos.ItemCreated
    '    'Sólo se dispara una vez, cuando pasa por el repeater.
    '    ' solo funciona si los items son los del cuerpo, sino da error, por eso ponemos el IF
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        ' rellenamos las listas de incidencias eléctricas y las volumétricas
    '        Formateo_controles_cliente(e)
    '    End If
    'End Sub

    Protected Sub treeView1_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeView1.SelectedNodeChanged
        Dim TipoNodo As String
        Dim v() As String
        v = Split(treeView1.SelectedNode.Value, "#")
        TipoNodo = v(1)
        If TipoNodo = "C" Or TipoNodo = "R" Then
            'lblPrefijoCaucePadre.Text = v(0)
            lblDesCodigoCaucePadre.Text = "Cauce Padre : " & treeView1.SelectedNode.Text
            txtcodigoCaucePadreNuevo.Text = v(0).ToString()

            '''Al dar de alta cauces OT la Raiz en el arbol es OT1 por lo que
            ''' le quito el 1 para que no de error
            ''' JACN 
            ''' 

            If (txtcodigoCaucePadreNuevo.Text = "OT1") Then
                txtcodigoCaucePadreNuevo.Text = "OT"
            End If

            ''' FIN
            ''' 



            If Request.QueryString("CodigoCauce") <> "" Then
                'edicion
                txtcodigoCauceNuevo.Text = Replace(txtcodigoCauce.Text, txtCodigoCaucePadre.Text, txtcodigoCaucePadreNuevo.Text)
            End If

            If Request.QueryString("nuevo") = "yes" Then
                'nuevos cauces
                'txtcodigoCauceNuevo.Text = txtcodigoCaucePadreNuevo.Text & "-" & txtcodigoCauce.Text
                txtcodigoCauceNuevo.Text = txtcodigoCaucePadreNuevo.Text & txtcodigoCauce.Text
            End If

            'TRATAMIENTO PARA EDICION DE CAUCES

            If Request.QueryString("CodigoCauce") <> "" Then
                Dim DenominacionExistente = ExisteCauce(txtcodigoCauceNuevo.Text)
                'Verificación de la existencia de registro coincidente según la codificación resultante
                If DenominacionExistente.ToString.Length > 0 Then
                    Alert(Page, "No es posible establecer el cauce padre seleccionado \n porque generaría una código de cauce duplicado \n para la denominación" & DenominacionExistente)
                End If
            End If

        Else
            Alert(Page, "Seleccione un cauce al que asociar el punto.")
        End If
    End Sub


    Protected Sub AceptarFiltro(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081002
        Dim strFiltro As String
        strFiltro = ""
        If txtFCodigoCauce.Text <> "" Then
            strFiltro = " AND CodigoCauce like '" + txtFCodigoCauce.Text + "' "
        End If

        'If ddlTipoCauce.Text <> "" Then
        '    strFiltro = " AND TipoCauce='" + ddlTipoCauce.Text + "' "
        'End If

        If txtFDenominacionCauce.Text <> "" Then
            strFiltro = strFiltro + " AND DenominacionCauce like '" + txtFDenominacionCauce.Text + "' "
        End If
        'ncm 20110224 comentado porque cambiamos la forma de filtrar
        'If txtFParaje.Text <> "" Then
        '    strFiltro = strFiltro + " AND ParajeToma like '" + txtFParaje.Text + "' "
        'End If

        'If txtFMunicipio.Text <> "" Then
        '    strFiltro = strFiltro + " AND MunicipioToma like '" + txtFMunicipio.Text + "' "
        'End If

        'If txtFProvincia.Text <> "" Then
        '    strFiltro = strFiltro + " AND ProvinciaToma like '" + txtFProvincia.Text + "' "
        'End If
        If txtFiltrado.Text <> "" Then
            strFiltro = strFiltro + " AND " + ddlFiltrado.SelectedValue + " LIKE '" + txtFiltrado.Text + "'"
        End If
        Session("FiltroCauce") = strFiltro
        crearDataSets()
    End Sub

    Protected Sub rellenarListas()
        'Dim dstTipoCauce As New DataSet
        Dim dstFiltrado As New DataSet
        Try

            'Dim sql As String
            'ncm 20110224 se comenta porque se cambia el filtrado de la pantalla
            'RDF 20081003
            'Se obtienen los distintos tipos de cauces que hay
            'utiles.Comprobar_Conexion_BD(Page, conexion)
            'sql = " SELECT TipoCauce  FROM PVYCR_Cauces  WHERE Not TipoCauce Is NULL GROUP BY TipoCauce ORDER BY TipoCauce "

            'Dim daCauces As New SqlDataAdapter(sql, conexion)
            'RDF 20081003
            'Se obtienen los distintos tipos de cauces que hay
            'daCauces.Fill(dstTipoCauce, "TablaTipoCauce")
            'ddlTipoCauce.DataSource = dstTipoCauce.Tables("TablaTipoCauce")
            'ddlTipoCauce.DataValueField = "TipoCauce"
            'ddlTipoCauce.DataBind()
            'ddlTipoCauce.Items.Insert(0, New ListItem("[Seleccionar]", ""))

            'ncm rellenamos el campo filtrado con las columnas de la tabla de cauces
            utiles.Comprobar_Conexion_BD(Page, conexion)
            daCauces.SelectCommand.CommandText = " SELECT Column_Name as columna  FROM INFORMATION_SCHEMA. COLUMNS where Table_Name = 'PVYCR_CAUCES' and COLUMN_NAME NOT IN ('CODIGOCAUCE','DENOMINACIONCAUCE') "
            daCauces.Fill(dstFiltrado, "TablaFiltrado")
            ddlFiltrado.DataSource = dstFiltrado.Tables("TablaFiltrado")
            ddlFiltrado.DataValueField = "columna"
            ddlFiltrado.DataBind()
            ddlFiltrado.Items.Insert(0, New ListItem("[Seleccionar]", ""))

        Catch ex As Exception

        End Try



    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim nombreinforme = "../listados/InformeCauces.aspx?nodoClave=" + utiles.BlancoANull(Me.txtcodigoCauce.Text)
        ClientScript.RegisterStartupScript(Page.GetType, "abreInforme", "<script language=javascript>window.open('" + nombreinforme + "')</script>")
    End Sub

    Protected Sub btnVertodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVertodos.Click
        'RDF 
        '29/10/2008

        crearDataSets()
    End Sub
    Protected Function ObtenerFiltrado(ByVal eldataitem As DataRowView) As String
        If utiles.nullABlanco(txtFiltrado.Text) <> "" And utiles.nullABlanco(ddlFiltrado.SelectedValue) <> "" Then
            Return eldataitem("" + ddlFiltrado.SelectedValue + "")
        End If
    End Function

    Protected Sub btnListFiltro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListFiltro.Click
        Dim ventanaChecks = "SelecCamposFiltradosMto.aspx?mto=cauces"

        If utiles.nullABlanco(txtNumRegFiltrados.Text) <> "" Or utiles.nullABlanco(txtNumRegFiltrados.Text) = "0" Then
            ClientScript.RegisterStartupScript(Page.GetType, "abreventanaChecks", "<script language=javascript>window.open('" + ventanaChecks + "','','toolbar=no,resizable=no, scrollbar=no, status=no')</script>")
        Else
            JavaScript.Alert(Page, "No hay elementos filtrados")
        End If

    End Sub
End Class
