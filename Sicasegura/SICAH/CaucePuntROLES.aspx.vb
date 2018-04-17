Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.IO

Partial Class SICAH_CaucePuntROLES
    Inherits System.Web.UI.Page

    Dim strdsn As String = ConfigurationSettings.AppSettings("dsnsegura_migracion")
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    'Dim daArbol As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    'Dim dstarbol As System.Data.DataSet = New System.Data.DataSet()

    Dim conexion_visibles As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim daCaucesVisibles As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion_visibles)
    Dim dstCaucesVisibles As System.Data.DataSet = New System.Data.DataSet()
    Dim str As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '----------------------------------------------------------------------------
        'Dim cookie As New HttpCookie("arrayPermisos", RamasVisiblesString())
        'Response.Cookies.Add(cookie)
        'ClientScript.RegisterStartupScript(Page.GetType, "mostrarRamasROLES", "<script language=javascript>mostrarRamasROLES();</script>")
        '---------------------------------------------------------------------------------------------------------------------------------------

        'EGB 10/02/2009 Versión SIN COOKIES Para Visualizar Permisos
        ClientScript.RegisterStartupScript(Page.GetType, "mostrarRamasROLES2", "<script language=javascript> document.getElementById('Permisos').value='" & RamasVisiblesString() & "';mostrarRamasROLES2();</script>")
        
    End Sub
    Private Function RamasVisiblesString()
        Dim str As String

        'NCM 18/09/2008 rellenamos en un dataset los valores que tiene el usuario para saber qué ramas puede ver

        daCaucesVisibles.SelectCommand.CommandText = "SELECT ARG, CM, OT0, OT1,S,VA,VB,VM from TUsuarios where login= '" & Session("loginUsuario").ToString & "' "

        daCaucesVisibles.Fill(dstCaucesVisibles, "TablaVisibles")

        'Construccion del string de permisos
        str = ""
        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("ARG") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("CM") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        'If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("ET") = "N" Then
        '    str = str & "none#"
        'Else
        '    str = str & "#"
        'End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("OT0") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("OT1") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("S") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VA") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VB") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If

        If dstCaucesVisibles.Tables("tablaVisibles").Rows(0).Item("VM") = "N" Then
            str = str & "none#"
        Else
            str = str & "#"
        End If
        'ncm 25/02/2010 la rama de peajes y trasvases no se verá, si alguna de las otras ramas está oculta.
        If str.Contains("none") Then
            str = str & "none#" & "none#"
        Else
            str = str & "#" & "#"
        End If
        conexion_visibles.Close()
        Return str
    End Function
End Class
