﻿using System;
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
        private decimal[] values_sum_;


        public DataCollect_ValueSum(PacketViewPropertyImpl prop) : base(prop)
        {
			values_sum_ = new decimal[ValueChannelNum];
        }

		protected override void OnExtractedValue(decimal[] value)
		{
			var index_max = Math.Min(values_sum_.Length, value.Length);

            for (var index = 0; index < index_max; index++) {
                values_sum_[index] += value[index];
            }
		}

		protected override decimal[] OnSampling()
		{
			var sampling_values = values_sum_;

			values_sum_ = new decimal[ValueChannelNum];

			return (values_sum_);
		}
    }
}