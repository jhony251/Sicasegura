<%@ Page Language="C#" AutoEventWireup="true" CodeFile="grafico.aspx.cs" Inherits="PDA_grafico" %>





<%@ Register assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>





<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--<meta name="viewport" content="height=830,user-scalable=yes" /> -->
    <title>Página sin título</title>
    <script src="wz_rotateimg.js" type="text/javascript"></script>
    <style type="text/css">
    </style>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GFLUVIALConnectionString %>" 
            SelectCommand="SELECT DISTINCT [CodigoPVYCR] FROM [PVYCR_DatosAcequias_NIVUS]">
        </asp:SqlDataSource>

        
    </div>
    
    <asp:Chart ID="Chart1" runat="server">
        <series>
            <asp:Series Name="Series1">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
    
    </form>
    
</body>
</html>
