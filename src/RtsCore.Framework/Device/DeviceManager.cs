using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace RtsCore.Framework.Device
{
    public class DeviceManager
    {
        private bool disposed_ = false;

        private List<DeviceClass>    devd_list_ = new List<DeviceClass>();
        private List<DeviceInstance> devi_list_ = new List<DeviceInstance>();

        private PacketManager pktm_;


        public DeviceManager(PacketManager pktm)
        {
            pktm_ = pktm;
        }

        ~DeviceManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed_)return;

            if (disposing) {
            }

            disposed_ = true;
        }

        public IEnumerable<DeviceClass> GetClasses()
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

        public bool AddClass(DeviceClass devd)
        {
            if (devd == null)return (false);

            /* 重複IDをチェック */
            if (FindClass(devd.ID) != null)return (false);

            /* 新しいデバイスを追加 */
            lock (devd_list_) {
                devd_list_.Add(devd);
            }

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
            var devi = devd.CreateInstance(this, devconf, devp);

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

        public DeviceProperty CreateProperty(Guid class_id)
        {
            /* クラスIDからクラスを検索 */
            var devd = FindClass(class_id);

            if (devd == null)return (null);

            return (devd.CreateProperty());
        }

        internal void SetupPacket(PacketObject packet)
        {
            pktm_.Enqueue(packet);
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
                    /* 各インスタンスのリダイレクト先を更新 */
                    UpdateRedirectMap();
                }
            }
        }
    }
}
