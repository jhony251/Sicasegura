//v.1.6 build 80523

/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/



dhtmlXGridObject.prototype.splitAt=function(ind){var z=document.createElement("DIV");this.entBox.appendChild(z);this._treeC=this.cellType._dhx_find("tree");this.entBox.style.position="relative";this._fake=new dhtmlXGridObject(z);this._fake.delim=this.delim;this._fake.customGroupFormat=this.customGroupFormat;this._fake._fake=this;this._fake._realfake=true;this._fake.imgURL=this.imgURL;this._fake._customSorts=this._customSorts;this._fake.noHeader=this.noHeader;this._fake._enbTts=this._enbTts;this._fake.fldSort=new Array();this._fake.selMultiRows=this.selMultiRows;this._fake.multiLine=this.multiLine;if (this.multiLine){this.attachEvent("onCellChanged",this._correctRowHeight);this.attachEvent("onXLE",function(){this.forEachRow(function(id){this._correctRowHeight(id)})
 });this.attachEvent("onResizeEnd",function(){this.forEachRow(function(id){this._correctRowHeight(id)})
 })};this._fake.loadedKidsHash=this.loadedKidsHash;if (this._h2)this._fake._h2=this._h2;this._fake._dInc=this._dInc;var b_ha=[[],[],[],[],[],[],[]];var b_ar=["hdrLabels","initCellWidth","cellType","cellAlign","cellVAlign","fldSort","columnColor"];var b_fu=["setHeader","setInitWidths","setColTypes","setColAlign","setColVAlign","setColSorting","setColumnColor"];this._fake.callEvent=function(){this._fake._split_event=true;var res=this._fake.callEvent.apply(this._fake,arguments);this._fake._split_event=false;return res};if (this._elmn)this._fake.enableLightMouseNavigation(true);if (this.__cssEven||this._cssUnEven)this._fake.attachEvent("onGridReconstructed",function(){this._fixAlterCss()});this._fake._cssEven=this._cssEven;this._fake._cssUnEven=this._cssUnEven;this._fake.isEditable=this.isEditable;this._fake._edtc=this._edtc;if (this._sst)this._fake.enableStableSorting(true);this._fake._sclE=this._sclE;this._fake._dclE=this._dclE;this._fake._f2kE=this._f2kE;this._fake._maskArr=this._maskArr;this._fake._dtmask=this._dtmask;this._fake.combos=this.combos;var width=0;for (var i=0;i<ind;i++){for (var j=0;j<b_ar.length;j++){if (this[b_ar[j]])b_ha[j][i]=this[b_ar[j]][i]};if (_isFF)b_ha[1][i]+=2;if ( this.cellWidthType == "%"){b_ha[1][i]=Math.round(parseInt(this[b_ar[1]][i])*this.entBox.offsetWidth/100);width+=b_ha[1][i]}else
 width+=parseInt(this[b_ar[1]][i]);this.setColumnHidden(i,true)};for (var j=0;j<b_ar.length;j++){var str=b_ha[j].join(this.delim);if (str!=""){if (b_fu[j]!="setHeader")this._fake[b_fu[j]](str);else
 this._fake[b_fu[j]](str,null,this._hstyles)}};this._fake._strangeParams=this._strangeParams;this._fake._drsclmn=this._drsclmn;var pa=this.entBox.childNodes[0];pa.style.left=width+"px";if (this.ftr){this.ftr.style.left=width+"px";this.ftr.style.position="relative";var ftrT=this.ftr.childNodes[0].rows[0];var ftrZ=document.createElement("TH");ftrT.appendChild(ftrZ);ftrZ.style.width='30px'};pa.style.top=0+"px";pa.style.position="absolute";var w=this.entBox.offsetWidth-width-(_isFF?0:(this.entBox.offsetWidth-this.entBox.clientWidth))+"px";if (w>0)pa.style.width=w;z.style.width=width+"px";z.style.height=this.objBuf.offsetHeight;z.style.borderBottom="0px";z.style.position="absolute";z.style.top="0px";z.style.left="0px";z.style.zIndex=11;if (this._ecspn)this._fake._ecspn=true;this._fake.init();this._fake.objBox.style.overflow="hidden";this._fake.objBox.style.overflowX="scroll";this._fake._srdh=this._srdh||20;function change_td(a,b){var c=b.nextSibling;var cp=b.parentNode;a.parentNode.insertBefore(b,a);if (!c)cp.appendChild(a);else
 cp.insertBefore(a,c);var z=a.style.display;a.style.display=b.style.display;b.style.display=z};function proc_hf(i,rows,mode,frows){var temp_header=(new Array(ind)).join(this.delim);var temp_rspan=[];if (i==2)for (var k=0;k<ind;k++){var r=rows[i-1].cells[k];if (r.rowSpan && r.rowSpan>1){temp_rspan[r._cellIndex]=r.rowSpan-1;frows[i-1].cells[k].rowSpan=r.rowSpan;r.rowSpan=1}};for (i;i<rows.length;i++){this._fake.attachHeader(temp_header,null,mode);frows=frows||this._fake.ftr.childNodes[0].rows;var max_ind=ind;var r_cor=0;for (var j=0;j<max_ind;j++){if (temp_rspan[j]){temp_rspan[j]=temp_rspan[j]-1;if (_isIE)rows[i].insertBefore(document.createElement("TD"),rows[i].cells[0])
 r_cor++;continue};var a=frows[i].cells[j-r_cor];var b=rows[i].cells[j-(_isIE?0:r_cor)];var t=b.rowSpan;change_td(a,b);if (t>1){temp_rspan[j]=t-1;b.rowSpan=t};if (frows[i].cells[j].colSpan>1){rows[i].cells[j].colSpan=frows[i].cells[j].colSpan;max_ind-=frows[i].cells[j].colSpan-1;for (var k=1;k < frows[i].cells[j].colSpan;k++)frows[i].removeChild(frows[i].cells[j+1])}}}};if (this.hdr.rows.length>2)proc_hf.call(this,2,this.hdr.rows,"_aHead",this._fake.hdr.rows);if (this.ftr){proc_hf.call(this,1,this.ftr.childNodes[0].rows,"_aFoot");this._fake.ftr.parentNode.style.bottom=(_isFF?2:1)+"px"};if (this.saveSizeToCookie){this.saveSizeToCookie=function(name,cookie_param){if (this._realfake)return this._fake.saveSizeToCookie.apply(this._fake,arguments);if (!name)name=this.entBox.id;var z=new Array();if (this.cellWidthType=='px')var n="cellWidthPX";else
 var n="cellWidthPC";for (var i=0;i<this[n].length;i++)if (i<ind)z[i]=this._fake[n][i];else
 z[i]=this[n][i];z=z.join(",")
 this.setCookie(name,cookie_param,0,z);this.setCookie(name,cookie_param,1,z2);this.setCookie("gridSizeA"+name,z,cookie_param);var z=(this.initCellWidth||(new Array)).join(",");this.setCookie("gridSizeB"+name,z,cookie_param);return true};this.loadSizeFromCookie=function(name){if (!name)name=this.entBox.id;var z=this.getCookie("gridSizeB"+name);if (!z)return

 this.initCellWidth=z.split(",");var z=this.getCookie("gridSizeA"+name);if (this.cellWidthType=='px')var n="cellWidthPX";else
 var n="cellWidthPC";var summ2=0;if ((z)&&(z.length)){z=z.split(",");for (var i=0;i<z.length;i++)if (i<ind){this._fake[n][i]=z[i];summ2+=z[i]*1}else
 this[n][i]=z[i]};this._fake.entBox.style.width=summ2+"px";this._fake.objBuf.style.width=summ2+"px";var pa=this.entBox.childNodes[0];pa.style.left=summ2-(_isFF?0:0)+"px";if (this.ftr)this.ftr.style.left=summ2-(_isFF?0:0)+"px";pa.style.width=this.entBox.offsetWidth-summ2+"px";this.setSizes();return true};this._fake.onRSE=this.onRSE};this.setCellTextStyleA=this.setCellTextStyle;this.setCellTextStyle=function(row_id,i,styleString){if (i<ind)return this._fake.setCellTextStyle(row_id,i,styleString);else return this.setCellTextStyleA(row_id,i,styleString)};this.setRowTextBoldA=this.setRowTextBold;this.setRowTextBold = function(row_id){this.setRowTextBoldA(row_id);this._fake.setRowTextBold(row_id)};this.setRowColorA=this.setRowColor;this.setRowColor = function(row_id,color){this.setRowColorA(row_id,color);this._fake.setRowColor(row_id,color)};this.setRowHiddenA=this.setRowHidden;this.setRowHidden = function(id,state){this.setRowHiddenA(id,state);this._fake.setRowHidden(id,state)};this.setRowTextNormalA=this.setRowTextNormal;this.setRowTextNormal = function(row_id){this.setRowTextNormalA(row_id);this._fake.setRowTextNormal(row_id)};this.getChangedRows = function(){var res=new Array();for (var i=0;i<this.rowsCol.length;i++){var row=this.rowsCol[i];var cols=row.childNodes.length;for (var j=0;j<cols;j++)if (j>=ind){if (row.childNodes[j].wasChanged){res[res.length]=row.idd;break}}else{if (this._fake.rowsAr[row.idd].childNodes[j].wasChanged){res[res.length]=row.idd;break}}};return res.join(this.delim)};this.setRowTextStyleA=this.setRowTextStyle;this.setRowTextStyle = function(row_id,styleString){this.setRowTextStyleA(row_id,styleString);this._fake.setRowTextStyle(row_id,styleString)};this.lockRowA = this.lockRow;this.lockRow = function(id,mode){this.lockRowA(id,mode);this._fake.lockRow(id,mode)};this.getColWidth = function(i){if (i<ind)return parseInt(this._fake.cellWidthPX[i])+((_isFF)?2:0);else return parseInt(this.cellWidthPX[i])+((_isFF)?2:0)};this.setColWidthA=this._fake.setColWidthA=this.setColWidth;this.setColWidth = function(i,value){if (i<ind)this._fake.setColWidthA(i,value);else this.setColWidthA(i,value);if ((i+1)==ind) this._fake._correctSplit()};this.adjustColumnSizeA=this.adjustColumnSize;this.adjustColumnSize=function(aind,c){if (aind<ind){if (_isIE)this._fake.obj.style.tableLayout="";this._fake.adjustColumnSize(aind,c);if (_isIE)this._fake.obj.style.tableLayout="fixed";this._fake._correctSplit()}else return this.adjustColumnSizeA(aind,c)};var zname="cells";this._bfs_cells=this[zname];this[zname]=function(){if (arguments[1]<ind)return this._fake.cells.apply(this._fake,arguments);else
 return this._bfs_cells.apply(this,arguments)};this._bfs_setColumnHidden=this.setColumnHidden;this.setColumnHidden=function(){if (parseInt(arguments[0])<ind){this._fake.setColumnHidden.apply(this._fake,arguments);return this._fake._correctSplit()}else
 return this._bfs_setColumnHidden.apply(this,arguments)};var zname="cells2";this._bfs_cells2=this[zname];this[zname]=function(){if (arguments[1]<ind)return this._fake.cells2.apply(this._fake,arguments);else
 return this._bfs_cells2.apply(this,arguments)};var zname="cells3";this._bfs_cells3=this[zname];this[zname]=function(){if (arguments[1]<ind){arguments[0]=arguments[0].idd;return this._fake.cells.apply(this._fake,arguments)}else
 return this._bfs_cells3.apply(this,arguments)};var zname="changeRowId";this._bfs_changeRowId=this[zname];this[zname]=function(){this._bfs_changeRowId.apply(this,arguments);this._fake.changeRowId.apply(this._fake,arguments)};if (this.collapseKids){this._fake["_bfs_collapseKids"]=this.collapseKids;this._fake["collapseKids"]=function(){return this._fake["collapseKids"].apply(this._fake,[this._fake.rowsAr[arguments[0].idd]])};this["_bfs_collapseKids"]=this.collapseKids;this["collapseKids"]=function(){var z=this["_bfs_collapseKids"].apply(this,arguments);this._fake._h2syncModel()};this._fake["_bfs_expandKids"]=this.expandKids;this._fake["expandKids"]=function(){return this._fake["expandKids"].apply(this._fake,[this._fake.rowsAr[arguments[0].idd]])};this["_bfs_expandAll"]=this.expandAll;this["expandAll"]=function(){this._bfs_expandAll();this._fake._h2syncModel()};this["_bfs_collapseAll"]=this.collapseAll;this["collapseAll"]=function(){this._bfs_collapseAll();this._fake._h2syncModel()};this["_bfs_expandKids"]=this.expandKids;this["expandKids"]=function(){var z=this["_bfs_expandKids"].apply(this,arguments);this._fake._h2syncModel()};this._fake._h2syncModel=function(){this._renderSort()};this._updateTGRState=function(a){return this._fake._updateTGRState(a)}};if (this._elmnh){this._setRowHoverA=this._fake._setRowHoverA=this._setRowHover;this._unsetRowHoverA=this._fake._unsetRowHoverA=this._unsetRowHover;this._setRowHover=this._fake._setRowHover=function(){var that=this.grid;that._setRowHoverA.apply(this,arguments);var z=(_isIE?event.srcElement:arguments[0].target);z=that._fake.rowsAr[that.getFirstParentOfType(z,'TD').parentNode.idd];if (z){that._fake._setRowHoverA.apply(that._fake.obj,[{target:z.childNodes[0]},arguments[1]])}};this._unsetRowHover=this._fake._unsetRowHover=function(){if (arguments[1])var that=this;else var that=this.grid;that._unsetRowHoverA.apply(this,arguments);that._fake._unsetRowHoverA.apply(that._fake.obj,arguments)};this._fake.enableRowsHover(true,this._hvrCss);this.enableRowsHover(false);this.enableRowsHover(true,this._fake._hvrCss)};this._updateTGRState=function(z){if (!z.update || z.id==0)return;if (this.rowsAr[z.id].imgTag)this.rowsAr[z.id].imgTag.src=this.imgURL+z.state+".gif";if (this._fake.rowsAr[z.id].imgTag)this._fake.rowsAr[z.id].imgTag.src=this.imgURL+z.state+".gif";z.update=false};this.copy_row=function(row){var x=row.cloneNode(true);x._skipInsert=row._skipInsert;var r_ind=ind;x._attrs={};if (this._ecspn){r_ind=0;for (var i=0;(i<x.childNodes.length && i<ind);i+=(x.childNodes[i].colSpan||1))
 r_ind++};while (x.childNodes.length>r_ind)x.removeChild(x.childNodes[x.childNodes.length-1]);var zm=r_ind;for (var i=0;i<zm;i++){x.childNodes[i].style.display=(this._fake._hrrar?(this._fake._hrrar[i]?this._fake._hrrar[i]:""):"");x.childNodes[i]._cellIndex=i;x.childNodes[i].combo_value=arguments[0].childNodes[i].combo_value;x.childNodes[i]._clearCell=arguments[0].childNodes[i]._clearCell;x.childNodes[i]._cellType=arguments[0].childNodes[i]._cellType;x.childNodes[i]._brval=arguments[0].childNodes[i]._brval;if(x.childNodes[i].colSpan>1)this._childIndexes=this._fake._childIndexes};if (this._h2 && this._treeC < ind){var trow=this._h2.get[arguments[0].idd];x.imgTag=x.childNodes[this._treeC].childNodes[0].childNodes[trow.level];x.valTag=x.childNodes[this._treeC].childNodes[0].childNodes[trow.level+2]};x.idd=row.idd;x.grid=this._fake;return x};var zname="_insertRowAt";this._bfs_insertRowAt=this[zname];this[zname]=function(){var r=this["_bfs_insertRowAt"].apply(this,arguments);arguments[0]=this.copy_row(arguments[0]);var r2=this._fake["_insertRowAt"].apply(this._fake,arguments);if (r._fhd){r2.parentNode.removeChild(r2);this._fake.rowsCol._dhx_removeAt(this._fake.rowsCol._dhx_find(r2));r._fhd=false};return r};var zname="setSizes";this._bfs_setSizes=this[zname];this[zname]=function(){if (this.cellWidthType == "%"){var z1=this.entBox.childNodes[0].offsetWidth;var z2=this.entBox.childNodes[1].offsetWidth;var width=Math.round(z1*this.entBox.offsetWidth/(z1+z2));this.entBox.childNodes[0].style.left=width+"px";this.entBox.childNodes[1].style.width=width+(_isIE?0:2)+"px";this.entBox.childNodes[0].style.width=this.entBox.offsetWidth-width-(_isFF?0:(this.entBox.offsetWidth-this.entBox.clientWidth))+"px";this._rec_count=(this._rec_count?this._rec_count+1:1);if (this._rec_count<2)this._fake.setColWidth(ind-1,this._fake.getColWidth(ind-1)-(_isIE?0:2));this._rec_count=null};this["_bfs_setSizes"].apply(this,arguments);if (this._notresize)return;var wcor=this.entBox.offsetWidth-this.entBox.clientWidth;if (wcor>1)wcor=wcor/2;z.style.height=this.entBox.offsetHeight-wcor+"px";if ((!this.noHeader)&& this._fake.hdr.offsetHeight!=this.hdr.offsetHeight){if (this.hdr.rows.length!=2)for (var i=1;i<this._fake.hdr.rows.length;i++){this._fake.hdr.rows[i].style.height=this.hdr.rows[i].offsetHeight+"px";var check=this._fake.hdr.rows[i].offsetHeight-this.hdr.rows[i].offsetHeight;if (check && this.hdr.rows[i].offsetHeight > check)this._fake.hdr.rows[i].style.height=this.hdr.rows[i].offsetHeight-check+"px"}else
 this._fake.hdr.style.height=this.hdr.offsetHeight+"px";this._rec_count=(this._rec_count?this._rec_count+1:1);if (this._rec_count<2)this._fake["setSizes"].apply(this._fake,arguments);this._rec_count=null};if (((this.obj.offsetWidth+1*((this.objBox.offsetHeight<=this.objBox.scrollHeight)?(_isFF?20:18):0))<=this.objBox.offsetWidth)&&(this._fake.obj.offsetWidth<=this._fake.objBox.offsetWidth))
 {this._fake.objBox.style.overflowX="hidden";this.objBox.style.overflowX="hidden"}else
 {this._fake.objBox.style.overflowX="scroll";this.objBox.style.overflowX="scroll"};var wa=this.entBox.offsetWidth-parseInt(this.entBox.childNodes[0].style.left)-(_isFF?2:0)+"px";if (wa>0)this.entBox.childNodes[0].style.width=wa;this._fake.objBox.scrollTop=this.objBox.scrollTop;this._fake["_bfs_setSizes"].apply(this._fake,arguments);this._fake.entCnt.rows[1].cells[0].childNodes[0].style.top = this.entCnt.rows[1].cells[0].childNodes[0].style.top};this._fake._bfs_setSizes=this._fake[zname];this._fake[zname]=function(){this["_bfs_setSizes"].apply(this,arguments);if (this._fake._notresize)return;if ((!this.noHeader)&& this._fake.hdr.offsetHeight!=this.hdr.offsetHeight){if (this.hdr.rows.length!=2)for (var i=1;i<this.hdr.rows.length;i++){if (this.hdr.rows[i].offsetHeight<this._fake.hdr.rows[i].cells[0].scrollHeight)return this._fake.setSizes();this._fake.hdr.rows[i].style.height=this.hdr.rows[i].offsetHeight+"px";var check=this._fake.hdr.rows[i].offsetHeight-this.hdr.rows[i].offsetHeight;if (check)this._fake.hdr.rows[i].style.height=this.hdr.rows[i].offsetHeight-check+"px"}else
 this._fake.hdr.style.height=this.hdr.offsetHeight+"px";this._fake["setSizes"].apply(this._fake,arguments)};this.entCnt.rows[1].cells[0].childNodes[0].style.top = this._fake.entCnt.rows[1].cells[0].childNodes[0].style.top;if (((this._fake.obj.offsetWidth+1*((this._fake.objBox.offsetHeight<=this._fake.objBox.scrollHeight)?(_isFF?20:18):0))<=this._fake.objBox.offsetWidth)&&(this.obj.offsetWidth<=this.objBox.offsetWidth))
 {this.objBox.style.overflowX="hidden";this._fake.objBox.style.overflowX="hidden"}else{this.objBox.style.overflowX="scroll";this._fake.objBox.style.overflowX="scroll"}};var zname="_doOnScroll";this._bfs__doOnScroll=this[zname];this[zname]=function(){this._bfs__doOnScroll.apply(this,arguments);this._fake.objBox.scrollTop=this.objBox.scrollTop;this._fake["_doOnScroll"].apply(this._fake,arguments)};var zname="doClick";this._bfs_doClick=this[zname];this[zname]=function(){this["_bfs_doClick"].apply(this,arguments);if (arguments[0].tagName=="TD"){var fl=(arguments[0]._cellIndex>=ind);if (!arguments[0].parentNode.idd)return;if (!this._fake.rowsAr[arguments[0].parentNode.idd])return;arguments[0]=this._fake.cells(arguments[0].parentNode.idd,(fl?0:arguments[0]._cellIndex)).cell;if (fl)this._fake.cell=null;this._fake["_bfs_doClick"].apply(this._fake,arguments);if (fl)this._fake.cell=this.cell;else this.cell=this._fake.cell;if (this._fake.onRowSelectTime)clearTimeout(this._fake.onRowSelectTime)
 if (fl){arguments[0].className=arguments[0].className.replace(/cellselected/g,"");globalActiveDHTMLGridObject=this;this._fake.cell=this.cell}else{this.objBox.scrollTop=this._fake.objBox.scrollTop}}};this._fake._bfs_doClick=this._fake[zname];this._fake[zname]=function(){this["_bfs_doClick"].apply(this,arguments);if (arguments[0].tagName=="TD"){var fl=(arguments[0]._cellIndex<ind);if (!arguments[0].parentNode.idd)return;arguments[0]=this._fake._bfs_cells(arguments[0].parentNode.idd,(fl?ind:arguments[0]._cellIndex)).cell;this._fake.cell=null;this._fake["_bfs_doClick"].apply(this._fake,arguments);this._fake.cell=this.cell;if (this._fake.onRowSelectTime)clearTimeout(this._fake.onRowSelectTime)
 if (fl){arguments[0].className=arguments[0].className.replace(/cellselected/g,"");globalActiveDHTMLGridObject=this;this._fake.cell=this.cell}}};this.clearSelectionA = this.clearSelection;this.clearSelection = function(mode){if (mode)this._fake.clearSelection();this.clearSelectionA()};this.moveRowUpA = this.moveRowUp;this.moveRowUp = function(row_id){this._fake.moveRowUp(row_id);this.moveRowUpA(row_id)};this.moveRowDownA = this.moveRowDown;this.moveRowDown = function(row_id){this._fake.moveRowDown(row_id);this.moveRowDownA(row_id)};this._fake.getUserData=function(){return this._fake.getUserData.apply(this._fake,arguments)};this._fake.setUserData=function(){return this._fake.setUserData.apply(this._fake,arguments)};this.getSortingStateA=this.getSortingState;this.getSortingState = function(){var z=this.getSortingStateA();if (z.length!=0)return z;return this._fake.getSortingState()};this.setSortImgStateA=this._fake.setSortImgStateA=this.setSortImgState;this.setSortImgState = function(a,b,c,d){this.setSortImgStateA(a,b,c,d);if (b*1<ind){this._fake.setSortImgStateA(a,b,c,d);this.setSortImgStateA(false)}else 
 this._fake.setSortImgStateA(false)};this._fake.doColResizeA = this._fake.doColResize;this._fake.doColResize = function(ev,el,startW,x,tabW){a=-1;var z=0;if (arguments[1]._cellIndex==(ind-1)){a = this._initalSplR + (ev.clientX-x);if (!this._initalSplF)this._initalSplF=arguments[3]+this.objBox.scrollWidth-this.objBox.offsetWidth;if (this.objBox.scrollWidth==this.objBox.offsetWidth && (this._fake.alter_split_resize || (ev.clientX-x)>0 )){arguments[3]=(this._initalSplF||arguments[3]);z=this.doColResizeA.apply(this,arguments)}else
 z=this.doColResizeA.apply(this,arguments)}else{if (this.obj.offsetWidth<this.entBox.offsetWidth)a=this.obj.offsetWidth;z=this.doColResizeA.apply(this,arguments)};this._correctSplit(a);this.resized=this._fake.resized=1;return z};this._fake.changeCursorState = function(ev){var el = ev.target||ev.srcElement;if(el.tagName!="TD")el = this.getFirstParentOfType(el,"TD")
 if ((el.tagName=="TD")&&(this._drsclmn)&&(!this._drsclmn[el._cellIndex])) return;var check = (ev.layerX||0)+(((!_isIE)&&(ev.target.tagName=="DIV"))?el.offsetLeft:0);var pos = parseInt(this.getPosition(el,this.hdrBox));if(((el.offsetWidth - (ev.offsetX||(pos-check)*-1))<10)||((this.entBox.offsetWidth - (ev.offsetX?(ev.offsetX+el.offsetLeft):0) + this.objBox.scrollLeft - check)<10)){el.style.cursor = "E-resize"}else
 el.style.cursor = "default";if (_isOpera)this.hdrBox.scrollLeft = this.objBox.scrollLeft};this._fake.startColResizeA = this._fake.startColResize;this._fake.startColResize = function(ev){var z=this.startColResizeA(ev);this._initalSplR=this.entBox.offsetWidth;this._initalSplF=null;if (this.entBox.onmousemove){var m=this.entBox.parentNode;if (m._aggrid)return z;m._aggrid=m.grid;m.grid=this;this.entBox.parentNode.onmousemove=this.entBox.onmousemove;this.entBox.onmousemove=null};return z};this._fake.stopColResizeA = this._fake.stopColResize;this._fake.stopColResize = function(ev){if (this.entBox.parentNode.onmousemove){var m=this.entBox.parentNode;m.grid=m._aggrid;m._aggrid=null;this.entBox.onmousemove=this.entBox.parentNode.onmousemove;this.entBox.parentNode.onmousemove=null};return this.stopColResizeA(ev)};this.doKeyA = this.doKey;this._fake.doKeyA = this._fake.doKey;this._fake.doKey=this.doKey=function(ev){if (!ev)return true;if (this._htkebl)return true;switch (ev.keyCode){case 9:
 if (!ev.shiftKey){if (this._realfake){if ((this.cell)&&(this.cell._cellIndex==(ind-1))){if (ev.preventDefault)ev.preventDefault();this._fake.selectCell(this.rowsCol._dhx_find(this.cell.parentNode),ind,false,false,true);return false}else
 var z=this.doKeyA(ev);globalActiveDHTMLGridObject=this;return z}else{if ((this.cell)&&(this.cell._cellIndex==(this.rowsCol[0].childNodes.length-1))){if (ev.preventDefault)ev.preventDefault();var z=this._fake.rowsCol[this.rowsCol._dhx_find(this.cell.parentNode)+1];if (z)this._fake.selectCell(z,0,false,false,true);return false}else
 return this.doKeyA(ev)}}else{if (this._realfake){if ((this.cell)&&(this.cell._cellIndex==0)){if (ev.preventDefault)ev.preventDefault();var z=this._fake.rowsCol[this.rowsCol._dhx_find(this.cell.parentNode)-1];if (z)this._fake.selectCell(z,this._fake.rowsCol[0].childNodes.length-1,false,false,true);return false}else
 return this.doKeyA(ev)}else{if ((this.cell)&&(this.cell._cellIndex==ind)){if (ev.preventDefault)ev.preventDefault();this._fake.selectCell(this.rowsCol._dhx_find(this.cell.parentNode),ind-1,false,false,true);return false}else
 return this.doKeyA(ev)}};break};return this.doKeyA(ev)};this.editCellA=this.editCell;this.editCell=function(){if (!this.cell && this._fake.cell)return this._fake.editCell();return this.editCellA()};this.deleteRowA = this.deleteRow;this.deleteRow=function(row_id,node){if (this.deleteRowA(row_id,node)===false) return false;this._fake.deleteRow(row_id)};this.clearAllA = this.clearAll;this.clearAll=function(){this.clearAllA();this._fake.clearAll()};this.attachEvent("onAfterSorting",function(i,b,c){if (i>=ind)this._fake.setSortImgState(false)
}) 



this._fake.sortField = function(a,b,c){this._fake.sortField.call(this._fake,a,b,this._fake.hdr.rows[0].cells[a]);if (this.fldSort[a]!="na")this.setSortImgState(true,arguments[0],this._fake.getSortingState()[1])
 };this.sortTreeRowsA = this.sortTreeRows;this._fake.sortTreeRowsA = this._fake.sortTreeRows;this.sortTreeRows=this._fake.sortTreeRows=function(col,type,order,ar){if (this._realfake)return this._fake.sortTreeRows(col,type,order,ar)

 this.sortTreeRowsA(col,type,order,ar);this._fake._h2syncModel();this._fake.setSortImgStateA(false);this._fake.fldSorted=null};this._fake._fillers=[];this._fake.rowsBuffer=this.rowsBuffer;this._add_filler_s=this._add_filler;this._add_filler=function(a,b,c){if (!c)this._fake._fillers.push(this._fake._add_filler.apply(this._fake,arguments));return this._add_filler_s.apply(this,arguments)};this._add_from_buffer_s=this._add_from_buffer;this._add_from_buffer=function() {var res=this._add_from_buffer_s.apply(this,arguments);if (res!=-1)this._fake._add_from_buffer.apply(this._fake,arguments);return res};this._fake.render_row=function(ind){var row=this._fake.render_row(ind);if (row == -1)return -1;if (row){return this.rowsAr[row.idd]=this._fake.copy_row(row)};return null};this._reset_view_s=this._reset_view;this._reset_view=function(){this._fake._reset_view(true);this._fake._fillers=[];this._reset_view_s()};this.attachEvent("onCellChanged",function(id,i,val){if (this._split_event && i<ind && this.rowsAr[id])this.rowsAr[id].cells[i].innerHTML=val})





 this._fake.combos=this.combos;this.setSizes();this.attachEvent("onXLE",function(){this._fake._correctSplit()})
 this._fake._correctSplit()};dhtmlXGridObject.prototype._correctSplit=function(a){a=a||(this.obj.scrollWidth-this.objBox.scrollLeft);if (a>-1){this.entBox.style.width=a+"px";this.objBuf.style.width=a+"px";var pa=this._fake.entBox.childNodes[0];pa.style.left=a+"px";if (this._fake.ftr)this._fake.ftr.style.left=a-(_isFF?2:0)+"px";pa.style.width=Math.max(0,this._fake.entBox.offsetWidth-a-(this._fake._sizeFix?this._fake._sizeFix*2:0))+"px"}};dhtmlXGridObject.prototype._correctRowHeight=function(id,ind){if (!this.rowsAr[id] || !this._fake.rowsAr[id])return;var h=this.rowsAr[id].offsetHeight;var h2=this._fake.rowsAr[id].offsetHeight;if (h>h2)this._fake.rowsAr[id].style.height=h+"px";if (h<h2)this.rowsAr[id].style.height=h2+"px"};//(c)dhtmlx ltd. www.dhtmlx.com
//v.1.6 build 80523

/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/