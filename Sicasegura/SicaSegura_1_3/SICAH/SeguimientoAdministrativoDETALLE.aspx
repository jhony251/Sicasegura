<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SeguimientoAdministrativoDETALLE.aspx.vb" Inherits="SICAH_SeguimientoAdministrativoDETALLE
" %>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />    
    
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
    <!--<script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>-->
    <script type="text/jscript" language="javascript" src="../js/utiles.js"></script>
    <script type="text/jscript" language="javascript" src="../js/utilesInterfazDinamica.js"></script>
    <script type="text/jscript" language="javascript" src="..\js/utilesMenus.js"></script> 
    <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
    
    <script language="javascript" type="text/javascript">
 

// <!CDATA[


    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación del Seguimiento Administrativo?")==true)
        return true;
      else
        return false;
    }
    
     function desplegarArbol(sender)
    {
        var despArbol = document.getElementById('desplegableArbol');
        if (despArbol.style.display=='none'){
            ocultarDesplegables();
            var elementoOrigen = event.srcElement;
            
            if (elementoOrigen.tagName=='IMG'){
                despArbol.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft+elementoOrigen.parentElement.offsetLeft +80+ document.body.scrollLeft) + 'px';
                despArbol.style.top = '' + (event.clientY-event.offsetY-elementoOrigen.offsetTop+elementoOrigen.parentElement.offsetHeight-3 + document.body.scrollTop) + 'px';
            }
            else if (elementoOrigen.tagName=='TD'){
                despArbol.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft+228 + document.body.scrollLeft) + 'px';        
                despArbol.style.top = '' + (event.clientY-event.offsetY+elementoOrigen.offsetHeight-5 + document.body.scrollTop) + 'px';
            }
            ocultarSelects();
            despArbol.style.display='';
        }
        else{
            despArbol.style.display='none';
            mostrarSelects();
        }
        
        event.cancelBubble=true;
        //document.getElementById('lbldespegableArbol').value = 'Seleccione el Sistema:';
    }
    
    function ocultarDesplegables()
    {
        
        var i;
        document.getElementById('desplegableArbol').style.display='none';
        mostrarSelects(); 
        
    }
    
  
  //Oculta todos los Selects presentes en la página actual
function ocultarSelects(){
    var elementos = document.getElementById('Form1').elements;
    for (i=0;i<elementos.length;i++){
        if (elementos[i].type == 'checkbox'){
            elementos[i].style.visibility='hidden';
        }
    }
}

var remostrarselects = true;
//Muestra todos los Selects presentes en la página actual
function mostrarSelects(){
    if (remostrarselects){
        var elementos = document.getElementById('Form1').elements;
        for (i=0;i<elementos.length;i++){
            if (elementos[i].type == 'checkbox')
                elementos[i].style.visibility='visible';
        }
    }
}
   function GetTreeHandle()
   {
       var tree;
       var treeName = 'treeView1';
      
       // Obtiene el manejador del TreeView de Servidor
       tree = document.getElementById( treeName );
   
       if ( null == tree || undefined == tree )
           return null;
   
       return tree;
   }

   function GetSelectedNode()
   {
       var tree = GetTreeHandle();
       var treeNode;
   
       if ( null == tree || undefined == tree )
           return null;
   
       treeNode = tree.getTreeNode( tree.selectedNodeIndex );  
    
       if ( null == treeNode || undefined == treeNode )
           return null;
           
       alert(treeNode);
       return treeNode;
   }
  function AbrirGrafico(codigo){
    window.open("perfil_acequiaFlash.aspx?codigo=" + codigo,"grafico", "toolbar=no,menubar=no,top=200,left=250,height=500,width=650");
   // window.open("perfil_acequia.aspx?codigo=" + codigo ,"grafico", "toolbar=no,menubar=no,top=200,left=250,height=500,width=650");
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


</script>
   <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
</head>
<body  bgcolor="#EEEAD2">
    <form id="form1" runat="server">   
    <span id="dsp4"></span>
    <span id="imagepath" style="display:none">../js/calendar/images</span> 
    <div id="desplegableArbol" style="position:absolute;width:331px;padding:2px;background-color:white;border:1px solid #8C8B83;display:none; left: 830px; top: 1206px; height: 299px;">
            <table style="height: 100%" width="100%">
                <tr>
                    <td style="width:96%; height: 17px;">
                    </td>
                    <td style="width: 10px; height: 17px;">
                        <asp:Image ID="imgCerrarVentana" runat="server" align="absmiddle" ImageUrl="~/SICAH/images/cerrar.GIF"
                        Style="cursor: pointer" ToolTip="Seleccionar Cauce Padre" /></td>
                </tr>
                <tr>
                    <td style="height: 273px; width: 94%;">
                      <asp:Panel ID="pnlArbol" Runat="server" Visible="true" Width="96%" ScrollBars="Auto" BorderStyle="Inset" Height="100%" Wrap="False" >
                          <asp:TreeView ID="treeView1" runat="server" Width="171px">
                          <SelectedNodeStyle BackColor="PaleGoldenrod" Font-Bold="True" Font-Underline="True" />

                          </asp:TreeView>
                      </asp:Panel> 
                    </td>
                    <td style="width: 10%; height: 273px;">
                    </td>
                </tr>
            </table>
   </div>
       <table width="100%" cellspacing="0" align="center" style="border: 0px solid #666666;background-color: white; height: 707px;">
       <tr>
        <td style="width: 952px; height: 675px;">
           <table cellspacing="0" cellpadding="0" style="width: 81%">
             <!-- Cabecera -->
             <tr>
              <td style="width: 951px">
                 <table width="100%">
                     <tr>
                       <asp:Label ID="lblPestanyasArbol" runat="server"></asp:Label><td width="50%" visible="false" >
                           
                       </td>   

                 </tr>
                  
                  </table>
                  
              </td>
                              
             </tr>
           <tr valign="top">
              <td style="width:951px">
                <table align="left" width="80%" cellspacing="0" cellpadding="0" 
                      style="border: 0px solid #444444; height: 450px;">
                <!-- Celda Menú - Contenido -->
                <tr valign="top">
                <!-- Celda Menú -->
                   <!--<td colspan="5"valign="top" style="padding-top: 20px; width:20%">  -->
                                      <!--<%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%>-->
                   <!--</td>-->
                   <!-- <td style="background-image:url(../images/dot2.gif);width:3px"></td>-->
                   <!-- Fin Celda Menú -->
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:3px; padding-right:3px;" valign="top">
                        <!-- Panel listar Seguimientos -->                      
                        <asp:Panel ID="pnlSeguimientos" Runat="server" Visible="true" Width="100%">
                            <table style="height: 80px; width: 100%;">                                                                   
                                    <tr>
                                        <td align="left" class="tituloLecturas" colspan="6">
                                            <asp:Label ID="lblNodo" runat="server" visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="encabListado" colspan="5">
                                            Seguimientos Adminstrativos</td>
                                        <td align="right" class="encabListado"><asp:Button runat="server" ID="btnImprimir" cssclass="boton" Text="Imprimir" /></td>
                                    </tr>
                                <tr>
                                    <td align="left" colspan="1" style="width: 102px">
                                    </td>
                                    <td align="right" colspan="5">
                                    </td>
                                </tr>
                                    <tr><td  align="left" colspan="1" style="width: 102px"><a  id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a></td>
                                        <td colspan="5" align="right"><asp:LinkButton runat="server" ID="lbtNuevo">&nbsp;Nuevo Seguimiento</asp:LinkButton></td>
                                   </tr>
                                    <tr>                                        
                                    <td class="encabListado" style="width: 117px">
                                    <table cellspacing="0" width=100% height=100%>
                                    <tr>
                                    <td><asp:LinkButton ID= "lbtFecha" runat=server>Fecha</asp:LinkButton>
                                        <asp:ImageButton ID="imgBOrd" runat="server" ImageUrl="images/A_DOWN.gif" ImageAlign="Top" /></td></tr>
                                    </table>
                                    
                                    </td>  
                                    <td class="encabListado" style="width: 207px">Tipo Suceso</td>  
                                    <td class="encabListado" style="width: 200px">Comentarios</td>  
                                    <td class="encabListado" style="width: 95px">CodigoPVYCR</td>   
                                    <td class="encabListado" style="width: 98px">Responsable</td> 
                                    <td class="encabListado" style="width: 72px">&nbsp;</td>
                                    </tr> 
                                <tr id="FilaFiltro">
                                    <td class="encabListado" style="width: 117px" valign="middle">
                                        <table>
                                            <tr>
                                                <td style="width: 1px">
                                                    Desde:</td>
                                                <td style="width: 4px">
                                                    <asp:TextBox ID="txtFFechaInicial" runat="server" CssClass="texto" Style="width: 74px"></asp:TextBox>&nbsp;</td>
                                                <td>
                                                    <asp:Image ID="imgFFechaInicial" runat="server" align="absmiddle" ImageAlign="Left"
                                                        ImageUrl="~/images/calendario.gif" Style="cursor: pointer" /></td>
                                                <td>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFFechaInicial"
                                                        Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                                        Type="Date" Width="1px"></asp:CompareValidator></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 1px">
                                                    Hasta:</td>
                                                <td style="width: 4px">
                                                    <asp:TextBox ID="txtFFechaFinal" runat="server" CssClass="texto" Style="width: 74px"></asp:TextBox></td>
                                                <td>
                                                    <asp:Image ID="imgFFechaFinal" runat="server" align="absmiddle" ImageAlign="Left"
                                                        ImageUrl="~/images/calendario.gif" Style="cursor: pointer" /></td>
                                                <td>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtFFechaFinal"
                                                        Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                                        Type="Date" Width="1px"></asp:CompareValidator></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="encabListado" valign="middle" style="width: 207px">
                                             <asp:DropDownList ID="ddlFTipoSuceso" runat="server" CssClass="texto" Width="202px" Font-Size="8pt">
                                             </asp:DropDownList></td>
                                    <td class="encabListado" valign="middle" style="width: 255px">
                                        <asp:TextBox ID="txtFComentarios" runat="server" Columns="13" CssClass="texto" 
                                            Width="200px"></asp:TextBox></td>
                                    <td class="encabListado" valign="middle" style="width: 95px">
                                           <asp:TextBox ID="txtFCodigoPVYCR" runat="server" Columns="13" CssClass="texto"></asp:TextBox></td>
                                    <td class="encabListado" valign="middle" style="width: 98px">
                                        <asp:TextBox ID="txtFResponsable" runat="server" Columns="13" CssClass="texto"></asp:TextBox></td>
                                    <td class="encabListado" valign="middle" style="width: 72px">
                                           <asp:LinkButton id="lbtAceptarUsReg" Runat="server" onclick="AceptarFiltro" 
                                               CssClass="enlaceLecturas" Width="60px">Aceptar</asp:LinkButton></td>
                                </tr>
                                
                                <tr style="width:100%">
                                <td colspan="6" style="height: 86px">
                                <asp:Panel ID="pnlScrollGridSeguimientosAdministrativos" runat="server" ScrollBars="Vertical">
                                     
                                    <asp:Repeater ID="rptSeguimientosAdministrativos" runat="server">       
                                    <HeaderTemplate>
                                           <table>            
                                    </HeaderTemplate>                                              
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td width="165px"><%#DataBinder.Eval(Container.DataItem, "Fecha", "{0:dd/MM/yyyy}")%></td>
                                            <td width="207px"><%#Container.DataItem("Denominacion")%></td>
                                            <td width="232px"><%#Container.DataItem("Comentarios")%></td>
                                            <td width="96px"><%#Container.DataItem("CodigoPVYCR")%></td>
                                            <td width="98px"><%#Container.DataItem("Responsable")%></td>                                            
                                            <td width="30px" align="left"  >  
                                                <table cellspacing="2">
                                                    <tr>                                
                                                        <td><asp:LinkButton ID="lbtEdit" Visible="<%#VisibleSegunTipo() %>" Runat="server" CommandName='editar' CommandArgument='<%#""& container.dataitem("codigoCauce")& "#" & container.dataitem("TipoSuceso")& "#" & container.dataitem("Fecha")%>'><img src="../images/edit.gif" border="0px" align="left" alt="Editar datos"></asp:LinkButton></td>
                                                        <td><asp:LinkButton ID="lbtDelete" Visible="<%#VisibleSegunTipo() %>" Runat="server" CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("codigoCauce")& "#" & container.dataitem("TipoSuceso")& "#" & container.dataitem("Fecha") %>'><img src="../images/del.gif" border="0px" align="left" alt="Borrar"></asp:LinkButton></td>
                                                        <td><asp:LinkButton ID="lbtBoletin" Visible='<%#VisibleSegunvalor(container.dataitem("numref")) %>' Runat="server" CommandName='boletin'  CommandArgument='<%# container.dataitem("numref")%>'  ><img src="../images/Editar.gif" border="0px" align="left" alt="Boletín Guardería" ></asp:LinkButton></td>
                	                                </tr>
                	                            </table>   
                	                        </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                </asp:Panel>
                                </td></tr>
                                <tr style="width: 100%">
                                    <td colspan="6" style="height: 177px" valign="top">
                                        <asp:Panel ID="pnlEDVSeguimientos" runat="server" Visible="False" BorderColor="Navy" BackColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1px">
                                            <table style="width: 702px">
                                                <tr>
                                                    <td class="titulosec" colspan="6" valign="bottom">
                                                        <asp:Label ID="lblModoED" runat="server" Text="EDICIÓN DE SEGUIMIENTO" Width="245px" CssClass="tituloSec" EnableTheming="True" Font-Bold="True" Height="6px"></asp:Label></td>
                                                    <td align="right" class="titulosec" colspan="1" style="width: 60px" valign="top">
                                                        <asp:ImageButton ID="imgCerrarED" runat="server" ImageUrl="~/images/close2.gif" ToolTip="Cerrar" /></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="height: 108px">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtEDFecha" runat="server" CssClass="texto" Width="94px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Image ID="imgCalFechaInicio" runat="Server" align="absmiddle" 
                                                                        ImageUrl="../images/calendario.gif" Style="cursor: pointer" />
                                                                </td>
                                                                <td style="width: 117px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top" width="207" style="height: 108px">
                                                        <asp:DropDownList ID="ddlEDTipoSuceso" runat="server" CssClass="texto" Font-Size="8pt" Width="220px">
                                                        </asp:DropDownList></td>
                                                    <td valign="top" style="height: 108px">
                                                        <asp:TextBox ID="txtEDComentarios" runat="server"  CssClass="texto" Height="80px"
                                                            TextMode="MultiLine" Width="230px" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox></td>
                                                    <td valign="top" width="95" style="height: 108px">
                                                        <asp:TextBox ID="txtEDCodigoPVYCR" runat="server" Columns="13" CssClass="texto"></asp:TextBox></td>
                                                    <td valign="top" style="height: 90px">
                                                        <asp:TextBox ID="txtEDResponsable" runat="server" Columns="13" CssClass="texto"></asp:TextBox></td>
                                                    <td valign="top" style="width: 55px; height: 90px;">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgAgregarSeguimiento" runat="server" 
                                                                        ImageUrl="~/images/plus.gif" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblOperacion" runat="server" Text="Agregar"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 15px">
                                                                    &nbsp;</td>
                                                                <td style="height: 15px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                <asp:ImageButton ID="imgCancelar" runat="server" ImageUrl="~/images/close2.gif" Visible="False" />
                                                                </td>
                                                                <td>
                                                                   <asp:Label ID="lblCancelar" runat="server" Text="Cancelar" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 60px; height: 108px;" valign="top">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        
                                         <asp:Panel ID="pnlDetalleBoletin" runat="server" Visible="False" BorderColor="Navy" BackColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1px">
                                            <table style="width: 702px">
                                                <tr>
                                                    <td class="titulosec" colspan="6" valign="bottom" style="width: 858px; height: 12px;">
                                                        <asp:Label ID="lblNumRef" runat="server" Text="RESUMEN BOLETIN " Width="401px" CssClass="tituloSec" EnableTheming="True" Font-Bold="True" Height="6px"></asp:Label></td>
                                                    <td align="right" class="titulosec" colspan="1" style="width: 25px; height: 12px;" valign="top">
                                                        <asp:ImageButton ID="imgcerrarboletin" runat="server" ImageUrl="~/images/close2.gif" ToolTip="Cerrar" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="titulosec" colspan="6" style="width: 702px" valign="bottom">
                                                        <table id="Table3" cellpadding="2" cellspacing="2" onclick="return TABLE1_onclick()">
                                                            <tr>
                                                                <td colspan="6" style="border-bottom: silver thin inset">
                                                                    <strong>Hechos Descritos</strong></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtHechos" runat="server" CssClass="texto" Height="67px" TextMode="MultiLine"
                                                                        Width="777px"></asp:TextBox></td>
                                                                <td colspan="5" style="height: 25px" align="right" valign="bottom">
                                                                    <asp:LinkButton ID="lnbVerDetalleBoletin" runat="server" Width="63px">Ver Detalle</asp:LinkButton></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="right" class="titulosec" colspan="1" style="width: 25px" valign="top">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        
                                        <!-- Etiquetas de Valor Seleccionado en el Repeater de Seguimientos -->
                                        <asp:Label ID="lblCodigoCauceSel" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblFechaSel" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblTipoSucesoSel" runat="server" Visible="False" Height="6px"></asp:Label>
                                        <asp:Label ID="lblNumRefSel" runat="server" Height="1px" Visible="False"></asp:Label></td>
                                </tr>
                                       
                            </table>
                        </asp:Panel> 
                            
                        <!-- Fin Panel Editar Seguimientos -->
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
