<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformacionGeneralP.aspx.vb" Inherits="SICAH_InformacionGeneralP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" /> 
    <script language="javascript" src="..\js/utilesMenus.js"></script>  
    <script type="text/javascript" src="../aspnet_client/OpenFlashChart/js/swfobject.js"></script>
</head>
<body style=" background-color:White">
    <form id="form1" runat="server">
        <table width="100%" cellspacing="0" align="center" style="border: 0px solid #666666;background-color: white">
			<tr>
				<td>
					<table cellspacing="0">
						<tr>
							<td>
								<asp:Label ID="lblCabecera" runat="server"></asp:Label>
								<asp:Label ID="lblPestanyas" runat="server"></asp:Label>
							</td>
                        </tr>
                            <!-- Celda Menú - Contenido -->
                        <tr>
                            <td style="" visible="false">
                                <table>
                                    <tr>
                                        <td visible="false" >
                                            <strong>
                                                <asp:TextBox ID="txtCodigoPVYCR" runat="server" CssClass="texto" ToolTip="CodigoPVYCR"
                                                        Width="115px" Visible="false">[CodigoPVYCR]</asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                        ControlToValidate="txtCodigoPVYCR" Display="Dynamic" 
                                                        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                            </strong>
                                            <asp:TextBox ID="txtIdElementoMedida" runat="server" CssClass="texto" Width="27px" 
                                                    ToolTip="Id Elemento Medida" Visible="false">[EM]</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ErrorMessage="RequiredFieldValidator" ControlToValidate="txtIdElementoMedida" 
                                                    Display="Dynamic">*</asp:RequiredFieldValidator>&nbsp;
                                            <asp:TextBox ID="txtDescripcionElementoMedida" runat="server" CssClass="texto" Visible="False" Width="44px">
                                            </asp:TextBox>
                                            <asp:Label ID="LBLinfo" runat="server" Width="0px" Visible="false"></asp:Label>
                                            <b>
                                                <asp:LinkButton ID="lbtAceptar" runat="server" Text="Búsqueda Rápida" CssClass="enlaceLecturas" Visible="false"></asp:LinkButton>
                                            </b>
                                            <asp:ImageButton ID="imgVisor" runat="server" ImageUrl="~/SICAH/images/iconos/imgVisor.gif"
                                                    ImageAlign="TextTop" Visible="false"/>
                                        </td>    
                                    </tr>
                                </table>
                            </td>
                         </tr>
                        <tr>
<!-- Celda Contenido -->
                            <td style="padding-left:2px; padding-right:2px;" valign="top">
<!-- Panel listar Puntos -->                      
                                <asp:Panel ID="pnlEDPuntos" Runat="server" BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Width="100%">
                                <table cellspacing="0" cellpadding="1" >
                                    <tr>
                                        <td style="width:100%;">
                                            <table cellspacing="2" cellpadding="2" id="TABLE1" onclick="return TABLE1_onclick()" style="width:100%  ;">
                                                <tr>
                                                    <td style="height: 1px; vertical-align: middle; text-align: left; background-color: gainsboro;">
                                                        <strong>
                                                            <span style="color: #003399">Código SICA</span>
                                                        </strong>
                                                    </td>
                                                    <td style="width: 1px; height: 1px; vertical-align: middle; text-align: left; background-color: gainsboro;">
                                                        <asp:TextBox ID="txtcodigoPunto" runat="server" CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td style="height: 1px; vertical-align: middle; text-align: right; background-color: gainsboro;">
                                                        <span style="color: #003399">Código Cauce</span>
                                                    </td>
                                                    <td style="width: 86px; height: 1px; vertical-align: middle; text-align: left; background-color: gainsboro;">
                                                        <asp:TextBox ID="txtcodigocauce" runat="server" CssClass="texto" Height="13px" Width="121px" ReadOnly="true"></asp:TextBox>
                                                    </td>                             
                                                    <td colspan="2" style="vertical-align: middle; width: 158px; height: 1px; background-color: gainsboro; text-align: left">
                                                        <asp:Label ID="lblDesCodigoCauce" runat="server" BackColor="#E0E0E0" Width="170%" ReadOnly="true"></asp:Label>
                                                    </td>                                 
                                                </tr>   
                                                <tr>
                                                    <td style="height: 1px; vertical-align: middle; text-align: right;">
                                                        Denominación
                                                    </td>
                                                    <td style="height: 1px; vertical-align: middle; width: 142px; text-align: left;" align="center" colspan="5">
                                                        <asp:TextBox style="width:100%;" ID="txtdenominacion" CssClass="texto" runat="server" Height="13px" MaxLength="255" ReadOnly="true"></asp:TextBox>
                                                    </td>  
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        Tipo Punto
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:DropDownList ID="ddlTipoPunto" runat="server" AutoPostBack="True" Font-Size="8pt" Width="136px" Enabled="false">
                                                            <asp:ListItem Value="0">[Seleccionar]</asp:ListItem>
                                                            <asp:ListItem Value="G">Acequia</asp:ListItem>
                                                            <asp:ListItem Value="M">Motor</asp:ListItem>
                                                        </asp:DropDownList>                                        
                                                    </td>
                                                    <td style="height: 1px; vertical-align: middle; text-align: right;">
                                                        Tipo Sensor
                                                    </td>
                                                    <td style="width: 1px; height: 1px; vertical-align: middle; text-align: left;">
                                                        <asp:TextBox ID="txtsensor" runat="server" CssClass="texto" Width="134px" Height="13px" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        Código DataLogger
                                                    </td>                              
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtDatalogger runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>                               
                                                    </td> 
                                                </tr>
                                                <tr>                                    
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        Usado en parte oficial
                                                    </td>
                                                    <td style="vertical-align: middle; width: 1px; text-align: left; height: 1px;">
                                                        <asp:CheckBox ID="chkUso" runat="server" CssClass="texto" Enabled="false"/>
                                                    </td> 
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                       % Regable
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:textbox ID=txtRegable runat=server CssClass=texto Height="13px" Width="134px" ReadOnly="true"/>
                                                    </td>       
                                                </tr>
                                                <tr>
                                                    <td class="encabListado" colspan="6" style=" width:360px; text-align: left; height: 21px;">
                                                        &nbsp;
                                                    </td>                                   
                                                </tr>                              
                                                <tr>                                                                                            
                                                    <td style="height: 1px; vertical-align: middle; text-align: right;">
                                                        X
                                                    </td>
                                                    <td style="height: 1px; vertical-align: middle; width: 1px; text-align: left;">
                                                        <asp:TextBox ID=txtX runat=server CssClass=texto Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>                                    
                                                        <asp:CompareValidator ID="ComCotaToma" runat="server" ControlToValidate="txtX" Display="Dynamic"
                                                            ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer"
                                                            Width="8px"></asp:CompareValidator>
                                                    </td>                                        
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        Y
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                       <asp:textbox ID=txtY runat=server CssClass=texto Height="13px" Width="134px" ReadOnly="true"></asp:textbox>
                                                       <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtY"
                                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                                            Type="Integer" Width="8px"></asp:CompareValidator>
                                                    </td>  
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        Longitud
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtLongitud runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                    </td>                                            
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle;  text-align: right; height: 1px;">
                                                        PKS
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtPKS runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td style=" vertical-align: middle; text-align: right; height: 1px;">
                                                        PKA
                                                    </td>
                                                    <td style="width: 86px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtPKA runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtPKA"
                                                            ErrorMessage="?" ValidationExpression="\d+([\.\,]\d+){0,1}" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>                                                                                   
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        RIO
                                                    </td>
                                                    <td style="vertical-align: middle; width: 102px; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtRIO runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 21px; ">
                                                    </td>                                   
                                                </tr>  
                                                <tr>
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        A1_M
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtA1_M runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        A2_M
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtA2_M runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                            ControlToValidate="txtA2_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" 
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        A3_M
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtA3_M runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                            ControlToValidate="txtA3_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" 
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>                                  
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        B1_M
                                                    </td>
                                                    <td style="width: 86px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtB1_M runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                            ControlToValidate="txtB1_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" 
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>                                        
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        B2_M
                                                    </td>
                                                    <td style="width: 102px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtB2_M runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                                            ControlToValidate="txtB2_M" ErrorMessage="?" 
                                                            ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator>
                                                    </td>                                                                                                                                                                 
                                                    <td style="vertical-align: middle;  text-align: right; height: 1px;">
                                                        B3_M
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtB3_M runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                                            ControlToValidate="txtB3_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" 
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>                                            
                                                </tr>      
                                                <tr>  
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        B4_M
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtB4_M runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                            ControlToValidate="txtB4_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" 
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>                                                                                
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        H12_M
                                                    </td>                                        
                                                    <td style="width: 86px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtH12_M runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                                            ControlToValidate="txtH12_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" 
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>                                    
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        H23_M
                                                    </td>
                                                    <td style="width: 102px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtH23_M runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                                            ControlToValidate="txtH23_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" 
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                     </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        H34_M
                                                    </td>
                                                    <td style="width: 102px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtH34_M runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                            ControlToValidate="txtH34_M" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" 
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>                                
                                                    <td style=" vertical-align: middle; text-align: right; height: 1px;">
                                                        Diámetro (mm)
                                                    </td>                              
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtDiametro runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox> 
                                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtDiametro"
                                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                                            Type="Integer" Width="8px"></asp:CompareValidator>
                                                    </td> 
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        Offset_M
                                                    </td>
                                                    <td style="width: 86px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox ID=txtOffset runat=server CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtOffset"
                                                            ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td> 
                                                </tr>       
                                                <tr>
                                                    <td   class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 21px; ">
                                                    </td>                                   
                                                </tr>  
                                                <tr>
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;"> 
                                                        Longitud Flotador
                                                    </td>
                                                    <td style="width: 1px; vertical-align: middle; text-align: left; height: 1px;">
                                                        <asp:TextBox CssClass="texto" ID="txtFlotador" runat="server" Height="13px" Width="134px" 
                                                            ReadOnly="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                            ControlToValidate="txtFlotador" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td style="vertical-align: middle; text-align: right; height: 1px;">
                                                        Factor Flotador
                                                    </td>
                                                    <td style="vertical-align: middle; width: 86px; text-align: left; height: 1px;">
                                                        <asp:textbox ID="txtFactor" runat="server" CssClass="texto" Height="13px" Width="134px" ReadOnly="true"></asp:textbox> 
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                                                            ControlToValidate="txtFactor" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" 
                                                            Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </td> 
                                                </tr>   
                                                <tr>
                                                    <td class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 21px; ">
                                                    </td>                                   
                                                </tr>  
                                                <asp:Panel ID="pnlNOVerGraficoAcequias" runat="server" Visible="false">
                                                <tr>
                                                    <td style="height: 1px; width: 3844px; vertical-align: middle; text-align: right;">
                                                        Acceso
                                                    </td>
                                                    <td style="width: 1px; height: 1px; vertical-align: middle; text-align: left;" colspan="5">
                                                <asp:TextBox ID=txtAccesoGrafico runat=server CssClass=texto TextMode="MultiLine" Width="780px" Height="80px"></asp:TextBox>
                                                    </td>  
                                                </tr>
                                                <tr>
                                                    <td   class="encabListado" colspan="6" style=" width: 360px; text-align: left; height: 21px; ">
                                                    </td>                                   
                                                </tr>                          
                                                <tr>                                    
                                                    <td style="vertical-align: middle; width: 3844px; text-align: right; height: 1px;">Observaciones</td><td style="width: 360px; vertical-align: middle; text-align: left; height: 1px;" colspan="5">
                                                        <asp:TextBox ID=txtObservacionesGrafico runat=server TextMode="MultiLine" CssClass="texto" Width="780px" Height="80px"></asp:TextBox>
                                                    </td>
                                                </tr>   
                                                </asp:Panel>
                                                <asp:Panel ID="pnlVerGraficoAcequias" runat="server" Visible="false">
                                                <tr >
                                                    <td colspan="4">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="height: 1px;vertical-align: middle; text-align: right;">
                                                                    Acceso
                                                                </td>
                                                                <td style="vertical-align: middle; text-align: left; width:1px;" colspan="3">
                                                                    <asp:TextBox ID=txtacceso runat=server CssClass=texto TextMode="MultiLine" Height="80px" Width="450px"></asp:TextBox>
                                                                </td>  
                                                            </tr>
                                                            <tr>
                                                                <td   class="encabListado" style="text-align: left; height: 21px;" colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: middle; text-align: right;">
                                                                    Observaciones
                                                                </td>
                                                                <td style="vertical-align: middle; text-align: left; ">
                                                                    <asp:TextBox ID=txtobservaciones runat=server TextMode="MultiLine" CssClass="texto" Height="80px" Width="450px">
                                                                    </asp:TextBox>
                                                                </td>                                    
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td colspan="2" >
                                                        <table>
                                                            <tr>
                                                                <td width="50px">                                  
                                                                    <div id="my_chart" >
                                                                        <script type="text/javascript">
                                                                            swfobject.embedSWF("../aspnet_client/OpenFlashChart/open-flash-chart.swf", "my_chart", "320", "200",  "9.0.0", "expressInstall.swf",  {"data-file":"Perfil_acequiaFlashData.aspx"}  );
                                                                        </script>
                                                                    </div>  
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>                                
                                                </asp:Panel>                     
                                            </table>           
                                        </td>
                                    </tr>                                    
                                    <tr>     <td>&nbsp</td>     </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnImprimir" Runat="server" cssclass="boton" Text='Imprimir' Visible="true" ></asp:Button>
                                        </td>
                                    </tr>                            
                                </table>
                                </asp:Panel>      
<!-- Fin Panel listar elementos -->   
                            </td>
<!-- Fin Celda Contenido -->        
                       </tr> 
<!-- Fin Celda Menú - Contenido -->
                    </table>
                </td>
            </tr>
        </table>         
    </form>
    </body>
</html>
