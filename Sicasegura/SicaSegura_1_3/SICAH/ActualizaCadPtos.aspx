<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ActualizaCadPtos.aspx.vb" Inherits="SICAH_ActualizaCadPtos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
       <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
       <link rel="stylesheet" href="..\styles.css">
     <link href="../styles.css" rel="stylesheet" />
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
    <script language="javascript" src="../js/utilesMenus.js"></script>  

</head>
<body bgcolor="#EEEAD2">
    <form id="form1" runat="server"  method=post>
    <span id="dsp4"></span>
       <table width="100%" cellspacing="2" align="center" style="border: 1px solid #666666;background-color: white">
       <tr>
        <td>
           <table cellspacing="0" cellpadding="1" width="100%" >
           <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label><tr>
              <td >
                <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <!-- Celda Menú - Contenido -->
                <tr>
                <!-- Celda Menú -->
                    <td valign="top" style="padding-top: 20px; width:20%;">
                    <%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%>
                    </td>   
                    <!-- línea vertical que separa el menú de los datos -->
                    <td style="background-image:url(../images/dot2.gif);width:3px"></td>
                   <!-- Fin Celda Menú -->                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%;" valign=top >  
                    <asp:Panel ID="pnlMostrar" runat="server" Visible="true" CssClass="capa" ><table><tr><td style="padding-top:180px; padding-left:280px; font-size:20px";><asp:Label runat="server" ID="lblMostrar" Text="ACTUALIZANDO CADUCIDAD DE PUNTOS..."></asp:Label></td></tr></table> </asp:Panel>
                    <asp:Panel ID="pnlOcultar" runat="server" Visible="false" CssClass="capa"><table><tr><td style="padding-top:180px; padding-left:280px; font-size:20px"; ><asp:Label runat="server" ID="Label1" Text="ACTUALIZACIÓN FINALIZADA"></asp:Label></td></tr></table></asp:Panel>
                    </td>
                </tr>
                </table>
              </td></tr>
            </table>
        </td>
       </tr>
     </table>
    </form>
</body>
</html>