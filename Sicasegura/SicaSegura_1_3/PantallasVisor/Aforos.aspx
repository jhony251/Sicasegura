<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Aforos.aspx.cs" Inherits="PantallasVisor_Aforos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
      <title>CONSULTA DE ATRIBUTOS DE AFOROS</title>
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
        <legend align=center style="font-weight:bold; color:black; font-size:14px">Atributos Aforos</legend>
    <table id="tablaCabecera" style="width: 100px; height: 132px;"> 

    <tr align=left >
        <td style="font-weight:bold; color:#004700; font-size:12px; background-color:#BBBB7C" colspan="2">Datos Aforos</td>
    </tr>    
	
	<tr>
	    <td style="font-weight:bold; width: 63px;">Código CHS: </td>
	    <td ><asp:TextBox ID="txtCod_chs" align="right" style="width: 173px" runat="server" Height="12px" CssClass="texto"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">Código Propio: </td>
	    <td><asp:TextBox ID="txtCod_propio" align="right" style="width: 173px" runat="server" Height="12px" CssClass="texto"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">X UTM OF: </td>
	    <td><asp:TextBox ID="txtX_UTM_OF" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">Y UTM OF: </td>
	    <td><asp:TextBox ID="txtY_UTM_OF" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
    <tr>
	    <td style="font-weight:bold; width: 63px;">Z UTM OF: </td>
	    <td><asp:TextBox ID="txtZ_UTM_OF" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	
	<tr>
	    <td style="font-weight:bold; width: 63px;">Nombre: </td>
	    <td><asp:TextBox ID="txtNombre" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	    <tr>
	    <td style="font-weight:bold; width: 63px;">Paraje: </td>
	    <td><asp:TextBox ID="txtparaje" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>	
    <tr>
	    <td style="font-weight:bold; width: 63px;">Municipio: </td>
	    <td><asp:TextBox ID="txtmunicipio" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">Cauce: </td>
	    <td><asp:TextBox ID="txtCauce" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 63px;">Acuífero: </td>
	    <td><asp:TextBox ID="txtacuifero" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
    <tr>
	    <td style="font-weight:bold; width: 63px;">Comentario: </td>
	    <td><asp:TextBox ID="txtComentario" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
	
	<tr>
	    <td style="font-weight:bold; width: 63px;">Reseña: </td>
	    <td><asp:TextBox ID="txtresena" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

    <tr>
	    <td style="font-weight:bold; width: 63px;">Propietario: </td>
	    <td><asp:TextBox ID="txtpropietari" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>


	<tr>
	    <td style="font-weight:bold; width: 63px;">Inicio Uso: </td>
	    <td><asp:TextBox ID="txtinicio_uso" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

	<tr>
	    <td style="font-weight:bold; width: 63px;">Fin Uso: </td>
	    <td><asp:TextBox ID="txtFin_uso" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>

	<tr>
	    <td style="font-weight:bold; width: 63px;">Código BD: </td>
	    <td><asp:TextBox ID="txtCodigo_BD" align="right" runat="server" CssClass="texto" Height="12px" Width="173px"></asp:TextBox></td>
	</tr>
		
    <tr>
        <td style="background-color:#AFAF6; border-collapse:collapse; width: 105px;" colspan=3>&nbsp</td>
    </tr>	
     </table>

        </fieldset>
    </form>
</body>
</html>
