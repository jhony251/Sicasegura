<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones_Totales.aspx.vb" Inherits="SICAH_Agrupaciones_Totales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  
  <script type="text/jscript" language="javascript" src="../js/utiles.js"></script>
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script type="text/jscript" language="javascript" src="../js/utilesListados.js"></script>
  
      <!-- Estilos tanto para el árbol como para agrupaciones -->
      <link href="../ext-all.css" rel="stylesheet" type="text/css" />  
       <!-- referencias para la agrupaciones -->
      
      <link href="../styles.css" rel="stylesheet" type="text/css" />       
             
        <!-- Estilos de página -->
        <link href="../estilosAgrupaciones.css" rel="stylesheet" type="text/css" id="theme" /> 
        <script language=javascript>

    function CargarAnyosE(AnyoInicio, AnyoFinal) {
        /* RDF 30/07/2008. Carga el año hidrológico en las cajas*/

        //document.getElementById("txtFiltroFechaIniE").value = AnyoInicio;
        //document.getElementById("txtFiltroFechaFinE").value = AnyoFinal;

    }

</script>
    <style type="text/css">
        .style1
        {
            font-size: 11pt;
            font-weight: bold;
        }
    </style>
</head>
<body style="background-color:#CCFFAA;">
    <form id="form1" runat="server">
        <span id="imagepath" style="display:none">../js/calendar/images</span>
        <span id="dsp4"></span>
        <table cellspacing="2" align="left" width="99%"  style="background-color:#CCFFAA;">
            <tr>
                <td>
                    <!-- Panel Puntos Agrupacion-->
                    <asp:Panel ID="pnlAgrupacion" Runat=server Visible="true" align="left">
                        <asp:label id="lblAgrupacionSel" runat=Server Visible=false Height="16px"></asp:label>                
                        <br />
                        <asp:Label ID=lblTitulo runat=server CssClass=tituloSec ForeColor=Maroon><B>TOTALES AGRUPACIÓN</B></asp:Label>
                        <br />
                        <table cellspacing=2 align="left" cellpadding=1 
                            style="border:1px solid Maroon;background-color:WhiteSmoke; width: 297px;" >
                            <tr>
                                <td style=" color:Red" colspan="3">
                                    <asp:LinkButton ID="lblAnyoHidrologicoE" runat="server" CssClass="enlaceLecturas" Visible="true" ></asp:LinkButton>
                                    <asp:TextBox ID="txtFiltrarCodFuenteDatoE" runat="server" CssClass="texto"  Width=18px Visible=false Height="17px"></asp:TextBox>
                                </td>                                
                            </tr>
                            <tr>
                                <td nowrap width="100" >
                                    <B>Desde</B><br />
                                    <asp:TextBox ID="txtFiltroFechaIniE" runat="server" CssClass="texto" Width=75px>[Fecha Desde]</asp:TextBox>
                                    <asp:CompareValidator ID=cvFIE runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaIniE Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                    <asp:Image ID="imgCalFechaIniE" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif" Style="cursor: pointer" visible=false/> 
                                </td>
                                <td nowrap width="100">
                                    <b>Hasta</b> <br />                                  
                                    <asp:TextBox ID="txtFiltroFechaFinE" runat="server" CssClass="texto"  Width=75px>[Fecha Hasta]</asp:TextBox>
                                    <asp:CompareValidator ID=cvFFE runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaFinE Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                    <asp:Image ID="imgCalFechaFinE" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif" Style="cursor: pointer" Visible=false />                                   
                                </td>  
                                <td valign="bottom">
                                    <asp:Button ID="btnActualizar" runat="server" Text=">>" 
                                        CssClass="boton" Width="19px"/>
                                </td>    
                            </tr>
                            <tr>
                                <td>
                                    <span class="style1">Vol. Consumido</span>&nbsp;(m3)
                                </td>
                                <td colspan="2">                             
                                    <asp:TextBox ID="txtVolumenGeneral" runat="server" width="128px"  
                                        CssClass="textoNumerico" ReadOnly=true></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="style1">Concesión Aprov.</span>&nbsp;(m3)
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtConcesionAprov" runat="server" width="128px"  
                                        CssClass="textoNumerico" ReadOnly=true Height="16px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>                
                                <td class="style1">
                                    % Total
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtPorcentajeTotal" runat="server" width="128px"  
                                        CssClass="textoNumerico" ReadOnly=true></asp:TextBox>
                                </td>
                            </tr>                
                        </table>
                    </asp:Panel>                 
                <!-- Fin Panel Editar Contadores -->
                </td>
             </tr>
        </table>
    </form>
</body>
</html>
