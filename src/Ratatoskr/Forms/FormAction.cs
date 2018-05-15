using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.SystemConfigs;
using Ratatoskr.Configs.Types;
using Ratatoskr.Gate;

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

        public static string MainFormShortcutKeyText(MainWindowActionId id)
        {
            return (ShortcutKeyText(ConfigManager.System.MainWindow.ShortcutKey.Value, id));
        }

        public static void MainFormKeyAction(Control control, string id_text)
        {
            if (   (control == null)
                || (id_text == null)
            ) {
                return;
            }

            var id = (MainWindowActionId)0;

            if (!Enum.TryParse(id_text, out id))return;

            MainFormKeyAction(control, id);
        }

        private delegate void MainFormKeyActionHandler(Control control, MainWindowActionId id);
        public static void MainFormKeyAction(Control control, MainWindowActionId id)
        {
            if ((control != null) && (control.InvokeRequired)) {
                control.Invoke((MainFormKeyActionHandler)MainFormKeyAction, id);
                return;
            }

            switch (id) {
                case MainWindowActionId.ApplicationExit:
                    Program.ShutdownRequest();
                    break;

                case MainWindowActionId.TimeStamp:
                    GatePacketManager.SetTimeStamp(ConfigManager.Language.MainMessage.TimeStampManual.Value);
                    break;

                case MainWindowActionId.PacketRedraw:
                    FormTaskManager.RedrawPacketRequest();
                    break;

                case MainWindowActionId.PacketClear:
                    GatePacketManager.ClearPacket();
                    break;

                case MainWindowActionId.PacketSaveConvertOff:
                    FormUiManager.PacketSave(true, false);
                    break;

                case MainWindowActionId.PacketSaveConvertOn:
                    FormUiManager.PacketSave(true, true);
                    break;

                case MainWindowActionId.PacketSaveAsConvertOff:
                    FormUiManager.PacketSave(false, false);
                    break;

                case MainWindowActionId.PacketSaveAsConvertOn:
                    FormUiManager.PacketSave(false, true);
                    break;

                case MainWindowActionId.FileOpen:
                    FormUiManager.FileOpen();
                    break;

                case MainWindowActionId.AutoTimeStampToggle:
                    ConfigManager.System.AutoTimeStamp.Enable.Value = !ConfigManager.System.AutoTimeStamp.Enable.Value;
                    FormUiManager.MainFrameMenuBarUpdate();
                    break;

                case MainWindowActionId.AutoScrollToggle:
                    ConfigManager.System.AutoScroll.Value = !ConfigManager.System.AutoScroll.Value;
                    FormUiManager.MainFrameMenuBarUpdate();
                    break;

                case MainWindowActionId.ProfileAdd:
                    ConfigManager.CreateNewProfile("New Profile", null, true);
                    break;
                case MainWindowActionId.ProfileRemove:
                    ConfigManager.DeleteProfile(ConfigManager.GetCurrentProfileID());
                    break;
                case MainWindowActionId.ProfileEdit:
                    if (FormUiManager.ShowProfileEditDialog("Edit Profile", ConfigManager.User, ConfigManager.User.ProfileName.Value)) {
                        ConfigManager.SaveCurrentProfile(true);
                        FormUiManager.MainFrameMenuBarUpdate();
                    }
                    break;
                case MainWindowActionId.ProfileExport:
                    ConfigManager.SaveConfig();
                    ConfigManager.ExportProfile(ConfigManager.GetCurrentProfileID());
                    break;

                case MainWindowActionId.Gate1_Connect:
                case MainWindowActionId.Gate2_Connect:
                case MainWindowActionId.Gate3_Connect:
                case MainWindowActionId.Gate4_Connect:
                case MainWindowActionId.Gate5_Connect:
                    var gate_list = GateManager.GetGateList();
                    var gate_id = (int)(id - MainWindowActionId.Gate1_Connect);

                    if (gate_id < gate_list.Length) {
                        gate_list[gate_id].ConnectRequest = !gate_list[gate_id].ConnectRequest;
                    }
                    break;

                case MainWindowActionId.ShowOptionDialog:
                    FormUiManager.ShowOptionDialog();
                    break;

                case MainWindowActionId.ShowAppDocument:
                    FormUiManager.ShowAppDocument();
                    break;

                case MainWindowActionId.ShowAppInformation:
                    FormUiManager.ShowAppInfo();
                    break;
            }
        }

        private delegate void ScriptEditorKeyActionHandler(Control control, ScriptWindowActionId id);
        public static void ScriptEditorKeyAction(Control control, ScriptWindowActionId id)
        {
            if ((control != null) && (control.InvokeRequired)) {
                control.Invoke((ScriptEditorKeyActionHandler)ScriptEditorKeyAction, id);
                return;
            }

            switch (id) {
                case ScriptWindowActionId.FormExit:
                    break;
                case ScriptWindowActionId.OpenScriptDirectory:
                    break;
                case ScriptWindowActionId.ScriptRun:
                    break;
                case ScriptWindowActionId.ScriptStop:
                    break;
            }
        }
    }
}
