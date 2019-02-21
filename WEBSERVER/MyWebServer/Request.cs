using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;
using System.IO;

/*
GET / HTTP/1.1
Host: localhost
Connection: keep-alive
Accept: text/html,application/xhtml+xml
User-Agent: Unit-Test-Agent/1.0 (The OS)
Accept-Encoding: gzip,deflate,sdch
Accept-Language: de-AT,de;q=0.8,en-US;q=0.6,en;q=0.4

*/
namespace MyWebServer
{
    public class Request : IRequest
    {
        private string content;
        public string reqheader;
        public List<string> url = new List<string>();

     
        public Request(Stream input)
        {
            try
            {
                StreamReader read = new StreamReader(input);
                string line;
                do
                {
                    line = read.ReadLine();
                    if (line != null && line.Contains("%20"))
                    {
                        line = line.Replace("%20", " ");
                    }
                    url.Add(line);
                    Console.WriteLine(line);
                    reqheader += url.Last();

                } while (!string.IsNullOrEmpty(line));


                if (Method == "POST" && ContentLength > 0)
                {

                    char[] buf = new char[ContentLength];
                    read.Read(buf, 0, ContentLength);
                    content = new string(buf);
                    Console.WriteLine("content:" + content);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }



        }


        /// <summary>
        /// Returns true if the request is valid. A request is valid, if method and url could be parsed. A header is not necessary.
        /// </summary>
        ///

        public bool IsValid
        {
            get
            {
                try
                {
                    if (url.First() == null)
                    {
                        return false;
                    }
                    else if (url.First().Contains("GET") || url.First().Contains("get") || url.First().Contains("post") ||
                         url.First().Contains("POST"))
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }


            }
        }


        /// <summary>
        /// Returns the request method in UPPERCASE. get -> GET.
        /// </summary>
        public string Method
        {
            get
            {
                if (!string.IsNullOrEmpty(url.First()))
                {


                    if (url.First().Contains("GET") || url.First().Contains("get"))
                    {
                        return "GET";
                    }
                    else if (url.First().Contains("POST") || url.First().Contains("post"))
                    {
                        return "POST";
                    }
                    else
                    {
                        return "error";
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns a URL object of the request. Never returns null.
        /// </summary>
        public IUrl raw;

        public IUrl Url
        {
            get
            {
                raw = new Url(url.First());
                return raw;
            }
        }

        /// <summary>
        /// Returns the request header. Never returns null. All keys must be lower case.
        /// </summary>
        private Dictionary<string, string> head1;

        public IDictionary<string, string> Headers
        {
            get
            {
                head1 = new Dictionary<string, string>();
                foreach (var VARIABLE in url)
                {
                    if (VARIABLE.Contains(':'))
                    {
                        head1.Add(VARIABLE.Split(':').First().Trim().ToLower(), VARIABLE.Split(':').Last().Trim());
                    }
                }

                return head1;
            }
        }

        /// <summary>
        /// Returns the user agent from the request header
        /// </summary>
        public string UserAgent
        {
            get { return Headers["user-agent"]; }
        }

        /// <summary>
        /// Returns the number of header or 0, if no header where found.
        /// </summary>
        public int HeaderCount
        {
            get { return Headers.Count; }
        }

        /// <summary>
        /// Returns the parsed content length request header.
        /// </summary>

        public int ContentLength
        {

            get
            {
                try
                {
                    return Convert.ToInt32(Headers["content-length"]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
        }

        /// <summary>
        /// Returns the parsed content type request header. Never returns null.
        /// </summary>
        public string ContentType
        {
            get { return Headers["content-type"]; }
        }

        /// <summary>
        /// Returns the request content (body) stream or null if there is no content stream.
        /// </summary>

        public Stream ContentStream
        {
            get
            {
                if (Method == "POST")
                {
                    byte[] s = UTF8Encoding.UTF8.GetBytes(content);
                    Stream b = new MemoryStream(s);
                    return b;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns the request content (body) as string or null if there is no content.
        /// </summary>
        public string ContentString
        {
            get { return content; }
        }

        /// <summary>
        /// Returns the request content (body) as byte[] or null if there is no content.
        /// </summary>
        public byte[] ContentBytes
        {
            get
            {
                byte[] byteArray = UTF8Encoding.UTF8.GetBytes(content);
                return byteArray;
            }
        }
    }
}