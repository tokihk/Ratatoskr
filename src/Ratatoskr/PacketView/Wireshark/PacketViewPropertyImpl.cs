using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.PacketView;
using Ratatoskr.General;

namespace Ratatoskr.PacketView.Wireshark
{
    internal class PacketViewPropertyImpl : PacketViewProperty
    {
		public BoolConfig		InnerWindowMode { get; } = new BoolConfig(true);

        public IntegerConfig	LibPcapLinkType	{ get; } = new IntegerConfig(1);

		public BoolConfig		SendPacketCapture { get; } = new BoolConfig(false);
		public BoolConfig		RecvPacketCapture { get; } = new BoolConfig(true);


        public override PacketViewProperty Clone()
        {
            return (ClassUtil.Clone<PacketViewPropertyImpl>(this));
        }
    }
}
