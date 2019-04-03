using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Framework.PacketConverter;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_PacketConverter : UserControl
    {
        private MainWindow_PacketConverterPanel panel_ = null;


        private bool init_complete_ = false;


        public MainWindow_PacketConverter()
        {
            InitializeComponent();
        }

        public MainWindow_PacketConverter(MainWindow_PacketConverterPanel panel, PacketConverterInstance pcvti) : this()
        {
            panel_ = panel;
            Instance = pcvti;
            Instance.Dock = DockStyle.Fill;

            Panel_Contents.Controls.Add(Instance);

            ChkBox_Enable.Checked = Instance.Property.ConverterEnable.Value;
            Label_Name.Text = Instance.Class.Name;
            ChkBox_FilterEnable.Checked = Instance.Property.TargetFilterEnable.Value;
            TBox_FilterValue.Text = Instance.Property.TargetFilterValue.Value.Trim();

            UpdatePacketCount();

            init_complete_ = true;
        }

        public PacketConverterInstance Instance { get; } = null;

        public bool ConverterEnable
        {
            get
            {
                return (Instance.Property.ConverterEnable.Value);
            }
        }

        private void MoveControl(Point pos)
        {
            panel_.MoveConverterIndex(this, PointToScreen(pos));
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
            Instance.Property.TargetFilterValue.Value = TBox_FilterValue.Text.Trim();

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
                TBox_FilterValue.BackColor = (filter_check)
                                           ? (RtsCore.Parameter.COLOR_OK)
                                           : (RtsCore.Parameter.COLOR_NG);
            } else {
                TBox_FilterValue.BackColor = Color.White;
            }

            if (filter_enable) {
                Btn_Filter.Image = (filter_check) ? (RtsCore.Resource.Images.target_ok_22x22) : (RtsCore.Resource.Images.target_ng_22x22);
            } else {
                Btn_Filter.Image = RtsCore.Resource.Images.target_22x22;
            }

            panel_.UpdateView();
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            panel_.RemovePacketConverter(this);
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
