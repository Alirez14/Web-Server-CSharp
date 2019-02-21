using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE1.Interfaces
{
    public interface IUrl
    {
        /// <summary>
        /// Returns the raw url.
        /// </summary>
        string RawUrl { get; }

        /// <summary>
        /// Returns the path of the url, without parameter.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Returns a dictionary with the parameter of the url. Never returns null.
        /// </summary>
        IDictionary<string, string> Parameter { get; }

        /// <summary>
        /// Returns the number of parameter of the url. Returns 0 if there are no parameter.
        /// </summary>
        int ParameterCount { get; }

        /// <summary>
        /// Returns the segments of the url path. A segment is divided by '/' chars. Never returns null.
        /// </summary>
        string[] Segments { get; }

        /// <summary>
        /// Returns the filename (with extension) of the url path. If the url contains no filename, a empty string is returned. Never returns null. A filename is present in the url, if the last segment contains a name with at least one dot.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Returns the extension of the url filename, including the leading dot. If the url contains no filename, a empty string is returned. Never returns null.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Returns the url fragment. A fragment is the part after a '#' char at the end of the url. If the url contains no fragment, a empty string is returned. Never returns null.
        /// </summary>
        string Fragment { get; }
    }
}
