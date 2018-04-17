﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eliminaobra.aspx.cs" Inherits="Obras_home_eliminaobra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>Eliminar Obra de CHS</title>
        
        <!--<link rel="stylesheet" href="home/css/style.css">-->
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/stylesheet_opt.css" />
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/theme.css" />
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/utiles.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/menu.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/ddmenu.js"></script>
        
        
    </head>
<body>
    <form id="form2" runat="server">
        <div id="container">
            <div id="container2">
                <asp:Literal ID="HTML_Links_Sup_Izq"     runat="server"></asp:Literal> 
                <asp:Literal ID="HTML_Subcabecera_Logos" runat="server"></asp:Literal>
                <asp:Literal ID="HTML_Menu_Navegacion"   runat="server"></asp:Literal>
                    <div id="content-wrapper">
                        <p class="breath">
                            <a name="up" id="up"></a>
                                Estas en::
                            <a href="iliminaobra.aspx">ELIMINAR OBRA</a> 
                        </p>
                        <div id="left-column">
                            <ul class="nav-menu">
                                <asp:Literal ID="HTML_Link_InformacionGeneral" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_DocumentacionAdministrativa" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_DocumentacionTecnica" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_DocumentacionEconomica" runat="server"></asp:Literal>
                            </ul>
                        </div>
                        <div id="central-content" ><center><img src="images/borrarobra.JPG" /></center></div>
                        <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
                    </div>
            </div>      
        </div>
        <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>
        
    </form>
</body>
</html>
