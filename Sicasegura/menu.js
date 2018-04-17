//ESTE ES EL JAVASCRIPT DEL NUEVO MENU;
var sfHover = function() {
	var sfEls = document.getElementById("globalLink").getElementsByTagName("LI");
	for (var i=0; i<sfEls.length; i++) {
		sfEls[i].onmouseover=function() {
			this.className+=" sfhover";
		}
		sfEls[i].onmouseout=function() {
			this.className=this.className.replace(new RegExp(" sfhover\\b"), "");
		}
	}
}
if (window.attachEvent) window.attachEvent("onload", sfHover);


if (navigator.appVersion.indexOf('MSIE 6.')>=0)
{ 
 
ieHover = function() {
 
var ieULs = document.getElementById('globalNav').getElementsByTagName('ul');
 
/** IE script to cover <select> elements with <iframe>s **/
 
for (j=0; j<ieULs.length; j++) {
 
ieULs[j].innerHTML = ('<iframe src="javascript:false;" scrolling="no" frameborder="0"></iframe>' + ieULs[j].innerHTML);
 var ieMat = ieULs[j].firstChild;
 
  ieMat.style.width=ieULs[j].offsetWidth+"px";
 
  ieMat.style.height=ieULs[j].offsetHeight+"px"; 
 
  ieULs[j].style.zIndex="99";
 
}
 
/** IE script to change class on mouseover **/
 
 var ieLIs = document.getElementById('globalNav').getElementsByTagName('li');
 
 for (var i=0; i<ieLIs.length; i++) if (ieLIs[i]) {
 
  ieLIs[i].onmouseover=function() {this.className+=" iehover";}
 
  ieLIs[i].onmouseout=function() {this.className=this.className.replace(' iehover', '');}
 
 }
}
 
if (window.attachEvent) window.attachEvent('onload', ieHover);
}

