<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones.aspx.vb" Inherits="SICAH_Agrupaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
     <link rel="stylesheet" href="..\styles.css">
     <link href="../styles.css" rel="stylesheet" />
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
     <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" src="..\js/utilesMenus.js"></script>  
    <link href="../styles.css" rel="stylesheet" />
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}
    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación del Punto?")==true)
        return true;
      else
        return false;
    }

    function filtrar(){
    if (document.getElementById("FilaFiltro").style.display=="none"){
        document.getElementById("FilaFiltro").style.display="block" ;
        document.getElementById("MostrarFiltro").innerText= "Ocultar Filtro";
                
    }else{
        document.getElementById("FilaFiltro").style.display="none";
        document.getElementById("MostrarFiltro").innerText= "Mostrar Filtro";
        }
    }

   
// ]]>
</script>
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
                  <td valign="top" style="padding-top: 20px; width:20%; height: 621px;">
                    <%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%>
                    </td>   
                    <!-- línea vertical que separa el menú de los datos -->
                    <td style="background-image:url(../images/dot2.gif);width:3px"></td>
                    <!-- Fin Celda Menú -->                           
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%" valign=top>
                        <!-- Panel listar Agrupaciones -->                      
                        <asp:Panel ID=pnlAgru Runat=server Visible=true>
                            <table width=100%>
                            <tr>
                                <td align="right"></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666">
                                <table align="right">
                                 <tr>
                                <td colspan="6"  align="right">Registros a mostrar  <asp:TextBox  ID="txtRegistros"  runat="server" Text="10" Width="30px"  CssClass="texto"></asp:TextBox><asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtRegistros"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                        <asp:Button runat="server" ID="btnVertodos" cssclass="boton" Text="Actualizar"  /></td>
                                </tr>
 
                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width=100%>                                               
                                 
                                    <tr><td class="tituloSec" colspan=4>MANTENIMIENTO DE AGRUPACIONES</td></tr>
                                    <tr>
                                        <td  align="left" colspan="1"><a  id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a></td>
                                        <td colspan="5" align="right"><asp:LinkButton runat="server" ID="lbtNuevo" Visible="<%# VisibleSegunPerfil()%>" OnClick="nuevaAgrupacion" Text=" Nueva Agrupación"></asp:LinkButton></td>
                                    </tr>
                                    <tr><td class="encabListado">Descripción</td>
                                    <td class="encabListado">Fecha Inicio</td>
                                    <td class="encabListado">Fecha Fin</td>   
                                    <td class="encabListado">Color Calendario</td>   
                                    <td class="encabListado">&nbsp;</td>                       
                                    </tr>
                                    <tr id="FilaFiltro"  >         
                                         <td class="encabListado" style="width:400px"><asp:TextBox runat="server" ID="txtFDescripcion" CssClass="texto" Width="380px" ></asp:TextBox></td>                                                  
                                         <td class="encabListado" style="width:150px"><asp:TextBox runat="server" ID="txtFFechaIni" CssClass="texto" Width="90px" ></asp:TextBox>&nbsp;<asp:Image ID="imgFFechaIni" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" /><asp:CompareValidator ID="cvFFechaIni" runat="server" ControlToValidate="txtFFechaIni"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator></td>   
                                         <td class="encabListado" style="width:150px"><asp:TextBox runat="server" ID="txtFFechaFin" CssClass="texto" Width="90px" ></asp:TextBox>&nbsp;<asp:Image ID="ImgFFechafin" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" /><asp:CompareValidator ID="cvFFechaFin" runat="server" ControlToValidate="txtFFechaFin"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator></td>  
                                         <td class="encabListado" style="width:150px"><asp:TextBox runat="server" ID="txtFColor" CssClass="texto"></asp:TextBox></td>                                         
                                         <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsReg" onclick="AceptarFiltro" Runat="server" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                    </tr>   
                                    <asp:Repeater ID=rptAgru runat=server>                                                     
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><%#Container.DataItem("Descripcion")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "fechaInicio", "{0:dd/MM/yyyy}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "fechaFin", "{0:dd/MM/yyyy}")%></td>
                                            <td><%#Container.DataItem("ColorCalendario")%></td >                                                                                    
                                            <td nowrap align=right width=36>
                                                <asp:LinkButton ID=lbtEdit Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='editar' CommandArgument='<%# container.dataitem("idAgrupacion")%>'><img src="../images/edit.gif" border=0 align=left alt="Editar datos"></asp:LinkButton>
                                             </td>                       
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>                                          
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Agrupaciones -->
                        
                        <!-- Panel Editar  Agrupaciones-->
                        <asp:Panel ID=pnlEDAgrupaciones Runat=server Visible=false BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px">
                        <asp:label id=lblAgrupacionSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec  Width="368px" ForeColor="#003399">&nbsp;<b>MANTENIMIENTO AGRUPACIÓN: </b></asp:Label>
                            <table width="100%" cellspacing=0 cellpadding=1 class="tablaEdicion">                           
                                <tr>
                                    <td style="background-color: gainsboro;"><strong><span style="color: #003399">Descripción</span></strong></td>
                                    <td ><asp:TextBox ID=txtDescripcion runat=server CssClass=texto Height="13px"  MaxLength="255"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDescripcion"
                                            Display="Dynamic" ErrorMessage="La descripción es obligatoria" Width="136px"></asp:RequiredFieldValidator></td>                                
                                </tr>    
                                <tr><td>&nbsp</td></tr>                                         
                                <tr >
                                 <td >Fecha Inicio</td>
                                <td colspan="6" width="100%">
                                <table width="100%" cellspacing=0 cellpadding=1 class="tablaEdicion">
                                <tr>
                                    <td >
                                       <asp:TextBox ID=txtFechaIni CssClass=texto runat=server Width="100px"></asp:TextBox>&nbsp<asp:Image ID="imgCalFechaIni" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />
                                         <asp:CompareValidator ID=CompareValidator11 runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFechaIni Operator=DataTypeCheck Type=date></asp:CompareValidator>                                      
                                    </td>  
                                    <td  style="width:50px">Fecha Fin</td>
                                    <td ><asp:TextBox ID=txtFechaFin CssClass=texto runat=server Width="100px" Height="13px"></asp:TextBox>&nbsp<asp:Image ID="imgCalFechaFin" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif" Style="cursor: pointer" />     
                                         <asp:CompareValidator ID=CompareValidator5 runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFechaIni Operator=DataTypeCheck Type=date></asp:CompareValidator>                               
                                    </td>                                    
                                    <td style="width:80px">Color Calendario</td>
                                    <td align="right"><asp:TextBox ID=txtColor runat=server CssClass="textoNumerico" Height="13px" Width="150px">
                                    </asp:TextBox></td>                                  
                                    </tr>
                                    </table>
                                    </td>
                                 </tr>
                                <tr>
                                    <td   class="encabListado" colspan="5" style=" width: 360px; text-align: left; height: 15px; "></td>                                   
                                </tr>                          
                               <tr>                                
                                    <td colspan="5" align="right" style="border-top:1px solid #666666; height: 24px;">
                                    <asp:Button ID="btnAceptar" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                    <asp:Button ID="btnCancelar" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>
                                    <asp:Button ID="btnImprimir" Runat="server" cssclass="boton" Text='Imprimir'></asp:Button>
                                    </td>
                                </tr>                           
                               </table>           
                       
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Agrupaciones -->
                    </td>
                    <!-- Fin Celda Contenido -->        
                </tr>
                <!-- Fin Celda Menú - Contenido -->
                       
                </table></td></table> 
        
</td></tr></table>
  </form>

</body>
</html>
