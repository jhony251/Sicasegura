<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones_new.aspx.vb" Inherits="SICAH_Agrupaciones_new" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
<title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link href="../styles.css" rel="stylesheet" type="text/css" />  
    <link href="../includes/ext-3.3.1/resources/css/ext-all.css" rel="stylesheet" type="text/css" /> 
<%--    <link href="../ext-all-agrup.css" rel="stylesheet" type="text/css" /> 
--%>    <link href="../EstilosArbol.css" rel="stylesheet" type="text/css" />
    
    
    
    
    <script src="../includes/ext-3.3.1/adapter/ext/ext-base.js" type="text/javascript"></script> 
    <script src="../includes/ext-3.3.1/ext-all.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../js/utilesObjetoArbolAgrup.js"></script> 
    
</head>
<body bgcolor="#eeead2"  style="overflow:auto;" >
    <form id="Form1" method="post" runat="server"   >
        <%--<table cellspacing="0" align="center" style="border: 1px solid #666666;background-color:#ffffff;width:99%;">
            <tr>
                <td style="height: 820px" valign="top">
                    <table cellspacing="0" cellpadding="1" style="width:100%;background-color:#ffffff">
                        <tr>
                            <td>
                                <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="background-color:#FFFFFF; width:100%">
                        <tr>
                            <td valign="top" style="width:16.5%;">
                                <iframe name="iframeBuscar" src="Agrupaciones_Arbol.aspx" runat="server" 
                                        id="iframeBuscar" style="width:100%; height:700px;"  frameborder="0" 
                                        marginheight="0" marginwidth="0" scrolling="no" class="HTMframe" >
                                </iframe>
                            </td>
                            <td valign="top" style="width:85%;" >
                                <iframe name="iframeDetalle" id="iframeDetalle"  src="Agrupaciones_detalle.aspx" 
                                        frameborder="0" marginheight="0" marginwidth="0" class="HTMframe" 
                                        style="width: 100%; height:700px; " scrolling="no" >
                                </iframe>
                            </td>                    
                        </tr>
                    </table>
                </td>
            </tr>
        </table>--%>
        
        <script type="text/javascript">
            document.body.onresize = function() {
                alertSize();
                

            
            }
            Ext.onReady(function() {
                Ext.state.Manager.setProvider(new Ext.state.CookieProvider());
                var viewport = new Ext.Viewport({
                id: 'viewport',
                border: false,
                layout: 'border',
                items: [
                            { region: 'north', contentEl: 'north', height: 110, margins: '0 0 0 0', layout: 'fit' },
                            { region: 'center', autoHeight: true, contentEl: 'center',  collapsible: false, split: true, margins: '0 3 3 0', layout: 'card' },
                            { region: 'west', autoHeight: true, contentEl: 'west', title: 'Árbol de Inscripciones y agrupaciones', split: true, width: 250, minSize: 175, maxSize: 400, collapsible: true, margins: '0 0 0 3', layout: 'fit' }
                            ]
                });
            });
            
            function alertSize() {
                var myWidth = 0, myHeight = 0;
                if (typeof (window.innerWidth) == 'number') {
                    //Non-IE
                    myWidth = window.innerWidth;
                    myHeight = window.innerHeight;
                } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                    //IE 6+ in 'standards compliant mode'
                    myWidth = document.documentElement.clientWidth;
                    myHeight = document.documentElement.clientHeight;
                } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                    //IE 4 compatible
                    myWidth = document.body.clientWidth;
                    myHeight = document.body.clientHeight;
                }
//                var panel = Ext.getCmp('pnlArbol');
//                var arbol = Ext.getCmp('Arbol');
//                panel.remove(arbol);
                
                document.getElementById('west').style.height = myHeight - 155 + 'px';
                document.getElementById('iframeDetalle').style.height = myHeight -130 + 'px';
            }
    </script>
    
    <div style="margin-top:60px;"><asp:Label ID="lblPestanyas" runat="server"></asp:Label></div>
    <div id="west">
        <%--<div id="ContenedorArbol"></div>--%>
        
        
    </div>
    <div id="center" class="x-hide-display">
        <iframe name="iframeDetalle" id="iframeDetalle"  src="Agrupaciones_detalle.aspx" 
                                        frameborder="0" marginheight="0" marginwidth="0" style="width:100%;" 
                                         scrolling="auto" >
        </iframe>
        <script type="text/javascript" language="javascript">
            alertSize();
        </script>
    </div>
      
    <div id="north" class="x-hide-display">
        <asp:Label ID="lblCabecera" runat="server"></asp:Label>     
    </div>
        
    </form>
</body>
</html>