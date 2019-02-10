using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BIF.SWE1.Interfaces;
using Microsoft.SqlServer.Server;

namespace MyWebServer
{
    class Naviplugin : IPlugin
    {
        /// <summary>
        /// Returns a score between 0 and 1 to indicate that the plugin is willing to handle the request. The plugin with the highest score will execute the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A score between 0 and 1</returns>
        public float CanHandle(IRequest req)
        {
            if (req.Method == "POST" && req.ContentString.Contains("street="))
            {
                return 1f;
            }

            return 0.0f;
        }


        public List<string> TestReader(string req)
        {
            List<string> adress = new List<string>();


            using (XmlReader reader =
                XmlReader.Create(@"C:\Users\Alirez\Desktop\SWE\brandenburg\brandenburg-latest.osm"))
            {
                while ( reader.Read())
                {

                    if ((reader.Name == "node" || reader.Name == "way") && reader.NodeType == XmlNodeType.Element)
                    {
                        string id = reader.GetAttribute("id");
                        using (XmlReader childtag = reader.ReadSubtree())
                        {
                            
                            while (childtag.Read())
                            {
                                if (childtag.Name == "tag" && reader.NodeType == XmlNodeType.Element)
                                {
                                    string key = childtag.GetAttribute("k");
                                    string value = childtag.GetAttribute("v");
                                   
                                    if (value.Contains(req) )
                                    {
                                        adress.Add("place with key : " + key + " and value :" + value +
                                                   "is in Germany place id: " +
                                                   id);
                                    }
                                }
                            }
                        }
                    }
                }
                
            }

            return adress;
        }

        /// <summary>
        /// Called by the server when the plugin should handle the request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A new response object.</returns>
        public IResponse Handle(IRequest req)
        {
            try
            {
               
                if (CanHandle(req) != 0.0f)
                {
                    Response resp;
                    string adress = req.ContentString;
                    adress = adress.Substring(7);
                  var result =  TestReader(adress);
                  if (result.Any())
                  {
                      string cont = string.Empty;
                      foreach (string s in result)
                      {
                          cont += s + "\n";
                      }

                       resp = new Response()
                      {
                          StatusCode = 200,
                          ContentType = Response.Typepmap[".html"],
                          content = cont


                      };
                  }
                  else
                  {
                       resp = new Response()
                      {
                          StatusCode = 200,
                          ContentType = Response.Typepmap[".html"],
                          content = "no data found"


                      };
                  }


                  return resp;
                }
                else
                {
                    return null;
                }
               
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}