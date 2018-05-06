using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Packet;

namespace Ratatoskr.Gate.AutoPacketSave
{
    internal sealed class AutoPacketSaveObject_FileSize : AutoPacketSaveObject
    {
        public AutoPacketSaveObject_FileSize()
        {
        }

        protected override void OnOutput(IEnumerable<PacketObject> packets)
        {
            WritePacket(packets);

            if (GetOutputFileSize() >= ConfigManager.System.AutoPacketSave.SaveValue_FileSize.Value) {
                ChangeNewFile();
            }
        }
    }
}
