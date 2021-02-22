<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XML.aspx.cs" Inherits="MainMenu.XML" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <div style="text-align: right">
                <asp:Button ID="Back" runat="server" BackColor="Black" ForeColor="White" Width="10%" class="btn btn-dark" Text="Back" OnClick="Back_Click" />
            </div>
            <br />
            <br />
            <asp:Button ID="btnSQLtoXML" runat="server" Text="Backup Dataset to XML" OnClick="btnSQLtoXML_Click" BackColor="Black" ForeColor="White" />
            <br />
            <br />
            <asp:Label ID="LblInfo" runat="server" />
            <br />
            <br />
            <asp:Button ID="btnShowXML" runat="server" Text="Show XML" OnClick="btnShowXML_Click" Visible="false" BackColor="Black" ForeColor="White" />
            <br />
            <br />
            <asp:Label ID="LblResult" runat="server" Font-Bold="true" />
            <br />
            <br />
            <table style="text-align: center; height: 300px; width: 100%">
                <tr>
                    <td>
                        <asp:Button ID="btnBackup" runat="server" Width="50%" Height="50" Text="Backup" BackColor="Black" ForeColor="White" OnClick="btnBackup_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnResetDatabase" runat="server" Width="50%" Height="50" Text="Reset Database" BackColor="Black" ForeColor="White" OnClick="btnResetDatabase_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnRestore" runat="server" Width="50%" Height="50" Text="Restore Database" BackColor="Black" ForeColor="White" OnClick="btnRestore_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
