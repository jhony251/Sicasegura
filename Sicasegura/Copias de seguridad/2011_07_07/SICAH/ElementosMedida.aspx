<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElementosMedida.aspx.vb" Inherits="SICAH_ElementosMedida" %>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <script language="javascript" src="..\js/utilesMenus.js"></script>     
    <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script> 
    
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
    <!--<script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>-->
    <script type="text/jscript" language="javascript" src="../js/utiles.js"></script>
    <script type="text/jscript" language="javascript" src="../js/utilesInterfazDinamica.js"></script>
    
    <script language="javascript" type="text/javascript">
 

// <!CDATA[


    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación del Elemento de Medida?")==true)
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
    <form id="Form1" runat="server">  
    <span id="dsp4"></span>
     <span id="imagepath" style="display:none">../js/calendar/images</span>   
    <div id="desplegableArbol" style="position:absolute;width:331px;padding:2px;background-color:white;border:1px solid #8C8B83;display:none; left: 487px; top: 255px; height: 299px;">
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
                   <!--<td colspan="5"valign="top" style="padding-top: 20px; width:20%">  -->
                   <%=genHTMLMenus.generaMenuMtoSica(Session("idperfil"), Application("PVYCR_Arbol_Updated"))%>      
                   <!--<%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%>-->
                   <!--</td>-->
                   <!-- <td style="background-image:url(../images/dot2.gif);width:3px"></td>-->
                   <!-- Fin Celda Menú -->
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%" valign=top>
                        <!-- Panel listar Elementos -->                      
                        <asp:Panel ID="pnlElementos" Runat="server" Visible="true">
                            <table width=100%>
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
                                </td>
                                </tr>
                                </table>
                                <table align="right">
                                <tr>
                                <td colspan="6"  align="right">Registros a mostrar  <asp:TextBox  ID="txtRegistros"  runat="server" Text="10" Width="30px"  CssClass="texto"></asp:TextBox><asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtRegistros"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                        <asp:Button runat="server" ID="btnVertodos" cssclass="boton" Text="Actualizar" CausesValidation="false"  /></td>
                                </tr>

                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width="100%">                                                                   
                                    <tr><td class="tituloSec" colspan=4>MANTENIMIENTO DE ELEMENTOS DE MEDIDA</td></tr>
                                    <tr><td  align="left" colspan="1"><a  id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a></td>
                                        <td colspan="9" align="right"><asp:LinkButton runat="server" ID="lbtNuevo" Visible=<%#VisibleSegunPerfil()%> OnClick="nuevoElemento" CausesValidation="false">&nbsp;Nuevo Elemento</asp:LinkButton></td>
                                   </tr>
                                    <tr>                                        
                                    <td class="encabListado">Código PVYCR</td>
                                    <td class="encabListado">Id Elemento</td>  
                                    <td class="encabListado">Tipo</td>   
                                    <td class="encabListado">&nbsp;</td>
                                    </tr> 
                                   <tr id="FilaFiltro">         
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFCodigoPVYCR" CssClass="texto" Columns="13"></asp:TextBox></td>                                                  
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFidElemento" CssClass="texto" Columns="13"></asp:TextBox></td>   
                                         <td class="encabListado" style="width:150px; vertical-align: middle; text-align: left; height: 1px;">&nbsp;<asp:DropDownList ID="ddlFTipoElemento" runat="server"  Font-Size="8pt" Width="136px">
                                            <asp:ListItem Value="0">[Seleccionar]</asp:ListItem>
                                            <asp:ListItem Value="Q">Caudalímetro</asp:ListItem>
                                            <asp:ListItem Value="V">Volumétrico</asp:ListItem>
                                            <asp:ListItem Value="E">Energía</asp:ListItem>
                                            <asp:ListItem Value="H">Horómetro</asp:ListItem>
                                            <asp:ListItem Value="D">Diferencial</asp:ListItem>
                                         </asp:DropDownList></td>  
                                         <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsReg" Runat="server" onclick="AceptarFiltro" CssClass="enlaceLecturas" CausesValidation="false">Aceptar</asp:LinkButton></td> 
                                    </tr>                                       
                                    <asp:Repeater ID="rptElementos" runat="server">                                                     
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><%#Container.DataItem("CodigoPVYCR")%></td>
                                            <td><%#Container.DataItem("idElementoMedida")%></td>
                                            <td><%#Container.DataItem("Tipo")%></td>                                            
                                            <td  align="left"  >                                                
                                                <asp:LinkButton ID=lbtEdit Visible=<%# VisibleSegunPerfil() %> Runat=server CommandName='editar' CommandArgument='<%# container.dataitem("codigoPVYCR")& "#" & container.dataitem("idelementoMedida")%>' CausesValidation="false"><img src="../images/edit.gif" border=0 align="left" alt="Editar datos"></asp:LinkButton>
                                                <asp:LinkButton ID=lbtDelete Visible=<%# VisibleSegunPerfil() %> Runat=server CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("codigoPVYCR")& "#" & container.dataitem("idelementoMedida") %>' CausesValidation="false"><img src="../images/del.gif" border=0 align="left" alt="Borrar"></asp:LinkButton>
                	                            <asp:LinkButton ID=lbtPerfil Visible='<%#esAcequia(container.dataitem("Tipo")) %>' Runat=server CommandName='perfilAcequia'  CommandArgument='<%# container.dataitem("Tipo")%>'  ><img src="../images/iconos/icoPestNibus.gif" border=0 align="left" alt="Sección Acequia" onclick="AbrirGrafico('<%# container.dataitem("CodigoPVYCR")%>');return false;" ></asp:LinkButton>
                	                        </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=4>                                 
                                  <!--<uc:paginacion ID="ucPaginacion" runat="server" /> -->       
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar elementos -->
                        
                        <!-- Panel Editar  elementos-->
                        <asp:Panel ID=pnlEDElementos Runat=server Visible=false BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px">
                        <asp:label id=lblElementoSel runat=Server Visible=False></asp:label>
                        <asp:label id=lblcodigoSel runat=Server Visible=False></asp:label>
                            <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec Height="1px" Width="473px"><b> MANTENIMIENTO ELEMENTOS MEDIDA: </b></asp:Label>
                            <table width="100%" cellspacing=0 cellpadding=1 class="tablaEdicion">
                            <tr>
                            <td style="width: 525px; height: 111px">
                                <table cellspacing=2 cellpadding=2 id="TABLE1">
                                <tr>
                                    <td style="height: 25px">Código PVYCR</td>
                                    <td style="width: 200px; height: 25px">
                                        <asp:DropDownList ID=ddlcodigoPVYCR runat=server CssClass=texto  Width="149px" CausesValidation=false></asp:DropDownList>&nbsp;
                                        <asp:TextBox ID=txtIdArbol visible=false runat=server CssClass=texto Height="13px" Width="121px"></asp:TextBox>
                                    <asp:Image ID="imgArbol" runat="server" align="absmiddle"  
                                            ImageUrl="~/SICAH/images/iconos/icoBtnSistemas.GIF" Style="cursor: pointer" ToolTip="Seleccionar del Arbol el Punto" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlcodigoPVYCR"
                                            Display="Dynamic" ErrorMessage="*" Width="3px"></asp:RequiredFieldValidator></td>
                                    <td style="width: 8px">
                                        Id Elemento</td>
                                    <td style="height: 25px"><asp:TextBox ID=txtIdElemento runat=server CssClass=texto Width=25px></asp:TextBox></td>
                                    <td>Tipo </td>
                                    <td style="width: 71px; height: 25px;">
                                        <asp:DropDownList ID="ddltipo" runat="server">
                                            <asp:ListItem Value="Q">Caudal</asp:ListItem>
                                            <asp:ListItem Value="H">Horas</asp:ListItem>
                                            <asp:ListItem Value="E">Energ&#237;a</asp:ListItem>
                                            <asp:ListItem Value="V">Volumen</asp:ListItem>                                       
                                            <asp:ListItem Value="D">Diferencial</asp:ListItem>                                       
                                        </asp:DropDownList>

                                    </td>                                      
                                </tr>     
                                    <tr>
                                        <td colspan="7" style="height: 8px">
                                            <asp:Label ID="lblDesCodigoPVYCR" runat="server" Width="100%" BackColor="#E0E0E0"></asp:Label></td>
                                    </tr>
                                <tr>
                                
                                    <td colspan="7" align="right" style="border-top:1px solid #666666; height: 24px;">
                                    <asp:Button ID="btnAceptar" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                    <asp:Button ID="btnCancelar" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>
                                    </td>
                                </tr>
                     
                               </table>           
                            </td>
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Contadores -->
                        <!-- Panel calculo masivo caudal, solo administradores -->                      
                        <asp:Panel ID="pnlCaudalMasivo" Runat="server" Visible="false">
                           <table width=100% >
                            <tr>
                                <td class="tituloSec" colspan="6">CALCULO MASIVO DE CAUDALES</td>
                            </tr>
                            </table>
                            <table width=100% style ="background-color:DarkGray; border:solid 1px black">
                            <tr>
                                <td class="tituloSec" colspan="6"></td>
                            </tr>
                        <tr > 
                            <td colspan="2">Código PVYCR:                               
                             <asp:DropDownList ID=ddlPVYCR runat=server CssClass=texto  Width="149px" AutoPostBack="true"></asp:DropDownList></td>               
                            <td> Id Elemento Medida: 
                              <asp:DropDownList ID=ddlElemento runat=server CssClass=texto  Width="60px"></asp:DropDownList></td>
                            <td> Fecha Desde: 
                              <asp:TextBox ID="txtFechaIni" runat="server" CssClass="texto" Width="75px"></asp:TextBox>
                              <asp:CompareValidator ID="cvFiltroFechaIniI" runat="server" Text="*" ErrorMessage="Fecha no válida" ControlToValidate="txtFechaIni" Operator="DataTypeCheck" Type="date"></asp:CompareValidator>
                              <asp:Image ID="imgCalFechaIniI" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"                         
                              Style="cursor: pointer" />                                                                          
                              <asp:RequiredFieldValidator Text="*" ID="rfvFechaini" runat="server" ControlToValidate="txtFechaIni" ></asp:RequiredFieldValidator>  
                            </td>
                            <td> Hasta: 
                              <asp:TextBox ID="txtFechaFin" runat="server" CssClass="texto" Width="75px"></asp:TextBox>
                              <asp:CompareValidator ID="cvtxtFiltroFechaFinI" runat="server" Text="*" ErrorMessage="Fecha no válida" ControlToValidate="txtFechaFin" Operator=DataTypeCheck Type=date></asp:CompareValidator>                              
                              <asp:Image ID="imgCalFechaFinI" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                              Style="cursor: pointer" />                                      
                              <asp:RequiredFieldValidator runat="server" Text="*" ID="rfvFechafin" ControlToValidate="txtfechaFin" ></asp:RequiredFieldValidator>
                            </td>                                                                                                  
                            <td>
                            <asp:Button ID="btnCaudal" runat="server" text="Calcular Caudal" CssClass="boton"/></td>               
                        </tr>
                        <tr>
                        <td colspan="6">
                            <asp:RequiredFieldValidator runat="server" Text="Debe seleccionar un código PVYCR" ID="rfvCodigo" ControlToValidate="ddlPVYCR" ></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                            </table>                           
                        </asp:Panel>     
                        <!-- Fin calculo masivo caudal -->
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
