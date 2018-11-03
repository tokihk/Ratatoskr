using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Packet;

namespace RtsCore.Framework.PacketView
{
    public partial class PacketViewInstance : UserControl, IDisposable
    {
        public    bool InitializeComplete { get; internal set; } = false;
        public    bool OperationBusy      { get; protected set; } = false;

        private PacketViewManager      viewm_;


        public PacketViewInstance(PacketViewManager viewm, PacketViewClass viewd, PacketViewProperty viewp, Guid id)
        {
            InitializeComponent();

            viewm_ = viewm;
            Class = viewd;
            Property = viewp;
            ID = id;
        }

        public PacketViewInstance()
        {
            InitializeComponent();
        }

        public Guid         ID       { get; }
        public PacketViewClass    Class    { get; }
        public PacketViewProperty Property { get; }


        public void BackupProperty()
        {
            if (!InitializeComplete)return;

            OnBackupProperty();
        }

        internal void Idle()
        {
            OnIdle();
        }

        internal void ClearPacket()
        {
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

        internal void EndDrawPacket(bool auto_scroll, bool next_packet_exist)
        {
            OnDrawPacketEnd(auto_scroll, next_packet_exist);
        }

        internal void DrawPacket(PacketObject packet)
        {
            OnDrawPacket(packet);
        }

        protected virtual void OnBackupProperty() { }
        protected virtual void OnIdle() { }
        protected virtual void OnClearPacket() { }
        protected virtual void OnDrawPacketBegin(bool auto_scroll) { }
        protected virtual void OnDrawPacketEnd(bool auto_scroll, bool next_packet_exist) { }
        protected virtual void OnDrawPacket(PacketObject packet) { }
    }
}
