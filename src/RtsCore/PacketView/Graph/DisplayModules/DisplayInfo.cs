using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews.Graph.DisplayModules
{
    internal class DisplayInfo
    {
        public DisplayInfo(uint layer_count, uint point_count)
        {
            LayerCount = layer_count;
            PointCount = point_count;
        }

        public uint LayerCount { get; } = 0;

        public uint PointCount { get; } = 0;
    }
}
