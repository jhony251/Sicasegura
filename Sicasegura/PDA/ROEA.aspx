<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ROEA.aspx.cs" Inherits="TiempoReal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GAM - Datos Instantáneos</title>
    <meta http-equiv="Page-Exit" content="revealTrans(Duration=1,Transition=7)" />
	<meta name="viewport" content="width=320,user-scalable=YES" />
    <link rel="SHORTCUT ICON" href="GAM.ico"/>
    
    
</head>
<body style="margin:0px;background-image:url('images/background.jpg');Background-repeat:repeat-y; width:320px;">
    <form id="form1" runat="server" style="width: 320px">
    <div id="Div1" style="">
	    <div id="Div2" style="top:0px;position:absolute;width:320px;">
		    <img src="images/header.jpg" alt="" />
	    </div>
	    <div id="Div3" style="top:15px;position:absolute;width:320px">
		    <center><font face="TAHOMA" size="3" color="white"><b>GAM</b></font></center>
	    </div>
	    <div id="Div4" style="top:65px;position:absolute;width:320px">
		    <center><font face="verdana" size="3" color="Black"><b>Datos en tiempo real</b><br />
                </b></font></center>
	    </div>
	    <div id="Div5" style="top:0px; left: 50px; position: absolute; width:270px;">
 	   </div>
 	   <div id="Div6" style="top:15px; left: 0px; position: absolute; width:50px;">
		    <asp:ImageButton ID="ImageButton2" runat="server"  ImageUrl="Images/flechaatras.png" 
                ImageAlign="Right" onclick="ImageButton2_Click"/>
 	   </div>
	</div>
<br /><br /><br /><br /><br /><br /><br />
    <iframe src="Taibilla.aspx" frameborder="0" scrolling="no" width="100%" 
        style="height: 217px;"></iframe>
    </form>
</body>
</html>
