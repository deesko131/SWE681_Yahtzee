﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.Security;

namespace Yahtzee
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {  //Added
            if (!Request.IsSecureConnection)
            {
                Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            

        }
    }
}