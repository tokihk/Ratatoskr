using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.PacketViews
{
    internal sealed class ViewManager
    {
        private const int DRAW_IVAL_MIN  = 10;
        private const int DRAW_IVAL_MAX  = 600;
        private const int DRAW_IVAL_STEP = 20;

        private const int DRAW_BUSY_LIMIT = 50;


        public delegate void InstanceUpdatedDelegate();
        public delegate void RedrawRequestDelegate();
        public delegate void StatusUpdatedDelegate();

        public event InstanceUpdatedDelegate InstanceUpdated = delegate() { };
        public event RedrawRequestDelegate   RedrawRequest   = delegate() { };
        public event StatusUpdatedDelegate   StatusUpdated   = delegate() { };


        private List<ViewClass>   viewd_list_ = new List<ViewClass>();
        private List<ViewControl> viewc_list_ = new List<ViewControl>();

        private Stopwatch draw_timer_ = new Stopwatch();
        private int       draw_timer_ival_ = DRAW_IVAL_MIN;
        private object    draw_sync_ = new object();

        private Queue<IEnumerable<PacketObject>> draw_packets_ = new Queue<IEnumerable<PacketObject>>();
        private ulong                            draw_packets_count_ = 0;
        private object                           draw_packets_sync_ = new object();

        private bool hispeed_mode_ = false;
        private bool hispeed_auto_stop_ = false;

        private PacketManager pktm_;


        public bool AutoScroll { get; set; } = false;


        public ViewManager(PacketManager pktm)
        {
            pktm_ = pktm;

            draw_timer_.Start();
        }

        public ulong DrawPacketCount
        {
            get {
                lock (draw_packets_) {
                    return (draw_packets_count_);
                }
            }
        }

        public IEnumerable<ViewClass> GetClasses()
        {
            lock (viewd_list_) {
                return (viewd_list_.ToArray());
            }
        }

        private ViewClass FindClass(Guid class_id)
        {
            lock (viewd_list_) {
                return (viewd_list_.Find(viewd => viewd.ID == class_id));
            }
        }

        public bool AddView(ViewClass viewd)
        {
            if (viewd == null)return (false);

            /* 重複IDをチェック */
            if (FindClass(viewd.ID) != null)return (false);

            /* 新しいデバイスを追加 */
            lock (viewd_list_) {
                viewd_list_.Add(viewd);
            }

            return (true);
        }

        public IEnumerable<ViewControl> GetInstances()
        {
            var devi_list_all = new List<ViewControl>();

            lock (viewc_list_) {
                devi_list_all.AddRange(viewc_list_);
            }

            return (devi_list_all.ToArray());
        }

        public ViewControl CreateControl(Guid class_id, Guid obj_id, ViewProperty viewp)
        {
            /* ビューIDからデバイスを検索 */
            var viewd = FindClass(class_id);

            if (viewd == null)return (null);

            /* デバイスインスタンス作成 */
            var viewi = viewd.CreateInstance(this, obj_id, viewp);

            if (viewi == null)return (null);

            var viewc = new ViewControl(this, viewi);

            /* デバイスインスタンス登録 */
            lock (viewc_list_) {
                viewc_list_.Add(viewc);
            }

            /* 初期化完了 */
            viewi.InitializeComplete = true;

            /* 初回の設定値バックアップ */
            viewi.BackupProperty();

            InstanceUpdated();

            return (viewc);
        }

        public ViewControl CreateControl(string class_id, Guid obj_id, ViewProperty viewp)
        {
            var id = Guid.Empty;

            if (!Guid.TryParse(class_id, out id))return (null);

            return (CreateControl(id, obj_id, viewp));
        }

        public void RemoveInstance(ViewControl viewi)
        {
            if (viewi == null)return;

            lock (viewc_list_) {
                viewc_list_.Remove(viewi);
            }

            InstanceUpdated();
        }

        public ViewProperty CreateProperty(Guid class_id)
        {
            /* クラスIDからクラスを検索 */
            var viewd = FindClass(class_id);

            if (viewd == null)return (null);

            return (viewd.CreateProperty());
        }

        internal void SetupPacket(PacketObject packet)
        {
            pktm_.Enqueue(packet);
        }

        public void ClearPacket()
        {
            /* 描画パケットをクリア */
            lock (draw_packets_sync_) {
                draw_packets_ = new Queue<IEnumerable<PacketObject>>();
                draw_packets_count_ = 0;
            }

            /* ビュークリア */
            lock (viewc_list_) {
                viewc_list_.ForEach(viewi => viewi.ClearPacket());
            }
        }

        public void RedrawPacket()
        {
            /* 再描画要求 */
            RedrawRequest();
        }

        public bool IsHighSpeedDraw
        {
            get { return (hispeed_mode_); }
        }

        public void HiSpeedDrawStart(bool auto_stop)
        {
            if (hispeed_mode_)return;

            /* 全てのビューの表示処理を停止 */
            lock (viewc_list_) {
                viewc_list_.ForEach(viewi => viewi.Hide());
            }

            hispeed_mode_ = true;
            hispeed_auto_stop_ = auto_stop;

            draw_timer_ival_ = DRAW_IVAL_MAX;

            StatusUpdated();
        }

        public void HiSpeedDrawStop()
        {
            if (!hispeed_mode_)return;

            /* 全てのビューの表示処理を再開 */
            lock (viewc_list_) {
                viewc_list_.ForEach(viewi => viewi.Show());
            }

            draw_timer_ival_ = DRAW_IVAL_MIN;

            hispeed_mode_ = false;
            hispeed_auto_stop_ = false;

            StatusUpdated();
        }

        private void PushDrawPackets(IEnumerable<PacketObject> packets)
        {
            lock (draw_packets_sync_) {
                draw_packets_.Enqueue(packets);
                draw_packets_count_ += (ulong)packets.Count();
            }
        }

        private IEnumerable<PacketObject> PopDrawPackets()
        {
            if (draw_packets_.Count == 0)return (null);

            lock (draw_packets_sync_) {
                var packets = draw_packets_.Dequeue();

                draw_packets_count_ -= (ulong)packets.Count();

                return (packets);
            }
        }

        public void DrawPacket(PacketObject packet)
        {
            DrawPacket(new []{ packet });
        }

        public void DrawPacket(IEnumerable<PacketObject> packets)
        {
            if (packets == null)return;
            if (packets.Count() == 0)return;

            PushDrawPackets(packets);
        }

        public void Poll()
        {
            if (hispeed_mode_) {
                /* 待ち時間無しで描画処理 */
                if (!DrawExec()) {
                    if (hispeed_auto_stop_) {
                        HiSpeedDrawStop();
                    }
                }

            } else {
                if (draw_timer_.ElapsedMilliseconds < draw_timer_ival_) {
                    /* 再描画中でなければ表示間隔に従う */
                    return;
                }

                if (DrawExec()) {
                    /* 描画間隔拡張 */
                    draw_timer_ival_ += DRAW_IVAL_STEP;

                } else {
                    /* 描画間隔短縮 */
                    draw_timer_ival_ = 0;
                }

                /* 描画間隔補正 */
                draw_timer_ival_ = Math.Min(draw_timer_ival_, DRAW_IVAL_MAX);
                draw_timer_ival_ = Math.Max(draw_timer_ival_, DRAW_IVAL_MIN);

                /* 描画タイマーリセット */
                draw_timer_.Restart();
            }
        }

        private bool DrawExec()
        {
            if (draw_packets_.Count == 0)return (false);

            var draw_busy_timer = new Stopwatch();
            var draw_packets = (IEnumerable<PacketObject>)null;

            /* 描画開始 */
            lock (draw_sync_) {
                viewc_list_.ForEach(viewi => viewi.BeginDrawPacket(AutoScroll));

                /* 描画時間の測定開始 */
                draw_busy_timer.Restart();

                while ((draw_packets = PopDrawPackets()) != null) {
                    /* 描画実行 */
                    viewc_list_.ForEach(viewi => viewi.DrawPacket(draw_packets));

                    /* 描画処理のタイムアウト */
                    if (draw_busy_timer.ElapsedMilliseconds > (draw_timer_ival_ * 2 / 3)) {
                        break;
                    }
                }

                /* 描画終了 */
                viewc_list_.ForEach(viewi => viewi.EndDrawPacket(AutoScroll));
            }

            return (true);
        }
    }
}
