using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE1.Interfaces
{
    public interface IPlugin
    {
        /// <summary>
        /// Returns a score between 0 and 1 to indicate that the plugin is willing to handle the request. The plugin with the highest score will execute the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A score between 0 and 1</returns>
        float CanHandle(IRequest req);

        /// <summary>
        /// Called by the server when the plugin should handle the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A new response object.</returns>
        IResponse Handle(IRequest req);
    }
}
