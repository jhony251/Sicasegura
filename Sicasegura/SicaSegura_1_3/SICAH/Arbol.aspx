<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Arbol.aspx.vb" Inherits="SICAH_Arbol" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
  <!-- Referencia Librerías de JScripts al nuevo objeto arbol, no cambiar orden de las librerias o no funciona -->
    <!--<script src="http://192.168.41.3:8081/includes/ext/adapter/ext/ext-base-debug-w-comments.js" type="text/javascript"></script>-->
    <!--<script src="http://192.168.41.3:8081/includes/ext/ext-all-debug.js" type="text/javascript"></script>   -->
    <!--<link href="http://192.168.41.3:8081/includes/ext/resources/css/ext-all.css" rel="stylesheet" type="text/css" />-->
    
    <script  type="text/javascript" language="javascript" src="../js/ext-base-debug-w-comments.js"></script>
    <script  type="text/javascript" language="javascript" src="../js/ext-all-debug.js"></script>    
    <link href="../ext-all.css" rel="stylesheet" type="text/css" />
    <!--<link href="http://192.168.41.3:8081/includes/ext/resources/css/reset-min.css" rel="stylesheet" type="text/css" />-->
    <link href="../EstilosArbol.css" rel="stylesheet" type="text/css" />
    <script type="text/jscript" language="javascript" src="../js/utilesObjetoArbol.js"></script>    

    
    
</head>
<body>
    <form id="form1" runat="server">  
    <div id="ContenedorBusqueda"></div>
    <div id="ContenedorArbol"></div>
    </form>
</body>
</html>
