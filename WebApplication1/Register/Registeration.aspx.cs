using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;

namespace WebApplication1
{
    public partial class Registeration : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string checkuser = "select count(*) from Registeration where PlayerName=@PName";
                SqlCommand com = new SqlCommand(checkuser, sqlCon);
                com.Prepare();
                com.Parameters.Add(new SqlParameter("@PName", TextBoxUN.Text));
                Int32 temp = Convert.ToInt32(com.ExecuteScalar().ToString());
                sqlCon.Close();
                if (temp == 1)
                {
                    Label1.Visible = true;
                    Label1.Text = "User already exists";
                    Clear();

                }
                else
                {
                    try
                    {
                        byte[] salt;
                        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                        string Salt = Convert.ToBase64String(salt);
                        sqlCon.Open();
                        string newuser = "insert into Registeration (PlayerName,Email,Password, Salt)values(@PName, @email, @password,@salt)";
                        SqlCommand comm = new SqlCommand(newuser, sqlCon);
                        comm.Prepare();
                        comm.Parameters.AddWithValue("@PName", TextBoxUN.Text.Trim());
                        comm.Parameters.AddWithValue("@email", TextBoxEmail.Text.Trim());
                        comm.Parameters.AddWithValue("@password", ComputeHash(Salt, TextBoxPass.Text.Trim()));
                        comm.Parameters.AddWithValue("@salt", Salt);
                        comm.ExecuteNonQuery();
                        sqlCon.Close();
                        Response.Write("Registeration is successful");
                        Clear();
                        Response.Redirect("Default.aspx");
                    }
                    catch (Exception)
                    { Response.Write("Error"); }
                }


                
            }

        }

   
        void Clear()
        {
            TextBoxUN.Text = "";
            TextBoxEmail.Text = "";
            TextBoxPass.Text = "";
            TextBoxRPass.Text = "";
        }
        static string ComputeHash(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var pdkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 1000);
            string slatedpass= Convert.ToBase64String(pdkdf2.GetBytes(16));
            return slatedpass;
                  
        }

        protected void Reset_Click(object sender, EventArgs e)
        {

        }


        	
    }

}