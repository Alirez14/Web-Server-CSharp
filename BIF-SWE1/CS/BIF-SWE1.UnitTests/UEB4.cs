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
    public class UEB4 : AbstractTestFixture<IUEB4>
    {
        [Test]
        public void HelloWorld()
        {
            var ueb = CreateInstance();
            ueb.HelloWorld();
        }

        #region Request
        [Test]
        public void request_should_handle_post()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", method: "POST"));
            Assert.That(obj, Is.Not.Null, "IUEB4.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Method, Is.EqualTo("POST"));
        }

        [Test]
        public void request_should_parse_post_content_length()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", method: "POST", body: "x=a&y=b"));
            Assert.That(obj, Is.Not.Null, "IUEB4.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Method, Is.EqualTo("POST"));
            Assert.That(obj.ContentLength, Is.EqualTo(7));
        }

        [Test]
        public void request_should_parse_post_content_type()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", method: "POST", body: "x=a&y=b"));
            Assert.That(obj, Is.Not.Null, "IUEB4.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Method, Is.EqualTo("POST"));
            Assert.That(obj.ContentType, Is.EqualTo("application/x-www-form-urlencoded"));
        }

        [Test]
        public void request_should_return_post_content()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", method: "POST", body: "x=a&y=b"));
            Assert.That(obj, Is.Not.Null, "IUEB4.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Method, Is.EqualTo("POST"));
            Assert.That(obj.ContentStream, Is.Not.Null);
            Assert.That(obj.ContentStream.Length, Is.EqualTo(7));
            byte[] bodyBytes = new byte[7];
            obj.ContentStream.Read(bodyBytes, 0, 7);
            var body = Encoding.UTF8.GetString(bodyBytes);
            Assert.That(body, Is.EqualTo("x=a&y=b"));
        }

        [Test]
        public void request_should_return_post_content_as_string()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", method: "POST", body: "x=a&y=b"));
            Assert.That(obj, Is.Not.Null, "IUEB4.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Method, Is.EqualTo("POST"));
            Assert.That(obj.ContentString, Is.EqualTo("x=a&y=b"));
        }

        [Test]
        public void request_should_return_post_content_as_bytes()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", method: "POST", body: "x=a&y=b"));
            Assert.That(obj, Is.Not.Null, "IUEB4.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Method, Is.EqualTo("POST"));
            Assert.That(obj.ContentBytes, Is.Not.Null);
            Assert.That(obj.ContentBytes.Length, Is.EqualTo(7));
            var body = Encoding.UTF8.GetString(obj.ContentBytes);
            Assert.That(body, Is.EqualTo("x=a&y=b"));
        }
        #endregion

        #region Response
        [Test]
        public void response_should_send_byte_content()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB4.GetResponse returned null");

            var content = Encoding.UTF8.GetBytes(string.Format("Hello World, my GUID is {0}! Ignore UTF-8 chars!", Guid.NewGuid()));
            obj.SetContent(content);
            obj.StatusCode = 200;

            using (var ms = new MemoryStream())
            {
                obj.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms, Encoding.ASCII))
                {
                    bool header_end_found = false;
                    for (int i = 0; i < 1000 && !sr.EndOfStream; i++)
                    {
                        if (sr.ReadLine().Trim() == "")
                        {
                            header_end_found = true;
                            break;
                        }
                    }
                    Assert.That(header_end_found, Is.True);
                    var buffer = new char[content.Length];
                    sr.Read(buffer, 0, buffer.Length);
                    Assert.That(buffer.Select(c => (byte)c), Is.EquivalentTo(content));
                }
            }
        }

        [Test]
        public void response_should_send_stream_content()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB4.GetResponse returned null");

            var bytes = Encoding.UTF8.GetBytes(string.Format("Hello World, my GUID is {0}! Ignore UTF-8 chars!", Guid.NewGuid()));
            var content = new MemoryStream(bytes);
            obj.SetContent(content);
            obj.StatusCode = 200;

            using (var ms = new MemoryStream())
            {
                obj.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms, Encoding.ASCII))
                {
                    bool header_end_found = false;
                    for (int i = 0; i < 1000 && !sr.EndOfStream; i++)
                    {
                        if (sr.ReadLine().Trim() == "")
                        {
                            header_end_found = true;
                            break;
                        }
                    }
                    Assert.That(header_end_found, Is.True);
                    var buffer = new char[content.Length];
                    sr.Read(buffer, 0, buffer.Length);
                    Assert.That(buffer.Select(c => (byte)c), Is.EquivalentTo(bytes));
                }
            }
        }
        #endregion

        #region PluginManager
        [Test]
        public void pluginmanager_hello_world()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB4.GetPluginManager returned null");
        }

        [Test]
        public void pluginmanager_returns_plugins()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB4.GetPluginManager returned null");
            Assert.That(obj.Plugins, Is.Not.Null);
        }

        [Test]
        public void pluginmanager_returns_1_plugin()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB4.GetPluginManager returned null");
            Assert.That(obj.Plugins, Is.Not.Null);
            Assert.That(obj.Plugins.Count(), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void pluginmanager_plugins_are_not_null()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB4.GetPluginManager returned null");
            Assert.That(obj.Plugins, Is.Not.Null);
            foreach (var plugin in obj.Plugins)
            {
                Assert.That(plugin, Is.Not.Null);
            }
        }

        private class Ueb4TestPlugin : IPlugin
        {

            public float CanHandle(IRequest req)
            {
                throw new NotImplementedException();
            }

            public IResponse Handle(IRequest req)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void pluginmanager_should_add_plugin()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB4.GetPluginManager returned null");
            Assert.That(obj.Plugins, Is.Not.Null);

            int count = obj.Plugins.Count();
            var myPlugin = new Ueb4TestPlugin();
            obj.Add(myPlugin);
            Assert.That(obj.Plugins.Count(), Is.EqualTo(count + 1));
            bool found = false;
            foreach (var plugin in obj.Plugins)
            {
                if (plugin == myPlugin) found = true;
            }
            Assert.That(found, Is.True, "New plugin was not found.");
        }

        [Test]
        public void pluginmanager_should_clear_plugins()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB4.GetPluginManager returned null");
            Assert.That(obj.Plugins, Is.Not.Null);

            obj.Clear();

            Assert.That(obj.Plugins, Is.Not.Null);
            Assert.That(obj.Plugins.Count(), Is.EqualTo(0));
        }
        #endregion
    }
}
