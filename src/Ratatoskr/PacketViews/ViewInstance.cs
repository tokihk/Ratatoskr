﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms;
using Ratatoskr.Packet;

namespace Ratatoskr.PacketViews
{
    internal partial class ViewInstance : UserControl, IDisposable
    {
        internal bool InitializeComplete { get; set; } = false;
        internal bool OperationBusy      { get; set; } = false;

        private ViewManager      viewm_;


        public ViewInstance(ViewManager viewm, ViewClass viewd, ViewProperty viewp, Guid id)
        {
            InitializeComponent();

            viewm_ = viewm;
            Class = viewd;
            Property = viewp;
            ID = id;
        }

        public ViewInstance()
        {
            InitializeComponent();
        }

        public Guid         ID       { get; }
        public ViewClass    Class    { get; }
        public ViewProperty Property { get; }


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
