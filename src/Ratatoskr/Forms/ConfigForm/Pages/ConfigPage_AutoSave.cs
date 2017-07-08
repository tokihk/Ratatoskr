using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs.UserConfigs;

namespace Ratatoskr.Forms.ConfigForm.Pages
{
    internal partial class ConfigPage_AutoSave : ConfigPage
    {
        private class CBoxItem_SaveFormat
        {
            public AutoSaveFormatType Value { get; }

            public CBoxItem_SaveFormat(AutoSaveFormatType value)
            {
                Value = value;
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() == typeof(AutoSaveFormatType)) {
                    return ((AutoSaveFormatType)obj == Value);
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
                return (Value.ToString());
            }
        }


        public ConfigPage_AutoSave()
        {
            InitializeComponent();
            InitializeSaveFormat();
        }

        private void InitializeSaveFormat()
        {
            CBox_SaveFormat.BeginUpdate();
            {
                foreach (AutoSaveFormatType type in Enum.GetValues(typeof(AutoSaveFormatType))) {
                    CBox_SaveFormat.Items.Add(new CBoxItem_SaveFormat(type));
                }
            }
            CBox_SaveFormat.EndUpdate();
        }

        private void SelectSaveFormat(AutoSaveFormatType type)
        {
            CBox_SaveFormat.SelectedItem = type;
        }

        private void SetSavePrefix(string prefix)
        {
            TBox_SavePrefix.Text = prefix;
        }

        private void SetSaveDirectory(string prefix)
        {
            TBox_SaveDir.Text = prefix;
        }

        private void SelectSaveTimming(AutoSaveTimmingType type)
        {
            switch (type) {
                case AutoSaveTimmingType.Interval:
                    RBtn_Timing_Interval.Checked = true;
                    break;

                case AutoSaveTimmingType.FileSize:
                    RBtn_Timing_FileSize.Checked = true;
                    break;

                case AutoSaveTimmingType.PacketCount:
                    RBtn_Timing_PacketCount.Checked = true;
                    break;

                default:
                    RBtn_Timing_Interval.Checked = true;
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

        private AutoSaveFormatType GetSaveFormat()
        {
            return ((CBox_SaveFormat.SelectedItem as CBoxItem_SaveFormat).Value);
        }

        private string GetSavePrefix()
        {
            return (TBox_SavePrefix.Text);
        }

        private string GetSaveDirectory()
        {
            return (TBox_SaveDir.Text);
        }

        private AutoSaveTimmingType GetSaveTimming()
        {
            var type = AutoSaveTimmingType.Interval;

            if (RBtn_Timing_Interval.Checked) {
                type = AutoSaveTimmingType.Interval;
            } else if (RBtn_Timing_FileSize.Checked) {
                type = AutoSaveTimmingType.FileSize;
            } else if (RBtn_Timing_PacketCount.Checked) {
                type = AutoSaveTimmingType.PacketCount;
            }

            return (type);
        }

        protected override void OnLoadConfig()
        {
            SelectSaveFormat(Config.AutoSaveFormat.Value);
            SetSavePrefix(Config.AutoSavePrefix.Value);
            SetSaveDirectory(Config.AutoSaveDirectory.Value);
            SelectSaveTimming(Config.AutoSaveTimming.Value);
            SetSaveTimmingValue_Interval(Config.AutoSaveValue_Interval.Value);
            SetSaveTimmingValue_FileSize(Config.AutoSaveValue_FileSize.Value);
            SetSaveTimmingValue_PacketCount(Config.AutoSaveValue_PacketCount.Value);
        }

        protected override void OnFlushConfig()
        {
            Config.AutoSaveFormat.Value = GetSaveFormat();
            Config.AutoSavePrefix.Value = GetSavePrefix();
            Config.AutoSaveDirectory.Value = GetSaveDirectory();
            Config.AutoSaveTimming.Value = GetSaveTimming();
            Config.AutoSaveValue_Interval.Value = Num_Interval.Value;
            Config.AutoSaveValue_FileSize.Value = Num_FileSize.Value;
            Config.AutoSaveValue_PacketCount.Value = Num_PacketCount.Value;
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
