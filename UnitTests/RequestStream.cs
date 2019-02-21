using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class RequestStream
    {
        public Stream ValidRequest(string url, string method = "GET", string host = "localhost", string body = null)
        {
            byte[] bodyBytes = null;
            if (body != null)
            {
                bodyBytes = Encoding.UTF8.GetBytes(body);
            }

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms, Encoding.ASCII);

            sw.WriteLine("{0} {1} HTTP/1.1", method, url);
            sw.WriteLine("Host: {0}", host);
            sw.WriteLine("Connection: keep-alive");
            sw.WriteLine("Accept: text/html,application/xhtml+xml");
            sw.WriteLine("User-Agent: Unit-Test-Agent/1.0 (The OS)");
            sw.WriteLine("Accept-Encoding: gzip,deflate,sdch");
            sw.WriteLine("Accept-Language: de-AT,de;q=0.8,en-US;q=0.6,en;q=0.4");

            if (bodyBytes != null)
            {
                sw.WriteLine(string.Format("Content-Length: {0}", bodyBytes.Length));
                sw.WriteLine("Content-Type: application/x-www-form-urlencoded");
            }
            sw.WriteLine();

            if (bodyBytes != null)
            {
                sw.Flush();
                ms.Write(bodyBytes, 0, bodyBytes.Length);
            }

            sw.Flush();

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static Stream GetInvalidRequestStream()
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms, Encoding.ASCII);

            sw.WriteLine("GET");
            sw.WriteLine();
            sw.Flush();

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static Stream GetEmptyRequestStream()
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms, Encoding.ASCII);

            sw.WriteLine();
            sw.Flush();

            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
