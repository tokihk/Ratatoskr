﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Device
{
    internal abstract class DeviceClass
    {
        public DeviceClass(Guid id)
        {
            ID = id;
        }

        public Guid ID { get; }

        public abstract string Name    { get; }
        public abstract string Details { get; }
        public abstract string DescID  { get; }

        public abstract Type GetPropertyType();
        public abstract DeviceProperty CreateProperty();

        public virtual bool AdminOnly   { get { return (false);} }
        public virtual bool CanUse      { get { return (true); } }
        public virtual bool CanSend     { get { return (true); } }
        public virtual bool CanRecv     { get { return (true); } }
        public virtual bool CanRedirect { get { return (true); } }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Guid) {
                return (((Guid)obj) == ID);
            }

            return (base.Equals(obj));
        }

        internal DeviceInstance CreateInstance(DeviceConfig devconf, DeviceProperty devp)
        {
            if (devp == null)return (null);
            if (devp.GetType() != GetPropertyType())return (null);

            return (OnCreateInstance(devconf, devp));
        }

        protected virtual DeviceInstance OnCreateInstance(DeviceConfig devconf, DeviceProperty devp)
        {
            return (null);
        }
    }
}
