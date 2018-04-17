<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CurvasAcequiasDETALLE.aspx.vb" Inherits="SICAH_CurvasAcequiasDETALLE" EnableEventValidation="false" %>
<%@Register TagPrefix="ofc" Namespace="OpenFlashChart" Assembly="OpenFlashChart" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  
  <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  
  <script type="text/jscript" language="javascript" src="../js/utiles.js"></script>
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script type="text/jscript" language="javascript" src="../js/utilesListados.js"></script>
  <script type="text/javascript" src="../aspnet_client/OpenFlashChart/js/swfobject.js"></script>
  <script language="javascript" type="text/javascript">
  
  function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación de la curva?")==true)
        return true;
      else
        return false;
    }
    
 
function TABLE1_onclick() {

}

  </script>
  
  <link href="../styles.css" rel="stylesheet" />
 
 
       
</head>
<body bgcolor="#EEEAD2">
  <form id="Form1" method="post" runat="server">
  <span id="dsp4"></span>
  <span id="imagepath" style="display:none">../js/calendar/images</span>
                        
    <table cellspacing="2" align="center" width="100%" style="border: 1px solid #666666;background-color: white;">
      <tr>
        <td style="padding-left:20px; padding-right:20px; width:100%" valign=top>

            <!-- Panel listar Regimenes de Curva asociados al elemento de medida -->
            <asp:Panel ID="pnlCurvasAcequias" runat="server" Visible="false">
                <table width="100%">
                 <tr><td class="tituloSec" colspan=10>
                     <asp:Label ID="Label1" runat="server" CssClass="tituloSec" Height="1px" Width="473px"><b> MANTENIMIENTO CURVAS DE ACEQUIAS </b></asp:Label></td></tr>
                 <tr><td colspan="10" align="right"><asp:LinkButton Visible=<%#VisibleSegunPerfil() %> runat="server" ID="lbtNuevo" OnClick="nuevaCurvaAcequia">&nbsp;Nueva Curva</asp:LinkButton></td></tr>
                 <tr><td>    
                    <asp:Repeater ID="rptCurvasAcequias" runat="server">
                        <HeaderTemplate>
                        <table width="100%" cellpadding="0">                       
                         <tr>
                            <td class="encabListado">
                                CodigoPVYCR
                            <td class="encabListado">
                                Id Elemento</td>
                            <td class="encabListado">
                                Régimen
                            <td class="encabListado">
                                Fecha Inicio</td>
                            <td class="encabListado">
                                Cód. Curva</td>
                            <td class="encabListado">
                                En Activo</td>
                            <td class="encabListado">
                                Fecha Fin Uso</td>
                            <td class="encabListado">
                                Comentarios</td>
                            <td class="encabListado">
                                Probabilidad</td>
                            <td class="encabListado">&nbsp;</td  
                                  
     
                         </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr <%# checkfila()%>="">
                                <td>
                                    <asp:Label id="lblCodigoPVYCR" runat="server" Text='<%#Container.DataItem("CodigoPVYCR")%>'></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label id="lblIdElementoMedida" runat="server" Text='<%#Container.DataItem("IdElementoMedida")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label id="lblRegimen" runat="server" Text='<%#Container.DataItem("Regimen")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label id="lblFechaInicioUso" runat="server" Text='<%#Container.DataItem("FECHA_INICIO_USO")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label id="lblCodCurva" runat="server" Text='<%#Container.DataItem("Cod_Curva")%>'></asp:Label>
                                </td>
                                <td>
                                  <asp:CheckBox ID="chkEnActivo" Enabled="false" runat="server" Checked='<%#Container.DataItem("En_Activo")%>' />
                                </td>
                                <td>
                                    <asp:Label id="lblFechaFinUso" runat="server" Text='<%#Container.DataItem("FECHA_FIN_USO")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label id="lblComentarios" runat="server" Text='<%#Container.DataItem("Comentarios")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label id="lblProbabilidad" runat="server" Text='<%#Container.DataItem("Probabilidad")%>'></asp:Label>
                                </td>
                                <td nowrap align=right width=36>
                                   <asp:LinkButton ID=lbtEdit Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='editar' CommandArgument='<%#"" & container.dataitem("codigoPVYCR")& "#" & container.dataitem("idelementoMedida") & "#" & container.dataitem("REGIMEN") & "#" & container.dataitem("FECHA_INICIO_USO")%>'><img src="../images/edit.gif" border=0 align=left alt="Editar datos"></asp:LinkButton>
                                   <asp:LinkButton ID=lbtDelete Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("codigoPVYCR")& "#" & container.dataitem("idelementoMedida") & "#" & container.dataitem("REGIMEN") & "#" & container.dataitem("FECHA_INICIO_USO")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar"></asp:LinkButton>
                	            </td
                               
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                        </table>
                        </FooterTemplate>
                    </asp:Repeater>
                 </td></tr>
                <tr><td><table cellpadding="0" cellspacing="0" width="100%"><tr><td>
                    &nbsp;<br />
                    <div id="ChartSeriesDeCurvasAcequiaN">
                     <script type="text/javascript">
                        swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "ChartSeriesDeCurvasAcequiaN", "437", "370",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico()%>"}  );
                    </script>
                    </div>
                </td>
                <!--IPIM 10/06/2009-->
                <td width="40%" valign="top"><br /><asp:Label ID="lblInformeCurvasAcequias" runat="server"></asp:Label></td>
                </tr></table></td></tr>
                </table>
            </asp:Panel>
            <!-- Fin Panel listar  Curvas de Acequiass -->
                        
                        <asp:Label ID="LBLinfoclave" runat="server" Visible="False" Width="0px"></asp:Label>
                        <asp:Label ID="LBLinfo" runat="server" Visible="False" Width="0px"></asp:Label>&nbsp;
            
            <!-- Panel Editar  Curvas de Acequias-->
                        
            <asp:Panel ID=pnlEDCurvasAcequias  Runat=server BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px">
                        <asp:label id=lblElementoSel runat=Server Visible=False></asp:label>
                        <asp:label id=lblcodigopvycrSel runat=Server Visible=False></asp:label>
                        <asp:label id=lblregimenSel runat=Server Visible=False></asp:label>
                        <asp:label id=lblFechaInicioUsoSel runat=Server Visible=False></asp:label>
                        <asp:Label ID="lblDetalle" runat="server" Visible="false" Text="No"></asp:Label>
                        
                            <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec Height="1px" Width="473px"><b> MANTENIMIENTO CURVA DE ACEQUIA: </b></asp:Label>
                            <table id="Table2" border="0" cellpadding="2" cellspacing="2" height="10">
                                <tr><td>&nbsp;</td></tr>
                                <tr style="color: #000000">
                                    <td nowrap="nowrap" style="width: 164px; text-align: right">
                                        Elemento de Medida</td>
                                    <td style="width: 40px; text-align: left">
                                            <asp:Label ID="lblDescripcionAcequia" runat="server" Width="302px" CssClass="texto" BorderWidth="0px"></asp:Label></td>
                                </tr>
                                <tr style="color: #000000">
                                    <td nowrap="nowrap" style="width: 164px; text-align: right">
                                        Régimen</td>
                                    <td style="text-align: left">
                                        <asp:DropDownList id="ddlregimen" runat="server" Width="218px" Font-Size="8pt">            
                                        </asp:DropDownList>&nbsp;<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Se requiere valor" Width="199px" ControlToValidate="ddlregimen"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="color: #000000">
                                    <td nowrap="nowrap" style="width: 164px; height: 1px; text-align: right">
                                        Fecha Inicio de Uso</td>
                                    <td style="width: 40px; height: 1px; text-align: left" colspan="" rowspan="">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 87px; height: 21px;">
                                        <asp:TextBox ID="txtFechaInicioUso" runat="server" CssClass="texto" Height="13px" Width="87px"></asp:TextBox></td>
                                                <td style="width: 20px; height: 21px">
                                                    <asp:Image
                                            ID="imgFechaInicioUso" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" /></td>
                                                <td style="width: 43px; height: 21px">
                                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Se requiere valor" Width="176px" ControlToValidate="txtFechaInicioUso"></asp:RequiredFieldValidator></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="color: #000000">
                                    <td nowrap="nowrap" style="width: 164px; height: 1px; text-align: right">
                                        Fecha Fin de Uso</td>
                                    <td rowspan="1" style="width: 40px; height: 1px; text-align: left">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 94px; height: 21px;">
                                        <asp:TextBox ID="txtFechaFinUso" runat="server" CssClass="texto" Height="13px" Width="87px"></asp:TextBox></td>
                                                <td style="width: 27px; height: 21px"><asp:Image
                                            ID="imgFechaFinUso" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 164px; height: 24px; text-align: right">
                                        En Activo</td>
                                    <td style="width: 40px; height: 24px; text-align: left">
                                        &nbsp;<asp:CheckBox ID="chkenActivo" runat="server" CssClass="texto" Style="left: -6px;
                                            position: relative; top: 0px" Width="96px" /></td>
                                </tr>
                                <tr>
                                    <td style="width: 164px; height: 1px; text-align: right">
                                        Comentarios</td>
                                    <td colspan="5" style="width: 7px; height: 1px; text-align: left"><table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2" style="height: 21px">
                                        <asp:TextBox ID="txtComentarios" runat="server" CssClass="texto" Height="13px" Width="564px"></asp:TextBox>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 164px; height: 1px; text-align: right">
                                        Probabilidad</td>
                                    <td colspan="5" style="width: 7px; height: 1px; text-align: left">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 63px; height: 21px;">
                                        <asp:TextBox ID="txtProbabilidad" runat="server" CssClass="texto" Height="13px" Width="60px"></asp:TextBox>
                                                </td>
                                                <td style="width: 27px; height: 21px">
                                                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtProbabilidad"
                                            ErrorMessage="Formato Incorrecto" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Width="232px"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 164px; text-align: right">
                                        Código de Curva</td>
                                    <td colspan="5" style="width: 7px; height: 1px; text-align: left">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 93px; height: 21px;">                                                
                                                <!--
                                        <asp:DropDownList ID=ddlCodigoCurva runat=server CssClass=combo Height="13px" Width="48px" Font-Size="8pt" AutoPostBack="True"></asp:DropDownList></td>                                        
                                                <td style="width: 27px; height: 21px">
                                                    </td>
                                                   -->
                                                   <asp:TextBox ID="txtCodigoCurva" runat="Server" CssClass="texto" Height="13px" Width="60px"></asp:TextBox></td>
                                                   <td style="width: 27px; height: 21px">&nbsp;
                                                   &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCodigoCurva"
                                            ErrorMessage="Formato Incorrecto" ValidationExpression="[0-9]*" Width="232px"></asp:RegularExpressionValidator></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" align="left" colspan="6">
                                        <asp:Panel ID="pnlEdicionCurva" runat="server" HorizontalAlign="Left" Width="700px">
                                            <table>
                                                <tr>
                                                    <td style="width: 136px; height: 2px" valign="top">
                                                    <table cellpadding="0" style="width: 173px" cellspacing="0">
                                                    <tr>
                                                    <td class="encabListado" width="20">
                                                        Nivel</td>
                                                    <td class="encabListado" width="20">
                                                        Caudal</td>
                                                    <td class="encabListado">&nbsp;</td>  
                                                    </tr>
                                                    </table>
                                                    </td>
                                                    <td rowspan="4" style="width: 515px" valign="top">
                                                        <div id="ChartCurvaAcequiaN" visible=true>
                                                        <script type="text/javascript">        
                                                            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "ChartCurvaAcequiaN", "500", "300",  "9.0.0", "expressInstall.swf",  {"data-file":"<%=getUrlGrafico2()%>"}  );
                                                        </script>
                                                        </div>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 184px; width: 150px;" valign="top">
                                        <asp:Panel ID="pnlCurvasAcequiasValores" runat="server" Height="220px"
                                            Width="172px" HorizontalAlign="Left" ScrollBars="Vertical">
                                            <asp:Repeater ID="rptCurvasAcequiasValores" runat="server">
                                                <HeaderTemplate>
                                                <table>
                                                 
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr <%# checkfila()%>="">
                                                        <td width="20">
                                                            <asp:Label id="lblNivel" runat="server" Text='<%#Container.DataItem("Nivel")%>' Width="35px"></asp:Label>
                                                        </td>
                                                        <td width="20">
                                                            <asp:Label ID="lblCaudal" runat="server" CssClass="texto" Text ='<%#Container.DataItem("Caudal")%>' Width="35px"></asp:Label>
                                                                                                                  
                                                        </td>
                                                        
                                                        <td nowrap align=right width=36>
                                                           <asp:LinkButton ID=lbtEdit Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='editar' CommandArgument='<%#"" & container.dataitem("Cod_Curva")& "#" & container.dataitem("Nivel")& "#" & Container.DataItem("Caudal")%>'><img src="../images/edit.gif" border=0 align=left alt="Editar datos"></asp:LinkButton>
                                                           <asp:LinkButton ID=lbtDelete Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("Cod_Curva")& "#" & container.dataitem("Nivel")& "#" & Container.DataItem("Caudal")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar"></asp:LinkButton>
                	                                    </td
                                                       
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </table>
                                                </FooterTemplate>
                                                                                       
                                            </asp:Repeater>
                                                                    
                                        </asp:Panel>
                                                       
                                                        <!-- Tabla de Edicion de Valores de Curva -->
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 136px; height: 9px" valign="top">
                                                        <asp:Panel ID="pnlEdicionValoresCurva" runat="server" Height="50px" Width="125px" Wrap="False">
                                                            <table cellpadding="0" cellspacing="0" style="width: 173px">
                                                                <tr>
                                                                    <td class="encabListado" style="width: 12px">
                                                                        <asp:TextBox ID="txtNuevoNivel" runat="server" CssClass="texto" Width="35px"></asp:TextBox></td>
                                                                    <td class="encabListado" style="width: 8px">
                                                                        <asp:TextBox ID="txtNuevoCaudal" runat="server" CssClass="texto" Width="35px"></asp:TextBox></td>
                                                                    <td class="encabListado" style="width: 213px" valign="middle">
                                                                        <asp:ImageButton ID="imgAgregarValoresCurva" runat="server" ImageUrl="~/images/plus.gif" />
                                                                        <asp:Label ID="lblOperacion" runat="server" Text="Agregar"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNuevoNivel"
                                                                            Display="Dynamic" ErrorMessage="Formato Incorrecto" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"
                                                                            Width="110px"></asp:RegularExpressionValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtNuevoCaudal"
                                                                            Display="Dynamic" ErrorMessage="Formato Incorrecto" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"
                                                                            Width="98px"></asp:RegularExpressionValidator></td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>                                            
                                        </asp:Panel>
                                    </td>
                                </tr>  
                                                           
                                <tr>
                                    <td align="right" colspan="7" style="border-top: #666666 1px solid; height: 24px">
                                        <asp:Button ID="btnAceptar" runat="server" CssClass="boton" Text="Aceptar" />
                                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" CssClass="boton"
                                            Text="Cancelar" />
                                    </td>
                                </tr>
                              
                            </table>
   
                        </asp:Panel>  
                                      
            <!-- Fin Panel Editar Curvas de Acequias -->
              
           </td>
           
      </tr>
    
      
     
     
    </table>
  </form>
</body>
</html>
