<%@ Page Language="C#" AutoEventWireup="true" CodeFile="esquemas.aspx.cs" Inherits="SICAH_Galeria_galeria" %>
    

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
        <link href="galeria/lightbox/css/screen.css" rel="stylesheet" type="text/css" />
        <link href="galeria/resource/lightbox.css" rel="stylesheet" type="text/css" />
        <link href="galeria/resource/sample.css" rel="stylesheet" type="text/css" />
        <script src="galeria/resource/lightbox_plus.js" type="text/javascript"></script>
  
       
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/stylesheet_opt.css" />
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/theme.css" />
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/utiles.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/menu.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/ddmenu.js"></script>

        <script type="text/javascript">

            function pageLoad() {
            }
    
        </script>
        <script>
            $(window).load(function() {
                $().UItoTop({ easingType: 'easeOutQuart' });
                $('#stuck_container').tmStickUp({});
            }); 
        </script>
        
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="container">
                <div id="container2">
                    <asp:Literal ID="HTML_Links_Sup_Izq"     runat="server"></asp:Literal> 
                    <asp:Literal ID="HTML_Subcabecera_Logos" runat="server"></asp:Literal>
                    <span style="text-shadow:0 0 0;"><asp:Literal ID="HTML_Menu_Navegacion"   runat="server"></asp:Literal></span>
                    <div id="content-wrapper">
                        <p class="breath" style="text-shadow:0 0 0;">
                            <a name="up" id="up"></a>
                                Estas en::
                            <a href="index.aspx"  style="text-shadow:0 0 0;">Inicio</a> > <a href="lecturas.aspx" style="text-shadow:0 0 0;">Lecturas</a> > <a href="inserta-lectura-volumen.aspx" style="text-shadow:0 0 0;">Imágenes</a>
                        </p>
                        <div id="left-column" style="text-shadow:0 0 0;">
                            <ul class="nav-menu">
                                <asp:Literal ID="HTML_Link_Infadm" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Infadm_pdf" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_InfContadores" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Esqumas" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Fotos" runat="server"></asp:Literal>
                            </ul>   
                        </div>
                        <div id="central-content">
                            <asp:Literal ID="LIT_Galeria" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
                </div>      
            </div>
            <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>
        </form>
    </body>
</html>

