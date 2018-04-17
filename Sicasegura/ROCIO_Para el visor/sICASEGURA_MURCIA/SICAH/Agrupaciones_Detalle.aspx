<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones_Detalle.aspx.vb" Inherits="SICAH_Agrupaciones_Detalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
      <!-- Estilos tanto para el árbol como para agrupaciones -->
      <link href="../ext-all.css" rel="stylesheet" type="text/css" />  
       <!-- referencias para la agrupaciones -->
      
      <link href="../styles.css" rel="stylesheet" type="text/css" />       
             
        <!-- Estilos de página -->
        <link href="../estilosAgrupaciones.css" rel="stylesheet" type="text/css" id="theme" /> 
<script language=javascript>

//    function Cargar() {
//        var a;
//        if (<%=request.Querystring("idAgrupacion") %>==null)
//            a=1;
//            else
//            a=<%=request.Querystring("idAgrupacion") %>;
//        alert(a);
//    

</script>
</head>
<body style="background-color:#eeead2"> 
    <form id="form1" runat="server">
        <asp:Panel ID="pnlAgrupacionConInscripcion" Runat="server" Visible="true" >
        <table cellspacing="2" align="center" width="100%" height="100%" cellpadding=2  style="background-color:#eeead2;" >
        <tr>
        <td align="left"   >
        <br /><asp:Label ID=Label1 runat=server CssClass=tituloSec>  DESCRIPCIÓN AGRUPACIÓN</asp:Label>
        </td>
        </tr>
        <tr>
        <td colspan="4" style="background-color:#eeead2">
         <asp:TextBox ID="txtDescripcion" runat="server" width="880px" height="20px" CssClass="texto" TextMode="multiLine" ReadOnly=true></asp:TextBox>
         </td>
        </tr>
         <tr>         
        <td ><iframe name="iframeAdmin" id="iframeAdmin"   src="Agrupaciones_DatosAdmin.aspx" height="360" frameborder="0"   marginheight="0" marginwidth="0" style="width: 890px;background-color:#eeead2"  scrolling="no"  ></iframe></td>
        </tr>
        <tr style="background-color:#eeead2">
        <td valign="top" >
           <!-- Panel Puntos Agrupacion-->
                <table cellpadding=0 cellspacing=0 width=100% >
                <tr>
                <td>
                <asp:label id="lblAgrupacionSel" runat=Server Visible=true ></asp:label>
                <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec><B>CONTENIDO AGRUPACIÓN</B></asp:Label></td>
                </tr>
                    <tr>
                            <td width="100%" style="text-align:left"><B>Relación de puntos pertenecientes a la Agrupación:</B></td>                                  
                           
                     </tr>
                     </table>
                    <table cellspacing=2 align="center" width= "880x" cellpadding=2 style="border:1px solid navy;background-color:WhiteSmoke"  >                        
                        <tr>
                            <td align="right" BackColor="WhiteSmoke"  >
                             <asp:label id="lblPuntosAgrupacion" runat=Server ></asp:label>
                            </td>
                        </tr>
                        </table>
         
                <!-- Fin Panel Editar Contadores -->
        </td>
        </tr>
         <tr>
        <td ><iframe name="iframeTotales" id="iframeTotales"   src="Agrupaciones_Totales.aspx" height="120" frameborder="0" marginheight="0" marginwidth="0" class="HTMframe" style="width: 890px;background-color:#eeead2" scrolling="no" ></iframe></td>
        </tr>
       
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlAgrupacionesSinInscrip" Runat="server" Visible="false" >
    <table cellspacing="2" align="center" width="100%" height="300" cellpadding=2  >
    <tr><td height=150 >&nbsp;</td></tr>
    <tr valign="middle">
    <td valign="middle" align="center" height="19" style="background-color:white; border:1px solid black">No hay información disponible</td>
    </tr>
    </table>
    </asp:Panel>
    </form>
</body>
</html>
