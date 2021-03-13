using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.ProtocolParser
{
	internal class ProtocolParsePacketInfo : ProtocolParseInfo
	{
		public InformationObject PacketInfo { get; } = new InformationObject("");
	}
}
