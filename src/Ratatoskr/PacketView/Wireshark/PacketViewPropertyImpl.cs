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
        public IntegerConfig	LibPcapLinkType		   { get; } = new IntegerConfig(1);
		public BoolConfig		TransferWithPcapHeader { get; } = new BoolConfig(true);

		public BoolConfig		SendPacketCapture { get; } = new BoolConfig(false);
		public BoolConfig		RecvPacketCapture { get; } = new BoolConfig(true);


        public override PacketViewProperty Clone()
        {
            return (ClassUtil.Clone<PacketViewPropertyImpl>(this));
        }
    }
}
