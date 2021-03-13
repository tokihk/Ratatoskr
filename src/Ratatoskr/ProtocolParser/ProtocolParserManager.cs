using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;

namespace Ratatoskr.ProtocolParser
{
	internal class ProtocolParserManager : ModuleManager<ProtocolParserManager, ProtocolParserClass, ProtocolParserInstance>
	{
		public ProtocolParserManager()
		{
			AddClass(new Ethernet.ProtocolParserClassImpl());
		}
	}
}
