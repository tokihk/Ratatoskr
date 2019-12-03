using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Protocol;
using RtsCore.Utility;

namespace RtsPlugin.Pcap.Protocols.EthenetII
{
    public class ProtocolDecoderClassImpl : ProtocolDecoderClass
    {
        public ProtocolDecoderClassImpl() : base(Guid.Parse("1A6FAD16-66AF-4585-95A0-90BED4A2CEA8"))
        {
        }

        public override string Name
        {
            get { return ("Ethernet II"); }
        }

        public override string Details
        {
            get { return (Name); }
        }

        public override ModuleVersion Version
        {
            get { return (new ModuleVersion(0, 1, 0, "")); }
        }

        protected override ProtocolDecoderInstance OnCreateInstance()
        {
            return (new ProtocolDecoderInstanceImpl(this));
        }
    }
}
