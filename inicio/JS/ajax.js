
function creaAjax() {
    var objetoAjax = false;
    try {
        /* Para navegadores distintos a internet explorer */
        objetoAjax = new ActiveXObject("Msxml2.XMLHTTP.6.0");
    } catch (e) {
        try {
            /* Para explorer */
            objetoAjax = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (E) {
            objetoAjax = false;
        }
    }

    if (!objetoAjax && typeof XMLHttpRequest != 'undefined') {
        objetoAjax = new XMLHttpRequest();
    }
    return objetoAjax;
}

function redirectCHS(url, contenedor, valores, metodo) {
    var ajax = creaAjax();
    var capaContenedora = document.getElementById(contenedor);
    var usr;

    /* Creamos y ejecutamos la instancia si el metodo elegido es POST */
    if (metodo.toUpperCase() == 'POST') {
        ajax.open('POST', url, true);
        ajax.onreadystatechange = function() {
            if (ajax.readyState == 1) {
                capaContenedora.innerHTML = "Cargando...";
            } else if (ajax.readyState == 4) {
                if (ajax.status == 200) {
                    document.getElementById(contenedor).innerHTML = ajax.responseText;
                    //alert(ajax.responseText);
                    var a = document.getElementById(contenedor).innerHTML;
                    usr= a.substr(a.indexOf('<cas:user>')+10,(a.indexOf('</cas')-a.indexOf('<cas:user>'))-10);
                    //alert(usr);
                    var url = 'https://sica.chsegura.es/inicio/login.aspx?usuar=' + usr;
                    //alert(url);
                    document.location.href = url;
                    
                } else if (ajax.status == 404) {
                    capaContenedora.innerHTML = "La direccion existe";
                } else {
                    capaContenedora.innerHTML = "Error: ".ajax.status;
                }
            }
        }
        ajax.setRequestHeader('Content-Type','application/x-www-form-urlencoded');
        ajax.send(valores);
        
        
        return;
    }
    /* Creamos y ejecutamos la instancia si el metodo elegido es GET */
    if (metodo.toUpperCase() == 'GET') {

        ajax.open('GET', url, true);
        ajax.onreadystatechange = function() {
            if (ajax.readyState == 1) {
                capaContenedora.innerHTML = "Cargando...";
            } else if (ajax.readyState == 4) {
                if (ajax.status == 200) {
                    document.getElementById(contenedor).innerHTML = ajax.responseText;
                } else if (ajax.status == 404) {
                    capaContenedora.innerHTML = "La direccion existe";
                } else {
                    capaContenedora.innerHTML = "Error: ".ajax.status;
                }
            }
        }
        ajax.setRequestHeader('Content-Type',
				'application/x-www-form-urlencoded');
        ajax.send(null);
        return

    }
}
function redirectTragsa(url, contenedor, valores, metodo) {
    var ajax = creaAjax();
    var capaContenedora = document.getElementById(contenedor);
    var usr;

    /* Creamos y ejecutamos la instancia si el metodo elegido es POST */
    if (metodo.toUpperCase() == 'POST') {
        ajax.open('POST', url, true);
        ajax.onreadystatechange = function() {
            if (ajax.readyState == 1) {
                capaContenedora.innerHTML = "Cargando...";
            } else if (ajax.readyState == 4) {
                if (ajax.status == 200) {
                    document.getElementById(contenedor).innerHTML = ajax.responseText;
                    //alert(ajax.responseText);
                    var a = document.getElementById(contenedor).innerHTML;
                    usr = a.substr(a.indexOf('<cas:user>') + 10, (a.indexOf('</cas') - a.indexOf('<cas:user>')) - 10);
                    //alert(usr);
                    var url = 'https://sicasegura/inicio/login.aspx?usuar=' + usr;
                    //alert(url);
                    document.location.href = url;

                } else if (ajax.status == 404) {
                    capaContenedora.innerHTML = "La direccion existe";
                } else {
                    capaContenedora.innerHTML = "Error: ".ajax.status;
                }
            }
        }
        ajax.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        ajax.send(valores);


        return;
    }
    /* Creamos y ejecutamos la instancia si el metodo elegido es GET */
    if (metodo.toUpperCase() == 'GET') {

        ajax.open('GET', url, true);
        ajax.onreadystatechange = function() {
            if (ajax.readyState == 1) {
                capaContenedora.innerHTML = "Cargando...";
            } else if (ajax.readyState == 4) {
                if (ajax.status == 200) {
                    document.getElementById(contenedor).innerHTML = ajax.responseText;
                } else if (ajax.status == 404) {
                    capaContenedora.innerHTML = "La direccion existe";
                } else {
                    capaContenedora.innerHTML = "Error: ".ajax.status;
                }
            }
        }
        ajax.setRequestHeader('Content-Type',
				'application/x-www-form-urlencoded');
        ajax.send(null);
        return

    }
}