<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Perfil_acequiaFlash.aspx.vb" Inherits="SICAH_Perfil_acequiaFlash" %>
<%@Register TagPrefix="ofc" Namespace="OpenFlashChart" Assembly="OpenFlashChart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <script type="text/javascript" src="../aspnet_client/OpenFlashChart/js/swfobject.js"></script>
</head>
<body>
    <form id="form1" runat="server" >
    <div>

    <asp:Panel ID="pnlGraficaCurvas" runat="server">
            <!--grafico -->
    <div id="my_chart1" style=" background-color:#C4BF7E">
    <asp:Label ID="lblTitulo" runat="server" style="color:White; font-weight:bold"></asp:Label>
   
    </div>
    <div id="my_chart" >
      <script type="text/javascript">
    swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "my_chart", "630", "450",  "9.0.0", "expressInstall.swf",  {"data-file":"Perfil_acequiaFlashData.aspx"}  );
</script>
    </div>
    <!--fin de grafico -->
    </asp:Panel>
    
    </div>
    </form>
    
</body>
</html>
