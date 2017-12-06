using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using shoppingcart.Models;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = "shoppingcart.config.log4net", Watch = true)]
namespace shoppingcart.NUnitTests
{
    [TestFixture]
    public class TestClass
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(TestClass));

        [Test]
        public void Test01()
        {
            logger.Debug("INSIDE  TEST01");

            Assert.IsTrue(true);
        }

        [Test]
        public void TestProductIsNotNull()
        {
            logger.Debug("INSIDE  TestProductIsNotNull");

            Product product = new Product();
            Assert.IsNotNull(product);
        }
    }
}
