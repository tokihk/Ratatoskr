using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Forms
{
    [Serializable]
    internal sealed class FormStatus
    {
        public string MainStatusBar_Text = "";

        public bool   MainProgressBar_Visible = false;
        public byte   MainProgressBar_Value   = 0;

        public ulong  PacketCount_All      = 0;
        public ulong  PacketCount_DrawAll  = 0;
        public ulong  PacketCount_DrawBusy = 0;

        public ulong  PacketBytePSec_All = 0;
    }
}
