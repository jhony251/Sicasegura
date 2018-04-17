<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    
    <meta http-equiv="Page-Exit" content="revealTrans(Duration=1,Transition=7)" />
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />  
    <meta http-equiv="Language" content="Spanish" />
	<meta name="viewport" content="width=320,user-scalable=no" />

    <title>SICA-Telegestión</title>

    <script language="javascript" type="text/javascript" src="grid/dhtmlxgrid.js"></script>
    <script language="javascript" type="text/javascript" src="grid/dhtmlxgridcell.js"></script>
    <script language="javascript" type="text/javascript" src="grid/ext/dhtmlxgrid_pgn.js"></script>    
    <script language="javascript" type="text/javascript" src="grid/ext/dhtmlxgrid_splt.js"></script> 
    <script language="javascript" type="text/javascript" src="grid/dhtmlxcommon.js"></script>

    <link rel="STYLESHEET" type="text/css" href="grid/dhtmlxgrid.css" />
    <link rel="STYLESHEET" type="text/css" href="grid/dhtmlxgrid_skins.css" />

</head>
<body style="margin:0px;background-image:url('images/background.jpg');Background-repeat:repeat-y; width:320px;">
    <div id="Div1" style="">
	    <div id="Div2" style="top:0px;position:absolute;width:320px;">
		    <img src="images/header.jpg" alt="" />
	    </div>
	    <div id="Div3" style="top:15px;position:absolute;width:320px">
		    <center><font face="TAHOMA" size="3" color="white"><b>SICA-Telegesti&oacute;n</b></font></center>
	    </div>
	    <div id="Div4" style="top:65px;position:absolute;width:320px">
		    <center><font face="verdana" size="3" color="Black"><b>Datos instant&aacute;neos</b></font></center>
	    </div>
		<div id="gridbox" style="top:95px;position:absolute;width:318px;height:295px"></div> 

	</div>

	<script language="javascript" type="text/javascript">
		var mygrid;
	    mygrid = new dhtmlXGridObject('gridbox');
		mygrid.setCellTextStyle("1", "font-family: arial; font-size:8pt;");
	    mygrid.setHeader("Cauce,Fecha,Calado (m),Caudal (m3/s),Velocidad (m/s)"); 
	    mygrid.setInitWidths("82,*,40,40,50");
	    mygrid.setColAlign("left,center,center,center,center");
	    mygrid.setColTypes("txt,ro,ro,ro,ro");
	    mygrid.enableMultiselect(false);
	    mygrid.setColumnColor("#D3E2E5,#d5f1ff");
	    mygrid.setColSorting("str,date,str,str,str")
		
	    //mygrid.setSkin("modern"); 
	    mygrid.init(); 
    
        <% 
	    Set Conexion=Server.CreateObject("ADODB.Connection")
	    Conexion.Open "Driver={SQL Server};Server=172.17.8.207\sap5sql2005;Database=GFLUVIAL;UID=gfluvial;PWD=sa9ia;"
	    
	    SQL = "SELECT   PVYCR_AccesosTopkapi.Denominacion as Cauce, PVYCR_DatosAcequias_NIVUS.Fecha AS Fecha, PVYCR_DatosAcequias_NIVUS.Calado_M AS [Calado (M)], PVYCR_DatosAcequias_NIVUS.Caudal_M3S AS [Caudal (M3/s)], PVYCR_DatosAcequias_NIVUS.Velocidad_MS AS [Velocidad (M/s)] FROM PVYCR_DatosAcequias_NIVUS INNER JOIN PVYCR_AccesosTopkapi ON PVYCR_DatosAcequias_NIVUS.CodigoPVYCR COLLATE Modern_Spanish_CI_AS = PVYCR_AccesosTopkapi.CodigoPVYCR WHERE PVYCR_DatosAcequias_NIVUS.Fecha = (SELECT MAX(d1.Fecha) FROM PVYCR_DatosAcequias_NIVUS d1 WHERE PVYCR_DatosAcequias_NIVUS.Codigopvycr =d1.CodigoPVYCR) ORDER BY fecha DESC"
	        	
	    Set rsMUR = conexion.Execute(SQL)
	    i=0
		Dim Calado, caudal, velocidad
	    while not rsMur.EOF
			Calado = Replace(rsMUR(2),",",".")
			Caudal = Replace(rsMUR(3),",",".")
			Velocidad = Replace(rsMUR(4),",",".")
		%>
		    mygrid.addRow(<%response.Write(i)%>,"<%response.Write(rsMUR(0))%>,<%response.Write(rsMUR(1))%>,<%response.Write(calado)%>,<%response.Write(caudal)%>,<%response.Write(velocidad)%>",<%response.Write(i)%>);
		    <%rsMUR.movenext
		    i=i+1%>
	    <%wend
	    conexion.Close
        set conexion = Nothing
	    %>
    
    
    </script>	
</body>
</html>
