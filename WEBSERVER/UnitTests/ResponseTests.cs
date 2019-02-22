using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIF.SWE1.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWebServer;


namespace UnitTests
{
    [TestClass]
    public class ResponseTests
    {
        [TestMethod]
        public void Response_Should_Return_200()
        {
            var resp = new Response();
            resp.StatusCode = 200;

            Assert.AreEqual("200 OK", resp.Status);
        }

        [TestMethod]
        public void Response_Should_Return_404_Not_Found()
        {
            var resp = new Response();
            resp.StatusCode = 404;

            Assert.AreEqual("404 Not Found", resp.Status);
        }

        [TestMethod]
        public void Response_Should_Save_Header()
        {
            var resp = new Response();
            resp.AddHeader("foo", "bar");

            Assert.IsNotNull(resp.Headers);
            Assert.IsTrue(resp.Headers.ContainsKey("foo"));
            Assert.AreEqual("bar", resp.Headers["foo"]);
        }

        [TestMethod]
        public void Response_Should_Replace_Header()
        {
            var resp = new Response();
            resp.AddHeader("foo", "bar");

            Assert.IsNotNull(resp.Headers);
            Assert.IsTrue(resp.Headers.ContainsKey("foo"));
            Assert.AreEqual("bar", resp.Headers["foo"]);

            resp.AddHeader("foo", "override");

            Assert.IsNotNull(resp.Headers);
            Assert.IsTrue(resp.Headers.ContainsKey("foo"));
            Assert.AreEqual("override", resp.Headers["foo"]);
        }

        [TestMethod]
        public void Response_Should_Throw_Exception()
        {
            var resp = new Response();
            Exception exp = null;

            try
            {
                resp.StatusCode = 0;
                int i = resp.StatusCode;
            }
            catch (Exception e)
            {
                exp = e;
            }

            Assert.IsNotNull(exp);
        }
    }
}
