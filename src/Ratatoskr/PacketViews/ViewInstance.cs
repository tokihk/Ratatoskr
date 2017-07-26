using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms;
using Ratatoskr.Scripts.PacketFilterExp.Parser;
using Ratatoskr.Scripts.PacketFilterExp;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.PacketViews
{
    internal partial class ViewInstance : UserControl, IDisposable
    {
        internal bool InitializeComplete { get; set; } = false;

        private ViewManager      viewm_;
        private ExpressionFilter filter_obj_ = null;


        public ViewInstance(ViewManager viewm, ViewClass viewd, ViewProperty viewp, Guid id)
        {
            InitializeComponent();

            viewm_ = viewm;
            Class = viewd;
            Property = viewp;
            ID = id;

            UpdateFilter();
        }

        public ViewInstance()
        {
            InitializeComponent();
        }

        public Guid         ID       { get; }
        public ViewClass    Class    { get; }
        public ViewProperty Property { get; }

        public static bool CheckFilter(string filter)
        {
            return (ExpressionFilter.Build(filter, null) != null);
        }

        public void UpdateFilter()
        {
            if (Property.TargetFilterEnable.Value) {
                filter_obj_ = ExpressionFilter.Build(Property.TargetFilterValue.Value, null);
            } else {
                filter_obj_ = null;
            }

            FormTaskManager.RedrawPacketRequest();
        }

        public void BackupProperty()
        {
            if (!InitializeComplete)return;

            OnBackupProperty();
        }

        internal void ClearPacket()
        {
            if (filter_obj_ != null) {
                filter_obj_.CallStack = new ExpressionCallStack();
            }

            OnClearPacket();
        }

        protected void RedrawPacket()
        {
            viewm_.RedrawPacket();
        }

        internal void BeginDrawPacket(bool auto_scroll)
        {
            OnDrawPacketBegin(auto_scroll);
        }

        internal void EndDrawPacket(bool auto_scroll)
        {
            OnDrawPacketEnd(auto_scroll);
        }

        internal void DrawPacket(IEnumerable<PacketObject> packets)
        {
            foreach (var packet in packets) {
                DrawPacket(packet);
            }
        }

        internal void DrawPacket(PacketObject packet)
        {
            if (   (filter_obj_ == null)
                || (filter_obj_.Input(packet))
            ) {
                OnDrawPacket(packet);
            }
        }

        protected virtual void OnBackupProperty() { }
        protected virtual void OnClearPacket() { }
        protected virtual void OnDrawPacketBegin(bool auto_scroll) { }
        protected virtual void OnDrawPacketEnd(bool auto_scroll) { }
        protected virtual void OnDrawPacket(PacketObject packet) { }
    }
}
