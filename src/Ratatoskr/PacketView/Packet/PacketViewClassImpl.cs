using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.PacketView;

namespace Ratatoskr.PacketView.Packet
{
    internal sealed class PacketViewClassImpl : PacketViewClass
    {
        public static readonly Guid ClassID = new Guid("1DFB59E7-C7A7-458A-8E27-6ECF4BE8B41D");


        public PacketViewClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("Packet"); }
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
