<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SubMenu.aspx.vb" Inherits="SICAH_SubMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <link href="../styles.css" rel="stylesheet" />
    <title>Untitled Page</title>
    <script language="javascript" src="..\js/utilesMenus.js"></script>  
</head>
<body>
    <form id="form1" runat="server">
      <table width="100%">
                     <tr>
                        <asp:Label ID="lblPestanyasArbol" runat="server"></asp:Label>                                               
                       <td width=50%>
                           <strong>
                               <asp:TextBox ID="txtCodigoPVYCR" runat="server" CssClass="texto" ToolTip="CodigoPVYCR"
                                   Width="115px" Visible="false">[CodigoPVYCR]</asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCodigoPVYCR"
                                   Display="Dynamic" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator></strong>
                                   <asp:TextBox ID="txtIdElementoMedida" runat="server" CssClass="texto" Width="27px" ToolTip="Id Elemento Medida" Visible="false">[EM]</asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtIdElementoMedida" Display="Dynamic">*</asp:RequiredFieldValidator>&nbsp;
                              <asp:TextBox ID="txtDescripcionElementoMedida" runat="server" CssClass="texto" Visible="False"
                               Width="44px"></asp:TextBox><asp:Label ID="LBLinfo" runat="server" Width="0px" Visible="false"></asp:Label>
                           
                            <b><asp:LinkButton ID="lbtAceptar" runat="server" Text="Búsqueda Rápida" CssClass="enlaceLecturas" Visible="false"></asp:LinkButton></b>
                              <asp:ImageButton ID="imgVisor" runat="server" ImageUrl="~/SICAH/images/iconos/imgVisor.gif"
                               ImageAlign="TextTop" Visible="false"/>
                         </td>
                               

                      </tr>
        </table>
    </form>
</body>
</html>
