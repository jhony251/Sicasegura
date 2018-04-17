<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CurvasAcequiasMAIN.aspx.vb" Inherits="SICAH_CurvasAcequiasMAIN" TRACE="false"%>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  <link rel="stylesheet" href="..\styles.css">


  <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script language="javascript" src="..\js/utilesListados.js"></script>
<script language="javascript" src="..\js/utilesMenus.js"></script>  

  <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
  <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
  <meta name="vs_defaultClientScript" content="JavaScript">
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../styles.css" rel="stylesheet" />
   
</head>
<body bgcolor="#EEEAD2">
  <form id="Form1" method="post" runat="server">
  <span id="dsp4"></span>
  <span id="imagepath" style="display:none">../js/calendar/images</span>
   <table width="100%" cellspacing="2" align="center" style="border: 1px solid #666666;background-color: white">
    
      <tr>
          <td style="height: 680px">
          <table cellspacing="0" cellpadding="1" style="width: 100%">
            <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label><tr>
             
        
          </table>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top" style="width:162px;" rowspan=2>
                        <%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%></td>
                    <td style="background-image:url(../images/dot2.gif);width:3px"></td>
                    <td style="height: 84px; width:160px" valign="top">
                        <iframe name="iframeBuscarAcequiasConCurva" id="iframeBuscarAcequiasConCurva" height="340" src="CurvasAcequiasBUSCAR.aspx?ConCurva=1" frameborder="0" marginheight="0" marginwidth="0" scrolling="no" class="HTMframe" style="width: 195px;"></iframe>
                        <iframe name="iframeBuscarAcequiasSinCurva" id="iframeBuscarAcequiasSinCurva" height="340" style="width:195px;" src="CurvasAcequiasBUSCAR.aspx?ConCurva=0" frameborder="0" marginheight="0" marginwidth="0" scrolling="no" class="HTMframe"></iframe>
                    </td>
                    <td rowspan="2" style="width:850px">
                        <iframe name="iframeDetalle" id="iframeDetalle" height="680" src="CurvasAcequiasDETALLE.aspx" frameborder="0" marginheight="0" marginwidth="0" class="HTMframe" style="width: 850px;" scrolling=no ></iframe>
                    </td>                    
                </tr>
                <tr>
                </tr>
            </table>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>