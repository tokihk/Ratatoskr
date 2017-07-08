using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Native;
using Ratatoskr.Generic;

namespace Ratatoskr.Devices.UdpClient
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private DevicePropertyImpl prop_;

        private List<SendSocket> sockets_send_ = new List<SendSocket>();
        private List<RecvSocket> sockets_recv_ = new List<RecvSocket>();

        private Task<IPAddress[]> local_addr_task_ = null;
        private Task<IPAddress[]> remote_addr_task_ = null;

        private byte[] send_buffer_ = new byte[2048];


        public DeviceInstanceImpl(DeviceManager devm, DeviceClass devd, DeviceProperty devp, Guid id, string name) : base(devm, devd, devp, id, name)
        {
            prop_ = devp as DevicePropertyImpl;
        }

        protected override EventResult OnConnectStart()
        {
            return (EventResult.Success);
        }

        protected override EventResult OnConnectBusy()
        {
            return (EventResult.Success);
        }

        protected override void OnConnected()
        {
        }

        protected override void OnDisconnectStart()
        {
            sockets_send_.ForEach(socket => socket.Close());
            sockets_send_.Clear();

            sockets_recv_.ForEach(socket => socket.Close());
            sockets_recv_.Clear();
        }

        protected override PollState OnPoll()
        {
            var busy = false;

            LocalAddressCollectPoll();

            RemoteAddressCollectPoll();

            SocketPoll(ref busy);

            return ((busy) ? (PollState.Busy) : (PollState.Idle));
        }

        private void LocalAddressCollectPoll()
        {
            if (prop_.BindMode.Value != BindModeType.None) {
                /* アドレス取得タスク完了 */
                if (   (local_addr_task_ != null)
                    && (local_addr_task_.IsCompleted)
                ) {
                    var ip_addrs = new List<IPAddress>();

                    /* ループバックアドレスを追加 */
                    ip_addrs.Add(IPAddress.Parse("127.0.0.1"));
                    ip_addrs.Add(IPAddress.Parse("::1"));

                    /* DNSからのアドレスを追加 */
                    ip_addrs.AddRange(local_addr_task_.Result);

                    /* 受信用ソケットリストを更新 */
                    UpdateRecvSocketList(ip_addrs);

                    local_addr_task_ = null;
                }

                /* アドレス取得タスク開始 */
                if (local_addr_task_ == null) {
                    /* DNSからアドレスを取得 */
                    local_addr_task_ = Dns.GetHostAddressesAsync(Dns.GetHostName());
                }
            }
        }

        private void RemoteAddressCollectPoll()
        {
            /* アドレス取得タスク完了 */
            if (   (remote_addr_task_ != null)
                && (remote_addr_task_.IsCompleted)
            ) {
                /* 送信用ソケットリストを更新 */
                UpdateSendSocketList(remote_addr_task_.Result);

                remote_addr_task_ = null;
            }

            /* アドレス取得タスク開始 */
            if (remote_addr_task_ == null) {
                /* DNSからアドレスを取得 */
                remote_addr_task_ = Dns.GetHostAddressesAsync(prop_.RemoteAddress.Value);
            }
        }

        private void UpdateSendSocketList(IEnumerable<IPAddress> addresses)
        {
            lock (sockets_send_) {
                /* アドレスリストに存在しないソケットを破棄 */
                foreach (var socket in sockets_send_) {
                    if (!addresses.Contains(socket.RemoteEndPoint.Address)) {
                        socket.Close();
                    }
                }

                /* 破棄済みのソケットをリストから削除 */
                sockets_send_.RemoveAll(socket => socket.IsClosed);

                /* アドレスリストからソケットを作成 */
                foreach (var address in addresses) {
                    if (sockets_send_.Find(socket => socket.RemoteEndPoint.Address.Equals(address)) == null) {
                        var socket_new = CreateSendSocket(address);

                        if (socket_new != null) {
                            sockets_send_.Add(socket_new);
                        }
                    }
                }
            }
        }

        private void UpdateRecvSocketList(IEnumerable<IPAddress> addresses)
        {
            lock (sockets_recv_) {
                /* アドレスリストに存在しないソケットを破棄 */
                foreach (var socket in sockets_recv_) {
                    if (!addresses.Contains(socket.LocalEndPoint.Address)) {
                        socket.Close();
                    }
                }

                /* 破棄済みのソケットをリストから削除 */
                sockets_recv_.RemoveAll(socket => socket.IsClosed);

                /* アドレスリストからソケットを作成 */
                foreach (var address in addresses) {
                    if (sockets_recv_.Find(socket => socket.LocalEndPoint.Address.Equals(address)) == null) {
                        var socket_new = CreateRecvSocket(address);

                        if (socket_new != null) {
                            sockets_recv_.Add(socket_new);
                        }
                    }
                }
            }
        }

        private void SocketPoll(ref bool busy)
        {
            SocketSendPoll(ref busy);
            SocketRecvPoll(ref busy);
        }

        private void SocketSendPoll(ref bool busy)
        {
            lock (sockets_send_) {
                var send_size = 0;

                /* 送信データ取得 */
                send_size = GetSendData(send_buffer_, send_buffer_.Length);
 
                if (send_size > 0) {
                    /* 送信データ生成 */
                    var send_data = ClassUtil.CloneCopy(send_buffer_, send_size);

                    /* 送信データをセットアップ */
                    sockets_send_.ForEach(socket => socket.PushSendData(send_data));
                }

                /* 送信シーケンス実行 */
                foreach (var socket in sockets_send_) {
                    socket.Poll(ref busy);
                }
            }
        }

        private void SocketRecvPoll(ref bool busy)
        {
            lock (sockets_recv_) {
                foreach (var socket in sockets_recv_) {
                    socket.Poll(ref busy);
                }
            }
        }

        private SendSocket CreateSendSocket(IPAddress remote_addr)
        {
            try {
                return (
                    new SendSocket(
                        this,
                        new IPEndPoint(
                            remote_addr,
                            (int)prop_.RemotePortNo.Value),
                        prop_.BindMode.Value));

            } catch {
                return (null);
            }
        }

        private RecvSocket CreateRecvSocket(IPAddress local_addr)
        {
            try {
                return (
                    new RecvSocket(
                        this,
                        new IPEndPoint(
                            local_addr,
                            (int)prop_.LocalPortNo.Value),
                        prop_.BindMode.Value));
            } catch {
                return (null);
            }
        }

        public void NotifySend(string remote_ep_text, byte[] data)
        {
            NotifySendComplete("", "", remote_ep_text, data);
        }

        public void NotifyRecv(string remote_ep_text, byte[] data)
        {
            NotifyRecvComplete("", remote_ep_text, "", data);
        }
    }
}
