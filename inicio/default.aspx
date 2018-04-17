<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/style.css" />
		<link rel="stylesheet" type="text/css" href="css/sbimenu.css" />
		<link href='https://fonts.googleapis.com/css?family=PT+Sans+Narrow' rel='stylesheet' type='text/css' />
		<link href='https://fonts.googleapis.com/css?family=News+Cycle&v1' rel='stylesheet' type='text/css' />
		<script src="https://www.google.com/jsapi/?key=ABQIAAAA7Ai-79ZB_CxLV8FBrxL_BRS96SqVw1_DMJMpdQZB7dA4HmtyAhQhzSBRu72AY9RLRq_601ABZ1fDlw" type="text/javascript"></script>


</head> 
<body>
    <form id="form1" runat="server">
        
        <div class="container">
        <div id="cabecera" class="cabecera">
            <img src="images/Cabecera_2011.jpg" width="920px"/>
        </div>
			<div class="header">
			    <h1>&nbsp;</h1>
				<%--<h1>Sistema Integrado de Control de Aprovechamientos <span> SICASegura</span></h1>
				<h2><img src="images/tragsa.gif" width="100px"></h2>--%>
			</div>
			<div class="content">
				<div id="sbi_container" class="sbi_container">
					<div class="sbi_panel" data-bg="images/1.jpg">
						<a href="#" class="sbi_label">Contacto</a>
						<div class="sbi_content">
							<ul>
								<li><a href="#"><font size="1">sica@chsegura.es</font></a></li>
							</ul>
						</div>						
					</div>
					<div class="sbi_panel" data-bg="images/2.jpg">
						<a href="#" class="sbi_label">SicaSegura</a>
						<div class="sbi_content">
							<ul>
								<li><asp:Literal ID="LinkSicaSegura" runat="server"></asp:Literal></li>
							</ul>
						</div>						
					</div>
					<div class="sbi_panel" data-bg="images/3.jpg">
						<a href="#" class="sbi_label">Esquemas</a>
						<div class="sbi_content">
							<ul>
								<li><asp:Literal ID="LinkEsquemasSegura" runat="server"></asp:Literal></li>
							</ul>
						</div>						
					</div>
					<div class="sbi_panel" data-bg="images/4.jpg">
						<a href="#" class="sbi_label">Documentos</a>
						<div class="sbi_content">
							<ul>
								<li><asp:Literal ID="LinkInformesSegura" runat="server"></asp:Literal></li>
							</ul>
						</div>						
					</div>
					<div class="sbi_panel" data-bg="images/5.jpg">
						<a href="#" class="sbi_label">ScadaSegura</a>
						<div class="sbi_content">
							<ul>
							    <li><asp:Literal ID="LIT_LinkSCADASegura" runat="server"></asp:Literal></li>
							</ul>
						</div>						
					</div>
					<div class="sbi_panel" data-bg="images/6.jpg">
						<a href="#" class="sbi_label">D.Manuales</a>
						<div class="sbi_content">
							<ul>
								<li><a target="##@####··" href="http://geocampo.tragsatec.es">Acceder</a></li>
							</ul>
						</div>						
					</div>
					<div class="sbi_panel" data-bg="images/7.jpg">
						<a href="#" class="sbi_label">Visor GIS</a>
						<div class="sbi_content">
							<ul>
								<li><a target="##@####··" href="https://sica.chsegura.es/UsuariosSegura/visorSICAH/VisorGuarderiasC.html?target=gr2ipz45jycnxm55agur5w55vailavan">Acceder</a></li>
							</ul>
						</div>						
					</div>
				</div>
			</div>
			<div class="more">
				<ul>
					<li>Otros servicios :</li>
					<li><a target="##@###···" href="http://www.chsegura.es/chs/cuenca/redesdecontrol/estadisticashidrologicas/partediario.html">PARTE DIARIO COMISARÍA DE AGUAS</a></li>
					<li><a target="##@###···" href="http://siam.imida.es/apex/f?p=101:1:2612484173223987">SIAM - SISTEMA DE INFORMACIÓN AGRARIA DE MURCIA</a></li>
					<li><a target="##@###···" href="http://www.aemet.es/es/eltiempo/prediccion/municipios/murcia-id30030">AEMET - AGENCIA ESTATAL DE METEOROLOGÍA</a></li>
				</ul>
			</div>
			
			<div class="topbar">
				<marquee><asp:Literal ID="Embalses" runat="server"></asp:Literal>
                </marquee>
			</div>
		</div>
		<div id="infouser">
            <asp:Literal ID="Infousuario" runat="server"></asp:Literal>
        </div>
		<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
		<script type="text/javascript" src="js/jquery.easing.1.3.js"></script>
		<script type="text/javascript" src="js/jquery.bgImageMenu.js"></script>
		<script type="text/javascript">
			$(function() {
				$('#sbi_container').bgImageMenu({
					defaultBg	: 'images/default.jpg',
					menuSpeed	: 300,
					border		: 1,
					type		: {
						mode		: 'verticalSlideAlt',
						speed		: 450,
						easing		: 'easeOutBack'
					}
				});
			});
		</script>
    </form>
</body>
</html>
