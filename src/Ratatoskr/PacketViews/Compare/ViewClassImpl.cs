using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews.Compare
{
    internal sealed class ViewClassImpl : ViewClass
    {
        public static readonly Guid ClassID = new Guid("1A4755F2-B85C-4BF7-B8EA-37D1BEAA2C76");


        public ViewClassImpl() : base(ClassID)
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
            return (typeof(ViewPropertyImpl));
        }

        public override ViewProperty CreateProperty()
        {
            return (new ViewPropertyImpl());
        }

        protected override ViewInstance OnCreateInstance(ViewManager viewm, Guid obj_id, ViewProperty viewp)
        {
            return (new ViewInstanceImpl(viewm, this, viewp, obj_id));
        }
    }
}
