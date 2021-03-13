using System;
using System.Collections.Generic;
using System.Text;
using Ratatoskr.General;

namespace RtsCore.Protocol
{
    public abstract class ProtocolDecoderClass
    {
        public ProtocolDecoderClass(Guid id)
        {
            ID = id;
        }

        public Guid ID { get; }

        public abstract string        Name    { get; }
        public abstract string        Details { get; }
        public virtual  ModuleVersion Version { get; } = new ModuleVersion(0, 1, 0, "");

        public override bool Equals(object obj)
        {
            if (obj is Guid obj_guid) {
                return (ID == obj_guid);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return (Name);
        }

        public ProtocolDecoderInstance CreateInstance()
        {
            return (OnCreateInstance());
        }

        protected virtual ProtocolDecoderInstance OnCreateInstance()
        {
            return (null);
        }
    }
}
