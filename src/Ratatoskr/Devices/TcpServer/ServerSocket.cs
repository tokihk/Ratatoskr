using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Devices.TcpServer
{
    internal sealed class ServerSocket
    {
        private DeviceInstanceImpl devi_;

        private IPEndPoint ipep_;
        private Socket socket_;

        private List<ClientSocket> clients_ = new List<ClientSocket>();

        private bool accept_state_ = false;


        public ServerSocket(DeviceInstanceImpl devi, IPEndPoint ipep, int limit)
        {
            devi_ = devi;
            ipep_ = ipep;

            socket_ = new Socket(ipep_.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            /* --- バインド --- */
            socket_.Bind(ipep_);

            /* --- 接続待ち受け開始 --- */
            socket_.Listen(limit);
        }

        public void Dispose()
        {
            try {
                /* === クライアント切断 === */
                clients_.ForEach(client => client.Dispose());
                clients_.Clear();

                /* === サーバーソケットを破棄 === */
                if (socket_ != null) {
                    socket_.Close();
                    socket_ = null;
                }
            } catch (Exception) {
            }
        }

        public void Poll(ref bool busy)
        {
            /* --- 接続待ち --- */
            if (!accept_state_) {
                accept_state_ = true;
                socket_.BeginAccept(CallbackAccept, socket_);
            }

            lock (clients_) {
                /* --- 処理 --- */
                foreach (var client in clients_) {
                    client.Poll(ref busy);
                }

                /* --- 破棄 --- */
                clients_.RemoveAll(client => client.IsDisposed);
            }
        }

        private void CallbackAccept(IAsyncResult iar)
        {
            try {
                var socket = iar.AsyncState as Socket;
                var client = socket.EndAccept(iar);

                if (client == null)return;

                /* --- 接続 --- */
                lock (clients_)
                {
                    clients_.Add(new ClientSocket(devi_, client));
                }
            } catch (Exception) {
            }

            accept_state_ = false;
        }

        public void Send(byte[] data)
        {
            lock (clients_) {
                clients_.ForEach(client => client.SendDataPush(data));
            }
        }
    }
}
