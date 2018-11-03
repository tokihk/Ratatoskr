using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using RtsCore.Packet;
using RtsCore.Framework.Packet.Filter;

namespace Ratatoskr.Api
{
    public class ApiTask_CapturePacket : IApiTask
    {
        private bool run_state_ = false;
        private bool cancel_req_ = false;

        private PacketFilterController filter_obj_ = null;

        public string                     CaptureFilter { get; }
        public ApiSandbox.WatchPacketType CaptureTarget { get; }

        public ulong DetectPacketCount { get; private set; } = 0;

        internal event ApiSandbox.PacketDetectedHandler PacketDetected; 


        internal ApiTask_CapturePacket(ApiSandbox sandbox, ApiSandbox.WatchPacketType target, string filter = "")
        {
            CaptureFilter = filter ?? ("");
            CaptureTarget = target;
        }

        public void Dispose()
        {
            Stop();
        }

        private void RegisterWatchEvent()
        {
            if (CaptureTarget.HasFlag(ApiSandbox.WatchPacketType.RawPacket)) {
                GatePacketManager.RawPacketEntried += OnPacketEntried;
            }

            if (CaptureTarget.HasFlag(ApiSandbox.WatchPacketType.ViewPacket)) {
                FormTaskManager.DrawPacketEntried += OnPacketEntried;
            }
        }

        private void UnregisterWatchEvent()
        {
            GatePacketManager.RawPacketEntried -= OnPacketEntried;
            FormTaskManager.DrawPacketEntried -= OnPacketEntried;
        }

        public bool IsRunning
        {
            get { return ((run_state_) && (!cancel_req_)); }
        }

        internal void StartAsync()
        {
            Stop();

            /* 進捗初期化 */
            DetectPacketCount = 0;

            /* フィルターモジュール生成 */
            filter_obj_ = PacketFilterController.Build(CaptureFilter);

            if (filter_obj_ == null)return;

            /* 監視イベント登録 */
            RegisterWatchEvent();

            /* 監視開始 */
            cancel_req_ = false;
            run_state_ = true;
        }

        public void Stop()
        {
            cancel_req_ = true;

            UnregisterWatchEvent();

            run_state_ = false;
        }

        private void OnPacketEntried(IEnumerable<PacketObject> packets)
        {
            if (cancel_req_)return;
            if (PacketDetected == null)return;

            foreach (var packet in packets) {
                if (cancel_req_)break;

                if ((filter_obj_.Input(packet)) && (!cancel_req_)) {
                    DetectPacketCount++;
                    PacketDetected(this, packet);
                }
            }
        }
    }
}
