using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Packet;
using Ratatoskr.Scripts.PacketFilterExp;
using Ratatoskr.Scripts.PacketFilterExp.Parser;

namespace Ratatoskr.PacketViews
{
    internal partial class ViewControl : UserControl
    {
        private bool filter_enable_ = false;

        private string           filter_exp_new_ = "";
        private ExpressionFilter filter_obj_new_ = null;

        private string           filter_exp_busy_ = "";
        private ExpressionFilter filter_obj_busy_ = null;

        private ViewManager  viewm_ = null;

        public ViewInstance Instance { get; }

        
        public ViewControl()
        {
            InitializeComponent();
        }

        public ViewControl(ViewManager viewm, ViewInstance viewi) : this()
        {
            viewm_ = viewm;
            Instance = viewi;
            Instance.Dock = DockStyle.Fill;

            Panel_Contents.Controls.Add(Instance);

            ChkBox_Filter.Checked = Instance.Property.TargetFilterEnable.Value;
            TBox_Filter.Text = Instance.Property.TargetFilterValue.Value.Trim();

            UpdateView();
        }

        public void BackupProperty()
        {
            Instance.Property.TargetFilterEnable.Value = ChkBox_Filter.Checked;
            Instance.Property.TargetFilterValue.Value = TBox_Filter.Text.Trim();

            Instance.BackupProperty();
        }

        private void Apply()
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
            filter_obj_new_ = ExpressionFilter.Build(filter_exp_new_);

            /* 表示更新 */
            if (filter_exp_new_.Length > 0) {
                TBox_Filter.BackColor = (filter_obj_new_ != null) ? (Color.LightSkyBlue) : (Color.LightPink);
            } else {
                TBox_Filter.BackColor = Color.White;
            }

            /* 変更状態確認 */
            TBox_Filter.ForeColor = (filter_exp_busy_ != filter_exp_new_) ? (Color.Gray) : (Color.Black);
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

        internal void EndDrawPacket(bool auto_scroll)
        {
            Instance.EndDrawPacket(auto_scroll);
        }

        internal void DrawPacket(IEnumerable<PacketObject> packets)
        {
            foreach (var packet in packets) {
                DrawPacket(packet);
            }
        }

        internal void DrawPacket(PacketObject packet)
        {
            if (   (filter_enable_)
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
