using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Yahtzee
{
    public partial class _JoinGame : Page
    {
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
            if (Session["ActiveGameID"] != null || activeGameId != 0)
            {
                string message = "You already have an active game. Would you like to resume play?";
                string caption = "Active Game Found";
                var result = MessageBox.Show(message, caption,
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if(activeGameId != 0)
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

        private int getActiveGame()
        {
            int activeGameId;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            string command = "select ISNULL(GameID,0) from Games where (PlayerOneName = @PlayerName OR PlayerTwoName = @PlayerName) AND Winner IS NULL";

            SqlCommand com = new SqlCommand(command, conn);
            com.Prepare();
            com.Parameters.Add(new SqlParameter("@PlayerName", User.Identity.Name));
            activeGameId = Convert.ToInt32(com.ExecuteScalar());
            conn.Close();

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
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            string command = "update Games set UpdatedDate = GETDATE(), PlayerTwoName = @PlayerTwoName where GameID = @GameID";

            SqlCommand com = new SqlCommand(command, conn);
            com.Prepare();
            com.Parameters.Add(new SqlParameter("@PlayerTwoName", User.Identity.Name));
            com.Parameters.Add(new SqlParameter("@GameID", Convert.ToInt32(Session["ActiveGameID"])));

            com.ExecuteNonQuery();
            conn.Close();
        }
    }
}