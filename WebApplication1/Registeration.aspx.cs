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
	public partial class Registeration : System.Web.UI.Page
	{ string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\players.mdf;Integrated Security=True";

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
				//to-do --> Prepared statements 
				//conn.Open();
				//string insertQuery = "insert into Registeration (PlayerName,Email,Password) values(@PName, @email, @password)";
				//SqlCommand com = new SqlCommand(insertQuery, conn);
				//string connectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\players.mdf;Integrated Security=True";
				using (SqlConnection sqlCon = new SqlConnection(connectionString))
				{
					sqlCon.Open();
					SqlCommand sqlCmd = new SqlCommand("UesrAdd", sqlCon);
					sqlCmd.CommandType =System.Data.CommandType.StoredProcedure;
					sqlCmd.Parameters.AddWithValue("@PlayerName", TextBoxUN.Text.Trim());
					sqlCmd.Parameters.AddWithValue("@Email", TextBoxEmail.Text.Trim());
					sqlCmd.Parameters.AddWithValue("@Password", TextBoxPass.Text.Trim());
					sqlCmd.ExecuteNonQuery();
					Response.Write("Registeration is successful");
					Response.Redirect("Default.aspx");
					//conn.Close();
					Clear();
				}
			}
			catch (Exception ex)
			{ Response.Write("Error:" + ex.ToString()); }
			
		}
		void Clear()
		{
			TextBoxUN.Text = TextBoxEmail.Text = TextBoxPass.Text = TextBoxRPass.Text = "";
		}
	}
}