using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using log4net;
namespace WebApplication1
{
    public partial class logout : System.Web.UI.Page
    {
        private static readonly ILog log = LogManager.GetLogger("test");
        protected void Page_Load(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //if (Context.User.Identity.Name != null)
            //{
                Session.Clear();
                Session.Abandon();
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                string status = "Logged out";
                log.InfoFormat("{0} - {1}", Context.User.Identity.Name, status);
                Response.Cache.SetNoStore();
                try
                {
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Buffer = true;
                    Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                    Response.Expires = -1;
                    Response.CacheControl = "no-cache";
                }
                catch (Exception ex)
                {
                   throw ex;
                }
            //}
            //else
            //{
                //Response.Write("Your are not Logged in");
                Response.Redirect("Login.aspx");
            //}

        }
    }
}