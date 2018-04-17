<%@ Page Language="VB" AutoEventWireup="false" CodeFile="caucepuntDETALLE.aspx.vb" Inherits="SICAH_caucepuntDETALLE" %>
<%@ Register TagPrefix="uc" TagName="paginacion" Src="~/Controls/paginacion.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
  <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
  
  <!-- Referencia Librerías de JScripts de Maquetación de Desplegables y Calendario -->
  
  <script type="text/jscript" language="javascript" src="../js/utiles.js"></script>
  <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
  <script type="text/jscript" language="javascript" src="../js/utilesListados.js"></script>
  
  <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
    <link href="../styles.css" rel="stylesheet" />
 
 <script language="javascript" type="text/javascript">
 
    function TABLE1_onclick() {

    }
    function TABLE2_onclick() {

    }
    function TABLE3_onclick() {

    }
// ncm para menu sin recarga 22/10/2008
//    function cambioIFrame(X, Y,radio,NumReg, Alerta)
//    {
//        window.parent.document.getElementById('iframeDetalleVisor').style.width = 920;
//        window.parent.document.getElementById('iframeDetalleVisor').style.height = 720;
//        window.parent.document.getElementById('iframeDetalleVisor').src="accesoVisorDesdeArbolMain.aspx?X=" + X + "&y=" + Y + "&zone=30&R=" + radio + "&alerta=" + Alerta;
//        
//    }
   function cambioIFrame(X, Y,radio,NumReg, Alerta)
    {
        window.parent.document.getElementById('iframeDetalle').style.width = 920;
        window.parent.document.getElementById('iframeDetalle').style.height = 720;
        window.parent.document.getElementById('iframeDetalle').src="accesoVisorDesdeArbolMain.aspx?X=" + X + "&y=" + Y + "&zone=30&R=" + radio + "&alerta=" + Alerta;
        
    }
    function verresultado()
    {
        var pagina=window.parent.document.getElementById('iframeDetalle').src;
        if (pagina.substr(0,26)=="accesoVisorDesdeArbol.aspx")
        {
           window.parent.document.getElementById('iframeDetalle').style.width = 900;           
        }
              
    }

    function actualizaLecturasVisor()
    {
    //Lectura del valor del campo oculto que discrimina entre Lecturas/Visor desde CaucePuntArbol
    //    var elem=window.parent.frames['iframeArbol'].document.getElementById("h_lecturasvisor").value;
    //    document.getElementById('lblLecturasVisor').text = elem;
    //    alert(elem);
    
        document.getElementById('lblLecturasVisor').value = window.parent.document.getElementById('h_lecturasvisor').value;
  
        alert(window.parent.document.getElementById('h_lecturasvisor').value);
    }
    
    
    function filtrarAcequias(){
    if (document.getElementById("filaFiltro").style.display=="none"){
        document.getElementById("filaFiltro").style.display="block" ;
        document.getElementById("MostrarFAcequia").innerText= "Ocultar Filtro";
                
    }else{
        document.getElementById("filaFiltro").style.display="none";
        document.getElementById("MostrarFAcequia").innerText= "Mostrar Filtro";
        }
    }
    function filtrarMotores(){
    if (document.getElementById("FilaFiltroM").style.display=="none"){
        document.getElementById("FilaFiltroM").style.display="block" ;
        document.getElementById("MostrarFiltroM").innerText= "Ocultar Filtro";
                
    }else{
        document.getElementById("FilaFiltroM").style.display="none";
        document.getElementById("MostrarFiltroM").innerText= "Mostrar Filtro";
        }
    }
    
    function filtrarEnergia(){
    if (document.getElementById("FilaFiltroE").style.display=="none"){
        document.getElementById("FilaFiltroE").style.display="block" ;
        document.getElementById("MostrarFiltroE").innerText= "Ocultar Filtro";
                
    }else{
        document.getElementById("FilaFiltroE").style.display="none";
        document.getElementById("MostrarFiltroE").innerText= "Mostrar Filtro";
        }
    }
    
    function filtrarHorometro(){
    if (document.getElementById("FilaFiltroH").style.display=="none"){
        document.getElementById("FilaFiltroH").style.display="block" ;
        document.getElementById("MostrarFiltroH").innerText= "Ocultar Filtro";
                
    }else{
        document.getElementById("FilaFiltroH").style.display="none";
        document.getElementById("MostrarFiltroH").innerText= "Mostrar Filtro";
        }
    }
    function filtrarDiferencial(){
    if (document.getElementById("FilaFiltroD").style.display=="none"){
        document.getElementById("FilaFiltroD").style.display="block" ;
        document.getElementById("MostrarFiltroD").innerText= "Ocultar Filtro";
                
    }else{
        document.getElementById("FilaFiltroD").style.display="none";
        document.getElementById("MostrarFiltroD").innerText= "Mostrar Filtro";
        }
    }
    
    function CargarAnyosQ(AnyoInicio, AnyoFinal)
    {
        /* RDF 30/07/2008. Carga el año hidrológico en las cajas*/
            //document.getElementById("txtFiltroNregQ").value="";
            document.getElementById("txtFiltroFechaIni").value=AnyoInicio;
            document.getElementById("txtFiltroFechaFin").value=AnyoFinal;

    }
     function CargarAnyosE(AnyoInicio, AnyoFinal)
    {
     /* RDF 30/07/2008. Carga el año hidrológico en las cajas*/

        document.getElementById("txtFiltroFechaIniE").value=AnyoInicio;
        document.getElementById("txtFiltroFechaFinE").value=AnyoFinal;        
        //document.getElementById("txtFiltroNRegE").value="";

    }
    function CargarAnyosV(AnyoInicio, AnyoFinal)
    {
     /* RDF 30/07/2008. Carga el año hidrológico en las cajas*/
        //document.getElementById("txtFiltroNRegV").value="";
        document.getElementById("txtFiltroFechaIniV").value=AnyoInicio;
        document.getElementById("txtFiltroFechaFinV").value=AnyoFinal;

    }

    function CargarAnyosH(AnyoInicio, AnyoFinal)
    {
     /* RDF 30/07/2008. Carga el año hidrológico en las cajas*/
        //document.getElementById("txtFiltroNRegH").value="";
        document.getElementById("txtFiltrofechaIniH").value=AnyoInicio;
        document.getElementById("txtFiltroFechaFinH").value=AnyoFinal;

    }
  function CargarAnyosD(AnyoInicio, AnyoFinal)
    {
     /* RDF 30/07/2008. Carga el año hidrológico en las cajas*/
        //document.getElementById("txtFiltroNRegH").value="";
        document.getElementById("txtFiltrofechaIniD").value=AnyoInicio;
        document.getElementById("txtFiltroFechaFinD").value=AnyoFinal;

    }    
    
   
function abreInforme(control, nodoTexto, nodoClave, filtro) { 
var nombreinforme;
var CodigoPVYCR = nodoClave;
var IdElementoMedida = nodoTexto.substr(nodoTexto.indexOf("-") + 1, 3);    
var FiltroIntervalo;
var FiltroNreg; 
var FiltroFechaIni; 
var FiltroFechaFin;
var FiltroNulas;

if (document.getElementById("ddlIntervalo"))
    FiltroIntervalo = document.getElementById("ddlIntervalo").value;
else if (document.getElementById("ddlIntervaloE"))
    FiltroIntervalo = document.getElementById("ddlIntervaloE").value;
else if (document.getElementById("ddlIntervaloH"))
    FiltroIntervalo = document.getElementById("ddlIntervaloH").value;
else if (document.getElementById("ddlIntervaloV"))
    FiltroIntervalo = document.getElementById("ddlIntervaloV").value;


if (document.getElementById("txtFiltroNregQ"))
    FiltroNreg=document.getElementById("txtFiltroNregQ").value
else if (document.getElementById("txtFiltroNregE")) 
    FiltroNreg=document.getElementById("txtFiltroNregE").value
else if (document.getElementById("txtFiltroNregH")) 
    FiltroNreg=document.getElementById("txtFiltroNregH").value
else if (document.getElementById("txtFiltroNregV")) 
    FiltroNreg=document.getElementById("txtFiltroNregV").value
    
if (document.getElementById("txtFiltroFechaIni"))
    FiltroFechaIni = document.getElementById("txtFiltroFechaIni").value
else if (document.getElementById("txtFiltroFechaIniE")) 
    FiltroFechaIni = document.getElementById("txtFiltroFechaIniE").value
else if (document.getElementById("txtFiltroFechaIniH")) 
    FiltroFechaIni = document.getElementById("txtFiltroFechaIniH").value
else if (document.getElementById("txtFiltroFechaIniV")) 
    FiltroFechaIni = document.getElementById("txtFiltroFechaIniV").value
    
if (document.getElementById("txtFiltroFechaFin"))
    FiltroFechaFin = document.getElementById("txtFiltroFechaFin").value
else if (document.getElementById("txtFiltroFechaFinE")) 
    FiltroFechaFin = document.getElementById("txtFiltroFechaFinE").value
else if (document.getElementById("txtFiltroFechaFinH")) 
    FiltroFechaFin = document.getElementById("txtFiltroFechaFinH").value
else if (document.getElementById("txtFiltroFechaFinV")) 
    FiltroFechaFin = document.getElementById("txtFiltroFechaFinV").value

if (document.getElementById("chkFiltroNulasQ"))
    FiltroNulas=document.getElementById("chkFiltroNulasQ").checked
else if (document.getElementById("chkFiltroNulasE")) 
    FiltroNulas=document.getElementById("chkFiltroNulasE").checked
else if (document.getElementById("chkFiltroNulasH")) 
    FiltroNulas=document.getElementById("chkFiltroNulasH").checked
else if (document.getElementById("chkFiltroNulasV")) 
    FiltroNulas=document.getElementById("chkFiltroNulasV").checked

/* RDF Modificado 30/07/2008. La etiqueta es un link */
    
if ((control.id == 'imgVolDiariosA') || (control.id == 'btnVolDiariosA'))
{
    nombreinforme = "../listados/ListadoVolumenesDias.aspx?codigoPVYCR=" + CodigoPVYCR + "&idElementoMedida=" + IdElementoMedida + "&FiltroNreg=" + FiltroNreg + "&filtroFechaIni=" + FiltroFechaIni + "&FiltroFechaFin=" + FiltroFechaFin 
}
else if ((control.id == 'imgInformeA') || (control.id == 'btnInformeA'))
{
    //nombreinforme = "../listados/InformeVolumenes.aspx?codigoPVYCR=" + CodigoPVYCR + "&idElementoMedida=" + IdElementoMedida + "&FiltroNregQ=" + FiltroNregQ + "&filtroFechaIni=" + FiltroFechaIni + "&FiltroFechaFin=" + FiltroFechaFin + "&informe=xls" + "&FiltroNulasQ=" + FiltroNulasQ + "&Filtro=" + filtro 
    nombreinforme = "../listados/InformeVolumenes.aspx?nodotexto=" + nodoTexto + "&nodoclave=" + nodoClave + "&filtrointervalo=" + FiltroIntervalo + "&FiltroNreg=" + FiltroNreg + "&filtroFechaIni=" + FiltroFechaIni + "&FiltroFechaFin=" + FiltroFechaFin + "&informe=xls" + "&FiltroNulas=" + FiltroNulas + "&Filtro=" + filtro
}
else if (control.id == 'imgCurvasAcequias')
{
    nombreinforme = "../listados/ListadoCurvasAcequias.aspx?codigoPVYCR=" + CodigoPVYCR + "&idElementoMedida=" + IdElementoMedida + "&FiltroNreg=" + FiltroNreg + "&filtroFechaIni=" + FiltroFechaIni + "&FiltroFechaFin=" + FiltroFechaFin
}
else if ((control.id == 'imgComparativaCaudales') ||(control.id == 'btnComparativaCaudales'))
{
    //nombreinforme = "../listados/ListadoCaudalesFiltrados.aspx?nodotexto=" + nodoTexto + "&nodoclave=" + nodoClave + "&filtrointervalo=" + FiltroIntervalo + "&FiltroNreg=" + FiltroNreg + "&filtroFechaIni=" + FiltroFechaIni + "&FiltroFechaFin=" + FiltroFechaFin + "&informe=xls" + "&FiltroNulas=" + FiltroNulas + "&Filtro=" + filtro
}
window.open(nombreinforme);
}

function SelDesSelTodas(tipo){
    var elementos = document.getElementById('Form1').elements;
    if (tipo == 'E'){
        if (document.getElementById("chkFEnergia").checked){ 
        for (i=0;i<elementos.length;i++){
            if ((elementos[i].id.indexOf('chkEnergia')>=0)&&(!elementos[i].checked))
                elementos[i].click();
        }
        }
        else{
            for (i=0;i<elementos.length;i++){

              if ((elementos[i].id.indexOf('chkEnergia')>=0)&&(elementos[i].checked))
                elementos[i].click();
        }
        }
    }
    else if (tipo == 'V'){
        if (document.getElementById("chkFMotor").checked){ 
        for (i=0;i<elementos.length;i++){
            if ((elementos[i].id.indexOf('chkMotor')>=0)&&(!elementos[i].checked))
                elementos[i].click();
        }
        }
        else{
            for (i=0;i<elementos.length;i++){

              if ((elementos[i].id.indexOf('chkMotor')>=0)&&(elementos[i].checked))
                elementos[i].click();
        }
        }
    
    }
    else if (tipo == 'A'){
        if (document.getElementById("chkFAcequias").checked){ 
        for (i=0;i<elementos.length;i++){
            if ((elementos[i].id.indexOf('chkAcequias')>=0)&&(!elementos[i].checked))
                elementos[i].click();
        }
        }
        else{
            for (i=0;i<elementos.length;i++){

              if ((elementos[i].id.indexOf('chkAcequias')>=0)&&(elementos[i].checked))
                elementos[i].click();
        }
        }
    
    }
     else if (tipo == 'H'){
        if (document.getElementById("chkFHorometro").checked){ 
        for (i=0;i<elementos.length;i++){
            if ((elementos[i].id.indexOf('chkHorometro')>=0)&&(!elementos[i].checked))
                elementos[i].click();
        }
        }
        else{
            for (i=0;i<elementos.length;i++){

              if ((elementos[i].id.indexOf('chkHorometro')>=0)&&(elementos[i].checked))
                elementos[i].click();
        }
        }
    
    }
    else if (tipo == 'D'){
        if (document.getElementById("chkFDiferencial").checked){ 
        for (i=0;i<elementos.length;i++){
            if ((elementos[i].id.indexOf('chkDiferencial')>=0)&&(!elementos[i].checked))
                elementos[i].click();
        }
        }
        else{
            for (i=0;i<elementos.length;i++){

              if ((elementos[i].id.indexOf('chkDiferencial')>=0)&&(elementos[i].checked))
                elementos[i].click();
        }
        }     
    
    }
}
     function mostrarIframe(nombreFrame)
    {
        if (nombreFrame == 'iframeInfC')
        {   
            window.parent.document.getElementById('iframeInfC').style.display = '';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }
        else if (nombreFrame == 'iframeInfP')
        {
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = '';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';

        }
        else if (nombreFrame == 'iframeInfE')
        {
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = '';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        }        
      else if (nombreFrame == 'iframeinformes')
        {
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = '';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
        } 
      else if (nombreFrame == 'iframeLecturas')
        {
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = '';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = 'none';
         }
      else if (nombreFrame == 'iframeDetalleVisor')
        {
            window.parent.document.getElementById('iframeInfC').style.display = 'none';
            window.parent.document.getElementById('iframeInfP').style.display = 'none';
            window.parent.document.getElementById('iframeInfE').style.display = 'none';
            window.parent.document.getElementById('iframeinformes').style.display = 'none';
            window.parent.document.getElementById('iframeLecturas').style.display = 'none';
            window.parent.document.getElementById('iframeDetalleVisor').style.display = '';

        }                                     
        
    }
    

    var elTimeoutTS;
    var controlLecturasSel;     
     
     function almacenarSesTareasSeleccionadas(){
        document.frames['frameUtil'].navigate('CaucePuntDETALLE_SalvaSeleccion.aspx?CondicionLecturas='+controlLecturasSel.value);
    }
       
    function clickTarea(sender,Fecha_Medida,Cod_Fuente_Dato){
        window.clearTimeout(elTimeoutTS);
        controlLecturasSel = document.getElementById('hidLecturasSeleccionadas');
        
        if (sender.checked){            
            controlLecturasSel.value += " (D.Fecha_medida='" + Fecha_Medida + "' AND D.Cod_Fuente_dato='" + Cod_Fuente_Dato + "') OR";                        
        }
        else{
            controlLecturasSel.value = controlTareasSel.value.replace(" (D.Fecha_medida='" + Fecha_Medida + "' AND D.Cod_Fuente_dato='" + Cod_Fuente_Dato + "') OR",'');                        
        }
        elTimeoutTS = window.setTimeout('almacenarSesTareasSeleccionadas()',1200);
    }

//function deSeleccionarTodasTareas(){
//    var elementos = document.getElementById('Form1').elements;
//    document.getElementById('hidTareasSeleccionadas').value = '';
//    for (i=0;i<elementos.length;i++){
//        if ((elementos[i].id.indexOf('cbxTarea')>=0)&&(elementos[i].checked))
//            elementos[i].click();
//    }
//}

//ncm 12/02/2009
 function confirmar_borrado()
    {
      if (confirm("¿Está seguro de la eliminación de la lectura?")==true)
        return true;
      else
        return false;
    }
 //ncm mostrar tipos de código fuente dato cuando hagan click sobre la columna del código fuente dato
 function mostrarInformacion(tipo){
    if(tipo=="C")
        window.open("TiposCodFuenteDato.aspx","mostrarInformacion", "resizable=0,toolbar=no,menubar=no,top=300,left=350,height=337,width=369");
        else
        window.open("TiposObtencionCaudal.aspx","mostrarInformacion", "resizable=0,toolbar=no,menubar=no,top=300,left=345,height=125,width=336");
  }    
</script> 

    <link href="../styles.css" rel="stylesheet" />
</head>
<body style="background-color:White">
<iframe name="frameUtil" id="frameUtil" src="" style="display:none"></iframe>
  <form id="Form1" method="post" runat="server">
  <asp:HiddenField ID="hidLecturasSeleccionadas" runat="server" EnableViewState="true" />  
  <span id="dsp4"></span>
  <span id="imagepath" style="display:none">../js/calendar/images</span>
    <table cellspacing="0" align="center" width="97%" style="border: 0px solid #666666;
      background-color: white" height="100%">
      <tr>
        <td style="width: 915px" height="100%" >
          <table cellspacing="0" cellpadding="0" width="100%" height="100%">
            <!--<asp:Label ID="lblCabecera" runat="server"></asp:Label><asp:Label ID="lblPestanyas" runat="server"></asp:Label>  -->
            <tr><td height=100% valign=middle>
            <asp:Label ID="lblpresentacion" runat="server"></asp:Label>
            </td></tr>
            <!-- Celda Menú - Contenido -->
            <tr>          
              <td>
                 <table>
                     <tr>
                       <asp:Label ID="lblPestanyasArbolQ" runat="server"></asp:Label><td width=50% visible="false">
                           <strong>
                            <asp:TextBox ID="txtCodigoPVYCR" runat="server" CssClass="texto" ToolTip="CodigoPVYCR"
                                   Width="115px" Visible="false">[CodigoPVYCR]</asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCodigoPVYCR"
                                   Display="Dynamic" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator></strong>
                                   <asp:TextBox ID="txtIdElementoMedida" runat="server" CssClass="texto" Width="27px" ToolTip="Id Elemento Medida" Visible="false">[EM]</asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtIdElementoMedida" Display="Dynamic">*</asp:RequiredFieldValidator>&nbsp;
                              <asp:TextBox ID="txtDescripcionElementoMedida" runat="server" CssClass="texto" Visible="False"
                               Width="44px"></asp:TextBox><asp:Label ID="LBLinfo" runat="server" Width="0px" Visible="false"></asp:Label>
                           
                            <b><asp:LinkButton ID="lbtAceptar" runat="server" Text="Búsqueda Rápida" CssClass="enlaceLecturas" Visible="false"></asp:LinkButton></b>
                              <asp:ImageButton ID="imgVisor" runat="server" ImageUrl="~/SICAH/images/iconos/imgVisor.gif"
                               ImageAlign="TextTop" Visible="false"/>
                         </td>
                               
                                               
                      </tr>
                  </table>
              </td>
                         
             </tr>
             <!-- Fin celda1 contenido arbol-->            
            
            <!--<tr>
              <td style="width: 915px">
                  </td>
               </tr>-->
             
                <tr>

                    <td style="padding-left:5px; width: 915px;border:1px solid #666666" valign=top >
                        <!-- Panel independiente en la cabecera para mostrar los listados -->
                        <asp:Panel ID="pnlIndependiente" runat="server" Visible="true" Height="635px">
                            <table width="99%">                            
                            <tr>
                                <td style="background-color:#cccccc; border:0px solid #666666;">
                                <table align="left">
                                <tr>                                        
                                    <td nowrap valign="top"> Fecha Desde: 
                                      <asp:TextBox ID="txtFiltroFechaIniI" runat="server" CssClass="texto" Width="75px"></asp:TextBox>
                                      <asp:CompareValidator ID="cvFiltroFechaIniI" runat="server" Text="*" ErrorMessage="Fecha no válida" ControlToValidate="txtFiltroFechaIniI" Operator="DataTypeCheck" Type="date"></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaIniI" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                                                            
                                    </td>
                                    <td nowrap valign="top" style="width: 158px"> Hasta: 
                                      <asp:TextBox ID="txtFiltroFechaFinI" runat="server" CssClass="texto" Width=75px></asp:TextBox>
                                      <asp:CompareValidator ID="cvtxtFiltroFechaFinI" runat="server" Text="*" ErrorMessage="Fecha no válida" ControlToValidate="txtFiltroFechaFinI" Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaFinI" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      
                                    </td>                                                                                                  
                                    <td>Intervalo:                               
                                    <asp:DropDownList ID="ddlIntervaloI" runat="server" AutoPostBack="false" Style="font: 10px Verdana; text-align: right" Visible="true">
                                        <asp:ListItem Text="Diario" Value="d" Selected=True></asp:ListItem>
                                        <asp:ListItem Text="Mensual" Value="m"></asp:ListItem>
                                        <asp:ListItem Text="Año" Value="a"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Button ID="btnCaudalesFiltradosI" runat="server" text="Caudales Filtrados" CssClass="boton" Visible="true"/></td></tr>
                                </table>
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                        <!-- Fin del panel independiente -->
                        <!-- Panel energia -->    
                        <asp:Panel ID=pnlEnergia Runat=server Visible=false style="overflow:auto;height: 692px; width:100%;">
                        <table width="99%">                                
                            <tr>
                                <td class="tituloLecturas" colspan=15><asp:Label ID="lblObtenerNomElementoE" runat="server"></asp:Label></td>
                                <td colspan="1" bgcolor="#FEFCC1" align="right"><asp:CheckBox ID="chkReducirLecE" runat="server" Visible="true" Checked="false" Text="Reducir Lecturas" /></td>
                            </tr>
                        </table>
                        <table width="99%" bgcolor="#BFBDC0" >
                            <tr>
                                <td class="filtrosCabecera" colspan="2">Intervalo de datos: <asp:Label ID="lblIntervaloFechaE" Font-Bold="true" runat="server"></asp:Label></td>
                                <td><asp:Label ID="lblLectPantallaE" runat="server"></asp:Label></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="filtrosCabecera">Total Acumulado (Kwh): <asp:Label ID="lblobtenervolumenacumulado" runat="server" Font-Bold="true"></asp:Label></td>
                                <td class="filtrosCabecera" style="color:Maroon; font:bold">Total Acumulado (m3): <asp:Label ID="lblobtenervolumenacumuladoE" Font-Bold="true" runat="server"></asp:Label></td>
                                <td>
                                    <!--<asp:Image runat="server" ImageUrl="~/SICAH/images/XLS.gif" ID="imgInformeE" Style="cursor: pointer" />-->
                                    <asp:LinkButton ID="btnModificarLecturasE" ToolTip="Fichero excel enriquecido" runat="server"><img src="images/ExcelIconBig.gif" border="0" /></asp:LinkButton>                                    
                                    <asp:LinkButton ID="btnModificarLecturasEPlano" ToolTip="Fichero excel plano" runat="server"><img src="images/Icon_edit.gif" border="0" /></asp:LinkButton>                                    
                                    <asp:LinkButton ID="btnInformeE"  runat="server" Text="Informe de lecturas" CssClass="enlaceLecturas"  />                                      
                                </td>
                                <td>
                                    <asp:Image runat="server" ImageUrl="~/SICAH/images/iconos/Grafica2.gif" ID="imgGraficoE" Style="cursor: pointer" />
                                    <asp:LinkButton ID="btnGraficoE"  runat="server" Text="Gráfico de consumos" CssClass="enlaceLecturas"  />                                      
                                </td>
                            </tr>
                            <tr>
                                <td class="filtrosCabecera">Total lecturas cargadas: <asp:Label ID="lblobtenerNumlecturasE" runat="server" Font-Bold="true"></asp:Label></td>
                                <td class="filtrosCabecera">Total lecturas existentes: <asp:Label ID="lblobtenerTotNumLecturasE" runat="server" Font-Bold="true"></asp:Label> </td>                                
                                <td>
                                    <asp:Image runat="server" ImageUrl="~/SICAH/images/XLS.gif" ID="imgComparativaCaudalesEXLS" Style="cursor: pointer" Visible="true" />                              
                                    <asp:LinkButton ID="btnComparativaCaudalesEXLS"  runat="server" Text="Informe de Consumos" CssClass="enlaceLecturas" />                                    
                                </td>
                                <td>
                                    <asp:Image runat="server" ImageUrl="~/SICAH/images/PDF.gif" ID="imgComparativaCaudalesE" Style="cursor: pointer" Visible="true" />                              
                                    <asp:LinkButton ID="btnComparativaCaudalesE"  runat="server" Text="Informe de Consumos" CssClass="enlaceLecturas" />                                    
                                        <asp:DropDownList ID="ddlIntervaloE" runat="server" AutoPostBack="false" Style="font: 10px Verdana; text-align: right" Visible="true">
                                        <asp:ListItem Text="Diario" Value="d" Selected=True></asp:ListItem>
                                        <asp:ListItem Text="Mensual" Value="m"></asp:ListItem>
                                        <asp:ListItem Text="Año" Value="a"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>  
                              <tr>
                                    <td class="filtrosCabecera">Concesión del Aprovechamiento:
                                       <asp:Label ID="lblVolMaxAnuLegE" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td class="filtrosCabecera">% Consumido:
                                       <asp:Label ID="lblPorConsumidoE" runat="server" Font-Bold="true"></asp:Label>
                                    </td>

                                </tr>           
                            <tr style="display:<%=VisibleSiAdmin() %>" >                                                                
                                <td colspan="2">
                                    <asp:LinkButton ID="btnDescargarArchivoE" runat="server" Text="Descargar archivo para ser modificado" CssClass="enlaceLecturas"></asp:LinkButton>
                                </td>
                                <td colspan="2">Subir Lecturas                                    
                                <input type="File" id="txtUploadE" runat="Server" NAME="txtUploadE" class="boton"> 
                                <asp:Button id="btnSubirLecturasE" runat="server" Text="Aceptar"  CssClass="boton"></asp:Button>                                    
                                </td>
                            </tr>    
                                                                                                        
                        </table>    
                        <table width="99%" id="TablaFiltrosE" bgcolor="#FEFCC1">
                            <asp:Label ID="lblidElemento" runat="server" Visible=false></asp:Label><tr>
                                <td bgcolor="#FEFCC1" style=" color:Red">
                                    <asp:LinkButton ID="lblAnyoHidrologicoE" runat="server" CssClass="enlaceLecturas" ></asp:LinkButton>
                                </td>
                           
                                    <td nowrap bgcolor="#FEFCC1">
                                        <asp:Label ID="lblFiltroNRegE" Text="Registros a Mostrar:" CssClass="texto" runat="server" Visible="False" ></asp:Label>
                                        <asp:TextBox ID="txtFiltroNRegE" runat="server" CssClass="texto" Width=50px Visible="false"></asp:TextBox>
                                        <asp:Label ID="lblFiltroNRegPagE" Text="Registros por página:" runat="server" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtFiltroNRegPagE" runat="server" CssClass="texto" Width="50px" Text="20" Visible="False"></asp:TextBox>                                    
                                    
                                    <asp:CompareValidator ID="cvNregE" runat="server" Text="*" ErrorMessage="Sólo números" ControlToValidate="txtFiltroNRegE" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>                                    
                                    </td>                                        
                                    <!--<td nowrap>
                                        <asp:CheckBox ID= chkFiltroPorDiaE runat=server Checked=false Text="Una Lec. Por Día" TextAlign=Left />
                                    </td>  --> 
                                    <!-- 
                                    <td nowrap>
                                        <asp:Label ID="lblFiltroUnoCadaXE" Text="Mostrar uno de cada:" CssClass="texto" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtFiltroUnoCadaXE" runat="server" CssClass="texto" Width=50px></asp:TextBox>
                                    

                                    </td>
                                        -->                                                            
                                                                                 
                                   <td nowrap bgcolor="#FEFCC1">
                                    <asp:TextBox ID="txtFiltrarCodFuenteDatoE" runat="server" CssClass="texto" Width=75px Visible=false></asp:TextBox>
                                    </td>
                             
                                    <td nowrap bgcolor="#FEFCC1">
                                      <asp:TextBox ID="txtFiltroFechaIniE" runat="server" CssClass="texto" Width=75px>[Fecha Desde]</asp:TextBox>
                                      <asp:CompareValidator ID=cvFIE runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaIniE Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaIniE" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      
                                    </td>
                          
                                    <td nowrap bgcolor="#FEFCC1">
                                      <asp:TextBox ID="txtFiltroFechaFinE" runat="server" CssClass="texto" Width=75px>[Fecha Hasta]</asp:TextBox>
                                      <asp:CompareValidator ID=cvFFE runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaFinE Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaFinE" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      

                                    </td>
                              
                                    <td nowrap bgcolor="#FEFCC1"> Mostrar Nulos
                                      <asp:CheckBox ID="chkFiltroNulasE" runat="server" CssClass="texto" Checked="false"/>
                                    </td>        
                              
                                  <td nowrap bgcolor="#FEFCC1">
                                    <asp:LinkButton ID="lbtFiltroAceptarE" runat="server" Text="Filtrar" CssClass="enlaceLecturas"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtFiltroCancelarE" runat="server" Text="Limpiar" CssClass="enlaceLecturas"></asp:LinkButton>
                                    
                                    <asp:Button ID="btnFiltroAceptarE" runat="server" text="Filtrar" CssClass="boton" Visible=false />
                                    <asp:Button ID="btnFiltroCancelarE" runat="server" Text="Limpiar" CssClass="boton" Visible=false />
                                    <asp:Button ID="btnCancelarE" runat="server" Text="Cancelar" CssClass="boton" Visible=false />
                                    </td>
                                
                             </tr>                            
                             </table>
                            <table id="TABLE3" width="99%">                              
                                   
                       
                                    <tr>
                                        <td  align="right" colspan="15"   >
                                        <a class="filtro" id = "MostrarFiltroE" href="javascript:void(0)"  onclick="javascript:filtrarEnergia()"  >Ocultar Filtro</a>
                                        </td>
                                            
                                     </tr>
                                    
                                    <tr>
                                    <!--<td class="encabListado">Código PVYCR</td>-->
                                    <td class="encabListado"></td>
                                    <td class="encabListado" style="width:10px" onclick="javascript:mostrarInformacion('C')">Código Fuente Dato</td>
                                    <td class="encabListado">Fecha Medida</td>
                                    <td class="encabListado" style="width:10px">Lectura I</td>
                                    <td class="encabListado" style="width:10px">Lectura II</td>
                                    <td class="encabListado" style="width:10px">Lectura III</td>
                                    <td class="encabListado" style="width:10px">Total (Kwh)</td>
                                    <td class="encabListado" style="width:10px;color:Maroon;font:bold;">Parcial (m3)(*)</td>
                                    <td class="encabListado" style="width:10px;color:Maroon;font:bold">Caudal Medio (m3/s)(*)</td>
                                    <td class="encabListado" style="width:10px">Total (Kvar)</td>
                                    <td class="encabListado" style="width:11px">Funciona</td>
                                    <td class="encabListado" style="width:60px">Incidencia Eléctrica</td>
                                    <td class="encabListado" style="width:10px; display:none">Consumo Eléctrico Adicional</td>
                                    <td class="encabListado" style="width:53px; display:none">Reinicio Lectura Eléctrica</td>
                                    <td class="encabListado" style="width:87px">Observaciones</td>
                                    <td class="encabListado" style="width:5px; font-size:5px">&nbsp;</td>                                    
                                    </tr>                                    
                                    
                                    <tr id="FilaFiltroE" >   
                                        <td class="encabListado">  
                                              <asp:CheckBox ID="chkFEnergia" runat="server" Visible="true" EnableViewState="true" onclick="javascript:SelDesSelTodas('E')" />
                                        </td>
                                         <td class="encabListado">
                                         <asp:DropDownList  Style="font:10px Verdana; text-align: right" Width="30px" ID="ddlFfuentedatosE" runat="server" AutoPostBack="false" >
                                         </asp:DropDownList>
                                         </td>                                                 
                                         <td class="encabListado" style="width:130px"><asp:TextBox runat="server" ID="txtFfechamedidaE" CssClass="texto" style="width:64px" ></asp:TextBox>
                                            <asp:Image    ID="imgFfechamedidaE" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" />
                                            <asp:CompareValidator ID="ComFfechamedidaE" runat="server" ControlToValidate="txtFfechamedidaE"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                         </td>  
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFlecturaIE" CssClass="texto" style="width:20px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtFlecturaIE"
                                         ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFlecturaIIE" CssClass="texto" style="width:28px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtFlecturaIIE"
                                         ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFlecturaIIIE" CssClass="texto" style="width:28px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtFlecturaIIIE"
                                         ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFtotalkwE" CssClass="texto" style="width:22px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtFtotalkwE"
                                         ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFdiferencialE" CssClass="texto" style="width:10px; display:none" ></asp:TextBox></td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFdiferencialES" CssClass="texto" style="width:10px; display:none"></asp:TextBox></td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFtotalkvarE" CssClass="texto" style="width:30px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtFtotalkvarE"
                                         ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>


                                         <td class="encabListado" style="width: 11px"><asp:TextBox runat="server" ID="txtFfuncionaE" CssClass="texto" style="width:15px"></asp:TextBox></td> 
                                         <td class="encabListado" style="width: 60px"><asp:TextBox runat="server" ID="txtFincidenciaE" CssClass="texto" style="width:30px"></asp:TextBox><asp:CompareValidator ID=ComFincidenciaE runat="server"  Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtFincidenciaE Operator=DataTypeCheck Type=integer></asp:CompareValidator></td> 
                                         <td class="encabListado" style="display:none"><asp:TextBox runat="server" ID="txtFconsumoE" CssClass="texto" style="width:50px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtFconsumoE"
                                         ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>  
                                         <td class="encabListado" style="width: 53px; display:none"><asp:TextBox runat="server" ID="txtFreinicioE" CssClass="texto" style="width:30px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFreinicioE"
                                         ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>  
                                         <td class="encabListado" style="width: 87px"><asp:TextBox runat="server" ID="txtFobservacionesE" CssClass="texto" style="width:70px"></asp:TextBox></td> 

                                         <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsRegE" Runat="server" onclick="AceptarFiltroEnergia" CssClass="enlaceLecturas">Acep.</asp:LinkButton></td> 
                                         
                                    </tr> 

                                    <asp:Repeater ID="rptEnergia" runat="server">                                                     
                                    <ItemTemplate>
                                        <tr id="rowEnergia"  <%# checkFila("E",Container.DataItem)%>>
                                            <!--<td nowrap><%#Container.DataItem("codigoPVYCR")%></td>-->
                                            <td align=left>
                                              <asp:CheckBox ID="chkEnergia" runat="server" EnableViewState="true" />
                                            </td>
                                            <td title="<%#VerCodFuenteDato(Container.DataItem)%>" onclick="javascript:mostrarInformacion('C')"><%#Container.DataItem("Cod_fuente_dato")%> </td>
                                            <td><%#Container.DataItem("fecha_Medida")%></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem, "LecturaI", "{0:#,##0.##}")%></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem, "LecturaII", "{0:#,##0.##}")%></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem, "LecturaIII", "{0:#,##0.##}")%></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem, "Total_Kwh", "{0:#,##0.##}")%></td>
                                            <td align="right" style="color:Maroon;"><%#DataBinder.Eval(Container.DataItem, "Diferencial", "{0:#,##0}")%></td>
                                            <td align="right" style="color:Maroon"><%#DataBinder.Eval(Container.DataItem, "Diferencial_Seg", "{0:#,##0}")%></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem, "Total_Kvar", "{0:#,##0.##}")%></td>
                                            <td><%#Container.DataItem("Funciona")%></td>
                                            <td><%#Container.DataItem("descIncElec")%></td>
                                            <td  align="right" style="display:none"><%#DataBinder.Eval(Container.DataItem, "ConsumoElectricoAdicional", "{0:#,##0.##}")%></td>
                                            <td align="right" style="display:none"><%#DataBinder.Eval(Container.DataItem,"ReinicioLecturaElectrica", "{0:#,##0.##}")%></td>
                                            <td title="<%#VerObservaciones(Container.DataItem)%>" ><%#VerObservacionesReducidas(Container.DataItem)%></td>
                                            <td nowrap><asp:LinkButton ID="lbtEditarE" Visible="<%#VisibleSegunPerfil() %>" Runat="server" CommandName='editar' CommandArgument='<%#"" & container.dataitem("Cod_fuente_dato")& "#" &  container.dataitem("Fecha_Medida")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtBorrarE" Visible="<%#VisibleSegunPerfil() %>" Runat="server" CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("Cod_fuente_dato")& "#" &  container.dataitem("Fecha_Medida")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar datos"></asp:LinkButton>
                                            <asp:Image ID="Image5" runat="server" Visible="<%#VisibleSiModificado(Container.DataItem) %>" ImageUrl="images/Circulo.gif"  ToolTip="Modificado" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                                
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=16>               
                                  <uc:paginacion ID="ucPaginacionE" runat="server" Visible="true" />        
                                  </td>
                              </tr>
                              <tr><td colspan="16" class="encabListadoValEstimados">(*) Los valores que se muestran son valores estimados</td>
                              </tr>
                               <!-- Fin Número de páginas -->   
                                              
                            </table>
                            
                        </asp:Panel> 
                        <!-- Fin Panel listar Energia' -->
                        <!--Panel listar Acequias -->
                        <asp:Panel ID="pnlAcequias" Runat="server" Visible="false" BorderColor="green" style="overflow:auto;height: 692px; width:100%;">
                            <table width="99%" bgcolor="#FEFCC1">  
                                <tr>
                                    <td nowrap class="tituloLecturas" colspan=10><asp:Label ID="lblObtenerNomElementoA" runat="server"></asp:Label></td>
                                    <td nowrap colspan="2" align=right style="width: 284px" visible="false">
                                        <asp:Label Visible=false ID="txtGrafica" runat="server" Font-Bold="True" Height="0px" Text="Comparativa de Caudales por Fuente de Dato"></asp:Label>&nbsp;<strong> </strong>
                                        <asp:ImageButton Visible=false ID="imgGrafica" runat="server" Height="13px" ImageUrl="~/SICAH/images/iconos/Grafica2.gif" Width="19px" ImageAlign="TextTop" /><br />
                                    </td>
                                    <td colspan="1" bgcolor="#FEFCC1" align="right"><asp:CheckBox ID="chkReducirLecQ" runat="server" Visible="true" Checked="false" Text="Reducir Lecturas" /></td>
                                </tr>
                            </table>

                            <table width="99%" bgcolor="#BFBDC0">
                                <tr>
                                    <td colspan="2" class="filtrosCabecera">Intervalo de datos: <asp:Label ID="lblIntervaloFechasQ" Font-Bold="true" runat="server"></asp:Label></td>
                                    <td align="left" width="250px">
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/iconos/Grafica2.gif" ID="imgGraficoLecturas" Style="cursor: pointer" />
                                        <asp:LinkButton ID="btnGraficoLecturas" runat="server" Text="Gráfico de lecturas" CssClass="enlaceLecturas" /> 
                                    </td> 
                                    <td align="left" >
                                        <!--<asp:Image runat="server" ImageUrl="~/SICAH/images/XLS.gif" ID="imgInformeA" Style="cursor: pointer" />    -->
                                        <asp:LinkButton ID="btnModificarLecturas" ToolTip="Fichero excel enriquecido" runat="server"><img src="images/ExcelIconBig.gif" border="0" /></asp:LinkButton>                                    
                                        <asp:LinkButton ID="btnModificarLecturasPlano" ToolTip="Fichero excel plano" runat="server"><img src="images/Icon_edit.gif" border="0" /></asp:LinkButton>                                    
                                        <asp:LinkButton ID="btnInformeA" runat="server" Text="Informe de lecturas" CssClass="enlaceLecturas" /> 
                                    </td> 
                                </tr>
                                <tr>
                                    <td class="filtrosCabecera">Total lecturas cargadas: <asp:Label ID="lblobtenerNumLecturasQ" Font-Bold="true" runat="server"></asp:Label></td>
                                    <td class="filtrosCabecera">Total lecturas existentes: <asp:Label ID="lblobtenerTotNumLecturasQ" Font-Bold="true" runat="server"></asp:Label></td>
                                    <td align="left" >
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/iconos/Grafica2.gif" ID="imgGrafico" Style="cursor: pointer" />
                                        <asp:LinkButton ID="btnGrafico" runat="server" Text="Gráfico de consumos" CssClass="enlaceLecturas" /> 
                                    </td> 
                                    <td align="left" >
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/XLS.gif" ID="imgComparativaCaudalesXLS" Style="cursor: pointer" Visible="true" /> 
                                        <asp:LinkButton ID="btnComparativaCaudalesXLS" runat="server" Text="Informe de Consumos" CssClass="enlaceLecturas" /> 
                                    </td> 
                                </tr>
                                <tr>
                                    <td class="filtrosCabecera">Total acumulado (m3): <asp:Label ID="lblCaudalAcumuladoQ" Font-Bold="true" runat="server"></asp:Label></td> 
                                    <td><asp:Label runat="server" ID="lblLectPantallaQ"></asp:Label></td>
                                    <td class="filtrosCabecera">
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/iconos/Grafica2.gif" ID="Image1" Style="cursor: pointer" />
                                        <asp:LinkButton ID="btnGraficoCurvas" runat="server" Text="Gráfico curvas acequias" CssClass="enlaceLecturas" /> 
                                    </td>
                                    <td class="filtrosCabecera" align="left"> 
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/PDF.gif" ID="imgComparativaCaudales" Style="cursor: pointer" Visible="true" /> 
                                        <asp:LinkButton ID="btnComparativaCaudales" runat="server" Text="Informe de Consumos" CssClass="enlaceLecturas" /> 
                                        <asp:DropDownList ID="ddlIntervalo" runat="server" AutoPostBack="false" Style="font: 10px Verdana; text-align: right" Visible="true">
                                            <asp:ListItem Text="Diario" Value="d" Selected=True></asp:ListItem>
                                            <asp:ListItem Text="Mensual" Value="m"></asp:ListItem>
                                            <asp:ListItem Text="Año" Value="a"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>  
                                  <tr>
                                    <td class="filtrosCabecera">Concesión del Aprovechamiento:
                                       <asp:Label ID="lblVolMaxAnuLegQ" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td class="filtrosCabecera">% Consumido:
                                       <asp:Label ID="lblPorConsumidoQ" runat="server" Font-Bold="true"></asp:Label>
                                    </td>

                                </tr>                                                                          
                                <tr style="display:<%=VisibleSiAdmin() %>">
                                    <td colspan="2">
                                    <asp:LinkButton ID="btnDescargarArchivoQ" runat="server" Text="Descargar archivo para ser modificado" CssClass="enlaceLecturas"></asp:LinkButton>
                                    </td>
                                    <td colspan="2">Subir Lecturas                                    
                                    <input type="File" id="txtUpload" runat="Server" NAME="txtUpload" class="boton"> 
                                    <asp:Button id="btnSubirLecturas" runat="server" Text="Aceptar"  CssClass="boton"></asp:Button>                                    
                                    </td>
                                </tr>    
                                              
                            </table>

                     
                        <table width="99%" id="Table4" bgcolor="#FEFCC1">
                            <asp:Label ID="Label2" runat="server" Visible=false></asp:Label><tr bgcolor="#FEFCC1">
                                <td bgcolor="#FEFCC1" >
                                    <asp:Label ID="lblidElementoA" runat="server" Visible=false></asp:Label>
                                </td>
                                <td nowrap style=" color:Red">
                                <asp:LinkButton ID="lblObtenerAnyoHidrologicoQ" runat="server"  CssClass="enlaceLecturas" ></asp:LinkButton>
                                </td>
                                <td nowrap bgcolor="#FEFCC1">
                                        <asp:label id="lblFiltroNregQ" runat="server" CssClass="texto" Text="Registros a Mostrar" Visible="False"></asp:label>
                                         <asp:TextBox ID="txtFiltroNregQ" runat="server" CssClass="texto" Width="50px" Visible="false" ></asp:TextBox>                                    
                                        <asp:CompareValidator ID=cvnregQ runat=server Text=* ErrorMessage="Sólo números" ControlToValidate=txtFiltroNRegQ Operator=DataTypeCheck Type=Integer></asp:CompareValidator>                                    
                                        <asp:Label ID="lblFiltroNRegPagQ" Text="Registros por página:" runat="server" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtFiltroNRegPagQ" runat="server" CssClass="texto" Width="40px" Text="20" Visible="False"></asp:TextBox>                                    
                                     </td>                             
                                                                     
                                                                                 
                                <td nowrap bgcolor="#FEFCC1">
                                  
                                    <asp:TextBox ID="txtFiltrarCodFuentedato" runat="server" CssClass="texto" Width=75px Visible=false></asp:TextBox>
                                  
                                    </td>
                                <td nowrap valign="top"> 
                                      <asp:TextBox ID="txtfiltroFechaIni" runat="server" CssClass="texto" Width=75px>[Fecha Inicio]</asp:TextBox>
                                      <asp:CompareValidator ID=cvfechaini runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaIni Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaIniQ" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                                                            
                                    </td>
                                <td nowrap valign="top"> 
                                      <asp:TextBox ID="txtFiltroFechaFin" runat="server" CssClass="texto" Width=75px>[Fecha hasta]</asp:TextBox>
                                      <asp:CompareValidator ID=cvfechafin runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaFin Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaFinQ" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      
                                    </td>
                              
                                <td nowrap bgcolor="#FEFCC1" style="width: 90px;"> Mostrar Nulos
                                      <asp:CheckBox ID="chkFiltroNulasQ" runat="server" CssClass="texto" Checked="false"/>
                                    </td>        
                              
                                <td nowrap bgcolor="#FEFCC1">
                                    <asp:LinkButton ID="lbtFiltroAceptarA" runat="server" Text="Filtrar" CssClass="enlaceLecturas"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtFiltroCancelarA" runat="server" Text="Limpiar" CssClass="enlaceLecturas"></asp:LinkButton>
                                    <asp:Button ID="btnFiltroAceptarA" runat="server" text="Filtrar" CssClass="boton" Visible="false" />
                                    <asp:Button ID="btnFiltroCancelarA" runat="server" Text="Limpiar" CssClass="boton" Visible="false" />
                                    <asp:Button ID="btnCancelarA" runat="server" Text="Cancelar" CssClass="boton" Visible="false" />      
                                    </td>
                                
                             </tr>
                     
                         </table>                    
                              
                            <table id="TABLE1" width="99%" >                                                                                     
                                       <tr>
                                            <td  align="right" colspan="13" >
                                            <a class="filtro" id = "MostrarFAcequia" href="javascript:void(0)" onclick="javascript:filtrarAcequias()">Ocultar Filtro</a>
                                            </td>
                                            
                                        </tr>
 
                                        <tr>                                        
                                        <!--<td class="encabListado">codigo PVYCR</td>-->
                                        <td class="encabListado"></td>
                                        <td class="encabListado" style="width:100px">Fecha Medida</td>
                                        <td class="encabListado" style="width:10px" onclick="javascript:mostrarInformacion('T')" >Tipo Obtención Caudal</td>
                                        <td class="encabListado" style="width:10px" onclick="javascript:mostrarInformacion('C')">Código Fuente Dato</td>
                                        <td class="encabListado" style="width:30px">Escala (m)</td>
                                        <td class="encabListado" style="width:30px">Calado (m)</td>
                                        <td class="encabListado" style="width:30px">Régimen Curva</td>
                                        <td class="encabListado" style="width:50px">Nº. Parada</td>
                                        <td class="encabListado" style="width:50px">Caudal (m3/s)</td>
                                        <td class="encabListado" style="width:50px">Parcial (m3)</td>
                                        <td class="encabListado" style="width:50px">Duda Calidad</td>
                                        <td class="encabListado" style="width:50">Observaciones</td>
                                        <td class="encabListado" style="width:1px; font-size:1px">&nbsp;</td>
                                        </tr>
                                        
                                        <tr id="filaFiltro"    > 
                                              <td class="encabListado">  
                                              <asp:CheckBox ID="chkFAcequias" runat="server" Visible="true" EnableViewState="true" onclick="javascript:SelDesSelTodas('A')" />
                                               </td>                                                    
                                             <td class="encabListado"><asp:TextBox runat="server" ID="txtFfechamedida" CssClass="texto" Width="64px" ></asp:TextBox>&nbsp
                                                <asp:Image    ID="imgFfechamedida" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                                Style="cursor: pointer" />
                                                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtFfechamedida"
                                                Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                                Type="Date"></asp:CompareValidator>
                                             </td>  
                                             <td class="encabListado">
                                                <asp:DropDownList Style="font: 10px Verdana; text-align: right" Width=50px ID="ddlFobtencioncaudal" runat="server" AutoPostBack="false" >
                                                </asp:DropDownList>
                                             </td> 
                                             <td class="encabListado"    >
                                            <asp:DropDownList  Style="font: 10px Verdana; text-align: right"  Width=50px ID="ddlFfuentedatos" runat="server" AutoPostBack="false" >
                                                </asp:DropDownList>
                                             </td> 
                                             <td class="encabListado"><asp:TextBox runat="server" ID="txtFescala" CssClass="texto" style="width:30px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtFescala"
                                            ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>  
                                             <td class="encabListado"><asp:TextBox runat="server" ID="txtFcalado" CssClass="texto" style="width:30px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFcalado"
                                            ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>  
                                             <td class="encabListado"><asp:TextBox runat="server" ID="txtFregcurva" CssClass="texto" style="width:30px"></asp:TextBox></td> 
                                             <td class="encabListado"><asp:TextBox runat="server" ID="txtFnparada" CssClass="texto" style="width:50px"></asp:TextBox><asp:CompareValidator ID=CompareValidator2 runat="server"  Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtFnparada Operator=DataTypeCheck Type=integer></asp:CompareValidator></td>                                                                                  
                                             <td class="encabListado"><asp:TextBox runat="server" ID="txtFcaudal" CssClass="texto" style="width:50px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFcaudal"
                                            ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>   
                                             <td class="encabListado"><asp:TextBox runat="server" ID="txtFdiferencial" CssClass="texto" style="width:50px;display:none"></asp:TextBox></td> 
                                             <td class="encabListado"><asp:TextBox runat="server" ID="txtFduda" CssClass="texto" style="width:50px"></asp:TextBox><asp:CompareValidator ID=CompareValidator1 runat="server"  Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtFduda Operator=DataTypeCheck Type=integer></asp:CompareValidator></td>  
                                             <td class="encabListado"  ><asp:TextBox runat="server" ID="txtFobservaciones" CssClass="texto"  style="width:100px"></asp:TextBox></td>      
                                             <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsReg" Runat="server" onclick="AceptarFiltroAcequias" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                        </tr> 
                                   
                                    <asp:Repeater ID=rptAcequias runat=server> 
                                       
                                                      
                                    <ItemTemplate>
                                        <tr   id="rowAcequia" <%# checkFila("Q",Container.DataItem)%>>
                                            <td align=left>
                                              <asp:CheckBox ID="chkAcequias" runat="server"  EnableViewState="true"  />
                                            </td>
                                            <td><%#Container.DataItem("Fecha_Medida")%><asp:HiddenField runat="server" ID="Hid_Fecha_Medida" Value='<%#Container.DataItem("Fecha_Medida") %>' /></td>
                                            <td title="<%#VerTipoObtencionCaudal(Container.DataItem)%>" onclick="javascript:mostrarInformacion('T')" ><%#Container.DataItem("TipoObtencionCaudal")%></td>
                                            <td title="<%#VerCodFuenteDato(Container.DataItem)%>" onclick="javascript:mostrarInformacion('C')"><%#Container.DataItem("Cod_Fuente_Dato")%><asp:HiddenField runat="server" ID="Hid_Cod_Fuente_Dato" Value='<%#Container.DataItem("Cod_Fuente_Dato") %>' /></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem,"Escala_M","{0:#,##0.##}")%></td>
                                            <td align="right"><%#Container.DataItem("Calado_M")%></td>
                                            <td><%#Container.DataItem("RegimenCurva")%></td>
                                            <td><%#Container.DataItem("NumeroParada")%></td>
                                            <td align="right"><%#Container.DataItem("Caudal_M3S")%></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem, "Diferencial", "{0:#,##0}")%></td>  
                                            <td><%#Container.DataItem("Duda_Calidad")%></td>                                                                                                                                 
                                            <td title="<%#VerObservaciones(Container.DataItem)%>" ><%#VerObservacionesReducidas(Container.DataItem)%></td>
                                            <td><asp:LinkButton ID=lbtEditarQ Visible="<%#VisibleSegunPerfil() %>" Runat=server CommandName='editar' CommandArgument='<%#"" & container.dataitem("Cod_fuente_dato")& "#" &  container.dataitem("Fecha_Medida")& "#" & Container.DataItem("TipoObtencionCaudal")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtBorrarQ" Visible="<%#VisibleSegunPerfil() %>" Runat="server" CommandName='borrar' OnClientClick='return confirmar_borrado();'  CausesValidation="false" CommandArgument='<%#"" & container.dataitem("Cod_fuente_dato")& "#" &  container.dataitem("Fecha_Medida")& "#" & Container.DataItem("TipoObtencionCaudal")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar datos"></asp:LinkButton>
                                            <asp:Image ID="Image5" runat="server" Visible="<%#VisibleSiModificado(Container.DataItem) %>" ImageUrl="images/Circulo.gif"  ToolTip="Modificado" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=13>               
                                  <uc:paginacion ID="ucPaginacionA" runat="server" Visible="true" />        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                          </asp:Panel> 
                        <!-- Fin Panel listar Acequias' -->                                            
                        <!-- Panel Volumen -->    
                        <asp:Panel ID=pnlVolumen Runat=server Visible=false style="overflow:auto;height: 692px; width:100%;" >          
                    
                    
                            <table width="99%">
                            <asp:Label ID="lblidelementoV" runat="server" Visible=false></asp:Label><tr>
                                <td class="tituloLecturas"><asp:Label ID="lblobtenerNomElementoV" runat=server></asp:Label></td>
                                <td colspan="1" bgcolor="#FEFCC1" align="right"><asp:CheckBox ID="chkReducirlecV" runat="server" Visible="true" Checked="false" Text="Reducir Lecturas" /></td>
                            </tr>
                            </table>
                            <table width="99%" bgcolor="#BFBDC0" >
                                <tr>
                                    <td class="filtrosCabecera" colspan="2">Intervalo de datos: <asp:Label ID="lblIntervaloFechaV"  Font-Bold="true" runat="server"></asp:Label></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="filtrosCabecera">Total Acumulado (m3): <asp:Label ID="lblobtenervolumenacumuladoV" Font-Bold="true" runat="server"></asp:Label></td>
                                    <td class="filtrosCabecera">Total lecturas cargadas: <asp:Label ID="lblobtenerNumlecturasV" Font-Bold="true" runat="server"></asp:Label></td>
                                    <td nowrap>
                                        <!--<asp:Image runat="server" ImageUrl="~/SICAH/images/XLS.gif" ID="imgInformeV" Style="cursor: pointer" />-->
                                        <asp:LinkButton ID="btnModificarLecturasV"  runat="server" Text="Fichero excel enriquecido" ToolTip="Fichero excel enriquecido" CssClass="enlaceLecturas"><img src="images/ExcelIconBig.gif" border="0" /></asp:LinkButton>
                                        <asp:LinkButton ID="btnModificarLecturasVPlano" ToolTip="Fichero excel plano" runat="server"><img src="images/Icon_edit.gif" border="0" /></asp:LinkButton>                                    
                                        <asp:LinkButton ID="btnInformeV"  runat="server" Text="Informe de lecturas" CssClass="enlaceLecturas"  />                                      
                                    </td>   
                                    <td>
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/XLS.gif" ID="imgComparativaCaudalesVXLS" Style="cursor: pointer" Visible="true" />                              
                                        <asp:LinkButton ID="btnComparativaCaudalesVXLS"  runat="server" Text="Informe de Consumos" CssClass="enlaceLecturas" />                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td class="filtrosCabecera">Total lecturas existentes: <asp:Label ID="lblobtenerTotNumLecturasV"  Font-Bold="true" runat="server"></asp:Label> </td>
                                    <td><asp:Label runat="server" ID="lblLectPantallaV"></asp:Label></td>
                                    <td>
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/iconos/Grafica2.gif" ID="imgGraficaV" Style="cursor: pointer" />
                                        <asp:LinkButton ID="btnGraficoV"  runat="server" Text="Gráfico de consumos" CssClass="enlaceLecturas"  />                                      
                                    </td>
                                    <td nowrap>
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/PDF.gif" ID="imgComparativaCaudalesV" Style="cursor: pointer" Visible="true" />                              
                                        <asp:LinkButton ID="btnComparativaCaudalesV"  runat="server" Text="Informe de Consumos" CssClass="enlaceLecturas" />                                    
                                            <asp:DropDownList ID="ddlIntervaloV" runat="server" AutoPostBack="false" Style="font: 10px Verdana; text-align: right" Visible="true">
                                            <asp:ListItem Text="Diario" Value="d" Selected=True></asp:ListItem>
                                            <asp:ListItem Text="Mensual" Value="m"></asp:ListItem>
                                            <asp:ListItem Text="Año" Value="a"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>  
                                 
                                    <tr>
                                    <td class="filtrosCabecera">Concesión del Aprovechamiento:
                                       <asp:Label ID="lblVolMaxAnuLegV" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td class="filtrosCabecera">% Consumido:
                                       <asp:Label ID="lblPorConsumidoV" runat="server" Font-Bold="true"></asp:Label>
                                    </td>

                                </tr>                                                                      
                                <tr style="display:<%=VisibleSiAdmin() %>">                                    
                                    <td colspan="2">
                                    <asp:LinkButton ID="btnDescargarArchivoV" runat="server" Text="Descargar archivo para ser modificado" CssClass="enlaceLecturas"></asp:LinkButton>
                                    </td>
                                    <td colspan="2">Subir Lecturas                                    
                                    <input type="File" id="txtUploadV" runat="Server" NAME="txtUploadV" class="boton"> 
                                    <asp:Button id="btnSubirLecturasV" runat="server" Text="Aceptar"  CssClass="boton"></asp:Button>                                    
                                    </td>
                                    </tr> 
                            </table>
                                  
                        <table width="99%" id="Table5" bgcolor="#FEFCC1" >
                            <asp:Label ID="Label1" runat="server" Visible=false></asp:Label><tr>
                                <td bgcolor="#FEFCC1" style=" color:Red">
                                    <asp:LinkButton ID="lblAnyoHidrologicoV" runat="server" CssClass="enlaceLecturas"></asp:LinkButton>
                                </td>
                           
                                    <td nowrap bgcolor="#FEFCC1">
                                        <asp:Label ID="lblFiltroNRegV" Text="Registros a Mostrar:" CssClass="texto" runat="server" visible="false"></asp:Label>
                                        <asp:TextBox ID="txtFiltroNRegV" runat="server" CssClass="texto" Width=50px Visible="false"></asp:TextBox>
                                        <asp:Label ID="lblFiltroNRegPagV" Text="Registros por página:" runat="server" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtFiltroNRegPagV" runat="server" CssClass="texto" Width="50px" Text="20" Visible="False"></asp:TextBox>                                    
                                    
                                    <asp:CompareValidator ID="cvNregV" runat="server" Text="*" ErrorMessage="Sólo números" ControlToValidate="txtFiltroNRegV" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>                                    
                                    </td>                                        
                                                                              
                                                                                 
                                   <td nowrap bgcolor="#FEFCC1">
                                    <asp:TextBox ID="txtFiltrarCodFuenteDatoV" runat="server" CssClass="texto" Width=75px Visible=false></asp:TextBox>
                                    </td>
                             
                                    <td nowrap bgcolor="#FEFCC1">
                                      <asp:TextBox ID="txtFiltroFechaIniV" runat="server" CssClass="texto" Width=75px>[Fecha Desde]</asp:TextBox>
                                      <asp:CompareValidator ID=CompareValidator4 runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaIniV Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaIniV" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      
                                    </td>
                          
                                    <td nowrap bgcolor="#FEFCC1">
                                      <asp:TextBox ID="txtFiltroFechaFinV" runat="server" CssClass="texto" Width=75px>[Fecha Hasta]</asp:TextBox>
                                      <asp:CompareValidator ID=CompareValidator6 runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaFinV Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaFinV" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      

                                    </td>
                              
                                    <td nowrap bgcolor="#FEFCC1"> Mostrar Nulos
                                      <asp:CheckBox ID="chkFiltroNulasV" runat="server" CssClass="texto" Checked="false"/>
                                    </td>        
                              
                                  <td nowrap bgcolor="#FEFCC1" >
                                    <asp:LinkButton ID="lbtFiltroAceptarV" runat="server" Text="Filtrar" CssClass="enlaceLecturas"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtFiltroCancelarV" runat="server" Text="Limpiar" CssClass="enlaceLecturas"></asp:LinkButton>
                                    
                                    <asp:Button ID="btnfiltroAceptarV" runat="server" text="Filtrar" CssClass="boton" Visible=false />
                                    <asp:Button ID="btnFiltroCancelarV" runat="server" Text="Limpiar" CssClass="boton" Visible=false />
                                    <asp:Button ID="btnCancelarV" runat="server" Text="Cancelar" CssClass="boton" Visible=false />
                                    </td>
                                
                             </tr>
                     
                            </table>                  
                            <table id="TABLE2" width="99%">
                                     <tr>
                                        <td  align="right" colspan="12" >
                                        <a class="filtro" id = "MostrarFiltroM" href="javascript:void(0)" onclick="javascript:filtrarMotores()">Ocultar Filtro</a>
                                        </td>                                          
                                        </tr>
                                    <tr>
                                    <!--<td class="encabListado">Código PVYCR</td>-->
                                    <td class="encabListado"></td>
                                    <td class="encabListado" onclick="javascript:mostrarInformacion('C')">Código Fuente Dato</td>
                                    <td class="encabListado" style="width: 120px">Fecha Medida</td>
                                    <td class="encabListado" style="width: 10px">Lectura Contador (m3)</td>
                                    <td class="encabListado">Parcial (m3)</td>
                                    <td class="encabListado" style="width:40px;color:Maroon;font:bold">Caudal Medio (m3/s)(*)</td>
                                    <td class="encabListado">Funciona</td>
                                    <td class="encabListado">Caudal Medido (l/s)</td>                                    
                                    <td class="encabListado" style="display:none">Justificación</td>
                                    <td class="encabListado">Incidencia Volumétrica</td>
                                    <td class="encabListado" style="display:none">Consumo Volumétrico Adicional</td>
                                    <td class="encabListado" style="display:none">Reinicio Lectura Volumetrica</td>                                                                       
                                    <td class="encabListado">Observaciones</td>
                                    <td class="encabListado">&nbsp;</td>
                                    </tr>
                                   <tr id="FilaFiltroM" >     
                                         <td class="encabListado">  
                                              <asp:CheckBox ID="chkFMotor" runat="server" Visible="true" EnableViewState="true" onclick="javascript:SelDesSelTodas('V')" />
                                         </td>                                                      
                                         <td class="encabListado">
                                         <asp:DropDownList Style="font: 10px Verdana; text-align: right" Width="40px" ID="ddlFfuentedatosM" runat="server" AutoPostBack="false" >
                                         </asp:DropDownList>
                                         </td>                                                 
                                         <td class="encabListado" style="width:120px"><asp:TextBox runat="server" ID="txtFfechamedidaM" CssClass="texto" style="width:64px" ></asp:TextBox>
                                            <asp:Image    ID="imgFfechamedidaM" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" />
                                            <asp:CompareValidator ID="ComFfechamedidaM" runat="server" ControlToValidate="txtFfechamedidaM"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                         </td>  
                                         <td class="encabListado" style="width: 100px"><asp:TextBox runat="server" ID="txtFlectura" CssClass="texto" style="width: 100px" >
                                             </asp:TextBox><asp:RegularExpressionValidator ID="Regularlectura" runat="server" ControlToValidate="txtFlectura"
                                             ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}">
                                             </asp:RegularExpressionValidator>
                                         </td>  
                                         <td class="encabListado" ><asp:TextBox runat="server" ID="txtFdiferencialM" CssClass="texto" style="width:50px;color:Maroon;font:bold; display:none"></asp:TextBox></td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFdiferencialMS" CssClass="texto" style="width:50px;color:Maroon;font:bold; display:none"></asp:TextBox></td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFfunciona" CssClass="texto" style="width:40px"></asp:TextBox></td> 
                                        <td class="encabListado"><asp:TextBox runat="server" ID="txtFCaudalMedido" CssClass="texto" style="width:50px"></asp:TextBox><asp:RegularExpressionValidator ID="regularcaudal" runat="server" ControlToValidate="txtFCaudalMedido"
                                         ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.]\d+){0,1}"></asp:RegularExpressionValidator></td>                                           
                                         <td class="encabListado" style="display:none"><asp:TextBox runat="server" ID="txtFjustificacion" CssClass="texto" style="width:50px"></asp:TextBox></td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFincidencia" CssClass="texto" style="width:50px"></asp:TextBox><asp:CompareValidator ID=Compareincidencia runat="server"  Display=Dynamic Text=? ErrorMessage="Sólo números" ControlToValidate=txtFincidencia Operator=DataTypeCheck Type=integer></asp:CompareValidator></td> 
                                         <td class="encabListado"  style="width:240px; display:none"><asp:TextBox runat="server" ID="txtFconsumo" CssClass="texto" style="width:40px">
                                            </asp:TextBox><asp:RegularExpressionValidator ID="Regularconsumo" runat="server" ControlToValidate="txtFconsumo"
                                            ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator>
                                         </td>  
                                         <td class="encabListado" style="display:none"><asp:TextBox runat="server" ID="txtFreinicio" CssClass="texto" style="width:25px"></asp:TextBox><asp:RegularExpressionValidator ID="Regularreinicio" runat="server" ControlToValidate="txtFreinicio"
                                         ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}"></asp:RegularExpressionValidator></td>  
                                       
                                         
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFobservacionesM" CssClass="texto"  style="width:80px"></asp:TextBox></td>                                                
                                         <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsRegM" Runat="server" onclick="AceptarFiltroMotores" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                    </tr>                                     
                                    <asp:Repeater ID=rptVolumen runat=server>                                              
                                    <ItemTemplate>
                                        <tr id="rowMotor" <%# checkFila("V",Container.DataItem)%>>
                                            <td align=left>
                                              <asp:CheckBox ID="chkMotor" runat="server" EnableViewState="true" />
                                            </td>
                                            <td nowrap title="<%#VerCodFuenteDato(Container.DataItem)%>" onclick="javascript:mostrarInformacion('C')"><%#Container.DataItem("Cod_fuente_dato")%> </td>
                                            <td nowrap><%#Container.DataItem("fecha_Medida")%></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem, "LecturaContador_M3", "{0:#,##0}")%></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem, "Diferencial", "{0:#,##0}")%></td>
                                            <td align="right" style="color:Maroon;font:bold"><%#DataBinder.Eval(Container.DataItem, "Diferencial_Seg", "{0:#,##0}")%></td>
                                            <td><%#Container.DataItem("Funciona")%></td>
                                            <td align="right"><%#Container.DataItem("CaudalMedido")%></td>
                                            <td style="display:none"><%#Container.DataItem("Justificacion")%></td>
                                            <td><%#Container.DataItem("descIncVol")%></td>
                                            <td align="right" style="display:none"><%#DataBinder.Eval(Container.DataItem, "ConsumoVolumetricoAdicional", "{0:#,##0.##}")%></td>
                                            <td align="right" style="display:none"><%#DataBinder.Eval(Container.DataItem,"ReinicioLecturaVolumetrica", "{0:#,##0.##}")%></td>                                            
                                            <td title="<%#VerObservaciones(Container.DataItem)%>"%><%#VerObservacionesReducidas(Container.DataItem)%></td>
                                            <td><asp:LinkButton ID=lbtEditarV Visible="<%#VisibleSegunPerfil() %>" Runat=server CommandName='editar' CommandArgument='<%#"" & container.dataitem("Cod_fuente_dato")& "#" &  container.dataitem("Fecha_Medida")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtBorrarV" Visible="<%#VisibleSegunPerfil() %>" Runat="server" CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("Cod_fuente_dato")& "#" &  container.dataitem("Fecha_Medida")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar datos"></asp:LinkButton>
                                            <asp:Image ID="Image5" runat="server" Visible="<%#VisibleSiModificado(Container.DataItem) %>" ImageUrl="images/Circulo.gif"  ToolTip="Modificado" />
                                            </td>
                                        </tr>                                        
                                    </ItemTemplate>
                                </asp:Repeater>                               
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=14>               
                                  <uc:paginacion ID="ucPaginacionV" runat="server" Visible="true" />        
                                  </td>
                              </tr>                            
                               <!-- Fin Número de páginas -->                  
                                <tr><td colspan="14" class="encabListadoValEstimados">(*) Los valores que se muestran son valores estimados</td>
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar Volumen' -->
                        <!-- Panel Horometros -->    
                        <asp:Panel ID=pnlHorometros Runat=server Visible=false  style="overflow:auto;height: 692px; width:100%;">
                           <table width="99%">
                            <asp:Label ID="lblidelementoH" runat="server" Visible=false></asp:Label><tr>
                                <td class="tituloLecturas"><asp:Label ID="lblObtenerNomElementoH" runat=server></asp:Label></td>
                                <td colspan="1" bgcolor="#FEFCC1" align="right"><asp:CheckBox ID="chkReducirLecH" runat="server" Visible="true" Checked="false" Text="Reducir Lecturas" /></td>
                            </tr>
                            </table>
                            <table width="99%" bgcolor="#BFBDC0" >
                            <tr>
                                <td class="filtrosCabecera" colspan="4">Intervalo de datos: <asp:Label ID="lblIntervaloFechaH" Font-Bold="true" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="filtrosCabecera">Total Acumulado (h): <asp:Label ID="lblobtenerVolumenAcumuladoHoras" runat="server" Font-Bold="true"></asp:Label></td>
                                <td colspan="3" class="filtrosCabecera" style="color:Maroon">Total Acumulado (m3): <asp:Label ID="lblobtenervolumenacumuladoH" runat="server" Font-Bold="true"></asp:Label></td>
                            </tr>
                              <tr>
                                <td class="filtrosCabecera">Total lecturas cargadas: <asp:Label ID="lblobtenerNumLecturasH" Font-Bold="true" runat="server"></asp:Label></td>
                                <td class="filtrosCabecera">Total lecturas existentes: <asp:Label ID="lblobtenerTotNumLecturasH" Font-Bold="true" runat="server"></asp:Label> </td>                                
                                <td>
                                    <asp:LinkButton ID="btnModificarLecturasH" ToolTip="Fichero excel plano" runat="server"><img src="images/Icon_edit.gif" border="0" /></asp:LinkButton>                                    
                                    <asp:LinkButton ID="btnInformeH"  runat="server" Text="Informe de lecturas" CssClass="enlaceLecturas"  />
                                </td>

                                <td ><asp:Label ID="lblLectPantallaH" runat="server"></asp:Label></td>
                                
                            </tr>
                            <tr>
                                    <td class="filtrosCabecera">Concesión del Aprovechamiento:
                                       <asp:Label ID="lblVolMaxAnuLegH" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td class="filtrosCabecera">% Consumido:
                                       <asp:Label ID="lblPorConsumidoH" runat="server" Font-Bold="true"></asp:Label>
                                    </td>

                                </tr>                                                                    
                            <tr style="display:<%=VisibleSiAdmin() %>">                                
                                <td colspan="2">
                                    <asp:LinkButton ID="btnDescargarArchivoH" runat="server" Text="Descargar archivo para ser modificado" CssClass="enlaceLecturas"></asp:LinkButton>
                                </td>
                                <td colspan="2">Subir Lecturas                                    
                                <input type="File" id="txtUploadH" runat="Server" NAME="txtUploadH" class="boton"> 
                                <asp:Button id="btnSubirLecturasH" runat="server" Text="Aceptar"  CssClass="boton"></asp:Button>                                    
                                </td>
                            </tr>
                            </table>

                        <table width="99%" id="Table6" bgcolor="#FEFCC1" >
                            <asp:Label ID="Label9" runat="server" Visible=false></asp:Label><tr>
                                <td bgcolor="#FEFCC1" style=" color:Red">
                                    <asp:LinkButton ID="lblAnyoHidrologicoH" runat="server" CssClass ="enlaceLecturas"></asp:LinkButton>
                                </td>
                           
                                    <td nowrap bgcolor="#FEFCC1">
                                        <asp:Label ID="lblFiltroNRegH" Text="Registros a Mostrar:" CssClass="texto" runat="server" visible="false"></asp:Label>
                                        <asp:TextBox ID="txtFiltroNRegH" runat="server" CssClass="texto" Width=50px Visible="false"></asp:TextBox>
                                        <asp:Label ID="lblFiltroNRegPagH" Text="Registros por página:" runat="server" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtFiltroNRegPagH" runat="server" CssClass="texto" Width="50px" Text="20" Visible="False"></asp:TextBox>
                                    
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" Text="*" ErrorMessage="Sólo números" ControlToValidate="txtFiltroNRegH" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>                                    
                                    </td>                                        
                                                                              
                                                                                 
                                   <td nowrap bgcolor="#FEFCC1">
                                    <asp:TextBox ID="txtFiltrarCodFuenteDatoH" runat="server" CssClass="texto" Width=75px Visible=false></asp:TextBox>
                                    </td>
                             
                                    <td nowrap bgcolor="#FEFCC1">
                                      <asp:TextBox ID="txtFiltrofechaIniH" runat="server" CssClass="texto" Width=75px>[Fecha Desde]</asp:TextBox>
                                      <asp:CompareValidator ID=CompareValidator7 runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaIniH Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="ImgCalFechaIniH" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      
                                    </td>
                          
                                    <td nowrap bgcolor="#FEFCC1">
                                      <asp:TextBox ID="txtFiltroFechaFinH" runat="server" CssClass="texto" Width=75px>[Fecha Hasta]</asp:TextBox>
                                      <asp:CompareValidator ID=CompareValidator8 runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaFinH Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="ImgCalFechaFinH" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      

                                    </td>
                              
                                    <td nowrap bgcolor="#FEFCC1"> Mostrar Nulos
                                      <asp:CheckBox ID="chkFiltroNulasH" runat="server" CssClass="texto" Checked="false"/>
                                    </td>        
                              
                                  <td nowrap bgcolor="#FEFCC1" >
                                    <asp:LinkButton ID="lbtFiltroAceptarH" runat="server" Text="Filtrar" CssClass="enlaceLecturas"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtFiltroCancelarH" runat="server" Text="Limpiar" CssClass="enlaceLecturas"></asp:LinkButton>
                                    
                                    <asp:Button ID="btnFiltroAceptarH" runat="server" text="Filtrar" CssClass="boton" Visible=false />
                                    <asp:Button ID="btnFiltroCancelarH" runat="server" Text="Limpiar" CssClass="boton" Visible=false />
                                    <asp:Button ID="btnCancelarH" runat="server" Text="Cancelar" CssClass="boton" Visible=false />
                                    </td>
                                
                             </tr>
                     
                            </table>                  

                            <table width="99%">                                               
                                 <tr>
                                        <td  align="right" colspan="12"   >
                                        <a class="filtro" id = "MostrarFiltroH" href="javascript:void(0)"  onclick="javascript:filtrarHorometro()" >Ocultar Filtro</a>
                                        </td>
                                            
                                     </tr>
                                 
                                    <tr>
                                    <!--<td class="encabListado">Código PVYCR</td>-->
                                    <td class="encabListado"></td>
                                    <td class="encabListado" style="width:10px;" onclick="javascript:mostrarInformacion('C')">Código Fuente Dato</td>
                                    <td class="encabListado">Fecha Medida</td>
                                    <td class="encabListado">Lectura Horómetro (h)</td>   
                                    <td class="encabListado" style="width:50px;color:Maroon;">Parcial (m3)</td>                                     
                                    <td class="encabListado" style="width:50px;color:Maroon">Caudal Medio (m3/s) (*)</td>                          
                                    <td class="encabListado" style="width:40px">Funciona</td>
                                    <td class="encabListado" style="width:60px">Incidencia Volumétrica</td>
                                    <td class="encabListado" style="display:none">Consumo Volumétrico Adicional</td>
                                    <td class="encabListado" style="display:none">Reinicio Lectura Volumétrcia</td>
                                    <td class="encabListado">Observaciones</td>
                                    <td class="encabListado">&nbsp;</td>
                                    </tr>
                                   <tr id="FilaFiltroH" >     
                                         <td class="encabListado">  
                                              <asp:CheckBox ID="chkFHorometro" runat="server" Visible="true" EnableViewState="true" onclick="javascript:SelDesSelTodas('H')" />
                                         </td>                                                      
                                         <td class="encabListado">
                                         <asp:DropDownList Style="font: 10px Verdana; text-align: right" Width="50px" ID="ddlFFuenteDatosH" runat="server" AutoPostBack="false" >
                                         </asp:DropDownList>
                                         </td>                                                 
                                         <td class="encabListado" style="width:100px"><asp:TextBox runat="server" ID="txtFFechaMedidaH" CssClass="texto" style="width:64px" ></asp:TextBox>
                                            <asp:Image    ID="imgFFechaH" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" />
                                            <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtFFechaMedidaH"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                         </td>  
                                         <td class="encabListado" style="width: 80px" align="right"><asp:TextBox runat="server" ID="txtFHoras" CssClass="texto" style="width:70px">
                                             </asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtFHoras"
                                             ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}">
                                             </asp:RegularExpressionValidator>
                                         </td>  
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFDiferencialH" CssClass="texto" style="width:50px;display:none"></asp:TextBox></td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFDiferencialHS" CssClass="texto" style="width:50px;display:none"></asp:TextBox></td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFFuncionaH" CssClass="texto" style="width:40px"></asp:TextBox></td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFFIncidenciaH" CssClass="texto" ></asp:TextBox></td> 
                                         <td class="encabListado" style="display:none"><asp:TextBox runat="server" ID="txtFConsumoH" CssClass="texto" style="width:50px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtFConsumoH"
                                             ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}">
                                             </asp:RegularExpressionValidator></td> 
                                         <td class="encabListado" style="display:none"><asp:TextBox runat="server" ID="txtFReinicioH" CssClass="texto" style="width:50px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtFReinicioH"
                                             ErrorMessage="?" ValidationExpression="(\>|\>=|\<|\<=|\=|)[\-\+]{0,1}\d+([\.\,]\d+){0,1}">
                                             </asp:RegularExpressionValidator></td> 
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFObservacionesH" CssClass="texto"  style="width:80px"></asp:TextBox></td>                                                
                                         <td class="encabListado" align="right"><asp:LinkButton id="lbAceptarFiltroH"  Runat="server"  onclick="AceptarFiltroHorometro" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                    </tr>                                          
                                    <asp:Repeater ID=rptHorometros runat=server>                 
                                    <ItemTemplate>
                                        <tr id="rowHorometro" <%# checkFila("H",Container.DataItem)%>>
                                             <td align=left>
                                              <asp:CheckBox ID="chkHorometro" runat="server" EnableViewState="true" />
                                            </td>
                                            <td nowrap title="<%#VerCodFuenteDato(Container.DataItem)%>" onclick="javascript:mostrarInformacion('C')"><%#Container.DataItem("Cod_fuente_dato")%> </td>
                                            <td nowrap><%#Container.DataItem("fecha_medida")%></td>
                                            <td align="right"><%#Container.DataItem("HorasIntervalo")%></td> 
                                            <td align="right" style="color:Maroon;"><%#DataBinder.Eval(Container.DataItem, "Diferencial", "{0:#,##0}")%></td>                                           
                                            <td align="right" style="color:Maroon"><%#DataBinder.Eval(Container.DataItem, "Diferencial_Seg", "{0:#,##0.###}")%></td>
                                            <td><%#Container.DataItem("Funciona")%></td>
                                            <td><%#Container.DataItem("descIncVol")%></td>
                                            <td align="right" style="display:none"><%#DataBinder.Eval(Container.DataItem, "ConsumoVolumetricoAdicional", "{0:#,##0.##}")%></td>
                                            <td align="right" style="display:none"><%#DataBinder.Eval(Container.DataItem,"ReinicioLecturaVolumetrica", "{0:#,##0.##}")%></td>
                                            <td title="<%#VerObservaciones(Container.DataItem)%>" ><%#VerObservacionesReducidas(Container.DataItem)%></td>
                                            <td><asp:LinkButton ID=lbtEditarH Visible="<%#VisibleSegunPerfil() %>" Runat=server CommandName='editar' CommandArgument='<%#"" & container.dataitem("Cod_fuente_dato")& "#" &  container.dataitem("Fecha_Medida")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtBorrarH" Visible="<%#VisibleSegunPerfil() %>" Runat="server" CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("Cod_fuente_dato")& "#" &  container.dataitem("Fecha_Medida")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar datos"></asp:LinkButton>
                                            <asp:Image ID="Image5" runat="server" Visible="<%#VisibleSiModificado(Container.DataItem) %>" ImageUrl="images/Circulo.gif"  ToolTip="Modificado" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=12>               
                                  <uc:paginacion ID="ucPaginacionH" runat="server" Visible="true" />        
                                  </td>
                              </tr>
                              <tr><td colspan="12" class="encabListadoValEstimados">(*) Los valores que se muestran son valores estimados</td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar horometros' -->   
                        <!-- Panel Editar  Energia-->
                        <asp:Panel ID=pnlEDEnergia Runat=server Visible=false BorderWidth=1px BorderColor=black style="margin-top:10px; margin-bottom:10px" >
                        <asp:label id=lblFechamedidaESel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTitulo runat=server CssClass=tituloSec style="padding:20px"><B><%#checkNombreAlimentacion() %> </B></asp:Label><br /><br />
                            <table width=100% cellspacing=20 cellpadding=10 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td >Fecha Medida</td>
                                    <td><asp:label ID=txtFechaMedida runat=server CssClass="displayClave" ></asp:label>
                                    </td>
                                    <td>Código Fuente Dato</td>
                                    <td nowrap><asp:label ID=txtCodFuenteDato runat=server CssClass="displayClave"></asp:label>
                                </tr>
                                <tr>
                                    <td>Lectura I</td>
                                    <td align="right">
                                      <asp:textbox ID=txtLecturaI runat=server CssClass=textoNumerico />

                                    </td>
                                    <td>Lectura II</td>
                                    <td>
                                      <asp:textbox ID=txtLecturaII runat=server CssClass=textoNumerico></asp:textbox>
                                    </td>                                 
                                </tr>
                                <tr>
                                    <td>Lectura III</td>
                                    <td>
                                      <asp:TextBox ID=txtLecturaIII runat="server" CssClass=textoNumerico></asp:TextBox>
                                    </td>
                                    <td>Total (Kwh)</td>
                                    <td>
                                      <asp:textbox ID=txtTotal_Kwh runat=server CssClass=textoNumerico ></asp:textbox>
                                    </td>                                     
                                </tr>
                                
                                <tr>
                                    <td>Total (Kvar)</td>
                                    <td>
                                      <asp:textbox ID=txtTotal_Kvar runat=server CssClass=textoNumerico ></asp:textbox>
                                    </td>
                                    
                                    <td>Funciona</td>
                                    <td style="font:verdana 11px">   
                                      <asp:DropDownList ID="ddlfunciona" runat="server">
                                        <asp:ListItem Value="2" Text=" "></asp:ListItem>
                                        <asp:ListItem Value="0" Text="SI"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="NO"></asp:ListItem>
                                      </asp:DropDownList></td>                                
                                </tr>
                                <tr>
                                    <td>Justificacion</td>
                                    <td><asp:textbox ID="txtJustificacion" runat="server" CssClass="texto"></asp:textbox></td>  
                                    <td>Incidencia Eléctrica</td>                              
                                    <td><asp:DropDownList ID="ddlIncidenciasE" runat="server"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Consumo Eléctrico Adicional</td>
                                    <td>
                                      <asp:textbox ID="txtConsumoElectricoAdicional" runat="server" CssClass="textoNumerico" ></asp:textbox>
                                    </td>  
                                    <td>Reinicio Lectura Eléctrica</td>                              
                                    <td>
                                      <asp:TextBox ID=txtReinicioLecturaElectrica runat=server CssClass="textoNumerico" ></asp:TextBox>
                                    </td>                                
                                </tr>
                                <tr>
                                    <td>Observaciones</td>
                                    <td colspan=3><asp:textbox ID="txtObservaciones" runat="server" CssClass="texto" Width=100% TextMode=multiLine></asp:textbox></td>
                                </tr>
                                 <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    &nbsp;
                                 </td>
                                </tr>          
                                <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    <asp:Button ID="btnAceptarEDEnergia" runat="server" Text="Aceptar" CssClass="boton"/>
                                    <asp:Button ID="btnCancelarEDEnergia" runat="server" Text="Cancelar" CssClass="boton"/>
                                 </td>
                                </tr>                                                            
                                </table>            
                            </td>
                           
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Energia -->
          <!-- Panel Diferenciales -->    
                        <asp:Panel ID="pnlDiferencial" Runat=server Visible=false style="overflow:auto;height: 692px; width:100%;" >          
                    
                    
                            <table width="99%">
                            <asp:Label ID="lblidelementoD" runat="server" Visible=false></asp:Label><tr>
                                <td class="tituloLecturas"><asp:Label ID="lblobtenerNomElementoD" runat=server></asp:Label></td>
                                <td colspan="1" bgcolor="#FEFCC1" align="right"><asp:CheckBox ID="chkReducirLecD" runat="server" Visible="false" Checked="false" Text="Reducir Lecturas" /></td>
                            </tr>
                            </table>
                            <table width="99%" bgcolor="#BFBDC0" >
                                <tr>
                                    <td class="filtrosCabecera" colspan="2">Intervalo de datos: <asp:Label ID="lblIntervaloFechaD"  Font-Bold="true" runat="server"></asp:Label></td>
                                    <td></td>   
                                    <td></td>                                 
                                </tr>
                                <tr>
                                    <td class="filtrosCabecera">Total Acumulado (m3): <asp:Label ID="lblobtenervolumenacumuladoD" Font-Bold="true" runat="server"></asp:Label></td>
                                    <td class="filtrosCabecera">Total lecturas cargadas: <asp:Label ID="lblobtenerNumlecturasD" Font-Bold="true" runat="server"></asp:Label></td>
                                  <td nowrap>
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/XLS.gif" ID="Image2" Style="cursor: pointer" />
                                        <asp:LinkButton ID="btnInformeD"  runat="server" Text="Informe de lecturas (Imprimible)" CssClass="enlaceLecturas"  />                                      
                                    </td>   
                                    <td>
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/XLS.gif" ID="Image3" Style="cursor: pointer" Visible="true" />                              
                                        <asp:LinkButton ID="btnComparativaCaudalesDXLS"  runat="server" Text="Informe de Consumos" CssClass="enlaceLecturas" />                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td class="filtrosCabecera">Total lecturas existentes: <asp:Label ID="lblObtenerTotNumlecturasD"  Font-Bold="true" runat="server"></asp:Label> </td>
                                    <td><asp:Label runat="server" ID="lblLectPantallaD"></asp:Label></td>
                                    <td>
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/iconos/Grafica2.gif" ID="Image4" Style="cursor: pointer" />
                                        <asp:LinkButton ID="btnGraficoD"  runat="server" Text="Gráfico de consumos" CssClass="enlaceLecturas"  />                                      
                                    </td>
                                    <td nowrap>
                                        <asp:Image runat="server" ImageUrl="~/SICAH/images/PDF.gif" ID="imgComparativaCaudalesD" Style="cursor: pointer" Visible="true" />                              
                                        <asp:LinkButton ID="btnComparativaCaudalesD"  runat="server" Text="Informe de Consumos" CssClass="enlaceLecturas" />                                    
                                            <asp:DropDownList ID="ddlIntervaloD" runat="server" AutoPostBack="false" Style="font: 10px Verdana; text-align: right" Visible="true">
                                            <asp:ListItem Text="Mensual" Value="m"></asp:ListItem>
                                            <asp:ListItem Text="Año" Value="a"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr> 
                                 <tr>
                                    <td class="filtrosCabecera">Concesión del Aprovechamiento:
                                       <asp:Label ID="lblVolMaxAnuLegD" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td class="filtrosCabecera">% Consumido:
                                       <asp:Label ID="lblPorConsumidoD" runat="server" Font-Bold="true"></asp:Label>
                                    </td>

                                </tr>                                          
                            </table>
                                  
                        <table width="99%" id="Table8" bgcolor="#FEFCC1" >
                            <asp:Label ID="Label11" runat="server" Visible=false></asp:Label><tr>
                                <td bgcolor="#FEFCC1" style=" color:Red">
                                    <asp:LinkButton ID="lblAnyoHidrologicoD" runat="server" CssClass="enlaceLecturas"></asp:LinkButton>
                                </td>
                           
                                    <td nowrap bgcolor="#FEFCC1">
                                        <asp:Label ID="lblFiltroNRegD" Text="Registros a Mostrar:" CssClass="texto" runat="server" visible="false"></asp:Label>
                                        <asp:TextBox ID="txtFiltroNRegD" runat="server" CssClass="texto" Width=50px Visible="false"></asp:TextBox>
                                        <asp:Label ID="lblFiltroNRegPagD" Text="Registros por página:" runat="server" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtFiltroNRegPagD" runat="server" CssClass="texto" Width="50px" Text="20" Visible="False"></asp:TextBox>                                    
                                    
                                    <asp:CompareValidator ID="cvNregD" runat="server" Text="*" ErrorMessage="Sólo números" ControlToValidate="txtFiltroNRegD" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>                                    
                                    </td>                                        
                                                                              
                                                                                 
                                   <td nowrap bgcolor="#FEFCC1">
                                    <asp:TextBox ID="txtFiltrarCodFuenteDatoD" runat="server" CssClass="texto" Width=75px Visible=false></asp:TextBox>
                                    </td>
                             
                                    <td nowrap bgcolor="#FEFCC1">
                                      <asp:TextBox ID="txtFiltroFechaIniD" runat="server" CssClass="texto" Width=75px>[Fecha Desde]</asp:TextBox>
                                      <asp:CompareValidator ID=CompareValidator11 runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaIniD Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaIniD" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      
                                    </td>
                          
                                    <td nowrap bgcolor="#FEFCC1">
                                      <asp:TextBox ID="txtFiltroFechaFinD" runat="server" CssClass="texto" Width=75px>[Fecha Hasta]</asp:TextBox>
                                      <asp:CompareValidator ID=CompareValidator12 runat=server Text=* ErrorMessage="Fecha no válida" ControlToValidate=txtFiltroFechaFinD Operator=DataTypeCheck Type=date></asp:CompareValidator>
                                      <asp:Image ID="imgCalFechaFinD" runat="Server" align="absmiddle" ImageUrl="../images/calendario.gif"
                                      Style="cursor: pointer" />                                      

                                    </td>
                              
                                    <td nowrap bgcolor="#FEFCC1"> Mostrar Nulos
                                      <asp:CheckBox ID="chkFiltroNulasD" runat="server" CssClass="texto" Checked="false"/>
                                    </td>        
                              
                                  <td nowrap bgcolor="#FEFCC1" >
                                    <asp:LinkButton ID="lbtFiltroAceptarD" runat="server" Text="Filtrar" CssClass="enlaceLecturas"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtFiltroCancelarD" runat="server" Text="Limpiar" CssClass="enlaceLecturas"></asp:LinkButton>
                                    
                                    <asp:Button ID="btnfiltroAceptarD" runat="server" text="Filtrar" CssClass="boton" Visible=false />
                                    <asp:Button ID="btnFiltroCancelarD" runat="server" Text="Limpiar" CssClass="boton" Visible=false />
                                    <asp:Button ID="btnCancelarD" runat="server" Text="Cancelar" CssClass="boton" Visible=false />
                                    </td>
                                
                             </tr>
                     
                            </table>                  
                            <table id="TABLE7" width="99%">
                                     <tr>
                                        <td  align="right" colspan="7" >
                                        <a class="filtro" id = "MostrarFiltroD" href="javascript:void(0)" onclick="javascript:filtrarDiferencial()">Ocultar Filtro</a>
                                        </td>                                          
                                        </tr>
                                    <tr>
                                    <!--<td class="encabListado">Código PVYCR</td>-->
                                    <td class="encabListado"></td>
                                    <td class="encabListado" style="width: 120px">Código Fuente Dato</td>
                                    <td class="encabListado" style="width: 171px">Fecha Medida</td>
                                    <td class="encabListado" >Suministro Mensual (m3)</td>
                                    <td class="encabListado">Acumulado (m3)</td>
                                    <td class="encabListado">Observaciones</td>
                                    <td class="encabListado">&nbsp;</td>
                                    </tr>
                                   <tr id="FilaFiltroD" >     
                                         <td class="encabListado">  
                                              <asp:CheckBox ID="chkFDiferencial" runat="server" Visible="true" EnableViewState="true" onclick="javascript:SelDesSelTodas('D')" />
                                         </td>                                                      
                                         <td class="encabListado">
                                         <asp:DropDownList Style="font: 10px Verdana; text-align: right" Width="90px" ID="ddlFfuentedatosD" runat="server" AutoPostBack="false" >
                                         </asp:DropDownList>
                                         </td>                                                 
                                         <td class="encabListado" style="width:120px"><asp:TextBox runat="server" ID="txtFfechamedidaD" CssClass="texto" style="width:66px" ></asp:TextBox>
                                            <asp:Image ID="imgFfechamedidaD" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif"
                                            Style="cursor: pointer" />
                                            <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="txtFfechamedidaD"
                                            Display="Dynamic" ErrorMessage="Sólo fechas" Operator="DataTypeCheck" Text="?"
                                            Type="Date" Width="1px"></asp:CompareValidator>
                                         </td>  
                                        <td class="encabListado"><asp:TextBox runat="server" ID="txtFSuministros" CssClass="texto" ></asp:TextBox><asp:CompareValidator ID="CompareValidator10" runat="server" Text="*" ErrorMessage="Sólo números" ControlToValidate="txtFSuministros" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>                                           
                                        <td class="encabListado">&nbsp;</td>                                           
                                         
                                         <td class="encabListado"><asp:TextBox runat="server" ID="txtFobservacionesD" CssClass="texto" ></asp:TextBox></td>                                                
                                         <td class="encabListado" align="right"><asp:LinkButton id="lbtAceptarUsRegD" Runat="server" onclick="AceptarFiltroDiferencial" CssClass="enlaceLecturas">Aceptar</asp:LinkButton></td> 
                                    </tr>                                     
                                    <asp:Repeater ID=rptDiferencial runat=server>                                              
                                    <ItemTemplate>
                                        <tr id="rowDiferencial" <%# checkFila("D",Container.DataItem)%>>
                                            <td align=left>
                                              <asp:CheckBox ID="chkDiferencial" runat="server" EnableViewState="true" />
                                            </td>
                                            <td nowrap title="<%#VerCodFuenteDato(Container.DataItem)%>" onclick="javascript:mostrarInformacion('C')"><%#Container.DataItem("Cod_fuente_dato")%> </td>
                                            <td nowrap><%#Container.DataItem("fecha_Medida")%></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem, "SuministroMensualM3", "{0:#,##0}")%></td>
                                            <td align="right"><%#DataBinder.Eval(Container.DataItem, "Diferencial", "{0:#,##0}")%></td>
                                            <td title="<%#VerObservaciones(Container.DataItem)%>"%><%#VerObservacionesReducidas(Container.DataItem)%></td>
                                            <td><asp:LinkButton ID=lbtEditarD Visible="<%#VisibleSegunPerfil() %>" Runat=server CommandName='editar' CommandArgument='<%#"" & container.dataitem("Cod_fuente_dato")& "#" &  container.dataitem("Fecha_Medida")%>'><img src="../images/edit.gif" border=0 align=absmiddle alt="Editar datos"></asp:LinkButton>
                                            <asp:LinkButton ID="lbtBorrarD" Visible="<%#VisibleSegunPerfil() %>" Runat="server" CommandName='borrar' OnClientClick='return confirmar_borrado();' CommandArgument='<%#"" & container.dataitem("Cod_fuente_dato")& "#" &  container.dataitem("Fecha_Medida")%>'><img src="../images/del.gif" border=0 align=absmiddle alt="Borrar datos"></asp:LinkButton></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                 <!-- Número de páginas -->
                               <tr>
                                  
                                  <td style="background-color:#D4D0C8; border:0px solid #D4D0C8" colspan=7>               
                                  <uc:paginacion ID="ucPaginacionD" runat="server" Visible="true" />        
                                  </td>
                              </tr>
                               <!-- Fin Número de páginas -->                  
                            </table>
                        </asp:Panel> 
                        <!-- Fin Panel listar diferenciales' -->                            
                        <!-- Panel Editar  Acequias-->
                        <asp:Panel ID=pnlEDAcequias Runat=server Visible=false BorderWidth=1px BorderColor=black style="margin-top:10px; margin-bottom:10px" >
                        <asp:label id=lblFechamedidaQSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTituloQ runat=server CssClass=tituloSec style="padding:20px"><B><%#checkNombreAcequias()%> </B></asp:Label><br /><br />
                            <table width=100% cellspacing=20 cellpadding=10 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td >Fecha Medida</td>
                                    <td><asp:label ID=lblFechamedidaQ runat=server CssClass="displayClave" ></asp:label>
                                    </td>
                                    <td>Código Fuente Dato</td>
                                    <td nowrap><asp:label ID=lblCodFuenteDatoQ runat=server CssClass="displayClave"></asp:label>
                                    <td>Tipo Obtención Caudal</td>
                                    <td><asp:Label ID=lblTipoObtencionCaudal runat="server" CssClass="displayClave"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Escala (m)</td>
                                    <td>
                                      <asp:textbox ID=txtEscalaQ runat=server CssClass=textoNumerico />
                                    </td>
                                    <td>Calado (m)</td>
                                    <td>
                                      <asp:textbox ID=txtCaladoQ runat=server CssClass=textoNumerico ></asp:textbox>
                                    </td>                                 
                                </tr>
                                <tr>
                                    
                                    <td>Régimen Curva</td>
                                    <td >   
                                      <asp:DropDownList ID="ddlRegimenCurvaQ" runat="server">
                                      </asp:DropDownList></td>                                
                                    <td>Nº Parada</td>
                                    <td>
                                      <asp:textbox ID=txtNumParadaQ runat=server CssClass=textoNumerico></asp:textbox>                                      
                                    </td>
                                      
                                </tr>
                                
                                <tr>
                                    <td>Caudal (m3/s)</td>
                                    <td><asp:textbox ID="txtCaudalQ" runat="server" CssClass="textoNumerico" ></asp:textbox>                                    
                                    </td>  
                                    <td>Duda Calidad</td>                              
                                    <td>
                                      <asp:textbox ID="txtDudaCalidad" runat="server" CssClass="textoNumerico"></asp:textbox>
                                    </td>  
                                </tr>
                                <tr>
                                    <td>Observaciones</td>
                                    <td colspan=5><asp:textbox ID="txtobservacionesQ" runat="server" CssClass="texto" Width=100% TextMode=MultiLine></asp:textbox></td>
                                </tr>
                                 <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    &nbsp;
                                 </td>
                                </tr>          
                                <tr>
                                <td>&nbsp;</td>
                                 <td nowrap>
                                    <asp:Button ID="btnAceptarEDAcequias" runat="server" Text="Aceptar" CssClass="boton"/>
                                    <asp:Button ID="btnCancelarEDAcequias" runat="server" Text="Cancelar" CssClass="boton"/>
                                 </td>
                                </tr>                                                            
                                </table>            
                            </td>
                           
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar acequias -->                     
                        <!-- Panel Editar  Volumetricos-->
                        <asp:Panel ID=pnlEDVolumen Runat=server Visible=false BorderWidth=1px BorderColor=black style="margin-top:10px; margin-bottom:10px" >
                        <asp:label id=lblFechamedidaVSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lblTituloV runat=server CssClass=tituloSec style="padding:20px"><B><%#checkNombreMotores() %> </B></asp:Label><br /><br />
                            <table width=100% cellspacing=20 cellpadding=10 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td >Fecha Medida</td>
                                    <td><asp:label ID=lblFechaMedidaV runat=server CssClass="displayClave" ></asp:label>
                                    </td>
                                    <td>Código Fuente Dato</td>
                                    <td nowrap><asp:label ID=lblCodfuentedatoV runat=server CssClass="displayClave"></asp:label>
                                </tr>
                                <tr>
                                    <td>Lectura Contador (m3)</td>
                                    <td>
                                      <asp:textbox ID=txtlecturacontador runat=server CssClass=textoNumerico />
                                    </td>
       
                                    <td>Funciona</td>
                                    <td style="font:verdana 11px">   
                                      <asp:DropDownList ID="ddlFuncionaV" runat="server">
                                      <asp:ListItem Value="2" Text=" "></asp:ListItem>
                                        <asp:ListItem Value="0" Text="SI"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="NO"></asp:ListItem>
                                      </asp:DropDownList>
                                    </td>                                          
                                </tr>
                                <tr>
                                <td>Caudal Medido (l/s)</td>
                                    <td><asp:textbox ID="txtCaudalMedidoV" runat="server" CssClass="textoNumerico" ></asp:textbox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtCaudalMedidov"
                                             ErrorMessage="?" ValidationExpression="[\-\+]{0,1}\d+([\.]\d+){0,1}">
                                             </asp:RegularExpressionValidator>
                                    </td>                                
                                    <td>Justificación</td>
                                    <td><asp:textbox ID="txtJustificacionV" runat="server" CssClass="texto"></asp:textbox></td>  
                                   </tr>
                                <tr>
                                    <td>Incidencia Volumétrica</td>                              
                                    <td><asp:DropDownList ID="ddlIncidenciaV" runat="server"></asp:DropDownList></td>                                
                                    <td>Consumo Volumétrico Adicional</td>
                                    <td>
                                      <asp:textbox ID="txtConsumoV" runat="server" CssClass="textoNumerico" ></asp:textbox>
                                    </td>  
                                                                
                                </tr>
                                <tr>
                                    <td>Reinicio Lectura Volumétrica</td>                              
                                    <td>
                                      <asp:TextBox ID=txtReinicioV runat=server CssClass="textoNumerico" ></asp:TextBox>
                                    </td>                                      
                                    <td>Observaciones</td>
                                    <td><asp:textbox ID="txtObservacionesV" runat="server" CssClass="texto" Width=100% TextMode=multiline></asp:textbox></td>
                                </tr>
                                 <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    &nbsp;
                                 </td>
                                </tr>          
                                <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    <asp:Button ID="btnAceptarEDVolumen" runat="server" Text="Aceptar" CssClass="boton"/>
                                    <asp:Button ID="btnCancelarEDVolumen" runat="server" Text="Cancelar" CssClass="boton"/>
                                 </td>
                                </tr>                                                            
                                </table>            
                            </td>
                           
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Volumétricos -->    
                        <!-- Panel Editar  horometros-->
                        <asp:Panel ID=pnlEDHorometros Runat=server Visible=false BorderWidth=1px BorderColor=black style="margin-top:10px; margin-bottom:10px" >
                        <asp:label id=lblFechamedidaHSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lbltituloH runat=server CssClass=tituloSec style="padding:20px"><B><%#checkNombreHorometros() %> </B></asp:Label><br /><br />
                            <table width=100% cellspacing=20 cellpadding=10 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td >Fecha Medida</td>
                                    <td><asp:label ID=lblFechaMedidaH runat=server CssClass="displayClave" ></asp:label>
                                    </td>
                                    <td>Código Fuente Dato</td>
                                    <td nowrap><asp:label ID=lblCodfuenteDatoH runat=server CssClass="displayClave"></asp:label>
                                </tr>
                                <tr>
                                    <td>Horas Intervalo</td>
                                    <td>
                                      <asp:textbox ID=txtHorasIntervalo runat=server CssClass=textoNumerico />
                                    </td>
       
                                    <td>Funciona</td>
                                    <td style="font:verdana 11px">   
                                      <asp:DropDownList ID="ddlFuncionaH" runat="server">
                                      <asp:ListItem Value="2" Text=" "></asp:ListItem>
                                        <asp:ListItem Value="0" Text="SI"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="NO"></asp:ListItem>
                                      </asp:DropDownList>
                                    </td>                                          
                                </tr>
                                <tr>
                                    <td>Incidencia Horómetro</td>                              
                                     <td><asp:DropDownList ID="ddlIncidenciaH" runat="server"></asp:DropDownList></td>
                                     <td>Consumo Horómetro Adicional</td>
                                    <td>
                                      <asp:textbox ID="txtConsumoH" runat="server" CssClass="textoNumerico" ></asp:textbox>
                                    </td>  
                                </tr>
                                <tr>
                                   <td>Reinicio Lectura Horómetro</td>                              
                                    <td>
                                      <asp:TextBox ID=txtreinicioH runat=server CssClass="textoNumerico" ></asp:TextBox>
                                   </td> 
                                   <td>&nbsp;</td>                               
                                </tr>
                                <tr>
                                  <td>Observaciones</td>
                                    <td colspan=3><asp:textbox ID="txtobservacionesH" runat="server" CssClass="texto" Width=100% TextMode=MultiLine></asp:textbox></td>                                                             
                                </tr>
                                 <tr>
                                    <td>&nbsp;</td>
                                 <td>
                                    &nbsp;
                                 </td>
                                </tr>          
                                <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    <asp:Button ID="btnAceptarEDHorometro" runat="server" Text="Aceptar" CssClass="boton"/>
                                    <asp:Button ID="btnCancelarEDHorometro" runat="server" Text="Cancelar" CssClass="boton"/>
                                 </td>
                                </tr>                                                            
                                </table>            
                            </td>
                           
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Horometros -->  
                        <!-- Panel Editar diferenciales-->
                        <asp:Panel ID=pnlEDDiferencial Runat=server Visible=false BorderWidth=1px BorderColor=black style="margin-top:10px; margin-bottom:10px" >
                        <asp:label id=lblFechamedidaDSel runat=Server Visible=False></asp:label>
                        <br /><asp:Label ID=lbltituloD runat=server CssClass=tituloSec style="padding:20px"><B><%#checkNombreDiferencial()%> </B></asp:Label><br /><br />
                            <table width=100% cellspacing=20 cellpadding=10 class="tablaEdicion">
                            <tr>
                            <td>
                                <table cellspacing=2 cellpadding=2>
                                <tr>
                                    <td >Fecha Medida</td>
                                    <td><asp:label ID=lblFechaMedidaD runat=server CssClass="displayClave" ></asp:label>
                                    </td>
                                    <td>Código Fuente Dato</td>
                                    <td nowrap><asp:label ID=lblCodfuenteDatoD runat=server CssClass="displayClave"></asp:label>
                                </tr>
                                <tr>
                                    <td>Suministros Mensual (m3)</td>
                                    <td>
                                      <asp:textbox ID=txtEDSuministroD runat=server CssClass=textoNumerico />
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtEDSuministroD"
                                             ErrorMessage="*" ValidationExpression="[\-\+]{0,1}\d+([\.]\d+){0,1}">
                                             </asp:RegularExpressionValidator>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>                                                            
                                <tr>
                                  <td>Observaciones</td>
                                  <td colspan="3"><asp:textbox ID="txtEDObservacionesD" runat="server" CssClass="texto" Width=100% TextMode=MultiLine></asp:textbox></td>                                                                   

                                </tr>
                                <tr>
                                <td>&nbsp;</td>
                                 <td>
                                    <asp:Button ID="btnAceptarEDDiferencial" runat="server" Text="Aceptar" CssClass="boton"/>
                                    <asp:Button ID="btnCancelarEDDiferencial" runat="server" Text="Cancelar" CssClass="boton"/>
                                 </td>
                                </tr>                                                            
                                </table>            
                            </td>
                           
                            </tr>
                            </table>
   
                        </asp:Panel>                 
                        <!-- Fin Panel Editar Diferenciales -->                            
                                                                                                    
                    </td>
                    <!-- Fin Celda2 contenido-->
                  </tr>
                  <!-- Fin Celda Menú contenido-->
                </table>
              </td>
            </tr>
          </table>
 
  </form>
</body>
</html>
