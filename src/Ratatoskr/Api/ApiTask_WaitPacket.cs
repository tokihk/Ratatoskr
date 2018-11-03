using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace Ratatoskr.Api
{
    public class ApiTask_WaitPacket : IApiTask
    {
        private ApiSandbox sandbox_;

        private ApiTask_CapturePacket capture_obj_;

        public PacketObject DetectPacket { get; private set; } = null;


        internal ApiTask_WaitPacket(ApiSandbox sandbox, ApiSandbox.WatchPacketType target, string filter = "")
        {
            sandbox_ = sandbox;
            capture_obj_ = new ApiTask_CapturePacket(sandbox, target, filter);
            capture_obj_.PacketDetected += OnPacketDetected;
        }

        public void Dispose()
        {
            Stop();
        }

        public bool IsRunning
        {
            get { return (capture_obj_.IsRunning); }
        }

        internal void StartAsync()
        {
            Stop();

            DetectPacket = null;

            capture_obj_.StartAsync();
        }

        public void Stop()
        {
            capture_obj_.Stop();
        }

        public PacketObject Join(int timeout_ms)
        {
            var sw_timeout = new System.Diagnostics.Stopwatch();

            /* timeoutが0以上のときのみタイムアウト監視 */
            if (timeout_ms >= 0) {
                sw_timeout.Restart();
            }

            /* 終了監視 */
            while (IsRunning) {
                /* タイムアウト監視 */
                if ((sw_timeout.IsRunning) && (sw_timeout.ElapsedMilliseconds >= timeout_ms)) {
                    break;
                }

                sandbox_.API_Sleep(10);
            }

            return (DetectPacket);
        }

        private void OnPacketDetected(object sender, PacketObject packet)
        {
            DetectPacket = packet;

            Stop();
        }
    }
}
