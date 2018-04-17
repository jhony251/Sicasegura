Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial
Partial Class SICAH_accesoVisorDesdeArbol
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim utmX As Int64 = Integer.Parse(Request.QueryString("X"))
            Dim utmY As Int64 = Integer.Parse(Request.QueryString("Y"))
            Dim Xmax As Int64 = utmX + 75
            Dim Xmin As Int64 = utmX - 350
            Dim Ymin As Int64 = utmY - 420
            Dim Ymax As Int64 = utmY + 5

            Dim URL As String = "https://www.chsegura.es/chsic/?escenario=sica&extent=" & Xmin & " " & Ymin & " " & Xmax & " " & Ymax & " 25830"
            Response.Redirect(URL)

        Catch ex As Exception

        End Try
        





        Dim conexion As SqlConnection

        'If ConfigurationManager.AppSettings("dsnPre") IsNot Nothing Then
        '    conexion = New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings("dsnPre"))
        'Else
        conexion = New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings("dsn"))
        'End If

        Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
        utiles.Comprobar_Conexion_BD(Page, conexion, True)

        comando.CommandText = "delete from TSesionVisor where fecha <= '" & DateTime.Now().Subtract(TimeSpan.FromDays(1)).ToString("dd/MM/yyyy") & "' "
        comando.ExecuteNonQuery()
        comando.CommandText = "delete from TSesionVisor where idSesion='" & Session.SessionID & "'"
        comando.ExecuteNonQuery()

        Dim TipoCauce As String = ""

        comando.CommandText = "insert into TSesionVisor(idSesion,idUsuario,fecha,modo,A,M,T,cA,cE,cG,cM,cN,cP,cR) values('" & Session.SessionID & "','" & Session("usuarioReg") & "',getdate(),'" & Session("modo") & "'," & Session("Acequias") & "," & Session("Motores") & "," & Session("Telemandos") & "," & Session("Aportaciones") & "," & Session("Edar") & "," & Session("Gravedad") & "," & Session("CauceMotor") & "," & Session("Noria") & "," & Session("Pozo") & "," & Session("Retorno") & ")"
        comando.ExecuteNonQuery()
        'Me.lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(4, "../", Page, 11, "E", "o")
        utiles.CerrarConexion(conexion)
        'ncm 03/11/2009 esta variable indica a S que se debe mantener el zoom (X e Y) que haya puesto el usuario, tras haber cambiado
        'alguno de los filtros, y a N que se recalculará la envolvente (x e Y) como se ha hecho siempre. 
        'Esto sólo ocurre si hablamos del mismo punto, cuando cambiamos
        'de rama (bien sea punto, cauce, elemento se pondrá a N y se recalculará la envolvente
   
        If Session("zoom").ToString = "S" Then
            'ncm 22/02/2010 la llamada comentada tiene activada por defecto la CAPA DES, nos piden que lo quitemos.
            'Response.Redirect(ConfigurationManager.AppSettings("urlVisor") & "?target=" & Session.SessionID & "&Visible=DES;Puntos%20Control;CAUCES&zone=30&R=" & Request.QueryString("R"))
            Response.Redirect(ConfigurationManager.AppSettings("urlVisor") & "?target=xwyz2010&Visible=Puntos%20Control;CAUCES&zone=30&R=" & Request.QueryString("R"))
        Else
            'Response.Redirect(ConfigurationManager.AppSettings("urlVisor") & "?target=" & Session.SessionID & "&X=" & Request.QueryString("X").ToString & _
            '"&Y=" & Request.QueryString("Y").ToString() & "&Visible=DES;Puntos%20Control;CAUCES&zone=30&R=" & Request.QueryString("R"))
            Response.Redirect(ConfigurationManager.AppSettings("urlVisor") & "?target=xwyz2010&X=" & Request.QueryString("X").ToString & _
            "&Y=" & Request.QueryString("Y").ToString() & "&Visible=Puntos%20Control;CAUCES&zone=30&R=" & Request.QueryString("R"))

        End If

    End Sub

    
End Class
