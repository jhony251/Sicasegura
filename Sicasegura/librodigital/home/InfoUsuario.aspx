<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoUsuario.aspx.cs" Inherits="librodigital_home_InfoUsuario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <title></title>
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/stylesheet_opt.css" />
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/theme.css" />
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/utiles.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/menu.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/ddmenu.js"></script>
         <script src="../../SICAH/HighCharts/jquery.min131.js" type="text/javascript"></script> 
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
                            <a href="#">Inicio</a> > <a href="infousuario.aspx">Informacion del Usuario</a>
                        </p>
                    
                        <div id="left-column">
                            <ul class="nav-menu">
                                <li>Información de contacto del usuario</li>
                                <li><a href="infousuario.aspx?update=true">Actualizacion de la información</a></li>
                                
                            </ul>
                        </div>
                        <div id="central-content">
                            <h2><asp:Literal ID="HTML_Título_Aprovechamiento" runat="server"></asp:Literal></h2>
                            <h2>Datos de contacto del Usuario</h2>
                            <b>Responsabilidad de cumplimentación:</b></br>
                            La información aquí reflejada es de obligatoria cumplimentación. Debe ser fiel y relativa a la 
                            persona de contacto que hace uso regular de este sistema, con la finalidad de poder localizar
                            de un modo rápido y veraz a la persona responsable de utilizar el Libro de Control. </br></br>
                            Para la información aquí facilitada o para cualquier otra información de 
                            caracter personal, quedan establecidos los derechos sobre la misma por parte del 
                            titular, en base al texto definido por la
                            <b>Ley Orgánica de Protección de Datos de Carácter Personal de España, </b>Consultable a través 
                            de la web del Boletín Oficial de Estado<br />
                            <br />
                            <br />
                            <br /><br />
                            <h2><asp:Label runat="server" ID="LBL_MesageNoDatos" ></asp:Label></h2>
                            <table>
                                <tr>
                                    <td width="33%">Nombre completo</td>
                                    <td width="33%">DNI/NIF/CIF</td>
                                    <td width="33%">Teléfono de contacto</td>
                                </tr>
                                <tr>
                                    <td width="33%">
                                        <asp:TextBox ID="TXT_Name" runat="server" Width="100%" CausesValidation="true"  AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td width="33%">
                                        <asp:TextBox ID="TXT_DNI" runat="server" Width="100%" CausesValidation="true"  AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td width="33%">
                                        <asp:TextBox ID="TXT_TLF" runat="server" Width="100%" MaxLength="9" CausesValidation="true"  AutoPostBack="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" width="100%">Dirección postal</td>
                                </tr>
                                <tr>
                                    <td colspan="3" width="100%">
                                        <asp:TextBox ID="TXT_Addres" runat="server" Height="50px" TextMode="MultiLine" 
                                            Width="100%" CausesValidation="true"  AutoPostBack="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="33%">Correo electrónico</td>
                                </tr>
                                <tr>
                                    <td width="33%">
                                        <asp:TextBox ID="TXT_Email" runat="server" Width="100%" CausesValidation="true" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td colspan="2" align="right">
                                        <asp:Button runat="server" ID="BTN_Validar_cambios" Text="Validar cambios"  onclick="BTN_Validar_cambios_Click"/>
                                    </td>   
                                </tr>                           
                            </table>
                        </div>
                    </div>
                    <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
                </div>      
            </div>
            <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>
        </form>
    </body>
</html>
