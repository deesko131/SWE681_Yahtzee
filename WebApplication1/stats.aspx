<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stats.aspx.cs" Inherits="WebApplication1.stats" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            background-color: #CCFFFF;
        }
        .auto-style3 {
            font-size: large;
        }
        .auto-style4 {
            width: 100%;
        }
        .auto-style6 {
            width: 725px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="auto-style1">
                <table class="auto-style4">
                    <tr>
                        <td class="auto-style6"><strong>
                <span class="auto-style3">&nbsp;&nbsp;&nbsp;&nbsp; Player Statistics</span></strong></td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Register/logout.aspx">Logout</asp:HyperLink>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1" HorizontalAlign="Center" CssClass="auto-style2">
                    <Columns>
                        <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME" ReadOnly="True" />
                        <asp:BoundField DataField="WINS" HeaderText="WINS" SortExpression="WINS" ReadOnly="True" />
                        <asp:BoundField DataField="LOSSES" HeaderText="LOSSES" SortExpression="LOSSES" ReadOnly="True" />
                        <asp:BoundField DataField="DRAWS" HeaderText="DRAWS" SortExpression="DRAWS" ReadOnly="True" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT NAME, 
SUM(CASE WHEN NAME = WINNER THEN 1 ELSE 0 END) as WINS,
SUM(CASE WHEN NAME != WINNER THEN 1 ELSE 0 END) as LOSSES,
SUM(CASE WHEN WINNER = 'Draw' THEN 1 ELSE 0 END) as DRAWS
FROM
(SELECT GameID, NAME, WINNER
FROM
(SELECT GameID, PlayerOneName as NAME, WINNER FROM Games 
UNION
SELECT GameID, PlayerTwoName as NAME, WINNER FROM Games) as tbl
)as tbl2
GROUP BY NAME
 "></asp:SqlDataSource>
        </div>
    </form>
 </body>
</html>
