using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.FileFormats;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Packet;
using Ratatoskr.Scripts.PacketFilterExp;

namespace Ratatoskr.Scripts.API
{
    public class API_PlayRecord
    {
        private const int SEND_TIMMING_MARGIN = -3;

        private bool         cancel_req_ = false;
        private IAsyncResult ar_task_ = null;
        private EventHandler ev_completed_ = null;

        public bool Success       { get; private set; } = false;
        public uint ProgressMax   { get; private set; } = 0;
        public uint ProgressNow   { get; private set; } = 0;


        private void Dispose()
        {
            cancel_req_ = false;
            if (ev_completed_ != null) {
                ev_completed_(this, EventArgs.Empty);
                ev_completed_ = null;
            }

            Success = false;
            ProgressMax = 0;
            ProgressNow = 0;
        }

        public bool IsBusy
        {
            get { return ((ar_task_ != null) && (!ar_task_.IsCompleted));}
        }

        public void CancelRequest()
        {
            cancel_req_ = true;
        }

        internal void ExecAsync(string gate, string file_path, string filter_exp, EventHandler complete)
        {
            Dispose();

            ev_completed_ = complete;

            if (   (gate == null)
                || (file_path == null)
                || (!File.Exists(file_path))
                || (filter_exp == null)
            ) {
                Dispose();
                return;
            }

            /* 送信ログファイル取得 */
            var reader_info = FormUiManager.CreatePacketLogReader(file_path);
            var reader = reader_info.reader as PacketLogReader;

            if (   (reader == null)
                || (reader_info.paths.Length == 0)
            ) {
                Dispose();
                return;
            }

            /* フィルターモジュール生成 */
            var detect_filter = ExpressionFilter.Build(filter_exp);

            if (detect_filter == null) {
                Dispose();
                return;
            }

            /* 送信先ゲート取得 */
            var gate_objs = GateManager.FindGateObjectFromWildcardAlias(gate);

            if (gate_objs == null) {
                Dispose();
                return;
            }

            ProgressMax = 0;
            ProgressNow = 0;

            /* 送信タスク開始 */
            ar_task_ = (new ExecTaskHandler(ExecTask)).BeginInvoke(
                gate_objs, detect_filter, reader, reader_info.option, new Queue<string>(reader_info.paths), ExecTaskComplete, null);
        }

        private delegate void ExecTaskHandler(GateObject[] gates, ExpressionFilter filter, PacketLogReader reader, FileFormatOption option, Queue<string> paths);
        private void ExecTask(GateObject[] gates, ExpressionFilter filter, PacketLogReader reader, FileFormatOption option, Queue<string> paths)
        {
            while ((!cancel_req_) && (paths.Count > 0)) {
                PlayRecord(gates, filter, reader, option, paths.Dequeue());
            }
        }

        private void ExecTaskComplete(IAsyncResult ar)
        {
            Dispose();
        }

        private void PlayRecord(GateObject[] gates, ExpressionFilter filter, PacketLogReader reader, FileFormatOption option, string path)
        {
            if (!reader.Open(option, path))return;

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

        private PacketObject LoadPlayPacket(PacketLogReader reader, ExpressionFilter filter)
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
