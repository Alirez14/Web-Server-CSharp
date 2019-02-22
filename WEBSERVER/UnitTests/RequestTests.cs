using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIF.SWE1.Interfaces;
using MyWebServer;

namespace UnitTests
{
    [TestClass]
    public class RequestTests
    {
        [TestMethod]
        public void User_Agent_Should_Not_Be_Null()
        {
            var stream = new RequestStream().ValidRequest("test.html");
            var req = new Request(stream);

            Assert.IsNotNull(req.UserAgent);
        }

        [TestMethod]
        public void Method_Should_Be_Null()
        {
            var stream = new RequestStream().ValidRequest("test.html", "foo");
            var req = new Request(stream);

            Assert.IsFalse(req.IsValid);
            Assert.IsNull(req.Method);
        }

        [TestMethod]
        public void Url_Fragment_Should_Not_Be_Null()
        {
            var stream = new RequestStream().ValidRequest("test.html#foo");
            var req = new Request(stream);

            Assert.IsNotNull(req.Url.Fragment);
        }

        [TestMethod]
        public void Url_Filename_Should_Be_Empty()
        {
            var stream = new RequestStream().ValidRequest("test#foo");
            var req = new Request(stream);

            Assert.AreEqual(String.Empty, req.Url.FileName);
        }

        [TestMethod]
        public void Not_Valid_Request()
        {
            var stream = new RequestStream().ValidRequest("");
            var req = new Request(stream);

            Assert.IsFalse(req.IsValid);

        }
    }
}
