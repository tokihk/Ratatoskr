using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.PacketView.Graph.Configs;

namespace Ratatoskr.PacketView.Graph.DisplayModules
{
    internal class DisplayContext
    {
        public DisplayContext(Graphics graphics, Rectangle draw_rect, uint hscroll_pos, PacketViewPropertyImpl prop, ChannelConfig[] ch_configs)
        {
            Canvas = graphics;
            CanvasRect = draw_rect;
			HScrollPos = hscroll_pos;
			Property = prop;
			ChannelConfigs = ch_configs;
        }

        public Graphics               Canvas         { get; }
        public Rectangle              CanvasRect     { get; }
		public uint                   HScrollPos     { get; }
		public PacketViewPropertyImpl Property       { get; }
		public ChannelConfig[]        ChannelConfigs { get; }
    }
}
