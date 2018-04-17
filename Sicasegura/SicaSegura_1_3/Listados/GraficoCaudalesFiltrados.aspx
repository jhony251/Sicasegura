<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GraficoCaudalesFiltrados.aspx.vb" Inherits="Listados_GraficoCaudalesFiltrados"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gráfico Caudales Filtrados</title>
    <link href="../styles.css" rel="stylesheet" />
    <script type="text/javascript" src="../aspnet_client/OpenFlashChart/js/swfobject.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>  
    <!-- Volumen y Caudalímetro -->  
    <table cellpadding="0" cellspacing="0" width="100%" style="margin-bottom:50px;">
        <tr><td style="font-weight:bold;"><u> FILTROS APLICADOS:</u></td></tr>
        <tr>
            <td valign="top"><asp:Label ID="lblFiltro1" runat="server"></asp:Label></td>
            <td rowspan="2" valign="top"><asp:Label ID="lblTotales" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblFiltro2" runat="server"></asp:Label></td>            
        </tr>        
    </table>
    <asp:Panel ID="pnl_VyQ" runat="server">
        <table>        
            <tr>
                <td style="border:1px solid black; background-color: #F8F8D8;">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkVar1" runat="server" Text="" AutoPostBack="True" CausesValidation="False" Checked="true" /><br />
                                <asp:CheckBox ID="chkVar2" runat="server" Text="" AutoPostBack="True" CausesValidation="False" Checked="False" /><br />
                                <asp:CheckBox ID="chkVar3" runat="server" Text="" AutoPostBack="True" CausesValidation="False" checked="False" /><br />                                
                            </td>
                            <td align="right" valign="bottom">
                                <asp:LinkButton ID="btnAmpliar" CssClass="enlaceLecturas" runat="server" Text="Ampliar" style="text-align:right"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid black;" colspan="2"><div id="Gr_Caudales"></div></td>                
            </tr>           
                     
        </table>
        <script type="text/javascript">
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_Caudales", "900", "400",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(1)%>"}  );            
        </script>
    </asp:Panel>  
    <!-- Fin Volumen y Caudalímetro -->
    
    <!-- Energía -->
    <asp:Panel ID="pnl_E" runat="server">
        <table>
            <tr>
                <td style="border:1px solid black; background-color: #F8F8D8;">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblVar1E" runat="server" Text=""><br /></asp:Label>                                
                            </td>
                            <td align="right" valign="bottom">
                                <asp:LinkButton ID="btnAmpliarE1" CssClass="enlaceLecturas" runat="server" Text="Ampliar" style="text-align:right"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid black;" colspan="2"><div id="Gr_EnergiaKvar"></div></td>                
            </tr>           
                     
        </table>
        <script type="text/javascript">
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_EnergiaKvar", "900", "400",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(3)%>"}  );            
        </script>
        <table>
            <tr>
                <td style="border:1px solid black; background-color: #F8F8D8;">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkVar2E" runat="server" Text="" AutoPostBack="True" CausesValidation="False" Checked="true" ForeColor="Black" /><br />                                
                                <asp:CheckBox ID="chkVar3E" runat="server" Text="" AutoPostBack="True" CausesValidation="False" Checked="false" ForeColor="Black" /><br />                                
                            </td>
                            <td align="right" valign="bottom">
                                <asp:LinkButton ID="btnAmpliarEnergia2" CssClass="enlaceLecturas" runat="server" Text="Ampliar" style="text-align:right"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid black;" colspan="2"><div id="Gr_EnergiaKw"></div></td>                
            </tr>           
                     
        </table>
        <script type="text/javascript">
            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_EnergiaKw", "900", "400",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(4)%>"}  );            
        </script>
    </asp:Panel>
    <!-- Fin Energía -->
    
    <!-- Diferenciales -->
    <asp:Panel ID="pnl_Diferenciales" runat="server">
    <table>        
        <tr>
            <td style="border:1px solid black; background-color: #F8F8D8;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkVar4" runat="server" Text="" AutoPostBack="True" CausesValidation="False" Checked="true" /><br />
                            <asp:CheckBox ID="chkVar5" runat="server" Text="" AutoPostBack="True" CausesValidation="False" Checked="false" /><br />                                                
                        </td>
                        <td align="right" valign="bottom">
                            <asp:LinkButton ID="btnAmpliarDiferenciales" CssClass="enlaceLecturas" runat="server" Text="Ampliar" style="text-align:right"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblLeyendaElectricos" runat="server" ForeColor="DarkRed" Text="(*) Los valores que se muestran son valores estimados"
                    Visible="False" Width="270px"></asp:Label></td>
        </tr>
        <tr>
            <td style="border: 1px solid black;" colspan="2"><div id="Gr_Diferenciales"></div></td>                
        </tr>           
                 
    </table>
    <script type="text/javascript">
        swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "Gr_Diferenciales", "900", "400",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico(2)%>"}  );            
    </script>
    </asp:Panel>
    <!-- Fin Diferenciales -->    
        </div>
    </form>
</body>
</html>
