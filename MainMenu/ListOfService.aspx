<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListOfService.aspx.cs" Inherits="MainMenu.ListOfService" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="text-align: left; width: 100%;">
            <tr>
                <td>Service name:</td>
            </tr>
            <tr style="width: 25%">

                <td>
                    <asp:TextBox Width="50%" ID="InputName" runat="server" CssClass="offset-sm-0"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="margin-top: 3%; height: 250px; width: 100%; overflow: auto;">
            <asp:GridView ID="GridListofServices" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"
                runat="server" OnRowDataBound="GridListofServices_RowDataBound" OnSelectedIndexChanged="GridListofServices_SelectedIndexChanged" AutoGenerateColumns="false" Width="100%">
                <Columns>
                    <asp:BoundField DataField="IDServis" HeaderText="IDService" HeaderStyle-Width="294" ItemStyle-Width="150" />
                    <asp:BoundField DataField="Naziv_Servisa" HeaderText="Service_Name" HeaderStyle-Width="294" ItemStyle-Width="150" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:Label ID="LblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <table style="text-align: center; height: 300px; width: 100%">
            <tr>
                <td style="width: 33%">
                    <asp:Button ID="Add" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Add" OnClick="Add_Click" />
                </td>
                <td style="width: 33%">
                    <asp:Button ID="Update" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Update" OnClick="Update_Click" />
                </td>
                <td style="width: 33%">
                    <asp:Button ID="Delete" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Delete" OnClick="Delete_Click" />
                </td>
            </tr>
            <tr style="width: 33%">
                <td colspan="3">
                    <asp:Button ID="Back" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Back" OnClick="Back_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
