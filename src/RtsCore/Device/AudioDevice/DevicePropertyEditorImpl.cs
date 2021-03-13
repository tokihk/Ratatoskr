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
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace RtsCore.Device.AudioDevice
{
    internal partial class DevicePropertyEditorImpl : DevicePropertyEditor
    {
        private class InputDeviceListItem
        {
            public InputDeviceListItem(WaveInCapabilities value)
            {
                Value = value;
            }

            public WaveInCapabilities Value { get; }

            public override int GetHashCode()
            {
                return (base.GetHashCode());
            }

            public override bool Equals(object obj)
            {
                if (obj is string) {
                    return (Value.NameGuid.ToString() == (obj as string));
                } else {
                    return (base.Equals(obj));
                }
            }

            public override string ToString()
            {
                return (Value.ProductName);
            }
        }

        private class OutputDeviceListItem
        {
            public OutputDeviceListItem(MMDevice dev)
            {
                Value = dev;
            }

            public MMDevice Value { get; }

            public override int GetHashCode()
            {
                return (base.GetHashCode());
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() == typeof(string)) {
                    return (Value.ID == (obj as string));
                } else {
                    return (base.Equals(obj));
                }
            }

            public override string ToString()
            {
                return (Value.FriendlyName);
            }
        }

        private DevicePropertyImpl devp_;


        public DevicePropertyEditorImpl() : base()
        {
            InitializeComponent();
        }

        public DevicePropertyEditorImpl(DevicePropertyImpl devp) : this()
        {
            devp_ = devp as DevicePropertyImpl;

            InitializeInputDeviceList();
            InitializeOutputDeviceList();
            InitializeSamplingRate();
            InitializeBitsPerSample();
            InitializeChannelNum();
            InitializeOutputChannelNum();
            InitializeOutputVolume();

            ChkBox_InputEnable.Checked = devp.InputEnable.Value;
            CBox_InputDeviceList.SelectedItem = devp.InputDeviceId.Value;
            CBox_InputSamplingRate.SelectedItem = (uint)devp.InputSamplingRate.Value;
            CBox_InputBitsPerSample.SelectedItem = (uint)devp.InputBitsPerSample.Value;
            CBox_InputChannelNum.SelectedItem = devp.InputChannelNum.Value;

            ChkBox_OutputEnable.Checked = devp.OutputEnable.Value;
            CBox_OutputDeviceList.SelectedItem = devp.OutputDeviceId.Value;
            CBox_OutputSamplingRate.SelectedItem = (uint)devp.OutputSamplingRate.Value;
            CBox_OutputBitsPerSample.SelectedItem = (uint)devp.OutputBitsPerSample.Value;
            CBox_OutputChannelNum.SelectedItem = devp.OutputChannelNum.Value;
            CBox_OutputVolume.SelectedItem = devp.OutputVolume.Value;
        }

        private void InitializeInputDeviceList()
        {
            var dev_count = WaveIn.DeviceCount;

            CBox_InputDeviceList.BeginUpdate();
            {
                for (var dev_no = 0; dev_no < dev_count; dev_no++) {
                    CBox_InputDeviceList.Items.Add(new InputDeviceListItem(WaveIn.GetCapabilities(dev_no)));
                }
                if (CBox_InputDeviceList.Items.Count > 0) {
                    CBox_InputDeviceList.SelectedIndex = 0;
                }
            }
            CBox_InputDeviceList.EndUpdate();
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
            CBox_OutputSamplingRate.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_InputSamplingRate.Items.Add((uint)item);
                    CBox_OutputSamplingRate.Items.Add((uint)item);
                }
                CBox_InputSamplingRate.SelectedIndex = 0;
                CBox_OutputSamplingRate.SelectedIndex = 0;
            }
            CBox_InputSamplingRate.EndUpdate();
            CBox_OutputSamplingRate.EndUpdate();
        }

        private void InitializeBitsPerSample()
        {
            var items = new [] {
                8,
                16,
            };

            CBox_InputBitsPerSample.BeginUpdate();
            CBox_OutputBitsPerSample.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_InputBitsPerSample.Items.Add((uint)item);
                    CBox_OutputBitsPerSample.Items.Add((uint)item);
                }
                CBox_InputBitsPerSample.SelectedIndex = 0;
                CBox_OutputBitsPerSample.SelectedIndex = 0;
            }
            CBox_InputBitsPerSample.EndUpdate();
            CBox_OutputBitsPerSample.EndUpdate();
        }

        private void InitializeChannelNum()
        {
            var items = new [] {
                1, 2,
            };

            CBox_InputChannelNum.BeginUpdate();
            CBox_OutputChannelNum.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_InputChannelNum.Items.Add((uint)item);
                    CBox_OutputChannelNum.Items.Add((uint)item);
                }
                CBox_InputChannelNum.SelectedIndex = 0;
                CBox_OutputChannelNum.SelectedIndex = 0;
            }
            CBox_InputChannelNum.EndUpdate();
            CBox_OutputChannelNum.EndUpdate();
        }

        private void InitializeOutputChannelNum()
        {
            CBox_OutputChannelNum.BeginUpdate();
            {
                var item = CBox_OutputDeviceList.SelectedItem as OutputDeviceListItem;

                CBox_OutputChannelNum.Items.Clear();
                if (item != null) {
                    for (var num = 1; num <= 2; num++) {
                        CBox_OutputChannelNum.Items.Add(num);
                    }
                    CBox_OutputChannelNum.SelectedIndex = 0;
                }
            }
            CBox_OutputChannelNum.EndUpdate();
        }

        private void InitializeOutputDeviceList()
        {
            CBox_OutputDeviceList.BeginUpdate();
            {
                foreach (var dev in new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)) {
                    CBox_OutputDeviceList.Items.Add(new OutputDeviceListItem(dev));
                }
                if (CBox_OutputDeviceList.Items.Count > 0) {
                    CBox_OutputDeviceList.SelectedIndex = 0;
                }
            }
            CBox_OutputDeviceList.EndUpdate();
        }

        private void InitializeOutputVolume()
        {
            var items = new [] {
                10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 200
            };

            CBox_OutputVolume.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_OutputVolume.Items.Add((uint)item);
                }
                CBox_OutputVolume.SelectedIndex = 0;
            }
            CBox_OutputVolume.EndUpdate();
        }

        public override void Flush()
        {
            devp_.InputEnable.Value = ChkBox_InputEnable.Checked;
            try {
                devp_.InputDeviceId.Value = (CBox_InputDeviceList.SelectedItem as InputDeviceListItem).Value.NameGuid.ToString();
            } catch {
                devp_.InputDeviceId.Value = "";
            }
            devp_.InputSamplingRate.Value = uint.Parse(CBox_InputSamplingRate.Text);
            devp_.InputBitsPerSample.Value = uint.Parse(CBox_InputBitsPerSample.Text);
            devp_.InputChannelNum.Value = uint.Parse(CBox_InputChannelNum.Text);

            devp_.OutputEnable.Value = ChkBox_OutputEnable.Checked;
            try {
                devp_.OutputDeviceId.Value = (CBox_OutputDeviceList.SelectedItem as OutputDeviceListItem).Value.ID;
            } catch {
                devp_.OutputDeviceId.Value = "";
            }
            devp_.OutputSamplingRate.Value = uint.Parse(CBox_OutputSamplingRate.Text);
            devp_.OutputBitsPerSample.Value = uint.Parse(CBox_OutputBitsPerSample.Text);
            devp_.OutputChannelNum.Value = uint.Parse(CBox_OutputChannelNum.Text);
            devp_.OutputVolume.Value = uint.Parse(CBox_OutputVolume.Text);
        }
    }
}
