<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="MainMenu.MainMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Menu</title>
    <style>
        html,
        body {
            height: 100%;
        }

        .auto-style1 {
            height: 600px;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="MainMenu" runat="server">

        <table style="text-align: center; margin-top: 5%;" class="auto-style1">
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnCRUDDrivers" runat="server" Width="50%" Height="50" Text="CRUD Drivers" BackColor="Black" ForeColor="White" OnClick="btnCRUDDrivers_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnCRUDWarrants" runat="server" Width="50%" Height="50" Text="CRUD Travel Warrants" BackColor="Black" ForeColor="White" OnClick="btnCRUDWarrants_Click" />
                </td>
            </tr>
             <tr>     
                <td colspan="2">
                    <asp:Button ID="btnShowVehicles" runat="server" Width="50%" Height="50" Text="Show Vehicles" BackColor="Black" ForeColor="White" OnClick="btnShowVehicles_Click" />
                </td>
            </tr>
            <tr>     
                <td colspan="2">
                    <asp:Button ID="btnXMLDatabase" runat="server" Width="50%" Height="50" Text="XML Converter" BackColor="Black" ForeColor="White" OnClick="btnXMLDatabase_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
