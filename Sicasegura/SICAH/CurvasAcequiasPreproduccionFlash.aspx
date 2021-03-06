<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CurvasAcequiasPreproduccionFlash.aspx.vb" Inherits="SICAH_CurvasAcequiasPreproduccionFlash" %>
<%@Register TagPrefix="ofc" Namespace="OpenFlashChart" Assembly="OpenFlashChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <script src="HighCharts/jquery.min131.js" type="text/javascript"></script> 
    <script type="text/javascript">
        jQuery.noConflict();
    </script>
    <%--<script type="text/javascript" src="../aspnet_client/OpenFlashChart/js/swfobject.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <img src="../images/cabecera2012.jpg" alt="Cabecera" width="900px" />
    <!-- IPIM 10/12/2008 Para hacer las gr�ficas distinguiendo los 3 posibles filtros 
    <div>
     <asp:Panel ID="pnlGraficaCurvas" runat="server">-->
    <!--grafico -->
    <!--<div id="my_chart1" style=" background-color:#C4BF7E">
      
    </div>
    <div id="my_chart" >
      <script type="text/javascript">
    //swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "my_chart", "630", "450",  "9.0.0", "expressInstall.swf",  {"data-file":"CurvasAcequiasPreproduccionFlashData.aspx"}  );
      </script>
    </div>-->
    <!--fin de grafico -->    
    <!--</asp:Panel>    
    </div>
    -->
    <!-- Gr�ficos por Tipo Obtenci�n Caudal -->
    <%--<div>     
        <table>            
            <tr>            
                <td style="border:1px solid black; background-color: #F8F8D8;">
                    <asp:PlaceHolder ID="plh_TipoObtencionCaudal" runat="server"></asp:PlaceHolder>
                </td>                        
            </tr>
            <tr>
                <td style="border:1px solid black;"><div id="Gr_TipoObtencionCaudal">
                </div>
                </td>                                
            </tr>                                
        </table>
        <script type="text/javascript">            
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_TipoObtencionCaudal", "600", "400",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(1)%>"}  );
        </script>
    </div> --%>
    <!-- Gr�ficos por C�d. Fuente Dato -->
    <%--<asp:Panel ID="pnlCodigoFuenteDato" runat="server">
    <p>&nbsp;</p>
    <div>         
        <table>
            <tr>
                <td style="border:1px solid black; background-color: #F8F8D8;">
                    <asp:PlaceHolder ID="plh_CodFuenteDato" runat="server"></asp:PlaceHolder>
                </td>                    
            </tr>
            <tr>
                <td style="border:1px solid black;"><div id="Gr_CodFuenteDato"></div></td>                
            </tr>                                
        </table>     
       <script type="text/javascript">            
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_CodFuenteDato", "600", "400",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(2)%>"}  );            
        </script>
    </div>      
    </asp:Panel>--%>
    <!-- Gr�ficos por r�gimen Curva -->  
    <%-- <div>         
        <table>
            <tr>
                <td style="border:1px solid black; background-color: #F8F8D8;">
                    <asp:PlaceHolder ID="plh_RegimenCurva" runat="server"></asp:PlaceHolder>
                </td>               
            </tr>
            <tr>
                <td style="border:1px solid black;"><div id="Gr_RegimenCurva"></div></td>                
            </tr>                                
        </table>       
        <script type="text/javascript">            
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_RegimenCurva", "600", "400",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(3)%>"}  );            
        </script>
    </div>   --%>     
   
    <asp:Literal ID="LIT_grafico" runat="server"></asp:Literal>
    <asp:Literal ID="LIT_grafico2" runat="server"></asp:Literal>
    <asp:Literal ID="LIT_grafico3" runat="server"></asp:Literal>
    <div id="chart1" style="width: 70%; margin-top:50px; height: 400px;"></div>
    <div id="chart2" style="width: 70%; margin-top:50px; height: 400px;"></div>
    <div id="chart3" style="width: 70%; margin-top:50px; height: 400px;"></div>
    <script src="HighCharts/jquery.cycle.all.js" type="text/javascript"></script>
    <!--<script src="Calidad/HighCharts/ph.js" type="text/javascript"></script>-->
    <script src="HighCharts/highstock.js" type="text/javascript"></script>
    </form>
</body>
</html>
