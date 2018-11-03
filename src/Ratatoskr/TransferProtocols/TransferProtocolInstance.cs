using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace Ratatoskr.TransferProtocols
{
    internal abstract class TransferProtocolInstance
    {
        public delegate void SendToDeviceDelegate(byte[] send_data, byte[] view_data);
        public delegate void RecvPayloadDataDelegate(byte[] payload_data);

        public event SendToDeviceDelegate    SendDevice  = delegate(byte[] send_data, byte[] view_data) { };
        public event RecvPayloadDataDelegate RecvPayload = delegate(byte[] payload_data) { };


        public TransferProtocolInstance()
        {
        }

        public void Send(byte[] data)
        {

        }

        public void Recv(byte[] data)
        {

        }

        public void Poll()
        {
            OnPoll();
        }

        protected virtual void OnSend(byte[] data) { }
        protected virtual void OnRecv(byte[] data) { }
        protected virtual void OnPoll() { }
    }
}
