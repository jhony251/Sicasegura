<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PantallaInformeInterriego.aspx.vb" Inherits="SICAH_PantallaInformeInterriego" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link rel="stylesheet" href="../styles.css" />
    <script type="text/jscript" language="javascript" src="../js/utiles.js"></script>
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script type="text/jscript" language="javascript" src="../js/utilesListados.js"></script>
  <script type="text/javascript" language="javascript">
  var visibilidad="hidden";
  
    function MostrarPanel(){
      visibilidad="visible";  
    }

    function verVisibilidad(){
        document.getElementById("divReport").style.visibility=visibilidad;
    }

  </script>
</head>
<body>
    <form id="form1" runat="server">    
  <span id="dsp4"></span>
  <span id="imagepath" style="display:none">../js/calendar/images</span> 
  <table cellspacing="0" cellpadding="1" style="width: 100%">
            <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label><tr>
              <td style="height: 15px">
                  &nbsp;</td>
            </tr>
  </table>
    
        
   
        <asp:Panel ID="pnlFiltros" runat="server">
         <table cellspacing="0" cellpadding="1" width="100%">   
                                 
            <tr>                
                <td style="visibility:hidden"> Fecha Desde: 
                  <asp:TextBox ID="txtFechaIni" runat="server" CssClass="texto" Width="75px"></asp:TextBox>
                  <asp:CompareValidator ID="cvFiltroFechaIniI" runat="server" Text="*" ErrorMessage="Fecha no v�lida" ControlToValidate="txtFechaIni" Operator="DataTypeCheck" Type="date"></asp:CompareValidator>
                  <asp:Image ID="imgCalFechaIniI" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                  Style="cursor: pointer" />                                                                            
                </td>
                <td style="visibility:hidden"> Hasta: 
                  <asp:TextBox ID="txtFechaFin" runat="server" CssClass="texto" Width="75px"></asp:TextBox>
                  <asp:CompareValidator ID="cvtxtFiltroFechaFinI" runat="server" Text="*" ErrorMessage="Fecha no v�lida" ControlToValidate="txtFechaFin" Operator=DataTypeCheck Type=date></asp:CompareValidator>
                  <asp:Image ID="imgCalFechaFinI" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                  Style="cursor: pointer" />                                      
                </td>                                                                                                  
                <td>Agrupaci�n:                               
                <asp:DropDownList ID="ddlAgrupacion" runat="server" AutoPostBack="false" Style="font: 10px Verdana; text-align: right" Visible="true">
                </asp:DropDownList>
                <asp:Button ID="btnListado" runat="server" text="ListadoInterriego" CssClass="boton"/></td>               
            </tr>
            <tr><td colspan="3" style="padding-top:20px;"> 
            
            </td></tr>
          </table>
        </asp:Panel>
      
    </form>
</body>
</html>
