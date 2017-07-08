using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketConverters.Convert
{
    internal sealed class PacketConverterClassImpl : PacketConverterClass
    {
        public static readonly Guid ClassID = new Guid("01E1BDEC-1369-4317-8973-B9765B2EB5C0");


        public PacketConverterClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("データ変換"); }
        }

        public override string Details
        {
            get { return (
                "パケット内のデータを変換します"
            ); }
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
