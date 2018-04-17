<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PantallaInformes.aspx.vb" Inherits="SICAH_PantallaInformes" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Listados disponibles</title>
       <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  
      <script type="text/jscript" language="javascript" src="../js/utiles.js"></script>
      <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
      <script type="text/jscript" language="javascript" src="../js/utilesListados.js"></script>
      <script type="text/javascript" language="javascript" src="../js/utilesMenus.js"></script>  
      <link href="../styles.css" rel="stylesheet" />
      <link href="../styles.css" rel="stylesheet" /> 
    
    <script language=javascript>
    function VerInformesel(){
        if (document.getElementById('Informesel').value == 'DocumentacionElemento'){
            document.getElementById('DocumentacionElemento').style.display='none';
            document.getElementById('DocumentacionElementoR').style.display='block';
        }
    }
    
    function mostrar(id){
        document.getElementById('GraficosL').style.display='block';
        document.getElementById('GraficosC').style.display='block';
        document.getElementById('GraficosCA').style.display='block';
        document.getElementById('InformesL').style.display='block';
        document.getElementById('InformesC').style.display='block';
        document.getElementById('Consumo').style.display='block';
        document.getElementById('DocumentacionCauce').style.display='block';
        document.getElementById('DocumentacionPunto').style.display='block';        
        document.getElementById('DocumentacionElemento').style.display='block';
        document.getElementById('GraficosLR').style.display='none';
        document.getElementById('GraficosCR').style.display='none';
        document.getElementById('GraficosCAR').style.display='none';
        document.getElementById('InformesLR').style.display='none';
        document.getElementById('InformesCR').style.display='none';
        document.getElementById('ConsumoR').style.display='none';
        document.getElementById('DocumentacionCauceR').style.display='none';
        document.getElementById('DocumentacionPuntoR').style.display='none';        
        document.getElementById('DocumentacionElementoR').style.display='none';
                
        /*document.getElementById('DivGraficosL').style.display='block';
        document.getElementById('DivGraficosC').style.display='block';
        document.getElementById('DivGraficosCA').style.display='block';
        document.getElementById('DivInformesL').style.display='block';
        document.getElementById('DivInformesC').style.display='block';
        document.getElementById('DivGraficosLR').style.display='none';
        document.getElementById('DivGraficosCR').style.display='none';
        document.getElementById('DivGraficosCAR').style.display='none';
        document.getElementById('DivInformesLR').style.display='none';
        document.getElementById('DivInformesCR').style.display='none';        */
        
        document.getElementById(id).style.display='none';
        document.getElementById(id+'R').style.display='block';
        
        /*if ((id != "Consumo") && (id != "DocumentacionCauce") && (id != "DocumentacionPunto") && (id != "DocumentacionElemento")) {
            document.getElementById("Div"+id).style.display='none';
            document.getElementById("Div"+id+'R').style.display='block';
        }*/
        
        document.getElementById('Informesel').value=id;


        switch (id){
            case "GraficosL":
                document.getElementById("pnlFiltroFechas1").style.display="none";
                document.getElementById("pnlFiltroFechas2").style.display="none";
                document.getElementById("pnlFiltroFechas3").style.display="none";
                break;
            case "GraficosCA":
                document.getElementById("pnlFiltroFechas1").style.display="none";
                document.getElementById("pnlFiltroFechas2").style.display="none";
                document.getElementById("pnlFiltroFechas3").style.display="none";
                break;
            case "InformesL":
                document.getElementById("pnlFiltroFechas1").style.display="none";
                document.getElementById("pnlFiltroFechas2").style.display="none";
                document.getElementById("pnlFiltroFechas3").style.display="none";
                document.getElementById("pnlFiltroFechas4").style.display="none";
                break;
            case "DocumentacionCauce":
                document.getElementById("pnlFiltroFechas1").style.display="none";
                document.getElementById("pnlFiltroFechas2").style.display="none";
                document.getElementById("pnlFiltroFechas3").style.display="none";
                document.getElementById("pnlFiltroFechas4").style.display="none";
                break;
            case "DocumentacionPunto":
                document.getElementById("pnlFiltroFechas1").style.display="none";
                document.getElementById("pnlFiltroFechas2").style.display="none";
                document.getElementById("pnlFiltroFechas3").style.display="none";
                document.getElementById("pnlFiltroFechas4").style.display="none";
                break;
            case "DocumentacionElemento":
                document.getElementById("pnlFiltroFechas1").style.display="none";
                document.getElementById("pnlFiltroFechas2").style.display="none";
                document.getElementById("pnlFiltroFechas3").style.display="none";
                document.getElementById("pnlFiltroFechas4").style.display="none";
                break;
            default:
                document.getElementById("pnlFiltroFechas1").style.display="block";
                document.getElementById("pnlFiltroFechas2").style.display="block";
                document.getElementById("pnlFiltroFechas3").style.display="block";
                document.getElementById("pnlFiltroFechas4").style.display="block";
        }
        
        
    }

    function ocultar(id){
        document.getElementById('Informesel').value='ninguno';
        document.getElementById(id).style.display='block';
        document.getElementById(id+'R').style.display='none';
        /*document.getElementById("Div"+id).style.display='block';
        document.getElementById("Div"+id+'R').style.display='none';*/
    }
    
    function abreInforme() { 
    var nombreinforme;
    var CodigoPVYCR = document.getElementById("txtCodigoPVYCR").value;
    var IdElementoMedida = document.getElementById("txtIdElementoMedida").value;   
    var FiltroFechaIni = document.getElementById("txtFiltroFechaIniI").value;
    var FiltroFechaFin = document.getElementById("txtFiltroFechaFinI").value;
    var FiltroIntervalo = document.getElementById("ddlIntervaloI").value;
    var nodoTexto = document.getElementById("txtNodoTexto").value;
    var nodoClave = document.getElementById("txtNodoClave").value;
        
    //ncm 30/03/2009 si es peaje o trasvase el valor del intervalo será el del ddlIntervaloD
    if ((nodoTexto.substring(0,1) == 'P') || (nodoTexto.substring(0,1) == 'T')){
        FiltroIntervalo = document.getElementById("ddlIntervaloD").value;
    }
    if (document.getElementById('Informesel').value == 'GraficosL')
    {
        //nombreinforme = "../listados/GraficoLecturas.aspx?nodotexto=" + nodoTexto + "&nodoclave=" + nodoClave + "&filtrointervalo=" + FiltroIntervalo + "&filtroFechaIni=" + FiltroFechaIni + "&FiltroFechaFin=" + FiltroFechaFin;
        //IPIM 20/08/2009: Para probar los gráficos de Marcos
        //nombreinforme="../listados/GraficoLecturas.aspx"
        nombreinforme = "../listados/GraficoLecturas.aspx?nodotexto=" + nodoTexto + "&nodoclave=" + nodoClave + "&filtrointervalo=" + FiltroIntervalo + "&filtroFechaIni=" + FiltroFechaIni + "&FiltroFechaFin=" + FiltroFechaFin;
    }
    else if (document.getElementById('Informesel').value == 'GraficosC')
    {
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + nodoTexto + "&nodoclave=" + nodoClave + "&filtrointervalo=" + FiltroIntervalo + "&filtroFechaIni=" + FiltroFechaIni + "&FiltroFechaFin=" + FiltroFechaFin + "&grafico=si";        
    }
    else if (document.getElementById('Informesel').value == 'GraficosCA')
    {
        nombreinforme = "CurvasAcequiasPreproduccionFlash.aspx" 
    }
    else if (document.getElementById('Informesel').value == 'InformesL')
    {
        //nombreinforme = "../listados/InformeVolumenes.aspx?nodotexto=" + Request.QueryString("nodotexto").ToString + "&nodoclave=" + Request.QueryString("nodoclave").ToString + "&Filtro=" 
        //nombreinforme = "../listados/InformeVolumenes.aspx?nodotexto=" + nodoTexto + "&nodoclave=" + nodoClave  
        
        nombreinforme = "../listados/InformeVolumenes.aspx?nodotexto=" + nodoTexto + "&nodoclave=" + nodoClave + "&informe=xls"    
     
    }
    else if (document.getElementById('Informesel').value == 'InformesC')
    {
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + nodoTexto + "&nodoclave=" + nodoClave + "&filtrointervalo=" + FiltroIntervalo + "&filtroFechaIni=" + FiltroFechaIni + "&FiltroFechaFin=" + FiltroFechaFin + "&informe=xls";
    }
    else if (document.getElementById('Informesel').value == 'Consumo')
    {
        nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + nodoTexto + "&nodoclave=" + nodoClave + "&filtrointervalo=" + FiltroIntervalo + "&filtroFechaIni=" + FiltroFechaIni + "&FiltroFechaFin=" + FiltroFechaFin;
    }
    else if (document.getElementById('Informesel').value == 'DocumentacionElemento')
    {
        nombreinforme = "../listados/ListadoContadorPantalla.aspx?codigoPVYCR=" + CodigoPVYCR;
    }
    else if (document.getElementById('Informesel').value == 'DocumentacionCauce')
    {
        nombreinforme = "../listados/InformeCauces.aspx?codigoPVYCR=" + CodigoPVYCR + "&nodoClave=" + nodoClave;
    }
    else if (document.getElementById('Informesel').value == 'DocumentacionPunto')
    {
        nombreinforme = "../listados/InformePuntos.aspx?codigoPVYCR=" + CodigoPVYCR + "&nodoClave=" + nodoClave;
    }
    else
	{
		alert('Debe seleccionar el informe que quiera mostrar');
		return;
	}

	if ((document.getElementById("txtTieneLecturas").value == '0') && (document.getElementById('Informesel').value == 'GraficosL')) {
	    alert('No se puede mostrar el gráfico porque no tiene lecturas cargadas');
	    return;
	}

    window.open(nombreinforme);
    }
 
     function mostrarIframe(nombreFrame)
    {
        if (nombreFrame == 'iframeInfC')
        {   
            window.parent.document.getElementById('iframeInfC').style.display = '';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }
        else if (nombreFrame == 'iframeInfP')
        {
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = '';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';

        }
        else if (nombreFrame == 'iframeInfE')
        {
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = '';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }        
      else if (nombreFrame == 'iframeinformes')
        {
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = '';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        } 
      else if (nombreFrame == 'iframeLecturas')
        {
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = '';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }
      else if (nombreFrame == 'iframeDetalleVisor')
        {
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = '';

        }                                 
        
    }


 
    </script>
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
</head>
<body  style="width: 900px; background-color:White" onload="VerInformesel()">
    <form id="form1" runat="server">
        <span id="dsp4"></span>
        <span id="imagepath" style="display:none">../js/calendar/images</span>
        <table width="100%" cellspacing="0" align="center" style="border: 0px solid #666666;background-color: white">
        <tr>
        <td style="width: 900px">
         <table cellspacing="0" cellpadding="1" width="100%">
            <tr>
              <td style="width: 900px">
                 <table width="100%">
                     <tr>
                     <asp:Label ID="lblPestanyasArbol" runat="server"></asp:Label>                                               
                       <td width=50% >
                           <strong>
                            <asp:TextBox ID="txtCodigoPVYCRB" runat="server" CssClass="texto" ToolTip="CodigoPVYCR"
                                   Width="115px" Visible="false">[CodigoPVYCR]</asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCodigoPVYCRB"
                                   Display="Dynamic" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator></strong>
                                   <asp:TextBox ID="txtIdElementoMedidaB" runat="server" CssClass="texto" Width="27px" ToolTip="Id Elemento Medida" Visible="false">[EM]</asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtIdElementoMedidaB" Display="Dynamic">*</asp:RequiredFieldValidator>&nbsp;
                              <asp:TextBox ID="txtDescripcionElementoMedida" runat="server" CssClass="texto" Visible="False"
                               Width="44px"></asp:TextBox><asp:Label ID="LBLinfo" runat="server" Width="0px" Visible="false"></asp:Label>
                           
                            <b><asp:LinkButton ID="lbtAceptar" runat="server" Text="Búsqueda Rápida" CssClass="enlaceLecturas" Visible="false"></asp:LinkButton></b>
                              <asp:ImageButton ID="imgVisor" runat="server" ImageUrl="~/SICAH/images/iconos/imgVisor.gif"
                               ImageAlign="TextTop" Visible="false"/>
                           </td>    

                      </tr>
                  </table>
              </td>
                         
             </tr>
          <table width=97% cellspacing="2" bgcolor=#FEFCC1 cellpadding="5"  >                                
                    <tr>
                        <td nowrap class="tituloLecturas" colspan="10"><asp:Label ID="lblNodo" visible="true" runat="server"></asp:Label></td>
                        
                    </tr>
          </table>
 
        <table width=97% cellspacing="2" bgcolor=#FEFCC1 cellpadding="5" >   
            <tr>

                <td   class="encabListado" colspan="10">                                        
                        <asp:Label ID="Label1" runat="server" Width="21px" Visible="true">Posibles Informes</asp:Label>  
                </td>    
            </tr>
        </table>
            
        <tr>
        <td>
        <table width="97%" cellspacing="2" align="center" style="border: 1px solid #666666;background-color: white" cellpadding="5" >
            <tr>
                <asp:TextBox id="Informesel" visible="true" runat="server" style="display:none"></asp:TextBox>
                <asp:TextBox id="txtCodigoPVYCR" visible="true" runat="server" style="display:none"></asp:TextBox>
                <asp:TextBox id="txtIdElementoMedida" visible="true" runat="server" style="display:none"></asp:TextBox>
                <asp:TextBox id="txtNodoClave" visible="true" runat="server" style="display:none"></asp:TextBox>
                <asp:TextBox id="txtNodoTexto" visible="true" runat="server" style="display:none"></asp:TextBox>
            </tr>
            <tr>            
                <td align="center" style="display: <%= verVisibilidad("GraficosL") %>">
                   
                    <img src="../images/Informes/graficosL.gif" id="GraficosL"  style="cursor: pointer;"  onclick="mostrar('GraficosL')" /> 
                    <img src="../images/Informes/graficosLR.gif" id="GraficosLR" style="cursor: pointer;display:none" onclick="ocultar('GraficosL')" />
                </td>
                <td align="center" style="display: <%= verVisibilidad("InformesL") %>">                    
                    
                    <img src="../images/Informes/InformesL.gif" id="InformesL" style="cursor: pointer;"  onclick="mostrar('InformesL')"   />
                    <img src="../images/Informes/InformesLR.gif" id="InformesLR" style="cursor: pointer;display:none" onclick="ocultar('InformesL')" />
                </td>             
                <td align="center" style="display: <%= verVisibilidad("DocumentacionCauce") %>">
                    <img src="../images/Informes/Cauces.gif" id="DocumentacionCauce" style="cursor: pointer;"  onclick="mostrar('DocumentacionCauce')"   />
                    <img src="../images/Informes/CaucesR.gif" id="DocumentacionCauceR" style="cursor: pointer;display:none" onclick="ocultar('DocumentacionCauce')" />
                </td>
            </tr>
            <tr>
                <td align="center" style="display: <%= verVisibilidad("GraficosC") %>">
                    
                    <img src="../images/Informes/graficosC.gif" id="GraficosC"  style="cursor: pointer;"  onclick="mostrar('GraficosC')"  />
                    <img src="../images/Informes/graficosCR.gif" id="GraficosCR" style="cursor: pointer;display:none" onclick="ocultar('GraficosC')" />
                </td>
                <td align="center" style="display: <%= verVisibilidad("InformesC") %>">
                   
                    <img src="../images/Informes/InformesC.gif" id="InformesC" style="cursor: pointer;"  onclick="mostrar('InformesC')"  />
                    <img src="../images/Informes/InformesCR.gif"  id="InformesCR" style="cursor: pointer;display:none" onclick="ocultar('InformesC')" />
                </td>                
                <td align="center" style="display:<%= verVisibilidad("DocumentacionPunto") %>">
                    <img src="../images/Informes/DocumentacionPunto.gif" id="DocumentacionPunto"  style="cursor: pointer;" onclick="mostrar('DocumentacionPunto')"   />
                    <img src="../images/Informes/DocumentacionPuntoR.gif" id="DocumentacionPuntoR" style="cursor: pointer;display:none" onclick="ocultar('DocumentacionPunto')"   />
                </td>
             </tr>
            <tr>
                <td align="center" style="display: <%= verVisibilidad("GraficosCA") %>">
                  
                    <img src="../images/Informes/GraficosCA.gif" id="GraficosCA" style="cursor: pointer;"  onclick="mostrar('GraficosCA')"  />
                    <img src="../images/Informes/GraficosCAR.gif" id="GraficosCAR" style="cursor: pointer;display:none" onclick="ocultar('GraficosCA')" />
                </td>
                <td align="center" style="display: <%= verVisibilidad("Consumo") %>">
                    <img src="../images/Informes/Consumo.gif" id="Consumo" style="cursor: pointer;display:none;"  onclick="mostrar('Consumo')"  />
                    <img src="../images/Informes/ConsumoR.gif"  id="ConsumoR" style="cursor: pointer;" onclick="ocultar('Consumo')" />
                </td>
                <td align="center" style="display: <%= verVisibilidad("DocumentacionElemento") %>">
                    <img src="../images/Informes/DocumentacionElemento.gif" style="cursor: pointer;"  id="DocumentacionElemento"  onclick="mostrar('DocumentacionElemento')"  />
                    <img src="../images/Informes/DocumentacionElementoR.gif"  id="DocumentacionElementoR" style="cursor: pointer;display:none;" onclick="ocultar('DocumentacionElemento')" />
             </td>                

            </tr>
            </table>
            </td>
            </tr>
            </table>
            </td>
            </tr>
            
        <table width="97%" align="center" style="border: 1px solid #666666;background-color: #cccccc" cellpadding="5" >                         
            <tr></tr>
            <tr>
                <td style="background-color:#cccccc;" colspan=14>
                
                <table align="left">
                <tr>                                                                        
                    <td  valign="top" style="display: <%= verVisibilidad("FiltroFechas1") %>"> 
                     <asp:Panel ID="pnlFiltroFechas1" runat="server">
                        Fecha Desde: 
                      <asp:TextBox ID="txtFiltroFechaIniI" runat="server" CssClass="texto" Width="75px"></asp:TextBox>
                      <asp:Image ID="imgCalFechaIniI" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                      Style="cursor: pointer" />    
                      <asp:CompareValidator ID="cvFiltroFechaIniI" runat="server" Text="*" ErrorMessage="Fecha no válida" ControlToValidate="txtFiltroFechaIniI" Operator="DataTypeCheck" Type="date"></asp:CompareValidator>                                                                                              
                      </asp:Panel>
                    </td>                    
                    <td nowrap valign="top" style="width: 158px" style="display: <%= verVisibilidad("FiltroFechas2") %>">
                      <asp:panel id="pnlFiltroFechas2" runat="server">
                         Hasta: 
                      <asp:TextBox ID="txtFiltroFechaFinI" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                      <asp:CompareValidator ID="cvtxtFiltroFechaFinI" runat="server" Text="*" ErrorMessage="Fecha no válida" ControlToValidate="txtFiltroFechaFinI" Operator=DataTypeCheck Type=date></asp:CompareValidator>
                      <asp:Image ID="imgCalFechaFinI" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                      Style="cursor: pointer" />         
                      </asp:panel>                             
                    </td>                                                                                                  
                    <td style="display: <%= verVisibilidad("FiltroFechas3") %>">
                    <asp:Panel ID="pnlFiltroFechas3" runat="server">
                      Intervalo:                               
                    <asp:DropDownList ID="ddlIntervaloI" runat="server" AutoPostBack="false" Style="font: 10px Verdana; text-align: right" Visible="true">
                        <asp:ListItem Text="Diario" Value="d" Selected=True></asp:ListItem>
                        <asp:ListItem Text="Mensual" Value="m"></asp:ListItem>
                        <asp:ListItem Text="Año" Value="a"></asp:ListItem>
                    </asp:DropDownList>
                    </asp:Panel>
                    </td>
                    <td style="display: <%= verVisibilidad("FiltroFechas4") %>">
                    <asp:Panel ID="pnlFiltroFechas4" runat="server">
                      Intervalo:                               
                    <asp:DropDownList ID="ddlIntervaloD" runat="server" AutoPostBack="false" Style="font: 10px Verdana; text-align: right" Visible="true">
                        <asp:ListItem Text="Mensual" Value="m"></asp:ListItem>
                        <asp:ListItem Text="Año" Value="a"></asp:ListItem>
                    </asp:DropDownList>
                    </asp:Panel>                    
                    </td>
                    <td>
                    <asp:Button ID="btnCaudalesFiltradosI" runat="server" text="Ejecutar Informe" CssClass="boton"  Visible="true"  /></td></tr>
                </table>
                </td>
            </tr>
            <tr></tr>
            <asp:HiddenField runat="server" ID="txtTieneLecturas" Value="0"/>
            </table>
    
    </table>

       
    </form>
    </body>
</html>
