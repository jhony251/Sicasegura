<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CaucepuntMain.aspx.vb" Inherits="SICAH_caucepuntMAIN" TRACE="false"%>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >
<http>
<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  <link rel="stylesheet" href="..\styles.css">


  <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script language="javascript" src="..\js/utilesListados.js"></script>
  <script language="javascript" src="..\js/utilesMenus.js"></script>  
  <script language="javascript">
  
    function OcultarMostrarArbol()
    {
        if (top.document.getElementById('iframeBuscar').style.width == '0px')
        {
            top.document.getElementById('iframeBuscar').style.width = 300;
            //top.document.getElementById('iframeArbol').style.width = 300;
         }
         else
         {
            top.document.getElementById('iframeBuscar').style.width = 0;       
            //top.document.getElementById('iframeArbol').style.width = 0;
            top.document.getElementById('imgflecha').src="images/flecha_ampliar.gif"
            
         }
        //top.document.getElementById('iframeArbol').src="accesoVisorDesdeArbolMain.aspx?X=" + X + "&y=" + Y + "&zone=30&R=" + radio + "&alerta=" + Alerta;
        
    }
       
  </script>
  <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
  <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
  <meta name="vs_defaultClientScript" content="JavaScript">
 <%-- <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">--%>
    <link href="../styles.css" rel="stylesheet" />
    
   
</head>
<!--<body bgcolor="#EEEAD2" onload="redimensiona();" id="cuerpo">-->
<body bgcolor="#EEEAD2" id="cuerpo">
  <form id="Form1" method="post" runat="server">
  <span id="dsp4"></span>
  <span id="imagepath" style="display:none">../js/calendar/images</span>
    
    <table cellspacing="0" align="center" style="border: 1px solid #666666;background-color: white; width: 100%;">
      <tr>
        <td style="height: 830px">
            <table cellspacing="0" cellpadding="1" style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>         
            <table cellpadding="0" cellspacing="0" style="width:100%;">
                <tr>
                   <td  valign="top" style="width:300px;">
                        <iframe name="iframeBuscar" runat="server" id="iframeBuscar" style="width:300px; 
                        height:710px;"  frameborder="0" marginheight="0" marginwidth="0" scrolling="no" 
                        class="HTMframe" ></iframe>               
                    </td>
                    <td  bgcolor="#F1E0B1" style="width:10px;">                        
                        <img src="images/flecha_reducir.gif" id="imgflecha" onclick="OcultarMostrarArbol()" />
                    </td>
                    <td >
                        <iframe name="iframeDetalle" id="iframeDetalle" height="720" src="caucepuntDETALLE.aspx" 
                        frameborder="0" marginheight="0" marginwidth="0" class="HTMframe" style="width:100%;"
                        scrolling="auto" ></iframe>
                    </td>                    
                </tr>
                
                
            </table>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>
