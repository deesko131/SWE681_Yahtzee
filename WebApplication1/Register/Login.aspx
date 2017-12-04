<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<style type="text/css">
		.auto-style1 {
			text-align: center;
            margin-left: 40px;
        }
	    .auto-style2 {
            font-size: x-large;
        }
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            width: 318px;
            text-align: right;
        }
        .auto-style5 {
            text-align: left;
            width: 222px;
        }
	    .auto-style6 {
            text-align: left;
        }
        .auto-style7 {
            width: 318px;
        }
        .auto-style8 {
            text-align: right;
            width: 222px;
        }
	</style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div class="auto-style1">
        	<strong><span class="auto-style2">Login Page</span></strong>
			<br />
            <br />
            <table class="auto-style3">
                <tr>
                    <td class="auto-style4">
			<asp:Label ID="Label1" runat="server" BorderColor="#CCCCFF" ForeColor="Black" Text="Player Name "></asp:Label>
			        </td>
                    <td class="auto-style5">
			<asp:TextBox ID="TextBoxPN" runat="server" OnTextChanged="TextBoxPN_TextChanged"></asp:TextBox>
                    </td>
                    <td class="auto-style6">
			            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxPN" ErrorMessage="Please enter Player Name" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">
			<asp:Label ID="Label2" runat="server" BorderColor="#CCCCFF" Text="Password"></asp:Label>
			        </td>
                    <td class="auto-style5">
			<asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td class="auto-style6">
			            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="Please enter Password" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <!-- cookies-->
                <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style5"><asp:CheckBox ID="chkPersistentCookie" runat="server" AutoPostBack="false" OnCheckedChanged="chkPersistentCookie_CheckedChanged" Text="Remember Me" /></td>
                <td class="auto-style6">&nbsp;</td>
                </tr>
                <tr>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style8">
			<asp:Button ID="ButtonLogin" runat="server" Text="Sign in" Height="34px" OnClick="ButtonLogin_Click" />
			        </td>
                <td class="auto-style6">&nbsp;</td>
                </tr>
            </table>
            <br />
			<br />
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<br />
			<br />
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<br />
			<br />
        </div>
    </form>
</body>
</html>
