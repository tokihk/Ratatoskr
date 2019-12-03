using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Framework.Device;

namespace Ratatoskr.Devices.TcpServer
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private ServerSocket[] servers_ = null;


        public DeviceInstanceImpl(DeviceManagementClass devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devm, devconf, devd, devp)
        {
        }

        protected override void OnConnectStart()
        {
            try {
                var prop = Property as DevicePropertyImpl;
                var hostname = Dns.GetHostName();
                var addrlist = new List<IPAddress>(Dns.GetHostAddresses(hostname));

                /* --- ループバックアドレスを追加 --- */
                addrlist.Add(IPAddress.Parse("127.0.0.1"));
                addrlist.Add(IPAddress.Parse("::1"));

                /* --- サーバーソケット一覧作成 --- */
                servers_ = addrlist.Select(
                                addr => new ServerSocket(
                                    this,
                                    new IPEndPoint(addr, (int)prop.LocalPortNo.Value),
                                    (int)prop.Capacity.Value)
                                ).ToArray();

            } catch (Exception) {
            }
        }

        protected override EventResult OnConnectBusy()
        {
            return (  ((servers_ != null) && (servers_.Length > 0))
                    ? (EventResult.Success)
                    : (EventResult.Busy));
        }

        protected override void OnConnected()
        {
        }

        protected override void OnDisconnectStart()
        {
            /* --- 全サーバーをシャットダウン --- */
            if (servers_ != null) {
                foreach (var server in servers_) {
                    server.Dispose();
                }
                servers_ = null;
            }
        }

        protected override PollState OnPoll()
        {
            var busy = false;

            /* 送信処理 */
            SendPoll(ref busy);

            /* サーバー処理 */
            foreach (var server in servers_) {
                server.Poll(ref busy);
            }

            return ((busy) ? (PollState.Active) : (PollState.Idle));
        }

        private void SendPoll(ref bool busy)
        {
            var send_data = GetSendData();

            if ((send_data == null) || (send_data.Length == 0))return;

            foreach (var server in servers_) {
                server.Send(send_data);
            }

            busy = true;
        }
    }
}
