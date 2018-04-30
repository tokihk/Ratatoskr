using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.TransferProtocols
{
    internal class TransferProtocolManager
    {
        private bool disposed_ = false;

        private List<TransferProtocolClass>    trpd_list_ = new List<TransferProtocolClass>();


        public TransferProtocolManager()
        {
        }

        ~TransferProtocolManager()
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

        public IEnumerable<TransferProtocolClass> GetClasses()
        {
            lock (trpd_list_) {
                return (trpd_list_.ToArray());
            }
        }

        public TransferProtocolClass FindClass(Guid class_id)
        {
            lock (trpd_list_) {
                return (trpd_list_.Find(devd => devd.ID == class_id));
            }
        }

        public bool AddClass(TransferProtocolClass devd)
        {
            if (devd == null)return (false);

            /* 重複IDをチェック */
            if (FindClass(devd.ID) != null)return (false);

            /* 新しいクラスを追加 */
            lock (trpd_list_) {
                trpd_list_.Add(devd);
            }

            return (true);
        }

        public TransferProtocolInstance CreateInstance(Guid class_id)
        {
            /* クラスIDからクラスを検索 */
            var devd = FindClass(class_id);

            if (devd == null)return (null);

            /* インスタンス作成 */
            return (devd.CreateInstance());
        }
    }
}
