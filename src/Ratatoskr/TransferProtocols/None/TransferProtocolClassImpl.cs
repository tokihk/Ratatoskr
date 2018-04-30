using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.TransferProtocols
{
    internal class TransferProtocolClassImpl : TransferProtocolClass
    {
        public TransferProtocolClassImpl(Guid id) : base(Guid.Parse("D004FD93-5547-4A6E-9B70-9B3FDF503626"))
        {
        }

        public override string Name
        {
            get { return ("None"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        protected override TransferProtocolInstance OnCreateInstance()
        {
            return (new TransferProtocolInstanceImpl());
        }

        public override string ToString()
        {
            return (Name);
        }
    }
}
