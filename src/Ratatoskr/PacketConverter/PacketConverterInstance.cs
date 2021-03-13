using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.General.Packet.Filter;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketConverter
{
    public partial class PacketConverterInstance : UserControl
    {
        private PacketConvertManager pcvtm_;

        private ulong packet_count_output_ = 0;

        private PacketFilterController filter_obj_ = null;


        public PacketConverterInstance()
        {
            InitializeComponent();
        }

        public PacketConverterInstance(PacketConvertManager pcvtm, PacketConverterClass pcvtd, PacketConverterProperty pcvtp, Guid id)
        {
            InitializeComponent();

            pcvtm_ = pcvtm;
            Class = pcvtd;
            Property = pcvtp;
            ID = id;

            UpdateFilter();
        }

        public Guid                    ID       { get; }
        public PacketConverterClass    Class    { get; }
        public PacketConverterProperty Property { get; set; }

        public void BackupProperty()
        {
            OnBackupProperty();
        }

        protected virtual void OnBackupProperty()
        {
        }

        public ulong OutputPacketCount
        {
            get { return (packet_count_output_); }
        }

        public void UpdateConvertStatus()
        {
            if (pcvtm_ != null) {
                pcvtm_.UpdateConvertStatus();
            }
        }

        public static bool CheckFilter(string filter)
        {
            return (PacketFilterController.Build(filter) != null);
        }

        public void UpdateFilter()
        {
            if (Property.TargetFilterEnable.Value) {
                filter_obj_ = PacketFilterController.Build(Property.TargetFilterValue.Value);
            } else {
                filter_obj_ = null;
            }
        }

        public void InputStatusClear()
        {
            packet_count_output_ = 0;

            if (filter_obj_ != null) {
                filter_obj_.CallStack = new PacketFilterCallStack();
            }

            OnInputStatusClear();
        }

        public void InputPacket(PacketObject input, ref List<PacketObject> output)
        {
            var packet_count = output.Count;

            if (   (filter_obj_ != null)
                && (!filter_obj_.Input(input))
            ) {
                /* パケット変換対象ではない場合は変換無しで追加 */
                output.Add(input);

            } else {
                /* パケット変換実施 */
                OnInputPacket(input, ref output);
            }

            /* 通過したパケット数を更新 */
            packet_count_output_ += (ulong)(output.Count - packet_count);
        }

        public void InputPacket(IEnumerable<PacketObject> input, ref List<PacketObject> output)
        {
            foreach (var packet in input) {
                InputPacket(packet, ref output);
            }
        }

        public void InputBreakOff(ref List<PacketObject> output)
        {
            OnInputBreak(ref output);
        }

        public void InputPoll(ref List<PacketObject> output)
        {
            OnInputPoll(ref output);
        }

        protected virtual void OnInputStatusClear()
        {
        }

        protected virtual void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
        }

        protected virtual void OnInputBreak(ref List<PacketObject> output)
        {
        }

        protected virtual void OnInputPoll(ref List<PacketObject> output)
        {
        }
    }
}
