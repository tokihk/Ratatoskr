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
        private decimal[] last_data_;


        public DataCollect_Value(PacketViewPropertyImpl prop) : base(prop)
        {
        }

        protected override void OnSamplingStart()
        {
			last_data_ = new decimal[ChannelNum];
        }

		protected override void OnSamplingValue(decimal[] channel_data)
		{
			channel_data.CopyTo(last_data_, 0);
		}

		protected override decimal[] OnSamplingEnd()
        {
			return (last_data_);
        }
    }
}
