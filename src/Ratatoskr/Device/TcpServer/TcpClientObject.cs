using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ratatoskr.General;
using Ratatoskr.General.Packet;

namespace Ratatoskr.Device.TcpServer
{
    internal class TcpClientObject
    {
		private DeviceInstanceImpl	devi_;

		private System.Net.Sockets.TcpClient	tcp_client_;

        private IAsyncResult send_task_ar_ = null;
        private IAsyncResult recv_task_ar_ = null;

		private string remote_ep_text_ = "";

		private Queue<byte[]> send_data_list_ = new Queue<byte[]>();

        private byte[] send_data_;
        private byte[] recv_data_;

		private bool dispose_state_ = false;


        public TcpClientObject(DeviceInstanceImpl devi, System.Net.Sockets.TcpClient tcp_client)
        {
            devi_ = devi;
            tcp_client_ = tcp_client;

            remote_ep_text_ = string.Format(
                                "{0:G}:{1:G}",
                                ((IPEndPoint)tcp_client_.Client.RemoteEndPoint).Address.ToString(),
                                ((IPEndPoint)tcp_client_.Client.RemoteEndPoint).Port.ToString());

            recv_data_ = new byte[tcp_client_.ReceiveBufferSize];

			NotifyClientConnect();
        }

		public void Dispose()
		{
			lock (this) {
				/* 既に開放処理中 */
				if (dispose_state_) {
					return;
				}

				dispose_state_ = true;

				/* 破棄開始 */
				(new DisposeTaskDelegate(DisposeTask)).BeginInvoke(null, null);

				/* 切断通知 */
				NotifyClientDisconnect();
			}
		}

		private delegate void DisposeTaskDelegate();
        private void DisposeTask()
        {
            /* 切断処理 */
            tcp_client_.Close();

			/* タスク終了待ち */
			{
				var handles = new List<WaitHandle>();

				if (send_task_ar_ != null) { handles.Add(send_task_ar_.AsyncWaitHandle); }
				if (recv_task_ar_ != null) { handles.Add(recv_task_ar_.AsyncWaitHandle); }

				if (handles.Count > 0) {
					WaitHandle.WaitAll(handles.ToArray(), 5000);
				}
			}
        }

        public bool IsDisposed
        {
            get { return (dispose_state_); }
        }

		public void SendSetup(byte[] data)
		{
            if (dispose_state_)return;

			send_data_list_.Enqueue(data);
		}

        public void Poll(ref bool busy)
        {
            if (dispose_state_)return;

            SendPoll(ref busy);
			RecvPoll(ref busy);
        }

        private void SendPoll(ref bool busy)
        {
			/* 送信中 */
			if ((send_task_ar_ != null) && (!send_task_ar_.IsCompleted)) {
				busy = true;
				return;
			}

			/* 送信データ取得 */
			if ((send_data_ == null) && (send_data_list_.Count > 0)) {
				send_data_ = send_data_list_.Dequeue();
			}

			/* 送信するデータが存在しない */
			if (send_data_ == null) {
				return;
			}

			/* 送信開始 */
			send_task_ar_ = tcp_client_.GetStream().BeginWrite(send_data_, 0, send_data_.Length, SendTaskComplete, tcp_client_);
        }

		private void SendTaskComplete(IAsyncResult ar)
		{
			try {
				if (ar.AsyncState is System.Net.Sockets.TcpClient tcp_client) {
					/* 送信完了 */
					tcp_client.GetStream().EndWrite(ar);

					/* 通知 */
					NotifyClientSendComplete(send_data_);

					send_data_ = null;
				}
			} catch {
			}
		}

        private void RecvPoll(ref bool busy)
        {
			/* 受信中 */
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
						/* 受信完了通知 */
						NotifyClientRecvComplete(ClassUtil.CloneCopy(recv_data_, recv_size));

					} else {
						/* 切断イベント */
						Dispose();
					}
				}
			} catch {
			}
		}

		private void NotifyClientConnect()
		{
			devi_.NotifyClientConnect(remote_ep_text_);
		}

		private void NotifyClientDisconnect()
		{
			devi_.NotifyClientDisconnect(remote_ep_text_);
		}

		private void NotifyClientSendComplete(byte[] send_data)
		{
            devi_.NotifyClientSendComplete(remote_ep_text_, send_data);
		}

		private void NotifyClientRecvComplete(byte[] recv_data)
		{
            devi_.NotifyClientRecvComplete(remote_ep_text_, recv_data);
		}
    }
}
