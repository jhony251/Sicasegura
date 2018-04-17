<%@ Page Language="C#" AutoEventWireup="true" CodeFile="obras_general.aspx.cs" Inherits="SICA_obras_general" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<    <head id="Head1" runat="server">
        <title></title>
        <link rel="shortcut icon" href="home/images/favicon.ico" />
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
                    <div id="content-wrapper">
                        <p class="breath">
                            <a name="up" id="up"></a>
                                Estas en::
                            <a href="index.aspx">SICA</a> > <a href="Obras.aspx">Obras</a> > Obras General
                        </p>
                        
                        <div id="left-column">
                            <ul class="nav-menu">
                                    <li class="selected"><a href="obras_general.aspx">Documentacion General</a></li>
                                    <li><a href="obras_administrativa.aspx">Documentacion Administrativa</a></li>
                                    <li><a href="obras_tecnica.aspx">Documentación Técnica</a></li>
                                    <li><a href="obras_economica.aspx">Documentación Económica</a></li>
                                </ul>                        </div>
                       
                        <div id="central-content" >
                            <iframe src="upload/Obras_general_upload.aspx" frameborder="0" scrolling="no" style="height:300px; width:200px;"></iframe>
                        </div>
                    </div>    
                    <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
                </div>      
            </div>
            <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>
        </form>
    </body>
</html>
