using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.PacketView;

namespace Ratatoskr.PacketView.Protocol
{
    internal sealed class PacketViewClassImpl : PacketViewClass
    {
        public static readonly Guid ClassID = new Guid("C5D2B14E-6CBD-4F06-AD28-8F9BE4E6E9B9");


        public PacketViewClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("Protocol"); }
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
