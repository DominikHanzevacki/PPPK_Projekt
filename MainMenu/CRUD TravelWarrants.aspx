<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRUD TravelWarrants.aspx.cs" Inherits="MainMenu.CRUD_TravelWarrants" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="text-align: left; width: 100%;">
                <tr>
                   
                    <td>Place of Departure:</td>
                    <td>Driver ID:</td>
                    <td>Date of Issue:</td>
                    
                </tr>
                <tr style="width: 25%">
                    
                    <td>
                        <asp:TextBox Width="75%" ID="Place_of_Departure" runat="server" CssClass="offset-sm-0"></asp:TextBox>

                    </td>
                    <td>
                        <asp:DropDownList ID="DdlDrivers" runat="server" Width="75%" AutoPostBack="True" Height="30px"></asp:DropDownList>
                    </td>
                      <td>
                        <asp:TextBox Width="75%" ID="Date_of_issue" runat="server" CssClass="offset-sm-0"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td>Destination Place:</td>
                    <td>Vehicle ID:</td>
                    <td>Delivery Date:</td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:TextBox Width="75%" ID="Destination_Place" runat="server" CssClass="offset-sm-0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlVehicles" runat="server" Width="75%" AutoPostBack="True" Height="30px"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox Width="75%" ID="Delivery_Date" runat="server" CssClass="offset-sm-0"></asp:TextBox>
                    </td>
                   
                </tr>
            </table>
            <div style="margin-top: 3%; height: 250px; width: 100%; overflow:auto;">
                <asp:GridView ID="GridListOfWarrants" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" runat="server" OnRowDataBound="GridListOfWarrants_RowDataBound" OnSelectedIndexChanged="GridListOfWarrants_SelectedIndexChanged" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="IDPutniNalog" HeaderText="IDPutniNalog" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Mjesto_Polaska" HeaderText="Mjesto_Polaska" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Mjesto_Putovanja" HeaderText="Mjesto Putovanja" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="VozacID" HeaderText="VozacID" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="VoziloID" HeaderText="VoziloID" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Datum_Izdavanja" HeaderText="Datum_Izdavanja" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Datum_Predaje" HeaderText="Datum_Predaje" HeaderStyle-Width="294" ItemStyle-Width="150" />
                        <asp:BoundField DataField="Tip_Putnog_Naloga" HeaderText="Tip" HeaderStyle-Width="294" ItemStyle-Width="150" />
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
                <tr style="width:100%">
                    <td colspan="3" >
                        <asp:Button ID="Back" runat="server" Height="50" BackColor="Black" ForeColor="White" Width="60%" class="btn btn-dark" Text="Back" OnClick="Back_Click"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
