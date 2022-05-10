using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketView.Graph.DataFormatModules
{
    internal class DataFormat_SignedByte : DataFormatModule
    {
		public DataFormat_SignedByte(PacketViewPropertyImpl prop) : base(prop)
		{
		}

        protected override void OnInputData(byte data)
        {
            ExtractValue((sbyte)data);
        }
    }
}
