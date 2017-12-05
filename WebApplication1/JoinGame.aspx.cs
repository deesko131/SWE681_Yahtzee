using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using log4net;

namespace Yahtzee
{
    public partial class _JoinGame : Page
    {
        private static readonly ILog log = LogManager.GetLogger("test");
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblUserName.Text = User.Identity.Name;
            }

            checkForActiveGame(); //Alert the user when an active game is found in the database and redirect them to it instead of showing game list.

        }

        private void checkForActiveGame()
        {
            int activeGameId = 0;
            activeGameId = getActiveGame();

            if(activeGameId == -1)
            {
                forfeitGame();
                string message = "This game was forfeited due to inactivity. See Move History to view the winner.";
                string caption = "Game Forfeited";
                var result = MessageBox.Show(message, caption,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    Response.Redirect("~/MainMenu.aspx");
                }
            }
            else
            {
                if (Session["ActiveGameID"] != null || activeGameId != 0)
                {
                    string message = "You already have an active game. Would you like to resume play?";
                    string caption = "Active Game Found";
                    var result = MessageBox.Show(message, caption,
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (activeGameId != 0)
                        {
                            Session["ActiveGameID"] = activeGameId;
                        }

                        Response.Redirect("~/Yahtzee.aspx");
                    }
                    else if (result == DialogResult.No)
                    {
                        Response.Redirect("~/MainMenu.aspx");
                    }
                }
            }

            
        }

        private void forfeitGame()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                string command = "update Games set Forfeit = 1, Winner = CASE WHEN UpdatedBy = PlayerOneName THEN PlayerOneName ELSE PlayerTwoName END where (PlayerOneName = @PlayerName OR PlayerTwoName = @PlayerName) AND Winner IS NULL";

                SqlCommand com = new SqlCommand(command, conn);
                com.Prepare();
                com.Parameters.Add(new SqlParameter("@PlayerName", User.Identity.Name));

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

        private int getActiveGame()
        {
            int activeGameId = 0;
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                string command = "select CASE WHEN DATEDIFF(minute,Games.UpdatedDate,GETDATE()) > 1 THEN -1 ELSE Games.GameID END from Games where (PlayerOneName = @PlayerName OR PlayerTwoName = @PlayerName) AND Winner IS NULL";

                SqlCommand com = new SqlCommand(command, conn);
                com.Prepare();
                com.Parameters.Add(new SqlParameter("@PlayerName", User.Identity.Name));
                activeGameId = Convert.ToInt32(com.ExecuteScalar());
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

            return activeGameId;
        }

        protected void gvGames_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var datakey = e.CommandArgument;
            if (e.CommandName == "Join")
            {
                Session["ActiveGameID"] = datakey;
                addPlayerTwoToGame();
                Response.Redirect("~/Yahtzee.aspx");
            }
            
        }

        private void addPlayerTwoToGame()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                string command = "update Games set UpdatedDate = GETDATE(), UpdatedBy = @PlayerTwoName, PlayerTwoName = @PlayerTwoName where GameID = @GameID";

                SqlCommand com = new SqlCommand(command, conn);
                com.Prepare();
                com.Parameters.Add(new SqlParameter("@PlayerTwoName", User.Identity.Name));
                com.Parameters.Add(new SqlParameter("@GameID", Convert.ToInt32(Session["ActiveGameID"])));

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
    }
}