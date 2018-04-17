<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListadoContadorPantalla.aspx.vb" Inherits="Listados_ListadoContadorPantalla" %>
<%@ Register TagPrefix="cc1" Namespace="NineRays.Reporting.Web" Assembly="NineRays.Reporting.Web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
</head>
<body>
    <form id="form1" runat="server">
 
        <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label>
          <cc1:SharpShooterWebViewer id=SharpShooterWebViewer1 runat="server" Width="850" Height="500px" PageIndex="0" Source="<%# reportGenerator1 %>" ImageFormat="Png" CacheTimeOut="02:00:00" BorderWidth="1px" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 20px">
    </cc1:SharpShooterWebViewer>    
    </form>
</body>
</html>
