Imports GuarderiaFluvial
Imports System.Data
Imports System.IO
Imports System
Imports System.Configuration

Partial Class SICAH_Agrupaciones_edicion
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_dbsica"))
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim rutaFicherosTemp As String = ConfigurationManager.AppSettings.Get("rutaFicherosTemp")
    Dim dst As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sql As String
        Dim idSistema As String = Request.QueryString("idSistema")
        Dim idPunto As String = Request.Form("id")
        Dim codigoPVYCR As String = Request.Form("codigoPVYCR")
        Dim EM As String = Request.Form("EM")
        Dim CFD As String = Request.Form("CFD")
        Dim TOC As String = Request.Form("TOC")
        Dim FI As String = Request.Form("FI")
        Dim FF As String = Request.Form("FF")

        Dim ficheroRespuesta As String = rutaFicherosTemp & DateTime.Now.Ticks.ToString() & ".json"
        Dim salida As StreamWriter
        salida = New StreamWriter(ficheroRespuesta, False, System.Text.Encoding.UTF8)

        If idSistema = "" Then
            Exit Sub
        End If

        If codigoPVYCR Is Nothing Then  'Listado
            If Request.QueryString("borrado") = "true" Then
                If conexion.State = ConnectionState.Closed Then conexion.Open()
                comando.CommandText = "DELETE [SICA_SIST_Sistemas-Punto] WHERE ID_Sistema=" & idSistema & " and id_puntoSistema=" & idPunto
                comando.ExecuteNonQuery()
                comando.CommandText = "DELETE SICA_SIST_PuntoSistema WHERE ID=" & idPunto
                comando.ExecuteNonQuery()
                salida.Write("[ {mensaje: 'Registro eliminado'} ]")
                Context.Response.ContentType = "text/json"
            Else
                sql = "SELECT p.ID, p.CodigoPVYCR, p.EM, p.CFD, P.TOC, CONVERT(NVARCHAR(15),P.FI,103)  AS FI, CONVERT(NVARCHAR(15),P.FF,103) AS FF FROM [SICA_SIST_Sistemas-Punto] SP inner join SICA_SIST_PuntoSistema P on sp.id_puntoSistema=p.id where ID_Sistema=" & idSistema
                dst = cargarDatosBD(sql, conexion)
                dst.WriteXml(salida)
                Context.Response.ContentType = "text/xml"
            End If
        Else
            If idPunto = "" Then   'Nuevo
                If conexion.State = ConnectionState.Closed Then conexion.Open()
                sql = "SELECT max(id) from SICA_SIST_PuntoSistema"
                comando.CommandText = sql
                Dim maxId As Integer = utiles.nullACero(comando.ExecuteScalar) + 1

                sql = "INSERT INTO SICA_SIST_PuntoSistema (id, CodigoPVYCR, EM, FI, FF) values (" & maxId & ", '" & codigoPVYCR & "', '" & EM & "', '" & _
                      FI & "', '" & FF & "')"
                comando.CommandText = sql
                comando.ExecuteNonQuery()
                sql = "INSERT INTO [SICA_SIST_Sistemas-Punto] (ID_Sistema, ID_PuntoSistema) values (" & idSistema & "," & maxId & ")"
                comando.CommandText = sql
                comando.ExecuteNonQuery()

                salida.Write("[ {mensaje: 'Nuevo registro insertado', id:" & maxId & "} ]")
            Else    'Actualiza
                If conexion.State = ConnectionState.Closed Then conexion.Open()
                sql = "UPDATE SICA_SIST_PuntoSistema SET CodigoPVYCR='" & codigoPVYCR & "', EM='" & EM & "', CFD='" & CFD & "', TOC='" & TOC & "', FI='" & FI & _
                      "', FF='" & FF & "' WHERE id=" & idPunto
                comando.CommandText = sql
                comando.ExecuteNonQuery()
                salida.Write("[ {mensaje: 'Registro actualizado'} ]")
            End If
            Context.Response.ContentType = "text/json"
        End If


        salida.Close()
        Context.Response.WriteFile(ficheroRespuesta)
        Context.Response.Flush()
    End Sub

    Private Function cargarDatosBD(ByVal sql As String, ByVal cn As SqlClient.SqlConnection) As DataSet
        Dim command As New System.Data.SqlClient.SqlCommand(sql, conexion)
        command.CommandType = CommandType.Text
        Dim dst As New DataSet
        Dim da As New SqlClient.SqlDataAdapter(command)
        da.Fill(dst)
        Return dst
    End Function
    
End Class
