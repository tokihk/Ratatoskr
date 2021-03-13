using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Forms
{
    internal static class FormAction
    {
        public static string ShortcutKeyText<EnumT>(IEnumerable<KeyActionConfig<EnumT>> configs, EnumT id)
            where EnumT : struct
        {
            if (configs == null)return ("");

            var key = configs.FirstOrDefault(config => config.ActionID.Equals(id));
            
            if (key == null)return ("");

            return (key.KeyPattern.ToString());
        }

        public static bool ActionIdTextToValue<EnumT>(string id_text, ref EnumT id)
            where EnumT : struct
        {
            if (id_text == null) {
                return (false);
            }

            return (Enum.TryParse(id_text, out id));
        }

        public static void SetupMenuAction<EnumT>(ToolStripMenuItem menu, EventHandler e)
            where EnumT : struct
        {
            if (menu == null)return;

            if (menu.HasDropDownItems) {
                /* 子持ちメニュー */

                /* 共通イベントを削除 */
                menu.Click -= e;

                /* サブメニューに対して同等の処理 */
                foreach (ToolStripItem item in menu.DropDownItems) {
                    SetupMenuAction<EnumT>(item as ToolStripMenuItem, e);
                }

            } else if (menu.Tag is string) {
                /* メニューのタグをActionShortcutIdに変換 */
                var id = (EnumT)(object)0;

                if (ActionIdTextToValue(menu.Tag as string, ref id)) {
                    menu.Tag = id;
                }

                /* 終端メニューの場合は共通イベントを設定 */
                menu.Click += e;
            }
        }

        public static void SetupMenuAction<EnumT>(MenuStrip menu, EventHandler e)
            where EnumT : struct
        {
            if (menu == null)return;

            foreach (ToolStripItem item in menu.Items) {
                SetupMenuAction<EnumT>(item as ToolStripMenuItem, e);
            }
        }

        private static void UpdateMenuKeyText<EnumT>(ToolStripMenuItem menu, IEnumerable<KeyActionConfig<EnumT>> configs)
            where EnumT : struct
        {
            if (menu == null)return;

            var disp_text = "";

            if (menu.HasDropDownItems) {
                /* サブメニューに対して同等の処理 */
                foreach (ToolStripItem item in menu.DropDownItems) {
                    UpdateMenuKeyText(item as ToolStripMenuItem, configs);
                }

            } else if (menu.Tag is EnumT) {
                disp_text = ShortcutKeyText(configs, (EnumT)menu.Tag);
            }

            menu.ShortcutKeyDisplayString = disp_text;
        }

        public static void UpdateMenuKeyText<EnumT>(MenuStrip menu, IEnumerable<KeyActionConfig<EnumT>> configs)
            where EnumT : struct
        {
            if (menu == null)return;

            foreach (ToolStripItem item in menu.Items) {
                UpdateMenuKeyText(item as ToolStripMenuItem, configs);
            }
        }
    }
}
