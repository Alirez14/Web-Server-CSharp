using System;
using System.IO;
using System.Linq;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class Staticfileplugin : IPlugin
    {
        /// <summary>
        /// Returns a score between 0 and 1 to indicate that the plugin is willing to handle the request. The plugin with the highest score will execute the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A score between 0 and 1</returns>
        public float CanHandle(IRequest req)
        {
            float handel=0;
            Request newreq = (Request) req;
            

            if (newreq.filename.Contains("\\"))
            {
                handel += 1f;
                if (newreq.filename.Contains("missing-"))
                {
                    handel -= 0.5f;
                }

            }
            else
            {
                handel = 0;

            }

            return handel;
        }

        /// <summary>
        /// Called by the server when the plugin should handle the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A new response object.</returns>
        public IResponse Handle(IRequest req)
        {
            if (CanHandle(req) != 0.0f)

            {
                Request newreq = (Request) req;
                var resp = new Response();
                try
                {
                    resp.StatusCode = 200;
                    using (FileStream fs = new FileStream(newreq.filename, FileMode.Open))
                    {
                        string content = string.Empty;
                        StreamReader read = new StreamReader(fs);
                        while (!read.EndOfStream)
                        {
                            content += read.ReadLine();
                        }

                        resp.SetContent(content);
                    }


                    return resp;
                }
                catch
                {
                    resp.StatusCode = 404;
                    return resp;
                }

            }
            else
                return null;
        }
    }
}