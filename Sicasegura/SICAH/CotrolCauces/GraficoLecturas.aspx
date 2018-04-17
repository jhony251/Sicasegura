<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GraficoLecturas.aspx.cs" Inherits="SICAH_CotrolCauces_GraficoLecturas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <!-- Start website specific code -->
    <script src="../../includes/HighCharts/jquery.min131.js" type="text/javascript"></script> 
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
<body style="width:98%; font-family:Verdana; margin:0px;background-color:#FFFFFF;">
    <form id="form1" runat="server">
    <%--<img src="../../images/cabecera2012.jpg" alt="Cabecera" width="900px" />--%>

    <asp:Literal ID="LIT_grafico" runat="server"></asp:Literal>
    <asp:Literal ID="LIT_grafico2" runat="server"></asp:Literal>
    <asp:Literal ID="LIT_grafico3" runat="server"></asp:Literal>
    <div id="chart1" style="width: 90%;margin-left:5%; margin-right:5%; margin-top:10px; height: 300px;"></div>
    <div id="chart2" style="width: 90%;margin-left:5%; margin-right:5%; margin-top:50px; height: 300px;"></div>
    <%--<div id="chart3" style="width: 70%; margin-top:50px; height: 400px;"></div>--%>


    <script src="../../includes/HighCharts/jquery.cycle.all.js" type="text/javascript"></script>
    <script src="../../includes/HighCharts/ph.js" type="text/javascript"></script>    
    <script src="../../includes/HighCharts/highstock.js" type="text/javascript"></script>
    <%--<script src="HighCharts/frontpage-theme.js" type="text/javascript"></script>--%>
    </form>
</body>
</html>
