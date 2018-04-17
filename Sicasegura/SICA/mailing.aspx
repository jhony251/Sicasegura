<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mailing.aspx.cs" Inherits="SICA_mailing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>Telemedida SICA</title>
        <link rel="shortcut icon" href="http://www.chsegura.es/home/images/favicon.ico" />
        <!--<link rel="stylesheet" href="home/css/style.css">-->
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/stylesheet_opt.css" />
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/theme.css" />
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/utiles.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/menu.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/ddmenu.js"></script>
        <script src="js/jquery.js"></script>
        <style type="text/css">
  	        .table{ width:99%;}   
        </style>
</head>
<body>
    <form id="form1" runat="server">
            <div id="container">
                <div id="container2">
                    <asp:Literal ID="HTML_Links_Sup_Izq"     runat="server"></asp:Literal> 
                    <asp:Literal ID="HTML_Subcabecera_Logos" runat="server"></asp:Literal>
                    <asp:Literal ID="HTML_Menu_Navegacion"   runat="server"></asp:Literal>
                   
                    <div id="left-column">
                   
                   
                    </div>
                   
                    <div id="central-content"  style="width:99%">
                        <div class="table">
                            <asp:Literal ID="HTML_Tabla_listado_Mailing"   runat="server"></asp:Literal>
                        </div>
                    </div>
                   
                   <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
                   
                </div>      
            </div>
            <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>

    	    
    
    	    

        </form>
</body>
</html>
