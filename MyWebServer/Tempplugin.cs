using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    class Tempplugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            var url = req.Url;
            string rawurl = url.RawUrl;
            string[] week = {"Samstag", "Sonntag", "Montag", "Dienstag", "Mitwoch", "Donnerstag", "Freitag"};

            foreach (var item in week)
            {
                if (rawurl.Contains(item))
                {
                    return 0.4f;
                }
            }
            if (rawurl.Contains(":"))
            {
                return 0.5f;
            }
            return 0f;
        }
       
        public IResponse Handle(IRequest req)
        {
            var resp = new Response();

            if (CanHandle(req) != 0.0f)
            {
                if (CanHandle(req) == 0.4f)
                {
                    var url = req.Url;
                    string rawurl = url.RawUrl;
                    resp.StatusCode = 200;
                    resp.SetContent(rawurl);
                    resp.ContentType = "text/html";
                }
                if(CanHandle(req) == 0.5f)
                {
                    var url = req.Url;
                    string rawurl = url.RawUrl;
                    resp.StatusCode = 200;
                    resp.SetContent(rawurl);
                    resp.ContentType = "text/xml";
                }
                return resp;
            }

            resp.StatusCode = 404;
            return resp;

        }
    }
}

