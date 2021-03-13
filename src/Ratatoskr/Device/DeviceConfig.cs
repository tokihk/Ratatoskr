using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Device
{
    [Serializable]
    internal class DeviceConfig
    {
        public bool SendEnable     { get; set; } = true;
        public bool RecvEnable     { get; set; } = true;
        public bool RedirectEnable { get; set; } = true;

        public uint SendDataQueueLimit     { get; set; } = 1;
        public uint RedirectDataQueueLimit { get; set; } = 1000;


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var obj_c = obj as DeviceConfig;

            if (obj_c != null) {
                return (   (SendEnable == obj_c.SendEnable)
                        && (RecvEnable == obj_c.RecvEnable)
                        && (RedirectEnable == obj_c.RedirectEnable)
                        && (SendDataQueueLimit == obj_c.SendDataQueueLimit)
                        && (RedirectDataQueueLimit == obj_c.RedirectDataQueueLimit));
            }

            return base.Equals(obj);
        }
    }
}
