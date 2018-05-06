using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Packet;

namespace Ratatoskr.FileFormats.PacketLog_Csv
{
    internal enum TextCharCode
    {
        UTF8,
        ShiftJIS,
        UnicodeB,
        UnicodeL,
    }

    internal sealed class FileFormatOptionImpl : FileFormatOption
    {
        public TextCharCode          CharCode  { get; set; } = TextCharCode.UTF8;
        public List<PacketElementID> ItemOrder { get; }      = new List<PacketElementID>();


        public FileFormatOptionImpl()
        {
            ItemOrder.Add(PacketElementID.Facility);
            ItemOrder.Add(PacketElementID.Alias);
            ItemOrder.Add(PacketElementID.Priority);
            ItemOrder.Add(PacketElementID.Attribute);
            ItemOrder.Add(PacketElementID.DateTime);
            ItemOrder.Add(PacketElementID.Information);
            ItemOrder.Add(PacketElementID.Direction);
            ItemOrder.Add(PacketElementID.Source);
            ItemOrder.Add(PacketElementID.Destination);
            ItemOrder.Add(PacketElementID.Mark);
            ItemOrder.Add(PacketElementID.Data);
        }

        public override FileFormatOptionEditor GetEditor()
        {
            return (null);
//            return (new FileFormatOptionEditorImpl(this));
        }
    }
}
