using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketView.Graph.DisplayModules
{
    internal class Display_Spectrum : DisplayModule
    {
        public Display_Spectrum(PacketViewPropertyImpl prop) : base(prop)
        {
        }

        public override uint PointCount
        {
            get { return (0); }
        }

        protected override void OnClearValue()
        {
        }

        protected override void OnInputValue(decimal[] value)
        {
        }
    }
}
