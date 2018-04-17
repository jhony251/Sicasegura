Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Partial Class SICAH_AccesoVisorDesdeArbolMain
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("claveTipo") = "C" And Request.QueryString("alerta") = "S" Then
            JavaScript.Alert(Me, "No existen puntos para ese cauce")
        Else

            If Request.QueryString("alerta").ToString = "S" Then
                JavaScript.Alert(Me, "No se puede mostrar el punto por no tener coordenadas")
            End If

        End If

    End Sub

    Protected Sub VerTodos(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            If lbVerTodos.Text = "Ver todos" Then
                Session("modo") = "T"
                lbVerTodos.Text = "Ver solo resaltados"

            Else
                Session("modo") = "X"
                lbVerTodos.Text = "Ver todos"
            End If
            'cada vez que el usuario marque o desmarque un filtro, utilizaremos la veriable de sesion "zoom" para indicar si se debe recalcular el
            'zoom o no
            Session("zoom") = "S"

        Catch Excepcion As Exception
        End Try

    End Sub

    Protected Sub VerAcequias(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081007
        'Checks de visualización de Acequias
        Try
            If chkAcequias.Checked Then
                Session("Acequias") = 1
            Else
                Session("Acequias") = 0
            End If
           Session("zoom") = "S"
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub VerMotores(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081007
        'Checks de visualización de Motores
        Try
            If chkMotores.Checked Then
                Session("Motores") = 1
            Else
                Session("Motores") = 0
            End If
         Session("zoom") = "S"
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub VerTelemandos(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081007
        'Checks de visualización de Telemandos
        Try
            If chkTelemandos.Checked Then
                Session("Telemandos") = 1
            Else
                Session("Telemandos") = 0
            End If
         Session("zoom") = "S"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub VerAportacion(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081107
        'Checks de visualización de Aportaciones
        Try
            If chkAportacion.Checked Then
                Session("Aportaciones") = 1
            Else
                Session("Aportaciones") = 0
            End If
         Session("zoom") = "S"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub VerEdar(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081107
        'Checks de visualización de Edar
        Try
            If chkEdar.Checked Then
                Session("Edar") = 1
            Else
                Session("Edar") = 0
            End If
            Session("zoom") = "S"
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub VerGravedad(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081107
        'Checks de visualización de Gravedad
        Try
            If chkGravedad.Checked Then
                Session("Gravedad") = 1
            Else
                Session("Gravedad") = 0
            End If
            Session("zoom") = "S"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub VerCauceMotor(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081107
        'Checks de visualización de CauceMotor
        Try
            If chkMotor.Checked Then
                Session("CauceMotor") = 1
            Else
                Session("CauceMotor") = 0
            End If
            Session("zoom") = "S"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub VerNoria(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081107
        'Checks de visualización de Noria
        Try
            If chkNoria.Checked Then
                Session("Noria") = 1
            Else
                Session("Noria") = 0
            End If
            Session("zoom") = "S"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub VerPozo(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081107
        'Checks de visualización de Pozo
        Try
            If chkPozo.Checked Then
                Session("Pozo") = 1
            Else
                Session("Pozo") = 0
            End If
            Session("zoom") = "S"
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub VerRetorno(ByVal sender As Object, ByVal e As System.EventArgs)
        'RDF 20081107
        'Checks de visualización de Retorno
        Try
            If chkRetorno.Checked Then
                Session("Retorno") = 1
            Else
                Session("Retorno") = 0
            End If
            Session("zoom") = "S"
        Catch ex As Exception

        End Try
  End Sub
  Protected Sub VerLeyendas(ByVal sender As Object, ByVal e As System.EventArgs)
    Page.RegisterClientScriptBlock("verLeyendas", "<script language=javascript>" & _
                              "window.open('../SICAH/leyendasVisor.aspx')" & _
                             "</script>")
    'Response.Redirect("../SICAH/leyendasVisor.aspx")

  End Sub

End Class
