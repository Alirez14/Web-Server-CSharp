﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BIF.SWE1.Interfaces
{
public class Response : IResponse
{
    public Response()
    {


    }
    /// <summary>
    /// Returns a writable dictionary of the response headers. Never returns null.
    /// </summary>
    public Dictionary<string, string> head = null;
    public IDictionary<string, string> Headers
    {
        get
            {
                if (head == null)
                    {
                        return null;
                    }
                else
                    {
                        return head;
                    }
            }

    }

    /// <summary>
    /// Returns the content length or 0 if no content is set yet.
    /// </summary>
    public int ContentLength
    {
        get;
    }

    /// <summary>
    /// Gets or sets the content type of the response.
    /// </summary>
    /// <exception cref="InvalidOperationException">A specialized implementation may throw a InvalidOperationException when the content type is set by the implementation.</exception>
    public string ContentType
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the current status code. An Exceptions is thrown, if no status code was set.
    /// </summary>

    public int code;

    public int StatusCode
    {
        get
            {
                if (code == 0)
                {
                    throw new Exception("Error");
                }
                return code;
            }

        set
            {
                try
                    {
                        code = value;
                        if (code == 0)
                            {
                                throw new Exception("Error");
                            }

                    }
                finally
                    {
                        code = value;
                    }
            }
    }
    /// <summary>
    /// Returns the status code as string. (200 OK)
    /// </summary>
    public string Status
    {
        get
            {

                string mass;
                Exception mas;

                switch (StatusCode)
                    {
                    case 200:
                    {

                        mass = "200 OK";
                        return mass;

                    }
                    case 404:
                    {

                        mas = new Exception("404 NOT FOUND");
                        return mas.Message;
                    }
                    case 500:
                    {
                        mas= new Exception("500 INTERNAL SERVER ERROR");
                        return mas.Message;


                    }
                    default:
                    {
                        mas = new Exception("ERROR");
                        return mas.Message;
                    }

                    }



            }


    }

    /// <summary>
    /// Adds or replaces a response header in the headers dictionary.
    /// </summary>
    /// <param name="header"></param>
    /// <param name="value"></param>

    public void AddHeader(string header, string value)
    {

        head = new Dictionary<string, string>();
        head.Add(header, value);
    }


    /// <summary>
    /// Gets or sets the Server response header. Defaults to "BIF-SWE1-Server".
    /// </summary>
    public string ServerHeader
    {
        get;
        set;
    }

    /// <summary>
    /// Sets a string content. The content will be encoded in UTF-8.
    /// </summary>
    /// <param name="content"></param>
    public void SetContent(string content)
    {

    }
    /// <summary>
    /// Sets a byte[] as content.
    /// </summary>
    /// <param name="content"></param>
    public void SetContent(byte[] content)
    {

    }
    /// <summary>
    /// Sets the stream as content.
    /// </summary>
    /// <param name="stream"></param>
    public void SetContent(Stream stream)
    {

    }

    /// <summary>
    /// Sends the response to the network stream.
    /// </summary>
    /// <param name="network"></param>
    public void Send(Stream network)
    {

    }
}
}