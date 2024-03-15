<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GraphsPage.aspx.cs" Inherits="SimionMariaBDIProiectTablou.GraphsPage" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1" border="1">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="No of paintings / technique"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="No of offers / technique"></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:Chart ID="Chart1" runat="server">
                            <series>
                                <asp:Series Name="ArtTechniques" ChartType="Column" XValueMember="Technique" YValueMembers="NumberOfArtworks">
                                </asp:Series>
                            </series>
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </chartareas>
                        </asp:Chart>
                    </td>
                    <td>
                        <asp:Chart ID="Chart2" runat="server">
                            <series>
                                <asp:Series Name="TotalOffers" ChartType="Doughnut" XValueMember="Technique" YValueMembers="TotalOfferAmount" YValuesPerPoint="2" >
                                    
                                </asp:Series>
                            </series>
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </chartareas>
                        </asp:Chart>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Total amount won by the gallery on each closed auction"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                   
                </tr>
                <tr>
                    <td>
                        <asp:Chart ID="Chart3" runat="server">
                            <series>
                                <asp:Series  Name="TotalWinningOffers" XValueMember="IdAuction" YValueMembers="TotalWinningAmount" ChartType="Bar" IsValueShownAsLabel="true">
                                </asp:Series>
                            </series>
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </chartareas>
                        </asp:Chart>
                    </td>
                    <td>&nbsp;</td>
                    
                    </tr>
            </table>
        </div>
    </form>
</body>
</html>
