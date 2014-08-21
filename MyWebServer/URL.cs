using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace ueb1
{
    public class Url : IUrl
    {
        public Url()
        {

        }

        public Url(string raw)
        {

        }

        public IDictionary<string, string> Parameter
        {
            get { throw new NotImplementedException(); }
        }

        public string Path
        {
            get { throw new NotImplementedException(); }
        }

        public string RawUrl
        {
            get { throw new NotImplementedException(); }
        }
    }
}
