// JScript File
// FUNCIONES DE UTILIDAD PARA LA NAVEGACIÓN POR LOS LISTADOS DE LA APLICACIÓN

function toggleCapa(usuario,letra,objeto){    
	var elementos = document.all(letra+usuario);
	if (elementos.length!=undefined){
		for (i=0;i<elementos.length;i++){
			if (elementos[i].style.display=='none') {
			    elementos[i].style.display='';
			    objeto.src='images/iconos/cerrar.gif';
			}else{
			    elementos[i].style.display='none';
			    objeto.src='images/iconos/abrir.gif';
			}
		}
	}
	else {
			if (elementos.style.display=='none'){
			    elementos.style.display='';
			    objeto.src='images/iconos/cerrar.gif';
			}else{
			    elementos.style.display='none';		
			    objeto.src='images/iconos/abrir.gif';
			}
	}
}


function toggleCabeceras(seccion,objeto){
    /*if (document.getElementById(seccion).style.display==''){
        document.getElementById(seccion).style.display='none';
        objeto.src='images/iconos/abrir.gif';
    }else{
        document.getElementById(seccion).style.display='';
        objeto.src='images/iconos/cerrar.gif';
    }*/
    
    var filas = document.getElementsByTagName("tr");
    
    if (filas.length!=undefined){
		for (i=0;i<filas.length;i++){
		    if (filas[i].id.indexOf(seccion)>=0)
		        if (filas[i].style.display=='none'){
		            filas[i].style.display='';	
                    objeto.src='images/iconos/cerrar.gif';
		        }
		        else{
		            filas[i].style.display='none';
                    objeto.src='images/iconos/abrir.gif';
		        }
		}
	}
}

function toggleFila(fila,entrando){
    if (entrando)
        fila.style.backgroundColor='#dEdAc2';
    else
        fila.style.backgroundColor='';
}

