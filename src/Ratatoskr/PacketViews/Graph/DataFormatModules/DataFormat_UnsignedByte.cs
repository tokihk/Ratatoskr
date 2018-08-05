using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews.Graph.DataFormatModules
{
    internal class DataFormat_UnsignedByte : DataFormatModule
    {
        protected override void OnAssignData(byte assign_data)
        {
            ExtractData((byte)assign_data);
        }
    }
}
