<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Yahtzee._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<div class="jumbotron">
        <h1>Yahtzee - Online Game</h1>
        <p class="lead">&nbsp;</p>
        <p>
			<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx">Login</asp:HyperLink>
&nbsp;|
			<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Registeration.aspx">Register</asp:HyperLink>
		&nbsp;|
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/logout.aspx">Logout</asp:HyperLink>
		</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Game Rules: </h2>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
        </div>
        <div class="col-md-4">
            <h2>&nbsp;</h2>
            <p>
                &nbsp;</p>
        </div>
        <div class="col-md-4">
            <h2>&nbsp;</h2>
        </div>
    </div>

</asp:Content>
