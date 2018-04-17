<%@ Page Language="VB" AutoEventWireup="false" CodeFile="caucepuntBUSCAR.aspx.vb" Inherits="SICAH_caucepuntBUSCAR" TRACE="False"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html>
<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  <link href="../styles.css" rel="stylesheet" />
  <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script type="text/jscript" language="javascript" src="../js/utilesListados.js"></script>
  <!-- referencia llibrerías para el arbol -->
  <script  type="text/javascript" language="javascript" src="../js/ext-base-debug-w-comments.js"></script>
  <script  type="text/javascript" language="javascript" src="../js/ext-all-debug.js"></script>    
  <link href="../ext-all.css" rel="stylesheet" type="text/css" />
  <!--<link href="http://192.168.41.3:8081/includes/ext/resources/css/reset-min.css" rel="stylesheet" type="text/css" />-->
  <link href="../EstilosArbol.css" rel="stylesheet" type="text/css" />
  <script type="text/jscript" language="javascript" src="../js/utilesObjetoArbol.js"></script>    

<script>
var previousSelectedNode = null; 
var previousSelectedNodeClass = null; 

function SeleccionaNodoFrameARBOL(namSelect)
//EGB. Esta función recorre todos los nodos por lo que es eficaz pero lenta. 
//ver SeleccionaNodoFrameARBOL2
{
    var treeview = window.parent.frames['iframeArbol'].document.getElementById("TreeView1");
    var nodes = treeview.getElementsByTagName("a");
    for(i=0;i<nodes.length;i++)
     {
       if (nodes[i].innerHTML.indexOf(namSelect)>0)  
       {
        nodes[i].className = "nodeSelected";
        break;    
       }
     }
     
}
function findPosY(obj) 
{
    var curtop = 0; 
    if (obj.offsetParent) 
    {
        while (obj.offsetParent) 
        {

            curtop += obj.offsetTop

            obj = obj.offsetParent;

        }
    }
    else if (obj.y) 
        curtop += obj.y;
        
    return curtop; 
}

function findPosX(obj) 
{
    var curleft = 0; 
    if (obj.offsetParent) 
    {
        while (obj.offsetParent) 
        { 
            curleft += obj.offsetLeft
            obj = obj.offsetParent;
        }

    }

    else if (obj.x) 
        curleft += obj.x;

    return curleft; 
}
function mostrarDIVS(cadenaDIVS) 
{
//EGB 06/08/2008 Equivalente a Expandir Nodo en Servidor, pero en Cliente.
   var arrayDIVS = cadenaDIVS.split("#");
   for (var i=0; i < arrayDIVS.length; i++) 
   {
      var miidDIV = arrayDIVS[i];
      var miDIV = window.parent.frames['iframeArbol'].document.getElementById(miidDIV+'Nodes');
      //miDIV.style.display='';
   }
}

function SeleccionaNodoFrameARBOL2(idcontrol,strDIVs)
{
   
    mostrarDIVS(strDIVs);
    
    var minodo = window.parent.frames['iframeArbol'].document.getElementById(idcontrol);
    var midivARBOL = window.parent.frames['iframeArbol'].document.getElementById('pnlArbol');
    //EGB 06/08/2008 He comentado esta linea xq esteticamente el posicionamiento y no me acaba de convencer.
    //midivARBOL.scrollTop = findPosY(minodo); 
    midivARBOL.scrollLeft  = findPosX(minodo); 
    //tratamiento de la selección en Azul
    if(previousSelectedNode == null) 
        {
           previousSelectedNode = minodo; 
           previousSelectedNodeClass = minodo.className;
        }
        else  
        {
            previousSelectedNode.className = previousSelectedNodeClass;
            previousSelectedNode = minodo; 
            previousSelectedNodeClass = minodo.className;
        }
                
        minodo.className = "nodeSelectedBusqueda"; 
    
  
}    
</script>
  
</head>
<body bgcolor="#eeead2">
  <form id="Form1" method="post" runat="server">
      <span id="dsp4"></span><span id="imagepath" style="display:none">../js/calendar/images</span>
                    <asp:Label ID="lblResultado" runat="server" style="font-weight:bold; font-size:7" Font-Names="Arial" Font-Size="8pt"> </asp:Label><table style="width: 300px;background-color: white;">
          <tr>
              <td colspan="3" style="height: 15px;" bgcolor="#eeead2">
                           <asp:TextBox ID="txtCauce" runat="server" CssClass="texto" Width="269px"></asp:TextBox>
                  <asp:ImageButton ID="imgBuscarCauce" runat="server" BorderStyle="None" BorderWidth="0px"
                               ImageAlign="Top" ImageUrl="~/SICAH/images/iconos/icoBtnSistemasBuscar2.gif" ToolTip="Buscar en el arbol de cauces" />
                               
                                <asp:Panel ID="pnlResultadoBusquedas" runat="server" ScrollBars="Vertical" Width="299px" Height="65px" BackColor="#EEEAD2" BorderColor="#EEEAD2" BorderStyle="None">
                    <table cellpadding="0" cellspacing="0">   
                                                        
                                <asp:Repeater ID=rptBusquedas runat=server>  
                                                                     
                                    <ItemTemplate>
                                        <tr >
                                            <td><a id="pulsarNodo" style="cursor:hand" onclick="<%# DataBinder.Eval(Container.DataItem,"ClaveNodo2","javascript:expandirArbol('{0:s}')") %>" class="<%#Container.DataItem("background_color")%>" onmouseout="style.backgroundColor='#EEEAD2'" onmouseover="style.backgroundColor='#D1E7CF'" target="iframeArbol"><%#Container.DataItem("Descripcion")%></a></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                                            
                     </table>
                    
                    </asp:Panel>
                            
              </td>
          </tr>
                    
      </table>   
    <!--<div  id="ContenedorBusqueda"></div>-->
    <div  id="ContenedorArbol"></div>            
   </form>
</body>
</html>
