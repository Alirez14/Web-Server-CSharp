using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIF.SWE1.Interfaces
{
    public interface IPluginManager
    {
        /// <summary>
        /// Returns a list of all plugins. Never returns null.
        /// </summary>
        IEnumerable<IPlugin> Plugins { get; }

        /// <summary>
        /// Adds a new plugin. If the plugin was already added, nothing will happen.
        /// </summary>
        /// <param name="plugin"></param>
        void Add(IPlugin plugin);

        /// <summary>
        /// Adds a new plugin by type name. If the plugin was already added, nothing will happen.
        /// Throws an exeption, when the type cannot be resoled or the type does not implement IPlugin.
        /// </summary>
        /// <param name="plugin"></param>
        void Add(string plugin);

        /// <summary>
        /// Clears all plugins
        /// </summary>
        void Clear();
    }
}
