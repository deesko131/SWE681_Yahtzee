using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;
//using WebApplication1.SaltedHash;
namespace WebApplication1
{
    public partial class Registeration : System.Web.UI.Page
    {
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\players.mdf;Integrated Security=True";
        /*private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HashingIterationCount = 10101;*/
        // SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {


            //if (IsPostBack)
            //{
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //to-do --> Prepared statements 
            //conn.Open();
            //string checkuser= "select count(*) from [Registeration] where PlayerName= '" + TextBoxUN.Text + "'";
            //SqlCommand com = new SqlCommand(checkuser, conn);
            //Int32 temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            //if (temp == 1)
            //{
            //Response.Write("User already exists");
            //}
            //conn.Close();
            //}
        }


        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                //conn.Open();
                //string insertQuery = "insert into Registeration (PlayerName,Email,Password) values(@PName, @email, @password)";
                //SqlCommand com = new SqlCommand(insertQuery, conn);
                //string connectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\players.mdf;Integrated Security=True";
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand(null, sqlCon);
                    //sqlCmd.CommandType = System.Data.CommandType.StoredProcedure

                    /*var saltBytes = new byte[16];
                    using (var provider = new RNGCryptoServiceProvider())
                        provider.GetNonZeroBytes(saltBytes);
                    string Salt = Convert.ToBase64String(saltBytes);
                    */
                    byte[] salt;
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                    string Salt = Convert.ToBase64String(salt);
                    sqlCmd.CommandText ="insert into Registeration (PlayerName,Email,Password, Salt)values(@PName, @email, @password,@salt)";
                    sqlCmd.Prepare();
                    sqlCmd.Parameters.AddWithValue("@PName", TextBoxUN.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@email", TextBoxEmail.Text.Trim());
                    //string ePass = EventHandlerTaskAsyncHelper.ComputeHash(TextBoxPass.Text, "SHA512", null);
                    //sqlCmd.Parameters.AddWithValue("@password", TextBoxPass.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@password", ComputeHash(Salt,TextBoxPass.Text.Trim()));
                    sqlCmd.Parameters.AddWithValue("@salt", Salt);
                    sqlCmd.ExecuteNonQuery();
                    Response.Write("Registeration is successful");
                    Response.Redirect("Default.aspx");
                    sqlCon.Close();
                    Clear();
                }
            }
            catch (Exception)
            { Response.Write("User exists"); }

        }
        void Clear()
        {
            TextBoxUN.Text = TextBoxEmail.Text = TextBoxPass.Text = TextBoxRPass.Text = " ";
        }
        static string ComputeHash(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var pdkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 1000);
            string slatedpass= Convert.ToBase64String(pdkdf2.GetBytes(16));
            return slatedpass;
          
                
        
        }
       

        /*internal static byte[] GeneralSalt(int saltByteSize=SaltByteSize)
        { using (RNGCryptoServiceProvider saltGenerator=new RNGCryptoServiceProvider())
            { byte[] salt = new byte[saltByteSize];
                saltGenerator.GetBytes(salt);
                return salt;

            }
        }
        internal static byte[] ComputeHash(string password, byte[] salt, int iterations=HashingIterationCount, int hashByteSize=HashByteSize)
        { using (Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, salt))
            { hashGenerator.IterationCount = iterations;
                return hashGenerator.GetBytes(HashByteSize);

            }
	}*/
    }

}