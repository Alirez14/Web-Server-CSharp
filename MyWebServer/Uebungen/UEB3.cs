using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyCRM.Uebungen
{
    public class UEB3 : IUEB3
    {
        public void HelloWorld()
        {
        }

        public IRequest GetRequest(System.IO.Stream network)
        {
            throw new NotImplementedException();
        }

        public IResponse GetResponse()
        {
            throw new NotImplementedException();
        }

        public IPlugin GetTestPlugin()
        {
            throw new NotImplementedException();
        }
    }
}
