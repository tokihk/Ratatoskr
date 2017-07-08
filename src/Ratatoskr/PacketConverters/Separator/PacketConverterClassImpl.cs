using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketConverters.Separator
{
    internal sealed class PacketConverterClassImpl : PacketConverterClass
    {
        public static readonly Guid ClassID = new Guid("EF01E50E-631B-4230-AD2D-3D8A674F3033");


        public PacketConverterClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("パケット区切り"); }
        }

        public override string Details
        {
            get { return ("パケットの切れ目を定義できます。"); }
        }

        public override Type GetPropertyType()
        {
            return (typeof(PacketConverterPropertyImpl));
        }

        public override PacketConverterProperty CreateProperty()
        {
            return (new PacketConverterPropertyImpl());
        }

        protected override PacketConverterInstance OnCreateInstance(PacketConverterManager pcvtm, Guid obj_id, PacketConverterProperty pcvtp)
        {
            return (new PacketConverterInstanceImpl(pcvtm, this, pcvtp, obj_id));
        }
    }
}
