using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    class Lowerplugin : IPlugin
    {
        /// <summary>
        /// Returns a score between 0 and 1 to indicate that the plugin is willing to handle the request. The plugin with the highest score will execute the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A score between 0 and 1</returns>
        public float CanHandle(IRequest req)
        {
    
            return 0.0f;
        }
        /// <summary>
        /// Called by the server when the plugin should handle the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A new response object.</returns>
        public IResponse Handle(IRequest req)
        {
            var resp = new Response();

            if (CanHandle(req) != 0.0f)
            {
                var url = req.Url;
                string rawurl = url.RawUrl;
                resp.StatusCode = 200;
                if (rawurl == String.Empty)
                {
                    resp.SetContent("Bitte geben Sie einen Text ein");
                }
                else
                {
                    resp.SetContent(rawurl);
                }
                return resp;
            }


            resp.StatusCode = 404;
            return resp;

        }
    }
}