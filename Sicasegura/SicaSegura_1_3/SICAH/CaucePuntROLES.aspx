<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CaucePuntROLES.aspx.vb" Inherits="SICAH_CaucePuntROLES" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<script>
    var gblPermisos;
    function getCookie(name)
    {
      var cname = name + "=";               
      var dc = document.cookie;             
      if (dc.length > 0) {              
        begin = dc.indexOf(cname);       
        if (begin != -1) {           
          begin += cname.length;       
          end = dc.indexOf(";", begin);
          if (end == -1) end = dc.length;
            return unescape(dc.substring(begin, end));
        } 
      }
      return null;
    }
    
    function mostrarRamasROLES2()
    {
       
    //EGB 10/03/2009 Mostrar/OcultarRamas en Funcion del rol de usuario sin COOKIES
     var i;
     var IdsCliente=top.frames['iframeArbol'].document.getElementById("IdsCliente").value;
     var arrayIdsCliente = IdsCliente.split("#"); 
    
     glbPermisos = document.getElementById("Permisos").value;
     var arrayPermisos = glbPermisos.split("#"); 
     
     var treeview = top.frames['iframeArbol'].document.getElementById("TreeView1");
     var nodes = treeview.getElementsByTagName("a");
     var nodos = nodes.length;
     
     for(i=0;i<arrayIdsCliente.length-1;i++)
         {
           top.frames['iframeArbol'].document.getElementById(arrayIdsCliente[i]).parentNode.parentNode.parentNode.parentNode.style.display=arrayPermisos[i];
         }
    }
    
    function mostrarRamasROLES() 
    {
    //EGB 1/10/2008 Mostrar/OcultarRamas en Funcion del rol de usuario con COOKIES
       var i;
       var cookiePermisos = getCookie('arrayPermisos');
       var arrayPermisos = cookiePermisos.split("#"); 
       var error = arrayPermisos[0].replace(",",'');
       var treeview = top.frames['iframeArbol'].document.getElementById("TreeView1");
       var nodes = treeview.getElementsByTagName("a");
       var nodos = nodes.length;
           
       if (nodos!=0){
         var cookieIdsClienteNodosPadre = getCookie('idsClienteNodosPadre');
         while (cookieIdsClienteNodosPadre=null)
         {
          i++;
         }
         cookieIdsClienteNodosPadre = getCookie('idsClienteNodosPadre');
         var arrayIdsCliente = cookieIdsClienteNodosPadre.split("#"); 
         //alert(cookieIdsClienteNodosPadre);      
      
         for(i=0;i<arrayIdsCliente.length-1;i++)
         {
           //alert(top.frames['iframeArbol'].document.getElementById(arrayIdsCliente[i]));

           top.frames['iframeArbol'].document.getElementById(arrayIdsCliente[i]).parentNode.parentNode.parentNode.parentNode.style.display=arrayPermisos[i];
         }
         }
         else{
         //alert(nodos); 
           for(i=0;i<nodos;i++)
         {
            
           nodes[i].style.display = ''
         }
         }       
     }
     
    
</script>
<body>
    <form id="form1" runat="server">
    <div>
    <INPUT id="Permisos" type="hidden" value="" />
    </div>
    </form>
</body>
</html>
