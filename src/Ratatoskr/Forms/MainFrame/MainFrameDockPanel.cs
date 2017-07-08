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


        private MainFrameSequentialCommandPanel MFDC_CmdListPanel_Control;

        private MainFrameGateRedirectPanel MFDC_RedirectListPanel_Control;


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
                "コマンドリスト",
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottomAutoHide,
                false,
                MFDC_CmdListPanel_Control = new MainFrameSequentialCommandPanel());

            AddDockContent(
                "MFDC_RedirectListPanel",
                "リダイレクトリスト",
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottomAutoHide,
                false,
                MFDC_RedirectListPanel_Control = new MainFrameGateRedirectPanel());
        }

        public void LoadConfig()
        {
            MFDC_CmdListPanel_Control.LoadConfig();
            MFDC_RedirectListPanel_Control.LoadConfig();

            LoadPacketViewConfig();
            LoadDockConfig();
        }

        private void LoadDockConfig()
        {
            var config_dock = ConfigManager.User.GetProfilePath(CONFIG_FILE_NAME, true);

            /* 設定ファイルから復元 */
            if (config_dock != null) {
                DockPanel_Main.LoadFromXml(config_dock, GetDockContentFromPersistString);
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
            MFDC_RedirectListPanel_Control.BackupConfig();

            BackupPacketViewConfig();
            BackupDockConfig();
        }

        private void BackupDockConfig()
        {
            var config_dock = ConfigManager.User.GetProfilePath(CONFIG_FILE_NAME);

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
            foreach (var viewi in FormTaskManager.GetPacketViewInstances()) {
                viewi.BackupProperty();
                ConfigManager.User.PacketView.Value.Add(
                    new PacketViewObjectConfig(viewi.Class.ID, viewi.ID, viewi.Property));
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
            var viewi = FormTaskManager.CreatePacketView(class_id, obj_id, viewp);

            if (viewi == null)return;

            var content_info = AddDockContent(
                                    viewi.ID.ToString(),
                                    viewi.Class.Name,
                                    DockAreas.Document,
                                    DockState.Document,
                                    true,
                                    new MainFramePacketView(viewi));
            
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

            var view = dock.ContentControl as MainFramePacketView;

            if (view == null)return;

            FormTaskManager.RemovePacketView(view.Instance);
        }
    }
}
