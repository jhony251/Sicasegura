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
        <table cellspacing="2" align="center" width="100%" height="100%" cellpadding=2  style="background-color:#eeead2;">
        <tr>
        <td align="left"  colspan="2" width="500" >
        <br /><asp:Label ID=Label1 runat=server CssClass=tituloSec>  DESCRIPCIÓN DEL SISTEMA DE CONTROL DE VOLÚMENES</asp:Label>
        </td>
        </tr>
        <tr>
        <td  style="background-color:#eeead2" colspan="2" width="500">
         <asp:TextBox ID="txtDescripcion" runat="server" width="880px" height="20px" CssClass="texto" TextMode="multiLine" ReadOnly=true></asp:TextBox>
         </td>
        </tr>
         <tr>         
        <td  colspan="2" width="500"><iframe name="iframeAdmin" id="iframeAdmin" src="Agrupaciones_DatosAdmin.aspx" height="360" frameborder="0"   marginheight="0" marginwidth="0" style="width: 890px;background-color:#eeead2"  scrolling="no"  ></iframe></td>
        </tr>
        <tr >
        <td valign="top" >
           <!-- Panel Puntos Agrupacion-->          
                <table cellpadding=0 cellspacing=0  width="500"  >
                <tr>
                <td>
                <asp:label id="lblAgrupacionSel" runat=Server Visible=true ></asp:label>
                <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec><B>ELEMENTOS DEL SCV</B></asp:Label></td>
                </tr>
                    <tr><td width="99%" style="text-align:left"><B>Relación de puntos pertenecientes a la Agrupación:</B></td>                                 
                     </tr>
                        <tr>
                            <td align="right" style="border:1px solid navy;background-color:WhiteSmoke"  >
                             <asp:label id="lblPuntosAgrupacion" runat=Server ></asp:label>
                            </td>
                        </tr>
                        <tr>
        <td>
            <iframe name="iframeTotales" id="iframeTotales"   
                src="Agrupaciones_Totales.aspx" height="350" frameborder="0" marginheight="0" 
                marginwidth="0" class="HTMframe" style="width: 509px; background-color:#eeead2" 
                scrolling="no" ></iframe></td>
        </tr>
                        </table>
         
                <!-- Fin Panel Editar Contadores -->
        </td><td width="500" rowspan="2" valign="top">
            <table cellpadding=0 cellspacing=0 width="100%" >
            <tr><td><iframe name="Grafico1" id="Grafico1" frameborder="0" width="100%" height="240px" scrolling="no" src="grafico.aspx" style="background-color:#eeead2"></iframe>
            </td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr><td><iframe name="Grafico2" id="Grafico2" frameborder="0" width="100%" height="240px" scrolling="no" src="grafico2.aspx" style="background-color:#eeead2"></iframe>
            </td></tr>
            </table>
        </td>
        </tr>
         
        </table>
    </asp:Panel>   
    </form>
</body>
</html>
