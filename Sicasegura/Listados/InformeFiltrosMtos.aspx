<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformeFiltrosMtos.aspx.vb" Inherits="Listados_InformeFiltrosMtos" %>
<%@ Register TagPrefix="cc1" Namespace="NineRays.Reporting.Web" Assembly="NineRays.Reporting.Web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        var url = document.location.href;        
        var param = url.substring(url.indexOf("?") + 1);
        
        function Redirigir() {
            var objs;
            var cadena;
            objs = document.getElementsByTagName("a");
            for (var i = 0; i < objs.length; i++) {
                cadena = objs[i].href;
                if (cadena.indexOf("guid") != -1) {
                    enlace = Mid(cadena, 0, cadena.indexOf("&"));
                }
            }
            document.location.href = cadena + "&" + param;
        }

        function Mid(str, start, len) {
            // Make sure start and len are within proper bounds
            if (start < 0 || len < 0) return "";
            var iEnd, iLen = String(str).length;
            if (start + len > iLen)
                iEnd = iLen;
            else
                iEnd = start + len;
            return String(str).substring(start, iEnd);
        }

    </script>
</head>
<body onload="javascript:Redirigir()">
    <form id="form1" runat="server">
    <div>
        <cc1:SharpShooterWebViewer id="SharpShooterWebViewer1" runat="server" Width="648px" Height="500px" PageIndex="0" Source="<%# reportGenerator1 %>" ImageFormat="Png" CacheTimeOut="02:00:00" BorderWidth="1px" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; visibility:hidden">
        </cc1:SharpShooterWebViewer>            
    </div>
    </form>
</body>
</html>
