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
using RtsCore.Framework.Device;

namespace Ratatoskr.Devices.AudioFile
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
            CBox_InputSamplingRate.SelectedItem = (uint)devp.InputSamplingRate.Value;
            CBox_InputBitsPerSample.SelectedItem = (uint)devp.InputBitsPerSample.Value;
            CBox_InputChannelNum.SelectedItem = devp.InputChannelNum.Value;

            Num_RepeatCount.Value = devp.RepeatCount.Value;
            CBox_ConnectAction.SelectedItem = devp.ConnectAction.Value;
        }

        private void InitializeSamplingRate()
        {
            var items = new [] {
                8000,
                11025,
                22050,
                44100,
            };

            CBox_InputSamplingRate.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_InputSamplingRate.Items.Add((uint)item);
                }
                CBox_InputSamplingRate.SelectedIndex = 0;
            }
            CBox_InputSamplingRate.EndUpdate();
        }

        private void InitializeBitsPerSample()
        {
            var items = new [] {
                8,
                16,
            };

            CBox_InputBitsPerSample.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_InputBitsPerSample.Items.Add((uint)item);
                }
                CBox_InputBitsPerSample.SelectedIndex = 0;
            }
            CBox_InputBitsPerSample.EndUpdate();
        }

        private void InitializeChannelNum()
        {
            var items = new [] {
                1,
                2,
            };

            CBox_InputChannelNum.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_InputChannelNum.Items.Add((uint)item);
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
                info.Append("認識できないファイルです。");
            }

            TBox_InputFileInfo.Text = info.ToString();
        }

        public override void Flush()
        {
            devp_.InputFilePath.Value = TBox_InputFilePath.Text;
            devp_.InputSamplingRate.Value = uint.Parse(CBox_InputSamplingRate.Text);
            devp_.InputBitsPerSample.Value = uint.Parse(CBox_InputBitsPerSample.Text);
            devp_.InputChannelNum.Value = uint.Parse(CBox_InputChannelNum.Text);

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
