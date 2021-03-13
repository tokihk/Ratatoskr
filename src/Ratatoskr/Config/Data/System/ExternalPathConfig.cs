using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Config.Data.System
{
    [Serializable]
    public sealed class ExternalPathConfig : ConfigHolder
    {
		public StringConfig WiresharkBin { get; } = new StringConfig("C:\\Program Files\\Wireshark\\Wireshark.exe");

        public ExternalPathConfig()
        {
        }
    }
}
