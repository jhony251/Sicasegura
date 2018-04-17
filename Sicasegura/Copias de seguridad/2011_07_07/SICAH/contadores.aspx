<%@ Page Language="VB" AutoEventWireup="false" CodeFile="contadores.aspx.vb" Inherits="SICAH_contadores" %>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
         <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
     <link rel="stylesheet" href="..\styles.css">
     <link href="../styles.css" rel="stylesheet" />
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
     <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" src="../js/utilesMenus.js"></script>  
    

    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />

<script language="javascript" type="text/javascript">
<!--

    function TABLE1_onclick() 
    {
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
    
//    function ocultarDesplegables()
//    {
//        
//        var i;
//        document.getElementById('desplegableEM').style.display='none';
//        mostrarSelects(); 
//        
//    }
    function ocultarPanel(panel)
    {
        document.getElementById(panel).style.display='none';
        
    }
    function mostrarPanel(panel)
    {
        document.getElementById(panel).style.display='';
        
    }
    
    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación del contador?")==true)
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
    
    function Historial(){
    if (document.getElementById("pnlRelacion").style.display=="none"){
        document.getElementById("pnlRelacion").style.display="block" ;
                
    }else{
        document.getElementById("pnlRelacion").style.display="none" ;
        }
    }
    

    
   function desplegarArbol(sender)
    {
        var despArbol = document.getElementById('desplegableArbol');
        if (despArbol.style.display=='none'){
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
// -->
</script>
</head>
<body bgcolor="#EEEAD2">
    <form id="form1" runat="server" method=post>
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
                     <%=genHTMLMenus.generaMenuMtoSica(Session("idperfil"), Application("PVYCR_Arbol_Updated"))%>  
                    <!-- Fin Celda Menú -->                    
                    <!-- Celda Contenido -->
                     <td style="padding-left:20px; padding-right:20px; width:79%" valign=top>
                        <!-- Panel listar Contadores -->                      
                        <asp:Panel ID=pnlContadores Runat=server Visible=true>
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
                                </td>
                                </tr>
                                </table>                                
                                <table align="right">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnListadoPantalla" runat="server" Text="Listar Información" CssClass="boton" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFiltroCodigoPVYCR" runat="server" Text="[Código PVYCR]" CssClass="texto"></asp:TextBox>
                                    </td>
                                
                                </tr>
                                 <tr>
                                <td colspan="6"  align="right">Registros a mostrar  <asp:TextBox  ID="txtRegistros"  runat="server" Text="10" Width="30px"  CssClass="texto"></asp:TextBox><asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtRegistros"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                        <asp:Button runat="server" ID="btnVertodos" cssclass="boton" Text="Actualizar"  /></td>
                                </tr>

                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width=100% >
                         
                            <tr>
                                <td align="right" colspan="6" style=" vertical-align: top">
                                <asp:Label ID="lblNuevoCont" runat="server" Text="Nuevo Contador" ></asp:Label>                          
                                </td>
                                <td align="right" style="width: 50px;" >
                                    <asp:DropDownList ID="ddlTipoCalculo" runat="server" AutoPostBack="True" Font-Size="8pt"
                                         Width="152px" Visible=<%#VisibleSegunPerfil() %>>
                                        <asp:ListItem Value="0">[Seleccionar]</asp:ListItem>
                                        <asp:ListItem Value="Q">Caudal</asp:ListItem>
                                        <asp:ListItem Value="H">Horas</asp:ListItem>
                                        <asp:ListItem Value="E">Energ&#237;a</asp:ListItem>
                                        <asp:ListItem Value="V">Volumen</asp:ListItem>
                                    </asp:DropDownList>&nbsp;
                                </td>
                            </tr>
                            </table> 
                            <table width=100%>                                               
                              

                                    <tr><td class="tituloSec" colspan=4>MANTENIMIENTO DE CONTADORES</td></tr>
                                   <tr>
                                        <td  align="left" colspan="1"><a  id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a></td>
                                   </tr>
                                    <tr><td class="encabListado">Id Contador</td>
                                    <td class="encabListado">Fecha Revisión</td>                                        
                                    <%-- <td class="encabListado">Fecha Inicial</td>
                                    <td class="encabListado">Fecha Fin</td>
                                    <td class="encabListado">Cod. Caracterización</td>                                                                               
                                    <td class="encabListado">Nº Serie </td>          --%>                               
                                    <td class="encabListado">Tipo / Marca </td>  
                                    <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width=310px ID="ddlFiltrado" runat="server" AutoPostBack="false" ></asp:DropDownList></td>                                                                               
                                    <td class="encabListado">&nbsp;</td>
                                    </tr>
                                     <tr id="FilaFiltro"  >         
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFidcontador" CssClass="texto" Columns="23"></asp:TextBox></td>                                                  
                                         <td nowrap class="encabListado"  style="width:150px"><asp:TextBox runat="server" ID="txtFFechaRevision" CssClass="texto" style="width:74px" ></asp:TextBox>&nbsp;<asp:Image    ID="imgFFechaRevision" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" /><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtFFechaRevision"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                         </td> 
                                     <%--    <td nowrap class="encabListado"  style="width:150px"><asp:TextBox runat="server" ID="txtFFechaInicial" CssClass="texto" style="width:74px" ></asp:TextBox>&nbsp;<asp:Image    ID="imgFFechaInicial" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" /><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtFFechaInicial"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                         </td>  
                                         <td nowrap class="encabListado" style="width:150px"><asp:TextBox runat="server" ID="txtFFechaFin" CssClass="texto" style="width:74px" ></asp:TextBox>&nbsp;<asp:Image    ID="imgFFechaFin" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" /><asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtFFechaFin"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                         </td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFCodCaract" CssClass="texto" Columns="10"></asp:TextBox></td>  
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFNserieActiva" CssClass="texto" style="width:150px"></asp:TextBox></td> --%>
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFNSerieReactiva" CssClass="texto" Columns="23"></asp:TextBox></td>  
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFiltrado" CssClass="texto" Columns="50"></asp:TextBox></td>  
                                         <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsReg" onclick="AceptarFiltro" Runat="server" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                </tr> 
                                    <asp:Repeater ID=rptContadores runat=server>                                                     
                                    <ItemTemplate>
                                        <tr <%# checkFila("no")%>>
                                            <td><%#Container.DataItem("IdContador")%></td>
                                           <td><%#DataBinder.Eval(Container.DataItem, "FechaRevision", "{0:dd/MM/yyyy}")%></td>                                            
                                            <%-- <td><%#DataBinder.Eval(Container.DataItem, "fechaInicial", "{0:dd/MM/yyyy}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "FechaFin", "{0:dd/MM/yyyy}")%></td>
                                            <td><%#Container.DataItem("Codigocaracterizacion")%></td>                            --%>                    
                                           <td><%#ObtenerNumSerie(Container.DataItem)%></td> 
                                             <td><%#ObtenerFiltrado(Container.DataItem)%></td>                                                                      
                                            <td nowrap align=right width=56>
                                                <asp:LinkButton ID="lbtEdit" Visible=<%#VisibleSegunPerfil() %>  Runat=server CommandName='editar' CommandArgument='<%#"" & container.dataitem("IdContador")& "#" & container.dataitem("FechaRevision")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtRelacionar" Visible=<%#VisibleSegunPerfil() %>  Runat=server CommandName='relacionar' CommandArgument='<%#"" & container.dataitem("IdContador")& "#" & container.dataitem("FechaRevision") & "#" & container.dataitem("codigoPVYCR")%>'><img src="../images/Asociar.gif" border=0 align=absmiddle alt="Relacionar Elementos"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtDelete" Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("IdContador")& "#" & container.dataitem("FechaRevision")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar"></asp:LinkButton>
                	                        </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=7>               
                                  <!--<uc:paginacion ID="ucPaginacion" runat="server" />-->        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Contadores -->                        
                        <!-- Panel Editar  Contadores-->
                        <asp:Panel ID=pnlEDContadores Runat=server Visible=false>
                        <asp:label id=lblContadorSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec><b>CONTADOR: </b></asp:Label><br />
                            <table width=100% cellspacing=0 cellpadding=1 class="tablaEdicion">
                            <tr>                          
                            <td class="cabeceraMTO" style="width: 1058px">
                            <!-- Panel Editar Cabecera Contadores-->
                            
                           <asp:Panel ID="pnlEDcabecera" runat=server Visible=false>
                                <table cellspacing=2 cellpadding=2>
                                <!-- Fila Cabecera Mantenimiento -->
                                <tr>                                    
                                    <td nowrap>Id Contador</td>
                                    <td><asp:TextBox ID="txtidContador" runat="server" CssClass="texto"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td >Fecha Revisión</td>
                                    <td><asp:TextBox ID=txtFecharevision CssClass=texto runat=server></asp:TextBox>
                                    <asp:CompareValidator ID=cvFechaRevision runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFechaRevision Operator=DataTypeCheck Type=date Display=Dynamic ></asp:CompareValidator>            
                                    <asp:RequiredFieldValidator ID="RQ1" runat="server" Text="*" ErrorMessage="Valor Obligatorio" ControlToValidate="txtFechaRevision" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>                                
                                    <td nowrap>Código PVYCR</td>
                                    <td ><asp:TextBox ID=txtCodigoPVYCR runat=server CssClass=texto></asp:TextBox>
                                    <asp:RequiredFieldValidator ID=RequiredFieldValidator4 runat=server Display=Dynamic ErrorMessage="El código PVYCR es obligado" ControlToValidate=txtCodigoPVYCR></asp:RequiredFieldValidator>
                                    </td>                                    
                                    <td>Código Caracterización</td>
                                    <td ><asp:TextBox ID=txtCodigoCaracterizacion runat=server CssClass=texto></asp:TextBox>
                                </tr> 
                                <tr>
                                    <td >Fecha Inicial</td>
                                    <td><asp:TextBox ID=txtFechaIni runat=server CssClass=texto></asp:TextBox>
                                    <asp:CompareValidator ID=cvfechaini runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtfechaini Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                    </td>
                                    <td >Fecha Fin</td>
                                    <td><asp:TextBox ID=txtFechaFin runat=server CssClass=texto></asp:TextBox>
                                    <asp:CompareValidator ID=cvfechafin runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtfechafin Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                    </td>                                                               
                                    <td nowrap>Tipo Contador</td>
                                    <td><asp:TextBox ID=txtTipoCont runat=server CssClass=texto></asp:TextBox></td>                                        
                                    <td nowrap>Acceso a Contador</td>
                                    <td><asp:CheckBox ID=chkAccesoContador runat=server CssClass=texto /></td>                                
                                </tr>                
                                <tr>
                                    <td nowrap>Aforable</td>
                                    <td><asp:textbox ID=txtAforable runat=server CssClass=texto /></td>
                                    <td>Distancia Pto Aforo</td>
                                    <td><asp:textbox ID=txtdPtoAForo runat=server CssClass=textoNumerico></asp:textbox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtdPtoAForo"
                                            ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </td>                                                                    
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>                                    
                                </tr>
                                <tr>                                
                                    <td nowrap >Descripción</td>
                                    <td colspan="6"><asp:textbox ID="txtDescripcion" runat=server CssClass="texto"  MaxLength="255" TextMode="MultiLine" width="640"/></td>
                                </tr>
                                </table>
                            </asp:Panel>
                            </td>
                            </tr>
                            <tr>
                            <td style="width: 1058px">
                            <!-- Panel Editar Contadores Tipo V-->
                            <asp:Panel ID="pnlEDTipoV" runat="server" Visible=false>
                                <table cellspacing=2 cellpadding=2 width=100% style="width: 100%" >           
                                <tr>
                                    <td>Número de Serie</td>
                                    <td><asp:TextBox ID=txtNSerie runat="server" CssClass=texto></asp:TextBox></td>
                                    <td>Marca</td>
                                    <td><asp:textbox ID=txtmarca runat=server CssClass=texto></asp:textbox>                      
                                    </td>                                     
                                    <td>Factor de Fabricación Contador Volúmen</td>
                                    <td><asp:textbox ID=txtContvol runat=server CssClass=texto></asp:textbox></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>                                
                                </tr>
                                <tr>
                                    <td colspan="10" align="right" style="border-top:1px solid #666666">
                                    <asp:Button ID="btnAceptarV" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                    <asp:Button ID="btnCancelarV" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>
                                    <asp:Button ID="btnListarV" Runat="server" cssclass="boton" Text='Imprimir' CausesValidation=False></asp:Button>
                                    </td>
                                </tr>                                
                                </table>
                            </asp:Panel>
                            <!-- Panel Editar Contadores Tipo E-->
                            <asp:Panel ID="pnlEDTipoE" runat="server" Visible=false>
                                <table cellspacing=2 cellpadding=2 width=100%>                                   
                                <tr>
                                    <td></td>
                                    <td width=25%>Referencia contrato<br /><asp:textbox ID="txtRefContrato" runat="server" CssClass=texto /></td>
                                    <td></td>
                                    <td width=25%>Potencia Contratada<br /><asp:textbox ID="txtPoContratada" runat="server" CssClass="texto"></asp:textbox></td>  
                                    <td></td>                              
                                    <td width=25%>Tarifa Contratada<br /><asp:TextBox ID=txttarifaContratada runat=server CssClass="texto"></asp:TextBox></td>                                
                                    <td></td>
                                    <td width=25%>Tarifa Discriminación Horaria<br /><asp:textbox ID="txtTDHoraria" runat="server" CssClass="texto"></asp:textbox></td>  
                                </tr>
                                <tr>
                                    <td></td>                              
                                    <td>Complementa Reactiva<br /><asp:TextBox ID=txtComplReactiva runat=server CssClass="texto"></asp:TextBox></td>                                                              
                                    <td></td>
                                    <td>CT (kwa)<br /><asp:textbox ID="txtCT" runat="server" CssClass="texto"></asp:textbox></td>
                                    <asp:CompareValidator ID=cvct runat=server Text=* ErrorMessage="Sólo números" ControlToValidate=txtct Operator=DataTypeCheck Type=Integer></asp:CompareValidator>
                                    <td></td>                                 
                                    <td>Tipo Contador Activa<br /><asp:TextBox ID=txtTipoConActiva runat=server CssClass="texto"></asp:TextBox></td>                                 
                                    <td></td>
                                    <td>Nº Serie Contador Activa<br /><asp:textbox ID="txtNSCActiva" runat="server" CssClass="texto"></asp:textbox></td>  
                                </tr>
                                <tr>
                                    <td></td>                              
                                    <td>Nº Serie Contador Reactiva<br /><asp:TextBox ID=txtNSCReactiva runat=server CssClass="texto"></asp:TextBox></td>                                                                
                                    <td></td>
                                    <td>Intensidad Contador Activa<br /><asp:textbox ID="txtICActiva" runat="server" CssClass="texto"></asp:textbox></td>                                 
                                    <td></td>                              
                                    <td>Tensión Contador Activa<br /><asp:TextBox ID=txtTCActiva runat=server CssClass="texto"></asp:TextBox></td>                                
                                    <td></td>
                                    <td>Constante Contador Activa<br /><asp:textbox ID="txtCCActivo" runat="server" CssClass="texto"></asp:textbox></td>  
                                </tr>
                                <tr>
                                    <td></td>                              
                                    <td>Factor de Fabricación Contador Activa<br /><asp:TextBox ID=txtFFCActiva runat=server CssClass="texto"></asp:TextBox></td>                                                                
                                    <td></td>
                                    <td>Factor de Corrección Contador Activa<br /><asp:textbox ID="txtFCCActiva" runat="server" CssClass="textoNumerico"></asp:textbox>
                                    <asp:CompareValidator ID=cvFCCActiva runat=server Text=* ErrorMessage="Sólo números" ControlToValidate=txtFCCActiva Operator=DataTypeCheck Type=Integer></asp:CompareValidator>                                                                  
                                    </td> 
                                    <td></td>                              
                                    <td>Relación (m3/kwh)<br /><asp:TextBox ID="txtRelacion" runat="server" CssClass="textoNumerico"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"  ControlToValidate="txtRelacion" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d{0,3}){0,1}" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </td>                                
                                    <td></td>
                                    <td>Tipo Relación (m3/kwh)<br /><asp:TextBox ID=txtTiporelacion runat=server CssClass="textoNumerico" MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" align="right" style="border-top:1px solid #666666">
                                    <asp:Button ID="btnAceptarE" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                    <asp:Button ID="btnCancelarE" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>
                                    <asp:Button ID="btnListarE" Runat="server" cssclass="boton" Text='Imprimir' CausesValidation=False></asp:Button>
                                    </td>
                                </tr>
                     
                                </table>  
                                </asp:Panel>  
                            <!-- Panel Editar Contadores Tipo H-->
                            <asp:Panel ID="pnlEDtipoH" runat="server" Visible=false>
                                <table cellspacing=2 cellpadding=2 width=100%>           
                                <tr>
                                    <td>Número de Serie</td>
                                    <td><asp:TextBox ID=txtNSerieH runat="server" CssClass=texto></asp:TextBox></td>
                                    <td>Marca</td>
                                    <td><asp:textbox ID=txtMarcaH runat=server CssClass=texto></asp:textbox>                      
                                    </td>     
                                    <td>Modelo</td>
                                    <td><asp:textbox ID=txtModeloH runat=server CssClass=texto></asp:textbox>                      
                                    </td>                                
                                    <td>Factor de Fabricación Contador Horómetro</td>
                                    <td><asp:textbox ID=txtContvolH runat=server CssClass=texto></asp:textbox></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>                                
                                </tr>
                                <tr>
                                    <td colspan="8" align="right" style="border-top:1px solid #666666">
                                    <asp:Button ID="btnAceptarH" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                    <asp:Button ID="btnCancelarH" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>
                                    <asp:Button ID="btnListarH" Runat="server" cssclass="boton" Text='Imprimir' CausesValidation=False></asp:Button>
                                    </td>
                                </tr>                                
                                </table>
                            </asp:Panel>
                            <!-- Panel Editar Contadores Tipo Q-->
                            <asp:Panel ID="pnlEDTipoQ" runat="server" Visible=false>
                                <table cellspacing=2 cellpadding=2 width=100%>           
                                <tr>
                                    <td>Número de Serie</td>
                                    <td><asp:TextBox ID=txtNSerieQ runat="server" CssClass=texto></asp:TextBox></td>
                                    <td>Marca</td>
                                    <td><asp:textbox ID=txtMarcaQ runat=server CssClass=texto></asp:textbox>                      
                                    </td>                                     
                                    <td>Modelo</td>
                                    <td>
                                        <asp:textbox ID=txtModeloQ runat=server CssClass=texto></asp:textbox>
                                    </td>
                                    <td>Unidades</td>
                                    <td><asp:textbox ID=txtUnidadesQ runat=server CssClass=textoNumerico></asp:textbox>
                                    <asp:CompareValidator ID=cvUnidadesQ runat=server Text=* ErrorMessage="Sólo números" ControlToValidate=txtUnidadesQ Operator=DataTypeCheck Type=Integer Display=Dynamic></asp:CompareValidator>
                                    </td>                                    
                                    <td>Factor de Fabricación Contador Caudal</td>
                                    <td><asp:textbox ID=txtContvolQ runat=server CssClass=texto></asp:textbox></td>
                                </tr>
                                
                                <tr>
                                    <td colspan="10" align="right" style="border-top:1px solid #666666">
                                    <asp:Button ID="btnAceptarQ" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                    <asp:Button ID="btnCancelarQ" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>
                                    <asp:Button ID="btnListarQ" Runat="server" cssclass="boton" Text='Imprimir' CausesValidation=False></asp:Button>
                                    </td>
                                </tr>                                
                                </table>
                            </asp:Panel>
                            <!--Panel de Asociacion a un Elemento de Medida-->
                            <asp:Panel ID="pnlAsociaEM" runat="server" Visible="false">
                                <table cellspacing=2 cellpadding=2>                                   
                                <tr>
                                    <td>
                                        Elemento<br />
                                        de Medida</td>
                                    <td style="width: 160px"><asp:textbox ID="Textbox1" runat="server" CssClass=texto Width="252px" /></td>
                                    <td>
                                        <asp:Image ID="imgEM" runat="server" align="absmiddle" ImageUrl="~/SICAH/images/iconos/icoBtnSistemas.GIF"
                                            Style="cursor: pointer" ToolTip="Seleccionar del Arbol" Visible="False" /></td>
                                    <td></td>  
                                </tr>
                                <tr>
                                    <td colspan="4" align="right" style="border-top:1px solid #666666">
                                    <asp:Button ID="Button1" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                    <asp:Button ID="Button2" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>
                                    </td>
                                </tr>
                     
                                </table>  
                                </asp:Panel>              
                            </td>
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Contadores -->
                        <!-- Panel Relacionar con Elementos de Medida -->
                        <asp:Panel ID=pnlRelacionarElemento Runat=server Visible=false  >
                        <asp:label id=lblContadorRel runat=Server Visible=False></asp:label>
                        <asp:label id=lblFechaRevRel runat=Server Visible=False></asp:label>
                        <asp:label id=lblElementoRel runat=Server Visible=False></asp:label>
                        <asp:label id="lblCodigoPVYCR" runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTituloRel runat=server CssClass=tituloSec><b>ELEMENTOS RELACIONADOS CON EL CONTADOR: </b></asp:Label><br />
                        <table width=100% cellspacing=0 cellpadding=1 class="tablaRelacion">
                        <tr>
                          <td align="right"></td>
                        </tr>
                        <tr>                      
                        </tr>  
                        <tr>                      
                        </tr> 
                        <tr>
                        <td colspan="0" align="left"><asp:LinkButton runat="server" ID="lbtNuevaRelacion" Visible=<%#VisibleSegunPerfil() %> CausesValidation="false" OnClick="nuevaRelacion">&nbsp;Relacionar Elemento</asp:LinkButton></td>                     
                        </tr>                       
                        <td colspan=2 >
                            <table >
                                    <td class="encabListado">CódigoPVYCR</td>
                                    <td class="encabListado">Elemento</td>
                                    <td class="encabListado">Fecha Inicial</td>
                                    <td class="encabListado">Fecha Fin</td>
                                     <td class="encabListado">&nbsp;</td>
                                     <asp:Repeater ID=rptHistorial runat=server>                                                     
                                    <ItemTemplate>
                                        <tr <%# checkFila(DataBinder.Eval(Container.DataItem, "FechaFinal", "{0:dd/MM/yyyy}"))%>>
                                            <td><%#Container.DataItem("CodigoPVYCR")%></td>
                                            <td><%#Container.DataItem("idElementoMedida")%></td>                                         
                                            <td><%#DataBinder.Eval(Container.DataItem, "fechaInicio", "{0:dd/MM/yyyy}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "FechaFinal", "{0:dd/MM/yyyy}")%></td>                                                                                                        
                                            <td nowrap align="right" width="36">
                                                <asp:LinkButton ID="lbtEditRel" Visible=<%#VisibleSegunPerfil() %>  Runat=server CommandName='editar' CommandArgument='<%#"" & container.dataitem("IdContador")& "#" & container.dataitem("FechaRevision")& "#" & container.dataitem("idElementoMedida") & "#" & container.dataitem("CodigoPVYCR")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtDeleteRel" Visible=<%#VisibleSegunPerfil() %> Runat=server CommandName='borrar' OnClientClick='return confirmar_borrado_Historial();'   CausesValidation="false" CommandArgument='<%#"" & container.dataitem("IdContador")& "#" & container.dataitem("FechaRevision")& "#" & container.dataitem("idElementoMedida") & "#" & container.dataitem("CodigoPVYCR")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar"></asp:LinkButton>
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
                            <asp:Panel ID="pnlRelacion" runat=server >
                                <table cellspacing=2 cellpadding=2>
                                <td nowrap >Elemento de Medida</td>
                                    <td><asp:TextBox ID="txtElementoMedida" runat="server" CssClass="texto" Enabled="false" ></asp:TextBox>
                                    <asp:Image ID="imgArbol" runat="server" align="absmiddle" ImageUrl="~/SICAH/images/iconos/icoBtnSistemas.GIF" Style="cursor: pointer" ToolTip="Seleccionar del Arbol el Elemento de Medida" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtElementoMedida" 
                                            Display="Dynamic" ErrorMessage="*" Width="3px" ></asp:RequiredFieldValidator>
                                 </td>
                                 <td >Fecha Inicial</td>
                                 <td><asp:TextBox ID="txtFIniRel" runat=server CssClass=texto></asp:TextBox>
                                    <asp:CompareValidator ID=CompareValidator4 runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFIniRel Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                 </td>
                                 <td >Fecha Fin</td>
                                    <td><asp:TextBox ID="txtFFinRel" runat=server CssClass=texto></asp:TextBox>
                                    <asp:CompareValidator ID=CompareValidator5 runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFFinRel Operator=DataTypeCheck Type=date></asp:CompareValidator>
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

                         <td> 
                         <asp:Panel  BackColor="whiteSmoke" BorderWidth="1"  BorderColor="Gray" runat="Server">
                         <table >
                             <tr>  
                             <br /><asp:Label ID="lblHistorial" runat=server CssClass=tituloSec ><b> Historial de contadores del punto: </b></asp:Label><br />
                             </tr> 

                            <td class="encabListado" >CódigoPVYCR</td>
                            <td class="encabListado">Elemento</td>
                            <td class="encabListado" >Contador</td>
                            <td class="encabListado">Fecha Revisión</td>                            
                            <td class="encabListado" >Fecha Inicial</td>
                            <td class="encabListado">Fecha Fin</td>
                            <asp:Repeater ID="rptHistorialElemento" runat="server">                                                     
                            <ItemTemplate>
                                <tr <%# checkFila(DataBinder.Eval(Container.DataItem, "FechaFinal", "{0:dd/MM/yyyy}"))%> >
                                    <td ><%#Container.DataItem("CodigoPVYCR")%></td>
                                    <td><%#Container.DataItem("idElementoMedida")%></td> 
                                    <td><%#Container.DataItem("idContador")%></td> 
                                    <td><%#DataBinder.Eval(Container.DataItem, "FechaRevision", "{0:dd/MM/yyyy}")%></td>                                        
                                    <td><%#DataBinder.Eval(Container.DataItem, "fechaInicio", "{0:dd/MM/yyyy}")%></td>
                                    <td><%#DataBinder.Eval(Container.DataItem, "FechaFinal", "{0:dd/MM/yyyy}")%></td>                                                                                                        
                                </tr>
                                <tr>
                                    <td colspan="15">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td style="border-top: solid 2px #BABA74; font-size: 2px">
                                            &nbsp;</td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                            </ItemTemplate>
                            </asp:Repeater>                            
                                                 
                        </table>  
                    </asp:Panel>
                    
                    
                        </td>  
                                   
                        </table>
                        <table>
                        <tr></tr>
                        <tr><td colspan="3">Se han resaltado los contadores que están vigentes actualmente</td> </tr>  
                        </table>
                                           
                    </asp:Panel>
                        <!-- Fin Panel Relacionar con Elementos de Medida -->
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
