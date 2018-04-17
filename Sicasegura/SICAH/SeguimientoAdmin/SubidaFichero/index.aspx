<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="SICAH_SeguimientoAdmin_SubidaFichero_post_file" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html >
<head id="Head1" runat="server">
    <title>Upload Files</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label runat="server" ID="LBL_IdCarpeta"></asp:Label>
    <table style="width:100%;">
        <tr>
            <td align="center">&nbsp;</td>
            <td align="center">
                <asp:ImageButton ID="ImageButton1" runat="server" 
                    ImageUrl="~/SICAH/SeguimientoAdmin/images/Upload.png" 
                    onclick="ImageButton1_Click" /></td>
            <td align="center">
                &nbsp;</td>
            <td align="center">
                <asp:ImageButton ID="ImageButton2" runat="server" 
                    ImageUrl="~/SICAH/SeguimientoAdmin/images/Consultar.png" 
                    onclick="ImageButton2_Click" /></td>
            <td align="center">&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td align="center">
                <asp:Label ID="LBL_NumFicheros" runat="server"></asp:Label>
            </td>
            <td></td>
        </tr>
    </table>
    </form>
</body>
</html>