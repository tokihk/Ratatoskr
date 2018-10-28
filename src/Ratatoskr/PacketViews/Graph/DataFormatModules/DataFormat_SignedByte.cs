﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews.Graph.DataFormatModules
{
    internal class DataFormat_SignedByte : DataFormatModule
    {
        protected override void OnAssignData(byte assign_data)
        {
            ExtractData((sbyte)assign_data);
        }
    }
}