<%@ Page Language="VB" AutoEventWireup="false" CodeFile="motores.aspx.vb" Inherits="SICAH_motores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  <link rel="stylesheet" href="..\styles.css">

  <script language="javascript" src="..\js/utilesListados.js"></script>
  <script language="javascript" src="..\js/utilesMenus.js"></script>     
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script language="javascript">
var ventanaDoc;
  </script>

  <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
  <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
  <meta name="vs_defaultClientScript" content="JavaScript">
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body bgcolor="#EEEAD2">
  <form id="Form1" method="post" runat="server">
  <span id="dsp4"></span>
   <span id="imagepath" style="display:none">../js/calendar/images</span>  
    <table cellspacing="2" align="center" width="100%" style="border: 1px solid #666666;
      background-color: white">
      <tr>
        <td>
          <table cellspacing="0" cellpadding="1" width="100%">
            <asp:Label ID="lblCabecera" runat="server"></asp:Label>
            <asp:Label ID="lblPestanyas" runat="server"></asp:Label>
            <tr>
              <td>
                <asp:Label ID="lblidMotor" runat="server" Visible="False"></asp:Label>
                <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <asp:Label ID="lbl1" runat="server" Visible=false></asp:Label>
                  <tr>
                    <td>
                      <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td class="titulo">
                            &nbsp;GESTIÓN DE LECTURAS DATOS MOTORES</td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFF0E2" class="celdacontenido" colspan="2">
                      <asp:Panel ID="pnlMotores" runat="server">
                        <table cellspacing="0" cellpadding="2" width="100%">
                          <tr class="titListado">
                            <td colspan="2" class="titListado">
                              &nbsp;SELECCIONE UN ELEMENTO DE MEDIDA</td>
                              <td colspan="1" class="titListado" nowrap align="right"><asp:LinkButton id="lbtNuevoElemento" Runat="server" >Nueva Lectura</asp:LinkButton>	</td>
                          </tr>
                            <tr class=subtitListado>
	                            <td class=subtitListado colspan=3 align=right>Filtro <asp:TextBox ID=txtFiltro Runat=server Columns=40 CssClass=texto></asp:TextBox>
	                                [<asp:LinkButton id=lbtFiltrar Runat=server>Filtrar</asp:LinkButton>]
	                                [<asp:LinkButton id=lbtBorrarFiltro Runat=server>Borrar filtro</asp:LinkButton>]	
	                            </td>
                            </tr>
                            <tr class=Ordenar>
                                <td nowrap>
                                    <asp:LinkButton ID= lbtordenarPVYCR runat=server CssClass=Ordenar>DENOMINACIÓN PUNTO/ELEMENTO MEDIDA</asp:LinkButton>
                                    <asp:Image ID=imgOrdPVYCRD Visible=false runat=server ImageUrl="images/A_DOWN.gif" align="absmiddle"/>
                                    <asp:Image ID=imgOrdPVYCRA Visible=true runat=server ImageUrl="images/A_UP.gif" align="absmiddle"/>
                                </td>
                                <td nowrap>
                                    <asp:LinkButton ID= lbtordenarCauce runat=server CssClass=Ordenar>DESCRIPCIÓN CAUCE</asp:LinkButton>
                                    <asp:Image ID=imgOrdCauceD Visible=false runat=server ImageUrl="images/A_DOWN.gif" align="absmiddle"/>
                                    <asp:Image ID=imgOrdCauceA Visible=false runat=server ImageUrl="images/A_UP.gif" align="absmiddle"/>                    
                                </td>
                                <td nowrap>
                                    <asp:LinkButton ID= lbtordenarNumReg runat=server CssClass=Ordenar>Nº REG.</asp:LinkButton>
                                    <asp:Image ID=imgOrdNumRegD Visible=false runat=server ImageUrl="images/A_DOWN.gif" align="absmiddle"/>
                                    <asp:Image ID=imgOrdNumRegA Visible=false runat=server ImageUrl="images/A_UP.gif" align="absmiddle"/>                                        
                                </td>
                            </tr>
                          
                          <tr>
                            <td colspan="3">
                              <asp:Repeater ID="rptMotores" runat="server">
                                <ItemTemplate>
                                  <tr <%# checkfila()%>>
                             	    <td >&nbsp;<asp:LinkButton ID=lbtPVYCR Runat=server OnClick='PVYCRSeleccionada' CommandArgument='<%#checkCodigoMotor(Container.DataItem)& "#" & checkFuentedato(Container.DataItem)%>' style="text-decoration:none; color:Maroon"><%#checkCodigoMotor(Container.DataItem)%></asp:LinkButton></td>
		                            <td><asp:Label ID=lbcauce Runat=server Text=<%#checkNombreMotor(Container.DataItem)%> style="text-decoration:none; color:Maroon"></asp:Label></td>
		                            <td><asp:Label ID=lbNumRegEstadilloM runat=server Text=<%#checkNumRegEstadilloM(Container.DataItem)%> style="text-decoration:none; color:Maroon" ></asp:Label></td>
                                  </tr>
                                </ItemTemplate>
                              </asp:Repeater>
                            </td>
                          </tr>
                        </table>
                      </asp:Panel>
                      <asp:Panel ID="pnlEstadillo" runat="server" Visible="False">
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
                      </asp:Panel>
                      <table width="100%" cellpadding="0">
                      <asp:Panel ID="pnlEtiquetas" runat="server" Visible="false">
                        <asp:Repeater ID="rptMotoresDetalle" runat="server">
                          <HeaderTemplate>
                            
                              <tr>
                                <th class="preproduccion" align="center">
                                  Fecha Medida</th>
                                <th class="preproduccion" align="center">
                                  Lectura Contador (m3)</th>
                                <th class="preproduccion" align="center">
                                  Funciona</th>
                                <th class="preproduccion" align="center">
                                Caudal Medido (l/s)</th>                                   
                                <th class="preproduccion" align="center">
                                  Incidencia Volumétrica</th>
                                <th class="preproduccion" align="center">
                                  Consumo Volumétrico Adicional</th>
                                <th class="preproduccion" align="center">
                                  Reinicio Lectura Volumétrica</th>                                                            
                              </tr>
                          </HeaderTemplate>
                          <ItemTemplate>
                            <tr>
                              <td class="L" align="center" style="font: 9px verdana; background-color: #DDDDDD">
                                <%#DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%>
                              </td>
                              <td class="L" align="right" style="font: 9px verdana; background-color: #DDDDDD" >
                                <%#DataBinder.Eval(Container.DataItem, "LecturaContador_M3", "{0:#,###}")%>
                              </td>
                              <td class="L" align="center" style="font: 9px verdana; background-color: #DDDDDD">
                                <%#Container.DataItem("Funciona")%>
                              </td>
                              <td class="L" align="right" style="font: 9px verdana; background-color: #DDDDDD">
                              <%#DataBinder.Eval(Container.DataItem, "CaudalMedido", "{0:#,###,###}")%>
                              </td>                              
                              <td class="L" align="center" style="font: 9px verdana; background-color: #DDDDDD">
                                <%#Container.DataItem("IncVol")%>
                              </td>
                              <td class="L" align="right" style="font: 9px verdana; background-color: #DDDDDD">
                                <%#DataBinder.Eval(Container.DataItem, "ConsumoVolumetricoAdicional", "{0:#,###.##}")%>
                              </td>
                              <td class="L" align="right" style="font: 9px verdana; background-color: #DDDDDD">
                                <%#DataBinder.Eval(Container.DataItem, "ReinicioLecturaVolumetrica", "{0:#,###.##}")%>
                              
                              </td>
                            
                            </tr>
                            <tr>
                              <td colspan="7" class="L" style="font: 9px verdana; background-color: #DDDDDD; color: black">
                                <%#Container.DataItem("Observaciones")%>
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
                      </asp:Panel>
                      <!--para poder continuar los datos de ambas selects, abro dod repeatrs distintos, pero no cierro el footerTemplate del primero
   y no pongo cadecera al segundo-->
                      <asp:Panel ID="pnlBotones" runat="server" Visible="False">
                        <tr>
                          <td colspan="7" style="padding-bottom: 2px">
                            <table width="100%">
                              <tr>
                                <td>
                                  &nbsp;</td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="7">
                            <table width="100%" cellpadding="0" cellspacing="0">
                              <tr>
                                <td style="background-color: #8CA4B5; padding: 2px; padding-right: 10px; color: white;
                                  border-bottom: 1px solid #eeeeee; font-weight: bold">
                                  SUBZONAS DE LOS REGISTROS DE CAMPO</td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="7">
                            <table width="100%" cellpadding="0px" cellspacing="0">
                              <tr>
                                <td align="right" style="border-top: 1px solid #666666">
                                  <asp:Button ID="btnAceptarLectura" runat="server" Text='Aceptar' CssClass="boton">
                                  </asp:Button>
                                  <asp:Button ID="btnCancelarLectura" runat="server" Text='Cancelar' CssClass="boton"
                                    CausesValidation="False"></asp:Button>
                                </td>
                                <td><asp:Button ID="btnListado" runat="server" Text='listado' CssClass="boton" Visible="false" /></td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                      </asp:Panel>
                      <asp:Panel ID="pnlPrincipal" runat="server" Visible="false">
                        <asp:Repeater ID="rptMotoresDetalle2" runat="server">
                          <HeaderTemplate>
                          <!--Inicio tabla detalle-->
                      
                        <table width="100%" cellspacing="5">
                         </HeaderTemplate>
                          <ItemTemplate>
                       
                            <tr>
                              <td style="font-weight: bold;" align="center">
                                Fecha Medida</td>
                              <td  style="font-weight: bold;" align="center">
                                Lectura Contador (m3)</td>                        
                              <td style="font-weight: bold;" align="center">
                                Funciona</td>
                              <td style="font-weight: bold" align="center">
                               Caudal Medido (l/s)</td>   
                              <td style="font-weight: bold" align="center">
                                Incidencia Volumétrica</td>
                              <td style="font-weight: bold" align="center">
                                Consumo Volumétrico Adicional</td>                              
                              <td style="font-weight: bold" align="center">
                                Reinicio Lectura Volumétrica</td>  
                                                                                                                        
                            </tr>
                            <tr >
                              <td align="center" <%# checkColumna_diferencial(Container.DataItem)%>>
                                <%#DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%>
                              </td>                              
                              <td  align="center">
                                <asp:TextBox ID="txtLeccontador" Style="font: 9px verdana; text-align: right;"
                                  runat="server" Text='<%# databinder.eval(container.dataitem,"LecturaContador_M3","{0:#,###.##}")%>'></asp:TextBox>
                              </td>
                                                                                
                              <td  align="center" >
                                <asp:DropDownList Style="font: 9px verdana; text-align: right; width:50px" ID="ddlfunciona" runat="server"
                                  AutoPostBack="true" >
                                  <asp:ListItem Value="0" Text="SI"></asp:ListItem>
                                  <asp:ListItem Value="1" Text="NO"></asp:ListItem>
                                  <asp:ListItem Value="2" Text=""></asp:ListItem>
                                </asp:DropDownList>
                              </td>
                                <td  align="center">
                                <asp:TextBox ID="txtCaudalMedido" Style="font: 9px verdana; text-align: right;"
                                  runat="server" Text='<%# databinder.eval(container.dataitem,"CaudalMedido","{0:#,###,###}")%>'></asp:TextBox>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtCaudalMedido"
                                ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.]\d+){0,1}" Display="Dynamic">
                                </asp:RegularExpressionValidator>   
                               
                              </td>  
                               <td  align="center" width="150px" >
                                <asp:DropDownList ID="ddlIncidenciasV" runat="server" Style="font: 9px verdana; text-align: right">
                                </asp:DropDownList>
                              </td>
                              <td  align="center">
                                <asp:TextBox ID="txtConsumoVol" Style="font: 9px verdana; text-align: right; width:100%"
                                  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ConsumoVolumetricoAdicional", "{0:#,###.##}")%>'></asp:TextBox>
                                
                              </td>                                                            
                              <td  align="center">
                                <asp:TextBox ID="txtReiniciVol" Style="font: 9px verdana; text-align: right; width:100%"
                                  runat="server" Text='<%# databinder.eval(container.dataitem,"ReinicioLecturaVolumetrica","{0:#,##0.##}")%>'></asp:TextBox>
                              </td>                                
                            </tr>
                            <tr>
                              <td colspan="4" style="font-weight: bold">
                                Observaciones
                              </td>
                              <td style="font-weight: bold; text-align:center">
                                Usuario </td> 
                                                 
                            </tr>
                            <tr>
                              <td colspan="4" class="L" style="color: black" align="center">
                                <asp:TextBox ID="txtObservaciones" Style="font: 9px verdana; width:100%" runat="server" Text='<%#Container.DataItem("Observaciones")%>'
                                  Columns="50" TextMode="SingleLine"></asp:TextBox>
                              </td>
                              <td class="L" style="color: black;" align="center">
                                <asp:TextBox ID="txtLogin" Style="font: 9px verdana; width:100%" runat="server" Text='<%#Container.DataItem("login")%>'
                                  TextMode="SingleLine" Enabled="false"></asp:TextBox>
                              </td> 
                                <td align="right">
                                <asp:CheckBox ID="cbAceptarM" runat="server" EnableViewState="true" Text="Aceptar" TextAlign="left" />                                                            
                              </td>   
                              <td align="right">
                               <asp:CheckBox ID="cbRechazarM" runat="server" EnableViewState="true" Text="Rechazar" TextAlign="Left" />
                                <asp:TextBox ID="txtCodigoPVYCR" runat="server" Text='<%# Container.dataitem("CodigoPVYCR") %>'
                                  Style="display: none"></asp:TextBox>
                                <asp:TextBox ID="txtCod_Fuente_Dato" runat="server" Text='<%# Container.dataitem("Cod_Fuente_Dato") %>'
                                  Style="display: none"></asp:TextBox>
                                <asp:TextBox ID="txtFecha_Medida" runat="server" Text='<%# Databinder.eval(Container.dataitem,"Fecha_medida","{0:dd/MM/yyyy HH:mm:ss}") %>'
                                  Style="display: none"></asp:TextBox>
                                <asp:TextBox ID="txtidElementoMedida" runat="server" Text='<%# Container.dataitem("idElementoMedida") %>'
                                  Style="display: none"></asp:TextBox>    
                              </td>                                                                      
                            </tr>
                            <tr>
                              <td colspan="15">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td style='border: solid 0px red;'>
                                      &nbsp;</td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                         
                          </ItemTemplate>
                          <FooterTemplate>
                          
                            
                           </FooterTemplate>
                        </asp:Repeater>
                      </asp:Panel>
                      <asp:Panel ID="pnlNuevoElemento" runat ="server" Visible="false" Width="100%" >
                      <table width="100%" >
                             <tr>
                             <td style="background-color: #CAD5DD">Cauce</td>
                             <td> <asp:DropDownList ID="ddlEDCauce" runat="server" style="font:9px Verdana; text-align: right;" AutoPostBack="true"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvEDCauce" Text="*" ControlToValidate="ddlEDCauce" runat="server" ></asp:RequiredFieldValidator></td>                                                    
                             <td style="background-color: #CAD5DD">Código PVYCR</td>
                             <td> <asp:DropDownList ID="ddlEDCodigoPVYCR" runat="server" style="font:9px Verdana; text-align: right;" AutoPostBack="true"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvedCodigo" runat="server" ControlToValidate="ddlEDCodigoPVYCR" Text="*"></asp:RequiredFieldValidator></td>                              
                             <td style="background-color: #CAD5DD"> Id Elemento Medida</td>
                             <td><asp:DropDownList ID="ddlEDidElemento" runat="server" style="font:9px Verdana; text-align: right;"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvelemento" Text="*" ControlToValidate="ddlEDidElemento" runat="server" Display="dynamic" ></asp:RequiredFieldValidator></td>                             
                             </tr>
                             <tr>
                             <td style="background-color: #CAD5DD">Código Fuente Dato</td>
                             <td> <asp:DropDownList ID="ddlEDCodFuenteDato" runat="server" style="font:9px Verdana; text-align: right;" AutoPostBack="true"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" ControlToValidate="ddlEDCodFuenteDato" runat="server" ></asp:RequiredFieldValidator></td>                           
                             <td style="background-color: #CAD5DD">Fecha Medida</td>
                             <td nowrap><asp:TextBox ID="txtEDFechaMedida" runat="server" CssClass="texto" Width="80px"></asp:TextBox>
                             <asp:Image ID="imgCalendario" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />
                             <asp:RequiredFieldValidator ID="efvedfecha" Text="*" ControlToValidate="txtEDFechaMedida" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>
                             </td>                            
                             <td>Lectura Contador (m3)</td>
                             <td><asp:TextBox ID="txtEdLecturaContador" runat="server" CssClass="textoNumerico"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEdLecturaContador" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                              </tr>                             
                             <tr>
                             <td>Funciona </td>                                                                               
                             <td><asp:DropDownList style="font:9px Verdana; text-align: right;" ID="ddlEDfunciona" runat="server" AutoPostBack="true"></asp:DropDownList>
                             </td>
                             <td>Caudal Medido (l/s)</td>
                             <td><asp:TextBox ID="txtEDCaudalMedido" runat="server" CssClass="textoNumerico"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEDCaudalMedido" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                              <td>Usuario</td>    
                              <td><asp:TextBox ID="txtEDUsuario" runat="server" CssClass="texto"></asp:TextBox></td>                                                          
                             </tr>
                             <tr>  
                              <td> Incidencia Volumétrica</td>
                              <td><asp:DropDownList ID="ddlEDIncidenciasV" runat="server" style="font:9px Verdana; text-align: right" ></asp:DropDownList></td>                                                        
                              <td>Consumo Volumétrico Adicional</td>
                              <td><asp:TextBox ID="txtEDConsumoVol" CssClass="textoNumerico" runat="server" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEDConsumoVol" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                              <td>Reinicio Lectura Volumétrica</td>
                              <td><asp:TextBox ID="txtEDReinicioVol" CssClass="textoNumerico" runat="server" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEDReinicioVol" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>                           
                              </tr>
                              <tr>
                              <td>Observaciones</td>
                              <td colspan="5"><asp:TextBox ID="txtEDObservaciones" runat="server" CssClass="texto" Width="99%"></asp:TextBox></td>
                              </tr>
                              <tr>
                              <td colspan="6" align="right">
                              <asp:Button ID="btnEDAceptar" runat="server" Text="Aceptar" CssClass="boton" />
                              <asp:Button ID="btnEDCancelar" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
                              </td>
                              </tr>
                              <tr>
                              <td colspan="4" align="left">                            
                              <asp:RegularExpressionValidator ID="rev2" runat="server" ControlToValidate="txtEDfechamedida"
                                ErrorMessage="Formato fecha 'dd/mm/yyyy hh:mm'" Font-Size="10pt" ValidationExpression="\d\d/\d\d/\d\d(\d\d){0,1}( \d\d:\d\d){0,1}"
                                Display="Dynamic"></asp:RegularExpressionValidator>                             <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtEDCaudalMedido"
                                ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.]\d+){0,1}" Display="Dynamic">
                                </asp:RegularExpressionValidator>   
                              </td>
                              </tr>
                              </table>
                      </asp:Panel>
                     </table>
                    </td>
                    </tr>
                    
                    <tr>
                      <td>
                        <asp:Panel ID="Panel1" runat="server" Width="100%" CssClass="filaPaginacion" Visible="false" >
                          <table cellspacing="1" cellpadding="0" width="100%">
                            <tr>
                              <td width="1%" nowrap>
                                <asp:LinkButton ID="lbtFirst" runat="server" CssClass="linkpag" CommandName="firstPage"><img src="../images/first.gif" border=0 align=absmiddle></asp:LinkButton>
                                <asp:LinkButton ID="lbtPrev" runat="server" CssClass="linkpag" CommandName="pagPrev"><img src="../images/atras.gif" border=0 align=absmiddle></asp:LinkButton></td>
                              <td align="center">
                                Página
                                <asp:DropDownList ID="ddlPaginacion" runat="server" AutoPostBack="True" Font-Size="7">
                                </asp:DropDownList>
                                de
                                <asp:Label ID="lblNumpaginas" Text="<%# numpaginas%>" runat="server"></asp:Label>
                              </td>
                              <td width="1%" nowrap>
                                <asp:LinkButton ID="lbtNext" runat="server" CssClass="linkpag" CommandName="pagSig"><img src="../images/alante.gif" border=0 align=absmiddle></asp:LinkButton>
                                <asp:LinkButton ID="lbtLast" runat="server" CssClass="linkpag" CommandName="lastPage"><img src="../images/last.gif" border=0 align=absmiddle></asp:LinkButton>
                              </td>
                            </tr>
                          </table>
                          <asp:Label ID="lblPagina" runat="server" Visible="false"></asp:Label>
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
