using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Forms
{
    internal sealed class FormStatus
    {
        public string MainStatusBar_Text = "";

        public bool   MainProgressBar_Visible = false;
        public byte   MainProgressBar_Value   = 0;

        public ulong  PacketCount_Cache    = 0;
        public ulong  PacketCount_Raw      = 0;
        public ulong  PacketCount_DrawAll  = 0;
        public ulong  PacketCount_DrawBusy = 0;

        public ulong  PacketBytePSec_All = 0;


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var obj_c = obj as FormStatus;

            if (obj_c != null) {
                return (   (MainStatusBar_Text == obj_c.MainStatusBar_Text)
                        && (MainProgressBar_Visible == obj_c.MainProgressBar_Visible)
                        && (MainProgressBar_Value == obj_c.MainProgressBar_Value)
                        && (PacketCount_Cache == obj_c.PacketCount_Cache)
                        && (PacketCount_Raw == obj_c.PacketCount_Raw)
                        && (PacketCount_DrawAll == obj_c.PacketCount_DrawAll)
                        && (PacketCount_DrawBusy == obj_c.PacketCount_DrawBusy)
                        && (PacketBytePSec_All == obj_c.PacketBytePSec_All)
                    );
            }

            return base.Equals(obj);
        }

        public void CopyTo(FormStatus obj)
        {
            obj.MainStatusBar_Text = string.Copy(MainStatusBar_Text);
            obj.MainProgressBar_Visible = MainProgressBar_Visible;
            obj.MainProgressBar_Value = MainProgressBar_Value;
            obj.PacketCount_Cache = PacketCount_Cache;
            obj.PacketCount_Raw = PacketCount_Raw;
            obj.PacketCount_DrawAll = PacketCount_DrawAll;
            obj.PacketCount_DrawBusy = PacketCount_DrawBusy;
            obj.PacketBytePSec_All = PacketBytePSec_All;
        }
    }
}
