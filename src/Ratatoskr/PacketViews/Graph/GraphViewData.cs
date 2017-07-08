using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews.Graph
{
    internal class GraphViewData
    {
        public GraphViewData(double value_min, double value_max, double[] values)
        {
            DataValueMin = value_min;
            DataValueMax = value_max;
            DataValues = values;
        }

        public double   DataValueMax { get; } = 1000;
        public double   DataValueMin { get; } = -1000;
        public double[] DataValues   { get; }
    }
}
