using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.PacketViews;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrameDockPanel : UserControl
    {
        private class DockContentInfo
        {
            public DockContentInfo(DockContent content, DockState default_state)
            {
                Content = content;
                DefaultState = default_state;
            }

            public DockContent Content      { get; }
            public DockState   DefaultState { get; }
        }


        private const string CONFIG_FILE_NAME = "setting-dock.xml";


        private DockPanel DockPanel_Main;
        private Dictionary<string, DockContentInfo> DockContents = new Dictionary<string, DockContentInfo>();


        private MainFrameSendDataListPanel MFDC_CmdListPanel_Control;
        private MainFrameWatchListPanel    MFDC_WatchListPanel_Control;


        public MainFrameDockPanel()
        {
            InitializeComponent();
            InitializeDockPanel();
        }

        public void InitializeDockPanel()
        {
            DockPanel_Main = new DockPanel();
            DockPanel_Main.BorderStyle = BorderStyle.None;
            DockPanel_Main.Dock = DockStyle.Fill;
            DockPanel_Main.DocumentStyle = DocumentStyle.DockingWindow;
            Controls.Add(DockPanel_Main);

            AddDockContent(
                "MFDC_CmdListPanel",
                ConfigManager.Language.MainUI.MCmdPanel_Title.Value,
                Icon.FromHandle(Properties.Resources.memo_32x32.GetHicon()),
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottomAutoHide,
                false,
                MFDC_CmdListPanel_Control = new MainFrameSendDataListPanel());

            AddDockContent(
                "MFDC_WatchListPanel",
                ConfigManager.Language.MainUI.WLPanel_Title.Value,
                Icon.FromHandle(Properties.Resources.watch_32x32.GetHicon()),
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottomAutoHide,
                false,
                MFDC_WatchListPanel_Control = new MainFrameWatchListPanel());
        }

        public void LoadConfig()
        {
            MFDC_CmdListPanel_Control.LoadConfig();

            LoadPacketViewConfig();
            LoadDockConfig();
        }

        private void LoadDockConfig()
        {
            var config_dock = Program.GetWorkspaceDirectory(CONFIG_FILE_NAME);

            /* 設定ファイルから復元 */
            if (   (config_dock != null)
                && (File.Exists(config_dock))
            ) {
                try {
                    DockPanel_Main.LoadFromXml(config_dock, GetDockContentFromPersistString);
                } catch (Exception) {
                }
            }

            /* 設定ファイルから復元できなかったものはデフォルト値で初期化 */
            foreach (var content in DockContents.Values) {
                if (content.Content.DockState == DockState.Unknown) {
                    content.Content.Show(DockPanel_Main, content.DefaultState);
                }
            }

            /* --- 自前で初期化 --- */
//            MFDC_CmdListPanel.Show(DockPanel_Main, DockState.DockBottomAutoHide);
//            MFDC_RedirectListPanel.Show(DockPanel_Main, DockState.DockBottomAutoHide);
//            MFDC_DataListPanel.Show(DockPanel_Main, DockState.DockBottomAutoHide);
        }

        private void LoadPacketViewConfig()
        {
            /* 全ビュー解放 */
            ClearPacketView();

            /* コンフィグからパケットビューを復元 */
            ConfigManager.User.PacketView.Value.ForEach(
                config => AddPacketView(
                    config.ViewClassID, config.ViewObjectID, config.ViewProperty, false));
        }

        public void BackupConfig()
        {
            MFDC_CmdListPanel_Control.BackupConfig();
            MFDC_WatchListPanel_Control.BackupConfig();

            BackupPacketViewConfig();
            BackupDockConfig();
        }

        private void BackupDockConfig()
        {
            var config_dock = Program.GetWorkspaceDirectory(CONFIG_FILE_NAME);

            if (config_dock != null) {
                Directory.CreateDirectory(Path.GetDirectoryName(config_dock));
                DockPanel_Main.SaveAsXml(config_dock);
            }
        }

        private void BackupPacketViewConfig()
        {
            /* 全設定を削除 */
            ConfigManager.User.PacketView.Value.Clear();

            /* パケットビューのみをスキャン */
            foreach (var viewc in FormTaskManager.GetPacketViewControls()) {
                viewc.BackupProperty();
                ConfigManager.User.PacketView.Value.Add(
                    new PacketViewObjectConfig(viewc.Instance.Class.ID, viewc.Instance.ID, viewc.Instance.Property));
            }
        }

        private IDockContent GetDockContentFromPersistString(string persistString)
        {
            var content = (DockContentInfo)null;
            
            if (!DockContents.TryGetValue(persistString, out content))return (null);

            return (content.Content);
        }

        private DockContentInfo AddDockContent(string name, string title, Icon icon, DockAreas areas, DockState state, bool can_close, Control control)
        {
            var content = new MainFrameDockContent(name, this);

            content.Text = title;
            content.Icon = icon;
            content.DockAreas = areas;
            content.CloseButtonVisible = can_close;
            content.SetupControl(control);

            var content_info = new DockContentInfo(content, state);

            DockContents.Add(name, content_info);

            return (content_info);
        }

        private DockContentInfo AddDockContent(string name, string title, DockAreas areas, DockState state, bool can_close, Control control)
        {
            return (AddDockContent(name, title, null, areas, state, can_close, control));
        }

        public void ClearPacketView()
        {
            foreach (var doc in DockPanel_Main.Documents) {
                doc.DockHandler.Close();
            }
        }

        private void AddPacketView(Guid class_id, Guid obj_id, ViewProperty viewp, bool init)
        {
            var viewc = FormTaskManager.CreatePacketView(class_id, obj_id, viewp);

            if (viewc == null)return;

            /* Graphオブジェクトのレイアウトが何故か復元できないので、とりあえずパケットビューだけ復元対象から外す */
            var content_info = AddDockContent(
//                                    viewi.ID.ToString(),
                                    viewc.Instance.ID.ToString() + (new Random()).Next(0, 99999).ToString(),
                                    viewc.Instance.Class.Name,
                                    DockAreas.Document,
                                    DockState.Document,
                                    true,
                                    viewc);
            
            if ((content_info != null) && (init)) {
                content_info.Content.Show(DockPanel_Main, content_info.DefaultState);
            }
        }

        public void AddPacketView(Guid class_id, ViewProperty viewp)
        {
            /* メニューからビュー追加を選んだときはすぐに初期化 */
            AddPacketView(class_id, Guid.NewGuid(), viewp, true);
        }

        public void AddPacket(string protocol, byte[] bitdata, uint bitsize)
        {
        }

        public void OnChildFormClosed(object sender, FormClosedEventArgs e)
        {
            var dock = sender as MainFrameDockContent;

            if (dock == null)return;

            var viewc = dock.ContentControl as ViewControl;

            if (viewc == null)return;

            FormTaskManager.RemovePacketView(viewc);
        }
    }
}
