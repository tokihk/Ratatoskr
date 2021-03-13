using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using RtsCore.Packet;
using System.Windows.Forms;

namespace RtsCore.Device
{
    public static class DeviceManager
    {
        private const int DEVICE_TIMER_IVAL_BASE    = 100;
        private const int DEVICE_TIMER_CNTR_1000MS  = 1000 / DEVICE_TIMER_IVAL_BASE;


        private static bool disposed_ = false;

        private static List<DeviceClass>    devd_list_ = new List<DeviceClass>();
        private static List<DeviceInstance> devi_list_ = new List<DeviceInstance>();

        private static PacketManager pktm_;

        private static System.Timers.Timer         dev_timer_base_;
        private static int                         dev_timer_cntr_1000ms_ = 0;

        public static AutoResetEvent  WaitHandle_1000ms { get; private set; }  = new AutoResetEvent(false);


        static DeviceManager()
        {
            dev_timer_base_ = new System.Timers.Timer();
            dev_timer_base_.AutoReset = true;
            dev_timer_base_.Interval = DEVICE_TIMER_IVAL_BASE;
            dev_timer_base_.Elapsed += OnDeviceBaseTimer;
            dev_timer_base_.Start();
        }

		public static void Startup()
        {
			PacketManager = GatePacketManager.BasePacketManager;
        }

        public static void Shutdown()
        {
            RemoveAllInstance();
        }

        public static PacketManager PacketManager { get; set; }

        private static void OnDeviceBaseTimer(Object sender, ElapsedEventArgs e)
        {
            dev_timer_cntr_1000ms_++;
            if (dev_timer_cntr_1000ms_ >= DEVICE_TIMER_CNTR_1000MS) {
                WaitHandle_1000ms.Set();
                dev_timer_cntr_1000ms_ = 0;
            }
        }

        public static IEnumerable<DeviceClass> GetDeviceList()
        {
            lock (devd_list_) {
                return (devd_list_.ToArray());
            }
        }

        public static DeviceClass FindClass(Guid class_id)
        {
            lock (devd_list_) {
                return (devd_list_.Find(devd => devd.ID == class_id));
            }
        }

        public static bool AddDevice(DeviceClass devd)
        {
            if (devd == null)return (false);

            /* 重複IDをチェック */
            if (FindClass(devd.ID) != null)return (false);

            /* 新しいデバイスを追加 */
            lock (devd_list_) {
                devd_list_.Add(devd);

                /* 名前順にソート */
                devd_list_.Sort((a, b) => a.Name.CompareTo(b.Name));
            }

            return (true);
        }

        public static void RemoveAllInstance()
        {
            foreach (var devi in GetInstances()) {
                devi.DeviceShutdownRequest();
            }
        }

        public static DeviceInstance[] GetInstances()
        {
            lock (devi_list_) {
                return (devi_list_.ToArray());
            }
        }

        public static DeviceInstance CreateInstance(DeviceConfig devconf, Guid class_id, DeviceProperty devp)
        {
            /* クラスIDからクラスを検索 */
            var devd = FindClass(class_id);

            if (devd == null)return (null);

            /* インスタンス作成 */
            var devi = devd.CreateInstance(devconf, devp);

            if (devi == null)return (null);

            /* インスタンス登録 */
            lock (devi_list_) {
                devi_list_.Add(devi);

                /* 各インスタンスのリダイレクト先を更新 */
                UpdateRedirectMap();
            }

            /* デバイス処理開始 */
            devi.DeviceStart();

            return (devi);
        }

        public static DeviceProperty CreateProperty(Guid class_id)
        {
            /* クラスIDからクラスを検索 */
            var devd = FindClass(class_id);

            if (devd == null)return (null);

            return (devd.CreateProperty());
        }

        internal static void SetupPacket(PacketObject packet)
        {
            PacketManager.Enqueue(packet);
        }

        internal static void UpdateRedirectMap()
        {
            foreach (var devi in devi_list_) {
                devi.UpdateRedirectMap(devi_list_);
            }
        }

        public static void Poll()
        {
            lock (devi_list_) {
                /* 終了済みインスタンスを破棄 */
                if (devi_list_.RemoveAll(devi => devi.IsShutdown) > 0) {
                    /* 各インスタンスのリダイレクト先を更新 */
                    UpdateRedirectMap();
                }
            }
        }

        public static DeviceInstance CreateDeviceObject(DeviceConfig devconf, Guid class_id, DeviceProperty devp)
        {
            return (CreateInstance(devconf, class_id, devp));
        }

        public static DeviceProperty CreateDeviceProperty(Guid class_id)
        {
            return (CreateProperty(class_id));
        }
    }
}
