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
using Ratatoskr.Config;
using Ratatoskr.Config.Data.User;
using Ratatoskr.PacketConverter;
using Ratatoskr.PacketView;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_FrameCenter : UserControl
    {
        private const string CONFIG_FILE_NAME = "setting-dock.xml";

        private enum DockContentsGroup
        {
            Fixed,
            PacketView,
        }


        private MainWindow_SendDataListPanel   MFDC_SendDataListPanel_Control = null;
		private MainWindow_SendTrafficPanel    MFDC_SendTrafficPanel_Control = null;
		private MainWindow_WatchListPanel      MFDC_WatchListPanel_Control = null;


        public MainWindow_FrameCenter()
        {
            InitializeComponent();

            DockPanel_Main.ShowDocumentIcon = true;
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
                "MFDC_SendDataListPanel_Control",
                ConfigManager.Language.MainUI.MCmdPanel_Title.Value,
                (uint)DockContentsGroup.Fixed,
                Icon.FromHandle(Ratatoskr.Resource.Images.memo_32x32.GetHicon()),
                DockAreas.Document | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottomAutoHide,
                false,
                MFDC_SendDataListPanel_Control = new MainWindow_SendDataListPanel());

#if DEBUG
			DockPanel_Main.AddDockContent(
				"MFDC_SendTrafficPanel_Control",
				ConfigManager.Language.MainUI.STPanel_Title.Value,
				(uint)DockContentsGroup.Fixed,
				Icon.FromHandle(Ratatoskr.Resource.Images.memo_32x32.GetHicon()),
				DockAreas.Document | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
				DockState.Document,
				false,
				MFDC_SendTrafficPanel_Control = new MainWindow_SendTrafficPanel());
#endif

			DockPanel_Main.AddDockContent(
                "MFDC_WatchListPanel",
                ConfigManager.Language.MainUI.WLPanel_Title.Value,
                (uint)DockContentsGroup.Fixed,
                Icon.FromHandle(Ratatoskr.Resource.Images.watch_32x32.GetHicon()),
                DockAreas.Document | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottomAutoHide,
                false,
                MFDC_WatchListPanel_Control = new MainWindow_WatchListPanel());

            MFDC_SendDataListPanel_Control.LoadConfig();
#if DEBUG
			MFDC_SendTrafficPanel_Control.LoadConfig();
#endif
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
            /* 設定ファイルから復元 */
            try {
                DockPanel_Main.ShowContents(ConfigManager.GetCurrentProfileFilePath(CONFIG_FILE_NAME, true));
            } catch (Exception) {
            }

            /* 設定ファイルから復元できなかったものはデフォルト値で初期化 */
//            foreach (var content in DockPanel_Main.GetDockContents()) {
//                if (content.DockState == DockState.Unknown) {
//                    content.Show(DockPanel_Main, content.DefaultDockState);
//                }
//            }

            /* --- 自前で初期化 --- */
//            MFDC_CmdListPanel.Show(DockPanel_Main, DockState.DockBottomAutoHide);
//            MFDC_RedirectListPanel.Show(DockPanel_Main, DockState.DockBottomAutoHide);
//            MFDC_DataListPanel.Show(DockPanel_Main, DockState.DockBottomAutoHide);
        }

        public void BackupConfig()
        {
            GatePanel_Main.BackupConfig();
            PacketConverter_Main.BackupConfig();
            
            MFDC_SendDataListPanel_Control?.BackupConfig();
			MFDC_SendTrafficPanel_Control?.BackupConfig();
			MFDC_WatchListPanel_Control?.BackupConfig();

            BackupDockConfig();
            BackupPacketViewConfig();
        }

        private void BackupDockConfig()
        {
            var config_dock = ConfigManager.GetCurrentProfileFilePath(CONFIG_FILE_NAME);

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
                    new PacketViewObjectConfig(
                        viewc.Instance.Class.ID,
                        viewc.Instance.ID,
                        viewc.Instance.Property));
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
            DockPanel_Main.RemoveDockContents((uint)DockContentsGroup.PacketView);
        }

        private void AddPacketView(Guid class_id, Guid obj_id, PacketViewProperty viewp, bool init)
        {
            var viewc = FormTaskManager.CreatePacketView(class_id, obj_id, viewp);

            if (viewc == null)return;

            /* Graphオブジェクトのレイアウトが何故か復元できないので、とりあえずパケットビューだけ復元対象から外す */
            DockPanel_Main.AddDockContent(
                                viewc.Instance.ID.ToString(),
//                                viewc.Instance.ID.ToString() + (new Random()).Next(0, 99999).ToString(),
                                viewc.Instance.Class.Name,
                                (uint)DockContentsGroup.PacketView,
                                Icon.FromHandle(Ratatoskr.Resource.Images.packet_view_16x16.GetHicon()),
                                DockAreas.Document,
                                DockState.Document,
                                true,
                                viewc);
        }

        public void AddPacketView(Guid class_id, PacketViewProperty viewp)
        {
            var config = new PacketViewObjectConfig();

            /* メニューからビュー追加を選んだときはすぐに初期化 */
            AddPacketView(class_id, config.ViewObjectID, viewp, true);
        }

        public void ClearPacketConverter()
        {
            PacketConverter_Main.ClearPacketConverter();
        }

        private void DockPanel_Main_DockContentClosed(object sender, Control control, FormClosedEventArgs e)
        {
            var viewc = control as PacketViewControl;

            if (viewc == null)return;

            FormTaskManager.RemovePacketView(viewc);

            viewc.Dispose();
        }
    }
}
