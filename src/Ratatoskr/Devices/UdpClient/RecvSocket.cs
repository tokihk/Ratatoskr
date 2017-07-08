using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.UdpClient
{
    internal sealed class RecvSocket
    {
        private DeviceInstanceImpl devi_;    

        private Socket socket_ = null;

        private byte[]    recv_buffer_ = new byte[2048];
        private EndPoint  recv_ep_;
        private bool      recv_state_ = false;
        private object    recv_sync_ = false;


        public IPEndPoint LocalEndPoint  { get; }


        public RecvSocket(DeviceInstanceImpl devi, IPEndPoint ep_local, BindModeType mode)
        {
            devi_ = devi;
            LocalEndPoint = ep_local;

            /* 受信用エンドポイントバッファ作成 */
            recv_ep_ = new IPEndPoint(
                                (ep_local.AddressFamily == AddressFamily.InterNetwork) ? (IPAddress.Any) : (IPAddress.IPv6Any),
                                ep_local.Port);

            /* ソケット作成 */
            socket_ = new Socket(ep_local.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            /* バインド設定 */
            switch (mode) {
                case BindModeType.Bind:
                {
                    socket_.Bind(ep_local);
                }
                    break;

                case BindModeType.Multicast:
                {
                    /* アドレス再利用許可 */
                    socket_.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);

                    /* マルチキャストアドレスにバインド */
                    socket_.Bind(
                        new IPEndPoint(
                            (ep_local.AddressFamily == AddressFamily.InterNetwork) ? (IPAddress.Any) : (IPAddress.IPv6Any),
                            ep_local.Port));
                    
                    /* マルチキャストグループに参加 */
                    if (ep_local.AddressFamily == AddressFamily.InterNetwork) {
                        socket_.SetSocketOption(
                            SocketOptionLevel.IP,
                            SocketOptionName.AddMembership,
                            new MulticastOption(ep_local.Address, IPAddress.Any));
                    } else {
                        socket_.SetSocketOption(
                            SocketOptionLevel.IPv6,
                            SocketOptionName.AddMembership,
                            new MulticastOption(ep_local.Address, IPAddress.IPv6Any));
                    }

                }
                    break;
            }
        }

        public bool IsClosed
        {
            get { return (socket_ == null); }
        }

        public void Close()
        {
            if (socket_ == null)return;

            socket_.Shutdown(SocketShutdown.Both);
            socket_.Close();
            socket_ = null;
        }

        public void Poll(ref bool busy)
        {
            if (IsClosed)return;

            RecvPoll(ref busy);
        }

        private void RecvPoll(ref bool busy)
        {
            /* 受信待ちのときは無視 */
            if (recv_state_) {
                return;
            }

            lock (recv_sync_) {
                /* 受信初期化 */
                recv_state_ = true;

                /* 受信開始 */
                socket_.BeginReceiveFrom(
                    recv_buffer_,
                    0,
                    recv_buffer_.Length,
                    SocketFlags.None,
                    ref recv_ep_,
                    RecvComplete,
                    null);
            }
        }

        private void RecvComplete(IAsyncResult iar)
        {
            lock (recv_sync_) {
                try {
                    /* 受信したサイズを取得 */
                    var recv_size = socket_.EndReceiveFrom(iar, ref recv_ep_);

                    if (recv_size > 0) {
                        devi_.NotifyRecv(
                            string.Format(
                                "{0:G}:{1:G}",
                                ((IPEndPoint)recv_ep_).Address.ToString(),
                                ((IPEndPoint)recv_ep_).Port.ToString()),
                            recv_buffer_.Take(recv_size).ToArray());
                    }

                } catch (Exception) {
                }

                recv_state_ = false;
            }
        }
    }
}
