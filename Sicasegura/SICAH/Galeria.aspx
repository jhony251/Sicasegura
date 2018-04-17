<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Galeria_old.aspx.vb" Inherits="SICAH_galeria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script type="text/javascript">
 
</script>
 <link href="../styles.css" rel="stylesheet" />
 <link rel="stylesheet" type="text/css" href="../js/LightBox/sample.css" media="screen,tv" />
 <script type="text/javascript" charset="UTF-8" src="../js/LightBox/spica.js"></script>
 <script type="text/javascript" charset="UTF-8" src="../js/LightBox/lightbox_plus.js"></script>
 <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
 
</head>
<body >
    <form id="form1" runat="server">
    <asp:Panel ID="pnlGaleria" Runat="server" Visible="true" height="700px" scrollbars="Auto"  >
    <table id="tprinciapl" cellspacing="0" cellpadding="1" style="width:89%"  >
    <!-- Cabecera -->
     <tr>               
              <td style="width: 900px">
                 <table width="100%">
                     <tr>
                       <asp:Label ID="lblPestanyasArbol" runat="server"></asp:Label><td width="50%" visible="false" >
                           
                       </td>   

                 </tr>
                  
                  </table>
                  
              </td>
                              
             </tr>

    <tr>
      <td style="width: 889px" >
       <asp:Label ID="lblHTML" runat="server"></asp:Label>               
      </td>
   </tr>
   </table>
   </asp:panel>
    </form>
</body>
</html>
