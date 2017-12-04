using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public partial class stats : System.Web.UI.Page
    {
        //string constring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if(IsPostBack)
             { BindGridView(); }*/

         }


         protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
         {
    
        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {
   
        }
    }
}