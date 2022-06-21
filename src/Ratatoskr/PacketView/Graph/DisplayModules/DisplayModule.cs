using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.PacketView.Graph.DisplayModules
{
	internal class DisplayModule : IDisposable
	{
		public DisplayModule()
		{
		}

		public void Dispose()
		{
			OnDisposed();
		}

		protected virtual void OnDisposed()
		{
		}

		public virtual uint PointCount
		{
			get { return (0); }
		}

		public void SetDisplayConfig(DisplayConfig config)
		{
			if (config == null)return;

			OnDisplayConfigUpdated(config);
		}

		protected virtual void OnDisplayConfigUpdated(DisplayConfig config)
		{
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
			OnDrawDisplay(dc);
        }

		protected virtual void OnDrawDisplay(DisplayContext dc)
		{
		}
    }
}
