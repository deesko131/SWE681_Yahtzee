<%@ Page Title="Yahtzee with Pals" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Yahtzee.aspx.cs" Inherits="Yahtzee.YahtzeeGame" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">
    <table class="nav-justified">
        <tr>
            <td class="text-left" style="width: 426px">
                <asp:Label ID="Welcome" runat="server" style="font-family: Arial; font-weight: normal; font-size: medium" Text="Welcome"></asp:Label>
            </td>
            <td class="text-right">
                <asp:HyperLink ID="Logout" runat="server" NavigateUrl="~/Register/logout.aspx" style="font-family: Arial; font-weight: normal; font-size: medium">Logout</asp:HyperLink>
            </td>
        </tr>
    </table>
    <h2 class="text-left">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </h2>
        <h2>Yahtzee with Pals</h2>
        <p>Score: </p>
    <h2><%: Title %></h2>
        <h3>Your Score: <asp:Label ID="lblPlayerOneScore" runat="server" Text="X" ></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;Their Score: <asp:Label ID="lblPlayerTwoScore" runat="server" Text="X" ></asp:Label></h3>
    <h3>You Rolled:</h3>

        <table id="tblDiceSet" border="1" class="nav-justified" style="text-align:center; width:auto">
            <tr>
                <td>
                    <asp:Label ID="lblDie1" runat="server" Text="X" ></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDie2" runat="server" Text="X"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDie3" runat="server" Text="X"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDie4" runat="server" Text="X"></asp:Label>
                </td>
                <td>
                     <asp:Label ID="lblDie5" runat="server" Text="X"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 43px">
                    <asp:CheckBox ID="chkHold1" runat="server" Text="Hold" />
                </td>
                <td style="height: 43px">
                    <asp:CheckBox ID="chkHold2" runat="server" Text="Hold" />
                </td>
                <td style="height: 43px">
                    <asp:CheckBox ID="chkHold3" runat="server" Text="Hold" />
                </td>
                <td style="height: 43px">
                    <asp:CheckBox ID="chkHold4" runat="server" Text="Hold" />
                </td>
                <td style="height: 43px">
                    <asp:CheckBox ID="chkHold5" runat="server" Text="Hold" />
                </td>
            </tr>
        </table>
        </div>
    <p style="text-align:center; vertical-align:bottom">
                    <asp:Button ID="btnRoll" runat="server" Text="Roll" Enabled="False" OnClick="BtnRoll_Click" />
                </p>
    <div align="center">
    <table class="nav-justified" border="1" style="width:auto">
        <tr>
            <td>Ones</td>
            <td style="text-align:center">
                    <asp:Label ID="lblOnesScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoOnes" runat="server" OnCheckedChanged="rdoOnes_CheckedChanged" AutoPostBack="True" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Twos</td>
            <td style="text-align:center">
                    <asp:Label ID="lblTwosScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoTwos" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Threes</td>
            <td style="text-align:center">
                    <asp:Label ID="lblThreesScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoThrees" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Fours</td>
            <td style="text-align:center">
                    <asp:Label ID="lblFoursScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoFours" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Fives</td>
            <td style="text-align:center">
                    <asp:Label ID="lblFivesScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoFives" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Sixes</td>
            <td style="text-align:center">
                    <asp:Label ID="lblSixesScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoSixes" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Upper Total</td>
            <td style="text-align:center">
                    &nbsp;</td>
            <td style="text-align:center">
                    <asp:Label ID="lblUpperTotal" runat="server" Text="0" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Bonus</td>
            <td style="text-align:center">
                    &nbsp;</td>
            <td style="text-align:center">
                    <asp:Label ID="lblBonus" runat="server" Text="0" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>3 of a Kind</td>
            <td style="text-align:center">
                    <asp:Label ID="lblThreeOfAKindScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoThreeOfAKind" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>4 of a Kind</td>
            <td style="text-align:center">
                    <asp:Label ID="lblFourOfAKindScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoFourOfAKind" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Full House</td>
            <td style="text-align:center">
                    <asp:Label ID="lblFullHouseScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoFullHouse" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Small Straight</td>
            <td style="text-align:center">
                    <asp:Label ID="lblSmallStraightScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoSmallStraight" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Large Straight</td>
            <td style="text-align:center">
                    <asp:Label ID="lblLargeStraightScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoLargeStraight" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Yahtzee!</td>
            <td style="text-align:center">
                    <asp:Label ID="lblYahtzeeScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoYahtzee" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Chance</td>
            <td style="text-align:center">
                    <asp:Label ID="lblChanceScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoChance" runat="server" GroupName="category" />
            </td>
        </tr>
        <tr>
            <td>Lower Total</td>
            <td style="text-align:center">
                    &nbsp;</td>
            <td style="text-align:center">
                    <asp:Label ID="lblLowerTotal" runat="server" Text="0" ></asp:Label>
            </td>
        </tr>
    </table>
                    <asp:Button ID="btnPlay" runat="server" Text="Play and End Turn" Enabled="False" OnClick="btnPlay_Click" />
    </div>
</asp:Content>
