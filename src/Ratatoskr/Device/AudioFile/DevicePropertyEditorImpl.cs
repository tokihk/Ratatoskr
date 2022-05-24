using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace Ratatoskr.Device.AudioFile
{
    internal partial class DevicePropertyEditorImpl : DevicePropertyEditor
    {
        private DevicePropertyImpl devp_;


        public DevicePropertyEditorImpl() : base()
        {
            InitializeComponent();
        }

        public DevicePropertyEditorImpl(DevicePropertyImpl devp) : this()
        {
            devp_ = devp as DevicePropertyImpl;

            InitializeSamplingRate();
            InitializeBitsPerSample();
            InitializeChannelNum();
            InitializeConnectActionList();

            TBox_InputFilePath.Text = devp.InputFilePath.Value;
            CBox_InputSamplingRate.SelectedItem = devp.InputSamplingRate.Value;
            CBox_InputBitsPerSample.SelectedItem = devp.InputBitsPerSample.Value;
            CBox_InputChannelNum.SelectedItem = devp.InputChannelNum.Value;

            Num_RepeatCount.Value = devp.RepeatCount.Value;
            CBox_ConnectAction.SelectedItem = devp.ConnectAction.Value;
        }

        private void InitializeSamplingRate()
        {
            CBox_InputSamplingRate.BeginUpdate();
            {
                foreach (var value in Enum.GetValues(typeof(SamplingRateType))) {
                    CBox_InputSamplingRate.Items.Add(value);
                }
                CBox_InputSamplingRate.SelectedIndex = 0;
            }
            CBox_InputSamplingRate.EndUpdate();
        }

        private void InitializeBitsPerSample()
        {
            CBox_InputBitsPerSample.BeginUpdate();
            {
                foreach (var value in Enum.GetValues(typeof(BitPerSampleType))) {
                    CBox_InputBitsPerSample.Items.Add(value);
                }
                CBox_InputBitsPerSample.SelectedIndex = 0;
            }
            CBox_InputBitsPerSample.EndUpdate();
        }

        private void InitializeChannelNum()
        {
            CBox_InputChannelNum.BeginUpdate();
            {
                foreach (var value in Enum.GetValues(typeof(ChannelNumberType))) {
                    CBox_InputChannelNum.Items.Add(value);
                }
                CBox_InputChannelNum.SelectedIndex = 0;
            }
            CBox_InputChannelNum.EndUpdate();
        }

        private void InitializeConnectActionList()
        {
            CBox_ConnectAction.BeginUpdate();
            {
                foreach (ConnectActionType item in Enum.GetValues(typeof(ConnectActionType))) {
                    CBox_ConnectAction.Items.Add(item);
                }
                if (CBox_ConnectAction.Items.Count > 0) {
                    CBox_ConnectAction.SelectedIndex = 0;
                }
            }
            CBox_ConnectAction.EndUpdate();
        }

        private void UpdateInputFileInfo()
        {
            var info = new StringBuilder();

            try {
                using (var reader = new MediaFoundationReader(TBox_InputFilePath.Text)) {
                    /* Format */
                    info.AppendFormat("Encoding:      {0}", reader.WaveFormat.Encoding.ToString());
                    info.AppendLine();

                    /* Time */
                    info.AppendFormat("Time:          {0}", reader.TotalTime);
                    info.AppendLine();

                    /* Sampling Rate */
                    info.AppendFormat("Sampling Rate: {0} Hz", reader.WaveFormat.SampleRate);
                    info.AppendLine();

                    /* Bits Per Sample */
                    info.AppendFormat("BitsPerSample: {0} bit", reader.WaveFormat.BitsPerSample);
                    info.AppendLine();

                    /* Channel */
                    info.AppendFormat("Channel:       {0}", reader.WaveFormat.Channels);
                }
            } catch {
                info.Append("Unknown format.");
            }

            TBox_InputFileInfo.Text = info.ToString();
        }

        public override void Flush()
        {
            devp_.InputFilePath.Value = TBox_InputFilePath.Text;
            devp_.InputSamplingRate.Value = (SamplingRateType)CBox_InputSamplingRate.SelectedItem;
            devp_.InputBitsPerSample.Value = (BitPerSampleType)CBox_InputBitsPerSample.SelectedItem;
            devp_.InputChannelNum.Value = (ChannelNumberType)CBox_InputChannelNum.SelectedItem;

            devp_.RepeatCount.Value = Num_RepeatCount.Value;
            devp_.ConnectAction.Value = (ConnectActionType)CBox_ConnectAction.SelectedItem;
        }

        private void Btn_InputFileSelect_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() != DialogResult.OK)return;

            TBox_InputFilePath.Text = dialog.FileName;
        }

        private void TBox_InputFilePath_TextChanged(object sender, EventArgs e)
        {
            UpdateInputFileInfo();
        }
    }
}
