<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeyendasVisor.aspx.vb" Inherits="SICAH_LeyendasVisor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Leyendas</title>
    <link rel="stylesheet" href="..\styles.css">
    
</head>
<body style=" background:#3C5469">
    <form id="form1" runat="server">
    <table width="100%" height="100%"  cellpadding="0" cellspacing="0">
        <tr>
            <td  style=" font-family:Arial Black; font-size:9px;color:White; border:solid 1px white; background-color:#2257D6" align="center">CAPAS PUNTUALES</td>
            <td  style=" font-family:Arial Black; font-size:9px;color:White; border:solid 1px white; background-color:#2257D6" align="center">ICONO</td>
            <td  style=" font-family:Arial Black; font-size:9px;color:White; border:solid 1px white; background-color:#2257D6" align="center">DESCRIPCIÓN</td>
        </tr>  
        <tr>
            <td  class="LeyendaV" align="center">Alberca - Captaciones</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image1" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/captacion.png" /></td>
            <td  class="LeyendaV">Captaciones digitalizadas en ALBERCA</td>
        </tr>
      <tr>
            <td class="LeyendaV" align="center">Alberca - Usos Puntuales</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image23" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/uso.png" /></td>
            <td  class="LeyendaV">Usos puntuales digitalizados en ALBERCA </td>
        </tr>        
        <tr>
            <td  class="LeyendaV" align="center">INVENTARIO 66 - Tomas</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image2" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/toma66.png" /></td>
            <td  class="LeyendaV">Toma puntual inventariada en 1966</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">INVENTARIO 76 - Tomas</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image3" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/toma76.png" /></td>
            <td  class="LeyendaV">Toma puntual inventariada en 1976</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">INVENTARIO 90 - Tomas</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image4" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/toma90.png" /></td>
            <td  class="LeyendaV">Toma puntual inventariada en 1990</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">SICA - Aforos</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image5" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/regla.png" /></td>
            <td  class="LeyendaV">Estación de aforos de la ROEA</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">SICA - Cauces</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image6" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/cauce.png" /></td>
            <td  class="LeyendaV">Puntos de toma del cauce relacionado</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">SICA - Personal de campo</td>
            <td class="LeyendaV" align="center"></td>
            <td  class="LeyendaV">Personal de campo SICA</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">SICA - Puntos de control</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image8" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/icocua_B32.png" /></td>
            <td  class="LeyendaV">PUNTO DE CONTROL EN LAMINA LIBRE. CONTROL TOTAL Telemando</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">&nbsp;</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image9" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/icocua_G32.png" /></td>
            <td  class="LeyendaV">PUNTO DE CONTROL EN LAMINA LIBRE. CONTROL MAXIMO Telemedida tiempo real</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">&nbsp; </td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image10" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/icocua_O32.png" /></td>
            <td  class="LeyendaV">PUNTO DE CONTROL EN LAMINA LIBRE. CONTROL MEDIO Telemedida </td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">&nbsp; </td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image11" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/icocua_Y32.png" /></td>
            <td  class="LeyendaV">PUNTO DE CONTROL EN LAMINA LIBRE. CONTROL MINIMO Medida manual</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">&nbsp;</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image12" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/icocir_B32.png" /></td>
            <td  class="LeyendaV">PUNTO DE CONTROL EN CONDUCCION FORZADA. CONTROL TOTAL Telemando</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">&nbsp;</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image13" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/icocir_G32.png" /></td>
            <td  class="LeyendaV">PUNTO DE CONTROL EN CONDUCCION FORZADA. CONTROL MAXIMO Telemedida tiempo real</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">&nbsp;</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image14" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/icocir_O32.png" /></td>
            <td  class="LeyendaV">PUNTO DE CONTROL EN CONDUCCION FORZADA. CONTROL MEDIO Telemedida diaria</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">&nbsp;</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image15" runat="server" ImageUrl="~/SICAH/images/iconosBuenos/icocir_Y32.png" /></td>
            <td  class="LeyendaV">PUNTO DE CONTROL EN CONDUCCION FORZADA. MINIMO Medida manual</td>
        </tr>    
        <tr>
            <td  style=" font-family:Arial Black; font-size:9px;color:White; border:solid 1px white;background-color:#2257D6" align="center">CAPAS POLIGONALES</td>
            <td  style=" font-family:Arial Black; font-size:9px;color:White; border:solid 1px white;background-color:#2257D6" align="center">ICONO</td>
            <td  style=" font-family:Arial Black; font-size:9px;color:White; border:solid 1px white;background-color:#2257D6" align="center">DESCRIPCIÓN</td>
        </tr>        
        
        <tr>
            <td  class="LeyendaV" align="center">Alberca - Usos</td>
            <td class="LeyendaV" align="center" bgcolor="blue">&nbsp;</td>
            <td  class="LeyendaV">Usos superficiales digitalizados en ALBERCA</td>
        </tr>
        <tr>
            <td  class="LeyendaV" align="center">CHS - Avance ATS (Tabala)</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image18" runat="server" ImageUrl="~/images/CHSAvance.png" /></td>
            <td  class="LeyendaV">&nbsp;</td>
        </tr>        
        <tr>
            <td  class="LeyendaV" align="center">CHS – Canales</td>
            <td class="LeyendaV" align="center">&nbsp;</td>
            <td  class="LeyendaV">Canales de la CHSs</td>
        </tr>        
        <tr>
            <td  class="LeyendaV" align="center">CHS – Cuencas</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image19" runat="server" ImageUrl="~/images/CHSCuencas.png" /></td>
            <td  class="LeyendaV">Limites de la cuenca</td>
        </tr>        
        <tr>
            <td  class="LeyendaV" align="center">CHS - Embalses</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image20" runat="server" ImageUrl="~/images/CHSEmbalses.png" /></td>
            <td  class="LeyendaV">Embalses de la CHS</td>
        </tr>        
        <tr>
            <td  class="LeyendaV" align="center">CHS – Red fluvial</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image22" runat="server" ImageUrl="~/images/CHSRedfluvial.png" /></td>
            <td  class="LeyendaV">Red fluvial</td>
        </tr>             
        <tr>
            <td  class="LeyendaV" align="center">General – Municipios</td>
            <td class="LeyendaV" align="center" bgcolor="yellow">&nbsp;</td>
            <td  class="LeyendaV">Términos municipales</td>
        </tr>                
        <tr>
            <td  class="LeyendaV" align="center">Inventario 66 - Destinos</td>
            <td class="LeyendaV" align="center" bgcolor="#824169">&nbsp;</td>
            <td  class="LeyendaV">Destinos superficiales inventariados en 1966</td>
        </tr>                
        <tr>
            <td  class="LeyendaV" align="center">Inventario 76 - Destinos</td>
            <td class="LeyendaV" align="center" bgcolor="#E14D16">&nbsp;</td>
            <td  class="LeyendaV">Destinos superficiales inventariados en 1976</td>
        </tr>                
        <tr>
            <td  class="LeyendaV" align="center">Inventario 90 - Destinos</td>
            <td class="LeyendaV" align="center" bgcolor="#6EAA16">&nbsp;</td>
            <td  class="LeyendaV">Destinos superficiales inventariados en 1990</td>
        </tr>                
        <tr>
            <td  class="LeyendaV" align="center">PHC1998 - Acuíferos</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image7" runat="server" ImageUrl="~/images/CHSacuiferos.png" /></td>
            <td  class="LeyendaV">Acuíferos de la CHS</td>
        </tr>                
        <tr>
            <td  class="LeyendaV" align="center">PHC1998 - Unidades Hidrológicas</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image16" runat="server" ImageUrl="~/images/CHSHidrologicas.png" /></td>
            <td  class="LeyendaV">Unidades Hidrológicas</td>
        </tr>                        
        <tr>
            <td  class="LeyendaV" align="center">PHC1998 – UDAS</td>
            <td class="LeyendaV" align="center" bgcolor="#D98E16">&nbsp;</td>
            <td  class="LeyendaV">Unidades de demanda agraria</td>
        </tr>                        
        <tr>
            <td  class="LeyendaV" align="center">SICA - Destinos</td>
            <td class="LeyendaV" align="center" bgcolor="#CC0099">&nbsp;</td>
            <td  class="LeyendaV">&nbsp;</td>
        </tr>                        
        <tr>
            <td  class="LeyendaV" align="center">SICA - Colectivos</td>
            <td class="LeyendaV" align="center" bgcolor="#F5FF33">&nbsp;</td>
            <td  class="LeyendaV">&nbsp;</td>
        </tr>                        
        <tr>
            <td  class="LeyendaV" align="center">SICA - Acequias</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image17" runat="server" ImageUrl="~/images/CHSAcequias.png" /></td>
            <td  class="LeyendaV">&nbsp;</td>
        </tr>                        
        <tr>
            <td  class="LeyendaV" align="center">SIGPAC - Parcelas</td>
            <td class="LeyendaV" align="center"><asp:Image ID="Image21" runat="server" ImageUrl="~/images/CHSParcelas.png" /></td>
            <td  class="LeyendaV">&nbsp;</td>
        </tr>                                
        <tr>
            <td  class="LeyendaV" align="center">Recintos PAC</td>
            <td class="LeyendaV" align="center" bgcolor="#CC3399">&nbsp;</td>
            <td  class="LeyendaV">&nbsp;</td>
        </tr>                                
        
    </table>
    </form>
</body>
</html>
