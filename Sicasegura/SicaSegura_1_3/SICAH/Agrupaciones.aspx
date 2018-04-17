<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones.aspx.vb" Inherits="SICAH_Agrupaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
      <!-- Estilos tanto para el árbol como para agrupaciones -->
      <link href="../ext-all.css" rel="stylesheet" type="text/css" />  
      
      <!-- referencia librerías para el arbol -->
      <script type="text/javascript" language="javascript" src="../js/ext-base-debug-w-comments.js"></script>
      <script type="text/javascript" language="javascript" src="../js/ext-all-debug.js"></script>                    
      <script type="text/javascript" language="javascript" src="../js/utilesObjetoArbol.js"></script> 
      <link href="../EstilosArbol.css" rel="stylesheet" type="text/css" />
      
      <!-- referencias para la agrupaciones -->
      <script src="../js/Agrupaciones.js" type="text/javascript"></script> 
      <script src="../js/RowEditor.js" type="text/javascript"></script>    
      
      <script src="../js/DragDrop.js" type="text/javascript"></script>   
      
      <link href="../styles.css" rel="stylesheet" type="text/css" />       
             
        <!-- Estilos de página -->
        <link href="../estilosAgrupaciones.css" rel="stylesheet" type="text/css" id="theme" /> 
         
        
</head>
<body bgcolor="#EEEAD2">

<form runat="server">
    <table cellspacing="2" align="center" width="100%" style="border:1px solid #666666;background-color:white">
        <tr><td>
            <table cellspacing="0" cellpadding="1" width="100%">
                <asp:label ID="lblCabecera" Runat="server"></asp:label>
                <asp:label ID="lblPestanyas" Runat="server"></asp:label>
                <tr><td>
                    <table width="100%" height="400">
                        <tr>
                            <td valign="top" style="padding-top: 20px;"><div  id="ContenedorArbol"></div></td>
                            <td align="center"><div id="grid_agrupaciones"></div></td>
                        </tr>
                    </table>
                </td></tr>
            </table>
        </td></tr>
    </table>
</form>

</body>
</html>
