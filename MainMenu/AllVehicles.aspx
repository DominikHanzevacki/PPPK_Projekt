<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllVehicles.aspx.cs" Inherits="MainMenu.AllVehicles" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top: 3%; height: 250px; width: 100%; overflow: auto;">
            <asp:GridView ID="GridListofVehicles" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"
                runat="server" OnRowDataBound="GridListofVehicles_RowDataBound" OnSelectedIndexChanged="GridListofVehicles_SelectedIndexChanged" AutoGenerateColumns="false" Width="100%">
                <Columns>
                    <asp:BoundField DataField="IDVozilo" HeaderText="IDVehicle" HeaderStyle-Width="294" ItemStyle-Width="150" />
                    <asp:BoundField DataField="Tip" HeaderText="Type" HeaderStyle-Width="294" ItemStyle-Width="150" />
                    <asp:BoundField DataField="Marka" HeaderText="Brand" HeaderStyle-Width="294" ItemStyle-Width="150" />
                    <asp:BoundField DataField="Godina_Proizvodnje" HeaderText="Year of manufacture" HeaderStyle-Width="294" ItemStyle-Width="150" />
                    <asp:BoundField DataField="Inicijalno_Stanje_Kilometara" HeaderText="Initial_State_of_Kilometers" HeaderStyle-Width="294" ItemStyle-Width="150" />
                </Columns>
            </asp:GridView>
        </div>
            <div style="text-align:center; margin-top:3%">
                <asp:Button ID="btnCreateHTMLDocument" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Create HTML Document" OnClick="btnCreateHTMLDocument_Click" />
            </div>
    </form>
</body>
</html>
