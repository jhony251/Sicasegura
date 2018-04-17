<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inserta-lectura-volumen.aspx.vb" Inherits="SICAH_motores" %>

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
                            <a href="index.aspx">Inicio</a> > <a href="lecturas.aspx">Lecturas</a> > <a href="inserta-lectura-volumen.aspx">Inserción lectura de volumen</a>
                        </p>
                        <div id="left-column">
                            <ul class="nav-menu">
                                
                                <asp:Literal ID="HTML_Listado_de_puntos_de_control" runat="server"></asp:Literal>
                                
                            </ul>    
                        </div>
                        <div id="central-content">
                            <asp:Panel ID="pnlNuevoElemento" runat ="server" Visible="true" Width="100%" >
                                <table>
                                     <tr style="visibility:hidden;">
                                         <td>Cauce</td>
                                         <td> 
                                            <asp:RequiredFieldValidator ID="rfvEDCauce" Text="*" ControlToValidate="TXTCodigoCauce" runat="server" ></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TXTCodigoCauce" runat="server"></asp:TextBox>
                                         </td>                                                    
                                         <td>Código PVYCR</td>
                                         <td> 
                                            <asp:RequiredFieldValidator ID="rfvedCodigo" runat="server" ControlToValidate="TXTCodigoPunto" Text="*"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TXTCodigoPunto" runat="server"></asp:TextBox>
                                         </td>                              
                                         <td> Id Elemento Medida</td>
                                         <td>
                                            <asp:RequiredFieldValidator ID="rfvelemento" Text="*" ControlToValidate="TXTIDEM" runat="server" Display="dynamic" ></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TXTIDEM" runat="server"></asp:TextBox>
                                         </td>                             
                                     </tr>
                                     <tr style="visibility:hidden;">
                                         <td style="background-color: #CAD5DD">Código Fuente Dato</td>
                                         <td> 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" ControlToValidate="TXTCFD" runat="server" ></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TXTCFD" runat="server"></asp:TextBox>
                                         </td>                           
                                         <td style="background-color: #CAD5DD">Fecha Medida</td>
                                         <td nowrap>
                                            
                                         </td>                            
                                         <td>Lectura Contador (m3)</td>
                                         <td>
                                            
                                         </td>
                                     </tr>  
                                     
                                     <tr>
                                        <td style="background-color: #CAD5DD">Fecha Medida</td>
                                        <td><asp:RequiredFieldValidator ID="efvedfecha" Text="*" ControlToValidate="TXTFechaMedida" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TXTFechaMedida" runat="server" CssClass="texto" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>Lectura Contador (m3)</td>
                                        <td>
                                            <asp:TextBox ID="txtEdLecturaContador" runat="server" CssClass="textoNumerico"></asp:TextBox>
                                        </td>
                                        <td>En Marcha </td>    
                                        <td>
                                            <asp:DropDownList style="font:9px Verdana; text-align: right;" ID="ddlEDfunciona" runat="server" AutoPostBack="true"></asp:DropDownList>
                                        </td>
                                     </tr>
                                     <%--<tr>
                                        <td colspan="6" align="center">
                                            <b>Por favor adjunte en este campo una imagen del contador en el mismo momento en el que toma la lectura.</b><br />
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </td>
                                     </tr>--%>
                                     
                                                                
                                     <tr style="visibility:hidden;">
                                         <td>En Marcha </td>                                                                               
                                         <td>
                                            
                                         </td>
                                         <td>Caudal Medido (l/s)</td>
                                         <td>
                                            <asp:TextBox ID="txtEDCaudalMedido" runat="server" CssClass="textoNumerico"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEDCaudalMedido" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator>
                                         </td>
                                         <td>Usuario</td>    
                                         <td>
                                            <asp:TextBox ID="txtEDUsuario" runat="server" CssClass="texto"></asp:TextBox>
                                         </td>                                                          
                                     </tr>
                                     <tr style="visibility:hidden;">  
                                         <td> Incidencia Volumétrica</td>
                                         <td>
                                            <asp:DropDownList ID="ddlEDIncidenciasV" runat="server" style="font:9px Verdana; text-align: right" ></asp:DropDownList>
                                         </td>                                                        
                                         <td>Consumo Volumétrico Adicional</td>
                                         <td>
                                            <asp:TextBox ID="txtEDConsumoVol" CssClass="textoNumerico" runat="server" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEDConsumoVol" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator>
                                         </td>
                                         <td>Reinicio Lectura Volumétrica</td>
                                         <td>
                                            <asp:TextBox ID="txtEDReinicioVol" CssClass="textoNumerico" runat="server" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEDReinicioVol" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator>
                                         </td>                           
                                      </tr>
                                      <tr style="visibility:hidden;">
                                          <td>Observaciones</td>
                                          <td colspan="5">
                                            <asp:TextBox ID="txtEDObservaciones" runat="server" CssClass="texto" Width="99%"></asp:TextBox>
                                          </td>
                                      </tr>
                                      <tr>
                                        <td colspan="6" align="right">
                                            <asp:Button ID="btnEDAceptar" runat="server" Text="Aceptar" CssClass="boton" />
                                            <asp:Button ID="btnEDCancelar" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
                                        </td>
                                      </tr>
                                      <tr>
                                        <td colspan="4" align="left" style="border:0px;">
                                        <b>Si ha cometido algun error de formato aparecerá aquí abajo:</b> <br /><br />                           
                                            <asp:RegularExpressionValidator ID="rev2" runat="server" ControlToValidate="TXTFechaMedida"
                                            ErrorMessage="Formato fecha 'dd/mm/yyyy hh:mm:ss'" Font-Size="10pt" ValidationExpression="^((((([0-1]?\d)|(2[0-8]))\/((0?\d)|(1[0-2])))|(29\/((0?[1,3-9])|(1[0-2])))|(30\/((0?[1,3-9])|(1[0-2])))|(31\/((0?[13578])|(1[0-2]))))\/((19\d{2})|([2-9]\d{3}))|(29\/0?2\/(((([2468][048])|([3579][26]))00)|(((19)|([2-9]\d))(([2468]0)|([02468][48])|([13579][26]))))))\s(([01]?\d)|(2[0-3]))(:[0-5]?\d){2}$"
                                            Display="Dynamic"></asp:RegularExpressionValidator>   
                                            <br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEdLecturaContador" ErrorMessage="El valor del campo lectura debe ser un numérico y/o decimal" ValidationExpression="[\-\+]{0,1}\d+([\.]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator>                          
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtEDCaudalMedido"  ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator>   
                                        </td>
                                      </tr>
                                      <tr>
                                        <td colspan="6">
                                            <h4>Es importante que recuerde que la información añadida debe de ser REAL y VERÁZ, tal y como requiere la Orden ARM/1312/2009, de 20 de mayo, en el ámbito territorial de la Demarcación Hidrográfica del Segura.</h4><br /> <br />
                                            <b>De este modo para la correcta cumplimentación de los datos siga estas simples indicaciones:</b><br /><br />
                                            (1) <b>El dato fecha</b> insertado debe corresponder al <b>momento en el que se toma la lectura</b>, no al momento en el que se está insertando<br />
                                            (2) El formato del <b>dato fecha</b> debe ser insertado indicando <b>la hora exacta</b> en la que el contador marcaba la lectura indicada<br />
                                            (3) La hora insertada en el campo fecha debe ser exacta, <b>pudiendo poner a "0" los segundos</b> si los desconociera <br />
                                            (4) <b>La lectura</b> tomada <b>debe ser un dato absoluto</b>, por y no relativo a lecturas anteriores<br />
                                            (5) La lectura tomada debe ser un <b>dato computado</b>, de tal modo que si su contador incluye algún tipo de multiplicador deberá de estar contemplado.<br />
                                            (6) La lectura tomada, por muy grande que sea <b>debe ser insertada en m3</b>
                                            
                                        </td>
                                      </tr>
                                  </table>
                            </asp:Panel>
                        </div>
                    </div>
                    <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
                </div>      
            </div>
            <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>
        </form>
    </body>
</html>

