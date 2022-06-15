using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Debugger;
using Ratatoskr.Gate;
using Ratatoskr.PacketConverter;
using Ratatoskr.PacketView;
using Ratatoskr.General.Packet;

namespace Ratatoskr.Forms
{
    internal static class FormTaskManager
    {
        private const int PACKET_CONVERTER_LIMIT = 10;
        private const int PACKET_VIEW_LIMIT = 5;

//        private const int PACKET_BLOCK_CAPACITY = 500;
//        private const int PACKET_BLOCK_CAPACITY = 2000;
//        private const int PACKET_BLOCK_CAPACITY = 4000;
//        private const int PACKET_BLOCK_CAPACITY = 8000;
//        private const int PACKET_BLOCK_CAPACITY = 10000;
        private const int PACKET_BLOCK_CAPACITY = 12000;
//        private const int PACKET_BLOCK_CAPACITY = 15000;

        private enum RedrawSequence
        {
            DrawingCancelStart,
            DrawingCancelBusy,
            Ready,
            PreprocessingStart,
            PreprocessingBusy,
            DrawingStart,
            DrawingBusy,
            Complete,
        }


        private static ulong  packet_count_all_ = 0;
        private static ulong  packet_count_draw_ = 0;

        private static Queue<IEnumerable<PacketObject>> draw_packets_ = null;
        private static object                           draw_packets_sync_ = new object();
        private static IAsyncResult                     draw_packet_ar_ = null;

        private static bool           redraw_req_ = false;
        private static bool           redraw_state_ = false;
        private static RedrawSequence redraw_seq_ = (RedrawSequence)0;
        private static ulong          redraw_step_all_ = 0;
        private static ulong          redraw_step_end_ = 0;
        private static byte           redraw_progress_ = 0;
        

        public delegate void EventHandler();
        public delegate void PacketEventHandler(IEnumerable<PacketObject> packets);

        public static event EventHandler       DrawPacketCleared = delegate() { };
        public static event PacketEventHandler DrawPacketEntried;
        public static event PacketEventHandler DrawPacketScanning;


        public static void Startup()
        {
            /* パケットビューイベントに登録 */
            PacketViewManager.Instance.InstanceUpdated += OnViewInstanceUpdated;
            PacketViewManager.Instance.RedrawRequest += OnViewRedrawRequest;
            PacketViewManager.Instance.StatusUpdated += OnViewStatusUpdated;

            /* パケットコンバーターマネージャー作成 */
            PacketConvertManager.Instance.ConvertStatusUpdated += OnUpdateConverter;

            /* パケット初期化 */
            draw_packets_ = new Queue<IEnumerable<PacketObject>>();
        }

        public static void Shutdown()
        {
        }

        public static void Poll()
        {
            DrawPacketPoll();
            RedrawPacketPoll();

            /* 常にパケット数を最新に更新 */
            UpdatePacketCount();

            /* 自動スクロール設定を更新 */
            PacketViewManager.Instance.AutoScroll = ConfigManager.System.AutoScroll.Value;

            /* 描画待ちパケットが一定以上になったら強制的に高速描画モードに移行 */
            if (PacketViewManager.Instance.DrawPacketCount >= ConfigManager.System.ApplicationCore.AutoHighSpeedDrawLimit.Value) {
                PacketViewManager.Instance.HiSpeedDrawStart(true);
            }

            /* パケットビュータスク */
            PacketViewManager.Instance.Poll();
        }

        private static void OnViewInstanceUpdated()
        {
            FormUiManager.MainFrameMenuBarUpdate();
        }

        private static void OnViewRedrawRequest()
        {
            RedrawPacketRequest();
        }

        private static void OnViewStatusUpdated()
        {
            FormUiManager.MainFrameStatusBarUpdate();
        }

        private static void OnUpdateConverter()
        {
            /* メニューを更新 */
            FormUiManager.MainFrameMenuBarUpdate();

            /* 変換器が更新されたので再描画 */
            RedrawPacketRequest();
        }

        public static IEnumerable<PacketViewClass> GetPacketViewClasses()
        {
            return (PacketViewManager.Instance.GetClassList());
        }

        public static IEnumerable<PacketViewControl> GetPacketViewControls()
        {
            return (PacketViewManager.Instance.GetInstanceList());
        }

        public static bool CanAddPacketView
        {
            get { return (PacketViewManager.Instance.GetInstanceList().Count() < PACKET_VIEW_LIMIT); }
        }

        public static PacketViewControl CreatePacketView(Guid class_id, Guid obj_id, PacketViewProperty viewp)
        {
            return (PacketViewManager.Instance.CreateControl(class_id, obj_id, viewp));
        }

        public static PacketViewControl CreatePacketView(Guid class_id, PacketViewProperty viewp)
        {
            return (CreatePacketView(class_id, Guid.NewGuid(), viewp));
        }

        public static void RemovePacketView(PacketViewControl viewi)
        {
            PacketViewManager.Instance.RemoveInstance(viewi);
        }

        public static void ClearPacketViewScreen()
        {
            PacketViewManager.Instance.ClearPacket();
        }

        public static PacketViewProperty CreatePacketPacketViewProperty(Guid class_id)
        {
            return (PacketViewManager.Instance.CreateProperty(class_id));
        }

        public static IEnumerable<PacketConverterClass> GetPacketConverterClasses()
        {
            return (PacketConvertManager.Instance.GetClassList());
        }

        public static IEnumerable<PacketConverterInstance> GetPacketConverterInstances()
        {
            return (PacketConvertManager.Instance.GetInstanceList());
        }

        public static bool CanAddPacketConverter
        {
            get { return (PacketConvertManager.Instance.GetInstanceList().Count() < PACKET_CONVERTER_LIMIT); }
        }

        public static IEnumerable<PacketConverterInstance> GetPacketConverterClone()
        {
            return (PacketConvertManager.Instance.GetCloneInstances());
        }

        public static PacketConverterInstance CreatePacketConverter(Guid class_id, PacketConverterProperty pcvtp)
        {
            return (PacketConvertManager.Instance.CreateInstance(class_id, Guid.NewGuid(), pcvtp));
        }

        public static void RemovePacketConverter(PacketConverterInstance pcvti)
        {
            PacketConvertManager.Instance.RemoveInstance(pcvti);
        }

        public static void SetPacketConverterIndex(PacketConverterInstance pcvti, int index)
        {
            PacketConvertManager.Instance.SetInstanceIndex(pcvti, index);
        }

        public static PacketConverterProperty CreatePacketConverterProperty(Guid class_id)
        {
            return (PacketConvertManager.Instance.CreateProperty(class_id));
        }

        public static bool IsHispeedDraw
        {
            get { return (PacketViewManager.Instance.IsHighSpeedDraw); }
        }

        public static void HiSpeedDrawToggle()
        {
            if (PacketViewManager.Instance.IsHighSpeedDraw) {
                PacketViewManager.Instance.HiSpeedDrawStop();
            } else {
                PacketViewManager.Instance.HiSpeedDrawStart(true);
            }
        }

        public static bool IsRedrawBusy
        {
            get { return (redraw_state_); }
        }

        public static void RedrawPacketRequest()
        {
            redraw_req_ = true;
            redraw_state_ = true;
            redraw_seq_ = (RedrawSequence)0;
        }

        private static void RedrawPacketPoll()
        {
            if (!redraw_state_)return;

            switch (redraw_seq_) {
                case RedrawSequence.DrawingCancelStart:
                {
                    /* イベント処理を一時停止 */
                    GatePacketManager.Enable = false;

                    redraw_seq_++;
                }
                    break;

                case RedrawSequence.DrawingCancelBusy:
                {
                    if (!IsDrawPreprocessBusy) {
                        /* 現在のパケットを全てクリア */
                        DrawPacketClear();

                        /* 描画処理開始 */
                        redraw_req_ = false;

                        redraw_seq_++;
                    }
                }
                    break;

                case RedrawSequence.Ready:
                {
                    DebugManager.MessageOut(DebugEventSender.Form, "RedrawSequence.Ready");

                    /* 高速描画モード開始 */
                    PacketViewManager.Instance.HiSpeedDrawStart(false);

                    /* 現在のパケットを全て描画パケットにセットアップ */
                    DrawPacketPush(GatePacketManager.GetPackets());

                    redraw_seq_++;
                }
                    break;

                case RedrawSequence.PreprocessingStart:
                {
                    DebugManager.MessageOut(DebugEventSender.Form, "RedrawSequence.PreprocessingStart");

                    redraw_step_all_ = (ulong)(Math.Max(draw_packets_.Count, 1));
                    redraw_step_end_ = 0;
                    redraw_progress_ = 0;

                    redraw_seq_++;

                    /* プログレスバーを初期化 */
                    FormUiManager.SetStatusText(StatusTextID.ReloadScreen, ConfigManager.Language.MainMessage.PacketNowPreprocessing.Value);
                    FormUiManager.SetProgressBar(redraw_progress_, false);
                }
                    break;

                case RedrawSequence.PreprocessingBusy:
                {
                    redraw_step_end_ = Math.Min(redraw_step_all_ - (ulong)draw_packets_.Count, redraw_step_all_);
                    redraw_step_end_ = Math.Max(redraw_step_end_, 1);
                    redraw_progress_ = Math.Min((byte)(redraw_step_end_ * 100 / redraw_step_all_), (byte)100);

                    if ((redraw_progress_ == 100) && (!IsDrawPreprocessBusy)) {
                        redraw_seq_++;
                    }

                    /* プログレスバーに反映 */
                    FormUiManager.SetProgressBar(redraw_progress_, false);
                }
                    break;

                case RedrawSequence.DrawingStart:
                {
                    DebugManager.MessageOut(DebugEventSender.Form, "RedrawSequence.DrawingStart");

                    redraw_step_all_ = (ulong)Math.Max(PacketViewManager.Instance.DrawPacketCount, 1);
                    redraw_step_end_ = 0;
                    redraw_progress_ = 0;

                    redraw_seq_++;

                    /* プログレスバーを初期化 */
                    FormUiManager.SetStatusText(StatusTextID.ReloadScreen, ConfigManager.Language.MainMessage.PacketNowDrawing.Value);
                    FormUiManager.SetProgressBar(0, false);
                }
                    break;

                case RedrawSequence.DrawingBusy:
                {
                    redraw_step_end_ = Math.Min(redraw_step_all_, redraw_step_all_ - (ulong)PacketViewManager.Instance.DrawPacketCount);
                    redraw_step_end_ = Math.Max(redraw_step_end_, 1);
                    redraw_progress_ = Math.Min((byte)(redraw_step_end_ * 100 / redraw_step_all_), (byte)100);

                    if (redraw_progress_ == 100) {
                        redraw_seq_++;
                    }

                    /* プログレスバーに反映 */
                    FormUiManager.SetProgressBar(redraw_progress_, false);
                }
                    break;

                case RedrawSequence.Complete:
                {
                    /* イベント処理再開 */
                    GatePacketManager.Enable = true;

                    /* 高速描画モード終了 */
                    PacketViewManager.Instance.HiSpeedDrawStop();

                    /* プログレスバーを最終値に設定 */
                    FormUiManager.SetStatusText(StatusTextID.ReloadScreen, "");
                    FormUiManager.SetProgressBar(100, true);

                    redraw_state_ = false;

                    DebugManager.MessageOut(DebugEventSender.Form, "RedrawSequence.Complete");
                }
                    break;

                default:
                {
                    redraw_seq_++;
                }
                    break;
            }
        }

        public static void DrawPacketClear()
        {
            if (FormUiManager.InvokeRequired) {
                FormUiManager.Invoke((MethodInvoker)DrawPacketClear);
                return;
            }

            /* 表示処理中パケットをクリア */
            lock (draw_packets_sync_) {
                draw_packets_ = new Queue<IEnumerable<PacketObject>>();
            }

            /* 変換器をクリア */
            PacketConvertManager.Instance.InputStatusClear();

            /* パケットビューをクリア */
            PacketViewManager.Instance.ClearPacket();

            /* パケット数を更新 */
            packet_count_all_ = 0;
            packet_count_draw_ = 0;

            UpdatePacketCount();

            /* イベント通知 */
            DrawPacketCleared();
        }

        public static void DrawPacketPush(IEnumerable<PacketObject> packets)
        {
            lock (draw_packets_sync_) {
                var packets_block = new Queue<PacketObject>();
                var packets_count = (ulong)0;

                /* 入力されたパケットをブロック単位で格納 */
                foreach (var packet in packets) {
                    packets_block.Enqueue(packet);

                    /* ブロックの最大パケット数に達した場合は区切り */
                    if (packets_block.Count >= PACKET_BLOCK_CAPACITY) {
                        draw_packets_.Enqueue(packets_block);
                        packets_count += (ulong)packets_block.Count;
                        packets_block = new Queue<PacketObject>();
                    }
                }

                /* 処理中のブロックを格納 */
                if (packets_block.Count > 0) {
                    packets_count += (ulong)packets_block.Count;
                    draw_packets_.Enqueue(packets_block);
                }

                packet_count_all_ += packets_count;
            }
        }

        private static IEnumerable<PacketObject> DrawPacketPop()
        {
            lock (draw_packets_sync_) {
                if (draw_packets_.Count == 0)return (null);

                return (draw_packets_.Dequeue());
            }
        }

        private static bool IsDrawPreprocessBusy
        {
            get { return ((draw_packet_ar_ != null) && (!draw_packet_ar_.IsCompleted)); }
        }

        private static void DrawPacketPoll()
        {
            /* 描画処理中 or 再描画準備中 */
            if (   (IsDrawPreprocessBusy)
                || (redraw_req_)
            ) {
                return;
            }

            /* 通常発生したパケットを処理 */
            if (DrawNormalPacketPoll())return;

            /* 変換器からのパケットを処理 */
            if (DrawConvertPacketPoll())return;
        }

        private static bool DrawNormalPacketPoll()
        {
            /* 描画パケットを取得 */
            var packets_list = new Queue<IEnumerable<PacketObject>>();
            var packets = (IEnumerable<PacketObject>)null;
            var packet_count = 0;
            
            while ((packets = DrawPacketPop()) != null) {
                packets_list.Enqueue(packets);
                packet_count += packets.Count();

                /* 1ループ当たりの処理パケット数が限界なので収集終了 */
                if (packet_count >= PACKET_BLOCK_CAPACITY) {
                    break;
                }
            }

            if (packets_list.Count == 0)return (false);

            /* 描画実行 */
            draw_packet_ar_ = (new DrawPacketTaskDelegate(DrawPacketTask)).BeginInvoke(packets_list, null, null);

            return (true);
        }

        private static bool DrawConvertPacketPoll()
        {
            /* 変換器が自動生成したパケットを取得 */
            var packets = PacketConvertManager.Instance.InputPoll();

            /* パケットが存在しなければ次の処理に渡す */
            if ((packets == null) || (packets.Count() == 0))return (false);

            /* 変換せずに表示 */
            DrawPacketExec(packets);

            return (true);
        }

        private delegate void DrawPacketTaskDelegate(IEnumerable<IEnumerable<PacketObject>> packets_list);
        private static void DrawPacketTask(IEnumerable<IEnumerable<PacketObject>> packets_list)
        {
            foreach (var packets in packets_list) {
                foreach (var packet in packets) {
                    /* 変換して表示*/
                    DrawPacketExec(PacketConvertManager.Instance.InputPacket(packet));

                    /* キャンセル処理 */
                    if (redraw_req_)return;
                }
            }
        }

        private static void DrawPacketExec(IEnumerable<PacketObject> packets)
        {
            /* 変換後のパケット数を更新 */
            packet_count_draw_ += (ulong)packets.Count();

            /* パケットビューに設定(パケットセットのみ) */
            PacketViewManager.Instance.DrawPacket(packets);

            /* イベント通知 */
            if (!redraw_state_) {
                DrawPacketEntried?.Invoke(packets);
            }
            DrawPacketScanning?.Invoke(packets);
        }

        private static void UpdatePacketCount()
        {
            FormUiManager.SetPacketCounter(GatePacketManager.PacketCount, packet_count_all_, packet_count_draw_, PacketViewManager.Instance.DrawPacketCount);
        }
    }
}
