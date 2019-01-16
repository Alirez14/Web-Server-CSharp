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
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"),8080);
            listener.Start();
            while (true)
            {
                Console.Write("Waiting for a connection... ");
                TcpClient s = listener.AcceptTcpClient();
                Console.WriteLine("Connected!");
             

                // Get a stream object for reading and writing
                NetworkStream stream = s.GetStream();

              
                
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
                        
                        

                    };
                    resp.Send(stream);

                }
                
                s.Close();

            }
            
            

        }

        static void Main(string[] args)
        {
           Listen();
        }
    }
}
