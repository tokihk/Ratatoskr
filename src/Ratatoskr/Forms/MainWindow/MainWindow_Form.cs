using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.SystemConfigs;
using Ratatoskr.Gate;
using RtsCore.Packet;
using RtsCore.Framework.PacketConverter;
using RtsCore.Framework.PacketView;
using RtsCore.Utility;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_Form : Form
    {
        public MainWindow_Form()
        {
            InitializeComponent();
            InitializeMenuBar();

            Icon = Properties.Resources.app_icon_48x48_ico;

            SetStatusText("");
            SetProgressBar(false, 0, 0);

            Visible = false;
        }

        private void InitializeMenuBar()
        {
            FormAction.SetupMenuAction<MainWindowActionId>(MBar_Main, OnMenuClick);

            BuildPacketViewMenu(MenuBar_View_PacketViewAdd);
            BuildPacketConverterMenu(MenuBar_View_PacketConverterAdd);
        }

        public void LoadConfig()
        {
            UpdateTitle();

            LoadMenuBarConfig();

            Panel_Center.LoadConfig();
            SingleCmdPanel_Main.LoadConfig();

            LoadWindowConfig();

            UpdateMenuBar();
            UpdateStatusBar();
            UpdateProfileList();
        }

        private void LoadMenuBarConfig()
        {
            FormAction.UpdateMenuKeyText(MBar_Main, ConfigManager.System.MainWindow.ShortcutKey.Value);

            MenuBar_File.Text = ConfigManager.Language.MainUI.MenuBar_File.Value;
            MenuBar_File_Open.Text = ConfigManager.Language.MainUI.MenuBar_File_Open.Value;
            MenuBar_File_Save.Text = ConfigManager.Language.MainUI.MenuBar_File_Save.Value;
            MenuBar_File_Save_Original.Text = ConfigManager.Language.MainUI.MenuBar_File_Save_Original.Value;
            MenuBar_File_Save_Shaping.Text = ConfigManager.Language.MainUI.MenuBar_File_Save_Shaping.Value;
            MenuBar_File_SaveAs.Text = ConfigManager.Language.MainUI.MenuBar_File_SaveAs.Value;
            MenuBar_File_SaveAs_Original.Text = ConfigManager.Language.MainUI.MenuBar_File_SaveAs_Original.Value;
            MenuBar_File_SaveAs_Shaping.Text = ConfigManager.Language.MainUI.MenuBar_File_SaveAs_Shaping.Value;
            MenuBar_File_Exit.Text = ConfigManager.Language.MainUI.MenuBar_File_Exit.Value;
            MenuBar_View.Text = ConfigManager.Language.MainUI.MenuBar_View.Value;
            MenuBar_Tool.Text = ConfigManager.Language.MainUI.MenuBar_Tool.Value;
            MenuBar_Help.Text = ConfigManager.Language.MainUI.MenuBar_Help.Value;
            MenuBar_Help_Document.Text = ConfigManager.Language.MainUI.MenuBar_Help_Document.Value;
            MenuBar_Help_About.Text = ConfigManager.Language.MainUI.MenuBar_Help_About.Value;
        }

        private void LoadWindowConfig()
        {
            /* 最小/最大化を解除 */
            WindowState = FormWindowState.Normal;

            /* 設定を反映 */
            Size = ConfigManager.System.MainWindow.Size.Value;
            Location = ConfigManager.System.MainWindow.Position.Value;
            WindowState = (ConfigManager.System.MainWindow.Maximize.Value) ? (FormWindowState.Maximized) : (FormWindowState.Normal);

            Menu_DataRate_SendData.Checked = ConfigManager.User.DataRateTarget.Value.HasFlag(PacketDataRateTarget.SendData);
            Menu_DataRate_RecvData.Checked = ConfigManager.User.DataRateTarget.Value.HasFlag(PacketDataRateTarget.RecvData);

            ApplyDataRateTarget();
        }

        public void BackupConfig()
        {
            Panel_Center.BackupConfig();
            SingleCmdPanel_Main.BackupConfig();

            BackupWindowConfig();

            ConfigManager.User.DataRateTarget.Value = 0;
            ConfigManager.User.DataRateTarget.Value |= (Menu_DataRate_SendData.Checked) ? (PacketDataRateTarget.SendData) : (0);
            ConfigManager.User.DataRateTarget.Value |= (Menu_DataRate_RecvData.Checked) ? (PacketDataRateTarget.RecvData) : (0);
        }

        private void BackupWindowConfig()
        {
            ConfigManager.System.MainWindow.Maximize.Value = (WindowState == FormWindowState.Maximized);

            /* 最小/最大化を解除 */
            WindowState = FormWindowState.Normal;

            ConfigManager.System.MainWindow.Size.Value = Size;
            ConfigManager.System.MainWindow.Position.Value = Location;
        }

        private void BuildPacketConverterMenu(ToolStripMenuItem menu)
        {
            /* 全メニューを削除 */
            menu.DropDownItems.Clear();

            /* パケット変換器一覧からメニューを作成 */
            foreach (var viewd in FormTaskManager.GetPacketConverterClasses()) {
                var menu_sub = new ToolStripMenuItem()
                {
                    Text = viewd.Name,
                    ToolTipText = viewd.Details,
                    Tag = viewd,
                };

                menu_sub.Click += OnMenuClick_PacketConverterAdd;

                menu.DropDownItems.Add(menu_sub);
            }
        }

        private void BuildPacketViewMenu(ToolStripMenuItem menu)
        {
            /* 全メニューを削除 */
            menu.DropDownItems.Clear();

            /* パケットビュー一覧からメニューを作成 */
            foreach (var viewd in FormTaskManager.GetPacketViewClasses()) {
                var menu_sub = new ToolStripMenuItem()
                {
                    Text = viewd.Name,
                    ToolTipText = viewd.Details,
                    Tag = viewd,
                };

                menu_sub.Click += OnMenuClick_PacketViewAdd;

                menu.DropDownItems.Add(menu_sub);
            }
        }

        public string SendTarget
        {
            get { return (SingleCmdPanel_Main.SendTarget); }
        }

        private void ApplyDataRateTarget()
        {
            var target = (PacketDataRateTarget)0;

            if (Menu_DataRate_SendData.Checked) {
                target |= PacketDataRateTarget.SendData;
            }

            if (Menu_DataRate_RecvData.Checked) {
                target |= PacketDataRateTarget.RecvData;
            }

            GatePacketManager.BasePacketManager.DataRateTarget = target;
        }

        public void UpdateTitle()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)UpdateTitle);
                return;
            }

            var title = new StringBuilder();

            title.Append(ConfigManager.Fixed.ApplicationName.Value);

            /* 管理者権限かどうかを確認 */
            if (Program.IsAdministratorMode) {
                title.Append(string.Format(" ({0})", ConfigManager.Language.MainUI.Title_AdminMode.Value));
            }

            /* プロファイルが読み込み専用かどうかを確認 */
            if (ConfigManager.User.ReadOnly.Value) {
                title.Append(" [Read-Only]");
            }

            Text = title.ToString();
        }

        public void UpdateMenuBar()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)UpdateMenuBar);
                return;
            }

            MenuBar_View_PacketConverterAdd.Enabled = FormTaskManager.CanAddPacketConverter;
            MenuBar_View_PacketViewAdd.Enabled = FormTaskManager.CanAddPacketView;
            MenuBar_View_AutoScroll.Checked = ConfigManager.System.AutoScroll.Value;
            MenuBar_Tool_AutoTimeStamp.Checked = ConfigManager.System.AutoTimeStamp.Enable.Value;
            MenuBar_Tool_ScriptManager.Checked = FormUiManager.ScriptWindowVisible();
        }

        public void UpdateStatusBar()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)UpdateStatusBar);
                return;
            }

            if (FormTaskManager.IsHispeedDraw) {
                Label_ViewDrawMode.Text = "High";
                Label_ViewDrawMode.BackColor = Color.Aqua;
            } else {
                Label_ViewDrawMode.Text = "Norm";
                Label_ViewDrawMode.BackColor = Color.LightCyan;
            }
        }

        private delegate void FormKeyActionHandler(MainWindowActionId id);
        public void FormKeyAction(MainWindowActionId id)
        {
            if (InvokeRequired) {
                Invoke((FormKeyActionHandler)FormKeyAction, id);
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
                    ConfigManager.SaveConfig(true);
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

                case MainWindowActionId.ShowScriptWindow:
                    FormUiManager.ScriptWindowVisible(true);
                    break;

                case MainWindowActionId.ShowOptionDialog:
                    FormUiManager.ShowOptionDialog();
                    break;

                case MainWindowActionId.ShowAppDocument:
                    FormUiManager.ShowAppDocument();
                    break;

                case MainWindowActionId.ShowAppDocument_PacketFilter:
                    FormUiManager.ShowAppDocument();
                    break;

                case MainWindowActionId.ShowAppInformation:
                    FormUiManager.ShowAppInfo();
                    break;
            }
        }

        private delegate void SetFormStatusHandler(FormStatus status);
        public void SetFormStatus(FormStatus status)
        {
            if (InvokeRequired) {
                BeginInvoke(new SetFormStatusHandler(SetFormStatus), status);
                return;
            }

            SetStatusText(status.MainStatusBar_Text);
            SetProgressBar(status.MainProgressBar_Visible, status.MainProgressBar_Value, 100);

            /* ステータスバーのカウンターを更新 */
            DDBtn_DataRate.Text = String.Format("Rate: {0,7}B/s", TextUtil.DecToText(status.PacketBytePSec_All));
            Label_PktCount_Cache.Text = String.Format("Cache: {0,9}", status.PacketCount_Cache);
            Label_PktCount_Raw.Text = String.Format("Raw: {0,9}", status.PacketCount_Raw);
            Label_PktCount_View.Text = String.Format("View: {0,9}", status.PacketCount_DrawAll);
            Label_PktCount_Busy.Text = String.Format("Busy: {0,9}", status.PacketCount_DrawBusy);

            /* 変換器のカウンターを更新 */
            Panel_Center.UpdatePacketConverterView();
        }

        private void UpdateProfileList()
        {
            MenuBar_ProfileList.BeginUpdate();
            {
                MenuBar_ProfileList.Items.Clear();
                MenuBar_ProfileList.Items.AddRange(ConfigManager.GetProfileList().ToArray());
                MenuBar_ProfileList.SelectedItem = ConfigManager.GetCurrentProfileID();
            }
            MenuBar_ProfileList.EndUpdate();
        }

        public void ClearPacketView()
        {
            Panel_Center.ClearPacketView();
        }

        public void AddPacketView(Guid class_id, PacketViewProperty viewp)
        {
            Panel_Center.AddPacketView(class_id, viewp);
        }

        public void ClearPacketConverter()
        {
            Panel_Center.ClearPacketConverter();
        }

        public void AddPacketConverter(Guid class_id, PacketConverterProperty pcvtp)
        {
            Panel_Center.AddPacketConverter(class_id, pcvtp);
        }

        private delegate void SetStatusTextDelegate(string text);
        public void SetStatusText(string text)
        {
            if (InvokeRequired) {
                Invoke(new SetStatusTextDelegate(SetStatusText), text);
                return;
            }

            Label_Status.Text = text;
        }

        private delegate void SetProgressBarDelegate(bool show, uint now, uint max);
        public void SetProgressBar(bool show, uint now, uint max)
        {
            if (InvokeRequired) {
                Invoke(new SetProgressBarDelegate(SetProgressBar), show, now, max);
                return;
            }

            PBar_Status.Visible = show;
            PBar_Status.Maximum = (int)max;
            PBar_Status.Value = Math.Min((int)now, PBar_Status.Maximum);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            /* 自動的に終了するのを禁止 */
            e.Cancel = true;

            /* アクションにするとアクション実行中はシャットダウンできなくなるので
             * アクションではなく直接シャットダウンする */
            Program.ShutdownRequest();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            SingleCmdPanel_Main.OnMainFormDeactivated();
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);

            SuspendLayout();
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);

            ResumeLayout(false);
            PerformLayout();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ConfigManager.System.MainWindow.Maximize.Value = (WindowState == FormWindowState.Maximized);
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menu) {
                if (menu.Tag is MainWindowActionId) {
                    FormKeyAction((MainWindowActionId)menu.Tag);
                }
            }
        }

        private void OnMenuClick_PacketConverterAdd(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menu) {
                if (menu.Tag is PacketConverterClass pcvtd) {
                    /* タグのクラスの変換器を追加 */
                    AddPacketConverter(pcvtd.ID, null);
                }
            }
        }

        private void OnMenuClick_PacketViewAdd(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menu) {
                if (menu.Tag is PacketViewClass viewc) {
                    /* タグのクラスのビューを追加 */
                    AddPacketView(viewc.ID, null);
                }
            }
        }

        private void MainFrame_KeyDown(object sender, KeyEventArgs e)
        {
            var config = ConfigManager.System.MainWindow.ShortcutKey.Value.Find(
                            value => (
                                   (value.KeyPattern.IsControl == e.Control)
                                && (value.KeyPattern.IsShift == e.Shift)
                                && (value.KeyPattern.IsAlt == e.Alt)
                                && (value.KeyPattern.KeyCode == e.KeyCode)));

            if (config == null)return;

            FormKeyAction(config.ActionID);
        }

        private void MainFrame_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainFrame_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] file_paths) {
                FormUiManager.FileOpen(file_paths);
            }
        }

        private void Label_ViewDrawMode_DoubleClick(object sender, EventArgs e)
        {
            FormTaskManager.HiSpeedDrawToggle();
        }

        private void OnDataRateTargetUpdate(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menu) {
                /* 選択したメニューのチェック状態を反転 */
                menu.Checked = !menu.Checked;

                /* 設定を適用 */
                ApplyDataRateTarget();
            }
        }

        private void MenuBar_Tool_ScriptIDE_Click(object sender, EventArgs e)
        {

        }

        private void MenuBar_ProfileList_DropDown(object sender, EventArgs e)
        {
            UpdateProfileList();
        }

        private void MenuBar_ProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MenuBar_ProfileList.SelectedItem is ConfigManager.ProfileData profile_next) {
                if (profile_next.ID != ConfigManager.GetCurrentProfileID()) {
                    Program.ChangeProfile(profile_next.ID);
                }
            }
        }

        private void MenuBar_Profile_OpenDir_Click(object sender, EventArgs e)
        {
            var profile_root = ConfigManager.GetProfileRootPath();

            if (!Directory.Exists(profile_root)) {
                Directory.CreateDirectory(profile_root);
            }

            System.Diagnostics.Process.Start(ConfigManager.GetProfileRootPath());
        }
    }
}
