using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace BIF.SWE1.Interfaces
{
    public class Response : IResponse
    {
        public string content;
        public string contenttype;

        /// <summary>
        /// Returns a writable dictionary of the response headers. Never returns null.
        /// </summary>
        public IDictionary<string, string> headers = new Dictionary<string, string>();

        public IDictionary<string, string> Headers
        {
            get { return headers; }
        }

        /// <summary>
        /// Returns the content length or 0 if no content is set yet.
        /// </summary>
        public int ContentLength
        {
            get { return Encoding.UTF8.GetByteCount(content); }
        }

        /// <summary>
        /// Gets or sets the content type of the response.
        /// </summary>
        /// <exception cref="InvalidOperationException">A specialized implementation may throw a InvalidOperationException when the content type is set by the implementation.</exception>
        public string ContentType
        {
            get { return contenttype; }
            set { contenttype = value; }
        }

        /// <summary>
        /// Gets or sets the current status code. An Exceptions is thrown, if no status code was set.
        /// </summary>
        public int statusCode = 0;

        public int StatusCode
        {
            get
            {
                if (statusCode == 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    return statusCode;
                }
            }
            set { statusCode = value; }
        }

        /// <summary>
        /// Returns the status code as string. (200 OK)
        /// </summary>
        public string Status
        {
            get
            {
                if (statusCode == 200)
                {
                    return "200 OK";
                }
                else if (statusCode == 404)
                {
                    return "404 Not Found";
                }
                else if (statusCode == 500)
                {
                    return "500 INTERNAL SERVER ERROR";
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Adds or replaces a response header in the headers dictionary.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="value"></param>
        public void AddHeader(string header, string value)
        {
            if (!Headers.ContainsKey(header))
            {
                Headers.Add(header, value);
            }
            else
            {
                Headers[header] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Server response header. Defaults to "BIF-SWE1-Server".
        /// </summary>
        public string ServerHeader { get; set; } = "BIF-SWE1-Server";

        /// <summary>
        /// Sets a string content. The content will be encoded in UTF-8.
        /// </summary>
        /// <param name="content"></param>
        public void SetContent(string content)
        {
            this.content = content;
        }

        /// <summary>
        /// Sets a byte[] as content.
        /// </summary>
        /// <param name="content"></param>
        public void SetContent(byte[] content)
        {
            this.content = UTF8Encoding.UTF8.GetString(content);
        }

        /// <summary>
        /// Sets the stream as content.
        /// </summary>
        /// <param name="stream"></param>
        public void SetContent(Stream stream)
        {
            StreamReader read = new StreamReader(stream);
            this.content = read.ReadToEnd();
        }

        /// <summary>
        /// Sends the response to the network stream.
        /// </summary>
        /// <param name="network"></param>
        public void Send(Stream network)
        {
            if ( StatusCode==200 && ContentLength==0  )
            {
                throw new NotImplementedException();
            }

            else
            {
                StreamWriter write = new StreamWriter(network,Encoding.UTF8);
                
                write.WriteLine($"HTTP/1.1 {Status}");
                write.WriteLine("Server: {0}", ServerHeader);
                foreach (var VARIABLE in Headers)
                {
                    write.WriteLine($"{VARIABLE.Key}: {VARIABLE.Value}"); 
                }
                    
                write.Write($"\n{this.content}");
                write.Flush();
            }
            
              
                
            
        }
    }
}