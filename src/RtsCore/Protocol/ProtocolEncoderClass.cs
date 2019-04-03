using System;
using System.Collections.Generic;
using System.Text;
using RtsCore.Utility;

namespace RtsCore.Protocol
{
    public abstract class ProtocolEncoderClass
    {
        public ProtocolEncoderClass(Guid id)
        {
            ID = id;
        }

        public Guid ID { get; }

        public abstract string        Name    { get; }
        public abstract string        Details { get; }
        public virtual  ModuleVersion Version { get; } = new ModuleVersion(0, 1, 0, "");

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
