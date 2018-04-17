<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BoletinGuarderia.aspx.vb" Inherits="SICAH_BoletinGuarderia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />    
   <link rel="stylesheet" href="../js/LightBox2/css/lightbox.css" type="text/css" media="screen" />

    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
    <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
    <script type="text/jscript" language="javascript" src="../js/utiles.js"></script>
    <script type="text/jscript" language="javascript" src="../js/utilesInterfazDinamica.js"></script>
    
    
    <script language="javascript" type="text/javascript">
 

// <!CDATA[

    function mostrar_panel_imagenes()
    {
        var despImagenes = document.getElementById('pnlEDImagenes');
       if (despImagenes.style.display=='none')
       {
        despImagenes.style.display='';
       }
       else
       {
        despImagenes.style.display='none';
       }
    }
    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación del Boletín de Guardería?")==true)
        return true;
      else
        return false;
    }
    
    function confirmar_borrado_imagenes()
    {
      if (confirm("¿Está seguro de la eliminación de la imagen?")==true)
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
                despArbol.style.left = '' + (event.clientX-event.offsetX-elementoOrigen.offsetLeft+elementoOrigen.parentElement.offsetLeft - 500+ document.body.scrollLeft) + 'px';
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
<body bgcolor="#EEEAD2">
    <form id="form1" runat="server">  
        <div id="desplegableArbol" style="position:absolute;width:331px;padding:2px;background-color:white;border:1px solid #8C8B83;display:none; left: 338px; top: 359px; height: 310px;">
        
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
    <td valign="top" style="padding-top: 20px; width:20%">
        <%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%>
    </td>
    <!-- línea vertical que separa el menú de los datos -->
    <td style="background-image:url(../images/dot2.gif);width:3px"></td>
    <!-- Fin Celda Menú -->
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width: 1090px;" valign=top>
                        <!-- Panel Listar Seguimientos -->                      
                        
                          <asp:Panel ID="pnlBoletines" Runat="server" Visible="true">
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
                            <table width="100%">                                                                   
                                    <tr><td class="tituloSec" colspan=6>MANTENIMIENTO DE BOLETINES</td></tr>
                                    <tr><td  align="left" colspan="1"><a  id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a></td>
                                        <td colspan="7" align="right"><asp:LinkButton runat="server" ID="lbtNuevo" Visible="<%#VisibleSegunPerfil() %>" OnClick="nuevoBoletin">&nbsp;Nuevo Boletin</asp:LinkButton></td>
                                   </tr>
                                    <tr>                                        
                                    <td class="encabListado" >
                                        Referencia</td>
                                    <td class="encabListado" >
                                        Codigo PVYCR</td>  
                                        <td class="encabListado" style="width: 138px">
                                            &nbsp;Fecha</td>
                                    <td class="encabListado" >Tipo</td>
                                    <td class="encabListado" >Zona Guarderia</td>   
                                    <td class="encabListado" >&nbsp;Hechos</td>
                                    <td class="encabListado" >Inc. Detectada Por</td> 
                                        <td class="encabListado" >
                                        </td>
                                    </tr> 
                                   <tr id="FilaFiltro">         
                                         <td class="encabListado" valign="top" style="height: 25px"><asp:TextBox runat="server" ID="txtFNumRef" CssClass="texto" Columns="13"></asp:TextBox></td>                                                  
                                         
                                         
                                         <td class="encabListado" valign="top" style="height: 25px">
                                         <table cellpadding="0" cellspacing="0">
                                         <tr>
                                            <td>
                                                 <asp:TextBox runat="server" ID="txtFCodigoPVYCR" CssClass="texto" Columns="13" Width="101px"></asp:TextBox>
                                 
                                                </td>
                                            <td>
                                            
                                                <asp:Image ID="imgFArbol" runat="server" align="absmiddle" ImageAlign="Left"
                                                            ImageUrl="~/SICAH/images/iconos/icoBtnSistemas.GIF" Style="cursor: pointer" Visible="False" />
                                             
                                            </td>
                                         </tr>
                                         </table>
                                                            
                                                            
                                                            
                                                            </td>   
                                       <td class="encabListado" valign="top" style="width: 138px; height: 25px;">
                                           <table>
                                               <tr>
                                                   <td style="width: 4px">
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
                                                   <td style="width: 4px">
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
                                         <td class="encabListado" style="width:150px; text-align: left; height: 25px;" valign="top"><asp:DropDownList ID="ddlFTipoSuceso" runat="server"  Font-Size="8pt" Width="161px" CssClass="texto"></asp:DropDownList></td>  
                                         <td class="encabListado" style="width:75px; height: 25px" valign="top"><asp:TextBox ID="txtFZona" runat="server" CssClass="texto" Width="75px"></asp:TextBox></td>
                                         <td class="encabListado" align="left" valign="top" style="height: 25px">
                                           <table>
                                               <tr>                                                   
                                                   <td style="width: 4px" colspan="2"><asp:TextBox ID="txtFHechos" runat="server" CssClass="texto" Width="290px"></asp:TextBox></td>
                                                  
                                               </tr>
                                               <tr>                                                  
                                                   <td style="width: 4px"><asp:CheckBox ID="chkGuarderia" runat="server" EnableViewState="true"  Text="Guarderia" TextAlign=Right /></td>
                                                   <td style="width: 4px"><asp:CheckBox ID="chkPVYCR" runat="server" EnableViewState="true"  Text="PVYCR" TextAlign=Right /></td>                                               </tr>
                                           </table>
                                             
                                          </td> 
                                       <td class="encabListado" style="width:150px; text-align: left; height: 25px;" valign="top"><asp:TextBox ID="txtFIncidencia" runat="server" CssClass="texto" Width="70px"></asp:TextBox></td>
                                       <td align="right" class="encabListado" valign="top" style="height: 25px">
                                           <asp:LinkButton id="lbtAceptarUsReg" Runat="server" onclick="AceptarFiltro" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td>
                                    </tr>                                       
                                    <asp:Repeater ID="rptBoletines" runat="server">   
                                        <ItemTemplate>
                                            <tr <%# checkFila()%>>
                                                <td><%#Container.DataItem("NumRef")%></td>
                                                <td><%#Container.DataItem("PVYCRRef")%></td>
                                                <td><%#DataBinder.Eval(Container.DataItem, "Fecha", "{0:dd/MM/yyyy}")%></td>  
                                                <td><%#Container.DataItem("Denominacion") %></td>                                          
                                                <td><%#Container.DataItem("ZonaGuarderia") %></td>                                          
                                                <td><%#Container.DataItem("Hechos")%></td>
                                                <td><%#Container.DataItem("IncDetectadaPor")%></td>
                                                
                                                <td  align="left">    
                                                    <table cellspacing="4">   
                                                        <tr>   
                                                            <td>                               
                                                                <asp:LinkButton ID="lbtEdit" Visible="<%# VisibleSegunPerfil() %>" Runat="server" CommandName='editar' CommandArgument='<%# container.dataitem("NumRef")%>'><img src="../images/edit.gif" border=0 align="left" alt="Editar datos"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lbtDelete" Visible="<%# VisibleSegunPerfil() %>" Runat="server" CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("NumRef")%>'><img src="../images/del.gif" border=0 align="left" alt="Borrar"></asp:LinkButton>
                	                                        </td>
                	                                    </tr> 
                	                                </table>   
                	                            </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=8>                                 
                                   
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar seguimientos -->
                        
                        
                        <!-- Panel Editar  Seguimientos-->
                        <asp:Panel ID="pnlEDBoletines" Runat="server" Visible="false" BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px">
                        <asp:label id="lblNumRefSel" runat="server" Visible="False"></asp:label>&nbsp;
                            <br /><asp:Label ID="lblTitulo" runat=server CssClass=tituloSec Height="1px" Width="473px"><b> EDICIÓN DE BOLETIN: </b></asp:Label><br />
                            <table cellspacing=0 cellpadding=1 class="tablaEdicion">
                            <tr>
                            <td style="width: 525px; height: 111px">
                                <table cellspacing=2 cellpadding=2 id="TABLE1">
                                    <tr>
                                        <td colspan="5" style="border-bottom: silver thin inset" valign="middle">
                                        </td>
                                        <td style="width: 595px" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="border-bottom: silver thin inset" valign="middle">
                                            <strong>Datos Generales</strong></td>
                                        <td style="width: 595px" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px; height: 70px;" valign="top">
                                            Hechos</td>
                                        <td colspan="3" valign="top" style="height: 70px">
                                            <asp:TextBox ID="txtHechos" runat="server" CssClass="texto" Height="80px" TextMode="MultiLine"
                                                Width="627px"></asp:TextBox></td>
                                        <td style="width: 186px; height: 70px;" valign="top">
                                            <table>
                                                <tr>
                                                    <td colspan="4" style="height: 15px" valign="middle">
                                            <table cellpadding="0" cellspacing="1">
                                                <tr>
                                                    <td align="right" style="width: 29px; height: 21px">
                                                        Fecha
                                                    </td>
                                                    <td style="width: 87px; height: 21px">
                                                        <asp:TextBox ID="txtFecha" runat="server" CssClass="texto" Height="13px"
                                                            Width="87px"></asp:TextBox></td>
                                                    <td style="width: 20px; height: 21px">
                                                        <asp:Image ID="imgFecha" runat="server" align="absmiddle" ImageAlign="Left"
                                                            ImageUrl="~/images/calendario.gif" Style="cursor: pointer" /></td>
                                                    <td style="width: 43px; height: 21px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFecha"
                                                            ErrorMessage="*" Width="1px" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                                </tr>
                                            </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 101px" valign="middle">
                                            Fotos</td>
                                                    <td style="width: 100px" valign="middle">
                                            <asp:CheckBox ID="chkFotos" runat="server" CssClass="texto" Width="71px" /></td>
                                                    <td style="width: 100px" valign="middle">
                                            Croquis</td>
                                                    <td style="width: 100px" valign="middle">
                                                    <asp:CheckBox ID="chkCroquis" runat="server" CssClass="texto" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 101px; height: 15px" valign="middle">
                                            Planos</td>
                                                    <td style="width: 100px; height: 15px" valign="middle">
                                                        <asp:CheckBox ID="chkPlanos" runat="server" CssClass="texto" Width="77px" /></td>
                                                    <td style="width: 100px; height: 15px" valign="middle">
                                                        Denuncia</td>
                                                    <td style="width: 100px; height: 15px" valign="middle">
                                            <asp:CheckBox ID="chkDenun" runat="server" CssClass="texto" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 595px; height: 70px;" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px; height: 24px" valign="top">
                                            Referencia</td>
                                        <td colspan="4" style="height: 24px" valign="top">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 100px; height: 34px" valign="top">
                                        <asp:TextBox ID="txtNumRef" runat="server" Columns="13" CssClass="texto" Width="125px" MaxLength="15">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNumRef" runat="server" Text="*" ControlToValidate="txtNumRef" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                                    <td align="right" style="width: 76px; height: 34px" valign="top">
                                                        S / Ref</td>
                                                    <td style="width: 75px; height: 34px" valign="top">
                                                    <asp:TextBox ID="txtSRef" runat="server" Columns="13" CssClass="texto" Width="142px" MaxLength="50"></asp:TextBox></td>
                                                    <td align="right" style="width: 63px; height: 34px" valign="top">
                                            PVYCRRef</td>
                                                    <td style="width: 103px; height: 34px" valign="top">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 87px; height: 21px">
                                                    <asp:DropDownList id="ddlPVYCRRef" runat="server" Width="248px" AutoPostBack=true OnTextChanged="recuperarDatosBoletin" CssClass="texto" Font-Size="8pt" CausesValidation="false">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtPVYCRRef" visible="false" runat="server" Columns="13" CssClass="texto" Width="178px"></asp:TextBox></td>
                                                    <td style="width: 20px; height: 21px">
                                                        <asp:Image ID="imgArbol" runat="server" align="absmiddle" ImageAlign="Left"
                                                            ImageUrl="~/SICAH/images/iconos/icoBtnSistemas.GIF" Style="cursor: pointer" Visible="False" /></td>
                                                </tr>
                                            </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 595px; height: 24px" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px; height: 14px" valign="top">
                                            Tipo</td>
                                        <td style="width: 7px; height: 14px;" valign="top">
                                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="texto" Font-Size="8pt"
                                                Width="248px" CausesValidation="false">
                                            </asp:DropDownList></td>
                                        <td style="height: 14px" align="left" valign="top" colspan="3">
                                            <table cellpadding="0" cellspacing="2">
                                                <tr>
                                                    <td align="right" style="width: 213px; height: 19px">
                                            Zona Guardería</td>
                                                    <td style="width: 100px; height: 19px;">
                                            <asp:TextBox ID="txtZonaGuarderia" runat="server" Columns="13" CssClass="texto" Width="100px" MaxLength="50"></asp:TextBox></td>
                                                    <td align="right" style="width: 45px; height: 19px;">
                                            Hoja</td>
                                                    <td style="width: 100px; height: 19px;">
                                            <asp:TextBox ID="txtHoja" runat="server" CssClass="texto" Height="13px" Width="67px" MaxLength="50"></asp:TextBox></td>
                                                    <td align="right" style="width: 100px; height: 19px">
                                            UTM-X</td>
                                                    <td style="width: 110px; height: 19px">
                                            <asp:TextBox ID="txtutm_x" runat="server" CssClass="texto" Height="13px" Width="75px" MaxLength="10"></asp:TextBox></td>
                                                    <td style="width: 56px; height: 19px">
                                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtutm_x"
                                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                                            Type="Integer" Width="8px"></asp:CompareValidator></td>
                                                    <td align="right" style="width: 100px; height: 19px">
                                            UTM-Y</td>
                                                    <td style="width: 100px; height: 19px">
                                            <asp:TextBox ID="txtutm_y" runat="server" CssClass="texto" Height="13px" Width="80px" MaxLength="50"></asp:TextBox></td>
                                                    <td style="width: 56px; height: 19px">
                                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtutm_y"
                                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                                            Type="Integer" Width="8px"></asp:CompareValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 595px; height: 14px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px; height: 32px;">
                                            Paraje</td>
                                        <td style="width: 7px; height: 32px;">
                                            <asp:TextBox ID="txtparaje" runat="server" CssClass="texto" Height="13px" Width="244px" MaxLength="50"></asp:TextBox></td>
                                        <td style="width: 312px; height: 32px;" align="right">
                                        </td>
                                        <td style="width: 186px; height: 32px;">
                                        </td>
                                        <td style="width: 186px; height: 32px;">
                                        </td>
                                        <td style="width: 595px; height: 32px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px">
                                            Término</td>
                                        <td style="width: 7px">
                                            <asp:TextBox ID="txtterminomunicipal" runat="server" CssClass="texto" Height="13px" Width="244px" MaxLength="50"></asp:TextBox></td>
                                        <td style="width: 312px" align="right">
                                            Provincia</td>
                                        <td>
                                            <asp:TextBox ID="txtprovincia" runat="server" CssClass="texto" Height="13px" Width="270px" MaxLength="50"></asp:TextBox></td>
                                        <td >
                                        </td>
                                        <td >
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px; height: 41px;">
                                            Titular</td>
                                        <td colspan="5" style="height: 41px">
                                            <asp:TextBox ID="txtTitularTerreno" runat="server" CssClass="texto" Height="13px"
                                                Width="628px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td > Incidencia detectada por:
                                        </td>
                                        <td colspan="6" >
                                        <asp:TextBox ID="txtIncDetectadaPor" runat="server" CssClass="texto" MaxLength="150" Width="628px"></asp:TextBox>
                                        </td>

                                    </tr>
                                    
                                    <tr>
                                        <td colspan="6" style="border-bottom: silver thin inset">
                                            <strong>
                                                <asp:Image ID="imgPlus" runat="server" align="absmiddle" ImageUrl="~/images/plus.gif"
                                                    Style="cursor: pointer" ToolTip="Seleccionar del Arbol" Visible="tRUE" />
                                                Imágenes</strong></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                            <asp:Panel ID="pnlEDImagenes" runat="server" Width="911px">
                                <table>
                                    <tr>
                                        <td align="center" style="border-right: #d4d0c8 0px solid; border-top: #d4d0c8 0px solid; border-left: #d4d0c8 0px solid; border-bottom: #d4d0c8 0px solid; background-color: #d4d0c8; width: 174px;">
                                            <strong>
                                            Foto 1</strong></td>
                                        <td align="center" style="border-right: #d4d0c8 0px solid; border-top: #d4d0c8 0px solid; border-left: #d4d0c8 0px solid; border-bottom: #d4d0c8 0px solid; background-color: #d4d0c8; width: 174px;">
                                            <strong>
                                            Foto 2</strong></td>
                                        <td align="center" style="border-right: #d4d0c8 0px solid; border-top: #d4d0c8 0px solid; border-left: #d4d0c8 0px solid; border-bottom: #d4d0c8 0px solid; background-color: #d4d0c8; width: 174px;">
                                            <strong>
                                            Foto 3</strong></td>
                                        <td align="center" style="border-right: #d4d0c8 0px solid; border-top: #d4d0c8 0px solid; border-left: #d4d0c8 0px solid; border-bottom: #d4d0c8 0px solid; background-color: #d4d0c8; width: 174px;">
                                            <strong>
                                            Foto 4</strong></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 174px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; height: 32px; border-bottom: gray thin solid;" align="center">
                                            <asp:Label ID="lblHTMLimgFoto1" runat="server"></asp:Label></td>
                                        <td style="width: 174px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; height: 32px; border-bottom: gray thin solid;" align="center">
                                            <asp:Label ID="lblHTMLimgFoto2" runat="server"></asp:Label></td>
                                        <td style="width: 174px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; height: 32px; border-bottom: gray thin solid;" align="center">
                                            <asp:Label ID="lblHTMLimgFoto3" runat="server"></asp:Label></td>
                                        <td style="width: 174px; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid; height: 32px; border-bottom: gray thin solid;" align="center">
                                            <asp:Label ID="lblHTMLimgFoto4" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-right: #d4d0c8 0px solid; border-top: #d4d0c8 0px solid;
                                            border-left: #d4d0c8 0px solid; border-bottom: #d4d0c8 0px solid; background-color: #d4d0c8; width: 174px;"><table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 155px">
                                                        <asp:FileUpload ID="FileUpload5" runat="server" Font-Names="Verdana" Font-Size="Smaller" Width="100%" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="imgUploadFoto1" runat="server" ImageUrl="~/images/upload.gif" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 155px">
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/Delete.gif"
                                                        ToolTip="Eliminar imagen" OnClientClick="return confirmar_borrado_imagenes();" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="border-right: #d4d0c8 0px solid; border-top: #d4d0c8 0px solid;
                                            border-left: #d4d0c8 0px solid; border-bottom: #d4d0c8 0px solid; background-color: #d4d0c8; width: 174px;"><table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 155px">
                                                        <asp:FileUpload ID="FileUpload6" runat="server" Font-Names="Verdana" Font-Size="Smaller" Width="100%" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="imgUploadFoto2" runat="server" ImageUrl="~/images/upload.gif" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 155px">
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/Delete.gif"
                                                        ToolTip="Eliminar imagen" OnClientClick="return confirmar_borrado_imagenes();" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="border-right: #d4d0c8 0px solid; border-top: #d4d0c8 0px solid;
                                            border-left: #d4d0c8 0px solid; border-bottom: #d4d0c8 0px solid; background-color: #d4d0c8; width: 174px;"><table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 155px">
                                                        <asp:FileUpload ID="FileUpload7" runat="server" Font-Names="Verdana" Font-Size="Smaller" Width="100%" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="imgUploadFoto3" runat="server" ImageUrl="~/images/upload.gif" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 155px">
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/images/Delete.gif"
                                                        ToolTip="Eliminar imagen" OnClientClick="return confirmar_borrado_imagenes();" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" style="border-right: #d4d0c8 0px solid; border-top: #d4d0c8 0px solid;
                                            border-left: #d4d0c8 0px solid; border-bottom: #d4d0c8 0px solid; background-color: #d4d0c8; width: 174px;"><table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 155px">
                                                        <asp:FileUpload ID="FileUpload8" runat="server" Font-Names="Verdana" Font-Size="Smaller" Width="100%" /></td>
                                                    <td>
                                                        <asp:ImageButton ID="imgUploadFoto4" runat="server" ImageUrl="~/images/upload.gif" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 155px">
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/images/Delete.gif"
                                                        ToolTip="Eliminar imagen" OnClientClick="return confirmar_borrado_imagenes();" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="border-bottom: silver thin inset">
                                            <strong>Autor</strong></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px">
                                            Nombre</td>
                                        <td style="width: 7px">
                                            <asp:TextBox ID="txtNombreAutor" runat="server" CssClass="texto" Height="13px" Width="244px" MaxLength="50"></asp:TextBox>
                                            </td>
                                        <td style="width: 312px" align="right">
                                            NIF</td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 100px; height: 19px">
                                            <asp:TextBox ID="txtNifAutor" runat="server" CssClass="texto" Height="13px" Width="107px" MaxLength="50"></asp:TextBox></td>
                                                    <td align="right" style="width: 100px; height: 19px">
                                                        Teléfono</td>
                                                    <td style="width: 100px; height: 19px">
                                            <asp:TextBox ID="txtTlfAutor" runat="server" CssClass="texto" Height="13px" Width="107px" MaxLength="15"></asp:TextBox></td>
                                                    <td style="width: 100px; height: 19px">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                            ControlToValidate="txtTlfAutor" ErrorMessage="?" ValidationExpression="^[0-9]{2,3}-? ?[0-9]{6,7}$"
                                                            Width="9px"></asp:RegularExpressionValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 186px">
                                        </td>
                                        <td style="width: 595px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px; height: 23px;">
                                            Domicilio</td>
                                        <td style="width: 7px; height: 23px;">
                                            <asp:TextBox ID="txtDomicilioAutor" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>
                                                </td>
                                        <td style="width: 312px; height: 23px;" align="right">
                                            Código Postal</td>
                                        <td style="width: 186px; height: 23px;">
                                            <asp:TextBox ID="txtCPAutor" runat="server" CssClass="texto" Height="13px" Width="47px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCPAutor"
                                                ErrorMessage="?" ValidationExpression="^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$"></asp:RegularExpressionValidator></td>
                                        <td style="width: 186px; height: 23px;">
                                        </td>
                                        <td style="width: 595px; height: 23px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px">
                                            Población</td>
                                        <td style="width: 7px">
                                            <asp:TextBox ID="txtPoblacionAutor" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>
                                                </td>
                                        <td style="width: 312px" align="right">
                                            Provincia</td>
                                        <td style="width: 186px">
                                            <asp:TextBox ID="txtProvinciaAutor" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>
                                        </td>
                                        <td style="width: 186px">
                                        </td>
                                        <td style="width: 595px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="border-bottom: silver thin inset">
                                            <strong>Representante</strong></td>
                                    </tr>
                                       <tr>
                                        <td style="width: 2081px; height: 6px;" valign="middle">
                                            Nombre</td>
                                        <td style="width: 7px; height: 6px;" valign="middle">
                                            <asp:TextBox ID="txtNombreRepresentante" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>                                               
                                                </td>
                                        <td style="width: 312px; height: 6px;" align="right" valign="middle">
                                            NIF</td>
                                        <td valign="middle" style="height: 6px">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 100px; height: 3px;">
                                            <asp:TextBox ID="txtNIFRepresentante" runat="server" CssClass="texto" Height="13px"
                                                Width="107px" MaxLength="50"></asp:TextBox></td>
                                                    <td align="right" style="width: 100px; height: 3px;">
                                                        Teléfono</td>
                                                    <td style="width: 100px; height: 3px;">
                                            <asp:TextBox ID="txtTlfRepresentante" runat="server" CssClass="texto" Height="13px"
                                                Width="107px" MaxLength="15"></asp:TextBox></td>
                                                    <td style="width: 100px; height: 3px">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                            ControlToValidate="txtTlfRepresentante" ErrorMessage="?" ValidationExpression="^[0-9]{2,3}-? ?[0-9]{6,7}$"
                                                            Width="9px"></asp:RegularExpressionValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 186px; height: 6px;">
                                        </td>
                                        <td style="width: 595px; height: 6px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px">
                                            Domicilio</td>
                                        <td style="width: 7px">
                                            <asp:TextBox ID="txtDomicilioRepresentante" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>                                            
                                        </td>
                                        <td style="width: 312px" align="right">
                                            Código Postal</td>
                                        <td style="width: 186px">
                                            <asp:TextBox ID="txtCPRepresentante" runat="server" CssClass="texto" Height="13px"
                                                Width="47px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCPRepresentante"
                                                ErrorMessage="?" ValidationExpression="^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$"></asp:RegularExpressionValidator></td>
                                        <td style="width: 186px">
                                        </td>
                                        <td style="width: 595px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px; height: 23px;">
                                            Población</td>
                                        <td style="width: 7px; height: 23px;">
                                            <asp:TextBox ID="txtPoblacionRepresentante" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>                                            
                                        </td>
                                        <td style="width: 312px; height: 23px;" align="right">
                                            Provincia</td>
                                        <td style="width: 186px; height: 23px;">
                                            <asp:TextBox ID="txtProvinciaRepresentante" runat="server" CssClass="texto" Height="13px"
                                                Width="270px" MaxLength="50"></asp:TextBox>                                            
                                        </td>
                                        <td style="width: 186px; height: 23px;">
                                        </td>
                                        <td style="width: 595px; height: 23px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="border-bottom: silver thin inset">
                                            <strong>Empresa</strong></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px" valign="middle">
                                            Nombre</td>
                                        <td style="width: 7px" valign="middle">
                                            <asp:TextBox ID="txtNombreEmpresa" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>
                                        </td>
                                        <td style="width: 312px" align="right" valign="middle">
                                            NIF</td>
                                        <td valign="middle">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 100px; height: 19px;">
                                            <asp:TextBox ID="txtNIFEmpresa" runat="server" CssClass="texto" Height="13px" Width="107px" MaxLength="50"></asp:TextBox></td>
                                                    <td align="right" style="width: 100px; height: 19px;">
                                                        Teléfono</td>
                                                    <td style="width: 100px; height: 19px;">
                                            <asp:TextBox ID="txtTlfEmpresa" runat="server" CssClass="texto" Height="13px" Width="107px" MaxLength="15"></asp:TextBox></td>
                                                    <td style="width: 100px; height: 19px">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                            ControlToValidate="txtTlfEmpresa" ErrorMessage="?" ValidationExpression="^[0-9]{2,3}-? ?[0-9]{6,7}$"
                                                            Width="9px"></asp:RegularExpressionValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 186px">
                                        </td>
                                        <td style="width: 595px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px">
                                            Domicilio</td>
                                        <td style="width: 7px">
                                            <asp:TextBox ID="txtDomicilioEmpresa" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>                                        
                                        </td>
                                        <td style="width: 312px" align="right">
                                            Código Postal</td>
                                        <td style="width: 186px">
                                            <asp:TextBox ID="txtCPEmpresa" runat="server" CssClass="texto" Height="13px" Width="47px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCPEmpresa"
                                                ErrorMessage="?" ValidationExpression="^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$"></asp:RegularExpressionValidator></td>
                                        <td style="width: 186px">
                                        </td>
                                        <td style="width: 595px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px">
                                            Población</td>
                                        <td style="width: 7px">
                                            <asp:TextBox ID="txtPoblacionEmpresa" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>                                            
                                        </td>
                                        <td style="width: 312px" align="right">
                                            Provincia</td>
                                        <td style="width: 186px">
                                            <asp:TextBox ID="txtProvinciaEmpresa" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>                                            
                                        </td>
                                        <td style="width: 186px">
                                        </td>
                                        <td style="width: 595px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="border-bottom: silver thin inset">
                                            <strong>Director Obra</strong></td>
                                    </tr>
                                       <tr>
                                        <td style="width: 2081px" valign="middle">
                                            Nombre</td>
                                        <td style="width: 7px" valign="middle">
                                            <asp:TextBox ID="txtNombreDirectorObra" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>                                            
                                        </td>
                                        <td style="width: 312px" align="right" valign="middle">
                                            NIF</td>
                                        <td style="width: 186px" valign="middle">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 100px; height: 21px">
                                            <asp:TextBox ID="txtNIFDirectorObra" runat="server" CssClass="texto" Height="13px"
                                                Width="107px" MaxLength="50"></asp:TextBox></td>
                                                    <td align="right" style="width: 85px; height: 21px">
                                                        Teléfono</td>
                                                    <td align="right" style="width: 100px; height: 21px">
                                            <asp:TextBox ID="txtTlfDirectorObra" runat="server" CssClass="texto" Height="13px"
                                                Width="107px" MaxLength="15"></asp:TextBox></td>
                                                    <td style="width: 100px; height: 21px">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                            ControlToValidate="txtTlfDirectorObra" ErrorMessage="?" ValidationExpression="^[0-9]{2,3}-? ?[0-9]{6,7}$"
                                                            Width="9px"></asp:RegularExpressionValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 186px">
                                        </td>
                                        <td style="width: 595px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px">
                                            Domicilio</td>
                                        <td style="width: 7px">
                                            <asp:TextBox ID="txtDomicilioDirectorObra" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>                                            
                                        </td>
                                        <td style="width: 312px" align="right">
                                            Código Postal</td>
                                        <td style="width: 186px">
                                            <asp:TextBox ID="txtCPDirectorObra" runat="server" CssClass="texto" Height="13px"
                                                Width="47px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCPDirectorObra"
                                                ErrorMessage="?" ValidationExpression="^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$"></asp:RegularExpressionValidator></td>
                                        <td style="width: 186px">
                                        </td>
                                        <td style="width: 595px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2081px">
                                            Población</td>
                                        <td style="width: 7px">
                                            <asp:TextBox ID="txtPoblacionDirectorObra" runat="server" CssClass="texto" Height="13px"
                                                Width="244px" MaxLength="50"></asp:TextBox>
                                         </td>
                                        <td style="width: 312px" align="right">
                                            Provincia</td>
                                        <td style="width: 186px">
                                            <asp:TextBox ID="txtProvinciaDirectorObra" runat="server" CssClass="texto" Height="13px"
                                                Width="270px" MaxLength="50"></asp:TextBox></td>
                                        <td style="width: 186px">
                                        </td>
                                        <td style="width: 595px">
                                        </td>
                                    </tr>
                                <tr>
                                
                                    <td colspan="7" align="right" style="border-top:1px solid #666666; height: 24px;">
                                    <asp:Button ID="btnAceptar" Runat="server" cssclass="boton" Text='Aceptar'></asp:Button>
                                    <asp:Button ID="btnCancelar" Runat="server" cssclass="boton" Text='Cancelar' CausesValidation=False></asp:Button>
                                        <asp:Button ID="btnImprimir" runat="server" CausesValidation="False" CssClass="boton"
                                            Text='Imprimir' />
                                    </td>
                                </tr>
                     
                               </table>           
                            </td>
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
