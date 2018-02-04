using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Configs.LanguageConfigs
{
    internal sealed class PacketViewConfig : ConfigHolder
    {
        public PacketViews.Packet.ViewLanguageConfig Packet { get; } = new PacketViews.Packet.ViewLanguageConfig();
    }
}
