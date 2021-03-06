﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.General.Packet;

namespace Ratatoskr.Gate.AutoLogger
{
    internal sealed class AutoLogObject_FileSize : AutoLogObject
    {
        public AutoLogObject_FileSize()
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
