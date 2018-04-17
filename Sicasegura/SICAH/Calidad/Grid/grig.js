Ext.require([
                            'Ext.grid.*',
                            'Ext.data.*',
                            'Ext.util.*',
                            'Ext.state.*'
                        ]);


Ext.onReady(function() {
    Ext.QuickTips.init();
    // setup the state provider, all state information will be saved to a cookie
    Ext.state.Manager.setProvider(Ext.create('Ext.state.CookieProvider'));
    // sample static data for the store


    /**
    * Custom function used for column renderer
    * @param {Object} val
    */
    function change(val) {
        if (val > 0) {
            return '<span style="color:green;">' + val + '</span>';
        } else if (val < 0) {
            return '<span style="color:red;">' + val + '</span>';
        }
        return val;
    }
    /**
    * Custom function used for column renderer
    * @param {Object} val
    */
    function UdsTemp(val) {
        if (val > 0) {
            return '<span style="color:green;">' + val + '</span>';
        } else if (val < 0) {
            return '<span style="color:red;">' + val + '</span>';
        }
        return val;
    }

    // create the data store
    var store = Ext.create('Ext.data.ArrayStore', {
        fields: [
                        { name: 'Fecha', type: 'date', dateFormat: 'j/m/Y G:i:s' },
                        { name: 'PVYCR' },
                        { name: 'Conductividad', type: 'float' },
                        { name: 'Turbidez', type: 'float' },
                        { name: 'Oxígeno', type: 'float' },
                        { name: 'pH', type: 'float' },
                        { name: 'Temperatura', type: 'float' }
                        ],
        data: myData
    });
    // create the Grid
    var grid = Ext.create('Ext.grid.Panel', {
        store: store,
        stateful: true,
        stateId: 'stateGrid',
        columns: [

                                { text: 'PVYCR', flex: 1, sortable: false, dataIndex: 'PVYCR' },
                                { text: 'Fecha', sortable: true, renderer: Ext.util.Format.dateRenderer('d/m/Y G:i'), dataIndex: 'Fecha' },
                                { text: 'Conduct. (uS/cm)', sortable: true, dataIndex: 'Conductividad' },
                                { text: 'Turbidez (ppm)', sortable: true, dataIndex: 'Turbidez' },
                                { text: 'Oxígeno (mg/l)', sortable: true, dataIndex: 'Oxígeno' },
                                { text: 'pH', sortable: true, dataIndex: 'pH' },
                                { text: 'Temperatura (ºC)', sortable: true, renderer: UdsTemp, dataIndex: 'Temperatura' }

                             ],
        height: 400,
        width: 720,
        title: 'Datos de Calidad del Agua',
        renderTo: 'grid-example22',
        viewConfig: { stripeRows: true }
    });
});