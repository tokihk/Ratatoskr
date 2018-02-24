using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Gate.PacketAutoSave
{
    internal sealed class PacketAutoSaveObject_FileSize : PacketAutoSaveObject
    {
        public PacketAutoSaveObject_FileSize()
        {
        }

        protected override void OnOutput(IEnumerable<PacketObject> packets)
        {
            WritePacket(packets);

            if (GetOutputFileSize() >= ConfigManager.User.Option.AutoSaveValue_FileSize.Value) {
                ChangeNewFile();
            }
        }
    }
}
