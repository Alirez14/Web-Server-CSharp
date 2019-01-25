using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using BIF.SWE1.Interfaces;
using System.IO;

namespace MyWebServer
{
    class ToLowerPlugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {
            if (req.Method == "POST" && req.ContentString.Contains("TO LOWER:"))
            {
                return 1f;
            }

            return 0.0f;
        }

        public IResponse Handle(IRequest req)
        {
           

            if (CanHandle(req) != 0.0f)
            {
                try
                {
                    string tolow = req.ContentString.ToLower();

                    var resp = new Response()
                    {
                        StatusCode = 200,
                        ContentType = Response.Typepmap[".html"],
                        content = "<body>"+tolow+"</body>"

                    };
                    
                  

                    return resp;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                return null;
            }
        }
    }
}