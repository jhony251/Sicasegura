<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformacionGeneralE.aspx.vb" Inherits="SICAH_InformacionGeneralE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />    
 <script language="javascript" src="..\js/utilesMenus.js"></script>  
</head>
<body style="width: 900px; background-color:White">
    <form id="form1" runat="server">
        <table width="100%" cellspacing="0" align="center" style="border: 0px solid #666666;background-color: white">
       <tr>
        <td style="width: 900px">
           <table cellspacing="0" width="100%">
                <!-- Celda Menú - Contenido -->
                    <tr>
              <td style="width: 900px">
                 <table width="100%">
                     <tr>
                       <asp:Label ID="lblPestanyasArbol" runat="server"></asp:Label>
                       <td width=50% visible="false" >
                           <strong>
                             <asp:TextBox ID="txtCodigoPVYCR" runat="server" CssClass="texto" ToolTip="CodigoPVYCR"
                                   Width="115px" Visible="false">[CodigoPVYCR]</asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCodigoPVYCR"
                                   Display="Dynamic" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator></strong>
                                   <asp:TextBox ID="txtIdElementoMedida" runat="server" CssClass="texto" Width="27px" ToolTip="Id Elemento Medida" Visible="false">[EM]</asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtIdElementoMedida" Display="Dynamic">*</asp:RequiredFieldValidator>&nbsp;
                              <asp:TextBox ID="txtDescripcionElementoMedida" runat="server" CssClass="texto" Visible="False"
                               Width="44px"></asp:TextBox><asp:Label ID="LBLinfo" runat="server" Width="0px" Visible="false"></asp:Label>
                           
                            <b><asp:LinkButton ID="lbtAceptar" runat="server" Text="Búsqueda Rápida" CssClass="enlaceLecturas" Visible="false"></asp:LinkButton></b>
                              <asp:ImageButton ID="imgVisor" runat="server" ImageUrl="~/SICAH/images/iconos/imgVisor.gif"
                               ImageAlign="TextTop" Visible="false"/>

                           </td>   

                 </tr>
                  </table>
              </td>
                         
             </tr>
                <tr>
               
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:2px; padding-right:2px; width:100%" valign=top>
                        <!-- Panel listar Elementos -->                      
                       <asp:Panel ID=pnlEDElementos Runat=server BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px">
                            <table width="100%" cellspacing=0 cellpadding=1 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td style="height: 25px; vertical-align: middle; width: 100px; text-align: left; background-color: gainsboro;">
                                    <strong><span style="color: #003399">Código PVYCR</span></strong></td>
                                    <td style="width: 139px; height: 25px">
                                    <asp:TextBox ID="txtEdCodigoPVYCR" runat="server" CssClass="texto" ReadOnly="true"></asp:TextBox>
                                    <td style="height: 25px; vertical-align: middle; width:100px; text-align: left; background-color: gainsboro;">
                                    <strong><span style="color: #003399">Id Elemento</span></strong></td>
                                    <td style="width: 110px; height: 25px"><asp:TextBox ID=txtIdElemento runat=server CssClass=texto Width=50px ReadOnly="true"></asp:TextBox></td>                             
                                    <td>Tipo </td>
                                    <td style="width: 139px; height: 25px;">
                                      <asp:TextBox ID="txtTipo" runat="server" CssClass="texto" ReadOnly="true"></asp:TextBox>

                                    </td>                                      
                                </tr>                          
                               </table>           
                            </td>
                            </tr>
                            </table>
   
                        </asp:Panel>              
                        <!-- Fin Panel listar elementos -->
                        
                    </td>
                    <!-- Fin Celda Contenido -->        
                </tr>
                <!-- Fin Celda Menú - Contenido -->
                   

        
          </table> 
        
</td></tr></table>
    </form>
    </body>
</html>
