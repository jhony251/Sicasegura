<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sistemas.aspx.vb" Inherits="SICAH_Sistemas" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
      <!-- Estilos tanto para el árbol como para agrupaciones -->
      <link href="../ext-all.css" rel="stylesheet" type="text/css" />  
      
      <!-- referencia librerías para el arbol -->
      <script type="text/javascript" language="javascript" src="../js/ext-base-debug-w-comments.js"></script>
      <script type="text/javascript" language="javascript" src="../js/ext-all-debug.js"></script>      
      <script type="text/javascript" language="javascript" src="../js/utilesObjetoArbol.js"></script> 
      <link href="../EstilosArbol.css" rel="stylesheet" type="text/css" />              
      
      
      <!-- referencias para la agrupaciones -->
      <script src="../js/Agrupaciones.js" type="text/javascript"></script> 
      <script src="../js/RowEditor.js" type="text/javascript"></script>    
      
      <script src="../js/DragDrop.js" type="text/javascript"></script>   
      
      <link href="../styles.css" rel="stylesheet" type="text/css" />       
             
    <!-- Estilos de página -->
    <link href="../estilosAgrupaciones.css" rel="stylesheet" type="text/css" id="theme" /> 
    
    <script language="javascript" type="text/javascript">
        function confirmar_borrado() {
            if (confirm("¿Está seguro de la eliminación del Sistema?") == true)
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
                                            <!-- Panel listar Agrupaciones -->                                                                                                              
                                            <asp:Panel ID=pnlCauces Runat=server Visible=true>
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
                                                    <tr><td class="tituloSec" colspan=2>MANTENIMIENTO DE AGRUPACIONES</td></tr>
                                                    <tr>
                                                        <td  align="left" colspan="2"><a id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a></td>
                                                        <td align="right" width="90"><asp:LinkButton Visible=<%#VisibleSegunPerfil() %> runat="server" ID="lbtNuevo" OnClick="nuevoCauce">&nbsp;Nueva Agrupación</asp:LinkButton></td>
                                                    </tr>
                                                    <tr>                                                        
                                                        <td class="encabListado">Descripcion</td>                                        
                                                        <td class="encabListado">Público</td>                                        
                                                        <td class="encabListado">&nbsp;</td>                             
                                                    </tr>
                                                    <tr id="FilaFiltro"  >         
                                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFDescripcion" CssClass="texto" Columns="20"></asp:TextBox></td>                                                                                       
                                                         <td class="encabListado">
                                                         <asp:DropDownList runat="server" ID="ddlFPublico" CssClass="texto">
                                                            <asp:ListItem Selected Text="Todos" Value="-1" ></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0" ></asp:ListItem>
                                                            <asp:ListItem  text="Si" Value="1" ></asp:ListItem>
                                                         </asp:DropDownList>                                                         
                                                         <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsReg"  Runat="server" OnClick="AceptarFiltro" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                                    </tr>   
                                                    <asp:Repeater ID=rptCauces runat=server>                                                     
                                                        <ItemTemplate>
                                                        <tr <%# checkFila()%>>                                            
                                                            <td><%#Container.DataItem("Descripcion")%></td>
                                                            <td><input type="checkbox" <%#Chequeado(Container.DataItem("Publico"))%> disabled> </td>
                                                            <td align="right">
                                                                <asp:LinkButton ID=lbtEdit Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='editar' CommandArgument='<%# container.dataitem("id")%>'><img src="../images/edit.gif" border=0 alt="Editar datos"></asp:LinkButton>
                                                                <asp:LinkButton ID=lbtDelete Visible=<%# VisibleSegunPerfil() %> Runat=server CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("id") %>' CausesValidation="false"><img src="../images/del.gif" border=0 alt="Borrar"></asp:LinkButton>
                            	                            </td>
            	                                        </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <!-- Número de páginas -->
                                                    <tr><td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=2></td></tr>
                                                    <!-- Fin Número de páginas -->                  
                                                </table>
                                            </asp:Panel> 
                                            <!-- Fin Panel listar Agrupaciones -->
                                        
                                            <!-- Panel Editar Agrupaciones - Descripción -->  
                                            <asp:Panel ID="pnlEDCauces" runat=server Visible=false>                      
                                                <asp:label id=lblIdSel runat=Server Visible=False></asp:label><br/>
                                                <asp:Label ID=lblTitulo runat=server CssClass=tituloSec   ForeColor="#666699" Width="232px">&nbsp;<b>MANTENIMIENTO SISTEMA: </b></asp:Label>
                                                <table width="100%" cellspacing=0 cellpadding=1 class="tablaEdicion" border="0" >
                                                    <tr>
                                                        <td style="width: 120px;" valign="top">Agrupación del RAACS:</td>                                                        
                                                        <td><asp:RadioButtonList runat="server" ID="rbEsRAAC" AutoPostBack="true" OnSelectedIndexChanged="FireOnSelectedIndexChangd">
                                                            <asp:ListItem Text="Si" Value="S"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator valign="top" ID="RequiredFieldValidator1" runat="server" ControlToValidate="rbEsRAAC" ErrorMessage="Es necesario marcar si/no"></asp:RequiredFieldValidator>
                                                        </td>                                                        
                                                    </tr>
                                                    <asp:Panel ID="pnlIDRaac" Visible="false" runat="server">
                                                        <tr>
                                                            <td>IDRAAC</td>
                                                            <td>
                                                                <asp:TextBox ID="txtNumInscripcion" runat="server" CssClass="texto"></asp:TextBox>                                                                
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNumInscripcion" ErrorMessage="El IDRAAC es obligatorio"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtNumInscripcion" runat="server" ValidationExpression="([0-9])+" ErrorMessage="El IDRAAC tiene que estar formado sólo por números"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>   
                                                    <asp:Panel ID="pnlNombreAgrup" Visible="false" runat="server">
                                                        <tr>
                                                            <td>Nombre Agrupación</td>
                                                            <td>                                                                
                                                                <asp:TextBox ID="txtNombreAgrup" runat="server" CssClass="texto"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNombreAgrup" ErrorMessage="El nombre de la agrupación es obligatorio"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtNombreAgrup" runat="server" ValidationExpression=".*[a-zA-Z]+.*" ErrorMessage="El nombre de la agrupación tiene que estar formado por letras"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>                                                    
                                                    <tr>
                                                        <td style="padding-bottom:20px;" valign="top">
                                                            Descripcion:</td>                                    
                                                        <td valign="top"><asp:TextBox runat="server" ID="txtDescripcion" CssClass="texto" Width="500"></asp:TextBox></td>                                              
                                                    </tr>   
                                                    <tr>                                                                                                                                                  
                                                        <td valign="top" style="padding-bottom: 20px;"><asp:CheckBox runat="server" ID="chbPublico" CssClass="texto"  Text="Público" Checked /></td>
                                                    </tr> 
                                                    <tr>
                                                        <td colspan="2">Puntos del Sistema:</td>
                                                    </tr>                                                      
                                                </table>      
                                            </asp:Panel>      
                                            <!-- Fin Panel Editar Agrupaciones - Descripción -->
                                            
                                            <!-- Editar Agrupaciones - Puntos -->                                                        
                                                <table width="80%" cellspacing=0 cellpadding=1 class="tablaEdicion" border="0" >                                                                                                        
                                                    <tr>
                                                        <td valign="top" style="padding-top: 20px;">                                                                                         
                                                        <!--<iframe name="iframeBuscar" runat="server" id="iframeBuscar" style="width:300px; height:710px;"  frameborder="0" marginheight="0" marginwidth="0" scrolling="no" class="HTMframe" ></iframe>                                                                               -->
                                                       
                                                        <div  id="ContenedorArbol" style="display:none"></div>
                                                        </td>
                                                        <td valign="top" style="padding-top: 20px;"><div id="grid_agrupaciones" style="display:none"></div></td>
                                                    </tr>
                                                </table>                                                 
                                            <!-- Fin Editar Agrupaciones - Descripción -->
                                            
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
