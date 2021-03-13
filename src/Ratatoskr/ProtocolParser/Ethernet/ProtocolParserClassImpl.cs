using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.ProtocolParser.Ethernet
{
	internal class ProtocolParserClassImpl : ProtocolParserClass
	{
		public static readonly Guid ClassID = Guid.Parse("2ED8911C-FCDB-4525-9DAA-92342AA49413");


		public ProtocolParserClassImpl() : base(ClassID)
		{
		}

		public override string Name { get; } = "Ethernet";

		protected override ProtocolParserInstance OnCreateInstance(ProtocolParserManager module_manager)
		{
			return (new ProtocolParserInstanceImpl(module_manager));
		}
	}
}
