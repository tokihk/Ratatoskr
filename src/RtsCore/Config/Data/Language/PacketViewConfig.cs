using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RtsCore.Config;

namespace RtsCore.Framework.Config.Data.Language
{
    public sealed class PacketViewConfig : ConfigHolder
    {
        public PacketViews.Packet.ViewLanguageConfig Packet { get; } = new PacketViews.Packet.ViewLanguageConfig();
    }
}
