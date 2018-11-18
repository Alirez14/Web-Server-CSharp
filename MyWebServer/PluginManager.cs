using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE1.Interfaces;
namespace MyWebServer
{
    public class PluginManager : IPluginManager
    {
        private List<IPlugin> plugins = new List<IPlugin>();
       public PluginManager()
       {
           
            IPlugin plug =new Plugin();
            plugins.Add(plug);
        }

        
        

        /// <summary>
            /// Returns a list of all plugins. Never returns null.
            /// </summary>
           public IEnumerable<IPlugin> Plugins {
            get { return plugins; } }

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