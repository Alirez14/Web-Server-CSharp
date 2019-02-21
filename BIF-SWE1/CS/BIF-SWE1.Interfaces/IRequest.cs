using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BIF.SWE1.Interfaces
{
    public interface IRequest
    {
        /// <summary>
        /// Returns true if the request is valid. A request is valid, if method and url could be parsed. A header is not necessary.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Returns the request method in UPPERCASE. get -> GET.
        /// </summary>
        string Method { get; }

        /// <summary>
        /// Returns a URL object of the request. Never returns null.
        /// </summary>
        IUrl Url { get; }
        
        /// <summary>
        /// Returns the request header. Never returns null. All keys must be lower case.
        /// </summary>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Returns the user agent from the request header
        /// </summary>
        string UserAgent { get; }

        /// <summary>
        /// Returns the number of header or 0, if no header where found.
        /// </summary>
        int HeaderCount { get; }

        /// <summary>
        /// Returns the parsed content length request header.
        /// </summary>
        int ContentLength { get; }

        /// <summary>
        /// Returns the parsed content type request header. Never returns null.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Returns the request content (body) stream or null if there is no content stream.
        /// </summary>
        Stream ContentStream { get; }

        /// <summary>
        /// Returns the request content (body) as string or null if there is no content.
        /// </summary>
        string ContentString { get; }

        /// <summary>
        /// Returns the request content (body) as byte[] or null if there is no content.
        /// </summary>
        byte[] ContentBytes { get; }
    }
}
