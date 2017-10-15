using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
           
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //to-do --> Prepared statements 
            conn.Open();
            string checkuser = "select count(*) from Registeration where PlayerName= '" + TextBoxPN.Text + "'";
            SqlCommand com = new SqlCommand(checkuser, conn);
            Int32 temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if (temp == 1)
            {
                conn.Open();
                //To-do: security measures needed
                string checkPasswordQuery = "select Password from Registeration where PlayerName= '" + TextBoxPN.Text + "'";
                SqlCommand passcom = new SqlCommand(checkPasswordQuery, conn);
                string Password = passcom.ExecuteScalar().ToString().Replace(" ","");
                conn.Close();
                if (Password == TextBoxPassword.Text)
                { //to-do: replace default.aspx to game.aspx
                    Session["New"] = TextBoxPN.Text;
                    Response.Redirect("default.aspx");
                }
                else
                {
                    Response.Write("Failed Login");
                }
             
            }
           

        
        }

        protected void TextBoxPN_TextChanged(object sender, EventArgs e)
        {

        }
    }
}