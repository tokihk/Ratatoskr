using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Framework.Device;
using RtsCore.Framework.FileFormat;
using RtsCore.Framework.PacketView;
using RtsCore.Framework.Plugin;
using RtsCore.Packet;
using RtsCore.Protocol;
using RtsPlugin.Pcap.ProtocolAnalyzer;

namespace RtsPlugin.Pcap
{
    public class PluginInstanceImpl : PluginInstance
    {
        private ProtocolAnalyzeManager pranm_ = null;


        public PluginInstanceImpl(PluginClass plgc) : base(plgc)
        {
            Interface.DevicePacketCaptureHandlers = new PacketEventHandler[]
            {
                OnDevicePacketEntry
            };

            Interface.DeviceClasses = new DeviceClass[]
            {
                new Devices.EthernetCapture.DeviceClassImpl(),
                new Devices.UsbCapture.DeviceClassImpl()
            };

            Interface.PacketViewClasses = new PacketViewClass[]
            {
                new PacketViews.Wireshark.PacketViewClassImpl(),
            };

            Interface.PacketLogFormatClasses = new FileFormatClass[]
            {
                new FileFormats.PacketLog_Pcap.FileFormatClassImpl(),
            };

            Interface.ProtocolDecoderClasses = new ProtocolDecoderClass[]
            {
                new Protocols.EthenetII.ProtocolDecoderClassImpl()
            };

            Interface.MenuBarItem = new ToolStripMenuItem("Wireshark startup", null, OnWiresharkStartup);

            pranm_ = new ProtocolAnalyzeManager();
        }

        public void OnDevicePacketEntry(IEnumerable<PacketObject> packets)
        {
            pranm_.InputPacket(packets);
        }

        private void OnWiresharkStartup(object sender, EventArgs ev)
        {
            pranm_.AnlyzerStartup();
        }
    }
}