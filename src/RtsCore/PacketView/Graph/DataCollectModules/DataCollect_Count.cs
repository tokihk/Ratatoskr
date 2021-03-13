using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace Ratatoskr.PacketViews.Graph.DataCollectModules
{
    internal class DataCollect_Count : DataCollectModule
    {
        private decimal data_count_ = 0;


        public DataCollect_Count(uint layer_count, uint sampling_ival) : base(layer_count, sampling_ival)
        {
        }

        protected override void OnAssignData(PacketObject base_packet, decimal[] assign_data)
        {
            data_count_++;
        }

        protected override void OnSamplingStart()
        {
            data_count_ = 0;
        }

        protected override void OnSamplingEnd(decimal[] sampling_data)
        {
            for (var index = 0; index < sampling_data.Length; index++) {
                sampling_data[index] = data_count_;
            }
        }
    }
}
