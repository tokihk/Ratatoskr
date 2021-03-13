using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Packet;
using RtsCore.Protocol;
using RtsCore.Framework.Device;
using RtsCore.Framework.FileFormat;
using RtsCore.Framework.PacketView;

namespace RtsCore.Framework.Plugin
{
    public class PluginInterface
    {
        public PacketEventHandler[]     DevicePacketCaptureHandlers = null;

        public DeviceClass[]            DeviceClasses = null;

        public PacketViewClass[]        PacketViewClasses = null;

        public FileFormatClass[]        PacketLogFormatClasses = null;

        public ProtocolEncoderClass[]   ProtocolEncoderClasses = null;

        public ProtocolDecoderClass[]   ProtocolDecoderClasses = null;

        public ToolStripMenuItem        MenuBarItem = null;
    }
}
