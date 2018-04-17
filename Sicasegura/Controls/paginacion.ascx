<%@ Control Language="VB" AutoEventWireup="false" CodeFile="paginacion.ascx.vb" Inherits="paginacion" %>
<link rel="stylesheet" href="../styles.css" />
<asp:Panel ID=pnlPaginacion Runat=server CssClass=filaPaginacion>
<table cellSpacing=1 cellPadding=0 width="100%">
    <tr>
        <td width="40%" nowrap align="right">
            <asp:LinkButton ID="lbtFirst" Runat=server CommandName=firstPage><img src="<%= ruta%>images/first.gif" border=0 align=absmiddle></asp:LinkButton>
            <asp:LinkButton id=lbtPrev runat="server" CommandName="pagPrev"><img src="<%= ruta%>images/atras.gif" border=0 align=absmiddle></asp:LinkButton>
        </td>
        <td align=center>
            Página
            <asp:DropDownList id=ddlPaginacion runat="server" AutoPostBack="True" Font-Size="7"></asp:DropDownList> de 
            <asp:Label id=lblNumpaginas Text="<%# numpaginas%>" Runat="server"></asp:Label>
        </td>
        <td width="40%" nowrap align="left">
            <asp:LinkButton id=lbtNext runat="server" CommandName="pagSig"><img src="<%= ruta%>images/alante.gif" border=0 align=absmiddle></asp:LinkButton>
            <asp:LinkButton ID=lbtLast Runat=server CommandName=lastPage><img src="<%= ruta%>images/last.gif" border=0 align=absmiddle></asp:LinkButton>
        </td>
    </tr>
</table>
<asp:Label ID=lblPagina Runat=server Visible=false></asp:Label>
<asp:label ID="lblMueve" runat="server" Visible="false" Text="no"></asp:label>
</asp:Panel>