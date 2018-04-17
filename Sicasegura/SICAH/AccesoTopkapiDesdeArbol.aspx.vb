Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial.SICA_GestionArboles
Imports System.Data.SqlClient
Imports System
Partial Class SICAH_AccesoTopkapiDesdeArbol
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ncm funciona aquí, ESTO SE COMENTÓ EL 26/08/2009 PARA HACERLO SACANDO LA INFORMACIÓMN DESDE LA TABLA POR PETICIÓN JCARRIL
        'iframeInicio.Attributes.Add("src", "http://192.168.194.11/topkapi/AuRevoir.asp")
        'iframeInicio.Attributes.Add("src", "http://192.168.194.11/topkapi/autologin.asp?Operateur=jcarril1&motpasse=jcarril1")
        'ncm funciona confe
        'iframeInicio.Attributes.Add("src", "http://172.17.30.53/topkapi/AuRevoir.asp")
        'iframeInicio.Attributes.Add("src", "http://172.17.30.53/topkapi/autologin.asp?Operateur=jcarril1&motpasse=jcarril1")

        'RDF 20/10/2008
        'Se obtiene la url que tiene que abrir
        Try

            Select Case Page.Request.QueryString("ClaveTipo")
                Case "P"
                    'Session("EnlaceP") = 7
                    'lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(Session("EnlaceP"), "../", Page, 11, "P", Session("ClaveNodo"), "N")
                    lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(7, "../", Page, 11, "P", Request.QueryString("clavenodo"), "N")

                Case Else
                    Exit Sub
            End Select
            'RDF
            'Fecha: 27/02/2009
            'Inicio Sesión SCADA
            'Pruebas
            'NO FUNCIONA, PROBARLO BIEN, NO SE PUEDE SUBIR A PRODUCCIÓN
            'NCM
            'Fecha: 26/08/2009
            'modificadás las urls según el correo mandado por jcarril para que funcione probado y pasado a pruebas
            '*****************************************************************
            '*****************************************************************
            '*****************************************************************


            Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings("dsnsegura_migracion"))
            Dim enlace As String
            conexion.Open()
            Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
            comando.CommandText = "SELECT Link FROM PVYCR_AccesosTopkapi WHERE codigoPVYCR='" & Request.QueryString("clavenodo") & "'"
            enlace = ""
            enlace = comando.ExecuteScalar
            lblEnlace.Text = enlace
            'ncm 29/10/2009 tengo que borrar el if completo cuando me creen el campo de tabla que discrimine entre las direcciones IP, si son confe u otras
            If (Request.ServerVariables("REMOTE_ADDR").ToString.Substring(0, 6) = "10.31.") Or (Request.ServerVariables("REMOTE_ADDR").ToString() = "89.140.163.10") Then
                lblEnlace.Text = ConfigurationManager.AppSettings("urlVisor_topkapi_CHS") & enlace
            Else
                lblEnlace.Text = ConfigurationManager.AppSettings("urlVisor_topkapi") & enlace
            End If
            'ncm comentado, se hace con código javascript

            'If (enlace <> "") Then
            'iframeVisor.Attributes.Add("src", enlace)
            'End If
            '    '*****************************************************************
            '    '*****************************************************************
            '    '*****************************************************************

        Catch Excepcion As Exception
            Alert(Page, "No se puede cargar la página de acceso al visor SCADA")
        End Try

    End Sub

 

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        'RDF
        'Fecha: 27/02/2009
        'Inicio Sesión SCADA
        'iframeInicio.Attributes.Add("src", "http://192.168.194./topkapi/AuRevoir.asp")
    End Sub
End Class
