<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ActualizadorXMLArbolAsync.aspx.vb" Inherits="SICAH_ActualizadorXMLArbolAsync" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />    
    
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
    <!--<script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>-->
    <script type="text/jscript" language="javascript" src="../js/utiles.js"></script>
    <script type="text/jscript" language="javascript" src="../js/utilesInterfazDinamica.js"></script>
    

    <link href="../styles.css" rel="stylesheet" />
</head>
<body  bgcolor="#EEEAD2">
    <form id="form1" runat="server">
    <table width="100%" cellspacing="2" align="center" style="border: 1px solid #666666;background-color: white">
       <tr>
        <td>
           <table cellspacing="0" cellpadding="1" width="100%">
           <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label><tr>
              <td>
                <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <!-- Celda Menú - Contenido -->
                <tr>
                <!-- Celda Menú -->
                   <!--<td colspan="6"valign="top" style="padding-top: 20px; width:20%">-->     
                     <%=genHTMLMenus.generaMenuMtoSica(Session("idperfil"), Application("PVYCR_Arbol_Updated"))%>
                   <!--</td>-->
                   <!-- <td style="background-image:url(../images/dot2.gif);width:3px"></td>-->
                   <!-- Fin Celda Menú -->
                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%" valign=top>
                        <!-- Panel listar Elementos -->                      
                        <table>
                            <tr>
                                <td style="width: 8px; height: 28px">
                                    <asp:ImageButton ID="imgUpdateEstructuraArbol" runat="server" ImageUrl="~/SICAH/images/Proceso_Run.gif" /></td>
                                <td style="width: 595px; color: black; font-family: Verdana; height: 28px">
                                    &nbsp;<asp:Label ID="lblMensajeBoton" runat="server" Text="Iniciar la Actualización"></asp:Label></td>
                            </tr>
                        </table>
                        &nbsp;<!-- Fin Panel listar elementos --><!-- Panel Editar  elementos-->
                        <!-- Fin Panel Editar Contadores -->
                    </td>
                    <!-- Fin Celda Contenido -->        
                </tr>
                <!-- Fin Celda Menú - Contenido -->
                   

        
</table></td></tr   >
          </table> 
        
</td></tr></table>
    </form>
</body>
</html>

