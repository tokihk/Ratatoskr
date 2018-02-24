using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Actions;
using Ratatoskr.Actions.ActionModules;
using Ratatoskr.Configs;
using Ratatoskr.Configs.SystemConfigs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.FileFormats;
using Ratatoskr.Gate;
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet;
using Ratatoskr.PacketConverters;
using Ratatoskr.PacketViews;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
            InitializeMenuBar();

            SetStatusText("");
            SetProgressBar(false, 0, 0);

            Visible = false;
        }

        private void InitializeMenuBar()
        {
            void InitializeMenuBarItem(ToolStripMenuItem menu)
            {
                if (menu == null)return;

                /* 規定動作ありのメニュー */
                if (menu.Name == "MenuBar_File_Exit") {
                    return;
                }

                /* パケット変換器リスト */
                if (menu.Name == "MenuBar_View_PacketConverterAdd") {
                    BuildPacketConverterMenu(menu);
                    return;
                }

                /* パケットビューリスト */
                if (menu.Name == "MenuBar_View_PacketViewAdd") {
                    BuildPacketViewMenu(menu);
                    return;
                }

                /* プロファイルリスト */
                if (menu.Name == "MenuBar_ProfileList") {
                    return;
                }

                /* 子持ちメニュー */
                if (menu.HasDropDownItems) {
                    /* 共通イベントを削除 */
                    menu.Click -= OnMenuBarClick;

                    /* サブメニューに対して同等の処理 */
                    foreach (ToolStripItem item in menu.DropDownItems) {
                        InitializeMenuBarItem(item as ToolStripMenuItem);
                    }

                    return;
                }

                /* メニューのタグをActionShortcutIdに変換 */
                if (menu.Tag is string) {
                    var id = ActionShortcutId.None;

                    if (Enum.TryParse<ActionShortcutId>(menu.Tag as string, out id)) {
                        menu.Tag = id;
                    }

                    /* 終端メニューの場合は共通イベントを設定 */
                    menu.Click += OnMenuBarClick;
                }
            }

            foreach (ToolStripItem item in MBar_Main.Items) {
                InitializeMenuBarItem(item as ToolStripMenuItem);
            }
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

        public void LoadConfig()
        {
            LoadTitleText();

            LoadMenuBarConfig();
            LoadMenuBarShortcutConfig();

            Panel_Center.LoadConfig();
            SingleCmdPanel_Main.LoadConfig();

            LoadWindowConfig();

            UpdateMenuBar();
            UpdateStatusBar();
            UpdateProfileList();
        }

        private void LoadTitleText()
        {
            var title = new StringBuilder();

            title.Append(ConfigManager.Fixed.ApplicationName.Value);

            /* 管理者権限かどうかを確認 */
            if (Program.IsAdministratorMode) {
                title.Append(string.Format(" ({0})", ConfigManager.Language.MainUI.Title_AdminMode.Value));
            }

            Text = title.ToString();
        }

        private void LoadMenuBarConfig()
        {
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

        private void LoadMenuBarShortcutConfig()
        {
            void LoadMenuBarShortcutConfig(ToolStripMenuItem menu)
            {
                if (menu == null)return;

                if (menu.HasDropDownItems) {
                    /* 子持ちメニュー */
                    foreach (ToolStripItem sub_menu in menu.DropDownItems) {
                        LoadMenuBarShortcutConfig(sub_menu as ToolStripMenuItem);
                    }

                } else if (menu.Tag is ActionShortcutId) {
                    /* 機能が割り当てられたメニュー */
                    menu.ShortcutKeyDisplayString = ActionShortcutManager.ShortcutText((ActionShortcutId)menu.Tag);
                }
            }

            foreach (ToolStripItem item in MBar_Main.Items) {
                LoadMenuBarShortcutConfig(item as ToolStripMenuItem);
            }
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

        private delegate void UpdateMenuBarDelegate();
        public void UpdateMenuBar()
        {
            if (InvokeRequired) {
                Invoke(new UpdateMenuBarDelegate(UpdateMenuBar));
                return;
            }

            MenuBar_View_PacketConverterAdd.Enabled = FormTaskManager.CanAddPacketConverter;
            MenuBar_View_PacketViewAdd.Enabled = FormTaskManager.CanAddPacketView;
            MenuBar_Tool_AutoTimeStamp.Checked = ConfigManager.User.Option.AutoTimeStamp.Value;
            MenuBar_View_AutoScroll.Checked = ConfigManager.User.Option.AutoScroll.Value;
        }

        private delegate void UpdateStatusBarDelegate();
        public void UpdateStatusBar()
        {
            if (InvokeRequired) {
                Invoke(new UpdateStatusBarDelegate(UpdateStatusBar));
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
            Label_PktCount_Raw.Text = String.Format("Raw: {0,9}", status.PacketCount_All);
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

        public void AddPacketView(Guid class_id, ViewProperty viewp)
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

        public void AddPacket(string protocol, byte[] bitdata, uint bitsize)
        {
            Panel_Center.AddPacket(protocol, bitdata, bitsize);
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
            PBar_Status.Value = (int)now;
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ConfigManager.System.MainWindow.Maximize.Value = (WindowState == FormWindowState.Maximized);
        }

        private void OnMenuBarClick(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;

            if (menu == null)return;
            if (!(menu.Tag is ActionShortcutId))return;

            ActionShortcutManager.Exec((ActionShortcutId)menu.Tag, OnActionCompleted);
        }

        private void MenuBar_File_Exit_Click(object sender, EventArgs e)
        {
            /* アクションにするとアクション実行中はシャットダウンできなくなるので
             * アクションではなく直接シャットダウンする */
            Program.ShutdownRequest();
        }

        private void OnActionCompleted(object sender, ActionObject.ActionResultType result, ActionParam[] result_values)
        {
            UpdateMenuBar();
        }

        private void OnMenuClick_PacketConverterAdd(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;

            if (menu == null)return;

            var pcvtd = menu.Tag as PacketConverterClass;

            if (pcvtd == null)return;

            /* タグのクラスの変換器を追加 */
            AddPacketConverter(pcvtd.ID, null);
        }

        private void OnMenuClick_PacketViewAdd(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;

            if (menu == null)return;

            var viewd = menu.Tag as ViewClass;

            if (viewd == null)return;

            /* タグのクラスのビューを追加 */
            AddPacketView(viewd.ID, null);
        }

        private void MainFrame_KeyDown(object sender, KeyEventArgs e)
        {
            var config = ConfigManager.User.Option.ShortcutKey.Value.Find(
                            value => (
                                   (value.KeyPattern.IsControl == e.Control)
                                && (value.KeyPattern.IsShift == e.Shift)
                                && (value.KeyPattern.IsAlt == e.Alt)
                                && (value.KeyPattern.KeyCode == e.KeyCode)));

            if (config == null)return;

            ActionShortcutManager.Exec(config.ActionID, OnActionCompleted);
        }

        private void MainFrame_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainFrame_DragDrop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (files == null)return;

            FormUiManager.FileOpen(files, null);
        }

        private void Label_ViewDrawMode_DoubleClick(object sender, EventArgs e)
        {
            FormTaskManager.HiSpeedDrawToggle();
        }

        private void OnDataRateTargetUpdate(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;

            if (menu == null)return;

            /* 選択したメニューのチェック状態を反転 */
            menu.Checked = !menu.Checked;

            /* 設定を適用 */
            ApplyDataRateTarget();
        }

        private void MenuBar_ProfileList_DropDown(object sender, EventArgs e)
        {
            UpdateProfileList();
        }

        private void MenuBar_ProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var profile_next = MenuBar_ProfileList.SelectedItem as ConfigManager.ProfileInfo;

            if (profile_next == null)return;

            if (profile_next.ID != ConfigManager.GetCurrentProfileID()) {
                Program.ChangeProfile(profile_next.ID);
            }
        }

        private void MenuBar_Profile_Edit_Click(object sender, EventArgs e)
        {
            var dialog = new Dialog.ProfileConfigDialog(ConfigManager.User);

            if (dialog.ShowDialog() != DialogResult.OK)return;

            ConfigManager.SaveCurrentProfile(false);

            UpdateProfileList();
        }

        private void MenuBar_Profile_New_Click(object sender, EventArgs e)
        {
            if (!FormUiManager.ConfirmMessageBox(ConfigManager.Language.MainMessage.Confirm_CreateNewProfile.Value))return;

            var dialog = new Dialog.ProfileConfigDialog(new UserConfig());

            if (dialog.ShowDialog() != DialogResult.OK)return;

            Program.ChangeProfile(ConfigManager.CreateNewProfile(dialog.Config));
        }

        private void MenuBar_Profile_Export_Click(object sender, EventArgs e)
        {
            /* 現在の状態を保存する */
            ConfigManager.SaveConfig();

            var writer_info = FormUiManager.CreateUserConfigWriter();

            if (writer_info.writer == null)return;

            /* 出力設定 */
            writer_info.option.TargetProfileID = ConfigManager.GetCurrentProfileID();

            if (!writer_info.writer.Open(writer_info.option, writer_info.path))return;

            writer_info.writer.Save();

            writer_info.writer.Close();
        }

        private void MenuBar_Profile_OpenDir_Click(object sender, EventArgs e)
        {
            var profile_root = ConfigManager.GetProfileRootPath();

            if (!Directory.Exists(profile_root)) {
                Directory.CreateDirectory(profile_root);
            }

            System.Diagnostics.Process.Start(ConfigManager.GetProfileRootPath());
        }

        private void MenuBar_Export_UserSetting_Click(object sender, EventArgs e)
        {
            MenuBar_Profile_Export_Click(sender, e);
        }
    }
}
