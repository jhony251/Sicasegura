<%@ Page Language="VB" AutoEventWireup="false" CodeFile="grafico.aspx.vb" Inherits="SICAH_Calidad_grafico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Start website specific code -->
    <script src="HighCharts/jquery.min131.js" type="text/javascript"></script> 
    <script type="text/javascript">
        jQuery.noConflict();
    </script>
    <!-- End website specific code -->
    <style>
        a
        {
        	color:gray;
        	font-style:normal;
        	font-variant:normal;
        }
        
        tr.header-default 
        {
        	color:#04408C;
            font-size: 11px;
            line-height: 26px; 
            border-color: #99BCE8;
            border-width: 1px;
            border-style: solid;
            background-image: none;
            background-color: #CBDDF3;
            background-image: -webkit-gradient(linear, 50% 0%, 50% 100%, color-stop(0%, #DAE7F6), color-stop(45%, #CDDEF3), color-stop(46%, #ABC7EC), color-stop(50%, #ABC7EC), color-stop(51%, #B8CFEE), color-stop(100%, #CBDDF3));
            background-image: -webkit-linear-gradient(top, #DAE7F6,#CDDEF3 45%,#ABC7EC 46%,#ABC7EC 50%,#B8CFEE 51%,#CBDDF3);
            background-image: -moz-linear-gradient(top, #DAE7F6,#CDDEF3 45%,#ABC7EC 46%,#ABC7EC 50%,#B8CFEE 51%,#CBDDF3);
            background-image: -o-linear-gradient(top, #DAE7F6,#CDDEF3 45%,#ABC7EC 46%,#ABC7EC 50%,#B8CFEE 51%,#CBDDF3);
            background-image: -ms-linear-gradient(top, #DAE7F6,#CDDEF3 45%,#ABC7EC 46%,#ABC7EC 50%,#B8CFEE 51%,#CBDDF3);
            background-image: linear-gradient(top, #DAE7F6,#CDDEF3 45%,#ABC7EC 46%,#ABC7EC 50%,#B8CFEE 51%,#CBDDF3);
            -moz-box-shadow: #f4f8fd 0 1px 0px 0 inset;
            -webkit-box-shadow: #f4f8fd 0 1px 0px 0 inset;
            -o-box-shadow: #f4f8fd 0 1px 0px 0 inset;
            box-shadow: #f4f8fd 0 1px 0px 0 inset;
        }
    </style>
    <!--Este parámetro Line-Height de la clase header-default define el alto de la cabecera del gráfico-->
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
<body style="width:100%; font-family:Verdana; margin:0px;">
    <form id="form1" runat="server">
    <table style="width:100%; font-size:8pt; font-weight:bold; color:White;" cellpadding="0" cellspacing="0">
        <tr style="background-color:#507CD1;" class="header-default">
            <td style="width:20%;text-align:center;">
                <asp:Literal ID="LIT_LinkPH" runat="server"></asp:Literal>PH</td>
            <td style="width:20%;text-align:center;">
                <asp:Literal ID="LIT_LinkConduct" runat="server"></asp:Literal>Conductividad</td> 
            <td style="width:20%;text-align:center;">
                <asp:Literal ID="LIT_LinkTurb" runat="server"></asp:Literal>Turbidez</td>
            <td style="width:20%;text-align:center;">
                <asp:Literal ID="LIT_LinkLDO" runat="server"></asp:Literal>LDO</td>
            <td style="width:20%;text-align:center;">
                <asp:Literal ID="LIT_LinkTemp" runat="server"></asp:Literal>Temperatura</td>
        </tr>
        <tr>
            <td colspan="5">
                <div id="chart2" style="width: 100%; margin-left: 0px; height: 387px;"></div>
                <!--Los Width y height de este contenedor definen el tamaño del gráfico-->
            </td>
        </tr>
    </table>
    
    <script type="text/javascript">
        jQuery(function() { 
                            var chart2 = new Highcharts.StockChart({
                                chart: {renderTo: 'chart2'},
                                rangeSelector: {selected: 1,inputEnabled: false},
                                navigator: { height: 60 },
                                title: { text: 'Valores', floating: true, align: 'right', x: -20, top: 20 },
                                xAxis: {maxZoom: 1 * 24 * 3600000},
                                series: [{ name: 'Valores', data: Values, tooltip: { yDecimals: 4}}]
                                });
                            });
    </script>
    <script src="HighCharts/jquery.cycle.all.js" type="text/javascript"></script>
    <!--<script src="Calidad/HighCharts/ph.js" type="text/javascript"></script>-->
    <script src="HighCharts/highstock.js" type="text/javascript"></script>
    </form>
</body>
</html>
