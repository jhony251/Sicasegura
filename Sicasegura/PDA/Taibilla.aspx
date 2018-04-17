<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Taibilla.aspx.cs" Inherits="Taibilla" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GAM - Datos Instantáneos</title>
    <meta http-equiv="Page-Exit" content="revealTrans(Duration=1,Transition=7)" />
	<meta name="viewport" content="width=320,user-scalable=YES" />
	<meta http-equiv="refresh" content="2">
    <link rel="SHORTCUT ICON" href="GAM.ico"/>
    
    
</head>
<body style="margin:0px;background-image:url('images/background.jpg');Background-repeat:repeat-y; width:320px;">
    
    <form id="form1" runat="server">
    <div>
    <center>
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="Cod EA,Fecha" DataSourceID="SqlDataSource1" BackColor="White" 
                   BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                   <RowStyle ForeColor="#000066" />
                <Columns>
                    <asp:BoundField DataField="Cod EA" HeaderText="Cod EA" 
                        ReadOnly="True" SortExpression="Cod EA" />
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="True" 
                        SortExpression="Fecha" />
                    <asp:BoundField DataField="Nivel (m)" HeaderText="Nivel (m)" 
                        SortExpression="Nivel (m)" />
                </Columns>
                   <FooterStyle BackColor="White" ForeColor="#000066" />
                   <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                   <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                   <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        <br />
        </center>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBSICAConnectionString %>" 
                
                SelectCommand="SELECT codigopvycr AS [Cod EA], fecha AS Fecha, valor AS [Nivel (m)] FROM SICA_TopKapi_Niveles_inst"></asp:SqlDataSource>

    </div>

    </form>
</body>
</html>

