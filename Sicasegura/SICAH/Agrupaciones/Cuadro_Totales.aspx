<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cuadro_Totales.aspx.cs" Inherits="SICAH_Agrupaciones_Cuadro_Totales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../HighCharts/jquery.min131.js" type="text/javascript"></script> 
    <script type="text/javascript">
    
      function pageLoad() {
      }
    
    </script>
    <style type="text/css">
        
        .style2
        {
            width: 100px;
            font-family: Tahoma;
            font-size: 7pt;
            vertical-align:bottom;
        }
    </style>
</head>
<body style="background-color:#CCFFAA;">
    <form id="form1" runat="server">
        <div>

            <table style="width:100%;">
                <tr>
                    <td class="style2">
                        Fecha de inicio</td>
                    <td class="style2   ">
                        Fecha Fin</td>
                    <td class="style2">
                        &nbsp;</td>
                    <td rowspan="7" valign="top"><div id="chart2" style="width: 100%; height: 200px;"></div>
                   
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:TextBox ID="TXT_FI" runat="server" Width="70"></asp:TextBox>
                        <asp:ImageButton ID="TXT_FI_BTN" runat="server"  Text="#" 
                            Width="15px" onclick="TXT_FI_Click" ImageUrl="../../images/calendario.gif" />
                        <div style="position:relative;"><asp:Calendar ID="Calendar1" runat="server" 
                                Visible="false" onselectionchanged="Calendar1_SelectionChanged">
                            <SelectedDayStyle BackColor="#CCCC00" BorderColor="#006600" />
                            </asp:Calendar></div>
                    </td>
                    <td class="style2"><asp:TextBox ID="TXT_FF" runat="server" Width="70"></asp:TextBox>
                        <asp:ImageButton ID="TXT_FF_BTN" runat="server"  Text="#" 
                            Width="15px" onclick="TXT_FF_Click" ImageUrl="../../images/calendario.gif"/>
                        <div style="position:relative;"><asp:Calendar ID="Calendar2" runat="server" 
                                Visible="false" onselectionchanged="Calendar2_SelectionChanged">
                            <SelectedDayStyle BackColor="#CCCC00" BorderColor="#006600" />
                            </asp:Calendar></div>
                    </td>
                    <td class="style2">
                        <asp:Button ID="Button1" runat="server" Height="22px" Text="Actualizar" 
                            onclick="Button1_Click" Width="75px" />
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="LBL_Concesion" runat="server" Text="Concesión (m3)"></asp:Label></td>
                    <td class="style2">
                        <asp:Label ID="LBL_Concesion_total" runat="server" Text="Concesión total (m3)"></asp:Label></td>
                    <td class="style2">
                        <asp:Label ID="LBL_Concesion_modificacion" runat="server" Text="Modificación (m3)"></asp:Label></td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:TextBox ID="TXT_Concesion" runat="server" Width="75px" style="text-align:right;"></asp:TextBox>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="TXT_Concesion_total" runat="server" Width="75px" style="text-align:right;"></asp:TextBox>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="TXT_Concesion_temporal" runat="server" Width="75px" style="text-align:right;"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="style2"><asp:Label ID="LBL_Consumido_vol" runat="server" Text="Consumo (m3)"></asp:Label></td>
                    <td class="style2"><asp:Label ID="LBL_Consumido_percent" runat="server" Text="Consumo (%)"></asp:Label></td>
                    <td class="style2"></td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:TextBox ID="TXT_Consumido_vol" runat="server" Width="75px" style="text-align:right;"></asp:TextBox>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="TXT_Consumido_percent" runat="server" Width="75px" style="text-align:right;"></asp:TextBox>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="TXT_Inscripcion" runat="server" Width="75px" Visible="false" style="text-align:right;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;<br />&nbsp;<br />&nbsp;</td><td>&nbsp;<br />&nbsp;<br />&nbsp;</td><td>&nbsp;<br />&nbsp;<br />&nbsp;</td>
                </tr>
            </table> 
        </div>
        
         <script type="text/javascript">
             jQuery(function() {
                 var chart2 = new Highcharts.Chart({
                     chart: { backgroundColor: '#CCFFAA', renderTo: 'chart2' },
                     rangeSelector: { selected: 1, inputEnabled: false },
                     title: { text: 'Parciales y Acumulados' },
                     legend: {
                         enabled: false
                     },

                     xAxis: { type: 'datetime'},
                     yAxis: {
                         title: {
                             text: 'Hm3'
                         },
                         plotLines: [{
                             value: 0,
                             width: 1,
                             color: '#808080'}]
                         },
                         plotOptions: {
                             area: {
                                 pointStart: 1940,
                                 marker: {
                                     enabled: false,
                                     symbol: 'circle',
                                     radius: 2,
                                     states: {
                                         hover: {
                                             enabled: true
                                         }
                                     }
                                 }
                             }
                         },
                         series: [{ name: 'Concesión (Hm3)', data: Values2, type: 'area', tooltip: { yDecimals: 2} }, { name: 'Acumulado (m3)', data: Values1, type: 'area', tooltip: { yDecimals: 3}}] //, { name: 'Consumo(m3)', type: 'area', data: Values, tooltip: { yDecimals: 5}}
                     });
                 });
         </script>
         <script src="../HighCharts/jquery.cycle.all.js" type="text/javascript"></script>
         <script src="../HighCharts/ph.js" type="text/javascript"></script>    
         <script src="../HighCharts/highstock.js" type="text/javascript"></script>
        
        
        
    </form>
    </body>
</html>
