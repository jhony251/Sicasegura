// JScript File
//esta funcion obtiene la fecha y la hora de nuestro PC y la muestra en la pagina web
function fechaHora () {
    dows = new Array("Domingo","Lunes","Martes","Miércoles","Jueves","Viernes","Sábado");
   months = new Array("Enero","Febrero","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre");
   now = new Date();
   dow = now.getDay();
   d = now.getDate();
   m = now.getMonth();
   h = now.getTime();
   y = now.getYear();
   document.write(dows[dow]+" "+d+" de "+months[m]+" de "+y);

}
