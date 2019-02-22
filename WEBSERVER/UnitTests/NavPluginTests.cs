using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWebServer;

namespace UnitTests
{
    [TestClass]
    public class NavPluginTests
    {
        [TestMethod]
        public void Nav_Plugin_CanHandle()
        {
            var stream = new RequestStream().ValidRequest("test.html", "POST", "localhost", "street=");
            var req = new Request(stream);
            var nav = new Naviplugin().CanHandle(req);

            Assert.AreEqual(1f, nav);
        }

        [TestMethod]
        public void Nav_Should_Throw_Exception()
        {
            var stream = new RequestStream().ValidRequest("test.html", "POST", "localhost", "street=");
            var req = new Request(stream);
            Exception exc = null;
            try
            {
                var nav = new Naviplugin().TestReader("foo");
            }
            catch (Exception e)
            {
                exc = e;
            }

            Assert.IsInstanceOfType(exc, typeof(Exception));

        }
    }
}
