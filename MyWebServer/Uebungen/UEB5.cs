using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;
using MyWebServer;

namespace Uebungen
{
    public class UEB5 : IUEB5
    {
        public void HelloWorld()
        {
        }

        public IPluginManager GetPluginManager()
        {
            throw new NotImplementedException();
        }

        public IRequest GetRequest(System.IO.Stream network)
        {
            throw new NotImplementedException();
        }

        public IPlugin GetStaticFilePlugin()
        {
            throw new NotImplementedException();
        }

        public string GetStaticFileUrl(string fileName)
        {
            throw new NotImplementedException();
        }

        public void SetStatiFileFolder(string folder)
        {
            throw new NotImplementedException();
        }
    }
}
