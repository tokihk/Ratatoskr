using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;
using Ratatoskr.PacketViews.Protocol.Configs;

namespace Ratatoskr.PacketViews.Protocol
{
    internal class ViewPropertyImpl : ViewProperty
    {
        public GuidConfig                ProtocolType    { get; } = new GuidConfig(Guid.Empty);

        public FrameListColumnListConfig FrameListColumn { get; } = new FrameListColumnListConfig();


        public override ViewProperty Clone()
        {
            return (ClassUtil.Clone<ViewPropertyImpl>(this));
        }
    }
}
