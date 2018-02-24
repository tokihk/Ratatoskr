using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.FileFormats;
using Ratatoskr.Gate;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_PlayRecord : ActionObject
    {
        private const int SEND_TIMMING_MARGIN = -3;

        public enum Argument
        {
            Gate,
            LogPath,
            DataType,
        }

        public enum ArgumentDataType
        {
            RecvDataOnly,
            SendDataOnly,
            RecvAndSendData,
        }

        public enum Result
        {
            State,
        }


        private bool exit_req_ = false;

        private IAsyncResult ar_task_ = null;

        private GateObject[]     gates_  = null;
        private PacketLogReader  reader_ = null;
        private FileFormatOption reader_option_ = null;
        private Queue<string>    reader_paths_ = null;

        private ArgumentDataType packet_data_type_;


        public Action_PlayRecord()
        {
            RegisterArgument(Argument.Gate.ToString(), typeof(string), null);
            RegisterArgument(Argument.LogPath.ToString(), typeof(string), null);
            RegisterArgument(Argument.DataType.ToString(), typeof(int), (int)ArgumentDataType.RecvDataOnly);
        }

        public Action_PlayRecord(string gate, string log_path, ArgumentDataType type) : this()
        {
            SetArgumentValue(Argument.Gate.ToString(), gate);
            SetArgumentValue(Argument.LogPath.ToString(), log_path);
            SetArgumentValue(Argument.DataType.ToString(), (int)type);
        }

        protected override void OnExecStart()
        {
            var param_gate = GetArgumentValue(Argument.Gate.ToString()) as string;
            var param_file_path = GetArgumentValue(Argument.LogPath.ToString()) as string;
            var param_data_type = (ArgumentDataType)GetArgumentValue(Argument.DataType.ToString());

            /* 送信ログファイル取得 */
            var reader_info = FormUiManager.CreatePacketLogReader(param_file_path);
            
            reader_ = reader_info.reader as PacketLogReader;

            if (reader_ == null) {
                SetResult(ActionResultType.Error_FileOpen, null);
                return;
            }

            reader_option_ = reader_info.option;
            reader_paths_ = new Queue<string>(reader_info.paths);
            packet_data_type_ = param_data_type;

            /* 送信先ゲート取得 */
            gates_ = GateManager.FindGateObjectFromWildcardAlias(param_gate);

            /* 送信タスク開始 */
            ar_task_ = (new ExecTaskDelegate(ExecTask)).BeginInvoke(null, null);
        }

        protected override void OnExecPoll()
        {
            if (!ar_task_.IsCompleted)return;

            SetResult(ActionResultType.Success, null);
        }

        protected override void OnExecComplete()
        {
            exit_req_ = true;

            if (ar_task_ != null) {
                while (!ar_task_.IsCompleted) {
                    System.Threading.Thread.Sleep(1);
                }
            }
        }

        private delegate void ExecTaskDelegate();
        private void ExecTask()
        {
            while ((!exit_req_) && (reader_paths_.Count > 0)) {
                PlayRecord(reader_, reader_option_, reader_paths_.Dequeue());
            }
        }

        private void PlayRecord(PacketLogReader reader, FileFormatOption option, string path)
        {
            if (!reader.Open(option, path))return;

            ProgressMax = (uint)reader.ProgressMax;
            ProgressNow = (uint)reader.ProgressNow;

            var packet_busy = LoadPlayPacket(reader, packet_data_type_);
            var packet_next = (DataPacketObject)null;
            var delay_timer = new System.Diagnostics.Stopwatch();
            var delay_value = 0;

            while ((!exit_req_) && (packet_busy != null)) {
                /* 次のデータ送信までの遅延 */
                while ((delay_timer.IsRunning) && (delay_timer.ElapsedMilliseconds < delay_value)) {
                    if (delay_timer.ElapsedMilliseconds > 10) {
                        System.Threading.Thread.Sleep(1);
                    }
                }

                /* パケット送信 */
                delay_timer.Restart();
                foreach (var gate in gates_) {
                    gate.SendRequest(packet_busy.GetData());
                }

                /* 次のパケットを取得 */
                packet_next = LoadPlayPacket(reader, packet_data_type_);
                if (packet_next != null) {
                    delay_value = (int)(packet_next.MakeTime - packet_busy.MakeTime).TotalMilliseconds + SEND_TIMMING_MARGIN;
                }
                packet_busy = packet_next;

                ProgressNow = (uint)reader.ProgressNow;
            }

            ProgressNow = (uint)reader.ProgressNow;

            reader.Close();
        }

        private DataPacketObject LoadPlayPacket(PacketLogReader reader, ArgumentDataType type)
        {
            var packet = (PacketObject)null;
            var packet_d = (DataPacketObject)null;

            while ((packet = reader.ReadPacket()) != null) {
                if (packet.Attribute != PacketAttribute.Data)continue;

                packet_d = packet as DataPacketObject;

                if (packet_d == null)continue;

                switch (packet_d.Direction) {
                    case PacketDirection.Recv:
                        if (type == ArgumentDataType.SendDataOnly)continue;
                        break;
                    case PacketDirection.Send:
                        if (type == ArgumentDataType.RecvDataOnly)continue;
                        break;
                    default:
                        continue;
                }

                return (packet_d);
            }

            return (null);
        }

#if false
        private delegate PacketLogReader LoadPacketLogReaderDelegate(string path);
        private PacketLogReader LoadPacketLogReader(string path)
        {
            if (FormUiManager.InvokeRequired()) {
                return (FormUiManager.Invoke(new LoadPacketLogReaderDelegate(LoadPacketLogReader), path) as PacketLogReader);
            }

//            var reader = FileManager.PacketOpen.SelectReaderFromPath(path, typeof(IPacketLogReader));

//            if (format == null)return (null);

//            var reader = format.GetReader();
//            var reader_p = reader.reader as PacketLogReader;

//            if (reader_p == null)return (null);

//            if (!reader_p.Open(reader.option, path))return (null);

            return (reader_p);
        }
#endif
    }
}
