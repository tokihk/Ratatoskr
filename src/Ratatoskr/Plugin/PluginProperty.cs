using Ratatoskr.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Plugin
{
    public abstract class PluginProperty : ConfigHolder
    {
        public PluginProperty() : base()
        {
        }

        public abstract PluginProperty Clone();

        public abstract PluginPropertyEditor GetPropertyEditor();
    }
}
