<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lanzador.aspx.vb" Inherits="Lanzador" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<style type="text/css">
TD {font:10px Tahoma}
A IMG {border-width:0px}
A {text-decoration:none; color:#114411; font-weight:bold}
A:hover {/*color:#bb3333*/}
INPUT.boton {font:10px Verdana}
INPUT.texto{font:11px Verdana;border:1px solid black;}
</style>
<script language=javascript>

function mostrar(id){
    document.getElementById(id+'G').style.display='none';
    document.getElementById(id).style.display='';
}

function ocultar(id){
    document.getElementById(id+'G').style.display='';
    document.getElementById(id).style.display='none';
}
</script>
</head>
<body bgcolor="gray">

<table width="100%" height="100%" >
<tr height="100%"><td valign="middle" height="100%">
<!-- tabla de la imagen completa -->
<table cellspacing="0" cellpadding="0" align="center" border="0" width="900" height="600" style="border:5px solid white"  >
<tr>
    <td valign=top style="
            background-image:url(images/cabeceraLanzadorPeque.jpg);
            background-position: left top; 
            height:75px; background-repeat:no-repeat; 
            padding-top:8px;
            background-color:White;
            border-bottom:solid 1px white">&nbsp;</td>
</tr>
<!-- imagen de fondo-->
<tr>
<td valign="top" style="height:500;background-image:url(images/fondoLanzadorPeque.jpg);background-position:left top; background-repeat:no-repeat;">
<table width="100%">
<tr>
<td valign="bottom" style="padding-top:25px;padding-left:15px">
    <!--El tiempo -->
    <!--<div style="background-color:White;width:250px;">
    <div >
    <script language="JavaScript" src="http://www.24log.es/clock/clock24.js"></script>
    <table width=100% cellspacing=1 cellpadding=3 class="clock24st" style="border:solid 1px black">
    <tr >
        <td class="clock24std" width=100% align=right >
        <a href="http://www.24log.es">
            <img src="http://www.24log.ru/clock/24log.gif" width="14" height="14" border="0" alt="contadores web" align="left" hspace="2">
        </a>    
            <span class="clock24s" id="clock24_30426" style="color:#114411;font:bold 10px Verdana;">reloj html</span>
        </td>
    </tr>
    </table>
    <script language="JavaScript">
    var clock24_30426 = new clock24('30426',60,'%dd / %M / %yyyy %W %hh:%nn:%ss','es');
    clock24_30426.daylight('ES'); clock24_30426.refresh();
    </script>
    </div>
    <div style="border: solid 1px black; margin-top:5px" >-->
    <!--el tiempo en Murcia-->
    <!--<table border="0" cellpadding="0" width="235" cellspacing="0">
    <tr>
        <td height="18" bgcolor="#FFFFFF" style="font-family:Verdana;color:#808080;font-size: 12px">
            <a  href="http://www.tutiempo.net/Tiempo-Espana.html" title="El tiempo en España" style="padding-left:5px">El Tiempo</a>
            <a  href="http://www.tutiempo.net/Tiempo-Murcia-E30001.html" title="El tiempo en Murcia">Murcia</a>
        </td>
    </tr>
    <tr>
        <td style="border-top: 1px dotted #C0C0C0; border-bottom: 1px dotted #C0C0C0" bgcolor="#FFFFFF">
            <script language="javascript" src="http://www.tutiempo.net/asociados/Espana/tiempo.php?st=VGllbXBvLU11cmNpYS1FMzAwMDEuaHRtbA%3D%3D"></script>
        </td>    
    </tr>
    </table>
    fin tiempo en Murcia
    </div>
    </div>-->
    <!-- fin del tiempo -->
    <table  width="100%" >

<tr valign="bottom">
<td align="left" valign="bottom">
<a href="http://guarderiafluvialpro.tragsatec.es/visorSICAH/visorguarderiasC.html" target="_blank" style="text-decoration:underline; font-size:12px; font-weight:bold; color:White ">
VISOR SISTEMA DE INFORMACIÓN GEOGRÁFICA
</a>
</td>
</tr>
<tr valign="bottom">
<td align="left" valign="bottom">
<a href="http://www.chsegura.es/chs/cuenca/redesdecontrol/estadisticashidrologicas/partediario.html" target="_blank" style="text-decoration:underline; font-size:12px; font-weight:bold; color:White ">
PARTE DIARIO COMISARIA DE AGUAS
</a>
</td>
</tr>
<tr valign="bottom">
<td align="left">
<a href="http://siam.imida.es" target="_blank" style="text-decoration:underline; font-size:12px; font-weight:bold; color: Orange">SIAM-SISTEMA DE INFORMACIÓN AGRARIA DE MURCIA</a>
</td>
</tr>
<tr valign="bottom">
<td align="left">
<a href="http://www.aemet.es/es/eltiempo/prediccion/localidades/murcia-30001" target="_blank" style="text-decoration:underline; font-size:12px; font-weight:bold; color: Orange">AEMET-AGENCIA ESTATAL DE METEREOLOGIA</a>
</td>
</tr>    
</table>
</td>
<td align="right" style="padding-top:25px; padding-right:15px">

<table style="width:145px;height:632px; background-image:url(images/aplicaciones/fondoIconosLanzador.png); background-position:left top; background-repeat:no-repeat">
<tr>
    <td align="center" style=" padding-top:10px"  >
    <a href="http://sicasegura.tragsatec.es/topkapidesdelanzador.aspx?icono=1" id="A1" target="_blank"  style="color: DarkGreen" >
    <img src="images/aplicaciones/esquemasG.jpg" id="esquemasG" style="filter:alpha(opacity=50);-moz-opacity:.5;opacity:.5; border:solid 3px white" onmouseover="mostrar('esquemas')" />
    <img src="images/aplicaciones/esquemas.jpg" id="esquemas" style="display:none; border:solid 3px white" onmouseout="ocultar('esquemas')" />
    <br/>Sistema Aprovechamientos
    </a>
    </td>
</tr>
<tr> 
    <td align="center" >
    <a href="http://localhost/sicasegura_1_3/SICAH/caucepuntMAIN.aspx?nodobusqueda=n&valor=0" target="_blank" style="color: DarkGreen">
    <img src="images/aplicaciones/sicaseguraG.jpg" id="sicaseguraG" style="filter:alpha(opacity=50);-moz-opacity:.5;opacity:.5;border:solid 3px white" onmouseover="mostrar('sicasegura')" />
    <img src="images/aplicaciones/sicasegura.jpg" id="sicasegura" style="display:none; border:solid 3px white" onmouseout="ocultar('sicasegura')" />
    <br />Control Aprovechamientos
    </a>
    </td>
</tr>
<tr>
     <td align="center" style="">
    <a href="http://sicasegura.tragsatec.es/topkapidesdelanzador.aspx?icono=3" target="_blank" style="color: DarkGreen">
    <img src="images/aplicaciones/datosentiemporealG.jpg" id="datosentiemporealG" style="filter:alpha(opacity=50);-moz-opacity:.5;opacity:.5;border:solid 3px white" onmouseover="mostrar('datosentiemporeal')" />
    <img src="images/aplicaciones/datosentiemporeal.jpg" id="datosentiemporeal" style="display:none; border:solid 3px white" onmouseout="ocultar('datosentiemporeal')" />
    <br />Datos Automáticos
    </a>
    </td>
</tr>
<tr>
     <td align="center" style="">
    <a href="http://guarderiafluvialpro.tragsatec.es" target="_blank" style="color: DarkGreen">
    <img src="images/aplicaciones/gfG.jpg" id="gfG" style="filter:alpha(opacity=50);-moz-opacity:.5;opacity:.5; border:solid 3px white" onmouseover="mostrar('gf')" />
    <img src="images/aplicaciones/gf.jpg" id="gf" style="display:none; border:solid 3px white" onmouseout="ocultar('gf')" />
    <br/>Datos Manuales
    </a>
    </td>
</tr>
<tr>
     <td align="center" style="">
    <a href="http://sicasegura.tragsatec.es/topkapidesdelanzador.aspx?icono=4" target="_blank" style="color: DarkGreen">
    <img src="images/aplicaciones/topkapiRemotoG.jpg" id="topkapiRemotoG" style="filter:alpha(opacity=50);-moz-opacity:.5;opacity:.5; border:solid 3px white" onmouseover="mostrar('topkapiRemoto')" />
    <img src="images/aplicaciones/topkapiRemoto.jpg" id="topkapiRemoto" style="display:none;border: solid 3px white" onmouseout="ocultar('topkapiRemoto')" />
    <br/>Control Remoto
    </a>
    </td>
</tr>
<tr>
    <td align="center" style=" padding-bottom:10px"> 
    <a style="color: DarkGreen">
    <img src="images/aplicaciones/vigilancia.jpg" id="vigilancia" style="filter:alpha(opacity=50);-moz-opacity:.5;opacity:.5; border:solid 3px white" />
    <img src="images/aplicaciones/vigilancia.jpg" id="vigilancia2" style="display:none; border:solid 3px white"  />
    <br/>Red de Vigilancia
    </a>   
    </td>
</tr>

</table>

</td>
</tr>
</table>

</td>
</tr>
</table>
</td></tr>
</table>
</body>
</html>
