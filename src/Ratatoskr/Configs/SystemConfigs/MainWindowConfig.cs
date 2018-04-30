using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;
using Ratatoskr.Forms;

namespace Ratatoskr.Configs.SystemConfigs
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

        public KeyConfig<MainFormActionId> ShortcutKey { get; } = new KeyConfig<MainFormActionId>();


        public MainWindowConfig()
        {
            /* ここにショートカットキーの初期設定を追加 */
            var key_map_default = new [] {
                new { id = MainFormActionId.ApplicationExit,        control = false, shift = false, alt = true,  key = Keys.F4  },
                new { id = MainFormActionId.FileOpen,               control = true,  shift = false, alt = false, key = Keys.O   },
                new { id = MainFormActionId.PacketSaveConvertOff,   control = true,  shift = false, alt = false, key = Keys.S   },
                new { id = MainFormActionId.PacketSaveAsConvertOff, control = true,  shift = true,  alt = false, key = Keys.S   },
                new { id = MainFormActionId.TimeStamp,              control = false, shift = false, alt = false, key = Keys.F6  },
                new { id = MainFormActionId.PacketClear,            control = false, shift = false, alt = false, key = Keys.F4  },
                new { id = MainFormActionId.PacketRedraw,           control = false, shift = false, alt = false, key = Keys.F5  },
                new { id = MainFormActionId.ShowAppDocument,        control = false, shift = false, alt = false, key = Keys.F1  },
            };

            /* デフォルトキーマップを適用 */
            foreach (var config in key_map_default) {
                /* 重複キー情報を削除 */
                ShortcutKey.Value.RemoveAll(
                    value => (
                           (value.KeyPattern.IsControl == config.control)
                        && (value.KeyPattern.IsShift == config.shift)
                        && (value.KeyPattern.IsAlt == config.alt)
                        && (value.KeyPattern.KeyCode == config.key)));

                /* キー情報を追加 */
                ShortcutKey.Value.Add(new KeyActionConfig<MainFormActionId>(config.control, config.shift, config.alt, config.key, config.id));
            }
        }
    }
}
