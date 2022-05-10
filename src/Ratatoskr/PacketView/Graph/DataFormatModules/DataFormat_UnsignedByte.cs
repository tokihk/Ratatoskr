using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketView.Graph.DataFormatModules
{
    internal class DataFormat_UnsignedByte : DataFormatModule
    {
		public DataFormat_UnsignedByte(PacketViewPropertyImpl prop) : base(prop)
		{
		}

        protected override void OnInputData(byte data)
        {
            ExtractValue((byte)data);
        }
    }
}
