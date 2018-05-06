using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;
using Ratatoskr.Gate;

namespace Ratatoskr.Forms
{
    internal enum MainFormActionId
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

        ShowOptionDialog,
        ShowAppDocument,
        ShowAppInformation,
    }

    internal enum ScriptIDEActionId
    {
        ScriptRun,
        ScriptStop,
    }

    internal static class FormKeyManager
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

        public static string MainFormShortcutKeyText(MainFormActionId id)
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

            var id = (MainFormActionId)0;

            if (!Enum.TryParse(id_text, out id))return;

            MainFormKeyAction(control, id);
        }

        private delegate void MainFormKeyActionHandler(Control control, MainFormActionId id);
        public static void MainFormKeyAction(Control control, MainFormActionId id)
        {
            if ((control != null) && (control.InvokeRequired)) {
                control.Invoke((MainFormKeyActionHandler)MainFormKeyAction, id);
                return;
            }

            switch (id) {
                case MainFormActionId.ApplicationExit:
                    Program.ShutdownRequest();
                    break;

                case MainFormActionId.TimeStamp:
                    GatePacketManager.SetTimeStamp(ConfigManager.Language.MainMessage.TimeStampManual.Value);
                    break;

                case MainFormActionId.PacketRedraw:
                    FormTaskManager.RedrawPacketRequest();
                    break;

                case MainFormActionId.PacketClear:
                    GatePacketManager.ClearPacket();
                    break;

                case MainFormActionId.PacketSaveConvertOff:
                    FormUiManager.PacketSave(true, false);
                    break;

                case MainFormActionId.PacketSaveConvertOn:
                    FormUiManager.PacketSave(true, true);
                    break;

                case MainFormActionId.PacketSaveAsConvertOff:
                    FormUiManager.PacketSave(false, false);
                    break;

                case MainFormActionId.PacketSaveAsConvertOn:
                    FormUiManager.PacketSave(false, true);
                    break;

                case MainFormActionId.FileOpen:
                    FormUiManager.FileOpen();
                    break;

                case MainFormActionId.AutoTimeStampToggle:
                    ConfigManager.System.AutoTimeStamp.Enable.Value = !ConfigManager.System.AutoTimeStamp.Enable.Value;
                    FormUiManager.MainFrameMenuBarUpdate();
                    break;

                case MainFormActionId.AutoScrollToggle:
                    ConfigManager.System.AutoScroll.Value = !ConfigManager.System.AutoScroll.Value;
                    FormUiManager.MainFrameMenuBarUpdate();
                    break;

                case MainFormActionId.ProfileAdd:
                    ConfigManager.CreateNewProfile("New Profile", null, true);
                    break;
                case MainFormActionId.ProfileRemove:
                    ConfigManager.DeleteProfile(ConfigManager.GetCurrentProfileID());
                    break;
                case MainFormActionId.ProfileEdit:
                    if (FormUiManager.ShowProfileEditDialog("Edit Profile", ConfigManager.User, ConfigManager.User.ProfileName.Value)) {
                        ConfigManager.SaveCurrentProfile(true);
                        FormUiManager.MainFrameMenuBarUpdate();
                    }
                    break;
                case MainFormActionId.ProfileExport:
                    ConfigManager.SaveConfig();
                    ConfigManager.ExportProfile(ConfigManager.GetCurrentProfileID());
                    break;

                case MainFormActionId.Gate1_Connect:
                case MainFormActionId.Gate2_Connect:
                case MainFormActionId.Gate3_Connect:
                case MainFormActionId.Gate4_Connect:
                case MainFormActionId.Gate5_Connect:
                    var gate_list = GateManager.GetGateList();
                    var gate_id = (int)(id - MainFormActionId.Gate1_Connect);

                    if (gate_id < gate_list.Length) {
                        gate_list[gate_id].ConnectRequest = !gate_list[gate_id].ConnectRequest;
                    }
                    break;

                case MainFormActionId.ShowOptionDialog:
                    FormUiManager.ShowOptionDialog();
                    break;

                case MainFormActionId.ShowAppDocument:
                    FormUiManager.ShowAppDocument();
                    break;

                case MainFormActionId.ShowAppInformation:
                    FormUiManager.ShowAppInfo();
                    break;
            }
        }
    }
}
