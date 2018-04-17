<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SeguimientoAdministrativo.aspx.vb" Inherits="SICAH_SeguimientoAdministrativo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    
    <link rel="Stylesheet" href="../../includes/ext-3.3.1/resources/css/ext-all.css"/>
    <link href="../../styles.css" rel="stylesheet" />
    
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
    <script type="text/jscript" language="javascript" src="../../js/calendar/calendar.js"></script>
    <script type="text/jscript" language="javascript" src="../../js/utiles.js"></script>
    <script type="text/jscript" language="javascript" src="../../js/utilesInterfazDinamica.js"></script>
    
    <script type="text/javascript" language="javascript" src="../../includes/ext-3.3.1/adapter/ext/ext-base.js"></script>
    <script type="text/javascript" language="javascript" src="../../includes/ext-3.3.1/ext-all.js"></script>
    
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
                despArbol.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft+elementoOrigen.parentElement.offsetLeft + document.body.scrollLeft) + 'px';
                despArbol.style.top = '' + (event.clientY-event.offsetY-elementoOrigen.offsetTop+elementoOrigen.parentElement.offsetHeight + document.body.scrollTop) + 'px';
            }
            else if (elementoOrigen.tagName=='TD'){
                despArbol.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft+228 + document.body.scrollLeft) + 'px';        
                despArbol.style.top = '' + (event.clientY-event.offsetY+elementoOrigen.offsetHeight-5 + document.body.scrollTop) + 'px';
            }
            despArbol.style.display='';
        }
        else{
            despArbol.style.display='none';
            //mostrarSelects();
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
    function ventanaCargaFicheros(p) {
        var win;
        if (!win) {
            win = new Ext.Window({
                title: 'Subida de ficheros',
                applyTo: 'hello-win',
                layout: 'fit',
                width: 500,
                height: 350,
                closeAction: 'close',
                buttons: [{ text: 'Cerrar', handler: function() { win.close(); } }, { text: 'Volver', handler: function() { document.getElementById('framesubida').src = 'SubidaFichero/index.aspx?P=' + p; } }],
                keys: [{ key: 27, /* hide on Esc*/fn: function() { win.close(); } }],
                html: '<iframe id=framesubida src=SubidaFichero/index.aspx?P=' + p + ' style="width:485px;height:283px;"></iframe>'
                });
            }
            win.show(this); 
    }


</script>

</head>
<body  bgcolor="#EEEAD2">
    <form id="form1" runat="server">  
        <div id="desplegableArbol" style="position:absolute;width:331px;padding:2px;background-color:white;border:1px solid #8C8B83;display:none; left: 487px; top: 255px; height: 310px;">
        
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
    <span id="dsp4"></span>
    <span id="imagepath" style="display:none">../../js/calendar/images</span>
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
    <td valign="top" style="padding-top: 20px; width:20%; height: 621px;">
        <%=genHTMLMenus.generaMenuMtoSicaDesdeSICAH(Session("idperfil"))%>
    </td>
    <!-- línea vertical que separa el menú de los datos -->
    <td style="background-image:url(./images/dot2.gif);width:3px; height: 621px;"></td>
    <!-- Fin Celda Menú -->
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%; height: 621px;" valign=top>
                        <!-- Panel listar Seguimientos -->                      
                        <asp:Panel ID="pnlSeguimientos" Runat="server" Visible="true" Width="100%">
                        <table width="100%">
                            <tr>
                                <td align="right"></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666">
                                <table align="left">
                                 <tr>
                                    <td colspan="2" align="left"  >Nº Reg. filtrados:  <asp:TextBox  ID="txtNumRegFiltrados"  runat="server" Width="30px" Enabled=false  CssClass="texto"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="txtNumRegFiltrados"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                            <asp:Button runat="server" ID="btnListFiltro" cssclass="boton" Text="Listar Filtro" />
                                </td>
                                </tr>
                                </table>

                                <table align="right">
                                <tr>
                                <td colspan="6"  align="right">Registros a mostrar  <asp:TextBox  ID="txtRegistros"  runat="server" Text="10" Width="30px"  CssClass="texto"></asp:TextBox><asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtRegistros"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                        <asp:Button runat="server" ID="btnVertodos" cssclass="boton" Text="Actualizar"  /> &nbsp;&nbsp;&nbsp;
                                        <asp:Button runat="server" ID="btnImprimir" cssclass="boton" Text="Imprimir" />
                                        </td>
                                </tr>

                                </table>
                                </td>
                            </tr>
                            </table>
                            <table style="height: 112px; width: 100%;">                                                                   
                                <tr>
                                    <td class="tituloSec" colspan=7>MANTENIMIENTO DE SEGUIMIENTOS ADMINISTRATIVOS</td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="1" style="width: 102px">
                                    </td>
                                    <td align="right" colspan="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td  align="left" colspan="1" style="width: 102px">
                                        <a  id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a>
                                    </td>
                                    <td colspan="6" align="right">
                                            <asp:LinkButton runat="server" ID="lbtNuevo" visible="<%#visibleSegunPerfil()%>">&nbsp;Nuevo Seguimiento</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>                                        
                                    <td class="encabListado" style="width: 117px">
                                        <table cellspacing="0" width=100% height=100%>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID= "lbtFecha" runat=server>Fecha</asp:LinkButton>
                                                    <asp:ImageButton ID="imgBOrd" runat="server" ImageUrl="images/A_DOWN.gif" ImageAlign="Top" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>  
                                    <td class="encabListado" style="width: 207px">Tipo Suceso</td>  
                                    <td class="encabListado" style="width: 255px">Comentarios</td>  
                                    <td class="encabListado" style="width: 95px">Código Cauce</td>   
                                    <td class="encabListado" style="width: 95px">Código PVYCR</td>
                                    <td class="encabListado" style="width: 98px">Responsable</td> 
                                    <td class="encabListado" style="width: 62px">&nbsp;</td>
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
                                            Width="184px"></asp:TextBox></td>
                                    <td class="encabListado" valign="middle" style="width: 95px">
                                        <asp:TextBox ID="txtFCodigoCauce" runat="server"  CssClass="texto" Width="90px"></asp:TextBox></td>
                                    <td class="encabListado" style="width: 95px" valign="middle">
                                        <asp:TextBox ID="txtFCodigoPVYCR" runat="server"  CssClass="texto"
                                            Width="90px"></asp:TextBox></td>
                                    <td class="encabListado" valign="middle" style="width: 98px">
                                        <asp:TextBox ID="txtFResponsable" runat="server" Columns="13" CssClass="texto" Width="75px"></asp:TextBox></td>
                                    <td class="encabListado" valign="middle" style="width: 62px">
                                           <asp:LinkButton id="lbtAceptarUsReg" Runat="server" onclick="AceptarFiltro" 
                                               CssClass="enlaceLecturas" Width="60px">Aceptar</asp:LinkButton></td>
                                </tr>
                                
                                <tr style="width:100%">
                                <td colspan="7">
                                <asp:Panel ID="pnlScrollGridSeguimientosAdministrativos" runat="server" 
                                        Height="100%" ScrollBars="Vertical" Width="100%">
                                     
                                    <asp:Repeater ID="rptSeguimientosAdministrativos" runat="server">       
                                    <HeaderTemplate>
                                           <table>            
                                    </HeaderTemplate>                                              
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td width="175px"><%#DataBinder.Eval(Container.DataItem, "Fecha", "{0:dd/MM/yyyy}")%></td>
                                            <td width="215px"><%#Container.DataItem("Denominacion")%></td>
                                            <td width="210px"><%#Container.DataItem("Comentarios")%></td>
                                            <td width="105px"><%#Container.DataItem("CodigoCauce")%></td>
                                            <td width="105px"><%#Container.DataItem("CodigoPVYCR")%></td>
                                            <td width="90px"><%#Container.DataItem("Responsable")%></td>                                            
                                            <td width="50px" align="left"  > 
                                            <table cellspacing="2">        
                                                <tr>                                      
                                                    <td><asp:LinkButton ID="lbtEdit" Visible="<%#VisibleSegunTipo() %>" Runat="server" CommandName='editar' CommandArgument='<%#""& container.dataitem("codigoCauce")& "#" & container.dataitem("TipoSuceso")& "#" & container.dataitem("Fecha")%>'><img src="./images/edit.gif" border="0px" align="left" alt="Editar datos"></asp:LinkButton>   </td>
                                                    <td><asp:LinkButton ID="lbtDelete" Visible="<%#VisibleSegunTipo() %>" Runat="server" CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("codigoCauce")& "#" & container.dataitem("TipoSuceso")& "#" & container.dataitem("Fecha") %>'><img src="./images/del.gif" border="0px" align="left" alt="Borrar"></asp:LinkButton></td>
                                                    <td><asp:LinkButton ID="lbtAddFiles" Visible="<%#VisibleSegunTipo() %>" Runat="server"  CommandName='AddFiles' OnClientClick='ventanaCargaFicheros();' CommandArgument='<%#"" & container.dataitem("codigoCauce")& "#" & container.dataitem("TipoSuceso")& "#" & container.dataitem("Fecha") %>' CausesValidation="False"><img src="./images/Folder<%#hayficheros(container.dataitem("codigoCauce")& container.dataitem("TipoSuceso") & container.dataitem("Fecha")) %>.gif" border="0px" align="left" alt="Consultar Documentación"></asp:LinkButton></td>
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
                                       
                            </table>
                                        <asp:Panel ID="pnlEDVSeguimientos" runat="server" Visible="False" BorderColor="Navy" BackColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1px">
                                            <table style="width: 702px" cellpadding="3">
                                                <tr>
                                                    <td class="titulosec" colspan="6" valign="bottom">
                                                        <asp:Label ID="lblModoED" runat="server" Text="EDICIÓN DE SEGUIMIENTO" Width="245px" CssClass="tituloSec" EnableTheming="True" Font-Bold="True" Height="6px"></asp:Label></td>
                                                    <td align="right" class="titulosec" colspan="1" valign="top">
                                                        <asp:ImageButton ID="imgCerrarED" runat="server" ImageUrl="./images/close2.gif" ToolTip="Cerrar" /></td>
                                                </tr>
                                                <tr>
                                                    <td rowspan="2" valign="top" style="height: 124px">
                                                        &nbsp;Fecha
                                                        <table cellpadding="3">
                                                            <tr>
                                                                <td style="height: 4px">
                                                                    <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtEDFecha" runat="server" CssClass="texto" Width="94px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Image ID="imgCalFechaInicio" runat="Server" align="absmiddle" 
                                                                        ImageUrl="./images/calendario.gif" Style="cursor: pointer" />
                                                                </td>
                                                                <td width="117px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td rowspan="2" valign="top" style="width: 207px; height: 124px">
                                                        &nbsp;Tipo Suceso<br />
                                                        <table>
                                                            <tr>
                                                                <td>
                                                        <asp:DropDownList ID="ddlEDTipoSuceso" runat="server" CssClass="texto" Font-Size="8pt" Width="213px" ToolTip="Tipo Suceso">
                                                        </asp:DropDownList></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td rowspan="2" valign="top" style="height: 124px">
                                                        &nbsp;Comentarios&nbsp;<table cellpadding="3">
                                                            <tr>
                                                                <td style="height: 115px">
                                                        <asp:TextBox ID="txtEDComentarios" runat="server"  CssClass="texto" Height="104px"
                                                            TextMode="MultiLine" Width="214px" Font-Names="Verdana" Font-Size="8pt" ToolTip="Comentarios"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top" style="width: 100px; height: 124px;" colspan="2" rowspan="2">
                                                        <table>
                                                            <tr>
                                                                <td style="width: 186px">
                                                                    Código Cauce</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 186px">
                                                                    <asp:DropDownList ID="ddlEDCodigoCauce" runat="server" CssClass="texto" 
                                                        Font-Size="8pt" Width="162px" ToolTip="Código de Cauce">
                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 186px">
                                                                    Codigo PVYCR</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 186px">
                                                                    <asp:DropDownList ID="ddlEDCodigoPVYCR" runat="server" CssClass="texto" 
                                                        Font-Size="8pt" Width="215px" ToolTip="Código PVYCR">
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 186px">
                                                                    Responsable</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 186px; height: 10px;">
                                                        <asp:TextBox ID="txtEDResponsable" runat="server" Columns="13" CssClass="texto" ToolTip="Responsable" Width="203px"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td rowspan="2" style="width: 55px; height: 124px;" valign="top">
                                                        <table style="width:100%;">
                                                            <tr>
                                                                <td style="width: 26px">
                                                                    <asp:ImageButton ID="imgAgregarSeguimiento" runat="server" 
                                                                        ImageUrl="./images/plus.gif" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblOperacion" runat="server" Text="Agregar"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 2px; width: 26px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 2px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 26px">
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 1px; width: 26px;">
                                                                    &nbsp;</td>
                                                                <td style="height: 1px">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 26px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 26px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 26px">
                                                                <asp:ImageButton ID="imgCancelar" runat="server" ImageUrl="./images/close2.gif" Visible="False" />
                                                                </td>
                                                                <td>
                                                                   <asp:Label ID="lblCancelar" runat="server" Text="Cancelar" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="height: 124px;" valign="top" rowspan="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                        </asp:Panel> &nbsp;
                                        
                                        <!-- Etiquetas de Valor Seleccionado en el Repeater de Seguimientos -->
                                        <asp:Label ID="lblCodigoCauceSel" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblFechaSel" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblTipoSucesoSel" runat="server" Visible="False" Height="6px"></asp:Label>
                                        <asp:Label ID="lblNumRefSel" runat="server" Height="1px" Visible="False"></asp:Label><!-- Fin Panel Editar Seguimientos --></td>
                    <!-- Fin Celda Contenido -->        
                </tr>
                <!-- Fin Celda Menú - Contenido -->
                   

        
</table></td></tr>
          </table> 
        
</td></tr></table>

<div id="hello-win" class="x-hidden">
    
</div>


    </form>
</body>
</html>
