<%@ Page Language="C#" AutoEventWireup="true" CodeFile="captacion.aspx.cs" Inherits="captacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CONSULTA DE ATRIBUTOS DE CAPTACIÓN</title>
    <link rel=stylesheet href="estilos.css">
    <script language="javascript">
    
        function redimensiona()
        {       
            window.resizeTo(document.getElementById('box').offsetWidth+80,(document.getElementById('box').offsetHeight+180));        
        }

    </script>
<base target="_self" />

</head>

<body onload="redimensiona();"   id="cuerpo">
    <form id="form1" runat="server">
        <fieldset id="box" style="border:2px ridge #CCCC99;padding:8px; width: 312px;">
        <legend align=center style="font-weight:bold; color:black; font-size:14px">Atributos Captación</legend>
    <table id="tablaCabecera" style="width: 100px; height: 132px;"> 

    <tr align=left >
        <td style="font-weight:bold; color:#004700; font-size:12px; background-color:#BBBB7C" colspan=2>Datos Captación</td>
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
	    <td style="font-weight:bold; width: 63px;">Huso: </td>
	    <td><asp:TextBox ID=txthuso align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">Cartógrafo: </td>
	    <td><asp:TextBox ID=txtcartografo align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
    <tr>
	    <td style="font-weight:bold; width: 63px;">Expediente: </td>
	    <td><asp:TextBox ID=txtexpediente align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	
	<tr>
	    <td style="font-weight:bold; width: 63px;">Nº Captación: </td>
	    <td><asp:TextBox ID=txtnumcaptacion align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

	
    <tr>
	    <td style="font-weight:bold; width: 63px;">Municipio: </td>
	    <td><asp:TextBox ID=txtmunicipio align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	    <tr>
	    <td style="font-weight:bold; width: 63px;">Provincia: </td>
	    <td><asp:TextBox ID=txtprovincia align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">Estado: </td>
	    <td><asp:TextBox ID=txtestado align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">Fiab. Precisión: </td>
	    <td><asp:TextBox ID=txtfiabprec align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
    <tr>
	    <td style="font-weight:bold; width: 63px;">Fiab. Asociada: </td>
	    <td><asp:TextBox ID=txtfiabasoci align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	
	<tr>
	    <td style="font-weight:bold; width: 63px;">Procedencia: </td>
	    <td><asp:TextBox ID=txtprocedencia align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

    <tr>
	    <td style="font-weight:bold; width: 63px;">Tipo Captación: </td>
	    <td><asp:TextBox ID=txttipocap align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>


	<tr>
	    <td style="font-weight:bold; width: 63px;">Tipo Catastro: </td>
	    <td><asp:TextBox ID=txttipocatastro align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

	<tr>
	    <td style="font-weight:bold; width: 63px;">Captación: </td>
	    <td><asp:TextBox ID=txtcaptacion align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

	<tr>
	    <td style="font-weight:bold; width: 63px;">Situación: </td>
	    <td><asp:TextBox ID=txtsituacion align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
		
	<tr>
	    <td style="font-weight:bold; width: 63px;">Caudal Máximo: </td>
	    <td><asp:TextBox ID=txtcaudal align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	
	<tr>
	    <td style="font-weight:bold; width: 63px;">Volumen Máximo Anual: </td>
	    <td><asp:TextBox ID=txtvolumen align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	
    <tr>
	    <td style="font-weight:bold; width: 63px;">Diámetro: </td>
	    <td><asp:TextBox ID=txtdiametro align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

    <tr>
	    <td style="font-weight:bold; width: 63px;">Profundidad: </td>
	    <td><asp:TextBox ID=txtprofundidad align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	
	 <tr>
	    <td style="font-weight:bold; width: 63px;">Instalación: </td>
	    <td><asp:TextBox ID=txtinstala align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

    <tr>
	    <td style="font-weight:bold; width: 63px;">Potencia: </td>
	    <td><asp:TextBox ID=txtpotencia align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

    <tr>
	    <td style="font-weight:bold; width: 63px;">Tipo Contador: </td>
	    <td><asp:TextBox ID=txttipocontador align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

    <tr>
	    <td style="font-weight:bold; width: 63px;">Fecha: </td>
	    <td><asp:TextBox ID=txtfecha align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

	
    <tr>
	    <td style="font-weight:bold; width: 63px;">BW: </td>
	    <td><asp:TextBox ID=txtbw align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

	
    <tr>
	    <td style="font-weight:bold; width: 63px;">CIF: </td>
	    <td><asp:TextBox ID=txtcif align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	
	<tr>
	    <td style="font-weight:bold; width: 63px;">Nombre: </td>
	    <td><asp:TextBox ID=txtnombre align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	
    <tr>
	    <td style="font-weight:bold; width: 63px;">Apellido 1: </td>
	    <td><asp:TextBox ID=txtapellido1 align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
    <tr>
	    <td style="font-weight:bold; width: 63px;">Apellido 2: </td>
	    <td><asp:TextBox ID=txtapellido2 align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>


    <tr>
        <td style="background-color:#AFAF6; border-collapse:collapse; width: 105px;" colspan=3>&nbsp</td>
    </tr>	
     </table>

        </fieldset>
    </form>
</body>
</html>
