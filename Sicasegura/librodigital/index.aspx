<%@ Page Language="VB" AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

    <head id="Head1" runat="server">
        <title></title>
        <link rel="shortcut icon" href="home/images/favicon.ico" />
        <!--<link rel="stylesheet" href="home/css/style.css">-->
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/stylesheet_opt.css" />
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/theme.css" />
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/utiles.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/menu.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/ddmenu.js"></script>
        <script src="home/js/jquery.js"></script>
        <script src="home/js/jquery-migrate-1.1.1.js"></script>
        <script src="home/js/jquery.easing.1.3.js"></script>
        <script src="home/js/script.js"></script> 
        <script src="home/js/superfish.js"></script>
        <script src="home/js/jquery.equalheights.js"></script>
        <script src="home/js/jquery.mobilemenu.js"></script>
        <script src="home/js/tmStickUp.js"></script>
        <script src="home/js/jquery.ui.totop.js"></script>
        <script>
              $(window).load(function(){
              $().UItoTop({ easingType: 'easeOutQuart' });
              $('#stuck_container').tmStickUp({});  
             }); 
        </script>
        
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="container">
                <div id="container2">
                   <asp:Literal ID="HTML_Links_Sup_Izq"     runat="server"></asp:Literal> 
                   <asp:Literal ID="HTML_Subcabecera_Logos" runat="server"></asp:Literal>
                   <asp:Literal ID="HTML_Menu_Navegacion"   runat="server"></asp:Literal>
                   
                   
                   
                   
                   
                   <div id="left-column">
                   
                   <br /><br /><br />
                      <table style="border-width:0px;">
                          <tr >
                             <td style="border-width:0px;" colspan="2"><h2>Control de Acceso</h2></td>
                          </tr>
                          <tr>
                             <td style="border-width:0px;" colspan="2">Por favor en los siguientes campos
                                            introduzca su nombre de usuario y
                                            contraseña para acceder al sistema<br /><br /></td>
                          </tr>
                          <tr>
                             <td style="border-width:0px;">Usuario:</td>
                             <td style="border-width:0px;"> <asp:TextBox ID="TXT_usuario" runat="server" Columns="13" BorderStyle="Solid"  BorderWidth="1px"></asp:TextBox></td>
                          </tr>
                          <tr>
                             <td style="border-width:0px;">Contraseña: </td>
                             <td style="border-width:0px;"><asp:TextBox ID="TXT_pass" runat="server" Columns="13" BackColor="White" BorderWidth="1px"  TextMode="Password"></asp:TextBox></td>
                          </tr>
                          <tr>
                             <td style="border-width:0px;"></td>
                             <td style="border-width:0px;"><asp:Button ID="BTN_login" runat="server" Text="Aceptar" BorderColor="Black" BorderStyle="None" BorderWidth="0px"  /></td>
                          </tr>
                      </table>
                   </div>
                   
                   <div id="central-content"  style="padding-left:30px;margin-top:10px;color:Gray; ">
                            <h2 style="width:300px;">Condiciones de uso del portal</h2><br /><br /><br />
                            
                             Este portal, denominado Libro Digital de Control de Aprovechamientos, nace al amparo de la disposición Séptima de la Resolución de 23 de abril de 2014 del Presidente de la Confederación Hidrográfica del Segura, por la que se adapta el contenido de la Orden ARM/1312/2009, de 20 de mayo, en el ámbito territorial de la Demarcación Hidrográfica del Segura. Mediante su utilización se puede satisfacer por los titulares las obligaciones relativas a la medición, registro y comunicación de los datos de consumo de cada aprovechamiento.

<br /><br />Al propio tiempo que se pueden consultar los datos del aprovechamiento como consumos históricos, 
                            datos de inscripción en Registro de Aguas, cartografía (Próximamente) siendo estos datos solo visibles para el usuario autorizado para dicha inscripción. El usuario queda informado de la posible publicación por este medio de los documento oficiales archivados en la Confederación Hidrográfica del Segura relativos a su inscripción, 

<br /><br />Una vez que se accede con el usuario y contraseña que Confederación Hidrográfica del Segura facilita al titular del 
                            aprovechamiento, el portal solicita unos datos de contacto, 
                            los cuales son de obligatoria cumplimentación.
                            
                            Una vez cumplimentados los datos, se contactará con el titular desde la oficina 
                            del SICA, de Comisaría de Aguas, para la validación de los mismos.<br />
                                                                  Cumplimentado este trámite y 
                            realizada la validación, se podrá acceder al portal y&nbsp; podrán facilitarse los datos de consumo del aprovechamiento de acuerdo a las obligaciones establecidas en la normativa de referencia:

                                 <br />
                                 <br />
                                                                  <b>1ª CATEGORIA:</b> Consumos hasta 50.000 m3/año                Deberán comunicar con periodicidad anual las lecturas de los dispositivos de medición. en los cinco primeros días naturales del mes de octubre
                                 <br />
                                 <b>2ª CATEGORÍA:</b> Consumos entre 50.000 y 500.000 m3/año       Deberán comunicar con periodicidad semestral las lecturas de los dispositivos de medición. en los cinco primeros días naturales de los meses de octubre y abril
                                 <br />
                                 <b>3ª CATEGORÍA:</b> 500.000 Y 1.500.000 m3/año **
                                 <br />
                                 <b>4ª CATEGORÍA:</b> Más de 1.500.000 m3/año ** <br />
                                 <br />
                                                                  El cómputo del volumen anual captado al que tienen derecho los titulares de los aprovechamientos se realizará por años hidrológicos, comenzando el 1 de octubre y finalizando el 30 de septiembre del año siguiente.

                                 <br />
                                 <br />
                                                                  ** Las captaciones de las categorías tercera y cuarta quedan exentas del envío de lecturas a la Confederación Hidrográfica del Segura siempre que dispongan de sistemas de registro y transmisión automáticos

                                 <br />
                                 <br />
                                                                  Queda establecido por el presente documento que ningún dato aportado por los titulares de un aprovechamiento tienen carácter oficial mientras no sea expresamente utilizado por la Confederación Hidrográfica del Segura en algún documento o informe oficial.

                                 <br />
                                 <br />
                                 <b>Continuando con el uso del portal, el usuario declara que está de acuerdo con lo aquí expresado y en ningún momento podrá usar la información que aporte por este medio para realizar reclamación alguna al Organismo,  salvo una expresa oficialización de la información por parte de la Confederación Hidrográfica del Segura.</b>

                             
                             
                             
                             
                             
                   
                   </div>
                   
                   <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
                   
                </div>      
            </div>
            <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>
    	    
        </form>
    </body>
</html>
