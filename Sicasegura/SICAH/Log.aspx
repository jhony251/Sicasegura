<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Log.aspx.vb" Inherits="SICAH_Log" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
       <link rel="stylesheet" href="..\styles.css">
     <link href="../styles.css" rel="stylesheet" />
    <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
     <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
    <script language="javascript" src="../js/utilesMenus.js"></script>  
    

    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
   <script language="javascript" type="text/javascript">
       function filtrar(){
    if (document.getElementById("FilaFiltro").style.display=="none"){
        document.getElementById("FilaFiltro").style.display="block" ;
        document.getElementById("MostrarFiltro").innerText= "Ocultar Filtro";
                
    }else{
        document.getElementById("FilaFiltro").style.display="none";
        document.getElementById("MostrarFiltro").innerText= "Mostrar Filtro";
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <span id="dsp4"></span>
     <span id="imagepath" style="display:none">../js/calendar/images</span>    
       <table width="100%" cellspacing="2" align="center" style="border: 1px solid #666666;background-color: white">
       <tr>
        <td>
           <table cellspacing="0" cellpadding="1" width="100%" >
           <asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label><tr>
              <td >
                <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <!-- Celda Menú - Contenido -->
                <tr>
                <!-- Celda Menú -->
                    <td valign="top" style="padding-top: 20px; width:20%; height: 621px;">
                    <%=genHTMLMenus.generaMenuMtoSica(session("idperfil"))%>
                    </td>   
                    <!-- línea vertical que separa el menú de los datos -->
                    <td style="background-image:url(../images/dot2.gif);width:3px"></td>
                   <!-- Fin Celda Menú -->                    
                    <!-- Celda Contenido -->
                    <td style="padding-left:20px; padding-right:20px; width:79%" valign=top >        
                    <asp:Panel ID=pnlLog Runat=server Visible=true>
                    <table width=100%>
                    <tr>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td style="background-color:#cccccc; border:1px solid #666666">
                        <table align="right">
                         <tr>
                        <td colspan="4"  align="right">Registros a mostrar  <asp:TextBox  ID="txtRegistros"  runat="server" Text="30" Width="30px"  CssClass="texto"></asp:TextBox><asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtRegistros"
                                    Display="Dynamic" ErrorMessage="Sólo números" Operator="DataTypeCheck" Text="?" Type="Integer" Width="8px"></asp:CompareValidator>
                                <asp:Button runat="server" ID="btnVertodos" cssclass="boton" Text="Actualizar"  /></td>
                        </tr>
                        </table>
                        </td>
                    </tr>
                    </table>
                    <table width=100%> 
                          <tr><td class="tituloSec" colspan=4>CONSULTA LOG</td>
                            </tr>
                            <tr>
                                <td  align="left" colspan="1"><a  id = "MostrarFiltro" href="javascript:void(0)" onclick="javascript:filtrar()">Ocultar Filtro</a></td>
                                <tr><td class="encabListado">Login</td>
                                <td class="encabListado">Fecha Conexión</td>
                                <td class="encabListado">Fecha Desconexión</td>   
                                <td class="encabListado">&nbsp;</td>   
                            </tr>
                            <tr id="FilaFiltro"  >         
                                 <td class="encabListado"><asp:TextBox runat="server" ID="txtFLogin" CssClass="texto" Columns="20"></asp:TextBox></td>                                                  
                                 <td class="encabListado"><asp:TextBox runat="server" ID="txtFFConexion" CssClass="texto" Columns="25"></asp:TextBox>
                                 <asp:CompareValidator ID="cvFFConexion" runat="server" Text="*" ErrorMessage="Fecha no válida" ControlToValidate="txtFFConexion" Operator="DataTypeCheck" Type="date"></asp:CompareValidator>
                                 <asp:Image ID="imgCalFechaCon" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"  Style="cursor: pointer" />  </td>  
                                 <td class="encabListado"><asp:TextBox runat="server" ID="txtFFDesconexion" CssClass="texto" Columns="20"></asp:TextBox>
                                 <asp:CompareValidator ID="cvCalFechafin" runat="server" Text="*" ErrorMessage="Fecha no válida" ControlToValidate="txtFFDesconexion" Operator="DataTypeCheck" Type="date"></asp:CompareValidator>
                                 <asp:Image ID="imgCalFechaDes" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"  Style="cursor: pointer" />  </td>  </td>  
                                 <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsReg"  Runat="server" OnClick="AceptarFiltro" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                            </tr>   
                            <asp:Repeater ID=rptLog runat=server>                                                     
                            <ItemTemplate>
                                <tr <%# checkFila()%>>
                                    <td><%#Container.DataItem("Login")%></td>
                                    <td><%#Container.DataItem("FechaConexion")%></td>
                                    <td><%#Container.DataItem("FechaDesconexion")%></td> 
                                    <td>&nbsp;</td>                           
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                         <!-- Número de páginas -->
                       <tr>
                          
                          <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=9> 
                                        
                          </td>
                      </tr>
                       <!-- Fin Número de páginas -->                  
                    </table>
                </asp:Panel>
                </td>
                </tr>
                </table>
                </td></tr></table>
        </td>
        </tr>
        </table>
    </form>
</body>
</html>
