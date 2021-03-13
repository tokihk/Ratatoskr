using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Config.Types
{
    public enum WindowDockState
    {
        Document,
        DockTopAutoHide,
        DockLeftAutoHide,
        DockBottomAutoHide,
        DockRightAutoHide,
        DockTop,
        DockLeft,
        DockBottom,
        DockRight,
    }


    [Serializable]
    public class WindowDockStateConfig : EnumConfig<WindowDockState>
    {
        public WindowDockStateConfig(WindowDockState value) : base(value)
        {
        }
    }
}
