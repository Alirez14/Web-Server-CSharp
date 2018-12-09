using System;
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
        private string curl;
        private string[] url = new string[100];


        public Request(Stream input)
        {
            StreamReader read;
            int i = 0;
            read = new StreamReader(input);

            while (!read.EndOfStream)
            {
                url[i] = read.ReadLine();
                curl += url[i] + "\n";
                i++;
            }
        }

        public string filename
        {
            get
            {
             string [] file=   url[0].Split(' ');
                return file[1];

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
                if (curl.Contains('/') == false)
                {
                    return false;
                }
                else
                {
                    if (Method == "error")
                    {
                        return false;
                    }

                    return true;
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
                if (curl.Contains("GET") || curl.Contains("get"))
                {
                    return "GET";
                }
                else if (curl.Contains("POST") || curl.Contains("post"))
                {
                    return "POST";
                }
                else
                {
                    return "error";
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
                string[] link = url[0].Split('/');
                string linkdone;

                if (link[1] == " HTTP" || link[1] == "test")
                {
                    linkdone = "/";
                }

                else
                {
                    if (link[1].Contains(" "))
                    {
                        linkdone = "/" + link[1].Substring(0, link[1].IndexOf(' '));
                    }
                    else
                    {
                        linkdone = link[1];
                    }
                }

                
                raw = new Url(linkdone);

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
                for (int i = 0; i < 10; i++)
                {
                    if ((url[i] != null) && (url[i].Contains(':')))
                    {
                        string[] url1 = url[i].Split(':');
                        url1[1] = url1[1].Trim();


                        head1.Add(url1[0].ToLower(), url1[1]);
                    }
                    else
                    {
                        continue;
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
            get { return url[10].Length; }
        }

        /// <summary>
        /// Returns the parsed content type request header. Never returns null.
        /// </summary>
        public string ContentType
        {
            get { return url[8].Substring(14); }
        }

        /// <summary>
        /// Returns the request content (body) stream or null if there is no content stream.
        /// </summary>

        public Stream ContentStream
        {
            get
            {
                // convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(url[10]);
//byte[] byteArray = Encoding.ASCII.GetBytes(contents);
                Stream stream = new MemoryStream(byteArray);
                return stream;
            }
        }

        /// <summary>
        /// Returns the request content (body) as string or null if there is no content.
        /// </summary>
        public string ContentString
        {
            get { return url[10]; }
        }

        /// <summary>
        /// Returns the request content (body) as byte[] or null if there is no content.
        /// </summary>
        public byte[] ContentBytes
        {
            get
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(url[10]);
                return byteArray;
            }
        }
    }
}