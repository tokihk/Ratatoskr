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
		private PacketViewPropertyImpl	prop_ = null;
		private DisplayConfig			disp_config_ = null;


		public DisplayModule(PacketViewPropertyImpl prop)
		{
			prop_ = prop;
		}

		public void Dispose()
		{
			OnDisposed();
		}

		protected virtual void OnDisposed()
		{
		}

		public PacketViewPropertyImpl Property
		{
			get { return (prop_); }
		}

		public DisplayConfig Config
		{
			get
			{
				return disp_config_;
			}
			set
			{
				if (disp_config_ == value)return;

				disp_config_ = value;

				OnDisplayConfigChanged(disp_config_);
			}
		}

		public virtual uint PointCount
		{
			get { return (0); }
		}

		protected virtual void OnDisplayConfigChanged(DisplayConfig config)
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
            if ((dc == null) || (Config == null)) {
                return;
            }

			OnDrawDisplay(dc);
        }

		protected virtual void OnDrawDisplay(DisplayContext dc)
		{
		}
    }
}
