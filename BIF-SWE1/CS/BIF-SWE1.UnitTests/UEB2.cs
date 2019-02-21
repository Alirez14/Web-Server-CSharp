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
    public class UEB2 : AbstractTestFixture<IUEB2>
    {
        [Test]
        public void HelloWorld()
        {
            var ueb = CreateInstance();
            ueb.HelloWorld();
        }


        #region Url
        [Test]
        public void url_should_create_with_path_fragment()
        {
            var obj = CreateInstance().GetUrl("/test.jpg#foo");
            Assert.That(obj, Is.Not.Null, "IUEB2.GetUrl returned null");

            Assert.That(obj.Path, Is.EqualTo("/test.jpg"));
        }

        [Test]
        public void url_should_parse_fragment()
        {
            var obj = CreateInstance().GetUrl("/test.jpg#foo");
            Assert.That(obj, Is.Not.Null, "IUEB2.GetUrl returned null");

            Assert.That(obj.Fragment, Is.EqualTo("foo"));
        }

        [Test]
        public void url_should_split_segments()
        {
            var obj = CreateInstance().GetUrl("/foo/bar/test.jpg");
            Assert.That(obj, Is.Not.Null, "IUEB2.GetUrl returned null");

            Assert.That(obj.Segments, Is.Not.Null);
            Assert.That(obj.Segments, Is.EquivalentTo(new [] { "foo", "bar", "test.jpg" }));
        }
        #endregion

        #region Request
        // Basic tests

        [Test]
        public void request_hello_world()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/"));
            Assert.That(obj, Is.Not.Null, "IUEB2.GetRequest returned null");
        }

        [Test]
        public void request_isValid_on_valid_request()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/"));
            Assert.That(obj, Is.Not.Null, "IUEB2.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
        }

        [Test]
        public void request_isInValid_on_invalid_request()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetInvalidRequestStream());
            Assert.That(obj, Is.Not.Null, "IUEB2.GetRequest returned null");
            Assert.That(obj.IsValid, Is.False);
        }

        [Test]
        public void request_isInValid_on_empty_request()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetEmptyRequestStream());
            Assert.That(obj, Is.Not.Null, "IUEB2.GetRequest returned null");
            Assert.That(obj.IsValid, Is.False);
        }

        [Test]
        public void request_should_parse_method_get()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", method: "GET"));
            Assert.That(obj, Is.Not.Null, "IUEB2.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Method, Is.EqualTo("GET"));
        }

        [Test]
        public void request_should_parse_method_post()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", method: "POST"));
            Assert.That(obj, Is.Not.Null, "IUEB2.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Method, Is.EqualTo("POST"));
        }

        [Test]
        public void request_should_parse_method_post_lowercase()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", method: "post"));
            Assert.That(obj, Is.Not.Null, "IUEB2.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Method, Is.EqualTo("POST"));
        }

        [Test]
        public void request_should_be_invalid_on_method_foo()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/", method: "FOO"));
            Assert.That(obj, Is.Not.Null, "IUEB2.GetRequest returned null");
            Assert.That(obj.IsValid, Is.False);
        }

        [Test]
        public void request_should_parse_url()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/"));
            Assert.That(obj, Is.Not.Null, "IUEB2.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Url, Is.Not.Null);
            Assert.That(obj.Url.RawUrl, Is.EqualTo("/"));
        }

        [Test]
        public void request_should_parse_url_2()
        {
            var obj = CreateInstance().GetRequest(RequestHelper.GetValidRequestStream("/foo.html?a=1&b=2"));
            Assert.That(obj, Is.Not.Null, "IUEB2.GetRequest returned null");
            Assert.That(obj.IsValid, Is.True);
            Assert.That(obj.Url, Is.Not.Null);
            Assert.That(obj.Url.RawUrl, Is.EqualTo("/foo.html?a=1&b=2"));
        }
        #endregion

        #region Response
        // Basic tests
        [Test]
        public void response_hello_world()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB2.GetResponse returned null");
        }

        [Test]
        public void response_should_throw_error_when_no_statuscode_was_set()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB2.GetResponse returned null");

            Assert.Throws(Is.InstanceOf<Exception>(), () => { var tmp = obj.StatusCode; });
        }

        [Test]
        public void response_should_save_statuscode()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB2.GetResponse returned null");

            obj.StatusCode = 404;
            Assert.That(obj.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void response_should_return_status_200()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB2.GetResponse returned null");

            obj.StatusCode = 200;
            Assert.That(obj.Status.ToUpper(), Is.EqualTo("200 OK"));
        }

        [Test]
        public void response_should_return_status_404()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB2.GetResponse returned null");

            obj.StatusCode = 404;
            Assert.That(obj.Status.ToUpper(), Is.EqualTo("404 NOT FOUND"));
        }

        [Test]
        public void response_should_return_status_500()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB2.GetResponse returned null");

            obj.StatusCode = 500;
            Assert.That(obj.Status.ToUpper(), Is.EqualTo("500 INTERNAL SERVER ERROR"));
        }

        [Test]
        public void response_should_save_header()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB2.GetResponse returned null");

            obj.AddHeader("foo", "bar");
            Assert.That(obj.Headers, Is.Not.Null);
            Assert.That(obj.Headers.ContainsKey("foo"), Is.True);
            Assert.That(obj.Headers["foo"], Is.EqualTo("bar"));
        }

        [Test]
        public void response_should_replace_header()
        {
            var obj = CreateInstance().GetResponse();
            Assert.That(obj, Is.Not.Null, "IUEB2.GetResponse returned null");

            obj.AddHeader("foo", "bar");
            Assert.That(obj.Headers, Is.Not.Null);
            Assert.That(obj.Headers.ContainsKey("foo"), Is.True);
            Assert.That(obj.Headers["foo"], Is.EqualTo("bar"));

            obj.AddHeader("foo", "override");
            Assert.That(obj.Headers.ContainsKey("foo"), Is.True);
            Assert.That(obj.Headers["foo"], Is.EqualTo("override"));
        }
        #endregion
    }
}
