using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;
using MyWebServer;

namespace Uebungen
{
    public class UEB6 : IUEB6
    {
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



        public string GetNaviUrl()
        {
            return String.Empty;
        }

        public IPlugin GetNavigationPlugin()
        {
           return new Naviplugin();
        }

        public IPlugin GetTemperaturePlugin()
        {
            return new Tempplugin();
        }

        public string GetTemperatureRestUrl(DateTime from, DateTime until)
        {
            string date = from.ToString() + " " + until.ToString();
            return date;
        }

        public string GetTemperatureUrl(DateTime from, DateTime until)
        {
            string date = from.ToString("F") + " " + until.ToString();
            return date;
        }

        public IPlugin GetToLowerPlugin()
        {
            return null;
        }

        public string GetToLowerUrl()
        {
            return string.Empty;
        }
    }
}
