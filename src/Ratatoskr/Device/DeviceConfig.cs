using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;

namespace Ratatoskr.Device
{
    [Serializable]
    internal class DeviceConfig
    {
		public bool DataSendCompletedNotify { get; set; } = true;
		public bool DataRecvCompletedNotify { get; set; } = true;
		public bool DeviceConnectNotify   { get; set; } = true;

        public bool DataSendEnable     { get; set; } = true;
        public uint DataSendQueueLimit { get; set; } = 1;

        public bool DataRedirectEnable     { get; set; } = true;
        public uint DataRedirectQueueLimit { get; set; } = 1000;


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is DeviceConfig obj_c) {
				return (ClassUtil.Compare(this, obj_c));
            }

            return (base.Equals(obj));
        }
    }
}
