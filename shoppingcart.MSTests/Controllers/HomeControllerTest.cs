using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using shoppingcart;
using shoppingcart.Controllers;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "shoppingcart.config.log4net", Watch = true)]
namespace shoppingcart.MSTests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeControllerTest));

        [TestMethod]
        public void Index()
        {
            logger.Debug("INSIDE  TEST  --  INDEX !!!");
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
