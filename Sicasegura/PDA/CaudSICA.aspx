<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaudSICA.aspx.cs" Inherits="PDA_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GAM - Datos Instantáneos</title>
    <meta http-equiv="Page-Exit" content="revealTrans(Duration=1,Transition=7)" />
	<meta name="viewport" content="width=320,user-scalable=YES" />
    <link rel="SHORTCUT ICON" href="GAM.ico"/>
    
    
</head>
<body style="margin:0px;background-image:url('images/background.jpg');Background-repeat:repeat-y; width:320px;">
    <form id="form1" runat="server" style="width: 320px">
    <div id="Div1" style="">
	    <div id="Div2" style="top:0px;position:absolute;width:320px;">
		    <img src="images/header.jpg" alt="" />
	    </div>
	    <div id="Div3" style="top:15px;position:absolute;width:320px">
		    <center><font face="TAHOMA" size="3" color="white"><b>GAM</b></font></center>
	    </div>
	    <div id="Div4" style="top:65px;position:absolute;width:320px">
		    <center><font face="verdana" size="3" color="Black"><b>Datos seleccionados</b></font></center>
	    </div>
	    <div id="Div5" style="top:0px; left: 50px; position: absolute; width:270px;">
		    <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="Images/charts.png" 
                ImageAlign="Right" onclick="ImageButton1_Click" Visible="False" />
 	   </div>
 	   <div id="Div6" style="top:15px; left: 0px; position: absolute; width:50px;">
		    <asp:ImageButton ID="ImageButton2" runat="server"  ImageUrl="Images/flechaatras.png" 
                ImageAlign="Right" onclick="ImageButton2_Click" />
 	   </div>
	</div>
	<br /><br /><br /><br />
    
    <br /><br /><br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DataSourceConnectionString %>" 
        SelectCommand="SELECT   PVYCR_AccesosTopkapi.Denominacion as Cauce, 
                                PVYCR_DatosAcequias_NIVUS.Fecha AS Fecha, 
                                PVYCR_DatosAcequias_NIVUS.Calado_M AS [Calado (m)], 
                                PVYCR_DatosAcequias_NIVUS.Caudal_M3S AS [Caudal (m3/s)], 
                                PVYCR_DatosAcequias_NIVUS.Velocidad_MS AS [Veloc. (m/s)]
                        FROM    PVYCR_DatosAcequias_NIVUS 
                                INNER JOIN PVYCR_AccesosTopkapi ON
                                PVYCR_DatosAcequias_NIVUS.CodigoPVYCR 
                                COLLATE Modern_Spanish_CI_AS = PVYCR_AccesosTopkapi.CodigoPVYCR
                        WHERE   PVYCR_DatosAcequias_NIVUS.Fecha = (SELECT MAX(d1.Fecha)
                                                                   FROM PVYCR_DatosAcequias_NIVUS d1
                                                                   WHERE PVYCR_DatosAcequias_NIVUS.Codigopvycr =d1.CodigoPVYCR)
                        ORDER BY fecha DESC">
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
        BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" DataSourceID="SqlDataSource1" Font-Bold="False" 
        Font-Size="7pt" Width="320px">
        <RowStyle BackColor="White" Font-Size="9px" ForeColor="#003399" Font-Names="Verdana" 
            HorizontalAlign="Center" />
        <Columns>
            <asp:CommandField ButtonType="Image" SelectImageUrl="Images/Select.png" 
                SelectText="Seleccionar" ShowSelectButton="True"  Visible="false"/>
        </Columns>
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <HeaderStyle BackColor="#003399" Font-Bold="False" ForeColor="#CCCCFF" 
            Font-Size="8pt" Font-Names="Verdana" />
    </asp:GridView>
    </form>
</body>
</html>
