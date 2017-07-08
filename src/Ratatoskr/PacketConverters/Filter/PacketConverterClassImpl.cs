using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketConverters.Filter
{
    internal sealed class PacketConverterClassImpl : PacketConverterClass
    {
        public static readonly Guid ClassID = new Guid("EFAEEC7A-DFBD-4BF2-B3D8-DF2B58A234AC");


        public PacketConverterClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("パケットフィルタ"); }
        }

        public override string Details
        {
            get { return ("任意のパケットのみを通過させます。"); }
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
