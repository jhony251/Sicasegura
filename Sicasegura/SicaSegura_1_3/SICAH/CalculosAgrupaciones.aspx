<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CalculosAgrupaciones.aspx.vb" Inherits="SICAH_CalculosAgrupaciones" %>
<%@ Register TagPrefix="cc1" Namespace="NineRays.Reporting.Web" Assembly="NineRays.Reporting.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link rel="stylesheet" href="..\styles.css" />
    
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
    <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
    <script type="text/jscript" language="javascript" src="../js/utiles.js"></script>
    <script type="text/jscript" language="javascript" src="../js/utilesInterfazDinamica.js"></script>
    
    <!-- JScripts de Maquetación de Desplegables -->
    <script type="text/jscript" language="javascript">
     function confirmar_borrado()
    {
      if (confirm("¿Está seguro de desasociar el Cálculo actual de la Agrupación?")==true)
        return true;
      else
        return false;
    }
    function MostrarArbolSeleccionado(VerArbolSistemas)
    {
    if (VerArbolSistemas=='1')
        {ocultarPanel('pnlArbolSistemas');
        mostrarPanel('pnlArbolCaucesSistemas');
        PonNegrita('lblVerArbolCaucesSistemas');
        QuitaNegrita('lblVerArbolSistemas');}
    else
        {ocultarPanel('pnlArbolCaucesSistemas');
        mostrarPanel('pnlArbolSistemas');
        QuitaNegrita('lblVerArbolCaucesSistemas');
        PonNegrita('lblVerArbolSistemas');}
    }
    
    function desplegarSistemas(sender)
    {
        var despSistemas = document.getElementById('desplegableSistemas');
        if (despSistemas.style.display=='none'){
            ocultarDesplegables();
            var elementoOrigen = event.srcElement;
            
            if (elementoOrigen.tagName=='IMG'){
                despSistemas.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft-elementoOrigen.parentElement.offsetLeft+228 + document.body.scrollLeft) + 'px';
                despSistemas.style.top = '' + (event.clientY-event.offsetY-elementoOrigen.offsetTop+elementoOrigen.parentElement.offsetHeight-5 + document.body.scrollTop) + 'px';
            }
            else if (elementoOrigen.tagName=='TD'){
                despSistemas.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft+228 + document.body.scrollLeft) + 'px';        
                despSistemas.style.top = '' + (event.clientY-event.offsetY+elementoOrigen.offsetHeight-5 + document.body.scrollTop) + 'px';
            }
            ocultarSelects();
            despSistemas.style.display='';
        }
        else{
            despSistemas.style.display='none';
            mostrarSelects();
        }
        
        event.cancelBubble=true;
        document.getElementById('lbldespegableSistemas').value = 'Seleccione el Sistema:';
    }
    
    function ocultarDesplegables()
    {
        
        var i;
        document.getElementById('desplegableSistemas').style.display='none';
        mostrarSelects(); 
        
    }
    function ocultarPanel(panel)
    {
        document.getElementById(panel).style.display='none';
        
    }
    function mostrarPanel(panel)
    {
        document.getElementById(panel).style.display='';
        
    }
    function GetMouseXY(event)
    {
      document.getElementById('MouseX').value=event.clientX+document.body.scrollLeft -10;
      document.getElementById('MouseY').value=event.clientY+document.body.scrollTop +10;   
    }
    
    function FormatoFormulas(frase)
    {
     var patron = /(\W)/g;
     newstr = frase.replace(patron, "<b>$1</b>");
     alert(newstr)
     document.write(newstr);
     
    }
    function PonNegrita(etiqueta)
    {
        document.getElementById(etiqueta).style.fontWeight = 'bold';
    }
    function QuitaNegrita(etiqueta)
    {
        document.getElementById(etiqueta).style.fontWeight='';
    }
    
    function getCookie(name)
    {
      var cname = name + "=";               
      var dc = document.cookie;             
      if (dc.length > 0) {              
        begin = dc.indexOf(cname);       
        if (begin != -1) {           
          begin += cname.length;       
          end = dc.indexOf(";", begin);
          if (end == -1) end = dc.length;
            return unescape(dc.substring(begin, end));
        } 
      }
      return null;
    }
    
    function setCookie(name, value, expires, path, domain, secure) 
    {
      document.cookie = name + "=" + escape(value) + 
      ((expires == null) ? "" : "; expires=" + expires.toGMTString()) +
      ((path == null) ? "" : "; path=" + path) +
      ((domain == null) ? "" : "; domain=" + domain) +
      ((secure == null) ? "" : "; secure");
    }

    function delCookie (name,path,domain) 
    {
      if (getCookie(name)) {
        document.cookie = name + "=" +
        ((path == null) ? "" : "; path=" + path) +
        ((domain == null) ? "" : "; domain=" + domain) +
        "; expires=Thu, 01-Jan-70 00:00:01 GMT";
      }
    }
    
    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación del registro?")==true)
        return true;
      else
        return false;
    }
    
    </script>
    
    
</head>

<body bgcolor="#EEEAD2">
  <form id="Form1" method="post" runat="server">
  <span id="dsp4"></span>
  <span id="imagepath" style="display:none">../js/calendar/images</span>

      <!-- Campos de Texto Ocultos que almacenan la posicion relativa del TR donde se ubica el bottón cmdlocalizarNodos -->
      <input id="MouseX" type="hidden" runat="server"/>
      <input id="MouseY" type="hidden" runat="server"/>
            
    <!--
    Tabla Principal 
    -->
    <table cellspacing="2" align="center" width="100%" style="border: 1px solid #666666">
      <tr>
          <td>
              <table cellspacing="0" cellpadding="2" width="100%" style="height: 100%">
                <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label><tr>
                    <td align="left" class="titulo" style="width: 880px">
                        &nbsp;GESTIÓN DE AGRUPACIONES
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        [<a href="Agrupaciones.aspx?nuevo=no">Mantenimiento de Agrupaciones...</a>]
                        [<a href="Calculos.aspx?nuevo=no">Mantenimiento de Cálculos ...</a>]
                    
      
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="titListado" style="width: 880px"><asp:Label ID="lblTitulo" runat="server" Text='CRITERIOS DE BÚSQUEDA DE AGRUPACIONES'></asp:Label>
                    </td>
                </tr>
                  <tr>
                      <td valign="top" style="width: 880px">
                          <table style="width: 624px" cellspacing="0">
                              <tr>
                                  <td style="width: 647px;" valign="middle">
                                      Fecha de Validez</td>
                                  <td style="width: 165px;" valign="middle">
                                                  <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="texto" Text="" Width="164px"></asp:TextBox></td>
                                  <td valign="middle">
                                                  <asp:Image ID="imgCalFechaInicio" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                                  Style="cursor: pointer" /></td>
                                  <td style="width: 69px;" valign="middle">
                                  </td>
                                  <td style="width: 638px;" valign="middle">
                                  </td>
                              </tr>
                              <tr>
                                  <td style="width: 647px; height: 2px">
                                  Agrupación</td>
                                  <td style="height: 2px; width: 165px;">
                                  <asp:TextBox ID="txtSistema" runat="server" CssClass="texto" Text="" Width="164px"></asp:TextBox></td>
                                  <td style="height: 2px">
                                      <asp:ImageButton ID="imgBuscarSistema"
                                      runat="server" BorderStyle="None" BorderWidth="0px" ImageAlign="Top" ImageUrl="~/SICAH/images/iconos/icoBtnSistemasBuscar2.gif"
                                      ToolTip="Buscar en el arbol" />&nbsp;</td>
                                  <td style="width: 69px; height: 2px">
                                  <asp:Button ID="btnFiltrar" runat="server" CssClass="boton" Text="Filtrar" Visible="False" /></td>
                                  <td style="width: 638px; height: 2px">
                                  <asp:RequiredFieldValidator ID="rfvtxtSistema" runat="server" 
                                      ControlToValidate="txtSistema" 
                                      ErrorMessage="Debe rellenar o seleccionar el nombre de la Agrupación" Height="14px" Width="263px"></asp:RequiredFieldValidator>
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>
                <tr>
                    <td>
                  <!--PANEL GENERAL-->
                  <asp:Panel ID="pnlGeneral" runat="server" Width="100%">
                  
                      <table cellpadding="2" cellspacing="2" width="100%">
                          <tr>
                              <td align="left" colspan="2" rowspan="1" style="width: 240px;" valign="top">
                                  <asp:Panel ID="pnlArbolSistemas" runat="server" BackColor="White" BorderStyle="Groove" BorderWidth="1px"
                                      ForeColor="#FFFFCC" Height="500px" ScrollBars="Auto" Style="overflow: scroll"
                                      HorizontalAlign="Left" Width="300px">
                                      <asp:TreeView ID="tvwSistemas" runat="server" Height="100%" Width="93%">
                                            <SelectedNodeStyle BackColor="PaleGoldenrod" Font-Bold="True" Font-Underline="True" />
                                      </asp:TreeView>
                                  </asp:Panel>
                              </td>
                              <td align="left" colspan="1" valign="top">
                                 
                                  <table style="width: 100%">
                                      <tr>
                                          <td style="width: 489px; height: 15px">
                                            <asp:Panel ID="pnlSistemasHijos" runat="server" Width="100%" Visible="False">
                                            <table width=100%>
                                                <tr>
                                                    <td colspan=2 align="left" class="titListadoCalculos" 
                                                        style="height: 16px; width: 787px;">
                                                        <img src="../SICAH/images/SubSistemas.gif" align="bottom"/>
                                                        SUBAGRUPACIONES</td>
                                                    
                                                </tr>
                                                
                                                <tr>
                                                    <td colspan="2" align="left" class="celdacontenido";">
                                                                                                    
                                                           <!--Datos de Sistemas Hijos-->
                                                          <asp:Repeater ID="rpt_sistemashijos" runat="server">
                                                              <HeaderTemplate>
                                                              <!--Encabezado del Repeater rpt_sistemashijos--> 
                                                                  <table width="100%" cellpadding="0">
                                                                    <tr>
                                                                        <th class="preproduccion" align="left">
                                                                            SubAgrupaciones de la Agrupación actual</th>
                                                                    </tr> 
                                                              </HeaderTemplate>
                                                              <ItemTemplate>
                                                              <!--Fila del Repeater rpt_calculos--> 
                                                                  <tr>        
                                                                    <td class="formula" align="left" style="font: 9px verdana; background-color: #DDDDDD"><%#Container.DataItem("Formula")%></td>                                                     
                                                                  </tr>  
                                                                  <!--Lineas de división Repeater rpt_sistemashijos -->   
                                                                  <tr>
                                                                      <td colspan="15">
                                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                                          <tr>
                                                                            <td style="border-top: solid 2px #BABA74; font-size: 2px">
                                                                                &nbsp;</td>
                                                                          </tr>
                                                                        </table>
                                                                      </td>
                                                                  </tr>
                                                              </ItemTemplate>
                                                              
                                                             
                                                              <FooterTemplate>
                                                             
                                                                </table>
                                                              </FooterTemplate>
                                                          </asp:Repeater>
                                                    </td>
                                                    
                                                    
                                                    
                                                </tr>
                                            
                                            </table>
                                          </asp:Panel>
                                        </td>
                                      </tr>
              
                 </asp:Panel>
                      
                        </td>
                </tr>
                                      <tr>
                                        <td style="height: 315px;" align="left" valign="top">
                                            <!--PANEL DE CALCULOS-->
                                           <asp:Panel ID="pnlCalculos" runat="server" BorderStyle ="None" HorizontalAlign="Left" Height="90%" ScrollBars="vertical" Visible="False">
                                            
                                                <table width="95%">
                                                    <tr>
                                                        <td align="left" class="titListadoCalculos" style="width: 792px; height: 18px">
                                                            <img align="bottom" src="../SICAH/images/Calculos.gif" style="width: 15px" />&nbsp;
                                                            <asp:Label ID="lblinfosistema2" runat="server" Width="446px"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="celdacontenido">
                                                            <!--Datos de Calculos del Sistema-->
                                                            <asp:Repeater ID="rpt_calculos" runat="server">
                                                                <HeaderTemplate>
                                                                    <!--Encabezado del Repeater rpt_calculos-->
                                                                    <table cellpadding="0" width="100%">
                                                                    
                                                                    <tr>
                                                                        <th align="left" class="preproduccion" style="width:25">
                                                                            Descripción</th>
                                                                        <th align="left" class="preproduccion" style="width:25">
                                                                            Fórmula</th>
                                                                        <th align="left" class="preproduccion" style="width:5">
                                                                        </th>
                                                                        <th align="left" class="preproduccion" style="width:5">
                                                                        </th>
                                                                    </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <!--Fila del Repeater rpt_calculos-->
                                                                    <tr onmouseout="style.backgroundColor=''" onmouseover="style.backgroundColor='LightBlue'"
                                                                        style="cursor: hand">
                                                                        <td>
                                                                             <%#Container.DataItem("DesCalculo")%>
                                                                        </td>
                                                                        <td>
                                                                             <%#Container.DataItem("Formula")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lbtEditar" Visible=<%#VisibleSegunperfil() %>  runat="server" CommandArgument='<%#""& Container.DataItem("IdCalculo") %>'
                                                                                CommandName='editar' CausesValidation="false"><img src="../images/edit.gif" border="0" align="absmiddle" alt="Editar"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lbtBorrar" Visible=<%#VisibleSegunperfil() %> runat="server"  OnClientClick='return confirmar_borrado();' CommandArgument='<%#""& Container.DataItem("IdSistema")& "#" & Container.DataItem("IdCalculo")%>'
                                                                                CommandName='borrar' CausesValidation="false"><img src="../images/del.gif" border="0" align="absmiddle" alt="Borrar"></asp:LinkButton></td>
                                                                    </tr>
                                                                    <!--Lineas de división Repeater rpt_calculos -->
                                                                    <tr>
                                                                        <td colspan="15">
                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td style="border-top: solid 2px #BABA74; font-size: 2px">
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <!--Fin de Repeater rpt_calculos-->
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </td>
                                                    </tr>
                                                    
                                                </table>
                                            </asp:Panel>
                                        </td>
                                            
                                      </tr>
                                      <tr>
                                        <td align="right">
                                            <asp:Button ID="btnAgregarCalculo" runat="server" CausesValidation="False" CssClass="boton"
                                            Text="Asociar Cálculo a la Agrupación..." Visible=False /></td>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td>
                                            <!--PANEL DE AGREGAR CALCULOS-->
                                            <asp:Panel ID="pnlAgregarCalculos" runat="server" Visible="False" Width="100%" BorderStyle="None">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" class="titListadoCalculos" colspan="2" style="height: 19px">
                                                            &nbsp;<img align="bottom" src="images/AgregarCalculos.GIF" />
                                                            SELECCIONAR CÁLCULO</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="subtitListado" style="height: 11px" valign="middle">
                                                            Tipo&nbsp;</td>
                                                        <td align="left" style="width: 711px; height: 11px">
                                                            <asp:DropDownList ID="ddlTipoCalculo" runat="server" Font-Size="8pt"
                                                                Width="152px" AutoPostBack="True">
                                                                <asp:ListItem Value="0">[Seleccionar]</asp:ListItem>
                                                                <asp:ListItem Value="Q">Caudal&#237;metro</asp:ListItem>
                                                                <asp:ListItem Value="H">Hor&#243;metro</asp:ListItem>
                                                                <asp:ListItem Value="E">Energ&#237;a</asp:ListItem>
                                                                <asp:ListItem Value="V">Volum&#233;trico</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="subtitListado" style="height: 23px">
                                                            Nombre&nbsp;</td>
                                                        <td align="left" class="subtitListado" style="width: 711px; height: 23px"><asp:DropDownList ID="ddlCalculos" runat="server" Font-Size="8pt"
                                                                Width="404px" Enabled="False">
                                                        </asp:DropDownList>
                                                            <asp:Button ID="btnguardar" runat="server" CausesValidation="False" CssClass="boton"
                                                                Text="+" /></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            
                                            
                                        </td>
                                      </tr>
                                  </table>
                        
              </table> 
           
    </table>
     
                      
     
     
                                    
                        
 
 </form>   
                               
</body>
</html>
