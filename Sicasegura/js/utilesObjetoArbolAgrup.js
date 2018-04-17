// JScript File
var busqueda = 'n';
var contador = 0;
//funcion que busca el nodo seleccionado desde el panel de busqueda de caucepuntBuscar.aspx

function expandirArbol(nodo){
    //var nodoABuscar = nodo.split(";");
    Ext.Ajax.request({
        url: 'DespliegueArbolAgrup.aspx?nodoBusqueda=s&valor=' + nodo,
        success: function(res, req) {
            var arbol = Ext.getCmp('pnlArbol');
            if (res.responseText != 'sinPermisos')
                var path = Ext.util.JSON.decode(res.responseText).path.split('/');           
            //tarea programada que comprueba si el arbol está totalmente cargado, para que me funcione la búsqueda de un nodo desde infoPtoControl
            Ext.TaskMgr.start({
                //la t es una variable que nos sirve para parar, en caso de que no se cargue el árbol por cualquier motivo y no se quede un bucle infinito
                run: function(t) {
                    if (arbolCargado) {
                        expandirHastaNodo(arbol, path, 0);
                        t.stop();
                    } else {
                        contador++;
                        if (contador > 10) t.stop(); // si a los diez segundos no se ha cargado el árbol que pare la búsqueda
                    }
                }
                ,
                interval: 1000 //1 seg, son milisegundos
            });

        },
        failure: function(res, req) {
            Ext.Msg.alert('Información', 'Nodo no encontrado');
        }
    });                   
}
//funcion que expande la rama del árbol que estamos buscando desde la busqueda del json que ahora está comentada
function expandirHastaNodo(arbol, path, indice) {
    //la variable path contiene la ruta raiz/nodo1/nodo2...
    if (path[indice]!=null) {
        var rama = arbol.getNodeById(path[indice]);
        if (rama!=null) {
            indice++;
            if(path[indice]==null)
                //si es el nodo buscado, es decir, el último del path, lo seleccionamos pero no lo expandimos
                rama.select();
            else        
                //si no es el nodo buscado, expandimos la rama
                rama.expand(false,true,function(){expandirHastaNodo(arbol, path, indice);});
        }else
            Ext.Msg.alert('Información','Nodo no encontrado');  //rama
    }
}
var arbolCargado=false;
function cargarPanelArbol(tam) {


    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }

    




    
    Ext.BLANK_IMAGE_URL = '../includes/ext-3.3.1/resources/images/default/tree/s.gif';
    var Arbol = new Ext.tree.TreePanel({
    dataUrl: 'DespliegueArbolAgrup.aspx?nodoBusqueda=n&valor=0',
        root: {
            nodeType: 'async',
            id: 'raiz',
            listeners: {
                expand: function() {
                    arbolCargado = true;
                }
            }
        },
        id: 'pnlArbol',
        text: 'sica',
        useArrows: true,
        autoHeight: false,
        autoScroll: true,
        enableDD: false,
        enableDrag: true,
        ddGroup: 'treeDDGroup',
        animate: true,
        
        rootVisible: false,
        frame: false,
        listeners: {
            beforeexpandnode: function() {
                if (Ext.getCmp('pnlAgrupaciones') != null) {
                    dragdrop(Ext.getCmp('pnlAgrupaciones'));
                }
            }
            
        },
        border: false,
        height: myHeight-140,       
        rootVisible: false
    });
    var pnlArbol = new Ext.Panel({
        renderTo: 'west',
        autoHeight: false,
        titleCollapse: true,
        border: false,
        bodyStyle: 'padding:0px;',
        listeners: { resize: { fn: function(el) { pnlArbol.doLayout(); } } },
        items: [Arbol]
    });
    Ext.EventManager.onWindowResize(function(w, h) {
           
    });
 
      
   };
 function cargarPanelBusqueda() {
 var pnlBusqueda = new Ext.Panel({  
     title: 'Búsquedas',
     id:'pnlBusqueda',
     renderTo: 'ContenedorBusqueda',
     url: 'DespliegueArbolAgrup.aspx?nodoBusqueda=s',
     border:false,
     height:50,
     titleCollapse: true,
         items: [{
            layout:'column',
            border:false,
            items:[{
                columnWidth:0.90,
                border:false,
                xtype:'textfield',
                name: 'Buscar',
                id:'txtBuscar',
                value:'Escriba aquí el nodo a buscar',
                cls:'textoB'
            },{
                columnWidth:.10,
                border:true,
                xtype:'button',
                //text: 'buscar',                           
                id:'btnBuscar',
                cls:'imagenB',
                handler: function(btnB) {                                    
                    var valor = Ext.getCmp('txtBuscar').getValue(); 
                    Ext.Ajax.request({
                    url: 'DespliegueArbolAgrup.aspx?nodoBusqueda=s&valor=' + valor,
                        success: function(res,req) {
                            var arbol = Ext.getCmp('pnlArbol');
                            if (res.responseText != 'sinPermisos')
                                var path = Ext.util.JSON.decode(res.responseText).path.split('/'); 
                                expandirHastaNodo(arbol, path,0);
                        },
                        failure: function(res,req) {
                            Ext.Msg.alert('Información','Nodo no encontrado');  
                        }                        
                    });
                }   	        
            }]
        }]
 });  
};
//Ext.onReady(cargarPanelBusqueda);
Ext.onReady(cargarPanelArbol);


