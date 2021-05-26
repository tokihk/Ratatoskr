using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;
using Ratatoskr.Device;
using Ratatoskr.FileFormat;
using Ratatoskr.PacketView;

namespace Ratatoskr.Plugin
{
    internal class PluginContext
    {
        public PluginContext(PluginClass plgc, PluginInterface plgp)
        {
            Class = plgc;
            Profile = plgp;
        }

        public PluginClass   Class   { get; }
        public PluginInterface Profile { get; }
    }
}
