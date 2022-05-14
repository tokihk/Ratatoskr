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


        public DisplayModule(PacketViewPropertyImpl prop, uint channel_num_max)
        {
			ChannelNum = Math.Min((uint)prop.ChannelList.Value.Count, channel_num_max);
        }

		public uint ChannelNum { get; }

        public void ClearValue()
        {
            OnClearValue();
        }

        protected virtual void OnClearValue()
        {
        }

        public void InputValue(decimal[] value)
        {
            OnInputValue(value);
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
