// JScript File
// Script con funciones de utilidad para el manejo de la interfaz en cliente.
// Mejora el rendimiento de la aplicación ahorrando recargas entre páginas e informando al cliente durante los tiempos muertos.


var remostrarselects = true;

//Muestra la capa "cargando..." que se le presenta al usuario en los tiempos muertos.
//El usuario deberá definir la capa en el documento HTML previamente al uso de esta función
function mostrarCargando(){
    remostrarselects = false;
    
    ocultarSelects();
    var capaCargando = document.getElementById('capaProgreso');

    capaCargando.style.width = ''+(document.body.scrollWidth+30)+'px';
    capaCargando.style.height = ''+(document.body.scrollHeight+1000)+'px'; 
    capaCargando.style.top = ''+(document.body.scrollTop) + 'px';
    capaCargando.style.display='';
}

//Oculta la capa "cargando..."
function ocultarCargando(){
    remostrarselects = true;
    var capaCargando = document.getElementById('capaProgreso');
    capaCargando.style.display='none';
    mostrarSelects();   
}

//Oculta todos los Selects presentes en la página actual
function ocultarSelects(){
    var elementos = document.getElementById('Form1').elements;
    for (i=0;i<elementos.length;i++){
        if ((elementos[i].tagName == 'SELECT')&&(elementos[i].id.indexOf('DespEstados')<0)){
            elementos[i].style.visibility='hidden';
        }
    }
}

//Muestra todos los Selects presentes en la página actual
function mostrarSelects(){
    if (remostrarselects){
        var elementos = document.getElementById('Form1').elements;
        for (i=0;i<elementos.length;i++){
            if (elementos[i].tagName == 'SELECT')
                elementos[i].style.visibility='visible';
        }
    }
}
