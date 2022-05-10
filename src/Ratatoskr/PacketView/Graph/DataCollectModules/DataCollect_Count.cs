using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Graph.DataCollectModules
{
    internal class DataCollect_Count : DataCollectModule
    {
        private decimal data_count_ = 0;


        public DataCollect_Count(PacketViewPropertyImpl prop) : base(prop)
        {
        }

        protected override void OnSamplingStart()
        {
            data_count_ = 0;
        }

		protected override void OnSamplingValue(decimal[] value)
		{
			data_count_++;
		}

		protected override decimal[] OnSamplingEnd()
        {
			return (Enumerable.Repeat(data_count_, (int)ChannelNum).ToArray());
        }
    }
}
