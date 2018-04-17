<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inserta-lectura-caudal.aspx.vb" Inherits="SICAH_acequias" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  <link rel="stylesheet" href="..\styles.css">

  <script language="javascript" src="..\js/utilesListados.js" type="text/javascript"></script>
  <script language="javascript" src="..\js/utilesMenus.js" type="text/javascript"></script>  

  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" type="text/javascript">
var ventanaDoc; 
  </script>
   <script language="javascript" type="text/javascript">
    function mensaje(){

            alert("Actualmente NO se pueden Aceptar/Rechazar datos");
            return false;
           }
    </script>    
   <script language="javascript" type="text/javascript">
    function AbrirGrafico(){
        window.open("perfil_acequiaFlash.aspx","grafico", "toolbar=no,menubar=no,top=200,left=250,height=500,width=650");
           }
//   function AbrirCurvas(){
//        //window.open("CurvasAcequiasPreproduccion.aspx?caudales=" + caudales + "&codigoPVYCR=" + codigoPVYCR + "&idElementoMedida=" + idElementomedida ,"grafico", "toolbar=no,menubar=no,top=200,left=250,height=500,width=650");
//        window.open("CurvasAcequiasPreproduccion.aspx?" ,"grafico", "toolbar=no,menubar=no,top=200,left=250,height=500,width=650");
//           }           
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
              <asp:Label ID="lblidelemen" runat="server" Visible="False"></asp:Label>
              <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <asp:Label ID="lbl1" runat="server"></asp:Label>
                <tr>
                  <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                      <tr>
                        <td class="titulo">
                          &nbsp;GESTIÓN DE LECTURAS DATOS ACEQUIAS</td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td bgcolor="#EFF0E2" class="celdacontenido">
                    <asp:Panel ID="pnlAcequias" runat="server">
                      <table cellspacing="0" cellpadding="2" width="100%">
                        <tr class="titListado">
                          <td colspan="2" class="titListado">
                            &nbsp;SELECCIONE UN ELEMENTO DE MEDIDA</td>
                            <td colspan="1" class="titListado" nowrap align="Right"><asp:LinkButton id="lbtNuevoElemento" Runat="server" >Nueva Lectura</asp:LinkButton>	</td>
                        </tr>
                        <tr class="subtitListado">
	                        <td class="subtitListado" colspan="3" align=right>Filtro <asp:TextBox ID=txtFiltro Runat=server Columns=40 CssClass=texto></asp:TextBox>
	                            [<asp:LinkButton id=lbtFiltrar Runat=server>Filtrar</asp:LinkButton>]
	                            [<asp:LinkButton id=lbtBorrarFiltro Runat=server>Borrar filtro</asp:LinkButton>]	
	                        </td>
                        </tr>
                        <tr class=Ordenar>
                            <td nowrap>
                                <asp:LinkButton ID= lbtordenarPVYCR runat=server CssClass=Ordenar>CÓDIGO PVYCR</asp:LinkButton>
                                <asp:Image ID=imgOrdPVYCRD Visible=false runat=server ImageUrl="images/A_DOWN.gif" align="absmiddle"/>
                                <asp:Image ID=imgOrdPVYCRA Visible=true runat=server ImageUrl="images/A_UP.gif" align="absmiddle" />            
                            </td>
                            <td nowrap>
                                <asp:LinkButton ID= lbtordenarCauce runat=server CssClass=Ordenar>DESCRIPCIÓN CAUCE</asp:LinkButton>
                                <asp:Image ID=imgOrdCauceD Visible=false runat=server ImageUrl="images/A_DOWN.gif" align="absmiddle"/>
                                <asp:Image ID=imgOrdCauceA Visible=false runat=server ImageUrl="images/A_UP.gif" align="absmiddle"/>                    
                            </td>
                            <td nowrap>
                                <asp:LinkButton ID= lbtordenarNumReg runat=server CssClass=Ordenar>Nº REG.</asp:LinkButton>
                                <asp:Image ID=imgOrdNumRegD Visible=false runat=server ImageUrl="images/A_DOWN.gif" align="absmiddle"/>
                                <asp:Image ID=imgOrdNumRegA Visible=false runat=server ImageUrl="images/A_UP.gif" align="absmiddle" />                                                        
                            </td>
                        </tr>
                        <asp:Repeater ID="rptAcequias" runat="server">
                          <ItemTemplate>
                            <tr <%# checkfila()%>>
	        	                <td>&nbsp;<asp:LinkButton ID=lbtPVYCR Runat=server OnClick='PVYCRSeleccionada' CommandArgument='<%#checkCodigoAcequia(Container.DataItem) & "#" & checkFuentedato(Container.DataItem)%>' style="text-decoration:none; color:Maroon"><%#checkCodigoAcequia(Container.DataItem)%> </asp:LinkButton></td>
           	    	            <td><asp:Label ID=lbcauce Runat=server Text=<%#checkNombreMotor(Container.DataItem)%> style="text-decoration:none; color:Maroon"></asp:Label></td>
    		                    <td><asp:Label ID=lbNumRegEstadilloM runat=server Text=<%#checkNumRegEstadilloA(Container.DataItem)%> style="text-decoration:none; color:Maroon" ></asp:Label></td>        
                            </tr>
                          </ItemTemplate>
                        </asp:Repeater>
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
                          <td align="right" style="border-top: 1px solid #666666">
                          </td>
                        </tr>
                      </table>
                    </asp:Panel>
                    <table width="100%">
                      <asp:Panel ID="pnlEtiquetas" runat="server" Visible="false">
                        <asp:Repeater ID="rptAcequiasDetalle" runat="server">
                          <HeaderTemplate>
                            <tr>
                              <th align="center">
                                Tipo Obtención<br /> Caudal</th>
                              <th colspan="2" align="center">
                                Fecha</th>
                              <th align="center">
                                Escala (m)</th>
                              <th align="center">
                                Calado (m)</th>
                              <th align="center">
                                Régimen<br /> Curva</th>
                              <th align="center">
                                Nº Parada</th>
                              <th align="center">
                                Caudal (m3/s)</th>
                              <th align="center">
                                Duda Calidad</th>
                              <th align="center" colspan="9">
                                Observaciones</th>
                            </tr>
                          </HeaderTemplate>
                          <ItemTemplate>
                            <tr>
                              <td class="L" style="background-color: #DDDDDD" align="center">
                                <%#Container.DataItem("TipoObtencionCaudal")%>
                              </td>
                              <td colspan="2" nowrap class="L" style="background-color: #DDDDDD" align="center">
                                <%#DataBinder.Eval(Container.DataItem, "fecha_medida", "{0:dd/MM/yyyy HH:mm}")%>
                              <td class="L" style="background-color: #DDDDDD" align="right">
                                <%#DataBinder.Eval(Container.DataItem, "Escala_M", "{0:#,##0.##}")%>
                              </td>
                              <td class="L" style="background-color: #DDDDDD" align="right">
                                <%#DataBinder.Eval(Container.DataItem, "Calado_m","{0:#,##0.##}")%>
                              </td>
                              <td class="L" align="center" style="background-color: #DDDDDD">
                                <%#Container.DataItem("RegimenCurva")%>
                              </td>
                              <td class="L" style="background-color: #DDDDDD" align="right">
                                <%#Container.DataItem("NumeroParada")%>
                              </td>
                              <td class="L" style="background-color: #DDDDDD" align="right">
                                <%#DataBinder.Eval(Container.DataItem,"Caudal_M3S","{0:#,##0.###}")%> 
                              </td>
                              <td class="L" style="background-color: #DDDDDD" align="right">
                                <%#Container.DataItem("Duda_Calidad")%>
                              </td>
                              <td colspan="9" class="L" style="background-color: #DDDDDD; color: black">
                                <%#Container.DataItem("Observaciones")%>
                              </td>
                            </tr>
                            <tr>
                              <td colspan="18">
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
                        <!--para poder continuar los datos de ambas selects, abro dod repeatrs distintos, pero no cierro el footerTemplate del primero
   y no pongo cadecera al segundo-->
                        <tr>
                          <td colspan="18" style="padding-bottom: 2px">
                            <table width="100%">
                              <tr>
                                <td>
                                  &nbsp;</td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                      </asp:Panel>
                      <tr>
                        <td>
                          <table style="border: 1px solid black">
                            
                            <asp:Panel ID="pnlEstadistica" runat="server">
                              <asp:Repeater ID="rptestadistica" runat="server">
                                <HeaderTemplate>
                                  <tr>
                                    <td class="titulo" colspan="6">
                                      &nbsp;Resumen de Datos</td>
                                  </tr>
                                  <tr>
                                    <th align="center" colspan="1">
                                      Régimen Curva</th>
                                    <th align="center" colspan="1">
                                      Nº Registros</th>
                                    <th align="center" colspan="1">
                                      Porcentaje</th>
                                  </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                  <tr>
                                    <td class="L" style="background-color: #DDDDDD" colspan="1" align="center">
                                      <%#Container.DataItem("REGIMEN")%>
                                    </td>
                                    <td class="L" style="background-color: #DDDDDD" colspan="1" align="center">
                                      <%#Container.DataItem("Registros")%>
                                    </td>
                                    <td class="L" style="background-color: #DDDDDD" colspan="1" align="center">
                                      <%#Container.DataItem("Porcentaje")%>
                                    </td>
                                  </tr>
                                </ItemTemplate>
                              </asp:Repeater>
                              <asp:Repeater runat="server" ID="rptTotalC">
                                <ItemTemplate>
                                  <tr>
                                    <td  colspan="2" style="font-weight: bold">
                                      Total Tipo Caudal
                                      <%#Container.DataItem("tipocaudal")%>
                                    </td>
                                    <td align=center class="L"  colspan="2" style="background-color: #DDDDDD">
                                      <%#Container.DataItem("RegistrosTTipoCaudal")%>
                                    </td>
                                  </tr>
                                </ItemTemplate>
                              </asp:Repeater>
                              <asp:Repeater runat="server" ID="rptTotal">
                                <ItemTemplate>
                                  <tr>
                                    <td  colspan="2" style="font-weight: bold">
                                      Total Registros</td>
                                    <td class="L" align="center" style="background-color: #DDDDDD" colspan="2">
                                      <%#Container.DataItem("RegistrosT")%>
                                    </td>
                                  </tr>
                                </ItemTemplate>
                              </asp:Repeater>
                            </asp:Panel>
                           
                           
                          </table>
                        </td>
                      </tr>
                      <asp:Panel ID="pnlBotones" runat="server" Visible="False">
                        <tr>
                          <td colspan="18" style="padding-bottom: 2px">
                            <table width="100%">
                              <tr>
                                <td>
                                  &nbsp;</td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="18">
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
                          <td colspan="18">
                            <table width="100%" cellpadding="5px" cellspacing="0">
                              <tr>
                                <td align="right" style="border-top: 1px solid #666666">
                                <input type="hidden" id="p1x" />                                
                                  <asp:Button ID="btnCalcular" runat="server" Text='Calcular' CssClass="boton"></asp:Button>
                                  <asp:Button ID="btnAceptarLectura" runat="server" Text='Aceptar' CssClass="boton">
                                  </asp:Button>
                                  <asp:Button ID="btnCancelarLectura" runat="server" Text='Cancelar' CssClass="boton"
                                    CausesValidation="False"></asp:Button>
                                  <asp:Button ID="btngrafico" runat="server" Text="Sección Acequia" CssClass="boton" 
                                  OnClientClick="AbrirGrafico()" />
                                   <asp:Button ID="btnCurvas" runat="server" Text="Curvas Acequia" CssClass="boton"/>
                                  <asp:Button ID="btnRechazadas" runat="server" Text="Ver rechazadas" CssClass="boton" Visible=false/>
                                </td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                      </asp:Panel>
                      <asp:Panel ID="pnlPrincipal" runat="server" Visible="false" EnableViewState="true">
                        <asp:Repeater ID="rptAcequiasDetalle2" runat="server" EnableViewState="true">
                          <HeaderTemplate>
                            <tr>
                              <td style="font-weight: bold; text-align: center">
                                Tipo Obtención<br />
                                Caudal</td>
                              <td colspan="2" style="font-weight: bold; text-align: center">
                                Fecha</td>
                              <td style="font-weight: bold; text-align: center">
                                Escala (m)</td>
                              <td style="font-weight: bold; text-align: center">
                                Calado (m)</td>
                              <td style="font-weight: bold; text-align: center">
                                Régimen Curva</td>
                              <td style="font-weight: bold; text-align: center">
                                Nº Parada</td>
                              <td style="font-weight: bold; text-align: center">
                                Duda Calidad</td>
                              <td style="font-weight: bold; text-align: center">
                                Tiempo (sg)</td>
                              <td style="font-weight: bold; text-align: center">
                                V11 (m/s)</td>
                              <td style="font-weight: bold; text-align: center">
                                V12 (m/s)</td>
                              <td style="font-weight: bold; text-align: center">
                                V21 (m/s)</td>
                              <td style="font-weight: bold; text-align: center">
                                V22 (m/s)</td>
                              <td style="font-weight: bold; text-align: center">
                                V31 (m/s)</td>
                              <td style="font-weight: bold; text-align: center">
                                V32 (m/s)</td>
                             
                                <td>
                                &nbsp;</td>
                              <td>
                                &nbsp;</td>
                            </tr>
                          </HeaderTemplate>
                          <ItemTemplate>
                            <asp:Panel ID="pnlnivusAntes" runat="server" Visible="<%# checkTipoCaudalC(container.dataitem)%>">
                              <tr id="filaNivusAntes" class="TipoCaudal" style="display: <%# checkTipoCaudalC(container.dataitem)%>">
                              <%#checkNivusAntes(Container.DataItem)%>                                
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="pnlPpal" runat="server" Visible="<%# checkTipoCaudalC(container.dataitem)%>">
                              <tr id="filaprincipal">
                                <td>
                                  &nbsp;</td>
                                <td class="TipoCaudal" align=center colspan="2" nowrap>
                                  <asp:Label ID="lbltonto" runat="server"></asp:Label>
                                  <asp:Label ID="lblFecha" runat="server" Text='<%# databinder.eval(container.dataitem,"fecha_medida","{0:dd/MM/yyyy HH:mm}")%>'></asp:Label>
                                                          

                                </td>
                                <td class="L" align="center">
                                  <asp:TextBox Style="font: 10px Verdana; text-align: right" ID="txtEscala" runat="server"
                                    Text='<%#DataBinder.Eval(Container.DataItem, "Escala_M", "{0:#,##0.##}")%>' Width="50px"></asp:TextBox>
                                </td>
                                <td class="L" align="center">
                                  <asp:TextBox Style="font: 10px Verdana; text-align: right" ID="txtCalado" runat="server"
                                    Text='<%#DataBinder.Eval(Container.DataItem, "Calado_m","{0:#,##0.##}")%>' Width="50px"></asp:TextBox>
                                </td>
                                <td width=50px>
                                  <asp:DropDownList Style="font: 10px Verdana; text-align: right" Width=50px ID="ddlRegimenCurva"
                                    runat="server" AutoPostBack="true" Enabled=false>
                                  </asp:DropDownList>
                                </td>
                                <td>
                                  <asp:TextBox ID="txtNParada" runat="server" Style="font: 10px Verdana; text-align: right"
                                    Width="50px" Text='<%#Container.dataItem("NumeroParada") %>' ></asp:TextBox>
                                    <asp:rangevalidator id="rvNParada" ControlToValidate="txtNParada" MinimumValue="0" MaximumValue="999999999" type="Integer" 
                                    display="dynamic" ErrorMessage="Sólo números enteros" runat="server" /> 

                                </td>
                                <td class="L" align="center">
                                  <asp:TextBox ID="txtDudaCalidad" runat="server" Style="font: 10px Verdana; text-align: right"
                                    Text='<%#Container.dataItem("Duda_Calidad") %>'></asp:TextBox>
                                    <asp:rangevalidator id="rvdudacalidad" ControlToValidate="txtDudaCalidad" MinimumValue="0" MaximumValue="999999999" type="Integer" 
                                    display="dynamic" ErrorMessage="Sólo números enteros" runat="server" /> 
                                </td>
                                <td class="L" align="right">
                                  <asp:TextBox ID="txtTiempo" runat="server" Style="font: 10px Verdana; text-align: right"
                                    Text='<%#Container.DataItem("Tiempo_sg")%>' Width="50px"></asp:TextBox>
                                </td>
                                <td class="L" align="center">
                                  <asp:TextBox ID="txtV11MS" runat="server" Style="font: 10px Verdana; text-align: right"
                                  Text='<%#DataBinder.Eval(Container.DataItem, "V11_ms", "{0:#,##0.###}")%>' Width="50px"></asp:TextBox>
                                </td>
                                <td class="L" align="center">
                                  <asp:TextBox ID="txtV12MS" runat="server" Style="font: 10px Verdana; text-align: right"
                                  Text='<%#DataBinder.Eval(Container.DataItem, "V12_ms", "{0:#,##0.###}")%>' Width="50px"></asp:TextBox>

                                </td>
                                <td class="L" align="center">
                                  <asp:TextBox ID="txtV21MS" runat="server" Style="font: 10px Verdana; text-align: right"
                                  Text='<%#DataBinder.Eval(Container.DataItem, "V21_ms", "{0:#,##0.###}")%>' Width="50px"></asp:TextBox>
                                </td>
                                <td class="L" align="center">
                                  <asp:TextBox ID="txtV22MS" runat="server" Style="font: 10px Verdana; text-align: right"
                                  Text='<%#DataBinder.Eval(Container.DataItem, "V22_ms", "{0:#,##0.###}")%>' Width="50px"></asp:TextBox>
                                </td>
                                <td class="L" align="center">
                                  <asp:TextBox ID="txtV31MS" runat="server" Style="font: 10px Verdana; text-align: right"
                                  Text='<%#DataBinder.Eval(Container.DataItem, "V31_ms", "{0:#,##0.###}")%>' Width="50px"></asp:TextBox>
                                </td>
                                <td class="L" align="center">
                                  <asp:TextBox ID="txtV32MS" runat="server" Style="font: 10px Verdana; text-align: right"
                                  Text='<%#DataBinder.Eval(Container.DataItem, "V32_ms", "{0:#,##0.###}")%>' Width="50px"></asp:TextBox>
                                </td> 
                                                          
                                <td align=left>
                                  <asp:RadioButton ID="rbSelCalculoA" runat="server" TextAlign=Right Text="Acep."
                                    Checked="true" GroupName="Seleccionar" />
                                </td>
                                <td align=left>
                                  <asp:RadioButton ID="rbSelCalculoR" runat="server" TextAlign=Right Text="Rech."
                                    Checked="false" GroupName="Seleccionar" Width=8px />
                                  <asp:TextBox ID="txtCodigoPVYCR" runat="server" Text='<%# Container.dataitem("CodigoPVYCR") %>'
                                    Style="display: none"></asp:TextBox>
                                  <asp:TextBox ID="txtCod_Fuente_Dato" runat="server" Text='<%# Container.dataitem("Cod_Fuente_Dato") %>'
                                    Style="display: none"></asp:TextBox>
                                  <asp:TextBox ID="txtFecha" runat="server" Text='<%# Databinder.eval(Container.dataitem,"Fecha_Medida","{0:dd/MM/yyyy HH:mm:ss}") %>'
                                    Style="display: none"></asp:TextBox>
                                  <asp:TextBox ID="txtidElementoMedida" runat="server" Text='<%# Container.dataitem("idElementoMedida") %>'
                                    Style="display: none"></asp:TextBox>                                  
                                </td>
                              </tr>
                              <tr>
                              <td></td>
                                <td style="font-weight: bold">
                                  Observaciones:
                                </td>
                                <td colspan="10" class="L" style="display: <%# checkTipoCaudalC(container.dataitem)%>">
                                  <asp:TextBox Style="font: 10px Verdana" ID="txtObservaciones" runat="server" Text='<%#Container.DataItem("Observaciones")%>'
                                    Width="99%" TextMode="SingleLine"></asp:TextBox>
                                </td>
                                <td style="font-weight: bold">
                                Usuario:</td>                                
                              <td colspan="2" class="L" style="color: black" align=center>
                                <asp:TextBox ID="txtLogin" Style="font: 9px verdana; width:130px" runat="server" Text='<%#Container.DataItem("login")%>'
                                  TextMode="SingleLine" Enabled=false></asp:TextBox>
                              </td>                            
                                
                                <td width=20px>
                                  <asp:TextBox runat="server" ID="txtColor" ReadOnly="true" Width=20px BorderColor=transparent></asp:TextBox>
                                </td>
                                <td nowrap>
                                  <asp:CheckBox ID="cbestimadaC" runat="server" EnableViewState="true"  Text="Estimada" Enabled=<%#estaActivo(container.dataitem)%> TextAlign=Right OnCheckedChanged="MarcarEstimada" />
                                  <asp:CheckBox ID="cbestimadaA" runat="server" EnableViewState="true" Visible=false />
                                  <asp:CheckBox ID="cbestimadaF" runat="server" EnableViewState="true"  Visible=false />
                                </td>
                              </tr>                              
                            </asp:Panel>
                            <!--FILA CAUDAL C-->
                            <asp:Panel ID="pnlC" runat="Server" Visible="<%# checkTipoCaudalC(container.dataitem)%>">
                              <tr style="display: <%# checkTipoCaudalC(container.dataitem)%>">
                                <td colspan="17">
                                  <table width="100%">
                                    <tr>
                                      <td class="L" align="left" style='font: bold 11px Tahoma; text-align: left; background-color: #DDDDDD' width="20">
                                        <asp:TextBox ID="txtTOCaudalC" runat="server" Text='<%#Container.DataItem("TipoObtencionCaudal")%>'
                                          Style="font: 10px Verdana; text-align: left; border: 0px; background-color: #DDDDDD" Width=20 ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L"width=1015>
                                        Caudal (m3/s):
                                        <asp:TextBox ID='txtCaudalC' runat="server" Text='<%#DataBinder.Eval(Container.dataitem,"Caudal_M3S", "{0:#,##0.###}") %>'
                                          Style='font: 10px Verdana; text-align: left; border: 0px' ReadOnly="true" width=1015 ></asp:TextBox>
                                      </td>
                                      <td nowrap class="L">
                                        <asp:CheckBox ID="cbRegimenCurva" runat="server" EnableViewState="true" OnCheckedChanged="SeleccionarcbRegimenCurva" AutoPostBack="true" Text="Habilitar Régimen Curva" TextAlign="Left" />
                                      </td>
                                      <td nowrap>
                                        <asp:CheckBox ID="cbC" runat="server" EnableViewState="true" />
                                      </td>
                                    </tr>
                                  </table>
                                </td>
                              </tr>
                            </asp:Panel>
                            <!--FILA CAUDAL A-->
                            <asp:Panel ID="pnlA" runat="server" Visible="<%# checkTipoCaudalA(container.dataitem)%>">
                              <tr>
                                <td colspan="17">
                                  <table width="100%">
                                    <tr>
                                      <td class="L" style='font: bold 11px Tahoma; background-color: #DDDDDD' align="left">
                                        <asp:TextBox ID="txtTOCaudalA" runat="server" Text='<%#Container.DataItem("TipoObtencionCaudal")%>'
                                          Style="font: 10px Verdana; text-align: left; border: 0px; background-color: #DDDDDD"  Width=20 ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Caudal (m3/s):
                                        <asp:TextBox ID='txtCaudalA' runat="server" Text='<%#DataBinder.Eval(Container.dataitem,"Caudal_M3S", "{0:#,##0.###}")%>'
                                          Style='font: 10px Verdana; text-align: left; border: 0px' ReadOnly="true"></asp:TextBox></td>
                                      <td class="L">
                                        Superficie Teórica (m2):
                                        <asp:TextBox ID='txtSuperficieTM' runat="Server" Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Altura Fangos (m):
                                        <asp:TextBox ID='txtaltM' runat="Server" Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Superficie Fangos (m2):
                                        <asp:TextBox ID='TxtSuperficieFM' runat="Server" Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Superficie Real (m2):
                                        <asp:TextBox ID='TxtSuperficieRM' runat="Server" Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Velocidad Molinete (m/s):
                                        <asp:TextBox ID='TxtVelocidadM' runat="Server" Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td>
                                        <asp:CheckBox ID="cbA" runat="server" EnableViewState="true" />
                                      </td>
                                    </tr>
                                  </table>
                                </td>
                              </tr>
                            </asp:Panel>
                            <!--FILA CAUDAL F-->
                            <asp:Panel ID="pnlF" runat="server" Visible="<%# checkTipoCaudalF(container.dataitem)%>">
                              <tr>
                                <td colspan="17" >
                                  <table width="100%">
                                    <tr>
                                      <td class="L" align="left" style='font: bold 11px Tahoma; background-color: #DDDDDD'>
                                        <asp:TextBox ID="txtTOCaudalF" runat="server" Text='<%#Container.DataItem("TipoObtencionCaudal")%>'
                                          Style="font: 10px Verdana; text-align: left; border: 0px; background-color: #DDDDDD" Width=20 ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Caudal (m3/s):
                                        <asp:TextBox ID='txtCaudalF' runat="Server"  Text='<%#DataBinder.Eval(Container.dataitem,"Caudal_M3S", "{0:#,##0.###}")%>' Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Superficie Teórica (m2):
                                        <asp:TextBox ID='TxtSuperficieTF' runat="Server" Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Altura Fangos (m):
                                        <asp:TextBox ID='TxtAltF' runat="Server" Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Superficie Fangos (m2):
                                        <asp:TextBox ID='TxtSuperficieFF' runat="Server" Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Superficie Real (m2):
                                        <asp:TextBox ID='TxtSuperficieRF' runat="Server" Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td class="L">
                                        Velocidad Flotador (m/s):
                                        <asp:TextBox ID='TxtVelocidadF' runat="Server" Style='font: 10px Verdana; text-align: left;
                                          border: 0px' ReadOnly="true"></asp:TextBox>
                                      </td>
                                      <td>
                                        <asp:CheckBox ID="cbF" runat="server" EnableViewState="true" />
                                      </td>
                                    </tr>
                                  </table>
                                </td>
                              </tr>
                             
                            </asp:Panel>                            
                            <asp:Panel ID="pnlNivusDespues" runat="server" Visible="<%# checkTipoCaudalF(container.dataitem)%>">
                              <tr id="filaNivusDespues" class="TipoCaudal" style="display: <%# checkTipoCaudalF(container.dataitem)%>">
                              <%#checkNivusDespues(Container.DataItem)%>                                
                                </tr>
                                 <tr>
                                <td colspan="17" style='border-top: solid 1px black;'>
                                  &nbsp;</td>
                              </tr>
                            </asp:Panel>                          
                          </ItemTemplate>
                          <FooterTemplate>
                          </FooterTemplate>
                        </asp:Repeater>
                      </asp:Panel>
                    </table>
                     <asp:Panel ID="pnlNuevoElemento" runat ="server" Visible="false" Width="100%" >
                      <table width="100%" ><tr>
                             <td style="background-color: #CAD5DD">Cauce</td>
                             <td> <asp:DropDownList ID="ddlEDCauce" runat="server" style="font:9px Verdana; text-align: right;" AutoPostBack="true"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvEDCauce" Text="*" ControlToValidate="ddlEDCauce" runat="server" ></asp:RequiredFieldValidator></td>                                                    
                             <td style="background-color: #CAD5DD">Código PVYCR</td>
                             <td> <asp:DropDownList ID="ddlEDCodigoPVYCR" runat="server" style="font:9px Verdana; text-align: right;" AutoPostBack="true"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvedCodigo" runat="server" ControlToValidate="ddlEDCodigoPVYCR" Text="*"></asp:RequiredFieldValidator></td>                              
                             <td style="background-color: #CAD5DD"> Id Elemento Medida</td>
                             <td><asp:DropDownList ID="ddlEDElemento" runat="server" style="font:9px Verdana; text-align: right;"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvelemento" Text="*" ControlToValidate="ddlEDElemento" runat="server" Display="dynamic" ></asp:RequiredFieldValidator></td>                             
                             </tr>
                             <tr>
                            <td style="background-color: #CAD5DD">Código Fuente Dato</td>
                             <td> <asp:DropDownList ID="ddlEDCodFuenteDato" runat="server" style="font:9px Verdana; text-align: right;" AutoPostBack="true"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="*" ControlToValidate="ddlEDCodFuenteDato" runat="server" ></asp:RequiredFieldValidator></td>                             
                             <td style="background-color: #CAD5DD">Fecha Medida</td>
                             <td nowrap><asp:TextBox ID="txtEDFechaMedida" runat="server" CssClass="texto" Width="80px"></asp:TextBox>
                              <asp:Image ID="imgCalendario" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />  
                             <asp:RequiredFieldValidator ID="efvedfecha" Text="*" ControlToValidate="txtEDFechaMedida" runat="server" Display="Dynamic" ></asp:RequiredFieldValidator>
                             </td>
                             <td style="background-color: #CAD5DD">Tipo Obtención Caudal</td>
                             <td><asp:TextBox ID="txtEDTipoObtencion" runat="server" CssClass="texto" Width="10px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" ControlToValidate="txtEDTipoObtencion" runat="server" Display="dynamic" ></asp:RequiredFieldValidator></td>
                             </tr>                             
                             <tr>
                             <td>Escala (m)</td>
                             <td><asp:TextBox ID="txtEDEscala" runat="server" CssClass="textoNumerico"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtEDEscala" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                             <td>Calado (m)</td>
                             <td><asp:TextBox ID="txtEDCalado" runat="server" CssClass="textoNumerico"> </asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEDCalado" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                             <td>Tiempo (sg)</td>
                             <td><asp:TextBox ID="txtEDTiempo" runat="server" CssClass="textoNumerico"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEDTiempo" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                             </tr>
                             <tr>
                             <td>V11 (m/s)</td>
                             <td><asp:TextBox ID="txtEDV11" runat="server" CssClass="textoNumerico"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtEDV11" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                             <td>V12 (m/s)</td>
                             <td><asp:TextBox ID="txtEDV12" runat="server" CssClass="textoNumerico"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEDV12" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>                             
                             <td>V21 (m/s)</td>
                             <td><asp:TextBox ID="txtEDV21" CssClass="textoNumerico" runat="server" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtEDV21" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>                             
                             </tr>
                             <tr>
                             <td>V22 (m/s)</td>
                             <td><asp:TextBox ID="txtEDV22" CssClass="textoNumerico" runat="server" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtEDV22" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td> 
                             <td>V31 (m/s)</td>
                             <td><asp:TextBox ID="txtEDV31" CssClass="textoNumerico" runat="server" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtEDV31" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td> 
                             <td>V32 (m/s)</td>
                             <td><asp:TextBox ID="txtEDV32" CssClass="textoNumerico" runat="server" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtEDV32" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>                              
                             </tr>
                             <tr>
                             <td>Usuario</td>    
                              <td><asp:TextBox ID="txtEDUsuario" runat="server" CssClass="texto"></asp:TextBox></td>                             
                              <td>Observaciones</td><td colspan="3"><asp:TextBox ID="txtEDObservaciones" runat="server" CssClass="texto" Width="90%"></asp:TextBox></td>
                              </tr>
                              <tr>
                              <td colspan="6" align="right">
                              <asp:Button ID="btnEDAceptar" runat="server" Text="Aceptar" CssClass="boton" />
                              <asp:Button ID="btnEDCancelar" runat="server" Text="Cancelar" CssClass="boton" CausesValidation="false" />
                              </td>
                              </tr>
                              <tr>
                              <td colspan="6" align="left">                              
                              <asp:RegularExpressionValidator ID="rev2" runat="server" ControlToValidate="txtEDfechamedida"
                                ErrorMessage="Formato fecha 'dd/mm/yyyy hh:mm'" Font-Size="10pt" ValidationExpression="\d\d/\d\d/\d\d(\d\d){0,1}( \d\d:\d\d){0,1}"
                                Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEDTipoObtencion" ErrorMessage="El Tipo Obtención Caudal debe ser C, F o A" ValidationExpression="[CFA]" Display="Dynamic"></asp:RegularExpressionValidator>                             
                                
                              </td>
                              </tr>
                              </table>
                      </asp:Panel>                    
                  </td>
                </tr>
                <tr>
                  <td>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" CssClass="filaPaginacion" Visible="false">
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
