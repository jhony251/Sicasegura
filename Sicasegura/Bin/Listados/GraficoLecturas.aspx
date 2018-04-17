<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GraficoLecturas.aspx.vb" Inherits="Listados_GraficoLecturas"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gráfico de Lecturas</title>
    <link href="../styles.css" rel="stylesheet" />
    <script type="text/javascript" src="../aspnet_client/OpenFlashChart/js/swfobject.js"></script>
    <script language="javascript" type="text/javascript">
       
    function VerEtiquetas(){        
        if (window.opener.document.getElementById("lblobtenerNumlecturasE")){
            document.getElementById("NumLecturas").outerHTML = "Total lecturas cargadas: <b>" + window.opener.document.getElementById("lblobtenerNumlecturasE").outerText + "</b>";
            document.getElementById("TotNumLecturas").outerHTML = "Total lecturas existentes: <b>" + window.opener.document.getElementById("lblobtenerTotNumLecturasE").outerText + "</b>";
            document.getElementById("TotalAcumuladoKwh").outerHTML = "Total acumulado (Kwh): <b>" + window.opener.document.getElementById("lblobtenervolumenacumulado").outerText + "</b>";
            document.getElementById("TotalAcumulado").outerHTML = "Total acumulado (m3): <b>" + window.opener.document.getElementById("lblobtenervolumenacumuladoE").outerText + "</b>";
            document.getElementById("IntervaloDatos").outerHTML = "Intervalo de datos: <b>" + window.opener.document.getElementById("lblIntervaloFechaE").outerText + "</b>";
        }else if (window.opener.document.getElementById("lblobtenerNumLecturasQ")){
            document.getElementById("NumLecturas").outerHTML = "Total lecturas cargadas: <b>" + window.opener.document.getElementById("lblobtenerNumLecturasQ").outerText + "</b>";
            document.getElementById("TotNumLecturas").outerHTML = "Total lecturas existentes: <b>" + window.opener.document.getElementById("lblobtenerTotNumLecturasQ").outerText + "</b>";
            document.getElementById("TotalAcumulado").outerHTML = "Total acumulado (m3): <b>" + window.opener.document.getElementById("lblCaudalAcumuladoQ").outerText + "</b>";
            document.getElementById("IntervaloDatos").outerHTML = "Intervalo de datos: <b>" + window.opener.document.getElementById("lblIntervaloFechasQ").outerText + "</b>";
        }else if (window.opener.document.getElementById("lblobtenerNumLecturasV")){
            document.getElementById("NumLecturas").outerHTML = "Total lecturas cargadas: <b>" + window.opener.document.getElementById("lblobtenerNumLecturasV").outerText + "</b>";
            document.getElementById("TotNumLecturas").outerHTML = "Total lecturas existentes: <b>" + window.opener.document.getElementById("lblobtenerTotNumLecturasV").outerText + "</b>";
            document.getElementById("TotalAcumulado").outerHTML = "Total acumulado (m3): <b>" + window.opener.document.getElementById("lblobtenervolumenacumuladoV").outerText + "</b>";
            document.getElementById("IntervaloDatos").outerHTML = "Intervalo de datos: <b>" + window.opener.document.getElementById("lblIntervaloFechaV").outerText + "</b>";
        }else if (window.opener.document.getElementById("lblobtenerNumLecturasH")){
            document.getElementById("NumLecturas").outerHTML = window.opener.document.getElementById("lblobtenerNumLecturasH").outerText + "</b>";
            document.getElementById("IntervaloDatos").outerHTML = "Intervalo de datos: <b>" + window.opener.document.getElementById("lblIntervaloFechaH").outerText + "</b>";            
        }                
    }
    
    function CierraVentana(){
        
        /*var el = document.getElementById("form1");
        var padre = el.parentNode;
        padre.removeChild(el);
        
        alert(window.opener.location.href);        
        window.opener.location.href=window.opener.location.href;*/

    }
    </script>
</head>
<body onload="VerEtiquetas()" onunload="CierraVentana()">    
    <form id="form1" runat="server">
    <!-- Gráficos por Tipo Obtención Caudal -->
    <div>                        
        <table cellpadding="0" cellspacing="0" width="100%" style="margin-bottom:50px;">
            <tr><td colspan="2"><asp:Label runat="server" ID="IntervaloDatos"></asp:Label></td></tr>
            <tr>
                <td><asp:Label runat="server" ID="NumLecturas"></asp:Label></td>
                <td><asp:Label runat="server" ID="TotNumLecturas"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="TotalAcumulado"></asp:Label></td>            
                <td><asp:Label runat="server" ID="TotalAcumuladoKwh"></asp:Label></td>
            </tr>        
        </table>
        
        <table>
            <tr>
                <td style="border:1px solid black; background-color: #F8F8D8;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td><asp:PlaceHolder ID="plh_TipoObtencionCaudal" runat="server"></asp:PlaceHolder></td>
                            <td align="right"><asp:LinkButton ID="btnAmpliarTipoObtencionCaudal" CssClass="enlaceLecturas" runat="server" Text="Ampliar"></asp:LinkButton></td>
                        </tr>
                    </table>                    
                </td>
                <td>&nbsp;</td>
                <td valign="top" rowspan="2">
                    <table border="1"><tr><td style="border:1px solid black;">
                        <div id="Gr_TipoObtencionCaudal1"></div>
                    </td></tr></table>
                </td>                
            </tr>
            <tr>
                <td style="border:1px solid black;"><div id="Gr_TipoObtencionCaudal"></div></td>                
                <td>&nbsp;</td>
                                  
            </tr>                                
        </table>
        <script type="text/javascript">            
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_TipoObtencionCaudal", "600", "400",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(1,0)%>"}  );
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_TipoObtencionCaudal1", "250", "200",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(1,1)%>"}  );
        </script>
    </div> 
    <!-- Gráficos por Cód. Fuente Dato -->
    <p>&nbsp;</p>
    <div> 
        <table>
            <tr>
                <td style="border:1px solid black; background-color: #F8F8D8;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td><asp:PlaceHolder ID="plh_CodFuenteDato" runat="server"></asp:PlaceHolder></td>
                            <td align="right"><asp:LinkButton ID="btnAmpliarCodFuenteDato" CssClass="enlaceLecturas" runat="server" Text="Ampliar"></asp:LinkButton></td>
                        </tr>
                    </table>                    
                </td>
                <td>&nbsp;</td>
                <td valign="top" rowspan="2">
                    <table><tr><td style="border:1px solid black;">
                        <div id="Gr_CodFuenteDato1"></div>
                    </td></tr></table>
                </td>
            </tr>
            <tr>
                <td style="border:1px solid black;"><div id="Gr_CodFuenteDato"></div></td>                
                <td>&nbsp;</td>
                                  
            </tr>                                
        </table>     
        <script type="text/javascript">            
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_CodFuenteDato", "600", "400",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(2,0)%>"}  );
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_CodFuenteDato1", "250", "200",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(2,1)%>"}  );
        </script>
    </div>      
    <!-- Gráficos por régimen Curva -->  
    <p>&nbsp;</p>
    <div> 
        <table>
            <tr>
                <td style="border:1px solid black; background-color: #F8F8D8;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td><asp:PlaceHolder ID="plh_RegimenCurva" runat="server"></asp:PlaceHolder></td>
                            <td align="right"><asp:LinkButton ID="btnAmpliarRegimenCurva" CssClass="enlaceLecturas" runat="server" Text="Ampliar"></asp:LinkButton></td>
                        </tr>
                    </table>                    
                </td>
                <td>&nbsp;</td>
                <td valign="top" rowspan="2">
                    <table><tr><td style="border:1px solid black;">
                        <div id="Gr_RegimenCurva1"></div>
                    </td></tr></table>
                </td>
            </tr>
            <tr>
                <td style="border:1px solid black;"><div id="Gr_RegimenCurva"></div></td>                
                <td>&nbsp;</td>
                                  
            </tr>                                
        </table>       
        <script type="text/javascript">            
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_RegimenCurva", "600", "400",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(3,0)%>"}  );
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_RegimenCurva1", "250", "200",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(3,1)%>"}  );
        </script>
    </div>        
    
    </form>
</body>
</html>
