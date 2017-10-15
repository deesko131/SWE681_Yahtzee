<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="WebApplication1.Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        	<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
				<AlternatingRowStyle BackColor="White" />
				<Columns>
					<asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
					<asp:BoundField DataField="PlayerName" HeaderText="PlayerName" SortExpression="PlayerName" />
					<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
					<asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
				</Columns>
				<EditRowStyle BackColor="#2461BF" />
				<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
				<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
				<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
				<RowStyle BackColor="#EFF3FB" />
				<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
				<SortedAscendingCellStyle BackColor="#F5F7FB" />
				<SortedAscendingHeaderStyle BackColor="#6D95E1" />
				<SortedDescendingCellStyle BackColor="#E9EBEF" />
				<SortedDescendingHeaderStyle BackColor="#4870BE" />
			</asp:GridView>
			<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Registeration]"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
