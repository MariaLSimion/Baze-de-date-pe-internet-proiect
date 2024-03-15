<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OffersPage.aspx.cs" Inherits="SimionMariaBDIProiectTablou.OffersPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Select a client"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownListSelectClient" runat="server" AutoPostBack="True" Height="16px" OnSelectedIndexChanged="DropDownListSelectClient_SelectedIndexChanged" Width="220px">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridViewOffersByClientName" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                    <td>
                        <asp:Label ID="LabelErr" runat="server" Text=":)"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Max no of offers winner "></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelMaxOffersWinner" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="ButtonDisplayMaxOffersWinner" runat="server" Height="35px" OnClick="ButtonDisplayMaxOffersWinner_Click" Text="Display max" Width="174px" />
                    </td>
                </tr>
                <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
</tr>
            </table>
        </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
