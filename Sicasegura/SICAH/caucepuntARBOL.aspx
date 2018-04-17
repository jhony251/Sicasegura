<%@ Page Language="VB" AutoEventWireup="false" CodeFile="caucepuntARBOL.aspx.vb" Inherits="SICAH_caucepuntARBOL" TRACE="False"%>
<%@ OutputCache Duration="86400" VaryByControl="none" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  <link href="../styles.css" rel="stylesheet" />
  <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script type="text/jscript" language="javascript" src="../js/utilesListados.js"></script>  
<script type="text/javascript">
var previousSelectedNode = null; 
var previousSelectedNodeClass = null; 
function onTreeNodeSelect()
    {
        if(previousSelectedNode == null) 
        {
           previousSelectedNode = this; 
           previousSelectedNodeClass = this.className;
        }
        else  
        {
            previousSelectedNode.className = previousSelectedNodeClass;
            previousSelectedNode = this; 

            previousSelectedNodeClass = this.className;
        }
        
        this.className = "nodeSelected"; 
        //Control del menu contextual del arbol
        hidemenuie5(event);
   
    }

    window.onload = function() {
        //Muestra el panel de carga
        mostrarCargando();

        //EGB Localización de nodos del arbol
        var treeview = document.getElementById("<%=TreeView1.ClientID %>");
        var nodes = treeview.getElementsByTagName("a");
        var nodos = nodes.length;
        //var strCookie='';
        var strIdsCliente = '';
        var ARG = '';
        var CM = '';
        var OT = '';
        var OT1 = '';
        var S = '';
        var VA = '';
        var VB = '';
        var VM = '';
        var P = '';
        var T = '';
        var CRE = '';

        for (i = 0; i < nodos; i++) {
            //EGB Carga de evento onclick que establece el style de selected node.
            nodes[i].onclick = onTreeNodeSelect;

            //EGB Carga de variables locales para almacenar los ID´s Cliente en una cookie
            //---------------------------------------------------------------
            if (ARG.length == 0) {
                if (nodes[i].innerHTML.indexOf('ARG Argos') > 0) {
                    ARG = nodes[i].id;
                }
            }
            //---------------------------------------------------------------
            if (CM.length == 0) {
                if (nodes[i].innerHTML.indexOf('CM Cabecera - Río Mundo') > 0) {
                    CM = nodes[i].id;
                }
            }
            //---------------------------------------------------------------
            if (OT.length == 0) {
                if (nodes[i].innerHTML.indexOf('OT Residuales EDAR') > 0) {
                    OT = nodes[i].id;
                }
            }

            //---------------------------------------------------------------
            if (OT1.length == 0) {
                if (nodes[i].innerHTML.indexOf('OT Pozos de CHS') > 0) {
                    OT1 = nodes[i].id;
                }
            }
            //---------------------------------------------------------------
            if (S.length == 0) {
                if (nodes[i].innerHTML.indexOf('S Pozos') > 0) {
                    S = nodes[i].id;
                }
            }
            //---------------------------------------------------------------
            if (VA.length == 0) {
                if (nodes[i].innerHTML.indexOf('VA Vega Alta') > 0) {
                    VA = nodes[i].id;
                }
            }
            //---------------------------------------------------------------
            if (VB.length == 0) {
                if (nodes[i].innerHTML.indexOf('VB Vega Baja') > 0) {
                    VB = nodes[i].id;
                }
            }
            //---------------------------------------------------------------
            if (VM.length == 0) {
                if (nodes[i].innerHTML.indexOf('VM Vega Media') > 0) {
                    VM = nodes[i].id;
                }
            }
            //---------------------------------------------------------------
            //ncm 25/02/2010 añadimos peajes y trasvases
            if (P.length == 0) {
                if (nodes[i].innerHTML.indexOf('P Peajes') > 0) {
                    P = nodes[i].id;
                }
            }
            if (T.length == 0) {
                if (nodes[i].innerHTML.indexOf('T Trasvases') > 0) {
                    T = nodes[i].id;
                }
            }
            if (CRE.length == 0) {
                if (nodes[i].innerHTML.indexOf('CRE Crevillente') > 0) {
                    CRE = nodes[i].id;
                }
            }
        }
        //EGB Carga la Cookie de Ids de Cliente
        //strCookie = ARG+'#'+CM+'#'+OT+'#'+OT1+'#'+S+'#'+VA+'#'+VB+'#'+VM;
        //Set_Cookie('idsClienteNodosPadre',strCookie,'1',null,null,null);
        //alert(strCookie);

        //EGB Carga TextBox Oculto
        strIdsCliente = ARG + '#' + CM + '#' + OT + '#' + OT1 + '#' + S + '#' + VA + '#' + VB + '#' + VM + '#' + P + '#' + T + '#' + CRE + '#';
        document.getElementById("IdsCliente").value = strIdsCliente;

        //Oculta capa de Progreso
        ocultarCargando();
        //Oculta menu Contextual
        hidemenuie5(event);
        //Establece la propiedad del iframe Roles
        document.getElementById("iframeRoles").src = "CaucePuntROLES.aspx";

    }
    
function mostrarCargando(){
    var capaCargando = document.getElementById('capaProgreso');
    capaCargando.style.width = ''+(document.body.scrollWidth+30)+'px';
    capaCargando.style.height = ''+(document.body.scrollHeight+1000)+'px'; 
    capaCargando.style.top = ''+(document.body.scrollTop) + 'px';
    capaCargando.style.display='';
}

//Oculta la capa "cargando..."
function ocultarCargando(){
    var capaCargando = document.getElementById('capaProgreso');
    capaCargando.style.display='none';
}   

function Delete_Cookie( name, path, domain ) {
    if ( Get_Cookie( name ) ) document.cookie = name + "=" +
    ( ( path ) ? ";path=" + path : "") +
    ( ( domain ) ? ";domain=" + domain : "" ) +
    ";expires=Thu, 01-Jan-1970 00:00:01 GMT";
}
	
function Set_Cookie( name, value, expires, path, domain, secure ) 
{
    // Obtención de la Hora en milisegundos
    var today = new Date();
    today.setTime( today.getTime() );

    if ( expires )
    {
    expires = expires * 1000 * 60 * 60 * 24;
    }
    var expires_date = new Date( today.getTime() + (expires) );

    document.cookie = name + "=" +escape( value ) +
    ( ( expires ) ? ";expires=" + expires_date.toGMTString() : "" ) + 
    ( ( path ) ? ";path=" + path : "" ) + 
    ( ( domain ) ? ";domain=" + domain : "" ) +
    ( ( secure ) ? ";secure" : "" );
}

/*
function getCookie(name)
{
  var cname = name + "=";               
  var dc = document.cookie;             
  if (dc.length > 0) {              
    begin = dc.indexOf(cname);       
    if (begin != -1) {           
      begin += cname.length;       
      end = dc.indexOf(";", begin);
      if (end == -1) end = dc.length;
        return unescape(dc.substring(begin, end));
    } 
  }
  return null;
}
function mostrarRamasROLES() 
{
//EGB 30/09/2008 Mostrar/OcultarRamas en Funcion del rol de usuario
   var cookiePermisos = getCookie('arrayPermisos');
   var arrayPermisos = cookiePermisos.split("#"); 
   alert(cookiePermisos)
   document.getElementById('treeView1n0').style.display=arrayPermisos[0]; 
   document.getElementById('treeView1n2').style.display=arrayPermisos[1];
   document.getElementById('treeView1n227').style.display=arrayPermisos[2];
   document.getElementById('treeView1n273').style.display=arrayPermisos[3];
   document.getElementById('treeView1n676').style.display=arrayPermisos[4];
   document.getElementById('treeView1n1603').style.display=arrayPermisos[5];
   document.getElementById('treeView1n3128').style.display=arrayPermisos[6];
   document.getElementById('treeView1n3586').style.display=arrayPermisos[7];

 }*/
</script>


  
</head>
<body bgcolor="#EEEAD2">
<div id="capaProgreso" style="position:absolute;left:0px;top:0px;width:100%;height:120%;z-index:0;background-color:white;filter: alpha(opacity=60);opacity: .6;-moz-opacity:0.6;">
<br />
    <br /><br />
    <br /><br /><br /><br /><br /><br />
<table width="100%"><tr><td width="100%" align="center" valign="middle" style="color:navy; font-size:14px"><b>CARGANDO...</b><br /></td></tr></table>
    <asp:Panel ID="pnlMenuContextual" BorderStyle=Outset BorderWidth="1PX" runat="server" CssClass="skin0" onMouseover="highlightie5(event)" onMouseout="lowlightie5(event)" Width="130px" Height="22px" style="display:none"> 
        <div class="menuitems">
            <table cellspacing=0 cellpadding=0>
            <tr>
                <td class="menuitems">
                    <img id="imgplus" src="../images/plus.gif" style="vertical-align:middle" />
                    <img id="imgplusON" src="../images/plusON.gif" style="vertical-align:middle;display:none" />
                </td>
                <td class="menuitems">
                    <a id ="ExpandirTodo"  href="javascript:void(0)"  onclick="javascript:TreeviewExpandCollapseAlldivs('<%=treeView1.ClientID%>', false); hidemenuie5(event);">Expandir Todo </a>
                </td>
            </tr>
            </table>
        </div>
        <div class="menuitems">
            <table cellspacing=0 cellpadding=0>
            <tr>
                <td class="menuitems">
                    <img id="imgminus" src="../images/minus.gif" style="vertical-align:middle"/>
                    <img id="imgminusON" src="../images/minusON.gif" style="vertical-align:middle;display:none" />
                </td>
                <td class="menuitems">
                    <a id ="ContraerTodo" href="javascript:void(0)"  onclick="javascript:TreeviewExpandCollapseAlldivs('<%=treeView1.ClientID%>', true); hidemenuie5(event);">Contraer Todo </a>
                </td>
            </tr>
            </table>
        </div> 
             
    </asp:Panel> 
    </div>



  <form id="Form1" method="post" runat="server">
  <INPUT id="IdsCliente" type="hidden" value="" />
  <div>
    
  
      <span id="dsp4"></span><span id="imagepath" style="display:none">../js/calendar/images</span>
        <table style="width: 300px; height: 622px; background-color: white;" oncontextmenu ="return showmenuie5(event,'');">
         <!-- <tr>
              <td colspan="3" style="height: 15px; width: 296px;">
                           <asp:TextBox ID="txtCauce" runat="server" CssClass="texto" Width="258px"></asp:TextBox>
                  <asp:ImageButton ID="imgBuscarCauce" runat="server" BorderStyle="None" BorderWidth="0px"
                               ImageAlign="Top" ImageUrl="~/SICAH/images/iconos/icoBtnSistemasBuscar2.gif" ToolTip="Buscar en el arbol de cauces" /></td>
          </tr>
            <tr>
                <td colspan="3" style="height: 15px; width: 296px;">
                    <asp:Panel ID="pnlResultadoBusquedas" runat="server" Height="100%" Width="100%">
                    <table width=100%>                                               
                                <asp:Repeater ID=rptBusquedas runat=server>  
                                    <HeaderTemplate>
                                        <tr><td class="tituloSec" colspan=4>Resultados de la Búsqueda</td></tr>
                                        <tr>
                                        <td class="encabListado">Descripción</td>
                                        <td class="encabListado">Tipo</td>
                                        
                                        </tr>
                                    </HeaderTemplate>                                                      
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td>
                                                <a href="../SICAH/InformacionGeneral.aspx?nodotexto=<%#Container.DataItem("Descripcion")%>&nodoclave=<%#Container.DataItem("ClaveNodo2")%>" target="iframeDetalle"><%#Container.DataItem("Descripcion")%></a></td>
                                            <td><%#Container.DataItem("Tipo")%></td>
                                            
                                            
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                                              
                     </table>
                    
                    </asp:Panel>
                </td>
            </tr>
         
          <tr>  -->
         
          <!--<td class="encabListado" style="padding-right: 5px; padding-left: 5px; background-color: #ffffcc; font-weight:bold; " valign="top" colspan="2">Modo</td>
          </tr>
          
          <tr>
          
            <td colspan=2 style=" border:solid 1px black; background-color: #ffffcc" width="80%" id="modo">
                                                 
                <input type="radio" name="seleccionar" value="L" id="rbLecturas" onclick="ActualizarOculto()" checked="CHECKED" style="width:15%" />
                Acceso a Datos<label for="rbLecturas"></label>
              
                <input type="radio" name="seleccionar" value="V" id="rbVisor" onclick="ActualizarOculto()" style="width:15%"  />
                Visor<label for="rbLecturas">&nbsp;</label>
               </td> 
          </tr>-->
          <tr>
              <td colspan="3" style="height: 622px; width: 296px;">
                      <asp:Panel ID="pnlArbol" Runat="server" Visible="true" Width="290px" ScrollBars="Auto" BorderWidth="1px" Height="615px">
                      <asp:TreeView ID="treeView1" runat="server" Width="171px" CollapseImageToolTip="" ExpandImageToolTip="">
                        <SelectedNodeStyle BackColor="PaleGoldenrod" Font-Bold="True" Font-Underline="True" />
                      </asp:TreeView>
                      </asp:Panel>
              </td>
          </tr>
      </table>  
       </div>    
      <asp:ImageButton ID="imgExportaExcel" runat="server" ImageUrl="~/SICAH/images/Excel.bmp" Visible="False" />
      
      <br />
      <iframe name="iframeRoles" id="iframeRoles" style="width:45px; height: 24px" src="" frameborder="1" marginheight="0" marginwidth="0" scrolling="no" class="HTMframe"></iframe>
  </form>
  <script type="text/jscript" language="javascript" src="../js/utilesArboles.js"></script>
  <script type="text/javascript">
    //Control del menu contextual del botón derecho del ratón
    var ie5=document.all&&document.getElementById
    var ns6=document.getElementById&&!document.all
    if (ie5||ns6)
    
    var menuobj=document.getElementById("pnlMenuContextual")
    
     
    function showmenuie5(e,t)
    {
        //document.getElementById("ExpandirTodo").innerText = "Expandir Cauce: " +  t
        //document.getElementById("ContraerTodo").innerText = "Contraer Cauce: " +  t
        
        var rightedge=ie5? document.body.clientWidth-event.clientX : window.innerWidth-e.clientX
        var bottomedge=ie5? document.body.clientHeight-event.clientY : window.innerHeight-e.clientY

        if (rightedge<menuobj.offsetWidth) 
            menuobj.style.left=ie5? document.body.scrollLeft+event.clientX-menuobj.offsetWidth : window.pageXOffset+e.clientX-menuobj.offsetWidth
        else
            menuobj.style.left=ie5? document.body.scrollLeft+event.clientX : window.pageXOffset+e.clientX

        if (bottomedge<menuobj.offsetHeight) 
            menuobj.style.top=ie5? document.body.scrollTop+event.clientY-menuobj.offsetHeight : window.pageYOffset+e.clientY-menuobj.offsetHeight
        else

        menuobj.style.top=ie5? document.body.scrollTop+event.clientY : window.pageYOffset+e.clientY

        if(ie5) 
        window.event.cancelBubble = true;
        
        else if(ns6) 
        e.stopPropagation();

        menuobj.style.visibility="visible"

        return false
    }
    
    function hidemenuie5(e)
    { 
        menuobj.style.visibility="hidden"
    }

    function highlightie5(e){

    var firingobj=ie5? event.srcElement : e.target

    if (firingobj.className=="menuitems"||ns6&&firingobj.parentNode.className=="menuitems"){

    if (ns6&&firingobj.parentNode.className=="menuitems") 
    firingobj=firingobj.parentNode 

    firingobj.style.backgroundColor="#eee8aa"
    firingobj.style.fontWeight='bold'
    
    if (document.getElementById("ExpandirTodo").style.fontWeight=='bold')
        {
        document.getElementById("imgplus").style.display='none'
        document.getElementById("imgplusON").style.display='block'
        document.getElementById("imgminus").style.display='block'
        document.getElementById("imgminusON").style.display='none'
        }
     if (document.getElementById("ContraerTodo").style.fontWeight=='bold')
        {
        document.getElementById("imgminus").style.display='none'
        document.getElementById("imgminusON").style.display='block'
        document.getElementById("imgplus").style.display='block'
        document.getElementById("imgplusON").style.display='none'
        }
    }

    }

    function lowlightie5(e)
    {
        var firingobj=ie5? event.srcElement : e.target

        if (firingobj.className=="menuitems"||ns6&&firingobj.parentNode.className=="menuitems")
        {

            if (ns6&&firingobj.parentNode.className=="menuitems") 
            firingobj=firingobj.parentNode 

            firingobj.style.backgroundColor=""
            
            firingobj.style.color=""
            firingobj.style.fontWeight=""
            
             if (document.getElementById("ExpandirTodo").style.fontWeight=='bold')
                {
                document.getElementById("imgplus").style.display='block'
                document.getElementById("imgplusON").style.display='none'
                document.getElementById("imgminus").style.display='none'
                document.getElementById("imgminusON").style.display='block'
                }
              if (document.getElementById("ContraerTodo").style.fontWeight=='bold')
                {
                document.getElementById("imgminus").style.display='block'
                document.getElementById("imgminusON").style.display='none'
                document.getElementById("imgplus").style.display='none'
                document.getElementById("imgplusON").style.display='block'
                }
            window.status=''
        }
    }


</script> 
</body>
</html>
