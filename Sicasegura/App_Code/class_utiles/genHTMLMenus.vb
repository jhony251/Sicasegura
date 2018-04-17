Imports Microsoft.VisualBasic
Imports System.Data

Public Class genHTMLMenus

    Shared Function generaMenuTareas() As String
        Dim resul As String = """"""
        resul = _
        "<table cellpadding=5 cellspacing=5>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""tipos.aspx?nuevo=yes"" class=""enlaces"">Nuevo Tipo</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""tipos.aspx"" class=""enlaces"">Mantenimiento Tipos</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""urgencias.aspx?nuevo=yes"" class=""enlaces"">Nueva Urgencia</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""urgencias.aspx"" class=""enlaces"">Mantenimiento Urgencias</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""estados.aspx"" class=""enlaces"">Mantenimiento Estados</a></td></tr>" & _
        "</table>"

        'Estos puntos existían también con la versión anterior, al principio del menú:
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""tareas.aspx?nuevo=yes"" class=""enlaces"">Nueva Tarea</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""tareas.aspx"" class=""enlaces"">Mantenimiento Tareas</a></td></tr>" & _
        '"    <tr><td><br /></td></tr>" & _

        Return resul
    End Function


    Shared Function generaMenuInformes() As String
        Dim resul As String = """"""
        resul = _
        "<table cellpadding=5 cellspacing=5>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""plantillas.aspx"" class=""enlaces"">Mantenimiento de Plantillas</a></td></tr>" & _
        "    <tr><td><br /></td></tr>" & _
        "</table>"

        Return resul

    End Function

    Shared Function generaMenuInformesGestion() As String
        Dim resul As String = """"""
		resul = _
		"<table cellpadding=5 cellspacing=5>" & _
		"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""filtroInforme.aspx?t=gral"" class=""enlaces"">Informe General de Tareas</a></td></tr>" & _
		"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""filtroInforme.aspx?t=gflotas"" class=""enlaces"">Informe de Gestión de Flotas</a></td></tr>" & _
		"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""filtroInforme.aspx?t=rgflotas"" class=""enlaces"">Resumen Gestión de Flotas</a></td></tr>" & _
		"    <tr><td><br /></td></tr>" & _
		"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""verLogErrores.aspx"" class=""enlaces"">Ver Log de Errores</a></td></tr>" & _
		"    <tr><td><br /></td></tr>" & _
		"</table>"

        Return resul

    End Function

    Shared Function generaMenuControlFlotas() As String
        Dim resul As String = """"""
        resul = _
        "<table cellpadding=5 cellspacing=5>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""filtroInforme.aspx"" class=""enlaces""></a></td></tr>" & _
        "    <tr><td><br /></td></tr>" & _
        "</table>"

        Return resul

    End Function

    Shared Function generaMenuIncidencias() As String
        Dim resul As String = """"""
        resul = _
        "<table cellpadding=5 cellspacing=5>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""incidencias.aspx"" class=""enlaces"">Gestión de Incidencias</a></td></tr>" & _
        "    <tr><td><br /></td></tr>" & _
        "</table>"

        Return resul

    End Function

    Shared Function generaMenuUtilidades() As String
        Dim resul As String = """"""
        resul = _
        "<table cellpadding=5 cellspacing=5>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""conversorSHP.aspx"" class=""enlaces"">Conversor de SHPs</a></td></tr>" & _
        "    <tr><td><br /></td></tr>" & _
        "</table>"

        Return resul

    End Function
    
    Shared Function generaMenuMtoArbol(ByVal tipo As String, ByVal strDescripcion As String, ByVal strQuery As String) As String
        Dim resul As String = ""
        Dim strTipo As String
        Select Case tipo
            Case "C"
                strTipo = "Cauce"
                'resul = _
                '"<table>" & _
                '"    <tr><td><img src=""../images/Nuevo.gif"" align=middle border=0 />&nbsp;</td><td><a href=""Cauces.aspx?asociacion=yes&nuevo=yes&" & strQuery & """ class=""enlaces"">Nuevo Cauce</a></td></tr>" & _
                '"    <tr><td><img src=""../images/Edit.gif"" align=middle border=0 />&nbsp;</td><td><a href=""Cauces.aspx?asociacion=yes&" & strQuery & """ class=""enlaces"">Editar Cauce <b>" & strDescripcion & "</b></a></td></tr>" & _
                'resul = "<table>" & "<tr><td><img src=""../images/Asociar.gif"" align=middle border=0 />&nbsp;</td><td><a href=""Puntos.aspx"" class=""enlaces"">Asociar Puntos al Cauce <b>" & strDescripcion & "</b></a></td></tr>" & _
                '"    <tr><td><br /></td></tr>" & _
                '"</table>"
                resul = "<table>" & "<tr><td><img src=""../images/Asociar.gif"" align=middle border=0 />&nbsp;</td><td>Asociar Puntos al Cauce <b>[" & strDescripcion & "]</b></a></td></tr>" & _
                "    <tr><td><br /></td></tr>" & _
                "</table>"


            Case "P" : strTipo = "Punto"
                'resul = _
                '"<table>" & _
                '"    <tr><td><img src=""../images/Nuevo.gif"" align=middle border=0 />&nbsp;<a href=""puntos.aspx?asociacion=yes&nuevo=yes"" class=""enlaces"">Nuevo Punto</a></td></tr>" & _
                '"    <tr><td><img src=""../images/Edit.gif"" align=middle border=0 />&nbsp;<a href=""puntos.aspx?asociacion=yes&" & strQuery & """ class=""enlaces""> Editar Punto <b>" & strDescripcion & "</b></a></td></tr>" & _
                'resul = "<table>" & "<tr><td><img src=""../images/Asociar.gif"" align=middle border=0 />&nbsp;<a href=""ElementosMedida.aspx"" class=""enlaces""> Asociar Elementos de Medida <b>" & strDescripcion & "</b></a></td></tr>" & _
                '"    <tr><td><br /></td></tr>" & _
                '"</table>"
                'resul = "<table>" & "<tr><td><img src=""../images/Asociar.gif"" align=middle border=0 />&nbsp;<a href=""ElementosMedida.aspx"" class=""enlaces""> Asociar Elementos de Medida <b>" & strDescripcion & "</b></a></td></tr>" & _
                '"    <tr><td><br /></td></tr>" & _
                '"</table>"
                resul = "<table>" & "<tr><td><img src=""../images/Asociar.gif"" align=middle border=0 />&nbsp; Asociar Elementos de Medida al Punto <b>[" & strDescripcion & "]</b></a></td></tr>" & _
               "    <tr><td><br /></td></tr>" & _
               "</table>"
            Case "E" : strTipo = "Elemento de Medida"
                'resul = _
                '"<table>" & _
                'resul = "<table>" &<tr><td>No existen operaciones </td><tr>" & _
                '"    <tr><td><img src=""../images/Nuevo.gif"" align=middle border=0 />&nbsp;<a href=""elementosmedida.aspx?asociacion=yes&nuevo=yes"" class=""enlaces"">Nuevo Elemento de Medida</a></td></tr>" & _
                '"    <tr><td><img src=""../images/Edit.gif"" align=middle border=0 />&nbsp;<a href=""elementosmedida.aspx?asociacion=yes& " & strQuery & """ class=""enlaces""> Editar Elemento de Medida <b>" & strDescripcion & "</b></a></td></tr>" & _
                '"    <tr><td><img src=""../images/Asociar.gif"" align=middle border=0 />&nbsp;<a href=""elementosmedida.aspx"" class=""enlaces""> Eliminar Elemento de Medida <b>" & strDescripcion & "</b></a></td></tr>" & _
                'resul = "<table>" & "<tr><td><br /></td></tr>" & _
                '"</table>"

        End Select



        Return resul
    End Function





    ''' <summary>
    ''' Funcion que genera el menú de mantenimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function generaMenuMtoSica() As String
        'EGB 19/08/2008 Agregamos el mantenimiento de Curvas de Acequias
        Dim resul As String = """"""

        resul = _
        "<table cellpadding=6 cellspacing=6>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Cauces.aspx"" class=""enlaces"">Mantenimiento Cauces</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Puntos.aspx"" class=""enlaces"">Mantenimiento Puntos</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""ElementosMedida.aspx"" class=""enlaces"">Mantenimiento Elementos Medida</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""contadores.aspx"" class=""enlaces"">Mantenimiento Contadores</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""motobombas.aspx"" class=""enlaces"">Mantenimiento Motobombas</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""CurvasAcequiasMAIN.aspx"" class=""enlaces"">Mantenimiento Curvas Acequias</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""BoletinGuarderia.aspx?numref=0"" class=""enlaces"">Mantenimiento Boletines de Guardería</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""SeguimientoAdministrativo.aspx"" class=""enlaces"">Mantenimiento Seguimientos Administrativos</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""UsuariosDeSistemas.aspx"" class=""enlaces"">Mantenimiento Usuarios Agrupaciones</a></td></tr>" & _
                "    <tr><td><br /></td></tr>" & _
        "</table>"
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Agrupaciones.aspx"" class=""enlaces"">Mantenimiento Agrupaciones</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""RelacionAgrupaciones.aspx"" class=""enlaces"">Mantenimiento Relación entre Agrupaciones</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Calculos.aspx"" class=""enlaces"">Mantenimiento Cálculos</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""CalculosRelacionAgrupaciones.aspx"" class=""enlaces"">Mantenimiento Calculos y Agrupaciones</a></td></tr>" & _


        Return resul
    End Function
    'ncm comentada por ncm el 26/01/2010 porque no se utiliza
    'Shared Function generaMenuMtoSica(ByVal incluir As Boolean) As String
    '    'Funcion Sobrecargada de generaMenuMtoSica : incluye parametro para activacion o desactivacion del menu
    '    'EGB. Se incluye para los mantenimientos, y para mantener la compatibilidad con el resto de ocurrencias de la funcion sin parametro
    '    'EGB 19/08/2008 Agregamos el mantenimiento de Curvas de Acequias
    '    Dim resul As String = ""
    '    If incluir = True Then

    '        resul = _
    '        "<td colspan=""5""valign=""top"" style=""padding-top: 20px;width:20%"">" & _
    '        "<table cellpadding=6 cellspacing=6>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Cauces.aspx"" class=""enlaces"">Mantenimiento Cauces</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Puntos.aspx"" class=""enlaces"">Mantenimiento Puntos</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""ElementosMedida.aspx"" class=""enlaces"">Mantenimiento Elementos Medida</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""contadores.aspx"" class=""enlaces"">Mantenimiento Contadores</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""motobombas.aspx"" class=""enlaces"">Mantenimiento Motobombas</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""CurvasAcequiasMAIN.aspx"" class=""enlaces"">Mantenimiento Curvas Acequias</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""BoletinGuarderia.aspx?numref=0"" class=""enlaces"">Mantenimiento Boletines de Guardería</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""SeguimientoAdministrativo.aspx"" class=""enlaces"">Mantenimiento Seguimientos Administrativos</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Agrupaciones.aspx"" class=""enlaces"">Mantenimiento Agrupaciones</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""RelacionAgrupaciones.aspx"" class=""enlaces"">Mantenimiento Relación entre Agrupaciones</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Calculos.aspx"" class=""enlaces"">Mantenimiento Cálculos</a></td></tr>" & _
    '        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""CalculosRelacionAgrupaciones.aspx"" class=""enlaces"">Mantenimiento Calculos y Agrupaciones</a></td></tr>" & _
    '        "    <tr><td><br /></td></tr>" & _
    '        "</table>" & _
    '        "</td>" & _
    '        "<td style=""background-image:url(../images/dot2.gif);width:6px""></td>"

    '    End If

    '    Return resul
    'End Function
    ''' <summary>
    ''' Funcion que genera el menú de mantenimiento en base a perfil
    ''' </summary>
    ''' <param name="idperfil"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function generaMenuMtoSica(ByVal idperfil As Integer) As String
        'EGB 19/08/2008 Agregamos el mantenimiento de Curvas de Acequias
        Dim resul As String = """"""

        resul = _
        "<table cellpadding=6 cellspacing=6>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Cauces.aspx"" class=""enlaces"">Mantenimiento Cauces</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Puntos.aspx"" class=""enlaces"">Mantenimiento Puntos</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""ElementosMedida.aspx"" class=""enlaces"">Mantenimiento Elementos Medida</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""contadores.aspx"" class=""enlaces"">Mantenimiento Contadores</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""motobombas.aspx"" class=""enlaces"">Mantenimiento Motobombas</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""CurvasAcequiasMAIN.aspx"" class=""enlaces"">Mantenimiento Curvas Acequias</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""BoletinGuarderia.aspx?numref=0"" class=""enlaces"">Mantenimiento Boletines de Guardería</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""SeguimientoAdministrativo.aspx"" class=""enlaces"">Mantenimiento Seguimientos Administrativos</a> <a href=""./Seguimientoadmin/SeguimientoAdministrativo.aspx"" class=""enlaces"">·</a> </td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""UsuariosDeSistemas.aspx"" class=""enlaces"">Mantenimiento Usuarios Agrupaciones</a></td></tr>"
        '& _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Agrupaciones.aspx"" class=""enlaces"">Mantenimiento Agrupaciones</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""RelacionAgrupaciones.aspx"" class=""enlaces"">Mantenimiento Relación entre Agrupaciones</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Calculos.aspx"" class=""enlaces"">Mantenimiento Cálculos</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""CalculosRelacionAgrupaciones.aspx"" class=""enlaces"">Mantenimiento Calculos y Agrupaciones</a></td></tr>"

        If idperfil = 1 Or idperfil = 14 Then
      resul = resul & "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""log.aspx"" class=""enlaces"">Consulta Log</a></td></tr>"
      resul = resul & "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""ActualizaCadPtos.aspx"" class=""enlaces"">Actualiza Caducidad Puntos</a></td></tr>"
        End If

        resul = resul & "    <tr><td><br /></td></tr>" & _
        "</table>"

        Return resul
    End Function
    ''' <summary>
    ''' Funcion que genera el menu de manteniemineto desde la carpeta SICAH
    ''' </summary>
    ''' <param name="idperfil"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function generaMenuMtoSicaDesdeSICAH(ByVal idperfil As Integer) As String
        'EGB 19/08/2008 Agregamos el mantenimiento de Curvas de Acequias
        Dim resul As String = """"""

        resul = _
        "<table cellpadding=6 cellspacing=6>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""../Cauces.aspx"" class=""enlaces"">Mantenimiento Cauces</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""../Puntos.aspx"" class=""enlaces"">Mantenimiento Puntos</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""../ElementosMedida.aspx"" class=""enlaces"">Mantenimiento Elementos Medida</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""../contadores.aspx"" class=""enlaces"">Mantenimiento Contadores</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""../motobombas.aspx"" class=""enlaces"">Mantenimiento Motobombas</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""../CurvasAcequiasMAIN.aspx"" class=""enlaces"">Mantenimiento Curvas Acequias</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""../BoletinGuarderia.aspx?numref=0"" class=""enlaces"">Mantenimiento Boletines de Guardería</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""./SeguimientoAdministrativo.aspx"" class=""enlaces"">Mantenimiento Seguimientos Administrativos</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""../UsuariosDeSistemas.aspx"" class=""enlaces"">Mantenimiento Usuarios Agrupaciones</a></td></tr>"
        '& _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Agrupaciones.aspx"" class=""enlaces"">Mantenimiento Agrupaciones</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""RelacionAgrupaciones.aspx"" class=""enlaces"">Mantenimiento Relación entre Agrupaciones</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Calculos.aspx"" class=""enlaces"">Mantenimiento Cálculos</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""CalculosRelacionAgrupaciones.aspx"" class=""enlaces"">Mantenimiento Calculos y Agrupaciones</a></td></tr>"

        If idperfil = 1 Or idperfil = 14 Then
            resul = resul & "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""../log.aspx"" class=""enlaces"">Consulta Log</a></td></tr>"
            resul = resul & "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""../ActualizaCadPtos.aspx"" class=""enlaces"">Actualiza Caducidad Puntos</a></td></tr>"
        End If

        resul = resul & "    <tr><td><br /></td></tr>" & _
        "</table>"

        Return resul
    End Function
    ''' <summary>
    ''' Funcion que genera el menú mantenimiento SICA en base al fichero de acualizacion de puntos.
    ''' </summary>
    ''' <param name="idperfil"></param>
    ''' <param name="estadoActualizacionXML"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function generaMenuMtoSica(ByVal idperfil As Integer, ByVal estadoActualizacionXML As String) As String
        'Funcion Sobrecargada de generaMenuMtoSica : incluye parametro que identifica el nivel de seguridad
        'EGB. Se incluye para la visualización de opciones de menu dependientes del nivel de seguridad del usuario
        'EGB 19/08/2008 Agregamos el mantenimiento de Curvas de Acequias
        Dim resul As String = """"""
        'Dim strProceso As String = ""

        resul = _
        "<td colspan=""5""valign=""top"" style=""padding-top: 20px;width:20%"">" & _
        "<table cellpadding=6 cellspacing=6>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Cauces.aspx"" class=""enlaces"">Mantenimiento Cauces</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Puntos.aspx"" class=""enlaces"">Mantenimiento Puntos</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""ElementosMedida.aspx"" class=""enlaces"">Mantenimiento Elementos Medida</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""contadores.aspx"" class=""enlaces"">Mantenimiento Contadores</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""motobombas.aspx"" class=""enlaces"">Mantenimiento Motobombas</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""CurvasAcequiasMAIN.aspx"" class=""enlaces"">Mantenimiento Curvas Acequias</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""BoletinGuarderia.aspx?numref=0"" class=""enlaces"">Mantenimiento Boletines de Guardería</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""SeguimientoAdministrativo.aspx"" class=""enlaces"">Mantenimiento Seguimientos Administrativos</a></td></tr>" & _
        "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""UsuariosDeSistemas.aspx"" class=""enlaces"">Mantenimiento Usuarios Agrupaciones</a></td></tr>"
        '& _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Agrupaciones.aspx"" class=""enlaces"">Mantenimiento Agrupaciones</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""RelacionAgrupaciones.aspx"" class=""enlaces"">Mantenimiento Relación entre Agrupaciones</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""Calculos.aspx"" class=""enlaces"">Mantenimiento Cálculos</a></td></tr>" & _
        '"    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""CalculosRelacionAgrupaciones.aspx"" class=""enlaces"">Mantenimiento Calculos y Agrupaciones</a></td></tr>"


    'NCM 29/04/2010 COMENTADO AL PONER EL ARBOL EXT
    'If idperfil = 1 Or idperfil = 14 Then
    '    If estadoActualizacionXML = "NOK" Then
    '        resul = resul & _
    '        "    <tr><td><img src=""../SICAH/images/Proceso_Warning.gif"" align=middle border=0 />&nbsp;<a href=""ActualizadorXMLArbolAsync.aspx"" class=""enlaces"">Regenerar Arbol de Cauces</a></td></tr>"

    '    Else
    '        resul = resul & _
    '       "    <tr><td><img src=""../SICAH/images/Proceso_OK.gif"" align=middle border=0 />&nbsp;<a href=""ActualizadorXMLArbolAsync.aspx"" class=""enlaces"">Regenerar Arbol de Cauces</a></td></tr>"

    '    End If

    'End If
        If idperfil = 1 Or idperfil = 14 Then
      resul = resul & "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""log.aspx"" class=""enlaces"">Consulta log</a></td></tr>"
      resul = resul & "    <tr><td><img src=""../images/puntoMenu.gif"" align=middle border=0 />&nbsp;<a href=""ActualizaCadPtos.aspx"" class=""enlaces"">Actualiza Caducidad Puntos</a></td></tr>"
        End If
        resul = resul & _
        "    <tr><td><br /></td></tr>" & _
            "</table>" & _
            "</td>" & _
            "<td style=""background-image:url(../images/dot2.gif);width:6px""></td>"


        Return resul
    End Function
End Class
