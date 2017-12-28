using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Devices;

namespace Ratatoskr.Gate
{
    [Serializable]
    internal class GateProperty
    {
        public GateProperty(string alias, Color color)
        {
            Alias = alias;
            Color = color;
        }

        public string               Alias              { get; set; } = "";
        public Color                Color              { get; set; } = Color.White;
        public bool                 ConnectRequest     { get; set; } = true;
        public string               RedirectAlias      { get; set; } = "";
        public DeviceDataRateTarget DataRateTarget     { get; set; } = DeviceDataRateTarget.RecvData;
        public ulong                DataRateGraphLimit { get; set; } = 0;
        public string               ConnectCommand     { get; set; } = null;
    }
}
