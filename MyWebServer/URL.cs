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
    }

    public int ParameterCount
    {
        get
            {

                ensureprameter();
                return result.Count();

            }
    }
    public string[] noParam=new string[5];
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
                        if (RawUrl.Contains('#'))
                            {
                                noParam= RawUrl.Split('#');
                                return noParam[0];
                            }
                        else
                            {
                                noParam = RawUrl.Split('?');
                                return noParam[0];
                            }
                    }
            }
    }

    public string RawUrl
    {



        get;

    }

    public string Extension
    {
        get
            {
                throw new NotImplementedException();
            }
    }

    public string FileName
    {
        get
            {
                throw new NotImplementedException();
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
                string[] str = RawUrl.Split('/');
                str = str.Where(w => w != str[0]).ToArray();

                return str;
            }
    }
}
}
