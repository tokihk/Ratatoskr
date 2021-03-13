using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;
using Ratatoskr.Forms;
using Ratatoskr.Config.Data.System;
using Ratatoskr.General.Packet;

namespace Ratatoskr.Gate.AutoLogger
{
    internal static class AutoLogManager
    {
        private static AutoLogObject         output_obj_ = null;
        private static Queue<PacketObject[]> output_packets_ = new Queue<PacketObject[]>();
        private static IAsyncResult          output_task_ = null;
        private static object                output_sync_ = new object();

        private static AutoPacketSaveTargetType  target_type_ = AutoPacketSaveTargetType.DevicePacket;
        private static AutoPacketSaveTimmingType timming_type_ = AutoPacketSaveTimmingType.NoSave;


        public static void Poll()
        {
            UpdatePoll();
            OutputPoll();
        }

        private static bool RunRequest
        {
            get { return (timming_type_ != AutoPacketSaveTimmingType.NoSave); }
        }

        private static void UpdatePoll()
        {
            if (   (timming_type_ != ConfigManager.System.AutoPacketSave.SaveTimming.Value)
                || (target_type_ != ConfigManager.System.AutoPacketSave.SaveTarget.Value)
            ) {
                timming_type_ = ConfigManager.System.AutoPacketSave.SaveTimming.Value;
                target_type_ = ConfigManager.System.AutoPacketSave.SaveTarget.Value;

                Restart();
            }
        }

        private static void RegisterWatchEvent()
        {
            if (timming_type_ != AutoPacketSaveTimmingType.NoSave) {
                switch (target_type_) {
                    case AutoPacketSaveTargetType.DevicePacket:
                        GatePacketManager.RawPacketEntried += OnPacketEntried;
                        break;
                    case AutoPacketSaveTargetType.ViewPacket:
                        FormTaskManager.DrawPacketEntried += OnPacketEntried;
                        break;
                }
            }
        }

        private static void UnregisterWatchEvent()
        {
            GatePacketManager.RawPacketEntried -= OnPacketEntried;
            FormTaskManager.DrawPacketEntried -= OnPacketEntried;
        }

        private static void Restart()
        {
            Stop();
            lock (output_sync_) {
                output_obj_ = LoadOutputModule(ConfigManager.System.AutoPacketSave.SaveTimming.Value);

                RegisterWatchEvent();
            }
        }

        private static void Stop()
        {
            lock (output_sync_) {
                output_obj_ = null;

                UnregisterWatchEvent();
            }
        }

        private static AutoLogObject LoadOutputModule(AutoPacketSaveTimmingType type)
        {
            var obj = (AutoLogObject)null;

            switch (timming_type_) {
                case AutoPacketSaveTimmingType.Interval:    obj = new AutoLogObject_Interval();    break;
                case AutoPacketSaveTimmingType.FileSize:    obj = new AutoLogObject_FileSize();    break;
                case AutoPacketSaveTimmingType.PacketCount: obj = new AutoLogObject_PacketCount(); break;
            }

            return (obj);
        }

        private static void AddPackets(IEnumerable<PacketObject> packets)
        {
            if (output_obj_ == null)return;

            lock (output_sync_) {
                output_packets_.Enqueue(packets.ToArray());
            }
        }

        private static void OutputPoll()
        {
            /* 出力モジュールが設定されていない */
            if (output_obj_ == null)return;

            /* 出力処理中 */
            if ((output_task_ != null) && (!output_task_.IsCompleted))return;

            /* 出力パケットが存在しない */
            if (output_packets_.Count == 0)return;

            var output_packets_new = new Queue<PacketObject[]>();
            var output_packets_now = (Queue<PacketObject[]>)null;

            lock (output_sync_) {
                output_packets_now = output_packets_;
                output_packets_ = output_packets_new;
            }

            output_task_ = (new OutputTaskHandler(OutputTask)).BeginInvoke(output_obj_, output_packets_now, null, null);
        }

        private delegate void OutputTaskHandler(AutoLogObject module, Queue<PacketObject[]> packets_list);
        private static void OutputTask(AutoLogObject module, Queue<PacketObject[]> packets_list)
        {
            if (module == null)return;
            if (packets_list == null)return;

            while (packets_list.Count > 0) {
                module.Output(packets_list.Dequeue());
            }
        }

        private static void OnPacketEntried(IEnumerable<PacketObject> packets)
        {
            AddPackets(packets);
        }
    }
}
