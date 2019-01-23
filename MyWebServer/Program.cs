using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public static void RequestHandler(TcpClient myClient)
        {
            MyWebServer.Request myRequest = new Request(myClient.GetStream());
            if (myRequest.IsValid)
            {
        
                IPlugin currentPlugin = new Staticfileplugin();
                PluginManager myPluginManager = new PluginManager();

                currentPlugin = SelectPlugin(myPluginManager, myRequest);

                IResponse myResponse = currentPlugin.Handle(myRequest);
                try
                {
                    myResponse.Send(myClient.GetStream()); //Antwort senden
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Error sending Response to Client: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("No valid request");
            }
        }

        private static async Task Listen(TcpClient myClient)
        {
            try
            {
               await  Task.Run(() => RequestHandler(myClient));
            }
            catch (Exception e)
            {
                Console.WriteLine("Responsehandler: " + e);
            }

            //await macht hier aus der asynchronen Funktion Task.Run() wieder eine synchrone Funktion (Request snychron empfangen)
            myClient.Close();
        }

        public static async Task ListeningAsync(TcpListener myListener)
        {
            while (true)
            {
                TcpClient myClient = await myListener.AcceptTcpClientAsync(); //asynchron akzeptieren für Multithreading
                Console.WriteLine("Client connected"+myClient.Client.RemoteEndPoint);
                Listen(myClient);
            }
        }


        static void Main(string[] args)
        {
            TcpListener myServer = null;
            try
            {
                Thread t = new Thread(()=> ListeningAsync(myServer));
                t.Start();

                Int32 myPort = 8080;
                IPAddress myIPAddress = IPAddress.Parse("127.0.0.1");
                myServer = new TcpListener(myIPAddress, myPort); //Unser Socket

                myServer.Start(); //Server starten
                Console.WriteLine("Server is running...");

                ListeningAsync(myServer);
            }
            catch (SocketException e)
            {
                Console.WriteLine($"SocketException: {e}");
            }

            //Wenn man Enter drückt, bekommt man eine schöne Trennzeile
            while (Console.Read() != -1)
            {
                Console.WriteLine("----------------------------------------------");
            }
        }
    }
}