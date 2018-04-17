<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="SICA_Gestion_Visitas_Campo_index" ValidateRequest=false EnableEventValidation="true"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="shortcut icon" href="http://www.chsegura.es/home/images/favicon.ico" />
        <!--<link rel="stylesheet" href="home/css/style.css">-->
        <link rel="stylesheet" href="https://www.chsegura.es/export/system/modules/es.chsegura.www/resources/stylesheet_opt.css" />
        <link rel="stylesheet" href="https://www.chsegura.es/export/system/modules/es.chsegura.www/resources/theme.css" />
        <script src="https://www.chsegura.es/export/system/modules/es.chsegura.www/resources/utiles.js"></script>
        <script src="https://www.chsegura.es/export/system/modules/es.chsegura.www/resources/menu.js"></script>
        <script src="https://www.chsegura.es/export/system/modules/es.chsegura.www/resources/ddmenu.js"></script>
        <script src="js/jquery.js"></script>
    </head>
<body>
    <form id="form1" runat="server">
         <div id="container">
                <div id="container2">
                    <asp:Literal ID="HTML_Links_Sup_Izq"     runat="server"></asp:Literal> 
                    <asp:Literal ID="HTML_Subcabecera_Logos" runat="server"></asp:Literal>
                    <asp:Literal ID="HTML_Menu_Navegacion"   runat="server"></asp:Literal>
                    <div id="content-wrapper">                    
                        <div id="left-column">
                                <ul class="nav-menu">
                                    <li><asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Añadir visita a Campo</asp:LinkButton></li>
                                    <li>
                                        <a>Por Estado</a>
                                        <ul>
                                            <asp:Literal ID="HTML_Link_visitas_pendientes" runat="server"></asp:Literal>
                                            <asp:Literal ID="HTML_Link_visitas_finalizadas" runat="server"></asp:Literal>
                                        </ul>    
                                    </li>
                                     <li>
                                        <a>Por Fecha</a>
                                        <ul>
                                            <asp:Literal ID="HTML_Link_Primeras_En_Finalizar_primero" runat="server"></asp:Literal>
                                            <asp:Literal ID="HTML_Link_Ultimas_En_Finalizar_priemro" runat="server"></asp:Literal>
                                            <asp:Literal ID="HTML_Link_Sin_fecha_asignada" runat="server"></asp:Literal>
                                        </ul>    
                                    </li>
                                </ul>    
                        </div>
                   
                        <div id="central-content"  >
                            <div class="table">
                                <asp:Panel ID="PNL_nueva_visita" runat="server">
                                    <table>
                                        <tr>
                                            <td><asp:DropDownList ID="DDL_EXP_ISM" runat="server" AutoPostBack="True" CausesValidation="True" Height="16px" Width="100px"></asp:DropDownList>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                Personal CHS<br />
                                                <asp:TextBox ID="TXT_PersonalCHS" runat="server" CausesValidation="True"></asp:TextBox>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                Personal UTE<br />
                                                <asp:TextBox ID="TXT_PersonalUTE" runat="server" CausesValidation="True"></asp:TextBox>
                                            </td>
                                            <td><asp:CheckBoxList ID="CBO_TIPO" runat="server" CausesValidation="True">
                                                    <asp:ListItem Value="0">Caracterización</asp:ListItem>
                                                    <asp:ListItem Value="1">Verificación</asp:ListItem>
                                                    <asp:ListItem Value="2">Valid. Telemedida</asp:ListItem>
                                                    <asp:ListItem Value="3">Acta Confrontación</asp:ListItem>
                                                    <asp:ListItem Value="4">Otros</asp:ListItem>
                                                </asp:CheckBoxList>
                                            
                                            </td>
                                            <td>
                                                Contacto
                                                <br /><asp:TextBox ID="TXT_Contacto" runat="server" CausesValidation="True" Height="65px" TextMode="MultiLine" Width="243px"></asp:TextBox>
                                                <br />
                                                <br />
                                                Observaciones<br />
                                                <asp:TextBox ID="TXT_Observaciones" runat="server" CausesValidation="True" Height="65px" TextMode="MultiLine" Width="243px"></asp:TextBox>
                                            </td>
                                           
                                            <td>
                                                <asp:Calendar ID="Calendar1" runat="server" Height="141px" Width="94px"></asp:Calendar>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="Button1" runat="server" Text="Añadir" OnClick="Button1_Click" />

                                                &nbsp;
                                                <asp:Button ID="BTN_Cancelar_nuevo" runat="server" OnClick="BTN_Cancelar_nuevo_Click" Text="Cancelar" />

                                            </td>
                                        </tr>
                                    </table>                        
                                </asp:Panel>
                                <div>
                                     <asp:Panel ID="PNL_Update" runat="server" Visible="False" >
                                        <table>
                                            <tr>
                                                <td>Actualizando registro</td>
                                            </tr>
                                            <tr>
                                                <td valign="top"><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br /><br /><br /><br />
                                                    Expediente ISM<br />
                                                    <asp:TextBox ID="TXT_EXP_ISM_Modificacion" runat="server" CausesValidation="True" Width="100px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    Personal CHS<br />
                                                            
                                                    <asp:TextBox ID="TXT_PersonalCHS_UPDATE" runat="server" CausesValidation="True"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    Personal UTE<br />
                                                    <asp:TextBox ID="TXT_PersonalUTE_UPDATE" runat="server" CausesValidation="True"></asp:TextBox>
                                                    <br />
                                                            
                                                </td>
                                                <td>Tipo de visita<br />
                                                    <asp:DropDownList ID="DDL_Tipo_Visita_Update" runat="server">
                                                        <asp:ListItem Value="0">Caracterizacion</asp:ListItem>
                                                        <asp:ListItem Value="1">Verificación</asp:ListItem>
                                                        <asp:ListItem Value="2">Valid. Telemedida</asp:ListItem>
                                                        <asp:ListItem Value="3">Acta Confrontación</asp:ListItem>
                                                        <asp:ListItem Value="4">Otros</asp:ListItem>
                                                    </asp:DropDownList>
                                            
                                                </td>
                                                <td>
                                                    Contacto
                                                    <br /><asp:TextBox ID="TXT_Contacto_update" runat="server" CausesValidation="True" Height="90px" TextMode="MultiLine" Width="243px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    Observaciones
                                                    <br />
                                                    <asp:TextBox ID="TXT_Observaciones_Update" runat="server" CausesValidation="True" Height="90px" TextMode="MultiLine" Width="243px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Calendar ID="Calendar2" runat="server" Height="140px" Width="115px"></asp:Calendar>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BTN_UPDATE" runat="server" Text="Actualizar" OnClick="BTN_UPDATE_Click" />

                                                    &nbsp;
                                                    <asp:Button ID="BTN_Cancelar_Update" runat="server" OnClick="BTN_Cancelar_Update_Click" Text="Cancelar" />

                                                </td>
                                            </tr>
                                        </table>

                                    </asp:Panel>
                                </div>
                                <asp:Panel ID="Panel1" runat="server" Visible="false" height="30px">
                                    
                                                <asp:Literal ID="LIT_Filtro_ISM" runat="server"></asp:Literal>
                                                <asp:Literal ID="LIT_Filtro_Tipo" runat="server"></asp:Literal>
                                                <asp:Literal ID="LIT_Filtro_fecha" runat="server"></asp:Literal>
                                           
                                </asp:Panel>
                                
                                <div style="overflow-y: scroll;overflow-x: hidden; height:500px;">
                                    <asp:Literal ID="LIT_Filtro_Activo" runat="server"></asp:Literal>
                                    <asp:GridView ID="DGV_Visitas" runat="server" AllowSorting="True" EnableSortingAndPagingCallbacks="False"  AutoGenerateEditButton="false" 
                                        OnRowEditing="DGV_Visitas_RowEditing" 
                                        OnSelectedIndexChanged="DGV_Visitas_SelectedIndexChanged" 
                                        OnRowDeleting="DGV_Visitas_RowDeleting" 
                                        OnRowCommand="DGV_Visitas_RowCommand" OnRowDataBound="DGV_Visitas_RowDataBound" 
                                        >
                                        <AlternatingRowStyle BackColor="#99CCFF" />
                                        <PagerStyle BorderStyle="None" BorderWidth="0px" HorizontalAlign="Justify" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" ItemStyle-Width="38px" ControlStyle-Height="18px"  HeaderText="Comandos"
                                             DeleteImageUrl="../images/borrar.png" ShowDeleteButton="True"  
                                             EditImageUrl="../images/edit.png" ShowEditButton="True" 
                                             SelectImageUrl="../images/filter.png"  ShowSelectButton="true" />
                                        </Columns>
                                    </asp:GridView>
                                </div>                          
                            </div>
                        </div>
                    </div>
                   <asp:Literal ID="HTML_Pie_Logo_Corporativo" runat="server"></asp:Literal>
                   
                </div>      
            </div>
            <asp:Literal ID="HTML_Pie_pagina" runat="server"></asp:Literal>
            
    	    
    </form>
</body>
</html>
