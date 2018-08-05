using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Packet;

namespace Ratatoskr.FileFormats.PacketLog_Ncom
{
    /* [MODE] */
    internal enum NcomMode
    {
        NXDN_Conv           = 1,
        NXDN_Trunking_TypeC = 2,
        NXDN_Trunking_TypeD = 3,
        dPMR_Conventional   = 6,
        dPMR_Trunking       = 7,
    }

    /* [TYPE] */
    internal enum NcomType
    {
        RxMonitor,
        TxMonitor,
    }

    /* [LINK] */
    internal enum NcomLink
    {
        Unknown,
    }

    /* [TRGT] */
    internal enum NcomTarget
    {
        SU       = 0,
        Repeater = 1,
    }

    /* [SQMD] */
    internal enum NcomSequenceMode
    {
        Normal,
        Sequence,
    }

    internal sealed class FileFormatOptionImpl : FileFormatOption
    {
        public NcomMode          MODE { get; set; } = NcomMode.NXDN_Conv;
        public NcomType          TYPE { get; set; } = NcomType.TxMonitor;
        public NcomLink          LINK { get; set; } = NcomLink.Unknown;
        public NcomTarget        TRGT { get; set; } = NcomTarget.Repeater;
        public NcomSequenceMode  SQMD { get; set; } = NcomSequenceMode.Sequence;


        public FileFormatOptionImpl()
        {
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (null);
//            return (new FileFormatOptionEditorImpl(this));
        }
    }
}
