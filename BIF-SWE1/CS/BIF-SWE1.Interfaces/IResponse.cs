using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BIF.SWE1.Interfaces
{
    public interface IResponse
    {
        /// <summary>
        /// Returns a writable dictionary of the response headers. Never returns null.
        /// </summary>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Returns the content length or 0 if no content is set yet.
        /// </summary>
        int ContentLength { get; }

        /// <summary>
        /// Gets or sets the content type of the response.
        /// </summary>
        /// <exception cref="InvalidOperationException">A specialized implementation may throw a InvalidOperationException when the content type is set by the implementation.</exception>
        string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the current status code. An Exceptions is thrown, if no status code was set.
        /// </summary>
        int StatusCode { get; set; }
        /// <summary>
        /// Returns the status code as string. (200 OK)
        /// </summary>
        string Status { get; }

        /// <summary>
        /// Adds or replaces a response header in the headers dictionary.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="value"></param>
        void AddHeader(string header, string value);

        /// <summary>
        /// Gets or sets the Server response header. Defaults to "BIF-SWE1-Server".
        /// </summary>
        string ServerHeader { get; set; }

        /// <summary>
        /// Sets a string content. The content will be encoded in UTF-8.
        /// </summary>
        /// <param name="content"></param>
        void SetContent(string content);
        /// <summary>
        /// Sets a byte[] as content.
        /// </summary>
        /// <param name="content"></param>
        void SetContent(byte[] content);
        /// <summary>
        /// Sets the stream as content.
        /// </summary>
        /// <param name="stream"></param>
        void SetContent(Stream stream);

        /// <summary>
        /// Sends the response to the network stream.
        /// </summary>
        /// <param name="network"></param>
        void Send(Stream network);
    }
}
