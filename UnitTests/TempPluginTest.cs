using System;
using System.Dynamic;
using System.IO;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIF.SWE1;
using BIF.SWE1.Interfaces;
using MyWebServer;

namespace UnitTests
{
    [TestClass]
    public class TempPluginTest
    {
        [TestMethod]
        public void CanHandleShouldNotBeZero()
        {
            //Arrange
            var stream = new RequestStream().ValidRequest("temp.html?date=&submit=ok");
            var temp = new Tempplugin();
            var req = new Request(stream);

            //Act
            float nummer = temp.CanHandle(req);
            float zero = 0.0f;

            //Assert
            Assert.AreEqual("GET", req.Method);
            Assert.IsNotNull(req.Url);
            Assert.AreNotEqual(zero, nummer);
        }

        [TestMethod]
        public void NoDate()
        {
            //Arrange
            var stream = new RequestStream().ValidRequest("temp.html?date=&Submit");
            var temp = new Tempplugin();

            //Act
            var req = new Request(stream);
            float nummer = temp.CanHandle(req);
            var resp = temp.Handle(req);
            float zero = 0.0f;
            
            //Assert
            Assert.AreNotEqual(zero, nummer);
            Assert.AreEqual(400, resp.StatusCode);
        }

        [TestMethod]
        public void ReturnXML()
        {

        }
    }
}
