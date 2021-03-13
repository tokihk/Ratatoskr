using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.PacketView;

namespace Ratatoskr.PacketView.Wireshark
{
    internal sealed class PacketViewClassImpl : PacketViewClass
    {
        public static readonly Guid ClassID = new Guid("45EB98CB-FB21-4A10-942D-EB6B0A20F05A");


        public PacketViewClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("Wireshark"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        public override Type GetPropertyType()
        {
            return (typeof(PacketViewPropertyImpl));
        }

        public override PacketViewProperty CreateProperty()
        {
            return (new PacketViewPropertyImpl());
        }

        protected override PacketViewInstance OnCreateInstance(PacketViewManager viewm, Guid obj_id, PacketViewProperty viewp)
        {
            return (new PacketViewInstanceImpl(viewm, this, viewp, obj_id));
        }
    }
}
