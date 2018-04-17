<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GraficoAmpliado.aspx.vb" Inherits="Listados_GraficoAmpliado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gráfico de Lecturas</title>
    <link href="../styles.css" rel="stylesheet" />
    <script type="text/javascript" src="../aspnet_client/OpenFlashChart/js/swfobject.js"></script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="border:1px solid black;">
                    <div id="Grafico"></div>
                </td>
            </tr>
        </table>
        <script type="text/javascript">            
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Grafico", "800", "600",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico()%>"}  );            
        </script>
    </div>
    </form>
</body>
</html>
