using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Actions;
using Ratatoskr.Actions.ActionModules;
using Ratatoskr.Configs;
using Ratatoskr.PacketConverters;
using Ratatoskr.PacketViews;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
            InitializeTitle();
            InitializeMenuBar();

            SetStatusText("");
            SetProgressBar(false, 0, 0);

            UpdateMenuBar();
            UpdateStatusBar();

            Visible = false;
        }

        private void InitializeTitle()
        {
            var title = new StringBuilder();

            title.Append(ConfigManager.Fixed.ApplicationName.Value);

            /* 管理者権限かどうかを確認 */
            if ((new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator)) {
                title.Append(" (Administrator Mode)");
            }

            Text = title.ToString();
        }

        private void InitializeMenuBar()
        {
            foreach (ToolStripItem item in MBar_Main.Items) {
                InitializeMenuBarItem(item as ToolStripMenuItem);
            }
        }

        private void InitializeMenuBarItem(ToolStripMenuItem menu)
        {
            if (menu == null)return;

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

            /* 子持ちメニュー */
            if (menu.HasDropDownItems) {
                /* 共通イベントを削除 */
                menu.Click -= OnMenuClick;

                /* サブメニューに対して同等の処理 */
                foreach (ToolStripItem item in menu.DropDownItems) {
                    InitializeMenuBarItem(item as ToolStripMenuItem);
                }

                return;
            }

            /* 終端メニューの場合は共通イベントを設定 */
            menu.Click += OnMenuClick;

            /* メニューのタグをActionShortcutIdに変換 */
            if (menu.Tag is string) {
                var id = ActionShortcutId.None;

                if (Enum.TryParse<ActionShortcutId>(menu.Tag as string, out id)) {
                    menu.Tag = id;
                }
            }

            /* ショートカットテキストを設定 */
            if (menu.Tag is ActionShortcutId) {
                menu.ShortcutKeyDisplayString = ActionShortcutManager.ShortcutText((ActionShortcutId)menu.Tag);
            }
        }

        private void InitializePatternBox()
        {
        }

        private void InitializeFilterBox()
        {
        }

        private void BuildPacketConverterMenu(ToolStripMenuItem menu)
        {
            /* 全メニューを削除 */
            menu.DropDownItems.Clear();

            /* パケット変換器一覧からメニューを作成 */
            foreach (var viewd in FormTaskManager.GetPacketConverterClasses()) {
                var menu_sub = new ToolStripMenuItem();

                menu_sub.Text = viewd.Name;
                menu_sub.ToolTipText = viewd.Details;
                menu_sub.Tag = viewd;
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
                var menu_sub = new ToolStripMenuItem();

                menu_sub.Text = viewd.Name;
                menu_sub.ToolTipText = viewd.Details;
                menu_sub.Tag = viewd;
                menu_sub.Click += OnMenuClick_PacketViewAdd;

                menu.DropDownItems.Add(menu_sub);
            }
        }

        public void LoadConfig()
        {
            Panel_Center.LoadConfig();
            SingleCmdPanel_Main.LoadConfig();

            LoadWindowConfig();
        }

        private void LoadWindowConfig()
        {
            /* 最小/最大化を解除 */
            WindowState = FormWindowState.Normal;

            /* 設定を反映 */
            Size = ConfigManager.User.MainWindow.Size.Value;
            Location = ConfigManager.User.MainWindow.Position.Value;
            WindowState = (ConfigManager.User.MainWindow.Maximize.Value) ? (FormWindowState.Maximized) : (FormWindowState.Normal);
        }

        public void BackupConfig()
        {
            Panel_Center.BackupConfig();
            SingleCmdPanel_Main.BackupConfig();

            BackupWindowConfig();
        }

        private void BackupWindowConfig()
        {
            ConfigManager.User.MainWindow.Maximize.Value = (WindowState == FormWindowState.Maximized);

            /* 最小/最大化を解除 */
            WindowState = FormWindowState.Normal;

            ConfigManager.User.MainWindow.Size.Value = Size;
            ConfigManager.User.MainWindow.Position.Value = Location;
        }

        private delegate void UpdateMenuBarDelegate();
        public void UpdateMenuBar()
        {
            if (InvokeRequired) {
                Invoke(new UpdateMenuBarDelegate(UpdateMenuBar));
                return;
            }

            MenuBar_File_SavePacket_AutoSave.Checked = ConfigManager.User.Option.AutoSave.Value;
            MenuBar_Edit_TimeStamp_Auto.Checked = ConfigManager.User.Option.AutoTimeStamp.Value;
            MenuBar_View_PacketConverterAdd.Enabled = FormTaskManager.CanAddPacketConverter;
            MenuBar_View_PacketViewAdd.Enabled = FormTaskManager.CanAddPacketView;
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

        private delegate void SetPacketCounterDelegate(ulong count_base, ulong count_proc, ulong count_busy);
        public void SetPacketCounter(ulong count_base, ulong count_proc, ulong count_busy)
        {
            if (InvokeRequired) {
                BeginInvoke(new SetPacketCounterDelegate(SetPacketCounter), count_base, count_proc, count_busy);
                return;
            }

            /* ステータスバーのカウンターを更新 */
            Label_PktCount_Raw.Text = String.Format("Raw: {0,9}", count_base);
            Label_PktCount_View.Text = String.Format("View: {0,9}", count_proc);
            Label_PktCount_Busy.Text = String.Format("Busy: {0,9}", count_busy);

            /* 変換器のカウンターを更新 */
            Panel_Center.UpdatePacketConverterView();
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

            /* アプリケーション終了 */
            ActionManager.AddNormalAction(new Action_Shutdown());
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ConfigManager.User.MainWindow.Maximize.Value = (WindowState == FormWindowState.Maximized);
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;

            if (menu == null)return;
            if (!(menu.Tag is ActionShortcutId))return;

            ActionShortcutManager.Exec((ActionShortcutId)menu.Tag, OnActionCompleted);
        }

        private void OnActionCompleted(object sender, ActionObject.ActionResultStatus result)
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
    }
}
