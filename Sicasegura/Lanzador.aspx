<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lanzador.aspx.vb" Inherits="Lanzador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>SICA -- Sistema Integrado de Control de Aprovechamientos</title>
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
    <form id="form1" runat="server">
        <div>
            <div style="position:absolute; top:120px; left:50%; margin-left: -430px;opacity:0.4;filter:alpha(opacity=40); background-color:White;" onmouseover="this.style.opacity=1;this.filters.alpha.opacity=100" onmouseout="this.style.opacity=0.4;this.filters.alpha.opacity=40">
                <asp:Literal ID="Embalses" runat="server"></asp:Literal>
            </div>
            <table width="100%" height="100%" >
                <tr height="100%">
                    <td valign="middle" height="100%">
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
                                    <table width="100%" border=0>
                                        <tr>
                                            <td valign="bottom" style="padding-top:25px;padding-left:15px">
                                                <table  width="100%" >
                                                    <tr valign="bottom">
                                                        <td align="left" valign="bottom">
                                                            <!--<a href="http://guarderiafluvialpro.tragsatec.es/visorSICAH/visorguarderiasC.html" target="_blank" style="text-decoration:underline; font-size:12px; font-weight:bold; color:White ">-->
                                                            <a href="http://guarderiafluvialpro.tragsatec.es/visorSilver/CHComun2/index.aspx?target=0il4ek55xunj2dfvi20x2w45" target="_blank" style="text-decoration:underline; font-size:12px; font-weight:bold; color:White ">
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
                                            <td valign="bottom" style="padding-top:25px;width:30%;">
                                            </td>
                                            <td align="right" style="padding-top:25px; padding-right:15px">
                                                <table style="width:145px;height:632px; background-image:url(images/aplicaciones/fondoIconosLanzador.png); background-position:left top; background-repeat:no-repeat">
                                                     <tr>
                                                         <td align="center" style="">
                                                        <a href="../SicaSegurainformes" target="_blank" style="color: DarkGreen">
                                                        <img src="images/aplicaciones/InformesG.jpg" id="InformesG" style="filter:alpha(opacity=50);-moz-opacity:.5;opacity:.5;border:solid 3px white" onmouseover="mostrar('Informes')" />
                                                        <img src="images/aplicaciones/Informes.jpg" id="Informes" style="display:none; border:solid 3px white" onmouseout="ocultar('Informes')" />
                                                        <br />Informes
                                                        </a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style=" padding-top:10px"  >
                                                         <a href="../esquemassegura" id="A1" target="_blank"  style="color: DarkGreen" >
                                                         <img src="images/aplicaciones/esquemasG.jpg" id="esquemasG" style="filter:alpha(opacity=50);-moz-opacity:.5;opacity:.5; border:solid 3px white" onmouseover="mostrar('esquemas')" />
                                                         <img src="images/aplicaciones/esquemas.jpg" id="esquemas" style="display:none; border:solid 3px white" onmouseout="ocultar('esquemas')" />
                                                         <br/>Sistema Aprovechamientos
                                                         </a>
                                                        </td>
                                                    </tr>
                                                    <tr> 
                                                        <td align="center" >
                                                        <a href="index.aspx" target="_blank" style="color: DarkGreen">
                                                        <img src="images/aplicaciones/sicaseguraG.jpg" id="sicaseguraG" style="filter:alpha(opacity=50);-moz-opacity:.5;opacity:.5;border:solid 3px white" onmouseover="mostrar('sicasegura')" />
                                                        <img src="images/aplicaciones/sicasegura.jpg" id="sicasegura" style="display:none; border:solid 3px white" onmouseout="ocultar('sicasegura')" />
                                                        <br />Control Aprovechamientos
                                                        </a>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                         <td align="center" style="">
                                                        <a href="../usuariossegura" target="_blank" style="color: DarkGreen">
                                                        <img src="images/aplicaciones/gfG.jpg" id="gfG" style="filter:alpha(opacity=50);-moz-opacity:.5;opacity:.5; border:solid 3px white" onmouseover="mostrar('gf')" />
                                                        <img src="images/aplicaciones/gf.jpg" id="gf" style="display:none; border:solid 3px white" onmouseout="ocultar('gf')" />
                                                        <br/>Datos Manuales
                                                        </a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                         <td align="center" style="">
                                                        <a href="../scadaSegura" target="_blank" style="color: DarkGreen">
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
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
