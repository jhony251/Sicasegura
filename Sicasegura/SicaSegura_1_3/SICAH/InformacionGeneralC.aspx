<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformacionGeneralC.aspx.vb" Inherits="SICAH_InformacionGeneralC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />    
 <script language="javascript" src="..\js/utilesMenus.js"></script> 
 <script language="javascript" type="text/javascript">
 
 function abrirfichero(fichero) { 
    alert(fichero);
    window.open(fichero);
    
}

</script>
  
</head>
<body style="width: 900px; background-color:White; height:640px">
    <form id="form1" runat="server">
       <table width="100%" cellspacing="0" align="center" style="border: 0px solid #666666;background-color: white">
       <tr>
        <td style="width: 900px">
           <table cellspacing="0" width="100%">
           <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label><!-- Celda Menú - Contenido --><tr>
              <td style="width: 900px">
                 <table width="100%">
                     <tr>
                     <asp:Label ID="lblPestanyasArbol" runat="server" ></asp:Label><td width=0% visible="false" >
                           <strong>
                               <asp:TextBox ID="txtCodigoPVYCR" runat="server" CssClass="texto" ToolTip="CodigoPVYCR"
                                   Width="115px" Visible="false">[CodigoPVYCR]</asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCodigoPVYCR"
                                   Display="Dynamic" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator></strong>
                                   <asp:TextBox ID="txtIdElementoMedida" runat="server" CssClass="texto" Width="27px" ToolTip="Id Elemento Medida" Visible="false">[EM]</asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtIdElementoMedida" Display="Dynamic">*</asp:RequiredFieldValidator>&nbsp;
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
             <!-- Fin celda1 contenido arbol-->            
                <tr>         
                    <!-- Celda Contenido -->
                    <td style="padding-left:2px; padding-right:2px; width:895px" valign=top>
                    <!-- Panel Editar  Cauces-->
                        <asp:Panel ID=pnlEDCauces Runat=server BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Height="690px">
                            <table width="98%" cellspacing=0 cellpadding=1 class="tablaEdicion" >
                            <tr>
                            <td >
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td  style="height: 1px;  text-align: right; background-color: #666699;">
                                        <strong><span style="color: white;text-align: right;">Código Cauce</span></strong></td>
                                    <td style="height: 1px;  text-align: left;" colspan="5"><asp:TextBox ID=txtcodigoCauce runat=server CssClass=texto Height="13px" MaxLength="20" ReadOnly=true></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcodigoCauce" ErrorMessage="El código es obligado"></asp:RequiredFieldValidator></td>
                                  
                                </tr>   
                                <tr >
                                    <td style="height: 1px; text-align: right; ">
                                        Denominación</td>
                                    <td style="height: 1px; text-align: left;" align="center" colspan="5">
                                       <asp:TextBox ID=txtdenominacion CssClass=texto runat=server Height="13px" MaxLength="255" Width="690px" ReadOnly=true></asp:TextBox>
                                    </td>  
                                </tr>
                                  
                                <tr>
                                    <td style="text-align: right;">
                                        Cód.Inventario</td>
                                    <td style="text-align: left;" nowrap><asp:TextBox ID=txtcodinventario runat=server CssClass=texto  Height="13px" MaxLength="15" ReadOnly=true Width="115"></asp:TextBox> <asp:ImageButton ID="imgPDF" runat="server" visible="false" ImageUrl="~/SICAH/images/iconos/icoPestBandeja.gif" /> <asp:LinkButton  ID="lbnPDF" Text="Ficha Aprov." runat="Server" Visible="false" /></td>
                                    <td style="text-align: right;" nowrap>
                                        Cód. Campo</td>
                                    <td style="text-align: left;"><asp:TextBox ID=txtcodcampo runat=server CssClass=texto  Height="13px" MaxLength="15" ReadOnly=true></asp:TextBox>
                                        </td>                                                          
                                    <td style="text-align: right;">
                                        Tipo Cauce</td>
                                    <td style="text-align: left;"><asp:TextBox ID=txtTipoToma runat=server CssClass=texto Height="13px" MaxLength="1" Width="16px" ReadOnly=true></asp:TextBox></td>                                        
                                </tr>                
                                <tr>
                                    <td style="text-align: right; height: 1px;">
                                        Margen Derivación</td>
                                    <td style="text-align: left; height: 1px;"><asp:textbox ID=txtMargenDeriv runat=server CssClass=texto Height="13px" Width="16px" MaxLength="1" ReadOnly=true></asp:textbox>
                                        </td>
                                    <td style="text-align: right; height: 1px;">
                                        Provincia</td>
                                    <td style="text-align: left; height: 1px;"><asp:textbox ID=txtProvincia runat=server CssClass=texto Height="13px"  MaxLength="20" ReadOnly=true/>
                                        </td>                              
                                    <td style="text-align: right; height: 1px;">
                                        Municipio</td>
                                    <td style="text-align: left; height: 1px;"><asp:TextBox ID=txtMunicipio runat="server" CssClass=texto Height="13px"  MaxLength="100" ReadOnly=true></asp:TextBox>
                                       </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 23px;">
                                        Otras Referencias</td>
                                    <td style="text-align: left; height: 23px;" colspan="5"><asp:textbox ID=txtOtrasRef runat=server CssClass=texto Height="13px" MaxLength="255" Width="690px" ReadOnly=true></asp:textbox>
                                        </td>
                                </tr>                                
                                <tr>
                                    <td style="text-align: right; height: 1px;">
                                        Paraje<br />
                                    </td>
                                    <td style="text-align: left; height: 1px;" colspan="5">
                                        <asp:textbox ID="txtParaje" runat="server" CssClass="texto" Height="13px"  MaxLength="255" Width="690px" ReadOnly=true></asp:textbox></td>
                                </tr>      
                                
                                <tr>
                                    <td style="height: 1px;  text-align: right;">
                                        Administrador</td>
                                    <td style="height: 1px; text-align: left;" align="center" colspan="5">
                                       <asp:TextBox ID=txtAdministrador CssClass=texto runat=server  Height="13px" MaxLength="255" Width="690px" ReadOnly=true></asp:TextBox>
                                    </td>  
                                </tr>
                                <tr>
                                    <td style="height: 1px;  text-align: right;">
                                        Caudal Máx.(l/seg)</td>
                                    <td style="height: 1px;  text-align: left;"><asp:TextBox ID=txtCaudal runat=server CssClass=texto  Height="13px" ReadOnly=true></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtCaudal"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>
                                    <td style="text-align: right;">
                                        Volumen Máx.Anual Legal (m3)</td>
                                    <td style="height: 1px;  text-align: left;"><asp:TextBox ID=txtVolumenLegal runat=server CssClass=texto  Height="13px" ReadOnly=true></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtVolumenLegal"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>                                                          
                                    <td style="text-align: right;">
                                        Volumen Máx Anual Teórico (m3)</td>
                                    <td style="height: 1px;text-align: left;"><asp:TextBox ID=txtVolumenAnual runat=server CssClass=texto Height="13px" ReadOnly=true></asp:TextBox>                                    
                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtVolumenAnual"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>                                        
                                </tr>                
                               <tr>
                                    <td style="height: 1px;  text-align: right;">
                                        X (toma)</td>
                                    <td style="height: 1px;  text-align: left;"><asp:TextBox ID=txtX runat=server CssClass=texto  Height="13px" ReadOnly=true></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtX"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>
                                    <td style="text-align: right;">
                                        Y (toma)</td>
                                    <td style="height: 1px;  text-align: left;"><asp:TextBox ID=txtY runat=server CssClass=texto  Height="13px" ReadOnly=true></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="txtY"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>                                                          
                                    <td style="text-align: right;">
                                        Cota Toma</td>
                                    <td style="height: 1px;text-align: left;"><asp:TextBox ID=txtCota runat=server CssClass=texto Height="13px" ReadOnly=true ></asp:TextBox>                                    
                                        <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtCota"
                                            Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?"
                                            Type="Integer" Width="8px"></asp:CompareValidator></td>                                        
                                </tr>                
                               <tr>
                                    <td style="height: 1px;  text-align: right;">
                                        Superficie Real Aproximada (has)</td>
                                    <td style="height: 1px;  text-align: left;"><asp:TextBox ID=txtSupReal runat=server CssClass=texto  Height="13px" ReadOnly=true></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                            ControlToValidate="txtSupReal" ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>
                                    <td style="text-align: right;">
                                        Superficie Inscrita (has)</td>
                                    <td style="width: 4px; height: 1px;  text-align: left;"><asp:TextBox ID=txtSupInscrita runat=server CssClass=texto  Height="13px" ReadOnly=true></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSupInscrita"
                                            ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>                                                          
                                    <td style="text-align: right;">
                                        % Tradicional</td>
                                    <td style="height: 1px;text-align: left;"><asp:TextBox ID=txtTradicional runat=server CssClass=texto Height="13px" ReadOnly=true></asp:TextBox>                                    
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTradicional"
                                            ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>                                        
                                </tr>         
                                <tr>
                                    <td style="height: 1px;  text-align: right;">
                                        Tipo Cultivo</td>
                                    <td style="height: 1px;  text-align: left;"><asp:TextBox ID=txtTipoCultivo runat=server CssClass=texto  Height="13px" MaxLength="100" ReadOnly=true></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        Longitud Cauce (km)</td>
                                    <td style="height: 1px;  text-align: left;"><asp:TextBox ID=txtLongitudCauce runat=server CssClass=texto  Height="13px" ReadOnly=true></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtLongitudCauce"
                                            ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>                                                          
                                    <td style="text-align: right;">
                                        EntreOjos y Contraparada</td>
                                    <td style="height: 1px;text-align: left;">
                                        &nbsp;<asp:CheckBox ID="chkEntreOjos" runat="server" CssClass="texto" Enabled=false /></td>                                        
                                </tr>                
                               <tr>
                                <td style="height: 1px; text-align:right;">
                                        En Activo  
                                        </td>
                                    <td style="height: 1px;">                                                                     
                                        <asp:CheckBox ID="chkenActivo" runat="server" CssClass="texto" Enabled=false />
                                    Medido en PVYCR
                                        <asp:CheckBox ID="chkMedido" runat="server" CssClass="texto" Enabled=false />
                                    </td>
                                    <td style="height: 1px; text-align:right;" colspan=2>
                                        Titular Contactado
                                        <asp:CheckBox ID="chkTitular" runat="server" CssClass="texto" Enabled=false />
                                        Contador OK<asp:CheckBox ID="chkContadorOK" runat="server" CssClass="texto" Enabled=false />
                                          </td> 
                                </tr>  
                                         
                               <tr>
                                    <td style="height: 1px;  text-align: right;">
                                        Observaciones</td>
                                    <td style="width: 7px; height: 1px;  text-align: left;" colspan="5"><asp:TextBox ID=txtObserva runat=server CssClass=texto  Height="13px" Width="690px" ReadOnly=true></asp:TextBox>
                                    </td>
                                </tr>                
              
       

                                <tr>
                                    <td   class="encabListado" colspan="6" style="text-align: left; height: 21px; ">                                        
                                        Datos Titular</td>
                                   
                                </tr>

                                <tr>
                                    <td style="height: 2px;  text-align: right;">
                                        Titular</td>
                                    <td style="height: 2px;text-align: left;" align="center" colspan="3">
                                       <asp:TextBox ID=txtTitular CssClass=texto runat=server  Height="13px" MaxLength="255" Width="440px" ReadOnly=true></asp:TextBox>
                                    </td>  
                                    <td style="height: 2px;text-align: right;">
                                        NIF</td>
                                    <td style="height: 2px;text-align: left;" align="center">
                                       <asp:TextBox ID=txtNIF CssClass=texto runat=server  Height="13px" MaxLength="15" ReadOnly=true></asp:TextBox>
                                    </td>  

                                </tr>
                                <tr>
                                    <td style="height: 1px;  text-align: right;">
                                        Dirección</td>
                                    <td style="height: 1px; text-align: left;" align="center" colspan="5">
                                       <asp:TextBox ID=txtDireccion CssClass=texto runat=server  Height="13px" MaxLength="255" Width="690px" ReadOnly=true></asp:TextBox>
                                    </td>  
                                </tr>

                                
                                <tr>
                                    <td style="text-align: right; height: 1px;">
                                        Municipio</td>
                                    <td style=" text-align: left; height: 1px;" colspan=5>
                                              <asp:TextBox ID="txtMunicipioTit" runat="server" CssClass="texto" Height="13px"  MaxLength="100" ReadOnly=true></asp:TextBox>
                                    Provincia
                                    <asp:TextBox ID=txtProvinciaTit runat=server CssClass="texto" Height="13px" MaxLength="20" ReadOnly=true></asp:TextBox>                               
                                    CP
                                    <asp:TextBox ID="txtCP" runat="server" CssClass="texto" Height="13px" MaxLength="5" ReadOnly=true></asp:TextBox>
                                    Teléfono
                                    <asp:TextBox ID=txtTelefono runat=server CssClass="texto" Height="13px" MaxLength="50" ReadOnly=true></asp:TextBox>                                        
                                    </td>
                                </tr>
                               
                                <tr>
                                        <td   class="encabListado" colspan="6" style="text-align: left; height: 21px; ">
                                        Datos Registro de Aguas</td>                                   
                                </tr>                                                            
                                <tr>
                                    <td style="text-align: right;">Expedientes originales</td>
                                    <td><asp:TextBox ID=txtExpediente runat=server CssClass="texto" Height="13px" MaxLength="50" ReadOnly=true></asp:TextBox></td>
                                    <td colspan="4" rowspan="2" valign="top">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr style="padding-top:1px;">
                                            <td style="text-align: right;" width="80"> Nº Antiguo de aprovechamientos</td>
                                            <td><asp:TextBox CssClass="texto" ID="txtRegAntiguo" runat="server" ReadOnly=true Width="40"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtRegAntiguo" Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                            </td>
                                            <td style="text-align: right;">Expedientes actuales</td>
                                            <td style="text-align: left;"><asp:TextBox ID=txtExptesLibro runat=server CssClass="texto" Height="13px" MaxLength="50" ReadOnly=true></asp:TextBox></td>
                                            <td style="text-align: right;">Otros expedientes relacionados</td>                                     
                                            <td style="text-align: left;"><asp:TextBox ID=txtOtrosExpedientes runat=server CssClass="texto" Height="13px" MaxLength="50" ReadOnly=true Width="50"></asp:TextBox></td>
                                        </tr>
                                        <tr style="padding-top:2px;">   
                                            <td style="text-align: right">Inscripción</td>
                                            <td style="width: 7px;"><asp:TextBox id="txtInscripcion" runat="server" cssclass="texto" Width="40"></asp:TextBox></td>
                                            <td style="text-align: right">Inscripción relacionada</td>
                                            <td style="width: 7px;"><asp:TextBox id="txtInscripcionRelacionada" runat="server" cssclass="texto"></asp:TextBox></td>
                                            <td style="text-align: right;">Inscripción estado</td>
                                            <td style="text-align: left;" align="center"><asp:TextBox ID="txtInscripcionEstado"  CssClass="texto" runat="server" Width="50"></asp:TextBox></td>  
                                        </tr>
                                    </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">Sección </td>
                                    <td style="text-align: left">   
                                        <asp:TextBox ID="txtSeccion" runat="server" CssClass="texto" Height="13px"  Width="40px" MaxLength="1" ReadOnly=true></asp:TextBox>
                                        Tomo <asp:TextBox ID=txtTomo runat=server CssClass="texto" Height="13px" Width="32px" ReadOnly=true></asp:TextBox>                               
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtTomo" Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>                                            
                                        Folio <asp:TextBox ID="txtFolio" runat="server" CssClass="texto" Height="13px"  Width="32px" ReadOnly=true></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtFolio" Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                    </td>                                  
                                </tr>                                                               
                                <tr>
                                        <td   class="encabListado" colspan="6" style="text-align: left; height: 21px; ">    
                                        Datos Contacto</td>
                                   
                                </tr>


                                <tr>                                   
                                                                                                     
                                    <td style="text-align: right; height: 1px;">
                                        Contacto</td>
                                    <td style="text-align: left; height: 1px;" colspan="5"><asp:TextBox ID=txtContacto runat=server CssClass="texto" Height="13px"  MaxLength="255" Width="690px" ReadOnly=true></asp:TextBox>
                                        </td>
                                </tr>                                
                                                             
                                <tr>                                    
                                    <td style="text-align: right;">
                                        Dirección</td>
                                    <td style="text-align: left;" colspan="3" rowspan=""><asp:TextBox ID=txtDireccionCont runat=server CssClass="texto" Height="13px"  MaxLength="255" Width="440px" ReadOnly=true></asp:TextBox>
                                        </td>
                                    <td style="text-align: right;">
                                        Teléfono</td>
                                    <td style="text-align: left;"><asp:TextBox ID=txtTelContacto runat=server CssClass="texto"  Height="13px" MaxLength="50" ReadOnly=true></asp:TextBox></td>

                                </tr>   
                <tr>
                    <td style="text-align: right;" colspan=6><asp:Button ID="btnImprimir" Runat="server" cssclass="boton" Text='Imprimir'></asp:Button></td>
                </tr>
                     
                               </table>           
                            </td>
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                    <!-- Fin Panel Editar Cauces -->                   
                    <asp:Panel ID="pnlCarpetaPDF" Runat="server" BackColor="WhiteSmoke" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" Height="690px" Visible="False">    
                    <table id="tprinciapl" cellspacing="0" cellpadding="1" style="width:89%"  >
                        <tr>  
                                <caption>
                                    <br />
                                    <asp:Label ID="lblPDF" runat="server" CssClass="tituloSec"><b> Contenido de la carpeta:  </b></asp:Label>
                                    <br />
                                </caption>
                        </tr>
                        <tr>
                         <td align="right" style="border-top:1px solid #666666">
                         </td>
                        </tr>                         
                        <tr>
                              <td style="width: 889px" >
                               <asp:Label ID="lblHTML" runat="server"></asp:Label>               
                               
                              </td>
                        </tr>
                        <tr>
                        <td align="right" style="border-top:1px solid #666666">
                                <asp:Button ID="btnSalir" Runat="server" cssclass="boton" Text='Salir' CausesValidation=False></asp:Button>
                        </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    </td>
                    <!-- Fin Celda Contenido -->        
                </tr>
                <!-- Fin Celda Menú - Contenido -->
                   

        
        </table>
        </td>
       </tr>
       </table>         
    </form>
</body></html>
