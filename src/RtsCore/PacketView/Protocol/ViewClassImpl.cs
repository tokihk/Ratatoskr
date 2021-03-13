using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Framework.PacketView;

namespace Ratatoskr.PacketViews.Protocol
{
    internal sealed class PacketViewClassImpl : PacketViewClass
    {
        public static readonly Guid ClassID = new Guid("3D0C0AED-215C-4D91-A979-947A1AD3B271");


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
