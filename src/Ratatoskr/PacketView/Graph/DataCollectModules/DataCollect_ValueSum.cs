using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Graph.DataCollectModules
{
    internal class DataCollect_ValueSum : DataCollectModule
    {
        private decimal[] value_sum_;


        public DataCollect_ValueSum(PacketViewPropertyImpl prop) : base(prop)
        {
        }

        protected override void OnSamplingStart()
        {
            value_sum_ = new decimal[ChannelNum];
        }

		protected override void OnSamplingValue(decimal[] channel_data)
		{
            for (var index = 0; index < channel_data.Length; index++) {
                value_sum_[index] += channel_data[index];
            }
		}

		protected override decimal[] OnSamplingEnd()
        {
			return (value_sum_);
        }
    }
}
