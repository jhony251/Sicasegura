<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones_Arbol.aspx.vb" Inherits="SICAH_Agrupaciones_Arbol" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
      <!-- Estilos tanto para el árbol como para agrupaciones -->
      <link href="../ext-all.css" rel="stylesheet" type="text/css" />  
      
      <!-- referencia librerías para el arbol -->
      <script type="text/javascript" language="javascript" src="../js/ext-base-debug-w-comments.js"></script>
      <script type="text/javascript" language="javascript" src="../js/ext-all-debug.js"></script>                    
      <script type="text/javascript" language="javascript" src="../js/utilesObjetoArbolAgrup.js"></script> 
      <link href="../EstilosArbol.css" rel="stylesheet" type="text/css" />
      
      <!-- referencias para la agrupaciones -->
      
      <link href="../styles.css" rel="stylesheet" type="text/css" />       
             
        <!-- Estilos de página -->
        <link href="../estilosAgrupaciones.css" rel="stylesheet" type="text/css" id="theme" /> 
         
        
</head>
<body style="background-color:#eeead2">

<form runat="server">
    <table cellspacing="2" align="center" width="100%" style="border:1px solid #666666;">
        <tr><td>
            <table cellspacing="0" cellpadding="1" width="100%">
                <tr><td>
                    <table width="100%" height="400">
                        <tr>
                            <td valign="top" style="padding-top: 20px;"><div  id="ContenedorArbol"></div></td>
                        </tr>
                    </table>
                </td>
                </tr>
            </table>
        </td></tr>
    </table>
</form>

</body>
</html>
