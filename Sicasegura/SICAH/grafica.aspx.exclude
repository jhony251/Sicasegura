<%@ Page Language="VB" AutoEventWireup="false" CodeFile="grafica.aspx.vb" Inherits="SICAH_grafica" %>
<%@ Register TagPrefix="dotnet"  Namespace="dotnetCHARTING" Assembly="dotnetCHARTING"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0"/>
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0"/>
    <meta name="vs_defaultClientScript" content="JavaScript"/>
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>    
   
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
    <link rel="stylesheet" href="../styles.css"/>
    
    <script type="text/javascript" language="javascript" src="../js/utilesListados.js"></script>
    <script type="text/jscript" language="javascript" src="../js/calendar/calendar.js"></script>
    
         

</head>

<body bgcolor="#EEEAD2" style="font-size: 10pt; font-family: Verdana; text-decoration: none">
<form id="form1" runat="server">              
    <span id="dsp4"></span>
    <span id="imagepath" style="display:none">../js/calendar/images</span>

    <table cellspacing="2" align="center" style="border: 1px solid #666666; background-color: white; width:100%">
    <tr>
    <td>

        <table>
        <tr>
            <td class="subtitListado" nowrap >
                <b>Fecha Inicio</b>
            </td>
            <td>
                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="texto" Text="" Width="130px" AutoPostBack="True"></asp:TextBox>
                <asp:Image ID="imgFechaInicio" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif" Style="cursor: pointer" />
           </td>
            <td class="subtitListado" nowrap >
                &nbsp;&nbsp;&nbsp;&nbsp;<b>Fecha Fin</b>
            </td>
            <td>
                <asp:TextBox ID="txtFechaFin" runat="server" CssClass="texto" Text=""  Width="135px" AutoPostBack="True"></asp:TextBox>
                <asp:Image ID="imgFechaFin" runat="server" align="absmiddle" ImageAlign="Left" ImageUrl="~/images/calendario.gif" Style="cursor: pointer" />
                &nbsp; &nbsp;
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" class="boton" />
            </td>
        </tr>
        </table>
        
        <table>
            <tr>
                <td>
                    <dotnet:Chart id="Chart" runat="server" Width="700px" Height="400px"></dotnet:Chart>
                </td>
            </tr>
            <tr>
                <td>
                <asp:TextBox id="pto" runat="server" style="visibility:hidden"></asp:TextBox>
                </td>
            </tr>
        </table>
    </td>
    </tr>
    </table>

</form>
<script type="text/jscript">D=document.all;</script>
<script type="text/jscript">initDXCal();</script>

</body>
</html>
