﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore;
using Ratatoskr.General.Packet.Filter;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView
{
    internal partial class PacketViewControl : UserControl
    {
        private bool filter_enable_ = false;

        private string           filter_exp_new_ = "";
        private PacketFilterController filter_obj_new_ = null;

        private string           filter_exp_busy_ = "";
        private PacketFilterController filter_obj_busy_ = null;

        private PacketViewManager  viewm_ = null;

        public PacketViewInstance Instance { get; }

        
        public PacketViewControl()
        {
            InitializeComponent();
        }

        public PacketViewControl(PacketViewManager viewm, PacketViewInstance viewi) : this()
        {
            viewm_ = viewm;
            Instance = viewi;
            Instance.Dock = DockStyle.Fill;

            Panel_Contents.Controls.Add(Instance);

            ChkBox_Filter.Checked = Instance.Property.TargetFilterEnable.Value;
            TBox_Filter.Text = Instance.Property.TargetFilterValue.Value.Trim();

            Apply(false);
        }

        public void BackupProperty()
        {
            Instance.Property.TargetFilterEnable.Value = ChkBox_Filter.Checked;
            Instance.Property.TargetFilterValue.Value = TBox_Filter.Text.Trim();

            Instance.BackupProperty();
        }

        private void Apply(bool redraw_req = true)
        {
            filter_exp_busy_ = filter_exp_new_;
            filter_obj_busy_ = filter_obj_new_;

            UpdateView();

            viewm_.RedrawPacket();
        }

        private void UpdateView()
        {
            /* 表示中のフィルター式をコンパイル */
            filter_enable_ = ChkBox_Filter.Checked;
            filter_exp_new_ = TBox_Filter.Text;
            filter_obj_new_ = PacketFilterController.Build(filter_exp_new_);

            /* 表示更新 */
            if (filter_exp_new_.Length > 0) {
                TBox_Filter.BackColor = (filter_obj_new_ != null)
                                      ? (Ratatoskr.Resource.AppColors.Ok)
                                      : (Ratatoskr.Resource.AppColors.Ng);
            } else {
                TBox_Filter.BackColor = Color.White;
            }

            /* 変更状態確認 */
            TBox_Filter.ForeColor = (filter_exp_busy_ != filter_exp_new_)
                                  ? (Color.Gray)
                                  : (Color.Black);
        }

        internal void Idle()
        {
            Instance.Idle();
        }

        internal void ClearPacket()
        {
            Instance.ClearPacket();
        }

        internal void BeginDrawPacket(bool auto_scroll)
        {
            Instance.BeginDrawPacket(auto_scroll);
        }

        internal void EndDrawPacket(bool auto_scroll, bool next_packet_exist)
        {
            Instance.EndDrawPacket(auto_scroll, next_packet_exist);
        }

        internal void DrawPacket(IEnumerable<PacketObject> packets)
        {
            foreach (var packet in packets) {
                DrawPacket(packet);
            }
        }

        internal void DrawPacket(PacketObject packet)
        {
            if (   (!filter_enable_)
                || (filter_obj_busy_ == null)
                || (filter_obj_busy_.Input(packet))
            ) {
                Instance.DrawPacket(packet);
            }
        }

        private void ChkBox_Filter_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
            Apply();
        }

        private void TBox_Filter_TextChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void TBox_Filter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                Apply();
            }
        }
    }
}
