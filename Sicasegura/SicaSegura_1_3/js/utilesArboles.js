// Archivo JScript
//EGB 23/09/2008 Funciones para Tratamiento de Arboles en CLiente


 //EstaFunción Contrae o Expande el control de cliente treeview (treeviewid) 
  function TreeviewExpandCollapseAll(treeViewId, expandAll) 
     {
         var displayState = (expandAll == true ? "none" : "block");
         var treeView = document.getElementById(treeViewId);
         if(treeView)
         {
             var treeLinks = treeView.getElementsByTagName("a");
             var nodeCount = treeLinks.length;
             var flag = true;
            
             for(i=0;i<nodeCount;i++)
             {
                  if(treeLinks[i].firstChild.tagName)
                  {
                      if(treeLinks[i].firstChild.tagName.toLowerCase() == "img")
                      {
                        var currentToggleLink = treeLinks[i];
                        var childContainer = GetParentByTagName("table", currentToggleLink).nextSibling;
                        if (childContainer.style.display == displayState) 
                        {
                            eval(currentToggleLink.href);
                        }
                     }
                  }
             }
         }
   } 
   
        
 function TreeviewExpandCollapseAlldivs(treeViewId, expandAll,selectednodeid) 
   {
         var displayState = (expandAll == true ? "none" : "");
         var nodopadre = document.getElementById(selectednodeid);
         var re =/Treeview1t/g;
         var selecteddivid = selectednodeid.replace(re,"Treeview1n");
         
         var divpadre = document.getElementById(selecteddivid);
         if(divpadre)
         {
            
           
            var treeDIVS = divpadre.getElementsByTagName("DIV");
            var DIVCount = treeDIVS.length;    
            alert(DIVCount);      
            for(i=0;i<DIVCount;i++)
            {
                treeDIVS[i].style.display=displayState;
                  
            }
         }
        
   } 
   
            
   
   
  //---------------------------------------------------------
   function TreeviewExpandCollapseAlldivs_Back(treeViewId, expandAll) 
   {
         var displayState = (expandAll == true ? "none" : "");
         var treeView = document.getElementById(treeViewId);
         if(treeView)
         {
            
             var treeDIVS = treeView.getElementsByTagName("DIV");
             var DIVCount = treeDIVS.length;          
             for(i=0;i<DIVCount;i++)
             {
                  treeDIVS[i].style.display=displayState;
                  
              }
            
         }
        
   } 
function TreeviewExpandCollapseAllSinMemoria(treeViewId, expandAll) 
     {
         var displayState = (expandAll == true ? "none" : "block");
         var treeView = document.getElementById(treeViewId);
         if(treeView)
         {
             var treeLinks = treeView.getElementsByTagName("a");
             var nodeCount = treeLinks.length;
             var flag = true;
            
             for(i=0;i<nodeCount;i++)
             {
                  if(treeLinks[i].firstChild.tagName)
                  {
                      if(treeLinks[i].firstChild.tagName.toLowerCase() == "img")
                      {
                        var currentToggleLink = treeLinks[i];
                        var childContainer = GetParentByTagName("table", currentToggleLink).nextSibling;
                       
                            eval(currentToggleLink.href);
                        
                     }
                  }
             }
         }
   } 
      