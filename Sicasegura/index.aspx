<%@ Page Language="VB" AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="index" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title>Sistema Integrado de Control de Aprovechamientos</title>
<link rel="stylesheet" href="styles.css">

<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
<meta name=vs_defaultClientScript content="JavaScript">
<meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body bgcolor="#EEEAD2" onload="document.getElementById('txtLogin').focus();" >
<form id="Form1" method="post" runat="server">

<table cellspacing=2 align=center width=100% height='100%' style="border:1px solid #666666;background-color:white">
<tr><td>
<table cellspacing=0 cellpadding=1 width=100%>
<asp:label ID="lblCabecera" Runat="server"></asp:label>
<asp:label ID="lblPestanyas" Runat="server"></asp:label></table>
</td></tr>
<tr><td align="center" height='100%' valign=middle>
<table width=500 align="top" height="400" style="background-image: url(images/llaves.jpg); background-repeat:no-repeat;">
<!-- Celda Men� - Contenido -->
<tr>
    <!-- Celda Contenido -->
    <td style="padding-left:170px;padding-right:20px;" valign="top">                   
    <table cellspacing=0 cellpadding=0> 
        <tr>
            <td><br /><br /><br /><br /><br /><br /><br /><br /><br /></td>
        </tr>  
        <tr>
            <td><br /><br /><br /><br /><br /></td>
        </tr>           
        <tr>
            <td align="left">Login</td>
            <td align="left">
            <asp:TextBox ID="txtLogin" runat="server" CssClass="texto"></asp:TextBox>
            </td>                                
        </tr>                
        <tr>
            <td align="left">Password</td>
            <td align="left">
            
            <asp:TextBox ID="txtPassword" runat="server" CssClass="texto" TextMode="password" EnableViewState="false" ></asp:TextBox>
            </td>                                
        </tr>                
    
        <tr>
            <td>&nbsp;</td>
            <td align="left" style="padding-top:15px;" nowrap>                
            <asp:Button ID="btnAceptar" runat="server" class="boton" Text="Aceptar" />
            <asp:Button ID="btnCancelar" runat="server" class="boton" Text="Cancelar" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td align="left" style="padding-top:15px;">         
                <asp:Button ID="btnCambiarP" runat="server" class="boton" Text="Cambiar Password" Width=135px />
            </td>
        </tr>
        <tr>
        <td colspan="2">&nbsp;</td>
        </tr> 
        <tr>
            <td>&nbsp;</td>
            <td colspan="2" style="color:Red">
                <asp:Label ID="lblClaveIncorrecta" runat="server"></asp:Label>
            </td>
        </tr>               
    </table>
    </td>
    <!-- Fin Celda Contenido -->
</tr>
<!-- Fin Celda Men� - Contenido -->
  <tr style="overflow: scroll">
      <td style="width: 703px">
        <iframe height="0px" width="0px"  runat="server" id="iframeInicio" visible="true" scrolling="yes" >        
        </iframe>   
        </td>
        </tr>   

</table></td></tr>
</table> 
        
</td></tr></table>

</form>
</body>
</html>