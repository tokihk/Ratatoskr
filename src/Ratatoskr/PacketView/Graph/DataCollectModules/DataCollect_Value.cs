﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Graph.DataCollectModules
{
    internal class DataCollect_Value : DataCollectModule
    {
        private decimal[] values_latest_;


        public DataCollect_Value(PacketViewPropertyImpl prop) : base(prop)
        {
        }

		protected override void OnExtractedValue(decimal[] value)
		{
			values_latest_ = value;
		}

		protected override decimal[] OnSampling()
		{
			return (values_latest_);
		}
    }
}