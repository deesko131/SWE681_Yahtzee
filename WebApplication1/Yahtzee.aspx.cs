using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;

namespace Yahtzee
{
    public partial class YahtzeeGame : Page
    {

        private String[] PlayerNames = new string[2];

        //These arrays hold the scores of the two players. 
        //{ones, twos, threes, fours, fives, sixes, 3 of a kind, 4 of a kind, full house, sm. straight, lg. straight, yahtzee, chance, bonus}
        private int[,] Points = new int[2,14];
        //private int[] PlayerOneScores = new int[14];
        //private int[] PlayerTwoScores = new int[14];
        private int selectedPointScore = 0;
        private int selectedPointCategory = 0;
        private int rollsRemaining = 3; //The number of rolls left in the user's turn
        private int playerIndex; //the index of the player whose turn it is
        Stack<int> selectedDie = new Stack<int>();
        
        Random randomNumber = new Random();
        private int[] dice = new int[5];

        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Welcome.Text = "Hello " + User.Identity.Name;
            }

            if(Request.QueryString["game"] == "New")
            {
                PlayerNames[0] = User.Identity.Name;
                lblPlayerOneName.Text = PlayerNames[0];
                //TO DO: log the creation of the game
            }



            ////If it is the logged in player's turn, make the roll button control enabled.
            //if (PlayerNames[playerIndex] == User.Identity.Name)
            //{
            //    btnRoll.Enabled = true;
            //}
            if (ViewState["rollsRemaining"] != null)
            {
                rollsRemaining = Convert.ToInt16(ViewState["rollsRemaining"]);
            }

            if(ViewState["dice"] != null)
            {
                dice =   (int[])ViewState["dice"];
            }
            
            
            btnRoll.Enabled = true;
        }

        private void setupGame()
        {
            //PlayerNames[0] = User.Identity.Name;
            PlayerNames[0] = "Sarah";
        }

        private void playerJoin()
        {
            //PlayerNames[1] = User.Identity.Name;
            PlayerNames[0] = "George";
        }

        private void Roll()
        {
            //first turn. Roll all 5 dice.
            if (rollsRemaining == 3)
            {
                for (int i = 0; i < 5; i++)
                {
                    int dieRoll = randomNumber.Next(1, 6);
                    dice[i] = dieRoll;
                }
                rollsRemaining--;
                ViewState["dice"] = dice;
            }
            //2nd or third turn, roll only unselected dice
            else if (rollsRemaining > 0)
            {
                //Push the index value of the checked die into the selected die stack
                if (chkHold1.Checked == true)
                {
                    selectedDie.Push(0);
                }
                if (chkHold2.Checked == true)
                {
                    selectedDie.Push(1);
                }
                if (chkHold3.Checked == true)
                {
                    selectedDie.Push(2);
                }
                if (chkHold4.Checked == true)
                {
                    selectedDie.Push(3);
                }
                if (chkHold5.Checked == true)
                {
                    selectedDie.Push(4);
                }

                for (int j = 0; j < 5; j++)
                {
                    if (!selectedDie.Contains(j))
                    {
                        int dieRoll = randomNumber.Next(1, 6);
                        dice[j] = dieRoll;
                    }
                }
                rollsRemaining--;
                ViewState["dice"] = dice;
            }
            renderDice();

            ViewState["rollsRemaining"] = rollsRemaining;

            if (rollsRemaining == 0)
            {
                btnRoll.Enabled = false;
                btnRoll.Visible = false;
            }
        }

       private void renderDice()
        {
            lblDie1.Text = dice[0].ToString();
            lblDie2.Text = dice[1].ToString();
            lblDie3.Text = dice[2].ToString();
            lblDie4.Text = dice[3].ToString();
            lblDie5.Text = dice[4].ToString();
        }

        protected void BtnRoll_Click(object sender, EventArgs e)
        {
            Roll();
        }

        //Checks if the set of 5 dice satisfies the requirements to be played in the category
        private bool isCategoryValid(int[] dice)
        {
            bool isValid = false;

            if(rdoOnes.Checked || rdoTwos.Checked || rdoThrees.Checked || rdoFours.Checked || rdoFives.Checked || rdoSixes.Checked || rdoChance.Checked)
            {
                isValid = true;
            }
            else
            {
                //Arrays to count the number of each 1 through 6
                ArrayList ones = new ArrayList();
                ArrayList twos = new ArrayList();
                ArrayList threes = new ArrayList();
                ArrayList fours = new ArrayList();
                ArrayList fives = new ArrayList();
                ArrayList sixes = new ArrayList();

                for (int i = 0; i < 5; i++)
                {
                    if(dice[i] == 1)
                    {
                        ones.Add(1);
                    }
                    else if (dice[i] == 2)
                    {
                        twos.Add(1);
                    }
                    else if (dice[i] == 3)
                    {
                        threes.Add(1);
                    }
                    else if (dice[i] == 4)
                    {
                        fours.Add(1);
                    }
                    else if (dice[i] == 5)
                    {
                        fives.Add(1);
                    }
                    else if (dice[i] == 6)
                    {
                        sixes.Add(1);
                    }
                }
                if (rdoThreeOfAKind.Checked)
                {
                    if (ones.Capacity >= 3 || twos.Capacity >= 3 || threes.Capacity >= 3 || fours.Capacity >= 3 || fives.Capacity >= 3 || sixes.Capacity >= 3)
                    {
                        isValid = true;
                    }
                }
                else if (rdoFourOfAKind.Checked)
                {
                    if (ones.Capacity >= 4 || twos.Capacity >= 4 || threes.Capacity >= 4 || fours.Capacity >= 4 || fives.Capacity >= 4 || sixes.Capacity >= 4)
                    {
                        isValid = true;
                    }
                }
                else if (rdoYahtzee.Checked)
                {
                    if (ones.Capacity >= 5 || twos.Capacity >= 5 || threes.Capacity >= 5 || fours.Capacity >= 5 || fives.Capacity >= 5 || sixes.Capacity >= 5)
                    {
                        isValid = true;
                    }
                }
                else if (rdoFullHouse.Checked)
                {
                    if (ones.Capacity >= 3 || twos.Capacity >= 3 || threes.Capacity >= 3 || fours.Capacity >= 3 || fives.Capacity >= 3 || sixes.Capacity >= 3)
                    {
                        if (ones.Capacity >= 2 || twos.Capacity >= 2 || threes.Capacity >= 2 || fours.Capacity >= 2 || fives.Capacity >= 2 || sixes.Capacity >= 2)
                        {
                            isValid = true;
                        }
                    }
                }
                else if (rdoLargeStraight.Checked)
                {
                    if(ones.Capacity == 1 && twos.Capacity == 1 && threes.Capacity == 1 && fours.Capacity == 1 && fives.Capacity == 1)
                    {
                        isValid = true;
                    }
                    else if (twos.Capacity == 1 && threes.Capacity == 1 && fours.Capacity == 1 && fives.Capacity == 1 && sixes.Capacity == 1)
                    {
                        isValid = true;
                    }
                }
                else if (rdoSmallStraight.Checked)
                {
                    if (ones.Capacity >= 1 && twos.Capacity >= 1 && threes.Capacity >= 1 && fours.Capacity >= 1)
                    {
                        isValid = true;
                    }
                    else if (twos.Capacity >= 1 && threes.Capacity >= 1 && fours.Capacity >= 1 && fives.Capacity >= 1)
                    {
                        isValid = true;
                    }
                    else if (threes.Capacity >= 1 && fours.Capacity >= 1 && fives.Capacity >= 1 && sixes.Capacity >= 1)
                    {
                        isValid = true;
                    }
                }

            }
            
            return isValid;
        }

        //For valid selected categories, this sets the text of the score label control for the category
        private void displayCategoryScore(int[] dice, int category)
        {
            int score = 0;

            zeroAllPoints();

            switch (category)
            {
                case 0: //ones
                    for(int i=0; i<5; i++)
                    {
                        if(dice[i] == 1)
                            score += 1;
                    }
                    lblOnesScore.Text = score.ToString();
                    break;
                case 1: //twos
                    for (int i = 0; i < 5; i++)
                    {
                        if (dice[i] == 2)
                            score += 2;
                    }
                    lblTwosScore.Text = score.ToString();
                    break;
                case 2: //threes
                    for (int i = 0; i < 5; i++)
                    {
                        if (dice[i] == 3)
                            score += 3;
                    }
                    lblThreesScore.Text = score.ToString();
                    break;
                case 3: //fours
                    for (int i = 0; i < 5; i++)
                    {
                        if (dice[i] == 4)
                            score += 4;
                    }
                    lblFoursScore.Text = score.ToString();
                    break;
                case 4: //fives
                    for (int i = 0; i < 5; i++)
                    {
                        if (dice[i] == 5)
                            score += 5;
                    }
                    lblFivesScore.Text = score.ToString();
                    break;
                case 5: //sixes
                    for (int i = 0; i < 5; i++)
                    {
                        if (dice[i] == 6)
                            score += 6;
                    }
                    lblSixesScore.Text = score.ToString();
                    break;
                case 6: //Three of a kind
                    for (int i = 0; i < 5; i++)
                    {
                            score += dice[i];
                    }
                    lblThreeOfAKindScore.Text = score.ToString();
                    break;
                case 7: //Four of a kind
                    for (int i = 0; i < 5; i++)
                    {
                            score += dice[i];
                    }
                    lblFourOfAKindScore.Text = score.ToString();
                    break;
                case 8: //Full house
                    score = 25;
                    lblFullHouseScore.Text = score.ToString();
                    break;
                case 9: //Small straight
                    score = 30;
                    lblSmallStraightScore.Text = score.ToString();
                    break;
                case 10: //Large straight
                    score = 40;
                    lblSmallStraightScore.Text = score.ToString();
                    break;
                case 11: //Yahtzee
                    score = 50;
                    lblYahtzeeScore.Text = score.ToString();
                    break;
                case 12: //Chance
                    for (int i = 0; i < 5; i++)
                    {
                            score += dice[i];
                    }
                    lblChanceScore.Text = score.ToString();
                    break;

            }
            selectedPointScore = score;
        }

        private void calculateTotalScore(int[] playerScores)
        {

        }

       

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            Points[playerIndex, selectedPointCategory] = selectedPointScore;
            //Cache("Points")

            //TO DO: Log the play
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register/logout.aspx");
        }

        protected void rdoOnes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoOnes.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 0);
                selectedPointCategory = 0;
            }
            else if (!rdoOnes.Checked)
            {
                lblOnesScore.Text = "0";
            }
        }

        protected void rdoTwos_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTwos.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 1);
                selectedPointCategory = 1;
            }
            else if (!rdoTwos.Checked)
            {
                lblTwosScore.Text = "0";
            }
        }

        protected void rdoThrees_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoThrees.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 2);
                selectedPointCategory = 2;
            }
            else if (!rdoThrees.Checked)
            {
                lblThreesScore.Text = "0";
            }
        }

        protected void rdoFours_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoFours.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 3);
                selectedPointCategory = 3;
            }
            else if (!rdoFours.Checked)
            {
                lblFoursScore.Text = "0";
            }
        }

        protected void rdoFives_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoFives.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 4);
                selectedPointCategory = 4;
            }
            else if (!rdoFives.Checked)
            {
                lblFivesScore.Text = "0";
            }
        }

        protected void rdoSixes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSixes.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 5);
                selectedPointCategory = 5;
            }
            else if (!rdoSixes.Checked)
            {
                lblSixesScore.Text = "0";
            }
        }

        protected void rdoThreeOfAKind_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoThreeOfAKind.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 6);
                selectedPointCategory = 6;
            }
            else if (!rdoSixes.Checked)
            {
                lblThreeOfAKindScore.Text = "0";
            }
        }

        protected void rdoFourOfAKind_CheckedChanged1(object sender, EventArgs e)
        {
            if (rdoFourOfAKind.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 7);
                selectedPointCategory = 7;
            }
            else if (!rdoSixes.Checked)
            {
                lblFourOfAKindScore.Text = "0";
            }
        }

        protected void rdoFullHouse_CheckedChanged(object sender, EventArgs e)
        {
                if (rdoFullHouse.Checked && isCategoryValid(dice))
                {
                    displayCategoryScore(dice, 8);
                    selectedPointCategory = 8;
                }
                else if (!rdoFullHouse.Checked)
                {
                    lblFullHouseScore.Text = "0";
                }
        }

        protected void rdoSmallStraight_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSmallStraight.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 9);
                selectedPointCategory = 9;
            }
            else if (!rdoSmallStraight.Checked)
            {
                lblSmallStraightScore.Text = "0";
            }
        }

        protected void rdoLargeStraight_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLargeStraight.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 10);
                selectedPointCategory = 10;
            }
            else if (!rdoLargeStraight.Checked)
            {
                lblLargeStraightScore.Text = "0";
            }
        }

        protected void rdoYahtzee_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoYahtzee.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 11);
                selectedPointCategory = 11;
            }
            else if (!rdoYahtzee.Checked)
            {
                lblYahtzeeScore.Text = "0";
            }
        }

        protected void rdoChance_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoChance.Checked && isCategoryValid(dice))
            {
                displayCategoryScore(dice, 12);
                selectedPointCategory = 12;
            }
            else if (!rdoChance.Checked)
            {
                lblChanceScore.Text = "0";
            }
        }

        private void zeroAllPoints()
        {
            lblOnesScore.Text = "0";
            lblTwosScore.Text = "0";
            lblThreesScore.Text = "0";
            lblFoursScore.Text = "0";
            lblFivesScore.Text = "0";
            lblSixesScore.Text = "0";
            lblFullHouseScore.Text = "0";
            lblSmallStraightScore.Text = "0";
            lblLargeStraightScore.Text = "0";
            lblYahtzeeScore.Text = "0";
            lblChanceScore.Text = "0";
        }

    }
}