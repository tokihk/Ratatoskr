﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Device;

namespace Ratatoskr.Device.UsbCapture
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

            InitializeDeviceList();
			InitializeDataContentsType();

            SelectDevice(devp_.DeviceName.Value);

            ChkBox_Filter_ControlTransfer.Checked     = devp_.Filter_ControlTransfer.Value;
            ChkBox_Filter_BulkTransfer.Checked        = devp_.Filter_BulkTransfer.Value;
            ChkBox_Filter_InterruptTransfer.Checked   = devp_.Filter_InterruptTransfer.Value;
            ChkBox_Filter_IsochronousTransfer.Checked = devp_.Filter_IsochronousTransfer.Value;

            ChkBox_Info_UsbDeviceID.Checked = devp_.InfoOut_UsbDeviceID.Value;
            ChkBox_Info_EndPoint.Checked = devp_.InfoOut_EndPoint.Value;
            ChkBox_Info_IrpID.Checked = devp_.InfoOut_IrpID.Value;
            ChkBox_Info_FuncType.Checked = devp_.InfoOut_FunctionType.Value;
            ChkBox_Info_FuncParam.Checked = devp_.InfoOut_FunctionParam.Value;

			CBox_DataContentsType.SelectedItem = devp_.DataContentsType.Value;
        }

        private void InitializeDeviceList()
        {
            var devices = UsbPcapManager.GetDeviceList();
           
            CBox_DeviceList.BeginUpdate();
            {
                CBox_DeviceList.Items.AddRange(devices.ToArray());
                if (CBox_DeviceList.Items.Count == 0) {
                    CBox_DeviceList.Enabled = false;
                }
            }
            CBox_DeviceList.EndUpdate();
        }

		private void InitializeDataContentsType()
		{
			CBox_DataContentsType.BeginUpdate();
			{
				foreach (UsbCaptureDataContentsType item in Enum.GetValues(typeof(UsbCaptureDataContentsType))) {
					CBox_DataContentsType.Items.Add(item);
				}
				CBox_DataContentsType.SelectedIndex = 0;
			}
			CBox_DataContentsType.EndUpdate();
		}

        private void UpdateView()
        {
            ChkBox_Info_FuncParam.Enabled = ChkBox_Info_FuncType.Checked;
        }

        private void SelectDevice(string value)
        {
            CBox_DeviceList.SelectedItem = value;
            if ((CBox_DeviceList.SelectedIndex < 0) && (CBox_DeviceList.Items.Count > 0)) {
                CBox_DeviceList.SelectedIndex = 0;
            }
        }

        public override void Flush()
        {
            var obj = CBox_DeviceList.SelectedItem as UsbPcapDeviceObject;

            if (obj != null) {
                devp_.DeviceName.Value = obj.DeviceName;
            }

            devp_.Filter_ControlTransfer.Value = ChkBox_Filter_ControlTransfer.Checked;
            devp_.Filter_BulkTransfer.Value = ChkBox_Filter_BulkTransfer.Checked;
            devp_.Filter_InterruptTransfer.Value = ChkBox_Filter_InterruptTransfer.Checked;
            devp_.Filter_IsochronousTransfer.Value = ChkBox_Filter_IsochronousTransfer.Checked;

            devp_.InfoOut_UsbDeviceID.Value = ChkBox_Info_UsbDeviceID.Checked;
            devp_.InfoOut_EndPoint.Value = ChkBox_Info_EndPoint.Checked;
            devp_.InfoOut_IrpID.Value = ChkBox_Info_IrpID.Checked;
            devp_.InfoOut_FunctionType.Value = ChkBox_Info_FuncType.Checked;
            devp_.InfoOut_FunctionParam.Value = ChkBox_Info_FuncParam.Checked;

			devp_.DataContentsType.Value = (UsbCaptureDataContentsType)CBox_DataContentsType.SelectedItem;
        }

        private void CBox_DeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = CBox_DeviceList.SelectedItem as UsbPcapDeviceObject;

            if (item == null)return;

//            USBPcapCMD.enumerate_attached_devices(item.DeviceName, USBPcapCMD.EnumerationType.ENUMERATE_USBPCAPCMD);
        }

        private void ChkBox_Info_FuncType_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }
    }
}
