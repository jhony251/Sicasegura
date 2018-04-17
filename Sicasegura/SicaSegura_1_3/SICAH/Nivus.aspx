<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Nivus.aspx.vb" Inherits="SICAH_Nivus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>SICA - Sistema Integral de Control de Aprovechamientos</title>
  <link rel="stylesheet" href="..\styles.css" />

  <script type="text/javascript" language="javascript" src="..\js/utilesListados.js"></script>
  <script type="Text/javascript" language="javascript" src="..\js/utilesMenus.js"></script>     
  
  <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
  <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
  <meta name="vs_defaultClientScript" content="JavaScript" />
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
</head>
<body bgcolor="#EEEAD2">
  <form id="Form1" method="post" runat="server">     
    <table cellspacing="2" align="center" width="100%" style="border: 1px solid #666666; background-color: white">
      <tr>
        <td>
          <table cellspacing="0" cellpadding="1" width="100%">
            <asp:Label ID="lblCabecera" runat="server"></asp:Label>
            <asp:Label ID="lblPestanyas" runat="server"></asp:Label>
            <tr>
              <td>                
                <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <asp:Label ID="lbl1" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblPuntoSelecc" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblFechaSelecc" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblPVYCRSelecc" runat="server" Visible="false"></asp:Label>
                  <tr>
                    <td>
                      <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td class="titulo">
                            &nbsp;GESTIÓN DE ACEQUIAS QUE DIFIEREN UN 10% DE LOS REGISTROS ANTERIORES</td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFF0E2" class="celdacontenido" colspan="2">
                      <asp:Panel ID="pnlPuntos" runat="server">
                        <table cellspacing="0" cellpadding="2" width="100%">
                          <tr class="titListado">
                            <td colspan="2" class="titListado">
                              &nbsp;SELECCIONE UN PUNTO</td>                              
                          </tr>
                            <tr class="Ordenar">                                
                                <td class="Ordenar">CODIGO PVYCR</td>                                
                            </tr>
                          
                          <tr>
                            <td colspan="3">
                              <asp:Repeater ID="rptPuntos" runat="server">
                                <ItemTemplate>
                                  <tr <%# checkfila()%>>
                             	    <td >&nbsp;<asp:LinkButton ID="lbtPunto" Runat="server" OnClick="PuntoSeleccionado" CommandArgument='<%#Container.dataitem("codigoPVYCR")%>' style="text-decoration:none; color:Maroon"><%#Container.DataItem("codigoPVYCR")%></asp:LinkButton></td>		                            
                                  </tr>
                                </ItemTemplate>
                              </asp:Repeater>
                            </td>
                          </tr>                          
                        </table>
                      </asp:Panel>
                      <asp:Panel ID="pnlPuntosFechas" runat="server" Visible="false">
                        <table cellspacing="0" cellpadding="2" width="100%">
                          <tr class="titListado">
                            <td colspan="6" style="background-color: #8CA4B5; padding: 2px; padding-right: 10px; color: white;
                              border-bottom: 1px solid #eeeeee; font-weight: bold">
                              <asp:Label ID="lblNombrePunto" runat="server" Width="100%">SELECCIONE UNA FECHA</asp:Label>
                            </td>                                  
                          </tr>
                            <tr class="Ordenar">                                
                                <td class="Ordenar">CODIGO PVYCR</td>                                
                                <td class="Ordenar">Fecha</td>                                
                            </tr>
                          
                          <tr>
                            <td colspan="3">
                              <asp:Repeater ID="rptPuntosFechas" runat="server">
                                <ItemTemplate>
                                  <tr <%# checkfila()%>>
                             	    <td >&nbsp;<asp:LinkButton ID="lbtPuntoFecha1" Runat="server" OnClick="PuntoFechaSeleccionado" CommandArgument='<%#Container.dataitem("fecha")%>' style="text-decoration:none; color:Maroon"><%#Container.DataItem("codigoPVYCR")%></asp:LinkButton></td>		                            
                             	    <td >&nbsp;<asp:LinkButton ID="lbtPuntoFecha2" Runat="server" OnClick="PuntoFechaSeleccionado" CommandArgument='<%#Container.dataitem("fecha")%>' style="text-decoration:none; color:Maroon"><%#DataBinder.Eval(Container.DataItem, "fecha", "{0:dd/MM/yyyy}")%></asp:LinkButton></td>		                            
                                  </tr>
                                </ItemTemplate>
                              </asp:Repeater>
                            </td>
                          </tr>                          
                        </table>
                      </asp:Panel>
                      <asp:Panel ID="pnlPuntosFechasHoras" runat="server" Visible="false">
                        <table cellspacing="0" cellpadding="2" width="100%">
                          <tr class="titListado">                            
                                <td colspan="6" style="background-color: #8CA4B5; padding: 2px; padding-right: 10px; color: white;
                              border-bottom: 1px solid #eeeeee; font-weight: bold">
                              <asp:Label ID="lblNombreFecha" runat="server" Width="100%">SELECCIONE UNA HORA</asp:Label>
                            </td>                             
                          </tr>
                            <tr class="Ordenar">                                
                                <td class="Ordenar">CODIGO PVYCR</td>
                                <td class="Ordenar">FECHA</td>
                                <td class="Ordenar">HORA</td>
                                <td class="Ordenar">CALADO</td>
                                <td class="Ordenar">CAUDAL</td>
                                <td class="Ordenar">VELOCIDAD</td>
                            </tr>
                          
                          <tr>
                            <td colspan="3">
                              <asp:Repeater ID="rptNivus" runat="server">
                                <ItemTemplate>
                                  <tr <%# checkfila()%>>
                             	    <td >&nbsp;<asp:LinkButton ID="lbtPVYCR" Runat="server" OnClick="PVYCRSeleccionada" CommandArgument='<%#Container.dataitem("codigoPVYCR") & "#" & container.dataitem("fecha") & "#" & container.dataitem("hora")%>' style="text-decoration:none; color:Maroon"><%#Container.DataItem("codigoPVYCR")%></asp:LinkButton></td>
		                            <td ><asp:LinkButton ID="lbtFecha" Runat="server" OnClick="PVYCRSeleccionada" CommandArgument='<%#Container.dataitem("codigoPVYCR") & "#" & container.dataitem("fecha") & "#" & container.dataitem("hora")%>' style="text-decoration:none; color:Maroon"><%#Container.DataItem("fecha")%></asp:LinkButton></td>
		                            <td ><asp:LinkButton ID="lbtHora" Runat="server" OnClick="PVYCRSeleccionada" CommandArgument='<%#Container.dataitem("codigoPVYCR") & "#" & container.dataitem("fecha") & "#" & container.dataitem("hora")%>' style="text-decoration:none; color:Maroon"><%#Container.DataItem("hora")%></asp:LinkButton></td>
		                            <td ><asp:LinkButton ID="lbtCalado" Runat="server" OnClick="PVYCRSeleccionada" CommandArgument='<%#Container.dataitem("codigoPVYCR") & "#" & container.dataitem("fecha") & "#" & container.dataitem("hora")%>' style="text-decoration:none; color:Maroon"><%#Container.DataItem("calado_m")%></asp:LinkButton></td>
		                            <td ><asp:LinkButton ID="lbtCaudal" Runat="server" OnClick="PVYCRSeleccionada" CommandArgument='<%#Container.dataitem("codigoPVYCR") & "#" & container.dataitem("fecha") & "#" & container.dataitem("hora")%>' style="text-decoration:none; color:Maroon"><%#Container.DataItem("caudal")%></asp:LinkButton></td>
		                            <td ><asp:LinkButton ID="lbtVelocidad" Runat="server" OnClick="PVYCRSeleccionada" CommandArgument='<%#Container.dataitem("codigoPVYCR") & "#" & container.dataitem("fecha") & "#" & container.dataitem("hora")%>' style="text-decoration:none; color:Maroon"><%#Container.DataItem("velocidad")%></asp:LinkButton></td>
                                  </tr>
                                </ItemTemplate>
                              </asp:Repeater>
                            </td>
                          </tr>                          
                        </table>
                      </asp:Panel>
                      <asp:Panel ID="pnlDetalle" runat="server" Visible="False">
                        <table width="100%" cellpadding="0" cellspacing="0">
                          <tr>
                            <td style="background-color: #8CA4B5; padding: 2px; padding-right: 10px; color: white;
                              border-bottom: 1px solid #eeeeee; font-weight: bold">
                              <asp:Label ID="lblNombrePVYCR" runat="server" Width="100%">NOMBRE PVYCR</asp:Label>
                            </td>
                          </tr>
                        </table>
                        <table width="100%">
                          <tr>
                            <td colspan="7" align="right" style="border-top: 1px solid #666666">
                            </td>
                          </tr>
                        </table>
                        <table width="100%">
                            <asp:Repeater ID="rptNivusDetalle" runat="server">
                          <HeaderTemplate>                            
                              <tr>
                                <th class="preproduccion" align="center">
                                  Fecha</th>
                                <th class="preproduccion" align="center">
                                  Hora</th>
                                <th class="preproduccion" align="center">
                                  Calado</th>
                                <th class="preproduccion" align="center">
                                  Caudal</th>                                   
                                <th class="preproduccion" align="center">
                                  Velocidad</th>                                
                              </tr>
                          </HeaderTemplate>
                          <ItemTemplate>
                            <tr>
                              <td class="L" align="center" style="font: 9px verdana; background-color: <%#CompruebaHora(Container.DataItem) %>">
                                <%#Container.DataItem("fecha")%>
                              </td>
                              <td class="L" align="right" style="font: 9px verdana; background-color: <%#CompruebaHora(Container.DataItem) %>" >
                                <%#Container.DataItem("Hora")%>
                              </td>
                              <td class="L" align="center" style="font: 9px verdana; background-color: <%#CompruebaHora(Container.DataItem) %>">
                                <%#Container.DataItem("Calado_m")%>
                              </td>
                              <td class="L" align="right" style="font: 9px verdana; background-color: <%#CompruebaHora(Container.DataItem) %>">
                                <%#Container.DataItem("Caudal")%>
                              </td>                              
                              <td class="L" align="center" style="font: 9px verdana; background-color: <%#CompruebaHora(Container.DataItem) %>">
                                <%#Container.DataItem("Velocidad")%>
                              </td>
                            </tr>                            
                            <tr>
                              <td colspan="7">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td style="border-top: solid 2px #BABA74; font-size: 2px">
                                      &nbsp;</td>
                                  </tr>
                                </table>
                              </td>
                            </tr>                            
                          </ItemTemplate>
                        </asp:Repeater>
                        </table>
                      </asp:Panel>                      
                      <asp:Panel ID="pnlEtiquetas" runat="server">
                      <table width="100%" cellpadding="0">  
                        <tr>
                            <td align="right" style="border-top: 1px solid #666666">                                
                                <asp:Button ID="btnProduccion" runat="server" Text="Pasar a Producción" CssClass="boton"  Visible=false/>                                  
                                <asp:Button ID="btnEstadillo" runat="server" Text="Borrar" CssClass="boton" Visible=false />
                                <asp:Button ID="btnCancelar" runat="server" Text="Volver" CssClass="boton" CausesValidation="false" Visible="false" />                                                                    
                            </td>
                        </tr>
                        </table>
                      </asp:Panel>                        
                     
                    </td>
                   </tr>                                      
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>
