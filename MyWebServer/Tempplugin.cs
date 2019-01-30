using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;
using System.Data.SqlClient;
using System.IO;

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

            return 0.0f;
        }

        public IResponse Handle(IRequest req)
        {
            var resp = new Response();
            Connection sql = new Connection("GetTemp");
            var con = sql.ConnectionString;

            SqlCommand command = sql.SqlCommand;
            List<string> data = new List<string>();

            if (CanHandle(req) != 0.0f)
            {
                string[] split = req.Url.RawUrl.Split('?');
                string[] getContent = split[1].Split('&');
                string[] date = getContent[0].Split('=');
                date[1] = date[1].Replace("\"", "'");
               
                command.CommandType = CommandType.StoredProcedure;

                var param = command.Parameters.Add("@DATE", con);
                param.Value = date[1];


                string path = req.Url.Path;
                path = path.Substring(1);
                string type = path.Substring(path.IndexOf('.'));

                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(reader[0].ToString());
                    }
                }
                else
                {
                    resp.StatusCode = 200;
                    resp.SetContent("No Content");
                    return resp;
                }

                if (File.Exists(path))
                {
                    try
                    {
                        resp.StatusCode = 200;
                        using (FileStream fs = new FileStream(path, FileMode.Open))
                        {
                            string content = string.Empty;
                            StreamReader read = new StreamReader(fs);
                            while (!read.EndOfStream)
                            {
                                content += read.ReadLine();
                            }

                            content = content.Replace("</body>", "<br>");
                            content += data[0];
                            resp.SetContent(content);
                            
                        }

                        resp.contenttype = Response.Typepmap[type];

                        
                        return resp;
                    }
                    catch
                    {
                        resp.StatusCode = 500;
                        return resp;
                    }
                }
            }
            return resp;

        }
    }
}

