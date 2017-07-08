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

namespace Ratatoskr.Devices.MicInput
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


        private DevicePropertyImpl devp_;


        public DevicePropertyEditorImpl() : base()
        {
            InitializeComponent();
        }

        public DevicePropertyEditorImpl(DevicePropertyImpl devp) : this()
        {
            devp_ = devp as DevicePropertyImpl;

            InitializeInputDeviceList();
            InitializeSamplingRate();
            InitializeBitsPerSample();
            InitializeChannelNum();

            CBox_InputDeviceList.SelectedItem = devp.InputDeviceId.Value;
            CBox_SamplingRate.SelectedItem = (uint)devp.SamplingRate.Value;
            CBox_BitsPerSample.SelectedItem = (uint)devp.BitsPerSample.Value;
            CBox_ChannelNum.SelectedItem = devp.ChannelNum.Value;
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

            CBox_SamplingRate.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_SamplingRate.Items.Add((uint)item);
                }
                CBox_SamplingRate.SelectedIndex = 0;
            }
            CBox_SamplingRate.EndUpdate();
        }

        private void InitializeBitsPerSample()
        {
            var items = new [] {
                8,
                16,
            };

            CBox_BitsPerSample.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_BitsPerSample.Items.Add((uint)item);
                }
                CBox_BitsPerSample.SelectedIndex = 0;
            }
            CBox_BitsPerSample.EndUpdate();
        }

        private void InitializeChannelNum()
        {
            CBox_ChannelNum.BeginUpdate();
            {
                var item = CBox_InputDeviceList.SelectedItem as InputDeviceListItem;

                CBox_ChannelNum.Items.Clear();
                if (item != null) {
                    for (var num = 1; num <= item.Value.Channels; num++) {
                        CBox_ChannelNum.Items.Add(num);
                    }
                    CBox_ChannelNum.SelectedIndex = 0;
                }
            }
            CBox_ChannelNum.EndUpdate();
        }

        public override void Flush()
        {
            try {
                devp_.InputDeviceId.Value = (CBox_InputDeviceList.SelectedItem as InputDeviceListItem).Value.NameGuid.ToString();
            } catch {
                devp_.InputDeviceId.Value = "";
            }
            devp_.SamplingRate.Value = uint.Parse(CBox_SamplingRate.Text);
            devp_.BitsPerSample.Value = uint.Parse(CBox_BitsPerSample.Text);
            devp_.ChannelNum.Value = uint.Parse(CBox_ChannelNum.Text);
        }
    }
}
