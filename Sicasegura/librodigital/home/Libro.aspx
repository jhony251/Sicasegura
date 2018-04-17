<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Libro.aspx.vb" Inherits="Informes" %>
<%@ Register TagPrefix="cc1" Namespace="NineRays.Reporting.Web" Assembly="NineRays.Reporting.Web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Informe Libro Digital</title>
    <script type="text/javascript" language="javascript">
        function Redirigir(){
            var objs;
            var cadena;            
            objs=document.getElementsByTagName("a");
            for (var i=0; i<objs.length; i++) {
                cadena=objs[i].href;                
                if (cadena.indexOf("guid")!=-1){
                    enlace=Mid(cadena,0,cadena.indexOf("&"));                                        
                }
            }           
            document.location.href=cadena;
        }
        
        function Mid(str, start, len)
        {
        // Make sure start and len are within proper bounds
            if (start < 0 || len < 0) return "";
            var iEnd, iLen = String(str).length;
            if (start + len > iLen)
                  iEnd = iLen;
            else
                  iEnd = start + len;
            return String(str).substring(start,iEnd);
        }

    </script>
    
    
        <title></title>
    <link rel="shortcut icon" href="images/favicon.ico" />
    <link rel="stylesheet" href="css/style.css">
    <script src="js/jquery.js"></script>
    <script src="js/jquery-migrate-1.1.1.js"></script>
    <script src="js/jquery.easing.1.3.js"></script>
    <script src="js/script.js"></script> 
    <script src="js/superfish.js"></script>
    <script src="js/jquery.equalheights.js"></script>
    <script src="js/jquery.mobilemenu.js"></script>
    <script src="js/tmStickUp.js"></script>
    <script src="js/jquery.ui.totop.js"></script>
    <script>
        $(window).load(function() {
            $().UItoTop({ easingType: 'easeOutQuart' });
            $('#stuck_container').tmStickUp({});
        }); 
    </script>
    <!--[if lt IE 8]>
     <div style=' clear: both; text-align:center; position: relative;'>
       <a href="http://windows.microsoft.com/en-US/internet-explorer/products/ie/home?ocid=ie6_countdown_bannercode">
         <img src="http://storage.ie6countdown.com/assets/100/images/banners/warning_bar_0000_us.jpg" border="0" height="42" width="820" alt="You are using an outdated browser. For a faster, safer browsing experience, upgrade for free today." />
       </a>
    </div>
    <![endif]-->
    <!--[if lt IE 9]>
    <script src="js/html5shiv.js"></script>
    <link rel="stylesheet" media="screen" href="css/ie.css">
    <![endif]-->
    <style>
    
    
div.transbox {
    background-color: #ffffff;
    border: 1px solid black;
    background: rgb(204, 204, 204); /* Fallback for older browsers without RGBA-support */
    background: rgba(204, 204, 204, 0.3);
}

div.scroll
{
	overflow-y: scroll;
	height: 130px;
	background: rgba(204, 204, 204, 0.5);
	
}
div.scroll:hover
{
	overflow-y: scroll;
	background: rgba(204, 204, 204, 0.2);
	color:Black;
}
div.transbox span {
    font-weight: bold;
    color: white;
}
h2.sin_bottom
{
	margin-bottom:-25px;
}
    
    </style>
    
    
    
    
    
</head>
<body onload="javascript:Redirigir()">
    <form id="form1" runat="server">
    
    <header>
          <div class="container">
            <div class="row">
                <div class="grid_12"><img align="right" src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/logo-chs.gif" /></div>
                <div class="grid_7" style="margin-top:-50px;">        
                    <a href="index.aspx">
                        <h2 class="sin_bottom"><asp:Literal ID="HTML_Titulo_Inscripcion" runat="server"></asp:Literal></h2><br />
                    </a>
                </div>
            </div>
            <font color="white"><asp:Literal ID="HTML_Subtitulo_Inscripcion" runat="server"></asp:Literal></font>
          </div>
          
          <section id="stuck_container">
          <!--==============================
                      Stuck menu
          =================================-->
            <div class="container">
              <div class="row">
                <div class="grid_12 ">
                  <div class="navigation ">
                    <nav>
                      <ul class="sf-menu">
                       <li><a href="index.aspx">Inicio</a></li>
                       <li><a href="lecturas.aspx">Insertar lecturas</a></li>
                       <li><a href="infgeografica.aspx">Información geográfica</a></li>
                       <li class="current"><a href="infadm.aspx">Información administrativa</a></li>
                       <li><a href="">Documentacion</a></li>
                       <li><a href="http://www.chsegura.es">CHS</a></li>
                       <li><a href="">Correspondencia</a></li>
                     </ul>
                    </nav>
                    <div class="clear"></div>
                  </div>                          
                </div>
             </div> 
            </div> 
          </section>
        </header>
    
    
    
    
    <div>
    <cc1:SharpShooterWebViewer id="SharpShooterWebViewer1" runat="server" Width="648px" Height="500px" PageIndex="0" Source="<%# reportGenerator1 %>" CssClass="webView" PagerCssClass="webViewPager" ImageFormat="Png" CacheTimeOut="02:00:00" BorderWidth="1px" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute;visibility:hidden">
        </cc1:SharpShooterWebViewer>
    </div>
    </form>
</body>
</html>
