// JScript File
// script con funciones para ver/desplegar/ocultar el menú de preproduccion

function desplegarPreproduccion(sender){
//muestra el menú 8motores, acequias,alimentacion...) de preproducción
    var despMotores = document.getElementById('desplegableMotores');
    var despAlimentacion = document.getElementById('desplegableAlimentacion');
    var despHorometros = document.getElementById('desplegableHorometros');
    var despAcequias = document.getElementById('desplegableAcequias');
    if ((despMotores.style.display=='none')||(despAlimentacion.style.display=='none')||(despAcequias.style.display=='none')||(despHorometros.style.display=='none')){
        ocultarPreproduccion();
        var elementoOrigen = event.srcElement;
        despMotores.style.display='';
        despAlimentacion.style.display='';
        despAcequias.style.display='';
        despHorometros.style.display='';        
    }
    else{
        despMotores.style.display='none';
        despAlimentacion.style.display='none';
        despAcequias.style.display='none';
        despHorometros.style.display='none';                
    }
    event.cancelBubble=true;
}

function ocultarPreproduccion(){
    var i;
    document.getElementById('desplegableMotores').style.display='none';
    document.getElementById('desplegableAlimentacion').style.display='none';
    document.getElementById('desplegableHorometros').style.display='none';
    document.getElementById('desplegableAcequias').style.display='none';    
    //mostrarSelects(); 
}
function Delete_Cookie( name, path, domain ) {
    if ( Get_Cookie( name ) ) document.cookie = name + "=" +
    ( ( path ) ? ";path=" + path : "") +
    ( ( domain ) ? ";domain=" + domain : "" ) +
    ";expires=Thu, 01-Jan-1970 00:00:01 GMT";
}
	
function Set_Cookie( name, value, expires, path, domain, secure ) 
{
    // Obtención de la Hora en milisegundos
    var today = new Date();
    today.setTime( today.getTime() );

    if ( expires )
    {
    expires = expires * 1000 * 60 * 60 * 24;
    }
    var expires_date = new Date( today.getTime() + (expires) );

    document.cookie = name + "=" +escape( value ) +
    ( ( expires ) ? ";expires=" + expires_date.toGMTString() : "" ) + 
    ( ( path ) ? ";path=" + path : "" ) + 
    ( ( domain ) ? ";domain=" + domain : "" ) +
    ( ( secure ) ? ";secure" : "" );
}

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

     function mostrarIframe(nombreFrame, visor)
    {
    //muestra el iframe (lecturas, general, informes...) sin recargar las paginas
    //el parametro visor, indica si la última vez que hemos accedido ha sido al visor, ya que en ese caso, el visor está en otro iframe
    //y para encontrar los iframes y ponerlos a visible hay que poner dos veces parent
    if (visor == 'N')
    {
        if (nombreFrame == 'iframeInfC')
        {   
            Set_Cookie('EnlaceC',0,null,null,null,null);
            Set_Cookie('EnlaceP',1,null,null,null,null);
            Set_Cookie('EnlaceE',2,null,null,null,null);
            window.parent.document.getElementById('iframeInfC').style.display = '';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }
        else if (nombreFrame == 'iframeInfP')
        {
            Set_Cookie('EnlaceC',0,null,null,null,null);
            Set_Cookie('EnlaceP',1,null,null,null,null);
            Set_Cookie('EnlaceE',2,null,null,null,null);
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = '';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';

        }
        else if (nombreFrame == 'iframeInfE')
        {
            Set_Cookie('EnlaceC',0,null,null,null,null);
            Set_Cookie('EnlaceP',1,null,null,null,null);
            Set_Cookie('EnlaceE',2,null,null,null,null);
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = '';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }        
      else if (nombreFrame == 'iframeinformes')
        {
            Set_Cookie('EnlaceC',3,null,null,null,null);
            Set_Cookie('EnlaceP',3,null,null,null,null);
            Set_Cookie('EnlaceE',3,null,null,null,null);
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = '';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        } 
      else if (nombreFrame == 'iframeLecturas')
        {
            Set_Cookie('EnlaceE',4,null,null,null,null);
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = '';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }
      else if (nombreFrame == 'iframeDetalleVisor')
        {
            Set_Cookie('EnlaceC',5,null,null,null,null);
            Set_Cookie('EnlaceP',5,null,null,null,null);            
            Set_Cookie('EnlaceE',5,null,null,null,null);
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = '';

        }                                     
        
    }
    else
    {
       if (nombreFrame == 'iframeInfC')
        {   
            Set_Cookie('EnlaceC',0,null,null,null,null);
            Set_Cookie('EnlaceP',1,null,null,null,null);
            Set_Cookie('EnlaceE',2,null,null,null,null);
            window.parent.parent.document.getElementById('iframeInfC').style.display = '';
            window.parent.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }
        else if (nombreFrame == 'iframeInfP')
        {
            Set_Cookie('EnlaceC',0,null,null,null,null);
            Set_Cookie('EnlaceP',1,null,null,null,null);
            Set_Cookie('EnlaceE',2,null,null,null,null);
            window.parent.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.parent.document.getElementById('iframeInfP').style.display = '';
            window.parent.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';

        }
        else if (nombreFrame == 'iframeInfE')
        {
            Set_Cookie('EnlaceC',0,null,null,null,null);
            Set_Cookie('EnlaceP',1,null,null,null,null);
            Set_Cookie('EnlaceE',2,null,null,null,null);
            window.parent.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.parent.document.getElementById('iframeInfE').style.display = '';
            window.parent.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }        
      else if (nombreFrame == 'iframeinformes')
        {
            Set_Cookie('EnlaceC',3,null,null,null,null);        
            Set_Cookie('EnlaceP',3,null,null,null,null);        
            Set_Cookie('EnlaceE',3,null,null,null,null);        
            window.parent.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.parent.document.getElementById('iframeinformes').style.display = '';
            window.parent.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        } 
      else if (nombreFrame == 'iframeLecturas')
        {
            Set_Cookie('EnlaceE',4,null,null,null,null);        
            window.parent.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.parent.document.getElementById('iframeLecturas').style.display = '';
            window.parent.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }
      else if (nombreFrame == 'iframeDetalleVisor')
        {
            Set_Cookie('EnlaceC',5,null,null,null,null);                
            Set_Cookie('EnlaceP',5,null,null,null,null);                
            Set_Cookie('EnlaceE',5,null,null,null,null);                
            window.parent.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.parent.document.getElementById('iframeDetalleVisor').style.display = '';

        }                                     
        
    }
    
}

     
