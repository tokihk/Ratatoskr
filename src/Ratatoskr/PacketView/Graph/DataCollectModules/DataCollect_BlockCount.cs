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

		protected override void OnExtractedValue(decimal[] value)
		{
			data_count_++;
		}

		protected override decimal[] OnSampling()
		{
			var sampling_value = data_count_;

			data_count_ = 0;

			return (new decimal[]{ sampling_value });
		}
    }
}
