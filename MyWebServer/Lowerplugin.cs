using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class Lowerplugin : IPlugin
    {

        /// <summary>
        /// Returns a score between 0 and 1 to indicate that the plugin is willing to handle the request. The plugin with the highest score will execute the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A score between 0 and 1</returns>
        public float CanHandle(IRequest req)
        {
            var url = req.Url;
            string rawurl = url.RawUrl;

            if (!rawurl.Contains("true"))
            {
                if (rawurl.Contains("/test") || rawurl == "/")
                {
                    return 1f;
                }
               
                return 0.0f;
            }
            else
            {
                return 1f;
            }

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
                var url = req.Url;
                string rawurl = url.RawUrl;
                var resp = new Response();
                resp.StatusCode = 200;
                resp.SetContent(rawurl);
                return resp;
            }
            else
                return null;
        }
    }
}