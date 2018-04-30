using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Scripts.PacketFilterExp;

namespace Ratatoskr.Scripts.API
{
    public class API_RecvWait
    {
        public enum WatchPacketType
        {
            RawPacket,
            ViewPacket,
        }


        private IAsyncResult ar_task_ = null;
        private EventHandler ev_completed_ = null;

        public bool CancelRequest { get; private set; } = false;
        public bool Success       { get; private set; } = false;

        private ExpressionFilter detect_filter_ = null;


        private void Dispose()
        {
            if (ev_completed_ != null) {
                ev_completed_(this, EventArgs.Empty);
                ev_completed_ = null;
            }
        }

        public void Cancel()
        {
            CancelRequest = true;
            while (IsBusy) {
                System.Threading.Thread.Sleep(10);
            }
            CancelRequest = false;

            Dispose();

            Success = false;

            GatePacketManager.RawPacketEntried -= OnPacketEntried;
            FormTaskManager.DrawPacketEntried -= OnPacketEntried;

            detect_filter_ = null;
        }

        public bool IsBusy
        {
            get { return ((ar_task_ != null) && (!ar_task_.IsCompleted));}
        }

        internal void ExecAsync(string filter_exp, WatchPacketType watch_type, uint timeout, EventHandler complete)
        {
            Cancel();

            if (   (filter_exp == null)
                || (timeout == 0)
            ) {
                Dispose();
                return;
            }

            /* フィルターモジュール生成 */
            detect_filter_ = ExpressionFilter.Build(filter_exp);

            if (detect_filter_ == null) {
                Dispose();
                return;
            }

            /* イベント登録 */
            switch (watch_type) {
                case WatchPacketType.RawPacket:
                    GatePacketManager.RawPacketEntried += OnPacketEntried;
                    break;
                case WatchPacketType.ViewPacket:
                    FormTaskManager.DrawPacketEntried += OnPacketEntried;
                    break;

                default:
                    Dispose();
                    return;
            }

            /* 監視開始 */
            ar_task_ = (new ExecTaskHandler(ExecTask)).BeginInvoke(timeout, null, null);
        }

        private delegate void ExecTaskHandler(uint timeout);
        private void ExecTask(uint timeout)
        {
            var sw_timeout = new System.Diagnostics.Stopwatch();

            sw_timeout.Restart();
            while ((!CancelRequest) && (!Success) && (sw_timeout.ElapsedMilliseconds < timeout)) {
                System.Threading.Thread.Sleep(10);
            }

            Dispose();
        }

        private void OnPacketEntried(IEnumerable<PacketObject> packets)
        {
            if (Success)return;

            foreach (var packet in packets) {
                if (detect_filter_.Input(packet)) {
                    Success = true;
                    return;
                }
            }
        }
    }
}
