using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace MyWebServer
{
    class Tempplugin : IPlugin
    {
        public float CanHandle(IRequest req)
        {

            if (req.Url.RawUrl.Contains("date="))
            {
                return 1f;
            }

            if (req.Url.RawUrl.Contains("GetTemperature"))
            {
                return 1f;
            }

            return 0.0f;
        }

        public IResponse Handle(IRequest req)
        {
            var resp = new Response();
            if (CanHandle(req) != 0)
            {
                //Get the Date from the URL 

                #region FindUrl

                string date = String.Empty;

                if (req.Url.RawUrl.Contains("date="))
                {
                    string[] split = req.Url.RawUrl.Split('?');
                    string[] getContent = split[1].Split('&');
                    string[] dateSplit = getContent[0].Split('=');
                    date = dateSplit[1];
                }

                if (req.Url.RawUrl.Contains("GetTemperature"))
                {
                    string[] split = req.Url.RawUrl.Split('/');
                    date = split[2];
                }

                #endregion

                //Find The Path 

                #region FindPath

                string path = String.Empty;
                string type = String.Empty;

                if (req.Url.RawUrl.Contains("date"))
                {
                    path = req.Url.Path;
                    path = path.Substring(1);
                    type = path.Substring(path.IndexOf('.'));
                }

                #endregion

                //Create a SQL Connection

                #region Connection


                var con = new SqlConnection(
                    @"Data Source=(local)\SQLEXPRESS;Initial Catalog=dotnet;Integrated Security=True");

                con.Open();
                var command = new SqlCommand("GetTemp", con);
                command.CommandTimeout = 180000;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DATE", date);
                var reader = command.ExecuteReader();

                #endregion

                //Save data into Xml

                #region SaveXml

                if (File.Exists(@"..\SWE\temp.xml"))
                {

                    File.Delete(@"..\SWE\temp.xml");
                }

                XmlDocument xml = new XmlDocument();
                if (reader.HasRows)
                {
                    XmlNode root = xml.CreateElement("Temperatur");
                    xml.AppendChild(root);

                    XmlNode weather = xml.CreateElement("Weather");
                    root.AppendChild(weather);

                    while (reader.Read())
                    {

                        XmlNode day = xml.CreateElement("date");
                        day.InnerText = reader[0].ToString().Split(' ')[0];
                        weather.AppendChild(day);

                        XmlNode temp = xml.CreateElement("temp");
                        temp.InnerText = reader[1].ToString();
                        weather.AppendChild(temp);

                        XmlNode status = xml.CreateElement("status");
                        status.InnerText = reader[2].ToString();
                        weather.AppendChild(status);

                    }

                    root.AppendChild(weather);

                    xml.Save(@"..\SWE\temp.xml");

                }

                else
                {
                    resp.StatusCode = 404;
                    resp.SetContent("No Data has been found");
                    con.Close();
                    return resp;
                }
                #endregion

                //Load the Xml File && set Content

                #region LoadXml & SetContent      

                string content = String.Empty;

                if (req.Url.RawUrl.Contains("date=") && File.Exists(path))
                {

                    try
                    {
                        resp.StatusCode = 200;
                        using (FileStream fs = new FileStream(path, FileMode.Open))
                        {
                            //string content = string.Empty;
                            StreamReader read = new StreamReader(fs);
                            while (!read.EndOfStream)
                            {
                                content += read.ReadLine();
                            }

                            List<string> xmlToString = new List<string>();
                            using (XmlTextReader xmlReader = new XmlTextReader(@"..\SWE\temp.xml"))
                            {
                                xmlReader.WhitespaceHandling = WhitespaceHandling.None;
                                xmlReader.MoveToContent();
                                xmlToString.Clear();

                                while (xmlReader.Read())
                                {
                                    if (xmlReader.Value == "")
                                    {
                                        continue;
                                    }

                                    xmlToString.Add(xmlReader.Value);
                                }
                            }

                            if (xmlToString.Count != 0)
                            {
                                string[] javaArray = xmlToString.ToArray();

                                content += "<table><tr><th>Date</th><th>Temperature</th><th>Status</th>" +
                                           "<tr><td>" + xmlToString.First() + "</td>" +
                                           "<td>" + xmlToString[1] + "</td>" + "<td>" + xmlToString[2] + "</td></tr></table>" +
                                           "<br> <button style = \"margin:auto; display:block;\" onclick = nextResult(" + javaArray + ")" + ">Next Result </button>";
                            }

                            else
                            {
                                resp.SetContent("Not Found");
                                con.Close();
                                return resp;
                            }

                            resp.ContentType = type;
                            resp.SetContent(content);
                            con.Close();
                            return resp;
                        }
                    }
                    catch
                    {
                        resp.StatusCode = 500;
                        return resp;
                    }
                }

                else if (req.Url.RawUrl.Contains("GetTemperature") && File.Exists(@"..\SWE\temp.xml"))
                {
                    var xmlDoc = XElement.Load(@"..\SWE\temp.xml");
                    content += xmlDoc;
                    resp.SetContent(content);
                    resp.ContentType = "text/xml";
                    resp.StatusCode = 200;
                    con.Close();
                    return resp;
                }
                else
                {
                    resp.StatusCode = 200;
                    resp.SetContent("Data Has not been Found, Pick another date");
                    con.Close();
                    return resp;
                }

            }
            #endregion

            return null;

        }
    }
}
