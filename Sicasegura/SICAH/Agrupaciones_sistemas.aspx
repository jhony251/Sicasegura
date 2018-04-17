<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones_sistemas.aspx.vb" Inherits="SICAH_Agrupaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
     <link rel="stylesheet" href="..\styles.css"/>
  
    <!-- JScripts de Maquetación de Desplegables -->
    <script type="text/jscript" language="javascript">
    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación del registro"+"\n"+"Los nodos dependientes se borrarán")==true)
        return true;
      else
        return false;
    }
    
   
    </script>    
</head>
<body onload="LoadEvent()" bgcolor="#EEEAD2">
    <form id="form1" runat="server" method=post>
      <table width="100%" cellspacing="2" align="center" style="border: 1px solid #666666;background-color: white">
       <tr>
        <td>
          <table cellspacing="0" cellpadding="1" width="100%">
           <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label><tr>
              <td>
                <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <!-- Celda Menú - Contenido -->
                <tr>
                <!-- Celda Menú -->
                  <td colspan="5"valign="top" style="padding-top: 20px; width:20%;">        
                     <%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%>
                   </td>
                    <td style="background-image:url(../images/dot2.gif);width:3px"></td>
                    <!-- Fin Celda Menú -->
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width: 79%;" valign=top>
                        <!-- Panel listar Sistemas -->
                                            
                        <asp:Panel ID="pnlSistemas" Runat=server>
                            <table width="100%">
                            <tr>
                                <td align="right"><a>Filtrar</a></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666">
                                <table align="right">
                                <tr>
                                    <td>
                                    <asp:TextBox ID="txtFiltroNombreSistema" runat="server" Text="[Nombre Agrupación]" CssClass="texto"></asp:TextBox>
                                    </td>
                                    <td>
                                    <asp:Button ID="btnFiltroAceptar" runat="server" text="Filtrar" CssClass="boton" />
                                    <asp:Button ID="btnFiltroCancelar" runat="server" Text="Limpiar" CssClass="boton" />
                                    </td>
                                
                                </tr>
                                <tr>
                                    <td colspan="3" style="color:#555555"><i></i></td>
                                </tr>
                                </table>
                                </td>
                            </tr>
                            </table>
                            
                            <table width="100%">  
                                <tr>
                                    <td colspan="2" style="height: 17px" class="tituloSec">
                                        MANTENIMIENTO DE AGRUPACIONES</td>
                                    <td align="left" style="width: 121px; height: 17px">
                                    </td>
                                </tr>
                                <tr><td style="width: 89px; height: 258px;"> 
                             
                                <asp:Panel ID="pnlArbol" runat="server" ScrollBars="Both" BorderStyle="Solid" BorderWidth="1px" Height="300px" Width="500px" HorizontalAlign="Left">
                               
                                    <asp:TreeView ID="tvwSistemas" runat="server" Height="100%" Width="100%">
                                      <SelectedNodeStyle BackColor="PaleGoldenrod" Font-Bold="True" Font-Underline="True" />
                                    </asp:TreeView>
                                
                                </asp:Panel>
                            
                                </td>
                                    <td align="left" style="width: 193px; height: 258px;" valign="top">
                                       
                                        <table width="100%" style="display: <% if visibleSegunPerfil()=false then %> none <%else %> block <%end if %>">
                                            <tr>
                                                <td colspan="3" style="font-weight: bold">
                                                    <asp:Label ID="lblinfo" runat="server" Width="128px">Acciones</asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 5px; height: 5px;"><asp:ImageButton ID="imgNuevoRaiz" runat="server" ImageUrl="~/SICAH/images/SubSistemas.gif" /></td>
                                                <td colspan="2">
                                                    Nueva Agrupación Raiz</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 5px; height: 5px;">
                                                    <asp:ImageButton ID="imgNuevo" runat="server" ImageUrl="~/SICAH/images/SubSistemas2.gif" /></td>
                                                <td colspan="2">
                                                    Nueva SubAgrupación</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px; width: 5px;">
                                                <asp:ImageButton ID="imgEditar" runat="server" ImageAlign="absmiddle" ImageUrl="../images/edit.gif" ToolTip="Ediar Nodo" Height="15px" Width="12px" /></td>
                                                <td style="height: 16px; width: 58px;">
                                        Editar</td>
                                                <td style="width: 35px; height: 16px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px; width: 5px;">
                                                <asp:ImageButton ID="imgBorrar" runat="server" ImageAlign="absmiddle" 
                                                    ImageUrl="../images/del.gif" Width="16px" ToolTip="Borrar Nodo" CausesValidation="False" /></td>
                                                <td style="height: 13px; width: 58px;">
                                        Borrar</td>
                                                <td style="width: 35px; height: 13px">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 121px; height: 258px;" align="left">
                                    </td>
                                </tr>
                            </table>
                            
                        </asp:Panel> 
                        <!-- Fin Panel listar Sistemas -->
                        
                        <!-- Panel Editar  Sistema-->
                        <asp:Panel ID="pnlEDSistemas" Runat=server Visible="False">
                        <asp:label id="lblSistemaSel" runat=Server Visible=False Height="16px"></asp:label>
                        <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec><B>AGRUPACIÓN: </B></asp:Label><br /><br />
                            <table width=100% cellspacing=0 cellpadding=10 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td width="22%">Nombre</td>
                                    <td colspan="3"><asp:TextBox ID=txtNombre runat=server CssClass=texto Width="296px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID=RequiredFieldValidator4 runat=server Display=Dynamic ErrorMessage="El nombre la agrupación es obligado" ControlToValidate=txtNombre></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="4" style="border-top:1px solid #666666">
                                        <asp:Button ID="btnAceptar" Runat="server" cssclass="boton" Text="Aceptar" />
                                        <asp:Button ID="btnCancelar" Runat="server" CausesValidation="False" 
                                            cssclass="boton" Text="Cancelar" />
                                    </td>
                                </tr>
                                </table>            
                            </td>
                            </tr>
                            </table>

                        </asp:Panel>                 
                        <!-- Fin Panel Editar Contadores -->
                    </td>
                    <!-- Fin Celda Contenido -->
                </tr>
                <!-- Fin Celda Menú - Contenido -->

                <!-- Número de páginas -->
                
                <!-- Fin Número de páginas -->
    </table>
        </td></table>
       </tr>
    </form>
</body>
</html>
