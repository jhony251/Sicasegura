<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CurvasAcequiasBUSCAR.aspx.vb" Inherits="SICAH_CurvasAcequiasBUSCAR" TRACE="False"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  <link href="../styles.css" rel="stylesheet" />
  <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script type="text/jscript" language="javascript" src="../js/utilesListados.js"></script>

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
      miDIV.style.display='';
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
      <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label>
                    <asp:Label ID="lblResultado" runat="server" style="font-weight:bold; " Font-Names="Arial" Font-Size="8pt"> </asp:Label><table style="width: 150px;background-color: white;">
                        <tr>
                            <td bgcolor="#eeead2" class="titulo" colspan="2">
                                <%=getLiteralEncabezado()%> Régimen de Curva</td>
                        </tr>
                        <tr>
                            <td bgcolor="#eeead2" style="width: 378px; height: 310" valign="top">
                           <asp:TextBox ID="txtBuscarAcequia" runat="server" CssClass="texto" Width="157px"></asp:TextBox></td>
                            <td bgcolor="#eeead2" style="width: 378px;" valign="top">
                  <asp:ImageButton ID="imgBuscarAcequiasConCurva" runat="server" BorderStyle="None" BorderWidth="0px"
                      ImageAlign="Top" ImageUrl="~/SICAH/images/iconos/icoBtnAcequiasBuscar.GIF" ToolTip="Actualizar" /></td>
                        </tr>
          <tr>
              <td bgcolor="#eeead2" colspan="2" style="width: 378px" valign="top">
                 
                               
                                <asp:Panel ID="pnlResultadoBusquedas" runat="server" ScrollBars="Vertical" Width="190px" Height="282px" BackColor="#EEEAD2">
                    <table cellpadding="0" cellspacing="0" width="173">   
                                                        
                                <asp:Repeater ID=rptBusquedas runat=server>  
                                                                     
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><a href="../SICAH/CurvasAcequiasDETALLE.aspx?nodotexto=<%#Container.DataItem("Descripcion")%>&nodoclave=<%#Container.DataItem("ClaveNodo2")%>&codigoPVYCR=<%#Container.DataItem("CodigoPVYCR")%>&idelementoMedida=<%#Container.DataItem("IdElementoMedida")%>" target="iframeDetalle"><%#Container.DataItem("Descripcion")%></a></td>  
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                                            
                     </table>
                    
                    </asp:Panel>
              </td>
          </tr>
          
        
          
      </table>      
   </form>
</body>
</html>
