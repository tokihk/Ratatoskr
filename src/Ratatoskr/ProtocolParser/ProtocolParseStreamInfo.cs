using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.ProtocolParser
{
	internal class ProtocolParseStreamInfo : ProtocolParseInfo
	{
		public InformationObject StreamInfo { get; } = new InformationObject("");
	}
}
