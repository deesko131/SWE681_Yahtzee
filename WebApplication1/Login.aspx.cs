using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

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
            String PlayerName = this.TextBoxPN.Text.Trim();
            String Passwd = this.TextBoxPassword.Text.Trim();
            //string checkuser = "select count(*) from Registeration where PlayerName= '" + TextBoxPN.Text + "'";
            string checkuser = "select count(*) from Registeration where PlayerName= @PlayerName";

            SqlCommand com = new SqlCommand(checkuser, conn);
            com.Prepare();
            com.Parameters.Add(new SqlParameter("@PlayerName", TextBoxPN.Text));
            Int32 temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if (temp == 1)
            {
                try
                {
                    
                    conn.Open();

                    
                    string checkpass = "select Password, Salt from Registeration where PlayerName=@PlayerName";
                    SqlCommand command = new SqlCommand(checkpass, conn);
                    command.Prepare();
                    command.Parameters.Add(new SqlParameter("@PlayerName", PlayerName));

                    //string Salt = command.ExecuteScalar().ToString();
                    //command.Parameters.Add(new SqlParameter("@Password", ComputeHash(Salt,TextBoxPassword.Text)));
                    //command.ExecuteNonQuery();
                     
                    SqlDataReader dr = command.ExecuteReader();
                    dr.Read();
                    string Password = dr.GetString(0);
             
                    string Salt = dr.GetString(1);

                    // command.Parameters.Add(new SqlParameter("@Passwd1", ComputeHash(Salt,TextBoxPassword.Text)));
                    //SqlDataAdapter sda = new SqlDataAdapter(checkpass, conn);

                    //string passwd = command.ExecuteScalar().ToString().Replace(" ", "");
                    //command.Prepare();


                    string Hashedpass = ComputeHash(Salt, TextBoxPassword.Text);
                    conn.Close();
                    
                    if (Hashedpass == Password)
                        Response.Redirect("game.aspx");

                    else Response.Write("Login Failed:");

                }

                catch (Exception ex)

                {
                    Response.Write("Login Failed:" + ex.ToString());

                }

            }
          else
                Response.Write("Login Failed:");
           

        }

         static string ComputeHash(string salt, string password)
            {
                var saltBytes = Convert.FromBase64String(salt);
                using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                    return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(16));
            }
        //string checkPasswordQuery = "select Password from Registeration where PlayerName= '" + TextBoxPN.Text + "'";
        //SqlCommand passcom = new SqlCommand(checkPasswordQuery, conn);
        //string Password = passcom.ExecuteScalar().ToString().Replace(" ","");
        //conn.Close();
        //if (Password == TextBoxPassword.Text)
        //{ //to-do: replace default.aspx to game.aspx
        //  Session["New"] = TextBoxPN.Text;
        /* Response.Redirect("default.aspx");
     }
     else
     {
         Response.Write("Failed Login");
     }

 }*/

   
    protected void TextBoxPN_TextChanged(object sender, EventArgs e)
    {

    }
}   
}