using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;
using MyWebServer;

namespace Uebungen
{
    public class UEB1 : IUEB1
    {
        public void HelloWorld()
        {
            // I'm fine
        }

        public IUrl GetUrl(string path)
        {
            return new Url(path);
        }
    }
}
