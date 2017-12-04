<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoveHistory.aspx.cs" Inherits="WebApplication1.MoveHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            font-size: large;
        }
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            width: 649px;
        }
        .auto-style5 {
            font-size: medium;
            color: #0066CC;
        }
        .auto-style6 {
            font-size: medium;
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="auto-style1">
                <span class="auto-style2"><strong>
                <br />
                <table class="auto-style3">
                    <tr>
                        <td>&nbsp;</td>
                        <td class="auto-style4">
                <span class="auto-style2"><strong>Move History </strong></span>
                        </td>
                        </strong>
                        <td>
                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="auto-style5" NavigateUrl="~/Register/logout.aspx">Logout</asp:HyperLink>
                        </td>
                    </tr>
                    <strong>
                    <tr>
                        <td>&nbsp;</td>
                        <td class="auto-style4">
            &nbsp;&nbsp;
            <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" DataKeyNames="GameID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="auto-style6">
            <Columns>
                <asp:BoundField HeaderText="Game ID" DataField="GameID" />
                <asp:BoundField HeaderText="First Player" DataField="PlayerOneName" />
                <asp:BoundField HeaderText="Second Player" DataField="PlayerTwoName" />
                <asp:BoundField HeaderText="Start Date" DataField="CreatedDate" />
                <asp:BoundField HeaderText="End Date" DataField="UpdatedDate" />
                <asp:BoundField HeaderText="Winner" DataField="Winner" />
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
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td class="auto-style4">
            <asp:GridView ID="GridView2" runat="server" HorizontalAlign="Center" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
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
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
                <br />
                </strong></span>
                <br />
&nbsp;&nbsp;
                <div class="auto-style1">
                </div>
            </div>
            <div class="auto-style1">
            </div>
            <br />
        </div>
    </form>
</body>
</html>
