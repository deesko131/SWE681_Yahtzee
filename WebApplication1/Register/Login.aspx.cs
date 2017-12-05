﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;
using log4net;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page

    {
        private static readonly ILog log = LogManager.GetLogger("test");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
            }
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));

        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); 
            conn.Open();
            String PlayerName = this.TextBoxPN.Text.Trim();
            String Passwd = this.TextBoxPassword.Text.Trim();
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

                    SqlDataReader dr = command.ExecuteReader();
                    dr.Read();
                    string Password = dr.GetString(0);

                    string Salt = dr.GetString(1);
                    string Hashedpass = ComputeHash(Salt, TextBoxPassword.Text);
                    conn.Close();

                    if (Hashedpass == Password)

                    {
                        string status = "Logged in";
                        log.InfoFormat("{0} - {1}", PlayerName, status);
                        FormsAuthentication.RedirectFromLoginPage(PlayerName, chkPersistentCookie.Checked);
                        Response.Redirect("~/MainMenu.aspx");
                    }
                    else
                    {
                        Response.Write("Login Failed");

                        string status = "Failed Login";
                        log.InfoFormat("{0} - {1}", PlayerName, status);

                    }
                }
                catch (Exception ex)

                {
                    //throw ex;
                    log.Error(ex.ToString());
                }
            }
            else Response.Write("Login Failed");

        }

         static string ComputeHash(string salt, string password)
            {
                var saltBytes = Convert.FromBase64String(salt);
                using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                    return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(16));
            }
    protected void TextBoxPN_TextChanged(object sender, EventArgs e)
    {

    }

    protected void chkPersistentCookie_CheckedChanged(object sender, EventArgs e)
        {

        }
    }   
}