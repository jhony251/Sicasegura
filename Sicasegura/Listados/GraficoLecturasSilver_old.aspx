<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GraficoLecturasSilver_old.aspx.vb" Inherits="Listados_GraficoLecturasSilver_old" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
        function VerEtiquetas(sesion){        
            var filtro = ""

            filtro += "&ID=" + window.opener.document.getElementById("lblIntervaloFechasQ").outerText;
            filtro += "&NL=" + window.opener.document.getElementById("lblobtenerNumLecturasQ").outerText;
            filtro += "&TNL=" + window.opener.document.getElementById("lblobtenerTotNumLecturasQ").outerText; 
            filtro += "&TA=" + window.opener.document.getElementById("lblCaudalAcumuladoQ").outerText;

            document.location.href="http://localhost:2304/graficoDeLecturas.aspx?sesion=" + sesion + filtro                    
        }
    </script>
</head>
<body onload="VerEtiquetas('<%=Session.SessionID %>')">
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" ID="IntervaloDatos"></asp:Label>
        <asp:Label runat="server" ID="NumLecturas"></asp:Label>
        <asp:Label runat="server" ID="TotNumLecturas"></asp:Label>
        <asp:Label runat="server" ID="TotalAcumulado"></asp:Label>                
    </div>
    </form>
</body>
</html>
