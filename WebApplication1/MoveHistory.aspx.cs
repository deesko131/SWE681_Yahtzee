using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using log4net;

namespace WebApplication1
{
    public partial class MoveHistory : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        ILog log1 = LogManager.GetLogger("Loginfo");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            { BindGridView(); }
        }
        private void BindGridView()
        {
            try
            {
                conn.Open();
                string cmdstring = "select * from Games";
                SqlCommand command = new SqlCommand(cmdstring, conn);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView1.DataSource = dt.DefaultView;
                GridView1.DataBind();
                conn.Close();
                
            }
            catch (Exception ex)
            { Response.Write(ex.ToString()); }

        }

        protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int GID=Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value);
            BindDetailView(GID);
        }
        private void BindDetailView(int GID)
        {
            try
            {
                conn.Open();
                string cmdstring = "Select PlayerName, Category, Points From [Moves] Where GameID=@GameID";
                SqlCommand cmd = new SqlCommand(cmdstring, conn);
                cmd.Parameters.AddWithValue("@GameID", GID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView2.DataSource = dt.DefaultView;
                GridView2.DataBind();
                conn.Close();
            }
            catch (Exception ex)

            {
                Response.Write("Login Failed" + ex.ToString());
                log1.Error("Error in openning connection");
            }
        }
    }
}