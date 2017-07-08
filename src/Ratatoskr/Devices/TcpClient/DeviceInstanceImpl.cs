using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Devices.TcpClient
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private Socket socket_ = null;

        private IPEndPoint[] ipep_list_ = null;
        private int          ipep_index_ = 0;
        private IPEndPoint   ipep_ = null;

        private IAsyncResult iar_connect_ = null;

        private string local_text_;
        private string remote_text_;

        private byte[] send_buffer_;
        private byte[] recv_buffer_;

        private bool send_state_ = false;
        private bool recv_state_ = false;

        private object send_sync_ = new object();
        private object recv_sync_ = new object();


        public DeviceInstanceImpl(DeviceManager devm, DeviceClass devd, DeviceProperty devp, Guid id, string name) : base(devm, devd, devp, id, name)
        {
        }

        protected override EventResult OnConnectStart()
        {
            ipep_list_ = null;
            ipep_index_ = 0;
            ipep_ = null;

            try {
                var prop = Property as DevicePropertyImpl;
                var addrlist = Dns.GetHostAddresses(prop.RemoteAddress.Value);

                /* --- 接続先EndPoint一覧作成 --- */
                var ipep_list = new List<IPEndPoint>();

                foreach (var addr in addrlist) {
                    ipep_list.Add(new IPEndPoint(addr, (int)prop.RemotePortNo.Value));
                }
                ipep_list_ = ipep_list.ToArray();

            } catch (Exception) {
                ipep_ = null;
            }

            return (EventResult.Success);
        }

        protected override EventResult OnConnectBusy()
        {
            var connect = false;

            /* --- 接続開始 --- */
            if (iar_connect_ == null) {
                ipep_ = LoadEndPoint();
                
                if (ipep_ != null) {
                    /* --- ソケット作成 --- */
                    socket_ = new Socket(ipep_.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    /* --- Keep Alive --- */
                    var keepalive = new List<byte>();

                    keepalive.AddRange(BitConverter.GetBytes(1u));    // u_long onoff
                    keepalive.AddRange(BitConverter.GetBytes(3000u)); // u_long keepalivetime
                    keepalive.AddRange(BitConverter.GetBytes(1000u)); // u_long keepaliveinterval

                    socket_.IOControl(IOControlCode.KeepAliveValues, keepalive.ToArray(), null);

                    iar_connect_ = socket_.BeginConnect(ipep_, null, null);
                }
            }

            /* --- 接続状態チェック --- */
            if ((iar_connect_ != null) && (iar_connect_.IsCompleted)) {
                if (socket_.Connected) {
                    connect = true;
                } else {
                    socket_.Close();
                }
                iar_connect_ = null;
            }

            return (  (connect)
                    ? (EventResult.Success)
                    : (EventResult.Busy));
        }

        protected override void OnConnected()
        {
            local_text_ = string.Format(
                            "{0:G}:{1:G}",
                            ((IPEndPoint)socket_.LocalEndPoint).Address.ToString(),
                            ((IPEndPoint)socket_.LocalEndPoint).Port.ToString());
            remote_text_ = string.Format(
                            "{0:G}:{1:G}",
                            ((IPEndPoint)socket_.RemoteEndPoint).Address.ToString(),
                            ((IPEndPoint)socket_.RemoteEndPoint).Port.ToString());

            recv_buffer_ = new byte[2048];

            NotifyMessage(
                PacketPriority.Standard,
                String.Format("Connect => [{0:G}]", remote_text_),
                "");

            send_state_ = false;
            recv_state_ = false;
        }

        protected override void OnDisconnectStart()
        {
            try {
                /* === 切断処理 === */
                if (socket_.Connected) {
                    socket_.Disconnect(false);
                }

                /* --- リソース解放待ち --- */
                var sw = new Stopwatch();

                sw.Restart();
                while (   (sw.ElapsedMilliseconds < 5000)
                        && (   (send_state_)
                            || (recv_state_))
                ) {
                    System.Threading.Thread.Sleep(1);
                }

                socket_.Close();
            } catch (Exception) {
            }
        }

        protected override void OnDisconnected()
        {
            NotifyMessage(
                PacketPriority.Standard,
                String.Format("Disconnect <= [{0:G}]", remote_text_),
                "");
        }

        protected override PollState OnPoll()
        {
            var busy = false;

            SendPoll(ref busy);
            RecvPoll(ref busy);
            ReconnectPoll(ref busy);

            return ((busy) ? (PollState.Busy) : (PollState.Idle));
        }

        private IPEndPoint LoadEndPoint()
        {
            if (ipep_list_ == null)return (null);
            if (ipep_list_.Length == 0)return (null);

            if (ipep_index_ >= ipep_list_.Length) {
                ipep_index_ = 0;
            }

            return (ipep_list_[ipep_index_++]);
        }

        private void SendPoll(ref bool busy)
        {
            /* 送信実行中 */
            if (send_state_) {
                busy = true;
                return;
            }

            lock (send_sync_) {
                /* --- 送信データリロード --- */
                if ((send_buffer_ == null) || (send_buffer_.Length == 0)) {
                    send_buffer_ = GetSendData();
                }

                /* 送信データが存在しない */
                if ((send_buffer_ == null) || (send_buffer_.Length == 0)) {
                    return;
                }

                /* --- 送信開始 --- */
                try {
                    send_state_ = true;

                    socket_.BeginSend(
                            send_buffer_,
                            0,
                            send_buffer_.Length,
                            SocketFlags.None,
                            CallbackSend,
                            socket_);

                    busy = true;

                } catch (Exception) {
                    ConnectReboot();
                }
            }
        }

        private void CallbackSend(IAsyncResult iar)
        {
            lock (send_sync_) {
                try {
                    var socket = iar.AsyncState as Socket;
                    var send_size = socket_.EndSend(iar);

                    /* --- 送信完了 --- */
                    if (send_size > 0) {
                        var send_data = send_buffer_;
                        var next_data = (byte[])null;

                        if (send_size < send_buffer_.Length) {
                            send_data = ClassUtil.CloneCopy(send_buffer_, send_size);
                            next_data = ClassUtil.CloneCopy(send_buffer_, send_size, int.MaxValue);
                        }

                        NotifySendComplete("", local_text_, remote_text_, send_data);
                        send_buffer_ = next_data;
                    }
                } catch (Exception) {
                    ConnectReboot();
                }

                send_state_ = false;
            }
        }

        private void RecvPoll(ref bool busy)
        {
            if (recv_state_)return;

            lock (recv_sync_) {
                /* --- 受信開始 --- */
                try {
                    recv_state_ = true;

                    socket_.BeginReceive(
                        recv_buffer_,
                        0,
                        recv_buffer_.Length,
                        SocketFlags.None,
                        CallbackRecv,
                        socket_);
                } catch (Exception) {
                    ConnectReboot();
                }
            }
        }

        private void CallbackRecv(IAsyncResult iar)
        {
            lock (recv_sync_) {
                try {
                    var socket = iar.AsyncState as Socket;
                    var recv_size = socket_.EndReceive(iar);

                    /* --- 受信完了 --- */
                    if (recv_size > 0) {
                        NotifyRecvComplete("", remote_text_, local_text_, ClassUtil.CloneCopy(recv_buffer_, recv_size));
                    }
                } catch (Exception) {
                    ConnectReboot();
                }

                recv_state_ = false;
            }
        }

        private void ReconnectPoll(ref bool busy)
        {
            /* --- サーバーからの切断をチェック --- */
            if (   (socket_.Poll(0, SelectMode.SelectRead))
                && (socket_.Available == 0)
            ) {
                ConnectReboot();
            }
        }
    }
}
