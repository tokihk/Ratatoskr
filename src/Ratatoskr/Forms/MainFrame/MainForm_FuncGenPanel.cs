using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Utility;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainForm_FuncGenPanel : UserControl
    {
        private enum ListViewDataType { Protocol, BitData, BitSize }


        public MainForm_FuncGenPanel()
        {
            InitializeComponent();
            InitializePacketList();
        }

        private void InitializePacketList()
        {
        }

        public void LoadConfig()
        {
        }

        public void BackupConfig()
        {
        }

        private string GetPacketListItemText(PacketObjectConfig config, ListViewDataType type)
        {
            switch (type) {
                case ListViewDataType.Protocol:     return (config.ProtocolName);
                case ListViewDataType.BitData:      return (HexTextEncoder.ToHexText(config.BitData));
                case ListViewDataType.BitSize:      return (config.BitSize.ToString());
                default:                            return ("");
            }
        }

        private void UpdatePacketListItemView(ListViewItem item)
        {
            var config = item.Tag as PacketObjectConfig;

            if (config == null)return;

            /* プロトコル名 */
            item.Text = GetPacketListItemText(config, ListViewDataType.Protocol);

            /* データ */
            foreach (ListViewItem.ListViewSubItem item_sub in item.SubItems) {
                if (item_sub.Tag == null)continue;

                item_sub.Text = GetPacketListItemText(config, (ListViewDataType)item_sub.Tag);
            }
        }

        public void AddPacket(string protocol, byte[] bitdata, uint bitsize)
        {
        }
    }
}
