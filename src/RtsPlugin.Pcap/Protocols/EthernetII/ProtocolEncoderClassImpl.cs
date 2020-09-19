using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Protocol;
using RtsCore.Utility;

namespace RtsPlugin.Pcap.Protocols.EthernetII
{
    public class ProtocolEncoderClassImpl : ProtocolEncoderClass
    {
        public ProtocolEncoderClassImpl() : base(Guid.Parse("3E5A58D3-E6DE-49B0-A239-D7B068A19C50"))
        {

        }

        public override string Name
        {
            get { return ("EthernetII"); }
        }

        public override string Details
        {
            get { return ("EthernetII"); }
        }
    }
}
