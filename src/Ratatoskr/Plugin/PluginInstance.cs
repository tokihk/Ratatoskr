using Ratatoskr.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Plugin
{
    internal class PluginInstance
    {
        public PluginInstance(PluginClass plgc)
        {
            Class    = plgc;
        }

        public PluginClass     Class     { get; }
        public PluginInterface Interface { get; } = new PluginInterface();
        public PluginProperty  Property  { get; private set; }

        
        public void LoadProperty(PluginProperty plgp)
        {
            if (plgp == null)return;

            Property = plgp;

            OnLoadProperty();
        }

        protected virtual void OnLoadProperty()
        {
        }
    }
}
