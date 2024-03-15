<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaintingsPage.aspx.cs" Inherits="SimionMariaBDIProiectTablou.PaintingsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style3 {
            width: 64%;
        }
        .auto-style6 {
            height: 26px;
        }
        .auto-style8 {
            height: 26px;
            width: 130px;
        }
        .auto-style9 {
            width: 35px;
            height: 26px;
        }
        .auto-style11 {
            height: 26px;
            width: 158px;
        }
        .auto-style12 {
            height: 26px;
            width: 208px;
        }
        .auto-style13 {
            height: 26px;
            width: 123px;
        }
        .auto-style14 {
            width: 61px;
            height: 26px;
        }
        .auto-style15 {
            width: 74px;
            height: 26px;
        }
        .auto-style16 {
            width: 488px;
        }
        .auto-style17 {
            width: 583px;
        }
        .auto-style18 {
            width: 858px;
        }
        .auto-style19 {
            width: 100%;
            height: 605px;
        }
        .auto-style20 {
            margin-top: 46px;
        }
        .auto-style21 {
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:SqlDataSource ID="SqlDataSource_Painting" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [IdLucrare], [Title], [Artist], [Year], [Technique], [ValoareMinima], [Dimensions], [Disponibilitate] FROM [Paintings]" DeleteCommand="DELETE FROM [Paintings] WHERE [IdLucrare] = @IdLucrare" InsertCommand="INSERT INTO [Paintings] ([Title], [Artist], [Year], [Technique], [ValoareMinima], [Dimensions], [Disponibilitate]) VALUES (@Title, @Artist, @Year, @Technique, @ValoareMinima, @Dimensions, @Disponibilitate)" UpdateCommand="UPDATE [Paintings] SET [Title] = @Title, [Artist] = @Artist, [Year] = @Year, [Technique] = @Technique, [ValoareMinima] = @ValoareMinima, [Dimensions] = @Dimensions, [Disponibilitate] = @Disponibilitate WHERE [IdLucrare] = @IdLucrare">
            <DeleteParameters>
                <asp:Parameter Name="IdLucrare" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="Artist" Type="String" />
                <asp:Parameter Name="Year" Type="Int32" />
                <asp:Parameter Name="Technique" Type="String" />
                <asp:Parameter Name="ValoareMinima" Type="Int32" />
                <asp:Parameter Name="Dimensions" Type="String" />
                <asp:Parameter Name="Disponibilitate" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="Artist" Type="String" />
                <asp:Parameter Name="Year" Type="Int32" />
                <asp:Parameter Name="Technique" Type="String" />
                <asp:Parameter Name="ValoareMinima" Type="Int32" />
                <asp:Parameter Name="Dimensions" Type="String" />
                <asp:Parameter Name="Disponibilitate" Type="String" />
                <asp:Parameter Name="IdLucrare" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <table class="auto-style3" border="1">
            <tr>
                <td class="auto-style14">
                    <asp:Label ID="LabelIDLucrare" runat="server" Text="ID"></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="LabelTitle" runat="server" Text="Title"></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:Label ID="LabelArtist" runat="server" Text="Artist"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:Label ID="LabelYear" runat="server" Text="Year"></asp:Label>
                </td>
                <td class="auto-style12">
                    <asp:Label ID="LabelTechique" runat="server" Text="Technique"></asp:Label>
                </td>
                <td class="auto-style6">
                    <asp:Label ID="LabelDimensions" runat="server" Text="Dimensions"></asp:Label>
                </td>
                <td class="auto-style13">
                    <asp:Label ID="LabelMinValue" runat="server" Text="MinValue"></asp:Label>
                </td>
                <td class="auto-style15">
                    <asp:Label ID="LabelDisponibilitate" runat="server" Text="Availability"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBoxIdUpdt" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxTitleUpdt" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxArtistUpdt" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxYearUpdt" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxTechniqueUpdt" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxDimensionsUpdt" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxMinValUpdt" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxAvailabilityUpdt" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="ButtonUpdate" runat="server" OnClick="ButtonUpdate_Click" Text="Update Painting" />
                </td>
            </tr>
            
           
        </table>
        <table class="auto-style19">
            <tr>
                <td class="auto-style18">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" DataKeyNames="IdLucrare" DataSourceID="SqlDataSource_Painting" AllowPaging="True" AllowSorting="True" AutoGenerateDeleteButton="True" AutoGenerateSelectButton="True" ShowFooter="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CssClass="auto-style20" Height="523px" Width="865px" PageSize="20" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating">
            <Columns>
                <asp:BoundField DataField="IdLucrare" HeaderText="IdLucrare" InsertVisible="False" ReadOnly="True" SortExpression="IdLucrare" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Artist" HeaderText="Artist" SortExpression="Artist" />
                <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                <asp:BoundField DataField="Technique" HeaderText="Technique" SortExpression="Technique" />
                <asp:BoundField DataField="Dimensions" HeaderText="Dimensions" SortExpression="Dimensions" />
                <asp:BoundField DataField="ValoareMinima" HeaderText="ValoareMinima" SortExpression="ValoareMinima" />
                <asp:BoundField DataField="Disponibilitate" HeaderText="Disponibilitate" SortExpression="Disponibilitate" />
            </Columns>
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" Font-Bold="true" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
        </asp:GridView>
                </td>
                <td>
                    <asp:Image ID="ImageSelectedPainting" runat="server" Height="667px" Width="816px" CssClass="auto-style21" />
                </td>
        
            </tr>
            
        </table>
        <asp:RadioButton ID="RadioButtonAll" runat="server" AutoPostBack="True" GroupName="Availability" Checked="True" Text="All" OnCheckedChanged="RadioButtonAll_CheckedChanged" />
        <asp:RadioButton ID="RadioButtonUnavailable" runat="server" AutoPostBack="True" GroupName="Availability" Text="Unavailable" OnCheckedChanged="RadioButtonUnavailable_CheckedChanged" />
        <asp:RadioButton ID="RadioButtonAvailable" runat="server" AutoPostBack="True" GroupName="Availability" OnCheckedChanged="RadioButtonAvailable_CheckedChanged" Text="Available" />
        <div>
            <table style="width:100%;">
                <tr>
                    <td class="auto-style16">
                        <asp:Label ID="LabelFiltrareDupaTehnica" runat="server" Font-Bold="True" Font-Overline="True" Font-Size="Large" Text="Filter by techinque"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <asp:CheckBox ID="CheckBoxOil" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxOil_CheckedChanged" Text="Oil on canvas" />
                    </td>
                    <td class="auto-style17">
                        <asp:CheckBox ID="CheckBoxTempera" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxTempera_CheckedChanged" Text="Tempera on wood" />
                    </td>
                    <td>
                        <asp:CheckBox ID="CheckBoxPastel" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBoxPastel_CheckedChanged" Text="Pastel on cardboard" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="ButtonToTheAuctions" runat="server" Height="45px" Text="Auctions" Width="206px" OnClick="ButtonToTheAuctions_Click" PostBackUrl="~/AuctionsPage.aspx" />
        <asp:Button ID="ButtonGoToOffersPage" runat="server" Height="44px" PostBackUrl="~/OffersPage.aspx" Text="Offers" Width="199px" />
        <asp:Button ID="ButtonGoToGraphsPage" runat="server" Height="44px" PostBackUrl="~/GraphsPage.aspx" Text="Graphs" Width="188px" />
        <table style="width:100%;" border="1" >
            <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Title"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Artist"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Year"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Technique"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Dimensions"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Minimun Value"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Availability"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="URL"></asp:Label>
                    </td>

            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBoxTitle" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxArtist" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxYear" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxTechnique" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxDimensions" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxMinVal" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxAvailability" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxURL" runat="server"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Button ID="ButtonInsertPainting" runat="server" OnClick="ButtonInsertPainting_Click" Text="Add a painting" Width="171px" />
                    
                </td>
                <td>
                    <asp:Label ID="LabelError" runat="server" Text="Add a painting"></asp:Label>
                </td>
                <td>
    <asp:Label ID="LabelValidation" runat="server" Text=":)" Font-Bold="True" ForeColor="Red"></asp:Label>
</td>
                
            </tr>
        </table>
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Select an artist"></asp:Label>
                </td>
               
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownListartisti" runat="server" AutoPostBack="True" Height="16px" Width="548px" OnSelectedIndexChanged="DropDownListartisti_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridViewPaintingsSelectedByartist" runat="server" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Height="141px" Width="461px">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                </td>
                
            </tr>
        </table>
    </form>
</body>
</html>
