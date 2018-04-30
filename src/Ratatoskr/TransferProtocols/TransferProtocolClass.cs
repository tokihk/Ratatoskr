using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.TransferProtocols
{
    internal abstract class TransferProtocolClass
    {
        public TransferProtocolClass(Guid id)
        {
            ID = id;
        }

        public Guid ID { get; }

        public abstract string Name { get; }
        public abstract string Details { get; }


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

        internal TransferProtocolInstance CreateInstance()
        {
            return (OnCreateInstance());
        }

        protected virtual TransferProtocolInstance OnCreateInstance()
        {
            return (null);
        }
    }
}
