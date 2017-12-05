using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using log4net;

namespace Yahtzee
{
    public class Global : HttpApplication
    {
        //added
        private static readonly ILog log = LogManager.GetLogger("test");
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //added
            log.Info("Startup application.");

        }
        
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            log.Error(ex.ToString());
            Server.ClearError();
            Response.Redirect("ErrorPage.aspx");
                 }
            
    }
}