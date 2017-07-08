using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.PacketConverters;
using Ratatoskr.PacketViews;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrameCenter : UserControl
    {
        public MainFrameCenter()
        {
            InitializeComponent();
        }

        public void LoadConfig()
        {
            GatePanel_Main.LoadConfig();
            DockPanel_Main.LoadConfig();
            PacketConverter_Main.LoadConfig();
        }

        public void BackupConfig()
        {
            GatePanel_Main.BackupConfig();
            DockPanel_Main.BackupConfig();
            PacketConverter_Main.BackupConfig();
        }

        public void ClearPacketView()
        {
            DockPanel_Main.ClearPacketView();
        }

        public void AddPacketView(Guid class_id, ViewProperty viewp)
        {
            DockPanel_Main.AddPacketView(class_id, viewp);
        }

        public void ClearPacketConverter()
        {
            PacketConverter_Main.ClearPacketConverter();
        }

        public void AddPacketConverter(Guid class_id, PacketConverterProperty pcvtp)
        {
            PacketConverter_Main.AddPacketConverter(class_id, pcvtp);
        }

        public void UpdatePacketConverterView()
        {
            PacketConverter_Main.UpdateView();
        }

        public void AddPacket(string protocol, byte[] bitdata, uint bitsize)
        {
            DockPanel_Main.AddPacket(protocol, bitdata, bitsize);
        }
    }
}
