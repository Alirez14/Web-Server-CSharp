using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using BIF.SWE1.Interfaces;

namespace MyWebServer
{
    public class PluginManager : IPluginManager
    {
        private IList<IPlugin> plugins = new List<IPlugin>();

        public PluginManager()
        {
            Assembly a =   typeof(PluginManager).Assembly;



            foreach (var type in a.GetTypes())
            {
                if (typeof(IPlugin).IsAssignableFrom(type))
                {
                    plugins.Add((IPlugin) Activator.CreateInstance(type));
                }
            }
        }


        /// <summary>
        /// Returns a list of all plugins. Never returns null.
        /// </summary>
        public IEnumerable<IPlugin> Plugins
        {
            get { return plugins; }
        }

        /// <summary>
        /// Adds a new plugin. If the plugin was already added, nothing will happen.
        /// </summary>
        /// <param name="plugin"></param>
        public void Add(IPlugin plugin)
        {
            if (!plugins.Contains(plugin))
            {
                plugins.Add(plugin);
            }
        }

        /// <summary>
        /// Adds a new plugin by type name. If the plugin was already added, nothing will happen.
        /// Throws an exeption, when the type cannot be resolved or the type does not implement IPlugin.
        /// </summary>
        /// <param name="plugin"></param>
        public void Add(string plugin)
        {
            Type t = Type.GetType(plugin);
            plugins.Add((IPlugin)Activator.CreateInstance(t));  
        }

        /// <summary>
        /// Clears all plugins
        /// </summary>
        public void Clear()
        {
            plugins.Clear();
        }
    }
}