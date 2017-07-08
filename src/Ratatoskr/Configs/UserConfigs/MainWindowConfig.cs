using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;


namespace Ratatoskr.Configs.UserConfigs
{
    [Serializable]
    internal sealed class MainWindowConfig : ConfigHolder
    {
        public PointConfig Position { get; } = new PointConfig(-1, -1);
        public SizeConfig  Size     { get; } = new SizeConfig(1024, 768);
        public BoolConfig  Maximize { get; } = new BoolConfig(true);

        public EnumConfig<DockState> CmdListPanel_DockState      { get; } = new EnumConfig<DockState>(DockState.DockBottomAutoHide);
        public EnumConfig<DockState> RedirectListPanel_DockState { get; } = new EnumConfig<DockState>(DockState.DockBottomAutoHide);
        public EnumConfig<DockState> DataListPanel_DockState     { get; } = new EnumConfig<DockState>(DockState.DockBottomAutoHide);
    }
}
