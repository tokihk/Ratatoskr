﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Devices.Null
{
    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return ("");
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (null);
        }
    }
}