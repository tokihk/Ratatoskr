using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.PacketView.Graph.Configs;

namespace Ratatoskr.PacketView.Graph.DisplayModules
{
    internal class DisplayModule
    {
		private const int CHANNEL_NUM_MAX = 8;


		private readonly Padding GRAPH_DETAILS_MARGIN;

		private const int GRID_X_LINES      = 50;
		private const int GRID_X_LINES_MAIN = 5;

		private const int GRID_Y_LINES      = 50;
		private const int GRID_Y_LINES_MAIN = 5;

		private readonly Brush GRAPH_DETAILS_BG_BRUSH;
		private readonly Font  GRAPH_DETAILS_GRID_SCALE_FONT;
		private readonly Brush GRAPH_DETAILS_GRID_SCALE_BRUSH;
		private readonly Pen   GRAPH_DETAILS_GRID_FRAME_PEN;
		private readonly Pen   GRAPH_DETAILS_GRID_LINE_MAIN_PEN;
		private readonly Pen   GRAPH_DETAILS_GRID_LINE_SUB_PEN;


        public DisplayModule(PacketViewPropertyImpl prop)
        {
            GRAPH_DETAILS_MARGIN = new Padding(50, 50, 100, 50);

            GRAPH_DETAILS_BG_BRUSH = new SolidBrush(Color.FromArgb(0, 24, 0));

            GRAPH_DETAILS_GRID_SCALE_FONT = new Font("MS Gothic", 8);
            GRAPH_DETAILS_GRID_SCALE_BRUSH = new SolidBrush(Color.FromArgb(80, 80, 80));

            GRAPH_DETAILS_GRID_FRAME_PEN = new Pen(Color.FromArgb(90, 90, 90));

            GRAPH_DETAILS_GRID_LINE_MAIN_PEN = new Pen(Color.FromArgb(70, 70, 70));
            GRAPH_DETAILS_GRID_LINE_MAIN_PEN.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            GRAPH_DETAILS_GRID_LINE_SUB_PEN = new Pen(Color.FromArgb(40, 40, 40));
            GRAPH_DETAILS_GRID_LINE_SUB_PEN.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
        }

        public virtual uint PointCount
        {
            get { return (1); }
        }

        public void ClearValue()
        {
            OnClearValue();
        }

        protected virtual void OnClearValue()
        {
        }

        public void InputValue(decimal[] value)
        {
			var value_adj = new decimal[CHANNEL_NUM_MAX];

			value.CopyTo(value_adj, 0);

            OnInputValue(value_adj);
        }

        protected virtual void OnInputValue(decimal[] value)
        {
        }

        public void DrawDisplay(DisplayContext dc)
        {
            if (dc == null) {
                return;
            }

			OnDrawDisplay(dc);
        }

		protected virtual void OnDrawDisplay(DisplayContext dc)
		{
		}
    }
}
