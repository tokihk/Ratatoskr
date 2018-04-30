using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.TransferProtocols
{
    internal class TransferProtocolInstanceImpl : TransferProtocolInstance
    {
        public TransferProtocolInstanceImpl()
        {
        }

        protected override void OnSend(byte[] data)
        {
        }

        protected override void OnRecv(byte[] data)
        {
        }
    }
}
