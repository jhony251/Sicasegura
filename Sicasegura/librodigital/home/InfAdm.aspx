<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfAdm.aspx.cs" Inherits="librodigital_home_InfAdm" %>


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
        <script src="../../SICAH/HighCharts/jquery.min131.js" type="text/javascript"></script> 
        <script type="text/javascript">

            function pageLoad() {
            }
    
        </script>
        <script>
            $(window).load(function() {
                $().UItoTop({ easingType: 'easeOutQuart' });
                $('#stuck_container').tmStickUp({});
            }); 
        </script>
        <style>
            iframe{	height:320px;padding-right:50px;}
            .tituloCampoRaacs{float:left; font-weight:bold; margin-right:10px; margin-left:10px;}
            .valorCampoRaacs{float:left; margin-right:10px; margin-left:10px;}
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="container">
                <div id="container2">
                    <asp:Literal ID="HTML_Links_Sup_Izq"     runat="server"></asp:Literal> 
                    <asp:Literal ID="HTML_Subcabecera_Logos" runat="server"></asp:Literal>
                    <asp:Literal ID="HTML_Menu_Navegacion"   runat="server"></asp:Literal>
                    <div id="content-wrapper">
                        <p class="breath">
                            <a name="up" id="up"></a>
                                Estas en::
                            <a href="index.aspx">Inicio</a> > <a href="infadm.aspx">Información Administrativa</a>
                        </p>
                    
                        <div id="left-column">
                            <ul class="nav-menu">
                                <asp:Literal ID="HTML_Link_Infadm" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Infadm_pdf" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_InfContadores" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Esqumas" runat="server"></asp:Literal>
                                <asp:Literal ID="HTML_Link_Fotos" runat="server"></asp:Literal>
                            </ul>    
                        </div>
                    
                        <div id="central-content">
                            <h2><asp:Literal ID="HTML_Título_Aprovechamiento" runat="server"></asp:Literal></h2>
                            <p>Información disponible en el Registro de Aguas de Confederación Hidrográfica del Segura relativa al aprovechamiento.</p>
                            <div>
                                <div style="background-color:#eaecef;">
                                    <div class="tituloCampoRaacs">Número de Inscripción</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Num_insc" runat="server"></asp:Literal></div>
                                
                                    <p>&nbsp;</p>
                                
                                    <div class="tituloCampoRaacs">Sección</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_Adm_seccion" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Tomo</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_Adm_tomo" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Hoja</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_Adm_hoja" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Fecha de Inscripción</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_Adm_Fec_inscripcion" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Firmado por</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_firmado" runat="server"></asp:Literal></div>
                                    <div class="clear"></div>
                                </div>
                                <p>&nbsp;</p>
                                <div class="tituloCampoRaacs" style="text-decoration:underline;">Localización</div>
                                <p>&nbsp;</p>
                                <div style="background-color:#eaecef;">
                                    <div class="tituloCampoRaacs">Lugar</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_Adm_Lugar" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Acuifero</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_Adm_Acuifero" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Término Municipal</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_Adm_Term_municipal" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Provincia</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_Adm_provincia" runat="server"></asp:Literal></div>
                                    <div class="clear"></div>
                                </div>
                                <p>&nbsp;</p>
                                <div class="tituloCampoRaacs" style="text-decoration:underline;">Caracterización y Dotaciones(m3/ud/año)</div>
                                <p>&nbsp;</p>    
                                <div style="background-color:#eaecef;">
                                    <div class="tituloCampoRaacs">Superficie (Ha)</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_superficie" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Desnivel (m)</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_desnivel" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Salto (m)</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_salto" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Potencia (cv)</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_potencia" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Caudal instantáneo (l/s)</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_Qinstantaneo" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Caudal Medio</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_Qmedio" runat="server"></asp:Literal></div>
                                    <p>&nbsp;</p>
                                    <div class="tituloCampoRaacs">Regadio</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_DotReg" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Industrial</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_DotInd" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Abastecimiento</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_DotAbs" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Ganadero</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_DotGan" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Hidroeléctricas</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_DotHid" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Otros</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_DotOtros" runat="server"></asp:Literal></div>
                                    <div class="clear"></div>
                                </div>
                                <p>&nbsp;</p>    
                                <div class="tituloCampoRaacs" style="text-decoration:underline;">Volúmenes (m3)</div>
                                <p>&nbsp;</p>
                                <div style="background-color:#eaecef;">
                                    <div class="tituloCampoRaacs">Regadio</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_VolReg" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Industrial</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_VolInd" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Abastecimiento</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_VolAbs" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Ganadero</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_VolGan" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Hidroeléctricas</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_VolHid" runat="server"></asp:Literal></div>
                                    <div class="tituloCampoRaacs">Otros</div>
                                    <div class="valorCampoRaacs"><asp:Literal ID="HTML_Inf_adm_VolOtros" runat="server"></asp:Literal></div>
                                    <div class="clear"></div>
                                </div>
                                <p>&nbsp;</p>
                                <div class="tituloCampoRaacs" style="text-decoration:underline;">Observaciones</div>
                                <p>&nbsp;</p>
                                <div class="valorCampoRaacs" style="border:solid 1px black;padding:5px;"><asp:Literal ID="HTML_Inf_adm_observaciones" runat="server"></asp:Literal></div>
                                <p>&nbsp;</p>    
                            </div>                                                          
                        </div>
                                
                    </div>
                    <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
                </div>      
            </div>
            <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>
    	    
        </form>
    </body>
</html>
                 
                