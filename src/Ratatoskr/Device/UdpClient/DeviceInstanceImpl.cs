using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;

namespace Ratatoskr.Device.UdpClient
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private DevicePropertyImpl devp_;

		private System.Net.Sockets.UdpClient	udp_client_;

		private IPEndPoint	local_ep_ = null;
		private string		local_ep_text_ = "";

		private IPEndPoint	remote_ep_ = null;
		private string		remote_ep_text_ = "";

        private byte[]			send_data_ = null;
		private IAsyncResult	send_task_ar_ = null;

		private IPEndPoint		recv_remote_ep_ = new IPEndPoint(IPAddress.Any, 0);
		private IAsyncResult	recv_task_ar_ = null;


		public DeviceInstanceImpl(DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devconf, devd, devp)
        {
            devp_ = devp as DevicePropertyImpl;
        }

        protected override void OnConnectStart()
        {
			send_task_ar_ = null;
			recv_task_ar_ = null;
        }

        protected override EventResult OnConnectBusy()
        {
			/* 処理中のオブジェクトを全て開放する */
			if (udp_client_ != null) {
				udp_client_.Close();
				udp_client_ = null;
			}

			var addr_family = (devp_.AddressFamily.Value == AddressFamilyType.IPv6) ? (AddressFamily.InterNetworkV6) : (AddressFamily.InterNetwork);

			/* === Local Setting === */
			try {
				switch (devp_.LocalBindMode.Value) {
					case BindModeType.NotBind:
						local_ep_ = null;
						local_ep_text_ = "";
						udp_client_ = new System.Net.Sockets.UdpClient(addr_family);
						break;
					case BindModeType.INADDR_ANY:
						local_ep_ = new IPEndPoint((addr_family == AddressFamily.InterNetworkV6) ? (IPAddress.IPv6Any) : (IPAddress.Any), (ushort)devp_.LocalPortNo.Value);
						local_ep_text_ = string.Format("INADDR_ANY:{0:G}", devp_.LocalPortNo.Value.ToString());
						udp_client_ = new System.Net.Sockets.UdpClient(local_ep_);
						break;
					case BindModeType.SelectAddress:
						local_ep_ = new IPEndPoint(devp_.LocalIpAddress.Value, (int)devp_.LocalPortNo.Value);
						local_ep_text_ = string.Format("{0:G}:{1:G}", local_ep_.Address.ToString(), local_ep_.Port.ToString());
						udp_client_ = new System.Net.Sockets.UdpClient(local_ep_);
						break;
					default:
						return (EventResult.Error);
				}

				/* 生成したエンドポイントが設定しているアドレスファミリーと異なる場合はエラー */
				if ((local_ep_ != null) && (local_ep_.AddressFamily != addr_family)) {
					return (EventResult.Error);
				}

			} catch {
				return (EventResult.Error);
			}

			/* === Remote Setting === */
			try {
				switch (devp_.RemoteAddressType.Value) {
					case AddressType.Unicast:
						remote_ep_ = new IPEndPoint(devp_.RemoteIpAddress.Value, (int)devp_.RemotePortNo.Value);
						break;
					case AddressType.Broadcast:
						udp_client_.EnableBroadcast = true;
						remote_ep_ = new IPEndPoint(IPAddress.Broadcast, (int)devp_.RemotePortNo.Value);
						break;
					case AddressType.Multicast:
						remote_ep_ = new IPEndPoint(devp_.RemoteIpAddress.Value, (int)devp_.RemotePortNo.Value);
						break;
					default:
						return (EventResult.Error);
				}

				remote_ep_text_ = string.Format("{0:G}:{1:G}", remote_ep_.Address.ToString(), remote_ep_.Port.ToString());

				/* 生成したエンドポイントが設定しているアドレスファミリーと異なる場合はエラー */
				if ((remote_ep_ != null) && (remote_ep_.AddressFamily != addr_family)) {
					return (EventResult.Error);
				}

			} catch {
				return (EventResult.Error);
			}

			/* Unicast - TTL */
			if (devp_.Unicast_TTL.Value) {
				udp_client_.Ttl = (byte)devp_.Unicast_TTL_Value.Value;
			}

			/* Multicast - TTL */
			if (devp_.Multicast_TTL.Value) {
				if (devp_.AddressFamily.Value == AddressFamilyType.IPv6) {
					udp_client_.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.MulticastTimeToLive, (byte)devp_.Multicast_TTL_Value.Value);
				} else {
					udp_client_.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, (byte)devp_.Multicast_TTL_Value.Value);
				}
			}

			/* Multicast - Loopback */
			if (devp_.Multicast_Loopback.Value) {
				udp_client_.MulticastLoopback = devp_.Multicast_Loopback.Value;
			}

			/* Multicast Interface */
			var nic_index = -1;

			if (devp_.Multicast_Interface.Value) {
				var nics = NetworkInterface.GetAllNetworkInterfaces();

				if ((nics != null) && (nics.Length > 0)) {
					for (var nic_index_temp = 0; nic_index_temp < nics.Length; nic_index_temp++) {
						if (nics[nic_index_temp].Id == devp_.Multicast_Interface_Value.Value) {
							nic_index = nic_index_temp;
							break;
						}
					}
				}
			}

			/* Multicast - Group */
			if (devp_.Multicast_GroupAddress.Value) {
				IPAddress ipaddr;

				foreach (var ipaddr_text in devp_.Multicast_GroupAddressList.Value) {
					if ((IPAddress.TryParse(ipaddr_text, out ipaddr)) && (ipaddr.AddressFamily == addr_family)) {
						if (nic_index >= 0) {
							udp_client_.JoinMulticastGroup(nic_index, ipaddr);
						} else {
							udp_client_.JoinMulticastGroup(ipaddr);
						}
					}
				}
			}

			return (EventResult.Success);
        }

        protected override void OnConnected()
        {
			/* リモートエンドポイントテキスト作成 */
			if (remote_ep_ != null) {
				remote_ep_text_ = string.Format(
									"{0:G}:{1:G}",
									remote_ep_.Address.ToString(),
									remote_ep_.Port.ToString());
			}

			/* 受信処理開始 */
			RecvStart();
		}

		protected override void OnDisconnectStart()
        {
			if (udp_client_ == null)return;

			udp_client_.Client.Close();

			/* 送信状態と受信状態が終了するまで待機 */
			while (   ((send_task_ar_ != null) && (!send_task_ar_.IsCompleted))
				   || ((recv_task_ar_ != null) && (!recv_task_ar_.IsCompleted))
			) {
			}

			udp_client_ = null;
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
			send_task_ar_ = udp_client_.BeginSend(send_data_, send_data_.Length, remote_ep_, SendTaskComplete, udp_client_);

			/* 送信完了通知(リクエストが完了したときに通知する場合) */
			NotifySendComplete("", local_ep_text_, remote_ep_text_, send_data_);

			busy = true;
		}

		private void SendTaskComplete(IAsyncResult ar)
		{
			try {
				if (ar.AsyncState is System.Net.Sockets.UdpClient udp_client) {
					/* 送信処理完了 */
					var send_complete_size = udp_client.EndSend(ar);
					var send_complete_data = send_data_;

					if (send_complete_size >= send_data_.Length) {
						/* 要求した送信データの全送信完了 */
						send_data_ = null;
					} else {
						/* 要求した送信データの一部のみ送信完了 */
						send_complete_data = ClassUtil.CloneCopy(send_data_, send_complete_size);
						send_data_ = ClassUtil.CloneCopy(send_data_, send_complete_size, send_data_.Length);
					}

					/* 送信完了通知(バッファへのセットアップが完了したときに通知する場合) */
					// NotifySendComplete("", local_ep_text_, remote_ep_text_, send_complete_data);
				}
			} catch {
			}
		}

		private void RecvStart()
		{
			if (!udp_client_.Client.IsBound)return;

			/* 受信状態であれば無視 */
			if ((recv_task_ar_ != null) && (!recv_task_ar_.IsCompleted)) {
				return;
			}

			/* 受信開始 */
			recv_task_ar_ = udp_client_.BeginReceive(RecvTaskComplete, udp_client_);
		}

		private void RecvTaskComplete(IAsyncResult ar)
		{
			try {
				if (ar.AsyncState is System.Net.Sockets.UdpClient udp_client) {
					/* 受信完了 */
					var recv_data = udp_client.EndReceive(ar, ref recv_remote_ep_);

					if (recv_data.Length > 0) {
						/* 受信通知 */
						NotifyRecvComplete("", GetEndPointString(recv_remote_ep_), local_ep_text_, recv_data);
					}

					RecvStart();
				}
			} catch {
			}
		}

		private string GetEndPointString(IPEndPoint ep)
		{
			return (string.Format(
							"{0:G}:{1:G}",
							remote_ep_.Address.ToString(),
							remote_ep_.Port.ToString()));
		}
    }
}
