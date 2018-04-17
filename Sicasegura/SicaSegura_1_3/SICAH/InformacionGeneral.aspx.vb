Imports GuarderiaFluvial
Imports System.Data.SqlClient
Partial Class SICAH_InformacionGeneral
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            Dim ClaveNodo, NodoClave, claveTipo, nodotexto, IdElementoMedida As String
            Dim puntoycoma As Integer = 0
            Dim guiones As Integer = 0
            Dim Tipo As String = ""
            Dim NivelCauce As String = ""
            'el nodoclave contiene el códigoPVYCR mas el tipo (E-elemento, P-punto, C-cauce)
            NodoClave = Request.QueryString("nodoclave").ToString
            'Nivel de anidamiento del cauce = X
            NivelCauce = Request.QueryString("X")

            Tipo = NodoClave.Substring(NodoClave.Length - 1, 1)

            Session("nodoclave") = NodoClave
            'el nodotexto contiene el codigoPVYCR mas la descripcion
            nodotexto = Request.QueryString("nodotexto").ToString
            Session("nodotexto") = nodotexto

            'según si han marcado información general o visor mostrará una cosa u otra
            Dim cookie As HttpCookie = Request.Cookies("c_lecturasvisor")

            If cookie Is Nothing Then
                Session("lblLecturasVisor") = ""
            Else
                Session("lblLecturasVisor") = cookie.Value
            End If

            If Session("lblLecturasVisor") = "V" Then
                'Response.Redirect("NCM_CaucePuntDETALLE.aspx?nodotexto=" & nodotexto & "&nodoclave=" & NodoClave)
                'Page.RegisterClientScriptBlock("mostrarCargando", "<script language=javascript>" & _
                '                                      "mostrarCargando()" & _
                '                             "</script>")
                Response.Redirect("CaucePuntDETALLE.aspx?nodotexto=" & nodotexto & "&nodoclave=" & NodoClave)
                'Page.RegisterClientScriptBlock("ocultarCargando", "<script language=javascript>" & _
                '                                    "ocultarCargando()" & _
                '                           "</script>")
            Else
                'Obtenemos la clave del nodo (o codigoPVYCR) y el tipo
                ClaveNodo = NodoClave.Substring(0, NodoClave.IndexOf(";"))
                Session("ClaveNodo") = ClaveNodo
                Dim V() As String
                V = Split(nodotexto, "-")
                puntoycoma = numeropuntoycoma(NodoClave)
                If puntoycoma = 2 Then
                    guiones = numeroguiones(nodotexto)
                    'si tiene mas de dos guines es un elemento de medida, si tiene 1 es un punto y si tiene 0 es un cauce
                    IdElementoMedida = V(guiones - 1).ToString
                    'obtenemos el tipo(punto, elemento, cauce)
                    claveTipo = NodoClave.Substring(NodoClave.Length - 3, 1)
                Else
                    claveTipo = NodoClave.Substring(NodoClave.Length - 1, 1)
                End If
                Session("claveTipo") = claveTipo
                Session("idElemento") = IdElementoMedida
                If claveTipo = "C" Then
                    If utiles.nullACero(Session("EnlaceC")) = 0 Then
                        Response.Redirect("InformacionGeneralC.aspx?clavenodo='" & ClaveNodo & "'&claveTipo='" & claveTipo & "'&X=" & NivelCauce)
                    Else
                        If Session("EnlaceC") = 0 Then
                            Response.Redirect("InformacionGeneralC.aspx?clavenodo='" & ClaveNodo & "'&claveTipo='" & claveTipo & "'&X=" & NivelCauce)
                        ElseIf Session("EnlaceC") = 3 Then
                            Response.Redirect("PantallaInformes.aspx?nodotexto=" & nodotexto & "&clavenodo=" & ClaveNodo & "&claveTipo=" & claveTipo)
                        ElseIf Session("EnlaceC") = 5 Then
                            'Page.RegisterClientScriptBlock("mostrarCargando", "<script language=javascript>" & _
                            '                     "mostrarCargando()" & _
                            '            "</script>")
                            Response.Redirect("CaucePuntDETALLE.aspx?nodotexto=" & nodotexto & "&nodoclave=" & NodoClave & "&LecturasVisor=V")
                            'Page.RegisterClientScriptBlock("ocultarCargando", "<script language=javascript>" & _
                            '                       "ocultarCargando()" & _
                            '              "</script>")
                        End If
                    End If

                ElseIf claveTipo = "P" Then
                    If utiles.nullACero(Session("EnlaceP")) = 0 Then
                        Response.Redirect("InformacionGeneralP.aspx?clavenodo='" & ClaveNodo & "'&claveTipo='" & claveTipo & "' ")
                    Else
                        If Session("EnlaceP") = 1 Then
                            Response.Redirect("InformacionGeneralP.aspx?clavenodo='" & ClaveNodo & "'&claveTipo='" & claveTipo & "' ")
                        ElseIf Session("EnlaceP") = 3 Then
                            Response.Redirect("PantallaInformes.aspx?nodotexto=" & nodotexto & "&clavenodo=" & ClaveNodo & "&claveTipo=" & claveTipo)
                        ElseIf Session("EnlaceP") = 5 Then
                            'Page.RegisterClientScriptBlock("mostrarCargando", "<script language=javascript>" & _
                            '                         "mostrarCargando()" & _
                            '                "</script>")
                            Response.Redirect("CaucePuntDETALLE.aspx?nodotexto=" & nodotexto & "&nodoclave=" & NodoClave & "&LecturasVisor=V")
                            'Page.RegisterClientScriptBlock("ocultarCargando", "<script language=javascript>" & _
                            '                      "ocultarCargando()" & _
                            '             "</script>")
                        ElseIf Session("EnlaceP") = 7 Then
                            '*********************************************************************************
                            '*********************************************************************************
                            '*********************************************************************************
                            'DESCOMENTAR CUANDO SE PONGA EN PRODUCCIÓN
                            '*********************************************************************************
                            '*********************************************************************************
                            ''RDF 20081023
                            Response.Redirect("AccesoTopkapiDesdeArbol.aspx?nodotexto=" & nodotexto & "&clavenodo=" & ClaveNodo & "&claveTipo=" & claveTipo)

                            'Response.Redirect("..\PantallasVisor\infoPunto.aspx?nodotexto=" & nodotexto & "&clavenodo=" & ClaveNodo & "&claveTipo=" & claveTipo)
                        ElseIf Session("EnlaceP") = 8 Then
                            'RDF 20081030
                            If ExistenImagenes(Page, ClaveNodo) Then
                                Response.Redirect("Galeria.aspx?nodotexto=" & nodotexto & "&clavenodo=" & ClaveNodo & "&claveTipo=" & claveTipo)
                            Else
                                Response.Redirect("InformacionGeneralP.aspx?clavenodo='" & ClaveNodo & "'&claveTipo='" & claveTipo & "' ")
                            End If

                        End If
                    End If


                ElseIf claveTipo = "E" Then
                    If utiles.nullACero(Session("EnlaceE")) = 0 Then
                        Response.Redirect("CaucePuntDETALLE.aspx?nodotexto=" & nodotexto & "&nodoclave=" & NodoClave & "&LecturasVisor=L")
                    Else
                        If Session("EnlaceE") = 2 Then
                            Response.Redirect("InformacionGeneralE.aspx?clavenodo='" & ClaveNodo & "'&claveTipo='" & claveTipo & "' ")
                        ElseIf Session("EnlaceE") = 3 Then
                            Response.Redirect("PantallaInformes.aspx?nodotexto=" & nodotexto & "&clavenodo=" & ClaveNodo & "&claveTipo=" & claveTipo)
                        ElseIf Session("EnlaceE") = 4 Then
                            Response.Redirect("CaucePuntDETALLE.aspx?nodotexto=" & nodotexto & "&nodoclave=" & NodoClave & "&LecturasVisor=L")
                        ElseIf Session("EnlaceE") = 5 Then
                            Response.Redirect("CaucePuntDETALLE.aspx?nodotexto=" & nodotexto & "&nodoclave=" & NodoClave & "&LecturasVisor=V")
                        End If
                    End If
                    'Response.Redirect("InformacionGeneralE.aspx?clavenodo='" & ClaveNodo & "'&idElemento='" & IdElementoMedida & "'&claveTipo='" & claveTipo & "' ")


                End If
            End If
            End If
    End Sub
    Private Function numeropuntoycoma(ByVal cadena As String) As Integer
        Return Math.Max(Split(cadena, ";").Length - 1, 0)
    End Function
    Private Function numeroguiones(ByVal cadena As String) As Integer
        Return Math.Max(Split(cadena, "-").Length - 1, 0)
    End Function
    Shared Function ExistenImagenes(ByVal pagina As Page, ByVal clavenodo As String) As Boolean
        'RDF 2/03/2009
        Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))


        utiles.Comprobar_Conexion_BD(pagina, conexion)

        'RDF          
        'Fecha: 2/03/2009
        'Se comprueba si el punto tiene algún registro en la tabla PVYCR_Imagenes
        Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
        comando.CommandText = "SELECT count(*) FROM PVYCR_Imagenes WHERE codigoPVYCR='" & clavenodo & "'"
        Dim Existe As Integer
        Existe = comando.ExecuteScalar()
        utiles.CerrarConexion(conexion)
        If Existe > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
