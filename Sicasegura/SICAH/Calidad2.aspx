<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calidad2.aspx.cs" Inherits="SICAH_Calidad2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>


    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link type="text/css" href="../Styles.css" rel="stylesheet"/>

    
    <link href="../includes/ext-3.3.1/resources/css/xtheme-gray.css" rel="stylesheet" type="text/css" />
    <link href="../includes/ext-3.3.1/resources/css/ext-all.css" rel="stylesheet" type="text/css" />
    <script src="../includes/ext-3.3.1/adapter/ext/ext-base.js" type="text/javascript"></script>
    <script src="../includes/ext-3.3.1/ext-all.js" type="text/javascript"></script>
<%--<link href="../includes/ext-4.0.7-gpl/resources/css/ext-all-gray.css" rel="stylesheet" type="text/css" />
    <link href="../includes/ext-4.0.7-gpl/examples/shared/example.css" rel="stylesheet" type="text/css" />
    <script src="../includes/ext-4.0.7-gpl/bootstrap.js" type="text/javascript"></script>
    <script src="../includes/ext-4.0.7-gpl/ext-all-debug.js" type="text/javascript"></script>
    <script src="../includes/ext-4.0.7-gpl/examples/grid/xml-grid.js" type="text/javascript"></script>--%>
    
    <style type="text/css">
        /* style rows on mouseover */
        .x-grid-row-over .x-grid-cell-inner {
            font-weight: bold;
        }
        /* shared styles for the ActionColumn icons */
        .x-action-col-cell img {
            height: 16px;
            width: 16px;
            cursor: pointer;
        }
        .x-ie6 .x-action-col-cell img {
            position:relative;
            top:-1px;
        }
    </style>
   
</head>
<body bgcolor="#EEEAD2" style="height:100%;">
    <form id="Form1" method="post" runat="server">
    <div style="position:absolute; top:0px; left:50%; margin-left:-450px;">
        <table style="width:900px;" cellspacing="1">
            <tr>
                <td class="tituloLecturas">
                    <asp:Literal ID="LIT_Titulo" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="filtrosCabecera">
                    <table width="100%">
                        <tr>
                            <td width="160px"><b>Periodo de Lecturas</b></td>
                            <td>
                                <asp:Literal ID="LIT_info" runat="server"></asp:Literal>
                            </td>
                            <td>&nbsp;</td>
                            <td width="40px">
                                <asp:Button ID="Button1" runat="server" Text="csv" onclick="Button1_Click" Height="26px" Width="38px" />
                            </td>
                        </tr>
                        <tr>
                            <td><b>Nº de muestras totales</b></td>
                            <td>
                                <asp:Literal ID="LIT_info0" runat="server"></asp:Literal>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td><b>Nº de muestras cargadas</b></td>
                            <td>
                                <asp:Literal ID="LIT_info1" runat="server"></asp:Literal>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="xls" onclick="Button2_Click1" Height="26px" Width="38px"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table style="width:895px;">
                <tr>
                    <td valign="top">
                          <div id="grid-example"></div>      
                    </td>
               </tr>
               <tr>
                    <td valign="top">
                         <iframe frameborder="0" title="X" width="100%" height="280px" scrolling="no" src="Calidad/grafico.aspx?var=conductividad&pvycr=<asp:Literal ID="LIT_PVYCR" runat="server"></asp:Literal>"></iframe>              
                    </td>
                </tr>
        </table>
    </div>
    <asp:Literal ID="Datos" runat="server"></asp:Literal>
    <script language="javascript" type="text/javascript">
            Ext.onReady(function() {
				var pluginExpanded = true;
                Ext.QuickTips.init();
                var store = new Ext.data.ArrayStore({

                    fields: [{ name: 'Fecha', type: 'date', dateFormat: 'j/m/Y G:i:s' },
                            { name: 'PVYCR' },
                            { name: 'Conductividad', type: 'float' },
                            { name: 'Turbidez', type: 'float' },
                            { name: 'Oxígeno', type: 'float' },
                            { name: 'pH', type: 'float' },
                            { name: 'Temperatura', type: 'float'}],
                    data: myData
                });
                var grid = new Ext.grid.GridPanel({
                    store: store,
                    columns: [{ header: 'PVYCR', flex: 1, sortable: false, dataIndex: 'PVYCR' },
                                { header: 'Fecha', sortable: true, renderer: Ext.util.Format.dateRenderer('d/m/Y G:i'), dataIndex: 'Fecha' },
                                { header: 'Conduct. (uS/cm)', sortable: true, dataIndex: 'Conductividad' },
                                { header: 'Turbidez (ppm)', sortable: true, dataIndex: 'Turbidez' },
                                { header: 'Oxígeno (mg/l)', sortable: true, dataIndex: 'Oxígeno' },
                                { header: 'pH', sortable: true, dataIndex: 'pH' },
                                { header: 'Temperatura (C)', sortable: true, renderer: UdsTemp, dataIndex: 'Temperatura'}],
                    height: 325,
                    width: 895,
                    title: 'Datos Calidad',
                    
                   /*bbar: Ext.create('Ext.PagingToolbar', {
						store: store,
						displayInfo: true,
						displayMsg: 'Displaying topics {0} - {1} of {2}',
						emptyMsg: "No topics to display",
						items:[
							'-', {
							text: 'Show Preview',
							pressed: pluginExpanded,
							enableToggle: true,
							toggleHandler: function(btn, pressed) {
								var preview = Ext.getCmp('gv').getPlugin('preview');
								preview.toggleExpanded(pressed);
							}
						}]
					}),*/
                    
                    renderTo: 'grid-example'
                });
                function UdsTemp(val) {
                    if (val > 0) {
                        return '<span style="color:green;">' + val + '</span>';
                    } else if (val < 0) {
                        return '<span style="color:red;">' + val + '</span>';
                    }
                    return val;
                }
            });
        
        </script>
        
    </form>
</body>
</html>