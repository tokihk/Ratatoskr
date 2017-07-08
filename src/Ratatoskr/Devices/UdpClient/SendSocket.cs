using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.UdpClient
{
    internal sealed class SendSocket
    {
        private DeviceInstanceImpl devi_;

        private Socket socket_;

        private Queue<byte[]> send_buffer_ = new Queue<byte[]>();
        private byte[]        send_data_ = null;
        private int           send_data_offset_ = 0;
        private bool          send_state_ = false;
        private object        send_sync_ = new object();

        private string remote_ep_text_;


        public IPEndPoint RemoteEndPoint { get; }


        public SendSocket(DeviceInstanceImpl devi, IPEndPoint ep_remote, BindModeType mode)
        {
            devi_ = devi;
            RemoteEndPoint = ep_remote;

            /* リモートエンドポイントテキスト作成 */
            remote_ep_text_ = string.Format(
                                "{0:G}:{1:G}",
                                RemoteEndPoint.Address.ToString(),
                                RemoteEndPoint.Port.ToString());

            /* ソケット作成 */
            socket_ = new Socket(ep_remote.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
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

        public void PushSendData(byte[] data)
        {
            lock (send_buffer_) {
                send_buffer_.Enqueue(data);
            }
        }

        private byte[] PopSendData()
        {
            if (send_buffer_.Count == 0)return (null);

            lock (send_buffer_) {
                return (send_buffer_.Dequeue());
            }
        }

        public void Poll(ref bool busy)
        {
            if (IsClosed)return;

            SendPoll(ref busy);
        }

        private void SendPoll(ref bool busy)
        {
            /* データ送信中は無視 */
            if (send_state_) {
                busy = true;
                return;
            }

            lock (send_sync_) {
                /* データを取得 */
                send_data_ = PopSendData();

                /* データが存在しないときは無視 */
                if ((send_data_ == null) || (send_data_.Length == 0))return;

                /* 送信初期化 */
                send_state_ = true;
                send_data_offset_ = 0;

                /* 送信開始 */
                socket_.BeginSendTo(
                    send_data_,
                    send_data_offset_,
                    send_data_.Length,
                    SocketFlags.None,
                    RemoteEndPoint,
                    SendComplete,
                    null);
            }
        }

        private void SendComplete(IAsyncResult ar)
        {
            lock (send_sync_) {
                try {
                    /* 送信が成功したサイズを取得 */
                    var send_size = socket_.EndSendTo(ar);

                    /* 送信完了通知 */
                    if (send_size > 0) {
                        devi_.NotifySend(remote_ep_text_, send_data_.Skip(send_data_offset_).Take(send_size).ToArray());
                    }

                    /* 送信位置を更新 */
                    send_data_offset_ += send_size;

                    /* 送信データが残っているときは再送 */
                    if (send_data_offset_ < send_data_.Length) {
                        socket_.BeginSendTo(
                            send_data_,
                            send_data_offset_,
                            send_data_.Length,
                            SocketFlags.None,
                            RemoteEndPoint,
                            SendComplete,
                            null);

                    } else {
                        send_state_ = false;
                    }

                } catch {
                }
            }
        }
    }
}
