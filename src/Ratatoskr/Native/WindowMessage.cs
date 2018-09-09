using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Native
{
    internal enum WindowMessage : UInt32
    {
        WM_PAINT = 0x000F,

        WM_DEVICECHANGE = 0x0219,

        LVM_FIRST =              0x1000,      // ListView messages
        TV_FIRST  =              0x1100,      // TreeView messages
        HDM_FIRST =              0x1200,      // Header messages
        TCM_FIRST =              0x1300,      // Tab control messages

        PGM_FIRST =              0x1400,      // Pager control messages

        ECM_FIRST =              0x1500,      // Edit control messages
        BCM_FIRST =              0x1600,      // Button control messages
        CBM_FIRST =              0x1700,      // Combobox control messages
        CCM_FIRST =              0x2000,      // Common control shared messages


        LVM_SETITEMSTATE = (LVM_FIRST + 43),
    }
}
