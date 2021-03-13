using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;
using RtsCore.Protocol;
using RtsCore.Framework.Device;
using RtsCore.Framework.FileFormat;
using RtsCore.Framework.PacketView;

namespace RtsCore.Framework.Plugin
{
    public class PluginContext
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
