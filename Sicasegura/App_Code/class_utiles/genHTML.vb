Imports System.IO
Imports System.Data.SqlClient

'Clases de utilidad
Namespace GuarderiaFluvial

    Public Class genHTML

        Shared Function compruebaUsuario(ByVal idUsuario As Integer) As Boolean

            Dim resul As Boolean = True
            If utiles.nullACero(idUsuario) = 0 Then
                resul = False
            End If
            Return resul
        End Function

        Shared Function cabecera(ByVal pagina As Page) As String
            Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
            Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
            'pagina.ClientScript.RegisterStartupScript(GetType(Page), "salida", "<script language=javascript>" & _
            '                                                                    "window.onbeforeunload = function(){" & _
            '                                                                    "var iX = window.document.body.offsetWidth - window.event.clientX ;" & _
            '                                                                    "var iY = window.event.clientY ;" & _
            '                                                                    "if (iX <=30 && iY < 0 )" & _
            '                                                                    "{" & _
            '                                                                    "    window.open('cerrarAplicacion.aspx','Cerrar', 'toolbar=no,menubar=no,top=200,left=250,height=1,width=1');" & _
            '                                                                    "}" & _
            '                                                                    "};" & _
            '                                                                    "</script>")

            Dim eltexto As String
            Dim ruta As String
            '************************************** ncm para que entre desde infoptocontrol 31/05/2010 ********************************************************
            Dim idUsuario As Integer = 0
            Dim rdr As SqlDataReader
            Dim encontrado As Boolean = False

            'ncm 04/11/2008 comentado porque lo cambiamos por comprobar_conexion
            utiles.Comprobar_Conexion_BD(pagina, conexion, True)
            'If conexion.State = Data.ConnectionState.Closed Then conexion.Open()


            '**************** IPIM 10/02/2011 POR INCIDENCIA 254: 
            'comando.CommandText = "select u.*,j.id as idJerarquia, j.prefijo from tusuarios u join tcargos c on c.id=u.idcargo join tjerarquias j on j.id=c.idjerarquia where u.login=@login"
            'comando.Parameters.AddWithValue("@login", "sica")
            ''comando.Parameters.AddWithValue("@password", "a")
            'rdr = comando.ExecuteReader()
            'If rdr.Read() Then
            '    pagina.Session("usuarioReg") = rdr("id")
            '    pagina.Session("nombreUsuario") = rdr("nombre") & " " & rdr("Apellidos")
            '    pagina.Session("loginUsuario") = rdr("login")
            '    pagina.Session("idJerarquiaUsuario") = rdr("idjerarquia")
            '    pagina.Session("idCargoUsuario") = rdr("idCargo")
            '    pagina.Session("prefijo") = rdr("prefijo")
            '    pagina.Session("idPerfil") = rdr("idPerfil")
            'End If
            '**************** FIN IPIM 10/02/2011 POR INCIDENCIA 254: 

            ' ****************************************** fin ncm 31/05/2010 **********************************************************************************************************************
            'If CInt("0" & pagina.Session("usuarioReg")) <= 0 Then
            '    If pagina.FindControl("txtLogin") Is Nothing Then
            '        pagina.Response.Redirect("index.aspx")
            '    End If
            '    eltexto = ""
            'Else
            '    If Directory.Exists(pagina.Server.MapPath("./images")) Then
            '        ruta = ""
            '    Else
            '        ruta = "../"
            '    End If
            '    eltexto = "<table width=""100%"" cellpadding=""0"" align=""right"" cellspacing=""0"">" & _
            '      "<tr><td align=""right"" style=""color:#A9A363; padding-right:2px;"" width=""12%"">" & pagina.Session("nombreUsuario") & " :<br> [<a href=""" & ruta & "abandon.aspx"" class=""cerrar"">cerrar sesión</a>]</td></tr>" & _
            '      "</table>"
            '    ' El color que había antes era E1DA93
            'End If
            Dim ipaddr As String = pagina.Request.ServerVariables("REMOTE_ADDR")

            Dim resul As String = "<tr>"
            If pagina.Session("idJerarquiaUsuario") = 19 Then
                'resul = resul & "<td class=""cabeceraAplicacion2"" valign=""bottom"">"
                'resul = resul & "<td class=""cabeceraSicaSegura"" valign=""bottom"">"
                resul = resul & "<td style='height:60px'>"
                resul &= "<table width='100%' cellspacing='0' cellpadding='0' height='60' style='border-bottom: 1px solid black'>"
                resul &= "<tr>"
                resul &= "<td style='width:120px'>"
                resul &= "<img src='images/cab_izq.jpg'>"
                resul &= "</td>"
                resul &= "<td style='background-image:url(images/cab_landscape.jpg);background-position:center center;background-repeat:no-repeat;text-align:right;vertical-align:bottom;padding-right:3px;padding-bottom:3px'>"
                resul &= "<table><tr><td style='color:navy;padding:3px;background-image:url(images/fhint.png);'><b>SICA - Sistema Integrado de Control de Aprovechamientos</b></td></tr></table>"
                resul &= "</td>"
                resul &= "<td style='width:120px'>"
                resul &= "<img src='images/cab_der.jpg'>"
                resul &= "</td>"
                resul &= "</tr>"
                resul &= "</table>"
            Else
                'resul = resul & "<td class=""cabeceraAplicacion"" valign=""bottom"">"
                'resul = resul & "<td class=""cabeceraSicaSegura"" valign=""bottom"">"
                resul = resul & "<td style='height:60px'>"
                resul &= "<table width='100%' cellspacing='0' cellpadding='0' height='60'>"
                resul &= "<tr>"
                resul &= "<td style='width:120px'>"
                resul &= "<img src='images/cab_izq.jpg'>"
                resul &= "</td>"
                resul &= "<td style='background-image:url(images/cab_landscape.jpg);background-position:center center;background-repeat:no-repeat;text-align:right;vertical-align:bottom;padding-right:3px;padding-bottom:3px'>"
                resul &= "<table><tr><td style='color:navy;padding:3px;background-image:url(images/fhint.png);'><b>SICA - Sistema Integrado de Control de Aprovechamientos</b></td></tr></table>"
                resul &= "</td>"
                resul &= "<td style='width:120px'>"
                resul &= "<img src='images/cab_der.jpg'>"
                resul &= "</td>"
                resul &= "</tr>"
                resul &= "</table>"
            End If
            resul = resul & eltexto & _
                                "</td>" & _
                                "</tr>"
            conexion.Close()
            Return resul

            'Dim resul As String = ""

            'resul &= "<table width='100%' cellspacing='0' cellpadding='0'>"
            'resul = "<tr>"
            'resul &= "<td style='width:120px'>"
            'resul &= "<img src='images/cab_izq.jpg'>"
            'resul &= "</td>"
            'resul &= "<td style='background-image:url(images/cab_landscape.jpg);background-position:center center;background-repeat:no-repeat;text-align:right;vertical-align:bottom;padding-right:3px;padding-bottom:3px'>"
            'resul &= "<table><tr><td style='color:navy;padding:3px;background-image:url(html/images/fhint.png);'><b>SICA - Sistema Integrado de Control de Aprovechamientos</b></td></tr></table>"
            'resul &= "</td>"
            'resul &= "<td style='width:120px'>"
            'resul &= "<img src='images/cab_der.jpg'>"
            'resul &= "</td>"
            'resul &= "</tr>"
            'resul &= "</table>"

            'Return resul


        End Function

        Shared Function cabeceraDesdeSICAH(ByVal pagina As Page) As String
            Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
            Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
            Dim eltexto As String
            Dim ruta As String
            Dim idUsuario As Integer = 0
            Dim rdr As SqlDataReader
            Dim encontrado As Boolean = False
            utiles.Comprobar_Conexion_BD(pagina, conexion, True)
            
            Dim ipaddr As String = pagina.Request.ServerVariables("REMOTE_ADDR")

            Dim resul As String = "<tr>"
            If pagina.Session("idJerarquiaUsuario") = 19 Then
                resul = resul & "<td style='height:60px'>"
                resul &= "<table width='100%' cellspacing='0' cellpadding='0' height='60' style='border-bottom: 1px solid black'>"
                resul &= "<tr>"
                resul &= "<td style='width:120px'>"
                resul &= "<img src='../images/cab_izq.jpg'>"
                resul &= "</td>"
                resul &= "<td style='background-image:url(../images/cab_landscape.jpg);background-position:center center;background-repeat:no-repeat;text-align:right;vertical-align:bottom;padding-right:3px;padding-bottom:3px'>"
                resul &= "<table><tr><td style='color:navy;padding:3px;background-image:url(../images/fhint.png);'><b>SICA - Sistema Integrado de Control de Aprovechamientos</b></td></tr></table>"
                resul &= "</td>"
                resul &= "<td style='width:120px'>"
                resul &= "<img src='../images/cab_der.jpg'>"
                resul &= "</td>"
                resul &= "</tr>"
                resul &= "</table>"
            Else
                resul = resul & "<td style='height:60px'>"
                resul &= "<table width='100%' cellspacing='0' cellpadding='0' height='60'>"
                resul &= "<tr>"
                resul &= "<td style='width:120px'>"
                resul &= "<img src='../images/cab_izq.jpg'>"
                resul &= "</td>"
                resul &= "<td style='background-image:url(../images/cab_landscape.jpg);background-position:center center;background-repeat:no-repeat;text-align:right;vertical-align:bottom;padding-right:3px;padding-bottom:3px'>"
                resul &= "<table><tr><td style='color:navy;padding:3px;background-image:url(../images/fhint.png);'><b>SICA - Sistema Integrado de Control de Aprovechamientos</b></td></tr></table>"
                resul &= "</td>"
                resul &= "<td style='width:120px'>"
                resul &= "<img src='../images/cab_der.jpg'>"
                resul &= "</td>"
                resul &= "</tr>"
                resul &= "</table>"
            End If
            resul = resul & eltexto & "</td></tr>"
            conexion.Close()
            Return resul

        End Function

        'Shared Function pestanyasMenu(ByVal seccion As Integer, ByVal subsec As String, ByVal pagina As Page, ByVal idperfil As Integer, ByVal idUsuario As Integer) As String

        '  Dim resul As String = ""
        '  Dim ruta As String = ""
        '  Dim cadena() As String
        '  If Directory.Exists(pagina.Server.MapPath("./images")) Then
        '    ruta = "images"
        '  Else
        '    ruta = "../images"
        '  End If
        '  cadena = Split(System.Configuration.ConfigurationManager.AppSettings("urlSICAH"), ",")
        '  Dim TienePermisosNivus = False

        '  Dim PermisosNivus() As String = Split(System.Configuration.ConfigurationManager.AppSettings("UsuariosPermitidosNivus"), ",")
        '  For i As Integer = 0 To PermisosNivus.Length - 1
        '    If idUsuario = PermisosNivus(i) Then
        '      TienePermisosNivus = True
        '    End If

        '  Next
        '  ruta = "images"

        '  'If TienePermisosNivus Then
        '  '  resul &= "<div id=""desplegableNivus"" style=""position:absolute;overflow:auto;width:130px;top:188px;padding:2px;background-color:#EEEAD2;border:0px solid #8C8B83;display:none;left:18px;padding-left:5px;border-left:1px solid #8C8B83; border-right:1px solid #8C8B83; border-bottom:solid 1px #8C8B83"" onmouseout=""style.backgroundColor='#EEEAD2'"" onmouseover=""style.backgroundColor='#CCD3B0'""> "
        '  '  resul &= "<img src='../SICAH/images/icoPestNibus.gif' border=0 align=absmiddle>&nbsp;&nbsp;<a href='" & subsec & "SICAH/nivus.aspx' >Insertar Nivus</a></div>"
        '  'End If
        '  resul &= "<tr>"
        '  resul &= "<td style=""padding-top:5px"">"
        '  resul &= "<table cellspacing=0 cellpadding=0 style=""/*border-bottom:1px solid #808080*/"">"
        '  resul &= "<tr>"
        '  resul &= "	<td style=""width:8px;background-image:url(" & subsec & ruta & "/pestBordeInf.gif)""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"

        '  'si el perfil es grabador de PVY o administrador se verá preproducción, sino no

        '  'EGB21042008
        '  If seccion = 4 Then
        '    'ncm 31/05/2010 si se llama desde infoPtoControl le pasaremos unos parámetros con el valor del nodo/codigoPVYCR y la busqueda a 's'
        '    'If pagina.Request.QueryString("nodobusqueda") = "s" Then
        '    '  resul &= "	<td width=17 class=""pestOn1""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
        '    '  resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='" & subsec & "SICAH/caucepuntMAIN.aspx?nodobusqueda=s&valor=" & pagina.Request.QueryString("valor") & "'> <img src='" & ruta & "/iconos/icoPestCauces.gif' border=0 align=absmiddle> <b>Cauces y puntos</b></a></td>"
        '    '  resul &= "	<td class=""pestOn3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '    'Else
        '    resul &= "	<td width=17 class=""pestOn1""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
        '    resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='" & subsec & "SICAH/caucepuntMAIN.aspx?nodobusqueda=n&valor=0'> <img src='" & ruta & "/iconos/icoPestCauces.gif' border=0 align=absmiddle> <b>Control de aprovechamientos</b></a></td>"
        '    resul &= "	<td class=""pestOn3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '    'End If
        '  Else
        '    resul &= "	<td class=""pestOff1""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"
        '    resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='" & subsec & "SICAH/caucepuntMAIN.aspx'> <img src='" & ruta & "/iconos/icoPestCauces.gif' border=0 align=absmiddle>Control de aprovechamientos</a></td>"
        '    'ncm resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='" & subsec & "SICAH/NCM_caucepuntMAIN.aspx'> <img src='" & ruta & "/iconos/icoPestCauces.gif' border=0 align=absmiddle> Cauces y puntos</a></td>"
        '    resul &= "	<td class=""pestOff3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  End If
        '  'If seccion = 5 Then
        '  '    resul &= "	<td width=17 class=""pestOn1""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
        '  '    resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='" & subsec & "SICAH/calculosistemas2.aspx'> <img src='" & ruta & "/iconos/icoPestSistemas2.gif' border=0 align=absmiddle> <b>Sistemas y Cálculos </b></a></td>"
        '  '    resul &= "	<td class=""pestOn3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  'Else
        '  '    resul &= "	<td class=""pestOff1""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"
        '  '    resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='" & subsec & "SICAH/calculosistemas2.aspx'> <img src='" & ruta & "/iconos/icoPestSistemas2.gif' border=0 align=absmiddle> Sistemas y Cálculos</a></td>"
        '  '    resul &= "	<td class=""pestOff3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  'End If

        '  'ncm 23/10/2008 ´creamos el submenú de preproducción
        '  resul &= "<div id=""desplegableMotores"" style=""position:absolute;overflow:auto;width:170px;top:105px;padding:2px;background-color:#EEEAD2; border-top:5px; border-top-style:solid; border-top-color:#8C8B83; display:none; left:238px; padding-left:5px;border-left:1px solid #8C8B83; border-right:1px solid #8C8B83"" onmouseout=""style.backgroundColor='#EEEAD2'"" onmouseover=""style.backgroundColor='#CCD3B0'"" >"
        '  resul &= "<img src='../SICAH/images/puntoMotor.ico' border=0 align=absmiddle>&nbsp;&nbsp;<a href='" & subsec & "SICAH/motores.aspx' >Conducción forzada - Volúmen</a>"
        '  resul &= "</div>"
        '  resul &= "<div id=""desplegableHorometros"" style=""position:absolute;overflow:auto;width:170px;top:130px;padding:2px;background-color:#EEEAD2; border-left:1px; border-left-style:solid; border-left-color:#8C8B83; border-right:1px solid #8C8B83;display:none;left:238px;padding-left:5px"" onmouseout=""style.backgroundColor='#EEEAD2'"" onmouseover=""style.backgroundColor='#CCD3B0'"" >"
        '  resul &= "<img src='../SICAH/images/cronometro.gif' border=0 align=absmiddle>&nbsp;&nbsp;<a href='" & subsec & "SICAH/horometros.aspx'>Conducción forzada - Tiempo</a></div>"
        '  resul &= "<div id=""desplegableAlimentacion"" style=""position:absolute;overflow:auto;width:170px;top:150px;padding:2px;background-color:#EEEAD2;border:0px solid #8C8B83;display:none;left:238px;padding-left:5px;border-left:1px solid #8C8B83; border-right:1px solid #8C8B83"" onmouseout=""style.backgroundColor='#EEEAD2'"" onmouseover=""style.backgroundColor='#CCD3B0'"">"
        '  resul &= "<img src='../SICAH/images/enchufecuadrado.jpg' border=0 align=absmiddle>&nbsp;&nbsp;<a href='" & subsec & "SICAH/alimentacion.aspx' >Conducción forzada - Energía</a></div> "
        '  resul &= "<div id=""desplegableAcequias"" style=""position:absolute;overflow:auto;width:170px;top:169px;padding:2px;background-color:#EEEAD2;border:0px solid #8C8B83;display:none;left:238px;padding-left:5px;border-left:1px solid #8C8B83; border-right:1px solid #8C8B83; border-bottom:solid 1px #8C8B83"" onmouseout=""style.backgroundColor='#EEEAD2'"" onmouseover=""style.backgroundColor='#CCD3B0'""> "
        '  resul &= "<img src='../SICAH/images/puntoAcequia2.gif' border=0 align=absmiddle>&nbsp;&nbsp;<a href='" & subsec & "SICAH/acequias.aspx' >Lámina libre - Caudal</a></div>"

        '  If idperfil = 11 Or idperfil = 1 Or idperfil = 14 Then
        '    'ncm 22/10/2008 nuevo código para el menú preproducción con submenú
        '    If seccion = 0 Then
        '      resul &= "	<td width=17 class=""pestOn1""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
        '      resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='#' onclick=""desplegarPreproduccion();return false;""> <img src='" & ruta & "/preproduccion.gif' border=0 align=absmiddle> <b>Información manual</b></a></td>"
        '      resul &= "	<td class=""pestOn3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '    Else
        '      resul &= "	<td width=17 class=""pestOff1Grande""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
        '      resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='#' onclick=""desplegarPreproduccion();return false;""> <img src='" & ruta & "/preproduccion.gif' border=0 align=absmiddle>Información manual</a></td>"
        '      resul &= "	<td class=""pestOff3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '    End If
        '  End If
        '  'ncm 14/06/2010 se comenta por petición del cliente
        '  'If seccion = 7 Then
        '  '    resul &= "	<td width=17 class=""pestOn1""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
        '  '    resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='" & subsec & "SICAH/telegestion.aspx'> <img src='" & ruta & "/iconos/icoPestNibus.gif' border=0 align=absmiddle> <b> Telegestión</b></a></td>"
        '  '    resul &= "	<td class=""pestOn3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  'Else
        '  '    resul &= "	<td class=""pestOff1""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"
        '  '    resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='" & subsec & "SICAH/telegestion.aspx'> <img src='" & ruta & "/iconos/icoPestNibus.gif' border=0 align=absmiddle> Telegestión</a></td>"
        '  '    resul &= "	<td class=""pestOff3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  'End If

        '  'RDF 15/10/2008
        '  'Visualización tie
        '  If seccion = 8 Then
        '    resul &= "	<td width=17 class=""pestOn1""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
        '    resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='" & subsec & "SICAH/AccesoTopkapi.aspx'> <img src='" & ruta & "/verArbolCaucesSistemas.GIF' border=0 align=absmiddle> <b> Información automática</b></a></td>"
        '    resul &= "	<td class=""pestOn3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  Else
        '    resul &= "	<td class=""pestOff1""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"
        '    resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='" & subsec & "SICAH/AccesoTopkapi.aspx'> <img src='" & ruta & "/verArbolCaucesSistemas.GIF' border=0 align=absmiddle> Información automática</a></td>"
        '    resul &= "	<td class=""pestOff3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  End If
        '  If seccion = 6 Then
        '    resul &= "	<td width=17 class=""pestOn1""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
        '    resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='" & subsec & "SICAH/index.aspx'> <img src='" & ruta & "/iconos/icoPestMantenimiento.gif' border=0 align=absmiddle> <b> Mantenimiento</b></a></td>"
        '    resul &= "	<td class=""pestOn3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  Else
        '    resul &= "	<td class=""pestOff1""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"
        '    resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='" & subsec & "SICAH/index.aspx'> <img src='" & ruta & "/iconos/icoPestMantenimiento.gif' border=0 align=absmiddle> Mantenimiento</a></td>"
        '    resul &= "	<td class=""pestOff3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  End If

        '  'ncm pruebas informe interriego
        '  'If seccion = 9 Then
        '  '    resul &= "	<td width=17 class=""pestOn1""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
        '  '    resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='" & subsec & "SICAH/PantallaInformeInterriego.aspx'> <img src='" & ruta & "/iconos/icoPestSistemas.gif' border=0 align=absmiddle> <b> Informe Interriego</b></a></td>"
        '  '    resul &= "	<td class=""pestOn3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  'Else
        '  '    resul &= "	<td class=""pestOff1""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"
        '  '    resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='" & subsec & "SICAH/PantallaInformeInterriego.aspx'> <img src='" & ruta & "/iconos/icoPestSistemas.gif' border=0 align=absmiddle> Informe Interriego</a></td>"
        '  '    resul &= "	<td class=""pestOff3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
        '  'End If


        '  resul &= "	<td style=""background-image:url(" & subsec & "images/pestBordeInf.gif)""><img src=""" & subsec & "images/relleno8.gif""></td>"
        '  resul &= "<td style=""background-image:url(" & subsec & "images/pestBordeInf.gif)"" align=""right"" nowrap>"

        '  If File.Exists(pagina.Server.MapPath("./accesoVisor.aspx")) Then
        '    resul &= "<a href=""documentos/Manual de usuario.pdf"" target=""_blank"" style=""color:navy"">Manual de usuario »</a>&nbsp;&nbsp;&nbsp;&nbsp;"
        '    resul &= "<a href=""accesoVisor.aspx"" target=""_blank"" style=""color:navy"">Acceso Gis »</a>&nbsp;&nbsp;&nbsp;&nbsp;"
        '    resul &= "<a href=""accesoVisorTOPKAPI.aspx"" target=""_blank"" style=""color:navy"">Acceso SCADA »</a>&nbsp;&nbsp;"

        '  ElseIf File.Exists(pagina.Server.MapPath("../accesoVisor.aspx")) Then
        '    resul &= "<a href=""../documentos/Manual de usuario.pdf"" target=""_blank"" style=""color:navy"">Manual de usuario »</a>&nbsp;&nbsp;&nbsp;&nbsp;"
        '    resul &= "<a href=""../accesoVisor.aspx"" target=""_blank"" style=""color:navy"">Acceso Gis »</a>&nbsp;&nbsp;&nbsp;&nbsp;"
        '    resul &= "<a href=""../accesoVisorTOPKAPI.aspx"" target=""_blank"" style=""color:navy"">Acceso SCADA »</a>&nbsp;&nbsp;"
        '  End If


        '  resul &= "</td>"
        '  resul &= ""
        '  resul &= "</tr>"
        '  resul &= "</table>"
        '  resul &= "<table width=100% cellspacing=0 cellpadding=0><tr><td style=""background-image:url(" & subsec & "images/gradBajoPest.jpg);height:11px;font-size:6px"">&nbsp;</td></tr></table>"
        '  resul &= "</td>"
        '  resul &= "</tr>"



        '  Return resul

        'End Function

        Shared Function pestanyasBandeja(ByVal opcion As Integer, ByVal pagina As Page) As String
            Dim resul As String = ""
            Dim ruta As String = ""
            ruta = "images"

            resul &= "<tr>"
            resul &= "<td>"
            resul &= "<table cellspacing=0 cellpadding=0 style=""/*border-bottom:1px solid #808080*/"">"

            resul &= "<tr>"
            resul &= "	<td style=""width:8px;background-image:url(images/pestBordeInf.gif);""><img src=""images/relleno8.gif""></td>"

            'Ver las tareas por estado
            If opcion = 0 Then
                resul &= "	<td width=17 class=""pestOn1""><img src=""images/relleno17.gif""></td>"
                resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='home.aspx?opcion=0'> <img src='" & ruta & "/iconos/icoOpCambioEstado.gif' border=0 align=absmiddle> <b>Por Estados</b></a></td>"
                resul &= "	<td class=""pestOn3""><img src=""images/relleno2.gif""></td>"
            Else
                resul &= "	<td width=17 class=""pestOff1Grande""><img src=""images/relleno17.gif""></td>"
                resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='home.aspx?opcion=0'> <img src='" & ruta & "/iconos/icoOpCambioEstado.gif' border=0 align=absmiddle> Por Estados</a></td>"
                resul &= "	<td class=""pestOff3""><img src=""images/relleno2.gif""></td>"
            End If

            'Ver las tareas por usuario
            If opcion = 1 Then
                resul &= "	<td width=17 class=""pestOn1""><img src=""images/relleno17.gif""></td>"
                resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='home.aspx?opcion=1'> <img src='" & ruta & "/iconos/icoPestUsuarios.gif' border=0 align=absmiddle> <b>Por Usuarios</b></a></td>"
                resul &= "	<td class=""pestOn3""><img src=""images/relleno2.gif""></td>"
            Else
                resul &= "	<td class=""pestOff1""><img src=""images/relleno8.gif""></td>"
                resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='home.aspx?opcion=1'> <img src='" & ruta & "/iconos/icoPestUsuarios.gif' border=0 align=absmiddle> Por Usuarios</a></td>"
                resul &= "	<td class=""pestOff3""><img src=""images/relleno2.gif""></td>"
            End If


            resul &= "<td style=""background-image:url(images/pestBordeInf.gif)""><img src=""images/relleno8.gif""></td>"
            resul &= "<td style=""background-image:url(images/pestBordeInf.gif)"" width=100%>&nbsp;</td>"
            resul &= "</tr>"

            resul &= "</table>"
            resul &= "<table width=100% cellspacing=0 cellpadding=0><tr><td style=""background-image:url(images/gradBajoPest.jpg);height:11px;font-size:6px"">&nbsp;</td></tr></table>"
            resul &= "</td>"
            resul &= "</tr>"


            Return resul

        End Function

        Shared Function pestanyasMenuAgrupacion(ByVal pagina As Page, ByVal CodigoPVYCR As String) As String
            Dim resul As String = ""
            Dim ruta As String = ""
            Dim cadena() As String
            If Directory.Exists(pagina.Server.MapPath("./images")) Then
                ruta = "images"
            Else
                ruta = "../images"
            End If

            'Dim ipaddr As String = pagina.Request.ServerVariables("REMOTE_ADDR")
            'en desarrollo para que prueben desde murcia tenemos localhost, ipmia, ipesther, etc, tendremos que desmenuzar la cadena
            'MyString = "VBScriptXisXfun!"
            cadena = Split(System.Configuration.ConfigurationManager.AppSettings("urlSICAH"), ",")
            ' MyArray(0) contains "VBScript".
            ' MyArray(1) contains "is".
            ' MyArray(2) contains "fun!".



            ruta = "images"

            resul &= "<tr>"
            resul &= "<td style=""padding-top:5px"">"
            resul &= "<table cellspacing=1 cellpadding=0 style=""/*border-bottom:1px solid #808080*/"">"
            resul &= "<tr>"
            resul &= "	<td style="" width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & "../SICAH/Agrupaciones_DatosAdmin.aspx?Pestanya=1&Nombre=" & CodigoPVYCR & "' style=""color:White"">  <b>Datos Administrativos I</b></a></td>"
            'resul &= "	<td width=200  border='1' style='border:1px solid #666666;background-color:white' class=""pestOn2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & "../" & "SICAH/Agrupaciones_DatosAdmin.aspx'""><a href='" & "../SICAH/Agrupaciones_DatosAdmin.aspx' style=""color:White"">  <b>Datos Administrativos I</b></a></td>"
            resul &= " <td width=1></td>"
            resul &= "	<td style="" width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & "../SICAH/Agrupaciones_DatosAdmin.aspx?Pestanya=2&Nombre=" & CodigoPVYCR & "' style=""color:White"">  <b>Datos Administrativos II</b></a></td>"

            resul &= "</td>"
            resul &= ""
            resul &= "</tr>"
            resul &= "</table>"
            ' resul &= "<table width=100% cellspacing=0 cellpadding=0><tr><td style=""background-image:url(" & subsec & "images/gradBajoPest.jpg);height:11px;font-size:6px"">&nbsp;</td></tr></table>"
            resul &= "</td>"
            resul &= "</tr>"



            Return resul


        End Function

        Shared Function pestanyasMenuArbol(ByVal seccion As Integer, ByVal subsec As String, ByVal pagina As Page, ByVal idperfil As Integer, ByVal tipo As String, ByVal clavenodo As String) As String

            Dim resul As String = ""
            Dim ruta As String = ""
            Dim cadena() As String
            If Directory.Exists(pagina.Server.MapPath("./images")) Then
                ruta = "images"
            Else
                ruta = "../images"
            End If

            'Dim ipaddr As String = pagina.Request.ServerVariables("REMOTE_ADDR")
            'en desarrollo para que prueben desde murcia tenemos localhost, ipmia, ipesther, etc, tendremos que desmenuzar la cadena
            'MyString = "VBScriptXisXfun!"
            cadena = Split(System.Configuration.ConfigurationManager.AppSettings("urlSICAH"), ",")
            ' MyArray(0) contains "VBScript".
            ' MyArray(1) contains "is".
            ' MyArray(2) contains "fun!".



            ruta = "images"

            resul &= "<tr>"
            resul &= "<td style=""padding-top:5px"">"
            resul &= "<table cellspacing=0 cellpadding=0 style=""/*border-bottom:1px solid #808080*/"">"
            resul &= "<tr>"
            resul &= "	<td style=""width:8px;background-image:url(" & subsec & ruta & "/pestBordeInf.gif)""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"
            'si el perfil es grabador de PVY o administrador se verá preproducción, sino no
            If idperfil = 11 Or idperfil = 1 Or idperfil = 14 Then
                If seccion = 0 Then
                    resul &= "	<td width=17 class=""pestOn1"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                    resul &= "	<td width=1% class=""pestOn2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White"">  <b>Información General</b></a></td>"
                    resul &= "	<td class=""pestOn3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                Else
                    If tipo = "C" Then
                        resul &= "	<td width=17 class=""pestOff1Grande"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                        resul &= "	<td width=1% class=""pestOff2"" style='cursor:hand' nowrap onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White""> Información General</a></td>"
                        resul &= "	<td class=""pestOff3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"

                    End If

                End If
                'If seccion = 0 Then
                '    resul &= "	<td width=17 class=""pestOn1""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                '    resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='" & subsec & "SICAH/motores.aspx'> <img src='" & ruta & "/puntoMotor.ico' border=0 align=absmiddle> <b>Motores</b></a></td>"
                '    resul &= "	<td class=""pestOn3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                'Else
                '    resul &= "	<td width=17 class=""pestOff1Grande""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                '    resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='" & subsec & "SICAH/motores.aspx'> <img src='" & ruta & "/puntoMotor.ico' border=0 align=absmiddle> Motores</a></td>"
                '    resul &= "	<td class=""pestOff3""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                'End If
                If seccion = 1 Then

                    resul &= "	<td width=17 class=""pestOn1"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                    resul &= "	<td width=1% class=""pestOn2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White""> <b>Información General</b></a></td>"
                    resul &= "	<td class=""pestOn3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                Else
                    If tipo = "P" Then
                        resul &= "	<td width=17 class=""pestOff1Grande"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                        resul &= "	<td width=1% class=""pestOff2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White""> Información General</a></td>"
                        resul &= "	<td class=""pestOff3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                    End If
                End If
                If seccion = 2 Then
                    resul &= "	<td width=17 class=""pestOn1"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                    resul &= "	<td width=1% class=""pestOn2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White""><b>Información General</b></a></td>"
                    resul &= "	<td class=""pestOn3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                Else
                    If tipo = "E" Then
                        resul &= "	<td width=17 class=""pestOff1Grande"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                        resul &= "	<td width=1% class=""pestOff2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White"">Información General</a></td>"
                        resul &= "	<td class=""pestOff3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                    End If

                End If
                If seccion = 3 Then
                    resul &= "	<td width=17 class=""pestOn1"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                    'resul &= "	<td width=1% class=""pestOn2"" nowrap><a href='" & subsec & "Listados/ListadoCaudalesFiltrados.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "'> <b>Resumen de Consumos</b></a></td>"
                    resul &= "	<td width=1% class=""pestOn2"" nowrap onclick=""javascript:document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><a href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "' style=""color:White""> <b>Informes</b></a></td>"
                    resul &= "	<td class=""pestOn3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                Else
                    'If tipo = "E" Then 'IPIM 28/08/2008: Para que los informes estén disponibles en todos los niveles
                    resul &= "	<td width=17 class=""pestOff1"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"
                    'resul &= "	<td width=1% class=""pestOff2"" nowrap><a href='" & subsec & "listados/ListadoCaudalesFiltrados.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "'>Resumen de Consumos</a></td>"
                    resul &= "	<td width=1% class=""pestOff2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><a href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "' style=""color:White"">Informes</a></td>"
                    resul &= "	<td class=""pestOff3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                    'End If
                End If


                If seccion = 4 Then
                    resul &= "	<td width=17 class=""pestOn1"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                    resul &= "	<td width=1% class=""pestOn2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'""><a href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L' style=""color:White""> <b>Detalle Lecturas</b></a></td>"
                    resul &= "	<td class=""pestOn3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                Else
                    If tipo = "E" Then
                        resul &= "	<td width=17 class=""pestOff1"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"
                        resul &= "	<td width=1% class=""pestOff2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'""><a href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L' style=""color:White"">  Detalle Lecturas</a></td>"
                        resul &= "	<td class=""pestOff3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                    End If
                End If

                If seccion = 5 Then
                    resul &= "	<td width=17 class=""pestOn1"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'""><img src=""" & subsec & ruta & "/relleno17.gif""></td>"
                    resul &= "	<td width=1% class=""pestOn2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'""><a href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V' style=""color:White""> <b>Visor</b></a></td>"
                    resul &= "	<td class=""pestOn3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                Else

                    resul &= "	<td width=17 class=""pestOff1"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'""><img src=""" & subsec & ruta & "/relleno8.gif""></td>"
                    resul &= "	<td width=1% class=""pestOff2"" nowrap style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'""><a href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V' style=""color:White""> Visor</a></td>"
                    resul &= "	<td class=""pestOff3"" style='cursor:hand' onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'""><img src=""" & subsec & ruta & "/relleno2.gif""></td>"
                End If
            End If

            resul &= "	<td style=""background-image:url(" & subsec & "images/pestBordeInf.gif)""><img src=""" & subsec & "images/relleno8.gif""></td>"
            resul &= "<td style=""background-image:url(" & subsec & "images/pestBordeInf.gif)"" align=""right"">"


            resul &= "</td>"
            resul &= ""
            resul &= "</tr>"
            resul &= "</table>"
            ' resul &= "<table width=100% cellspacing=0 cellpadding=0><tr><td style=""background-image:url(" & subsec & "images/gradBajoPest.jpg);height:11px;font-size:6px"">&nbsp;</td></tr></table>"
            resul &= "</td>"
            resul &= "</tr>"



            Return resul

        End Function

        Shared Function EnlacesMenuArbol(ByVal seccion As Integer, ByVal subsec As String, ByVal pagina As Page, ByVal idperfil As Integer, ByVal tipo As String, ByVal clavenodo As String, ByVal visor As String) As String

            Dim resul As String = ""
            Dim ruta As String = ""
            Dim cadena() As String
            If Directory.Exists(pagina.Server.MapPath("./images")) Then
                ruta = "images"
            Else
                ruta = "../images"
            End If

            cadena = Split(System.Configuration.ConfigurationManager.AppSettings("urlSICAH"), ",")

            ruta = "images"
            If visor = "S" Then
                If idperfil = 11 Or idperfil = 1 Or idperfil = 14 Then
                    If seccion = 0 Then
                        pagina.Session("EnlaceC") = 0
                        'pagina.Session("EnlaceP") = 1
                        'pagina.Session("EnlaceE") = 2
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" style=""color:White;"">  <b>Información General</b></a></td>"
                    Else
                        If tipo = "C" Then
                            resul &= "	<td style=""width: 170px; background: #263E53; color: white;cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" style=""color:White;""> Información General</a></td>"
                        End If
                    End If

                    If seccion = 1 Then
                        'pagina.Session("EnlaceC") = 0
                        pagina.Session("EnlaceP") = 1
                        'pagina.Session("EnlaceE") = 2

                        resul &= "	<td style=""width: 170px; background: #263E53; color: white;cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" style=""color:White;""> <b>Información General</b></a></td>"
                    Else
                        If tipo = "P" Then
                            resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" style=""color:White;""> Información General</a></td>"
                        End If
                    End If
                    If seccion = 2 Then
                        'pagina.Session("EnlaceC") = 0
                        'pagina.Session("EnlaceP") = 1
                        pagina.Session("EnlaceE") = 2

                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" style=""color:White;""><b>Información General</b></a></td>"
                    Else
                        If tipo = "E" Then
                            resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'""  nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" style=""color:White;"">Información General</a></td>"
                        End If

                    End If
                    If seccion = 3 Then
                        'pagina.Session("EnlaceC") = 3
                        'pagina.Session("EnlaceP") = 3
                        pagina.Session("EnlaceE") = 3
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> <b>Informes</b></a></td>"
                    Else
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;"">Informes</a></td>"
                    End If


                    If seccion = 4 Then
                        pagina.Session("EnlaceE") = 4
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'"" style=""color:White;""> <b> Lecturas</b></a></td>"
                    Else
                        If tipo = "E" Then
                            resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'"" style=""color:White;""> Lecturas</a></td>"
                        End If
                    End If

                    If seccion = 5 Then
                        pagina.Session("EnlaceC") = 5
                        pagina.Session("EnlaceP") = 5
                        If pagina.Session("EnlaceE") <> 4 Then
                            pagina.Session("EnlaceE") = 5
                        End If
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'"" style=""color:White;""> <b>Visor</b></a></td>"
                    Else
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'"" style=""color:White;""> Visor</a></td>"
                    End If

                    'EGB Si no Existe SeguimientoAdministrativo para Cauces o Puntos seleccionados no se visualiza la pestaña.
                    If seccion = 6 Then
                        'pagina.Session("Enlace" & pagina.Session("ClaveTipo")) = 6
                        If ExisteSeguimientoAdmininistrativo(pagina.Session("clavenodo"), tipo, pagina) = True Then
                            'pagina.Session("Enlace" & pagina.Session("ClaveTipo")) = 6
                            resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/SeguimientoAdministrativoDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/SeguimientoAdministrativoDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> <b>Seguimiento Administrativo</b></a></td>"
                        End If

                    Else
                        If ExisteSeguimientoAdmininistrativo(pagina.Session("clavenodo"), tipo, pagina) = True Then
                            resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:window.parent.document.location.href='" & subsec & "SICAH/SeguimientoAdministrativoDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/SeguimientoAdministrativoDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> Seguimiento Administrativo</a></td>"
                        End If

                    End If

                    'RDF
                    '17102008
                    'Visualización de la pestaña de Visor Topkapi
                    If seccion = 7 Then
                        '*********************************************************************************
                        '*********************************************************************************
                        '*********************************************************************************
                        'DESCOMENTAR CUANDO SE PONGA EN PRODUCCIÓN
                        '*********************************************************************************
                        '*********************************************************************************
                        '*********************************************************************************

                        If ExisteAccesoTopkapi(pagina.Session("clavenodo"), tipo, pagina) = True Then
                            resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/AccesoTopkapiDesdeArbol.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> <b>SCADA</b></a></td>"
                        Else
                            pagina.Session("EnlaceP") = 0
                        End If

                    Else
                        If tipo = "P" Then
                            If ExisteAccesoTopkapi(pagina.Session("clavenodo"), tipo, pagina) = True Then
                                resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/AccesoTopkapiDesdeArbol.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> SCADA</a></td>"
                            End If
                        End If


                        '    resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href=""javascript:window.parent.document.location.href='" & subsec & "PantallasVisor/infoPunto.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> Pruebas Ro</a></td>"
                        'Else
                        '    resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href=""javascript:window.parent.document.location.href='" & subsec & "PantallasVisor/infoPunto.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> Pruebas Ro</a></td>"
                    End If

                    'RDF
                    '28/10/2008
                    'Galería fotográfica
                    If seccion = 8 Then
                        If ExistenImagenes(pagina, pagina.Session("clavenodo"), tipo) = True Then
                            pagina.Session("EnlaceP") = 8
                            resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/Galeria.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> <b>Galería</b></a></td>"
                        Else
                            pagina.Session("EnlaceP") = 0
                        End If

                    Else
                        If tipo = "P" Then
                            If ExistenImagenes(pagina, pagina.Session("clavenodo"), tipo) = True Then
                                resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href=""javascript:window.parent.document.location.href='" & subsec & "SICAH/Galeria.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> Galería</a></td>"
                            End If
                        End If

                    End If


                End If


            Else

                If idperfil = 11 Or idperfil = 1 Or idperfil = 14 Then
                    If seccion = 0 Then
                        pagina.Session("EnlaceC") = 0
                        'pagina.Session("EnlaceP") = 1
                        'pagina.Session("EnlaceE") = 2

                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White;"">  <b>Información General</b></a></td>"
                    Else
                        If tipo = "C" Then
                            resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & subsec & "SICAH/InformacionGeneralC.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White;""> Información General</a></td>"

                        End If

                    End If

                    If seccion = 1 Then
                        'pagina.Session("EnlaceC") = 0
                        pagina.Session("EnlaceP") = 1
                        'pagina.Session("EnlaceE") = 2

                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White;""> <b>Información General</b></a></td>"
                    Else
                        If tipo = "P" Then
                            resul &= "	<td style=""width: 170px; background: #263E53; color: white;cursor:hand"" onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & subsec & "SICAH/InformacionGeneralP.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White;""> Información General</a></td>"
                        End If
                    End If
                    If seccion = 2 Then
                        'pagina.Session("EnlaceC") = 0
                        'pagina.Session("EnlaceP") = 1
                        pagina.Session("EnlaceE") = 2

                        resul &= "	<td style=""width: 170px; background: #263E53; color: white;cursor:hand"" onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White;""><b>Información General</b></a></td>"
                    Else
                        If tipo = "E" Then
                            resul &= "	<td style=""width: 170px; background: #263E53; color: white;cursor:hand"" onclick=""javascript:document.location.href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "'"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & subsec & "SICAH/InformacionGeneralE.aspx?clavenodo=" & pagina.Session("clavenodo") & "' style=""color:White;"">Información General</a></td>"
                        End If

                    End If
                    If seccion = 3 Then
                        'pagina.Session("EnlaceC") = 3
                        'pagina.Session("EnlaceP") = 3
                        pagina.Session("EnlaceE") = 3
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><a href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "' style=""color:White;""> <b>Informes</b></a></td>"
                    Else
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:document.location.href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><a href='" & subsec & "SICAH/PantallaInformes.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "' style=""color:White;"">Informes</a></td>"
                    End If


                    If seccion = 4 Then
                        pagina.Session("EnlaceE") = 4
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'""><a href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L' style=""color:White;""> <b> Lecturas</b></a></td>"
                    Else
                        If tipo = "E" Then
                            resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L'""><a href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=L' style=""color:White;""> Lecturas</a></td>"
                        End If
                    End If

                    If seccion = 5 Then
                        'pagina.Session("EnlaceC") = 5
                        'pagina.Session("EnlaceP") = 5
                        'pagina.Session("EnlaceE") = 5
                        pagina.Session("Enlace" & pagina.Session("ClaveTipo")) = 5
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'""><a href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V' style=""color:White;""> <b>Visor</b></a></td>"
                    Else
                        resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:document.location.href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V'""><a href='" & subsec & "SICAH/CaucePuntDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&nodoclave=" & pagina.Session("nodoclave") & "&LecturasVisor=V' style=""color:White;""> Visor</a></td>"
                    End If

                    'EGB Si no Existe SeguimientoAdministrativo para Cauces o Puntos seleccionados no se visualiza la pestaña.
                    If seccion = 6 Then
                        'pagina.Session("Enlace" & pagina.Session("ClaveTipo")) = 6                        
                        If ExisteSeguimientoAdmininistrativo(pagina.Session("clavenodo"), tipo, pagina) = True Then
                            'pagina.Session("Enlace" & pagina.Session("ClaveTipo")) = 6
                            resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:document.location.href='" & subsec & "SICAH/SeguimientoAdministrativoDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><a href='" & subsec & "SICAH/SeguimientoAdministrativoDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "' style=""color:White;""> <b>Seguimiento Administrativo</b></a></td>"
                        End If

                    Else
                        If ExisteSeguimientoAdmininistrativo(pagina.Session("clavenodo"), tipo, pagina) = True Then
                            resul &= "	<td style=""width: 170px; background: #263E53; cursor:hand"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap onclick=""javascript:document.location.href='" & subsec & "SICAH/SeguimientoAdministrativoDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'""><a href='" & subsec & "SICAH/SeguimientoAdministrativoDETALLE.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "' style=""color:White;"">Seguimiento Administrativo</a></td>"
                        End If

                    End If

                    'RDF
                    '17102008
                    'Visualización de la pestaña de Visor Topkapi
                    If seccion = 7 Then
                        '*********************************************************************************
                        '*********************************************************************************
                        '*********************************************************************************
                        'DESCOMENTAR CUANDO SE PONGA EN PRODUCCIÓN
                        '*********************************************************************************
                        '*********************************************************************************
                        '*********************************************************************************

                        If ExisteAccesoTopkapi(pagina.Session("clavenodo"), tipo, pagina) = True Then
                            'pagina.Session("EnlaceP") = 7
                            resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & subsec & "SICAH/AccesoTopkapiDesdeArbol.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "' style=""color:White;""> <b>SCADA</b></a></td>"
                            'Else
                            '   pagina.Session("EnlaceP") = 0
                        End If
                    Else
                        If tipo = "P" Then
                            If ExisteAccesoTopkapi(pagina.Session("clavenodo"), tipo, pagina) = True Then
                                resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & subsec & "SICAH/AccesoTopkapiDesdeArbol.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "' style=""color:White;""> SCADA</a></td>"
                            End If
                        End If
                    End If
                    '    resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href=""javascript:window.parent.document.location.href='" & subsec & "PantallasVisor/infoPunto.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> Pruebas Ro</a></td>"
                    'Else
                    '    resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href=""javascript:window.parent.document.location.href='" & subsec & "PantallasVisor/infoPunto.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "'"" style=""color:White;""> Pruebas Ro</a></td>"
                    'End If

                    'RDF
                    '28/10/2008
                    'Galería fotográfica
                    Dim SICA_IO As New SicaSegura.SICA_SysemIO

                    If seccion = 8 Then
                        If ExistenImagenes(pagina, pagina.Session("clavenodo"), pagina.Session("clavenodo")) = True Then ' SICA_IO.ExisteCarpetaEnServidor("C:\Inetpub\wwwroot\BaseFotografica\Images\" & pagina.Session("clavenodo")) = True Then
                            'If ExistenImagenes(pagina, pagina.Session("clavenodo"), tipo) = True Then
                            pagina.Session("EnlaceP") = 8
                            resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & subsec & "SICAH/Galeria.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "' style=""color:White;""> <b>Galería</b></a></td>"
                        Else
                            pagina.Session("EnlaceP") = 0
                        End If
                    Else
                        If tipo = "P" Then
                            'If ExistenImagenes(pagina, pagina.Session("clavenodo"), tipo) = True Then
                            If ExistenImagenes(pagina, pagina.Session("clavenodo"), pagina.Session("clavenodo")) = True Then 'SICA_IO.ExisteCarpetaEnServidor("C:\Inetpub\wwwroot\BaseFotografica\Images\" & pagina.Session("clavenodo")) = True Then
                                resul &= "	<td style=""width: 170px; background: #263E53"" onmouseout=""style.backgroundColor='#263E53'"" onmouseover=""style.backgroundColor='#0050AA'"" nowrap><a href='" & subsec & "SICAH/Galeria.aspx?nodotexto=" & pagina.Session("nodotexto") & "&clavenodo=" & pagina.Session("clavenodo") & "&claveTipo=" & pagina.Session("claveTipo") & "' style=""color:White;""> Galería</a></td>"
                            End If
                        End If

                    End If

                End If
            End If 'visor


            Return resul

        End Function

        ''' <summary>
        ''' Comprobamos si hay seguimientos administrativos para mostrar botón o no
        ''' </summary>
        ''' <param name="clavenodo"></param>
        ''' <param name="tiponodo"></param>
        ''' <param name="pagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function ExisteSeguimientoAdmininistrativo(ByVal clavenodo As String, ByVal tiponodo As String, ByVal pagina As Page) As Boolean
            'EGB 08/10/2008
            Dim valor As Boolean
            Dim sentenciaSel As String
            Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
            Dim daSeguimientosAdministrativos As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
            Dim dstSeguimientosAdministrativos As System.Data.DataSet = New System.Data.DataSet()
            Dim v_CodigoPVYCR() As String
            Dim CodigoPVYCR As String
            Dim v_CodigoCauce() As String
            Dim CodigoCauce As String


            'If conexion.State = Data.ConnectionState.Closed Then conexion.Open()
            utiles.Comprobar_Conexion_BD(pagina, conexion)
            'Verificamos si existen registros asociados para un Cauce en la tabla de SeguimientosAdministrativos

            Select Case tiponodo
                Case "C"
                    'SELECT filtrando por Cauce
                    v_CodigoCauce = Split(clavenodo)
                    CodigoCauce = v_CodigoCauce(0).ToString
                    sentenciaSel = "SELECT * FROM PVYCR_SeguimientoAdministrativo WHERE CodigoCauce='" & CodigoCauce & "'"
                Case "P"
                    v_CodigoPVYCR = Split(clavenodo)
                    CodigoPVYCR = v_CodigoPVYCR(0).ToString
                    sentenciaSel = "SELECT * FROM PVYCR_SeguimientoAdministrativo WHERE CodigoPVYCR='" & CodigoPVYCR & "'"
                Case Else
                    Return False

            End Select

            daSeguimientosAdministrativos.SelectCommand.CommandText = sentenciaSel
            daSeguimientosAdministrativos.Fill(dstSeguimientosAdministrativos, "TablaSeguimientosAdministrativos")

            valor = False
            If dstSeguimientosAdministrativos.Tables("TablaSeguimientosAdministrativos").Rows.Count > 0 Then
                valor = True
            End If

            Return valor
        End Function
        ''' <summary>
        ''' Funcion para comprobar si hay pestaña de SCADA. Comentada porque con el CAS no se puede acceder directamente a una página del SCADA
        ''' </summary>
        ''' <param name="clavenodo"></param>
        ''' <param name="tiponodo"></param>
        ''' <param name="pagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function ExisteAccesoTopkapi(ByVal clavenodo As String, ByVal tiponodo As String, ByVal pagina As Page) As Boolean
            'RDF 17/10/2008
            'Dim valor As Boolean
            'Dim sentenciaSel As String
            'Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
            'Dim daAccesos As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
            'Dim dstAccesos As System.Data.DataSet = New System.Data.DataSet()
            'Dim CodigoPVYCR As String
            'Dim v_CodigoPVYCR() As String


            ''If conexion.State = Data.ConnectionState.Closed Then conexion.Open()
            'utiles.Comprobar_Conexion_BD(pagina, conexion)
            ''Verificamos si existen registros asociados para un Cauce en la tabla de SeguimientosAdministrativos

            'Select Case tiponodo
            '    Case "P"
            '        v_CodigoPVYCR = Split(clavenodo)
            '        CodigoPVYCR = v_CodigoPVYCR(0).ToString
            '        sentenciaSel = "SELECT * FROM PVYCR_AccesosTopkapi WHERE CodigoPVYCR='" & CodigoPVYCR & "'"
            '    Case Else
            '        Return False

            'End Select

            'daAccesos.SelectCommand.CommandText = sentenciaSel
            'daAccesos.Fill(dstAccesos, "TablaAccesos")

            'valor = False
            'If dstAccesos.Tables("TablaAccesos").Rows.Count > 0 Then
            '    valor = True
            'End If

            ''Return valor
            Return False
        End Function
        Shared Function botonDesplegable(ByVal pagina As Page, ByVal id As String, ByVal texto As String, ByVal codigoJavascript As String, ByVal desplegable As Boolean)
            Dim resul As String = ""
            Dim ruta As String = ""
            If Directory.Exists(pagina.Server.MapPath("./images")) Then
                ruta = "./"
            ElseIf Directory.Exists(pagina.Server.MapPath("../images")) Then
                ruta = "../"
            End If
            Dim imagenDerecha As String = ""
            If desplegable Then
                imagenDerecha = "3.gif"
            Else : imagenDerecha = "4.gif"
            End If

            resul = "<div id='" & id & "' style='cursor:hand;cursor:pointer;' onclick='" & codigoJavascript & "' onmouseover=""document.getElementById('" & id & "imgoff').style.display='none';document.getElementById('" & id & "imgon').style.display='';document.getElementById('" & id & "imgon').disabled=true;"" onmouseout=""document.getElementById('" & id & "imgon').style.display='none';document.getElementById('" & id & "imgoff').style.display='';"">"
            resul &= "<table cellspacing='0' cellpadding='0' border='0' width='1'>"
            resul &= "<tr>"
            resul &= "<td><img src='" & ruta & "images/botonDesplegable/1.gif'></td>"
            resul &= "<td style='background-image:url(" & ruta & "images/botonDesplegable/2.gif);background-repeat:repeat-x;padding-right:12px;padding-left:5px' nowrap UNSELECTABLE='on'>" & texto & "</td>"
            resul &= "<td style='padding:0px'><img id='" & id & "imgoff' src='" & ruta & "images/botonDesplegable/" & imagenDerecha & "'>" & _
                    "<img id='" & id & "imgon' src='" & ruta & "images/botonDesplegable/" & imagenDerecha.Replace(".gif", "on.gif") & "' style='display:none'></td>"
            resul &= "</tr>"
            resul &= "</table>"
            resul &= "</div>"

            Return resul
        End Function


        ''' <summary>
        ''' Comprobación de la existencia de la carpeta de fotos para mostrar el botón de la galería
        ''' </summary>
        ''' <param name="pagina"></param>
        ''' <param name="clavenodo"></param>
        ''' <param name="tiponodo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function ExistenImagenes(ByVal pagina As Page, ByVal clavenodo As String, ByVal tiponodo As String) As Boolean

            'RDF 2/03/2009
            'Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
            'utiles.Comprobar_Conexion_BD(pagina, conexion)
            'RDF          
            'Fecha: 2/03/2009
            'Se comprueba si el punto tiene algún registro en la tabla PVYCR_Imagenes
            'Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
            'comando.CommandText = "SELECT count(*) FROM PVYCR_Imagenes WHERE codigoPVYCR='" & clavenodo & "'"
            'Dim Existe As Integer
            'Existe = comando.ExecuteScalar()
            'If Existe > 0 Then
            '    conexion.Close()
            '    Return True
            'Else
            '    conexion.Close()
            '    Return False
            'End If

            Dim ruta As String = "c:\inetpub\wwwroot\basefotografica\images\" & clavenodo

            ' Ahora, si no existe la carpeta la creo (jmolina4;01/04/2014)
            Dim carpeta As DirectoryInfo
            If System.IO.Directory.Exists(ruta) Then
                Return True
            Else
                Try
                    carpeta = New DirectoryInfo(ruta)
                    carpeta.Create()

                Catch ex As Exception

                End Try
                
                If System.IO.Directory.Exists(ruta) Then
                    Return True
                Else
                    Return False
                End If
            End If

        End Function
        ''' <summary>
        ''' Colocar la imagen de fuentecaputa en la presentación
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function ObtenerPresentacion() As String
            Dim presentacion As String

            'IPIM 16/06/2010: Lo quitamos pq quieren quitar la presentación
            'presentacion = "<table width=""100%"" align=""center"">"
            'presentacion &= "<tr>"
            'presentacion &= "<td style=""background-color:#E1DA93"">"
            'presentacion &= "&nbsp;"
            'presentacion &= "</td>"
            'presentacion &= "<td width=""1%"" align=""center"">"
            'presentacion &= "<object classid=""clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"" codebase=""http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0"" width=""700"" height=""500"" id=""pelicula"" align=""middle"">"
            'presentacion &= "<param name=""allowScriptAccess"" value=""sameDomain"" />"
            'presentacion &= "<param name=""movie"" value=""../images/pelicula.swf"" />"
            'presentacion &= "<param name=""quality"" value=""high"" />"
            'presentacion &= "<param name=""bgcolor"" value=""#ffffff"" />"
            'presentacion &= "<embed src=""../images/pelicula.swf"" quality=""high"" bgcolor=""#ffffff"" width=""700"" height=""500"" name=""pelicula"" align=""middle"" allowScriptAccess=""sameDomain"" type=""application/x-shockwave-flash"" pluginspage=""http://www.macromedia.com/go/getflashplayer"" />"
            'presentacion &= "</object>"

            'presentacion &= "</td>"
            'presentacion &= "<td style=""background-color:#E1DA93"">"
            'presentacion &= "&nbsp;"
            'presentacion &= "</td>"

            'presentacion &= "</tr>"
            'presentacion &= "</table> "

            presentacion = "<table width='100%' height='600px' border='0' align='center' cellpadding='0' cellspacing='0' style=""background-image: url(images/fondoPresentacion.jpg);" & _
                         "background-position: center center; background-repeat: no-repeat;"">"
            presentacion &= "<tr>"
            presentacion &= "<td align=""right"" valign=""top"" width=""100%"" height=""100%"">"
            presentacion &= "<img src=""images/textoSICA.png"">"
            presentacion &= "</td>"
            presentacion &= "</tr>"
            presentacion &= "</table> "
            Return presentacion
        End Function
        ''' <summary>
        ''' Generación del menú de Cabecera
        ''' </summary>
        ''' <param name="seccion"></param>
        ''' <param name="subsec"></param>
        ''' <param name="pagina"></param>
        ''' <param name="idperfil"></param>
        ''' <param name="idUsuario"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function pestanyasMenu(ByVal seccion As Integer, ByVal subsec As String, ByVal pagina As Page, ByVal idperfil As Integer, ByVal idUsuario As Integer) As String
            Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
            Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
            pagina.ClientScript.RegisterStartupScript(GetType(Page), "salida", "<script language=javascript>" & _
                                                                                "window.onbeforeunload = function(){" & _
                                                                                "var iX = window.document.body.offsetWidth - window.event.clientX ;" & _
                                                                                "var iY = window.event.clientY ;" & _
                                                                                "if (iX <=30 && iY < 0 )" & _
                                                                                "{" & _
                                                                                "    window.open('cerrarAplicacion.aspx','Cerrar', 'toolbar=no,menubar=no,top=200,left=250,height=1,width=1');" & _
                                                                                "}" & _
                                                                                "};" & _
                                                                                "</script>")

            Dim textoCabecera As String = cabecera(pagina)
            Dim eltexto, ruta As String


            '************************************** ncm para presentacion servidor 29/06/2010 ********************************************************
            'Dim idUsuario As Integer = 0
            Dim rdr As SqlDataReader
            Dim encontrado As Boolean = False
            'ncm 04/11/2008 comentado porque lo cambiamos por comprobar_conexion
            utiles.Comprobar_Conexion_BD(pagina, conexion, True)

            '**************** IPIM 10/02/2011 POR INCIDENCIA 254: 
            'comando.CommandText = "select u.*,j.id as idJerarquia, j.prefijo from tusuarios u join tcargos c on c.id=u.idcargo join tjerarquias j on j.id=c.idjerarquia where u.login=@login"
            'comando.Parameters.AddWithValue("@login", "sica")
            ''comando.Parameters.AddWithValue("@password", "a")
            'comando.Parameters.AddWithValue("@login", pagina.Session("loginUsuario"))
            'rdr = comando.ExecuteReader()
            'If rdr.Read() Then
            '    pagina.Session("usuarioReg") = rdr("id")
            '    pagina.Session("nombreUsuario") = rdr("nombre") & " " & rdr("Apellidos")
            '    pagina.Session("loginUsuario") = rdr("login")
            '    pagina.Session("idJerarquiaUsuario") = rdr("idjerarquia")
            '    pagina.Session("idCargoUsuario") = rdr("idCargo")
            '    pagina.Session("prefijo") = rdr("prefijo")
            '    pagina.Session("idPerfil") = rdr("idPerfil")
            'End If
            '**************** FIN IPIM 10/02/2011 POR INCIDENCIA 254: 

            '************************************** ncm para presentacion servidor 29/06/2010 ***************
            'If CInt("0" & pagina.Session("usuarioReg")) <= 0 Then
            '    If pagina.FindControl("txtLogin") Is Nothing Then
            '        pagina.Response.Redirect("index.aspx")
            '    End If
            '    eltexto = ""
            'Else

            If Directory.Exists(pagina.Server.MapPath("./images")) Then
                ruta = ""
            ElseIf Directory.Exists(pagina.Server.MapPath("./../images")) Then
                ruta = "../"
            Else
                ruta = "../../"
            End If
            eltexto = "<td align=""right"" style=""color:#A9A363; padding-right:2px;border-bottom: 3px solid #ececec;"" width=""12%"">" & pagina.Session("nombreUsuario") & " :<br> [<a href=""" & ruta & "abandon.aspx"" class=""cerrar"">cerrar sesión</a>]</td>"

            '  End If

            Dim clase As String
            Dim pag As Integer
            If pagina.Request.QueryString("pag") <> "" Then
                pag = pagina.Request.QueryString("pag")
            Else
                If Not pagina.Session("pag") Is Nothing Then
                    pag = pagina.Session("pag")
                Else
                    pag = 1
                End If
            End If

            pagina.Session("pag") = pag

            Dim estiloPest1 As String = "glink", estiloPest2 As String = "glink", estiloPest3 As String = "glink", estiloPest4 As String = "glink", estiloPest5 As String = "glink", estiloPest6 As String = "glink"

            Select Case pag
                Case 1
                    estiloPest1 = "glinkOn"
                Case 2
                    estiloPest2 = "glinkOn"
                Case 3
                    estiloPest3 = "glinkOn"
                Case 4
                    estiloPest4 = "glinkOn"
                Case 5
                    estiloPest5 = "glinkOn"
                Case 6
                    estiloPest6 = "glinkOn"
            End Select

            Dim resul As String

            If subsec = "../../" Then
                resul = "<link href=""../../stylesheet_opt.css"" type=""text/css"" rel=""stylesheet"" />" & _
                                  "<script src=""../../menu.js"" type=""text/javascript""></script>"
            Else
                resul = "<link href=""../stylesheet_opt.css"" type=""text/css"" rel=""stylesheet"" />" & _
                                  "<script src=""../menu.js"" type=""text/javascript""></script>"
            End If

            resul &= "<td style=""padding-top:0px"">"
            resul &= "<table cellspacing=0 cellpadding=0 width=100%><tr><td>"

            resul &= "<div class=""contenedorMenu"">" & _
                     "<div id=""globalNav"">	   " & _
                     "<ul id=""globalLink"" class=""sinregistro"">"

            resul &= "<li class=""primernivel""><a href='" & subsec & "SICAH/Agrupaciones_new.aspx?pag=5'  class='" & estiloPest5 & "'>CONTROL DE APROVECHAMIENTOS</a>" & _
                     "<ul>" & _
                     "<li><a href='" & subsec & "SICAH/Agrupaciones_new.aspx?pag=5'>APROVECHAMIENTOS</a></li>" & _
                     "<li><a href='" & subsec & "SICAH/Sistemas.aspx?pag=5'>ALTA Y MODIFICACIÓN</a></li>" & _
                     "</ul>" & _
                     "</li>"




            If idperfil <> 10 Then
                resul &= "<li class=""primernivel""><a href=""javascript:void(0)"" class='" & estiloPest2 & "'>INFORMACIÓN MANUAL</a>" & _
                     "<ul>" & _
                     "<li><a href='" & subsec & "SICAH/motores.aspx?pag=2'>CONDUCCIÓN FORZADA - VOLUMEN</a></li>" & _
                     "<li><a href='" & subsec & "SICAH/horometros.aspx?pag=2'>CONDUCCIÓN FORZADA - TIEMPO</a></li>" & _
                     "<li><a href='" & subsec & "SICAH/alimentacion.aspx?pag=2'>CONDUCCIÓN FORZADA - ENERGÍA</a></li>" & _
                     "<li><a href='" & subsec & "SICAH/acequias.aspx?pag=2'>LÁMINA LIBRE - CAUDAL</a></li>" & _
                     "</ul>" & _
                     "</li>" '& _
                '"<li class=""primernivel""><a href='" & subsec & "SICAH/AccesoTopkapi.aspx?pag=3' class='" & estiloPest3 & "'>INFORMACIÓN AUTOMÁTICA</a>" & _
                '"</li>"
            End If

            resul &= "<li class=""nivelPequenyo""><a href='" & subsec & "SICAH/index.aspx?pag=4' class='" & estiloPest4 & "'>MANTENIMIENTO</a>" & _
                     "</li>"

            If File.Exists(pagina.Server.MapPath("./accesoVisor.aspx")) Then
                resul &= "<li class=""nivelPequenyo""><a href=""javascript:void(0)"" class=""glink"">HERRAMIENTAS</a>" & _
                     "<ul>" & _
                     "<li><a href=""documentos/Manual de usuario.pdf"" target=""_blank"">MANUAL DE USUARIO</a></li>" & _
                     "<li><a href=""accesoVisor.aspx"" target=""_blank"">ACCESO GIS</a></li>" & _
                     "</ul>" & _
                     "</li>"
            ElseIf File.Exists(pagina.Server.MapPath("../accesoVisor.aspx")) Then
                resul &= "<li class=""nivelPequenyo""><a href=""javascript:void(0)"" class=""glink"">HERRAMIENTAS</a>" & _
                     "<ul>" & _
                     "<li><a href=""../documentos/Manual de usuario.pdf"" target=""_blank"">MANUAL DE USUARIO</a></li>" & _
                     "<li><a href=""../accesoVisor.aspx"" target=""_blank"">ACCESO GIS</a></li>" & _
                     "</ul>" & _
                     "</li>"
            ElseIf File.Exists(pagina.Server.MapPath("../../accesoVisor.aspx")) Then
                resul &= "<li class=""nivelPequenyo""><a href=""javascript:void(0)"" class=""glink"">HERRAMIENTAS</a>" & _
                     "<ul>" & _
                     "<li><a href=""../../documentos/Manual de usuario.pdf"" target=""_blank"">MANUAL DE USUARIO</a></li>" & _
                     "<li><a href=""../../accesoVisor.aspx"" target=""_blank"">ACCESO GIS</a></li>" & _
                     "</ul>" & _
                     "</li>"

                '' "<li><a href=""../accesoVisorTOPKAPI.aspx"" target=""_blank"">ACCESO SCADA</a></li>" & _
            End If

            resul &= "<li class=""primernivel""><a href='" & subsec & "SICAH/caucepuntMAIN.aspx?nodobusqueda=n&valor=0&pag=1' class='" & estiloPest1 & "'>CONTROL DE CAUCES Y PUNTOS</a></li>"


            'resul &= "<li class=""nivelPequenyo""><a href='" & subsec & "SICAH/Agrupaciones_new.aspx?pag=5' class=""glink"">AGRUPACIONES</a>" & _
            '         "</li>"

            ' jmolina4. 21/02/2014. Añado nueva pestaña: Petición de informes
            'If pagina.Session("loginUsuario") = "atsica8" Then
            resul &= "<li class=""nivelPequenyo""><a href='" & subsec & "SICAH/PetiInfo/PeticionInformacion.aspx?pag=6' class='" & estiloPest6 & "'>SOLICITUDES</a>" & _
                     "</li>"
            'End If

            resul &= "<li class=""nivelPequenyo""><a href='" & ruta & "abandon.aspx' class=""glink"">CERRAR SESIÓN</a>" & _
                     "</li>" & _
                     "</ul>" & _
                     "</div>" & _
                     "</div>" & _
                     "</td></tr></table></td>"

            conexion.Close()
            Return resul

        End Function
        ''' <summary>
        ''' Generación del menú de cabecera desde SICAH
        ''' </summary>
        ''' <param name="seccion"></param>
        ''' <param name="subsec"></param>
        ''' <param name="pagina"></param>
        ''' <param name="idperfil"></param>
        ''' <param name="idUsuario"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function pestanyasMenuDentroSICAH(ByVal seccion As Integer, ByVal subsec As String, ByVal pagina As Page, ByVal idperfil As Integer, ByVal idUsuario As Integer) As String
            Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
            Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
            pagina.ClientScript.RegisterStartupScript(GetType(Page), "salida", "<script language=javascript>" & _
                                                                                "window.onbeforeunload = function(){" & _
                                                                                "var iX = window.document.body.offsetWidth - window.event.clientX ;" & _
                                                                                "var iY = window.event.clientY ;" & _
                                                                                "if (iX <=30 && iY < 0 )" & _
                                                                                "{" & _
                                                                                "    window.open('cerrarAplicacion.aspx','Cerrar', 'toolbar=no,menubar=no,top=200,left=250,height=1,width=1');" & _
                                                                                "}" & _
                                                                                "};" & _
                                                                                "</script>")

            Dim textoCabecera As String = cabecera(pagina)
            Dim eltexto, ruta As String
            Dim rdr As SqlDataReader
            Dim encontrado As Boolean = False
            utiles.Comprobar_Conexion_BD(pagina, conexion, True)

            If Directory.Exists(pagina.Server.MapPath("./images")) Then
                ruta = "../"
            Else
                ruta = "../../"
            End If
            eltexto = "<td align=""right"" style=""color:#A9A363; padding-right:2px;border-bottom: 3px solid #ececec;"" width=""12%"">" & pagina.Session("nombreUsuario") & " :<br> [<a href=""" & ruta & "abandon.aspx"" class=""cerrar"">cerrar sesión</a>]</td>"

            '  End If

            Dim clase As String
            Dim pag As Integer
            If pagina.Request.QueryString("pag") <> "" Then
                pag = pagina.Request.QueryString("pag")
            Else
                If Not pagina.Session("pag") Is Nothing Then
                    pag = pagina.Session("pag")
                Else
                    pag = 1
                End If
            End If

            pagina.Session("pag") = pag

            Dim estiloPest1 As String = "glink", estiloPest2 As String = "glink", estiloPest3 As String = "glink", estiloPest4 As String = "glink", estiloPest5 As String = "glink", estiloPest6 As String = "glink"

            Select Case pag
                Case 1
                    estiloPest1 = "glinkOn"
                Case 2
                    estiloPest2 = "glinkOn"
                Case 3
                    estiloPest3 = "glinkOn"
                Case 4
                    estiloPest4 = "glinkOn"
                Case 5
                    estiloPest5 = "glinkOn"
                Case 6
                    estiloPest6 = "glinkOn"
            End Select

            Dim resul As String = "<link href=""../../stylesheet_opt.css"" type=""text/css"" rel=""stylesheet"" />" & _
                                  "<script src=""../../menu.js"" type=""text/javascript""></script>"
            resul &= "<td style=""padding-top:0px"">"
            resul &= "<table cellspacing=0 cellpadding=0 width=100%><tr><td>"

            resul &= "<div class=""contenedorMenu"">" & _
                     "<div id=""globalNav"">	   " & _
                     "<ul id=""globalLink"" class=""sinregistro"">" & _
                     "<li class=""primernivel""><a href='" & subsec & "SICAH/caucepuntMAIN.aspx?nodobusqueda=n&valor=0&pag=1' class='" & estiloPest1 & "'>CONTROL DE APROVECHAMIENTOS</a></li>"

            If idperfil <> 10 Then
                resul &= "<li class=""primernivel""><a href=""javascript:void(0)"" class='" & estiloPest2 & "'>INFORMACIÓN MANUAL</a>" & _
                     "<ul>" & _
                     "<li><a href='" & subsec & "../SICAH/motores.aspx?pag=2'>CONDUCCIÓN FORZADA - VOLUMEN</a></li>" & _
                     "<li><a href='" & subsec & "../SICAH/horometros.aspx?pag=2'>CONDUCCIÓN FORZADA - TIEMPO</a></li>" & _
                     "<li><a href='" & subsec & "../SICAH/alimentacion.aspx?pag=2'>CONDUCCIÓN FORZADA - ENERGÍA</a></li>" & _
                     "<li><a href='" & subsec & "../SICAH/acequias.aspx?pag=2'>LÁMINA LIBRE - CAUDAL</a></li>" & _
                     "</ul>" & _
                     "</li>" '& _
                '"<li class=""primernivel""><a href='" & subsec & "SICAH/AccesoTopkapi.aspx?pag=3' class='" & estiloPest3 & "'>INFORMACIÓN AUTOMÁTICA</a>" & _
                '"</li>"
            End If

            resul &= "<li class=""nivelPequenyo""><a href='" & subsec & "../SICAH/index.aspx?pag=4' class='" & estiloPest4 & "'>MANTENIMIENTO</a>" & _
                     "</li>"

            If File.Exists(pagina.Server.MapPath("./accesoVisor.aspx")) Then
                resul &= "<li class=""nivelPequenyo""><a href=""javascript:void(0)"" class=""glink"">HERRAMIENTAS</a>" & _
                     "<ul>" & _
                     "<li><a href=""../documentos/Manual de usuario.pdf"" target=""_blank"">MANUAL DE USUARIO</a></li>" & _
                     "<li><a href=""../accesoVisor.aspx"" target=""_blank"">ACCESO GIS</a></li>" & _
                     "</ul>" & _
                     "</li>"
            ElseIf File.Exists(pagina.Server.MapPath("../../accesoVisor.aspx")) Then
                resul &= "<li class=""nivelPequenyo""><a href=""javascript:void(0)"" class=""glink"">HERRAMIENTAS</a>" & _
                     "<ul>" & _
                     "<li><a href=""../../documentos/Manual de usuario.pdf"" target=""_blank"">MANUAL DE USUARIO</a></li>" & _
                     "<li><a href=""../../accesoVisor.aspx"" target=""_blank"">ACCESO GIS</a></li>" & _
                     "</ul>" & _
                     "</li>"
                '' "<li><a href=""../accesoVisorTOPKAPI.aspx"" target=""_blank"">ACCESO SCADA</a></li>" & _
            End If

            resul &= "<li class=""nivelPequenyo""><a href='" & subsec & "SICAH/Agrupaciones_new.aspx?pag=5'  class='" & estiloPest5 & "'>AGRUPACIONES</a>" & _
                     "<ul>" & _
                     "<li><a href='" & subsec & "../SICAH/Agrupaciones_new.aspx?pag=5'>SISTEMAS</a></li>" & _
                     "<li><a href='" & subsec & "../SICAH/Sistemas.aspx?pag=5'>ALTA Y MODIFICACIÓN</a></li>" & _
                     "</ul>" & _
                     "</li>"

            'resul &= "<li class=""nivelPequenyo""><a href='" & subsec & "SICAH/Agrupaciones_new.aspx?pag=5' class=""glink"">AGRUPACIONES</a>" & _
            '         "</li>"

            ' jmolina4. 21/02/2014. Añado nueva pestaña: Petición de informes
            'If pagina.Session("loginUsuario") = "atsica8" Then
            resul &= "<li class=""nivelPequenyo""><a href='" & subsec & "../SICAH/PetiInfo/PeticionInformacion.aspx?pag=6' class='" & estiloPest6 & "'>SOLICITUDES</a>" & _
                     "</li>"
            'End If

            resul &= "<li class=""nivelPequenyo""><a href='" & ruta & "../abandon.aspx' class=""glink"">CERRAR SESIÓN</a>" & _
                     "</li>" & _
                     "</ul>" & _
                     "</div>" & _
                     "</div>" & _
                     "</td></tr></table></td>"

            conexion.Close()
            Return resul

        End Function


    End Class

End Namespace
