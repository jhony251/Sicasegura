<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TiposObtencionCaudal.aspx.vb" Inherits="SICAH_TiposObtencionCaudal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Tipos Obtención Caudales</title>
<link href="../styles.css" rel="stylesheet" />
</head>
<body style="margin:0px; padding:2px">
    <form id="form1" runat="server">
    <div>
    <table bgcolor="#EFE7BE" border="4px solid gray">
        <tr style="background-color:#cccccc; font-weight:bold"><td style="border:1px solid #666666">Tipo Obtención Caudal</td><td style="border:1px solid #666666">Descripción</td></tr>
        <asp:Repeater ID="rptTipos" runat="server">
        <ItemTemplate>
        <tr><td align="center" class="preproduccion"><%#Container.DataItem("TipoObtencionCaudal")%><td class="preproduccion"><%#Container.DataItem("Descripcion")%></td></tr>
        </ItemTemplate>
        </asp:Repeater>
       
    </table>
    </div>
    </form>
    </body>
</html>
