using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIF.SWE1.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWebServer;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace UnitTests
{
    [TestClass]
    public class ToLowerTests
    {
        [TestMethod]
        public void Plugin_Can_Handle()
        {
            var stream = new RequestStream().ValidRequest("test.html", "POST", "localhost", "message=");
            var req = new Request(stream);
            ToLowerPlugin lower = new ToLowerPlugin();
            float i = lower.CanHandle(req);

            Assert.AreEqual(1f, i);
        }

        [TestMethod]
        public void Response_Should_Return_Type_Html()
        {
            var stream = new RequestStream().ValidRequest("test.html", "POST", "localhost", "message=");
            var req = new Request(stream);
            ToLowerPlugin lower = new ToLowerPlugin();
            var resp = lower.Handle(req);

            Assert.AreEqual(200, resp.StatusCode);
            Assert.AreEqual("text/html", resp.ContentType);
        }

        [TestMethod]
        public void Plugin_Send_200_OK()
        {
            var stream = new RequestStream().ValidRequest("test.html", "POST", "localhost", "message=SINA");
            var req = new Request(stream);
            ToLowerPlugin lower = new ToLowerPlugin();
            var resp = lower.Handle(req);

            Assert.AreEqual(200, resp.StatusCode);

            using (var ms = new MemoryStream())
            {
                resp.Send(ms);
                Assert.IsTrue(ms.Length > 0);
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms))
                {
                    var firstLine = sr.ReadLine();
                    Assert.AreEqual("HTTP/1.1 200 OK", firstLine);
                }
            }
        }
    }
}
