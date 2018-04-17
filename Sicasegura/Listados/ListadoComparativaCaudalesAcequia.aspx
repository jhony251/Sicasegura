<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListadoComparativaCaudalesAcequia.aspx.vb" Inherits="Listados_ListadoComparativaCaudalesAcequia" %>
<%@ Register TagPrefix="cc1" Namespace="NineRays.Reporting.Web" Assembly="NineRays.Reporting.Web" %>

<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link rel="stylesheet" href="../styles.css" />
</head>
<body>
    <form id="form1" runat="server">
<div>
        <cc1:SharpShooterWebViewer id="SharpShooterWebViewer1" runat="server" Width="648px" Height="500px" PageIndex="0" Source="<%# reportGenerator1 %>" ImageFormat="Png" CacheTimeOut="02:00:00" BorderWidth="1px" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 320px">
        </cc1:SharpShooterWebViewer>         
</div>
    </form>    
</body>
</html>
