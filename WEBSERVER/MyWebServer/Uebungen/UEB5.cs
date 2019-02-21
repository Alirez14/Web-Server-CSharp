using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BIF.SWE1.Interfaces;
using MyWebServer;

namespace Uebungen
{
    public class UEB5 : IUEB5
    {
        public string s = string.Empty;
        public void HelloWorld()
        {
        }

        public IPluginManager GetPluginManager()
        {
            return new PluginManager();
  
        }

        public IRequest GetRequest(System.IO.Stream network)
        {
            return new Request(network);
            
        }



        public IPlugin GetStaticFilePlugin()
        {
            return new Staticfileplugin();
        }

        public string GetStaticFileUrl(string fileName)
        {
            return s + "\\" + fileName;

        }

        public void SetStatiFileFolder(string folder)
        {
            s = folder;
        }
    }
}
