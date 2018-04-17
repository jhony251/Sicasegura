Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial

Partial Class jerarquiaUsuarios
    Inherits System.Web.UI.UserControl

    Dim conexion As SqlConnection
    Dim strdsn As String = ConfigurationSettings.AppSettings("dsn")
    Dim daJerarquia, daOperaciones, daResponsables, daEstados, daResponsable As SqlDataAdapter
    Dim dstJerarquia, dstOperaciones, dstResponsables, dstEstados, dstResponsable As DataSet
    Public ruta As String

    Protected Sub TreeView2_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView2.SelectedNodeChanged
        If InStr(TreeView2.SelectedNode.Text, "Asignar") > 0 Then
            If InStr(TreeView2.SelectedNode.Text, "color:red;") > 0 Then
                Page.ClientScript.RegisterStartupScript(Page.GetType, "MostrarCapaArbolJerarquico", "<script language=javascript>alert('No se puede reasignar una tarea a un funcionario');mostrarCapa();</script>")
                Exit Sub
            End If
            If lblActualizarDatos.Text = "True" Then
                Dim comando As SqlCommand = New SqlCommand("", conexion)
                Dim comandoLog As SqlCommand = New SqlCommand("", conexion)
                Dim comandoEstado As SqlCommand = New SqlCommand("", conexion)
                Dim sentenciaSQL, strAlerta As String
                strAlerta = "Responsable asignado"

                If conexion.State = ConnectionState.Closed Then conexion.Open()

                If lblDireccion.Text = "0" Then
                    comando.CommandText = "update " & lblTabla.Text & " set " & lblCampoAActualizar.Text & "=" & TreeView2.SelectedValue & " where id=" & lblClaveTabla.Text
                Else
                    sentenciaSQL = "update " & lblTabla.Text & " set " & _
                                lblCampoAActualizar.Text & " = @" & lblCampoAActualizar.Text & _
                                " where (id = 0" & lblClaveTabla.Text & " and idPDA = '" & lblClaveSecTabla.Text & "')"
                    comando.CommandText = sentenciaSQL
                    comando.Parameters.Clear()
                    comando.Parameters.AddWithValue("@" & lblCampoAActualizar.Text, TreeView2.SelectedValue)

                    If lblTabla.Text = "TTareas" Then

                        Dim idUsuarioOrig As Integer = 0
                        comandoLog.CommandText = "select idUsuario from ttareas where id=" & lblClaveTabla.Text & " and idPDA='" & lblClaveSecTabla.Text & "'"
                        If Not IsNothing(comandoLog.ExecuteScalar()) Then idUsuarioOrig = comandoLog.ExecuteScalar

                        utiles.EscribeLog(lblClaveTabla.Text, lblClaveSecTabla.Text, comando, conexion, 4, 0, Session("usuarioReg"), idUsuarioOrig, TreeView2.SelectedValue)

                        ActualizarFechaTareas(lblClaveTabla.Text)
                        strAlerta = "Tarea reasignada"
                    End If
                End If

                comando.ExecuteNonQuery()
                conexion.Close()
                TreeView2.Nodes.Clear()
                If lblPagina.Text = "tareas.aspx" Then
                    'Page.ClientScript.RegisterStartupScript(Page.GetType, "cerrar", "<script language=javascript>alert('" & strAlerta & "');/*document.getElementById('capa').style.visibility='hidden'*/cerrar();window.location='" & Request.QueryString("reqURL") & "';</script>")
                    Page.ClientScript.RegisterStartupScript(Page.GetType, "cerrar", "<script language=javascript>alert('" & strAlerta & "');/*document.getElementById('capa').style.visibility='hidden'*/cerrar();window.location='" & lblPagina.Text & "';</script>")
                Else
                    'Page.ClientScript.RegisterStartupScript(Page.GetType, "cerrar", "<script language=javascript>alert('" & strAlerta & "');/*window.location='" & lblPagina.Text & "';*//*document.getElementById('capa').style.visibility='hidden'*/cerrar();window.location='" & Request.QueryString("reqURL") & "';</script>")
                    Page.ClientScript.RegisterStartupScript(Page.GetType, "cerrar", "<script language=javascript>alert('" & strAlerta & "');/*window.location='" & lblPagina.Text & "';*//*document.getElementById('capa').style.visibility='hidden'*/cerrar();window.location='" & lblPagina.Text & "';</script>")
                End If
            Else
                Dim comando As SqlCommand = New SqlCommand("", conexion)
                comando.CommandText = "select nombre + ' ' + apellidos as usuario from tusuarios where id=" & TreeView2.SelectedValue
                If conexion.State = ConnectionState.Closed Then conexion.Open()
                comando.ExecuteNonQuery()
                lblResponsable.Text = "Responsable: " & comando.ExecuteScalar
                Page.ClientScript.RegisterStartupScript(Page.GetType, "cerrar", "<script language=javascript>alert('Responsable asignado. Recuerde grabar antes de salir.');" & _
                        "document.getElementById(""txtIdResponsable"").value=""" & TreeView2.SelectedValue & """;" & _
                        "document.getElementById(""lblResponsable1"").outerHTML=""<span id='lblResponsable1'>" & lblResponsable.Text & "</span>"";" & _
                        "document.getElementById(""ucJerarquiaUsuarios_capa"").style.visibility=""hidden"";</script>")

            End If
        End If
    End Sub


    Public Sub CargarJerarquia(ByVal Tabla As String, ByVal ClaveTabla As Integer, ByVal ClaveSecTabla As String, ByVal CampoAActualizar As String, ByVal Direccion As Integer, _
                            ByVal ActualizarDatos As Boolean, ByVal Pagina As String, ByVal IdUsuario As Integer, ByVal IdCargo As Integer, ByVal rutaF As String)
        'Direccion: 0 = arriba (saca los responsables), 1 = abajo (saca los subordinados)
        'ActualizarDatos: true = si queremos que en esta página se actualizen en b.d. los datos.
        '                 false = si no queremos que se actualizen, pq se actualizarán después.        
        'lblTabla
        'lblClaveTabla
        'lblClaveSecTabla
        'lblCampoAActualizar
        'lblDireccion
        'lblActualizarDatos
        'lblIdUsuario
        'lblPagina
        'idCargo

        ruta = rutaF
        pnlJerarquiaUs.Visible = True
        pnlFotos.Visible = False
        pnlEstado.Visible = False

        Dim IdResponsable As Integer
        IdResponsable = 0

        lblTabla.Text = Tabla
        lblClaveTabla.Text = ClaveTabla
        lblClaveSecTabla.Text = ClaveSecTabla
        lblCampoAActualizar.Text = CampoAActualizar
        lblDireccion.Text = Direccion
        lblActualizarDatos.Text = ActualizarDatos
        lblIdUsuario.Text = IdUsuario
        lblPagina.Text = Pagina

        TreeView2.Nodes.Clear()

        If conexion.State = ConnectionState.Closed Then conexion.Open()
        Dim comUsuario As SqlCommand = New SqlCommand("", conexion)
        'comUsuario.CommandText = "select idCargo from tusuarios where id=" & IdUsuario
        'IdCargo = comUsuario.ExecuteScalar

        If Direccion = 0 Then
            Dim comResponsable As SqlCommand = New SqlCommand("", conexion)
            comResponsable.CommandText = "select idResponsable from tusuarios where id=" & IdUsuario
            IdResponsable = comResponsable.ExecuteScalar

            comUsuario.CommandText = "select id from tcargos where indicejerarquico<(Select indiceJerarquico from tcargos where id=" & IdCargo & ") and idjerarquia=(Select idjerarquia fROm tcargos where id=" & IdCargo & ")"
            Dim id As Integer = 0
            id = comUsuario.ExecuteScalar()

            If id = 0 Then
                Page.ClientScript.RegisterStartupScript(Page.GetType, "NoResponsable", "<script language=javascript>alert('No se le puede asignar ningún responsable porque\n no hay ningún usuario con cargo superior.');</script>")
                lblResponsable.Text = ""
                Exit Sub
            End If

            comResponsable.CommandText = "select nombre + ' ' + apellidos as usuario from tusuarios where id=" & IdResponsable
            comUsuario.CommandText = "select nombre + ' ' + apellidos as usuario from tusuarios where id=" & IdUsuario

            lblResponsable.Text = comResponsable.ExecuteScalar
            lblNombre.Text = comUsuario.ExecuteScalar

            If lblResponsable.Text.Length + lblNombre.Text.Length + 4 > 42 Then
                lblNombre.Text = Left(lblNombre.Text, -lblResponsable.Text.Length + 35) & "..."
            End If
            'lblResponsable.Text = "Responsable: " & lblResponsable.Text

            daResponsables.SelectCommand.CommandText = "select U.id as id, U.nombre + ' ' + U.apellidos as Usuario, U.idResponsable" & _
                ", C.nombre as cargo, C.indiceJerarquico, C.idJerarquia from TUsuarios U left outer join TCargos C " & _
                "on U.idCargo=C.id where C.indiceJerarquico<(select indiceJerarquico from TCargos where id=" & IdCargo & _
                ") and C.idJerarquia=(select idJerarquia from TCargos where id=" & IdCargo & ") order by C.indiceJerarquico"
            daResponsables.Fill(dstResponsables, "TablaCargo")

            daJerarquia.SelectCommand.CommandText = "EXEC jerarquia null, null,true"
        Else
            comUsuario.CommandText = "select nombre + ' ' + apellidos as usuario from tusuarios where id=" & IdUsuario
            lblNombre.Text = "Tarea asignada a: " & comUsuario.ExecuteScalar

            daResponsables.SelectCommand.CommandText = "select U.id as id, U.nombre + ' ' + U.apellidos as Usuario, U.idResponsable" & _
                ", C.nombre as cargo, C.indiceJerarquico, C.idJerarquia from TUsuarios U left outer join TCargos C " & _
                "on U.idCargo=C.id where C.indiceJerarquico>(select indiceJerarquico from TCargos where id=" & IdCargo & _
                ") and C.idJerarquia=(select idJerarquia from TCargos where id=" & IdCargo & ") order by C.indiceJerarquico"
            daResponsables.Fill(dstResponsables, "TablaCargo")

            daJerarquia.SelectCommand.CommandText = "EXEC jerarquia " & IdUsuario & ", null,true"
        End If
        daJerarquia.Fill(dstJerarquia, "TablaJerarquia")

        'Cargamos los responsables o subordinados
        Dim j, i As Integer

        Dim numeroComas, profundidad As Integer
        Dim nodoUltimo, rama As TreeNode
        Dim EsResponsable As Boolean
        For j = 0 To dstJerarquia.Tables("TablaJerarquia").Rows.Count - 1
            dstOperaciones.Clear()
            daOperaciones.SelectCommand.CommandText = "Select C.nombre as cargo, J.nombre as jerarquia from tusuarios U left outer " & _
                    "join tcargos C on U.idCargo=C.id left outer " & _
                    "join tjerarquias J on C.idJerarquia=J.id where U.id=" & dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario")
            daOperaciones.Fill(dstOperaciones, "TablaOperaciones")

            numeroComas = UBound(Split(dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("padres"), ","))
            If numeroComas = 0 Then
                If dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario") <> IdUsuario Then
                    TreeView2.Nodes.Add(New TreeNode("<b>" & dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("nombre") & " " & _
                            dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("apellidos") & _
                            "</b> - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("jerarquia") & _
                            " - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("cargo") & _
                            "&nbsp;&nbsp;&nbsp;&nbsp;<img src='" & ruta & "images/iconos/icoSelecResponsable.gif' border='0' alt='Asignar'>", dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario")))
                Else
                    TreeView2.Nodes.Add(New TreeNode("<b>" & dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("nombre") & " " & _
                            dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("apellidos") & _
                            "</b> - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("jerarquia") & _
                            " - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("cargo"), dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario")))
                End If
            Else
                EsResponsable = False
                For i = 0 To dstResponsables.Tables("TablaCargo").Rows.Count - 1
                    If dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario") = dstResponsables.Tables("TablaCargo").Rows(i).Item("id") Then
                        EsResponsable = True
                    End If
                Next
                If EsResponsable Then
                    nodoUltimo = TreeView2.Nodes(TreeView2.Nodes.Count - 1)

                    While nodoUltimo.ChildNodes.Count > 0
                        nodoUltimo = nodoUltimo.ChildNodes(nodoUltimo.ChildNodes.Count - 1)
                    End While
                    If dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario") <> IdUsuario Then
                        rama = New TreeNode("<b>" & dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("nombre") & " " & _
                                dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("apellidos") & _
                                "</b> - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("jerarquia") & _
                                " - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("cargo") & _
                                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='" & ruta & "images/iconos/icoSelecResponsable.gif' border='0' alt='Asignar'>", dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario"))
                    Else
                        rama = New TreeNode("<b>" & dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("nombre") & " " & _
                                dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("apellidos") & _
                                "</b> - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("jerarquia") & _
                                " - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("cargo"), dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario"))
                    End If
                    nodoUltimo.ChildNodes.Add(rama)


                    If CInt(rama.Value) = CInt(IdResponsable) Then
                        For profundidad = 1 To numeroComas - 1
                            nodoUltimo = nodoUltimo.Parent
                        Next
                        nodoUltimo.Expanded = True
                        rama.Parent.Expanded = True
                    End If
                End If
            End If
        Next
        Page.ClientScript.RegisterStartupScript(Page.GetType, "capa", "<script language=javascript>mostrarCapa();</script>")
    End Sub

    ''' <summary>
    ''' Versión de "cargarJerarquia" más simplificada, por JFEM
    ''' </summary>
    ''' <param name="Tabla"></param>
    ''' <param name="ClaveTabla"></param>
    ''' <param name="ClaveSecTabla"></param>
    ''' <param name="CampoAActualizar"></param>
    ''' <param name="Direccion"></param>
    ''' <param name="ActualizarDatos"></param>
    ''' <param name="Pagina"></param>
    ''' <param name="IdUsuario"></param>
    ''' <param name="IdCargo"></param>
    ''' <param name="rutaF"></param>
    ''' <remarks></remarks>
    Public Sub cargarJerarquiaSimple(ByVal Tabla As String, ByVal ClaveTabla As Integer, ByVal ClaveSecTabla As String, ByVal CampoAActualizar As String, ByVal Direccion As Integer, _
                            ByVal ActualizarDatos As Boolean, ByVal Pagina As String, ByVal IdUsuario As Integer, ByVal IdCargo As Integer, ByVal rutaF As String)
        'Versión de "cargarJerarquia" más simplificada, por JFEM
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        Dim indiceJerarquico As Integer = Integer.MaxValue

        ruta = rutaF
        pnlJerarquiaUs.Visible = True
        pnlFotos.Visible = False
        pnlEstado.Visible = False

        lblTabla.Text = Tabla
        lblClaveTabla.Text = ClaveTabla
        lblClaveSecTabla.Text = ClaveSecTabla
        lblCampoAActualizar.Text = CampoAActualizar
        lblDireccion.Text = Direccion
        lblActualizarDatos.Text = ActualizarDatos
        lblIdUsuario.Text = IdUsuario
        lblPagina.Text = Pagina

        Dim IdResponsable As Integer
        IdResponsable = 0
        daJerarquia.SelectCommand.CommandText = "select idResponsable from tusuarios where id=" & IdUsuario
        IdResponsable = daJerarquia.SelectCommand.ExecuteScalar


        TreeView2.Nodes.Clear()

        daJerarquia.SelectCommand.CommandText = "select indiceJerarquico from tcargos where id=" & IdCargo
        If Not IsNothing(daJerarquia.SelectCommand.ExecuteScalar()) Then indiceJerarquico = daJerarquia.SelectCommand.ExecuteScalar()

        If Direccion = 0 Then
            daJerarquia.SelectCommand.CommandText = "select nombre + ' ' + apellidos as usuario from tusuarios where id=" & IdResponsable
            lblResponsable.Text = daJerarquia.SelectCommand.ExecuteScalar
            daJerarquia.SelectCommand.CommandText = "select nombre + ' ' + apellidos as usuario from tusuarios where id=" & IdUsuario
            lblNombre.Text = daJerarquia.SelectCommand.ExecuteScalar
            If lblResponsable.Text.Length + lblNombre.Text.Length + 4 > 42 Then
                lblNombre.Text = Left(lblNombre.Text, -lblResponsable.Text.Length + 35) & "..."
            End If

            daJerarquia.SelectCommand.CommandText = "exec jerarquiaCargos null,null,true"
        ElseIf Direccion = 1 Then
            daJerarquia.SelectCommand.CommandText = "select U.nombre + ' ' + U.apellidos as usuario from tusuarios U join TTareas T on T.idUsuario=U.id where T.id=" & ClaveTabla & " and T.idPDA='" & ClaveSecTabla & "'"
            lblNombre.Text = "Tarea asignada a: " & daJerarquia.SelectCommand.ExecuteScalar

            daJerarquia.SelectCommand.CommandText = "exec jerarquiaCargos " & IdUsuario & ",null,true"
        End If
        daJerarquia.Fill(dstJerarquia, "TablaJerarquia")

        'Cargamos los responsables o subordinados
        Dim j As Integer

        Dim numeroComas, profundidad As Integer
        Dim nodoUltimo, rama As TreeNode
        Dim color As String = ""
        For j = 0 To dstJerarquia.Tables("TablaJerarquia").Rows.Count - 1
            If Direccion = 1 Or dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("indiceJerarquico") < indiceJerarquico Then
                dstOperaciones.Clear()
                daOperaciones.SelectCommand.CommandText = "Select C.nombre as cargo, J.nombre as jerarquia, U.idEmpresa from tusuarios U left outer " & _
                        "join tcargos C on U.idCargo=C.id left outer " & _
                        "join tjerarquias J on C.idJerarquia=J.id where U.id=" & dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario")
                daOperaciones.Fill(dstOperaciones, "TablaOperaciones")
                If utiles.nullACero(dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("idEmpresa")) = 5 Then
                    color = " style='color:red;'"
                Else
                    color = ""
                End If

                numeroComas = UBound(Split(dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("padres"), ","))
                If numeroComas = 0 Then
                    If dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario") <> IdUsuario Then
                        TreeView2.Nodes.Add(New TreeNode("<b" & color & ">" & dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("nombre") & " " & _
                                dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("apellidos") & _
                                "</b> - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("jerarquia") & _
                                " - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("cargo") & _
                                "&nbsp;&nbsp;&nbsp;&nbsp;<img src='" & ruta & "images/iconos/icoSelecResponsable.gif' border='0' alt='Asignar'>", dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario")))
                    Else
                        TreeView2.Nodes.Add(New TreeNode("<b>" & dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("nombre") & " " & _
                                dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("apellidos") & _
                                "</b> - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("jerarquia") & _
                                " - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("cargo"), dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario")))
                    End If
                Else

                    nodoUltimo = TreeView2.Nodes(TreeView2.Nodes.Count - 1)
                    For profundidad = 1 To numeroComas - 1
                        nodoUltimo = nodoUltimo.ChildNodes(nodoUltimo.ChildNodes.Count - 1)
                    Next
                    If dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario") <> IdUsuario Then
                        rama = New TreeNode("<b" & color & ">" & dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("nombre") & " " & _
                                dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("apellidos") & _
                                "</b> - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("jerarquia") & _
                                " - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("cargo") & _
                                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='" & ruta & "images/iconos/icoSelecResponsable.gif' border='0' alt='Asignar'>", dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario"))
                    Else
                        rama = New TreeNode("<b>" & dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("nombre") & " " & _
                                dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("apellidos") & _
                                "</b> - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("jerarquia") & _
                                " - " & dstOperaciones.Tables("TablaOperaciones").Rows(0).Item("cargo"), dstJerarquia.Tables("TablaJerarquia").Rows(j).Item("idUsuario"))
                    End If
                    nodoUltimo.ChildNodes.Add(rama)


                    If Direccion = 1 Then
                        TreeView2.Nodes(0).Expanded = True
                    End If
                End If

            End If
        Next
        Page.ClientScript.RegisterStartupScript(Page.GetType, "MostrarCapaArbolJerarquico", "<script language=javascript>mostrarCapa();</script>")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        conexion = New SqlConnection(strdsn)

        daResponsables = New SqlDataAdapter("", conexion)
        dstResponsables = New DataSet()

        daJerarquia = New SqlDataAdapter("", conexion)
        dstJerarquia = New DataSet()

        daOperaciones = New SqlDataAdapter("", conexion)
        dstOperaciones = New DataSet()

        daEstados = New SqlDataAdapter("", conexion)
        dstEstados = New DataSet

        daResponsable = New SqlDataAdapter("", conexion)
        dstResponsable = New DataSet()

    End Sub

    Public Property lblResponsableText() As String
        Get
            Return lblResponsable.Text
        End Get
        Set(ByVal value As String)
            lblResponsable.Text = value
        End Set
    End Property

    Public Sub CambiarEstado(ByVal IdTarea, ByVal IdEstado, ByVal idPDA, ByVal idUsuario, ByVal paginaF, ByVal rutaF)
        ruta = rutaF
        lblPaginaSel.Text = paginaF
        pnlEstado.Visible = True
        pnlFotos.Visible = False
        pnlJerarquiaUs.Visible = False

        lblIdTarea.Text = IdTarea
        lblIdPDA.Text = idPDA
        lblIdUsuario2.Text = idUsuario

        daEstados.SelectCommand.CommandText = "select * from testadotareas order by orden"
        daEstados.Fill(dstEstados, "TablaEstado")

        lstEstado.Items.Clear()
        lstEstado.DataSource = dstEstados.Tables("TablaEstado")
        lstEstado.DataTextField = "nombre"
        lstEstado.DataValueField = "id"
        lstEstado.DataBind()
        'lstEstado.Items.Insert(0, New ListItem("[Seleccionar]", ""))
        Estado(IdEstado)
        Page.ClientScript.RegisterStartupScript(Page.GetType, "capa", "<script language=javascript>mostrarCapa();</script>")
    End Sub

    Public Sub CargarFotos(ByVal idPDA As String, ByVal idTarea As Integer)
        ruta = "../"
        pnlFotos.Visible = True
        pnlEstado.Visible = False
        pnlJerarquiaUs.Visible = False

        Dim rutaFotos As String = Server.MapPath("../fotos/#" & idPDA & "#" & idTarea)
        Dim lista1() As String
        lista1 = System.IO.Directory.GetFiles(rutaFotos)
        Dim archivo As String
        Dim pos As Integer

        Dim file, strHtml As String
        Dim i As Integer
        i = 0
        strHtml = "<table width='100%' align='center'><tr>"
        Dim cadena As String
        Dim linea As Integer
        'For Each linea In Request.ServerVariables
        '    Response.Write(linea & ": " & Request.ServerVariables(linea) & "<br>")
        'Next


        For Each file In lista1
            pos = InStrRev(file, "\")
            archivo = Right(file, file.Length - pos)
            linea = InStrRev(Request.ServerVariables("HTTP_REFERER").ToString, "/")
            cadena = Left(Request.ServerVariables("HTTP_REFERER").ToString, linea - 1)
            linea = InStrRev(cadena, "/")
            cadena = Left(cadena, linea - 1)

            cadena &= "/fotos/%23" & idPDA & "%23" & idTarea & "/" & archivo
            pos = InStrRev(archivo, ".")
            archivo = Left(archivo, pos - 1)

            'Page.ClientScript.RegisterStartupScript(Page.GetType, "ampliarFoto", "<script language=javascript>window.open('" & file & "','_blank','menubar=yes,location=yes,scrollbars=yes,resizable=yes');</script>")
            strHtml &= "<td align='center' valign='bottom' style='text-align:center;'><a href='" & cadena & "' target='_blank'>" & _
                    "<img src=""volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a><br>" & archivo & "</td>"
            i = i + 1
            If i Mod 3 = 0 Then
                strHtml &= "</tr><tr>"
            End If
        Next

        strHtml &= "</tr></table>"
        capa4.InnerHtml = strHtml
        Page.ClientScript.RegisterStartupScript(Page.GetType, "capa", "<script language=javascript>mostrarCapa();</script>")
    End Sub

    Protected Sub lstEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstEstado.SelectedIndexChanged
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        Dim sentenciaSQL As String
        sentenciaSQL = "Update ttareas set idEstado='" & lstEstado.SelectedValue & "', ultModificacion='" & DateTime.Now & "' where id=" & lblIdTarea.Text & " and idPDA='" & lblIdPDA.Text & "'"

        comando.CommandText = "Update ttareas set idEstado='" & lstEstado.SelectedValue & "', ultModificacion='" & DateTime.Now & "' where id=" & lblIdTarea.Text & " and idPDA='" & lblIdPDA.Text & "'"
        comando.ExecuteNonQuery()

        Try
            comando.CommandText = "insert into ttareasLog (idTarea,idPDA,tipo,fecha,idEstado,idUsuario,sentenciaSQL) values (" & _
                        lblIdTarea.Text & ",'" & lblIdPDA.Text & "'" & _
                        ",9,'" & DateTime.Now() & "',0" & lstEstado.SelectedValue & "," & lblIdUsuario2.Text & _
                        ",'" & sentenciaSQL.Replace("'", "''") & "')"
            comando.ExecuteNonQuery()

            JavaScript.Alert(Page, "El estado ha sido cambiado")

            If lblPaginaSel.Text <> "" Then
                Page.ClientScript.RegisterStartupScript(Page.GetType, "recargar", "<script language=javascript>window.location='" & lblPaginaSel.Text & "';</script>")
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType, "CambiarEstado", "<script>if (document.getElementById('ddlEstado')){document.getElementById('ddlEstado').value='" & lstEstado.SelectedValue & "';}</script>")
            End If
            Exit Sub
        Catch ex As Exception
            Response.Write("ERROR: " & comando.CommandText)
        End Try
    End Sub

    Protected Sub Estado(ByVal id As Integer)
        Dim i As Integer
        For i = 0 To lstEstado.Items.Count - 1
			If utiles.nullACero(lstEstado.Items(i).Value) = id Then
				lblEstado.Text = "Estado: " & lstEstado.Items(i).Text
			End If
        Next

        If lblEstado.Text.Length > 200 Then
            lblEstado.Text = Left(lblEstado.Text, 117) & "..."
        End If
    End Sub

    Protected Sub ActualizarFechaTareas(ByVal idTarea)
        Dim comando As SqlCommand = New SqlCommand("", conexion)

        If conexion.State = ConnectionState.Closed Then conexion.Open()
        comando.CommandText = "update ttareas set ultModificacion='" & DateTime.Now & "' where id=" & idTarea
        comando.ExecuteNonQuery()
    End Sub

    Function EsResponsableF(ByVal idUsuario1 As Integer, ByVal idUsuario2 As Integer)
        Dim resultado As Boolean
        resultado = False
        daResponsable.SelectCommand.CommandText = "EXEC jerarquia " & idUsuario2 & ", null,true"
        daResponsable.Fill(dstResponsable, "TablaResponsables")

        Dim i As Integer
        For i = 0 To dstResponsable.Tables("TablaResponsables").Rows.Count - 1
            If dstResponsable.Tables("TablaResponsables").Rows(i).Item("idUsuario") = idUsuario1 And dstResponsable.Tables("TablaResponsables").Rows(i).Item("idUsuario") <> idUsuario2 Then
                resultado = True
                Exit For
            End If
        Next
        Return resultado
    End Function
End Class
