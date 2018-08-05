using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.PacketViews.Graph.Configs;

namespace Ratatoskr.PacketViews.Graph
{
    internal partial class GraphControlPanel : UserControl
    {
        private class ChannelListItem
        {
            public ChannelListItem(uint channel_no)
            {
                Channel = channel_no;
            }

            public uint Channel { get; }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is uint) {
                    return (Channel == (uint)obj);
                }

                return (base.Equals(obj));
            }

            public override string ToString()
            {
                return (string.Format("CH{0}", Channel));
            }
        }


        private ViewPropertyImpl prop_;

        private bool ui_update_busy_ = false;


        public event EventHandler SamplingSettingUpdated;
        public event EventHandler DisplaySettingUpdated;


        public GraphControlPanel()
        {
            InitializeComponent();
            InitializeComboBox<DataFormatType>(CBox_DataFormat);
            InitializeComboBox<DataEndianType>(CBox_DataEndian);
            InitializeComboBox<DataCollectModeType>(CBox_DataCollectMode);
            InitializeChannelList();
//            UpdateTrackBarFromNumeric(TBar_ChSet_ValueDiv, Num_ChSet_ValueMag);
//            UpdateTrackBarFromNumeric(TBar_ChSet_ValueOffset, Num_ChSet_ValueCenter);
        }

        private void InitializeComboBox<EnumType>(ComboBox control)
        {
            control.BeginUpdate();
            {
                control.Items.Clear();
                foreach (EnumType value in Enum.GetValues(typeof(EnumType))) {
                    control.Items.Add(value);
                }
                control.SelectedIndex = 0;
            }
            control.EndUpdate();
        }

        private void InitializeDataCollectMode()
        {
            CBox_DataCollectMode.BeginUpdate();
            {
                CBox_DataCollectMode.Items.Clear();
                foreach (DataCollectModeType value in Enum.GetValues(typeof(DataCollectModeType))) {
                    CBox_DataCollectMode.Items.Add(value);
                }
                CBox_DataCollectMode.SelectedIndex = 0;
            }
            CBox_DataCollectMode.EndUpdate();
        }

        private void InitializeChannelList()
        {
            CBox_ChSet_Channel.BeginUpdate();
            {
                CBox_ChSet_Channel.Items.Clear();
                for (var channel = (uint)1; channel <= 10; channel++) {
                    CBox_ChSet_Channel.Items.Add(new ChannelListItem(channel));
                }
                CBox_ChSet_Channel.SelectedIndex = 0;
            }
            CBox_ChSet_Channel.EndUpdate();
        }

        public void LoadConfig(ViewPropertyImpl prop)
        {
            CBox_DataFormat.SelectedItem = prop.DataFormat.Value;
            CBox_DataEndian.SelectedItem = prop.DataEndian.Value;
            Num_DataChannel.Value = prop.DataChannelNum.Value;
            CBox_DataCollectMode.SelectedItem = prop.DataCollectMode.Value;

            Num_SamplingPoint.Value = prop.SamplingPoint.Value;
            Num_SamplingInterval.Value = prop.SamplingInterval.Value / 1000;

            Num_DisplayPoint.Value = prop.DisplayPoint.Value;
            Num_AxisY_ValueMax.Value = prop.AxisY_ValueMax.Value;
            Num_AxisY_ValueMin.Value = prop.AxisY_ValueMin.Value;

            CBox_ChSet_Channel.SelectedItem = prop.CurrentChannel.Value;

            prop_ = prop;
        }

        private void LoadChannelConfig()
        {
            if (prop_ == null)return;

            if (   (prop_.CurrentChannel.Value > 0)
                && (prop_.CurrentChannel.Value <= prop_.ChannelList.Value.Count)
            ) {
                var conf = prop_.ChannelList.Value[(int)prop_.CurrentChannel.Value - 1];

                Btn_ChSet_Color.BackColor = conf.ForeColor;
                Num_ChSet_ValueMag.Value = conf.Magnification;
                TBar_ChSet_ValueOffset.Value = (int)Math.Max((decimal)TBar_ChSet_ValueOffset.Minimum, Math.Min((decimal)TBar_ChSet_ValueOffset.Maximum, conf.Offset));
            }
        }

        public void BackupConfig()
        {
            if (prop_ == null)return;

            BackupSamplingConfig();

            BackupDisplayConfig();
        }

        private void BackupSamplingConfig()
        {
            if (prop_ == null)return;

            prop_.DataFormat.Value = (DataFormatType)CBox_DataFormat.SelectedItem;
            prop_.DataEndian.Value = (DataEndianType)CBox_DataEndian.SelectedItem;
            prop_.DataChannelNum.Value = Num_DataChannel.Value;
            prop_.DataCollectMode.Value = (DataCollectModeType)CBox_DataCollectMode.SelectedItem;

            prop_.SamplingPoint.Value = Num_SamplingPoint.Value;
            prop_.SamplingInterval.Value = Num_SamplingInterval.Value * 1000;
        }

        private void BackupDisplayConfig()
        {
            if (prop_ == null)return;

            BackupChannelConfig();

            prop_.DisplayPoint.Value = Num_DisplayPoint.Value;
            prop_.AxisY_ValueMax.Value = Num_AxisY_ValueMax.Value;
            prop_.AxisY_ValueMin.Value = Num_AxisY_ValueMin.Value;

            prop_.CurrentChannel.Value = (CBox_ChSet_Channel.SelectedItem as ChannelListItem).Channel;
        }

        private void BackupChannelConfig()
        {
            if (prop_ == null)return;

            var conf = GetCurrentChannelConfig();

            if (conf != null) {
                conf.ForeColor = Btn_ChSet_Color.BackColor;
                conf.Magnification = Num_ChSet_ValueMag.Value;
                conf.Offset = TBar_ChSet_ValueOffset.Value;
            }
        }

        private ChannelConfig GetCurrentChannelConfig()
        {
            var conf = (ChannelConfig)null;

            if (   (prop_.CurrentChannel.Value > 0)
                && (prop_.CurrentChannel.Value <= prop_.ChannelList.Value.Count)
            ) {
                conf = prop_.ChannelList.Value[(int)prop_.CurrentChannel.Value - 1];

                conf.ForeColor = Btn_ChSet_Color.BackColor;
                conf.Magnification = Num_ChSet_ValueMag.Value;
                conf.Offset = TBar_ChSet_ValueOffset.Value;
            }

            return (conf);
        }

        private void UpdateTrackBarFromNumeric(TrackBar tbar, NumericUpDown num)
        {
            if (ui_update_busy_)return;

            ui_update_busy_ = true;
            {
                tbar.Value = (int)((tbar.Maximum - tbar.Minimum) * num.Value / num.Maximum);
            }
            ui_update_busy_ = false;
        }

        private void CBox_ChSet_Channel_SelectedIndexChanged(object sender, EventArgs e)
        {
            BackupConfig();

            LoadChannelConfig();
        }

        private void OnSamplingSettingUpdated(object sender, EventArgs e)
        {
            BackupSamplingConfig();

            SamplingSettingUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void OnDisplaySettingUpdated(object sender, EventArgs e)
        {
            /* 補正 */
            Num_AxisY_ValueMin.Value = Math.Min(Num_AxisY_ValueMin.Value, Num_AxisY_ValueMax.Value);
            Num_AxisY_ValueMax.Value = Math.Max(Num_AxisY_ValueMin.Value, Num_AxisY_ValueMax.Value);

            BackupDisplayConfig();

            DisplaySettingUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void Btn_ChSet_Color_Click(object sender, EventArgs e)
        {
            var conf = GetCurrentChannelConfig();

            if (conf == null)return;

            var dialog = new ColorDialog();

            dialog.Color = conf.ForeColor;

            if (dialog.ShowDialog() != DialogResult.OK)return;

            Btn_ChSet_Color.BackColor = dialog.Color;

            OnDisplaySettingUpdated(sender, e);
        }
    }
}
