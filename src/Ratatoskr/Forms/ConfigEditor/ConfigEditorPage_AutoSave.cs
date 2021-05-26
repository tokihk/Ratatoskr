using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Config.Data.System;

namespace Ratatoskr.Forms.ConfigEditor
{
    internal partial class ConfigEditorPage_AutoSave : ConfigEditorPage
    {
        private class CBoxItem_SaveFormat
        {
            public AutoPacketSaveFormatType Value { get; }

            public CBoxItem_SaveFormat(AutoPacketSaveFormatType value)
            {
                Value = value;
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() == typeof(AutoPacketSaveFormatType)) {
                    return ((AutoPacketSaveFormatType)obj == Value);
                } else {
                    return (base.Equals(obj));
                }
            }

            public override int GetHashCode()
            {
                return (base.GetHashCode());
            }

            public override string ToString()
            {
                if (Value == AutoPacketSaveFormatType.Ratatoskr) {
                    return (ConfigManager.Fixed.ApplicationName.Value);
                } else {
                    return (Value.ToString());
                }
            }
        }

        public ConfigEditorPage_AutoSave() : base()
        {
            InitializeComponent();
            InitializeSaveFormat();
            InitializeSaveTarget();
        }

        private void InitializeSaveFormat()
        {
            CBox_SaveFormat.BeginUpdate();
            {
                foreach (AutoPacketSaveFormatType type in Enum.GetValues(typeof(AutoPacketSaveFormatType))) {
                    CBox_SaveFormat.Items.Add(new CBoxItem_SaveFormat(type));
                }
            }
            CBox_SaveFormat.EndUpdate();
        }

        private void InitializeSaveTarget()
        {
            CBox_SaveTarget.BeginUpdate();
            {
                foreach (AutoPacketSaveTargetType type in Enum.GetValues(typeof(AutoPacketSaveTargetType))) {
                    CBox_SaveTarget.Items.Add(type);
                }
                if (CBox_SaveTarget.Items.Count > 0) {
                    CBox_SaveTarget.SelectedIndex = 0;
                }
            }
            CBox_SaveTarget.EndUpdate();
        }

        private void SelectSaveTimming(AutoPacketSaveTimmingType type)
        {
            switch (type) {
                case AutoPacketSaveTimmingType.NoSave:
                    RBtn_Timing_None.Checked = true;
                    break;

                case AutoPacketSaveTimmingType.Interval:
                    RBtn_Timing_Interval.Checked = true;
                    break;

                case AutoPacketSaveTimmingType.FileSize:
                    RBtn_Timing_FileSize.Checked = true;
                    break;

                case AutoPacketSaveTimmingType.PacketCount:
                    RBtn_Timing_PacketCount.Checked = true;
                    break;

                default:
                    RBtn_Timing_None.Checked = true;
                    break;
            }
        }

        private AutoPacketSaveTimmingType GetSaveTimming()
        {
            var type = AutoPacketSaveTimmingType.Interval;

            if (RBtn_Timing_None.Checked) {
                type = AutoPacketSaveTimmingType.NoSave;
            } else if (RBtn_Timing_Interval.Checked) {
                type = AutoPacketSaveTimmingType.Interval;
            } else if (RBtn_Timing_FileSize.Checked) {
                type = AutoPacketSaveTimmingType.FileSize;
            } else if (RBtn_Timing_PacketCount.Checked) {
                type = AutoPacketSaveTimmingType.PacketCount;
            }

            return (type);
        }

        protected override void OnLoadConfig()
        {
            CBox_SaveFormat.SelectedItem = Config.System.AutoPacketSave.SaveFormat.Value;
            TBox_SavePrefix.Text = Config.System.AutoPacketSave.SavePrefix.Value.Trim();
            TBox_SaveDir.Text = Config.System.AutoPacketSave.SaveDirectory.Value.Trim();
            CBox_SaveTarget.SelectedItem = Config.System.AutoPacketSave.SaveTarget.Value;
            SelectSaveTimming(Config.System.AutoPacketSave.SaveTimming.Value);
            Num_Interval.Value = Config.System.AutoPacketSave.SaveValue_Interval.Value;
            Num_FileSize.Value = Config.System.AutoPacketSave.SaveValue_FileSize.Value;
            Num_PacketCount.Value = Config.System.AutoPacketSave.SaveValue_PacketCount.Value;
        }

        protected override void OnFlushConfig()
        {
            Config.System.AutoPacketSave.SaveFormat.Value = (CBox_SaveFormat.SelectedItem as CBoxItem_SaveFormat).Value;
            Config.System.AutoPacketSave.SavePrefix.Value = TBox_SavePrefix.Text.Trim();
            Config.System.AutoPacketSave.SaveDirectory.Value = TBox_SaveDir.Text.Trim();
            Config.System.AutoPacketSave.SaveTarget.Value = (AutoPacketSaveTargetType)CBox_SaveTarget.SelectedItem;
            Config.System.AutoPacketSave.SaveTimming.Value = GetSaveTimming();
            Config.System.AutoPacketSave.SaveValue_Interval.Value = Num_Interval.Value;
            Config.System.AutoPacketSave.SaveValue_FileSize.Value = Num_FileSize.Value;
            Config.System.AutoPacketSave.SaveValue_PacketCount.Value = Num_PacketCount.Value;
        }

        private void Btn_SaveDir_Ref_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();

            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            dialog.SelectedPath = TBox_SaveDir.Text;
            dialog.ShowNewFolderButton = true;

            if (dialog.ShowDialog() != DialogResult.OK)return;

            TBox_SaveDir.Text = dialog.SelectedPath;
        }
    }
}
