<%@ Page Language="VB" AutoEventWireup="false" CodeFile="caucepunt.aspx.vb" Inherits="SICAH_caucepunt" %>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  <link rel="stylesheet" href="..\styles.css">
  
  <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script language="javascript" src="..\js/utilesListados.js"></script>
  <script language="javascript" src="..\js/utilesMenus.js"></script>  
  
  <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
  <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
  <meta name="vs_defaultClientScript" content="JavaScript">
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
</head>
<body onload="LoadEvent()" bgcolor="#EEEAD2">
  <form id="Form1" method="post" runat="server">
  <span id="dsp4"></span>
  <span id="imagepath" style="display:none">../js/calendar/images</span>
    <table cellspacing="2" align="center" width="100%" style="border: 1px solid #666666;
      background-color: white">
      <tr>
        <td>
          <table cellspacing="0" cellpadding="1" width="100%">
            <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label><tr>
              <td>
               <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <!-- Celda Menú - Contenido -->
                   <tr>
                       <td colspan="3" style="padding-right: 20px; padding-left: 20px; height: 9px" valign="top">
                           <asp:TextBox ID="txtCauce" runat="server" CssClass="texto" Text="" Width="339px"></asp:TextBox>
                           <asp:ImageButton ID="imgBuscarCauce" runat="server" BorderStyle="None" BorderWidth="0px"
                               ImageAlign="Top" ImageUrl="~/SICAH/images/iconos/icoBtnSistemasBuscar2.gif" ToolTip="Buscar en el arbol de cauces" /></td>
                   </tr>
                <tr>
                   <!-- Celda1 menú árbol-->
                    <td colspan="2" style="padding-right: 20px; padding-left: 20px; width: 301px" valign="top">
                      <!-- Panel arbol -->                      
                      <asp:Panel ID=pnlArbol Runat=server Visible=true Width=420px ScrollBars=Auto BorderStyle=Inset Height=650px >
                      <asp:TreeView ID="treeView1" runat="server">
                      <SelectedNodeStyle BackColor="PaleGoldenrod" Font-Bold="True" Font-Underline="True" />
                      </asp:TreeView>
                      </asp:Panel>
                    <!-- Fin Panel arbol-->
                    </td>
                  
                    <!-- Fin celda1 contenido arbol-->
                    <!-- Celda2 contenido-->
                    <td style="padding-left:20px; padding-right:20px;" valign=top >
                        <!-- Panel energia -->    
                        <asp:Panel ID=pnlEnergia Runat=server Visible=false Height=650px>
                            <table width=100%>
                            <asp:Label ID="lblidElemento" runat="server" Visible=false></asp:Label><tr>
                                <td colspan=14 align="right"><a>Filtrar por uno o varios de los siguientes campos</a></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666" colspan=14>
                                <table align="left" width=100%>
                                <tr>                                                       
                                   <td nowrap>Cod. Fuente Dato:
                                    <asp:TextBox ID="txtFiltrarCodFuenteDatoE" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                    </td>
                                    <td nowrap> Fecha Desde: 
                                      <asp:TextBox ID="txtFiltroFechaIniE" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                      <asp:CompareValidator ID=cvFIE runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaIniE Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaIniE" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      
                                    </td>
                                    <td nowrap> Hasta: 
                                      <asp:TextBox ID="txtFiltroFechaFinE" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                      <asp:CompareValidator ID=cvFFE runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaFinE Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaFinE" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      

                                    </td>
                                    <td nowrap> ¿Mostrar Lecturas Nulas?
                                      <asp:CheckBox ID="chkFiltroNulasE" runat="server" CssClass="texto" Checked=false/>
                                    </td>        
                                    <td colspan=12 nowrap>
                                    <asp:Button ID="btnFiltroAceptarE" runat="server" text="Filtrar" CssClass="boton" />
                                    <asp:Button ID="btnFiltroCancelarE" runat="server" Text="Limpiar" CssClass="boton" />
                                    <asp:Button ID="btnCancelarE" runat="server" Text="Cancelar" CssClass="boton" />
                                    </td>
                                
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label ID="lblFiltroNRegE" Text="Nº Reg. Mostrados:" CssClass="texto" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtFiltroNRegE" runat="server" CssClass="texto" Width=50px></asp:TextBox>
                                    
                                    <asp:CompareValidator ID=cvNregE runat=server Text=* ErrorMessage="Sólo números" ControlToValidate=txtFiltroNRegE Operator=DataTypeCheck Type=Integer></asp:CompareValidator>                                    
                                    </td>                                        
                                    <!--<td nowrap>
                                        <asp:CheckBox ID= chkFiltroPorDiaE runat=server Checked=false Text="Una Lec. Por Día" TextAlign=Left />
                                    </td>  -->  
                                  
                                                                                                    
                                    <td colspan="14" style="color:#555555">&nbsp;</td>
                                </tr>
                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width=100%>
                                <asp:Repeater ID=rptEnergia runat=server>  
                                    <HeaderTemplate>
                                        <tr>
                                            <td class="titulolec" colspan=15>PUNTO: <%#obtenerCodigoPVYCR()%></td>
                                        </tr>
                                    
                                        <tr>
                                            <td class="tituloSec" colspan=15>DATOS ALIMENTACIÓN: <%#obtenerNomElemento()%></td>
                                        </tr>
                                        <tr>
                                            <td class="AnyoHidrologico" colspan=15>
                                            <%#obtenerAnyoHidrologico()%>&nbsp; &nbsp;&nbsp;Total Acumulado (Kwh): <%#obtenerVolumenElectricoAcumulado()%>&nbsp;&nbsp;&nbsp;&nbsp;<%#obtenerNumLecturasE()%>&nbsp;&nbsp;&nbsp;&nbsp; <%#obtenerTotNumLecturas("E", obtenerCodigoPVYCR(), obtenerNomElemento())%>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td class="encabListado">Código PVYCR</td>
                                        <td class="encabListado">Cod. Fuente Dato</td>
                                        <td class="encabListado">Fecha Medida</td>
                                        <td class="encabListado">Lectura I</td>
                                        <td class="encabListado">Lectura II</td>
                                        <td class="encabListado">Lectura III</td>
                                        <td class="encabListado">Total (Kwh)</td>
                                        <td class="encabListado">Diferencial (Kwh)</td>
                                        <td class="encabListado">Total (Kvar)</td>
                                        <td class="encabListado">Funciona</td>
                                        <td class="encabListado">Justificación</td>
                                        <td class="encabListado">Inc. Eléctrica</td>
                                        <td class="encabListado">Consumo Elec.Adi.</td>
                                        <td class="encabListado">Reinicio Lec. Elec.</td>
                                        <td class="encabListado">Observaciones</td>
                                        <td class="encabListado">&nbsp;</td>
                                        </tr>
                                    </HeaderTemplate>                                                      
                                    <ItemTemplate>
                                        <tr <%# checkFila("E",Container.DataItem)%>>
                                            <td nowrap><%#Container.DataItem("codigoPVYCR")%></td>
                                            <td nowrap><%#Container.DataItem("Cod_fuente_dato")%> - <%#Container.DataItem("DescFuenteDato")%></td>
                                            <td nowrap><%#Container.DataItem("fecha_Medida")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "LecturaI", "{0:#,##0.##}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "LecturaII", "{0:#,##0.##}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "LecturaIII", "{0:#,##0.##}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "Total_Kwh", "{0:#,##0.##}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "Diferencial", "{0:#,##0.##}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "Total_Kvar", "{0:#,##0.##}")%></td>
                                            <td><%#Container.DataItem("Funciona")%></td>
                                            <td><%#Container.DataItem("Justificacion")%></td>
                                            <td><%#Container.DataItem("descIncElec")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "ConsumoElectricoAdicional", "{0:#,##0.##}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem,"ReinicioLecturaElectrica", "{0:#,##0.##}")%></td>
                                            <td><%#Container.DataItem("Observaciones")%></td>
                                            <td><asp:LinkButton ID=lbtEditarE Runat=server CommandName='editar' CommandArgument='<%# container.dataitem("Fecha_Medida")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=15>               
                                  <uc:paginacion ID="ucPaginacionE" runat="server" Visible=false />        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Energia' -->
                        <!--Panel listar Acequias -->
                        <asp:Panel ID=pnlAcequias Runat=server Visible=false Height=650px>
                            <table width=100%>
                            <asp:Label ID="lblidElementoA" runat="server" Visible=false></asp:Label><tr>
                                <td align="right"><a>Filtrar por uno o varios de los siguientes campos</a></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666">
                                <table align="left" width=100%>
                                <tr>
                                                                        
                                    <td nowrap> Cod.Fuente Dato: 
                                      <asp:TextBox ID="txtFiltrarCodFuenteDato" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                    </td>    
                                                                            
                                    <td nowrap> Fecha Desde: 
                                      <asp:TextBox ID="txtfiltroFechaIni" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                      <asp:CompareValidator ID=cvfechaini runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaIni Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaIniQ" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                                                            
                                    </td>
                                    <td nowrap> Hasta: 
                                      <asp:TextBox ID="txtFiltroFechaFin" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                      <asp:CompareValidator ID=cvfechafin runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaFin Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaFinQ" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      
                                    </td>
                                    <td nowrap> ¿Mostrar Lecturas Nulas?
                                      <asp:CheckBox ID="chkFiltroNulasQ" runat="server" CssClass="texto" Checked=false/>
                                    </td>         
                                    <td nowrap>
                                    <asp:Button ID="btnFiltroAceptarA" runat="server" text="Filtrar" CssClass="boton" />
                                    <asp:Button ID="btnFiltroCancelarA" runat="server" Text="Limpiar" CssClass="boton" />
                                    <asp:Button ID="btnCancelarA" runat="server" Text="Cancelar" CssClass="boton" />
                                    </td>
                                
                                </tr>
                                <tr>
                                     <td nowrap> Nº Reg. Mostrados:
                                        <asp:TextBox ID="txtFiltroNregQ" runat="server" CssClass="texto" Width=50px></asp:TextBox>                                    
                                        <asp:CompareValidator ID=cvnregQ runat=server Text=* ErrorMessage="Sólo números" ControlToValidate=txtFiltroNRegQ Operator=DataTypeCheck Type=Integer></asp:CompareValidator>                                    
                                     </td>
                                    <td colspan="12" style="color:#555555">&nbsp;</td>
                                </tr>
                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width=100%>                                               
                                <asp:Repeater ID=rptAcequias runat=server>  
                                    <HeaderTemplate>
                                        <tr>
                                            <td class="titulolec" colspan=12>PUNTO: <%#obtenerCodigoPVYCR()%></td>
                                        </tr>
                                    
                                        <tr>
                                           <td class="tituloSec" colspan=12>DATOS ACEQUIAS: <%#obtenerNomElemento()%></td>
                                        </tr>
                                        <tr>
                                            <td class="AnyoHidrologico" colspan=12>
                                                <%#obtenerAnyoHidrologico()%> &nbsp; &nbsp;&nbsp;Total Acumulado (m3): <%#obtenerCaudalAcumulado() %>&nbsp;&nbsp;&nbsp;&nbsp;<%#obtenerNumLecturasQ()%> &nbsp;&nbsp;&nbsp;&nbsp; <%#obtenerTotNumLecturas("Q", obtenerCodigoPVYCR(), obtenerNomElemento())%>
                                            </td>
                                        </tr>
                                        <tr>                                        
                                        <td class="encabListado">codigo PVYCR</td>
                                        <td class="encabListado">Fecha Medida</td>
                                        <td class="encabListado">T. Obt. Caudal</td>
                                        <td class="encabListado">Cod.Fuente Dato</td>
                                        <td class="encabListado">Escala</td>
                                        <td class="encabListado">Calado</td>
                                        <td class="encabListado">Régimen Curva</td>
                                        <td class="encabListado">Nº. Parada</td>
                                        <td class="encabListado">Caudal (m3/s)</td>
                                        <td class="encabListado">Diferencial (m3)</td>
                                        <td class="encabListado">Duda Calidad</td>
                                        <td class="encabListado">Observaciones</td>
                                        <td class="encabListado">&nbsp;</td>
                                        </tr>
                                    </HeaderTemplate>                                                      
                                    <ItemTemplate>
                                        <tr <%# checkFila("Q",Container.DataItem)%>>
                                            <td nowrap><%#Container.DataItem("codigoPVYCR")%></td>
                                            <td nowrap><%#Container.DataItem("Fecha_Medida")%></td>
                                            <td nowrap><%#Container.DataItem("TipoObtencionCaudal")%></td>
                                            <td nowrap><%#Container.DataItem("Cod_Fuente_Dato")%> - <%#Container.DataItem("DescFuenteDato")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem,"Escala_M","{0:#,##0.##}")%></td>
                                            <td><%#Container.DataItem("Calado_M")%></td>
                                            <td><%#Container.DataItem("RegimenCurva")%></td>
                                            <td><%#Container.DataItem("NumeroParada")%></td>
                                            <td><%#Container.DataItem("Caudal_M3S")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "Diferencial", "{0:#,##0.##}")%></td>  
                                            <td><%#Container.DataItem("Duda_Calidad")%></td>                                                                                      
                                            <td><%#Container.DataItem("Observaciones")%></td>
                                            <td><asp:LinkButton ID=lbtEditarQ Runat=server CommandName='editar' CommandArgument='<%# container.dataitem("Fecha_Medida")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton></td>                                            
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=11>               
                                  <uc:paginacion ID="ucPaginacionA" runat="server" Visible=false />        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Acequias' -->                                            
                        <!-- Panel Volumen -->    
                        <asp:Panel ID=pnlVolumen Runat=server Visible=false Height=650px>
                            <table width=100%>
                            <asp:Label ID="lblidelementoV" runat="server" Visible=false></asp:Label><tr>
                                <td align="right"><a>Filtrar por uno o varios de los siguientes campos</a></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666">
                                <table align=left>
                                <tr>
                                    <td nowrap>Cod. Fuente Dato:
                                    <asp:TextBox ID="txtFiltrarCodFuenteDatoV" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                    </td>                                                                 
                                    <td nowrap> Fecha Desde: 
                                      <asp:TextBox ID="txtFiltroFechaIniV" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                      <asp:CompareValidator ID=cvFIV runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaIniV Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaIniV" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                                                                                                  
                                    </td>
                                    <td nowrap> Hasta: 
                                      <asp:TextBox ID="txtFiltroFechaFinV" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                      <asp:CompareValidator ID=cvFFV runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaFinV Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaFinV" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                                                                                                  
                                    </td>
                                    <td nowrap>¿Mostrar Lecturas Nulas?
                                      <asp:CheckBox ID="chkFiltroNulasV" runat="server" CssClass="texto" Checked=false/>
                                    </td>                                                                         
                                    <td nowrap>
                                    <asp:Button ID="btnFiltroAceptarV" runat="server" text="Filtrar" CssClass="boton" />
                                    <asp:Button ID="btnFiltroCancelarV" runat="server" Text="Limpiar" CssClass="boton" />
                                    <asp:Button ID="btnCancelarV" runat="server" Text="Cancelar" CssClass="boton" />
                                    </td>
                                
                                </tr>
                                <tr>
                                    <td nowrap> Nº Reg. Mostrados:
                                        <asp:TextBox ID="txtFiltroNRegV" runat="server" CssClass="texto" Width=50px></asp:TextBox>                                      
                                        <asp:CompareValidator ID=cvnregv runat=server Text=* ErrorMessage="Sólo números" ControlToValidate=txtFiltroNRegV Operator=DataTypeCheck Type=Integer></asp:CompareValidator>                                                                     
                                    </td> 
                                    <td colspan="3" style="color:#555555">&nbsp;</td>
                                </tr>
                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width=100%>                                               
                                <asp:Repeater ID=rptVolumen runat=server>  
                                    <HeaderTemplate>
                                        <tr>
                                            <td class="titulolec" colspan=10>PUNTO: <%#obtenerCodigoPVYCR()%></td>
                                        </tr>
                                        <tr>
                                            <td class="tituloSec" colspan=10>DATOS MOTORES: <%#obtenerNomElemento()%></td>
                                        </tr>
                                        <tr>
                                            <td class="AnyoHidrologico" colspan=12>
                                                <%#obtenerAnyoHidrologico()%> &nbsp; &nbsp;&nbsp;Total Acumulado (m3) : <%#obtenerVolumenAcumulado() %>&nbsp;&nbsp;&nbsp;&nbsp;<%#obtenerNumLecturasV()%> &nbsp;&nbsp;&nbsp;&nbsp; <%#obtenerTotNumLecturas("V", obtenerCodigoPVYCR(), obtenerNomElemento())%>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td class="encabListado">Código PVYCR</td>
                                        <td class="encabListado">Cod. Fuente Dato</td>
                                        <td class="encabListado">Fecha Medida</td>
                                        <td class="encabListado">Lectura Contador (m3)</td>
                                        <td class="encabListado">Diferencial (m3)</td>
                                        <td class="encabListado">Funciona</td>
                                        <td class="encabListado">Justificación</td>
                                        <td class="encabListado">Inc. Volumétrica</td>
                                        <td class="encabListado">Consumo Vol.Adi.</td>
                                        <td class="encabListado">Reinicio Lec. Vol.</td>
                                        <td class="encabListado">Observaciones</td>
                                        <td class="encabListado">&nbsp;</td>
                                        </tr>
                                    </HeaderTemplate>                                                      
                                    <ItemTemplate>
                                        <tr <%# checkFila("V",Container.DataItem)%> >
                                            <td nowrap><%#Container.DataItem("codigoPVYCR")%></td>
                                            <td nowrap><%#Container.DataItem("Cod_fuente_dato")%> - <%#Container.DataItem("DescFuenteDato")%></td>
                                            <td nowrap><%#Container.DataItem("fecha_Medida")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "LecturaContador_M3", "{0:#,##0.##}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "Diferencial", "{0:#,##0.##}")%></td>
                                            <td><%#Container.DataItem("Funciona")%></td>
                                            <td><%#Container.DataItem("Justificacion")%></td>
                                            <td><%#Container.DataItem("descIncVol")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "ConsumoVolumetricoAdicional", "{0:#,##0.##}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem,"ReinicioLecturaVolumetrica", "{0:#,##0.##}")%></td>
                                            <td><%#Container.DataItem("Observaciones")%></td>
                                            <td><asp:LinkButton ID=lbtEditarV Runat=server CommandName='editar' CommandArgument='<%# container.dataitem("Fecha_Medida")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=14>               
                                  <uc:paginacion ID="ucPaginacionV" runat="server" Visible=false />        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Volumen' -->
                        <!-- Panel Horometros -->    
                        <asp:Panel ID=pnlHorometros Runat=server Visible=false Width=720px  Height=650px >
                            <table width=100%>
                            <asp:Label ID="lblidelementoH" runat="server" Visible=false></asp:Label><tr>
                                <td align="right"><a>Filtrar por uno o varios de los siguientes campos</a></td>
                            </tr>
                            <tr>
                                <td style="background-color:#cccccc; border:1px solid #666666">
                                <table align="left">
                                <tr>
                                    <td nowrap>Cod. Fuente Dato:
                                    <asp:TextBox ID="txtFiltrarCodFuenteDatoH" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                    </td>
                                    <td nowrap> Fecha Desde: 
                                      <asp:TextBox ID="txtFiltroFechaIniH" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                      <asp:CompareValidator ID=cvFIH runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaIniH Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaIniH" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                                                            
                                    </td>
                                    <td nowrap> Hasta: 
                                      <asp:TextBox ID="txtFiltroFechaFinH" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                      <asp:CompareValidator ID=cvFFH runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaFinH Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaFinH" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                               
                                    </td>
                                    
                                    <td nowrap>
                                    <asp:Button ID="btnFiltroAceptarH" runat="server" text="Filtrar" CssClass="boton" />
                                    <asp:Button ID="btnFiltroCancelarH" runat="server" Text="Limpiar" CssClass="boton" />
                                    <asp:Button ID="btnCancelarH" runat="server" Text="Cancelar" CssClass="boton" />
                                    </td>
                                
                                </tr>
                                <tr>
                                    <td nowrap> Nº Reg. Mostrados:
                                        <asp:TextBox ID="txtFiltroNRegH" runat="server" CssClass="texto" Width=50px></asp:TextBox>                                                                                                                                                    
                                        <asp:CompareValidator ID=cvdptoaforo runat=server Text=* ErrorMessage="Sólo números" ControlToValidate=txtFiltroNRegH Operator=DataTypeCheck Type=Integer></asp:CompareValidator>
                                    </td>
                                    <td colspan="3" style="color:#555555">&nbsp;</td>
                                </tr>
                                </table>
                                </td>
                            </tr>
                            </table>
                            <table width=100%>                                               
                                <asp:Repeater ID=rptHorometros runat=server>  
                                    <HeaderTemplate>
                                        <tr>
                                            <td class="titulolec" colspan=9>PUNTO: <%#obtenerCodigoPVYCR()%></td>
                                        </tr>

                                        <tr>
                                            <td class="tituloSec" colspan=9>DATOS HORÓMETROS: <%#obtenerNomElemento()%></td>
                                        </tr>
                                        <tr>
                                            <td class="AnyoHidrologico" colspan=9>
                                                <%#obtenerAnyoHidrologico()%>&nbsp;&nbsp;&nbsp;&nbsp;<%#obtenerNumLecturasH()%>&nbsp;&nbsp;&nbsp;&nbsp; <%#obtenerTotNumLecturas("H", obtenerCodigoPVYCR(), obtenerNomElemento())%> 
                                            </td>
                                        </tr>
                                        <tr>
                                        <td class="encabListado">Código PVYCR</td>
                                        <td class="encabListado">Cod. Fuente Dato</td>
                                        <td class="encabListado">Fecha Medida</td>
                                        <td class="encabListado">Horas Intervalo</td>                                        
                                        <td class="encabListado">Funciona</td>
                                        <td class="encabListado">Inc. Volumétrica</td>
                                        <td class="encabListado">Consumo Vol.Adi.</td>
                                        <td class="encabListado">Reinicio Lec. Vol.</td>
                                        <td class="encabListado">Observaciones</td>
                                        <td class="encabListado">&nbsp;</td>
                                        </tr>
                                    </HeaderTemplate>                                                      
                                    <ItemTemplate>
                                        <tr <%# checkFila("H",Container.DataItem)%>>
                                            <td nowrap><%#Container.DataItem("codigoPVYCR")%></td>
                                            <td nowrap><%#Container.DataItem("Cod_fuente_dato")%> - <%#Container.DataItem("DescFuenteDato")%></td>
                                            <td nowrap><%#Container.DataItem("fecha_medida")%></td>
                                            <td><%#Container.DataItem("HorasIntervalo")%></td>                                            
                                            <td><%#Container.DataItem("Funciona")%></td>
                                            <td><%#Container.DataItem("descIncVol")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem, "ConsumoVolumetricoAdicional", "{0:#,##0.##}")%></td>
                                            <td><%#DataBinder.Eval(Container.DataItem,"ReinicioLecturaVolumetrica", "{0:#,##0.##}")%></td>
                                            <td><%#Container.DataItem("Observaciones")%></td>
                                            <td><asp:LinkButton ID=lbtEditarH Runat=server CommandName='editar' CommandArgument='<%# container.dataitem("Fecha_Medida")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=14>               
                                  <uc:paginacion ID="ucPaginacionH" runat="server" Visible=false />        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar horometros' -->   
                        <!-- Panel Editar  Energia-->
                        <asp:Panel ID=pnlEDEnergia Runat=server Visible=false BorderWidth=1px BorderColor=black style="margin-top:10px" >
                        <asp:label id=lblFechamedidaESel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec style="padding:20px"><B><%#checkNombreAlimentacion() %> </B></asp:Label><br /><br />
                            <table width=100% cellspacing=20 cellpadding=10 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td >Fecha Medida</td>
                                    <td><asp:label ID=txtFechaMedida runat=server CssClass="displayClave" ></asp:label>
                                    </td>
                                    <td>Cod. Fuente Dato</td>
                                    <td nowrap><asp:label ID=txtCodFuenteDato runat=server CssClass="displayClave"></asp:label>
                                </tr>
                                <tr>
                                    <td>Lectura I</td>
                                    <td>
                                      <asp:textbox ID=txtLecturaI runat=server CssClass=texto />

                                    </td>
                                    <td>Lectura II</td>
                                    <td>
                                      <asp:textbox ID=txtLecturaII runat=server CssClass=texto></asp:textbox>
                                    </td>                                 
                                </tr>
                                <tr>
                                    <td>Lectura III</td>
                                    <td>
                                      <asp:TextBox ID=txtLecturaIII runat="server" CssClass=texto></asp:TextBox>
                                    </td>
                                    <td>Total (Kwh)</td>
                                    <td>
                                      <asp:textbox ID=txtTotal_Kwh runat=server CssClass=texto ></asp:textbox>
                                    </td>                                     
                                </tr>
                                
                                <tr>
                                    <td>Total (Kvar)</td>
                                    <td>
                                      <asp:textbox ID=txtTotal_Kvar runat=server CssClass=texto ></asp:textbox>
                                    </td>
                                    
                                    <td>Funciona</td>
                                    <td style="font:verdana 11px">   
                                      <asp:DropDownList ID="ddlfunciona" runat="server">
                                        <asp:ListItem Value="0" Text="SI"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="NO"></asp:ListItem>
                                      </asp:DropDownList></td>                                
                                </tr>
                                <tr>
                                    <td>Justificacion</td>
                                    <td><asp:textbox ID="txtJustificacion" runat="server" CssClass="texto"></asp:textbox></td>  
                                    <td>Inc. Eléctrica</td>                              
                                    <td><asp:DropDownList ID="ddlIncidenciasE" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Consumo Elec. Adicional</td>
                                    <td>
                                      <asp:textbox ID="txtConsumoElectricoAdicional" runat="server" CssClass="texto" ></asp:textbox>
                                    </td>  
                                    <td>Reinicio Lec. Eléctrica</td>                              
                                    <td>
                                      <asp:TextBox ID=txtReinicioLecturaElectrica runat=server CssClass="texto" ></asp:TextBox>
                                    </td>                                
                                </tr>
                                <tr>
                                    <td>Observaciones</td>
                                    <td colspan=3><asp:textbox ID="txtObservaciones" runat="server" CssClass="texto" Width=100% TextMode=multiLine></asp:textbox></td>
                                </tr>
                                 <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    &nbsp;
                                 </td>
                                </tr>          
                                <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    <asp:Button ID="btnAceptarEDEnergia" runat="server" Text="Aceptar" CssClass="boton"/>
                                    <asp:Button ID="btnCancelarEDEnergia" runat="server" Text="Cancelar" CssClass="boton"/>
                                 </td>
                                </tr>                                                            
                                </table>            
                            </td>
                           
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Energia -->
                        <!-- Panel Editar  Acequias-->
                        <asp:Panel ID=pnlEDAcequias Runat=server Visible=false BorderWidth=1px BorderColor=black style="margin-top:10px" >
                        <asp:label id=lblFechamedidaQSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTituloQ runat=server CssClass=tituloSec style="padding:20px"><B><%#checkNombreAcequias()%> </B></asp:Label><br /><br />
                            <table width=100% cellspacing=20 cellpadding=10 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td >Fecha Medida</td>
                                    <td><asp:label ID=lblFechamedidaQ runat=server CssClass="displayClave" ></asp:label>
                                    </td>
                                    <td>Cod. Fuente Dato</td>
                                    <td nowrap><asp:label ID=lblCodFuenteDatoQ runat=server CssClass="displayClave"></asp:label>
                                    <td>TipoObt. Caudal</td>
                                    <td><asp:Label ID=lblTipoObtencionCaudal runat="server" CssClass="displayClave"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Escala (m)</td>
                                    <td>
                                      <asp:textbox ID=txtEscalaQ runat=server CssClass=texto />
                                    </td>
                                    <td>Calado (m)</td>
                                    <td>
                                      <asp:textbox ID=txtCaladoQ runat=server CssClass=texto ></asp:textbox>
                                    </td>                                 
                                </tr>
                                <tr>
                                    
                                    <td>Régimen Curva</td>
                                    <td >   
                                      <asp:DropDownList ID="ddlRegimenCurvaQ" runat="server">
                                      </asp:DropDownList></td>                                
                                    <td>Nº Parada</td>
                                    <td>
                                      <asp:textbox ID=txtNumParadaQ runat=server CssClass=texto></asp:textbox>                                      
                                    </td>
                                      
                                </tr>
                                
                                <tr>
                                    <td>Caudal (m3/s)</td>
                                    <td><asp:textbox ID="txtCaudalQ" runat="server" CssClass="texto" ></asp:textbox></td>  
                                    <td>Duda Calidad</td>                              
                                    <td>
                                      <asp:textbox ID="txtDudaCalidad" runat="server" CssClass="texto"></asp:textbox>
                                    </td>  
                                </tr>
                                <tr>
                                    <td>Observaciones</td>
                                    <td colspan=5><asp:textbox ID="txtobservacionesQ" runat="server" CssClass="texto" Width=100% TextMode=MultiLine></asp:textbox></td>
                                </tr>
                                 <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    &nbsp;
                                 </td>
                                </tr>          
                                <tr>
                                <td>&nbsp;</td>
                                 <td nowrap>
                                    <asp:Button ID="btnAceptarEDAcequias" runat="server" Text="Aceptar" CssClass="boton"/>
                                    <asp:Button ID="btnCancelarEDAcequias" runat="server" Text="Cancelar" CssClass="boton"/>
                                 </td>
                                </tr>                                                            
                                </table>            
                            </td>
                           
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar acequias -->                     
                        <!-- Panel Editar  Volumetricos-->
                        <asp:Panel ID=pnlEDVolumen Runat=server Visible=false BorderWidth=1px BorderColor=black style="margin-top:10px" >
                        <asp:label id=lblFechamedidaVSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTituloV runat=server CssClass=tituloSec style="padding:20px"><B><%#checkNombreMotores() %> </B></asp:Label><br /><br />
                            <table width=100% cellspacing=20 cellpadding=10 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td >Fecha Medida</td>
                                    <td><asp:label ID=lblFechaMedidaV runat=server CssClass="displayClave" ></asp:label>
                                    </td>
                                    <td>Cod. Fuente Dato</td>
                                    <td nowrap><asp:label ID=lblCodfuentedatoV runat=server CssClass="displayClave"></asp:label>
                                </tr>
                                <tr>
                                    <td>Lectura Contador (m3)</td>
                                    <td>
                                      <asp:textbox ID=txtlecturacontador runat=server CssClass=texto />
                                    </td>
       
                                    <td>Funciona</td>
                                    <td style="font:verdana 11px">   
                                      <asp:DropDownList ID="ddlFuncionaV" runat="server">
                                        <asp:ListItem Value="0" Text="SI"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="NO"></asp:ListItem>
                                      </asp:DropDownList>
                                    </td>                                          
                                </tr>
                                <tr>
                                    <td>Justificacion</td>
                                    <td><asp:textbox ID="txtJustificacionV" runat="server" CssClass="texto"></asp:textbox></td>  
                                    <td>Inc. Volumétrica</td>                              
                                    <td><asp:DropDownList ID="ddlIncidenciaV" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Consumo Vol. Adicional</td>
                                    <td>
                                      <asp:textbox ID="txtConsumoV" runat="server" CssClass="texto" ></asp:textbox>
                                    </td>  
                                    <td>Reinicio Lec. Volumétrica</td>                              
                                    <td>
                                      <asp:TextBox ID=txtReinicioV runat=server CssClass="texto" ></asp:TextBox>
                                    </td>                                
                                </tr>
                                <tr>
                                    <td>Observaciones</td>
                                    <td colspan=3><asp:textbox ID="txtObservacionesV" runat="server" CssClass="texto" Width=100% TextMode=multiline></asp:textbox></td>
                                </tr>
                                 <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    &nbsp;
                                 </td>
                                </tr>          
                                <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    <asp:Button ID="btnAceptarEDVolumen" runat="server" Text="Aceptar" CssClass="boton"/>
                                    <asp:Button ID="btnCancelarEDVolumen" runat="server" Text="Cancelar" CssClass="boton"/>
                                 </td>
                                </tr>                                                            
                                </table>            
                            </td>
                           
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Volumétricos -->    
                        <!-- Panel Editar  horometros-->
                        <asp:Panel ID=pnlEDHorometros Runat=server Visible=false BorderWidth=1px BorderColor=black style="margin-top:10px" >
                        <asp:label id=lblFechamedidaHSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lbltituloH runat=server CssClass=tituloSec style="padding:20px"><B><%#checkNombreHorometros() %> </B></asp:Label><br /><br />
                            <table width=100% cellspacing=20 cellpadding=10 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td >Fecha Medida</td>
                                    <td><asp:label ID=lblFechaMedidaH runat=server CssClass="displayClave" ></asp:label>
                                    </td>
                                    <td>Cod. Fuente Dato</td>
                                    <td nowrap><asp:label ID=lblCodfuenteDatoH runat=server CssClass="displayClave"></asp:label>
                                </tr>
                                <tr>
                                    <td>Horas Intervalo</td>
                                    <td>
                                      <asp:textbox ID=txtHorasIntervalo runat=server CssClass=texto />
                                    </td>
       
                                    <td>Funciona</td>
                                    <td style="font:verdana 11px">   
                                      <asp:DropDownList ID="ddlFuncionaH" runat="server">
                                        <asp:ListItem Value="0" Text="SI"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="NO"></asp:ListItem>
                                      </asp:DropDownList>
                                    </td>                                          
                                </tr>
                                <tr>
                                    <td>Inc. Horómetro</td>                              
                                    <td><asp:DropDownList ID="ddlIncidenciaH" runat="server"></asp:DropDownList></td>
                                     <td>Consumo Hor. Adicional</td>
                                    <td>
                                      <asp:textbox ID="txtConsumoH" runat="server" CssClass="texto" ></asp:textbox>
                                    </td>  
                                </tr>
                                <tr>
                                   <td>Reinicio Lec. Horómetro</td>                              
                                    <td>
                                      <asp:TextBox ID=txtreinicioH runat=server CssClass="texto" ></asp:TextBox>
                                   </td> 
                                   <td>&nbsp;</td>                               
                                </tr>
                                <tr>
                                  <td>Observaciones</td>
                                    <td colspan=4><asp:textbox ID="txtobservacionesH" runat="server" CssClass="texto" Width=100% TextMode=MultiLine></asp:textbox></td>                                                             
                                </tr>
                                 <tr>
                                    <td>&nbsp;</td>
                                 <td>
                                    &nbsp;
                                 </td>
                                </tr>          
                                <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    <asp:Button ID="btnAceptarEDHorometro" runat="server" Text="Aceptar" CssClass="boton"/>
                                    <asp:Button ID="btnCancelarEDHorometro" runat="server" Text="Cancelar" CssClass="boton"/>
                                 </td>
                                </tr>                                                            
                                </table>            
                            </td>
                           
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Horometros -->                                                                     
                    </td>
                    <!-- Fin Celda2 contenido-->
                  </tr>
                  <!-- Fin Celda Menú contenido-->
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>
