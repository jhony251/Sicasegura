<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CurvasAcequiasARBOL.aspx.vb" Inherits="SICAH_CurvasAcequiasARBOL" TRACE="False"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  <link href="../styles.css" rel="stylesheet" />
  <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script type="text/jscript" language="javascript" src="../js/utilesListados.js"></script>
<script type="text/javascript">
var previousSelectedNode = null; 
var previousSelectedNodeClass = null; 
function onTreeNodeSelect()
    {
        if(previousSelectedNode == null) 
        {
           previousSelectedNode = this; 
           previousSelectedNodeClass = this.className;
        }
        else  
        {
            previousSelectedNode.className = previousSelectedNodeClass;
            previousSelectedNode = this; 

            previousSelectedNodeClass = this.className;
        }
        
        this.className = "nodeSelected"; 
        //alert(this.id);
        //window.parent.document.getElementById("h_lblDescripcionAcequia").innerHTML=document.getElementById(this.id).innerHTML
        
    }

window.onload = function()
{
    var treeview = document.getElementById("<%=TreeView1.ClientID %>");
        var nodes = treeview.getElementsByTagName("a");
     for(i=0;i<nodes.length;i++)
     {
        nodes[i].onclick = onTreeNodeSelect;   
     }
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
     
</script>


  
</head>
<body bgcolor="#EEEAD2">
  <form id="Form1" method="post" runat="server">
      <span id="dsp4"></span><span id="imagepath" style="display:none">../js/calendar/images</span>
        <table style="width: 300px; height: 74px; background-color: white">
         <!-- <tr>
              <td colspan="3" style="height: 15px; width: 296px;">
                           <asp:TextBox ID="txtCauce" runat="server" CssClass="texto" Width="258px"></asp:TextBox>
                  <asp:ImageButton ID="imgBuscarCauce" runat="server" BorderStyle="None" BorderWidth="0px"
                               ImageAlign="Top" ImageUrl="~/SICAH/images/iconos/icoBtnSistemasBuscar2.gif" ToolTip="Buscar en el arbol de cauces" /></td>
          </tr>
            <tr>
                <td colspan="3" style="height: 15px; width: 296px;">
                    <asp:Panel ID="pnlResultadoBusquedas" runat="server" Height="100%" Width="100%">
                    <table width=100%>                                               
                                <asp:Repeater ID=rptBusquedas runat=server>  
                                    <HeaderTemplate>
                                        <tr><td class="tituloSec" colspan=4>Resultados de la Búsqueda</td></tr>
                                        <tr>
                                        <td class="encabListado">Descripción</td>
                                        <td class="encabListado">Tipo</td>
                                        
                                        </tr>
                                    </HeaderTemplate>                                                      
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td>
                                                <a href="../SICAH/InformacionGeneral.aspx?nodotexto=<%#Container.DataItem("Descripcion")%>&nodoclave=<%#Container.DataItem("ClaveNodo2")%>" target="iframeDetalle"><%#Container.DataItem("Descripcion")%></a></td>
                                            <td><%#Container.DataItem("Tipo")%></td>
                                            
                                            
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                                              
                     </table>
                    
                    </asp:Panel>
                </td>
            </tr>
         
          <tr>  -->
         
          <!--<td class="encabListado" style="padding-right: 5px; padding-left: 5px; background-color: #ffffcc; font-weight:bold; " valign="top" colspan="2">Modo</td>
          </tr>
          
          <tr>
          
            <td colspan=2 style=" border:solid 1px black; background-color: #ffffcc" width="80%" id="modo">
                                                 
                <input type="radio" name="seleccionar" value="L" id="rbLecturas" onclick="ActualizarOculto()" checked="CHECKED" style="width:15%" />
                Acceso a Datos<label for="rbLecturas"></label>
              
                <input type="radio" name="seleccionar" value="V" id="rbVisor" onclick="ActualizarOculto()" style="width:15%"  />
                Visor<label for="rbLecturas">&nbsp;</label>
               </td> 
          </tr>-->
          <tr>
              <td colspan="3" style="height: 572px; width: 296px;">
                      <asp:Panel ID="pnlArbol" Runat="server" Visible="true" Width="280px" ScrollBars="Auto" BorderStyle="Inset" Height="559px" Wrap="False" >
                      <asp:TreeView ID="treeView1" runat="server" Width="171px" >
                        <SelectedNodeStyle BackColor="PaleGoldenrod" Font-Bold="True" Font-Underline="True" />
                      </asp:TreeView>
                      </asp:Panel>
              </td>
          </tr>
      </table>      
      <asp:ImageButton ID="imgExportaExcel" runat="server" ImageUrl="~/SICAH/images/Excel.bmp" Visible="False" />
      <br />
  </form>
</body>
</html>
