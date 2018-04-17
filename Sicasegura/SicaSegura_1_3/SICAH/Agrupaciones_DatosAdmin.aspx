<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones_DatosAdmin.aspx.vb" Inherits="SICAH_Agrupaciones_DatosAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
      <!-- Estilos tanto para el árbol como para agrupaciones -->
      <link href="../ext-all.css" rel="stylesheet" type="text/css" />  
       <!-- referencias para la agrupaciones -->
      
      <link href="../styles.css" rel="stylesheet" type="text/css" />       
      <link href="../styles.css" rel="stylesheet" type="text/css" />            
        <!-- Estilos de página -->
        <link href="../estilosAgrupaciones.css" rel="stylesheet" type="text/css" id="theme" /> 
</head>
<body style="background-color:#eeead2">
     <form id="form1" runat="server" >
        <table cellspacing="2" align="left" width="100%"  style="background-color:#eeead2" >
        <tr>
        <td>
           <!-- Panel Puntos Agrupacion-->
                <asp:Panel ID="pnlAgrupacion" Runat=server Visible="true" BackColor="#eeead2">
                <asp:label id="lblAgrupacionSel" runat=Server Visible=false Height="16px"></asp:label>                
                <br><asp:Label ID=lblTitulo runat=server CssClass=tituloSec><B>DATOS ADMINISTRATIVOS: ID RACCS // DATOS ADMINISTRATIVOS:</B></asp:Label><br/>
                    <table width=100% cellspacing=0 cellpadding=0 class="tablaEdicion" style="background-color:#eeead2"  >
                    <tr>
                    <td><table>
                    <tr ><td><asp:Label ID="lblPestanyas" runat="server"></asp:Label></td></tr>
                        <asp:Panel ID="pnlAgrupacion1" Runat="server" border="1" BackColor="WhiteSmoke" BorderColor="Navy"  >
                            <tr><td><table cellpadding="2" cellspacing="2" width="100%" style="border:1px solid black;background-color:WhiteSmoke" >                              
                                <tr>
                                    <td>
                                        <table cellpadding="2" cellspacing="2"  >
                                            <tr>
                                                <td align="right"  >
                                                    Sección</td>
                                                <td nowrap> 
                                                    <asp:Label ID="txtSec" runat="server" CssClass="displayClave" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    Tomo</td>
                                                <td >
                                                    <asp:Label ID="txtTomo" runat="server" CssClass="displayClave" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    Hoja</td>
                                                <td >
                                                    <asp:Label ID="txtHoja" runat="server" CssClass="displayClave" Font-Bold="true" Font-Size="Medium" ReadOnly=true />
                                                </td>
                                            </tr>
                                            <tr>                                                
                                                <td >
                                                    Acuífero</td>
                                                <td colspan="3" >
                                                    <asp:TextBox ID="txtACUIF" runat="server" width="350px"  CssClass="texto" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Regadío</td>
                                                <td>
                                                    <asp:CheckBox ID="chkUSORE" runat="server" Enabled="false"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    Volumen Regadío</td>
                                                 <td>
                                                     <asp:TextBox ID="txtVOLRE" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Industrial</td>
                                                <td>
                                                    <asp:CheckBox ID="chkUSOIN" runat="server" Enabled="false"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    Volumen Industrial</td>
                                                 <td>
                                                     <asp:TextBox ID="txtVOLIN" runat="server" width="100" 
                                                        CssClass="textoNumerico"  ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Abastecimiento</td>
                                                <td>
                                                    <asp:CheckBox ID="chkUSOAB" runat="server"  Enabled="false"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    Volumen Abastecimiento</td>
                                                 <td>
                                                     <asp:TextBox ID="txtVOLAB" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Ganadero</td>
                                                <td>
                                                    <asp:CheckBox ID="chkUSOGA" runat="server" Enabled="false"></asp:CheckBox>
                                                </td>
                                                <td>
                                                    Volumen Ganadero</td>
                                                 <td>
                                                     <asp:TextBox ID="txtVOLGA" runat="server" CssClass="textoNumerico" width="100" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Otros</td>
                                                <td>
                                                    <asp:TextBox ID="txtUSOOT" runat="server" CssClass="texto" width="100" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                 <td>
                                                    Volumen Otros</td>
                                                 <td>
                                                     <asp:TextBox ID="txtVOLOT" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                              </tr>
                                               <tr>
                                                <td></td>
                                                <td>
                                                    VOLUMEN POR AÑO: </td>
                                                <td>
                                                    Regadío</td>
                                                <td>
                                                    <asp:TextBox ID="txtVOL1RE" runat="server" 
                                                        CssClass="textoNumerico" width="100" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Industrial</td>
                                                <td>
                                                    <asp:TextBox ID="txtVOL1IN" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                 <td>
                                                    Abastecimiento</td>
                                                <td>
                                                    <asp:TextBox ID="txtVOL1AB" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td></td><td></td>
                                                <td>
                                                    Ganadero</td>
                                                <td>
                                                    <asp:TextBox ID="txtVOL1GA" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Otros</td>
                                                <td>
                                                    <asp:TextBox ID="txtVOL1OT" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Caudal Máximo Instantáneo </td>
                                                <td>
                                                    <asp:TextBox ID="txtCAUDAL1" runat="server"  width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Caudal Medio</td>
                                                <td>
                                                    <asp:TextBox ID="txtCAUDALME" runat="server"  width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                     </table>
                                    </td>
                                </tr>
                            </table>
                            </td></tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlAgrupacion2" Runat="server" border="1" BackColor="#eeead2" BorderColor="Navy"  >
                            <table cellpadding="2" cellspacing="2" width="100%" style="border:1px solid black;background-color:WhiteSmoke" >                          
                                <tr>
                                    <td>
                                        <table cellpadding="2" cellspacing="2" >
                                             <tr>
                                                <td>
                                                    Superficie</td>
                                                <td>
                                                    <asp:TextBox ID="txtSUPER" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Desnivel</td>
                                                 <td>
                                                     <asp:TextBox ID="txtDESN" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Salto</td>
                                                <td>
                                                     <asp:TextBox ID="txtSALTO" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Potencia</td>
                                                 <td>
                                                     <asp:TextBox ID="txtPOTEN" runat="server" width="100" 
                                                        CssClass="textoNumerico" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>                                            
                                             <tr>
                                                <td>
                                                    Título</td>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtTITULO" runat="server" CssClass="texto" 
                                                        TextMode="multiLine" Width="100%" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    Condiciones Específicas</td>
                                                <td colspan="6">
                                                    <asp:TextBox ID="txtCESP" runat="server" CssClass="texto" 
                                                        TextMode="multiLine" Width="100%" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    Observ. 1</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtOBS1" runat="server" CssClass="texto" width="330"
                                                        TextMode="multiLine" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                 <td>
                                                    Observ. 2</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtOBS2" runat="server" CssClass="texto" 
                                                        TextMode="multiLine" Width="330" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                               <td>
                                                    Observ. 3</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtOBS3" runat="server" CssClass="texto" 
                                                        TextMode="multiLine" Width="330" ReadOnly=true></asp:TextBox>
                                                </td>
                                                 <td>
                                                    Observ. 4</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtOBS4" runat="server" CssClass="texto" 
                                                        TextMode="multiLine" Width="330" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                 <td>
                                                    Observ. 5</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtOBS5" runat="server" CssClass="texto" 
                                                        TextMode="multiLine" Width="330" ReadOnly=true></asp:TextBox>
                                                </td>
                                                <td>
                                                    Observ. 6</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtOBS6" runat="server" CssClass="texto" 
                                                        TextMode="multiLine" Width="330" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Fecha Inscripción</td>
                                                <td>
                                                    <asp:TextBox ID="txtFECINS" runat="server"  width="100" 
                                                        CssClass="texto" ReadOnly=true></asp:TextBox>
                                                </td>
                                                <td>
                                                    Firmado</td>
                                                 <td colspan="2">
                                                     <asp:TextBox ID="txtFIRMADO" runat="server" width="100" 
                                                        CssClass="texto" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Fecha Certificación</td>
                                                <td>
                                                     <asp:TextBox ID="txtFECCER" runat="server" width="100" 
                                                        CssClass="texto" ReadOnly="true"></asp:TextBox>
                                                </td>
                                             </tr>   
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                     </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel></table>
                    </td>
                    </tr>
                    </table>
                </asp:Panel>   
                <asp:Panel ID="pnlAgrupacionesSinInscrip" Runat="server" Visible="false" >
                <table cellspacing="2" align="center" width="100%" height="300" cellpadding=2  >
                <tr valign="middle">
                <td valign="middle" align="center" height="19" style="background-color:white; border:1px solid black">No hay información disponible</td>
                </tr>
                </table>
                </asp:Panel>              
                <!-- Fin Panel Editar Contadores -->
        </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
