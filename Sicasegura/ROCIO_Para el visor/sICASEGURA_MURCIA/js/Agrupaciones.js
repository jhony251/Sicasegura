/*function paraprobar(s, r) {
    alert('hola');
}*/
//var pnlAgrupaciones = new Ext.grid.GridPanel({        
//listeners: { load: paraprobar },


Ext.onReady(function() {
    Ext.QuickTips.init();

    var idSel = document.getElementById("IdSel").value;

    var cancelarRegistro = false;
    var aux = 0;

    var rowsAgrupaciones = new Ext.ux.grid.RowEditor({
        id: 'rowsAgrupaciones',
        saveText: 'Aceptar',
        listeners: {
            canceledit: function(rowsAgrupaciones, record, index, b) {
                if (cancelarRegistro) {
                    rowsAgrupaciones.grid.store.removeAt(rowsAgrupaciones.rowIndex);
                    cancelarRegistro = false;
                } else {
                    return false;
                    cancelarRegistro = false;
                }
            },
            afteredit: function(rowsAgrupaciones, record, index, row) {
                rowsAgrupaciones.stopEditing();
                var resultadoId;

                Ext.Ajax.request({
                    url: 'Agrupaciones_edicion.aspx?idSistema=' + idSel,
                    mirows: rowsAgrupaciones,
                    success: function(response, opts) {
                        var res = Ext.decode(response.responseText);
                        if (res[0]) {
                            Ext.Msg.alert("Aviso", res[0].mensaje);
                            if (res[0].id != undefined) {
                                opts.mirows.grid.store.data.items[row].data.ID = res[0].id;
                                opts.mirows.grid.getView().refresh();
                            }
                        }
                    },
                    failure: function(response, opts) {
                        Ext.Msg.alert("Aviso", "No se ha podido acceder al servidor.");
                    },
                    headers: {
                        'id': 'id', 'CodigoPVYCR': 'CodigoPVYCR', 'EM': 'EM', 'CFD': 'CFD', 'TOC': 'TOC', 'FI': 'FI', 'FF': 'FF'
                    },
                    params: {
                        id: index.data.ID,
                        CodigoPVYCR: index.data.CodigoPVYCR,
                        EM: index.data.EM,
                        CFD: index.data.CFD,
                        TOC: index.data.TOC,
                        FI: index.data.FI,
                        FF: index.data.FF
                    }
                });

                pnlAgrupaciones.store.commitChanges();
                pnlAgrupaciones.getView().refresh();
                cancelarRegistro = false;

            }
        }
    });

    var Registro = Ext.data.Record.create([
            { name: 'ID', type: 'int' },
            { name: 'CodigoPVYCR', type: 'string' },
            { name: 'EM', type: 'string' },
            { name: 'CFD', type: 'string' },
            { name: 'TOC', type: 'string' },
            { name: 'FI', type: 'string' /*type: 'date'*/ },
            { name: 'FF', type: 'string' }
         ])

    var pnlAgrupaciones = new Ext.grid.GridPanel({
        frame: true,
        plugins: [rowsAgrupaciones],
        id: 'pnlAgrupaciones',
        enableDrop: true,
        autoScroll: true,
        //width: 800,
        stripeRows: true,
        deferRowRender: false,
        store: new Ext.data.Store({
            url: 'Agrupaciones_edicion.aspx?idSistema=' + idSel, autoLoad: true, autoSave: true,
            reader: new Ext.data.XmlReader(
            { record: 'Table', idIndex: 0 }, Registro)
        }),
        height: 700,
        enableColumnResize: true,
        enableColumnHide: false,
        style: 'color: #660000',
        enableColumnMove: false,
        view: new Ext.grid.GridView({ markDirty: false }),
        tbar: [/*{ iconCls: 'icon-user-add',
            text: 'Añadir',
            handler: function() {
                var e = new Registro();
                rowsAgrupaciones.stopEditing();
                pnlAgrupaciones.store.insert(0, e);
                pnlAgrupaciones.getView().refresh();
                pnlAgrupaciones.getSelectionModel().selectRow(0);
                rowsAgrupaciones.startEditing(0);
                cancelarRegistro = true;
            }
        }, */{
        ref: '../removeBtn',
        //autoExpandColumn: 'CodigoPVYCR',
        iconCls: 'icon-user-delete',
        text: 'Eliminar',
        disabled: true,
        handler: function() {
            Ext.Msg.show({
                title: 'AVISO',
                msg: 'Deseas eliminar el registro ?',
                layout: 'fit',
                width: 280,
                height: 280,
                icon: Ext.Msg.WARNING,
                buttons: Ext.Msg.OKCANCEL,
                fn: function(btn, text) {
                    var aux = "";
                    if (btn == 'ok') {
                        rowsAgrupaciones.stopEditing();
                        var s = pnlAgrupaciones.getSelectionModel().getSelections();
                        for (var i = 0, r; r = s[i]; i++) {
                            pnlAgrupaciones.store.remove(r);
                            Ext.Ajax.request({
                                url: 'Agrupaciones_edicion.aspx?idSistema=' + idSel + '&borrado=true',
                                success:
                                function(response, opts) {
                                    var res = Ext.decode(response.responseText);
                                    if (res[0]) {
                                        Ext.Msg.alert("Aviso", res[0].mensaje);
                                    }
                                },
                                failure:
                                function(response, opts) {
                                    Ext.Msg.alert("Aviso", "No se ha podido acceder al servidor.");
                                },
                                headers: {
                                    'id': 'id'
                                },
                                params: {
                                    id: r.data.ID
                                }
                            });
                        }
                    }
                }
            });
        }
}],
        columns: [
    new Ext.grid.RowNumberer(),
    { id: 'id', header: 'Id', dataIndex: 'ID', align: 'center', hidden: true },
    { id: 'CodigoPVYCR', header: 'CodigoPVYCR', /*width: 500,*/dataIndex: 'CodigoPVYCR', align: 'center', editor: { xtype: 'textfield', allowBlank: false, blankText: "El campo CodigoPVYCR es requerido"} },
    { id: 'EM', header: 'Elemento de Medida', /*width: 500, */dataIndex: 'EM', align: 'center', editor: { xtype: 'textfield', allowBlank: false, blankText: "El campo Elemento de Medida es requerido"} },
    { id: 'CFD', header: 'Código Fuente Dato', /*width: 500, */dataIndex: 'CFD', align: 'center', editor: { xtype: 'textfield', allowBlank: true} },
    { id: 'TOC', header: 'Tipo Obtención Caudal', /*width: 500, */dataIndex: 'TOC', align: 'center', editor: { xtype: 'textfield', allowBlank: true} },
    /*{ id: 'FI', header: 'Fecha Inicio', dataIndex: 'FI', align: 'center', xtype: 'datecolumn', editor: { xtype: 'datefield', format: 'Y/m/Y'} },
    { id: 'FF', header: 'Fecha Fin', dataIndex: 'FF', align: 'center', editor: { xtype: 'datefield', format: 'd/m/Y', allowBlank: false, blankText: "El campo Fecha Fin es requerido"} */
    {id: 'FI', header: 'Fecha Inicio', dataIndex: 'FI', align: 'center', editor: { xtype: 'textfield',allowBlank: false, blankText: "El campo Fecha Fin es requerido"} },
    { id: 'FF', header: 'Fecha Fin', dataIndex: 'FF', align: 'center', editor: { xtype: 'textfield', allowBlank: false, blankText: "El campo Fecha Fin es requerido"} }
    ]
    });

    pnlAgrupaciones.getSelectionModel().on('selectionchange', function(sm) {
        pnlAgrupaciones.removeBtn.setDisabled(sm.getCount() < 1);
    });

    pnlAgrupaciones.render('grid_agrupaciones');
})

