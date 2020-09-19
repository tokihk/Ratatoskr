using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;
using RtsCore.Framework.Device;

namespace Ratatoskr.Devices.TcpServer
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
		private DevicePropertyImpl devp_;

		private System.Net.Sockets.TcpListener		tcp_listener_ = null;
		private List<TcpClientObject>				tcp_clients_ = new List<TcpClientObject>();

		private object tcp_clients_sync_ = new object();

		private IAsyncResult accept_task_ar_ = null;

		private string local_ep_text_ = "";


        public DeviceInstanceImpl(DeviceManagementClass devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devm, devconf, devd, devp)
        {
        }

        protected override void OnConnectStart()
        {
			devp_ = Property as DevicePropertyImpl;
        }

        protected override EventResult OnConnectBusy()
        {
			var addr_family = (devp_.AddressFamily.Value == AddressFamilyType.IPv6) ? (AddressFamily.InterNetworkV6) : (AddressFamily.InterNetwork);

			try {
				/* ソケット生成 + バインド */
				switch (devp_.LocalBindMode.Value) {
					case BindModeType.INADDR_ANY:
						local_ep_text_ = string.Format("INADDR_ANY:{0}", devp_.LocalPortNo.Value);
						tcp_listener_ = new TcpListener((addr_family == AddressFamily.InterNetworkV6) ? (IPAddress.IPv6Any) : (IPAddress.Any), (ushort)devp_.LocalPortNo.Value);
						break;

					case BindModeType.SelectAddress:
						/* 選択中のアドレスファミリと異なる設定の場合はエラー */
						if (devp_.LocalIpAddress.Value.AddressFamily != addr_family) {
							return (EventResult.Error);
						}

						local_ep_text_ = string.Format("{0}:{1}", devp_.LocalIpAddress.Value, devp_.LocalPortNo.Value);
						tcp_listener_ = new TcpListener(devp_.LocalIpAddress.Value, (ushort)devp_.LocalPortNo.Value);
						break;
				}

				/* 待ち受け開始 */
				tcp_listener_.Start((int)devp_.Capacity.Value);

				return (EventResult.Success);

			} catch {
				return (EventResult.Error);
			}
        }

        protected override void OnConnected()
        {
        }

        protected override void OnDisconnectStart()
        {
			/* 受付処理終了 */
			tcp_listener_.Stop();

            /* 全クライアントをシャットダウン */
			tcp_clients_.ForEach(client => client.Dispose());
			tcp_clients_.Clear();
        }

        protected override PollState OnPoll()
        {
			var busy = false;

			/* クライアント待ち受け処理 */
			AcceptPoll();

            /* 送信処理 */
            SendPoll();

            /* クライアント処理 */
			lock (tcp_clients_sync_) {
				tcp_clients_.ForEach(client => client.Poll(ref busy));

				/* 切断済みのクライアントを除外 */
				tcp_clients_.RemoveAll(client => client.IsDisposed);
			}

            return ((busy) ? (PollState.Busy) : (PollState.Idle));
        }

		private void AcceptPoll()
		{
			/* 接続待機中 */
			if ((accept_task_ar_ != null) && (!accept_task_ar_.IsCompleted)) {
				return;
			}

			/* 次の接続要求の受付処理を開始 */
			accept_task_ar_ = tcp_listener_.BeginAcceptTcpClient(AcceptTcpClientTaskComplete, tcp_listener_);
		}

		private void SendPoll()
		{
			byte[] send_data;

			while ((send_data = GetSendData()) != null) {
				/* 全クライアントに向けて送信開始 */
				tcp_clients_.ForEach(client => client.SendSetup(send_data));
			}
		}

		private void AcceptTcpClientTaskComplete(IAsyncResult ar)
		{
			try {
				if (ar.AsyncState is TcpListener tcp_listener) {
					var tcp_client = tcp_listener_.EndAcceptTcpClient(ar);

					/* 新しいクライアントの受け入れ */
					if (tcp_client != null) {
						/* Buffer Size */
						tcp_client.SendBufferSize = (int)devp_.SendBufferSize.Value;
						tcp_client.ReceiveBufferSize = (int)devp_.RecvBufferSize.Value;

						/* Reuse Address */
						if (devp_.ReuseAddress.Value) {
							tcp_client.Client.SetSocketOption(
								(devp_.AddressFamily.Value == AddressFamilyType.IPv6) ? (SocketOptionLevel.IPv6) : (SocketOptionLevel.IP),
								SocketOptionName.ReuseAddress,
								true);
						}

						/* Keep Alive設定 */
						if (devp_.KeepAliveOnOff.Value) {
							SocketIOControl(
								tcp_client.Client,
								IOControlCode.KeepAliveValues,
								(uint)((devp_.KeepAliveOnOff.Value) ? (1u) : (0u)),
								(uint)devp_.KeepAliveTime_Value.Value,
								(uint)devp_.KeepAliveInterval_Value.Value);
						}

						/* TTL設定 */
						if (devp_.TTL_Unicast.Value) {
							tcp_client.Client.Ttl = (byte)devp_.TTL_Unicast_Value.Value;
						}

						/* クライアントリストへ追加 */
						lock (tcp_clients_sync_) {
							tcp_clients_.Add(new TcpClientObject(this, tcp_client));
						}
					}
				}
			} catch {
			}
		}

		private void SocketIOControl(Socket socket, IOControlCode code, params UInt32[] outputs)
		{
			var output_values = new List<byte>();

			foreach (var output in outputs) {
				output_values.AddRange(BitConverter.GetBytes(output));
			}

			socket.IOControl(code, output_values.ToArray(), null);
		}

		public void NotifyClientConnect(string remote_ep_text)
		{
			NotifyMessage(PacketPriority.Standard, "Device Event", String.Format("Connect <= [{0:G}]", remote_ep_text));
		}

		public void NotifyClientDisconnect(string remote_ep_text)
		{
			NotifyMessage(PacketPriority.Standard, "Device Event", String.Format("Disconnect => [{0:G}]", remote_ep_text));
		}

		public void NotifyClientSendComplete(string remote_ep_text, byte[] send_data)
		{
            NotifySendComplete("", local_ep_text_, remote_ep_text, send_data);
		}

		public void NotifyClientRecvComplete(string remote_ep_text, byte[] recv_data)
		{
            NotifyRecvComplete("", remote_ep_text, local_ep_text_, recv_data);
		}
    }
}
