<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRUD Drivers.aspx.cs" Inherits="MainMenu.CRUD_Drivers" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CRUD Drivers</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="text-align: left; width: 100%;">
                <tr>
                   
                    <td>Name:</td>
                    <td>Mobile Number:</td>
                    
                </tr>
                <tr style="width: 25%">
                    
                    <td>
                        <asp:TextBox Width="75%" ID="InputIme" runat="server" CssClass="offset-sm-0"></asp:TextBox></td>
                    <td>
                        <asp:TextBox Width="75%" ID="InputBrojMobitela" runat="server" CssClass="offset-sm-0"></asp:TextBox>

                    </td>
                    
                </tr>
                <tr>
                    <td>Surname:</td>
                    <td>Driver's license number:</td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:TextBox Width="75%" ID="InputPrezime" runat="server" CssClass="offset-sm-0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox Width="75%" ID="InputBrojVozacke" runat="server" CssClass="offset-sm-0"></asp:TextBox>
                    </td>
                   
                </tr>
            </table>
            <div style="margin-top: 3%; height: 250px; width: 100%; overflow:auto;">
                <asp:GridView ID="GridListofDrivers" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"
                    runat="server" OnRowDataBound="GridListofDrivers_RowDataBound" OnSelectedIndexChanged="GridListofDrivers_SelectedIndexChanged" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="IDVozac" HeaderText="IDDriver" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Ime" HeaderText="Name" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Prezime" HeaderText="Surname" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Broj_Mobitela" HeaderText="Mobile Number" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Broj_Vozacke_Dozvole" HeaderText="Driver's license number" HeaderStyle-Width="294" ItemStyle-Width="150" />
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Label ID="LblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            <table style="text-align:center; height:300px;  width:100%" >
                <tr>
                    <td style="width:33%">
                        <asp:Button ID="Add" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Add" OnClick="Add_Click" />
                    </td>
                    <td style="width:33%">
                        <asp:Button ID="Update" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Update" OnClick="Update_Click" />
                    </td>
                    <td style="width:33%">
                       <asp:Button ID="Delete" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Delete" OnClick="Delete_Click"/>
                    </td>
                </tr>
                <tr style="width:33%">
                    <td colspan="1" >
                        <asp:Button ID="btnExport" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Export" OnClick="btnExport_Click"/>
                    </td>
                    <td colspan="1" >
                        <asp:Button ID="Back" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Back" OnClick="Back_Click"/>
                    </td>
                    <td colspan="1" >
                        <asp:Button ID="btnImport" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Import" OnClick="btnImport_Click"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
