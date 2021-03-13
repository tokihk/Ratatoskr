using Ratatoskr.General.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;
using PacketDotNet;

namespace Ratatoskr.ProtocolParser
{
	internal class ProtocolParserInstance : ModuleInstance<ProtocolParserManager, ProtocolParserClass, ProtocolParserInstance>, IDisposable
	{
		public ProtocolParserInstance(ProtocolParserManager manager) : base(manager)
		{
		}

		public ProtocolParsePacketInfo ParsePacket(PacketObject packet)
		{
			return (OnParsePacket(packet));
		}

		protected virtual ProtocolParsePacketInfo OnParsePacket(PacketObject packet)
		{
			return (null);
		}
	}
}
