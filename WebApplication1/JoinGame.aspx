<%@ Page Title="Join Game" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JoinGame.aspx.cs" Inherits="Yahtzee._JoinGame" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<div class="jumbotron">
        <h1>Welcome
            <asp:Label ID="lblUserName" runat="server" Text="User"></asp:Label>
            !</h1>
        <p class="lead">&nbsp;</p>
        <p>
            <asp:GridView ID="gvGames" runat="server" AutoGenerateColumns="False" Caption="Games waiting for another player" CaptionAlign="Top" DataKeyNames="GameID" DataSourceID="SqlDataSource1" EmptyDataText="No games found." OnRowCommand="gvGames_RowCommand">
                <Columns>
                    <asp:BoundField DataField="PlayerOneName" HeaderText="Player Name" SortExpression="PlayerOneName" />
                    <asp:BoundField DataField="GameID" HeaderText="GameID" InsertVisible="False" ReadOnly="True" SortExpression="GameID" Visible="False" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Date Created" SortExpression="CreatedDate" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button runat="server" Text="Join" CommandName="Join" CommandArgument='<%# Eval("GameID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
		    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [PlayerOneName], [GameID], [CreatedDate] FROM [Games] WHERE ([PlayerTwoName] IS NULL)"></asp:SqlDataSource>
		</p>
        <p>
			&nbsp;</p>
    </div>

</asp:Content>
