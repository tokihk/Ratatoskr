using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.FileFormats;
using Ratatoskr.Gate.AutoTimeStamp;
using Ratatoskr.Gate.PacketAutoSave;
using Ratatoskr.PacketConverters;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.Gate
{
    internal static class GatePacketManager
    {
        public delegate void EventHandler();
        public delegate void PacketEventHandler(IEnumerable<PacketObject> packets);


        private static bool enable_ = true;

        private static PacketContainer packets_ = new PacketContainer();
        private static object          packets_sync_ = new object();

        private static IAsyncResult ar_load_ = null;
        private static IAsyncResult ar_save_ = null;


        public static PacketManager BasePacketManager { get; } = new PacketManager();


        public static event EventHandler       EventPacketCleared = delegate() { };
        public static event PacketEventHandler EventPacketEntried = delegate(IEnumerable<PacketObject> packets) { };


        public static void Startup()
        {
            BasePacketManager.PacketEntry += OnPacketEntry;
        }

        public static void Shutdown()
        {
        }

        public static void Poll()
        {
            AutoTimeStampManager.Poll();

            /* パケット処理 */
            PacketPoll();
        }

        private static void PacketPoll()
        {
            if (!enable_)return;
            if ((ar_load_ != null) && (!ar_load_.IsCompleted))return;
            if ((ar_save_ != null) && (!ar_save_.IsCompleted))return;

            /* 溜まっている全イベントパケットを取得 */
            AddPacket(BasePacketManager.DequeueAll());
        }

        private static void OnPacketEntry(PacketObject packet, ref bool ignore)
        {
            AutoTimeStampManager.OnEntryPacket(packet);
        }

        public static bool Enable
        {
            get { return (enable_); }
            set { enable_ = value;  }
        }

        public static ulong EventPacketCount
        {
            get { return (packets_.Count); }
        }

        public static IEnumerable<PacketObject> GetEventPackets()
        {
            return (packets_);
        }

        public static void ClearPacket()
        {
            AutoTimeStampManager.OnClearPacket();

            /* コンテナ初期化 */
            lock (packets_sync_) {
                packets_ = new PacketContainer();
            }

            BasePacketManager.Clear();

            /* イベント通知 */
            EventPacketCleared();

            /* UIパケットをクリア */
            FormTaskManager.DrawPacketClear();
        }

        private static void AddPacket(IEnumerable<PacketObject> packets)
        {
            if (packets == null)return;

            /* パケット一覧に追加 */
            packets_.Add(packets);

            /* 自動保存モジュールに転送 */
            PacketAutoSaveManager.Output(packets);

            /* 描画実行 */
            FormTaskManager.DrawPacketPush(packets);

            /* イベント通知 */
            EventPacketEntried(packets);
        }

        public static void SetTimeStamp()
        {
            var packet = new MessagePacketObject(
                                PacketFacility.System,
                                "",
                                PacketPriority.Notice,
                                DateTime.UtcNow,
                                "TimeStamp",
                                0x00,
                                "--------------------------------------------");
           
            BasePacketManager.Enqueue(packet);
        }

        public static void SetComment(string text)
        {
            var packet = new MessagePacketObject(
                                PacketFacility.System,
                                "",
                                PacketPriority.Notice,
                                DateTime.UtcNow,
                                "Comment",
                                0x00,
                                text);
           
            BasePacketManager.Enqueue(packet);
        }

        public static bool IsLoadBusy
        {
            get { return ((ar_load_ != null) && (!ar_load_.IsCompleted)); }
        }

        public static bool IsSaveBusy
        {
            get { return ((ar_save_ != null) && (!ar_save_.IsCompleted)); }
        }

        public static void LoadPacketFile(IEnumerable<string> paths, FileFormatReader reader, FileFormatOption option)
        {
            if (paths == null)return;
            if (reader == null)return;

            if (IsLoadBusy)return;
            if (IsSaveBusy)return;

            /* パケットクリア */
            ClearPacket();

            /* 読込処理を開始 */
            ar_load_ = (new LoadPacketFilesTaskDelegate(LoadPacketFilesTask)).BeginInvoke(paths, reader, option, null, null);
        }

        private delegate void LoadPacketFilesTaskDelegate(IEnumerable<string> paths, FileFormatReader reader, FileFormatOption option);
        private static void LoadPacketFilesTask(IEnumerable<string> paths, FileFormatReader reader, FileFormatOption option)
        {
            var packets_new = new PacketContainer();

            foreach (var path in paths.Select((value, index) => new { value, index })) {
                /* ステータステキストを更新 */
                FormUiManager.SetStatusText(
                    StatusTextId.SaveLoadEventFile,
                    String.Format(
                        "{0} {1} / {2}",
                        ConfigManager.Language.MainMessage.EventFileLoading.Value,
                        path.index + 1,
                        paths.Count()));
                
                /* プログレスバーを初期化 */
                FormUiManager.SetProgressBar(0, false);

                /* ファイルを1つずつ処理 */
                var task = (new LoadPacketFileExecTaskDelegate(LoadPacketFileExecTask)).BeginInvoke(
                                packets_new, path.value, reader, option, null, null);

                /* 完了待ち */
                while (!task.IsCompleted) {
                    System.Threading.Thread.Sleep(100);
                    FormUiManager.SetProgressBar((byte)reader.Progress, false);
                }
            }

            /* コンテナ差し替え */
            packets_ = packets_new;

            /* ステータスバーを終了 */
            FormUiManager.SetStatusText(StatusTextId.SaveLoadEventFile, ConfigManager.Language.MainMessage.EventFileLoadComplete.Value);
            FormUiManager.SetProgressBar(100, true);

            /* 再描画 */
            FormTaskManager.RedrawPacketRequest();
        }

        private delegate void LoadPacketFileExecTaskDelegate(PacketContainer packets, string path, FileFormatReader reader, FileFormatOption option);
        private static void LoadPacketFileExecTask(PacketContainer packets, string path, FileFormatReader reader, FileFormatOption option)
        {
            reader.Read(packets, option, path);
        }

        public static void SavePacketFile(string path, FileFormatWriter writer, FileFormatOption option, IEnumerable<PacketConverterInstance> pcvt_list)
        {
            if (IsLoadBusy)return;
            if (IsSaveBusy)return;

            /* タスク開始 */
            ar_save_ = (new SavePacketFileTaskDelegate(SavePacketFileTask)).BeginInvoke(path, writer, option, pcvt_list, null, null);
        }

        private delegate void SavePacketFileTaskDelegate(string path, FileFormatWriter writer, FileFormatOption option, IEnumerable<PacketConverterInstance> pcvt_list);
        private static void SavePacketFileTask(string path, FileFormatWriter writer, FileFormatOption option, IEnumerable<PacketConverterInstance> pcvt_list)
        {
            var progress_max = Math.Max(packets_.Count, 1);

            /* ステータスバーを初期化 */
            FormUiManager.SetStatusText(StatusTextId.SaveLoadEventFile, ConfigManager.Language.MainMessage.EventFileSaving.Value);
            FormUiManager.SetProgressBar(0, true);

            /* ファイル初期化 */
            writer.Write(null, null, path, false);

            var task_method = new SavePacketFileExecTaskDelegate(SavePacketFileExecTask);
            var task_result = (IAsyncResult)null;

            /* --- フィルタ適用有り --- */
            if (pcvt_list != null) {
                var count = (ulong)0;

                /* 変換器リセット */
                PacketConverterManager.InputStatusClear(pcvt_list);

                var task_packets = (IEnumerable<PacketObject>)null;

                foreach (var packet in packets_) {
                    /* パケット変換 */
                    task_packets = PacketConverterManager.InputPacket(pcvt_list, packet);

                    /* 直前の出力の完了待ち */
                    if (task_result != null) {
                        task_method.EndInvoke(task_result);
                    }

                    /* ファイル出力 */
                    task_result = task_method.BeginInvoke(task_packets, path, writer, option, null, null);

                    /* プログレスバー更新 */
                    FormUiManager.SetProgressBar((byte)((double)(++count) / packets_.Count * 100), false);
                }

                /* 変換済みパケットを出力 */
                if (task_packets != null) {
                    task_result = task_method.BeginInvoke(task_packets, path, writer, option, null, null);
                }

                /* 変換器内の残りパケットを処理 */
                task_packets = PacketConverterManager.InputBreakOff(pcvt_list);
                if (task_packets != null) {
                    /* 書込みタスクの完了待ち */
                    if (task_result != null) {
                        task_method.EndInvoke(task_result);
                    }

                    /* 残りパケットを出力 */
                    task_result = task_method.BeginInvoke(task_packets, path, writer, option, null, null);
                }

                /* 書込みタスクの完了待ち */
                if (task_result != null) {
                    task_method.EndInvoke(task_result);
                }

            } else {
                /* --- フィルタ適用無し --- */
                task_result = task_method.BeginInvoke(packets_, path, writer, option, null, null);
                
                /* 完了待ち */
                while (!task_result.IsCompleted) {
                    System.Threading.Thread.Sleep(100);
                    FormUiManager.SetProgressBar((byte)writer.Progress, false);
                }
            }

            /* ステータスバーを終了 */
            FormUiManager.SetStatusText(StatusTextId.SaveLoadEventFile, ConfigManager.Language.MainMessage.EventFileSaveComplete.Value);
            FormUiManager.SetProgressBar(100, true);
        }

        private delegate void SavePacketFileExecTaskDelegate(IEnumerable<PacketObject> packets, string path, FileFormatWriter writer, FileFormatOption option);
        private static void SavePacketFileExecTask(IEnumerable<PacketObject> packets, string path, FileFormatWriter writer, FileFormatOption option)
        {
            writer.Write(packets, option, path, true);
        }
    }
}
