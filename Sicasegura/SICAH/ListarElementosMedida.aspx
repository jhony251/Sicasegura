<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListarElementosMedida.aspx.vb" Inherits="SICAH_ListarElementosMedida" %>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Guardería Fluvial</title>
     <link rel="stylesheet" href="..\styles.css">
     <script language="javascript" src="..\js/utilesMenus.js"></script>  
</head>
<body bgcolor="#EEEAD2">
    <form id="form1" runat="server" method=post>
      <table width="100%" cellspacing="2" align="center" style="border: 1px solid #666666;background-color: white">
       <tr>
        <td>
          <table cellspacing="0" cellpadding="1" width="100%">
           <asp:Label ID="lblCabecera" runat="server"></asp:Label>
           <asp:Label ID="lblPestanyas" runat="server"></asp:Label>
            <tr>
              <td>
                <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <!-- Celda Menú - Contenido -->
                <tr>
                   <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px;" valign=top>
                        <!-- Panel listar Energia -->                      
                        <asp:Panel ID=pnlEnergia Runat=server Visible=false>
                            <table width=100%>
                            <tr>
                                <td align="right"><a>Filtrar</a></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666">
                                <table align="right">
                                <tr>
                                    <td>
                                    <asp:TextBox ID="txtFiltroCodigoPVYCRE" runat="server" Text="[Código PVYCR]" CssClass="texto"></asp:TextBox>
                                    </td>
                                    <td>
                                    <asp:Button ID="btnFiltroAceptarE" runat="server" text="Filtrar" CssClass="boton" />
                                    <asp:Button ID="btnFiltroCancelarE" runat="server" Text="Limpiar" CssClass="boton" />
                                    </td>
                                
                                </tr>
                                <tr>
                                    <td colspan="3" style="color:#555555"><i>Utilice el caracter "%" como comodín</i></td>
                                </tr>
                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width=100%>                                               
                                <asp:Repeater ID=rptEnergia runat=server>  
                                    <HeaderTemplate>
                                        <tr><td class="tituloSec" colspan=2>DATOS ALIMENTACIÓN</td></tr>
                                        <tr>
                                        <td class="encabListado">codigo PVYCR</td>
                                        <td class="encabListado">Id Intervalo</td>
                                        <td class="encabListado">Fecha Inicial</td>
                                        <td class="encabListado">Fecha Fin</td>
                                        <td class="encabListado">Ref. Contrato</td>
                                        <td class="encabListado">Pot. Contratada</td>
                                        <td class="encabListado">Tarifa Contratada</td>
                                        <td class="encabListado">&nbsp;</td>
                                        </tr>
                                    </HeaderTemplate>                                                      
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><%#Container.DataItem("codigoPVYCR")%></td>
                                            <td><%#Container.DataItem("idIntervalo")%></td>
                                            <td><%#Container.DataItem("fechaInicial")%></td>
                                            <td><%#Container.DataItem("FechaFin")%></td>
                                            <td><%#Container.DataItem("E_Ref_Contrato")%></td>
                                            <td><%#Container.DataItem("E_pot_Contratada")%></td>
                                            <td><%#Container.DataItem("E_Tarifa_contratada")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=6>               
                                  <uc:paginacion ID="ucPaginacionE" runat="server" />        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Energia' -->
                        <!--Panel listar Acequias -->
                        <asp:Panel ID=pnlAcequias Runat=server Visible=false>
                            <table width=100%>
                            <tr>
                                <td align="right"><a>Filtrar Por Fecha Medida</a></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666">
                                <table align="right">
                                <tr>
                                    <td>
                                    <asp:TextBox ID="txtfiltroFechaA" runat="server" CssClass="texto"></asp:TextBox>
                                    <asp:CompareValidator ID=cvfechaA runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaA Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                    </td>
                                    <td>
                                    <asp:Button ID="btnFiltroAceptarA" runat="server" text="Filtrar" CssClass="boton" />
                                    <asp:Button ID="btnFiltroCancelarA" runat="server" Text="Limpiar" CssClass="boton" />
                                    <asp:Button ID="btnVoverArbol" runat="server" Text="Cancelar" CssClass="boton" />
                                    </td>
                                
                                </tr>
                                <tr>
                                    <td colspan="4" style="color:#555555">&nbsp;</td>
                                </tr>
                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width=100%>                                               
                                <asp:Repeater ID=rptAcequias runat=server>  
                                    <HeaderTemplate>
                                        <tr>
                                           <td class="tituloSec" colspan=2>DATOS ACEQUIAS: <%#obtenerNomElemento()%></td>
                                        </tr>
                                        <tr>                                        
                                        <td class="encabListado">codigo PVYCR</td>
                                        <td class="encabListado">Fecha Medida</td>
                                        <td class="encabListado">T. Obt. Caudal</td>
                                        <td class="encabListado">Escala</td>
                                        <td class="encabListado">Calado</td>
                                        <td class="encabListado">Régimen Curva</td>
                                        <td class="encabListado">Nº. Parada</td>
                                        <td class="encabListado">Caudal (m3/s)</td>
                                        <td class="encabListado">Duda Calidad</td>
                                        <td class="encabListado">Observaciones</td>
                                        </tr>
                                    </HeaderTemplate>                                                      
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><%#Container.DataItem("codigoPVYCR")%></td>
                                            <td><%#Container.DataItem("Fecha_Medida")%></td>
                                            <td><%#Container.DataItem("TipoObtencionCaudal")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "Escala_M", "{0:#,##0.##}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "Calado_M","{0:#,##0.##}")%></td>
                                            <td><%#Container.DataItem("RegimenCurva")%></td>
                                            <td><%#Container.DataItem("NumeroParada")%></td>
                                            <td><%#Container.DataItem("Caudal_M3S")%></td>
                                            <td><%#Container.DataItem("Duda_Calidad")%></td>
                                            <td><%#Container.DataItem("Observaciones")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=10>               
                                  <uc:paginacion ID="ucPaginacionA" runat="server" />        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Energia' -->                        
                        
                    </td>
                    <!-- Fin Celda Contenido -->        
                </tr>
                <!-- Fin Celda Menú - Contenido -->
                   
        
</table></td></tr>
          </table> 
        
</td></tr></table>
    </form>
</body>
</html>
