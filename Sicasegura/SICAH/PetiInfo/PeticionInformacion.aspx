<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PeticionInformacion.aspx.vb" Inherits="SICAH_PetiInfo_PeticionInformacion" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
     <link href="../../styles.css" rel="stylesheet"/>
     
     
     <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
     <script type="text/jscript" language="javascript" src="../../js/calendar/calendar.js"/>     
     <script type="text/jscript" language="javascript" src="../../js/utilesMenus.js"/>
     <script type="text/jscript" language="javascript" src="../../menu.js"/>
           

    
<script language="javascript" type="text/javascript">
// <!CDATA[

    function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación del Cauce?")==true)
        return true;
      else
        return false;
    }
    

// ]]>
</script>

    <link href="../../styles.css" rel="stylesheet" />

</head>
<body bgcolor="#EEEAD2">
    <form id="Form1" runat="server" method="post">
    
    <span id="dsp4"></span>    
    <span id="imagepath" style="display:none">../../js/calendar/images</span>
      <table width="90%" cellspacing="2" align="center" style="border: 1px solid #666666;background-color: white">
       <tr>
        <td>
           <table cellspacing="0" cellpadding="1" width="100%">
           <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label><tr>
              <td>
                <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <!-- Celda Menú - Contenido -->
                <tr>

                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%" valign="top">
                        <!-- Panel consulta de peticiones -->                      
                        <asp:Panel ID="pnlConsulta" Runat="server" Visible="true">

                            <table width="100%">                                               
                                 

                                    <tr>
                                    <td class="tituloSec" colspan="6">PETICIÓN DE INFORMACIÓN. Consulta</td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="encabListado">Petición:
                                        </td> 
                                        <td class="encabListado"><asp:TextBox runat="server" ID="filtroNumsoli" CssClass="texto" Columns="10"></asp:TextBox>
                                        </td>
                                        <td class="encabListado" style="white-space: nowrap">Fecha alta. Desde: 
                                        </td> 
                                        <td style="white-space: nowrap" class="encabListado"><asp:TextBox runat="server" ID="filtroFeDesde" CssClass="texto" Columns="10"></asp:TextBox>
                                          &nbsp;
                                          <asp:Image    ID="imgFiltroFeDesde" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" />
                                            <asp:CompareValidator ID="validadorFiltroFeDesde" runat="server" ControlToValidate="filtroFeDesde"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="No valido"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                        </td>
                                        <td class="encabListado" style="white-space: nowrap">Fecha alta. Hasta: 
                                        </td> 
                                        <td style="white-space: nowrap" class="encabListado"><asp:TextBox runat="server" ID="filtroFeHasta" CssClass="texto" Columns="10"></asp:TextBox>
                                        &nbsp;
                                          <asp:Image    ID="imgFiltroFeHasta" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" />
                                            <asp:CompareValidator ID="CompareFiltroFeHasta" runat="server" ControlToValidate="filtroFeHasta"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="No valido"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="encabListado">Origen
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="filtroOrigen" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>
                                        <td class="encabListado">Nombre solicitante
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="filtroNombreSoli" CssClass="texto" Columns="50"></asp:TextBox>
                                        </td> 
                                        <td class="encabListado">Correo solicitante                                        
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="filtroEmail" CssClass="texto" Columns="50"></asp:TextBox>
                                        </td>                                                                                
                                    </tr>
                                    <tr>
                                        <td class="encabListado">Clase
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="filtroClase" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>
                                        <td class="encabListado">Tipo
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="filtroTipo" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>
                                        <td class="encabListado">SubTipo
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="filtroSubtipo" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>                                        
                                    </tr>

                                    <tr>
                                        <td class="encabListado">Inscripción
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="filtroInscripcion" CssClass="texto" Columns="20"></asp:TextBox>
                                        </td>
                                        <td class="encabListado">Descripción
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="filtroDescripcion" CssClass="texto" Columns="50"></asp:TextBox>
                                        </td> 
                                        <td class="encabListado">Ref. Expe
                                        </td>                                                                               
                                        <td class="encabListado"><asp:TextBox runat="server" ID="filtroRefExpediente" CssClass="texto" Columns="50"></asp:TextBox>
                                        </td>                                          
                                    </tr>
                                           
                                    <tr>
                                        <td class="encabListado">Cod. SICA
                                        </td>                                                                               
                                        <td class="encabListado"><asp:TextBox runat="server" ID="filtroCodSICA" CssClass="texto" Columns="30"></asp:TextBox>
                                        </td>  

                                        <td class="encabListado">Estado
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="filtroEstado" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>

                                        <td class="encabListado">Asignado
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="filtroAsignado" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>
                                       
                                    </tr>
                                             
                                    <tr>
                                        <td class="encabListado" colspan="6" style="text-align: right;">
                                        <asp:Button ID="botonFiltroLimpiar" Runat="server" cssclass="boton" Text="Limpiar"/>
                                        &nbsp;
                                        <asp:Button ID="botonFiltroBuscar" Runat="server" cssclass="boton" Text="Buscar"/>
                                        &nbsp;
                                        <asp:Button ID="botonFiltroNuevo" Runat="server" cssclass="boton" Text="Nuevo"/>
                                        </td>
                                    </tr>

                            </table>
                            <table width="100%">
                                        <tr>
                                            <td class="tituloLecturas">Peti</td>
                                            <td class="tituloLecturas">Fecha</td>
                                            <td class="tituloLecturas">Estado</td>
                                            <td class="tituloLecturas">Tipo</td>
                                            <td class="tituloLecturas">Subtipo</td>
                                            <td class="tituloLecturas">Ins</td>
                                            <td class="tituloLecturas">Descripción</td>
                                            <td class="tituloLecturas">SICA</td>
                                            <td class="tituloLecturas">Asig</td>
                                            <td class="tituloLecturas">&nbsp;</td>                                            
                                        </tr>                                    
                                    <asp:Repeater ID="rptPeticiones" runat="server">                                                     
                                    <ItemTemplate>
                                        <tr <%# checkFila()%>>
                                            <td><%#Container.DataItem("CodSoli")%></td>
                                            <td><%#Container.DataItem("FechaSoli")%></td>
                                            <td style="width:250px;"><%#Container.DataItem("DesEstado")%></td>
                                            <td style="width:200px;"><%#Container.DataItem("DesTipo")%></td>
                                            <td><%#Container.DataItem("DesSubtipo")%></td>
                                            <td><%#Container.DataItem("Inscripcion")%></td>
                                            <td style="width:200px;"><%#Container.DataItem("Descripcion")%></td>
                                            <td><%#Container.DataItem("CodSICA")%></td>
                                            <td><%#Container.DataItem("Asignado")%></td>
                                             <td style="white-space: nowrap" align="right" width="36">
                                                <asp:LinkButton ID="botonFiltroEdicion" Runat="server" CommandName="editar" CommandArgument=<%# container.dataitem("CodSoli")%>><img src="../../images/edit.gif" border="0" align="left" alt="Editar datos"></asp:LinkButton>                                                
                	                        </td
                                            
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                 
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel consulta de peticiones -->
                        
                        <!-- Panel Editar  Peticion-->
                        <asp:Panel ID="pnlEdicion" Runat="server" Visible="false">
                        
                            <table width="100%">                                               
                                 

                                    <tr>
                                    <td class="tituloSec" colspan="6">PETICIÓN DE INFORMACIÓN. Edición</td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="encabListado">Petición:
                                        </td> 
                                        <td class="encabListado"><asp:TextBox runat="server" ID="edicionNumsoli" CssClass="texto" Columns="10" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="encabListado" style="white-space: nowrap">Fecha Solicitud: 
                                        </td> 
                                        <td style="white-space: nowrap" class="encabListado" colspan="3"><asp:TextBox runat="server" ID="edicionFecha" CssClass="texto" Columns="10" Enabled="false"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="encabListado">Origen
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="210px" ID="edicionOrigen" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>
                                        <td class="encabListado">Nombre solicitante
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="edicionNombreSoli" CssClass="texto" Columns="50"></asp:TextBox>
                                        </td>                                               
                                        <td class="encabListado">Correo solicitante
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="edicionEmail" CssClass="texto" Columns="50"></asp:TextBox>
                                        </td>                                                                           
                                    </tr>
                                    <tr>
                                        <td class="encabListado">Clase
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="210px" ID="edicionClase" runat="server" AutoPostBack="true" OnSelectedIndexChanged="edicionClase_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td class="encabListado">Tipo
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="edicionTipo" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>
                                        <td class="encabListado">SubTipo
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="edicionSubtipo" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>                                        
                                    </tr>


                                    <tr>
                                        <td class="encabListado">Requiere firma de
                                        </td>
                                        <td class="encabListado"><asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="210px" ID="edicionRequiereFirma" runat="server" AutoPostBack="false"></asp:DropDownList>
                                        </td>
                                        <td class="encabListado">Fecha consumo desde
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="edicionFeConsuDesde" CssClass="texto" Columns="10" Enabled="true"></asp:TextBox>
                                            &nbsp;
                                          <asp:Image    ID="imgEdicionFeConsuDesde" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" />
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="edicionFeConsuDesde"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="No valido"
                                            Type="Date" Width="1px"></asp:CompareValidator>                                        
                                        </td>                                        
                                        <td class="encabListado">Fecha consumo hasta
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="edicionFeConsuHasta" CssClass="texto" Columns="10" Enabled="true"></asp:TextBox>
                                            &nbsp;
                                          <asp:Image    ID="imgEdicionFeConsuHasta" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" />
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="edicionFeConsuHasta"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="No valido"
                                            Type="Date" Width="1px"></asp:CompareValidator>                                          
                                        </td>                                                           
                                    </tr>
                                    
                                    
                                    <tr>
                                        <td class="encabListado">Inscripción
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="edicionInscripcion" CssClass="texto" Columns="20"></asp:TextBox>
                                        </td>
                                        <td class="encabListado">Descripción
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="edicionDescripcion" CssClass="texto" Columns="50" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                        </td>                                        
                                        <td class="encabListado">Ref. Expe
                                        </td>
                                        <td class="encabListado"><asp:TextBox runat="server" ID="edicionRefExpediente" CssClass="texto" Columns="50"></asp:TextBox>
                                        </td>                                                           
                                    </tr>
                                                                                   
                                    <tr>
                                        <td class="encabListado">Cod. SICA
                                        </td>                                                                               
                                        <td class="encabListado"><asp:TextBox runat="server" ID="edicionCodSICA" CssClass="texto" Columns="20"></asp:TextBox>
                                        </td>                                     
                                        <td class="encabListado">Estado
                                        </td>
                                        <td class="encabListado">
                                        <asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="edicionEstado" runat="server" AutoPostBack="false" Enabled="false" ></asp:DropDownList>
                                        </td>
                                        <td class="encabListado">Fecha Estado
                                        </td>
                                        <td class="encabListado">
                                        <asp:TextBox runat="server" ID="edicionFechaEstado" CssClass="texto" Columns="10" Enabled="False"></asp:TextBox>
                                        </td>                                        

                                    </tr>
                                                                                 
                                    <tr>
                                    
                                        <td class="encabListado">Asignado
                                        </td>    
                                        <td class="encabListado">
                                          <asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="210px" ID="edicionAsignado" runat="server" AutoPostBack="false"  Enabled="false"></asp:DropDownList>
                                        </td>   

                                        <td class="encabListado">Motivo Estado
                                        </td>
                                        <td class="encabListado">
                                        <asp:TextBox runat="server" ID="edicionMotivoEstado" CssClass="texto" Columns="40" Enabled="False"></asp:TextBox>
                                        </td>                                                                              
                                                                            
                                        <td class="encabListado" colspan="2" style="text-align: right;">
                                        <asp:Button ID="botonEdicionGrabar" Runat="server" cssclass="boton" Text="Grabar"/>
                                        &nbsp;
                                        <asp:Button ID="botonEdicionSalirGrabar" Runat="server" cssclass="boton" Text="Grabar y Salir"/>
                                        &nbsp;
                                        <asp:Button ID="botonEdicionSalirNoGrabar" Runat="server" cssclass="boton" Text="Salir Sin grabar"/>
                                        </td>
                                    </tr>

                            </table>

                            <!-- Panel Editar Peticion. Acciones que puede realizar el solicitante-->
                            <asp:Panel ID="pnlEdSolicitante" Runat="server" Visible="false">
                                <br/>
                                <table width="100%">
                                    <tr>
                                        <td class="tituloLecturas" colspan="4">Acciones asociadas a Solicitante</td>                                       
                                    </tr> 
                                     


                                    <tr>
                                        <td class="encabListado">Anular Informe
                                        </td>
                                        <td class="encabListado">Motivo
                                        </td>    
                                        <td class="encabListado">
                                          <asp:TextBox runat="server" ID="edAcSoMotivoAnu" cssClass="texto" Columns="80"></asp:TextBox>
                                        </td>                                                                             
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcSoAnular" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>

                                    <tr>
                                        <td class="encabListado">Repetir Informe
                                        </td>
                                        <td class="encabListado">Motivo
                                        </td>    
                                        <td class="encabListado">
                                          <asp:TextBox runat="server" ID="edAcSoMotivoRepe" cssClass="texto" Columns="80"></asp:TextBox>
                                        </td>                                                                             
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcSoRepetir" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>
                                           
                                    <tr>
                                        <td class="encabListado" colspan="3">Visualizar Informe
                                        </td>
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcSoVisualizarInfo" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>
                                                                                                            
                                </table>                                
                            </asp:Panel>     
                            
                            <!-- Panel Editar Peticion. Acciones que puede realizar el asignado (persona uqe hace el informe)-->                                                                           
                            <asp:Panel ID="pnlEdAsignado" Runat="server" Visible="false">
                                <br/>
                                <table width="100%">
                                    <tr>
                                        <td class="tituloLecturas" colspan="2">Acciones asociadas a Asignado</td>                                       
                                    </tr>  

                                    <tr>
                                        <td class="encabListado">Añadir Informe
                                        </td>
                                        <td class="encabListado" style="text-align: right;">                                        
                                        <asp:FileUpload ID="FileUpload" runat="server" Font-Names="Verdana" Font-Size="Smaller" Width="50%" />                                        
                                        <asp:Button ID="botonAcAsigAgregarInfo" Runat="server" cssclass="boton" Text="Ejecutar"/>
                                        </td>                                        
                                    </tr>

                                    <tr>
                                        <td class="encabListado">Eliminar Informe
                                        </td>
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcAsigBorrarInfo" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>
                                            
                                    <tr>
                                        <td class="encabListado">Pedir Validación a Responsable
                                        </td>
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcAsigPedirValiInfo" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>
                                                                                                                                                
                                    <tr>
                                        <td class="encabListado">Visualizar Informe
                                        </td>
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcAsigVisualizarInfo" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>
                                                                        
                                </table>                                
                            
                            </asp:Panel>           
                            
                            <!-- Panel Editar Peticion. Acciones que puede realizar el responsable(s) de la aplicación-->                                                                                                                    
                            <asp:Panel ID="pnlEdResponsable" Runat="server" Visible="false">
                                <br/>
                                <table width="100%">
                                    <tr>
                                        <td class="tituloLecturas" colspan="6">Acciones asociadas a Responsable</td>                                       
                                    </tr>  
                                                                                                            
                                    <tr>
                                        <td class="encabListado">Anular Informe
                                        </td>
                                        <td class="encabListado">Motivo
                                        </td>    
                                        <td class="encabListado" colspan="3">
                                          <asp:TextBox Runat="server" ID="edAcReMotivoAnu" cssClass="texto" Columns="80"></asp:TextBox>
                                        </td>                                                                             
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcReAnular" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>


                                    <tr>
                                        <td class="encabListado" colspan="3">Asignar Informe
                                        </td>                 

                                        <td class="encabListado">Asignado
                                        </td>    
                                        <td class="encabListado">
                                          <asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="edAcReAsigAsignado" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>                                                                             
                                                                                                 
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcReAsig" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>
                                                                        
                              
                                    <tr>
                                        <td class="encabListado">Pedir Revisión Informe
                                        </td>
                                        <td class="encabListado">Motivo
                                        </td>    
                                        <td class="encabListado" colspan="3">
                                          <asp:TextBox runat="server" ID="edAcRePedirReviAsigMotivo" CssClass="texto" Columns="80"></asp:TextBox>
                                        </td>                                                                             
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcRePedirReviAsig" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>	
                                    
                                    <tr>
                                        <td class="encabListado">Reasignar Informe
                                        </td>
                                        <td class="encabListado">Motivo
                                        </td>    
                                        <td class="encabListado">
                                          <asp:TextBox runat="server" ID="edAcReReasigMotivo" CssClass="texto" Columns="50"></asp:TextBox>
                                        </td>                    

                                        <td class="encabListado">Asignado
                                        </td>    
                                        <td class="encabListado">
                                          <asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width="310px" ID="edAcReReasigAsignado" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                        </td>                                                                             
                                                                                                 
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcReReasig" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>
                                    
                                                                  
                                    <tr>
                                        <td class="encabListado" colspan="5">Validar Informe
                                        </td>
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcReValidarInfo" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>
                                                                        
                                    <tr>
                                        <td class="encabListado" colspan="5">Visualizar Informe
                                        </td>
                                        <td class="encabListado" style="text-align: right;">
                                        <asp:Button ID="botonAcReVisualizarInfo" Runat="server" cssclass="boton" Text="Ejecutar"/>                                        
                                        </td>                                        
                                    </tr>
                                                                        
                                </table>                                
                            
                            </asp:Panel>                                                    
                        </asp:Panel>               
                        <!-- Fin Panel Editar Peticion -->
                    </td>
                    <!-- Fin Celda Contenido -->        
                </tr>
                <!-- Fin Celda Menú - Contenido -->
                   

        
</table></td></tr></table> 
        
</td></tr></table> 
    </form>
</body>
</html>

