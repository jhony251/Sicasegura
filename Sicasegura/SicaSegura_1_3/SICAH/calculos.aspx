<%@ Page Language="VB" AutoEventWireup="false" CodeFile="calculos.aspx.vb" Inherits="SICAH_calculos" %>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
     <link rel="stylesheet" href="..\styles.css">
     <link href="../styles.css" rel="stylesheet" />
     <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
     <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
    <script type="text/jscript" language="javascript">
    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación del registro?")==true)
        return true;
      else
        return false;
    }
    
   
    </script>   
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
</head>
<body bgcolor="#EEEAD2">
    <form id="form1" runat="server" method=post>
    <span id="dsp4"></span>
    <span id="imagepath" style="display:none">../js/calendar/images</span>
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
                    <td style="background-image:url(../images/dot2.gif);width:4px"></td>
                    <!-- Fin Celda Menú -->
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%" valign=top>
                        <!-- Panel listar Calculos -->                      
                        <asp:Panel ID="pnlCalculos" Runat=server Visible=true>
                          
                             <table width="100%">
                            <tr>
                                <td align="right" colspan="5"><a>Filtrar</a></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666" colspan="5">
                                <table align="right">
                                <tr>
                                    <td>
                                    <asp:TextBox ID="txtFiltroDescripcionCalculo" runat="server" Text="[Descripción Cálculo]" CssClass="texto"></asp:TextBox>
                                    </td>
                                    <td>
                                    <asp:Button ID="btnFiltroAceptar" runat="server" text="Filtrar" CssClass="boton" />
                                    <asp:Button ID="btnFiltroCancelar" runat="server" Text="Limpiar" CssClass="boton" />
                                    </td>
                                
                                </tr>
                                <tr>
                                    <td colspan="3" style="color:#555555; height: 4px;"><i>Utilice el caracter "%" como comodín</i></td>
                                </tr>
                                </table>
                                </td>
                            </tr>
                            
                            <tr><td class="tituloSec" colspan=2>MANTENIMIENTO DE CÁLCULOS</td></tr>
                                        <tr><td colspan="5" align="right"><asp:LinkButton runat="server" ID="lbtNuevo" Visible="<%#VisibleSegunPerfil() %>" OnClick="nuevoCalculo">Nuevo Cálculo</asp:LinkButton></a></td></tr>
                                        <tr><td class="encabListado">Descripción</td>
                                        <td class="encabListado">Formula</td>
                                        <td class="encabListado">Fecha Inicial</td>
                                        <td class="encabListado">Fecha Final</td>
                                        <td class="encabListado">&nbsp;</td>
                                        </tr>
                            <!--</table>
                            <table width="100%">                                               -->
                                <asp:Repeater ID="rptCalculos" runat="server">  
                                                                                
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><%#Container.DataItem("DesCalculo")%></td>
                                            <td><%#Container.DataItem("Formula")%></td>
                                            <td><%#Container.DataItem("FechaInicio")%></td>
                                            <td><%#Container.DataItem("FechaFin")%></td>
                                                                                      
                                            <td nowrap align=right width=36>
                                                <asp:LinkButton ID="lbtEdit" Runat=server CommandName='editar' Visible="<%#VisibleSegunPerfil() %>" CommandArgument='<%# container.dataitem("idcalculo")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtDelete" Runat=server CommandName='borrar' Visible="<%#VisibleSegunPerfil() %>" OnClientClick='return confirmar_borrado();' CommandArgument='<%# container.dataitem("idcalculo")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar"></asp:LinkButton>
                	                        </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8; height: 10px;" colspan=6>               
                                  <uc:paginacion ID="ucPaginacion" runat="server" />        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Contadores -->
                        
                        <!-- Panel Editar  Contadores-->
                        <asp:Panel ID="pnlEDCalculos" Runat="server" Visible="false">
                        <asp:label id="lblCalculoSel" runat="server" Visible="False"></asp:label>&nbsp;<br />
                            <!--PANEL DE CALCULOS-->
                            <asp:Panel ID="pnlDetalleCalculo" runat="server" Visible="True"
                                >
                                <table>
                                    <tr>
                                        <td align="left" class="tituloSec" colspan="4" valign="top" style="border-bottom: gray thin solid">
                                            <asp:Label ID="lblCalculoFormula" runat="server" Text="CÁLCULO"></asp:Label><br />
                                            Datos Generales</td>
                                        <td align="left" class="tituloSec" colspan="1" style="width: 2%;
                                            clip: rect(auto auto auto auto); height: 4px" valign="top">
                                        <asp:Button ID="btnCerrar" runat="server" CausesValidation="False" CssClass="boton"
                                            Text="x" Width="18px" ToolTip="Cerrar Edición Cálculo" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 11%; height: 21px;" valign="middle">
                                            Descripcion:</td>
                                        <td align="left" colspan="3" valign="bottom" style="width: 2580px">
                                            <asp:Label ID="lblNombreCalculo" runat="server" Width="250px" Height="17px"></asp:Label>
                                            <asp:TextBox ID="txtNombreCalculo" runat="server" CssClass="texto" Text="" Width="247px" Visible="False"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombreCalculo"
                                                ErrorMessage="Se requiere descripción" Width="120px"></asp:RequiredFieldValidator></td>
                                        <td align="left" colspan="1" style="padding-right: 9px; width: 2%; clip: rect(auto auto auto auto);
                                            height: 4px" valign="bottom">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 11%; height: 19px" valign="middle">
                                            Validez desde:</td>
                                        <td align="left" colspan="3" style="height: 19px; width: 2580px;" valign="bottom">
                                            <asp:Label ID="lblFechaValidez" runat="server" Height="17px" Width="108px"></asp:Label>
                                            <asp:TextBox ID="txtFechaValidez" runat="server" CssClass="texto" Text="" Visible="False"
                                                Width="116px"></asp:TextBox>
                                            <asp:Image ID="imgFecha" runat="server"
                                                    align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif" Style="cursor: pointer"
                                                    Visible="False" /></td>
                                        <td align="left" colspan="1" style="padding-right: 9px; width: 2%; clip: rect(auto auto auto auto);
                                            height: 4px" valign="bottom">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 11%; height: 19px" valign="middle">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; hasta:</td>
                                        <td align="left" colspan="3" style="height: 19px; width: 2580px;" valign="bottom">
                                            <asp:Label ID="lblFechaValidezHasta" runat="server" Height="17px" Width="48px"></asp:Label>
                                            <asp:TextBox ID="txtFechaValidezHasta" runat="server" CssClass="texto" Text="" Visible="False"
                                                Width="116px"></asp:TextBox>
                                            <asp:Image ID="imgFechaHasta" runat="server"
                                                    align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif" Style="cursor: pointer"
                                                    Visible="False" /></td>
                                        <td align="left" colspan="1" style="padding-right: 9px; width: 2%; clip: rect(auto auto auto auto);
                                            height: 4px" valign="bottom">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="4" style="height: 19px" valign="middle">
                                            &nbsp;<asp:ImageButton ID="imgEditarNombreCalculo" runat="server" ImageUrl="../SICAH/images/Proceso_OK.gif" Width="18px" Height="18px" ToolTip="Editar Descripción del Cálculo" /></td>
                                        <td align="right" colspan="1" style="padding-right: 9px; width: 2%; clip: rect(auto auto auto auto);
                                            height: 4px" valign="middle">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" style="border-top: gray thin solid"
                                            valign="bottom" rowspan="3">
                                            <asp:Panel ID="pnlOperandos" runat="server" Height="100%" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" class="tituloSec" colspan="4"
                                            valign="bottom">
                                            <asp:Button ID="btnAgregarCalculo" runat="server" CausesValidation="False" CssClass="boton"
                                                Text="+" ToolTip="Agregar Operando" Width="18px" />
                                                            Operandos del Cálculo</td>
                                                    </tr>
                                                    <tr>
                                        <td align="left" class="celdacontenido" colspan="4" style="height: 98px; border-bottom: gray thin solid;" valign="top">
                                            <!--Datos de Calculos del Sistema-->
                                            <asp:Repeater ID="rpt_calculoselementosmedida" runat="server">
                                                <HeaderTemplate>
                                                    <!--Encabezado del Repeater rpt_calculos-->
                                                    <table cellpadding="0" >
                                                        <tr>
                                                            <th align="left" class="preproduccion">
                                                                Operando (Elemento de Medida)
                                                            </th>
                                                            <th align="left" class="preproduccion">
                                                                Factor
                                                            </th>
                             
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <!--Fila del Repeater rpt_calculoselementosmedida-->
                                                    <tr <%# checkfila()%>>
                                                        <td>
                                                            <%#Container.DataItem("CodigoElementoMedida")%>
                                                        </td>
                                                        <td>
                                                            <%#Container.DataItem("Factor")%>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtEditar" runat="server" CausesValidation="false" CommandArgument='<%#""& Container.DataItem("IdCalculo")& "#" & Container.DataItem("CodigoPVYCR")& "#" & Container.DataItem("IdElementoMedida")%>'
                                                                CommandName='editar'><img src="../images/edit.gif" border="0" align="absmiddle" alt="Editar"></asp:LinkButton></td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtBorrar" OnClientClick='return confirmar_borrado();' runat="server" CausesValidation="false" CommandArgument='<%#""& Container.DataItem("IdCalculo")& "#" & Container.DataItem("CodigoPVYCR")& "#" & Container.DataItem("IdElementoMedida")%>'
                                                                CommandName='borrar'><img src="../images/del.gif" border="0" align="absmiddle" alt="Borrar"></asp:LinkButton></td>
                                                    </tr>
                                                    
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <!--Fin de Repeater rpt_calculos-->
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td align="left" class="tituloSec" colspan="1" style="padding-right: 9px; width: 2%;
                                            clip: rect(auto auto auto auto); height: 4px" valign="bottom">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="celdacontenido" colspan="1" style="padding-right: 9px; width: 2%;
                                            clip: rect(auto auto auto auto); height: 4px" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="subtitListado" colspan="1" style="padding-right: 9px; width: 2%;
                                            clip: rect(auto auto auto auto); height: 1px" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="subtitListado" colspan="4" valign="top" style="height: 147px">
                            <asp:Panel ID="pnlEdicionDetalleCalculo" runat="server" Width="100%" Wrap="False" Visible="False" BackColor="Info">
                            <table cellpadding="2" cellspacing="2" style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid; border-bottom: silver thin solid" width="100%">
                                <tr>
                                    <td style="width: 158px; height: 1px" align="right">
                                        Factor</td>
                                    <td colspan="3" style="width: 2815px; height: 1px;" align="left">
                                        <asp:TextBox ID="txtFactor" runat="server" Height="13px" Width="23px" CausesValidation="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFactor" runat="server" ControlToValidate="txtFactor"
                                            ErrorMessage="!" Width="3px"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFactor"
                                            ErrorMessage="Formato de Factor: Operador(+,-,*,/)+Valor Numérico" ValidationExpression="[+,\-,*,\/]\d+"></asp:RegularExpressionValidator></td>
                                    <td align="right" colspan="1" style="width: 466px; height: 1px">
                                        <asp:Button ID="btncerraredicion" runat="server" CausesValidation="False" CssClass="boton"
                                                Text="x" ToolTip="Cerrar Edicion Operando" Width="18px" /></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 158px;">
                                        Tipo
                                        Elemento de Medida</td>
                                    <td colspan="4" style="width: 466px;" align="left">
                                        <asp:DropDownList ID="ddlTipoElementoMedida" runat="server" AutoPostBack="True" Font-Size="8pt"
                                            Height="9px" Width="152px">
                                            <asp:ListItem Value="0">[Seleccionar]</asp:ListItem>
                                            <asp:ListItem Value="Q">Caudal&#237;metro</asp:ListItem>
                                            <asp:ListItem Value="H">Hor&#243;metro</asp:ListItem>
                                            <asp:ListItem Value="E">Energ&#237;a</asp:ListItem>
                                            <asp:ListItem Value="V">Volum&#233;trico</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTipoElementoMedida" runat="server" ControlToValidate="ddlTipoElementoMedida"
                                            ErrorMessage="!"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 158px" align="right">
                                        Elemento de Medida</td>
                                    <td colspan="4" style="width: 466px" align="left">
                                        <asp:DropDownList ID="ddlElementoMedida" runat="server" Font-Size="8pt" Height="9px" Width="402px" Enabled="False" CausesValidation="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvElementoMedida" runat="server" ControlToValidate="ddlElementoMedida"
                                            ErrorMessage="!" Width="1px"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="5" style="border-top: #666666 1px solid; height: 10px;">
                                        <asp:Button ID="btnAceptar" runat="server" CssClass="boton" Text="Aceptar" Width="50px" Visible="False" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                                        </td>
                                        <td align="right" class="subtitListado" colspan="1" style="padding-right: 9px; width: 2%;
                                            clip: rect(auto auto auto auto); height: 147px" valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
        
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Contadores -->
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
