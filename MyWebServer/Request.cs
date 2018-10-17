using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;
using System.IO;


namespace MyWebServer
{
    public class Request : IRequest
    {
        public Request() { }
    
        /// <summary>
        /// Returns true if the request is valid. A request is valid, if method and url could be parsed. A header is not necessary.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Returns the request method in UPPERCASE. get -> GET.
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// Returns a URL object of the request. Never returns null.
        /// </summary>
        public IUrl Url { get; }

        /// <summary>
        /// Returns the request header. Never returns null. All keys must be lower case.
        /// </summary>
        public IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Returns the user agent from the request header
        /// </summary>
        public string UserAgent { get; }

        /// <summary>
        /// Returns the number of header or 0, if no header where found.
        /// </summary>
        public int HeaderCount { get; }

        /// <summary>
        /// Returns the parsed content length request header.
        /// </summary>
        public int ContentLength { get; }

        /// <summary>
        /// Returns the parsed content type request header. Never returns null.
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Returns the request content (body) stream or null if there is no content stream.
        /// </summary>

        public Stream ContentStream { get; }

        /// <summary>
        /// Returns the request content (body) as string or null if there is no content.
        /// </summary>
        public string ContentString { get; }

        /// <summary>
        /// Returns the request content (body) as byte[] or null if there is no content.
        /// </summary>
        public byte[] ContentBytes { get; }
    }

}


