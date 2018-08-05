using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews.Graph
{
    internal sealed class ViewClassImpl : ViewClass
    {
        public static readonly Guid ClassID = new Guid("9CEC36E8-B675-4CFB-A6B5-1FE79DBA16EB");


        public ViewClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("Graph"); }
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
