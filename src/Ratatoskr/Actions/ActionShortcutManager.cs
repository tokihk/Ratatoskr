using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ratatoskr.Actions.ActionModules;
using Ratatoskr.Configs;

namespace Ratatoskr.Actions
{
    internal enum ActionShortcutId
    {
        None,

        ApplicationExit,

        TimeStampRun,
        EventPacketRedraw,
        EventPacketClear,

        FileOpen,
        ConfigSave,

        PacketSaveRuleOff,
        PacketSaveRuleOn,
        PacketSaveAsRuleOff,
        PacketSaveAsRuleOn,

        AutoSaveToggle,
        AutoTimeStampToggle,
        AutoScrollToggle,

        ShowConfigDialog,
        ShowAppInformation,
    }

    internal static class ActionShortcutManager
    {
        public static void Exec(ActionShortcutId id, ActionObject.ActionCompletedDelegate method)
        {
            var action = (ActionObject)null;

            switch (id) {
                case ActionShortcutId.ApplicationExit:
                    action = new Action_Shutdown();
                    break;
                case ActionShortcutId.TimeStampRun:
                    action = new Action_TimeStamp();
                    break;
                case ActionShortcutId.EventPacketRedraw:
                    action = new Action_ScreenUpdate();
                    break;
                case ActionShortcutId.EventPacketClear:
                    action = new Action_PacketClear();
                    break;
                case ActionShortcutId.FileOpen:
                    action = new Action_FileOpen();
                    break;
                case ActionShortcutId.PacketSaveRuleOff:
                    action = new Action_PacketSave(true, false);
                    break;
                case ActionShortcutId.PacketSaveRuleOn:
                    action = new Action_PacketSave(true, true);
                    break;
                case ActionShortcutId.PacketSaveAsRuleOff:
                    action = new Action_PacketSave(false, false);
                    break;
                case ActionShortcutId.PacketSaveAsRuleOn:
                    action = new Action_PacketSave(false, true);
                    break;
                case ActionShortcutId.AutoScrollToggle:
                    action = new Action_AutoScroll(!ConfigManager.User.Option.AutoScroll.Value);
                    break;
                case ActionShortcutId.AutoTimeStampToggle:
                    action = new Action_AutoTimeStamp(!ConfigManager.User.Option.AutoTimeStamp.Value);
                    break;
                case ActionShortcutId.AutoSaveToggle:
                    action = new Action_AutoPacketSave(!ConfigManager.User.Option.AutoSave.Value);
                    break;
                case ActionShortcutId.ShowAppInformation:
                    action = new Action_ShowAppInfo();
                    break;
                case ActionShortcutId.ShowConfigDialog:
                    action = new Action_ShowConfigDialog();
                    break;
            }

            if (action == null)return;

            if (method != null) {
                action.ActionCompleted += method;
            }

            action.Exec();
        }

        public static string ShortcutText(ActionShortcutId id)
        {
            var key = ConfigManager.User.Option.ShortcutKey.Value.Find(config => config.ActionID == id);

            if (key != null) {
                return (key.KeyPattern.ToString());
            } else {
                return ("");
            }
        }
    }
}
