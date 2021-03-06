﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;
using Ratatoskr.Config;

namespace Ratatoskr.Device
{
    [Serializable]
    internal abstract class DeviceProperty : ConfigHolder
    {
        public abstract DeviceProperty Clone();

        public abstract DevicePropertyEditor GetPropertyEditor();

        public virtual string GetStatusString()
        {
            return ("");
        }
    }
}
