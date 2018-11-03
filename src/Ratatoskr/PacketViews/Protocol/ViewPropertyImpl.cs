using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.PacketViews.Protocol.Configs;
using RtsCore.Config.Types;
using RtsCore.Framework.PacketView;
using RtsCore.Generic;

namespace Ratatoskr.PacketViews.Protocol
{
    internal class PacketViewPropertyImpl : PacketViewProperty
    {
        public GuidConfig                ProtocolType    { get; } = new GuidConfig(Guid.Empty);

        public FrameListColumnListConfig FrameListColumn { get; } = new FrameListColumnListConfig();


        public override PacketViewProperty Clone()
        {
            return (ClassUtil.Clone<PacketViewPropertyImpl>(this));
        }
    }
}
