<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UsuariosDeSistemas.aspx.vb" Inherits="SICAH_UsuariosDeSistemas" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
      <link href="../styles.css" rel="stylesheet" type="text/css" />       
      
    <script language="javascript" type="text/javascript">
        function confirmar_borrado() {
            if (confirm("¿Está seguro de la eliminación del Usuario?") == true)
                return true;
            else
                return false;
        }

        function filtrar() {
            if (document.getElementById("FilaFiltro").style.display == "none") {
                document.getElementById("FilaFiltro").style.display = "block";
                document.getElementById("MostrarFiltro").innerText = "Ocultar Filtro";

            } else {
                document.getElementById("FilaFiltro").style.display = "none";
                document.getElementById("MostrarFiltro").innerText = "Mostrar Filtro";
            }
        }        
    </script>
</head>


<body bgcolor="#EEEAD2">
<input type="hidden" value="<%=verLbl()%>" id="IdSel"/>
    <form runat="server">     
        <table width="100%" cellspacing="2" align="center" style="border: 1px solid #666666;background-color: white">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="1" width="100%">
                        <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label>
                        <tr>
                            <td>
                                <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">                
                                    <tr>
                                        <td style="padding-left:20px; padding-right:20px; width:79%" valign=top>
                                            <!-- Panel listar Usuarios -->                                                                                                              
                                            <asp:Panel ID=pnlUsuarios Runat=server Visible=true>
                                                <table width=100%>
                                                    <tr><td align="right"></td></tr>
                                                    <tr>
                                                        <td style="background-color:#cccccc; border:1px solid #666666">
                                                            <table align="right">
                                                                <tr>
                                                                    <td colspan="2"  align="right">Registros a mostrar  <asp:TextBox  ID="txtRegistros"  runat="server" Text="10" Width="30px"  CssClass="texto"></asp:TextBox><asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtRegistros"
                                                                        Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                                                        <asp:Button runat="server" ID="btnVertodos" cssclass="boton" Text="Actualizar"  />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width=100%>  
                                                    <tr><td class="tituloSec" colspan=2>MANTENIMIENTO DE USUARIOS DE SISTEMAS</td></tr>
                                                    <tr>
                                                        <td  align="left" width="90%"><a id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a></td>
                                                        <td align="right" nowrap><asp:LinkButton Visible=<%#VisibleSegunPerfil() %> runat="server" ID="lbtNuevo" OnClick="nuevoUsuario">&nbsp;Nuevo Usuario</asp:LinkButton></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="encabListado">Login</td>                                        
                                                        <td class="encabListado">&nbsp;</td>                             
                                                    </tr>
                                                    <tr id="FilaFiltro"  >         
                                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFLogin" CssClass="texto" Columns="20"></asp:TextBox></td>                                                                                       
                                                         <td class="encabListado"></td>
                                                         <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsReg"  Runat="server" OnClick="AceptarFiltro" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                                    </tr>   
                                                    <asp:Repeater ID=rptCauces runat=server>                                                     
                                                        <ItemTemplate>
                                                        <tr <%# checkFila()%>>                                            
                                                            <td><%#Container.DataItem("Login")%></td>
                                                            <td><input type="checkbox" <%#Chequeado(Container.DataItem("Externo"))%> disabled> </td>
                                                            <td align="right">
                                                                <asp:LinkButton ID=lbtEdit Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='editar' CommandArgument='<%# container.dataitem("ID_Usuario")%>'><img src="../images/edit.gif" border=0 alt="Editar datos"></asp:LinkButton>
                                                                <asp:LinkButton ID=lbtDelete Visible=<%# VisibleSegunPerfil() %> Runat=server CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("ID_Usuario") %>' CausesValidation="false"><img src="../images/del.gif" border=0 alt="Borrar"></asp:LinkButton>
                            	                            </td>
            	                                        </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <!-- Número de páginas -->
                                                    <tr><td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=2></td></tr>
                                                    <!-- Fin Número de páginas -->                  
                                                </table>
                                            </asp:Panel> 
                                            <!-- Fin Panel listar Usuarios -->
                                        
                                            <!-- Panel Editar Usuarios -->  
                                            <asp:Panel ID="pnlEDUsuarios" runat=server Visible=false>                      
                                                <asp:label id=lblIdSel runat=Server Visible=False></asp:label><br/>
                                                <asp:Label ID=lblTitulo runat=server CssClass=tituloSec   ForeColor="#666699" Width="232px">&nbsp;<b>MANTENIMIENTO SISTEMA: </b></asp:Label>
                                                <table width="100%" cellspacing=0 cellpadding=1 class="tablaEdicion" border="0" >
                                                    <tr>
                                                        <td style="padding-bottom:20px;" valign="top">
                                                            Login:</td>                                    
                                                        <td valign="top"><asp:TextBox runat="server" ID="txtLogin" CssClass="texto" MaxLength="10"></asp:TextBox></td>
                                                    </tr> 
                                                    <tr>                                                                                                                                                  
                                                        <td valign="top" style="padding-bottom: 20px;"><asp:CheckBox runat="server" ID="chbExterno" Text="Externo" AutoPostBack="true" /></td>
                                                    </tr> 
                                                    <asp:Panel runat="server" ID="pnlDatos" Visible="false">
                                                    <tr>
                                                        <td style="padding-bottom:20px;" valign="top">
                                                            Nombre:</td>                                    
                                                        <td valign="top"><asp:TextBox runat="server" ID="txtNombre" CssClass="texto" Width="500"></asp:TextBox></td>
                                                    </tr>   
                                                    <tr>
                                                        <td style="padding-bottom:20px;" valign="top">
                                                            DNI:</td>                                    
                                                        <td valign="top"><asp:TextBox runat="server" ID="txtDNI" CssClass="texto"></asp:TextBox></td>
                                                    </tr> 
                                                    <tr>
                                                        <td style="padding-bottom:20px;" valign="top">
                                                            Password:</td>                                    
                                                        <td valign="top"><asp:TextBox runat="server" ID="txtPassword" CssClass="texto"></asp:TextBox></td>
                                                    </tr> 
                                                    </asp:Panel>
                                                    <tr>
                                                        <td style="padding-bottom:20px;" valign="top">
                                                            Sistemas:</td>                                    
                                                        <td valign="top" style="padding-bottom: 30px"><asp:ListBox runat="server" ID="lbSistemas" SelectionMode="Multiple" Height="400"></asp:ListBox></td>
                                                    </tr>   
                                                                                                          
                                                </table>      
                                            </asp:Panel>      
                                            <!-- Fin Panel Editar Usuarios -->                                            
                                           
                                            <!-- Panel Botones -->  
                                            <asp:Panel ID="pnlBotones" runat=server Visible=false>                      
                                                <table width="100%" cellspacing=0 cellpadding=1 class="tablaEdicion" border="0" >                                                                                                                                                    
                                                    <tr>
                                                        <td align="right" style="border-top:1px solid #666666; height: 24px;">
                                                        <asp:Button ID="btnAceptar" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                                        <asp:Button ID="btnCancelar" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>                                                        
                                                        </td>
                                                    </tr>                     
                                                </table>      
                                            </asp:Panel>      
                                            <!-- Fin Panel Botones -->                                            
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
