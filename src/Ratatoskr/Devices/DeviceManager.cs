using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Devices
{
    internal class DeviceManager
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

        public DeviceInstance FindInstance(Guid obj_id)
        {
            try {
                return (GetInstances().First(devi => devi.ID == obj_id));
            } catch {
                return (null);
            }
        }

        public void RemoveAllInstance()
        {
            foreach (var devi in GetInstances()) {
                devi.ShutdownRequest();
            }
        }

        public DeviceInstance[] GetInstances()
        {
            lock (devi_list_) {
                return (devi_list_.ToArray());
            }
        }

        public DeviceInstance CreateInstance(Guid class_id, Guid obj_id, string name, DeviceProperty devp)
        {
            /* クラスIDからクラスを検索 */
            var devd = FindClass(class_id);

            if (devd == null)return (null);

            /* インスタンス作成 */
            var devi = devd.CreateInstance(this, obj_id, name, devp);

            if (devi == null)return (null);

            /* インスタンス登録 */
            lock (devi_list_) {
                devi_list_.Add(devi);
            }

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

        public void Poll()
        {
            lock (devi_list_) {
                /* 終了済みインスタンスを破棄 */
                devi_list_.RemoveAll(devi => devi.IsShutdown);
            }
        }
    }
}
