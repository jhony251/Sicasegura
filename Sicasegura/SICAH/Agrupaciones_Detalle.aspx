<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agrupaciones_Detalle.aspx.vb" Inherits="SICAH_Agrupaciones_Detalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
        <%--<link href="../includes/ext-3.3.1/resources/css/xtheme-gray.css" rel="stylesheet" type="text/css" />--%>
        <link href="../includes/ext-3.3.1/resources/css/ext-all.css" rel="stylesheet" type="text/css" />
        
        <%--<link href="../styles.css" rel="stylesheet" type="text/css" /> --%>      
        <%--<link href="../estilosAgrupaciones.css" rel="stylesheet" type="text/css" id="theme" /> --%>

        <script src="../includes/ext-3.3.1/adapter/ext/ext-base.js" type="text/javascript"></script>
        <script src="../includes/ext-3.3.1/ext-all-debug-w-comments.js" type="text/javascript"></script>
        <script src="../includes/ext-3.3.1/src/locale/ext-lang-es.js" type="text/javascript"></script>
        
       

        <style type="text/css">
            a{text-decoration:none;}
            /* style rows on mouseover */
            .x-grid-row-over .x-grid-cell-inner {
                font-weight: bold;
            }
            /* shared styles for the ActionColumn icons */
            .x-action-col-cell img {
                height: 26px;
                width: 26px;
                cursor: pointer;
            }
            .graycol{background-color:#EAEAEA;border-color:#EAEAEA;}
            /* custom icon for the "buy" ActionColumn icon */
            .x-action-col-cell img.icon-NoRaacs {
                background-image: url(./agrupaciones/images/arrowred.png);
            }
            /* custom icon for the "alert" ActionColumn icon */
            .x-action-col-cell img.icon-Raacs {
                background-image: url(./agrupaciones/images/arrowgreen.png);
            }
            /* custom icon for the "alert" ActionColumn icon */
            .x-action-col-cell img.icon-RaacsAlert {
                background-image: url(./agrupaciones/images/arrowgreen.png);
            }
            </style>
</head>
<body style="background-color:#ffffff; margin-left:3px;"> 
    <form id="form1" runat="server">
        
        <asp:Panel ID="pnlAgrupacionConInscripcion" Runat="server" Visible="true" Height="327px" Width="98%" >
            <table cellspacing="2" align="center" width="100%" cellpadding="2"  style="background-color:#ffffff;">
                <tr>         
                    <td valign="top">
                        <iframe name="iframeAdmin" id="iframeAdmin" src="Agrupaciones_DatosAdmin.aspx" 
                            height="308" frameborder="0"   marginheight="0" marginwidth="0" 
                            style="width:100%; background-color:#eeead2"  scrolling="no"  ></iframe>
                        
                    </td>    
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PNL_Segunda_y_Tercerafila" runat="server" Width="98%">
            <table width="100%">
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>                  
                        <table id="segundafila" width="100%" cellspacing="0" style="background-color:#cc7722;">
                            <tr style="border:solid 1px black;">
                                <td style="width:30%; font:15px Trebuchet MS,Georgia,Tahoma;">
                                    <asp:Label ID="Label1" runat="server">
                                        <B>
                                            <font color="white">
                                                ALBERCA : Información cartográfica
                                            </font>
                                        </B>
                                    </asp:Label>            
                                </td>
                                <td align="right" style="width:70%; font:15px Trebuchet MS,Georgia,Tahoma;">
                                    <asp:Label ID="LBL_DescargaHoja" runat="server" ></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:label id="lblAgrupacionSel" runat="Server" Visible="true" ></asp:label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <table id="tercerafila" width="100%" cellspacing="0" style="background-color:#668800;">
                            <tr style="border:solid 1px black;">
                                <td style="width:30%; font:15px Trebuchet MS,Georgia,Tahoma;">
                                    <asp:Label ID="Label2" runat="server" >
                                        <B><font color="white">S.I.C.A : Información de consumos</font></B>
                                    </asp:Label>
                                </td>
                                <td align="right" style="font:15px Trebuchet MS,Georgia,Tahoma;">
                                    <font color="white"> Informe de Consumos
                                        <asp:DropDownList ID="DDL_Periodo_Informe" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="DDL_Formato_Informe" runat="server">
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="IMGBTN_DownloadInforma" runat="server" ImageUrl="~/SICAH/Agrupaciones/images/download.gif" />
                                        <asp:Literal runat="server" ID="LIT_Librodig"></asp:Literal>
                                    </font>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table style="border:solid 1px black; background-color:#CCFFAA;" width="100%">
                                        <tr>
                                            <td colspan="2" style="font:10px Trebuchet MS,Georgia,Tahoma;">
                                                <asp:label id="lblPuntosAgrupacion" runat="server" ></asp:label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td  colspan="2">
                                                <hr />
                                            </td>   
                                        </tr>
                                        <tr>
                                            <td valign="top"  style="width:65%;">
                                            
                                                <iframe name="iframeTotales" id="iframeTotales"   
                                                    src="" height="200" frameborder="0" marginheight="0" 
                                                    marginwidth="0" class="HTMframe" style="width: 100%; background-color:#CCFFAA" 
                                                    scrolling="no" ></iframe>
                                           
                                                <!--Fin Panel Editar Contadores -->
                                            </td>
                                            
                                            <td valign="top"  style="width:35%;">
                                                <iframe name="Grafico2" id="Grafico2" frameborder="0" height="200px" 
                                                        scrolling="no" src="" style="background-color:#CCFFAA; width: 100%;"></iframe>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
            </table>
        </asp:Panel>
        
        <asp:Panel ID="PNL_Lista_de_excedidos" runat="server" Visible="false">
            <div id="grid_excedidos" style="margin-left:50px;margin-top:50px;"></div>
            <asp:Literal ID="LIT_Datos_Grid_Excedidos" runat="server"></asp:Literal>
            <script language="javascript" type="text/javascript">
                var icon = ""; //Icono flecha de acceso (verde o roja) a cada inscripción.
                Ext.onReady(function() {
                Ext.BLANK_IMAGE_URL = '../includes/ext-3.3.1/resources/images/default/tree/s.gif';
                
                    var normalize = (function() {
                        var from = "ÃÀÁÄÂÈÉËÊÌÍÏÎÒÓÖÔÙÚÜÛãàáäâèéëêìíïîòóöôùúüûÑñÇç",
                      to = "AAAAAEEEEIIIIOOOOUUUUaaaaaeeeeiiiioooouuuuNncc",
                      mapping = {};

                        for (var i = 0, j = from.length; i < j; i++)
                            mapping[from.charAt(i)] = to.charAt(i);

                        return function(str) {
                            var ret = [];
                            for (var i = 0, j = str.length; i < j; i++) {
                                var c = str.charAt(i);
                                if (mapping.hasOwnProperty(str.charAt(i)))
                                    ret.push(mapping[c]);
                                else
                                    ret.push(c);
                            }
                            return ret.join('');
                            var aux = "";
                        }

                    })();


                    var datos = [['Expr1', 'Insc', 'numero'], ['SEC', 'S', 'texto'], ['TOMO', 'T', 'numero'], ['HOJA', 'H', 'numero'], ['Procedencia', 'Procedencia', 'texto'], ['ACUIF', 'Acuífero', 'palabra'], ['LUGAR', 'LUGAR', 'palabra'], ['TERMI', 'TERMI', 'texto'], ['PROVI', 'PROVI', 'texto'], ['FECINS', 'Fecha Insc.', 'fecha'], ['Concesion', 'Concesión (m3)', 'valor']];

                    var registro = Ext.data.Record.create([
                        { name: 'filtro', type: 'string' },
                        { name: 'filtrosql', type: 'string' }
                    ]);

                    var gridPanelFiltros = new Ext.grid.GridPanel({
                        id: 'gridPanelFiltros',
                        width: 390,
                        height: 200,
                        stripeRows: true,
                        autoScroll: true,
                        store: new Ext.data.Store({

                    }),
                    enableHdMenu: true,
                    view: new Ext.grid.GridView({ markDirty: false }),
                    tbar: [
                             {
                                 text: 'Filtrar Tabla',
                                 handler: function(btnEliminar, b) {
                                     var filtrosServidor = '';
                                     if (gridPanelFiltros.store.data.items.length != 0) {
                                         for (var i = 0; i < gridPanelFiltros.store.data.items.length; i++) {
                                             filtrosServidor += 'and ' + gridPanelFiltros.store.data.items[i].data.filtrosql + ' ';
                                         }
                                         Ext.getCmp('gridDetalle').store.removeAll(true);

                                         if (Ext.getCmp('gridDetalle').getView().grid) Ext.getCmp('gridDetalle').getView().refresh();

                                         Ext.getCmp('gridDetalle').store.load({ params: { 'start': 0, 'limit': 20, 'filtros': filtrosServidor.substring(3)} });
                                     }
                                     else {
                                         Ext.Msg.alert('Aviso', 'Debe añadir algún filtro.');
                                     }

                                 }

                             },
                             {
                                 ref: '../removeBtn',
                                 iconCls: 'icon-user-delete',
                                 text: 'Eliminar Filtro',
                                 disabled: true,
                                 handler: function(btnEliminar, b) {
                                     Ext.Msg.show({
                                         msg: '¿Deseas eliminar el filtro?',
                                         layout: 'fit',
                                         width: 280,
                                         height: 280,
                                         icon: Ext.Msg.WARNING,
                                         buttons: Ext.Msg.OKCANCEL,
                                         fn: function(btn, text) {
                                             if (btn == 'ok') {
                                                 var indiceTotal = btnEliminar.ownerCt.ownerCt.store.data.items.length;
                                                 var s = btnEliminar.ownerCt.ownerCt.getSelectionModel().getSelections();
                                                 for (var m = 0, r; r = s[m]; m++) {
                                                     btnEliminar.ownerCt.ownerCt.store.remove(r);
                                                 }
                                             }
                                         }
                                     });
                                 }
}],
                    columns: [
                                { header: 'Filtro', dataIndex: 'filtro', width: 380, align: 'left', menuDisabled: true },
                                { header: 'FiltroSQL', dataIndex: 'filtrosql', width: 300, align: 'left', hidden: true, menuDisabled: true }
                            ]
                });

                gridPanelFiltros.getSelectionModel().on('selectionchange', function(sm) {
                    this.grid.removeBtn.setDisabled(sm.getCount() < 1);
                });


                var radioCompara = new Ext.form.RadioGroup({
                    xtype: 'radiogroup',
                    width: 150,
                    // Put all controls in a single column with width 100%
                    columns: 3,
                    hidden: true,
                    items: [
                            { boxLabel: '=', name: 'rg', inputValue: '=', checked: true },
                            { boxLabel: '<', name: 'rg', inputValue: '<' },
                            { boxLabel: '>', name: 'rg', inputValue: '>' }
                        ]
                });

                var comboTipo = new Ext.form.ComboBox({
                    id: 'cmb_tipo',
                    mode: 'local',
                    width: 150,
                    editable: true,
                    forceSelection: true,
                    hidden: true,
                    typeAhead: true,
                    store: new Ext.data.JsonStore({
                        url: 'agrupaciones/Obtener_datos.aspx?tipo=combo_tipo',
                        fields: ['id', 'valor']
                    }),
                    valueField: 'id',
                    displayField: 'valor',
                    emptyText: 'Teclee una busqueda...',
                    triggerAction: 'all',
                    lastQuery: ''
                });

                var numeroField = new Ext.form.NumberField({
                    id: 'numero_field',
                    hidden: true,
                    emptyText: 'Introduzca un valor...',
                    width: 150
                });

                var textoField = new Ext.form.TextField({
                    id: 'texto_field',
                    hidden: true,
                    emptyText: 'Teclee una busqueda...',
                    width: 150
                });


                var fechaInicio = new Ext.form.DateField({
                    id: 'fecha_inicio',
                    hidden: true,
                    emptyText: 'Desde: dd/mm/AAAA',
                    format: 'd/m/Y',
                    width: 150
                });

                var fechaFin = new Ext.form.DateField({
                    id: 'fecha_fin',
                    hidden: true,
                    format: 'd/m/Y',
                    emptyText: 'Hasta: dd/mm/AAAA',
                    width: 150
                });

                var botonBuscar = new Ext.Button({
                    title: 'Buscar',
                    id: 'buscar_palabra_grid',
                    //iconCls: 'aceptarReducido',
                    text: 'Buscar',
                    hidden: true,
                    cls: 'btn-negrita',
                    style: { width: '70%', marginRight: '80px' },
                    handler: function(b, e) {
                        var valorNormalizado = normalize(Ext.getCmp('texto_field').getValue()); var m = 0;
                        Ext.getCmp('cmb_tipo').reset();
                        Ext.getCmp('cmb_tipo').store.load({ params: { 'campo': Ext.getCmp('comboCampo').getValue(), 'valor': valorNormalizado} });
                        Ext.getCmp('cmb_tipo').focus();
                    }
                });

                var ventanaFiltros = new Ext.Window({
                    id: 'ventanaFiltros',
                    width: 400,
                    closeAction: 'hide',
                    resizable: false,
                    title: 'Filtros de búsqueda',
                    height: 300,
                    items: [
                            {
                                frame: true,
                                layout: 'table',
                                layoutConfig: { columns: 2 },
                                items: [
                                {
                                    width: 200,
                                    items: [
                                        new Ext.form.ComboBox({
                                            width: 150,
                                            editable: false,
                                            id: 'comboCampo',
                                            emptyText: 'Seleccione un campo...',
                                            store: new Ext.data.ArrayStore({
                                                fields: ['id', 'texto', 'tipo'],
                                                data: datos
                                            }),
                                            valueField: 'id',
                                            displayField: 'texto',
                                            mode: 'local',
                                            forceSelection: true,
                                            triggerAction: 'all',
                                            lastQuery: '',
                                            listeners: {
                                                select: function(a, b) {
                                                    switch (b.data.tipo) {
                                                        case 'numero':
                                                            comboTipo.hide();
                                                            radioCompara.hide();
                                                            numeroField.show();
                                                            textoField.hide();
                                                            botonBuscar.hide();
                                                            fechaInicio.hide();
                                                            fechaFin.hide();
                                                            break;
                                                        case 'texto':
                                                            radioCompara.hide();
                                                            numeroField.hide();
                                                            comboTipo.store.removeAll(true);
                                                            comboTipo.setValue("");
                                                            comboTipo.reset();
                                                            comboTipo.store.load({ params: { 'campo': b.data.id} });
                                                            comboTipo.show();
                                                            textoField.hide();
                                                            botonBuscar.hide();
                                                            fechaInicio.hide();
                                                            fechaFin.hide();
                                                            break;
                                                        case 'palabra':
                                                            radioCompara.hide();
                                                            numeroField.hide();
                                                            textoField.show();
                                                            comboTipo.setValue("");
                                                            comboTipo.store.removeAll(true);
                                                            comboTipo.reset();
                                                            comboTipo.show();
                                                            botonBuscar.show();
                                                            fechaInicio.hide();
                                                            fechaFin.hide();

                                                            break;
                                                        case 'fecha':
                                                            comboTipo.hide();
                                                            radioCompara.hide();
                                                            numeroField.hide();
                                                            textoField.hide();
                                                            botonBuscar.hide();
                                                            fechaInicio.show();
                                                            fechaFin.show();

                                                            break;
                                                        case 'valor':
                                                            comboTipo.hide();
                                                            radioCompara.show();
                                                            numeroField.show();
                                                            textoField.hide();
                                                            botonBuscar.hide();
                                                            fechaInicio.hide();
                                                            fechaFin.hide();
                                                            break;
                                                    }
                                                }
                                            }
                                        }),
                                        {
                                            xtype: 'button',
                                            text: 'Añadir filtro',
                                            handler: function() {


                                                var filtro0; var filtro1;
                                                var valido = false;
                                                if (Ext.getCmp('comboCampo').getValue() == "") {
                                                    Ext.Msg.alert('Aviso', 'Debe seleccionar algún campo.');
                                                }
                                                else {
                                                    var tipo = Ext.getCmp('comboCampo').store.data.items[Ext.getCmp('comboCampo').selectedIndex].data.tipo;
                                                    var campotexto = Ext.getCmp('comboCampo').store.data.items[Ext.getCmp('comboCampo').selectedIndex].data.texto;

                                                    switch (tipo) {
                                                        case "texto":
                                                            if (comboTipo.getValue() != "") {
                                                                filtro0 = campotexto + ' = ' + comboTipo.getValue();
                                                                filtro1 = Ext.getCmp('comboCampo').getValue() + ' like #' + comboTipo.getValue() + '#';
                                                                valido = true;
                                                            }
                                                            else Ext.Msg.alert('Aviso', 'Debe seleccionar algún valor.');
                                                            break;

                                                        case "palabra":
                                                            if (comboTipo.getValue() != "") {
                                                                filtro0 = campotexto + ' = ' + comboTipo.getValue();
                                                                filtro1 = Ext.getCmp('comboCampo').getValue() + ' like #' + comboTipo.getValue() + '#';
                                                                valido = true;
                                                            }
                                                            else Ext.Msg.alert('Aviso', 'Debe seleccionar algún valor.');
                                                            break;

                                                        case "numero":
                                                            if (numeroField.getValue() != "") {
                                                                filtro0 = campotexto + ' = ' + numeroField.getValue();
                                                                filtro1 = Ext.getCmp('comboCampo').getValue() + '=' + numeroField.getValue();
                                                                valido = true;
                                                            }
                                                            else Ext.Msg.alert('Aviso', 'Debe introducir algún valor.');
                                                            break;

                                                        case 'valor':
                                                            if (numeroField.getValue() != "") {
                                                                filtro0 = campotexto + ' ' + radioCompara.getValue().inputValue + ' ' + numeroField.getValue();
                                                                filtro1 = Ext.getCmp('comboCampo').getValue() + radioCompara.getValue().inputValue + numeroField.getValue();
                                                                valido = true;
                                                            }
                                                            else Ext.Msg.alert('Aviso', 'Debe introducir algún valor.');
                                                            break;

                                                        case 'fecha':
                                                            if (fechaFin.getValue() != "" && fechaInicio.getValue() != "") {
                                                                var fechanueva = new Date(fechaInicio.getValue().getFullYear(), fechaInicio.getValue().getMonth(), fechaInicio.getValue().getDate());
                                                                var fechanueva2 = new Date(fechaFin.getValue().getFullYear(), fechaFin.getValue().getMonth(), fechaFin.getValue().getDate());
                                                                filtro0 = campotexto + ' desde ' + fechanueva.format('d/m/Y') + ' hasta ' + fechanueva2.format('d/m/Y');
                                                                filtro1 = Ext.getCmp('comboCampo').getValue() + ' between #' + fechanueva.format('d/m/Y') + '# and #' + fechanueva2.format('d/m/Y') + '#';
                                                                valido = true;
                                                            }
                                                            else Ext.Msg.alert('Aviso', 'Debe elegir una fecha de inicio y una fecha de fin.');
                                                            break;

                                                    }
                                                    if (valido) {
                                                        var e = new registro({ filtro: filtro0, filtrosql: filtro1 });
                                                        gridPanelFiltros.store.add(e);
                                                        gridPanelFiltros.getView().refresh();
                                                    }
                                                }

                                            }
                                        }

                                    ]

                                },

                                {
                                    width: 200,
                                    items: [
                                        textoField,
                                        fechaInicio,
                                        fechaFin,
                                        comboTipo,
                                        radioCompara,
                                        numeroField,
                                        botonBuscar
                                   ]
                                }
                            ]
                            },

                            {
                                items: [gridPanelFiltros]
                            }
                        ]
                });

                var store = new Ext.data.JsonStore({
                    root: 'rows',
                    totalProperty: 'results',
                    idProperty: 'Expr1',
                    remoteSort: true,
                    fields: [
                                    { name: 'Fecha_Inscripcion' },
                                    { name: 'Procedencia' },
                                    { name: 'SEC' },
                                    { name: 'TOMO' },
                                    { name: 'HOJA' },
                                    { name: 'ACUIF' },
                                    { name: 'LUGAR' },
                                    { name: 'TERMI' },
                                    { name: 'PROVI' },
                                    { name: 'Expr1', type: 'int' },
                                    { name: 'Concesion', type: 'float' },
                                    { name: 'Consumido', type: 'float' },
                                    { name: 'SICA' },
                                    { name: 'Exceso', type: 'float'}],
                    url: 'agrupaciones/Obtener_datos.aspx'
                });
                store.setDefaultSort('Expr1', 'asc');

                
                var InSICA = "";
                var grid = new Ext.grid.GridPanel({

                    id: 'gridDetalle',
                    store: store,
                    stateful: true,
                    collapsible: true,
                    multiSelect: true,
                    columns: [
                                        { id: 'col0', header: 'Insc', sortable: true, dataIndex: 'Expr1', width: 50 },
                                        { header: 'S', sortable: true, dataIndex: 'SEC', width: 30, renderer: function(value, metaData, record, rowIndex, colIndex, store) { metaData.css = 'graycol'; return value; } },
                                        { header: 'T', sortable: true, dataIndex: 'TOMO', width: 30 },
                                        { header: 'H', sortable: true, dataIndex: 'HOJA', width: 35, renderer: function(value, metaData, record, rowIndex, colIndex, store) { metaData.css = 'graycol'; return value; } },
                                        { header: 'Procedencia', sortable: true, dataIndex: 'Procedencia' },
                                        { header: 'Acuífero', sortable: true, dataIndex: 'ACUIF' },
                                        { header: 'LUGAR', sortable: true, dataIndex: 'LUGAR' },
                                        { header: 'TERMI', sortable: true, dataIndex: 'TERMI' },
                                        { header: 'PROVI', sortable: true, dataIndex: 'PROVI', width: 50 },
                                        { header: 'Fecha Insc.', sortable: true, width: 100, dataIndex: 'Fecha_Inscripcion' },
                                        { header: 'Concesión (m3)', sortable: true, dataIndex: 'Concesion' },
                                        { header: 'Consumido (m3)', sortable: false, dataIndex: 'Consumido' },
                                        { header: '%', sortable: false, dataIndex: 'Exceso', width: 30 },
                                        {
                                            xtype: 'actioncolumn',
                                            items: [{
                                                // icon 0
                                                //iconCls: 'icon-NoRaacs',
                                                tooltip: 'Información del aprovechamiento',
                                                getClass: function(value, meta, record, rowIdx, colIdx) {
                                                    var rec = store.getAt(rowIdx);

                                                    if (rec.json.SICA == "NO") {
                                                        //this.items[1].tooltip = 'Información del aprovechamiento';
                                                        return 'icon-NoRaacs';
                                                    }
                                                    else {
                                                        //this.items[1].tooltip = 'Información del Aprovechamiento, Alberca y SICA';
                                                        return 'icon-Raacs';
                                                    }
                                                },
                                                handler: function(grid, rowIndex, colIndex) {
                                                    var rec = store.getAt(rowIndex);
                                                    //alert("Sell " + rec.id);
                                                    if (rec.json.SICA != "NO") {
                                                        document.location = 'agrupaciones_detalle.aspx?nombre=' + rec.id + '&RAAC=1&idAgrupacion=' + rec.json.SICA;
                                                        InSICA = "./agrupaciones/images/sica.png";
                                                    }
                                                    else { document.location = 'agrupaciones_detalle.aspx?nombre=' + rec.id + '&idAgrupacion=' + rec.id; }
                                                    //document.location = 'agrupaciones_detalle.aspx?nombre=2000&idAgrupacion=2000';

                                                }
}]
                                            }
                           ],
                    title: 'Listado de aprovechamientos vigentes',

                    // paging bar on the bottom
                    bbar: new Ext.PagingToolbar({

                        pageSize: 20,
                        store: store,
                        displayInfo: true,
                        displayMsg: 'Displaying topics {0} - {1} of {2}',
                        emptyMsg: "No topics to display",
                        items: ['-',
                                { text: 'Filtros', id: 'botonFiltros', frame: true, handler: function() { ventanaFiltros.show(); } },
                                { text: 'Borrar Filtros', id: 'botonBorrarFiltros', frame: true, handler: function() {  Ext.getCmp('gridDetalle').store.removeAll(true); 
                                                                                                                        if (Ext.getCmp('gridDetalle').getView().grid)   
                                                                                                                            Ext.getCmp('gridDetalle').getView().refresh();
                                                                                                                        Ext.getCmp('gridDetalle').store.load({ params: { 'start': 0, 'limit': 20, 'filtros': 'BORRAFILTROS'} });
                                                                                                                      } }
                                ]
                        }),


                        viewConfig: {
                            stripeRows: true,
                            enableTextSelection: true
                        }
                    });
                    var vp = new Ext.Viewport({
                        id: 'container',
                        layout: 'border',
                        items: [{
                            layout: 'fit',
                            region: 'center',
                            border: false,
                            items: grid
}],
                            renderTo: 'grid_excedidos'
                        });
                        store.load({ params: { start: 0, limit: 20} });
                    });
                    
                    
                    
                    
                    function CallMethod(src, dest) {
                        //var ctrl = document.getElementById(src);
                        // call server side method
                        PageMethods.GetColorArrow(src, CallSuccess, CallFailed, dest);
                    }
                    // set the destination textbox value with the ContactName
                    function CallSuccess(res, destCtrl) {
                        //var dest = document.getElementById(destCtrl);
                        this.icon = res;
                    }
                    // alert message on some failure
                    function CallFailed(res, destCtrl) {
                        alert(res.get_message());
                    }
                </script>    
        </asp:Panel>
        <%--Para poder hacer llamadas a C# desde javascrip--%>   
    </form>
</body>
</html>
