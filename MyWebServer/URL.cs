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
            if (string.IsNullOrEmpty(raw))
            {
                RawUrl = String.Empty;
            }
            else if (raw.Contains("GET") || raw.Contains("POST"))
            {
                string[] completeurl = raw.Split(' ');
                RawUrl = completeurl[1];
            }
            else
            {
                RawUrl = raw;
            }
        }

        private Dictionary<string, string> result = null;

        public IDictionary<string, string> Parameter
        {
            get
            {
                ensureprameter();
                return result;
            }
        }

        private void ensureprameter()
        {
            if (result == null)
            {
                result = new Dictionary<string, string>();
                //"/test.jpg?x=1&y=2"

                if (RawUrl != null)
                {
                    if (RawUrl.Contains('?'))
                    {
                        int index = RawUrl.IndexOf('?');
                        string sub = RawUrl.Substring(index + 1);
                        string[] prams = sub.Split('&');
                        for (int i = 0; i < prams.Length; i++)
                        {
                            string[] pairs = prams[i].Split('=');
                            result.Add(pairs[0], pairs[1]);
                        }
                    }
                }
            }
        }

        public int ParameterCount
        {
            get
            {
                ensureprameter();
                return result.Count();
            }
        }

        public string[] noParam = new string[5];

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
                    if (RawUrl.Contains('#') && !RawUrl.Contains('.'))
                    {
                        string[] path = RawUrl.Split('#');

                        if (ParameterCount == 0)
                        {
                            return path[0];
                        }
                        else
                        {
                            int i = path[0].IndexOf('?');
                            string path2 = path[0].Substring(0, i);
                            return path2;
                        }
                    }
                    else if (RawUrl.Contains('#') && RawUrl.Contains('.'))
                    {
                        if (RawUrl.IndexOf('.') > RawUrl.IndexOf('#'))
                        {
                            string path = RawUrl;

                            if (ParameterCount == 0)
                            {
                                return path;
                            }
                            else
                            {
                                int i = path.IndexOf('?');
                                string path2 = path.Substring(0, i);
                                return path2;
                            }
                        }
                        else
                        {
                            string[] path = RawUrl.Split('#');

                            if (ParameterCount == 0)
                            {
                                return path[0];
                            }
                            else
                            {
                                int i = path[0].IndexOf('?');
                                string path2 = path[0].Substring(0, i);
                                return path2;
                            }
                        }
                    }
                    else
                    {
                        if (ParameterCount == 0)
                        {
                            return RawUrl;
                        }
                        else
                        {
                            int i = RawUrl.IndexOf('?');
                            string path = RawUrl.Substring(0, i);
                            return path;
                        }
                    }
                }
            }
        }

        public string RawUrl { get; }

        public string Extension
        {
            get { throw new NotImplementedException(); }
        }

        public string FileName
        {
            get
            {
                string file = Segments.Last();
                if (file.Contains('.'))

                {
                    return file;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public string Fragment
        {
            get
            {
                int index = RawUrl.IndexOf("#");
                string sub = RawUrl.Substring(index + 1);
                return sub;
            }
        }

        public string[] Segments
        {
            get
            {
                char[] delimiters = new char[] {'/'};
                string[] _segment = RawUrl.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                return _segment;
            }
        }
    }
}