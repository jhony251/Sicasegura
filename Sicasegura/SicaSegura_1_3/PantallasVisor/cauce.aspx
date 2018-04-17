<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cauce.aspx.cs" Inherits="cauce" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CONSULTA DE ATRIBUTOS DEL CAUCE</title>
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
        <fieldset id="box" style="border:2px ridge #CCCC99;padding:8px; width: 584px;">
        <legend align=center style="font-weight:bold; color:black; font-size:14px">Atributos Cauce</legend>
        
    <table id="tablaCabecera" style="width: 100px; height: 132px;"> 
    
    <tr align="center" >
        <td style="font-weight:bold; color:#004700; font-size:12px; background-color:#BBBB7C" colspan=2>Datos Cauce</td>
    </tr>    
	
	<tr>
	    <td style="font-weight:bold; width: 99px;">Código Cauce: </td>
	    <td ><asp:TextBox ID="txtcodigocauce" align="right" style="width: 173px" runat="server"  CssClass="texto"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Denominación: </td>
	    <td><asp:TextBox ID="txtdenominacion" align="right" style="width: 173px" runat="server" CssClass="texto"></asp:TextBox></td>

	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">Código Inventario: </td>
	    <td><asp:TextBox ID="txtinventario" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Código Campo: </td>
	    <td><asp:TextBox ID="txtcodcampo" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>
    <tr>
	    <td style="font-weight:bold; width: 99px;">Tipo Cauce: </td>
	    <td><asp:TextBox ID="txttipocauce" align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Margen: </td>
	    <td><asp:TextBox ID="txtmargen" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>
    <tr>
	    <td style="font-weight:bold; width: 99px;">Otras Referencias: </td>
	    <td><asp:TextBox ID="txtotrasref" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Paraje: </td>
	    <td><asp:TextBox ID="txtparaje" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">Municipio: </td>
	    <td><asp:TextBox ID="txtmunicipio" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Provincia: </td>
	    <td><asp:TextBox ID="txtprovincia" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>
    <tr>
	    <td style="font-weight:bold; width: 99px;">Administrador: </td>
	    <td><asp:TextBox ID="txtadministrador" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Titular: </td>
	    <td><asp:TextBox ID="txttitular" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>
    <tr>
	    <td style="font-weight:bold; width: 99px;">NIF: </td>
	    <td><asp:TextBox ID="txtnif" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Dirección: </td>
	    <td><asp:TextBox ID="txtdireccion" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">Municipio: </td>
	    <td><asp:TextBox ID="txtmunicipiotit" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Cód. Postal: </td>
	    <td><asp:TextBox ID="txtcodpostal" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>		
	<tr>
	    <td style="font-weight:bold; width: 99px;">Provincia: </td>
	    <td><asp:TextBox ID="txtprovinciatit" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Teléfono: </td>
	    <td><asp:TextBox ID="txttel" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>
	
    <tr>
	    <td style="font-weight:bold; width: 99px;">Ref. Expediente: </td>
	    <td><asp:TextBox ID="txtrefexp" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Ref. Exptes. Registro: </td>
	    <td><asp:TextBox ID="txtrefexpreg" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">Nº Registro Antiguo: </td>
	    <td><asp:TextBox ID="txtregantiguo" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Otros Expedientes: </td>
	    <td><asp:TextBox ID="txtotrosexp" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>

    <tr>
	    <td style="font-weight:bold; width: 99px;">Sección: </td>
	    <td><asp:TextBox ID="txtseccion" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Tomo: </td>
	    <td><asp:TextBox ID="txttomo" align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>

    <tr>
	    <td style="font-weight:bold; width: 99px;">Hoja: </td>
	    <td><asp:TextBox ID=txthoja align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Nombre Contacto: </td>
	    <td><asp:TextBox ID=txtcontacto align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>
	
	<tr>
	    <td style="font-weight:bold; width: 99px;">Dirección: </td>
	    <td><asp:TextBox ID=txtdireccontacto align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Teléfono: </td>
	    <td><asp:TextBox ID=txttelcontacto align="right" runat="server" CssClass="texto"  Width="173px"></asp:TextBox></td>
	</tr>
    <tr>
	    <td style="font-weight:bold; width: 99px;">Caudal Máx(l/s): </td>
	    <td><asp:TextBox ID=txtcaudalmax align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Volumen Máx. Anual Legal(m3): </td>
	    <td><asp:TextBox ID=txtvolmaxlegal align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">Volumen Máx. Anual Teórico(m3): </td>
	    <td><asp:TextBox ID=txtvolmaxteorico align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">X: </td>
	    <td><asp:TextBox ID=txtx align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">Y: </td>
	    <td><asp:TextBox ID=txty align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Cota Toma: </td>
	    <td><asp:TextBox ID=txtcotatoma align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">Superficie Real Aproximada(has): </td>
	    <td><asp:TextBox ID=txtsupreal align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Superficie Inscrita(has): </td>
	    <td><asp:TextBox ID=txtsupinscrita align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">Porcentaje Tradicional: </td>
	    <td><asp:TextBox ID=txtporcentaje align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Tipo Cultivo: </td>
	    <td><asp:TextBox ID=txttipocultivo align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">Longitud Cauce(km): </td>
	    <td><asp:TextBox ID=txtlongitud align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Entre ojos y contraparada: </td>
	    <td><asp:TextBox ID=txtentreojos align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>

	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">En Activo: </td>
	    <td><asp:TextBox ID=txtenactivo align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Medido en PVYCR: </td>
	    <td><asp:TextBox ID="txtmedido" align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>

	</tr>
	<tr>
	    <td style="font-weight:bold; width: 99px;">Titular Contactado: </td>
	    <td><asp:TextBox ID="txttitularcontac" align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	    <td style="font-weight:bold; width: 99px;">Contador OK: </td>
	    <td ><asp:TextBox ID="txtcontadorok" align="right" runat="server" CssClass="texto" Width="173px"></asp:TextBox></td>
	</tr>
	
     </table>

        </fieldset>
    </form>

</body>
</html>
