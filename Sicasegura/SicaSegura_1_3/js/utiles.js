// JScript File

//Esta función obtiene las coordenadas X e Y absolutas de un control en una página web
function getControlXY(elemento){
    var elPadre=elemento;
    var x=0;
    var y=0;
    
    while (elPadre.tagName != "BODY"){
        x += elPadre.offsetLeft;
        y += elPadre.offsetTop;
        elPadre = elPadre.parentElement;
    }
    alert(x);
    alert(y);
}

//Idem que la anterior pero retorna los dos valores
function PosicionControlXY(elemento){
    var elPadre=elemento;
    var pos = {x: element.offsetLeft || 0, y:element.offsetTop || 0};
    
    while (elPadre.tagName != "BODY"){
        pos.x += elPadre.offsetLeft;
        pos.y += elPadre.offsetTop;
        elPadre = elPadre.parentElement;
    }
    return p;
}
