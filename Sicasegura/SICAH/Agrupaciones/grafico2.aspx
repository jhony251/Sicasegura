<%@ Page Language="VB" AutoEventWireup="false" CodeFile="grafico2.aspx.vb" Inherits="SICAH_grafico2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Start website specific code -->
    <script src="../HighCharts/jquery.min131.js" type="text/javascript"></script> 
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
<body style="width:98%; font-family:Verdana; margin:0px;background-color:#CCFFAA;">
    <form id="form1" runat="server">
    <table style="width:100%; font-size:8pt; font-weight:bold; " cellpadding="3" cellspacing="0">
          <tr>
            <td colspan="5">
                <div id="chart2" style="width: 100%;  height: 232px;"></div>
            </td>
        </tr>
    </table>
    
   <script type="text/javascript">
       jQuery(function() {
           var chart2 = new Highcharts.Chart({
           chart: { backgroundColor: '#CCFFAA', renderTo: 'chart2' },
               rangeSelector: { selected: 1, inputEnabled: false },
               navigator: { height: 60 },
               title: { text: 'Totales', align: 'center' },
               legend: {
                   enabled: false
               },
               xAxis: {
                        title: { text: 'Años Hidrológicos' },
                        type: 'datetime'
               }, 
               /*xAxis:
               {    
                    title: { text: 'Años Hidrológicos'},
                    categories: ['04/05', '05/06', '07/08', '09/10', '10/11', '11/12']
               },*/
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
                series: [{ name: 'Concesión (Hm3)', type: 'area', data: Values2, tooltip: { yDecimals: 2} }, { name: 'Consumo (Hm3)', type:'column', data: Values, tooltip: { yDecimals: 2}}]
               });
           });
    </script>
    <script src="../HighCharts/jquery.cycle.all.js" type="text/javascript"></script>
    <script src="../HighCharts/ph.js" type="text/javascript"></script>    
    <script src="../HighCharts/highstock.js" type="text/javascript"></script>
    <%--<script src="HighCharts/frontpage-theme.js" type="text/javascript"></script>--%>
    </form>
</body>
</html>
