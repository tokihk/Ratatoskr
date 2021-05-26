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
		private enum TabPageId
		{
			PacketView,
			ScriptEditor,
		}


		private MainWindow_PacketView				TabContent_PacketView;
		private ScriptWindow.ScriptWindow_Control	TabContent_ScriptEditor;


        public MainWindow_FrameCenter()
        {
            InitializeComponent();

			TabContent_PacketView = new MainWindow_PacketView();
			TabContent_PacketView.Dock = DockStyle.Fill;

			TabContent_ScriptEditor = new ScriptWindow.ScriptWindow_Control();
			TabContent_ScriptEditor.Dock = DockStyle.Fill;

			InitializeTabList();

			UpdateTabView();
        }

		private void InitializeTabList()
		{
			var page_list = new []
			{
				new { text = "Packet View",   id = TabPageId.PacketView   },
//				new { text = "Script Editor", id = TabPageId.ScriptEditor },
			};

			TabPage page;

			foreach (var page_data in page_list) {
				page = new TabPage();
				page.Text = page_data.text;
				page.Tag  = page_data.id;
				
				Tab_Frame.TabPages.Add(page);
			}

			Tab_Frame.SelectedIndex = 0;
		}

        public void LoadConfig()
        {
            GatePanel_Main.LoadConfig();

			TabContent_PacketView.LoadConfig();
			TabContent_ScriptEditor.LoadConfig();
        }

        public void BackupConfig()
        {
            GatePanel_Main.BackupConfig();
            TabContent_PacketView.BackupConfig();
			TabContent_ScriptEditor.BackupConfig();
        }

		private void UpdateTabView()
		{
			var tab = Tab_Frame.SelectedTab;

			if (tab == null)return;

			var control_old = (Panel_Frame.Controls.Count == 0) ? (null) : (Panel_Frame.Controls[0]);
			var control_new = control_old;

			switch ((TabPageId)tab.Tag) {
				case TabPageId.PacketView:		control_new = TabContent_PacketView;	break;
				case TabPageId.ScriptEditor:	control_new = TabContent_ScriptEditor;	break;
			}

			if (control_new == control_old)return;

			Panel_Frame.Controls.Clear();
			Panel_Frame.Controls.Add(control_new);
		}

        public void UpdatePacketConverterView()
        {
			TabContent_PacketView.UpdatePacketConverterView();
        }

        public void AddPacketConverter(Guid class_id, PacketConverterProperty pcvtp)
        {
			TabContent_PacketView.AddPacketConverter(class_id, pcvtp);
        }

        public void ClearPacketView()
        {
			TabContent_PacketView.ClearPacketView();
        }

        private void AddPacketView(Guid class_id, Guid obj_id, PacketViewProperty viewp, bool init)
        {
			TabContent_PacketView.AddPacketView(class_id, obj_id, viewp, init);
        }

        public void AddPacketView(Guid class_id, PacketViewProperty viewp)
        {
            var config = new PacketViewObjectConfig();

            /* メニューからビュー追加を選んだときはすぐに初期化 */
            AddPacketView(class_id, config.ViewObjectID, viewp, true);
        }

        public void ClearPacketConverter()
        {
			TabContent_PacketView.ClearPacketConverter();
        }

		private void Tab_Frame_Selected(object sender, TabControlEventArgs e)
		{
			UpdateTabView();
		}
	}
}
