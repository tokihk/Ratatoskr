using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;

namespace Ratatoskr.ProtocolParser
{
	internal abstract class ProtocolParserClass : ModuleClass<ProtocolParserManager, ProtocolParserClass, ProtocolParserInstance>
	{
		public ProtocolParserClass(Guid id) : base(id)
		{
		}
	}
}
