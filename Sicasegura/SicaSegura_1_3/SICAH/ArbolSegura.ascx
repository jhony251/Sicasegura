<%@ Control Language="VB" AutoEventWireup="true" CodeFile="ArbolSegura.ascx.vb"
  Inherits="ArbolSegura" %>
<table width="100%" id="tableTreeView" cellpadding="0" cellspacing="0">
<tr><td>
  <asp:TreeView ID="treeView1" runat="server">
    <LevelStyles>
      <asp:TreeNodeStyle Font-Bold="true" ForeColor="#003C00" Font-Underline="False" />
      <asp:TreeNodeStyle Font-Bold="true" ForeColor="Darkgreen" />
      <asp:TreeNodeStyle ForeColor="Darkgreen" />
    </LevelStyles>
    <NodeStyle ForeColor="green" />
  </asp:TreeView>
  </td></tr>
</table>
