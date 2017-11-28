using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.Security;

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

            //TODO: Get Active game ID and put into Session
            //Alert the user when an active game is found in the database and redirect them to it instead of showing game list.
        }

    }
}