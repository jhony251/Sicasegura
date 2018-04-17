<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>GAM -- IPHONE</title>
	<!--<meta name="viewport" content="width=320,user-scalable=YES" />-->
	<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
	<link href="./icono.png" rel="apple-touch-icon" />
	<script language="javascript" type="text/javascript">
        var updateLayout = function()
                                    {
                                    if (window.innerWidth != currentWidth)
                                        {
                                        currentWidth = window.innerWidth;
                                        var orient = (currentWidth==320) ? "profile" : "landscape";
                                        document.body.setAttribute("orient", orient);
                                        window.scrollTo(0,1);
                                        }
                                    };
                                    iPhone.DomLoad(updateLayout);
                                    setInterval(updateLayout,500);
    </script>
	</head>
    <body style="margin:0px;background-image:url('images/background.jpg');Background-repeat:repeat-y; width:320px;">
    
        <form id="form1" runat="server" style="width:320px;">
            <div id="Content" style="width:320px;">
	            <div id="header" style="top:0px; left:0px; position:absolute; width:320px;">
		            <img src="images/header.jpg" alt="" />
	            </div>
	            <div id="headerTitle" style="top:15px;position:absolute;width:320px;">
		            <center><font face="TAHOMA" size="3" color="white"><b>GAM</b></font></center>
	            </div>
	            <div id="Div5" style="top:14px; left: 50px; position: absolute; width:250px;">
		            <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="~/PDA/Images/Logo_Tragsatec_Transparente.gif" 
                        ImageAlign="Right" Width="23px" />
 	            </div>
	            <div id="headerPage" style="top:65px;position:absolute;width:320px;">
		            <center><font face="verdana" size="3" color="Black"><b>Selección de Información</b></font></center>
	            </div>
	            <div id="BKContentPage" style="top:125px;position:absolute;width:320px;">
		            <img src="images/fondo_periodos.JPG" alt="" />
	            </div>
	            <div id="contentPage" style="top:10px;position:absolute;width:320px;">
		            <div id="Item1" style="top:130px;left:60px;position:relative;width:320px;"><a href="CaudSICA.aspx"><font face="verdana" style="font-size:13pt;" color="Black"><b> Datos SICA</b></font></a></div>
		            <div id="Item2" style="top:153px;left:60px;position:relative;width:320px;"><a href="EAforos.aspx"><font face="verdana" style="font-size:13pt;" color="Black"><b> Datos SAIH </b></font></a></div>
		            <div id="Item3" style="top:176px;left:60px;position:relative;width:320px;"><a href="ROEA.aspx"><font face="verdana" style="font-size:13pt;" color="Black"><b> Datos ROEA </b></font></a></div>
		            <div id="Item4" style="top:199px;left:60px;position:relative;width:320px;"><font face="verdana" style="font-size:13pt;" color="Black"><b> Datos BES  </b></font></div>
	            </div>
	            <div id="FooterPage" style="top:400px;position:absolute;width:320px;">
		            <center><font face="verdana" size="1" color="Red"><b>Los datos siempre serán mostrados en relación al año hidrológico</b></font></center>
	            </div>
            </div>
        </form>
    </body>
</html>