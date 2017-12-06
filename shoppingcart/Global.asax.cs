using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "shoppingcart.config.log4net", Watch = true)]
namespace shoppingcart
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            log4net.ILog logger2 = log4net.LogManager.GetLogger(this.GetType());

            logger.Debug("INSIDE APP START !!!!!");
            logger2.Debug("INSIDE APP START !!!!!");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
