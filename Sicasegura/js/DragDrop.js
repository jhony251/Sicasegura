function dragdrop(panel) {
    //Si no existe se crea drop zone              
    var detailsPanel = panel;
    var pnlDropTargetEl = detailsPanel.body;
    var pnlDropTarget = new Ext.dd.DropTarget(pnlDropTargetEl, {
        ddGroup: 'treeDDGroup',
        id: 'hecho',
        copy: false,
        notifyDrop: function(ddSource, e, data) {
            if (data.node.attributes.href != null) {
                var inicio = data.node.attributes.href.indexOf(";");
                var fin = data.node.attributes.href.substr(inicio).indexOf("&");
                //alert(data.node.attributes.href.substr(inicio + 1, fin - 1));
                //alert(data.node.attributes.href);

                if (data.node.attributes.href.substr(inicio + 1, fin - 1) == "P") {
                    var inicio2 = data.node.attributes.href.indexOf("&");
                    var fin2 = data.node.attributes.href.substr(inicio2).indexOf(";");
                    var strCodigoPVYCR = data.node.attributes.href.substr(inicio2 + 1, fin2 - 1).replace("nodoclave=", "");
                    var fecha = new Date();
                    var fecha2 = new Date(fecha.getFullYear() + 1, fecha.getMonth(), fecha.getDate());

                    var Registro = Ext.data.Record.create([
                    { name: 'ID', type: 'int' },
                    { name: 'CodigoPVYCR', type: 'string' },
                    { name: 'EM', type: 'string' },
                    { name: 'CFD', type: 'string' },
                    { name: 'TOC', type: 'string' },
                    { name: 'FI', type: 'datetime' },
                    { name: 'FF', type: 'datetime' }
                    ])
                    var e = new Registro();
                    //alert(Ext.getCmp('rowsAgrupaciones').id);
                    Ext.getCmp('rowsAgrupaciones').stopEditing();
                    detailsPanel.store.insert(0, e);
                    detailsPanel.getView().refresh();
                    detailsPanel.getSelectionModel().selectRow(0);
                    /*Ext.getCmp('rowsAgrupaciones').startEditing(0);
                    cancelarRegistro = true;*/
                    
                    Ext.getCmp('rowsAgrupaciones').stopEditing();
                    var idSel = document.getElementById("IdSel").value;
                    Ext.Ajax.request({
                        url: 'Agrupaciones_edicion.aspx?idSistema=' + idSel,
                        mirows: Ext.getCmp('rowsAgrupaciones'),
                        success: function(response, opts) {
                            var res = Ext.decode(response.responseText);
                            if (res[0]) {
                                Ext.Msg.alert("Aviso", res[0].mensaje);
                                if (res[0].id != undefined) {                                    
                                    opts.mirows.grid.store.data.items[0].data.ID = res[0].id;
                                    opts.mirows.grid.store.data.items[0].data.CodigoPVYCR = strCodigoPVYCR;
                                    opts.mirows.grid.store.data.items[0].data.EM = 'S';
                                    opts.mirows.grid.store.data.items[0].data.FI = fecha.getDate() + "/" + (fecha.getMonth() + 1) + "/" + fecha.getFullYear();
                                    opts.mirows.grid.store.data.items[0].data.FF = fecha2.getDate() + "/" + (fecha2.getMonth() + 1) + "/" + fecha2.getFullYear();
                                    opts.mirows.grid.getView().refresh();
                                }
                            }
                        },
                        failure: function(response, opts) {
                            Ext.Msg.alert("Aviso", "No se ha podido acceder al servidor.");
                        },
                        headers: {
                            'id': 'id', 'CodigoPVYCR': 'CodigoPVYCR', 'EM': 'EM', 'FI': 'FI', 'FF': 'FF'
                        },
                        params: {
                            //id: 10,
                            CodigoPVYCR: strCodigoPVYCR,
                            EM: 'S',
                            FI: fecha.getDate() + "/" + (fecha.getMonth() + 1) + "/" + fecha.getFullYear(),
                            FF: fecha2.getDate() + "/" + (fecha2.getMonth() + 1) + "/" + fecha2.getFullYear()
                        }
                    });

                    detailsPanel.store.commitChanges();
                    detailsPanel.getView().refresh();
                    cancelarRegistro = false;

                    return true;
                } else { return false; }
            }
        }
    });
    //fin drop
}