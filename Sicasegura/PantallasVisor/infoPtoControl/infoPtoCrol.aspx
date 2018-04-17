<%@ Page Language="C#" AutoEventWireup="true" CodeFile="infoPtoCrol.aspx.cs" Inherits="infoPtoCrol" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CONSULTA DE ATRIBUTOS DE PUNTOS DE CONTROL</title>
    <link rel=stylesheet href="estilos.css">
    <script language="javascript">
    function redimensiona(){
    
        window.resizeTo(document.getElementById('tablacabecera').offsetWidth+70,(document.getElementById('cuerpo').offsetHeight +  document.getElementById('td1').offsetHeight*14));        
           }
    </script>
<base target="_self" />
<script language="javascript" src="js/calendar/calendar.js"></script>

</head>
<body onload="redimensiona();" id="cuerpo">

    <form id="form1" runat="server">
<fieldset style="border:2px ridge #CCCC99;padding:8px">
<legend align=center style="font-weight:bold; color:black; font-size:14px">Atributos Ptos de Control</legend>
  <asp:Repeater ID=rptCabecera Runat=server>
    
    <ItemTemplate>

        <table id="tablaCabecera" border=0 > 
        <tr id="tr1" >
            <td colspan="3" style="border-top:double 4px #006E00; border-bottom:double 4px #006E00;border-left:double 2px #006E00;border-right:double 2px #006E00">
                <table cellspacing=0 cellpadding=5 >
                <tr>
                    <td nowrap style="font-weight:bold; background-color:#CCD7A2; text-align:left" id="td1">Cod. Cauce:</td>
                    <td nowrap style=" background-color:#CCD7A2; text-align:left"><%#DataBinder.Eval(Container.DataItem, "CodigoCauce")%></td>
                    <td nowrap style=" background-color:#CCD7A2; text-align:left"><%#DataBinder.Eval(Container.DataItem, "DenominacionCauce")%></td>
                </tr>
	            <tr>
	                <td nowrap style="font-weight:bold; background-color:#CCD7A2">Cod. PVYCR: </td>
	                <td nowrap style=" background-color:#CCD7A2; text-align:left"><%#DataBinder.Eval(Container.DataItem, "CodigoPVYCR")%></td>
	                <td nowrap style=" background-color:#CCD7A2; text-align:left"><%#DataBinder.Eval(Container.DataItem, "DenominacionPunto")%></td>
	            </tr>    
                </table>
            </td>            
        </tr>
        <tr>
            <td style="background-color:#AFAF6; border-collapse:collapse" colspan=3>&nbsp</td>
        </tr>	
	
        <tr align=left >
            <td style="font-weight:bold; color:#004700; font-size:12px; ; background-color:#BBBB7C" colspan=2>Datos Motores</td>
        </tr>    	
	    <tr>
	        <td style="font-weight:bold">X UTM: </td>
	        <td colspan="1" class=L align=left><%#DataBinder.Eval(Container.DataItem, "X_UTM")%></td>
	    </tr>
	    <tr>
	        <td style="font-weight:bold">Y UTM: </td>
	        <td class=L align=left><%#DataBinder.Eval(Container.DataItem, "Y_UTM")%></td>
	    </tr>
        <tr>
            <td style="background-color:#AFAF6; border-collapse:collapse" colspan=3>&nbsp</td>
        </tr>	
        </table>
        </ItemTemplate>
        </asp:Repeater>
   <asp:Repeater ID=rptPuntosE Runat=server>
    
    <ItemTemplate>

        <table id="tablaCabecera" border=0 > 
    <!--PANEL energia-->
        <tr style="display:<%#checkVisibleE(Container.DataItem)%>">
            <td style="background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#004700; font-size:12px; background-color:#BBBB7C" colspan=2>Datos última lectura</td>
        </tr>		
        <tr style="display:<%#checkVisibleE(Container.DataItem)%>">
            <td style="background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#004700; font-size:11px;background-color:#D7C177" colspan=2>Contador Energía: <%#DataBinder.Eval(Container.DataItem, "idelementoMedidaE")%></td>
        </tr>    	
        <%#checkSinLecturasE(Container.DataItem)%>	    	
	    <tr style="display:<%#checkVisibleE(Container.DataItem)%>">
	        <td style="font-weight:bold">Fecha: </td>
	        <td class=L  align=left><%#DataBinder.Eval(Container.DataItem, "Fecha_medidaE", "{0:dd/MM/yyyy HH:mm}")%></td>
	    </tr>	
	    <tr style="display:<%#checkVisibleE(Container.DataItem)%>">
	        <td style="font-weight:bold">Lectura I: </td>
	        <td class=L align=left><%#DataBinder.Eval(Container.DataItem, "LecturaI", "{0:#,##0.###}")%></td>
	    </tr>
	    <tr style="display:<%#checkVisibleE(Container.DataItem)%>">
	        <td style="font-weight:bold">Lectura II: </td>
	        <td class=L align=left><%#DataBinder.Eval(Container.DataItem, "LecturaII", "{0:#,##0.###}")%></td>
	    </tr>
	    <tr style="display:<%#checkVisibleE(Container.DataItem)%>">
	        <td style="font-weight:bold">Lectura III: </td>
	        <td class=L align=left ><%# DataBinder.Eval(Container.DataItem, "LecturaIII", "{0:#,##0.###}")%></td>
	    </tr>
	    <tr style="display:<%#checkVisibleE(Container.DataItem)%>">
	        <td style="font-weight:bold">Total (Kwh): </td>
	        <td class=L align=left ><%# DataBinder.Eval(Container.DataItem, "Total_KWH", "{0:#,##0.#}")%></td>
	    </tr>
	    <tr style="display:<%#checkVisibleE(Container.DataItem)%>">
	        <td style="font-weight:bold">Total (Kvar): </td>
	        <td class=L align=left ><%# DataBinder.Eval(Container.DataItem, "Total_KVAR", "{0:#,##0.#}")%></td>
	    </tr>
        <tr style="display:<%#checkVisibleE(Container.DataItem)%>;background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#BD4600; font-size:14px">
                <td >Número Lecturas:
                    <asp:Label runat=server ID="lblNlecturasE" Text="<%#checkNumLecturasE((System.Data.DataRowView)Container.DataItem)%>" ></asp:Label>
                </td>
        </tr> 
    </table>
  
    </ItemTemplate>
   </asp:Repeater> 
     <asp:Repeater ID=rptPuntosV Runat=server>
    
    <ItemTemplate>

        <table id="tablaCabecera" border=0 > 
    <!--PANEL VOLUMENES-->
        <tr style="display:<%#checkVisibleV(Container.DataItem)%>">
            <td style="background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#004700; font-size:12px; background-color:#BBBB7C" colspan=2>Datos última lectura</td>
        </tr>		
        <tr style="display:<%#checkVisibleV(Container.DataItem)%>">
            <td style="background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#004700; font-size:11px;background-color:#D7C177" colspan=2>Contador Volumétrico: <%#DataBinder.Eval(Container.DataItem, "idElementoMedidaV")%></td>
        </tr>    	
        <%#checkSinLecturasV(Container.DataItem)%>	    	
	    <tr style="display:<%#checkVisibleV(Container.DataItem)%>">
	        <td style="font-weight:bold">Fecha: </td>
	        <td class=L  align=left><%#DataBinder.Eval(Container.DataItem, "Fecha_medidaV", "{0:dd/MM/yyyy HH:mm}")%></td>
	    </tr>	
	    <tr style="display:<%#checkVisibleV(Container.DataItem)%>">
	        <td style="font-weight:bold">Lectura Cont.(m3): </td>
	        <td class=L align=left><%#DataBinder.Eval(Container.DataItem, "LecturaContador_M3", "{0:#,##0.###}")%></td>
	    </tr>
        <tr style="display:<%#checkVisibleV(Container.DataItem)%>;background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#BD4600; font-size:14px">
                <td >Número Lecturas:
                    <asp:Label runat=server ID="lblNlecturasV" Text="<%#checkNumLecturasV((System.Data.DataRowView)Container.DataItem)%>" ></asp:Label>
                </td>
        </tr> 
    </table>
  
    </ItemTemplate>
   </asp:Repeater> 
      <asp:Repeater ID=rptPuntosH Runat=server>
    
    <ItemTemplate>

        <table id="tablaCabecera" border=0 > 
    <!--PANEL HOROMETROS-->
        <tr style="display:<%#checkVisibleH(Container.DataItem)%>">
            <td style="background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#004700; font-size:12px; background-color:#BBBB7C" colspan=2>Datos última lectura</td>
        </tr>		
        <tr style="display:<%#checkVisibleH(Container.DataItem)%>">
            <td style="background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#004700; font-size:11px;background-color:#D7C177" colspan=2>Contador Horómetros: <%#DataBinder.Eval(Container.DataItem, "idElementoMedidaH")%></td>
        </tr>    	
    	<%#checkSinLecturasH(Container.DataItem)%>	
	    <tr style="display:<%#checkVisibleH(Container.DataItem)%>">
	        <td style="font-weight:bold">Fecha: </td>
	        <td class=L  align=left><%#DataBinder.Eval(Container.DataItem, "Fecha_medidaH", "{0:dd/MM/yyyy HH:mm}")%></td>
	    </tr>	
	      <tr style="display:<%#checkVisibleH(Container.DataItem)%>">
	        <td style="font-weight:bold">Horas Intervalo: </td>
	        <td class=L align=left ><%#DataBinder.Eval(Container.DataItem,"HorasIntervalo")%></td>
	    </tr>
       <tr style="display:<%#checkVisibleH(Container.DataItem)%>;background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#BD4600; font-size:14px">
            <td >Número Lecturas:
            <asp:Label runat=server ID="lblNlecturasH" Text="<%#checkNumLecturasH((System.Data.DataRowView)Container.DataItem)%>" ></asp:Label>
            </td>
       </tr> 	
    </table>
  
    </ItemTemplate>
   </asp:Repeater> 
   <asp:Repeater ID=rptCabeceraA Runat=server EnableTheming="False">
    <ItemTemplate>
    <table id="tablaCabecera"> 
    <tr id="tr1">
        <td colspan="3">
        <table cellspacing=0 cellpadding=5 style="border-top:double 4px #006E00; border-bottom:double 4px #006E00;border-left:double 2px #006E00;border-right:double 2px #006E00">
        <tr>
            <td nowrap style="font-weight:bold; background-color:#CCD7A2; text-align:left" id="td1">Cod. Cauce:</td>
            <td nowrap style=" background-color:#CCD7A2; text-align:left"><%#DataBinder.Eval(Container.DataItem, "CodigoCauce")%></td>
            <td nowrap style=" background-color:#CCD7A2; text-align:left"><%#DataBinder.Eval(Container.DataItem, "DenominacionCauce")%></td>        
        </tr>        
	    <tr>
	        <td nowrap style="font-weight:bold; background-color:#CCD7A2">Cod. PVYCR: </td>
	        <td nowrap style=" background-color:#CCD7A2; text-align:left"><%#DataBinder.Eval(Container.DataItem, "CodigoPVYCR")%></td>
	        <td nowrap style=" background-color:#CCD7A2; text-align:left"><%#DataBinder.Eval(Container.DataItem, "DenominacionPunto")%></td>
	    </tr>
        </table>
        </td>
    </tr>
    <!--tr>
        <td style="font-weight:bold">Denominación Cauce:</td>
        <td class=L align=left><%#DataBinder.Eval(Container.DataItem, "DenominacionCauce")%></td>
    </tr>-->
	<!--tr>
	    <td style="font-weight:bold">Denominacion Pto.:</td>
	    <td class=L align=left><%#DataBinder.Eval(Container.DataItem, "DenominacionPunto")%></td>
	</tr>--> 
    <tr>
        <td style="background-color:#AFAF6; border-collapse:collapse" colspan=3>&nbsp</td>
    </tr>	
    <tr align=left >
        <td style="font-weight:bold; color:#004700; font-size:12px; background-color:#BBBB7C" colspan=2>Datos Acequias</td>
    </tr>    
	
	<tr>
	    <td style="font-weight:bold">X UTM: </td>
	    <td class=L align=left><%#DataBinder.Eval(Container.DataItem, "X_UTM")%></td>
	</tr>
	<tr>
	    <td style="font-weight:bold">Y UTM: </td>
	    <td class=L align=left><%#DataBinder.Eval(Container.DataItem, "Y_UTM")%></td>
	</tr>
    <tr>
        <td style="background-color:#AFAF6; border-collapse:collapse" colspan=3>&nbsp</td>
    </tr>	
     </table>
  
    </ItemTemplate>
   </asp:Repeater> 
  <asp:Repeater ID=rptPuntosA Runat=server EnableTheming="False">
    <ItemTemplate>
    <table id="tablaCabecera"> 
    <tr style="display:<%#checkVisibleA(Container.DataItem)%>">
        <td style="background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#004700; font-size:12px; background-color:#BBBB7C" colspan=2>Datos última lectura</td>
    </tr>		
    <tr style="display:<%#checkVisibleA(Container.DataItem)%>">
        <td style="background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#004700; font-size:11px;background-color:#D7C177" colspan=2>Contador Caudalímetro: <%#DataBinder.Eval(Container.DataItem,"idelementoMedida")%></td>
    </tr>    	
    <%#checkSinLecturasA(Container.DataItem)%>	
	<tr style="display:<%#checkVisibleA(Container.DataItem)%>">
	    <td style="font-weight:bold">Fecha: </td>
	    <td class=L  align=left><%#DataBinder.Eval(Container.DataItem, "Fecha_medida", "{0:dd/MM/yyyy HH:mm}")%></td>
	</tr>
	<tr style="display:<%#checkVisibleA(Container.DataItem)%>">
	    <td style="font-weight:bold">Escala(m):</td>
	    <td class=L align=left> <%#DataBinder.Eval(Container.DataItem, "Escala_M", "{0:#,##0.##}")%></td>
	</tr>
	<tr style="display:<%#checkVisibleA(Container.DataItem)%>">
	    <td style="font-weight:bold">Calado(m):</td>
	    <td class=L align=left> <%#DataBinder.Eval(Container.DataItem, "Calado_M", "{0:#,##0.##}")%></td>
	</tr>
	<tr style="display:<%#checkVisibleA(Container.DataItem)%>">
	    <td style="font-weight:bold">Tipo Obt. Caudal:</td>
	    <td class=L align=left> <%#DataBinder.Eval(Container.DataItem, "TipoObtencionCaudal")%></td>
	</tr>
	<tr style="display:<%#checkVisibleA(Container.DataItem)%>">
	    <td style="font-weight:bold">Régimen Curva:</td>
	    <td class=L align=left> <%#DataBinder.Eval(Container.DataItem, "RegimenCurva")%></td>
	</tr>
	<tr style="display:<%#checkVisibleA(Container.DataItem)%>">
	    <td style="font-weight:bold">Número Parada:</td>
	    <td class=L align=left> <%#DataBinder.Eval(Container.DataItem, "NumeroParada")%></td>
	</tr>
	<tr style="display:<%#checkVisibleA(Container.DataItem)%>">
	    <td style="font-weight:bold">Caudal (m3):</td>
	    <td class=L align=left> <%#DataBinder.Eval(Container.DataItem, "Caudal_M3S")%></td>
	</tr>
	<tr style="display:<%#checkVisibleA(Container.DataItem)%>">
	    <td style="font-weight:bold">Duda Calidad:</td>
	    <td class=L align=left> <%#DataBinder.Eval(Container.DataItem, "DUDA_CALIDAD")%></td>
	 </tr>
	<tr style="display:<%#checkVisibleA(Container.DataItem)%>">
	    <td style="font-weight:bold">Observaciones:</td>
	    <td class=L align=left> <%#DataBinder.Eval(Container.DataItem, "Observaciones")%></td>
	</tr>
       <tr style="display:<%#checkVisibleA(Container.DataItem)%>;background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#BD4600; font-size:14px">
            <td >Número Lecturas:
            <asp:Label runat=server ID="lblNlecturasQ" Text="<%#checkNumLecturasQ((System.Data.DataRowView)Container.DataItem)%>" ></asp:Label>
            </td>
       </tr> 	
	
    </table>
    </ItemTemplate>

    </asp:Repeater>    
  
    <table width=100% style="display:<%#checkOcultarTabla() %>">
        <tr>
        <td>&nbsp;</td>
        </tr>
        <tr style="display:<%#checkVisibleLecturasM()%>;background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#BD4600; font-size:16px">
            <td style='background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#9B4600; font-size:11px'>Número Lecturas:
                <asp:Label runat=server ID="lblNlecturas" Text="<%#checkNumLecturasM()%>" ></asp:Label>
            </td>
        </tr> 
        <tr style="display:<%#checkVisibleLecturasA()%>;background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#BD4600; font-size:14px">
            <td style='background-color:#AFAF6; border-collapse:separate; font-weight:bold; color:#9B4600; font-size:11px'>Número Lecturas: 
                <asp:Label runat=server ID="Label1" Text="<%#checkNumLecturasA()%>" ></asp:Label>
            </td>
        </tr> 

    </table>
     </fieldset> 
    <table>
    
    <tr>
        <td><b><i>
            <asp:Label runat=server ID="lblfecha" style="font-weight:bold; color:#004700; font-size:10px;" Text="Seleccionar Fecha" ></asp:Label>
        </i></b>
        
    
    
    <span id="dsp4"></span>    
    <span id="imagepath" style="display:none">js/calendar/images</span>
    <asp:Image runat="Server" ID="imgCalFRDesde" ImageUrl="images/calendario.gif"  style="cursor:pointer" ImageAlign="AbsMiddle" />
    <asp:TextBox runat="server" ID="txtFechaDesde" CssClass="texto" Text='' Width="70" EnableViewState=false></asp:TextBox>
    <asp:Button runat = "server" ID="btnAceptar"  OnClick="btnAceptarFecha_click" Text="Aceptar" style="background-color:#AFAF6A; border: 1px solid black; font-size: 10px"/>
    <br />
    <asp:RequiredFieldValidator ID="rfVallidato" runat=server ControlToValidate="txtFechaDesde" Display=Dynamic ErrorMessage="Debe seleccionar una fecha"></asp:RequiredFieldValidator>
    </td>
    </tr>
   </table>
  <table width="100%">
    <tr>
    <td align="center"><asp:Button runat = "server" ID="btnArbol" Text="Ubicación y características del punto de control" style="background-color:#AFAF6A; border: 1px solid black; font-size: 10px" OnClick="btnArbol_Click" CausesValidation="false"/></td>
    </tr>
  </table>
    </form>
  
 
</body>
</html>
