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
    internal enum MainWindowActionId
    {
        ApplicationExit,

        TimeStamp,

        PacketRedraw,
        PacketClear,

        PacketSaveConvertOff,
        PacketSaveConvertOn,
        PacketSaveAsConvertOff,
        PacketSaveAsConvertOn,

        FileOpen,

        AutoTimeStampToggle,
        AutoScrollToggle,

        ProfileAdd,
        ProfileRemove,
        ProfileEdit,
        ProfileExport,

        Gate1_Connect,
        Gate2_Connect,
        Gate3_Connect,
        Gate4_Connect,
        Gate5_Connect,

        PacketConverterVisible,

        ShowScriptWindow,
        ShowOptionDialog,

        ShowAppDocument,
        ShowAppDocument_PacketFilter,

        ShowAppInformation,
    }

    [Serializable]
    internal sealed class MainWindowConfig : ConfigHolder
    {
        public PointConfig Position { get; } = new PointConfig(-1, -1);
        public SizeConfig  Size     { get; } = new SizeConfig(1024, 768);
        public BoolConfig  Maximize { get; } = new BoolConfig(true);

        public EnumConfig<DockState> CmdListPanel_DockState      { get; } = new EnumConfig<DockState>(DockState.DockBottomAutoHide);
        public EnumConfig<DockState> RedirectListPanel_DockState { get; } = new EnumConfig<DockState>(DockState.DockBottomAutoHide);
        public EnumConfig<DockState> DataListPanel_DockState     { get; } = new EnumConfig<DockState>(DockState.DockBottomAutoHide);

        public KeyConfig<MainWindowActionId> ShortcutKey { get; } = new KeyConfig<MainWindowActionId>();

        public BoolConfig GatePanelDetailsMode   { get; } = new BoolConfig(true);
        public BoolConfig PacketConverterVisible { get; } = new BoolConfig(true);

        public MainWindowConfig()
        {
            /* ここにショートカットキーの初期設定を追加 */
            var key_map_default = new [] {
                new { id = MainWindowActionId.ApplicationExit,        control = false, shift = false, alt = true,  key = Keys.F4  },
                new { id = MainWindowActionId.FileOpen,               control = true,  shift = false, alt = false, key = Keys.O   },
                new { id = MainWindowActionId.PacketSaveConvertOff,   control = true,  shift = false, alt = false, key = Keys.S   },
                new { id = MainWindowActionId.PacketSaveAsConvertOff, control = true,  shift = true,  alt = false, key = Keys.S   },
                new { id = MainWindowActionId.TimeStamp,              control = false, shift = false, alt = false, key = Keys.F6  },
                new { id = MainWindowActionId.PacketClear,            control = false, shift = false, alt = false, key = Keys.F4  },
                new { id = MainWindowActionId.PacketRedraw,           control = false, shift = false, alt = false, key = Keys.F5  },
                new { id = MainWindowActionId.ShowAppDocument,        control = false, shift = false, alt = false, key = Keys.F1  },

                new { id = MainWindowActionId.Gate1_Connect,          control = true,  shift = false, alt = false, key = Keys.D1  },
                new { id = MainWindowActionId.Gate2_Connect,          control = true,  shift = false, alt = false, key = Keys.D2  },
                new { id = MainWindowActionId.Gate3_Connect,          control = true,  shift = false, alt = false, key = Keys.D3  },
                new { id = MainWindowActionId.Gate4_Connect,          control = true,  shift = false, alt = false, key = Keys.D4  },
                new { id = MainWindowActionId.Gate5_Connect,          control = true,  shift = false, alt = false, key = Keys.D5  },
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
                ShortcutKey.Value.Add(new KeyActionConfig<MainWindowActionId>(config.control, config.shift, config.alt, config.key, config.id));
            }
        }
    }
}
