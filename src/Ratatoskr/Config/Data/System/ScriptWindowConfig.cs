using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Config.Data.System
{
    public enum ScriptWindowActionId
    {
        FormExit,

        OpenScriptDirectory,

        ScriptRun,
        ScriptStop,
    }

    [Serializable]
    public sealed class ScriptWindowConfig : ConfigHolder
    {
        public BoolConfig  Visible  { get; } = new BoolConfig(false);
        public PointConfig Position { get; } = new PointConfig(-1, -1);
        public SizeConfig  Size     { get; } = new SizeConfig(1024, 768);
        public BoolConfig  Maximize { get; } = new BoolConfig(true);

        public KeyConfig<ScriptWindowActionId> ShortcutKey { get; } = new KeyConfig<ScriptWindowActionId>();


        public ScriptWindowConfig()
        {
            /* ここにショートカットキーの初期設定を追加 */
            var key_map_default = new [] {
                new { id = ScriptWindowActionId.ScriptRun,             control = false, shift = true,  alt = false,  key = Keys.F5  },
                new { id = ScriptWindowActionId.ScriptStop,            control = false, shift = false, alt = false,  key = Keys.F5  },
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
                ShortcutKey.Value.Add(new KeyActionConfig<ScriptWindowActionId>(config.control, config.shift, config.alt, config.key, config.id));
            }
        }
    }
}
