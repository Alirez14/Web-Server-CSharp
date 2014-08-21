using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;
using ueb1;

namespace MyCRM.Uebungen
{
    public class UEB2 : IUEB2
    {
        public void HelloWorld()
        {
        }

        public IUrl GetUrl(string path)
        {
            return new Url(path);
        }
    }
}
