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
        public void Plugin_Manager_Should_Select_Tempplugin()
        {
            //Arrange
            var stream = new RequestStream().ValidRequest("temp.html?date=&submit=ok");
            var temp = new Tempplugin();
            var req = new Request(stream);
            var manager = new PluginManager();
            var plugin = Program.SelectPlugin(manager, req);
            var type = typeof(Tempplugin);

            //Assert
            Assert.IsInstanceOfType(plugin, type);
        }

        [TestMethod]
        public void CanHandle_Should_Not_Be_Zero()
        {
            //Arrange
            var stream = new RequestStream().ValidRequest("temp.html?date=&submit=ok");
            var temp = new Tempplugin();
            var req = new Request(stream);
            float zero;

            //Act
            float nummer = temp.CanHandle(req);
            zero = 0.0f;

            //Assert
            Assert.IsTrue(req.IsValid);
            Assert.AreEqual("GET", req.Method);
            Assert.IsNotNull(req.Url);
            Assert.AreNotEqual(zero, nummer);
        }

        [TestMethod]
        public void No_Given_Date()
        {
            //Arrange
            var stream = new RequestStream().ValidRequest("temp.html?date=&Submit");
            var temp = new Tempplugin();
            float zero = 0.0f;

            //Act
            var req = new Request(stream);
            float nummer = temp.CanHandle(req);
            var resp = temp.Handle(req);
            
            //Assert
            Assert.AreNotEqual(zero, nummer);
            Assert.AreEqual(400, resp.StatusCode);
        }

        [TestMethod]
        public void Should_Return_Xml()
        {
            //Arrange
            var stream = new RequestStream().ValidRequest("/GetTemperature/2019-01-10");
            var temp = new Tempplugin();

            //Act
            var req = new Request(stream);
            float nummer = temp.CanHandle(req);
            var resp = temp.Handle(req);
            float zero = 0.0f;

            //Assert
            Assert.AreNotEqual(zero, nummer);
            Assert.IsNotNull(resp.ContentType);
            Assert.AreEqual("text/xml", resp.ContentType);
            Assert.AreEqual(200, resp.StatusCode);
        }

        [TestMethod]
        public void Should_Return_Html()
        {
            //Arrange
            var stream = new RequestStream().ValidRequest(@"\C:\Users\sina_\OneDrive\Desktop\sina\SWEPROJ\WEBSERVER\SWE\temp.html?date=2019-01-10&submit=Search");
            var temp = new Tempplugin();

            //Act
            var req = new Request(stream);
            var resp = temp.Handle(req);

            //Assert
            Assert.IsTrue(req.IsValid);
            Assert.AreEqual("GET", req.Method);
            Assert.AreEqual("text/html", resp.ContentType);
            Assert.AreEqual(200, resp.StatusCode);
        }
    }
}
