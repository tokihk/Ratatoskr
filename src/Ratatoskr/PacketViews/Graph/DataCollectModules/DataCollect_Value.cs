using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Packet;

namespace Ratatoskr.PacketViews.Graph.DataCollectModules
{
    internal class DataCollect_Value : DataCollectModule
    {
        private decimal[] last_data_;


        public DataCollect_Value(uint layer_count, uint sampling_ival) : base(layer_count, sampling_ival)
        {
            last_data_ = new decimal[LayerCount];
        }

        protected override void OnAssignData(PacketObject base_packet, decimal[] assign_data)
        {
            assign_data.CopyTo(last_data_, 0);
        }

        protected override void OnSamplingStart()
        {
        }

        protected override void OnSamplingEnd(decimal[] sampling_data)
        {
            last_data_.CopyTo(sampling_data, 0);
        }
    }
}
