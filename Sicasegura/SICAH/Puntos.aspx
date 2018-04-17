<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Puntos.aspx.vb" Inherits="SICAH_puntos" %>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
     <link rel="stylesheet" href="..\styles.css">
     <link href="../styles.css" rel="stylesheet" />
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
     <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
<script language="javascript" src="..\js/utilesMenus.js"></script> 
<script type="text/javascript" src="../aspnet_client/OpenFlashChart/js/swfobject.js"></script>
    

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
    
      function desplegarArbol(sender)
    {
        var despArbol = document.getElementById('desplegableArbol');
        if (despArbol.style.display=='none'){
            ocultarDesplegables();
            var elementoOrigen = event.srcElement;
            
            if (elementoOrigen.tagName=='IMG'){
                despArbol.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft+elementoOrigen.parentElement.offsetLeft-255 + document.body.scrollLeft) + 'px';
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

   
// ]]>
</script>

    <link href="../styles.css" rel="stylesheet" />
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
  <form id="Form1" runat="server" method=post>
  <div id="desplegableArbol" style="position:absolute;width:331px;padding:2px;background-color:white;border:1px solid #8C8B83;display:none; left: 192px; top: 176px; height: 299px;">
        
            <table style="height: 100%" width="100%">
                <tr>
                    <td style="width:96%; height: 17px;">
                    </td>
                    <td style="width: 18px; height: 17px;">
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
                    <td style="width: 18px; height: 273px;">
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
                   <!--<td colspan="5"valign="top" style="padding-top: 20px; width:20%">  -->
                   <%=genHTMLMenus.generaMenuMtoSica(Session("idperfil"), Application("PVYCR_Arbol_Updated"))%>      
                   <!--<%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%>-->
                   <!--</td>-->
                   <!--<td style="background-image:url(../images/dot2.gif);width:3px">-->
                   
                    <!-- Fin Celda Menú -->
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%" valign=top>
                        <!-- Panel listar Contadores -->                      
                        <asp:Panel ID=pnlPuntos Runat=server Visible=true>
                            <table width=100%>
                            <tr>
                                <td align="right"></td>
                            </tr>
                            
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666">
                                <table align="left">
                                <tr><td>&nbsp</td></tr>
                                 <tr>
                                    <td colspan="2" align="left"  >Nº Reg. filtrados:  <asp:TextBox  ID="txtNumRegFiltrados"  runat="server" Width="30px" Enabled=false  CssClass="texto"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="txtNumRegFiltrados"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                            <asp:Button runat="server" ID="btnListFiltro" cssclass="boton" Text="Listar Filtro" />
                                </td>
                                </tr>
                                </table>
                                
                                <table align="right">
                                <tr style="display:<%= visibleSegunPerfil2()%>"><td colspan="6"  align="right"><asp:Button runat="server" ID="btnVerIncidencias" cssclass="boton" Text="Listar Incidencias" /></td></tr> 
                                 <tr>
                                <td colspan="6"  align="right">Registros a mostrar  <asp:TextBox  ID="txtRegistros"  runat="server" Text="10" Width="55px"  CssClass="texto"></asp:TextBox><asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtRegistros"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                        <asp:Button runat="server" ID="btnVertodos" cssclass="boton" Text="Actualizar"  /></td>
                                </tr>
                                </table>
                                </td>                                                
                            </tr>
                            </table>
                            <table width=100%>                                               
                                 
                                    <tr><td class="tituloSec" colspan=4>MANTENIMIENTO DE PUNTOS</td></tr>
                                    <tr>
                                        <td  align="left" colspan="1"><a  id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a></td>
                                        <td colspan="5" align="right"><asp:LinkButton runat="server" ID="lbtNuevo" Visible="<%# VisibleSegunPerfil()%>" OnClick="nuevoPunto" Text=" Nuevo Punto"></asp:LinkButton></td>
                                    </tr>
                                    <tr><td class="encabListado">Código PVYCR</td>
                                    <%--<td class="encabListado">Código Cauce</td>--%>
                                    <td class="encabListado">Denominación</td>   
                                    <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width=310px ID="ddlFiltrado" runat="server" AutoPostBack="false" ></asp:DropDownList></td>                                    
                                    <%--<td class="encabListado">Tipo</td> --%>  
                                    <td class="encabListado">&nbsp;</td>                            
                                    </tr>
                                    <tr id="FilaFiltro"  >         
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFCodigoPVYCR" CssClass="texto" Columns="13"></asp:TextBox></td>                                                  
                                         <%--<td class="encabListado"><asp:TextBox runat="server" ID="txtFCodigoCauce" CssClass="texto" Columns="13"></asp:TextBox></td>   --%>
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFDenominacionPunto" CssClass="texto" Columns="50"></asp:TextBox></td>  
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFiltrado" CssClass="texto" Columns="50"></asp:TextBox></td>  
<%--                                         <td class="encabListado" style="width:150px; vertical-align: middle; text-align: left; height: 1px;">&nbsp;<asp:DropDownList ID="ddlFTipoPunto" runat="server"  Font-Size="8pt"  Width="136px">
                                            <asp:ListItem Value="0">[Seleccionar]</asp:ListItem>
                                            <asp:ListItem Value="G">Acequia</asp:ListItem>
                                            <asp:ListItem Value="M">Motor</asp:ListItem>
                                            <asp:ListItem Value="P">Peaje</asp:ListItem>
                                            <asp:ListItem Value="T">Trasvase</asp:ListItem>
                                         </asp:DropDownList></td>  
--%>                                         <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsReg" onclick="AceptarFiltro" Runat="server" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                    </tr>   
                                    <asp:Repeater ID=rptPuntos runat=server>                                                     
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><%#Container.DataItem("CodigoPVYCR")%></td>
                                            <%--<td><%#Container.DataItem("CodigoCauce")%></td>--%>
                                            <td><%#Container.DataItem("DenominacionPunto")%></td>
                                            <td><%#ObtenerFiltrado(Container.DataItem)%></td>
                                            <%--<td><%#Container.DataItem("TipoPunto")%></td > --%>                                                                                   
                                            <td nowrap align=right width=36>
                                                <asp:LinkButton ID=lbtEdit Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='editar' CommandArgument='<%# container.dataitem("CodigoPVYCR")%>'><img src="../images/edit.gif" border=0 align=left alt="Editar datos"></asp:LinkButton>
                                                <!--<asp:LinkButton ID=lbtDelete Runat=server CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%# container.dataitem("CodigoPVYCR") %>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar"></asp:LinkButton>-->
                                                <asp:LinkButton ID=lbtPerfil Visible='<%#esAcequia(container.dataitem("TipoPunto"), container.dataitem("CodigoPVYCR")) %>' Runat=server CommandName='perfilAcequia' CommandArgument='<%# container.dataitem("TipoPunto")%>'  ><img src="../images/iconos/icoPestNibus.gif" border=0 align=left alt="Sección Acequia" onclick="AbrirGrafico('<%# container.dataitem("CodigoPVYCR")%>');return false;" ></asp:LinkButton>                	                        

                                             </td>                       
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=9> 
                                                
                                  <!-- <uc:paginacion ID="ucPaginacion" runat="server" />  -->    
                                    
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Puntos -->
                        
                        <!-- Panel Editar  Puntos-->
                        <asp:Panel ID=pnlEDPuntos Runat=server Visible=false BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px">
                        <asp:label id=lblPuntoSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec  Width="368px" ForeColor="#003399">&nbsp;<b>MANTENIMIENTO PUNTO: </b></asp:Label>
                            <table width="100%" cellspacing=0 cellpadding=1 class="tablaEdicion">
                            <tr>
                            <td >
                                <table cellspacing=2 cellpadding=2 id="TABLE1" onclick="return TABLE1_onclick()" width="100%" >
                                <tr>
                                    <td style="height: 1px; vertical-align: middle; width: 3844px; text-align: left; background-color: gainsboro">
                                        <strong><span style="color: #003399">CódigoPVYCR</span></strong></td>
                                    <td style="width: 454px; height: 1px; vertical-align: middle; text-align: left; background-color: gainsboro"><asp:TextBox ID=txtcodigoPunto runat=server CssClass=texto Height="13px" Width="134px"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcodigoPunto"
                                            Display="Dynamic" ErrorMessage="El código PVYCR es obligado" Width="136px"></asp:RequiredFieldValidator></td>
                                    <td style="height: 1px; width: 395px; vertical-align: middle; text-align: right; background-color: gainsboro">
                                        <span style="color: #003399">Código Cauce</span></td>
                                    <td style="width: 97px; height: 1px; vertical-align: middle; text-align: left; background-color: gainsboro">
                                    <table >
                                    <tr>
                                        <td>
                                          <asp:TextBox ID=txtcodigocauce runat=server CssClass=texto Height="13px" Width="121px"></asp:TextBox>
                                          <asp:TextBox ID=txtIdArbol visible=false runat=server CssClass=texto Height="13px" Width="121px"></asp:TextBox>
                                        </td>
                                                <td><asp:Image ID="imgArbol" runat="server" align="absmiddle" ImageUrl="~/SICAH/images/iconos/icoBtnSistemas.GIF"
                                                    Style="cursor: pointer" ToolTip="Seleccionar del Arbol el Cauce" />
                                                </td>
                                    </tr>               
                                    </table>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcodigocauce"
                                            Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>                             
                                    <td colspan="2" style="vertical-align: middle; width: 158px; height: 1px; background-color: gainsboro;
                                        text-align: left">
                                        <asp:Label ID="lblDesCodigoCauce" runat="server" BackColor="#E0E0E0" Width="264px" Height="16px"></asp:Label><br />
                                    </td>
                                  
                                </tr>   
                                <tr >
                                    <td style="height: 1px; vertical-align: middle; width: 3844px; text-align: right;">
                                        Denominación</td>
                                    <td style="height: 1px; vertical-align: middle; width: 142px; text-align: left;" align="center" colspan="5">
                                       <asp:TextBox ID=txtdenominacion CssClass=texto runat=server Width="818px" Height="13px" MaxLength="255"></asp:TextBox>
                                    </td>  
                                </tr>
                                <tr  style="height: 2px">
                                    <td style="vertical-align: middle; width: 3844px; text-align: right; height: 1px;">Tipo Punto
                                    </td>
                                    <td style="width: 454px; vertical-align: middle; text-align: left; height: 1px;">
                                        &nbsp;<asp:DropDownList ID="ddlTipoPunto" runat="server" AutoPostBack="False" Font-Size="8pt"
                                            Width="136px">
                                            <asp:ListItem Value="0">[Seleccionar]</asp:ListItem>
                                            <asp:ListItem Value="G">Acequia</asp:ListItem>
                                            <asp:ListItem Value="M">Motor</asp:ListItem>
                                            <asp:ListItem Value="P">Peaje</asp:ListItem>
                                            <asp:ListItem Value="T">Trasvase</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlTipoPunto"
                                            ErrorMessage="*" Operator="NotEqual" ValueToCompare="0" Display="Dynamic"></asp:CompareValidator>
                                    </td>
                                    <td style="height: 1px; vertical-align: middle; width: 395px; text-align: right">
                                        Tipo Sensor</td>
                                    <td style="width: 97px; height: 1px; vertical-align: middle; text-align: left"><asp:TextBox ID=txtSensor runat=server CssClass=texto Width="134px" Height="13px" OnTextChanged="ModificarEsTelemedida"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                        Código DataLogger</td>                              
                                    <td style="vertical-align: middle; text-align: left; height: 1px; width: 1px"><asp:TextBox ID=txtDataLogger runat=server CssClass="texto" MaxLength="5" Height="13px" Width="134px"></asp:TextBox>                               
                                    </td> 
                                </tr>
                                <tr>                                    
                                    <td style="vertical-align: middle; width: 3844px; text-align: right; height: 1px;">
                                        Usado en parte oficial</td>
                                    <td style="vertical-align: middle; width: 454px; text-align: left; height: 1px;">
                                        &nbsp;<asp:CheckBox ID="chkUso" runat="server" CssClass="texto" />
                                        </td> 
                                    <td style="width: 395px; vertical-align: middle; text-align: right; height: 1px;">
                                            % Regable</td>
                                        <td style="width: 97px; vertical-align: middle; text-align: left; height: 1px;"><asp:textbox ID=txtRegable runat=server CssClass=textoNumerico Height="13px" Width="134px" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRegable"
                                                ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>       
                                    <td style="width: 395px; vertical-align: middle; text-align: right; height: 1px;">
                                            Favorito</td>
                                        <td style="width: 97px; vertical-align: middle; text-align: left; height: 1px;">
                                        <asp:DropDownList ID="ddlFavorito" runat="server" AutoPostBack="False" Font-Size="8pt"
                                            Width="136px">
                                            <asp:ListItem Value="SI">SI</asp:ListItem>
                                            <asp:ListItem Value="NO" Selected="True">NO</asp:ListItem>
                                        </asp:DropDownList></td>                                                       
                                </tr>
                                 <tr>                                    
                                    <td style="vertical-align: middle; width: 3844px; text-align: right; height: 1px;">Fecha Inicio</td>
                                    <td style="vertical-align: middle; width: 454px; text-align: left; height: 1px;">&nbsp;<asp:TextBox runat="server" ID="txtFechaInicion" CssClass="texto" style="width:104px" ></asp:TextBox>
                                            <asp:Image    ID="imgfechaInicion" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif" Style="cursor: pointer" />
                                            <asp:CustomValidator ID="ComFfechamedidaM" runat="server" ControlToValidate="txtFechaInicion" Display="Dynamic" ErrorMessage="Sólo fechas" EnableClientScript="true" ClientValidationFunction="validarFecha"></asp:CustomValidator>
                                        </td> 
                                    
                                    <td style="width: 395px; vertical-align: middle; text-align: right; height: 1px;">
                                            Fecha Fin</td>
                                    <td style="width: 395px; vertical-align: middle; text-align: left; height: 1px;">&nbsp;<asp:TextBox runat="server" ID="txtFechaFin" CssClass="texto" style="width:104px" ></asp:TextBox>
                                            <asp:Image    ID="imgfechaFin" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif" Style="cursor: pointer" />
                                            <asp:CustomValidator ID="ComFfechamedida" runat="server" ControlToValidate="txtFechaFin" Display="Dynamic" ErrorMessage="Sólo fechas" EnableClientScript="true" ClientValidationFunction="validarFecha"></asp:CustomValidator>
                                        </td>                                                
                                </tr>
                               <tr>
                                    <td   class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 15px; "></td>                                   
                                </tr>                              
                                <tr>                                                                                            
                                    <td style="height: 1px; vertical-align: middle; width: 3844px; text-align: right;">
                                        X</td>
                                    <td style="height: 1px; vertical-align: middle; width: 454px; text-align: left;"><asp:TextBox ID=txtX runat=server CssClass=textoNumerico Height="13px" Width="134px"></asp:TextBox><asp:CompareValidator ID="ComCotaToma" runat="server" ControlToValidate="txtX" Display="Dynamic"
                                            ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer"
                                            Width="8px"></asp:CompareValidator></td>                                        
                                    <td style="vertical-align: middle; width: 395px; text-align: right; height: 1px;">
                                        Y</td>
                                    <td style="width: 97px; vertical-align: middle; text-align: left; height: 1px;">
                                    <asp:textbox ID=txtY runat=server CssClass=textoNumerico Height="13px" Width="134px"></asp:textbox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtY"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>  
                                    <td style="vertical-align: middle; width: 3844px; text-align: right; height: 1px;">
                                        Longitud</td>
                                    <td style="width: 454px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtLongitud runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtLongitud"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: middle; width: 3844px; text-align: right; height: 1px;">
                                        PKS</td>
                                    <td style="width: 454px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtPKS runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtPKS"
                                            ErrorMessage="?" ValidationExpression="\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                                    <td style="width: 395px; vertical-align: middle; text-align: right; height: 1px;">
                                        PKA</td>
                                    <td style="width: 97px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtPKA runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtPKA"
                                            ErrorMessage="?" ValidationExpression="\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>                                                                                   
                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                        RIO</td>
                                    <td style="vertical-align: middle; text-align: left; height: 1px; width: 1px;"><asp:TextBox ID=txtRIO runat=server CssClass="texto" Height="13px" Width="134px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td   class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 15px; "></td>                                   
                                </tr>  
                                <tr>
                                    <td style="vertical-align: middle; width: 3844px; text-align: right; height: 1px;">
                                        A1_M</td>
                                    <td style="width: 454px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtA1_M runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                            ControlToValidate="txtA1_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>                                
                                    <td style="vertical-align: middle; width: 395px; text-align: right; height: 1px;">
                                        A2_M</td>
                                    <td style="width: 97px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtA2_M runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                            ControlToValidate="txtA2_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                                    <td style="vertical-align: middle; width: 395px; text-align: right; height: 1px;">
                                        A3_M</td>
                                    <td style="width: 97px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtA3_M runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                            ControlToValidate="txtA3_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                               </tr>                                                                          
                               <tr>     
                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                     B1_M</td>
                                     <td style="vertical-align: middle; text-align: left; height: 1px; width: 1px;"><asp:TextBox ID=txtB1_M runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                            ControlToValidate="txtB1_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>                               
                                    <td style="width: 3844px; text-align: right; height: 1px;">B2_M</td>
                                    <td style="width: 454px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtB2_M runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                            ControlToValidate="txtB2_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>
                                    <td style="vertical-align: middle; width: 395px; text-align: right; height: 1px;">B3_M</td>
                                    <td style="width: 97px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtB3_M runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                            ControlToValidate="txtB3_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                                </tr>      
                               <tr>
                                    <td style="vertical-align: middle; width: 395px; text-align: right; height: 1px;">B4_M</td>
                                    <td style="width: 97px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtB4_M runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                            ControlToValidate="txtB4_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                        H12_M</td>
                                    <td style="vertical-align: middle; text-align: left; height: 1px; width: 1px;"><asp:TextBox ID=txtH12_M runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                            ControlToValidate="txtH12_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>                                                                                                                                                           
                                    <td style="vertical-align: middle; width: 3844px; text-align: right; height: 1px;">
                                        &nbsp;H23_M</td>
                                    <td style="width: 454px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtH23_M runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                            ControlToValidate="txtH23_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                              </tr>
                              <tr>
                                    <td style="vertical-align: middle; width: 3844px; text-align: right; height: 1px;">
                                        &nbsp;H34_M</td>
                                    <td style="width: 454px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtH34_M runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                            ControlToValidate="txtH34_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>                              
                                    <td style="width: 3844px; vertical-align: middle; text-align: right; height: 1px;">
                                        Diámetro (mm)</td>                              
                                    <td style="width: 454px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtDiametro runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtDiametro"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td> 
                                    <td style="vertical-align: middle; width: 395px; text-align: right; height: 1px;">
                                        &nbsp; Offset_M</td>
                                    <td style="width: 97px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox ID=txtOffset runat=server CssClass="textoNumerico" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtOffset" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td> 
                                </tr>       
                                <tr>
                                    <td   class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 15px; "></td>                                   
                                </tr>  
                                <tr>
                                    <td style="width: 3844px; vertical-align: middle; text-align: right; height: 1px;"> Longitud Flotador</td>
                                    <td style="width: 454px; vertical-align: middle; text-align: left; height: 1px;"><asp:TextBox CssClass="textoNumerico" ID="txtFlotador" runat="server" Height="13px" Width="134px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ControlToValidate="txtFlotador" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                                     <td style="vertical-align: middle; width: 395px; text-align: right; height: 1px;">
                                        Factor Flotador</td>
                                    <td style="vertical-align: middle; width: 97px; text-align: left; height: 1px;"><asp:textbox ID="txtFactor" runat="server" CssClass="textoNumerico" Height="13px" Width="134px"></asp:textbox><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtFactor"
                                            ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator></td> 
                                 </tr>   
                                 <tr>
                                    <td   class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 15px; "></td>                                   
                                </tr> 
                                <asp:Panel ID="pnlNOVerGraficoAcequias" runat="server" Visible="false">
                                     <tr>
                                    <td style="height: 1px; width: 3844px; vertical-align: middle; text-align: right;">
                                        Acceso</td>
                                    <td style="width: 1px; height: 1px; vertical-align: middle; text-align: left;" colspan="5"><asp:TextBox ID=txtAccesoA runat=server CssClass=texto TextMode="MultiLine" Width="810px" Height="80px"></asp:TextBox></td>  
                                </tr>
                                <tr>
                                    <td   class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 15px; "></td>                                   
                                </tr>                          
                                <tr>                                    
                                    <td style="vertical-align: middle; width: 3844px; text-align: right; height: 1px;">Observaciones</td><td style="width: 360px; vertical-align: middle; text-align: left; height: 1px;" colspan="5"><asp:TextBox ID=txtObservacionesA runat=server TextMode="MultiLine" CssClass="texto" Width="810px" Height="80px"></asp:TextBox></td>

                                </tr>   
                                </asp:Panel>
                                <asp:Panel ID="pnlVerGraficoAcequias" runat="server" Visible="false">
                                <tr >
                                <td colspan="4"><table width="100%">
                                    <tr><td style="height: 1px;vertical-align: middle; text-align: right;">Acceso</td>
                                    <td style="vertical-align: middle; text-align: left; width:1px;" colspan="3"><asp:TextBox ID=txtacceso runat=server CssClass=texto TextMode="MultiLine" Height="80px" Width="430px"></asp:TextBox></td>  
                                    </tr>
                                    <tr><td   class="encabListado" style="text-align: left; height: 15px;" colspan="2"></td></tr>
                                    <tr>
                                    <td style="vertical-align: middle; text-align: right;">Observaciones</td><td style="vertical-align: middle; text-align: left; "><asp:TextBox ID=txtobservaciones runat=server TextMode="MultiLine" CssClass="texto" Height="80px" Width="430px"></asp:TextBox></td>                                    
                                </tr></table></td>
                                <td colspan="2" ><table  ><tr>
                                <td width="50px">                                  
                                     <div id="my_chart" >
                                    <script type="text/javascript">
                                    swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "my_chart", "350", "200",  "9.0.0", "expressInstall.swf",  {"data-file":"Perfil_acequiaFlashData.aspx"}  );
                                    </script>
                                    </div>
                                </td> </tr></table></td>
                                 
                                </tr>                                
                                </asp:Panel> 
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
<script language="javascript">
   function validarFecha(source, arguments)
   {
      if (arguments.Value != ""){
        var fecha = new Date();
        fecha = Date.parse(arguments.Value);
        if (isNaN(fecha))
            arguments.IsValid=false;
        else
            arguments.IsValid=true;
      }
      else
        arguments.IsValid=false;
   }
</script>
