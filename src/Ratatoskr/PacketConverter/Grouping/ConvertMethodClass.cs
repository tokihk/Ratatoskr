using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.PacketConverter;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketConverter.Grouping
{
    internal partial class ConvertMethodClass : UserControl
    {
        private Dictionary<string, ConvertMethodInstance[]> mi_map_ = new Dictionary<string, ConvertMethodInstance[]>();
        private PacketObject packet_prev_ = null;


        public ConvertMethodClass()
        {
            InitializeComponent();
        }

        public ConvertMethodClass(PacketConverterInstance instance, PacketConverterPropertyImpl prop)
        {
            Instance = instance;
            Property = prop;
        }

        public PacketConverterInstance     Instance { get; }
        public PacketConverterPropertyImpl Property { get; }

        public void BackupProperty()
        {
            OnBackupProperty();
        }

        protected virtual void OnBackupProperty()
        {
        }

        public void UpdateConvertStatus()
        {
            Instance.UpdateConvertStatus();
        }

        private ConvertMethodInstance CreateMethodInstance()
        {
            return (OnCreateMethodInstance());
        }

        protected virtual ConvertMethodInstance OnCreateMethodInstance()
        {
            return (null);
        }

        private ConvertMethodInstance LoadMethodInstance(PacketObject packet)
        {
            ConvertMethodInstance   mi;
            ConvertMethodInstance[] mi_list;

            if (Property.Local_EachAlias.Value) {
                /* Alias毎に解析 */
                if (!mi_map_.TryGetValue(packet.Alias, out mi_list)) {
                    mi_list = new ConvertMethodInstance[Enum.GetValues(typeof(PacketDirection)).Length];
                    mi_map_.Add(packet.Alias, mi_list);
                }

            } else {
                /* Alias混合で解析(強制的にFirstオブジェクトを使用) */
                if (mi_map_.Count == 0) {
                    mi_map_.Add(packet.Alias, new ConvertMethodInstance[Enum.GetValues(typeof(PacketDirection)).Length]);
                }
                mi_list = mi_map_.First().Value;
            }

            if (Property.Local_EachDirection.Value) {
                /* Direction毎に解析 */
                if (mi_list[(int)packet.Direction] == null) {
                    mi_list[(int)packet.Direction] = CreateMethodInstance();
                }
                mi = mi_list[(int)packet.Direction];
            } else {
                /* Direction混合で解析(強制的に0番目のオブジェクトを使用) */
                if (mi_list[0] == null) {
                    mi_list[0] = CreateMethodInstance();
                }
                mi = mi_list[0];
            }

            return (mi);
        }

        public void InputStatusClear()
        {
            mi_map_ = new Dictionary<string, ConvertMethodInstance[]>();
        }

        public void InputPacket(PacketObject input, ref List<PacketObject> output)
        {
            if (packet_prev_ != null) {
                if (   (Property.Local_DivideByDirectionChange.Value)
                    && (packet_prev_.Direction != input.Direction)
                ) {
                    InputBreak(ref output);
                }
            }

            var mi = LoadMethodInstance(input);

            if (mi != null) {
                mi.InputPacket(input, ref output);
            }
        }

        public void InputBreak(ref List<PacketObject> output)
        {
            foreach (var mi_list in mi_map_) {
                foreach (var mi in mi_list.Value) {
                    mi?.InputBreak(ref output);
                }
            }
        }

        public void InputPoll(ref List<PacketObject> output)
        {
            foreach (var mi_list in mi_map_) {
                foreach (var mi in mi_list.Value) {
                    mi?.InputPoll(ref output);
                }
            }
        }
    }
}
