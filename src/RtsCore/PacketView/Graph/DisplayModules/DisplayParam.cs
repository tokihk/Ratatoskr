using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews.Graph.DisplayModules
{
    internal class DisplayParam
    {
        public DisplayParam(Graphics graphics, Rectangle draw_rect, uint axis_x_min, uint axis_x_max, decimal axis_y_min, decimal axis_y_max)
        {
            Canvas = graphics;
            CanvasRect = draw_rect;
            AxisX_Min = Math.Min(axis_x_min, axis_x_max);
            AxisX_Max = Math.Max(axis_x_min, axis_x_max);
            AxisY_Min = Math.Min(axis_y_min, axis_y_max);
            AxisY_Max = Math.Max(axis_y_min, axis_y_max);
        }

        public Graphics  Canvas     { get; }
        public Rectangle CanvasRect { get; }

        public uint      AxisX_Min { get; }
        public uint      AxisX_Max { get; }

        public decimal   AxisY_Min { get; }
        public decimal   AxisY_Max { get; }
    }
}
