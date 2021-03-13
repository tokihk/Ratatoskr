using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.FileFormat;
using Ratatoskr.Gate;
using Ratatoskr.General.Packet.Filter;
using Ratatoskr.General.Packet;

namespace Ratatoskr.Api
{
    public class ApiTask_ReplayRecordFile : IApiTask
    {
        private const int SEND_TIMMING_MARGIN = -3;

        private ApiSandbox sandbox_ = null;

        private bool       cancel_req_ = false;

        private IAsyncResult ar_task_ = null;

        public string GateAlias { get; }
        public string FilePath  { get; }
        public string Filter    { get; }

        public uint ProgressMax   { get; private set; } = 1;
        public uint ProgressNow   { get; private set; } = 1;

        public bool Success { get; private set; } = false;


        internal ApiTask_ReplayRecordFile(ApiSandbox sandbox, string gate_alias, string file_path, string filter = "")
        {
            sandbox_ = sandbox;

            GateAlias = gate_alias ?? "";
            FilePath = file_path ?? "";
            Filter = filter ?? "";
        }

        public void Dispose()
        {
            Stop();
        }

        public bool IsRunning
        {
            get { return ((ar_task_ != null) && (!ar_task_.IsCompleted));}
        }

        internal void StartAsync()
        {
            Stop();

            /* 進捗初期化 */
            cancel_req_ = false;
            Success = false;
            ProgressMax = 1;
            ProgressNow = 1;

            /* 送信ログファイル取得 */
            var info = FileManager.PacketLogOpen.GetReadTargetFromPath(FilePath);

            if (info == null)return;
            if (info.FilePath == null)return;

            /* フィルターモジュール生成 */
            var filter_obj = PacketFilterController.Build(Filter);

            if (filter_obj == null)return;

            /* 送信先ゲート取得 */
            var gate_objs = GateManager.FindGateObjectFromWildcardAlias(GateAlias);

            if (gate_objs == null)return;

            /* 送信タスク開始 */
            ar_task_ = (new ExecTaskHandler(ExecTask)).BeginInvoke(
                gate_objs, filter_obj, info, null, null);
        }

        public void Stop()
        {
            cancel_req_ = true;

            while (IsRunning) {
                sandbox_.API_Sleep(10);
            }
        }

        public bool Join(int timeout_ms = -1)
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

            return (Success);
        }

        private delegate void ExecTaskHandler(GateObject[] gates, PacketFilterController filter, FileReadTargetInfo info);
        private void ExecTask(GateObject[] gates, PacketFilterController filter, FileReadTargetInfo info)
        {
            PlayRecord(gates, filter, info);

            Success = (!cancel_req_);
        }

        private void PlayRecord(GateObject[] gates, PacketFilterController filter, FileReadTargetInfo info)
        {
            var reader = info.Reader as PacketLogReader;

            if (reader == null)return;

            if (!reader.Open(info.Option, info.FilePath))return;

            ProgressMax = (uint)reader.ProgressMax;
            ProgressNow = (uint)reader.ProgressNow;

            var packet_busy = LoadPlayPacket(reader, filter);
            var packet_next = (PacketObject)null;
            var delay_timer = new System.Diagnostics.Stopwatch();
            var delay_value = 0;

            while ((!cancel_req_) && (packet_busy != null)) {
                /* 次のデータ送信までの遅延 */
                while ((delay_timer.IsRunning) && (delay_timer.ElapsedMilliseconds < delay_value)) {
                    if (delay_timer.ElapsedMilliseconds > 10) {
                        System.Threading.Thread.Sleep(1);
                    }
                }

                /* パケット送信 */
                delay_timer.Restart();
                foreach (var gate in gates) {
                    gate.SendRequest(packet_busy.Data);
                }

                /* 次のパケットを取得 */
                packet_next = LoadPlayPacket(reader, filter);
                if (packet_next != null) {
                    delay_value = (int)(packet_next.MakeTime - packet_busy.MakeTime).TotalMilliseconds + SEND_TIMMING_MARGIN;
                }
                packet_busy = packet_next;

                ProgressNow = (uint)reader.ProgressNow;
            }

            ProgressNow = (uint)reader.ProgressNow;

            reader.Close();
        }

        private PacketObject LoadPlayPacket(PacketLogReader reader, PacketFilterController filter)
        {
            var packet = (PacketObject)null;

            while ((packet = reader.ReadPacket()) != null) {
                /* データパケット以外は無視 */
                if (packet.Attribute != PacketAttribute.Data)continue;

                /* フィルターに合致しないパケットは無視 */
                if (!filter.Input(packet))continue;

                return (packet);
            }

            return (null);
        }
    }
}
