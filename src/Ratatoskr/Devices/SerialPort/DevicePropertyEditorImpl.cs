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

namespace Ratatoskr.Devices.SerialPort
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

            InitializePortList();
            InitializeBaudRate();
            InitializeParity();
            InitializeDataBits();
            InitializeStopBits();
            InitializeFlowControl();

            CBox_PortList.SelectedItem = devp_.PortName.Value;
            CBox_BaudRate.Text = devp_.BaudRate.Value.ToString();
            CBox_Parity.SelectedItem = devp_.Parity.Value;
            CBox_DataBits.SelectedItem = (byte)devp_.DataBits.Value;
            CBox_StopBits.SelectedItem = devp_.StopBits.Value;

            ChkBox_fOutxCtsFlow.Checked = devp_.fOutxCtsFlow.Value;
            ChkBox_fOutxDsrFlow.Checked = devp_.fOutxDsrFlow.Value;
            ChkBox_fDsrSensitivity.Checked = devp_.fDsrSensitivity.Value;
            ChkBox_fTXContinueOnXoff.Checked = devp_.fTXContinueOnXoff.Value;
            ChkBox_fOutX.Checked = devp_.fOutX.Value;
            ChkBox_fInX.Checked = devp_.fInX.Value;

            CBox_fDtrControl.SelectedItem = devp_.fDtrControl.Value;
            CBox_fRtsControl.SelectedItem = devp_.fRtsControl.Value;

            Num_XonLim.Value = devp_.XonLim.Value;
            Num_XoffLim.Value = devp_.XoffLim.Value;
            TBox_XonChar.Text = devp_.XonChar.Value.ToString();
            TBox_XoffChar.Text = devp_.XoffChar.Value.ToString();
        }

        private void InitializePortList()
        {
            var ports = ComPortInfo.GetSerialPortList();
           
            CBox_PortList.BeginUpdate();
            {
                CBox_PortList.Items.AddRange(ports.ToArray());
                CBox_PortList.SelectedIndex = 0;
            }
            CBox_PortList.EndUpdate();
        }

        private void InitializeBaudRate()
        {
            var items = new [] {
                110,
                300,
                600,
                1200,
                2400,
                4800,
                9600,
                14400,
                19200,
                38400,
                56000,
                57600,
                115200,
                128000,
                256000,
            };

            CBox_BaudRate.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_BaudRate.Items.Add(item);
                }
                CBox_BaudRate.SelectedIndex = 0;
            }
            CBox_BaudRate.EndUpdate();
        }

        private void InitializeParity()
        {
            CBox_Parity.BeginUpdate();
            {
                foreach (var obj in Enum.GetValues(typeof(Parity))) {
                    CBox_Parity.Items.Add(obj);
                }
                CBox_Parity.SelectedIndex = 0;
            }
            CBox_Parity.EndUpdate();
        }

        private void InitializeDataBits()
        {
            var items = new byte[]
            {
                5,
                6,
                7,
                8,
            };

            CBox_DataBits.BeginUpdate();
            {
                foreach (var item in items) {
                    CBox_DataBits.Items.Add(item);
                }
                CBox_DataBits.SelectedIndex = 0;
            }
            CBox_DataBits.EndUpdate();
        }

        private void InitializeStopBits()
        {
            CBox_StopBits.BeginUpdate();
            {
                foreach (var obj in Enum.GetValues(typeof(StopBits))) {
                    CBox_StopBits.Items.Add(obj);
                }
                CBox_StopBits.SelectedIndex = 0;
            }
            CBox_StopBits.EndUpdate();
        }

        private void InitializeFlowControl()
        {
            CBox_fDtrControl.BeginUpdate();
            {
                foreach (var obj in Enum.GetValues(typeof(fDtrControlType))) {
                    CBox_fDtrControl.Items.Add(obj);
                }
                CBox_fDtrControl.SelectedIndex = 0;
            }
            CBox_fDtrControl.EndUpdate();

            CBox_fRtsControl.BeginUpdate();
            {
                foreach (var obj in Enum.GetValues(typeof(fRtsControlType))) {
                    CBox_fRtsControl.Items.Add(obj);
                }
                CBox_fRtsControl.SelectedIndex = 0;
            }
            CBox_fRtsControl.EndUpdate();
        }

        public override void Flush()
        {
            devp_.PortName.Value = (CBox_PortList.SelectedItem as ComPortInfo).DeviceName;
            devp_.BaudRate.Value = uint.Parse(CBox_BaudRate.Text);
            devp_.Parity.Value = (Parity)CBox_Parity.SelectedItem;
            devp_.DataBits.Value = (byte)CBox_DataBits.SelectedItem;
            devp_.StopBits.Value = (StopBits)CBox_StopBits.SelectedItem;

            devp_.fOutxCtsFlow.Value = ChkBox_fOutxCtsFlow.Checked;
            devp_.fOutxDsrFlow.Value = ChkBox_fOutxDsrFlow.Checked;
            devp_.fDsrSensitivity.Value = ChkBox_fDsrSensitivity.Checked;
            devp_.fTXContinueOnXoff.Value = ChkBox_fTXContinueOnXoff.Checked;
            devp_.fOutX.Value = ChkBox_fOutX.Checked;
            devp_.fInX.Value = ChkBox_fInX.Checked;

            devp_.fDtrControl.Value = (fDtrControlType)CBox_fDtrControl.SelectedItem;
            devp_.fRtsControl.Value = (fRtsControlType)CBox_fRtsControl.SelectedItem;

            devp_.XonLim.Value = Num_XonLim.Value;
            devp_.XoffLim.Value = Num_XoffLim.Value;
            devp_.XonChar.Value = byte.Parse(TBox_XonChar.Text);
            devp_.XoffChar.Value = byte.Parse(TBox_XoffChar.Text);
        }
    }
}
