<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccesoTopkapi.aspx.vb" Inherits="SICAH_AccesoTopkapi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<script type="text/javascript">
 
</script>
 <link href="../styles.css" rel="stylesheet" />
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <script language="javascript" src="..\js/utilesMenus.js"></script>  
</head>
<body >
    <form id="form1" runat="server">
      <table style="width: 100%">
      <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label>
    </table>
    <table id="tprinciapl" cellspacing="0" cellpadding="1" style="width:89%" >
    <tr>
      <td style="width: 1000px">
        <iframe height="750px" width="1200px" runat="server" id="iframeVisor" scrolling="no"  >
        </iframe>                 
      </td>
   </tr>
   </table>
    </form>
</body>
</html>
