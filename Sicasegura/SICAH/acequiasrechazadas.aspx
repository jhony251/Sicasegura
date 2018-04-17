<%@ Page Language="VB" AutoEventWireup="false" CodeFile="acequiasrechazadas.aspx.vb" Inherits="SICAH_acequiasrechazadas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Guardería Fluvial</title>
     <link rel="stylesheet" href="..\styles.css">
      <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
  <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
  <meta name="vs_defaultClientScript" content="JavaScript">
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body bgcolor="#EEEAD2">
    <form id="form1" runat="server" method=post>
      <table width="100%" cellspacing="2" align="center" style="border: 1px solid #666666;background-color: white">
       <tr>
        <td>
          <table cellspacing="0" cellpadding="1" width="100%">
            <tr>
              <td>
              <asp:Label ID="lblCabecera" runat="server"></asp:Label>
              <asp:Label ID="lblPestanyas" runat="server"></asp:Label>
                <table align="center" width="100%" cellspacing="0" cellpadding="0" style="border: 1px solid #444444">
                <asp:Label ID="lbl1" runat="server"></asp:Label>
                <tr>
                  <td colspan="5">
                    <table cellpadding="0" cellspacing="0" width="100%">
                      <tr>
                        <td class="titulo">
                          &nbsp;GESTIÓN DE LECTURAS DATOS ACEQUIAS</td>
                      </tr>
                    </table>
                    <table width="100%" cellpadding="0" cellspacing="0">
                      <tr>
                        <td style="background-color: #8CA4B5; padding: 2px; padding-right: 10px; color: white;
                         border-bottom : 1px solid #eeeeee; font-weight: bold">
                         <asp:Label ID="lblNombrePVYCR" runat="server" Width="100%">NOMBRE PVYCR</asp:Label>
                        </td>
                      </tr>
                    </table>
                    <table width="100%">
                      <tr>
                        <td colspan="15" align="right" style="border-top: 1px solid #666666">
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                <asp:Panel ID="pnlPrincipal" runat="server" Visible="false" EnableViewState="true">
                  <asp:Repeater ID="rptRechazadas" runat="server" EnableViewState="true">
                   <HeaderTemplate>
                     <td style="font-weight: bold; text-align: center">
                       Código</td>
                     <td colspan="2" style="font-weight: bold; text-align: center">
                       Fecha</td>
                     <td style="font-weight: bold; text-align: center">
                       Escala (m)</td>
                     <td style="font-weight: bold; text-align: center">
                       Calado (m)</td>
                   </HeaderTemplate>
                   <ItemTemplate>
                     <tr>
                       <td class="L" style="background-color: #DDDDDD" align="center">
                         <%#Container.DataItem("CodigoPVYCR")%>
                       </td>
                       <td colspan="2" nowrap class="L" style="background-color: #DDDDDD" align="center">
                         <%#DataBinder.Eval(Container.DataItem, "fecha", "{0:dd/MM/yyyy HH:mm}")%>
                       <td class="L" style="background-color: #DDDDDD" align="center">
                         <%#DataBinder.Eval(Container.DataItem, "Escala_M", "{0:#,##0.##}")%>
                       </td>
                       <td class="L" style="background-color: #DDDDDD" align="center">
                         <%#DataBinder.Eval(Container.DataItem, "Calado_m","{0:#,##0.##}")%>
                       </td>
                     </tr>
                   </ItemTemplate>
                  </asp:Repeater>
                </asp:Panel>
                </tr>
                </table>
            </td>
            </tr>
           
          </table>
         
        </td>
       </tr>
      </table>
    </form>
</body>
</html>
