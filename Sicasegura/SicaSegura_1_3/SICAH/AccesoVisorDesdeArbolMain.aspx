<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccesoVisorDesdeArbolMain.aspx.vb" Inherits="SICAH_AccesoVisorDesdeArbolMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script type="text/javascript">
    function OcultarMostrarLeyendas()
    {
        if (document.getElementById('iframeLeyendasVisor').style.width == '0px')
        {
            document.getElementById('iframeLeyendasVisor').style.width = 300;
            document.getElementById('leyendas').style.display='';
         }
         else
         {
         
            document.getElementById('iframeLeyendasVisor').style.width = 0;       
            document.getElementById('leyendas').style.display='none';
            top.document.getElementById('imgflecha').src="images/flecha_ampliar.gif"
            
         }
        
    }
    function tamañoLeyendas()
    {
        document.getElementById('iframeLeyendasVisor').style.width=0;
    }
</script>
 <link href="../styles.css" rel="stylesheet" />

    <title>Untitled Page</title>   
</head>
<body onload="tamañoLeyendas()" >
    <form id="form1" runat="server">
    <table id="tprinciapl" cellspacing="0" cellpadding="1" style="width: 100%;" >
    <!--<div style="background-color:Red; width:300px; height:400px; top: 0; left:0; z-index:200; position:absolute"> dfsgfg</div>   -->
    <tr>
      <td colspan="2">    
        <iframe marginheight="1" id="iframeMenu" height="20px" width="900px" visible="true" src="SubMenu.aspx" scrolling="no" frameborder=0>       
        </iframe>     
      </td>
    </tr>
    <tr>
    <td id="leyendas" style="margin:0 0 0 0; display:none">
       <iframe height="640" width="0" id="iframeLeyendasVisor"  src="LeyendasVisor.aspx" >
       </iframe>  
    </td>
    <td style="width: 703px">          
                <iframe height="640px" width="900px" id="iframeVisor" scrolling="yes" src="accesoVisorDesdeArbol.aspx?X=<%=Request.querystring("X")%>&y=<%=Request.querystring("Y")%>&zone=30&R=<%=Request.querystring("R")%>" >
            </iframe>  
       
    </td>      
   </tr>
   </table>
   <table style="width: 100%">
   <tr>   
  <td style="border: 1px solid #666666;background-color: #3C5469; height: 12px; text-align: center; color:White; font-weight:bold;" valign="top">
  <img src="../images/flecha_Leyendas.png" id="imgflecha" onclick="OcultarMostrarLeyendas()" style="cursor:hand" /> <p />Leyendas
  </td>
  <td>
  
   <table style="width: 100%;">
      
   <tr>
      <td nowrap style="border: 1px solid #666666;background-color: #cccccc; height: 12px; text-align: right;">Filtros capa Puntos de control  </td>
      <td nowrap style="border: 1px solid #666666;background-color: #cccccc; height: 12px;"  > 
      <asp:LinkButton runat="server" ID="lbVerTodos" Text="Ver todos" OnClick="VerTodos" class="enlaceLecturas"   Font-Bold="true" />
         
      <asp:CheckBox runat="server" ID="chkTelemandos" Text="Ver solo Telegestión" Checked="false" OnCheckedChanged="verTelemandos" AutoPostBack="true" Font-Bold="True" />
      </td>
      <td nowrap style="border: 1px solid #666666;background-color:#cccccc; height: 12px;">
               Visualización puntos: 
                <asp:CheckBox runat="server" ID="chkAcequias" Text="Medida en lámina libre" Checked="true" OnCheckedChanged="verAcequias" AutoPostBack="true" Font-Bold="True"  />
                <asp:CheckBox runat="server" ID="chkMotores" Text="Medida en conducción forzada" Checked="true" OnCheckedChanged="verMotores" AutoPostBack="true" Font-Bold="True"   />
      </td>
   </tr>
    </table>
   <table style="width: 100%">
   <tr>
      <td colspan="2" nowrap style="border: 1px solid #666666;background-color: #cccccc; height: 12px; text-align: right;">Filtros capa Cauces  </td>
      <td nowrap style="border: 1px solid #666666;background-color: #cccccc; height: 12px"  > &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
      <asp:CheckBox runat="server" ID="chkAportacion" Text="Aportaciones" Checked="true" OnCheckedChanged="VerAportacion" AutoPostBack="true" Font-Bold="True" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:CheckBox runat="server" ID="chkEdar" Text="Edar" Checked="true" OnCheckedChanged="VerEdar" AutoPostBack="true" Font-Bold="True" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:CheckBox runat="server" ID="chkGravedad" Text="Gravedad" Checked="true" OnCheckedChanged="VerGravedad" AutoPostBack="true" Font-Bold="True" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:CheckBox runat="server" ID="chkMotor" Text="Motor" Checked="true"  AutoPostBack="true" OnCheckedChanged="VerCauceMotor" Font-Bold="True" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:CheckBox runat="server" ID="chkNoria" Text="Noria" Checked="true" OnCheckedChanged="VerNoria" AutoPostBack="true" Font-Bold="True" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:CheckBox runat="server" ID="chkPozo" Text="Pozo" Checked="true"  OnCheckedChanged="VerPozo" AutoPostBack="true" Font-Bold="True" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:CheckBox runat="server" ID="chkRetorno" Text="Retorno" Checked="true" OnCheckedChanged="VerRetorno" AutoPostBack="true" Font-Bold="True" />

      </td>     
   </tr>
    </table>
    </td>
    </tr>
 </table>
    </form>
</body>
</html>
