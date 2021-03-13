using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.ProtocolParser;
using Ratatoskr.General;

namespace Ratatoskr.PacketView.Protocol
{
	internal enum PacketListColumnID
	{
		Alias,
		Datetime_UTC,
		Datetime_Local,
		Source,
		Destination,
		PacketLength,
		PacketInformation,
	}

    internal class PacketViewPropertyImpl : PacketViewProperty
    {
        public GuidConfig								ProtocolType		{ get; } = new GuidConfig(Ratatoskr.ProtocolParser.Ethernet.ProtocolParserClassImpl.ClassID);

		public ColumnListConfig<PacketListColumnID>		PacketListColumn	{ get; } = new ColumnListConfig<PacketListColumnID>();


		public PacketViewPropertyImpl()
		{
			PacketListColumn.Value.Add(new KeyValuePair<PacketListColumnID, int>(PacketListColumnID.Datetime_Local, 170));
			PacketListColumn.Value.Add(new KeyValuePair<PacketListColumnID, int>(PacketListColumnID.Source, 150));
			PacketListColumn.Value.Add(new KeyValuePair<PacketListColumnID, int>(PacketListColumnID.Destination, 150));
			PacketListColumn.Value.Add(new KeyValuePair<PacketListColumnID, int>(PacketListColumnID.PacketLength, 100));
			PacketListColumn.Value.Add(new KeyValuePair<PacketListColumnID, int>(PacketListColumnID.PacketInformation, 150));
		}

        public override PacketViewProperty Clone()
        {
            return (ClassUtil.Clone<PacketViewPropertyImpl>(this));
        }
    }
}
