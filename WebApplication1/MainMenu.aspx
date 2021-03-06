﻿<%@ Page Title="Main Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="Yahtzee._MainMenu" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<div class="jumbotron">
        <h1>Welcome
            <asp:Label ID="lblUserName" runat="server" Text="User"></asp:Label>
            !</h1>
        <p class="lead">&nbsp;</p>
        <p>
			<asp:HyperLink ID="lnkNewGame" runat="server" NavigateUrl="~/Yahtzee.aspx?game=New">Create New Game</asp:HyperLink>
&nbsp;|
			<asp:HyperLink ID="lnkJoinGame" runat="server" NavigateUrl="JoinGame.aspx">Join Game</asp:HyperLink>
		&nbsp;|
            <asp:HyperLink ID="lnkStats" runat="server" NavigateUrl="Stats.aspx">Stats</asp:HyperLink>
            &nbsp;|
            <asp:HyperLink ID="lnkMoveHistory" runat="server" NavigateUrl="MoveHistory.aspx">Game History</asp:HyperLink>
		&nbsp;|
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Register/logout.aspx">Logout</asp:HyperLink>
		</p>
        <p>
			&nbsp;</p>
    </div>

</asp:Content>
