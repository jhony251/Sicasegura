<%@ Page Language="VB" AutoEventWireup="false" CodeFile="infoPunto.aspx.vb" Inherits="PantallasVisor_infoElemento" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>PUNTOS DE CONTROL</title>
    <link rel=stylesheet href="../Styles.css">
    <script language="javascript">
        function redimensiona()
        {       
            window.resizeTo(document.getElementById('box').offsetWidth+80,(document.getElementById('box').offsetHeight+200));        
        }

    </script>
<base target="_self" />

</head>

<body onload="redimensiona();"  id="cuerpo" bgcolor="beige">
    <form id="form1" runat="server">
 
       <fieldset id="box" style="border:2px ridge #CCCC99;padding:8px; ">
        <legend align=center style="font-weight:bold; color:black; font-size:14px">Galería Fotográfica</legend>
        
        <table id="tablaCabecera" style="width: 100px; height: 132px;"> 
           <tr>
              <td style="width: 703px">
               <asp:Label ID="lblHTML" runat="server"></asp:Label>               
              </td>
           </tr>
        	
         </table>
        </fieldset>

    <fieldset id="Fieldset1" style="border:2px ridge #CCCC99;padding:8px; ">
        <legend align=center style="font-weight:bold; color:black; font-size:14px">Elementos de Medida</legend>
        <table id="Table1" tyle="width: 600px; height: 132px;"> 
            <tr>                                        
            <td class="encabListado">Código PVYCR</td>
            <td class="encabListado">Id Elemento</td>  
            <td class="encabListado">Tipo</td>   
            </tr> 
            <asp:Repeater ID="rptElementos" runat="server">                                                     
            <ItemTemplate>
                <tr <%# checkFila()%>>
                    <td><%#Container.DataItem("CodigoPVYCR")%></td>
                    <td><%#Container.DataItem("idElementoMedida")%></td>
                    <td><%#Container.DataItem("Tipo")%></td>                                            
                 </tr>
            </ItemTemplate>
            </asp:Repeater>        	
         </table>
    </fieldset><fieldset id="Fieldset2" style="border:2px ridge #CCCC99;padding:8px; ">
        <legend align=center style="font-weight:bold; color:black; font-size:14px">Lecturas</legend>
        <table id="Table2" tyle="width: 600px; height: 132px;">
            <tr>
                <td class="encabListado">
                    Código PVYCR</td>
                <td class="encabListado">
                    Id Elemento</td>
                <td class="encabListado">
                    Tipo</td>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr <%# checkFila()%>>
                        <td>
                            <%#Container.DataItem("CodigoPVYCR")%>
                        </td>
                        <td>
                            <%#Container.DataItem("idElementoMedida")%>
                        </td>
                        <td>
                            <%#Container.DataItem("Tipo")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </fieldset>
    <div>
        <asp:Panel ID="pnlGraficaConsumo" runat="server">
                <!--grafico -->
        <div id="my_chart1" style=" background-color:#C4BF7E">
        <asp:Label ID="lblTitulo" runat="server" style="color:White; font-weight:bold"></asp:Label>
       
        </div>
        <div id="my_chart" >
          <script type="text/javascript">
                swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "my_chart", "630", "450",  "9.0.0", "expressInstall.swf",  {"data-file":"CurvasAcequiasPreproduccionFlashData.aspx"}  );
        </script>
        </div>
        <!--fin de grafico -->
        </asp:Panel>
</div>

    </form>
</body>
</html>