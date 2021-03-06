﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Device.Null
{
    internal class DeviceClassImpl : DeviceClass
    {
        public DeviceClassImpl() : base(Guid.Parse("577E457E-2830-4944-AD3D-C77C41226A9B"))
        {
        }

        public override string Name
        {
            get { return ("Null"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        public override string DescID
        {
            get { return ("Null"); }
        }

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
            return (new DeviceInstanceImpl(devconf, this, devp));
        }

        public override string ToString()
        {
            return (Name);
        }
    }
}
