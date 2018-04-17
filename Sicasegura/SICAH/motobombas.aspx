<%@ Page Language="VB" AutoEventWireup="false" CodeFile="motobombas.aspx.vb" Inherits="SICAH_motobombas" %>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
     <link rel="stylesheet" href="..\styles.css">
     <link href="../styles.css" rel="stylesheet" />
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
     <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />

<script language="javascript" src="..\js/utilesMenus.js"></script>  
    
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}
    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación de la motobomba?")==true)
        return true;
      else
        return false;
    }
    
    function confirmar_borrado_Historial()
    {
      if (confirm("¿Está seguro de la eliminación de la relación con el Elemento de Medida?")==true)
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
    
    function desplegarEM(sender)
    {
        var despSistemas = document.getElementById('desplegableEM');
        if (despSistemas.style.display=='none'){
            ocultarDesplegables();
            var elementoOrigen = event.srcElement;
            
            if (elementoOrigen.tagName=='IMG'){
                despSistemas.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft-elementoOrigen.parentElement.offsetLeft+228 + document.body.scrollLeft) + 'px';
                despSistemas.style.top = '' + (event.clientY-event.offsetY-elementoOrigen.offsetTop+elementoOrigen.parentElement.offsetHeight-5 + document.body.scrollTop) + 'px';
            }
            else if (elementoOrigen.tagName=='TD'){
                despSistemas.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft+228 + document.body.scrollLeft) + 'px';        
                despSistemas.style.top = '' + (event.clientY-event.offsetY+elementoOrigen.offsetHeight-5 + document.body.scrollTop) + 'px';
            }
            ocultarSelects();
            despSistemas.style.display='';
        }
        else{
            despSistemas.style.display='none';
            mostrarSelects();
        }
        
        event.cancelBubble=true;
        document.getElementById('lbldespegableEM').value = 'Seleccione el Elemento de Medida:';
    }
    
    
    function desplegarArbol(sender)
    {
        var despArbol = document.getElementById('desplegableArbol');
        if (despArbol.style.display == 'none') {            
            ocultarDesplegables();
            var elementoOrigen = event.srcElement;
            
            if (elementoOrigen.tagName=='IMG'){
                despArbol.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft+elementoOrigen.parentElement.offsetLeft -200+ document.body.scrollLeft) + 'px';
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
            despArbol.style.display = 'none';
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
function mostrarSelects() {
    if (remostrarselects) {
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

// ]]>
</script>

    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
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
                   <td colspan="5"valign="top" style="padding-top: 20px; width:20%">        
                     <%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%>
                   </td>
                    <td style="background-image:url(../images/dot2.gif);width:3px"></td>
                    <!-- Fin Celda Menú -->
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%" valign=top>
                        <!-- Panel listar Contadores -->                      
                        <asp:Panel ID=pnlMotobombas runat="server" Visible=true>
                            <table width=100%>
                            <tr>
                                <td align="right"><a>Listado</a></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666">
                                 <table align="left">
                                 <tr><td>&nbsp;</td></tr>
                                 <tr>
                                    <td colspan="2" align="left"  >Nº Reg. filtrados:  <asp:TextBox  ID="txtNumRegFiltrados"  runat="server" Width="30px" Enabled=false  CssClass="texto"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtNumRegFiltrados"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                            <asp:Button runat="server" ID="btnListFiltro" cssclass="boton" Text="Listar Filtro" />
                                </td>
                                </tr>
                                </table>

                                <table align="right">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnListadoPantalla" runat="server" text="Listar Información" CssClass="boton" />
                                    </td>
                                    <td>
                                    <asp:TextBox ID="txtFiltroCodigoPVYCR" runat="server" Text="[Código Motobomba]" CssClass="texto"></asp:TextBox>
                                    </td>
<%--                                    <td>
                                    <asp:Button ID="btnFiltroAceptar" runat="server" text="Filtrar" CssClass="boton" />
                                    <asp:Button ID="btnFiltroCancelar" runat="server" Text="Limpiar" CssClass="boton" />
                                    </td>
--%>                                
                                </tr>
                                <tr>
                                <td colspan="6"  align="right">Registros a mostrar  <asp:TextBox  ID="txtRegistros"  runat="server" Text="10" Width="30px"  CssClass="texto"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtRegistros"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                        <asp:Button runat="server" ID="btnVertodos" cssclass="boton" Text="Actualizar"  /></td>
                                </tr>

                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width=100%>                                               
                                
                                    
                                        <tr><td class="tituloSec" colspan="4">MANTENIMIENTO DE MOTOBOMBAS</td></tr>                                        
                                        <tr>                                           
                                           <td  align="left" colspan="<1" >
                                                <a  id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a>
                                            </td>
                                            <td colspan="9" align="right"><asp:LinkButton runat="server" Visible=<%#VisibleSegunPerfil() %> ID="lbtNuevo" OnClick="nuevaMotobomba">Nueva Motobomba</asp:LinkButton></a></td>
                                            
                                        </tr>
                                        <tr><td class="encabListado">Código Motobomba</td>
                                        <%--<td class="encabListado">Código PVYCR</td>--%>
                                        <td class="encabListado">Fecha Revisión</td>  
                                        <td class="encabListado">Descripción</td>   
                                      <%--  <td class="encabListado">Marca</td>                                
                                        <td class="encabListado">Tipo</td> 
                                        <td class="encabListado">Nº Serie</td> 
                                        <td class="encabListado">Modelo</td> --%>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width=310px ID="ddlFiltrado" runat="server" AutoPostBack="false" ></asp:DropDownList></td>
                                        <td class="encabListado">&nbsp;</td>
                                        </tr>
                                                            
                                      <tr id="FilaFiltro"  >                                                          
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFCodigoMotobomba" CssClass="texto" Columns="20"></asp:TextBox></td>  
                                         <%--<td class="encabListado"><asp:TextBox runat="server" ID="txtFCodigoPVYCR" CssClass="texto" Columns="10"></asp:TextBox></td> --%>
                                         <td class="encabListado" style="width:120px"><asp:TextBox runat="server" ID="txtFFechaRevision" CssClass="texto" style="width:74px" ></asp:TextBox>
                                            <asp:Image    ID="imgFfechamedidaM" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" />
                                            <asp:CompareValidator ID="ComFfechamedidaM" runat="server" ControlToValidate="txtFFechaRevision"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                         </td>  
                                         
                                         
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFDescripcion" CssClass="texto" Columns="23"></asp:TextBox></td>  
<%--                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFMarca" CssClass="texto" Columns="15"></asp:TextBox></td>  
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFTipo" CssClass="texto" Columns="10"></asp:TextBox></td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFNSerie" CssClass="texto" Columns="8"></asp:TextBox></td>                                                                                  
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFModelo" CssClass="texto" Columns="10"></asp:TextBox></td>   
--%>   
                                          <td class="encabListado"><asp:TextBox runat="server" ID="txtFiltrado" CssClass="texto" Columns="50"></asp:TextBox></td>  
                                           <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsReg" Runat="server" onclick="AceptarFiltro" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                    </tr> 
                                    

                                  
                                    
                                    <asp:Repeater ID="rptMotobombas" runat="server">                                      
                                                                                     
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><%#Container.DataItem("CodigoMotobomba")%></td>
                                            <%--<td><%#Container.DataItem("CodigoPVYCR")%></td>--%>
                                            <td><%#DataBinder.Eval(Container.DataItem, "FechaRevision", "{0:dd/MM/yyyy}")%></td>    
                                            <td><%#Container.DataItem("Descripcion")%></td>
<%--                                            <td><%#Container.DataItem("MarcaBomba")%></td>
                                            <td><%#Container.DataItem("TipoBomba")%></td>
                                            <td><%#Container.DataItem("NumSerieBomba")%></td>
                                            <td><%#Container.DataItem("ModeloBomba")%></td>
--%>
                                            <td><%#ObtenerFiltrado(Container.DataItem)%></td>                                                                                        
                                            
                                            <td nowrap align=right width=56>
                                                <asp:LinkButton ID=lbtEdit runat="server" Visible=<%#VisibleSegunPerfil() %> CommandName='editar' CommandArgument='<%#"" & container.dataitem("CodigoMotobomba")& "#" & container.dataitem("FechaRevision") %>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtRelacionar" Visible=<%#VisibleSegunPerfil() %>  Runat=server CommandName='relacionar' CommandArgument='<%#"" & container.dataitem("CodigoMotobomba")& "#" & container.dataitem("FechaRevision")%>'><img src="../images/Asociar.gif" border=0 align=absmiddle alt="Relacionar Elementos"></asp:LinkButton>
                                                <asp:LinkButton ID=lbtDelete runat="server" Visible=<%#VisibleSegunPerfil() %> CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("CodigoMotobomba")& "#" & container.dataitem("FechaRevision") %>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar"></asp:LinkButton>
                	                        </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=9> 
                                                
                                  <uc:paginacion ID="ucPaginacion" runat="server" Visible=false />        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Motobombas -->
                        
                        <!-- Panel Editar  Motobombas-->
                        <asp:Panel ID="pnlEDMotobombas" Runat="server" Visible="false" BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px">
                        <asp:label id="lblMotobombaSel" runat="server"></asp:label>
                        <br /><asp:Label ID="lblTitulo" runat="server" CssClass="tituloSec"  Width="368px"><b> MANTENIMIENTO MOTOBOMBA: </b></asp:Label>
                            <table width="100%" cellspacing=0 cellpadding=1 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2 id="TABLE1" onclick="return TABLE1_onclick()">
                                <tr>
                                    <td style="height: 25px">Código Motobomba</td>
                                    <td style="width: 142px; height: 25px"><asp:TextBox ID=txtcodigoMotobomba runat="server" CssClass="texto"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcodigoMotobomba"
                                            Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                    <td style="height: 25px">Descripción</td>
                                    <td style="width: 139px; height: 25px;"><asp:TextBox ID="txtdescripcion" runat="server" CssClass="texto"></asp:TextBox></td>                             
                                    <td style="height: 25px">Fecha Revisión</td>
                                    <td style="height: 25px">
                                        <asp:TextBox ID="txtFecharevision" CssClass="texto" runat="server" Width="88px"></asp:TextBox>&nbsp;<asp:Image
                                            ID="imgFechaRevision" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" Visible="false" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtFecharevision" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtFecharevision"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date"></asp:CompareValidator>
                                        &nbsp;&nbsp;
                                    </td>                                    
                                    <td style="height: 25px">Código PVYCR</td>
                                    <td style="height: 25px" ><asp:TextBox ID="txtCodigoPVYCR" runat="server" CssClass="texto"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display=Dynamic ErrorMessage="*" ControlToValidate=txtCodigoPVYCR></asp:RequiredFieldValidator>
                                    </td>                                
                                </tr>     
                                <tr>
                                    <td style="height: 39px">Fecha Inicial</td>
                                    <td style="width: 142px; height: 39px"><asp:TextBox ID=txtFechaIni runat="server" CssClass="texto" Width="88px"></asp:TextBox>&nbsp;
                                        <asp:Image ID="imgFechaInicial" runat="server" align="absmiddle" ImageAlign="Left"
                                            ImageUrl="~/images/calendario.gif" Style="cursor: pointer" />
                                        <asp:CompareValidator ID="CompareValidator16" runat="server" ControlToValidate="txtFechaIni"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date"></asp:CompareValidator>
                                    </td>
                                    <td style="height: 39px">Fecha Fin</td>
                                    <td style="width: 139px; height: 39px;"><asp:TextBox ID=txtFechaFin runat="server" CssClass="texto" Width="88px"></asp:TextBox>
                                        <asp:Image ID="imgFechaFin" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" />
                                        &nbsp;&nbsp;<asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtFechaFin"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                    </td>                                                          
                                    <td style="height: 39px">Grupos Motobomba</td>
                                    <td style="height: 39px"><asp:TextBox ID=txtGMotobomba runat="server" CssClass="texto"></asp:TextBox>
                                    <asp:CompareValidator ID=ComGMotobomb runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtGMotobomba Operator=DataTypeCheck Type=Integer></asp:CompareValidator>
                                    </td>                                        
                                    <td style="height: 39px">Tipo Bomba</td>
                                    <td style="height: 39px"><asp:TextBox ID=txtTipoMotobomba runat="server" CssClass="texto"></asp:TextBox></td>                                        
                                </tr>                
                                <tr>
                                    <td>Marca Bomba</td>
                                    <td style="width: 142px"><asp:textbox ID=txtmarcaBomba runat="server" CssClass="texto"></asp:textbox></td>
                                    <td>Num. Serie Bomba</td>
                                    <td style="width: 139px"><asp:textbox ID=txtNSerieMotobomba runat="server" CssClass="texto" /></td>                              
                                    <td>Modelo Bomba</td>
                                    <td><asp:TextBox ID=txtModelo runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td>Qn Bomba</td>
                                    <td><asp:textbox ID=txtQnBomba runat="server" CssClass="texto"></asp:textbox>                      
                                    </td>                                     
                                </tr>
                                <tr>
                                    <td>Marca Motor</td>
                                    <td style="width: 142px"><asp:textbox ID=txtMarcaMotor runat="server" CssClass="texto"></asp:textbox></td>
                                    <td>Modelo Motor</td>
                                    <td style="width: 139px"><asp:textbox ID="txtModeloMotor" runat="server" CssClass="texto" /></td>                                                         
                                    <td>Tipo Motor (h/v)</td>
                                    <td><asp:textbox ID="txttipoMotor" runat="server" CssClass="texto"></asp:textbox></td>  
                                    <td>Num. Serie Motor</td>                              
                                    <td><asp:TextBox ID=txtNSerieMotobombaMotor runat="server" CssClass="texto"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>RPM</td>
                                    <td style="width: 142px"><asp:textbox ID="txtRPM" runat="server" CssClass="texto"></asp:textbox></td>  
                                    <td>Potencia (Cv)</td>                              
                                    <td style="width: 139px"><asp:TextBox ID=txtPotencia_CV runat="server" CssClass="textoNumerico"></asp:TextBox> 
                                    <asp:CompareValidator ID=CompareValidator3 runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtPotencia_CV Operator=DataTypeCheck Type=Integer></asp:CompareValidator></td> 
                                    <td>Potencia (Kw)</td>
                                    <td><asp:textbox ID="txtPotencia_kw" runat="server" CssClass="textoNumerico"></asp:textbox> 
                                    <asp:CompareValidator ID=CompareValidator17 runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtPotencia_CV Operator=DataTypeCheck Type=Integer></asp:CompareValidator></td> 
                                    <td>Tipo Pot.</td>                              
                                    <td><asp:TextBox ID=txttipoPot runat="server" CssClass="texto"></asp:TextBox></td>
                                </tr>       
                                <tr>
                                    <td>Potencia Teórica (kw)</td>
                                    <td style="width: 142px"><asp:textbox ID="txtPotenciaTeorica_kw" runat="server" CssClass="textoNumerico"></asp:textbox>  
                                    <asp:CompareValidator ID=CompareValidator2 runat="server"  Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtPotenciaTeorica_kw Operator=DataTypeCheck Type=Integer></asp:CompareValidator></td>
                                    <td>DDP Motor (v)</td>                              
                                    <td style="width: 139px"><asp:TextBox ID=txtDDPMotor runat="server" CssClass="textoNumerico"></asp:TextBox>                               
                                    <asp:CompareValidator ID=CompareValidator4 runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtDDPMotor Operator=DataTypeCheck Type=Integer></asp:CompareValidator></td> 
                                    <td>Intensidad Motor (A)</td>
                                    <td><asp:textbox ID="txtIntensidadMotor_A" runat="server" CssClass="texto"></asp:textbox></td> 
                                    <td>Cos. Fi.</td>                              
                                    <td><asp:TextBox ID=txtCosFi runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtCosFi"
                                             ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic">
                                             </asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td>Caudal (L/Seg)</td>
                                    <td style="width: 142px"><asp:TextBox ID=txtCaudal_LSeg runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCaudal_LSeg"
                                             ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic">
                                             </asp:RegularExpressionValidator>
                                    </td>
                                    <td>Tipo Caudal</td>
                                    <td style="width: 139px"><asp:TextBox ID=txtTipoCaudal runat="server" CssClass="texto"></asp:TextBox>
                                    </td>                                                                                   
                                    <td>Altura Geométrica Impulsión</td>
                                    <td><asp:TextBox ID=txtAlturaGeometrica runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:CompareValidator ID=CompareValidator8 runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtAlturaGeometrica Operator=DataTypeCheck  Type=Integer></asp:CompareValidator></td>
                                    <td>Altura Manométrica Teórica</td>
                                    <td><asp:TextBox ID=txtAlturaManometrica runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:CompareValidator ID=CompareValidator9 runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtAlturaManometrica Operator=DataTypeCheck Type=Integer></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td>Tipo Altura</td>
                                    <td style="width: 142px"><asp:TextBox ID=txtTipoAlt runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td style="height: 43px"> Profundidad Nivel Estático</td>
                                    <td style="height: 43px"><asp:TextBox CssClass="textoNumerico" ID="txtProfundidadEstatica" runat="server"></asp:TextBox>
                                    <asp:CompareValidator ID=CompEstatica runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtProfundidadEstatica Operator=DataTypeCheck Type=Integer></asp:CompareValidator>
                                    </td>
                                    <td style="height: 43px">Profundidad Nivel Dinámico</td>
                                    <td style="width: 139px; height: 43px;"><asp:TextBox ID=txtProfundidadDinamica runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:CompareValidator ID=CompareValidator7 runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtProfundidadDinamica Operator=DataTypeCheck Type=Integer></asp:CompareValidator> </td>
                                    <td style="height: 43px">Longitud Aspiración</td>
                                    <td style="height: 43px"><asp:TextBox ID=txtLongitudAspiracion runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:CompareValidator ID=CompareValidator10 runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtLongitudAspiracion Operator=DataTypeCheck Type=Integer></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td style="height: 43px">Dn Aspiración</td>
                                    <td style="height: 43px; width: 142px;"><asp:TextBox ID=txtDnAspiracion runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td>Material Tub Aspiración</td>
                                    <td><asp:TextBox ID=txtMaterialTubAsp runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td>Longitud Impulsión</td>
                                    <td style="width: 139px"><asp:TextBox ID=txtLongImpulsion runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:CompareValidator ID=CompareValidator11 runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtLongImpulsion Operator=DataTypeCheck Type=Integer></asp:CompareValidator> </td>
                                    <td>Dn Impulsión</td>
                                    <td><asp:TextBox ID=txtDnImpulsion runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:CompareValidator ID=CompareValidator12 runat="server" Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtDnImpulsion Operator=DataTypeCheck Type=Integer></asp:CompareValidator></td>
                                </tr>

                                <tr>                                   
                                                                                                     
                                    <td>Material Tub Impulsión</td>
                                    <td style="width: 142px"><asp:TextBox ID=txtMaterialtubImp runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td>
                                        Voltímetro</td>
                                    <td><asp:TextBox ID=txtVolimetro runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td>Amperimetro</td>
                                    <td style="width: 139px"><asp:TextBox ID=txtAmperimetro runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td>Manómetro (kg/cm2)</td>
                                    <td><asp:TextBox ID=txtManometro runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:CompareValidator ID=CompareValidator14 runat="server"  Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtManometro Operator=DataTypeCheck Type=Integer></asp:CompareValidator></td>
                                </tr>                                
                                <tr>                                    
                                    
                                    <td>Revoluciones/min</td>
                                    <td style="width: 142px"><asp:TextBox ID=txtRev10min runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:CompareValidator ID=ComRev10min Display=Dynamic runat="server" Text=? ErrorMessage="Sólo números" ControlToValidate=txtRev10min Operator=DataTypeCheck Type=Integer></asp:CompareValidator>
                                    </td>
                                    <td>Bomba en Func.</td>
                                    <td><asp:TextBox ID=txtBombaEnFuncionamiento runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:CompareValidator ID=CompareValidator15 Display=Dynamic runat="server" Text=? ErrorMessage="Sólo números" ControlToValidate=txtBombaEnFuncionamiento Operator=DataTypeCheck Type=Integer></asp:CompareValidator>     </td>                               
                                    <td>Cota Toma</td>
                                    <td style="width: 139px"><asp:TextBox ID=txtCotaToma runat="server" CssClass="texto"></asp:TextBox>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCotaToma"
                                             ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}">
                                             </asp:RegularExpressionValidator>
                                    </td>
                                    <td>Destino1</td>
                                    <td><asp:TextBox ID=txtDestino1 runat="server" CssClass="texto"></asp:TextBox></td>
                                </tr>                                
                                <tr>
                                    
                                    <td>Destino2</td>
                                    <td style="width: 142px"><asp:TextBox ID=txtDestino2 runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td>Destino3</td>
                                    <td><asp:TextBox ID=txtDestino3 runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td>Cota Destino1</td>
                                    <td style="width: 139px"><asp:TextBox ID=txtCotaDestino1 runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCotaDestino1"
                                             ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}">
                                             </asp:RegularExpressionValidator>
                                    <td>Cota Destino2</td>
                                    <td><asp:TextBox ID=txtCotaDestino2 runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCotaDestino2"
                                             ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}">
                                             </asp:RegularExpressionValidator>
                                    </td>
                                </tr>   
                                <tr>
                                    
                                    <td>Cota Destino3</td>
                                    <td style="width: 142px"><asp:TextBox ID=txtCotaDestino3 runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCotaDestino3"
                                             ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}">
                                             </asp:RegularExpressionValidator>
                                    </td>
                                    <td style="height: 30px">UTM Destino1</td>
                                    <td style="height: 30px"><asp:TextBox ID=txtUTMDestino1 runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td style="height: 30px">UTM Destino2</td>
                                    <td style="width: 139px; height: 30px;"><asp:TextBox ID=txtUTMDestino2 runat="server" CssClass="texto"></asp:TextBox></td>
                                    <td style="height: 30px">UTM Destino3</td>
                                    <td style="height: 30px"><asp:TextBox ID=txtUTMDestino3 runat="server" CssClass="texto"></asp:TextBox></td>
                                </tr>   
                                <tr>                                    
                                    <td style="vertical-align: middle; width: 3844px; text-align: left; height: 1px;">Observaciones</td><td style="width: 360px; vertical-align: middle; text-align: left; height: 1px;" colspan="8"><asp:TextBox ID="txtobservaciones" runat="server" TextMode="MultiLine" CssClass="texto" Width="854px" Height="80px"></asp:TextBox></td>
                                </tr>                                                                                                                   
                                <tr>
                                
                                    <td colspan="8" align="right" style="border-top:1px solid #666666; height: 24px;">
                                    <asp:Button ID="btnAceptar" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                    <asp:Button ID="btnCancelar" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>
                                    <asp:Button ID="btnListar" Runat="server" cssclass="boton" Text='Imprimir'></asp:Button>
                                    </td>
                                </tr>
                     
                               </table>           
                            </td>
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Contadores -->
                        
                        <!-- Panel Relacionar con Elementos de Medida -->
                        <asp:Panel ID=pnlRelacionarElemento Runat=server Visible=false>
                        <asp:label id=lblMotobombaRel runat=Server Visible=False></asp:label>
                        <asp:label id=lblFechaRevRel runat=Server Visible=False></asp:label>
                        <asp:label id=lblElementoRel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTituloRel runat=server CssClass=tituloSec><b>ELEMENTOS RELACIONADOS CON LA MOTOBOMBA: </b></asp:Label><br />
                        <table width=100% cellspacing=0 cellpadding=1 class="tablaRelacion">
                        <tr>
                          <td align="right"></td>
                        </tr>
                        <tr>                      
                        </tr>  
                        <tr>                      
                        </tr> 
                        <tr>
                        <td colspan="0" align="left"><asp:LinkButton runat="server" ID="lbtNuevaRelacion" Visible=<%#VisibleSegunPerfil() %> CausesValidation="false" OnClick="nuevaRelacion">&nbsp;Relacionar otro Elemento</asp:LinkButton></td>                     
                        </tr>                       
                        <td colspan=2 >
                            <table >
                                    <td class="encabListado">CódigoPVYCR</td>
                                    <td class="encabListado">Elemento</td>
                                    <td class="encabListado">&nbsp;</td>
                                    <asp:Repeater ID=rptHistorial runat=server>                                                     
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><%#Container.DataItem("CodigoPVYCR")%></td>
                                            <td><%#Container.DataItem("idElementoMedida")%></td>                                         
                                            <td nowrap align="right" width="36">
                                                <asp:LinkButton ID="lbtEditRel" Visible=<%#VisibleSegunPerfil() %>  Runat=server CommandName='editar' CommandArgument='<%#"" & container.dataitem("CodigoMotobomba")& "#" & container.dataitem("FechaRevision")& "#" & container.dataitem("idElementoMedida") & "#" & container.dataitem("CodigoPVYCR")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtDeleteRel" Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='borrar' OnClientClick='return confirmar_borrado_Historial();'   CausesValidation="false" CommandArgument='<%#"" & container.dataitem("CodigoMotobomba")& "#" & container.dataitem("FechaRevision")& "#" & container.dataitem("idElementoMedida") & "#" & container.dataitem("CodigoPVYCR")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar"></asp:LinkButton>
                	                        </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </td>
                            <tr></tr>
                            <tr>
                                    <td>
                                         <br />
                                        </td>
                                    <td style="width: 160px"></td>
                                    <td>
                                        <asp:Image ID="Image1" runat="server" align="absmiddle" ImageUrl="~/SICAH/images/iconos/icoBtnSistemas.GIF"
                                            Style="cursor: pointer" ToolTip="Seleccionar del Arbol" Visible="False" /></td>
                                    <td></td>  
                                </tr>
                            <td class="cabeceraRelacion" style="width: 1058px"  >
                            <asp:Panel ID="pnlElementosRelacionados" runat="server" Visible="true">     
                                <tr>Elementos de medida sin asignar</tr>       
                                <tr>                    
                                <asp:ListBox runat="server" ID="lstElementos" Width="200px" BackColor="#F9F6E7"  Font-Size="11px" Font-Names="Arial"   SelectionMode="Multiple"></asp:ListBox>
                                <asp:Button ID="btnAceptarElementos" Runat="server" cssclass="boton" Text='Añadir'></asp:Button>(Permite selección múltiple)
                                </tr>
                            </asp:Panel>
                            </td>
                            <td class="cabeceraRelacion" style="width: 1058px"  >
                            <asp:Panel ID="pnlRelacion" runat=server >
                                <table cellspacing=2 cellpadding=2>
                                <td nowrap >Elemento de Medida</td>
                                    <td><asp:TextBox ID="txtElementoMedida" runat="server" CssClass="texto" Enabled="false" ></asp:TextBox>
                                    <asp:Image ID="imgArbol" runat="server" align="absmiddle" ImageUrl="~/SICAH/images/iconos/icoBtnSistemas.GIF" Style="cursor: pointer" ToolTip="Seleccionar del Arbol el Elemento de Medida" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtElementoMedida" 
                                            Display="Dynamic" ErrorMessage="*" Width="3px" ></asp:RequiredFieldValidator>
                                 </td>                                      
                                 <td><asp:Button ID="btnAnyadir" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                 <asp:Button ID="btnCancelarRel" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>  
                                 </td>                                                                                  
                                </table>
                            </asp:Panel>  
                            </td>   
                            <tr></tr>                   
                            <tr>
                               <td colspan="10" align="right" style="border-top:1px solid #666666">
                                <asp:Button ID="btnSalir" Runat="server" cssclass="boton" Text='Salir' CausesValidation=False></asp:Button>
                               </td>
                            </tr>               
                        </table>
                        </asp:Panel>
                        <!-- Fin Panel Relacionar con Elementos de Medida -->
                        
                    </td>
                    <!-- Fin Celda Contenido -->        
                </tr>
                <!-- Fin Celda Menú - Contenido -->
                   

        
</table></td></tr   >
          </table> 
        
</td></tr></table>
    </form>
</body>
</html>
