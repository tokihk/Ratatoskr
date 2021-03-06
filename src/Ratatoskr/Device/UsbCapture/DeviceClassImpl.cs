﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Device;

namespace Ratatoskr.Device.UsbCapture
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("2CDB3FBD-46B6-4CBA-91F9-D51ECBBCB748"))
        {
        }

        public override string Name
        {
            get { return ("USB Capture"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        public override string DescID
        {
            get { return ("UsbCapture"); }
        }

        public override bool AdminOnly => true;

        public override Type GetPropertyType()
        {
            return (typeof(DevicePropertyImpl));
        }

        public override DeviceProperty CreateProperty()
        {
            return (new DevicePropertyImpl());
        }

        protected override DeviceInstance OnCreateInstance(DeviceConfig devconf, DeviceProperty devp)
        {
            return (new DeviceInstanceImplByDirect(devconf, this, devp));
        }

        public override string ToString()
        {
            return (Name);
        }
    }
}
