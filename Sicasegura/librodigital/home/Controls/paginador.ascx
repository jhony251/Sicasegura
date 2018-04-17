<%@ control language="C#" autoeventwireup="true" inherits="valControls.paginador, App_Web_paginador.ascx.6e49e4bf" classname="paginador" %>

<%
    /*

    CONTROL DE PAGINACI�N PARA ASP.NET
    JOS� FERN�NDEZ MART�NEZ - 18/01/2007
    Estilos del control, a definir si se quiere personalizar la presentaci�n:
    -botCambioPag: clase de estilo para los botones de p�gina previa, siguiente, primera y �ltima
    -despPaginas: clase de estilo para el desplegable de p�ginas
    -tablaPaginacion: clase de estilo para la tabla donde va el control
    -panelPaginacion: clase de estilo del panel contenedor global (que se traducir� en un div o un span)
    -celdaIzqda: clase de estilo de la celda izquierda, con los botones de retroceder p�gina
    -celdaCentral: clase de estilo de la celda central, que contiene el desplegable de p�ginas
    -celdaDcha: clase de estilo de la celda derecha, con los botones de avanzar p�gina

    */
%>

<asp:Panel ID="pnlPaginador" runat="server" CssClass="panelPaginacion">
<table width="100%" cellspacing="0" cellpadding="2" class="tablaPaginacion">
<tr>
<td width="1%" nowrap class="celdaIzqda">
<b>
<asp:LinkButton ID="lbtFirst" runat="server" OnClick="lbtFirst_Click" CssClass="botCambioPag">&lt;&lt;</asp:LinkButton>
&nbsp;
<asp:LinkButton ID="lbtPrev" runat="server" OnClick="lbtPrev_Click" CssClass="botCambioPag">&lt;</asp:LinkButton>
</b>
</td>
<td align="center" class="celdaCentral">
    P�gina
    <asp:DropDownList ID="ddlNumPagina" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNumPagina_SelectedIndexChanged" EnableViewState="true" CssClass="despPaginas"></asp:DropDownList> 
    de 
    <asp:Label ID="lblNumPaginas" runat="server"></asp:Label>
    
    <asp:Label ID="lblSentSQLOrig" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblItemsPorPagina" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblNombreTabla" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblNombreControl" runat="server" Visible="false"></asp:Label>
    
</td>
<td width="1%" nowrap align="right" class="celdaDcha">
<b>
<asp:LinkButton ID="lbtNext" runat="server" OnClick="lbtNext_Click" CssClass="botCambioPag">&gt;</asp:LinkButton>
&nbsp;
<asp:LinkButton ID="lbtLast" runat="server" OnClick="lbtLast_Click" CssClass="botCambioPag">&gt;&gt;</asp:LinkButton>
</b>
</td>
</tr>
</table>
</asp:Panel>
