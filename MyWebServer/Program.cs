using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    class Program
    {
        private static IPlugin SelectPlugin(IPluginManager mgr, IRequest req)
        {
            IPlugin plugin = null;
            float max = 0;
            foreach (var p in mgr.Plugins)
            {
                float canHandle = p.CanHandle(req);
                if (canHandle > max)
                {
                    max = canHandle;
                    plugin = p;
                }
            }

            return plugin;
        }
        private static void Listen()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8081);
            listener.Start();
            while (true)
            {
                Socket s = listener.AcceptSocket();
                NetworkStream stream = new NetworkStream(s);
                Request req = new Request(stream);
                Console.WriteLine(req.reqheader);
                PluginManager ipm =new PluginManager();
                IPlugin plug = SelectPlugin(ipm, req);

                if (plug != null)
                {
                    IResponse rep = plug.Handle(req);
                    rep.Send(stream);
                }
                else
                {
                    IResponse resp = new Response()
                    {
                        StatusCode = 404,
                        ContentType = "text/html",
                        icontent = "error",
                        

                    };
                    resp.Send(stream);

                }

/*                StreamReader sr = new StreamReader(stream);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                    if (string.IsNullOrEmpty(line)) break;
                }

                StreamWriter sw = new StreamWriter(stream);
                var body = "<html><body><h1>Hello World!</h1><p>a Text</p></body></html>";
                sw.WriteLine("HTTP/1.1 200 OK");
                sw.WriteLine("connection: close");
                sw.WriteLine("content-type: text/html");
                sw.WriteLine("content-length: " + body.Length);
                sw.WriteLine();
                sw.Write(body);
                sw.Flush();
                s.Close();*/
            }

        }

        static void Main(string[] args)
        {
           Listen();
        }
    }
}
