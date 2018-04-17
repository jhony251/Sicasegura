<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="librodigital_home_index" %>

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
        <script src="../../SICAH/HighCharts/jquery.min131.js" type="text/javascript"></script> 
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
        <style>
            iframe{
            	height:320px;
            	padding-right:50px;
            	}

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
                            <a href="index.aspx">Inicio</a> > <a href="index.aspx">Consumos</a>
                        </p>
                    
                        <div id="left-column">
                            <ul class="nav-menu">
                                <li>Volúmenes consumidos</li>
                                <asp:Literal ID="HTML_Link_descarga_derechos" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Lecturas" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Gis" runat="server"></asp:Literal>
                            </ul>    
                        </div>
                    
                        <div id="central-content">
                            <h2><asp:Literal ID="HTML_Título_Aprovechamiento" runat="server"></asp:Literal></h2>
                            <p>Información resumida de todos los elementos de su aprovechamiento</p>
                            <div>
                            <table style="background-color:#eaecef;">
                                
                                <tr style="background-color:#eaecef;">
                                    <td style="border:0px; background-color:#eaecef;" valign="middle" width="150px"> Selecione el Año Hidrológico</td>
                                    <td style="border:0px; background-color:#eaecef;"> 
                                       
                                        <asp:DropDownList ID="DDL_AnyoHidrologico" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Button ID="Button1" runat="server"  Text="Actualizar"  onclick="Button1_Click" Visible="false" />
                                        
                                    </td>
                                </tr>
                            </table>
                            <table>
        
                            <tr>
                                <td  style="border:0px; background-color:#eaecef;">
                                    <asp:Label ID="LBL_Concesion" runat="server" Text="Concesión (m3)"></asp:Label>
                                    <asp:TextBox ID="TXT_Concesion" Enabled="false" runat="server" Width="75px" style="text-align:right;"></asp:TextBox>
                                </td>
                                <td  style="border:0px; background-color:#eaecef;">
                                    <asp:Label ID="LBL_Concesion_modificacion" runat="server" Text="Modificación Concesión (m3)"></asp:Label>
                                    *<asp:TextBox ID="TXT_Concesion_temporal" Enabled="false" runat="server" Width="75px" style="text-align:right;"></asp:TextBox>
                                </td>
                                <td  style="border:0px; background-color:#eaecef;">
                                    <asp:Label ID="LBL_Concesion_total" runat="server" Text="Concesión Vigente (m3)"></asp:Label>
                                    <asp:TextBox ID="TXT_Concesion_total" Enabled="false" runat="server" Width="75px" style="text-align:right;"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td  style="border:0px; background-color:#eaecef;" colspan="2">
                                    <asp:Label ID="LBL_Consumido_vol" runat="server" Text="Suministrado (m3)"></asp:Label>
                                    <asp:TextBox ID="TXT_Consumido_vol" runat="server" Enabled="false" Width="75px" style="text-align:right;"></asp:TextBox>
                                    Última fecha Computada: <asp:Literal ID="HTML_Fechaultimalecturasuministrado" runat="server"></asp:Literal>
                                </td>
                                
                                <td  style="border:0px; background-color:#eaecef;">
                                    <asp:Label ID="LBL_Consumido_percent" runat="server" Text="Suministrado (%)"></asp:Label>
                                    <asp:TextBox ID="TXT_Consumido_percent" runat="server" Enabled="false" Width="75px" style="text-align:right;"></asp:TextBox>
                                    <asp:TextBox ID="TXT_Inscripcion" runat="server" Enabled="false" Width="75px" Visible="false" style="text-align:right;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"  style="border:0px;" ><asp:Literal ID="HTML_Iframe_Grafico_Consumo" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                            <table>
                                <tr>
                                    <td width="50%">Toda la información aquí mostrada, está calculada en base a datos previamente contrastados y validados por 
                                        parte del organismo.
                                    </td>
                                    <td  width="50%"><font size="6px">* Modificación de la concesión acorde a:
                                            <br /> (1) contrato de cesión autorizado
                                            <br /> (2) Autorización por cargo a otros años hidrológicos
                                            <br /> (3) Autorización por junta de gobierno.</font>
                                    </td>
                                     
                                </tr>
                            </table> 
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
