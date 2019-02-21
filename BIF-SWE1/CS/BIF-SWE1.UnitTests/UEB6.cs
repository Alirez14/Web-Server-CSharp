using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BIF.SWE1.Interfaces;
using System.IO;

namespace BIF.SWE1.UnitTests
{
    [TestFixture]
    public class UEB6 : AbstractTestFixture<IUEB6>
    {
        [Test]
        public void HelloWorld()
        {
            var ueb = CreateInstance();
            ueb.HelloWorld();
        }

        private static StringBuilder GetBody(IResponse resp)
        {
            StringBuilder body = new StringBuilder();
            using (var ms = new MemoryStream())
            {
                resp.Send(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var sr = new StreamReader(ms);
                while (!sr.EndOfStream)
                {
                    body.AppendLine(sr.ReadLine());
                }
            }
            return body;
        }

        #region Temp-Plugin tests
        [Test]
        public void temp_plugin_HelloWorld()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");
        }

        [Test]
        public void temp_plugin_get_url()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url = ueb.GetTemperatureUrl(new DateTime(2014, 1, 1), new DateTime(2014, 1, 2));
            Assert.That(url, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");
        }

        [Test]
        public void temp_plugin_get_url_2()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url_1 = ueb.GetTemperatureUrl(new DateTime(2014, 1, 1), new DateTime(2014, 1, 2));
            Assert.That(url_1, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");

            var url_2 = ueb.GetTemperatureUrl(new DateTime(2015, 1, 1), new DateTime(2015, 1, 2));
            Assert.That(url_2, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");

            Assert.That(url_1, Is.Not.EqualTo(url_2));
        }

        [Test]
        public void temp_plugin_get_rest_url()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url = ueb.GetTemperatureRestUrl(new DateTime(2014, 1, 1), new DateTime(2014, 1, 2));
            Assert.That(url, Is.Not.Null, "IUEB6.GetTemperatureRestUrl returned null");
        }

        [Test]
        public void temp_plugin_get_different_urls()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url_html = ueb.GetTemperatureUrl(new DateTime(2014, 1, 1), new DateTime(2014, 1, 2));
            Assert.That(url_html, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");

            var url_rest = ueb.GetTemperatureRestUrl(new DateTime(2014, 1, 1), new DateTime(2014, 1, 2));
            Assert.That(url_rest, Is.Not.Null, "IUEB6.GetTemperatureRestUrl returned null");

            Assert.That(url_html, Is.Not.EqualTo(url_rest));
        }

        [Test]
        public void temp_plugin_handle()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url = ueb.GetTemperatureUrl(new DateTime(2014, 1, 1), new DateTime(2014, 1, 2));
            Assert.That(url, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentType, Is.EqualTo("text/html"));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));
        }

        [Test]
        public void temp_plugin_handle_rest_call()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetTemperaturePlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetTemperaturePlugin returned null");

            var url = ueb.GetTemperatureRestUrl(new DateTime(2014, 1, 1), new DateTime(2014, 1, 2));
            Assert.That(url, Is.Not.Null, "IUEB6.GetTemperatureUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentType, Is.EqualTo("text/xml"));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));
        }
        #endregion
        
        #region Navi-Plugin tests
        [Test]
        public void navi_plugin_HelloWorld()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetNavigationPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetNavigationPlugin returned null");
        }

        [Test]
        public void navi_plugin_get_url()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetNavigationPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetNavigationPlugin returned null");

            var url = ueb.GetNaviUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetNaviUrl returned null");
        }

        [Test]
        public void navi_plugin_handle()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetNavigationPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetNavigationPlugin returned null");

            var url = ueb.GetNaviUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetNaviUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url, method: "POST", body: "street=Hauptplatz"));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));
        }

        [Test]
        public void navi_plugin_contains_summary()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetNavigationPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetNavigationPlugin returned null");

            var url = ueb.GetNaviUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetNaviUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url, method: "POST", body: "street=Hauptplatz"));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            var body = GetBody(resp);
            Assert.That(body.ToString(), Does.Contain("Orte gefunden")); // 42 Orte gefunden
        }

        [Test]
        public void navi_plugin_handle_empty()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetNavigationPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetNavigationPlugin returned null");

            var url = ueb.GetNaviUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetNaviUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url, method: "POST", body: "street="));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            var body = GetBody(resp);
            Assert.That(body.ToString(), Does.Contain("Bitte geben Sie eine Anfrage ein"));
        }
        #endregion

        #region ToLower-Plugin tests
        [Test]
        public void lower_plugin_HelloWorld()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");
        }

        [Test]
        public void lower_plugin_get_url()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = ueb.GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");
        }

        [Test]
        public void lower_plugin_handle()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = ueb.GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            string textToTest = string.Format("Hello - WorlD! {0}", Guid.NewGuid());

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url, method: "POST", body: string.Format("text={0}",  textToTest)));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            StringBuilder body = GetBody(resp);
            Assert.That(body.ToString(), Does.Contain(textToTest.ToLower()));
        }

        [Test]
        public void lower_plugin_handle_empty()
        {
            var ueb = CreateInstance();
            var plugin = ueb.GetToLowerPlugin();
            Assert.That(plugin, Is.Not.Null, "IUEB6.GetToLowerPlugin returned null");

            var url = ueb.GetToLowerUrl();
            Assert.That(url, Is.Not.Null, "IUEB6.GetToLowerUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url, method: "POST", body: "text="));
            Assert.That(req, Is.Not.Null, "IUEB6.GetRequest returned null");

            Assert.That(plugin.CanHandle(req), Is.GreaterThan(0).And.LessThanOrEqualTo(1));

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            StringBuilder body = GetBody(resp);
            Assert.That(body.ToString(), Does.Contain("Bitte geben Sie einen Text ein"));
        }
        #endregion
    }
}
