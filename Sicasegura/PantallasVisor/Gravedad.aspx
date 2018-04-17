<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Gravedad.aspx.cs" Inherits="Gravedad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CONSULTA DE ATRIBUTOS DE GRAVEDAD</title>
    <link rel=stylesheet href="estilos.css">
    <script language="javascript">
    
        function redimensiona()
        {       
            window.resizeTo(document.getElementById('box').offsetWidth+80,(document.getElementById('box').offsetHeight+200));        
        }

    </script>
<base target="_self" />

</head>

<body onload="redimensiona();"  id="cuerpo">
    <form id="form1" runat="server">
        <fieldset id="box" style="border:2px ridge #CCCC99;padding:8px; width: 288px;">
        <legend align=center style="font-weight:bold; color:black; font-size:14px">Atributos Gravedad</legend>
    <table id="tablaCabecera" style="width: 100px; height: 128px;"> 

    <tr align=left >
        <td style="font-weight:bold; color:#004700; font-size:12px; background-color:#BBBB7C" colspan=2>Datos Gravedad</td>
    </tr>    
	
	<tr>
	    <td style="font-weight:bold; width: 63px;">Código: </td>
	    <td><asp:TextBox ID=txtCodigo align="right" style="width: 173px" runat="server" CssClass="texto"></asp:TextBox>
	    </td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">X: </td>
	    <td ><asp:TextBox ID=txtX align="right" style="width: 173px" runat="server" Height="12px" CssClass="texto"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">Y: </td>
	    <td><asp:TextBox ID=txtY align="right" style="width: 173px" runat="server" Height="12px" CssClass="texto"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">Invent90: </td>
	    <td><asp:TextBox ID=txtInvent90 align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>


    <tr>
        <td style="background-color:#AFAF6; border-collapse:collapse; width: 105px;" colspan=3>&nbsp</td>
    </tr>	
     </table>

        </fieldset>
    </form>
</body>
</html>
