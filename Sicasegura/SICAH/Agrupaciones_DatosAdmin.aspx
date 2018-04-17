<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones_DatosAdmin.aspx.vb" Inherits="SICAH_Agrupaciones_DatosAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    
    <style>
        body{font-family:Tahoma; font-size:8pt;}
        .titulosec {font:15px Trebuchet MS,Georgia,Tahoma;  color:#222266;}
        .textoNumerico, .displayClave, .texto{font-family:Tahoma; font-size:8pt;}
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <table cellspacing="0" cellpadding="0" width="100%" style="background-color:#3344dd;">
        <tr>
            <td align="left" valign="middle"  class="titulosec">
                <B>
                    <font color="white">REGISTRO DE AGUAS : Datos administrativos</font> 
                </B>
            </td>
            <td align="right" valign="middle" style=" text-decoration:none; font:15px Trebuchet MS,Georgia,Tahoma;">
                <asp:Label ID="LBL_DescargaHoja" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>    
            <td colspan="2">
                <table width="100%" cellspacing="0"  style="border:solid 1px black; background-color:#99aaff;">
                    <tr>
                        <td align="right" style="width:16%;" >
                            Sección/Tomo/hoja</td>
                        <td nowrap  style="width:16%;"> 
                            <asp:Label ID="txtSec" runat="server" CssClass="displayClave" Font-Bold="true" Font-Size="12px" ReadOnly="true" />
                            &nbsp;/
                            <asp:Label ID="txtTomo" runat="server" CssClass="displayClave" Font-Bold="true" Font-Size="12px" ReadOnly="true" />
                            &nbsp;/&nbsp;
                            <asp:Label ID="txtHoja" runat="server" CssClass="displayClave" Font-Bold="true" Font-Size="12px" ReadOnly="true" />
                        </td>
                        <td align="right"  style="width:16%;">
                            </td>
                        <td  style="width:16%;">
                            &nbsp;</td>
                        <td align="right"  style="width:16%;">
                            </td>
                        <td  style="width:16%;">
                            &nbsp;</td>
                    </tr> 
                    <tr>                                              
                        <td >
                            Acuífero
                        </td>
                        <td>
                            <asp:TextBox ID="txtACUIF" runat="server" CssClass="texto"
                             ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Fecha Inscripción
                        </td>
                        <td>
                            <asp:TextBox ID="txtFECINS" runat="server" CssClass="texto"
                             ReadOnly=true></asp:TextBox>
                        </td>
                        <td>
                            Firmado
                        </td>
                        <td>
                             <asp:TextBox ID="txtFIRMADO" runat="server" CssClass="texto"
                              ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        
                        <td>
                            Volumen Regadío(m3)</td>
                         <td>
                             <asp:TextBox ID="txtVOLRE" runat="server"  CssClass="textoNumerico"
                              ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Regadío(m3/ud/año)</td>
                        <td>
                            <asp:TextBox ID="txtVOL1RE" runat="server" CssClass="textoNumerico" 
                             ReadOnly="true"></asp:TextBox>
                        </td>
                         <td>
                            Fecha Certificación
                        </td>
                        <td>
                             <asp:TextBox ID="txtFECCER" runat="server" CssClass="texto"
                              ReadOnly="true"></asp:TextBox>
                        </td>
                    
                    </tr>
                    <tr>  
                        <td>
                            Volumen Industrial(m3)</td>
                         <td>
                             <asp:TextBox ID="txtVOLIN" runat="server" CssClass="textoNumerico" 
                              ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Industrial(m3/ud/año)</td>
                        <td>
                            <asp:TextBox ID="txtVOL1IN" runat="server" CssClass="textoNumerico"
                             ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Desnivel(m)
                         </td>
                         <td>
                             <asp:TextBox ID="txtDESN" runat="server" CssClass="textoNumerico"
                              ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Volumen Abastecimiento(m3)</td>
                        <td>
                             <asp:TextBox ID="txtVOLAB" runat="server"  CssClass="textoNumerico"
                              ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Abastecimiento(m3/ud/año)</td>
                        <td>
                            <asp:TextBox ID="txtVOL1AB" runat="server" CssClass="textoNumerico"
                             ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Salto(Cv/Kw)
                        </td>
                        <td>
                             <asp:TextBox ID="txtSALTO" runat="server" CssClass="textoNumerico"
                              ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Volumen Ganadero(m3)</td>
                         <td>
                             <asp:TextBox ID="txtVOLGA" runat="server" CssClass="textoNumerico" 
                              ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Ganadero(m3/ud/año)
                        </td>
                        <td>
                            <asp:TextBox ID="txtVOL1GA" runat="server" CssClass="textoNumerico"
                             ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Potencia
                         </td>
                         <td>
                             <asp:TextBox ID="txtPOTEN" runat="server" CssClass="textoNumerico"
                              ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Volumen Otros(m3)
                        </td>
                        <td>
                             <asp:TextBox ID="txtVOLOT" runat="server" CssClass="textoNumerico"
                              ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Otros(m3/ud/año)
                        </td>
                        <td>
                            <asp:TextBox ID="txtVOL1OT" runat="server" CssClass="textoNumerico"
                             ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Superficie(Ha)
                        </td>
                        <td>
                            <asp:TextBox ID="txtSUPER" runat="server" CssClass="textoNumerico"
                             ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Caudal Máximo Instantáneo(l/s)
                        </td>
                        <td>
                            <asp:TextBox ID="txtCAUDAL1" runat="server" CssClass="textoNumerico"
                             ReadOnly="true"></asp:TextBox>
                        </td>
                        <td>
                            Caudal Medio(l/s)
                        </td>
                        <td>
                            <asp:TextBox ID="txtCAUDALME" runat="server" CssClass="textoNumerico"
                             ReadOnly="true"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            Título
                        </td>
                        <td colspan="6">
                            <asp:TextBox ID="txtTITULO" runat="server" CssClass="texto" 
                                TextMode="multiLine" Width="98%" ReadOnly="true" Height="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Condiciones Específicas</td>
                        <td colspan="6">
                            <asp:TextBox ID="txtCESP" runat="server" CssClass="texto" 
                                TextMode="multiLine" Width="98%" ReadOnly="true" Height="60"></asp:TextBox>
                        </td>
                    </tr>                   
                </table>        
            </td>
        </tr>
    </table>   
    </form>
</body>
</html>
