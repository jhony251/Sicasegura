<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Cauces.aspx.vb" Inherits="SICAH_cauces" %>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
     <link rel="stylesheet" href="..\styles.css">
     <link href="../styles.css" rel="stylesheet" />
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
     <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
     <!--<script language="javascript" src="../js/utilesInterfazDinamica.js"></script>-->
    <script language="javascript" src="..\js/utilesMenus.js"></script>  

    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />

    
<script language="javascript" type="text/javascript">
// <!CDATA[

    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación del Cauce?")==true)
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
                despArbol.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft+elementoOrigen.parentElement.offsetLeft -175+ document.body.scrollLeft) + 'px';
                despArbol.style.top = '' + (event.clientY-event.offsetY-elementoOrigen.offsetTop+elementoOrigen.parentElement.offsetHeight + document.body.scrollTop) + 'px';
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
           
       return treeNode;
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
    <form id="Form1" runat="server" method=post>
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
                   <!--<td colspan="5"valign="top" style="padding-top: 20px;width:20%">  -->       
                   <%=genHTMLMenus.generaMenuMtoSica(Session("idperfil"), Application("PVYCR_Arbol_Updated"))%>
                   <!-- </td> -->
                   <!--<td style="background-image:url(../images/dot2.gif);width:6px"></td>-->
                   <!-- Fin Celda Menú -->
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%" valign=top>
                        <!-- Panel listar Contadores -->                      
                        <asp:Panel ID=pnlCauces Runat=server Visible=true>
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
                                        <asp:Button runat="server" ID="btnVertodos" cssclass="boton" Text="Actualizar"  /></td>
                                </tr>
                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width=100%>                                               
                                 

                                    <tr><td class="tituloSec" colspan=4>MANTENIMIENTO DE CAUCES</td>
                                    </tr>
                                    <tr>
                                        <td  align="left" colspan="1"><a  id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a></td>
                                        <td colspan="7" align="right"><asp:LinkButton Visible=<%#VisibleSegunPerfil() %> runat="server" ID="lbtNuevo" OnClick="nuevoCauce">&nbsp;Nuevo Cauce</asp:LinkButton></a></td></tr>
                                    <tr><td class="encabListado">Código Cauce</td>
                                        <!--<td class="encabListado">Tipo Cauce</td>-->
                                        <td class="encabListado">Denominación</td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width=310px ID="ddlFiltrado" runat="server" AutoPostBack="false" ></asp:DropDownList></td>
                                        <!--<td class="encabListado" >Paraje Toma</td>  -->
                                        <!--<td class="encabListado" >Municipio Toma</td>-->
                                        <!--<td class="encabListado" >Provincia Toma</td>  -->
                                        <td class="encabListado">&nbsp;</td>                             
                                    </tr>
                                    <tr id="FilaFiltro"  >         
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFCodigoCauce" CssClass="texto" Columns="20"></asp:TextBox></td>                                                  
                                   <%--      <!--<td class="encabListado" >-->
                                           <!--<asp:DropDownList  Style=font: 10px Verdana; text-align: right  Width=50px ID="ddlTipoCauce" runat="server" AutoPostBack="false" >-->
                                            <!--</asp:DropDownList>-->
                                         <!--</td>-->--%>
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFDenominacionCauce" CssClass="texto" Columns="34"></asp:TextBox></td>  
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFiltrado" CssClass="texto" Columns="50"></asp:TextBox></td>  
                                         <!--<td class="encabListado" ><asp:TextBox runat="server" ID="txtFParaje" CssClass="texto" Columns="25"></asp:TextBox></td>  -->
                                         <!--<td class="encabListado" ><asp:TextBox runat="server" ID="txtFMunicipio" CssClass="texto" Columns="20"></asp:TextBox></td>  -->
                                         <!--<td class="encabListado" ><asp:TextBox runat="server" ID="txtFProvincia" CssClass="texto" Columns="10"></asp:TextBox></td>  -->
                                         <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsReg"  Runat="server" OnClick="AceptarFiltro" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                    </tr>   
                                    <asp:Repeater ID=rptCauces runat=server>                                                     
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><%#Container.DataItem("CodigoCauce")%></td>
                                            <%--<td><%#Container.DataItem("TipoCauce")%></td>--%>
                                            <td><%#Container.DataItem("DenominacionCauce")%></td>
                                            <td><%#ObtenerFiltrado(Container.DataItem)%></td>
                                    <%--        <td><%#Container.DataItem("ParajeToma")%></td >  
                                            <td><%#Container.DataItem("MunicipioToma")%></td > 
                                            <td><%#Container.DataItem("ProvinciaToma")%></td >       --%>                                                                            
                                            <td nowrap align=right width=36>
                                                <asp:LinkButton ID=lbtEdit Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='editar' CommandArgument='<%# container.dataitem("codigoCauce")%>'><img src="../images/edit.gif" border=0 align=left alt="Editar datos"></asp:LinkButton>
                                                <!--<asp:LinkButton ID=lbtDelete Runat=server CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%# container.dataitem("CodigoCauce") %>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar"></asp:LinkButton>-->
                	                        </td
                                            
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=9> 
                                                
                                 <!-- <uc:paginacion ID="ucPaginacion" runat="server" />   -->     
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Puntos -->
                        
                        <!-- Panel Editar  Puntos-->
                        <asp:Panel ID=pnlEDCauces Runat=server Visible=false BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px">
                        <asp:label id=lblCauceSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec   ForeColor="#666699" Width="232px">&nbsp;<b>MANTENIMIENTO CAUCE: </b></asp:Label>
                            <table width="100%" cellspacing=0 cellpadding=1 class="tablaEdicion" border="1" >
                            <tr>
                            <td >
                                <table cellspacing=2 cellpadding=2 id="TABLE1" border="0">
                                <tr>
                                    <td rowspan="1" style="width: 164px; background-color: #666699; text-align: right">
                                        <strong><span nowrap style="color: white;text-align: right;">Código Cauce</span></strong></td>
                                    <td style="width: 8px;  width:60px; text-align: left;">
                                    <asp:TextBox ID=txtcodigoCauce runat=server CssClass=texto Height="13px" MaxLength="20"></asp:TextBox></td>
                                    <td colspan="1" style="width: 40px; text-align: left">
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcodigoCauce" ErrorMessage="El código es obligado" Width="128px"></asp:RequiredFieldValidator></td>
                                    <td colspan="2" style="width: 40px; text-align: left">
                                        <asp:TextBox ID="txtcodigoCauceNuevo" runat="server" CssClass="texto" Height="13px" MaxLength="20" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtCodigoCaucePadre" runat="server" CssClass="texto" Height="13px" MaxLength="20" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtcodigoCaucePadreNuevo" runat="server" CssClass="texto" Height="13px" MaxLength="20" Visible="False"></asp:TextBox></td>
                                  
                                        
                                </tr>   
                                    <tr>
                                        <td rowspan="1" style="width: 164px; background-color: #666699; text-align: right">
                                            <strong><span style="color: #ffffff">Código Cauce Padre</span></strong></td>
                                        <td colspan="5" style="height: 1px; background-color: gainsboro; text-align: left">
                                    <asp:Image
                                            ID="imgArbol" runat="server" align="absmiddle" ImageUrl="~/SICAH/images/iconos/icoBtnSistemas.GIF"
                                            Style="cursor: pointer" ToolTip="Seleccionar Cauce Padre" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcodigoCaucePadreNuevo"
                                                ErrorMessage="Seleccione el Cauce Padre" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblDesCodigoCaucePadre" runat="server" BackColor="#E0E0E0" Width="366px"></asp:Label></td>
                                    </tr>
                                <tr >
                                    <td nowrap style="height: 1px;  text-align: right; width: 164px;">
                                        Denominación</td>
                                    <td style="height: 1px;  text-align: left;" align="center" colspan="5">
                                       <asp:TextBox ID=txtdenominacion CssClass=texto runat=server Height="13px" MaxLength="255" Width="624px" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdenominacion"
                                            ErrorMessage="RequiredFieldValidator">Denominación Requerida</asp:RequiredFieldValidator></td>  
                                </tr>
                                  
                                <tr>
                                    <td nowrap style="height: 1px;  text-align: right; width: 164px;">
                                        Cód.Inventario</td>
                                    <td style="width: 40px; height: 1px;  text-align: left;"><asp:TextBox ID=txtcodinventario runat=server CssClass=texto  Height="13px" MaxLength="15"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 153px;">
                                        Cód. Campo</td>
                                    <td style="width: 4px; height: 1px;  text-align: left;"><asp:TextBox ID=txtcodcampo runat=server CssClass=texto  Height="13px" MaxLength="15"></asp:TextBox>
                                        </td>                                                          
                                    <td style="height: 1px;  width: 177px; text-align: right;">
                                        Tipo Cauce</td>
                                    <td style="height: 1px;  width: 58px; text-align: left;"><asp:TextBox ID=txtTipoToma runat=server CssClass=texto Height="13px" MaxLength="1" Width="16px" ></asp:TextBox></td>                                        
                                </tr>                
                                <tr>
                                    <td nowrap style=" text-align: right; height: 1px; width: 164px;">
                                        Margen Derivación</td>
                                    <td style="width: 40px;  text-align: left; height: 1px;"><asp:textbox ID=txtMargenDeriv runat=server CssClass=texto Height="13px" Width="16px" MaxLength="1"></asp:textbox>
                                        </td>
                                    <td style="width: 153px;  text-align: right; height: 1px;">
                                        Provincia</td>
                                    <td style="width: 4px;  text-align: left; height: 1px;"><asp:textbox ID=txtProvincia runat=server CssClass=texto Height="13px"  MaxLength="20" />
                                        </td>                              
                                    <td style=" width: 177px; text-align: right; height: 1px;">
                                        Municipio</td>
                                    <td style=" width: 58px; text-align: left; height: 1px;"><asp:TextBox ID=txtMunicipio runat="server" CssClass=texto Height="13px"  MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                                       </td>
                                </tr>
                                <tr>
                                    <td nowrap style=" text-align: right; height: 23px; width: 164px;">
                                        Otras Referencias</td>
                                    <td style="width: 142px;  text-align: left; height: 23px;" colspan="5"><asp:textbox ID=txtOtrasRef runat=server CssClass=texto Height="13px" MaxLength="255" Width="624px" TextMode="MultiLine"></asp:textbox>
                                        </td>
                                </tr>
                                
                                <tr>
                                    <td style=" text-align: right; height: 1px; width: 164px;">
                                        Paraje<br />
                                    </td>
                                    <td style=" width: 142px; text-align: left; height: 1px;" colspan="5">
                                        <asp:textbox ID="txtParaje" runat="server" CssClass="texto" Height="13px"  MaxLength="255" Width="624px" TextMode="MultiLine"></asp:textbox></td>
                                </tr>       
                                
                                <tr>
                                    <td style="height: 1px;  text-align: right; width: 164px;">
                                        Administrador</td>
                                    <td style="height: 1px;  width: 142px; text-align: left;" align="center" colspan="5">
                                       <asp:TextBox ID=txtAdministrador CssClass=texto runat=server  Height="13px" MaxLength="255" Width="624px" TextMode="MultiLine"></asp:TextBox>
                                    </td>  
                                </tr>
                                <tr>
                                    <td nowrap style="height: 1px;  text-align: right; width: 164px;">
                                        Caudal Máx.(l/seg)</td>
                                    <td style="width: 40px; height: 1px;  text-align: left;"><asp:TextBox ID=txtCaudal runat=server CssClass=texto  Height="13px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtCaudal"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>
                                    <td nowrap style="text-align: left; width: 153px;">
                                        Volumen Máx. Anual Legal (m3)</td>
                                    <td style="width: 4px; height: 1px;  text-align: left;"><asp:TextBox ID=txtVolumenLegal runat=server CssClass=texto  Height="13px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtVolumenLegal"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>                                                          
                                    <td nowrap style="width: 177px; text-align: right;">
                                        Volumen Máx Anual Teórico (m3)</td>
                                    <td style="height: 1px;  width: 58px; text-align: left;"><asp:TextBox ID=txtVolumenAnual runat=server CssClass=texto Height="13px" ></asp:TextBox>                                    
                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtVolumenAnual"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>                                        
                                </tr>                
                               <tr>
                                    <td style="height: 1px;  text-align: right; width: 164px;">
                                        X (toma)</td>
                                    <td style="width: 40px; height: 1px;  text-align: left;"><asp:TextBox ID=txtX runat=server CssClass=texto  Height="13px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtX"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>
                                    <td style="text-align: right; width: 153px;">
                                        Y (toma)</td>
                                    <td style="width: 4px; height: 1px;  text-align: left;"><asp:TextBox ID=txtY runat=server CssClass=texto  Height="13px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtY"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>                                                          
                                    <td style="text-align: right; width: 177px;">
                                        Cota Toma</td>
                                    <td style="height: 1px;  width: 58px; text-align: left;"><asp:TextBox ID=txtCota runat=server CssClass=texto Height="13px" ></asp:TextBox>                                    
                                        <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtCota"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>                                        
                                </tr>                
                               <tr>
                                    <td nowrap style="height: 1px;  text-align: left; width: 164px;">
                                        Superficie Real Aproximada (has)</td>
                                    <td style="width: 40px; height: 1px;  text-align: left;"><asp:TextBox ID=txtSupReal runat=server CssClass=texto  Height="13px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                            ControlToValidate="txtSupReal" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>
                                    <td nowrap style="text-align: right; width: 153px;">
                                        Superficie Inscrita (has)</td>
                                    <td style="width: 4px; height: 1px;  text-align: left;"><asp:TextBox ID=txtSupInscrita runat=server CssClass=texto  Height="13px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSupInscrita"
                                            ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>                                                          
                                    <td style="text-align: right; width: 177px;">
                                        % Tradicional</td>
                                    <td style="height: 1px;  width: 58px; text-align: left;"><asp:TextBox ID=txtTradicional runat=server CssClass=texto Height="13px" ></asp:TextBox>                                    
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTradicional"
                                            ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>                                        
                                </tr>         
                                                               <tr>
                                    <td nowrap style="text-align: right; width: 164px;">
                                        Tipo Cultivo</td>
                                    <td style="width: 40px;  text-align: left;"><asp:TextBox ID=txtTipoCultivo runat=server CssClass=texto  Height="13px" MaxLength="100"></asp:TextBox>
                                    </td>
                                    <td nowrap style="text-align: right; width: 153px;">
                                        Longitud Cauce (km)</td>
                                    <td style="width: 4px;  text-align: left;"><asp:TextBox ID=txtLongitudCauce runat=server CssClass=texto  Height="13px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtLongitudCauce"
                                            ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>                                                          
                                    <td nowrap style="text-align: right; width: 177px;">
                                        EntreOjos y Contraparada</td>
                                    <td style="width: 58px; text-align: left;">
                                        &nbsp;<asp:CheckBox ID="chkEntreOjos" runat="server" CssClass="texto" Width="112px" style="left: -6px; position: relative; top: 0px" /></td>                                        
                                </tr>                
                               <tr>
                                    <td style="height: 24px;  text-align: right; width: 164px;">
                                        En Activo</td>
                                    <td style="width: 40px; height: 24px;  text-align: left;">
                                        &nbsp;<asp:CheckBox ID="chkenActivo" runat="server" CssClass="texto" Width="96px" style="left: -6px; position: relative; top: 0px" /></td>
                                    <td nowrap style="text-align: right; width: 153px; height: 24px;">
                                        Medido en PVYCR</td>
                                    <td style="width: 4px; height: 24px;  text-align: left;">
                                        &nbsp;<asp:CheckBox ID="chkMedido" runat="server" CssClass="texto" Width="88px" style="left: -6px; position: relative; top: 0px" /></td>                                                          
                                    <td nowrap style="text-align: right; width: 177px; height: 24px;">
                                        Titular Contactado</td>
                                    <td style="height: 24px;  width: 58px; text-align: left;">
                                        &nbsp;<asp:CheckBox ID="chkTitular" runat="server" CssClass="texto" Width="80px" style="left: -6px; position: relative; top: 0px" /></td>                                        
                                </tr>  
                                <tr>
                                    <td style="height: 1px;  text-align: right; width: 164px;">
                                        Contador OK</td>
                                    <td style="width: 40px; height: 1px;  text-align: left;">
                                        &nbsp;<asp:CheckBox ID="chkContadorOK" runat="server" CssClass="texto" Width="112px" style="left: -6px; position: relative; top: 0px" /></td>
                                </tr>                
                               <tr>
                                    <td style="height: 1px;  text-align: right; width: 164px">
                                        Observaciones</td>
                                    <td style="width: 7px; height: 1px;  text-align: left;" colspan="5"><asp:TextBox TextMode="MultiLine" ID=txtObserva runat=server CssClass=texto  Height="45px" Width="770px"></asp:TextBox>
                                    </td>
                                </tr>    
                                <tr>
                                    <td   class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 21px; ">                                        
                                        Datos Titular</td>
                                   
                                </tr>

                                <tr>
                                    <td style="height: 2px;  text-align: right; width: 164px;">
                                        Titular</td>
                                    <td style="height: 2px;  width: 348px; text-align: left;" align="center" colspan="3">
                                       <asp:TextBox ID=txtTitular CssClass=texto runat=server  Height="13px" MaxLength="255" Width="440px" TextMode="MultiLine"></asp:TextBox>
                                    </td>  
                                    <td style="height: 2px; width: 177px; text-align: right;">
                                        NIF</td>
                                    <td style="height: 2px;  width: 58px; text-align: left;" align="center">
                                       <asp:TextBox ID=txtNIF CssClass=texto runat=server  Height="13px" MaxLength="15"></asp:TextBox>
                                    </td>  

                                </tr>
                                <tr>
                                    <td style="height: 1px;  text-align: right; width: 164px;">
                                        Dirección</td>
                                    <td style="height: 1px;  width: 348px; text-align: left;" align="center" colspan="5">
                                       <asp:TextBox ID=txtDireccion CssClass=texto runat=server  Height="13px" MaxLength="255" Width="624px" TextMode="MultiLine"></asp:TextBox>
                                    </td>  
                                </tr>

                                
                                <tr>
                                    <td style="text-align: right; height: 1px; width: 164px;">
                                        Municipio</td>
                                    <td style="width: 40px;  text-align: left; height: 1px;">
                                              <asp:TextBox ID="txtMunicipioTit" runat="server" CssClass="texto" Height="13px" Style="left: 0px;
                                            position: relative; top: 0px"  MaxLength="100" TextMode="MultiLine"></asp:TextBox></td>
                                    <td style="width: 153px; text-align: right; height: 1px;">
                                        Provincia</td>                              
                                    <td style="width: 4px;  text-align: left; height: 1px;"><asp:TextBox ID=txtProvinciaTit runat=server CssClass="texto" Height="13px" MaxLength="20"></asp:TextBox>                               
                                    </td> 
                                    <td style="width: 177px; text-align: right; height: 1px;">
                                        CP</td>
                                    <td style=" width: 58px; text-align: left; height: 1px;">
                                        &nbsp;
                                        <asp:TextBox ID="txtCP" runat="server" CssClass="texto" Height="13px" Style="left: 0px;
                                            position: relative; top: -6px"  MaxLength="5"></asp:TextBox></td> 
                                </tr>
                                <tr>
                                    <td style=" text-align: right; height: 1px; width: 164px;">
                                        Teléfono</td>
                                    <td style="width: 40px;  text-align: left; height: 1px;"><asp:TextBox ID=txtTelefono runat=server CssClass="texto" Height="13px" MaxLength="50" ></asp:TextBox>
                                        </td>
                                </tr>
                                <tr>
                                        <td   class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 21px; ">
                                        Datos Registro de Aguas</td>
                                   
                                </tr>

                                <tr>
                                    <td nowrap style="text-align: right; height: 4px; width: 164px;">
                                        Expediente Libro</td>
                                    <td style="width: 40px;  text-align: left; height: 4px;" colspan=""><asp:TextBox ID=txtExpediente runat=server CssClass="texto" Height="13px" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    <td style="text-align: right; width: 153px;"> Registro Antiguo</td>
                                    <td style="width: 4px;  text-align: left; height: 4px;"><asp:TextBox CssClass="texto" ID="txtRegAntiguo" runat="server" Height="13px" TextMode="MultiLine" ></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtRegAntiguo"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>
                                    <td nowrap style="text-align: right; width: 177px;">
                                        Exptes. Registro</td>
                                    <td style="width: 58px;  text-align: left; height: 4px;"><asp:TextBox ID=txtExptesLibro runat=server CssClass="texto" Height="13px" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                </tr>
                                <tr>
                                    <td nowrap style="text-align: right; height: 30px; width: 164px;">
                                        Otros Expedientes</td>
                                    <td style="width: 40px;  text-align: left; height: 30px;"><asp:TextBox ID=txtOtrosExpedientes runat=server CssClass="texto" Height="13px" MaxLength="50" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                </tr>
                                
                                 <tr>
                                    <td style="text-align: right; height: 1px; width: 164px;">
                                        Sección</td>
                                    <td style="width: 40px;  text-align: left; height: 1px;">
                                              <asp:TextBox ID="txtSeccion" runat="server" CssClass="texto" Height="13px" Style="left: 0px;
                                            position: relative; top: 0px" Width="40px" MaxLength="1"></asp:TextBox></td>
                                    <td style="width: 153px; text-align: right; height: 1px;">
                                        Tomo</td>                              
                                    <td style="width: 4px;  text-align: left; height: 1px;"><asp:TextBox ID=txtTomo runat=server CssClass="texto" Height="13px" Width="68px"></asp:TextBox>                               
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtTomo"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="68px"></asp:CompareValidator></td> 
                                    <td style="width: 177px; text-align: right; height: 1px;">
                                        Folio</td>
                                    <td style=" width: 58px; text-align: left; height: 1px;">                                       
                                        <asp:TextBox ID="txtFolio" runat="server" CssClass="texto" Height="13px"  Width="68px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtFolio"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="68px"></asp:CompareValidator></td> 
                                </tr>
                                
                                <tr>
                                <td style="text-align: right">Inscripción</td>
                                <td style="width: 7px;"><asp:TextBox id="txtInscripcion" runat="server" cssclass="texto"></asp:TextBox></td>
                                <td style="text-align: right">Inscripción relacionada</td>
                                <td style="width: 7px;"><asp:TextBox id="txtInscripcionRelacionada" runat="server" cssclass="texto"></asp:TextBox></td>
                                <td style="text-align: right;">Inscripción estado</td>
                                <td style="width: 348px; text-align: left;" align="center">
                                <asp:DropDownList ID="cmbInscripcionEstado" style="font:11px Verdana" runat=server>
                                    <asp:ListItem Text="" Value="0" Selected></asp:ListItem>
                                    <asp:ListItem Text="Anulado" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Vigente" Value="2"></asp:ListItem>
                                </asp:DropDownList>                                   
                                </td>  
                              </tr>
                                <tr>
                                        <td   class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 21px; ">    
                                        Datos Contacto</td>
                                   
                                </tr>


                                <tr>                                   
                                                                                                     
                                    <td style="text-align: right; height: 1px; width: 164px;">
                                        Contacto</td>
                                    <td style="width: 7px;  text-align: left; height: 1px;" colspan="5"><asp:TextBox ID=txtContacto runat=server CssClass="texto" Height="13px"  MaxLength="255" Width="624px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                </tr>                                
                                                             
                                <tr>                                    
                                    <td style="text-align: right; height: 1px; width: 164px;">
                                        Dirección</td>
                                    <td style="width: 7px;  text-align: left; height: 1px;" colspan="3" rowspan=""><asp:TextBox ID=txtDireccionCont runat=server CssClass="texto" Height="13px"  MaxLength="255" Width="440px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    <td style="width: 177px; text-align: right; height: 1px;">
                                        Teléfono</td>
                                    <td style="width: 58px;  text-align: left; height: 1px;"><asp:TextBox ID=txtTelContacto runat=server CssClass="texto"  Height="13px" MaxLength="50" TextMode="MultiLine"></asp:TextBox></td>

                                </tr>   
                                <tr>
                                
                                    <td colspan="6" align="right" 
                                        style="border-top:1px solid #666666; height: 24px;">
                                    <asp:Button ID="btnAceptar" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                    <asp:Button ID="btnCancelar" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>
                                    <asp:Button ID="btnImprimir" Runat="server" cssclass="boton" Text='Imprimir'></asp:Button>
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
                   

        
</table></td></tr></table> 
        
</td></tr></table>
    </form>
</body>
</html>
