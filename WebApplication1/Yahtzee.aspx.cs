using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using System.IO;
using log4net;

namespace Yahtzee
{
    public partial class YahtzeeGame : Page
    {
        private static readonly ILog log = LogManager.GetLogger("test");
        private String[] PlayerNames = new string[2];

        string playerOneName;
        string playerTwoName;
        string playerOneScore;
        string playerTwoScore;
        string winner;
        string playersTurn;


        private int selectedPointScore = 0;
        private int selectedPointCategory = 0;
        private int rollsRemaining = 3; //The number of rolls left in the user's turn
        Stack<int> selectedDie = new Stack<int>();
        private DataTable gameMoves = new DataTable();
        
        Random randomNumber = new Random();
        private int[] dice = new int[5];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
            }
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
                checkForActiveGame();

                if (Session["ActiveGameID"] == null)
                {
                    Session["ActiveGameID"] = createGameRecord();
                }
              
                Response.Redirect("~/Yahtzee.aspx");
            }
           
            if (!IsPostBack)
            {
                setupGame();
                
            }

            lblPlayerOneName.Text = ViewState["playerOneName"].ToString();
            lblPlayerTwoName.Text = ViewState["playerTwoName"].ToString();
            if (ViewState["playerOneScore"] != null)
            {
                lblPlayerOneScore.Text = ViewState["playerOneScore"].ToString();
            }
            if (ViewState["playerTwoScore"] != null)
            {
                lblPlayerTwoScore.Text = ViewState["playerTwoScore"].ToString();
            }
            if (ViewState["rollsRemaining"] != null)
            {
                rollsRemaining = Convert.ToInt16(ViewState["rollsRemaining"]);
            }

            if (ViewState["dice"] != null)
            {
                dice = (int[])ViewState["dice"];
            }
        }

        private void setupGame()
        {
            if (Session["ActiveGameID"] != null)
            {
                getGame();
                ViewState["playersTurn"] = playersTurn;
                ViewState["playerOneName"] = playerOneName;
                ViewState["playerTwoName"] = playerTwoName;
                ViewState["playerOneScore"] = playerOneScore;
                ViewState["playerTwoScore"] = playerTwoScore;
                
                getMoves();
                clearRadios();
                loadPoints();
                updateLowerUpperBonus();
                lblPlayerTwoName.Text = ViewState["playerTwoName"].ToString();
                if (lblPlayerTwoName.Text == "")
                {
                    lblMessage.Text = "Waiting for Player 2 to join.";
                }
                else
                {
                    lblMessage.Text = "It is " + ViewState["playersTurn"].ToString() + "'s turn";

                    //If it is the logged in player's turn, make the roll button control enabled.
                    if (ViewState["playersTurn"].ToString() == User.Identity.Name)
                    {
                        btnRoll.Enabled = true;
                        btnPlay.Enabled = true;
                    }
                    else
                    {
                        btnRoll.Enabled = false;
                        btnPlay.Enabled = false;
                    }
                }
            }
            else
            {
                Response.Redirect("~/MainMenu.aspx");
            }
        }

        private void Roll()
        {
            //first turn. Roll all 5 dice.
            if (rollsRemaining == 3)
            {
                for (int i = 0; i < 5; i++)
                {
                    int dieRoll = randomNumber.Next(1, 7);
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
                    lblLargeStraightScore.Text = score.ToString();
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
        }

        protected void btnPlay_Click(object sender, EventArgs e)
        {
            //Points[playerIndex, selectedPointCategory] = selectedPointScore;
            saveMove(getSelectedCategory(), selectedPointScore);
            
            setupGame();
            
            checkForWinner();
            lblPlayerOneName.Text = ViewState["playerOneName"].ToString();
            lblPlayerTwoName.Text = ViewState["playerTwoName"].ToString();
            if (ViewState["playerOneScore"] != null)
            {
                lblPlayerOneScore.Text = ViewState["playerOneScore"].ToString();
            }
            if (ViewState["playerTwoScore"] != null)
            {
                lblPlayerTwoScore.Text = ViewState["playerTwoScore"].ToString();
            }
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
            if(rdoOnes.Enabled == true)
            {
                lblOnesScore.Text = "0";
            }
            if (rdoTwos.Enabled == true)
            {
                lblTwosScore.Text = "0";
            }
            
            if(rdoThrees.Enabled == true)
            {
                lblThreesScore.Text = "0";
            }
            if (rdoFours.Enabled == true)
            {
                lblFoursScore.Text = "0";
            }
            if (rdoFives.Enabled == true)
            {
                lblFivesScore.Text = "0";
            }
            if (rdoSixes.Enabled == true)
            {
                lblSixesScore.Text = "0";
            }
            if (rdoFullHouse.Enabled == true)
            {
                lblFullHouseScore.Text = "0";
            }
            if (rdoSmallStraight.Enabled == true)
            {
                lblSmallStraightScore.Text = "0";
            }
            if (rdoLargeStraight.Enabled == true)
            {
                lblLargeStraightScore.Text = "0";
            }
            if (rdoYahtzee.Enabled == true)
            {
                lblYahtzeeScore.Text = "0";
            }
            if (rdoChance.Enabled == true)
            {
                lblChanceScore.Text = "0";
            }
        }

        private void updateLowerUpperBonus()
        {
            //Upper scores
            int ones = Convert.ToInt32(lblOnesScore.Text);
            int twos = Convert.ToInt32(lblTwosScore.Text);
            int threes = Convert.ToInt32(lblThreesScore.Text);
            int fours = Convert.ToInt32(lblFoursScore.Text);
            int fives = Convert.ToInt32(lblFivesScore.Text);
            int sixes = Convert.ToInt32(lblSixesScore.Text);
            

            //Lower scores
            int threeOfAKind = Convert.ToInt32(lblThreeOfAKindScore.Text);
            int fourOfAKind = Convert.ToInt32(lblFourOfAKindScore.Text);
            int fullHouse = Convert.ToInt32(lblFullHouseScore.Text);
            int smallStraight = Convert.ToInt32(lblSmallStraightScore.Text);
            int largeStraight = Convert.ToInt32(lblLargeStraightScore.Text);
            int yahtzee = Convert.ToInt32(lblYahtzeeScore.Text);
            int chance = Convert.ToInt32(lblChanceScore.Text);

            

            int[] upper = { ones, twos, threes, fours, fives, sixes };
            int[] lower = { threeOfAKind, fourOfAKind, fullHouse, smallStraight, largeStraight, yahtzee, chance };

            if (upper.Sum() > 62)
            {
                lblBonus.Text = upper.Sum().ToString();
            }

            int bonus = Convert.ToInt32(lblBonus.Text);

            lblUpperTotal.Text = (upper.Sum() + bonus).ToString();
            lblLowerTotal.Text = lower.Sum().ToString();

            int totalScore = Convert.ToInt32(lblUpperTotal.Text) + Convert.ToInt32(lblLowerTotal.Text);
        }

        private void getGame()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                string command = "select * from Games where GameID = @GameID";

                SqlCommand com = new SqlCommand(command, conn);
                com.Prepare();
                com.Parameters.Add(new SqlParameter("@GameID", Session["ActiveGameID"]));
                SqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        playerOneName = reader["PlayerOneName"].ToString();
                        playerTwoName = reader["PlayerTwoName"].ToString();
                        playerOneScore = reader["PlayerOneScore"].ToString();
                        playerTwoScore = reader["PlayerTwoScore"].ToString();
                        winner = reader["Winner"].ToString();
                        playersTurn = reader["PlayersTurn"].ToString();
                    }
                }
                conn.Close();
            }
            catch (SqlException ex)
            {
                log.InfoFormat("SQL ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }
            catch(Exception ex)
            {
                log.InfoFormat("APP ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }
        }

        private int createGameRecord()
        {
            int gameId = 0;
            try
            {
                
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                string command = "insert into Games (PlayerOneName,PlayerTwoName, PlayerOneScore,PlayerTwoScore, Winner, PlayersTurn, CreatedBy, UpdatedBy) values(@PlayerOneName, @PlayerTwoName, @PlayerOneScore,@PlayerTwoScore, @Winner, @PlayersTurn, @PlayerOneName,@PlayerOneName); SELECT SCOPE_IDENTITY()";

                SqlCommand com = new SqlCommand(command, conn);
                com.Prepare();
                com.Parameters.Add(new SqlParameter("@PlayerOneName", User.Identity.Name));
                com.Parameters.Add(new SqlParameter("@PlayerTwoName", DBNull.Value));
                com.Parameters.Add(new SqlParameter("@PlayerOneScore", "0"));
                com.Parameters.Add(new SqlParameter("@PlayerTwoScore", "0"));
                com.Parameters.Add(new SqlParameter("@Winner", DBNull.Value));
                com.Parameters.Add(new SqlParameter("@PlayersTurn", User.Identity.Name));
                gameId = Convert.ToInt32(com.ExecuteScalar().ToString());
                conn.Close();
            }
            catch (SqlException ex)
            {
                log.InfoFormat("SQL ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }
            catch (Exception ex)
            {
                log.InfoFormat("APP ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }

            return gameId;
        }

        private void checkForActiveGame()
        {
            if(Session["ActiveGameID"] != null)
            {
                string message = "You already have an active game. Would you like to resume play?";
                string caption = "Active Game Found";
                var result = MessageBox.Show(message, caption,
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Response.Redirect("~/Yahtzee.aspx");
                }
                else if (result == DialogResult.No)
                {
                    Response.Redirect("~/MainMenu.aspx");
                }
            }
        }

        //Commits the user's move to the Moves database table
        private void saveMove(string category, int points)
        {
            try
            {

                int gameId = Convert.ToInt32(Session["ActiveGameID"]);

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                string command = "insert into Moves (GameID, PlayerName, Category, Points) values(@GameID, @PlayerName, @Category, @Points)";

                SqlCommand com = new SqlCommand(command, conn);
                com.Prepare();
                com.Parameters.Add(new SqlParameter("@GameID", gameId));
                com.Parameters.Add(new SqlParameter("@PlayerName", User.Identity.Name));
                com.Parameters.Add(new SqlParameter("@Category", category));
                com.Parameters.Add(new SqlParameter("@Points", points));

                com.ExecuteNonQuery();
                conn.Close();

                updateGamesRecord(gameId, points);

            }
            catch (SqlException ex)
            {
                log.InfoFormat("SQL ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }
            catch (Exception ex)
            {
                log.InfoFormat("APP ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }

        }

        //Updates the game record with latest total scores and sets the next player's turn
        private void updateGamesRecord(int gameId, int points)
        {
            try
            {
                string nextPlayer;
                string updatedBy;
                int oneScore = Convert.ToInt32(lblPlayerOneScore.Text);
                int twoScore = Convert.ToInt32(lblPlayerTwoScore.Text);
                if (ViewState["playersTurn"].ToString() == lblPlayerOneName.Text)
                {
                    oneScore = oneScore + points;
                    updatedBy = lblPlayerOneName.Text;
                    nextPlayer = lblPlayerTwoName.Text;
                }
                else
                {
                    twoScore = twoScore + points;
                    updatedBy = lblPlayerTwoName.Text;
                    nextPlayer = lblPlayerOneName.Text;
                }

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                string command = "update Games set UpdatedDate = GETDATE(), UpdatedBy = @UpdatedBy, PlayerOneScore = @PlayerOneScore, PlayerTwoScore = @PlayerTwoScore, PlayersTurn = @NextPlayer where GameID = @GameID";

                SqlCommand com = new SqlCommand(command, conn);
                com.Prepare();
                com.Parameters.Add(new SqlParameter("@PlayerOneScore", oneScore.ToString()));
                com.Parameters.Add(new SqlParameter("@PlayerTwoScore", twoScore.ToString()));
                com.Parameters.Add(new SqlParameter("@UpdatedBy", updatedBy));
                com.Parameters.Add(new SqlParameter("@NextPlayer", nextPlayer));
                com.Parameters.Add(new SqlParameter("@GameID", gameId));
                
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                log.InfoFormat("SQL ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }
            catch (Exception ex)
            {
                log.InfoFormat("APP ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }
        }

        private void clearRadios()
        {
            rdoChance.Checked = false;
            rdoOnes.Checked = false;
            rdoTwos.Checked = false;
            rdoThrees.Checked = false;
            rdoFours.Checked = false;
            rdoFives.Checked = false;
            rdoSixes.Checked = false;
            rdoFullHouse.Checked = false;
            rdoSmallStraight.Checked = false;
            rdoLargeStraight.Checked = false;
            rdoYahtzee.Checked = false;
        }

        private void getMoves()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                string command = "select * from Moves where GameID = @GameID";

                SqlCommand com = new SqlCommand(command, conn);
                com.Prepare();
                com.Parameters.Add(new SqlParameter("@GameID", Session["ActiveGameID"]));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = com;
                da.Fill(gameMoves);
                conn.Close();
                da.Dispose();
            }
            catch (SqlException ex)
            {
                log.InfoFormat("SQL ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }
            catch (Exception ex)
            {
                log.InfoFormat("APP ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }

        }

        //Checks if all 26 moves have been played, calculates the winner and saves the winner's name to the Games table.
        private void checkForWinner()
        {
            string winner;
            
            if (gameMoves.Rows.Count == 26)
            {
                if (Convert.ToInt32(playerOneScore) > Convert.ToInt32(playerTwoScore))
                {
                    winner = playerOneName;
                }
                else if (Convert.ToInt32(playerOneScore) == Convert.ToInt32(playerTwoScore))
                {
                    winner = "Draw";
                }
                else
                {
                    winner = playerTwoName;
                }

                updateWinner(winner, Convert.ToInt32(Session["ActiveGameID"]));

                MessageBox.Show(winner + " won the game.");
                Session["ActiveGameID"] = null;
            }
        }

        private void updateWinner(string winner, int gameId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                string command = "update Games set PlayersTurn = @PlayersTurn, Winner = @Winner where GameID = @GameID";

                SqlCommand com = new SqlCommand(command, conn);
                com.Prepare();
                com.Parameters.Add(new SqlParameter("@GameID", gameId));
                com.Parameters.Add(new SqlParameter("@PlayersTurn", DBNull.Value));
                com.Parameters.Add(new SqlParameter("@Winner", winner));

                com.ExecuteNonQuery();
                conn.Close();

            }
            catch (SqlException ex)
            {
                log.InfoFormat("SQL ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }
            catch (Exception ex)
            {
                log.InfoFormat("APP ERROR: User- {0} Error- {1}", User.Identity.Name, ex.ToString());
            }
        }

        private void loadPoints()
        {
            string playersTurn = ViewState["playersTurn"].ToString();
            foreach (DataRow row in gameMoves.Rows)
            {
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Chance")
                {
                    rdoChance.Enabled = false;
                    lblChanceScore.Text = row["Points"].ToString();
                }
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Ones")
                {
                    rdoOnes.Enabled = false;
                    lblOnesScore.Text = row["Points"].ToString();
                }
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Twos")
                {
                    rdoTwos.Enabled = false;
                    lblTwosScore.Text = row["Points"].ToString();
                }
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Threes")
                {
                    rdoThrees.Enabled = false;
                    lblThreesScore.Text = row["Points"].ToString();
                }
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Fours")
                {
                    rdoFours.Enabled = false;
                    lblFoursScore.Text = row["Points"].ToString();
                }
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Fives")
                {
                    rdoFives.Enabled = false;
                    lblFivesScore.Text = row["Points"].ToString();
                }
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Sixes")
                {
                    rdoSixes.Enabled = false;
                    lblSixesScore.Text = row["Points"].ToString();
                }
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Full House")
                {
                    rdoFullHouse.Enabled = false;
                    lblFullHouseScore.Text = row["Points"].ToString();
                }
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Small Straight")
                {
                    rdoSmallStraight.Enabled = false;
                    lblSmallStraightScore.Text = row["Points"].ToString();
                }
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Large Straight")
                {
                    rdoLargeStraight.Enabled = false;
                    lblLargeStraightScore.Text = row["Points"].ToString();
                }
                if (row["PlayerName"].ToString() == playersTurn && row["category"].ToString() == "Yahtzee")
                {
                    rdoYahtzee.Enabled = false;
                    lblYahtzeeScore.Text = row["Points"].ToString();
                }
            }
        }

        private string getSelectedCategory()
        {
            string category = "";

            if (rdoChance.Checked)
            {
                category = "Chance";
                selectedPointScore = Convert.ToInt32(lblChanceScore.Text);
            }
            else if (rdoOnes.Checked)
            {
                category = "Ones";
                selectedPointScore = Convert.ToInt32(lblOnesScore.Text);
            }
            else if (rdoTwos.Checked)
            {
                category = "Twos";
                selectedPointScore = Convert.ToInt32(lblTwosScore.Text);
            }
            else if (rdoThrees.Checked)
            {
                category = "Threes";
                selectedPointScore = Convert.ToInt32(lblThreesScore.Text);
            }
            else if (rdoFours.Checked)
            {
                category = "Fours";
                selectedPointScore = Convert.ToInt32(lblFoursScore.Text);
            }
            else if (rdoFives.Checked)
            {
                category = "Fives";
                selectedPointScore = Convert.ToInt32(lblFivesScore.Text);
            }
            else if (rdoSixes.Checked)
            {
                category = "Sixes";
                selectedPointScore = Convert.ToInt32(lblSixesScore.Text);
            }
            else if (rdoFullHouse.Checked)
            {
                category = "Full House";
                selectedPointScore = Convert.ToInt32(lblFullHouseScore.Text);
            }
            else if (rdoSmallStraight.Checked)
            {
                category = "Small Straight";
                selectedPointScore = Convert.ToInt32(lblSmallStraightScore.Text);
            }
            else if (rdoLargeStraight.Checked)
            {
                category = "Large Straight";
                selectedPointScore = Convert.ToInt32(lblLargeStraightScore.Text);
            }
            else if (rdoYahtzee.Checked)
            {
                category = "Yahtzee";
                selectedPointScore = Convert.ToInt32(lblYahtzeeScore.Text);
            }

            return category;
        }
    }
}