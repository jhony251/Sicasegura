<%@ Page Language="C#" AutoEventWireup="true" CodeFile="indexie.aspx.cs" Inherits="SICAH_SeguimientoAdmin_SubidaFichero_post_file" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html >
<head id="Head1" runat="server">
    <title>Upload Files</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Label ID="LBL_listaFicheros" runat="server"></asp:Label>
        
        <asp:Panel ID="PNL_CajaSubida" runat="server">
            <asp:FileUpload ID="FileUpload1" runat="server"  /> &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"   
                Text="Upload File" Height="22px" />&nbsp;<br />
            
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </asp:Panel>
        </div>
    </form>
</body>
</html>