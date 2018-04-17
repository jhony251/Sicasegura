<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="SICA_Inscripciones_Index" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="shortcut icon" href="http://www.chsegura.es/home/images/favicon.ico" />
        <!--<link rel="stylesheet" href="home/css/style.css">-->
        <link rel="stylesheet" href="https://www.chsegura.es/export/system/modules/es.chsegura.www/resources/stylesheet_opt.css" />
        <link rel="stylesheet" href="https://www.chsegura.es/export/system/modules/es.chsegura.www/resources/theme.css" />
        <script src="https://www.chsegura.es/export/system/modules/es.chsegura.www/resources/utiles.js"></script>
        <script src="https://www.chsegura.es/export/system/modules/es.chsegura.www/resources/menu.js"></script>
        <script src="https://www.chsegura.es/export/system/modules/es.chsegura.www/resources/ddmenu.js"></script>
        <script src="js/jquery.js"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="container">
                    <div id="container2">
                        <asp:Literal ID="HTML_Links_Sup_Izq"     runat="server"></asp:Literal> 
                        <asp:Literal ID="HTML_Subcabecera_Logos" runat="server"></asp:Literal>
                        <asp:Literal ID="HTML_Menu_Navegacion"   runat="server"></asp:Literal>
                        <div id="content-wrapper">                    
                            <div id="left-column">
                                <ul class="nav-menu">
                                    <li></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
        </form>
    </body>
</html>
