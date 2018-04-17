<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Datos.aspx.vb" Inherits="Datos" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="EstilosIframe.css" />
    <script language="javascript">
        function dateChanged(texto) {
            var resultado;
            resultado = isDate(texto);
            if (resultado == true) {
                //document.getElementById("txtFecha").innerText = texto + " 00:00";
            }
        }
        
        function isDate(fech){

            var res;
            var fecha = new Date(fech);
            if (isNaN(fecha)==false){
            res=true
            }else{res=false}

            return res;
    }
    </script>
</head>
<body >
    <form id="form1" runat="server">
    <div>
    <table width="320" height="100%" class="tabla" cellpadding="0" cellspacing="0">
        <tr><td align="center" colspan="3" style="padding-bottom:5px;"><asp:Label ID="lblTitulo" runat="server"></asp:Label></td></tr>
        <tr>
            <td valign="top" width="50" Height="12px">Fecha</td>
            <td width="30" valign="top" style="padding-right:5px;"><asp:TextBox ID="txtFecha" runat="server" CssClass="caja" onchange="dateChanged(this.value)"></asp:TextBox></td>
            <td rowspan="7" valign="top"><asp:Calendar runat="server" ID="calendario" CssClass="calendar" >
            <TitleStyle BorderColor="#105A7B" BorderWidth="3"
            BackColor="#105A7B" Height="10px" Width="30px" />            
            </asp:Calendar></td>
        </tr>
        <tr>
            <td valign="top" colspan="2" Height="12px"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="txtFecha" Text="* La fecha es obligatoria" ForeColor="Red" class="alertas"></asp:RequiredFieldValidator>           
            <asp:comparevalidator id="cvFecha" runat="server" Display="Dynamic" ControlToValidate="txtFecha" Type="Date" Operator=DataTypeCheck class="alertas" Text="* Fecha incorrecta"></asp:comparevalidator>
            </td>            
        </tr>
        <asp:Panel runat="server" ID="pnlAcequias" Visible="false">        
        <tr>     
            <td title="Formato: 0.000" Height="12px">Caudal (m3/s)</td>
            <td title="Formato: 0.000"><asp:TextBox ID="txtCaudal" runat="server" class="caja"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" Height="12px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCaudal" Display="Dynamic" Text="* El caudal es obligatorio"  class="alertas"></asp:RequiredFieldValidator>           
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCaudal" ErrorMessage="* Formato incorrecto (0.000)" ValidationExpression="[\-\+]{0,1}\d+([\.\,][0-9]{0,3})?$" Display="Dynamic" class="alertas"></asp:RegularExpressionValidator>
            </td>                
        </tr>        
        <tr>            
            <td Height="12px">Nivel</td>
            <td><asp:TextBox ID="txtNivel" runat="server" CssClass="caja"></asp:TextBox></td>
        </tr>
        <tr>   
            <td colspan="2" Height="12px">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtNivel" ErrorMessage="* El nivel tiene que ser numérico" ValidationExpression="[\-\+]{0,1}\d+([\.\,]\d+){0,1}" Display="Dynamic" class="alertas"></asp:RegularExpressionValidator>
            </td>           
        </tr>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlMotores" Visible="false">
        <tr>            
            <td title="Formato: 0.000" Height="12px">Total (Kwh)</td>
            <td title="Formato: 0.000"><asp:TextBox ID="txttotalKwh" runat="server" class="caja"></asp:TextBox></td>
        </tr>
        <tr>     
            <td colspan="2" Height="12px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txttotalKwh" Display="Dynamic" Text="* El volumen es obligatorio"  class="alertas"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txttotalKwh" ErrorMessage="* Formato incorrecto (0.000)" ValidationExpression="[\-\+]{0,1}\d+([\.\,][0-9]{0,3})?$" Display="Dynamic" class="alertas"></asp:RegularExpressionValidator>
            </td>                
        </tr>        
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAlimentacion" Visible="false">
        <tr>            
            <td title="Formato: 0.000" Height="12px">Lectura (m3/s)</td>
            <td title="Formato: 0.000"><asp:TextBox ID="txtLecturaAlim" runat="server" class="caja"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" Height="12px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLecturaAlim" Display="Dynamic" Text="* El volumen es obligatorio"  class="alertas"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtLecturaAlim" ErrorMessage="* Formato incorrecto (0.000)" ValidationExpression="[\-\+]{0,1}\d+([\.\,][0-9]{0,3})?$" Display="Dynamic" class="alertas"></asp:RegularExpressionValidator>
            </td>                
        </tr>        
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlHorometros" Visible="false">
        <tr>            
            <td title="Formato: 0.000" Height="12px">Horas (h)</td>
            <td title="Formato: 0.000"><asp:TextBox ID="txtHoras" runat="server" class="caja"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" Height="12px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtHoras" Display="Dynamic" Text="* Las horas son obligatorias"  class="alertas"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtHoras" ErrorMessage="* Formato incorrecto (0.000)" ValidationExpression="[\-\+]{0,1}\d+([\.\,][0-9]{0,3})?$" Display="Dynamic" class="alertas"></asp:RegularExpressionValidator>
            </td>                
        </tr>        
        </asp:Panel>        
        <tr>
            <td colspan="3" align="left" style="padding-top:10px;">
                <asp:Button Text="Aceptar" runat="server" ID="btnAceptar" CssClass="boton" />           
                <asp:Button Text="Cancelar" runat="server" ID="btnCancelar" CssClass="boton" />
            </td>
        </tr>
        
    </table>
    </div>
    </form>
</body>
</html>
