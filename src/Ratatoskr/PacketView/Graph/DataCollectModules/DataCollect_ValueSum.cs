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
        private decimal[] data_;


        public DataCollect_ValueSum(uint layer_count, uint sampling_ival) : base(layer_count, sampling_ival)
        {
            data_ = new decimal[LayerCount];
        }

        protected override void OnAssignData(PacketObject base_packet, decimal[] assign_data)
        {
            for (var index = 0; index < assign_data.Length; index++) {
                data_[index] = data_[index] + assign_data[index];
            }
        }

        protected override void OnSamplingStart()
        {
            data_.Initialize();
        }

        protected override void OnSamplingEnd(decimal[] sampling_data)
        {
            data_.CopyTo(sampling_data, 0);
        }
    }
}
