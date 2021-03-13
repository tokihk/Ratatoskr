using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Generic;
using RtsCore.Framework.Plugin;

namespace RtsPlugin.Pcap
{
    public class PluginPropertyImpl : PluginProperty
    {
		public override PluginProperty Clone()
        {
            return (ClassUtil.Clone<PluginPropertyImpl>(this));
        }

        public override PluginPropertyEditor GetPropertyEditor()
        {
            return (new PluginPropertyEditorImpl(this));
        }
    }
}
