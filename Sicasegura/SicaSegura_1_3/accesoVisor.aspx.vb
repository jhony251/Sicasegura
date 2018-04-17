Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial

Partial Class accesoVisor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings("dsn"))
        Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

        utiles.Comprobar_Conexion_BD(Page, conexion, True)
        comando.CommandText = "delete from TSesionVisor where fecha <= '" & DateTime.Now().Subtract(TimeSpan.FromDays(1)).ToString("dd/MM/yyyy") & "' "
        comando.ExecuteNonQuery()
        comando.CommandText = "delete from TSesionVisor where idSesion='" & Session.SessionID & "'"
        comando.ExecuteNonQuery()

        comando.CommandText = "insert into TSesionVisor(idSesion,idUsuario,fecha) values('" & Session.SessionID & "vailavan" & "','" & Session("usuarioReg") & "',getdate())"
        comando.ExecuteNonQuery()
        Response.Redirect(ConfigurationManager.AppSettings("urlVisor") & "?target=" & Session.SessionID & "vailavan")

        'Dim TipoCauce As String = ""

        'comando.CommandText = "insert into TSesionVisor(idSesion,idUsuario,fecha,modo,A,M,T,cA,cE,cG,cM,cN,cP,cR) values('" & Session.SessionID & "','" & Session("usuarioReg") & "',getdate(),'" & Session("modo") & "'," & Session("Acequias") & "," & Session("Motores") & "," & Session("Telemandos") & "," & Session("Aportaciones") & "," & Session("Edar") & "," & Session("Gravedad") & "," & Session("CauceMotor") & "," & Session("Noria") & "," & Session("Pozo") & "," & Session("Retorno") & ")"
        'comando.ExecuteNonQuery()
        'Response.Redirect(ConfigurationManager.AppSettings("urlVisor") & "?target=" & Session.SessionID)
        
    End Sub
End Class
