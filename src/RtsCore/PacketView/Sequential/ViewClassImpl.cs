using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Framework.PacketView;

namespace Ratatoskr.PacketViews.Sequential
{
    internal sealed class PacketViewClassImpl : PacketViewClass
    {
        public static readonly Guid ClassID = new Guid("931D5052-BFC2-4560-97F3-A51B0448E172");


        public PacketViewClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("Sequential"); }
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
