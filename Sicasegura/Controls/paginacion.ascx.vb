Imports System.Data
Imports System.Data.SqlClient

Partial Class paginacion
    Inherits System.Web.UI.UserControl

    Dim conexion As SqlConnection
    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")

    Public numpaginas As Integer
    'Public pageSize As Integer
    Public pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Public ruta As String


    Public Sub calcularPags(ByVal txtComando As String)
        Dim i As Integer
        conexion = New SqlConnection(strdsn)
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        If txtComando.ToUpper().IndexOf("EXEC") >= 0 Then
            Dim da As SqlDataAdapter = New SqlDataAdapter("", conexion)
            Dim ds As DataSet = New DataSet
            da.SelectCommand.CommandText = txtComando
            da.Fill(ds, "TablaCount")
            numpaginas = ds.Tables("TablaCount").Rows.Count
            ds.Tables.Clear()
            ds = Nothing
            da = Nothing
            GC.Collect()
        Else
            comando.CommandText = "select count(*) from (" & txtComando & ") dtable"
            numpaginas = comando.ExecuteScalar
        End If

        If numpaginas Mod pageSize <> 0 Then
            numpaginas = CInt(numpaginas / pageSize - 0.5) + 1
        Else
            numpaginas = CInt(numpaginas / pageSize)
        End If

        'ipim puesto para que no dé error ni quede mal el 0
        If numpaginas = 0 Then
            numpaginas = 1
        End If
        'fin

        ddlPaginacion.Items.Clear()
        For i = 1 To numpaginas
            ddlPaginacion.Items.Add(New System.Web.UI.WebControls.ListItem(i, i))
        Next
        If numpaginas < CInt(lblPagina.Text) Then
            ddlPaginacion.SelectedIndex = 0
            lblPagina.Text = 1
        Else
            ddlPaginacion.SelectedIndex = CInt(lblPagina.Text) - 1
        End If

        'Esto estaba en las páginas después de la llamada a ucPaginacion.calcularPags(txtComando)
        
        If CInt(lblPagina.Text) < numpaginas Then
            lbtNext.Visible = True
        Else
            lbtNext.Visible = False
        End If
        If CInt(lblPagina.Text) > 1 Then
            lbtPrev.Visible = True
        Else
            lbtPrev.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Sobrecarga del método calcularPags, para calcularlo metiendo directamente el número de registros que hay que paginar
    ''' </summary>
    ''' <param name="registros">Número de registros que habrá que paginar</param>
    ''' <remarks></remarks>
    Public Sub calcularPags(ByVal registros As Integer)
        Dim i As Integer

        numpaginas = registros

        If numpaginas Mod pageSize <> 0 Then
            numpaginas = CInt(numpaginas / pageSize - 0.5) + 1
        Else
            numpaginas = CInt(numpaginas / pageSize)
        End If

        'ipim puesto para que no dé error ni quede mal el 0
        If numpaginas = 0 Then
            numpaginas = 1
        End If
        'fin

        ddlPaginacion.Items.Clear()
        For i = 1 To numpaginas
            ddlPaginacion.Items.Add(New System.Web.UI.WebControls.ListItem(i, i))
        Next
        ddlPaginacion.SelectedIndex = CInt(lblPagina.Text) - 1

        If CInt(lblPagina.Text) < numpaginas Then
            lbtNext.Visible = True
        Else
            lbtNext.Visible = False
        End If
        If CInt(lblPagina.Text) > 1 Then
            lbtPrev.Visible = True
        Else
            lbtPrev.Visible = False
        End If
    End Sub

    Public Sub lbtNext_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lbtNext.Command, lbtPrev.Command, lbtFirst.Command, lbtLast.Command
        lblMueve.Text = "si"
        If e.CommandName = "pagPrev" Then
            lblPagina.Text = (CInt(lblPagina.Text) - 1).ToString()
        ElseIf e.CommandName = "pagSig" Then
            lblPagina.Text = (CInt(lblPagina.Text) + 1).ToString()
        ElseIf e.CommandName = "firstPage" Then
            lblPagina.Text = "1"
        ElseIf e.CommandName = "lastPage" Then
            lblPagina.Text = ddlPaginacion.Items(ddlPaginacion.Items.Count - 1).Value
        End If
    End Sub

    Public Property lblMuevetext() As String
        Get
            Return lblMueve.Text
        End Get
        Set(ByVal value As String)
            lblMueve.Text = value
        End Set
    End Property

    Public Property lblPaginatext() As String
        Get
            Return lblPagina.Text
        End Get
        Set(ByVal value As String)
            lblPagina.Text = value
        End Set
    End Property

    Public Sub lblNumpaginasDatabind()
        lblNumpaginas.DataBind()
    End Sub

    Private Sub ddlPaginacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPaginacion.SelectedIndexChanged
        lblPagina.Text = ddlPaginacion.Items(ddlPaginacion.SelectedIndex).Value
        lblMueve.Text = "si"
    End Sub

    Public Sub calcularPagsBandeja(ByVal txtComando As String)
        Dim i As Integer
        conexion = New SqlConnection(strdsn)
        Dim comando As SqlCommand = New SqlCommand("", conexion)

        If conexion.State = ConnectionState.Closed Then conexion.Open()
        comando.CommandText = "create table #ttemporaljerarquia(idusuario int,nombre nvarchar(1000),apellidos nvarchar(1000),[login] nvarchar(255), password nvarchar(255),sincroniza bit, padres nvarchar(1000))"
        comando.ExecuteNonQuery()
        comando.CommandText = "insert into #ttemporaljerarquia exec jerarquia " & Session("usuarioReg") & ", null,1 "
        comando.ExecuteNonQuery()

        comando.CommandText = "select count(*) from (" & txtComando & ") dtable"
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        numpaginas = comando.ExecuteScalar

        comando.CommandText = "drop table #ttemporaljerarquia"
        comando.ExecuteNonQuery()

        If numpaginas Mod pageSize <> 0 Then
            numpaginas = CInt(numpaginas / pageSize - 0.5) + 1
        Else
            numpaginas = CInt(numpaginas / pageSize)
        End If

        'ipim puesto para que no dé error ni quede mal el 0
        If numpaginas = 0 Then
            numpaginas = 1
        End If
        'fin

        ddlPaginacion.Items.Clear()
        For i = 1 To numpaginas
            ddlPaginacion.Items.Add(New System.Web.UI.WebControls.ListItem(i, i))
        Next
        ddlPaginacion.SelectedIndex = CInt(lblPagina.Text) - 1

        'Esto estaba en las páginas después de la llamada a ucPaginacion.calcularPags(txtComando)

        If CInt(lblPagina.Text) < numpaginas Then
            lbtNext.Visible = True
        Else
            lbtNext.Visible = False
        End If
        If CInt(lblPagina.Text) > 1 Then
            lbtPrev.Visible = True
        Else
            lbtPrev.Visible = False
        End If
        
    End Sub

End Class



