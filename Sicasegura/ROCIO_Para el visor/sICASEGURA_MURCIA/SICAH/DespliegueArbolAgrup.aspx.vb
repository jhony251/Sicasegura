Imports System.IO
Imports GuarderiaFluvial
Partial Class SICAH_DespliegueArbolAgrup
    Inherits System.Web.UI.Page
    Dim conexionDBSica As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_dbsica"))
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim conexionU As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim dacauces As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim daclasificacion As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionU)
    Dim daAgrupaciones As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexionDBSica)
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
          
            'obtenemos el idpadre para saber los hijos
            Dim idPadre As String = node

            'si no es un nodo final
            'Si contiene la letra W, significa que es un nodo hijo y no tiene que cargar más 
            'nodos del árbol, es el último nivel

            If node.Substring(0, 1) <> "W" Then
                If node.Substring(0, 1) <> "P" Then

                    If Integer.Parse(idPadre) > 0 Then
                        'abrimos fichero donde caegaremos los hijos
                        'salida = New StreamWriter(rutaficherohijos, False, System.Text.Encoding.UTF8)
                        'el tipo de fichero debe de aer un json
                        Response.ContentType = "text/json"
                        'salida.WriteLine("[")
                        Response.Write("[")
                        'cargamos los hijos de los nodos padres
                        CargarRamasHijos(node, Integer.Parse(idPadre))
                        'salida.WriteLine("]")
                        Response.Write("]")
                        'salida.Close()
                        'cargamos los hijos en el árbol
                        'Response.WriteFile(rutaficherohijos)
                        Response.Flush()
                        'File.Delete(rutaficherohijos)
                    End If
                Else
                    Response.ContentType = "text/json"

                    'cargamos la ruta de las ramas raices ARG/CM/S/OT1....
                    Response.Write("[" & RamasVisiblesString(node.Substring(1, 1)) & "]")
                    'Response.WriteFile(rutaficheroRamaR)
                    Response.Flush()

                End If
            End If
        End If
    End Sub
    Protected Sub CargarRamasHijos(ByVal nodo As String, ByVal idarbolPadre As String)
        'con el idpadre obtendremos los hijos
        sentencia = "SELECT     SICA_SIST_Sistemas.ID, SICA_SIST_Sistemas.Descripcion, [SICA_SIST_Sistemas-Punto].ID_PuntoSistema, " & _
        " SICA_SIST_PuntoSistema.CodigoPVYCR, SICA_SIST_PuntoSistema.EM, SICA_SIST_PuntoSistema.CFD, SICA_SIST_PuntoSistema.TOC, SICA_SIST_PuntoSistema.FI, SICA_SIST_PuntoSistema.FF,SICA_SIST_Sistemas.NumInscripcion " & _
        " FROM         SICA_SIST_Sistemas INNER JOIN     [SICA_SIST_Sistemas-Punto] ON SICA_SIST_Sistemas.ID = [SICA_SIST_Sistemas-Punto].ID_Sistema " & _
        " INNER JOIN    SICA_SIST_PuntoSistema ON [SICA_SIST_Sistemas-Punto].ID_PuntoSistema = SICA_SIST_PuntoSistema.ID WHERE SICA_SIST_Sistemas.ID=" & idarbolPadre
        Dim dstPuntos As New Data.DataSet()
        utiles.Comprobar_Conexion_BD(Page, conexionDBSica)
        daAgrupaciones.SelectCommand.CommandText = sentencia
        daAgrupaciones.Fill(dstPuntos, "Tablahijos")
       

        For Each fila In dstPuntos.Tables("TablaHijos").Rows
            hijos &= ",{id: 'W" & fila("ID_PuntoSistema") & "',text: '" & fila("CodigoPVYCR") & "',cls:'CauceNivel2Tree'," & _
             " href:'Agrupaciones_Detalle.aspx?idAgrupacion=" & fila("ID") & "&Nombre=" & fila("NumInscripcion") & "',hrefTarget:'iframeDetalle',leaf:'true'"
            tipoPunto = ObtenerTipoPunto(fila("CodigoPVYCR"))
            Dim esfavorito As String = ""
            Dim Caducado As Integer = 0
            Dim tipoIcono As String = ""
            tipoIcono = ObtenerIconoPunto(fila("CodigoPVYCR"))
            esfavorito = utiles.nullABlanco(ObtenerFavorito(fila("CodigoPVYCR")))
            Caducado = EstaCaducado(fila("CodigoPVYCR"))
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
            hijos &= estilo & "}"
        Next

        If (hijos.Length > 0) Then
            hijos = hijos.Substring(1)
        End If
        'salida.WriteLine(hijos)
        Response.Write(hijos)

    End Sub

    Protected Function ObtenerIdNodoPadre(ByVal nodo As String, ByVal tipo As String) As Integer
        Dim sentenciaTipo As String = ""
        sentencia = "SELECT  ID FROM  SICA_SIST_Sistemas WHERE "
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

        'Primer nivel
        Dim Str As String = "{id:'P0',text: 'Agrupaciones RACCS',cls:'CauceNivel0Tree'},{id:'P1',text: 'Agrupaciones',cls:'CauceNivel0Tree'}"

        Response.ContentType = "text/json"
        'cargamos la ruta de las ramas raices ARG/CM/S/OT1....
        Response.Write("[" & Str & "]")
        'Response.WriteFile(rutaficheroRamaR)
        Response.Flush()
        Response.ContentType = "text/json"

        ''cargamos la ruta de las ramas raices ARG/CM/S/OT1....
        'Response.Write("[" & RamasVisiblesString() & "]")
        ''Response.WriteFile(rutaficheroRamaR)
        'Response.Flush()
        ''File.Delete(rutaficheroRamaR)
    End Sub
    Protected Sub CargarDataSEtPermisos()
        'NCM rellenamos en un dataset los valores que tiene el usuario para saber qué ramas puede ver

        daRamasVisibles.SelectCommand.CommandText = "SELECT ARG, CM, ET, OT1,S,VA,VB,VM,EA,MU,PL,MO,GU,CHI,CHE from TUsuarios where login= '" & Session("loginUsuario").ToString & "' "

        utiles.Comprobar_Conexion_BD(Page, conexionU)
        daRamasVisibles.Fill(dstRamasVisibles, "TablaVisibles")
        utiles.CerrarConexion(conexionU)


    End Sub
    Private Function RamasVisiblesString(ByVal Nodo As String)
        Dim str As String = ""

        Dim dstAgrupaciones As New Data.DataSet()
        If Nodo = "0" Then
            'Agrupaciones RAACS
            daAgrupaciones.SelectCommand.CommandText = "SELECT     ID, Descripcion, Publico, NumInscripcion  FROM SICA_SIST_Sistemas WHERE not NumInscripcion  is null order by 4"

            daAgrupaciones.Fill(dstAgrupaciones, "TablaAgrup")
            For Each fila In dstAgrupaciones.Tables("TablaAgrup").Rows
                str = str & ",{id: '" & fila("ID") & "',text: '" & fila("NumInscripcion") & "',cls:'CauceNivel1Tree'," & _
                " href:'Agrupaciones_Detalle.aspx?idAgrupacion=" & fila("ID") & "&Nombre=" & fila("NumInscripcion") & "&RAAC=1',hrefTarget:'iframeDetalle'}"
            Next

            If str.Length > 0 Then
                str = str.Substring(1)
            End If
            Return str
        Else
            'Agrupaciones
            daAgrupaciones.SelectCommand.CommandText = "SELECT     ID, Descripcion, Publico, NumInscripcion, Nombre  FROM SICA_SIST_Sistemas WHERE NumInscripcion  is null order by 4"

            daAgrupaciones.Fill(dstAgrupaciones, "TablaAgrup")

            For Each fila In dstAgrupaciones.Tables("TablaAgrup").Rows
                str = str & ",{id: '" & fila("ID") & "',text: '" & fila("Nombre") & "',cls:'CauceNivel1Tree'," & _
                " href:'Agrupaciones_Detalle.aspx?idAgrupacion=" & fila("ID") & "&Nombre=" & fila("Nombre") & "&RAAC=0',hrefTarget:'iframeDetalle'}"
            Next

            If str.Length > 0 Then
                str = str.Substring(1)
            End If
            Return str


        End If
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
