<%@ Page Language="VB" AutoEventWireup="false" CodeFile="correspondencia.aspx.vb" Inherits="SICAH_SeguimientoAdministrativo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
        <link rel="shortcut icon" href="home/images/favicon.ico" />
        <!--<link rel="stylesheet" href="home/css/style.css">-->
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/stylesheet_opt.css" />
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/theme.css" />
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/utiles.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/menu.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/ddmenu.js"></script>
        <script src="../../SICAH/HighCharts/jquery.min131.js" type="text/javascript"></script> 
        <script type="text/javascript">

            function pageLoad() {
            }
    
        </script>
        <script>
            $(window).load(function() {
                $().UItoTop({ easingType: 'easeOutQuart' });
                $('#stuck_container').tmStickUp({});
            }); 
        </script>
        <script language="javascript" type="text/javascript">

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


    </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="container">
                <div id="container2">
                    <asp:Literal ID="HTML_Links_Sup_Izq"     runat="server"></asp:Literal> 
                    <asp:Literal ID="HTML_Subcabecera_Logos" runat="server"></asp:Literal>
                    <asp:Literal ID="HTML_Menu_Navegacion"   runat="server"></asp:Literal>
                    <div id="content-wrapper">
                        <p class="breath">
                            <a name="up" id="up"></a>
                                Estas en::
                            <a href="index.aspx">Inicio</a> > <a href="correspondencia.aspx">Correspondencia</a>
                        </p>
                        <div id="left-column">
                            <ul class="nav-menu">
                                <asp:Literal ID="HTML_Link_Infadm" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Infadm_pdf" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_InfContadores" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Esqumas" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Fotos" runat="server"></asp:Literal>
                            </ul>    
                        </div>
                        <div id="central-content"> 
                            <h2><asp:Literal ID="HTML_Título_Aprovechamiento" runat="server"></asp:Literal></h2>
                            <p>Información detallada de todas las comunicaciones e incidencias relativas a su aprovechamiento registradas en Confederación Hidrográfica del Segura</p>
                              
                            <!-- Panel listar Seguimientos -->                      
                            <asp:Panel ID="pnlSeguimientos" Runat="server" Visible="true">
                                
                                <!--<table style=" visibility:hidden; height: 112px; width: 100%;">                                                                   
                                    
                                    <tr>                                        
                                        <td class="style2">Fecha</td>  
                                        <td class="style3">Tipo Suceso</td>  
                                        <td class="style4">Comentarios</td>  
                                        <td class="style5">Código Cauce</td>   
                                        <td class="style5">Código PVYCR</td>
                                        <td class="style6">Responsable</td> 
                                        <td class="style7"></td>
                                    </tr> 
                                    <tr id="FilaFiltro">
                                        <td class="encabListado" style="width: 117px" valign="middle">
                                            
                                        </td>
                                        <td class="encabListado" valign="middle" style="width: 207px">
                                            <asp:DropDownList ID="ddlFTipoSuceso" runat="server" CssClass="texto" Width="102px" Font-Size="8pt"></asp:DropDownList>
                                        </td>
                                        <td class="encabListado" valign="middle" style="width: 255px">
                                            <asp:TextBox ID="txtFComentarios" runat="server" Columns="13" CssClass="texto" 
                                                Width="118px"></asp:TextBox>
                                        </td>
                                        <td class="encabListado" valign="middle" style="width: 95px">
                                            <asp:TextBox ID="txtFCodigoCauce" runat="server"  CssClass="texto" Width="90px"></asp:TextBox>
                                        </td>
                                        <td class="encabListado" style="width: 95px" valign="middle">
                                            <asp:TextBox ID="txtFCodigoPVYCR" runat="server"  CssClass="texto"  Width="90px"></asp:TextBox>
                                        </td>
                                        <td class="encabListado" valign="middle" style="width: 98px">
                                            <asp:TextBox ID="txtFResponsable" runat="server" Columns="13" CssClass="texto" Width="75px"></asp:TextBox>
                                        </td>
                                        <td class="encabListado" valign="middle" style="width: 62px">
                                               <asp:LinkButton id="lbtAceptarUsReg" Runat="server" onclick="AceptarFiltro" CssClass="enlaceLecturas" Width="60px">Aceptar</asp:LinkButton>
                                        </td>
                                    </tr> 
                                </table>-->  
                                    
                                <asp:Panel ID="pnlScrollGridSeguimientosAdministrativos" runat="server" Height="100%" ScrollBars="Vertical" Width="100%">
                                    <asp:Repeater ID="rptSeguimientosAdministrativos" runat="server">       
                                        
                                        <HeaderTemplate>
                                        <table>
                                           <tr>
                                                <th>Fecha</th>            
                                                <th>Comentario</th>
                                                
                                            </tr>
                                        </HeaderTemplate>                                              
                                        <ItemTemplate>
                                            <tr <%# checkFila()%>>
                                                <td width="15%"><%#DataBinder.Eval(Container.DataItem, "Fecha", "{0:dd/MM/yyyy}")%></td>
                                                
                                                <td width="85%"><%#Container.DataItem("Comentarios")%></td>
                                                
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                                        
                                <!--<asp:Panel ID="pnlEDVSeguimientos" runat="server" Visible="False" BorderColor="Navy" BackColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1px">
                                    <table style="width: 702px" cellpadding="3">
                                        <tr>
                                            <td class="titulosec" colspan="6" valign="bottom">
                                                <asp:Label ID="lblModoED" runat="server" Text="EDICIÓN DE SEGUIMIENTO" Width="245px" CssClass="tituloSec" EnableTheming="True" Font-Bold="True" Height="6px"></asp:Label></td>
                                            <td align="right" class="titulosec" colspan="1" valign="top">
                                                <asp:ImageButton ID="imgCerrarED" runat="server" ImageUrl="~/images/close2.gif" ToolTip="Cerrar" /></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="height: 124px">
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
                                                                ImageUrl="../images/calendario.gif" Style="cursor: pointer" />
                                                        </td>
                                                        <td width="117px">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top" style="width: 207px; height: 124px">
                                                &nbsp;Tipo Suceso<br />
                                                <table>
                                                    <tr>
                                                        <td>
                                                <asp:DropDownList ID="ddlEDTipoSuceso" runat="server" CssClass="texto" Font-Size="8pt" Width="213px" ToolTip="Tipo Suceso">
                                                </asp:DropDownList></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top" style="height: 124px">
                                                &nbsp;Comentarios&nbsp;<table cellpadding="3">
                                                    <tr>
                                                        <td style="height: 115px">
                                                <asp:TextBox ID="txtEDComentarios" runat="server"  CssClass="texto" Height="104px"
                                                    TextMode="MultiLine" Width="214px" Font-Names="Verdana" Font-Size="8pt" ToolTip="Comentarios"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top" style="width: 100px; height: 124px;" colspan="2">
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
                                            <td style="width: 55px; height: 124px;" valign="top">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td style="width: 26px">
                                                            <asp:ImageButton ID="imgAgregarSeguimiento" runat="server" 
                                                                ImageUrl="~/images/plus.gif" />
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
                                                        <asp:ImageButton ID="imgCancelar" runat="server" ImageUrl="~/images/close2.gif" Visible="False" />
                                                        </td>
                                                        <td>
                                                           <asp:Label ID="lblCancelar" runat="server" Text="Cancelar" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="height: 124px;" valign="top">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:Panel>-->   
                            </asp:Panel>                           
                            <!-- Etiquetas de Valor Seleccionado en el Repeater de Seguimientos -->
                            <asp:Label ID="lblCodigoCauceSel" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblFechaSel" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblTipoSucesoSel" runat="server" Visible="False" Height="6px"></asp:Label>
                            <asp:Label ID="lblNumRefSel" runat="server" Height="1px" Visible="False"></asp:Label><!-- Fin Panel Editar Seguimientos -->
                        </div>        
                    </div>
                </div>
                <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
            </div>      
            </div>
            <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>
        </form>
    </body>
</html>
