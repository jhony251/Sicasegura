<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MenuPrincipal.aspx.vb" Inherits="SICAH_MenuPrincipal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
     <link rel="stylesheet" href="..\styles.css">
     <script language="javascript" src="..\js/utilesMenus.js"></script>     
  <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
  <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
  <meta name="vs_defaultClientScript" content="JavaScript">
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body bgcolor="#EEEAD2">

  <form id="Form1" method="post" runat="server" >
    <table cellspacing="2" align="center" width="100%" style="border: 1px solid #666666;
      background-color: white">
      <tr>
        <td>
          <table cellspacing="0" cellpadding="1" width="100%">
            <asp:Label ID="lblCabecera" runat="server"></asp:Label>
            <asp:Label ID="lblPestanyas" runat="server"></asp:Label>
           </table>
           <table width="100%" align="center">
            <tr>
            <td style="background-color:#E1DA93">
            &nbsp;
            </td>
            <td width=1% align="center">
            <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" width="700" height="500" id="pelicula" align="middle">
            <param name="allowScriptAccess" value="sameDomain" />
            <param name="movie" value="../images/pelicula.swf" />
            <param name="quality" value="high" />
            <param name="bgcolor" value="#ffffff" />
            <embed src="../images/pelicula.swf" quality="high" bgcolor="#ffffff" width="700" height="500" name="pelicula" align="middle" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
            </object>
            
            </td>
            <td style="background-color:#E1DA93">
            &nbsp;
            </td>
                
            </tr>
           </table> 
          
        </td>
      </tr>
  
    </table>
  </form>
</body>
</html>
