
Ext.application({

name: 'Meteo', 
launch: function(){ 

	var ACTUAL_PANEL = 0; 
	var ACTUAL_PANELDATOS = 0; 
	var estacionSel = -1;
	var opcionSel = -1;
	 
	var estaciones = new Array('Arguellite', 'Elche de la Sierra', 'El Carche', 'Majal Blanco','Miller', 'Ontur');
	var codEstaciones = new Array('arguellite', 'elche', 'elcarche','majalblanco','miller','ontur');
	
	var latitudEstaciones = new Array(38.335677630703344,  38.449853587398344,  38.444697000000000,  37.879983000000000,  38.222345207276430,  38.616863812440920);
	var longitudEstaciones = new Array(-2.4298477505193320, -2.0427946234558996, -1.1688080000000000, -1.2090210000000000, -2.4599339678293550, -1.4964455195866644);
	
	var opciones = new Array('Tiempo Real','Ubicación','Datos resumen');
	 
	var itemsEstaciones = new Array();
	var itemsOpciones = new Array();

		

				
	/*******************
	 * ESTACIONES
	 * *****************/	
	for (i=0;i<estaciones.length;i++) {

		itemsEstaciones[i] = {	 
								xtype: 'button', 
								text: estaciones[i],
								ui: 'action',
								style: 'background-color:#1111AA; text-align:center; margin:1px 1px 1px 1px; padding:10px 5px 10px 5px;',
								listeners:{ 
									tap: function(btn){ 										
										navigate(1);
										Ext.getCmp('botonEstacionOp').setText(btn.getText());
										for (j=0;j<estaciones.length;j++) if (estaciones[j] == btn.getText()) estacionSel = j;										
										} 
								}
							}

	 
	}

    
	/*******************
	 * OPCIONES
	 * *****************/

	// Tiempo real
	itemsOpciones[0] = {	 
							xtype: 'button', 
							text: opciones[0],
							ui: 'action',
							style: 'background-color:#1111AA; text-align:center; margin:1px 1px 1px 1px; padding:10px 5px 10px 5px;',	
							listeners:{ 
								tap: function(btn){ 										
									Ext.getCmp('botonEstacionD').setText(Ext.getCmp('botonEstacionOp').getText());
									Ext.getCmp('botonOpcionD').setText(btn.getText());										
									for (j=0;j<opciones.length;j++) if (opciones[j] == btn.getText()) opcionSel = j;
									navigate(2);
									navigateDatos(opcionSel);
									obtenerDatosRealesEstacion();
																			
									} 
							}
						};

 

	// Ubicación
	itemsOpciones[1] = {	 
							xtype: 'button', 
							text: opciones[1],
							ui: 'action',
							style: 'background-color:#1111AA; text-align:center; margin:1px 1px 1px 1px; padding:10px 5px 10px 5px;',	
							listeners:{ 
								tap: function(btn){ 										
									Ext.getCmp('botonEstacionD').setText(Ext.getCmp('botonEstacionOp').getText());
									Ext.getCmp('botonOpcionD').setText(btn.getText());										
									for (j=0;j<opciones.length;j++) if (opciones[j] == btn.getText()) opcionSel = j;
									navigate(2);
									navigateDatos(opcionSel);										
									
									// espero un poquito para que se vea correctamente (de otra forma hace cosas raras)
									var task = Ext.create('Ext.util.DelayedTask', function () {
										Ext.getCmp('botonActualizarD').doTap(Ext.getCmp('botonActualizarD'), Ext.EventObject());
									});									
									task.delay(250);
									
								} 
							}
						};
							

  
	// Datos resumen
	itemsOpciones[2] = {	 
							xtype: 'button', 
							text: opciones[2],
							ui: 'action',
							style: 'background-color:#1111AA; text-align:center; margin:1px 1px 1px 1px; padding:10px 5px 10px 5px;',	
							listeners:{ 
								tap: function(btn){ 										
									Ext.getCmp('botonEstacionD').setText(Ext.getCmp('botonEstacionOp').getText());
									Ext.getCmp('botonOpcionD').setText(btn.getText());										
									for (j=0;j<opciones.length;j++) if (opciones[j] == btn.getText()) opcionSel = j;
									navigate(2);
									navigateDatos(opcionSel);	
									pDatosResumen.getStore().getProxy().setUrl('../getInfoStation.aspx?action=getdata24JSON&Station=' + codEstaciones[estacionSel]);									
									//pDatosResumen.setTpl(tlpAux);
									pDatosResumen.getStore().load();	
									
									//console.log(pDatosResumen.getHtml());
									//console.log('-->');

									// espero un poquito para que se vea correctamente (de otra forma hace cosas raras)
									var task = Ext.create('Ext.util.DelayedTask', function () {
										  // Loop through the data array 
										  var data = pDatosResumen.getStore().getRange();
										  var showData = new Array(); 
										  for(var i=0;i<data.length;i++){
											  showData.push(data[i].data); 
										  }
  
										//console.log(showData);
										pDatosResumen.setHtml(tlpAux.applyTemplate(showData));
									});									
									//task.delay(1000);
									

									
									//console.log(tlpAux.apply(pDatosResumen.getStore().getRange(1,288)));
									//pDatosResumen.updateHtml(tlpAux.applyTemplate(pDatosResumen.getStore().getRange(1,288)));
									
									//pDatosResumen.updateHtml('<table> <tr><td>az </td><td> az </td><td> az </td></tr>'  + pDatosResumen.getHtml() + '</table>');
									//pDatosResumen.refresh();
 
  
								} 
							}
						};  
  	/*******************
	 * FUNCIONES
	 * *****************/
	 var navigate = function(subpanel){ 
		 ACTUAL_PANEL = subpanel; 
		 Ext.getCmp('panelPrincipal').setActiveItem(ACTUAL_PANEL); 	 
	 } 
 
	 var navigateDatos = function(subpaneldatos){ 
		 ACTUAL_PANELDATOS = subpaneldatos; 
		 Ext.getCmp('panelDatos').setActiveItem(ACTUAL_PANELDATOS); 	 
	 } 
	 
     var obtenerDatosRealesEstacion = function() {
	 	 
		Ext.getCmp('cRealFecha').setValue('');
		Ext.getCmp('cRealHora').setValue('');
		Ext.getCmp('cRealTemperatura').setValue('');
		Ext.getCmp('cRealHumedad').setValue('');
		Ext.getCmp('cRealVientoDir').setValue('');
		Ext.getCmp('cRealVientoVel').setValue('');		
		Ext.getCmp('cRealLluviaDia').setValue('');
		Ext.getCmp('cRealLluviaMes').setValue('');
		Ext.getCmp('cRealPresion').setValue('');					
		
		Ext.Ajax.request({
			url: '../getInfoStation.aspx',
			method: 'GET',
			params: {
				action: 'getdataJSON',
				Station: codEstaciones[estacionSel]
			},
			success: function(response, opts){
				var text = response.responseText;
				// process server response here
				var ojson = Ext.JSON.decode(text, true);
				if (ojson != null) {
					Ext.getCmp('cRealFecha').setValue(ojson.Fecha);
					Ext.getCmp('cRealHora').setValue(ojson.Hora);
					Ext.getCmp('cRealTemperatura').setValue(ojson.outTemp);
					Ext.getCmp('cRealHumedad').setValue(ojson.outHumidity);
					Ext.getCmp('cRealVientoDir').setValue(ojson.windDir);
					Ext.getCmp('cRealVientoVel').setValue(ojson.windSpeed);		
					Ext.getCmp('cRealLluviaDia').setValue(ojson.rainDay);
					Ext.getCmp('cRealLluviaMes').setValue(ojson.rainMonth);
					Ext.getCmp('cRealPresion').setValue(ojson.barometer);
				} else Ext.Msg.alert('INFO', 'No se han recuperado datos'); 				
			},
			failure: function(form, action){
				Ext.Msg.alert('INFO', 'Error al intentar conectar'); 
			}			
		});	 
	 
	 }
	 
 
 /* **************************************************************************
  * PANELES 
  * ***************************************************************************/
  
	/*******************
	 * PANEL DE ESTACIONES
	 * *****************/	  
	var pEstaciones = Ext.create('Ext.Panel', { 
		scroll: 'vertical', 	
		id: 'panelEstaciones',
		style: 'background-color: #1111AA',	 		
		items:[
			{ 
				docked:'top', 
				xtype:'toolbar',			 
				items:[
					{
						xtype: 'spacer'
					},
					{ 
						text:'Estaciones Meteorologicas', 
						ui:'round',
						handler: function(btn){ 						
								} 
					},
					{
						xtype: 'spacer'
					}					
				]
			}, 
			{ 
				xtype: 'panel', 
				style: 'background-color: #1111AA; margin:10px 1px 10px 1px;',	
				layout: {
						type: 'vbox',
				//		align: 'middle'
				},				
				items: itemsEstaciones
			}
		]
	});

	/*******************
	 * PANEL DE OPCIONES
	 * *****************/	
	var pOpciones = Ext.create('Ext.Panel', {
		scroll: 'vertical', 	
		id: 'panelOpciones',
		style: 'background-color: #1111AA',	
		items:[
			{ 
				 docked:'top', 
				 xtype:'toolbar', 
				 items:[	
					{
						xtype: 'spacer'
					},
					{ 
					    id:'botonEstacionOp',
						text:'Estacion', 
						ui:'round',
						handler: function(btn){ 
						    estacionSel = -1;
							opcionSel = -1;
							navigate(0); 						
								} 
					},					
					{
						xtype: 'spacer'
					}					 					
				]
			},		
			{ 
				xtype: 'panel', 
				style: 'background-color: #1111AA; margin:10px 1px 10px 1px;',					
				layout: {
						type: 'vbox',
				//		align: 'middle'
				},					
				items: itemsOpciones
			},
			{ 
				 docked:'bottom', 
				 xtype:'toolbar', 
				 items:[
					{ 
						text:'Volver', 
						ui:'back',
						iconCls: 'arrow_left',						
						handler: function(btn){ 
						    estacionSel = -1;
							opcionSel = -1;
							navigate(0); 
						} 
					}				
				]
			}			
		]
	});

	/*******************
	 * SUBPANEL 0 DE PANEL DE DATOS (TIMEPO REAL)
	 * *****************/	
	var pDatosTiempoReal = Ext.create('Ext.Panel', { 
		id: 'panelDatosTiempoReal', 
		scrollable: {
			direction: 'vertical',
			directionLock: true
		},
		style: 'background-color: #1111AA; margin:10px 1px 10px 1px;',	
		items:[
			{ 
				xtype: 'panel', 			
				defaults: {
					labelWidth: '60%',
					disabled: true,
					useClearIcon: false,
					autoCapitalize: false,
					style: 'margin:1px 10px 1px 10px; '
				},
				items:[
				{
				    id:'cRealFecha',
					xtype: 'textfield', 
					name: 'fecha', 
					label: 'Fecha'					 			
				},		
				{
				    id:'cRealHora',
					xtype: 'textfield', 
					name: 'hora', 
					label: 'Hora'
				},					
				{
				    id:'cRealTemperatura',
					xtype: 'textfield', 
					name: 'temperatura', 
					label: 'Temperatura (ºC)'
				},
				{
				    id:'cRealHumedad',
					xtype: 'textfield', 
					name: 'humedad', 
					label: 'Humedad (%)'
				},
				{
				    id:'cRealVientoDir',
					xtype: 'textfield', 
					name: 'vientodir', 
					label: 'Viento direcc. (ºC)'
				},	
				{
				    id:'cRealVientoVel',
					xtype: 'textfield', 
					name: 'vientovel', 
					label: 'Viento velo. (Km/h)'
				},	
				{
				    id:'cRealLluviaDia',
					xtype: 'textfield', 
					name: 'lluviadia', 
					label: 'Lluvia día (mm)'
				},	
				{
				    id:'cRealLluviaMes',
					xtype: 'textfield', 
					name: 'lluviames', 
					label: 'Lluvia mes (mm)'
				},	
				{
				    id:'cRealPresion',
					xtype: 'textfield', 
					name: 'presion', 
					label: 'Presión (mBares)'
				}	
				]
			}					
		]
	});		
	


	/*******************
	 * SUBPANEL 1 DE PANEL DE DATOS (UBICACION)
	 * *****************/		
	var pDatosUbicacion = Ext.create('Ext.Panel', { 
		id: 'panelDatosUbicacion', 
		layout: 'fit',
		style: 'background-color: #1111AA',		
		items: [
			{ 
				id: 'mapa',
				xtype:'map', 
				mapOptions: { 
					center: new google.maps.LatLng(0.0, 0.0), 
					mapTypeId: google.maps.MapTypeId.TERRAIN, 
					zoom: 20 
				},
				listeners: {
					maprender: function(component, googleMap, eOpts) {	
				    
						var markers = new Array();
						for (i=0;i<estaciones.length;i++) {

							markers[i] = new google.maps.Marker({
								map: googleMap,
								position: new google.maps.LatLng(latitudEstaciones[i],longitudEstaciones[i]),
								title : estaciones[i],
								draggable:false
							});																			
						 
						}		
					}
				
				}
			}				
		]
	});
	
	
	/*******************
	 * SUBPANEL 2 DE PANEL RESUMEN DATOS (RESUMEN)
	 * *****************/	
	var tlpAux = new Ext.XTemplate('<table  width="100%" style="border:10px solid #1111AA;background-color:#1111AA;color:white;">' +
				'<tr><td ><p style="font-size:small;text-align:center;">Fecha</p> </td><td ><p style="font-size:small;text-align:center;">Hora</p></td><td ><p style="font-size:small;text-align:center;">Temp (ºC)</p> </td><td ><p style="font-size:small;text-align:center;">Hum (%)</p> </td><td ><p style="font-size:small;text-align:center;">Pre (mb)</p> </td><td><p style="font-size:small;text-align:center;">Rain (mm)</p> </td></tr>' +
				'<tpl for=".">' +
					'<tr><td><p style="font-size:small;text-align:center;">{Fecha}</p></td><td><p style="font-size:small;text-align:center;">{Hora}</p></td><td><p style="font-size:small;text-align:center;">{outTemp}</p></td><td ><p style="font-size:small;text-align:center;">{outHumidity}</p></td><td><p style="font-size:small;text-align:center;">{barometer}</p></td><td><p style="font-size:small;text-align:center;">{rain10}</p></td></tr>' +
				'</tpl>' +
				'</table>');				
				
	var pDatosResumen =Ext.create('Ext.DataView', {
		fullscreen: true,
		store: {
			autoLoad: false,
			fields: ['Fecha', 'Hora', 'outTemp', 'outHumidity', 'barometer','rain10'],

			proxy: {
				type: 'ajax',
				url: '../getInfoStation.aspx?action=getdata24JSON&Station=majalblanco',// + codEstaciones[estacionSel],

				reader: {
					type: 'json',
					rootProperty: 'results'					
				}
			},
			listeners: {
				load: function(sender, node, records) {
				//	Ext.each(records, function(record, index){
				//		this.logRecord(record);
				//	}, this);
		
				  var data = node;
				  var showData = new Array(); 
				  for(var i=0;i<data.length;i++){
					  showData.push(data[i].data); 
				  }

					//console.log(showData);
					pDatosResumen.setHtml(tlpAux.applyTemplate(showData));				
					
				}
			}			
		},

	//	itemTpl: '<table><tr><td width="100px">{outTemp} </td><td width="100px"> {outHumidity} </td><td width="100px"> {barometer} </td></tr> </table>',
		tpl: tlpAux
	});
	
	

				

	
	/*******************
	 * PANEL 3. DATOS
	 * *****************/	
	var pDatos = Ext.create('Ext.Panel', { 
		id: 'panelDatos', 	
		iconCls:'myToolBar',		
		layout: 'card', 
		xtype: 'panel',
		style: 'background-color: #1111AA',		
		items:[
			{ 
				docked:'top', 
				xtype:'toolbar',			 
				items:[
					{
						xtype: 'spacer'
					},				
					{ 
					    id:'botonEstacionD',
						text:'Estacion', 
						ui:'round',
						handler: function(btn){ 
						    estacionSel = -1;
							opcionSel = -1;
							navigate(0); 						
								} 
					},
					{ 
					    id:'botonOpcionD',
						text:'Opcion', 
						ui:'round',
						handler: function(btn){ 			
							navigate(1); 						
								} 
					},
					{
						xtype: 'spacer'
					}					
				]
			},		
			pDatosTiempoReal, pDatosUbicacion, pDatosResumen,
			{ 
				 docked:'bottom', 
				 xtype:'toolbar', 
				 items:[
					{ 
						text:'Volver', 
						ui:'back',
						text:'Volver', 
						iconCls: 'arrow_left',						
						handler: function(btn){ 
							opcionSel = -1;
							navigate(1); 
						} 
					},
					{ 
					xtype: 'spacer' 
					},
					{ 
					    id:'botonActualizarD',
						text:'Actualizar', 
						iconCls: 'refresh',
						iconMask: true,
						handler: function(btn){ 
									if (ACTUAL_PANELDATOS == 0)	obtenerDatosRealesEstacion();
									else if (ACTUAL_PANELDATOS==1) {
										pDatosUbicacion.getComponent('mapa').setMapOptions(
											{ 
												center: new google.maps.LatLng(latitudEstaciones[estacionSel], longitudEstaciones[estacionSel]), 
												mapTypeId: google.maps.MapTypeId.TERRAIN, 
												zoom: 20 
											}
										);
									} else if (ACTUAL_PANELDATOS==2) {
										pDatosResumen.getStore().load();
									}
								} 
					}					
				]
			}			
		]
	});
	

									
	/*******************
	 * PANEL PRINCIPAL
	 * *****************/ 
	Ext.Viewport.add(Ext.create('Ext.Panel',{
			id: 'panelPrincipal',
			layout: 'card', 
			xtype: 'panel',	
			style: 'background-color: blue',
			items: [pEstaciones,pOpciones,pDatos]
		}
	));
	
  } 
  
});