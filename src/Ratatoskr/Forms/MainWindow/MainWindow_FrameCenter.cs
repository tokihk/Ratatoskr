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
using Ratatoskr.PacketConverters;
using Ratatoskr.PacketViews;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_FrameCenter : UserControl
    {
        private const string CONFIG_FILE_NAME = "setting-dock.xml";


        private MainWindow_SendDataListPanel MFDC_CmdListPanel_Control;
        private MainWindow_WatchListPanel    MFDC_WatchListPanel_Control;


        public MainWindow_FrameCenter()
        {
            InitializeComponent();
        }

        public void LoadConfig()
        {
            GatePanel_Main.LoadConfig();
            PacketConverter_Main.LoadConfig();

            LoadDockPanelConfig();
            LoadPacketViewConfig();
            LoadDockConfig();
        }

        public void LoadDockPanelConfig()
        {
            DockPanel_Main.ClearDockContents();

            DockPanel_Main.AddDockContent(
                "MFDC_CmdListPanel",
                ConfigManager.Language.MainUI.MCmdPanel_Title.Value,
                Icon.FromHandle(Properties.Resources.memo_32x32.GetHicon()),
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottomAutoHide,
                false,
                MFDC_CmdListPanel_Control = new MainWindow_SendDataListPanel());

            DockPanel_Main.AddDockContent(
                "MFDC_WatchListPanel",
                ConfigManager.Language.MainUI.WLPanel_Title.Value,
                Icon.FromHandle(Properties.Resources.watch_32x32.GetHicon()),
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottomAutoHide,
                false,
                MFDC_WatchListPanel_Control = new MainWindow_WatchListPanel());

            MFDC_CmdListPanel_Control.LoadConfig();
            MFDC_WatchListPanel_Control.LoadConfig();
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

        private void LoadDockConfig()
        {
            var config_dock = Program.GetWorkspaceDirectory(CONFIG_FILE_NAME);

            /* 設定ファイルから復元 */
            if (   (config_dock != null)
                && (File.Exists(config_dock))
            ) {
                try {
                    DockPanel_Main.LoadFromXml(config_dock);
                } catch (Exception) {
                }
            }

            /* 設定ファイルから復元できなかったものはデフォルト値で初期化 */
            foreach (var content in DockPanel_Main.GetDockContents()) {
                if (content.DockState == DockState.Unknown) {
                    content.Show(DockPanel_Main, content.DefaultDockState);
                }
            }

            /* --- 自前で初期化 --- */
//            MFDC_CmdListPanel.Show(DockPanel_Main, DockState.DockBottomAutoHide);
//            MFDC_RedirectListPanel.Show(DockPanel_Main, DockState.DockBottomAutoHide);
//            MFDC_DataListPanel.Show(DockPanel_Main, DockState.DockBottomAutoHide);
        }

        public void BackupConfig()
        {
            GatePanel_Main.BackupConfig();
            PacketConverter_Main.BackupConfig();

            BackupDockConfig();
            BackupPacketViewConfig();
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

        public void UpdatePacketConverterView()
        {
            PacketConverter_Main.UpdateView();
        }

        public void AddPacketConverter(Guid class_id, PacketConverterProperty pcvtp)
        {
            PacketConverter_Main.AddPacketConverter(class_id, pcvtp);
        }

        public void ClearPacketView()
        {
            DockPanel_Main.ClearDockContents();
        }

        private void AddPacketView(Guid class_id, Guid obj_id, ViewProperty viewp, bool init)
        {
            var viewc = FormTaskManager.CreatePacketView(class_id, obj_id, viewp);

            if (viewc == null)return;

            /* Graphオブジェクトのレイアウトが何故か復元できないので、とりあえずパケットビューだけ復元対象から外す */
            DockPanel_Main.AddDockContent(
                                viewc.Instance.ID.ToString() + (new Random()).Next(0, 99999).ToString(),
                                viewc.Instance.Class.Name,
                                Icon.FromHandle(Properties.Resources.memo_32x32.GetHicon()),
                                DockAreas.Document,
                                DockState.Document,
                                true,
                                viewc);
        }

        public void AddPacketView(Guid class_id, ViewProperty viewp)
        {
            /* メニューからビュー追加を選んだときはすぐに初期化 */
            AddPacketView(class_id, Guid.NewGuid(), viewp, true);
        }

        public void ClearPacketConverter()
        {
            PacketConverter_Main.ClearPacketConverter();
        }

        private void DockPanel_Main_DockContentClosed(object sender, Control control, FormClosedEventArgs e)
        {
            var viewc = control as ViewControl;

            if (viewc == null)return;

            FormTaskManager.RemovePacketView(viewc);

            viewc.Dispose();
        }
    }
}
