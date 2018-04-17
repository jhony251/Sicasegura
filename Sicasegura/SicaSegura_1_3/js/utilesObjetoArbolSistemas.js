﻿// JScript File
var busqueda = 'n';
var contador = 0;
//funcion que busca el nodo seleccionado desde el panel de busqueda de caucepuntBuscar.aspx
function expandirArbol(nodo){
    //var nodoABuscar = nodo.split(";");
    Ext.Ajax.request({
        url: 'DespliegueArbol.aspx?nodoBusqueda=s&valor=' + nodo,
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
function cargarPanelArbol() {

    var Arbol = new Ext.tree.TreePanel({
        dataUrl: 'DespliegueArbol.aspx?nodoBusqueda=n&valor=0',
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
        useArrows: false,
        autoHeight: false,
        autoScroll: true,
        enableDD: false,
        enableDrag: true,
        ddGroup: 'treeDDGroup',
        animate: true,
        listeners: {
            beforeexpandnode: function() {
                if (Ext.getCmp('pnlAgrupaciones') != null) {
                    dragdrop(Ext.getCmp('pnlAgrupaciones'));
                }
            }
        },
        border: true,
        height: 694,
        width: 248,
        overflow: scroll,
        rootVisible: false
    });
 var pnlArbol = new Ext.Panel({  
     renderTo: 'ContenedorArbol',
     autoHeight:true,
     titleCollapse: true, 
     border: false, 
     bodyStyle: 'padding:0px;',
     items: [Arbol]
 });  
      
   };
 function cargarPanelBusqueda() {
 var pnlBusqueda = new Ext.Panel({  
     title: 'Búsquedas',
     id:'pnlBusqueda',
     renderTo: 'ContenedorBusqueda',
     url: 'DespliegueArbol.aspx?nodoBusqueda=s',
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
                    url: 'DespliegueArbol.aspx?nodoBusqueda=s&valor=' + valor,
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

