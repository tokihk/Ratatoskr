using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketConverter
{
    public sealed class PacketConvertManager
    {
		public static PacketConvertManager Instance { get; } = new PacketConvertManager();


        public static void InputStatusClear(IEnumerable<PacketConverterInstance> pcvti_list)
        {
            foreach (var pcvti in pcvti_list) {
                pcvti.InputStatusClear();
            }
        }

        public static IEnumerable<PacketObject> InputPacket(IEnumerable<PacketConverterInstance> pcvti_list, IEnumerable<PacketObject> packets)
        {
            var output = new List<PacketObject>();

            foreach (var packet in packets) {
                output.AddRange(InputPacket(pcvti_list, packet));
            }

            return (output);
        }

        public static IEnumerable<PacketObject> InputPacket(IEnumerable<PacketConverterInstance> pcvti_list, PacketObject packet)
        {
            var input = (IEnumerable<PacketObject>)(new []{ packet });
            var output = new List<PacketObject>();

            foreach (var pcvti in pcvti_list) {
                if (pcvti.Property.ConverterEnable.Value) {
                    pcvti.InputPacket(input, ref output);
                    input = output;
                    output = new List<PacketObject>();
                }
            }

            return (input);
        }

        public static IEnumerable<PacketObject> InputBreakOff(IEnumerable<PacketConverterInstance> pcvti_list)
        {
            var output = new List<PacketObject>();

            foreach (var pcvti in pcvti_list) {
                pcvti.InputBreakOff(ref output);
            }

            return (output);
        }

        public static IEnumerable<PacketObject> InputPoll(IEnumerable<PacketConverterInstance> pcvti_list)
        {
            var output = new List<PacketObject>();

            foreach (var pcvti in pcvti_list) {
                pcvti.InputPoll(ref output);
            }

            return (output);
        }


        private List<PacketConverterClass>    pcvtd_list_ = new List<PacketConverterClass>();
        private List<PacketConverterInstance> pcvti_list_ = new List<PacketConverterInstance>();


        private PacketConvertManager()
        {
            AddClass(new Filter.PacketConverterClassImpl());
            AddClass(new Grouping.PacketConverterClassImpl());
            AddClass(new Convert.PacketConverterClassImpl());
        }

        public event Action ConvertStatusUpdated;

        public IEnumerable<PacketConverterClass> GetClassList()
        {
            lock (pcvtd_list_) {
                return (pcvtd_list_.ToArray());
            }
        }

        private PacketConverterClass FindClass(Guid class_id)
        {
            lock (pcvtd_list_) {
                return (pcvtd_list_.Find(viewd => viewd.ID == class_id));
            }
        }

        public bool AddClass(PacketConverterClass pcvtd)
        {
            if (pcvtd == null)return (false);

            /* 重複IDをチェック */
            if (FindClass(pcvtd.ID) != null)return (false);

            /* 新しいクラスを追加 */
            lock (pcvtd_list_) {
                pcvtd_list_.Add(pcvtd);
            }

            return (true);
        }

        public IEnumerable<PacketConverterInstance> GetInstanceList()
        {
            var devi_list_all = new List<PacketConverterInstance>();

            lock (pcvti_list_) {
                devi_list_all.AddRange(pcvti_list_);
            }

            return (devi_list_all.ToArray());
        }

        public PacketConverterInstance CreateInstance(Guid class_id, Guid obj_id, PacketConverterProperty pcvtp)
        {
            /* IDからクラスを検索 */
            var viewd = FindClass(class_id);

            if (viewd == null)return (null);

            /* インスタンス作成 */
            var viewi = viewd.CreateInstance(this, obj_id, pcvtp);

            if (viewi == null)return (null);

            /* インスタンス登録 */
            lock (pcvti_list_) {
                pcvti_list_.Add(viewi);
            }

            UpdateConvertStatus();

            return (viewi);
        }

        public PacketConverterInstance CreateInstance(string class_id, Guid obj_id, PacketConverterProperty pcvtp)
        {
            var id = Guid.Empty;

            if (!Guid.TryParse(class_id, out id))return (null);

            return (CreateInstance(id, obj_id, pcvtp));
        }

        public void RemoveInstance(PacketConverterInstance pcvti)
        {
            if (pcvti == null)return;

            lock (pcvti_list_) {
                pcvti_list_.Remove(pcvti);
            }

            UpdateConvertStatus();
        }

        public void SetInstanceIndex(PacketConverterInstance pcvti, int index)
        {
            lock (pcvti_list_) {
                index = (index < 0) ? (pcvti_list_.Count) : (index);
                index = Math.Min(pcvti_list_.Count - 1, index);
                index = Math.Max(0, index);

                /* リストから削除 */
                pcvti_list_.Remove(pcvti);

                /* 新しい位置に挿入 */
                pcvti_list_.Insert(index, pcvti);
            }

            /* 再描画 */
            UpdateConvertStatus();
        }

        public IEnumerable<PacketConverterInstance> GetCloneInstances()
        {
            var clone_list = new List<PacketConverterInstance>();

            lock (pcvti_list_) {
                foreach (var pcvti in pcvti_list_) {
                    /* 現在の設定をプロパティにバックアップ */
                    pcvti.BackupProperty();

                    /* 新規インスタンスを生成 */
                    var pcvti_clone = pcvti.Class.CreateInstance(null, Guid.Empty, pcvti.Property);

                    if (pcvti_clone == null)continue;

                    clone_list.Add(pcvti_clone);
                }
            }

            return (clone_list);
        }

        public PacketConverterProperty CreateProperty(Guid class_id)
        {
            /* クラスIDからクラスを検索 */
            var viewd = FindClass(class_id);

            if (viewd == null)return (null);

            return (viewd.CreateProperty());
        }

        public void UpdateConvertStatus()
        {
            ConvertStatusUpdated();
        }

        public void InputStatusClear()
        {
            lock (pcvti_list_) {
                InputStatusClear(pcvti_list_);
            }
        }

        public IEnumerable<PacketObject> InputPacket(IEnumerable<PacketObject> packets)
        {
            lock (pcvti_list_) {
                return (InputPacket(pcvti_list_, packets));
            }
        }

        public IEnumerable<PacketObject> InputPacket(PacketObject packet)
        {
            lock (pcvti_list_) {
                return (InputPacket(pcvti_list_, packet));
            }
        }

        public IEnumerable<PacketObject> InputBreakOff()
        {
            lock (pcvti_list_) {
                return (InputBreakOff(pcvti_list_));
            }
        }

        public IEnumerable<PacketObject> InputPoll()
        {
            lock (pcvti_list_) {
                return (InputPoll(pcvti_list_));
            }
        }
    }
}
