using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;

namespace Ratatoskr.Protocol
{
	internal class ProtocolPacket
	{
		public ProtocolPacket()
		{
		}

		public uint						HeaderLength  { get; }
		public byte[]					HeaderData    { get; }
		public ProtocolPacketProperty	Property	  { get; }

		public uint				PayloadLength { get; }
		public byte[]			PayloadData   { get; }
		public ProtocolPacket	PayloadPacket { get; }


		public void UnpackSegment(BitData bitdata)
		{

		}

		private void OnUnpackSegment(BitData bitdata)
		{
		}
	}
}
