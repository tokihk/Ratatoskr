using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.General.Pcap;

namespace Ratatoskr.FileFormat.PacketLog_Pcap
{
	internal partial class FileFormatReaderOptionEditorImpl : FileFormatOptionEditor
	{
		public FileFormatReaderOptionEditorImpl()
		{
			InitializeComponent();
			InitializeComboBoxForEnum<PcapPacketInfoType>(CBox_PacketInfoType);
			InitializeComboBoxForEnum<PcapPacketSourceType>(CBox_PacketSourceType);
			InitializeComboBoxForEnum<PcapPacketDestinationType>(CBox_PacketDestinationType);
			InitializeComboBoxForEnum<PcapPacketDataType>(CBox_PacketDataType);
		}

		private void InitializeComboBoxForEnum<EnumType>(ComboBox control)
		{
			control.BeginUpdate();
			{
				control.Items.Clear();
				foreach (EnumType type in Enum.GetValues(typeof(EnumType))) {
					control.Items.Add(type);
				}
				control.SelectedIndex = 0;
			}
			control.EndUpdate();
		}

		protected override void OnLoadOption(FileFormatOption option)
		{
			if (option is FileFormatReaderOptionImpl option_i) {
				TBox_PcapFilter.Text = option_i.PcapFilter.Trim();

				CBox_PacketInfoType.SelectedItem = option_i.PacketInfoType;
				CBox_PacketSourceType.SelectedItem = option_i.PacketSourceType;
				CBox_PacketDestinationType.SelectedItem = option_i.PacketDestinationType;
				CBox_PacketDataType.SelectedItem = option_i.PacketDataType;
			}
		}

		protected override void OnBackupOption(FileFormatOption option)
		{
			if (option is FileFormatReaderOptionImpl option_i) {
				option_i.PcapFilter = TBox_PcapFilter.Text.Trim();

				option_i.PacketInfoType = (PcapPacketInfoType)CBox_PacketInfoType.SelectedItem;
				option_i.PacketSourceType = (PcapPacketSourceType)CBox_PacketSourceType.SelectedItem;
				option_i.PacketDestinationType = (PcapPacketDestinationType)CBox_PacketDestinationType.SelectedItem;
				option_i.PacketDataType = (PcapPacketDataType)CBox_PacketDataType.SelectedItem;
			}
		}
	}
}
