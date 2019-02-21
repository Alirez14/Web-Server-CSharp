using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BIF.SWE1.Interfaces;
using System.IO;

namespace BIF.SWE1.UnitTests
{
    public class Ueb5TestPlugin : IPlugin
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

    public class Ueb5NoTestPlugin 
    {
        
    }

    [TestFixture]
    public class UEB5 : AbstractTestFixture<IUEB5>
    {
        [Test]
        public void HelloWorld()
        {
            var ueb = CreateInstance();
            ueb.HelloWorld();
        }

        #region Helper
        private IPlugin SelectPlugin(IPluginManager mgr, IRequest req)
        {
            IPlugin plugin = null;
            float max = 0;
            foreach (var p in mgr.Plugins)
            {
                float canHandle = p.CanHandle(req);
                if (canHandle > max)
                {
                    max = canHandle;
                    plugin = p;
                }
            }

            return plugin;
        }
        #endregion

        #region Milestone 2
        /// <summary>
        /// Der WebServer kann Plugins laden und benutzen (keine hardcodierten Stellen im Code mehr)
        /// Das statische Dateien Plugin funktioniert (und kann z.B. die Startseite ausliefern)
        /// erste Unittests wurden implementiert
        /// bei einem weiteren Plugin ist ein deutlicher Fortschritt zu sehen
        /// </summary>
        [Test]
        public void milestone2_return_main_page()
        {
            var ueb = CreateInstance();
            
            var mgr = ueb.GetPluginManager();
            Assert.That(mgr, Is.Not.Null, "IUEB5.GetPluginManager returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream("/"));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            var plugin = SelectPlugin(mgr, req);
            Assert.That(plugin, Is.Not.Null, "No plugin found to server the '/' request");

            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));

            using (var ms = new MemoryStream())
            {
                resp.Send(ms);
                Assert.That(ms.Length, Is.GreaterThan(0));
            }
        }

        [Test]
        public void milestone2_return_error_on_invalid_url()
        {
            var ueb = CreateInstance();

            var mgr = ueb.GetPluginManager();
            Assert.That(mgr, Is.Not.Null, "IUEB5.GetPluginManager returned null");
            
            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream("/i_am_a_unknown_url.html"));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            var plugin = SelectPlugin(mgr, req);

            if (plugin != null)
            { 
                var resp = plugin.Handle(req);
                Assert.That(resp, Is.Not.Null);
                Assert.That(resp.StatusCode, Is.Not.EqualTo(200));
            }
            else
            {
                // No plugin will not handle unknown URL
            }
        }
        #endregion

        #region PluginManager
        [Test]
        public void pluginmanager_return_all_plugins()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetPluginManager returned null");
            Assert.That(obj.Plugins, Is.Not.Null);
            Assert.That(obj.Plugins.Count(), Is.GreaterThanOrEqualTo(4));
        }

        [Test]
        public void pluginmanager_all_plugins_have_unique_type()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetPluginManager returned null");
            Assert.That(obj.Plugins, Is.Not.Null);
            Dictionary<Type, bool> _typeMap = new Dictionary<Type, bool>();
            foreach (var p in obj.Plugins)
            {
                Assert.That(p, Is.Not.Null);
                var t = p.GetType();
                Assert.That(_typeMap.ContainsKey(t), Is.False);
                _typeMap.Add(t, true);
            }
        }
        
        [Test]
        public void pluginmanager_contains_plugin_for_start_page()
        {
            var ueb = CreateInstance();
            var obj = ueb.GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetPluginManager returned null");
            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream("/"));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");
            
            Assert.That(obj.Plugins, Is.Not.Null);
            IPlugin plugin = SelectPlugin(obj, req);
            Assert.That(plugin, Is.Not.Null);
            var resp = plugin.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.GreaterThan(0));
        }

        [Test]
        public void pluginmanager_should_add_plugin()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetPluginManager returned null");
            Assert.That(obj.Plugins, Is.Not.Null);

            int count = obj.Plugins.Count();
            obj.Add("BIF.SWE1.UnitTests.Ueb5TestPlugin, BIF-SWE1.UnitTests");
            Assert.That(obj.Plugins.Count(), Is.EqualTo(count + 1));
            bool found = false;
            foreach (var plugin in obj.Plugins)
            {
                if (plugin is Ueb5TestPlugin) found = true;
            }
            Assert.That(found, Is.True, "New plugin was not found.");
        }

        [Test]
        public void pluginmanager_should_fail_adding_non_existing_plugin()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetPluginManager returned null");
            Assert.That(obj.Plugins, Is.Not.Null);

            Assert.That(() => obj.Add("BIF.SWE1.UnitTests.Ueb999TestPlugin, BIF-SWE1.UnitTests"), Throws.InstanceOf<Exception>());
        }

        [Test]
        public void pluginmanager_should_fail_adding_plugin_not_implementing_plugin()
        {
            var obj = CreateInstance().GetPluginManager();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetPluginManager returned null");
            Assert.That(obj.Plugins, Is.Not.Null);

            Assert.That(() => obj.Add("BIF.SWE1.UnitTests.Ueb5NoTestPlugin, BIF-SWE1.UnitTests"), Throws.InstanceOf<Exception>());
        }
        #endregion

        #region Static File Plugin
        private const string static_file_content = "Hello World!";

        private void SetupStaticFilePlugin(IUEB5 ueb, string fileName)
        {
            string folder = Path.Combine(WorkingDirectory, "static-files");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            using (var fs = File.OpenWrite(Path.Combine(folder, fileName)))
            using (var sw = new StreamWriter(fs))
            {
                fs.SetLength(0);
                sw.Write(static_file_content);
            }
            
            ueb.SetStatiFileFolder(folder);
        }
        [Test]
        public void staticfileplugin_hello_world()
        {
            var ueb = CreateInstance();
            SetupStaticFilePlugin(ueb, "foo.txt");
            
            var obj = ueb.GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = ueb.GetStaticFileUrl("bar.txt");
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");
            Assert.That(url.EndsWith("bar.txt"));
        }

        [Test]
        public void staticfileplugin_return_file()
        {
            var fileName = string.Format("foo-{0}.txt", Guid.NewGuid());
            var ueb = CreateInstance();
            SetupStaticFilePlugin(ueb, fileName);

            var obj = ueb.GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = ueb.GetStaticFileUrl(fileName);
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void staticfileplugin_return_file_content()
        {
            var fileName = string.Format("foo-{0}.txt", Guid.NewGuid());
            var ueb = CreateInstance();
            SetupStaticFilePlugin(ueb, fileName);

            var obj = ueb.GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = ueb.GetStaticFileUrl(fileName);
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            Assert.That(obj.CanHandle(req), Is.GreaterThan(0.0f).And.LessThanOrEqualTo(1.0f));
            var resp = obj.Handle(req);
            Assert.That(resp, Is.Not.Null);
            Assert.That(resp.StatusCode, Is.EqualTo(200));
            Assert.That(resp.ContentLength, Is.EqualTo(static_file_content.Length));

            using (var ms = new MemoryStream())
            {
                resp.Send(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var sr = new StreamReader(ms);
                string lastLine = null;
                while(!sr.EndOfStream)
                {
                    lastLine = sr.ReadLine();
                }
                Assert.That(lastLine, Is.EqualTo(static_file_content));
            }
        }

        [Test]
        public void staticfileplugin_fail_on_missing_file()
        {
            var fileName = string.Format("foo-{0}.txt", Guid.NewGuid());
            var ueb = CreateInstance();
            SetupStaticFilePlugin(ueb, fileName);

            var obj = ueb.GetStaticFilePlugin();
            Assert.That(obj, Is.Not.Null, "IUEB5.GetStaticFilePlugin returned null");

            var url = ueb.GetStaticFileUrl("missing-" + fileName);
            Assert.That(url, Is.Not.Null, "IUEB5.GetStaticFileUrl returned null");

            var req = ueb.GetRequest(RequestHelper.GetValidRequestStream(url));
            Assert.That(req, Is.Not.Null, "IUEB5.GetRequest returned null");

            if (obj.CanHandle(req) > 0)
            {
                var resp = obj.Handle(req);
                Assert.That(resp, Is.Not.Null);
                Assert.That(resp.StatusCode, Is.EqualTo(404));
            }
            else
            {
                // static file plugin will not handle missing files.
            }
        }
        #endregion
    }
}
