<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lecturas.aspx.vb" Inherits="Lecturas" %>
<%@ Register TagPrefix="uc" TagName="paginador" Src="~/librodigital/home/Controls/paginador.ascx" %>

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
                            <a href="index.aspx">Inicio</a> > <a href="lecturas.aspx">Lecturas</a>
                        </p>
                        <div id="left-column">
                            <ul class="nav-menu">
                                
                                <asp:Literal ID="HTML_Listado_de_puntos_de_control" runat="server"></asp:Literal>
                                
                            </ul>    
                        </div>
                        <div id="central-content">
                            <h2><asp:Literal ID="HTML_Título_Aprovechamiento" runat="server"></asp:Literal></h2>
                            <p>Información detallada del elemento de medida seleccionado de su aprovechamiento</p>
                            
                            <asp:Panel runat="server" ID="pnlPanelLecturas" Visible="false">
                                <table>
                                    <tr>
                                        <td style="border:0px;" align="right">
                                            <img src="images/file_edit.png" width="25px" />
                                            <asp:LinkButton ID="LBAddlectura" runat="server">Añadir nueva lectura</asp:LinkButton> 
                                            
                                        </td>
                                    </tr>   
                                </table>
                                <!-- Caudalímetro -->
                                <asp:Panel runat="server" ID="pnlLecturasQ" Visible="false">
                                    <table cellpadding="0" cellspacing="0" style="border: 1px solid #105A7B" width="100%">                                
                                        <asp:Repeater runat="server" ID="rptLecturasQ">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th class="gridCabecera">Fecha</th>
                                                    <th class="gridCabecera">TOC</th>
                                                    <th class="gridCabecera">CFD</th>
                                                    <th class="gridCabecera">Escala (m)</th>
                                                    <th class="gridCabecera">Calado (m)</th>
                                                    <th class="gridCabecera">Curva</th>
                                                    <th class="gridCabecera">Parada</th>
                                                    <th class="gridCabecera">Caudal (m3/s)</th>
                                                    <th class="gridCabecera">Duda Calidad</th>
                                                    <th class="gridCabecera">Observaciones</th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="fila_grid0">
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("TipoObtencionCaudal")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Cod_Fuente_Dato")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Escala_M")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Calado_M")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("RegimenCurva")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("NumeroParada")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Caudal_M3S")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Duda_Calidad")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Observaciones")%></td>
                                                </tr>
                                            </ItemTemplate>                        
                                        </asp:Repeater>    
                                    </table>                                    
                                </asp:Panel>                              
                                <!-- Volumen -->
                                <asp:Panel runat="server" ID="pnlLecturasV" Visible="false">
                                    <table cellpadding="0" cellspacing="0" style="border: 1px solid #105A7B" width="100%">         
                                        <asp:Repeater runat="server" ID="rptLecturasV" Visible="false">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th class="gridCabecera">Fecha Medida</th>
                                                    <th class="gridCabecera">Código Fuente Dato</th>
                                                    <th class="gridCabecera">Lectura  (m3)</th>
                                                    
                                                    <th class="gridCabecera">En marcha</th>
                                                    <th class="gridCabecera">Caudal Medido (l/s)</th>
                                                    <th class="gridCabecera">Incidencia Volumétrica</th>                            
                                                    <th class="gridCabecera">Observaciones</th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="fila_grid0">                            
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%></td>                            
                                                    <%-- <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Cod_Fuente_Dato")%></td>--%>
                                                    <%-- <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("LecturaContador_M3")%></td>--%>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & DataBinder.Eval(Container.DataItem, "LecturaContador_M3", "{0:N}")%></td>
                                                    <%-- <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("En_Marcha")%></td>--%>
                                                    <%-- <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("CaudalMedido")%></td>--%>
                                                    <%-- <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("idIncidenciaVolumetrica")%></td> --%>                           
                                                    <%-- <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Observaciones")%></td>--%>   
                                                </tr>
                                            </ItemTemplate>                        
                                        </asp:Repeater>   
                                    </table>
                                    <asp:GridView ID="DGV_Volumen" runat="server" AllowPaging="true">
                                    </asp:GridView>
                                </asp:Panel>            
                                <!-- Energía -->
                                <asp:Panel runat="server" ID="pnlLecturasE" Visible="false">
                                    <table cellpadding="0" cellspacing="0" style="border: 1px solid #105A7B" width="100%">                          
                                        <asp:Repeater runat="server" ID="rptLecturasE">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th class="gridCabecera">Fecha Medida</th>
                                                    <th class="gridCabecera">Código Fuente Dato</th>
                                                    <th class="gridCabecera">LecturaI</th>
                                                    <th class="gridCabecera">LecturaII</th>
                                                    <th class="gridCabecera">LecturaIII</th>
                                                    <th class="gridCabecera">Total (Kwh)</th>
                                                    <th class="gridCabecera">Total (Kvar)</th>
                                                    <th class="gridCabecera">En marcha</th>
                                                    <th class="gridCabecera">Incidencia Eléctrica</th>
                                                    <th class="gridCabecera">Observaciones</th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="fila_grid0">                             
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%></td>                            
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Cod_Fuente_Dato")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("LecturaI")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("LecturaII")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("LecturaIII")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Total_KWH")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Total_Kvar")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Funciona")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("idIncidenciaElectrica")%></td>                            
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Observaciones")%></td>
                                                </tr>
                                            </ItemTemplate>                        
                                        </asp:Repeater>   
                                    </table>     
                                </asp:Panel>
                                <!-- Horómetros -->            
                                <asp:Panel runat="server" ID="pnlLecturasH" Visible="false">
                                    <table cellpadding="0" cellspacing="0" style="border: 1px solid #105A7B" width="100%">                         
                                        <asp:Repeater runat="server" ID="rptLecturasH">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th class="gridCabecera">Fecha Medida</th>
                                                    <th class="gridCabecera">Código Fuente Dato</th>
                                                    <th class="gridCabecera">Lectura Horómetro (h)</th>
                                                    <th class="gridCabecera">En marcha</th>
                                                    <th class="gridCabecera">Incidencia Volumétrica</th>
                                                    <th class="gridCabecera">Observaciones</th>                            
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="fila_grid0">                                              
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%></td>                            
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Cod_Fuente_Dato")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("HorasIntervalo")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Funciona")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("idIncidenciaVolumetrica")%></td>
                                                    <td style="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Observaciones")%></td>                            
                                                </tr>                        
                                            </ItemTemplate>                        
                                        </asp:Repeater>                    
                                    </table>
                                </asp:Panel>
                                
                                <table width="100%">
                                    <!-- Número de páginas -->
                                    <tr><td class="paginador" align="center"><br /><br /><asp:Label CssClass="LBLNohaydatos" runat="server" Visible="false" ID="lblNohay" Text="No se han encontrado registros en la base de datos"></asp:Label></td></tr>
                                    <tr>                   
                                        <td style="padding-left:20px; padding-right:20px;" class="paginador">               
                                            <uc:paginador ID="ucPaginador" runat="server"/>                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        * Tenga en cuenta que las lecturas mostradas en rojo son secturas que han sido añadidas al sistema 
                                                        pero que aun no han pasado filtro de revision y validación<br /><br />
                                                        ** Igualmente tenga en cuenta que las lecturas introducidas desde esta plataforma no formarán
                                                        parte del informe de volúmenes suministrados hasta que sean validados y revisados.
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <!-- Fin Número de páginas -->
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
