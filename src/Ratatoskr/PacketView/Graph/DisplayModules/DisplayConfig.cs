using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketView.Graph.DisplayModules
{
    internal class DisplayConfig
    {
		public Rectangle DisplayRect { get; set; } = new Rectangle();

		public uint DisplayPoint { get; set; } = 0;

		public uint DisplayAxisX_Offset { get; set; } = 0;

		public GraphChannelConfig[] ChannelConfigs { get; set; } = null;
    }
}
