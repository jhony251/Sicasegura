<%@ Page Language="VB" AutoEventWireup="false" CodeFile="home.aspx.vb" Inherits="home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="StyleSheet.css" />
    <link href="js/Treedhtmlx/dhtmlxtree.css" rel="stylesheet" type="text/css" />
    <script src="js/Treedhtmlx/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="js/Treedhtmlx/dhtmlxtree.js" type="text/javascript"></script>
    <script src="js/Treedhtmlx/ext/dhtmlxtree_start.js" type="text/javascript"></script>  
    <script language="javascript">
        function rellenarInformacion(NombreTitular, NIFTitular, DireccionTitular, MunicipioTitular, CodPostalTitular,
                        ProvinciaTitular, TelefonoTitular, ReferenciaExpedienteLibroAprovechamiento, NumeroRegistroAntiguo,
                        ReferenciaExpedientesRegistroAguas, OtrosExpedientes, Seccion, Tomo, Hoja, Inscripcion, Inscripcion_estado,
                        InscripcionRelacionada) {
            var datosTitular;
            var nombrePDF = "BOE-A-2009-8731.pdf";
            datosTitular = "<div id='datosTitular'><table width=100%>" + 
            "<tr><td colspan=2><b>Titular: </b>" + NombreTitular + "</td><td><b>NIF: </b>" + NIFTitular + "</td><!--<td><a href='" + nombrePDF + "' target='_blank'><img src='images/pdf.png' border='0'></a></td>--></tr>" + 
            "<tr><td colspan=4><b>Dirección: </b>" + DireccionTitular + "</td></tr>" + 
            "<tr><td><b>Municipio: </b>" + MunicipioTitular + "</td><td><b>Provincia: </b>" + ProvinciaTitular + "</td><td><b>CP: </b>" + CodPostalTitular + 
            "</td><td><b>Teléfono: </b>" + TelefonoTitular + "</td></tr>" + 
            "</table></div>";
            document.getElementById("datosTitular").innerHTML = datosTitular;
            var datosRegistro;
            datosRegistro="<dif id='registroAguas'><table width=100%>" +
            "<tr><td colspan=3><b>Expedientes originales: </b>" + ReferenciaExpedienteLibroAprovechamiento + "</td><td><b>Nº Antiguo de aprovechamientos: </b>" +
            NumeroRegistroAntiguo + "</td><td><b>Expedientes actuales: </b>" + ReferenciaExpedientesRegistroAguas + "</td><td><b>Otros expedientes relacionados: </b>" +
            OtrosExpedientes + "</td></tr>" +
            "<tr><td><b>Sección: </b>" + Seccion + "</td><td><b>Tomo: </b>" + Tomo + "</td><td><b>Folio: </b>" + Hoja + "</td><td><b>Inscripción: </b>" + Inscripcion +
            "</td><td><b>Inscripción relacionada: </b>" + InscripcionRelacionada + "</td><td><b>Inscripción estado: </b>" + Inscripcion_estado + "</td></tr>" +
            "</table></div>"
            document.getElementById("datosRegistro").innerHTML = datosRegistro;                        
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
    <asp:HiddenField runat="server" ID="codigoPVYCR" Value="" />
    <asp:HiddenField runat="server" ID="EM" Value="" />
        <asp:Literal ID="Cabecera" runat="server"></asp:Literal>
        <table class="contenedor" align="center" style="background-color:#105A7B">
            <tr><td colspan="2" width="400px" style="background-color:#FFFFFF"><img src="Resources/Images/cabecera.jpg" border="0" /></td></tr>
            <tr><td colspan="2" class="cabeceraInformacion">Datos Titular</td></tr>
            <tr><td colspan="2" style="background-color:#FFFFFF">
            <div id="datosTitular"></div>
            </td></tr>
            <tr><td colspan="2" class="cabeceraInformacion">Datos Registro de Aguas</td></tr>
            <tr><td colspan="2" style="background-color:#FFFFFF">
            <div id="datosRegistro"></div>
            </td></tr>            
            <tr>
                <td valign="Top" style="background-color:#FFFFFF" width="530px">
                    <div id="treeBox"></div>
                    <script  type="text/javascript" language="javascript">
                        var tree = new dhtmlXTreeObject("treeBox", "100%", "100%", 0);
                        tree.setImagePath("js/Treedhtmlx/imgs/csh_bluebooks/");
                    </script>
                    <div class="dhtmlxTree" id="treeboxbox_tree" setImagePath="js/Treedhtmlx/imgs/csh_bluebooks/"
                        style="width:530px;height:220px; overflow:auto; margin-top:10px;">
                        <asp:Literal ID="arbol" runat="server"></asp:Literal>
                    </div>
               </td>
               <td valign="top" height="220" style="background-color:#FFFFFF"><iframe frameborder="no" scrolling="auto" width="100%" height="110%" src="Datos.aspx" id="datos"></iframe></td>
            </tr>                        
            <tr><td height="300px" valign="top" style="background-color:#FFFFFF" colspan="2">
                <iframe frameborder="no" scrolling="auto" width="100%" height="100%" id="lecturas"></iframe>                               
            </td></tr>
        </table>
    </div>
    </form>
</body>
</html>
