Imports System.Data
Imports System.Net
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System.IO
Imports System

Partial Class SICAH_galeria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Se obtiene la url que tiene que abrir
        Try
            'borrar
            'Alert(Page, Request.ServerVariables("REMOTE_ADDR").ToString())


            Select Case Page.Request.QueryString("ClaveTipo")
                Case "P"
                    'Session("EnlaceP") = 7
                    'lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(Session("EnlaceP"), "../", Page, 11, "P", Session("ClaveNodo"), "N")
                    lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(8, "../", Page, 11, "P", Request.QueryString("clavenodo"), "N")

                Case Else
                    Exit Sub
            End Select
            CargarFotos(Request.QueryString("clavenodo"))


        

        Catch Excepcion As Exception
            Alert(Page, "No se puede cargar la galeria fotográfica")
        End Try

    End Sub
  Public Sub CargarFotos(ByVal CodigoPunto As String)

    Dim strHtml As String
    strHtml = "<table width='100%' align='center'><tr>"
    Dim cadena As String
    Dim urlImagenes As String

    'Se obtiene el número de imágenes que tiene el punto
    'RDF 2/03/2009
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    utiles.Comprobar_Conexion_BD(Page, conexion)

    'RDF 
    'Fecha: 03/03/2009
    '********************************************************************************
    '********************************************************************************

    If (Request.ServerVariables("REMOTE_ADDR").ToString.Substring(0, 6) = "10.31.") Or (Request.ServerVariables("REMOTE_ADDR").ToString() = "89.140.163.10") Then
      'La petición se está haciendo desde la CHS
      urlImagenes = ConfigurationSettings.AppSettings("http_imagenes_CHS")
    Else
      'en cualquier otro caso
      urlImagenes = ConfigurationSettings.AppSettings("http_imagenes")
    End If
    '********************************************************************************
    '********************************************************************************

    'RDF          
    'Fecha: 2/03/2009
    'Se comprueba si el punto tiene algún registro en la tabla PVYCR_Imagenes
    Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    comando.CommandText = "SELECT NumImagenes FROM PVYCR_Imagenes WHERE codigoPVYCR='" & CodigoPunto & "'"
    Dim NumeroImagenes As Integer
    NumeroImagenes = comando.ExecuteScalar()
    Dim nombreimagen As String
    Dim Contador As Integer
    Dim miniatura As String

    '/********************************* codigo de Juan Antonio *********************/
    Dim miniaturaError As String

    Dim comillas = Chr(34)

    miniaturaError = urlImagenes & "01-Croquis/TB/" & "CroquisDefault.jpg"

    strHtml &= "<td align='center' valign='bottom' colspan='5' style='text-align:center;'>"

    strHtml &= "<center><a href='" & urlImagenes & "01-Croquis/" & Request.QueryString("clavenodo") & ".jpg' rel='lightbox_plus' class='vertical' target='_blank'>"

    strHtml &= "<img width='320' height='226' src='" & urlImagenes & "01-Croquis/TB/" & Request.QueryString("clavenodo") & ".jpg'  onerror=" & comillas & "this.src='" & miniaturaError & "';" & comillas & "> "

    strHtml &= "</a></center><br><hr><br>"

    strHtml &= "</td>"

    strHtml &= "</tr><tr>"
    '/************************************* fin código Juan Antonio****************************************/
    Contador = 1
    While (Contador <= NumeroImagenes)
      cadena = urlImagenes & Request.QueryString("clavenodo") & "/" & Request.QueryString("clavenodo") & "-" & String.Format("{0:000}", Contador) & ".JPG"
      miniatura = urlImagenes & Request.QueryString("clavenodo") & "/TB/" & Request.QueryString("clavenodo") & "-" & String.Format("{0:000}", Contador) & ".JPG"
      nombreimagen = Request.QueryString("clavenodo") & "-" & String.Format("{0:000}", Contador) & ".JPG"
      strHtml &= "<td align='center' valign='bottom' style='text-align:center;'><a href='" & cadena & "' rel='lightbox_plus' class='vertical' target='_blank'>" & _
      "<img src=" & miniatura & " height=150 width=150 border=0></a><br>" & nombreimagen & "</td>"

      Contador = Contador + 1
      If (Contador - 1) Mod 5 = 0 Then
        strHtml &= "</tr><tr>"
      End If

    End While

    strHtml &= "</tr></table>"

    lblHTML.Text = strHtml
  End Sub
End Class
