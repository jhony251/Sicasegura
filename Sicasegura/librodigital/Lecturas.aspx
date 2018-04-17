<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lecturas.aspx.vb" Inherits="Lecturas" %>
<%@ Register TagPrefix="uc" TagName="paginador" Src="~/Controls/paginador.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="EstilosIframe.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!-- Caudalímetro -->
        <asp:Panel runat="server" ID="pnlLecturasQ" Visible="false">
            <table cellpadding="0" cellspacing="0" style="border: 1px solid #105A7B" width="100%">                                
                <asp:Repeater runat="server" ID="rptLecturasQ">
                    <HeaderTemplate>
                        <tr>
                            <th class="gridCabecera">Fecha Medida</th>
                            <th class="gridCabecera">Tipo Obtención Caudal</th>
                            <th class="gridCabecera">Código Fuente Dato</th>
                            <th class="gridCabecera">Escala (m)</th>
                            <th class="gridCabecera">Calado (m)</th>
                            <th class="gridCabecera">Regimen Curva</th>
                            <th class="gridCabecera">Nº Parada</th>
                            <th class="gridCabecera">Caudal (m3/s)</th>
                            <th class="gridCabecera">Duda Calidad</th>
                            <th class="gridCabecera">Observaciones</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("TipoObtencionCaudal")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Cod_Fuente_Dato")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Escala_M")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Calado_M")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("RegimenCurva")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("NumeroParada")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Caudal_M3S")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Duda_Calidad")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Observaciones")%></td>
                        </tr>
                    </ItemTemplate>                        
                </asp:Repeater>    
            </table>                                    
        </asp:Panel>
                                            
         <!-- Volumen -->
        <asp:Panel runat="server" ID="pnlLecturasV" Visible="false">
            <table cellpadding="0" cellspacing="0" style="border: 1px solid #105A7B" width="100%">         
                <asp:Repeater runat="server" ID="rptLecturasV">
                    <HeaderTemplate>
                        <tr>
                            <th class="gridCabecera">Fecha Medida</th>
                            <th class="gridCabecera">Código Fuente Dato</th>
                            <th class="gridCabecera">Lectura contador (m3)</th>
                            <th class="gridCabecera">Funciona</th>
                            <th class="gridCabecera">Caudal Medido (l/s)</th>
                            <th class="gridCabecera">Incidencia Volumétrica</th>                            
                            <th class="gridCabecera">Observaciones</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>                            
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%></td>                            
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Cod_Fuente_Dato")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("LecturaContador_M3")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Funciona")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("CaudalMedido")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("idIncidenciaVolumetrica")%></td>                            
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Observaciones")%></td>
                        </tr>
                    </ItemTemplate>                        
                </asp:Repeater>   
            </table>
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
                            <th class="gridCabecera">Funciona</th>
                            <th class="gridCabecera">Incidencia Eléctrica</th>
                            <th class="gridCabecera">Observaciones</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>                             
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%></td>                            
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Cod_Fuente_Dato")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("LecturaI")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("LecturaII")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("LecturaIII")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Total_KWH")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Total_Kvar")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Funciona")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("idIncidenciaElectrica")%></td>                            
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Observaciones")%></td>
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
                            <th class="gridCabecera">Funciona</th>
                            <th class="gridCabecera">Incidencia Volumétrica</th>
                            <th class="gridCabecera">Observaciones</th>                            
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>                                              
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%></td>                            
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Cod_Fuente_Dato")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("HorasIntervalo")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("Funciona")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" &nbsp;" & Container.DataItem("idIncidenciaVolumetrica")%></td>
                            <td class="<%#GetEstilo(container.dataitem)%>"><%#" " & Container.DataItem("Observaciones")%></td>                            
                        </tr>                        
                    </ItemTemplate>                        
                </asp:Repeater>                    
            </table>
        </asp:Panel>
        <table width="100%">
            <!-- Número de páginas -->
            <tr><td class="paginador"><asp:Label runat="server" Visible="false" ID="lblNohay" Text="No hay registros"></asp:Label></td></tr>
            <tr>                   
                <td style="padding-left:20px; padding-right:20px;" class="paginador">               
                    <uc:paginador ID="ucPaginador" runat="server"/>                        
                </td>
            </tr>
            <!-- Fin Número de páginas -->
        </table>
    </div>
    </form>
</body>
</html>
