using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews.Protocol
{
    internal sealed class ViewClassImpl : ViewClass
    {
        public static readonly Guid ClassID = new Guid("3D0C0AED-215C-4D91-A979-947A1AD3B271");


        public ViewClassImpl() : base(ClassID)
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
