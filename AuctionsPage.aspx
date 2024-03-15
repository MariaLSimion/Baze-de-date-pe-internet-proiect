<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuctionsPage.aspx.cs" Inherits="SimionMariaBDIProiectTablou.AuctionsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 854px;
        }
        .auto-style2 {
            width: 854px;
            height: 48px;
        }
        .auto-style3 {
            height: 48px;
        }
        .auto-style4 {
            width: 474px;
        }
        .auto-style5 {
            width: 422px;
        }
        .auto-style6 {
            width: 474px;
            height: 33px;
        }
        .auto-style7 {
            width: 422px;
            height: 33px;
        }
        .auto-style8 {
            height: 33px;
        }
        .auto-style9 {
            width: 854px;
            height: 33px;
        }
        .auto-style10 {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%;">
            
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Hour"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Location"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="MinimAmount"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Status(Open/Closed/Comming soon)"></asp:Label>
                </td>
            </tr>
            <tr>
    <td class="auto-style10">
        <asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                </td>
    <td class="auto-style10">
        <asp:TextBox ID="TextBoxHour" runat="server"></asp:TextBox>
                </td>
    <td class="auto-style10">
        <asp:TextBox ID="TextBoxLocation" runat="server"></asp:TextBox>
                </td>
    <td class="auto-style10">
        <asp:TextBox ID="TextBoxMinAmount" runat="server"></asp:TextBox>
                </td>
    <td class="auto-style10">
        <asp:TextBox ID="TextBoxStatus" runat="server"></asp:TextBox>
                </td>
</tr>
            <tr>
                <td>
                    <asp:Button ID="ButtonInsertAuction" runat="server" Height="43px" Text="Insert Auction" Width="136px" OnClick="ButtonInsertAuction_Click" />
                </td>
                <td>
                    <asp:Label ID="LabelErrorAddAuction" runat="server" Text="Add a new auction"></asp:Label>
                </td>
            </tr>
        </table>
        <div>
            <table style="width:100%;">
                <tr>
                    <td class="auto-style10">
                        <asp:TextBox ID="TextBoxIdAucUpdt" runat="server"></asp:TextBox>
                    </td>
                     <td class="auto-style10">
     <asp:TextBox ID="TextBoxDateAucUpdt" runat="server"></asp:TextBox>
 </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="TextBoxHourAucUpdt" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="TextBoxLocationAucUpdt" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxMinAmountAucUpdt" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxStatusAucUpdt" runat="server"></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td>
                        <asp:Button ID="ButtonUpdateAuction" runat="server" Height="35px" Text="Update auction" Width="138px" OnClick="ButtonUpdateAuction_Click" />
                    </td>
                    <td>
                        <asp:Label ID="LabelUpdateAuctionErr" runat="server" Text="Update Auction"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <asp:SqlDataSource ID="SqlDataSource_Auctions" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Auctions]" DeleteCommand="DELETE FROM [Auctions] WHERE [IdAuction] = @IdAuction" InsertCommand="INSERT INTO [Auctions] ([Date], [Hour], [Location], [MinimAmount], [Status]) VALUES (@Date, @Hour, @Location, @MinimAmount, @Status)" UpdateCommand="UPDATE [Auctions] SET [Date] = @Date, [Hour] = @Hour, [Location] = @Location, [MinimAmount] = @MinimAmount, [Status] = @Status WHERE [IdAuction] = @IdAuction">
            <DeleteParameters>
                <asp:Parameter Name="IdAuction" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter DbType="Date" Name="Date" />
                <asp:Parameter DbType="Time" Name="Hour" />
                <asp:Parameter Name="Location" Type="String" />
                <asp:Parameter Name="MinimAmount" Type="Int32" />
                <asp:Parameter Name="Status" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter DbType="Date" Name="Date" />
                <asp:Parameter DbType="Time" Name="Hour" />
                <asp:Parameter Name="Location" Type="String" />
                <asp:Parameter Name="MinimAmount" Type="Int32" />
                <asp:Parameter Name="Status" Type="String" />
                <asp:Parameter Name="IdAuction" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDataSource_Auctions" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" AutoGenerateSelectButton="True" DataKeyNames="IdAuction" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="IdAuction" HeaderText="IdAuction" InsertVisible="False" ReadOnly="True" SortExpression="IdAuction" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="Hour" HeaderText="Hour" SortExpression="Hour" />
                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                <asp:BoundField DataField="MinimAmount" HeaderText="MinimAmount" SortExpression="MinimAmount" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
                </td>
                
                <td class="auto-style1">
                    <asp:GridView ID="GridViewWantedAuction" runat="server" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None">
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <FooterStyle BackColor="Tan" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <SortedAscendingCellStyle BackColor="#FAFAE7" />
                        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                        <SortedDescendingCellStyle BackColor="#E1DB9C" />
                        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="LabelChangeStatus" runat="server" Text="Change status of a painting"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBoxIdWantedAuction" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style3">
                    <asp:Label ID="Label1" runat="server" Text="Input the id of the auction you want to search for"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:TextBox ID="TextBoxIdPainting" runat="server" Width="288px"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    <asp:Button ID="ButtonDisplayWantedAuction" runat="server" OnClick="ButtonDisplayWantedAuction_Click" Text="Display" Width="224px" />
                </td>
                <td>
                    <asp:Label ID="LabelError" runat="server" Font-Bold="True" ForeColor="Red" Text=":)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">
                    <asp:Button ID="ButtonChangeStatus" runat="server" OnClick="ButtonChangeStatus_Click" Text="Change Status" Width="267px" />
                </td>
                <td class="auto-style9">
                    <asp:Label ID="LabelStatusError" runat="server" Font-Bold="True" ForeColor="#FF0066" Text="&lt;3"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:Button ID="ButtonRefreshAuctionStatus" runat="server" OnClick="ButtonRefreshAuctionStatus_Click" Text="Refresh auction status" Width="199px" Visible="False" />
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource_Paintings" runat="server"></asp:SqlDataSource>
        <table style="width:100%;" border="1">
           
            <tr>
    <td class="auto-style4">
        <asp:Label ID="LabelIdAuction" runat="server" Text="Input the id of an auction"></asp:Label>
                </td>
    <td class="auto-style5">
        <asp:Label ID="LabelIdPainting" runat="server" Text="Input the id of a painting"></asp:Label>
                </td>
    <td>Amount offered</td>
</tr>
            <tr>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBoxAuction" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="TextBoxPainting" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelAmount" runat="server" Font-Bold="True" Text="0"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:Button ID="ButtonCalculateAmountOffered" runat="server" OnClick="ButtonCalculateAmountOffered_Click" Text="Calculate amout offered" Width="274px" />
                </td>
                <td class="auto-style7"></td>
                <td class="auto-style8">&nbsp;</td>
            </tr>
        </table>
        <p>
            &nbsp;</p>
        <table style="width:100%;" border="1">
            <tr>
                <td>
                    <asp:Label ID="LabelOpenAuc" runat="server" Text="Our open auctions"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabelCommingSoon" runat="server" Text="Our future auctions"></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:Button ID="ButtonOpenAuc" runat="server" Text="Click here to display out open auctions" Width="359px" OnClick="ButtonOpenAuc_Click" />
                </td>
                <td>
                    <asp:Button ID="ButtonCommingSoonAuc" runat="server" Text="Click here to display our future auctions" Width="311px" OnClick="ButtonCommingSoonAuc_Click" />
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridViewOpenAuc" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                    </asp:GridView>
                </td>
                <td>
                    <asp:GridView ID="GridViewCommingSoonAuc" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                    </asp:GridView>
                </td>
               
            </tr>
        </table>
    </form>
</body>
</html>
