<%@ Control Language="VB" AutoEventWireup="false" CodeFile="jerarquiaUsuarios.ascx.vb" Inherits="jerarquiaUsuarios" %>
<script language=javascript>
    var ratonPulsado = false;
    var antX=0;
    var antY=0;

    function inicioMoverCapa(){
        ratonPulsado = true;
        antX = window.event.clientX;
        antY = window.event.clientY;
        var laCapa;
        if (document.getElementById("ucJerarquiaUsuarios_capa")){
            laCapa=document.getElementById("ucJerarquiaUsuarios_capa");
        }else{
            laCapa=document.getElementById("ucJerarquiaUsuarios1_capa");
        }
        
    }
    
    function moverCapa(){
        var actX = window.event.clientX;
        var actY = window.event.clientY;
        var laCapa;
        if (document.getElementById("ucJerarquiaUsuarios_capa")){
            laCapa=document.getElementById("ucJerarquiaUsuarios_capa");
        }else{
            laCapa=document.getElementById("ucJerarquiaUsuarios1_capa");
        }
        
        
        laCapa.style.pixelLeft += actX-antX;
        laCapa.style.pixelTop += actY-antY;
        
        antX = actX;
        antY = actY;

    }

    function capturaRaton(x,y){    
        var capturaX;
        var capturaY;
        if (document.getElementById("ucJerarquiaUsuarios_capturaX")){
            capturaX=document.getElementById("ucJerarquiaUsuarios_capturaX");
            capturaY=document.getElementById("ucJerarquiaUsuarios_capturaY");
        }else{
            capturaX=document.getElementById("ucJerarquiaUsuarios1_capturaX");
            capturaY=document.getElementById("ucJerarquiaUsuarios1_capturaY");
        }
        
        if (x==0 && y==0){
            capturaX.value=(document.documentElement.offsetWidth-700)/2;
            capturaY.value=(document.documentElement.offsetHeight-500)/2;
        }else{
            if (window.event.clientX+x>document.documentElement.offsetWidth){
                capturaX.value=document.documentElement.offsetWidth-x-30;
            }else{
                capturaX.value=window.event.clientX;
            }
            capturaY.value=window.event.clientY+ document.documentElement.scrollTop+y;
        }
    }

    function capturaRaton2(srcElement){    
        var capturaX;
        var capturaY;
        var laCapa;

        if (document.getElementById("ucJerarquiaUsuarios_capturaX")){
            capturaX=document.getElementById("ucJerarquiaUsuarios_capturaX");
            capturaY=document.getElementById("ucJerarquiaUsuarios_capturaY");
        }else{
            capturaX=document.getElementById("ucJerarquiaUsuarios1_capturaX");
            capturaY=document.getElementById("ucJerarquiaUsuarios1_capturaY");
        }
        if (document.getElementById("ucJerarquiaUsuarios_capa")){
            laCapa=document.getElementById("ucJerarquiaUsuarios_capa");
        }else{
            laCapa=document.getElementById("ucJerarquiaUsuarios1_capa");
        }
        
        
        
        capturaX.value = window.event.clientX;
        capturaY.value = window.event.clientY + srcElement.offsetHeight/2;
    }

    
    function mostrarCapa(){    
        var cantidad;
        cantidad=document.getElementsByTagName("select").length;        
        
        var i;
        for (i=0;i<cantidad;i++){
        document.getElementsByTagName("select").item(i).style.visibility="hidden";
        }
        
        var lstEstado;
        var capa;
        var capturaX;
        var capturaY;
        
        if (document.getElementById("ucJerarquiaUsuarios_lstEstado")){
            lstEstado=document.getElementById("ucJerarquiaUsuarios_lstEstado");
        }else{
            lstEstado=document.getElementById("ucJerarquiaUsuarios1_lstEstado");
        }
        
        if (document.getElementById("ucJerarquiaUsuarios_capa")){
            capa=document.getElementById("ucJerarquiaUsuarios_capa");
        }else{
            capa=document.getElementById("ucJerarquiaUsuarios1_capa");
        }
        
        if (document.getElementById("ucJerarquiaUsuarios_capturaX")){
            capturaX=document.getElementById("ucJerarquiaUsuarios_capturaX");
            capturaY=document.getElementById("ucJerarquiaUsuarios_capturaY");
        }else{
            capturaX=document.getElementById("ucJerarquiaUsuarios1_capturaX");
            capturaY=document.getElementById("ucJerarquiaUsuarios1_capturaY");
        }
        
        if (parseInt(capturaX.value)+capa.offsetWidth > document.documentElement.offsetWidth) capturaX.value = (parseInt(capturaX.value) - capa.offsetWidth +10);
        if (parseInt(capturaY.value)+capa.offsetHeight > document.documentElement.offsetHeight) capturaY.value = (document.documentElement.offsetHeight - capa.offsetHeight -5);

        
        
        if (lstEstado){
            lstEstado.style.visibility="visible";
        }
             
        capa.style.visibility="visible";        
        capa.style.left=capturaX.value;
        capa.style.top=capturaY.value;                       
        
    }   
    
    function cerrar(){
        var cantidad;
        if (document.getElementsByTagName("select")){
        cantidad=document.getElementsByTagName("select").length;
        }
        
        var i;
        for (i=0;i<cantidad;i++){
        document.getElementsByTagName("select").item(i).style.visibility="visible";
        }
        
        var lstEstado;
        var capa;   
        
        if (document.getElementById("ucJerarquiaUsuarios_lstEstado")){
            lstEstado=document.getElementById("ucJerarquiaUsuarios_lstEstado");
        }else{
            lstEstado=document.getElementById("ucJerarquiaUsuarios1_lstEstado");
        }
        
        if (document.getElementById("ucJerarquiaUsuarios_capa")){
            capa=document.getElementById("ucJerarquiaUsuarios_capa");
        }else{
            capa=document.getElementById("ucJerarquiaUsuarios1_capa");
        }
        
        if (lstEstado){
            lstEstado.style.visibility="hidden";
        }        
        capa.style.visibility="hidden";
    }    
</script>
    <div class="capa" id="capa" runat="server"> 
    <asp:Panel ID="pnlJerarquiaUs" runat="server" Visible="true">
    <table>
        <tr>
            <td style="background-color:gray; cursor: arrow" onmousedown="inicioMoverCapa()" onmouseup="ratonPulsado=false" onmousemove="if (ratonPulsado) moverCapa();">
                <a href="javascript:cerrar()"><img align="right" src="<%=ruta %>js/calendar/images/close.gif" border="0" />&nbsp;&nbsp;</a>               
                <asp:Label ID=lblNombre runat=server Font-Names=Verdana Font-Size=8 ForeColor=white style="cursor:default"></asp:Label>&nbsp;&nbsp;                
                <asp:Label ID="lblResponsable" runat=server Font-Names=Verdana Font-Size=8 ForeColor=white></asp:Label>
                <asp:Label ID="lblTabla" runat=server Visible="false"></asp:Label>
                <asp:Label ID="lblClaveTabla" runat=server Visible="false"></asp:Label>
                <asp:Label ID="lblClaveSecTabla" runat=server Visible="false"></asp:Label>    
                <asp:Label ID="lblCampoAActualizar" runat=server Visible="false"></asp:Label>
                <asp:Label ID="lblDireccion" runat=server Visible="false"></asp:Label>
                <asp:Label ID="lblActualizarDatos" runat="server" Visible="false"></asp:Label>    
                <asp:Label ID="lblIdUsuario" runat=server Visible="false"></asp:Label>    
                <asp:Label ID="lblPagina" runat=server Visible="false"></asp:Label>                        
            </td>   
        </tr>
        <tr>
            <td>                
                <div class="capa2" id="capa2" runat="server">                                            
                    <asp:TreeView ID="TreeView2" runat=server>
                        <Nodes></Nodes>
                    </asp:TreeView>  
                </div>        
            </td>
        </tr>
    </table>     
    </asp:Panel>
    
    <asp:Panel ID="pnlEstado" runat="server" Visible="false">
    <table>
        <tr>
            <td style="background-color:gray; cursor: default" visible="false">
                <img align="right" src="<%=ruta %>js/calendar/images/close.gif" style="cursor:pointer" onclick="cerrar();" />&nbsp;&nbsp;
                <asp:Label ID="lblEstado" runat="server" Font-Names=Verdana Font-Size=8 ForeColor=white></asp:Label>
                <asp:Label ID="lblIdTarea" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblIdPDA" runat="server" Visible="False"></asp:Label>  
                <asp:Label ID="lblIdUsuario2" runat="server" Visible="False"></asp:Label>                                
                <asp:Label ID="lblPaginaSel" runat="server" Visible="false"></asp:Label>
            </td>            
        </tr>
        <tr>    
            <td>
                <div class="capa3" id="capa3" runat="server">                    
                    <asp:ListBox runat="server" ID="lstEstado" Width="200px" Height="100px" CssClass="texto" AutoPostBack="true"></asp:ListBox>
                </div>            
            </td>        
        </tr>
    </table>    
    </asp:Panel>
    
    <asp:Panel ID="pnlFotos" runat="server" Visible="false">
    <table>
        <tr>
            <td style="background-color:gray; cursor: default">
                <img align="right" src="<%=ruta %>js/calendar/images/close.gif" style="cursor:pointer" onclick="cerrar();" />&nbsp;&nbsp;                                
            </td>            
        </tr>
        <tr>
        <td>
            <div class="capa4" id="capa4" runat="server"></div>
        </td>
        </tr>
    </table>
    </asp:Panel>
    
    <asp:TextBox ID="capturaX" runat="server" style="display: none;"></asp:TextBox>              
    <asp:TextBox ID="capturaY" runat="server" style="display: none;"></asp:TextBox>  
    </div>            