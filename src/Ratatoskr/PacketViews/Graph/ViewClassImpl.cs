using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketViews.Graph
{
    internal sealed class ViewClassImpl : ViewClass
    {
        public static readonly Guid ClassID = new Guid("547D1DAF-45EF-4A8E-A37A-F16B8A708F70");


        public ViewClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("グラフ"); }
        }

        public override string Details
        {
            get { return ("データをグラフ化します"); }
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
