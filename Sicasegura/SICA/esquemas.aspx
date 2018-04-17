<%@ Page Language="C#" AutoEventWireup="true" CodeFile="esquemas.aspx.cs" Inherits="SICA_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                            <a href="index.aspx">SICA</a> > <a href="esquemas.aspx">Esquemas</a>
                        </p>
                        <div id="left-column">
                            <ul class="nav-menu">
                                <asp:Literal ID="HTML_Links_puntoscontrol" runat="server"></asp:Literal>
                            </ul>
                        </div>
                       
                        <div id="central-content">
                            <div>
                                <asp:FileUpload ID="FileUpload1" runat="server" multiple="multiple" />
                                <asp:Button ID="Button1" runat="server" Text="Subir Fichero" onclick="Button1_Click" /><br /><br />
                                <asp:Literal ID="HTML_Galeria" runat="server"></asp:Literal>
                            </div>
                        </div>
                   </div>
                   <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
                   
                </div>      
            </div>
            <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>
        </form>
    </body>
</html>
