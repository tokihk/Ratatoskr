using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Graph.DataCollectModules
{
    internal class DataCollect_BlockCount : DataCollectModule
    {
        private ulong data_count_ = 0;


        public DataCollect_BlockCount(PacketViewPropertyImpl prop) : base(prop)
        {
        }

		protected override void OnExtractedValue(long[] value)
		{
			data_count_++;
		}

		protected override long[] OnSampling()
		{
			var sampling_value = data_count_;

			data_count_ = 0;

			return (new long[]{ (long)sampling_value });
		}
    }
}
