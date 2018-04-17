Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports GuarderiaFluvial

Partial Class SICAH_ActualizadorXMLArbolAsync

    Inherits System.Web.UI.Page
    Protected Sub imgUpdateEstructuraArbol_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpdateEstructuraArbol.Click
        Dim miservicio As New XMLDataSetMaker.XMLDataSetMaker
        Dim resul As String
        Dim innertext, g_innertext As String
        Dim strScript As String

        miservicio.Timeout = -1

        'Llamada al Servicio Paso a Paso 

        'Paso 0 --------------------------------------------------------------------------------
        innertext = miservicio.Inicializacion()
        'Mensaje en cliente
        g_innertext = innertext & "\n"
        'Paso 1 --------------------------------------------------------------------------------
        innertext = miservicio.Normalizacion()
        'Mensaje en cliente
        g_innertext = g_innertext & innertext & "\n"
        'Paso 2 --------------------------------------------------------------------------------
        innertext = miservicio.Formato()
        'Mensaje en cliente
        g_innertext = g_innertext & innertext & "\n"
        'Paso 3 --------------------------------------------------------------------------------
        innertext = miservicio.GenerarXML(MapPath("xmlDataSet.xml"))
        'Mensaje en cliente
        g_innertext = g_innertext & innertext & "\n"

        'Tratamiento de la cadena
        g_innertext = Replace(g_innertext, "'", "-")

        strScript = "<SCRIPT language='javascript'>" & _
                                "document.getElementById('" + lblMensajeBoton.ClientID + "').innerText='" & g_innertext & "';" & _
                                "</SCRIPT>"
        If Not Page.ClientScript.IsStartupScriptRegistered("ActualizaTexto") Then
            Page.ClientScript.RegisterStartupScript(Page.GetType, "ActualizaTexto", strScript)
        End If

        'Muestra Resultados
        If Right(g_innertext, Len("Correctamente")) = "Correctamente" Then
            Application("PVYCR_Arbol_Updated") = "OK"
        Else
            Application("PVYCR_Arbol_Updated") = "NOK"
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            If Request.QueryString("asociacion") <> "yes" Then
                lblCabecera.Text = genHTML.cabecera(Page)
                lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))
            End If
            imgUpdateEstructuraArbol.Attributes.Add("onclick", "document.getElementById('" & lblMensajeBoton.ClientID & "').innerText='Procesando...Por favor Espere'")
        End If
    End Sub


End Class
