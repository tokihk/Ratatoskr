using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.SystemConfigs;

namespace Ratatoskr.Forms.OptionEditForm
{
    internal partial class OptionEditPage_AutoSave : OptionEditPage
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


        public OptionEditPage_AutoSave()
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

        private void SelectSaveFormat(AutoPacketSaveFormatType type)
        {
            CBox_SaveFormat.SelectedItem = type;
        }

        private void SetSavePrefix(string prefix)
        {
            TBox_SavePrefix.Text = prefix.Trim();
        }

        private void SetSaveDirectory(string prefix)
        {
            TBox_SaveDir.Text = prefix.Trim();
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

        private void SetSaveTimmingValue_Interval(decimal value)
        {
            Num_Interval.Value = value;
        }

        private void SetSaveTimmingValue_FileSize(decimal value)
        {
            Num_FileSize.Value = value;
        }

        private void SetSaveTimmingValue_PacketCount(decimal value)
        {
            Num_PacketCount.Value = value;
        }

        private AutoPacketSaveFormatType GetSaveFormat()
        {
            return ((CBox_SaveFormat.SelectedItem as CBoxItem_SaveFormat).Value);
        }

        private string GetSavePrefix()
        {
            return (TBox_SavePrefix.Text.Trim());
        }

        private string GetSaveDirectory()
        {
            return (TBox_SaveDir.Text.Trim());
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
            SelectSaveFormat(Config.AutoSaveFormat);
            SetSavePrefix(Config.AutoSavePrefix);
            SetSaveDirectory(Config.AutoSaveDirectory);
            CBox_SaveTarget.SelectedItem = Config.AutoSaveTarget;
            SelectSaveTimming(Config.AutoSaveTimming);
            SetSaveTimmingValue_Interval(Config.AutoSaveValue_Interval);
            SetSaveTimmingValue_FileSize(Config.AutoSaveValue_FileSize);
            SetSaveTimmingValue_PacketCount(Config.AutoSaveValue_PacketCount);
        }

        protected override void OnFlushConfig()
        {
            Config.AutoSaveFormat = GetSaveFormat();
            Config.AutoSavePrefix = GetSavePrefix();
            Config.AutoSaveDirectory = GetSaveDirectory();
            Config.AutoSaveTarget = (AutoPacketSaveTargetType)CBox_SaveTarget.SelectedItem;
            Config.AutoSaveTimming = GetSaveTimming();
            Config.AutoSaveValue_Interval = Num_Interval.Value;
            Config.AutoSaveValue_FileSize = Num_FileSize.Value;
            Config.AutoSaveValue_PacketCount = Num_PacketCount.Value;
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
