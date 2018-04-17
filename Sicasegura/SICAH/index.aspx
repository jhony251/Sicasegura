<%@ Page Language="VB" AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="SICAH_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
<link rel="stylesheet" href="../styles.css" />
  <script language="javascript" src="..\js/utilesMenus.js"></script>  
</head>
<body bgcolor="#EEEAD2">

<form id="Form1" method="post" runat="server">
<table cellspacing="2" align="center" width="100%" style="border:1px solid #666666;background-color:white">
<tr><td>

<table cellspacing="0" cellpadding="1" width="100%">
<asp:label ID="lblCabecera" Runat="server"></asp:label>
<asp:label ID="lblPestanyas" Runat="server"></asp:label>
<tr><td><table width="100%" height="400">
<!-- Celda Menú - Contenido -->
<tr>
    <!-- Celda Menú -->
    <td valign="top" style="padding-top: 20px; width:183px;">
        <%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%>
    </td>
    <!-- línea vertical que separa el menú de los datos -->
    <td style="background-image:url(../images/dot2.gif);width:3px"></td>
    <!-- Fin Celda Menú -->
    
    <!-- Celda Contenido -->
    <td align="center">
        <b>MANTENIMIENTOS</b><br /><br /><img src="../SICAH/images/MtoSubSistemas.gif" />
    </td>
    <!-- Fin Celda Contenido -->
</tr>
<!-- Fin Celda Menú - Contenido -->
</table></td></tr>
</table>

</td></tr></table>
</form>
</body>
</html>
