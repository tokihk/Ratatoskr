using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews.Graph.DisplayModules
{
    internal class DisplayLayerParam
    {
        public DisplayLayerParam()
        {
        }

        public Color ForeColor  { get; set; } = Color.Black;

        public decimal AxisY_Offset        { get; set; } = 0;
        public decimal AxisY_Magnification { get; set; } = 0;
    }
}
