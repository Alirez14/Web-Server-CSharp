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
            float handel = 0;


            if (req.Url.Path.Contains('.'))
            {
                handel = 0.9f;
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

                //find the path 
                #region finding the path 
                string path = req.Url.Path;
                path = path.Replace("/", "\\");
                path = path.Substring(1);
                #endregion

                //Type of the file 
                string type = path.Substring(path.IndexOf('.'));

                var resp = new Response();

                //Check if the file exist in System
                if (File.Exists(path))
                {
                    try
                    {
                        //200 means the page exist
                        resp.StatusCode = 200;

                        //reading data from the file
                        using (FileStream fs = new FileStream(path, FileMode.Open))
                        {
                            string content = string.Empty;
                            StreamReader read = new StreamReader(fs);
                            while (!read.EndOfStream)
                            {
                                content += read.ReadLine();
                            }

                            resp.SetContent(content);
                        }

                        resp.contenttype = Response.Typepmap[type];

                        return resp;
                    }
                    catch
                    {
                        resp.StatusCode = 500;
                        return resp;
                    }
                }
                else
                {
                    try
                    {
                        //Page did not found
                        resp.StatusCode = 404;

                        return resp;
                    }
                    catch
                    {
                        resp.StatusCode = 500;
                        return resp;
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}