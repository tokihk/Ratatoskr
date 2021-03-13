using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;
using Ratatoskr.General.Packet;

namespace Ratatoskr.Device.TcpClient
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
		private DevicePropertyImpl devp_;

		private System.Net.Sockets.TcpClient tcp_client_ = null;

        private IAsyncResult connect_task_ar_ = null;
        private IAsyncResult send_task_ar_ = null;
        private IAsyncResult recv_task_ar_ = null;

		private string local_ep_text_ = "";
		private string remote_ep_text_ = "";

        private byte[] send_data_;
        private byte[] recv_data_;


        public DeviceInstanceImpl(DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devconf, devd, devp)
        {
        }

        protected override void OnConnectStart()
        {
			devp_ = Property as DevicePropertyImpl;

			connect_task_ar_ = null;
			send_task_ar_ = null;
			recv_task_ar_ = null;

			send_data_ = null;
			recv_data_ = null;
        }

        protected override EventResult OnConnectBusy()
        {
			void SocketIOControl(Socket socket, IOControlCode code, params UInt32[] outputs)
			{
				var output_values = new List<byte>();

				foreach (var output in outputs) {
					output_values.AddRange(BitConverter.GetBytes(output));
				}

				socket.IOControl(code, output_values.ToArray(), null);
			}

			var addr_family = (devp_.AddressFamily.Value == AddressFamilyType.IPv6) ? (AddressFamily.InterNetworkV6) : (AddressFamily.InterNetwork);

			try {
				/* 接続準備 */
				if (connect_task_ar_ == null) {
					/* 古いオブジェクトを削除 */
					if (tcp_client_ != null) {
						tcp_client_.Close();
						tcp_client_.Dispose();
					}

					/* 選択中のアドレスファミリと異なる設定の場合はエラー */
					if (devp_.RemoteIpAddress.Value.AddressFamily != addr_family) {
						return (EventResult.Error);
					}

					/* ソケット生成 */
					tcp_client_ = new System.Net.Sockets.TcpClient(addr_family);

					/* Buffer Size */
					tcp_client_.SendBufferSize = (int)devp_.SendBufferSize.Value;
					tcp_client_.ReceiveBufferSize = (int)devp_.RecvBufferSize.Value;

					/* Reuse Address */
					if (devp_.ReuseAddress.Value) {
						tcp_client_.Client.SetSocketOption(
							(addr_family == AddressFamily.InterNetworkV6) ? (SocketOptionLevel.IPv6) : (SocketOptionLevel.IP),
							SocketOptionName.ReuseAddress,
							true);
					}

					/* Keep Alive設定 */
					if (devp_.KeepAliveOnOff.Value) {
						SocketIOControl(
							tcp_client_.Client,
							IOControlCode.KeepAliveValues,
							(uint)((devp_.KeepAliveOnOff.Value) ? (1u) : (0u)),
							(uint)devp_.KeepAliveTime_Value.Value,
							(uint)devp_.KeepAliveInterval_Value.Value);
					}

					/* TTL設定 */
					if (devp_.TTL_Unicast.Value) {
						tcp_client_.Client.Ttl = (byte)devp_.TTL_Unicast_Value.Value;
					}

					/* 接続開始 */
					connect_task_ar_ = tcp_client_.BeginConnect(devp_.RemoteIpAddress.Value, (ushort)devp_.RemotePortNo.Value, null, null);
				}

				/* 接続処理 */
				if (connect_task_ar_ != null) {
					/* 接続処理待ち */
					if (!connect_task_ar_.IsCompleted) {
						return (EventResult.Busy);
					}

					/* 接続失敗 */
					if (!tcp_client_.Connected) {
						connect_task_ar_ = null;
						return (EventResult.Error);
					}

					tcp_client_.EndConnect(connect_task_ar_);
				}

				/* 接続完了 */
				return (EventResult.Success);
			} catch {
				connect_task_ar_ = null;

				return (EventResult.Error);
			}
        }

        protected override void OnConnected()
        {
            local_ep_text_ = string.Format(
                            "{0:G}:{1:G}",
                            ((IPEndPoint)tcp_client_.Client.LocalEndPoint).Address.ToString(),
                            ((IPEndPoint)tcp_client_.Client.LocalEndPoint).Port.ToString());

            remote_ep_text_ = string.Format(
                            "{0:G}:{1:G}",
                            ((IPEndPoint)tcp_client_.Client.RemoteEndPoint).Address.ToString(),
                            ((IPEndPoint)tcp_client_.Client.RemoteEndPoint).Port.ToString());

            recv_data_ = new byte[tcp_client_.ReceiveBufferSize];

            NotifyMessage(
                PacketPriority.Standard,
                "Device Event",
                String.Format("Connect => [{0:G}]", remote_ep_text_));

			/* 受信開始 */
			RecvStart();
        }

        protected override void OnDisconnectStart()
        {
        }

        protected override void OnDisconnected()
        {
			if (tcp_client_ != null) {
				if (tcp_client_.Connected) {
					tcp_client_.Close();
				}

				NotifyMessage(
					PacketPriority.Standard,
					"Device Event",
					String.Format("Disconnect <= [{0:G}]", remote_ep_text_));

				tcp_client_.Dispose();
				tcp_client_ = null;
			}
        }

        protected override PollState OnPoll()
        {
            var busy = false;

            SendPoll(ref busy);

            return ((busy) ? (PollState.Active) : (PollState.Idle));
        }

		private void SendPoll(ref bool busy)
		{
			/* 送信状態であればアクティブ状態でスキップ */
			if ((send_task_ar_ != null) && (!send_task_ar_.IsCompleted)) {
				busy = true;
				return;
			}

			/* 切断状態であれば再接続 */
			if (!tcp_client_.Connected) {
				ConnectReboot();
				return;
			}

			/* 送信ブロック取得 */
			if (send_data_ == null) {
				send_data_ = GetSendData();
			}

			/* 送信データが存在しない場合はIDLE状態でスキップ */
			if (send_data_ == null) {
				busy = false;
				return;
			}

			/* 送信開始 */
			send_task_ar_ = tcp_client_.GetStream().BeginWrite(send_data_, 0, send_data_.Length, SendTaskComplete, tcp_client_);

			/* 送信完了通知(リクエスト時点で通知する場合) */
			// NotifySendComplete("", local_ep_text_, remote_ep_text_, send_data_);

			busy = true;
		}

		private void SendTaskComplete(IAsyncResult ar)
		{
			try {
				if (ar.AsyncState is System.Net.Sockets.TcpClient tcp_client) {
					/* 送信処理完了 */
					tcp_client.GetStream().EndWrite(ar);

					/* 送信完了通知(バッファへのセットアップが完了したときに通知する場合) */
					NotifySendComplete("", local_ep_text_, remote_ep_text_, send_data_);

					send_data_ = null;
				}
			} catch {
				ConnectReboot();
			}
		}

		private void RecvStart()
		{
			if (!tcp_client_.Client.IsBound)return;

			/* 受信状態であれば無視 */
			if ((recv_task_ar_ != null) && (!recv_task_ar_.IsCompleted)) {
				return;
			}

			/* 受信開始 */
			recv_task_ar_ = tcp_client_.GetStream().BeginRead(recv_data_, 0, recv_data_.Length, RecvTaskComplete, tcp_client_);
		}

		private void RecvTaskComplete(IAsyncResult ar)
		{
			try {
				if (ar.AsyncState is System.Net.Sockets.TcpClient tcp_client) {
					/* 受信完了 */
					var recv_size = tcp_client.GetStream().EndRead(ar);

					if (recv_size > 0) {
						/* 受信通知 */
						NotifyRecvComplete("", remote_ep_text_, local_ep_text_, ClassUtil.CloneCopy(recv_data_, recv_size));
					} else {
						/* 切断通知 */
						ConnectReboot();
					}

					RecvStart();
				}
			} catch {
				ConnectReboot();
			}
		}
    }
}
