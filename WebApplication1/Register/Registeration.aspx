<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="true" CodeBehind="Registeration.aspx.cs" Inherits="WebApplication1.Registeration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<style type="text/css">
		.auto-style1 {
			width: 100%;
		}
		.auto-style2 {
			width: 120px;
		}
		.auto-style3 {
			width: 188px;
		}
		.auto-style4 {
			width: 120px;
			text-align: right;
			background-color: #CCCCFF;
		}
		.auto-style5 {
			width: 120px;
			text-align: right;
			height: 32px;
			background-color: #CCCCFF;
		}
		.auto-style6 {
			width: 188px;
			height: 32px;
		}
		.auto-style7 {
            height: 32px;
            text-align: left;
            width: 576px;
        }
		.auto-style8 {
            text-align: left;
            width: 576px;
        }
		.auto-style9 {
			width: 120px;
			height: 32px;
		}
		.auto-style10 {
			height: 32px;
            width: 576px;
        }
	    .auto-style11 {
            color: #FF0000;
        }
        .auto-style12 {
            width: 576px;
        }
	</style>
</head>
<body style="height: 160px; width: 747px">
    <form id="form1" runat="server" autocomplete="off">
		<table class="auto-style1">
			<tr>
				<td class="auto-style4">Player Name</td>
				<td class="auto-style3">
					<asp:TextBox ID="TextBoxUN" runat="server" Width="157px"></asp:TextBox>
				</td>
				<td class="auto-style8">
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxUN" ErrorMessage="Player Name is required" ForeColor="#FF3300"></asp:RequiredFieldValidator>
				    <br />
                    <asp:Label ID="Label1" runat="server" CssClass="auto-style11" Text="Label" Visible="False"></asp:Label>
				    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxUN" CssClass="auto-style11" ErrorMessage="Name must contain at least four characters." ValidationExpression="[a-z0-9_-]{4,40}"></asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td class="auto-style5">Email</td>
				<td class="auto-style6">
					<asp:TextBox ID="TextBoxEmail" runat="server" Width="157px"></asp:TextBox>
				</td>
				<td class="auto-style7">
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Email is required" ForeColor="#FF3300"></asp:RequiredFieldValidator>
					<br />
					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Please enter a valid email" ForeColor="#FF3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td class="auto-style4">Password</td>
				<td class="auto-style3">
					<asp:TextBox ID="TextBoxPass" runat="server" TextMode="Password" Width="157px"></asp:TextBox>
				</td>
				<td class="auto-style8">
					<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxPass" ErrorMessage="Password is required" ForeColor="#FF3300"></asp:RequiredFieldValidator>
				    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBoxPass" CssClass="auto-style11" ErrorMessage="Minimum 8 and maximum 12 characters at least: 1 uppercase alphabet, 1 lowercase alphabet, 1 number, and 1 special character." ValidationExpression="(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*#?&amp;])[A-Za-z\d$@$!%*#?&amp;]{8,12}"></asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td class="auto-style5">Confirm Password</td>
				<td class="auto-style6">
					<asp:TextBox ID="TextBoxRPass" runat="server" OnTextChanged="TextBox4_TextChanged" TextMode="Password" Width="157px"></asp:TextBox>
				</td>
				<td class="auto-style7">
					<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxRPass" ErrorMessage="Confrim Password is required" ForeColor="#FF3300"></asp:RequiredFieldValidator>
					<br />
					<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxPass" ControlToValidate="TextBoxRPass" ErrorMessage="Password does not match" ForeColor="#FF3300"></asp:CompareValidator>
				</td>
			</tr>
			<tr>
				<td class="auto-style2">&nbsp;</td>
				<td class="auto-style3">
					<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Register" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:Button ID="Reset" runat="server" OnClientClick="this.form.reset();return false" Text="Reset" CausesValidation="false"/>
                </td>
				<td class="auto-style12">&nbsp;</td>
			</tr>
			<tr>
				<td class="auto-style9"></td>
				<td class="auto-style6"></td>
				<td class="auto-style10"></td>
			</tr>
			<tr>
				<td class="auto-style2">&nbsp;</td>
				<td class="auto-style3">&nbsp;</td>
				<td class="auto-style12">&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
