<%@ Page Language="VB" AutoEventWireup="false" CodeFile="grafico.aspx.vb" Inherits="SICAH_grafico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Start website specific code -->
    <script src="../../SICAH/HighCharts/jquery.min131.js"type="text/javascript"></script> 
    <script type="text/javascript">
        jQuery.noConflict();
    </script>
    <!-- End website specific code -->
    <style>
        a
        {
        	color:White;
        	font-style:normal;
        	font-variant:normal;
        	
        	
        }
    </style>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
<body style="width:98%; font-family:Verdana; margin:20px; padding-right:60px;">
    <form id="form1" runat="server">
    <table style="width:100%; font-size:8pt; font-weight:bold;" cellpadding="3" cellspacing="0">
        <tr>
            <td colspan="5">
                <div id="chart2" style="width: 90%; height: 300px;"></div>
            </td>
        </tr>
    </table>
     <asp:Literal ID="HTML_JS_Grafico" runat="server"></asp:Literal>
    <script src="../../SICAH/HighCharts/jquery.cycle.all.js" type="text/javascript"></script>
    <script src="../../SICAH/HighCharts/ph.js" type="text/javascript"></script>    
    <script src="../../SICAH/HighCharts/highstock.js" type="text/javascript"></script>
    <%--<script src="HighCharts/frontpage-theme.js" type="text/javascript"></script>--%>   
    <script type="text/javascript">
       /* jQuery(function() { 
                            var chart2 = new Highcharts.Chart({
                            chart: { backgroundColor: '#ffffff', renderTo: 'chart2' },
                                rangeSelector: {selected: 1,inputEnabled: false},
                                title: { text: 'VOLÚMENES SUMINISTRADOS EN EL AÑO HIDROLÓGICO' },
                                legend: {
                                    enabled: true
                                },
                                
                                xAxis: { title: {
                                    text: 'Año Hidrológico'
                                }, type: 'datetime'
                                },
                                yAxis: {
                                    title: {
                                        text: 'hm3'
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
                                                radius: 0,
                                                states: {
                                                    hover: {
                                                        enabled: true
                                                    }
                                                }
                                            }
                                        },
                                        line: {
                                            pointStart: 1940,
                                            marker: {
                                                enabled: false,
                                                symbol: 'circle',
                                                radius: 0,
                                                states: {
                                                    hover: {
                                                        enabled: true
                                                    }
                                                }
                                            }
                                        }
                                    },
                                
                                series: [{ name: 'Suministrado(hm3)', type: 'area', data: Values, tooltip: { yDecimals: 2}},{ name: 'Concesión (hm3)', data: Values2, type: 'line', tooltip: { yDecimals: 2} }]
                                });
                            });*/
                            
    </script>

    </form>
</body>
</html>
