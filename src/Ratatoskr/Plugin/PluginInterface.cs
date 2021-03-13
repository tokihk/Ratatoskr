using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.General.Packet;
using RtsCore.Protocol;
using Ratatoskr.Device;
using Ratatoskr.FileFormat;
using Ratatoskr.PacketView;

namespace Ratatoskr.Plugin
{
    internal class PluginInterface
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
