using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyCRM.Uebungen
{
    public class UEB4 : IUEB4
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

        public IResponse GetResponse()
        {
            throw new NotImplementedException();
        }
    }
}
