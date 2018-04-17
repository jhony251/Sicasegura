<%@ Page Language="C#" AutoEventWireup="true" CodeFile="galeria.aspx.cs" Inherits="SICAH_Galeria_galeria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <script src="resource/lightbox_plus.js" type="text/javascript"></script>
    <link href="lightbox/css/screen.css" rel="stylesheet" type="text/css" />
    <link href="resource/lightbox.css" rel="stylesheet" type="text/css" />
    <link href="resource/sample.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tprinciapl" cellspacing="2" cellpadding="1" style="width:100%"  >
            <!-- Enlaces -->
            <tr>               
                <td>
                    <table width="100%">
                        <tr>
                            <asp:Literal ID="LIT_Menu" runat="server"></asp:Literal>
                        </tr>
                    </table>
                </td>
            </tr>
            <!-- Croquis -->
            <tr>
                
                    <asp:Literal ID="LIT_Croquis" runat="server"></asp:Literal>
                
            </tr> 
            
            <!-- Subir ficheros -->
            <tr>
                <td> 
                    <table width="100%" align="center">
                        <tr>
                            <td style="background: #263E53; color: white;cursor:hand" colspan="2">
                            <a name="ancla">
                            <a href="#ancla" style="color:White;">Incluir imagenes asociadas en galería.</a>
                            </td>
                        </tr>                            
                        <tr>
                            <td>                        
                                <asp:FileUpload ID="FileUpload" runat="server" Font-Names="Verdana" Font-Size="Smaller" Width="80%" style="color: Black"/>                                        
                            </td>
                            <td>
                                <asp:Button ID="botonFileUpload" Runat="server" Text="Añadir Imagen" OnClick="BotonFileUpload_Click" Font-Size="Smaller" style="color: Black" />
                            </td>
                        </tr>
                    </table>                                                                                      
                    <br />
                    <hr />                    
                </td>
            </tr> 
                        
            <!-- Galería -->
            <tr>
                <td>
                    <asp:Literal ID="LIT_Galeria" runat="server"></asp:Literal>
                </td>
            </tr>
            
        </table>
        
    </div>
    </form>
</body>
</html>
