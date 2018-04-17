<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones_new.aspx.vb" Inherits="SICAH_Agrupaciones_new" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
<title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
 
</head>
<body >
  <form id="Form1" method="post" runat="server"  >
    <table cellspacing="0" align="center" style="border: 1px solid #666666;background-color:#eeead2;" width=100% >
      <tr>
        <td style="height: 830px">
          <table cellspacing="0" cellpadding="1" style="width:100%;background-color:#eeead2" >
          <tr><td>
            <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label>
          </td></tr>
          </table>
            <table cellpadding="0" cellspacing="0" style="background-color:#eeead2" >
                <tr>
                   <td  valign="top">
                        <iframe name="iframeBuscar" src="Agrupaciones_Arbol.aspx" runat="server" id="iframeBuscar" style="width:300px; height:670px;"  frameborder="0" marginheight="0" marginwidth="0" scrolling="no" class="HTMframe" ></iframe>
                    </td>
                    
                    
                    <td  bgcolor="#eeead2">                        
                        <img src="images/flecha_reducir.gif" id="imgflecha" onclick="OcultarMostrarArbol()" />
                    </td>
 
                    <td valign="top" >
                       <iframe name="iframeDetalle" id="iframeDetalle"  height="670" src="Agrupaciones_detalle.aspx" frameborder="0" marginheight="0" marginwidth="0" class="HTMframe" style="width: 900px " scrolling="no" ></iframe>
                    </td>                    
                </tr>
                
            </table>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>