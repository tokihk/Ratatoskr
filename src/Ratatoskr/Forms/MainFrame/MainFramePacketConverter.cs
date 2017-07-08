using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.PacketConverters;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFramePacketConverter : UserControl
    {
        private bool init_complete_ = false;


        public MainFramePacketConverter()
        {
            InitializeComponent();
        }

        public MainFramePacketConverter(PacketConverterInstance pcvti) : this()
        {
            Instance = pcvti;
            Instance.Dock = DockStyle.Fill;

            Panel_Contents.Controls.Add(Instance);

            ChkBox_Enable.Checked = Instance.Property.ConverterEnable.Value;
            Label_Name.Text = Instance.Class.Name;
            ChkBox_FilterEnable.Checked = Instance.Property.TargetFilterEnable.Value;
            TBox_FilterValue.Text = Instance.Property.TargetFilterValue.Value;

            UpdatePacketCount();

            init_complete_ = true;
        }

        public PacketConverterInstance Instance { get; } = null;

        private void MoveControl(Point pos)
        {
            var panel = Parent as MainFramePacketConverterPanel;

            if (panel == null)return;

            panel.MoveConverterIndex(this, PointToScreen(pos));
        }

        public void UpdatePacketCount()
        {
            Label_OutputCount.Text = Instance.OutputPacketCount.ToString();
        }

        private void Apply()
        {
            if (!init_complete_)return;

            Instance.Property.ConverterEnable.Value = ChkBox_Enable.Checked;
            Instance.Property.TargetFilterEnable.Value = ChkBox_FilterEnable.Checked;
            Instance.Property.TargetFilterValue.Value = TBox_FilterValue.Text;

            Instance.UpdateFilter();
            Instance.UpdateConvertStatus();

            UpdateView();
        }

        private void UpdateView()
        {
            var text = TBox_FilterValue.Text;
            var filter_enable = (ChkBox_FilterEnable.Checked) && (text != null) && (text.Length > 0);
            var filter_check = PacketConverterInstance.CheckFilter(text);

            TBox_FilterValue.ForeColor = (text != Instance.Property.TargetFilterValue.Value)
                                       ? (Color.Gray)
                                       : (Color.Black);

            if ((text != null) && (text.Length > 0)) {
                TBox_FilterValue.BackColor = (filter_check) ? (Color.LightSkyBlue) : (Color.LightPink);
            } else {
                TBox_FilterValue.BackColor = Color.White;
            }

            if (filter_enable) {
                Btn_Filter.Image = (filter_check) ? (Properties.Resources.target_ok_22x22) : (Properties.Resources.target_ng_22x22);
            } else {
                Btn_Filter.Image = Properties.Resources.target_22x22;
            }
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            var panel = Parent as MainFramePacketConverterPanel;

            if (panel == null)return;

            panel.RemovePacketConverter(this);
        }

        private void Label_Handle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                MoveControl(e.Location);
            }
        }

        private void ChkBox_Enable_CheckedChanged(object sender, EventArgs e)
        {
            Apply();
        }

        private void Btn_Filter_Click(object sender, EventArgs e)
        {
            CMenu_Target.Show(
                PointToScreen(
                    new Point(
                        Btn_Filter.Location.X,
                        Btn_Filter.Location.Y + Btn_Filter.Size.Height)));
        }

        private void ChkBox_FilterEnable_Click(object sender, EventArgs e)
        {
            ChkBox_FilterEnable.Checked = !ChkBox_FilterEnable.Checked;

            Apply();
        }

        private void TBox_FilterValue_TextChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void TBox_FilterValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                Apply();
            }
        }
    }
}
