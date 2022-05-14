using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.PacketView.Graph.DisplayModules
{
	internal class DisplayModule
	{
		public DisplayModule(PacketViewPropertyImpl prop)
		{
		}

		public uint ChannelNumber
		{
			get;
		}

		public virtual uint PointCount
		{
			get { return (0); }
		}

        public void ClearValue()
        {
            OnClearValue();
        }

        protected virtual void OnClearValue()
        {
        }

        public void InputValue(long[] value)
        {
            OnInputValue(value);
        }

        protected virtual void OnInputValue(long[] value)
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
