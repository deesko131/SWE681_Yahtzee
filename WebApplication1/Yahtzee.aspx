<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Yahtzee.aspx.cs" Inherits="Yahtzee.YahtzeeGame" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">
    <h2><%: Title %>Yahtzee with Pals</h2>
        <p>Score: </p>
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
                    <asp:Button ID="btnRoll" runat="server" Text="Roll" Enabled="False" OnClick="btnRoll_Click" />
                </p>
    <div align="center">
    <table class="nav-justified" border="1" style="width:auto">
        <tr>
            <td>Ones</td>
            <td style="text-align:center">
                    <asp:Label ID="lblOnesScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoOnes" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Twos</td>
            <td style="text-align:center">
                    <asp:Label ID="lblTwosScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoTwos" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Threes</td>
            <td style="text-align:center">
                    <asp:Label ID="lblThreesScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoThrees" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Fours</td>
            <td style="text-align:center">
                    <asp:Label ID="lblFoursScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoFours" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Fives</td>
            <td style="text-align:center">
                    <asp:Label ID="lblFivesScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoFives" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Sixes</td>
            <td style="text-align:center">
                    <asp:Label ID="lblSixesScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoSixes" runat="server" />
            </td>
        </tr>
        <tr>
            <td>3 of a Kind</td>
            <td style="text-align:center">
                    <asp:Label ID="lblThreeOfAKindScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoThreeOfAKind" runat="server" />
            </td>
        </tr>
        <tr>
            <td>4 of a Kind</td>
            <td style="text-align:center">
                    <asp:Label ID="lblFourOfAKindScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoFourOfAKind" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Full House</td>
            <td style="text-align:center">
                    <asp:Label ID="lblFullHouseScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoFullHouse" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Small Straight</td>
            <td style="text-align:center">
                    <asp:Label ID="lblSmallStraightScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoSmallStraight" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Large Straight</td>
            <td style="text-align:center">
                    <asp:Label ID="lblLargeStraightScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoLargeStraight" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Yahtzee!</td>
            <td style="text-align:center">
                    <asp:Label ID="lblYahtzeeScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoYahtzee" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Chance</td>
            <td style="text-align:center">
                    <asp:Label ID="lblChanceScore" runat="server" Text="0" ></asp:Label>
                </td>
            <td style="text-align:center">
                <asp:RadioButton ID="rdoChance" runat="server" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
