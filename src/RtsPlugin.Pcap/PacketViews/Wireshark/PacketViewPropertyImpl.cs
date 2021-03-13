using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config.Types;
using RtsCore.Framework.PacketView;
using RtsCore.Generic;

namespace RtsPlugin.Pcap.PacketViews.Wireshark
{
    internal enum LibPcapLinkType
    {
        Ethernet                = 1,
        Custom                  = 147,
    }

    internal class PacketViewPropertyImpl : PacketViewProperty
    {
        public EnumConfig<LibPcapLinkType> LinkType    { get; } = new EnumConfig<LibPcapLinkType>(LibPcapLinkType.Custom);


        public override PacketViewProperty Clone()
        {
            return (ClassUtil.Clone<PacketViewPropertyImpl>(this));
        }
    }
}
