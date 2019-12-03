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
        public GuidConfig                DecoderClassID  { get; } = new GuidConfig(Guid.Empty);

        public EventListColumnListConfig EventListColumn { get; } = new EventListColumnListConfig();

		public IntegerConfig ChartAxisMag_X { get; } = new IntegerConfig((decimal)1.0f);
		public IntegerConfig ChartAxisMag_Y { get; } = new IntegerConfig((decimal)1.0f);


		public override PacketViewProperty Clone()
        {
            return (ClassUtil.Clone<PacketViewPropertyImpl>(this));
        }
    }
}
