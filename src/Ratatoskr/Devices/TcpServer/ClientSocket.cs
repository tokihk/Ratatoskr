using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Packet;

namespace Ratatoskr.Devices.TcpServer
{
    internal class ClientSocket
    {
        private byte[] DAMMY_DATA = new byte[] { 0x00 };


        private DeviceInstanceImpl devi_;

        private Socket socket_;

        private string local_text_;
        private string remote_text_;

        private Queue<byte[]> send_buffer_queue_ = new Queue<byte[]>();
        private byte[] send_buffer_;
        private byte[] recv_buffer_;

        private bool send_state_ = false;
        private bool recv_state_ = false;

        private object send_sync_ = new object();
        private object recv_sync_ = new object();

        private volatile bool dispose_state_ = false;


        public ClientSocket(DeviceInstanceImpl devi, Socket socket)
        {
            devi_ = devi;
            socket_ = socket;

            local_text_ = string.Format(
                                "{0:G}:{1:G}",
                                ((IPEndPoint)socket_.LocalEndPoint).Address.ToString(),
                                ((IPEndPoint)socket_.LocalEndPoint).Port.ToString());

            remote_text_ = string.Format(
                                "{0:G}:{1:G}",
                                ((IPEndPoint)socket_.RemoteEndPoint).Address.ToString(),
                                ((IPEndPoint)socket_.RemoteEndPoint).Port.ToString());

            recv_buffer_ = new byte[2048];

            devi_.NotifyMessage(
                PacketPriority.Standard,
                "Device Event",
                String.Format("Connect <= [{0:G}]", remote_text_));
        }

        public void Dispose()
        {
            if (dispose_state_)return;

            /* === 切断処理 === */
            socket_.Disconnect(false);

            /* --- リソース解放待ち --- */
            var sw = new Stopwatch();

            while (   (sw.ElapsedMilliseconds < 5000)
                    && (   (send_state_)
                        || (recv_state_))
            ) {
                System.Threading.Thread.Sleep(1);
            }

            socket_.Close();

            devi_.NotifyMessage(
                PacketPriority.Standard,
                "Device Event",
                String.Format("Disconnect => [{0:G}]", remote_text_));

            dispose_state_ = true;
        }

        public bool IsDisposed
        {
            get { return (dispose_state_); }
        }

        public void Poll(ref bool busy)
        {
            if (dispose_state_)return;

            SendPoll(ref busy);
            RecvPoll(ref busy);
            ConnectPoll(ref busy);
        }

        private void SendPoll(ref bool busy)
        {
            /* --- 送信中 --- */
            if (send_state_) {
                busy = true;
                return;
            }

            lock (send_sync_) {
                /* --- 送信データリロード --- */
                if ((send_buffer_ == null) || (send_buffer_.Length == 0)) {
                    send_buffer_ = SendDataPop();
                }

                /* --- 送信データ無し --- */
                if ((send_buffer_ == null) || (send_buffer_.Length == 0))return;

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
                    send_state_ = false;
                }
            }
        }

        private void CallbackSend(IAsyncResult iar)
        {
            lock (send_sync_) {
                try {
                    var socket = iar.AsyncState as Socket;
                    var send_size = socket.EndSend(iar);

                    if (send_size > 0) {
                        var send_data = send_buffer_;
                        var next_data = (byte[])null;

                        if (send_size < send_buffer_.Length) {
                            send_data = ClassUtil.CloneCopy(send_buffer_, send_size);
                            next_data = ClassUtil.CloneCopy(send_buffer_, send_size, int.MaxValue);
                        }

                        devi_.NotifySendComplete("", local_text_, remote_text_, send_data);
                        send_buffer_ = next_data;
                    }
                } catch (Exception) {
                }

                send_state_ = false;
            }
        }

        private void RecvPoll(ref bool busy)
        {
            /* --- 受信中 --- */
            if (recv_state_)return;

            lock (recv_sync_) {
                /* 受信開始 */
                try {
                    socket_.BeginReceive(
                        recv_buffer_,
                        0,
                        recv_buffer_.Length,
                        SocketFlags.None,
                        CallbackRecv,
                        socket_);
                } catch (Exception) {
                }
            }
        }

        private void CallbackRecv(IAsyncResult iar)
        {
            lock (recv_sync_) {
                try {
                    var socket = iar.AsyncState as Socket;
                    var recv_size = socket.EndReceive(iar);

                    /* --- 受信完了 --- */
                    if (recv_size > 0) {
                        devi_.NotifyRecvComplete("", remote_text_, local_text_, ClassUtil.CloneCopy(recv_buffer_, recv_size));
                    }

                } catch (Exception) {
                }

                recv_sync_ = false;
            }
        }

        private void ConnectPoll(ref bool busy)
        {
            /* === クライアントからの切断を確認 === */
            if (   (socket_.Poll(0, SelectMode.SelectRead))
                && (socket_.Available == 0)
            ) {
                Dispose();
            }
        }

        public void SendDataPush(byte[] data)
        {
            if (dispose_state_)return;

            var busy = false;

            /* バッファへ追加 */
            lock (send_buffer_queue_) {
                send_buffer_queue_.Enqueue(data);
            }

            /* 送信動作を即座に実行 */
            SendPoll(ref busy);
        }

        private byte[] SendDataPop()
        {
            if (send_buffer_queue_.Count == 0)return (null);

            lock (send_buffer_queue_) {
                return (send_buffer_queue_.Dequeue());
            }
        }
    }
}
