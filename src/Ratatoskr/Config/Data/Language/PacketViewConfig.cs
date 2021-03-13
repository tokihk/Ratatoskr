using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Config;

namespace Ratatoskr.Config.Data.Language
{
    internal sealed class PacketViewConfig : ConfigHolder
    {
        public PacketView.Packet.PacketViewLanguageConfig Packet { get; } = new PacketView.Packet.PacketViewLanguageConfig();
    }
}
