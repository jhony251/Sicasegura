Namespace GuarderiaFluvial

Public Class JavaScript

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'Utilidades de código cliente para ASP.NET
    'Autor: José Fernández Martínez
    'Fecha última actualización: 02/07/2004
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Overloads Shared Sub ScrollToControl(ByVal pagina As System.Web.UI.Page, ByVal micontrol As System.Web.UI.Control)
        'Hace que el control especificado se scrollee hasta ser visible en la página
            'Dim identificador As String
        Dim strScript As String = "<SCRIPT language='javascript'>" & _
            "document.getElementById('" + micontrol.ClientID + "').scrollIntoView(true);" & _
            "</SCRIPT>"
            'If Not pagina.IsStartupScriptRegistered("scrollTo") Then
            '    pagina.RegisterStartupScript("scrollTo", strScript)
            'End If
            If Not pagina.ClientScript.IsStartupScriptRegistered("scrollTo") Then
                pagina.ClientScript.RegisterStartupScript(pagina.GetType, "scrollTo", strScript)
            End If
    End Sub

    Overloads Shared Sub ScrollToControl(ByVal pagina As System.Web.UI.Page, ByVal micontrol As System.Web.UI.Control, ByVal alineacion As Boolean)
        'Hace que el control especificado se scrollee hasta ser visible en la página
        Dim strScript As String = "<SCRIPT language='javascript'>" & _
            "document.getElementById('" + micontrol.ClientID + "').scrollIntoView(" & alineacion.ToString.ToLower & ");" & _
            "</SCRIPT>"
            'If Not pagina.IsStartupScriptRegistered("scrollTo") Then
            '    pagina.RegisterStartupScript("scrollTo", strScript)
            'End If
            If Not pagina.ClientScript.IsStartupScriptRegistered("scrollTo") Then
                pagina.ClientScript.RegisterStartupScript(pagina.GetType, "scrollTo", strScript)
            End If
    End Sub

    Shared Sub SetFocusToControl(ByVal pagina As System.Web.UI.Page, ByVal micontrol As System.Web.UI.Control)
        'Pone el foco en el control de servidor especificado
        Dim strScript As String = "<SCRIPT language='javascript'>" & _
            "document.getElementById('" + micontrol.ClientID + "').focus();" & _
            "</SCRIPT>"
            'If Not pagina.IsStartupScriptRegistered("setFocus") Then
            '    pagina.RegisterStartupScript("setFocus", strScript)
            'End If
            If Not pagina.ClientScript.IsStartupScriptRegistered("setFocus") Then
                pagina.ClientScript.RegisterStartupScript(pagina.GetType, "setFocus", strScript)
            End If
    End Sub

    Shared Sub Alert(ByVal pagina As System.Web.UI.Page, ByVal mensaje As String)
        'Muestra un alert de javascript
        Dim strScript As String = "<SCRIPT language='javascript'>" & _
            "alert('" & mensaje & "');" & _
            "</SCRIPT>"
            'If Not pagina.IsStartupScriptRegistered("Alert") Then
            '    pagina.RegisterStartupScript("Alert", strScript)
            '    End If
            If Not pagina.ClientScript.IsStartupScriptRegistered("Alert") Then
                pagina.ClientScript.RegisterStartupScript(pagina.GetType, "Alert", strScript)
            End If
    End Sub

    Shared Sub preservaScroll(ByVal pagina As System.Web.UI.Page)
        'Preserva el desplazamiento de scroll de la página en cuestión entre postbacks;
        'esta función debería llamarse al inicio o al final del método Page_load
        If InStr(pagina.Request.ServerVariables("HTTP_USER_AGENT"), "MSIE") > 0 Then
                pagina.ClientScript.RegisterHiddenField("__scrollTop", "0")
                Alert(pagina, "lsñldkjñldkfj")
            If pagina.IsPostBack Then
                    'pagina.RegisterStartupScript("recuperarScroll", "<script language='javascript'>" & _
                    '                                                                          "document.body.scrollTop=" & pagina.Request.Form("__scrollTop") & ";" & _
                    '                                                                          "document.Form1.__scrollTop.value=document.body.scrollTop;" & _
                    '                                                                          "</script>")
                    pagina.ClientScript.RegisterStartupScript(pagina.GetType, "recuperarScroll", "<script language='javascript'>" & _
                                                                                              "document.body.scrollTop=" & pagina.Request.Form("__scrollTop") & ";" & _
                                                                                              "document.Form1.__scrollTop.value=document.body.scrollTop;" & _
                                                                                              "</script>")
            End If
                'pagina.RegisterStartupScript("asignarScroll", "<script language='javascript'>document.body.onscroll=function(){ document.Form1.__scrollTop.value=document.body.scrollTop;}</script>")
                pagina.ClientScript.RegisterStartupScript(pagina.GetType, "asignarScroll", "<script language='javascript'>document.body.onscroll=function(){ document.Form1.__scrollTop.value=document.body.scrollTop;}</script>")
        End If
    End Sub

    Shared Sub preservaScroll(ByVal pagina As System.Web.UI.Page, ByVal servercontrol As System.Web.UI.Control)
        'Preserva el desplazamiento de scroll del control de servidor pasado como parámetro entre postbacks;
        'esta función debería llamarse al inicio o al final del método Page_load
        If InStr(pagina.Request.ServerVariables("HTTP_USER_AGENT"), "MSIE") > 0 Then
                'pagina.RegisterHiddenField("__scrollTop", "0")
                pagina.ClientScript.RegisterHiddenField("__scrollTop", "0")
            If pagina.IsPostBack Then
                    'pagina.RegisterStartupScript("recuperarScroll", "<script language=javascript>" & _
                    '                                                                          "document.getElementById('" & servercontrol.ClientID & "').scrollTop=" & pagina.Request.Form("__scrollTop") & ";" & _
                    '                                                                          "document.Form1.__scrollTop.value=document.getElementById('" & servercontrol.ClientID & "').scrollTop;" & _
                    '                                                                          "</script>")
                    pagina.ClientScript.RegisterStartupScript(pagina.GetType, "recuperarScroll", "<script language=javascript>" & _
                                                                                              "document.getElementById('" & servercontrol.ClientID & "').scrollTop=" & pagina.Request.Form("__scrollTop") & ";" & _
                                                                                              "document.Form1.__scrollTop.value=document.getElementById('" & servercontrol.ClientID & "').scrollTop;" & _
                                                                                              "</script>")
            End If
                'pagina.RegisterStartupScript("asignarScroll", "<script language=javascript>document.getElementById('" & servercontrol.ClientID & "').onscroll=function(){ document.Form1.__scrollTop.value=document.getElementById('" & servercontrol.ClientID & "').scrollTop;}</script>")
                pagina.ClientScript.RegisterStartupScript(pagina.GetType, "asignarScroll", "<script language=javascript>document.getElementById('" & servercontrol.ClientID & "').onscroll=function(){ document.Form1.__scrollTop.value=document.getElementById('" & servercontrol.ClientID & "').scrollTop;}</script>")
        End If
    End Sub

    Shared Sub preservaScroll(ByVal pagina As System.Web.UI.Page, ByVal clientID As String)
        'Preserva el desplazamiento de scroll del elemento cliente pasado como parámetro (usualmente un DIV) entre postbacks;
        'esta función debería llamarse al inicio o al final del método Page_load
        If InStr(pagina.Request.ServerVariables("HTTP_USER_AGENT"), "MSIE") > 0 Then
                'pagina.RegisterHiddenField("__scrollTop", "0")
                pagina.ClientScript.RegisterHiddenField("__scrollTop", "0")
            If pagina.IsPostBack Then
                    'pagina.RegisterStartupScript("recuperarScroll", "<script language=javascript>" & _
                    '                                                                              "document.getElementById('" & clientID & "').scrollTop=" & pagina.Request.Form("__scrollTop") & ";" & _
                    '                                                                              "document.Form1.__scrollTop.value=document.getElementById('" & clientID & "').scrollTop;" & _
                    '                                                                              "</script>")
                    pagina.ClientScript.RegisterStartupScript(pagina.GetType, "recuperarScroll", "<script language=javascript>" & _
                                                                                              "document.getElementById('" & clientID & "').scrollTop=" & pagina.Request.Form("__scrollTop") & ";" & _
                                                                                              "document.Form1.__scrollTop.value=document.getElementById('" & clientID & "').scrollTop;" & _
                                                                                              "</script>")
            End If
                'pagina.RegisterStartupScript("asignarScroll", "<script language=javascript>document.getElementById('" & clientID & "').onscroll=function(){ document.Form1.__scrollTop.value=document.getElementById('" & clientID & "').scrollTop;}</script>")
                pagina.ClientScript.RegisterStartupScript(pagina.GetType, "asignarScroll", "<script language=javascript>document.getElementById('" & clientID & "').onscroll=function(){ document.Form1.__scrollTop.value=document.getElementById('" & clientID & "').scrollTop;}</script>")
        End If
        End Sub

        Shared Sub controlEntero(ByVal pagina As System.Web.UI.Page, ByVal elcontrol As System.Web.UI.WebControls.TextBox)
            'If Not pagina.IsClientScriptBlockRegistered("scriptEntero") Then
            '    pagina.RegisterClientScriptBlock("scriptEntero", "<script language=javascript>" & _
            '                                    "function entero(control){" & _
            '                                    "	if ((event.keyCode<'0'.charCodeAt(0)) || (event.keyCode>'9'.charCodeAt(0))){" & vbCrLf & _
            '                                    "			event.keyCode=0;" & vbCrLf & _
            '                                    "	}" & vbCrLf & _
            '                                    "}" & _
            '                                    "</script>")

            'End If

            If Not pagina.ClientScript.IsClientScriptBlockRegistered("scriptEntero") Then
                pagina.ClientScript.RegisterClientScriptBlock(pagina.GetType, "scriptEntero", "<script language=javascript>" & _
                                                "function entero(control){" & _
                                                "	if ((event.keyCode<'0'.charCodeAt(0)) || (event.keyCode>'9'.charCodeAt(0))){" & vbCrLf & _
                                                "			event.keyCode=0;" & vbCrLf & _
                                                "	}" & vbCrLf & _
                                                "}" & _
                                                "</script>")

            End If

            elcontrol.Attributes.Add("onkeypress", "entero(this);")
        End Sub


        Shared Sub controlDouble(ByVal pagina As System.Web.UI.Page, ByVal elcontrol As System.Web.UI.WebControls.TextBox)
            If Not pagina.ClientScript.IsClientScriptBlockRegistered("scriptDouble") Then
                pagina.ClientScript.RegisterClientScriptBlock(pagina.GetType, "scriptEntero", "<script language=javascript>" & vbCrLf & _
                                                "function doble(control){" & vbCrLf & _
                                                "   if ((event.keyCode!='.'.charCodeAt(0)) &&  (event.keyCode!='-'.charCodeAt(0)) ){" & vbCrLf & _
                                                "	  if ((event.keyCode<'0'.charCodeAt(0)) || (event.keyCode>'9'.charCodeAt(0)) ){" & vbCrLf & _
                                                "			event.keyCode=0;" & vbCrLf & _
                                                "	   }" & vbCrLf & _
                                                "	}" & vbCrLf & _
                                                "}" & _
                                                "</script>")

            End If

            elcontrol.Attributes.Add("onkeypress", "doble(this);")
        End Sub

        Shared Sub controlFormatea(ByVal pagina As System.Web.UI.Page, ByVal elcontrol As System.Web.UI.WebControls.TextBox)

            If Not pagina.ClientScript.IsClientScriptBlockRegistered("scriptFormatea") Then
                pagina.ClientScript.RegisterClientScriptBlock(pagina.GetType, "scriptFormatea", "<script language=javascript>" & _
                            "function formatea(elemento){" & _
                                "if (elemento.value != """"){ " & _
                                    "var sEntero = Math.floor(parseFloat(elemento.value)); " & _
                                    "var sDecimal = """"; " & _
                                    "var sEnteroF = """"; " & _
                                    "var sEnterotmp = """"; " & _
                                    "while (sEntero >= 1000){ " & _
                                        "if (sEnteroF != """") sEnteroF = ""."" + sEnteroF; " & _
                                        "/*if (sEntero%1000 == 0) sEnteroF = ""000"" + sEnteroF;*/ " & _
                                        "/*else {*/ " & _
                                        "sEnterotmp = ""000"" + (sEntero%1000); " & _
                                        "sEnterotmp = sEnterotmp.substring(sEnterotmp.length-3, sEnterotmp.length); " & _
                                        "sEnteroF = sEnterotmp + sEnteroF;" & _
                                        "/*}*/" & _
                                        "sEntero = Math.floor(sEntero/1000); " & _
                                    "} " & _
                                "if (elemento.value.indexOf("","") > 0) " & _
                                    "sDecimal = elemento.value.substring(elemento.value.indexOf("",""),elemento.value.length).substring(0,3); " & _
                                "if (sEnteroF != """") " & _
                                    "sEnteroF = sEntero + ""."" + sEnteroF; " & _
                                "else " & _
                                    "sEnteroF = sEntero; " & _
                                "elemento.value = sEnteroF+sDecimal; " & _
                                "}" & _
                            "}" & _
                            "</script>")
            End If

            elcontrol.Attributes.Add("onBlur", "formatea(this);")
        End Sub

        Shared Sub controlFormatea(ByVal pagina As System.Web.UI.Page, ByVal elcontrol As System.Web.UI.WebControls.TextBox, ByVal NumDecimales As Integer)

            If Not pagina.ClientScript.IsClientScriptBlockRegistered("scriptFormatea") Then
                pagina.ClientScript.RegisterClientScriptBlock(pagina.GetType, "scriptFormatea2", "<script language=javascript>" & _
                            "function formatea2(elemento, NumDecimales){" & _
                                    "if (elemento.value != """"){ " & _
                                        "var sEntero = Math.floor(parseFloat(elemento.value)); " & _
                                        "var sDecimal = """"; " & _
                                        "var sEnteroF = """"; " & _
                                        "var sEnterotmp = """"; " & _
                                        "while (sEntero >= 1000){ " & _
                                            "if (sEnteroF != """") sEnteroF = ""."" + sEnteroF; " & _
                                            "/*if (sEntero%1000 == 0) sEnteroF = ""000"" + sEnteroF;*/ " & _
                                            "/*else {*/ " & _
                                            "sEnterotmp = ""000"" + (sEntero%1000); " & _
                                            "sEnterotmp = sEnterotmp.substring(sEnterotmp.length-3, sEnterotmp.length); " & _
                                            "sEnteroF = sEnterotmp + sEnteroF;" & _
                                            "/*}*/" & _
                                            "sEntero = Math.floor(sEntero/1000); " & _
                                        "} " & _
                                    "if (elemento.value.indexOf("","") > 0) " & _
                                        "sDecimal = elemento.value.substring(elemento.value.indexOf("",""),elemento.value.length).substring(0," & NumDecimales + 1 & "); " & _
                                    "if (sEnteroF != """") " & _
                                        "sEnteroF = sEntero + ""."" + sEnteroF; " & _
                                    "else " & _
                                        "sEnteroF = sEntero; " & _
                                    "elemento.value = sEnteroF+sDecimal; " & _
                                    "}" & _
                                "}" & _
                            "</script>")
            End If
            elcontrol.Attributes.Add("onBlur", "formatea2(this," & NumDecimales & ");")
        End Sub
        Shared Sub controlDesFormatea(ByVal pagina As System.Web.UI.Page, ByVal elcontrol As System.Web.UI.WebControls.TextBox)

            If Not pagina.ClientScript.IsClientScriptBlockRegistered("scriptDesFormatea") Then
                pagina.ClientScript.RegisterClientScriptBlock(pagina.GetType, "scriptDesFormatea", "<script language=javascript>" & _
                              "function desformatea(elemento){" & _
                              "elemento.value=elemento.value.replace(/\./g,"""");" & _
                              "elemento.select();" & _
                              "}" & _
                              "</script>")
            End If

            elcontrol.Attributes.Add("onFocus", "desformatea(this);")
        End Sub
        Shared Sub controlScrollArbol(ByVal pagina As System.Web.UI.Page, ByVal tvwArbol As TreeView, ByVal pnlArbol As Panel)
            'Script de Cliente Necesario para mantener el scroll del panel contendor del arbol de sistemas.
            Dim script As String = _
              "function ScrollArbol()" + _
              "{{" + _
              " try" + _
              " {{" + _
              "   var elem = document.getElementById('{0}_SelectedNode');" + _
              "   if(elem != null )" + _
              "   {{" + _
              "     var node = document.getElementById(elem.value);" + _
              "     if(node != null)" + _
              "     {{" + _
              "       node.scrollIntoView(true);" + _
              "       {1}.scrollLeft = 0;" + _
              "     }}" + _
              "   }}" + _
              " }}" + _
              " catch(oException)" + _
              " {{}}" + _
              "}}"

            pagina.ClientScript.RegisterClientScriptBlock(pagina.GetType(), "ScrollArbol", _
              String.Format(script, tvwArbol.ClientID, pnlArbol.ClientID), True)
        End Sub

        Shared Sub NodoSeleccionadoArbol(ByVal pagina As System.Web.UI.Page, ByVal tvwArbol As TreeView)
            'Script de Cliente Necesario para mantener el scroll del panel contendor del arbol de sistemas.
            Dim script As String = _
              "function NodoSeleccionado()" + _
              "{{" + _
              " try" + _
              " {{" + _
              "   var elem = document.getElementById('{0}_SelectedNode');" + _
              "   if(elem != null )" + _
              "   {{" + _
              "     var node = document.getElementById(elem.value);" + _
              "     if(node != null)" + _
              "     {{" + _
              "       var texto = node.getAttribute('Text'); alert(texto); return texto;" + _
              "     }}" + _
              "   }}" + _
              " }}" + _
              " catch(oException)" + _
              " {{alert('NOK');}}" + _
              "}}"

            pagina.ClientScript.RegisterClientScriptBlock(pagina.GetType(), "NodoSeleccionado", _
              String.Format(script, tvwArbol.ClientID), True)
        End Sub

    End Class
End Namespace
