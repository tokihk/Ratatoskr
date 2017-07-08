using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.PacketViews;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFramePacketView : UserControl
    {
        public MainFramePacketView()
        {
            InitializeComponent();
        }

        public MainFramePacketView(ViewInstance viewi) : this()
        {
            Instance = viewi;
            Instance.Dock = DockStyle.Fill;

            Panel_Contents.Controls.Add(Instance);

            ChkBox_Filter.Checked = Instance.Property.TargetFilterEnable.Value;
            TBox_Filter.Text = Instance.Property.TargetFilterValue.Value;
        }

        public ViewInstance Instance { get; }


        private void Apply()
        {
            Instance.Property.TargetFilterEnable.Value = ChkBox_Filter.Checked;
            Instance.Property.TargetFilterValue.Value = TBox_Filter.Text;
            Instance.UpdateFilter();

            UpdateView();
        }

        private void UpdateView()
        {
            var text = TBox_Filter.Text;

            TBox_Filter.ForeColor = (text != Instance.Property.TargetFilterValue.Value)
                                  ? (Color.Gray)
                                  : (Color.Black);

            if ((text != null) && (text.Length > 0)) {
                TBox_Filter.BackColor = (ViewInstance.CheckFilter(text)) ? (Color.LightSkyBlue) : (Color.LightPink);
            } else {
                TBox_Filter.BackColor = Color.White;
            }
        }

        private void ChkBox_Filter_CheckedChanged(object sender, EventArgs e)
        {
            Instance.Property.TargetFilterEnable.Value = ChkBox_Filter.Checked;

            FormTaskManager.RedrawPacketRequest();
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
