using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Graph.DataCollectModules
{
    internal class DataCollect_Value : DataCollectModule
    {
        private long[] values_latest_;


        public DataCollect_Value(PacketViewPropertyImpl prop) : base(prop)
        {
        }

		protected override void OnExtractedValue(long[] value)
		{
			values_latest_ = value;
		}

		protected override long[] OnSampling()
		{
			return (values_latest_);
		}
    }
}
