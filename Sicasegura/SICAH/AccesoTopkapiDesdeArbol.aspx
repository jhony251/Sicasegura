<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccesoTopkapiDesdeArbol.aspx.vb" Inherits="SICAH_AccesoTopkapiDesdeArbol" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<script type="text/javascript">
 function cargarEnlace(){
 var result = document.getElementById("lblEnlace");
 document.getElementById("iframeVisor").src=result.innerText;
 }
</script>
 <link href="../styles.css" rel="stylesheet" />
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
</head>
<body onload="cargarEnlace()" >
<asp:Label ID="lblEnlace" runat="server" style="display:none"></asp:Label>
    <form id="form1" runat="server">
    <table id="tprinciapl" cellspacing="0" cellpadding="1" style="width:89%" >
    <!-- Cabecera -->
     <tr>
              <td style="width: 900px">
                 <table width="100%">
                     <tr>
                       <asp:Label ID="lblPestanyasArbol" runat="server"></asp:Label><td width=50% visible="false" >
                           
                       </td>   

                 </tr>
                  
                  </table>
                  
              </td>
                              
             </tr>

    <tr style="overflow: scroll">
      <td style="width: 703px">
        <iframe height="0px" width="0px"  runat="server" id="iframeInicio" visible="true" scrolling="yes" >        
        </iframe>              
        <br />
        <iframe height="700px" width="895px"  runat="server" id="iframeVisor" scrolling="yes" visible="true" style="overflow:auto"  >
        </iframe>                 
      </td>
   </tr>
   
   </table>
    </form>
</body>

</html>

