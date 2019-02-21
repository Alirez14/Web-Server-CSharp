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
    public class UEB3 : AbstractTestFixture<IUEB3>
    {
        [Test]
        public void HelloWorld()
        {
            var ueb = CreateInstance();
            ueb.HelloWorld();
        }

        #region Milestone 1
        /// <summary>
        /// Meilenstein 1: Der WebServer ist in einer ersten Version implementiert und Multi-User fähig. Der Request, die URL sowie der Response sind in Objekte gekapselt. Die Funktionalität wird mittels eines Mock/Test/ersten Plugins getestet
        /// </summary>
        [Test]
        public void milestone1_return_main_page()
        {
            var ueb = CreateInstance();
            var obj = ueb.GetTestPlugin();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetTestPlugin returned null");
            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream("/"));
            Assert.That(obj, Is.Not.Null, "IUEB3.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            using (var ms = new MemoryStream())
            {
                resp.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
            }
        }
        #endregion

        #region Request
        [Test]
        public void request_should_parse_header()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/"));
            Assert.That(obj, Is.Not.Null, "IUEB3.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Headers, Is.Not.Null);
            Assert.That(obj.Headers.Count, Is.GreaterThan(0));
            Assert.That(obj.HeaderCount, Is.GreaterThan(0));
        }

        [Test]
        public void request_should_return_header()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/"));
            Assert.That(obj, Is.Not.Null, "IUEB3.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Headers, Is.Not.Null);
            Assert.That(obj.Headers.ContainsKey("user-agent"), Is.True);
            Assert.That(obj.Headers["user-agent"], Is.EqualTo("Unit-Test-Agent/1.0 (The OS)"));
        }

        [Test]
        public void request_should_return_random_header()
        {
            var header = "random_" + Guid.NewGuid();
            var header_value = "value_" + Guid.NewGuid();

            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", header: new[] { new[] { header, header_value } }));
            Assert.That(obj, Is.Not.Null, "IUEB3.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Headers, Is.Not.Null);
            Assert.That(obj.Headers.ContainsKey(header), Is.True);
            Assert.That(obj.Headers[header], Is.EqualTo(header_value));
        }

        [Test]
        public void request_should_return_useragent()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/"));
            Assert.That(obj, Is.Not.Null, "IUEB3.GetRequest returned null");
            Assert.That(obj.UserAgent, Is.EqualTo("Unit-Test-Agent/1.0 (The OS)"));
        }
        #endregion

        #region Response
        [Test]
        public void response_should_save_contenttype()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            obj.ContentType = "text/plain";
            Assert.That(obj.ContentType, Is.EqualTo("text/plain"));
        }

        [Test]
        public void response_should_save_serverheader()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            obj.ServerHeader = "foo";
            Assert.That(obj.ServerHeader, Is.EqualTo("foo"));
        }

        [Test]
        public void response_should_return_default_serverheader()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            Assert.That(obj.ServerHeader, Is.EqualTo("BIF-SWE1-Server"));
        }

        [Test]
        public void response_should_save_string_content()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            var content = "Hello World, my GUID is " + Guid.NewGuid() + "!";
            obj.SetContent(content);
        }

        [Test]
        public void response_should_set_content_length()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            var content = "Hello World, my GUID is " + Guid.NewGuid() + "!";
            obj.SetContent(content);
            Assert.That(obj.ContentLength, Is.EqualTo(Encoding.UTF8.GetByteCount(content)));
        }

        [Test]
        public void response_should_set_content_length_with_utf8()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            var content = "Test: äöüÄÖÜß";
            obj.SetContent(content);
            Assert.That(obj.ContentLength, Is.EqualTo(Encoding.UTF8.GetByteCount(content)));
        }

        [Test]
        public void response_should_send_200()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            var content = "Hello World, my GUID is " + Guid.NewGuid() + "!";
            obj.SetContent(content);
            obj.StatusCode = 200;

            using (var ms = new MemoryStream())
            {
                obj.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms))
                {
                    var firstLine = sr.ReadLine();
                    Assert.That(firstLine, Does.StartWith("HTTP/1."));
                    Assert.That(firstLine, Does.EndWith("200 OK"));
                }
            }
        }

        [Test]
        public void response_should_send_404()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            obj.StatusCode = 404;

            using (var ms = new MemoryStream())
            {
                obj.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms))
                {
                    var firstLine = sr.ReadLine();
                    Assert.That(firstLine, Does.StartWith("HTTP/1."));
                    Assert.That(firstLine, Does.EndWith("404 Not Found"));
                }
            }
        }

        [Test]
        public void response_should_send_header()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            obj.StatusCode = 404;
            var header = "X-Test-Header-" + Guid.NewGuid();
            var header_value = "val_" + Guid.NewGuid();
            obj.AddHeader(header, header_value);

            using (var ms = new MemoryStream())
            {
                obj.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms))
                {
                    var expected = string.Format("{0}: {1}", header, header_value);
                    while(!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        if (line == expected)
                            return;
                    }
                }
            }
            Assert.Fail("Header not found.");
        }

        [Test]
        public void response_should_send_server_header()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            obj.StatusCode = 200;
            var header = "Server";
            var header_value = "server_" + Guid.NewGuid();
            obj.ServerHeader = header_value;

            using (var ms = new MemoryStream())
            {
                obj.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms))
                {
                    var expected = string.Format("{0}: {1}", header, header_value);
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        if (line == expected)
                            return;
                    }
                }
            }
            Assert.Fail("Header not found.");
        }

        [Test]
        public void response_should_send_content()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            var content = "Hello World, my GUID is " + Guid.NewGuid() + "!";
            obj.SetContent(content);
            obj.StatusCode = 200;

            using (var ms = new MemoryStream())
            {
                obj.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms, Encoding.UTF8))
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
                    Assert.That(sr.ReadToEnd(), Is.EqualTo(content));
                }
            }
        }

        [Test]
        public void response_should_fail_sending_no_content()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            obj.StatusCode = 200;
            // Setting a content type but no content is not allowed
            obj.ContentType = "text/html";

            using (var ms = new MemoryStream())
            {
                Assert.That(() => obj.Send(ms), Throws.InstanceOf<Exception>());
            }
        }

        [Test]
        public void response_should_send_content_utf8()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetResponse returned null");

            var content = string.Format("Hello World, my GUID is {0}! And I'll add UTF-8 chars: öäüÖÄÜß!", Guid.NewGuid());
            obj.SetContent(content);
            obj.StatusCode = 200;

            using (var ms = new MemoryStream())
            {
                obj.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms, Encoding.UTF8))
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
                    Assert.That(sr.ReadToEnd(), Is.EqualTo(content));
                }
            }
        }

        #endregion

        #region TestPlugin
        [Test]
        public void testplugin_hello_world()
        {
            var obj = CreateInstance().GetTestPlugin();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetTestPlugin returned null");
        }

        [Test]
        public void testplugin_cannot_handle_url()
        {
            var ueb = CreateInstance();
            var obj = ueb.GetTestPlugin();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetTestPlugin returned null");
            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream("/foo.html"));
            Assert.That(req, Is.Not.Null, "IUEB3.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.EqualTo(0.0f));
        }

        [Test]
        public void testplugin_can_handle_test_url()
        {
            var ueb = CreateInstance();
            var obj = ueb.GetTestPlugin();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetTestPlugin returned null");
            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream("/test/foo.html"));
            Assert.That(req, Is.Not.Null, "IUEB3.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
        }

        [Test]
        public void testplugin_can_handle_test_url_with_parameter()
        {
            var ueb = CreateInstance();
            var obj = ueb.GetTestPlugin();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetTestPlugin returned null");
            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream("/foo.html?test_plugin=true"));
            Assert.That(req, Is.Not.Null, "IUEB3.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
        }

        [Test]
        public void testplugin_can_handle_request()
        {
            var ueb = CreateInstance();
            var obj = ueb.GetTestPlugin();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetTestPlugin returned null");
            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream("/test/foo.html"));
            Assert.That(req, Is.Not.Null, "IUEB3.GetRequest returned null");

            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
        }

        [Test]
        public void testplugin_return_valid_response()
        {
            var ueb = CreateInstance();
            var obj = ueb.GetTestPlugin();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetTestPlugin returned null");
            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream("/test/foo.html"));
            Assert.That(req, Is.Not.Null, "IUEB3.GetRequest returned null");

            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));
        }

        [Test]
        public void testplugin_response_send_content()
        {
            var ueb = CreateInstance();
            var obj = ueb.GetTestPlugin();
            Assert.That(obj, Is.Not.Null, "IUEB3.GetTestPlugin returned null");
            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream("/test/foo.html"));
            Assert.That(req, Is.Not.Null, "IUEB3.GetRequest returned null");

            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            using (var ms = new MemoryStream())
            {
                resp.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
            }
        }
        #endregion
    }
}
