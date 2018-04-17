<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InfUsuario.aspx.vb" Inherits="librodigital_home_InfUsuario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/stylesheet_opt.css" />
        <link rel="stylesheet" href="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/theme.css" />
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/utiles.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/menu.js"></script>
        <script src="http://www.chsegura.es/export/system/modules/es.chsegura.www/resources/ddmenu.js"></script>
         <script src="../../SICAH/HighCharts/jquery.min131.js" type="text/javascript"></script> 
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
                            <a href="index.aspx">Inicio</a> > <a href="index.aspx">Consumos</a>
                        </p>
                    
                        <div id="left-column">
                        </div>
                        <div id="central-content">
                            La información aquí reflejada es de obligatoria cumplimentación. Debe ser fiel y relativa a la 
                            persona de contacto que hace uso regular de este sistema, con la finalidad de poder localizar
                            de un modo rápido y veraz a la persona responsable de utilizar el Libro de Control. </br></br>
                            En el caso de de que este formulario no sea relleno en tiempo y forma, siendo el tiempo concedido
                            para hacerlo 3 meses, la cuenta para usar este servicio podrá ser deshabilitada, no eximiendo esto 
                            de las obligaciones que regula la Orden Ministerial 13/2008 
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </body>
</html>
