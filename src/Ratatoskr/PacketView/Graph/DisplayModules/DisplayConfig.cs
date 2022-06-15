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
		public uint					GraphRecordPoint	{ get; set; } = 0;

		public uint					GraphDisplayPoint	{ get; set; } = 0;

		public uint					GraphDisplayOffset	{ get; set; } = 0;

		public GraphChannelConfig[]	GraphChannelConfigs	{ get; set; } = null;

		public Rectangle			DisplayRect			{ get; set; }
    }
}
