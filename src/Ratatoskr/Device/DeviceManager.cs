using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using Ratatoskr.Debugger;
using Ratatoskr.Gate;
using Ratatoskr.General.Packet;

namespace Ratatoskr.Device
{
    internal class DeviceManager
    {
		public static DeviceManager Instance { get; private set; }

		public static void Initialize(DeviceManager instance = null)
		{
			if (instance == null) {
				instance = new DeviceManager();
			}
			Instance = instance;
		}


        private const int DEVICE_TIMER_IVAL_BASE    = 100;
        private const int DEVICE_TIMER_CNTR_1000MS  = 1000 / DEVICE_TIMER_IVAL_BASE;


        private static List<DeviceClass>    devd_list_ = new List<DeviceClass>();
        private static List<DeviceInstance> devi_list_ = new List<DeviceInstance>();

        private static System.Timers.Timer         dev_timer_base_;
        private static int                         dev_timer_cntr_1000ms_ = 0;


        private DeviceManager()
        {
			/* 標準デバイスをここに追加する */
            AddDevice(new Null.DeviceClassImpl());

            AddDevice(new SerialPort.DeviceClassImpl());

            AddDevice(new TcpServer.DeviceClassImpl());
            AddDevice(new TcpClient.DeviceClassImpl());
            AddDevice(new UdpClient.DeviceClassImpl());

            AddDevice(new AudioDevice.DeviceClassImpl());
            AddDevice(new AudioFile.DeviceClassImpl());

            AddDevice(new UsbCapture.DeviceClassImpl());
            AddDevice(new Ethernet.DeviceClassImpl());

#if DEBUG
            AddDevice(new UsbCommHost.DeviceClassImpl());
#endif

            dev_timer_base_ = new System.Timers.Timer();
            dev_timer_base_.AutoReset = true;
            dev_timer_base_.Interval = DEVICE_TIMER_IVAL_BASE;
            dev_timer_base_.Elapsed += OnDeviceBaseTimer;
            dev_timer_base_.Start();
        }

		public void Startup()
        {
        }

        public void Shutdown()
        {
            RemoveAllInstance();
        }

        private void OnDeviceBaseTimer(Object sender, ElapsedEventArgs e)
        {
            dev_timer_cntr_1000ms_++;
            if (dev_timer_cntr_1000ms_ >= DEVICE_TIMER_CNTR_1000MS) {
				lock (devi_list_) {
					devi_list_.ForEach(devi => devi.DataRateSamplingRequest());
				}
                dev_timer_cntr_1000ms_ = 0;
            }
        }

        public IEnumerable<DeviceClass> GetDeviceList()
        {
            lock (devd_list_) {
                return (devd_list_.ToArray());
            }
        }

        public DeviceClass FindClass(Guid class_id)
        {
            lock (devd_list_) {
                return (devd_list_.Find(devd => devd.ID == class_id));
            }
        }

        public bool AddDevice(DeviceClass devd)
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

			DebugManager.MessageOut(DebugEventSender.Device ,DebugEventType.ControlEvent, String.Format("Device Class Added. count = {0}", devd_list_.Count));

            return (true);
        }

        public void RemoveAllInstance()
        {
            foreach (var devi in GetInstances()) {
                devi.DeviceShutdownRequest();
            }
        }

        public DeviceInstance[] GetInstances()
        {
            lock (devi_list_) {
                return (devi_list_.ToArray());
            }
        }

        public DeviceInstance CreateInstance(DeviceConfig devconf, Guid class_id, DeviceProperty devp)
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

				DebugManager.MessageOut(DebugEventSender.Device ,DebugEventType.ControlEvent, String.Format("Device Instance Added. count = {0}", devi_list_.Count));

                /* 各インスタンスのリダイレクト先を更新 */
                UpdateRedirectMap();
            }

            /* デバイス処理開始 */
            devi.DeviceStart();

            return (devi);
        }

        public DeviceProperty CreateProperty(Guid class_id)
        {
            /* クラスIDからクラスを検索 */
            var devd = FindClass(class_id);

            if (devd == null)return (null);

            return (devd.CreateProperty());
        }

        internal void SetupPacket(PacketObject packet)
        {
            GatePacketManager.BasePacketManager.Enqueue(packet);
        }

        internal void UpdateRedirectMap()
        {
            foreach (var devi in devi_list_) {
                devi.UpdateRedirectMap(devi_list_);
            }
        }

        public void Poll()
        {
            lock (devi_list_) {
                /* 終了済みインスタンスを破棄 */
                if (devi_list_.RemoveAll(devi => devi.IsShutdown) > 0) {
					DebugManager.MessageOut(DebugEventSender.Device ,DebugEventType.ControlEvent, String.Format("Device Instance Removed. count = {0}", devi_list_.Count));

                    /* 各インスタンスのリダイレクト先を更新 */
                    UpdateRedirectMap();
                }
            }
        }

        public DeviceInstance CreateDeviceObject(DeviceConfig devconf, Guid class_id, DeviceProperty devp)
        {
            return (CreateInstance(devconf, class_id, devp));
        }

        public DeviceProperty CreateDeviceProperty(Guid class_id)
        {
            return (CreateProperty(class_id));
        }
    }
}
