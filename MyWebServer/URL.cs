using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class Url : IUrl
    {
        public Url()
        {
        }

        public Url(string raw)
        {
            RawUrl = raw;
        }

        private Dictionary<string, string> result;
        public IDictionary<string, string> Parameter
        {

            get
            {
                if (result == null)
                {
                    result = new Dictionary<string, string>();
                    //"/test.jpg?x=1&y=2"

                    if (RawUrl != null)
                    {


                        int index = RawUrl.IndexOf('?');
                        string sub = RawUrl.Substring(index + 1);
                        for (int i = 0; i < sub.Length; i++)
                        {
                            int parametrs = i - 1;
                            int value = i + 1;
                            if (sub[i] == '=')
                            {
                                result.Add(sub[parametrs].ToString(), sub[value].ToString());

                            }
                        }

                    }

                }
                return result;

            }
        }

        public int ParameterCount
        {
            get
            {

                
                return Parameter.Count();
                
            }
        }

        public string Path
        {
            get
            {
                if (String.IsNullOrEmpty(RawUrl))
                {
                    return null;
                }
                else
                {
                    string[] noParam = RawUrl.Split('?');
                    return noParam[0];
                }
            }
        }

        public string RawUrl
        {



            get;
            
        }

        public string Extension
        {
            get { throw new NotImplementedException(); }
        }

        public string FileName
        {
            get { throw new NotImplementedException(); }
        }

        public string Fragment
        {
            get { throw new NotImplementedException(); }
        }

        public string[] Segments
        {
            get
            {
                string[] str = RawUrl.Split('/');
                return str;
            }
        }
    }
}