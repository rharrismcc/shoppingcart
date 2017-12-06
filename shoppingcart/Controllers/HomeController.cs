using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace shoppingcart.Controllers
{
    public class HomeController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            logger.Debug("HOME CONTROLLER : INDEX!");

            return View();
        }

        public ActionResult About()
        {
            logger.Debug("HOME CONTROLLER : ABOUT!");

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            logger.Debug("HOME CONTROLLER : CONTACT!");

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}