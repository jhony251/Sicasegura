<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GraficoConsumos.aspx.vb" Inherits="Listados_GraficoConsumos" %>
<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>
    

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gráfico de Consumos</title>
    <script type="text/javascript" src="Silverlight.js"></script>
    <script type="text/javascript">

        function onSilverlightError(sender, args) {

            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }
            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            var errMsg = "Error no controlado en la aplicación de Silverlight 2 " + appSource + "\n";

            errMsg += "Código: " + iErrorCode + "    \n";
            errMsg += "Categoría: " + errorType + "       \n";
            errMsg += "Mensaje: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "Archivo: " + args.xamlFile + "     \n";
                errMsg += "Línea: " + args.lineNumber + "     \n";
                errMsg += "Posición: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "Línea: " + args.lineNumber + "     \n";
                    errMsg += "Posición: " + args.charPosition + "     \n";
                }
                errMsg += "Nombre de método: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <div>           
            <div id="silverlightControlHost" style="height:1500px;">
		    <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
			<param name="source" value="../ClientBin/SilverlightSica.xap"/>
			<param name="onerror" value="onSilverlightError" />
			<param name="background" value="white" />
		<param name="minRuntimeVersion" value="3.0.40624.0" />
			<param name="autoUpgrade" value="true" />
			<param name="InitParams" value="<%=initParams() %>" />
		 <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40624.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
		</object>
		<iframe style='visibility:hidden;height:0;width:0;border:0px'></iframe>
    </div>         
          
        </div>
    </form>
</body>
</html>
