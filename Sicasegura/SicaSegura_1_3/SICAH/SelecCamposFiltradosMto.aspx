<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelecCamposFiltradosMto.aspx.vb" Inherits="SICAH_SelecCamposFiltradosMto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informe Datos Filtrados</title>
    <script language="javascript" type="text/javascript">
    function redimensiona(){        
        //window.resizeTo(document.getElementById('tablacabecera').offsetWidth + 50, (document.getElementById('cuerpo').offsetHeight * 1.2));
        window.resizeTo(document.getElementById('tablacabecera').offsetWidth + 50, document.getElementById('cuerpo').offsetHeight+50);
        window.moveTo(260, 260);        
           }
    
</script>   

</head>
<body onload="redimensiona();" id="cuerpo" style="background-color:#EAE2BD;">
    <form id="form1" runat="server">    
    <table id="tablacabecera">
    <tr><td style="font: 11px Tahoma;">
        <asp:CheckBoxList ID="chklMtos" runat="server" AutoPostBack="True" >
        </asp:CheckBoxList>
    </td></tr>
    </table>
    <table width="100%">
    <tr><td align="right">      
        <asp:Button ID="btnAceptar" runat="server" cssclass="boton" Text="Aceptar" />
        <asp:Button ID="btnCancelar" runat="server" cssclass="boton" Text="Cancelar"  />
    </td></tr>
    </table>
    
    </form>
</body>
</html>
