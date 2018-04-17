<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformeVolumenes.aspx.vb" Inherits="SICAH_InformeVolumenes" %>
<%@ Register TagPrefix="cc1" Namespace="NineRays.Reporting.Web" Assembly="NineRays.Reporting.Web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Informe Volúmenes</title>
    <link rel="stylesheet" href="../styles.css" />
</head>
<body>
    <form id="form1" runat="server">
    
    </form>
    <cc1:SharpShooterWebViewer id="SharpShooterWebViewer1" runat="server" Width="648px" Height="500px" PageIndex="0" Source="<%# reportGenerator1 %>" CssClass="webView" PagerCssClass="webViewPager" ImageFormat="Png" CacheTimeOut="02:00:00" BorderWidth="1px" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute;">
    </cc1:SharpShooterWebViewer>    
</body>
</html>
