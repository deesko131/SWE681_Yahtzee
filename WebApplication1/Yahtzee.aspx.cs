using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

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

            if(rdoOnes.Checked || rdoTwos.Checked)
            {
                isValid = true;
            }
            return isValid;
        }

        //For valid selected categories, this sets the text of the score label control for the category
        private void displayCategoryScore(int[] dice, int category)
        {
            int score = 0;

            switch (category)
            {
                case 0: //ones
                    for(int i=0; i<5; i++)
                    {
                        if(dice[i] == 1)
                            score += dice[i];
                    }
                    lblOnesScore.Text = score.ToString();
                    break;
                
            }
            selectedPointScore = score;
        }

        private void calculateTotalScore(int[] playerScores)
        {

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

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            Points[playerIndex, selectedPointCategory] = selectedPointScore;
            //Cache("Points")
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register/logout.aspx");
        }

        protected void Welcome_TextChanged(object sender, EventArgs e)
        {

        }
    }
}