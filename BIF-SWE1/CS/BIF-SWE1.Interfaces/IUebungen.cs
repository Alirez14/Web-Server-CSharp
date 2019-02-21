using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BIF.SWE1.Interfaces
{
    public interface IUEB1
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();

        /// <summary>
        /// Must return a IUrl implementation. A valid, invalid, empty or null url may be passed. This method must not fail in any case.
        /// </summary>
        /// <param name="path">A url. May be valid, invalid, empty or null.</param>
        /// <returns>A IUrl implementation.</returns>
        IUrl GetUrl(string url);
    }
    public interface IUEB2
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();

        /// <summary>
        /// Must return a IUrl implementation. A valid, invalid, empty or null url may be passed. This method must not fail in any case.
        /// </summary>
        /// <param name="path">A url. May be null.</param>
        /// <returns>A IUrl implementation.</returns>
        IUrl GetUrl(string url);

        /// <summary>
        /// Must return a IRequest implementation. A valid, invalid or empty (containing one empty line) stream may be passed. This method must not fail in any case.
        /// </summary>
        /// <param name="network">A stream simulating the network.</param>
        /// <returns>A IRequest implementation</returns>
        IRequest GetRequest(Stream network);

        /// <summary>
        /// Must return a empty IResponse implementation. This method must not fail.
        /// </summary>
        /// <returns>A IResponse implementation</returns>
        IResponse GetResponse();
    }
    public interface IUEB3
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();

        /// <summary>
        /// Must return a IRequest implementation. A valid, invalid or empty (containing one empty line) stream may be passed. This method must not fail in any case.
        /// </summary>
        /// <param name="network">A stream simulating the network.</param>
        /// <returns>A IRequest implementation</returns>
        IRequest GetRequest(Stream network);

        /// <summary>
        /// Must return a empty IResponse implementation. This method must not fail.
        /// </summary>
        /// <returns>A IResponse implementation</returns>
        IResponse GetResponse();

        /// <summary>
        /// Must return a first/test/mock plugin implementation. This method must not fail.
        /// </summary>
        /// <returns>A IPlugin implementation</returns>
        IPlugin GetTestPlugin();
    }
    public interface IUEB4
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();

        /// <summary>
        /// Must return a IRequest implementation. A valid, invalid or empty (containing one empty line) stream may be passed. This method must not fail in any case.
        /// </summary>
        /// <param name="network">A stream simulating the network.</param>
        /// <returns>A IRequest implementation</returns>
        IRequest GetRequest(Stream network);

        /// <summary>
        /// Must return a empty IResponse implementation. This method must not fail.
        /// </summary>
        /// <returns>A IResponse implementation</returns>
        IResponse GetResponse();

        /// <summary>
        /// Must return a IPluginManager implementation. This method must not fail.
        /// </summary>
        /// <returns>A IPluginManager implementation</returns>
        IPluginManager GetPluginManager();
    }
    public interface IUEB5
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();

        /// <summary>
        /// Must return a IRequest implementation. A valid, invalid or empty (containing one empty line) stream may be passed. This method must not fail in any case.
        /// </summary>
        /// <param name="network">A stream simulating the network.</param>
        /// <returns>A IRequest implementation</returns>
        IRequest GetRequest(Stream network);

        /// <summary>
        /// Must return a IPluginManager implementation. This method must not fail.
        /// </summary>
        /// <returns>A IPluginManager implementation</returns>
        IPluginManager GetPluginManager();

        /// <summary>
        /// Must return a static file plugin implementation. This method must not fail.
        /// </summary>
        /// <returns>A IPlugin implementation</returns>
        IPlugin GetStaticFilePlugin();

        /// <summary>
        /// Sets the folder path relative to the current working directory where test files are located. These files should be handles by the static file plugin.
        /// </summary>
        /// <param name="folder"></param>
        void SetStatiFileFolder(string folder);

        /// <summary>
        /// Returns a valid url for the static file plugin. The plugin should be able to return the given file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>A valid URL</returns>
        string GetStaticFileUrl(string fileName);
    }
    public interface IUEB6
    {
        /// <summary>
        /// This method is only called to prove the unit test setup.
        /// </summary>
        void HelloWorld();

        /// <summary>
        /// Must return a IRequest implementation. A valid, invalid or empty (containing one empty line) stream may be passed. This method must not fail in any case.
        /// </summary>
        /// <param name="network">A stream simulating the network.</param>
        /// <returns>A IRequest implementation</returns>
        IRequest GetRequest(Stream network);

        /// <summary>
        /// Must return a IPluginManager implementation. This method must not fail.
        /// </summary>
        /// <returns>A IPluginManager implementation</returns>
        IPluginManager GetPluginManager();

        /// <summary>
        /// Must return a temperature plugin implementation. This method must not fail.
        /// </summary>
        /// <returns>A IPlugin implementation</returns>
        IPlugin GetTemperaturePlugin();

        /// <summary>
        /// Must return a navigation plugin implementation. This method must not fail.
        /// </summary>
        /// <returns>A IPlugin implementation</returns>
        IPlugin GetNavigationPlugin();

        /// <summary>
        /// Must return a ToLower plugin implementation. This method must not fail.
        /// </summary>
        /// <returns>A IPlugin implementation</returns>
        IPlugin GetToLowerPlugin();

        /// <summary>
        /// Returns a valid url for the temperature plugin. The plugin should be able to return a page showing temperature data of the given date range.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>A valid URL</returns>
        string GetTemperatureUrl(DateTime from, DateTime until);

        /// <summary>
        /// Returns a valid url for the temperature plugin. The plugin should be able to return a xml document containing temperature data of the given date range.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>A valid URL</returns>
        string GetTemperatureRestUrl(DateTime from, DateTime until);

        /// <summary>
        /// Returns a valid url for the temperature plugin. The plugin should be able to return a page showing all cities where a posted street exists. The name of the posted field will be "street".
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>A valid URL</returns>
        string GetNaviUrl();

        /// <summary>
        /// Returns a valid url for the ToLower plugin. The plugin should be able to return a page showing a posted text lowercase. The name of the posted field will be "text".
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>A valid URL</returns>
        string GetToLowerUrl();
    }
}
