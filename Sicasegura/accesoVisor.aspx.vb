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



        Dim VisitorsIPAddr As String
        VisitorsIPAddr = String.Empty
        If Not (HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR") Is Nothing) Then
            VisitorsIPAddr = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR").ToString()
        ElseIf Not (HttpContext.Current.Request.UserHostAddress.Length = 0) Then
            VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress
        End If


        If VisitorsIPAddr.Contains("10.31") Then
            Response.Redirect(ConfigurationManager.AppSettings("urlVisor_CHS") & "?target=xwyz2010")
        Else
            Response.Redirect(ConfigurationManager.AppSettings("urlVisor") & "?target=xwyz2010")
        End If




        'Dim TipoCauce As String = ""

        'comando.CommandText = "insert into TSesionVisor(idSesion,idUsuario,fecha,modo,A,M,T,cA,cE,cG,cM,cN,cP,cR) values('" & Session.SessionID & "','" & Session("usuarioReg") & "',getdate(),'" & Session("modo") & "'," & Session("Acequias") & "," & Session("Motores") & "," & Session("Telemandos") & "," & Session("Aportaciones") & "," & Session("Edar") & "," & Session("Gravedad") & "," & Session("CauceMotor") & "," & Session("Noria") & "," & Session("Pozo") & "," & Session("Retorno") & ")"
        'comando.ExecuteNonQuery()
        'Response.Redirect(ConfigurationManager.AppSettings("urlVisor") & "?target=" & Session.SessionID)

    End Sub
End Class
