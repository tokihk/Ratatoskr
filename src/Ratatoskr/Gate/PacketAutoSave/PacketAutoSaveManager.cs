using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Gate.PacketAutoSave
{
    internal static class PacketAutoSaveManager
    {
        private static PacketAutoSaveObject  output_obj_ = null;
        private static Queue<PacketObject[]> output_packets_ = new Queue<PacketObject[]>();
        private static IAsyncResult          output_task_ = null;
        private static object                output_sync_ = new object();


        private static void Start()
        {
            Stop();
            lock (output_sync_) {
                output_obj_ = LoadOutputModule(ConfigManager.User.Option.AutoSaveTimming.Value);
            }
        }

        private static void Stop()
        {
            lock (output_sync_) {
                if (output_obj_ == null)return;

                output_obj_ = null;
            }
        }

        public static void Poll()
        {
            OutputPoll();
        }

        public static void Setup()
        {
            Start();
        }

        private static PacketAutoSaveObject LoadOutputModule(AutoSaveTimmingType type)
        {
            var obj = (PacketAutoSaveObject)null;

            switch (ConfigManager.User.Option.AutoSaveTimming.Value) {
                case AutoSaveTimmingType.Interval:    obj = new PacketAutoSaveObject_Interval();    break;
                case AutoSaveTimmingType.FileSize:    obj = new PacketAutoSaveObject_FileSize();    break;
                case AutoSaveTimmingType.PacketCount: obj = new PacketAutoSaveObject_PacketCount(); break;
            }

            return (obj);
        }

        public static void Output(IEnumerable<PacketObject> packets)
        {
            if (output_obj_ == null)return;

            lock (output_sync_) {
                output_packets_.Enqueue(packets.ToArray());
            }

            OutputPoll();
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

        private delegate void OutputTaskHandler(PacketAutoSaveObject module, Queue<PacketObject[]> packets_list);
        private static void OutputTask(PacketAutoSaveObject module, Queue<PacketObject[]> packets_list)
        {
            if (module == null)return;
            if (packets_list == null)return;

            while (packets_list.Count > 0) {
                module.Output(packets_list.Dequeue());
            }
        }
    }
}
