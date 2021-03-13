using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.PacketConverter;

namespace Ratatoskr.PacketConverter.Grouping
{
    internal sealed class PacketConverterClassImpl : PacketConverterClass
    {
        public static readonly Guid ClassID = new Guid("EF01E50E-631B-4230-AD2D-3D8A674F3033");


        public PacketConverterClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("Grouping"); }
        }

        public override string Details
        {
            get { return ("Specify the boundary of the packet."); }
        }

        public override Type GetPropertyType()
        {
            return (typeof(PacketConverterPropertyImpl));
        }

        public override PacketConverterProperty CreateProperty()
        {
            return (new PacketConverterPropertyImpl());
        }

        protected override PacketConverterInstance OnCreateInstance(PacketConvertManager pcvtm, Guid obj_id, PacketConverterProperty pcvtp)
        {
            return (new PacketConverterInstanceImpl(pcvtm, this, pcvtp, obj_id));
        }
    }
}
