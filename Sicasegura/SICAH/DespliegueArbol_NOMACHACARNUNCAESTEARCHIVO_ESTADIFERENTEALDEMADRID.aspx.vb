Imports System.IO
Imports GuarderiaFluvial
Partial Class SICAH_DespliegueArbol
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim conexionU As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim dacauces As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim daclasificacion As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionU)
    Dim daRamasVisibles As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionU)
    Dim dstcauces As System.Data.DataSet = New System.Data.DataSet()
    Dim dstRamasVisibles As System.Data.DataSet = New System.Data.DataSet()
    Dim sentencia As String = ""
    'variables para el arbol
    Dim cauceshijos As Integer = 0
    Dim tipo As String = ""
    Dim codigoCauce As String = ""
    Dim descripcion As String = ""
    Dim nivel As String = ""
    Dim nodoclave As String = ""
    Dim hijos As String = ""
    Dim codigoPVYCR As String = ""
    Dim idElementoMedida As String = ""
    Dim tipoPunto As String = ""
    Dim estilo As String = ""
    Dim TieneHijosSiNo As Boolean = False
    Dim esTelemedida As String = ""
    Dim siguientenodo As String = ""
    Dim idNodoActual As Integer = 0

    ' Establecemos el fichero donde residirá la respuesta, es decir que contendrá los hijos de la rama seleccionada por el usuario
    'Dim rutaficherohijos As String = ConfigurationSettings.AppSettings("rutaArbol") & "/arbol" & "-" & DateTime.Now.Ticks.ToString() + ".json"

    'inicializamos el fichero de salida
    'Dim salida As StreamWriter
    Dim tBusqueda As System.Data.DataTable = New System.Data.DataTable()
    Dim fila As System.Data.DataRow


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'la variable nodoBusqueda a s indica que han pulsado el botón de buscar
        Dim nodobusqueda As String = Request("nodobusqueda").ToString
        Dim valor As String = Request("valor").ToString
        Dim node As String = ""
        Dim nodeABuscar = ""

        node = Replace(Request("node"), " ", "")
        nodeABuscar = Request("valor")
        'nodobusqueda = s indica que se está realizando una busqueda en el arbol, sino se carga el árbol normal
        If nodobusqueda = "s" Then
            CargarBusqueda()
        Else
            CargarArbol()
        End If

    End Sub
    Protected Sub CargarArbol()
        Dim node As String = ""
        Dim nodeABuscar = ""
        node = Replace(Request("node"), " ", "")
        'si es la primera vez, cargaremos las ramas según los privilegios que tenga el usuario
        If node = "raiz" Then
            CargarRamasRaices()
        Else
            Dim padreAnterior As Integer = 0
            Dim tipo As String = ObtenerTipo(node)
            'obtenemos el idpadre para saber los hijos
            Dim idPadre As Integer = ObtenerIdNodoPadre(node, tipo)

            'si no es un nodo final
            If idPadre > 0 Then
                'abrimos fichero donde caegaremos los hijos
                'salida = New StreamWriter(rutaficherohijos, False, System.Text.Encoding.UTF8)
                'el tipo de fichero debe de aer un json
                Response.ContentType = "text/json"
                'salida.WriteLine("[")
                Response.Write("[")
                'cargamos los hijos de los nodos padres
                CargarRamasHijos(node, idPadre)
                'salida.WriteLine("]")
                Response.Write("]")
                'salida.Close()
                'cargamos los hijos en el árbol
                'Response.WriteFile(rutaficherohijos)
                Response.Flush()
                'File.Delete(rutaficherohijos)
            End If
        End If
    End Sub
    Protected Sub CargarRamasHijos(ByVal nodo As String, ByVal idarbolPadre As String)
        'con el idpadre obtendremos los hijos
        sentencia = "select distinct idarbol,CODIGOCAUCE,codigoPVYCR, idElementoMedida,DESCRIPCION,tipo,X,claveNodo, esTelemedida from  pvycr_arbol where idarbolpadre = " & idarbolPadre & " order by clavenodo"
        utiles.Comprobar_Conexion_BD(Page, conexion)
        dacauces.SelectCommand.CommandText = sentencia
        dacauces.Fill(dstcauces, "Tablahijos")
        Dim cauceshijos As Integer = dstcauces.Tables("TablaHijos").Rows.Count
        Dim tipo As String = ""
        Dim codigoCauce As String = ""
        Dim descripcion As String = ""
        Dim nivel As String = ""
        Dim nodoclave As String = ""
        Dim hijos As String = ""
        Dim codigoPVYCR As String = ""
        Dim idElementoMedida As String = ""
        Dim tipoPunto As String = ""
        Dim estilo As String = ""
        Dim TieneHijosSiNo As Boolean = False
        'Dim esTelemedida As String = ""
        Dim esfavorito As String = ""
        Dim Caducado As Integer = 0
        Dim tipoIcono As String = ""
        Dim tipoIconoE As String = ""
        Dim tipoElemen As String = ""
        'vamos a recorrer cada hijo y aplicarle los estilos
        For cauceshijos = 0 To cauceshijos - 1
            tipo = dstcauces.Tables("TablaHijos").Rows(cauceshijos).Item("tipo").ToString
            codigoCauce = dstcauces.Tables("TablaHijos").Rows(cauceshijos).Item("codigocauce").ToString
            'algunas descripciones llevan comillas dobles y esto hace que al mostrar la informaciín general del nodo de error, hay que quitárselas
            descripcion = dstcauces.Tables("TablaHijos").Rows(cauceshijos).Item("descripcion").ToString.Replace("""", " ")
            nivel = dstcauces.Tables("TablaHijos").Rows(cauceshijos).Item("X").ToString
            nodoclave = dstcauces.Tables("TablaHijos").Rows(cauceshijos).Item("claveNodo").ToString.Replace("#", ";")
            codigoPVYCR = dstcauces.Tables("TablaHijos").Rows(cauceshijos).Item("codigoPVYCR").ToString
            idElementoMedida = dstcauces.Tables("TablaHijos").Rows(cauceshijos).Item("idElementoMedida").ToString
            TieneHijosSiNo = TieneHijos(dstcauces.Tables("TablaHijos").Rows(cauceshijos).Item("idArbol").ToString)
            'esTelemedida = dstcauces.Tables("TablaHijos").Rows(cauceshijos).Item("esTelemedida").ToString
            hijos &= ",{"
            Select Case tipo
                Case "C"
                    hijos &= "href:'InformacionGeneral.aspx?nodotexto=" & descripcion & "&nodoclave=" & nodoclave & "&x=" & nivel & "',hrefTarget:'iframeDetalle'," & _
                    "id:'" & codigoCauce & "',text:'" & descripcion & "'"
                    'Dependiendo del nivel del cauce le aplicaremos un estilo u otro.
                    Select Case nivel
                        Case "1"
                            estilo = ",cls:'CauceNivel1Tree'"
                        Case "2"
                            estilo = ",cls:'CauceNivel2Tree'"
                        Case "3"
                            estilo = ",cls:'CauceNivel3Tree'"
                    End Select
                    If TieneHijosSiNo = False Then
                        hijos = hijos & ",leaf:'true'"
                        estilo = ",cls:'CaucesSinHijosTree'"
                    End If
                    hijos = hijos & estilo
                Case "P"
                    'vamos a obtener el tipo de punto para saber qué icono debe llevar
                    'ncm lo cambiamo porque nos han cambiado los iconos y ahora se sacan de la tabla PVYCR_clasificacionelemento 08/06/2010
                    tipoPunto = ObtenerTipoPunto(codigoPVYCR)
                    tipoIcono = ObtenerIconoPunto(codigoPVYCR)
                    esfavorito = utiles.nullABlanco(ObtenerFavorito(codigoPVYCR))
                    Caducado = EstaCaducado(codigoPVYCR)
                    hijos &= "href:'InformacionGeneral.aspx?nodotexto=" & descripcion & "&nodoclave=" & nodoclave & "&x=" & nivel & "',hrefTarget:'iframeDetalle'," & _
                    "id:'" & codigoPVYCR & "',text:'" & descripcion & "'"

                    Select Case tipoPunto
                        Case "M"
                            'estilo = ",cls:'PuntoMTree'"
                            Select Case tipoIcono
                                Case "0"
                                    estilo = ",cls:'PuntoCirYTree'"
                                Case "1"
                                    estilo = ",cls:'PuntoCirOTree'"
                                Case "2"
                                    estilo = ",cls:'PuntoCirGTree'"
                                Case "3"
                                    estilo = ",cls:'PuntoCirBTree'"
                            End Select
                        Case "G"
                            'estilo = ",cls:'PuntoGTree'"
                            Select Case tipoIcono
                                Case "0"
                                    estilo = ",cls:'PuntoCuaYTree'"
                                Case "1"
                                    estilo = ",cls:'PuntoCuaOTree'"
                                Case "2"
                                    estilo = ",cls:'PuntoCuaGTree'"
                                Case "3"
                                    estilo = ",cls:'PuntoCuaBTree'"
                            End Select
                        Case "P"
                            estilo = ",cls:'PuntoPTree'"
                        Case "T"
                            estilo = ",cls:'PuntoTTree'"
                    End Select
                    'ncm 08/06/2010 comentado porque ahora si es telemedida nos lo indica ya funcion obtenerIconoPunto
                    ''punto telemedida
                    'If esTelemedida = "S" And tipoPunto = "G" Then
                    '  estilo = ",cls:'PuntoTeleGTree'"
                    'ElseIf esTelemedida = "S" And tipoPunto = "M" Then
                    '  estilo = ",cls:'PuntoTeleMTree'"
                    'End If

                    ' ************************ puntos favoritos ***************************************
                    'If esfavorito.ToUpper = "SI" And esTelemedida = "S" And tipoPunto = "G" Then
                    '  estilo = ",cls:'PuntoFavTeleGTree'"
                    'ElseIf esfavorito.ToUpper = "SI" And tipoPunto = "G" Then
                    '  estilo = ",cls:'PuntoFavGTree'"
                    'ElseIf esfavorito.ToUpper = "SI" And esTelemedida = "S" And tipoPunto = "M" Then
                    '  estilo = ",cls:'PuntoFavTeleMTree'"
                    'ElseIf esfavorito.ToUpper = "SI" And tipoPunto = "M" Then
                    '  estilo = ",cls:'PuntoFavMTree'"
                    'ElseIf esfavorito.ToUpper = "SI" And tipoPunto = "P" Then
                    '  estilo = ",cls:'PuntoFavPTree'"
                    'ElseIf esfavorito.ToUpper = "SI" And tipoPunto = "T" Then
                    '  estilo = ",cls:'PuntoFavTTree'"
                    'End If
                    If esfavorito.ToUpper = "SI" And tipoPunto = "G" Then
                        Select Case tipoIcono
                            Case "0"
                                estilo = ",cls:'PuntoFavCuaYTree'"
                            Case "1"
                                estilo = ",cls:'PuntoFavCuaOTree'"
                            Case "2"
                                estilo = ",cls:'PuntoFavCuaGTree'"
                            Case "3"
                                estilo = ",cls:'PuntoFavCuaBTree'"
                        End Select
                    ElseIf esfavorito.ToUpper = "SI" And tipoPunto = "M" Then
                        Select Case tipoIcono
                            Case "0"
                                estilo = ",cls:'PuntoFavCirYTree'"
                            Case "1"
                                estilo = ",cls:'PuntoFavCirOTree'"
                            Case "2"
                                estilo = ",cls:'PuntoFavCirGTree'"
                            Case "3"
                                estilo = ",cls:'PuntoFavCirBTree'"
                        End Select
                    ElseIf esfavorito.ToUpper = "SI" And tipoPunto = "P" Then
                        estilo = ",cls:'PuntoFavPTree'"
                    ElseIf esfavorito.ToUpper = "SI" And tipoPunto = "T" Then
                        estilo = ",cls:'PuntoFavTTree'"
                    End If
                    ' ****************************** puntos caducados *********************************************
                    'If Caducado > 0 And esfavorito.ToUpper = "SI" And esTelemedida = "S" And tipoPunto = "G" Then
                    '  estilo = ",cls:'PuntoCadFavTeleGTree'"
                    'ElseIf Caducado > 0 And esfavorito.ToUpper = "SI" And esTelemedida = "S" And tipoPunto = "M" Then
                    '  estilo = ",cls:'PuntoCadFavTeleMTree'"
                    'ElseIf Caducado > 0 And esfavorito.ToUpper = "SI" And tipoPunto = "G" Then
                    '  estilo = ",cls:'PuntoCadFavGTree'"
                    'ElseIf Caducado > 0 And esfavorito.ToUpper = "SI" And tipoPunto = "T" Then
                    '  estilo = ",cls:'PuntoCadFavTTree'"
                    'ElseIf Caducado > 0 And esfavorito.ToUpper = "SI" And tipoPunto = "P" Then
                    '  estilo = ",cls:'PuntoCadFavPTree'"
                    'ElseIf Caducado > 0 And esfavorito.ToUpper = "SI" And tipoPunto = "M" Then
                    '  estilo = ",cls:'PuntoCadFavMTree'"
                    'ElseIf Caducado > 0 And esTelemedida = "S" And tipoPunto = "G" Then
                    '  estilo = ",cls:'PuntoCadTeleGTree'"
                    'ElseIf Caducado > 0 And esTelemedida = "S" And tipoPunto = "M" Then
                    '  estilo = ",cls:'PuntoCadTeleMTree'"
                    'ElseIf Caducado > 0 And tipoPunto = "T" Then
                    '  estilo = ",cls:'PuntoCadTTree'"
                    'ElseIf Caducado > 0 And tipoPunto = "P" Then
                    '  estilo = ",cls:'PuntoCadPTree'"
                    'ElseIf Caducado > 0 And tipoPunto = "G" Then
                    '  estilo = ",cls:'PuntoCadGTree'"
                    'ElseIf Caducado > 0 And tipoPunto = "M" Then
                    'estilo = ",cls:'PuntoCadMTree'"         
                    'End If
                    If Caducado > 0 And esfavorito.ToUpper = "SI" And tipoPunto = "G" Then
                        'estilo = ",cls:'PuntoCadFavGTree'"
                        Select Case tipoIcono
                            Case "0"
                                estilo = ",cls:'PuntoCadFavCuaYTree'"
                            Case "1"
                                estilo = ",cls:'PuntoCadFavCuaOTree'"
                            Case "2"
                                estilo = ",cls:'PuntoCadFavCuaGTree'"
                            Case "3"
                                estilo = ",cls:'PuntoCadFavCuaBTree'"
                        End Select
                    ElseIf Caducado > 0 And esfavorito.ToUpper = "SI" And tipoPunto = "T" Then
                        estilo = ",cls:'PuntoCadFavTTree'"
                    ElseIf Caducado > 0 And esfavorito.ToUpper = "SI" And tipoPunto = "P" Then
                        estilo = ",cls:'PuntoCadFavPTree'"
                    ElseIf Caducado > 0 And esfavorito.ToUpper = "SI" And tipoPunto = "M" Then
                        'estilo = ",cls:'PuntoCadFavMTree'"
                        Select Case tipoIcono
                            Case "0"
                                estilo = ",cls:'PuntoCadFavCirYTree'"
                            Case "1"
                                estilo = ",cls:'PuntoCadFavCirOTree'"
                            Case "2"
                                estilo = ",cls:'PuntoCadFavCirGTree'"
                            Case "3"
                                estilo = ",cls:'PuntoCadFavCirBTree'"
                        End Select
                    ElseIf Caducado > 0 And tipoPunto = "T" Then
                        estilo = ",cls:'PuntoCadTTree'"
                    ElseIf Caducado > 0 And tipoPunto = "P" Then
                        estilo = ",cls:'PuntoCadPTree'"
                    ElseIf Caducado > 0 And tipoPunto = "G" Then
                        Select Case tipoIcono
                            Case "0"
                                estilo = ",cls:'PuntoCadCuaYTree'"
                            Case "1"
                                estilo = ",cls:'PuntoCadCuaOTree'"
                            Case "2"
                                estilo = ",cls:'PuntoCadCuaGTree'"
                            Case "3"
                                estilo = ",cls:'PuntoCadCuaBTree'"
                        End Select
                    ElseIf Caducado > 0 And tipoPunto = "M" Then
                        'estilo = ",cls:'PuntoCadMTree'"
                        Select Case tipoIcono
                            Case "0"
                                estilo = ",cls:'PuntoCadCirYTree'"
                            Case "1"
                                estilo = ",cls:'PuntoCadCirOTree'"
                            Case "2"
                                estilo = ",cls:'PuntoCadCirGTree'"
                            Case "3"
                                estilo = ",cls:'PuntoCadCirBTree'"
                        End Select
                    End If

                    If TieneHijosSiNo = False Then
                        hijos = hijos & ",leaf:'true'"
                    End If
                    hijos = hijos & estilo
                Case "E"
                    tipoPunto = ObtenerTipoPunto(codigoPVYCR)

                    tipoIconoE = ObtenerIconoPuntoE(codigoPVYCR, idElementoMedida)

                    hijos &= "href:'InformacionGeneral.aspx?nodotexto=" & descripcion & "&nodoclave=" & nodoclave & "&x=" & nivel & "',hrefTarget:'iframeDetalle'," & _
                    "id:'" & codigoPVYCR & "-" & idElementoMedida & "',text:'" & descripcion & "',leaf:'true'" ',cls:'ElementoMedidaTree'"
                    'https://sica.chsegura.es/sicasegura/SICAH/InformacionGeneral.aspx?nodotexto=DS001P01-Q01%20-%20CAUDAL&nodoclave=DS001P01;E;Q&x=3
                    tipoElemen = idElementoMedida.Substring(0, 1)
                    Select Case tipoPunto
                        Case "G"
                            Select Case tipoElemen
                                Case "Q"
                                    Select Case tipoIconoE
                                        Case "0"
                                            estilo = ",cls:'ElementoCuaQYTree'"
                                        Case "1"
                                            estilo = ",cls:'ElementoCuaQOTree'"
                                        Case "2"
                                            estilo = ",cls:'ElementoCuaQGTree'"
                                        Case "3"
                                            estilo = ",cls:'ElementoCuaQBTree'"
                                    End Select
                                Case "N"
                                    Select Case tipoIconoE
                                        Case "0"
                                            estilo = ",cls:'ElementoCuaNYTree'"
                                        Case "1"
                                            estilo = ",cls:'ElementoCuaNOTree'"
                                        Case "2"
                                            estilo = ",cls:'ElementoCuaNGTree'"
                                        Case "3"
                                            estilo = ",cls:'ElementoCuaNBTree'"
                                    End Select
                            End Select 'tipo elemento
                        Case "M"
                            Select Case tipoElemen
                                Case "Q"
                                    Select Case tipoIconoE
                                        Case "0"
                                            estilo = ",cls:'ElementoCirQYTree'"
                                        Case "1"
                                            estilo = ",cls:'ElementoCirQOTree'"
                                        Case "2"
                                            estilo = ",cls:'ElementoCirQGTree'"
                                        Case "3"
                                            estilo = ",cls:'ElementoCirQBTree'"
                                    End Select
                                Case "V"
                                    Select Case tipoIconoE
                                        Case "0"
                                            estilo = ",cls:'ElementoCirVYTree'"
                                        Case "1"
                                            estilo = ",cls:'ElementoCirVOTree'"
                                        Case "2"
                                            estilo = ",cls:'ElementoCirVGTree'"
                                        Case "3"
                                            estilo = ",cls:'ElementoCirVBTree'"
                                    End Select
                                Case "E"
                                    Select Case tipoIconoE
                                        Case "0"
                                            estilo = ",cls:'ElementoCirEYTree'"
                                        Case "1"
                                            estilo = ",cls:'ElementoCirEOTree'"
                                        Case "2"
                                            estilo = ",cls:'ElementoCirEGTree'"
                                        Case "3"
                                            estilo = ",cls:'ElementoCirEBTree'"
                                    End Select
                                Case "H"
                                    Select Case tipoIconoE
                                        Case "0"
                                            estilo = ",cls:'ElementoCirHYTree'"
                                        Case "1"
                                            estilo = ",cls:'ElementoCirHOTree'"
                                        Case "2"
                                            estilo = ",cls:'ElementoCirHGTree'"
                                        Case "3"
                                            estilo = ",cls:'ElementoCirHBTree'"
                                    End Select
                            End Select 'tipo elemento
                    End Select 'tipo punto
                    hijos &= estilo
            End Select 'tipo "p o e"
            hijos &= "}"
        Next
        If (hijos.Length > 0) Then
            hijos = hijos.Substring(1)
        End If
        'salida.WriteLine(hijos)
        Response.Write(hijos)

    End Sub

    Protected Function ObtenerIdNodoPadre(ByVal nodo As String, ByVal tipo As String) As Integer
        Dim sentenciaTipo As String = ""
        sentencia = "SELECT idarbol FROM PVYCR_ARBOL WHERE "
        'Cargaremos los cauces padre 
        Select Case tipo
            Case "C"

                If nodo = "OT1" Then
                    sentenciaTipo &= " tipo='" & tipo & "' and codigoCauce='OT'and clavenodo like 'OT1#%' order by clavenodo,x,y"
                Else
                    sentenciaTipo &= " tipo='" & tipo & "' and codigoCauce='" & nodo & "' order by codigoCauce,x,y"
                End If

            Case "P"
                sentenciaTipo &= " tipo='" & tipo & "' and codigoPVYCR='" & nodo & "' order by codigoPVYCR,x,y"
            Case "E"
                sentenciaTipo &= " tipo='" & tipo & "' and idElementoMedida='" & nodo & "' order by codigoPVYCR, x, y"
        End Select
        utiles.Comprobar_Conexion_BD(Page, conexion)
        dacauces.SelectCommand.CommandText = sentencia & sentenciaTipo
        Return dacauces.SelectCommand.ExecuteScalar
    End Function
    Protected Function ObtenerTipo(ByVal nodo As String) As String

        Dim sentencia As String = "select tipo from pvycr_arbol where clavenodo like '" & nodo & "#%'"
        Dim tipo As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        dacauces.SelectCommand.CommandText = sentencia
        dacauces.Fill(dstcauces, "TablaTipos")
        Dim NumTipos As Integer = dstcauces.Tables("TablaTipos").Rows.Count
        'si es un cauce únicamente devuelve un registro, sino, ordenamos descendentemente para coger el tipo punto,
        'ya que no se da el caso que un elemento tenga como hijo otro elemento de medida.
        If NumTipos = 1 Then
            tipo = dacauces.SelectCommand.ExecuteScalar
        ElseIf NumTipos > 1 Then
            sentencia = "select tipo from pvycr_arbol where clavenodo like '" & nodo & "#%' order by tipo desc"
            dacauces.SelectCommand.CommandText = sentencia
            tipo = dacauces.SelectCommand.ExecuteScalar
        End If
        Return tipo
    End Function
    Protected Function ObtenerTipoPunto(ByVal codigoPVYCR As String) As String
        'obtenemos el punto si es M o G
        Dim sentenciatipopunto = "select tipoPunto from PVYCR_puntos where codigoPVYCR = '" & codigoPVYCR & "'"
        utiles.Comprobar_Conexion_BD(Page, conexion)
        dacauces.SelectCommand.CommandText = sentenciatipopunto
        Return dacauces.SelectCommand.ExecuteScalar
    End Function
    Protected Function TieneHijos(ByVal idPadre As Integer) As Boolean
        'comprobamos si el noso tiene hijos
        Dim sentencia = "select count(*) from pvycr_arbol where idarbolPadre = " & idPadre
        utiles.Comprobar_Conexion_BD(Page, conexion)
        dacauces.SelectCommand.CommandText = sentencia
        Dim NumHijos As Integer = dacauces.SelectCommand.ExecuteScalar
        If NumHijos = 0 Then
            Return (False)
        Else
            Return True
        End If
    End Function
    Protected Sub CargarBusqueda()
        'obtenemos el id del noso que vamos a buscar
        Dim idNodoBusqueda As Integer
        Dim permisos As Boolean = True
        idNodoBusqueda = ObtenerIdNodoBusqueda()
        'reordenamos el dataset con toda la rama a buscar
        ObtenerRamasBusqueda(idNodoBusqueda)
        'construimos el json con la ruta raiz/nodonivel1/nodonivel2...
        CargarArbolBusqueda()
    End Sub
    Protected Function ObtenerIdNodoBusqueda() As Integer
        Dim valor As String = Request("valor")
        'NCM Todo lo comentado corresponde la la búqueda que hay en el json txtBuscar y que tb hemos comentado, es código que funciona
        'CAUCES, si es un cauce buscamos el id y el idpadre
        'sentencia = "select idArbol,idarbolPadre from pvycr_arbol where codigocauce = '" & valor & "'"
        'utiles.Comprobar_Conexion_BD(Page, conexion)
        'dacauces.SelectCommand.CommandText = sentencia
        'Dim idArbol As Integer
        'idArbol = dacauces.SelectCommand.ExecuteScalar
        ''si no es un cauce, comprobamos con los guiones si se trata de un punto o de un elemento de medida
        'If idArbol = Nothing Then
        '  Dim numGuiones As Integer = Math.Max(Split(valor, "-").Length - 1, 0)
        '  'PUNTOS
        '  If numGuiones = 0 Then
        '    sentencia = "select idArbol,idarbolPadre from pvycr_arbol where codigoPVYCR = '" & valor & "' and tipo = 'P'"
        '  Else
        '    Dim codigoPVYCR As String = valor.Substring(0, valor.LastIndexOf("-"))
        '    Dim cadena() As String = valor.Split("-")
        '    Dim elemento As String = cadena(cadena.LongLength - 1).ToString()
        '    If elemento.Length = 3 Then
        '      'ELEMENTO MEDIDA
        '      sentencia = "select idArbol,idarbolPadre from pvycr_arbol where codigoPVYCR = '" & codigoPVYCR & "' and idElementoMedida = '" & elemento & "' "
        '    Else
        '      'PUNTOS
        '      sentencia = "select idArbol,idarbolPadre from pvycr_arbol where codigoPVYCR = '" & valor & "' and tipo = 'P'"
        '    End If
        '  End If
        '  dacauces.SelectCommand.CommandText = sentencia
        '  idArbol = dacauces.SelectCommand.ExecuteScalar
        'End If
        sentencia = "select idArbol,idarbolPadre from pvycr_arbol where claveNodo='" & valor.Replace(";", "#") & "' "
        utiles.Comprobar_Conexion_BD(Page, conexion)
        dacauces.SelectCommand.CommandText = sentencia
        Dim idArbol As Integer
        idArbol = dacauces.SelectCommand.ExecuteScalar
        Return idArbol
    End Function
    Protected Sub ObtenerRamasBusqueda(ByVal idNodoBusqueda As Integer)
        'obtendremos un dataset con todos los nodos que forman la rama que queremos buscar (desde el nodo raiz hasta el buscado)
        Dim hijo As Boolean = True
        Dim i As Integer = 0
        While hijo
            sentencia = "select idArbol, idarbolPadre, tipo, codigoCauce,codigoPVYCR,idElementoMedida,claveNodo,descripcion, x, esTelemedida  from PVYCR_arbol where idarbol=" & idNodoBusqueda
            utiles.Comprobar_Conexion_BD(Page, conexion)
            dacauces.SelectCommand.CommandText = sentencia
            dacauces.Fill(tBusqueda)
            If tBusqueda.Rows(i).Item("idarbolpadre").ToString = "" Then
                hijo = False
                Exit While
            Else
                idNodoBusqueda = tBusqueda.Rows(i).Item("idarbolpadre")
                i = i + 1
                Continue While
            End If
        End While
    End Sub
    Protected Sub CargarRamasBusqueda()
        'formamos el json con la ruta raiz/nosonivel1/nodonivel2...
        Dim cauceshijos As Integer = tBusqueda.Rows.Count
        Dim tipo As String = ""
        Dim codigoCauce As String = ""
        Dim codigoPVYCR As String = ""
        Dim idElementoMedida As String = ""
        Dim llaves As String = "{path:'"
        Dim cadena As String = ""
        Dim claveNodo As String = ""
        Dim raiz() As String
        If cauceshijos > 0 Then
            claveNodo = tBusqueda.Rows(cauceshijos - 1).Item("claveNodo").ToString
            raiz = claveNodo.Split("#")
        End If
        'vamos a recorrer cada hijo y a formar la ruta raiz/nodo1/nodo2...
        For cauceshijos = cauceshijos - 1 To 0 Step -1

            tipo = tBusqueda.Rows(cauceshijos).Item("tipo").ToString
            codigoCauce = tBusqueda.Rows(cauceshijos).Item("codigocauce").ToString
            codigoPVYCR = tBusqueda.Rows(cauceshijos).Item("codigoPVYCR").ToString
            idElementoMedida = tBusqueda.Rows(cauceshijos).Item("idElementoMedida").ToString
            Select Case tipo
                Case "C"
                    If codigoCauce = "OT" Then

                        codigoCauce = raiz(0)
                    End If
                    cadena &= "/" & codigoCauce

                Case "P"
                    cadena &= "/" & codigoPVYCR
                Case "E"
                    cadena &= "/" & codigoPVYCR & "-" & idElementoMedida
            End Select
        Next
        If cadena.Length > 0 Then
            cadena = cadena.Substring(1)
        End If
        cadena = llaves & cadena & "'}"
        'salida.WriteLine(cadena)
        Response.Write(cadena)
    End Sub
    Protected Sub CargarArbolBusqueda()
        'Dim rutaficheroRamaB As String = ConfigurationSettings.AppSettings("rutaArbol") & "/arbol" & "-" & DateTime.Now.Ticks.ToString() + "RamaB.json"
        'abrimos fichero donde caegaremos los hijos
        'salida = New StreamWriter(rutaficheroRamaB, False, System.Text.Encoding.UTF8)
        Response.ContentType = "text/json"
        'cargamos la ruta de la rama raiz/nodo1/nodo2....
        CargarRamasBusqueda()
        'salida.Close()
        'Response.WriteFile(rutaficheroRamaB)
        Response.Flush()
        'File.Delete(rutaficheroRamaB)
    End Sub
    Protected Sub CargarRamasRaices()
        Dim rutaficheroRamaR As String = ConfigurationSettings.AppSettings("rutaArbol") & "/arbol" & "-" & DateTime.Now.Ticks.ToString() + "RamaR.json"
        'salida = New StreamWriter(rutaficheroRamaR, False, System.Text.Encoding.UTF8)

        'salida.WriteLine("[" & RamasVisiblesString() & "]")

        'salida.Close()
        Response.ContentType = "text/json"
        'cargamos la ruta de las ramas raices ARG/CM/S/OT1....
        Response.Write("[" & RamasVisiblesString() & "]")
        'Response.WriteFile(rutaficheroRamaR)
        Response.Flush()
        'File.Delete(rutaficheroRamaR)
    End Sub
    Protected Sub CargarDataSEtPermisos()
        'NCM rellenamos en un dataset los valores que tiene el usuario para saber qué ramas puede ver

        daRamasVisibles.SelectCommand.CommandText = "SELECT ARG, CM, ET, OT1,S,VA,VB,VM,EA,MU,PL,MO,GU,CHI,CHE,AZ from TUsuarios where login= '" & Session("loginUsuario").ToString & "' "

        utiles.Comprobar_Conexion_BD(Page, conexionU)
        daRamasVisibles.Fill(dstRamasVisibles, "TablaVisibles")
        utiles.CerrarConexion(conexionU)


    End Sub
    Private Function RamasVisiblesString()
        Dim str As String = ""

        CargarDataSEtPermisos()
        ''''
        ''En el caso de ser un usuario de CHS que no está en GeoCampo
        ''''
        If (dstRamasVisibles.Tables("tablaVisibles").Rows.Count = 0) Then
            ''''''
            ''''''
            '''En este caso es cuando NO se encuentra el usuario en GEOCAMPO
            ''''''
            ''''''
            str = str & ",{id: 'ARG',text: 'ARG Argos',cls:'CauceNivel0Tree',visible: false }"
            str = str & ",{id: 'AZ',text: 'AZ Azarbe',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'CHE',text: 'CHE Centrales Hidroeléctricas',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'CHI',text: 'CHI Río Chicamo',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'CM',text:  'CM Cabecera - Río Mundo',cls:  'CauceNivel0Tree'}"
            str = str & ",{id: 'EA',text: 'EA Estaciones de Aforo',cls:'CauceNivel0Tree',visible: false }"
            str = str & ",{id: 'ET',text: 'ET Estación de Tratamiento',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'GU',text: 'GU Río Guadalentín',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'MO',text: 'MO Río Moratalla',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'MU',text: 'MU Río Mula',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'OT1',text: 'OT Pozos de CHS',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'P',text: 'P Peajes',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'PL',text: 'PL Río Pliego',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'S',text: 'S Pozos Subterráneas',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'T',text: 'T Trasvases',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'VA',text: 'VA Vega Alta',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'VB',text: 'VB Vega Baja',cls:'CauceNivel0Tree'}"
            str = str & ",{id: 'VM',text: 'VM Vega Media',cls:'CauceNivel0Tree'} "

        Else
            ''''''
            ''''''
            '''En este caso es cuando se encuentra el usuario en GEOCAMPO
            ''''''
            ''''''
            'Construccion del string de permisos
            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("ARG")) = "S" Then
                str = str & ",{id: 'ARG',text: 'ARG Argos',cls:'CauceNivel0Tree',visible: false }"
            End If

            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("ET")) = "S" Then
                str = str & ",{id: 'ET',text: 'ET Estación de Tratamiento',cls:'CauceNivel0Tree'}"
            End If

            'If dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("OT0") = "S" Then
            '    str = str & ",{id: 'OT0',text: 'OT Residuales EDARS ',cls:'CauceNivel0Tree'}"
            'End If

            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("OT1")) = "S" Then
                str = str & ",{id: 'OT1',text: 'OT Pozos de CHS',cls:'CauceNivel0Tree'}"
            End If

            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("CM")) = "S" Then
                str = str & ",{id:   'CM',text:  'CM Cabecera - Río Mundo',cls:  'CauceNivel0Tree'}"
            End If

            'ncm la rama de peajes y trasvases no se verá, si alguna de las otras ramas está oculta.

            If (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("ARG")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("CM")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("ET")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("OT1")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("S")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("VA")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("VB")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("VM")) = "S") Then
                str = str & ",{id: 'P',text: 'P Peajes',cls:'CauceNivel0Tree'}"
            End If


            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("S")) = "S" Then
                str = str & ",{id: 'S',text: 'S Pozos Subterráneas',cls:'CauceNivel0Tree'}"
            End If
            If (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("ARG")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("CM")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("ET")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("OT1")) = "S") And _
               (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("S")) = "S") And _
              (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("VA")) = "S") And _
              (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("VB")) = "S") And _
              (utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("VM")) = "S") Then
                str = str & ",{id: 'T',text: 'T Trasvases',cls:'CauceNivel0Tree'}"
            End If


            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("VA")) = "S" Then
                str = str & ",{id: 'VA',text: 'VA Vega Alta',cls:'CauceNivel0Tree'}"
            End If

            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("VB")) = "S" Then
                str = str & ",{id: 'VB',text: 'VB Vega Baja',cls:'CauceNivel0Tree'}"
            End If

            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("VM")) = "S" Then
                str = str & ",{id: 'VM',text: 'VM Vega Media',cls:'CauceNivel0Tree'} "
            End If
            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("EA")) = "S" Then
                str = str & ",{id: 'EA',text: 'EA Estaciones de Aforo',cls:'CauceNivel0Tree',visible: false }"
            End If
            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("MU")) = "S" Then
                str = str & ",{id: 'MU',text: 'MU Río Mula',cls:'CauceNivel0Tree'}"
            End If
            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("PL")) = "S" Then
                str = str & ",{id: 'PL',text: 'PL Río Pliego',cls:'CauceNivel0Tree'}"
            End If
            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("MO")) = "S" Then
                str = str & ",{id: 'MO',text: 'MO Río Moratalla',cls:'CauceNivel0Tree'}"
            End If
            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("GU")) = "S" Then
                str = str & ",{id: 'GU',text: 'GU Río Guadalentín',cls:'CauceNivel0Tree'}"
            End If
            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("CHI")) = "S" Then
                str = str & ",{id: 'CHI',text: 'CHI Río Chicamo',cls:'CauceNivel0Tree'}"
            End If
            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("CHE")) = "S" Then
                str = str & ",{id: 'CHE',text: 'CHE Centrales Hidroeléctricas',cls:'CauceNivel0Tree'}"
            End If
            If utiles.nullABlanco(dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item("AZ")) = "S" Then
                str = str & ",{id: 'AZ',text: 'AZ Azarbe',cls:'CauceNivel0Tree'}"
            End If
            str = str & ",{id: 'AZ',text: 'AZ Azarbe',cls:'CauceNivel0Tree'}"
        End If

        

        If str.Length > 0 Then
            str = str.Substring(1)
        End If
        Return str

    End Function
    Private Function tienePermisos() As Boolean
        Dim claveNodo As String = ""
        Dim raiz() As String
        Dim cauceshijos As Integer = tBusqueda.Rows.Count
        CargarDataSEtPermisos()

        If cauceshijos > 0 Then
            claveNodo = tBusqueda.Rows(cauceshijos - 1).Item("claveNodo").ToString
            raiz = claveNodo.Split("#")
        End If

        'Construccion del string de permisos
        If dstRamasVisibles.Tables("tablaVisibles").Rows(0).Item(raiz(0)) = "S" Then
            Return True
        Else
            Return False
        End If

    End Function
    Protected Function ObtenerFavorito(ByVal codigoPVYCR As String) As String
        Dim favorito As String = ""
        'obtenemos el punto si es M o G
        Dim sentenciatipopunto = "select favorito from PVYCR_puntos where codigoPVYCR = '" & codigoPVYCR & "'"
        utiles.Comprobar_Conexion_BD(Page, conexion)
        dacauces.SelectCommand.CommandText = sentenciatipopunto
        favorito = utiles.nullABlanco(utiles.nothingABlanco(dacauces.SelectCommand.ExecuteScalar))
        Return favorito
    End Function
    Protected Function EstaCaducado(ByVal codigoPVYCR As String) As Integer
        Dim sentencia = "select count(*) from PVYCR_puntos where codigoPVYCR = '" & codigoPVYCR & "' and ((fecha_fin is not null and dateadd(month,2,fecha_fin)<getdate()) or (fecha_fin is null and fecha_inicion is null)) "
        utiles.Comprobar_Conexion_BD(Page, conexion)
        dacauces.SelectCommand.CommandText = sentencia
        Return dacauces.SelectCommand.ExecuteScalar
    End Function
    Protected Function ObtenerIconoPunto(ByVal codigoPVYCR As String) As String
        'obtenemos tipo del icono para saber qué icono escoger.
        'si es tipo = 1 --> amarillos (xxxxY32)
        'si es tipo = 2 --> naranjas (xxxxO32)
        'si es tipo = 3 --> verdes (xxxxG32)
        'si es tipo = 4 --> azules (xxxxO32)
        Dim sentenciatipopunto = "select top 1 tipo from pvycr_clasificacionelementos where em is null and codigoPVYCR = '" & codigoPVYCR & "'"

        utiles.Comprobar_Conexion_BD(Page, conexionU)
        daclasificacion.SelectCommand.CommandText = sentenciatipopunto
        Dim icono As String = daclasificacion.SelectCommand.ExecuteScalar
        utiles.CerrarConexion(conexionU)

        If icono = Nothing Then
            icono = "0"
        End If
        Return icono
    End Function
    Protected Function ObtenerIconoPuntoE(ByVal codigoPVYCR As String, ByVal idElemento As String) As String
        'obtenemos tipo del icono para saber qué icono escoger.
        'si es tipo = 1 --> amarillos (xxxxY32)
        'si es tipo = 2 --> naranjas (xxxxO32)
        'si es tipo = 3 --> verdes (xxxxG32)
        'si es tipo = 4 --> azules (xxxxO32)
        Dim sentenciatipopunto = "select top 1 tipo from pvycr_clasificacionelementos where codigoPVYCR = '" & codigoPVYCR & "' and em = '" & idElemento & "' "

        utiles.Comprobar_Conexion_BD(Page, conexionU)
        daclasificacion.SelectCommand.CommandText = sentenciatipopunto
        Dim icono As String = daclasificacion.SelectCommand.ExecuteScalar
        utiles.CerrarConexion(conexionU)

        If icono = Nothing Then
            icono = "0"
        End If
        Return icono
    End Function
End Class
